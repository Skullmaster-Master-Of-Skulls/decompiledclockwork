using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001B4 RID: 436
	internal class NotSupportedAssemble : ImageAssembleBase
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x00080FF2 File Offset: 0x0007F1F2
		public NotSupportedAssemble(IWebGreaseContext context) : base(context)
		{
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00080FFB File Offset: 0x0007F1FB
		internal override ImageType Type
		{
			get
			{
				return ImageType.NotSupported;
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00080FFE File Offset: 0x0007F1FE
		internal override string DefaultExtension
		{
			get
			{
				return ".bmp";
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00081005 File Offset: 0x0007F205
		protected override ImageFormat Format
		{
			get
			{
				return ImageFormat.Bmp;
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0008100C File Offset: 0x0007F20C
		internal override bool Assemble(List<BitmapContainer> inputImages)
		{
			foreach (BitmapContainer bitmapContainer in inputImages)
			{
				base.ImageXmlMap.AppendToXml(bitmapContainer.InputImage.AbsoluteImagePath, "Not supported");
			}
			return false;
		}
	}
}
