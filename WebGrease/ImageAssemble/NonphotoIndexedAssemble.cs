using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001B2 RID: 434
	internal class NonphotoIndexedAssemble : ImageAssembleBase
	{
		// Token: 0x0600164A RID: 5706 RVA: 0x00080F4B File Offset: 0x0007F14B
		public NonphotoIndexedAssemble(IWebGreaseContext context) : base(context)
		{
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x00080F54 File Offset: 0x0007F154
		internal override ImageType Type
		{
			get
			{
				return ImageType.NonphotoIndexed;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00080F57 File Offset: 0x0007F157
		internal override string DefaultExtension
		{
			get
			{
				return ".png";
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00080F5E File Offset: 0x0007F15E
		protected override ImageFormat Format
		{
			get
			{
				return ImageFormat.Png;
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00080F68 File Offset: 0x0007F168
		protected override void SaveImage(Bitmap newImage)
		{
			if (!File.Exists(base.AssembleFileName))
			{
				Bitmap bitmap = null;
				try
				{
					bitmap = ColorQuantizer.Quantize(newImage, PixelFormat.Format8bppIndexed);
					base.SaveImage(bitmap);
					base.OptimizeImage();
				}
				finally
				{
					if (bitmap != null)
					{
						bitmap.Dispose();
					}
				}
			}
		}
	}
}
