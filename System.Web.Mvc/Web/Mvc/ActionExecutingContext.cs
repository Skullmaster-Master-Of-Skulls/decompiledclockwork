using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020000E0 RID: 224
	public class ActionExecutingContext : ControllerContext
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x0000FBE9 File Offset: 0x0000DDE9
		public ActionExecutingContext()
		{
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000FBF1 File Offset: 0x0000DDF1
		public ActionExecutingContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> actionParameters) : base(controllerContext)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			if (actionParameters == null)
			{
				throw new ArgumentNullException("actionParameters");
			}
			this.ActionDescriptor = actionDescriptor;
			this.ActionParameters = actionParameters;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000FC24 File Offset: 0x0000DE24
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public virtual ActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000FC35 File Offset: 0x0000DE35
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000FC3D File Offset: 0x0000DE3D
		public virtual IDictionary<string, object> ActionParameters { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000FC46 File Offset: 0x0000DE46
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000FC4E File Offset: 0x0000DE4E
		public ActionResult Result { get; set; }
	}
}
