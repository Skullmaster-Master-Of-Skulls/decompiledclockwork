using System;

namespace a.b
{
	// Token: 0x0200024E RID: 590
	internal class ij : ho
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x0005A94B File Offset: 0x0005994B
		public ba a()
		{
			return this.a;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0005A953 File Offset: 0x00059953
		public void a(ba A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0005A95C File Offset: 0x0005995C
		public override string ToString()
		{
			if (this.a == null)
			{
				return null;
			}
			return "Mail Attachment: " + this.a.ToString();
		}

		// Token: 0x04000FB0 RID: 4016
		internal ba a;
	}
}
