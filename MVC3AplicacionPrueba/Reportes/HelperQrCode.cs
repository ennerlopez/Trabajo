using System.IO;
using MessagingToolkit.QRCode.Codec;

namespace MVC3AplicacionPrueba.Reportes
{
    public static class HelperQrCode
    {

        public static byte[] generar(string input, int qrlevel = 10)
        {
            string toenc = input;
            var qe = new QRCodeEncoder();
            qe.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qe.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
          
            qe.QRCodeVersion = qrlevel;
            var bm = qe.Encode(toenc);



            return imageToByteArray(bm);
        }

        private  static byte[] imageToByteArray(System.Drawing.Bitmap imageIn)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            
            return ms.ToArray();
        }
 
    }
}