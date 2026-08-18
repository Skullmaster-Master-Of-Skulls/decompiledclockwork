using System;
using System.Collections;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x0200023E RID: 574
	internal class e : j, w
	{
		// Token: 0x06001336 RID: 4918 RVA: 0x000566E0 File Offset: 0x000556E0
		public e(s A_0) : base(A_0)
		{
			this.c = new ArrayList();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000566F4 File Offset: 0x000556F4
		public void a(r A_0)
		{
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000566F8 File Offset: 0x000556F8
		public XmlNode an(XmlNode A_0, u A_1)
		{
			p p = new p(this.b, A_1, "thead");
			p p2 = new p(this.b, A_1, "tbody");
			p p3 = new p(this.b, A_1);
			p p4 = new p(this.b, A_1, "tfoot");
			u u = new u(A_0, A_1);
			u.b(false);
			u.c(A_1.n());
			XmlNode xmlNode = A_0.Attributes["border"];
			if (xmlNode != null)
			{
				int num = 1;
				try
				{
					num = int.Parse(xmlNode.Value);
				}
				catch (Exception)
				{
				}
				u.b((float)num);
			}
			XmlNode xmlNode2 = A_0.Attributes["bordercolor"];
			if (xmlNode2 != null)
			{
				u.a(j.a(xmlNode2.Value, Color.BLACK));
			}
			else
			{
				u.a(A_1.n());
			}
			XmlNode xmlNode3 = A_0.Attributes["bgcolor"];
			if (xmlNode3 != null)
			{
				u.d(j.a(xmlNode3.Value, Color.BLACK));
			}
			XmlNode xmlNode4 = A_0.Attributes["cellspacing"];
			if (xmlNode4 != null)
			{
				int num2 = 0;
				try
				{
					num2 = int.Parse(xmlNode4.Value);
				}
				catch (Exception)
				{
				}
				u.c((float)num2);
			}
			int num3 = Math.Max(Math.Max(p.d(A_0), p2.d(A_0)), Math.Max(p3.d(A_0), p4.d(A_0)));
			if (num3 == 0)
			{
				return A_0.NextSibling;
			}
			this.b = new PdfPTable(num3);
			((PdfPTable)this.b).SplitLate = false;
			((PdfPTable)this.b).SplitRows = true;
			((PdfPTable)this.b).DefaultCell.HorizontalAlignment = 1;
			XmlNode xmlNode5 = A_0.Attributes["width"];
			if (xmlNode5 != null)
			{
				int num4 = -1;
				try
				{
					num4 = int.Parse(xmlNode5.Value);
				}
				catch (Exception)
				{
				}
				if (num4 > 0)
				{
					((PdfPTable)this.b).TotalWidth = (float)((int)((double)num4 * 0.9));
					((PdfPTable)this.b).LockedWidth = true;
				}
				else
				{
					((PdfPTable)this.b).TotalWidth = A_1.k();
					((PdfPTable)this.b).LockedWidth = true;
				}
			}
			else
			{
				((PdfPTable)this.b).TotalWidth = A_1.k();
				((PdfPTable)this.b).LockedWidth = true;
			}
			bool flag = false;
			string text = p.f(A_0);
			if (text != null)
			{
				if (text.IndexOf('%') == -1)
				{
					p.b(text);
					if (p.c().Length == num3)
					{
						((PdfPTable)this.b).SetWidths(p.c());
						flag = true;
					}
				}
				else
				{
					p.a(text);
					if (p.b().Length == num3)
					{
						((PdfPTable)this.b).SetWidthPercentage(p.b(), PageSize.A4);
						flag = true;
					}
				}
			}
			text = p2.f(A_0);
			if (!flag && text != null)
			{
				text = text.Replace("100%", "?");
				if (text.IndexOf('%') == -1)
				{
					p2.b(text);
					if (p2.c().Length == num3)
					{
						((PdfPTable)this.b).SetWidths(p2.c());
						flag = true;
					}
				}
				else
				{
					p2.a(text);
					if (p2.b().Length == num3)
					{
						((PdfPTable)this.b).SetWidthPercentage(p2.b(), PageSize.A4);
						flag = true;
					}
				}
			}
			text = p3.f(A_0);
			if (!flag && text != null)
			{
				if (text.IndexOf('%') == -1)
				{
					p3.b(text);
					if (p3.c().Length == num3)
					{
						((PdfPTable)this.b).SetWidths(p3.c());
						flag = true;
					}
				}
				else
				{
					p3.a(text);
					if (p3.b().Length == num3)
					{
						((PdfPTable)this.b).SetWidthPercentage(p3.b(), PageSize.A4);
						flag = true;
					}
				}
			}
			text = p4.f(A_0);
			if (!flag && text != null)
			{
				if (text.IndexOf('%') == -1)
				{
					p4.b(text);
					if (p4.c().Length == num3)
					{
						((PdfPTable)this.b).SetWidths(p4.c());
					}
				}
				else
				{
					p4.a(text);
					if (p4.b().Length == num3)
					{
						((PdfPTable)this.b).SetWidthPercentage(p4.b(), PageSize.A4);
					}
				}
			}
			p.b(A_0, (PdfPTable)this.b, u);
			p2.b(A_0, (PdfPTable)this.b, u);
			p3.b(A_0, (PdfPTable)this.b, u);
			p4.b(A_0, (PdfPTable)this.b, u);
			((PdfPTable)this.b).DefaultCell.Border = 0;
			((PdfPTable)this.b).CompleteRow();
			if (A_1.h() != null)
			{
				this.b = (PdfPTable)A_1.h()(A_0, this.b);
			}
			return A_0.NextSibling;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00056C48 File Offset: 0x00055C48
		public IElement ao()
		{
			return this.b;
		}

		// Token: 0x04000F7F RID: 3967
		private new IElement b;

		// Token: 0x04000F80 RID: 3968
		private ArrayList c;
	}
}
