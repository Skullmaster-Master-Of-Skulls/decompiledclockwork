using System;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x02000228 RID: 552
	internal class q : j, w
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x0005222A File Offset: 0x0005122A
		public q(s A_0) : base(A_0)
		{
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00052233 File Offset: 0x00051233
		IElement w.b()
		{
			if (this.c != null)
			{
				return this.c;
			}
			if (this.d != null)
			{
				return this.d;
			}
			return this.b;
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0005225C File Offset: 0x0005125C
		public XmlNode an(XmlNode A_0, u A_1)
		{
			XmlNode firstChild = A_0.FirstChild;
			Font font;
			if (A_1.b() != null)
			{
				font = A_1.b();
			}
			else
			{
				font = base.g(A_0);
			}
			font.Color = A_1.e();
			font.Size = (float)A_1.q();
			font.SetStyle(A_1.c());
			if (firstChild != null)
			{
				while (firstChild.Value == null && firstChild.HasChildNodes)
				{
					firstChild = firstChild.FirstChild;
				}
				XmlNode xmlNode = A_0.Attributes["href"];
				string text;
				if (xmlNode != null)
				{
					string value = xmlNode.Value;
					if (value != null && value.Length > 0 && value[0] != '#')
					{
						text = xmlNode.Value;
					}
					else
					{
						text = "http://www.foo.com";
					}
				}
				else
				{
					text = "http://www.foo.com";
				}
				if (firstChild.Value != null)
				{
					this.b = new Anchor(au.c(firstChild.Value.Trim()), font);
					this.b.Reference = text;
					this.b.Font.Color = Color.BLUE;
					this.b.Font.SetStyle(5);
					if (A_1.h() != null)
					{
						this.b = (Anchor)A_1.h()(A_0, this.b);
					}
					this.c = new Phrase();
					this.c.Add(this.b);
				}
				else
				{
					v v = new v(base.a(), text);
					v.an(firstChild, A_1);
					this.d = v.c();
				}
			}
			return A_0.NextSibling;
		}

		// Token: 0x04000F3B RID: 3899
		private new Anchor b;

		// Token: 0x04000F3C RID: 3900
		private Phrase c;

		// Token: 0x04000F3D RID: 3901
		private PdfPTable d;
	}
}
