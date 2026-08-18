using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000071 RID: 113
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlTriggerAttribute : Attribute
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x000477F4 File Offset: 0x00046BF4
		public SqlTriggerAttribute()
		{
			this.m_fName = null;
			this.m_fTarget = null;
			this.m_fEvent = null;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0004781C File Offset: 0x00046C1C
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x00047830 File Offset: 0x00046C30
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

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00047844 File Offset: 0x00046C44
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00047858 File Offset: 0x00046C58
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0004786C File Offset: 0x00046C6C
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00047880 File Offset: 0x00046C80
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

		// Token: 0x040001EE RID: 494
		private string m_fName;

		// Token: 0x040001EF RID: 495
		private string m_fTarget;

		// Token: 0x040001F0 RID: 496
		private string m_fEvent;
	}
}
