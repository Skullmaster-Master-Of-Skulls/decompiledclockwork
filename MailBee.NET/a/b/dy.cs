using System;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200034D RID: 845
	internal sealed class dy : iy
	{
		// Token: 0x06001EBE RID: 7870 RVA: 0x00082A6C File Offset: 0x00081A6C
		public dy(h9 A_0) : base(ie.a)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("colorTable");
			}
			this.a = A_0;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00082A8A File Offset: 0x00081A8A
		public void a()
		{
			this.a.a();
			this.b = 0;
			this.c = 0;
			this.d = 0;
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00082AAC File Offset: 0x00081AAC
		protected override void da(f A_0)
		{
			if ("colortbl".Equals(A_0.nu()))
			{
				base.c(A_0);
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00082AC8 File Offset: 0x00081AC8
		protected override void dz(c9 A_0)
		{
			string text = A_0.jz();
			if (text == "red")
			{
				this.b = A_0.j2();
				return;
			}
			if (text == "green")
			{
				this.c = A_0.j2();
				return;
			}
			if (!(text == "blue"))
			{
				return;
			}
			this.d = A_0.j2();
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00082B2C File Offset: 0x00081B2C
		protected override void ft(bp A_0)
		{
			if (";".Equals(A_0.eu()))
			{
				this.a.a(new es(this.b, this.c, this.d));
				this.b = 0;
				this.c = 0;
				this.d = 0;
				return;
			}
			throw new RtfColorTableFormatException(fa.h(A_0.eu()));
		}

		// Token: 0x04001400 RID: 5120
		private readonly h9 a;

		// Token: 0x04001401 RID: 5121
		private int b;

		// Token: 0x04001402 RID: 5122
		private new int c;

		// Token: 0x04001403 RID: 5123
		private int d;
	}
}
