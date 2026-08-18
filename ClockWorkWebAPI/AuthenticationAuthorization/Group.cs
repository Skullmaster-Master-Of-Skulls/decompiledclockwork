using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	public class Group
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x00028801 File Offset: 0x00026A01
		public Group(GroupMembership groupType)
		{
			this.groupType = groupType;
			this.authenticationLookupMethods = new List<AuthenticationLookupMethod>();
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00028820 File Offset: 0x00026A20
		public GroupMembership GroupType
		{
			get
			{
				return this.groupType;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00028838 File Offset: 0x00026A38
		public List<AuthenticationLookupMethod> AuthenticationLookupMethods
		{
			get
			{
				return this.authenticationLookupMethods;
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00028850 File Offset: 0x00026A50
		public void AddAuthenticationLookupMethod(AuthenticationLookupMethod method)
		{
			this.authenticationLookupMethods.Add(method);
		}

		// Token: 0x0400032E RID: 814
		private GroupMembership groupType;

		// Token: 0x0400032F RID: 815
		private List<AuthenticationLookupMethod> authenticationLookupMethods;
	}
}
