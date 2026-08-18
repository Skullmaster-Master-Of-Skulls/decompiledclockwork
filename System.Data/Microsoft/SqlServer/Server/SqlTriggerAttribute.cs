using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200029D RID: 669
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlTriggerAttribute : Attribute
	{
		// Token: 0x0600227F RID: 8831 RVA: 0x0028C158 File Offset: 0x0028B558
		public SqlTriggerAttribute()
		{
			this.m_fName = null;
			this.m_fTarget = null;
			this.m_fEvent = null;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x0028C188 File Offset: 0x0028B588
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x0028C1A8 File Offset: 0x0028B5A8
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x0028C1C8 File Offset: 0x0028B5C8
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x0028C1E8 File Offset: 0x0028B5E8
		public string Target
		{
			get
			{
				return this.m_fTarget;
			}
			set
			{
				this.m_fTarget = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0028C208 File Offset: 0x0028B608
		// (set) Token: 0x06002285 RID: 8837 RVA: 0x0028C228 File Offset: 0x0028B628
		public string Event
		{
			get
			{
				return this.m_fEvent;
			}
			set
			{
				this.m_fEvent = value;
			}
		}

		// Token: 0x0400166A RID: 5738
		private string m_fName;

		// Token: 0x0400166B RID: 5739
		private string m_fTarget;

		// Token: 0x0400166C RID: 5740
		private string m_fEvent;
	}
}
