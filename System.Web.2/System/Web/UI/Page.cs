using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Configuration;
using System.EnterpriseServices;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.ModelBinding;
using System.Web.RegularExpressions;
using System.Web.Routing;
using System.Web.Security.Cryptography;
using System.Web.SessionState;
using System.Web.UI.Adapters;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Util;
using System.Xml;

namespace System.Web.UI
{
	// Token: 0x020002D3 RID: 723
	[DefaultEvent("Load")]
	[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[DesignerCategory("ASPXCodeBehind")]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.WebFormCodeDomSerializer, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.TypeCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	public class Page : TemplateControl, IHttpHandler
	{
		// Token: 0x06002081 RID: 8321 RVA: 0x000684A0 File Offset: 0x000666A0
		static Page()
		{
			Page.s_systemPostFields = new StringSet();
			Page.s_systemPostFields.Add("__EVENTTARGET");
			Page.s_systemPostFields.Add("__EVENTARGUMENT");
			Page.s_systemPostFields.Add("__VIEWSTATEFIELDCOUNT");
			Page.s_systemPostFields.Add("__VIEWSTATEGENERATOR");
			Page.s_systemPostFields.Add("__VIEWSTATE");
			Page.s_systemPostFields.Add("__VIEWSTATEENCRYPTED");
			Page.s_systemPostFields.Add("__PREVIOUSPAGE");
			Page.s_systemPostFields.Add("__CALLBACKID");
			Page.s_systemPostFields.Add("__CALLBACKPARAM");
			Page.s_systemPostFields.Add("__LASTFOCUS");
			Page.s_systemPostFields.Add(Page.UniqueFilePathSuffixID);
			Page.s_systemPostFields.Add(HttpResponse.RedirectQueryStringVariable);
			Page.s_systemPostFields.Add("__EVENTVALIDATION");
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0006862C File Offset: 0x0006682C
		public Page()
		{
			this._page = this;
			this._enableViewStateMac = true;
			this.ID = "__Page";
			this._supportsStyleSheets = -1;
			base.SetValidateRequestModeInternal(ValidateRequestMode.Enabled, false);
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x00068679 File Offset: 0x00066879
		public ModelStateDictionary ModelState
		{
			get
			{
				if (this._modelState == null)
				{
					this._modelState = new ModelStateDictionary();
				}
				return this._modelState;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x00068694 File Offset: 0x00066894
		// (set) Token: 0x06002085 RID: 8325 RVA: 0x0006869C File Offset: 0x0006689C
		private IValueProvider ActiveValueProvider { get; set; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x000686A5 File Offset: 0x000668A5
		// (set) Token: 0x06002087 RID: 8327 RVA: 0x000686AD File Offset: 0x000668AD
		internal bool IsExecutingAsyncTasks
		{
			get
			{
				return this._executingAsyncTasks;
			}
			set
			{
				this._executingAsyncTasks = value;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x000686B8 File Offset: 0x000668B8
		public ModelBindingExecutionContext ModelBindingExecutionContext
		{
			get
			{
				if (this._modelBindingExecutionContext == null)
				{
					this._modelBindingExecutionContext = new ModelBindingExecutionContext(new HttpContextWrapper(this.Context), this.ModelState);
					this._modelBindingExecutionContext.PublishService<StateBag>(this.ViewState);
					this._modelBindingExecutionContext.PublishService<RouteData>(this.RouteData);
				}
				return this._modelBindingExecutionContext;
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00068711 File Offset: 0x00066911
		internal void SetActiveValueProvider(IValueProvider valueProvider)
		{
			this.ActiveValueProvider = valueProvider;
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0006871A File Offset: 0x0006691A
		public virtual bool TryUpdateModel<TModel>(TModel model) where TModel : class
		{
			if (this.ActiveValueProvider == null)
			{
				throw new InvalidOperationException(SR.GetString("Page_InvalidUpdateModelAttempt", new object[]
				{
					"TryUpdateModel"
				}));
			}
			return this.TryUpdateModel<TModel>(model, this.ActiveValueProvider);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00068750 File Offset: 0x00066950
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (valueProvider == null)
			{
				throw new ArgumentNullException("valueProvider");
			}
			IModelBinder defaultBinder = ModelBinders.Binders.DefaultBinder;
			ModelBindingContext bindingContext = new ModelBindingContext
			{
				ModelBinderProviders = ModelBinderProviders.Providers,
				ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeof(TModel)),
				ModelState = this.ModelState,
				ValueProvider = valueProvider
			};
			return defaultBinder.BindModel(this.ModelBindingExecutionContext, bindingContext) && this.ModelState.IsValid;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00068803 File Offset: 0x00066A03
		public virtual void UpdateModel<TModel>(TModel model) where TModel : class
		{
			if (this.ActiveValueProvider == null)
			{
				throw new InvalidOperationException(SR.GetString("Page_InvalidUpdateModelAttempt", new object[]
				{
					"UpdateModel"
				}));
			}
			this.UpdateModel<TModel>(model, this.ActiveValueProvider);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00068838 File Offset: 0x00066A38
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider) where TModel : class
		{
			if (!this.TryUpdateModel<TModel>(model, valueProvider))
			{
				throw new InvalidOperationException(SR.GetString("Page_UpdateModel_UpdateUnsuccessful", new object[]
				{
					typeof(TModel).FullName
				}));
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0006886C File Offset: 0x00066A6C
		// (set) Token: 0x0600208F RID: 8335 RVA: 0x00068896 File Offset: 0x00066A96
		[DefaultValue(UnobtrusiveValidationMode.None)]
		[WebCategory("Behavior")]
		[WebSysDescription("Page_UnobtrusiveValidationMode")]
		public UnobtrusiveValidationMode UnobtrusiveValidationMode
		{
			get
			{
				UnobtrusiveValidationMode? unobtrusiveValidationMode = this._unobtrusiveValidationMode;
				if (unobtrusiveValidationMode == null)
				{
					return ValidationSettings.UnobtrusiveValidationMode;
				}
				return unobtrusiveValidationMode.GetValueOrDefault();
			}
			set
			{
				if (value < UnobtrusiveValidationMode.None || value > UnobtrusiveValidationMode.WebForms)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._unobtrusiveValidationMode = new UnobtrusiveValidationMode?(value);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x000688B7 File Offset: 0x00066AB7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpApplicationState Application
		{
			get
			{
				return this._application;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x000688BF File Offset: 0x00066ABF
		protected internal override HttpContext Context
		{
			get
			{
				if (this._context == null)
				{
					this._context = HttpContext.Current;
				}
				return this._context;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x000688DA File Offset: 0x00066ADA
		private StringSet ControlStateLoadedControlIds
		{
			get
			{
				if (this._controlStateLoadedControlIds == null)
				{
					this._controlStateLoadedControlIds = new StringSet();
				}
				return this._controlStateLoadedControlIds;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x000688F5 File Offset: 0x00066AF5
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x000688FD File Offset: 0x00066AFD
		internal string ClientState
		{
			get
			{
				return this._clientState;
			}
			set
			{
				this._clientState = value;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x00068906 File Offset: 0x00066B06
		internal string ClientOnSubmitEvent
		{
			get
			{
				if (this.ClientScript.HasSubmitStatements || (this.Form != null && this.Form.SubmitDisabledControls && this.EnabledControls.Count > 0))
				{
					return "javascript:return WebForm_OnSubmit();";
				}
				return string.Empty;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x00068943 File Offset: 0x00066B43
		public ClientScriptManager ClientScript
		{
			get
			{
				if (this._clientScriptManager == null)
				{
					this._clientScriptManager = new ClientScriptManager(this);
				}
				return this._clientScriptManager;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0006895F File Offset: 0x00066B5F
		// (set) Token: 0x06002098 RID: 8344 RVA: 0x00068975 File Offset: 0x00066B75
		[DefaultValue("")]
		[WebSysDescription("Page_ClientTarget")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ClientTarget
		{
			get
			{
				if (this._clientTarget != null)
				{
					return this._clientTarget;
				}
				return string.Empty;
			}
			set
			{
				this._clientTarget = value;
				if (this._request != null)
				{
					this._request.ClientTarget = value;
				}
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00068994 File Offset: 0x00066B94
		public string ClientQueryString
		{
			get
			{
				if (this._clientQueryString == null)
				{
					if (this.RequestInternal != null && this.Request.HasQueryString)
					{
						Hashtable hashtable = new Hashtable();
						foreach (object obj in ((IEnumerable)Page.s_systemPostFields))
						{
							string key = (string)obj;
							hashtable.Add(key, true);
						}
						HttpValueCollection httpValueCollection = (HttpValueCollection)(this.SkipFormActionValidation ? this.Request.Unvalidated.QueryString : this.Request.QueryString);
						this._clientQueryString = httpValueCollection.ToString(true, hashtable);
					}
					else
					{
						this._clientQueryString = string.Empty;
					}
				}
				return this._clientQueryString;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00068A70 File Offset: 0x00066C70
		// (set) Token: 0x0600209B RID: 8347 RVA: 0x00068A78 File Offset: 0x00066C78
		internal bool ContainsEncryptedViewState
		{
			get
			{
				return this._containsEncryptedViewState;
			}
			set
			{
				this._containsEncryptedViewState = value;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x00068A81 File Offset: 0x00066C81
		// (set) Token: 0x0600209D RID: 8349 RVA: 0x00068A89 File Offset: 0x00066C89
		[DefaultValue("")]
		[WebSysDescription("Page_ErrorPage")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ErrorPage
		{
			get
			{
				return this._errorPage;
			}
			set
			{
				this._errorPage = value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00068A92 File Offset: 0x00066C92
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsCallback
		{
			get
			{
				return this._isCallback;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x00007722 File Offset: 0x00005922
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00068A9C File Offset: 0x00066C9C
		protected internal virtual string UniqueFilePathSuffix
		{
			get
			{
				if (this._uniqueFilePathSuffix != null)
				{
					return this._uniqueFilePathSuffix;
				}
				long num = DateTime.Now.Ticks % 999983L;
				this._uniqueFilePathSuffix = Page.UniqueFilePathSuffixID + "=" + num.ToString("D6", CultureInfo.InvariantCulture);
				this._uniqueFilePathSuffix = this._uniqueFilePathSuffix.PadLeft(6, '0');
				return this._uniqueFilePathSuffix;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x00068B12 File Offset: 0x00066D12
		// (set) Token: 0x060020A2 RID: 8354 RVA: 0x00068B1A File Offset: 0x00066D1A
		public Control AutoPostBackControl
		{
			get
			{
				return this._autoPostBackControl;
			}
			set
			{
				this._autoPostBackControl = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x00068B24 File Offset: 0x00066D24
		internal bool ClientSupportsFocus
		{
			get
			{
				return this._request != null && (this._request.Browser.EcmaScriptVersion >= Page.FocusMinimumEcmaVersion || this._request.Browser.JScriptVersion >= Page.FocusMinimumJScriptVersion);
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00068B74 File Offset: 0x00066D74
		internal bool ClientSupportsJavaScript
		{
			get
			{
				if (!this._clientSupportsJavaScriptChecked)
				{
					this._clientSupportsJavaScript = (this._request != null && this._request.Browser.EcmaScriptVersion >= Page.JavascriptMinimumVersion);
					this._clientSupportsJavaScriptChecked = true;
				}
				return this._clientSupportsJavaScript;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x00068BC1 File Offset: 0x00066DC1
		private ArrayList EnabledControls
		{
			get
			{
				if (this._enabledControls == null)
				{
					this._enabledControls = new ArrayList();
				}
				return this._enabledControls;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x00068BDC File Offset: 0x00066DDC
		internal string FocusedControlID
		{
			get
			{
				if (this._focusedControlID == null)
				{
					return string.Empty;
				}
				return this._focusedControlID;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x00068BF2 File Offset: 0x00066DF2
		internal Control FocusedControl
		{
			get
			{
				return this._focusedControl;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00068BFA File Offset: 0x00066DFA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HtmlHead Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x00068C02 File Offset: 0x00066E02
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new virtual char IdSeparator
		{
			get
			{
				if (!this._haveIdSeparator)
				{
					if (base.AdapterInternal != null)
					{
						this._idSeparator = this.PageAdapter.IdSeparator;
					}
					else
					{
						this._idSeparator = base.IdSeparatorFromConfig;
					}
					this._haveIdSeparator = true;
				}
				return this._idSeparator;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00068C40 File Offset: 0x00066E40
		internal string LastFocusedControl
		{
			[AspNetHostingPermission(SecurityAction.Assert, Level = AspNetHostingPermissionLevel.Low)]
			get
			{
				if (this.RequestInternal != null)
				{
					string text = this.Request["__LASTFOCUS"];
					if (text != null)
					{
						return text;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x00068C70 File Offset: 0x00066E70
		// (set) Token: 0x060020AC RID: 8364 RVA: 0x00068CA1 File Offset: 0x00066EA1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool MaintainScrollPositionOnPostBack
		{
			get
			{
				return (this.RequestInternal == null || this.RequestInternal.Browser == null || this.RequestInternal.Browser.SupportsMaintainScrollPositionOnPostback) && this._maintainScrollPosition;
			}
			set
			{
				if (this._maintainScrollPosition != value)
				{
					this._maintainScrollPosition = value;
					if (this._maintainScrollPosition)
					{
						this.LoadScrollPosition();
					}
				}
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x00068CC1 File Offset: 0x00066EC1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("MasterPage_MasterPage")]
		public MasterPage Master
		{
			get
			{
				if (this._master == null && !this._preInitWorkComplete)
				{
					this._master = MasterPage.CreateMaster(this, this.Context, this._masterPageFile, this._contentTemplateCollection);
				}
				return this._master;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x00068CF7 File Offset: 0x00066EF7
		// (set) Token: 0x060020AF RID: 8367 RVA: 0x00068D04 File Offset: 0x00066F04
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("MasterPage_MasterPageFile")]
		public virtual string MasterPageFile
		{
			get
			{
				return VirtualPath.GetVirtualPathString(this._masterPageFile);
			}
			set
			{
				if (this._preInitWorkComplete)
				{
					throw new InvalidOperationException(SR.GetString("PropertySetBeforePageEvent", new object[]
					{
						"MasterPageFile",
						"Page_PreInit"
					}));
				}
				if (value != VirtualPath.GetVirtualPathString(this._masterPageFile))
				{
					this._masterPageFile = VirtualPath.CreateAllowNull(value);
					if (this._master != null && this.Controls.Contains(this._master))
					{
						this.Controls.Remove(this._master);
					}
					this._master = null;
				}
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x00068D91 File Offset: 0x00066F91
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x00068D9C File Offset: 0x00066F9C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int MaxPageStateFieldLength
		{
			get
			{
				return this._maxPageStateFieldLength;
			}
			set
			{
				if (base.ControlState > ControlState.FrameworkInitialized)
				{
					throw new InvalidOperationException(SR.GetString("PropertySetAfterFrameworkInitialize", new object[]
					{
						"MaxPageStateFieldLength"
					}));
				}
				if (value == 0 || value < -1)
				{
					throw new ArgumentException(SR.GetString("Page_Illegal_MaxPageStateFieldLength"), "MaxPageStateFieldLength");
				}
				this._maxPageStateFieldLength = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x00068DF3 File Offset: 0x00066FF3
		// (set) Token: 0x060020B3 RID: 8371 RVA: 0x00068DFB File Offset: 0x00066FFB
		internal bool ContainsCrossPagePost
		{
			get
			{
				return this._containsCrossPagePost;
			}
			set
			{
				this._containsCrossPagePost = value;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x00068E04 File Offset: 0x00067004
		internal bool RenderFocusScript
		{
			get
			{
				return this._requireFocusScript;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x00068E0C File Offset: 0x0006700C
		internal Stack PartialCachingControlStack
		{
			get
			{
				return this._partialCachingControlStack;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x00068E14 File Offset: 0x00067014
		protected virtual PageStatePersister PageStatePersister
		{
			get
			{
				if (this._persister == null)
				{
					PageAdapter pageAdapter = this.PageAdapter;
					if (pageAdapter != null)
					{
						this._persister = pageAdapter.GetStatePersister();
					}
					if (this._persister == null)
					{
						this._persister = new HiddenFieldPageStatePersister(this);
					}
				}
				return this._persister;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x00068E5C File Offset: 0x0006705C
		internal string RequestViewStateString
		{
			get
			{
				if (!this._cachedRequestViewState)
				{
					StringBuilder stringBuilder = new StringBuilder();
					try
					{
						NameValueCollection requestValueCollection = this.RequestValueCollection;
						if (requestValueCollection != null)
						{
							string text = this.RequestValueCollection["__VIEWSTATEFIELDCOUNT"];
							if (this.MaxPageStateFieldLength == -1 || text == null)
							{
								this._cachedRequestViewState = true;
								this._requestViewState = this.RequestValueCollection["__VIEWSTATE"];
								return this._requestViewState;
							}
							int num = Convert.ToInt32(text, CultureInfo.InvariantCulture);
							if (num < 0)
							{
								throw new HttpException(SR.GetString("ViewState_InvalidViewState"));
							}
							for (int i = 0; i < num; i++)
							{
								string text2 = "__VIEWSTATE";
								if (i > 0)
								{
									text2 += i.ToString(CultureInfo.InvariantCulture);
								}
								string text3 = this.RequestValueCollection[text2];
								if (text3 == null)
								{
									throw new HttpException(SR.GetString("ViewState_MissingViewStateField", new object[]
									{
										text2
									}));
								}
								stringBuilder.Append(text3);
							}
						}
						this._cachedRequestViewState = true;
						this._requestViewState = stringBuilder.ToString();
					}
					catch (Exception inner)
					{
						ViewStateException.ThrowViewStateError(inner, stringBuilder.ToString());
					}
				}
				return this._requestViewState;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x00068F98 File Offset: 0x00067198
		internal string ValidatorInvalidControl
		{
			get
			{
				if (this._validatorInvalidControl == null)
				{
					return string.Empty;
				}
				return this._validatorInvalidControl;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00068FAE File Offset: 0x000671AE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TraceContext Trace
		{
			get
			{
				return this.Context.Trace;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x00068FBB File Offset: 0x000671BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpRequest Request
		{
			get
			{
				if (this._request == null)
				{
					throw new HttpException(SR.GetString("Request_not_available"));
				}
				return this._request;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x00068FDB File Offset: 0x000671DB
		internal HttpRequest RequestInternal
		{
			get
			{
				return this._request;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x00068FE3 File Offset: 0x000671E3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpResponse Response
		{
			get
			{
				if (this._response == null)
				{
					throw new HttpException(SR.GetString("Response_not_available"));
				}
				return this._response;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x00069003 File Offset: 0x00067203
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public RouteData RouteData
		{
			get
			{
				if (this.Context != null && this.Context.Request != null)
				{
					return this.Context.Request.RequestContext.RouteData;
				}
				return null;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x00069031 File Offset: 0x00067231
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpServerUtility Server
		{
			get
			{
				return this.Context.Server;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0006903E File Offset: 0x0006723E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Cache Cache
		{
			get
			{
				if (this._cache == null)
				{
					throw new HttpException(SR.GetString("Cache_not_available"));
				}
				return this._cache;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x00069060 File Offset: 0x00067260
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual HttpSessionState Session
		{
			get
			{
				if (!this._sessionRetrieved)
				{
					this._sessionRetrieved = true;
					try
					{
						this._session = this.Context.Session;
					}
					catch
					{
					}
				}
				if (this._session == null)
				{
					throw new HttpException(SR.GetString("Session_not_enabled"));
				}
				return this._session;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x000690C0 File Offset: 0x000672C0
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x00069114 File Offset: 0x00067314
		[Bindable(true)]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Title
		{
			get
			{
				if (this.Page.Header == null && base.ControlState >= ControlState.ChildrenInitialized)
				{
					throw new InvalidOperationException(SR.GetString("Page_Title_Requires_Head"));
				}
				if (this._titleToBeSet != null)
				{
					return this._titleToBeSet;
				}
				return this.Page.Header.Title;
			}
			set
			{
				if (this.Page.Header != null)
				{
					this.Page.Header.Title = value;
					return;
				}
				if (base.ControlState >= ControlState.ChildrenInitialized)
				{
					throw new InvalidOperationException(SR.GetString("Page_Title_Requires_Head"));
				}
				this._titleToBeSet = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x00069160 File Offset: 0x00067360
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x000691B4 File Offset: 0x000673B4
		[Bindable(true)]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string MetaDescription
		{
			get
			{
				if (this.Page.Header == null && base.ControlState >= ControlState.ChildrenInitialized)
				{
					throw new InvalidOperationException(SR.GetString("Page_Description_Requires_Head"));
				}
				if (this._descriptionToBeSet != null)
				{
					return this._descriptionToBeSet;
				}
				return this.Page.Header.Description;
			}
			set
			{
				if (this.Page.Header != null)
				{
					this.Page.Header.Description = value;
					return;
				}
				if (base.ControlState >= ControlState.ChildrenInitialized)
				{
					throw new InvalidOperationException(SR.GetString("Page_Description_Requires_Head"));
				}
				this._descriptionToBeSet = value;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x00069200 File Offset: 0x00067400
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x00069254 File Offset: 0x00067454
		[Bindable(true)]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string MetaKeywords
		{
			get
			{
				if (this.Page.Header == null && base.ControlState >= ControlState.ChildrenInitialized)
				{
					throw new InvalidOperationException(SR.GetString("Page_Keywords_Requires_Head"));
				}
				if (this._keywordsToBeSet != null)
				{
					return this._keywordsToBeSet;
				}
				return this.Page.Header.Keywords;
			}
			set
			{
				if (this.Page.Header != null)
				{
					this.Page.Header.Keywords = value;
					return;
				}
				if (base.ControlState >= ControlState.ChildrenInitialized)
				{
					throw new InvalidOperationException(SR.GetString("Page_Keywords_Requires_Head"));
				}
				this._keywordsToBeSet = value;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x000692A0 File Offset: 0x000674A0
		internal bool ContainsTheme
		{
			get
			{
				return this._theme != null;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x000692AB File Offset: 0x000674AB
		// (set) Token: 0x060020C9 RID: 8393 RVA: 0x000692B4 File Offset: 0x000674B4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string Theme
		{
			get
			{
				return this._themeName;
			}
			set
			{
				if (this._preInitWorkComplete)
				{
					throw new InvalidOperationException(SR.GetString("PropertySetBeforePageEvent", new object[]
					{
						"Theme",
						"Page_PreInit"
					}));
				}
				if (!string.IsNullOrEmpty(value) && !FileUtil.IsValidDirectoryName(value))
				{
					throw new ArgumentException(SR.GetString("Page_theme_invalid_name", new object[]
					{
						value
					}), "Theme");
				}
				this._themeName = value;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x00069328 File Offset: 0x00067528
		internal bool SupportsStyleSheets
		{
			get
			{
				if (this._supportsStyleSheets != -1)
				{
					return this._supportsStyleSheets == 1;
				}
				if (this.Header != null && this.Header.StyleSheet != null && this.RequestInternal != null && this.Request.Browser != null && this.Request.Browser["preferredRenderingType"] != "xhtml-mp" && this.Request.Browser.SupportsCss && !this.Page.IsCallback && (this.ScriptManager == null || !this.ScriptManager.IsInAsyncPostBack))
				{
					this._supportsStyleSheets = 1;
					return true;
				}
				this._supportsStyleSheets = 0;
				return false;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x000693DE File Offset: 0x000675DE
		// (set) Token: 0x060020CC RID: 8396 RVA: 0x000693E6 File Offset: 0x000675E6
		[Browsable(false)]
		[Filterable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string StyleSheetTheme
		{
			get
			{
				return this._styleSheetName;
			}
			set
			{
				if (this._pageFlags[1])
				{
					throw new InvalidOperationException(SR.GetString("SetStyleSheetThemeCannotBeSet"));
				}
				this._styleSheetName = value;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x0006940D File Offset: 0x0006760D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPrincipal User
		{
			get
			{
				return this.Context.User;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x0006941A File Offset: 0x0006761A
		internal XhtmlConformanceMode XhtmlConformanceMode
		{
			get
			{
				if (!this._xhtmlConformanceModeSet)
				{
					if (base.DesignMode)
					{
						this._xhtmlConformanceMode = XhtmlConformanceMode.Transitional;
					}
					else
					{
						this._xhtmlConformanceMode = base.GetXhtmlConformanceSection().Mode;
					}
					this._xhtmlConformanceModeSet = true;
				}
				return this._xhtmlConformanceMode;
			}
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x00069454 File Offset: 0x00067654
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual HtmlTextWriter CreateHtmlTextWriter(TextWriter tw)
		{
			if (this.Context != null && this.Context.Request != null && this.Context.Request.Browser != null)
			{
				return this.Context.Request.Browser.CreateHtmlTextWriter(tw);
			}
			HtmlTextWriter htmlTextWriter = Page.CreateHtmlTextWriterInternal(tw, this._request);
			if (htmlTextWriter == null)
			{
				htmlTextWriter = new HtmlTextWriter(tw);
			}
			return htmlTextWriter;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000694B7 File Offset: 0x000676B7
		internal static HtmlTextWriter CreateHtmlTextWriterInternal(TextWriter tw, HttpRequest request)
		{
			if (request != null && request.Browser != null)
			{
				return request.Browser.CreateHtmlTextWriterInternal(tw);
			}
			return new Html32TextWriter(tw);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000694D8 File Offset: 0x000676D8
		public static HtmlTextWriter CreateHtmlTextWriterFromType(TextWriter tw, Type writerType)
		{
			if (writerType == typeof(HtmlTextWriter))
			{
				return new HtmlTextWriter(tw);
			}
			if (writerType == typeof(Html32TextWriter))
			{
				return new Html32TextWriter(tw);
			}
			HtmlTextWriter result;
			try
			{
				Util.CheckAssignableType(typeof(HtmlTextWriter), writerType);
				result = (HtmlTextWriter)HttpRuntime.CreateNonPublicInstance(writerType, new object[]
				{
					tw
				});
			}
			catch
			{
				throw new HttpException(SR.GetString("Invalid_HtmlTextWriter", new object[]
				{
					writerType.FullName
				}));
			}
			return result;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x00069574 File Offset: 0x00067774
		public override Control FindControl(string id)
		{
			if (StringUtil.EqualsIgnoreCase(id, "__Page"))
			{
				return this;
			}
			return base.FindControl(id, 0);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00007722 File Offset: 0x00005922
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetTypeHashCode()
		{
			return 0;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0006958D File Offset: 0x0006778D
		internal override string GetUniqueIDPrefix()
		{
			if (this.Parent == null)
			{
				return string.Empty;
			}
			return base.GetUniqueIDPrefix();
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000695A4 File Offset: 0x000677A4
		internal uint GetClientStateIdentifier()
		{
			int nonRandomizedHashCode = StringUtil.GetNonRandomizedHashCode(this.TemplateSourceDirectory, true);
			return (uint)(nonRandomizedHashCode + StringUtil.GetNonRandomizedHashCode(base.GetType().Name, true));
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000695D4 File Offset: 0x000677D4
		private bool HandleError(Exception e)
		{
			try
			{
				this.Context.TempError = e;
				this.OnError(EventArgs.Empty);
				if (this.Context.TempError == null)
				{
					return true;
				}
			}
			finally
			{
				this.Context.TempError = null;
			}
			if (!string.IsNullOrEmpty(this._errorPage) && this.Context.IsCustomErrorEnabled)
			{
				this._response.RedirectToErrorPage(this._errorPage, CustomErrorsSection.GetSettings(this.Context).RedirectMode);
				return true;
			}
			PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_UNHANDLED);
			string postMessage = null;
			if (this.Context.TraceIsEnabled)
			{
				this.Trace.Warn(SR.GetString("Unhandled_Err_Error"), null, e);
				if (this.Trace.PageOutput)
				{
					StringWriter stringWriter = new StringWriter();
					HtmlTextWriter output = new HtmlTextWriter(stringWriter);
					this.BuildPageProfileTree(false);
					this.Trace.EndRequest();
					this.Trace.StopTracing();
					this.Trace.StatusCode = 500;
					this.Trace.Render(output);
					postMessage = stringWriter.ToString();
				}
			}
			if (HttpException.GetErrorFormatter(e) != null)
			{
				return false;
			}
			if (e is SecurityException)
			{
				return false;
			}
			throw new HttpUnhandledException(null, postMessage, e);
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x00069710 File Offset: 0x00067910
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsCrossPagePostBack
		{
			get
			{
				return this._isCrossPagePostBack;
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x00069718 File Offset: 0x00067918
		internal bool IsExportingWebPart
		{
			get
			{
				return this._pageFlags[2];
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x00069726 File Offset: 0x00067926
		internal bool IsExportingWebPartShared
		{
			get
			{
				return this._pageFlags[4];
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x00069734 File Offset: 0x00067934
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPostBack
		{
			get
			{
				return this._requestValueCollection != null && (this._isCrossPagePostBack || (!this._pageFlags[8] && !this.ViewStateMacValidationErrorWasSuppressed && (this.Context.ServerExecuteDepth <= 0 || (this.Context.Handler != null && !(base.GetType() != this.Context.Handler.GetType()))) && !this._fPageLayoutChanged));
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x000697B2 File Offset: 0x000679B2
		internal NameValueCollection RequestValueCollection
		{
			get
			{
				return this._requestValueCollection;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000697BA File Offset: 0x000679BA
		// (set) Token: 0x060020DD RID: 8413 RVA: 0x000697C2 File Offset: 0x000679C2
		[Browsable(false)]
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool EnableEventValidation
		{
			get
			{
				return this._enableEventValidation;
			}
			set
			{
				if (base.ControlState > ControlState.FrameworkInitialized)
				{
					throw new InvalidOperationException(SR.GetString("PropertySetAfterFrameworkInitialize", new object[]
					{
						"EnableEventValidation"
					}));
				}
				this._enableEventValidation = value;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x000697F2 File Offset: 0x000679F2
		// (set) Token: 0x060020DF RID: 8415 RVA: 0x000697FA File Offset: 0x000679FA
		[Browsable(false)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				base.EnableViewState = value;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00069803 File Offset: 0x00067A03
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x0006980C File Offset: 0x00067A0C
		[Browsable(false)]
		[DefaultValue(ViewStateEncryptionMode.Auto)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ViewStateEncryptionMode ViewStateEncryptionMode
		{
			get
			{
				return this._encryptionMode;
			}
			set
			{
				if (base.ControlState > ControlState.FrameworkInitialized)
				{
					throw new InvalidOperationException(SR.GetString("PropertySetAfterFrameworkInitialize", new object[]
					{
						"ViewStateEncryptionMode"
					}));
				}
				if (value < ViewStateEncryptionMode.Auto || value > ViewStateEncryptionMode.Never)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._encryptionMode = value;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x0006985A File Offset: 0x00067A5A
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x00069862 File Offset: 0x00067A62
		[Browsable(false)]
		public string ViewStateUserKey
		{
			get
			{
				return this._viewStateUserKey;
			}
			set
			{
				if (base.ControlState >= ControlState.Initialized)
				{
					throw new HttpException(SR.GetString("Too_late_for_ViewStateUserKey"));
				}
				this._viewStateUserKey = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x00069884 File Offset: 0x00067A84
		// (set) Token: 0x060020E5 RID: 8421 RVA: 0x0006988C File Offset: 0x00067A8C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x00069895 File Offset: 0x00067A95
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x0006989D File Offset: 0x00067A9D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ValidateRequestMode.Enabled)]
		public override ValidateRequestMode ValidateRequestMode
		{
			get
			{
				return base.ValidateRequestMode;
			}
			set
			{
				base.ValidateRequestMode = value;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060020E8 RID: 8424 RVA: 0x000698A6 File Offset: 0x00067AA6
		// (set) Token: 0x060020E9 RID: 8425 RVA: 0x000698B5 File Offset: 0x00067AB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public bool SkipFormActionValidation
		{
			get
			{
				return this._pageFlags[64];
			}
			set
			{
				if (value != this.SkipFormActionValidation)
				{
					this._clientQueryString = null;
				}
				this._pageFlags[64] = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x000698D5 File Offset: 0x00067AD5
		// (set) Token: 0x060020EB RID: 8427 RVA: 0x000698DD File Offset: 0x00067ADD
		[Browsable(false)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000698E8 File Offset: 0x00067AE8
		internal static string DecryptString(string s, Purpose purpose)
		{
			if (s == null)
			{
				return null;
			}
			byte[] array = HttpServerUtility.UrlTokenDecode(s);
			byte[] array2 = null;
			if (array != null)
			{
				if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider)
				{
					ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(purpose, CryptoServiceOptions.CacheableOutput);
					array2 = cryptoService.Unprotect(array);
				}
				else
				{
					array2 = MachineKeySection.EncryptOrDecryptData(false, array, null, 0, array.Length, false, false, IVType.Hash);
				}
			}
			if (array2 == null)
			{
				throw new HttpException(SR.GetString("ViewState_InvalidViewState"));
			}
			return Encoding.UTF8.GetString(array2);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x00069957 File Offset: 0x00067B57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DesignerInitialize()
		{
			this.InitRecursive(null);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00069960 File Offset: 0x00067B60
		internal NameValueCollection GetCollectionBasedOnMethod(bool dontReturnNull)
		{
			if (this._request.HttpVerb == HttpVerb.POST)
			{
				if (!dontReturnNull && !this._request.HasForm)
				{
					return null;
				}
				return this._request.Form;
			}
			else
			{
				if (!dontReturnNull && !this._request.HasQueryString)
				{
					return null;
				}
				return this._request.QueryString;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000699B8 File Offset: 0x00067BB8
		private bool DetermineIsExportingWebPart()
		{
			byte[] queryStringBytes = this.Request.QueryStringBytes;
			if (queryStringBytes == null || queryStringBytes.Length < 28)
			{
				return false;
			}
			if (queryStringBytes[0] != 95 || queryStringBytes[1] != 95 || queryStringBytes[2] != 87 || queryStringBytes[3] != 69 || queryStringBytes[4] != 66 || queryStringBytes[5] != 80 || queryStringBytes[6] != 65 || queryStringBytes[7] != 82 || queryStringBytes[8] != 84 || queryStringBytes[9] != 69 || queryStringBytes[10] != 88 || queryStringBytes[11] != 80 || queryStringBytes[12] != 79 || queryStringBytes[13] != 82 || queryStringBytes[14] != 84 || queryStringBytes[15] != 61 || queryStringBytes[16] != 116 || queryStringBytes[17] != 114 || queryStringBytes[18] != 117 || queryStringBytes[19] != 101 || queryStringBytes[20] != 38)
			{
				return false;
			}
			this._pageFlags.Set(2);
			return true;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x00069A98 File Offset: 0x00067C98
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual NameValueCollection DeterminePostBackMode()
		{
			if (this.Context.Request == null)
			{
				return null;
			}
			if (this.Context.PreventPostback)
			{
				return null;
			}
			NameValueCollection nameValueCollection = this.GetCollectionBasedOnMethod(false);
			if (nameValueCollection == null)
			{
				return null;
			}
			bool flag = false;
			string[] values = nameValueCollection.GetValues(null);
			if (values != null)
			{
				int num = values.Length;
				for (int i = 0; i < num; i++)
				{
					if (values[i].StartsWith("__VIEWSTATE", StringComparison.Ordinal) || values[i] == "__EVENTTARGET")
					{
						flag = true;
						break;
					}
				}
			}
			if (nameValueCollection["__VIEWSTATE"] == null && nameValueCollection["__VIEWSTATEFIELDCOUNT"] == null && nameValueCollection["__EVENTTARGET"] == null && !flag)
			{
				nameValueCollection = null;
			}
			else if (this.Request.QueryStringText.IndexOf(HttpResponse.RedirectQueryStringAssignment, StringComparison.Ordinal) != -1)
			{
				nameValueCollection = null;
			}
			return nameValueCollection;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x00069B60 File Offset: 0x00067D60
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual NameValueCollection DeterminePostBackModeUnvalidated()
		{
			if (this._request.HttpVerb != HttpVerb.POST)
			{
				return this._request.Unvalidated.QueryString;
			}
			return this._request.Unvalidated.Form;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00069B94 File Offset: 0x00067D94
		internal static string EncryptString(string s, Purpose purpose)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] input;
			if (AspNetCryptoServiceProvider.Instance.IsDefaultProvider)
			{
				ICryptoService cryptoService = AspNetCryptoServiceProvider.Instance.GetCryptoService(purpose, CryptoServiceOptions.CacheableOutput);
				input = cryptoService.Protect(bytes);
			}
			else
			{
				input = MachineKeySection.EncryptOrDecryptData(true, bytes, null, 0, bytes.Length, false, false, IVType.Hash);
			}
			return HttpServerUtility.UrlTokenEncode(input);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00069BE8 File Offset: 0x00067DE8
		private void LoadAllState()
		{
			object obj = this.LoadPageStateFromPersistenceMedium();
			IDictionary dictionary = null;
			Pair pair = null;
			Pair pair2 = obj as Pair;
			if (obj != null)
			{
				dictionary = (pair2.First as IDictionary);
				pair = (pair2.Second as Pair);
			}
			if (dictionary != null)
			{
				this._controlsRequiringPostBack = (ArrayList)dictionary["__ControlsRequirePostBackKey__"];
				if (this._registeredControlsRequiringControlState != null)
				{
					foreach (object obj2 in ((IEnumerable)this._registeredControlsRequiringControlState))
					{
						Control control = (Control)obj2;
						control.LoadControlStateInternal(dictionary[control.UniqueID]);
					}
				}
			}
			if (pair != null)
			{
				string s = (string)pair.First;
				int num = int.Parse(s, NumberFormatInfo.InvariantInfo);
				this._fPageLayoutChanged = (num != this.GetTypeHashCode());
				if (!this._fPageLayoutChanged)
				{
					base.LoadViewStateRecursive(pair.Second);
				}
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00069CEC File Offset: 0x00067EEC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual object LoadPageStateFromPersistenceMedium()
		{
			PageStatePersister pageStatePersister = this.PageStatePersister;
			try
			{
				pageStatePersister.Load();
			}
			catch (HttpException ex)
			{
				if (this._pageFlags[8])
				{
					return null;
				}
				if (this.ShouldSuppressMacValidationException(ex))
				{
					if (this.Context != null && this.Context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "Ignoring page state", ex);
					}
					this.ViewStateMacValidationErrorWasSuppressed = true;
					return null;
				}
				ex.WebEventCode = 3002;
				throw;
			}
			return new Pair(pageStatePersister.ControlState, pageStatePersister.ViewState);
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00069D8C File Offset: 0x00067F8C
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x00069D9E File Offset: 0x00067F9E
		private bool ViewStateMacValidationErrorWasSuppressed
		{
			get
			{
				return this._pageFlags[128];
			}
			set
			{
				this._pageFlags[128] = value;
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00069DB4 File Offset: 0x00067FB4
		internal bool ShouldSuppressMacValidationException(Exception e)
		{
			if (!EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsFromCrossPagePostbacks)
			{
				return false;
			}
			if (ViewStateException.IsMacValidationException(e))
			{
				if (EnableViewStateMacRegistryHelper.SuppressMacValidationErrorsAlways)
				{
					return true;
				}
				if (!string.IsNullOrEmpty(this.ViewStateUserKey))
				{
					return false;
				}
				if (this._requestValueCollection == null)
				{
					return true;
				}
				if (!this.VerifyClientStateIdentifier(this._requestValueCollection["__VIEWSTATEGENERATOR"]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00069E10 File Offset: 0x00068010
		private bool VerifyClientStateIdentifier(string identifier)
		{
			uint num;
			return identifier != null && uint.TryParse(identifier, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num) && num == this.GetClientStateIdentifier();
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x00069E40 File Offset: 0x00068040
		internal void LoadScrollPosition()
		{
			if (this._previousPagePath != null)
			{
				return;
			}
			if (this._requestValueCollection != null)
			{
				string text = this._requestValueCollection["__SCROLLPOSITIONX"];
				if (text != null)
				{
					double num;
					this._scrollPositionX = (HttpUtility.TryParseCoordinates(text, out num) ? ((int)num) : 0);
				}
				string text2 = this._requestValueCollection["__SCROLLPOSITIONY"];
				if (text2 != null)
				{
					double num;
					this._scrollPositionY = (HttpUtility.TryParseCoordinates(text2, out num) ? ((int)num) : 0);
				}
			}
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00069EB6 File Offset: 0x000680B6
		internal IStateFormatter2 CreateStateFormatter()
		{
			return new ObjectStateFormatter(this, true);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00069EC0 File Offset: 0x000680C0
		internal ICollection DecomposeViewStateIntoChunks()
		{
			string clientState = this.ClientState;
			if (clientState == null)
			{
				return null;
			}
			if (this.MaxPageStateFieldLength <= 0)
			{
				return new ArrayList(1)
				{
					clientState
				};
			}
			int num = this.ClientState.Length / this.MaxPageStateFieldLength;
			ArrayList arrayList = new ArrayList(num + 1);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				arrayList.Add(clientState.Substring(num2, this.MaxPageStateFieldLength));
				num2 += this.MaxPageStateFieldLength;
			}
			if (num2 < clientState.Length)
			{
				arrayList.Add(clientState.Substring(num2));
			}
			if (arrayList.Count == 0)
			{
				arrayList.Add(string.Empty);
			}
			return arrayList;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x00069F70 File Offset: 0x00068170
		internal void RenderViewStateFields(HtmlTextWriter writer)
		{
			if (this._hiddenFieldsToRender == null)
			{
				this._hiddenFieldsToRender = new Dictionary<string, string>();
			}
			if (this.ClientState != null)
			{
				ICollection collection = this.DecomposeViewStateIntoChunks();
				writer.WriteLine();
				if (collection.Count > 1)
				{
					string value = collection.Count.ToString(CultureInfo.InvariantCulture);
					writer.Write("<input type=\"hidden\" name=\"");
					writer.Write("__VIEWSTATEFIELDCOUNT");
					writer.Write("\" id=\"");
					writer.Write("__VIEWSTATEFIELDCOUNT");
					writer.Write("\" value=\"");
					writer.Write(value);
					writer.WriteLine("\" />");
					this._hiddenFieldsToRender["__VIEWSTATEFIELDCOUNT"] = value;
				}
				int num = 0;
				foreach (object obj in collection)
				{
					string value2 = (string)obj;
					writer.Write("<input type=\"hidden\" name=\"");
					string text = "__VIEWSTATE";
					writer.Write("__VIEWSTATE");
					if (num > 0)
					{
						string text2 = num.ToString(CultureInfo.InvariantCulture);
						text += text2;
						writer.Write(text2);
					}
					writer.Write("\" id=\"");
					writer.Write(text);
					writer.Write("\" value=\"");
					writer.Write(value2);
					writer.WriteLine("\" />");
					num++;
					this._hiddenFieldsToRender[text] = value2;
				}
				if (EnableViewStateMacRegistryHelper.WriteViewStateGeneratorField)
				{
					this.ClientScript.RegisterHiddenField("__VIEWSTATEGENERATOR", this.GetClientStateIdentifier().ToString("X8", CultureInfo.InvariantCulture));
					return;
				}
			}
			else
			{
				writer.Write("\r\n<input type=\"hidden\" name=\"");
				writer.Write("__VIEWSTATE");
				writer.Write("\" id=\"");
				writer.Write("__VIEWSTATE");
				writer.WriteLine("\" value=\"\" />");
				this._hiddenFieldsToRender["__VIEWSTATE"] = string.Empty;
			}
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0006A178 File Offset: 0x00068378
		internal void BeginFormRender(HtmlTextWriter writer, string formUniqueID)
		{
			bool flag = this.RenderDivAroundHiddenInputs(writer);
			if (flag)
			{
				writer.WriteLine();
				if (this.RenderingCompatibility >= VersionUtil.Framework40)
				{
					writer.Write("<div class=\"aspNetHidden\">");
				}
				else
				{
					writer.Write("<div>");
				}
			}
			this.ClientScript.RenderHiddenFields(writer);
			this.RenderViewStateFields(writer);
			if (flag)
			{
				writer.WriteLine("</div>");
			}
			if (this.ClientSupportsJavaScript)
			{
				if (this.MaintainScrollPositionOnPostBack && !this._requireScrollScript)
				{
					this.ClientScript.RegisterHiddenField("__SCROLLPOSITIONX", this._scrollPositionX.ToString(CultureInfo.InvariantCulture));
					this.ClientScript.RegisterHiddenField("__SCROLLPOSITIONY", this._scrollPositionY.ToString(CultureInfo.InvariantCulture));
					this.ClientScript.RegisterStartupScript(typeof(Page), "PageScrollPositionScript", "\r\ntheForm.oldSubmit = theForm.submit;\r\ntheForm.submit = WebForm_SaveScrollPositionSubmit;\r\n\r\ntheForm.oldOnSubmit = theForm.onsubmit;\r\ntheForm.onsubmit = WebForm_SaveScrollPositionOnSubmit;\r\n" + (this.IsPostBack ? "\r\ntheForm.oldOnLoad = window.onload;\r\nwindow.onload = WebForm_RestoreScrollPosition;\r\n" : string.Empty), true);
					this.RegisterWebFormsScript();
					this._requireScrollScript = true;
				}
				if (this.ClientSupportsFocus && this.Form != null && (this.RenderFocusScript || this.Form.DefaultFocus.Length > 0 || this.Form.DefaultButton.Length > 0))
				{
					string text = string.Empty;
					if (this.FocusedControlID.Length > 0)
					{
						text = this.FocusedControlID;
					}
					else if (this.FocusedControl != null)
					{
						if (this.FocusedControl.Visible)
						{
							text = this.FocusedControl.ClientID;
						}
					}
					else if (this.ValidatorInvalidControl.Length > 0)
					{
						text = this.ValidatorInvalidControl;
					}
					else if (this.LastFocusedControl.Length > 0)
					{
						text = this.LastFocusedControl;
					}
					else if (this.Form.DefaultFocus.Length > 0)
					{
						text = this.Form.DefaultFocus;
					}
					else if (this.Form.DefaultButton.Length > 0)
					{
						text = this.Form.DefaultButton;
					}
					int num;
					if (text.Length > 0 && !CrossSiteScriptingValidation.IsDangerousString(text, out num) && CrossSiteScriptingValidation.IsValidJavascriptId(text))
					{
						this.ClientScript.RegisterClientScriptResource(typeof(HtmlForm), "Focus.js");
						if (!this.ClientScript.IsClientScriptBlockRegistered(typeof(HtmlForm), "Focus"))
						{
							this.RegisterWebFormsScript();
							this.ClientScript.RegisterStartupScript(typeof(HtmlForm), "Focus", "WebForm_AutoFocus('" + Util.QuoteJScriptString(text) + "');", true);
						}
						IScriptManager scriptManager = this.ScriptManager;
						if (scriptManager != null)
						{
							scriptManager.SetFocusInternal(text);
						}
					}
				}
				if (this.RenderDisabledControlsScript)
				{
					this.ClientScript.RegisterOnSubmitStatement(typeof(Page), "PageReEnableControlsScript", "WebForm_ReEnableControls();");
					this.RegisterWebFormsScript();
				}
				if (this._fRequirePostBackScript)
				{
					this.RenderPostBackScript(writer, formUniqueID);
				}
				if (this._fRequireWebFormsScript)
				{
					this.RenderWebFormsScript(writer);
				}
			}
			this.ClientScript.RenderClientScriptBlocks(writer);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0006A478 File Offset: 0x00068678
		internal void EndFormRenderArrayAndExpandoAttribute(HtmlTextWriter writer, string formUniqueID)
		{
			if (this.ClientSupportsJavaScript)
			{
				if (this.RenderDisabledControlsScript)
				{
					foreach (object obj in this.EnabledControls)
					{
						Control control = (Control)obj;
						this.ClientScript.RegisterArrayDeclaration("__enabledControlArray", "'" + control.ClientID + "'");
					}
				}
				this.ClientScript.RenderArrayDeclares(writer);
				this.ClientScript.RenderExpandoAttribute(writer);
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x0006A518 File Offset: 0x00068718
		private bool RenderDisabledControlsScript
		{
			get
			{
				return this.Form.SubmitDisabledControls && this.EnabledControls.Count > 0 && this._request.Browser.W3CDomVersion.Major > 0;
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0006A550 File Offset: 0x00068750
		internal void EndFormRenderHiddenFields(HtmlTextWriter writer, string formUniqueID)
		{
			if (this.RequiresViewStateEncryptionInternal)
			{
				this.ClientScript.RegisterHiddenField("__VIEWSTATEENCRYPTED", string.Empty);
			}
			if (this._containsCrossPagePost)
			{
				string hiddenFieldInitialValue = Page.EncryptString(this.Request.CurrentExecutionFilePath, Purpose.WebForms_Page_PreviousPageID);
				this.ClientScript.RegisterHiddenField("__PREVIOUSPAGE", hiddenFieldInitialValue);
			}
			if (this.EnableEventValidation)
			{
				this.ClientScript.SaveEventValidationField();
			}
			if (this.ClientScript.HasRegisteredHiddenFields)
			{
				bool flag = this.RenderDivAroundHiddenInputs(writer);
				if (flag)
				{
					writer.WriteLine();
					if (this.RenderingCompatibility >= VersionUtil.Framework40)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "aspNetHidden");
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
				}
				this.ClientScript.RenderHiddenFields(writer);
				if (flag)
				{
					writer.RenderEndTag();
				}
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0006A618 File Offset: 0x00068818
		internal void EndFormRenderPostBackAndWebFormsScript(HtmlTextWriter writer, string formUniqueID)
		{
			if (this.ClientSupportsJavaScript)
			{
				if (this._fRequirePostBackScript && !this._fPostBackScriptRendered)
				{
					this.RenderPostBackScript(writer, formUniqueID);
				}
				if (this._fRequireWebFormsScript && !this._fWebFormsScriptRendered)
				{
					this.RenderWebFormsScript(writer);
				}
			}
			this.ClientScript.RenderClientStartupScripts(writer);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0006A668 File Offset: 0x00068868
		internal void EndFormRender(HtmlTextWriter writer, string formUniqueID)
		{
			this.EndFormRenderArrayAndExpandoAttribute(writer, formUniqueID);
			this.EndFormRenderHiddenFields(writer, formUniqueID);
			this.EndFormRenderPostBackAndWebFormsScript(writer, formUniqueID);
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x0006A682 File Offset: 0x00068882
		internal bool IsInOnFormRender
		{
			get
			{
				return this._inOnFormRender;
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0006A68A File Offset: 0x0006888A
		internal void OnFormRender()
		{
			if (this._fOnFormRenderCalled)
			{
				throw new HttpException(SR.GetString("Multiple_forms_not_allowed"));
			}
			this._fOnFormRenderCalled = true;
			this._inOnFormRender = true;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0006A6B2 File Offset: 0x000688B2
		internal void OnFormPostRender(HtmlTextWriter writer)
		{
			this._inOnFormRender = false;
			if (this._postFormRenderDelegate != null)
			{
				this._postFormRenderDelegate(writer, null);
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0006A6D0 File Offset: 0x000688D0
		internal void ResetOnFormRenderCalled()
		{
			this._fOnFormRenderCalled = false;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0006A6DC File Offset: 0x000688DC
		public void SetFocus(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (this.Form == null)
			{
				throw new InvalidOperationException(SR.GetString("Form_Required_For_Focus"));
			}
			if (this.Form.ControlState == ControlState.PreRendered)
			{
				throw new InvalidOperationException(SR.GetString("Page_MustCallBeforeAndDuringPreRender", new object[]
				{
					"SetFocus"
				}));
			}
			this._focusedControl = control;
			this._focusedControlID = null;
			this.RegisterFocusScript();
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0006A750 File Offset: 0x00068950
		public void SetFocus(string clientID)
		{
			if (clientID == null || clientID.Trim().Length == 0)
			{
				throw new ArgumentNullException("clientID");
			}
			if (this.Form == null)
			{
				throw new InvalidOperationException(SR.GetString("Form_Required_For_Focus"));
			}
			if (this.Form.ControlState == ControlState.PreRendered)
			{
				throw new InvalidOperationException(SR.GetString("Page_MustCallBeforeAndDuringPreRender", new object[]
				{
					"SetFocus"
				}));
			}
			this._focusedControlID = clientID.Trim();
			this._focusedControl = null;
			this.RegisterFocusScript();
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0006A7D5 File Offset: 0x000689D5
		internal void SetValidatorInvalidControlFocus(string clientID)
		{
			if (string.IsNullOrEmpty(this._validatorInvalidControl))
			{
				this._validatorInvalidControl = clientID;
				this.RegisterFocusScript();
			}
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0006A7F1 File Offset: 0x000689F1
		[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
		internal static void ThreadResetAbortWithAssert()
		{
			Thread.ResetAbort();
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0006A7F8 File Offset: 0x000689F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
		public string GetPostBackEventReference(Control control)
		{
			return this.ClientScript.GetPostBackEventReference(control, string.Empty);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0006A80B File Offset: 0x00068A0B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
		public string GetPostBackEventReference(Control control, string argument)
		{
			return this.ClientScript.GetPostBackEventReference(control, argument);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0006A80B File Offset: 0x00068A0B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.GetPostBackEventReference. http://go.microsoft.com/fwlink/?linkid=14202")]
		public string GetPostBackClientEvent(Control control, string argument)
		{
			return this.ClientScript.GetPostBackEventReference(control, argument);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0006A81A File Offset: 0x00068A1A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.GetPostBackClientHyperlink. http://go.microsoft.com/fwlink/?linkid=14202")]
		public string GetPostBackClientHyperlink(Control control, string argument)
		{
			return this.ClientScript.GetPostBackClientHyperlink(control, argument, false);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0006A82C File Offset: 0x00068A2C
		internal void InitializeStyleSheet()
		{
			if (this._pageFlags[1])
			{
				return;
			}
			string styleSheetTheme = this.StyleSheetTheme;
			if (!string.IsNullOrEmpty(styleSheetTheme))
			{
				BuildResultCompiledType themeBuildResultType = ThemeDirectoryCompiler.GetThemeBuildResultType(this.Context, styleSheetTheme);
				if (themeBuildResultType == null)
				{
					throw new HttpException(SR.GetString("Page_theme_not_found", new object[]
					{
						styleSheetTheme
					}));
				}
				this._styleSheet = (PageTheme)themeBuildResultType.CreateInstance();
				this._styleSheet.Initialize(this, true);
			}
			this._pageFlags.Set(1);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0006A8B0 File Offset: 0x00068AB0
		private void InitializeThemes()
		{
			string theme = this.Theme;
			if (string.IsNullOrEmpty(theme))
			{
				return;
			}
			BuildResultCompiledType themeBuildResultType = ThemeDirectoryCompiler.GetThemeBuildResultType(this.Context, theme);
			if (themeBuildResultType != null)
			{
				this._theme = (PageTheme)themeBuildResultType.CreateInstance();
				this._theme.Initialize(this, false);
				return;
			}
			throw new HttpException(SR.GetString("Page_theme_not_found", new object[]
			{
				theme
			}));
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0006A918 File Offset: 0x00068B18
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal void AddContentTemplate(string templateName, ITemplate template)
		{
			if (this._contentTemplateCollection == null)
			{
				this._contentTemplateCollection = new Hashtable(11, StringComparer.OrdinalIgnoreCase);
			}
			try
			{
				this._contentTemplateCollection.Add(templateName, template);
			}
			catch (ArgumentException)
			{
				throw new HttpException(SR.GetString("MasterPage_Multiple_content", new object[]
				{
					templateName
				}));
			}
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0006A97C File Offset: 0x00068B7C
		private void ApplyMasterPage()
		{
			if (this.Master != null)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this._masterPageFile.VirtualPathString.ToLower(CultureInfo.InvariantCulture));
				MasterPage.ApplyMasterRecursive(this.Master, arrayList);
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0006A9BF File Offset: 0x00068BBF
		internal void ApplyControlSkin(Control ctrl)
		{
			if (this._theme != null)
			{
				this._theme.ApplyControlSkin(ctrl);
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0006A9D5 File Offset: 0x00068BD5
		internal bool ApplyControlStyleSheet(Control ctrl)
		{
			if (this._styleSheet != null)
			{
				this._styleSheet.ApplyControlSkin(ctrl);
				return true;
			}
			return false;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0006A9F0 File Offset: 0x00068BF0
		internal void RegisterFocusScript()
		{
			if (this.ClientSupportsFocus && !this._requireFocusScript)
			{
				this.ClientScript.RegisterHiddenField("__LASTFOCUS", string.Empty);
				this._requireFocusScript = true;
				if (this._partialCachingControlStack != null)
				{
					foreach (object obj in this._partialCachingControlStack)
					{
						BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
						basePartialCachingControl.RegisterFocusScript();
					}
				}
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0006AA7C File Offset: 0x00068C7C
		internal void RegisterPostBackScript()
		{
			if (!this.ClientSupportsJavaScript)
			{
				return;
			}
			if (this._fPostBackScriptRendered)
			{
				return;
			}
			if (!this._fRequirePostBackScript)
			{
				this.ClientScript.RegisterHiddenField("__EVENTTARGET", string.Empty);
				this.ClientScript.RegisterHiddenField("__EVENTARGUMENT", string.Empty);
				this._fRequirePostBackScript = true;
			}
			if (this._partialCachingControlStack != null)
			{
				foreach (object obj in this._partialCachingControlStack)
				{
					BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
					basePartialCachingControl.RegisterPostBackScript();
				}
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0006AB28 File Offset: 0x00068D28
		private void RenderPostBackScript(HtmlTextWriter writer, string formUniqueID)
		{
			writer.Write(base.EnableLegacyRendering ? "\r\n<script type=\"text/javascript\">\r\n<!--\r\n" : "\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n");
			if (this.PageAdapter != null)
			{
				writer.Write("var theForm = ");
				writer.Write(this.PageAdapter.GetPostBackFormReference(formUniqueID));
				writer.WriteLine(";");
			}
			else
			{
				writer.Write("var theForm = document.forms['");
				writer.Write(formUniqueID);
				writer.WriteLine("'];");
				writer.Write("if (!theForm) {\r\n    theForm = document.");
				writer.Write(formUniqueID);
				writer.WriteLine(";\r\n}");
			}
			writer.WriteLine("function __doPostBack(eventTarget, eventArgument) {\r\n    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {\r\n        theForm.__EVENTTARGET.value = eventTarget;\r\n        theForm.__EVENTARGUMENT.value = eventArgument;\r\n        theForm.submit();\r\n    }\r\n}");
			writer.WriteLine(base.EnableLegacyRendering ? "// -->\r\n</script>\r\n" : "//]]>\r\n</script>\r\n");
			this._fPostBackScriptRendered = true;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0006ABE8 File Offset: 0x00068DE8
		internal void RegisterWebFormsScript()
		{
			if (this.ClientSupportsJavaScript)
			{
				if (this._fWebFormsScriptRendered)
				{
					return;
				}
				this.RegisterPostBackScript();
				this._fRequireWebFormsScript = true;
				if (this._partialCachingControlStack != null)
				{
					foreach (object obj in this._partialCachingControlStack)
					{
						BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
						basePartialCachingControl.RegisterWebFormsScript();
					}
				}
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0006AC68 File Offset: 0x00068E68
		private void RenderWebFormsScript(HtmlTextWriter writer)
		{
			this.ClientScript.RenderWebFormsScript(writer);
			this._fWebFormsScriptRendered = true;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0006AC7D File Offset: 0x00068E7D
		[Obsolete("The recommended alternative is ClientScript.IsClientScriptBlockRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool IsClientScriptBlockRegistered(string key)
		{
			return this.ClientScript.IsClientScriptBlockRegistered(typeof(Page), key);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0006AC95 File Offset: 0x00068E95
		[Obsolete("The recommended alternative is ClientScript.IsStartupScriptRegistered(string key). http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool IsStartupScriptRegistered(string key)
		{
			return this.ClientScript.IsStartupScriptRegistered(typeof(Page), key);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0006ACAD File Offset: 0x00068EAD
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.RegisterArrayDeclaration(string arrayName, string arrayValue). http://go.microsoft.com/fwlink/?linkid=14202")]
		public void RegisterArrayDeclaration(string arrayName, string arrayValue)
		{
			this.ClientScript.RegisterArrayDeclaration(arrayName, arrayValue);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0006ACBC File Offset: 0x00068EBC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Obsolete("The recommended alternative is ClientScript.RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue). http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
		{
			this.ClientScript.RegisterHiddenField(hiddenFieldName, hiddenFieldInitialValue);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0006ACCB File Offset: 0x00068ECB
		[Obsolete("The recommended alternative is ClientScript.RegisterClientScriptBlock(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void RegisterClientScriptBlock(string key, string script)
		{
			this.ClientScript.RegisterClientScriptBlock(typeof(Page), key, script);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0006ACE4 File Offset: 0x00068EE4
		[Obsolete("The recommended alternative is ClientScript.RegisterStartupScript(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void RegisterStartupScript(string key, string script)
		{
			this.ClientScript.RegisterStartupScript(typeof(Page), key, script, false);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0006ACFE File Offset: 0x00068EFE
		[Obsolete("The recommended alternative is ClientScript.RegisterOnSubmitStatement(Type type, string key, string script). http://go.microsoft.com/fwlink/?linkid=14202")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterOnSubmitStatement(string key, string script)
		{
			this.ClientScript.RegisterOnSubmitStatement(typeof(Page), key, script);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0006AD17 File Offset: 0x00068F17
		internal void RegisterEnabledControl(Control control)
		{
			this.EnabledControls.Add(control);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0006AD28 File Offset: 0x00068F28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterRequiresControlState(Control control)
		{
			if (control == null)
			{
				throw new ArgumentException(SR.GetString("Page_ControlState_ControlCannotBeNull"));
			}
			if (control.ControlState == ControlState.PreRendered)
			{
				throw new InvalidOperationException(SR.GetString("Page_MustCallBeforeAndDuringPreRender", new object[]
				{
					"RegisterRequiresControlState"
				}));
			}
			if (this._registeredControlsRequiringControlState == null)
			{
				this._registeredControlsRequiringControlState = new ControlSet();
			}
			if (!this._registeredControlsRequiringControlState.Contains(control))
			{
				this._registeredControlsRequiringControlState.Add(control);
				IDictionary dictionary = (IDictionary)this.PageStatePersister.ControlState;
				if (dictionary != null)
				{
					string uniqueID = control.UniqueID;
					if (!this.ControlStateLoadedControlIds.Contains(uniqueID))
					{
						control.LoadControlStateInternal(dictionary[uniqueID]);
						this.ControlStateLoadedControlIds.Add(uniqueID);
					}
				}
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0006ADDE File Offset: 0x00068FDE
		public bool RequiresControlState(Control control)
		{
			return this._registeredControlsRequiringControlState != null && this._registeredControlsRequiringControlState.Contains(control);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0006ADF6 File Offset: 0x00068FF6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void UnregisterRequiresControlState(Control control)
		{
			if (control == null)
			{
				throw new ArgumentException(SR.GetString("Page_ControlState_ControlCannotBeNull"));
			}
			if (this._registeredControlsRequiringControlState == null)
			{
				return;
			}
			this._registeredControlsRequiringControlState.Remove(control);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0006AE20 File Offset: 0x00069020
		internal bool ShouldLoadControlState(Control control)
		{
			if (this._registeredControlsRequiringClearChildControlState == null)
			{
				return true;
			}
			foreach (object obj in this._registeredControlsRequiringClearChildControlState.Keys)
			{
				Control control2 = (Control)obj;
				if (control != control2 && control.IsDescendentOf(control2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0006AE98 File Offset: 0x00069098
		internal void RegisterRequiresClearChildControlState(Control control)
		{
			if (this._registeredControlsRequiringClearChildControlState == null)
			{
				this._registeredControlsRequiringClearChildControlState = new HybridDictionary();
				this._registeredControlsRequiringClearChildControlState.Add(control, true);
			}
			else if (this._registeredControlsRequiringClearChildControlState[control] == null)
			{
				this._registeredControlsRequiringClearChildControlState.Add(control, true);
			}
			IDictionary dictionary = (IDictionary)this.PageStatePersister.ControlState;
			if (dictionary != null)
			{
				List<string> list = new List<string>(dictionary.Count);
				foreach (object obj in dictionary.Keys)
				{
					string text = (string)obj;
					Control control2 = this.FindControl(text);
					if (control2 != null && control2.IsDescendentOf(control))
					{
						list.Add(text);
					}
				}
				foreach (string key in list)
				{
					dictionary[key] = null;
				}
			}
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0006AFB8 File Offset: 0x000691B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterRequiresPostBack(Control control)
		{
			if (!(control is IPostBackDataHandler) && !(control.AdapterInternal is IPostBackDataHandler))
			{
				throw new HttpException(SR.GetString("Ctrl_not_data_handler"));
			}
			if (this._registeredControlsThatRequirePostBack == null)
			{
				this._registeredControlsThatRequirePostBack = new ArrayList();
			}
			this._registeredControlsThatRequirePostBack.Add(control.UniqueID);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0006B011 File Offset: 0x00069211
		internal void PushCachingControl(BasePartialCachingControl c)
		{
			if (this._partialCachingControlStack == null)
			{
				this._partialCachingControlStack = new Stack();
			}
			this._partialCachingControlStack.Push(c);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0006B032 File Offset: 0x00069232
		internal void PopCachingControl()
		{
			this._partialCachingControlStack.Pop();
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0006B040 File Offset: 0x00069240
		private void ProcessPostData(NameValueCollection postData, bool fBeforeLoad)
		{
			if (this._changedPostDataConsumers == null)
			{
				this._changedPostDataConsumers = new ArrayList();
			}
			if (postData != null)
			{
				foreach (object obj in postData)
				{
					string text = (string)obj;
					if (text != null && !Page.IsSystemPostField(text))
					{
						Control control = this.FindControl(text);
						if (control == null)
						{
							if (fBeforeLoad)
							{
								if (this._leftoverPostData == null)
								{
									this._leftoverPostData = new NameValueCollection();
								}
								this._leftoverPostData.Add(text, null);
							}
						}
						else
						{
							IPostBackDataHandler postBackDataHandler = control.PostBackDataHandler;
							if (postBackDataHandler == null)
							{
								if (control.PostBackEventHandler != null)
								{
									this.RegisterRequiresRaiseEvent(control.PostBackEventHandler);
								}
							}
							else
							{
								if (postBackDataHandler != null)
								{
									NameValueCollection postCollection = control.CalculateEffectiveValidateRequest() ? this._requestValueCollection : this._unvalidatedRequestValueCollection;
									bool flag = postBackDataHandler.LoadPostData(text, postCollection);
									if (flag)
									{
										this._changedPostDataConsumers.Add(control);
									}
								}
								if (this._controlsRequiringPostBack != null)
								{
									this._controlsRequiringPostBack.Remove(text);
								}
							}
						}
					}
				}
			}
			ArrayList arrayList = null;
			if (this._controlsRequiringPostBack != null)
			{
				foreach (object obj2 in this._controlsRequiringPostBack)
				{
					string text2 = (string)obj2;
					Control control2 = this.FindControl(text2);
					if (control2 != null)
					{
						IPostBackDataHandler postBackDataHandler2 = control2.AdapterInternal as IPostBackDataHandler;
						if (postBackDataHandler2 == null)
						{
							postBackDataHandler2 = (control2 as IPostBackDataHandler);
						}
						if (postBackDataHandler2 == null)
						{
							throw new HttpException(SR.GetString("Postback_ctrl_not_found", new object[]
							{
								text2
							}));
						}
						NameValueCollection postCollection2 = control2.CalculateEffectiveValidateRequest() ? this._requestValueCollection : this._unvalidatedRequestValueCollection;
						bool flag2 = postBackDataHandler2.LoadPostData(text2, postCollection2);
						if (flag2)
						{
							this._changedPostDataConsumers.Add(control2);
						}
					}
					else if (fBeforeLoad)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(text2);
					}
				}
				this._controlsRequiringPostBack = arrayList;
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0006B25C File Offset: 0x0006945C
		private Task ProcessPostDataAsync(NameValueCollection postData, bool fBeforeLoad)
		{
			Page.<ProcessPostDataAsync>d__393 <ProcessPostDataAsync>d__;
			<ProcessPostDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessPostDataAsync>d__.<>4__this = this;
			<ProcessPostDataAsync>d__.postData = postData;
			<ProcessPostDataAsync>d__.fBeforeLoad = fBeforeLoad;
			<ProcessPostDataAsync>d__.<>1__state = -1;
			<ProcessPostDataAsync>d__.<>t__builder.Start<Page.<ProcessPostDataAsync>d__393>(ref <ProcessPostDataAsync>d__);
			return <ProcessPostDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0006B2B0 File Offset: 0x000694B0
		private Task<bool> LoadPostDataAsync(IPostBackDataHandler consumer, string postKey, NameValueCollection postCollection)
		{
			Page.<LoadPostDataAsync>d__394 <LoadPostDataAsync>d__;
			<LoadPostDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<LoadPostDataAsync>d__.<>4__this = this;
			<LoadPostDataAsync>d__.consumer = consumer;
			<LoadPostDataAsync>d__.postKey = postKey;
			<LoadPostDataAsync>d__.postCollection = postCollection;
			<LoadPostDataAsync>d__.<>1__state = -1;
			<LoadPostDataAsync>d__.<>t__builder.Start<Page.<LoadPostDataAsync>d__394>(ref <LoadPostDataAsync>d__);
			return <LoadPostDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0006B30C File Offset: 0x0006950C
		internal void RaiseChangedEvents()
		{
			if (this._changedPostDataConsumers != null)
			{
				for (int i = 0; i < this._changedPostDataConsumers.Count; i++)
				{
					Control control = (Control)this._changedPostDataConsumers[i];
					if (control != null)
					{
						IPostBackDataHandler postBackDataHandler = control.PostBackDataHandler;
						if ((control == null || control.IsDescendentOf(this)) && control != null && control.PostBackDataHandler != null)
						{
							postBackDataHandler.RaisePostDataChangedEvent();
						}
					}
				}
			}
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0006B370 File Offset: 0x00069570
		internal Task RaiseChangedEventsAsync()
		{
			Page.<RaiseChangedEventsAsync>d__396 <RaiseChangedEventsAsync>d__;
			<RaiseChangedEventsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RaiseChangedEventsAsync>d__.<>4__this = this;
			<RaiseChangedEventsAsync>d__.<>1__state = -1;
			<RaiseChangedEventsAsync>d__.<>t__builder.Start<Page.<RaiseChangedEventsAsync>d__396>(ref <RaiseChangedEventsAsync>d__);
			return <RaiseChangedEventsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0006B3B4 File Offset: 0x000695B4
		private void RaisePostBackEvent(NameValueCollection postData)
		{
			if (this._registeredControlThatRequireRaiseEvent != null)
			{
				this.RaisePostBackEvent(this._registeredControlThatRequireRaiseEvent, null);
				return;
			}
			string text = postData["__EVENTTARGET"];
			bool flag = !string.IsNullOrEmpty(text);
			if (flag || this.AutoPostBackControl != null)
			{
				Control control = null;
				if (flag)
				{
					control = this.FindControl(text);
				}
				if (control != null && control.PostBackEventHandler != null)
				{
					string eventArgument = postData["__EVENTARGUMENT"];
					this.RaisePostBackEvent(control.PostBackEventHandler, eventArgument);
					return;
				}
			}
			else
			{
				this.Validate();
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0006B430 File Offset: 0x00069630
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
		{
			sourceControl.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0006B439 File Offset: 0x00069639
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void RegisterRequiresRaiseEvent(IPostBackEventHandler control)
		{
			this._registeredControlThatRequireRaiseEvent = control;
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x0006B442 File Offset: 0x00069642
		public bool IsPostBackEventControlRegistered
		{
			get
			{
				return this._registeredControlThatRequireRaiseEvent != null;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x0006B450 File Offset: 0x00069650
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsValid
		{
			get
			{
				if (!this._validated)
				{
					throw new HttpException(SR.GetString("IsValid_Cant_Be_Called"));
				}
				if (this._validators != null)
				{
					ValidatorCollection validators = this.Validators;
					int count = validators.Count;
					for (int i = 0; i < count; i++)
					{
						if (!validators[i].IsValid)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0006B4A8 File Offset: 0x000696A8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ValidatorCollection Validators
		{
			get
			{
				if (this._validators == null)
				{
					this._validators = new ValidatorCollection();
				}
				return this._validators;
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x0006B4C4 File Offset: 0x000696C4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Page PreviousPage
		{
			get
			{
				if (this._previousPage == null && this._previousPagePath != null)
				{
					if (!Util.IsUserAllowedToPath(this.Context, this._previousPagePath))
					{
						throw new InvalidOperationException(SR.GetString("Previous_Page_Not_Authorized"));
					}
					ITypedWebObjectFactory typedWebObjectFactory = (ITypedWebObjectFactory)BuildManager.GetVPathBuildResult(this.Context, this._previousPagePath);
					if (typeof(Page).IsAssignableFrom(typedWebObjectFactory.InstantiatedType))
					{
						this._previousPage = (Page)typedWebObjectFactory.CreateInstance();
						this._previousPage._isCrossPagePostBack = true;
						this.Server.Execute(this._previousPage, TextWriter.Null, true, false);
					}
				}
				return this._previousPage;
			}
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0006B579 File Offset: 0x00069779
		public string MapPath(string virtualPath)
		{
			return this._request.MapPath(VirtualPath.CreateAllowNull(virtualPath), base.TemplateControlVirtualDirectory, true);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0006B593 File Offset: 0x00069793
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void InitOutputCache(int duration, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
		{
			this.InitOutputCache(duration, null, varyByHeader, varyByCustom, location, varyByParam);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0006B5A4 File Offset: 0x000697A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void InitOutputCache(int duration, string varyByContentEncoding, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
		{
			if (this._isCrossPagePostBack)
			{
				return;
			}
			this.InitOutputCache(new OutputCacheParameters
			{
				Duration = duration,
				VaryByContentEncoding = varyByContentEncoding,
				VaryByHeader = varyByHeader,
				VaryByCustom = varyByCustom,
				Location = location,
				VaryByParam = varyByParam
			});
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0006B5F4 File Offset: 0x000697F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal virtual void InitOutputCache(OutputCacheParameters cacheSettings)
		{
			if (this._isCrossPagePostBack)
			{
				return;
			}
			OutputCacheProfile outputCacheProfile = null;
			HttpCachePolicy cache = this.Response.Cache;
			OutputCacheLocation outputCacheLocation = (OutputCacheLocation)(-1);
			int num = 0;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			bool flag = false;
			RuntimeConfig appConfig = RuntimeConfig.GetAppConfig();
			OutputCacheSection outputCache = appConfig.OutputCache;
			if (!outputCache.EnableOutputCache)
			{
				return;
			}
			if (cacheSettings.CacheProfile != null && cacheSettings.CacheProfile.Length != 0)
			{
				OutputCacheSettingsSection outputCacheSettings = appConfig.OutputCacheSettings;
				outputCacheProfile = outputCacheSettings.OutputCacheProfiles[cacheSettings.CacheProfile];
				if (outputCacheProfile == null)
				{
					throw new HttpException(SR.GetString("CacheProfile_Not_Found", new object[]
					{
						cacheSettings.CacheProfile
					}));
				}
				if (!outputCacheProfile.Enabled)
				{
					return;
				}
			}
			if (outputCacheProfile != null)
			{
				num = outputCacheProfile.Duration;
				text = outputCacheProfile.VaryByContentEncoding;
				text2 = outputCacheProfile.VaryByHeader;
				text3 = outputCacheProfile.VaryByCustom;
				text4 = outputCacheProfile.VaryByParam;
				text5 = outputCacheProfile.SqlDependency;
				flag = outputCacheProfile.NoStore;
				text6 = outputCacheProfile.VaryByControl;
				outputCacheLocation = outputCacheProfile.Location;
				if (string.IsNullOrEmpty(text))
				{
					text = null;
				}
				if (string.IsNullOrEmpty(text2))
				{
					text2 = null;
				}
				if (string.IsNullOrEmpty(text3))
				{
					text3 = null;
				}
				if (string.IsNullOrEmpty(text4))
				{
					text4 = null;
				}
				if (string.IsNullOrEmpty(text6))
				{
					text6 = null;
				}
				if (StringUtil.EqualsIgnoreCase(text4, "none"))
				{
					text4 = null;
				}
				if (StringUtil.EqualsIgnoreCase(text6, "none"))
				{
					text6 = null;
				}
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.Duration))
			{
				num = cacheSettings.Duration;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.VaryByContentEncoding))
			{
				text = cacheSettings.VaryByContentEncoding;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.VaryByHeader))
			{
				text2 = cacheSettings.VaryByHeader;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.VaryByCustom))
			{
				text3 = cacheSettings.VaryByCustom;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.VaryByControl))
			{
				text6 = cacheSettings.VaryByControl;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.VaryByParam))
			{
				text4 = cacheSettings.VaryByParam;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.SqlDependency))
			{
				text5 = cacheSettings.SqlDependency;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.NoStore))
			{
				flag = cacheSettings.NoStore;
			}
			if (cacheSettings.IsParameterSet(OutputCacheParameter.Location))
			{
				outputCacheLocation = cacheSettings.Location;
			}
			if (outputCacheLocation == (OutputCacheLocation)(-1))
			{
				outputCacheLocation = OutputCacheLocation.Any;
			}
			if (outputCacheLocation != OutputCacheLocation.None && (outputCacheProfile == null || outputCacheProfile.Enabled))
			{
				if ((outputCacheProfile == null || outputCacheProfile.Duration == -1) && !cacheSettings.IsParameterSet(OutputCacheParameter.Duration))
				{
					throw new HttpException(SR.GetString("Missing_output_cache_attr", new object[]
					{
						"duration"
					}));
				}
				if ((outputCacheProfile == null || (outputCacheProfile.VaryByParam == null && outputCacheProfile.VaryByControl == null)) && !cacheSettings.IsParameterSet(OutputCacheParameter.VaryByParam) && !cacheSettings.IsParameterSet(OutputCacheParameter.VaryByControl))
				{
					throw new HttpException(SR.GetString("Missing_output_cache_attr", new object[]
					{
						"varyByParam"
					}));
				}
			}
			if (flag)
			{
				this.Response.Cache.SetNoStore();
			}
			HttpCacheability cacheability;
			switch (outputCacheLocation)
			{
			case OutputCacheLocation.Any:
				cacheability = HttpCacheability.Public;
				break;
			case OutputCacheLocation.Client:
				cacheability = HttpCacheability.Private;
				break;
			case OutputCacheLocation.Downstream:
				cacheability = HttpCacheability.Public;
				cache.SetNoServerCaching();
				break;
			case OutputCacheLocation.Server:
				cacheability = HttpCacheability.Server;
				break;
			case OutputCacheLocation.None:
				cacheability = HttpCacheability.NoCache;
				break;
			case OutputCacheLocation.ServerAndClient:
				cacheability = HttpCacheability.ServerAndPrivate;
				break;
			default:
				throw new ArgumentOutOfRangeException("cacheSettings", SR.GetString("Invalid_cache_settings_location"));
			}
			cache.SetCacheability(cacheability);
			if (outputCacheLocation != OutputCacheLocation.None)
			{
				cache.SetExpires(this.Context.Timestamp.AddSeconds((double)num));
				cache.SetMaxAge(new TimeSpan(0, 0, num));
				cache.SetValidUntilExpires(true);
				cache.SetLastModified(this.Context.Timestamp);
				if (outputCacheLocation != OutputCacheLocation.Client)
				{
					if (text != null)
					{
						string[] array = text.Split(Page.s_varySeparator);
						foreach (string text7 in array)
						{
							cache.VaryByContentEncodings[text7.Trim()] = true;
						}
					}
					if (text2 != null)
					{
						string[] array3 = text2.Split(Page.s_varySeparator);
						foreach (string text8 in array3)
						{
							cache.VaryByHeaders[text8.Trim()] = true;
						}
					}
					if (this.PageAdapter != null)
					{
						StringCollection cacheVaryByHeaders = this.PageAdapter.CacheVaryByHeaders;
						if (cacheVaryByHeaders != null)
						{
							foreach (string header in cacheVaryByHeaders)
							{
								cache.VaryByHeaders[header] = true;
							}
						}
					}
					if (outputCacheLocation != OutputCacheLocation.Downstream)
					{
						if (text3 != null)
						{
							cache.SetVaryByCustom(text3);
						}
						if (string.IsNullOrEmpty(text4) && string.IsNullOrEmpty(text6) && (this.PageAdapter == null || this.PageAdapter.CacheVaryByParams == null))
						{
							cache.VaryByParams.IgnoreParams = true;
						}
						else
						{
							if (!string.IsNullOrEmpty(text4))
							{
								string[] array5 = text4.Split(Page.s_varySeparator);
								foreach (string text9 in array5)
								{
									cache.VaryByParams[text9.Trim()] = true;
								}
							}
							if (!string.IsNullOrEmpty(text6))
							{
								string[] array7 = text6.Split(Page.s_varySeparator);
								foreach (string text10 in array7)
								{
									cache.VaryByParams[text10.Trim()] = true;
								}
							}
							if (this.PageAdapter != null)
							{
								IList cacheVaryByParams = this.PageAdapter.CacheVaryByParams;
								if (cacheVaryByParams != null)
								{
									foreach (object obj in cacheVaryByParams)
									{
										string header2 = (string)obj;
										cache.VaryByParams[header2] = true;
									}
								}
							}
						}
						if (!string.IsNullOrEmpty(text5))
						{
							this.Response.AddCacheDependency(new CacheDependency[]
							{
								SqlCacheDependency.CreateOutputCacheDependency(text5)
							});
						}
					}
				}
			}
		}

		// Token: 0x1700094B RID: 2379
		// (set) Token: 0x0600213A RID: 8506 RVA: 0x0006BBB8 File Offset: 0x00069DB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("The recommended alternative is HttpResponse.AddFileDependencies. http://go.microsoft.com/fwlink/?linkid=14202")]
		protected ArrayList FileDependencies
		{
			set
			{
				this.Response.AddFileDependencies(value);
			}
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00036414 File Offset: 0x00034614
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected object GetWrappedFileDependencies(string[] virtualFileDependencies)
		{
			return virtualFileDependencies;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0006BBC6 File Offset: 0x00069DC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected internal void AddWrappedFileDependencies(object virtualFileDependencies)
		{
			this.Response.AddVirtualPathDependencies((string[])virtualFileDependencies);
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0006BBE7 File Offset: 0x00069DE7
		// (set) Token: 0x0600213D RID: 8509 RVA: 0x0006BBD9 File Offset: 0x00069DD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Buffer
		{
			get
			{
				return this.Response.BufferOutput;
			}
			set
			{
				this.Response.BufferOutput = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0006BC02 File Offset: 0x00069E02
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x0006BBF4 File Offset: 0x00069DF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ContentType
		{
			get
			{
				return this.Response.ContentType;
			}
			set
			{
				this.Response.ContentType = value;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0006BC22 File Offset: 0x00069E22
		// (set) Token: 0x06002141 RID: 8513 RVA: 0x0006BC0F File Offset: 0x00069E0F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CodePage
		{
			get
			{
				return this.Response.ContentEncoding.CodePage;
			}
			set
			{
				this.Response.ContentEncoding = Encoding.GetEncoding(value);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x0006BC47 File Offset: 0x00069E47
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x0006BC34 File Offset: 0x00069E34
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ResponseEncoding
		{
			get
			{
				return this.Response.ContentEncoding.EncodingName;
			}
			set
			{
				this.Response.ContentEncoding = Encoding.GetEncoding(value);
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0006BCE4 File Offset: 0x00069EE4
		// (set) Token: 0x06002145 RID: 8517 RVA: 0x0006BC5C File Offset: 0x00069E5C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Culture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.DisplayName;
			}
			set
			{
				CultureInfo cultureInfo = null;
				if (StringUtil.EqualsIgnoreCase(value, HttpApplication.AutoCulture))
				{
					CultureInfo cultureInfo2 = this.CultureFromUserLanguages(true);
					if (cultureInfo2 != null)
					{
						cultureInfo = cultureInfo2;
					}
				}
				else
				{
					if (StringUtil.StringStartsWithIgnoreCase(value, HttpApplication.AutoCulture))
					{
						CultureInfo cultureInfo3 = this.CultureFromUserLanguages(true);
						if (cultureInfo3 != null)
						{
							cultureInfo = cultureInfo3;
							goto IL_54;
						}
						try
						{
							cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(value.Substring(5));
							goto IL_54;
						}
						catch
						{
							goto IL_54;
						}
					}
					cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(value);
				}
				IL_54:
				if (cultureInfo != null)
				{
					Thread.CurrentThread.CurrentCulture = cultureInfo;
					this._dynamicCulture = cultureInfo;
				}
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x0006BCF5 File Offset: 0x00069EF5
		internal CultureInfo DynamicCulture
		{
			get
			{
				return this._dynamicCulture;
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x0003246A File Offset: 0x0003066A
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x0006BD00 File Offset: 0x00069F00
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int LCID
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture.LCID;
			}
			set
			{
				CultureInfo cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(value);
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				this._dynamicCulture = cultureInfo;
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0006BD28 File Offset: 0x00069F28
		private CultureInfo CultureFromUserLanguages(bool specific)
		{
			if (this._context != null && this._context.Request != null && this._context.Request.UserLanguages != null)
			{
				try
				{
					return CultureUtil.CreateReadOnlyCulture(this._context.Request.UserLanguages, specific);
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x0006BE14 File Offset: 0x0006A014
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x0006BD8C File Offset: 0x00069F8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string UICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture.DisplayName;
			}
			set
			{
				CultureInfo cultureInfo = null;
				if (StringUtil.EqualsIgnoreCase(value, HttpApplication.AutoCulture))
				{
					CultureInfo cultureInfo2 = this.CultureFromUserLanguages(false);
					if (cultureInfo2 != null)
					{
						cultureInfo = cultureInfo2;
					}
				}
				else
				{
					if (StringUtil.StringStartsWithIgnoreCase(value, HttpApplication.AutoCulture))
					{
						CultureInfo cultureInfo3 = this.CultureFromUserLanguages(false);
						if (cultureInfo3 != null)
						{
							cultureInfo = cultureInfo3;
							goto IL_54;
						}
						try
						{
							cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(value.Substring(5));
							goto IL_54;
						}
						catch
						{
							goto IL_54;
						}
					}
					cultureInfo = HttpServerUtility.CreateReadOnlyCultureInfo(value);
				}
				IL_54:
				if (cultureInfo != null)
				{
					Thread.CurrentThread.CurrentUICulture = cultureInfo;
					this._dynamicUICulture = cultureInfo;
				}
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0006BE25 File Offset: 0x0006A025
		internal CultureInfo DynamicUICulture
		{
			get
			{
				return this._dynamicUICulture;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0006BE60 File Offset: 0x0006A060
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x0006BE2D File Offset: 0x0006A02D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TimeSpan AsyncTimeout
		{
			get
			{
				if (!this._asyncTimeoutSet)
				{
					if (this.Context != null)
					{
						PagesSection pages = RuntimeConfig.GetConfig(this.Context).Pages;
						if (pages != null)
						{
							this.AsyncTimeout = pages.AsyncTimeout;
						}
					}
					if (!this._asyncTimeoutSet)
					{
						this.AsyncTimeout = TimeSpan.FromSeconds((double)Page.DefaultAsyncTimeoutSeconds);
					}
				}
				return this._asyncTimeout;
			}
			set
			{
				if (value < TimeSpan.Zero)
				{
					throw new ArgumentException(SR.GetString("Page_Illegal_AsyncTimeout"), "AsyncTimeout");
				}
				this._asyncTimeout = value;
				this._asyncTimeoutSet = true;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x0006BEC5 File Offset: 0x0006A0C5
		// (set) Token: 0x06002150 RID: 8528 RVA: 0x0006BEBC File Offset: 0x0006A0BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected int TransactionMode
		{
			get
			{
				return this._transactionMode;
			}
			set
			{
				this._transactionMode = value;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x0006BED6 File Offset: 0x0006A0D6
		// (set) Token: 0x06002152 RID: 8530 RVA: 0x0006BECD File Offset: 0x0006A0CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected bool AspCompatMode
		{
			get
			{
				return this._aspCompatMode;
			}
			set
			{
				this._aspCompatMode = value;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0006BEE7 File Offset: 0x0006A0E7
		// (set) Token: 0x06002154 RID: 8532 RVA: 0x0006BEDE File Offset: 0x0006A0DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected bool AsyncMode
		{
			get
			{
				return this._asyncMode;
			}
			set
			{
				this._asyncMode = value;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x0006BEFD File Offset: 0x0006A0FD
		// (set) Token: 0x06002156 RID: 8534 RVA: 0x0006BEEF File Offset: 0x0006A0EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool TraceEnabled
		{
			get
			{
				return this.Trace.IsEnabled;
			}
			set
			{
				this.Trace.IsEnabled = value;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x0006BF18 File Offset: 0x0006A118
		// (set) Token: 0x06002158 RID: 8536 RVA: 0x0006BF0A File Offset: 0x0006A10A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TraceMode TraceModeValue
		{
			get
			{
				return this.Trace.TraceMode;
			}
			set
			{
				this.Trace.TraceMode = value;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0006BF25 File Offset: 0x0006A125
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x0006BF2D File Offset: 0x0006A12D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EnableViewStateMac
		{
			get
			{
				return this._enableViewStateMac;
			}
			set
			{
				if (!EnableViewStateMacRegistryHelper.EnforceViewStateMac)
				{
					this._enableViewStateMac = value;
				}
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x0006BF40 File Offset: 0x0006A140
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x0006BFB4 File Offset: 0x0006A1B4
		[Browsable(false)]
		[Filterable(false)]
		[Obsolete("The recommended alternative is Page.SetFocus and Page.MaintainScrollPositionOnPostBack. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool SmartNavigation
		{
			get
			{
				if (this._smartNavSupport == SmartNavigationSupport.NotDesiredOrSupported)
				{
					return false;
				}
				if (this._smartNavSupport == SmartNavigationSupport.Desired)
				{
					HttpContext httpContext = HttpContext.Current;
					if (httpContext == null)
					{
						return false;
					}
					HttpBrowserCapabilities browser = httpContext.Request.Browser;
					if (!string.Equals(browser.Browser, "ie", StringComparison.OrdinalIgnoreCase) || browser.MajorVersion < 6 || !browser.Win32)
					{
						this._smartNavSupport = SmartNavigationSupport.NotDesiredOrSupported;
					}
					else
					{
						this._smartNavSupport = SmartNavigationSupport.IE6OrNewer;
					}
				}
				return this._smartNavSupport > SmartNavigationSupport.NotDesiredOrSupported;
			}
			set
			{
				if (value)
				{
					this._smartNavSupport = SmartNavigationSupport.Desired;
					return;
				}
				this._smartNavSupport = SmartNavigationSupport.NotDesiredOrSupported;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0006BFC8 File Offset: 0x0006A1C8
		internal bool IsTransacted
		{
			get
			{
				return this._transactionMode != 0;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x0006BED6 File Offset: 0x0006A0D6
		internal bool IsInAspCompatMode
		{
			get
			{
				return this._aspCompatMode;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0006BEE7 File Offset: 0x0006A0E7
		public bool IsAsync
		{
			get
			{
				return this._asyncMode;
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002161 RID: 8545 RVA: 0x0006BFD3 File Offset: 0x0006A1D3
		// (remove) Token: 0x06002162 RID: 8546 RVA: 0x0006BFE6 File Offset: 0x0006A1E6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler LoadComplete
		{
			add
			{
				base.Events.AddHandler(Page.EventLoadComplete, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.EventLoadComplete, value);
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0006BFFC File Offset: 0x0006A1FC
		protected virtual void OnLoadComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Page.EventLoadComplete];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0006C02C File Offset: 0x0006A22C
		protected virtual void OnPreRenderComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Page.EventPreRenderComplete];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0006C05A File Offset: 0x0006A25A
		private void PerformPreRenderComplete()
		{
			this.OnPreRenderComplete(EventArgs.Empty);
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002166 RID: 8550 RVA: 0x0006C067 File Offset: 0x0006A267
		// (remove) Token: 0x06002167 RID: 8551 RVA: 0x0006C07A File Offset: 0x0006A27A
		public event EventHandler PreInit
		{
			add
			{
				base.Events.AddHandler(Page.EventPreInit, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.EventPreInit, value);
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002168 RID: 8552 RVA: 0x0006C08D File Offset: 0x0006A28D
		// (remove) Token: 0x06002169 RID: 8553 RVA: 0x0006C0A0 File Offset: 0x0006A2A0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler PreLoad
		{
			add
			{
				base.Events.AddHandler(Page.EventPreLoad, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.EventPreLoad, value);
			}
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600216A RID: 8554 RVA: 0x0006C0B3 File Offset: 0x0006A2B3
		// (remove) Token: 0x0600216B RID: 8555 RVA: 0x0006C0C6 File Offset: 0x0006A2C6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler PreRenderComplete
		{
			add
			{
				base.Events.AddHandler(Page.EventPreRenderComplete, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.EventPreRenderComplete, value);
			}
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0006C0D9 File Offset: 0x0006A2D9
		protected override void FrameworkInitialize()
		{
			base.FrameworkInitialize();
			this.InitializeStyleSheet();
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void InitializeCulture()
		{
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0006C0E7 File Offset: 0x0006A2E7
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this._theme != null)
			{
				this._theme.SetStyleSheet();
			}
			if (this._styleSheet != null)
			{
				this._styleSheet.SetStyleSheet();
			}
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0006C118 File Offset: 0x0006A318
		protected virtual void OnPreInit(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Page.EventPreInit];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0006C146 File Offset: 0x0006A346
		private void PerformPreInit()
		{
			this.OnPreInit(EventArgs.Empty);
			this.InitializeThemes();
			this.ApplyMasterPage();
			this._preInitWorkComplete = true;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0006C168 File Offset: 0x0006A368
		private Task PerformPreInitAsync()
		{
			Page.<PerformPreInitAsync>d__495 <PerformPreInitAsync>d__;
			<PerformPreInitAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PerformPreInitAsync>d__.<>4__this = this;
			<PerformPreInitAsync>d__.<>1__state = -1;
			<PerformPreInitAsync>d__.<>t__builder.Start<Page.<PerformPreInitAsync>d__495>(ref <PerformPreInitAsync>d__);
			return <PerformPreInitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06002172 RID: 8562 RVA: 0x0006C1AB File Offset: 0x0006A3AB
		// (remove) Token: 0x06002173 RID: 8563 RVA: 0x0006C1BE File Offset: 0x0006A3BE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler InitComplete
		{
			add
			{
				base.Events.AddHandler(Page.EventInitComplete, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.EventInitComplete, value);
			}
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0006C1D4 File Offset: 0x0006A3D4
		protected virtual void OnInitComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Page.EventInitComplete];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0006C204 File Offset: 0x0006A404
		protected virtual void OnPreLoad(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Page.EventPreLoad];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0006C232 File Offset: 0x0006A432
		public void RegisterRequiresViewStateEncryption()
		{
			if (base.ControlState >= ControlState.PreRendered)
			{
				throw new InvalidOperationException(SR.GetString("Too_late_for_RegisterRequiresViewStateEncryption"));
			}
			this._viewStateEncryptionRequested = true;
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x0006C254 File Offset: 0x0006A454
		internal bool RequiresViewStateEncryptionInternal
		{
			get
			{
				return this.ViewStateEncryptionMode == ViewStateEncryptionMode.Always || (this._viewStateEncryptionRequested && this.ViewStateEncryptionMode == ViewStateEncryptionMode.Auto);
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06002178 RID: 8568 RVA: 0x0006C274 File Offset: 0x0006A474
		// (remove) Token: 0x06002179 RID: 8569 RVA: 0x0006C287 File Offset: 0x0006A487
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler SaveStateComplete
		{
			add
			{
				base.Events.AddHandler(Page.EventSaveStateComplete, value);
			}
			remove
			{
				base.Events.RemoveHandler(Page.EventSaveStateComplete, value);
			}
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0006C29C File Offset: 0x0006A49C
		protected virtual void OnSaveStateComplete(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Page.EventSaveStateComplete];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0006C2CA File Offset: 0x0006A4CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ProcessRequest(HttpContext context)
		{
			if (HttpRuntime.NamedPermissionSet != null && !HttpRuntime.DisableProcessRequestInApplicationTrust)
			{
				if (!HttpRuntime.ProcessRequestInApplicationTrust)
				{
					this.ProcessRequestWithAssert(context);
					return;
				}
				if (base.NoCompile)
				{
					HttpRuntime.NamedPermissionSet.PermitOnly();
				}
			}
			this.ProcessRequestWithNoAssert(context);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0006C304 File Offset: 0x0006A504
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void ProcessRequestWithAssert(HttpContext context)
		{
			this.ProcessRequestWithNoAssert(context);
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0006C30D File Offset: 0x0006A50D
		private void ProcessRequestWithNoAssert(HttpContext context)
		{
			this.SetIntrinsics(context);
			this.ProcessRequest();
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0006C31C File Offset: 0x0006A51C
		[SecurityPermission(SecurityAction.Assert, ControlThread = true)]
		private void SetCultureWithAssert(Thread currentThread, CultureInfo currentCulture, CultureInfo currentUICulture)
		{
			this.SetCulture(currentThread, currentCulture, currentUICulture);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0006C327 File Offset: 0x0006A527
		private void SetCulture(Thread currentThread, CultureInfo currentCulture, CultureInfo currentUICulture)
		{
			currentThread.CurrentCulture = currentCulture;
			currentThread.CurrentUICulture = currentUICulture;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0006C338 File Offset: 0x0006A538
		private void ProcessRequest()
		{
			Thread currentThread = Thread.CurrentThread;
			CultureInfo currentCulture = currentThread.CurrentCulture;
			CultureInfo currentUICulture = currentThread.CurrentUICulture;
			try
			{
				this.ProcessRequest(true, true);
			}
			finally
			{
				this.RestoreCultures(currentThread, currentCulture, currentUICulture);
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0006C380 File Offset: 0x0006A580
		private void ProcessRequest(bool includeStagesBeforeAsyncPoint, bool includeStagesAfterAsyncPoint)
		{
			if (includeStagesBeforeAsyncPoint)
			{
				this.FrameworkInitialize();
				base.ControlState = ControlState.FrameworkInitialized;
			}
			bool flag = this.Context.WorkerRequest is IIS7WorkerRequest;
			try
			{
				try
				{
					if (this.IsTransacted)
					{
						this.ProcessRequestTransacted();
					}
					else
					{
						this.ProcessRequestMain(includeStagesBeforeAsyncPoint, includeStagesAfterAsyncPoint);
					}
					if (includeStagesAfterAsyncPoint)
					{
						flag = false;
						this.ProcessRequestEndTrace();
					}
				}
				catch (ThreadAbortException)
				{
					try
					{
						if (flag)
						{
							this.ProcessRequestEndTrace();
						}
					}
					catch
					{
					}
				}
				finally
				{
					if (includeStagesAfterAsyncPoint)
					{
						this.ProcessRequestCleanup();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0006C42C File Offset: 0x0006A62C
		private Task ProcessRequestAsync(bool includeStagesBeforeAsyncPoint, bool includeStagesAfterAsyncPoint)
		{
			Page.<ProcessRequestAsync>d__515 <ProcessRequestAsync>d__;
			<ProcessRequestAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessRequestAsync>d__.<>4__this = this;
			<ProcessRequestAsync>d__.includeStagesBeforeAsyncPoint = includeStagesBeforeAsyncPoint;
			<ProcessRequestAsync>d__.includeStagesAfterAsyncPoint = includeStagesAfterAsyncPoint;
			<ProcessRequestAsync>d__.<>1__state = -1;
			<ProcessRequestAsync>d__.<>t__builder.Start<Page.<ProcessRequestAsync>d__515>(ref <ProcessRequestAsync>d__);
			return <ProcessRequestAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0006C47F File Offset: 0x0006A67F
		private void RestoreCultures(Thread currentThread, CultureInfo prevCulture, CultureInfo prevUICulture)
		{
			if (prevCulture != currentThread.CurrentCulture || prevUICulture != currentThread.CurrentUICulture)
			{
				if (HttpRuntime.IsFullTrust)
				{
					this.SetCulture(currentThread, prevCulture, prevUICulture);
					return;
				}
				this.SetCultureWithAssert(currentThread, prevCulture, prevUICulture);
			}
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0006C4B0 File Offset: 0x0006A6B0
		private void ProcessRequestTransacted()
		{
			bool flag = false;
			TransactedCallback callback = new TransactedCallback(this.ProcessRequestMain);
			Transactions.InvokeTransacted(callback, (TransactionOption)this._transactionMode, ref flag);
			try
			{
				if (flag)
				{
					this.OnAbortTransaction(EventArgs.Empty);
					WebBaseEvent.RaiseSystemEvent(this, 2002);
				}
				else
				{
					this.OnCommitTransaction(EventArgs.Empty);
					WebBaseEvent.RaiseSystemEvent(this, 2001);
				}
				this.ValidateRawUrlIfRequired();
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception e)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_DURING_REQUEST);
				PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_TOTAL);
				if (!this.HandleError(e))
				{
					throw;
				}
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0006C550 File Offset: 0x0006A750
		private void ProcessRequestCleanup()
		{
			if (this._request == null)
			{
				return;
			}
			this._request = null;
			this._response = null;
			if (!this.IsCrossPagePostBack)
			{
				this.UnloadRecursive(true);
			}
			if (this.Context.TraceIsEnabled)
			{
				this.Trace.StopTracing();
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0006C590 File Offset: 0x0006A790
		private void ProcessRequestEndTrace()
		{
			if (this.Context.TraceIsEnabled)
			{
				this.Trace.EndRequest();
				if (this.Trace.PageOutput && !this.IsCallback && (this.ScriptManager == null || !this.ScriptManager.IsInAsyncPostBack))
				{
					this.Trace.Render(this.CreateHtmlTextWriter(this.Response.Output));
					this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
				}
			}
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0006C60C File Offset: 0x0006A80C
		internal void SetPreviousPage(Page previousPage)
		{
			this._previousPage = previousPage;
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0006C615 File Offset: 0x0006A815
		private void ProcessRequestMain()
		{
			this.ProcessRequestMain(true, true);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0006C620 File Offset: 0x0006A820
		private void ProcessRequestMain(bool includeStagesBeforeAsyncPoint, bool includeStagesAfterAsyncPoint)
		{
			try
			{
				HttpContext context = this.Context;
				string text = null;
				if (includeStagesBeforeAsyncPoint)
				{
					if (this.IsInAspCompatMode)
					{
						AspCompatApplicationStep.OnPageStartSessionObjects();
					}
					if (this.PageAdapter != null)
					{
						this._requestValueCollection = this.PageAdapter.DeterminePostBackMode();
						if (this._requestValueCollection != null)
						{
							this._unvalidatedRequestValueCollection = this.PageAdapter.DeterminePostBackModeUnvalidated();
						}
					}
					else
					{
						this._requestValueCollection = this.DeterminePostBackMode();
						if (this._requestValueCollection != null)
						{
							this._unvalidatedRequestValueCollection = this.DeterminePostBackModeUnvalidated();
						}
					}
					string text2 = string.Empty;
					if (this.DetermineIsExportingWebPart())
					{
						if (!RuntimeConfig.GetAppConfig().WebParts.EnableExport)
						{
							throw new InvalidOperationException(SR.GetString("WebPartExportHandler_DisabledExportHandler"));
						}
						text = this.Request.QueryString["webPart"];
						if (string.IsNullOrEmpty(text))
						{
							throw new InvalidOperationException(SR.GetString("WebPartExportHandler_InvalidArgument"));
						}
						if (string.Equals(this.Request.QueryString["scope"], "shared", StringComparison.OrdinalIgnoreCase))
						{
							this._pageFlags.Set(4);
						}
						string text3 = this.Request.QueryString["query"];
						if (text3 == null)
						{
							text3 = string.Empty;
						}
						this.Request.QueryStringText = text3;
						context.Trace.IsEnabled = false;
					}
					if (this._requestValueCollection != null)
					{
						if (this._requestValueCollection["__VIEWSTATEENCRYPTED"] != null)
						{
							this.ContainsEncryptedViewState = true;
						}
						text2 = this._requestValueCollection["__CALLBACKID"];
						if (text2 != null && this._request.HttpVerb == HttpVerb.POST)
						{
							this._isCallback = true;
						}
						else if (!this.IsCrossPagePostBack)
						{
							VirtualPath virtualPath = null;
							if (this._requestValueCollection["__PREVIOUSPAGE"] != null)
							{
								try
								{
									virtualPath = VirtualPath.CreateNonRelativeAllowNull(Page.DecryptString(this._requestValueCollection["__PREVIOUSPAGE"], Purpose.WebForms_Page_PreviousPageID));
								}
								catch
								{
									this._pageFlags[8] = true;
								}
								if (virtualPath != null && virtualPath != this.Request.CurrentExecutionFilePathObject)
								{
									this._pageFlags[8] = true;
									this._previousPagePath = virtualPath;
								}
							}
						}
					}
					if (this.MaintainScrollPositionOnPostBack)
					{
						this.LoadScrollPosition();
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "Begin PreInit");
					}
					if (EtwTrace.IsTraceEnabled(5, 4))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_PRE_INIT_ENTER, this._context.WorkerRequest);
					}
					this.PerformPreInit();
					if (EtwTrace.IsTraceEnabled(5, 4))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_PRE_INIT_LEAVE, this._context.WorkerRequest);
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "End PreInit");
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "Begin Init");
					}
					if (EtwTrace.IsTraceEnabled(5, 4))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_INIT_ENTER, this._context.WorkerRequest);
					}
					this.InitRecursive(null);
					if (EtwTrace.IsTraceEnabled(5, 4))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_INIT_LEAVE, this._context.WorkerRequest);
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "End Init");
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "Begin InitComplete");
					}
					this.OnInitComplete(EventArgs.Empty);
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "End InitComplete");
					}
					if (this.IsPostBack)
					{
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "Begin LoadState");
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_LOAD_VIEWSTATE_ENTER, this._context.WorkerRequest);
						}
						this.LoadAllState();
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_LOAD_VIEWSTATE_LEAVE, this._context.WorkerRequest);
						}
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End LoadState");
							this.Trace.Write("aspx.page", "Begin ProcessPostData");
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_LOAD_POSTDATA_ENTER, this._context.WorkerRequest);
						}
						this.ProcessPostData(this._requestValueCollection, true);
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_LOAD_POSTDATA_LEAVE, this._context.WorkerRequest);
						}
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End ProcessPostData");
						}
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "Begin PreLoad");
					}
					this.OnPreLoad(EventArgs.Empty);
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "End PreLoad");
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "Begin Load");
					}
					if (EtwTrace.IsTraceEnabled(5, 4))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_LOAD_ENTER, this._context.WorkerRequest);
					}
					this.LoadRecursive();
					if (EtwTrace.IsTraceEnabled(5, 4))
					{
						EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_LOAD_LEAVE, this._context.WorkerRequest);
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "End Load");
					}
					if (this.IsPostBack)
					{
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "Begin ProcessPostData Second Try");
						}
						this.ProcessPostData(this._leftoverPostData, false);
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End ProcessPostData Second Try");
							this.Trace.Write("aspx.page", "Begin Raise ChangedEvents");
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_POST_DATA_CHANGED_ENTER, this._context.WorkerRequest);
						}
						this.RaiseChangedEvents();
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_POST_DATA_CHANGED_LEAVE, this._context.WorkerRequest);
						}
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End Raise ChangedEvents");
							this.Trace.Write("aspx.page", "Begin Raise PostBackEvent");
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_RAISE_POSTBACK_ENTER, this._context.WorkerRequest);
						}
						this.RaisePostBackEvent(this._requestValueCollection);
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_RAISE_POSTBACK_LEAVE, this._context.WorkerRequest);
						}
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End Raise PostBackEvent");
						}
					}
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "Begin LoadComplete");
					}
					this.OnLoadComplete(EventArgs.Empty);
					if (context.TraceIsEnabled)
					{
						this.Trace.Write("aspx.page", "End LoadComplete");
					}
					if (this.IsPostBack && this.IsCallback)
					{
						this.PrepareCallback(text2);
					}
					else if (!this.IsCrossPagePostBack)
					{
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "Begin PreRender");
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_PRE_RENDER_ENTER, this._context.WorkerRequest);
						}
						this.PreRenderRecursiveInternal();
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_PRE_RENDER_LEAVE, this._context.WorkerRequest);
						}
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End PreRender");
						}
					}
				}
				if (this._legacyAsyncInfo == null || this._legacyAsyncInfo.CallerIsBlocking)
				{
					this.ExecuteRegisteredAsyncTasks();
				}
				this.ValidateRawUrlIfRequired();
				if (includeStagesAfterAsyncPoint)
				{
					if (this.IsCallback)
					{
						this.RenderCallback();
					}
					else if (!this.IsCrossPagePostBack)
					{
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "Begin PreRenderComplete");
						}
						this.PerformPreRenderComplete();
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End PreRenderComplete");
						}
						if (context.TraceIsEnabled)
						{
							this.BuildPageProfileTree(this.EnableViewState);
							this.Trace.Write("aspx.page", "Begin SaveState");
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_SAVE_VIEWSTATE_ENTER, this._context.WorkerRequest);
						}
						this.SaveAllState();
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_SAVE_VIEWSTATE_LEAVE, this._context.WorkerRequest);
						}
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End SaveState");
							this.Trace.Write("aspx.page", "Begin SaveStateComplete");
						}
						this.OnSaveStateComplete(EventArgs.Empty);
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End SaveStateComplete");
							this.Trace.Write("aspx.page", "Begin Render");
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_RENDER_ENTER, this._context.WorkerRequest);
						}
						if (text != null)
						{
							this.ExportWebPart(text);
						}
						else
						{
							this.RenderControl(this.CreateHtmlTextWriter(this.Response.Output));
						}
						if (EtwTrace.IsTraceEnabled(5, 4))
						{
							EtwTrace.Trace(EtwTraceType.ETW_TYPE_PAGE_RENDER_LEAVE, this._context.WorkerRequest);
						}
						if (context.TraceIsEnabled)
						{
							this.Trace.Write("aspx.page", "End Render");
						}
						this.CheckRemainingAsyncTasks(false);
					}
				}
			}
			catch (ThreadAbortException ex)
			{
				HttpApplication.CancelModuleException ex2 = ex.ExceptionState as HttpApplication.CancelModuleException;
				if (!includeStagesBeforeAsyncPoint || !includeStagesAfterAsyncPoint || this._context.Handler != this || this._context.ApplicationInstance == null || ex2 == null || ex2.Timeout)
				{
					this.CheckRemainingAsyncTasks(true);
					throw;
				}
				this._context.ApplicationInstance.CompleteRequest();
				Page.ThreadResetAbortWithAssert();
			}
			catch (ConfigurationException)
			{
				throw;
			}
			catch (Exception e)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_DURING_REQUEST);
				PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_TOTAL);
				if (!this.HandleError(e))
				{
					throw;
				}
			}
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0006CFFC File Offset: 0x0006B1FC
		private Task ProcessRequestMainAsync(bool includeStagesBeforeAsyncPoint, bool includeStagesAfterAsyncPoint)
		{
			Page.<ProcessRequestMainAsync>d__523 <ProcessRequestMainAsync>d__;
			<ProcessRequestMainAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessRequestMainAsync>d__.<>4__this = this;
			<ProcessRequestMainAsync>d__.includeStagesBeforeAsyncPoint = includeStagesBeforeAsyncPoint;
			<ProcessRequestMainAsync>d__.includeStagesAfterAsyncPoint = includeStagesAfterAsyncPoint;
			<ProcessRequestMainAsync>d__.<>1__state = -1;
			<ProcessRequestMainAsync>d__.<>t__builder.Start<Page.<ProcessRequestMainAsync>d__523>(ref <ProcessRequestMainAsync>d__);
			return <ProcessRequestMainAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0006D050 File Offset: 0x0006B250
		internal WithinCancellableCallbackTaskAwaitable GetWaitForPreviousStepCompletionAwaitable()
		{
			AspNetSynchronizationContext aspNetSynchronizationContext = SynchronizationContext.Current as AspNetSynchronizationContext;
			if (aspNetSynchronizationContext != null)
			{
				return aspNetSynchronizationContext.WaitForPendingOperationsAsync().WithinCancellableCallback(this.Context);
			}
			return WithinCancellableCallbackTaskAwaitable.Completed;
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0006D082 File Offset: 0x0006B282
		private void BuildPageProfileTree(bool enableViewState)
		{
			if (!this._profileTreeBuilt)
			{
				this._profileTreeBuilt = true;
				base.BuildProfileTree("ROOT", enableViewState);
			}
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0006D0A0 File Offset: 0x0006B2A0
		private void ExportWebPart(string exportedWebPartID)
		{
			WebPart webPart = null;
			WebPartManager currentWebPartManager = WebPartManager.GetCurrentWebPartManager(this);
			if (currentWebPartManager != null)
			{
				webPart = currentWebPartManager.WebParts[exportedWebPartID];
			}
			if (webPart == null || webPart.IsClosed || webPart is ProxyWebPart)
			{
				this.Response.Redirect(this.Request.RawUrl, false);
				return;
			}
			this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			this.Response.Expires = 0;
			this.Response.ContentType = "application/mswebpart";
			string text = webPart.DisplayTitle;
			if (string.IsNullOrEmpty(text))
			{
				text = SR.GetString("Part_Untitled");
			}
			NonWordRegex nonWordRegex = new NonWordRegex();
			this.Response.AddHeader("content-disposition", "attachment; filename=" + nonWordRegex.Replace(text, "") + ".WebPart");
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(this.Response.Output))
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument();
				currentWebPartManager.ExportWebPart(webPart, xmlTextWriter);
				xmlTextWriter.WriteEndDocument();
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0006D1B8 File Offset: 0x0006B3B8
		private void InitializeWriter(HtmlTextWriter writer)
		{
			Html32TextWriter html32TextWriter = writer as Html32TextWriter;
			if (html32TextWriter != null && this.Request.Browser != null)
			{
				html32TextWriter.ShouldPerformDivTableSubstitution = this.Request.Browser.Tables;
			}
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0006D1F2 File Offset: 0x0006B3F2
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.InitializeWriter(writer);
			base.Render(writer);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0006D204 File Offset: 0x0006B404
		private void PrepareCallback(string callbackControlID)
		{
			this.Response.Cache.SetNoStore();
			try
			{
				string eventArgument = this._requestValueCollection["__CALLBACKPARAM"];
				this._callbackControl = (this.FindControl(callbackControlID) as ICallbackEventHandler);
				if (this._callbackControl == null)
				{
					throw new InvalidOperationException(SR.GetString("Page_CallBackTargetInvalid", new object[]
					{
						callbackControlID
					}));
				}
				this._callbackControl.RaiseCallbackEvent(eventArgument);
			}
			catch (Exception ex)
			{
				this.Response.Clear();
				this.Response.Write('e');
				if (this.Context.IsCustomErrorEnabled)
				{
					this.Response.Write(SR.GetString("Page_CallBackError"));
				}
				else
				{
					bool flag = !string.IsNullOrEmpty(this._requestValueCollection["__CALLBACKLOADSCRIPT"]);
					this.Response.Write(flag ? Util.QuoteJScriptString(HttpUtility.HtmlEncode(ex.Message)) : HttpUtility.HtmlEncode(ex.Message));
				}
			}
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0006D30C File Offset: 0x0006B50C
		private Task PrepareCallbackAsync(string callbackControlID)
		{
			Page.<PrepareCallbackAsync>d__530 <PrepareCallbackAsync>d__;
			<PrepareCallbackAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PrepareCallbackAsync>d__.<>4__this = this;
			<PrepareCallbackAsync>d__.callbackControlID = callbackControlID;
			<PrepareCallbackAsync>d__.<>1__state = -1;
			<PrepareCallbackAsync>d__.<>t__builder.Start<Page.<PrepareCallbackAsync>d__530>(ref <PrepareCallbackAsync>d__);
			return <PrepareCallbackAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0006D358 File Offset: 0x0006B558
		private void RenderCallback()
		{
			bool flag = !string.IsNullOrEmpty(this._requestValueCollection["__CALLBACKLOADSCRIPT"]);
			try
			{
				string text = null;
				if (flag)
				{
					text = this._requestValueCollection["__CALLBACKINDEX"];
					if (string.IsNullOrEmpty(text))
					{
						throw new HttpException(SR.GetString("Page_CallBackInvalid"));
					}
					foreach (char c in text)
					{
						if (c < '0' || c > '9')
						{
							throw new HttpException(SR.GetString("Page_CallBackInvalid"));
						}
					}
					this.Response.Write("<script>parent.__pendingCallbacks[");
					this.Response.Write(text);
					this.Response.Write("].xmlRequest.responseText=\"");
				}
				if (this._callbackControl != null)
				{
					string callbackResult = this._callbackControl.GetCallbackResult();
					if (this.EnableEventValidation)
					{
						string eventValidationFieldValue = this.ClientScript.GetEventValidationFieldValue();
						this.Response.Write(eventValidationFieldValue.Length.ToString(CultureInfo.InvariantCulture));
						this.Response.Write('|');
						this.Response.Write(eventValidationFieldValue);
					}
					else
					{
						this.Response.Write('s');
					}
					this.Response.Write(flag ? Util.QuoteJScriptString(callbackResult) : callbackResult);
				}
				if (flag)
				{
					this.Response.Write("\";parent.__pendingCallbacks[");
					this.Response.Write(text);
					this.Response.Write("].xmlRequest.readyState=4;parent.WebForm_CallbackComplete();</script>");
				}
			}
			catch (Exception ex)
			{
				this.Response.Clear();
				this.Response.Write('e');
				if (this.Context.IsCustomErrorEnabled)
				{
					this.Response.Write(SR.GetString("Page_CallBackError"));
				}
				else
				{
					this.Response.Write(flag ? Util.QuoteJScriptString(HttpUtility.HtmlEncode(ex.Message)) : HttpUtility.HtmlEncode(ex.Message));
				}
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0006D558 File Offset: 0x0006B758
		private bool RenderDivAroundHiddenInputs(HtmlTextWriter writer)
		{
			return writer.RenderDivAroundHiddenInputs && (!base.EnableLegacyRendering || this.RenderingCompatibility >= VersionUtil.Framework40);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0006D57E File Offset: 0x0006B77E
		internal void SetForm(HtmlForm form)
		{
			this._form = form;
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0006D587 File Offset: 0x0006B787
		internal void SetPostFormRenderDelegate(RenderMethod renderMethod)
		{
			this._postFormRenderDelegate = renderMethod;
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0006D590 File Offset: 0x0006B790
		public HtmlForm Form
		{
			get
			{
				return this._form;
			}
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0006D598 File Offset: 0x0006B798
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RegisterViewStateHandler()
		{
			this._needToPersistViewState = true;
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0006D5A4 File Offset: 0x0006B7A4
		private void SaveAllState()
		{
			if (!this._needToPersistViewState)
			{
				return;
			}
			Pair pair = new Pair();
			IDictionary dictionary = null;
			if (this._registeredControlsRequiringControlState != null && this._registeredControlsRequiringControlState.Count > 0)
			{
				dictionary = new HybridDictionary(this._registeredControlsRequiringControlState.Count + 1);
				foreach (object obj in ((IEnumerable)this._registeredControlsRequiringControlState))
				{
					Control control = (Control)obj;
					object obj2 = control.SaveControlStateInternal();
					if (dictionary[control.UniqueID] == null && obj2 != null)
					{
						dictionary.Add(control.UniqueID, obj2);
					}
				}
			}
			if (this._registeredControlsThatRequirePostBack != null && this._registeredControlsThatRequirePostBack.Count > 0)
			{
				if (dictionary == null)
				{
					dictionary = new HybridDictionary();
				}
				dictionary.Add("__ControlsRequirePostBackKey__", this._registeredControlsThatRequirePostBack);
			}
			if (dictionary != null && dictionary.Count > 0)
			{
				pair.First = dictionary;
			}
			ViewStateMode viewStateMode = this.ViewStateMode;
			if (viewStateMode == ViewStateMode.Inherit)
			{
				viewStateMode = ViewStateMode.Enabled;
			}
			Pair pair2 = new Pair(this.GetTypeHashCode().ToString(NumberFormatInfo.InvariantInfo), base.SaveViewStateRecursive(viewStateMode));
			if (this.Context.TraceIsEnabled)
			{
				int viewstateSize = 0;
				if (pair2.Second is Pair)
				{
					viewstateSize = base.EstimateStateSize(((Pair)pair2.Second).First);
				}
				else if (pair2.Second is Triplet)
				{
					viewstateSize = base.EstimateStateSize(((Triplet)pair2.Second).First);
				}
				this.Trace.AddControlStateSize(this.UniqueID, viewstateSize, (dictionary == null) ? 0 : base.EstimateStateSize(dictionary[this.UniqueID]));
			}
			pair.Second = pair2;
			this.SavePageStateToPersistenceMedium(pair);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0006D76C File Offset: 0x0006B96C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal virtual void SavePageStateToPersistenceMedium(object state)
		{
			PageStatePersister pageStatePersister = this.PageStatePersister;
			if (state is Pair)
			{
				Pair pair = (Pair)state;
				pageStatePersister.ControlState = pair.First;
				pageStatePersister.ViewState = pair.Second;
			}
			else
			{
				pageStatePersister.ViewState = state;
			}
			pageStatePersister.Save();
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0006D7B6 File Offset: 0x0006B9B6
		private void SetIntrinsics(HttpContext context)
		{
			this.SetIntrinsics(context, false);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0006D7C0 File Offset: 0x0006B9C0
		private void SetIntrinsics(HttpContext context, bool allowAsync)
		{
			this._context = context;
			this._request = context.Request;
			this._response = context.Response;
			this._application = context.Application;
			this._cache = context.Cache;
			if (!allowAsync && this._context != null && this._context.ApplicationInstance != null)
			{
				this._context.SyncContext.Disable();
			}
			if (!string.IsNullOrEmpty(this._clientTarget))
			{
				this._request.ClientTarget = this._clientTarget;
			}
			HttpCapabilitiesBase browser = this._request.Browser;
			if (browser != null)
			{
				this._response.ContentType = browser.PreferredRenderingMime;
				string preferredResponseEncoding = browser.PreferredResponseEncoding;
				string preferredRequestEncoding = browser.PreferredRequestEncoding;
				if (!string.IsNullOrEmpty(preferredResponseEncoding))
				{
					this._response.ContentEncoding = Encoding.GetEncoding(preferredResponseEncoding);
				}
				if (!string.IsNullOrEmpty(preferredRequestEncoding))
				{
					this._request.ContentEncoding = Encoding.GetEncoding(preferredRequestEncoding);
				}
			}
			base.HookUpAutomaticHandlers();
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0006D8B0 File Offset: 0x0006BAB0
		internal void SetHeader(HtmlHead header)
		{
			this._header = header;
			if (!string.IsNullOrEmpty(this._titleToBeSet))
			{
				if (this._header == null)
				{
					throw new InvalidOperationException(SR.GetString("Page_Title_Requires_Head"));
				}
				this.Title = this._titleToBeSet;
				this._titleToBeSet = null;
			}
			if (!string.IsNullOrEmpty(this._descriptionToBeSet))
			{
				if (this._header == null)
				{
					throw new InvalidOperationException(SR.GetString("Page_Description_Requires_Head"));
				}
				this.MetaDescription = this._descriptionToBeSet;
				this._descriptionToBeSet = null;
			}
			if (!string.IsNullOrEmpty(this._keywordsToBeSet))
			{
				if (this._header == null)
				{
					throw new InvalidOperationException(SR.GetString("Page_Description_Requires_Head"));
				}
				this.MetaKeywords = this._keywordsToBeSet;
				this._keywordsToBeSet = null;
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0006D96C File Offset: 0x0006BB6C
		internal override void UnloadRecursive(bool dispose)
		{
			base.UnloadRecursive(dispose);
			if (this._previousPage != null && this._previousPage.IsCrossPagePostBack)
			{
				this._previousPage.UnloadRecursive(dispose);
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0006D996 File Offset: 0x0006BB96
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected IAsyncResult AspCompatBeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
		{
			this.SetIntrinsics(context);
			this._aspCompatStep = new AspCompatApplicationStep(context, new AspCompatCallback(this.ProcessRequest));
			return this._aspCompatStep.BeginAspCompatExecution(cb, extraData);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0006D9C4 File Offset: 0x0006BBC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void AspCompatEndProcessRequest(IAsyncResult result)
		{
			this._aspCompatStep.EndAspCompatExecution(result);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0006D9D4 File Offset: 0x0006BBD4
		public void ExecuteRegisteredAsyncTasks()
		{
			if (this._legacyAsyncTaskManager == null)
			{
				return;
			}
			if (this._legacyAsyncTaskManager.TaskExecutionInProgress)
			{
				return;
			}
			HttpAsyncResult httpAsyncResult = this._legacyAsyncTaskManager.ExecuteTasks(null, null);
			if (httpAsyncResult.Error != null)
			{
				throw new HttpException(null, httpAsyncResult.Error);
			}
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0006DA1C File Offset: 0x0006BC1C
		private void CheckRemainingAsyncTasks(bool isThreadAbort)
		{
			if (this._legacyAsyncTaskManager != null)
			{
				this._legacyAsyncTaskManager.DisposeTimer();
				if (isThreadAbort)
				{
					this._legacyAsyncTaskManager.CompleteAllTasksNow(true);
					return;
				}
				if (!this._legacyAsyncTaskManager.FailedToStartTasks && this._legacyAsyncTaskManager.AnyTasksRemain)
				{
					throw new HttpException(SR.GetString("Registered_async_tasks_remain"));
				}
			}
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0006DA78 File Offset: 0x0006BC78
		public void RegisterAsyncTask(PageAsyncTask task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			if (SynchronizationContextUtil.CurrentMode == SynchronizationContextMode.Legacy)
			{
				if (this._legacyAsyncTaskManager == null)
				{
					this._legacyAsyncTaskManager = new LegacyPageAsyncTaskManager(this);
				}
				LegacyPageAsyncTask task2 = new LegacyPageAsyncTask(task.BeginHandler, task.EndHandler, task.TimeoutHandler, task.State, task.ExecuteInParallel);
				this._legacyAsyncTaskManager.AddTask(task2);
				return;
			}
			if (!(this is IHttpAsyncHandler))
			{
				throw new InvalidOperationException(SR.GetString("Async_required"));
			}
			if (this._asyncTaskManager == null)
			{
				this._asyncTaskManager = new PageAsyncTaskManager();
			}
			IPageAsyncTask pageAsyncTask2;
			if (task.TaskHandler == null)
			{
				IPageAsyncTask pageAsyncTask = new PageAsyncTaskApm(task.BeginHandler, task.EndHandler, task.State);
				pageAsyncTask2 = pageAsyncTask;
			}
			else
			{
				IPageAsyncTask pageAsyncTask = new PageAsyncTaskTap(task.TaskHandler);
				pageAsyncTask2 = pageAsyncTask;
			}
			IPageAsyncTask task3 = pageAsyncTask2;
			this._asyncTaskManager.EnqueueTask(task3);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0006DB48 File Offset: 0x0006BD48
		private void AsyncPageProcessRequestBeforeAsyncPointCancellableCallback(object state)
		{
			this.ProcessRequest(true, false);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0006DB54 File Offset: 0x0006BD54
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected IAsyncResult AsyncPageBeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
		{
			if (SynchronizationContextUtil.CurrentMode == SynchronizationContextMode.Legacy)
			{
				return this.LegacyAsyncPageBeginProcessRequest(context, callback, extraData);
			}
			return TaskAsyncHelper.BeginTask(() => this.ProcessRequestAsync(context), callback, extraData);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0006DBA0 File Offset: 0x0006BDA0
		internal CancellationTokenSource CreateCancellationTokenFromAsyncTimeout()
		{
			TimeSpan asyncTimeout = this.AsyncTimeout;
			if (!(asyncTimeout <= Page._maxAsyncTimeout))
			{
				return new CancellationTokenSource();
			}
			return new CancellationTokenSource(asyncTimeout);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0006DBD0 File Offset: 0x0006BDD0
		private Task ProcessRequestAsync(HttpContext context)
		{
			Page.<ProcessRequestAsync>d__554 <ProcessRequestAsync>d__;
			<ProcessRequestAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessRequestAsync>d__.<>4__this = this;
			<ProcessRequestAsync>d__.context = context;
			<ProcessRequestAsync>d__.<>1__state = -1;
			<ProcessRequestAsync>d__.<>t__builder.Start<Page.<ProcessRequestAsync>d__554>(ref <ProcessRequestAsync>d__);
			return <ProcessRequestAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0006DC1C File Offset: 0x0006BE1C
		private IAsyncResult LegacyAsyncPageBeginProcessRequest(HttpContext context, AsyncCallback callback, object extraData)
		{
			this.SetIntrinsics(context, true);
			if (this._legacyAsyncInfo == null)
			{
				this._legacyAsyncInfo = new Page.LegacyPageAsyncInfo(this);
			}
			this._legacyAsyncInfo.AsyncResult = new HttpAsyncResult(callback, extraData);
			this._legacyAsyncInfo.CallerIsBlocking = (callback == null);
			try
			{
				this._context.InvokeCancellableCallback(new WaitCallback(this.AsyncPageProcessRequestBeforeAsyncPointCancellableCallback), null);
			}
			catch (Exception error)
			{
				if (this._context.SyncContext.PendingOperationsCount == 0)
				{
					throw;
				}
				this._legacyAsyncInfo.SetError(error);
			}
			if (this._legacyAsyncTaskManager != null && !this._legacyAsyncInfo.CallerIsBlocking)
			{
				this._legacyAsyncTaskManager.RegisterHandlersForPagePreRenderCompleteAsync();
			}
			this._legacyAsyncInfo.AsyncPointReached = true;
			this._context.SyncContext.Disable();
			this._legacyAsyncInfo.CallHandlers(true);
			return this._legacyAsyncInfo.AsyncResult;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0006DD08 File Offset: 0x0006BF08
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void AsyncPageEndProcessRequest(IAsyncResult result)
		{
			if (SynchronizationContextUtil.CurrentMode == SynchronizationContextMode.Legacy)
			{
				this.LegacyAsyncPageEndProcessRequest(result);
				return;
			}
			TaskAsyncHelper.EndTask(result);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0006DD20 File Offset: 0x0006BF20
		private void LegacyAsyncPageEndProcessRequest(IAsyncResult result)
		{
			if (this._legacyAsyncInfo == null)
			{
				return;
			}
			this._legacyAsyncInfo.AsyncResult.End();
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0006DD3C File Offset: 0x0006BF3C
		public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler)
		{
			this.AddOnPreRenderCompleteAsync(beginHandler, endHandler, null);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0006DD48 File Offset: 0x0006BF48
		public void AddOnPreRenderCompleteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
		{
			if (beginHandler == null)
			{
				throw new ArgumentNullException("beginHandler");
			}
			if (endHandler == null)
			{
				throw new ArgumentNullException("endHandler");
			}
			if (SynchronizationContextUtil.CurrentMode == SynchronizationContextMode.Normal)
			{
				this.RegisterAsyncTask(new PageAsyncTask(beginHandler, endHandler, null, state));
				return;
			}
			if (this._legacyAsyncInfo == null)
			{
				if (!(this is IHttpAsyncHandler))
				{
					throw new InvalidOperationException(SR.GetString("Async_required"));
				}
				this._legacyAsyncInfo = new Page.LegacyPageAsyncInfo(this);
			}
			if (this._legacyAsyncInfo.AsyncPointReached)
			{
				throw new InvalidOperationException(SR.GetString("Async_addhandler_too_late"));
			}
			this._legacyAsyncInfo.AddHandler(beginHandler, endHandler, state);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0006DDE4 File Offset: 0x0006BFE4
		public virtual void Validate()
		{
			this._validated = true;
			if (this._validators != null)
			{
				for (int i = 0; i < this.Validators.Count; i++)
				{
					this.Validators[i].Validate();
				}
			}
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0006DE28 File Offset: 0x0006C028
		public virtual void Validate(string validationGroup)
		{
			this._validated = true;
			if (this._validators != null)
			{
				ValidatorCollection validators = this.GetValidators(validationGroup);
				if (string.IsNullOrEmpty(validationGroup) && this._validators.Count == validators.Count)
				{
					this.Validate();
					return;
				}
				for (int i = 0; i < validators.Count; i++)
				{
					validators[i].Validate();
				}
			}
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0006DE8C File Offset: 0x0006C08C
		public ValidatorCollection GetValidators(string validationGroup)
		{
			if (validationGroup == null)
			{
				validationGroup = string.Empty;
			}
			ValidatorCollection validatorCollection = new ValidatorCollection();
			if (this._validators != null)
			{
				for (int i = 0; i < this.Validators.Count; i++)
				{
					BaseValidator baseValidator = this.Validators[i] as BaseValidator;
					if (baseValidator != null)
					{
						if (string.Compare(baseValidator.ValidationGroup, validationGroup, StringComparison.Ordinal) == 0)
						{
							validatorCollection.Add(baseValidator);
						}
					}
					else if (validationGroup.Length == 0)
					{
						validatorCollection.Add(this.Validators[i]);
					}
				}
			}
			return validatorCollection;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0006DF10 File Offset: 0x0006C110
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void VerifyRenderingInServerForm(Control control)
		{
			if (this.Context == null || base.DesignMode)
			{
				return;
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!this._inOnFormRender && !this.IsCallback)
			{
				throw new HttpException(SR.GetString("ControlRenderedOutsideServerForm", new object[]
				{
					control.ClientID,
					control.GetType().Name
				}));
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x0006DF79 File Offset: 0x0006C179
		public PageAdapter PageAdapter
		{
			get
			{
				if (this._pageAdapter == null)
				{
					this.ResolveAdapter();
					this._pageAdapter = (PageAdapter)base.AdapterInternal;
				}
				return this._pageAdapter;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0006DFA4 File Offset: 0x0006C1A4
		internal string RelativeFilePath
		{
			get
			{
				if (this._relativeFilePath == null)
				{
					string text = this.Context.Request.CurrentExecutionFilePath;
					string filePath = this.Context.Request.FilePath;
					if (filePath.Equals(text))
					{
						int num = text.LastIndexOf('/');
						if (num >= 0)
						{
							text = text.Substring(num + 1);
						}
						this._relativeFilePath = text;
					}
					else
					{
						this._relativeFilePath = this.Server.UrlDecode(UrlPath.MakeRelative(filePath, text));
					}
				}
				return this._relativeFilePath;
			}
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x0006E022 File Offset: 0x0006C222
		internal bool GetDesignModeInternal()
		{
			if (!this._designModeChecked)
			{
				this._designMode = (base.Site != null && base.Site.DesignMode);
				this._designModeChecked = true;
			}
			return this._designMode;
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0006E055 File Offset: 0x0006C255
		[Browsable(false)]
		public IDictionary Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new HybridDictionary();
				}
				return this._items;
			}
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x0006E070 File Offset: 0x0006C270
		internal void PushDataBindingContext(object dataItem)
		{
			if (this._dataBindingContext == null)
			{
				this._dataBindingContext = new Stack();
			}
			this._dataBindingContext.Push(dataItem);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0006E091 File Offset: 0x0006C291
		internal void PopDataBindingContext()
		{
			this._dataBindingContext.Pop();
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0006E09F File Offset: 0x0006C29F
		public object GetDataItem()
		{
			if (this._dataBindingContext == null || this._dataBindingContext.Count == 0)
			{
				throw new InvalidOperationException(SR.GetString("Page_MissingDataBindingContext"));
			}
			return this._dataBindingContext.Peek();
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0006E0D1 File Offset: 0x0006C2D1
		internal static bool IsSystemPostField(string field)
		{
			return Page.s_systemPostFields.Contains(field);
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0006E0DE File Offset: 0x0006C2DE
		internal IScriptManager ScriptManager
		{
			get
			{
				return (IScriptManager)this.Items[typeof(IScriptManager)];
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0006E0FC File Offset: 0x0006C2FC
		private void ValidateRawUrlIfRequired()
		{
			bool flag = !this.SkipFormActionValidation && base.CalculateEffectiveValidateRequest();
			if (flag)
			{
				string rawUrl = this._request.RawUrl;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0006E12C File Offset: 0x0006C32C
		internal bool IsPartialRenderingSupported
		{
			get
			{
				if (!this._pageFlags[32])
				{
					Type scriptManagerType = this.ScriptManagerType;
					if (scriptManagerType != null)
					{
						object obj = this.Page.Items[scriptManagerType];
						if (obj != null)
						{
							PropertyInfo property = scriptManagerType.GetProperty("SupportsPartialRendering");
							if (property != null)
							{
								object value = property.GetValue(obj, null);
								this._pageFlags[16] = (bool)value;
							}
						}
					}
					this._pageFlags[32] = true;
				}
				return this._pageFlags[16];
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0006E1B9 File Offset: 0x0006C3B9
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x0006E1DD File Offset: 0x0006C3DD
		internal Type ScriptManagerType
		{
			get
			{
				if (Page._scriptManagerType == null)
				{
					Page._scriptManagerType = BuildManager.GetType("System.Web.UI.ScriptManager", false);
				}
				return Page._scriptManagerType;
			}
			set
			{
				Page._scriptManagerType = value;
			}
		}

		// Token: 0x04001B37 RID: 6967
		private const string HiddenClassName = "aspNetHidden";

		// Token: 0x04001B38 RID: 6968
		private const string PageID = "__Page";

		// Token: 0x04001B39 RID: 6969
		private const string PageScrollPositionScriptKey = "PageScrollPositionScript";

		// Token: 0x04001B3A RID: 6970
		private const string PageSubmitScriptKey = "PageSubmitScript";

		// Token: 0x04001B3B RID: 6971
		private const string PageReEnableControlsScriptKey = "PageReEnableControlsScript";

		// Token: 0x04001B3C RID: 6972
		private const string PageRegisteredControlsThatRequirePostBackKey = "__ControlsRequirePostBackKey__";

		// Token: 0x04001B3D RID: 6973
		private const string EnabledControlArray = "__enabledControlArray";

		// Token: 0x04001B3E RID: 6974
		internal static readonly object EventPreRenderComplete = new object();

		// Token: 0x04001B3F RID: 6975
		internal static readonly object EventPreLoad = new object();

		// Token: 0x04001B40 RID: 6976
		internal static readonly object EventLoadComplete = new object();

		// Token: 0x04001B41 RID: 6977
		internal static readonly object EventPreInit = new object();

		// Token: 0x04001B42 RID: 6978
		internal static readonly object EventInitComplete = new object();

		// Token: 0x04001B43 RID: 6979
		internal static readonly object EventSaveStateComplete = new object();

		// Token: 0x04001B44 RID: 6980
		private static readonly Version FocusMinimumEcmaVersion = new Version("1.4");

		// Token: 0x04001B45 RID: 6981
		private static readonly Version FocusMinimumJScriptVersion = new Version("3.0");

		// Token: 0x04001B46 RID: 6982
		private static readonly Version JavascriptMinimumVersion = new Version("1.0");

		// Token: 0x04001B47 RID: 6983
		private static readonly Version MSDomScrollMinimumVersion = new Version("4.0");

		// Token: 0x04001B48 RID: 6984
		private static readonly string UniqueFilePathSuffixID = "__ufps";

		// Token: 0x04001B49 RID: 6985
		private string _uniqueFilePathSuffix;

		// Token: 0x04001B4A RID: 6986
		internal static readonly int DefaultMaxPageStateFieldLength = -1;

		// Token: 0x04001B4B RID: 6987
		internal static readonly int DefaultAsyncTimeoutSeconds = 45;

		// Token: 0x04001B4C RID: 6988
		private int _maxPageStateFieldLength = Page.DefaultMaxPageStateFieldLength;

		// Token: 0x04001B4D RID: 6989
		private string _requestViewState;

		// Token: 0x04001B4E RID: 6990
		private bool _cachedRequestViewState;

		// Token: 0x04001B4F RID: 6991
		private PageAdapter _pageAdapter;

		// Token: 0x04001B50 RID: 6992
		private bool _fPageLayoutChanged;

		// Token: 0x04001B51 RID: 6993
		private bool _haveIdSeparator;

		// Token: 0x04001B52 RID: 6994
		private char _idSeparator;

		// Token: 0x04001B53 RID: 6995
		private bool _sessionRetrieved;

		// Token: 0x04001B54 RID: 6996
		private HttpSessionState _session;

		// Token: 0x04001B55 RID: 6997
		private int _transactionMode;

		// Token: 0x04001B56 RID: 6998
		private bool _aspCompatMode;

		// Token: 0x04001B57 RID: 6999
		private bool _asyncMode;

		// Token: 0x04001B58 RID: 7000
		private static readonly TimeSpan _maxAsyncTimeout = TimeSpan.FromMilliseconds(2147483647.0);

		// Token: 0x04001B59 RID: 7001
		private TimeSpan _asyncTimeout;

		// Token: 0x04001B5A RID: 7002
		private bool _asyncTimeoutSet;

		// Token: 0x04001B5B RID: 7003
		private PageAsyncTaskManager _asyncTaskManager;

		// Token: 0x04001B5C RID: 7004
		private LegacyPageAsyncTaskManager _legacyAsyncTaskManager;

		// Token: 0x04001B5D RID: 7005
		private Page.LegacyPageAsyncInfo _legacyAsyncInfo;

		// Token: 0x04001B5E RID: 7006
		private CultureInfo _dynamicCulture;

		// Token: 0x04001B5F RID: 7007
		private CultureInfo _dynamicUICulture;

		// Token: 0x04001B60 RID: 7008
		private string _clientState;

		// Token: 0x04001B61 RID: 7009
		private PageStatePersister _persister;

		// Token: 0x04001B62 RID: 7010
		internal ControlSet _registeredControlsRequiringControlState;

		// Token: 0x04001B63 RID: 7011
		private StringSet _controlStateLoadedControlIds;

		// Token: 0x04001B64 RID: 7012
		internal HybridDictionary _registeredControlsRequiringClearChildControlState;

		// Token: 0x04001B65 RID: 7013
		internal const ViewStateEncryptionMode EncryptionModeDefault = ViewStateEncryptionMode.Auto;

		// Token: 0x04001B66 RID: 7014
		private ViewStateEncryptionMode _encryptionMode;

		// Token: 0x04001B67 RID: 7015
		private bool _viewStateEncryptionRequested;

		// Token: 0x04001B68 RID: 7016
		private ArrayList _enabledControls;

		// Token: 0x04001B69 RID: 7017
		internal HttpRequest _request;

		// Token: 0x04001B6A RID: 7018
		internal HttpResponse _response;

		// Token: 0x04001B6B RID: 7019
		internal HttpApplicationState _application;

		// Token: 0x04001B6C RID: 7020
		internal Cache _cache;

		// Token: 0x04001B6D RID: 7021
		internal string _errorPage;

		// Token: 0x04001B6E RID: 7022
		private string _clientTarget;

		// Token: 0x04001B6F RID: 7023
		private HtmlForm _form;

		// Token: 0x04001B70 RID: 7024
		private bool _inOnFormRender;

		// Token: 0x04001B71 RID: 7025
		private bool _fOnFormRenderCalled;

		// Token: 0x04001B72 RID: 7026
		private bool _fRequireWebFormsScript;

		// Token: 0x04001B73 RID: 7027
		private bool _fWebFormsScriptRendered;

		// Token: 0x04001B74 RID: 7028
		private bool _fRequirePostBackScript;

		// Token: 0x04001B75 RID: 7029
		private bool _fPostBackScriptRendered;

		// Token: 0x04001B76 RID: 7030
		private bool _containsCrossPagePost;

		// Token: 0x04001B77 RID: 7031
		private RenderMethod _postFormRenderDelegate;

		// Token: 0x04001B78 RID: 7032
		internal Dictionary<string, string> _hiddenFieldsToRender;

		// Token: 0x04001B79 RID: 7033
		private bool _requireFocusScript;

		// Token: 0x04001B7A RID: 7034
		private bool _profileTreeBuilt;

		// Token: 0x04001B7B RID: 7035
		internal const bool MaintainScrollPositionOnPostBackDefault = false;

		// Token: 0x04001B7C RID: 7036
		private bool _maintainScrollPosition;

		// Token: 0x04001B7D RID: 7037
		private ClientScriptManager _clientScriptManager;

		// Token: 0x04001B7E RID: 7038
		private static Type _scriptManagerType;

		// Token: 0x04001B7F RID: 7039
		internal const bool EnableViewStateMacDefault = true;

		// Token: 0x04001B80 RID: 7040
		internal const bool EnableEventValidationDefault = true;

		// Token: 0x04001B81 RID: 7041
		internal const string systemPostFieldPrefix = "__";

		// Token: 0x04001B82 RID: 7042
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const string postEventSourceID = "__EVENTTARGET";

		// Token: 0x04001B83 RID: 7043
		private const string lastFocusID = "__LASTFOCUS";

		// Token: 0x04001B84 RID: 7044
		private const string _scrollPositionXID = "__SCROLLPOSITIONX";

		// Token: 0x04001B85 RID: 7045
		private const string _scrollPositionYID = "__SCROLLPOSITIONY";

		// Token: 0x04001B86 RID: 7046
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const string postEventArgumentID = "__EVENTARGUMENT";

		// Token: 0x04001B87 RID: 7047
		internal const string ViewStateFieldPrefixID = "__VIEWSTATE";

		// Token: 0x04001B88 RID: 7048
		internal const string ViewStateFieldCountID = "__VIEWSTATEFIELDCOUNT";

		// Token: 0x04001B89 RID: 7049
		internal const string ViewStateGeneratorFieldID = "__VIEWSTATEGENERATOR";

		// Token: 0x04001B8A RID: 7050
		internal const string ViewStateEncryptionID = "__VIEWSTATEENCRYPTED";

		// Token: 0x04001B8B RID: 7051
		internal const string EventValidationPrefixID = "__EVENTVALIDATION";

		// Token: 0x04001B8C RID: 7052
		internal const string WebPartExportID = "__WEBPARTEXPORT";

		// Token: 0x04001B8D RID: 7053
		private bool _requireScrollScript;

		// Token: 0x04001B8E RID: 7054
		private bool _isCallback;

		// Token: 0x04001B8F RID: 7055
		private bool _isCrossPagePostBack;

		// Token: 0x04001B90 RID: 7056
		private bool _containsEncryptedViewState;

		// Token: 0x04001B91 RID: 7057
		private bool _enableEventValidation = true;

		// Token: 0x04001B92 RID: 7058
		internal const string callbackID = "__CALLBACKID";

		// Token: 0x04001B93 RID: 7059
		internal const string callbackParameterID = "__CALLBACKPARAM";

		// Token: 0x04001B94 RID: 7060
		internal const string callbackLoadScriptID = "__CALLBACKLOADSCRIPT";

		// Token: 0x04001B95 RID: 7061
		internal const string callbackIndexID = "__CALLBACKINDEX";

		// Token: 0x04001B96 RID: 7062
		internal const string previousPageID = "__PREVIOUSPAGE";

		// Token: 0x04001B97 RID: 7063
		private Stack _partialCachingControlStack;

		// Token: 0x04001B98 RID: 7064
		private ArrayList _controlsRequiringPostBack;

		// Token: 0x04001B99 RID: 7065
		private ArrayList _registeredControlsThatRequirePostBack;

		// Token: 0x04001B9A RID: 7066
		private NameValueCollection _leftoverPostData;

		// Token: 0x04001B9B RID: 7067
		private IPostBackEventHandler _registeredControlThatRequireRaiseEvent;

		// Token: 0x04001B9C RID: 7068
		private ArrayList _changedPostDataConsumers;

		// Token: 0x04001B9D RID: 7069
		private bool _needToPersistViewState;

		// Token: 0x04001B9E RID: 7070
		private bool _enableViewStateMac;

		// Token: 0x04001B9F RID: 7071
		private string _viewStateUserKey;

		// Token: 0x04001BA0 RID: 7072
		private string _themeName;

		// Token: 0x04001BA1 RID: 7073
		private PageTheme _theme;

		// Token: 0x04001BA2 RID: 7074
		private string _styleSheetName;

		// Token: 0x04001BA3 RID: 7075
		private PageTheme _styleSheet;

		// Token: 0x04001BA4 RID: 7076
		private VirtualPath _masterPageFile;

		// Token: 0x04001BA5 RID: 7077
		private MasterPage _master;

		// Token: 0x04001BA6 RID: 7078
		private IDictionary _contentTemplateCollection;

		// Token: 0x04001BA7 RID: 7079
		private SmartNavigationSupport _smartNavSupport;

		// Token: 0x04001BA8 RID: 7080
		internal HttpContext _context;

		// Token: 0x04001BA9 RID: 7081
		private ValidatorCollection _validators;

		// Token: 0x04001BAA RID: 7082
		private bool _validated;

		// Token: 0x04001BAB RID: 7083
		private HtmlHead _header;

		// Token: 0x04001BAC RID: 7084
		private int _supportsStyleSheets;

		// Token: 0x04001BAD RID: 7085
		private Control _autoPostBackControl;

		// Token: 0x04001BAE RID: 7086
		private string _focusedControlID;

		// Token: 0x04001BAF RID: 7087
		private Control _focusedControl;

		// Token: 0x04001BB0 RID: 7088
		private string _validatorInvalidControl;

		// Token: 0x04001BB1 RID: 7089
		private int _scrollPositionX;

		// Token: 0x04001BB2 RID: 7090
		private int _scrollPositionY;

		// Token: 0x04001BB3 RID: 7091
		private Page _previousPage;

		// Token: 0x04001BB4 RID: 7092
		private VirtualPath _previousPagePath;

		// Token: 0x04001BB5 RID: 7093
		private bool _preInitWorkComplete;

		// Token: 0x04001BB6 RID: 7094
		private bool _clientSupportsJavaScriptChecked;

		// Token: 0x04001BB7 RID: 7095
		private bool _clientSupportsJavaScript;

		// Token: 0x04001BB8 RID: 7096
		private string _titleToBeSet;

		// Token: 0x04001BB9 RID: 7097
		private string _descriptionToBeSet;

		// Token: 0x04001BBA RID: 7098
		private string _keywordsToBeSet;

		// Token: 0x04001BBB RID: 7099
		private ICallbackEventHandler _callbackControl;

		// Token: 0x04001BBC RID: 7100
		private bool _xhtmlConformanceModeSet;

		// Token: 0x04001BBD RID: 7101
		private XhtmlConformanceMode _xhtmlConformanceMode;

		// Token: 0x04001BBE RID: 7102
		private const int styleSheetInitialized = 1;

		// Token: 0x04001BBF RID: 7103
		private const int isExportingWebPart = 2;

		// Token: 0x04001BC0 RID: 7104
		private const int isExportingWebPartShared = 4;

		// Token: 0x04001BC1 RID: 7105
		private const int isCrossPagePostRequest = 8;

		// Token: 0x04001BC2 RID: 7106
		private const int isPartialRenderingSupported = 16;

		// Token: 0x04001BC3 RID: 7107
		private const int isPartialRenderingSupportedSet = 32;

		// Token: 0x04001BC4 RID: 7108
		private const int skipFormActionValidation = 64;

		// Token: 0x04001BC5 RID: 7109
		private const int wasViewStateMacErrorSuppressed = 128;

		// Token: 0x04001BC6 RID: 7110
		private SimpleBitVector32 _pageFlags;

		// Token: 0x04001BC7 RID: 7111
		private NameValueCollection _requestValueCollection;

		// Token: 0x04001BC8 RID: 7112
		private NameValueCollection _unvalidatedRequestValueCollection;

		// Token: 0x04001BC9 RID: 7113
		private ModelStateDictionary _modelState;

		// Token: 0x04001BCA RID: 7114
		private ModelBindingExecutionContext _modelBindingExecutionContext;

		// Token: 0x04001BCB RID: 7115
		private UnobtrusiveValidationMode? _unobtrusiveValidationMode;

		// Token: 0x04001BCC RID: 7116
		private bool _executingAsyncTasks;

		// Token: 0x04001BCD RID: 7117
		private static StringSet s_systemPostFields;

		// Token: 0x04001BCF RID: 7119
		private string _clientQueryString;

		// Token: 0x04001BD0 RID: 7120
		private static char[] s_varySeparator = new char[]
		{
			';'
		};

		// Token: 0x04001BD1 RID: 7121
		internal const bool BufferDefault = true;

		// Token: 0x04001BD2 RID: 7122
		internal const bool SmartNavigationDefault = false;

		// Token: 0x04001BD3 RID: 7123
		private AspCompatApplicationStep _aspCompatStep;

		// Token: 0x04001BD4 RID: 7124
		private string _relativeFilePath;

		// Token: 0x04001BD5 RID: 7125
		private bool _designModeChecked;

		// Token: 0x04001BD6 RID: 7126
		private bool _designMode;

		// Token: 0x04001BD7 RID: 7127
		private IDictionary _items;

		// Token: 0x04001BD8 RID: 7128
		private Stack _dataBindingContext;

		// Token: 0x02000973 RID: 2419
		private class LegacyPageAsyncInfo
		{
			// Token: 0x06006A12 RID: 27154 RVA: 0x00178E84 File Offset: 0x00177084
			internal LegacyPageAsyncInfo(Page page)
			{
				this._page = page;
				this._app = page.Context.ApplicationInstance;
				this._syncContext = page.Context.SyncContext;
				this._completionCallback = new AsyncCallback(this.OnAsyncHandlerCompletion);
				this._callHandlersThreadpoolCallback = new WaitCallback(this.CallHandlersFromThreadpoolThread);
			}

			// Token: 0x17001D3B RID: 7483
			// (get) Token: 0x06006A13 RID: 27155 RVA: 0x00178EE4 File Offset: 0x001770E4
			// (set) Token: 0x06006A14 RID: 27156 RVA: 0x00178EEC File Offset: 0x001770EC
			internal HttpAsyncResult AsyncResult
			{
				get
				{
					return this._asyncResult;
				}
				set
				{
					this._asyncResult = value;
				}
			}

			// Token: 0x17001D3C RID: 7484
			// (get) Token: 0x06006A15 RID: 27157 RVA: 0x00178EF5 File Offset: 0x001770F5
			// (set) Token: 0x06006A16 RID: 27158 RVA: 0x00178EFD File Offset: 0x001770FD
			internal bool AsyncPointReached
			{
				get
				{
					return this._asyncPointReached;
				}
				set
				{
					this._asyncPointReached = value;
				}
			}

			// Token: 0x17001D3D RID: 7485
			// (get) Token: 0x06006A17 RID: 27159 RVA: 0x00178F06 File Offset: 0x00177106
			// (set) Token: 0x06006A18 RID: 27160 RVA: 0x00178F0E File Offset: 0x0017710E
			internal bool CallerIsBlocking
			{
				get
				{
					return this._callerIsBlocking;
				}
				set
				{
					this._callerIsBlocking = value;
				}
			}

			// Token: 0x06006A19 RID: 27161 RVA: 0x00178F18 File Offset: 0x00177118
			internal void AddHandler(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
			{
				if (this._handlerCount == 0)
				{
					this._beginHandlers = new ArrayList();
					this._endHandlers = new ArrayList();
					this._stateObjects = new ArrayList();
				}
				this._beginHandlers.Add(beginHandler);
				this._endHandlers.Add(endHandler);
				this._stateObjects.Add(state);
				this._handlerCount++;
			}

			// Token: 0x06006A1A RID: 27162 RVA: 0x00178F84 File Offset: 0x00177184
			internal void CallHandlers(bool onPageThread)
			{
				try
				{
					if (this.CallerIsBlocking || onPageThread)
					{
						this.CallHandlersPossiblyUnderLock(onPageThread);
					}
					else
					{
						HttpApplication app = this._app;
						lock (app)
						{
							this.CallHandlersPossiblyUnderLock(onPageThread);
						}
					}
				}
				catch (Exception ex)
				{
					this._error = ex;
					this._completed = true;
					this._asyncResult.Complete(onPageThread, null, this._error);
					if (!onPageThread && ex is ThreadAbortException && ((ThreadAbortException)ex).ExceptionState is HttpApplication.CancelModuleException)
					{
						Page.ThreadResetAbortWithAssert();
					}
				}
			}

			// Token: 0x06006A1B RID: 27163 RVA: 0x0017902C File Offset: 0x0017722C
			private void CallHandlersPossiblyUnderLock(bool onPageThread)
			{
				ThreadContext threadContext = null;
				if (!onPageThread)
				{
					threadContext = this._app.OnThreadEnter();
				}
				try
				{
					while (this._currentHandler < this._handlerCount && this._error == null)
					{
						try
						{
							IAsyncResult asyncResult = ((BeginEventHandler)this._beginHandlers[this._currentHandler])(this._page, EventArgs.Empty, this._completionCallback, this._stateObjects[this._currentHandler]);
							if (asyncResult == null)
							{
								throw new InvalidOperationException(SR.GetString("Async_null_asyncresult"));
							}
							if (!asyncResult.CompletedSynchronously)
							{
								return;
							}
							try
							{
								((EndEventHandler)this._endHandlers[this._currentHandler])(asyncResult);
							}
							finally
							{
								this._currentHandler++;
							}
						}
						catch (Exception ex)
						{
							if (onPageThread && this._syncContext.PendingOperationsCount == 0)
							{
								throw;
							}
							PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_DURING_REQUEST);
							PerfCounters.IncrementCounter(AppPerfCounter.ERRORS_TOTAL);
							try
							{
								if (!this._page.HandleError(ex))
								{
									this._error = ex;
								}
							}
							catch (Exception error)
							{
								this._error = error;
							}
						}
					}
					if (!this._syncContext.PendingCompletion(this._callHandlersThreadpoolCallback))
					{
						if (this._error == null && this._syncContext.Error != null)
						{
							try
							{
								if (!this._page.HandleError(this._syncContext.Error))
								{
									this._error = this._syncContext.Error;
									this._syncContext.ClearError();
								}
							}
							catch (Exception error2)
							{
								this._error = error2;
							}
						}
						try
						{
							this._page.Context.InvokeCancellableCallback(delegate(object o)
							{
								this._page.ProcessRequest(false, true);
							}, null);
						}
						catch (Exception error3)
						{
							if (onPageThread)
							{
								throw;
							}
							this._error = error3;
						}
						if (threadContext != null)
						{
							try
							{
								threadContext.DisassociateFromCurrentThread();
							}
							finally
							{
								threadContext = null;
							}
						}
						this._completed = true;
						this._asyncResult.Complete(onPageThread, null, this._error);
					}
				}
				finally
				{
					if (threadContext != null)
					{
						threadContext.DisassociateFromCurrentThread();
					}
				}
			}

			// Token: 0x06006A1C RID: 27164 RVA: 0x001792BC File Offset: 0x001774BC
			private void OnAsyncHandlerCompletion(IAsyncResult ar)
			{
				if (ar.CompletedSynchronously)
				{
					return;
				}
				try
				{
					((EndEventHandler)this._endHandlers[this._currentHandler])(ar);
				}
				catch (Exception error)
				{
					this._error = error;
				}
				if (this._completed)
				{
					return;
				}
				this._currentHandler++;
				if (Thread.CurrentThread.IsThreadPoolThread)
				{
					this.CallHandlers(false);
					return;
				}
				ThreadPool.QueueUserWorkItem(this._callHandlersThreadpoolCallback);
			}

			// Token: 0x06006A1D RID: 27165 RVA: 0x00179344 File Offset: 0x00177544
			private void CallHandlersFromThreadpoolThread(object data)
			{
				this.CallHandlers(false);
			}

			// Token: 0x06006A1E RID: 27166 RVA: 0x0017934D File Offset: 0x0017754D
			internal void SetError(Exception error)
			{
				this._error = error;
			}

			// Token: 0x04003862 RID: 14434
			private Page _page;

			// Token: 0x04003863 RID: 14435
			private bool _callerIsBlocking;

			// Token: 0x04003864 RID: 14436
			private HttpApplication _app;

			// Token: 0x04003865 RID: 14437
			private AspNetSynchronizationContextBase _syncContext;

			// Token: 0x04003866 RID: 14438
			private HttpAsyncResult _asyncResult;

			// Token: 0x04003867 RID: 14439
			private bool _asyncPointReached;

			// Token: 0x04003868 RID: 14440
			private int _handlerCount;

			// Token: 0x04003869 RID: 14441
			private ArrayList _beginHandlers;

			// Token: 0x0400386A RID: 14442
			private ArrayList _endHandlers;

			// Token: 0x0400386B RID: 14443
			private ArrayList _stateObjects;

			// Token: 0x0400386C RID: 14444
			private AsyncCallback _completionCallback;

			// Token: 0x0400386D RID: 14445
			private WaitCallback _callHandlersThreadpoolCallback;

			// Token: 0x0400386E RID: 14446
			private int _currentHandler;

			// Token: 0x0400386F RID: 14447
			private Exception _error;

			// Token: 0x04003870 RID: 14448
			private bool _completed;
		}
	}
}
