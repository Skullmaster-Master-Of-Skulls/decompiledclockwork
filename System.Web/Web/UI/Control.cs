using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020003BC RID: 956
	[ToolboxItem("System.Web.UI.Design.WebControlToolboxItem, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.ControlCodeDomSerializer, Microsoft.VisualStudio.Web, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	[ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Require)]
	[Bindable(true)]
	[DefaultProperty("ID")]
	[DesignerCategory("Code")]
	[Designer("System.Web.UI.Design.ControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Control : IComponent, IDisposable, IParserAccessor, IUrlResolutionService, IDataBindingsAccessor, IControlBuilderAccessor, IControlDesignerAccessor, IExpressionsAccessor
	{
		// Token: 0x06002E50 RID: 11856 RVA: 0x000CF5E8 File Offset: 0x000CE5E8
		public Control()
		{
			if (this is INamingContainer)
			{
				this.flags.Set(128);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000CF608 File Offset: 0x000CE608
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_ClientID")]
		[Browsable(false)]
		public virtual string ClientID
		{
			get
			{
				this.EnsureID();
				string uniqueID = this.UniqueID;
				if (uniqueID != null && uniqueID.IndexOf(this.IdSeparator) >= 0)
				{
					return uniqueID.Replace(this.IdSeparator, '_');
				}
				return uniqueID;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x000CF644 File Offset: 0x000CE644
		protected char ClientIDSeparator
		{
			get
			{
				return '_';
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002E53 RID: 11859 RVA: 0x000CF648 File Offset: 0x000CE648
		// (remove) Token: 0x06002E54 RID: 11860 RVA: 0x000CF65B File Offset: 0x000CE65B
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

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000CF670 File Offset: 0x000CE670
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

		// Token: 0x06002E56 RID: 11862 RVA: 0x000CF694 File Offset: 0x000CE694
		protected virtual ControlAdapter ResolveAdapter()
		{
			if (this.flags[32768])
			{
				return this._adapter;
			}
			if (this.DesignMode)
			{
				this.flags.Set(32768);
				return null;
			}
			HttpContext context = this.Context;
			if (context != null)
			{
				this._adapter = context.Request.Browser.GetAdapter(this);
			}
			this.flags.Set(32768);
			return this._adapter;
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000CF70B File Offset: 0x000CE70B
		protected ControlAdapter Adapter
		{
			get
			{
				if (this.flags[32768])
				{
					return this._adapter;
				}
				this._adapter = this.ResolveAdapter();
				this.flags.Set(32768);
				return this._adapter;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000CF748 File Offset: 0x000CE748
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

		// Token: 0x06002E59 RID: 11865 RVA: 0x000CF81E File Offset: 0x000CE81E
		internal void ValidateEvent(string uniqueID)
		{
			this.ValidateEvent(uniqueID, string.Empty);
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000CF82C File Offset: 0x000CE82C
		internal void ValidateEvent(string uniqueID, string eventArgument)
		{
			if (this.Page != null && this.SupportsEventValidation)
			{
				this.Page.ClientScript.ValidateEvent(uniqueID, eventArgument);
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000CF850 File Offset: 0x000CE850
		private bool SupportsEventValidation
		{
			get
			{
				return SupportsEventValidationAttribute.SupportsEventValidation(base.GetType());
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000CF85D File Offset: 0x000CE85D
		protected EventHandlerList Events
		{
			get
			{
				this.EnsureOccasionalFields();
				if (this._occasionalFields.Events == null)
				{
					this._occasionalFields.Events = new EventHandlerList();
				}
				return this._occasionalFields.Events;
			}
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000CF88D File Offset: 0x000CE88D
		protected bool HasEvents()
		{
			return this._occasionalFields != null && this._occasionalFields.Events != null;
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000CF8AA File Offset: 0x000CE8AA
		// (set) Token: 0x06002E5F RID: 11871 RVA: 0x000CF8D4 File Offset: 0x000CE8D4
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
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000CF938 File Offset: 0x000CE938
		// (set) Token: 0x06002E61 RID: 11873 RVA: 0x000CF994 File Offset: 0x000CE994
		[DefaultValue(true)]
		[Browsable(false)]
		[WebSysDescription("Control_EnableTheming")]
		[Themeable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x06002E62 RID: 11874 RVA: 0x000CFA07 File Offset: 0x000CEA07
		internal bool ShouldSerializeEnableTheming()
		{
			return this.flags[8192];
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000CFA19 File Offset: 0x000CEA19
		internal bool IsBindingContainer
		{
			get
			{
				return this is INamingContainer && !(this is INonBindingContainer);
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002E64 RID: 11876 RVA: 0x000CFA31 File Offset: 0x000CEA31
		protected internal bool IsChildControlStateCleared
		{
			get
			{
				return this.flags[262144];
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000CFA43 File Offset: 0x000CEA43
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000CFA74 File Offset: 0x000CEA74
		[Browsable(false)]
		[WebSysDescription("Control_SkinId")]
		[DefaultValue("")]
		[Filterable(false)]
		[WebCategory("Behavior")]
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

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000CFAF8 File Offset: 0x000CEAF8
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

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x000CFB2D File Offset: 0x000CEB2D
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

		// Token: 0x06002E69 RID: 11881 RVA: 0x000CFB44 File Offset: 0x000CEB44
		private void EnsureOccasionalFields()
		{
			if (this._occasionalFields == null)
			{
				this._occasionalFields = new Control.OccasionalFields();
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002E6A RID: 11882 RVA: 0x000CFB59 File Offset: 0x000CEB59
		// (set) Token: 0x06002E6B RID: 11883 RVA: 0x000CFB6A File Offset: 0x000CEB6A
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[Themeable(false)]
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

		// Token: 0x06002E6C RID: 11884 RVA: 0x000CFB73 File Offset: 0x000CEB73
		internal void SetEnableViewStateInternal(bool value)
		{
			if (!value)
			{
				this.flags.Set(4);
				return;
			}
			this.flags.Clear(4);
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x000CFB94 File Offset: 0x000CEB94
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
				}
				return true;
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x000CFBBC File Offset: 0x000CEBBC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_NamingContainer")]
		[Bindable(false)]
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

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000CFC18 File Offset: 0x000CEC18
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
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

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x000CFC3E File Offset: 0x000CEC3E
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

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000CFC5A File Offset: 0x000CEC5A
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000CFC69 File Offset: 0x000CEC69
		protected bool LoadViewStateByID
		{
			get
			{
				return ViewStateModeByIdAttribute.IsEnabled(base.GetType());
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000CFC76 File Offset: 0x000CEC76
		// (set) Token: 0x06002E74 RID: 11892 RVA: 0x000CFC9F File Offset: 0x000CEC9F
		[WebSysDescription("Control_Page")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[Browsable(false)]
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

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000CFCB6 File Offset: 0x000CECB6
		internal virtual bool IsReloadable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000CFCBC File Offset: 0x000CECBC
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

		// Token: 0x06002E77 RID: 11895 RVA: 0x000CFD00 File Offset: 0x000CED00
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

		// Token: 0x06002E78 RID: 11896 RVA: 0x000CFD31 File Offset: 0x000CED31
		internal virtual TemplateControl GetTemplateControl()
		{
			if (this._templateControl == null && this.Parent != null)
			{
				this._templateControl = this.Parent.GetTemplateControl();
			}
			return this._templateControl;
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x000CFD5A File Offset: 0x000CED5A
		// (set) Token: 0x06002E7A RID: 11898 RVA: 0x000CFD62 File Offset: 0x000CED62
		[Browsable(false)]
		[Bindable(false)]
		[WebSysDescription("Control_TemplateControl")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TemplateControl TemplateControl
		{
			get
			{
				return this.GetTemplateControl();
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			set
			{
				this._templateControl = value;
			}
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000CFD6C File Offset: 0x000CED6C
		internal bool IsDescendentOf(Control ancestor)
		{
			Control control = this;
			while (control != ancestor && control.Parent != null)
			{
				control = control.Parent;
			}
			return control == ancestor;
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000CFD94 File Offset: 0x000CED94
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebSysDescription("Control_Parent")]
		public virtual Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000CFD9C File Offset: 0x000CED9C
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

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000CFDC7 File Offset: 0x000CEDC7
		[Browsable(false)]
		[WebSysDescription("Control_TemplateSourceDirectory")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000CFDE8 File Offset: 0x000CEDE8
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000CFDF5 File Offset: 0x000CEDF5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[WebSysDescription("Control_TemplateSourceDirectory")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000CFE04 File Offset: 0x000CEE04
		// (set) Token: 0x06002E82 RID: 11906 RVA: 0x000CFE6D File Offset: 0x000CEE6D
		internal VirtualPath TemplateControlVirtualDirectory
		{
			get
			{
				if (this._templateSourceVirtualDirectory != null)
				{
					return this._templateSourceVirtualDirectory;
				}
				TemplateControl templateControl = this.TemplateControl;
				if (templateControl == null)
				{
					HttpContext context = this.Context;
					if (context != null)
					{
						this._templateSourceVirtualDirectory = context.Request.CurrentExecutionFilePathObject.Parent;
					}
					return this._templateSourceVirtualDirectory;
				}
				if (templateControl != this)
				{
					this._templateSourceVirtualDirectory = templateControl.TemplateControlVirtualDirectory;
				}
				return this._templateSourceVirtualDirectory;
			}
			set
			{
				this._templateSourceVirtualDirectory = value;
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000CFE76 File Offset: 0x000CEE76
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x000CFE7E File Offset: 0x000CEE7E
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

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000CFE87 File Offset: 0x000CEE87
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x000CFEB2 File Offset: 0x000CEEB2
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[WebSysDescription("Control_Site")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000CFEE8 File Offset: 0x000CEEE8
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x000CFF18 File Offset: 0x000CEF18
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

		// Token: 0x06002E89 RID: 11913 RVA: 0x000CFF73 File Offset: 0x000CEF73
		private void ResetVisible()
		{
			this.Visible = true;
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000CFF7C File Offset: 0x000CEF7C
		private bool ShouldSerializeVisible()
		{
			return this.flags[16];
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000CFF8C File Offset: 0x000CEF8C
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

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06002E8C RID: 11916 RVA: 0x000D000C File Offset: 0x000CF00C
		// (remove) Token: 0x06002E8D RID: 11917 RVA: 0x000D001F File Offset: 0x000CF01F
		[WebSysDescription("Control_OnDataBind")]
		[WebCategory("Data")]
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

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06002E8E RID: 11918 RVA: 0x000D0032 File Offset: 0x000CF032
		// (remove) Token: 0x06002E8F RID: 11919 RVA: 0x000D0045 File Offset: 0x000CF045
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

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06002E90 RID: 11920 RVA: 0x000D0058 File Offset: 0x000CF058
		// (remove) Token: 0x06002E91 RID: 11921 RVA: 0x000D006B File Offset: 0x000CF06B
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

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06002E92 RID: 11922 RVA: 0x000D007E File Offset: 0x000CF07E
		// (remove) Token: 0x06002E93 RID: 11923 RVA: 0x000D0091 File Offset: 0x000CF091
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

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002E94 RID: 11924 RVA: 0x000D00A4 File Offset: 0x000CF0A4
		// (remove) Token: 0x06002E95 RID: 11925 RVA: 0x000D00B7 File Offset: 0x000CF0B7
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

		// Token: 0x06002E96 RID: 11926 RVA: 0x000D00CC File Offset: 0x000CF0CC
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

		// Token: 0x06002E97 RID: 11927 RVA: 0x000D0118 File Offset: 0x000CF118
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

		// Token: 0x06002E98 RID: 11928 RVA: 0x000D016C File Offset: 0x000CF16C
		protected virtual void OnDataBinding(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._occasionalFields.Events[Control.EventDataBinding] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000D01A7 File Offset: 0x000CF1A7
		public virtual void DataBind()
		{
			this.DataBind(true);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000D01B0 File Offset: 0x000CF1B0
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

		// Token: 0x06002E9B RID: 11931 RVA: 0x000D0220 File Offset: 0x000CF220
		protected virtual void DataBindChildren()
		{
			if (this.HasControls())
			{
				this.EnsureOccasionalFields();
				string collectionReadOnly = this._occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
				try
				{
					try
					{
						int count = this._occasionalFields.Controls.Count;
						for (int i = 0; i < count; i++)
						{
							this._occasionalFields.Controls[i].DataBind();
						}
					}
					finally
					{
						this._occasionalFields.Controls.SetCollectionReadOnly(collectionReadOnly);
					}
				}
				catch
				{
					throw;
				}
			}
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000D02BC File Offset: 0x000CF2BC
		internal void PreventAutoID()
		{
			if (!this.flags[128])
			{
				this.flags.Set(64);
			}
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000D02E0 File Offset: 0x000CF2E0
		protected virtual void AddParsedSubObject(object obj)
		{
			Control control = obj as Control;
			if (control != null)
			{
				this.Controls.Add(control);
			}
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000D0303 File Offset: 0x000CF303
		private void UpdateNamingContainer(Control namingContainer)
		{
			if (this._namingContainer != null && this._namingContainer != namingContainer)
			{
				this.ClearCachedUniqueIDRecursive();
			}
			this._namingContainer = namingContainer;
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000D0324 File Offset: 0x000CF324
		private void ClearCachedUniqueIDRecursive()
		{
			this._cachedUniqueID = null;
			if (this._occasionalFields != null)
			{
				this._occasionalFields.UniqueIDPrefix = null;
				if (this._occasionalFields.Controls != null)
				{
					int count = this._occasionalFields.Controls.Count;
					for (int i = 0; i < count; i++)
					{
						this._occasionalFields.Controls[i].ClearCachedUniqueIDRecursive();
					}
				}
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000D038C File Offset: 0x000CF38C
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

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000D03B4 File Offset: 0x000CF3B4
		private void GenerateAutomaticID()
		{
			this.flags.Set(2097152);
			this._namingContainer.EnsureOccasionalFields();
			int num = this._namingContainer._occasionalFields.NamedControlsID++;
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

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000D045C File Offset: 0x000CF45C
		internal virtual string GetUniqueIDPrefix()
		{
			this.EnsureOccasionalFields();
			if (this._occasionalFields.UniqueIDPrefix == null)
			{
				string uniqueID = this.UniqueID;
				if (!string.IsNullOrEmpty(uniqueID))
				{
					this._occasionalFields.UniqueIDPrefix = uniqueID + this.IdSeparator;
				}
				else
				{
					this._occasionalFields.UniqueIDPrefix = string.Empty;
				}
			}
			return this._occasionalFields.UniqueIDPrefix;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000D04C4 File Offset: 0x000CF4C4
		protected internal virtual void OnInit(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._occasionalFields.Events[Control.EventInit] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000D0500 File Offset: 0x000CF500
		internal virtual void InitRecursive(Control namingContainer)
		{
			this.ResolveAdapter();
			if (this._occasionalFields != null && this._occasionalFields.Controls != null)
			{
				if (this.flags[128])
				{
					namingContainer = this;
				}
				string collectionReadOnly = this._occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
				int count = this._occasionalFields.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					Control control = this._occasionalFields.Controls[i];
					control.UpdateNamingContainer(namingContainer);
					if (control._id == null && namingContainer != null && !control.flags[64])
					{
						control.GenerateAutomaticID();
					}
					control._page = this.Page;
					control.InitRecursive(namingContainer);
				}
				this._occasionalFields.Controls.SetCollectionReadOnly(collectionReadOnly);
			}
			if (this._controlState < ControlState.Initialized)
			{
				this._controlState = ControlState.ChildrenInitialized;
				if (this.Page != null && !this.DesignMode && this.Page.ContainsTheme && this.EnableTheming)
				{
					this.ApplySkin(this.Page);
				}
				if (this._adapter != null)
				{
					this._adapter.OnInit(EventArgs.Empty);
				}
				else
				{
					this.OnInit(EventArgs.Empty);
				}
				this._controlState = ControlState.Initialized;
			}
			this.TrackViewState();
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000D0648 File Offset: 0x000CF648
		protected void ClearChildState()
		{
			this.ClearChildControlState();
			this.ClearChildViewState();
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000D0656 File Offset: 0x000CF656
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

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000D0686 File Offset: 0x000CF686
		protected void ClearChildViewState()
		{
			if (this._occasionalFields != null)
			{
				this._occasionalFields.ControlsViewState = null;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x000D069C File Offset: 0x000CF69C
		protected bool HasChildViewState
		{
			get
			{
				return this._occasionalFields != null && this._occasionalFields.ControlsViewState != null && this._occasionalFields.ControlsViewState.Count > 0;
			}
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000D06C8 File Offset: 0x000CF6C8
		public virtual void Focus()
		{
			this.Page.SetFocus(this);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000D06D8 File Offset: 0x000CF6D8
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
			if (this._adapter == null || pair.Second == null)
			{
				return;
			}
			this._adapter.LoadAdapterControlState(pair.Second);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000D075D File Offset: 0x000CF75D
		protected internal virtual void LoadControlState(object savedState)
		{
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000D0760 File Offset: 0x000CF760
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
			}
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000D07C4 File Offset: 0x000CF7C4
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
					if (obj != null && this._adapter != null)
					{
						this._adapter.LoadAdapterViewState(obj);
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

		// Token: 0x06002EAE RID: 11950 RVA: 0x000D08CC File Offset: 0x000CF8CC
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

		// Token: 0x06002EAF RID: 11951 RVA: 0x000D0950 File Offset: 0x000CF950
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

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000D09E9 File Offset: 0x000CF9E9
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

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000D0A10 File Offset: 0x000CFA10
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

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000D0A60 File Offset: 0x000CFA60
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

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000D0AC0 File Offset: 0x000CFAC0
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

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000D0B14 File Offset: 0x000CFB14
		protected internal virtual void OnLoad(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._occasionalFields.Events[Control.EventLoad] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000D0B50 File Offset: 0x000CFB50
		internal virtual void LoadRecursive()
		{
			if (this._controlState < ControlState.Loaded)
			{
				if (this._adapter != null)
				{
					this._adapter.OnLoad(EventArgs.Empty);
				}
				else
				{
					this.OnLoad(EventArgs.Empty);
				}
			}
			if (this._occasionalFields != null && this._occasionalFields.Controls != null)
			{
				string collectionReadOnly = this._occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
				int count = this._occasionalFields.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					this._occasionalFields.Controls[i].LoadRecursive();
				}
				this._occasionalFields.Controls.SetCollectionReadOnly(collectionReadOnly);
			}
			if (this._controlState < ControlState.Loaded)
			{
				this._controlState = ControlState.Loaded;
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000D0C0C File Offset: 0x000CFC0C
		protected internal virtual void OnPreRender(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._occasionalFields.Events[Control.EventPreRender] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000D0C48 File Offset: 0x000CFC48
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
				if (this._adapter != null)
				{
					this._adapter.OnPreRender(EventArgs.Empty);
				}
				else
				{
					this.OnPreRender(EventArgs.Empty);
				}
				if (this._occasionalFields != null && this._occasionalFields.Controls != null)
				{
					string collectionReadOnly = this._occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
					int count = this._occasionalFields.Controls.Count;
					for (int i = 0; i < count; i++)
					{
						this._occasionalFields.Controls[i].PreRenderRecursiveInternal();
					}
					this._occasionalFields.Controls.SetCollectionReadOnly(collectionReadOnly);
				}
			}
			this._controlState = ControlState.PreRendered;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000D0D20 File Offset: 0x000CFD20
		internal int EstimateStateSize(object state)
		{
			if (state == null)
			{
				return 0;
			}
			return Util.SerializeWithAssert(new ObjectStateFormatter(), state).Length;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000D0D38 File Offset: 0x000CFD38
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
			if (this._occasionalFields != null && this._occasionalFields.Controls != null)
			{
				int count = this._occasionalFields.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					this._occasionalFields.Controls[i].BuildProfileTree(this.UniqueID, calcViewState);
				}
			}
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000D0E1C File Offset: 0x000CFE1C
		internal object SaveControlStateInternal()
		{
			object obj = this.SaveControlState();
			object obj2 = null;
			if (this._adapter != null)
			{
				obj2 = this._adapter.SaveAdapterControlState();
			}
			if (obj != null || obj2 != null)
			{
				return new Pair(obj, obj2);
			}
			return null;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000D0E55 File Offset: 0x000CFE55
		protected internal virtual object SaveControlState()
		{
			return null;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000D0E58 File Offset: 0x000CFE58
		protected virtual object SaveViewState()
		{
			if (this.flags[32])
			{
				this.ViewState["Visible"] = !this.flags[16];
			}
			if (this._viewState != null)
			{
				return this._viewState.SaveViewState();
			}
			return null;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000D0EB0 File Offset: 0x000CFEB0
		internal object SaveViewStateRecursive()
		{
			if (this.flags[4])
			{
				return null;
			}
			object obj = null;
			if (this._adapter != null)
			{
				obj = this._adapter.SaveAdapterViewState();
			}
			object obj2 = this.SaveViewState();
			ArrayList arrayList = null;
			if (this.HasControls())
			{
				ControlCollection controls = this._occasionalFields.Controls;
				int count = controls.Count;
				bool loadViewStateByID = this.LoadViewStateByID;
				for (int i = 0; i < count; i++)
				{
					Control control = controls[i];
					object obj3 = control.SaveViewStateRecursive();
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
			if (this._adapter != null)
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

		// Token: 0x06002EBE RID: 11966 RVA: 0x000D0FA1 File Offset: 0x000CFFA1
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000D0FAC File Offset: 0x000CFFAC
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

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000D1038 File Offset: 0x000D0038
		protected internal virtual void RenderChildren(HtmlTextWriter writer)
		{
			ICollection children = (this._occasionalFields == null) ? null : this._occasionalFields.Controls;
			this.RenderChildrenInternal(writer, children);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000D1064 File Offset: 0x000D0064
		public virtual void RenderControl(HtmlTextWriter writer)
		{
			this.RenderControl(writer, this.Adapter);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000D1074 File Offset: 0x000D0074
		protected void RenderControl(HtmlTextWriter writer, ControlAdapter adapter)
		{
			if (!this.flags[16] && !this.flags[512])
			{
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
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000D1101 File Offset: 0x000D0101
		private void RenderControlInternal(HtmlTextWriter writer, ControlAdapter adapter)
		{
			if (adapter != null)
			{
				adapter.BeginRender(writer);
				adapter.Render(writer);
				adapter.EndRender(writer);
				return;
			}
			this.Render(writer);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x000D1124 File Offset: 0x000D0124
		protected internal virtual void OnUnload(EventArgs e)
		{
			if (this.HasEvents())
			{
				EventHandler eventHandler = this._occasionalFields.Events[Control.EventUnload] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000D1160 File Offset: 0x000D0160
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
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000D11D4 File Offset: 0x000D01D4
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
			if (this._occasionalFields != null && this._occasionalFields.Controls != null)
			{
				string collectionReadOnly = this._occasionalFields.Controls.SetCollectionReadOnly("Parent_collections_readonly");
				int count = this._occasionalFields.Controls.Count;
				for (int i = 0; i < count; i++)
				{
					this._occasionalFields.Controls[i].UnloadRecursive(dispose);
				}
				this._occasionalFields.Controls.SetCollectionReadOnly(collectionReadOnly);
			}
			if (this._adapter != null)
			{
				this._adapter.OnUnload(EventArgs.Empty);
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

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000D12E0 File Offset: 0x000D02E0
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

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000D130B File Offset: 0x000D030B
		protected virtual bool OnBubbleEvent(object source, EventArgs args)
		{
			return false;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x000D130E File Offset: 0x000D030E
		[WebSysDescription("Control_Controls")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ControlCollection Controls
		{
			get
			{
				if (this._occasionalFields == null || this._occasionalFields.Controls == null)
				{
					this.EnsureOccasionalFields();
					this._occasionalFields.Controls = this.CreateControlCollection();
				}
				return this._occasionalFields.Controls;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x000D1347 File Offset: 0x000D0347
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Control_State")]
		[Browsable(false)]
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

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000D1382 File Offset: 0x000D0382
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected virtual bool ViewStateIgnoresCase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x000D1388 File Offset: 0x000D0388
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
				else if (control._id != null || (control._occasionalFields != null && control._occasionalFields.Controls != null))
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

		// Token: 0x06002ECD RID: 11981 RVA: 0x000D155D File Offset: 0x000D055D
		protected virtual ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000D1565 File Offset: 0x000D0565
		protected internal virtual void CreateChildControls()
		{
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000D1567 File Offset: 0x000D0567
		// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x000D1575 File Offset: 0x000D0575
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

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000D15B0 File Offset: 0x000D05B0
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

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000D1608 File Offset: 0x000D0608
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

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000D16D7 File Offset: 0x000D06D7
		internal void DirtyNameTable()
		{
			if (this._occasionalFields != null)
			{
				this._occasionalFields.NamedControls = null;
			}
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000D16ED File Offset: 0x000D06ED
		private void EnsureNamedControlsTable()
		{
			this._occasionalFields.NamedControls = new HybridDictionary(this._occasionalFields.NamedControlsID, true);
			this.FillNamedControlsTable(this, this._occasionalFields.Controls);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000D1720 File Offset: 0x000D0720
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

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000D17D8 File Offset: 0x000D07D8
		public virtual Control FindControl(string id)
		{
			return this.FindControl(id, 0);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000D17E4 File Offset: 0x000D07E4
		protected virtual Control FindControl(string id, int pathOffset)
		{
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
				if (this.HasControls() && this._occasionalFields.NamedControls == null)
				{
					this.EnsureNamedControlsTable();
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

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000D18C7 File Offset: 0x000D08C7
		internal void ClearNamingContainer()
		{
			this.EnsureOccasionalFields();
			this._occasionalFields.NamedControlsID = 0;
			this.DirtyNameTable();
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000D18E4 File Offset: 0x000D08E4
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

		// Token: 0x06002EDA RID: 11994 RVA: 0x000D1911 File Offset: 0x000D0911
		public virtual bool HasControls()
		{
			return this._occasionalFields != null && this._occasionalFields.Controls != null && this._occasionalFields.Controls.Count > 0;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000D193D File Offset: 0x000D093D
		internal bool HasRenderingData()
		{
			return this.HasControls() || this.HasRenderDelegate();
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000D194F File Offset: 0x000D094F
		internal bool HasRenderDelegate()
		{
			return this.RareFields != null && this.RareFields.RenderMethod != null;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000D196C File Offset: 0x000D096C
		protected bool IsLiteralContent()
		{
			return this._occasionalFields != null && this._occasionalFields.Controls != null && this._occasionalFields.Controls.Count == 1 && this._occasionalFields.Controls[0] is LiteralControl;
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000D19BC File Offset: 0x000D09BC
		protected bool IsTrackingViewState
		{
			get
			{
				return this.flags[2];
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000D19CA File Offset: 0x000D09CA
		protected virtual void TrackViewState()
		{
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
			this.flags.Set(2);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000D19EC File Offset: 0x000D09EC
		protected virtual void EnsureChildControls()
		{
			if (!this.ChildControlsCreated && !this.flags[256])
			{
				this.flags.Set(256);
				try
				{
					this.ResolveAdapter();
					if (this._adapter != null)
					{
						this._adapter.CreateChildControls();
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

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000D1A70 File Offset: 0x000D0A70
		internal void SetControlBuilder(ControlBuilder controlBuilder)
		{
			this.RareFieldsEnsured.ControlBuilder = controlBuilder;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000D1A80 File Offset: 0x000D0A80
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
			if (!(control is TemplateControl))
			{
				control._templateSourceVirtualDirectory = null;
			}
			control._templateControl = null;
			control.flags.Clear(2048);
			control.ClearCachedUniqueIDRecursive();
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000D1B08 File Offset: 0x000D0B08
		internal void SetDesignMode()
		{
			this.flags.Set(65536);
			this.flags.Set(131072);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000D1B2A File Offset: 0x000D0B2A
		protected virtual void SetDesignModeState(IDictionary data)
		{
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000D1B2C File Offset: 0x000D0B2C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void SetRenderMethodDelegate(RenderMethod renderMethod)
		{
			this.RareFieldsEnsured.RenderMethod = renderMethod;
			this.Controls.SetCollectionReadOnly("Collection_readonly_Codeblocks");
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000D1B4B File Offset: 0x000D0B4B
		bool IDataBindingsAccessor.HasDataBindings
		{
			get
			{
				return this.RareFields != null && this.RareFields.DataBindings != null && this.RareFields.DataBindings.Count != 0;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06002EE7 RID: 12007 RVA: 0x000D1B7C File Offset: 0x000D0B7C
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

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000D1BA9 File Offset: 0x000D0BA9
		void IParserAccessor.AddParsedSubObject(object obj)
		{
			this.AddParsedSubObject(obj);
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000D1BB4 File Offset: 0x000D0BB4
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

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000D1C09 File Offset: 0x000D0C09
		// (set) Token: 0x06002EEB RID: 12011 RVA: 0x000D1C20 File Offset: 0x000D0C20
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

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000D1C30 File Offset: 0x000D0C30
		internal IPostBackDataHandler PostBackDataHandler
		{
			get
			{
				IPostBackDataHandler postBackDataHandler = this._adapter as IPostBackDataHandler;
				if (postBackDataHandler != null)
				{
					return postBackDataHandler;
				}
				return this as IPostBackDataHandler;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000D1C58 File Offset: 0x000D0C58
		internal IPostBackEventHandler PostBackEventHandler
		{
			get
			{
				IPostBackEventHandler postBackEventHandler = this._adapter as IPostBackEventHandler;
				if (postBackEventHandler != null)
				{
					return postBackEventHandler;
				}
				return this as IPostBackEventHandler;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000D1C80 File Offset: 0x000D0C80
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

		// Token: 0x06002EEF RID: 12015 RVA: 0x000D1CAD File Offset: 0x000D0CAD
		IDictionary IControlDesignerAccessor.GetDesignModeState()
		{
			return this.GetDesignModeState();
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000D1CB5 File Offset: 0x000D0CB5
		void IControlDesignerAccessor.SetDesignModeState(IDictionary data)
		{
			this.SetDesignModeState(data);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000D1CBE File Offset: 0x000D0CBE
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

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000D1CF8 File Offset: 0x000D0CF8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000D1D10 File Offset: 0x000D0D10
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000D1D44 File Offset: 0x000D0D44
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

		// Token: 0x0400217C RID: 8572
		internal const bool EnableViewStateDefault = true;

		// Token: 0x0400217D RID: 8573
		internal const char ID_SEPARATOR = '$';

		// Token: 0x0400217E RID: 8574
		private const char ID_RENDER_SEPARATOR = '_';

		// Token: 0x0400217F RID: 8575
		internal const char LEGACY_ID_SEPARATOR = ':';

		// Token: 0x04002180 RID: 8576
		private const int idNotCalculated = 1;

		// Token: 0x04002181 RID: 8577
		private const int marked = 2;

		// Token: 0x04002182 RID: 8578
		private const int disableViewState = 4;

		// Token: 0x04002183 RID: 8579
		private const int controlsCreated = 8;

		// Token: 0x04002184 RID: 8580
		private const int invisible = 16;

		// Token: 0x04002185 RID: 8581
		private const int visibleDirty = 32;

		// Token: 0x04002186 RID: 8582
		private const int idNotRequired = 64;

		// Token: 0x04002187 RID: 8583
		private const int isNamingContainer = 128;

		// Token: 0x04002188 RID: 8584
		private const int creatingControls = 256;

		// Token: 0x04002189 RID: 8585
		private const int notVisibleOnPage = 512;

		// Token: 0x0400218A RID: 8586
		private const int themeApplied = 1024;

		// Token: 0x0400218B RID: 8587
		private const int mustRenderID = 2048;

		// Token: 0x0400218C RID: 8588
		private const int disableTheming = 4096;

		// Token: 0x0400218D RID: 8589
		private const int enableThemingSet = 8192;

		// Token: 0x0400218E RID: 8590
		private const int styleSheetApplied = 16384;

		// Token: 0x0400218F RID: 8591
		private const int controlAdapterResolved = 32768;

		// Token: 0x04002190 RID: 8592
		private const int designMode = 65536;

		// Token: 0x04002191 RID: 8593
		private const int designModeChecked = 131072;

		// Token: 0x04002192 RID: 8594
		private const int disableChildControlState = 262144;

		// Token: 0x04002193 RID: 8595
		internal const int isWebControlDisabled = 524288;

		// Token: 0x04002194 RID: 8596
		private const int controlStateApplied = 1048576;

		// Token: 0x04002195 RID: 8597
		private const int useGeneratedID = 2097152;

		// Token: 0x04002196 RID: 8598
		private const string automaticIDPrefix = "ctl";

		// Token: 0x04002197 RID: 8599
		private const string automaticLegacyIDPrefix = "_ctl";

		// Token: 0x04002198 RID: 8600
		private const int automaticIDCount = 128;

		// Token: 0x04002199 RID: 8601
		internal static readonly object EventDataBinding = new object();

		// Token: 0x0400219A RID: 8602
		internal static readonly object EventInit = new object();

		// Token: 0x0400219B RID: 8603
		internal static readonly object EventLoad = new object();

		// Token: 0x0400219C RID: 8604
		internal static readonly object EventUnload = new object();

		// Token: 0x0400219D RID: 8605
		internal static readonly object EventPreRender = new object();

		// Token: 0x0400219E RID: 8606
		private static readonly object EventDisposed = new object();

		// Token: 0x0400219F RID: 8607
		private string _id;

		// Token: 0x040021A0 RID: 8608
		private string _cachedUniqueID;

		// Token: 0x040021A1 RID: 8609
		private Control _parent;

		// Token: 0x040021A2 RID: 8610
		private ControlState _controlState;

		// Token: 0x040021A3 RID: 8611
		private StateBag _viewState;

		// Token: 0x040021A4 RID: 8612
		private Control _namingContainer;

		// Token: 0x040021A5 RID: 8613
		internal Page _page;

		// Token: 0x040021A6 RID: 8614
		private Control.OccasionalFields _occasionalFields;

		// Token: 0x040021A7 RID: 8615
		private TemplateControl _templateControl;

		// Token: 0x040021A8 RID: 8616
		private VirtualPath _templateSourceVirtualDirectory;

		// Token: 0x040021A9 RID: 8617
		internal ControlAdapter _adapter;

		// Token: 0x040021AA RID: 8618
		internal SimpleBitVector32 flags;

		// Token: 0x040021AB RID: 8619
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

		// Token: 0x020003BD RID: 957
		private sealed class ControlRareFields : IDisposable
		{
			// Token: 0x06002EF6 RID: 12022 RVA: 0x000D2245 File Offset: 0x000D1245
			internal ControlRareFields()
			{
			}

			// Token: 0x06002EF7 RID: 12023 RVA: 0x000D224D File Offset: 0x000D124D
			public void Dispose()
			{
				this.ControlBuilder = null;
				if (this.OwnerControl != null)
				{
					this.OwnerControl.Dispose();
				}
				this.ControlDesignerAccessorUserData = null;
				this.DesignModeState = null;
			}

			// Token: 0x040021AC RID: 8620
			public ISite Site;

			// Token: 0x040021AD RID: 8621
			public RenderMethod RenderMethod;

			// Token: 0x040021AE RID: 8622
			public ControlBuilder ControlBuilder;

			// Token: 0x040021AF RID: 8623
			public DataBindingCollection DataBindings;

			// Token: 0x040021B0 RID: 8624
			public Control OwnerControl;

			// Token: 0x040021B1 RID: 8625
			public ExpressionBindingCollection ExpressionBindings;

			// Token: 0x040021B2 RID: 8626
			public bool RequiredControlState;

			// Token: 0x040021B3 RID: 8627
			public IDictionary ControlDesignerAccessorUserData;

			// Token: 0x040021B4 RID: 8628
			public IDictionary DesignModeState;
		}

		// Token: 0x020003BE RID: 958
		private sealed class OccasionalFields : IDisposable
		{
			// Token: 0x06002EF8 RID: 12024 RVA: 0x000D2277 File Offset: 0x000D1277
			internal OccasionalFields()
			{
			}

			// Token: 0x06002EF9 RID: 12025 RVA: 0x000D227F File Offset: 0x000D127F
			public void Dispose()
			{
				if (this.Events != null)
				{
					this.Events.Dispose();
					this.Events = null;
				}
				if (this.RareFields != null)
				{
					this.RareFields.Dispose();
				}
				this.ControlsViewState = null;
			}

			// Token: 0x040021B5 RID: 8629
			public string SkinId;

			// Token: 0x040021B6 RID: 8630
			public EventHandlerList Events;

			// Token: 0x040021B7 RID: 8631
			public IDictionary ControlsViewState;

			// Token: 0x040021B8 RID: 8632
			public ControlCollection Controls;

			// Token: 0x040021B9 RID: 8633
			public int NamedControlsID;

			// Token: 0x040021BA RID: 8634
			public IDictionary NamedControls;

			// Token: 0x040021BB RID: 8635
			public Control.ControlRareFields RareFields;

			// Token: 0x040021BC RID: 8636
			public string UniqueIDPrefix;

			// Token: 0x040021BD RID: 8637
			public string SpacerImageUrl;
		}
	}
}
