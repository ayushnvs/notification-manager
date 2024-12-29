using Android.Graphics;
using Android.Graphics.Drawables;

namespace NotificationManager.Platforms.Android.Helpers;

public class ImageHelper
{
    public static byte[] DrawableToByteArray(Drawable drawable)
    {
        Bitmap bitmap = Bitmap.CreateBitmap(drawable.IntrinsicWidth, drawable.IntrinsicHeight, Bitmap.Config.Argb8888);
        Canvas canvas = new Canvas(bitmap);
        drawable.SetBounds(0, 0, canvas.Width, canvas.Height);
        drawable.Draw(canvas);

        using (MemoryStream stream = new MemoryStream())
        {
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            return stream.ToArray();
        }
    }
}
