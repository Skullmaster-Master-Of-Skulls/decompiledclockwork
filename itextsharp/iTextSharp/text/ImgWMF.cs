using System;
using System.IO;
using System.Net;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.codec.wmf;

namespace iTextSharp.text
{
	// Token: 0x020004AA RID: 1194
	public class ImgWMF : Image
	{
		// Token: 0x06002866 RID: 10342 RVA: 0x000F5ECF File Offset: 0x000F4ECF
		public ImgWMF(Image image) : base(image)
		{
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x000F5ED8 File Offset: 0x000F4ED8
		public ImgWMF(Uri url) : base(url)
		{
			this.ProcessParameters();
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x000F5EE7 File Offset: 0x000F4EE7
		public ImgWMF(string filename) : this(Utilities.ToURL(filename))
		{
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000F5EF5 File Offset: 0x000F4EF5
		public ImgWMF(byte[] img) : base(null)
		{
			this.rawData = img;
			this.originalData = img;
			this.ProcessParameters();
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000F5F14 File Offset: 0x000F4F14
		private void ProcessParameters()
		{
			this.type = 35;
			this.originalType = 6;
			Stream stream = null;
			try
			{
				string p;
				if (this.rawData == null)
				{
					WebRequest webRequest = WebRequest.Create(this.url);
					stream = webRequest.GetResponse().GetResponseStream();
					p = this.url.ToString();
				}
				else
				{
					stream = new MemoryStream(this.rawData);
					p = "Byte array";
				}
				InputMeta inputMeta = new InputMeta(stream);
				if (inputMeta.ReadInt() != -1698247209)
				{
					throw new BadElementException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.placeable.windows.metafile", p));
				}
				inputMeta.ReadWord();
				int num = inputMeta.ReadShort();
				int num2 = inputMeta.ReadShort();
				int num3 = inputMeta.ReadShort();
				int num4 = inputMeta.ReadShort();
				int num5 = inputMeta.ReadWord();
				this.dpiX = 72;
				this.dpiY = 72;
				this.scaledHeight = (float)(num4 - num2) / (float)num5 * 72f;
				this.Top = this.scaledHeight;
				this.scaledWidth = (float)(num3 - num) / (float)num5 * 72f;
				this.Right = this.scaledWidth;
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
				this.plainWidth = this.Width;
				this.plainHeight = base.Height;
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x000F6050 File Offset: 0x000F5050
		public void ReadWMF(PdfTemplate template)
		{
			base.TemplateData = template;
			template.Width = this.Width;
			template.Height = base.Height;
			Stream stream = null;
			try
			{
				if (this.rawData == null)
				{
					WebRequest webRequest = WebRequest.Create(this.url);
					stream = webRequest.GetResponse().GetResponseStream();
				}
				else
				{
					stream = new MemoryStream(this.rawData);
				}
				MetaDo metaDo = new MetaDo(stream, template);
				metaDo.ReadAll();
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
			}
		}
	}
}
