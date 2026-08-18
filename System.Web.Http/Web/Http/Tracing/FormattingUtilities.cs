using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Properties;
using System.Web.Http.Routing;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000173 RID: 371
	internal static class FormattingUtilities
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0001F940 File Offset: 0x0001DB40
		public static string ActionArgumentsToString(IDictionary<string, object> actionArguments)
		{
			return string.Join(", ", from k in actionArguments.Keys
			select k + "=" + FormattingUtilities.ValueToString(actionArguments[k], CultureInfo.CurrentCulture));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		public static string ActionDescriptorToString(HttpActionDescriptor actionDescriptor)
		{
			string str = string.Join(", ", from p in actionDescriptor.GetParameters()
			select p.ParameterType.Name + " " + p.ParameterName);
			return actionDescriptor.ActionName + "(" + str + ")";
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001F9F6 File Offset: 0x0001DBF6
		public static string ActionInvokeToString(HttpActionContext actionContext)
		{
			return FormattingUtilities.ActionInvokeToString(actionContext.ActionDescriptor.ActionName, actionContext.ActionArguments);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001FA0E File Offset: 0x0001DC0E
		public static string ActionInvokeToString(string actionName, IDictionary<string, object> arguments)
		{
			return actionName + "(" + FormattingUtilities.ActionArgumentsToString(arguments) + ")";
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001FA33 File Offset: 0x0001DC33
		public static string FormattersToString(IEnumerable<MediaTypeFormatter> formatters)
		{
			return string.Join(", ", from f in formatters
			select f.GetType().Name);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001FA64 File Offset: 0x0001DC64
		public static string ModelBinderToString(ModelBinderProvider provider)
		{
			CompositeModelBinderProvider compositeModelBinderProvider = provider as CompositeModelBinderProvider;
			if (compositeModelBinderProvider == null)
			{
				return provider.GetType().Name;
			}
			string str = string.Join(", ", compositeModelBinderProvider.Providers.Select(new Func<ModelBinderProvider, string>(FormattingUtilities.ModelBinderToString)));
			return provider.GetType().Name + "(" + str + ")";
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001FAC4 File Offset: 0x0001DCC4
		public static string ModelStateToString(ModelStateDictionary modelState)
		{
			if (modelState.IsValid)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in modelState.Keys)
			{
				ModelState modelState2 = modelState[text];
				if (modelState2.Errors.Count > 0)
				{
					foreach (ModelError modelError in modelState2.Errors)
					{
						string value = Error.Format(SRResources.TraceModelStateErrorMessage, new object[]
						{
							text,
							modelError.ErrorMessage
						});
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(value);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001FBF3 File Offset: 0x0001DDF3
		public static string RouteToString(IHttpRouteData routeData)
		{
			return string.Join(",", from pair in routeData.Values
			select Error.Format("{0}:{1}", new object[]
			{
				pair.Key,
				pair.Value
			}));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001FC28 File Offset: 0x0001DE28
		public static string ValueProviderToString(IValueProvider provider)
		{
			CompositeValueProvider compositeValueProvider = provider as CompositeValueProvider;
			if (compositeValueProvider == null)
			{
				return provider.GetType().Name;
			}
			string str = string.Join(", ", compositeValueProvider.Select(new Func<IValueProvider, string>(FormattingUtilities.ValueProviderToString)));
			return provider.GetType().Name + "(" + str + ")";
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001FC83 File Offset: 0x0001DE83
		public static string ValueToString(object value, CultureInfo cultureInfo)
		{
			if (value == null)
			{
				return FormattingUtilities.NullMessage;
			}
			return Convert.ToString(value, cultureInfo);
		}

		// Token: 0x040002D9 RID: 729
		public static readonly string NullMessage = "null";
	}
}
