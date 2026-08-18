using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200014C RID: 332
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Checked")]
	[DefaultEvent("CheckedChanged")]
	[DefaultBindingProperty("CheckState")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionCheckBox")]
	public class CheckBox : ButtonBase
	{
		// Token: 0x06000CDF RID: 3295 RVA: 0x00024EB8 File Offset: 0x000230B8
		public CheckBox()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(25);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
			this.autoCheck = true;
			this.TextAlign = ContentAlignment.MiddleLeft;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00024F28 File Offset: 0x00023128
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00024F30 File Offset: 0x00023130
		private bool AccObjDoDefaultAction
		{
			get
			{
				return this.accObjDoDefaultAction;
			}
			set
			{
				this.accObjDoDefaultAction = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00024F39 File Offset: 0x00023139
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00024F44 File Offset: 0x00023144
		[DefaultValue(Appearance.Normal)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("CheckBoxAppearanceDescr")]
		public Appearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Appearance));
				}
				if (this.appearance != value)
				{
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Appearance))
					{
						this.appearance = value;
						if (base.OwnerDraw)
						{
							this.Refresh();
						}
						else
						{
							base.UpdateStyles();
						}
						this.OnAppearanceChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06000CE4 RID: 3300 RVA: 0x00024FDC File Offset: 0x000231DC
		// (remove) Token: 0x06000CE5 RID: 3301 RVA: 0x00024FEF File Offset: 0x000231EF
		[SRCategory("CatPropertyChanged")]
		[SRDescription("CheckBoxOnAppearanceChangedDescr")]
		public event EventHandler AppearanceChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EVENT_APPEARANCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EVENT_APPEARANCECHANGED, value);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00025002 File Offset: 0x00023202
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x0002500A File Offset: 0x0002320A
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("CheckBoxAutoCheckDescr")]
		public bool AutoCheck
		{
			get
			{
				return this.autoCheck;
			}
			set
			{
				this.autoCheck = value;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00025013 File Offset: 0x00023213
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0002501C File Offset: 0x0002321C
		[Bindable(true)]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		[SRDescription("CheckBoxCheckAlignDescr")]
		public ContentAlignment CheckAlign
		{
			get
			{
				return this.checkAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.checkAlign != value)
				{
					this.checkAlign = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.CheckAlign);
					if (base.OwnerDraw)
					{
						base.Invalidate();
						return;
					}
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00025083 File Offset: 0x00023283
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0002508E File Offset: 0x0002328E
		[Bindable(true)]
		[SettingsBindable(true)]
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.checkState > CheckState.Unchecked;
			}
			set
			{
				if (value != this.Checked)
				{
					this.CheckState = (value ? CheckState.Checked : CheckState.Unchecked);
				}
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x000250A6 File Offset: 0x000232A6
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x000250B0 File Offset: 0x000232B0
		[Bindable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(CheckState.Unchecked)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("CheckBoxCheckStateDescr")]
		public CheckState CheckState
		{
			get
			{
				return this.checkState;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(CheckState));
				}
				if (this.checkState != value)
				{
					bool @checked = this.Checked;
					this.checkState = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(241, (int)this.checkState, 0);
					}
					if (@checked != this.Checked)
					{
						this.OnCheckedChanged(EventArgs.Empty);
					}
					this.OnCheckStateChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06000CEE RID: 3310 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x06000CEF RID: 3311 RVA: 0x000238FC File Offset: 0x00021AFC
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

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06000CF0 RID: 3312 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x06000CF1 RID: 3313 RVA: 0x0002390E File Offset: 0x00021B0E
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

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00025134 File Offset: 0x00023334
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "BUTTON";
				if (base.OwnerDraw)
				{
					createParams.Style |= 11;
				}
				else
				{
					createParams.Style |= 5;
					if (this.Appearance == Appearance.Button)
					{
						createParams.Style |= 4096;
					}
					ContentAlignment contentAlignment = base.RtlTranslateContent(this.CheckAlign);
					if ((contentAlignment & CheckBox.anyRight) != (ContentAlignment)0)
					{
						createParams.Style |= 32;
					}
				}
				return createParams;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000251BB File Offset: 0x000233BB
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, 24);
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000251C6 File Offset: 0x000233C6
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(25);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000251F4 File Offset: 0x000233F4
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			if (this.Appearance == Appearance.Button)
			{
				ButtonStandardAdapter buttonStandardAdapter = new ButtonStandardAdapter(this);
				return buttonStandardAdapter.GetPreferredSizeCore(proposedConstraints);
			}
			if (base.FlatStyle != FlatStyle.System)
			{
				return base.GetPreferredSizeCore(proposedConstraints);
			}
			Size clientSize = TextRenderer.MeasureText(this.Text, this.Font);
			Size sz = this.SizeFromClientSize(clientSize);
			sz.Width += this.flatSystemStylePaddingWidth;
			sz.Height = (DpiHelper.EnableDpiChangedHighDpiImprovements ? Math.Max(sz.Height + 5, this.flatSystemStyleMinimumHeight) : (sz.Height + 5));
			return sz + base.Padding.Size;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00025299 File Offset: 0x00023499
		internal override Rectangle OverChangeRectangle
		{
			get
			{
				if (this.Appearance == Appearance.Button)
				{
					return base.OverChangeRectangle;
				}
				if (base.FlatStyle == FlatStyle.Standard)
				{
					return new Rectangle(-1, -1, 1, 1);
				}
				return base.Adapter.CommonLayout().Layout().checkBounds;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x000252D3 File Offset: 0x000234D3
		internal override Rectangle DownChangeRectangle
		{
			get
			{
				if (this.Appearance == Appearance.Button || base.FlatStyle == FlatStyle.System)
				{
					return base.DownChangeRectangle;
				}
				return base.Adapter.CommonLayout().Layout().checkBounds;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x00025303 File Offset: 0x00023503
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0002530B File Offset: 0x0002350B
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		public override ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00025314 File Offset: 0x00023514
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0002531C File Offset: 0x0002351C
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("CheckBoxThreeStateDescr")]
		public bool ThreeState
		{
			get
			{
				return this.threeState;
			}
			set
			{
				this.threeState = value;
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06000CFC RID: 3324 RVA: 0x00025325 File Offset: 0x00023525
		// (remove) Token: 0x06000CFD RID: 3325 RVA: 0x00025338 File Offset: 0x00023538
		[SRDescription("CheckBoxOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EVENT_CHECKEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EVENT_CHECKEDCHANGED, value);
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06000CFE RID: 3326 RVA: 0x0002534B File Offset: 0x0002354B
		// (remove) Token: 0x06000CFF RID: 3327 RVA: 0x0002535E File Offset: 0x0002355E
		[SRDescription("CheckBoxOnCheckStateChangedDescr")]
		public event EventHandler CheckStateChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.EVENT_CHECKSTATECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.EVENT_CHECKSTATECHANGED, value);
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00025371 File Offset: 0x00023571
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new CheckBox.CheckBoxAccessibleObject(this);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002537C File Offset: 0x0002357C
		protected virtual void OnAppearanceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[CheckBox.EVENT_APPEARANCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x000253AC File Offset: 0x000235AC
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			if (base.FlatStyle == FlatStyle.System)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemCaptureStart, -1);
			}
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
			if (base.FlatStyle == FlatStyle.System)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.SystemCaptureEnd, -1);
			}
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.EVENT_CHECKEDCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00025418 File Offset: 0x00023618
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			if (base.OwnerDraw)
			{
				this.Refresh();
			}
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.EVENT_CHECKSTATECHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00025454 File Offset: 0x00023654
		protected override void OnClick(EventArgs e)
		{
			if (this.autoCheck)
			{
				CheckState checkState = this.CheckState;
				if (checkState != CheckState.Unchecked)
				{
					if (checkState != CheckState.Checked)
					{
						this.CheckState = CheckState.Unchecked;
					}
					else if (this.threeState)
					{
						this.CheckState = CheckState.Indeterminate;
						if (this.AccObjDoDefaultAction)
						{
							base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
						}
					}
					else
					{
						this.CheckState = CheckState.Unchecked;
					}
				}
				else
				{
					this.CheckState = CheckState.Checked;
				}
			}
			base.OnClick(e);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x000254BE File Offset: 0x000236BE
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (base.IsHandleCreated)
			{
				base.SendMessage(241, (int)this.checkState, 0);
			}
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000254E2 File Offset: 0x000236E2
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000254EC File Offset: 0x000236EC
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left && base.MouseIsPressed && base.MouseIsDown)
			{
				Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
				if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						if (base.Capture)
						{
							this.OnClick(mevent);
						}
						this.OnMouseClick(mevent);
					}
				}
			}
			base.OnMouseUp(mevent);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00025579 File Offset: 0x00023779
		internal override ButtonBaseAdapter CreateFlatAdapter()
		{
			return new CheckBoxFlatAdapter(this);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00025581 File Offset: 0x00023781
		internal override ButtonBaseAdapter CreatePopupAdapter()
		{
			return new CheckBoxPopupAdapter(this);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00025589 File Offset: 0x00023789
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new CheckBoxStandardAdapter(this);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00025594 File Offset: 0x00023794
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && Control.IsMnemonic(charCode, this.Text) && base.CanSelect)
			{
				if (this.FocusInternal())
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						this.OnClick(EventArgs.Empty);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000255E4 File Offset: 0x000237E4
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", CheckState: " + ((int)this.CheckState).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x04000766 RID: 1894
		private static readonly object EVENT_CHECKEDCHANGED = new object();

		// Token: 0x04000767 RID: 1895
		private static readonly object EVENT_CHECKSTATECHANGED = new object();

		// Token: 0x04000768 RID: 1896
		private static readonly object EVENT_APPEARANCECHANGED = new object();

		// Token: 0x04000769 RID: 1897
		private static readonly ContentAlignment anyRight = (ContentAlignment)1092;

		// Token: 0x0400076A RID: 1898
		private bool autoCheck;

		// Token: 0x0400076B RID: 1899
		private bool threeState;

		// Token: 0x0400076C RID: 1900
		private bool accObjDoDefaultAction;

		// Token: 0x0400076D RID: 1901
		private ContentAlignment checkAlign = ContentAlignment.MiddleLeft;

		// Token: 0x0400076E RID: 1902
		private CheckState checkState;

		// Token: 0x0400076F RID: 1903
		private Appearance appearance;

		// Token: 0x04000770 RID: 1904
		private const int FlatSystemStylePaddingWidth = 25;

		// Token: 0x04000771 RID: 1905
		private const int FlatSystemStyleMinimumHeight = 13;

		// Token: 0x04000772 RID: 1906
		internal int flatSystemStylePaddingWidth = 25;

		// Token: 0x04000773 RID: 1907
		internal int flatSystemStyleMinimumHeight = 13;

		// Token: 0x0200061D RID: 1565
		[ComVisible(true)]
		public class CheckBoxAccessibleObject : ButtonBase.ButtonBaseAccessibleObject
		{
			// Token: 0x06006308 RID: 25352 RVA: 0x0016E643 File Offset: 0x0016C843
			public CheckBoxAccessibleObject(Control owner) : base(owner)
			{
			}

			// Token: 0x17001519 RID: 5401
			// (get) Token: 0x06006309 RID: 25353 RVA: 0x0016E64C File Offset: 0x0016C84C
			public override string DefaultAction
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return string.Empty;
					}
					string accessibleDefaultActionDescription = base.Owner.AccessibleDefaultActionDescription;
					if (accessibleDefaultActionDescription != null)
					{
						return accessibleDefaultActionDescription;
					}
					if (((CheckBox)base.Owner).Checked)
					{
						return SR.GetString("AccessibleActionUncheck");
					}
					return SR.GetString("AccessibleActionCheck");
				}
			}

			// Token: 0x1700151A RID: 5402
			// (get) Token: 0x0600630A RID: 25354 RVA: 0x0016E6A0 File Offset: 0x0016C8A0
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.CheckButton;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.CheckButton;
				}
			}

			// Token: 0x1700151B RID: 5403
			// (get) Token: 0x0600630B RID: 25355 RVA: 0x0016E6CC File Offset: 0x0016C8CC
			public override AccessibleStates State
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleStates.None;
					}
					CheckState checkState = ((CheckBox)base.Owner).CheckState;
					if (checkState == CheckState.Checked)
					{
						return AccessibleStates.Checked | base.State;
					}
					if (checkState != CheckState.Indeterminate)
					{
						return base.State;
					}
					return AccessibleStates.Mixed | base.State;
				}
			}

			// Token: 0x0600630C RID: 25356 RVA: 0x0016E718 File Offset: 0x0016C918
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				CheckBox checkBox = base.Owner as CheckBox;
				if (checkBox != null)
				{
					checkBox.AccObjDoDefaultAction = true;
				}
				try
				{
					base.DoDefaultAction();
				}
				finally
				{
					if (checkBox != null)
					{
						checkBox.AccObjDoDefaultAction = false;
					}
				}
			}
		}
	}
}
