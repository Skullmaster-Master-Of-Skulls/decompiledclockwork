using System;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x0200023B RID: 571
	internal class y : j, w
	{
		// Token: 0x0600131F RID: 4895 RVA: 0x00055562 File Offset: 0x00054562
		public y(s A_0) : base(A_0)
		{
			this.b = null;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00055572 File Offset: 0x00054572
		private new Phrase b()
		{
			return this.b;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0005557A File Offset: 0x0005457A
		IElement w.c()
		{
			return this.b;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00055584 File Offset: 0x00054584
		public XmlNode an(XmlNode A_0, u A_1)
		{
			u u = new u(A_0, A_1);
			bool a_ = true;
			Font font;
			if (A_1.b() != null)
			{
				font = A_1.b();
			}
			else
			{
				font = base.g(A_0);
			}
			if (A_0.Name.ToLower() == "br")
			{
				a_ = false;
				this.b = new Phrase("\r\n", font);
			}
			else
			{
				this.b = new Phrase(string.Empty, font);
			}
			if (A_0.Name.ToLower() == "hr")
			{
				font.Color = A_1.e();
				font.SetStyle(8);
				Chunk chunk = new Chunk(new string(' ', 175) + "\r\n", font);
				this.b.Add(chunk);
			}
			g g = new g(A_0.Attributes["style"]);
			if (A_0.Name.ToLower() == "b" || A_0.Name.ToLower() == "strong")
			{
				this.b.Font.Size += 2f;
			}
			if (g.a("font-weight") != null)
			{
				string a = g.a("font-weight").ToLower();
				if (!(a == "normal"))
				{
					if (a == "bold")
					{
						this.b.Font.Size += 2f;
					}
				}
				else
				{
					u.a(A_1.c() | 0);
				}
			}
			else if (A_0.Name.ToLower() == "i")
			{
				u.a(A_1.c() | 2);
			}
			else if (A_0.Name.ToLower() == "u")
			{
				u.a(A_1.c() | 4);
			}
			else if (A_0.Name.ToLower() == "strike")
			{
				u.a(A_1.c() | 8);
			}
			if (g.a("font-style") != null)
			{
				string a = g.a("font-style").ToLower();
				if (!(a == "normal"))
				{
					if (a == "italic")
					{
						u.a(A_1.c() | 2);
					}
				}
				else
				{
					u.a(A_1.c() | 0);
				}
			}
			if (g.a("text-decoration") != null)
			{
				string a = g.a("text-decoration").ToLower();
				if (!(a == "underline"))
				{
					if (a == "line-through")
					{
						u.a(A_1.c() | 8);
					}
				}
				else
				{
					u.a(A_1.c() | 4);
				}
			}
			this.b.Font.SetStyle(u.c());
			u.b(A_1.q());
			if (A_0.Name.ToLower() == "font")
			{
				XmlNode xmlNode = A_0.Attributes["name"];
				if (xmlNode != null)
				{
					u.a(j.a(xmlNode.Value));
				}
				XmlNode xmlNode2 = A_0.Attributes["color"];
				if (xmlNode2 != null)
				{
					u.b(j.a(xmlNode2.Value, Color.BLACK));
				}
				XmlNode xmlNode3 = A_0.Attributes["size"];
				int a_2 = A_1.q();
				if (xmlNode3 != null)
				{
					try
					{
						a_2 = 10 + int.Parse(xmlNode3.Value);
					}
					catch (ArgumentException)
					{
					}
					u.b(a_2);
				}
			}
			if (u.b() != null)
			{
				this.b.Font = u.b();
			}
			this.b.Font.Color = u.e();
			this.b.Font.Size = (float)u.q();
			this.b.Font.SetFamily(u.p());
			if (A_0.Name.ToLower() == "pagebreak")
			{
				base.a().n().NewPage();
				if (A_0.Attributes["border"] != null)
				{
					PdfContentByte directContent = base.a().g().DirectContent;
					directContent.Rectangle(100f, 200f, 300f, 100f);
					directContent.Stroke();
				}
			}
			if (A_1.h() != null)
			{
				this.b = (Phrase)A_1.h()(A_0, this.b);
			}
			new k(this.b, this.b, a_).a(A_0, u);
			return A_0.NextSibling;
		}

		// Token: 0x04000F78 RID: 3960
		private new Phrase b;
	}
}
