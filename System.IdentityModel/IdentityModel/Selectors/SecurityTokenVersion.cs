using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001AB RID: 427
	public abstract class SecurityTokenVersion
	{
		// Token: 0x06000E05 RID: 3589
		public abstract ReadOnlyCollection<string> GetSecuritySpecifications();
	}
}
