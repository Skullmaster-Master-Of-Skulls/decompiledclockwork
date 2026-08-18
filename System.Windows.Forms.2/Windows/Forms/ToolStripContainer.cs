using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003EC RID: 1004
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripContainerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("ToolStripContainerDesc")]
	public class ToolStripContainer : ContainerControl
	{
		// Token: 0x06004463 RID: 17507 RVA: 0x0012121C File Offset: 0x0011F41C
		public ToolStripContainer()
		{
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
			base.SuspendLayout();
			try
			{
				this.topPanel = new ToolStripPanel(this);
				this.bottomPanel = new ToolStripPanel(this);
				this.leftPanel = new ToolStripPanel(this);
				this.rightPanel = new ToolStripPanel(this);
				this.contentPanel = new ToolStripContentPanel();
				this.contentPanel.Dock = DockStyle.Fill;
				this.topPanel.Dock = DockStyle.Top;
				this.bottomPanel.Dock = DockStyle.Bottom;
				this.rightPanel.Dock = DockStyle.Right;
				this.leftPanel.Dock = DockStyle.Left;
				ToolStripContainer.ToolStripContainerTypedControlCollection toolStripContainerTypedControlCollection = this.Controls as ToolStripContainer.ToolStripContainerTypedControlCollection;
				if (toolStripContainerTypedControlCollection != null)
				{
					toolStripContainerTypedControlCollection.AddInternal(this.contentPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.leftPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.rightPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.topPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.bottomPanel);
				}
			}
			finally
			{
				base.ResumeLayout(true);
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06004464 RID: 17508 RVA: 0x000B0CB7 File Offset: 0x000AEEB7
		// (set) Token: 0x06004465 RID: 17509 RVA: 0x000EC372 File Offset: 0x000EA572
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06004466 RID: 17510 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x06004467 RID: 17511 RVA: 0x00011A2B File Offset: 0x0000FC2B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x06004469 RID: 17513 RVA: 0x00011A3C File Offset: 0x0000FC3C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x0600446B RID: 17515 RVA: 0x00012F98 File Offset: 0x00011198
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x14000356 RID: 854
		// (add) Token: 0x0600446C RID: 17516 RVA: 0x00058DD2 File Offset: 0x00056FD2
		// (remove) Token: 0x0600446D RID: 17517 RVA: 0x00058DDB File Offset: 0x00056FDB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x0600446F RID: 17519 RVA: 0x00011A98 File Offset: 0x0000FC98
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		// Token: 0x14000357 RID: 855
		// (add) Token: 0x06004470 RID: 17520 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06004471 RID: 17521 RVA: 0x00011AAA File Offset: 0x0000FCAA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06004473 RID: 17523 RVA: 0x00011ABB File Offset: 0x0000FCBB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x14000358 RID: 856
		// (add) Token: 0x06004474 RID: 17524 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06004475 RID: 17525 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged += value;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x00121320 File Offset: 0x0011F520
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerBottomToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel BottomToolStripPanel
		{
			get
			{
				return this.bottomPanel;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06004477 RID: 17527 RVA: 0x00121328 File Offset: 0x0011F528
		// (set) Token: 0x06004478 RID: 17528 RVA: 0x00121335 File Offset: 0x0011F535
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerBottomToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool BottomToolStripPanelVisible
		{
			get
			{
				return this.BottomToolStripPanel.Visible;
			}
			set
			{
				this.BottomToolStripPanel.Visible = value;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06004479 RID: 17529 RVA: 0x00121343 File Offset: 0x0011F543
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerContentPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripContentPanel ContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x000E2B53 File Offset: 0x000E0D53
		// (set) Token: 0x0600447B RID: 17531 RVA: 0x000E2B5B File Offset: 0x000E0D5B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x14000359 RID: 857
		// (add) Token: 0x0600447C RID: 17532 RVA: 0x000E2B64 File Offset: 0x000E0D64
		// (remove) Token: 0x0600447D RID: 17533 RVA: 0x000E2B6D File Offset: 0x000E0D6D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x00011B4A File Offset: 0x0000FD4A
		// (set) Token: 0x0600447F RID: 17535 RVA: 0x00112D8E File Offset: 0x00110F8E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		// Token: 0x1400035A RID: 858
		// (add) Token: 0x06004480 RID: 17536 RVA: 0x00112D97 File Offset: 0x00110F97
		// (remove) Token: 0x06004481 RID: 17537 RVA: 0x00112DA0 File Offset: 0x00110FA0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x0001A23C File Offset: 0x0001843C
		// (set) Token: 0x06004483 RID: 17539 RVA: 0x0001A244 File Offset: 0x00018444
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		// Token: 0x1400035B RID: 859
		// (add) Token: 0x06004484 RID: 17540 RVA: 0x000463EF File Offset: 0x000445EF
		// (remove) Token: 0x06004485 RID: 17541 RVA: 0x000463F8 File Offset: 0x000445F8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x0012134B File Offset: 0x0011F54B
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 175);
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x06004488 RID: 17544 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x1400035C RID: 860
		// (add) Token: 0x06004489 RID: 17545 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x0600448A RID: 17546 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x0012135C File Offset: 0x0011F55C
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerLeftToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel LeftToolStripPanel
		{
			get
			{
				return this.leftPanel;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x00121364 File Offset: 0x0011F564
		// (set) Token: 0x0600448D RID: 17549 RVA: 0x00121371 File Offset: 0x0011F571
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerLeftToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool LeftToolStripPanelVisible
		{
			get
			{
				return this.LeftToolStripPanel.Visible;
			}
			set
			{
				this.LeftToolStripPanel.Visible = value;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x0012137F File Offset: 0x0011F57F
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerRightToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel RightToolStripPanel
		{
			get
			{
				return this.rightPanel;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x00121387 File Offset: 0x0011F587
		// (set) Token: 0x06004490 RID: 17552 RVA: 0x00121394 File Offset: 0x0011F594
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerRightToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool RightToolStripPanelVisible
		{
			get
			{
				return this.RightToolStripPanel.Visible;
			}
			set
			{
				this.RightToolStripPanel.Visible = value;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06004491 RID: 17553 RVA: 0x001213A2 File Offset: 0x0011F5A2
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerTopToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel TopToolStripPanel
		{
			get
			{
				return this.topPanel;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x001213AA File Offset: 0x0011F5AA
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x001213B7 File Offset: 0x0011F5B7
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerTopToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool TopToolStripPanelVisible
		{
			get
			{
				return this.TopToolStripPanel.Visible;
			}
			set
			{
				this.TopToolStripPanel.Visible = value;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x000EC606 File Offset: 0x000EA806
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x001213C5 File Offset: 0x0011F5C5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new ToolStripContainer.ToolStripContainerTypedControlCollection(this, true);
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x001213D0 File Offset: 0x0011F5D0
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			RightToLeft rightToLeft = this.RightToLeft;
			if (rightToLeft == RightToLeft.Yes)
			{
				this.RightToolStripPanel.Dock = DockStyle.Left;
				this.LeftToolStripPanel.Dock = DockStyle.Right;
				return;
			}
			this.RightToolStripPanel.Dock = DockStyle.Right;
			this.LeftToolStripPanel.Dock = DockStyle.Left;
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x00121420 File Offset: 0x0011F620
		protected override void OnSizeChanged(EventArgs e)
		{
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				control.SuspendLayout();
			}
			base.OnSizeChanged(e);
			foreach (object obj2 in this.Controls)
			{
				Control control2 = (Control)obj2;
				control2.ResumeLayout();
			}
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x001214C8 File Offset: 0x0011F6C8
		internal override void RecreateHandleCore()
		{
			if (base.IsHandleCreated)
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					control.CreateControl(true);
				}
			}
			base.RecreateHandleCore();
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal override bool AllowsKeyboardToolTip()
		{
			return false;
		}

		// Token: 0x04002629 RID: 9769
		private ToolStripPanel topPanel;

		// Token: 0x0400262A RID: 9770
		private ToolStripPanel bottomPanel;

		// Token: 0x0400262B RID: 9771
		private ToolStripPanel leftPanel;

		// Token: 0x0400262C RID: 9772
		private ToolStripPanel rightPanel;

		// Token: 0x0400262D RID: 9773
		private ToolStripContentPanel contentPanel;

		// Token: 0x0200080E RID: 2062
		internal class ToolStripContainerTypedControlCollection : WindowsFormsUtils.ReadOnlyControlCollection
		{
			// Token: 0x06006F4D RID: 28493 RVA: 0x001985EA File Offset: 0x001967EA
			public ToolStripContainerTypedControlCollection(Control c, bool isReadOnly) : base(c, isReadOnly)
			{
				this.owner = (c as ToolStripContainer);
			}

			// Token: 0x06006F4E RID: 28494 RVA: 0x00198620 File Offset: 0x00196820
			public override void Add(Control value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ToolStripContainerUseContentPanel"));
				}
				Type type = value.GetType();
				if (!this.contentPanelType.IsAssignableFrom(type) && !this.panelType.IsAssignableFrom(type))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("TypedControlCollectionShouldBeOfTypes", new object[]
					{
						this.contentPanelType.Name,
						this.panelType.Name
					}), new object[0]), value.GetType().Name);
				}
				base.Add(value);
			}

			// Token: 0x06006F4F RID: 28495 RVA: 0x001986CA File Offset: 0x001968CA
			public override void Remove(Control value)
			{
				if ((value is ToolStripPanel || value is ToolStripContentPanel) && !this.owner.DesignMode && this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.Remove(value);
			}

			// Token: 0x06006F50 RID: 28496 RVA: 0x00198708 File Offset: 0x00196908
			internal override void SetChildIndexInternal(Control child, int newIndex)
			{
				if (child is ToolStripPanel || child is ToolStripContentPanel)
				{
					if (this.owner.DesignMode)
					{
						return;
					}
					if (this.IsReadOnly)
					{
						throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
					}
				}
				base.SetChildIndexInternal(child, newIndex);
			}

			// Token: 0x0400431D RID: 17181
			private ToolStripContainer owner;

			// Token: 0x0400431E RID: 17182
			private Type contentPanelType = typeof(ToolStripContentPanel);

			// Token: 0x0400431F RID: 17183
			private Type panelType = typeof(ToolStripPanel);
		}
	}
}
