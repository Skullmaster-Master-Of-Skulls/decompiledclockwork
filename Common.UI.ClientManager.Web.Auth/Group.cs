using System;
using System.Collections.Generic;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class Group
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00004967 File Offset: 0x00002B67
		public Group(GroupMembership groupType)
		{
			this._groupType = groupType;
			this._authenticationLookupMethods = new List<AuthenticationLookupMethod>();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004984 File Offset: 0x00002B84
		public GroupMembership GroupType
		{
			get
			{
				return this._groupType;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000499C File Offset: 0x00002B9C
		public List<AuthenticationLookupMethod> AuthenticationLookupMethods
		{
			get
			{
				return this._authenticationLookupMethods;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000049B4 File Offset: 0x00002BB4
		public void AddAuthenticationLookupMethod(AuthenticationLookupMethod method)
		{
			this._authenticationLookupMethods.Add(method);
		}

		// Token: 0x04000019 RID: 25
		private readonly GroupMembership _groupType;

		// Token: 0x0400001A RID: 26
		private readonly List<AuthenticationLookupMethod> _authenticationLookupMethods;
	}
}
