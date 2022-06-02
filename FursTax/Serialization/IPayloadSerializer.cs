using MadWare.Furs.Requests;
using MadWare.Furs.Responses;
using System;

namespace MadWare.Furs.Serialization
{
    public interface IPayloadSerializer
    {
        string SerializeRequest(BaseRequestBody b);

        BaseResponseBody DeserializeResponse(string r, Type typeOfRequestBody);
    }
}