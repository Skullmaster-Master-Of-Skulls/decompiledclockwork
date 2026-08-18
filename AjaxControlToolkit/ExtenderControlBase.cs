using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000007 RID: 7
	[ClientScriptResource(null, "BaseScripts")]
	[Themeable(true)]
	public abstract class ExtenderControlBase : ExtenderControl, IControlResolver
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000053 RID: 83 RVA: 0x00003110 File Offset: 0x00001310
		// (remove) Token: 0x06000054 RID: 84 RVA: 0x00003148 File Offset: 0x00001348
		public event ResolveControlEventHandler ResolveControlID;

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000317D File Offset: 0x0000137D
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000318B File Offset: 0x0000138B
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return this.GetPropertyValue<bool>("Enabled", true);
			}
			set
			{
				this.SetPropertyValue<bool>("Enabled", value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003199 File Offset: 0x00001399
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000031A1 File Offset: 0x000013A1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string ClientState
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

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000031AC File Offset: 0x000013AC
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000031DA File Offset: 0x000013DA
		[ClientPropertyName("id")]
		[ExtenderControlProperty]
		public string BehaviorID
		{
			get
			{
				string propertyValue = this.GetPropertyValue<string>("BehaviorID", "");
				if (!string.IsNullOrEmpty(propertyValue))
				{
					return propertyValue;
				}
				return this.ClientID;
			}
			set
			{
				this.SetPropertyValue<string>("BehaviorID", value);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000031E8 File Offset: 0x000013E8
		private string GetClientStateFieldID()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}_ClientState", new object[]
			{
				this.ID
			});
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003215 File Offset: 0x00001415
		protected override void OnInit(EventArgs e)
		{
			if (this.EnableClientState)
			{
				this.CreateClientStateField();
			}
			this.Page.PreLoad += this.Page_PreLoad;
			base.OnInit(e);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003244 File Offset: 0x00001444
		private void Page_PreLoad(object sender, EventArgs e)
		{
			this.LoadClientStateValues();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000324C File Offset: 0x0000144C
		private HiddenField CreateClientStateField()
		{
			HiddenField hiddenField = new HiddenField();
			hiddenField.ID = this.GetClientStateFieldID();
			this.Controls.Add(hiddenField);
			this.ClientStateFieldID = hiddenField.ID;
			return hiddenField;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003284 File Offset: 0x00001484
		private void LoadClientStateValues()
		{
			if (this.EnableClientState && !string.IsNullOrEmpty(this.ClientStateFieldID))
			{
				HiddenField hiddenField = (HiddenField)this.NamingContainer.FindControl(this.ClientStateFieldID);
				if (hiddenField != null && !string.IsNullOrEmpty(hiddenField.Value))
				{
					this.ClientState = hiddenField.Value;
				}
			}
			if (this.ClientStateValuesLoaded != null)
			{
				this.ClientStateValuesLoaded(this, EventArgs.Empty);
			}
			this._loadedClientStateValues = true;
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000060 RID: 96 RVA: 0x000032FC File Offset: 0x000014FC
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x00003334 File Offset: 0x00001534
		protected event EventHandler ClientStateValuesLoaded;

		// Token: 0x06000062 RID: 98 RVA: 0x0000336C File Offset: 0x0000156C
		private void SaveClientStateValues()
		{
			if (this.EnableClientState)
			{
				HiddenField hiddenField;
				if (string.IsNullOrEmpty(this.ClientStateFieldID))
				{
					hiddenField = this.CreateClientStateField();
				}
				else
				{
					hiddenField = (HiddenField)this.NamingContainer.FindControl(this.ClientStateFieldID);
				}
				if (hiddenField != null)
				{
					hiddenField.Value = this.ClientState;
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000033BF File Offset: 0x000015BF
		// (set) Token: 0x06000064 RID: 100 RVA: 0x000033D1 File Offset: 0x000015D1
		[IDReferenceProperty(typeof(HiddenField))]
		[DefaultValue("")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExtenderControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ClientStateFieldID
		{
			get
			{
				return this.GetPropertyValue<string>("ClientStateFieldID", "");
			}
			set
			{
				this.SetPropertyValue<string>("ClientStateFieldID", value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000065 RID: 101 RVA: 0x000033DF File Offset: 0x000015DF
		// (set) Token: 0x06000066 RID: 102 RVA: 0x000033E7 File Offset: 0x000015E7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool EnableClientState
		{
			get
			{
				return this._enableClientState;
			}
			set
			{
				this._enableClientState = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000033F0 File Offset: 0x000015F0
		protected virtual string ClientControlType
		{
			get
			{
				ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)TypeDescriptor.GetAttributes(this)[typeof(ClientScriptResourceAttribute)];
				return clientScriptResourceAttribute.ComponentType;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000341E File Offset: 0x0000161E
		public Control ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003427 File Offset: 0x00001627
		public override Control FindControl(string id)
		{
			return this.FindControlHelper(id);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003430 File Offset: 0x00001630
		protected Control TargetControl
		{
			get
			{
				return this.FindControlHelper(base.TargetControlID);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003440 File Offset: 0x00001640
		protected Control FindControlHelper(string id)
		{
			Control control;
			if (this._findControlHelperCache.ContainsKey(id))
			{
				control = this._findControlHelperCache[id];
			}
			else
			{
				control = base.FindControl(id);
				Control namingContainer = this.NamingContainer;
				while (control == null && namingContainer != null)
				{
					control = namingContainer.FindControl(id);
					namingContainer = namingContainer.NamingContainer;
				}
				if (control == null)
				{
					ResolveControlEventArgs resolveControlEventArgs = new ResolveControlEventArgs(id);
					this.OnResolveControlID(resolveControlEventArgs);
					control = resolveControlEventArgs.Control;
				}
				if (control != null)
				{
					this._findControlHelperCache[id] = control;
				}
			}
			return control;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000034BB File Offset: 0x000016BB
		protected virtual void OnResolveControlID(ResolveControlEventArgs e)
		{
			if (this.ResolveControlID != null)
			{
				this.ResolveControlID(this, e);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000034D4 File Offset: 0x000016D4
		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl)
		{
			if (!this.Enabled || !targetControl.Visible)
			{
				return null;
			}
			this.EnsureValid();
			ScriptBehaviorDescriptor scriptBehaviorDescriptor = new ScriptBehaviorDescriptor(this.ClientControlType, targetControl.ClientID);
			this.RenderScriptAttributes(scriptBehaviorDescriptor);
			this.RenderInnerScript(scriptBehaviorDescriptor);
			return new List<ScriptDescriptor>(new ScriptDescriptor[]
			{
				scriptBehaviorDescriptor
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000352A File Offset: 0x0000172A
		public virtual void EnsureValid()
		{
			this.CheckIfValid(true);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003534 File Offset: 0x00001734
		protected virtual bool CheckIfValid(bool throwException)
		{
			bool result = true;
			foreach (object obj in TypeDescriptor.GetProperties(this))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Attributes[typeof(RequiredPropertyAttribute)] != null && (propertyDescriptor.GetValue(this) == null || !propertyDescriptor.ShouldSerializeValue(this)))
				{
					result = false;
					if (throwException)
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "{0} missing required {1} property value for {2}.", new object[]
						{
							base.GetType().ToString(),
							propertyDescriptor.Name,
							this.ID
						}), propertyDescriptor.Name);
					}
				}
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003600 File Offset: 0x00001800
		protected virtual void RenderScriptAttributes(ScriptBehaviorDescriptor descriptor)
		{
			ComponentDescriber.DescribeComponent(this, new ScriptComponentDescriptorWrapper(descriptor), this.Page, this);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003615 File Offset: 0x00001815
		protected virtual void RenderInnerScript(ScriptBehaviorDescriptor descriptor)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003617 File Offset: 0x00001817
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!this.Enabled)
			{
				return null;
			}
			return ToolkitResourceManager.GetControlScriptReferences(base.GetType());
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000362E File Offset: 0x0000182E
		protected V GetPropertyValue<V>(string propertyName, V nullValue)
		{
			if (this.ViewState[propertyName] == null)
			{
				return nullValue;
			}
			return (V)((object)this.ViewState[propertyName]);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003651 File Offset: 0x00001851
		protected void SetPropertyValue<V>(string propertyName, V value)
		{
			this.ViewState[propertyName] = value;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003665 File Offset: 0x00001865
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.RegisterLocalization();
			if (this.Enabled && this.TargetControl.Visible)
			{
				this.SaveClientStateValues();
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003690 File Offset: 0x00001890
		private void RegisterLocalization()
		{
			string localeKey = new Localization().GetLocaleKey();
			if (string.IsNullOrEmpty(localeKey))
			{
				return;
			}
			string script = string.Format("Sys.Extended.UI.Localization.SetLocale(\"{0}\");", localeKey);
			this.Page.ClientScript.RegisterStartupScript(base.GetType(), "f93b988bab7e44ffbcff635ee599ade2", script, true);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000036DA File Offset: 0x000018DA
		protected override void OnLoad(EventArgs e)
		{
			if (!this._loadedClientStateValues)
			{
				this.LoadClientStateValues();
			}
			base.OnLoad(e);
			ToolkitResourceManager.RegisterCssReferences(this);
		}

		// Token: 0x0400001A RID: 26
		private Dictionary<string, Control> _findControlHelperCache = new Dictionary<string, Control>();

		// Token: 0x0400001B RID: 27
		private string _clientState;

		// Token: 0x0400001C RID: 28
		private bool _enableClientState;

		// Token: 0x0400001D RID: 29
		private bool _loadedClientStateValues;
	}
}
