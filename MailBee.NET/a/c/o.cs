using System;
using System.Xml;
using iTextSharp.text;

namespace a.c
{
	// Token: 0x02000236 RID: 566
	internal class o : j, w
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x00055158 File Offset: 0x00054158
		public o(s A_0) : base(A_0)
		{
			this.b = null;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00055168 File Offset: 0x00054168
		public new List b()
		{
			return this.b;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00055170 File Offset: 0x00054170
		IElement w.c()
		{
			return this.b();
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00055178 File Offset: 0x00054178
		public XmlNode an(XmlNode A_0, u A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("el");
			}
			if (A_0.Name == "ul")
			{
				this.b = new List(false, 10f);
				this.b.SetListSymbol("•");
			}
			else
			{
				this.b = new List(true, 10f);
			}
			this.b.IndentationLeft = 10f;
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode a_ = (XmlNode)obj;
				f f = new f(base.a());
				f.an(a_, new u(a_, A_1));
				this.b().Add(f.b());
			}
			if (A_1.h() != null)
			{
				this.b = (List)A_1.h()(A_0, this.b);
			}
			return A_0.NextSibling;
		}

		// Token: 0x04000F75 RID: 3957
		private new List b;
	}
}
