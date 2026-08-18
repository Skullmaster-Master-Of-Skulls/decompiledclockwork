using System;
using System.Drawing.Imaging;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AB0 RID: 2736
	internal class ImageHandler
	{
		// Token: 0x06006808 RID: 26632 RVA: 0x00185530 File Offset: 0x00183730
		internal static Escher.RecordType GetImageType(ImageFormat format)
		{
			Escher.RecordType result = Escher.RecordType.MSOFBTUNKNOWN;
			if (format != null)
			{
				if (format.Equals(ImageFormat.Bmp))
				{
					result = Escher.RecordType.MSOFBTBLIP_DIB;
				}
				if (format.Equals(ImageFormat.Jpeg))
				{
					result = Escher.RecordType.MSOFBTBLIP_JPEG;
				}
				if (format.Equals(ImageFormat.Gif))
				{
					result = Escher.RecordType.MSOFBTBLIP_GIF;
				}
				if (format.Equals(ImageFormat.Png))
				{
					result = Escher.RecordType.MSOFBTBLIP_GIF;
				}
			}
			return result;
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x0018558F File Offset: 0x0018378F
		internal static string GetUniqueImageID()
		{
			return "Image_" + ImageHandler.imageCount++;
		}

		// Token: 0x04001B1D RID: 6941
		private static int imageCount;
	}
}
