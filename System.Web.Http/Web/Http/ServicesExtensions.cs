using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Tracing;
using System.Web.Http.Validation;
using System.Web.Http.ValueProviders;

namespace System.Web.Http
{
	// Token: 0x02000118 RID: 280
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ServicesExtensions
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x00016874 File Offset: 0x00014A74
		public static IEnumerable<ModelBinderProvider> GetModelBinderProviders(this ServicesContainer services)
		{
			return services.GetServices<ModelBinderProvider>();
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001687C File Offset: 0x00014A7C
		public static ModelMetadataProvider GetModelMetadataProvider(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<ModelMetadataProvider>();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00016884 File Offset: 0x00014A84
		public static IEnumerable<ModelValidatorProvider> GetModelValidatorProviders(this ServicesContainer services)
		{
			return services.GetServices<ModelValidatorProvider>();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001688C File Offset: 0x00014A8C
		internal static IModelValidatorCache GetModelValidatorCache(this ServicesContainer services)
		{
			return services.GetService<IModelValidatorCache>();
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00016894 File Offset: 0x00014A94
		public static IContentNegotiator GetContentNegotiator(this ServicesContainer services)
		{
			return services.GetService<IContentNegotiator>();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001689C File Offset: 0x00014A9C
		public static IHttpControllerActivator GetHttpControllerActivator(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpControllerActivator>();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000168A4 File Offset: 0x00014AA4
		public static IHttpActionSelector GetActionSelector(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpActionSelector>();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000168AC File Offset: 0x00014AAC
		public static IHttpActionInvoker GetActionInvoker(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpActionInvoker>();
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000168B4 File Offset: 0x00014AB4
		public static IActionValueBinder GetActionValueBinder(this ServicesContainer services)
		{
			return services.GetService<IActionValueBinder>();
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000168BC File Offset: 0x00014ABC
		public static IEnumerable<ValueProviderFactory> GetValueProviderFactories(this ServicesContainer services)
		{
			return services.GetServices<ValueProviderFactory>();
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000168C4 File Offset: 0x00014AC4
		public static IBodyModelValidator GetBodyModelValidator(this ServicesContainer services)
		{
			return services.GetService<IBodyModelValidator>();
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000168CC File Offset: 0x00014ACC
		public static IHostBufferPolicySelector GetHostBufferPolicySelector(this ServicesContainer services)
		{
			return services.GetService<IHostBufferPolicySelector>();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000168D4 File Offset: 0x00014AD4
		public static IHttpControllerSelector GetHttpControllerSelector(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpControllerSelector>();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000168DC File Offset: 0x00014ADC
		public static IAssembliesResolver GetAssembliesResolver(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IAssembliesResolver>();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000168E4 File Offset: 0x00014AE4
		public static IHttpControllerTypeResolver GetHttpControllerTypeResolver(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IHttpControllerTypeResolver>();
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000168EC File Offset: 0x00014AEC
		public static IApiExplorer GetApiExplorer(this ServicesContainer services)
		{
			return services.GetServiceOrThrow<IApiExplorer>();
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000168F4 File Offset: 0x00014AF4
		public static IDocumentationProvider GetDocumentationProvider(this ServicesContainer services)
		{
			return services.GetService<IDocumentationProvider>();
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000168FC File Offset: 0x00014AFC
		public static IExceptionHandler GetExceptionHandler(this ServicesContainer services)
		{
			return services.GetService<IExceptionHandler>();
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00016904 File Offset: 0x00014B04
		public static IEnumerable<IExceptionLogger> GetExceptionLoggers(this ServicesContainer services)
		{
			return services.GetServices<IExceptionLogger>();
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001690C File Offset: 0x00014B0C
		public static IEnumerable<IFilterProvider> GetFilterProviders(this ServicesContainer services)
		{
			return services.GetServices<IFilterProvider>();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00016914 File Offset: 0x00014B14
		public static ITraceManager GetTraceManager(this ServicesContainer services)
		{
			return services.GetService<ITraceManager>();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001691C File Offset: 0x00014B1C
		public static ITraceWriter GetTraceWriter(this ServicesContainer services)
		{
			return services.GetService<ITraceWriter>();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00016924 File Offset: 0x00014B24
		internal static IEnumerable<TService> GetServices<TService>(this ServicesContainer services)
		{
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			return services.GetServices(typeof(TService)).Cast<TService>();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00016949 File Offset: 0x00014B49
		private static TService GetService<TService>(this ServicesContainer services)
		{
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			return (TService)((object)services.GetService(typeof(TService)));
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00016970 File Offset: 0x00014B70
		private static T GetServiceOrThrow<T>(this ServicesContainer services)
		{
			T service = services.GetService<T>();
			if (service == null)
			{
				throw Error.InvalidOperation(SRResources.DependencyResolverNoService, new object[]
				{
					typeof(T).FullName
				});
			}
			return service;
		}
	}
}
