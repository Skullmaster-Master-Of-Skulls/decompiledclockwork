using System;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x020004F2 RID: 1266
	public class ImgJBIG2 : Image
	{
		// Token: 0x06002B49 RID: 11081 RVA: 0x00105FC8 File Offset: 0x00104FC8
		private ImgJBIG2(Image image) : base(image)
		{
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x00105FD1 File Offset: 0x00104FD1
		public ImgJBIG2() : base(null)
		{
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x00105FDC File Offset: 0x00104FDC
		public ImgJBIG2(int width, int height, byte[] data, byte[] globals) : base(null)
		{
			this.type = 36;
			this.originalType = 9;
			this.scaledHeight = (float)height;
			this.Top = this.scaledHeight;
			this.scaledWidth = (float)width;
			this.Right = this.scaledWidth;
			this.bpc = 1;
			this.colorspace = 1;
			this.rawData = data;
			this.plainWidth = this.Width;
			this.plainHeight = base.Height;
			if (globals != null)
			{
				this.global = globals;
				try
				{
					this.globalHash = PdfEncryption.DigestComputeHash("MD5", this.global);
				}
				catch
				{
				}
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002B4C RID: 11084 RVA: 0x0010608C File Offset: 0x0010508C
		public byte[] GlobalBytes
		{
			get
			{
				return this.global;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x00106094 File Offset: 0x00105094
		public byte[] GlobalHash
		{
			get
			{
				return this.globalHash;
			}
		}

		// Token: 0x04001DDE RID: 7646
		private byte[] global;

		// Token: 0x04001DDF RID: 7647
		private byte[] globalHash;
	}
}
