using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Validation;

namespace System.Web.Http
{
	// Token: 0x02000197 RID: 407
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class FromBodyAttribute : ParameterBindingAttribute
	{
		// Token: 0x06000A68 RID: 2664 RVA: 0x00022EF0 File Offset: 0x000210F0
		public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
		{
			if (parameter == null)
			{
				throw Error.ArgumentNull("parameter");
			}
			IEnumerable<MediaTypeFormatter> formatters = parameter.Configuration.Formatters;
			IBodyModelValidator bodyModelValidator = parameter.Configuration.Services.GetBodyModelValidator();
			return parameter.BindWithFormatter(formatters, bodyModelValidator);
		}
	}
}
