using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x0200051C RID: 1308
	internal class ConvertToOperation : ImageOperation, IImageOperation
	{
		// Token: 0x06002EB5 RID: 11957 RVA: 0x00098B70 File Offset: 0x00096D70
		public ConvertToOperation(ImageFormat format)
		{
			this.Format = format;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00098B7F File Offset: 0x00096D7F
		public ConvertToOperation(ImageFormat format, int index) : base(index)
		{
			this.Format = format;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00098B90 File Offset: 0x00096D90
		public Image Apply(Image image)
		{
			Image result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				image.Save(memoryStream, this.Format);
				result = new Bitmap(memoryStream);
			}
			return result;
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x00098BD4 File Offset: 0x00096DD4
		// (set) Token: 0x06002EB9 RID: 11961 RVA: 0x00098BDC File Offset: 0x00096DDC
		public ImageFormat Format { get; set; }

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06002EBA RID: 11962 RVA: 0x00098BE5 File Offset: 0x00096DE5
		public string Name
		{
			get
			{
				return "SaveAs";
			}
		}
	}
}
