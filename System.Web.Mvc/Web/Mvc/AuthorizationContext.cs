using System;

namespace System.Web.Mvc
{
	// Token: 0x02000127 RID: 295
	public class AuthorizationContext : ControllerContext
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x00014EC1 File Offset: 0x000130C1
		public AuthorizationContext()
		{
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00014EC9 File Offset: 0x000130C9
		[Obsolete("The recommended alternative is the constructor AuthorizationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor).")]
		public AuthorizationContext(ControllerContext controllerContext) : base(controllerContext)
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00014ED2 File Offset: 0x000130D2
		public AuthorizationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor) : base(controllerContext)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			this.ActionDescriptor = actionDescriptor;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00014EF0 File Offset: 0x000130F0
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x00014EF8 File Offset: 0x000130F8
		public virtual ActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00014F01 File Offset: 0x00013101
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x00014F09 File Offset: 0x00013109
		public ActionResult Result { get; set; }
	}
}
