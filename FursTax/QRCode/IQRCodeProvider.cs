using MadWare.Furs.Models.Common;

namespace MadWare.Furs.QRCode
{
    public interface IQRCodeProvider
    {
        string GenerateQRCode(QRCodeModel model);
    }
}