using System;
using System.Xml;
using iTextSharp.text;

namespace a.c
{
	// Token: 0x0200023A RID: 570
	internal class t : j, w
	{
		// Token: 0x0600131B RID: 4891 RVA: 0x000553C5 File Offset: 0x000543C5
		public t(s A_0) : base(A_0)
		{
			this.b = null;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000553D5 File Offset: 0x000543D5
		public new Paragraph b()
		{
			return this.b;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000553DD File Offset: 0x000543DD
		IElement w.c()
		{
			return this.b;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000553E8 File Offset: 0x000543E8
		public XmlNode an(XmlNode A_0, u A_1)
		{
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
			this.b = new Paragraph(string.Empty, font);
			if (A_0.Name.ToLower() == "center")
			{
				this.b.Alignment = 1;
			}
			XmlNode xmlNode = A_0.Attributes["align"];
			if (xmlNode != null)
			{
				string a = xmlNode.Value.ToLower();
				if (!(a == "left"))
				{
					if (!(a == "right"))
					{
						if (a == "center")
						{
							this.b.Alignment = 1;
						}
					}
					else
					{
						this.b.Alignment = 2;
					}
				}
				else
				{
					this.b.Alignment = 0;
				}
			}
			if (A_0.Value == "<p>-space-</p>\r\n\r\n")
			{
				this.b.Font.SetColor(255, 255, 255);
			}
			else
			{
				this.b.Font.SetColor(0, 0, 0);
			}
			if (A_1.h() != null)
			{
				this.b = (Paragraph)A_1.h()(A_0, this.b);
			}
			new k(base.a(), this.b, false).a(A_0, A_1);
			return A_0.NextSibling;
		}

		// Token: 0x04000F77 RID: 3959
		private new Paragraph b;
	}
}
