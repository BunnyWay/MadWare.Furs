using MadWare.Furs.Requests;
using MadWare.Furs.Responses;

namespace MadWare.Furs.Encryption
{
    public interface IDigitalSignatureProvider
    {
        /// <summary>
        /// Sign payload
        /// </summary>
        /// <param name="payload">Payload to add signature</param>
        /// <param name="e">Serialized representation of the payload</param>
        /// <returns>Signed payload</returns>
        string SignRequest(string requestPayload, BaseRequestBody b);

        /// <summary>
        /// Verifies response signature
        /// </summary>
        /// <param name="responsePayload"></param>
        /// <returns></returns>
        bool VerifyResponseSignature(string responsePayload, BaseResponseBody b);
    }
}