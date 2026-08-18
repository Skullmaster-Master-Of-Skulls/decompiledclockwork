using System;
using System.IO;
using System.Net;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x020005F8 RID: 1528
	public class Jpeg2000 : Image
	{
		// Token: 0x06003415 RID: 13333 RVA: 0x00142746 File Offset: 0x00141746
		public Jpeg2000(Image image) : base(image)
		{
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x0014274F File Offset: 0x0014174F
		public Jpeg2000(Uri url) : base(url)
		{
			this.ProcessParameters();
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x0014275E File Offset: 0x0014175E
		public Jpeg2000(byte[] img) : base(null)
		{
			this.rawData = img;
			this.originalData = img;
			this.ProcessParameters();
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x0014277B File Offset: 0x0014177B
		public Jpeg2000(byte[] img, float width, float height) : this(img)
		{
			this.scaledWidth = width;
			this.scaledHeight = height;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x00142794 File Offset: 0x00141794
		private int Cio_read(int n)
		{
			int num = 0;
			for (int i = n - 1; i >= 0; i--)
			{
				num += this.inp.ReadByte() << (i << 3);
			}
			return num;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x001427C8 File Offset: 0x001417C8
		public void Jp2_read_boxhdr()
		{
			this.boxLength = this.Cio_read(4);
			this.boxType = this.Cio_read(4);
			if (this.boxLength == 1)
			{
				if (this.Cio_read(4) != 0)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("cannot.handle.box.sizes.higher.than.2.32"));
				}
				this.boxLength = this.Cio_read(4);
				if (this.boxLength == 0)
				{
					throw new IOException(MessageLocalization.GetComposedMessage("unsupported.box.size.eq.eq.0"));
				}
			}
			else if (this.boxLength == 0)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("unsupported.box.size.eq.eq.0"));
			}
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x00142850 File Offset: 0x00141850
		private void ProcessParameters()
		{
			this.type = 33;
			this.originalType = 8;
			this.inp = null;
			try
			{
				if (this.rawData == null)
				{
					WebRequest webRequest = WebRequest.Create(this.url);
					this.inp = webRequest.GetResponse().GetResponseStream();
					this.url.ToString();
				}
				else
				{
					this.inp = new MemoryStream(this.rawData);
				}
				this.boxLength = this.Cio_read(4);
				if (this.boxLength == 12)
				{
					this.boxType = this.Cio_read(4);
					if (1783636000 != this.boxType)
					{
						throw new IOException(MessageLocalization.GetComposedMessage("expected.jp.marker"));
					}
					if (218793738 != this.Cio_read(4))
					{
						throw new IOException(MessageLocalization.GetComposedMessage("error.with.jp.marker"));
					}
					this.Jp2_read_boxhdr();
					if (1718909296 != this.boxType)
					{
						throw new IOException(MessageLocalization.GetComposedMessage("expected.ftyp.marker"));
					}
					Utilities.Skip(this.inp, this.boxLength - 8);
					this.Jp2_read_boxhdr();
					for (;;)
					{
						if (1785737832 != this.boxType)
						{
							if (this.boxType == 1785737827)
							{
								break;
							}
							Utilities.Skip(this.inp, this.boxLength - 8);
							this.Jp2_read_boxhdr();
						}
						if (1785737832 == this.boxType)
						{
							goto Block_10;
						}
					}
					throw new IOException(MessageLocalization.GetComposedMessage("expected.jp2h.marker"));
					Block_10:
					this.Jp2_read_boxhdr();
					if (1768449138 != this.boxType)
					{
						throw new IOException(MessageLocalization.GetComposedMessage("expected.ihdr.marker"));
					}
					this.scaledHeight = (float)this.Cio_read(4);
					this.Top = this.scaledHeight;
					this.scaledWidth = (float)this.Cio_read(4);
					this.Right = this.scaledWidth;
					this.bpc = -1;
				}
				else
				{
					if (this.boxLength != -11534511)
					{
						throw new IOException(MessageLocalization.GetComposedMessage("not.a.valid.jpeg2000.file"));
					}
					Utilities.Skip(this.inp, 4);
					int num = this.Cio_read(4);
					int num2 = this.Cio_read(4);
					int num3 = this.Cio_read(4);
					int num4 = this.Cio_read(4);
					Utilities.Skip(this.inp, 16);
					this.colorspace = this.Cio_read(2);
					this.bpc = 8;
					this.scaledHeight = (float)(num2 - num4);
					this.Top = this.scaledHeight;
					this.scaledWidth = (float)(num - num3);
					this.Right = this.scaledWidth;
				}
			}
			finally
			{
				if (this.inp != null)
				{
					try
					{
						this.inp.Close();
					}
					catch
					{
					}
					this.inp = null;
				}
			}
			this.plainWidth = this.Width;
			this.plainHeight = base.Height;
		}

		// Token: 0x0400230E RID: 8974
		public const int JP2_JP = 1783636000;

		// Token: 0x0400230F RID: 8975
		public const int JP2_IHDR = 1768449138;

		// Token: 0x04002310 RID: 8976
		public const int JPIP_JPIP = 1785751920;

		// Token: 0x04002311 RID: 8977
		public const int JP2_FTYP = 1718909296;

		// Token: 0x04002312 RID: 8978
		public const int JP2_JP2H = 1785737832;

		// Token: 0x04002313 RID: 8979
		public const int JP2_COLR = 1668246642;

		// Token: 0x04002314 RID: 8980
		public const int JP2_JP2C = 1785737827;

		// Token: 0x04002315 RID: 8981
		public const int JP2_URL = 1970433056;

		// Token: 0x04002316 RID: 8982
		public const int JP2_DBTL = 1685348972;

		// Token: 0x04002317 RID: 8983
		public const int JP2_BPCC = 1651532643;

		// Token: 0x04002318 RID: 8984
		public const int JP2_JP2 = 1785737760;

		// Token: 0x04002319 RID: 8985
		private Stream inp;

		// Token: 0x0400231A RID: 8986
		private int boxLength;

		// Token: 0x0400231B RID: 8987
		private int boxType;
	}
}
