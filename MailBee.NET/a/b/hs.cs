using System;

namespace a.b
{
	// Token: 0x0200037C RID: 892
	internal abstract class hs : i1
	{
		// Token: 0x0600207A RID: 8314 RVA: 0x00087227 File Offset: 0x00086227
		protected hs(hu A_0)
		{
			this.a = A_0;
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00087236 File Offset: 0x00086236
		public hu n()
		{
			return this.a;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0008723E File Offset: 0x0008623E
		public void o(gq A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("visitor");
			}
			this.dw(A_0);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x00087255 File Offset: 0x00086255
		public sealed override bool Equals(object obj)
		{
			return obj == this || (obj != null && !(base.GetType() != obj.GetType()) && this.dx(obj));
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0008727C File Offset: 0x0008627C
		public sealed override int GetHashCode()
		{
			return f3.a(base.GetType().GetHashCode(), this.dy());
		}

		// Token: 0x0600207F RID: 8319
		protected abstract void dw(gq A_0);

		// Token: 0x06002080 RID: 8320 RVA: 0x00087294 File Offset: 0x00086294
		protected virtual bool dx(object A_0)
		{
			return true;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00087297 File Offset: 0x00086297
		protected virtual int dy()
		{
			return 251705873;
		}

		// Token: 0x04001488 RID: 5256
		private readonly hu a;
	}
}
