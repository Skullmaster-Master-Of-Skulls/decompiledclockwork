using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.WebPages.Scope;

namespace System.Web.Mvc
{
	// Token: 0x02000185 RID: 389
	public class ViewContext : ControllerContext
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x0001D3F6 File Offset: 0x0001B5F6
		public ViewContext()
		{
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001D40C File Offset: 0x0001B60C
		public ViewContext(ControllerContext controllerContext, IView view, ViewDataDictionary viewData, TempDataDictionary tempData, TextWriter writer) : base(controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}
			if (viewData == null)
			{
				throw new ArgumentNullException("viewData");
			}
			if (tempData == null)
			{
				throw new ArgumentNullException("tempData");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.View = view;
			this.ViewData = viewData;
			this.Writer = writer;
			this.TempData = tempData;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x0001D491 File Offset: 0x0001B691
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		public virtual bool ClientValidationEnabled
		{
			get
			{
				return ViewContext.GetClientValidationEnabled(this.Scope, this.HttpContext);
			}
			set
			{
				ViewContext.SetClientValidationEnabled(value, this.Scope, this.HttpContext);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x0001D4DE File Offset: 0x0001B6DE
		public virtual FormContext FormContext
		{
			get
			{
				return (this.HttpContext.Items[ViewContext._formContextKey] as FormContext) ?? this._defaultFormContext;
			}
			set
			{
				this.HttpContext.Items[ViewContext._formContextKey] = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x0001D4F6 File Offset: 0x0001B6F6
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x0001D518 File Offset: 0x0001B718
		internal Func<string> FormIdGenerator
		{
			get
			{
				if (this._formIdGenerator == null)
				{
					this._formIdGenerator = new Func<string>(this.DefaultFormIdGenerator);
				}
				return this._formIdGenerator;
			}
			set
			{
				this._formIdGenerator = value;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0001D521 File Offset: 0x0001B721
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x0001D528 File Offset: 0x0001B728
		internal static Func<IDictionary<object, object>> GlobalScopeThunk { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0001D530 File Offset: 0x0001B730
		private IDictionary<object, object> Scope
		{
			get
			{
				if (this.ScopeThunk != null)
				{
					return this.ScopeThunk();
				}
				if (this._transientScope == null)
				{
					this._transientScope = new Dictionary<object, object>();
				}
				return this._transientScope;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0001D55F File Offset: 0x0001B75F
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0001D570 File Offset: 0x0001B770
		internal Func<IDictionary<object, object>> ScopeThunk
		{
			get
			{
				return this._scopeThunk ?? ViewContext.GlobalScopeThunk;
			}
			set
			{
				this._scopeThunk = value;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0001D579 File Offset: 0x0001B779
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0001D581 File Offset: 0x0001B781
		public virtual TempDataDictionary TempData { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0001D58A File Offset: 0x0001B78A
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0001D59D File Offset: 0x0001B79D
		public virtual bool UnobtrusiveJavaScriptEnabled
		{
			get
			{
				return ViewContext.GetUnobtrusiveJavaScriptEnabled(this.Scope, this.HttpContext);
			}
			set
			{
				ViewContext.SetUnobtrusiveJavaScriptEnabled(value, this.Scope, this.HttpContext);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0001D5B1 File Offset: 0x0001B7B1
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0001D5C4 File Offset: 0x0001B7C4
		public virtual string ValidationSummaryMessageElement
		{
			get
			{
				return ViewContext.GetValidationSummaryMessageElement(this.Scope, this.HttpContext);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw Error.ParameterCannotBeNullOrEmpty("value");
				}
				ViewContext.SetValidationSummaryMessageElement(value, this.Scope, this.HttpContext);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0001D5EB File Offset: 0x0001B7EB
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0001D5FE File Offset: 0x0001B7FE
		public virtual string ValidationMessageElement
		{
			get
			{
				return ViewContext.GetValidationMessageElement(this.Scope, this.HttpContext);
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw Error.ParameterCannotBeNullOrEmpty("value");
				}
				ViewContext.SetValidationMessageElement(value, this.Scope, this.HttpContext);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0001D625 File Offset: 0x0001B825
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0001D62D File Offset: 0x0001B82D
		public virtual IView View { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0001D640 File Offset: 0x0001B840
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001D679 File Offset: 0x0001B879
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0001D681 File Offset: 0x0001B881
		public virtual ViewDataDictionary ViewData { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001D68A File Offset: 0x0001B88A
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0001D692 File Offset: 0x0001B892
		public virtual TextWriter Writer { get; set; }

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001D69C File Offset: 0x0001B89C
		private string DefaultFormIdGenerator()
		{
			int num = ViewContext.IncrementFormCount(this.HttpContext.Items);
			return string.Format(CultureInfo.InvariantCulture, "form{0}", new object[]
			{
				num
			});
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001D6DA File Offset: 0x0001B8DA
		internal static bool GetClientValidationEnabled(IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			return ViewContext.ScopeCache.Get(scope, httpContext).ClientValidationEnabled;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		internal FormContext GetFormContextForClientValidation()
		{
			if (!this.ClientValidationEnabled)
			{
				return null;
			}
			return this.FormContext;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0001D6FA File Offset: 0x0001B8FA
		internal static bool GetUnobtrusiveJavaScriptEnabled(IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			return ViewContext.ScopeCache.Get(scope, httpContext).UnobtrusiveJavaScriptEnabled;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001D708 File Offset: 0x0001B908
		internal static string GetValidationSummaryMessageElement(IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			return ViewContext.ScopeCache.Get(scope, httpContext).ValidationSummaryMessageElement;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001D716 File Offset: 0x0001B916
		internal static string GetValidationMessageElement(IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			return ViewContext.ScopeCache.Get(scope, httpContext).ValidationMessageElement;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001D724 File Offset: 0x0001B924
		private static int IncrementFormCount(IDictionary items)
		{
			object obj = items[ViewContext._lastFormNumKey];
			int num = (obj != null) ? ((int)obj + 1) : 0;
			items[ViewContext._lastFormNumKey] = num;
			return num;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0001D760 File Offset: 0x0001B960
		public void OutputClientValidation()
		{
			FormContext formContextForClientValidation = this.GetFormContextForClientValidation();
			if (formContextForClientValidation == null || this.UnobtrusiveJavaScriptEnabled)
			{
				return;
			}
			string format = "<script type=\"text/javascript\">\r\n//<![CDATA[\r\nif (!window.mvcClientValidationMetadata) {{ window.mvcClientValidationMetadata = []; }}\r\nwindow.mvcClientValidationMetadata.push({0});\r\n//]]>\r\n</script>".Replace("\r\n", Environment.NewLine);
			string jsonValidationMetadata = formContextForClientValidation.GetJsonValidationMetadata();
			string value = string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				jsonValidationMetadata
			});
			this.Writer.Write(value);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0001D7C3 File Offset: 0x0001B9C3
		internal static void SetClientValidationEnabled(bool enabled, IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			ViewContext.ScopeCache.Get(scope, httpContext).ClientValidationEnabled = enabled;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001D7D2 File Offset: 0x0001B9D2
		internal static void SetUnobtrusiveJavaScriptEnabled(bool enabled, IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			ViewContext.ScopeCache.Get(scope, httpContext).UnobtrusiveJavaScriptEnabled = enabled;
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001D7E1 File Offset: 0x0001B9E1
		internal static void SetValidationSummaryMessageElement(string elementName, IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			ViewContext.ScopeCache.Get(scope, httpContext).ValidationSummaryMessageElement = elementName;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001D7F0 File Offset: 0x0001B9F0
		internal static void SetValidationMessageElement(string elementName, IDictionary<object, object> scope = null, HttpContextBase httpContext = null)
		{
			ViewContext.ScopeCache.Get(scope, httpContext).ValidationMessageElement = elementName;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001D800 File Offset: 0x0001BA00
		private static TValue ScopeGet<TValue>(IDictionary<object, object> scope, string name, TValue defaultValue = default(TValue))
		{
			object value;
			if (scope.TryGetValue(name, out value))
			{
				return (TValue)((object)Convert.ChangeType(value, typeof(TValue), CultureInfo.InvariantCulture));
			}
			return defaultValue;
		}

		// Token: 0x040002D9 RID: 729
		private const string ClientValidationScript = "<script type=\"text/javascript\">\r\n//<![CDATA[\r\nif (!window.mvcClientValidationMetadata) {{ window.mvcClientValidationMetadata = []; }}\r\nwindow.mvcClientValidationMetadata.push({0});\r\n//]]>\r\n</script>";

		// Token: 0x040002DA RID: 730
		internal static readonly string ClientValidationKeyName = "ClientValidationEnabled";

		// Token: 0x040002DB RID: 731
		internal static readonly string UnobtrusiveJavaScriptKeyName = "UnobtrusiveJavaScriptEnabled";

		// Token: 0x040002DC RID: 732
		internal static readonly string ValidationSummaryMessageElementKeyName = "ValidationSummaryMessageElement";

		// Token: 0x040002DD RID: 733
		internal static readonly string ValidationMessageElementKeyName = "ValidationMessageElement";

		// Token: 0x040002DE RID: 734
		private static readonly object _formContextKey = new object();

		// Token: 0x040002DF RID: 735
		private static readonly object _lastFormNumKey = new object();

		// Token: 0x040002E0 RID: 736
		private Func<IDictionary<object, object>> _scopeThunk;

		// Token: 0x040002E1 RID: 737
		private IDictionary<object, object> _transientScope;

		// Token: 0x040002E2 RID: 738
		private DynamicViewDataDictionary _dynamicViewDataDictionary;

		// Token: 0x040002E3 RID: 739
		private Func<string> _formIdGenerator;

		// Token: 0x040002E4 RID: 740
		private FormContext _defaultFormContext = new FormContext();

		// Token: 0x02000186 RID: 390
		private sealed class ScopeCache
		{
			// Token: 0x06000AE9 RID: 2793 RVA: 0x0001D874 File Offset: 0x0001BA74
			private ScopeCache(IDictionary<object, object> scope)
			{
				this._scope = scope;
				this._clientValidationEnabled = ViewContext.ScopeGet<bool>(scope, ViewContext.ClientValidationKeyName, false);
				this._unobtrusiveJavaScriptEnabled = ViewContext.ScopeGet<bool>(scope, ViewContext.UnobtrusiveJavaScriptKeyName, false);
				this._validationSummaryMessageElement = ViewContext.ScopeGet<string>(scope, ViewContext.ValidationSummaryMessageElementKeyName, "span");
				this._validationMessageElement = ViewContext.ScopeGet<string>(scope, ViewContext.ValidationMessageElementKeyName, "span");
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0001D8DE File Offset: 0x0001BADE
			// (set) Token: 0x06000AEB RID: 2795 RVA: 0x0001D8E6 File Offset: 0x0001BAE6
			public bool ClientValidationEnabled
			{
				get
				{
					return this._clientValidationEnabled;
				}
				set
				{
					this._clientValidationEnabled = value;
					this._scope[ViewContext.ClientValidationKeyName] = value;
				}
			}

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0001D905 File Offset: 0x0001BB05
			// (set) Token: 0x06000AED RID: 2797 RVA: 0x0001D90D File Offset: 0x0001BB0D
			public bool UnobtrusiveJavaScriptEnabled
			{
				get
				{
					return this._unobtrusiveJavaScriptEnabled;
				}
				set
				{
					this._unobtrusiveJavaScriptEnabled = value;
					this._scope[ViewContext.UnobtrusiveJavaScriptKeyName] = value;
				}
			}

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0001D92C File Offset: 0x0001BB2C
			// (set) Token: 0x06000AEF RID: 2799 RVA: 0x0001D934 File Offset: 0x0001BB34
			public string ValidationSummaryMessageElement
			{
				get
				{
					return this._validationSummaryMessageElement;
				}
				set
				{
					this._validationSummaryMessageElement = value;
					this._scope[ViewContext.ValidationSummaryMessageElementKeyName] = value;
				}
			}

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x0001D94E File Offset: 0x0001BB4E
			// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x0001D956 File Offset: 0x0001BB56
			public string ValidationMessageElement
			{
				get
				{
					return this._validationMessageElement;
				}
				set
				{
					this._validationMessageElement = value;
					this._scope[ViewContext.ValidationMessageElementKeyName] = value;
				}
			}

			// Token: 0x06000AF2 RID: 2802 RVA: 0x0001D970 File Offset: 0x0001BB70
			public static ViewContext.ScopeCache Get(IDictionary<object, object> scope, HttpContextBase httpContext)
			{
				if (httpContext == null && System.Web.HttpContext.Current != null)
				{
					httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
				}
				ViewContext.ScopeCache scopeCache = null;
				scope = (scope ?? ScopeStorage.CurrentScope);
				if (httpContext != null)
				{
					scopeCache = (httpContext.Items[ViewContext.ScopeCache._cacheKey] as ViewContext.ScopeCache);
				}
				if (scopeCache == null || scopeCache._scope != scope)
				{
					scopeCache = new ViewContext.ScopeCache(scope);
					if (httpContext != null)
					{
						httpContext.Items[ViewContext.ScopeCache._cacheKey] = scopeCache;
					}
				}
				return scopeCache;
			}

			// Token: 0x040002EA RID: 746
			private static readonly object _cacheKey = new object();

			// Token: 0x040002EB RID: 747
			private bool _clientValidationEnabled;

			// Token: 0x040002EC RID: 748
			private IDictionary<object, object> _scope;

			// Token: 0x040002ED RID: 749
			private bool _unobtrusiveJavaScriptEnabled;

			// Token: 0x040002EE RID: 750
			private string _validationSummaryMessageElement;

			// Token: 0x040002EF RID: 751
			private string _validationMessageElement;
		}
	}
}
