using System;

namespace a
{
	// Token: 0x020004A5 RID: 1189
	internal sealed class @as : IDisposable
	{
		// Token: 0x06002880 RID: 10368 RVA: 0x000BCE78 File Offset: 0x000BBE78
		public @as(Action A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x000BCE87 File Offset: 0x000BBE87
		public void Dispose()
		{
			this.a();
		}

		// Token: 0x04001BB6 RID: 7094
		private readonly Action a;
	}
}
