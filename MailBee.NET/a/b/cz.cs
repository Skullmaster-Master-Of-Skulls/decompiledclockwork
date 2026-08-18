using System;

namespace a.b
{
	// Token: 0x0200028B RID: 651
	internal class cz : fu
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x000693F6 File Offset: 0x000683F6
		public cz()
		{
			this.a = null;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00069405 File Offset: 0x00068405
		public cz(em A_0) : this(A_0, "")
		{
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00069413 File Offset: 0x00068413
		public cz(em A_0, string A_1) : base(A_0)
		{
			this.a = A_1;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00069423 File Offset: 0x00068423
		public new string a()
		{
			return this.a;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0006942B File Offset: 0x0006842B
		public new void a(string A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00069434 File Offset: 0x00068434
		public new bool a(object A_0)
		{
			cz cz = (cz)A_0;
			string text = cz.a();
			string text2 = this.a();
			bool flag;
			if (text == null)
			{
				flag = (text2 == null);
			}
			else
			{
				flag = text.Equals(text2);
			}
			return flag && cz.e() == this.e() && cz.d() == this.d() && cz.c().Equals(this.c());
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0006949C File Offset: 0x0006849C
		public override int GetHashCode()
		{
			return (int)this.e();
		}

		// Token: 0x0400113C RID: 4412
		private new string a;
	}
}
