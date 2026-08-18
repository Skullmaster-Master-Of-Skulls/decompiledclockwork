using System;

namespace System.Web.Mvc
{
	// Token: 0x0200016A RID: 362
	public class ExceptionContext : ControllerContext
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x0001A8B7 File Offset: 0x00018AB7
		public ExceptionContext()
		{
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0001A8BF File Offset: 0x00018ABF
		public ExceptionContext(ControllerContext controllerContext, Exception exception) : base(controllerContext)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Exception = exception;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001A8DD File Offset: 0x00018ADD
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x0001A8E5 File Offset: 0x00018AE5
		public virtual Exception Exception { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001A8EE File Offset: 0x00018AEE
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x0001A8F6 File Offset: 0x00018AF6
		public bool ExceptionHandled { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0001A8FF File Offset: 0x00018AFF
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x0001A910 File Offset: 0x00018B10
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

		// Token: 0x04000288 RID: 648
		private ActionResult _result;
	}
}
