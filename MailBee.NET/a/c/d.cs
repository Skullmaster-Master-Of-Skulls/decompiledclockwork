using System;
using System.Xml;
using iTextSharp.text;

namespace a.c
{
	// Token: 0x0200022F RID: 559
	internal class d : j, w
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x0005389E File Offset: 0x0005289E
		public d(s A_0) : base(A_0)
		{
			this.b = null;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000538AE File Offset: 0x000528AE
		public new Phrase b()
		{
			return this.b;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000538B6 File Offset: 0x000528B6
		IElement w.c()
		{
			return this.b;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000538C0 File Offset: 0x000528C0
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
			this.b = new Phrase("\r\n\r\n\t\t\t\t");
			if (A_1.h() != null)
			{
				this.b = (Phrase)A_1.h()(A_0, this.b);
			}
			new k(this.b, this.b, true).a(A_0, A_1);
			return A_0.NextSibling;
		}

		// Token: 0x04000F47 RID: 3911
		private new Phrase b;
	}
}
