using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001FC RID: 508
	public class RequestClaimCollection : Collection<RequestClaim>
	{
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00047534 File Offset: 0x00045734
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x0004753C File Offset: 0x0004573C
		public string Dialect
		{
			get
			{
				return this._dialect;
			}
			set
			{
				this._dialect = value;
			}
		}

		// Token: 0x04000E7D RID: 3709
		private string _dialect = "http://schemas.xmlsoap.org/ws/2005/05/identity";
	}
}
