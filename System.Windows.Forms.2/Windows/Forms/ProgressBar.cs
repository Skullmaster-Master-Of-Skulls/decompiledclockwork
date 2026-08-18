using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000329 RID: 809
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultBindingProperty("Value")]
	[SRDescription("DescriptionProgressBar")]
	public class ProgressBar : Control
	{
		// Token: 0x060033A6 RID: 13222 RVA: 0x000EB3F8 File Offset: 0x000E95F8
		public ProgressBar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Selectable | ControlStyles.UseTextForAccessibility, false);
			this.ForeColor = this.defaultForeColor;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060033A7 RID: 13223 RVA: 0x000EB448 File Offset: 0x000E9648
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "msctls_progress32";
				if (this.Style == ProgressBarStyle.Continuous)
				{
					createParams.Style |= 1;
				}
				else if (this.Style == ProgressBarStyle.Marquee && !base.DesignMode)
				{
					createParams.Style |= 8;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x000B90B9 File Offset: 0x000B72B9
		// (set) Token: 0x060033A9 RID: 13225 RVA: 0x000B90C1 File Offset: 0x000B72C1
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

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060033AA RID: 13226 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060033AB RID: 13227 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060033AC RID: 13228 RVA: 0x000EB4D5 File Offset: 0x000E96D5
		// (set) Token: 0x060033AD RID: 13229 RVA: 0x000EB4E0 File Offset: 0x000E96E0
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(ProgressBarStyle.Blocks)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStyleDescr")]
		public ProgressBarStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (this.style != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ProgressBarStyle));
					}
					this.style = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
					if (this.style == ProgressBarStyle.Marquee)
					{
						this.StartMarquee();
					}
				}
			}
		}

		// Token: 0x14000261 RID: 609
		// (add) Token: 0x060033AE RID: 13230 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x060033AF RID: 13231 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060033B0 RID: 13232 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060033B1 RID: 13233 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x14000262 RID: 610
		// (add) Token: 0x060033B2 RID: 13234 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x060033B3 RID: 13235 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060033B4 RID: 13236 RVA: 0x000E2B53 File Offset: 0x000E0D53
		// (set) Token: 0x060033B5 RID: 13237 RVA: 0x000E2B5B File Offset: 0x000E0D5B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x14000263 RID: 611
		// (add) Token: 0x060033B6 RID: 13238 RVA: 0x000E2B64 File Offset: 0x000E0D64
		// (remove) Token: 0x060033B7 RID: 13239 RVA: 0x000E2B6D File Offset: 0x000E0D6D
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

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000EB540 File Offset: 0x000E9740
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 23);
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x060033BB RID: 13243 RVA: 0x000131DF File Offset: 0x000113DF
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

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x060033BD RID: 13245 RVA: 0x0001A27A File Offset: 0x0001847A
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

		// Token: 0x14000264 RID: 612
		// (add) Token: 0x060033BE RID: 13246 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		// (remove) Token: 0x060033BF RID: 13247 RVA: 0x0005AAE9 File Offset: 0x00058CE9
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

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x0001A1F5 File Offset: 0x000183F5
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

		// Token: 0x14000265 RID: 613
		// (add) Token: 0x060033C2 RID: 13250 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x060033C3 RID: 13251 RVA: 0x00024115 File Offset: 0x00022315
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

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000EB54B File Offset: 0x000E974B
		// (set) Token: 0x060033C5 RID: 13253 RVA: 0x000EB553 File Offset: 0x000E9753
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarMarqueeAnimationSpeed")]
		public int MarqueeAnimationSpeed
		{
			get
			{
				return this.marqueeSpeed;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("MarqueeAnimationSpeed must be non-negative");
				}
				this.marqueeSpeed = value;
				if (!base.DesignMode)
				{
					this.StartMarquee();
				}
			}
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000EB57C File Offset: 0x000E977C
		private void StartMarquee()
		{
			if (base.IsHandleCreated && this.style == ProgressBarStyle.Marquee)
			{
				if (this.marqueeSpeed == 0)
				{
					base.SendMessage(1034, 0, this.marqueeSpeed);
					return;
				}
				base.SendMessage(1034, 1, this.marqueeSpeed);
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000EB5C9 File Offset: 0x000E97C9
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000EB5D4 File Offset: 0x000E97D4
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMaximumDescr")]
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
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Maximum", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Maximum",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.minimum > value)
					{
						this.minimum = value;
					}
					this.maximum = value;
					if (this.value > this.maximum)
					{
						this.value = this.maximum;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(1030, this.minimum, this.maximum);
						this.UpdatePos();
					}
				}
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000EB68B File Offset: 0x000E988B
		// (set) Token: 0x060033CA RID: 13258 RVA: 0x000EB694 File Offset: 0x000E9894
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMinimumDescr")]
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
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Minimum", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Minimum",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.maximum < value)
					{
						this.maximum = value;
					}
					this.minimum = value;
					if (this.value < this.minimum)
					{
						this.value = this.minimum;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(1030, this.minimum, this.maximum);
						this.UpdatePos();
					}
				}
			}
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000EB74B File Offset: 0x000E994B
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 8193, 0, ColorTranslator.ToWin32(this.BackColor));
			}
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000EB77F File Offset: 0x000E997F
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1033, 0, ColorTranslator.ToWin32(this.ForeColor));
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x060033CE RID: 13262 RVA: 0x0001365E File Offset: 0x0001185E
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

		// Token: 0x14000266 RID: 614
		// (add) Token: 0x060033CF RID: 13263 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x060033D0 RID: 13264 RVA: 0x00013670 File Offset: 0x00011870
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

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000EB7B3 File Offset: 0x000E99B3
		// (set) Token: 0x060033D2 RID: 13266 RVA: 0x000EB7BC File Offset: 0x000E99BC
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

		// Token: 0x14000267 RID: 615
		// (add) Token: 0x060033D3 RID: 13267 RVA: 0x000EB810 File Offset: 0x000E9A10
		// (remove) Token: 0x060033D4 RID: 13268 RVA: 0x000EB829 File Offset: 0x000E9A29
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Combine(this.onRightToLeftLayoutChanged, value);
			}
			remove
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Remove(this.onRightToLeftLayoutChanged, value);
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000EB842 File Offset: 0x000E9A42
		// (set) Token: 0x060033D6 RID: 13270 RVA: 0x000EB84A File Offset: 0x000E9A4A
		[DefaultValue(10)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStepDescr")]
		public int Step
		{
			get
			{
				return this.step;
			}
			set
			{
				this.step = value;
				if (base.IsHandleCreated)
				{
					base.SendMessage(1028, this.step, 0);
				}
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x000B2619 File Offset: 0x000B0819
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

		// Token: 0x14000268 RID: 616
		// (add) Token: 0x060033D9 RID: 13273 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x060033DA RID: 13274 RVA: 0x000B262B File Offset: 0x000B082B
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

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x060033DC RID: 13276 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x14000269 RID: 617
		// (add) Token: 0x060033DD RID: 13277 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x060033DE RID: 13278 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000EB86E File Offset: 0x000E9A6E
		// (set) Token: 0x060033E0 RID: 13280 RVA: 0x000EB878 File Offset: 0x000E9A78
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[Bindable(true)]
		[SRDescription("ProgressBarValueDescr")]
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
					this.UpdatePos();
				}
			}
		}

		// Token: 0x1400026A RID: 618
		// (add) Token: 0x060033E1 RID: 13281 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x060033E2 RID: 13282 RVA: 0x000238FC File Offset: 0x00021AFC
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

		// Token: 0x1400026B RID: 619
		// (add) Token: 0x060033E3 RID: 13283 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x060033E4 RID: 13284 RVA: 0x0002390E File Offset: 0x00021B0E
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

		// Token: 0x1400026C RID: 620
		// (add) Token: 0x060033E5 RID: 13285 RVA: 0x000B9380 File Offset: 0x000B7580
		// (remove) Token: 0x060033E6 RID: 13286 RVA: 0x000B9389 File Offset: 0x000B7589
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

		// Token: 0x1400026D RID: 621
		// (add) Token: 0x060033E7 RID: 13287 RVA: 0x000B9392 File Offset: 0x000B7592
		// (remove) Token: 0x060033E8 RID: 13288 RVA: 0x000B939B File Offset: 0x000B759B
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

		// Token: 0x1400026E RID: 622
		// (add) Token: 0x060033E9 RID: 13289 RVA: 0x000B93A4 File Offset: 0x000B75A4
		// (remove) Token: 0x060033EA RID: 13290 RVA: 0x000B93AD File Offset: 0x000B75AD
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

		// Token: 0x1400026F RID: 623
		// (add) Token: 0x060033EB RID: 13291 RVA: 0x000E35B4 File Offset: 0x000E17B4
		// (remove) Token: 0x060033EC RID: 13292 RVA: 0x000E35BD File Offset: 0x000E17BD
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

		// Token: 0x14000270 RID: 624
		// (add) Token: 0x060033ED RID: 13293 RVA: 0x000E35C6 File Offset: 0x000E17C6
		// (remove) Token: 0x060033EE RID: 13294 RVA: 0x000E35CF File Offset: 0x000E17CF
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

		// Token: 0x14000271 RID: 625
		// (add) Token: 0x060033EF RID: 13295 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x060033F0 RID: 13296 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x060033F1 RID: 13297 RVA: 0x000EB8F0 File Offset: 0x000E9AF0
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 32
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000EB940 File Offset: 0x000E9B40
		public void Increment(int value)
		{
			if (this.Style == ProgressBarStyle.Marquee)
			{
				throw new InvalidOperationException(SR.GetString("ProgressBarIncrementMarqueeException"));
			}
			this.value += value;
			if (this.value < this.minimum)
			{
				this.value = this.minimum;
			}
			if (this.value > this.maximum)
			{
				this.value = this.maximum;
			}
			this.UpdatePos();
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000EB9B0 File Offset: 0x000E9BB0
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(1030, this.minimum, this.maximum);
			base.SendMessage(1028, this.step, 0);
			base.SendMessage(1026, this.value, 0);
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 8193, 0, ColorTranslator.ToWin32(this.BackColor));
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1033, 0, ColorTranslator.ToWin32(this.ForeColor));
			this.StartMarquee();
			SystemEvents.UserPreferenceChanged += this.UserPreferenceChangedHandler;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000EBA5F File Offset: 0x000E9C5F
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.UserPreferenceChangedHandler;
			base.OnHandleDestroyed(e);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000EBA79 File Offset: 0x000E9C79
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
			if (this.onRightToLeftLayoutChanged != null)
			{
				this.onRightToLeftLayoutChanged(this, e);
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000EBAA8 File Offset: 0x000E9CA8
		public void PerformStep()
		{
			if (this.Style == ProgressBarStyle.Marquee)
			{
				throw new InvalidOperationException(SR.GetString("ProgressBarPerformStepMarqueeException"));
			}
			this.Increment(this.step);
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000EBACF File Offset: 0x000E9CCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ResetForeColor()
		{
			this.ForeColor = this.defaultForeColor;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000EBADD File Offset: 0x000E9CDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeForeColor()
		{
			return this.ForeColor != this.defaultForeColor;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x00028D57 File Offset: 0x00026F57
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000EBAF0 File Offset: 0x000E9CF0
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

		// Token: 0x060033FB RID: 13307 RVA: 0x000EBB6D File Offset: 0x000E9D6D
		private void UpdatePos()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(1026, this.value, 0);
			}
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000EBB8C File Offset: 0x000E9D8C
		private void UserPreferenceChangedHandler(object o, UserPreferenceChangedEventArgs e)
		{
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1033, 0, ColorTranslator.ToWin32(this.ForeColor));
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 8193, 0, ColorTranslator.ToWin32(this.BackColor));
			}
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000EBBE7 File Offset: 0x000E9DE7
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ProgressBar.ProgressBarAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x04001ED2 RID: 7890
		private int minimum;

		// Token: 0x04001ED3 RID: 7891
		private int maximum = 100;

		// Token: 0x04001ED4 RID: 7892
		private int step = 10;

		// Token: 0x04001ED5 RID: 7893
		private int value;

		// Token: 0x04001ED6 RID: 7894
		private int marqueeSpeed = 100;

		// Token: 0x04001ED7 RID: 7895
		private Color defaultForeColor = SystemColors.Highlight;

		// Token: 0x04001ED8 RID: 7896
		private ProgressBarStyle style;

		// Token: 0x04001ED9 RID: 7897
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04001EDA RID: 7898
		private bool rightToLeftLayout;

		// Token: 0x020007CD RID: 1997
		[ComVisible(true)]
		internal class ProgressBarAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006D83 RID: 28035 RVA: 0x0009B963 File Offset: 0x00099B63
			internal ProgressBarAccessibleObject(ProgressBar owner) : base(owner)
			{
			}

			// Token: 0x170017E9 RID: 6121
			// (get) Token: 0x06006D84 RID: 28036 RVA: 0x00192790 File Offset: 0x00190990
			private ProgressBar OwningProgressBar
			{
				get
				{
					return base.Owner as ProgressBar;
				}
			}

			// Token: 0x06006D85 RID: 28037 RVA: 0x0009B96C File Offset: 0x00099B6C
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed() && (AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported());
			}

			// Token: 0x06006D86 RID: 28038 RVA: 0x0019279D File Offset: 0x0019099D
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && (patternId == 10002 || patternId == 10003 || patternId == 10018 || base.IsPatternSupported(patternId));
			}

			// Token: 0x06006D87 RID: 28039 RVA: 0x001927CC File Offset: 0x001909CC
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID > 30009)
				{
					if (propertyID <= 30043)
					{
						if (propertyID != 30033 && propertyID != 30043)
						{
							goto IL_7F;
						}
					}
					else if (propertyID != 30048)
					{
						if (propertyID - 30051 > 1)
						{
							goto IL_7F;
						}
						return double.NaN;
					}
					return true;
				}
				if (propertyID == 30003)
				{
					return 50012;
				}
				if (propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID == 30009)
				{
					return true;
				}
				IL_7F:
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006D88 RID: 28040 RVA: 0x0019285F File Offset: 0x00190A5F
			internal override void SetValue(double newValue)
			{
				throw new InvalidOperationException("Progress Bar is read-only.");
			}

			// Token: 0x170017EA RID: 6122
			// (get) Token: 0x06006D89 RID: 28041 RVA: 0x00016297 File Offset: 0x00014497
			internal override double LargeChange
			{
				get
				{
					return double.NaN;
				}
			}

			// Token: 0x170017EB RID: 6123
			// (get) Token: 0x06006D8A RID: 28042 RVA: 0x0019286C File Offset: 0x00190A6C
			internal override double Maximum
			{
				get
				{
					ProgressBar owningProgressBar = this.OwningProgressBar;
					int? num = (owningProgressBar != null) ? new int?(owningProgressBar.Maximum) : null;
					if (num == null)
					{
						return double.NaN;
					}
					return (double)num.GetValueOrDefault();
				}
			}

			// Token: 0x170017EC RID: 6124
			// (get) Token: 0x06006D8B RID: 28043 RVA: 0x001928B4 File Offset: 0x00190AB4
			internal override double Minimum
			{
				get
				{
					ProgressBar owningProgressBar = this.OwningProgressBar;
					int? num = (owningProgressBar != null) ? new int?(owningProgressBar.Minimum) : null;
					if (num == null)
					{
						return double.NaN;
					}
					return (double)num.GetValueOrDefault();
				}
			}

			// Token: 0x170017ED RID: 6125
			// (get) Token: 0x06006D8C RID: 28044 RVA: 0x00016297 File Offset: 0x00014497
			internal override double SmallChange
			{
				get
				{
					return double.NaN;
				}
			}

			// Token: 0x170017EE RID: 6126
			// (get) Token: 0x06006D8D RID: 28045 RVA: 0x001928FC File Offset: 0x00190AFC
			internal override double RangeValue
			{
				get
				{
					ProgressBar owningProgressBar = this.OwningProgressBar;
					int? num = (owningProgressBar != null) ? new int?(owningProgressBar.Value) : null;
					if (num == null)
					{
						return double.NaN;
					}
					return (double)num.GetValueOrDefault();
				}
			}

			// Token: 0x170017EF RID: 6127
			// (get) Token: 0x06006D8E RID: 28046 RVA: 0x00013062 File Offset: 0x00011262
			internal override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}
		}
	}
}
