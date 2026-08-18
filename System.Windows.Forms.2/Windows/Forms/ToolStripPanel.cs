using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020003EE RID: 1006
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(ToolStripPanel), "ToolStripPanel_standalone.bmp")]
	public class ToolStripPanel : ContainerControl, IArrangedElement, IComponent, IDisposable
	{
		// Token: 0x060044D6 RID: 17622 RVA: 0x0012178C File Offset: 0x0011F98C
		public ToolStripPanel()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledRowMargin = DpiHelper.LogicalToDeviceUnits(ToolStripPanel.rowMargin, 0);
			}
			base.SuspendLayout();
			base.AutoScaleMode = AutoScaleMode.None;
			this.InitFlowLayout();
			this.AutoSize = true;
			this.MinimumSize = Size.Empty;
			this.state[ToolStripPanel.stateLocked | ToolStripPanel.stateBeginInit | ToolStripPanel.stateChangingZOrder] = false;
			this.TabStop = false;
			ToolStripManager.ToolStripPanels.Add(this);
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Selectable, false);
			base.ResumeLayout(true);
		}

		// Token: 0x060044D7 RID: 17623 RVA: 0x00121846 File Offset: 0x0011FA46
		internal ToolStripPanel(ToolStripContainer owner) : this()
		{
			this.owner = owner;
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x000B90B9 File Offset: 0x000B72B9
		// (set) Token: 0x060044D9 RID: 17625 RVA: 0x000B90C1 File Offset: 0x000B72C1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x000B0CB7 File Offset: 0x000AEEB7
		// (set) Token: 0x060044DB RID: 17627 RVA: 0x000EC372 File Offset: 0x000EA572
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

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x060044DD RID: 17629 RVA: 0x00011A2B File Offset: 0x0000FC2B
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

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x060044DF RID: 17631 RVA: 0x00011A3C File Offset: 0x0000FC3C
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

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x060044E1 RID: 17633 RVA: 0x00011A4D File Offset: 0x0000FC4D
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x14000365 RID: 869
		// (add) Token: 0x060044E2 RID: 17634 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x060044E3 RID: 17635 RVA: 0x00011A5F File Offset: 0x0000FC5F
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected override Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x00121855 File Offset: 0x0011FA55
		// (set) Token: 0x060044E7 RID: 17639 RVA: 0x0012185D File Offset: 0x0011FA5D
		public Padding RowMargin
		{
			get
			{
				return this.scaledRowMargin;
			}
			set
			{
				this.scaledRowMargin = value;
				LayoutTransaction.DoLayout(this, this, "RowMargin");
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x060044E9 RID: 17641 RVA: 0x00121872 File Offset: 0x0011FA72
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
				if (value == DockStyle.Left || value == DockStyle.Right)
				{
					this.Orientation = Orientation.Vertical;
					return;
				}
				this.Orientation = Orientation.Horizontal;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x00121892 File Offset: 0x0011FA92
		internal Rectangle DragBounds
		{
			get
			{
				return LayoutUtils.InflateRect(base.ClientRectangle, ToolStripPanel.DragMargin);
			}
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x060044EB RID: 17643 RVA: 0x0010C4D9 File Offset: 0x0010A6D9
		internal bool IsInDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x000AFBF0 File Offset: 0x000ADDF0
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x001218A4 File Offset: 0x0011FAA4
		// (set) Token: 0x060044EE RID: 17646 RVA: 0x001218B6 File Offset: 0x0011FAB6
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool Locked
		{
			get
			{
				return this.state[ToolStripPanel.stateLocked];
			}
			set
			{
				this.state[ToolStripPanel.stateLocked] = value;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x001218C9 File Offset: 0x0011FAC9
		// (set) Token: 0x060044F0 RID: 17648 RVA: 0x001218D4 File Offset: 0x0011FAD4
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (this.orientation != value)
				{
					this.orientation = value;
					this.scaledRowMargin = LayoutUtils.FlipPadding(this.scaledRowMargin);
					this.InitFlowLayout();
					foreach (object obj in this.RowsInternal)
					{
						ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
						toolStripPanelRow.OnOrientationChanged();
					}
				}
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060044F1 RID: 17649 RVA: 0x00121954 File Offset: 0x0011FB54
		private ToolStripRendererSwitcher RendererSwitcher
		{
			get
			{
				if (this.rendererSwitcher == null)
				{
					this.rendererSwitcher = new ToolStripRendererSwitcher(this);
					this.HandleRendererChanged(this, EventArgs.Empty);
					this.rendererSwitcher.RendererChanged += this.HandleRendererChanged;
				}
				return this.rendererSwitcher;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x00121993 File Offset: 0x0011FB93
		// (set) Token: 0x060044F3 RID: 17651 RVA: 0x001219A0 File Offset: 0x0011FBA0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripRenderer Renderer
		{
			get
			{
				return this.RendererSwitcher.Renderer;
			}
			set
			{
				this.RendererSwitcher.Renderer = value;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x001219AE File Offset: 0x0011FBAE
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x001219BB File Offset: 0x0011FBBB
		[SRDescription("ToolStripRenderModeDescr")]
		[SRCategory("CatAppearance")]
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return this.RendererSwitcher.RenderMode;
			}
			set
			{
				this.RendererSwitcher.RenderMode = value;
			}
		}

		// Token: 0x14000366 RID: 870
		// (add) Token: 0x060044F6 RID: 17654 RVA: 0x001219C9 File Offset: 0x0011FBC9
		// (remove) Token: 0x060044F7 RID: 17655 RVA: 0x001219DC File Offset: 0x0011FBDC
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripPanel.EventRendererChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripPanel.EventRendererChanged, value);
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x001219F0 File Offset: 0x0011FBF0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ToolStripPanelRowsDescr")]
		internal ToolStripPanel.ToolStripPanelRowCollection RowsInternal
		{
			get
			{
				ToolStripPanel.ToolStripPanelRowCollection toolStripPanelRowCollection = (ToolStripPanel.ToolStripPanelRowCollection)base.Properties.GetObject(ToolStripPanel.PropToolStripPanelRowCollection);
				if (toolStripPanelRowCollection == null)
				{
					toolStripPanelRowCollection = this.CreateToolStripPanelRowCollection();
					base.Properties.SetObject(ToolStripPanel.PropToolStripPanelRowCollection, toolStripPanelRowCollection);
				}
				return toolStripPanelRowCollection;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060044F9 RID: 17657 RVA: 0x00121A30 File Offset: 0x0011FC30
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripPanelRowsDescr")]
		public ToolStripPanelRow[] Rows
		{
			get
			{
				ToolStripPanelRow[] array = new ToolStripPanelRow[this.RowsInternal.Count];
				this.RowsInternal.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x000B25EE File Offset: 0x000B07EE
		// (set) Token: 0x060044FB RID: 17659 RVA: 0x000B25F6 File Offset: 0x000B07F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x14000367 RID: 871
		// (add) Token: 0x060044FC RID: 17660 RVA: 0x000B25FF File Offset: 0x000B07FF
		// (remove) Token: 0x060044FD RID: 17661 RVA: 0x000B2608 File Offset: 0x000B0808
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x060044FF RID: 17663 RVA: 0x00121A5C File Offset: 0x0011FC5C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				if (AccessibilityImprovements.Level2)
				{
					base.SetStyle(ControlStyles.Selectable, value);
				}
				base.TabStop = value;
			}
		}

		// Token: 0x14000368 RID: 872
		// (add) Token: 0x06004500 RID: 17664 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x06004501 RID: 17665 RVA: 0x000B262B File Offset: 0x000B082B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06004503 RID: 17667 RVA: 0x00024185 File Offset: 0x00022385
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x14000369 RID: 873
		// (add) Token: 0x06004504 RID: 17668 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06004505 RID: 17669 RVA: 0x0004677A File Offset: 0x0004497A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x00121A78 File Offset: 0x0011FC78
		public void BeginInit()
		{
			this.state[ToolStripPanel.stateBeginInit] = true;
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00121A8C File Offset: 0x0011FC8C
		public void EndInit()
		{
			this.state[ToolStripPanel.stateBeginInit] = false;
			this.state[ToolStripPanel.stateEndInit] = true;
			try
			{
				if (!this.state[ToolStripPanel.stateInJoin])
				{
					this.JoinControls();
				}
			}
			finally
			{
				this.state[ToolStripPanel.stateEndInit] = false;
			}
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00121AF8 File Offset: 0x0011FCF8
		private ToolStripPanel.ToolStripPanelRowCollection CreateToolStripPanelRowCollection()
		{
			return new ToolStripPanel.ToolStripPanelRowCollection(this);
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x00121B00 File Offset: 0x0011FD00
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new ToolStripPanel.ToolStripPanelControlCollection(this);
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x00121B08 File Offset: 0x0011FD08
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ToolStripManager.ToolStripPanels.Remove(this);
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x00121B1F File Offset: 0x0011FD1F
		private void InitFlowLayout()
		{
			if (this.Orientation == Orientation.Horizontal)
			{
				FlowLayout.SetFlowDirection(this, FlowDirection.TopDown);
			}
			else
			{
				FlowLayout.SetFlowDirection(this, FlowDirection.LeftToRight);
			}
			FlowLayout.SetWrapContents(this, false);
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x00121B40 File Offset: 0x0011FD40
		private Point GetStartLocation(ToolStrip toolStripToDrag)
		{
			if (toolStripToDrag.IsCurrentlyDragging && this.Orientation == Orientation.Horizontal && toolStripToDrag.RightToLeft == RightToLeft.Yes)
			{
				return new Point(toolStripToDrag.Right, toolStripToDrag.Top);
			}
			return toolStripToDrag.Location;
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x00121B73 File Offset: 0x0011FD73
		private void HandleRendererChanged(object sender, EventArgs e)
		{
			this.OnRendererChanged(e);
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x00121B7C File Offset: 0x0011FD7C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			ToolStripPanelRenderEventArgs toolStripPanelRenderEventArgs = new ToolStripPanelRenderEventArgs(e.Graphics, this);
			this.Renderer.DrawToolStripPanelBackground(toolStripPanelRenderEventArgs);
			if (!toolStripPanelRenderEventArgs.Handled)
			{
				base.OnPaintBackground(e);
			}
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x00121BB4 File Offset: 0x0011FDB4
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if (!this.state[ToolStripPanel.stateBeginInit] && !this.state[ToolStripPanel.stateInJoin])
			{
				if (!this.state[ToolStripPanel.stateLayoutSuspended])
				{
					this.Join(e.Control as ToolStrip, e.Control.Location);
					return;
				}
				this.BeginInit();
			}
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x00121C24 File Offset: 0x0011FE24
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			ISupportToolStripPanel supportToolStripPanel = e.Control as ISupportToolStripPanel;
			if (supportToolStripPanel != null && supportToolStripPanel.ToolStripPanelRow != null)
			{
				supportToolStripPanel.ToolStripPanelRow.ControlsInternal.Remove(e.Control);
			}
			base.OnControlRemoved(e);
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00121C68 File Offset: 0x0011FE68
		protected override void OnLayout(LayoutEventArgs e)
		{
			if (e.AffectedComponent != this.ParentInternal && e.AffectedComponent is Control)
			{
				ISupportToolStripPanel supportToolStripPanel = e.AffectedComponent as ISupportToolStripPanel;
				if (supportToolStripPanel != null && this.RowsInternal.Contains(supportToolStripPanel.ToolStripPanelRow))
				{
					LayoutTransaction.DoLayout(supportToolStripPanel.ToolStripPanelRow, e.AffectedComponent as IArrangedElement, e.AffectedProperty);
				}
			}
			base.OnLayout(e);
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x00121CD5 File Offset: 0x0011FED5
		internal override void OnLayoutSuspended()
		{
			base.OnLayoutSuspended();
			this.state[ToolStripPanel.stateLayoutSuspended] = true;
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x00121CEE File Offset: 0x0011FEEE
		internal override void OnLayoutResuming(bool resumeLayout)
		{
			base.OnLayoutResuming(resumeLayout);
			this.state[ToolStripPanel.stateLayoutSuspended] = false;
			if (this.state[ToolStripPanel.stateBeginInit])
			{
				this.EndInit();
			}
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x00121D20 File Offset: 0x0011FF20
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			if (!this.state[ToolStripPanel.stateBeginInit])
			{
				if (base.Controls.Count > 0)
				{
					base.SuspendLayout();
					Control[] array = new Control[base.Controls.Count];
					Point[] array2 = new Point[base.Controls.Count];
					int num = 0;
					foreach (object obj in this.RowsInternal)
					{
						ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
						foreach (object obj2 in toolStripPanelRow.ControlsInternal)
						{
							Control control = (Control)obj2;
							array[num] = control;
							array2[num] = new Point(toolStripPanelRow.Bounds.Width - control.Right, control.Top);
							num++;
						}
					}
					base.Controls.Clear();
					for (int i = 0; i < array.Length; i++)
					{
						this.Join(array[i] as ToolStrip, array2[i]);
					}
					base.ResumeLayout(true);
					return;
				}
			}
			else
			{
				this.state[ToolStripPanel.stateRightToLeftChanged] = true;
			}
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x00121E9C File Offset: 0x0012009C
		protected virtual void OnRendererChanged(EventArgs e)
		{
			this.Renderer.InitializePanel(this);
			base.Invalidate();
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripPanel.EventRendererChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x00121EDC File Offset: 0x001200DC
		protected override void OnParentChanged(EventArgs e)
		{
			this.PerformUpdate();
			base.OnParentChanged(e);
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x00121EEB File Offset: 0x001200EB
		protected override void OnDockChanged(EventArgs e)
		{
			this.PerformUpdate();
			base.OnDockChanged(e);
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x00121EFA File Offset: 0x001200FA
		internal void PerformUpdate()
		{
			this.PerformUpdate(false);
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00121F03 File Offset: 0x00120103
		internal void PerformUpdate(bool forceLayout)
		{
			if (!this.state[ToolStripPanel.stateBeginInit] && !this.state[ToolStripPanel.stateInJoin])
			{
				this.JoinControls(forceLayout);
			}
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x00121F30 File Offset: 0x00120130
		private void ResetRenderMode()
		{
			this.RendererSwitcher.ResetRenderMode();
		}

		// Token: 0x0600451B RID: 17691 RVA: 0x00121F3D File Offset: 0x0012013D
		private bool ShouldSerializeRenderMode()
		{
			return this.RendererSwitcher.ShouldSerializeRenderMode();
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x00121F4A File Offset: 0x0012014A
		private bool ShouldSerializeDock()
		{
			return this.owner == null && this.Dock > DockStyle.None;
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x00121F5F File Offset: 0x0012015F
		private void JoinControls()
		{
			this.JoinControls(false);
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x00121F68 File Offset: 0x00120168
		private void JoinControls(bool forceLayout)
		{
			ToolStripPanel.ToolStripPanelControlCollection toolStripPanelControlCollection = base.Controls as ToolStripPanel.ToolStripPanelControlCollection;
			if (toolStripPanelControlCollection.Count > 0)
			{
				toolStripPanelControlCollection.Sort();
				Control[] array = new Control[toolStripPanelControlCollection.Count];
				toolStripPanelControlCollection.CopyTo(array, 0);
				int i = 0;
				while (i < array.Length)
				{
					int count = this.RowsInternal.Count;
					ISupportToolStripPanel supportToolStripPanel = array[i] as ISupportToolStripPanel;
					if (supportToolStripPanel == null || supportToolStripPanel.ToolStripPanelRow == null || supportToolStripPanel.IsCurrentlyDragging)
					{
						goto IL_8B;
					}
					ToolStripPanelRow toolStripPanelRow = supportToolStripPanel.ToolStripPanelRow;
					if (!toolStripPanelRow.Bounds.Contains(array[i].Location))
					{
						goto IL_8B;
					}
					IL_117:
					i++;
					continue;
					IL_8B:
					if (array[i].AutoSize)
					{
						array[i].Size = array[i].PreferredSize;
					}
					Point location = array[i].Location;
					if (this.state[ToolStripPanel.stateRightToLeftChanged])
					{
						location = new Point(base.Width - array[i].Right, location.Y);
					}
					this.Join(array[i] as ToolStrip, array[i].Location);
					if (count < this.RowsInternal.Count || forceLayout)
					{
						this.OnLayout(new LayoutEventArgs(this, PropertyNames.Rows));
						goto IL_117;
					}
					goto IL_117;
				}
			}
			this.state[ToolStripPanel.stateRightToLeftChanged] = false;
		}

		// Token: 0x0600451F RID: 17695 RVA: 0x001220AC File Offset: 0x001202AC
		private void GiveToolStripPanelFeedback(ToolStrip toolStripToDrag, Point screenLocation)
		{
			if (this.Orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes)
			{
				screenLocation.Offset(-toolStripToDrag.Width, 0);
			}
			if (ToolStripPanel.CurrentFeedbackRect == null)
			{
				ToolStripPanel.CurrentFeedbackRect = new ToolStripPanel.FeedbackRectangle(toolStripToDrag.ClientRectangle);
			}
			if (!ToolStripPanel.CurrentFeedbackRect.Visible)
			{
				toolStripToDrag.SuspendCaputureMode();
				try
				{
					ToolStripPanel.CurrentFeedbackRect.Show(screenLocation);
					toolStripToDrag.CaptureInternal = true;
					return;
				}
				finally
				{
					toolStripToDrag.ResumeCaputureMode();
				}
			}
			ToolStripPanel.CurrentFeedbackRect.Move(screenLocation);
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x00122138 File Offset: 0x00120338
		internal static void ClearDragFeedback()
		{
			ToolStripPanel.FeedbackRectangle feedbackRectangle = ToolStripPanel.feedbackRect;
			ToolStripPanel.feedbackRect = null;
			if (feedbackRectangle != null)
			{
				feedbackRectangle.Dispose();
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x0012215A File Offset: 0x0012035A
		// (set) Token: 0x06004522 RID: 17698 RVA: 0x00122161 File Offset: 0x00120361
		private static ToolStripPanel.FeedbackRectangle CurrentFeedbackRect
		{
			get
			{
				return ToolStripPanel.feedbackRect;
			}
			set
			{
				ToolStripPanel.feedbackRect = value;
			}
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x00122169 File Offset: 0x00120369
		public void Join(ToolStrip toolStripToDrag)
		{
			this.Join(toolStripToDrag, Point.Empty);
		}

		// Token: 0x06004524 RID: 17700 RVA: 0x00122178 File Offset: 0x00120378
		public void Join(ToolStrip toolStripToDrag, int row)
		{
			if (row < 0)
			{
				throw new ArgumentOutOfRangeException("row", SR.GetString("IndexOutOfRange", new object[]
				{
					row.ToString(CultureInfo.CurrentCulture)
				}));
			}
			Point empty = Point.Empty;
			Rectangle rectangle = Rectangle.Empty;
			if (row >= this.RowsInternal.Count)
			{
				rectangle = this.DragBounds;
			}
			else
			{
				rectangle = this.RowsInternal[row].DragBounds;
			}
			if (this.Orientation == Orientation.Horizontal)
			{
				empty = new Point(0, rectangle.Bottom - 1);
			}
			else
			{
				empty = new Point(rectangle.Right - 1, 0);
			}
			this.Join(toolStripToDrag, empty);
		}

		// Token: 0x06004525 RID: 17701 RVA: 0x0012221C File Offset: 0x0012041C
		public void Join(ToolStrip toolStripToDrag, int x, int y)
		{
			this.Join(toolStripToDrag, new Point(x, y));
		}

		// Token: 0x06004526 RID: 17702 RVA: 0x0012222C File Offset: 0x0012042C
		public void Join(ToolStrip toolStripToDrag, Point location)
		{
			if (toolStripToDrag == null)
			{
				throw new ArgumentNullException("toolStripToDrag");
			}
			if (!this.state[ToolStripPanel.stateBeginInit] && !this.state[ToolStripPanel.stateInJoin])
			{
				try
				{
					this.state[ToolStripPanel.stateInJoin] = true;
					toolStripToDrag.ParentInternal = this;
					this.MoveInsideContainer(toolStripToDrag, location);
					return;
				}
				finally
				{
					this.state[ToolStripPanel.stateInJoin] = false;
				}
			}
			base.Controls.Add(toolStripToDrag);
			toolStripToDrag.Location = location;
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x001222C4 File Offset: 0x001204C4
		internal void MoveControl(ToolStrip toolStripToDrag, Point screenLocation)
		{
			if (toolStripToDrag == null)
			{
				return;
			}
			Point point = base.PointToClient(screenLocation);
			if (!this.DragBounds.Contains(point))
			{
				this.MoveOutsideContainer(toolStripToDrag, screenLocation);
				return;
			}
			this.Join(toolStripToDrag, point);
		}

		// Token: 0x06004528 RID: 17704 RVA: 0x00122304 File Offset: 0x00120504
		private void MoveInsideContainer(ToolStrip toolStripToDrag, Point clientLocation)
		{
			if (((ISupportToolStripPanel)toolStripToDrag).IsCurrentlyDragging && !this.DragBounds.Contains(clientLocation))
			{
				return;
			}
			ToolStripPanel.ClearDragFeedback();
			if (toolStripToDrag.Site != null && toolStripToDrag.Site.DesignMode && base.IsHandleCreated && (clientLocation.X < 0 || clientLocation.Y < 0))
			{
				Point point = base.PointToClient(WindowsFormsUtils.LastCursorPoint);
				if (base.ClientRectangle.Contains(point))
				{
					clientLocation = point;
				}
			}
			ToolStripPanelRow toolStripPanelRow = ((ISupportToolStripPanel)toolStripToDrag).ToolStripPanelRow;
			bool flag = false;
			if (toolStripPanelRow != null && toolStripPanelRow.Visible && toolStripPanelRow.ToolStripPanel == this)
			{
				if (toolStripToDrag.IsCurrentlyDragging)
				{
					flag = toolStripPanelRow.DragBounds.Contains(clientLocation);
				}
				else
				{
					flag = toolStripPanelRow.Bounds.Contains(clientLocation);
				}
			}
			if (flag)
			{
				((ISupportToolStripPanel)toolStripToDrag).ToolStripPanelRow.MoveControl(toolStripToDrag, this.GetStartLocation(toolStripToDrag), clientLocation);
				return;
			}
			ToolStripPanelRow toolStripPanelRow2 = this.PointToRow(clientLocation);
			if (toolStripPanelRow2 == null)
			{
				int num = this.RowsInternal.Count;
				if (this.Orientation == Orientation.Horizontal)
				{
					num = ((clientLocation.Y <= base.Padding.Left) ? 0 : num);
				}
				else
				{
					num = ((clientLocation.X <= base.Padding.Left) ? 0 : num);
				}
				ToolStripPanelRow toolStripPanelRow3 = null;
				if (this.RowsInternal.Count > 0)
				{
					if (num == 0)
					{
						toolStripPanelRow3 = this.RowsInternal[0];
					}
					else if (num > 0)
					{
						toolStripPanelRow3 = this.RowsInternal[num - 1];
					}
				}
				if (toolStripPanelRow3 != null && toolStripPanelRow3.ControlsInternal.Count == 1 && toolStripPanelRow3.ControlsInternal.Contains(toolStripToDrag))
				{
					toolStripPanelRow2 = toolStripPanelRow3;
					if (toolStripToDrag.IsInDesignMode)
					{
						Point endClientLocation = (this.Orientation == Orientation.Horizontal) ? new Point(clientLocation.X, toolStripPanelRow2.Bounds.Y) : new Point(toolStripPanelRow2.Bounds.X, clientLocation.Y);
						((ISupportToolStripPanel)toolStripToDrag).ToolStripPanelRow.MoveControl(toolStripToDrag, this.GetStartLocation(toolStripToDrag), endClientLocation);
					}
				}
				else
				{
					toolStripPanelRow2 = new ToolStripPanelRow(this);
					this.RowsInternal.Insert(num, toolStripPanelRow2);
				}
			}
			else if (!toolStripPanelRow2.CanMove(toolStripToDrag))
			{
				int num2 = this.RowsInternal.IndexOf(toolStripPanelRow2);
				if (toolStripPanelRow != null && toolStripPanelRow.ControlsInternal.Count == 1 && num2 > 0 && num2 - 1 == this.RowsInternal.IndexOf(toolStripPanelRow))
				{
					return;
				}
				toolStripPanelRow2 = new ToolStripPanelRow(this);
				this.RowsInternal.Insert(num2, toolStripPanelRow2);
				clientLocation.Y = toolStripPanelRow2.Bounds.Y;
			}
			bool flag2 = toolStripPanelRow != toolStripPanelRow2;
			if (!flag2 && toolStripPanelRow != null && toolStripPanelRow.ControlsInternal.Count > 1)
			{
				toolStripPanelRow.LeaveRow(toolStripToDrag);
				toolStripPanelRow = null;
				flag2 = true;
			}
			if (flag2)
			{
				if (toolStripPanelRow != null)
				{
					toolStripPanelRow.LeaveRow(toolStripToDrag);
				}
				toolStripPanelRow2.JoinRow(toolStripToDrag, clientLocation);
			}
			if (flag2 && ((ISupportToolStripPanel)toolStripToDrag).IsCurrentlyDragging)
			{
				for (int i = 0; i < this.RowsInternal.Count; i++)
				{
					LayoutTransaction.DoLayout(this.RowsInternal[i], this, PropertyNames.Rows);
				}
				if (this.RowsInternal.IndexOf(toolStripPanelRow2) > 0)
				{
					IntSecurity.AdjustCursorPosition.Assert();
					try
					{
						Point position = toolStripToDrag.PointToScreen(toolStripToDrag.GripRectangle.Location);
						if (this.Orientation == Orientation.Vertical)
						{
							position.X += toolStripToDrag.GripRectangle.Width / 2;
							position.Y = Cursor.Position.Y;
						}
						else
						{
							position.Y += toolStripToDrag.GripRectangle.Height / 2;
							position.X = Cursor.Position.X;
						}
						Cursor.Position = position;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		// Token: 0x06004529 RID: 17705 RVA: 0x001226F0 File Offset: 0x001208F0
		private void MoveOutsideContainer(ToolStrip toolStripToDrag, Point screenLocation)
		{
			ToolStripPanel toolStripPanel = ToolStripManager.ToolStripPanelFromPoint(toolStripToDrag, screenLocation);
			if (toolStripPanel != null)
			{
				using (new LayoutTransaction(toolStripPanel, toolStripPanel, null))
				{
					toolStripPanel.MoveControl(toolStripToDrag, screenLocation);
				}
				toolStripToDrag.PerformLayout();
				return;
			}
			this.GiveToolStripPanelFeedback(toolStripToDrag, screenLocation);
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x00122744 File Offset: 0x00120944
		public ToolStripPanelRow PointToRow(Point clientLocation)
		{
			foreach (object obj in this.RowsInternal)
			{
				ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
				Rectangle rectangle = LayoutUtils.InflateRect(toolStripPanelRow.Bounds, toolStripPanelRow.Margin);
				if (this.ParentInternal != null)
				{
					if (this.Orientation == Orientation.Horizontal && rectangle.Width == 0)
					{
						rectangle.Width = this.ParentInternal.DisplayRectangle.Width;
					}
					else if (this.Orientation == Orientation.Vertical && rectangle.Height == 0)
					{
						rectangle.Height = this.ParentInternal.DisplayRectangle.Height;
					}
				}
				if (rectangle.Contains(clientLocation))
				{
					return toolStripPanelRow;
				}
			}
			return null;
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x00122828 File Offset: 0x00120A28
		[Conditional("DEBUG")]
		private void Debug_VerifyOneToOneCellRowControlMatchup()
		{
			for (int i = 0; i < this.RowsInternal.Count; i++)
			{
				ToolStripPanelRow toolStripPanelRow = this.RowsInternal[i];
				foreach (object obj in toolStripPanelRow.Cells)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)obj;
					if (toolStripPanelCell.Control != null)
					{
						ToolStripPanelRow toolStripPanelRow2 = ((ISupportToolStripPanel)toolStripPanelCell.Control).ToolStripPanelRow;
						if (toolStripPanelRow2 != toolStripPanelRow)
						{
							int num = (toolStripPanelRow2 != null) ? this.RowsInternal.IndexOf(toolStripPanelRow2) : -1;
						}
					}
				}
			}
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x001228E0 File Offset: 0x00120AE0
		[Conditional("DEBUG")]
		private void Debug_PrintRows()
		{
			for (int i = 0; i < this.RowsInternal.Count; i++)
			{
				for (int j = 0; j < this.RowsInternal[i].ControlsInternal.Count; j++)
				{
				}
			}
		}

		// Token: 0x0600452D RID: 17709 RVA: 0x000072B6 File Offset: 0x000054B6
		[Conditional("DEBUG")]
		private void Debug_VerifyCountRows()
		{
		}

		// Token: 0x0600452E RID: 17710 RVA: 0x00122924 File Offset: 0x00120B24
		[Conditional("DEBUG")]
		private void Debug_VerifyNoOverlaps()
		{
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				foreach (object obj2 in base.Controls)
				{
					Control control2 = (Control)obj2;
					if (control != control2)
					{
						Rectangle bounds = control.Bounds;
						bounds.Intersect(control2.Bounds);
						if (!LayoutUtils.IsZeroWidthOrHeight(bounds))
						{
							ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
							ISupportToolStripPanel supportToolStripPanel2 = control2 as ISupportToolStripPanel;
							string str = string.Format(CultureInfo.CurrentCulture, "OVERLAP detection:\r\n{0}: {1} row {2} row bounds {3}", new object[]
							{
								(control.Name == null) ? "" : control.Name,
								control.Bounds,
								(!this.RowsInternal.Contains(supportToolStripPanel.ToolStripPanelRow)) ? "unknown" : this.RowsInternal.IndexOf(supportToolStripPanel.ToolStripPanelRow).ToString(CultureInfo.CurrentCulture),
								supportToolStripPanel.ToolStripPanelRow.Bounds
							});
							str += string.Format(CultureInfo.CurrentCulture, "\r\n{0}: {1} row {2} row bounds {3}", new object[]
							{
								(control2.Name == null) ? "" : control2.Name,
								control2.Bounds,
								(!this.RowsInternal.Contains(supportToolStripPanel2.ToolStripPanelRow)) ? "unknown" : this.RowsInternal.IndexOf(supportToolStripPanel2.ToolStripPanelRow).ToString(CultureInfo.CurrentCulture),
								supportToolStripPanel2.ToolStripPanelRow.Bounds
							});
						}
					}
				}
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x0600452F RID: 17711 RVA: 0x00122B40 File Offset: 0x00120D40
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.RowsInternal;
			}
		}

		// Token: 0x04002633 RID: 9779
		private Orientation orientation;

		// Token: 0x04002634 RID: 9780
		private static readonly Padding rowMargin = new Padding(3, 0, 0, 0);

		// Token: 0x04002635 RID: 9781
		private Padding scaledRowMargin = ToolStripPanel.rowMargin;

		// Token: 0x04002636 RID: 9782
		private ToolStripRendererSwitcher rendererSwitcher;

		// Token: 0x04002637 RID: 9783
		private Type currentRendererType = typeof(Type);

		// Token: 0x04002638 RID: 9784
		private BitVector32 state;

		// Token: 0x04002639 RID: 9785
		private ToolStripContainer owner;

		// Token: 0x0400263A RID: 9786
		internal static TraceSwitch ToolStripPanelDebug;

		// Token: 0x0400263B RID: 9787
		internal static TraceSwitch ToolStripPanelFeedbackDebug;

		// Token: 0x0400263C RID: 9788
		internal static TraceSwitch ToolStripPanelMissingRowDebug;

		// Token: 0x0400263D RID: 9789
		[ThreadStatic]
		private static Rectangle lastFeedbackRect = Rectangle.Empty;

		// Token: 0x0400263E RID: 9790
		private static readonly int PropToolStripPanelRowCollection = PropertyStore.CreateKey();

		// Token: 0x0400263F RID: 9791
		private static readonly int stateLocked = BitVector32.CreateMask();

		// Token: 0x04002640 RID: 9792
		private static readonly int stateBeginInit = BitVector32.CreateMask(ToolStripPanel.stateLocked);

		// Token: 0x04002641 RID: 9793
		private static readonly int stateChangingZOrder = BitVector32.CreateMask(ToolStripPanel.stateBeginInit);

		// Token: 0x04002642 RID: 9794
		private static readonly int stateInJoin = BitVector32.CreateMask(ToolStripPanel.stateChangingZOrder);

		// Token: 0x04002643 RID: 9795
		private static readonly int stateEndInit = BitVector32.CreateMask(ToolStripPanel.stateInJoin);

		// Token: 0x04002644 RID: 9796
		private static readonly int stateLayoutSuspended = BitVector32.CreateMask(ToolStripPanel.stateEndInit);

		// Token: 0x04002645 RID: 9797
		private static readonly int stateRightToLeftChanged = BitVector32.CreateMask(ToolStripPanel.stateLayoutSuspended);

		// Token: 0x04002646 RID: 9798
		internal static readonly Padding DragMargin = new Padding(10);

		// Token: 0x04002647 RID: 9799
		private static readonly object EventRendererChanged = new object();

		// Token: 0x04002648 RID: 9800
		[ThreadStatic]
		private static ToolStripPanel.FeedbackRectangle feedbackRect;

		// Token: 0x0200080F RID: 2063
		private class FeedbackRectangle : IDisposable
		{
			// Token: 0x06006F51 RID: 28497 RVA: 0x00198748 File Offset: 0x00196948
			public FeedbackRectangle(Rectangle bounds)
			{
				this.dropDown = new ToolStripPanel.FeedbackRectangle.FeedbackDropDown(bounds);
			}

			// Token: 0x17001854 RID: 6228
			// (get) Token: 0x06006F52 RID: 28498 RVA: 0x0019875C File Offset: 0x0019695C
			// (set) Token: 0x06006F53 RID: 28499 RVA: 0x00198780 File Offset: 0x00196980
			public bool Visible
			{
				get
				{
					return this.dropDown != null && !this.dropDown.IsDisposed && this.dropDown.Visible;
				}
				set
				{
					if (this.dropDown != null && !this.dropDown.IsDisposed)
					{
						this.dropDown.Visible = value;
					}
				}
			}

			// Token: 0x06006F54 RID: 28500 RVA: 0x001987A3 File Offset: 0x001969A3
			public void Show(Point newLocation)
			{
				this.dropDown.Show(newLocation);
			}

			// Token: 0x06006F55 RID: 28501 RVA: 0x001987B1 File Offset: 0x001969B1
			public void Move(Point newLocation)
			{
				this.dropDown.MoveTo(newLocation);
			}

			// Token: 0x06006F56 RID: 28502 RVA: 0x001987BF File Offset: 0x001969BF
			protected void Dispose(bool disposing)
			{
				if (disposing && this.dropDown != null)
				{
					this.Visible = false;
					this.dropDown.Dispose();
					this.dropDown = null;
				}
			}

			// Token: 0x06006F57 RID: 28503 RVA: 0x001987E5 File Offset: 0x001969E5
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x06006F58 RID: 28504 RVA: 0x001987F0 File Offset: 0x001969F0
			~FeedbackRectangle()
			{
				this.Dispose(false);
			}

			// Token: 0x04004320 RID: 17184
			private ToolStripPanel.FeedbackRectangle.FeedbackDropDown dropDown;

			// Token: 0x020008CC RID: 2252
			private class FeedbackDropDown : ToolStripDropDown
			{
				// Token: 0x0600731A RID: 29466 RVA: 0x001A538C File Offset: 0x001A358C
				public FeedbackDropDown(Rectangle bounds)
				{
					base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
					base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
					base.SetStyle(ControlStyles.CacheText, true);
					base.AutoClose = false;
					this.AutoSize = false;
					base.DropShadowEnabled = false;
					base.Bounds = bounds;
					Rectangle rect = bounds;
					rect.Inflate(-1, -1);
					Region region = new Region(bounds);
					region.Exclude(rect);
					IntSecurity.ChangeWindowRegionForTopLevel.Assert();
					base.Region = region;
				}

				// Token: 0x0600731B RID: 29467 RVA: 0x001A540C File Offset: 0x001A360C
				private void ForceSynchronousPaint()
				{
					if (!base.IsDisposed && this._numPaintsServiced == 0)
					{
						try
						{
							NativeMethods.MSG msg = default(NativeMethods.MSG);
							while (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, IntPtr.Zero), 15, 15, 1))
							{
								SafeNativeMethods.UpdateWindow(new HandleRef(null, msg.hwnd));
								int numPaintsServiced = this._numPaintsServiced;
								this._numPaintsServiced = numPaintsServiced + 1;
								if (numPaintsServiced > 20)
								{
									break;
								}
							}
						}
						finally
						{
							this._numPaintsServiced = 0;
						}
					}
				}

				// Token: 0x0600731C RID: 29468 RVA: 0x000072B6 File Offset: 0x000054B6
				protected override void OnPaint(PaintEventArgs e)
				{
				}

				// Token: 0x0600731D RID: 29469 RVA: 0x001A5490 File Offset: 0x001A3690
				protected override void OnPaintBackground(PaintEventArgs e)
				{
					base.Renderer.DrawToolStripBackground(new ToolStripRenderEventArgs(e.Graphics, this));
					base.Renderer.DrawToolStripBorder(new ToolStripRenderEventArgs(e.Graphics, this));
				}

				// Token: 0x0600731E RID: 29470 RVA: 0x001A54C0 File Offset: 0x001A36C0
				protected override void OnOpening(CancelEventArgs e)
				{
					base.OnOpening(e);
					e.Cancel = false;
				}

				// Token: 0x0600731F RID: 29471 RVA: 0x001A54D0 File Offset: 0x001A36D0
				public void MoveTo(Point newLocation)
				{
					base.Location = newLocation;
					this.ForceSynchronousPaint();
				}

				// Token: 0x06007320 RID: 29472 RVA: 0x001A54DF File Offset: 0x001A36DF
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				protected override void WndProc(ref Message m)
				{
					if (m.Msg == 132)
					{
						m.Result = (IntPtr)(-1);
					}
					base.WndProc(ref m);
				}

				// Token: 0x0400455B RID: 17755
				private const int MAX_PAINTS_TO_SERVICE = 20;

				// Token: 0x0400455C RID: 17756
				private int _numPaintsServiced;
			}
		}

		// Token: 0x02000810 RID: 2064
		[ListBindable(false)]
		[ComVisible(false)]
		public class ToolStripPanelRowCollection : ArrangedElementCollection, IList, ICollection, IEnumerable
		{
			// Token: 0x06006F59 RID: 28505 RVA: 0x00198820 File Offset: 0x00196A20
			public ToolStripPanelRowCollection(ToolStripPanel owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006F5A RID: 28506 RVA: 0x0019882F File Offset: 0x00196A2F
			public ToolStripPanelRowCollection(ToolStripPanel owner, ToolStripPanelRow[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			// Token: 0x17001855 RID: 6229
			public virtual ToolStripPanelRow this[int index]
			{
				get
				{
					return (ToolStripPanelRow)base.InnerList[index];
				}
			}

			// Token: 0x06006F5C RID: 28508 RVA: 0x00198858 File Offset: 0x00196A58
			public int Add(ToolStripPanelRow value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				int num = base.InnerList.Add(value);
				this.OnAdd(value, num);
				return num;
			}

			// Token: 0x06006F5D RID: 28509 RVA: 0x0019888C File Offset: 0x00196A8C
			public void AddRange(ToolStripPanelRow[] value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ToolStripPanel toolStripPanel = this.owner;
				if (toolStripPanel != null)
				{
					toolStripPanel.SuspendLayout();
				}
				try
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.Add(value[i]);
					}
				}
				finally
				{
					if (toolStripPanel != null)
					{
						toolStripPanel.ResumeLayout();
					}
				}
			}

			// Token: 0x06006F5E RID: 28510 RVA: 0x001988EC File Offset: 0x00196AEC
			public void AddRange(ToolStripPanel.ToolStripPanelRowCollection value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ToolStripPanel toolStripPanel = this.owner;
				if (toolStripPanel != null)
				{
					toolStripPanel.SuspendLayout();
				}
				try
				{
					int count = value.Count;
					for (int i = 0; i < count; i++)
					{
						this.Add(value[i]);
					}
				}
				finally
				{
					if (toolStripPanel != null)
					{
						toolStripPanel.ResumeLayout();
					}
				}
			}

			// Token: 0x06006F5F RID: 28511 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
			public bool Contains(ToolStripPanelRow value)
			{
				return base.InnerList.Contains(value);
			}

			// Token: 0x06006F60 RID: 28512 RVA: 0x00198958 File Offset: 0x00196B58
			public virtual void Clear()
			{
				if (this.owner != null)
				{
					this.owner.SuspendLayout();
				}
				try
				{
					while (this.Count != 0)
					{
						this.RemoveAt(this.Count - 1);
					}
				}
				finally
				{
					if (this.owner != null)
					{
						this.owner.ResumeLayout();
					}
				}
			}

			// Token: 0x06006F61 RID: 28513 RVA: 0x001989B8 File Offset: 0x00196BB8
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x17001856 RID: 6230
			// (get) Token: 0x06006F62 RID: 28514 RVA: 0x0011CD5C File Offset: 0x0011AF5C
			bool IList.IsFixedSize
			{
				get
				{
					return base.InnerList.IsFixedSize;
				}
			}

			// Token: 0x06006F63 RID: 28515 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
			bool IList.Contains(object value)
			{
				return base.InnerList.Contains(value);
			}

			// Token: 0x17001857 RID: 6231
			// (get) Token: 0x06006F64 RID: 28516 RVA: 0x0014D7A3 File Offset: 0x0014B9A3
			bool IList.IsReadOnly
			{
				get
				{
					return base.InnerList.IsReadOnly;
				}
			}

			// Token: 0x06006F65 RID: 28517 RVA: 0x001989C0 File Offset: 0x00196BC0
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			// Token: 0x06006F66 RID: 28518 RVA: 0x001989C9 File Offset: 0x00196BC9
			void IList.Remove(object value)
			{
				this.Remove(value as ToolStripPanelRow);
			}

			// Token: 0x06006F67 RID: 28519 RVA: 0x001989D7 File Offset: 0x00196BD7
			int IList.Add(object value)
			{
				return this.Add(value as ToolStripPanelRow);
			}

			// Token: 0x06006F68 RID: 28520 RVA: 0x001989E5 File Offset: 0x00196BE5
			int IList.IndexOf(object value)
			{
				return this.IndexOf(value as ToolStripPanelRow);
			}

			// Token: 0x06006F69 RID: 28521 RVA: 0x001989F3 File Offset: 0x00196BF3
			void IList.Insert(int index, object value)
			{
				this.Insert(index, value as ToolStripPanelRow);
			}

			// Token: 0x17001858 RID: 6232
			object IList.this[int index]
			{
				get
				{
					return base.InnerList[index];
				}
				set
				{
					throw new NotSupportedException(SR.GetString("ToolStripCollectionMustInsertAndRemove"));
				}
			}

			// Token: 0x06006F6C RID: 28524 RVA: 0x0011CE4C File Offset: 0x0011B04C
			public int IndexOf(ToolStripPanelRow value)
			{
				return base.InnerList.IndexOf(value);
			}

			// Token: 0x06006F6D RID: 28525 RVA: 0x00198A02 File Offset: 0x00196C02
			public void Insert(int index, ToolStripPanelRow value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.InnerList.Insert(index, value);
				this.OnAdd(value, index);
			}

			// Token: 0x06006F6E RID: 28526 RVA: 0x00198A27 File Offset: 0x00196C27
			private void OnAdd(ToolStripPanelRow value, int index)
			{
				if (this.owner != null)
				{
					LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
				}
			}

			// Token: 0x06006F6F RID: 28527 RVA: 0x000072B6 File Offset: 0x000054B6
			private void OnAfterRemove(ToolStripPanelRow row)
			{
			}

			// Token: 0x06006F70 RID: 28528 RVA: 0x00198A42 File Offset: 0x00196C42
			public void Remove(ToolStripPanelRow value)
			{
				base.InnerList.Remove(value);
				this.OnAfterRemove(value);
			}

			// Token: 0x06006F71 RID: 28529 RVA: 0x00198A58 File Offset: 0x00196C58
			public void RemoveAt(int index)
			{
				ToolStripPanelRow row = null;
				if (index < this.Count && index >= 0)
				{
					row = (ToolStripPanelRow)base.InnerList[index];
				}
				base.InnerList.RemoveAt(index);
				this.OnAfterRemove(row);
			}

			// Token: 0x06006F72 RID: 28530 RVA: 0x0011D029 File Offset: 0x0011B229
			public void CopyTo(ToolStripPanelRow[] array, int index)
			{
				base.InnerList.CopyTo(array, index);
			}

			// Token: 0x04004321 RID: 17185
			private ToolStripPanel owner;
		}

		// Token: 0x02000811 RID: 2065
		internal class ToolStripPanelControlCollection : WindowsFormsUtils.TypedControlCollection
		{
			// Token: 0x06006F73 RID: 28531 RVA: 0x00198A99 File Offset: 0x00196C99
			public ToolStripPanelControlCollection(ToolStripPanel owner) : base(owner, typeof(ToolStrip))
			{
				this.owner = owner;
			}

			// Token: 0x06006F74 RID: 28532 RVA: 0x00198AB4 File Offset: 0x00196CB4
			internal override void AddInternal(Control value)
			{
				if (value != null)
				{
					using (new LayoutTransaction(value, value, PropertyNames.Parent))
					{
						base.AddInternal(value);
						return;
					}
				}
				base.AddInternal(value);
			}

			// Token: 0x06006F75 RID: 28533 RVA: 0x00198AFC File Offset: 0x00196CFC
			internal void Sort()
			{
				if (this.owner.Orientation == Orientation.Horizontal)
				{
					base.InnerList.Sort(new ToolStripPanel.ToolStripPanelControlCollection.YXComparer());
					return;
				}
				base.InnerList.Sort(new ToolStripPanel.ToolStripPanelControlCollection.XYComparer());
			}

			// Token: 0x04004322 RID: 17186
			private ToolStripPanel owner;

			// Token: 0x020008CD RID: 2253
			public class XYComparer : IComparer
			{
				// Token: 0x06007322 RID: 29474 RVA: 0x001A5504 File Offset: 0x001A3704
				public int Compare(object first, object second)
				{
					Control control = first as Control;
					Control control2 = second as Control;
					if (control.Bounds.X < control2.Bounds.X)
					{
						return -1;
					}
					if (control.Bounds.X != control2.Bounds.X)
					{
						return 1;
					}
					if (control.Bounds.Y < control2.Bounds.Y)
					{
						return -1;
					}
					return 1;
				}
			}

			// Token: 0x020008CE RID: 2254
			public class YXComparer : IComparer
			{
				// Token: 0x06007324 RID: 29476 RVA: 0x001A5580 File Offset: 0x001A3780
				public int Compare(object first, object second)
				{
					Control control = first as Control;
					Control control2 = second as Control;
					if (control.Bounds.Y < control2.Bounds.Y)
					{
						return -1;
					}
					if (control.Bounds.Y != control2.Bounds.Y)
					{
						return 1;
					}
					if (control.Bounds.X < control2.Bounds.X)
					{
						return -1;
					}
					return 1;
				}
			}
		}
	}
}
