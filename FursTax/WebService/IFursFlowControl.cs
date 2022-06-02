using MadWare.Furs.Requests;
using MadWare.Furs.Responses;
using System.Threading.Tasks;

namespace MadWare.Furs.WebService
{
    public interface IFursFlowControl<TRequest, TResponse> where TRequest : BaseRequestBody
                                                           where TResponse : BaseResponseBody
    {
        Task OnRequestPayloadSerialized(string requestPayload, TRequest requestBody);

        Task OnRequestPayloadSigned(string signedRequestPayload, TRequest requestBody);

        Task OnSuccessfulResponse(string responsePayload, TResponse responseBody);

        Task OnErrorResponse(string responsePayload, TResponse responseBody);
    }

    public interface IFursFlowControl<TResponse> : IFursFlowControl<BaseRequestBody, TResponse> where TResponse : BaseResponseBody
    {
    }

    public interface IFursFlowControl : IFursFlowControl<BaseResponseBody>
    {
    }
}