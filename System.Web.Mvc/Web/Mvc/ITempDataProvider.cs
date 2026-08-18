using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020001CD RID: 461
	public interface ITempDataProvider
	{
		// Token: 0x06000D9C RID: 3484
		IDictionary<string, object> LoadTempData(ControllerContext controllerContext);

		// Token: 0x06000D9D RID: 3485
		void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values);
	}
}
