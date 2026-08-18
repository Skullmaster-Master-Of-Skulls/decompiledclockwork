using System;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x0200010E RID: 270
	public sealed class FormValueProvider : NameValueCollectionValueProvider
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x000139A5 File Offset: 0x00011BA5
		public FormValueProvider(ControllerContext controllerContext) : this(controllerContext, new UnvalidatedRequestValuesWrapper(controllerContext.HttpContext.Request.Unvalidated))
		{
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000139C3 File Offset: 0x00011BC3
		internal FormValueProvider(ControllerContext controllerContext, IUnvalidatedRequestValues unvalidatedValues) : base(controllerContext.HttpContext.Request.Form, unvalidatedValues.Form, CultureInfo.CurrentCulture)
		{
		}
	}
}
