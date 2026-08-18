using System;

namespace a.b
{
	// Token: 0x02000270 RID: 624
	internal class h5 : co
	{
		// Token: 0x0600166A RID: 5738 RVA: 0x000666A6 File Offset: 0x000656A6
		public new virtual string b()
		{
			return this.d(this.u.b(35072, 9));
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000666C0 File Offset: 0x000656C0
		public new virtual string d()
		{
			return this.d(this.u.b(35073, 9));
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x000666DA File Offset: 0x000656DA
		public new virtual int e()
		{
			return this.h(this.u.b(35074, 9));
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000666F4 File Offset: 0x000656F4
		public new virtual string a()
		{
			return this.d(this.u.b(35075, 9));
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0006670E File Offset: 0x0006570E
		public new virtual string f()
		{
			return this.d(this.u.b(35076, 9));
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00066728 File Offset: 0x00065728
		public new virtual string c()
		{
			return this.d(this.u.b(35077, 9));
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00066742 File Offset: 0x00065742
		public new virtual string g()
		{
			return this.d(this.u.b(35078, 9));
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0006675C File Offset: 0x0006575C
		public h5(bs A_0, dx A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x00066766 File Offset: 0x00065766
		public h5(bs A_0, dx A_1, c0 A_2, fb A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00066774 File Offset: 0x00065774
		public override string ToString()
		{
			return string.Format("Channel ASCII or Unicode string values: {0}\nItem link ASCII or Unicode string values: {1}\nItem hash Integer 32-bit signed: {2}\nItem GUID ASCII or Unicode string values: {3}\nChannel GUID ASCII or Unicode string values: {4}\nItem XML ASCII or Unicode string values: {5}\nSubscription ASCII or Unicode string values: {6}", new object[]
			{
				this.b(),
				this.d(),
				this.e(),
				this.a(),
				this.f(),
				this.c(),
				this.g()
			});
		}
	}
}
