using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Http.Internal;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000127 RID: 295
	public class ApiControllerActionSelector : IHttpActionSelector
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x00017A7C File Offset: 0x00015C7C
		public virtual HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			ApiControllerActionSelector.ActionSelectorCacheItem internalSelector = this.GetInternalSelector(controllerContext.ControllerDescriptor);
			return internalSelector.SelectAction(controllerContext);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00017AAC File Offset: 0x00015CAC
		public virtual ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor)
		{
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			ApiControllerActionSelector.ActionSelectorCacheItem internalSelector = this.GetInternalSelector(controllerDescriptor);
			return internalSelector.GetActionMapping();
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00017AD8 File Offset: 0x00015CD8
		private ApiControllerActionSelector.ActionSelectorCacheItem GetInternalSelector(HttpControllerDescriptor controllerDescriptor)
		{
			if (this._fastCache == null)
			{
				ApiControllerActionSelector.ActionSelectorCacheItem actionSelectorCacheItem = new ApiControllerActionSelector.ActionSelectorCacheItem(controllerDescriptor);
				Interlocked.CompareExchange<ApiControllerActionSelector.ActionSelectorCacheItem>(ref this._fastCache, actionSelectorCacheItem, null);
				return actionSelectorCacheItem;
			}
			if (this._fastCache.HttpControllerDescriptor == controllerDescriptor)
			{
				return this._fastCache;
			}
			object obj;
			if (controllerDescriptor.Properties.TryGetValue(this._cacheKey, out obj))
			{
				return (ApiControllerActionSelector.ActionSelectorCacheItem)obj;
			}
			ApiControllerActionSelector.ActionSelectorCacheItem actionSelectorCacheItem2 = new ApiControllerActionSelector.ActionSelectorCacheItem(controllerDescriptor);
			controllerDescriptor.Properties.TryAdd(this._cacheKey, actionSelectorCacheItem2);
			return actionSelectorCacheItem2;
		}

		// Token: 0x04000204 RID: 516
		private ApiControllerActionSelector.ActionSelectorCacheItem _fastCache;

		// Token: 0x04000205 RID: 517
		private readonly object _cacheKey = new object();

		// Token: 0x02000128 RID: 296
		private class ActionSelectorCacheItem
		{
			// Token: 0x06000729 RID: 1833 RVA: 0x00017BB8 File Offset: 0x00015DB8
			public ActionSelectorCacheItem(HttpControllerDescriptor controllerDescriptor)
			{
				this._controllerDescriptor = controllerDescriptor;
				MethodInfo[] methods = this._controllerDescriptor.ControllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
				MethodInfo[] array = Array.FindAll<MethodInfo>(methods, new Predicate<MethodInfo>(ApiControllerActionSelector.ActionSelectorCacheItem.IsValidActionMethod));
				this._combinedCandidateActions = new CandidateAction[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					MethodInfo methodInfo = array[i];
					ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = new ReflectedHttpActionDescriptor(this._controllerDescriptor, methodInfo);
					this._combinedCandidateActions[i] = new CandidateAction
					{
						ActionDescriptor = reflectedHttpActionDescriptor
					};
					HttpActionBinding actionBinding = reflectedHttpActionDescriptor.ActionBinding;
					this._actionParameterNames.Add(reflectedHttpActionDescriptor, (from binding in actionBinding.ParameterBindings
					where !binding.Descriptor.IsOptional && TypeHelper.CanConvertFromString(binding.Descriptor.ParameterType) && binding.WillReadUri()
					select binding.Descriptor.Prefix ?? binding.Descriptor.ParameterName).ToArray<string>());
				}
				this._combinedActionNameMapping = (from c in this._combinedCandidateActions
				select c.ActionDescriptor).ToLookup((HttpActionDescriptor actionDesc) => actionDesc.ActionName, StringComparer.OrdinalIgnoreCase);
			}

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x00017D0C File Offset: 0x00015F0C
			public HttpControllerDescriptor HttpControllerDescriptor
			{
				get
				{
					return this._controllerDescriptor;
				}
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x00017D24 File Offset: 0x00015F24
			private void InitializeStandardActions()
			{
				if (this._standardActions != null)
				{
					return;
				}
				ApiControllerActionSelector.StandardActionSelectionCache standardActionSelectionCache = new ApiControllerActionSelector.StandardActionSelectionCache();
				if (this._controllerDescriptor.IsAttributeRouted())
				{
					standardActionSelectionCache.StandardCandidateActions = new CandidateAction[0];
				}
				else
				{
					List<CandidateAction> list = new List<CandidateAction>();
					for (int i = 0; i < this._combinedCandidateActions.Length; i++)
					{
						CandidateAction candidateAction = this._combinedCandidateActions[i];
						ReflectedHttpActionDescriptor reflectedHttpActionDescriptor = (ReflectedHttpActionDescriptor)candidateAction.ActionDescriptor;
						if (reflectedHttpActionDescriptor.MethodInfo.DeclaringType != this._controllerDescriptor.ControllerType || !candidateAction.ActionDescriptor.IsAttributeRouted())
						{
							list.Add(candidateAction);
						}
					}
					standardActionSelectionCache.StandardCandidateActions = list.ToArray();
				}
				standardActionSelectionCache.StandardActionNameMapping = (from c in standardActionSelectionCache.StandardCandidateActions
				select c.ActionDescriptor).ToLookup((HttpActionDescriptor actionDesc) => actionDesc.ActionName, StringComparer.OrdinalIgnoreCase);
				int num = ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds.Length;
				standardActionSelectionCache.CacheListVerbs = new CandidateAction[num][];
				for (int j = 0; j < num; j++)
				{
					standardActionSelectionCache.CacheListVerbs[j] = ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerbWorker(ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds[j], standardActionSelectionCache.StandardCandidateActions);
				}
				this._standardActions = standardActionSelectionCache;
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x00017E68 File Offset: 0x00016068
			public HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
			{
				this.InitializeStandardActions();
				List<ApiControllerActionSelector.CandidateActionWithParams> list = this.FindMatchingActions(controllerContext, false);
				switch (list.Count)
				{
				case 0:
					throw new HttpResponseException(this.CreateSelectionError(controllerContext));
				case 1:
					ApiControllerActionSelector.ActionSelectorCacheItem.ElevateRouteData(controllerContext, list[0]);
					return list[0].ActionDescriptor;
				default:
				{
					string text = ApiControllerActionSelector.ActionSelectorCacheItem.CreateAmbiguousMatchList(list);
					throw Error.InvalidOperation(SRResources.ApiControllerActionSelector_AmbiguousMatch, new object[]
					{
						text
					});
				}
				}
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x00017EDF File Offset: 0x000160DF
			private static void ElevateRouteData(HttpControllerContext controllerContext, ApiControllerActionSelector.CandidateActionWithParams selectedCandidate)
			{
				controllerContext.RouteData = selectedCandidate.RouteDataSource;
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x00017EF0 File Offset: 0x000160F0
			private List<ApiControllerActionSelector.CandidateActionWithParams> FindMatchingActions(HttpControllerContext controllerContext, bool ignoreVerbs = false)
			{
				IHttpRouteData routeData = controllerContext.RouteData;
				IEnumerable<IHttpRouteData> subRoutes = routeData.GetSubRoutes();
				IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound = (subRoutes == null) ? this.GetInitialCandidateWithParameterListForRegularRoutes(controllerContext, ignoreVerbs) : ApiControllerActionSelector.ActionSelectorCacheItem.GetInitialCandidateWithParameterListForDirectRoutes(controllerContext, subRoutes, ignoreVerbs);
				List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound2 = this.FindActionMatchRequiredRouteAndQueryParameters(candidatesFound);
				List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound3 = ApiControllerActionSelector.ActionSelectorCacheItem.RunOrderFilter(candidatesFound2);
				List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound4 = ApiControllerActionSelector.ActionSelectorCacheItem.RunPrecedenceFilter(candidatesFound3);
				return this.FindActionMatchMostRouteAndQueryParameters(candidatesFound4);
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x00017F48 File Offset: 0x00016148
			private HttpResponseMessage CreateSelectionError(HttpControllerContext controllerContext)
			{
				List<ApiControllerActionSelector.CandidateActionWithParams> list = this.FindMatchingActions(controllerContext, true);
				if (list.Count > 0)
				{
					return ApiControllerActionSelector.ActionSelectorCacheItem.Create405Response(controllerContext, list);
				}
				return this.CreateActionNotFoundResponse(controllerContext);
			}

			// Token: 0x06000730 RID: 1840 RVA: 0x00017F78 File Offset: 0x00016178
			private static HttpResponseMessage Create405Response(HttpControllerContext controllerContext, IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> allowedCandidates)
			{
				HttpMethod method = controllerContext.Request.Method;
				HttpResponseMessage httpResponseMessage = controllerContext.Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, Error.Format(SRResources.ApiControllerActionSelector_HttpMethodNotSupported, new object[]
				{
					method
				}));
				HashSet<HttpMethod> hashSet = new HashSet<HttpMethod>();
				foreach (ApiControllerActionSelector.CandidateActionWithParams candidateActionWithParams in allowedCandidates)
				{
					hashSet.UnionWith(candidateActionWithParams.ActionDescriptor.SupportedHttpMethods);
				}
				foreach (HttpMethod httpMethod in hashSet)
				{
					httpResponseMessage.Content.Headers.Allow.Add(httpMethod.ToString());
				}
				return httpResponseMessage;
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x00018060 File Offset: 0x00016260
			private HttpResponseMessage CreateActionNotFoundResponse(HttpControllerContext controllerContext)
			{
				return controllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[]
				{
					controllerContext.Request.RequestUri
				}), Error.Format(SRResources.ApiControllerActionSelector_ActionNotFound, new object[]
				{
					this._controllerDescriptor.ControllerName
				}));
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x000180C0 File Offset: 0x000162C0
			private HttpResponseMessage CreateActionNotFoundResponse(HttpControllerContext controllerContext, string actionName)
			{
				return controllerContext.Request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[]
				{
					controllerContext.Request.RequestUri
				}), Error.Format(SRResources.ApiControllerActionSelector_ActionNameNotFound, new object[]
				{
					this._controllerDescriptor.ControllerName,
					actionName
				}));
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x00018124 File Offset: 0x00016324
			private static List<ApiControllerActionSelector.CandidateActionWithParams> GetInitialCandidateWithParameterListForDirectRoutes(HttpControllerContext controllerContext, IEnumerable<IHttpRouteData> subRoutes, bool ignoreVerbs)
			{
				HttpRequestMessage request = controllerContext.Request;
				HttpMethod method = controllerContext.Request.Method;
				IEnumerable<KeyValuePair<string, string>> queryNameValuePairs = request.GetQueryNameValuePairs();
				List<ApiControllerActionSelector.CandidateActionWithParams> list = new List<ApiControllerActionSelector.CandidateActionWithParams>();
				foreach (IHttpRouteData httpRouteData in subRoutes)
				{
					ISet<string> combinedParameterNames = ApiControllerActionSelector.ActionSelectorCacheItem.GetCombinedParameterNames(queryNameValuePairs, httpRouteData.Values);
					CandidateAction[] directRouteCandidates = httpRouteData.Route.GetDirectRouteCandidates();
					string text;
					httpRouteData.Values.TryGetValue("action", out text);
					foreach (CandidateAction candidateAction in directRouteCandidates)
					{
						if ((text == null || candidateAction.MatchName(text)) && (ignoreVerbs || candidateAction.MatchVerb(method)))
						{
							list.Add(new ApiControllerActionSelector.CandidateActionWithParams(candidateAction, combinedParameterNames, httpRouteData));
						}
					}
				}
				return list;
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x00018210 File Offset: 0x00016410
			private IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> GetInitialCandidateWithParameterListForRegularRoutes(HttpControllerContext controllerContext, bool ignoreVerbs = false)
			{
				CandidateAction[] initialCandidateList = this.GetInitialCandidateList(controllerContext, ignoreVerbs);
				return ApiControllerActionSelector.ActionSelectorCacheItem.GetCandidateActionsWithBindings(controllerContext, initialCandidateList);
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x00018230 File Offset: 0x00016430
			private CandidateAction[] GetInitialCandidateList(HttpControllerContext controllerContext, bool ignoreVerbs = false)
			{
				HttpMethod method = controllerContext.Request.Method;
				IHttpRouteData routeData = controllerContext.RouteData;
				string text;
				CandidateAction[] result;
				if (routeData.Values.TryGetValue("action", out text))
				{
					HttpActionDescriptor[] array = this._standardActions.StandardActionNameMapping[text].ToArray<HttpActionDescriptor>();
					if (array.Length == 0)
					{
						throw new HttpResponseException(this.CreateActionNotFoundResponse(controllerContext, text));
					}
					CandidateAction[] array2 = new CandidateAction[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = new CandidateAction
						{
							ActionDescriptor = array[i]
						};
					}
					if (ignoreVerbs)
					{
						result = array2;
					}
					else
					{
						result = ApiControllerActionSelector.ActionSelectorCacheItem.FilterIncompatibleVerbs(method, array2);
					}
				}
				else if (ignoreVerbs)
				{
					result = this._standardActions.StandardCandidateActions;
				}
				else
				{
					result = ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerb(method, this._standardActions.CacheListVerbs, this._standardActions.StandardCandidateActions);
				}
				return result;
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x0001832C File Offset: 0x0001652C
			private static CandidateAction[] FilterIncompatibleVerbs(HttpMethod incomingMethod, CandidateAction[] candidatesFoundByName)
			{
				return (from candidate in candidatesFoundByName
				where candidate.ActionDescriptor.SupportedHttpMethods.Contains(incomingMethod)
				select candidate).ToArray<CandidateAction>();
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x0001835D File Offset: 0x0001655D
			public ILookup<string, HttpActionDescriptor> GetActionMapping()
			{
				return this._combinedActionNameMapping;
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x00018368 File Offset: 0x00016568
			private static ISet<string> GetCombinedParameterNames(IEnumerable<KeyValuePair<string, string>> queryNameValuePairs, IDictionary<string, object> routeValues)
			{
				HashSet<string> hashSet = new HashSet<string>(routeValues.Keys, StringComparer.OrdinalIgnoreCase);
				hashSet.Remove("controller");
				hashSet.Remove("action");
				HashSet<string> hashSet2 = new HashSet<string>(hashSet, StringComparer.OrdinalIgnoreCase);
				if (queryNameValuePairs != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in queryNameValuePairs)
					{
						hashSet2.Add(keyValuePair.Key);
					}
				}
				return hashSet2;
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x000183F0 File Offset: 0x000165F0
			private List<ApiControllerActionSelector.CandidateActionWithParams> FindActionMatchRequiredRouteAndQueryParameters(IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				List<ApiControllerActionSelector.CandidateActionWithParams> list = new List<ApiControllerActionSelector.CandidateActionWithParams>();
				foreach (ApiControllerActionSelector.CandidateActionWithParams candidateActionWithParams in candidatesFound)
				{
					HttpActionDescriptor actionDescriptor = candidateActionWithParams.ActionDescriptor;
					if (ApiControllerActionSelector.ActionSelectorCacheItem.IsSubset(this._actionParameterNames[actionDescriptor], candidateActionWithParams.CombinedParameterNames))
					{
						list.Add(candidateActionWithParams);
					}
				}
				return list;
			}

			// Token: 0x0600073A RID: 1850 RVA: 0x00018480 File Offset: 0x00016680
			private List<ApiControllerActionSelector.CandidateActionWithParams> FindActionMatchMostRouteAndQueryParameters(List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				if (candidatesFound.Count > 1)
				{
					return (from candidate in candidatesFound
					group candidate by this._actionParameterNames[candidate.ActionDescriptor].Length into g
					orderby g.Key descending
					select g).First<IGrouping<int, ApiControllerActionSelector.CandidateActionWithParams>>().ToList<ApiControllerActionSelector.CandidateActionWithParams>();
				}
				return candidatesFound;
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x000184FC File Offset: 0x000166FC
			private static ApiControllerActionSelector.CandidateActionWithParams[] GetCandidateActionsWithBindings(HttpControllerContext controllerContext, CandidateAction[] candidatesFound)
			{
				HttpRequestMessage request = controllerContext.Request;
				IEnumerable<KeyValuePair<string, string>> queryNameValuePairs = request.GetQueryNameValuePairs();
				IHttpRouteData routeData = controllerContext.RouteData;
				IDictionary<string, object> values = routeData.Values;
				ISet<string> combinedParameterNames = ApiControllerActionSelector.ActionSelectorCacheItem.GetCombinedParameterNames(queryNameValuePairs, values);
				return Array.ConvertAll<CandidateAction, ApiControllerActionSelector.CandidateActionWithParams>(candidatesFound, (CandidateAction candidate) => new ApiControllerActionSelector.CandidateActionWithParams(candidate, combinedParameterNames, routeData));
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x0001855C File Offset: 0x0001675C
			private static bool IsSubset(string[] actionParameters, ISet<string> routeAndQueryParameters)
			{
				foreach (string item in actionParameters)
				{
					if (!routeAndQueryParameters.Contains(item))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x000185B8 File Offset: 0x000167B8
			private static List<ApiControllerActionSelector.CandidateActionWithParams> RunOrderFilter(List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				if (candidatesFound.Count == 0)
				{
					return candidatesFound;
				}
				int minOrder = candidatesFound.Min((ApiControllerActionSelector.CandidateActionWithParams c) => c.CandidateAction.Order);
				return (from c in candidatesFound
				where c.CandidateAction.Order == minOrder
				select c).AsList<ApiControllerActionSelector.CandidateActionWithParams>();
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x00018644 File Offset: 0x00016844
			private static List<ApiControllerActionSelector.CandidateActionWithParams> RunPrecedenceFilter(List<ApiControllerActionSelector.CandidateActionWithParams> candidatesFound)
			{
				if (candidatesFound.Count == 0)
				{
					return candidatesFound;
				}
				decimal highestPrecedence = candidatesFound.Min((ApiControllerActionSelector.CandidateActionWithParams c) => c.CandidateAction.Precedence);
				return (from c in candidatesFound
				where c.CandidateAction.Precedence == highestPrecedence
				select c).AsList<ApiControllerActionSelector.CandidateActionWithParams>();
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x000186A4 File Offset: 0x000168A4
			private static CandidateAction[] FindActionsForVerb(HttpMethod verb, CandidateAction[][] actionsByVerb, CandidateAction[] otherActions)
			{
				for (int i = 0; i < ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds.Length; i++)
				{
					if (object.ReferenceEquals(verb, ApiControllerActionSelector.ActionSelectorCacheItem._cacheListVerbKinds[i]))
					{
						return actionsByVerb[i];
					}
				}
				return ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerbWorker(verb, otherActions);
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x000186E0 File Offset: 0x000168E0
			private static CandidateAction[] FindActionsForVerbWorker(HttpMethod verb, CandidateAction[] candidates)
			{
				List<CandidateAction> list = new List<CandidateAction>();
				ApiControllerActionSelector.ActionSelectorCacheItem.FindActionsForVerbWorker(verb, candidates, list);
				return list.ToArray();
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x00018704 File Offset: 0x00016904
			private static void FindActionsForVerbWorker(HttpMethod verb, CandidateAction[] candidates, List<CandidateAction> listCandidates)
			{
				foreach (CandidateAction candidateAction in candidates)
				{
					if (candidateAction.ActionDescriptor != null && candidateAction.ActionDescriptor.SupportedHttpMethods.Contains(verb))
					{
						listCandidates.Add(candidateAction);
					}
				}
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x00018748 File Offset: 0x00016948
			private static string CreateAmbiguousMatchList(IEnumerable<ApiControllerActionSelector.CandidateActionWithParams> ambiguousCandidates)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (ApiControllerActionSelector.CandidateActionWithParams candidateActionWithParams in ambiguousCandidates)
				{
					HttpActionDescriptor actionDescriptor = candidateActionWithParams.ActionDescriptor;
					string text;
					if (actionDescriptor.ControllerDescriptor != null && actionDescriptor.ControllerDescriptor.ControllerType != null)
					{
						text = actionDescriptor.ControllerDescriptor.ControllerType.FullName;
					}
					else
					{
						text = string.Empty;
					}
					stringBuilder.AppendLine();
					stringBuilder.Append(Error.Format(SRResources.ActionSelector_AmbiguousMatchType, new object[]
					{
						actionDescriptor.ActionName,
						text
					}));
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x00018808 File Offset: 0x00016A08
			private static bool IsValidActionMethod(MethodInfo methodInfo)
			{
				return !methodInfo.IsSpecialName && !methodInfo.GetBaseDefinition().DeclaringType.IsAssignableFrom(TypeHelper.ApiControllerType) && methodInfo.GetCustomAttribute<NonActionAttribute>() == null;
			}

			// Token: 0x04000206 RID: 518
			private readonly HttpControllerDescriptor _controllerDescriptor;

			// Token: 0x04000207 RID: 519
			private readonly CandidateAction[] _combinedCandidateActions;

			// Token: 0x04000208 RID: 520
			private readonly IDictionary<HttpActionDescriptor, string[]> _actionParameterNames = new Dictionary<HttpActionDescriptor, string[]>();

			// Token: 0x04000209 RID: 521
			private readonly ILookup<string, HttpActionDescriptor> _combinedActionNameMapping;

			// Token: 0x0400020A RID: 522
			private static readonly HttpMethod[] _cacheListVerbKinds = new HttpMethod[]
			{
				HttpMethod.Get,
				HttpMethod.Put,
				HttpMethod.Post
			};

			// Token: 0x0400020B RID: 523
			private ApiControllerActionSelector.StandardActionSelectionCache _standardActions;
		}

		// Token: 0x02000129 RID: 297
		[DebuggerDisplay("{DebuggerToString()}")]
		private class CandidateActionWithParams
		{
			// Token: 0x0600074F RID: 1871 RVA: 0x0001886A File Offset: 0x00016A6A
			public CandidateActionWithParams(CandidateAction candidateAction, ISet<string> parameters, IHttpRouteData routeDataSource)
			{
				this.CandidateAction = candidateAction;
				this.CombinedParameterNames = parameters;
				this.RouteDataSource = routeDataSource;
			}

			// Token: 0x17000238 RID: 568
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x00018887 File Offset: 0x00016A87
			// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001888F File Offset: 0x00016A8F
			public CandidateAction CandidateAction { get; private set; }

			// Token: 0x17000239 RID: 569
			// (get) Token: 0x06000752 RID: 1874 RVA: 0x00018898 File Offset: 0x00016A98
			// (set) Token: 0x06000753 RID: 1875 RVA: 0x000188A0 File Offset: 0x00016AA0
			public ISet<string> CombinedParameterNames { get; private set; }

			// Token: 0x1700023A RID: 570
			// (get) Token: 0x06000754 RID: 1876 RVA: 0x000188A9 File Offset: 0x00016AA9
			// (set) Token: 0x06000755 RID: 1877 RVA: 0x000188B1 File Offset: 0x00016AB1
			public IHttpRouteData RouteDataSource { get; private set; }

			// Token: 0x1700023B RID: 571
			// (get) Token: 0x06000756 RID: 1878 RVA: 0x000188BA File Offset: 0x00016ABA
			public HttpActionDescriptor ActionDescriptor
			{
				get
				{
					return this.CandidateAction.ActionDescriptor;
				}
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x000188C8 File Offset: 0x00016AC8
			private string DebuggerToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.CandidateAction.DebuggerToString());
				if (this.CombinedParameterNames.Count > 0)
				{
					stringBuilder.Append(", Params =");
					foreach (string arg in this.CombinedParameterNames)
					{
						stringBuilder.AppendFormat(" {0}", arg);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0200012A RID: 298
		private class StandardActionSelectionCache
		{
			// Token: 0x1700023C RID: 572
			// (get) Token: 0x06000758 RID: 1880 RVA: 0x00018954 File Offset: 0x00016B54
			// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001895C File Offset: 0x00016B5C
			public ILookup<string, HttpActionDescriptor> StandardActionNameMapping { get; set; }

			// Token: 0x1700023D RID: 573
			// (get) Token: 0x0600075A RID: 1882 RVA: 0x00018965 File Offset: 0x00016B65
			// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001896D File Offset: 0x00016B6D
			public CandidateAction[] StandardCandidateActions { get; set; }

			// Token: 0x1700023E RID: 574
			// (get) Token: 0x0600075C RID: 1884 RVA: 0x00018976 File Offset: 0x00016B76
			// (set) Token: 0x0600075D RID: 1885 RVA: 0x0001897E File Offset: 0x00016B7E
			public CandidateAction[][] CacheListVerbs { get; set; }
		}
	}
}
