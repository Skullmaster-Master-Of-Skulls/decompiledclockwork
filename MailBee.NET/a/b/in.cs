using System;

namespace a.b
{
	// Token: 0x02000395 RID: 917
	internal abstract class @in : f8
	{
		// Token: 0x060020FA RID: 8442 RVA: 0x00087E61 File Offset: 0x00086E61
		protected @in(gl A_0)
		{
			this.a = A_0;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00087E70 File Offset: 0x00086E70
		public gl p()
		{
			return this.a;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00087E78 File Offset: 0x00086E78
		public void q(cy A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("visitor");
			}
			this.ev(A_0);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x00087E8F File Offset: 0x00086E8F
		public sealed override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.ew(obj));
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00087EB6 File Offset: 0x00086EB6
		public sealed override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.ex());
		}

		// Token: 0x060020FF RID: 8447
		protected abstract void ev(cy A_0);

		// Token: 0x06002100 RID: 8448 RVA: 0x00087ECE File Offset: 0x00086ECE
		protected virtual bool ew(object A_0)
		{
			return true;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00087ED1 File Offset: 0x00086ED1
		protected virtual int ex()
		{
			return 251705873;
		}

		// Token: 0x040014C5 RID: 5317
		private readonly gl a;
	}
}
