using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001B5 RID: 437
	internal class PhotoAssemble : ImageAssembleBase
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x00081070 File Offset: 0x0007F270
		public PhotoAssemble(IWebGreaseContext context) : base(context)
		{
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00081079 File Offset: 0x0007F279
		internal override string DefaultExtension
		{
			get
			{
				return ".jpg";
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00081080 File Offset: 0x0007F280
		internal override ImageType Type
		{
			get
			{
				return ImageType.Photo;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00081083 File Offset: 0x0007F283
		protected override ImageFormat Format
		{
			get
			{
				return ImageFormat.Jpeg;
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0008109C File Offset: 0x0007F29C
		protected override void SaveImage(Bitmap newImage)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			IEnumerable<ImageCodecInfo> source = from e in imageEncoders
			where e.MimeType == "image/jpeg"
			select e;
			ImageCodecInfo encoder = source.First<ImageCodecInfo>();
			Encoder quality = Encoder.Quality;
			using (EncoderParameter encoderParameter = new EncoderParameter(quality, 100L))
			{
				using (EncoderParameters encoderParameters = new EncoderParameters(1))
				{
					encoderParameters.Param[0] = encoderParameter;
					newImage.Save(base.AssembleFileName, encoder, encoderParameters);
				}
			}
		}

		// Token: 0x04000BCC RID: 3020
		private const long DefaultJpegQuality = 100L;
	}
}
