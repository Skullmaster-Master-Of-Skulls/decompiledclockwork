using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x020000EE RID: 238
	public class DefaultActionValueBinder : IActionValueBinder
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x000138E4 File Offset: 0x00011AE4
		public virtual HttpActionBinding GetBinding(HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			HttpParameterDescriptor[] array = actionDescriptor.GetParameters().ToArray<HttpParameterDescriptor>();
			HttpParameterBinding[] bindings = Array.ConvertAll<HttpParameterDescriptor, HttpParameterBinding>(array, new Converter<HttpParameterDescriptor, HttpParameterBinding>(this.GetParameterBinding));
			HttpActionBinding httpActionBinding = new HttpActionBinding(actionDescriptor, bindings);
			DefaultActionValueBinder.EnsureOneBodyParameter(httpActionBinding);
			return httpActionBinding;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00013930 File Offset: 0x00011B30
		private static void EnsureOneBodyParameter(HttpActionBinding actionBinding)
		{
			IList<HttpParameterDescriptor> parameters = actionBinding.ActionDescriptor.GetParameters();
			int num = -1;
			for (int i = 0; i < actionBinding.ParameterBindings.Length; i++)
			{
				if (actionBinding.ParameterBindings[i].WillReadBody)
				{
					if (num >= 0)
					{
						string parameterName = parameters[num].ParameterName;
						string parameterName2 = parameters[i].ParameterName;
						string message = Error.Format(SRResources.ParameterBindingCantHaveMultipleBodyParameters, new object[]
						{
							parameterName,
							parameterName2
						});
						actionBinding.ParameterBindings[i] = new ErrorParameterBinding(parameters[i], message);
						actionBinding.ParameterBindings[num] = new ErrorParameterBinding(parameters[num], message);
					}
					else
					{
						num = i;
					}
				}
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000139E4 File Offset: 0x00011BE4
		protected virtual HttpParameterBinding GetParameterBinding(HttpParameterDescriptor parameter)
		{
			ParameterBindingAttribute parameterBindingAttribute = parameter.ParameterBinderAttribute;
			if (parameterBindingAttribute != null)
			{
				return parameterBindingAttribute.GetBinding(parameter);
			}
			ParameterBindingRulesCollection parameterBindingRules = parameter.Configuration.ParameterBindingRules;
			if (parameterBindingRules != null)
			{
				HttpParameterBinding httpParameterBinding = parameterBindingRules.LookupBinding(parameter);
				if (httpParameterBinding != null)
				{
					return httpParameterBinding;
				}
			}
			Type parameterType = parameter.ParameterType;
			if (TypeHelper.CanConvertFromString(parameterType))
			{
				return parameter.BindWithAttribute(new FromUriAttribute());
			}
			parameterBindingAttribute = new FromBodyAttribute();
			return parameterBindingAttribute.GetBinding(parameter);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00013AB0 File Offset: 0x00011CB0
		internal static ParameterBindingRulesCollection GetDefaultParameterBinders()
		{
			ParameterBindingRulesCollection parameterBindingRulesCollection = new ParameterBindingRulesCollection();
			parameterBindingRulesCollection.Add(typeof(CancellationToken), (HttpParameterDescriptor parameter) => new CancellationTokenParameterBinding(parameter));
			parameterBindingRulesCollection.Add(typeof(HttpRequestMessage), (HttpParameterDescriptor parameter) => new HttpRequestParameterBinding(parameter));
			parameterBindingRulesCollection.Add(delegate(HttpParameterDescriptor parameter)
			{
				if (!typeof(HttpContent).IsAssignableFrom(parameter.ParameterType))
				{
					return null;
				}
				return parameter.BindAsError(Error.Format(SRResources.ParameterBindingIllegalType, new object[]
				{
					parameter.ParameterType.Name,
					parameter.ParameterName
				}));
			});
			return parameterBindingRulesCollection;
		}
	}
}
