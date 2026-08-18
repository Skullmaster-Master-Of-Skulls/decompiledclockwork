using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020006AB RID: 1707
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06003DB7 RID: 15799 RVA: 0x000D2E9B File Offset: 0x000D1E9B
		// (set) Token: 0x06003DB8 RID: 15800 RVA: 0x000D2EA3 File Offset: 0x000D1EA3
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x000D2EAC File Offset: 0x000D1EAC
		internal bool HasInfo
		{
			get
			{
				return null != this._principal;
			}
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000D2EBC File Offset: 0x000D1EBC
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x04001F83 RID: 8067
		private IPrincipal _principal;
	}
}
