using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x02000084 RID: 132
	[DefaultProperty("Triggers")]
	[Designer("System.Web.UI.Design.UpdatePanelDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(EmbeddedResourceFinder), "System.Web.Resources.UpdatePanel.bmp")]
	public class UpdatePanel : Control, IAttributeAccessor, IUpdatePanel
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0001A3DB File Offset: 0x000185DB
		public UpdatePanel()
		{
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001A3EA File Offset: 0x000185EA
		internal UpdatePanel(IScriptManagerInternal scriptManager, IPage page)
		{
			this._scriptManager = scriptManager;
			this._page = page;
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001A408 File Offset: 0x00018608
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("WebControl_Attributes")]
		public AttributeCollection Attributes
		{
			get
			{
				if (this._attributes == null)
				{
					StateBag bag = new StateBag(true);
					this._attributes = new AttributeCollection(bag);
				}
				return this._attributes;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001A436 File Offset: 0x00018636
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0001A43E File Offset: 0x0001863E
		[ResourceDescription("UpdatePanel_ChildrenAsTriggers")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool ChildrenAsTriggers
		{
			get
			{
				return this._childrenAsTriggers;
			}
			set
			{
				this._childrenAsTriggers = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001A447 File Offset: 0x00018647
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0001A450 File Offset: 0x00018650
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateInstance(TemplateInstance.Single)]
		public ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				if (!base.DesignMode && this._contentTemplate != null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_CannotSetContentTemplate, new object[]
					{
						this.ID
					}));
				}
				this._contentTemplate = value;
				if (this._contentTemplate != null)
				{
					this.CreateContents();
				}
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001A4A6 File Offset: 0x000186A6
		public sealed override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001A4AE File Offset: 0x000186AE
		[Browsable(false)]
		public Control ContentTemplateContainer
		{
			get
			{
				if (this._contentTemplateContainer == null)
				{
					this._contentTemplateContainer = this.CreateContentTemplateContainer();
					this.AddContentTemplateContainer();
				}
				return this._contentTemplateContainer;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001A4D0 File Offset: 0x000186D0
		[Browsable(false)]
		public bool IsInPartialRendering
		{
			get
			{
				return this._asyncPostBackMode;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001A4D8 File Offset: 0x000186D8
		private IPage IPage
		{
			get
			{
				if (this._page != null)
				{
					return this._page;
				}
				Page page = this.Page;
				if (page == null)
				{
					throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
				}
				return new PageWrapper(page);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001A50F File Offset: 0x0001870F
		protected internal virtual bool RequiresUpdate
		{
			get
			{
				return this._explicitUpdate || this.UpdateMode == UpdatePanelUpdateMode.Always || (this._triggers != null && this._triggers.Count != 0 && this._triggers.HasTriggered());
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001A545 File Offset: 0x00018745
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001A54D File Offset: 0x0001874D
		[ResourceDescription("UpdatePanel_RenderMode")]
		[Category("Layout")]
		[DefaultValue(UpdatePanelRenderMode.Block)]
		public UpdatePanelRenderMode RenderMode
		{
			get
			{
				return this._renderMode;
			}
			set
			{
				if (value < UpdatePanelRenderMode.Block || value > UpdatePanelRenderMode.Inline)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._renderMode = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001A56C File Offset: 0x0001876C
		internal IScriptManagerInternal ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					Page page = this.Page;
					if (page == null)
					{
						throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
					}
					this._scriptManager = System.Web.UI.ScriptManager.GetCurrent(page);
					if (this._scriptManager == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ScriptManagerRequired, new object[]
						{
							this.ID
						}));
					}
				}
				return this._scriptManager;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0001A5D4 File Offset: 0x000187D4
		[Category("Behavior")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.UpdatePanelTriggerCollectionEditor, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", typeof(UITypeEditor))]
		[ResourceDescription("UpdatePanel_Triggers")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public UpdatePanelTriggerCollection Triggers
		{
			get
			{
				if (this._triggers == null)
				{
					this._triggers = new UpdatePanelTriggerCollection(this);
				}
				return this._triggers;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001A5F0 File Offset: 0x000187F0
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001A5F8 File Offset: 0x000187F8
		[ResourceDescription("UpdatePanel_UpdateMode")]
		[Category("Behavior")]
		[DefaultValue(UpdatePanelUpdateMode.Always)]
		public UpdatePanelUpdateMode UpdateMode
		{
			get
			{
				return this._updateMode;
			}
			set
			{
				if (value < UpdatePanelUpdateMode.Always || value > UpdatePanelUpdateMode.Conditional)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._updateMode = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001A614 File Offset: 0x00018814
		private UpdatePanel.SingleChildControlCollection ChildControls
		{
			get
			{
				return this.Controls as UpdatePanel.SingleChildControlCollection;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001A62E File Offset: 0x0001882E
		private void AddContentTemplateContainer()
		{
			this.ChildControls.AddSingleChild(this._contentTemplateContainer);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001A641 File Offset: 0x00018841
		internal void ClearContent()
		{
			this.ContentTemplateContainer.Controls.Clear();
			this._contentTemplateContainer = null;
			this.ChildControls.ClearInternal();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001A668 File Offset: 0x00018868
		private void CreateContents()
		{
			if (base.DesignMode)
			{
				this.ClearContent();
			}
			if (this._contentTemplateContainer == null)
			{
				this._contentTemplateContainer = this.CreateContentTemplateContainer();
				if (this._contentTemplate != null)
				{
					this._contentTemplate.InstantiateIn(this._contentTemplateContainer);
				}
				this.AddContentTemplateContainer();
				return;
			}
			if (this._contentTemplate != null)
			{
				this._contentTemplate.InstantiateIn(this._contentTemplateContainer);
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001A6D0 File Offset: 0x000188D0
		protected virtual Control CreateContentTemplateContainer()
		{
			return new Control();
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001A6D7 File Offset: 0x000188D7
		protected sealed override ControlCollection CreateControlCollection()
		{
			return new UpdatePanel.SingleChildControlCollection(this);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001A6DF File Offset: 0x000188DF
		protected internal virtual void Initialize()
		{
			if (this._triggers != null && this.ScriptManager.SupportsPartialRendering)
			{
				this._triggers.Initialize();
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001A701 File Offset: 0x00018901
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.RegisterPanel();
			if (this._contentTemplateContainer == null)
			{
				this._contentTemplateContainer = this.CreateContentTemplateContainer();
				this.AddContentTemplateContainer();
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001A72A File Offset: 0x0001892A
		protected internal override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.DesignMode && !this.ScriptManager.IsInAsyncPostBack)
			{
				this.Initialize();
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001A74E File Offset: 0x0001894E
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (!this.ChildrenAsTriggers && this.UpdateMode == UpdatePanelUpdateMode.Always)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_ChildrenTriggersAndUpdateAlways, new object[]
				{
					this.ID
				}));
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001A78B File Offset: 0x0001898B
		protected internal override void OnUnload(EventArgs e)
		{
			if (!base.DesignMode && this._panelRegistered)
			{
				this.ScriptManager.UnregisterUpdatePanel(this);
			}
			base.OnUnload(e);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001A7B0 File Offset: 0x000189B0
		private void RegisterPanel()
		{
			if (!base.DesignMode && !this._panelRegistered)
			{
				for (Control parent = this.Parent; parent != null; parent = parent.Parent)
				{
					UpdatePanel updatePanel = parent as UpdatePanel;
					if (updatePanel != null)
					{
						updatePanel.RegisterPanel();
						break;
					}
				}
				this.ScriptManager.RegisterUpdatePanel(this);
				this._panelRegistered = true;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0001A805 File Offset: 0x00018A05
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.IPage.VerifyRenderingInServerForm(this);
			base.Render(writer);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001A81C File Offset: 0x00018A1C
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			if (this._asyncPostBackMode)
			{
				if (this._rendered)
				{
					return;
				}
				HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter(CultureInfo.CurrentCulture));
				base.RenderChildren(htmlTextWriter);
				PageRequestManager.EncodeString(writer, "updatePanel", this.ClientID, htmlTextWriter.InnerWriter.ToString());
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				if (this._attributes != null)
				{
					this._attributes.AddAttributes(writer);
				}
				if (this.RenderMode == UpdatePanelRenderMode.Block)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Div);
				}
				else
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
				}
				base.RenderChildren(writer);
				writer.RenderEndTag();
			}
			this._rendered = true;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001A8BF File Offset: 0x00018ABF
		internal void SetAsyncPostBackMode(bool asyncPostBackMode)
		{
			if (this._asyncPostBackModeInitialized)
			{
				throw new InvalidOperationException(AtlasWeb.UpdatePanel_SetPartialRenderingModeCalledOnce);
			}
			this._asyncPostBackMode = asyncPostBackMode;
			this._asyncPostBackModeInitialized = true;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0001A8E4 File Offset: 0x00018AE4
		public void Update()
		{
			if (this.UpdateMode == UpdatePanelUpdateMode.Always)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_UpdateConditional, new object[]
				{
					this.ID
				}));
			}
			if (this._asyncPostBackModeInitialized)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_UpdateTooLate, new object[]
				{
					this.ID
				}));
			}
			this._explicitUpdate = true;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001A950 File Offset: 0x00018B50
		string IAttributeAccessor.GetAttribute(string key)
		{
			if (this._attributes == null)
			{
				return null;
			}
			return this._attributes[key];
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001A968 File Offset: 0x00018B68
		void IAttributeAccessor.SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}

		// Token: 0x04000208 RID: 520
		private const string UpdatePanelToken = "updatePanel";

		// Token: 0x04000209 RID: 521
		private new IPage _page;

		// Token: 0x0400020A RID: 522
		private IScriptManagerInternal _scriptManager;

		// Token: 0x0400020B RID: 523
		private AttributeCollection _attributes;

		// Token: 0x0400020C RID: 524
		private bool _childrenAsTriggers = true;

		// Token: 0x0400020D RID: 525
		private ITemplate _contentTemplate;

		// Token: 0x0400020E RID: 526
		private Control _contentTemplateContainer;

		// Token: 0x0400020F RID: 527
		private bool _asyncPostBackMode;

		// Token: 0x04000210 RID: 528
		private bool _asyncPostBackModeInitialized;

		// Token: 0x04000211 RID: 529
		private UpdatePanelUpdateMode _updateMode;

		// Token: 0x04000212 RID: 530
		private bool _rendered;

		// Token: 0x04000213 RID: 531
		private bool _explicitUpdate;

		// Token: 0x04000214 RID: 532
		private UpdatePanelRenderMode _renderMode;

		// Token: 0x04000215 RID: 533
		private UpdatePanelTriggerCollection _triggers;

		// Token: 0x04000216 RID: 534
		private bool _panelRegistered;

		// Token: 0x02000168 RID: 360
		private sealed class SingleChildControlCollection : ControlCollection
		{
			// Token: 0x0600102D RID: 4141 RVA: 0x00037B27 File Offset: 0x00035D27
			public SingleChildControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x0600102E RID: 4142 RVA: 0x00037B30 File Offset: 0x00035D30
			internal void AddSingleChild(Control child)
			{
				base.Add(child);
			}

			// Token: 0x0600102F RID: 4143 RVA: 0x00037B39 File Offset: 0x00035D39
			public override void Add(Control child)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_CannotModifyControlCollection, new object[]
				{
					base.Owner.ID
				}));
			}

			// Token: 0x06001030 RID: 4144 RVA: 0x00037B39 File Offset: 0x00035D39
			public override void AddAt(int index, Control child)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_CannotModifyControlCollection, new object[]
				{
					base.Owner.ID
				}));
			}

			// Token: 0x06001031 RID: 4145 RVA: 0x00037B39 File Offset: 0x00035D39
			public override void Clear()
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_CannotModifyControlCollection, new object[]
				{
					base.Owner.ID
				}));
			}

			// Token: 0x06001032 RID: 4146 RVA: 0x00037B64 File Offset: 0x00035D64
			internal void ClearInternal()
			{
				try
				{
					this._allowClear = true;
					base.Clear();
				}
				finally
				{
					this._allowClear = false;
				}
			}

			// Token: 0x06001033 RID: 4147 RVA: 0x00037B98 File Offset: 0x00035D98
			public override void Remove(Control value)
			{
				if (!this._allowClear)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_CannotModifyControlCollection, new object[]
					{
						base.Owner.ID
					}));
				}
				base.Remove(value);
			}

			// Token: 0x06001034 RID: 4148 RVA: 0x00037BD2 File Offset: 0x00035DD2
			public override void RemoveAt(int index)
			{
				if (!this._allowClear)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.UpdatePanel_CannotModifyControlCollection, new object[]
					{
						base.Owner.ID
					}));
				}
				base.RemoveAt(index);
			}

			// Token: 0x040004EF RID: 1263
			private bool _allowClear;
		}
	}
}
