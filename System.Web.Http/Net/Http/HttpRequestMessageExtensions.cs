using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Hosting;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Results;
using System.Web.Http.Routing;

namespace System.Net.Http
{
	// Token: 0x020000E9 RID: 233
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestMessageExtensions
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x00012674 File Offset: 0x00010874
		public static HttpConfiguration GetConfiguration(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.Configuration;
			}
			return request.LegacyGetConfiguration();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000126A6 File Offset: 0x000108A6
		internal static HttpConfiguration LegacyGetConfiguration(this HttpRequestMessage request)
		{
			return request.GetProperty(HttpPropertyKeys.HttpConfigurationKey);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000126B4 File Offset: 0x000108B4
		public static void SetConfiguration(this HttpRequestMessage request, HttpConfiguration configuration)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				requestContext.Configuration = configuration;
			}
			request.Properties[HttpPropertyKeys.HttpConfigurationKey] = configuration;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00012700 File Offset: 0x00010900
		public static IDependencyScope GetDependencyScope(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IDependencyScope dependencyScope;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.DependencyScope, out dependencyScope))
			{
				IDependencyResolver dependencyResolver = request.GetConfiguration().DependencyResolver;
				dependencyScope = dependencyResolver.BeginScope();
				if (dependencyScope == null)
				{
					throw Error.InvalidOperation(SRResources.DependencyResolver_BeginScopeReturnsNull, new object[]
					{
						dependencyResolver.GetType().Name
					});
				}
				request.Properties[HttpPropertyKeys.DependencyScope] = dependencyScope;
				request.RegisterForDispose(dependencyScope);
			}
			return dependencyScope;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001277F File Offset: 0x0001097F
		public static HttpRequestContext GetRequestContext(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.RequestContextKey);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001279A File Offset: 0x0001099A
		public static void SetRequestContext(this HttpRequestMessage request, HttpRequestContext context)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (context == null)
			{
				throw Error.ArgumentNull("context");
			}
			request.Properties[HttpPropertyKeys.RequestContextKey] = context;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000127C9 File Offset: 0x000109C9
		public static SynchronizationContext GetSynchronizationContext(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.SynchronizationContextKey);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000127E4 File Offset: 0x000109E4
		internal static void SetSynchronizationContext(this HttpRequestMessage request, SynchronizationContext synchronizationContext)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties[HttpPropertyKeys.SynchronizationContextKey] = synchronizationContext;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00012808 File Offset: 0x00010A08
		public static X509Certificate2 GetClientCertificate(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.ClientCertificate;
			}
			return request.LegacyGetClientCertificate();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001283C File Offset: 0x00010A3C
		internal static X509Certificate2 LegacyGetClientCertificate(this HttpRequestMessage request)
		{
			X509Certificate2 x509Certificate = null;
			Func<HttpRequestMessage, X509Certificate2> func;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.ClientCertificateKey, out x509Certificate) && request.Properties.TryGetValue(HttpPropertyKeys.RetrieveClientCertificateDelegateKey, out func))
			{
				x509Certificate = func(request);
				if (x509Certificate != null)
				{
					request.Properties.Add(HttpPropertyKeys.ClientCertificateKey, x509Certificate);
				}
			}
			return x509Certificate;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00012890 File Offset: 0x00010A90
		public static IHttpRouteData GetRouteData(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.RouteData;
			}
			return request.LegacyGetRouteData();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000128C2 File Offset: 0x00010AC2
		internal static IHttpRouteData LegacyGetRouteData(this HttpRequestMessage request)
		{
			return request.GetProperty(HttpPropertyKeys.HttpRouteDataKey);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000128D0 File Offset: 0x00010AD0
		public static void SetRouteData(this HttpRequestMessage request, IHttpRouteData routeData)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				requestContext.RouteData = routeData;
			}
			request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001291B File Offset: 0x00010B1B
		public static HttpActionDescriptor GetActionDescriptor(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.HttpActionDescriptorKey);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00012936 File Offset: 0x00010B36
		internal static void SetActionDescriptor(this HttpRequestMessage request, HttpActionDescriptor actionDescriptor)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties[HttpPropertyKeys.HttpActionDescriptorKey] = actionDescriptor;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00012958 File Offset: 0x00010B58
		private static T GetProperty<T>(this HttpRequestMessage request, string key)
		{
			T result;
			request.Properties.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00012978 File Offset: 0x00010B78
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, InvalidByteRangeException invalidByteRangeException)
		{
			if (invalidByteRangeException == null)
			{
				throw Error.ArgumentNull("invalidByteRangeException");
			}
			HttpResponseMessage httpResponseMessage = request.CreateErrorResponse(HttpStatusCode.RequestedRangeNotSatisfiable, invalidByteRangeException);
			httpResponseMessage.Content.Headers.ContentRange = invalidByteRangeException.ContentRange;
			return httpResponseMessage;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000129B7 File Offset: 0x00010BB7
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message)
		{
			return request.CreateErrorResponse(statusCode, new HttpError(message));
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000129F0 File Offset: 0x00010BF0
		internal static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message, string messageDetail)
		{
			return request.CreateErrorResponse(statusCode, delegate(bool includeErrorDetail)
			{
				if (!includeErrorDetail)
				{
					return new HttpError(message);
				}
				return new HttpError(message, messageDetail);
			});
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00012A54 File Offset: 0x00010C54
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, string message, Exception exception)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => new HttpError(exception, includeErrorDetail)
			{
				Message = message
			});
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00012AAC File Offset: 0x00010CAC
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, Exception exception)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => new HttpError(exception, includeErrorDetail));
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00012B00 File Offset: 0x00010D00
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, ModelStateDictionary modelState)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => new HttpError(modelState, includeErrorDetail));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00012B4C File Offset: 0x00010D4C
		public static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, HttpError error)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.CreateErrorResponse(statusCode, (bool includeErrorDetail) => error);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00012B88 File Offset: 0x00010D88
		private static HttpResponseMessage CreateErrorResponse(this HttpRequestMessage request, HttpStatusCode statusCode, Func<bool, HttpError> errorCreator)
		{
			HttpConfiguration configuration = request.GetConfiguration();
			HttpError value = errorCreator(request.ShouldIncludeErrorDetail());
			if (configuration == null)
			{
				using (HttpConfiguration httpConfiguration = new HttpConfiguration())
				{
					return request.CreateResponse(statusCode, value, httpConfiguration);
				}
			}
			return request.CreateResponse(statusCode, value, configuration);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00012BE4 File Offset: 0x00010DE4
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, T value)
		{
			return request.CreateResponse(HttpStatusCode.OK, value, null);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00012BF3 File Offset: 0x00010DF3
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value)
		{
			return request.CreateResponse(statusCode, value, null);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00012C00 File Offset: 0x00010E00
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, HttpConfiguration configuration)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			configuration = (configuration ?? request.GetConfiguration());
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
			}
			IContentNegotiator contentNegotiator = configuration.Services.GetContentNegotiator();
			if (contentNegotiator == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoContentNegotiator, new object[]
				{
					typeof(IContentNegotiator).FullName
				});
			}
			IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;
			return NegotiatedContentResult<T>.Execute(statusCode, value, contentNegotiator, request, formatters);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00012C81 File Offset: 0x00010E81
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, string mediaType)
		{
			return request.CreateResponse(statusCode, value, new MediaTypeHeaderValue(mediaType));
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00012C94 File Offset: 0x00010E94
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeHeaderValue mediaType)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			HttpConfiguration configuration = request.GetConfiguration();
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
			}
			MediaTypeFormatter mediaTypeFormatter = configuration.Formatters.FindWriter(typeof(T), mediaType);
			if (mediaTypeFormatter == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoMatchingFormatter, new object[]
				{
					mediaType,
					typeof(T).Name
				});
			}
			return request.CreateResponse(statusCode, value, mediaTypeFormatter, mediaType);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00012D25 File Offset: 0x00010F25
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeFormatter formatter)
		{
			return request.CreateResponse(statusCode, value, formatter, null);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00012D34 File Offset: 0x00010F34
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, string mediaType)
		{
			MediaTypeHeaderValue mediaType2 = (mediaType != null) ? new MediaTypeHeaderValue(mediaType) : null;
			return request.CreateResponse(statusCode, value, formatter, mediaType2);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00012D5A File Offset: 0x00010F5A
		public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			return FormattedContentResult<T>.Execute(statusCode, value, formatter, mediaType, request);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00012D84 File Offset: 0x00010F84
		public static void RegisterForDispose(this HttpRequestMessage request, IDisposable resource)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (resource == null)
			{
				return;
			}
			List<IDisposable> registeredResourcesForDispose = HttpRequestMessageExtensions.GetRegisteredResourcesForDispose(request);
			registeredResourcesForDispose.Add(resource);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00012DB4 File Offset: 0x00010FB4
		public static void RegisterForDispose(this HttpRequestMessage request, IEnumerable<IDisposable> resources)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (resources == null)
			{
				throw Error.ArgumentNull("resources");
			}
			List<IDisposable> registeredResourcesForDispose = HttpRequestMessageExtensions.GetRegisteredResourcesForDispose(request);
			foreach (IDisposable disposable in resources)
			{
				if (disposable != null)
				{
					registeredResourcesForDispose.Add(disposable);
				}
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00012E24 File Offset: 0x00011024
		public static void DisposeRequestResources(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			List<IDisposable> list;
			if (request.Properties.TryGetValue(HttpPropertyKeys.DisposableRequestResourcesKey, out list))
			{
				foreach (IDisposable disposable in list)
				{
					try
					{
						disposable.Dispose();
					}
					catch
					{
					}
				}
				list.Clear();
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00012EAC File Offset: 0x000110AC
		public static Guid GetCorrelationId(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			Guid guid;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.RequestCorrelationKey, out guid))
			{
				guid = Trace.CorrelationManager.ActivityId;
				if (guid == Guid.Empty)
				{
					guid = Guid.NewGuid();
				}
				request.Properties.Add(HttpPropertyKeys.RequestCorrelationKey, guid);
			}
			return guid;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00012F10 File Offset: 0x00011110
		public static IEnumerable<KeyValuePair<string, string>> GetQueryNameValuePairs(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			Uri requestUri = request.RequestUri;
			if (requestUri == null || string.IsNullOrEmpty(requestUri.Query))
			{
				return Enumerable.Empty<KeyValuePair<string, string>>();
			}
			IEnumerable<KeyValuePair<string, string>> enumerable;
			request.Properties.TryGetValue(HttpPropertyKeys.RequestQueryNameValuePairsKey, out enumerable);
			string text;
			request.Properties.TryGetValue(HttpPropertyKeys.CachedRequestQueryKey, out text);
			if (enumerable == null || (text != null && !object.ReferenceEquals(text, requestUri.Query ?? string.Empty)))
			{
				FormDataCollection formData = new FormDataCollection(requestUri);
				enumerable = formData.GetJQueryNameValuePairs().ToArray<KeyValuePair<string, string>>();
				request.Properties[HttpPropertyKeys.RequestQueryNameValuePairsKey] = enumerable;
				request.Properties[HttpPropertyKeys.CachedRequestQueryKey] = (requestUri.Query ?? string.Empty);
			}
			return enumerable;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00012FD8 File Offset: 0x000111D8
		public static UrlHelper GetUrlHelper(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.Url;
			}
			return new UrlHelper(request);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001300C File Offset: 0x0001120C
		public static bool IsLocal(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.IsLocal;
			}
			return request.LegacyIsLocal();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00013040 File Offset: 0x00011240
		internal static bool LegacyIsLocal(this HttpRequestMessage request)
		{
			Lazy<bool> property = request.GetProperty(HttpPropertyKeys.IsLocalKey);
			return property != null && property.Value;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00013064 File Offset: 0x00011264
		public static bool IsBatchRequest(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return request.GetProperty(HttpPropertyKeys.IsBatchRequest);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00013080 File Offset: 0x00011280
		public static bool ShouldIncludeErrorDetail(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext != null)
			{
				return requestContext.IncludeErrorDetail;
			}
			return request.LegacyShouldIncludeErrorDetail();
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000130B4 File Offset: 0x000112B4
		internal static bool LegacyShouldIncludeErrorDetail(this HttpRequestMessage request)
		{
			HttpConfiguration configuration = request.GetConfiguration();
			IncludeErrorDetailPolicy includeErrorDetailPolicy = IncludeErrorDetailPolicy.Default;
			if (configuration != null)
			{
				includeErrorDetailPolicy = configuration.IncludeErrorDetailPolicy;
			}
			switch (includeErrorDetailPolicy)
			{
			case IncludeErrorDetailPolicy.Default:
			{
				Lazy<bool> property = request.GetProperty(HttpPropertyKeys.IncludeErrorDetailKey);
				if (property != null)
				{
					return property.Value;
				}
				break;
			}
			case IncludeErrorDetailPolicy.LocalOnly:
				break;
			case IncludeErrorDetailPolicy.Always:
				return true;
			case IncludeErrorDetailPolicy.Never:
				return false;
			default:
				return false;
			}
			return request.IsLocal();
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0001310E File Offset: 0x0001130E
		public static IEnumerable<IDisposable> GetResourcesForDisposal(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return HttpRequestMessageExtensions.GetRegisteredResourcesForDispose(request);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00013124 File Offset: 0x00011324
		private static List<IDisposable> GetRegisteredResourcesForDispose(HttpRequestMessage request)
		{
			List<IDisposable> list;
			if (!request.Properties.TryGetValue(HttpPropertyKeys.DisposableRequestResourcesKey, out list))
			{
				list = new List<IDisposable>();
				request.Properties[HttpPropertyKeys.DisposableRequestResourcesKey] = list;
			}
			return list;
		}
	}
}
