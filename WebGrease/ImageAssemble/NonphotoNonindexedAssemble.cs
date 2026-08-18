using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001B3 RID: 435
	internal class NonphotoNonindexedAssemble : ImageAssembleBase
	{
		// Token: 0x0600164F RID: 5711 RVA: 0x00080FBC File Offset: 0x0007F1BC
		public NonphotoNonindexedAssemble(IWebGreaseContext context) : base(context)
		{
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00080FC5 File Offset: 0x0007F1C5
		internal override ImageType Type
		{
			get
			{
				return ImageType.NonphotoNonindexed;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00080FC8 File Offset: 0x0007F1C8
		internal override string DefaultExtension
		{
			get
			{
				return ".png";
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00080FCF File Offset: 0x0007F1CF
		protected override ImageFormat Format
		{
			get
			{
				return ImageFormat.Png;
			}
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00080FD6 File Offset: 0x0007F1D6
		protected override void SaveImage(Bitmap newImage)
		{
			if (!File.Exists(base.AssembleFileName))
			{
				base.SaveImage(newImage);
				base.OptimizeImage();
			}
		}
	}
}
