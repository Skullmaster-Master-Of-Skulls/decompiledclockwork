using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x020004A9 RID: 1193
	[ComVisible(true)]
	public class TrustManagerContext
	{
		// Token: 0x06002F3A RID: 12090 RVA: 0x0009FF51 File Offset: 0x0009EF51
		public TrustManagerContext() : this(TrustManagerUIContext.Run)
		{
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x0009FF5A File Offset: 0x0009EF5A
		public TrustManagerContext(TrustManagerUIContext uiContext)
		{
			this.m_ignorePersistedDecision = false;
			this.m_uiContext = uiContext;
			this.m_keepAlive = false;
			this.m_persist = true;
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x0009FF7E File Offset: 0x0009EF7E
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x0009FF86 File Offset: 0x0009EF86
		public virtual TrustManagerUIContext UIContext
		{
			get
			{
				return this.m_uiContext;
			}
			set
			{
				this.m_uiContext = value;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x0009FF8F File Offset: 0x0009EF8F
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x0009FF97 File Offset: 0x0009EF97
		public virtual bool NoPrompt
		{
			get
			{
				return this.m_noPrompt;
			}
			set
			{
				this.m_noPrompt = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002F40 RID: 12096 RVA: 0x0009FFA0 File Offset: 0x0009EFA0
		// (set) Token: 0x06002F41 RID: 12097 RVA: 0x0009FFA8 File Offset: 0x0009EFA8
		public virtual bool IgnorePersistedDecision
		{
			get
			{
				return this.m_ignorePersistedDecision;
			}
			set
			{
				this.m_ignorePersistedDecision = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x0009FFB1 File Offset: 0x0009EFB1
		// (set) Token: 0x06002F43 RID: 12099 RVA: 0x0009FFB9 File Offset: 0x0009EFB9
		public virtual bool KeepAlive
		{
			get
			{
				return this.m_keepAlive;
			}
			set
			{
				this.m_keepAlive = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x0009FFC2 File Offset: 0x0009EFC2
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x0009FFCA File Offset: 0x0009EFCA
		public virtual bool Persist
		{
			get
			{
				return this.m_persist;
			}
			set
			{
				this.m_persist = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x0009FFD3 File Offset: 0x0009EFD3
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x0009FFDB File Offset: 0x0009EFDB
		public virtual ApplicationIdentity PreviousApplicationIdentity
		{
			get
			{
				return this.m_appId;
			}
			set
			{
				this.m_appId = value;
			}
		}

		// Token: 0x0400180A RID: 6154
		private bool m_ignorePersistedDecision;

		// Token: 0x0400180B RID: 6155
		private TrustManagerUIContext m_uiContext;

		// Token: 0x0400180C RID: 6156
		private bool m_noPrompt;

		// Token: 0x0400180D RID: 6157
		private bool m_keepAlive;

		// Token: 0x0400180E RID: 6158
		private bool m_persist;

		// Token: 0x0400180F RID: 6159
		private ApplicationIdentity m_appId;
	}
}
