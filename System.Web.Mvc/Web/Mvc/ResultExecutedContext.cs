using System;

namespace System.Web.Mvc
{
	// Token: 0x0200016B RID: 363
	public class ResultExecutedContext : ControllerContext
	{
		// Token: 0x06000978 RID: 2424 RVA: 0x0001A919 File Offset: 0x00018B19
		public ResultExecutedContext()
		{
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001A921 File Offset: 0x00018B21
		public ResultExecutedContext(ControllerContext controllerContext, ActionResult result, bool canceled, Exception exception) : base(controllerContext)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			this.Result = result;
			this.Canceled = canceled;
			this.Exception = exception;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0001A94E File Offset: 0x00018B4E
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0001A956 File Offset: 0x00018B56
		public virtual bool Canceled { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001A95F File Offset: 0x00018B5F
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x0001A967 File Offset: 0x00018B67
		public virtual Exception Exception { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001A970 File Offset: 0x00018B70
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x0001A978 File Offset: 0x00018B78
		public bool ExceptionHandled { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0001A981 File Offset: 0x00018B81
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x0001A989 File Offset: 0x00018B89
		public virtual ActionResult Result { get; set; }
	}
}
