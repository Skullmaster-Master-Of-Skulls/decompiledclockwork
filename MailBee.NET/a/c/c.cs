using System;
using System.Xml;
using iTextSharp.text;

namespace a.c
{
	// Token: 0x02000240 RID: 576
	internal class c : j, w
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x0005702A File Offset: 0x0005602A
		public c(s A_0) : base(A_0)
		{
			this.b = null;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0005703A File Offset: 0x0005603A
		private new Paragraph b()
		{
			return this.b;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00057042 File Offset: 0x00056042
		IElement w.c()
		{
			return this.b();
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0005704C File Offset: 0x0005604C
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
			this.b = new Paragraph(A_0.Value, font);
			if (A_1.h() != null)
			{
				this.b = (Paragraph)A_1.h()(A_0, this.b);
			}
			return A_0.NextSibling;
		}

		// Token: 0x04000F83 RID: 3971
		private new Paragraph b;
	}
}
