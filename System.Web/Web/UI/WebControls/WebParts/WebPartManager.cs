using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Xml;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000730 RID: 1840
	[NonVisualControl]
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartManagerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PersistChildren(false)]
	[ParseChildren(true)]
	[ViewStateModeById]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebPartManager : Control, INamingContainer, IPersonalizable
	{
		// Token: 0x060058CB RID: 22731 RVA: 0x0016442D File Offset: 0x0016342D
		public WebPartManager()
		{
			this._allowEventCancellation = true;
			this._displayMode = WebPartManager.BrowseDisplayMode;
			this._webPartZones = new WebPartZoneCollection();
			this._partAndChildControlIDs = new HybridDictionary(true);
			this._zoneIDs = new HybridDictionary(true);
		}

		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x060058CC RID: 22732 RVA: 0x0016446A File Offset: 0x0016346A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TransformerTypeCollection AvailableTransformers
		{
			get
			{
				if (this._availableTransformers == null)
				{
					this._availableTransformers = this.CreateAvailableTransformers();
				}
				return this._availableTransformers;
			}
		}

		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x060058CD RID: 22733 RVA: 0x00164488 File Offset: 0x00163488
		// (set) Token: 0x060058CE RID: 22734 RVA: 0x001644BA File Offset: 0x001634BA
		[WebCategory("Behavior")]
		[WebSysDefaultValue("WebPartManager_DefaultCloseProviderWarning")]
		[WebSysDescription("WebPartManager_CloseProviderWarning")]
		public virtual string CloseProviderWarning
		{
			get
			{
				object obj = this.ViewState["CloseProviderWarning"];
				if (obj == null)
				{
					return SR.GetString("WebPartManager_DefaultCloseProviderWarning");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["CloseProviderWarning"] = value;
			}
		}

		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x060058CF RID: 22735 RVA: 0x001644D0 File Offset: 0x001634D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartConnectionCollection Connections
		{
			get
			{
				WebPartConnectionCollection webPartConnectionCollection = new WebPartConnectionCollection(this);
				if (this._staticConnections != null)
				{
					foreach (object obj in this._staticConnections)
					{
						WebPartConnection webPartConnection = (WebPartConnection)obj;
						if (!this.Internals.ConnectionDeleted(webPartConnection))
						{
							webPartConnectionCollection.Add(webPartConnection);
						}
					}
				}
				if (this._dynamicConnections != null)
				{
					foreach (object obj2 in this._dynamicConnections)
					{
						WebPartConnection webPartConnection2 = (WebPartConnection)obj2;
						if (!this.Internals.ConnectionDeleted(webPartConnection2))
						{
							webPartConnectionCollection.Add(webPartConnection2);
						}
					}
				}
				webPartConnectionCollection.SetReadOnly("WebPartManager_ConnectionsReadOnly");
				return webPartConnectionCollection;
			}
		}

		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x060058D0 RID: 22736 RVA: 0x001645BC File Offset: 0x001635BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x060058D1 RID: 22737 RVA: 0x001645C4 File Offset: 0x001635C4
		// (set) Token: 0x060058D2 RID: 22738 RVA: 0x001645F6 File Offset: 0x001635F6
		[WebSysDefaultValue("WebPartManager_DefaultDeleteWarning")]
		[WebCategory("Behavior")]
		[WebSysDescription("WebPartManager_DeleteWarning")]
		public virtual string DeleteWarning
		{
			get
			{
				object obj = this.ViewState["DeleteWarning"];
				if (obj == null)
				{
					return SR.GetString("WebPartManager_DefaultDeleteWarning");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DeleteWarning"] = value;
			}
		}

		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x060058D3 RID: 22739 RVA: 0x00164609 File Offset: 0x00163609
		// (set) Token: 0x060058D4 RID: 22740 RVA: 0x00164614 File Offset: 0x00163614
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual WebPartDisplayMode DisplayMode
		{
			get
			{
				return this._displayMode;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.DisplayMode == value)
				{
					return;
				}
				if (!this.SupportedDisplayModes.Contains(value))
				{
					throw new ArgumentException(SR.GetString("WebPartManager_InvalidDisplayMode"), "value");
				}
				if (!value.IsEnabled(this))
				{
					throw new ArgumentException(SR.GetString("WebPartManager_DisabledDisplayMode"), "value");
				}
				WebPartDisplayModeCancelEventArgs webPartDisplayModeCancelEventArgs = new WebPartDisplayModeCancelEventArgs(value);
				this.OnDisplayModeChanging(webPartDisplayModeCancelEventArgs);
				if (this._allowEventCancellation && webPartDisplayModeCancelEventArgs.Cancel)
				{
					return;
				}
				if (this.DisplayMode == WebPartManager.ConnectDisplayMode && this.SelectedWebPart != null)
				{
					this.EndWebPartConnecting();
					if (this.SelectedWebPart != null)
					{
						return;
					}
				}
				if (this.DisplayMode == WebPartManager.EditDisplayMode && this.SelectedWebPart != null)
				{
					this.EndWebPartEditing();
					if (this.SelectedWebPart != null)
					{
						return;
					}
				}
				WebPartDisplayModeEventArgs e = new WebPartDisplayModeEventArgs(this.DisplayMode);
				this._displayMode = value;
				this.OnDisplayModeChanged(e);
			}
		}

		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x060058D5 RID: 22741 RVA: 0x001646FB File Offset: 0x001636FB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public WebPartDisplayModeCollection DisplayModes
		{
			get
			{
				if (this._displayModes == null)
				{
					this._displayModes = this.CreateDisplayModes();
					this._displayModes.SetReadOnly("WebPartManager_DisplayModesReadOnly");
				}
				return this._displayModes;
			}
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x060058D6 RID: 22742 RVA: 0x00164727 File Offset: 0x00163727
		protected internal WebPartConnectionCollection DynamicConnections
		{
			get
			{
				if (this._dynamicConnections == null)
				{
					this._dynamicConnections = new WebPartConnectionCollection(this);
				}
				return this._dynamicConnections;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x060058D7 RID: 22743 RVA: 0x00164744 File Offset: 0x00163744
		// (set) Token: 0x060058D8 RID: 22744 RVA: 0x0016476D File Offset: 0x0016376D
		[WebSysDescription("WebPartManager_EnableClientScript")]
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public virtual bool EnableClientScript
		{
			get
			{
				object obj = this.ViewState["EnableClientScript"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableClientScript"] = value;
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x060058D9 RID: 22745 RVA: 0x00164785 File Offset: 0x00163785
		// (set) Token: 0x060058DA RID: 22746 RVA: 0x00164788 File Offset: 0x00163788
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue(true)]
		public override bool EnableTheming
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("WebPartManager_CantSetEnableTheming"));
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x060058DB RID: 22747 RVA: 0x0016479C File Offset: 0x0016379C
		// (set) Token: 0x060058DC RID: 22748 RVA: 0x001647CE File Offset: 0x001637CE
		[WebSysDefaultValue("WebPartChrome_ConfirmExportSensitive")]
		[WebSysDescription("WebPartManager_ExportSensitiveDataWarning")]
		[WebCategory("Behavior")]
		public virtual string ExportSensitiveDataWarning
		{
			get
			{
				object obj = this.ViewState["ExportSensitiveDataWarning"];
				if (obj == null)
				{
					return SR.GetString("WebPartChrome_ConfirmExportSensitive");
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ExportSensitiveDataWarning"] = value;
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x060058DD RID: 22749 RVA: 0x001647E1 File Offset: 0x001637E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected WebPartManagerInternals Internals
		{
			get
			{
				if (this._internals == null)
				{
					this._internals = new WebPartManagerInternals(this);
				}
				return this._internals;
			}
		}

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x060058DE RID: 22750 RVA: 0x001647FD File Offset: 0x001637FD
		protected virtual bool IsCustomPersonalizationStateDirty
		{
			get
			{
				return this._hasDataChanged;
			}
		}

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x060058DF RID: 22751 RVA: 0x00164808 File Offset: 0x00163808
		private PermissionSet MediumPermissionSet
		{
			get
			{
				if (this._mediumPermissionSet == null)
				{
					this._mediumPermissionSet = new PermissionSet(PermissionState.None);
					this._mediumPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
					this._mediumPermissionSet.AddPermission(new AspNetHostingPermission(AspNetHostingPermissionLevel.Medium));
				}
				return this._mediumPermissionSet;
			}
		}

		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x060058E0 RID: 22752 RVA: 0x00164858 File Offset: 0x00163858
		private PermissionSet MinimalPermissionSet
		{
			get
			{
				if (this._minimalPermissionSet == null)
				{
					this._minimalPermissionSet = new PermissionSet(PermissionState.None);
					this._minimalPermissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
					this._minimalPermissionSet.AddPermission(new AspNetHostingPermission(AspNetHostingPermissionLevel.Minimal));
				}
				return this._minimalPermissionSet;
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x060058E1 RID: 22753 RVA: 0x001648A7 File Offset: 0x001638A7
		[WebCategory("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebPartManager_Personalization")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public WebPartPersonalization Personalization
		{
			get
			{
				if (this._personalization == null)
				{
					this._personalization = this.CreatePersonalization();
				}
				return this._personalization;
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x060058E2 RID: 22754 RVA: 0x001648C3 File Offset: 0x001638C3
		internal bool RenderClientScript
		{
			get
			{
				return this._renderClientScript;
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x060058E3 RID: 22755 RVA: 0x001648CB File Offset: 0x001638CB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public WebPart SelectedWebPart
		{
			get
			{
				return this._selectedWebPart;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x060058E4 RID: 22756 RVA: 0x001648D3 File Offset: 0x001638D3
		// (set) Token: 0x060058E5 RID: 22757 RVA: 0x001648DC File Offset: 0x001638DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DefaultValue("")]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x060058E6 RID: 22758 RVA: 0x0016490E File Offset: 0x0016390E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebPartManager_StaticConnections")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WebPartConnectionCollection StaticConnections
		{
			get
			{
				if (this._staticConnections == null)
				{
					this._staticConnections = new WebPartConnectionCollection(this);
				}
				return this._staticConnections;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x0016492C File Offset: 0x0016392C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public WebPartDisplayModeCollection SupportedDisplayModes
		{
			get
			{
				if (this._supportedDisplayModes == null)
				{
					this._supportedDisplayModes = new WebPartDisplayModeCollection();
					foreach (object obj in this.DisplayModes)
					{
						WebPartDisplayMode webPartDisplayMode = (WebPartDisplayMode)obj;
						if (!webPartDisplayMode.AssociatedWithToolZone)
						{
							this._supportedDisplayModes.Add(webPartDisplayMode);
						}
					}
					this._supportedDisplayModes.SetReadOnly("WebPartManager_DisplayModesReadOnly");
				}
				return this._supportedDisplayModes;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x001649BC File Offset: 0x001639BC
		// (set) Token: 0x060058E9 RID: 22761 RVA: 0x001649C0 File Offset: 0x001639C0
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Visible
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("ControlNonVisual", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x001649F2 File Offset: 0x001639F2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public WebPartCollection WebParts
		{
			get
			{
				if (this.HasControls())
				{
					return new WebPartCollection(this.Controls);
				}
				return new WebPartCollection();
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x060058EB RID: 22763 RVA: 0x00164A0D File Offset: 0x00163A0D
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartZoneCollection Zones
		{
			get
			{
				return this._webPartZones;
			}
		}

		// Token: 0x1400010E RID: 270
		// (add) Token: 0x060058EC RID: 22764 RVA: 0x00164A15 File Offset: 0x00163A15
		// (remove) Token: 0x060058ED RID: 22765 RVA: 0x00164A28 File Offset: 0x00163A28
		[WebSysDescription("WebPartManager_AuthorizeWebPart")]
		[WebCategory("Action")]
		public event WebPartAuthorizationEventHandler AuthorizeWebPart
		{
			add
			{
				base.Events.AddHandler(WebPartManager.AuthorizeWebPartEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.AuthorizeWebPartEvent, value);
			}
		}

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x060058EE RID: 22766 RVA: 0x00164A3B File Offset: 0x00163A3B
		// (remove) Token: 0x060058EF RID: 22767 RVA: 0x00164A4E File Offset: 0x00163A4E
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_ConnectionsActivated")]
		public event EventHandler ConnectionsActivated
		{
			add
			{
				base.Events.AddHandler(WebPartManager.ConnectionsActivatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.ConnectionsActivatedEvent, value);
			}
		}

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x060058F0 RID: 22768 RVA: 0x00164A61 File Offset: 0x00163A61
		// (remove) Token: 0x060058F1 RID: 22769 RVA: 0x00164A74 File Offset: 0x00163A74
		[WebSysDescription("WebPartManager_ConnectionsActivating")]
		[WebCategory("Action")]
		public event EventHandler ConnectionsActivating
		{
			add
			{
				base.Events.AddHandler(WebPartManager.ConnectionsActivatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.ConnectionsActivatingEvent, value);
			}
		}

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x060058F2 RID: 22770 RVA: 0x00164A87 File Offset: 0x00163A87
		// (remove) Token: 0x060058F3 RID: 22771 RVA: 0x00164A9A File Offset: 0x00163A9A
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_DisplayModeChanged")]
		public event WebPartDisplayModeEventHandler DisplayModeChanged
		{
			add
			{
				base.Events.AddHandler(WebPartManager.DisplayModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.DisplayModeChangedEvent, value);
			}
		}

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x060058F4 RID: 22772 RVA: 0x00164AAD File Offset: 0x00163AAD
		// (remove) Token: 0x060058F5 RID: 22773 RVA: 0x00164AC0 File Offset: 0x00163AC0
		[WebSysDescription("WebPartManager_DisplayModeChanging")]
		[WebCategory("Action")]
		public event WebPartDisplayModeCancelEventHandler DisplayModeChanging
		{
			add
			{
				base.Events.AddHandler(WebPartManager.DisplayModeChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.DisplayModeChangingEvent, value);
			}
		}

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x060058F6 RID: 22774 RVA: 0x00164AD3 File Offset: 0x00163AD3
		// (remove) Token: 0x060058F7 RID: 22775 RVA: 0x00164AE6 File Offset: 0x00163AE6
		[WebSysDescription("WebPartManager_SelectedWebPartChanged")]
		[WebCategory("Action")]
		public event WebPartEventHandler SelectedWebPartChanged
		{
			add
			{
				base.Events.AddHandler(WebPartManager.SelectedWebPartChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.SelectedWebPartChangedEvent, value);
			}
		}

		// Token: 0x14000114 RID: 276
		// (add) Token: 0x060058F8 RID: 22776 RVA: 0x00164AF9 File Offset: 0x00163AF9
		// (remove) Token: 0x060058F9 RID: 22777 RVA: 0x00164B0C File Offset: 0x00163B0C
		[WebSysDescription("WebPartManager_SelectedWebPartChanging")]
		[WebCategory("Action")]
		public event WebPartCancelEventHandler SelectedWebPartChanging
		{
			add
			{
				base.Events.AddHandler(WebPartManager.SelectedWebPartChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.SelectedWebPartChangingEvent, value);
			}
		}

		// Token: 0x14000115 RID: 277
		// (add) Token: 0x060058FA RID: 22778 RVA: 0x00164B1F File Offset: 0x00163B1F
		// (remove) Token: 0x060058FB RID: 22779 RVA: 0x00164B32 File Offset: 0x00163B32
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartAdded")]
		public event WebPartEventHandler WebPartAdded
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartAddedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartAddedEvent, value);
			}
		}

		// Token: 0x14000116 RID: 278
		// (add) Token: 0x060058FC RID: 22780 RVA: 0x00164B45 File Offset: 0x00163B45
		// (remove) Token: 0x060058FD RID: 22781 RVA: 0x00164B58 File Offset: 0x00163B58
		[WebSysDescription("WebPartManager_WebPartAdding")]
		[WebCategory("Action")]
		public event WebPartAddingEventHandler WebPartAdding
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartAddingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartAddingEvent, value);
			}
		}

		// Token: 0x14000117 RID: 279
		// (add) Token: 0x060058FE RID: 22782 RVA: 0x00164B6B File Offset: 0x00163B6B
		// (remove) Token: 0x060058FF RID: 22783 RVA: 0x00164B7E File Offset: 0x00163B7E
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartClosed")]
		public event WebPartEventHandler WebPartClosed
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartClosedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartClosedEvent, value);
			}
		}

		// Token: 0x14000118 RID: 280
		// (add) Token: 0x06005900 RID: 22784 RVA: 0x00164B91 File Offset: 0x00163B91
		// (remove) Token: 0x06005901 RID: 22785 RVA: 0x00164BA4 File Offset: 0x00163BA4
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartClosing")]
		public event WebPartCancelEventHandler WebPartClosing
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartClosingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartClosingEvent, value);
			}
		}

		// Token: 0x14000119 RID: 281
		// (add) Token: 0x06005902 RID: 22786 RVA: 0x00164BB7 File Offset: 0x00163BB7
		// (remove) Token: 0x06005903 RID: 22787 RVA: 0x00164BCA File Offset: 0x00163BCA
		[WebSysDescription("WebPartManager_WebPartDeleted")]
		[WebCategory("Action")]
		public event WebPartEventHandler WebPartDeleted
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartDeletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartDeletedEvent, value);
			}
		}

		// Token: 0x1400011A RID: 282
		// (add) Token: 0x06005904 RID: 22788 RVA: 0x00164BDD File Offset: 0x00163BDD
		// (remove) Token: 0x06005905 RID: 22789 RVA: 0x00164BF0 File Offset: 0x00163BF0
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartDeleting")]
		public event WebPartCancelEventHandler WebPartDeleting
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartDeletingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartDeletingEvent, value);
			}
		}

		// Token: 0x1400011B RID: 283
		// (add) Token: 0x06005906 RID: 22790 RVA: 0x00164C03 File Offset: 0x00163C03
		// (remove) Token: 0x06005907 RID: 22791 RVA: 0x00164C16 File Offset: 0x00163C16
		[WebSysDescription("WebPartManager_WebPartMoved")]
		[WebCategory("Action")]
		public event WebPartEventHandler WebPartMoved
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartMovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartMovedEvent, value);
			}
		}

		// Token: 0x1400011C RID: 284
		// (add) Token: 0x06005908 RID: 22792 RVA: 0x00164C29 File Offset: 0x00163C29
		// (remove) Token: 0x06005909 RID: 22793 RVA: 0x00164C3C File Offset: 0x00163C3C
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartMoving")]
		public event WebPartMovingEventHandler WebPartMoving
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartMovingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartMovingEvent, value);
			}
		}

		// Token: 0x1400011D RID: 285
		// (add) Token: 0x0600590A RID: 22794 RVA: 0x00164C4F File Offset: 0x00163C4F
		// (remove) Token: 0x0600590B RID: 22795 RVA: 0x00164C62 File Offset: 0x00163C62
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartsConnected")]
		public event WebPartConnectionsEventHandler WebPartsConnected
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartsConnectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartsConnectedEvent, value);
			}
		}

		// Token: 0x1400011E RID: 286
		// (add) Token: 0x0600590C RID: 22796 RVA: 0x00164C75 File Offset: 0x00163C75
		// (remove) Token: 0x0600590D RID: 22797 RVA: 0x00164C88 File Offset: 0x00163C88
		[WebSysDescription("WebPartManager_WebPartsConnecting")]
		[WebCategory("Action")]
		public event WebPartConnectionsCancelEventHandler WebPartsConnecting
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartsConnectingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartsConnectingEvent, value);
			}
		}

		// Token: 0x1400011F RID: 287
		// (add) Token: 0x0600590E RID: 22798 RVA: 0x00164C9B File Offset: 0x00163C9B
		// (remove) Token: 0x0600590F RID: 22799 RVA: 0x00164CAE File Offset: 0x00163CAE
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartsDisconnected")]
		public event WebPartConnectionsEventHandler WebPartsDisconnected
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartsDisconnectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartsDisconnectedEvent, value);
			}
		}

		// Token: 0x14000120 RID: 288
		// (add) Token: 0x06005910 RID: 22800 RVA: 0x00164CC1 File Offset: 0x00163CC1
		// (remove) Token: 0x06005911 RID: 22801 RVA: 0x00164CD4 File Offset: 0x00163CD4
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartsDisconnecting")]
		public event WebPartConnectionsCancelEventHandler WebPartsDisconnecting
		{
			add
			{
				base.Events.AddHandler(WebPartManager.WebPartsDisconnectingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(WebPartManager.WebPartsDisconnectingEvent, value);
			}
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x00164CE8 File Offset: 0x00163CE8
		protected virtual void ActivateConnections()
		{
			try
			{
				this._allowEventCancellation = false;
				foreach (WebPartConnection webPartConnection in this.ConnectionsToActivate())
				{
					webPartConnection.Activate();
				}
			}
			finally
			{
				this._allowEventCancellation = true;
			}
		}

		// Token: 0x06005913 RID: 22803 RVA: 0x00164D38 File Offset: 0x00163D38
		internal void AddWebPart(WebPart webPart)
		{
			((WebPartManager.WebPartManagerControlCollection)this.Controls).AddWebPart(webPart);
		}

		// Token: 0x06005914 RID: 22804 RVA: 0x00164D4C File Offset: 0x00163D4C
		private WebPart AddDynamicWebPartToZone(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			if (!this.IsAuthorized(webPart))
			{
				return null;
			}
			WebPart webPart2 = this.CopyWebPart(webPart);
			this.Internals.SetIsStatic(webPart2, false);
			this.Internals.SetIsShared(webPart2, this.Personalization.Scope == PersonalizationScope.Shared);
			this.AddWebPartToZone(webPart2, zone, zoneIndex);
			this.Internals.AddWebPart(webPart2);
			this.Personalization.CopyPersonalizationState(webPart, webPart2);
			this.OnWebPartAdded(new WebPartEventArgs(webPart2));
			return webPart2;
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x00164DC4 File Offset: 0x00163DC4
		public WebPart AddWebPart(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			this.Personalization.EnsureEnabled(true);
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (zone == null)
			{
				throw new ArgumentNullException("zone");
			}
			if (!this._webPartZones.Contains(zone))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_MustRegister"), "zone");
			}
			if (zoneIndex < 0)
			{
				throw new ArgumentOutOfRangeException("zoneIndex");
			}
			if (webPart.Zone != null && !webPart.IsClosed)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_AlreadyInZone"), "webPart");
			}
			WebPartAddingEventArgs webPartAddingEventArgs = new WebPartAddingEventArgs(webPart, zone, zoneIndex);
			this.OnWebPartAdding(webPartAddingEventArgs);
			if (this._allowEventCancellation && webPartAddingEventArgs.Cancel)
			{
				return null;
			}
			WebPart webPart2;
			if (this.Controls.Contains(webPart))
			{
				webPart2 = webPart;
				this.AddWebPartToZone(webPart, zone, zoneIndex);
				this.OnWebPartAdded(new WebPartEventArgs(webPart2));
			}
			else
			{
				webPart2 = this.AddDynamicWebPartToZone(webPart, zone, zoneIndex);
			}
			return webPart2;
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x00164EA4 File Offset: 0x00163EA4
		private void AddWebPartToDictionary(WebPart webPart)
		{
			if (this._partsForZone != null)
			{
				string zoneID = this.Internals.GetZoneID(webPart);
				if (!string.IsNullOrEmpty(zoneID))
				{
					SortedList sortedList = (SortedList)this._partsForZone[zoneID];
					if (sortedList == null)
					{
						sortedList = new SortedList(new WebPart.ZoneIndexComparer());
						this._partsForZone[zoneID] = sortedList;
					}
					sortedList.Add(webPart, null);
				}
			}
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x00164F04 File Offset: 0x00163F04
		private void AddWebPartToZone(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			IList allWebPartsForZone = this.GetAllWebPartsForZone(zone);
			WebPartCollection webPartsForZone = this.GetWebPartsForZone(zone);
			int num;
			if (zoneIndex < webPartsForZone.Count)
			{
				WebPart value = webPartsForZone[zoneIndex];
				num = allWebPartsForZone.IndexOf(value);
			}
			else
			{
				num = allWebPartsForZone.Count;
			}
			for (int i = 0; i < num; i++)
			{
				WebPart webPart2 = (WebPart)allWebPartsForZone[i];
				this.Internals.SetZoneIndex(webPart2, i);
			}
			for (int j = num; j < allWebPartsForZone.Count; j++)
			{
				WebPart webPart3 = (WebPart)allWebPartsForZone[j];
				this.Internals.SetZoneIndex(webPart3, j + 1);
			}
			this.Internals.SetZoneIndex(webPart, num);
			this.Internals.SetZoneID(webPart, zone.ID);
			this.Internals.SetIsClosed(webPart, false);
			this._hasDataChanged = true;
			this.AddWebPartToDictionary(webPart);
		}

		// Token: 0x06005918 RID: 22808 RVA: 0x00164FE0 File Offset: 0x00163FE0
		public virtual void BeginWebPartConnecting(WebPart webPart)
		{
			this.Personalization.EnsureEnabled(true);
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (webPart.IsClosed)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_CantBeginConnectingClosed"), "webPart");
			}
			if (!this.Controls.Contains(webPart))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "webPart");
			}
			if (this.DisplayMode != WebPartManager.ConnectDisplayMode)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_MustBeInConnect"));
			}
			if (webPart == this.SelectedWebPart)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_AlreadyInConnect"), "webPart");
			}
			WebPartCancelEventArgs webPartCancelEventArgs = new WebPartCancelEventArgs(webPart);
			this.OnSelectedWebPartChanging(webPartCancelEventArgs);
			if (this._allowEventCancellation && webPartCancelEventArgs.Cancel)
			{
				return;
			}
			if (this.SelectedWebPart != null)
			{
				this.EndWebPartConnecting();
				if (this.SelectedWebPart != null)
				{
					return;
				}
			}
			this.SetSelectedWebPart(webPart);
			this.Internals.CallOnConnectModeChanged(webPart);
			this.OnSelectedWebPartChanged(new WebPartEventArgs(webPart));
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x001650D8 File Offset: 0x001640D8
		public virtual void BeginWebPartEditing(WebPart webPart)
		{
			this.Personalization.EnsureEnabled(true);
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (webPart.IsClosed)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_CantBeginEditingClosed"), "webPart");
			}
			if (!this.Controls.Contains(webPart))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "webPart");
			}
			if (this.DisplayMode != WebPartManager.EditDisplayMode)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_MustBeInEdit"));
			}
			if (webPart == this.SelectedWebPart)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_AlreadyInEdit"), "webPart");
			}
			WebPartCancelEventArgs webPartCancelEventArgs = new WebPartCancelEventArgs(webPart);
			this.OnSelectedWebPartChanging(webPartCancelEventArgs);
			if (this._allowEventCancellation && webPartCancelEventArgs.Cancel)
			{
				return;
			}
			if (this.SelectedWebPart != null)
			{
				this.EndWebPartEditing();
				if (this.SelectedWebPart != null)
				{
					return;
				}
			}
			this.SetSelectedWebPart(webPart);
			this.Internals.CallOnEditModeChanged(webPart);
			this.OnSelectedWebPartChanged(new WebPartEventArgs(webPart));
		}

		// Token: 0x0600591A RID: 22810 RVA: 0x001651D0 File Offset: 0x001641D0
		protected virtual bool CheckRenderClientScript()
		{
			bool result = false;
			if (this.EnableClientScript && this.Page != null)
			{
				HttpBrowserCapabilities browser = this.Page.Request.Browser;
				if (browser.Win32 && browser.MSDomVersion.CompareTo(new Version(5, 5)) >= 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600591B RID: 22811 RVA: 0x00165220 File Offset: 0x00164220
		private void CloseOrphanedParts()
		{
			if (this.HasControls())
			{
				try
				{
					this._allowEventCancellation = false;
					foreach (object obj in this.Controls)
					{
						WebPart webPart = (WebPart)obj;
						if (webPart.IsOrphaned)
						{
							this.CloseWebPart(webPart);
						}
					}
				}
				finally
				{
					this._allowEventCancellation = true;
				}
			}
		}

		// Token: 0x0600591C RID: 22812 RVA: 0x001652A8 File Offset: 0x001642A8
		public bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			return this.CanConnectWebParts(provider, providerConnectionPoint, consumer, consumerConnectionPoint, null);
		}

		// Token: 0x0600591D RID: 22813 RVA: 0x001652B6 File Offset: 0x001642B6
		public virtual bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer)
		{
			return this.CanConnectWebPartsCore(provider, providerConnectionPoint, consumer, consumerConnectionPoint, transformer, false);
		}

		// Token: 0x0600591E RID: 22814 RVA: 0x001652C8 File Offset: 0x001642C8
		private bool CanConnectWebPartsCore(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer, bool throwOnError)
		{
			if (!this.Personalization.IsModifiable)
			{
				if (!throwOnError)
				{
					return false;
				}
				this.Personalization.EnsureEnabled(true);
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!this.Controls.Contains(provider))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "provider");
			}
			if (consumer == null)
			{
				throw new ArgumentNullException("consumer");
			}
			if (!this.Controls.Contains(consumer))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "consumer");
			}
			if (providerConnectionPoint == null)
			{
				throw new ArgumentNullException("providerConnectionPoint");
			}
			if (consumerConnectionPoint == null)
			{
				throw new ArgumentNullException("consumerConnectionPoint");
			}
			Control control = provider.ToControl();
			Control control2 = consumer.ToControl();
			if (providerConnectionPoint.ControlType != control.GetType())
			{
				throw new ArgumentException(SR.GetString("WebPartManager_InvalidConnectionPoint"), "providerConnectionPoint");
			}
			if (consumerConnectionPoint.ControlType != control2.GetType())
			{
				throw new ArgumentException(SR.GetString("WebPartManager_InvalidConnectionPoint"), "consumerConnectionPoint");
			}
			if (provider == consumer)
			{
				if (throwOnError)
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_CantConnectToSelf"));
				}
				return false;
			}
			else if (provider.IsClosed)
			{
				if (throwOnError)
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_CantConnectClosed", new object[]
					{
						provider.ID
					}));
				}
				return false;
			}
			else if (consumer.IsClosed)
			{
				if (throwOnError)
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_CantConnectClosed", new object[]
					{
						consumer.ID
					}));
				}
				return false;
			}
			else if (!providerConnectionPoint.GetEnabled(control))
			{
				if (throwOnError)
				{
					throw new InvalidOperationException(SR.GetString("WebPartConnection_DisabledConnectionPoint", new object[]
					{
						providerConnectionPoint.ID,
						provider.ID
					}));
				}
				return false;
			}
			else
			{
				if (consumerConnectionPoint.GetEnabled(control2))
				{
					if (!providerConnectionPoint.AllowsMultipleConnections)
					{
						foreach (object obj in this.Connections)
						{
							WebPartConnection webPartConnection = (WebPartConnection)obj;
							if (webPartConnection.Provider == provider && webPartConnection.ProviderConnectionPoint == providerConnectionPoint)
							{
								if (throwOnError)
								{
									throw new InvalidOperationException(SR.GetString("WebPartConnection_Duplicate", new object[]
									{
										providerConnectionPoint.ID,
										provider.ID
									}));
								}
								return false;
							}
						}
					}
					if (!consumerConnectionPoint.AllowsMultipleConnections)
					{
						foreach (object obj2 in this.Connections)
						{
							WebPartConnection webPartConnection2 = (WebPartConnection)obj2;
							if (webPartConnection2.Consumer == consumer && webPartConnection2.ConsumerConnectionPoint == consumerConnectionPoint)
							{
								if (throwOnError)
								{
									throw new InvalidOperationException(SR.GetString("WebPartConnection_Duplicate", new object[]
									{
										consumerConnectionPoint.ID,
										consumer.ID
									}));
								}
								return false;
							}
						}
					}
					if (transformer == null)
					{
						if (providerConnectionPoint.InterfaceType != consumerConnectionPoint.InterfaceType)
						{
							if (throwOnError)
							{
								throw new InvalidOperationException(SR.GetString("WebPartConnection_NoCommonInterface", new string[]
								{
									providerConnectionPoint.DisplayName,
									provider.ID,
									consumerConnectionPoint.DisplayName,
									consumer.ID
								}));
							}
							return false;
						}
						else
						{
							ConnectionInterfaceCollection secondaryInterfaces = providerConnectionPoint.GetSecondaryInterfaces(control);
							if (!consumerConnectionPoint.SupportsConnection(control2, secondaryInterfaces))
							{
								if (throwOnError)
								{
									throw new InvalidOperationException(SR.GetString("WebPartConnection_IncompatibleSecondaryInterfaces", new string[]
									{
										consumerConnectionPoint.DisplayName,
										consumer.ID,
										providerConnectionPoint.DisplayName,
										provider.ID
									}));
								}
								return false;
							}
						}
					}
					else
					{
						Type type = transformer.GetType();
						if (!this.AvailableTransformers.Contains(type))
						{
							throw new InvalidOperationException(SR.GetString("WebPartConnection_TransformerNotAvailable", new object[]
							{
								type.FullName
							}));
						}
						Type consumerType = WebPartTransformerAttribute.GetConsumerType(type);
						Type providerType = WebPartTransformerAttribute.GetProviderType(type);
						if (providerConnectionPoint.InterfaceType != consumerType)
						{
							if (throwOnError)
							{
								throw new InvalidOperationException(SR.GetString("WebPartConnection_IncompatibleProviderTransformer", new object[]
								{
									providerConnectionPoint.DisplayName,
									provider.ID,
									type.FullName
								}));
							}
							return false;
						}
						else if (providerType != consumerConnectionPoint.InterfaceType)
						{
							if (throwOnError)
							{
								throw new InvalidOperationException(SR.GetString("WebPartConnection_IncompatibleConsumerTransformer", new object[]
								{
									type.FullName,
									consumerConnectionPoint.DisplayName,
									consumer.ID
								}));
							}
							return false;
						}
						else if (!consumerConnectionPoint.SupportsConnection(control2, ConnectionInterfaceCollection.Empty))
						{
							if (throwOnError)
							{
								throw new InvalidOperationException(SR.GetString("WebPartConnection_ConsumerRequiresSecondaryInterfaces", new object[]
								{
									consumerConnectionPoint.DisplayName,
									consumer.ID
								}));
							}
							return false;
						}
					}
					return true;
				}
				if (throwOnError)
				{
					throw new InvalidOperationException(SR.GetString("WebPartConnection_DisabledConnectionPoint", new object[]
					{
						consumerConnectionPoint.ID,
						consumer.ID
					}));
				}
				return false;
			}
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x001657F4 File Offset: 0x001647F4
		public void CloseWebPart(WebPart webPart)
		{
			this.CloseOrDeleteWebPart(webPart, false);
		}

		// Token: 0x06005920 RID: 22816 RVA: 0x00165800 File Offset: 0x00164800
		private void CloseOrDeleteWebPart(WebPart webPart, bool delete)
		{
			this.Personalization.EnsureEnabled(true);
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (!this.Controls.Contains(webPart))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "webPart");
			}
			if (!delete && webPart.IsClosed)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_AlreadyClosed"), "webPart");
			}
			if (delete)
			{
				if (webPart.IsStatic)
				{
					throw new ArgumentException(SR.GetString("WebPartManager_CantDeleteStatic"), "webPart");
				}
				if (webPart.IsShared && this.Personalization.Scope == PersonalizationScope.User)
				{
					throw new ArgumentException(SR.GetString("WebPartManager_CantDeleteSharedInUserScope"), "webPart");
				}
			}
			WebPartCancelEventArgs webPartCancelEventArgs = new WebPartCancelEventArgs(webPart);
			if (delete)
			{
				this.OnWebPartDeleting(webPartCancelEventArgs);
			}
			else
			{
				this.OnWebPartClosing(webPartCancelEventArgs);
			}
			if (this._allowEventCancellation && webPartCancelEventArgs.Cancel)
			{
				return;
			}
			if (this.DisplayMode == WebPartManager.ConnectDisplayMode && webPart == this.SelectedWebPart)
			{
				this.EndWebPartConnecting();
				if (this.SelectedWebPart != null)
				{
					return;
				}
			}
			if (this.DisplayMode == WebPartManager.EditDisplayMode && webPart == this.SelectedWebPart)
			{
				this.EndWebPartEditing();
				if (this.SelectedWebPart != null)
				{
					return;
				}
			}
			if (delete)
			{
				this.Internals.CallOnDeleting(webPart);
			}
			else
			{
				this.Internals.CallOnClosing(webPart);
			}
			if (!webPart.IsClosed)
			{
				this.RemoveWebPartFromZone(webPart);
			}
			this.DisconnectWebPart(webPart);
			if (delete)
			{
				this.Internals.RemoveWebPart(webPart);
				this.OnWebPartDeleted(new WebPartEventArgs(webPart));
				return;
			}
			this.OnWebPartClosed(new WebPartEventArgs(webPart));
		}

		// Token: 0x06005921 RID: 22817 RVA: 0x00165984 File Offset: 0x00164984
		private WebPartConnection[] ConnectionsToActivate()
		{
			ArrayList arrayList = new ArrayList();
			HybridDictionary connectionIDs = new HybridDictionary(true);
			WebPartConnection[] array = new WebPartConnection[this.StaticConnections.Count + this.DynamicConnections.Count];
			this.StaticConnections.CopyTo(array, 0);
			this.DynamicConnections.CopyTo(array, this.StaticConnections.Count);
			foreach (WebPartConnection connection in array)
			{
				this.ConnectionsToActivateHelper(connection, connectionIDs, arrayList);
			}
			WebPartConnection[] array3 = (WebPartConnection[])arrayList.ToArray(typeof(WebPartConnection));
			foreach (WebPartConnection webPartConnection in array3)
			{
				if (!webPartConnection.IsShared)
				{
					ArrayList arrayList2 = new ArrayList();
					foreach (object obj in arrayList)
					{
						WebPartConnection webPartConnection2 = (WebPartConnection)obj;
						if (webPartConnection != webPartConnection2 && webPartConnection2.IsShared && webPartConnection.ConflictsWith(webPartConnection2))
						{
							arrayList2.Add(webPartConnection2);
						}
					}
					foreach (object obj2 in arrayList2)
					{
						WebPartConnection webPartConnection3 = (WebPartConnection)obj2;
						this.DisconnectWebParts(webPartConnection3);
						arrayList.Remove(webPartConnection3);
					}
				}
			}
			array3 = (WebPartConnection[])arrayList.ToArray(typeof(WebPartConnection));
			foreach (WebPartConnection webPartConnection4 in array3)
			{
				if (webPartConnection4.IsShared && !webPartConnection4.IsStatic)
				{
					ArrayList arrayList3 = new ArrayList();
					foreach (object obj3 in arrayList)
					{
						WebPartConnection webPartConnection5 = (WebPartConnection)obj3;
						if (webPartConnection4 != webPartConnection5 && webPartConnection5.IsStatic && webPartConnection4.ConflictsWith(webPartConnection5))
						{
							arrayList3.Add(webPartConnection5);
						}
					}
					foreach (object obj4 in arrayList3)
					{
						WebPartConnection webPartConnection6 = (WebPartConnection)obj4;
						this.DisconnectWebParts(webPartConnection6);
						arrayList.Remove(webPartConnection6);
					}
				}
			}
			ArrayList arrayList4 = new ArrayList();
			foreach (object obj5 in arrayList)
			{
				WebPartConnection webPartConnection7 = (WebPartConnection)obj5;
				bool flag = false;
				foreach (object obj6 in arrayList)
				{
					WebPartConnection webPartConnection8 = (WebPartConnection)obj6;
					if (webPartConnection7 != webPartConnection8)
					{
						if (webPartConnection7.ConflictsWithConsumer(webPartConnection8))
						{
							webPartConnection7.Consumer.SetConnectErrorMessage(SR.GetString("WebPartConnection_Duplicate", new object[]
							{
								webPartConnection7.ConsumerConnectionPoint.DisplayName,
								webPartConnection7.Consumer.DisplayTitle
							}));
							flag = true;
						}
						if (webPartConnection7.ConflictsWithProvider(webPartConnection8))
						{
							webPartConnection7.Consumer.SetConnectErrorMessage(SR.GetString("WebPartConnection_Duplicate", new object[]
							{
								webPartConnection7.ProviderConnectionPoint.DisplayName,
								webPartConnection7.Provider.DisplayTitle
							}));
							flag = true;
						}
					}
				}
				if (!flag)
				{
					arrayList4.Add(webPartConnection7);
				}
			}
			this.StaticConnections.SetReadOnly("WebPartManager_StaticConnectionsReadOnly");
			this.DynamicConnections.SetReadOnly("WebPartManager_DynamicConnectionsReadOnly");
			return (WebPartConnection[])arrayList4.ToArray(typeof(WebPartConnection));
		}

		// Token: 0x06005922 RID: 22818 RVA: 0x00165DFC File Offset: 0x00164DFC
		private void ConnectionsToActivateHelper(WebPartConnection connection, IDictionary connectionIDs, ArrayList connectionsToActivate)
		{
			string id = connection.ID;
			if (string.IsNullOrEmpty(id))
			{
				throw new InvalidOperationException(SR.GetString("WebPartConnection_NoID"));
			}
			if (connectionIDs.Contains(id))
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_DuplicateConnectionID", new object[]
				{
					id
				}));
			}
			connectionIDs.Add(id, null);
			if (connection.Deleted)
			{
				return;
			}
			WebPart provider = connection.Provider;
			if (provider == null)
			{
				if (connection.IsStatic)
				{
					throw new InvalidOperationException(SR.GetString("WebPartConnection_NoProvider", new object[]
					{
						connection.ProviderID
					}));
				}
				this.DisconnectWebParts(connection);
				return;
			}
			else
			{
				WebPart consumer = connection.Consumer;
				if (consumer == null)
				{
					if (connection.IsStatic)
					{
						throw new InvalidOperationException(SR.GetString("WebPartConnection_NoConsumer", new object[]
						{
							connection.ConsumerID
						}));
					}
					this.DisconnectWebParts(connection);
					return;
				}
				else
				{
					if (provider is ProxyWebPart || consumer is ProxyWebPart)
					{
						return;
					}
					Control control = provider.ToControl();
					Control control2 = consumer.ToControl();
					if (control == control2)
					{
						throw new InvalidOperationException(SR.GetString("WebPartManager_CantConnectToSelf"));
					}
					if (connection.ProviderConnectionPoint == null)
					{
						consumer.SetConnectErrorMessage(SR.GetString("WebPartConnection_NoProviderConnectionPoint", new object[]
						{
							connection.ProviderConnectionPointID,
							provider.DisplayTitle
						}));
						return;
					}
					if (connection.ConsumerConnectionPoint == null)
					{
						consumer.SetConnectErrorMessage(SR.GetString("WebPartConnection_NoConsumerConnectionPoint", new object[]
						{
							connection.ConsumerConnectionPointID,
							consumer.DisplayTitle
						}));
						return;
					}
					connectionsToActivate.Add(connection);
					return;
				}
			}
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x00165F90 File Offset: 0x00164F90
		public WebPartConnection ConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			return this.ConnectWebParts(provider, providerConnectionPoint, consumer, consumerConnectionPoint, null);
		}

		// Token: 0x06005924 RID: 22820 RVA: 0x00165FA0 File Offset: 0x00164FA0
		public virtual WebPartConnection ConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer)
		{
			this.CanConnectWebPartsCore(provider, providerConnectionPoint, consumer, consumerConnectionPoint, transformer, true);
			if (this.DynamicConnections.IsReadOnly)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_ConnectTooLate"));
			}
			WebPartConnectionsCancelEventArgs webPartConnectionsCancelEventArgs = new WebPartConnectionsCancelEventArgs(provider, providerConnectionPoint, consumer, consumerConnectionPoint);
			this.OnWebPartsConnecting(webPartConnectionsCancelEventArgs);
			if (this._allowEventCancellation && webPartConnectionsCancelEventArgs.Cancel)
			{
				return null;
			}
			Control control = provider.ToControl();
			Control control2 = consumer.ToControl();
			WebPartConnection webPartConnection = new WebPartConnection();
			webPartConnection.ID = this.CreateDynamicConnectionID();
			webPartConnection.ProviderID = control.ID;
			webPartConnection.ConsumerID = control2.ID;
			webPartConnection.ProviderConnectionPointID = providerConnectionPoint.ID;
			webPartConnection.ConsumerConnectionPointID = consumerConnectionPoint.ID;
			if (transformer != null)
			{
				this.Internals.SetTransformer(webPartConnection, transformer);
			}
			this.Internals.SetIsShared(webPartConnection, this.Personalization.Scope == PersonalizationScope.Shared);
			this.Internals.SetIsStatic(webPartConnection, false);
			this.DynamicConnections.Add(webPartConnection);
			this._hasDataChanged = true;
			this.OnWebPartsConnected(new WebPartConnectionsEventArgs(provider, providerConnectionPoint, consumer, consumerConnectionPoint, webPartConnection));
			return webPartConnection;
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x001660B0 File Offset: 0x001650B0
		protected virtual WebPart CopyWebPart(WebPart webPart)
		{
			GenericWebPart genericWebPart = webPart as GenericWebPart;
			WebPart webPart2;
			if (genericWebPart != null)
			{
				Control childControl = genericWebPart.ChildControl;
				this.VerifyType(childControl);
				Type type = childControl.GetType();
				Control control = (Control)this.Internals.CreateObjectFromType(type);
				control.ID = this.CreateDynamicWebPartID(type);
				webPart2 = this.CreateWebPart(control);
			}
			else
			{
				this.VerifyType(webPart);
				webPart2 = (WebPart)this.Internals.CreateObjectFromType(webPart.GetType());
			}
			webPart2.ID = this.CreateDynamicWebPartID(webPart.GetType());
			return webPart2;
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x0016613C File Offset: 0x0016513C
		protected virtual TransformerTypeCollection CreateAvailableTransformers()
		{
			TransformerTypeCollection transformerTypeCollection = new TransformerTypeCollection();
			WebPartsSection webParts = RuntimeConfig.GetConfig().WebParts;
			IDictionary transformerEntries = webParts.Transformers.GetTransformerEntries();
			foreach (object obj in transformerEntries.Values)
			{
				Type value = (Type)obj;
				transformerTypeCollection.Add(value);
			}
			return transformerTypeCollection;
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x001661BC File Offset: 0x001651BC
		private static ICollection[] CreateConnectionPoints(Type type)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				object[] customAttributes = methodInfo.GetCustomAttributes(typeof(ConnectionConsumerAttribute), true);
				if (customAttributes.Length == 1)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					Type type2 = null;
					if (parameters.Length == 1)
					{
						type2 = parameters[0].ParameterType;
					}
					if (!methodInfo.IsPublic || methodInfo.ReturnType != typeof(void) || type2 == null)
					{
						throw new InvalidOperationException(SR.GetString("WebPartManager_InvalidConsumerSignature", new object[]
						{
							methodInfo.Name,
							type.FullName
						}));
					}
					ConnectionConsumerAttribute connectionConsumerAttribute = customAttributes[0] as ConnectionConsumerAttribute;
					string displayName = connectionConsumerAttribute.DisplayName;
					string id = connectionConsumerAttribute.ID;
					Type connectionPointType = connectionConsumerAttribute.ConnectionPointType;
					bool allowsMultipleConnections = connectionConsumerAttribute.AllowsMultipleConnections;
					ConsumerConnectionPoint value;
					if (connectionPointType == null)
					{
						value = new ConsumerConnectionPoint(methodInfo, type2, type, displayName, id, allowsMultipleConnections);
					}
					else
					{
						object[] args = new object[]
						{
							methodInfo,
							type2,
							type,
							displayName,
							id,
							allowsMultipleConnections
						};
						value = (ConsumerConnectionPoint)Activator.CreateInstance(connectionPointType, args);
					}
					arrayList.Add(value);
				}
				object[] customAttributes2 = methodInfo.GetCustomAttributes(typeof(ConnectionProviderAttribute), true);
				if (customAttributes2.Length == 1)
				{
					Type returnType = methodInfo.ReturnType;
					if (!methodInfo.IsPublic || returnType == typeof(void) || methodInfo.GetParameters().Length != 0)
					{
						throw new InvalidOperationException(SR.GetString("WebPartManager_InvalidProviderSignature", new object[]
						{
							methodInfo.Name,
							type.FullName
						}));
					}
					ConnectionProviderAttribute connectionProviderAttribute = customAttributes2[0] as ConnectionProviderAttribute;
					string displayName2 = connectionProviderAttribute.DisplayName;
					string id2 = connectionProviderAttribute.ID;
					Type connectionPointType2 = connectionProviderAttribute.ConnectionPointType;
					bool allowsMultipleConnections2 = connectionProviderAttribute.AllowsMultipleConnections;
					ProviderConnectionPoint value2;
					if (connectionPointType2 == null)
					{
						value2 = new ProviderConnectionPoint(methodInfo, returnType, type, displayName2, id2, allowsMultipleConnections2);
					}
					else
					{
						object[] args2 = new object[]
						{
							methodInfo,
							returnType,
							type,
							displayName2,
							id2,
							allowsMultipleConnections2
						};
						value2 = (ProviderConnectionPoint)Activator.CreateInstance(connectionPointType2, args2);
					}
					arrayList2.Add(value2);
				}
			}
			return new ICollection[]
			{
				new ConsumerConnectionPointCollection(arrayList),
				new ProviderConnectionPointCollection(arrayList2)
			};
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x00166451 File Offset: 0x00165451
		protected sealed override ControlCollection CreateControlCollection()
		{
			return new WebPartManager.WebPartManagerControlCollection(this);
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0016645C File Offset: 0x0016545C
		protected virtual WebPartDisplayModeCollection CreateDisplayModes()
		{
			return new WebPartDisplayModeCollection
			{
				WebPartManager.BrowseDisplayMode,
				WebPartManager.CatalogDisplayMode,
				WebPartManager.DesignDisplayMode,
				WebPartManager.EditDisplayMode,
				WebPartManager.ConnectDisplayMode
			};
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x001664AC File Offset: 0x001654AC
		private string CreateDisplayTitle(string title, WebPart webPart, int count)
		{
			string text = title;
			if (webPart.Hidden)
			{
				text = SR.GetString("WebPart_HiddenFormatString", new object[]
				{
					text
				});
			}
			if (webPart is ErrorWebPart)
			{
				text = SR.GetString("WebPart_ErrorFormatString", new object[]
				{
					text
				});
			}
			if (count != 0)
			{
				if (count < WebPartManager.displayTitleSuffix.Length)
				{
					text += WebPartManager.displayTitleSuffix[count];
				}
				else
				{
					text = text + " [" + count.ToString(CultureInfo.CurrentCulture) + "]";
				}
			}
			return text;
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x00166534 File Offset: 0x00165534
		private IDictionary CreateDisplayTitles()
		{
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			foreach (object obj in this.Controls)
			{
				WebPart webPart = (WebPart)obj;
				string text = webPart.Title;
				if (string.IsNullOrEmpty(text))
				{
					text = SR.GetString("Part_Untitled");
				}
				if (webPart is UnauthorizedWebPart)
				{
					hashtable[webPart] = text;
				}
				else
				{
					ArrayList arrayList = (ArrayList)hashtable2[text];
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						hashtable2[text] = arrayList;
						hashtable[webPart] = this.CreateDisplayTitle(text, webPart, 0);
					}
					else
					{
						int count = arrayList.Count;
						if (count == 1)
						{
							WebPart webPart2 = (WebPart)arrayList[0];
							hashtable[webPart2] = this.CreateDisplayTitle(text, webPart2, 1);
						}
						hashtable[webPart] = this.CreateDisplayTitle(text, webPart, count + 1);
					}
					arrayList.Add(webPart);
				}
			}
			return hashtable;
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x0016664C File Offset: 0x0016564C
		protected virtual string CreateDynamicConnectionID()
		{
			return "c" + Math.Abs(Guid.NewGuid().GetHashCode()).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600592D RID: 22829 RVA: 0x00166688 File Offset: 0x00165688
		protected virtual string CreateDynamicWebPartID(Type webPartType)
		{
			if (webPartType == null)
			{
				throw new ArgumentNullException("webPartType");
			}
			string text = "wp" + Math.Abs(Guid.NewGuid().GetHashCode()).ToString(CultureInfo.InvariantCulture);
			if (this.Page != null && this.Page.Trace.IsEnabled)
			{
				text += webPartType.Name;
			}
			return text;
		}

		// Token: 0x0600592E RID: 22830 RVA: 0x001666FC File Offset: 0x001656FC
		protected virtual ErrorWebPart CreateErrorWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID, string errorMessage)
		{
			return new ErrorWebPart(originalID, originalTypeName, originalPath, genericWebPartID)
			{
				ErrorMessage = errorMessage
			};
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0016671D File Offset: 0x0016571D
		protected virtual WebPartPersonalization CreatePersonalization()
		{
			return new WebPartPersonalization(this);
		}

		// Token: 0x06005930 RID: 22832 RVA: 0x00166725 File Offset: 0x00165725
		public virtual GenericWebPart CreateWebPart(Control control)
		{
			return WebPartManager.CreateWebPartStatic(control);
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x00166730 File Offset: 0x00165730
		internal static GenericWebPart CreateWebPartStatic(Control control)
		{
			GenericWebPart genericWebPart = new GenericWebPart(control);
			genericWebPart.CreateChildControls();
			return genericWebPart;
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x0016674B File Offset: 0x0016574B
		public void DeleteWebPart(WebPart webPart)
		{
			this.CloseOrDeleteWebPart(webPart, true);
		}

		// Token: 0x06005933 RID: 22835 RVA: 0x00166758 File Offset: 0x00165758
		protected virtual void DisconnectWebPart(WebPart webPart)
		{
			try
			{
				this._allowEventCancellation = false;
				foreach (object obj in this.Connections)
				{
					WebPartConnection webPartConnection = (WebPartConnection)obj;
					if (webPartConnection.Provider == webPart || webPartConnection.Consumer == webPart)
					{
						this.DisconnectWebParts(webPartConnection);
					}
				}
			}
			finally
			{
				this._allowEventCancellation = true;
			}
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x001667E0 File Offset: 0x001657E0
		public virtual void DisconnectWebParts(WebPartConnection connection)
		{
			this.Personalization.EnsureEnabled(true);
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			WebPart provider = connection.Provider;
			ProviderConnectionPoint providerConnectionPoint = connection.ProviderConnectionPoint;
			WebPart consumer = connection.Consumer;
			ConsumerConnectionPoint consumerConnectionPoint = connection.ConsumerConnectionPoint;
			WebPartConnectionsCancelEventArgs webPartConnectionsCancelEventArgs = new WebPartConnectionsCancelEventArgs(provider, providerConnectionPoint, consumer, consumerConnectionPoint, connection);
			this.OnWebPartsDisconnecting(webPartConnectionsCancelEventArgs);
			if (this._allowEventCancellation && webPartConnectionsCancelEventArgs.Cancel)
			{
				return;
			}
			WebPartConnectionsEventArgs e = new WebPartConnectionsEventArgs(provider, providerConnectionPoint, consumer, consumerConnectionPoint);
			if (this.StaticConnections.Contains(connection))
			{
				if (this.StaticConnections.IsReadOnly)
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_DisconnectTooLate"));
				}
				if (this.Internals.ConnectionDeleted(connection))
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_AlreadyDisconnected"));
				}
				this.Internals.DeleteConnection(connection);
				this._hasDataChanged = true;
				this.OnWebPartsDisconnected(e);
				return;
			}
			else
			{
				if (!this.DynamicConnections.Contains(connection))
				{
					throw new ArgumentException(SR.GetString("WebPartManager_UnknownConnection"), "connection");
				}
				if (this.DynamicConnections.IsReadOnly)
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_DisconnectTooLate"));
				}
				if (this.ShouldRemoveConnection(connection))
				{
					this.DynamicConnections.Remove(connection);
				}
				else
				{
					if (this.Internals.ConnectionDeleted(connection))
					{
						throw new InvalidOperationException(SR.GetString("WebPartManager_AlreadyDisconnected"));
					}
					this.Internals.DeleteConnection(connection);
				}
				this._hasDataChanged = true;
				this.OnWebPartsDisconnected(e);
				return;
			}
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x0016694C File Offset: 0x0016594C
		public virtual void EndWebPartConnecting()
		{
			this.Personalization.EnsureEnabled(true);
			WebPart selectedWebPart = this.SelectedWebPart;
			if (selectedWebPart == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_NoSelectedWebPartConnect"));
			}
			WebPartCancelEventArgs webPartCancelEventArgs = new WebPartCancelEventArgs(selectedWebPart);
			this.OnSelectedWebPartChanging(webPartCancelEventArgs);
			if (this._allowEventCancellation && webPartCancelEventArgs.Cancel)
			{
				return;
			}
			this.SetSelectedWebPart(null);
			this.Internals.CallOnConnectModeChanged(selectedWebPart);
			this.OnSelectedWebPartChanged(new WebPartEventArgs(null));
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x001669C0 File Offset: 0x001659C0
		public virtual void EndWebPartEditing()
		{
			this.Personalization.EnsureEnabled(true);
			WebPart selectedWebPart = this.SelectedWebPart;
			if (selectedWebPart == null)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_NoSelectedWebPartEdit"));
			}
			WebPartCancelEventArgs webPartCancelEventArgs = new WebPartCancelEventArgs(selectedWebPart);
			this.OnSelectedWebPartChanging(webPartCancelEventArgs);
			if (this._allowEventCancellation && webPartCancelEventArgs.Cancel)
			{
				return;
			}
			this.SetSelectedWebPart(null);
			this.Internals.CallOnEditModeChanged(selectedWebPart);
			this.OnSelectedWebPartChanged(new WebPartEventArgs(null));
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x00166A34 File Offset: 0x00165A34
		public virtual void ExportWebPart(WebPart webPart, XmlWriter writer)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (!this.Controls.Contains(webPart))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "webPart");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (webPart.ExportMode == WebPartExportMode.None)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_PartNotExportable"), "webPart");
			}
			bool excludeSensitive = webPart.ExportMode == WebPartExportMode.NonSensitiveData && this.Personalization.Scope != PersonalizationScope.Shared;
			writer.WriteStartElement("webParts");
			writer.WriteStartElement("webPart");
			writer.WriteAttributeString("xmlns", "http://schemas.microsoft.com/WebPart/v3");
			writer.WriteStartElement("metaData");
			writer.WriteStartElement("type");
			Control control = webPart.ToControl();
			UserControl userControl = control as UserControl;
			if (userControl != null)
			{
				writer.WriteAttributeString("src", userControl.AppRelativeVirtualPath);
			}
			else
			{
				writer.WriteAttributeString("name", WebPartUtil.SerializeType(control.GetType()));
			}
			writer.WriteEndElement();
			writer.WriteElementString("importErrorMessage", webPart.ImportErrorMessage);
			writer.WriteEndElement();
			writer.WriteStartElement("data");
			IDictionary personalizablePropertyValues = PersonalizableAttribute.GetPersonalizablePropertyValues(webPart, PersonalizationScope.Shared, excludeSensitive);
			writer.WriteStartElement("properties");
			GenericWebPart genericWebPart = webPart as GenericWebPart;
			if (genericWebPart != null)
			{
				this.ExportIPersonalizable(writer, control, excludeSensitive);
				IDictionary personalizablePropertyValues2 = PersonalizableAttribute.GetPersonalizablePropertyValues(control, PersonalizationScope.Shared, excludeSensitive);
				this.ExportToWriter(personalizablePropertyValues2, writer);
				writer.WriteEndElement();
				writer.WriteStartElement("genericWebPartProperties");
				this.ExportIPersonalizable(writer, webPart, excludeSensitive);
				this.ExportToWriter(personalizablePropertyValues, writer);
			}
			else
			{
				this.ExportIPersonalizable(writer, webPart, excludeSensitive);
				this.ExportToWriter(personalizablePropertyValues, writer);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x00166BE4 File Offset: 0x00165BE4
		private void ExportIPersonalizable(XmlWriter writer, Control control, bool excludeSensitive)
		{
			IPersonalizable personalizable = control as IPersonalizable;
			if (personalizable != null)
			{
				PersonalizationDictionary personalizationDictionary = new PersonalizationDictionary();
				personalizable.Save(personalizationDictionary);
				if (personalizationDictionary.Count > 0)
				{
					writer.WriteStartElement("ipersonalizable");
					this.ExportToWriter(personalizationDictionary, writer, true, excludeSensitive);
					writer.WriteEndElement();
				}
			}
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x00166C2C File Offset: 0x00165C2C
		private static void ExportProperty(XmlWriter writer, string name, string value, Type type, PersonalizationScope scope, bool isIPersonalizable)
		{
			writer.WriteStartElement("property");
			writer.WriteAttributeString("name", name);
			writer.WriteAttributeString("type", WebPartManager.GetExportName(type));
			if (isIPersonalizable)
			{
				writer.WriteAttributeString("scope", scope.ToString());
			}
			if (value == null)
			{
				writer.WriteAttributeString("null", "true");
			}
			else
			{
				writer.WriteString(value);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x00166C9E File Offset: 0x00165C9E
		private void ExportToWriter(IDictionary propBag, XmlWriter writer)
		{
			this.ExportToWriter(propBag, writer, false, false);
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x00166CAC File Offset: 0x00165CAC
		private void ExportToWriter(IDictionary propBag, XmlWriter writer, bool isIPersonalizable, bool excludeSensitive)
		{
			foreach (object obj in propBag)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (!(text == "AuthorizationFilter") && !(text == "ImportErrorMessage"))
				{
					PropertyInfo propertyInfo = null;
					object obj2 = null;
					Pair pair = dictionaryEntry.Value as Pair;
					PersonalizationScope scope = PersonalizationScope.User;
					if (!isIPersonalizable && pair != null)
					{
						propertyInfo = (PropertyInfo)pair.First;
						obj2 = pair.Second;
					}
					else if (isIPersonalizable)
					{
						PersonalizationEntry personalizationEntry = dictionaryEntry.Value as PersonalizationEntry;
						if (personalizationEntry != null && (this.Personalization.Scope == PersonalizationScope.Shared || personalizationEntry.Scope == PersonalizationScope.User))
						{
							obj2 = personalizationEntry.Value;
							scope = personalizationEntry.Scope;
						}
						if (excludeSensitive && personalizationEntry.IsSensitive)
						{
							continue;
						}
					}
					Type type = (propertyInfo != null) ? propertyInfo.PropertyType : ((obj2 != null) ? obj2.GetType() : typeof(object));
					string value;
					if (this.ShouldExportProperty(propertyInfo, type, obj2, out value))
					{
						WebPartManager.ExportProperty(writer, text, value, type, scope, isIPersonalizable);
					}
				}
			}
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00166DFC File Offset: 0x00165DFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x00166E30 File Offset: 0x00165E30
		private IList GetAllWebPartsForZone(WebPartZoneBase zone)
		{
			if (this._partsForZone == null)
			{
				this._partsForZone = new HybridDictionary(true);
				foreach (object obj in this.Controls)
				{
					WebPart webPart = (WebPart)obj;
					if (!webPart.IsClosed)
					{
						string zoneID = this.Internals.GetZoneID(webPart);
						if (!string.IsNullOrEmpty(zoneID))
						{
							SortedList sortedList = (SortedList)this._partsForZone[zoneID];
							if (sortedList == null)
							{
								sortedList = new SortedList(new WebPart.ZoneIndexComparer());
								this._partsForZone[zoneID] = sortedList;
							}
							sortedList.Add(webPart, null);
						}
					}
				}
			}
			SortedList sortedList2 = (SortedList)this._partsForZone[zone.ID];
			if (sortedList2 == null)
			{
				sortedList2 = new SortedList();
			}
			return sortedList2.GetKeyList();
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x00166F18 File Offset: 0x00165F18
		private static ICollection[] GetConnectionPoints(Type type)
		{
			if (WebPartManager.ConnectionPointsCache == null)
			{
				WebPartManager.ConnectionPointsCache = Hashtable.Synchronized(new Hashtable());
			}
			WebPartManager.ConnectionPointKey key = new WebPartManager.ConnectionPointKey(type, CultureInfo.CurrentCulture, CultureInfo.CurrentUICulture);
			ICollection[] array = (ICollection[])WebPartManager.ConnectionPointsCache[key];
			if (array == null)
			{
				array = WebPartManager.CreateConnectionPoints(type);
				WebPartManager.ConnectionPointsCache[key] = array;
			}
			return array;
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x00166F74 File Offset: 0x00165F74
		internal ConsumerConnectionPoint GetConsumerConnectionPoint(WebPart webPart, string connectionPointID)
		{
			ConsumerConnectionPointCollection consumerConnectionPoints = this.GetConsumerConnectionPoints(webPart);
			if (consumerConnectionPoints != null && consumerConnectionPoints.Count > 0)
			{
				return consumerConnectionPoints[connectionPointID];
			}
			return null;
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x00166F9E File Offset: 0x00165F9E
		public virtual ConsumerConnectionPointCollection GetConsumerConnectionPoints(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			return WebPartManager.GetConsumerConnectionPoints(webPart.ToControl().GetType());
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x00166FC0 File Offset: 0x00165FC0
		private static ConsumerConnectionPointCollection GetConsumerConnectionPoints(Type type)
		{
			ICollection[] connectionPoints = WebPartManager.GetConnectionPoints(type);
			return (ConsumerConnectionPointCollection)connectionPoints[0];
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x00166FDC File Offset: 0x00165FDC
		public static WebPartManager GetCurrentWebPartManager(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			return page.Items[typeof(WebPartManager)] as WebPartManager;
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x00167008 File Offset: 0x00166008
		protected internal virtual string GetDisplayTitle(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (!this.Controls.Contains(webPart))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "webPart");
			}
			if (!this._allowCreateDisplayTitles)
			{
				return string.Empty;
			}
			if (this._displayTitles == null)
			{
				this._displayTitles = this.CreateDisplayTitles();
			}
			return (string)this._displayTitles[webPart];
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x0016707C File Offset: 0x0016607C
		private static ICollection GetEnabledConnectionPoints(ICollection connectionPoints, WebPart webPart)
		{
			Control control = webPart.ToControl();
			ArrayList arrayList = new ArrayList();
			foreach (object obj in connectionPoints)
			{
				ConnectionPoint connectionPoint = (ConnectionPoint)obj;
				if (connectionPoint.GetEnabled(control))
				{
					arrayList.Add(connectionPoint);
				}
			}
			return arrayList;
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x001670EC File Offset: 0x001660EC
		internal ConsumerConnectionPointCollection GetEnabledConsumerConnectionPoints(WebPart webPart)
		{
			ICollection enabledConnectionPoints = WebPartManager.GetEnabledConnectionPoints(this.GetConsumerConnectionPoints(webPart), webPart);
			return new ConsumerConnectionPointCollection(enabledConnectionPoints);
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x00167110 File Offset: 0x00166110
		internal ProviderConnectionPointCollection GetEnabledProviderConnectionPoints(WebPart webPart)
		{
			ICollection enabledConnectionPoints = WebPartManager.GetEnabledConnectionPoints(this.GetProviderConnectionPoints(webPart), webPart);
			return new ProviderConnectionPointCollection(enabledConnectionPoints);
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x00167134 File Offset: 0x00166134
		public string GetExportUrl(WebPart webPart)
		{
			string text = (this.Personalization.Scope == PersonalizationScope.Shared) ? "&scope=shared" : string.Empty;
			string queryStringText = this.Page.Request.QueryStringText;
			return string.Concat(new string[]
			{
				this.Page.Request.FilePath,
				"?__WEBPARTEXPORT=true&webPart=",
				HttpUtility.UrlEncode(webPart.ID),
				(!string.IsNullOrEmpty(queryStringText)) ? ("&query=" + HttpUtility.UrlEncode(queryStringText)) : string.Empty,
				text
			});
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x001671CC File Offset: 0x001661CC
		private static Type GetExportType(string name)
		{
			switch (name)
			{
			case "string":
				return typeof(string);
			case "int":
				return typeof(int);
			case "bool":
				return typeof(bool);
			case "double":
				return typeof(double);
			case "single":
				return typeof(float);
			case "datetime":
				return typeof(DateTime);
			case "color":
				return typeof(Color);
			case "unit":
				return typeof(Unit);
			case "fontsize":
				return typeof(FontSize);
			case "direction":
				return typeof(ContentDirection);
			case "helpmode":
				return typeof(WebPartHelpMode);
			case "chromestate":
				return typeof(PartChromeState);
			case "chrometype":
				return typeof(PartChromeType);
			case "exportmode":
				return typeof(WebPartExportMode);
			case "object":
				return typeof(object);
			}
			return WebPartUtil.DeserializeType(name, false);
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x001673BC File Offset: 0x001663BC
		private static string GetExportName(Type type)
		{
			if (type == typeof(string))
			{
				return "string";
			}
			if (type == typeof(int))
			{
				return "int";
			}
			if (type == typeof(bool))
			{
				return "bool";
			}
			if (type == typeof(double))
			{
				return "double";
			}
			if (type == typeof(float))
			{
				return "single";
			}
			if (type == typeof(DateTime))
			{
				return "datetime";
			}
			if (type == typeof(Color))
			{
				return "color";
			}
			if (type == typeof(Unit))
			{
				return "unit";
			}
			if (type == typeof(FontSize))
			{
				return "fontsize";
			}
			if (type == typeof(ContentDirection))
			{
				return "direction";
			}
			if (type == typeof(WebPartHelpMode))
			{
				return "helpmode";
			}
			if (type == typeof(PartChromeState))
			{
				return "chromestate";
			}
			if (type == typeof(PartChromeType))
			{
				return "chrometype";
			}
			if (type == typeof(WebPartExportMode))
			{
				return "exportmode";
			}
			if (type == typeof(object))
			{
				return "object";
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x001674EC File Offset: 0x001664EC
		public GenericWebPart GetGenericWebPart(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Control parent = control.Parent;
			GenericWebPart genericWebPart = parent as GenericWebPart;
			if (genericWebPart != null && genericWebPart.ChildControl == control)
			{
				return genericWebPart;
			}
			foreach (object obj in this.Controls)
			{
				WebPart webPart = (WebPart)obj;
				GenericWebPart genericWebPart2 = webPart as GenericWebPart;
				if (genericWebPart2 != null && genericWebPart2.ChildControl == control)
				{
					return genericWebPart2;
				}
			}
			return null;
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x0016758C File Offset: 0x0016658C
		internal ProviderConnectionPoint GetProviderConnectionPoint(WebPart webPart, string connectionPointID)
		{
			ProviderConnectionPointCollection providerConnectionPoints = this.GetProviderConnectionPoints(webPart);
			if (providerConnectionPoints != null && providerConnectionPoints.Count > 0)
			{
				return providerConnectionPoints[connectionPointID];
			}
			return null;
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x001675B6 File Offset: 0x001665B6
		public virtual ProviderConnectionPointCollection GetProviderConnectionPoints(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			return WebPartManager.GetProviderConnectionPoints(webPart.ToControl().GetType());
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x001675D8 File Offset: 0x001665D8
		private static ProviderConnectionPointCollection GetProviderConnectionPoints(Type type)
		{
			ICollection[] connectionPoints = WebPartManager.GetConnectionPoints(type);
			return (ProviderConnectionPointCollection)connectionPoints[1];
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x001675F4 File Offset: 0x001665F4
		internal WebPartCollection GetWebPartsForZone(WebPartZoneBase zone)
		{
			if (zone == null)
			{
				throw new ArgumentNullException("zone");
			}
			if (!this._webPartZones.Contains(zone))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_MustRegister"), "zone");
			}
			IList allWebPartsForZone = this.GetAllWebPartsForZone(zone);
			WebPartCollection webPartCollection = new WebPartCollection();
			if (allWebPartsForZone.Count > 0)
			{
				foreach (object obj in allWebPartsForZone)
				{
					WebPart webPart = (WebPart)obj;
					if (this.ShouldRenderWebPartInZone(webPart, zone))
					{
						webPartCollection.Add(webPart);
					}
				}
			}
			return webPartCollection;
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x001676A0 File Offset: 0x001666A0
		internal WebPartConnection GetConnectionForConsumer(WebPart consumer, ConsumerConnectionPoint connectionPoint)
		{
			ConsumerConnectionPoint consumerConnectionPoint = connectionPoint ?? this.GetConsumerConnectionPoint(consumer, null);
			foreach (object obj in this.StaticConnections)
			{
				WebPartConnection webPartConnection = (WebPartConnection)obj;
				if (!this.Internals.ConnectionDeleted(webPartConnection) && webPartConnection.Consumer == consumer)
				{
					ConsumerConnectionPoint consumerConnectionPoint2 = this.GetConsumerConnectionPoint(consumer, webPartConnection.ConsumerConnectionPointID);
					if (consumerConnectionPoint2 == consumerConnectionPoint)
					{
						return webPartConnection;
					}
				}
			}
			foreach (object obj2 in this.DynamicConnections)
			{
				WebPartConnection webPartConnection2 = (WebPartConnection)obj2;
				if (!this.Internals.ConnectionDeleted(webPartConnection2) && webPartConnection2.Consumer == consumer)
				{
					ConsumerConnectionPoint consumerConnectionPoint3 = this.GetConsumerConnectionPoint(consumer, webPartConnection2.ConsumerConnectionPointID);
					if (consumerConnectionPoint3 == consumerConnectionPoint)
					{
						return webPartConnection2;
					}
				}
			}
			return null;
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x001677B4 File Offset: 0x001667B4
		internal WebPartConnection GetConnectionForProvider(WebPart provider, ProviderConnectionPoint connectionPoint)
		{
			ProviderConnectionPoint providerConnectionPoint = connectionPoint ?? this.GetProviderConnectionPoint(provider, null);
			foreach (object obj in this.StaticConnections)
			{
				WebPartConnection webPartConnection = (WebPartConnection)obj;
				if (!this.Internals.ConnectionDeleted(webPartConnection) && webPartConnection.Provider == provider)
				{
					ProviderConnectionPoint providerConnectionPoint2 = this.GetProviderConnectionPoint(provider, webPartConnection.ProviderConnectionPointID);
					if (providerConnectionPoint2 == providerConnectionPoint)
					{
						return webPartConnection;
					}
				}
			}
			foreach (object obj2 in this.DynamicConnections)
			{
				WebPartConnection webPartConnection2 = (WebPartConnection)obj2;
				if (!this.Internals.ConnectionDeleted(webPartConnection2) && webPartConnection2.Provider == provider)
				{
					ProviderConnectionPoint providerConnectionPoint3 = this.GetProviderConnectionPoint(provider, webPartConnection2.ProviderConnectionPointID);
					if (providerConnectionPoint3 == providerConnectionPoint)
					{
						return webPartConnection2;
					}
				}
			}
			return null;
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x001678C8 File Offset: 0x001668C8
		private static void ImportReadTo(XmlReader reader, string elementToFind)
		{
			while (reader.Name != elementToFind)
			{
				if (!reader.Read())
				{
					throw new XmlException();
				}
			}
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x001678E8 File Offset: 0x001668E8
		private static void ImportReadTo(XmlReader reader, string elementToFindA, string elementToFindB)
		{
			while (reader.Name != elementToFindA && reader.Name != elementToFindB)
			{
				if (!reader.Read())
				{
					throw new XmlException();
				}
			}
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x00167916 File Offset: 0x00166916
		private static void ImportSkipTo(XmlReader reader, string elementToFind)
		{
			while (reader.Name != elementToFind)
			{
				reader.Skip();
				if (reader.EOF)
				{
					throw new XmlException();
				}
			}
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x0016793C File Offset: 0x0016693C
		public virtual WebPart ImportWebPart(XmlReader reader, out string errorMessage)
		{
			this.Personalization.EnsureEnabled(true);
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.MinimalPermissionSet.PermitOnly();
			bool flag = true;
			string text = string.Empty;
			WebPart result;
			try
			{
				try
				{
					reader.MoveToContent();
					reader.ReadStartElement("webParts");
					WebPartManager.ImportSkipTo(reader, "webPart");
					string attribute = reader.GetAttribute("xmlns");
					if (string.IsNullOrEmpty(attribute))
					{
						errorMessage = SR.GetString("WebPart_ImportErrorNoVersion");
						result = null;
					}
					else if (!string.Equals(attribute, "http://schemas.microsoft.com/WebPart/v3", StringComparison.OrdinalIgnoreCase))
					{
						errorMessage = SR.GetString("WebPart_ImportErrorInvalidVersion");
						result = null;
					}
					else
					{
						WebPartManager.ImportReadTo(reader, "metaData");
						reader.ReadStartElement("metaData");
						WebPartManager.ImportSkipTo(reader, "type");
						string attribute2 = reader.GetAttribute("name");
						string attribute3 = reader.GetAttribute("src");
						WebPartManager.ImportSkipTo(reader, "importErrorMessage");
						text = reader.ReadElementString();
						WebPart webPart = null;
						Control control = null;
						Type type;
						try
						{
							bool isShared = this.Personalization.Scope == PersonalizationScope.Shared;
							if (!string.IsNullOrEmpty(attribute2))
							{
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
								this.MediumPermissionSet.PermitOnly();
								flag = true;
								type = WebPartUtil.DeserializeType(attribute2, true);
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
								this.MinimalPermissionSet.PermitOnly();
								flag = true;
								if (!this.IsAuthorized(type, null, null, isShared))
								{
									errorMessage = SR.GetString("WebPartManager_ForbiddenType");
									return null;
								}
								if (!type.IsSubclassOf(typeof(WebPart)))
								{
									if (!type.IsSubclassOf(typeof(Control)))
									{
										errorMessage = SR.GetString("WebPartManager_TypeMustDeriveFromControl");
										return null;
									}
									control = (Control)this.Internals.CreateObjectFromType(type);
									control.ID = this.CreateDynamicWebPartID(type);
									webPart = this.CreateWebPart(control);
								}
								else
								{
									webPart = (WebPart)this.Internals.CreateObjectFromType(type);
								}
							}
							else
							{
								if (!this.IsAuthorized(typeof(UserControl), attribute3, null, isShared))
								{
									errorMessage = SR.GetString("WebPartManager_ForbiddenType");
									return null;
								}
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
								control = this.Page.LoadControl(attribute3);
								type = control.GetType();
								this.MinimalPermissionSet.PermitOnly();
								flag = true;
								control.ID = this.CreateDynamicWebPartID(type);
								webPart = this.CreateWebPart(control);
							}
						}
						catch
						{
							if (!string.IsNullOrEmpty(text))
							{
								errorMessage = text;
							}
							else
							{
								errorMessage = SR.GetString("WebPartManager_ErrorLoadingWebPartType");
							}
							return null;
						}
						if (string.IsNullOrEmpty(text))
						{
							text = SR.GetString("WebPart_DefaultImportErrorMessage");
						}
						WebPartManager.ImportSkipTo(reader, "data");
						reader.ReadStartElement("data");
						WebPartManager.ImportSkipTo(reader, "properties");
						if (!reader.IsEmptyElement)
						{
							reader.ReadStartElement("properties");
							CodeAccessPermission.RevertPermitOnly();
							flag = false;
							this.ImportIPersonalizable(reader, (control != null) ? control : webPart);
							this.MinimalPermissionSet.PermitOnly();
							flag = true;
						}
						IDictionary personalizablePropertyEntries;
						if (control != null)
						{
							if (!reader.IsEmptyElement)
							{
								personalizablePropertyEntries = PersonalizableAttribute.GetPersonalizablePropertyEntries(type);
								while (reader.Name != "property")
								{
									reader.Skip();
									if (reader.EOF)
									{
										errorMessage = null;
										return webPart;
									}
								}
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
								this.ImportFromReader(personalizablePropertyEntries, control, reader);
								this.MinimalPermissionSet.PermitOnly();
								flag = true;
							}
							WebPartManager.ImportSkipTo(reader, "genericWebPartProperties");
							reader.ReadStartElement("genericWebPartProperties");
							CodeAccessPermission.RevertPermitOnly();
							flag = false;
							this.ImportIPersonalizable(reader, webPart);
							this.MinimalPermissionSet.PermitOnly();
							flag = true;
							personalizablePropertyEntries = PersonalizableAttribute.GetPersonalizablePropertyEntries(webPart.GetType());
						}
						else
						{
							personalizablePropertyEntries = PersonalizableAttribute.GetPersonalizablePropertyEntries(type);
						}
						while (reader.Name != "property")
						{
							reader.Skip();
							if (reader.EOF)
							{
								errorMessage = null;
								return webPart;
							}
						}
						CodeAccessPermission.RevertPermitOnly();
						flag = false;
						this.ImportFromReader(personalizablePropertyEntries, webPart, reader);
						this.MinimalPermissionSet.PermitOnly();
						flag = true;
						errorMessage = null;
						result = webPart;
					}
				}
				catch (XmlException)
				{
					errorMessage = SR.GetString("WebPartManager_ImportInvalidFormat");
					result = null;
				}
				catch (Exception ex)
				{
					if (this.Context != null && this.Context.IsCustomErrorEnabled)
					{
						errorMessage = ((text.Length != 0) ? text : SR.GetString("WebPart_DefaultImportErrorMessage"));
					}
					else
					{
						errorMessage = ex.Message;
					}
					result = null;
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertPermitOnly();
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06005955 RID: 22869 RVA: 0x00167E08 File Offset: 0x00166E08
		private void ImportIPersonalizable(XmlReader reader, Control control)
		{
			if (control is IPersonalizable)
			{
				WebPartManager.ImportReadTo(reader, "ipersonalizable", "property");
				if (reader.Name == "ipersonalizable")
				{
					reader.ReadStartElement("ipersonalizable");
					this.ImportFromReader(null, control, reader);
				}
			}
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x00167E48 File Offset: 0x00166E48
		private void ImportFromReader(IDictionary personalizableProperties, Control target, XmlReader reader)
		{
			WebPartManager.ImportReadTo(reader, "property");
			this.MinimalPermissionSet.PermitOnly();
			bool flag = true;
			try
			{
				try
				{
					IDictionary dictionary;
					if (personalizableProperties != null)
					{
						dictionary = new HybridDictionary();
					}
					else
					{
						dictionary = new PersonalizationDictionary();
					}
					while (reader.Name == "property")
					{
						string attribute = reader.GetAttribute("name");
						string attribute2 = reader.GetAttribute("type");
						string attribute3 = reader.GetAttribute("scope");
						bool flag2 = string.Equals(reader.GetAttribute("null"), "true", StringComparison.OrdinalIgnoreCase);
						if (attribute == "AuthorizationFilter" || attribute == "ZoneID" || attribute == "ZoneIndex")
						{
							reader.ReadElementString();
							if (!reader.Read())
							{
								throw new XmlException();
							}
						}
						else
						{
							string text = reader.ReadElementString();
							object value = null;
							bool flag3 = false;
							PropertyInfo propertyInfo = null;
							if (personalizableProperties != null)
							{
								PersonalizablePropertyEntry personalizablePropertyEntry = (PersonalizablePropertyEntry)personalizableProperties[attribute];
								if (personalizablePropertyEntry != null)
								{
									propertyInfo = personalizablePropertyEntry.PropertyInfo;
									UrlPropertyAttribute urlPropertyAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(UrlPropertyAttribute), true) as UrlPropertyAttribute;
									if (urlPropertyAttribute != null && CrossSiteScriptingValidation.IsDangerousUrl(text))
									{
										throw new InvalidDataException(SR.GetString("WebPart_BadUrl", new object[]
										{
											text
										}));
									}
								}
							}
							Type type = null;
							if (!string.IsNullOrEmpty(attribute2))
							{
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
								this.MediumPermissionSet.PermitOnly();
								flag = true;
								type = WebPartManager.GetExportType(attribute2);
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
								this.MinimalPermissionSet.PermitOnly();
								flag = true;
							}
							if (propertyInfo != null && (propertyInfo.PropertyType == type || type == null))
							{
								TypeConverterAttribute typeConverterAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute), true) as TypeConverterAttribute;
								if (typeConverterAttribute != null)
								{
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									this.MediumPermissionSet.PermitOnly();
									flag = true;
									Type type2 = WebPartUtil.DeserializeType(typeConverterAttribute.ConverterTypeName, false);
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									this.MinimalPermissionSet.PermitOnly();
									flag = true;
									if (type2 != null && type2.IsSubclassOf(typeof(TypeConverter)))
									{
										TypeConverter typeConverter = (TypeConverter)this.Internals.CreateObjectFromType(type2);
										if (Util.CanConvertToFrom(typeConverter, typeof(string)))
										{
											if (!flag2)
											{
												value = typeConverter.ConvertFromInvariantString(text);
											}
											flag3 = true;
										}
									}
								}
								if (!flag3)
								{
									TypeConverter converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
									if (Util.CanConvertToFrom(converter, typeof(string)))
									{
										if (!flag2)
										{
											value = converter.ConvertFromInvariantString(text);
										}
										flag3 = true;
									}
								}
							}
							if (!flag3 && type != null)
							{
								if (type == typeof(string))
								{
									if (!flag2)
									{
										value = text;
									}
									flag3 = true;
								}
								else
								{
									TypeConverter converter2 = TypeDescriptor.GetConverter(type);
									if (Util.CanConvertToFrom(converter2, typeof(string)))
									{
										if (!flag2)
										{
											value = converter2.ConvertFromInvariantString(text);
										}
										flag3 = true;
									}
								}
							}
							if (flag2 && personalizableProperties == null)
							{
								flag3 = true;
							}
							if (!flag3)
							{
								throw new HttpException(SR.GetString("WebPartManager_ImportInvalidData", new object[]
								{
									attribute
								}));
							}
							if (personalizableProperties != null)
							{
								dictionary.Add(attribute, value);
							}
							else
							{
								PersonalizationScope scope = string.Equals(attribute3, PersonalizationScope.Shared.ToString(), StringComparison.OrdinalIgnoreCase) ? PersonalizationScope.Shared : PersonalizationScope.User;
								dictionary.Add(attribute, new PersonalizationEntry(value, scope));
							}
						}
						while (reader.Name != "property")
						{
							if (reader.EOF || reader.Name == "genericWebPartProperties" || reader.Name == "properties" || (reader.Name == "ipersonalizable" && reader.NodeType == XmlNodeType.EndElement))
							{
								goto IL_385;
							}
							reader.Skip();
						}
					}
					IL_385:
					if (personalizableProperties != null)
					{
						IDictionary dictionary2 = BlobPersonalizationState.SetPersonalizedProperties(target, dictionary);
						if (dictionary2 != null && dictionary2.Count > 0)
						{
							IVersioningPersonalizable versioningPersonalizable = target as IVersioningPersonalizable;
							if (versioningPersonalizable != null)
							{
								versioningPersonalizable.Load(dictionary2);
							}
						}
					}
					else
					{
						((IPersonalizable)target).Load((PersonalizationDictionary)dictionary);
					}
				}
				finally
				{
					if (flag)
					{
						CodeAccessPermission.RevertPermitOnly();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x00168260 File Offset: 0x00167260
		public virtual bool IsAuthorized(Type type, string path, string authorizationFilter, bool isShared)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type == typeof(UserControl))
			{
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentException(SR.GetString("WebPartManager_PathCannotBeEmpty"));
				}
			}
			else if (!string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_PathMustBeEmpty", new object[]
				{
					path
				}));
			}
			WebPartAuthorizationEventArgs webPartAuthorizationEventArgs = new WebPartAuthorizationEventArgs(type, path, authorizationFilter, isShared);
			this.OnAuthorizeWebPart(webPartAuthorizationEventArgs);
			return webPartAuthorizationEventArgs.IsAuthorized;
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x001682DC File Offset: 0x001672DC
		public bool IsAuthorized(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			string authorizationFilter = webPart.AuthorizationFilter;
			string id = webPart.ID;
			if (!string.IsNullOrEmpty(id) && this.Personalization.IsEnabled)
			{
				string authorizationFilter2 = this.Personalization.GetAuthorizationFilter(webPart.ID);
				if (authorizationFilter2 != null)
				{
					authorizationFilter = authorizationFilter2;
				}
			}
			GenericWebPart genericWebPart = webPart as GenericWebPart;
			if (genericWebPart != null)
			{
				string path = null;
				Control childControl = genericWebPart.ChildControl;
				UserControl userControl = childControl as UserControl;
				Type type;
				if (userControl != null)
				{
					type = typeof(UserControl);
					path = userControl.AppRelativeVirtualPath;
				}
				else
				{
					type = childControl.GetType();
				}
				return this.IsAuthorized(type, path, authorizationFilter, webPart.IsShared);
			}
			return this.IsAuthorized(webPart.GetType(), null, authorizationFilter, webPart.IsShared);
		}

		// Token: 0x06005959 RID: 22873 RVA: 0x0016839C File Offset: 0x0016739C
		internal bool IsConsumerConnected(WebPart consumer, ConsumerConnectionPoint connectionPoint)
		{
			return this.GetConnectionForConsumer(consumer, connectionPoint) != null;
		}

		// Token: 0x0600595A RID: 22874 RVA: 0x001683AC File Offset: 0x001673AC
		internal bool IsProviderConnected(WebPart provider, ProviderConnectionPoint connectionPoint)
		{
			return this.GetConnectionForProvider(provider, connectionPoint) != null;
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x001683BC File Offset: 0x001673BC
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadControlState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 3)
			{
				throw new ArgumentException(SR.GetString("Invalid_ControlState"));
			}
			base.LoadControlState(array[0]);
			if (array[1] != null)
			{
				WebPart webPart = this.WebParts[(string)array[1]];
				if (webPart == null || webPart.IsClosed)
				{
					this.SetSelectedWebPart(null);
					this.OnSelectedWebPartChanged(new WebPartEventArgs(null));
				}
				else
				{
					this.SetSelectedWebPart(webPart);
				}
			}
			if (array[2] != null)
			{
				string modeName = (string)array[2];
				WebPartDisplayMode webPartDisplayMode = this.SupportedDisplayModes[modeName];
				webPartDisplayMode.IsEnabled(this);
				if (webPartDisplayMode == null)
				{
					this._displayMode = WebPartManager.BrowseDisplayMode;
					this.OnDisplayModeChanged(new WebPartDisplayModeEventArgs(null));
					return;
				}
				this._displayMode = webPartDisplayMode;
			}
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x0016847F File Offset: 0x0016747F
		protected virtual void LoadCustomPersonalizationState(PersonalizationDictionary state)
		{
			this._personalizationState = state;
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x00168488 File Offset: 0x00167488
		private void LoadDynamicConnections(PersonalizationEntry entry)
		{
			if (entry != null)
			{
				object[] array = (object[])entry.Value;
				if (array != null)
				{
					for (int i = 0; i < array.Length; i += 7)
					{
						string id = (string)array[i];
						string consumerID = (string)array[i + 1];
						string consumerConnectionPointID = (string)array[i + 2];
						string providerID = (string)array[i + 3];
						string providerConnectionPointID = (string)array[i + 4];
						WebPartConnection webPartConnection = new WebPartConnection();
						webPartConnection.ID = id;
						webPartConnection.ConsumerID = consumerID;
						webPartConnection.ConsumerConnectionPointID = consumerConnectionPointID;
						webPartConnection.ProviderID = providerID;
						webPartConnection.ProviderConnectionPointID = providerConnectionPointID;
						this.Internals.SetIsShared(webPartConnection, entry.Scope == PersonalizationScope.Shared);
						this.Internals.SetIsStatic(webPartConnection, false);
						Type type = array[i + 5] as Type;
						if (type != null)
						{
							if (!type.IsSubclassOf(typeof(WebPartTransformer)))
							{
								throw new InvalidOperationException(SR.GetString("WebPartTransformerAttribute_NotTransformer", new object[]
								{
									type.Name
								}));
							}
							object savedState = array[i + 6];
							WebPartTransformer transformer = (WebPartTransformer)this.Internals.CreateObjectFromType(type);
							this.Internals.LoadConfigurationState(transformer, savedState);
							this.Internals.SetTransformer(webPartConnection, transformer);
						}
						this.DynamicConnections.Add(webPartConnection);
					}
				}
			}
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x001685E0 File Offset: 0x001675E0
		private void LoadDynamicWebPart(string id, string typeName, string path, string genericWebPartID, bool isShared)
		{
			WebPart webPart = null;
			Type type = WebPartUtil.DeserializeType(typeName, false);
			if (type == null)
			{
				string @string;
				if (this.Context != null && this.Context.IsCustomErrorEnabled)
				{
					@string = SR.GetString("WebPartManager_ErrorLoadingWebPartType");
				}
				else
				{
					@string = SR.GetString("Invalid_type", new object[]
					{
						typeName
					});
				}
				webPart = this.CreateErrorWebPart(id, typeName, path, genericWebPartID, @string);
			}
			else if (type.IsSubclassOf(typeof(WebPart)))
			{
				string authorizationFilter = this.Personalization.GetAuthorizationFilter(id);
				if (this.IsAuthorized(type, null, authorizationFilter, isShared))
				{
					try
					{
						webPart = (WebPart)this.Internals.CreateObjectFromType(type);
						webPart.ID = id;
						goto IL_291;
					}
					catch
					{
						string string2;
						if (this.Context != null && this.Context.IsCustomErrorEnabled)
						{
							string2 = SR.GetString("WebPartManager_CantCreateInstance");
						}
						else
						{
							string2 = SR.GetString("WebPartManager_CantCreateInstanceWithType", new object[]
							{
								typeName
							});
						}
						webPart = this.CreateErrorWebPart(id, typeName, path, genericWebPartID, string2);
						goto IL_291;
					}
				}
				webPart = new UnauthorizedWebPart(id, typeName, path, genericWebPartID);
			}
			else if (type.IsSubclassOf(typeof(Control)))
			{
				string authorizationFilter2 = this.Personalization.GetAuthorizationFilter(genericWebPartID);
				if (this.IsAuthorized(type, path, authorizationFilter2, isShared))
				{
					Control control = null;
					try
					{
						if (!string.IsNullOrEmpty(path))
						{
							control = this.Page.LoadControl(path);
						}
						else
						{
							control = (Control)this.Internals.CreateObjectFromType(type);
						}
						control.ID = id;
						webPart = this.CreateWebPart(control);
						webPart.ID = genericWebPartID;
						goto IL_291;
					}
					catch
					{
						string string3;
						if (control == null && string.IsNullOrEmpty(path))
						{
							if (this.Context != null && this.Context.IsCustomErrorEnabled)
							{
								string3 = SR.GetString("WebPartManager_CantCreateInstance");
							}
							else
							{
								string3 = SR.GetString("WebPartManager_CantCreateInstanceWithType", new object[]
								{
									typeName
								});
							}
						}
						else if (control == null)
						{
							if (this.Context != null && this.Context.IsCustomErrorEnabled)
							{
								string3 = SR.GetString("WebPartManager_InvalidPath");
							}
							else
							{
								string3 = SR.GetString("WebPartManager_InvalidPathWithPath", new object[]
								{
									path
								});
							}
						}
						else
						{
							string3 = SR.GetString("WebPartManager_CantCreateGeneric");
						}
						webPart = this.CreateErrorWebPart(id, typeName, path, genericWebPartID, string3);
						goto IL_291;
					}
				}
				webPart = new UnauthorizedWebPart(id, typeName, path, genericWebPartID);
			}
			else
			{
				string string4;
				if (this.Context != null && this.Context.IsCustomErrorEnabled)
				{
					string4 = SR.GetString("WebPartManager_TypeMustDeriveFromControl");
				}
				else
				{
					string4 = SR.GetString("WebPartManager_TypeMustDeriveFromControlWithType", new object[]
					{
						typeName
					});
				}
				webPart = this.CreateErrorWebPart(id, typeName, path, genericWebPartID, string4);
			}
			IL_291:
			this.Internals.SetIsStatic(webPart, false);
			this.Internals.SetIsShared(webPart, isShared);
			this.Internals.AddWebPart(webPart);
		}

		// Token: 0x0600595F RID: 22879 RVA: 0x001688C4 File Offset: 0x001678C4
		private void LoadDynamicWebParts(PersonalizationEntry entry)
		{
			if (entry != null)
			{
				object[] array = (object[])entry.Value;
				if (array != null)
				{
					bool isShared = entry.Scope == PersonalizationScope.Shared;
					for (int i = 0; i < array.Length; i += 4)
					{
						string id = (string)array[i];
						string typeName = (string)array[i + 1];
						string path = (string)array[i + 2];
						string genericWebPartID = (string)array[i + 3];
						this.LoadDynamicWebPart(id, typeName, path, genericWebPartID, isShared);
					}
				}
			}
		}

		// Token: 0x06005960 RID: 22880 RVA: 0x00168938 File Offset: 0x00167938
		private void LoadDeletedConnectionState(PersonalizationEntry entry)
		{
			if (entry != null)
			{
				string[] array = (string[])entry.Value;
				if (array != null)
				{
					foreach (string b in array)
					{
						WebPartConnection webPartConnection = null;
						foreach (object obj in this.StaticConnections)
						{
							WebPartConnection webPartConnection2 = (WebPartConnection)obj;
							if (string.Equals(webPartConnection2.ID, b, StringComparison.OrdinalIgnoreCase))
							{
								webPartConnection = webPartConnection2;
								break;
							}
						}
						if (webPartConnection == null)
						{
							foreach (object obj2 in this.DynamicConnections)
							{
								WebPartConnection webPartConnection3 = (WebPartConnection)obj2;
								if (string.Equals(webPartConnection3.ID, b, StringComparison.OrdinalIgnoreCase))
								{
									webPartConnection = webPartConnection3;
									break;
								}
							}
						}
						if (webPartConnection != null)
						{
							this.Internals.DeleteConnection(webPartConnection);
						}
						else
						{
							this._hasDataChanged = true;
						}
					}
				}
			}
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x00168A54 File Offset: 0x00167A54
		private void LoadWebPartState(PersonalizationEntry entry)
		{
			if (entry != null)
			{
				object[] array = (object[])entry.Value;
				if (array != null)
				{
					for (int i = 0; i < array.Length; i += 4)
					{
						string id = (string)array[i];
						string zoneID = (string)array[i + 1];
						int zoneIndex = (int)array[i + 2];
						bool isClosed = (bool)array[i + 3];
						WebPart webPart = (WebPart)this.FindControl(id);
						if (webPart != null)
						{
							this.Internals.SetZoneID(webPart, zoneID);
							this.Internals.SetZoneIndex(webPart, zoneIndex);
							this.Internals.SetIsClosed(webPart, isClosed);
						}
						else
						{
							this._hasDataChanged = true;
						}
					}
				}
			}
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x00168AFC File Offset: 0x00167AFC
		public virtual void MoveWebPart(WebPart webPart, WebPartZoneBase zone, int zoneIndex)
		{
			this.Personalization.EnsureEnabled(true);
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (!this.Controls.Contains(webPart))
			{
				throw new ArgumentException(SR.GetString("UnknownWebPart"), "webPart");
			}
			if (zone == null)
			{
				throw new ArgumentNullException("zone");
			}
			if (!this._webPartZones.Contains(zone))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_MustRegister"), "zone");
			}
			if (zoneIndex < 0)
			{
				throw new ArgumentOutOfRangeException("zoneIndex");
			}
			if (webPart.Zone == null || webPart.IsClosed)
			{
				throw new ArgumentException(SR.GetString("WebPartManager_MustBeInZone"), "webPart");
			}
			if (webPart.Zone == zone && webPart.ZoneIndex == zoneIndex)
			{
				return;
			}
			WebPartMovingEventArgs webPartMovingEventArgs = new WebPartMovingEventArgs(webPart, zone, zoneIndex);
			this.OnWebPartMoving(webPartMovingEventArgs);
			if (this._allowEventCancellation && webPartMovingEventArgs.Cancel)
			{
				return;
			}
			this.RemoveWebPartFromZone(webPart);
			this.AddWebPartToZone(webPart, zone, zoneIndex);
			this.OnWebPartMoved(new WebPartEventArgs(webPart));
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x00168BFC File Offset: 0x00167BFC
		protected virtual void OnAuthorizeWebPart(WebPartAuthorizationEventArgs e)
		{
			WebPartAuthorizationEventHandler webPartAuthorizationEventHandler = (WebPartAuthorizationEventHandler)base.Events[WebPartManager.AuthorizeWebPartEvent];
			if (webPartAuthorizationEventHandler != null)
			{
				webPartAuthorizationEventHandler(this, e);
			}
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x00168C2C File Offset: 0x00167C2C
		protected virtual void OnConnectionsActivated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[WebPartManager.ConnectionsActivatedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x00168C5C File Offset: 0x00167C5C
		protected virtual void OnConnectionsActivating(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[WebPartManager.ConnectionsActivatingEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x00168C8C File Offset: 0x00167C8C
		protected virtual void OnDisplayModeChanged(WebPartDisplayModeEventArgs e)
		{
			WebPartDisplayModeEventHandler webPartDisplayModeEventHandler = (WebPartDisplayModeEventHandler)base.Events[WebPartManager.DisplayModeChangedEvent];
			if (webPartDisplayModeEventHandler != null)
			{
				webPartDisplayModeEventHandler(this, e);
			}
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x00168CBC File Offset: 0x00167CBC
		protected virtual void OnDisplayModeChanging(WebPartDisplayModeCancelEventArgs e)
		{
			WebPartDisplayModeCancelEventHandler webPartDisplayModeCancelEventHandler = (WebPartDisplayModeCancelEventHandler)base.Events[WebPartManager.DisplayModeChangingEvent];
			if (webPartDisplayModeCancelEventHandler != null)
			{
				webPartDisplayModeCancelEventHandler(this, e);
			}
		}

		// Token: 0x06005968 RID: 22888 RVA: 0x00168CEC File Offset: 0x00167CEC
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode)
			{
				Page page = this.Page;
				if (page != null)
				{
					WebPartManager webPartManager = (WebPartManager)page.Items[typeof(WebPartManager)];
					if (webPartManager != null)
					{
						throw new InvalidOperationException(SR.GetString("WebPartManager_OnlyOneInstance"));
					}
					page.Items[typeof(WebPartManager)] = this;
					page.InitComplete += this.OnPageInitComplete;
					page.LoadComplete += this.OnPageLoadComplete;
					page.SaveStateComplete += this.OnPageSaveStateComplete;
					page.RegisterRequiresControlState(this);
					this.Personalization.LoadInternal();
				}
			}
		}

		// Token: 0x06005969 RID: 22889 RVA: 0x00168DA4 File Offset: 0x00167DA4
		protected internal override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			if (!base.DesignMode)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Items.Remove(typeof(WebPartManager));
				}
			}
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x00168DE0 File Offset: 0x00167DE0
		private void OnPageInitComplete(object sender, EventArgs e)
		{
			if (this._personalizationState != null)
			{
				this.LoadDynamicConnections(this._personalizationState["DynamicConnectionsShared"]);
				this.LoadDynamicConnections(this._personalizationState["DynamicConnectionsUser"]);
				this.LoadDeletedConnectionState(this._personalizationState["DeletedConnectionsShared"]);
				this.LoadDeletedConnectionState(this._personalizationState["DeletedConnectionsUser"]);
				this.LoadDynamicWebParts(this._personalizationState["DynamicWebPartsShared"]);
				this.LoadDynamicWebParts(this._personalizationState["DynamicWebPartsUser"]);
				this.LoadWebPartState(this._personalizationState["WebPartStateShared"]);
				this.LoadWebPartState(this._personalizationState["WebPartStateUser"]);
			}
			this._pageInitComplete = true;
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00168EAF File Offset: 0x00167EAF
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.CloseOrphanedParts();
			this._allowCreateDisplayTitles = true;
			this.OnConnectionsActivating(EventArgs.Empty);
			this.ActivateConnections();
			this.OnConnectionsActivated(EventArgs.Empty);
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x00168EDC File Offset: 0x00167EDC
		private void OnPageSaveStateComplete(object sender, EventArgs e)
		{
			this.Personalization.ExtractPersonalizationState();
			foreach (object obj in this.Controls)
			{
				WebPart webPart = (WebPart)obj;
				this.Personalization.ExtractPersonalizationState(webPart);
			}
			this.Personalization.SaveInternal();
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x00168F50 File Offset: 0x00167F50
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterStartupScript(this, typeof(WebPartManager), "ExportSensitiveDataWarningDeclaration", "var __wpmExportWarning='" + Util.QuoteJScriptString(this.ExportSensitiveDataWarning) + "';", true);
				this.Page.ClientScript.RegisterStartupScript(this, typeof(WebPartManager), "CloseProviderWarningDeclaration", "var __wpmCloseProviderWarning='" + Util.QuoteJScriptString(this.CloseProviderWarning) + "';", true);
				this.Page.ClientScript.RegisterStartupScript(this, typeof(WebPartManager), "DeleteWarningDeclaration", "var __wpmDeleteWarning='" + Util.QuoteJScriptString(this.DeleteWarning) + "';", true);
				this._renderClientScript = this.CheckRenderClientScript();
				if (this._renderClientScript)
				{
					this.Page.RegisterPostBackScript();
					this.RegisterClientScript();
				}
			}
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x00169048 File Offset: 0x00168048
		protected virtual void OnSelectedWebPartChanged(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.SelectedWebPartChangedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x00169078 File Offset: 0x00168078
		protected virtual void OnSelectedWebPartChanging(WebPartCancelEventArgs e)
		{
			WebPartCancelEventHandler webPartCancelEventHandler = (WebPartCancelEventHandler)base.Events[WebPartManager.SelectedWebPartChangingEvent];
			if (webPartCancelEventHandler != null)
			{
				webPartCancelEventHandler(this, e);
			}
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x001690A8 File Offset: 0x001680A8
		protected virtual void OnWebPartAdded(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartAddedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x001690D8 File Offset: 0x001680D8
		protected virtual void OnWebPartAdding(WebPartAddingEventArgs e)
		{
			WebPartAddingEventHandler webPartAddingEventHandler = (WebPartAddingEventHandler)base.Events[WebPartManager.WebPartAddingEvent];
			if (webPartAddingEventHandler != null)
			{
				webPartAddingEventHandler(this, e);
			}
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x00169108 File Offset: 0x00168108
		protected virtual void OnWebPartClosed(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartClosedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00169138 File Offset: 0x00168138
		protected virtual void OnWebPartClosing(WebPartCancelEventArgs e)
		{
			WebPartCancelEventHandler webPartCancelEventHandler = (WebPartCancelEventHandler)base.Events[WebPartManager.WebPartClosingEvent];
			if (webPartCancelEventHandler != null)
			{
				webPartCancelEventHandler(this, e);
			}
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x00169168 File Offset: 0x00168168
		protected virtual void OnWebPartDeleted(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartDeletedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x00169198 File Offset: 0x00168198
		protected virtual void OnWebPartDeleting(WebPartCancelEventArgs e)
		{
			WebPartCancelEventHandler webPartCancelEventHandler = (WebPartCancelEventHandler)base.Events[WebPartManager.WebPartDeletingEvent];
			if (webPartCancelEventHandler != null)
			{
				webPartCancelEventHandler(this, e);
			}
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x001691C8 File Offset: 0x001681C8
		protected virtual void OnWebPartMoved(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartMovedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x001691F8 File Offset: 0x001681F8
		protected virtual void OnWebPartMoving(WebPartMovingEventArgs e)
		{
			WebPartMovingEventHandler webPartMovingEventHandler = (WebPartMovingEventHandler)base.Events[WebPartManager.WebPartMovingEvent];
			if (webPartMovingEventHandler != null)
			{
				webPartMovingEventHandler(this, e);
			}
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x00169228 File Offset: 0x00168228
		protected virtual void OnWebPartsConnected(WebPartConnectionsEventArgs e)
		{
			WebPartConnectionsEventHandler webPartConnectionsEventHandler = (WebPartConnectionsEventHandler)base.Events[WebPartManager.WebPartsConnectedEvent];
			if (webPartConnectionsEventHandler != null)
			{
				webPartConnectionsEventHandler(this, e);
			}
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x00169258 File Offset: 0x00168258
		protected virtual void OnWebPartsConnecting(WebPartConnectionsCancelEventArgs e)
		{
			WebPartConnectionsCancelEventHandler webPartConnectionsCancelEventHandler = (WebPartConnectionsCancelEventHandler)base.Events[WebPartManager.WebPartsConnectingEvent];
			if (webPartConnectionsCancelEventHandler != null)
			{
				webPartConnectionsCancelEventHandler(this, e);
			}
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x00169288 File Offset: 0x00168288
		protected virtual void OnWebPartsDisconnected(WebPartConnectionsEventArgs e)
		{
			WebPartConnectionsEventHandler webPartConnectionsEventHandler = (WebPartConnectionsEventHandler)base.Events[WebPartManager.WebPartsDisconnectedEvent];
			if (webPartConnectionsEventHandler != null)
			{
				webPartConnectionsEventHandler(this, e);
			}
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x001692B8 File Offset: 0x001682B8
		protected virtual void OnWebPartsDisconnecting(WebPartConnectionsCancelEventArgs e)
		{
			WebPartConnectionsCancelEventHandler webPartConnectionsCancelEventHandler = (WebPartConnectionsCancelEventHandler)base.Events[WebPartManager.WebPartsDisconnectingEvent];
			if (webPartConnectionsCancelEventHandler != null)
			{
				webPartConnectionsCancelEventHandler(this, e);
			}
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x001692E8 File Offset: 0x001682E8
		protected virtual void RegisterClientScript()
		{
			this.Page.ClientScript.RegisterClientScriptResource(this, typeof(WebPartManager), "WebParts.js");
			bool allowPageDesign = this.DisplayMode.AllowPageDesign;
			string text = "null";
			if (allowPageDesign)
			{
				text = "document.getElementById('" + this.ClientID + "___Drag')";
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			foreach (object obj in this._webPartZones)
			{
				WebPartZoneBase webPartZoneBase = (WebPartZoneBase)obj;
				string text2 = (webPartZoneBase.LayoutOrientation == Orientation.Vertical) ? "true" : "false";
				string text3 = "false";
				string text4 = "black";
				if (allowPageDesign && webPartZoneBase.AllowLayoutChange)
				{
					text3 = "true";
					text4 = ColorTranslator.ToHtml(webPartZoneBase.DragHighlightColor);
				}
				stringBuilder.AppendFormat("\r\nzoneElement = document.getElementById('{0}');\r\nif (zoneElement != null) {{\r\n    zoneObject = __wpm.AddZone(zoneElement, '{1}', {2}, {3}, '{4}');", new object[]
				{
					webPartZoneBase.ClientID,
					webPartZoneBase.UniqueID,
					text2,
					text3,
					text4
				});
				WebPartCollection webPartsForZone = this.GetWebPartsForZone(webPartZoneBase);
				foreach (object obj2 in webPartsForZone)
				{
					WebPart webPart = (WebPart)obj2;
					string arg = "null";
					string arg2 = "false";
					if (allowPageDesign)
					{
						arg = "document.getElementById('" + webPart.TitleBarID + "')";
						if (webPart.AllowZoneChange)
						{
							arg2 = "true";
						}
					}
					stringBuilder.AppendFormat("\r\n    zoneObject.AddWebPart(document.getElementById('{0}'), {1}, {2});", webPart.WholePartID, arg, arg2);
				}
				stringBuilder.Append("\r\n}");
			}
			string script = string.Format(CultureInfo.InvariantCulture, "\r\n<script type=\"text/javascript\">\r\n\r\n__wpm = new WebPartManager();\r\n__wpm.overlayContainerElement = {0};\r\n__wpm.personalizationScopeShared = {1};\r\n\r\nvar zoneElement;\r\nvar zoneObject;\r\n{2}\r\n</script>\r\n", new object[]
			{
				text,
				(this.Personalization.Scope == PersonalizationScope.Shared) ? "true" : "false",
				stringBuilder.ToString()
			});
			this.Page.ClientScript.RegisterStartupScript(this, typeof(WebPartManager), string.Empty, script, false);
			IScriptManager scriptManager = this.Page.ScriptManager;
			if (scriptManager != null && scriptManager.SupportsPartialRendering)
			{
				scriptManager.RegisterDispose(this, "WebPartManager_Dispose();");
			}
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x00169574 File Offset: 0x00168574
		internal void RegisterZone(WebZone zone)
		{
			if (this._pageInitComplete)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_RegisterTooLate"));
			}
			string id = zone.ID;
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_NoZoneID"), "zone");
			}
			if (this._zoneIDs.Contains(id))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_DuplicateZoneID", new object[]
				{
					id
				}));
			}
			this._zoneIDs.Add(id, zone);
			WebPartZoneBase webPartZoneBase = zone as WebPartZoneBase;
			if (webPartZoneBase == null)
			{
				ToolZone toolZone = (ToolZone)zone;
				WebPartDisplayModeCollection displayModes = this.DisplayModes;
				WebPartDisplayModeCollection supportedDisplayModes = this.SupportedDisplayModes;
				foreach (object obj in toolZone.AssociatedDisplayModes)
				{
					WebPartDisplayMode value = (WebPartDisplayMode)obj;
					if (displayModes.Contains(value) && !supportedDisplayModes.Contains(value))
					{
						supportedDisplayModes.AddInternal(value);
					}
				}
				return;
			}
			if (this._webPartZones.Contains(webPartZoneBase))
			{
				throw new ArgumentException(SR.GetString("WebPartManager_AlreadyRegistered"), "zone");
			}
			this._webPartZones.Add(webPartZoneBase);
			WebPartCollection initialWebParts = webPartZoneBase.GetInitialWebParts();
			((WebPartManager.WebPartManagerControlCollection)this.Controls).AddWebPartsFromZone(webPartZoneBase, initialWebParts);
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x001696D0 File Offset: 0x001686D0
		private void RemoveWebPartFromDictionary(WebPart webPart)
		{
			if (this._partsForZone != null)
			{
				string zoneID = this.Internals.GetZoneID(webPart);
				if (!string.IsNullOrEmpty(zoneID))
				{
					SortedList sortedList = (SortedList)this._partsForZone[zoneID];
					if (sortedList != null)
					{
						sortedList.Remove(webPart);
					}
				}
			}
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x00169716 File Offset: 0x00168716
		internal void RemoveWebPart(WebPart webPart)
		{
			((WebPartManager.WebPartManagerControlCollection)this.Controls).RemoveWebPart(webPart);
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x0016972C File Offset: 0x0016872C
		private void RemoveWebPartFromZone(WebPart webPart)
		{
			WebPartZoneBase zone = webPart.Zone;
			this.Internals.SetIsClosed(webPart, true);
			this._hasDataChanged = true;
			this.RemoveWebPartFromDictionary(webPart);
			if (zone != null)
			{
				IList allWebPartsForZone = this.GetAllWebPartsForZone(zone);
				for (int i = 0; i < allWebPartsForZone.Count; i++)
				{
					WebPart webPart2 = (WebPart)allWebPartsForZone[i];
					this.Internals.SetZoneIndex(webPart2, i);
				}
			}
		}

		// Token: 0x06005981 RID: 22913 RVA: 0x00169794 File Offset: 0x00168794
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.DisplayMode.AllowPageDesign)
			{
				string value = string.Format(CultureInfo.InvariantCulture, "\r\n<div id=\"{0}___Drag\" style=\"display:none; position:absolute; z-index: 32000; filter:alpha(opacity=75)\"></div>", new object[]
				{
					this.ClientID
				});
				writer.WriteLine(value);
			}
		}

		// Token: 0x06005982 RID: 22914 RVA: 0x001697D8 File Offset: 0x001687D8
		protected internal override object SaveControlState()
		{
			object[] array = new object[3];
			array[0] = base.SaveControlState();
			if (this.SelectedWebPart != null)
			{
				array[1] = this.SelectedWebPart.ID;
			}
			if (this._displayMode != WebPartManager.BrowseDisplayMode)
			{
				array[2] = this._displayMode.Name;
			}
			for (int i = 0; i < 3; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06005983 RID: 22915 RVA: 0x0016983C File Offset: 0x0016883C
		protected virtual void SaveCustomPersonalizationState(PersonalizationDictionary state)
		{
			PersonalizationScope scope = this.Personalization.Scope;
			int count = this.Controls.Count;
			if (count > 0)
			{
				object[] array = new object[count * 4];
				for (int i = 0; i < count; i++)
				{
					WebPart webPart = (WebPart)this.Controls[i];
					array[4 * i] = webPart.ID;
					array[4 * i + 1] = this.Internals.GetZoneID(webPart);
					array[4 * i + 2] = webPart.ZoneIndex;
					array[4 * i + 3] = webPart.IsClosed;
				}
				if (scope == PersonalizationScope.Shared)
				{
					state["WebPartStateShared"] = new PersonalizationEntry(array, PersonalizationScope.Shared);
				}
				else
				{
					state["WebPartStateUser"] = new PersonalizationEntry(array, PersonalizationScope.User);
				}
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Controls)
			{
				WebPart webPart2 = (WebPart)obj;
				if (!webPart2.IsStatic && ((scope == PersonalizationScope.User && !webPart2.IsShared) || (scope == PersonalizationScope.Shared && webPart2.IsShared)))
				{
					arrayList.Add(webPart2);
				}
			}
			int count2 = arrayList.Count;
			if (count2 > 0)
			{
				object[] array2 = new object[count2 * 4];
				for (int j = 0; j < count2; j++)
				{
					WebPart webPart3 = (WebPart)arrayList[j];
					string text = null;
					string text2 = null;
					ProxyWebPart proxyWebPart = webPart3 as ProxyWebPart;
					string text3;
					string text4;
					if (proxyWebPart != null)
					{
						text3 = proxyWebPart.OriginalID;
						text4 = proxyWebPart.OriginalTypeName;
						text = proxyWebPart.OriginalPath;
						text2 = proxyWebPart.GenericWebPartID;
					}
					else
					{
						GenericWebPart genericWebPart = webPart3 as GenericWebPart;
						if (genericWebPart != null)
						{
							Control childControl = genericWebPart.ChildControl;
							UserControl userControl = childControl as UserControl;
							text3 = childControl.ID;
							if (userControl != null)
							{
								text4 = WebPartUtil.SerializeType(typeof(UserControl));
								text = userControl.AppRelativeVirtualPath;
							}
							else
							{
								text4 = WebPartUtil.SerializeType(childControl.GetType());
							}
							text2 = genericWebPart.ID;
						}
						else
						{
							text3 = webPart3.ID;
							text4 = WebPartUtil.SerializeType(webPart3.GetType());
						}
					}
					array2[4 * j] = text3;
					array2[4 * j + 1] = text4;
					if (!string.IsNullOrEmpty(text))
					{
						array2[4 * j + 2] = text;
					}
					if (!string.IsNullOrEmpty(text2))
					{
						array2[4 * j + 3] = text2;
					}
				}
				if (scope == PersonalizationScope.Shared)
				{
					state["DynamicWebPartsShared"] = new PersonalizationEntry(array2, PersonalizationScope.Shared);
				}
				else
				{
					state["DynamicWebPartsUser"] = new PersonalizationEntry(array2, PersonalizationScope.User);
				}
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (object obj2 in this.StaticConnections)
			{
				WebPartConnection webPartConnection = (WebPartConnection)obj2;
				if (this.Internals.ConnectionDeleted(webPartConnection))
				{
					arrayList2.Add(webPartConnection);
				}
			}
			foreach (object obj3 in this.DynamicConnections)
			{
				WebPartConnection webPartConnection2 = (WebPartConnection)obj3;
				if (this.Internals.ConnectionDeleted(webPartConnection2))
				{
					arrayList2.Add(webPartConnection2);
				}
			}
			int count3 = arrayList2.Count;
			if (arrayList2.Count > 0)
			{
				string[] array3 = new string[count3];
				for (int k = 0; k < count3; k++)
				{
					WebPartConnection webPartConnection3 = (WebPartConnection)arrayList2[k];
					array3[k] = webPartConnection3.ID;
				}
				if (scope == PersonalizationScope.Shared)
				{
					state["DeletedConnectionsShared"] = new PersonalizationEntry(array3, PersonalizationScope.Shared);
				}
				else
				{
					state["DeletedConnectionsUser"] = new PersonalizationEntry(array3, PersonalizationScope.User);
				}
			}
			ArrayList arrayList3 = new ArrayList();
			foreach (object obj4 in this.DynamicConnections)
			{
				WebPartConnection webPartConnection4 = (WebPartConnection)obj4;
				if ((scope == PersonalizationScope.User && !webPartConnection4.IsShared) || (scope == PersonalizationScope.Shared && webPartConnection4.IsShared))
				{
					arrayList3.Add(webPartConnection4);
				}
			}
			int count4 = arrayList3.Count;
			if (count4 > 0)
			{
				object[] array4 = new object[count4 * 7];
				for (int l = 0; l < count4; l++)
				{
					WebPartConnection webPartConnection5 = (WebPartConnection)arrayList3[l];
					WebPartTransformer transformer = webPartConnection5.Transformer;
					array4[7 * l] = webPartConnection5.ID;
					array4[7 * l + 1] = webPartConnection5.ConsumerID;
					array4[7 * l + 2] = webPartConnection5.ConsumerConnectionPointID;
					array4[7 * l + 3] = webPartConnection5.ProviderID;
					array4[7 * l + 4] = webPartConnection5.ProviderConnectionPointID;
					if (transformer != null)
					{
						array4[7 * l + 5] = transformer.GetType();
						array4[7 * l + 6] = this.Internals.SaveConfigurationState(transformer);
					}
				}
				if (scope == PersonalizationScope.Shared)
				{
					state["DynamicConnectionsShared"] = new PersonalizationEntry(array4, PersonalizationScope.Shared);
					return;
				}
				state["DynamicConnectionsUser"] = new PersonalizationEntry(array4, PersonalizationScope.User);
			}
		}

		// Token: 0x06005984 RID: 22916 RVA: 0x00169D80 File Offset: 0x00168D80
		protected void SetPersonalizationDirty()
		{
			this.Personalization.SetDirty();
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x00169D8D File Offset: 0x00168D8D
		private bool ShouldRenderWebPartInZone(WebPart part, WebPartZoneBase zone)
		{
			return !(part is UnauthorizedWebPart);
		}

		// Token: 0x06005986 RID: 22918 RVA: 0x00169D9A File Offset: 0x00168D9A
		protected void SetSelectedWebPart(WebPart webPart)
		{
			this._selectedWebPart = webPart;
		}

		// Token: 0x06005987 RID: 22919 RVA: 0x00169DA4 File Offset: 0x00168DA4
		private bool ShouldExportProperty(PropertyInfo propertyInfo, Type propertyValueType, object propertyValue, out string exportString)
		{
			string text = propertyValue as string;
			if (text != null)
			{
				exportString = text;
				return true;
			}
			TypeConverter typeConverter = null;
			if (propertyInfo != null)
			{
				TypeConverterAttribute typeConverterAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute), true) as TypeConverterAttribute;
				if (typeConverterAttribute != null)
				{
					Type type = WebPartUtil.DeserializeType(typeConverterAttribute.ConverterTypeName, false);
					if (type != null && type.IsSubclassOf(typeof(TypeConverter)))
					{
						TypeConverter typeConverter2 = (TypeConverter)this.Internals.CreateObjectFromType(type);
						if (Util.CanConvertToFrom(typeConverter2, typeof(string)))
						{
							typeConverter = typeConverter2;
						}
					}
				}
			}
			if (typeConverter == null)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(propertyValueType);
				if (Util.CanConvertToFrom(converter, typeof(string)))
				{
					typeConverter = converter;
				}
			}
			if (typeConverter == null)
			{
				exportString = null;
				return propertyInfo == null && propertyValue == null;
			}
			if (propertyValue != null)
			{
				exportString = typeConverter.ConvertToInvariantString(propertyValue);
				return true;
			}
			exportString = null;
			return true;
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x00169E71 File Offset: 0x00168E71
		private bool ShouldRemoveConnection(WebPartConnection connection)
		{
			return !connection.IsShared || this.Personalization.Scope != PersonalizationScope.User;
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x00169E8B File Offset: 0x00168E8B
		protected override void TrackViewState()
		{
			this.Personalization.ApplyPersonalizationState();
			base.TrackViewState();
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x00169EA0 File Offset: 0x00168EA0
		private void VerifyType(Control control)
		{
			if (control is UserControl)
			{
				return;
			}
			Type type = control.GetType();
			string text = WebPartUtil.SerializeType(type);
			Type type2 = WebPartUtil.DeserializeType(text, false);
			if (type2 != type)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_CantAddControlType", new object[]
				{
					text
				}));
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x00169EEC File Offset: 0x00168EEC
		bool IPersonalizable.IsDirty
		{
			get
			{
				return this.IsCustomPersonalizationStateDirty;
			}
		}

		// Token: 0x0600598C RID: 22924 RVA: 0x00169EF4 File Offset: 0x00168EF4
		void IPersonalizable.Load(PersonalizationDictionary state)
		{
			this.LoadCustomPersonalizationState(state);
		}

		// Token: 0x0600598D RID: 22925 RVA: 0x00169EFD File Offset: 0x00168EFD
		void IPersonalizable.Save(PersonalizationDictionary state)
		{
			this.SaveCustomPersonalizationState(state);
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x00169F08 File Offset: 0x00168F08
		// Note: this type is marked as 'beforefieldinit'.
		static WebPartManager()
		{
			WebPartManager.AuthorizeWebPartEvent = new object();
			WebPartManager.ConnectionsActivatedEvent = new object();
			WebPartManager.ConnectionsActivatingEvent = new object();
			WebPartManager.DisplayModeChangedEvent = new object();
			WebPartManager.DisplayModeChangingEvent = new object();
			WebPartManager.SelectedWebPartChangingEvent = new object();
			WebPartManager.SelectedWebPartChangedEvent = new object();
			WebPartManager.WebPartAddedEvent = new object();
			WebPartManager.WebPartAddingEvent = new object();
			WebPartManager.WebPartClosedEvent = new object();
			WebPartManager.WebPartClosingEvent = new object();
			WebPartManager.WebPartDeletedEvent = new object();
			WebPartManager.WebPartDeletingEvent = new object();
			WebPartManager.WebPartMovedEvent = new object();
			WebPartManager.WebPartMovingEvent = new object();
			WebPartManager.WebPartsConnectedEvent = new object();
			WebPartManager.WebPartsConnectingEvent = new object();
			WebPartManager.WebPartsDisconnectedEvent = new object();
			WebPartManager.WebPartsDisconnectingEvent = new object();
			WebPartManager.displayTitleSuffix = new string[]
			{
				" [0]",
				" [1]",
				" [2]",
				" [3]",
				" [4]",
				" [5]",
				" [6]",
				" [7]",
				" [8]",
				" [9]",
				" [10]",
				" [11]",
				" [12]",
				" [13]",
				" [14]",
				" [15]",
				" [16]",
				" [17]",
				" [18]",
				" [19]",
				" [20]"
			};
		}

		// Token: 0x04002FFB RID: 12283
		private const string DynamicConnectionIDPrefix = "c";

		// Token: 0x04002FFC RID: 12284
		private const string DynamicWebPartIDPrefix = "wp";

		// Token: 0x04002FFD RID: 12285
		private const int baseIndex = 0;

		// Token: 0x04002FFE RID: 12286
		private const int selectedWebPartIndex = 1;

		// Token: 0x04002FFF RID: 12287
		private const int displayModeIndex = 2;

		// Token: 0x04003000 RID: 12288
		private const int controlStateArrayLength = 3;

		// Token: 0x04003001 RID: 12289
		private const string DragOverlayElementHtmlTemplate = "\r\n<div id=\"{0}___Drag\" style=\"display:none; position:absolute; z-index: 32000; filter:alpha(opacity=75)\"></div>";

		// Token: 0x04003002 RID: 12290
		private const string ExportSensitiveDataWarningDeclaration = "ExportSensitiveDataWarningDeclaration";

		// Token: 0x04003003 RID: 12291
		private const string CloseProviderWarningDeclaration = "CloseProviderWarningDeclaration";

		// Token: 0x04003004 RID: 12292
		private const string DeleteWarningDeclaration = "DeleteWarningDeclaration";

		// Token: 0x04003005 RID: 12293
		private const string StartupScript = "\r\n<script type=\"text/javascript\">\r\n\r\n__wpm = new WebPartManager();\r\n__wpm.overlayContainerElement = {0};\r\n__wpm.personalizationScopeShared = {1};\r\n\r\nvar zoneElement;\r\nvar zoneObject;\r\n{2}\r\n</script>\r\n";

		// Token: 0x04003006 RID: 12294
		private const string ZoneScript = "\r\nzoneElement = document.getElementById('{0}');\r\nif (zoneElement != null) {{\r\n    zoneObject = __wpm.AddZone(zoneElement, '{1}', {2}, {3}, '{4}');";

		// Token: 0x04003007 RID: 12295
		private const string ZonePartScript = "\r\n    zoneObject.AddWebPart(document.getElementById('{0}'), {1}, {2});";

		// Token: 0x04003008 RID: 12296
		private const string ZoneEndScript = "\r\n}";

		// Token: 0x04003009 RID: 12297
		private const string AuthorizationFilterName = "AuthorizationFilter";

		// Token: 0x0400300A RID: 12298
		private const string ImportErrorMessageName = "ImportErrorMessage";

		// Token: 0x0400300B RID: 12299
		private const string ZoneIDName = "ZoneID";

		// Token: 0x0400300C RID: 12300
		private const string ZoneIndexName = "ZoneIndex";

		// Token: 0x0400300D RID: 12301
		internal const string ExportRootElement = "webParts";

		// Token: 0x0400300E RID: 12302
		internal const string ExportPartElement = "webPart";

		// Token: 0x0400300F RID: 12303
		internal const string ExportPartNamespaceAttribute = "xmlns";

		// Token: 0x04003010 RID: 12304
		internal const string ExportPartNamespaceValue = "http://schemas.microsoft.com/WebPart/v3";

		// Token: 0x04003011 RID: 12305
		internal const string ExportMetaDataElement = "metaData";

		// Token: 0x04003012 RID: 12306
		internal const string ExportTypeElement = "type";

		// Token: 0x04003013 RID: 12307
		internal const string ExportErrorMessageElement = "importErrorMessage";

		// Token: 0x04003014 RID: 12308
		internal const string ExportDataElement = "data";

		// Token: 0x04003015 RID: 12309
		internal const string ExportPropertiesElement = "properties";

		// Token: 0x04003016 RID: 12310
		internal const string ExportPropertyElement = "property";

		// Token: 0x04003017 RID: 12311
		internal const string ExportTypeNameAttribute = "name";

		// Token: 0x04003018 RID: 12312
		internal const string ExportUserControlSrcAttribute = "src";

		// Token: 0x04003019 RID: 12313
		internal const string ExportPropertyNameAttribute = "name";

		// Token: 0x0400301A RID: 12314
		internal const string ExportGenericPartPropertiesElement = "genericWebPartProperties";

		// Token: 0x0400301B RID: 12315
		internal const string ExportIPersonalizableElement = "ipersonalizable";

		// Token: 0x0400301C RID: 12316
		internal const string ExportPropertyTypeAttribute = "type";

		// Token: 0x0400301D RID: 12317
		internal const string ExportPropertyScopeAttribute = "scope";

		// Token: 0x0400301E RID: 12318
		internal const string ExportPropertyNullAttribute = "null";

		// Token: 0x0400301F RID: 12319
		private const string ExportTypeBool = "bool";

		// Token: 0x04003020 RID: 12320
		private const string ExportTypeInt = "int";

		// Token: 0x04003021 RID: 12321
		private const string ExportTypeChromeState = "chromestate";

		// Token: 0x04003022 RID: 12322
		private const string ExportTypeChromeType = "chrometype";

		// Token: 0x04003023 RID: 12323
		private const string ExportTypeColor = "color";

		// Token: 0x04003024 RID: 12324
		private const string ExportTypeDateTime = "datetime";

		// Token: 0x04003025 RID: 12325
		private const string ExportTypeDirection = "direction";

		// Token: 0x04003026 RID: 12326
		private const string ExportTypeDouble = "double";

		// Token: 0x04003027 RID: 12327
		private const string ExportTypeExportMode = "exportmode";

		// Token: 0x04003028 RID: 12328
		private const string ExportTypeFontSize = "fontsize";

		// Token: 0x04003029 RID: 12329
		private const string ExportTypeHelpMode = "helpmode";

		// Token: 0x0400302A RID: 12330
		private const string ExportTypeObject = "object";

		// Token: 0x0400302B RID: 12331
		private const string ExportTypeSingle = "single";

		// Token: 0x0400302C RID: 12332
		private const string ExportTypeString = "string";

		// Token: 0x0400302D RID: 12333
		private const string ExportTypeUnit = "unit";

		// Token: 0x0400302E RID: 12334
		public static readonly WebPartDisplayMode CatalogDisplayMode = new WebPartManager.CatalogWebPartDisplayMode();

		// Token: 0x0400302F RID: 12335
		public static readonly WebPartDisplayMode ConnectDisplayMode = new WebPartManager.ConnectWebPartDisplayMode();

		// Token: 0x04003030 RID: 12336
		public static readonly WebPartDisplayMode DesignDisplayMode = new WebPartManager.DesignWebPartDisplayMode();

		// Token: 0x04003031 RID: 12337
		public static readonly WebPartDisplayMode EditDisplayMode = new WebPartManager.EditWebPartDisplayMode();

		// Token: 0x04003032 RID: 12338
		public static readonly WebPartDisplayMode BrowseDisplayMode = new WebPartManager.BrowseWebPartDisplayMode();

		// Token: 0x04003033 RID: 12339
		private static Hashtable ConnectionPointsCache;

		// Token: 0x04003047 RID: 12359
		private PermissionSet _minimalPermissionSet;

		// Token: 0x04003048 RID: 12360
		private PermissionSet _mediumPermissionSet;

		// Token: 0x04003049 RID: 12361
		private WebPartPersonalization _personalization;

		// Token: 0x0400304A RID: 12362
		private WebPartDisplayMode _displayMode;

		// Token: 0x0400304B RID: 12363
		private WebPartDisplayModeCollection _displayModes;

		// Token: 0x0400304C RID: 12364
		private WebPartDisplayModeCollection _supportedDisplayModes;

		// Token: 0x0400304D RID: 12365
		private WebPartManagerInternals _internals;

		// Token: 0x0400304E RID: 12366
		private bool _allowCreateDisplayTitles;

		// Token: 0x0400304F RID: 12367
		private bool _pageInitComplete;

		// Token: 0x04003050 RID: 12368
		private bool _allowEventCancellation;

		// Token: 0x04003051 RID: 12369
		private PersonalizationDictionary _personalizationState;

		// Token: 0x04003052 RID: 12370
		private bool _hasDataChanged;

		// Token: 0x04003053 RID: 12371
		private WebPartConnectionCollection _staticConnections;

		// Token: 0x04003054 RID: 12372
		private WebPartConnectionCollection _dynamicConnections;

		// Token: 0x04003055 RID: 12373
		private WebPartZoneCollection _webPartZones;

		// Token: 0x04003056 RID: 12374
		private TransformerTypeCollection _availableTransformers;

		// Token: 0x04003057 RID: 12375
		private IDictionary _displayTitles;

		// Token: 0x04003058 RID: 12376
		private static string[] displayTitleSuffix;

		// Token: 0x04003059 RID: 12377
		private IDictionary _partsForZone;

		// Token: 0x0400305A RID: 12378
		private IDictionary _partAndChildControlIDs;

		// Token: 0x0400305B RID: 12379
		private IDictionary _zoneIDs;

		// Token: 0x0400305C RID: 12380
		private WebPart _selectedWebPart;

		// Token: 0x0400305D RID: 12381
		private bool _renderClientScript;

		// Token: 0x02000731 RID: 1841
		private sealed class WebPartManagerControlCollection : ControlCollection
		{
			// Token: 0x0600598F RID: 22927 RVA: 0x0016A0C7 File Offset: 0x001690C7
			public WebPartManagerControlCollection(WebPartManager owner) : base(owner)
			{
				this._manager = owner;
				base.SetCollectionReadOnly("WebPartManager_CannotModify");
			}

			// Token: 0x06005990 RID: 22928 RVA: 0x0016A0E4 File Offset: 0x001690E4
			internal void AddWebPart(WebPart webPart)
			{
				string collectionReadOnly = base.SetCollectionReadOnly(null);
				try
				{
					try
					{
						this.AddWebPartHelper(webPart);
					}
					finally
					{
						base.SetCollectionReadOnly(collectionReadOnly);
					}
				}
				catch
				{
					throw;
				}
			}

			// Token: 0x06005991 RID: 22929 RVA: 0x0016A12C File Offset: 0x0016912C
			private void AddWebPartHelper(WebPart webPart)
			{
				string id = webPart.ID;
				if (string.IsNullOrEmpty(id))
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_NoWebPartID"));
				}
				if (this._manager._partAndChildControlIDs.Contains(id))
				{
					throw new InvalidOperationException(SR.GetString("WebPartManager_DuplicateWebPartID", new object[]
					{
						id
					}));
				}
				this._manager._partAndChildControlIDs.Add(id, null);
				GenericWebPart genericWebPart = webPart as GenericWebPart;
				if (genericWebPart != null)
				{
					string id2 = genericWebPart.ChildControl.ID;
					if (string.IsNullOrEmpty(id2))
					{
						throw new InvalidOperationException(SR.GetString("WebPartManager_NoChildControlID"));
					}
					if (this._manager._partAndChildControlIDs.Contains(id2))
					{
						throw new InvalidOperationException(SR.GetString("WebPartManager_DuplicateWebPartID", new object[]
						{
							id2
						}));
					}
					this._manager._partAndChildControlIDs.Add(id2, null);
				}
				this._manager.Internals.SetIsStandalone(webPart, false);
				webPart.SetWebPartManager(this._manager);
				this.Add(webPart);
				this._manager._partsForZone = null;
			}

			// Token: 0x06005992 RID: 22930 RVA: 0x0016A23C File Offset: 0x0016923C
			internal void AddWebPartsFromZone(WebPartZoneBase zone, WebPartCollection webParts)
			{
				if (webParts != null && webParts.Count != 0)
				{
					string collectionReadOnly = base.SetCollectionReadOnly(null);
					try
					{
						try
						{
							string id = zone.ID;
							int num = 0;
							foreach (object obj in webParts)
							{
								WebPart webPart = (WebPart)obj;
								this._manager.Internals.SetIsShared(webPart, true);
								WebPart webPart2 = webPart;
								if (!this._manager.IsAuthorized(webPart))
								{
									webPart2 = new UnauthorizedWebPart(webPart);
								}
								this._manager.Internals.SetIsStatic(webPart2, true);
								this._manager.Internals.SetIsShared(webPart2, true);
								this._manager.Internals.SetZoneID(webPart2, id);
								this._manager.Internals.SetZoneIndex(webPart2, num);
								this.AddWebPartHelper(webPart2);
								num++;
							}
						}
						finally
						{
							base.SetCollectionReadOnly(collectionReadOnly);
						}
					}
					catch
					{
						throw;
					}
				}
			}

			// Token: 0x06005993 RID: 22931 RVA: 0x0016A364 File Offset: 0x00169364
			internal void RemoveWebPart(WebPart webPart)
			{
				string collectionReadOnly = base.SetCollectionReadOnly(null);
				try
				{
					try
					{
						this._manager._partAndChildControlIDs.Remove(webPart.ID);
						GenericWebPart genericWebPart = webPart as GenericWebPart;
						if (genericWebPart != null)
						{
							this._manager._partAndChildControlIDs.Remove(genericWebPart.ChildControl.ID);
						}
						this.Remove(webPart);
						this._manager._hasDataChanged = true;
						webPart.SetWebPartManager(null);
						this._manager.Internals.SetIsStandalone(webPart, true);
						this._manager._partsForZone = null;
					}
					finally
					{
						base.SetCollectionReadOnly(collectionReadOnly);
					}
				}
				catch
				{
					throw;
				}
			}

			// Token: 0x0400305E RID: 12382
			private WebPartManager _manager;
		}

		// Token: 0x02000732 RID: 1842
		private sealed class BrowseWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x06005994 RID: 22932 RVA: 0x0016A418 File Offset: 0x00169418
			public BrowseWebPartDisplayMode() : base("Browse")
			{
			}
		}

		// Token: 0x02000733 RID: 1843
		private sealed class CatalogWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x06005995 RID: 22933 RVA: 0x0016A425 File Offset: 0x00169425
			public CatalogWebPartDisplayMode() : base("Catalog")
			{
			}

			// Token: 0x17001722 RID: 5922
			// (get) Token: 0x06005996 RID: 22934 RVA: 0x0016A432 File Offset: 0x00169432
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001723 RID: 5923
			// (get) Token: 0x06005997 RID: 22935 RVA: 0x0016A435 File Offset: 0x00169435
			public override bool AssociatedWithToolZone
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001724 RID: 5924
			// (get) Token: 0x06005998 RID: 22936 RVA: 0x0016A438 File Offset: 0x00169438
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001725 RID: 5925
			// (get) Token: 0x06005999 RID: 22937 RVA: 0x0016A43B File Offset: 0x0016943B
			public override bool ShowHiddenWebParts
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000734 RID: 1844
		private sealed class ConnectionPointKey
		{
			// Token: 0x0600599A RID: 22938 RVA: 0x0016A43E File Offset: 0x0016943E
			public ConnectionPointKey(Type type, CultureInfo culture, CultureInfo uiCulture)
			{
				this._type = type;
				this._culture = culture;
				this._uiCulture = uiCulture;
			}

			// Token: 0x0600599B RID: 22939 RVA: 0x0016A45C File Offset: 0x0016945C
			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				WebPartManager.ConnectionPointKey connectionPointKey = obj as WebPartManager.ConnectionPointKey;
				return connectionPointKey != null && connectionPointKey._type.Equals(this._type) && connectionPointKey._culture.Equals(this._culture) && connectionPointKey._uiCulture.Equals(this._uiCulture);
			}

			// Token: 0x0600599C RID: 22940 RVA: 0x0016A4B4 File Offset: 0x001694B4
			public override int GetHashCode()
			{
				int hashCode = this._type.GetHashCode();
				int num = (hashCode << 5) + hashCode ^ this._culture.GetHashCode();
				return (num << 5) + num ^ this._uiCulture.GetHashCode();
			}

			// Token: 0x0400305F RID: 12383
			private Type _type;

			// Token: 0x04003060 RID: 12384
			private CultureInfo _culture;

			// Token: 0x04003061 RID: 12385
			private CultureInfo _uiCulture;
		}

		// Token: 0x02000735 RID: 1845
		private sealed class ConnectWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x0600599D RID: 22941 RVA: 0x0016A4F0 File Offset: 0x001694F0
			public ConnectWebPartDisplayMode() : base("Connect")
			{
			}

			// Token: 0x17001726 RID: 5926
			// (get) Token: 0x0600599E RID: 22942 RVA: 0x0016A4FD File Offset: 0x001694FD
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001727 RID: 5927
			// (get) Token: 0x0600599F RID: 22943 RVA: 0x0016A500 File Offset: 0x00169500
			public override bool AssociatedWithToolZone
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001728 RID: 5928
			// (get) Token: 0x060059A0 RID: 22944 RVA: 0x0016A503 File Offset: 0x00169503
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001729 RID: 5929
			// (get) Token: 0x060059A1 RID: 22945 RVA: 0x0016A506 File Offset: 0x00169506
			public override bool ShowHiddenWebParts
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000736 RID: 1846
		private sealed class DesignWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x060059A2 RID: 22946 RVA: 0x0016A509 File Offset: 0x00169509
			public DesignWebPartDisplayMode() : base("Design")
			{
			}

			// Token: 0x1700172A RID: 5930
			// (get) Token: 0x060059A3 RID: 22947 RVA: 0x0016A516 File Offset: 0x00169516
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700172B RID: 5931
			// (get) Token: 0x060059A4 RID: 22948 RVA: 0x0016A519 File Offset: 0x00169519
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700172C RID: 5932
			// (get) Token: 0x060059A5 RID: 22949 RVA: 0x0016A51C File Offset: 0x0016951C
			public override bool ShowHiddenWebParts
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x02000737 RID: 1847
		private sealed class EditWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x060059A6 RID: 22950 RVA: 0x0016A51F File Offset: 0x0016951F
			public EditWebPartDisplayMode() : base("Edit")
			{
			}

			// Token: 0x1700172D RID: 5933
			// (get) Token: 0x060059A7 RID: 22951 RVA: 0x0016A52C File Offset: 0x0016952C
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700172E RID: 5934
			// (get) Token: 0x060059A8 RID: 22952 RVA: 0x0016A52F File Offset: 0x0016952F
			public override bool AssociatedWithToolZone
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700172F RID: 5935
			// (get) Token: 0x060059A9 RID: 22953 RVA: 0x0016A532 File Offset: 0x00169532
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001730 RID: 5936
			// (get) Token: 0x060059AA RID: 22954 RVA: 0x0016A535 File Offset: 0x00169535
			public override bool ShowHiddenWebParts
			{
				get
				{
					return true;
				}
			}
		}
	}
}
