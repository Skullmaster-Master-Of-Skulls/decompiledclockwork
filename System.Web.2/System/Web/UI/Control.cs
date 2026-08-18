using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200025F RID: 607
	[Bindable(true)]
	[DefaultProperty("ID")]
	[DesignerCategory("Code")]
	[Designer("System.Web.UI.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.ControlCodeDomSerializer, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	[ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Require)]
	[ToolboxItem("System.Web.UI.Design.WebControlToolboxItem, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Control : IComponent, IDisposable, IParserAccessor, IUrlResolutionService, IDataBindingsAccessor, IControlBuilderAccessor, IControlDesignerAccessor, IExpressionsAccessor
	{
		// Token: 0x06001BC0 RID: 7104 RVA: 0x00057571 File Offset: 0x00055771
		public Control()
		{
			if (this is INamingContainer)
			{
				this.flags.Set(128);
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00057591 File Offset: 0x00055791
		// (set) Token: 0x06001BC2 RID: 7106 RVA: 0x000575A5 File Offset: 0x000557A5
		private ClientIDMode ClientIDModeValue
		{
			get
			{
				return (ClientIDMode)this.flags[100663296, 25];
			}
			set
			{
				this.flags[100663296, 25] = (int)value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x000575BA File Offset: 0x000557BA
		// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x000575C2 File Offset: 0x000557C2
		[DefaultValue(ClientIDMode.Inherit)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_ClientIDMode")]
		public virtual ClientIDMode ClientIDMode
		{
			get
			{
				return this.ClientIDModeValue;
			}
			set
			{
				if (this.ClientIDModeValue != value)
				{
					if (value != this.EffectiveClientIDModeValue)
					{
						this.ClearEffectiveClientIDMode();
						this.ClearCachedClientID();
					}
					this.ClientIDModeValue = value;
				}
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x000575E9 File Offset: 0x000557E9
		// (set) Token: 0x06001BC6 RID: 7110 RVA: 0x000575FD File Offset: 0x000557FD
		private ClientIDMode EffectiveClientIDModeValue
		{
			get
			{
				return (ClientIDMode)this.flags[402653184, 27];
			}
			set
			{
				this.flags[402653184, 27] = (int)value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x00057614 File Offset: 0x00055814
		internal virtual ClientIDMode EffectiveClientIDMode
		{
			get
			{
				if (this.EffectiveClientIDModeValue == ClientIDMode.Inherit)
				{
					this.EffectiveClientIDModeValue = this.ClientIDMode;
					if (this.EffectiveClientIDModeValue == ClientIDMode.Inherit)
					{
						if (this.NamingContainer != null)
						{
							this.EffectiveClientIDModeValue = this.NamingContainer.EffectiveClientIDMode;
						}
						else
						{
							HttpContext context = this.Context;
							if (context != null)
							{
								this.EffectiveClientIDModeValue = RuntimeConfig.GetConfig(context).Pages.ClientIDMode;
							}
							else
							{
								this.EffectiveClientIDModeValue = RuntimeConfig.GetConfig().Pages.ClientIDMode;
							}
						}
					}
				}
				return this.EffectiveClientIDModeValue;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00057698 File Offset: 0x00055898
		internal string UniqueClientID
		{
			get
			{
				string uniqueID = this.UniqueID;
				if (uniqueID != null && uniqueID.IndexOf(this.IdSeparator) >= 0)
				{
					return uniqueID.Replace(this.IdSeparator, '_');
				}
				return uniqueID;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000576CE File Offset: 0x000558CE
		internal string StaticClientID
		{
			get
			{
				string result;
				if (!this.flags[2097152])
				{
					if ((result = this.ID) == null)
					{
						return string.Empty;
					}
				}
				else
				{
					result = string.Empty;
				}
				return result;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000576F7 File Offset: 0x000558F7
		// (set) Token: 0x06001BCB RID: 7115 RVA: 0x00057734 File Offset: 0x00055934
		internal ControlAdapter AdapterInternal
		{
			get
			{
				if (this._occasionalFields == null || this._occasionalFields.RareFields == null || this._occasionalFields.RareFields.Adapter == null)
				{
					return null;
				}
				return this._occasionalFields.RareFields.Adapter;
			}
			set
			{
				if (value != null)
				{
					this.RareFieldsEnsured.Adapter = value;
					return;
				}
				if (this._occasionalFields != null && this._occasionalFields.RareFields != null && this._occasionalFields.RareFields.Adapter != null)
				{
					this._occasionalFields.RareFields.Adapter = null;
				}
			}
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x0005778C File Offset: 0x0005598C
		private string GetClientID()
		{
			ClientIDMode clientIDMode = this.EffectiveClientIDMode;
			if (clientIDMode == ClientIDMode.Predictable)
			{
				return this.PredictableClientID;
			}
			if (clientIDMode != ClientIDMode.Static)
			{
				return this.UniqueClientID;
			}
			return this.StaticClientID;
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x000577C0 File Offset: 0x000559C0
		private string GetPredictableClientIDPrefix()
		{
			Control namingContainer = this.NamingContainer;
			string text;
			if (namingContainer != null)
			{
				if (this._id == null)
				{
					this.GenerateAutomaticID();
				}
				if (namingContainer is Page || namingContainer is MasterPage)
				{
					text = this._id;
				}
				else
				{
					text = namingContainer.GetClientID();
					if (string.IsNullOrEmpty(text))
					{
						text = this._id;
					}
					else if (!string.IsNullOrEmpty(this._id) && (!(this is IDataItemContainer) || this is IDataBoundItemControl))
					{
						text = text + "_" + this._id;
					}
				}
			}
			else
			{
				text = this._id;
			}
			return text;
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00057850 File Offset: 0x00055A50
		private string GetPredictableClientIDSuffix()
		{
			string text = null;
			Control dataItemContainer = this.DataItemContainer;
			if (dataItemContainer != null && !(dataItemContainer is IDataBoundItemControl) && (!(this is IDataItemContainer) || this is IDataBoundItemControl))
			{
				Control dataKeysContainer = dataItemContainer.DataKeysContainer;
				if (dataKeysContainer != null && ((IDataKeysControl)dataKeysContainer).ClientIDRowSuffix != null && ((IDataKeysControl)dataKeysContainer).ClientIDRowSuffix.Length != 0)
				{
					text = string.Empty;
					IOrderedDictionary values = ((IDataKeysControl)dataKeysContainer).ClientIDRowSuffixDataKeys[((IDataItemContainer)dataItemContainer).DisplayIndex].Values;
					foreach (string key in ((IDataKeysControl)dataKeysContainer).ClientIDRowSuffix)
					{
						text = text + "_" + values[key].ToString();
					}
				}
				else
				{
					int displayIndex = ((IDataItemContainer)dataItemContainer).DisplayIndex;
					if (displayIndex >= 0)
					{
						text = "_" + displayIndex.ToString(CultureInfo.InvariantCulture);
					}
				}
			}
			return text;
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00057948 File Offset: 0x00055B48
		internal string PredictableClientID
		{
			get
			{
				if (this._cachedPredictableID != null)
				{
					return this._cachedPredictableID;
				}
				this._cachedPredictableID = this.GetPredictableClientIDPrefix();
				string predictableClientIDSuffix = this.GetPredictableClientIDSuffix();
				if (!string.IsNullOrEmpty(predictableClientIDSuffix))
				{
					if (!string.IsNullOrEmpty(this._cachedPredictableID))
					{
						this._cachedPredictableID += predictableClientIDSuffix;
					}
					else
					{
						this._cachedPredictableID = predictableClientIDSuffix.Substring(1);
					}
				}
				if (!string.IsNullOrEmpty(this._cachedPredictableID))
				{
					return this._cachedPredictableID;
				}
				return string.Empty;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000579C6 File Offset: 0x00055BC6
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_ClientID")]
		public virtual string ClientID
		{
			get
			{
				if (this.EffectiveClientIDMode != ClientIDMode.Static)
				{
					this.EnsureID();
				}
				return this.GetClientID();
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x000579DD File Offset: 0x00055BDD
		protected char ClientIDSeparator
		{
			get
			{
				return '_';
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001BD2 RID: 7122 RVA: 0x000579E1 File Offset: 0x00055BE1
		// (remove) Token: 0x06001BD3 RID: 7123 RVA: 0x000579F4 File Offset: 0x00055BF4
		[WebSysDescription("Control_OnDisposed")]
		public event EventHandler Disposed
		{
			add
			{
				this.Events.AddHandler(Control.EventDisposed, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.EventDisposed, value);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00057A08 File Offset: 0x00055C08
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal virtual HttpContext Context
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.Context;
				}
				return HttpContext.Current;
			}
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00057A2C File Offset: 0x00055C2C
		protected virtual ControlAdapter ResolveAdapter()
		{
			if (this.flags[32768])
			{
				return this.AdapterInternal;
			}
			if (this.DesignMode)
			{
				this.flags.Set(32768);
				return null;
			}
			HttpContext context = this.Context;
			if (context != null && context.Request.Browser != null)
			{
				this.AdapterInternal = context.Request.Browser.GetAdapter(this);
			}
			this.flags.Set(32768);
			return this.AdapterInternal;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00057AB0 File Offset: 0x00055CB0
		protected ControlAdapter Adapter
		{
			get
			{
				if (this.flags[32768])
				{
					return this.AdapterInternal;
				}
				this.AdapterInternal = this.ResolveAdapter();
				this.flags.Set(32768);
				return this.AdapterInternal;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00057AF0 File Offset: 0x00055CF0
		protected internal bool DesignMode
		{
			get
			{
				if (!this.flags[131072])
				{
					Page page = this.Page;
					if (page != null)
					{
						if (page.GetDesignModeInternal())
						{
							this.flags.Set(65536);
						}
						else
						{
							this.flags.Clear(65536);
						}
					}
					else if (this.Site != null)
					{
						if (this.Site.DesignMode)
						{
							this.flags.Set(65536);
						}
						else
						{
							this.flags.Clear(65536);
						}
					}
					else if (this.Parent != null && this.Parent.DesignMode)
					{
						this.flags.Set(65536);
					}
					this.flags.Set(131072);
				}
				return this.flags[65536];
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00057BC6 File Offset: 0x00055DC6
		internal void ValidateEvent(string uniqueID)
		{
			this.ValidateEvent(uniqueID, string.Empty);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00057BD4 File Offset: 0x00055DD4
		internal void ValidateEvent(string uniqueID, string eventArgument)
		{
			if (this.Page != null && this.SupportsEventValidation)
			{
				this.Page.ClientScript.ValidateEvent(uniqueID, eventArgument);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00057BF8 File Offset: 0x00055DF8
		private bool SupportsEventValidation
		{
			get
			{
				return SupportsEventValidationAttribute.SupportsEventValidation(base.GetType());
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001BDB RID: 7131 RVA: 0x00057C05 File Offset: 0x00055E05
		protected EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x00057C20 File Offset: 0x00055E20
		protected bool HasEvents()
		{
			return this._events != null;
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x00057C2B File Offset: 0x00055E2B
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x00057C58 File Offset: 0x00055E58
		[ParenthesizePropertyName(true)]
		[MergableProperty(false)]
		[Filterable(false)]
		[Themeable(false)]
		[WebSysDescription("Control_ID")]
		public virtual string ID
		{
			get
			{
				if (!this.flags[1] && !this.flags[2048])
				{
					return null;
				}
				return this._id;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					value = null;
				}
				string id = this._id;
				this._id = value;
				this.ClearCachedUniqueIDRecursive();
				this.flags.Set(1);
				this.flags.Clear(2097152);
				if (this._namingContainer != null && id != null)
				{
					this._namingContainer.DirtyNameTable();
				}
				if (id != null && id != this._id)
				{
					this.ClearCachedClientID();
				}
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x00057CD0 File Offset: 0x00055ED0
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x00057D2C File Offset: 0x00055F2C
		[Browsable(false)]
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_EnableTheming")]
		public virtual bool EnableTheming
		{
			get
			{
				if (this.flags[8192])
				{
					return !this.flags[4096];
				}
				if (this.Parent != null)
				{
					return this.Parent.EnableTheming;
				}
				return !this.flags[4096];
			}
			set
			{
				if (this._controlState >= ControlState.FrameworkInitialized && !this.DesignMode)
				{
					throw new InvalidOperationException(SR.GetString("PropertySetBeforePreInitOrAddToControls", new object[]
					{
						"EnableTheming"
					}));
				}
				if (!value)
				{
					this.flags.Set(4096);
				}
				else
				{
					this.flags.Clear(4096);
				}
				this.flags.Set(8192);
			}
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x00057D9D File Offset: 0x00055F9D
		internal bool ShouldSerializeEnableTheming()
		{
			return this.flags[8192];
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x00057DAF File Offset: 0x00055FAF
		internal bool IsBindingContainer
		{
			get
			{
				return this is INamingContainer && !(this is INonBindingContainer);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00057DC7 File Offset: 0x00055FC7
		protected internal bool IsChildControlStateCleared
		{
			get
			{
				return this.flags[262144];
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001BE4 RID: 7140 RVA: 0x00057DD9 File Offset: 0x00055FD9
		// (set) Token: 0x06001BE5 RID: 7141 RVA: 0x00057E08 File Offset: 0x00056008
		[Browsable(false)]
		[DefaultValue("")]
		[Filterable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_SkinId")]
		public virtual string SkinID
		{
			get
			{
				if (this._occasionalFields == null)
				{
					return string.Empty;
				}
				if (this._occasionalFields.SkinId != null)
				{
					return this._occasionalFields.SkinId;
				}
				return string.Empty;
			}
			set
			{
				if (!this.DesignMode)
				{
					if (this.flags[16384])
					{
						throw new InvalidOperationException(SR.GetString("PropertySetBeforeStyleSheetApplied", new object[]
						{
							"SkinId"
						}));
					}
					if (this._controlState >= ControlState.FrameworkInitialized)
					{
						throw new InvalidOperationException(SR.GetString("PropertySetBeforePreInitOrAddToControls", new object[]
						{
							"SkinId"
						}));
					}
				}
				this.EnsureOccasionalFields();
				this._occasionalFields.SkinId = value;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001BE6 RID: 7142 RVA: 0x00057E88 File Offset: 0x00056088
		private Control.ControlRareFields RareFieldsEnsured
		{
			get
			{
				this.EnsureOccasionalFields();
				Control.ControlRareFields controlRareFields = this._occasionalFields.RareFields;
				if (controlRareFields == null)
				{
					controlRareFields = new Control.ControlRareFields();
					this._occasionalFields.RareFields = controlRareFields;
				}
				return controlRareFields;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x00057EBD File Offset: 0x000560BD
		private Control.ControlRareFields RareFields
		{
			get
			{
				if (this._occasionalFields != null)
				{
					return this._occasionalFields.RareFields;
				}
				return null;
			}
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x00057ED4 File Offset: 0x000560D4
		private void EnsureOccasionalFields()
		{
			if (this._occasionalFields == null)
			{
				this._occasionalFields = new Control.OccasionalFields();
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x00057EE9 File Offset: 0x000560E9
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x00057EFA File Offset: 0x000560FA
		[DefaultValue(true)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_MaintainState")]
		public virtual bool EnableViewState
		{
			get
			{
				return !this.flags[4];
			}
			set
			{
				this.SetEnableViewStateInternal(value);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x00057F03 File Offset: 0x00056103
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x00057F30 File Offset: 0x00056130
		[DefaultValue(ViewStateMode.Inherit)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_ViewStateMode")]
		public virtual ViewStateMode ViewStateMode
		{
			get
			{
				if (!this.flags[8388608])
				{
					return ViewStateMode.Inherit;
				}
				if (!this.flags[16777216])
				{
					return ViewStateMode.Disabled;
				}
				return ViewStateMode.Enabled;
			}
			set
			{
				if (value < ViewStateMode.Inherit || value > ViewStateMode.Disabled)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value == ViewStateMode.Inherit)
				{
					this.flags.Clear(8388608);
					return;
				}
				this.flags.Set(8388608);
				if (value == ViewStateMode.Enabled)
				{
					this.flags.Set(16777216);
					return;
				}
				this.flags.Clear(16777216);
			}
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00057F99 File Offset: 0x00056199
		internal void SetEnableViewStateInternal(bool value)
		{
			if (!value)
			{
				this.flags.Set(4);
				return;
			}
			this.flags.Clear(4);
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00057FB8 File Offset: 0x000561B8
		protected internal bool IsViewStateEnabled
		{
			get
			{
				for (Control control = this; control != null; control = control.Parent)
				{
					if (!control.EnableViewState)
					{
						return false;
					}
					ViewStateMode viewStateMode = control.ViewStateMode;
					if (viewStateMode != ViewStateMode.Inherit)
					{
						return viewStateMode == ViewStateMode.Enabled;
					}
				}
				return true;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x00057FF0 File Offset: 0x000561F0
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_NamingContainer")]
		public virtual Control NamingContainer
		{
			get
			{
				if (this._namingContainer == null && this.Parent != null)
				{
					if (this.Parent.flags[128])
					{
						this._namingContainer = this.Parent;
					}
					else
					{
						this._namingContainer = this.Parent.NamingContainer;
					}
				}
				return this._namingContainer;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0005804C File Offset: 0x0005624C
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control BindingContainer
		{
			get
			{
				Control control = this.NamingContainer;
				while (control is INonBindingContainer)
				{
					control = control.BindingContainer;
				}
				return control;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00058074 File Offset: 0x00056274
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control DataItemContainer
		{
			get
			{
				Control control = this.NamingContainer;
				while (control != null && !(control is IDataItemContainer))
				{
					control = control.DataItemContainer;
				}
				return control;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000580A0 File Offset: 0x000562A0
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control DataKeysContainer
		{
			get
			{
				Control control = this.NamingContainer;
				while (control != null && !(control is IDataKeysControl))
				{
					control = control.DataKeysContainer;
				}
				return control;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000580C9 File Offset: 0x000562C9
		protected char IdSeparator
		{
			get
			{
				if (this.Page != null)
				{
					return this.Page.IdSeparator;
				}
				return this.IdSeparatorFromConfig;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000580E5 File Offset: 0x000562E5
		internal char IdSeparatorFromConfig
		{
			get
			{
				if (!this.EnableLegacyRendering)
				{
					return '$';
				}
				return ':';
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x000580F4 File Offset: 0x000562F4
		protected bool LoadViewStateByID
		{
			get
			{
				return ViewStateModeByIdAttribute.IsEnabled(base.GetType());
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x00058101 File Offset: 0x00056301
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x0005812A File Offset: 0x0005632A
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_Page")]
		public virtual Page Page
		{
			get
			{
				if (this._page == null && this.Parent != null)
				{
					this._page = this.Parent.Page;
				}
				return this._page;
			}
			set
			{
				if (this.OwnerControl != null)
				{
					throw new InvalidOperationException();
				}
				this._page = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x00058141 File Offset: 0x00056341
		// (set) Token: 0x06001BF9 RID: 7161 RVA: 0x00058180 File Offset: 0x00056380
		internal RouteCollection RouteCollection
		{
			get
			{
				if (this._occasionalFields == null || this._occasionalFields.RareFields == null || this._occasionalFields.RareFields.RouteCollection == null)
				{
					return RouteTable.Routes;
				}
				return this._occasionalFields.RareFields.RouteCollection;
			}
			set
			{
				if (value != null)
				{
					this.RareFieldsEnsured.RouteCollection = value;
					return;
				}
				if (this._occasionalFields != null && this._occasionalFields.RareFields != null && this._occasionalFields.RareFields.RouteCollection != null)
				{
					this._occasionalFields.RareFields.RouteCollection = null;
				}
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x00007722 File Offset: 0x00005922
		internal virtual bool IsReloadable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x000581D8 File Offset: 0x000563D8
		internal bool EnableLegacyRendering
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.XhtmlConformanceMode == XhtmlConformanceMode.Legacy;
				}
				return !this.DesignMode && this.Adapter == null && this.GetXhtmlConformanceSection().Mode == XhtmlConformanceMode.Legacy;
			}
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0005821C File Offset: 0x0005641C
		internal XhtmlConformanceSection GetXhtmlConformanceSection()
		{
			HttpContext context = this.Context;
			XhtmlConformanceSection xhtmlConformance;
			if (context != null)
			{
				xhtmlConformance = RuntimeConfig.GetConfig(context).XhtmlConformance;
			}
			else
			{
				xhtmlConformance = RuntimeConfig.GetConfig().XhtmlConformance;
			}
			return xhtmlConformance;
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00058250 File Offset: 0x00056450
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x000582AC File Offset: 0x000564AC
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Version RenderingCompatibility
		{
			get
			{
				if (this._occasionalFields == null || this._occasionalFields.RareFields == null || this._occasionalFields.RareFields.RenderingCompatibility == null)
				{
					return this.RuntimeConfig.Pages.ControlRenderingCompatibilityVersion;
				}
				return this._occasionalFields.RareFields.RenderingCompatibility;
			}
			set
			{
				if (value != null)
				{
					this.RareFieldsEnsured.RenderingCompatibility = value;
					return;
				}
				if (this._occasionalFields != null && this._occasionalFields.RareFields != null && this._occasionalFields.RareFields.RenderingCompatibility != null)
				{
					this._occasionalFields.RareFields.RenderingCompatibility = null;
				}
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00058310 File Offset: 0x00056510
		private RuntimeConfig RuntimeConfig
		{
			get
			{
				HttpContext context = this.Context;
				if (context != null)
				{
					return RuntimeConfig.GetConfig(context);
				}
				return RuntimeConfig.GetConfig();
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00058333 File Offset: 0x00056533
		public string GetRouteUrl(object routeParameters)
		{
			return this.GetRouteUrl(new RouteValueDictionary(routeParameters));
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00058341 File Offset: 0x00056541
		public string GetRouteUrl(string routeName, object routeParameters)
		{
			return this.GetRouteUrl(routeName, new RouteValueDictionary(routeParameters));
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00058350 File Offset: 0x00056550
		public string GetRouteUrl(RouteValueDictionary routeParameters)
		{
			return this.GetRouteUrl(null, routeParameters);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0005835C File Offset: 0x0005655C
		public string GetRouteUrl(string routeName, RouteValueDictionary routeParameters)
		{
			VirtualPathData virtualPath = this.RouteCollection.GetVirtualPath(this.Context.Request.RequestContext, routeName, routeParameters);
			if (virtualPath != null)
			{
				return virtualPath.VirtualPath;
			}
			return null;
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00058394 File Offset: 0x00056594
		internal virtual TemplateControl GetTemplateControl()
		{
			if ((this._occasionalFields == null || this._occasionalFields.TemplateControl == null) && this.Parent != null)
			{
				TemplateControl templateControl = this.Parent.GetTemplateControl();
				if (templateControl != null)
				{
					this.EnsureOccasionalFields();
					this._occasionalFields.TemplateControl = templateControl;
				}
			}
			if (this._occasionalFields == null)
			{
				return null;
			}
			return this._occasionalFields.TemplateControl;
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x000583F4 File Offset: 0x000565F4
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x000583FC File Offset: 0x000565FC
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_TemplateControl")]
		public TemplateControl TemplateControl
		{
			get
			{
				return this.GetTemplateControl();
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			set
			{
				if (value != null)
				{
					this.EnsureOccasionalFields();
					this._occasionalFields.TemplateControl = value;
					return;
				}
				if (this._occasionalFields != null && this._occasionalFields.TemplateControl != null)
				{
					this._occasionalFields.TemplateControl = null;
				}
			}
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00058438 File Offset: 0x00056638
		internal bool IsDescendentOf(Control ancestor)
		{
			Control control = this;
			while (control != ancestor && control.Parent != null)
			{
				control = control.Parent;
			}
			return control == ancestor;
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00058460 File Offset: 0x00056660
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_Parent")]
		public virtual Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x00058468 File Offset: 0x00056668
		internal bool IsParentedToUpdatePanel
		{
			get
			{
				for (Control parent = this.Parent; parent != null; parent = parent.Parent)
				{
					if (parent is IUpdatePanel)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00058493 File Offset: 0x00056693
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_TemplateSourceDirectory")]
		public virtual string TemplateSourceDirectory
		{
			get
			{
				if (this.TemplateControlVirtualDirectory == null)
				{
					return string.Empty;
				}
				return this.TemplateControlVirtualDirectory.VirtualPathStringNoTrailingSlash;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000584B4 File Offset: 0x000566B4
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x000584C1 File Offset: 0x000566C1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_TemplateSourceDirectory")]
		public string AppRelativeTemplateSourceDirectory
		{
			get
			{
				return VirtualPath.GetAppRelativeVirtualPathStringOrEmpty(this.TemplateControlVirtualDirectory);
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			set
			{
				this.TemplateControlVirtualDirectory = VirtualPath.CreateNonRelativeAllowNull(value);
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x000584D0 File Offset: 0x000566D0
		// (set) Token: 0x06001C0E RID: 7182 RVA: 0x00058598 File Offset: 0x00056798
		internal VirtualPath TemplateControlVirtualDirectory
		{
			get
			{
				if (this._occasionalFields != null && this._occasionalFields.TemplateSourceVirtualDirectory != null)
				{
					return this._occasionalFields.TemplateSourceVirtualDirectory;
				}
				TemplateControl templateControl = this.TemplateControl;
				if (templateControl == null)
				{
					HttpContext context = this.Context;
					if (context != null)
					{
						VirtualPath parent = context.Request.CurrentExecutionFilePathObject.Parent;
						if (parent != null)
						{
							this.EnsureOccasionalFields();
							this._occasionalFields.TemplateSourceVirtualDirectory = parent;
						}
					}
					if (this._occasionalFields == null)
					{
						return null;
					}
					return this._occasionalFields.TemplateSourceVirtualDirectory;
				}
				else
				{
					if (templateControl != this)
					{
						VirtualPath templateControlVirtualDirectory = templateControl.TemplateControlVirtualDirectory;
						if (templateControlVirtualDirectory != null)
						{
							this.EnsureOccasionalFields();
							this._occasionalFields.TemplateSourceVirtualDirectory = templateControlVirtualDirectory;
						}
					}
					if (this._occasionalFields == null)
					{
						return null;
					}
					return this._occasionalFields.TemplateSourceVirtualDirectory;
				}
			}
			set
			{
				if (value != null)
				{
					this.EnsureOccasionalFields();
					this._occasionalFields.TemplateSourceVirtualDirectory = value;
					return;
				}
				if (this._occasionalFields != null && this._occasionalFields.TemplateSourceVirtualDirectory != null)
				{
					this._occasionalFields.TemplateSourceVirtualDirectory = null;
				}
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x000585E8 File Offset: 0x000567E8
		// (set) Token: 0x06001C10 RID: 7184 RVA: 0x000585F0 File Offset: 0x000567F0
		internal ControlState ControlState
		{
			get
			{
				return this._controlState;
			}
			set
			{
				this._controlState = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000585F9 File Offset: 0x000567F9
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00058624 File Offset: 0x00056824
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[WebSysDescription("Control_Site")]
		public ISite Site
		{
			get
			{
				if (this.OwnerControl != null)
				{
					return this.OwnerControl.Site;
				}
				if (this.RareFields != null)
				{
					return this.RareFields.Site;
				}
				return null;
			}
			set
			{
				if (this.OwnerControl != null)
				{
					throw new InvalidOperationException(SR.GetString("Substitution_SiteNotAllowed"));
				}
				this.RareFieldsEnsured.Site = value;
				this.flags.Clear(131072);
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0005865A File Offset: 0x0005685A
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0005868C File Offset: 0x0005688C
		[Bindable(true)]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("Control_Visible")]
		public virtual bool Visible
		{
			get
			{
				return !this.flags[16] && (this._parent == null || this.DesignMode || this._parent.Visible);
			}
			set
			{
				if (this.flags[2])
				{
					bool flag = !this.flags[16];
					if (flag != value)
					{
						this.flags.Set(32);
					}
				}
				if (!value)
				{
					this.flags.Set(16);
					return;
				}
				this.flags.Clear(16);
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000586E7 File Offset: 0x000568E7
		private void ResetVisible()
		{
			this.Visible = true;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000586F0 File Offset: 0x000568F0
		private bool ShouldSerializeVisible()
		{
			return this.flags[16];
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x00058700 File Offset: 0x00056900
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_UniqueID")]
		public virtual string UniqueID
		{
			get
			{
				if (this._cachedUniqueID != null)
				{
					return this._cachedUniqueID;
				}
				Control namingContainer = this.NamingContainer;
				if (namingContainer != null)
				{
					if (this._id == null)
					{
						this.GenerateAutomaticID();
					}
					if (this.Page == namingContainer)
					{
						this._cachedUniqueID = this._id;
					}
					else
					{
						string uniqueIDPrefix = namingContainer.GetUniqueIDPrefix();
						if (uniqueIDPrefix.Length == 0)
						{
							return this._id;
						}
						this._cachedUniqueID = uniqueIDPrefix + this._id;
					}
					return this._cachedUniqueID;
				}
				return this._id;
			}
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00058780 File Offset: 0x00056980
		public string GetUniqueIDRelativeTo(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!this.IsDescendentOf(control.NamingContainer))
			{
				throw new InvalidOperationException(SR.GetString("Control_NotADescendentOfNamingContainer", new object[]
				{
					control.ID
				}));
			}
			if (control.NamingContainer == this.Page)
			{
				return this.UniqueID;
			}
			return this.UniqueID.Substring(control.NamingContainer.UniqueID.Length + 1);
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001C19 RID: 7193 RVA: 0x000587FA File Offset: 0x000569FA
		// (remove) Token: 0x06001C1A RID: 7194 RVA: 0x0005880D File Offset: 0x00056A0D
		[WebCategory("Data")]
		[WebSysDescription("Control_OnDataBind")]
		public event EventHandler DataBinding
		{
			add
			{
				this.Events.AddHandler(Control.EventDataBinding, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.EventDataBinding, value);
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001C1B RID: 7195 RVA: 0x00058820 File Offset: 0x00056A20
		// (remove) Token: 0x06001C1C RID: 7196 RVA: 0x00058833 File Offset: 0x00056A33
		[WebSysDescription("Control_OnInit")]
		public event EventHandler Init
		{
			add
			{
				this.Events.AddHandler(Control.EventInit, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.EventInit, value);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001C1D RID: 7197 RVA: 0x00058846 File Offset: 0x00056A46
		// (remove) Token: 0x06001C1E RID: 7198 RVA: 0x00058859 File Offset: 0x00056A59
		[WebSysDescription("Control_OnLoad")]
		public event EventHandler Load
		{
			add
			{
				this.Events.AddHandler(Control.EventLoad, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.EventLoad, value);
			}
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001C1F RID: 7199 RVA: 0x0005886C File Offset: 0x00056A6C
		// (remove) Token: 0x06001C20 RID: 7200 RVA: 0x0005887F File Offset: 0x00056A7F
		[WebSysDescription("Control_OnPreRender")]
		public event EventHandler PreRender
		{
			add
			{
				this.Events.AddHandler(Control.EventPreRender, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.EventPreRender, value);
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001C21 RID: 7201 RVA: 0x00058892 File Offset: 0x00056A92
		// (remove) Token: 0x06001C22 RID: 7202 RVA: 0x000588A5 File Offset: 0x00056AA5
		[WebSysDescription("Control_OnUnload")]
		public event EventHandler Unload
		{
			add
			{
				this.Events.AddHandler(Control.EventUnload, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.EventUnload, value);
			}
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000588B8 File Offset: 0x00056AB8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void ApplyStyleSheetSkin(Page page)
		{
			if (page == null)
			{
				return;
			}
			if (this.flags[16384])
			{
				throw new InvalidOperationException(SR.GetString("StyleSheetAreadyAppliedOnControl"));
			}
			if (page.ApplyControlStyleSheet(this))
			{
				this.flags.Set(16384);
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00058904 File Offset: 0x00056B04
		private void ApplySkin(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			if (this.flags[1024])
			{
				return;
			}
			if (ThemeableAttribute.IsTypeThemeable(base.GetType()))
			{
				page.ApplyControlSkin(this);
				this.flags.Set(1024);
			}
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00058958 File Offset: 0x00056B58
		protected virtual void OnDataBinding(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._events[Control.EventDataBinding] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0005898E File Offset: 0x00056B8E
		public virtual void DataBind()
		{
			this.DataBind(true);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x00058998 File Offset: 0x00056B98
		protected virtual void DataBind(bool raiseOnDataBinding)
		{
			bool flag = false;
			if (this.IsBindingContainer)
			{
				bool flag2;
				object dataItem = DataBinder.GetDataItem(this, out flag2);
				if (flag2 && this.Page != null)
				{
					this.Page.PushDataBindingContext(dataItem);
					flag = true;
				}
			}
			try
			{
				if (raiseOnDataBinding)
				{
					this.OnDataBinding(EventArgs.Empty);
				}
				this.DataBindChildren();
			}
			finally
			{
				if (flag)
				{
					this.Page.PopDataBindingContext();
				}
			}
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00058A08 File Offset: 0x00056C08
		protected virtual void DataBindChildren()
		{
			if (this.HasControls())
			{
				string collectionReadOnly = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
				try
				{
					try
					{
						int count = this._controls.Count;
						for (int i = 0; i < count; i++)
						{
							this._controls[i].DataBind();
						}
					}
					finally
					{
						this._controls.SetCollectionReadOnly(collectionReadOnly);
					}
				}
				catch
				{
					throw;
				}
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x00058A88 File Offset: 0x00056C88
		internal void PreventAutoID()
		{
			if (!this.flags[128])
			{
				this.flags.Set(64);
			}
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x00058AAC File Offset: 0x00056CAC
		protected virtual void AddParsedSubObject(object obj)
		{
			Control control = obj as Control;
			if (control != null)
			{
				this.Controls.Add(control);
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00058ACF File Offset: 0x00056CCF
		private void UpdateNamingContainer(Control namingContainer)
		{
			if (this._namingContainer == null || (this._namingContainer != null && this._namingContainer != namingContainer))
			{
				this.ClearCachedUniqueIDRecursive();
			}
			if (this.EffectiveClientIDModeValue != ClientIDMode.Inherit)
			{
				this.ClearCachedClientID();
				this.ClearEffectiveClientIDMode();
			}
			this._namingContainer = namingContainer;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x00058B0C File Offset: 0x00056D0C
		private void ClearCachedUniqueIDRecursive()
		{
			this._cachedUniqueID = null;
			if (this._occasionalFields != null)
			{
				this._occasionalFields.UniqueIDPrefix = null;
			}
			if (this._controls != null)
			{
				int count = this._controls.Count;
				for (int i = 0; i < count; i++)
				{
					this._controls[i].ClearCachedUniqueIDRecursive();
				}
			}
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x00058B65 File Offset: 0x00056D65
		protected void EnsureID()
		{
			if (this._namingContainer != null)
			{
				if (this._id == null)
				{
					this.GenerateAutomaticID();
				}
				this.flags.Set(2048);
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00058B90 File Offset: 0x00056D90
		private void GenerateAutomaticID()
		{
			this.flags.Set(2097152);
			this._namingContainer.EnsureOccasionalFields();
			Control.OccasionalFields occasionalFields = this._namingContainer._occasionalFields;
			int namedControlsID = occasionalFields.NamedControlsID;
			occasionalFields.NamedControlsID = namedControlsID + 1;
			int num = namedControlsID;
			if (this.EnableLegacyRendering)
			{
				this._id = "_ctl" + num.ToString(NumberFormatInfo.InvariantInfo);
			}
			else if (num < 128)
			{
				this._id = Control.automaticIDs[num];
			}
			else
			{
				this._id = "ctl" + num.ToString(NumberFormatInfo.InvariantInfo);
			}
			this._namingContainer.DirtyNameTable();
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00058C38 File Offset: 0x00056E38
		internal virtual string GetUniqueIDPrefix()
		{
			this.EnsureOccasionalFields();
			if (this._occasionalFields.UniqueIDPrefix == null)
			{
				string uniqueID = this.UniqueID;
				if (!string.IsNullOrEmpty(uniqueID))
				{
					this._occasionalFields.UniqueIDPrefix = uniqueID + this.IdSeparator.ToString();
				}
				else
				{
					this._occasionalFields.UniqueIDPrefix = string.Empty;
				}
			}
			return this._occasionalFields.UniqueIDPrefix;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00058CA4 File Offset: 0x00056EA4
		protected internal virtual void OnInit(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._events[Control.EventInit] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00058CDC File Offset: 0x00056EDC
		internal virtual void InitRecursive(Control namingContainer)
		{
			this.ResolveAdapter();
			if (this._controls != null)
			{
				if (this.flags[128])
				{
					namingContainer = this;
				}
				string collectionReadOnly = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
				int count = this._controls.Count;
				for (int i = 0; i < count; i++)
				{
					Control control = this._controls[i];
					control.UpdateNamingContainer(namingContainer);
					if (control._id == null && namingContainer != null && !control.flags[64])
					{
						control.GenerateAutomaticID();
					}
					control._page = this.Page;
					control.InitRecursive(namingContainer);
				}
				this._controls.SetCollectionReadOnly(collectionReadOnly);
			}
			if (this._controlState < ControlState.Initialized)
			{
				this._controlState = ControlState.ChildrenInitialized;
				if (this.Page != null && !this.DesignMode && this.Page.ContainsTheme && this.EnableTheming)
				{
					this.ApplySkin(this.Page);
				}
				if (this.AdapterInternal != null)
				{
					this.AdapterInternal.OnInit(EventArgs.Empty);
				}
				else
				{
					this.OnInit(EventArgs.Empty);
				}
				this._controlState = ControlState.Initialized;
			}
			this.TrackViewState();
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00058E00 File Offset: 0x00057000
		internal Task InitRecursiveAsync(Control namingContainer, Page page)
		{
			Control.<InitRecursiveAsync>d__225 <InitRecursiveAsync>d__;
			<InitRecursiveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitRecursiveAsync>d__.<>4__this = this;
			<InitRecursiveAsync>d__.namingContainer = namingContainer;
			<InitRecursiveAsync>d__.page = page;
			<InitRecursiveAsync>d__.<>1__state = -1;
			<InitRecursiveAsync>d__.<>t__builder.Start<Control.<InitRecursiveAsync>d__225>(ref <InitRecursiveAsync>d__);
			return <InitRecursiveAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00058E53 File Offset: 0x00057053
		protected void ClearChildState()
		{
			this.ClearChildControlState();
			this.ClearChildViewState();
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00058E61 File Offset: 0x00057061
		protected void ClearChildControlState()
		{
			if (this.ControlState < ControlState.Initialized)
			{
				return;
			}
			this.flags.Set(262144);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresClearChildControlState(this);
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00058E91 File Offset: 0x00057091
		protected void ClearChildViewState()
		{
			if (this._occasionalFields != null)
			{
				this._occasionalFields.ControlsViewState = null;
			}
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00058EA8 File Offset: 0x000570A8
		protected void ClearEffectiveClientIDMode()
		{
			this.EffectiveClientIDModeValue = ClientIDMode.Inherit;
			if (this.HasControls())
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					control.ClearEffectiveClientIDMode();
				}
			}
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00058F10 File Offset: 0x00057110
		protected void ClearCachedClientID()
		{
			this._cachedPredictableID = null;
			if (this.HasControls())
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					control.ClearCachedClientID();
				}
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x00058F78 File Offset: 0x00057178
		protected bool HasChildViewState
		{
			get
			{
				return this._occasionalFields != null && this._occasionalFields.ControlsViewState != null && this._occasionalFields.ControlsViewState.Count > 0;
			}
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00058FA4 File Offset: 0x000571A4
		public virtual void Focus()
		{
			this.Page.SetFocus(this);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00058FB4 File Offset: 0x000571B4
		internal void LoadControlStateInternal(object savedStateObj)
		{
			if (this.flags[1048576])
			{
				return;
			}
			this.flags.Set(1048576);
			Pair pair = (Pair)savedStateObj;
			if (pair == null)
			{
				return;
			}
			Page page = this.Page;
			if (page != null && !page.ShouldLoadControlState(this))
			{
				return;
			}
			if (pair.First != null)
			{
				this.LoadControlState(pair.First);
			}
			if (this.AdapterInternal == null || pair.Second == null)
			{
				return;
			}
			this.AdapterInternal.LoadAdapterControlState(pair.Second);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void LoadControlState(object savedState)
		{
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0005903C File Offset: 0x0005723C
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
				object obj = this.ViewState["Visible"];
				if (obj != null)
				{
					if (!(bool)obj)
					{
						this.flags.Set(16);
					}
					else
					{
						this.flags.Clear(16);
					}
					this.flags.Set(32);
				}
				object obj2 = this.ViewState["ValidateRequestMode"];
				if (obj2 != null)
				{
					this.flags[1610612736, 29] = (int)obj2;
					this.flags.Set(4194304);
				}
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000590DC File Offset: 0x000572DC
		internal void LoadViewStateRecursive(object savedState)
		{
			if (savedState == null || this.flags[4])
			{
				return;
			}
			if (this.Page != null && this.Page.IsPostBack)
			{
				object obj = null;
				Pair pair = savedState as Pair;
				object first;
				ArrayList arrayList;
				if (pair != null)
				{
					first = pair.First;
					arrayList = (ArrayList)pair.Second;
				}
				else
				{
					Triplet triplet = (Triplet)savedState;
					first = triplet.First;
					obj = triplet.Second;
					arrayList = (ArrayList)triplet.Third;
				}
				try
				{
					if (obj != null && this.AdapterInternal != null)
					{
						this.AdapterInternal.LoadAdapterViewState(obj);
					}
					if (first != null)
					{
						this.LoadViewState(first);
					}
					if (arrayList != null)
					{
						if (this.LoadViewStateByID)
						{
							this.LoadChildViewStateByID(arrayList);
						}
						else
						{
							this.LoadChildViewStateByIndex(arrayList);
						}
					}
				}
				catch (InvalidCastException)
				{
					throw new HttpException(SR.GetString("Controls_Cant_Change_Between_Posts"));
				}
				catch (IndexOutOfRangeException)
				{
					throw new HttpException(SR.GetString("Controls_Cant_Change_Between_Posts"));
				}
			}
			this._controlState = ControlState.ViewStateLoaded;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000591E4 File Offset: 0x000573E4
		internal void LoadChildViewStateByID(ArrayList childState)
		{
			int count = childState.Count;
			for (int i = 0; i < count; i += 2)
			{
				string text = (string)childState[i];
				object obj = childState[i + 1];
				Control control = this.FindControl(text);
				if (control != null)
				{
					control.LoadViewStateRecursive(obj);
				}
				else
				{
					this.EnsureOccasionalFields();
					if (this._occasionalFields.ControlsViewState == null)
					{
						this._occasionalFields.ControlsViewState = new Hashtable();
					}
					this._occasionalFields.ControlsViewState[text] = obj;
				}
			}
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00059268 File Offset: 0x00057468
		internal void LoadChildViewStateByIndex(ArrayList childState)
		{
			ControlCollection controls = this.Controls;
			int count = controls.Count;
			int count2 = childState.Count;
			for (int i = 0; i < count2; i += 2)
			{
				int num = (int)childState[i];
				object obj = childState[i + 1];
				if (num < count)
				{
					controls[num].LoadViewStateRecursive(obj);
				}
				else
				{
					this.EnsureOccasionalFields();
					if (this._occasionalFields.ControlsViewState == null)
					{
						this._occasionalFields.ControlsViewState = new Hashtable();
					}
					this._occasionalFields.ControlsViewState[num] = obj;
				}
			}
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x00059301 File Offset: 0x00057501
		internal void ResolvePhysicalOrVirtualPath(string path, out VirtualPath virtualPath, out string physicalPath)
		{
			if (UrlPath.IsAbsolutePhysicalPath(path))
			{
				physicalPath = path;
				virtualPath = null;
				return;
			}
			physicalPath = null;
			virtualPath = this.TemplateControlVirtualDirectory.Combine(VirtualPath.Create(path));
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00059328 File Offset: 0x00057528
		protected internal string MapPathSecure(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath", SR.GetString("VirtualPath_Length_Zero"));
			}
			VirtualPath virtualPath2;
			string text;
			this.ResolvePhysicalOrVirtualPath(virtualPath, out virtualPath2, out text);
			if (text == null)
			{
				text = virtualPath2.MapPathInternal(this.TemplateControlVirtualDirectory, true);
			}
			HttpRuntime.CheckFilePermission(text);
			return text;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00059378 File Offset: 0x00057578
		protected internal Stream OpenFile(string path)
		{
			string text = null;
			VirtualFile virtualFile = null;
			path = path.Trim();
			if (UrlPath.IsAbsolutePhysicalPath(path))
			{
				text = path;
			}
			else
			{
				virtualFile = HostingEnvironment.VirtualPathProvider.GetFile(path);
				MapPathBasedVirtualFile mapPathBasedVirtualFile = virtualFile as MapPathBasedVirtualFile;
				if (mapPathBasedVirtualFile != null)
				{
					text = mapPathBasedVirtualFile.PhysicalPath;
				}
			}
			if (text != null)
			{
				HttpRuntime.CheckFilePermission(text);
			}
			if (virtualFile != null)
			{
				return virtualFile.Open();
			}
			return new FileStream(text, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000593D8 File Offset: 0x000575D8
		internal Stream OpenFileAndGetDependency(VirtualPath virtualPath, string physicalPath, out CacheDependency dependency)
		{
			if (physicalPath == null && HostingEnvironment.UsingMapPathBasedVirtualPathProvider)
			{
				physicalPath = virtualPath.MapPathInternal(this.TemplateControlVirtualDirectory, true);
			}
			Stream result;
			if (physicalPath != null)
			{
				HttpRuntime.CheckFilePermission(physicalPath);
				result = new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
				dependency = new CacheDependency(0, physicalPath);
			}
			else
			{
				result = virtualPath.OpenFile();
				dependency = VirtualPathProvider.GetCacheDependency(virtualPath);
			}
			return result;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x0005942C File Offset: 0x0005762C
		protected internal virtual void OnLoad(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._events[Control.EventLoad] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00059464 File Offset: 0x00057664
		internal virtual void LoadRecursive()
		{
			if (this._controlState < ControlState.Loaded)
			{
				if (this.AdapterInternal != null)
				{
					this.AdapterInternal.OnLoad(EventArgs.Empty);
				}
				else
				{
					this.OnLoad(EventArgs.Empty);
				}
			}
			if (this._controls != null)
			{
				string collectionReadOnly = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
				int count = this._controls.Count;
				for (int i = 0; i < count; i++)
				{
					this._controls[i].LoadRecursive();
				}
				this._controls.SetCollectionReadOnly(collectionReadOnly);
			}
			if (this._controlState < ControlState.Loaded)
			{
				this._controlState = ControlState.Loaded;
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00059500 File Offset: 0x00057700
		internal Task LoadRecursiveAsync(Page page)
		{
			Control.<LoadRecursiveAsync>d__246 <LoadRecursiveAsync>d__;
			<LoadRecursiveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadRecursiveAsync>d__.<>4__this = this;
			<LoadRecursiveAsync>d__.page = page;
			<LoadRecursiveAsync>d__.<>1__state = -1;
			<LoadRecursiveAsync>d__.<>t__builder.Start<Control.<LoadRecursiveAsync>d__246>(ref <LoadRecursiveAsync>d__);
			return <LoadRecursiveAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0005954C File Offset: 0x0005774C
		protected internal virtual void OnPreRender(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._events[Control.EventPreRender] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00059584 File Offset: 0x00057784
		internal virtual void PreRenderRecursiveInternal()
		{
			if (!this.Visible)
			{
				this.flags.Set(16);
			}
			else
			{
				this.flags.Clear(16);
				this.EnsureChildControls();
				if (this.AdapterInternal != null)
				{
					this.AdapterInternal.OnPreRender(EventArgs.Empty);
				}
				else
				{
					this.OnPreRender(EventArgs.Empty);
				}
				if (this._controls != null)
				{
					string collectionReadOnly = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
					int count = this._controls.Count;
					for (int i = 0; i < count; i++)
					{
						this._controls[i].PreRenderRecursiveInternal();
					}
					this._controls.SetCollectionReadOnly(collectionReadOnly);
				}
			}
			this._controlState = ControlState.PreRendered;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x0005963C File Offset: 0x0005783C
		internal Task PreRenderRecursiveInternalAsync(Page page)
		{
			Control.<PreRenderRecursiveInternalAsync>d__249 <PreRenderRecursiveInternalAsync>d__;
			<PreRenderRecursiveInternalAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PreRenderRecursiveInternalAsync>d__.<>4__this = this;
			<PreRenderRecursiveInternalAsync>d__.page = page;
			<PreRenderRecursiveInternalAsync>d__.<>1__state = -1;
			<PreRenderRecursiveInternalAsync>d__.<>t__builder.Start<Control.<PreRenderRecursiveInternalAsync>d__249>(ref <PreRenderRecursiveInternalAsync>d__);
			return <PreRenderRecursiveInternalAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x00059687 File Offset: 0x00057887
		internal int EstimateStateSize(object state)
		{
			if (state == null)
			{
				return 0;
			}
			return Util.SerializeWithAssert(new ObjectStateFormatter(), state).Length;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000596A0 File Offset: 0x000578A0
		protected void BuildProfileTree(string parentId, bool calcViewState)
		{
			calcViewState = (calcViewState && !this.flags[4]);
			int viewStateSize;
			if (calcViewState)
			{
				viewStateSize = this.EstimateStateSize(this.SaveViewState());
			}
			else
			{
				viewStateSize = 0;
			}
			int controlStateSize = 0;
			if (this.Page != null && this.Page._registeredControlsRequiringControlState != null && this.Page._registeredControlsRequiringControlState.Contains(this))
			{
				controlStateSize = this.EstimateStateSize(this.SaveControlStateInternal());
			}
			this.Page.Trace.AddNewControl(this.UniqueID, parentId, base.GetType().FullName, viewStateSize, controlStateSize);
			if (this._controls != null)
			{
				int count = this._controls.Count;
				for (int i = 0; i < count; i++)
				{
					this._controls[i].BuildProfileTree(this.UniqueID, calcViewState);
				}
			}
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x0005976C File Offset: 0x0005796C
		internal object SaveControlStateInternal()
		{
			object obj = this.SaveControlState();
			object obj2 = null;
			if (this.AdapterInternal != null)
			{
				obj2 = this.AdapterInternal.SaveAdapterControlState();
			}
			if (obj != null || obj2 != null)
			{
				return new Pair(obj, obj2);
			}
			return null;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x0000298D File Offset: 0x00000B8D
		protected internal virtual object SaveControlState()
		{
			return null;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000597A8 File Offset: 0x000579A8
		protected virtual object SaveViewState()
		{
			if (this.flags[32])
			{
				this.ViewState["Visible"] = !this.flags[16];
			}
			if (this.flags[4194304])
			{
				this.ViewState["ValidateRequestMode"] = (int)this.ValidateRequestMode;
			}
			if (this._viewState != null)
			{
				return this._viewState.SaveViewState();
			}
			return null;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0005982C File Offset: 0x00057A2C
		internal object SaveViewStateRecursive(ViewStateMode inheritedMode)
		{
			if (this.flags[4])
			{
				return null;
			}
			bool flag;
			if (this.flags[8388608])
			{
				if (this.flags[16777216])
				{
					flag = true;
					inheritedMode = ViewStateMode.Enabled;
				}
				else
				{
					flag = false;
					inheritedMode = ViewStateMode.Disabled;
				}
			}
			else
			{
				flag = (inheritedMode == ViewStateMode.Enabled);
			}
			object obj = null;
			object obj2 = null;
			if (flag)
			{
				if (this.AdapterInternal != null)
				{
					obj = this.AdapterInternal.SaveAdapterViewState();
				}
				obj2 = this.SaveViewState();
			}
			ArrayList arrayList = null;
			if (this.HasControls())
			{
				ControlCollection controls = this._controls;
				int count = controls.Count;
				bool loadViewStateByID = this.LoadViewStateByID;
				for (int i = 0; i < count; i++)
				{
					Control control = controls[i];
					object obj3 = control.SaveViewStateRecursive(inheritedMode);
					if (obj3 != null)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList(count);
						}
						if (loadViewStateByID)
						{
							control.EnsureID();
							arrayList.Add(control.ID);
						}
						else
						{
							arrayList.Add(i);
						}
						arrayList.Add(obj3);
					}
				}
			}
			if (this.AdapterInternal != null)
			{
				if (obj2 != null || obj != null || arrayList != null)
				{
					return new Triplet(obj2, obj, arrayList);
				}
			}
			else if (obj2 != null || arrayList != null)
			{
				return new Pair(obj2, arrayList);
			}
			return null;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00059958 File Offset: 0x00057B58
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00059964 File Offset: 0x00057B64
		internal void RenderChildrenInternal(HtmlTextWriter writer, ICollection children)
		{
			if (this.RareFields != null && this.RareFields.RenderMethod != null)
			{
				writer.BeginRender();
				this.RareFields.RenderMethod(writer, this);
				writer.EndRender();
				return;
			}
			if (children != null)
			{
				foreach (object obj in children)
				{
					Control control = (Control)obj;
					control.RenderControl(writer);
				}
			}
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000599F0 File Offset: 0x00057BF0
		protected internal virtual void RenderChildren(HtmlTextWriter writer)
		{
			ICollection controls = this._controls;
			this.RenderChildrenInternal(writer, controls);
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00059A0C File Offset: 0x00057C0C
		public virtual void RenderControl(HtmlTextWriter writer)
		{
			this.RenderControl(writer, this.Adapter);
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00059A1C File Offset: 0x00057C1C
		protected void RenderControl(HtmlTextWriter writer, ControlAdapter adapter)
		{
			if (this.flags[16] || this.flags[512])
			{
				this.TraceNonRenderingControlInternal(writer);
				return;
			}
			HttpContext httpContext = (this.Page == null) ? null : this.Page._context;
			if (httpContext != null && httpContext.TraceIsEnabled)
			{
				int bufferedLength = httpContext.Response.GetBufferedLength();
				this.RenderControlInternal(writer, adapter);
				int bufferedLength2 = httpContext.Response.GetBufferedLength();
				httpContext.Trace.AddControlSize(this.UniqueID, bufferedLength2 - bufferedLength);
				return;
			}
			this.RenderControlInternal(writer, adapter);
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00059AB4 File Offset: 0x00057CB4
		private void RenderControlInternal(HtmlTextWriter writer, ControlAdapter adapter)
		{
			try
			{
				this.BeginRenderTracing(writer, this);
				if (adapter != null)
				{
					adapter.BeginRender(writer);
					adapter.Render(writer);
					adapter.EndRender(writer);
				}
				else
				{
					this.Render(writer);
				}
			}
			finally
			{
				this.EndRenderTracing(writer, this);
			}
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00059B08 File Offset: 0x00057D08
		protected internal virtual void OnUnload(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._events[Control.EventUnload] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x00059B40 File Offset: 0x00057D40
		public virtual void Dispose()
		{
			if (this.Site != null)
			{
				IContainer container = (IContainer)this.Site.GetService(typeof(IContainer));
				if (container != null)
				{
					container.Remove(this);
					EventHandler eventHandler = this.Events[Control.EventDisposed] as EventHandler;
					if (eventHandler != null)
					{
						eventHandler(this, EventArgs.Empty);
					}
				}
			}
			if (this._occasionalFields != null)
			{
				this._occasionalFields.Dispose();
			}
			if (this._events != null)
			{
				this._events.Dispose();
				this._events = null;
			}
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x00059BD0 File Offset: 0x00057DD0
		internal virtual void UnloadRecursive(bool dispose)
		{
			Page page = this.Page;
			if (page != null && page.RequiresControlState(this))
			{
				page.UnregisterRequiresControlState(this);
				this.RareFieldsEnsured.RequiredControlState = true;
			}
			if (this.flags[2097152])
			{
				this._id = null;
				this.flags.Clear(2097152);
			}
			if (this._controls != null)
			{
				string collectionReadOnly = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
				int count = this._controls.Count;
				for (int i = 0; i < count; i++)
				{
					this._controls[i].UnloadRecursive(dispose);
				}
				this._controls.SetCollectionReadOnly(collectionReadOnly);
			}
			if (this.AdapterInternal != null)
			{
				this.AdapterInternal.OnUnload(EventArgs.Empty);
			}
			else
			{
				this.OnUnload(EventArgs.Empty);
			}
			if (dispose)
			{
				this.Dispose();
			}
			if (this.IsReloadable)
			{
				this._controlState = ControlState.Constructed;
			}
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00059CBC File Offset: 0x00057EBC
		protected void RaiseBubbleEvent(object source, EventArgs args)
		{
			for (Control parent = this.Parent; parent != null; parent = parent.Parent)
			{
				if (parent.OnBubbleEvent(source, args))
				{
					return;
				}
			}
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool OnBubbleEvent(object source, EventArgs args)
		{
			return false;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00059CE7 File Offset: 0x00057EE7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_Controls")]
		public virtual ControlCollection Controls
		{
			get
			{
				if (this._controls == null)
				{
					this._controls = this.CreateControlCollection();
				}
				return this._controls;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00059D03 File Offset: 0x00057F03
		// (set) Token: 0x06001C5D RID: 7261 RVA: 0x00059D17 File Offset: 0x00057F17
		[WebCategory("Behavior")]
		[WebSysDescription("Control_ValidateRequestMode")]
		[DefaultValue(ValidateRequestMode.Inherit)]
		public virtual ValidateRequestMode ValidateRequestMode
		{
			get
			{
				return (ValidateRequestMode)this.flags[1610612736, 29];
			}
			set
			{
				this.SetValidateRequestModeInternal(value, true);
			}
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00059D24 File Offset: 0x00057F24
		internal void SetValidateRequestModeInternal(ValidateRequestMode value, bool setDirty)
		{
			if (value < ValidateRequestMode.Inherit || value > ValidateRequestMode.Enabled)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			int num = this.flags[1610612736, 29];
			if (setDirty && num != (int)value)
			{
				this.flags.Set(4194304);
			}
			this.flags[1610612736, 29] = (int)value;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x00059D84 File Offset: 0x00057F84
		internal bool CalculateEffectiveValidateRequest()
		{
			RuntimeConfig config = RuntimeConfig.GetConfig();
			HttpRuntimeSection httpRuntime = config.HttpRuntime;
			if (httpRuntime.RequestValidationMode >= VersionUtil.Framework45)
			{
				for (Control control = this; control != null; control = control.Parent)
				{
					ValidateRequestMode validateRequestMode = control.ValidateRequestMode;
					if (validateRequestMode != ValidateRequestMode.Inherit)
					{
						return validateRequestMode == ValidateRequestMode.Enabled;
					}
				}
			}
			return true;
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00059DCE File Offset: 0x00057FCE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_State")]
		protected virtual StateBag ViewState
		{
			get
			{
				if (this._viewState != null)
				{
					return this._viewState;
				}
				this._viewState = new StateBag(this.ViewStateIgnoresCase);
				if (this.IsTrackingViewState)
				{
					this._viewState.TrackViewState();
				}
				return this._viewState;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00007722 File Offset: 0x00005922
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual bool ViewStateIgnoresCase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00059E0C File Offset: 0x0005800C
		protected internal virtual void AddedControl(Control control, int index)
		{
			if (control.OwnerControl != null)
			{
				throw new InvalidOperationException(SR.GetString("Substitution_NotAllowed"));
			}
			if (control._parent != null)
			{
				control._parent.Controls.Remove(control);
			}
			control._parent = this;
			control._page = this.Page;
			control.flags.Clear(131072);
			Control control2 = this.flags[128] ? this : this._namingContainer;
			if (control2 != null)
			{
				control.UpdateNamingContainer(control2);
				if (control._id == null && !control.flags[64])
				{
					control.GenerateAutomaticID();
				}
				else if (control._id != null || control._controls != null)
				{
					control2.DirtyNameTable();
				}
			}
			if (this._controlState >= ControlState.ChildrenInitialized)
			{
				control.InitRecursive(control2);
				if (control._controlState >= ControlState.Initialized && control.RareFields != null && control.RareFields.RequiredControlState)
				{
					this.Page.RegisterRequiresControlState(control);
				}
				if (this._controlState >= ControlState.ViewStateLoaded)
				{
					object savedState = null;
					if (this._occasionalFields != null && this._occasionalFields.ControlsViewState != null)
					{
						savedState = this._occasionalFields.ControlsViewState[index];
						if (this.LoadViewStateByID)
						{
							control.EnsureID();
							savedState = this._occasionalFields.ControlsViewState[control.ID];
							this._occasionalFields.ControlsViewState.Remove(control.ID);
						}
						else
						{
							savedState = this._occasionalFields.ControlsViewState[index];
							this._occasionalFields.ControlsViewState.Remove(index);
						}
					}
					control.LoadViewStateRecursive(savedState);
					if (this._controlState >= ControlState.Loaded)
					{
						control.LoadRecursive();
						if (this._controlState >= ControlState.PreRendered)
						{
							control.PreRenderRecursiveInternal();
						}
					}
				}
			}
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00059FD4 File Offset: 0x000581D4
		protected virtual ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void CreateChildControls()
		{
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00059FDC File Offset: 0x000581DC
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00059FEA File Offset: 0x000581EA
		protected bool ChildControlsCreated
		{
			get
			{
				return this.flags[8];
			}
			set
			{
				if (!value && this.flags[8])
				{
					this.Controls.Clear();
				}
				if (value)
				{
					this.flags.Set(8);
					return;
				}
				this.flags.Clear(8);
			}
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x0005A024 File Offset: 0x00058224
		public string ResolveUrl(string relativeUrl)
		{
			if (relativeUrl == null)
			{
				throw new ArgumentNullException("relativeUrl");
			}
			if (relativeUrl.Length == 0 || !UrlPath.IsRelativeUrl(relativeUrl))
			{
				return relativeUrl;
			}
			string appRelativeTemplateSourceDirectory = this.AppRelativeTemplateSourceDirectory;
			if (string.IsNullOrEmpty(appRelativeTemplateSourceDirectory))
			{
				return relativeUrl;
			}
			string virtualPath = UrlPath.Combine(appRelativeTemplateSourceDirectory, relativeUrl);
			return this.Context.Response.ApplyAppPathModifier(virtualPath);
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x0005A07C File Offset: 0x0005827C
		public string ResolveClientUrl(string relativeUrl)
		{
			if (this.DesignMode && this.Page != null && this.Page.Site != null)
			{
				IUrlResolutionService urlResolutionService = (IUrlResolutionService)this.Page.Site.GetService(typeof(IUrlResolutionService));
				if (urlResolutionService != null)
				{
					return urlResolutionService.ResolveClientUrl(relativeUrl);
				}
			}
			if (relativeUrl == null)
			{
				throw new ArgumentNullException("relativeUrl");
			}
			string virtualPathString = VirtualPath.GetVirtualPathString(this.TemplateControlVirtualDirectory);
			if (string.IsNullOrEmpty(virtualPathString))
			{
				return relativeUrl;
			}
			string text = this.Context.Request.ClientBaseDir.VirtualPathString;
			if (!UrlPath.IsAppRelativePath(relativeUrl))
			{
				if (StringUtil.EqualsIgnoreCase(text, virtualPathString))
				{
					return relativeUrl;
				}
				if (relativeUrl.Length == 0 || !UrlPath.IsRelativeUrl(relativeUrl))
				{
					return relativeUrl;
				}
			}
			string to = UrlPath.Combine(virtualPathString, relativeUrl);
			text = UrlPath.AppendSlashToPathIfNeeded(text);
			return HttpUtility.UrlPathEncode(UrlPath.MakeRelative(text, to));
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0005A14D File Offset: 0x0005834D
		internal void DirtyNameTable()
		{
			if (this._occasionalFields != null)
			{
				this._occasionalFields.NamedControls = null;
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0005A163 File Offset: 0x00058363
		private void EnsureNamedControlsTable()
		{
			this._occasionalFields.NamedControls = new HybridDictionary(this._occasionalFields.NamedControlsID, true);
			this.FillNamedControlsTable(this, this._controls);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0005A190 File Offset: 0x00058390
		private void FillNamedControlsTable(Control namingContainer, ControlCollection controls)
		{
			int count = controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control control = controls[i];
				if (control._id != null)
				{
					try
					{
						namingContainer.EnsureOccasionalFields();
						namingContainer._occasionalFields.NamedControls.Add(control._id, control);
					}
					catch
					{
						throw new HttpException(SR.GetString("Duplicate_id_used", new object[]
						{
							control._id,
							"FindControl"
						}));
					}
				}
				if (control.HasControls() && !control.flags[128])
				{
					this.FillNamedControlsTable(namingContainer, control.Controls);
				}
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0005A244 File Offset: 0x00058444
		public virtual Control FindControl(string id)
		{
			return this.FindControl(id, 0);
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x0005A250 File Offset: 0x00058450
		protected virtual Control FindControl(string id, int pathOffset)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			this.EnsureChildControls();
			if (!this.flags[128])
			{
				Control namingContainer = this.NamingContainer;
				if (namingContainer != null)
				{
					return namingContainer.FindControl(id, pathOffset);
				}
				return null;
			}
			else
			{
				if (this.HasControls())
				{
					this.EnsureOccasionalFields();
					if (this._occasionalFields.NamedControls == null)
					{
						this.EnsureNamedControlsTable();
					}
				}
				if (this._occasionalFields == null || this._occasionalFields.NamedControls == null)
				{
					return null;
				}
				char[] anyOf = new char[]
				{
					'$',
					':'
				};
				int num = id.IndexOfAny(anyOf, pathOffset);
				string key;
				if (num == -1)
				{
					key = id.Substring(pathOffset);
					return this._occasionalFields.NamedControls[key] as Control;
				}
				key = id.Substring(pathOffset, num - pathOffset);
				Control control = this._occasionalFields.NamedControls[key] as Control;
				if (control == null)
				{
					return null;
				}
				return control.FindControl(id, num + 1);
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x0005A338 File Offset: 0x00058538
		internal Control FindControlFromPageIfNecessary(string id)
		{
			Control control = this.FindControl(id);
			if (control == null && this.Page != null)
			{
				char[] anyOf = new char[]
				{
					'$',
					':'
				};
				if (id.IndexOfAny(anyOf) != -1)
				{
					control = this.Page.FindControl(id);
				}
			}
			return control;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x0005A381 File Offset: 0x00058581
		internal void ClearNamingContainer()
		{
			this.EnsureOccasionalFields();
			this._occasionalFields.NamedControlsID = 0;
			this.DirtyNameTable();
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0005A39C File Offset: 0x0005859C
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected virtual IDictionary GetDesignModeState()
		{
			Control.ControlRareFields rareFieldsEnsured = this.RareFieldsEnsured;
			if (rareFieldsEnsured.DesignModeState == null)
			{
				rareFieldsEnsured.DesignModeState = new HybridDictionary();
			}
			return rareFieldsEnsured.DesignModeState;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x0005A3C9 File Offset: 0x000585C9
		public virtual bool HasControls()
		{
			return this._controls != null && this._controls.Count > 0;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0005A3E3 File Offset: 0x000585E3
		internal bool HasRenderingData()
		{
			return this.HasControls() || this.HasRenderDelegate();
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x0005A3F5 File Offset: 0x000585F5
		internal bool HasRenderDelegate()
		{
			return this.RareFields != null && this.RareFields.RenderMethod != null;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x0005A40F File Offset: 0x0005860F
		protected bool IsLiteralContent()
		{
			return this._controls != null && this._controls.Count == 1 && this._controls[0] is LiteralControl;
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x0005A43D File Offset: 0x0005863D
		protected bool IsTrackingViewState
		{
			get
			{
				return this.flags[2];
			}
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0005A44B File Offset: 0x0005864B
		protected virtual void TrackViewState()
		{
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
			this.flags.Set(2);
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0005A46C File Offset: 0x0005866C
		protected virtual void EnsureChildControls()
		{
			if (!this.ChildControlsCreated && !this.flags[256])
			{
				this.flags.Set(256);
				try
				{
					this.ResolveAdapter();
					if (this.AdapterInternal != null)
					{
						this.AdapterInternal.CreateChildControls();
					}
					else
					{
						this.CreateChildControls();
					}
					this.ChildControlsCreated = true;
				}
				finally
				{
					this.flags.Clear(256);
				}
			}
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0005A4F0 File Offset: 0x000586F0
		internal void SetControlBuilder(ControlBuilder controlBuilder)
		{
			this.RareFieldsEnsured.ControlBuilder = controlBuilder;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x0005A500 File Offset: 0x00058700
		protected internal virtual void RemovedControl(Control control)
		{
			if (control.OwnerControl != null)
			{
				throw new InvalidOperationException(SR.GetString("Substitution_NotAllowed"));
			}
			if (this._namingContainer != null && control._id != null)
			{
				this._namingContainer.DirtyNameTable();
			}
			control.UnloadRecursive(false);
			control._parent = null;
			control._page = null;
			control._namingContainer = null;
			if (!(control is TemplateControl) && control._occasionalFields != null)
			{
				control._occasionalFields.TemplateSourceVirtualDirectory = null;
			}
			if (control._occasionalFields != null)
			{
				control._occasionalFields.TemplateControl = null;
			}
			control.flags.Clear(2048);
			control.ClearCachedUniqueIDRecursive();
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x0005A5A2 File Offset: 0x000587A2
		internal void SetDesignMode()
		{
			this.flags.Set(65536);
			this.flags.Set(131072);
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void SetDesignModeState(IDictionary data)
		{
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x0005A5C4 File Offset: 0x000587C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void SetRenderMethodDelegate(RenderMethod renderMethod)
		{
			this.RareFieldsEnsured.RenderMethod = renderMethod;
			this.Controls.SetCollectionReadOnly("Collection_readonly_Codeblocks");
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0005A5E3 File Offset: 0x000587E3
		bool IDataBindingsAccessor.HasDataBindings
		{
			get
			{
				return this.RareFields != null && this.RareFields.DataBindings != null && this.RareFields.DataBindings.Count != 0;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0005A610 File Offset: 0x00058810
		DataBindingCollection IDataBindingsAccessor.DataBindings
		{
			get
			{
				Control.ControlRareFields rareFieldsEnsured = this.RareFieldsEnsured;
				if (rareFieldsEnsured.DataBindings == null)
				{
					rareFieldsEnsured.DataBindings = new DataBindingCollection();
				}
				return rareFieldsEnsured.DataBindings;
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x0005A63D File Offset: 0x0005883D
		void IParserAccessor.AddParsedSubObject(object obj)
		{
			this.AddParsedSubObject(obj);
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0005A648 File Offset: 0x00058848
		internal string SpacerImageUrl
		{
			get
			{
				this.EnsureOccasionalFields();
				if (this._occasionalFields.SpacerImageUrl == null)
				{
					this._occasionalFields.SpacerImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(WebControl), "Spacer.gif");
				}
				return this._occasionalFields.SpacerImageUrl;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0005A69D File Offset: 0x0005889D
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x0005A6B4 File Offset: 0x000588B4
		private Control OwnerControl
		{
			get
			{
				if (this.RareFields == null)
				{
					return null;
				}
				return this.RareFields.OwnerControl;
			}
			set
			{
				this.RareFieldsEnsured.OwnerControl = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0005A6C4 File Offset: 0x000588C4
		internal IPostBackDataHandler PostBackDataHandler
		{
			get
			{
				IPostBackDataHandler postBackDataHandler = this.AdapterInternal as IPostBackDataHandler;
				if (postBackDataHandler != null)
				{
					return postBackDataHandler;
				}
				return this as IPostBackDataHandler;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x0005A6EC File Offset: 0x000588EC
		internal IPostBackEventHandler PostBackEventHandler
		{
			get
			{
				IPostBackEventHandler postBackEventHandler = this.AdapterInternal as IPostBackEventHandler;
				if (postBackEventHandler != null)
				{
					return postBackEventHandler;
				}
				return this as IPostBackEventHandler;
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x0005A712 File Offset: 0x00058912
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void BeginRenderTracing(TextWriter writer, object traceObject)
		{
			RenderTraceListener.CurrentListeners.BeginRendering(writer, traceObject);
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x0005A720 File Offset: 0x00058920
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void EndRenderTracing(TextWriter writer, object traceObject)
		{
			RenderTraceListener.CurrentListeners.EndRendering(writer, traceObject);
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x0005A72E File Offset: 0x0005892E
		private void TraceNonRenderingControlInternal(TextWriter writer)
		{
			this.BeginRenderTracing(writer, this);
			this.EndRenderTracing(writer, this);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x0005A740 File Offset: 0x00058940
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetTraceData(object traceDataKey, object traceDataValue)
		{
			this.SetTraceData(this, traceDataKey, traceDataValue);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x0005A74B File Offset: 0x0005894B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
		{
			RenderTraceListener.CurrentListeners.SetTraceData(tracedObject, traceDataKey, traceDataValue);
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0005A75C File Offset: 0x0005895C
		IDictionary IControlDesignerAccessor.UserData
		{
			get
			{
				Control.ControlRareFields rareFieldsEnsured = this.RareFieldsEnsured;
				if (rareFieldsEnsured.ControlDesignerAccessorUserData == null)
				{
					rareFieldsEnsured.ControlDesignerAccessorUserData = new HybridDictionary();
				}
				return rareFieldsEnsured.ControlDesignerAccessorUserData;
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0005A789 File Offset: 0x00058989
		IDictionary IControlDesignerAccessor.GetDesignModeState()
		{
			return this.GetDesignModeState();
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0005A791 File Offset: 0x00058991
		void IControlDesignerAccessor.SetDesignModeState(IDictionary data)
		{
			this.SetDesignModeState(data);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0005A79A File Offset: 0x0005899A
		void IControlDesignerAccessor.SetOwnerControl(Control owner)
		{
			if (owner == this)
			{
				throw new ArgumentException(SR.GetString("Control_CannotOwnSelf"), "owner");
			}
			this.OwnerControl = owner;
			this._parent = owner.Parent;
			this._page = owner.Page;
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0005A7D4 File Offset: 0x000589D4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		ControlBuilder IControlBuilderAccessor.ControlBuilder
		{
			get
			{
				if (this.RareFields == null)
				{
					return null;
				}
				return this.RareFields.ControlBuilder;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0005A7EC File Offset: 0x000589EC
		bool IExpressionsAccessor.HasExpressions
		{
			get
			{
				if (this.RareFields == null)
				{
					return false;
				}
				ExpressionBindingCollection expressionBindings = this.RareFields.ExpressionBindings;
				return expressionBindings != null && expressionBindings.Count > 0;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0005A820 File Offset: 0x00058A20
		ExpressionBindingCollection IExpressionsAccessor.Expressions
		{
			get
			{
				ExpressionBindingCollection expressionBindingCollection = this.RareFieldsEnsured.ExpressionBindings;
				if (expressionBindingCollection == null)
				{
					expressionBindingCollection = new ExpressionBindingCollection();
					this.RareFields.ExpressionBindings = expressionBindingCollection;
				}
				return expressionBindingCollection;
			}
		}

		// Token: 0x040018D6 RID: 6358
		internal static readonly object EventDataBinding = new object();

		// Token: 0x040018D7 RID: 6359
		internal static readonly object EventInit = new object();

		// Token: 0x040018D8 RID: 6360
		internal static readonly object EventLoad = new object();

		// Token: 0x040018D9 RID: 6361
		internal static readonly object EventUnload = new object();

		// Token: 0x040018DA RID: 6362
		internal static readonly object EventPreRender = new object();

		// Token: 0x040018DB RID: 6363
		private static readonly object EventDisposed = new object();

		// Token: 0x040018DC RID: 6364
		internal const bool EnableViewStateDefault = true;

		// Token: 0x040018DD RID: 6365
		internal const char ID_SEPARATOR = '$';

		// Token: 0x040018DE RID: 6366
		private const char ID_RENDER_SEPARATOR = '_';

		// Token: 0x040018DF RID: 6367
		internal const char LEGACY_ID_SEPARATOR = ':';

		// Token: 0x040018E0 RID: 6368
		private string _id;

		// Token: 0x040018E1 RID: 6369
		private string _cachedUniqueID;

		// Token: 0x040018E2 RID: 6370
		private string _cachedPredictableID;

		// Token: 0x040018E3 RID: 6371
		private Control _parent;

		// Token: 0x040018E4 RID: 6372
		private ControlState _controlState;

		// Token: 0x040018E5 RID: 6373
		private StateBag _viewState;

		// Token: 0x040018E6 RID: 6374
		private EventHandlerList _events;

		// Token: 0x040018E7 RID: 6375
		private ControlCollection _controls;

		// Token: 0x040018E8 RID: 6376
		private Control _namingContainer;

		// Token: 0x040018E9 RID: 6377
		internal Page _page;

		// Token: 0x040018EA RID: 6378
		private Control.OccasionalFields _occasionalFields;

		// Token: 0x040018EB RID: 6379
		private const int idNotCalculated = 1;

		// Token: 0x040018EC RID: 6380
		private const int marked = 2;

		// Token: 0x040018ED RID: 6381
		private const int disableViewState = 4;

		// Token: 0x040018EE RID: 6382
		private const int controlsCreated = 8;

		// Token: 0x040018EF RID: 6383
		private const int invisible = 16;

		// Token: 0x040018F0 RID: 6384
		private const int visibleDirty = 32;

		// Token: 0x040018F1 RID: 6385
		private const int idNotRequired = 64;

		// Token: 0x040018F2 RID: 6386
		private const int isNamingContainer = 128;

		// Token: 0x040018F3 RID: 6387
		private const int creatingControls = 256;

		// Token: 0x040018F4 RID: 6388
		private const int notVisibleOnPage = 512;

		// Token: 0x040018F5 RID: 6389
		private const int themeApplied = 1024;

		// Token: 0x040018F6 RID: 6390
		private const int mustRenderID = 2048;

		// Token: 0x040018F7 RID: 6391
		private const int disableTheming = 4096;

		// Token: 0x040018F8 RID: 6392
		private const int enableThemingSet = 8192;

		// Token: 0x040018F9 RID: 6393
		private const int styleSheetApplied = 16384;

		// Token: 0x040018FA RID: 6394
		private const int controlAdapterResolved = 32768;

		// Token: 0x040018FB RID: 6395
		private const int designMode = 65536;

		// Token: 0x040018FC RID: 6396
		private const int designModeChecked = 131072;

		// Token: 0x040018FD RID: 6397
		private const int disableChildControlState = 262144;

		// Token: 0x040018FE RID: 6398
		internal const int isWebControlDisabled = 524288;

		// Token: 0x040018FF RID: 6399
		private const int controlStateApplied = 1048576;

		// Token: 0x04001900 RID: 6400
		private const int useGeneratedID = 2097152;

		// Token: 0x04001901 RID: 6401
		private const int validateRequestModeDirty = 4194304;

		// Token: 0x04001902 RID: 6402
		private const int viewStateNotInherited = 8388608;

		// Token: 0x04001903 RID: 6403
		private const int viewStateMode = 16777216;

		// Token: 0x04001904 RID: 6404
		private const int clientIDMode = 100663296;

		// Token: 0x04001905 RID: 6405
		private const int clientIDModeOffset = 25;

		// Token: 0x04001906 RID: 6406
		private const int effectiveClientIDMode = 402653184;

		// Token: 0x04001907 RID: 6407
		private const int effectiveClientIDModeOffset = 27;

		// Token: 0x04001908 RID: 6408
		private const int validateRequestMode = 1610612736;

		// Token: 0x04001909 RID: 6409
		private const int validateRequestModeOffset = 29;

		// Token: 0x0400190A RID: 6410
		internal SimpleBitVector32 flags;

		// Token: 0x0400190B RID: 6411
		private const string automaticIDPrefix = "ctl";

		// Token: 0x0400190C RID: 6412
		private const string automaticLegacyIDPrefix = "_ctl";

		// Token: 0x0400190D RID: 6413
		private const int automaticIDCount = 128;

		// Token: 0x0400190E RID: 6414
		private static readonly string[] automaticIDs = new string[]
		{
			"ctl00",
			"ctl01",
			"ctl02",
			"ctl03",
			"ctl04",
			"ctl05",
			"ctl06",
			"ctl07",
			"ctl08",
			"ctl09",
			"ctl10",
			"ctl11",
			"ctl12",
			"ctl13",
			"ctl14",
			"ctl15",
			"ctl16",
			"ctl17",
			"ctl18",
			"ctl19",
			"ctl20",
			"ctl21",
			"ctl22",
			"ctl23",
			"ctl24",
			"ctl25",
			"ctl26",
			"ctl27",
			"ctl28",
			"ctl29",
			"ctl30",
			"ctl31",
			"ctl32",
			"ctl33",
			"ctl34",
			"ctl35",
			"ctl36",
			"ctl37",
			"ctl38",
			"ctl39",
			"ctl40",
			"ctl41",
			"ctl42",
			"ctl43",
			"ctl44",
			"ctl45",
			"ctl46",
			"ctl47",
			"ctl48",
			"ctl49",
			"ctl50",
			"ctl51",
			"ctl52",
			"ctl53",
			"ctl54",
			"ctl55",
			"ctl56",
			"ctl57",
			"ctl58",
			"ctl59",
			"ctl60",
			"ctl61",
			"ctl62",
			"ctl63",
			"ctl64",
			"ctl65",
			"ctl66",
			"ctl67",
			"ctl68",
			"ctl69",
			"ctl70",
			"ctl71",
			"ctl72",
			"ctl73",
			"ctl74",
			"ctl75",
			"ctl76",
			"ctl77",
			"ctl78",
			"ctl79",
			"ctl80",
			"ctl81",
			"ctl82",
			"ctl83",
			"ctl84",
			"ctl85",
			"ctl86",
			"ctl87",
			"ctl88",
			"ctl89",
			"ctl90",
			"ctl91",
			"ctl92",
			"ctl93",
			"ctl94",
			"ctl95",
			"ctl96",
			"ctl97",
			"ctl98",
			"ctl99",
			"ctl100",
			"ctl101",
			"ctl102",
			"ctl103",
			"ctl104",
			"ctl105",
			"ctl106",
			"ctl107",
			"ctl108",
			"ctl109",
			"ctl110",
			"ctl111",
			"ctl112",
			"ctl113",
			"ctl114",
			"ctl115",
			"ctl116",
			"ctl117",
			"ctl118",
			"ctl119",
			"ctl120",
			"ctl121",
			"ctl122",
			"ctl123",
			"ctl124",
			"ctl125",
			"ctl126",
			"ctl127"
		};

		// Token: 0x0200095A RID: 2394
		private sealed class ControlRareFields : IDisposable
		{
			// Token: 0x060069C7 RID: 27079 RVA: 0x000030B5 File Offset: 0x000012B5
			internal ControlRareFields()
			{
			}

			// Token: 0x060069C8 RID: 27080 RVA: 0x001780A7 File Offset: 0x001762A7
			public void Dispose()
			{
				this.ControlBuilder = null;
				if (this.OwnerControl != null)
				{
					this.OwnerControl.Dispose();
				}
				this.ControlDesignerAccessorUserData = null;
				this.DesignModeState = null;
				this.RenderingCompatibility = null;
				this.RouteCollection = null;
			}

			// Token: 0x040037F4 RID: 14324
			public ISite Site;

			// Token: 0x040037F5 RID: 14325
			public RenderMethod RenderMethod;

			// Token: 0x040037F6 RID: 14326
			public ControlBuilder ControlBuilder;

			// Token: 0x040037F7 RID: 14327
			public DataBindingCollection DataBindings;

			// Token: 0x040037F8 RID: 14328
			public Control OwnerControl;

			// Token: 0x040037F9 RID: 14329
			public ExpressionBindingCollection ExpressionBindings;

			// Token: 0x040037FA RID: 14330
			public bool RequiredControlState;

			// Token: 0x040037FB RID: 14331
			public IDictionary ControlDesignerAccessorUserData;

			// Token: 0x040037FC RID: 14332
			public IDictionary DesignModeState;

			// Token: 0x040037FD RID: 14333
			public Version RenderingCompatibility;

			// Token: 0x040037FE RID: 14334
			public RouteCollection RouteCollection;

			// Token: 0x040037FF RID: 14335
			public ControlAdapter Adapter;
		}

		// Token: 0x0200095B RID: 2395
		private sealed class OccasionalFields : IDisposable
		{
			// Token: 0x060069C9 RID: 27081 RVA: 0x000030B5 File Offset: 0x000012B5
			internal OccasionalFields()
			{
			}

			// Token: 0x060069CA RID: 27082 RVA: 0x001780DF File Offset: 0x001762DF
			public void Dispose()
			{
				if (this.RareFields != null)
				{
					this.RareFields.Dispose();
				}
				this.ControlsViewState = null;
			}

			// Token: 0x04003800 RID: 14336
			public string SkinId;

			// Token: 0x04003801 RID: 14337
			public IDictionary ControlsViewState;

			// Token: 0x04003802 RID: 14338
			public int NamedControlsID;

			// Token: 0x04003803 RID: 14339
			public IDictionary NamedControls;

			// Token: 0x04003804 RID: 14340
			public Control.ControlRareFields RareFields;

			// Token: 0x04003805 RID: 14341
			public string UniqueIDPrefix;

			// Token: 0x04003806 RID: 14342
			public string SpacerImageUrl;

			// Token: 0x04003807 RID: 14343
			public TemplateControl TemplateControl;

			// Token: 0x04003808 RID: 14344
			public VirtualPath TemplateSourceVirtualDirectory;
		}
	}
}
