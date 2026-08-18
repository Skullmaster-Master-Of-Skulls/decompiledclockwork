using System;
using System.Security.Principal;

namespace System.Web.Mvc.Filters
{
	// Token: 0x02000061 RID: 97
	public class AuthenticationContext : ControllerContext
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00008A6F File Offset: 0x00006C6F
		public AuthenticationContext()
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00008A77 File Offset: 0x00006C77
		public AuthenticationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IPrincipal principal) : base(controllerContext)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			this.ActionDescriptor = actionDescriptor;
			this.Principal = principal;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00008A9C File Offset: 0x00006C9C
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public ActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00008AAD File Offset: 0x00006CAD
		// (set) Token: 0x06000295 RID: 661 RVA: 0x00008AB5 File Offset: 0x00006CB5
		public IPrincipal Principal { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00008ABE File Offset: 0x00006CBE
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00008AC6 File Offset: 0x00006CC6
		public ActionResult Result { get; set; }
	}
}
