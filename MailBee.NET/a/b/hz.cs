using System;
using System.Collections;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002CB RID: 715
	internal class hz : EntryNode, gj, h4
	{
		// Token: 0x060018CB RID: 6347 RVA: 0x0006F3B7 File Offset: 0x0006E3B7
		public hz(gg A_0, DirectoryNode A_1) : base(A_0, A_1)
		{
			this.a = A_0.a();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0006F3CD File Offset: 0x0006E3CD
		public eg a()
		{
			return this.a;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0006F3D5 File Offset: 0x0006E3D5
		public int oy()
		{
			return base.Property.h();
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0006F3E2 File Offset: 0x0006E3E2
		public override bool s()
		{
			return true;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0006F3E5 File Offset: 0x0006E3E5
		protected override bool lf()
		{
			return true;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006F3E8 File Offset: 0x0006E3E8
		public Array ji()
		{
			return new object[0];
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0006F3F0 File Offset: 0x0006E3F0
		public IEnumerator jj()
		{
			return ((IEnumerable)new ArrayList
			{
				base.Property,
				this.a
			}).GetEnumerator();
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0006F416 File Offset: 0x0006E416
		public bool jk()
		{
			return false;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0006F419 File Offset: 0x0006E419
		public string jl()
		{
			return base.Name;
		}

		// Token: 0x04001245 RID: 4677
		private eg a;
	}
}
