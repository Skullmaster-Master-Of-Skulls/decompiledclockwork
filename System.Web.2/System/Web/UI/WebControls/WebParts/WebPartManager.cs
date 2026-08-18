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
	// Token: 0x020005A4 RID: 1444
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartManagerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[NonVisualControl]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ViewStateModeById]
	public class WebPartManager : Control, INamingContainer, IPersonalizable
	{
		// Token: 0x06004858 RID: 18520 RVA: 0x000ED2BE File Offset: 0x000EB4BE
		public WebPartManager()
		{
			this._allowEventCancellation = true;
			this._displayMode = WebPartManager.BrowseDisplayMode;
			this._webPartZones = new WebPartZoneCollection();
			this._partAndChildControlIDs = new HybridDictionary(true);
			this._zoneIDs = new HybridDictionary(true);
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06004859 RID: 18521 RVA: 0x000ED2FB File Offset: 0x000EB4FB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x000ED318 File Offset: 0x000EB518
		// (set) Token: 0x0600485B RID: 18523 RVA: 0x000ED34A File Offset: 0x000EB54A
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

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x0600485C RID: 18524 RVA: 0x000ED360 File Offset: 0x000EB560
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

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x0600485D RID: 18525 RVA: 0x000610DF File Offset: 0x0005F2DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x0600485E RID: 18526 RVA: 0x000ED44C File Offset: 0x000EB64C
		// (set) Token: 0x0600485F RID: 18527 RVA: 0x000ED47E File Offset: 0x000EB67E
		[WebCategory("Behavior")]
		[WebSysDefaultValue("WebPartManager_DefaultDeleteWarning")]
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

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06004860 RID: 18528 RVA: 0x000ED491 File Offset: 0x000EB691
		// (set) Token: 0x06004861 RID: 18529 RVA: 0x000ED49C File Offset: 0x000EB69C
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

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x000ED583 File Offset: 0x000EB783
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06004863 RID: 18531 RVA: 0x000ED5AF File Offset: 0x000EB7AF
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

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06004864 RID: 18532 RVA: 0x000ED5CC File Offset: 0x000EB7CC
		// (set) Token: 0x06004865 RID: 18533 RVA: 0x00085F35 File Offset: 0x00084135
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebPartManager_EnableClientScript")]
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

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x000097B7 File Offset: 0x000079B7
		// (set) Token: 0x06004867 RID: 18535 RVA: 0x000ED5F5 File Offset: 0x000EB7F5
		[Browsable(false)]
		[DefaultValue(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x000ED608 File Offset: 0x000EB808
		// (set) Token: 0x06004869 RID: 18537 RVA: 0x000ED63A File Offset: 0x000EB83A
		[WebCategory("Behavior")]
		[WebSysDefaultValue("WebPartChrome_ConfirmExportSensitive")]
		[WebSysDescription("WebPartManager_ExportSensitiveDataWarning")]
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

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x000ED64D File Offset: 0x000EB84D
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

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x0600486B RID: 18539 RVA: 0x000ED669 File Offset: 0x000EB869
		protected virtual bool IsCustomPersonalizationStateDirty
		{
			get
			{
				return this._hasDataChanged;
			}
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x0600486C RID: 18540 RVA: 0x000ED674 File Offset: 0x000EB874
		protected virtual PermissionSet MediumPermissionSet
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

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x0600486D RID: 18541 RVA: 0x000ED6C4 File Offset: 0x000EB8C4
		protected virtual PermissionSet MinimalPermissionSet
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

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x000ED713 File Offset: 0x000EB913
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebPartManager_Personalization")]
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

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x0600486F RID: 18543 RVA: 0x000ED72F File Offset: 0x000EB92F
		internal bool RenderClientScript
		{
			get
			{
				return this._renderClientScript;
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x000ED737 File Offset: 0x000EB937
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPart SelectedWebPart
		{
			get
			{
				return this._selectedWebPart;
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06004871 RID: 18545 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x06004872 RID: 18546 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06004873 RID: 18547 RVA: 0x000ED73F File Offset: 0x000EB93F
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebPartManager_StaticConnections")]
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

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x000ED75C File Offset: 0x000EB95C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x000ED7EC File Offset: 0x000EB9EC
		private bool UsePermitOnly
		{
			get
			{
				if (this._usePermitOnly == null)
				{
					this._usePermitOnly = new bool?(RuntimeConfig.GetAppConfig().Trust.LegacyCasModel);
				}
				return this._usePermitOnly.Value;
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x000097B7 File Offset: 0x000079B7
		// (set) Token: 0x06004877 RID: 18551 RVA: 0x0006110C File Offset: 0x0005F30C
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x000ED820 File Offset: 0x000EBA20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06004879 RID: 18553 RVA: 0x000ED83B File Offset: 0x000EBA3B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartZoneCollection Zones
		{
			get
			{
				return this._webPartZones;
			}
		}

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x0600487A RID: 18554 RVA: 0x000ED843 File Offset: 0x000EBA43
		// (remove) Token: 0x0600487B RID: 18555 RVA: 0x000ED856 File Offset: 0x000EBA56
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_AuthorizeWebPart")]
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

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x0600487C RID: 18556 RVA: 0x000ED869 File Offset: 0x000EBA69
		// (remove) Token: 0x0600487D RID: 18557 RVA: 0x000ED87C File Offset: 0x000EBA7C
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

		// Token: 0x14000114 RID: 276
		// (add) Token: 0x0600487E RID: 18558 RVA: 0x000ED88F File Offset: 0x000EBA8F
		// (remove) Token: 0x0600487F RID: 18559 RVA: 0x000ED8A2 File Offset: 0x000EBAA2
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_ConnectionsActivating")]
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

		// Token: 0x14000115 RID: 277
		// (add) Token: 0x06004880 RID: 18560 RVA: 0x000ED8B5 File Offset: 0x000EBAB5
		// (remove) Token: 0x06004881 RID: 18561 RVA: 0x000ED8C8 File Offset: 0x000EBAC8
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

		// Token: 0x14000116 RID: 278
		// (add) Token: 0x06004882 RID: 18562 RVA: 0x000ED8DB File Offset: 0x000EBADB
		// (remove) Token: 0x06004883 RID: 18563 RVA: 0x000ED8EE File Offset: 0x000EBAEE
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_DisplayModeChanging")]
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

		// Token: 0x14000117 RID: 279
		// (add) Token: 0x06004884 RID: 18564 RVA: 0x000ED901 File Offset: 0x000EBB01
		// (remove) Token: 0x06004885 RID: 18565 RVA: 0x000ED914 File Offset: 0x000EBB14
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_SelectedWebPartChanged")]
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

		// Token: 0x14000118 RID: 280
		// (add) Token: 0x06004886 RID: 18566 RVA: 0x000ED927 File Offset: 0x000EBB27
		// (remove) Token: 0x06004887 RID: 18567 RVA: 0x000ED93A File Offset: 0x000EBB3A
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_SelectedWebPartChanging")]
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

		// Token: 0x14000119 RID: 281
		// (add) Token: 0x06004888 RID: 18568 RVA: 0x000ED94D File Offset: 0x000EBB4D
		// (remove) Token: 0x06004889 RID: 18569 RVA: 0x000ED960 File Offset: 0x000EBB60
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

		// Token: 0x1400011A RID: 282
		// (add) Token: 0x0600488A RID: 18570 RVA: 0x000ED973 File Offset: 0x000EBB73
		// (remove) Token: 0x0600488B RID: 18571 RVA: 0x000ED986 File Offset: 0x000EBB86
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartAdding")]
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

		// Token: 0x1400011B RID: 283
		// (add) Token: 0x0600488C RID: 18572 RVA: 0x000ED999 File Offset: 0x000EBB99
		// (remove) Token: 0x0600488D RID: 18573 RVA: 0x000ED9AC File Offset: 0x000EBBAC
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

		// Token: 0x1400011C RID: 284
		// (add) Token: 0x0600488E RID: 18574 RVA: 0x000ED9BF File Offset: 0x000EBBBF
		// (remove) Token: 0x0600488F RID: 18575 RVA: 0x000ED9D2 File Offset: 0x000EBBD2
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

		// Token: 0x1400011D RID: 285
		// (add) Token: 0x06004890 RID: 18576 RVA: 0x000ED9E5 File Offset: 0x000EBBE5
		// (remove) Token: 0x06004891 RID: 18577 RVA: 0x000ED9F8 File Offset: 0x000EBBF8
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartDeleted")]
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

		// Token: 0x1400011E RID: 286
		// (add) Token: 0x06004892 RID: 18578 RVA: 0x000EDA0B File Offset: 0x000EBC0B
		// (remove) Token: 0x06004893 RID: 18579 RVA: 0x000EDA1E File Offset: 0x000EBC1E
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

		// Token: 0x1400011F RID: 287
		// (add) Token: 0x06004894 RID: 18580 RVA: 0x000EDA31 File Offset: 0x000EBC31
		// (remove) Token: 0x06004895 RID: 18581 RVA: 0x000EDA44 File Offset: 0x000EBC44
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartMoved")]
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

		// Token: 0x14000120 RID: 288
		// (add) Token: 0x06004896 RID: 18582 RVA: 0x000EDA57 File Offset: 0x000EBC57
		// (remove) Token: 0x06004897 RID: 18583 RVA: 0x000EDA6A File Offset: 0x000EBC6A
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

		// Token: 0x14000121 RID: 289
		// (add) Token: 0x06004898 RID: 18584 RVA: 0x000EDA7D File Offset: 0x000EBC7D
		// (remove) Token: 0x06004899 RID: 18585 RVA: 0x000EDA90 File Offset: 0x000EBC90
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

		// Token: 0x14000122 RID: 290
		// (add) Token: 0x0600489A RID: 18586 RVA: 0x000EDAA3 File Offset: 0x000EBCA3
		// (remove) Token: 0x0600489B RID: 18587 RVA: 0x000EDAB6 File Offset: 0x000EBCB6
		[WebCategory("Action")]
		[WebSysDescription("WebPartManager_WebPartsConnecting")]
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

		// Token: 0x14000123 RID: 291
		// (add) Token: 0x0600489C RID: 18588 RVA: 0x000EDAC9 File Offset: 0x000EBCC9
		// (remove) Token: 0x0600489D RID: 18589 RVA: 0x000EDADC File Offset: 0x000EBCDC
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

		// Token: 0x14000124 RID: 292
		// (add) Token: 0x0600489E RID: 18590 RVA: 0x000EDAEF File Offset: 0x000EBCEF
		// (remove) Token: 0x0600489F RID: 18591 RVA: 0x000EDB02 File Offset: 0x000EBD02
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

		// Token: 0x060048A0 RID: 18592 RVA: 0x000EDB18 File Offset: 0x000EBD18
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

		// Token: 0x060048A1 RID: 18593 RVA: 0x000EDB68 File Offset: 0x000EBD68
		internal void AddWebPart(WebPart webPart)
		{
			((WebPartManager.WebPartManagerControlCollection)this.Controls).AddWebPart(webPart);
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x000EDB7C File Offset: 0x000EBD7C
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

		// Token: 0x060048A3 RID: 18595 RVA: 0x000EDBF4 File Offset: 0x000EBDF4
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

		// Token: 0x060048A4 RID: 18596 RVA: 0x000EDCD4 File Offset: 0x000EBED4
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

		// Token: 0x060048A5 RID: 18597 RVA: 0x000EDD34 File Offset: 0x000EBF34
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

		// Token: 0x060048A6 RID: 18598 RVA: 0x000EDE10 File Offset: 0x000EC010
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

		// Token: 0x060048A7 RID: 18599 RVA: 0x000EDF08 File Offset: 0x000EC108
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

		// Token: 0x060048A8 RID: 18600 RVA: 0x000EE000 File Offset: 0x000EC200
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

		// Token: 0x060048A9 RID: 18601 RVA: 0x000EE050 File Offset: 0x000EC250
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

		// Token: 0x060048AA RID: 18602 RVA: 0x000EE0D4 File Offset: 0x000EC2D4
		public bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			return this.CanConnectWebParts(provider, providerConnectionPoint, consumer, consumerConnectionPoint, null);
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x000EE0E2 File Offset: 0x000EC2E2
		public virtual bool CanConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartTransformer transformer)
		{
			return this.CanConnectWebPartsCore(provider, providerConnectionPoint, consumer, consumerConnectionPoint, transformer, false);
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x000EE0F4 File Offset: 0x000EC2F4
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
								string name = "WebPartConnection_NoCommonInterface";
								object[] args = new string[]
								{
									providerConnectionPoint.DisplayName,
									provider.ID,
									consumerConnectionPoint.DisplayName,
									consumer.ID
								};
								throw new InvalidOperationException(SR.GetString(name, args));
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
									string name2 = "WebPartConnection_IncompatibleSecondaryInterfaces";
									object[] args = new string[]
									{
										consumerConnectionPoint.DisplayName,
										consumer.ID,
										providerConnectionPoint.DisplayName,
										provider.ID
									};
									throw new InvalidOperationException(SR.GetString(name2, args));
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

		// Token: 0x060048AD RID: 18605 RVA: 0x000EE5F4 File Offset: 0x000EC7F4
		public void CloseWebPart(WebPart webPart)
		{
			this.CloseOrDeleteWebPart(webPart, false);
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x000EE600 File Offset: 0x000EC800
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

		// Token: 0x060048AF RID: 18607 RVA: 0x000EE784 File Offset: 0x000EC984
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

		// Token: 0x060048B0 RID: 18608 RVA: 0x000EEBEC File Offset: 0x000ECDEC
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

		// Token: 0x060048B1 RID: 18609 RVA: 0x000EED65 File Offset: 0x000ECF65
		public WebPartConnection ConnectWebParts(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint)
		{
			return this.ConnectWebParts(provider, providerConnectionPoint, consumer, consumerConnectionPoint, null);
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x000EED74 File Offset: 0x000ECF74
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

		// Token: 0x060048B3 RID: 18611 RVA: 0x000EEE84 File Offset: 0x000ED084
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

		// Token: 0x060048B4 RID: 18612 RVA: 0x000EEF10 File Offset: 0x000ED110
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

		// Token: 0x060048B5 RID: 18613 RVA: 0x000EEF90 File Offset: 0x000ED190
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
					if (!methodInfo.IsPublic || !(methodInfo.ReturnType == typeof(void)) || !(type2 != null))
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
					if (!methodInfo.IsPublic || !(returnType != typeof(void)) || methodInfo.GetParameters().Length != 0)
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

		// Token: 0x060048B6 RID: 18614 RVA: 0x000EF226 File Offset: 0x000ED426
		protected sealed override ControlCollection CreateControlCollection()
		{
			return new WebPartManager.WebPartManagerControlCollection(this);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x000EF230 File Offset: 0x000ED430
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

		// Token: 0x060048B8 RID: 18616 RVA: 0x000EF280 File Offset: 0x000ED480
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

		// Token: 0x060048B9 RID: 18617 RVA: 0x000EF304 File Offset: 0x000ED504
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

		// Token: 0x060048BA RID: 18618 RVA: 0x000EF424 File Offset: 0x000ED624
		protected virtual string CreateDynamicConnectionID()
		{
			return "c" + Math.Abs(Guid.NewGuid().GetHashCode()).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x000EF460 File Offset: 0x000ED660
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

		// Token: 0x060048BC RID: 18620 RVA: 0x000EF4DC File Offset: 0x000ED6DC
		protected virtual ErrorWebPart CreateErrorWebPart(string originalID, string originalTypeName, string originalPath, string genericWebPartID, string errorMessage)
		{
			return new ErrorWebPart(originalID, originalTypeName, originalPath, genericWebPartID)
			{
				ErrorMessage = errorMessage
			};
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x000EF4FD File Offset: 0x000ED6FD
		protected virtual WebPartPersonalization CreatePersonalization()
		{
			return new WebPartPersonalization(this);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x000EF505 File Offset: 0x000ED705
		public virtual GenericWebPart CreateWebPart(Control control)
		{
			return WebPartManager.CreateWebPartStatic(control);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x000EF510 File Offset: 0x000ED710
		internal static GenericWebPart CreateWebPartStatic(Control control)
		{
			GenericWebPart genericWebPart = new GenericWebPart(control);
			genericWebPart.CreateChildControls();
			return genericWebPart;
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x000EF52B File Offset: 0x000ED72B
		public void DeleteWebPart(WebPart webPart)
		{
			this.CloseOrDeleteWebPart(webPart, true);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x000EF538 File Offset: 0x000ED738
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

		// Token: 0x060048C2 RID: 18626 RVA: 0x000EF5C0 File Offset: 0x000ED7C0
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

		// Token: 0x060048C3 RID: 18627 RVA: 0x000EF72C File Offset: 0x000ED92C
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

		// Token: 0x060048C4 RID: 18628 RVA: 0x000EF7A0 File Offset: 0x000ED9A0
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

		// Token: 0x060048C5 RID: 18629 RVA: 0x000EF814 File Offset: 0x000EDA14
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

		// Token: 0x060048C6 RID: 18630 RVA: 0x000EF9C4 File Offset: 0x000EDBC4
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

		// Token: 0x060048C7 RID: 18631 RVA: 0x000EFA0C File Offset: 0x000EDC0C
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

		// Token: 0x060048C8 RID: 18632 RVA: 0x000EFA7F File Offset: 0x000EDC7F
		private void ExportToWriter(IDictionary propBag, XmlWriter writer)
		{
			this.ExportToWriter(propBag, writer, false, false);
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x000EFA8C File Offset: 0x000EDC8C
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

		// Token: 0x060048CA RID: 18634 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x000EFBE4 File Offset: 0x000EDDE4
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

		// Token: 0x060048CC RID: 18636 RVA: 0x000EFCD0 File Offset: 0x000EDED0
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

		// Token: 0x060048CD RID: 18637 RVA: 0x000EFD2C File Offset: 0x000EDF2C
		internal ConsumerConnectionPoint GetConsumerConnectionPoint(WebPart webPart, string connectionPointID)
		{
			ConsumerConnectionPointCollection consumerConnectionPoints = this.GetConsumerConnectionPoints(webPart);
			if (consumerConnectionPoints != null && consumerConnectionPoints.Count > 0)
			{
				return consumerConnectionPoints[connectionPointID];
			}
			return null;
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x000EFD56 File Offset: 0x000EDF56
		public virtual ConsumerConnectionPointCollection GetConsumerConnectionPoints(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			return WebPartManager.GetConsumerConnectionPoints(webPart.ToControl().GetType());
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x000EFD78 File Offset: 0x000EDF78
		private static ConsumerConnectionPointCollection GetConsumerConnectionPoints(Type type)
		{
			ICollection[] connectionPoints = WebPartManager.GetConnectionPoints(type);
			return (ConsumerConnectionPointCollection)connectionPoints[0];
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x000EFD94 File Offset: 0x000EDF94
		public static WebPartManager GetCurrentWebPartManager(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			return page.Items[typeof(WebPartManager)] as WebPartManager;
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x000EFDC0 File Offset: 0x000EDFC0
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

		// Token: 0x060048D2 RID: 18642 RVA: 0x000EFE34 File Offset: 0x000EE034
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

		// Token: 0x060048D3 RID: 18643 RVA: 0x000EFEA4 File Offset: 0x000EE0A4
		internal ConsumerConnectionPointCollection GetEnabledConsumerConnectionPoints(WebPart webPart)
		{
			ICollection enabledConnectionPoints = WebPartManager.GetEnabledConnectionPoints(this.GetConsumerConnectionPoints(webPart), webPart);
			return new ConsumerConnectionPointCollection(enabledConnectionPoints);
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x000EFEC8 File Offset: 0x000EE0C8
		internal ProviderConnectionPointCollection GetEnabledProviderConnectionPoints(WebPart webPart)
		{
			ICollection enabledConnectionPoints = WebPartManager.GetEnabledConnectionPoints(this.GetProviderConnectionPoints(webPart), webPart);
			return new ProviderConnectionPointCollection(enabledConnectionPoints);
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x000EFEEC File Offset: 0x000EE0EC
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

		// Token: 0x060048D6 RID: 18646 RVA: 0x000EFF80 File Offset: 0x000EE180
		private static Type GetExportType(string name)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 2699759368U)
			{
				if (num <= 684013793U)
				{
					if (num != 309987595U)
					{
						if (num != 398550328U)
						{
							if (num == 684013793U)
							{
								if (name == "fontsize")
								{
									return typeof(FontSize);
								}
							}
						}
						else if (name == "string")
						{
							return typeof(string);
						}
					}
					else if (name == "chrometype")
					{
						return typeof(PartChromeType);
					}
				}
				else if (num <= 2133018345U)
				{
					if (num != 1031692888U)
					{
						if (num == 2133018345U)
						{
							if (name == "single")
							{
								return typeof(float);
							}
						}
					}
					else if (name == "color")
					{
						return typeof(Color);
					}
				}
				else if (num != 2515107422U)
				{
					if (num == 2699759368U)
					{
						if (name == "double")
						{
							return typeof(double);
						}
					}
				}
				else if (name == "int")
				{
					return typeof(int);
				}
			}
			else if (num <= 3365180733U)
			{
				if (num <= 3099987130U)
				{
					if (num != 3082431356U)
					{
						if (num == 3099987130U)
						{
							if (name == "object")
							{
								return typeof(object);
							}
						}
					}
					else if (name == "exportmode")
					{
						return typeof(WebPartExportMode);
					}
				}
				else if (num != 3345367044U)
				{
					if (num == 3365180733U)
					{
						if (name == "bool")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "chromestate")
				{
					return typeof(PartChromeState);
				}
			}
			else if (num <= 3748513642U)
			{
				if (num != 3437915536U)
				{
					if (num == 3748513642U)
					{
						if (name == "direction")
						{
							return typeof(ContentDirection);
						}
					}
				}
				else if (name == "datetime")
				{
					return typeof(DateTime);
				}
			}
			else if (num != 3781080961U)
			{
				if (num == 3904182791U)
				{
					if (name == "unit")
					{
						return typeof(Unit);
					}
				}
			}
			else if (name == "helpmode")
			{
				return typeof(WebPartHelpMode);
			}
			return WebPartUtil.DeserializeType(name, false);
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x000F0270 File Offset: 0x000EE470
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

		// Token: 0x060048D8 RID: 18648 RVA: 0x000F03EC File Offset: 0x000EE5EC
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

		// Token: 0x060048D9 RID: 18649 RVA: 0x000F048C File Offset: 0x000EE68C
		internal ProviderConnectionPoint GetProviderConnectionPoint(WebPart webPart, string connectionPointID)
		{
			ProviderConnectionPointCollection providerConnectionPoints = this.GetProviderConnectionPoints(webPart);
			if (providerConnectionPoints != null && providerConnectionPoints.Count > 0)
			{
				return providerConnectionPoints[connectionPointID];
			}
			return null;
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x000F04B6 File Offset: 0x000EE6B6
		public virtual ProviderConnectionPointCollection GetProviderConnectionPoints(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			return WebPartManager.GetProviderConnectionPoints(webPart.ToControl().GetType());
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x000F04D8 File Offset: 0x000EE6D8
		private static ProviderConnectionPointCollection GetProviderConnectionPoints(Type type)
		{
			ICollection[] connectionPoints = WebPartManager.GetConnectionPoints(type);
			return (ProviderConnectionPointCollection)connectionPoints[1];
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x000F04F4 File Offset: 0x000EE6F4
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

		// Token: 0x060048DD RID: 18653 RVA: 0x000F05A0 File Offset: 0x000EE7A0
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

		// Token: 0x060048DE RID: 18654 RVA: 0x000F06B8 File Offset: 0x000EE8B8
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

		// Token: 0x060048DF RID: 18655 RVA: 0x000F07D0 File Offset: 0x000EE9D0
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

		// Token: 0x060048E0 RID: 18656 RVA: 0x000F07F0 File Offset: 0x000EE9F0
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

		// Token: 0x060048E1 RID: 18657 RVA: 0x000F081E File Offset: 0x000EEA1E
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

		// Token: 0x060048E2 RID: 18658 RVA: 0x000F0844 File Offset: 0x000EEA44
		public virtual WebPart ImportWebPart(XmlReader reader, out string errorMessage)
		{
			this.Personalization.EnsureEnabled(true);
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			bool flag = false;
			if (this.UsePermitOnly)
			{
				this.MinimalPermissionSet.PermitOnly();
				flag = true;
			}
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
								if (this.UsePermitOnly)
								{
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									this.MediumPermissionSet.PermitOnly();
									flag = true;
								}
								type = WebPartUtil.DeserializeType(attribute2, true);
								if (this.UsePermitOnly)
								{
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									this.MinimalPermissionSet.PermitOnly();
									flag = true;
								}
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
								if (this.UsePermitOnly)
								{
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
								}
								control = this.Page.LoadControl(attribute3);
								type = control.GetType();
								if (this.UsePermitOnly)
								{
									this.MinimalPermissionSet.PermitOnly();
									flag = true;
								}
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
							if (this.UsePermitOnly)
							{
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
							}
							this.ImportIPersonalizable(reader, (control != null) ? control : webPart);
							if (this.UsePermitOnly)
							{
								this.MinimalPermissionSet.PermitOnly();
								flag = true;
							}
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
								if (this.UsePermitOnly)
								{
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
								}
								this.ImportFromReader(personalizablePropertyEntries, control, reader);
								if (this.UsePermitOnly)
								{
									this.MinimalPermissionSet.PermitOnly();
									flag = true;
								}
							}
							WebPartManager.ImportSkipTo(reader, "genericWebPartProperties");
							reader.ReadStartElement("genericWebPartProperties");
							if (this.UsePermitOnly)
							{
								CodeAccessPermission.RevertPermitOnly();
								flag = false;
							}
							this.ImportIPersonalizable(reader, webPart);
							if (this.UsePermitOnly)
							{
								this.MinimalPermissionSet.PermitOnly();
								flag = true;
							}
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
						if (this.UsePermitOnly)
						{
							CodeAccessPermission.RevertPermitOnly();
							flag = false;
						}
						this.ImportFromReader(personalizablePropertyEntries, webPart, reader);
						if (this.UsePermitOnly)
						{
							this.MinimalPermissionSet.PermitOnly();
							flag = true;
						}
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

		// Token: 0x060048E3 RID: 18659 RVA: 0x000F0D7C File Offset: 0x000EEF7C
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

		// Token: 0x060048E4 RID: 18660 RVA: 0x000F0DBC File Offset: 0x000EEFBC
		private void ImportFromReader(IDictionary personalizableProperties, Control target, XmlReader reader)
		{
			WebPartManager.ImportReadTo(reader, "property");
			bool flag = false;
			if (this.UsePermitOnly)
			{
				this.MinimalPermissionSet.PermitOnly();
				flag = true;
			}
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
								if (this.UsePermitOnly)
								{
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									this.MediumPermissionSet.PermitOnly();
									flag = true;
								}
								type = WebPartManager.GetExportType(attribute2);
								if (this.UsePermitOnly)
								{
									CodeAccessPermission.RevertPermitOnly();
									flag = false;
									this.MinimalPermissionSet.PermitOnly();
									flag = true;
								}
							}
							if (propertyInfo != null && (propertyInfo.PropertyType == type || type == null))
							{
								TypeConverterAttribute typeConverterAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(TypeConverterAttribute), true) as TypeConverterAttribute;
								if (typeConverterAttribute != null)
								{
									if (this.UsePermitOnly)
									{
										CodeAccessPermission.RevertPermitOnly();
										flag = false;
										this.MediumPermissionSet.PermitOnly();
										flag = true;
									}
									Type type2 = WebPartUtil.DeserializeType(typeConverterAttribute.ConverterTypeName, false);
									if (this.UsePermitOnly)
									{
										CodeAccessPermission.RevertPermitOnly();
										flag = false;
										this.MinimalPermissionSet.PermitOnly();
										flag = true;
									}
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
								goto IL_3CD;
							}
							reader.Skip();
						}
					}
					IL_3CD:
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

		// Token: 0x060048E5 RID: 18661 RVA: 0x000F121C File Offset: 0x000EF41C
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

		// Token: 0x060048E6 RID: 18662 RVA: 0x000F12A4 File Offset: 0x000EF4A4
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

		// Token: 0x060048E7 RID: 18663 RVA: 0x000F1364 File Offset: 0x000EF564
		internal bool IsConsumerConnected(WebPart consumer, ConsumerConnectionPoint connectionPoint)
		{
			return this.GetConnectionForConsumer(consumer, connectionPoint) != null;
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x000F1371 File Offset: 0x000EF571
		internal bool IsProviderConnected(WebPart provider, ProviderConnectionPoint connectionPoint)
		{
			return this.GetConnectionForProvider(provider, connectionPoint) != null;
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x000F1380 File Offset: 0x000EF580
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

		// Token: 0x060048EA RID: 18666 RVA: 0x000F1443 File Offset: 0x000EF643
		protected virtual void LoadCustomPersonalizationState(PersonalizationDictionary state)
		{
			this._personalizationState = state;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x000F144C File Offset: 0x000EF64C
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

		// Token: 0x060048EC RID: 18668 RVA: 0x000F15A8 File Offset: 0x000EF7A8
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
						goto IL_27E;
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
						goto IL_27E;
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
						goto IL_27E;
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
						goto IL_27E;
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
			IL_27E:
			this.Internals.SetIsStatic(webPart, false);
			this.Internals.SetIsShared(webPart, isShared);
			this.Internals.AddWebPart(webPart);
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x000F1878 File Offset: 0x000EFA78
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

		// Token: 0x060048EE RID: 18670 RVA: 0x000F18EC File Offset: 0x000EFAEC
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

		// Token: 0x060048EF RID: 18671 RVA: 0x000F1A08 File Offset: 0x000EFC08
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

		// Token: 0x060048F0 RID: 18672 RVA: 0x000F1AB0 File Offset: 0x000EFCB0
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

		// Token: 0x060048F1 RID: 18673 RVA: 0x000F1BB0 File Offset: 0x000EFDB0
		protected virtual void OnAuthorizeWebPart(WebPartAuthorizationEventArgs e)
		{
			WebPartAuthorizationEventHandler webPartAuthorizationEventHandler = (WebPartAuthorizationEventHandler)base.Events[WebPartManager.AuthorizeWebPartEvent];
			if (webPartAuthorizationEventHandler != null)
			{
				webPartAuthorizationEventHandler(this, e);
			}
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x000F1BE0 File Offset: 0x000EFDE0
		protected virtual void OnConnectionsActivated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[WebPartManager.ConnectionsActivatedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x000F1C10 File Offset: 0x000EFE10
		protected virtual void OnConnectionsActivating(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[WebPartManager.ConnectionsActivatingEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x000F1C40 File Offset: 0x000EFE40
		protected virtual void OnDisplayModeChanged(WebPartDisplayModeEventArgs e)
		{
			WebPartDisplayModeEventHandler webPartDisplayModeEventHandler = (WebPartDisplayModeEventHandler)base.Events[WebPartManager.DisplayModeChangedEvent];
			if (webPartDisplayModeEventHandler != null)
			{
				webPartDisplayModeEventHandler(this, e);
			}
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x000F1C70 File Offset: 0x000EFE70
		protected virtual void OnDisplayModeChanging(WebPartDisplayModeCancelEventArgs e)
		{
			WebPartDisplayModeCancelEventHandler webPartDisplayModeCancelEventHandler = (WebPartDisplayModeCancelEventHandler)base.Events[WebPartManager.DisplayModeChangingEvent];
			if (webPartDisplayModeCancelEventHandler != null)
			{
				webPartDisplayModeCancelEventHandler(this, e);
			}
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x000F1CA0 File Offset: 0x000EFEA0
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

		// Token: 0x060048F7 RID: 18679 RVA: 0x000F1D58 File Offset: 0x000EFF58
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

		// Token: 0x060048F8 RID: 18680 RVA: 0x000F1D94 File Offset: 0x000EFF94
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

		// Token: 0x060048F9 RID: 18681 RVA: 0x000F1E63 File Offset: 0x000F0063
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.CloseOrphanedParts();
			this._allowCreateDisplayTitles = true;
			this.OnConnectionsActivating(EventArgs.Empty);
			this.ActivateConnections();
			this.OnConnectionsActivated(EventArgs.Empty);
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x000F1E90 File Offset: 0x000F0090
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

		// Token: 0x060048FB RID: 18683 RVA: 0x000F1F04 File Offset: 0x000F0104
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

		// Token: 0x060048FC RID: 18684 RVA: 0x000F1FFC File Offset: 0x000F01FC
		protected virtual void OnSelectedWebPartChanged(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.SelectedWebPartChangedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x000F202C File Offset: 0x000F022C
		protected virtual void OnSelectedWebPartChanging(WebPartCancelEventArgs e)
		{
			WebPartCancelEventHandler webPartCancelEventHandler = (WebPartCancelEventHandler)base.Events[WebPartManager.SelectedWebPartChangingEvent];
			if (webPartCancelEventHandler != null)
			{
				webPartCancelEventHandler(this, e);
			}
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x000F205C File Offset: 0x000F025C
		protected virtual void OnWebPartAdded(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartAddedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x000F208C File Offset: 0x000F028C
		protected virtual void OnWebPartAdding(WebPartAddingEventArgs e)
		{
			WebPartAddingEventHandler webPartAddingEventHandler = (WebPartAddingEventHandler)base.Events[WebPartManager.WebPartAddingEvent];
			if (webPartAddingEventHandler != null)
			{
				webPartAddingEventHandler(this, e);
			}
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x000F20BC File Offset: 0x000F02BC
		protected virtual void OnWebPartClosed(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartClosedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x000F20EC File Offset: 0x000F02EC
		protected virtual void OnWebPartClosing(WebPartCancelEventArgs e)
		{
			WebPartCancelEventHandler webPartCancelEventHandler = (WebPartCancelEventHandler)base.Events[WebPartManager.WebPartClosingEvent];
			if (webPartCancelEventHandler != null)
			{
				webPartCancelEventHandler(this, e);
			}
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x000F211C File Offset: 0x000F031C
		protected virtual void OnWebPartDeleted(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartDeletedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x000F214C File Offset: 0x000F034C
		protected virtual void OnWebPartDeleting(WebPartCancelEventArgs e)
		{
			WebPartCancelEventHandler webPartCancelEventHandler = (WebPartCancelEventHandler)base.Events[WebPartManager.WebPartDeletingEvent];
			if (webPartCancelEventHandler != null)
			{
				webPartCancelEventHandler(this, e);
			}
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x000F217C File Offset: 0x000F037C
		protected virtual void OnWebPartMoved(WebPartEventArgs e)
		{
			WebPartEventHandler webPartEventHandler = (WebPartEventHandler)base.Events[WebPartManager.WebPartMovedEvent];
			if (webPartEventHandler != null)
			{
				webPartEventHandler(this, e);
			}
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x000F21AC File Offset: 0x000F03AC
		protected virtual void OnWebPartMoving(WebPartMovingEventArgs e)
		{
			WebPartMovingEventHandler webPartMovingEventHandler = (WebPartMovingEventHandler)base.Events[WebPartManager.WebPartMovingEvent];
			if (webPartMovingEventHandler != null)
			{
				webPartMovingEventHandler(this, e);
			}
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x000F21DC File Offset: 0x000F03DC
		protected virtual void OnWebPartsConnected(WebPartConnectionsEventArgs e)
		{
			WebPartConnectionsEventHandler webPartConnectionsEventHandler = (WebPartConnectionsEventHandler)base.Events[WebPartManager.WebPartsConnectedEvent];
			if (webPartConnectionsEventHandler != null)
			{
				webPartConnectionsEventHandler(this, e);
			}
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x000F220C File Offset: 0x000F040C
		protected virtual void OnWebPartsConnecting(WebPartConnectionsCancelEventArgs e)
		{
			WebPartConnectionsCancelEventHandler webPartConnectionsCancelEventHandler = (WebPartConnectionsCancelEventHandler)base.Events[WebPartManager.WebPartsConnectingEvent];
			if (webPartConnectionsCancelEventHandler != null)
			{
				webPartConnectionsCancelEventHandler(this, e);
			}
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x000F223C File Offset: 0x000F043C
		protected virtual void OnWebPartsDisconnected(WebPartConnectionsEventArgs e)
		{
			WebPartConnectionsEventHandler webPartConnectionsEventHandler = (WebPartConnectionsEventHandler)base.Events[WebPartManager.WebPartsDisconnectedEvent];
			if (webPartConnectionsEventHandler != null)
			{
				webPartConnectionsEventHandler(this, e);
			}
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x000F226C File Offset: 0x000F046C
		protected virtual void OnWebPartsDisconnecting(WebPartConnectionsCancelEventArgs e)
		{
			WebPartConnectionsCancelEventHandler webPartConnectionsCancelEventHandler = (WebPartConnectionsCancelEventHandler)base.Events[WebPartManager.WebPartsDisconnectingEvent];
			if (webPartConnectionsCancelEventHandler != null)
			{
				webPartConnectionsCancelEventHandler(this, e);
			}
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x000F229C File Offset: 0x000F049C
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
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\r\nzoneElement = document.getElementById('{0}');\r\nif (zoneElement != null) {{\r\n    zoneObject = __wpm.AddZone(zoneElement, '{1}', {2}, {3}, '{4}');", new object[]
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

		// Token: 0x0600490B RID: 18699 RVA: 0x000F2520 File Offset: 0x000F0720
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

		// Token: 0x0600490C RID: 18700 RVA: 0x000F2678 File Offset: 0x000F0878
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

		// Token: 0x0600490D RID: 18701 RVA: 0x000F26BE File Offset: 0x000F08BE
		internal void RemoveWebPart(WebPart webPart)
		{
			((WebPartManager.WebPartManagerControlCollection)this.Controls).RemoveWebPart(webPart);
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x000F26D4 File Offset: 0x000F08D4
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

		// Token: 0x0600490F RID: 18703 RVA: 0x000F273C File Offset: 0x000F093C
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

		// Token: 0x06004910 RID: 18704 RVA: 0x000F277C File Offset: 0x000F097C
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

		// Token: 0x06004911 RID: 18705 RVA: 0x000F27E0 File Offset: 0x000F09E0
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

		// Token: 0x06004912 RID: 18706 RVA: 0x000F2D2C File Offset: 0x000F0F2C
		protected void SetPersonalizationDirty()
		{
			this.Personalization.SetDirty();
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x000F2D39 File Offset: 0x000F0F39
		private bool ShouldRenderWebPartInZone(WebPart part, WebPartZoneBase zone)
		{
			return !(part is UnauthorizedWebPart);
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x000F2D46 File Offset: 0x000F0F46
		protected void SetSelectedWebPart(WebPart webPart)
		{
			this._selectedWebPart = webPart;
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x000F2D50 File Offset: 0x000F0F50
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

		// Token: 0x06004916 RID: 18710 RVA: 0x000F2E2F File Offset: 0x000F102F
		private bool ShouldRemoveConnection(WebPartConnection connection)
		{
			return !connection.IsShared || this.Personalization.Scope != PersonalizationScope.User;
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x000F2E49 File Offset: 0x000F1049
		protected override void TrackViewState()
		{
			this.Personalization.ApplyPersonalizationState();
			base.TrackViewState();
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x000F2E5C File Offset: 0x000F105C
		private void VerifyType(Control control)
		{
			if (control is UserControl)
			{
				return;
			}
			Type type = control.GetType();
			string text = WebPartUtil.SerializeType(type);
			Type left = WebPartUtil.DeserializeType(text, false);
			if (left != type)
			{
				throw new InvalidOperationException(SR.GetString("WebPartManager_CantAddControlType", new object[]
				{
					text
				}));
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06004919 RID: 18713 RVA: 0x000F2EAB File Offset: 0x000F10AB
		bool IPersonalizable.IsDirty
		{
			get
			{
				return this.IsCustomPersonalizationStateDirty;
			}
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x000F2EB3 File Offset: 0x000F10B3
		void IPersonalizable.Load(PersonalizationDictionary state)
		{
			this.LoadCustomPersonalizationState(state);
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x000F2EBC File Offset: 0x000F10BC
		void IPersonalizable.Save(PersonalizationDictionary state)
		{
			this.SaveCustomPersonalizationState(state);
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x000F2EC8 File Offset: 0x000F10C8
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

		// Token: 0x04002732 RID: 10034
		public static readonly WebPartDisplayMode CatalogDisplayMode = new WebPartManager.CatalogWebPartDisplayMode();

		// Token: 0x04002733 RID: 10035
		public static readonly WebPartDisplayMode ConnectDisplayMode = new WebPartManager.ConnectWebPartDisplayMode();

		// Token: 0x04002734 RID: 10036
		public static readonly WebPartDisplayMode DesignDisplayMode = new WebPartManager.DesignWebPartDisplayMode();

		// Token: 0x04002735 RID: 10037
		public static readonly WebPartDisplayMode EditDisplayMode = new WebPartManager.EditWebPartDisplayMode();

		// Token: 0x04002736 RID: 10038
		public static readonly WebPartDisplayMode BrowseDisplayMode = new WebPartManager.BrowseWebPartDisplayMode();

		// Token: 0x04002737 RID: 10039
		private static Hashtable ConnectionPointsCache;

		// Token: 0x0400274B RID: 10059
		private PermissionSet _minimalPermissionSet;

		// Token: 0x0400274C RID: 10060
		private PermissionSet _mediumPermissionSet;

		// Token: 0x0400274D RID: 10061
		private bool? _usePermitOnly;

		// Token: 0x0400274E RID: 10062
		private const string DynamicConnectionIDPrefix = "c";

		// Token: 0x0400274F RID: 10063
		private const string DynamicWebPartIDPrefix = "wp";

		// Token: 0x04002750 RID: 10064
		private const int baseIndex = 0;

		// Token: 0x04002751 RID: 10065
		private const int selectedWebPartIndex = 1;

		// Token: 0x04002752 RID: 10066
		private const int displayModeIndex = 2;

		// Token: 0x04002753 RID: 10067
		private const int controlStateArrayLength = 3;

		// Token: 0x04002754 RID: 10068
		private WebPartPersonalization _personalization;

		// Token: 0x04002755 RID: 10069
		private WebPartDisplayMode _displayMode;

		// Token: 0x04002756 RID: 10070
		private WebPartDisplayModeCollection _displayModes;

		// Token: 0x04002757 RID: 10071
		private WebPartDisplayModeCollection _supportedDisplayModes;

		// Token: 0x04002758 RID: 10072
		private WebPartManagerInternals _internals;

		// Token: 0x04002759 RID: 10073
		private bool _allowCreateDisplayTitles;

		// Token: 0x0400275A RID: 10074
		private bool _pageInitComplete;

		// Token: 0x0400275B RID: 10075
		private bool _allowEventCancellation;

		// Token: 0x0400275C RID: 10076
		private PersonalizationDictionary _personalizationState;

		// Token: 0x0400275D RID: 10077
		private bool _hasDataChanged;

		// Token: 0x0400275E RID: 10078
		private WebPartConnectionCollection _staticConnections;

		// Token: 0x0400275F RID: 10079
		private WebPartConnectionCollection _dynamicConnections;

		// Token: 0x04002760 RID: 10080
		private WebPartZoneCollection _webPartZones;

		// Token: 0x04002761 RID: 10081
		private TransformerTypeCollection _availableTransformers;

		// Token: 0x04002762 RID: 10082
		private IDictionary _displayTitles;

		// Token: 0x04002763 RID: 10083
		private static string[] displayTitleSuffix;

		// Token: 0x04002764 RID: 10084
		private IDictionary _partsForZone;

		// Token: 0x04002765 RID: 10085
		private IDictionary _partAndChildControlIDs;

		// Token: 0x04002766 RID: 10086
		private IDictionary _zoneIDs;

		// Token: 0x04002767 RID: 10087
		private WebPart _selectedWebPart;

		// Token: 0x04002768 RID: 10088
		private bool _renderClientScript;

		// Token: 0x04002769 RID: 10089
		private const string DragOverlayElementHtmlTemplate = "\r\n<div id=\"{0}___Drag\" style=\"display:none; position:absolute; z-index: 32000; filter:alpha(opacity=75)\"></div>";

		// Token: 0x0400276A RID: 10090
		private const string ExportSensitiveDataWarningDeclaration = "ExportSensitiveDataWarningDeclaration";

		// Token: 0x0400276B RID: 10091
		private const string CloseProviderWarningDeclaration = "CloseProviderWarningDeclaration";

		// Token: 0x0400276C RID: 10092
		private const string DeleteWarningDeclaration = "DeleteWarningDeclaration";

		// Token: 0x0400276D RID: 10093
		private const string StartupScript = "\r\n<script type=\"text/javascript\">\r\n\r\n__wpm = new WebPartManager();\r\n__wpm.overlayContainerElement = {0};\r\n__wpm.personalizationScopeShared = {1};\r\n\r\nvar zoneElement;\r\nvar zoneObject;\r\n{2}\r\n</script>\r\n";

		// Token: 0x0400276E RID: 10094
		private const string ZoneScript = "\r\nzoneElement = document.getElementById('{0}');\r\nif (zoneElement != null) {{\r\n    zoneObject = __wpm.AddZone(zoneElement, '{1}', {2}, {3}, '{4}');";

		// Token: 0x0400276F RID: 10095
		private const string ZonePartScript = "\r\n    zoneObject.AddWebPart(document.getElementById('{0}'), {1}, {2});";

		// Token: 0x04002770 RID: 10096
		private const string ZoneEndScript = "\r\n}";

		// Token: 0x04002771 RID: 10097
		private const string AuthorizationFilterName = "AuthorizationFilter";

		// Token: 0x04002772 RID: 10098
		private const string ImportErrorMessageName = "ImportErrorMessage";

		// Token: 0x04002773 RID: 10099
		private const string ZoneIDName = "ZoneID";

		// Token: 0x04002774 RID: 10100
		private const string ZoneIndexName = "ZoneIndex";

		// Token: 0x04002775 RID: 10101
		internal const string ExportRootElement = "webParts";

		// Token: 0x04002776 RID: 10102
		internal const string ExportPartElement = "webPart";

		// Token: 0x04002777 RID: 10103
		internal const string ExportPartNamespaceAttribute = "xmlns";

		// Token: 0x04002778 RID: 10104
		internal const string ExportPartNamespaceValue = "http://schemas.microsoft.com/WebPart/v3";

		// Token: 0x04002779 RID: 10105
		internal const string ExportMetaDataElement = "metaData";

		// Token: 0x0400277A RID: 10106
		internal const string ExportTypeElement = "type";

		// Token: 0x0400277B RID: 10107
		internal const string ExportErrorMessageElement = "importErrorMessage";

		// Token: 0x0400277C RID: 10108
		internal const string ExportDataElement = "data";

		// Token: 0x0400277D RID: 10109
		internal const string ExportPropertiesElement = "properties";

		// Token: 0x0400277E RID: 10110
		internal const string ExportPropertyElement = "property";

		// Token: 0x0400277F RID: 10111
		internal const string ExportTypeNameAttribute = "name";

		// Token: 0x04002780 RID: 10112
		internal const string ExportUserControlSrcAttribute = "src";

		// Token: 0x04002781 RID: 10113
		internal const string ExportPropertyNameAttribute = "name";

		// Token: 0x04002782 RID: 10114
		internal const string ExportGenericPartPropertiesElement = "genericWebPartProperties";

		// Token: 0x04002783 RID: 10115
		internal const string ExportIPersonalizableElement = "ipersonalizable";

		// Token: 0x04002784 RID: 10116
		internal const string ExportPropertyTypeAttribute = "type";

		// Token: 0x04002785 RID: 10117
		internal const string ExportPropertyScopeAttribute = "scope";

		// Token: 0x04002786 RID: 10118
		internal const string ExportPropertyNullAttribute = "null";

		// Token: 0x04002787 RID: 10119
		private const string ExportTypeBool = "bool";

		// Token: 0x04002788 RID: 10120
		private const string ExportTypeInt = "int";

		// Token: 0x04002789 RID: 10121
		private const string ExportTypeChromeState = "chromestate";

		// Token: 0x0400278A RID: 10122
		private const string ExportTypeChromeType = "chrometype";

		// Token: 0x0400278B RID: 10123
		private const string ExportTypeColor = "color";

		// Token: 0x0400278C RID: 10124
		private const string ExportTypeDateTime = "datetime";

		// Token: 0x0400278D RID: 10125
		private const string ExportTypeDirection = "direction";

		// Token: 0x0400278E RID: 10126
		private const string ExportTypeDouble = "double";

		// Token: 0x0400278F RID: 10127
		private const string ExportTypeExportMode = "exportmode";

		// Token: 0x04002790 RID: 10128
		private const string ExportTypeFontSize = "fontsize";

		// Token: 0x04002791 RID: 10129
		private const string ExportTypeHelpMode = "helpmode";

		// Token: 0x04002792 RID: 10130
		private const string ExportTypeObject = "object";

		// Token: 0x04002793 RID: 10131
		private const string ExportTypeSingle = "single";

		// Token: 0x04002794 RID: 10132
		private const string ExportTypeString = "string";

		// Token: 0x04002795 RID: 10133
		private const string ExportTypeUnit = "unit";

		// Token: 0x020009F5 RID: 2549
		private sealed class WebPartManagerControlCollection : ControlCollection
		{
			// Token: 0x06006D34 RID: 27956 RVA: 0x00186F44 File Offset: 0x00185144
			public WebPartManagerControlCollection(WebPartManager owner) : base(owner)
			{
				this._manager = owner;
				base.SetCollectionReadOnly("WebPartManager_CannotModify");
			}

			// Token: 0x06006D35 RID: 27957 RVA: 0x00186F60 File Offset: 0x00185160
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

			// Token: 0x06006D36 RID: 27958 RVA: 0x00186FA8 File Offset: 0x001851A8
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

			// Token: 0x06006D37 RID: 27959 RVA: 0x001870B4 File Offset: 0x001852B4
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

			// Token: 0x06006D38 RID: 27960 RVA: 0x001871DC File Offset: 0x001853DC
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

			// Token: 0x04003A2E RID: 14894
			private WebPartManager _manager;
		}

		// Token: 0x020009F6 RID: 2550
		private sealed class BrowseWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x06006D39 RID: 27961 RVA: 0x00187290 File Offset: 0x00185490
			public BrowseWebPartDisplayMode() : base("Browse")
			{
			}
		}

		// Token: 0x020009F7 RID: 2551
		private sealed class CatalogWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x06006D3A RID: 27962 RVA: 0x0018729D File Offset: 0x0018549D
			public CatalogWebPartDisplayMode() : base("Catalog")
			{
			}

			// Token: 0x17001E12 RID: 7698
			// (get) Token: 0x06006D3B RID: 27963 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E13 RID: 7699
			// (get) Token: 0x06006D3C RID: 27964 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool AssociatedWithToolZone
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E14 RID: 7700
			// (get) Token: 0x06006D3D RID: 27965 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E15 RID: 7701
			// (get) Token: 0x06006D3E RID: 27966 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool ShowHiddenWebParts
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x020009F8 RID: 2552
		private sealed class ConnectionPointKey
		{
			// Token: 0x06006D3F RID: 27967 RVA: 0x001872AA File Offset: 0x001854AA
			public ConnectionPointKey(Type type, CultureInfo culture, CultureInfo uiCulture)
			{
				this._type = type;
				this._culture = culture;
				this._uiCulture = uiCulture;
			}

			// Token: 0x06006D40 RID: 27968 RVA: 0x001872C8 File Offset: 0x001854C8
			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				WebPartManager.ConnectionPointKey connectionPointKey = obj as WebPartManager.ConnectionPointKey;
				return connectionPointKey != null && connectionPointKey._type.Equals(this._type) && connectionPointKey._culture.Equals(this._culture) && connectionPointKey._uiCulture.Equals(this._uiCulture);
			}

			// Token: 0x06006D41 RID: 27969 RVA: 0x00187320 File Offset: 0x00185520
			public override int GetHashCode()
			{
				int hashCode = this._type.GetHashCode();
				int num = (hashCode << 5) + hashCode ^ this._culture.GetHashCode();
				return (num << 5) + num ^ this._uiCulture.GetHashCode();
			}

			// Token: 0x04003A2F RID: 14895
			private Type _type;

			// Token: 0x04003A30 RID: 14896
			private CultureInfo _culture;

			// Token: 0x04003A31 RID: 14897
			private CultureInfo _uiCulture;
		}

		// Token: 0x020009F9 RID: 2553
		private sealed class ConnectWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x06006D42 RID: 27970 RVA: 0x0018735C File Offset: 0x0018555C
			public ConnectWebPartDisplayMode() : base("Connect")
			{
			}

			// Token: 0x17001E16 RID: 7702
			// (get) Token: 0x06006D43 RID: 27971 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E17 RID: 7703
			// (get) Token: 0x06006D44 RID: 27972 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool AssociatedWithToolZone
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E18 RID: 7704
			// (get) Token: 0x06006D45 RID: 27973 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E19 RID: 7705
			// (get) Token: 0x06006D46 RID: 27974 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool ShowHiddenWebParts
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x020009FA RID: 2554
		private sealed class DesignWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x06006D47 RID: 27975 RVA: 0x00187369 File Offset: 0x00185569
			public DesignWebPartDisplayMode() : base("Design")
			{
			}

			// Token: 0x17001E1A RID: 7706
			// (get) Token: 0x06006D48 RID: 27976 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E1B RID: 7707
			// (get) Token: 0x06006D49 RID: 27977 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E1C RID: 7708
			// (get) Token: 0x06006D4A RID: 27978 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool ShowHiddenWebParts
			{
				get
				{
					return true;
				}
			}
		}

		// Token: 0x020009FB RID: 2555
		private sealed class EditWebPartDisplayMode : WebPartDisplayMode
		{
			// Token: 0x06006D4B RID: 27979 RVA: 0x00187376 File Offset: 0x00185576
			public EditWebPartDisplayMode() : base("Edit")
			{
			}

			// Token: 0x17001E1D RID: 7709
			// (get) Token: 0x06006D4C RID: 27980 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool AllowPageDesign
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E1E RID: 7710
			// (get) Token: 0x06006D4D RID: 27981 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool AssociatedWithToolZone
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E1F RID: 7711
			// (get) Token: 0x06006D4E RID: 27982 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool RequiresPersonalization
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001E20 RID: 7712
			// (get) Token: 0x06006D4F RID: 27983 RVA: 0x000097B7 File Offset: 0x000079B7
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
