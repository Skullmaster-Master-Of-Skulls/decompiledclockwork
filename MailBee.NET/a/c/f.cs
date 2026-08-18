using System;
using System.Xml;
using iTextSharp.text;

namespace a.c
{
	// Token: 0x02000237 RID: 567
	internal class f : j, w
	{
		// Token: 0x06001312 RID: 4882 RVA: 0x00055288 File Offset: 0x00054288
		public f(s A_0) : base(A_0)
		{
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00055291 File Offset: 0x00054291
		public new ListItem b()
		{
			return this.b;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00055299 File Offset: 0x00054299
		IElement w.c()
		{
			return this.b();
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000552A4 File Offset: 0x000542A4
		public XmlNode an(XmlNode A_0, u A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("el");
			}
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
			this.b = new ListItem(string.Empty, font);
			if (A_1.h() != null)
			{
				this.b = (ListItem)A_1.h()(A_0, this.b);
			}
			new k(this.b, this.b, true).a(A_0, A_1);
			if (A_0.ParentNode.ParentNode.Name.ToLower() != "li" && A_0.NextSibling == null)
			{
				this.b().Add(new Paragraph("\r\n"));
			}
			return A_0.NextSibling;
		}

		// Token: 0x04000F76 RID: 3958
		private new ListItem b;
	}
}
