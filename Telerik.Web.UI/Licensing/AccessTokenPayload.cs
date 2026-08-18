using System;

namespace Telerik.Licensing
{
	// Token: 0x02000431 RID: 1073
	internal class AccessTokenPayload
	{
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x0007E29D File Offset: 0x0007C49D
		public string Grant_Type
		{
			get
			{
				return this._grantType;
			}
		}

		// Token: 0x040009DB RID: 2523
		private readonly string _grantType = "client_credentials";
	}
}
