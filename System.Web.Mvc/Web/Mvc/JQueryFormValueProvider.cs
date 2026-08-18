using System;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x0200003A RID: 58
	public class JQueryFormValueProvider : NameValueCollectionValueProvider
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000055F1 File Offset: 0x000037F1
		public JQueryFormValueProvider(ControllerContext controllerContext) : this(controllerContext, new UnvalidatedRequestValuesWrapper(controllerContext.HttpContext.Request.Unvalidated))
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000560F File Offset: 0x0000380F
		internal JQueryFormValueProvider(ControllerContext controllerContext, IUnvalidatedRequestValues unvalidatedValues) : base(controllerContext.HttpContext.Request.Form, unvalidatedValues.Form, CultureInfo.CurrentCulture, true)
		{
		}
	}
}
