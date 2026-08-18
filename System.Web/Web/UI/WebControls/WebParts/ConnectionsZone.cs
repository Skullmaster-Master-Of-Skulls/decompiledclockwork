using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006B2 RID: 1714
	[Designer("System.Web.UI.Design.WebControls.WebParts.ConnectionsZoneDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ConnectionsZone : ToolZone
	{
		// Token: 0x060053F5 RID: 21493 RVA: 0x00154A67 File Offset: 0x00153A67
		public ConnectionsZone() : base(WebPartManager.ConnectDisplayMode)
		{
			this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
			this._pendingConnectionPointID = string.Empty;
			this._pendingConnectionType = ConnectionsZone.ConnectionType.None;
			this._pendingSelectedValue = null;
			this._pendingConsumerID = string.Empty;
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x060053F6 RID: 21494 RVA: 0x00154AA0 File Offset: 0x00153AA0
		private ArrayList AvailableTransformers
		{
			get
			{
				if (this._availableTransformers == null)
				{
					this._availableTransformers = new ArrayList();
					TransformerTypeCollection availableTransformers = base.WebPartManager.AvailableTransformers;
					foreach (object obj in availableTransformers)
					{
						Type type = (Type)obj;
						this._availableTransformers.Add(WebPartUtil.CreateObjectFromType(type));
					}
				}
				return this._availableTransformers;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x00154B24 File Offset: 0x00153B24
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("ConnectionsZone_CancelVerb")]
		[DefaultValue(null)]
		public virtual WebPartVerb CancelVerb
		{
			get
			{
				if (this._cancelVerb == null)
				{
					this._cancelVerb = new WebPartConnectionsCancelVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._cancelVerb).TrackViewState();
					}
				}
				return this._cancelVerb;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x060053F8 RID: 21496 RVA: 0x00154B52 File Offset: 0x00153B52
		[WebCategory("Verbs")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebSysDescription("ConnectionsZone_CloseVerb")]
		[DefaultValue(null)]
		public virtual WebPartVerb CloseVerb
		{
			get
			{
				if (this._closeVerb == null)
				{
					this._closeVerb = new WebPartConnectionsCloseVerb();
					this._closeVerb.EventArgument = "close";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._closeVerb).TrackViewState();
					}
				}
				return this._closeVerb;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x00154B90 File Offset: 0x00153B90
		// (set) Token: 0x060053FA RID: 21498 RVA: 0x00154BC2 File Offset: 0x00153BC2
		[WebSysDefaultValue("ConnectionsZone_ConfigureConnectionTitle")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConfigureConnectionTitleDescription")]
		public virtual string ConfigureConnectionTitle
		{
			get
			{
				string text = (string)this.ViewState["ConfigureConnectionTitle"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConfigureConnectionTitle");
			}
			set
			{
				this.ViewState["ConfigureConnectionTitle"] = value;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x00154BD5 File Offset: 0x00153BD5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("ConnectionsZone_ConfigureVerb")]
		[WebCategory("Verbs")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual WebPartVerb ConfigureVerb
		{
			get
			{
				if (this._configureVerb == null)
				{
					this._configureVerb = new WebPartConnectionsConfigureVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._configureVerb).TrackViewState();
					}
				}
				return this._configureVerb;
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x060053FC RID: 21500 RVA: 0x00154C04 File Offset: 0x00153C04
		// (set) Token: 0x060053FD RID: 21501 RVA: 0x00154C36 File Offset: 0x00153C36
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ConnectionsZone_ConnectToConsumerInstructionText")]
		[WebSysDescription("ConnectionsZone_ConnectToConsumerInstructionTextDescription")]
		public virtual string ConnectToConsumerInstructionText
		{
			get
			{
				string text = (string)this.ViewState["ConnectToConsumerInstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConnectToConsumerInstructionText");
			}
			set
			{
				this.ViewState["ConnectToConsumerInstructionText"] = value;
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x00154C4C File Offset: 0x00153C4C
		// (set) Token: 0x060053FF RID: 21503 RVA: 0x00154C7E File Offset: 0x00153C7E
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToConsumerTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConnectToConsumerText")]
		public virtual string ConnectToConsumerText
		{
			get
			{
				string text = (string)this.ViewState["ConnectToConsumerText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConnectToConsumerText");
			}
			set
			{
				this.ViewState["ConnectToConsumerText"] = value;
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x00154C94 File Offset: 0x00153C94
		// (set) Token: 0x06005401 RID: 21505 RVA: 0x00154CC6 File Offset: 0x00153CC6
		[WebSysDefaultValue("ConnectionsZone_ConnectToConsumerTitle")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToConsumerTitleDescription")]
		public virtual string ConnectToConsumerTitle
		{
			get
			{
				string text = (string)this.ViewState["ConnectToConsumerTitle"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConnectToConsumerTitle");
			}
			set
			{
				this.ViewState["ConnectToConsumerTitle"] = value;
			}
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06005402 RID: 21506 RVA: 0x00154CDC File Offset: 0x00153CDC
		// (set) Token: 0x06005403 RID: 21507 RVA: 0x00154D0E File Offset: 0x00153D0E
		[WebSysDefaultValue("ConnectionsZone_ConnectToProviderInstructionText")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToProviderInstructionTextDescription")]
		public virtual string ConnectToProviderInstructionText
		{
			get
			{
				string text = (string)this.ViewState["ConnectToProviderInstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConnectToProviderInstructionText");
			}
			set
			{
				this.ViewState["ConnectToProviderInstructionText"] = value;
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06005404 RID: 21508 RVA: 0x00154D24 File Offset: 0x00153D24
		// (set) Token: 0x06005405 RID: 21509 RVA: 0x00154D56 File Offset: 0x00153D56
		[WebSysDescription("ConnectionsZone_ConnectToProviderTextDescription")]
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ConnectionsZone_ConnectToProviderText")]
		public virtual string ConnectToProviderText
		{
			get
			{
				string text = (string)this.ViewState["ConnectToProviderText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConnectToProviderText");
			}
			set
			{
				this.ViewState["ConnectToProviderText"] = value;
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06005406 RID: 21510 RVA: 0x00154D6C File Offset: 0x00153D6C
		// (set) Token: 0x06005407 RID: 21511 RVA: 0x00154D9E File Offset: 0x00153D9E
		[WebSysDefaultValue("ConnectionsZone_ConnectToProviderTitle")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToProviderTitleDescription")]
		public virtual string ConnectToProviderTitle
		{
			get
			{
				string text = (string)this.ViewState["ConnectToProviderTitle"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConnectToProviderTitle");
			}
			set
			{
				this.ViewState["ConnectToProviderTitle"] = value;
			}
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x00154DB1 File Offset: 0x00153DB1
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Verbs")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("ConnectionsZone_ConnectVerb")]
		public virtual WebPartVerb ConnectVerb
		{
			get
			{
				if (this._connectVerb == null)
				{
					this._connectVerb = new WebPartConnectionsConnectVerb();
					this._connectVerb.EventArgument = "connect";
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._connectVerb).TrackViewState();
					}
				}
				return this._connectVerb;
			}
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06005409 RID: 21513 RVA: 0x00154DF0 File Offset: 0x00153DF0
		// (set) Token: 0x0600540A RID: 21514 RVA: 0x00154E22 File Offset: 0x00153E22
		[WebSysDefaultValue("ConnectionsZone_ConsumersTitle")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConsumersTitleDescription")]
		public virtual string ConsumersTitle
		{
			get
			{
				string text = (string)this.ViewState["ConsumersTitle"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConsumersTitle");
			}
			set
			{
				this.ViewState["ConsumersTitle"] = value;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x0600540B RID: 21515 RVA: 0x00154E38 File Offset: 0x00153E38
		// (set) Token: 0x0600540C RID: 21516 RVA: 0x00154E6A File Offset: 0x00153E6A
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ConnectionsZone_ConsumersInstructionText")]
		[WebSysDescription("ConnectionsZone_ConsumersInstructionTextDescription")]
		public virtual string ConsumersInstructionText
		{
			get
			{
				string text = (string)this.ViewState["ConsumersInstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ConsumersInstructionText");
			}
			set
			{
				this.ViewState["ConsumersInstructionText"] = value;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x0600540D RID: 21517 RVA: 0x00154E7D File Offset: 0x00153E7D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Verbs")]
		[WebSysDescription("ConnectionsZone_DisconnectVerb")]
		public virtual WebPartVerb DisconnectVerb
		{
			get
			{
				if (this._disconnectVerb == null)
				{
					this._disconnectVerb = new WebPartConnectionsDisconnectVerb();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._disconnectVerb).TrackViewState();
					}
				}
				return this._disconnectVerb;
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x0600540E RID: 21518 RVA: 0x00154EAB File Offset: 0x00153EAB
		protected override bool Display
		{
			get
			{
				return base.Display && this.WebPartToConnect != null;
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x0600540F RID: 21519 RVA: 0x00154EC3 File Offset: 0x00153EC3
		// (set) Token: 0x06005410 RID: 21520 RVA: 0x00154ECB File Offset: 0x00153ECB
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string EmptyZoneText
		{
			get
			{
				return base.EmptyZoneText;
			}
			set
			{
				base.EmptyZoneText = value;
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06005411 RID: 21521 RVA: 0x00154ED4 File Offset: 0x00153ED4
		// (set) Token: 0x06005412 RID: 21522 RVA: 0x00154F06 File Offset: 0x00153F06
		[WebSysDefaultValue("ConnectionsZone_WarningConnectionDisabled")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_WarningMessage")]
		public virtual string ExistingConnectionErrorMessage
		{
			get
			{
				string text = (string)this.ViewState["ExistingConnectionErrorMessage"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_WarningConnectionDisabled");
			}
			set
			{
				this.ViewState["ExistingConnectionErrorMessage"] = value;
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x00154F1C File Offset: 0x00153F1C
		// (set) Token: 0x06005414 RID: 21524 RVA: 0x00154F4E File Offset: 0x00153F4E
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ConnectionsZone_Get")]
		[WebSysDescription("ConnectionsZone_GetDescription")]
		public virtual string GetText
		{
			get
			{
				string text = (string)this.ViewState["GetText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_Get");
			}
			set
			{
				this.ViewState["GetText"] = value;
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x00154F64 File Offset: 0x00153F64
		// (set) Token: 0x06005416 RID: 21526 RVA: 0x00154F96 File Offset: 0x00153F96
		[WebSysDefaultValue("ConnectionsZone_GetFromText")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_GetFromTextDescription")]
		public virtual string GetFromText
		{
			get
			{
				string text = (string)this.ViewState["GetFromText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_GetFromText");
			}
			set
			{
				this.ViewState["GetFromText"] = value;
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x00154FAC File Offset: 0x00153FAC
		// (set) Token: 0x06005418 RID: 21528 RVA: 0x00154FDE File Offset: 0x00153FDE
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ConnectionsZone_HeaderText")]
		[WebSysDescription("ConnectionsZone_HeaderTextDescription")]
		public override string HeaderText
		{
			get
			{
				string text = (string)this.ViewState["HeaderText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_HeaderText");
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x00154FF4 File Offset: 0x00153FF4
		// (set) Token: 0x0600541A RID: 21530 RVA: 0x00155026 File Offset: 0x00154026
		[WebSysDefaultValue("ConnectionsZone_InstructionText")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_InstructionTextDescription")]
		public override string InstructionText
		{
			get
			{
				string text = (string)this.ViewState["InstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_InstructionText");
			}
			set
			{
				this.ViewState["InstructionText"] = value;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x0015503C File Offset: 0x0015403C
		// (set) Token: 0x0600541C RID: 21532 RVA: 0x0015506E File Offset: 0x0015406E
		[WebSysDefaultValue("ConnectionsZone_InstructionTitle")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_InstructionTitleDescription")]
		public virtual string InstructionTitle
		{
			get
			{
				string text = (string)this.ViewState["InstructionTitle"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_InstructionTitle");
			}
			set
			{
				this.ViewState["InstructionTitle"] = value;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x00155084 File Offset: 0x00154084
		// (set) Token: 0x0600541E RID: 21534 RVA: 0x001550B6 File Offset: 0x001540B6
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ErrorMessage")]
		[WebSysDefaultValue("ConnectionsZone_ErrorCantContinueConnectionCreation")]
		public virtual string NewConnectionErrorMessage
		{
			get
			{
				string text = (string)this.ViewState["NewConnectionErrorMessage"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ErrorCantContinueConnectionCreation");
			}
			set
			{
				this.ViewState["NewConnectionErrorMessage"] = value;
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x001550CC File Offset: 0x001540CC
		// (set) Token: 0x06005420 RID: 21536 RVA: 0x001550FE File Offset: 0x001540FE
		[WebSysDefaultValue("ConnectionsZone_NoExistingConnectionInstructionText")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_NoExistingConnectionInstructionTextDescription")]
		public virtual string NoExistingConnectionInstructionText
		{
			get
			{
				string text = (string)this.ViewState["NoExistingConnectionInstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_NoExistingConnectionInstructionText");
			}
			set
			{
				this.ViewState["NoExistingConnectionInstructionText"] = value;
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06005421 RID: 21537 RVA: 0x00155114 File Offset: 0x00154114
		// (set) Token: 0x06005422 RID: 21538 RVA: 0x00155146 File Offset: 0x00154146
		[WebSysDefaultValue("ConnectionsZone_NoExistingConnectionTitle")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_NoExistingConnectionTitleDescription")]
		public virtual string NoExistingConnectionTitle
		{
			get
			{
				string text = (string)this.ViewState["NoExistingConnectionTitle"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_NoExistingConnectionTitle");
			}
			set
			{
				this.ViewState["NoExistingConnectionTitle"] = value;
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06005423 RID: 21539 RVA: 0x00155159 File Offset: 0x00154159
		// (set) Token: 0x06005424 RID: 21540 RVA: 0x00155161 File Offset: 0x00154161
		[Browsable(false)]
		[Themeable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override PartChromeType PartChromeType
		{
			get
			{
				return base.PartChromeType;
			}
			set
			{
				base.PartChromeType = value;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x0015516C File Offset: 0x0015416C
		// (set) Token: 0x06005426 RID: 21542 RVA: 0x0015519E File Offset: 0x0015419E
		[WebSysDefaultValue("ConnectionsZone_ProvidersTitle")]
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ProvidersTitleDescription")]
		public virtual string ProvidersTitle
		{
			get
			{
				string text = (string)this.ViewState["ProvidersTitle"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ProvidersTitle");
			}
			set
			{
				this.ViewState["ProvidersTitle"] = value;
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x001551B4 File Offset: 0x001541B4
		// (set) Token: 0x06005428 RID: 21544 RVA: 0x001551E6 File Offset: 0x001541E6
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ConnectionsZone_ProvidersInstructionText")]
		[WebSysDescription("ConnectionsZone_ProvidersInstructionTextDescription")]
		public virtual string ProvidersInstructionText
		{
			get
			{
				string text = (string)this.ViewState["ProvidersInstructionText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_ProvidersInstructionText");
			}
			set
			{
				this.ViewState["ProvidersInstructionText"] = value;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x001551FC File Offset: 0x001541FC
		// (set) Token: 0x0600542A RID: 21546 RVA: 0x0015522E File Offset: 0x0015422E
		[WebCategory("Appearance")]
		[WebSysDefaultValue("ConnectionsZone_SendText")]
		[WebSysDescription("ConnectionsZone_SendTextDescription")]
		public virtual string SendText
		{
			get
			{
				string text = (string)this.ViewState["SendText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_SendText");
			}
			set
			{
				this.ViewState["SendText"] = value;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x00155244 File Offset: 0x00154244
		// (set) Token: 0x0600542C RID: 21548 RVA: 0x00155276 File Offset: 0x00154276
		[WebSysDefaultValue("ConnectionsZone_SendToText")]
		[WebSysDescription("ConnectionsZone_SendToTextDescription")]
		[WebCategory("Appearance")]
		public virtual string SendToText
		{
			get
			{
				string text = (string)this.ViewState["SendToText"];
				if (text != null)
				{
					return text;
				}
				return SR.GetString("ConnectionsZone_SendToText");
			}
			set
			{
				this.ViewState["SendToText"] = value;
			}
		}

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x0600542D RID: 21549 RVA: 0x00155289 File Offset: 0x00154289
		protected WebPart WebPartToConnect
		{
			get
			{
				if (base.WebPartManager != null && base.WebPartManager.DisplayMode == WebPartManager.ConnectDisplayMode)
				{
					return base.WebPartManager.SelectedWebPart;
				}
				return null;
			}
		}

		// Token: 0x0600542E RID: 21550 RVA: 0x001552B2 File Offset: 0x001542B2
		protected override void Close()
		{
			if (this.WebPartToConnect != null)
			{
				base.WebPartManager.EndWebPartConnecting();
			}
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x001552C8 File Offset: 0x001542C8
		private void ClearPendingConnection()
		{
			this._pendingConnectionType = ConnectionsZone.ConnectionType.None;
			this._pendingConnectionPointID = string.Empty;
			this._pendingSelectedValue = null;
			this._pendingConsumerID = string.Empty;
			this._pendingConsumer = null;
			this._pendingConsumerConnectionPoint = null;
			this._pendingProvider = null;
			this._pendingProviderConnectionPoint = null;
			this._pendingTransformerConfigurationControlTypeName = null;
			this._pendingConnectionID = null;
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00155324 File Offset: 0x00154324
		private void ConnectConsumer(string consumerConnectionPointID)
		{
			WebPart webPartToConnect = this.WebPartToConnect;
			if (webPartToConnect == null || webPartToConnect.IsClosed)
			{
				this.DisplayConnectionError();
				return;
			}
			ConsumerConnectionPoint consumerConnectionPoint = base.WebPartManager.GetConsumerConnectionPoint(webPartToConnect, consumerConnectionPointID);
			if (consumerConnectionPoint == null)
			{
				this.DisplayConnectionError();
				return;
			}
			this.EnsureChildControls();
			if (this._connectDropDownLists == null || !this._connectDropDownLists.Contains(consumerConnectionPoint) || this._connectionPointInfo == null || !this._connectionPointInfo.Contains(consumerConnectionPoint))
			{
				this.DisplayConnectionError();
				return;
			}
			DropDownList dropDownList = (DropDownList)this._connectDropDownLists[consumerConnectionPoint];
			string text = this.Page.Request.Form[dropDownList.UniqueID];
			if (!string.IsNullOrEmpty(text))
			{
				IDictionary dictionary = (IDictionary)this._connectionPointInfo[consumerConnectionPoint];
				if (dictionary == null || !dictionary.Contains(text))
				{
					this.DisplayConnectionError();
					return;
				}
				ConnectionsZone.ProviderInfo providerInfo = (ConnectionsZone.ProviderInfo)dictionary[text];
				Type transformerType = providerInfo.TransformerType;
				if (transformerType != null)
				{
					WebPartTransformer transformer = (WebPartTransformer)WebPartUtil.CreateObjectFromType(transformerType);
					if (this.GetConfigurationControl(transformer) == null)
					{
						if (base.WebPartManager.CanConnectWebParts(providerInfo.WebPart, providerInfo.ConnectionPoint, webPartToConnect, consumerConnectionPoint, transformer))
						{
							base.WebPartManager.ConnectWebParts(providerInfo.WebPart, providerInfo.ConnectionPoint, webPartToConnect, consumerConnectionPoint, transformer);
						}
						else
						{
							this.DisplayConnectionError();
						}
						this.Reset();
					}
					else
					{
						this._pendingConnectionType = ConnectionsZone.ConnectionType.Consumer;
						this._pendingConnectionPointID = consumerConnectionPointID;
						this._pendingSelectedValue = text;
						this._mode = ConnectionsZone.ConnectionsZoneMode.ConfiguringTransformer;
						base.ChildControlsCreated = false;
					}
				}
				else
				{
					if (base.WebPartManager.CanConnectWebParts(providerInfo.WebPart, providerInfo.ConnectionPoint, webPartToConnect, consumerConnectionPoint))
					{
						base.WebPartManager.ConnectWebParts(providerInfo.WebPart, providerInfo.ConnectionPoint, webPartToConnect, consumerConnectionPoint);
					}
					else
					{
						this.DisplayConnectionError();
					}
					this.Reset();
				}
				dropDownList.SelectedValue = null;
			}
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x001554F4 File Offset: 0x001544F4
		private void ConnectProvider(string providerConnectionPointID)
		{
			WebPart webPartToConnect = this.WebPartToConnect;
			if (webPartToConnect == null || webPartToConnect.IsClosed)
			{
				this.DisplayConnectionError();
				return;
			}
			ProviderConnectionPoint providerConnectionPoint = base.WebPartManager.GetProviderConnectionPoint(webPartToConnect, providerConnectionPointID);
			if (providerConnectionPoint == null)
			{
				this.DisplayConnectionError();
				return;
			}
			this.EnsureChildControls();
			if (this._connectDropDownLists == null || !this._connectDropDownLists.Contains(providerConnectionPoint) || this._connectionPointInfo == null || !this._connectionPointInfo.Contains(providerConnectionPoint))
			{
				this.DisplayConnectionError();
				return;
			}
			DropDownList dropDownList = (DropDownList)this._connectDropDownLists[providerConnectionPoint];
			string text = this.Page.Request.Form[dropDownList.UniqueID];
			if (!string.IsNullOrEmpty(text))
			{
				IDictionary dictionary = (IDictionary)this._connectionPointInfo[providerConnectionPoint];
				if (dictionary == null || !dictionary.Contains(text))
				{
					this.DisplayConnectionError();
					return;
				}
				ConnectionsZone.ConsumerInfo consumerInfo = (ConnectionsZone.ConsumerInfo)dictionary[text];
				Type transformerType = consumerInfo.TransformerType;
				if (transformerType != null)
				{
					WebPartTransformer transformer = (WebPartTransformer)WebPartUtil.CreateObjectFromType(transformerType);
					if (this.GetConfigurationControl(transformer) == null)
					{
						if (base.WebPartManager.CanConnectWebParts(webPartToConnect, providerConnectionPoint, consumerInfo.WebPart, consumerInfo.ConnectionPoint, transformer))
						{
							base.WebPartManager.ConnectWebParts(webPartToConnect, providerConnectionPoint, consumerInfo.WebPart, consumerInfo.ConnectionPoint, transformer);
						}
						else
						{
							this.DisplayConnectionError();
						}
						this.Reset();
					}
					else
					{
						this._pendingConnectionType = ConnectionsZone.ConnectionType.Provider;
						this._pendingConnectionPointID = providerConnectionPointID;
						this._pendingSelectedValue = text;
						this._mode = ConnectionsZone.ConnectionsZoneMode.ConfiguringTransformer;
						base.ChildControlsCreated = false;
					}
				}
				else
				{
					if (base.WebPartManager.CanConnectWebParts(webPartToConnect, providerConnectionPoint, consumerInfo.WebPart, consumerInfo.ConnectionPoint))
					{
						base.WebPartManager.ConnectWebParts(webPartToConnect, providerConnectionPoint, consumerInfo.WebPart, consumerInfo.ConnectionPoint);
					}
					else
					{
						this.DisplayConnectionError();
					}
					this.Reset();
				}
				dropDownList.SelectedValue = null;
			}
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x001556C4 File Offset: 0x001546C4
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			this._connectDropDownLists = new HybridDictionary();
			this._connectionPointInfo = new HybridDictionary();
			this._pendingTransformerConfigurationControl = null;
			WebPart webPartToConnect = this.WebPartToConnect;
			if (webPartToConnect != null && !webPartToConnect.IsClosed)
			{
				WebPartManager webPartManager = base.WebPartManager;
				ProviderConnectionPointCollection enabledProviderConnectionPoints = base.WebPartManager.GetEnabledProviderConnectionPoints(webPartToConnect);
				foreach (object obj in enabledProviderConnectionPoints)
				{
					ProviderConnectionPoint providerConnectionPoint = (ProviderConnectionPoint)obj;
					DropDownList dropDownList = new DropDownList();
					dropDownList.ID = "_providerlist_" + providerConnectionPoint.ID;
					dropDownList.EnableViewState = false;
					this._connectDropDownLists[providerConnectionPoint] = dropDownList;
					this.Controls.Add(dropDownList);
				}
				ConsumerConnectionPointCollection enabledConsumerConnectionPoints = base.WebPartManager.GetEnabledConsumerConnectionPoints(webPartToConnect);
				foreach (object obj2 in enabledConsumerConnectionPoints)
				{
					ConsumerConnectionPoint consumerConnectionPoint = (ConsumerConnectionPoint)obj2;
					DropDownList dropDownList2 = new DropDownList();
					dropDownList2.ID = "_consumerlist_" + consumerConnectionPoint.ID;
					dropDownList2.EnableViewState = false;
					this._connectDropDownLists[consumerConnectionPoint] = dropDownList2;
					this.Controls.Add(dropDownList2);
				}
				this.SetDropDownProperties();
				if (this._pendingConnectionType == ConnectionsZone.ConnectionType.Consumer)
				{
					if (this.EnsurePendingData())
					{
						this._pendingProvider.ToControl();
						this._pendingConsumer.ToControl();
						if (this._pendingSelectedValue != null)
						{
							IDictionary dictionary = (IDictionary)this._connectionPointInfo[this._pendingConsumerConnectionPoint];
							ConnectionsZone.ProviderInfo providerInfo = (ConnectionsZone.ProviderInfo)dictionary[this._pendingSelectedValue];
							this._pendingTransformer = (WebPartTransformer)WebPartUtil.CreateObjectFromType(providerInfo.TransformerType);
						}
						this._pendingTransformerConfigurationControl = this.GetConfigurationControl(this._pendingTransformer);
						if (this._pendingTransformerConfigurationControl != null)
						{
							((ITransformerConfigurationControl)this._pendingTransformerConfigurationControl).Cancelled += this.OnConfigurationControlCancelled;
							((ITransformerConfigurationControl)this._pendingTransformerConfigurationControl).Succeeded += this.OnConfigurationControlSucceeded;
							this.Controls.Add(this._pendingTransformerConfigurationControl);
						}
					}
				}
				else if (this._pendingConnectionType == ConnectionsZone.ConnectionType.Provider && this.EnsurePendingData())
				{
					this._pendingProvider.ToControl();
					this._pendingConsumer.ToControl();
					IDictionary dictionary2 = (IDictionary)this._connectionPointInfo[this._pendingProviderConnectionPoint];
					ConnectionsZone.ConsumerInfo consumerInfo = (ConnectionsZone.ConsumerInfo)dictionary2[this._pendingSelectedValue];
					this._pendingTransformer = (WebPartTransformer)WebPartUtil.CreateObjectFromType(consumerInfo.TransformerType);
					this._pendingTransformerConfigurationControl = this.GetConfigurationControl(this._pendingTransformer);
					if (this._pendingTransformerConfigurationControl != null)
					{
						((ITransformerConfigurationControl)this._pendingTransformerConfigurationControl).Cancelled += this.OnConfigurationControlCancelled;
						((ITransformerConfigurationControl)this._pendingTransformerConfigurationControl).Succeeded += this.OnConfigurationControlSucceeded;
						this.Controls.Add(this._pendingTransformerConfigurationControl);
					}
				}
				this.SetTransformerConfigurationControlProperties();
			}
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00155A04 File Offset: 0x00154A04
		private bool EnsurePendingData()
		{
			if (this.WebPartToConnect == null)
			{
				this.ClearPendingConnection();
				this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
				return false;
			}
			if (this._pendingConsumer != null && (this._pendingConsumerConnectionPoint == null || this._pendingProvider == null || this._pendingProviderConnectionPoint == null))
			{
				this.DisplayConnectionError();
				return false;
			}
			if (this._pendingConnectionType == ConnectionsZone.ConnectionType.Provider)
			{
				this._pendingProvider = this.WebPartToConnect;
				this._pendingProviderConnectionPoint = base.WebPartManager.GetProviderConnectionPoint(this.WebPartToConnect, this._pendingConnectionPointID);
				if (this._pendingProviderConnectionPoint == null)
				{
					this.DisplayConnectionError();
					return false;
				}
				IDictionary dictionary = (IDictionary)this._connectionPointInfo[this._pendingProviderConnectionPoint];
				ConnectionsZone.ConsumerInfo consumerInfo = null;
				if (dictionary != null)
				{
					consumerInfo = (ConnectionsZone.ConsumerInfo)dictionary[this._pendingSelectedValue];
				}
				if (consumerInfo == null)
				{
					this.DisplayConnectionError();
					return false;
				}
				this._pendingConsumer = consumerInfo.WebPart;
				this._pendingConsumerConnectionPoint = consumerInfo.ConnectionPoint;
				return true;
			}
			else
			{
				string pendingConsumerID = this._pendingConsumerID;
				if (this._pendingConnectionType != ConnectionsZone.ConnectionType.Consumer)
				{
					this.ClearPendingConnection();
					return false;
				}
				if (!string.IsNullOrEmpty(this._pendingConnectionID))
				{
					WebPartConnection webPartConnection = base.WebPartManager.Connections[this._pendingConnectionID];
					if (webPartConnection != null)
					{
						this._pendingConnectionPointID = webPartConnection.ConsumerConnectionPointID;
						this._pendingConsumer = webPartConnection.Consumer;
						this._pendingConsumerConnectionPoint = webPartConnection.ConsumerConnectionPoint;
						this._pendingConsumerID = webPartConnection.Consumer.ID;
						this._pendingProvider = webPartConnection.Provider;
						this._pendingProviderConnectionPoint = webPartConnection.ProviderConnectionPoint;
						this._pendingTransformer = webPartConnection.Transformer;
						this._pendingSelectedValue = null;
						this._pendingConnectionType = ConnectionsZone.ConnectionType.Consumer;
						return true;
					}
					this.DisplayConnectionError();
					return false;
				}
				else
				{
					if (string.IsNullOrEmpty(pendingConsumerID))
					{
						this._pendingConsumer = this.WebPartToConnect;
					}
					else
					{
						this._pendingConsumer = base.WebPartManager.WebParts[pendingConsumerID];
					}
					this._pendingConsumerConnectionPoint = base.WebPartManager.GetConsumerConnectionPoint(this._pendingConsumer, this._pendingConnectionPointID);
					if (this._pendingConsumerConnectionPoint == null)
					{
						this.DisplayConnectionError();
						return false;
					}
					if (!string.IsNullOrEmpty(this._pendingSelectedValue))
					{
						IDictionary dictionary2 = (IDictionary)this._connectionPointInfo[this._pendingConsumerConnectionPoint];
						ConnectionsZone.ProviderInfo providerInfo = null;
						if (dictionary2 != null)
						{
							providerInfo = (ConnectionsZone.ProviderInfo)dictionary2[this._pendingSelectedValue];
						}
						if (providerInfo == null)
						{
							this.DisplayConnectionError();
							return false;
						}
						this._pendingProvider = providerInfo.WebPart;
						this._pendingProviderConnectionPoint = providerInfo.ConnectionPoint;
					}
					return true;
				}
			}
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x00155C60 File Offset: 0x00154C60
		private void Disconnect(string connectionID)
		{
			WebPartConnection webPartConnection = base.WebPartManager.Connections[connectionID];
			if (webPartConnection != null)
			{
				if (webPartConnection.Provider != this.WebPartToConnect && webPartConnection.Consumer != this.WebPartToConnect)
				{
					throw new InvalidOperationException(SR.GetString("ConnectionsZone_DisconnectInvalid"));
				}
				base.WebPartManager.DisconnectWebParts(webPartConnection);
			}
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x00155CBC File Offset: 0x00154CBC
		private Control GetConfigurationControl(WebPartTransformer transformer)
		{
			Control control = transformer.CreateConfigurationControl();
			if (control == null)
			{
				return null;
			}
			if (!(control is ITransformerConfigurationControl))
			{
				throw new InvalidOperationException(SR.GetString("ConnectionsZone_MustImplementITransformerConfigurationControl"));
			}
			string assemblyQualifiedName = control.GetType().AssemblyQualifiedName;
			if (this._pendingTransformerConfigurationControlTypeName != null && this._pendingTransformerConfigurationControlTypeName != assemblyQualifiedName)
			{
				this.DisplayConnectionError();
				return null;
			}
			this._pendingTransformerConfigurationControlTypeName = assemblyQualifiedName;
			return control;
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00155D20 File Offset: 0x00154D20
		private string GetDisplayTitle(WebPart part, ConnectionPoint connectionPoint, bool isConsumer)
		{
			if (part == null)
			{
				return SR.GetString("Part_Unknown");
			}
			int num = isConsumer ? base.WebPartManager.GetConsumerConnectionPoints(part).Count : base.WebPartManager.GetProviderConnectionPoints(part).Count;
			if (num == 1)
			{
				return part.DisplayTitle;
			}
			return part.DisplayTitle + " (" + ((connectionPoint != null) ? connectionPoint.DisplayName : SR.GetString("Part_Unknown")) + ")";
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x00155D98 File Offset: 0x00154D98
		private IDictionary GetValidConsumers(WebPart provider, ProviderConnectionPoint providerConnectionPoint, WebPartCollection webParts)
		{
			HybridDictionary hybridDictionary = new HybridDictionary();
			if (providerConnectionPoint == null || provider == null || !provider.AllowConnect)
			{
				return hybridDictionary;
			}
			if (!providerConnectionPoint.AllowsMultipleConnections && base.WebPartManager.IsProviderConnected(provider, providerConnectionPoint))
			{
				return hybridDictionary;
			}
			foreach (object obj in webParts)
			{
				WebPart webPart = (WebPart)obj;
				if (webPart.AllowConnect && webPart != provider && !webPart.IsClosed)
				{
					foreach (object obj2 in base.WebPartManager.GetConsumerConnectionPoints(webPart))
					{
						ConsumerConnectionPoint consumerConnectionPoint = (ConsumerConnectionPoint)obj2;
						if (base.WebPartManager.CanConnectWebParts(provider, providerConnectionPoint, webPart, consumerConnectionPoint))
						{
							hybridDictionary.Add(webPart.ID + '$' + consumerConnectionPoint.ID, new ConnectionsZone.ConsumerInfo(webPart, consumerConnectionPoint));
						}
						else
						{
							foreach (object obj3 in this.AvailableTransformers)
							{
								WebPartTransformer webPartTransformer = (WebPartTransformer)obj3;
								if (base.WebPartManager.CanConnectWebParts(provider, providerConnectionPoint, webPart, consumerConnectionPoint, webPartTransformer))
								{
									hybridDictionary.Add(webPart.ID + '$' + consumerConnectionPoint.ID, new ConnectionsZone.ConsumerInfo(webPart, consumerConnectionPoint, webPartTransformer.GetType()));
									break;
								}
							}
						}
					}
				}
			}
			return hybridDictionary;
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x00155F78 File Offset: 0x00154F78
		private IDictionary GetValidProviders(WebPart consumer, ConsumerConnectionPoint consumerConnectionPoint, WebPartCollection webParts)
		{
			HybridDictionary hybridDictionary = new HybridDictionary();
			if (consumerConnectionPoint == null || consumer == null || !consumer.AllowConnect)
			{
				return hybridDictionary;
			}
			if (!consumerConnectionPoint.AllowsMultipleConnections && base.WebPartManager.IsConsumerConnected(consumer, consumerConnectionPoint))
			{
				return hybridDictionary;
			}
			foreach (object obj in webParts)
			{
				WebPart webPart = (WebPart)obj;
				if (webPart.AllowConnect && webPart != consumer && !webPart.IsClosed)
				{
					foreach (object obj2 in base.WebPartManager.GetProviderConnectionPoints(webPart))
					{
						ProviderConnectionPoint providerConnectionPoint = (ProviderConnectionPoint)obj2;
						if (base.WebPartManager.CanConnectWebParts(webPart, providerConnectionPoint, consumer, consumerConnectionPoint))
						{
							hybridDictionary.Add(webPart.ID + '$' + providerConnectionPoint.ID, new ConnectionsZone.ProviderInfo(webPart, providerConnectionPoint));
						}
						else
						{
							foreach (object obj3 in this.AvailableTransformers)
							{
								WebPartTransformer webPartTransformer = (WebPartTransformer)obj3;
								if (base.WebPartManager.CanConnectWebParts(webPart, providerConnectionPoint, consumer, consumerConnectionPoint, webPartTransformer))
								{
									hybridDictionary.Add(webPart.ID + '$' + providerConnectionPoint.ID, new ConnectionsZone.ProviderInfo(webPart, providerConnectionPoint, webPartTransformer.GetType()));
									break;
								}
							}
						}
					}
				}
			}
			return hybridDictionary;
		}

		// Token: 0x06005439 RID: 21561 RVA: 0x00156158 File Offset: 0x00155158
		private bool HasConfigurationControl(WebPartTransformer transformer)
		{
			return transformer.CreateConfigurationControl() != null;
		}

		// Token: 0x0600543A RID: 21562 RVA: 0x00156168 File Offset: 0x00155168
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array.Length != 8)
				{
					throw new ArgumentException(SR.GetString("Invalid_ControlState"));
				}
				base.LoadControlState(array[0]);
				if (array[1] != null)
				{
					this._mode = (ConnectionsZone.ConnectionsZoneMode)array[1];
				}
				if (array[2] != null)
				{
					this._pendingConnectionPointID = (string)array[2];
				}
				if (array[3] != null)
				{
					this._pendingConnectionType = (ConnectionsZone.ConnectionType)array[3];
				}
				if (array[4] != null)
				{
					this._pendingSelectedValue = (string)array[4];
				}
				if (array[5] != null)
				{
					this._pendingConsumerID = (string)array[5];
				}
				if (array[6] != null)
				{
					this._pendingTransformerConfigurationControlTypeName = (string)array[6];
				}
				if (array[7] != null)
				{
					this._pendingConnectionID = (string)array[7];
					return;
				}
			}
			else
			{
				base.LoadControlState(null);
			}
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x00156230 File Offset: 0x00155230
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array.Length != 6)
			{
				throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.CancelVerb).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.CloseVerb).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.ConfigureVerb).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.ConnectVerb).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.DisconnectVerb).LoadViewState(array[5]);
			}
		}

		// Token: 0x0600543C RID: 21564 RVA: 0x001562CD File Offset: 0x001552CD
		private void OnConfigurationControlCancelled(object sender, EventArgs e)
		{
			this.Reset();
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x001562D5 File Offset: 0x001552D5
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
				this.Page.PreRenderComplete += this.OnPagePreRenderComplete;
			}
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0015630C File Offset: 0x0015530C
		private void OnConfigurationControlSucceeded(object sender, EventArgs e)
		{
			this.EnsurePendingData();
			if (this._pendingConnectionType == ConnectionsZone.ConnectionType.Consumer && !string.IsNullOrEmpty(this._pendingConnectionID))
			{
				base.WebPartManager.Personalization.SetDirty();
			}
			else if (base.WebPartManager.CanConnectWebParts(this._pendingProvider, this._pendingProviderConnectionPoint, this._pendingConsumer, this._pendingConsumerConnectionPoint, this._pendingTransformer))
			{
				base.WebPartManager.ConnectWebParts(this._pendingProvider, this._pendingProviderConnectionPoint, this._pendingConsumer, this._pendingConsumerConnectionPoint, this._pendingTransformer);
			}
			else
			{
				this.DisplayConnectionError();
			}
			this.Reset();
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x001563AB File Offset: 0x001553AB
		protected override void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			this.Reset();
			base.OnDisplayModeChanged(sender, e);
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x001563BB File Offset: 0x001553BB
		private void OnPagePreRenderComplete(object sender, EventArgs e)
		{
			this.SetTransformerConfigurationControlProperties();
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x001563C3 File Offset: 0x001553C3
		protected override void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			if (base.WebPartManager != null && base.WebPartManager.DisplayMode == WebPartManager.ConnectDisplayMode)
			{
				this.Reset();
			}
			base.OnSelectedWebPartChanged(sender, e);
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x001563ED File Offset: 0x001553ED
		private void DisplayConnectionError()
		{
			this._displayErrorMessage = true;
			this.Reset();
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x001563FC File Offset: 0x001553FC
		protected override void RaisePostBackEvent(string eventArgument)
		{
			if (this.WebPartToConnect == null)
			{
				this.ClearPendingConnection();
				this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
				return;
			}
			string[] array = eventArgument.Split(new char[]
			{
				'$'
			});
			if (array.Length == 2 && string.Equals(array[0], "disconnect", StringComparison.OrdinalIgnoreCase))
			{
				if (this.DisconnectVerb.Visible && this.DisconnectVerb.Enabled)
				{
					string connectionID = array[1];
					this.Disconnect(connectionID);
					this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
					return;
				}
			}
			else if (array.Length == 3 && string.Equals(array[0], "connect", StringComparison.OrdinalIgnoreCase))
			{
				if (this.ConnectVerb.Visible && this.ConnectVerb.Enabled)
				{
					string text = array[2];
					if (string.Equals(array[1], "provider", StringComparison.OrdinalIgnoreCase))
					{
						this.ConnectProvider(text);
						return;
					}
					this.ConnectConsumer(text);
					return;
				}
			}
			else
			{
				if (array.Length == 2 && string.Equals(array[0], "edit", StringComparison.OrdinalIgnoreCase))
				{
					this._pendingConnectionID = array[1];
					this._pendingConnectionType = ConnectionsZone.ConnectionType.Consumer;
					this._mode = ConnectionsZone.ConnectionsZoneMode.ConfiguringTransformer;
					return;
				}
				if (string.Equals(eventArgument, "connectconsumer", StringComparison.OrdinalIgnoreCase))
				{
					this._mode = ConnectionsZone.ConnectionsZoneMode.ConnectToConsumer;
					return;
				}
				if (string.Equals(eventArgument, "connectprovider", StringComparison.OrdinalIgnoreCase))
				{
					this._mode = ConnectionsZone.ConnectionsZoneMode.ConnectToProvider;
					return;
				}
				if (string.Equals(eventArgument, "close", StringComparison.OrdinalIgnoreCase))
				{
					if (this.CloseVerb.Visible && this.CloseVerb.Enabled)
					{
						this.Close();
						this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
						return;
					}
				}
				else if (string.Equals(eventArgument, "cancel", StringComparison.OrdinalIgnoreCase))
				{
					if (this.CancelVerb.Visible && this.CancelVerb.Enabled)
					{
						this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
						return;
					}
				}
				else
				{
					base.RaisePostBackEvent(eventArgument);
				}
			}
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0015659D File Offset: 0x0015559D
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.SetDropDownProperties();
			base.Render(writer);
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x001565C0 File Offset: 0x001555C0
		private void RenderAddVerbs(HtmlTextWriter writer)
		{
			WebPart webPartToConnect = this.WebPartToConnect;
			WebPartCollection webParts = null;
			if (base.WebPartManager != null)
			{
				webParts = base.WebPartManager.WebParts;
			}
			if (webPartToConnect != null || base.DesignMode)
			{
				bool flag = base.DesignMode;
				if (!flag && base.WebPartManager != null)
				{
					ProviderConnectionPointCollection enabledProviderConnectionPoints = base.WebPartManager.GetEnabledProviderConnectionPoints(webPartToConnect);
					foreach (object obj in enabledProviderConnectionPoints)
					{
						ProviderConnectionPoint providerConnectionPoint = (ProviderConnectionPoint)obj;
						if (this.GetValidConsumers(webPartToConnect, providerConnectionPoint, webParts).Count != 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					ZoneLinkButton zoneLinkButton = new ZoneLinkButton(this, "connectconsumer");
					zoneLinkButton.Text = this.ConnectToConsumerText;
					zoneLinkButton.ApplyStyle(base.VerbStyle);
					zoneLinkButton.Page = this.Page;
					zoneLinkButton.RenderControl(writer);
					writer.WriteBreak();
				}
				bool flag2 = base.DesignMode;
				if (!flag2 && base.WebPartManager != null)
				{
					ConsumerConnectionPointCollection enabledConsumerConnectionPoints = base.WebPartManager.GetEnabledConsumerConnectionPoints(webPartToConnect);
					foreach (object obj2 in enabledConsumerConnectionPoints)
					{
						ConsumerConnectionPoint consumerConnectionPoint = (ConsumerConnectionPoint)obj2;
						if (this.GetValidProviders(webPartToConnect, consumerConnectionPoint, webParts).Count != 0)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					ZoneLinkButton zoneLinkButton2 = new ZoneLinkButton(this, "connectprovider");
					zoneLinkButton2.Text = this.ConnectToProviderText;
					zoneLinkButton2.ApplyStyle(base.VerbStyle);
					zoneLinkButton2.Page = this.Page;
					zoneLinkButton2.RenderControl(writer);
					writer.WriteBreak();
				}
				if (flag2 || flag)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Hr);
					writer.RenderEndTag();
				}
			}
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x00156794 File Offset: 0x00155794
		protected override void RenderBody(HtmlTextWriter writer)
		{
			if (this.PartChromeType == PartChromeType.Default || this.PartChromeType == PartChromeType.BorderOnly || this.PartChromeType == PartChromeType.TitleAndBorder)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, "Black");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "1px");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "Solid");
			}
			base.RenderBodyTableBeginTag(writer);
			this.RenderErrorMessage(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			switch (this._mode)
			{
			case ConnectionsZone.ConnectionsZoneMode.ConnectToConsumer:
				this.RenderConnectToConsumersDropDowns(writer);
				break;
			case ConnectionsZone.ConnectionsZoneMode.ConnectToProvider:
				this.RenderConnectToProvidersDropDowns(writer);
				break;
			case ConnectionsZone.ConnectionsZoneMode.ConfiguringTransformer:
				if (this._pendingTransformerConfigurationControl != null)
				{
					this.RenderTransformerConfigurationHeader(writer);
					this._pendingTransformerConfigurationControl.RenderControl(writer);
				}
				break;
			default:
				this.RenderAddVerbs(writer);
				this.RenderExistingConnections(writer);
				break;
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			WebZone.RenderBodyTableEndTag(writer);
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x00156878 File Offset: 0x00155878
		private void RenderConnectToConsumersDropDowns(HtmlTextWriter writer)
		{
			WebPart webPartToConnect = this.WebPartToConnect;
			if (webPartToConnect != null)
			{
				ProviderConnectionPointCollection enabledProviderConnectionPoints = base.WebPartManager.GetEnabledProviderConnectionPoints(webPartToConnect);
				bool flag = true;
				Label label = new Label();
				label.Page = this.Page;
				label.AssociatedControlInControlTree = false;
				foreach (object obj in enabledProviderConnectionPoints)
				{
					ProviderConnectionPoint providerConnectionPoint = (ProviderConnectionPoint)obj;
					DropDownList dropDownList = (DropDownList)this._connectDropDownLists[providerConnectionPoint];
					if (dropDownList != null && dropDownList.Enabled)
					{
						if (flag)
						{
							string connectToConsumerTitle = this.ConnectToConsumerTitle;
							if (!string.IsNullOrEmpty(connectToConsumerTitle))
							{
								label.Text = connectToConsumerTitle;
								label.ApplyStyle(base.LabelStyle);
								label.AssociatedControlID = string.Empty;
								label.RenderControl(writer);
								writer.WriteBreak();
							}
							string connectToConsumerInstructionText = this.ConnectToConsumerInstructionText;
							if (!string.IsNullOrEmpty(connectToConsumerInstructionText))
							{
								writer.WriteBreak();
								label.Text = connectToConsumerInstructionText;
								label.ApplyStyle(base.InstructionTextStyle);
								label.AssociatedControlID = string.Empty;
								label.RenderControl(writer);
								writer.WriteBreak();
							}
							flag = false;
						}
						writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
						writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
						writer.RenderBeginTag(HtmlTextWriterTag.Table);
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						label.ApplyStyle(base.LabelStyle);
						label.Text = this.SendText;
						label.AssociatedControlID = string.Empty;
						label.RenderControl(writer);
						writer.RenderEndTag();
						base.LabelStyle.AddAttributesToRender(writer, this);
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						writer.WriteEncodedText(providerConnectionPoint.DisplayName);
						writer.RenderEndTag();
						writer.RenderEndTag();
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						label.Text = this.SendToText;
						label.AssociatedControlID = dropDownList.ClientID;
						label.RenderControl(writer);
						writer.RenderEndTag();
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						dropDownList.ApplyStyle(base.EditUIStyle);
						dropDownList.RenderControl(writer);
						writer.RenderEndTag();
						writer.RenderEndTag();
						writer.RenderEndTag();
						WebPartVerb connectVerb = this.ConnectVerb;
						connectVerb.EventArgument = string.Join('$'.ToString(CultureInfo.InvariantCulture), new string[]
						{
							"connect",
							"provider",
							providerConnectionPoint.ID
						});
						this.RenderVerb(writer, connectVerb);
						writer.RenderEndTag();
					}
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				WebPartVerb cancelVerb = this.CancelVerb;
				cancelVerb.EventArgument = "cancel";
				this.RenderVerb(writer, cancelVerb);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x00156B44 File Offset: 0x00155B44
		private void RenderConnectToProvidersDropDowns(HtmlTextWriter writer)
		{
			WebPart webPartToConnect = this.WebPartToConnect;
			if (webPartToConnect != null)
			{
				ConsumerConnectionPointCollection enabledConsumerConnectionPoints = base.WebPartManager.GetEnabledConsumerConnectionPoints(webPartToConnect);
				bool flag = true;
				Label label = new Label();
				label.Page = this.Page;
				label.AssociatedControlInControlTree = false;
				foreach (object obj in enabledConsumerConnectionPoints)
				{
					ConsumerConnectionPoint consumerConnectionPoint = (ConsumerConnectionPoint)obj;
					DropDownList dropDownList = (DropDownList)this._connectDropDownLists[consumerConnectionPoint];
					if (dropDownList != null && dropDownList.Enabled)
					{
						if (flag)
						{
							string connectToProviderTitle = this.ConnectToProviderTitle;
							if (!string.IsNullOrEmpty(connectToProviderTitle))
							{
								label.Text = connectToProviderTitle;
								label.ApplyStyle(base.LabelStyle);
								label.AssociatedControlID = string.Empty;
								label.RenderControl(writer);
								writer.WriteBreak();
							}
							string connectToProviderInstructionText = this.ConnectToProviderInstructionText;
							if (!string.IsNullOrEmpty(connectToProviderInstructionText))
							{
								writer.WriteBreak();
								label.Text = connectToProviderInstructionText;
								label.ApplyStyle(base.InstructionTextStyle);
								label.AssociatedControlID = string.Empty;
								label.RenderControl(writer);
								writer.WriteBreak();
							}
							flag = false;
						}
						writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
						writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
						writer.RenderBeginTag(HtmlTextWriterTag.Table);
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						label.ApplyStyle(base.LabelStyle);
						label.Text = this.GetText;
						label.AssociatedControlID = string.Empty;
						label.RenderControl(writer);
						writer.RenderEndTag();
						base.LabelStyle.AddAttributesToRender(writer, this);
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						writer.WriteEncodedText(consumerConnectionPoint.DisplayName);
						writer.RenderEndTag();
						writer.RenderEndTag();
						writer.RenderBeginTag(HtmlTextWriterTag.Tr);
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						label.Text = this.GetFromText;
						label.AssociatedControlID = dropDownList.ClientID;
						label.RenderControl(writer);
						writer.RenderEndTag();
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						dropDownList.ApplyStyle(base.EditUIStyle);
						dropDownList.RenderControl(writer);
						writer.RenderEndTag();
						writer.RenderEndTag();
						writer.RenderEndTag();
						WebPartVerb connectVerb = this.ConnectVerb;
						connectVerb.EventArgument = string.Join('$'.ToString(CultureInfo.InvariantCulture), new string[]
						{
							"connect",
							"consumer",
							consumerConnectionPoint.ID
						});
						this.RenderVerb(writer, connectVerb);
						writer.RenderEndTag();
					}
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "right");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				WebPartVerb cancelVerb = this.CancelVerb;
				cancelVerb.EventArgument = "cancel";
				this.RenderVerb(writer, cancelVerb);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x00156E10 File Offset: 0x00155E10
		private void RenderErrorMessage(HtmlTextWriter writer)
		{
			if (this._displayErrorMessage)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				TableCell tableCell = new TableCell();
				tableCell.ApplyStyle(base.ErrorStyle);
				tableCell.Text = this.NewConnectionErrorMessage;
				tableCell.RenderControl(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x00156E58 File Offset: 0x00155E58
		private void RenderExistingConnections(HtmlTextWriter writer)
		{
			WebPartManager webPartManager = base.WebPartManager;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (webPartManager != null)
			{
				WebPart webPartToConnect = this.WebPartToConnect;
				WebPartConnectionCollection connections = webPartManager.Connections;
				foreach (object obj in connections)
				{
					WebPartConnection webPartConnection = (WebPartConnection)obj;
					if (webPartConnection.Provider == webPartToConnect)
					{
						if (!flag)
						{
							this.RenderInstructionTitle(writer);
							this.RenderInstructionText(writer);
							flag = true;
						}
						if (!flag2)
						{
							writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
							base.LabelStyle.AddAttributesToRender(writer, this);
							writer.RenderBeginTag(HtmlTextWriterTag.Legend);
							writer.Write(this.ConsumersTitle);
							writer.RenderEndTag();
							string consumersInstructionText = this.ConsumersInstructionText;
							if (!string.IsNullOrEmpty(consumersInstructionText))
							{
								writer.WriteBreak();
								Label label = new Label();
								label.Text = consumersInstructionText;
								label.Page = this.Page;
								label.ApplyStyle(base.InstructionTextStyle);
								label.RenderControl(writer);
								writer.WriteBreak();
							}
							flag2 = true;
						}
						this.RenderExistingConsumerConnection(writer, webPartConnection);
					}
				}
				if (flag2)
				{
					writer.RenderEndTag();
				}
				foreach (object obj2 in connections)
				{
					WebPartConnection webPartConnection2 = (WebPartConnection)obj2;
					if (webPartConnection2.Consumer == webPartToConnect)
					{
						if (!flag)
						{
							this.RenderInstructionTitle(writer);
							this.RenderInstructionText(writer);
							flag = true;
						}
						if (!flag3)
						{
							writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
							base.LabelStyle.AddAttributesToRender(writer, this);
							writer.RenderBeginTag(HtmlTextWriterTag.Legend);
							writer.Write(this.ProvidersTitle);
							writer.RenderEndTag();
							string providersInstructionText = this.ProvidersInstructionText;
							if (!string.IsNullOrEmpty(providersInstructionText))
							{
								writer.WriteBreak();
								Label label2 = new Label();
								label2.Text = providersInstructionText;
								label2.Page = this.Page;
								label2.ApplyStyle(base.InstructionTextStyle);
								label2.RenderControl(writer);
								writer.WriteBreak();
							}
							flag3 = true;
						}
						this.RenderExistingProviderConnection(writer, webPartConnection2);
					}
				}
			}
			if (flag3)
			{
				writer.RenderEndTag();
			}
			if (flag)
			{
				writer.WriteBreak();
				return;
			}
			this.RenderNoExistingConnection(writer);
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x001570A8 File Offset: 0x001560A8
		private void RenderExistingConnection(HtmlTextWriter writer, string connectionPointName, string partTitle, string disconnectEventArg, string editEventArg, bool consumer, bool isActive)
		{
			Label label = new Label();
			label.Page = this.Page;
			label.ApplyStyle(base.LabelStyle);
			writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			label.Text = (consumer ? this.SendText : this.GetText);
			label.RenderControl(writer);
			writer.RenderEndTag();
			base.LabelStyle.AddAttributesToRender(writer, this);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.WriteEncodedText(connectionPointName);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			label.Text = (consumer ? this.SendToText : this.GetFromText);
			label.RenderControl(writer);
			writer.RenderEndTag();
			base.LabelStyle.AddAttributesToRender(writer, this);
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			writer.WriteEncodedText(partTitle);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			WebPartVerb disconnectVerb = this.DisconnectVerb;
			disconnectVerb.EventArgument = disconnectEventArg;
			this.RenderVerb(writer, disconnectVerb);
			if (this.VerbButtonType == ButtonType.Link)
			{
				writer.Write("&nbsp;");
			}
			if (isActive)
			{
				WebPartVerb configureVerb = this.ConfigureVerb;
				if (editEventArg == null)
				{
					configureVerb.Enabled = false;
				}
				else
				{
					configureVerb.Enabled = true;
					configureVerb.EventArgument = editEventArg;
				}
				this.RenderVerb(writer, configureVerb);
			}
			else
			{
				writer.WriteBreak();
				label.ApplyStyle(base.ErrorStyle);
				label.Text = this.ExistingConnectionErrorMessage;
				label.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600544C RID: 21580 RVA: 0x00157238 File Offset: 0x00156238
		private void RenderExistingConsumerConnection(HtmlTextWriter writer, WebPartConnection connection)
		{
			WebPart webPartToConnect = this.WebPartToConnect;
			ProviderConnectionPoint providerConnectionPoint = base.WebPartManager.GetProviderConnectionPoint(webPartToConnect, connection.ProviderConnectionPointID);
			WebPart consumer = connection.Consumer;
			ConsumerConnectionPoint consumerConnectionPoint = connection.ConsumerConnectionPoint;
			string displayTitle = this.GetDisplayTitle(consumer, consumerConnectionPoint, true);
			string editEventArg = null;
			WebPartTransformer transformer = connection.Transformer;
			if (transformer != null && this.HasConfigurationControl(transformer))
			{
				editEventArg = "edit" + '$'.ToString(CultureInfo.InvariantCulture) + connection.ID;
			}
			bool isActive = providerConnectionPoint != null && consumerConnectionPoint != null && connection.Provider != null && connection.Consumer != null && connection.IsActive;
			this.RenderExistingConnection(writer, (providerConnectionPoint != null) ? providerConnectionPoint.DisplayName : SR.GetString("Part_Unknown"), displayTitle, string.Join('$'.ToString(CultureInfo.InvariantCulture), new string[]
			{
				"disconnect",
				connection.ID
			}), editEventArg, true, isActive);
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x0015732C File Offset: 0x0015632C
		private void RenderExistingProviderConnection(HtmlTextWriter writer, WebPartConnection connection)
		{
			WebPart webPartToConnect = this.WebPartToConnect;
			ConsumerConnectionPoint consumerConnectionPoint = base.WebPartManager.GetConsumerConnectionPoint(webPartToConnect, connection.ConsumerConnectionPointID);
			WebPart provider = connection.Provider;
			ProviderConnectionPoint providerConnectionPoint = connection.ProviderConnectionPoint;
			string displayTitle = this.GetDisplayTitle(provider, providerConnectionPoint, false);
			string editEventArg = null;
			WebPartTransformer transformer = connection.Transformer;
			if (transformer != null && this.HasConfigurationControl(transformer))
			{
				editEventArg = "edit" + '$'.ToString(CultureInfo.InvariantCulture) + connection.ID;
			}
			bool isActive = providerConnectionPoint != null && consumerConnectionPoint != null && connection.Provider != null && connection.Consumer != null && connection.IsActive;
			this.RenderExistingConnection(writer, (consumerConnectionPoint != null) ? consumerConnectionPoint.DisplayName : SR.GetString("Part_Unknown"), displayTitle, string.Join('$'.ToString(CultureInfo.InvariantCulture), new string[]
			{
				"disconnect",
				connection.ID
			}), editEventArg, false, isActive);
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x00157420 File Offset: 0x00156420
		private void RenderInstructionText(HtmlTextWriter writer)
		{
			string instructionText = this.InstructionText;
			if (!string.IsNullOrEmpty(instructionText))
			{
				Label label = new Label();
				label.Text = instructionText;
				label.Page = this.Page;
				label.ApplyStyle(base.InstructionTextStyle);
				label.RenderControl(writer);
				writer.WriteBreak();
				writer.WriteBreak();
			}
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x00157474 File Offset: 0x00156474
		private void RenderInstructionTitle(HtmlTextWriter writer)
		{
			if (this.PartChromeType == PartChromeType.None || this.PartChromeType == PartChromeType.BorderOnly)
			{
				return;
			}
			string instructionTitle = this.InstructionTitle;
			if (!string.IsNullOrEmpty(instructionTitle))
			{
				Label label = new Label();
				if (this.WebPartToConnect != null)
				{
					label.Text = string.Format(CultureInfo.CurrentCulture, instructionTitle, new object[]
					{
						this.WebPartToConnect.DisplayTitle
					});
				}
				else
				{
					label.Text = instructionTitle;
				}
				label.Page = this.Page;
				label.ApplyStyle(base.LabelStyle);
				label.RenderControl(writer);
				writer.WriteBreak();
			}
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x00157508 File Offset: 0x00156508
		private void RenderNoExistingConnection(HtmlTextWriter writer)
		{
			string noExistingConnectionTitle = this.NoExistingConnectionTitle;
			if (!string.IsNullOrEmpty(noExistingConnectionTitle))
			{
				Label label = new Label();
				label.Text = noExistingConnectionTitle;
				label.Page = this.Page;
				label.ApplyStyle(base.LabelStyle);
				label.RenderControl(writer);
				writer.WriteBreak();
				writer.WriteBreak();
			}
			string noExistingConnectionInstructionText = this.NoExistingConnectionInstructionText;
			if (!string.IsNullOrEmpty(noExistingConnectionInstructionText))
			{
				Label label2 = new Label();
				label2.Text = noExistingConnectionInstructionText;
				label2.Page = this.Page;
				label2.ApplyStyle(base.InstructionTextStyle);
				label2.RenderControl(writer);
				writer.WriteBreak();
				writer.WriteBreak();
			}
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x001575A4 File Offset: 0x001565A4
		private void RenderTransformerConfigurationHeader(HtmlTextWriter writer)
		{
			if (this.EnsurePendingData())
			{
				bool flag = this._pendingConsumer == this.WebPartToConnect;
				string displayTitle;
				string displayName;
				if (this._pendingConnectionType == ConnectionsZone.ConnectionType.Consumer && flag)
				{
					displayTitle = this._pendingProvider.DisplayTitle;
					displayName = this._pendingConsumerConnectionPoint.DisplayName;
				}
				else
				{
					displayTitle = this._pendingConsumer.DisplayTitle;
					displayName = this._pendingProviderConnectionPoint.DisplayName;
				}
				Label label = new Label();
				label.Page = this.Page;
				label.ApplyStyle(base.LabelStyle);
				label.Text = (flag ? this.ConnectToProviderTitle : this.ConnectToConsumerTitle);
				label.RenderControl(writer);
				writer.WriteBreak();
				writer.WriteBreak();
				label.ApplyStyle(base.InstructionTextStyle);
				label.Text = (flag ? this.ConnectToProviderInstructionText : this.ConnectToConsumerInstructionText);
				label.RenderControl(writer);
				writer.WriteBreak();
				writer.WriteBreak();
				writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				label.ApplyStyle(base.LabelStyle);
				label.Text = (flag ? this.GetText : this.SendText);
				label.RenderControl(writer);
				writer.RenderEndTag();
				base.LabelStyle.AddAttributesToRender(writer, this);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.WriteEncodedText(displayName);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				label.Text = (flag ? this.GetFromText : this.SendToText);
				label.RenderControl(writer);
				writer.RenderEndTag();
				base.LabelStyle.AddAttributesToRender(writer, this);
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				writer.WriteEncodedText(displayTitle);
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.WriteBreak();
				writer.RenderBeginTag(HtmlTextWriterTag.Hr);
				writer.RenderEndTag();
				writer.WriteBreak();
				label.ApplyStyle(base.LabelStyle);
				label.Text = this.ConfigureConnectionTitle;
				label.RenderControl(writer);
				writer.WriteBreak();
				writer.WriteBreak();
			}
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x001577B0 File Offset: 0x001567B0
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			base.RenderVerbsInternal(writer, new WebPartVerb[]
			{
				this.CloseVerb
			});
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x001577D5 File Offset: 0x001567D5
		private void Reset()
		{
			this.ClearPendingConnection();
			base.ChildControlsCreated = false;
			this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x001577EC File Offset: 0x001567EC
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			if (this._mode != ConnectionsZone.ConnectionsZoneMode.ExistingConnections || obj != null)
			{
				return new object[]
				{
					obj,
					this._mode,
					this._pendingConnectionPointID,
					this._pendingConnectionType,
					this._pendingSelectedValue,
					this._pendingConsumerID,
					this._pendingTransformerConfigurationControlTypeName,
					this._pendingConnectionID
				};
			}
			return null;
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x00157864 File Offset: 0x00156864
		protected override object SaveViewState()
		{
			object[] array = new object[]
			{
				base.SaveViewState(),
				(this._cancelVerb != null) ? ((IStateManager)this._cancelVerb).SaveViewState() : null,
				(this._closeVerb != null) ? ((IStateManager)this._closeVerb).SaveViewState() : null,
				(this._configureVerb != null) ? ((IStateManager)this._configureVerb).SaveViewState() : null,
				(this._connectVerb != null) ? ((IStateManager)this._connectVerb).SaveViewState() : null,
				(this._disconnectVerb != null) ? ((IStateManager)this._disconnectVerb).SaveViewState() : null
			};
			for (int i = 0; i < 6; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x00157914 File Offset: 0x00156914
		private void SelectValueInList(ListControl list, string value)
		{
			if (list == null)
			{
				this.DisplayConnectionError();
				return;
			}
			ListItem listItem = list.Items.FindByValue(value);
			if (listItem != null)
			{
				listItem.Selected = true;
				return;
			}
			this.DisplayConnectionError();
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0015794C File Offset: 0x0015694C
		private void SetDropDownProperties()
		{
			bool flag = false;
			WebPart webPartToConnect = this.WebPartToConnect;
			if (webPartToConnect != null && !webPartToConnect.IsClosed)
			{
				WebPartCollection webParts = base.WebPartManager.WebParts;
				ProviderConnectionPointCollection enabledProviderConnectionPoints = base.WebPartManager.GetEnabledProviderConnectionPoints(webPartToConnect);
				foreach (object obj in enabledProviderConnectionPoints)
				{
					ProviderConnectionPoint providerConnectionPoint = (ProviderConnectionPoint)obj;
					DropDownList dropDownList = (DropDownList)this._connectDropDownLists[providerConnectionPoint];
					if (dropDownList != null)
					{
						dropDownList.Items.Clear();
						dropDownList.SelectedIndex = 0;
						IDictionary validConsumers = this.GetValidConsumers(webPartToConnect, providerConnectionPoint, webParts);
						if (validConsumers.Count == 0)
						{
							dropDownList.Enabled = false;
							dropDownList.Items.Add(new ListItem(SR.GetString("ConnectionsZone_NoConsumers"), string.Empty));
						}
						else
						{
							dropDownList.Enabled = true;
							dropDownList.Items.Add(new ListItem());
							this._connectionPointInfo[providerConnectionPoint] = validConsumers;
							WebPartConnection webPartConnection = providerConnectionPoint.AllowsMultipleConnections ? null : base.WebPartManager.GetConnectionForProvider(webPartToConnect, providerConnectionPoint);
							WebPart webPart = null;
							ConsumerConnectionPoint consumerConnectionPoint = null;
							if (webPartConnection != null)
							{
								webPart = webPartConnection.Consumer;
								consumerConnectionPoint = webPartConnection.ConsumerConnectionPoint;
								dropDownList.Enabled = false;
							}
							else
							{
								flag = true;
							}
							foreach (object obj2 in validConsumers)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
								ConnectionsZone.ConsumerInfo consumerInfo = (ConnectionsZone.ConsumerInfo)dictionaryEntry.Value;
								ListItem listItem = new ListItem();
								listItem.Text = this.GetDisplayTitle(consumerInfo.WebPart, consumerInfo.ConnectionPoint, true);
								listItem.Value = (string)dictionaryEntry.Key;
								if (webPartConnection != null && consumerInfo.WebPart == webPart && consumerInfo.ConnectionPoint == consumerConnectionPoint)
								{
									listItem.Selected = true;
								}
								dropDownList.Items.Add(listItem);
							}
						}
					}
				}
				ConsumerConnectionPointCollection enabledConsumerConnectionPoints = base.WebPartManager.GetEnabledConsumerConnectionPoints(webPartToConnect);
				foreach (object obj3 in enabledConsumerConnectionPoints)
				{
					ConsumerConnectionPoint consumerConnectionPoint2 = (ConsumerConnectionPoint)obj3;
					DropDownList dropDownList2 = (DropDownList)this._connectDropDownLists[consumerConnectionPoint2];
					if (dropDownList2 != null)
					{
						dropDownList2.Items.Clear();
						dropDownList2.SelectedIndex = 0;
						IDictionary validProviders = this.GetValidProviders(webPartToConnect, consumerConnectionPoint2, webParts);
						if (validProviders.Count == 0)
						{
							dropDownList2.Enabled = false;
							dropDownList2.Items.Add(new ListItem(SR.GetString("ConnectionsZone_NoProviders"), string.Empty));
						}
						else
						{
							dropDownList2.Enabled = true;
							dropDownList2.Items.Add(new ListItem());
							this._connectionPointInfo[consumerConnectionPoint2] = validProviders;
							WebPartConnection webPartConnection2 = consumerConnectionPoint2.AllowsMultipleConnections ? null : base.WebPartManager.GetConnectionForConsumer(webPartToConnect, consumerConnectionPoint2);
							WebPart webPart2 = null;
							ProviderConnectionPoint providerConnectionPoint2 = null;
							if (webPartConnection2 != null)
							{
								webPart2 = webPartConnection2.Provider;
								providerConnectionPoint2 = webPartConnection2.ProviderConnectionPoint;
								dropDownList2.Enabled = false;
							}
							else
							{
								flag = true;
							}
							foreach (object obj4 in validProviders)
							{
								DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj4;
								ConnectionsZone.ProviderInfo providerInfo = (ConnectionsZone.ProviderInfo)dictionaryEntry2.Value;
								ListItem listItem2 = new ListItem();
								listItem2.Text = this.GetDisplayTitle(providerInfo.WebPart, providerInfo.ConnectionPoint, false);
								listItem2.Value = (string)dictionaryEntry2.Key;
								if (webPartConnection2 != null && providerInfo.WebPart == webPart2 && providerInfo.ConnectionPoint == providerConnectionPoint2)
								{
									listItem2.Selected = true;
								}
								dropDownList2.Items.Add(listItem2);
							}
						}
					}
				}
				if (this._pendingConnectionType == ConnectionsZone.ConnectionType.Consumer && this._pendingSelectedValue != null && this._pendingSelectedValue.Length > 0)
				{
					this.EnsurePendingData();
					if (this._pendingConsumerConnectionPoint == null)
					{
						this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
						return;
					}
					DropDownList dropDownList3 = (DropDownList)this._connectDropDownLists[this._pendingConsumerConnectionPoint];
					if (dropDownList3 == null)
					{
						this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
						return;
					}
					this.SelectValueInList(dropDownList3, this._pendingSelectedValue);
				}
				else if (this._pendingConnectionType == ConnectionsZone.ConnectionType.Provider)
				{
					this.EnsurePendingData();
					if (this._pendingProviderConnectionPoint == null)
					{
						this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
						return;
					}
					DropDownList dropDownList4 = (DropDownList)this._connectDropDownLists[this._pendingProviderConnectionPoint];
					if (dropDownList4 == null)
					{
						this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
						return;
					}
					this.SelectValueInList(dropDownList4, this._pendingSelectedValue);
				}
				if (!flag && (this._mode == ConnectionsZone.ConnectionsZoneMode.ConnectToConsumer || this._mode == ConnectionsZone.ConnectionsZoneMode.ConnectToProvider))
				{
					this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
				}
			}
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00157E78 File Offset: 0x00156E78
		private void SetTransformerConfigurationControlProperties()
		{
			if (this.EnsurePendingData())
			{
				Control control = this._pendingProvider.ToControl();
				Control control2 = this._pendingConsumer.ToControl();
				object @object = this._pendingProviderConnectionPoint.GetObject(control);
				object data = this._pendingTransformer.Transform(@object);
				this._pendingConsumerConnectionPoint.SetObject(control2, data);
				if ((this._pendingConnectionType == ConnectionsZone.ConnectionType.Consumer && (string.IsNullOrEmpty(this._pendingConnectionID) || this._pendingConsumerConnectionPoint.AllowsMultipleConnections)) || this._pendingConnectionType == ConnectionsZone.ConnectionType.Provider)
				{
					this._pendingConsumerConnectionPoint.SetObject(control2, null);
				}
			}
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x00157F08 File Offset: 0x00156F08
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._cancelVerb != null)
			{
				((IStateManager)this._cancelVerb).TrackViewState();
			}
			if (this._closeVerb != null)
			{
				((IStateManager)this._closeVerb).TrackViewState();
			}
			if (this._configureVerb != null)
			{
				((IStateManager)this._configureVerb).TrackViewState();
			}
			if (this._connectVerb != null)
			{
				((IStateManager)this._connectVerb).TrackViewState();
			}
			if (this._disconnectVerb != null)
			{
				((IStateManager)this._disconnectVerb).TrackViewState();
			}
		}

		// Token: 0x04002EA3 RID: 11939
		private const int baseIndex = 0;

		// Token: 0x04002EA4 RID: 11940
		private const int cancelVerbIndex = 1;

		// Token: 0x04002EA5 RID: 11941
		private const int closeVerbIndex = 2;

		// Token: 0x04002EA6 RID: 11942
		private const int configureVerbIndex = 3;

		// Token: 0x04002EA7 RID: 11943
		private const int connectVerbIndex = 4;

		// Token: 0x04002EA8 RID: 11944
		private const int disconnectVerbIndex = 5;

		// Token: 0x04002EA9 RID: 11945
		private const int viewStateArrayLength = 6;

		// Token: 0x04002EAA RID: 11946
		private const int modeIndex = 1;

		// Token: 0x04002EAB RID: 11947
		private const int pendingConnectionPointIDIndex = 2;

		// Token: 0x04002EAC RID: 11948
		private const int pendingConnectionTypeIndex = 3;

		// Token: 0x04002EAD RID: 11949
		private const int pendingSelectedValueIndex = 4;

		// Token: 0x04002EAE RID: 11950
		private const int pendingConsumerIDIndex = 5;

		// Token: 0x04002EAF RID: 11951
		private const int pendingTransformerTypeNameIndex = 6;

		// Token: 0x04002EB0 RID: 11952
		private const int pendingConnectionIDIndex = 7;

		// Token: 0x04002EB1 RID: 11953
		private const int controlStateArrayLength = 8;

		// Token: 0x04002EB2 RID: 11954
		private const string connectEventArgument = "connect";

		// Token: 0x04002EB3 RID: 11955
		private const string connectConsumerEventArgument = "connectconsumer";

		// Token: 0x04002EB4 RID: 11956
		private const string connectProviderEventArgument = "connectprovider";

		// Token: 0x04002EB5 RID: 11957
		private const string providerEventArgument = "provider";

		// Token: 0x04002EB6 RID: 11958
		private const string consumerEventArgument = "consumer";

		// Token: 0x04002EB7 RID: 11959
		private const string disconnectEventArgument = "disconnect";

		// Token: 0x04002EB8 RID: 11960
		private const string configureEventArgument = "edit";

		// Token: 0x04002EB9 RID: 11961
		private const string closeEventArgument = "close";

		// Token: 0x04002EBA RID: 11962
		private const string cancelEventArgument = "cancel";

		// Token: 0x04002EBB RID: 11963
		private const string providerListIdPrefix = "_providerlist_";

		// Token: 0x04002EBC RID: 11964
		private const string consumerListIdPrefix = "_consumerlist_";

		// Token: 0x04002EBD RID: 11965
		private WebPartVerb _closeVerb;

		// Token: 0x04002EBE RID: 11966
		private WebPartVerb _connectVerb;

		// Token: 0x04002EBF RID: 11967
		private WebPartVerb _disconnectVerb;

		// Token: 0x04002EC0 RID: 11968
		private WebPartVerb _configureVerb;

		// Token: 0x04002EC1 RID: 11969
		private WebPartVerb _cancelVerb;

		// Token: 0x04002EC2 RID: 11970
		private IDictionary _connectDropDownLists;

		// Token: 0x04002EC3 RID: 11971
		private ArrayList _availableTransformers;

		// Token: 0x04002EC4 RID: 11972
		private WebPartTransformer _pendingTransformer;

		// Token: 0x04002EC5 RID: 11973
		private Control _pendingTransformerConfigurationControl;

		// Token: 0x04002EC6 RID: 11974
		private bool _displayErrorMessage;

		// Token: 0x04002EC7 RID: 11975
		private WebPart _pendingConsumer;

		// Token: 0x04002EC8 RID: 11976
		private WebPart _pendingProvider;

		// Token: 0x04002EC9 RID: 11977
		private ConsumerConnectionPoint _pendingConsumerConnectionPoint;

		// Token: 0x04002ECA RID: 11978
		private ProviderConnectionPoint _pendingProviderConnectionPoint;

		// Token: 0x04002ECB RID: 11979
		private IDictionary _connectionPointInfo;

		// Token: 0x04002ECC RID: 11980
		private ConnectionsZone.ConnectionsZoneMode _mode;

		// Token: 0x04002ECD RID: 11981
		private string _pendingConnectionPointID;

		// Token: 0x04002ECE RID: 11982
		private ConnectionsZone.ConnectionType _pendingConnectionType;

		// Token: 0x04002ECF RID: 11983
		private string _pendingSelectedValue;

		// Token: 0x04002ED0 RID: 11984
		private string _pendingConsumerID;

		// Token: 0x04002ED1 RID: 11985
		private string _pendingTransformerConfigurationControlTypeName;

		// Token: 0x04002ED2 RID: 11986
		private string _pendingConnectionID;

		// Token: 0x020006B3 RID: 1715
		private abstract class ConnectionPointInfo
		{
			// Token: 0x0600545A RID: 21594 RVA: 0x00157F7A File Offset: 0x00156F7A
			protected ConnectionPointInfo(WebPart webPart)
			{
				this._webPart = webPart;
			}

			// Token: 0x0600545B RID: 21595 RVA: 0x00157F89 File Offset: 0x00156F89
			protected ConnectionPointInfo(WebPart webPart, Type transformerType) : this(webPart)
			{
				this._transformerType = transformerType;
			}

			// Token: 0x1700158C RID: 5516
			// (get) Token: 0x0600545C RID: 21596 RVA: 0x00157F99 File Offset: 0x00156F99
			public Type TransformerType
			{
				get
				{
					return this._transformerType;
				}
			}

			// Token: 0x1700158D RID: 5517
			// (get) Token: 0x0600545D RID: 21597 RVA: 0x00157FA1 File Offset: 0x00156FA1
			public WebPart WebPart
			{
				get
				{
					return this._webPart;
				}
			}

			// Token: 0x04002ED3 RID: 11987
			private WebPart _webPart;

			// Token: 0x04002ED4 RID: 11988
			private Type _transformerType;
		}

		// Token: 0x020006B4 RID: 1716
		private sealed class ConsumerInfo : ConnectionsZone.ConnectionPointInfo
		{
			// Token: 0x0600545E RID: 21598 RVA: 0x00157FA9 File Offset: 0x00156FA9
			public ConsumerInfo(WebPart webPart, ConsumerConnectionPoint connectionPoint) : base(webPart)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x0600545F RID: 21599 RVA: 0x00157FB9 File Offset: 0x00156FB9
			public ConsumerInfo(WebPart webPart, ConsumerConnectionPoint connectionPoint, Type transformerType) : base(webPart, transformerType)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x1700158E RID: 5518
			// (get) Token: 0x06005460 RID: 21600 RVA: 0x00157FCA File Offset: 0x00156FCA
			public ConsumerConnectionPoint ConnectionPoint
			{
				get
				{
					return this._connectionPoint;
				}
			}

			// Token: 0x04002ED5 RID: 11989
			private ConsumerConnectionPoint _connectionPoint;
		}

		// Token: 0x020006B5 RID: 1717
		private sealed class ProviderInfo : ConnectionsZone.ConnectionPointInfo
		{
			// Token: 0x06005461 RID: 21601 RVA: 0x00157FD2 File Offset: 0x00156FD2
			public ProviderInfo(WebPart webPart, ProviderConnectionPoint connectionPoint) : base(webPart)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x06005462 RID: 21602 RVA: 0x00157FE2 File Offset: 0x00156FE2
			public ProviderInfo(WebPart webPart, ProviderConnectionPoint connectionPoint, Type transformerType) : base(webPart, transformerType)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x1700158F RID: 5519
			// (get) Token: 0x06005463 RID: 21603 RVA: 0x00157FF3 File Offset: 0x00156FF3
			public ProviderConnectionPoint ConnectionPoint
			{
				get
				{
					return this._connectionPoint;
				}
			}

			// Token: 0x04002ED6 RID: 11990
			private ProviderConnectionPoint _connectionPoint;
		}

		// Token: 0x020006B6 RID: 1718
		private enum ConnectionType
		{
			// Token: 0x04002ED8 RID: 11992
			None,
			// Token: 0x04002ED9 RID: 11993
			Consumer,
			// Token: 0x04002EDA RID: 11994
			Provider
		}

		// Token: 0x020006B7 RID: 1719
		private enum ConnectionsZoneMode
		{
			// Token: 0x04002EDC RID: 11996
			ExistingConnections,
			// Token: 0x04002EDD RID: 11997
			ConnectToConsumer,
			// Token: 0x04002EDE RID: 11998
			ConnectToProvider,
			// Token: 0x04002EDF RID: 11999
			ConfiguringTransformer
		}
	}
}
