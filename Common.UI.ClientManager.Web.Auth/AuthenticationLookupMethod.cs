using System;
using System.Collections.Generic;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class AuthenticationLookupMethod
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000048B2 File Offset: 0x00002AB2
		public AuthenticationLookupMethod(AuthenticationMethod authMethod)
		{
			this._authenticationMethod = authMethod;
			this._lookupMethods = new List<LookupMethod>();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000048CE File Offset: 0x00002ACE
		public void AddLookupMethod(LookupMethod lookupMethod)
		{
			this._lookupMethods.Add(lookupMethod);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000048DE File Offset: 0x00002ADE
		public AuthenticationMethod AuthenticationMethod
		{
			get
			{
				return this._authenticationMethod;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000048E6 File Offset: 0x00002AE6
		public List<LookupMethod> LookupMethods
		{
			get
			{
				return this._lookupMethods;
			}
		}

		// Token: 0x04000015 RID: 21
		private readonly AuthenticationMethod _authenticationMethod;

		// Token: 0x04000016 RID: 22
		private readonly List<LookupMethod> _lookupMethods;
	}
}
