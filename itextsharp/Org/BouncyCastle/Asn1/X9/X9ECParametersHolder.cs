using System;

namespace Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200035E RID: 862
	public abstract class X9ECParametersHolder
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x000BA123 File Offset: 0x000B9123
		public X9ECParameters Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = this.CreateParameters();
				}
				return this.parameters;
			}
		}

		// Token: 0x06001EE2 RID: 7906
		protected abstract X9ECParameters CreateParameters();

		// Token: 0x0400155E RID: 5470
		private X9ECParameters parameters;
	}
}
