using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000031 RID: 49
	public static class ParameterBindingExtensions
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00006E00 File Offset: 0x00005000
		public static HttpParameterBinding BindAsError(this HttpParameterDescriptor parameter, string message)
		{
			return new ErrorParameterBinding(parameter, message);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006E09 File Offset: 0x00005009
		public static HttpParameterBinding BindWithAttribute(this HttpParameterDescriptor parameter, ParameterBindingAttribute attribute)
		{
			return attribute.GetBinding(parameter);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006E12 File Offset: 0x00005012
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter)
		{
			return parameter.BindWithAttribute(new ModelBinderAttribute());
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006E20 File Offset: 0x00005020
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, IModelBinder binder)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IEnumerable<ValueProviderFactory> valueProviderFactories = new ModelBinderAttribute().GetValueProviderFactories(configuration);
			return parameter.BindWithModelBinding(binder, valueProviderFactories);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006E48 File Offset: 0x00005048
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, params ValueProviderFactory[] valueProviderFactories)
		{
			return parameter.BindWithModelBinding((IEnumerable<ValueProviderFactory>)valueProviderFactories);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006E58 File Offset: 0x00005058
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, IEnumerable<ValueProviderFactory> valueProviderFactories)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IModelBinder modelBinder = new ModelBinderAttribute().GetModelBinder(configuration, parameter.ParameterType);
			return new ModelBinderParameterBinding(parameter, modelBinder, valueProviderFactories);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006E86 File Offset: 0x00005086
		public static HttpParameterBinding BindWithModelBinding(this HttpParameterDescriptor parameter, IModelBinder binder, IEnumerable<ValueProviderFactory> valueProviderFactories)
		{
			return new ModelBinderParameterBinding(parameter, binder, valueProviderFactories);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006E90 File Offset: 0x00005090
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
			IBodyModelValidator bodyModelValidator = configuration.Services.GetBodyModelValidator();
			return new FormatterParameterBinding(parameter, formatters, bodyModelValidator);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006EBF File Offset: 0x000050BF
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter, params MediaTypeFormatter[] formatters)
		{
			return parameter.BindWithFormatter((IEnumerable<MediaTypeFormatter>)formatters);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006ED0 File Offset: 0x000050D0
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter, IEnumerable<MediaTypeFormatter> formatters)
		{
			HttpConfiguration configuration = parameter.Configuration;
			IBodyModelValidator bodyModelValidator = configuration.Services.GetBodyModelValidator();
			return new FormatterParameterBinding(parameter, formatters, bodyModelValidator);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006EF8 File Offset: 0x000050F8
		public static HttpParameterBinding BindWithFormatter(this HttpParameterDescriptor parameter, IEnumerable<MediaTypeFormatter> formatters, IBodyModelValidator bodyModelValidator)
		{
			return new FormatterParameterBinding(parameter, formatters, bodyModelValidator);
		}
	}
}
