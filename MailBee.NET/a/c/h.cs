using System;
using System.Xml;
using iTextSharp.text;

namespace a.c
{
	// Token: 0x02000231 RID: 561
	internal class h : j, w
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x00053C28 File Offset: 0x00052C28
		public h(s A_0) : base(A_0)
		{
			this.b = null;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00053C38 File Offset: 0x00052C38
		private new Chunk b()
		{
			return this.b;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00053C40 File Offset: 0x00052C40
		IElement w.c()
		{
			return this.b;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00053C48 File Offset: 0x00052C48
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
			font.SetStyle(A_1.c());
			if (A_0.FirstChild != null)
			{
				this.b = new Chunk(A_0.FirstChild.Value + "\r\n", font);
				if (A_1.h() != null)
				{
					this.b = (Chunk)A_1.h()(A_0, this.b);
				}
			}
			return A_0.NextSibling;
		}

		// Token: 0x04000F59 RID: 3929
		private new Chunk b;
	}
}
