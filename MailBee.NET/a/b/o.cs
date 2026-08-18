using System;
using System.IO;

namespace a.b
{
	// Token: 0x020002EF RID: 751
	internal abstract class o : af
	{
		// Token: 0x06001A88 RID: 6792 RVA: 0x00074A3D File Offset: 0x00073A3D
		protected o()
		{
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00074A45 File Offset: 0x00073A45
		protected o(y A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00074A54 File Offset: 0x00073A54
		protected void a(Stream A_0, byte[] A_1)
		{
			A_0.Write(A_1, 0, A_1.Length);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00074A61 File Offset: 0x00073A61
		public void a3(Stream A_0)
		{
			this.bc(A_0);
		}

		// Token: 0x06001A8C RID: 6796
		public abstract void bc(Stream A_0);

		// Token: 0x040012E3 RID: 4835
		protected y a;
	}
}
