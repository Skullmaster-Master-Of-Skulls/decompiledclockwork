using System;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x02000112 RID: 274
	public sealed class QueryStringValueProvider : NameValueCollectionValueProvider
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x00013BB7 File Offset: 0x00011DB7
		public QueryStringValueProvider(ControllerContext controllerContext) : this(controllerContext, new UnvalidatedRequestValuesWrapper(controllerContext.HttpContext.Request.Unvalidated))
		{
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00013BD5 File Offset: 0x00011DD5
		internal QueryStringValueProvider(ControllerContext controllerContext, IUnvalidatedRequestValues unvalidatedValues) : base(controllerContext.HttpContext.Request.QueryString, unvalidatedValues.QueryString, CultureInfo.InvariantCulture)
		{
		}
	}
}
