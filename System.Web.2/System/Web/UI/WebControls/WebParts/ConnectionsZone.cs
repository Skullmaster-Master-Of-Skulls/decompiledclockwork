using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000534 RID: 1332
	[Designer("System.Web.UI.Design.WebControls.WebParts.ConnectionsZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class ConnectionsZone : ToolZone
	{
		// Token: 0x0600438D RID: 17293 RVA: 0x000DE4D8 File Offset: 0x000DC6D8
		public ConnectionsZone() : base(WebPartManager.ConnectDisplayMode)
		{
			this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
			this._pendingConnectionPointID = string.Empty;
			this._pendingConnectionType = ConnectionsZone.ConnectionType.None;
			this._pendingSelectedValue = null;
			this._pendingConsumerID = string.Empty;
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x0600438E RID: 17294 RVA: 0x000DE510 File Offset: 0x000DC710
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

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x0600438F RID: 17295 RVA: 0x000DE594 File Offset: 0x000DC794
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("ConnectionsZone_CancelVerb")]
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

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x000DE5C2 File Offset: 0x000DC7C2
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("ConnectionsZone_CloseVerb")]
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

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06004391 RID: 17297 RVA: 0x000DE600 File Offset: 0x000DC800
		// (set) Token: 0x06004392 RID: 17298 RVA: 0x000DE632 File Offset: 0x000DC832
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConfigureConnectionTitleDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConfigureConnectionTitle")]
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

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06004393 RID: 17299 RVA: 0x000DE645 File Offset: 0x000DC845
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
		[WebSysDescription("ConnectionsZone_ConfigureVerb")]
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

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x000DE674 File Offset: 0x000DC874
		// (set) Token: 0x06004395 RID: 17301 RVA: 0x000DE6A6 File Offset: 0x000DC8A6
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToConsumerInstructionTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConnectToConsumerInstructionText")]
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

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x000DE6BC File Offset: 0x000DC8BC
		// (set) Token: 0x06004397 RID: 17303 RVA: 0x000DE6EE File Offset: 0x000DC8EE
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

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06004398 RID: 17304 RVA: 0x000DE704 File Offset: 0x000DC904
		// (set) Token: 0x06004399 RID: 17305 RVA: 0x000DE736 File Offset: 0x000DC936
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToConsumerTitleDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConnectToConsumerTitle")]
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

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x0600439A RID: 17306 RVA: 0x000DE74C File Offset: 0x000DC94C
		// (set) Token: 0x0600439B RID: 17307 RVA: 0x000DE77E File Offset: 0x000DC97E
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToProviderInstructionTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConnectToProviderInstructionText")]
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

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x0600439C RID: 17308 RVA: 0x000DE794 File Offset: 0x000DC994
		// (set) Token: 0x0600439D RID: 17309 RVA: 0x000DE7C6 File Offset: 0x000DC9C6
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToProviderTextDescription")]
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

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x0600439E RID: 17310 RVA: 0x000DE7DC File Offset: 0x000DC9DC
		// (set) Token: 0x0600439F RID: 17311 RVA: 0x000DE80E File Offset: 0x000DCA0E
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConnectToProviderTitleDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConnectToProviderTitle")]
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

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060043A0 RID: 17312 RVA: 0x000DE821 File Offset: 0x000DCA21
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Verbs")]
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

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x000DE860 File Offset: 0x000DCA60
		// (set) Token: 0x060043A2 RID: 17314 RVA: 0x000DE892 File Offset: 0x000DCA92
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConsumersTitleDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConsumersTitle")]
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

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060043A3 RID: 17315 RVA: 0x000DE8A8 File Offset: 0x000DCAA8
		// (set) Token: 0x060043A4 RID: 17316 RVA: 0x000DE8DA File Offset: 0x000DCADA
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ConsumersInstructionTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_ConsumersInstructionText")]
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

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060043A5 RID: 17317 RVA: 0x000DE8ED File Offset: 0x000DCAED
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x000DE91B File Offset: 0x000DCB1B
		protected override bool Display
		{
			get
			{
				return base.Display && this.WebPartToConnect != null;
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000DE930 File Offset: 0x000DCB30
		// (set) Token: 0x060043A8 RID: 17320 RVA: 0x000DE938 File Offset: 0x000DCB38
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
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

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x000DE944 File Offset: 0x000DCB44
		// (set) Token: 0x060043AA RID: 17322 RVA: 0x000DE976 File Offset: 0x000DCB76
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_WarningMessage")]
		[WebSysDefaultValue("ConnectionsZone_WarningConnectionDisabled")]
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

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x000DE98C File Offset: 0x000DCB8C
		// (set) Token: 0x060043AC RID: 17324 RVA: 0x000DE9BE File Offset: 0x000DCBBE
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_GetDescription")]
		[WebSysDefaultValue("ConnectionsZone_Get")]
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

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000DE9D4 File Offset: 0x000DCBD4
		// (set) Token: 0x060043AE RID: 17326 RVA: 0x000DEA06 File Offset: 0x000DCC06
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_GetFromTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_GetFromText")]
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

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x000DEA1C File Offset: 0x000DCC1C
		// (set) Token: 0x060043B0 RID: 17328 RVA: 0x000A0A1D File Offset: 0x0009EC1D
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_HeaderTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_HeaderText")]
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

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x000DEA50 File Offset: 0x000DCC50
		// (set) Token: 0x060043B2 RID: 17330 RVA: 0x0008B81D File Offset: 0x00089A1D
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_InstructionTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_InstructionText")]
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

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x000DEA84 File Offset: 0x000DCC84
		// (set) Token: 0x060043B4 RID: 17332 RVA: 0x000DEAB6 File Offset: 0x000DCCB6
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_InstructionTitleDescription")]
		[WebSysDefaultValue("ConnectionsZone_InstructionTitle")]
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

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x060043B5 RID: 17333 RVA: 0x000DEACC File Offset: 0x000DCCCC
		// (set) Token: 0x060043B6 RID: 17334 RVA: 0x000DEAFE File Offset: 0x000DCCFE
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

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x060043B7 RID: 17335 RVA: 0x000DEB14 File Offset: 0x000DCD14
		// (set) Token: 0x060043B8 RID: 17336 RVA: 0x000DEB46 File Offset: 0x000DCD46
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_NoExistingConnectionInstructionTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_NoExistingConnectionInstructionText")]
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

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x060043B9 RID: 17337 RVA: 0x000DEB5C File Offset: 0x000DCD5C
		// (set) Token: 0x060043BA RID: 17338 RVA: 0x000DEB8E File Offset: 0x000DCD8E
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_NoExistingConnectionTitleDescription")]
		[WebSysDefaultValue("ConnectionsZone_NoExistingConnectionTitle")]
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

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x000DEBA1 File Offset: 0x000DCDA1
		// (set) Token: 0x060043BC RID: 17340 RVA: 0x000DEBA9 File Offset: 0x000DCDA9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Themeable(false)]
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

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x000DEBB4 File Offset: 0x000DCDB4
		// (set) Token: 0x060043BE RID: 17342 RVA: 0x000DEBE6 File Offset: 0x000DCDE6
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ProvidersTitleDescription")]
		[WebSysDefaultValue("ConnectionsZone_ProvidersTitle")]
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

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x000DEBFC File Offset: 0x000DCDFC
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x000DEC2E File Offset: 0x000DCE2E
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_ProvidersInstructionTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_ProvidersInstructionText")]
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

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x000DEC44 File Offset: 0x000DCE44
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x000DEC76 File Offset: 0x000DCE76
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_SendTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_SendText")]
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

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x000DEC8C File Offset: 0x000DCE8C
		// (set) Token: 0x060043C4 RID: 17348 RVA: 0x000DECBE File Offset: 0x000DCEBE
		[WebCategory("Appearance")]
		[WebSysDescription("ConnectionsZone_SendToTextDescription")]
		[WebSysDefaultValue("ConnectionsZone_SendToText")]
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

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x000DECD1 File Offset: 0x000DCED1
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

		// Token: 0x060043C6 RID: 17350 RVA: 0x000DECFA File Offset: 0x000DCEFA
		protected override void Close()
		{
			if (this.WebPartToConnect != null)
			{
				base.WebPartManager.EndWebPartConnecting();
			}
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000DED10 File Offset: 0x000DCF10
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

		// Token: 0x060043C8 RID: 17352 RVA: 0x000DED6C File Offset: 0x000DCF6C
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

		// Token: 0x060043C9 RID: 17353 RVA: 0x000DEF44 File Offset: 0x000DD144
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

		// Token: 0x060043CA RID: 17354 RVA: 0x000DF11C File Offset: 0x000DD31C
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
						Control control = this._pendingProvider.ToControl();
						Control control2 = this._pendingConsumer.ToControl();
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
					Control control3 = this._pendingProvider.ToControl();
					Control control4 = this._pendingConsumer.ToControl();
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

		// Token: 0x060043CB RID: 17355 RVA: 0x000DF468 File Offset: 0x000DD668
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

		// Token: 0x060043CC RID: 17356 RVA: 0x000DF6C4 File Offset: 0x000DD8C4
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

		// Token: 0x060043CD RID: 17357 RVA: 0x000DF720 File Offset: 0x000DD920
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

		// Token: 0x060043CE RID: 17358 RVA: 0x000DF784 File Offset: 0x000DD984
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

		// Token: 0x060043CF RID: 17359 RVA: 0x000DF7FC File Offset: 0x000DD9FC
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
							hybridDictionary.Add(webPart.ID + "$" + consumerConnectionPoint.ID, new ConnectionsZone.ConsumerInfo(webPart, consumerConnectionPoint));
						}
						else
						{
							foreach (object obj3 in this.AvailableTransformers)
							{
								WebPartTransformer webPartTransformer = (WebPartTransformer)obj3;
								if (base.WebPartManager.CanConnectWebParts(provider, providerConnectionPoint, webPart, consumerConnectionPoint, webPartTransformer))
								{
									hybridDictionary.Add(webPart.ID + "$" + consumerConnectionPoint.ID, new ConnectionsZone.ConsumerInfo(webPart, consumerConnectionPoint, webPartTransformer.GetType()));
									break;
								}
							}
						}
					}
				}
			}
			return hybridDictionary;
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x000DF9D8 File Offset: 0x000DDBD8
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
							hybridDictionary.Add(webPart.ID + "$" + providerConnectionPoint.ID, new ConnectionsZone.ProviderInfo(webPart, providerConnectionPoint));
						}
						else
						{
							foreach (object obj3 in this.AvailableTransformers)
							{
								WebPartTransformer webPartTransformer = (WebPartTransformer)obj3;
								if (base.WebPartManager.CanConnectWebParts(webPart, providerConnectionPoint, consumer, consumerConnectionPoint, webPartTransformer))
								{
									hybridDictionary.Add(webPart.ID + "$" + providerConnectionPoint.ID, new ConnectionsZone.ProviderInfo(webPart, providerConnectionPoint, webPartTransformer.GetType()));
									break;
								}
							}
						}
					}
				}
			}
			return hybridDictionary;
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x000DFBB4 File Offset: 0x000DDDB4
		private bool HasConfigurationControl(WebPartTransformer transformer)
		{
			return transformer.CreateConfigurationControl() != null;
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x000DFBC0 File Offset: 0x000DDDC0
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

		// Token: 0x060043D3 RID: 17363 RVA: 0x000DFC88 File Offset: 0x000DDE88
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

		// Token: 0x060043D4 RID: 17364 RVA: 0x000DFD25 File Offset: 0x000DDF25
		private void OnConfigurationControlCancelled(object sender, EventArgs e)
		{
			this.Reset();
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x000DFD2D File Offset: 0x000DDF2D
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
				this.Page.PreRenderComplete += this.OnPagePreRenderComplete;
			}
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x000DFD64 File Offset: 0x000DDF64
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

		// Token: 0x060043D7 RID: 17367 RVA: 0x000DFE03 File Offset: 0x000DE003
		protected override void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			this.Reset();
			base.OnDisplayModeChanged(sender, e);
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x000DFE13 File Offset: 0x000DE013
		private void OnPagePreRenderComplete(object sender, EventArgs e)
		{
			this.SetTransformerConfigurationControlProperties();
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x000DFE1B File Offset: 0x000DE01B
		protected override void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			if (base.WebPartManager != null && base.WebPartManager.DisplayMode == WebPartManager.ConnectDisplayMode)
			{
				this.Reset();
			}
			base.OnSelectedWebPartChanged(sender, e);
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000DFE45 File Offset: 0x000DE045
		private void DisplayConnectionError()
		{
			this._displayErrorMessage = true;
			this.Reset();
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000DFE54 File Offset: 0x000DE054
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

		// Token: 0x060043DC RID: 17372 RVA: 0x000DFFF3 File Offset: 0x000DE1F3
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			this.SetDropDownProperties();
			base.Render(writer);
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000E0018 File Offset: 0x000DE218
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

		// Token: 0x060043DE RID: 17374 RVA: 0x000E01E8 File Offset: 0x000DE3E8
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

		// Token: 0x060043DF RID: 17375 RVA: 0x000E02CC File Offset: 0x000DE4CC
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

		// Token: 0x060043E0 RID: 17376 RVA: 0x000E0594 File Offset: 0x000DE794
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

		// Token: 0x060043E1 RID: 17377 RVA: 0x000E085C File Offset: 0x000DEA5C
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

		// Token: 0x060043E2 RID: 17378 RVA: 0x000E08A4 File Offset: 0x000DEAA4
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

		// Token: 0x060043E3 RID: 17379 RVA: 0x000E0AF4 File Offset: 0x000DECF4
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

		// Token: 0x060043E4 RID: 17380 RVA: 0x000E0C84 File Offset: 0x000DEE84
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

		// Token: 0x060043E5 RID: 17381 RVA: 0x000E0D70 File Offset: 0x000DEF70
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

		// Token: 0x060043E6 RID: 17382 RVA: 0x000E0E5C File Offset: 0x000DF05C
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

		// Token: 0x060043E7 RID: 17383 RVA: 0x000E0EB0 File Offset: 0x000DF0B0
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

		// Token: 0x060043E8 RID: 17384 RVA: 0x000E0F40 File Offset: 0x000DF140
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

		// Token: 0x060043E9 RID: 17385 RVA: 0x000E0FDC File Offset: 0x000DF1DC
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

		// Token: 0x060043EA RID: 17386 RVA: 0x000E11E7 File Offset: 0x000DF3E7
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			base.RenderVerbsInternal(writer, new WebPartVerb[]
			{
				this.CloseVerb
			});
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x000E11FF File Offset: 0x000DF3FF
		private void Reset()
		{
			this.ClearPendingConnection();
			base.ChildControlsCreated = false;
			this._mode = ConnectionsZone.ConnectionsZoneMode.ExistingConnections;
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x000E1218 File Offset: 0x000DF418
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

		// Token: 0x060043ED RID: 17389 RVA: 0x000E1290 File Offset: 0x000DF490
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

		// Token: 0x060043EE RID: 17390 RVA: 0x000E1340 File Offset: 0x000DF540
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

		// Token: 0x060043EF RID: 17391 RVA: 0x000E1378 File Offset: 0x000DF578
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

		// Token: 0x060043F0 RID: 17392 RVA: 0x000E18A4 File Offset: 0x000DFAA4
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

		// Token: 0x060043F1 RID: 17393 RVA: 0x000E1934 File Offset: 0x000DFB34
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

		// Token: 0x040025EC RID: 9708
		private const int baseIndex = 0;

		// Token: 0x040025ED RID: 9709
		private const int cancelVerbIndex = 1;

		// Token: 0x040025EE RID: 9710
		private const int closeVerbIndex = 2;

		// Token: 0x040025EF RID: 9711
		private const int configureVerbIndex = 3;

		// Token: 0x040025F0 RID: 9712
		private const int connectVerbIndex = 4;

		// Token: 0x040025F1 RID: 9713
		private const int disconnectVerbIndex = 5;

		// Token: 0x040025F2 RID: 9714
		private const int viewStateArrayLength = 6;

		// Token: 0x040025F3 RID: 9715
		private const int modeIndex = 1;

		// Token: 0x040025F4 RID: 9716
		private const int pendingConnectionPointIDIndex = 2;

		// Token: 0x040025F5 RID: 9717
		private const int pendingConnectionTypeIndex = 3;

		// Token: 0x040025F6 RID: 9718
		private const int pendingSelectedValueIndex = 4;

		// Token: 0x040025F7 RID: 9719
		private const int pendingConsumerIDIndex = 5;

		// Token: 0x040025F8 RID: 9720
		private const int pendingTransformerTypeNameIndex = 6;

		// Token: 0x040025F9 RID: 9721
		private const int pendingConnectionIDIndex = 7;

		// Token: 0x040025FA RID: 9722
		private const int controlStateArrayLength = 8;

		// Token: 0x040025FB RID: 9723
		private WebPartVerb _closeVerb;

		// Token: 0x040025FC RID: 9724
		private WebPartVerb _connectVerb;

		// Token: 0x040025FD RID: 9725
		private WebPartVerb _disconnectVerb;

		// Token: 0x040025FE RID: 9726
		private WebPartVerb _configureVerb;

		// Token: 0x040025FF RID: 9727
		private WebPartVerb _cancelVerb;

		// Token: 0x04002600 RID: 9728
		private const string connectEventArgument = "connect";

		// Token: 0x04002601 RID: 9729
		private const string connectConsumerEventArgument = "connectconsumer";

		// Token: 0x04002602 RID: 9730
		private const string connectProviderEventArgument = "connectprovider";

		// Token: 0x04002603 RID: 9731
		private const string providerEventArgument = "provider";

		// Token: 0x04002604 RID: 9732
		private const string consumerEventArgument = "consumer";

		// Token: 0x04002605 RID: 9733
		private const string disconnectEventArgument = "disconnect";

		// Token: 0x04002606 RID: 9734
		private const string configureEventArgument = "edit";

		// Token: 0x04002607 RID: 9735
		private const string closeEventArgument = "close";

		// Token: 0x04002608 RID: 9736
		private const string cancelEventArgument = "cancel";

		// Token: 0x04002609 RID: 9737
		private const string providerListIdPrefix = "_providerlist_";

		// Token: 0x0400260A RID: 9738
		private const string consumerListIdPrefix = "_consumerlist_";

		// Token: 0x0400260B RID: 9739
		private IDictionary _connectDropDownLists;

		// Token: 0x0400260C RID: 9740
		private ArrayList _availableTransformers;

		// Token: 0x0400260D RID: 9741
		private WebPartTransformer _pendingTransformer;

		// Token: 0x0400260E RID: 9742
		private Control _pendingTransformerConfigurationControl;

		// Token: 0x0400260F RID: 9743
		private bool _displayErrorMessage;

		// Token: 0x04002610 RID: 9744
		private WebPart _pendingConsumer;

		// Token: 0x04002611 RID: 9745
		private WebPart _pendingProvider;

		// Token: 0x04002612 RID: 9746
		private ConsumerConnectionPoint _pendingConsumerConnectionPoint;

		// Token: 0x04002613 RID: 9747
		private ProviderConnectionPoint _pendingProviderConnectionPoint;

		// Token: 0x04002614 RID: 9748
		private IDictionary _connectionPointInfo;

		// Token: 0x04002615 RID: 9749
		private ConnectionsZone.ConnectionsZoneMode _mode;

		// Token: 0x04002616 RID: 9750
		private string _pendingConnectionPointID;

		// Token: 0x04002617 RID: 9751
		private ConnectionsZone.ConnectionType _pendingConnectionType;

		// Token: 0x04002618 RID: 9752
		private string _pendingSelectedValue;

		// Token: 0x04002619 RID: 9753
		private string _pendingConsumerID;

		// Token: 0x0400261A RID: 9754
		private string _pendingTransformerConfigurationControlTypeName;

		// Token: 0x0400261B RID: 9755
		private string _pendingConnectionID;

		// Token: 0x020009E7 RID: 2535
		private abstract class ConnectionPointInfo
		{
			// Token: 0x06006D0D RID: 27917 RVA: 0x001867C9 File Offset: 0x001849C9
			protected ConnectionPointInfo(WebPart webPart)
			{
				this._webPart = webPart;
			}

			// Token: 0x06006D0E RID: 27918 RVA: 0x001867D8 File Offset: 0x001849D8
			protected ConnectionPointInfo(WebPart webPart, Type transformerType) : this(webPart)
			{
				this._transformerType = transformerType;
			}

			// Token: 0x17001E07 RID: 7687
			// (get) Token: 0x06006D0F RID: 27919 RVA: 0x001867E8 File Offset: 0x001849E8
			public Type TransformerType
			{
				get
				{
					return this._transformerType;
				}
			}

			// Token: 0x17001E08 RID: 7688
			// (get) Token: 0x06006D10 RID: 27920 RVA: 0x001867F0 File Offset: 0x001849F0
			public WebPart WebPart
			{
				get
				{
					return this._webPart;
				}
			}

			// Token: 0x04003A14 RID: 14868
			private WebPart _webPart;

			// Token: 0x04003A15 RID: 14869
			private Type _transformerType;
		}

		// Token: 0x020009E8 RID: 2536
		private sealed class ConsumerInfo : ConnectionsZone.ConnectionPointInfo
		{
			// Token: 0x06006D11 RID: 27921 RVA: 0x001867F8 File Offset: 0x001849F8
			public ConsumerInfo(WebPart webPart, ConsumerConnectionPoint connectionPoint) : base(webPart)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x06006D12 RID: 27922 RVA: 0x00186808 File Offset: 0x00184A08
			public ConsumerInfo(WebPart webPart, ConsumerConnectionPoint connectionPoint, Type transformerType) : base(webPart, transformerType)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x17001E09 RID: 7689
			// (get) Token: 0x06006D13 RID: 27923 RVA: 0x00186819 File Offset: 0x00184A19
			public ConsumerConnectionPoint ConnectionPoint
			{
				get
				{
					return this._connectionPoint;
				}
			}

			// Token: 0x04003A16 RID: 14870
			private ConsumerConnectionPoint _connectionPoint;
		}

		// Token: 0x020009E9 RID: 2537
		private sealed class ProviderInfo : ConnectionsZone.ConnectionPointInfo
		{
			// Token: 0x06006D14 RID: 27924 RVA: 0x00186821 File Offset: 0x00184A21
			public ProviderInfo(WebPart webPart, ProviderConnectionPoint connectionPoint) : base(webPart)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x06006D15 RID: 27925 RVA: 0x00186831 File Offset: 0x00184A31
			public ProviderInfo(WebPart webPart, ProviderConnectionPoint connectionPoint, Type transformerType) : base(webPart, transformerType)
			{
				this._connectionPoint = connectionPoint;
			}

			// Token: 0x17001E0A RID: 7690
			// (get) Token: 0x06006D16 RID: 27926 RVA: 0x00186842 File Offset: 0x00184A42
			public ProviderConnectionPoint ConnectionPoint
			{
				get
				{
					return this._connectionPoint;
				}
			}

			// Token: 0x04003A17 RID: 14871
			private ProviderConnectionPoint _connectionPoint;
		}

		// Token: 0x020009EA RID: 2538
		private enum ConnectionType
		{
			// Token: 0x04003A19 RID: 14873
			None,
			// Token: 0x04003A1A RID: 14874
			Consumer,
			// Token: 0x04003A1B RID: 14875
			Provider
		}

		// Token: 0x020009EB RID: 2539
		private enum ConnectionsZoneMode
		{
			// Token: 0x04003A1D RID: 14877
			ExistingConnections,
			// Token: 0x04003A1E RID: 14878
			ConnectToConsumer,
			// Token: 0x04003A1F RID: 14879
			ConnectToProvider,
			// Token: 0x04003A20 RID: 14880
			ConfiguringTransformer
		}
	}
}
