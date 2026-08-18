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
	// Token: 0x0200022B RID: 555
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Value")]
	[DefaultEvent("ValueChanged")]
	[DefaultBindingProperty("Value")]
	[Designer("System.Windows.Forms.Design.DateTimePickerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionDateTimePicker")]
	public class DateTimePicker : Control
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x000AAC68 File Offset: 0x000A8E68
		public DateTimePicker()
		{
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, false);
			this.format = DateTimePickerFormat.Long;
			if (AccessibilityImprovements.Level3)
			{
				base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x00027F43 File Offset: 0x00026143
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x00012F98 File Offset: 0x00011198
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x14000187 RID: 391
		// (add) Token: 0x060023DF RID: 9183 RVA: 0x00058DD2 File Offset: 0x00056FD2
		// (remove) Token: 0x060023E0 RID: 9184 RVA: 0x00058DDB File Offset: 0x00056FDB
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

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x14000188 RID: 392
		// (add) Token: 0x060023E3 RID: 9187 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x060023E4 RID: 9188 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x14000189 RID: 393
		// (add) Token: 0x060023E7 RID: 9191 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x060023E8 RID: 9192 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060023E9 RID: 9193 RVA: 0x000AAD27 File Offset: 0x000A8F27
		// (set) Token: 0x060023EA RID: 9194 RVA: 0x000AAD30 File Offset: 0x000A8F30
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarForeColorDescr")]
		public Color CalendarForeColor
		{
			get
			{
				return this.calendarForeColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
					{
						"value"
					}));
				}
				if (!value.Equals(this.calendarForeColor))
				{
					this.calendarForeColor = value;
					this.SetControlColor(1, value);
				}
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060023EB RID: 9195 RVA: 0x000AAD8D File Offset: 0x000A8F8D
		// (set) Token: 0x060023EC RID: 9196 RVA: 0x000AADA4 File Offset: 0x000A8FA4
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[AmbientValue(null)]
		[SRDescription("DateTimePickerCalendarFontDescr")]
		public Font CalendarFont
		{
			get
			{
				if (this.calendarFont == null)
				{
					return this.Font;
				}
				return this.calendarFont;
			}
			set
			{
				if ((value == null && this.calendarFont != null) || (value != null && !value.Equals(this.calendarFont)))
				{
					this.calendarFont = value;
					this.calendarFontHandleWrapper = null;
					this.SetControlCalendarFont();
				}
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000AADD6 File Offset: 0x000A8FD6
		private IntPtr CalendarFontHandle
		{
			get
			{
				if (this.calendarFont == null)
				{
					return base.FontHandle;
				}
				if (this.calendarFontHandleWrapper == null)
				{
					this.calendarFontHandleWrapper = new Control.FontHandleWrapper(this.CalendarFont);
				}
				return this.calendarFontHandleWrapper.Handle;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x000AAE0B File Offset: 0x000A900B
		// (set) Token: 0x060023EF RID: 9199 RVA: 0x000AAE14 File Offset: 0x000A9014
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarTitleBackColorDescr")]
		public Color CalendarTitleBackColor
		{
			get
			{
				return this.calendarTitleBackColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
					{
						"value"
					}));
				}
				if (!value.Equals(this.calendarTitleBackColor))
				{
					this.calendarTitleBackColor = value;
					this.SetControlColor(2, value);
				}
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000AAE71 File Offset: 0x000A9071
		// (set) Token: 0x060023F1 RID: 9201 RVA: 0x000AAE7C File Offset: 0x000A907C
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarTitleForeColorDescr")]
		public Color CalendarTitleForeColor
		{
			get
			{
				return this.calendarTitleForeColor;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
					{
						"value"
					}));
				}
				if (!value.Equals(this.calendarTitleForeColor))
				{
					this.calendarTitleForeColor = value;
					this.SetControlColor(3, value);
				}
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000AAED9 File Offset: 0x000A90D9
		// (set) Token: 0x060023F3 RID: 9203 RVA: 0x000AAEE4 File Offset: 0x000A90E4
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarTrailingForeColorDescr")]
		public Color CalendarTrailingForeColor
		{
			get
			{
				return this.calendarTrailingText;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
					{
						"value"
					}));
				}
				if (!value.Equals(this.calendarTrailingText))
				{
					this.calendarTrailingText = value;
					this.SetControlColor(5, value);
				}
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000AAF41 File Offset: 0x000A9141
		// (set) Token: 0x060023F5 RID: 9205 RVA: 0x000AAF4C File Offset: 0x000A914C
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerCalendarMonthBackgroundDescr")]
		public Color CalendarMonthBackground
		{
			get
			{
				return this.calendarMonthBackground;
			}
			set
			{
				if (value.IsEmpty)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
					{
						"value"
					}));
				}
				if (!value.Equals(this.calendarMonthBackground))
				{
					this.calendarMonthBackground = value;
					this.SetControlColor(4, value);
				}
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x000AAFAC File Offset: 0x000A91AC
		// (set) Token: 0x060023F7 RID: 9207 RVA: 0x000AAFF8 File Offset: 0x000A91F8
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Bindable(true)]
		[SRDescription("DateTimePickerCheckedDescr")]
		public bool Checked
		{
			get
			{
				if (this.ShowCheckBox && base.IsHandleCreated)
				{
					NativeMethods.SYSTEMTIME lParam = new NativeMethods.SYSTEMTIME();
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4097, 0, lParam);
					return num == 0;
				}
				return this.validTime;
			}
			set
			{
				if (this.Checked != value)
				{
					if (this.ShowCheckBox && base.IsHandleCreated)
					{
						if (value)
						{
							int wParam = 0;
							NativeMethods.SYSTEMTIME lParam = DateTimePicker.DateTimeToSysTime(this.Value);
							UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, wParam, lParam);
						}
						else
						{
							int wParam2 = 1;
							NativeMethods.SYSTEMTIME lParam2 = null;
							UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, wParam2, lParam2);
						}
					}
					this.validTime = value;
				}
			}
		}

		// Token: 0x1400018A RID: 394
		// (add) Token: 0x060023F8 RID: 9208 RVA: 0x000131E8 File Offset: 0x000113E8
		// (remove) Token: 0x060023F9 RID: 9209 RVA: 0x000131F1 File Offset: 0x000113F1
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

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x000AB070 File Offset: 0x000A9270
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysDateTimePick32";
				createParams.Style |= this.style;
				DateTimePickerFormat dateTimePickerFormat = this.format;
				switch (dateTimePickerFormat)
				{
				case DateTimePickerFormat.Long:
					createParams.Style |= 4;
					break;
				case DateTimePickerFormat.Short:
				case (DateTimePickerFormat)3:
					break;
				case DateTimePickerFormat.Time:
					createParams.Style |= 8;
					break;
				default:
					if (dateTimePickerFormat != DateTimePickerFormat.Custom)
					{
					}
					break;
				}
				createParams.ExStyle |= 512;
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x000AB12D File Offset: 0x000A932D
		// (set) Token: 0x060023FC RID: 9212 RVA: 0x000AB138 File Offset: 0x000A9338
		[DefaultValue(null)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatBehavior")]
		[SRDescription("DateTimePickerCustomFormatDescr")]
		public string CustomFormat
		{
			get
			{
				return this.customFormat;
			}
			set
			{
				if ((value != null && !value.Equals(this.customFormat)) || (value == null && this.customFormat != null))
				{
					this.customFormat = value;
					if (base.IsHandleCreated && this.format == DateTimePickerFormat.Custom)
					{
						base.SendMessage(NativeMethods.DTM_SETFORMAT, 0, this.customFormat);
					}
				}
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x000AB18C File Offset: 0x000A938C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, this.PreferredHeight);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x060023FF RID: 9215 RVA: 0x000131DF File Offset: 0x000113DF
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

		// Token: 0x1400018B RID: 395
		// (add) Token: 0x06002400 RID: 9216 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x06002401 RID: 9217 RVA: 0x000238FC File Offset: 0x00021AFC
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

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x000AB19E File Offset: 0x000A939E
		// (set) Token: 0x06002403 RID: 9219 RVA: 0x000AB1AE File Offset: 0x000A93AE
		[DefaultValue(LeftRightAlignment.Left)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("DateTimePickerDropDownAlignDescr")]
		public LeftRightAlignment DropDownAlign
		{
			get
			{
				if ((this.style & 32) == 0)
				{
					return LeftRightAlignment.Left;
				}
				return LeftRightAlignment.Right;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(LeftRightAlignment));
				}
				this.SetStyleBit(value == LeftRightAlignment.Right, 32);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x06002405 RID: 9221 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x1400018C RID: 396
		// (add) Token: 0x06002406 RID: 9222 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06002407 RID: 9223 RVA: 0x0005AAD7 File Offset: 0x00058CD7
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

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x000AB1E2 File Offset: 0x000A93E2
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x000AB1EC File Offset: 0x000A93EC
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("DateTimePickerFormatDescr")]
		public DateTimePickerFormat Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 1, 8, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DateTimePickerFormat));
				}
				if (this.format != value)
				{
					this.format = value;
					base.RecreateHandle();
					this.OnFormatChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1400018D RID: 397
		// (add) Token: 0x0600240A RID: 9226 RVA: 0x000AB241 File Offset: 0x000A9441
		// (remove) Token: 0x0600240B RID: 9227 RVA: 0x000AB254 File Offset: 0x000A9454
		[SRCategory("CatPropertyChanged")]
		[SRDescription("DateTimePickerOnFormatChangedDescr")]
		public event EventHandler FormatChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.EVENT_FORMATCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.EVENT_FORMATCHANGED, value);
			}
		}

		// Token: 0x1400018E RID: 398
		// (add) Token: 0x0600240C RID: 9228 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x0600240D RID: 9229 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x0600240E RID: 9230 RVA: 0x000AB268 File Offset: 0x000A9468
		internal static DateTime EffectiveMinDate(DateTime minDate)
		{
			DateTime minimumDateTime = DateTimePicker.MinimumDateTime;
			if (minDate < minimumDateTime)
			{
				return minimumDateTime;
			}
			return minDate;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000AB288 File Offset: 0x000A9488
		internal static DateTime EffectiveMaxDate(DateTime maxDate)
		{
			DateTime maximumDateTime = DateTimePicker.MaximumDateTime;
			if (maxDate > maximumDateTime)
			{
				return maximumDateTime;
			}
			return maxDate;
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x000AB2A7 File Offset: 0x000A94A7
		// (set) Token: 0x06002411 RID: 9233 RVA: 0x000AB2B4 File Offset: 0x000A94B4
		[SRCategory("CatBehavior")]
		[SRDescription("DateTimePickerMaxDateDescr")]
		public DateTime MaxDate
		{
			get
			{
				return DateTimePicker.EffectiveMaxDate(this.max);
			}
			set
			{
				if (value != this.max)
				{
					if (value < DateTimePicker.EffectiveMinDate(this.min))
					{
						throw new ArgumentOutOfRangeException("MaxDate", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"MaxDate",
							DateTimePicker.FormatDateTime(value),
							"MinDate"
						}));
					}
					if (value > DateTimePicker.MaximumDateTime)
					{
						throw new ArgumentOutOfRangeException("MaxDate", SR.GetString("DateTimePickerMaxDate", new object[]
						{
							DateTimePicker.FormatDateTime(DateTimePicker.MaxDateTime)
						}));
					}
					this.max = value;
					this.SetRange();
					if (this.Value > this.max)
					{
						this.Value = this.max;
					}
				}
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x000AB37C File Offset: 0x000A957C
		public static DateTime MaximumDateTime
		{
			get
			{
				DateTime maxSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MaxSupportedDateTime;
				if (maxSupportedDateTime.Year > DateTimePicker.MaxDateTime.Year)
				{
					return DateTimePicker.MaxDateTime;
				}
				return maxSupportedDateTime;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x000AB3B6 File Offset: 0x000A95B6
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x000AB3C4 File Offset: 0x000A95C4
		[SRCategory("CatBehavior")]
		[SRDescription("DateTimePickerMinDateDescr")]
		public DateTime MinDate
		{
			get
			{
				return DateTimePicker.EffectiveMinDate(this.min);
			}
			set
			{
				if (value != this.min)
				{
					if (value > DateTimePicker.EffectiveMaxDate(this.max))
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("InvalidHighBoundArgument", new object[]
						{
							"MinDate",
							DateTimePicker.FormatDateTime(value),
							"MaxDate"
						}));
					}
					if (value < DateTimePicker.MinimumDateTime)
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("DateTimePickerMinDate", new object[]
						{
							DateTimePicker.FormatDateTime(DateTimePicker.MinimumDateTime)
						}));
					}
					this.min = value;
					this.SetRange();
					if (this.Value < this.min)
					{
						this.Value = this.min;
					}
				}
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x000AB48C File Offset: 0x000A968C
		public static DateTime MinimumDateTime
		{
			get
			{
				DateTime minSupportedDateTime = CultureInfo.CurrentCulture.Calendar.MinSupportedDateTime;
				if (minSupportedDateTime.Year < 1753)
				{
					return new DateTime(1753, 1, 1);
				}
				return minSupportedDateTime;
			}
		}

		// Token: 0x1400018F RID: 399
		// (add) Token: 0x06002416 RID: 9238 RVA: 0x000131FA File Offset: 0x000113FA
		// (remove) Token: 0x06002417 RID: 9239 RVA: 0x00013203 File Offset: 0x00011403
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

		// Token: 0x14000190 RID: 400
		// (add) Token: 0x06002418 RID: 9240 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x06002419 RID: 9241 RVA: 0x0002390E File Offset: 0x00021B0E
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

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x0600241B RID: 9243 RVA: 0x0001365E File Offset: 0x0001185E
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

		// Token: 0x14000191 RID: 401
		// (add) Token: 0x0600241C RID: 9244 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x0600241D RID: 9245 RVA: 0x00013670 File Offset: 0x00011870
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

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x000AB4C8 File Offset: 0x000A96C8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int PreferredHeight
		{
			get
			{
				if (this.prefHeightCache > -1)
				{
					return (int)this.prefHeightCache;
				}
				int num = base.FontHeight;
				num += SystemInformation.BorderSize.Height * 4 + 3;
				this.prefHeightCache = (short)num;
				return num;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x000AB509 File Offset: 0x000A9709
		// (set) Token: 0x06002420 RID: 9248 RVA: 0x000AB514 File Offset: 0x000A9714
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

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x000AB568 File Offset: 0x000A9768
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x000AB575 File Offset: 0x000A9775
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerShowNoneDescr")]
		public bool ShowCheckBox
		{
			get
			{
				return (this.style & 2) != 0;
			}
			set
			{
				this.SetStyleBit(value, 2);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x000AB57F File Offset: 0x000A977F
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x000AB58C File Offset: 0x000A978C
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("DateTimePickerShowUpDownDescr")]
		public bool ShowUpDown
		{
			get
			{
				return (this.style & 1) != 0;
			}
			set
			{
				if (this.ShowUpDown != value)
				{
					this.SetStyleBit(value, 1);
				}
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06002426 RID: 9254 RVA: 0x000AB59F File Offset: 0x000A979F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.ResetValue();
					return;
				}
				this.Value = DateTime.Parse(value, CultureInfo.CurrentCulture);
			}
		}

		// Token: 0x14000192 RID: 402
		// (add) Token: 0x06002427 RID: 9255 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06002428 RID: 9256 RVA: 0x0004677A File Offset: 0x0004497A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000AB5C4 File Offset: 0x000A97C4
		// (set) Token: 0x0600242A RID: 9258 RVA: 0x000AB5E4 File Offset: 0x000A97E4
		[SRCategory("CatBehavior")]
		[Bindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("DateTimePickerValueDescr")]
		public DateTime Value
		{
			get
			{
				if (!this.userHasSetValue && this.validTime)
				{
					return this.creationTime;
				}
				return this.value;
			}
			set
			{
				bool flag = !DateTime.Equals(this.Value, value);
				if (!this.userHasSetValue || flag)
				{
					if (value < this.MinDate || value > this.MaxDate)
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							DateTimePicker.FormatDateTime(value),
							"'MinDate'",
							"'MaxDate'"
						}));
					}
					string text = this.Text;
					this.value = value;
					this.userHasSetValue = true;
					if (base.IsHandleCreated)
					{
						int wParam = 0;
						NativeMethods.SYSTEMTIME lParam = DateTimePicker.DateTimeToSysTime(value);
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, wParam, lParam);
					}
					if (flag)
					{
						this.OnValueChanged(EventArgs.Empty);
					}
					if (!text.Equals(this.Text))
					{
						this.OnTextChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x14000193 RID: 403
		// (add) Token: 0x0600242B RID: 9259 RVA: 0x000AB6CF File Offset: 0x000A98CF
		// (remove) Token: 0x0600242C RID: 9260 RVA: 0x000AB6E8 File Offset: 0x000A98E8
		[SRCategory("CatAction")]
		[SRDescription("DateTimePickerOnCloseUpDescr")]
		public event EventHandler CloseUp
		{
			add
			{
				this.onCloseUp = (EventHandler)Delegate.Combine(this.onCloseUp, value);
			}
			remove
			{
				this.onCloseUp = (EventHandler)Delegate.Remove(this.onCloseUp, value);
			}
		}

		// Token: 0x14000194 RID: 404
		// (add) Token: 0x0600242D RID: 9261 RVA: 0x000AB701 File Offset: 0x000A9901
		// (remove) Token: 0x0600242E RID: 9262 RVA: 0x000AB71A File Offset: 0x000A991A
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

		// Token: 0x14000195 RID: 405
		// (add) Token: 0x0600242F RID: 9263 RVA: 0x000AB733 File Offset: 0x000A9933
		// (remove) Token: 0x06002430 RID: 9264 RVA: 0x000AB74C File Offset: 0x000A994C
		[SRCategory("CatAction")]
		[SRDescription("valueChangedEventDescr")]
		public event EventHandler ValueChanged
		{
			add
			{
				this.onValueChanged = (EventHandler)Delegate.Combine(this.onValueChanged, value);
			}
			remove
			{
				this.onValueChanged = (EventHandler)Delegate.Remove(this.onValueChanged, value);
			}
		}

		// Token: 0x14000196 RID: 406
		// (add) Token: 0x06002431 RID: 9265 RVA: 0x000AB765 File Offset: 0x000A9965
		// (remove) Token: 0x06002432 RID: 9266 RVA: 0x000AB77E File Offset: 0x000A997E
		[SRCategory("CatAction")]
		[SRDescription("DateTimePickerOnDropDownDescr")]
		public event EventHandler DropDown
		{
			add
			{
				this.onDropDown = (EventHandler)Delegate.Combine(this.onDropDown, value);
			}
			remove
			{
				this.onDropDown = (EventHandler)Delegate.Remove(this.onDropDown, value);
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000AB797 File Offset: 0x000A9997
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new DateTimePicker.DateTimePickerAccessibleObject(this);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000AB7A0 File Offset: 0x000A99A0
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 256
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			this.creationTime = DateTime.Now;
			base.CreateHandle();
			if (this.userHasSetValue && this.validTime)
			{
				int wParam = 0;
				NativeMethods.SYSTEMTIME lParam = DateTimePicker.DateTimeToSysTime(this.Value);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, wParam, lParam);
			}
			else if (!this.validTime)
			{
				int wParam2 = 1;
				NativeMethods.SYSTEMTIME lParam2 = null;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, wParam2, lParam2);
			}
			if (this.format == DateTimePickerFormat.Custom)
			{
				base.SendMessage(NativeMethods.DTM_SETFORMAT, 0, this.customFormat);
			}
			this.UpdateUpDown();
			this.SetAllControlColors();
			this.SetControlCalendarFont();
			this.SetRange();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000AB894 File Offset: 0x000A9A94
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override void DestroyHandle()
		{
			this.value = this.Value;
			base.DestroyHandle();
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000AB8A8 File Offset: 0x000A9AA8
		private static string FormatDateTime(DateTime value)
		{
			return value.ToString("G", CultureInfo.CurrentCulture);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000AB8BB File Offset: 0x000A9ABB
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			return base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, this.PreferredHeight);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000AB8CC File Offset: 0x000A9ACC
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = this.PreferredHeight;
			int width = CommonProperties.GetSpecifiedBounds(this).Width;
			return new Size(width, preferredHeight);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000AB8F8 File Offset: 0x000A9AF8
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000AB92D File Offset: 0x000A9B2D
		protected virtual void OnCloseUp(EventArgs eventargs)
		{
			if (this.onCloseUp != null)
			{
				this.onCloseUp(this, eventargs);
			}
			this._expandCollapseState = UnsafeNativeMethods.ExpandCollapseState.Collapsed;
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20005);
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000AB96B File Offset: 0x000A9B6B
		protected virtual void OnDropDown(EventArgs eventargs)
		{
			if (this.onDropDown != null)
			{
				this.onDropDown(this, eventargs);
			}
			this._expandCollapseState = UnsafeNativeMethods.ExpandCollapseState.Expanded;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000AB98C File Offset: 0x000A9B8C
		protected virtual void OnFormatChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[DateTimePicker.EVENT_FORMATCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000AB9BA File Offset: 0x000A9BBA
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			SystemEvents.UserPreferenceChanged += this.MarshaledUserPreferenceChanged;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000AB9D4 File Offset: 0x000A9BD4
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.MarshaledUserPreferenceChanged;
			base.OnHandleDestroyed(e);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000AB9EE File Offset: 0x000A9BEE
		protected virtual void OnValueChanged(EventArgs eventargs)
		{
			if (this.onValueChanged != null)
			{
				this.onValueChanged(this, eventargs);
			}
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000ABA05 File Offset: 0x000A9C05
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

		// Token: 0x06002441 RID: 9281 RVA: 0x000ABA34 File Offset: 0x000A9C34
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.prefHeightCache = -1;
			base.Height = this.PreferredHeight;
			if (this.calendarFont == null)
			{
				this.calendarFontHandleWrapper = null;
				this.SetControlCalendarFont();
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000ABA65 File Offset: 0x000A9C65
		private void ResetCalendarForeColor()
		{
			this.CalendarForeColor = Control.DefaultForeColor;
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000ABA72 File Offset: 0x000A9C72
		private void ResetCalendarFont()
		{
			this.CalendarFont = null;
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000ABA7B File Offset: 0x000A9C7B
		private void ResetCalendarMonthBackground()
		{
			this.CalendarMonthBackground = DateTimePicker.DefaultMonthBackColor;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000ABA88 File Offset: 0x000A9C88
		private void ResetCalendarTitleBackColor()
		{
			this.CalendarTitleBackColor = DateTimePicker.DefaultTitleBackColor;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000ABA95 File Offset: 0x000A9C95
		private void ResetCalendarTitleForeColor()
		{
			this.CalendarTitleBackColor = Control.DefaultForeColor;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000ABAA2 File Offset: 0x000A9CA2
		private void ResetCalendarTrailingForeColor()
		{
			this.CalendarTrailingForeColor = DateTimePicker.DefaultTrailingForeColor;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000ABAAF File Offset: 0x000A9CAF
		private void ResetFormat()
		{
			this.Format = DateTimePickerFormat.Long;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000ABAB8 File Offset: 0x000A9CB8
		private void ResetMaxDate()
		{
			this.MaxDate = DateTimePicker.MaximumDateTime;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000ABAC5 File Offset: 0x000A9CC5
		private void ResetMinDate()
		{
			this.MinDate = DateTimePicker.MinimumDateTime;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000ABAD4 File Offset: 0x000A9CD4
		private void ResetValue()
		{
			this.value = DateTime.Now;
			this.userHasSetValue = false;
			if (base.IsHandleCreated)
			{
				int wParam = 0;
				NativeMethods.SYSTEMTIME lParam = DateTimePicker.DateTimeToSysTime(this.value);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4098, wParam, lParam);
			}
			this.Checked = false;
			this.OnValueChanged(EventArgs.Empty);
			this.OnTextChanged(EventArgs.Empty);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000ABB3F File Offset: 0x000A9D3F
		private void SetControlColor(int colorIndex, Color value)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4102, colorIndex, ColorTranslator.ToWin32(value));
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000ABB5C File Offset: 0x000A9D5C
		private void SetControlCalendarFont()
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4105, this.CalendarFontHandle, NativeMethods.InvalidIntPtr);
			}
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000ABB80 File Offset: 0x000A9D80
		private void SetAllControlColors()
		{
			this.SetControlColor(4, this.calendarMonthBackground);
			this.SetControlColor(1, this.calendarForeColor);
			this.SetControlColor(2, this.calendarTitleBackColor);
			this.SetControlColor(3, this.calendarTitleForeColor);
			this.SetControlColor(5, this.calendarTrailingText);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000ABBCE File Offset: 0x000A9DCE
		private void SetRange()
		{
			this.SetRange(DateTimePicker.EffectiveMinDate(this.min), DateTimePicker.EffectiveMaxDate(this.max));
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000ABBEC File Offset: 0x000A9DEC
		private void SetRange(DateTime min, DateTime max)
		{
			if (base.IsHandleCreated)
			{
				int num = 0;
				NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
				num |= 3;
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(min);
				systemtimearray.wYear1 = systemtime.wYear;
				systemtimearray.wMonth1 = systemtime.wMonth;
				systemtimearray.wDayOfWeek1 = systemtime.wDayOfWeek;
				systemtimearray.wDay1 = systemtime.wDay;
				systemtimearray.wHour1 = systemtime.wHour;
				systemtimearray.wMinute1 = systemtime.wMinute;
				systemtimearray.wSecond1 = systemtime.wSecond;
				systemtimearray.wMilliseconds1 = systemtime.wMilliseconds;
				systemtime = DateTimePicker.DateTimeToSysTime(max);
				systemtimearray.wYear2 = systemtime.wYear;
				systemtimearray.wMonth2 = systemtime.wMonth;
				systemtimearray.wDayOfWeek2 = systemtime.wDayOfWeek;
				systemtimearray.wDay2 = systemtime.wDay;
				systemtimearray.wHour2 = systemtime.wHour;
				systemtimearray.wMinute2 = systemtime.wMinute;
				systemtimearray.wSecond2 = systemtime.wSecond;
				systemtimearray.wMilliseconds2 = systemtime.wMilliseconds;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4100, num, systemtimearray);
			}
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000ABCF8 File Offset: 0x000A9EF8
		private void SetStyleBit(bool flag, int bit)
		{
			if ((this.style & bit) != 0 == flag)
			{
				return;
			}
			if (flag)
			{
				this.style |= bit;
			}
			else
			{
				this.style &= ~bit;
			}
			if (base.IsHandleCreated)
			{
				base.RecreateHandle();
				base.Invalidate();
				base.Update();
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000ABD50 File Offset: 0x000A9F50
		private bool ShouldSerializeCalendarForeColor()
		{
			return !this.CalendarForeColor.Equals(Control.DefaultForeColor);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000ABD7E File Offset: 0x000A9F7E
		private bool ShouldSerializeCalendarFont()
		{
			return this.calendarFont != null;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000ABD89 File Offset: 0x000A9F89
		private bool ShouldSerializeCalendarTitleBackColor()
		{
			return !this.calendarTitleBackColor.Equals(DateTimePicker.DefaultTitleBackColor);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000ABDA9 File Offset: 0x000A9FA9
		private bool ShouldSerializeCalendarTitleForeColor()
		{
			return !this.calendarTitleForeColor.Equals(DateTimePicker.DefaultTitleForeColor);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000ABDC9 File Offset: 0x000A9FC9
		private bool ShouldSerializeCalendarTrailingForeColor()
		{
			return !this.calendarTrailingText.Equals(DateTimePicker.DefaultTrailingForeColor);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000ABDE9 File Offset: 0x000A9FE9
		private bool ShouldSerializeCalendarMonthBackground()
		{
			return !this.calendarMonthBackground.Equals(DateTimePicker.DefaultMonthBackColor);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000ABE09 File Offset: 0x000AA009
		private bool ShouldSerializeMaxDate()
		{
			return this.max != DateTimePicker.MaximumDateTime && this.max != DateTime.MaxValue;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000ABE2F File Offset: 0x000AA02F
		private bool ShouldSerializeMinDate()
		{
			return this.min != DateTimePicker.MinimumDateTime && this.min != DateTime.MinValue;
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000ABE55 File Offset: 0x000AA055
		private bool ShouldSerializeValue()
		{
			return this.userHasSetValue;
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000ABE5D File Offset: 0x000AA05D
		private bool ShouldSerializeFormat()
		{
			return this.Format != DateTimePickerFormat.Long;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000ABE6C File Offset: 0x000AA06C
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Value: " + DateTimePicker.FormatDateTime(this.Value);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000ABE98 File Offset: 0x000AA098
		private void UpdateUpDown()
		{
			if (this.ShowUpDown)
			{
				DateTimePicker.EnumChildren enumChildren = new DateTimePicker.EnumChildren();
				NativeMethods.EnumChildrenCallback lpEnumFunc = new NativeMethods.EnumChildrenCallback(enumChildren.enumChildren);
				UnsafeNativeMethods.EnumChildWindows(new HandleRef(this, base.Handle), lpEnumFunc, NativeMethods.NullHandleRef);
				if (enumChildren.hwndFound != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(enumChildren, enumChildren.hwndFound), null, true);
					SafeNativeMethods.UpdateWindow(new HandleRef(enumChildren, enumChildren.hwndFound));
				}
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000ABF10 File Offset: 0x000AA110
		private void MarshaledUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			try
			{
				base.BeginInvoke(new UserPreferenceChangedEventHandler(this.UserPreferenceChanged), new object[]
				{
					sender,
					pref
				});
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000ABF54 File Offset: 0x000AA154
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Locale)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000ABF66 File Offset: 0x000AA166
		private void WmCloseUp(ref Message m)
		{
			this.OnCloseUp(EventArgs.Empty);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000ABF74 File Offset: 0x000AA174
		private void WmDateTimeChange(ref Message m)
		{
			NativeMethods.NMDATETIMECHANGE nmdatetimechange = (NativeMethods.NMDATETIMECHANGE)m.GetLParam(typeof(NativeMethods.NMDATETIMECHANGE));
			DateTime d = this.value;
			bool flag = this.validTime;
			if (nmdatetimechange.dwFlags != 1)
			{
				this.validTime = true;
				this.value = DateTimePicker.SysTimeToDateTime(nmdatetimechange.st);
				this.userHasSetValue = true;
			}
			else
			{
				this.validTime = false;
			}
			if (this.value != d || flag != this.validTime)
			{
				this.OnValueChanged(EventArgs.Empty);
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000AC004 File Offset: 0x000AA204
		private void WmDropDown(ref Message m)
		{
			if (this.RightToLeftLayout && this.RightToLeft == RightToLeft.Yes)
			{
				IntPtr intPtr = base.SendMessage(4104, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					int num = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, intPtr), -20));
					num |= 5242880;
					num &= -12289;
					UnsafeNativeMethods.SetWindowLong(new HandleRef(this, intPtr), -20, new HandleRef(this, (IntPtr)num));
				}
			}
			this.OnDropDown(EventArgs.Empty);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000AC088 File Offset: 0x000AA288
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			this.SetAllControlColors();
			base.OnSystemColorsChanged(e);
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000AC098 File Offset: 0x000AA298
		private void WmReflectCommand(ref Message m)
		{
			if (m.HWnd == base.Handle)
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				int code = nmhdr.code;
				if (code == -759)
				{
					this.WmDateTimeChange(ref m);
					return;
				}
				if (code != -754)
				{
					if (code == -753)
					{
						this.WmCloseUp(ref m);
						return;
					}
				}
				else
				{
					this.WmDropDown(ref m);
				}
			}
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000AC104 File Offset: 0x000AA304
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 71)
			{
				if (msg != 513)
				{
					if (msg == 8270)
					{
						this.WmReflectCommand(ref m);
						base.WndProc(ref m);
						return;
					}
					base.WndProc(ref m);
				}
				else
				{
					this.FocusInternal();
					if (!base.ValidationCancelled)
					{
						base.WndProc(ref m);
						return;
					}
				}
				return;
			}
			base.WndProc(ref m);
			this.UpdateUpDown();
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000AC16C File Offset: 0x000AA36C
		internal static NativeMethods.SYSTEMTIME DateTimeToSysTime(DateTime time)
		{
			return new NativeMethods.SYSTEMTIME
			{
				wYear = (short)time.Year,
				wMonth = (short)time.Month,
				wDayOfWeek = (short)time.DayOfWeek,
				wDay = (short)time.Day,
				wHour = (short)time.Hour,
				wMinute = (short)time.Minute,
				wSecond = (short)time.Second,
				wMilliseconds = 0
			};
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000AC1E9 File Offset: 0x000AA3E9
		internal static DateTime SysTimeToDateTime(NativeMethods.SYSTEMTIME s)
		{
			return new DateTime((int)s.wYear, (int)s.wMonth, (int)s.wDay, (int)s.wHour, (int)s.wMinute, (int)s.wSecond);
		}

		// Token: 0x04000EBD RID: 3773
		protected static readonly Color DefaultTitleBackColor = SystemColors.ActiveCaption;

		// Token: 0x04000EBE RID: 3774
		protected static readonly Color DefaultTitleForeColor = SystemColors.ActiveCaptionText;

		// Token: 0x04000EBF RID: 3775
		protected static readonly Color DefaultMonthBackColor = SystemColors.Window;

		// Token: 0x04000EC0 RID: 3776
		protected static readonly Color DefaultTrailingForeColor = SystemColors.GrayText;

		// Token: 0x04000EC1 RID: 3777
		private static readonly object EVENT_FORMATCHANGED = new object();

		// Token: 0x04000EC2 RID: 3778
		private static readonly string DateTimePickerLocalizedControlTypeString = SR.GetString("DateTimePickerLocalizedControlType");

		// Token: 0x04000EC3 RID: 3779
		private const int TIMEFORMAT_NOUPDOWN = 8;

		// Token: 0x04000EC4 RID: 3780
		private EventHandler onCloseUp;

		// Token: 0x04000EC5 RID: 3781
		private EventHandler onDropDown;

		// Token: 0x04000EC6 RID: 3782
		private EventHandler onValueChanged;

		// Token: 0x04000EC7 RID: 3783
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04000EC8 RID: 3784
		private UnsafeNativeMethods.ExpandCollapseState _expandCollapseState;

		// Token: 0x04000EC9 RID: 3785
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static readonly DateTime MinDateTime = new DateTime(1753, 1, 1);

		// Token: 0x04000ECA RID: 3786
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static readonly DateTime MaxDateTime = new DateTime(9998, 12, 31);

		// Token: 0x04000ECB RID: 3787
		private int style;

		// Token: 0x04000ECC RID: 3788
		private short prefHeightCache = -1;

		// Token: 0x04000ECD RID: 3789
		private bool validTime = true;

		// Token: 0x04000ECE RID: 3790
		private bool userHasSetValue;

		// Token: 0x04000ECF RID: 3791
		private DateTime value = DateTime.Now;

		// Token: 0x04000ED0 RID: 3792
		private DateTime creationTime = DateTime.Now;

		// Token: 0x04000ED1 RID: 3793
		private DateTime max = DateTime.MaxValue;

		// Token: 0x04000ED2 RID: 3794
		private DateTime min = DateTime.MinValue;

		// Token: 0x04000ED3 RID: 3795
		private Color calendarForeColor = Control.DefaultForeColor;

		// Token: 0x04000ED4 RID: 3796
		private Color calendarTitleBackColor = DateTimePicker.DefaultTitleBackColor;

		// Token: 0x04000ED5 RID: 3797
		private Color calendarTitleForeColor = DateTimePicker.DefaultTitleForeColor;

		// Token: 0x04000ED6 RID: 3798
		private Color calendarMonthBackground = DateTimePicker.DefaultMonthBackColor;

		// Token: 0x04000ED7 RID: 3799
		private Color calendarTrailingText = DateTimePicker.DefaultTrailingForeColor;

		// Token: 0x04000ED8 RID: 3800
		private Font calendarFont;

		// Token: 0x04000ED9 RID: 3801
		private Control.FontHandleWrapper calendarFontHandleWrapper;

		// Token: 0x04000EDA RID: 3802
		private string customFormat;

		// Token: 0x04000EDB RID: 3803
		private DateTimePickerFormat format;

		// Token: 0x04000EDC RID: 3804
		private bool rightToLeftLayout;

		// Token: 0x02000682 RID: 1666
		private sealed class EnumChildren
		{
			// Token: 0x06006714 RID: 26388 RVA: 0x001820DC File Offset: 0x001802DC
			public bool enumChildren(IntPtr hwnd, IntPtr lparam)
			{
				this.hwndFound = hwnd;
				return true;
			}

			// Token: 0x04003A8B RID: 14987
			public IntPtr hwndFound = IntPtr.Zero;
		}

		// Token: 0x02000683 RID: 1667
		[ComVisible(true)]
		public class DateTimePickerAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006716 RID: 26390 RVA: 0x0009B963 File Offset: 0x00099B63
			public DateTimePickerAccessibleObject(DateTimePicker owner) : base(owner)
			{
			}

			// Token: 0x1700166D RID: 5741
			// (get) Token: 0x06006717 RID: 26391 RVA: 0x001820FC File Offset: 0x001802FC
			public override string KeyboardShortcut
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return string.Empty;
					}
					Label previousLabel = base.PreviousLabel;
					if (previousLabel != null)
					{
						char mnemonic = WindowsFormsUtils.GetMnemonic(previousLabel.Text, false);
						if (mnemonic != '\0')
						{
							return "Alt+" + mnemonic.ToString();
						}
					}
					string keyboardShortcut = base.KeyboardShortcut;
					if (keyboardShortcut == null || keyboardShortcut.Length == 0)
					{
						char mnemonic2 = WindowsFormsUtils.GetMnemonic(base.Owner.Text, false);
						if (mnemonic2 != '\0')
						{
							return "Alt+" + mnemonic2.ToString();
						}
					}
					return keyboardShortcut;
				}
			}

			// Token: 0x1700166E RID: 5742
			// (get) Token: 0x06006718 RID: 26392 RVA: 0x00182180 File Offset: 0x00180380
			public override string Value
			{
				[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return string.Empty;
					}
					string value = base.Value;
					if (value == null || value.Length == 0)
					{
						return base.Owner.Text;
					}
					return value;
				}
			}

			// Token: 0x1700166F RID: 5743
			// (get) Token: 0x06006719 RID: 26393 RVA: 0x001821BC File Offset: 0x001803BC
			public override AccessibleStates State
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = base.State;
					if (((DateTimePicker)base.Owner).ShowCheckBox && ((DateTimePicker)base.Owner).Checked)
					{
						accessibleStates |= AccessibleStates.Checked;
					}
					return accessibleStates;
				}
			}

			// Token: 0x17001670 RID: 5744
			// (get) Token: 0x0600671A RID: 26394 RVA: 0x00182204 File Offset: 0x00180404
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.ComboBox;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					if (!AccessibilityImprovements.Level3)
					{
						return AccessibleRole.DropList;
					}
					return AccessibleRole.ComboBox;
				}
			}

			// Token: 0x0600671B RID: 26395 RVA: 0x0009B96C File Offset: 0x00099B6C
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed() && (AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported());
			}

			// Token: 0x0600671C RID: 26396 RVA: 0x0018223C File Offset: 0x0018043C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30004)
				{
					return DateTimePicker.DateTimePickerLocalizedControlTypeString;
				}
				if (propertyID == 30028)
				{
					return this.IsPatternSupported(10005);
				}
				if (propertyID == 30041)
				{
					return this.IsPatternSupported(10015);
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x0600671D RID: 26397 RVA: 0x00182290 File Offset: 0x00180490
			internal override bool IsPatternSupported(int patternId)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return false;
				}
				if (patternId == 10005)
				{
					return AccessibilityImprovements.Level5;
				}
				if (patternId == 10015)
				{
					return ((DateTimePicker)base.Owner).ShowCheckBox;
				}
				return base.IsPatternSupported(patternId);
			}

			// Token: 0x17001671 RID: 5745
			// (get) Token: 0x0600671E RID: 26398 RVA: 0x001822CA File Offset: 0x001804CA
			internal override UnsafeNativeMethods.ToggleState ToggleState
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return UnsafeNativeMethods.ToggleState.ToggleState_Off;
					}
					if (!((DateTimePicker)base.Owner).Checked)
					{
						return UnsafeNativeMethods.ToggleState.ToggleState_Off;
					}
					return UnsafeNativeMethods.ToggleState.ToggleState_On;
				}
			}

			// Token: 0x0600671F RID: 26399 RVA: 0x001822EB File Offset: 0x001804EB
			internal override void Toggle()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				((DateTimePicker)base.Owner).Checked = !((DateTimePicker)base.Owner).Checked;
			}

			// Token: 0x06006720 RID: 26400 RVA: 0x00182319 File Offset: 0x00180519
			internal override void Collapse()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				if (base.Owner.IsHandleCreated && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Expanded)
				{
					base.Owner.SendMessage(4109, 0, 0);
				}
			}

			// Token: 0x06006721 RID: 26401 RVA: 0x0018234D File Offset: 0x0018054D
			internal override void Expand()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				if (base.Owner.IsHandleCreated && this.ExpandCollapseState == UnsafeNativeMethods.ExpandCollapseState.Collapsed)
				{
					base.Owner.SendMessage(260, (IntPtr)40, 0);
				}
			}

			// Token: 0x17001672 RID: 5746
			// (get) Token: 0x06006722 RID: 26402 RVA: 0x00182386 File Offset: 0x00180586
			internal override UnsafeNativeMethods.ExpandCollapseState ExpandCollapseState
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return UnsafeNativeMethods.ExpandCollapseState.Collapsed;
					}
					return ((DateTimePicker)base.Owner)._expandCollapseState;
				}
			}
		}
	}
}
