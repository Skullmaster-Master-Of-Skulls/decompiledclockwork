using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x02000357 RID: 855
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("Scroll")]
	public abstract class ScrollBar : Control
	{
		// Token: 0x060037F0 RID: 14320 RVA: 0x000F9BB8 File Offset: 0x000F7DB8
		public ScrollBar()
		{
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			this.TabStop = false;
			if ((this.CreateParams.Style & 1) != 0)
			{
				this.scrollOrientation = ScrollOrientation.VerticalScroll;
				return;
			}
			this.scrollOrientation = ScrollOrientation.HorizontalScroll;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060037F1 RID: 14321 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x060037F2 RID: 14322 RVA: 0x00011A4D File Offset: 0x0000FC4D
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

		// Token: 0x14000299 RID: 665
		// (add) Token: 0x060037F3 RID: 14323 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x060037F4 RID: 14324 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060037F5 RID: 14325 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x060037F6 RID: 14326 RVA: 0x00012F98 File Offset: 0x00011198
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
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

		// Token: 0x1400029A RID: 666
		// (add) Token: 0x060037F7 RID: 14327 RVA: 0x00058DD2 File Offset: 0x00056FD2
		// (remove) Token: 0x060037F8 RID: 14328 RVA: 0x00058DDB File Offset: 0x00056FDB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060037F9 RID: 14329 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060037FA RID: 14330 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x1400029B RID: 667
		// (add) Token: 0x060037FB RID: 14331 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x060037FC RID: 14332 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x060037FD RID: 14333 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060037FE RID: 14334 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x1400029C RID: 668
		// (add) Token: 0x060037FF RID: 14335 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06003800 RID: 14336 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06003801 RID: 14337 RVA: 0x000F9C30 File Offset: 0x000F7E30
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SCROLLBAR";
				createParams.Style &= -8388609;
				return createParams;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06003803 RID: 14339 RVA: 0x00019BFD File Offset: 0x00017DFD
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000F9C62 File Offset: 0x000F7E62
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements && this.ScaleScrollBarForDpiChange)
			{
				base.Scale((float)deviceDpiNew / (float)deviceDpiOld);
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06003805 RID: 14341 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x06003806 RID: 14342 RVA: 0x00013238 File Offset: 0x00011438
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

		// Token: 0x1400029D RID: 669
		// (add) Token: 0x06003807 RID: 14343 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06003808 RID: 14344 RVA: 0x0005AAD7 File Offset: 0x00058CD7
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

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06003809 RID: 14345 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x0600380A RID: 14346 RVA: 0x0001A27A File Offset: 0x0001847A
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

		// Token: 0x1400029E RID: 670
		// (add) Token: 0x0600380B RID: 14347 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		// (remove) Token: 0x0600380C RID: 14348 RVA: 0x0005AAE9 File Offset: 0x00058CE9
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

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x0600380E RID: 14350 RVA: 0x0001A1F5 File Offset: 0x000183F5
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

		// Token: 0x1400029F RID: 671
		// (add) Token: 0x0600380F RID: 14351 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x06003810 RID: 14352 RVA: 0x00024115 File Offset: 0x00022315
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

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06003811 RID: 14353 RVA: 0x000F9C86 File Offset: 0x000F7E86
		// (set) Token: 0x06003812 RID: 14354 RVA: 0x000F9CA4 File Offset: 0x000F7EA4
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[SRDescription("ScrollBarLargeChangeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int LargeChange
		{
			get
			{
				return Math.Min(this.largeChange, this.maximum - this.minimum + 1);
			}
			set
			{
				if (this.largeChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("LargeChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"LargeChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.largeChange = value;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06003813 RID: 14355 RVA: 0x000F9D0E File Offset: 0x000F7F0E
		// (set) Token: 0x06003814 RID: 14356 RVA: 0x000F9D16 File Offset: 0x000F7F16
		[SRCategory("CatBehavior")]
		[DefaultValue(100)]
		[SRDescription("ScrollBarMaximumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
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
					if (this.minimum > value)
					{
						this.minimum = value;
					}
					if (value < this.value)
					{
						this.Value = value;
					}
					this.maximum = value;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06003815 RID: 14357 RVA: 0x000F9D4E File Offset: 0x000F7F4E
		// (set) Token: 0x06003816 RID: 14358 RVA: 0x000F9D56 File Offset: 0x000F7F56
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[SRDescription("ScrollBarMinimumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
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
					if (this.maximum < value)
					{
						this.maximum = value;
					}
					if (value > this.value)
					{
						this.value = value;
					}
					this.minimum = value;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06003817 RID: 14359 RVA: 0x000F9D8E File Offset: 0x000F7F8E
		// (set) Token: 0x06003818 RID: 14360 RVA: 0x000F9DA4 File Offset: 0x000F7FA4
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("ScrollBarSmallChangeDescr")]
		public int SmallChange
		{
			get
			{
				return Math.Min(this.smallChange, this.LargeChange);
			}
			set
			{
				if (this.smallChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("SmallChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SmallChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.smallChange = value;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06003819 RID: 14361 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x0600381A RID: 14362 RVA: 0x000B2619 File Offset: 0x000B0819
		[DefaultValue(false)]
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

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x0600381B RID: 14363 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x0600381C RID: 14364 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x140002A0 RID: 672
		// (add) Token: 0x0600381D RID: 14365 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x0600381E RID: 14366 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x0600381F RID: 14367 RVA: 0x000F9E0E File Offset: 0x000F800E
		// (set) Token: 0x06003820 RID: 14368 RVA: 0x000F9E18 File Offset: 0x000F8018
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Bindable(true)]
		[SRDescription("ScrollBarValueDescr")]
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					if (value < this.minimum || value > this.maximum)
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'minimum'",
							"'maximum'"
						}));
					}
					this.value = value;
					this.UpdateScrollInfo();
					this.OnValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06003821 RID: 14369 RVA: 0x000F9E9A File Offset: 0x000F809A
		// (set) Token: 0x06003822 RID: 14370 RVA: 0x000F9EA2 File Offset: 0x000F80A2
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRDescription("ControlDpiChangeScale")]
		public bool ScaleScrollBarForDpiChange
		{
			get
			{
				return this.scaleScrollBarForDpiChange;
			}
			set
			{
				this.scaleScrollBarForDpiChange = value;
			}
		}

		// Token: 0x140002A1 RID: 673
		// (add) Token: 0x06003823 RID: 14371 RVA: 0x000131E8 File Offset: 0x000113E8
		// (remove) Token: 0x06003824 RID: 14372 RVA: 0x000131F1 File Offset: 0x000113F1
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

		// Token: 0x140002A2 RID: 674
		// (add) Token: 0x06003825 RID: 14373 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06003826 RID: 14374 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x140002A3 RID: 675
		// (add) Token: 0x06003827 RID: 14375 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x06003828 RID: 14376 RVA: 0x000238FC File Offset: 0x00021AFC
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

		// Token: 0x140002A4 RID: 676
		// (add) Token: 0x06003829 RID: 14377 RVA: 0x000131FA File Offset: 0x000113FA
		// (remove) Token: 0x0600382A RID: 14378 RVA: 0x00013203 File Offset: 0x00011403
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

		// Token: 0x140002A5 RID: 677
		// (add) Token: 0x0600382B RID: 14379 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x0600382C RID: 14380 RVA: 0x0002390E File Offset: 0x00021B0E
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

		// Token: 0x140002A6 RID: 678
		// (add) Token: 0x0600382D RID: 14381 RVA: 0x000B93B6 File Offset: 0x000B75B6
		// (remove) Token: 0x0600382E RID: 14382 RVA: 0x000B93BF File Offset: 0x000B75BF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		// Token: 0x140002A7 RID: 679
		// (add) Token: 0x0600382F RID: 14383 RVA: 0x000B93C8 File Offset: 0x000B75C8
		// (remove) Token: 0x06003830 RID: 14384 RVA: 0x000B93D1 File Offset: 0x000B75D1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		// Token: 0x140002A8 RID: 680
		// (add) Token: 0x06003831 RID: 14385 RVA: 0x00011C92 File Offset: 0x0000FE92
		// (remove) Token: 0x06003832 RID: 14386 RVA: 0x00011C9B File Offset: 0x0000FE9B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		// Token: 0x140002A9 RID: 681
		// (add) Token: 0x06003833 RID: 14387 RVA: 0x000F9EAB File Offset: 0x000F80AB
		// (remove) Token: 0x06003834 RID: 14388 RVA: 0x000F9EBE File Offset: 0x000F80BE
		[SRCategory("CatAction")]
		[SRDescription("ScrollBarOnScrollDescr")]
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ScrollBar.EVENT_SCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.EVENT_SCROLL, value);
			}
		}

		// Token: 0x140002AA RID: 682
		// (add) Token: 0x06003835 RID: 14389 RVA: 0x000F9ED1 File Offset: 0x000F80D1
		// (remove) Token: 0x06003836 RID: 14390 RVA: 0x000F9EE4 File Offset: 0x000F80E4
		[SRCategory("CatAction")]
		[SRDescription("valueChangedEventDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(ScrollBar.EVENT_VALUECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.EVENT_VALUECHANGED, value);
			}
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000F9EF7 File Offset: 0x000F80F7
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (this.scrollOrientation == ScrollOrientation.VerticalScroll)
			{
				specified &= ~BoundsSpecified.Width;
			}
			else
			{
				specified &= ~BoundsSpecified.Height;
			}
			return base.GetScaledBounds(bounds, factor, specified);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000F9F19 File Offset: 0x000F8119
		internal override IntPtr InitializeDCForWmCtlColor(IntPtr dc, int msg)
		{
			return IntPtr.Zero;
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000F9F20 File Offset: 0x000F8120
		protected override void OnEnabledChanged(EventArgs e)
		{
			if (base.Enabled)
			{
				this.UpdateScrollInfo();
			}
			base.OnEnabledChanged(e);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000F9F37 File Offset: 0x000F8137
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateScrollInfo();
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000F9F48 File Offset: 0x000F8148
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.EVENT_SCROLL];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, se);
			}
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000F9F78 File Offset: 0x000F8178
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			this.wheelDelta += e.Delta;
			bool flag = false;
			while (Math.Abs(this.wheelDelta) >= 120)
			{
				if (this.wheelDelta > 0)
				{
					this.wheelDelta -= 120;
					this.DoScroll(ScrollEventType.SmallDecrement);
					flag = true;
				}
				else
				{
					this.wheelDelta += 120;
					this.DoScroll(ScrollEventType.SmallIncrement);
					flag = true;
				}
			}
			if (flag)
			{
				this.DoScroll(ScrollEventType.EndScroll);
			}
			if (e is HandledMouseEventArgs)
			{
				((HandledMouseEventArgs)e).Handled = true;
			}
			base.OnMouseWheel(e);
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000FA00C File Offset: 0x000F820C
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ScrollBar.EVENT_VALUECHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000FA03A File Offset: 0x000F823A
		private int ReflectPosition(int position)
		{
			if (this is HScrollBar)
			{
				return this.minimum + (this.maximum - this.LargeChange + 1) - position;
			}
			return position;
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000FA060 File Offset: 0x000F8260
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

		// Token: 0x06003840 RID: 14400 RVA: 0x000FA0E0 File Offset: 0x000F82E0
		protected void UpdateScrollInfo()
		{
			if (base.IsHandleCreated && base.Enabled)
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
				scrollinfo.fMask = 23;
				scrollinfo.nMin = this.minimum;
				scrollinfo.nMax = this.maximum;
				scrollinfo.nPage = this.LargeChange;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					scrollinfo.nPos = this.ReflectPosition(this.value);
				}
				else
				{
					scrollinfo.nPos = this.value;
				}
				scrollinfo.nTrackPos = 0;
				UnsafeNativeMethods.SetScrollInfo(new HandleRef(this, base.Handle), 2, scrollinfo, true);
			}
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000FA190 File Offset: 0x000F8390
		private void WmReflectScroll(ref Message m)
		{
			ScrollEventType type = (ScrollEventType)NativeMethods.Util.LOWORD(m.WParam);
			this.DoScroll(type);
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x000FA1B0 File Offset: 0x000F83B0
		private void DoScroll(ScrollEventType type)
		{
			if (this.RightToLeft == RightToLeft.Yes)
			{
				switch (type)
				{
				case ScrollEventType.SmallDecrement:
					type = ScrollEventType.SmallIncrement;
					break;
				case ScrollEventType.SmallIncrement:
					type = ScrollEventType.SmallDecrement;
					break;
				case ScrollEventType.LargeDecrement:
					type = ScrollEventType.LargeIncrement;
					break;
				case ScrollEventType.LargeIncrement:
					type = ScrollEventType.LargeDecrement;
					break;
				case ScrollEventType.First:
					type = ScrollEventType.Last;
					break;
				case ScrollEventType.Last:
					type = ScrollEventType.First;
					break;
				}
			}
			int newValue = this.value;
			int oldValue = this.value;
			switch (type)
			{
			case ScrollEventType.SmallDecrement:
				newValue = Math.Max(this.value - this.SmallChange, this.minimum);
				break;
			case ScrollEventType.SmallIncrement:
				newValue = Math.Min(this.value + this.SmallChange, this.maximum - this.LargeChange + 1);
				break;
			case ScrollEventType.LargeDecrement:
				newValue = Math.Max(this.value - this.LargeChange, this.minimum);
				break;
			case ScrollEventType.LargeIncrement:
				newValue = Math.Min(this.value + this.LargeChange, this.maximum - this.LargeChange + 1);
				break;
			case ScrollEventType.ThumbPosition:
			case ScrollEventType.ThumbTrack:
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.fMask = 16;
				SafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 2, scrollinfo);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					newValue = this.ReflectPosition(scrollinfo.nTrackPos);
				}
				else
				{
					newValue = scrollinfo.nTrackPos;
				}
				break;
			}
			case ScrollEventType.First:
				newValue = this.minimum;
				break;
			case ScrollEventType.Last:
				newValue = this.maximum - this.LargeChange + 1;
				break;
			}
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(type, oldValue, newValue, this.scrollOrientation);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000FA34C File Offset: 0x000F854C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 5)
			{
				if (msg != 20)
				{
					if (msg - 8468 <= 1)
					{
						this.WmReflectScroll(ref m);
						return;
					}
					base.WndProc(ref m);
				}
			}
			else if (UnsafeNativeMethods.GetFocus() == base.Handle)
			{
				this.DefWndProc(ref m);
				base.SendMessage(8, 0, 0);
				base.SendMessage(7, 0, 0);
				return;
			}
		}

		// Token: 0x04002187 RID: 8583
		private static readonly object EVENT_SCROLL = new object();

		// Token: 0x04002188 RID: 8584
		private static readonly object EVENT_VALUECHANGED = new object();

		// Token: 0x04002189 RID: 8585
		private int minimum;

		// Token: 0x0400218A RID: 8586
		private int maximum = 100;

		// Token: 0x0400218B RID: 8587
		private int smallChange = 1;

		// Token: 0x0400218C RID: 8588
		private int largeChange = 10;

		// Token: 0x0400218D RID: 8589
		private int value;

		// Token: 0x0400218E RID: 8590
		private ScrollOrientation scrollOrientation;

		// Token: 0x0400218F RID: 8591
		private int wheelDelta;

		// Token: 0x04002190 RID: 8592
		private bool scaleScrollBarForDpiChange = true;
	}
}
