using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x02000233 RID: 563
	internal class v : j, w
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x00054D25 File Offset: 0x00053D25
		public v(s A_0) : base(A_0)
		{
			this.b = null;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00054D40 File Offset: 0x00053D40
		public v(s A_0, string A_1) : base(A_0)
		{
			this.b = null;
			this.d = A_1;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00054D62 File Offset: 0x00053D62
		public new Image b()
		{
			return this.b;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00054D6A File Offset: 0x00053D6A
		public PdfPTable c()
		{
			return this.c;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00054D72 File Offset: 0x00053D72
		IElement w.d()
		{
			if (this.c != null)
			{
				return this.c;
			}
			return this.b;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00054D8C File Offset: 0x00053D8C
		public XmlNode an(XmlNode A_0, u A_1)
		{
			XmlNode xmlNode = A_0.Attributes["src"];
			if (xmlNode == null)
			{
				return A_0.NextSibling;
			}
			string text;
			if (base.a().e().Length == 0)
			{
				text = xmlNode.Value;
			}
			else if (new Regex("\\w+:/+", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(xmlNode.Value).Success)
			{
				text = xmlNode.Value;
			}
			else if (!base.a().p() && !Path.IsPathRooted(xmlNode.Value) && base.a().e() != string.Empty)
			{
				text = Path.Combine(base.a().e(), xmlNode.Value);
			}
			else
			{
				text = base.a().e() + "/" + xmlNode.Value;
			}
			if (A_1.o() != null)
			{
				text = A_1.o()(text);
			}
			try
			{
				if (base.a().p())
				{
					this.b = Image.GetInstance(new Uri(text));
				}
				else
				{
					this.b = Image.GetInstance(text);
				}
			}
			catch (IOException)
			{
				return A_0.NextSibling;
			}
			catch (NotSupportedException)
			{
				return A_0.NextSibling;
			}
			catch (WebException)
			{
				return A_0.NextSibling;
			}
			float num = this.b.Height;
			XmlNode xmlNode2 = A_0.Attributes["height"];
			if (xmlNode2 != null)
			{
				try
				{
					num = float.Parse(xmlNode2.Value);
				}
				catch (Exception)
				{
				}
			}
			float num2 = this.b.Width;
			XmlNode xmlNode3 = A_0.Attributes["width"];
			if (xmlNode3 != null)
			{
				try
				{
					num2 = float.Parse(xmlNode3.Value);
				}
				catch (Exception)
				{
				}
			}
			this.b.ScaleToFit((float)((double)num2 / 1.5), (float)((double)num / 1.5));
			this.b.Border = 5;
			this.b.BackgroundColor = A_1.n();
			if (A_1.h() != null)
			{
				this.b = (Image)A_1.h()(A_0, this.b);
			}
			this.c = new PdfPTable(1);
			this.c.SetWidthPercentage(new float[]
			{
				(float)((double)num2 / 1.5)
			}, PageSize.A4);
			this.c.HorizontalAlignment = 0;
			this.c.DefaultCell.Border = 0;
			if (!string.IsNullOrEmpty(this.d))
			{
				this.b.Annotation = new Annotation(0f, 0f, 0f, 0f, this.d);
			}
			this.c.AddCell(this.b);
			return A_0.NextSibling;
		}

		// Token: 0x04000F71 RID: 3953
		private new Image b;

		// Token: 0x04000F72 RID: 3954
		private PdfPTable c;

		// Token: 0x04000F73 RID: 3955
		private string d = string.Empty;
	}
}
