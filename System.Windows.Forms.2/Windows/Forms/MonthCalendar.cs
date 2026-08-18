using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	// Token: 0x02000301 RID: 769
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("SelectionRange")]
	[DefaultEvent("DateChanged")]
	[DefaultBindingProperty("SelectionRange")]
	[Designer("System.Windows.Forms.Design.MonthCalendarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionMonthCalendar")]
	public class MonthCalendar : Control
	{
		// Token: 0x06003075 RID: 12405 RVA: 0x000DAE68 File Offset: 0x000D9068
		public MonthCalendar()
		{
			this.PrepareForDrawing();
			this.selectionStart = this.todayDate;
			this.selectionEnd = this.todayDate;
			this._focusedDate = this.todayDate;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.TabStop = true;
			if (MonthCalendar.restrictUnmanagedCode == null)
			{
				bool flag = false;
				try
				{
					IntSecurity.UnmanagedCode.Demand();
					MonthCalendar.restrictUnmanagedCode = new bool?(false);
				}
				catch
				{
					flag = true;
				}
				if (flag)
				{
					new RegistryPermission(PermissionState.Unrestricted).Assert();
					try
					{
						RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework");
						if (registryKey != null)
						{
							object value = registryKey.GetValue("AllowWindowsFormsReentrantDestroy");
							if (value != null && value is int && (int)value == 1)
							{
								MonthCalendar.restrictUnmanagedCode = new bool?(false);
							}
							else
							{
								MonthCalendar.restrictUnmanagedCode = new bool?(true);
							}
						}
						else
						{
							MonthCalendar.restrictUnmanagedCode = new bool?(true);
						}
					}
					catch
					{
						MonthCalendar.restrictUnmanagedCode = new bool?(true);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x000DB044 File Offset: 0x000D9244
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level5)
			{
				return new MonthCalendar.MonthCalendarAccessibleObjectLevel5(this);
			}
			if (AccessibilityImprovements.Level1)
			{
				return new MonthCalendar.MonthCalendarAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x000DB068 File Offset: 0x000D9268
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.PrepareForDrawing();
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000DB078 File Offset: 0x000D9278
		private void PrepareForDrawing()
		{
			if (DpiHelper.EnableMonthCalendarHighDpiImprovements)
			{
				this.scaledExtraPadding = base.LogicalToDeviceUnits(2);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000DB090 File Offset: 0x000D9290
		// (set) Token: 0x0600307A RID: 12410 RVA: 0x000DB0E0 File Offset: 0x000D92E0
		[Localizable(true)]
		[SRDescription("MonthCalendarAnnuallyBoldedDatesDescr")]
		public DateTime[] AnnuallyBoldedDates
		{
			get
			{
				DateTime[] array = new DateTime[this.annualArrayOfDates.Count];
				for (int i = 0; i < this.annualArrayOfDates.Count; i++)
				{
					array[i] = (DateTime)this.annualArrayOfDates[i];
				}
				return array;
			}
			set
			{
				this.annualArrayOfDates.Clear();
				for (int i = 0; i < 12; i++)
				{
					this.monthsOfYear[i] = 0;
				}
				if (value != null && value.Length != 0)
				{
					for (int j = 0; j < value.Length; j++)
					{
						this.annualArrayOfDates.Add(value[j]);
					}
					for (int k = 0; k < value.Length; k++)
					{
						this.monthsOfYear[value[k].Month - 1] |= 1 << value[k].Day - 1;
					}
				}
				base.RecreateHandle();
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x00027F43 File Offset: 0x00026143
		// (set) Token: 0x0600307C RID: 12412 RVA: 0x00012F98 File Offset: 0x00011198
		[SRDescription("MonthCalendarMonthBackColorDescr")]
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

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x0600307E RID: 12414 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x14000230 RID: 560
		// (add) Token: 0x0600307F RID: 12415 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06003080 RID: 12416 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06003082 RID: 12418 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x14000231 RID: 561
		// (add) Token: 0x06003083 RID: 12419 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06003084 RID: 12420 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06003085 RID: 12421 RVA: 0x000DB180 File Offset: 0x000D9380
		// (set) Token: 0x06003086 RID: 12422 RVA: 0x000DB1D0 File Offset: 0x000D93D0
		[Localizable(true)]
		public DateTime[] BoldedDates
		{
			get
			{
				DateTime[] array = new DateTime[this.arrayOfDates.Count];
				for (int i = 0; i < this.arrayOfDates.Count; i++)
				{
					array[i] = (DateTime)this.arrayOfDates[i];
				}
				return array;
			}
			set
			{
				this.arrayOfDates.Clear();
				if (value != null && value.Length != 0)
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.arrayOfDates.Add(value[i]);
					}
				}
				base.RecreateHandle();
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x000DB21B File Offset: 0x000D941B
		// (set) Token: 0x06003088 RID: 12424 RVA: 0x000DB223 File Offset: 0x000D9423
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("MonthCalendarDimensionsDescr")]
		public Size CalendarDimensions
		{
			get
			{
				return this.dimensions;
			}
			set
			{
				if (!this.dimensions.Equals(value))
				{
					this.SetCalendarDimensions(value.Width, value.Height);
				}
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x000DB254 File Offset: 0x000D9454
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysMonthCal32";
				createParams.Style |= 3;
				if (!this.showToday)
				{
					createParams.Style |= 16;
				}
				if (!this.showTodayCircle)
				{
					createParams.Style |= 8;
				}
				if (this.showWeekNumbers)
				{
					createParams.Style |= 4;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600308A RID: 12426 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x000DB2FA File Offset: 0x000D94FA
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(9);
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000DB303 File Offset: 0x000D9503
		protected override Size DefaultSize
		{
			get
			{
				return this.GetMinReqRect();
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x000131DF File Offset: 0x000113DF
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

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000DB30B File Offset: 0x000D950B
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x000DB314 File Offset: 0x000D9514
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(Day.Default)]
		[SRDescription("MonthCalendarFirstDayOfWeekDescr")]
		public Day FirstDayOfWeek
		{
			get
			{
				return this.firstDayOfWeek;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("FirstDayOfWeek", (int)value, typeof(Day));
				}
				if (value != this.firstDayOfWeek)
				{
					this.firstDayOfWeek = value;
					if (base.IsHandleCreated)
					{
						if (value == Day.Default)
						{
							base.RecreateHandle();
						}
						else
						{
							base.SendMessage(4111, 0, (int)value);
						}
						if (AccessibilityImprovements.Level5)
						{
							this.UpdateDisplayRange();
							this.OnDisplayRangeChanged(EventArgs.Empty);
						}
					}
				}
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06003091 RID: 12433 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x06003092 RID: 12434 RVA: 0x00013238 File Offset: 0x00011438
		[SRDescription("MonthCalendarForeColorDescr")]
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

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x06003094 RID: 12436 RVA: 0x0001A1F5 File Offset: 0x000183F5
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

		// Token: 0x14000232 RID: 562
		// (add) Token: 0x06003095 RID: 12437 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x06003096 RID: 12438 RVA: 0x00024115 File Offset: 0x00022315
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x000DB391 File Offset: 0x000D9591
		// (set) Token: 0x06003098 RID: 12440 RVA: 0x000DB3A0 File Offset: 0x000D95A0
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarMaxDateDescr")]
		public DateTime MaxDate
		{
			get
			{
				return DateTimePicker.EffectiveMaxDate(this.maxDate);
			}
			set
			{
				if (value != this.maxDate)
				{
					if (value < DateTimePicker.EffectiveMinDate(this.minDate))
					{
						throw new ArgumentOutOfRangeException("MaxDate", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"MaxDate",
							MonthCalendar.FormatDate(value),
							"MinDate"
						}));
					}
					this.maxDate = value;
					this.SetRange();
				}
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x000DB40F File Offset: 0x000D960F
		// (set) Token: 0x0600309A RID: 12442 RVA: 0x000DB418 File Offset: 0x000D9618
		[SRCategory("CatBehavior")]
		[DefaultValue(7)]
		[SRDescription("MonthCalendarMaxSelectionCountDescr")]
		public int MaxSelectionCount
		{
			get
			{
				return this.maxSelectionCount;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("MaxSelectionCount", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"MaxSelectionCount",
						value.ToString("D", CultureInfo.CurrentCulture),
						1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value != this.maxSelectionCount)
				{
					if (base.IsHandleCreated && (int)((long)base.SendMessage(4100, value, 0)) == 0)
					{
						throw new ArgumentException(SR.GetString("MonthCalendarMaxSelCount", new object[]
						{
							value.ToString("D", CultureInfo.CurrentCulture)
						}), "MaxSelectionCount");
					}
					this.maxSelectionCount = value;
				}
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000DB4CD File Offset: 0x000D96CD
		// (set) Token: 0x0600309C RID: 12444 RVA: 0x000DB4DC File Offset: 0x000D96DC
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarMinDateDescr")]
		public DateTime MinDate
		{
			get
			{
				return DateTimePicker.EffectiveMinDate(this.minDate);
			}
			set
			{
				if (value != this.minDate)
				{
					if (value > DateTimePicker.EffectiveMaxDate(this.maxDate))
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("InvalidHighBoundArgument", new object[]
						{
							"MinDate",
							MonthCalendar.FormatDate(value),
							"MaxDate"
						}));
					}
					if (value < DateTimePicker.MinimumDateTime)
					{
						throw new ArgumentOutOfRangeException("MinDate", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"MinDate",
							MonthCalendar.FormatDate(value),
							MonthCalendar.FormatDate(DateTimePicker.MinimumDateTime)
						}));
					}
					this.minDate = value;
					this.SetRange();
				}
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x000DB594 File Offset: 0x000D9794
		// (set) Token: 0x0600309E RID: 12446 RVA: 0x000DB5E4 File Offset: 0x000D97E4
		[Localizable(true)]
		[SRDescription("MonthCalendarMonthlyBoldedDatesDescr")]
		public DateTime[] MonthlyBoldedDates
		{
			get
			{
				DateTime[] array = new DateTime[this.monthlyArrayOfDates.Count];
				for (int i = 0; i < this.monthlyArrayOfDates.Count; i++)
				{
					array[i] = (DateTime)this.monthlyArrayOfDates[i];
				}
				return array;
			}
			set
			{
				this.monthlyArrayOfDates.Clear();
				this.datesToBoldMonthly = 0;
				if (value != null && value.Length != 0)
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.monthlyArrayOfDates.Add(value[i]);
					}
					for (int j = 0; j < value.Length; j++)
					{
						this.datesToBoldMonthly |= 1 << value[j].Day - 1;
					}
				}
				base.RecreateHandle();
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000DB664 File Offset: 0x000D9864
		private DateTime Now
		{
			get
			{
				return DateTime.Now.Date;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x0001365E File Offset: 0x0001185E
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

		// Token: 0x14000233 RID: 563
		// (add) Token: 0x060030A2 RID: 12450 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x060030A3 RID: 12451 RVA: 0x00013670 File Offset: 0x00011870
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

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000DB67E File Offset: 0x000D987E
		// (set) Token: 0x060030A5 RID: 12453 RVA: 0x000DB688 File Offset: 0x000D9888
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

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000DB6DC File Offset: 0x000D98DC
		// (set) Token: 0x060030A7 RID: 12455 RVA: 0x000DB6E4 File Offset: 0x000D98E4
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[SRDescription("MonthCalendarScrollChangeDescr")]
		public int ScrollChange
		{
			get
			{
				return this.scrollChange;
			}
			set
			{
				if (this.scrollChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("ScrollChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"ScrollChange",
							value.ToString("D", CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value > 20000)
					{
						throw new ArgumentOutOfRangeException("ScrollChange", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"ScrollChange",
							value.ToString("D", CultureInfo.CurrentCulture),
							20000.ToString("D", CultureInfo.CurrentCulture)
						}));
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4116, value, 0);
					}
					this.scrollChange = value;
				}
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000DB7BF File Offset: 0x000D99BF
		// (set) Token: 0x060030A9 RID: 12457 RVA: 0x000DB7C8 File Offset: 0x000D99C8
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarSelectionEndDescr")]
		public DateTime SelectionEnd
		{
			get
			{
				return this.selectionEnd;
			}
			set
			{
				if (this.selectionEnd != value)
				{
					if (value < this.MinDate)
					{
						throw new ArgumentOutOfRangeException("SelectionEnd", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SelectionEnd",
							MonthCalendar.FormatDate(value),
							"MinDate"
						}));
					}
					if (value > this.MaxDate)
					{
						throw new ArgumentOutOfRangeException("SelectionEnd", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"SelectionEnd",
							MonthCalendar.FormatDate(value),
							"MaxDate"
						}));
					}
					if (this.selectionStart > value)
					{
						this.selectionStart = value;
					}
					if ((value - this.selectionStart).Days >= this.maxSelectionCount)
					{
						this.selectionStart = value.AddDays((double)(1 - this.maxSelectionCount));
					}
					this.SetSelRange(this.selectionStart, value);
				}
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000DB8BE File Offset: 0x000D9ABE
		// (set) Token: 0x060030AB RID: 12459 RVA: 0x000DB8C8 File Offset: 0x000D9AC8
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarSelectionStartDescr")]
		public DateTime SelectionStart
		{
			get
			{
				return this.selectionStart;
			}
			set
			{
				if (this.selectionStart != value)
				{
					if (value < this.minDate)
					{
						throw new ArgumentOutOfRangeException("SelectionStart", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SelectionStart",
							MonthCalendar.FormatDate(value),
							"MinDate"
						}));
					}
					if (value > this.maxDate)
					{
						throw new ArgumentOutOfRangeException("SelectionStart", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"SelectionStart",
							MonthCalendar.FormatDate(value),
							"MaxDate"
						}));
					}
					if (this.selectionEnd < value)
					{
						this.selectionEnd = value;
					}
					if ((this.selectionEnd - value).Days >= this.maxSelectionCount)
					{
						this.selectionEnd = value.AddDays((double)(this.maxSelectionCount - 1));
					}
					this.SetSelRange(value, this.selectionEnd);
				}
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000DB9BE File Offset: 0x000D9BBE
		// (set) Token: 0x060030AD RID: 12461 RVA: 0x000DB9D1 File Offset: 0x000D9BD1
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarSelectionRangeDescr")]
		[Bindable(true)]
		public SelectionRange SelectionRange
		{
			get
			{
				return new SelectionRange(this.SelectionStart, this.SelectionEnd);
			}
			set
			{
				this.SetSelectionRange(value.Start, value.End);
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000DB9E5 File Offset: 0x000D9BE5
		// (set) Token: 0x060030AF RID: 12463 RVA: 0x000DB9ED File Offset: 0x000D9BED
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("MonthCalendarShowTodayDescr")]
		public bool ShowToday
		{
			get
			{
				return this.showToday;
			}
			set
			{
				if (this.showToday != value)
				{
					this.showToday = value;
					base.UpdateStyles();
					this.AdjustSize();
				}
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000DBA0B File Offset: 0x000D9C0B
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x000DBA13 File Offset: 0x000D9C13
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("MonthCalendarShowTodayCircleDescr")]
		public bool ShowTodayCircle
		{
			get
			{
				return this.showTodayCircle;
			}
			set
			{
				if (this.showTodayCircle != value)
				{
					this.showTodayCircle = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000DBA2B File Offset: 0x000D9C2B
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x000DBA33 File Offset: 0x000D9C33
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("MonthCalendarShowWeekNumbersDescr")]
		public bool ShowWeekNumbers
		{
			get
			{
				return this.showWeekNumbers;
			}
			set
			{
				if (this.showWeekNumbers != value)
				{
					this.showWeekNumbers = value;
					base.UpdateStyles();
					this.AdjustSize();
				}
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x000DBA54 File Offset: 0x000D9C54
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarSingleMonthSizeDescr")]
		public Size SingleMonthSize
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				if (!base.IsHandleCreated)
				{
					return MonthCalendar.DefaultSingleMonthSize;
				}
				if ((int)((long)base.SendMessage(4105, 0, ref rect)) == 0)
				{
					throw new InvalidOperationException(SR.GetString("InvalidSingleMonthSize"));
				}
				return new Size(rect.right, rect.bottom);
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000B252B File Offset: 0x000B072B
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x000B2533 File Offset: 0x000B0733
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Localizable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000DBAAE File Offset: 0x000D9CAE
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level5 && !base.DesignMode;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x14000234 RID: 564
		// (add) Token: 0x060030BA RID: 12474 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x060030BB RID: 12475 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000DBAC4 File Offset: 0x000D9CC4
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x000DBB2C File Offset: 0x000D9D2C
		[SRCategory("CatBehavior")]
		[SRDescription("MonthCalendarTodayDateDescr")]
		public DateTime TodayDate
		{
			get
			{
				if (this.todayDateSet)
				{
					return this.todayDate;
				}
				if (base.IsHandleCreated)
				{
					NativeMethods.SYSTEMTIME systemtime = new NativeMethods.SYSTEMTIME();
					int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4109, 0, systemtime);
					return DateTimePicker.SysTimeToDateTime(systemtime).Date;
				}
				return this.Now.Date;
			}
			set
			{
				if (!this.todayDateSet || DateTime.Compare(value, this.todayDate) != 0)
				{
					if (DateTime.Compare(value, this.maxDate) > 0)
					{
						throw new ArgumentOutOfRangeException("TodayDate", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"TodayDate",
							MonthCalendar.FormatDate(value),
							MonthCalendar.FormatDate(this.maxDate)
						}));
					}
					if (DateTime.Compare(value, this.minDate) < 0)
					{
						throw new ArgumentOutOfRangeException("TodayDate", SR.GetString("InvalidLowBoundArgument", new object[]
						{
							"TodayDate",
							MonthCalendar.FormatDate(value),
							MonthCalendar.FormatDate(this.minDate)
						}));
					}
					this.todayDate = value.Date;
					this.todayDateSet = true;
					this.UpdateTodayDate();
				}
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000DBBFE File Offset: 0x000D9DFE
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("MonthCalendarTodayDateSetDescr")]
		public bool TodayDateSet
		{
			get
			{
				return this.todayDateSet;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000DBC06 File Offset: 0x000D9E06
		// (set) Token: 0x060030C0 RID: 12480 RVA: 0x000DBC0E File Offset: 0x000D9E0E
		[SRCategory("CatAppearance")]
		[SRDescription("MonthCalendarTitleBackColorDescr")]
		public Color TitleBackColor
		{
			get
			{
				return this.titleBackColor;
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
				this.titleBackColor = value;
				this.SetControlColor(2, value);
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000DBC46 File Offset: 0x000D9E46
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x000DBC4E File Offset: 0x000D9E4E
		[SRCategory("CatAppearance")]
		[SRDescription("MonthCalendarTitleForeColorDescr")]
		public Color TitleForeColor
		{
			get
			{
				return this.titleForeColor;
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
				this.titleForeColor = value;
				this.SetControlColor(3, value);
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x000DBC86 File Offset: 0x000D9E86
		// (set) Token: 0x060030C4 RID: 12484 RVA: 0x000DBC8E File Offset: 0x000D9E8E
		[SRCategory("CatAppearance")]
		[SRDescription("MonthCalendarTrailingForeColorDescr")]
		public Color TrailingForeColor
		{
			get
			{
				return this.trailingForeColor;
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
				this.trailingForeColor = value;
				this.SetControlColor(5, value);
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000DBCC6 File Offset: 0x000D9EC6
		public void AddAnnuallyBoldedDate(DateTime date)
		{
			this.annualArrayOfDates.Add(date);
			this.monthsOfYear[date.Month - 1] |= 1 << date.Day - 1;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000DBD00 File Offset: 0x000D9F00
		public void AddBoldedDate(DateTime date)
		{
			if (!this.arrayOfDates.Contains(date))
			{
				this.arrayOfDates.Add(date);
			}
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000DBD27 File Offset: 0x000D9F27
		public void AddMonthlyBoldedDate(DateTime date)
		{
			this.monthlyArrayOfDates.Add(date);
			this.datesToBoldMonthly |= 1 << date.Day - 1;
		}

		// Token: 0x14000235 RID: 565
		// (add) Token: 0x060030C8 RID: 12488 RVA: 0x000DBD56 File Offset: 0x000D9F56
		// (remove) Token: 0x060030C9 RID: 12489 RVA: 0x000DBD6F File Offset: 0x000D9F6F
		private event EventHandler CalendarViewChanged
		{
			add
			{
				this._onCalendarViewChanged = (EventHandler)Delegate.Combine(this._onCalendarViewChanged, value);
			}
			remove
			{
				this._onCalendarViewChanged = (EventHandler)Delegate.Remove(this._onCalendarViewChanged, value);
			}
		}

		// Token: 0x14000236 RID: 566
		// (add) Token: 0x060030CA RID: 12490 RVA: 0x000131E8 File Offset: 0x000113E8
		// (remove) Token: 0x060030CB RID: 12491 RVA: 0x000131F1 File Offset: 0x000113F1
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

		// Token: 0x14000237 RID: 567
		// (add) Token: 0x060030CC RID: 12492 RVA: 0x000DBD88 File Offset: 0x000D9F88
		// (remove) Token: 0x060030CD RID: 12493 RVA: 0x000DBDA1 File Offset: 0x000D9FA1
		[SRCategory("CatAction")]
		[SRDescription("MonthCalendarOnDateChangedDescr")]
		public event DateRangeEventHandler DateChanged
		{
			add
			{
				this.onDateChanged = (DateRangeEventHandler)Delegate.Combine(this.onDateChanged, value);
			}
			remove
			{
				this.onDateChanged = (DateRangeEventHandler)Delegate.Remove(this.onDateChanged, value);
			}
		}

		// Token: 0x14000238 RID: 568
		// (add) Token: 0x060030CE RID: 12494 RVA: 0x000DBDBA File Offset: 0x000D9FBA
		// (remove) Token: 0x060030CF RID: 12495 RVA: 0x000DBDD3 File Offset: 0x000D9FD3
		[SRCategory("CatAction")]
		[SRDescription("MonthCalendarOnDateSelectedDescr")]
		public event DateRangeEventHandler DateSelected
		{
			add
			{
				this.onDateSelected = (DateRangeEventHandler)Delegate.Combine(this.onDateSelected, value);
			}
			remove
			{
				this.onDateSelected = (DateRangeEventHandler)Delegate.Remove(this.onDateSelected, value);
			}
		}

		// Token: 0x14000239 RID: 569
		// (add) Token: 0x060030D0 RID: 12496 RVA: 0x000DBDEC File Offset: 0x000D9FEC
		// (remove) Token: 0x060030D1 RID: 12497 RVA: 0x000DBE05 File Offset: 0x000DA005
		private event EventHandler DisplayRangeChanged
		{
			add
			{
				this._onDisplayRangeChanged = (EventHandler)Delegate.Combine(this._onDisplayRangeChanged, value);
			}
			remove
			{
				this._onDisplayRangeChanged = (EventHandler)Delegate.Remove(this._onDisplayRangeChanged, value);
			}
		}

		// Token: 0x1400023A RID: 570
		// (add) Token: 0x060030D2 RID: 12498 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x060030D3 RID: 12499 RVA: 0x000238FC File Offset: 0x00021AFC
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

		// Token: 0x1400023B RID: 571
		// (add) Token: 0x060030D4 RID: 12500 RVA: 0x000131FA File Offset: 0x000113FA
		// (remove) Token: 0x060030D5 RID: 12501 RVA: 0x00013203 File Offset: 0x00011403
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

		// Token: 0x1400023C RID: 572
		// (add) Token: 0x060030D6 RID: 12502 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x060030D7 RID: 12503 RVA: 0x0002390E File Offset: 0x00021B0E
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

		// Token: 0x1400023D RID: 573
		// (add) Token: 0x060030D8 RID: 12504 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x060030D9 RID: 12505 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x1400023E RID: 574
		// (add) Token: 0x060030DA RID: 12506 RVA: 0x000DBE1E File Offset: 0x000DA01E
		// (remove) Token: 0x060030DB RID: 12507 RVA: 0x000DBE37 File Offset: 0x000DA037
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

		// Token: 0x060030DC RID: 12508 RVA: 0x000DBE50 File Offset: 0x000DA050
		private void AdjustSize()
		{
			Size minReqRect = this.GetMinReqRect();
			this.Size = minReqRect;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000DBE6C File Offset: 0x000DA06C
		private void BoldDates(DateBoldEventArgs e)
		{
			int size = e.Size;
			e.DaysToBold = new int[size];
			SelectionRange displayRange = this.GetDisplayRange(false);
			int num = displayRange.Start.Month;
			int year = displayRange.Start.Year;
			int count = this.arrayOfDates.Count;
			for (int i = 0; i < count; i++)
			{
				DateTime t = (DateTime)this.arrayOfDates[i];
				if (DateTime.Compare(t, displayRange.Start) >= 0 && DateTime.Compare(t, displayRange.End) <= 0)
				{
					int month = t.Month;
					int year2 = t.Year;
					int num2 = (year2 == year) ? (month - num) : (month + year2 * 12 - year * 12 - num);
					e.DaysToBold[num2] |= 1 << t.Day - 1;
				}
			}
			num--;
			int j = 0;
			while (j < size)
			{
				e.DaysToBold[j] |= (this.monthsOfYear[num % 12] | this.datesToBoldMonthly);
				j++;
				num++;
			}
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000DBF94 File Offset: 0x000DA194
		private bool CompareDayAndMonth(DateTime t1, DateTime t2)
		{
			return t1.Day == t2.Day && t1.Month == t2.Month;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000DBFB8 File Offset: 0x000DA1B8
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
			base.CreateHandle();
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000DC00C File Offset: 0x000DA20C
		protected override void Dispose(bool disposing)
		{
			if (this.mdsBuffer != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.mdsBuffer);
				this.mdsBuffer = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000DC03D File Offset: 0x000DA23D
		private static string FormatDate(DateTime value)
		{
			return value.ToString("d", CultureInfo.CurrentCulture);
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000DC050 File Offset: 0x000DA250
		public SelectionRange GetDisplayRange(bool visible)
		{
			if (visible)
			{
				return this.GetMonthRange(0);
			}
			return this.GetMonthRange(1);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000DC064 File Offset: 0x000DA264
		private MonthCalendar.HitArea GetHitArea(int hit)
		{
			if (hit <= 196608)
			{
				switch (hit)
				{
				case 65536:
					return MonthCalendar.HitArea.TitleBackground;
				case 65537:
					return MonthCalendar.HitArea.TitleMonth;
				case 65538:
					return MonthCalendar.HitArea.TitleYear;
				default:
					switch (hit)
					{
					case 131072:
						return MonthCalendar.HitArea.CalendarBackground;
					case 131073:
						return MonthCalendar.HitArea.Date;
					case 131074:
						return MonthCalendar.HitArea.DayOfWeek;
					case 131075:
						return MonthCalendar.HitArea.WeekNumbers;
					default:
						if (hit == 196608)
						{
							return MonthCalendar.HitArea.TodayLink;
						}
						break;
					}
					break;
				}
			}
			else if (hit <= 16908289)
			{
				if (hit == 16842755)
				{
					return MonthCalendar.HitArea.NextMonthButton;
				}
				if (hit == 16908289)
				{
					return MonthCalendar.HitArea.NextMonthDate;
				}
			}
			else
			{
				if (hit == 33619971)
				{
					return MonthCalendar.HitArea.PrevMonthButton;
				}
				if (hit == 33685505)
				{
					return MonthCalendar.HitArea.PrevMonthDate;
				}
			}
			return MonthCalendar.HitArea.Nowhere;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000DC100 File Offset: 0x000DA300
		private Size GetMinReqRect()
		{
			return this.GetMinReqRect(0, false, false);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000DC10C File Offset: 0x000DA30C
		private Size GetMinReqRect(int newDimensionLength, bool updateRows, bool updateCols)
		{
			Size singleMonthSize = this.SingleMonthSize;
			Size textExtent;
			using (WindowsFont windowsFont = WindowsFont.FromFont(this.Font))
			{
				textExtent = WindowsGraphicsCacheManager.MeasurementGraphics.GetTextExtent(DateTime.Now.ToShortDateString(), windowsFont);
			}
			int num = textExtent.Height + 4;
			int num2 = singleMonthSize.Height;
			if (this.ShowToday)
			{
				num2 -= num;
			}
			if (updateRows)
			{
				int num3 = (newDimensionLength - num + 6) / (num2 + 6);
				this.dimensions.Height = ((num3 < 1) ? 1 : num3);
			}
			if (updateCols)
			{
				int num4 = (newDimensionLength - this.scaledExtraPadding) / singleMonthSize.Width;
				this.dimensions.Width = ((num4 < 1) ? 1 : num4);
			}
			singleMonthSize.Width = (singleMonthSize.Width + 6) * this.dimensions.Width - 6;
			singleMonthSize.Height = (num2 + 6) * this.dimensions.Height - 6 + num;
			if (base.IsHandleCreated)
			{
				int num5 = (int)((long)base.SendMessage(4117, 0, 0));
				if (num5 > singleMonthSize.Width)
				{
					singleMonthSize.Width = num5;
				}
			}
			singleMonthSize.Width += this.scaledExtraPadding;
			singleMonthSize.Height += this.scaledExtraPadding;
			return singleMonthSize;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000DC264 File Offset: 0x000DA464
		private SelectionRange GetMonthRange(int flag)
		{
			NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
			SelectionRange selectionRange = new SelectionRange();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4103, flag, systemtimearray);
			NativeMethods.SYSTEMTIME systemtime = new NativeMethods.SYSTEMTIME();
			systemtime.wYear = systemtimearray.wYear1;
			systemtime.wMonth = systemtimearray.wMonth1;
			systemtime.wDayOfWeek = systemtimearray.wDayOfWeek1;
			systemtime.wDay = systemtimearray.wDay1;
			selectionRange.Start = DateTimePicker.SysTimeToDateTime(systemtime);
			systemtime.wYear = systemtimearray.wYear2;
			systemtime.wMonth = systemtimearray.wMonth2;
			systemtime.wDayOfWeek = systemtimearray.wDayOfWeek2;
			systemtime.wDay = systemtimearray.wDay2;
			selectionRange.End = DateTimePicker.SysTimeToDateTime(systemtime);
			return selectionRange;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000DC318 File Offset: 0x000DA518
		private int GetPreferredHeight(int height, bool updateRows)
		{
			return this.GetMinReqRect(height, updateRows, false).Height;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000DC338 File Offset: 0x000DA538
		private int GetPreferredWidth(int width, bool updateCols)
		{
			return this.GetMinReqRect(width, false, updateCols).Width;
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000DC358 File Offset: 0x000DA558
		public MonthCalendar.HitTestInfo HitTest(int x, int y)
		{
			NativeMethods.MCHITTESTINFO mchittestinfo = new NativeMethods.MCHITTESTINFO();
			mchittestinfo.pt_x = x;
			mchittestinfo.pt_y = y;
			mchittestinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.MCHITTESTINFO));
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4110, 0, mchittestinfo);
			MonthCalendar.HitArea hitArea = this.GetHitArea(mchittestinfo.uHit);
			if (MonthCalendar.HitTestInfo.HitAreaHasValidDateTime(hitArea))
			{
				NativeMethods.SYSTEMTIME systemtime = new NativeMethods.SYSTEMTIME();
				systemtime.wYear = mchittestinfo.st_wYear;
				systemtime.wMonth = mchittestinfo.st_wMonth;
				systemtime.wDayOfWeek = mchittestinfo.st_wDayOfWeek;
				systemtime.wDay = mchittestinfo.st_wDay;
				systemtime.wHour = mchittestinfo.st_wHour;
				systemtime.wMinute = mchittestinfo.st_wMinute;
				systemtime.wSecond = mchittestinfo.st_wSecond;
				systemtime.wMilliseconds = mchittestinfo.st_wMilliseconds;
				return new MonthCalendar.HitTestInfo(new Point(mchittestinfo.pt_x, mchittestinfo.pt_y), hitArea, DateTimePicker.SysTimeToDateTime(systemtime));
			}
			return new MonthCalendar.HitTestInfo(new Point(mchittestinfo.pt_x, mchittestinfo.pt_y), hitArea);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000DC45A File Offset: 0x000DA65A
		public MonthCalendar.HitTestInfo HitTest(Point point)
		{
			return this.HitTest(point.X, point.Y);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000DC470 File Offset: 0x000DA670
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000DC4A8 File Offset: 0x000DA6A8
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.SetSelRange(this.selectionStart, this.selectionEnd);
			if (this.maxSelectionCount != 7)
			{
				base.SendMessage(4100, this.maxSelectionCount, 0);
			}
			this.AdjustSize();
			if (this.todayDateSet)
			{
				NativeMethods.SYSTEMTIME lParam = DateTimePicker.DateTimeToSysTime(this.todayDate);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4108, 0, lParam);
			}
			this.SetControlColor(1, this.ForeColor);
			this.SetControlColor(4, this.BackColor);
			this.SetControlColor(2, this.titleBackColor);
			this.SetControlColor(3, this.titleForeColor);
			this.SetControlColor(5, this.trailingForeColor);
			int lparam;
			if (this.firstDayOfWeek == Day.Default)
			{
				lparam = 4108;
			}
			else
			{
				lparam = (int)this.firstDayOfWeek;
			}
			base.SendMessage(4111, 0, lparam);
			this.SetRange();
			if (this.scrollChange != 0)
			{
				base.SendMessage(4116, this.scrollChange, 0);
			}
			SystemEvents.UserPreferenceChanged += this.MarshaledUserPreferenceChanged;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000DC5B6 File Offset: 0x000DA7B6
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.MarshaledUserPreferenceChanged;
			base.OnHandleDestroyed(e);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000DC5D0 File Offset: 0x000DA7D0
		private void OnCalendarViewChanged(EventArgs e)
		{
			EventHandler onCalendarViewChanged = this._onCalendarViewChanged;
			if (onCalendarViewChanged == null)
			{
				return;
			}
			onCalendarViewChanged(this, e);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000DC5E4 File Offset: 0x000DA7E4
		protected virtual void OnDateChanged(DateRangeEventArgs drevent)
		{
			if (this.onDateChanged != null)
			{
				this.onDateChanged(this, drevent);
			}
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000DC5FB File Offset: 0x000DA7FB
		protected virtual void OnDateSelected(DateRangeEventArgs drevent)
		{
			if (this.onDateSelected != null)
			{
				this.onDateSelected(this, drevent);
			}
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000DC612 File Offset: 0x000DA812
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = ((MonthCalendar.MonthCalendarAccessibleObjectLevel5)base.AccessibilityObject).FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(20005);
			}
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000DC64A File Offset: 0x000DA84A
		private void OnDisplayRangeChanged(EventArgs e)
		{
			EventHandler onDisplayRangeChanged = this._onDisplayRangeChanged;
			if (onDisplayRangeChanged == null)
			{
				return;
			}
			onDisplayRangeChanged(this, e);
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x000DC65E File Offset: 0x000DA85E
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.AdjustSize();
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000DC66D File Offset: 0x000DA86D
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			this.SetControlColor(1, this.ForeColor);
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000DC683 File Offset: 0x000DA883
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.SetControlColor(4, this.BackColor);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x000DC699 File Offset: 0x000DA899
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (AccessibilityImprovements.Level5)
			{
				this.UpdateDisplayRange();
			}
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000DC6AF File Offset: 0x000DA8AF
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

		// Token: 0x060030F8 RID: 12536 RVA: 0x000DC6E0 File Offset: 0x000DA8E0
		public void RemoveAllAnnuallyBoldedDates()
		{
			this.annualArrayOfDates.Clear();
			for (int i = 0; i < 12; i++)
			{
				this.monthsOfYear[i] = 0;
			}
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000DC70E File Offset: 0x000DA90E
		public void RemoveAllBoldedDates()
		{
			this.arrayOfDates.Clear();
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000DC71B File Offset: 0x000DA91B
		public void RemoveAllMonthlyBoldedDates()
		{
			this.monthlyArrayOfDates.Clear();
			this.datesToBoldMonthly = 0;
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000DC730 File Offset: 0x000DA930
		public void RemoveAnnuallyBoldedDate(DateTime date)
		{
			int num = this.annualArrayOfDates.Count;
			int i;
			for (i = 0; i < num; i++)
			{
				if (this.CompareDayAndMonth((DateTime)this.annualArrayOfDates[i], date))
				{
					this.annualArrayOfDates.RemoveAt(i);
					break;
				}
			}
			num--;
			for (int j = i; j < num; j++)
			{
				if (this.CompareDayAndMonth((DateTime)this.annualArrayOfDates[j], date))
				{
					return;
				}
			}
			this.monthsOfYear[date.Month - 1] &= ~(1 << date.Day - 1);
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000DC7D0 File Offset: 0x000DA9D0
		public void RemoveBoldedDate(DateTime date)
		{
			int count = this.arrayOfDates.Count;
			for (int i = 0; i < count; i++)
			{
				if (DateTime.Compare(((DateTime)this.arrayOfDates[i]).Date, date.Date) == 0)
				{
					this.arrayOfDates.RemoveAt(i);
					base.Invalidate();
					return;
				}
			}
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000DC830 File Offset: 0x000DAA30
		public void RemoveMonthlyBoldedDate(DateTime date)
		{
			int num = this.monthlyArrayOfDates.Count;
			int i;
			for (i = 0; i < num; i++)
			{
				if (this.CompareDayAndMonth((DateTime)this.monthlyArrayOfDates[i], date))
				{
					this.monthlyArrayOfDates.RemoveAt(i);
					break;
				}
			}
			num--;
			for (int j = i; j < num; j++)
			{
				if (this.CompareDayAndMonth((DateTime)this.monthlyArrayOfDates[j], date))
				{
					return;
				}
			}
			this.datesToBoldMonthly &= ~(1 << date.Day - 1);
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000DC8C4 File Offset: 0x000DAAC4
		private void ResetAnnuallyBoldedDates()
		{
			this.annualArrayOfDates.Clear();
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000DC70E File Offset: 0x000DA90E
		private void ResetBoldedDates()
		{
			this.arrayOfDates.Clear();
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000DC8D1 File Offset: 0x000DAAD1
		private void ResetCalendarDimensions()
		{
			this.CalendarDimensions = new Size(1, 1);
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000DC8E0 File Offset: 0x000DAAE0
		private void ResetMaxDate()
		{
			this.MaxDate = DateTime.MaxValue;
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000DC8ED File Offset: 0x000DAAED
		private void ResetMinDate()
		{
			this.MinDate = DateTime.MinValue;
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000DC8FA File Offset: 0x000DAAFA
		private void ResetMonthlyBoldedDates()
		{
			this.monthlyArrayOfDates.Clear();
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000DC907 File Offset: 0x000DAB07
		private void ResetSelectionRange()
		{
			this.SetSelectionRange(this.Now, this.Now);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000DC91B File Offset: 0x000DAB1B
		private void ResetTrailingForeColor()
		{
			this.TrailingForeColor = MonthCalendar.DEFAULT_TRAILING_FORE_COLOR;
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000DC928 File Offset: 0x000DAB28
		private void ResetTitleForeColor()
		{
			this.TitleForeColor = MonthCalendar.DEFAULT_TITLE_FORE_COLOR;
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000DC935 File Offset: 0x000DAB35
		private void ResetTitleBackColor()
		{
			this.TitleBackColor = MonthCalendar.DEFAULT_TITLE_BACK_COLOR;
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000DC942 File Offset: 0x000DAB42
		private void ResetTodayDate()
		{
			this.todayDateSet = false;
			this.UpdateTodayDate();
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000DC954 File Offset: 0x000DAB54
		private IntPtr RequestBuffer(int reqSize)
		{
			int num = 4;
			if (reqSize * num > this.mdsBufferSize)
			{
				if (this.mdsBuffer != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.mdsBuffer);
					this.mdsBuffer = IntPtr.Zero;
				}
				float num2 = (float)(reqSize - 1) / 12f;
				int num3 = (int)(num2 + 1f) * 12;
				this.mdsBufferSize = num3 * num;
				this.mdsBuffer = Marshal.AllocHGlobal(this.mdsBufferSize);
				return this.mdsBuffer;
			}
			return this.mdsBuffer;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000DC9D4 File Offset: 0x000DABD4
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Rectangle bounds = base.Bounds;
			Size maxWindowTrackSize = SystemInformation.MaxWindowTrackSize;
			bool flag = !DpiHelper.EnableMonthCalendarHighDpiImprovements || !base.IsCurrentlyBeingScaled;
			if (width != bounds.Width)
			{
				if (width > maxWindowTrackSize.Width)
				{
					width = maxWindowTrackSize.Width;
				}
				width = this.GetPreferredWidth(width, flag);
			}
			if (height != bounds.Height)
			{
				if (height > maxWindowTrackSize.Height)
				{
					height = maxWindowTrackSize.Height;
				}
				height = this.GetPreferredHeight(height, flag);
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000DCA60 File Offset: 0x000DAC60
		private void SetControlColor(int colorIndex, Color value)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4106, colorIndex, ColorTranslator.ToWin32(value));
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000DCA7D File Offset: 0x000DAC7D
		private void SetRange()
		{
			this.SetRange(DateTimePicker.EffectiveMinDate(this.minDate), DateTimePicker.EffectiveMaxDate(this.maxDate));
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000DCA9C File Offset: 0x000DAC9C
		private void SetRange(DateTime minDate, DateTime maxDate)
		{
			if (this.selectionStart < minDate)
			{
				this.selectionStart = minDate;
			}
			if (this.selectionStart > maxDate)
			{
				this.selectionStart = maxDate;
			}
			if (this.selectionEnd < minDate)
			{
				this.selectionEnd = minDate;
			}
			if (this.selectionEnd > maxDate)
			{
				this.selectionEnd = maxDate;
			}
			if (AccessibilityImprovements.Level5)
			{
				if (this.selectionStart > this._focusedDate)
				{
					this._focusedDate = this.selectionStart.Date;
				}
				if (this.selectionEnd < this._focusedDate)
				{
					this._focusedDate = this.selectionEnd.Date;
				}
			}
			this.SetSelRange(this.selectionStart, this.selectionEnd);
			if (base.IsHandleCreated)
			{
				int num = 0;
				NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
				num |= 3;
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(minDate);
				systemtimearray.wYear1 = systemtime.wYear;
				systemtimearray.wMonth1 = systemtime.wMonth;
				systemtimearray.wDayOfWeek1 = systemtime.wDayOfWeek;
				systemtimearray.wDay1 = systemtime.wDay;
				systemtime = DateTimePicker.DateTimeToSysTime(maxDate);
				systemtimearray.wYear2 = systemtime.wYear;
				systemtimearray.wMonth2 = systemtime.wMonth;
				systemtimearray.wDayOfWeek2 = systemtime.wDayOfWeek;
				systemtimearray.wDay2 = systemtime.wDay;
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, num, systemtimearray) == 0)
				{
					throw new InvalidOperationException(SR.GetString("MonthCalendarRange", new object[]
					{
						minDate.ToShortDateString(),
						maxDate.ToShortDateString()
					}));
				}
				if (AccessibilityImprovements.Level5)
				{
					this.UpdateDisplayRange();
				}
			}
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000DCC3C File Offset: 0x000DAE3C
		public void SetCalendarDimensions(int x, int y)
		{
			if (x < 1)
			{
				throw new ArgumentOutOfRangeException("x", SR.GetString("MonthCalendarInvalidDimensions", new object[]
				{
					x.ToString("D", CultureInfo.CurrentCulture),
					y.ToString("D", CultureInfo.CurrentCulture)
				}));
			}
			if (y < 1)
			{
				throw new ArgumentOutOfRangeException("y", SR.GetString("MonthCalendarInvalidDimensions", new object[]
				{
					x.ToString("D", CultureInfo.CurrentCulture),
					y.ToString("D", CultureInfo.CurrentCulture)
				}));
			}
			while (x * y > 12)
			{
				if (x > y)
				{
					x--;
				}
				else
				{
					y--;
				}
			}
			if (this.dimensions.Width != x || this.dimensions.Height != y)
			{
				this.dimensions.Width = x;
				this.dimensions.Height = y;
				this.AdjustSize();
			}
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000DCD28 File Offset: 0x000DAF28
		public void SetDate(DateTime date)
		{
			if (date.Ticks < this.minDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"date",
					MonthCalendar.FormatDate(date),
					"MinDate"
				}));
			}
			if (date.Ticks > this.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"date",
					MonthCalendar.FormatDate(date),
					"MaxDate"
				}));
			}
			this.SetSelectionRange(date, date);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000DCDD0 File Offset: 0x000DAFD0
		public void SetSelectionRange(DateTime date1, DateTime date2)
		{
			if (date1.Ticks < this.minDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date1", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"SelectionStart",
					MonthCalendar.FormatDate(date1),
					"MinDate"
				}));
			}
			if (date1.Ticks > this.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date1", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"SelectionEnd",
					MonthCalendar.FormatDate(date1),
					"MaxDate"
				}));
			}
			if (date2.Ticks < this.minDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date2", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"SelectionStart",
					MonthCalendar.FormatDate(date2),
					"MinDate"
				}));
			}
			if (date2.Ticks > this.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("date2", SR.GetString("InvalidHighBoundArgumentEx", new object[]
				{
					"SelectionEnd",
					MonthCalendar.FormatDate(date2),
					"MaxDate"
				}));
			}
			if (date1 > date2)
			{
				date2 = date1;
			}
			if ((date2 - date1).Days >= this.maxSelectionCount)
			{
				if (date1.Ticks == this.selectionStart.Ticks)
				{
					date1 = date2.AddDays((double)(1 - this.maxSelectionCount));
				}
				else
				{
					date2 = date1.AddDays((double)(this.maxSelectionCount - 1));
				}
			}
			this.SetSelRange(date1, date2);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000DCF64 File Offset: 0x000DB164
		private void SetSelRange(DateTime lower, DateTime upper)
		{
			bool flag = false;
			if (this.selectionStart != lower || this.selectionEnd != upper)
			{
				flag = true;
				this.selectionStart = lower;
				this.selectionEnd = upper;
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.SYSTEMTIMEARRAY systemtimearray = new NativeMethods.SYSTEMTIMEARRAY();
				NativeMethods.SYSTEMTIME systemtime = DateTimePicker.DateTimeToSysTime(lower);
				systemtimearray.wYear1 = systemtime.wYear;
				systemtimearray.wMonth1 = systemtime.wMonth;
				systemtimearray.wDayOfWeek1 = systemtime.wDayOfWeek;
				systemtimearray.wDay1 = systemtime.wDay;
				systemtime = DateTimePicker.DateTimeToSysTime(upper);
				systemtimearray.wYear2 = systemtime.wYear;
				systemtimearray.wMonth2 = systemtime.wMonth;
				systemtimearray.wDayOfWeek2 = systemtime.wDayOfWeek;
				systemtimearray.wDay2 = systemtime.wDay;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4102, 0, systemtimearray);
			}
			if (flag)
			{
				this.OnDateChanged(new DateRangeEventArgs(lower, upper));
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000DD047 File Offset: 0x000DB247
		private bool ShouldSerializeAnnuallyBoldedDates()
		{
			return this.annualArrayOfDates.Count > 0;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000DD057 File Offset: 0x000DB257
		private bool ShouldSerializeBoldedDates()
		{
			return this.arrayOfDates.Count > 0;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000DD067 File Offset: 0x000DB267
		private bool ShouldSerializeCalendarDimensions()
		{
			return !this.dimensions.Equals(new Size(1, 1));
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000DD08C File Offset: 0x000DB28C
		private bool ShouldSerializeTrailingForeColor()
		{
			return !this.TrailingForeColor.Equals(MonthCalendar.DEFAULT_TRAILING_FORE_COLOR);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000DD0BC File Offset: 0x000DB2BC
		private bool ShouldSerializeTitleForeColor()
		{
			return !this.TitleForeColor.Equals(MonthCalendar.DEFAULT_TITLE_FORE_COLOR);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000DD0EC File Offset: 0x000DB2EC
		private bool ShouldSerializeTitleBackColor()
		{
			return !this.TitleBackColor.Equals(MonthCalendar.DEFAULT_TITLE_BACK_COLOR);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000DD11A File Offset: 0x000DB31A
		private bool ShouldSerializeMonthlyBoldedDates()
		{
			return this.monthlyArrayOfDates.Count > 0;
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000DD12A File Offset: 0x000DB32A
		private bool ShouldSerializeMaxDate()
		{
			return this.maxDate != DateTimePicker.MaximumDateTime && this.maxDate != DateTime.MaxValue;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000DD150 File Offset: 0x000DB350
		private bool ShouldSerializeMinDate()
		{
			return this.minDate != DateTimePicker.MinimumDateTime && this.minDate != DateTime.MinValue;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000DD176 File Offset: 0x000DB376
		private bool ShouldSerializeSelectionRange()
		{
			return !DateTime.Equals(this.selectionEnd, this.selectionStart);
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000DBBFE File Offset: 0x000D9DFE
		private bool ShouldSerializeTodayDate()
		{
			return this.todayDateSet;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000DD18C File Offset: 0x000DB38C
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", " + this.SelectionRange.ToString();
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000DD1B6 File Offset: 0x000DB3B6
		public void UpdateBoldedDates()
		{
			base.RecreateHandle();
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x000DD1C0 File Offset: 0x000DB3C0
		private void UpdateDisplayRange()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			SelectionRange displayRange = this.GetDisplayRange(false);
			if (this._currentDisplayRange == null)
			{
				this._currentDisplayRange = displayRange;
				return;
			}
			if (this._currentDisplayRange.Start != displayRange.Start || this._currentDisplayRange.End != displayRange.End)
			{
				this._currentDisplayRange = displayRange;
				this.OnDisplayRangeChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x000DD230 File Offset: 0x000DB430
		private void UpdateTodayDate()
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.SYSTEMTIME lParam = null;
				if (this.todayDateSet)
				{
					lParam = DateTimePicker.DateTimeToSysTime(this.todayDate);
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4108, 0, lParam);
			}
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000DD274 File Offset: 0x000DB474
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

		// Token: 0x06003122 RID: 12578 RVA: 0x000ABF54 File Offset: 0x000AA154
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Locale)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x000DD2B8 File Offset: 0x000DB4B8
		private void WmDateChanged(ref Message m)
		{
			NativeMethods.NMSELCHANGE nmselchange = (NativeMethods.NMSELCHANGE)m.GetLParam(typeof(NativeMethods.NMSELCHANGE));
			DateTime dateTime;
			DateTime end;
			if (AccessibilityImprovements.Level5)
			{
				dateTime = nmselchange.stSelStart;
				end = nmselchange.stSelEnd;
				this._focusedDate = ((dateTime == this.selectionStart) ? end.Date : dateTime.Date);
				this.selectionStart = dateTime;
				this.selectionEnd = end;
			}
			else
			{
				dateTime = (this.selectionStart = DateTimePicker.SysTimeToDateTime(nmselchange.stSelStart));
				end = (this.selectionEnd = DateTimePicker.SysTimeToDateTime(nmselchange.stSelEnd));
			}
			if (AccessibilityImprovements.Level1)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				base.AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
			}
			if (dateTime.Ticks < this.minDate.Ticks || end.Ticks < this.minDate.Ticks)
			{
				this.SetSelRange(this.minDate, this.minDate);
			}
			else if (dateTime.Ticks > this.maxDate.Ticks || end.Ticks > this.maxDate.Ticks)
			{
				this.SetSelRange(this.maxDate, this.maxDate);
			}
			if (AccessibilityImprovements.Level5 && base.IsAccessibilityObjectCreated)
			{
				this.UpdateDisplayRange();
				MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObjectLevel = (MonthCalendar.MonthCalendarAccessibleObjectLevel5)base.AccessibilityObject;
				monthCalendarAccessibleObjectLevel.RaiseAutomationEventForChild(20005);
			}
			this.OnDateChanged(new DateRangeEventArgs(dateTime, end));
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000DD428 File Offset: 0x000DB628
		private void WmDateBold(ref Message m)
		{
			NativeMethods.NMDAYSTATE nmdaystate = (NativeMethods.NMDAYSTATE)m.GetLParam(typeof(NativeMethods.NMDAYSTATE));
			DateTime start = DateTimePicker.SysTimeToDateTime(nmdaystate.stStart);
			DateBoldEventArgs dateBoldEventArgs = new DateBoldEventArgs(start, nmdaystate.cDayState);
			this.BoldDates(dateBoldEventArgs);
			this.mdsBuffer = this.RequestBuffer(dateBoldEventArgs.Size);
			Marshal.Copy(dateBoldEventArgs.DaysToBold, 0, this.mdsBuffer, dateBoldEventArgs.Size);
			nmdaystate.prgDayState = this.mdsBuffer;
			Marshal.StructureToPtr(nmdaystate, m.LParam, false);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000DD4B0 File Offset: 0x000DB6B0
		private void WmCalViewChanged(ref Message m)
		{
			NativeMethods.NMVIEWCHANGE nmviewchange = (NativeMethods.NMVIEWCHANGE)m.GetLParam(typeof(NativeMethods.NMVIEWCHANGE));
			if (this.mcCurView != (NativeMethods.MONTCALENDAR_VIEW_MODE)nmviewchange.uNewView)
			{
				this.mcOldView = this.mcCurView;
				this.mcCurView = (NativeMethods.MONTCALENDAR_VIEW_MODE)nmviewchange.uNewView;
				if (AccessibilityImprovements.Level5)
				{
					this.OnCalendarViewChanged(EventArgs.Empty);
				}
				if (AccessibilityImprovements.Level1)
				{
					base.AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
					base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				}
			}
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000DD52C File Offset: 0x000DB72C
		private void WmDateSelected(ref Message m)
		{
			NativeMethods.NMSELCHANGE nmselchange = (NativeMethods.NMSELCHANGE)m.GetLParam(typeof(NativeMethods.NMSELCHANGE));
			DateTime start = this.selectionStart = DateTimePicker.SysTimeToDateTime(nmselchange.stSelStart);
			DateTime end = this.selectionEnd = DateTimePicker.SysTimeToDateTime(nmselchange.stSelEnd);
			if (AccessibilityImprovements.Level1)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
				base.AccessibilityNotifyClients(AccessibleEvents.ValueChange, -1);
			}
			if (start.Ticks < this.minDate.Ticks || end.Ticks < this.minDate.Ticks)
			{
				this.SetSelRange(this.minDate, this.minDate);
			}
			else if (start.Ticks > this.maxDate.Ticks || end.Ticks > this.maxDate.Ticks)
			{
				this.SetSelRange(this.maxDate, this.maxDate);
			}
			this.OnDateSelected(new DateRangeEventArgs(start, end));
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000DD61B File Offset: 0x000DB81B
		private void WmGetDlgCode(ref Message m)
		{
			m.Result = (IntPtr)1;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000DD62C File Offset: 0x000DB82C
		private void WmReflectCommand(ref Message m)
		{
			if (m.HWnd == base.Handle)
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				switch (nmhdr.code)
				{
				case -750:
					if (AccessibilityImprovements.Level1)
					{
						this.WmCalViewChanged(ref m);
					}
					break;
				case -749:
					this.WmDateChanged(ref m);
					return;
				case -748:
					break;
				case -747:
					this.WmDateBold(ref m);
					if (AccessibilityImprovements.Level5)
					{
						this.UpdateDisplayRange();
						return;
					}
					break;
				case -746:
					this.WmDateSelected(ref m);
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x000DD6C0 File Offset: 0x000DB8C0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 15)
			{
				if (msg != 2)
				{
					if (msg == 15)
					{
						base.WndProc(ref m);
						if (AccessibilityImprovements.Level5 && this.mcCurView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
						{
							this.UpdateDisplayRange();
							return;
						}
						return;
					}
				}
				else
				{
					bool? flag = MonthCalendar.restrictUnmanagedCode;
					bool flag2 = true;
					if ((flag.GetValueOrDefault() == flag2 & flag != null) && this.nativeWndProcCount > 0)
					{
						throw new InvalidOperationException();
					}
					base.WndProc(ref m);
					if (!AccessibilityImprovements.Level5 || !base.IsAccessibilityObjectCreated)
					{
						return;
					}
					if (base.IsHandleCreated)
					{
						UnsafeNativeMethods.UiaReturnRawElementProvider(new HandleRef(this, base.Handle), IntPtr.Zero, IntPtr.Zero, null);
					}
					if (ApiHelper.IsApiAvailable("UIAutomationCore.dll", "UiaDisconnectProvider"))
					{
						((MonthCalendar.MonthCalendarAccessibleObjectLevel5)base.AccessibilityObject).DisconnectChildren();
						int num = UnsafeNativeMethods.UiaDisconnectProvider(base.AccessibilityObject);
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 135)
				{
					this.WmGetDlgCode(ref m);
					return;
				}
				if (msg != 513)
				{
					if (msg == 8270)
					{
						this.WmReflectCommand(ref m);
						base.WndProc(ref m);
						return;
					}
				}
				else
				{
					this.FocusInternal();
					if (!base.ValidationCancelled)
					{
						base.WndProc(ref m);
						return;
					}
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x000DD7F0 File Offset: 0x000DB9F0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void DefWndProc(ref Message m)
		{
			bool? flag = MonthCalendar.restrictUnmanagedCode;
			bool flag2 = true;
			if (flag.GetValueOrDefault() == flag2 & flag != null)
			{
				this.nativeWndProcCount++;
				try
				{
					base.DefWndProc(ref m);
				}
				finally
				{
					this.nativeWndProcCount--;
				}
				return;
			}
			base.DefWndProc(ref m);
		}

		// Token: 0x04001412 RID: 5138
		private const long DAYS_TO_1601 = 548229L;

		// Token: 0x04001413 RID: 5139
		private const long DAYS_TO_10000 = 3615900L;

		// Token: 0x04001414 RID: 5140
		private static readonly Color DEFAULT_TITLE_BACK_COLOR = SystemColors.ActiveCaption;

		// Token: 0x04001415 RID: 5141
		private static readonly Color DEFAULT_TITLE_FORE_COLOR = SystemColors.ActiveCaptionText;

		// Token: 0x04001416 RID: 5142
		private static readonly Color DEFAULT_TRAILING_FORE_COLOR = SystemColors.GrayText;

		// Token: 0x04001417 RID: 5143
		private const int MINIMUM_ALLOC_SIZE = 12;

		// Token: 0x04001418 RID: 5144
		private const int MONTHS_IN_YEAR = 12;

		// Token: 0x04001419 RID: 5145
		private const int INSERT_WIDTH_SIZE = 6;

		// Token: 0x0400141A RID: 5146
		private const int INSERT_HEIGHT_SIZE = 6;

		// Token: 0x0400141B RID: 5147
		private const Day DEFAULT_FIRST_DAY_OF_WEEK = Day.Default;

		// Token: 0x0400141C RID: 5148
		private const int DEFAULT_MAX_SELECTION_COUNT = 7;

		// Token: 0x0400141D RID: 5149
		private const int DEFAULT_SCROLL_CHANGE = 0;

		// Token: 0x0400141E RID: 5150
		private const int UNIQUE_DATE = 0;

		// Token: 0x0400141F RID: 5151
		private const int ANNUAL_DATE = 1;

		// Token: 0x04001420 RID: 5152
		private const int MONTHLY_DATE = 2;

		// Token: 0x04001421 RID: 5153
		private static readonly Size DefaultSingleMonthSize = new Size(176, 153);

		// Token: 0x04001422 RID: 5154
		private const int MaxScrollChange = 20000;

		// Token: 0x04001423 RID: 5155
		private const int ExtraPadding = 2;

		// Token: 0x04001424 RID: 5156
		private int scaledExtraPadding = 2;

		// Token: 0x04001425 RID: 5157
		private IntPtr mdsBuffer = IntPtr.Zero;

		// Token: 0x04001426 RID: 5158
		private int mdsBufferSize;

		// Token: 0x04001427 RID: 5159
		private Color titleBackColor = MonthCalendar.DEFAULT_TITLE_BACK_COLOR;

		// Token: 0x04001428 RID: 5160
		private Color titleForeColor = MonthCalendar.DEFAULT_TITLE_FORE_COLOR;

		// Token: 0x04001429 RID: 5161
		private Color trailingForeColor = MonthCalendar.DEFAULT_TRAILING_FORE_COLOR;

		// Token: 0x0400142A RID: 5162
		private bool showToday = true;

		// Token: 0x0400142B RID: 5163
		private bool showTodayCircle = true;

		// Token: 0x0400142C RID: 5164
		private bool showWeekNumbers;

		// Token: 0x0400142D RID: 5165
		private bool rightToLeftLayout;

		// Token: 0x0400142E RID: 5166
		private Size dimensions = new Size(1, 1);

		// Token: 0x0400142F RID: 5167
		private int maxSelectionCount = 7;

		// Token: 0x04001430 RID: 5168
		private DateTime maxDate = DateTime.MaxValue;

		// Token: 0x04001431 RID: 5169
		private DateTime minDate = DateTime.MinValue;

		// Token: 0x04001432 RID: 5170
		private int scrollChange;

		// Token: 0x04001433 RID: 5171
		private bool todayDateSet;

		// Token: 0x04001434 RID: 5172
		private DateTime todayDate = DateTime.Now.Date;

		// Token: 0x04001435 RID: 5173
		private DateTime selectionStart;

		// Token: 0x04001436 RID: 5174
		private DateTime selectionEnd;

		// Token: 0x04001437 RID: 5175
		private DateTime _focusedDate;

		// Token: 0x04001438 RID: 5176
		private SelectionRange _currentDisplayRange;

		// Token: 0x04001439 RID: 5177
		private Day firstDayOfWeek = Day.Default;

		// Token: 0x0400143A RID: 5178
		private NativeMethods.MONTCALENDAR_VIEW_MODE mcCurView;

		// Token: 0x0400143B RID: 5179
		private NativeMethods.MONTCALENDAR_VIEW_MODE mcOldView;

		// Token: 0x0400143C RID: 5180
		private int[] monthsOfYear = new int[12];

		// Token: 0x0400143D RID: 5181
		private int datesToBoldMonthly;

		// Token: 0x0400143E RID: 5182
		private ArrayList arrayOfDates = new ArrayList();

		// Token: 0x0400143F RID: 5183
		private ArrayList annualArrayOfDates = new ArrayList();

		// Token: 0x04001440 RID: 5184
		private ArrayList monthlyArrayOfDates = new ArrayList();

		// Token: 0x04001441 RID: 5185
		private DateRangeEventHandler onDateChanged;

		// Token: 0x04001442 RID: 5186
		private DateRangeEventHandler onDateSelected;

		// Token: 0x04001443 RID: 5187
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04001444 RID: 5188
		private EventHandler _onCalendarViewChanged;

		// Token: 0x04001445 RID: 5189
		private EventHandler _onDisplayRangeChanged;

		// Token: 0x04001446 RID: 5190
		private int nativeWndProcCount;

		// Token: 0x04001447 RID: 5191
		private static bool? restrictUnmanagedCode;

		// Token: 0x020006DE RID: 1758
		public sealed class HitTestInfo
		{
			// Token: 0x06006B2A RID: 27434 RVA: 0x0018CF7D File Offset: 0x0018B17D
			internal HitTestInfo(Point pt, MonthCalendar.HitArea area, DateTime time)
			{
				this.point = pt;
				this.hitArea = area;
				this.time = time;
			}

			// Token: 0x06006B2B RID: 27435 RVA: 0x0018CF9A File Offset: 0x0018B19A
			internal HitTestInfo(Point pt, MonthCalendar.HitArea area)
			{
				this.point = pt;
				this.hitArea = area;
			}

			// Token: 0x1700173E RID: 5950
			// (get) Token: 0x06006B2C RID: 27436 RVA: 0x0018CFB0 File Offset: 0x0018B1B0
			public Point Point
			{
				get
				{
					return this.point;
				}
			}

			// Token: 0x1700173F RID: 5951
			// (get) Token: 0x06006B2D RID: 27437 RVA: 0x0018CFB8 File Offset: 0x0018B1B8
			public MonthCalendar.HitArea HitArea
			{
				get
				{
					return this.hitArea;
				}
			}

			// Token: 0x17001740 RID: 5952
			// (get) Token: 0x06006B2E RID: 27438 RVA: 0x0018CFC0 File Offset: 0x0018B1C0
			public DateTime Time
			{
				get
				{
					return this.time;
				}
			}

			// Token: 0x06006B2F RID: 27439 RVA: 0x0018CFC8 File Offset: 0x0018B1C8
			internal static bool HitAreaHasValidDateTime(MonthCalendar.HitArea hitArea)
			{
				return hitArea == MonthCalendar.HitArea.Date || hitArea == MonthCalendar.HitArea.WeekNumbers;
			}

			// Token: 0x04003B69 RID: 15209
			private readonly Point point;

			// Token: 0x04003B6A RID: 15210
			private readonly MonthCalendar.HitArea hitArea;

			// Token: 0x04003B6B RID: 15211
			private readonly DateTime time;
		}

		// Token: 0x020006DF RID: 1759
		public enum HitArea
		{
			// Token: 0x04003B6D RID: 15213
			Nowhere,
			// Token: 0x04003B6E RID: 15214
			TitleBackground,
			// Token: 0x04003B6F RID: 15215
			TitleMonth,
			// Token: 0x04003B70 RID: 15216
			TitleYear,
			// Token: 0x04003B71 RID: 15217
			NextMonthButton,
			// Token: 0x04003B72 RID: 15218
			PrevMonthButton,
			// Token: 0x04003B73 RID: 15219
			CalendarBackground,
			// Token: 0x04003B74 RID: 15220
			Date,
			// Token: 0x04003B75 RID: 15221
			NextMonthDate,
			// Token: 0x04003B76 RID: 15222
			PrevMonthDate,
			// Token: 0x04003B77 RID: 15223
			DayOfWeek,
			// Token: 0x04003B78 RID: 15224
			WeekNumbers,
			// Token: 0x04003B79 RID: 15225
			TodayLink
		}

		// Token: 0x020006E0 RID: 1760
		[ComVisible(true)]
		internal class MonthCalendarAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006B30 RID: 27440 RVA: 0x0018CFD6 File Offset: 0x0018B1D6
			public MonthCalendarAccessibleObject(Control owner) : base(owner)
			{
				this.calendar = (owner as MonthCalendar);
			}

			// Token: 0x06006B31 RID: 27441 RVA: 0x0018CFEB File Offset: 0x0018B1EB
			internal override void ClearOwnerControlInternal()
			{
				this.calendar = null;
				base.ClearOwnerControlInternal();
			}

			// Token: 0x17001741 RID: 5953
			// (get) Token: 0x06006B32 RID: 27442 RVA: 0x0018CFFC File Offset: 0x0018B1FC
			public override AccessibleRole Role
			{
				get
				{
					if (this.calendar != null)
					{
						AccessibleRole accessibleRole = this.calendar.AccessibleRole;
						if (accessibleRole != AccessibleRole.Default)
						{
							return accessibleRole;
						}
					}
					return AccessibleRole.Table;
				}
			}

			// Token: 0x17001742 RID: 5954
			// (get) Token: 0x06006B33 RID: 27443 RVA: 0x0018D028 File Offset: 0x0018B228
			public override string Help
			{
				get
				{
					string help = base.Help;
					if (help != null)
					{
						return help;
					}
					if (this.calendar != null)
					{
						return this.calendar.GetType().Name + "(" + this.calendar.GetType().BaseType.Name + ")";
					}
					return string.Empty;
				}
			}

			// Token: 0x17001743 RID: 5955
			// (get) Token: 0x06006B34 RID: 27444 RVA: 0x0018D084 File Offset: 0x0018B284
			public override string Name
			{
				get
				{
					string text = base.Name;
					if (text != null)
					{
						return text;
					}
					if (this.calendar != null)
					{
						if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
						{
							if (DateTime.Equals(this.calendar.SelectionStart.Date, this.calendar.SelectionEnd.Date))
							{
								text = SR.GetString("MonthCalendarSingleDateSelected", new object[]
								{
									this.calendar.SelectionStart.ToLongDateString()
								});
							}
							else
							{
								text = SR.GetString("MonthCalendarRangeSelected", new object[]
								{
									this.calendar.SelectionStart.ToLongDateString(),
									this.calendar.SelectionEnd.ToLongDateString()
								});
							}
						}
						else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_YEAR)
						{
							if (object.Equals(this.calendar.SelectionStart.Month, this.calendar.SelectionEnd.Month))
							{
								text = SR.GetString("MonthCalendarSingleDateSelected", new object[]
								{
									this.calendar.SelectionStart.ToString("y")
								});
							}
							else
							{
								text = SR.GetString("MonthCalendarRangeSelected", new object[]
								{
									this.calendar.SelectionStart.ToString("y"),
									this.calendar.SelectionEnd.ToString("y")
								});
							}
						}
						else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_DECADE)
						{
							if (object.Equals(this.calendar.SelectionStart.Year, this.calendar.SelectionEnd.Year))
							{
								text = SR.GetString("MonthCalendarSingleYearSelected", new object[]
								{
									this.calendar.SelectionStart.ToString("yyyy")
								});
							}
							else
							{
								text = SR.GetString("MonthCalendarYearRangeSelected", new object[]
								{
									this.calendar.SelectionStart.ToString("yyyy"),
									this.calendar.SelectionEnd.ToString("yyyy")
								});
							}
						}
						else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_CENTURY)
						{
							text = SR.GetString("MonthCalendarSingleDecadeSelected", new object[]
							{
								this.calendar.SelectionStart.ToString("yyyy")
							});
						}
					}
					return text;
				}
			}

			// Token: 0x17001744 RID: 5956
			// (get) Token: 0x06006B35 RID: 27445 RVA: 0x0018D318 File Offset: 0x0018B518
			// (set) Token: 0x06006B36 RID: 27446 RVA: 0x0018D4C4 File Offset: 0x0018B6C4
			public override string Value
			{
				get
				{
					string result = string.Empty;
					try
					{
						if (this.calendar != null)
						{
							if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
							{
								if (DateTime.Equals(this.calendar.SelectionStart.Date, this.calendar.SelectionEnd.Date))
								{
									result = this.calendar.SelectionStart.ToLongDateString();
								}
								else
								{
									result = string.Format("{0} - {1}", this.calendar.SelectionStart.ToLongDateString(), this.calendar.SelectionEnd.ToLongDateString());
								}
							}
							else if (this.calendar.mcCurView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_YEAR)
							{
								if (object.Equals(this.calendar.SelectionStart.Month, this.calendar.SelectionEnd.Month))
								{
									result = this.calendar.SelectionStart.ToString("y");
								}
								else
								{
									result = string.Format("{0} - {1}", this.calendar.SelectionStart.ToString("y"), this.calendar.SelectionEnd.ToString("y"));
								}
							}
							else
							{
								result = string.Format("{0} - {1}", this.calendar.SelectionRange.Start.ToString(), this.calendar.SelectionRange.End.ToString());
							}
						}
					}
					catch
					{
						result = base.Value;
					}
					return result;
				}
				set
				{
					base.Value = value;
				}
			}

			// Token: 0x04003B7A RID: 15226
			protected MonthCalendar calendar;
		}

		// Token: 0x020006E1 RID: 1761
		internal class CalendarAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006B37 RID: 27447 RVA: 0x0018D4CD File Offset: 0x0018B6CD
			public CalendarAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject, int calendarIndex, string initName) : base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._initName = initName;
			}

			// Token: 0x06006B38 RID: 27448 RVA: 0x0018D4EC File Offset: 0x0018B6EC
			internal void DisconnectChildren()
			{
				int num = UnsafeNativeMethods.UiaDisconnectProvider(this._calendarHeaderAccessibleObject);
				if (this._calendarBodyAccessibleObject != null)
				{
					this._calendarBodyAccessibleObject.DisconnectChildren();
					num = UnsafeNativeMethods.UiaDisconnectProvider(this._calendarBodyAccessibleObject);
				}
			}

			// Token: 0x17001745 RID: 5957
			// (get) Token: 0x06006B39 RID: 27449 RVA: 0x0018D524 File Offset: 0x0018B724
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(4U, this._calendarIndex, 0, 0);
				}
			}

			// Token: 0x17001746 RID: 5958
			// (get) Token: 0x06006B3A RID: 27450 RVA: 0x0018D53F File Offset: 0x0018B73F
			internal MonthCalendar.CalendarBodyAccessibleObject CalendarBodyAccessibleObject
			{
				get
				{
					if (this._calendarBodyAccessibleObject == null)
					{
						this._calendarBodyAccessibleObject = new MonthCalendar.CalendarBodyAccessibleObject(this, this._monthCalendarAccessibleObject, this._calendarIndex);
					}
					return this._calendarBodyAccessibleObject;
				}
			}

			// Token: 0x17001747 RID: 5959
			// (get) Token: 0x06006B3B RID: 27451 RVA: 0x0018D567 File Offset: 0x0018B767
			internal MonthCalendar.CalendarHeaderAccessibleObject CalendarHeaderAccessibleObject
			{
				get
				{
					if (this._calendarHeaderAccessibleObject == null)
					{
						this._calendarHeaderAccessibleObject = new MonthCalendar.CalendarHeaderAccessibleObject(this, this._monthCalendarAccessibleObject, this._calendarIndex);
					}
					return this._calendarHeaderAccessibleObject;
				}
			}

			// Token: 0x17001748 RID: 5960
			// (get) Token: 0x06006B3C RID: 27452 RVA: 0x0018D58F File Offset: 0x0018B78F
			internal override int Column
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return -1;
					}
					return this._calendarIndex % this._monthCalendarAccessibleObject.ColumnCount;
				}
			}

			// Token: 0x17001749 RID: 5961
			// (get) Token: 0x06006B3D RID: 27453 RVA: 0x0018D5B2 File Offset: 0x0018B7B2
			internal override UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x1700174A RID: 5962
			// (get) Token: 0x06006B3E RID: 27454 RVA: 0x0018D5BC File Offset: 0x0018B7BC
			internal SelectionRange DateRange
			{
				get
				{
					if (this._dateRange == null && this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						SelectionRange calendarPartDateRange = this._monthCalendarAccessibleObject.GetCalendarPartDateRange(4U, this._calendarIndex, 0, 0);
						if (calendarPartDateRange == null)
						{
							return null;
						}
						SelectionRange displayRange = this._monthCalendarAccessibleObject.GetDisplayRange(false);
						if (displayRange == null)
						{
							return null;
						}
						if (this._calendarIndex == 0 && displayRange.Start < calendarPartDateRange.Start)
						{
							calendarPartDateRange.Start = displayRange.Start;
						}
						LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
						MonthCalendar.CalendarAccessibleObject calendarAccessibleObject;
						if (calendarsAccessibleObjects == null)
						{
							calendarAccessibleObject = null;
						}
						else
						{
							LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects.Last;
							calendarAccessibleObject = ((last != null) ? last.Value : null);
						}
						if (calendarAccessibleObject == this && displayRange.End > calendarPartDateRange.End)
						{
							calendarPartDateRange.End = displayRange.End;
						}
						this._dateRange = calendarPartDateRange;
					}
					return this._dateRange;
				}
			}

			// Token: 0x06006B3F RID: 27455 RVA: 0x0018D68C File Offset: 0x0018B88C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
					MonthCalendar.CalendarTodayLinkAccessibleObject calendarTodayLinkAccessibleObject;
					if (calendarsAccessibleObjects == null)
					{
						calendarTodayLinkAccessibleObject = null;
					}
					else
					{
						LinkedListNode<MonthCalendar.CalendarAccessibleObject> linkedListNode = calendarsAccessibleObjects.Find(this);
						if (linkedListNode == null)
						{
							calendarTodayLinkAccessibleObject = null;
						}
						else
						{
							LinkedListNode<MonthCalendar.CalendarAccessibleObject> next = linkedListNode.Next;
							calendarTodayLinkAccessibleObject = ((next != null) ? next.Value : null);
						}
					}
					MonthCalendar.CalendarTodayLinkAccessibleObject result;
					if ((result = calendarTodayLinkAccessibleObject) == null)
					{
						if (!this._monthCalendarAccessibleObject.ShowToday)
						{
							return null;
						}
						result = this._monthCalendarAccessibleObject.TodayLinkAccessibleObject;
					}
					return result;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					if (this._calendarIndex == 0)
					{
						return this._monthCalendarAccessibleObject.NextButtonAccessibleObject;
					}
					LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects2 = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
					if (calendarsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> linkedListNode2 = calendarsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> previous = linkedListNode2.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
					return this.CalendarHeaderAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.LastChild:
					return this.CalendarBodyAccessibleObject;
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x06006B40 RID: 27456 RVA: 0x0018D75C File Offset: 0x0018B95C
			internal override int GetChildId()
			{
				return 3 + this._calendarIndex;
			}

			// Token: 0x06006B41 RID: 27457 RVA: 0x00015ECC File Offset: 0x000140CC
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
			{
				return null;
			}

			// Token: 0x06006B42 RID: 27458 RVA: 0x0018D768 File Offset: 0x0018B968
			internal MonthCalendar.MonthCalendarChildAccessibleObject GetChildFromPoint(NativeMethods.MCHITTESTINFOLEVEL5 hitTestInfo)
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || this.CalendarBodyAccessibleObject.RowsAccessibleObjects == null)
				{
					return this;
				}
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject = null;
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject2 in this.CalendarBodyAccessibleObject.RowsAccessibleObjects)
				{
					if (calendarRowAccessibleObject2.Row == hitTestInfo.iRow)
					{
						calendarRowAccessibleObject = calendarRowAccessibleObject2;
						break;
					}
				}
				if (calendarRowAccessibleObject == null)
				{
					return this;
				}
				if (hitTestInfo.uHit == 131075)
				{
					return calendarRowAccessibleObject.WeekNumberCellAccessibleObject ?? this;
				}
				if (calendarRowAccessibleObject.CellsAccessibleObjects == null)
				{
					return this;
				}
				MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject = null;
				foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject2 in calendarRowAccessibleObject.CellsAccessibleObjects)
				{
					if (calendarCellAccessibleObject2.Column == hitTestInfo.iCol)
					{
						calendarCellAccessibleObject = calendarCellAccessibleObject2;
						break;
					}
				}
				if (calendarCellAccessibleObject == null)
				{
					return this;
				}
				return calendarCellAccessibleObject;
			}

			// Token: 0x06006B43 RID: 27459 RVA: 0x0018D86C File Offset: 0x0018BA6C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50033;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return this.IsEnabled;
			}

			// Token: 0x06006B44 RID: 27460 RVA: 0x00015ECC File Offset: 0x000140CC
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
			{
				return null;
			}

			// Token: 0x1700174B RID: 5963
			// (get) Token: 0x06006B45 RID: 27461 RVA: 0x0018D8A0 File Offset: 0x0018BAA0
			internal override bool HasKeyboardFocus
			{
				get
				{
					if (this._monthCalendarAccessibleObject.Focused)
					{
						MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
						int? num = (focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null;
						int calendarIndex = this._calendarIndex;
						return num.GetValueOrDefault() == calendarIndex & num != null;
					}
					return false;
				}
			}

			// Token: 0x06006B46 RID: 27462 RVA: 0x0018D8FA File Offset: 0x0018BAFA
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10007 || patternId == 10013 || base.IsPatternSupported(patternId);
			}

			// Token: 0x1700174C RID: 5964
			// (get) Token: 0x06006B47 RID: 27463 RVA: 0x0018D919 File Offset: 0x0018BB19
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x1700174D RID: 5965
			// (get) Token: 0x06006B48 RID: 27464 RVA: 0x0018D5B2 File Offset: 0x0018B7B2
			public override AccessibleObject Parent
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x1700174E RID: 5966
			// (get) Token: 0x06006B49 RID: 27465 RVA: 0x0015966C File Offset: 0x0015786C
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Client;
				}
			}

			// Token: 0x1700174F RID: 5967
			// (get) Token: 0x06006B4A RID: 27466 RVA: 0x0018D921 File Offset: 0x0018BB21
			internal override int Row
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return -1;
					}
					return this._calendarIndex / this._monthCalendarAccessibleObject.ColumnCount;
				}
			}

			// Token: 0x06006B4B RID: 27467 RVA: 0x0018D944 File Offset: 0x0018BB44
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
				int? num = (focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null;
				int calendarIndex = this._calendarIndex;
				if (num.GetValueOrDefault() == calendarIndex & num != null)
				{
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x17001750 RID: 5968
			// (get) Token: 0x06006B4C RID: 27468 RVA: 0x0018D9A0 File Offset: 0x0018BBA0
			public override AccessibleStates State
			{
				get
				{
					if (!this.IsEnabled)
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this.HasKeyboardFocus)
					{
						accessibleStates |= (AccessibleStates.Selected | AccessibleStates.Focused);
					}
					return accessibleStates;
				}
			}

			// Token: 0x04003B7B RID: 15227
			private const int ChildIdIncrement = 3;

			// Token: 0x04003B7C RID: 15228
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B7D RID: 15229
			private readonly int _calendarIndex;

			// Token: 0x04003B7E RID: 15230
			private readonly string _initName;

			// Token: 0x04003B7F RID: 15231
			private MonthCalendar.CalendarBodyAccessibleObject _calendarBodyAccessibleObject;

			// Token: 0x04003B80 RID: 15232
			private MonthCalendar.CalendarHeaderAccessibleObject _calendarHeaderAccessibleObject;

			// Token: 0x04003B81 RID: 15233
			private SelectionRange _dateRange;
		}

		// Token: 0x020006E2 RID: 1762
		internal class CalendarBodyAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006B4D RID: 27469 RVA: 0x0018D9CC File Offset: 0x0018BBCC
			public CalendarBodyAccessibleObject(MonthCalendar.CalendarAccessibleObject calendarAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex) : base(monthCalendarAccessibleObject)
			{
				this._calendarAccessibleObject = calendarAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._initName = this._monthCalendarAccessibleObject.GetCalendarPartText(5U, this._calendarIndex, 0, 0);
				this._initRuntimeId = new int[]
				{
					this._calendarAccessibleObject.RuntimeId[0],
					this._calendarAccessibleObject.RuntimeId[1],
					this._calendarAccessibleObject.RuntimeId[2],
					this.GetChildId()
				};
			}

			// Token: 0x17001751 RID: 5969
			// (get) Token: 0x06006B4E RID: 27470 RVA: 0x0018DA54 File Offset: 0x0018BC54
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(6U, this._calendarIndex, 0, 0);
				}
			}

			// Token: 0x06006B4F RID: 27471 RVA: 0x0018DA70 File Offset: 0x0018BC70
			internal void DisconnectChildren()
			{
				if (this._rowsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in this._rowsAccessibleObjects)
				{
					calendarRowAccessibleObject.DisconnectChildren();
					int num = UnsafeNativeMethods.UiaDisconnectProvider(calendarRowAccessibleObject);
				}
			}

			// Token: 0x06006B50 RID: 27472 RVA: 0x0018DAD4 File Offset: 0x0018BCD4
			internal void ClearChildCollection()
			{
				if (this.RowsAccessibleObjects != null)
				{
					foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in this.RowsAccessibleObjects)
					{
						calendarRowAccessibleObject.ClearChildCollection();
					}
				}
				this._rowsAccessibleObjects = null;
			}

			// Token: 0x17001752 RID: 5970
			// (get) Token: 0x06006B51 RID: 27473 RVA: 0x0018DB38 File Offset: 0x0018BD38
			internal override int ColumnCount
			{
				get
				{
					if (this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
					{
						return 4;
					}
					return 7;
				}
			}

			// Token: 0x06006B52 RID: 27474 RVA: 0x0018DB4C File Offset: 0x0018BD4C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
					return null;
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
					return this._calendarAccessibleObject.CalendarHeaderAccessibleObject;
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this.RowsAccessibleObjects;
					if (rowsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> first = rowsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.LastChild:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects2 = this.RowsAccessibleObjects;
					if (rowsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> last = rowsAccessibleObjects2.Last;
					if (last == null)
					{
						return null;
					}
					return last.Value;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x06006B53 RID: 27475 RVA: 0x0001627D File Offset: 0x0001447D
			internal override int GetChildId()
			{
				return 2;
			}

			// Token: 0x06006B54 RID: 27476 RVA: 0x0018DBC4 File Offset: 0x0018BDC4
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaders()
			{
				if (this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
				{
					return null;
				}
				LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this.RowsAccessibleObjects;
				LinkedList<MonthCalendar.CalendarCellAccessibleObject> linkedList;
				if (rowsAccessibleObjects == null)
				{
					linkedList = null;
				}
				else
				{
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> first = rowsAccessibleObjects.First;
					linkedList = ((first != null) ? first.Value.CellsAccessibleObjects : null);
				}
				LinkedList<MonthCalendar.CalendarCellAccessibleObject> linkedList2 = linkedList;
				if (linkedList2 == null)
				{
					return null;
				}
				UnsafeNativeMethods.IRawElementProviderSimple[] result;
				try
				{
					UnsafeNativeMethods.IRawElementProviderSimple[] array = new UnsafeNativeMethods.IRawElementProviderSimple[linkedList2.Count];
					int num = 0;
					foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in linkedList2)
					{
						array[num++] = calendarCellAccessibleObject;
					}
					result = array;
				}
				catch (Exception ex)
				{
					throw ex;
				}
				return result;
			}

			// Token: 0x06006B55 RID: 27477 RVA: 0x0018DC74 File Offset: 0x0018BE74
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetItem(int rowIndex, int columnIndex)
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || this.RowsAccessibleObjects == null)
				{
					return null;
				}
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject = null;
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject2 in this.RowsAccessibleObjects)
				{
					if (calendarRowAccessibleObject2.Row == rowIndex)
					{
						calendarRowAccessibleObject = calendarRowAccessibleObject2;
						break;
					}
				}
				if (calendarRowAccessibleObject == null)
				{
					return null;
				}
				if (rowIndex >= 0 && columnIndex == -1)
				{
					return calendarRowAccessibleObject.WeekNumberCellAccessibleObject;
				}
				if (calendarRowAccessibleObject.CellsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in calendarRowAccessibleObject.CellsAccessibleObjects)
				{
					if (calendarCellAccessibleObject.Column == columnIndex)
					{
						return calendarCellAccessibleObject;
					}
				}
				return null;
			}

			// Token: 0x06006B56 RID: 27478 RVA: 0x0018DD54 File Offset: 0x0018BF54
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30009)
				{
					if (propertyID == 30003)
					{
						return 50036;
					}
					if (propertyID == 30009)
					{
						return this.IsEnabled;
					}
				}
				else
				{
					if (propertyID == 30030)
					{
						return this.IsPatternSupported(10006);
					}
					if (propertyID == 30038)
					{
						return this.IsPatternSupported(10012);
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006B57 RID: 27479 RVA: 0x0018DDD0 File Offset: 0x0018BFD0
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaders()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.ShowWeekNumbers || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH || this.RowsAccessibleObjects == null)
				{
					return null;
				}
				List<UnsafeNativeMethods.IRawElementProviderSimple> list = new List<UnsafeNativeMethods.IRawElementProviderSimple>();
				foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in this.RowsAccessibleObjects)
				{
					if (calendarRowAccessibleObject.Row != -1)
					{
						if (calendarRowAccessibleObject.WeekNumberCellAccessibleObject == null)
						{
							return null;
						}
						list.Add(calendarRowAccessibleObject.WeekNumberCellAccessibleObject);
					}
				}
				return list.ToArray();
			}

			// Token: 0x17001753 RID: 5971
			// (get) Token: 0x06006B58 RID: 27480 RVA: 0x0018DE7C File Offset: 0x0018C07C
			internal override bool HasKeyboardFocus
			{
				get
				{
					if (this._monthCalendarAccessibleObject.Focused)
					{
						MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
						int? num = (focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null;
						int calendarIndex = this._calendarIndex;
						return num.GetValueOrDefault() == calendarIndex & num != null;
					}
					return false;
				}
			}

			// Token: 0x06006B59 RID: 27481 RVA: 0x0018DED6 File Offset: 0x0018C0D6
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10006 || patternId == 10012 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001754 RID: 5972
			// (get) Token: 0x06006B5A RID: 27482 RVA: 0x0018DEF5 File Offset: 0x0018C0F5
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x17001755 RID: 5973
			// (get) Token: 0x06006B5B RID: 27483 RVA: 0x0018DEFD File Offset: 0x0018C0FD
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarAccessibleObject;
				}
			}

			// Token: 0x17001756 RID: 5974
			// (get) Token: 0x06006B5C RID: 27484 RVA: 0x0018DF05 File Offset: 0x0018C105
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Table;
				}
			}

			// Token: 0x17001757 RID: 5975
			// (get) Token: 0x06006B5D RID: 27485 RVA: 0x0018DF09 File Offset: 0x0018C109
			internal override int RowCount
			{
				get
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this.RowsAccessibleObjects;
					if (rowsAccessibleObjects == null)
					{
						return -1;
					}
					return rowsAccessibleObjects.Count;
				}
			}

			// Token: 0x17001758 RID: 5976
			// (get) Token: 0x06006B5E RID: 27486 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal override UnsafeNativeMethods.RowOrColumnMajor RowOrColumnMajor
			{
				get
				{
					return UnsafeNativeMethods.RowOrColumnMajor.RowOrColumnMajor_RowMajor;
				}
			}

			// Token: 0x17001759 RID: 5977
			// (get) Token: 0x06006B5F RID: 27487 RVA: 0x0018DF1C File Offset: 0x0018C11C
			internal LinkedList<MonthCalendar.CalendarRowAccessibleObject> RowsAccessibleObjects
			{
				get
				{
					if (this._rowsAccessibleObjects == null && this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						this._rowsAccessibleObjects = new LinkedList<MonthCalendar.CalendarRowAccessibleObject>();
						int num = (this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH) ? -1 : 0;
						int num2 = (this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH) ? 6 : 3;
						for (int i = num; i < num2; i++)
						{
							MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject = new MonthCalendar.CalendarRowAccessibleObject(this, this._monthCalendarAccessibleObject, this._calendarIndex, i);
							LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = calendarRowAccessibleObject.CellsAccessibleObjects;
							if (cellsAccessibleObjects != null && cellsAccessibleObjects.Count > 0)
							{
								this._rowsAccessibleObjects.AddLast(calendarRowAccessibleObject);
							}
						}
					}
					return this._rowsAccessibleObjects;
				}
			}

			// Token: 0x1700175A RID: 5978
			// (get) Token: 0x06006B60 RID: 27488 RVA: 0x0018DFBA File Offset: 0x0018C1BA
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x06006B61 RID: 27489 RVA: 0x0018DFC4 File Offset: 0x0018C1C4
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
				int? num = (focusedCell != null) ? new int?(focusedCell.CalendarIndex) : null;
				int calendarIndex = this._calendarIndex;
				if (num.GetValueOrDefault() == calendarIndex & num != null)
				{
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x1700175B RID: 5979
			// (get) Token: 0x06006B62 RID: 27490 RVA: 0x0018E01E File Offset: 0x0018C21E
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.Default;
				}
			}

			// Token: 0x04003B82 RID: 15234
			private const int ChildId = 2;

			// Token: 0x04003B83 RID: 15235
			private readonly MonthCalendar.CalendarAccessibleObject _calendarAccessibleObject;

			// Token: 0x04003B84 RID: 15236
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B85 RID: 15237
			private readonly int _calendarIndex;

			// Token: 0x04003B86 RID: 15238
			private readonly string _initName;

			// Token: 0x04003B87 RID: 15239
			private readonly int[] _initRuntimeId;

			// Token: 0x04003B88 RID: 15240
			private LinkedList<MonthCalendar.CalendarRowAccessibleObject> _rowsAccessibleObjects;
		}

		// Token: 0x020006E3 RID: 1763
		internal abstract class CalendarButtonAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006B63 RID: 27491 RVA: 0x0018E025 File Offset: 0x0018C225
			public CalendarButtonAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject) : base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x1700175C RID: 5980
			// (get) Token: 0x06006B64 RID: 27492 RVA: 0x00187100 File Offset: 0x00185300
			public override string DefaultAction
			{
				get
				{
					return SR.GetString("AccessibleActionClick");
				}
			}

			// Token: 0x06006B65 RID: 27493 RVA: 0x00016430 File Offset: 0x00014630
			public override void DoDefaultAction()
			{
				this.Invoke();
			}

			// Token: 0x06006B66 RID: 27494 RVA: 0x0018E035 File Offset: 0x0018C235
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50000;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006B67 RID: 27495 RVA: 0x0018E051 File Offset: 0x0018C251
			internal override void Invoke()
			{
				this.RaiseMouseClick();
			}

			// Token: 0x06006B68 RID: 27496 RVA: 0x0018E059 File Offset: 0x0018C259
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10000 || base.IsPatternSupported(patternId);
			}

			// Token: 0x1700175D RID: 5981
			// (get) Token: 0x06006B69 RID: 27497 RVA: 0x0018E06C File Offset: 0x0018C26C
			public override AccessibleObject Parent
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x06006B6A RID: 27498 RVA: 0x0018E074 File Offset: 0x0018C274
			private void RaiseMouseClick()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.IsEnabled || !this.IsEnabled)
				{
					return;
				}
				NativeMethods.RECT rect = this.Bounds;
				int x = rect.left + (rect.right - rect.left) / 2;
				int y = rect.top + (rect.bottom - rect.top) / 2;
				this.RaiseMouseClick(x, y);
			}

			// Token: 0x06006B6B RID: 27499 RVA: 0x0018E0E8 File Offset: 0x0018C2E8
			private void RaiseMouseClick(int x, int y)
			{
				Point point = default(Point);
				UnsafeNativeMethods.BOOL physicalCursorPos = UnsafeNativeMethods.GetPhysicalCursorPos(ref point);
				bool flag = UnsafeNativeMethods.GetSystemMetrics(23) != 0;
				this.SendMouseInput(x, y, 32769U);
				this.SendMouseInput(0, 0, flag ? 8U : 2U);
				this.SendMouseInput(0, 0, flag ? 16U : 4U);
				Thread.Sleep(50);
				if (physicalCursorPos == UnsafeNativeMethods.BOOL.TRUE)
				{
					this.SendMouseInput(point.X, point.Y, 32769U);
				}
			}

			// Token: 0x1700175E RID: 5982
			// (get) Token: 0x06006B6C RID: 27500 RVA: 0x0015F2AD File Offset: 0x0015D4AD
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.PushButton;
				}
			}

			// Token: 0x06006B6D RID: 27501 RVA: 0x0018E160 File Offset: 0x0018C360
			private void SendMouseInput(int x, int y, uint flags)
			{
				if ((flags & 32768U) != 0U)
				{
					int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(78);
					int systemMetrics2 = UnsafeNativeMethods.GetSystemMetrics(79);
					int systemMetrics3 = UnsafeNativeMethods.GetSystemMetrics(76);
					int systemMetrics4 = UnsafeNativeMethods.GetSystemMetrics(77);
					x = (x - systemMetrics3) * 65536 / systemMetrics + 65536 / (systemMetrics * 2);
					y = (y - systemMetrics4) * 65536 / systemMetrics2 + 65536 / (systemMetrics2 * 2);
					flags |= 16384U;
				}
				NativeMethods.INPUT input = default(NativeMethods.INPUT);
				input.type = 0;
				input.inputUnion.mi.dx = x;
				input.inputUnion.mi.dy = y;
				input.inputUnion.mi.mouseData = 0;
				input.inputUnion.mi.dwFlags = (int)flags;
				input.inputUnion.mi.time = 0;
				input.inputUnion.mi.dwExtraInfo = IntPtr.Zero;
				UnsafeNativeMethods.SendInput(1U, new NativeMethods.INPUT[]
				{
					input
				}, Marshal.SizeOf(input));
			}

			// Token: 0x04003B89 RID: 15241
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006E4 RID: 1764
		internal class CalendarCellAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B6E RID: 27502 RVA: 0x0018E270 File Offset: 0x0018C470
			public CalendarCellAccessibleObject(MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject, MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex, int columnIndex) : base(monthCalendarAccessibleObject)
			{
				this._calendarRowAccessibleObject = calendarRowAccessibleObject;
				this._calendarBodyAccessibleObject = calendarBodyAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._rowIndex = rowIndex;
				this._columnIndex = columnIndex;
				this._initRuntimeId = new int[]
				{
					this._calendarRowAccessibleObject.RuntimeId[0],
					this._calendarRowAccessibleObject.RuntimeId[1],
					this._calendarRowAccessibleObject.RuntimeId[2],
					this._calendarRowAccessibleObject.RuntimeId[3],
					this._calendarRowAccessibleObject.RuntimeId[4],
					this.GetChildId()
				};
			}

			// Token: 0x1700175F RID: 5983
			// (get) Token: 0x06006B6F RID: 27503 RVA: 0x0018E316 File Offset: 0x0018C516
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(8U, this._calendarIndex, this._rowIndex, this._columnIndex);
				}
			}

			// Token: 0x17001760 RID: 5984
			// (get) Token: 0x06006B70 RID: 27504 RVA: 0x0018E33B File Offset: 0x0018C53B
			internal int CalendarIndex
			{
				get
				{
					return this._calendarIndex;
				}
			}

			// Token: 0x17001761 RID: 5985
			// (get) Token: 0x06006B71 RID: 27505 RVA: 0x0018E343 File Offset: 0x0018C543
			internal override int Column
			{
				get
				{
					return this._columnIndex;
				}
			}

			// Token: 0x17001762 RID: 5986
			// (get) Token: 0x06006B72 RID: 27506 RVA: 0x0018E34B File Offset: 0x0018C54B
			internal override UnsafeNativeMethods.IRawElementProviderSimple ContainingGrid
			{
				get
				{
					return this._calendarBodyAccessibleObject;
				}
			}

			// Token: 0x17001763 RID: 5987
			// (get) Token: 0x06006B73 RID: 27507 RVA: 0x0018E353 File Offset: 0x0018C553
			internal virtual SelectionRange DateRange
			{
				get
				{
					if (this._dateRange == null)
					{
						this._dateRange = this._monthCalendarAccessibleObject.GetCalendarPartDateRange(8U, this._calendarIndex, this._rowIndex, this._columnIndex);
					}
					return this._dateRange;
				}
			}

			// Token: 0x17001764 RID: 5988
			// (get) Token: 0x06006B74 RID: 27508 RVA: 0x0018E388 File Offset: 0x0018C588
			public override string Description
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH || this.DateRange == null)
					{
						return null;
					}
					DateTime start = this.DateRange.Start;
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					int weekOfYear = currentCulture.Calendar.GetWeekOfYear(start, currentCulture.DateTimeFormat.CalendarWeekRule, this._monthCalendarAccessibleObject.FirstDayOfWeek);
					return string.Format(SR.GetString("MonthCalendarWeekNumberDescription"), weekOfYear) + ", " + start.ToString("dddd", currentCulture);
				}
			}

			// Token: 0x06006B75 RID: 27509 RVA: 0x0018E41C File Offset: 0x0018C61C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return base.FragmentNavigate(direction);
					}
					if (this._columnIndex == 0)
					{
						return this._calendarRowAccessibleObject.WeekNumberCellAccessibleObject;
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode = cellsAccessibleObjects.Find(this);
					if (linkedListNode == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> previous = linkedListNode.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				else
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects2 = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode2 = cellsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> next = linkedListNode2.Next;
					if (next == null)
					{
						return null;
					}
					return next.Value;
				}
			}

			// Token: 0x06006B76 RID: 27510 RVA: 0x0018E4AA File Offset: 0x0018C6AA
			internal override int GetChildId()
			{
				return 1 + this._columnIndex;
			}

			// Token: 0x06006B77 RID: 27511 RVA: 0x0018E4B4 File Offset: 0x0018C6B4
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaderItems()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
				{
					return null;
				}
				LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this._calendarBodyAccessibleObject.RowsAccessibleObjects;
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject;
				if (rowsAccessibleObjects == null)
				{
					calendarRowAccessibleObject = null;
				}
				else
				{
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> first = rowsAccessibleObjects.First;
					calendarRowAccessibleObject = ((first != null) ? first.Value : null);
				}
				MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject2 = calendarRowAccessibleObject;
				if (calendarRowAccessibleObject2 == null || calendarRowAccessibleObject2.CellsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in calendarRowAccessibleObject2.CellsAccessibleObjects)
				{
					if (calendarCellAccessibleObject.Column == this._columnIndex)
					{
						return new UnsafeNativeMethods.IRawElementProviderSimple[]
						{
							calendarCellAccessibleObject
						};
					}
				}
				return null;
			}

			// Token: 0x06006B78 RID: 27512 RVA: 0x0018E56C File Offset: 0x0018C76C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30009)
				{
					if (propertyID == 30003)
					{
						return 50029;
					}
					if (propertyID == 30009)
					{
						return this.IsEnabled;
					}
				}
				else
				{
					if (propertyID == 30029)
					{
						return this.IsPatternSupported(10007);
					}
					if (propertyID == 30039)
					{
						return this.IsPatternSupported(10013);
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006B79 RID: 27513 RVA: 0x0018E5E8 File Offset: 0x0018C7E8
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaderItems()
			{
				AccessibleObject weekNumberCellAccessibleObject = this._calendarRowAccessibleObject.WeekNumberCellAccessibleObject;
				if (weekNumberCellAccessibleObject == null)
				{
					return null;
				}
				return new UnsafeNativeMethods.IRawElementProviderSimple[]
				{
					weekNumberCellAccessibleObject
				};
			}

			// Token: 0x17001765 RID: 5989
			// (get) Token: 0x06006B7A RID: 27514 RVA: 0x0018E610 File Offset: 0x0018C810
			internal override bool HasKeyboardFocus
			{
				get
				{
					return this._monthCalendarAccessibleObject.Focused && this._monthCalendarAccessibleObject.FocusedCell == this;
				}
			}

			// Token: 0x06006B7B RID: 27515 RVA: 0x0018E62F File Offset: 0x0018C82F
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10007 || patternId == 10013 || base.IsPatternSupported(patternId);
			}

			// Token: 0x17001766 RID: 5990
			// (get) Token: 0x06006B7C RID: 27516 RVA: 0x0018E650 File Offset: 0x0018C850
			public override string Name
			{
				get
				{
					if (this.DateRange == null)
					{
						return string.Empty;
					}
					switch (this._monthCalendarAccessibleObject.CalendarView)
					{
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH:
						return string.Format("{0:D}", this.DateRange.Start);
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_YEAR:
						return string.Format("{0:Y}", this.DateRange.Start);
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_DECADE:
						return string.Format("{0:yyy}", this.DateRange.Start);
					case NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_CENTURY:
						return string.Format("{0:yyy} - {1:yyy}", this.DateRange.Start, this.DateRange.End);
					default:
						return string.Empty;
					}
				}
			}

			// Token: 0x17001767 RID: 5991
			// (get) Token: 0x06006B7D RID: 27517 RVA: 0x0018E710 File Offset: 0x0018C910
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarRowAccessibleObject;
				}
			}

			// Token: 0x17001768 RID: 5992
			// (get) Token: 0x06006B7E RID: 27518 RVA: 0x00178958 File Offset: 0x00176B58
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Cell;
				}
			}

			// Token: 0x17001769 RID: 5993
			// (get) Token: 0x06006B7F RID: 27519 RVA: 0x0018E718 File Offset: 0x0018C918
			internal override int Row
			{
				get
				{
					return this._rowIndex;
				}
			}

			// Token: 0x1700176A RID: 5994
			// (get) Token: 0x06006B80 RID: 27520 RVA: 0x0018E720 File Offset: 0x0018C920
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x06006B81 RID: 27521 RVA: 0x0018E728 File Offset: 0x0018C928
			public override void Select(AccessibleSelection flags)
			{
				if (this.DateRange != null)
				{
					this._monthCalendarAccessibleObject.SetSelectionRange(this.DateRange.Start, this.DateRange.End);
				}
			}

			// Token: 0x1700176B RID: 5995
			// (get) Token: 0x06006B82 RID: 27522 RVA: 0x0018E754 File Offset: 0x0018C954
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Focusable | AccessibleStates.Selectable;
					if (this._monthCalendarAccessibleObject.Focused && this._monthCalendarAccessibleObject.FocusedCell == this)
					{
						return accessibleStates | AccessibleStates.Focused | AccessibleStates.Selected;
					}
					if (this.DateRange != null && this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH && this.DateRange.Start >= this._monthCalendarAccessibleObject.SelectionRange.Start && this.DateRange.End <= this._monthCalendarAccessibleObject.SelectionRange.End)
					{
						accessibleStates |= AccessibleStates.Selected;
					}
					return accessibleStates;
				}
			}

			// Token: 0x04003B8A RID: 15242
			private const int ChildIdIncrement = 1;

			// Token: 0x04003B8B RID: 15243
			private readonly MonthCalendar.CalendarRowAccessibleObject _calendarRowAccessibleObject;

			// Token: 0x04003B8C RID: 15244
			private readonly MonthCalendar.CalendarBodyAccessibleObject _calendarBodyAccessibleObject;

			// Token: 0x04003B8D RID: 15245
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B8E RID: 15246
			private readonly int _calendarIndex;

			// Token: 0x04003B8F RID: 15247
			private readonly int _rowIndex;

			// Token: 0x04003B90 RID: 15248
			private readonly int _columnIndex;

			// Token: 0x04003B91 RID: 15249
			private readonly int[] _initRuntimeId;

			// Token: 0x04003B92 RID: 15250
			private SelectionRange _dateRange;
		}

		// Token: 0x020006E5 RID: 1765
		internal class CalendarDayOfWeekCellAccessibleObject : MonthCalendar.CalendarCellAccessibleObject
		{
			// Token: 0x06006B83 RID: 27523 RVA: 0x0018E7E6 File Offset: 0x0018C9E6
			public CalendarDayOfWeekCellAccessibleObject(MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject, MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex, int columnIndex, string initName) : base(calendarRowAccessibleObject, calendarBodyAccessibleObject, monthCalendarAccessibleObject, calendarIndex, rowIndex, columnIndex)
			{
				this._calendarRowAccessibleObject = calendarRowAccessibleObject;
				this._initName = initName;
			}

			// Token: 0x1700176C RID: 5996
			// (get) Token: 0x06006B84 RID: 27524 RVA: 0x00015ECC File Offset: 0x000140CC
			internal override SelectionRange DateRange
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700176D RID: 5997
			// (get) Token: 0x06006B85 RID: 27525 RVA: 0x0017F055 File Offset: 0x0017D255
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x1700176E RID: 5998
			// (get) Token: 0x06006B86 RID: 27526 RVA: 0x00015ECC File Offset: 0x000140CC
			public override string Description
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006B87 RID: 27527 RVA: 0x0018E808 File Offset: 0x0018CA08
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return base.FragmentNavigate(direction);
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode = cellsAccessibleObjects.Find(this);
					if (linkedListNode == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> previous = linkedListNode.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				else
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects2 = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> linkedListNode2 = cellsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> next = linkedListNode2.Next;
					if (next == null)
					{
						return null;
					}
					return next.Value;
				}
			}

			// Token: 0x06006B88 RID: 27528 RVA: 0x0018E882 File Offset: 0x0018CA82
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50034;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return false;
			}

			// Token: 0x1700176F RID: 5999
			// (get) Token: 0x06006B89 RID: 27529 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal override bool HasKeyboardFocus
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006B8A RID: 27530 RVA: 0x000072B6 File Offset: 0x000054B6
			internal override void Invoke()
			{
			}

			// Token: 0x06006B8B RID: 27531 RVA: 0x0018E8AF File Offset: 0x0018CAAF
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId != 10000 && patternId != 10007 && patternId != 10013 && base.IsPatternSupported(patternId);
			}

			// Token: 0x17001770 RID: 6000
			// (get) Token: 0x06006B8C RID: 27532 RVA: 0x0018E8D8 File Offset: 0x0018CAD8
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x17001771 RID: 6001
			// (get) Token: 0x06006B8D RID: 27533 RVA: 0x00177CA4 File Offset: 0x00175EA4
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.ColumnHeader;
				}
			}

			// Token: 0x17001772 RID: 6002
			// (get) Token: 0x06006B8E RID: 27534 RVA: 0x00011A20 File Offset: 0x0000FC20
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.None;
				}
			}

			// Token: 0x04003B93 RID: 15251
			private readonly MonthCalendar.CalendarRowAccessibleObject _calendarRowAccessibleObject;

			// Token: 0x04003B94 RID: 15252
			private readonly string _initName;
		}

		// Token: 0x020006E6 RID: 1766
		internal class CalendarHeaderAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B8F RID: 27535 RVA: 0x0018E8E0 File Offset: 0x0018CAE0
			public CalendarHeaderAccessibleObject(MonthCalendar.CalendarAccessibleObject calendarAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex) : base(monthCalendarAccessibleObject)
			{
				this._calendarAccessibleObject = calendarAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._initName = this._monthCalendarAccessibleObject.GetCalendarPartText(5U, this._calendarIndex, 0, 0);
				this._initRuntimeId = new int[]
				{
					this._calendarAccessibleObject.RuntimeId[0],
					this._calendarAccessibleObject.RuntimeId[1],
					this._calendarAccessibleObject.RuntimeId[2],
					this.GetChildId()
				};
			}

			// Token: 0x17001773 RID: 6003
			// (get) Token: 0x06006B90 RID: 27536 RVA: 0x0018E968 File Offset: 0x0018CB68
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(5U, this._calendarIndex, 0, 0);
				}
			}

			// Token: 0x06006B91 RID: 27537 RVA: 0x0018E983 File Offset: 0x0018CB83
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					return this._calendarAccessibleObject.CalendarBodyAccessibleObject;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					return null;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06006B92 RID: 27538 RVA: 0x00013062 File Offset: 0x00011262
			internal override int GetChildId()
			{
				return 1;
			}

			// Token: 0x17001774 RID: 6004
			// (get) Token: 0x06006B93 RID: 27539 RVA: 0x0018E9A2 File Offset: 0x0018CBA2
			public override string Name
			{
				get
				{
					return this._initName;
				}
			}

			// Token: 0x17001775 RID: 6005
			// (get) Token: 0x06006B94 RID: 27540 RVA: 0x0018E9AA File Offset: 0x0018CBAA
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarAccessibleObject;
				}
			}

			// Token: 0x17001776 RID: 6006
			// (get) Token: 0x06006B95 RID: 27541 RVA: 0x0018E9B2 File Offset: 0x0018CBB2
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x04003B95 RID: 15253
			private const int ChildId = 1;

			// Token: 0x04003B96 RID: 15254
			private readonly MonthCalendar.CalendarAccessibleObject _calendarAccessibleObject;

			// Token: 0x04003B97 RID: 15255
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003B98 RID: 15256
			private readonly int _calendarIndex;

			// Token: 0x04003B99 RID: 15257
			private readonly string _initName;

			// Token: 0x04003B9A RID: 15258
			private readonly int[] _initRuntimeId;
		}

		// Token: 0x020006E7 RID: 1767
		internal class CalendarNextButtonAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B96 RID: 27542 RVA: 0x0018E9BA File Offset: 0x0018CBBA
			public CalendarNextButtonAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject) : base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x17001777 RID: 6007
			// (get) Token: 0x06006B97 RID: 27543 RVA: 0x0018E9CA File Offset: 0x0018CBCA
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(1U, 0, 0, 0);
				}
			}

			// Token: 0x17001778 RID: 6008
			// (get) Token: 0x06006B98 RID: 27544 RVA: 0x0018E9E0 File Offset: 0x0018CBE0
			public override string Description
			{
				get
				{
					return SR.GetString("CalendarNextButtonAccessibleObjectDescription");
				}
			}

			// Token: 0x06006B99 RID: 27545 RVA: 0x0018E9EC File Offset: 0x0018CBEC
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return this._monthCalendarAccessibleObject.PreviousButtonAccessibleObject;
					}
					return base.FragmentNavigate(direction);
				}
				else
				{
					LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
					if (calendarsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> first = calendarsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
			}

			// Token: 0x06006B9A RID: 27546 RVA: 0x0001627D File Offset: 0x0001447D
			internal override int GetChildId()
			{
				return 2;
			}

			// Token: 0x06006B9B RID: 27547 RVA: 0x0018EA2B File Offset: 0x0018CC2B
			internal override void Invoke()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.IsEnabled || !this.IsEnabled)
				{
					return;
				}
				base.Invoke();
				this._monthCalendarAccessibleObject.UpdateDisplayRange();
			}

			// Token: 0x17001779 RID: 6009
			// (get) Token: 0x06006B9C RID: 27548 RVA: 0x0018EA64 File Offset: 0x0018CC64
			internal override bool IsEnabled
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return false;
					}
					SelectionRange displayRange = this._monthCalendarAccessibleObject.GetDisplayRange(true);
					return displayRange != null && this._monthCalendarAccessibleObject.IsEnabled && this._monthCalendarAccessibleObject.MaxDate > displayRange.End;
				}
			}

			// Token: 0x1700177A RID: 6010
			// (get) Token: 0x06006B9D RID: 27549 RVA: 0x0018EAB5 File Offset: 0x0018CCB5
			public override string Name
			{
				get
				{
					return SR.GetString("MonthCalendarNextButtonAccessibleName");
				}
			}

			// Token: 0x04003B9B RID: 15259
			private const int ChildId = 2;

			// Token: 0x04003B9C RID: 15260
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006E8 RID: 1768
		internal class CalendarPreviousButtonAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006B9E RID: 27550 RVA: 0x0018EAC1 File Offset: 0x0018CCC1
			public CalendarPreviousButtonAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject) : base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x1700177B RID: 6011
			// (get) Token: 0x06006B9F RID: 27551 RVA: 0x0018EAD1 File Offset: 0x0018CCD1
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(2U, 0, 0, 0);
				}
			}

			// Token: 0x1700177C RID: 6012
			// (get) Token: 0x06006BA0 RID: 27552 RVA: 0x0018EAE7 File Offset: 0x0018CCE7
			public override string Description
			{
				get
				{
					return SR.GetString("CalendarPreviousButtonAccessibleObjectDescription");
				}
			}

			// Token: 0x06006BA1 RID: 27553 RVA: 0x0018EAF3 File Offset: 0x0018CCF3
			internal override void Invoke()
			{
				if (!this._monthCalendarAccessibleObject.IsHandleCreated || !this._monthCalendarAccessibleObject.IsEnabled || !this.IsEnabled)
				{
					return;
				}
				base.Invoke();
				this._monthCalendarAccessibleObject.UpdateDisplayRange();
			}

			// Token: 0x1700177D RID: 6013
			// (get) Token: 0x06006BA2 RID: 27554 RVA: 0x0018EB2C File Offset: 0x0018CD2C
			internal override bool IsEnabled
			{
				get
				{
					if (!this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						return false;
					}
					SelectionRange displayRange = this._monthCalendarAccessibleObject.GetDisplayRange(true);
					return displayRange != null && this._monthCalendarAccessibleObject.IsEnabled && this._monthCalendarAccessibleObject.MinDate < displayRange.Start;
				}
			}

			// Token: 0x06006BA3 RID: 27555 RVA: 0x0018EB7D File Offset: 0x0018CD7D
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					return this._monthCalendarAccessibleObject.NextButtonAccessibleObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06006BA4 RID: 27556 RVA: 0x00013062 File Offset: 0x00011262
			internal override int GetChildId()
			{
				return 1;
			}

			// Token: 0x1700177E RID: 6014
			// (get) Token: 0x06006BA5 RID: 27557 RVA: 0x0018EB96 File Offset: 0x0018CD96
			public override string Name
			{
				get
				{
					return SR.GetString("MonthCalendarPreviousButtonAccessibleName");
				}
			}

			// Token: 0x04003B9D RID: 15261
			private const int ChildId = 1;

			// Token: 0x04003B9E RID: 15262
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006E9 RID: 1769
		internal class CalendarRowAccessibleObject : MonthCalendar.MonthCalendarChildAccessibleObject
		{
			// Token: 0x06006BA6 RID: 27558 RVA: 0x0018EBA4 File Offset: 0x0018CDA4
			public CalendarRowAccessibleObject(MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex) : base(monthCalendarAccessibleObject)
			{
				this._calendarBodyAccessibleObject = calendarBodyAccessibleObject;
				this._monthCalendarAccessibleObject = monthCalendarAccessibleObject;
				this._calendarIndex = calendarIndex;
				this._rowIndex = rowIndex;
				this._initRuntimeId = new int[]
				{
					this._calendarBodyAccessibleObject.RuntimeId[0],
					this._calendarBodyAccessibleObject.RuntimeId[1],
					this._calendarBodyAccessibleObject.RuntimeId[2],
					this._calendarBodyAccessibleObject.RuntimeId[3],
					this.GetChildId()
				};
			}

			// Token: 0x1700177F RID: 6015
			// (get) Token: 0x06006BA7 RID: 27559 RVA: 0x0018EC2A File Offset: 0x0018CE2A
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(7U, this._calendarIndex, this._rowIndex, 0);
				}
			}

			// Token: 0x17001780 RID: 6016
			// (get) Token: 0x06006BA8 RID: 27560 RVA: 0x0018EC4C File Offset: 0x0018CE4C
			internal LinkedList<MonthCalendar.CalendarCellAccessibleObject> CellsAccessibleObjects
			{
				get
				{
					if (this._cellsAccessibleObjects == null && this._monthCalendarAccessibleObject.IsHandleCreated)
					{
						this._cellsAccessibleObjects = new LinkedList<MonthCalendar.CalendarCellAccessibleObject>();
						int num = 0;
						int num2 = (this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH) ? 7 : 4;
						for (int i = num; i < num2; i++)
						{
							string calendarPartText = this._monthCalendarAccessibleObject.GetCalendarPartText(8U, this._calendarIndex, this._rowIndex, i);
							if (!string.IsNullOrEmpty(calendarPartText))
							{
								MonthCalendar.CalendarCellAccessibleObject value = (this._rowIndex == -1) ? new MonthCalendar.CalendarDayOfWeekCellAccessibleObject(this, this._calendarBodyAccessibleObject, this._monthCalendarAccessibleObject, this._calendarIndex, this._rowIndex, i, calendarPartText) : new MonthCalendar.CalendarCellAccessibleObject(this, this._calendarBodyAccessibleObject, this._monthCalendarAccessibleObject, this._calendarIndex, this._rowIndex, i);
								this._cellsAccessibleObjects.AddLast(value);
							}
						}
					}
					return this._cellsAccessibleObjects;
				}
			}

			// Token: 0x06006BA9 RID: 27561 RVA: 0x0018ED27 File Offset: 0x0018CF27
			internal void ClearChildCollection()
			{
				this._cellsAccessibleObjects = null;
			}

			// Token: 0x06006BAA RID: 27562 RVA: 0x0018ED30 File Offset: 0x0018CF30
			internal void DisconnectChildren()
			{
				int num = UnsafeNativeMethods.UiaDisconnectProvider(this._weekNumberCellAccessibleObject);
				if (this._cellsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarCellAccessibleObject provider in this._cellsAccessibleObjects)
				{
					num = UnsafeNativeMethods.UiaDisconnectProvider(provider);
				}
			}

			// Token: 0x17001781 RID: 6017
			// (get) Token: 0x06006BAB RID: 27563 RVA: 0x0018ED98 File Offset: 0x0018CF98
			public override string Description
			{
				get
				{
					if (this._rowIndex == -1 || this._monthCalendarAccessibleObject.IsHandleCreated || this._monthCalendarAccessibleObject.CalendarView != NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
					{
						return null;
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this.CellsAccessibleObjects;
					MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject;
					if (cellsAccessibleObjects == null)
					{
						calendarCellAccessibleObject = null;
					}
					else
					{
						LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> first = cellsAccessibleObjects.First;
						calendarCellAccessibleObject = ((first != null) ? first.Value : null);
					}
					MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject2 = calendarCellAccessibleObject;
					if (calendarCellAccessibleObject2 == null || calendarCellAccessibleObject2.DateRange == null)
					{
						return null;
					}
					string weekNumber = this.GetWeekNumber(calendarCellAccessibleObject2.DateRange.Start);
					return string.Format(SR.GetString("MonthCalendarWeekNumberDescription"), weekNumber);
				}
			}

			// Token: 0x06006BAC RID: 27564 RVA: 0x0018EE18 File Offset: 0x0018D018
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				switch (direction)
				{
				case UnsafeNativeMethods.NavigateDirection.NextSibling:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = this._calendarBodyAccessibleObject.RowsAccessibleObjects;
					if (rowsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> linkedListNode = rowsAccessibleObjects.Find(this);
					if (linkedListNode == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> next = linkedListNode.Next;
					if (next == null)
					{
						return null;
					}
					return next.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.PreviousSibling:
				{
					LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects2 = this._calendarBodyAccessibleObject.RowsAccessibleObjects;
					if (rowsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> linkedListNode2 = rowsAccessibleObjects2.Find(this);
					if (linkedListNode2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarRowAccessibleObject> previous = linkedListNode2.Previous;
					if (previous == null)
					{
						return null;
					}
					return previous.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.FirstChild:
				{
					if (this._monthCalendarAccessibleObject.ShowWeekNumbers && this._rowIndex != -1)
					{
						return this.WeekNumberCellAccessibleObject;
					}
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> first = cellsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
				case UnsafeNativeMethods.NavigateDirection.LastChild:
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects2 = this.CellsAccessibleObjects;
					if (cellsAccessibleObjects2 == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> last = cellsAccessibleObjects2.Last;
					if (last == null)
					{
						return null;
					}
					return last.Value;
				}
				default:
					return base.FragmentNavigate(direction);
				}
			}

			// Token: 0x06006BAD RID: 27565 RVA: 0x0018EEFC File Offset: 0x0018D0FC
			internal override int GetChildId()
			{
				return 1 + this._rowIndex;
			}

			// Token: 0x06006BAE RID: 27566 RVA: 0x0018D86C File Offset: 0x0018BA6C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50033;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return this.IsEnabled;
			}

			// Token: 0x06006BAF RID: 27567 RVA: 0x0018EF08 File Offset: 0x0018D108
			private string GetWeekNumber(DateTime date)
			{
				return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, this._monthCalendarAccessibleObject.FirstDayOfWeek).ToString();
			}

			// Token: 0x17001782 RID: 6018
			// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x0018EF48 File Offset: 0x0018D148
			internal override bool HasKeyboardFocus
			{
				get
				{
					MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
					return this._monthCalendarAccessibleObject.Focused && focusedCell != null && focusedCell.CalendarIndex == this._calendarIndex && focusedCell.Row == this._rowIndex;
				}
			}

			// Token: 0x17001783 RID: 6019
			// (get) Token: 0x06006BB1 RID: 27569 RVA: 0x00015ECC File Offset: 0x000140CC
			public override string Name
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001784 RID: 6020
			// (get) Token: 0x06006BB2 RID: 27570 RVA: 0x0018EF8F File Offset: 0x0018D18F
			public override AccessibleObject Parent
			{
				get
				{
					return this._calendarBodyAccessibleObject;
				}
			}

			// Token: 0x17001785 RID: 6021
			// (get) Token: 0x06006BB3 RID: 27571 RVA: 0x001786EE File Offset: 0x001768EE
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.Row;
				}
			}

			// Token: 0x17001786 RID: 6022
			// (get) Token: 0x06006BB4 RID: 27572 RVA: 0x0018EF97 File Offset: 0x0018D197
			internal override int Row
			{
				get
				{
					return this._rowIndex;
				}
			}

			// Token: 0x17001787 RID: 6023
			// (get) Token: 0x06006BB5 RID: 27573 RVA: 0x0018EF9F File Offset: 0x0018D19F
			internal override int[] RuntimeId
			{
				get
				{
					return this._initRuntimeId;
				}
			}

			// Token: 0x06006BB6 RID: 27574 RVA: 0x0018EFA8 File Offset: 0x0018D1A8
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this._monthCalendarAccessibleObject.FocusedCell;
				if (focusedCell != null && focusedCell.CalendarIndex == this._calendarIndex && focusedCell.Row == this._rowIndex)
				{
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x17001788 RID: 6024
			// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x0018EFEC File Offset: 0x0018D1EC
			internal MonthCalendar.CalendarWeekNumberCellAccessibleObject WeekNumberCellAccessibleObject
			{
				get
				{
					if (this._monthCalendarAccessibleObject.ShowWeekNumbers && this._monthCalendarAccessibleObject.CalendarView == NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH)
					{
						LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this.CellsAccessibleObjects;
						if (((cellsAccessibleObjects != null) ? cellsAccessibleObjects.First : null) != null && this.CellsAccessibleObjects.First.Value.DateRange != null)
						{
							if (this._weekNumberCellAccessibleObject == null)
							{
								this._weekNumberCellAccessibleObject = new MonthCalendar.CalendarWeekNumberCellAccessibleObject(this, this._calendarBodyAccessibleObject, this._monthCalendarAccessibleObject, this._calendarIndex, this._rowIndex, -1, this.GetWeekNumber(this.CellsAccessibleObjects.First.Value.DateRange.Start));
							}
							return this._weekNumberCellAccessibleObject;
						}
					}
					return null;
				}
			}

			// Token: 0x04003B9F RID: 15263
			private const int ChildIdIncrement = 1;

			// Token: 0x04003BA0 RID: 15264
			private readonly MonthCalendar.CalendarBodyAccessibleObject _calendarBodyAccessibleObject;

			// Token: 0x04003BA1 RID: 15265
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;

			// Token: 0x04003BA2 RID: 15266
			private readonly int _calendarIndex;

			// Token: 0x04003BA3 RID: 15267
			private readonly int _rowIndex;

			// Token: 0x04003BA4 RID: 15268
			private readonly int[] _initRuntimeId;

			// Token: 0x04003BA5 RID: 15269
			private LinkedList<MonthCalendar.CalendarCellAccessibleObject> _cellsAccessibleObjects;

			// Token: 0x04003BA6 RID: 15270
			private MonthCalendar.CalendarWeekNumberCellAccessibleObject _weekNumberCellAccessibleObject;
		}

		// Token: 0x020006EA RID: 1770
		internal class CalendarTodayLinkAccessibleObject : MonthCalendar.CalendarButtonAccessibleObject
		{
			// Token: 0x06006BB8 RID: 27576 RVA: 0x0018F093 File Offset: 0x0018D293
			public CalendarTodayLinkAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject) : base(calendarAccessibleObject)
			{
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x17001789 RID: 6025
			// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x0018F0A3 File Offset: 0x0018D2A3
			public override Rectangle Bounds
			{
				get
				{
					return this._monthCalendarAccessibleObject.GetCalendarPartRectangle(3U, 0, 0, 0);
				}
			}

			// Token: 0x1700178A RID: 6026
			// (get) Token: 0x06006BBA RID: 27578 RVA: 0x0018F0B9 File Offset: 0x0018D2B9
			public override string Description
			{
				get
				{
					return SR.GetString("CalendarTodayLinkAccessibleObjectDescription");
				}
			}

			// Token: 0x06006BBB RID: 27579 RVA: 0x0018F0C5 File Offset: 0x0018D2C5
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					return base.FragmentNavigate(direction);
				}
				LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
				if (calendarsAccessibleObjects == null)
				{
					return null;
				}
				LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects.Last;
				if (last == null)
				{
					return null;
				}
				return last.Value;
			}

			// Token: 0x06006BBC RID: 27580 RVA: 0x0018F0F4 File Offset: 0x0018D2F4
			internal override int GetChildId()
			{
				LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this._monthCalendarAccessibleObject.CalendarsAccessibleObjects;
				int? num = 3 + ((calendarsAccessibleObjects != null) ? new int?(calendarsAccessibleObjects.Count) : null);
				if (num == null)
				{
					return -1;
				}
				return num.GetValueOrDefault();
			}

			// Token: 0x1700178B RID: 6027
			// (get) Token: 0x06006BBD RID: 27581 RVA: 0x0018F15C File Offset: 0x0018D35C
			public override string Name
			{
				get
				{
					return string.Format(SR.GetString("MonthCalendarTodayButtonAccessibleName"), this._monthCalendarAccessibleObject.TodayDate.ToShortDateString());
				}
			}

			// Token: 0x04003BA7 RID: 15271
			private const int ChildIdIncrement = 3;

			// Token: 0x04003BA8 RID: 15272
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}

		// Token: 0x020006EB RID: 1771
		internal class CalendarWeekNumberCellAccessibleObject : MonthCalendar.CalendarCellAccessibleObject
		{
			// Token: 0x06006BBE RID: 27582 RVA: 0x0018F18B File Offset: 0x0018D38B
			public CalendarWeekNumberCellAccessibleObject(MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject, MonthCalendar.CalendarBodyAccessibleObject calendarBodyAccessibleObject, MonthCalendar.MonthCalendarAccessibleObjectLevel5 monthCalendarAccessibleObject, int calendarIndex, int rowIndex, int columnIndex, string weekNumber) : base(calendarRowAccessibleObject, calendarBodyAccessibleObject, monthCalendarAccessibleObject, calendarIndex, rowIndex, columnIndex)
			{
				this._calendarRowAccessibleObject = calendarRowAccessibleObject;
				this._weekNumber = weekNumber;
			}

			// Token: 0x1700178C RID: 6028
			// (get) Token: 0x06006BBF RID: 27583 RVA: 0x00015ECC File Offset: 0x000140CC
			internal override SelectionRange DateRange
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700178D RID: 6029
			// (get) Token: 0x06006BC0 RID: 27584 RVA: 0x0017F055 File Offset: 0x0017D255
			public override string DefaultAction
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x1700178E RID: 6030
			// (get) Token: 0x06006BC1 RID: 27585 RVA: 0x00015ECC File Offset: 0x000140CC
			public override string Description
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06006BC2 RID: 27586 RVA: 0x0018F1AB File Offset: 0x0018D3AB
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction != UnsafeNativeMethods.NavigateDirection.NextSibling)
				{
					if (direction != UnsafeNativeMethods.NavigateDirection.PreviousSibling)
					{
						return base.FragmentNavigate(direction);
					}
					return null;
				}
				else
				{
					LinkedList<MonthCalendar.CalendarCellAccessibleObject> cellsAccessibleObjects = this._calendarRowAccessibleObject.CellsAccessibleObjects;
					if (cellsAccessibleObjects == null)
					{
						return null;
					}
					LinkedListNode<MonthCalendar.CalendarCellAccessibleObject> first = cellsAccessibleObjects.First;
					if (first == null)
					{
						return null;
					}
					return first.Value;
				}
			}

			// Token: 0x06006BC3 RID: 27587 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal override int GetChildId()
			{
				return 0;
			}

			// Token: 0x06006BC4 RID: 27588 RVA: 0x0018E882 File Offset: 0x0018CA82
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50034;
				}
				if (propertyID != 30009)
				{
					return base.GetPropertyValue(propertyID);
				}
				return false;
			}

			// Token: 0x1700178F RID: 6031
			// (get) Token: 0x06006BC5 RID: 27589 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal override bool HasKeyboardFocus
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006BC6 RID: 27590 RVA: 0x000072B6 File Offset: 0x000054B6
			internal override void Invoke()
			{
			}

			// Token: 0x06006BC7 RID: 27591 RVA: 0x0018E8AF File Offset: 0x0018CAAF
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId != 10000 && patternId != 10007 && patternId != 10013 && base.IsPatternSupported(patternId);
			}

			// Token: 0x17001790 RID: 6032
			// (get) Token: 0x06006BC8 RID: 27592 RVA: 0x0018F1E2 File Offset: 0x0018D3E2
			public override string Name
			{
				get
				{
					return string.Format(SR.GetString("MonthCalendarWeekNumberDescription"), this._weekNumber);
				}
			}

			// Token: 0x17001791 RID: 6033
			// (get) Token: 0x06006BC9 RID: 27593 RVA: 0x0018054C File Offset: 0x0017E74C
			public override AccessibleRole Role
			{
				get
				{
					return AccessibleRole.RowHeader;
				}
			}

			// Token: 0x17001792 RID: 6034
			// (get) Token: 0x06006BCA RID: 27594 RVA: 0x00011A20 File Offset: 0x0000FC20
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.None;
				}
			}

			// Token: 0x04003BA9 RID: 15273
			private const int ChildId = 0;

			// Token: 0x04003BAA RID: 15274
			private readonly MonthCalendar.CalendarRowAccessibleObject _calendarRowAccessibleObject;

			// Token: 0x04003BAB RID: 15275
			private readonly string _weekNumber;
		}

		// Token: 0x020006EC RID: 1772
		internal class MonthCalendarAccessibleObjectLevel5 : MonthCalendar.MonthCalendarAccessibleObject
		{
			// Token: 0x06006BCB RID: 27595 RVA: 0x0018F1F9 File Offset: 0x0018D3F9
			public MonthCalendarAccessibleObjectLevel5(MonthCalendar owner) : base(owner)
			{
				this.calendar.DisplayRangeChanged += this.OnMonthCalendarStateChanged;
				this.calendar.CalendarViewChanged += this.OnMonthCalendarStateChanged;
			}

			// Token: 0x17001793 RID: 6035
			// (get) Token: 0x06006BCC RID: 27596 RVA: 0x0018F230 File Offset: 0x0018D430
			internal LinkedList<MonthCalendar.CalendarAccessibleObject> CalendarsAccessibleObjects
			{
				get
				{
					if (!this.IsHandleCreated)
					{
						return null;
					}
					if (this._calendarsAccessibleObjects == null)
					{
						this._calendarsAccessibleObjects = new LinkedList<MonthCalendar.CalendarAccessibleObject>();
						string b = string.Empty;
						for (int i = 0; i < 12; i++)
						{
							string calendarPartText = this.GetCalendarPartText(5U, i, 0, 0);
							if (calendarPartText == string.Empty || calendarPartText == b)
							{
								break;
							}
							MonthCalendar.CalendarAccessibleObject value = new MonthCalendar.CalendarAccessibleObject(this, i, calendarPartText);
							this._calendarsAccessibleObjects.AddLast(value);
							b = calendarPartText;
						}
					}
					return this._calendarsAccessibleObjects;
				}
			}

			// Token: 0x06006BCD RID: 27597 RVA: 0x0018F2AC File Offset: 0x0018D4AC
			internal void DisconnectChildren()
			{
				int num = UnsafeNativeMethods.UiaDisconnectProvider(this._previousButtonAccessibleObject);
				num = UnsafeNativeMethods.UiaDisconnectProvider(this._nextButtonAccessibleObject);
				num = UnsafeNativeMethods.UiaDisconnectProvider(this._todayLinkAccessibleObject);
				num = UnsafeNativeMethods.UiaDisconnectProvider(this._focusedCellAccessibleObject);
				if (this._calendarsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this._calendarsAccessibleObjects)
				{
					calendarAccessibleObject.DisconnectChildren();
					num = UnsafeNativeMethods.UiaDisconnectProvider(calendarAccessibleObject);
				}
			}

			// Token: 0x06006BCE RID: 27598 RVA: 0x0018F340 File Offset: 0x0018D540
			private DayOfWeek CastDayToDayOfWeek(Day day)
			{
				switch (day)
				{
				case Day.Monday:
					return DayOfWeek.Monday;
				case Day.Tuesday:
					return DayOfWeek.Tuesday;
				case Day.Wednesday:
					return DayOfWeek.Wednesday;
				case Day.Thursday:
					return DayOfWeek.Thursday;
				case Day.Friday:
					return DayOfWeek.Friday;
				case Day.Saturday:
					return DayOfWeek.Saturday;
				case Day.Sunday:
					return DayOfWeek.Sunday;
				case Day.Default:
					return DayOfWeek.Sunday;
				default:
					return DayOfWeek.Sunday;
				}
			}

			// Token: 0x17001794 RID: 6036
			// (get) Token: 0x06006BCF RID: 27599 RVA: 0x0018F37B File Offset: 0x0018D57B
			internal NativeMethods.MONTCALENDAR_VIEW_MODE CalendarView
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return NativeMethods.MONTCALENDAR_VIEW_MODE.MCMV_MONTH;
					}
					return this.calendar.mcCurView;
				}
			}

			// Token: 0x17001795 RID: 6037
			// (get) Token: 0x06006BD0 RID: 27600 RVA: 0x0018F394 File Offset: 0x0018D594
			internal override int ColumnCount
			{
				get
				{
					if (!this.IsHandleCreated || this.CalendarsAccessibleObjects == null)
					{
						return -1;
					}
					LinkedListNode<MonthCalendar.CalendarAccessibleObject> first = this.CalendarsAccessibleObjects.First;
					int num = (first != null) ? first.Value.Bounds.Y : 0;
					int num2 = 0;
					foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
					{
						if (calendarAccessibleObject.Bounds.Y > num)
						{
							break;
						}
						num2++;
					}
					return num2;
				}
			}

			// Token: 0x06006BD1 RID: 27601 RVA: 0x0018F434 File Offset: 0x0018D634
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				if (!this.IsHandleCreated)
				{
					return base.ElementProviderFromPoint(x, y);
				}
				int num = (int)x;
				int num2 = (int)y;
				NativeMethods.MCHITTESTINFOLEVEL5 hitTestInfo = this.GetHitTestInfo(num, num2);
				int uHit = hitTestInfo.uHit;
				if (uHit <= 1048576)
				{
					if (uHit <= 65538)
					{
						if (uHit == 0)
						{
							return this;
						}
						if (uHit - 65536 > 2)
						{
							goto IL_1B4;
						}
						MonthCalendar.CalendarAccessibleObject calendarFromPoint = this.GetCalendarFromPoint(num, num2);
						return ((calendarFromPoint != null) ? calendarFromPoint.CalendarHeaderAccessibleObject : null) ?? this;
					}
					else
					{
						switch (uHit)
						{
						case 131072:
							return this.GetCalendarFromPoint(num, num2) ?? this;
						case 131073:
						case 131074:
						case 131075:
							break;
						case 131076:
						{
							LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this.CalendarsAccessibleObjects;
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject;
							if (calendarsAccessibleObjects == null)
							{
								calendarAccessibleObject = null;
							}
							else
							{
								LinkedListNode<MonthCalendar.CalendarAccessibleObject> first = calendarsAccessibleObjects.First;
								calendarAccessibleObject = ((first != null) ? first.Value : null);
							}
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject2 = calendarAccessibleObject;
							if (calendarAccessibleObject2 != null && calendarAccessibleObject2.Bounds.Contains(num, num2))
							{
								return calendarAccessibleObject2;
							}
							return this;
						}
						case 131077:
						{
							LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects2 = this.CalendarsAccessibleObjects;
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject3;
							if (calendarsAccessibleObjects2 == null)
							{
								calendarAccessibleObject3 = null;
							}
							else
							{
								LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects2.Last;
								calendarAccessibleObject3 = ((last != null) ? last.Value : null);
							}
							MonthCalendar.CalendarAccessibleObject calendarAccessibleObject4 = calendarAccessibleObject3;
							if (calendarAccessibleObject4 != null && calendarAccessibleObject4.Bounds.Contains(num, num2))
							{
								return calendarAccessibleObject4;
							}
							return this;
						}
						default:
							if (uHit == 196608)
							{
								return this.TodayLinkAccessibleObject;
							}
							if (uHit != 1048576)
							{
								goto IL_1B4;
							}
							return this;
						}
					}
				}
				else if (uHit <= 16908289)
				{
					if (uHit == 16777216 || uHit == 16842755)
					{
						return this.NextButtonAccessibleObject;
					}
					if (uHit != 16908289)
					{
						goto IL_1B4;
					}
				}
				else
				{
					if (uHit == 33554432 || uHit == 33619971)
					{
						return this.PreviousButtonAccessibleObject;
					}
					if (uHit != 33685505)
					{
						goto IL_1B4;
					}
				}
				MonthCalendar.CalendarAccessibleObject calendarFromPoint2 = this.GetCalendarFromPoint(num, num2);
				return ((calendarFromPoint2 != null) ? calendarFromPoint2.GetChildFromPoint(hitTestInfo) : null) ?? this;
				IL_1B4:
				return base.ElementProviderFromPoint(x, y);
			}

			// Token: 0x17001796 RID: 6038
			// (get) Token: 0x06006BD2 RID: 27602 RVA: 0x0018F5FD File Offset: 0x0018D7FD
			internal DayOfWeek FirstDayOfWeek
			{
				get
				{
					if (!base.IsOwnerControlDestroyed())
					{
						return this.CastDayToDayOfWeek(this.calendar.FirstDayOfWeek);
					}
					return DayOfWeek.Sunday;
				}
			}

			// Token: 0x17001797 RID: 6039
			// (get) Token: 0x06006BD3 RID: 27603 RVA: 0x0018F61A File Offset: 0x0018D81A
			internal bool Focused
			{
				get
				{
					return !base.IsOwnerControlDestroyed() && this.calendar.Focused;
				}
			}

			// Token: 0x17001798 RID: 6040
			// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x0018F631 File Offset: 0x0018D831
			internal MonthCalendar.CalendarCellAccessibleObject FocusedCell
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return null;
					}
					if (UnsafeNativeMethods.UiaClientsAreListening())
					{
						if (this._focusedCellAccessibleObject == null)
						{
							this._focusedCellAccessibleObject = this.GetCellByDate(this.calendar._focusedDate);
						}
						return this._focusedCellAccessibleObject;
					}
					return null;
				}
			}

			// Token: 0x06006BD5 RID: 27605 RVA: 0x0018F66C File Offset: 0x0018D86C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
				{
					return this.PreviousButtonAccessibleObject;
				}
				if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return base.FragmentNavigate(direction);
				}
				if (this.ShowToday)
				{
					return this.TodayLinkAccessibleObject;
				}
				LinkedList<MonthCalendar.CalendarAccessibleObject> calendarsAccessibleObjects = this.CalendarsAccessibleObjects;
				if (calendarsAccessibleObjects == null)
				{
					return null;
				}
				LinkedListNode<MonthCalendar.CalendarAccessibleObject> last = calendarsAccessibleObjects.Last;
				if (last == null)
				{
					return null;
				}
				return last.Value;
			}

			// Token: 0x06006BD6 RID: 27606 RVA: 0x0018F6C0 File Offset: 0x0018D8C0
			private MonthCalendar.CalendarAccessibleObject GetCalendarFromPoint(int x, int y)
			{
				if (!this.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					if (calendarAccessibleObject.Bounds.Contains(x, y))
					{
						return calendarAccessibleObject;
					}
				}
				return null;
			}

			// Token: 0x06006BD7 RID: 27607 RVA: 0x0018F738 File Offset: 0x0018D938
			internal SelectionRange GetCalendarPartDateRange(uint dwPart, int calendarIndex = 0, int rowIndex = 0, int columnIndex = 0)
			{
				if (!this.IsHandleCreated)
				{
					return null;
				}
				NativeMethods.MCGRIDINFO mcgridinfo = new NativeMethods.MCGRIDINFO
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCGRIDINFO)),
					dwFlags = 1,
					dwPart = dwPart,
					iCalendar = calendarIndex,
					iCol = columnIndex,
					iRow = rowIndex
				};
				if (!(UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4120, 0, ref mcgridinfo) != IntPtr.Zero))
				{
					return null;
				}
				return new SelectionRange(mcgridinfo.stStart, mcgridinfo.stEnd);
			}

			// Token: 0x06006BD8 RID: 27608 RVA: 0x0018F7E8 File Offset: 0x0018D9E8
			internal NativeMethods.RECT GetCalendarPartRectangle(uint dwPart, int calendarIndex = 0, int rowIndex = 0, int columnIndex = 0)
			{
				if (!this.IsHandleCreated)
				{
					return default(NativeMethods.RECT);
				}
				NativeMethods.MCGRIDINFO mcgridinfo = new NativeMethods.MCGRIDINFO
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCGRIDINFO)),
					dwFlags = 2,
					dwPart = dwPart,
					iCalendar = calendarIndex,
					iCol = columnIndex,
					iRow = rowIndex
				};
				bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4120, 0, ref mcgridinfo) != IntPtr.Zero;
				if (flag)
				{
					return this.calendar.RectangleToScreen(mcgridinfo.rc);
				}
				return default(NativeMethods.RECT);
			}

			// Token: 0x06006BD9 RID: 27609 RVA: 0x0018F8A8 File Offset: 0x0018DAA8
			internal unsafe string GetCalendarPartText(uint dwPart, int calendarIndex = 0, int rowIndex = 0, int columnIndex = 0)
			{
				if (!this.IsHandleCreated)
				{
					return string.Empty;
				}
				char[] array = new char[20];
				char[] array2;
				char* pszName;
				if ((array2 = array) == null || array2.Length == 0)
				{
					pszName = null;
				}
				else
				{
					pszName = &array2[0];
				}
				NativeMethods.MCGRIDINFO mcgridinfo = new NativeMethods.MCGRIDINFO
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCGRIDINFO)),
					dwFlags = 4,
					dwPart = dwPart,
					iCalendar = calendarIndex,
					iCol = columnIndex,
					iRow = rowIndex,
					pszName = pszName,
					cchName = (UIntPtr)((ulong)((long)array.Length)) - 1
				};
				bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4120, 0, ref mcgridinfo) != IntPtr.Zero;
				array2 = null;
				string text = string.Empty;
				foreach (char c in array)
				{
					if (c != '\0' && c != '‎')
					{
						text += c.ToString();
					}
				}
				return text;
			}

			// Token: 0x06006BDA RID: 27610 RVA: 0x0018F9BC File Offset: 0x0018DBBC
			private MonthCalendar.CalendarCellAccessibleObject GetCellByDate(DateTime date)
			{
				if (!this.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					if (calendarAccessibleObject.DateRange != null)
					{
						DateTime start = calendarAccessibleObject.DateRange.Start;
						DateTime end = calendarAccessibleObject.DateRange.End;
						if (!(date < start) && !(date > end))
						{
							LinkedList<MonthCalendar.CalendarRowAccessibleObject> rowsAccessibleObjects = calendarAccessibleObject.CalendarBodyAccessibleObject.RowsAccessibleObjects;
							if (rowsAccessibleObjects == null)
							{
								return null;
							}
							foreach (MonthCalendar.CalendarRowAccessibleObject calendarRowAccessibleObject in rowsAccessibleObjects)
							{
								if (calendarRowAccessibleObject.CellsAccessibleObjects == null)
								{
									return null;
								}
								foreach (MonthCalendar.CalendarCellAccessibleObject calendarCellAccessibleObject in calendarRowAccessibleObject.CellsAccessibleObjects)
								{
									SelectionRange dateRange = calendarCellAccessibleObject.DateRange;
									if (dateRange != null && date >= dateRange.Start && date <= dateRange.End)
									{
										return calendarCellAccessibleObject;
									}
								}
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06006BDB RID: 27611 RVA: 0x00015ECC File Offset: 0x000140CC
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetColumnHeaders()
			{
				return null;
			}

			// Token: 0x06006BDC RID: 27612 RVA: 0x0018FB58 File Offset: 0x0018DD58
			internal SelectionRange GetDisplayRange(bool visible)
			{
				if (!this.IsHandleCreated)
				{
					return null;
				}
				return this.calendar.GetDisplayRange(visible);
			}

			// Token: 0x06006BDD RID: 27613 RVA: 0x0018FB70 File Offset: 0x0018DD70
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				return this._focusedCellAccessibleObject;
			}

			// Token: 0x06006BDE RID: 27614 RVA: 0x0018FB70 File Offset: 0x0018DD70
			public override AccessibleObject GetFocused()
			{
				return this._focusedCellAccessibleObject;
			}

			// Token: 0x06006BDF RID: 27615 RVA: 0x0018FB78 File Offset: 0x0018DD78
			private NativeMethods.MCHITTESTINFOLEVEL5 GetHitTestInfo(int xScreen, int yScreen)
			{
				if (!this.IsHandleCreated)
				{
					return default(NativeMethods.MCHITTESTINFOLEVEL5);
				}
				Point pt = this.calendar.PointToClient(new Point(xScreen, yScreen));
				NativeMethods.MCHITTESTINFOLEVEL5 result = new NativeMethods.MCHITTESTINFOLEVEL5
				{
					cbSize = (uint)Marshal.SizeOf(typeof(NativeMethods.MCHITTESTINFOLEVEL5)),
					pt = pt
				};
				UnsafeNativeMethods.SendMessage(new HandleRef(this.calendar, this.calendar.Handle), 4110, 0, ref result);
				return result;
			}

			// Token: 0x06006BE0 RID: 27616 RVA: 0x0018FBF8 File Offset: 0x0018DDF8
			internal override UnsafeNativeMethods.IRawElementProviderSimple GetItem(int row, int column)
			{
				if (!this.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return null;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					if (calendarAccessibleObject.Row == row && calendarAccessibleObject.Column == column)
					{
						return calendarAccessibleObject;
					}
				}
				return null;
			}

			// Token: 0x06006BE1 RID: 27617 RVA: 0x0018FC70 File Offset: 0x0018DE70
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30030)
				{
					if (propertyID <= 30005)
					{
						if (propertyID != 30003)
						{
							if (propertyID == 30005)
							{
								return this.Name;
							}
						}
						else
						{
							if (base.IsOwnerControlDestroyed() || this.calendar.AccessibleRole == AccessibleRole.Default)
							{
								return 50001;
							}
							return base.GetPropertyValue(propertyID);
						}
					}
					else
					{
						if (propertyID == 30009)
						{
							return this.IsEnabled;
						}
						if (propertyID == 30030)
						{
							return this.IsPatternSupported(10006);
						}
					}
				}
				else if (propertyID <= 30043)
				{
					if (propertyID == 30038)
					{
						return this.IsPatternSupported(10012);
					}
					if (propertyID == 30043)
					{
						return this.IsPatternSupported(10002);
					}
				}
				else
				{
					if (propertyID == 30090)
					{
						return this.IsPatternSupported(10018);
					}
					if (propertyID == 30096)
					{
						return this.State;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006BE2 RID: 27618 RVA: 0x00015ECC File Offset: 0x000140CC
			internal override UnsafeNativeMethods.IRawElementProviderSimple[] GetRowHeaders()
			{
				return null;
			}

			// Token: 0x17001799 RID: 6041
			// (get) Token: 0x06006BE3 RID: 27619 RVA: 0x0018FD8B File Offset: 0x0018DF8B
			internal bool IsEnabled
			{
				get
				{
					return !base.IsOwnerControlDestroyed() && this.calendar.Enabled;
				}
			}

			// Token: 0x1700179A RID: 6042
			// (get) Token: 0x06006BE4 RID: 27620 RVA: 0x0018FDA2 File Offset: 0x0018DFA2
			internal bool IsHandleCreated
			{
				get
				{
					return !base.IsOwnerControlDestroyed() && this.calendar.IsHandleCreated;
				}
			}

			// Token: 0x06006BE5 RID: 27621 RVA: 0x0018FDBC File Offset: 0x0018DFBC
			internal int GetCalendarHandle()
			{
				if (!base.IsOwnerControlDestroyed())
				{
					return base.Owner.InternalHandle.ToInt32();
				}
				return 0;
			}

			// Token: 0x06006BE6 RID: 27622 RVA: 0x0018FDE8 File Offset: 0x0018DFE8
			internal override bool IsPatternSupported(int patternId)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return false;
				}
				if (patternId <= 10006)
				{
					if (patternId == 10002)
					{
						return true;
					}
					if (patternId == 10006)
					{
						return true;
					}
				}
				else
				{
					if (patternId == 10012)
					{
						return true;
					}
					if (patternId == 10018)
					{
						return true;
					}
				}
				return base.IsPatternSupported(patternId);
			}

			// Token: 0x1700179B RID: 6043
			// (get) Token: 0x06006BE7 RID: 27623 RVA: 0x0018FE3A File Offset: 0x0018E03A
			internal DateTime MinDate
			{
				get
				{
					if (!base.IsOwnerControlDestroyed())
					{
						return this.calendar.MinDate;
					}
					return DateTime.MinValue;
				}
			}

			// Token: 0x1700179C RID: 6044
			// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x0018FE55 File Offset: 0x0018E055
			internal DateTime MaxDate
			{
				get
				{
					if (!base.IsOwnerControlDestroyed())
					{
						return this.calendar.MaxDate;
					}
					return DateTime.MaxValue;
				}
			}

			// Token: 0x1700179D RID: 6045
			// (get) Token: 0x06006BE9 RID: 27625 RVA: 0x0018FE70 File Offset: 0x0018E070
			internal MonthCalendar.CalendarNextButtonAccessibleObject NextButtonAccessibleObject
			{
				get
				{
					if (this._nextButtonAccessibleObject == null)
					{
						this._nextButtonAccessibleObject = new MonthCalendar.CalendarNextButtonAccessibleObject(this);
					}
					return this._nextButtonAccessibleObject;
				}
			}

			// Token: 0x06006BEA RID: 27626 RVA: 0x0018FE8C File Offset: 0x0018E08C
			private void OnMonthCalendarStateChanged(object sender, EventArgs e)
			{
				this.RebuildAccessibilityTree();
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(20005);
			}

			// Token: 0x1700179E RID: 6046
			// (get) Token: 0x06006BEB RID: 27627 RVA: 0x0018FEAA File Offset: 0x0018E0AA
			internal MonthCalendar.CalendarPreviousButtonAccessibleObject PreviousButtonAccessibleObject
			{
				get
				{
					if (this._previousButtonAccessibleObject == null)
					{
						this._previousButtonAccessibleObject = new MonthCalendar.CalendarPreviousButtonAccessibleObject(this);
					}
					return this._previousButtonAccessibleObject;
				}
			}

			// Token: 0x06006BEC RID: 27628 RVA: 0x0018FEC6 File Offset: 0x0018E0C6
			internal void RaiseAutomationEventForChild(int automationEventId)
			{
				if (!this.IsHandleCreated)
				{
					return;
				}
				if (this._calendarsAccessibleObjects == null)
				{
					return;
				}
				this._focusedCellAccessibleObject = null;
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(automationEventId);
			}

			// Token: 0x06006BED RID: 27629 RVA: 0x0018FEF4 File Offset: 0x0018E0F4
			private void RebuildAccessibilityTree()
			{
				if (!this.IsHandleCreated || this.CalendarsAccessibleObjects == null)
				{
					return;
				}
				foreach (MonthCalendar.CalendarAccessibleObject calendarAccessibleObject in this.CalendarsAccessibleObjects)
				{
					calendarAccessibleObject.CalendarBodyAccessibleObject.ClearChildCollection();
				}
				this._calendarsAccessibleObjects = null;
				this._focusedCellAccessibleObject = null;
				if (this.CalendarsAccessibleObjects.Count > 0)
				{
					MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
					if (focusedCell == null)
					{
						return;
					}
					focusedCell.RaiseAutomationEvent(20005);
				}
			}

			// Token: 0x1700179F RID: 6047
			// (get) Token: 0x06006BEE RID: 27630 RVA: 0x0018FF90 File Offset: 0x0018E190
			internal override int RowCount
			{
				get
				{
					if (this.ColumnCount <= 0 || this.CalendarsAccessibleObjects == null)
					{
						return 0;
					}
					return (int)Math.Ceiling((double)this.CalendarsAccessibleObjects.Count / (double)this.ColumnCount);
				}
			}

			// Token: 0x170017A0 RID: 6048
			// (get) Token: 0x06006BEF RID: 27631 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal override UnsafeNativeMethods.RowOrColumnMajor RowOrColumnMajor
			{
				get
				{
					return UnsafeNativeMethods.RowOrColumnMajor.RowOrColumnMajor_RowMajor;
				}
			}

			// Token: 0x170017A1 RID: 6049
			// (get) Token: 0x06006BF0 RID: 27632 RVA: 0x0018FFBF File Offset: 0x0018E1BF
			internal SelectionRange SelectionRange
			{
				get
				{
					if (!base.IsOwnerControlDestroyed())
					{
						return this.calendar.SelectionRange;
					}
					return null;
				}
			}

			// Token: 0x06006BF1 RID: 27633 RVA: 0x0018FFD6 File Offset: 0x0018E1D6
			internal override void SetFocus()
			{
				MonthCalendar.CalendarCellAccessibleObject focusedCell = this.FocusedCell;
				if (focusedCell == null)
				{
					return;
				}
				focusedCell.RaiseAutomationEvent(20005);
			}

			// Token: 0x06006BF2 RID: 27634 RVA: 0x0018FFEE File Offset: 0x0018E1EE
			internal void SetSelectionRange(DateTime d1, DateTime d2)
			{
				if (this.IsHandleCreated)
				{
					this.calendar.SetSelectionRange(d1, d2);
				}
			}

			// Token: 0x170017A2 RID: 6050
			// (get) Token: 0x06006BF3 RID: 27635 RVA: 0x00190005 File Offset: 0x0018E205
			internal bool ShowToday
			{
				get
				{
					return !base.IsOwnerControlDestroyed() && this.calendar.ShowToday;
				}
			}

			// Token: 0x170017A3 RID: 6051
			// (get) Token: 0x06006BF4 RID: 27636 RVA: 0x0019001C File Offset: 0x0018E21C
			internal bool ShowWeekNumbers
			{
				get
				{
					return !base.IsOwnerControlDestroyed() && this.calendar.ShowWeekNumbers;
				}
			}

			// Token: 0x170017A4 RID: 6052
			// (get) Token: 0x06006BF5 RID: 27637 RVA: 0x00190033 File Offset: 0x0018E233
			internal DateTime TodayDate
			{
				get
				{
					if (!base.IsOwnerControlDestroyed())
					{
						return this.calendar.TodayDate;
					}
					return DateTime.Today;
				}
			}

			// Token: 0x170017A5 RID: 6053
			// (get) Token: 0x06006BF6 RID: 27638 RVA: 0x0019004E File Offset: 0x0018E24E
			internal MonthCalendar.CalendarTodayLinkAccessibleObject TodayLinkAccessibleObject
			{
				get
				{
					if (this._todayLinkAccessibleObject == null)
					{
						this._todayLinkAccessibleObject = new MonthCalendar.CalendarTodayLinkAccessibleObject(this);
					}
					return this._todayLinkAccessibleObject;
				}
			}

			// Token: 0x06006BF7 RID: 27639 RVA: 0x0019006A File Offset: 0x0018E26A
			internal void UpdateDisplayRange()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				this.calendar.UpdateDisplayRange();
			}

			// Token: 0x04003BAC RID: 15276
			private const int MaxCalendarsCount = 12;

			// Token: 0x04003BAD RID: 15277
			private MonthCalendar.CalendarCellAccessibleObject _focusedCellAccessibleObject;

			// Token: 0x04003BAE RID: 15278
			private MonthCalendar.CalendarPreviousButtonAccessibleObject _previousButtonAccessibleObject;

			// Token: 0x04003BAF RID: 15279
			private MonthCalendar.CalendarNextButtonAccessibleObject _nextButtonAccessibleObject;

			// Token: 0x04003BB0 RID: 15280
			private LinkedList<MonthCalendar.CalendarAccessibleObject> _calendarsAccessibleObjects;

			// Token: 0x04003BB1 RID: 15281
			private MonthCalendar.CalendarTodayLinkAccessibleObject _todayLinkAccessibleObject;
		}

		// Token: 0x020006ED RID: 1773
		internal abstract class MonthCalendarChildAccessibleObject : AccessibleObject
		{
			// Token: 0x06006BF8 RID: 27640 RVA: 0x00190080 File Offset: 0x0018E280
			public MonthCalendarChildAccessibleObject(MonthCalendar.MonthCalendarAccessibleObjectLevel5 calendarAccessibleObject)
			{
				if (calendarAccessibleObject == null)
				{
					throw new ArgumentNullException();
				}
				this._monthCalendarAccessibleObject = calendarAccessibleObject;
			}

			// Token: 0x06006BF9 RID: 27641 RVA: 0x00190098 File Offset: 0x0018E298
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID <= 30090)
				{
					switch (propertyID)
					{
					case 30005:
						return this.Name;
					case 30006:
					case 30007:
						break;
					case 30008:
						return this.HasKeyboardFocus;
					case 30009:
						return false;
					case 30010:
						return this.IsEnabled;
					default:
						if (propertyID == 30090)
						{
							return this.IsPatternSupported(10018);
						}
						break;
					}
				}
				else
				{
					if (propertyID == 30095)
					{
						return this.Role;
					}
					if (propertyID == 30096)
					{
						return this.State;
					}
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x170017A6 RID: 6054
			// (get) Token: 0x06006BFA RID: 27642 RVA: 0x00011A20 File Offset: 0x0000FC20
			internal virtual bool HasKeyboardFocus
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170017A7 RID: 6055
			// (get) Token: 0x06006BFB RID: 27643 RVA: 0x00190143 File Offset: 0x0018E343
			internal virtual bool IsEnabled
			{
				get
				{
					return this._monthCalendarAccessibleObject.IsEnabled;
				}
			}

			// Token: 0x06006BFC RID: 27644 RVA: 0x00190150 File Offset: 0x0018E350
			internal override bool IsPatternSupported(int patternId)
			{
				return patternId == 10018 || base.IsPatternSupported(patternId);
			}

			// Token: 0x170017A8 RID: 6056
			// (get) Token: 0x06006BFD RID: 27645 RVA: 0x00190163 File Offset: 0x0018E363
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					return this._monthCalendarAccessibleObject;
				}
			}

			// Token: 0x06006BFE RID: 27646 RVA: 0x0019016B File Offset: 0x0018E36B
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction == UnsafeNativeMethods.NavigateDirection.Parent)
				{
					return this.Parent;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x170017A9 RID: 6057
			// (get) Token: 0x06006BFF RID: 27647 RVA: 0x0019017E File Offset: 0x0018E37E
			internal override int[] RuntimeId
			{
				get
				{
					return new int[]
					{
						42,
						this._monthCalendarAccessibleObject.GetCalendarHandle(),
						this.GetChildId()
					};
				}
			}

			// Token: 0x170017AA RID: 6058
			// (get) Token: 0x06006C00 RID: 27648 RVA: 0x00011A20 File Offset: 0x0000FC20
			public override AccessibleStates State
			{
				get
				{
					return AccessibleStates.None;
				}
			}

			// Token: 0x04003BB2 RID: 15282
			private readonly MonthCalendar.MonthCalendarAccessibleObjectLevel5 _monthCalendarAccessibleObject;
		}
	}
}
