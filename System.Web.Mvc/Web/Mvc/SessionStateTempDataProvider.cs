using System;
using System.Collections.Generic;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001CE RID: 462
	public class SessionStateTempDataProvider : ITempDataProvider
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x00023D74 File Offset: 0x00021F74
		public virtual IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
		{
			HttpSessionStateBase session = controllerContext.HttpContext.Session;
			if (session != null)
			{
				Dictionary<string, object> dictionary = session["__ControllerTempData"] as Dictionary<string, object>;
				if (dictionary != null)
				{
					session.Remove("__ControllerTempData");
					return dictionary;
				}
			}
			return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00023DBC File Offset: 0x00021FBC
		public virtual void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			HttpSessionStateBase session = controllerContext.HttpContext.Session;
			bool flag = values != null && values.Count > 0;
			if (session == null)
			{
				if (flag)
				{
					throw new InvalidOperationException(MvcResources.SessionStateTempDataProvider_SessionStateDisabled);
				}
			}
			else
			{
				if (flag)
				{
					session["__ControllerTempData"] = values;
					return;
				}
				if (session["__ControllerTempData"] != null)
				{
					session.Remove("__ControllerTempData");
				}
			}
		}

		// Token: 0x04000386 RID: 902
		internal const string TempDataSessionStateKey = "__ControllerTempData";
	}
}
