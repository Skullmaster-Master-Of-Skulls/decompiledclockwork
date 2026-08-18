using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003ED RID: 1005
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripContentPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Load")]
	[Docking(DockingBehavior.Never)]
	[InitializationEvent("Load")]
	[ToolboxItem(false)]
	public class ToolStripContentPanel : Panel
	{
		// Token: 0x0600449A RID: 17562 RVA: 0x00121530 File Offset: 0x0011F730
		public ToolStripContentPanel()
		{
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x00013062 File Offset: 0x00011262
		// (set) Token: 0x0600449C RID: 17564 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Localizable(false)]
		public override AutoSizeMode AutoSizeMode
		{
			get
			{
				return AutoSizeMode.GrowOnly;
			}
			set
			{
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x000FFF04 File Offset: 0x000FE104
		// (set) Token: 0x0600449E RID: 17566 RVA: 0x000FFF0C File Offset: 0x000FE10C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x000B0CB7 File Offset: 0x000AEEB7
		// (set) Token: 0x060044A0 RID: 17568 RVA: 0x000EC372 File Offset: 0x000EA572
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x060044A2 RID: 17570 RVA: 0x00011A2B File Offset: 0x0000FC2B
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

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x060044A4 RID: 17572 RVA: 0x00011A3C File Offset: 0x0000FC3C
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

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x000FFEE1 File Offset: 0x000FE0E1
		// (set) Token: 0x060044A6 RID: 17574 RVA: 0x000FFEE9 File Offset: 0x000FE0E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x060044A8 RID: 17576 RVA: 0x00121544 File Offset: 0x0011F744
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (this.ParentInternal is ToolStripContainer && value == Color.Transparent)
				{
					this.ParentInternal.BackColor = Color.Transparent;
				}
				base.BackColor = value;
			}
		}

		// Token: 0x1400035D RID: 861
		// (add) Token: 0x060044A9 RID: 17577 RVA: 0x000FFEF2 File Offset: 0x000FE0F2
		// (remove) Token: 0x060044AA RID: 17578 RVA: 0x000FFEFB File Offset: 0x000FE0FB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x000E2B53 File Offset: 0x000E0D53
		// (set) Token: 0x060044AC RID: 17580 RVA: 0x000E2B5B File Offset: 0x000E0D5B
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

		// Token: 0x1400035E RID: 862
		// (add) Token: 0x060044AD RID: 17581 RVA: 0x000E2B64 File Offset: 0x000E0D64
		// (remove) Token: 0x060044AE RID: 17582 RVA: 0x000E2B6D File Offset: 0x000E0D6D
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

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x000FFF26 File Offset: 0x000FE126
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		// Token: 0x1400035F RID: 863
		// (add) Token: 0x060044B1 RID: 17585 RVA: 0x00100028 File Offset: 0x000FE228
		// (remove) Token: 0x060044B2 RID: 17586 RVA: 0x00100031 File Offset: 0x000FE231
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		// Token: 0x14000360 RID: 864
		// (add) Token: 0x060044B3 RID: 17587 RVA: 0x00121577 File Offset: 0x0011F777
		// (remove) Token: 0x060044B4 RID: 17588 RVA: 0x0012158A File Offset: 0x0011F78A
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripContentPanelOnLoadDescr")]
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(ToolStripContentPanel.EventLoad, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripContentPanel.EventLoad, value);
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x000B184D File Offset: 0x000AFA4D
		// (set) Token: 0x060044B6 RID: 17590 RVA: 0x000B1855 File Offset: 0x000AFA55
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		// Token: 0x14000361 RID: 865
		// (add) Token: 0x060044B7 RID: 17591 RVA: 0x0010003A File Offset: 0x000FE23A
		// (remove) Token: 0x060044B8 RID: 17592 RVA: 0x00100043 File Offset: 0x000FE243
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060044B9 RID: 17593 RVA: 0x00011C3F File Offset: 0x0000FE3F
		// (set) Token: 0x060044BA RID: 17594 RVA: 0x000FFF6E File Offset: 0x000FE16E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060044BB RID: 17595 RVA: 0x00011C22 File Offset: 0x0000FE22
		// (set) Token: 0x060044BC RID: 17596 RVA: 0x000FFF77 File Offset: 0x000FE177
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x000FFF80 File Offset: 0x000FE180
		// (set) Token: 0x060044BE RID: 17598 RVA: 0x000FFF88 File Offset: 0x000FE188
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x000B25EE File Offset: 0x000B07EE
		// (set) Token: 0x060044C0 RID: 17600 RVA: 0x000B25F6 File Offset: 0x000B07F6
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

		// Token: 0x14000362 RID: 866
		// (add) Token: 0x060044C1 RID: 17601 RVA: 0x000B25FF File Offset: 0x000B07FF
		// (remove) Token: 0x060044C2 RID: 17602 RVA: 0x000B2608 File Offset: 0x000B0808
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

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060044C3 RID: 17603 RVA: 0x000FFFC0 File Offset: 0x000FE1C0
		// (set) Token: 0x060044C4 RID: 17604 RVA: 0x000FFFC8 File Offset: 0x000FE1C8
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
				base.TabStop = value;
			}
		}

		// Token: 0x14000363 RID: 867
		// (add) Token: 0x060044C5 RID: 17605 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x060044C6 RID: 17606 RVA: 0x000B262B File Offset: 0x000B082B
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

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x0012159D File Offset: 0x0011F79D
		private ToolStripRendererSwitcher RendererSwitcher
		{
			get
			{
				if (this.rendererSwitcher == null)
				{
					this.rendererSwitcher = new ToolStripRendererSwitcher(this, ToolStripRenderMode.System);
					this.HandleRendererChanged(this, EventArgs.Empty);
					this.rendererSwitcher.RendererChanged += this.HandleRendererChanged;
				}
				return this.rendererSwitcher;
			}
		}

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x001215DD File Offset: 0x0011F7DD
		// (set) Token: 0x060044C9 RID: 17609 RVA: 0x001215EA File Offset: 0x0011F7EA
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

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x001215F8 File Offset: 0x0011F7F8
		// (set) Token: 0x060044CB RID: 17611 RVA: 0x00121605 File Offset: 0x0011F805
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

		// Token: 0x14000364 RID: 868
		// (add) Token: 0x060044CC RID: 17612 RVA: 0x00121613 File Offset: 0x0011F813
		// (remove) Token: 0x060044CD RID: 17613 RVA: 0x00121626 File Offset: 0x0011F826
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripContentPanel.EventRendererChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripContentPanel.EventRendererChanged, value);
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x00121639 File Offset: 0x0011F839
		private void HandleRendererChanged(object sender, EventArgs e)
		{
			this.OnRendererChanged(e);
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x00121642 File Offset: 0x0011F842
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!base.RecreatingHandle)
			{
				this.OnLoad(EventArgs.Empty);
			}
		}

		// Token: 0x060044D0 RID: 17616 RVA: 0x00121660 File Offset: 0x0011F860
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripContentPanel.EventLoad];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060044D1 RID: 17617 RVA: 0x00121690 File Offset: 0x0011F890
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			ToolStripContentPanelRenderEventArgs toolStripContentPanelRenderEventArgs = new ToolStripContentPanelRenderEventArgs(e.Graphics, this);
			this.Renderer.DrawToolStripContentPanelBackground(toolStripContentPanelRenderEventArgs);
			if (!toolStripContentPanelRenderEventArgs.Handled)
			{
				base.OnPaintBackground(e);
			}
		}

		// Token: 0x060044D2 RID: 17618 RVA: 0x001216C8 File Offset: 0x0011F8C8
		protected virtual void OnRendererChanged(EventArgs e)
		{
			if (this.Renderer is ToolStripProfessionalRenderer)
			{
				this.state[ToolStripContentPanel.stateLastDoubleBuffer] = this.DoubleBuffered;
				base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			}
			else
			{
				this.DoubleBuffered = this.state[ToolStripContentPanel.stateLastDoubleBuffer];
			}
			this.Renderer.InitializeContentPanel(this);
			base.Invalidate();
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripContentPanel.EventRendererChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x0012174F File Offset: 0x0011F94F
		private void ResetRenderMode()
		{
			this.RendererSwitcher.ResetRenderMode();
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x0012175C File Offset: 0x0011F95C
		private bool ShouldSerializeRenderMode()
		{
			return this.RendererSwitcher.ShouldSerializeRenderMode();
		}

		// Token: 0x0400262E RID: 9774
		private ToolStripRendererSwitcher rendererSwitcher;

		// Token: 0x0400262F RID: 9775
		private BitVector32 state;

		// Token: 0x04002630 RID: 9776
		private static readonly int stateLastDoubleBuffer = BitVector32.CreateMask();

		// Token: 0x04002631 RID: 9777
		private static readonly object EventRendererChanged = new object();

		// Token: 0x04002632 RID: 9778
		private static readonly object EventLoad = new object();
	}
}
