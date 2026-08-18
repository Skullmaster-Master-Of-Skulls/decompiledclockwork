using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc.Properties;
using System.Web.Routing;
using System.Web.WebPages;
using System.Web.WebPages.Html;
using System.Web.WebPages.Scope;

namespace System.Web.Mvc
{
	// Token: 0x0200017F RID: 383
	public class HtmlHelper
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x0001C259 File Offset: 0x0001A459
		public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer) : this(viewContext, viewDataContainer, RouteTable.Routes)
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public HtmlHelper(ViewContext viewContext, IViewDataContainer viewDataContainer, RouteCollection routeCollection)
		{
			if (viewContext == null)
			{
				throw new ArgumentNullException("viewContext");
			}
			if (viewDataContainer == null)
			{
				throw new ArgumentNullException("viewDataContainer");
			}
			if (routeCollection == null)
			{
				throw new ArgumentNullException("routeCollection");
			}
			this.ViewContext = viewContext;
			this.ViewDataContainer = viewDataContainer;
			this.RouteCollection = routeCollection;
			this.ClientValidationRuleFactory = ((string name, ModelMetadata metadata) => ModelValidatorProviders.Providers.GetValidators(metadata ?? ModelMetadata.FromStringExpression(name, this.ViewData), this.ViewContext).SelectMany((ModelValidator v) => v.GetClientValidationRules()));
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x0001C32B File Offset: 0x0001A52B
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x0001C334 File Offset: 0x0001A534
		public static bool ClientValidationEnabled
		{
			get
			{
				return ViewContext.GetClientValidationEnabled(null, null);
			}
			set
			{
				ViewContext.SetClientValidationEnabled(value, null, null);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0001C33E File Offset: 0x0001A53E
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x0001C345 File Offset: 0x0001A545
		public static string IdAttributeDotReplacement
		{
			get
			{
				return HtmlHelper.IdAttributeDotReplacement;
			}
			set
			{
				HtmlHelper.IdAttributeDotReplacement = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0001C34D File Offset: 0x0001A54D
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0001C355 File Offset: 0x0001A555
		internal Func<string, ModelMetadata, IEnumerable<ModelClientValidationRule>> ClientValidationRuleFactory { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0001C35E File Offset: 0x0001A55E
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0001C366 File Offset: 0x0001A566
		public RouteCollection RouteCollection { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0001C36F File Offset: 0x0001A56F
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0001C378 File Offset: 0x0001A578
		public static bool UnobtrusiveJavaScriptEnabled
		{
			get
			{
				return ViewContext.GetUnobtrusiveJavaScriptEnabled(null, null);
			}
			set
			{
				ViewContext.SetUnobtrusiveJavaScriptEnabled(value, null, null);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001C382 File Offset: 0x0001A582
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0001C38B File Offset: 0x0001A58B
		public static string ValidationSummaryMessageElement
		{
			get
			{
				return ViewContext.GetValidationSummaryMessageElement(null, null);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw Error.ParameterCannotBeNullOrEmpty("value");
				}
				ViewContext.SetValidationSummaryMessageElement(value, null, null);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001C3A8 File Offset: 0x0001A5A8
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0001C3B1 File Offset: 0x0001A5B1
		public static string ValidationMessageElement
		{
			get
			{
				return ViewContext.GetValidationMessageElement(null, null);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw Error.ParameterCannotBeNullOrEmpty("value");
				}
				ViewContext.SetValidationMessageElement(value, null, null);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x0001C3D8 File Offset: 0x0001A5D8
		[Dynamic]
		public dynamic ViewBag
		{
			[return: Dynamic]
			get
			{
				if (this._dynamicViewDataDictionary == null)
				{
					this._dynamicViewDataDictionary = new DynamicViewDataDictionary(() => this.ViewData);
				}
				return this._dynamicViewDataDictionary;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0001C411 File Offset: 0x0001A611
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x0001C419 File Offset: 0x0001A619
		public ViewContext ViewContext { get; private set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x0001C422 File Offset: 0x0001A622
		public ViewDataDictionary ViewData
		{
			get
			{
				return this.ViewDataContainer.ViewData;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0001C42F File Offset: 0x0001A62F
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x0001C437 File Offset: 0x0001A637
		public IViewDataContainer ViewDataContainer { get; internal set; }

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001C440 File Offset: 0x0001A640
		public static RouteValueDictionary AnonymousObjectToHtmlAttributes(object htmlAttributes)
		{
			return HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0001C448 File Offset: 0x0001A648
		public MvcHtmlString AntiForgeryToken()
		{
			return new MvcHtmlString(AntiForgery.GetHtml().ToString());
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0001C45C File Offset: 0x0001A65C
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x0001C484 File Offset: 0x0001A684
		public Html5DateRenderingMode Html5DateRenderingMode
		{
			get
			{
				object obj;
				if (ScopeStorage.CurrentScope.TryGetValue(HtmlHelper._html5InputsModeKey, out obj))
				{
					return (Html5DateRenderingMode)obj;
				}
				return Html5DateRenderingMode.CurrentCulture;
			}
			set
			{
				ScopeStorage.CurrentScope[HtmlHelper._html5InputsModeKey] = value;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0001C49B File Offset: 0x0001A69B
		[Obsolete("This method is deprecated. Use the AntiForgeryToken() method instead. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MvcHtmlString AntiForgeryToken(string salt)
		{
			if (!string.IsNullOrEmpty(salt))
			{
				throw new NotSupportedException("This method is deprecated. Use the AntiForgeryToken() method instead. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.");
			}
			return this.AntiForgeryToken();
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0001C4B6 File Offset: 0x0001A6B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This method is deprecated. Use the AntiForgeryToken() method instead. To specify a custom domain for the generated cookie, use the <httpCookies> configuration element. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.", true)]
		public MvcHtmlString AntiForgeryToken(string salt, string domain, string path)
		{
			if (!string.IsNullOrEmpty(salt) || !string.IsNullOrEmpty(domain) || !string.IsNullOrEmpty(path))
			{
				throw new NotSupportedException("This method is deprecated. Use the AntiForgeryToken() method instead. To specify a custom domain for the generated cookie, use the <httpCookies> configuration element. To specify custom data to be embedded within the token, use the static AntiForgeryConfig.AdditionalDataProvider property.");
			}
			return this.AntiForgeryToken();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0001C4E1 File Offset: 0x0001A6E1
		public string AttributeEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return HttpUtility.HtmlAttributeEncode(value);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0001C4F7 File Offset: 0x0001A6F7
		public string AttributeEncode(object value)
		{
			return this.AttributeEncode(Convert.ToString(value, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0001C50A File Offset: 0x0001A70A
		public void EnableClientValidation()
		{
			this.EnableClientValidation(true);
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0001C513 File Offset: 0x0001A713
		public void EnableClientValidation(bool enabled)
		{
			this.ViewContext.ClientValidationEnabled = enabled;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001C521 File Offset: 0x0001A721
		public void EnableUnobtrusiveJavaScript()
		{
			this.EnableUnobtrusiveJavaScript(true);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001C52A File Offset: 0x0001A72A
		public void EnableUnobtrusiveJavaScript(bool enabled)
		{
			this.ViewContext.UnobtrusiveJavaScriptEnabled = enabled;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001C538 File Offset: 0x0001A738
		public string Encode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			return HttpUtility.HtmlEncode(value);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0001C54E File Offset: 0x0001A74E
		public string Encode(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			return HttpUtility.HtmlEncode(value);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0001C55F File Offset: 0x0001A75F
		internal string EvalString(string key)
		{
			return Convert.ToString(this.ViewData.Eval(key), CultureInfo.CurrentCulture);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0001C577 File Offset: 0x0001A777
		internal string EvalString(string key, string format)
		{
			return Convert.ToString(this.ViewData.Eval(key, format), CultureInfo.CurrentCulture);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0001C590 File Offset: 0x0001A790
		public string FormatValue(object value, string format)
		{
			return ViewDataDictionary.FormatValueInternal(value, format);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0001C599 File Offset: 0x0001A799
		internal bool EvalBoolean(string key)
		{
			return Convert.ToBoolean(this.ViewData.Eval(key), CultureInfo.InvariantCulture);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		internal static IView FindPartialView(ViewContext viewContext, string partialViewName, ViewEngineCollection viewEngineCollection)
		{
			ViewEngineResult viewEngineResult = viewEngineCollection.FindPartialView(viewContext, partialViewName);
			if (viewEngineResult.View != null)
			{
				return viewEngineResult.View;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in viewEngineResult.SearchedLocations)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(value);
			}
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.Common_PartialViewNotFound, new object[]
			{
				partialViewName,
				stringBuilder
			}));
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0001C650 File Offset: 0x0001A850
		public static string GenerateIdFromName(string name)
		{
			return HtmlHelper.GenerateIdFromName(name, HtmlHelper.IdAttributeDotReplacement);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0001C65D File Offset: 0x0001A85D
		public static string GenerateIdFromName(string name, string idAttributeDotReplacement)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (idAttributeDotReplacement == null)
			{
				throw new ArgumentNullException("idAttributeDotReplacement");
			}
			if (name.Length == 0)
			{
				return string.Empty;
			}
			return TagBuilder.CreateSanitizedId(name, idAttributeDotReplacement);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0001C690 File Offset: 0x0001A890
		public static string GenerateLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return HtmlHelper.GenerateLink(requestContext, routeCollection, linkText, routeName, actionName, controllerName, null, null, null, routeValues, htmlAttributes);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		public static string GenerateLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return HtmlHelper.GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, htmlAttributes, true);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		private static string GenerateLinkInternal(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string actionName, string controllerName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, bool includeImplicitMvcValues)
		{
			string value = UrlHelper.GenerateUrl(routeName, actionName, controllerName, protocol, hostName, fragment, routeValues, routeCollection, requestContext, includeImplicitMvcValues);
			TagBuilder tagBuilder = new TagBuilder("a")
			{
				InnerHtml = ((!string.IsNullOrEmpty(linkText)) ? HttpUtility.HtmlEncode(linkText) : string.Empty)
			};
			tagBuilder.MergeAttributes<string, object>(htmlAttributes);
			tagBuilder.MergeAttribute("href", value);
			return tagBuilder.ToString(TagRenderMode.Normal);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0001C744 File Offset: 0x0001A944
		public static string GenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return HtmlHelper.GenerateRouteLink(requestContext, routeCollection, linkText, routeName, null, null, null, routeValues, htmlAttributes);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001C764 File Offset: 0x0001A964
		public static string GenerateRouteLink(RequestContext requestContext, RouteCollection routeCollection, string linkText, string routeName, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
		{
			return HtmlHelper.GenerateLinkInternal(requestContext, routeCollection, linkText, routeName, null, null, protocol, hostName, fragment, routeValues, htmlAttributes, false);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001C788 File Offset: 0x0001A988
		public static string GetFormMethodString(FormMethod method)
		{
			switch (method)
			{
			case FormMethod.Get:
				return "get";
			case FormMethod.Post:
				return "post";
			default:
				return "post";
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
		public static string GetInputTypeString(InputType inputType)
		{
			switch (inputType)
			{
			case InputType.CheckBox:
				return "checkbox";
			case InputType.Hidden:
				return "hidden";
			case InputType.Password:
				return "password";
			case InputType.Radio:
				return "radio";
			case InputType.Text:
				return "text";
			default:
				return "text";
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001C808 File Offset: 0x0001AA08
		internal object GetModelStateValue(string key, Type destinationType)
		{
			ModelState modelState;
			if (this.ViewData.ModelState.TryGetValue(key, out modelState) && modelState.Value != null)
			{
				return modelState.Value.ConvertTo(destinationType, null);
			}
			return null;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0001C841 File Offset: 0x0001AA41
		public IDictionary<string, object> GetUnobtrusiveValidationAttributes(string name)
		{
			return this.GetUnobtrusiveValidationAttributes(name, null);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0001C84C File Offset: 0x0001AA4C
		public IDictionary<string, object> GetUnobtrusiveValidationAttributes(string name, ModelMetadata metadata)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!this.ViewContext.UnobtrusiveJavaScriptEnabled)
			{
				return dictionary;
			}
			FormContext formContextForClientValidation = this.ViewContext.GetFormContextForClientValidation();
			if (formContextForClientValidation == null)
			{
				return dictionary;
			}
			string fullHtmlFieldName = this.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
			if (formContextForClientValidation.RenderedField(fullHtmlFieldName))
			{
				return dictionary;
			}
			formContextForClientValidation.RenderedField(fullHtmlFieldName, true);
			IEnumerable<ModelClientValidationRule> clientRules = this.ClientValidationRuleFactory(name, metadata);
			UnobtrusiveValidationAttributesGenerator.GetValidationAttributes(clientRules, dictionary);
			return dictionary;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0001C8BC File Offset: 0x0001AABC
		public MvcHtmlString HttpMethodOverride(HttpVerbs httpVerb)
		{
			string httpMethod;
			if (httpVerb <= HttpVerbs.Delete)
			{
				if (httpVerb == HttpVerbs.Put)
				{
					httpMethod = "PUT";
					goto IL_59;
				}
				if (httpVerb == HttpVerbs.Delete)
				{
					httpMethod = "DELETE";
					goto IL_59;
				}
			}
			else
			{
				if (httpVerb == HttpVerbs.Head)
				{
					httpMethod = "HEAD";
					goto IL_59;
				}
				if (httpVerb == HttpVerbs.Patch)
				{
					httpMethod = "PATCH";
					goto IL_59;
				}
				if (httpVerb == HttpVerbs.Options)
				{
					httpMethod = "OPTIONS";
					goto IL_59;
				}
			}
			throw new ArgumentException(MvcResources.HtmlHelper_InvalidHttpVerb, "httpVerb");
			IL_59:
			return this.HttpMethodOverride(httpMethod);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0001C92C File Offset: 0x0001AB2C
		public MvcHtmlString HttpMethodOverride(string httpMethod)
		{
			if (string.IsNullOrEmpty(httpMethod))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "httpMethod");
			}
			if (string.Equals(httpMethod, "GET", StringComparison.OrdinalIgnoreCase) || string.Equals(httpMethod, "POST", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(MvcResources.HtmlHelper_InvalidHttpMethod, "httpMethod");
			}
			TagBuilder tagBuilder = new TagBuilder("input");
			tagBuilder.Attributes["type"] = "hidden";
			tagBuilder.Attributes["name"] = "X-HTTP-Method-Override";
			tagBuilder.Attributes["value"] = httpMethod;
			return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001C9CA File Offset: 0x0001ABCA
		public IHtmlString Raw(string value)
		{
			return new HtmlString(value);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001C9D2 File Offset: 0x0001ABD2
		public IHtmlString Raw(object value)
		{
			return new HtmlString((value == null) ? null : value.ToString());
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		internal virtual void RenderPartialInternal(string partialViewName, ViewDataDictionary viewData, object model, TextWriter writer, ViewEngineCollection viewEngineCollection)
		{
			if (string.IsNullOrEmpty(partialViewName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "partialViewName");
			}
			ViewDataDictionary viewData2;
			if (model == null)
			{
				if (viewData == null)
				{
					viewData2 = new ViewDataDictionary(this.ViewData);
				}
				else
				{
					viewData2 = new ViewDataDictionary(viewData);
				}
			}
			else if (viewData == null)
			{
				viewData2 = new ViewDataDictionary(model);
			}
			else
			{
				viewData2 = new ViewDataDictionary(viewData)
				{
					Model = model
				};
			}
			ViewContext viewContext = new ViewContext(this.ViewContext, this.ViewContext.View, viewData2, this.ViewContext.TempData, writer);
			IView view = HtmlHelper.FindPartialView(viewContext, partialViewName, viewEngineCollection);
			view.Render(viewContext, writer);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001CA80 File Offset: 0x0001AC80
		public void SetValidationSummaryMessageElement(string elementName)
		{
			if (string.IsNullOrEmpty(elementName))
			{
				throw Error.ParameterCannotBeNullOrEmpty("elementName");
			}
			this.ViewContext.ValidationSummaryMessageElement = elementName;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001CAA1 File Offset: 0x0001ACA1
		public void SetValidationMessageElement(string elementName)
		{
			if (string.IsNullOrEmpty(elementName))
			{
				throw Error.ParameterCannotBeNullOrEmpty("elementName");
			}
			this.ViewContext.ValidationMessageElement = elementName;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001CAC2 File Offset: 0x0001ACC2
		public static IDictionary<string, object> ObjectToDictionary(object value)
		{
			return TypeHelper.ObjectToDictionary(value);
		}

		// Token: 0x040002C4 RID: 708
		public static readonly string ValidationInputCssClassName = "input-validation-error";

		// Token: 0x040002C5 RID: 709
		public static readonly string ValidationInputValidCssClassName = "input-validation-valid";

		// Token: 0x040002C6 RID: 710
		public static readonly string ValidationMessageCssClassName = "field-validation-error";

		// Token: 0x040002C7 RID: 711
		public static readonly string ValidationMessageValidCssClassName = "field-validation-valid";

		// Token: 0x040002C8 RID: 712
		public static readonly string ValidationSummaryCssClassName = "validation-summary-errors";

		// Token: 0x040002C9 RID: 713
		public static readonly string ValidationSummaryValidCssClassName = "validation-summary-valid";

		// Token: 0x040002CA RID: 714
		private static readonly object _html5InputsModeKey = new object();

		// Token: 0x040002CB RID: 715
		private DynamicViewDataDictionary _dynamicViewDataDictionary;
	}
}
