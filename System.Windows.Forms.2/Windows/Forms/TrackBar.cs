using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200040E RID: 1038
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("Scroll")]
	[DefaultBindingProperty("Value")]
	[Designer("System.Windows.Forms.Design.TrackBarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTrackBar")]
	public class TrackBar : Control, ISupportInitialize
	{
		// Token: 0x060047E9 RID: 18409 RVA: 0x0012F804 File Offset: 0x0012DA04
		public TrackBar()
		{
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			this.requestedDim = this.PreferredDimension;
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060047EA RID: 18410 RVA: 0x0012F862 File Offset: 0x0012DA62
		// (set) Token: 0x060047EB RID: 18411 RVA: 0x0012F86C File Offset: 0x0012DA6C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TrackBarAutoSizeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return this.autoSize;
			}
			set
			{
				if (this.autoSize != value)
				{
					this.autoSize = value;
					if (this.orientation == Orientation.Horizontal)
					{
						base.SetStyle(ControlStyles.FixedHeight, this.autoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
					}
					else
					{
						base.SetStyle(ControlStyles.FixedWidth, this.autoSize);
						base.SetStyle(ControlStyles.FixedHeight, false);
					}
					this.AdjustSize();
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000390 RID: 912
		// (add) Token: 0x060047EC RID: 18412 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x060047ED RID: 18413 RVA: 0x00011A5F File Offset: 0x0000FC5F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
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

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x060047EE RID: 18414 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060047EF RID: 18415 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x14000391 RID: 913
		// (add) Token: 0x060047F0 RID: 18416 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x060047F1 RID: 18417 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x060047F2 RID: 18418 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060047F3 RID: 18419 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x14000392 RID: 914
		// (add) Token: 0x060047F4 RID: 18420 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x060047F5 RID: 18421 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x060047F6 RID: 18422 RVA: 0x0012F8D4 File Offset: 0x0012DAD4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "msctls_trackbar32";
				switch (this.tickStyle)
				{
				case TickStyle.None:
					createParams.Style |= 16;
					break;
				case TickStyle.TopLeft:
					createParams.Style |= 5;
					break;
				case TickStyle.BottomRight:
					createParams.Style |= 1;
					break;
				case TickStyle.Both:
					createParams.Style |= 9;
					break;
				}
				if (this.orientation == Orientation.Vertical)
				{
					createParams.Style |= 2;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x060047F7 RID: 18423 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x060047F8 RID: 18424 RVA: 0x0012F99F File Offset: 0x0012DB9F
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, this.PreferredDimension);
			}
		}

		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x060047FA RID: 18426 RVA: 0x000131DF File Offset: 0x000113DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x060047FC RID: 18428 RVA: 0x0001A27A File Offset: 0x0001847A
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

		// Token: 0x14000393 RID: 915
		// (add) Token: 0x060047FD RID: 18429 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		// (remove) Token: 0x060047FE RID: 18430 RVA: 0x0005AAE9 File Offset: 0x00058CE9
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

		// Token: 0x170011A4 RID: 4516
		// (get) Token: 0x060047FF RID: 18431 RVA: 0x0012F9AE File Offset: 0x0012DBAE
		// (set) Token: 0x06004800 RID: 18432 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return SystemColors.WindowText;
			}
			set
			{
			}
		}

		// Token: 0x14000394 RID: 916
		// (add) Token: 0x06004801 RID: 18433 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06004802 RID: 18434 RVA: 0x0005AAD7 File Offset: 0x00058CD7
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

		// Token: 0x170011A5 RID: 4517
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x06004804 RID: 18436 RVA: 0x0001A1F5 File Offset: 0x000183F5
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

		// Token: 0x14000395 RID: 917
		// (add) Token: 0x06004805 RID: 18437 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x06004806 RID: 18438 RVA: 0x00024115 File Offset: 0x00022315
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

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06004807 RID: 18439 RVA: 0x0012F9B5 File Offset: 0x0012DBB5
		// (set) Token: 0x06004808 RID: 18440 RVA: 0x0012F9C0 File Offset: 0x0012DBC0
		[SRCategory("CatBehavior")]
		[DefaultValue(5)]
		[SRDescription("TrackBarLargeChangeDescr")]
		public int LargeChange
		{
			get
			{
				return this.largeChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("LargeChange", SR.GetString("TrackBarLargeChangeError", new object[]
					{
						value
					}));
				}
				if (this.largeChange != value)
				{
					this.largeChange = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(1045, 0, value);
					}
				}
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06004809 RID: 18441 RVA: 0x0012FA1B File Offset: 0x0012DC1B
		// (set) Token: 0x0600480A RID: 18442 RVA: 0x0012FA23 File Offset: 0x0012DC23
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("TrackBarMaximumDescr")]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (this.maximum != value)
				{
					if (value < this.minimum)
					{
						this.minimum = value;
					}
					this.SetRange(this.minimum, value);
				}
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x0012FA4B File Offset: 0x0012DC4B
		// (set) Token: 0x0600480C RID: 18444 RVA: 0x0012FA53 File Offset: 0x0012DC53
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("TrackBarMinimumDescr")]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (this.minimum != value)
				{
					if (value > this.maximum)
					{
						this.maximum = value;
					}
					this.SetRange(value, this.maximum);
				}
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x0600480D RID: 18445 RVA: 0x0012FA7B File Offset: 0x0012DC7B
		// (set) Token: 0x0600480E RID: 18446 RVA: 0x0012FA84 File Offset: 0x0012DC84
		[SRCategory("CatAppearance")]
		[DefaultValue(Orientation.Horizontal)]
		[Localizable(true)]
		[SRDescription("TrackBarOrientationDescr")]
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
					if (this.orientation == Orientation.Horizontal)
					{
						base.SetStyle(ControlStyles.FixedHeight, this.autoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
						base.Width = this.requestedDim;
					}
					else
					{
						base.SetStyle(ControlStyles.FixedHeight, false);
						base.SetStyle(ControlStyles.FixedWidth, this.autoSize);
						base.Height = this.requestedDim;
					}
					if (base.IsHandleCreated)
					{
						Rectangle bounds = base.Bounds;
						base.RecreateHandle();
						base.SetBounds(bounds.X, bounds.Y, bounds.Height, bounds.Width, BoundsSpecified.All);
						this.AdjustSize();
					}
				}
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x0600480F RID: 18447 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06004810 RID: 18448 RVA: 0x0001365E File Offset: 0x0001185E
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

		// Token: 0x14000396 RID: 918
		// (add) Token: 0x06004811 RID: 18449 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06004812 RID: 18450 RVA: 0x00013670 File Offset: 0x00011870
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

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06004813 RID: 18451 RVA: 0x0012FB5C File Offset: 0x0012DD5C
		private int PreferredDimension
		{
			get
			{
				int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(3);
				return systemMetrics * 8 / 3;
			}
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x0012FB75 File Offset: 0x0012DD75
		private void RedrawControl()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1032, 1, this.maximum);
				base.Invalidate();
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06004815 RID: 18453 RVA: 0x0012FB98 File Offset: 0x0012DD98
		// (set) Token: 0x06004816 RID: 18454 RVA: 0x0012FBA0 File Offset: 0x0012DDA0
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
					{
						this.OnRightToLeftLayoutChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x0012FBF4 File Offset: 0x0012DDF4
		// (set) Token: 0x06004818 RID: 18456 RVA: 0x0012FBFC File Offset: 0x0012DDFC
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("TrackBarSmallChangeDescr")]
		public int SmallChange
		{
			get
			{
				return this.smallChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SmallChange", SR.GetString("TrackBarSmallChangeError", new object[]
					{
						value
					}));
				}
				if (this.smallChange != value)
				{
					this.smallChange = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(1047, 0, value);
					}
				}
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06004819 RID: 18457 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x0600481A RID: 18458 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x14000397 RID: 919
		// (add) Token: 0x0600481B RID: 18459 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x0600481C RID: 18460 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x0600481D RID: 18461 RVA: 0x0012FC57 File Offset: 0x0012DE57
		// (set) Token: 0x0600481E RID: 18462 RVA: 0x0012FC5F File Offset: 0x0012DE5F
		[SRCategory("CatAppearance")]
		[DefaultValue(TickStyle.BottomRight)]
		[SRDescription("TrackBarTickStyleDescr")]
		public TickStyle TickStyle
		{
			get
			{
				return this.tickStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TickStyle));
				}
				if (this.tickStyle != value)
				{
					this.tickStyle = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x0012FC9D File Offset: 0x0012DE9D
		// (set) Token: 0x06004820 RID: 18464 RVA: 0x0012FCA5 File Offset: 0x0012DEA5
		[SRCategory("CatAppearance")]
		[DefaultValue(1)]
		[SRDescription("TrackBarTickFrequencyDescr")]
		public int TickFrequency
		{
			get
			{
				return this.tickFrequency;
			}
			set
			{
				if (this.tickFrequency != value)
				{
					this.tickFrequency = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(1044, value, 0);
						base.Invalidate();
					}
				}
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06004821 RID: 18465 RVA: 0x0012FCD3 File Offset: 0x0012DED3
		// (set) Token: 0x06004822 RID: 18466 RVA: 0x0012FCE4 File Offset: 0x0012DEE4
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Bindable(true)]
		[SRDescription("TrackBarValueDescr")]
		public int Value
		{
			get
			{
				this.GetTrackBarValue();
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					if (!this.initializing && (value < this.minimum || value > this.maximum))
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'Minimum'",
							"'Maximum'"
						}));
					}
					this.value = value;
					this.SetTrackBarPosition();
					this.OnValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000398 RID: 920
		// (add) Token: 0x06004823 RID: 18467 RVA: 0x000131E8 File Offset: 0x000113E8
		// (remove) Token: 0x06004824 RID: 18468 RVA: 0x000131F1 File Offset: 0x000113F1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		// Token: 0x14000399 RID: 921
		// (add) Token: 0x06004825 RID: 18469 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x06004826 RID: 18470 RVA: 0x000238FC File Offset: 0x00021AFC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		// Token: 0x1400039A RID: 922
		// (add) Token: 0x06004827 RID: 18471 RVA: 0x000131FA File Offset: 0x000113FA
		// (remove) Token: 0x06004828 RID: 18472 RVA: 0x00013203 File Offset: 0x00011403
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		// Token: 0x1400039B RID: 923
		// (add) Token: 0x06004829 RID: 18473 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x0600482A RID: 18474 RVA: 0x0002390E File Offset: 0x00021B0E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		// Token: 0x1400039C RID: 924
		// (add) Token: 0x0600482B RID: 18475 RVA: 0x0012FD6E File Offset: 0x0012DF6E
		// (remove) Token: 0x0600482C RID: 18476 RVA: 0x0012FD81 File Offset: 0x0012DF81
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(TrackBar.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		// Token: 0x1400039D RID: 925
		// (add) Token: 0x0600482D RID: 18477 RVA: 0x0012FD94 File Offset: 0x0012DF94
		// (remove) Token: 0x0600482E RID: 18478 RVA: 0x0012FDA7 File Offset: 0x0012DFA7
		[SRCategory("CatBehavior")]
		[SRDescription("TrackBarOnScrollDescr")]
		public event EventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(TrackBar.EVENT_SCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.EVENT_SCROLL, value);
			}
		}

		// Token: 0x1400039E RID: 926
		// (add) Token: 0x0600482F RID: 18479 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06004830 RID: 18480 RVA: 0x00013F90 File Offset: 0x00012190
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		// Token: 0x1400039F RID: 927
		// (add) Token: 0x06004831 RID: 18481 RVA: 0x0012FDBA File Offset: 0x0012DFBA
		// (remove) Token: 0x06004832 RID: 18482 RVA: 0x0012FDCD File Offset: 0x0012DFCD
		[SRCategory("CatAction")]
		[SRDescription("valueChangedEventDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(TrackBar.EVENT_VALUECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.EVENT_VALUECHANGED, value);
			}
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x0012FDE0 File Offset: 0x0012DFE0
		private void AdjustSize()
		{
			if (base.IsHandleCreated)
			{
				int num = this.requestedDim;
				try
				{
					if (this.orientation == Orientation.Horizontal)
					{
						base.Height = (this.autoSize ? this.PreferredDimension : num);
					}
					else
					{
						base.Width = (this.autoSize ? this.PreferredDimension : num);
					}
				}
				finally
				{
					this.requestedDim = num;
				}
			}
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x0012FE50 File Offset: 0x0012E050
		public void BeginInit()
		{
			this.initializing = true;
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x0012FE59 File Offset: 0x0012E059
		private void ConstrainValue()
		{
			if (this.initializing)
			{
				return;
			}
			if (this.Value < this.minimum)
			{
				this.Value = this.minimum;
			}
			if (this.Value > this.maximum)
			{
				this.Value = this.maximum;
			}
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0012FE98 File Offset: 0x0012E098
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 4
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0012FEE8 File Offset: 0x0012E0E8
		public void EndInit()
		{
			this.initializing = false;
			this.ConstrainValue();
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0012FEF8 File Offset: 0x0012E0F8
		private void GetTrackBarValue()
		{
			if (base.IsHandleCreated)
			{
				this.value = (int)((long)base.SendMessage(1024, 0, 0));
				if (this.orientation == Orientation.Vertical)
				{
					this.value = this.Minimum + this.Maximum - this.value;
				}
				if (this.orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes && !base.IsMirrored)
				{
					this.value = this.Minimum + this.Maximum - this.value;
				}
			}
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x0012FF7C File Offset: 0x0012E17C
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x0012FFB4 File Offset: 0x0012E1B4
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(1031, 0, this.minimum);
			base.SendMessage(1032, 0, this.maximum);
			base.SendMessage(1044, this.tickFrequency, 0);
			base.SendMessage(1045, 0, this.largeChange);
			base.SendMessage(1047, 0, this.smallChange);
			this.SetTrackBarPosition();
			this.AdjustSize();
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00130034 File Offset: 0x0012E234
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				base.RecreateHandle();
			}
			EventHandler eventHandler = base.Events[TrackBar.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x0013007C File Offset: 0x0012E27C
		protected virtual void OnScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.EVENT_SCROLL];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x001300AC File Offset: 0x0012E2AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
			if (handledMouseEventArgs != null)
			{
				if (handledMouseEventArgs.Handled)
				{
					return;
				}
				handledMouseEventArgs.Handled = true;
			}
			if ((Control.ModifierKeys & (Keys.Shift | Keys.Alt)) != Keys.None || Control.MouseButtons != MouseButtons.None)
			{
				return;
			}
			int mouseWheelScrollLines = SystemInformation.MouseWheelScrollLines;
			if (mouseWheelScrollLines == 0)
			{
				return;
			}
			this.cumulativeWheelData += e.Delta;
			float num = (float)this.cumulativeWheelData / 120f;
			if (mouseWheelScrollLines == -1)
			{
				mouseWheelScrollLines = this.TickFrequency;
			}
			int num2 = (int)((float)mouseWheelScrollLines * num);
			if (num2 != 0)
			{
				if (num2 > 0)
				{
					int num3 = num2;
					this.Value = Math.Min(num3 + this.Value, this.Maximum);
					this.cumulativeWheelData -= (int)((float)num2 * (120f / (float)mouseWheelScrollLines));
				}
				else
				{
					int num3 = -num2;
					this.Value = Math.Max(this.Value - num3, this.Minimum);
					this.cumulativeWheelData -= (int)((float)num2 * (120f / (float)mouseWheelScrollLines));
				}
			}
			if (e.Delta != this.Value)
			{
				this.OnScroll(EventArgs.Empty);
				this.OnValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x001301C8 File Offset: 0x0012E3C8
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.EVENT_VALUECHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x001301F6 File Offset: 0x0012E3F6
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.RedrawControl();
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00130205 File Offset: 0x0012E405
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			this.RedrawControl();
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00130214 File Offset: 0x0012E414
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			this.requestedDim = ((this.orientation == Orientation.Horizontal) ? height : width);
			if (this.autoSize)
			{
				if (this.orientation == Orientation.Horizontal)
				{
					if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
					{
						height = this.PreferredDimension;
					}
				}
				else if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					width = this.PreferredDimension;
				}
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00130270 File Offset: 0x0012E470
		public void SetRange(int minValue, int maxValue)
		{
			if (this.minimum != minValue || this.maximum != maxValue)
			{
				if (minValue > maxValue)
				{
					maxValue = minValue;
				}
				this.minimum = minValue;
				this.maximum = maxValue;
				if (base.IsHandleCreated)
				{
					base.SendMessage(1031, 0, this.minimum);
					base.SendMessage(1032, 1, this.maximum);
					base.Invalidate();
				}
				if (this.value < this.minimum)
				{
					this.value = this.minimum;
				}
				if (this.value > this.maximum)
				{
					this.value = this.maximum;
				}
				this.SetTrackBarPosition();
			}
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00130318 File Offset: 0x0012E518
		private void SetTrackBarPosition()
		{
			if (base.IsHandleCreated)
			{
				int lparam = this.value;
				if (this.orientation == Orientation.Vertical)
				{
					lparam = this.Minimum + this.Maximum - this.value;
				}
				if (this.orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes && !base.IsMirrored)
				{
					lparam = this.Minimum + this.Maximum - this.value;
				}
				base.SendMessage(1029, 1, lparam);
			}
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00130390 File Offset: 0x0012E590
		public override string ToString()
		{
			string text = base.ToString();
			return string.Concat(new string[]
			{
				text,
				", Minimum: ",
				this.Minimum.ToString(CultureInfo.CurrentCulture),
				", Maximum: ",
				this.Maximum.ToString(CultureInfo.CurrentCulture),
				", Value: ",
				this.Value.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00130410 File Offset: 0x0012E610
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg - 8468 <= 1)
			{
				int num = NativeMethods.Util.LOWORD(m.WParam);
				if ((num <= 3 || num - 5 <= 3) && this.value != this.Value)
				{
					this.OnScroll(EventArgs.Empty);
					this.OnValueChanged(EventArgs.Empty);
					return;
				}
			}
			else
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x04002719 RID: 10009
		private static readonly object EVENT_SCROLL = new object();

		// Token: 0x0400271A RID: 10010
		private static readonly object EVENT_VALUECHANGED = new object();

		// Token: 0x0400271B RID: 10011
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x0400271C RID: 10012
		private bool autoSize = true;

		// Token: 0x0400271D RID: 10013
		private int largeChange = 5;

		// Token: 0x0400271E RID: 10014
		private int maximum = 10;

		// Token: 0x0400271F RID: 10015
		private int minimum;

		// Token: 0x04002720 RID: 10016
		private Orientation orientation;

		// Token: 0x04002721 RID: 10017
		private int value;

		// Token: 0x04002722 RID: 10018
		private int smallChange = 1;

		// Token: 0x04002723 RID: 10019
		private int tickFrequency = 1;

		// Token: 0x04002724 RID: 10020
		private TickStyle tickStyle = TickStyle.BottomRight;

		// Token: 0x04002725 RID: 10021
		private int requestedDim;

		// Token: 0x04002726 RID: 10022
		private int cumulativeWheelData;

		// Token: 0x04002727 RID: 10023
		private bool initializing;

		// Token: 0x04002728 RID: 10024
		private bool rightToLeftLayout;
	}
}
