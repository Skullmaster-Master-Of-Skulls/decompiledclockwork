using System;

namespace a.b
{
	// Token: 0x0200026F RID: 623
	internal class hf
	{
		// Token: 0x06001660 RID: 5728 RVA: 0x0006658D File Offset: 0x0006558D
		public virtual string f()
		{
			return this.b(12289);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0006659A File Offset: 0x0006559A
		public virtual int b()
		{
			return this.a(3093);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000665A7 File Offset: 0x000655A7
		public virtual string a()
		{
			return this.b(12290);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000665B4 File Offset: 0x000655B4
		public virtual string d()
		{
			return this.b(12291);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000665C1 File Offset: 0x000655C1
		public virtual int c()
		{
			return this.a(24573);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000665CE File Offset: 0x000655CE
		public virtual int e()
		{
			return this.a(24543);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x000665DC File Offset: 0x000655DC
		public virtual string g()
		{
			string text = this.a();
			if (text != null && text.ToUpper().Equals("SMTP"))
			{
				string text2 = this.d();
				if (text2 != null && text2.Length != 0)
				{
					return text2;
				}
			}
			return this.b(14846);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00066624 File Offset: 0x00065624
		internal hf(ew A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00066633 File Offset: 0x00065633
		private string b(int A_0)
		{
			if (this.d.a(A_0))
			{
				return this.d.b(A_0).c();
			}
			return string.Empty;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0006665C File Offset: 0x0006565C
		private int a(int A_0)
		{
			if (this.d.a(A_0))
			{
				bh bh = this.d.b(A_0);
				if (bh.f == 3)
				{
					return bh.g;
				}
				if (bh.f == 2)
				{
					return (int)((short)bh.g);
				}
			}
			return 0;
		}

		// Token: 0x040010B3 RID: 4275
		public const int a = 1;

		// Token: 0x040010B4 RID: 4276
		public const int b = 2;

		// Token: 0x040010B5 RID: 4277
		public const int c = 3;

		// Token: 0x040010B6 RID: 4278
		private ew d;
	}
}
