using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200036E RID: 878
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("SplitterMoved")]
	[Docking(DockingBehavior.AutoDock)]
	[Designer("System.Windows.Forms.Design.SplitContainerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionSplitContainer")]
	public class SplitContainer : ContainerControl, ISupportInitialize
	{
		// Token: 0x060038BC RID: 14524 RVA: 0x000FC514 File Offset: 0x000FA714
		public SplitContainer()
		{
			this.panel1 = new SplitterPanel(this);
			this.panel2 = new SplitterPanel(this);
			this.splitterRect = default(Rectangle);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			((WindowsFormsUtils.TypedControlCollection)this.Controls).AddInternal(this.panel1);
			((WindowsFormsUtils.TypedControlCollection)this.Controls).AddInternal(this.panel2);
			this.UpdateSplitter();
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060038BD RID: 14525 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x060038BE RID: 14526 RVA: 0x000EC372 File Offset: 0x000EA572
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("FormAutoScrollDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get
			{
				return false;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x000FC5FA File Offset: 0x000FA7FA
		// (set) Token: 0x060038C0 RID: 14528 RVA: 0x000FC602 File Offset: 0x000FA802
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(typeof(Point), "0, 0")]
		public override Point AutoScrollOffset
		{
			get
			{
				return base.AutoScrollOffset;
			}
			set
			{
				base.AutoScrollOffset = value;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060038C1 RID: 14529 RVA: 0x00011A34 File Offset: 0x0000FC34
		// (set) Token: 0x060038C2 RID: 14530 RVA: 0x00011A3C File Offset: 0x0000FC3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060038C3 RID: 14531 RVA: 0x00011A23 File Offset: 0x0000FC23
		// (set) Token: 0x060038C4 RID: 14532 RVA: 0x00011A2B File Offset: 0x0000FC2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x000FC60B File Offset: 0x000FA80B
		// (set) Token: 0x060038C6 RID: 14534 RVA: 0x000FC613 File Offset: 0x000FA813
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormAutoScrollPositionDescr")]
		public new Point AutoScrollPosition
		{
			get
			{
				return base.AutoScrollPosition;
			}
			set
			{
				base.AutoScrollPosition = value;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060038C7 RID: 14535 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x060038C8 RID: 14536 RVA: 0x00011A4D File Offset: 0x0000FC4D
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

		// Token: 0x140002AB RID: 683
		// (add) Token: 0x060038C9 RID: 14537 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x060038CA RID: 14538 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060038CB RID: 14539 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060038CC RID: 14540 RVA: 0x00011A98 File Offset: 0x0000FC98
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override Image BackgroundImage
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

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060038CD RID: 14541 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060038CE RID: 14542 RVA: 0x00011ABB File Offset: 0x0000FCBB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060038CF RID: 14543 RVA: 0x0002FD2D File Offset: 0x0002DF2D
		// (set) Token: 0x060038D0 RID: 14544 RVA: 0x0002FD35 File Offset: 0x0002DF35
		[Browsable(false)]
		[SRDescription("ContainerControlBindingContextDescr")]
		public override BindingContext BindingContext
		{
			get
			{
				return base.BindingContextInternal;
			}
			set
			{
				base.BindingContextInternal = value;
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000FC61C File Offset: 0x000FA81C
		// (set) Token: 0x060038D2 RID: 14546 RVA: 0x000FC624 File Offset: 0x000FA824
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[DispId(-504)]
		[SRDescription("SplitterBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.Invalidate();
					this.SetInnerMostBorder(this);
					if (this.ParentInternal != null && this.ParentInternal is SplitterPanel)
					{
						SplitContainer owner = ((SplitterPanel)this.ParentInternal).Owner;
						owner.SetInnerMostBorder(owner);
					}
				}
				switch (this.BorderStyle)
				{
				case BorderStyle.None:
					this.BORDERSIZE = 0;
					return;
				case BorderStyle.FixedSingle:
					this.BORDERSIZE = 1;
					return;
				case BorderStyle.Fixed3D:
					this.BORDERSIZE = 4;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060038D3 RID: 14547 RVA: 0x000EC606 File Offset: 0x000EA806
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x140002AC RID: 684
		// (add) Token: 0x060038D4 RID: 14548 RVA: 0x000FC6D2 File Offset: 0x000FA8D2
		// (remove) Token: 0x060038D5 RID: 14549 RVA: 0x000FC6DB File Offset: 0x000FA8DB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlAdded
		{
			add
			{
				base.ControlAdded += value;
			}
			remove
			{
				base.ControlAdded -= value;
			}
		}

		// Token: 0x140002AD RID: 685
		// (add) Token: 0x060038D6 RID: 14550 RVA: 0x000FC6E4 File Offset: 0x000FA8E4
		// (remove) Token: 0x060038D7 RID: 14551 RVA: 0x000FC6ED File Offset: 0x000FA8ED
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlRemoved
		{
			add
			{
				base.ControlRemoved += value;
			}
			remove
			{
				base.ControlRemoved -= value;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x000FC700 File Offset: 0x000FA900
		public new DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
				if (this.ParentInternal != null && this.ParentInternal is SplitterPanel)
				{
					SplitContainer owner = ((SplitterPanel)this.ParentInternal).Owner;
					owner.SetInnerMostBorder(owner);
				}
				this.ResizeSplitContainer();
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000FC747 File Offset: 0x000FA947
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 100);
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060038DB RID: 14555 RVA: 0x000FC755 File Offset: 0x000FA955
		// (set) Token: 0x060038DC RID: 14556 RVA: 0x000FC760 File Offset: 0x000FA960
		[DefaultValue(FixedPanel.None)]
		[SRCategory("CatLayout")]
		[SRDescription("SplitContainerFixedPanelDescr")]
		public FixedPanel FixedPanel
		{
			get
			{
				return this.fixedPanel;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FixedPanel));
				}
				if (this.fixedPanel != value)
				{
					this.fixedPanel = value;
					FixedPanel fixedPanel = this.fixedPanel;
					if (fixedPanel == FixedPanel.Panel2)
					{
						if (this.Orientation == Orientation.Vertical)
						{
							this.panelSize = base.Width - this.SplitterDistanceInternal - this.SplitterWidthInternal;
							return;
						}
						this.panelSize = base.Height - this.SplitterDistanceInternal - this.SplitterWidthInternal;
						return;
					}
					else
					{
						this.panelSize = this.SplitterDistanceInternal;
					}
				}
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000FC7F9 File Offset: 0x000FA9F9
		// (set) Token: 0x060038DE RID: 14558 RVA: 0x000FC801 File Offset: 0x000FAA01
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("SplitContainerIsSplitterFixedDescr")]
		public bool IsSplitterFixed
		{
			get
			{
				return this.splitterFixed;
			}
			set
			{
				this.splitterFixed = value;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000FC80C File Offset: 0x000FAA0C
		private bool IsSplitterMovable
		{
			get
			{
				if (this.Orientation == Orientation.Vertical)
				{
					return base.Width >= this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
				}
				return base.Height >= this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060038E0 RID: 14560 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool IsContainerControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x000FC861 File Offset: 0x000FAA61
		// (set) Token: 0x060038E2 RID: 14562 RVA: 0x000FC86C File Offset: 0x000FAA6C
		[SRCategory("CatBehavior")]
		[DefaultValue(Orientation.Vertical)]
		[Localizable(true)]
		[SRDescription("SplitContainerOrientationDescr")]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Orientation));
				}
				if (this.orientation != value)
				{
					this.orientation = value;
					this.splitDistance = 0;
					this.SplitterDistance = this.SplitterDistanceInternal;
					this.UpdateSplitter();
				}
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000FC8C8 File Offset: 0x000FAAC8
		// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000FC8D0 File Offset: 0x000FAAD0
		private Cursor OverrideCursor
		{
			get
			{
				return this.overrideCursor;
			}
			set
			{
				if (this.overrideCursor != value)
				{
					this.overrideCursor = value;
					if (base.IsHandleCreated)
					{
						NativeMethods.POINT point = new NativeMethods.POINT();
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetCursorPos(point);
						UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
						if ((rect.left <= point.x && point.x < rect.right && rect.top <= point.y && point.y < rect.bottom) || UnsafeNativeMethods.GetCapture() == base.Handle)
						{
							base.SendMessage(32, base.Handle, 1);
						}
					}
				}
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000FC983 File Offset: 0x000FAB83
		private bool CollapsedMode
		{
			get
			{
				return this.Panel1Collapsed || this.Panel2Collapsed;
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000FC995 File Offset: 0x000FAB95
		[SRCategory("CatAppearance")]
		[SRDescription("SplitContainerPanel1Descr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SplitterPanel Panel1
		{
			get
			{
				return this.panel1;
			}
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000FC99D File Offset: 0x000FAB9D
		private void CollapsePanel(SplitterPanel p, bool collapsing)
		{
			p.Collapsed = collapsing;
			if (collapsing)
			{
				p.Visible = false;
			}
			else
			{
				p.Visible = true;
			}
			this.UpdateSplitter();
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060038E8 RID: 14568 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x060038E9 RID: 14569 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x140002AE RID: 686
		// (add) Token: 0x060038EA RID: 14570 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x060038EB RID: 14571 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000FC9BF File Offset: 0x000FABBF
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x000FC9CC File Offset: 0x000FABCC
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[SRDescription("SplitContainerPanel1CollapsedDescr")]
		public bool Panel1Collapsed
		{
			get
			{
				return this.panel1.Collapsed;
			}
			set
			{
				if (value != this.panel1.Collapsed)
				{
					if (value && this.panel2.Collapsed)
					{
						this.CollapsePanel(this.panel2, false);
					}
					this.CollapsePanel(this.panel1, value);
				}
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000FCA06 File Offset: 0x000FAC06
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x000FCA13 File Offset: 0x000FAC13
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[SRDescription("SplitContainerPanel2CollapsedDescr")]
		public bool Panel2Collapsed
		{
			get
			{
				return this.panel2.Collapsed;
			}
			set
			{
				if (value != this.panel2.Collapsed)
				{
					if (value && this.panel1.Collapsed)
					{
						this.CollapsePanel(this.panel1, false);
					}
					this.CollapsePanel(this.panel2, value);
				}
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000FCA4D File Offset: 0x000FAC4D
		// (set) Token: 0x060038F1 RID: 14577 RVA: 0x000FCA55 File Offset: 0x000FAC55
		[SRCategory("CatLayout")]
		[DefaultValue(25)]
		[Localizable(true)]
		[SRDescription("SplitContainerPanel1MinSizeDescr")]
		[RefreshProperties(RefreshProperties.All)]
		public int Panel1MinSize
		{
			get
			{
				return this.panel1MinSize;
			}
			set
			{
				this.newPanel1MinSize = value;
				if (value != this.Panel1MinSize && !this.initializing)
				{
					this.ApplyPanel1MinSize(value);
				}
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000FCA76 File Offset: 0x000FAC76
		[SRCategory("CatAppearance")]
		[SRDescription("SplitContainerPanel2Descr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SplitterPanel Panel2
		{
			get
			{
				return this.panel2;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x000FCA7E File Offset: 0x000FAC7E
		// (set) Token: 0x060038F4 RID: 14580 RVA: 0x000FCA86 File Offset: 0x000FAC86
		[SRCategory("CatLayout")]
		[DefaultValue(25)]
		[Localizable(true)]
		[SRDescription("SplitContainerPanel2MinSizeDescr")]
		[RefreshProperties(RefreshProperties.All)]
		public int Panel2MinSize
		{
			get
			{
				return this.panel2MinSize;
			}
			set
			{
				this.newPanel2MinSize = value;
				if (value != this.Panel2MinSize && !this.initializing)
				{
					this.ApplyPanel2MinSize(value);
				}
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000FCAA7 File Offset: 0x000FACA7
		// (set) Token: 0x060038F6 RID: 14582 RVA: 0x000FCAB0 File Offset: 0x000FACB0
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SettingsBindable(true)]
		[SRDescription("SplitContainerSplitterDistanceDescr")]
		[DefaultValue(50)]
		public int SplitterDistance
		{
			get
			{
				return this.splitDistance;
			}
			set
			{
				if (value != this.SplitterDistance)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("SplitterDistance", SR.GetString("InvalidLowBoundArgument", new object[]
						{
							"SplitterDistance",
							value.ToString(CultureInfo.CurrentCulture),
							"0"
						}));
					}
					try
					{
						this.setSplitterDistance = true;
						if (this.Orientation == Orientation.Vertical)
						{
							if (value < this.Panel1MinSize)
							{
								value = this.Panel1MinSize;
							}
							if (value + this.SplitterWidthInternal > base.Width - this.Panel2MinSize)
							{
								value = base.Width - this.Panel2MinSize - this.SplitterWidthInternal;
							}
							if (value < 0)
							{
								throw new InvalidOperationException(SR.GetString("SplitterDistanceNotAllowed"));
							}
							this.splitDistance = value;
							this.splitterDistance = value;
							this.panel1.WidthInternal = this.SplitterDistance;
						}
						else
						{
							if (value < this.Panel1MinSize)
							{
								value = this.Panel1MinSize;
							}
							if (value + this.SplitterWidthInternal > base.Height - this.Panel2MinSize)
							{
								value = base.Height - this.Panel2MinSize - this.SplitterWidthInternal;
							}
							if (value < 0)
							{
								throw new InvalidOperationException(SR.GetString("SplitterDistanceNotAllowed"));
							}
							this.splitDistance = value;
							this.splitterDistance = value;
							this.panel1.HeightInternal = this.SplitterDistance;
						}
						FixedPanel fixedPanel = this.fixedPanel;
						if (fixedPanel != FixedPanel.Panel1)
						{
							if (fixedPanel == FixedPanel.Panel2)
							{
								if (this.Orientation == Orientation.Vertical)
								{
									this.panelSize = base.Width - this.SplitterDistance - this.SplitterWidthInternal;
								}
								else
								{
									this.panelSize = base.Height - this.SplitterDistance - this.SplitterWidthInternal;
								}
							}
						}
						else
						{
							this.panelSize = this.SplitterDistance;
						}
						this.UpdateSplitter();
					}
					finally
					{
						this.setSplitterDistance = false;
					}
					this.OnSplitterMoved(new SplitterEventArgs(this.SplitterRectangle.X + this.SplitterRectangle.Width / 2, this.SplitterRectangle.Y + this.SplitterRectangle.Height / 2, this.SplitterRectangle.X, this.SplitterRectangle.Y));
				}
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000FCCEC File Offset: 0x000FAEEC
		// (set) Token: 0x060038F8 RID: 14584 RVA: 0x000FCCF4 File Offset: 0x000FAEF4
		private int SplitterDistanceInternal
		{
			get
			{
				return this.splitterDistance;
			}
			set
			{
				this.SplitterDistance = value;
			}
		}

		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060038F9 RID: 14585 RVA: 0x000FCCFD File Offset: 0x000FAEFD
		// (set) Token: 0x060038FA RID: 14586 RVA: 0x000FCD08 File Offset: 0x000FAF08
		[SRCategory("CatLayout")]
		[DefaultValue(1)]
		[Localizable(true)]
		[SRDescription("SplitContainerSplitterIncrementDescr")]
		public int SplitterIncrement
		{
			get
			{
				return this.splitterInc;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("SplitterIncrement", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"SplitterIncrement",
						value.ToString(CultureInfo.CurrentCulture),
						"1"
					}));
				}
				this.splitterInc = value;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060038FB RID: 14587 RVA: 0x000FCD5C File Offset: 0x000FAF5C
		[SRCategory("CatLayout")]
		[SRDescription("SplitContainerSplitterRectangleDescr")]
		[Browsable(false)]
		public Rectangle SplitterRectangle
		{
			get
			{
				Rectangle result = this.splitterRect;
				result.X = this.splitterRect.X - base.Left;
				result.Y = this.splitterRect.Y - base.Top;
				return result;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000FCDA3 File Offset: 0x000FAFA3
		// (set) Token: 0x060038FD RID: 14589 RVA: 0x000FCDAB File Offset: 0x000FAFAB
		[SRCategory("CatLayout")]
		[SRDescription("SplitContainerSplitterWidthDescr")]
		[Localizable(true)]
		[DefaultValue(4)]
		public int SplitterWidth
		{
			get
			{
				return this.splitterWidth;
			}
			set
			{
				this.newSplitterWidth = value;
				if (value != this.SplitterWidth && !this.initializing)
				{
					this.ApplySplitterWidth(value);
				}
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000FCDCC File Offset: 0x000FAFCC
		private int SplitterWidthInternal
		{
			get
			{
				if (!this.CollapsedMode)
				{
					return this.splitterWidth;
				}
				return 0;
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000FCDDE File Offset: 0x000FAFDE
		// (set) Token: 0x06003900 RID: 14592 RVA: 0x000FCDE6 File Offset: 0x000FAFE6
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[DispId(-516)]
		[SRDescription("ControlTabStopDescr")]
		public new bool TabStop
		{
			get
			{
				return this.tabStop;
			}
			set
			{
				if (this.TabStop != value)
				{
					this.tabStop = value;
					this.OnTabStopChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06003902 RID: 14594 RVA: 0x00024185 File Offset: 0x00022385
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
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

		// Token: 0x06003903 RID: 14595 RVA: 0x000FCE03 File Offset: 0x000FB003
		public void BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000FCE0C File Offset: 0x000FB00C
		public void EndInit()
		{
			this.initializing = false;
			if (this.newPanel1MinSize != this.panel1MinSize)
			{
				this.ApplyPanel1MinSize(this.newPanel1MinSize);
			}
			if (this.newPanel2MinSize != this.panel2MinSize)
			{
				this.ApplyPanel2MinSize(this.newPanel2MinSize);
			}
			if (this.newSplitterWidth != this.splitterWidth)
			{
				this.ApplySplitterWidth(this.newSplitterWidth);
			}
		}

		// Token: 0x140002AF RID: 687
		// (add) Token: 0x06003905 RID: 14597 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06003906 RID: 14598 RVA: 0x00011AAA File Offset: 0x0000FCAA
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		// Token: 0x140002B0 RID: 688
		// (add) Token: 0x06003907 RID: 14599 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06003908 RID: 14600 RVA: 0x00011ACD File Offset: 0x0000FCCD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		// Token: 0x140002B1 RID: 689
		// (add) Token: 0x06003909 RID: 14601 RVA: 0x000FCE6E File Offset: 0x000FB06E
		// (remove) Token: 0x0600390A RID: 14602 RVA: 0x000FCE81 File Offset: 0x000FB081
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovingDescr")]
		public event SplitterCancelEventHandler SplitterMoving
		{
			add
			{
				base.Events.AddHandler(SplitContainer.EVENT_MOVING, value);
			}
			remove
			{
				base.Events.RemoveHandler(SplitContainer.EVENT_MOVING, value);
			}
		}

		// Token: 0x140002B2 RID: 690
		// (add) Token: 0x0600390B RID: 14603 RVA: 0x000FCE94 File Offset: 0x000FB094
		// (remove) Token: 0x0600390C RID: 14604 RVA: 0x000FCEA7 File Offset: 0x000FB0A7
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovedDescr")]
		public event SplitterEventHandler SplitterMoved
		{
			add
			{
				base.Events.AddHandler(SplitContainer.EVENT_MOVED, value);
			}
			remove
			{
				base.Events.RemoveHandler(SplitContainer.EVENT_MOVED, value);
			}
		}

		// Token: 0x140002B3 RID: 691
		// (add) Token: 0x0600390D RID: 14605 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x0600390E RID: 14606 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x0600390F RID: 14607 RVA: 0x000243C3 File Offset: 0x000225C3
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x06003910 RID: 14608 RVA: 0x000FCEBC File Offset: 0x000FB0BC
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.IsSplitterMovable && !this.IsSplitterFixed)
			{
				if (e.KeyData == Keys.Escape && this.splitBegin)
				{
					this.splitBegin = false;
					this.splitBreak = true;
					return;
				}
				if (e.KeyData == Keys.Right || e.KeyData == Keys.Down || e.KeyData == Keys.Left || (e.KeyData == Keys.Up && this.splitterFocused))
				{
					if (this.splitBegin)
					{
						this.splitMove = true;
					}
					if (e.KeyData == Keys.Left || (e.KeyData == Keys.Up && this.splitterFocused))
					{
						this.splitterDistance -= this.SplitterIncrement;
						this.splitterDistance = ((this.splitterDistance < this.Panel1MinSize) ? (this.splitterDistance + this.SplitterIncrement) : Math.Max(this.splitterDistance, this.BORDERSIZE));
					}
					if (e.KeyData == Keys.Right || (e.KeyData == Keys.Down && this.splitterFocused))
					{
						this.splitterDistance += this.SplitterIncrement;
						if (this.Orientation == Orientation.Vertical)
						{
							this.splitterDistance = ((this.splitterDistance + this.SplitterWidth > base.Width - this.Panel2MinSize - this.BORDERSIZE) ? (this.splitterDistance - this.SplitterIncrement) : this.splitterDistance);
						}
						else
						{
							this.splitterDistance = ((this.splitterDistance + this.SplitterWidth > base.Height - this.Panel2MinSize - this.BORDERSIZE) ? (this.splitterDistance - this.SplitterIncrement) : this.splitterDistance);
						}
					}
					if (!this.splitBegin)
					{
						this.splitBegin = true;
					}
					if (this.splitBegin && !this.splitMove)
					{
						this.initialSplitterDistance = this.SplitterDistanceInternal;
						this.DrawSplitBar(1);
						return;
					}
					this.DrawSplitBar(2);
					Rectangle rectangle = this.CalcSplitLine(this.splitterDistance, 0);
					int x = rectangle.X;
					int y = rectangle.Y;
					SplitterCancelEventArgs splitterCancelEventArgs = new SplitterCancelEventArgs(base.Left + this.SplitterRectangle.X + this.SplitterRectangle.Width / 2, base.Top + this.SplitterRectangle.Y + this.SplitterRectangle.Height / 2, x, y);
					this.OnSplitterMoving(splitterCancelEventArgs);
					if (splitterCancelEventArgs.Cancel)
					{
						this.SplitEnd(false);
					}
				}
			}
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000FD134 File Offset: 0x000FB334
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (this.splitBegin && this.IsSplitterMovable && (e.KeyData == Keys.Right || e.KeyData == Keys.Down || e.KeyData == Keys.Left || (e.KeyData == Keys.Up && this.splitterFocused)))
			{
				this.DrawSplitBar(3);
				this.ApplySplitterDistance();
				this.splitBegin = false;
				this.splitMove = false;
			}
			if (this.splitBreak)
			{
				this.splitBreak = false;
				this.SplitEnd(false);
			}
			using (Graphics graphics = base.CreateGraphicsInternal())
			{
				if (this.BackgroundImage == null)
				{
					using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
					{
						graphics.FillRectangle(solidBrush, this.SplitterRectangle);
					}
				}
				this.DrawFocus(graphics, this.SplitterRectangle);
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000FD224 File Offset: 0x000FB424
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.SetInnerMostBorder(this);
			if (this.IsSplitterMovable && !this.setSplitterDistance)
			{
				this.ResizeSplitContainer();
			}
			base.OnLayout(e);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000FD24A File Offset: 0x000FB44A
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000FD25C File Offset: 0x000FB45C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!this.IsSplitterFixed && this.IsSplitterMovable)
			{
				if (this.Cursor == this.DefaultCursor && this.SplitterRectangle.Contains(e.Location))
				{
					if (this.Orientation == Orientation.Vertical)
					{
						this.OverrideCursor = Cursors.VSplit;
					}
					else
					{
						this.OverrideCursor = Cursors.HSplit;
					}
				}
				else
				{
					this.OverrideCursor = null;
				}
				if (this.splitterClick)
				{
					int num = e.X;
					int num2 = e.Y;
					this.splitterDrag = true;
					this.SplitMove(num, num2);
					if (this.Orientation == Orientation.Vertical)
					{
						num = Math.Max(Math.Min(num, base.Width - this.Panel2MinSize), this.Panel1MinSize);
						num2 = Math.Max(num2, 0);
					}
					else
					{
						num2 = Math.Max(Math.Min(num2, base.Height - this.Panel2MinSize), this.Panel1MinSize);
						num = Math.Max(num, 0);
					}
					Rectangle rectangle = this.CalcSplitLine(this.GetSplitterDistance(e.X, e.Y), 0);
					int x = rectangle.X;
					int y = rectangle.Y;
					SplitterCancelEventArgs splitterCancelEventArgs = new SplitterCancelEventArgs(num, num2, x, y);
					this.OnSplitterMoving(splitterCancelEventArgs);
					if (splitterCancelEventArgs.Cancel)
					{
						this.SplitEnd(false);
					}
				}
			}
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000FD3AB File Offset: 0x000FB5AB
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (!base.Enabled)
			{
				return;
			}
			this.OverrideCursor = null;
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000FD3C4 File Offset: 0x000FB5C4
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.IsSplitterMovable && this.SplitterRectangle.Contains(e.Location))
			{
				if (!base.Enabled)
				{
					return;
				}
				if (e.Button == MouseButtons.Left && e.Clicks == 1 && !this.IsSplitterFixed)
				{
					this.splitterFocused = true;
					IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
					if (containerControlInternal != null)
					{
						ContainerControl containerControl = containerControlInternal as ContainerControl;
						if (containerControl == null)
						{
							containerControlInternal.ActiveControl = this;
						}
						else
						{
							containerControl.SetActiveControlInternal(this);
						}
					}
					base.SetActiveControlInternal(null);
					this.nextActiveControl = this.panel2;
					this.SplitBegin(e.X, e.Y);
					this.splitterClick = true;
				}
			}
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000FD480 File Offset: 0x000FB680
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (!base.Enabled)
			{
				return;
			}
			if (!this.IsSplitterFixed && this.IsSplitterMovable && this.splitterClick)
			{
				base.CaptureInternal = false;
				if (this.splitterDrag)
				{
					this.CalcSplitLine(this.GetSplitterDistance(e.X, e.Y), 0);
					this.SplitEnd(true);
				}
				else
				{
					this.SplitEnd(false);
				}
				this.splitterClick = false;
				this.splitterDrag = false;
			}
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000FD4FC File Offset: 0x000FB6FC
		protected override void OnMove(EventArgs e)
		{
			base.OnMove(e);
			this.SetSplitterRect(this.Orientation == Orientation.Vertical);
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000FD514 File Offset: 0x000FB714
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.Focused)
			{
				this.DrawFocus(e.Graphics, this.SplitterRectangle);
			}
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000FD538 File Offset: 0x000FB738
		public void OnSplitterMoving(SplitterCancelEventArgs e)
		{
			SplitterCancelEventHandler splitterCancelEventHandler = (SplitterCancelEventHandler)base.Events[SplitContainer.EVENT_MOVING];
			if (splitterCancelEventHandler != null)
			{
				splitterCancelEventHandler(this, e);
			}
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000FD568 File Offset: 0x000FB768
		public void OnSplitterMoved(SplitterEventArgs e)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[SplitContainer.EVENT_MOVED];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, e);
			}
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000FD596 File Offset: 0x000FB796
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			this.panel1.RightToLeft = this.RightToLeft;
			this.panel2.RightToLeft = this.RightToLeft;
			this.UpdateSplitter();
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000FD5C8 File Offset: 0x000FB7C8
		private void ApplyPanel1MinSize(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Panel1MinSize", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"Panel1MinSize",
					value.ToString(CultureInfo.CurrentCulture),
					"0"
				}));
			}
			if (this.Orientation == Orientation.Vertical)
			{
				if (base.DesignMode && base.Width != this.DefaultSize.Width && value + this.Panel2MinSize + this.SplitterWidth > base.Width)
				{
					throw new ArgumentOutOfRangeException("Panel1MinSize", SR.GetString("InvalidArgument", new object[]
					{
						"Panel1MinSize",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else if (this.Orientation == Orientation.Horizontal && base.DesignMode && base.Height != this.DefaultSize.Height && value + this.Panel2MinSize + this.SplitterWidth > base.Height)
			{
				throw new ArgumentOutOfRangeException("Panel1MinSize", SR.GetString("InvalidArgument", new object[]
				{
					"Panel1MinSize",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.panel1MinSize = value;
			if (value > this.SplitterDistanceInternal)
			{
				this.SplitterDistanceInternal = value;
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000FD714 File Offset: 0x000FB914
		private void ApplyPanel2MinSize(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Panel2MinSize", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"Panel2MinSize",
					value.ToString(CultureInfo.CurrentCulture),
					"0"
				}));
			}
			if (this.Orientation == Orientation.Vertical)
			{
				if (base.DesignMode && base.Width != this.DefaultSize.Width && value + this.Panel1MinSize + this.SplitterWidth > base.Width)
				{
					throw new ArgumentOutOfRangeException("Panel2MinSize", SR.GetString("InvalidArgument", new object[]
					{
						"Panel2MinSize",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else if (this.Orientation == Orientation.Horizontal && base.DesignMode && base.Height != this.DefaultSize.Height && value + this.Panel1MinSize + this.SplitterWidth > base.Height)
			{
				throw new ArgumentOutOfRangeException("Panel2MinSize", SR.GetString("InvalidArgument", new object[]
				{
					"Panel2MinSize",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.panel2MinSize = value;
			if (value > this.Panel2.Width)
			{
				this.SplitterDistanceInternal = this.Panel2.Width + this.SplitterWidthInternal;
			}
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000FD874 File Offset: 0x000FBA74
		private void ApplySplitterWidth(int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("SplitterWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"SplitterWidth",
					value.ToString(CultureInfo.CurrentCulture),
					"1"
				}));
			}
			if (this.Orientation == Orientation.Vertical)
			{
				if (base.DesignMode && value + this.Panel1MinSize + this.Panel2MinSize > base.Width)
				{
					throw new ArgumentOutOfRangeException("SplitterWidth", SR.GetString("InvalidArgument", new object[]
					{
						"SplitterWidth",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else if (this.Orientation == Orientation.Horizontal && base.DesignMode && value + this.Panel1MinSize + this.Panel2MinSize > base.Height)
			{
				throw new ArgumentOutOfRangeException("SplitterWidth", SR.GetString("InvalidArgument", new object[]
				{
					"SplitterWidth",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.splitterWidth = value;
			this.UpdateSplitter();
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000FD988 File Offset: 0x000FBB88
		private void ApplySplitterDistance()
		{
			using (new LayoutTransaction(this, this, "SplitterDistance", false))
			{
				this.SplitterDistanceInternal = this.splitterDistance;
			}
			if (this.BackColor == Color.Transparent)
			{
				base.Invalidate();
			}
			if (this.Orientation != Orientation.Vertical)
			{
				this.splitterRect.Y = base.Location.Y + this.SplitterDistanceInternal;
				return;
			}
			if (this.RightToLeft == RightToLeft.No)
			{
				this.splitterRect.X = base.Location.X + this.SplitterDistanceInternal;
				return;
			}
			this.splitterRect.X = base.Right - this.SplitterDistanceInternal - this.SplitterWidthInternal;
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000FDA58 File Offset: 0x000FBC58
		private Rectangle CalcSplitLine(int splitSize, int minWeight)
		{
			Rectangle result = default(Rectangle);
			Orientation orientation = this.Orientation;
			if (orientation != Orientation.Horizontal)
			{
				if (orientation == Orientation.Vertical)
				{
					result.Width = this.SplitterWidthInternal;
					result.Height = base.Height;
					if (result.Width < minWeight)
					{
						result.Width = minWeight;
					}
					if (this.RightToLeft == RightToLeft.No)
					{
						result.X = this.panel1.Location.X + splitSize;
					}
					else
					{
						result.X = base.Width - splitSize - this.SplitterWidthInternal;
					}
				}
			}
			else
			{
				result.Width = base.Width;
				result.Height = this.SplitterWidthInternal;
				if (result.Width < minWeight)
				{
					result.Width = minWeight;
				}
				result.Y = this.panel1.Location.Y + splitSize;
			}
			return result;
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000FDB34 File Offset: 0x000FBD34
		private void DrawSplitBar(int mode)
		{
			if (mode != 1 && this.lastDrawSplit != -1)
			{
				this.DrawSplitHelper(this.lastDrawSplit);
				this.lastDrawSplit = -1;
			}
			else if (mode != 1 && this.lastDrawSplit == -1)
			{
				return;
			}
			if (mode == 3)
			{
				if (this.lastDrawSplit != -1)
				{
					this.DrawSplitHelper(this.lastDrawSplit);
				}
				this.lastDrawSplit = -1;
				return;
			}
			if (this.splitMove || this.splitBegin)
			{
				this.DrawSplitHelper(this.splitterDistance);
				this.lastDrawSplit = this.splitterDistance;
				return;
			}
			this.DrawSplitHelper(this.splitterDistance);
			this.lastDrawSplit = this.splitterDistance;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000FDBD3 File Offset: 0x000FBDD3
		private void DrawFocus(Graphics g, Rectangle r)
		{
			r.Inflate(-1, -1);
			ControlPaint.DrawFocusRectangle(g, r, this.ForeColor, this.BackColor);
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000FDBF4 File Offset: 0x000FBDF4
		private void DrawSplitHelper(int splitSize)
		{
			Rectangle rectangle = this.CalcSplitLine(splitSize, 3);
			IntPtr handle = base.Handle;
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(this, handle), NativeMethods.NullHandleRef, 1026);
			IntPtr handle2 = ControlPaint.CreateHalftoneHBRUSH();
			IntPtr handle3 = SafeNativeMethods.SelectObject(new HandleRef(this, dcex), new HandleRef(null, handle2));
			SafeNativeMethods.PatBlt(new HandleRef(this, dcex), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 5898313);
			SafeNativeMethods.SelectObject(new HandleRef(this, dcex), new HandleRef(null, handle3));
			SafeNativeMethods.DeleteObject(new HandleRef(null, handle2));
			UnsafeNativeMethods.ReleaseDC(new HandleRef(this, handle), new HandleRef(null, dcex));
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000FDCA8 File Offset: 0x000FBEA8
		private int GetSplitterDistance(int x, int y)
		{
			int num;
			if (this.Orientation == Orientation.Vertical)
			{
				num = x - this.anchor.X;
			}
			else
			{
				num = y - this.anchor.Y;
			}
			int val = 0;
			Orientation orientation = this.Orientation;
			if (orientation != Orientation.Horizontal)
			{
				if (orientation == Orientation.Vertical)
				{
					if (this.RightToLeft == RightToLeft.No)
					{
						val = Math.Max(this.panel1.Width + num, this.BORDERSIZE);
					}
					else
					{
						val = Math.Max(this.panel1.Width - num, this.BORDERSIZE);
					}
				}
			}
			else
			{
				val = Math.Max(this.panel1.Height + num, this.BORDERSIZE);
			}
			if (this.Orientation == Orientation.Vertical)
			{
				return Math.Max(Math.Min(val, base.Width - this.Panel2MinSize), this.Panel1MinSize);
			}
			return Math.Max(Math.Min(val, base.Height - this.Panel2MinSize), this.Panel1MinSize);
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000FDD8C File Offset: 0x000FBF8C
		private bool ProcessArrowKey(bool forward)
		{
			Control control = this;
			if (base.ActiveControl != null)
			{
				control = base.ActiveControl.ParentInternal;
			}
			return control.SelectNextControl(base.ActiveControl, forward, false, false, true);
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000FDDC0 File Offset: 0x000FBFC0
		private void RepaintSplitterRect()
		{
			if (base.IsHandleCreated)
			{
				Graphics graphics = base.CreateGraphicsInternal();
				if (this.BackgroundImage != null)
				{
					using (TextureBrush textureBrush = new TextureBrush(this.BackgroundImage, WrapMode.Tile))
					{
						graphics.FillRectangle(textureBrush, base.ClientRectangle);
						goto IL_62;
					}
				}
				using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
				{
					graphics.FillRectangle(solidBrush, this.splitterRect);
				}
				IL_62:
				graphics.Dispose();
			}
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000FDE54 File Offset: 0x000FC054
		private void SetSplitterRect(bool vertical)
		{
			if (vertical)
			{
				this.splitterRect.X = ((this.RightToLeft == RightToLeft.Yes) ? (base.Width - this.splitterDistance - this.SplitterWidthInternal) : (base.Location.X + this.splitterDistance));
				this.splitterRect.Y = base.Location.Y;
				this.splitterRect.Width = this.SplitterWidthInternal;
				this.splitterRect.Height = base.Height;
				return;
			}
			this.splitterRect.X = base.Location.X;
			this.splitterRect.Y = base.Location.Y + this.SplitterDistanceInternal;
			this.splitterRect.Width = base.Width;
			this.splitterRect.Height = this.SplitterWidthInternal;
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000FDF3C File Offset: 0x000FC13C
		private void ResizeSplitContainer()
		{
			if (this.splitContainerScaling)
			{
				return;
			}
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			if (base.Width == 0)
			{
				this.panel1.Size = new Size(0, this.panel1.Height);
				this.panel2.Size = new Size(0, this.panel2.Height);
			}
			else if (base.Height == 0)
			{
				this.panel1.Size = new Size(this.panel1.Width, 0);
				this.panel2.Size = new Size(this.panel2.Width, 0);
			}
			else
			{
				if (this.Orientation == Orientation.Vertical)
				{
					if (!this.CollapsedMode)
					{
						if (this.FixedPanel == FixedPanel.Panel1)
						{
							this.panel1.Size = new Size(this.panelSize, base.Height);
							this.panel2.Size = new Size(Math.Max(base.Width - this.panelSize - this.SplitterWidthInternal, this.Panel2MinSize), base.Height);
						}
						if (this.FixedPanel == FixedPanel.Panel2)
						{
							this.panel2.Size = new Size(this.panelSize, base.Height);
							this.splitterDistance = Math.Max(base.Width - this.panelSize - this.SplitterWidthInternal, this.Panel1MinSize);
							this.panel1.WidthInternal = this.splitterDistance;
							this.panel1.HeightInternal = base.Height;
						}
						if (this.FixedPanel == FixedPanel.None)
						{
							if (this.ratioWidth != 0.0)
							{
								this.splitterDistance = Math.Max((int)Math.Floor((double)base.Width / this.ratioWidth), this.Panel1MinSize);
							}
							this.panel1.WidthInternal = this.splitterDistance;
							this.panel1.HeightInternal = base.Height;
							this.panel2.Size = new Size(Math.Max(base.Width - this.splitterDistance - this.SplitterWidthInternal, this.Panel2MinSize), base.Height);
						}
						if (this.RightToLeft == RightToLeft.No)
						{
							this.panel2.Location = new Point(this.panel1.WidthInternal + this.SplitterWidthInternal, 0);
						}
						else
						{
							this.panel1.Location = new Point(base.Width - this.panel1.WidthInternal, 0);
						}
						this.RepaintSplitterRect();
						this.SetSplitterRect(true);
					}
					else if (this.Panel1Collapsed)
					{
						this.panel2.Size = base.Size;
						this.panel2.Location = new Point(0, 0);
					}
					else if (this.Panel2Collapsed)
					{
						this.panel1.Size = base.Size;
						this.panel1.Location = new Point(0, 0);
					}
				}
				else if (this.Orientation == Orientation.Horizontal)
				{
					if (!this.CollapsedMode)
					{
						if (this.FixedPanel == FixedPanel.Panel1)
						{
							this.panel1.Size = new Size(base.Width, this.panelSize);
							int num = this.panelSize + this.SplitterWidthInternal;
							this.panel2.Size = new Size(base.Width, Math.Max(base.Height - num, this.Panel2MinSize));
							this.panel2.Location = new Point(0, num);
						}
						if (this.FixedPanel == FixedPanel.Panel2)
						{
							this.panel2.Size = new Size(base.Width, this.panelSize);
							this.splitterDistance = Math.Max(base.Height - this.Panel2.Height - this.SplitterWidthInternal, this.Panel1MinSize);
							this.panel1.HeightInternal = this.splitterDistance;
							this.panel1.WidthInternal = base.Width;
							int y = this.splitterDistance + this.SplitterWidthInternal;
							this.panel2.Location = new Point(0, y);
						}
						if (this.FixedPanel == FixedPanel.None)
						{
							if (this.ratioHeight != 0.0)
							{
								this.splitterDistance = Math.Max((int)Math.Floor((double)base.Height / this.ratioHeight), this.Panel1MinSize);
							}
							this.panel1.HeightInternal = this.splitterDistance;
							this.panel1.WidthInternal = base.Width;
							int num2 = this.splitterDistance + this.SplitterWidthInternal;
							this.panel2.Size = new Size(base.Width, Math.Max(base.Height - num2, this.Panel2MinSize));
							this.panel2.Location = new Point(0, num2);
						}
						this.RepaintSplitterRect();
						this.SetSplitterRect(false);
					}
					else if (this.Panel1Collapsed)
					{
						this.panel2.Size = base.Size;
						this.panel2.Location = new Point(0, 0);
					}
					else if (this.Panel2Collapsed)
					{
						this.panel1.Size = base.Size;
						this.panel1.Location = new Point(0, 0);
					}
				}
				try
				{
					this.resizeCalled = true;
					this.ApplySplitterDistance();
				}
				finally
				{
					this.resizeCalled = false;
				}
			}
			this.panel1.ResumeLayout();
			this.panel2.ResumeLayout();
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000FE498 File Offset: 0x000FC698
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			try
			{
				this.splitContainerScaling = true;
				base.ScaleControl(factor, specified);
				float num;
				if (this.orientation == Orientation.Vertical)
				{
					num = factor.Width;
				}
				else
				{
					num = factor.Height;
				}
				this.SplitterWidth = (int)Math.Round((double)((float)this.SplitterWidth * num));
			}
			finally
			{
				this.splitContainerScaling = false;
			}
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000FE500 File Offset: 0x000FC700
		protected override void Select(bool directed, bool forward)
		{
			if (this.selectNextControl)
			{
				return;
			}
			if (this.Panel1.Controls.Count > 0 || this.Panel2.Controls.Count > 0 || this.TabStop)
			{
				this.SelectNextControlInContainer(this, forward, true, true, false);
				return;
			}
			try
			{
				Control parentInternal = this.ParentInternal;
				this.selectNextControl = true;
				while (parentInternal != null)
				{
					if (parentInternal.SelectNextControl(this, forward, true, true, parentInternal.ParentInternal == null))
					{
						break;
					}
					parentInternal = parentInternal.ParentInternal;
				}
			}
			finally
			{
				this.selectNextControl = false;
			}
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000FE5A0 File Offset: 0x000FC7A0
		private bool SelectNextControlInContainer(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!base.Contains(ctl) || (!nested && ctl.ParentInternal != this))
			{
				ctl = null;
			}
			SplitterPanel splitterPanel = null;
			for (;;)
			{
				ctl = base.GetNextControl(ctl, forward);
				SplitterPanel splitterPanel2 = ctl as SplitterPanel;
				if (splitterPanel2 != null && splitterPanel2.Visible)
				{
					if (splitterPanel != null)
					{
						goto IL_8D;
					}
					splitterPanel = splitterPanel2;
				}
				if (!forward && splitterPanel != null && ctl.ParentInternal != splitterPanel)
				{
					break;
				}
				if (ctl == null)
				{
					goto IL_8D;
				}
				if (ctl.CanSelect && ctl.TabStop)
				{
					goto Block_11;
				}
				if (ctl == null)
				{
					goto IL_8D;
				}
			}
			ctl = splitterPanel;
			goto IL_8D;
			Block_11:
			if (ctl is SplitContainer)
			{
				((SplitContainer)ctl).Select(forward, forward);
			}
			else
			{
				SplitContainer.SelectNextActiveControl(ctl, forward, tabStopOnly, nested, wrap);
			}
			return true;
			IL_8D:
			if (ctl != null && this.TabStop)
			{
				this.splitterFocused = true;
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					ContainerControl containerControl = containerControlInternal as ContainerControl;
					if (containerControl == null)
					{
						containerControlInternal.ActiveControl = this;
					}
					else
					{
						IntSecurity.ModifyFocus.Demand();
						containerControl.SetActiveControlInternal(this);
					}
				}
				base.SetActiveControlInternal(null);
				this.nextActiveControl = ctl;
				return true;
			}
			if (!this.SelectNextControlInPanel(ctl, forward, tabStopOnly, nested, wrap))
			{
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					try
					{
						this.selectNextControl = true;
						parentInternal.SelectNextControl(this, forward, true, true, true);
					}
					finally
					{
						this.selectNextControl = false;
					}
				}
			}
			return false;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000FE6E0 File Offset: 0x000FC8E0
		private bool SelectNextControlInPanel(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!base.Contains(ctl) || (!nested && ctl.ParentInternal != this))
			{
				ctl = null;
			}
			for (;;)
			{
				ctl = base.GetNextControl(ctl, forward);
				if (ctl == null || (ctl is SplitterPanel && ctl.Visible))
				{
					goto IL_73;
				}
				if (ctl.CanSelect && (!tabStopOnly || ctl.TabStop))
				{
					break;
				}
				if (ctl == null)
				{
					goto IL_73;
				}
			}
			if (ctl is SplitContainer)
			{
				((SplitContainer)ctl).Select(forward, forward);
			}
			else
			{
				SplitContainer.SelectNextActiveControl(ctl, forward, tabStopOnly, nested, wrap);
			}
			return true;
			IL_73:
			if (ctl == null || (ctl is SplitterPanel && !ctl.Visible))
			{
				this.callBaseVersion = true;
			}
			else
			{
				ctl = base.GetNextControl(ctl, forward);
				if (forward)
				{
					this.nextActiveControl = this.panel2;
				}
				else if (ctl == null || !ctl.ParentInternal.Visible)
				{
					this.callBaseVersion = true;
				}
				else
				{
					this.nextActiveControl = this.panel2;
				}
			}
			return false;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000FE7C0 File Offset: 0x000FC9C0
		private static void SelectNextActiveControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			ContainerControl containerControl = ctl as ContainerControl;
			if (containerControl != null)
			{
				bool flag = true;
				if (containerControl.ParentInternal != null)
				{
					IContainerControl containerControlInternal = containerControl.ParentInternal.GetContainerControlInternal();
					if (containerControlInternal != null)
					{
						containerControlInternal.ActiveControl = containerControl;
						flag = (containerControlInternal.ActiveControl == containerControl);
					}
				}
				if (flag)
				{
					ctl.SelectNextControl(null, forward, tabStopOnly, nested, wrap);
					return;
				}
			}
			else
			{
				ctl.Select();
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000FE818 File Offset: 0x000FCA18
		private void SetInnerMostBorder(SplitContainer sc)
		{
			foreach (object obj in sc.Controls)
			{
				Control control = (Control)obj;
				bool flag = false;
				if (control is SplitterPanel)
				{
					foreach (object obj2 in control.Controls)
					{
						Control control2 = (Control)obj2;
						SplitContainer splitContainer = control2 as SplitContainer;
						if (splitContainer != null && splitContainer.Dock == DockStyle.Fill)
						{
							if (splitContainer.BorderStyle != this.BorderStyle)
							{
								break;
							}
							((SplitterPanel)control).BorderStyle = BorderStyle.None;
							this.SetInnerMostBorder(splitContainer);
							flag = true;
						}
					}
					if (!flag)
					{
						((SplitterPanel)control).BorderStyle = this.BorderStyle;
					}
				}
			}
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000FE918 File Offset: 0x000FCB18
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None && this.Orientation == Orientation.Horizontal && height < this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize)
			{
				height = this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
			}
			if ((specified & BoundsSpecified.Width) != BoundsSpecified.None && this.Orientation == Orientation.Vertical && width < this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize)
			{
				width = this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
			}
			base.SetBoundsCore(x, y, width, height, specified);
			this.SetSplitterRect(this.Orientation == Orientation.Vertical);
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000FE9BC File Offset: 0x000FCBBC
		private void SplitBegin(int x, int y)
		{
			this.anchor = new Point(x, y);
			this.splitterDistance = this.GetSplitterDistance(x, y);
			this.initialSplitterDistance = this.splitterDistance;
			this.initialSplitterRectangle = this.SplitterRectangle;
			IntSecurity.UnmanagedCode.Assert();
			try
			{
				if (this.splitContainerMessageFilter == null)
				{
					this.splitContainerMessageFilter = new SplitContainer.SplitContainerMessageFilter(this);
				}
				Application.AddMessageFilter(this.splitContainerMessageFilter);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			base.CaptureInternal = true;
			this.DrawSplitBar(1);
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000FEA4C File Offset: 0x000FCC4C
		private void SplitMove(int x, int y)
		{
			int num = this.GetSplitterDistance(x, y);
			int num2 = num - this.initialSplitterDistance;
			int num3 = num2 % this.SplitterIncrement;
			if (this.splitterDistance != num)
			{
				if (this.Orientation == Orientation.Vertical)
				{
					if (num + this.SplitterWidthInternal <= base.Width - this.Panel2MinSize - this.BORDERSIZE)
					{
						this.splitterDistance = num - num3;
					}
				}
				else if (num + this.SplitterWidthInternal <= base.Height - this.Panel2MinSize - this.BORDERSIZE)
				{
					this.splitterDistance = num - num3;
				}
			}
			this.DrawSplitBar(2);
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000FEAE0 File Offset: 0x000FCCE0
		private void SplitEnd(bool accept)
		{
			this.DrawSplitBar(3);
			if (this.splitContainerMessageFilter != null)
			{
				Application.RemoveMessageFilter(this.splitContainerMessageFilter);
				this.splitContainerMessageFilter = null;
			}
			if (accept)
			{
				this.ApplySplitterDistance();
			}
			else if (this.splitterDistance != this.initialSplitterDistance)
			{
				this.splitterClick = false;
				this.splitterDistance = (this.SplitterDistanceInternal = this.initialSplitterDistance);
			}
			this.anchor = Point.Empty;
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000FEB50 File Offset: 0x000FCD50
		private void UpdateSplitter()
		{
			if (this.splitContainerScaling)
			{
				return;
			}
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			if (this.Orientation == Orientation.Vertical)
			{
				bool flag = this.RightToLeft == RightToLeft.Yes;
				if (!this.CollapsedMode)
				{
					this.panel1.HeightInternal = base.Height;
					this.panel1.WidthInternal = this.splitterDistance;
					this.panel2.Size = new Size(base.Width - this.splitterDistance - this.SplitterWidthInternal, base.Height);
					if (!flag)
					{
						this.panel1.Location = new Point(0, 0);
						this.panel2.Location = new Point(this.splitterDistance + this.SplitterWidthInternal, 0);
					}
					else
					{
						this.panel1.Location = new Point(base.Width - this.splitterDistance, 0);
						this.panel2.Location = new Point(0, 0);
					}
					this.RepaintSplitterRect();
					this.SetSplitterRect(true);
					if (!this.resizeCalled)
					{
						this.ratioWidth = (((double)base.Width / (double)this.panel1.Width > 0.0) ? ((double)base.Width / (double)this.panel1.Width) : this.ratioWidth);
					}
				}
				else
				{
					if (this.Panel1Collapsed)
					{
						this.panel2.Size = base.Size;
						this.panel2.Location = new Point(0, 0);
					}
					else if (this.Panel2Collapsed)
					{
						this.panel1.Size = base.Size;
						this.panel1.Location = new Point(0, 0);
					}
					if (!this.resizeCalled)
					{
						this.ratioWidth = (((double)base.Width / (double)this.splitterDistance > 0.0) ? ((double)base.Width / (double)this.splitterDistance) : this.ratioWidth);
					}
				}
			}
			else if (!this.CollapsedMode)
			{
				this.panel1.Location = new Point(0, 0);
				this.panel1.WidthInternal = base.Width;
				this.panel1.HeightInternal = this.SplitterDistanceInternal;
				int num = this.splitterDistance + this.SplitterWidthInternal;
				this.panel2.Size = new Size(base.Width, base.Height - num);
				this.panel2.Location = new Point(0, num);
				this.RepaintSplitterRect();
				this.SetSplitterRect(false);
				if (!this.resizeCalled)
				{
					this.ratioHeight = (((double)base.Height / (double)this.panel1.Height > 0.0) ? ((double)base.Height / (double)this.panel1.Height) : this.ratioHeight);
				}
			}
			else
			{
				if (this.Panel1Collapsed)
				{
					this.panel2.Size = base.Size;
					this.panel2.Location = new Point(0, 0);
				}
				else if (this.Panel2Collapsed)
				{
					this.panel1.Size = base.Size;
					this.panel1.Location = new Point(0, 0);
				}
				if (!this.resizeCalled)
				{
					this.ratioHeight = (((double)base.Height / (double)this.splitterDistance > 0.0) ? ((double)base.Height / (double)this.splitterDistance) : this.ratioHeight);
				}
			}
			this.panel1.ResumeLayout();
			this.panel2.ResumeLayout();
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000FEED4 File Offset: 0x000FD0D4
		private void WmSetCursor(ref Message m)
		{
			if (!(m.WParam == base.InternalHandle) || ((int)m.LParam & 65535) != 1)
			{
				this.DefWndProc(ref m);
				return;
			}
			if (this.OverrideCursor != null)
			{
				Cursor.CurrentInternal = this.OverrideCursor;
				return;
			}
			Cursor.CurrentInternal = this.Cursor;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000FEF38 File Offset: 0x000FD138
		internal override Rectangle GetToolNativeScreenRectangle()
		{
			Rectangle toolNativeScreenRectangle = base.GetToolNativeScreenRectangle();
			Rectangle splitterRectangle = this.SplitterRectangle;
			return new Rectangle(toolNativeScreenRectangle.X + splitterRectangle.X, toolNativeScreenRectangle.Y + splitterRectangle.Y, splitterRectangle.Width, splitterRectangle.Height);
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000FEF84 File Offset: 0x000FD184
		internal override void AfterControlRemoved(Control control, Control oldParent)
		{
			base.AfterControlRemoved(control, oldParent);
			if (control is SplitContainer && control.Dock == DockStyle.Fill)
			{
				this.SetInnerMostBorder(this);
			}
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000FEFA8 File Offset: 0x000FD1A8
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Tab)
				{
					if (keys - Keys.Left <= 3)
					{
						if (this.splitterFocused)
						{
							return false;
						}
						if (this.ProcessArrowKey(keys == Keys.Right || keys == Keys.Down))
						{
							return true;
						}
					}
				}
				else if (this.ProcessTabKey((keyData & Keys.Shift) == Keys.None))
				{
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000FF010 File Offset: 0x000FD210
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessTabKey(bool forward)
		{
			if (!this.TabStop || this.IsSplitterFixed)
			{
				return base.ProcessTabKey(forward);
			}
			if (this.nextActiveControl != null)
			{
				base.SetActiveControlInternal(this.nextActiveControl);
				this.nextActiveControl = null;
			}
			if (this.SelectNextControlInPanel(base.ActiveControl, forward, true, true, true))
			{
				this.nextActiveControl = null;
				this.splitterFocused = false;
				return true;
			}
			if (this.callBaseVersion)
			{
				this.callBaseVersion = false;
				return base.ProcessTabKey(forward);
			}
			this.splitterFocused = true;
			IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
			if (containerControlInternal != null)
			{
				ContainerControl containerControl = containerControlInternal as ContainerControl;
				if (containerControl == null)
				{
					containerControlInternal.ActiveControl = this;
				}
				else
				{
					containerControl.SetActiveControlInternal(this);
				}
			}
			base.SetActiveControlInternal(null);
			return true;
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000FF0C1 File Offset: 0x000FD2C1
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			if (this.splitContainerMessageFilter != null)
			{
				Application.RemoveMessageFilter(this.splitContainerMessageFilter);
				this.splitContainerMessageFilter = null;
			}
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000FF0E4 File Offset: 0x000FD2E4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 == 7)
			{
				this.splitterFocused = true;
				base.WndProc(ref msg);
				return;
			}
			if (msg2 == 8)
			{
				this.splitterFocused = false;
				base.WndProc(ref msg);
				return;
			}
			if (msg2 == 32)
			{
				this.WmSetCursor(ref msg);
				return;
			}
			base.WndProc(ref msg);
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000FF132 File Offset: 0x000FD332
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new SplitContainer.SplitContainerTypedControlCollection(this, typeof(SplitterPanel), true);
		}

		// Token: 0x04002292 RID: 8850
		private const int DRAW_START = 1;

		// Token: 0x04002293 RID: 8851
		private const int DRAW_MOVE = 2;

		// Token: 0x04002294 RID: 8852
		private const int DRAW_END = 3;

		// Token: 0x04002295 RID: 8853
		private const int rightBorder = 5;

		// Token: 0x04002296 RID: 8854
		private const int leftBorder = 2;

		// Token: 0x04002297 RID: 8855
		private int BORDERSIZE;

		// Token: 0x04002298 RID: 8856
		private Orientation orientation = Orientation.Vertical;

		// Token: 0x04002299 RID: 8857
		private SplitterPanel panel1;

		// Token: 0x0400229A RID: 8858
		private SplitterPanel panel2;

		// Token: 0x0400229B RID: 8859
		private BorderStyle borderStyle;

		// Token: 0x0400229C RID: 8860
		private FixedPanel fixedPanel;

		// Token: 0x0400229D RID: 8861
		private int panel1MinSize = 25;

		// Token: 0x0400229E RID: 8862
		private int newPanel1MinSize = 25;

		// Token: 0x0400229F RID: 8863
		private int panel2MinSize = 25;

		// Token: 0x040022A0 RID: 8864
		private int newPanel2MinSize = 25;

		// Token: 0x040022A1 RID: 8865
		private bool tabStop = true;

		// Token: 0x040022A2 RID: 8866
		private int panelSize;

		// Token: 0x040022A3 RID: 8867
		private Rectangle splitterRect;

		// Token: 0x040022A4 RID: 8868
		private int splitterInc = 1;

		// Token: 0x040022A5 RID: 8869
		private bool splitterFixed;

		// Token: 0x040022A6 RID: 8870
		private int splitterDistance = 50;

		// Token: 0x040022A7 RID: 8871
		private int splitterWidth = 4;

		// Token: 0x040022A8 RID: 8872
		private int newSplitterWidth = 4;

		// Token: 0x040022A9 RID: 8873
		private int splitDistance = 50;

		// Token: 0x040022AA RID: 8874
		private int lastDrawSplit = 1;

		// Token: 0x040022AB RID: 8875
		private int initialSplitterDistance;

		// Token: 0x040022AC RID: 8876
		private Rectangle initialSplitterRectangle;

		// Token: 0x040022AD RID: 8877
		private Point anchor = Point.Empty;

		// Token: 0x040022AE RID: 8878
		private bool splitBegin;

		// Token: 0x040022AF RID: 8879
		private bool splitMove;

		// Token: 0x040022B0 RID: 8880
		private bool splitBreak;

		// Token: 0x040022B1 RID: 8881
		private Cursor overrideCursor;

		// Token: 0x040022B2 RID: 8882
		private Control nextActiveControl;

		// Token: 0x040022B3 RID: 8883
		private bool callBaseVersion;

		// Token: 0x040022B4 RID: 8884
		private bool splitterFocused;

		// Token: 0x040022B5 RID: 8885
		private bool splitterClick;

		// Token: 0x040022B6 RID: 8886
		private bool splitterDrag;

		// Token: 0x040022B7 RID: 8887
		private double ratioWidth;

		// Token: 0x040022B8 RID: 8888
		private double ratioHeight;

		// Token: 0x040022B9 RID: 8889
		private bool resizeCalled;

		// Token: 0x040022BA RID: 8890
		private bool splitContainerScaling;

		// Token: 0x040022BB RID: 8891
		private bool setSplitterDistance;

		// Token: 0x040022BC RID: 8892
		private static readonly object EVENT_MOVING = new object();

		// Token: 0x040022BD RID: 8893
		private static readonly object EVENT_MOVED = new object();

		// Token: 0x040022BE RID: 8894
		private SplitContainer.SplitContainerMessageFilter splitContainerMessageFilter;

		// Token: 0x040022BF RID: 8895
		private bool selectNextControl;

		// Token: 0x040022C0 RID: 8896
		private bool initializing;

		// Token: 0x020007E6 RID: 2022
		private class SplitContainerMessageFilter : IMessageFilter
		{
			// Token: 0x06006DF6 RID: 28150 RVA: 0x00193781 File Offset: 0x00191981
			public SplitContainerMessageFilter(SplitContainer splitContainer)
			{
				this.owner = splitContainer;
			}

			// Token: 0x06006DF7 RID: 28151 RVA: 0x00193790 File Offset: 0x00191990
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			bool IMessageFilter.PreFilterMessage(ref Message m)
			{
				if (m.Msg >= 256 && m.Msg <= 264)
				{
					if ((m.Msg == 256 && (int)m.WParam == 27) || m.Msg == 260)
					{
						this.owner.splitBegin = false;
						this.owner.SplitEnd(false);
						this.owner.splitterClick = false;
						this.owner.splitterDrag = false;
					}
					return true;
				}
				return false;
			}

			// Token: 0x040042CB RID: 17099
			private SplitContainer owner;
		}

		// Token: 0x020007E7 RID: 2023
		internal class SplitContainerTypedControlCollection : WindowsFormsUtils.TypedControlCollection
		{
			// Token: 0x06006DF8 RID: 28152 RVA: 0x00193813 File Offset: 0x00191A13
			public SplitContainerTypedControlCollection(Control c, Type type, bool isReadOnly) : base(c, type, isReadOnly)
			{
				this.owner = (c as SplitContainer);
			}

			// Token: 0x06006DF9 RID: 28153 RVA: 0x0019382A File Offset: 0x00191A2A
			public override void Remove(Control value)
			{
				if (value is SplitterPanel && !this.owner.DesignMode && this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.Remove(value);
			}

			// Token: 0x06006DFA RID: 28154 RVA: 0x00193860 File Offset: 0x00191A60
			internal override void SetChildIndexInternal(Control child, int newIndex)
			{
				if (child is SplitterPanel)
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

			// Token: 0x040042CC RID: 17100
			private SplitContainer owner;
		}
	}
}
