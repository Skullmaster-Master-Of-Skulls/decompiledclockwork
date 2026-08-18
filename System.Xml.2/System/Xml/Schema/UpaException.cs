using System;

namespace System.Xml.Schema
{
	// Token: 0x020001EA RID: 490
	internal class UpaException : Exception
	{
		// Token: 0x06002083 RID: 8323 RVA: 0x000B2604 File Offset: 0x000B0804
		public UpaException(object particle1, object particle2)
		{
			this.particle1 = particle1;
			this.particle2 = particle2;
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x000B261A File Offset: 0x000B081A
		public object Particle1
		{
			get
			{
				return this.particle1;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x000B2622 File Offset: 0x000B0822
		public object Particle2
		{
			get
			{
				return this.particle2;
			}
		}

		// Token: 0x04000DAF RID: 3503
		private object particle1;

		// Token: 0x04000DB0 RID: 3504
		private object particle2;
	}
}
