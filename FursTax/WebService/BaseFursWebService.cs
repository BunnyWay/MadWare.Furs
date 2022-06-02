using MadWare.Furs.Encryption;
using MadWare.Furs.Http;
using MadWare.Furs.Requests;
using MadWare.Furs.Responses;
using MadWare.Furs.Serialization;
using MadWare.Furs.ZOI;
using System;
using System.Threading.Tasks;

namespace MadWare.Furs.WebService
{
    public abstract class BaseFursWebService
    {
        protected string Url { get; set; }

        private IPayloadSerializer serializer;
        private IZOIProvider zoiProvider;
        private IDigitalSignatureProvider digSigProvider;
        private IHttpService httpService;

        public BaseFursWebService(string url,
                                  IPayloadSerializer serializer,
                                  IZOIProvider zoiProvider,
                                  IDigitalSignatureProvider digSigProvider,
                                  IHttpService httpService)
        {
            this.Url = url;
            this.serializer = serializer;
            this.zoiProvider = zoiProvider;
            this.digSigProvider = digSigProvider;
            this.httpService = httpService;
        }

        /// <summary>
        /// Sets custom implementation of IPayloadSerializer
        /// </summary>
        /// <param name="ps"></param>
        public void SetPayloadSerializer(IPayloadSerializer ps)
        {
            this.serializer = ps;
        }

        /// <summary>
        /// Sets custom implementation of IZOIProvider
        /// </summary>
        /// <param name="zp"></param>
        public void SetZOIProvider(IZOIProvider zp)
        {
            this.zoiProvider = zp;
        }

        /// <summary>
        /// Sets custom implementation of IDigitalSignatureProvider
        /// </summary>
        /// <param name="dsp"></param>
        public void SetDigitalSignatureProvider(IDigitalSignatureProvider dsp)
        {
            this.digSigProvider = dsp;
        }

        /// <summary>
        /// Sets custom implementation of IHttpService
        /// </summary>
        /// <param name="hs"></param>
        public void SetHttpService(IHttpService hs)
        {
            this.httpService = hs;
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(TRequest requestBody, IFursFlowControl<TRequest, TResponse> flowControl = null) where TRequest : BaseRequestBody
                                                                                            where TResponse : BaseResponseBody
        {
            //validate request first
            requestBody.ValidateBody();

            //check if zoi calculation is needed
            if (this.zoiProvider.MustCalculateZOI(requestBody))
                this.zoiProvider.CalculateZOI(requestBody);

            //serialize payload
            string reqPayload = this.serializer.SerializeRequest(requestBody);
            if (flowControl != null)
                await flowControl.OnRequestPayloadSerialized(reqPayload, requestBody).ConfigureAwait(false);

            //sign request payload
            string signedReqPayload = this.digSigProvider.SignRequest(reqPayload, requestBody);
            if (flowControl != null)
                await flowControl.OnRequestPayloadSigned(signedReqPayload, requestBody).ConfigureAwait(false);

            //send request
            string responsePayload = await this.httpService.SendRequest(this.Url, signedReqPayload, requestBody).ConfigureAwait(false);

            //deserialize response
            TResponse responseBody = (TResponse)this.serializer.DeserializeResponse(responsePayload, requestBody.GetType());

            //verify response payload signature
            if (!this.digSigProvider.VerifyResponseSignature(responsePayload, responseBody))
                throw new UnauthorizedAccessException("Response signature is invalid.");

            if (flowControl != null)
                if (!responseBody.IsErrorResponse())
                    await flowControl.OnSuccessfulResponse(responsePayload, responseBody).ConfigureAwait(false);
                else
                    await flowControl.OnErrorResponse(responsePayload, responseBody).ConfigureAwait(false);

            return responseBody;
        }

        public async Task<TResponse> SendRequestAsync<TResponse>(BaseRequestBody requestBody, IFursFlowControl<TResponse> flowControl = null) where TResponse : BaseResponseBody
        {
            return await this.SendRequestAsync<BaseRequestBody, TResponse>(requestBody, flowControl).ConfigureAwait(false);
        }

        public async Task<BaseResponseBody> SendRequestAsync(BaseRequestBody requestBody, IFursFlowControl flowControl = null)
        {
            return await this.SendRequestAsync<BaseResponseBody>(requestBody, flowControl);
        }
    }
}