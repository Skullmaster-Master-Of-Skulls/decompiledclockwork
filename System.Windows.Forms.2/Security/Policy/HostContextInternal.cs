using System;

namespace System.Security.Policy
{
	// Token: 0x02000104 RID: 260
	internal class HostContextInternal
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000DEEC File Offset: 0x0000C0EC
		public HostContextInternal(TrustManagerContext trustManagerContext)
		{
			if (trustManagerContext == null)
			{
				this.persist = true;
				return;
			}
			this.ignorePersistedDecision = trustManagerContext.IgnorePersistedDecision;
			this.noPrompt = trustManagerContext.NoPrompt;
			this.persist = trustManagerContext.Persist;
			this.previousAppId = trustManagerContext.PreviousApplicationIdentity;
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000DF3A File Offset: 0x0000C13A
		public bool IgnorePersistedDecision
		{
			get
			{
				return this.ignorePersistedDecision;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000DF42 File Offset: 0x0000C142
		public bool NoPrompt
		{
			get
			{
				return this.noPrompt;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000DF4A File Offset: 0x0000C14A
		public bool Persist
		{
			get
			{
				return this.persist;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000DF52 File Offset: 0x0000C152
		public ApplicationIdentity PreviousAppId
		{
			get
			{
				return this.previousAppId;
			}
		}

		// Token: 0x04000453 RID: 1107
		private bool ignorePersistedDecision;

		// Token: 0x04000454 RID: 1108
		private bool noPrompt;

		// Token: 0x04000455 RID: 1109
		private bool persist;

		// Token: 0x04000456 RID: 1110
		private ApplicationIdentity previousAppId;
	}
}
