using System;

namespace System.ServiceModel
{
	// Token: 0x02000150 RID: 336
	public sealed class NonDualMessageSecurityOverHttp : MessageSecurityOverHttp
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x0002616C File Offset: 0x0002436C
		public NonDualMessageSecurityOverHttp()
		{
			this.establishSecurityContext = true;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0002617B File Offset: 0x0002437B
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00026183 File Offset: 0x00024383
		public bool EstablishSecurityContext
		{
			get
			{
				return this.establishSecurityContext;
			}
			set
			{
				this.establishSecurityContext = value;
			}
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0002618C File Offset: 0x0002438C
		protected override bool IsSecureConversationEnabled()
		{
			return this.establishSecurityContext;
		}

		// Token: 0x04000B88 RID: 2952
		internal const bool DefaultEstablishSecurityContext = true;

		// Token: 0x04000B89 RID: 2953
		private bool establishSecurityContext;
	}
}
