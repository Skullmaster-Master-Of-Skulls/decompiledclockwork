using System;

namespace System.Web.Mvc.Filters
{
	// Token: 0x02000060 RID: 96
	public class AuthenticationChallengeContext : ControllerContext
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00008A12 File Offset: 0x00006C12
		public AuthenticationChallengeContext()
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00008A1A File Offset: 0x00006C1A
		public AuthenticationChallengeContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor, ActionResult result) : base(controllerContext)
		{
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			this._actionDescriptor = actionDescriptor;
			this._result = result;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00008A55 File Offset: 0x00006C55
		public ActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
			set
			{
				this._actionDescriptor = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00008A5E File Offset: 0x00006C5E
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00008A66 File Offset: 0x00006C66
		public ActionResult Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x04000083 RID: 131
		private ActionDescriptor _actionDescriptor;

		// Token: 0x04000084 RID: 132
		private ActionResult _result;
	}
}
