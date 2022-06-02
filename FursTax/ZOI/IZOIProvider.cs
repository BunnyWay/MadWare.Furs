using MadWare.Furs.Requests;

namespace MadWare.Furs.ZOI
{
    public interface IZOIProvider
    {
        bool MustCalculateZOI(BaseRequestBody b);

        void CalculateZOI(BaseRequestBody b);
    }
}