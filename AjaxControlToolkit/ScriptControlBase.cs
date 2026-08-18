using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200001B RID: 27
	[ClientScriptResource(null, "BaseScripts")]
	public class ScriptControlBase : ScriptControl, INamingContainer, IControlResolver, IPostBackDataHandler, ICallbackEventHandler, IClientStateManager
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000043FF File Offset: 0x000025FF
		public bool IsRenderingScript
		{
			get
			{
				return this._renderingScript;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004407 File Offset: 0x00002607
		public ScriptControlBase(HtmlTextWriterTag tag) : this(false, tag)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004411 File Offset: 0x00002611
		protected ScriptControlBase(bool enableClientState, HtmlTextWriterTag tag)
		{
			this._tagKey = tag;
			this._enableClientState = enableClientState;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004427 File Offset: 0x00002627
		protected virtual bool SupportsClientState
		{
			get
			{
				return this._enableClientState;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000442F File Offset: 0x0000262F
		protected virtual string SaveClientState()
		{
			return null;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004432 File Offset: 0x00002632
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this._tagKey;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000443A File Offset: 0x0000263A
		protected string ClientStateFieldID
		{
			get
			{
				if (this._cachedClientStateFieldID == null)
				{
					this._cachedClientStateFieldID = this.ClientID + "_ClientState";
				}
				return this._cachedClientStateFieldID;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004460 File Offset: 0x00002660
		protected virtual string ClientControlType
		{
			get
			{
				ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
				return clientScriptResourceAttribute.ComponentType;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000448E File Offset: 0x0000268E
		protected ScriptManager ScriptManager
		{
			get
			{
				this.EnsureScriptManager();
				return this._scriptManager;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000449C File Offset: 0x0000269C
		public override Control FindControl(string id)
		{
			Control control = base.FindControl(id);
			if (control != null)
			{
				return control;
			}
			for (Control namingContainer = this.NamingContainer; namingContainer != null; namingContainer = namingContainer.NamingContainer)
			{
				control = namingContainer.FindControl(id);
				if (control != null)
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000044D7 File Offset: 0x000026D7
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ToolkitResourceManager.RegisterCssReferences(this);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000044E6 File Offset: 0x000026E6
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			base.EnsureID();
			this.EnsureScriptManager();
			if (this.SupportsClientState)
			{
				ScriptManager.RegisterHiddenField(this, this.ClientStateFieldID, this.SaveClientState());
				this.Page.RegisterRequiresPostBack(this);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004521 File Offset: 0x00002721
		private void EnsureScriptManager()
		{
			if (this._scriptManager == null)
			{
				this._scriptManager = ScriptManager.GetCurrent(this.Page);
				if (this._scriptManager == null)
				{
					throw new HttpException("A ScriptManager is required on the page to use ASP.NET AJAX Script Components.");
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004550 File Offset: 0x00002750
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (this.SupportsClientState)
			{
				string text = postCollection[this.ClientStateFieldID];
				if (!string.IsNullOrEmpty(text))
				{
					this.LoadClientState(text);
				}
			}
			return false;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004582 File Offset: 0x00002782
		protected virtual void LoadClientState(string clientState)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004584 File Offset: 0x00002784
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			if (!this.Visible)
			{
				return null;
			}
			base.EnsureID();
			List<ScriptDescriptor> list = new List<ScriptDescriptor>();
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor(this.ClientControlType, this.ClientID);
			this.DescribeComponent(scriptControlDescriptor);
			list.Add(scriptControlDescriptor);
			return list;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000045C8 File Offset: 0x000027C8
		protected virtual void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			try
			{
				this._renderingScript = true;
				ComponentDescriber.DescribeComponent(this, new ScriptComponentDescriptorWrapper(descriptor), this.Page, this);
			}
			finally
			{
				this._renderingScript = false;
			}
			if (this.SupportsClientState)
			{
				descriptor.AddElementProperty("clientStateField", this.ClientStateFieldID);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004624 File Offset: 0x00002824
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!this.Visible)
			{
				return null;
			}
			return ToolkitResourceManager.GetControlScriptReferences(base.GetType());
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000463B File Offset: 0x0000283B
		public Control ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004644 File Offset: 0x00002844
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000464E File Offset: 0x0000284E
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004656 File Offset: 0x00002856
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004658 File Offset: 0x00002858
		string ICallbackEventHandler.GetCallbackResult()
		{
			return this.GetCallbackResult();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004660 File Offset: 0x00002860
		protected virtual string GetCallbackResult()
		{
			string callbackArgument = this._callbackArgument;
			this._callbackArgument = null;
			return this.ExecuteCallbackMethod(callbackArgument);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004684 File Offset: 0x00002884
		private string ExecuteCallbackMethod(string callbackArgument)
		{
			Type type = base.GetType();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(callbackArgument) as Dictionary<string, object>;
			string text = (string)dictionary["name"];
			object[] array = (object[])dictionary["args"];
			string clientState = (string)dictionary["state"];
			if (this != null && ((IClientStateManager)this).SupportsClientState)
			{
				((IClientStateManager)this).LoadClientState(clientState);
			}
			object value = null;
			string text2 = null;
			try
			{
				MethodInfo method = type.GetMethod(text, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
				if (method == null)
				{
					throw new MissingMethodException(type.FullName, text);
				}
				ParameterInfo[] parameters = method.GetParameters();
				ExtenderControlMethodAttribute extenderControlMethodAttribute = (ExtenderControlMethodAttribute)Attribute.GetCustomAttribute(method, typeof(ExtenderControlMethodAttribute));
				if (extenderControlMethodAttribute == null || !extenderControlMethodAttribute.IsScriptMethod || array.Length != parameters.Length)
				{
					throw new MissingMethodException(type.FullName, text);
				}
				object[] array2 = new object[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					if (array[i] != null)
					{
						array2[i] = Convert.ChangeType(array[i], parameters[i].ParameterType, CultureInfo.InvariantCulture);
					}
				}
				value = method.Invoke(this, array2);
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				text2 = innerException.GetType().FullName + ":" + innerException.Message;
			}
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			if (text2 == null)
			{
				dictionary2["result"] = value;
				if (this != null && ((IClientStateManager)this).SupportsClientState)
				{
					dictionary2["state"] = ((IClientStateManager)this).SaveClientState();
				}
			}
			else
			{
				dictionary2["error"] = text2;
			}
			return javaScriptSerializer.Serialize(dictionary2);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004850 File Offset: 0x00002A50
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.RaiseCallbackEvent(eventArgument);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004859 File Offset: 0x00002A59
		protected virtual void RaiseCallbackEvent(string eventArgument)
		{
			this._callbackArgument = eventArgument;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004862 File Offset: 0x00002A62
		bool IClientStateManager.SupportsClientState
		{
			get
			{
				return this.SupportsClientState;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000486A File Offset: 0x00002A6A
		void IClientStateManager.LoadClientState(string clientState)
		{
			this.LoadClientState(clientState);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004873 File Offset: 0x00002A73
		string IClientStateManager.SaveClientState()
		{
			return this.SaveClientState();
		}

		// Token: 0x04000040 RID: 64
		private HtmlTextWriterTag _tagKey;

		// Token: 0x04000041 RID: 65
		private bool _enableClientState;

		// Token: 0x04000042 RID: 66
		private string _cachedClientStateFieldID;

		// Token: 0x04000043 RID: 67
		private string _callbackArgument;

		// Token: 0x04000044 RID: 68
		private ScriptManager _scriptManager;

		// Token: 0x04000045 RID: 69
		private bool _renderingScript;
	}
}
