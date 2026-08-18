using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000117 RID: 279
	public class DefaultHttpControllerSelector : IHttpControllerSelector
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x0001635C File Offset: 0x0001455C
		public DefaultHttpControllerSelector(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._controllerInfoCache = new Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>>(new Func<ConcurrentDictionary<string, HttpControllerDescriptor>>(this.InitializeControllerInfoCache));
			this._configuration = configuration;
			this._controllerTypeCache = new HttpControllerTypeCache(this._configuration);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000163AC File Offset: 0x000145AC
		public virtual HttpControllerDescriptor SelectController(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IHttpRouteData routeData = request.GetRouteData();
			HttpControllerDescriptor directRouteController;
			if (routeData != null)
			{
				directRouteController = DefaultHttpControllerSelector.GetDirectRouteController(routeData);
				if (directRouteController != null)
				{
					return directRouteController;
				}
			}
			string controllerName = this.GetControllerName(request);
			if (string.IsNullOrEmpty(controllerName))
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[]
				{
					request.RequestUri
				}), Error.Format(SRResources.ControllerNameNotFound, new object[]
				{
					request.RequestUri
				})));
			}
			if (this._controllerInfoCache.Value.TryGetValue(controllerName, out directRouteController))
			{
				return directRouteController;
			}
			ICollection<Type> controllerTypes = this._controllerTypeCache.GetControllerTypes(controllerName);
			if (controllerTypes.Count == 0)
			{
				throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[]
				{
					request.RequestUri
				}), Error.Format(SRResources.DefaultControllerFactory_ControllerNameNotFound, new object[]
				{
					controllerName
				})));
			}
			throw DefaultHttpControllerSelector.CreateAmbiguousControllerException(request.GetRouteData().Route, controllerName, controllerTypes);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000164D4 File Offset: 0x000146D4
		public virtual IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
		{
			return this._controllerInfoCache.Value.ToDictionary((KeyValuePair<string, HttpControllerDescriptor> c) => c.Key, (KeyValuePair<string, HttpControllerDescriptor> c) => c.Value, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00016530 File Offset: 0x00014730
		public virtual string GetControllerName(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			IHttpRouteData routeData = request.GetRouteData();
			if (routeData == null)
			{
				return null;
			}
			string result = null;
			routeData.Values.TryGetValue("controller", out result);
			return result;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00016570 File Offset: 0x00014770
		private static HttpControllerDescriptor GetDirectRouteController(IHttpRouteData routeData)
		{
			CandidateAction[] directRouteCandidates = routeData.GetDirectRouteCandidates();
			if (directRouteCandidates != null)
			{
				HttpControllerDescriptor controllerDescriptor = directRouteCandidates[0].ActionDescriptor.ControllerDescriptor;
				for (int i = 1; i < directRouteCandidates.Length; i++)
				{
					CandidateAction candidateAction = directRouteCandidates[i];
					if (candidateAction.ActionDescriptor.ControllerDescriptor != controllerDescriptor)
					{
						throw DefaultHttpControllerSelector.CreateDirectRouteAmbiguousControllerException(directRouteCandidates);
					}
				}
				return controllerDescriptor;
			}
			return null;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000165C0 File Offset: 0x000147C0
		private static Exception CreateDirectRouteAmbiguousControllerException(CandidateAction[] candidates)
		{
			HashSet<Type> hashSet = new HashSet<Type>();
			for (int i = 0; i < candidates.Length; i++)
			{
				hashSet.Add(candidates[i].ActionDescriptor.ControllerDescriptor.ControllerType);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in hashSet)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(type.FullName);
			}
			return Error.InvalidOperation(SRResources.DirectRoute_AmbiguousController, new object[]
			{
				stringBuilder,
				Environment.NewLine
			});
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00016674 File Offset: 0x00014874
		private static Exception CreateAmbiguousControllerException(IHttpRoute route, string controllerName, ICollection<Type> matchingTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Type type in matchingTypes)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(type.FullName);
			}
			string message = Error.Format(SRResources.DefaultControllerFactory_ControllerNameAmbiguous_WithRouteTemplate, new object[]
			{
				controllerName,
				route.RouteTemplate,
				stringBuilder,
				Environment.NewLine
			});
			return new InvalidOperationException(message);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00016708 File Offset: 0x00014908
		private ConcurrentDictionary<string, HttpControllerDescriptor> InitializeControllerInfoCache()
		{
			ConcurrentDictionary<string, HttpControllerDescriptor> concurrentDictionary = new ConcurrentDictionary<string, HttpControllerDescriptor>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, ILookup<string, Type>> cache = this._controllerTypeCache.Cache;
			foreach (KeyValuePair<string, ILookup<string, Type>> keyValuePair in cache)
			{
				string key = keyValuePair.Key;
				foreach (IGrouping<string, Type> grouping in keyValuePair.Value)
				{
					foreach (Type controllerType in grouping)
					{
						if (concurrentDictionary.Keys.Contains(key))
						{
							hashSet.Add(key);
							break;
						}
						concurrentDictionary.TryAdd(key, new HttpControllerDescriptor(this._configuration, key, controllerType));
					}
				}
			}
			foreach (string key2 in hashSet)
			{
				HttpControllerDescriptor httpControllerDescriptor;
				concurrentDictionary.TryRemove(key2, out httpControllerDescriptor);
			}
			return concurrentDictionary;
		}

		// Token: 0x040001DD RID: 477
		private const string ControllerKey = "controller";

		// Token: 0x040001DE RID: 478
		public static readonly string ControllerSuffix = "Controller";

		// Token: 0x040001DF RID: 479
		private readonly HttpConfiguration _configuration;

		// Token: 0x040001E0 RID: 480
		private readonly HttpControllerTypeCache _controllerTypeCache;

		// Token: 0x040001E1 RID: 481
		private readonly Lazy<ConcurrentDictionary<string, HttpControllerDescriptor>> _controllerInfoCache;
	}
}
