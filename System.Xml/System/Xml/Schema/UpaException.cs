using System;

namespace System.Xml.Schema
{
	// Token: 0x02000192 RID: 402
	internal class UpaException : Exception
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x0005E974 File Offset: 0x0005D974
		public UpaException(object particle1, object particle2)
		{
			this.particle1 = particle1;
			this.particle2 = particle2;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0005E98A File Offset: 0x0005D98A
		public object Particle1
		{
			get
			{
				return this.particle1;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005E992 File Offset: 0x0005D992
		public object Particle2
		{
			get
			{
				return this.particle2;
			}
		}

		// Token: 0x04000CBA RID: 3258
		private object particle1;

		// Token: 0x04000CBB RID: 3259
		private object particle2;
	}
}
