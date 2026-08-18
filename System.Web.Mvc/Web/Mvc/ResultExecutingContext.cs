using System;

namespace System.Web.Mvc
{
	// Token: 0x0200016C RID: 364
	public class ResultExecutingContext : ControllerContext
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x0001A992 File Offset: 0x00018B92
		public ResultExecutingContext()
		{
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001A99A File Offset: 0x00018B9A
		public ResultExecutingContext(ControllerContext controllerContext, ActionResult result) : base(controllerContext)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			this.Result = result;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		public bool Cancel { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0001A9C9 File Offset: 0x00018BC9
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x0001A9D1 File Offset: 0x00018BD1
		public virtual ActionResult Result { get; set; }
	}
}
