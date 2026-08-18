using System;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000089 RID: 137
	public class AlphaRouteConstraint : RegexRouteConstraint
	{
		// Token: 0x0600037B RID: 891 RVA: 0x0000AEB3 File Offset: 0x000090B3
		public AlphaRouteConstraint() : base("^[a-z]*$")
		{
		}
	}
}
