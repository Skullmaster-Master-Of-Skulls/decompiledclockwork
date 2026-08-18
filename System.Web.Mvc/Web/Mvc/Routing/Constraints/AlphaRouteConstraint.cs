using System;

namespace System.Web.Mvc.Routing.Constraints
{
	// Token: 0x02000016 RID: 22
	public class AlphaRouteConstraint : RegexRouteConstraint
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003C3B File Offset: 0x00001E3B
		public AlphaRouteConstraint() : base("^[a-z]*$")
		{
		}
	}
}
