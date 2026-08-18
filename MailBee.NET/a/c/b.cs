using System;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x0200023C RID: 572
	internal class b : j, w
	{
		// Token: 0x06001323 RID: 4899 RVA: 0x00055A28 File Offset: 0x00054A28
		public b(s A_0) : base(A_0)
		{
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00055A38 File Offset: 0x00054A38
		public new int b()
		{
			return this.c;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00055A40 File Offset: 0x00054A40
		public void a(int A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00055A4C File Offset: 0x00054A4C
		public XmlNode an(XmlNode A_0, u A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("node");
			}
			u u = new u(A_0, A_1);
			u.b(true);
			this.b = new PdfPCell();
			if (A_1.j() == 0f && A_1.l() != 0f)
			{
				this.b.BorderColor = A_1.g();
			}
			else
			{
				this.b.BorderColor = A_1.m();
			}
			if ((double)A_1.j() != 0.5 && A_1.j() != 0f)
			{
				this.b.BorderWidth = A_1.j();
			}
			else
			{
				this.b.BorderWidth = A_1.l() / 5f;
			}
			this.b.BackgroundColor = A_1.n();
			this.b.Padding = A_1.d();
			g g = new g(A_0.Attributes["style"]);
			if (!A_1.i())
			{
				XmlNode xmlNode = A_0.Attributes["bgcolor"];
				if (xmlNode != null)
				{
					this.b.BackgroundColor = j.a(xmlNode.Value, Color.BLACK);
				}
				if (g.a("background-color") != null)
				{
					this.b.BackgroundColor = j.a(g.a("background-color"), Color.BLACK);
				}
			}
			XmlNode xmlNode2 = A_0.Attributes["colspan"];
			if (xmlNode2 != null)
			{
				try
				{
					this.b.Colspan = int.Parse(xmlNode2.Value);
				}
				catch (Exception)
				{
				}
			}
			XmlNode xmlNode3 = A_0.Attributes["rowspan"];
			if (xmlNode3 != null)
			{
				try
				{
					this.b.Rowspan = int.Parse(xmlNode3.Value);
				}
				catch (Exception)
				{
				}
			}
			XmlNode xmlNode4 = A_0.Attributes["align"];
			string a = null;
			if (xmlNode4 != null)
			{
				a = xmlNode4.Value.ToLower();
			}
			if (g.a("text-align") != null)
			{
				a = g.a("text-align");
			}
			if (!(a == "left"))
			{
				if (!(a == "right"))
				{
					if (a == "center")
					{
						this.b.HorizontalAlignment = 1;
					}
				}
				else
				{
					this.b.HorizontalAlignment = 2;
				}
			}
			else
			{
				this.b.HorizontalAlignment = 0;
			}
			if (g.a("vertical-align") != null)
			{
				string a2 = g.a("vertical-align");
				if (!(a2 == "top"))
				{
					if (!(a2 == "bottom"))
					{
						if (a2 == "middle")
						{
							this.b.VerticalAlignment = 5;
						}
					}
					else
					{
						this.b.VerticalAlignment = 6;
					}
				}
				else
				{
					this.b.VerticalAlignment = 4;
				}
			}
			if (g.a("border") != null)
			{
				foreach (string text in g.a("border").Split(new char[]
				{
					' ',
					'\t'
				}))
				{
					if (text.EndsWith("px"))
					{
						string[] array2 = text.Split(new char[]
						{
							' '
						});
						int num = 1;
						try
						{
							num = int.Parse(array2[1]);
						}
						catch (Exception)
						{
						}
						this.b.BorderWidthLeft = (float)num;
						this.b.BorderWidthRight = (float)num;
						this.b.BorderWidthTop = (float)num;
						this.b.BorderWidthBottom = (float)num;
					}
					else if (text[0] == '#')
					{
						this.b.BorderColorLeft = j.a(text, Color.WHITE);
						this.b.BorderColorRight = j.a(text, Color.WHITE);
						this.b.BorderColorTop = j.a(text, Color.WHITE);
						this.b.BorderColorBottom = j.a(text, Color.WHITE);
					}
				}
			}
			XmlNode xmlNode5 = A_0.Attributes["width"];
			if (xmlNode5 != null && xmlNode5.Value.Trim() != "100%")
			{
				try
				{
					u.a((float)int.Parse(xmlNode5.Value));
					goto IL_434;
				}
				catch (Exception)
				{
					goto IL_434;
				}
			}
			if (this.c != -1)
			{
				u.a((float)this.c);
			}
			IL_434:
			if (A_1.h() != null)
			{
				this.b = (PdfPCell)A_1.h()(A_0, this.b);
			}
			new k(this.b, this.b, true).a(A_0, u);
			return A_0.NextSibling;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00055F08 File Offset: 0x00054F08
		public IElement ao()
		{
			return this.b;
		}

		// Token: 0x04000F79 RID: 3961
		private new PdfPCell b;

		// Token: 0x04000F7A RID: 3962
		private int c = -1;
	}
}
