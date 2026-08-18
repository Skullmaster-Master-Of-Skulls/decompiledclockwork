using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.Validation
{
	// Token: 0x0200017B RID: 379
	public sealed class ModelValidatedEventArgs : EventArgs
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00020317 File Offset: 0x0001E517
		public ModelValidatedEventArgs(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			this.ActionContext = actionContext;
			this.ParentNode = parentNode;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002033B File Offset: 0x0001E53B
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00020343 File Offset: 0x0001E543
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002034C File Offset: 0x0001E54C
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x00020354 File Offset: 0x0001E554
		public ModelValidationNode ParentNode { get; private set; }
	}
}
