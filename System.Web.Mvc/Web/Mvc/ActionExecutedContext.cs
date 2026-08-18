using System;

namespace System.Web.Mvc
{
	// Token: 0x020000DF RID: 223
	public class ActionExecutedContext : ControllerContext
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x0000FB56 File Offset: 0x0000DD56
		public ActionExecutedContext()
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000FB5E File Offset: 0x0000DD5E
		public ActionExecutedContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor, bool canceled, Exception exception) : base(controllerContext)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			this.ActionDescriptor = actionDescriptor;
			this.Canceled = canceled;
			this.Exception = exception;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000FB8B File Offset: 0x0000DD8B
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000FB93 File Offset: 0x0000DD93
		public virtual ActionDescriptor ActionDescriptor { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public virtual bool Canceled { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000FBAD File Offset: 0x0000DDAD
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000FBB5 File Offset: 0x0000DDB5
		public virtual Exception Exception { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000FBBE File Offset: 0x0000DDBE
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000FBC6 File Offset: 0x0000DDC6
		public bool ExceptionHandled { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000FBCF File Offset: 0x0000DDCF
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		public ActionResult Result
		{
			get
			{
				return this._result ?? EmptyResult.Instance;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x0400019A RID: 410
		private ActionResult _result;
	}
}
