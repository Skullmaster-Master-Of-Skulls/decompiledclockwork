using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001CC RID: 460
	public class PartialViewResult : ViewResultBase
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x00023CC4 File Offset: 0x00021EC4
		protected override ViewEngineResult FindView(ControllerContext context)
		{
			ViewEngineResult viewEngineResult = base.ViewEngineCollection.FindPartialView(context, base.ViewName);
			if (viewEngineResult.View != null)
			{
				return viewEngineResult;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in viewEngineResult.SearchedLocations)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(value);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_PartialViewNotFound, new object[]
			{
				base.ViewName,
				stringBuilder
			}));
		}
	}
}
