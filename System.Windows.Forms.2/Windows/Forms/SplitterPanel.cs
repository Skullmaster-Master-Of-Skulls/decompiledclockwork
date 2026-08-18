using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000374 RID: 884
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Docking(DockingBehavior.Never)]
	[Designer("System.Windows.Forms.Design.SplitterPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	public sealed class SplitterPanel : Panel
	{
		// Token: 0x060039A6 RID: 14758 RVA: 0x000FFEB8 File Offset: 0x000FE0B8
		public SplitterPanel(SplitContainer owner)
		{
			this.owner = owner;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000FFED0 File Offset: 0x000FE0D0
		// (set) Token: 0x060039A8 RID: 14760 RVA: 0x000FFED8 File Offset: 0x000FE0D8
		internal bool Collapsed
		{
			get
			{
				return this.collapsed;
			}
			set
			{
				this.collapsed = value;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x000FFEE1 File Offset: 0x000FE0E1
		// (set) Token: 0x060039AA RID: 14762 RVA: 0x000FFEE9 File Offset: 0x000FE0E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new bool AutoSize
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

		// Token: 0x140002C2 RID: 706
		// (add) Token: 0x060039AB RID: 14763 RVA: 0x000FFEF2 File Offset: 0x000FE0F2
		// (remove) Token: 0x060039AC RID: 14764 RVA: 0x000FFEFB File Offset: 0x000FE0FB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x00013062 File Offset: 0x00011262
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000072B6 File Offset: 0x000054B6
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

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000FFF04 File Offset: 0x000FE104
		// (set) Token: 0x060039B0 RID: 14768 RVA: 0x000FFF0C File Offset: 0x000FE10C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new AnchorStyles Anchor
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

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000FFF15 File Offset: 0x000FE115
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000FFF1D File Offset: 0x000FE11D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x000FFF26 File Offset: 0x000FE126
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new DockStyle Dock
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

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x00011BDA File Offset: 0x0000FDDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x000FFF2F File Offset: 0x000FE12F
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x000FFF41 File Offset: 0x000FE141
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHeightDescr")]
		public new int Height
		{
			get
			{
				if (this.Collapsed)
				{
					return 0;
				}
				return base.Height;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("SplitContainerPanelHeight"));
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000FFF52 File Offset: 0x000FE152
		// (set) Token: 0x060039B9 RID: 14777 RVA: 0x000FFF5A File Offset: 0x000FE15A
		internal int HeightInternal
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000B184D File Offset: 0x000AFA4D
		// (set) Token: 0x060039BB RID: 14779 RVA: 0x000B1855 File Offset: 0x000AFA55
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000FFF63 File Offset: 0x000FE163
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 0, 0, 0);
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x00011C3F File Offset: 0x0000FE3F
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x000FFF6E File Offset: 0x000FE16E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Size MinimumSize
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

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x00011C22 File Offset: 0x0000FE22
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000FFF77 File Offset: 0x000FE177
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Size MaximumSize
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

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000FFF80 File Offset: 0x000FE180
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000FFF88 File Offset: 0x000FE188
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

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000FFF91 File Offset: 0x000FE191
		internal SplitContainer Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000FFF99 File Offset: 0x000FE199
		// (set) Token: 0x060039C5 RID: 14789 RVA: 0x000FFFA1 File Offset: 0x000FE1A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Control Parent
		{
			get
			{
				return base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060039C6 RID: 14790 RVA: 0x000FFFAA File Offset: 0x000FE1AA
		// (set) Token: 0x060039C7 RID: 14791 RVA: 0x000B2533 File Offset: 0x000B0733
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Size Size
		{
			get
			{
				if (this.Collapsed)
				{
					return Size.Empty;
				}
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x000B25EE File Offset: 0x000B07EE
		// (set) Token: 0x060039C9 RID: 14793 RVA: 0x000B25F6 File Offset: 0x000B07F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060039CA RID: 14794 RVA: 0x000FFFC0 File Offset: 0x000FE1C0
		// (set) Token: 0x060039CB RID: 14795 RVA: 0x000FFFC8 File Offset: 0x000FE1C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060039CC RID: 14796 RVA: 0x000FFFD1 File Offset: 0x000FE1D1
		// (set) Token: 0x060039CD RID: 14797 RVA: 0x000FFFD9 File Offset: 0x000FE1D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000FFFE2 File Offset: 0x000FE1E2
		// (set) Token: 0x060039CF RID: 14799 RVA: 0x000FFFF4 File Offset: 0x000FE1F4
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWidthDescr")]
		public new int Width
		{
			get
			{
				if (this.Collapsed)
				{
					return 0;
				}
				return base.Width;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("SplitContainerPanelWidth"));
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x00100005 File Offset: 0x000FE205
		// (set) Token: 0x060039D1 RID: 14801 RVA: 0x0010000D File Offset: 0x000FE20D
		internal int WidthInternal
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x140002C3 RID: 707
		// (add) Token: 0x060039D2 RID: 14802 RVA: 0x00100016 File Offset: 0x000FE216
		// (remove) Token: 0x060039D3 RID: 14803 RVA: 0x0010001F File Offset: 0x000FE21F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		// Token: 0x140002C4 RID: 708
		// (add) Token: 0x060039D4 RID: 14804 RVA: 0x00100028 File Offset: 0x000FE228
		// (remove) Token: 0x060039D5 RID: 14805 RVA: 0x00100031 File Offset: 0x000FE231
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x140002C5 RID: 709
		// (add) Token: 0x060039D6 RID: 14806 RVA: 0x0010003A File Offset: 0x000FE23A
		// (remove) Token: 0x060039D7 RID: 14807 RVA: 0x00100043 File Offset: 0x000FE243
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x140002C6 RID: 710
		// (add) Token: 0x060039D8 RID: 14808 RVA: 0x000B25FF File Offset: 0x000B07FF
		// (remove) Token: 0x060039D9 RID: 14809 RVA: 0x000B2608 File Offset: 0x000B0808
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x140002C7 RID: 711
		// (add) Token: 0x060039DA RID: 14810 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x060039DB RID: 14811 RVA: 0x000B262B File Offset: 0x000B082B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x040022DA RID: 8922
		private SplitContainer owner;

		// Token: 0x040022DB RID: 8923
		private bool collapsed;
	}
}
