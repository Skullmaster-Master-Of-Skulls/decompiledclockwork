using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	public class AuthenticationLookupMethod
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x00028658 File Offset: 0x00026858
		public AuthenticationLookupMethod(AuthenticationMethod authMethod)
		{
			this.authenticationMethod = authMethod;
			this.lookupMethods = new List<LookupMethod>();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00028674 File Offset: 0x00026874
		public void AddLookupMethod(LookupMethod lookupMethod)
		{
			this.lookupMethods.Add(lookupMethod);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00028684 File Offset: 0x00026884
		public AuthenticationMethod AuthenticationMethod
		{
			get
			{
				return this.authenticationMethod;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0002869C File Offset: 0x0002689C
		public List<LookupMethod> LookupMethods
		{
			get
			{
				return this.lookupMethods;
			}
		}

		// Token: 0x04000329 RID: 809
		private AuthenticationMethod authenticationMethod;

		// Token: 0x0400032A RID: 810
		private List<LookupMethod> lookupMethods;
	}
}
