using System;
using System.ComponentModel;
using System.Web.Http.Controllers;

namespace System.Web.Http.Validation
{
	// Token: 0x0200017C RID: 380
	public sealed class ModelValidatingEventArgs : CancelEventArgs
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x0002035D File Offset: 0x0001E55D
		public ModelValidatingEventArgs(HttpActionContext actionContext, ModelValidationNode parentNode)
		{
			if (actionContext == null)
			{
				throw Error.ArgumentNull("actionContext");
			}
			this.ActionContext = actionContext;
			this.ParentNode = parentNode;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00020381 File Offset: 0x0001E581
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00020389 File Offset: 0x0001E589
		public HttpActionContext ActionContext { get; private set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00020392 File Offset: 0x0001E592
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0002039A File Offset: 0x0001E59A
		public ModelValidationNode ParentNode { get; private set; }
	}
}
