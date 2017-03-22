using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace OuiHop.Helpers
{
   public static class ImageHelper
    {
        public static async Task<BitmapImage> ByteArrayToBitmapImage(byte[] byteArray)
        {
            var bitmapImage = new BitmapImage();
            var stream = new InMemoryRandomAccessStream();
            await stream.WriteAsync(byteArray.AsBuffer());
            stream.Seek(0);
            bitmapImage.SetSource(stream);
            return bitmapImage;
        }
        public static BitmapImage base64image(string base64string)
        {
            var imageBytes = Convert.FromBase64String(base64string);
            using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes((byte[])imageBytes);
                    writer.StoreAsync().GetResults();
                }

                var image = new BitmapImage();
                image.SetSource(ms);
                return image;
            }
        }
    }
}
