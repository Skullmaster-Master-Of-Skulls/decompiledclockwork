using System;

namespace a.b
{
	// Token: 0x0200026B RID: 619
	internal class fk : ii
	{
		// Token: 0x0600163A RID: 5690 RVA: 0x00064728 File Offset: 0x00063728
		public new virtual a4 b()
		{
			int a_ = 4089;
			if (this.x.a(a_))
			{
				hy hy = this.x.b(a_);
				byte[] array = new byte[16];
				Array.Copy(hy.h, array, 16);
				return new a4(array);
			}
			return null;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00064774 File Offset: 0x00063774
		public override string kn()
		{
			int a_ = 12289;
			if (this.x.a(a_))
			{
				return this.d(a_);
			}
			return string.Empty;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x000647A2 File Offset: 0x000637A2
		internal fk(bs A_0, dx A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x000647AC File Offset: 0x000637AC
		public new virtual string a()
		{
			return this.x.ToString();
		}
	}
}
