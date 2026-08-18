using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000202 RID: 514
	public class Status
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x000478FC File Offset: 0x00045AFC
		public Status(string code, string reason)
		{
			if (string.IsNullOrEmpty(code))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("code");
			}
			this._code = code;
			this._reason = reason;
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x0004792A File Offset: 0x00045B2A
		// (set) Token: 0x0600110F RID: 4367 RVA: 0x00047932 File Offset: 0x00045B32
		public string Code
		{
			get
			{
				return this._code;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("code");
				}
				this._code = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00047953 File Offset: 0x00045B53
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0004795B File Offset: 0x00045B5B
		public string Reason
		{
			get
			{
				return this._reason;
			}
			set
			{
				this._reason = value;
			}
		}

		// Token: 0x04000EA0 RID: 3744
		private string _code;

		// Token: 0x04000EA1 RID: 3745
		private string _reason;
	}
}
