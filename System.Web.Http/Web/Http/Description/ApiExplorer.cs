using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Internal;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.Routing;
using System.Web.Http.Services;

namespace System.Web.Http.Description
{
	// Token: 0x020000BC RID: 188
	public class ApiExplorer : IApiExplorer
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0000CF7C File Offset: 0x0000B17C
		public ApiExplorer(HttpConfiguration configuration)
		{
			this._config = configuration;
			this._apiDescriptions = new Lazy<Collection<ApiDescription>>(new Func<Collection<ApiDescription>>(this.InitializeApiDescriptions));
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000CFA2 File Offset: 0x0000B1A2
		public Collection<ApiDescription> ApiDescriptions
		{
			get
			{
				return this._apiDescriptions.Value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000CFAF File Offset: 0x0000B1AF
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000CFB7 File Offset: 0x0000B1B7
		public IDocumentationProvider DocumentationProvider { get; set; }

		// Token: 0x0600043D RID: 1085 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		public virtual bool ShouldExploreController(string controllerVariableValue, HttpControllerDescriptor controllerDescriptor, IHttpRoute route)
		{
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			ApiExplorerSettingsAttribute apiExplorerSettingsAttribute = controllerDescriptor.GetCustomAttributes<ApiExplorerSettingsAttribute>().FirstOrDefault<ApiExplorerSettingsAttribute>();
			return (apiExplorerSettingsAttribute == null || !apiExplorerSettingsAttribute.IgnoreApi) && ApiExplorer.MatchRegexConstraint(route, "controller", controllerVariableValue);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000D010 File Offset: 0x0000B210
		public virtual bool ShouldExploreAction(string actionVariableValue, HttpActionDescriptor actionDescriptor, IHttpRoute route)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			ApiExplorerSettingsAttribute apiExplorerSettingsAttribute = actionDescriptor.GetCustomAttributes<ApiExplorerSettingsAttribute>().FirstOrDefault<ApiExplorerSettingsAttribute>();
			return (apiExplorerSettingsAttribute == null || !apiExplorerSettingsAttribute.IgnoreApi) && ApiExplorer.MatchRegexConstraint(route, "action", actionVariableValue);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000D078 File Offset: 0x0000B278
		public virtual Collection<HttpMethod> GetHttpMethodsSupportedByAction(IHttpRoute route, HttpActionDescriptor actionDescriptor)
		{
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			IList<HttpMethod> list = new List<HttpMethod>();
			IList<HttpMethod> supportedHttpMethods = actionDescriptor.SupportedHttpMethods;
			HttpMethodConstraint httpMethodConstraint = route.Constraints.Values.FirstOrDefault((object c) => typeof(HttpMethodConstraint).IsAssignableFrom(c.GetType())) as HttpMethodConstraint;
			if (httpMethodConstraint == null)
			{
				list = supportedHttpMethods;
			}
			else
			{
				list = httpMethodConstraint.AllowedMethods.Intersect(supportedHttpMethods).ToList<HttpMethod>();
			}
			return new Collection<HttpMethod>(list);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000D380 File Offset: 0x0000B580
		private IEnumerable<IHttpRoute> FlattenRoutes(IEnumerable<IHttpRoute> routes)
		{
			foreach (IHttpRoute route in routes)
			{
				IEnumerable<IHttpRoute> nested = route as IEnumerable<IHttpRoute>;
				if (nested != null)
				{
					foreach (IHttpRoute subRoute in this.FlattenRoutes(nested))
					{
						yield return subRoute;
					}
				}
				else
				{
					yield return route;
				}
			}
			yield break;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		private static HttpControllerDescriptor GetDirectRouteController(CandidateAction[] directRouteCandidates)
		{
			if (directRouteCandidates != null)
			{
				HttpControllerDescriptor controllerDescriptor = directRouteCandidates[0].ActionDescriptor.ControllerDescriptor;
				for (int i = 1; i < directRouteCandidates.Length; i++)
				{
					if (directRouteCandidates[i].ActionDescriptor.ControllerDescriptor != controllerDescriptor)
					{
						return null;
					}
				}
				return controllerDescriptor;
			}
			return null;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000D3E8 File Offset: 0x0000B5E8
		private Collection<ApiDescription> InitializeApiDescriptions()
		{
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			IHttpControllerSelector httpControllerSelector = this._config.Services.GetHttpControllerSelector();
			IDictionary<string, HttpControllerDescriptor> controllerMapping = httpControllerSelector.GetControllerMapping();
			if (controllerMapping != null)
			{
				ApiExplorer.ApiDescriptionComparer comparer = new ApiExplorer.ApiDescriptionComparer();
				foreach (IHttpRoute route in this.FlattenRoutes(this._config.Routes))
				{
					CandidateAction[] directRouteCandidates = route.GetDirectRouteCandidates();
					HttpControllerDescriptor directRouteController = ApiExplorer.GetDirectRouteController(directRouteCandidates);
					Collection<ApiDescription> collection2 = (directRouteController != null && directRouteCandidates != null) ? this.ExploreDirectRoute(directRouteController, directRouteCandidates, route) : this.ExploreRouteControllers(controllerMapping, route);
					collection2 = ApiExplorer.RemoveInvalidApiDescriptions(collection2);
					foreach (ApiDescription apiDescription in collection2)
					{
						if (!collection.Contains(apiDescription, comparer))
						{
							collection.Add(apiDescription);
						}
					}
				}
			}
			return collection;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		private Collection<ApiDescription> ExploreDirectRoute(HttpControllerDescriptor controllerDescriptor, CandidateAction[] candidates, IHttpRoute route)
		{
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			if (this.ShouldExploreController(controllerDescriptor.ControllerName, controllerDescriptor, route))
			{
				foreach (CandidateAction candidateAction in candidates)
				{
					HttpActionDescriptor actionDescriptor = candidateAction.ActionDescriptor;
					string actionName = actionDescriptor.ActionName;
					if (this.ShouldExploreAction(actionName, actionDescriptor, route))
					{
						string text = route.RouteTemplate;
						if (ApiExplorer._actionVariableRegex.IsMatch(text))
						{
							text = ApiExplorer._actionVariableRegex.Replace(text, actionName);
						}
						this.PopulateActionDescriptions(actionDescriptor, route, text, collection);
					}
				}
			}
			return collection;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000D584 File Offset: 0x0000B784
		private Collection<ApiDescription> ExploreRouteControllers(IDictionary<string, HttpControllerDescriptor> controllerMappings, IHttpRoute route)
		{
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			string routeTemplate = route.RouteTemplate;
			string key;
			if (ApiExplorer._controllerVariableRegex.IsMatch(routeTemplate))
			{
				using (IEnumerator<KeyValuePair<string, HttpControllerDescriptor>> enumerator = controllerMappings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, HttpControllerDescriptor> keyValuePair = enumerator.Current;
						key = keyValuePair.Key;
						HttpControllerDescriptor value = keyValuePair.Value;
						if (this.ShouldExploreController(key, value, route))
						{
							string localPath = ApiExplorer._controllerVariableRegex.Replace(routeTemplate, key);
							this.ExploreRouteActions(route, localPath, value, collection);
						}
					}
					return collection;
				}
			}
			HttpControllerDescriptor controllerDescriptor;
			if (route.Defaults.TryGetValue("controller", out key) && controllerMappings.TryGetValue(key, out controllerDescriptor) && this.ShouldExploreController(key, controllerDescriptor, route))
			{
				this.ExploreRouteActions(route, routeTemplate, controllerDescriptor, collection);
			}
			return collection;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000D654 File Offset: 0x0000B854
		private void ExploreRouteActions(IHttpRoute route, string localPath, HttpControllerDescriptor controllerDescriptor, Collection<ApiDescription> apiDescriptions)
		{
			if (!controllerDescriptor.IsAttributeRouted())
			{
				ServicesContainer services = controllerDescriptor.Configuration.Services;
				ILookup<string, HttpActionDescriptor> actionMapping = services.GetActionSelector().GetActionMapping(controllerDescriptor);
				if (actionMapping != null)
				{
					string key;
					if (ApiExplorer._actionVariableRegex.IsMatch(localPath))
					{
						using (IEnumerator<IGrouping<string, HttpActionDescriptor>> enumerator = actionMapping.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IGrouping<string, HttpActionDescriptor> grouping = enumerator.Current;
								key = grouping.Key;
								string localPath2 = ApiExplorer._actionVariableRegex.Replace(localPath, key);
								this.PopulateActionDescriptions(grouping, key, route, localPath2, apiDescriptions);
							}
							return;
						}
					}
					if (route.Defaults.TryGetValue("action", out key))
					{
						this.PopulateActionDescriptions(actionMapping[key], key, route, localPath, apiDescriptions);
						return;
					}
					foreach (IGrouping<string, HttpActionDescriptor> actionDescriptors in actionMapping)
					{
						this.PopulateActionDescriptions(actionDescriptors, null, route, localPath, apiDescriptions);
					}
				}
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000D760 File Offset: 0x0000B960
		private void PopulateActionDescriptions(IEnumerable<HttpActionDescriptor> actionDescriptors, string actionVariableValue, IHttpRoute route, string localPath, Collection<ApiDescription> apiDescriptions)
		{
			foreach (HttpActionDescriptor actionDescriptor in actionDescriptors)
			{
				if (this.ShouldExploreAction(actionVariableValue, actionDescriptor, route) && !actionDescriptor.IsAttributeRouted())
				{
					this.PopulateActionDescriptions(actionDescriptor, route, localPath, apiDescriptions);
				}
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		private void PopulateActionDescriptions(HttpActionDescriptor actionDescriptor, IHttpRoute route, string localPath, Collection<ApiDescription> apiDescriptions)
		{
			string apiDocumentation = this.GetApiDocumentation(actionDescriptor);
			HttpParsedRoute parsedRoute = RouteParser.Parse(localPath);
			IList<ApiParameterDescription> list = this.CreateParameterDescriptions(actionDescriptor, parsedRoute, route.Defaults);
			string relativePath;
			if (!ApiExplorer.TryExpandUriParameters(route, parsedRoute, list, out relativePath))
			{
				return;
			}
			ApiParameterDescription bodyParameter = list.FirstOrDefault((ApiParameterDescription description) => description.Source == ApiParameterSource.FromBody);
			IEnumerable<MediaTypeFormatter> enumerable = (bodyParameter != null) ? (from f in actionDescriptor.Configuration.Formatters
			where f.CanReadType(bodyParameter.ParameterDescriptor.ParameterType)
			select f) : Enumerable.Empty<MediaTypeFormatter>();
			ResponseDescription responseDescription = this.CreateResponseDescription(actionDescriptor);
			Type returnType = responseDescription.ResponseType ?? responseDescription.DeclaredType;
			IEnumerable<MediaTypeFormatter> enumerable2 = (returnType != null && returnType != typeof(void)) ? (from f in actionDescriptor.Configuration.Formatters
			where f.CanWriteType(returnType)
			select f) : Enumerable.Empty<MediaTypeFormatter>();
			enumerable = ApiExplorer.GetInnerFormatters(enumerable);
			enumerable2 = ApiExplorer.GetInnerFormatters(enumerable2);
			IList<HttpMethod> httpMethodsSupportedByAction = this.GetHttpMethodsSupportedByAction(route, actionDescriptor);
			foreach (HttpMethod httpMethod in httpMethodsSupportedByAction)
			{
				apiDescriptions.Add(new ApiDescription
				{
					Documentation = apiDocumentation,
					HttpMethod = httpMethod,
					RelativePath = relativePath,
					ActionDescriptor = actionDescriptor,
					Route = route,
					SupportedResponseFormatters = new Collection<MediaTypeFormatter>(enumerable2.ToList<MediaTypeFormatter>()),
					SupportedRequestBodyFormatters = new Collection<MediaTypeFormatter>(enumerable.ToList<MediaTypeFormatter>()),
					ParameterDescriptions = new Collection<ApiParameterDescription>(list),
					ResponseDescription = responseDescription
				});
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000D9DC File Offset: 0x0000BBDC
		private ResponseDescription CreateResponseDescription(HttpActionDescriptor actionDescriptor)
		{
			Collection<ResponseTypeAttribute> customAttributes = actionDescriptor.GetCustomAttributes<ResponseTypeAttribute>();
			Type responseType = (from attribute in customAttributes
			select attribute.ResponseType).FirstOrDefault<Type>();
			return new ResponseDescription
			{
				DeclaredType = actionDescriptor.ReturnType,
				ResponseType = responseType,
				Documentation = this.GetApiResponseDocumentation(actionDescriptor)
			};
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		private static IEnumerable<MediaTypeFormatter> GetInnerFormatters(IEnumerable<MediaTypeFormatter> mediaTypeFormatters)
		{
			foreach (MediaTypeFormatter formatter in mediaTypeFormatters)
			{
				yield return Decorator.GetInner<MediaTypeFormatter>(formatter);
			}
			yield break;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000DC11 File Offset: 0x0000BE11
		private static bool ShouldEmitPrefixes(ICollection<ApiParameterDescription> parameterDescriptions)
		{
			return parameterDescriptions.Count((ApiParameterDescription parameter) => parameter.Source == ApiParameterSource.FromUri && parameter.ParameterDescriptor != null && !TypeHelper.CanConvertFromString(parameter.ParameterDescriptor.ParameterType) && parameter.CanConvertPropertiesFromString()) > 1;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		internal static bool TryExpandUriParameters(IHttpRoute route, HttpParsedRoute parsedRoute, ICollection<ApiParameterDescription> parameterDescriptions, out string expandedRouteTemplate)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
			bool flag = ApiExplorer.ShouldEmitPrefixes(parameterDescriptions);
			string prefix = string.Empty;
			foreach (ApiParameterDescription apiParameterDescription in parameterDescriptions)
			{
				if (apiParameterDescription.Source == ApiParameterSource.FromUri)
				{
					if (apiParameterDescription.ParameterDescriptor == null)
					{
						ApiExplorer.AddPlaceholder(dictionary, apiParameterDescription.Name);
					}
					else if (TypeHelper.CanConvertFromString(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						ApiExplorer.AddPlaceholder(dictionary, apiParameterDescription.Name);
					}
					else if (ApiExplorer.IsBindableCollection(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						string parameterName = apiParameterDescription.ParameterDescriptor.ParameterName;
						Type collectionElementType = ApiExplorer.GetCollectionElementType(apiParameterDescription.ParameterDescriptor.ParameterType);
						PropertyInfo[] array = ApiParameterDescription.GetBindableProperties(collectionElementType).ToArray<PropertyInfo>();
						if (array.Any<PropertyInfo>())
						{
							ApiExplorer.AddPlaceholderForProperties(dictionary, array, parameterName + "[0].");
							ApiExplorer.AddPlaceholderForProperties(dictionary, array, parameterName + "[1].");
						}
						else
						{
							ApiExplorer.AddPlaceholder(dictionary, parameterName + "[0]");
							ApiExplorer.AddPlaceholder(dictionary, parameterName + "[1]");
						}
					}
					else if (ApiExplorer.IsBindableKeyValuePair(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						ApiExplorer.AddPlaceholder(dictionary, "key");
						ApiExplorer.AddPlaceholder(dictionary, "value");
					}
					else if (ApiExplorer.IsBindableDictionry(apiParameterDescription.ParameterDescriptor.ParameterType))
					{
						string parameterName2 = apiParameterDescription.ParameterDescriptor.ParameterName;
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[0].key");
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[0].value");
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[1].key");
						ApiExplorer.AddPlaceholder(dictionary, parameterName2 + "[1].value");
					}
					else if (apiParameterDescription.CanConvertPropertiesFromString())
					{
						if (flag)
						{
							prefix = apiParameterDescription.Name + ".";
						}
						ApiExplorer.AddPlaceholderForProperties(dictionary, apiParameterDescription.GetBindableProperties(), prefix);
					}
				}
			}
			BoundRouteTemplate boundRouteTemplate = parsedRoute.Bind(null, dictionary, new HttpRouteValueDictionary(route.Defaults), new HttpRouteValueDictionary(route.Constraints));
			if (boundRouteTemplate == null)
			{
				expandedRouteTemplate = null;
				return false;
			}
			expandedRouteTemplate = Uri.UnescapeDataString(boundRouteTemplate.BoundTemplate);
			return true;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000DE8C File Offset: 0x0000C08C
		private static Type GetCollectionElementType(Type collectionType)
		{
			Type type = collectionType.GetElementType();
			if (type == null)
			{
				type = CollectionModelBinderUtil.GetGenericBinderTypeArgs(typeof(ICollection<>), collectionType).First<Type>();
			}
			return type;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
		private static void AddPlaceholderForProperties(Dictionary<string, object> parameterValuesForRoute, IEnumerable<PropertyInfo> properties, string prefix)
		{
			foreach (PropertyInfo propertyInfo in properties)
			{
				string queryParameterName = prefix + propertyInfo.Name;
				ApiExplorer.AddPlaceholder(parameterValuesForRoute, queryParameterName);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000DF18 File Offset: 0x0000C118
		private static bool IsBindableCollection(Type type)
		{
			return type.IsArray || new CollectionModelBinderProvider().GetBinder(null, type) != null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000DF36 File Offset: 0x0000C136
		private static bool IsBindableDictionry(Type type)
		{
			return new DictionaryModelBinderProvider().GetBinder(null, type) != null;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000DF4A File Offset: 0x0000C14A
		private static bool IsBindableKeyValuePair(Type type)
		{
			return TypeHelper.GetTypeArgumentsIfMatch(type, typeof(KeyValuePair<, >)) != null;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000DF62 File Offset: 0x0000C162
		private static void AddPlaceholder(Dictionary<string, object> parameterValuesForRoute, string queryParameterName)
		{
			if (!parameterValuesForRoute.ContainsKey(queryParameterName))
			{
				parameterValuesForRoute.Add(queryParameterName, "{" + queryParameterName + "}");
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000DF84 File Offset: 0x0000C184
		private IList<ApiParameterDescription> CreateParameterDescriptions(HttpActionDescriptor actionDescriptor, HttpParsedRoute parsedRoute, IDictionary<string, object> routeDefaults)
		{
			IList<ApiParameterDescription> list = new List<ApiParameterDescription>();
			HttpActionBinding actionBinding = ApiExplorer.GetActionBinding(actionDescriptor);
			if (actionBinding != null)
			{
				HttpParameterBinding[] parameterBindings = actionBinding.ParameterBindings;
				if (parameterBindings != null)
				{
					foreach (HttpParameterBinding parameterBinding in parameterBindings)
					{
						list.Add(this.CreateParameterDescriptionFromBinding(parameterBinding));
					}
				}
			}
			else
			{
				Collection<HttpParameterDescriptor> parameters = actionDescriptor.GetParameters();
				if (parameters != null)
				{
					foreach (HttpParameterDescriptor parameter in parameters)
					{
						list.Add(this.CreateParameterDescriptionFromDescriptor(parameter));
					}
				}
			}
			ApiExplorer.AddUndeclaredRouteParameters(parsedRoute, routeDefaults, list);
			return list;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000E050 File Offset: 0x0000C250
		private static void AddUndeclaredRouteParameters(HttpParsedRoute parsedRoute, IDictionary<string, object> routeDefaults, IList<ApiParameterDescription> parameterDescriptions)
		{
			foreach (PathSegment pathSegment in parsedRoute.PathSegments)
			{
				PathContentSegment pathContentSegment = pathSegment as PathContentSegment;
				if (pathContentSegment != null)
				{
					foreach (PathSubsegment pathSubsegment in pathContentSegment.Subsegments)
					{
						PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
						if (pathParameterSubsegment != null)
						{
							string parameterName = pathParameterSubsegment.ParameterName;
							object obj;
							if (!parameterDescriptions.Any((ApiParameterDescription p) => string.Equals(p.Name, parameterName, StringComparison.OrdinalIgnoreCase)) && (!routeDefaults.TryGetValue(parameterName, out obj) || obj != RouteParameter.Optional))
							{
								parameterDescriptions.Add(new ApiParameterDescription
								{
									Name = parameterName,
									Source = ApiParameterSource.FromUri
								});
							}
						}
					}
				}
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000E164 File Offset: 0x0000C364
		private ApiParameterDescription CreateParameterDescriptionFromDescriptor(HttpParameterDescriptor parameter)
		{
			return new ApiParameterDescription
			{
				ParameterDescriptor = parameter,
				Name = (parameter.Prefix ?? parameter.ParameterName),
				Documentation = this.GetApiParameterDocumentation(parameter),
				Source = ApiParameterSource.Unknown
			};
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		private ApiParameterDescription CreateParameterDescriptionFromBinding(HttpParameterBinding parameterBinding)
		{
			ApiParameterDescription apiParameterDescription = this.CreateParameterDescriptionFromDescriptor(parameterBinding.Descriptor);
			if (parameterBinding.WillReadBody)
			{
				apiParameterDescription.Source = ApiParameterSource.FromBody;
			}
			else if (parameterBinding.WillReadUri())
			{
				apiParameterDescription.Source = ApiParameterSource.FromUri;
			}
			return apiParameterDescription;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		private string GetApiDocumentation(HttpActionDescriptor actionDescriptor)
		{
			IDocumentationProvider documentationProvider = this.DocumentationProvider ?? actionDescriptor.Configuration.Services.GetDocumentationProvider();
			if (documentationProvider != null)
			{
				return documentationProvider.GetDocumentation(actionDescriptor);
			}
			return null;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000E21C File Offset: 0x0000C41C
		private string GetApiParameterDocumentation(HttpParameterDescriptor parameterDescriptor)
		{
			IDocumentationProvider documentationProvider = this.DocumentationProvider ?? parameterDescriptor.Configuration.Services.GetDocumentationProvider();
			if (documentationProvider != null)
			{
				return documentationProvider.GetDocumentation(parameterDescriptor);
			}
			return null;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E250 File Offset: 0x0000C450
		private string GetApiResponseDocumentation(HttpActionDescriptor actionDescriptor)
		{
			IDocumentationProvider documentationProvider = this.DocumentationProvider ?? actionDescriptor.Configuration.Services.GetDocumentationProvider();
			if (documentationProvider != null)
			{
				return documentationProvider.GetResponseDocumentation(actionDescriptor);
			}
			return null;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E284 File Offset: 0x0000C484
		private static Collection<ApiDescription> RemoveInvalidApiDescriptions(Collection<ApiDescription> apiDescriptions)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (ApiDescription apiDescription in apiDescriptions)
			{
				string id = apiDescription.ID;
				if (hashSet2.Contains(id))
				{
					hashSet.Add(id);
				}
				else
				{
					hashSet2.Add(id);
				}
			}
			Collection<ApiDescription> collection = new Collection<ApiDescription>();
			foreach (ApiDescription apiDescription2 in apiDescriptions)
			{
				string id2 = apiDescription2.ID;
				if (!hashSet.Contains(id2))
				{
					collection.Add(apiDescription2);
				}
			}
			return collection;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E360 File Offset: 0x0000C560
		private static bool MatchRegexConstraint(IHttpRoute route, string parameterName, string parameterValue)
		{
			IDictionary<string, object> constraints = route.Constraints;
			object obj;
			if (constraints != null && constraints.TryGetValue(parameterName, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					string pattern = "^(" + text + ")$";
					return parameterValue != null && Regex.IsMatch(parameterValue, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
				}
			}
			return true;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		private static HttpActionBinding GetActionBinding(HttpActionDescriptor actionDescriptor)
		{
			HttpControllerDescriptor controllerDescriptor = actionDescriptor.ControllerDescriptor;
			if (controllerDescriptor == null)
			{
				return null;
			}
			ServicesContainer services = controllerDescriptor.Configuration.Services;
			IActionValueBinder actionValueBinder = services.GetActionValueBinder();
			return (actionValueBinder != null) ? actionValueBinder.GetBinding(actionDescriptor) : null;
		}

		// Token: 0x0400013C RID: 316
		private Lazy<Collection<ApiDescription>> _apiDescriptions;

		// Token: 0x0400013D RID: 317
		private readonly HttpConfiguration _config;

		// Token: 0x0400013E RID: 318
		private static readonly Regex _actionVariableRegex = new Regex(string.Format(CultureInfo.CurrentCulture, "{{{0}}}", new object[]
		{
			"action"
		}), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x0400013F RID: 319
		private static readonly Regex _controllerVariableRegex = new Regex(string.Format(CultureInfo.CurrentCulture, "{{{0}}}", new object[]
		{
			"controller"
		}), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x020000BD RID: 189
		private sealed class ApiDescriptionComparer : IEqualityComparer<ApiDescription>
		{
			// Token: 0x06000461 RID: 1121 RVA: 0x0000E455 File Offset: 0x0000C655
			public bool Equals(ApiDescription x, ApiDescription y)
			{
				return string.Equals(x.ID, y.ID, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06000462 RID: 1122 RVA: 0x0000E469 File Offset: 0x0000C669
			public int GetHashCode(ApiDescription obj)
			{
				return obj.ID.ToUpperInvariant().GetHashCode();
			}
		}
	}
}
