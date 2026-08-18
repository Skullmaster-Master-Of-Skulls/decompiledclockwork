using System;

namespace System.Web.Mvc
{
	// Token: 0x02000064 RID: 100
	public interface IViewEngine
	{
		// Token: 0x060002A2 RID: 674
		ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache);

		// Token: 0x060002A3 RID: 675
		ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache);

		// Token: 0x060002A4 RID: 676
		void ReleaseView(ControllerContext controllerContext, IView view);
	}
}
