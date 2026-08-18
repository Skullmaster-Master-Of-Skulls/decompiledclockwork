using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web.Handlers;
using System.Web.Security.Cryptography;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000253 RID: 595
	public sealed class ClientScriptManager
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x000557EC File Offset: 0x000539EC
		internal ClientScriptManager(Page owner)
		{
			this._owner = owner;
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x000557FB File Offset: 0x000539FB
		internal bool HasRegisteredHiddenFields
		{
			get
			{
				return this._registeredHiddenFields != null && this._registeredHiddenFields.Count > 0;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00055815 File Offset: 0x00053A15
		internal bool HasSubmitStatements
		{
			get
			{
				return this._registeredOnSubmitStatements != null && this._registeredOnSubmitStatements.Count > 0;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0005582F File Offset: 0x00053A2F
		internal Dictionary<Assembly, Dictionary<string, object>> RegisteredResourcesToSuppress
		{
			get
			{
				if (this._registeredResourcesToSuppress == null)
				{
					this._registeredResourcesToSuppress = new Dictionary<Assembly, Dictionary<string, object>>();
				}
				return this._registeredResourcesToSuppress;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0005584A File Offset: 0x00053A4A
		private ClientScriptManager.IEventValidationProvider EventValidationProvider
		{
			get
			{
				if (this._eventValidationProvider == null)
				{
					if (AppSettings.UseLegacyEventValidationCompatibility)
					{
						this._eventValidationProvider = new ClientScriptManager.LegacyEventValidationProvider(this);
					}
					else
					{
						this._eventValidationProvider = new ClientScriptManager.DefaultEventValidationProvider(this);
					}
				}
				return this._eventValidationProvider;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0005587C File Offset: 0x00053A7C
		internal string GetEventValidationFieldValue()
		{
			if (this._eventValidationProvider != null)
			{
				object eventValidationStoreObject = this._eventValidationProvider.GetEventValidationStoreObject();
				if (eventValidationStoreObject != null)
				{
					IStateFormatter2 stateFormatter = this._owner.CreateStateFormatter();
					return stateFormatter.Serialize(eventValidationStoreObject, Purpose.WebForms_ClientScriptManager_EventValidation);
				}
			}
			return string.Empty;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000558BE File Offset: 0x00053ABE
		public void RegisterForEventValidation(PostBackOptions options)
		{
			this.RegisterForEventValidation(options.TargetControl.UniqueID, options.Argument);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000558D7 File Offset: 0x00053AD7
		public void RegisterForEventValidation(string uniqueId)
		{
			this.RegisterForEventValidation(uniqueId, string.Empty);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000558E8 File Offset: 0x00053AE8
		public void RegisterForEventValidation(string uniqueId, string argument)
		{
			if (!this._owner.EnableEventValidation || this._owner.DesignMode)
			{
				return;
			}
			if (string.IsNullOrEmpty(uniqueId))
			{
				return;
			}
			if (this._owner.ControlState < ControlState.PreRendered && !this._owner.IsCallback)
			{
				throw new InvalidOperationException(SR.GetString("ClientScriptManager_RegisterForEventValidation_Too_Early"));
			}
			this.EventValidationProvider.RegisterForEventValidation(uniqueId, argument);
			if (this._owner.PartialCachingControlStack != null)
			{
				foreach (object obj in this._owner.PartialCachingControlStack)
				{
					BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
					basePartialCachingControl.RegisterForEventValidation(uniqueId, argument);
				}
			}
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000559B0 File Offset: 0x00053BB0
		internal void SaveEventValidationField()
		{
			string eventValidationFieldValue = this.GetEventValidationFieldValue();
			if (!string.IsNullOrEmpty(eventValidationFieldValue))
			{
				this.RegisterHiddenField("__EVENTVALIDATION", eventValidationFieldValue);
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000559D8 File Offset: 0x00053BD8
		internal static void EnsureJqueryRegistered()
		{
			if (ClientScriptManager._scriptResourceMapping != null && ClientScriptManager._scriptResourceMapping.GetDefinition("jquery", typeof(Page).Assembly) == null && ClientScriptManager._scriptResourceMapping.GetDefinition("jquery") == null)
			{
				throw new InvalidOperationException(SR.GetString("ClientScriptManager_JqueryNotRegistered"));
			}
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x00055A30 File Offset: 0x00053C30
		private void EnsureEventValidationFieldLoaded()
		{
			if (this._eventValidationFieldLoaded)
			{
				return;
			}
			this._eventValidationFieldLoaded = true;
			string text = null;
			if (this._owner.RequestValueCollection != null)
			{
				text = this._owner.RequestValueCollection["__EVENTVALIDATION"];
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			IStateFormatter2 stateFormatter = this._owner.CreateStateFormatter();
			object eventValidationField = null;
			try
			{
				eventValidationField = stateFormatter.Deserialize(text, Purpose.WebForms_ClientScriptManager_EventValidation);
			}
			catch (Exception ex)
			{
				if (!this._owner.ShouldSuppressMacValidationException(ex))
				{
					ViewStateException.ThrowViewStateError(ex, text);
				}
			}
			if (!this.EventValidationProvider.TryLoadEventValidationField(eventValidationField))
			{
				ViewStateException.ThrowViewStateError(null, text);
			}
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x00055AD8 File Offset: 0x00053CD8
		public void ValidateEvent(string uniqueId)
		{
			this.ValidateEvent(uniqueId, string.Empty);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00055AE8 File Offset: 0x00053CE8
		public void ValidateEvent(string uniqueId, string argument)
		{
			if (!this._owner.EnableEventValidation)
			{
				return;
			}
			if (string.IsNullOrEmpty(uniqueId))
			{
				throw new ArgumentException(SR.GetString("Parameter_NullOrEmpty", new object[]
				{
					"uniqueId"
				}), "uniqueId");
			}
			this.EnsureEventValidationFieldLoaded();
			if (this._eventValidationProvider == null || !this._eventValidationProvider.IsValid(uniqueId, argument))
			{
				throw new ArgumentException(SR.GetString("ClientScriptManager_InvalidPostBackArgument"));
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00055B5B File Offset: 0x00053D5B
		internal void ClearHiddenFields()
		{
			this._registeredHiddenFields = null;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x00055B64 File Offset: 0x00053D64
		internal static ScriptKey CreateScriptKey(Type type, string key)
		{
			return new ScriptKey(type, key);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00055B6D File Offset: 0x00053D6D
		internal static ScriptKey CreateScriptIncludeKey(Type type, string key, bool isResource)
		{
			return new ScriptKey(type, key, true, isResource);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00055B78 File Offset: 0x00053D78
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context)
		{
			return this.GetCallbackEventReference(control, argument, clientCallback, context, false);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00055B86 File Offset: 0x00053D86
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, bool useAsync)
		{
			return this.GetCallbackEventReference(control, argument, clientCallback, context, null, useAsync);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00055B98 File Offset: 0x00053D98
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!(control is ICallbackEventHandler))
			{
				throw new InvalidOperationException(SR.GetString("Page_CallBackTargetInvalid", new object[]
				{
					control.UniqueID
				}));
			}
			return this.GetCallbackEventReference("'" + control.UniqueID + "'", argument, clientCallback, context, clientErrorCallback, useAsync);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00055C00 File Offset: 0x00053E00
		public string GetCallbackEventReference(string target, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
		{
			this._owner.RegisterWebFormsScript();
			if (this._owner.ClientSupportsJavaScript && this._owner.RequestInternal != null && this._owner.RequestInternal.Browser.SupportsCallback)
			{
				this.RegisterStartupScript(typeof(Page), "PageCallbackScript", (this._owner.RequestInternal != null && string.Equals(this._owner.RequestInternal.Url.Scheme, "https", StringComparison.OrdinalIgnoreCase)) ? ("\r\nvar callBackFrameUrl='" + Util.QuoteJScriptString(this.GetWebResourceUrl(typeof(Page), "SmartNav.htm"), false) + "';\r\nWebForm_InitCallback();") : "\r\nWebForm_InitCallback();", true);
			}
			if (argument == null)
			{
				argument = "null";
			}
			else if (argument.Length == 0)
			{
				argument = "\"\"";
			}
			if (context == null)
			{
				context = "null";
			}
			else if (context.Length == 0)
			{
				context = "\"\"";
			}
			return string.Concat(new string[]
			{
				"WebForm_DoCallback(",
				target,
				",",
				argument,
				",",
				clientCallback,
				",",
				context,
				",",
				(clientErrorCallback == null) ? "null" : clientErrorCallback,
				",",
				useAsync ? "true" : "false",
				")"
			});
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00055D79 File Offset: 0x00053F79
		public string GetPostBackClientHyperlink(Control control, string argument)
		{
			return this.GetPostBackClientHyperlink(control, argument, true, false);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00055D85 File Offset: 0x00053F85
		public string GetPostBackClientHyperlink(Control control, string argument, bool registerForEventValidation)
		{
			return this.GetPostBackClientHyperlink(control, argument, true, registerForEventValidation);
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00055D91 File Offset: 0x00053F91
		internal string GetPostBackClientHyperlink(Control control, string argument, bool escapePercent, bool registerForEventValidation)
		{
			return "javascript:" + this.GetPostBackEventReference(control, argument, escapePercent, registerForEventValidation);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00055DA8 File Offset: 0x00053FA8
		public string GetPostBackEventReference(Control control, string argument)
		{
			return this.GetPostBackEventReference(control, argument, false, false);
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00055DB4 File Offset: 0x00053FB4
		public string GetPostBackEventReference(Control control, string argument, bool registerForEventValidation)
		{
			return this.GetPostBackEventReference(control, argument, false, registerForEventValidation);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00055DC0 File Offset: 0x00053FC0
		private string GetPostBackEventReference(Control control, string argument, bool forUrl, bool registerForEventValidation)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			this._owner.RegisterPostBackScript();
			string text = control.UniqueID;
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(text, argument);
			}
			if (control.EnableLegacyRendering && this._owner.IsInOnFormRender && text != null && text.IndexOf(':') >= 0)
			{
				text = text.Replace(':', '$');
			}
			string str = "__doPostBack('" + text + "','";
			return str + Util.QuoteJScriptString(argument, forUrl) + "')";
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x00055E4C File Offset: 0x0005404C
		public string GetPostBackEventReference(PostBackOptions options)
		{
			return this.GetPostBackEventReference(options, false);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00055E58 File Offset: 0x00054058
		public string GetPostBackEventReference(PostBackOptions options, bool registerForEventValidation)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(options);
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			if (options.RequiresJavaScriptProtocol)
			{
				stringBuilder.Append("javascript:");
			}
			if (options.AutoPostBack)
			{
				stringBuilder.Append("setTimeout('");
			}
			if (!options.PerformValidation && !options.TrackFocus && options.ClientSubmit && string.IsNullOrEmpty(options.ActionUrl))
			{
				string postBackEventReference = this.GetPostBackEventReference(options.TargetControl, options.Argument);
				if (options.AutoPostBack)
				{
					stringBuilder.Append(Util.QuoteJScriptString(postBackEventReference));
					stringBuilder.Append("', 0)");
				}
				else
				{
					stringBuilder.Append(postBackEventReference);
				}
				return stringBuilder.ToString();
			}
			stringBuilder.Append("WebForm_DoPostBackWithOptions");
			stringBuilder.Append("(new WebForm_PostBackOptions(\"");
			stringBuilder.Append(options.TargetControl.UniqueID);
			stringBuilder.Append("\", ");
			if (string.IsNullOrEmpty(options.Argument))
			{
				stringBuilder.Append("\"\", ");
			}
			else
			{
				stringBuilder.Append("\"");
				stringBuilder.Append(Util.QuoteJScriptString(options.Argument));
				stringBuilder.Append("\", ");
			}
			if (options.PerformValidation)
			{
				flag = true;
				stringBuilder.Append("true, ");
			}
			else
			{
				stringBuilder.Append("false, ");
			}
			if (options.ValidationGroup != null && options.ValidationGroup.Length > 0)
			{
				flag = true;
				stringBuilder.Append("\"");
				stringBuilder.Append(options.ValidationGroup);
				stringBuilder.Append("\", ");
			}
			else
			{
				stringBuilder.Append("\"\", ");
			}
			if (options.ActionUrl != null && options.ActionUrl.Length > 0)
			{
				flag = true;
				this._owner.ContainsCrossPagePost = true;
				stringBuilder.Append("\"");
				stringBuilder.Append(Util.QuoteJScriptString(options.ActionUrl));
				stringBuilder.Append("\", ");
			}
			else
			{
				stringBuilder.Append("\"\", ");
			}
			if (options.TrackFocus)
			{
				this._owner.RegisterFocusScript();
				flag = true;
				stringBuilder.Append("true, ");
			}
			else
			{
				stringBuilder.Append("false, ");
			}
			if (options.ClientSubmit)
			{
				flag = true;
				this._owner.RegisterPostBackScript();
				stringBuilder.Append("true))");
			}
			else
			{
				stringBuilder.Append("false))");
			}
			if (options.AutoPostBack)
			{
				stringBuilder.Append("', 0)");
			}
			string result = null;
			if (flag)
			{
				result = stringBuilder.ToString();
				this._owner.RegisterWebFormsScript();
			}
			return result;
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000560E7 File Offset: 0x000542E7
		public string GetWebResourceUrl(Type type, string resourceName)
		{
			return ClientScriptManager.GetWebResourceUrl(this._owner, type, resourceName, false, (this._owner == null) ? null : this._owner.ScriptManager);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00056110 File Offset: 0x00054310
		internal static string GetWebResourceUrl(Page owner, Type type, string resourceName, bool htmlEncoded, IScriptManager scriptManager)
		{
			bool enableCdn = scriptManager != null && scriptManager.EnableCdn;
			return ClientScriptManager.GetWebResourceUrl(owner, type, resourceName, htmlEncoded, scriptManager, enableCdn);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x00056138 File Offset: 0x00054338
		internal static string GetWebResourceUrl(Page owner, Type type, string resourceName, bool htmlEncoded, IScriptManager scriptManager, bool enableCdn)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(resourceName))
			{
				throw new ArgumentNullException("resourceName");
			}
			if (owner != null && owner.DesignMode)
			{
				ISite site = ((IComponent)owner).Site;
				if (site != null)
				{
					IResourceUrlGenerator resourceUrlGenerator = site.GetService(typeof(IResourceUrlGenerator)) as IResourceUrlGenerator;
					if (resourceUrlGenerator != null)
					{
						return resourceUrlGenerator.GetResourceUrl(type, resourceName);
					}
				}
				return resourceName;
			}
			return AssemblyResourceLoader.GetWebResourceUrl(type, resourceName, htmlEncoded, scriptManager, enableCdn);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000561B1 File Offset: 0x000543B1
		public bool IsClientScriptBlockRegistered(string key)
		{
			return this.IsClientScriptBlockRegistered(typeof(Page), key);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000561C4 File Offset: 0x000543C4
		public bool IsClientScriptBlockRegistered(Type type, string key)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._registeredClientScriptBlocks != null && this._registeredClientScriptBlocks.Contains(ClientScriptManager.CreateScriptKey(type, key));
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x000561F6 File Offset: 0x000543F6
		public bool IsClientScriptIncludeRegistered(string key)
		{
			return this.IsClientScriptIncludeRegistered(typeof(Page), key);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00056209 File Offset: 0x00054409
		public bool IsClientScriptIncludeRegistered(Type type, string key)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._registeredClientScriptBlocks != null && this._registeredClientScriptBlocks.Contains(ClientScriptManager.CreateScriptIncludeKey(type, key, false));
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0005623C File Offset: 0x0005443C
		public bool IsStartupScriptRegistered(string key)
		{
			return this.IsStartupScriptRegistered(typeof(Page), key);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0005624F File Offset: 0x0005444F
		public bool IsStartupScriptRegistered(Type type, string key)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._registeredClientStartupScripts != null && this._registeredClientStartupScripts.Contains(ClientScriptManager.CreateScriptKey(type, key));
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00056281 File Offset: 0x00054481
		public bool IsOnSubmitStatementRegistered(string key)
		{
			return this.IsOnSubmitStatementRegistered(typeof(Page), key);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00056294 File Offset: 0x00054494
		public bool IsOnSubmitStatementRegistered(Type type, string key)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._registeredOnSubmitStatements != null && this._registeredOnSubmitStatements.Contains(ClientScriptManager.CreateScriptKey(type, key));
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x000562C8 File Offset: 0x000544C8
		public void RegisterArrayDeclaration(string arrayName, string arrayValue)
		{
			if (arrayName == null)
			{
				throw new ArgumentNullException("arrayName");
			}
			if (this._registeredArrayDeclares == null)
			{
				this._registeredArrayDeclares = new ListDictionary();
			}
			if (!this._registeredArrayDeclares.Contains(arrayName))
			{
				this._registeredArrayDeclares[arrayName] = new ArrayList();
			}
			ArrayList arrayList = (ArrayList)this._registeredArrayDeclares[arrayName];
			arrayList.Add(arrayValue);
			if (this._owner.PartialCachingControlStack != null)
			{
				foreach (object obj in this._owner.PartialCachingControlStack)
				{
					BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
					basePartialCachingControl.RegisterArrayDeclaration(arrayName, arrayValue);
				}
			}
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00056390 File Offset: 0x00054590
		internal void RegisterArrayDeclaration(Control control, string arrayName, string arrayValue)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterArrayDeclaration(control, arrayName, arrayValue);
				return;
			}
			this.RegisterArrayDeclaration(arrayName, arrayValue);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x000563C6 File Offset: 0x000545C6
		public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
		{
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, true);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x000563D4 File Offset: 0x000545D4
		public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
		{
			StringUtil.CheckAndTrimString(controlId, "controlId");
			StringUtil.CheckAndTrimString(attributeName, "attributeName");
			ListDictionary listDictionary = null;
			if (this._registeredControlsWithExpandoAttributes == null)
			{
				this._registeredControlsWithExpandoAttributes = new ListDictionary(StringComparer.Ordinal);
			}
			else
			{
				listDictionary = (ListDictionary)this._registeredControlsWithExpandoAttributes[controlId];
			}
			if (listDictionary == null)
			{
				listDictionary = new ListDictionary(StringComparer.Ordinal);
				this._registeredControlsWithExpandoAttributes.Add(controlId, listDictionary);
			}
			if (encode)
			{
				attributeValue = Util.QuoteJScriptString(attributeValue);
			}
			listDictionary.Add(attributeName, attributeValue);
			if (this._owner.PartialCachingControlStack != null)
			{
				foreach (object obj in this._owner.PartialCachingControlStack)
				{
					BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
					basePartialCachingControl.RegisterExpandoAttribute(controlId, attributeName, attributeValue);
				}
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x000564B8 File Offset: 0x000546B8
		internal void RegisterExpandoAttribute(Control control, string controlId, string attributeName, string attributeValue, bool encode)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterExpandoAttribute(control, controlId, attributeName, attributeValue, encode);
				return;
			}
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x000564F8 File Offset: 0x000546F8
		public void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
		{
			if (hiddenFieldName == null)
			{
				throw new ArgumentNullException("hiddenFieldName");
			}
			if (this._registeredHiddenFields == null)
			{
				this._registeredHiddenFields = new ListDictionary();
			}
			if (!this._registeredHiddenFields.Contains(hiddenFieldName))
			{
				this._registeredHiddenFields.Add(hiddenFieldName, hiddenFieldInitialValue);
			}
			if (this._owner._hiddenFieldsToRender == null)
			{
				this._owner._hiddenFieldsToRender = new Dictionary<string, string>();
			}
			this._owner._hiddenFieldsToRender[hiddenFieldName] = hiddenFieldInitialValue;
			if (this._owner.PartialCachingControlStack != null)
			{
				foreach (object obj in this._owner.PartialCachingControlStack)
				{
					BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
					basePartialCachingControl.RegisterHiddenField(hiddenFieldName, hiddenFieldInitialValue);
				}
			}
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x000565D0 File Offset: 0x000547D0
		internal void RegisterHiddenField(Control control, string hiddenFieldName, string hiddenFieldValue)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterHiddenField(control, hiddenFieldName, hiddenFieldValue);
				return;
			}
			this.RegisterHiddenField(hiddenFieldName, hiddenFieldValue);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00056606 File Offset: 0x00054806
		public void RegisterClientScriptBlock(Type type, string key, string script)
		{
			this.RegisterClientScriptBlock(type, key, script, false);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00056612 File Offset: 0x00054812
		public void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (addScriptTags)
			{
				this.RegisterScriptBlock(ClientScriptManager.CreateScriptKey(type, key), script, ClientAPIRegisterType.ClientScriptBlocksWithoutTags);
				return;
			}
			this.RegisterScriptBlock(ClientScriptManager.CreateScriptKey(type, key), script, ClientAPIRegisterType.ClientScriptBlocks);
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0005664C File Offset: 0x0005484C
		internal void RegisterClientScriptBlock(Control control, Type type, string key, string script, bool addScriptTags)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterClientScriptBlock(control, type, key, script, addScriptTags);
				return;
			}
			this.RegisterClientScriptBlock(type, key, script, addScriptTags);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0005668A File Offset: 0x0005488A
		public void RegisterClientScriptInclude(string key, string url)
		{
			this.RegisterClientScriptInclude(typeof(Page), key, url);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0005669E File Offset: 0x0005489E
		public void RegisterClientScriptInclude(Type type, string key, string url)
		{
			this.RegisterClientScriptInclude(type, key, url, false);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x000566AC File Offset: 0x000548AC
		internal void RegisterClientScriptInclude(Type type, string key, string url, bool isResource)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (string.IsNullOrEmpty(url))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("url");
			}
			string script = "\r\n<script src=\"" + HttpUtility.HtmlAttributeEncode(url) + "\" type=\"text/javascript\"></script>";
			this.RegisterScriptBlock(ClientScriptManager.CreateScriptIncludeKey(type, key, isResource), script, ClientAPIRegisterType.ClientScriptBlocks);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00056708 File Offset: 0x00054908
		internal void RegisterClientScriptInclude(Control control, Type type, string key, string url)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterClientScriptInclude(control, type, key, url);
				return;
			}
			this.RegisterClientScriptInclude(type, key, url);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00056742 File Offset: 0x00054942
		public void RegisterClientScriptResource(Type type, string resourceName)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterClientScriptInclude(type, resourceName, this.GetWebResourceUrl(type, resourceName), true);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0005676C File Offset: 0x0005496C
		internal void RegisterClientScriptResource(Control control, Type type, string resourceName)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterClientScriptResource(control, type, resourceName);
				return;
			}
			this.RegisterClientScriptResource(type, resourceName);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x000567A4 File Offset: 0x000549A4
		internal void RegisterDefaultButtonScript(Control button, HtmlTextWriter writer, bool useAddAttribute)
		{
			this._owner.RegisterWebFormsScript();
			if (this._owner.EnableLegacyRendering)
			{
				if (useAddAttribute)
				{
					writer.AddAttribute("language", "javascript", false);
				}
				else
				{
					writer.WriteAttribute("language", "javascript", false);
				}
			}
			string value = "javascript:return WebForm_FireDefaultButton(event, '" + button.ClientID + "')";
			if (useAddAttribute)
			{
				writer.AddAttribute("onkeypress", value);
				return;
			}
			writer.WriteAttribute("onkeypress", value);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00056822 File Offset: 0x00054A22
		public void RegisterOnSubmitStatement(Type type, string key, string script)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterOnSubmitStatementInternal(ClientScriptManager.CreateScriptKey(type, key), script);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00056848 File Offset: 0x00054A48
		internal void RegisterOnSubmitStatement(Control control, Type type, string key, string script)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterOnSubmitStatement(control, type, key, script);
				return;
			}
			this.RegisterOnSubmitStatement(type, key, script);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00056884 File Offset: 0x00054A84
		internal void RegisterOnSubmitStatementInternal(ScriptKey key, string script)
		{
			if (string.IsNullOrEmpty(script))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("script");
			}
			if (this._registeredOnSubmitStatements == null)
			{
				this._registeredOnSubmitStatements = new ListDictionary();
			}
			int num = script.Length - 1;
			while (num >= 0 && char.IsWhiteSpace(script, num))
			{
				num--;
			}
			if (num >= 0 && script[num] != ';')
			{
				script = script.Substring(0, num + 1) + ";" + script.Substring(num + 1);
			}
			if (!this._registeredOnSubmitStatements.Contains(key))
			{
				this._registeredOnSubmitStatements.Add(key, script);
			}
			if (this._owner.PartialCachingControlStack != null)
			{
				foreach (object obj in this._owner.PartialCachingControlStack)
				{
					BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
					basePartialCachingControl.RegisterOnSubmitStatement(key, script);
				}
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0005697C File Offset: 0x00054B7C
		internal void RegisterScriptBlock(ScriptKey key, string script, ClientAPIRegisterType type)
		{
			switch (type)
			{
			case ClientAPIRegisterType.ClientScriptBlocks:
				this.RegisterScriptBlock(key, script, ref this._registeredClientScriptBlocks, ref this._clientScriptBlocks, false);
				break;
			case ClientAPIRegisterType.ClientScriptBlocksWithoutTags:
				this.RegisterScriptBlock(key, script, ref this._registeredClientScriptBlocks, ref this._clientScriptBlocks, true);
				break;
			case ClientAPIRegisterType.ClientStartupScripts:
				this.RegisterScriptBlock(key, script, ref this._registeredClientStartupScripts, ref this._clientStartupScripts, false);
				break;
			case ClientAPIRegisterType.ClientStartupScriptsWithoutTags:
				this.RegisterScriptBlock(key, script, ref this._registeredClientStartupScripts, ref this._clientStartupScripts, true);
				break;
			}
			if (this._owner.PartialCachingControlStack != null)
			{
				foreach (object obj in this._owner.PartialCachingControlStack)
				{
					BasePartialCachingControl basePartialCachingControl = (BasePartialCachingControl)obj;
					basePartialCachingControl.RegisterScriptBlock(type, key, script);
				}
			}
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00056A60 File Offset: 0x00054C60
		private void RegisterScriptBlock(ScriptKey key, string script, ref ListDictionary scriptBlocks, ref ArrayList scriptList, bool needsScriptTags)
		{
			if (scriptBlocks == null)
			{
				scriptBlocks = new ListDictionary();
				scriptList = new ArrayList();
			}
			if (!scriptBlocks.Contains(key))
			{
				Tuple<ScriptKey, string, bool> value = new Tuple<ScriptKey, string, bool>(key, script, needsScriptTags);
				scriptBlocks.Add(key, null);
				scriptList.Add(value);
			}
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00056AA7 File Offset: 0x00054CA7
		public void RegisterStartupScript(Type type, string key, string script)
		{
			this.RegisterStartupScript(type, key, script, false);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00056AB3 File Offset: 0x00054CB3
		public void RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (addScriptTags)
			{
				this.RegisterScriptBlock(ClientScriptManager.CreateScriptKey(type, key), script, ClientAPIRegisterType.ClientStartupScriptsWithoutTags);
				return;
			}
			this.RegisterScriptBlock(ClientScriptManager.CreateScriptKey(type, key), script, ClientAPIRegisterType.ClientStartupScripts);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00056AEC File Offset: 0x00054CEC
		internal void RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags)
		{
			IScriptManager scriptManager = this._owner.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterStartupScript(control, type, key, script, addScriptTags);
				return;
			}
			this.RegisterStartupScript(type, key, script, addScriptTags);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00056B2C File Offset: 0x00054D2C
		internal void RenderArrayDeclares(HtmlTextWriter writer)
		{
			if (this._registeredArrayDeclares == null || this._registeredArrayDeclares.Count == 0)
			{
				return;
			}
			writer.Write(this._owner.EnableLegacyRendering ? "\r\n<script type=\"text/javascript\">\r\n<!--\r\n" : "\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n");
			IDictionaryEnumerator enumerator = this._registeredArrayDeclares.GetEnumerator();
			while (enumerator.MoveNext())
			{
				writer.Write("var ");
				writer.Write(enumerator.Key);
				writer.Write(" =  new Array(");
				IEnumerator enumerator2 = ((ArrayList)enumerator.Value).GetEnumerator();
				bool flag = true;
				while (enumerator2.MoveNext())
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						writer.Write(", ");
					}
					writer.Write(enumerator2.Current);
				}
				writer.WriteLine(");");
			}
			writer.Write(this._owner.EnableLegacyRendering ? "// -->\r\n</script>\r\n" : "//]]>\r\n</script>\r\n");
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00056C0C File Offset: 0x00054E0C
		internal void RenderExpandoAttribute(HtmlTextWriter writer)
		{
			if (this._registeredControlsWithExpandoAttributes == null || this._registeredControlsWithExpandoAttributes.Count == 0)
			{
				return;
			}
			writer.Write(this._owner.EnableLegacyRendering ? "\r\n<script type=\"text/javascript\">\r\n<!--\r\n" : "\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n");
			foreach (object obj in this._registeredControlsWithExpandoAttributes)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string value = (string)dictionaryEntry.Key;
				writer.Write("var ");
				writer.Write(value);
				writer.Write(" = document.all ? document.all[\"");
				writer.Write(value);
				writer.Write("\"] : document.getElementById(\"");
				writer.Write(value);
				writer.WriteLine("\");");
				ListDictionary listDictionary = (ListDictionary)dictionaryEntry.Value;
				foreach (object obj2 in listDictionary)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					writer.Write(value);
					writer.Write(".");
					writer.Write(dictionaryEntry2.Key);
					if (dictionaryEntry2.Value == null)
					{
						writer.WriteLine(" = null;");
					}
					else
					{
						writer.Write(" = \"");
						writer.Write(dictionaryEntry2.Value);
						writer.WriteLine("\";");
					}
				}
			}
			writer.Write(this._owner.EnableLegacyRendering ? "// -->\r\n</script>\r\n" : "//]]>\r\n</script>\r\n");
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00056DCC File Offset: 0x00054FCC
		internal void RenderHiddenFields(HtmlTextWriter writer)
		{
			if (this._registeredHiddenFields == null || this._registeredHiddenFields.Count == 0)
			{
				return;
			}
			foreach (object obj in this._registeredHiddenFields)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (text == null)
				{
					text = string.Empty;
				}
				writer.WriteLine();
				writer.Write("<input type=\"hidden\" name=\"");
				writer.Write(text);
				writer.Write("\" id=\"");
				writer.Write(text);
				writer.Write("\" value=\"");
				HttpUtility.HtmlEncode((string)dictionaryEntry.Value, writer);
				writer.Write("\" />");
			}
			this.ClearHiddenFields();
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00056EA4 File Offset: 0x000550A4
		internal void RenderClientScriptBlocks(HtmlTextWriter writer)
		{
			bool flag = false;
			if (this._clientScriptBlocks != null)
			{
				flag = this.RenderRegisteredScripts(writer, this._clientScriptBlocks, true);
			}
			if (!string.IsNullOrEmpty(this._owner.ClientOnSubmitEvent) && this._owner.ClientSupportsJavaScript)
			{
				if (!flag)
				{
					writer.Write(this._owner.EnableLegacyRendering ? "\r\n<script type=\"text/javascript\">\r\n<!--\r\n" : "\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n");
				}
				writer.Write("function WebForm_OnSubmit() {\r\n");
				if (this._registeredOnSubmitStatements != null)
				{
					foreach (object obj in this._registeredOnSubmitStatements.Values)
					{
						string value = (string)obj;
						writer.Write(value);
					}
				}
				writer.WriteLine("\r\nreturn true;\r\n}");
				writer.Write(this._owner.EnableLegacyRendering ? "// -->\r\n</script>\r\n" : "//]]>\r\n</script>\r\n");
				return;
			}
			if (flag)
			{
				writer.Write(this._owner.EnableLegacyRendering ? "// -->\r\n</script>\r\n" : "//]]>\r\n</script>\r\n");
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00056FC4 File Offset: 0x000551C4
		internal void RenderClientStartupScripts(HtmlTextWriter writer)
		{
			if (this._clientStartupScripts != null)
			{
				bool flag = this.RenderRegisteredScripts(writer, this._clientStartupScripts, false);
				if (flag)
				{
					writer.Write(this._owner.EnableLegacyRendering ? "// -->\r\n</script>\r\n" : "//]]>\r\n</script>\r\n");
				}
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x0005700C File Offset: 0x0005520C
		private bool RenderRegisteredScripts(HtmlTextWriter writer, ArrayList scripts, bool checkForScriptManagerRegistrations)
		{
			writer.WriteLine();
			bool flag = false;
			checkForScriptManagerRegistrations &= (this._registeredResourcesToSuppress != null);
			foreach (object obj in scripts)
			{
				Tuple<ScriptKey, string, bool> tuple = (Tuple<ScriptKey, string, bool>)obj;
				if (checkForScriptManagerRegistrations)
				{
					ScriptKey item = tuple.Item1;
					Dictionary<string, object> dictionary;
					if (item.IsResource && this._registeredResourcesToSuppress.TryGetValue(item.Assembly, out dictionary) && dictionary.ContainsKey(item.Key))
					{
						continue;
					}
				}
				if (tuple.Item3)
				{
					if (!flag)
					{
						writer.Write(this._owner.EnableLegacyRendering ? "\r\n<script type=\"text/javascript\">\r\n<!--\r\n" : "\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n");
						flag = true;
					}
				}
				else if (flag)
				{
					writer.Write(this._owner.EnableLegacyRendering ? "// -->\r\n</script>\r\n" : "//]]>\r\n</script>\r\n");
					flag = false;
				}
				writer.Write(tuple.Item2);
			}
			return flag;
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0005710C File Offset: 0x0005530C
		internal void RenderWebFormsScript(HtmlTextWriter writer)
		{
			Dictionary<string, object> dictionary;
			if (this._registeredResourcesToSuppress != null && this._registeredResourcesToSuppress.TryGetValue(AssemblyResourceLoader.GetAssemblyFromType(typeof(Page)), out dictionary) && dictionary.ContainsKey("WebForms.js"))
			{
				return;
			}
			writer.Write("\r\n<script src=\"");
			writer.Write(ClientScriptManager.GetWebResourceUrl(this._owner, typeof(Page), "WebForms.js", true, this._owner.ScriptManager));
			writer.Write("\" type=\"text/javascript\"></script>");
			if (this._owner.ScriptManager != null && this._owner.ScriptManager.EnableCdn && this._owner.ScriptManager.EnableCdnFallback)
			{
				string webResourceUrl = ClientScriptManager.GetWebResourceUrl(this._owner, typeof(Page), "WebForms.js", true, this._owner.ScriptManager, false);
				if (!string.IsNullOrEmpty(webResourceUrl))
				{
					writer.Write("\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n");
					writer.Write("window.WebForm_PostBackOptions||document.write('<script type=\"text/javascript\" src=\"" + webResourceUrl + "\"><\\/script>');");
					writer.Write("//]]>\r\n</script>\r\n");
				}
			}
			writer.WriteLine();
		}

		// Token: 0x040018A8 RID: 6312
		private const string IncludeScriptBegin = "\r\n<script src=\"";

		// Token: 0x040018A9 RID: 6313
		private const string IncludeScriptEnd = "\" type=\"text/javascript\"></script>";

		// Token: 0x040018AA RID: 6314
		internal const string ClientScriptStart = "\r\n<script type=\"text/javascript\">\r\n//<![CDATA[\r\n";

		// Token: 0x040018AB RID: 6315
		internal const string ClientScriptStartLegacy = "\r\n<script type=\"text/javascript\">\r\n<!--\r\n";

		// Token: 0x040018AC RID: 6316
		internal const string ClientScriptEnd = "//]]>\r\n</script>\r\n";

		// Token: 0x040018AD RID: 6317
		internal const string ClientScriptEndLegacy = "// -->\r\n</script>\r\n";

		// Token: 0x040018AE RID: 6318
		internal const string JscriptPrefix = "javascript:";

		// Token: 0x040018AF RID: 6319
		private const string _callbackFunctionName = "WebForm_DoCallback";

		// Token: 0x040018B0 RID: 6320
		private const string _postbackOptionsFunctionName = "WebForm_DoPostBackWithOptions";

		// Token: 0x040018B1 RID: 6321
		private const string _postBackFunctionName = "__doPostBack";

		// Token: 0x040018B2 RID: 6322
		private const string PageCallbackScriptKey = "PageCallbackScript";

		// Token: 0x040018B3 RID: 6323
		internal static IScriptResourceMapping _scriptResourceMapping;

		// Token: 0x040018B4 RID: 6324
		private ListDictionary _registeredClientScriptBlocks;

		// Token: 0x040018B5 RID: 6325
		private ArrayList _clientScriptBlocks;

		// Token: 0x040018B6 RID: 6326
		private ListDictionary _registeredClientStartupScripts;

		// Token: 0x040018B7 RID: 6327
		private ArrayList _clientStartupScripts;

		// Token: 0x040018B8 RID: 6328
		private Dictionary<Assembly, Dictionary<string, object>> _registeredResourcesToSuppress;

		// Token: 0x040018B9 RID: 6329
		private bool _eventValidationFieldLoaded;

		// Token: 0x040018BA RID: 6330
		private ListDictionary _registeredOnSubmitStatements;

		// Token: 0x040018BB RID: 6331
		private IDictionary _registeredArrayDeclares;

		// Token: 0x040018BC RID: 6332
		private ListDictionary _registeredHiddenFields;

		// Token: 0x040018BD RID: 6333
		private ListDictionary _registeredControlsWithExpandoAttributes;

		// Token: 0x040018BE RID: 6334
		private ClientScriptManager.IEventValidationProvider _eventValidationProvider;

		// Token: 0x040018BF RID: 6335
		private Page _owner;

		// Token: 0x02000957 RID: 2391
		private interface IEventValidationProvider
		{
			// Token: 0x060069B8 RID: 27064
			object GetEventValidationStoreObject();

			// Token: 0x060069B9 RID: 27065
			bool IsValid(string uniqueId, string argument);

			// Token: 0x060069BA RID: 27066
			void RegisterForEventValidation(string uniqueId, string argument);

			// Token: 0x060069BB RID: 27067
			bool TryLoadEventValidationField(object eventValidationField);
		}

		// Token: 0x02000958 RID: 2392
		private sealed class DefaultEventValidationProvider : ClientScriptManager.IEventValidationProvider
		{
			// Token: 0x060069BC RID: 27068 RVA: 0x00177DB2 File Offset: 0x00175FB2
			internal DefaultEventValidationProvider(ClientScriptManager clientScriptManager)
			{
				this._clientScriptManager = clientScriptManager;
			}

			// Token: 0x060069BD RID: 27069 RVA: 0x00177DC1 File Offset: 0x00175FC1
			public object GetEventValidationStoreObject()
			{
				if (this._outboundEvents != null && this._outboundEvents.Count > 0)
				{
					return this._outboundEvents;
				}
				return null;
			}

			// Token: 0x060069BE RID: 27070 RVA: 0x00177DE1 File Offset: 0x00175FE1
			public bool IsValid(string uniqueId, string argument)
			{
				return this._inboundEvents != null && this._inboundEvents.Contains(uniqueId, argument);
			}

			// Token: 0x060069BF RID: 27071 RVA: 0x00177DFC File Offset: 0x00175FFC
			public void RegisterForEventValidation(string uniqueId, string argument)
			{
				if (this._outboundEvents == null)
				{
					if (this._clientScriptManager._owner.IsCallback)
					{
						this._clientScriptManager.EnsureEventValidationFieldLoaded();
						if (this._outboundEvents == null)
						{
							this._outboundEvents = new EventValidationStore();
						}
					}
					else
					{
						this._outboundEvents = new EventValidationStore();
						this._outboundEvents.Add(null, this._clientScriptManager._owner.ClientState);
					}
				}
				this._outboundEvents.Add(uniqueId, argument);
			}

			// Token: 0x060069C0 RID: 27072 RVA: 0x00177E78 File Offset: 0x00176078
			public bool TryLoadEventValidationField(object eventValidationField)
			{
				EventValidationStore eventValidationStore = eventValidationField as EventValidationStore;
				if (eventValidationStore == null || eventValidationStore.Count < 1)
				{
					return true;
				}
				string requestViewStateString = this._clientScriptManager._owner.RequestViewStateString;
				if (!eventValidationStore.Contains(null, requestViewStateString))
				{
					return false;
				}
				this._inboundEvents = eventValidationStore;
				if (this._clientScriptManager._owner.IsCallback)
				{
					EventValidationStore outboundEvents = eventValidationStore.Clone();
					this._outboundEvents = outboundEvents;
				}
				return true;
			}

			// Token: 0x040037EE RID: 14318
			private readonly ClientScriptManager _clientScriptManager;

			// Token: 0x040037EF RID: 14319
			private EventValidationStore _inboundEvents;

			// Token: 0x040037F0 RID: 14320
			private EventValidationStore _outboundEvents;
		}

		// Token: 0x02000959 RID: 2393
		private sealed class LegacyEventValidationProvider : ClientScriptManager.IEventValidationProvider
		{
			// Token: 0x060069C1 RID: 27073 RVA: 0x00177EDF File Offset: 0x001760DF
			internal LegacyEventValidationProvider(ClientScriptManager clientScriptManager)
			{
				this._clientScriptManager = clientScriptManager;
			}

			// Token: 0x060069C2 RID: 27074 RVA: 0x00177EEE File Offset: 0x001760EE
			private static int ComputeHashKey(string uniqueId, string argument)
			{
				if (string.IsNullOrEmpty(argument))
				{
					return StringUtil.GetStringHashCode(uniqueId);
				}
				return StringUtil.GetStringHashCode(uniqueId) ^ StringUtil.GetStringHashCode(argument);
			}

			// Token: 0x060069C3 RID: 27075 RVA: 0x00177F0C File Offset: 0x0017610C
			public object GetEventValidationStoreObject()
			{
				if (this._validEventReferences != null && this._validEventReferences.Count > 0)
				{
					return this._validEventReferences;
				}
				return null;
			}

			// Token: 0x060069C4 RID: 27076 RVA: 0x00177F2C File Offset: 0x0017612C
			public bool IsValid(string uniqueId, string argument)
			{
				if (this._clientPostBackValidatedEventTable == null)
				{
					return false;
				}
				int num = ClientScriptManager.LegacyEventValidationProvider.ComputeHashKey(uniqueId, argument);
				return this._clientPostBackValidatedEventTable.Contains(num);
			}

			// Token: 0x060069C5 RID: 27077 RVA: 0x00177F5C File Offset: 0x0017615C
			public void RegisterForEventValidation(string uniqueId, string argument)
			{
				int num = ClientScriptManager.LegacyEventValidationProvider.ComputeHashKey(uniqueId, argument);
				string text = this._clientScriptManager._owner.ClientState;
				if (text == null)
				{
					text = string.Empty;
				}
				if (this._validEventReferences == null)
				{
					if (this._clientScriptManager._owner.IsCallback)
					{
						this._clientScriptManager.EnsureEventValidationFieldLoaded();
						if (this._validEventReferences == null)
						{
							this._validEventReferences = new ArrayList();
						}
					}
					else
					{
						this._validEventReferences = new ArrayList();
						this._validEventReferences.Add(StringUtil.GetStringHashCode(text));
					}
				}
				this._validEventReferences.Add(num);
			}

			// Token: 0x060069C6 RID: 27078 RVA: 0x00177FFC File Offset: 0x001761FC
			public bool TryLoadEventValidationField(object eventValidationField)
			{
				ArrayList arrayList = eventValidationField as ArrayList;
				if (arrayList == null || arrayList.Count < 1)
				{
					return true;
				}
				int num = (int)arrayList[0];
				string requestViewStateString = this._clientScriptManager._owner.RequestViewStateString;
				if (num != StringUtil.GetStringHashCode(requestViewStateString))
				{
					return false;
				}
				this._clientPostBackValidatedEventTable = new HybridDictionary(arrayList.Count - 1, true);
				for (int i = 1; i < arrayList.Count; i++)
				{
					int num2 = (int)arrayList[i];
					this._clientPostBackValidatedEventTable[num2] = null;
				}
				if (this._clientScriptManager._owner.IsCallback)
				{
					this._validEventReferences = arrayList;
				}
				return true;
			}

			// Token: 0x040037F1 RID: 14321
			private readonly ClientScriptManager _clientScriptManager;

			// Token: 0x040037F2 RID: 14322
			private ArrayList _validEventReferences;

			// Token: 0x040037F3 RID: 14323
			private HybridDictionary _clientPostBackValidatedEventTable;
		}
	}
}
