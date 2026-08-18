using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x0200036F RID: 879
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("SplitterMoved")]
	[DefaultProperty("Dock")]
	[SRDescription("DescriptionSplitter")]
	[Designer("System.Windows.Forms.Design.SplitterDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Splitter : Control
	{
		// Token: 0x0600393E RID: 14654 RVA: 0x000FF15C File Offset: 0x000FD35C
		public Splitter()
		{
			base.SetStyle(ControlStyles.Selectable, false);
			this.TabStop = false;
			this.minSize = 25;
			this.minExtra = 25;
			this.Dock = DockStyle.Left;
		}

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06003940 RID: 14656 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(AnchorStyles.None)]
		public override AnchorStyles Anchor
		{
			get
			{
				return AnchorStyles.None;
			}
			set
			{
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000B90B9 File Offset: 0x000B72B9
		// (set) Token: 0x06003942 RID: 14658 RVA: 0x000B90C1 File Offset: 0x000B72C1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06003943 RID: 14659 RVA: 0x000FF1C9 File Offset: 0x000FD3C9
		protected override Size DefaultSize
		{
			get
			{
				return new Size(3, 3);
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x000FF1D4 File Offset: 0x000FD3D4
		protected override Cursor DefaultCursor
		{
			get
			{
				DockStyle dock = this.Dock;
				if (dock - DockStyle.Top <= 1)
				{
					return Cursors.HSplit;
				}
				if (dock - DockStyle.Left > 1)
				{
					return base.DefaultCursor;
				}
				return Cursors.VSplit;
			}
		}

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x06003946 RID: 14662 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
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

		// Token: 0x140002B4 RID: 692
		// (add) Token: 0x06003947 RID: 14663 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06003948 RID: 14664 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003949 RID: 14665 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x0600394A RID: 14666 RVA: 0x00011A98 File Offset: 0x0000FC98
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x140002B5 RID: 693
		// (add) Token: 0x0600394B RID: 14667 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x0600394C RID: 14668 RVA: 0x00011AAA File Offset: 0x0000FCAA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x140002B6 RID: 694
		// (add) Token: 0x0600394F RID: 14671 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06003950 RID: 14672 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x0001A27A File Offset: 0x0001847A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		// Token: 0x140002B7 RID: 695
		// (add) Token: 0x06003953 RID: 14675 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		// (remove) Token: 0x06003954 RID: 14676 RVA: 0x0005AAE9 File Offset: 0x00058CE9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000FF208 File Offset: 0x000FD408
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x000FF210 File Offset: 0x000FD410
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
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000FF250 File Offset: 0x000FD450
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				return createParams;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003958 RID: 14680 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x000FF2C0 File Offset: 0x000FD4C0
		[Localizable(true)]
		[DefaultValue(DockStyle.Left)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (value != DockStyle.Top && value != DockStyle.Bottom && value != DockStyle.Left && value != DockStyle.Right)
				{
					throw new ArgumentException(SR.GetString("SplitterInvalidDockEnum"));
				}
				int num = this.splitterThickness;
				base.Dock = value;
				DockStyle dock = this.Dock;
				if (dock - DockStyle.Top > 1)
				{
					if (dock - DockStyle.Left > 1)
					{
						return;
					}
					if (this.splitterThickness != -1)
					{
						base.Width = num;
					}
				}
				else if (this.splitterThickness != -1)
				{
					base.Height = num;
					return;
				}
			}
		}

		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000FF330 File Offset: 0x000FD530
		private bool Horizontal
		{
			get
			{
				DockStyle dock = this.Dock;
				return dock == DockStyle.Left || dock == DockStyle.Right;
			}
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x0600395C RID: 14684 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x0600395D RID: 14685 RVA: 0x0001A1F5 File Offset: 0x000183F5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		// Token: 0x140002B8 RID: 696
		// (add) Token: 0x0600395E RID: 14686 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x0600395F RID: 14687 RVA: 0x00024115 File Offset: 0x00022315
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003960 RID: 14688 RVA: 0x000FF34E File Offset: 0x000FD54E
		// (set) Token: 0x06003961 RID: 14689 RVA: 0x000FF356 File Offset: 0x000FD556
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(25)]
		[SRDescription("SplitterMinExtraDescr")]
		public int MinExtra
		{
			get
			{
				return this.minExtra;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.minExtra = value;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x000FF366 File Offset: 0x000FD566
		// (set) Token: 0x06003963 RID: 14691 RVA: 0x000FF36E File Offset: 0x000FD56E
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(25)]
		[SRDescription("SplitterMinSizeDescr")]
		public int MinSize
		{
			get
			{
				return this.minSize;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.minSize = value;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06003964 RID: 14692 RVA: 0x000FF37E File Offset: 0x000FD57E
		// (set) Token: 0x06003965 RID: 14693 RVA: 0x000FF39C File Offset: 0x000FD59C
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("SplitterSplitPositionDescr")]
		public int SplitPosition
		{
			get
			{
				if (this.splitSize == -1)
				{
					this.splitSize = this.CalcSplitSize();
				}
				return this.splitSize;
			}
			set
			{
				Splitter.SplitData splitData = this.CalcSplitBounds();
				if (value > this.maxSize)
				{
					value = this.maxSize;
				}
				if (value < this.minSize)
				{
					value = this.minSize;
				}
				this.splitSize = value;
				this.DrawSplitBar(3);
				if (splitData.target == null)
				{
					this.splitSize = -1;
					return;
				}
				Rectangle bounds = splitData.target.Bounds;
				switch (this.Dock)
				{
				case DockStyle.Top:
					bounds.Height = value;
					break;
				case DockStyle.Bottom:
					bounds.Y += bounds.Height - this.splitSize;
					bounds.Height = value;
					break;
				case DockStyle.Left:
					bounds.Width = value;
					break;
				case DockStyle.Right:
					bounds.X += bounds.Width - this.splitSize;
					bounds.Width = value;
					break;
				}
				splitData.target.Bounds = bounds;
				Application.DoEvents();
				this.OnSplitterMoved(new SplitterEventArgs(base.Left, base.Top, base.Left + bounds.Width / 2, base.Top + bounds.Height / 2));
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003966 RID: 14694 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x06003967 RID: 14695 RVA: 0x000B2619 File Offset: 0x000B0819
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

		// Token: 0x140002B9 RID: 697
		// (add) Token: 0x06003968 RID: 14696 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x06003969 RID: 14697 RVA: 0x000B262B File Offset: 0x000B082B
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

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x0600396B RID: 14699 RVA: 0x00024185 File Offset: 0x00022385
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
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

		// Token: 0x140002BA RID: 698
		// (add) Token: 0x0600396C RID: 14700 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x0600396D RID: 14701 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x140002BB RID: 699
		// (add) Token: 0x0600396E RID: 14702 RVA: 0x000E35B4 File Offset: 0x000E17B4
		// (remove) Token: 0x0600396F RID: 14703 RVA: 0x000E35BD File Offset: 0x000E17BD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		// Token: 0x140002BC RID: 700
		// (add) Token: 0x06003970 RID: 14704 RVA: 0x000B9380 File Offset: 0x000B7580
		// (remove) Token: 0x06003971 RID: 14705 RVA: 0x000B9389 File Offset: 0x000B7589
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		// Token: 0x140002BD RID: 701
		// (add) Token: 0x06003972 RID: 14706 RVA: 0x000B9392 File Offset: 0x000B7592
		// (remove) Token: 0x06003973 RID: 14707 RVA: 0x000B939B File Offset: 0x000B759B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		// Token: 0x140002BE RID: 702
		// (add) Token: 0x06003974 RID: 14708 RVA: 0x000B93A4 File Offset: 0x000B75A4
		// (remove) Token: 0x06003975 RID: 14709 RVA: 0x000B93AD File Offset: 0x000B75AD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		// Token: 0x140002BF RID: 703
		// (add) Token: 0x06003976 RID: 14710 RVA: 0x000E35C6 File Offset: 0x000E17C6
		// (remove) Token: 0x06003977 RID: 14711 RVA: 0x000E35CF File Offset: 0x000E17CF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		// Token: 0x140002C0 RID: 704
		// (add) Token: 0x06003978 RID: 14712 RVA: 0x000FF4C3 File Offset: 0x000FD6C3
		// (remove) Token: 0x06003979 RID: 14713 RVA: 0x000FF4D6 File Offset: 0x000FD6D6
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovingDescr")]
		public event SplitterEventHandler SplitterMoving
		{
			add
			{
				base.Events.AddHandler(Splitter.EVENT_MOVING, value);
			}
			remove
			{
				base.Events.RemoveHandler(Splitter.EVENT_MOVING, value);
			}
		}

		// Token: 0x140002C1 RID: 705
		// (add) Token: 0x0600397A RID: 14714 RVA: 0x000FF4E9 File Offset: 0x000FD6E9
		// (remove) Token: 0x0600397B RID: 14715 RVA: 0x000FF4FC File Offset: 0x000FD6FC
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovedDescr")]
		public event SplitterEventHandler SplitterMoved
		{
			add
			{
				base.Events.AddHandler(Splitter.EVENT_MOVED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Splitter.EVENT_MOVED, value);
			}
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000FF510 File Offset: 0x000FD710
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
			if (mode != 3)
			{
				this.DrawSplitHelper(this.splitSize);
				this.lastDrawSplit = this.splitSize;
				return;
			}
			if (this.lastDrawSplit != -1)
			{
				this.DrawSplitHelper(this.lastDrawSplit);
			}
			this.lastDrawSplit = -1;
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000FF588 File Offset: 0x000FD788
		private Rectangle CalcSplitLine(int splitSize, int minWeight)
		{
			Rectangle bounds = base.Bounds;
			Rectangle bounds2 = this.splitTarget.Bounds;
			switch (this.Dock)
			{
			case DockStyle.Top:
				if (bounds.Height < minWeight)
				{
					bounds.Height = minWeight;
				}
				bounds.Y = bounds2.Y + splitSize;
				break;
			case DockStyle.Bottom:
				if (bounds.Height < minWeight)
				{
					bounds.Height = minWeight;
				}
				bounds.Y = bounds2.Y + bounds2.Height - splitSize - bounds.Height;
				break;
			case DockStyle.Left:
				if (bounds.Width < minWeight)
				{
					bounds.Width = minWeight;
				}
				bounds.X = bounds2.X + splitSize;
				break;
			case DockStyle.Right:
				if (bounds.Width < minWeight)
				{
					bounds.Width = minWeight;
				}
				bounds.X = bounds2.X + bounds2.Width - splitSize - bounds.Width;
				break;
			}
			return bounds;
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x000FF680 File Offset: 0x000FD880
		private int CalcSplitSize()
		{
			Control control = this.FindTarget();
			if (control == null)
			{
				return -1;
			}
			Rectangle bounds = control.Bounds;
			DockStyle dock = this.Dock;
			if (dock - DockStyle.Top <= 1)
			{
				return bounds.Height;
			}
			if (dock - DockStyle.Left > 1)
			{
				return -1;
			}
			return bounds.Width;
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000FF6C8 File Offset: 0x000FD8C8
		private Splitter.SplitData CalcSplitBounds()
		{
			Splitter.SplitData splitData = new Splitter.SplitData();
			Control control = this.FindTarget();
			splitData.target = control;
			if (control != null)
			{
				DockStyle dock = control.Dock;
				if (dock - DockStyle.Top > 1)
				{
					if (dock - DockStyle.Left <= 1)
					{
						this.initTargetSize = control.Bounds.Width;
					}
				}
				else
				{
					this.initTargetSize = control.Bounds.Height;
				}
				Control parentInternal = this.ParentInternal;
				Control.ControlCollection controls = parentInternal.Controls;
				int count = controls.Count;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < count; i++)
				{
					Control control2 = controls[i];
					if (control2 != control)
					{
						DockStyle dock2 = control2.Dock;
						if (dock2 - DockStyle.Top > 1)
						{
							if (dock2 - DockStyle.Left <= 1)
							{
								num += control2.Width;
							}
						}
						else
						{
							num2 += control2.Height;
						}
					}
				}
				Size clientSize = parentInternal.ClientSize;
				if (this.Horizontal)
				{
					this.maxSize = clientSize.Width - num - this.minExtra;
				}
				else
				{
					this.maxSize = clientSize.Height - num2 - this.minExtra;
				}
				splitData.dockWidth = num;
				splitData.dockHeight = num2;
			}
			return splitData;
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000FF7F0 File Offset: 0x000FD9F0
		private void DrawSplitHelper(int splitSize)
		{
			if (this.splitTarget == null)
			{
				return;
			}
			Rectangle rectangle = this.CalcSplitLine(splitSize, 3);
			IntPtr handle = this.ParentInternal.Handle;
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(this.ParentInternal, handle), NativeMethods.NullHandleRef, 1026);
			IntPtr handle2 = ControlPaint.CreateHalftoneHBRUSH();
			IntPtr handle3 = SafeNativeMethods.SelectObject(new HandleRef(this.ParentInternal, dcex), new HandleRef(null, handle2));
			SafeNativeMethods.PatBlt(new HandleRef(this.ParentInternal, dcex), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 5898313);
			SafeNativeMethods.SelectObject(new HandleRef(this.ParentInternal, dcex), new HandleRef(null, handle3));
			SafeNativeMethods.DeleteObject(new HandleRef(null, handle2));
			UnsafeNativeMethods.ReleaseDC(new HandleRef(this.ParentInternal, handle), new HandleRef(null, dcex));
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000FF8CC File Offset: 0x000FDACC
		private Control FindTarget()
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal == null)
			{
				return null;
			}
			Control.ControlCollection controls = parentInternal.Controls;
			int count = controls.Count;
			DockStyle dock = this.Dock;
			for (int i = 0; i < count; i++)
			{
				Control control = controls[i];
				if (control != this)
				{
					switch (dock)
					{
					case DockStyle.Top:
						if (control.Bottom == base.Top)
						{
							return control;
						}
						break;
					case DockStyle.Bottom:
						if (control.Top == base.Bottom)
						{
							return control;
						}
						break;
					case DockStyle.Left:
						if (control.Right == base.Left)
						{
							return control;
						}
						break;
					case DockStyle.Right:
						if (control.Left == base.Right)
						{
							return control;
						}
						break;
					}
				}
			}
			return null;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000FF97C File Offset: 0x000FDB7C
		private int GetSplitSize(int x, int y)
		{
			int num;
			if (this.Horizontal)
			{
				num = x - this.anchor.X;
			}
			else
			{
				num = y - this.anchor.Y;
			}
			int val = 0;
			switch (this.Dock)
			{
			case DockStyle.Top:
				val = this.splitTarget.Height + num;
				break;
			case DockStyle.Bottom:
				val = this.splitTarget.Height - num;
				break;
			case DockStyle.Left:
				val = this.splitTarget.Width + num;
				break;
			case DockStyle.Right:
				val = this.splitTarget.Width - num;
				break;
			}
			return Math.Max(Math.Min(val, this.maxSize), this.minSize);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000FFA27 File Offset: 0x000FDC27
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.splitTarget != null && e.KeyCode == Keys.Escape)
			{
				this.SplitEnd(false);
			}
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000FFA49 File Offset: 0x000FDC49
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left && e.Clicks == 1)
			{
				this.SplitBegin(e.X, e.Y);
			}
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000FFA7C File Offset: 0x000FDC7C
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.splitTarget != null)
			{
				int x = e.X + base.Left;
				int y = e.Y + base.Top;
				Rectangle rectangle = this.CalcSplitLine(this.GetSplitSize(e.X, e.Y), 0);
				int x2 = rectangle.X;
				int y2 = rectangle.Y;
				this.OnSplitterMoving(new SplitterEventArgs(x, y, x2, y2));
			}
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000FFAF0 File Offset: 0x000FDCF0
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.splitTarget != null)
			{
				int num = e.X + base.Left;
				int num2 = e.Y + base.Top;
				Rectangle rectangle = this.CalcSplitLine(this.GetSplitSize(e.X, e.Y), 0);
				int x = rectangle.X;
				int y = rectangle.Y;
				this.SplitEnd(true);
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x000FFB5C File Offset: 0x000FDD5C
		protected virtual void OnSplitterMoving(SplitterEventArgs sevent)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[Splitter.EVENT_MOVING];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, sevent);
			}
			if (this.splitTarget != null)
			{
				this.SplitMove(sevent.SplitX, sevent.SplitY);
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x000FFBA4 File Offset: 0x000FDDA4
		protected virtual void OnSplitterMoved(SplitterEventArgs sevent)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[Splitter.EVENT_MOVED];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, sevent);
			}
			if (this.splitTarget != null)
			{
				this.SplitMove(sevent.SplitX, sevent.SplitY);
			}
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x000FFBEC File Offset: 0x000FDDEC
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.Horizontal)
			{
				if (width < 1)
				{
					width = 3;
				}
				this.splitterThickness = width;
			}
			else
			{
				if (height < 1)
				{
					height = 3;
				}
				this.splitterThickness = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x000FFC24 File Offset: 0x000FDE24
		private void SplitBegin(int x, int y)
		{
			Splitter.SplitData splitData = this.CalcSplitBounds();
			if (splitData.target != null && this.minSize < this.maxSize)
			{
				this.anchor = new Point(x, y);
				this.splitTarget = splitData.target;
				this.splitSize = this.GetSplitSize(x, y);
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (this.splitterMessageFilter != null)
					{
						this.splitterMessageFilter = new Splitter.SplitterMessageFilter(this);
					}
					Application.AddMessageFilter(this.splitterMessageFilter);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				base.CaptureInternal = true;
				this.DrawSplitBar(1);
			}
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000FFCC4 File Offset: 0x000FDEC4
		private void SplitEnd(bool accept)
		{
			this.DrawSplitBar(3);
			this.splitTarget = null;
			base.CaptureInternal = false;
			if (this.splitterMessageFilter != null)
			{
				Application.RemoveMessageFilter(this.splitterMessageFilter);
				this.splitterMessageFilter = null;
			}
			if (accept)
			{
				this.ApplySplitPosition();
			}
			else if (this.splitSize != this.initTargetSize)
			{
				this.SplitPosition = this.initTargetSize;
			}
			this.anchor = Point.Empty;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000FFD30 File Offset: 0x000FDF30
		private void ApplySplitPosition()
		{
			this.SplitPosition = this.splitSize;
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000FFD40 File Offset: 0x000FDF40
		private void SplitMove(int x, int y)
		{
			int num = this.GetSplitSize(x - base.Left + this.anchor.X, y - base.Top + this.anchor.Y);
			if (this.splitSize != num)
			{
				this.splitSize = num;
				this.DrawSplitBar(2);
			}
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000FFD94 File Offset: 0x000FDF94
		public override string ToString()
		{
			string text = base.ToString();
			return string.Concat(new string[]
			{
				text,
				", MinExtra: ",
				this.MinExtra.ToString(CultureInfo.CurrentCulture),
				", MinSize: ",
				this.MinSize.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x040022C1 RID: 8897
		private const int DRAW_START = 1;

		// Token: 0x040022C2 RID: 8898
		private const int DRAW_MOVE = 2;

		// Token: 0x040022C3 RID: 8899
		private const int DRAW_END = 3;

		// Token: 0x040022C4 RID: 8900
		private const int defaultWidth = 3;

		// Token: 0x040022C5 RID: 8901
		private BorderStyle borderStyle;

		// Token: 0x040022C6 RID: 8902
		private int minSize = 25;

		// Token: 0x040022C7 RID: 8903
		private int minExtra = 25;

		// Token: 0x040022C8 RID: 8904
		private Point anchor = Point.Empty;

		// Token: 0x040022C9 RID: 8905
		private Control splitTarget;

		// Token: 0x040022CA RID: 8906
		private int splitSize = -1;

		// Token: 0x040022CB RID: 8907
		private int splitterThickness = 3;

		// Token: 0x040022CC RID: 8908
		private int initTargetSize;

		// Token: 0x040022CD RID: 8909
		private int lastDrawSplit = -1;

		// Token: 0x040022CE RID: 8910
		private int maxSize;

		// Token: 0x040022CF RID: 8911
		private static readonly object EVENT_MOVING = new object();

		// Token: 0x040022D0 RID: 8912
		private static readonly object EVENT_MOVED = new object();

		// Token: 0x040022D1 RID: 8913
		private Splitter.SplitterMessageFilter splitterMessageFilter;

		// Token: 0x020007E8 RID: 2024
		private class SplitData
		{
			// Token: 0x040042CD RID: 17101
			public int dockWidth = -1;

			// Token: 0x040042CE RID: 17102
			public int dockHeight = -1;

			// Token: 0x040042CF RID: 17103
			internal Control target;
		}

		// Token: 0x020007E9 RID: 2025
		private class SplitterMessageFilter : IMessageFilter
		{
			// Token: 0x06006DFC RID: 28156 RVA: 0x001938AE File Offset: 0x00191AAE
			public SplitterMessageFilter(Splitter splitter)
			{
				this.owner = splitter;
			}

			// Token: 0x06006DFD RID: 28157 RVA: 0x001938C0 File Offset: 0x00191AC0
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public bool PreFilterMessage(ref Message m)
			{
				if (m.Msg >= 256 && m.Msg <= 264)
				{
					if (m.Msg == 256 && (int)((long)m.WParam) == 27)
					{
						this.owner.SplitEnd(false);
					}
					return true;
				}
				return false;
			}

			// Token: 0x040042D0 RID: 17104
			private Splitter owner;
		}
	}
}
