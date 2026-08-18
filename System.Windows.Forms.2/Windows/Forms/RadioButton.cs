using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200033E RID: 830
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Checked")]
	[DefaultEvent("CheckedChanged")]
	[DefaultBindingProperty("Checked")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("System.Windows.Forms.Design.RadioButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionRadioButton")]
	public class RadioButton : ButtonBase
	{
		// Token: 0x0600358C RID: 13708 RVA: 0x000F2984 File Offset: 0x000F0B84
		public RadioButton()
		{
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(24);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
			base.SetStyle(ControlStyles.StandardClick, false);
			this.TextAlign = ContentAlignment.MiddleLeft;
			this.TabStop = false;
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000F2A02 File Offset: 0x000F0C02
		// (set) Token: 0x0600358E RID: 13710 RVA: 0x000F2A0A File Offset: 0x000F0C0A
		[DefaultValue(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("RadioButtonAutoCheckDescr")]
		public bool AutoCheck
		{
			get
			{
				return this.autoCheck;
			}
			set
			{
				if (this.autoCheck != value)
				{
					this.autoCheck = value;
					this.PerformAutoUpdates(false);
				}
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000F2A23 File Offset: 0x000F0C23
		// (set) Token: 0x06003590 RID: 13712 RVA: 0x000F2A2C File Offset: 0x000F0C2C
		[DefaultValue(Appearance.Normal)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("RadioButtonAppearanceDescr")]
		public Appearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (this.appearance != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(Appearance));
					}
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

		// Token: 0x14000285 RID: 645
		// (add) Token: 0x06003591 RID: 13713 RVA: 0x000F2AC4 File Offset: 0x000F0CC4
		// (remove) Token: 0x06003592 RID: 13714 RVA: 0x000F2AD7 File Offset: 0x000F0CD7
		[SRCategory("CatPropertyChanged")]
		[SRDescription("RadioButtonOnAppearanceChangedDescr")]
		public event EventHandler AppearanceChanged
		{
			add
			{
				base.Events.AddHandler(RadioButton.EVENT_APPEARANCECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadioButton.EVENT_APPEARANCECHANGED, value);
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000F2AEA File Offset: 0x000F0CEA
		// (set) Token: 0x06003594 RID: 13716 RVA: 0x000F2AF2 File Offset: 0x000F0CF2
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		[SRDescription("RadioButtonCheckAlignDescr")]
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
				this.checkAlign = value;
				if (base.OwnerDraw)
				{
					base.Invalidate();
					return;
				}
				base.UpdateStyles();
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000F2B2E File Offset: 0x000F0D2E
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x000F2B38 File Offset: 0x000F0D38
		[Bindable(true)]
		[SettingsBindable(true)]
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("RadioButtonCheckedDescr")]
		public bool Checked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				if (this.isChecked != value)
				{
					this.isChecked = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(241, value ? 1 : 0, 0);
					}
					base.Invalidate();
					base.Update();
					this.PerformAutoUpdates(false);
					this.OnCheckedChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x14000286 RID: 646
		// (add) Token: 0x06003597 RID: 13719 RVA: 0x000238F3 File Offset: 0x00021AF3
		// (remove) Token: 0x06003598 RID: 13720 RVA: 0x000238FC File Offset: 0x00021AFC
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

		// Token: 0x14000287 RID: 647
		// (add) Token: 0x06003599 RID: 13721 RVA: 0x00023905 File Offset: 0x00021B05
		// (remove) Token: 0x0600359A RID: 13722 RVA: 0x0002390E File Offset: 0x00021B0E
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

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000F2B90 File Offset: 0x000F0D90
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
					createParams.Style |= 4;
					if (this.Appearance == Appearance.Button)
					{
						createParams.Style |= 4096;
					}
					ContentAlignment contentAlignment = base.RtlTranslateContent(this.CheckAlign);
					if ((contentAlignment & RadioButton.anyRight) != (ContentAlignment)0)
					{
						createParams.Style |= 32;
					}
				}
				return createParams;
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x0600359C RID: 13724 RVA: 0x000251BB File Offset: 0x000233BB
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, 24);
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000F2C17 File Offset: 0x000F0E17
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.flatSystemStylePaddingWidth = base.LogicalToDeviceUnits(24);
				this.flatSystemStyleMinimumHeight = base.LogicalToDeviceUnits(13);
			}
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000F2C44 File Offset: 0x000F0E44
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			if (base.FlatStyle != FlatStyle.System)
			{
				return base.GetPreferredSizeCore(proposedConstraints);
			}
			Size clientSize = TextRenderer.MeasureText(this.Text, this.Font);
			Size result = this.SizeFromClientSize(clientSize);
			result.Width += this.flatSystemStylePaddingWidth;
			result.Height = (DpiHelper.EnableDpiChangedHighDpiImprovements ? Math.Max(result.Height + 5, this.flatSystemStyleMinimumHeight) : (result.Height + 5));
			return result;
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000F2CBE File Offset: 0x000F0EBE
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

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060035A0 RID: 13728 RVA: 0x000F2CF8 File Offset: 0x000F0EF8
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

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x060035A2 RID: 13730 RVA: 0x000B2619 File Offset: 0x000B0819
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

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x00025303 File Offset: 0x00023503
		// (set) Token: 0x060035A4 RID: 13732 RVA: 0x0002530B File Offset: 0x0002350B
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

		// Token: 0x14000288 RID: 648
		// (add) Token: 0x060035A5 RID: 13733 RVA: 0x000F2D28 File Offset: 0x000F0F28
		// (remove) Token: 0x060035A6 RID: 13734 RVA: 0x000F2D3B File Offset: 0x000F0F3B
		[SRDescription("RadioButtonOnCheckedChangedDescr")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(RadioButton.EVENT_CHECKEDCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadioButton.EVENT_CHECKEDCHANGED, value);
			}
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000F2D4E File Offset: 0x000F0F4E
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new RadioButton.RadioButtonAccessibleObject(this);
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000F2D56 File Offset: 0x000F0F56
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (base.IsHandleCreated)
			{
				base.SendMessage(241, this.isChecked ? 1 : 0, 0);
			}
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000F2D80 File Offset: 0x000F0F80
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
			EventHandler eventHandler = (EventHandler)base.Events[RadioButton.EVENT_CHECKEDCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000F2DC6 File Offset: 0x000F0FC6
		protected override void OnClick(EventArgs e)
		{
			if (this.autoCheck)
			{
				this.Checked = true;
			}
			base.OnClick(e);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000F2DDE File Offset: 0x000F0FDE
		protected override void OnEnter(EventArgs e)
		{
			if (Control.MouseButtons == MouseButtons.None)
			{
				if (UnsafeNativeMethods.GetKeyState(9) >= 0)
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						this.OnClick(e);
					}
				}
				else
				{
					this.PerformAutoUpdates(true);
					this.TabStop = true;
				}
			}
			base.OnEnter(e);
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x000F2E20 File Offset: 0x000F1020
		private void PerformAutoUpdates(bool tabbedInto)
		{
			if (this.autoCheck)
			{
				if (this.firstfocus)
				{
					this.WipeTabStops(tabbedInto);
				}
				this.TabStop = this.isChecked;
				if (this.isChecked)
				{
					Control parentInternal = this.ParentInternal;
					if (parentInternal != null)
					{
						Control.ControlCollection controls = parentInternal.Controls;
						for (int i = 0; i < controls.Count; i++)
						{
							Control control = controls[i];
							if (control != this && control is RadioButton)
							{
								RadioButton radioButton = (RadioButton)control;
								if (radioButton.autoCheck && radioButton.Checked)
								{
									PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["Checked"];
									propertyDescriptor.SetValue(radioButton, false);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x000F2ECC File Offset: 0x000F10CC
		private void WipeTabStops(bool tabbedInto)
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal != null)
			{
				Control.ControlCollection controls = parentInternal.Controls;
				for (int i = 0; i < controls.Count; i++)
				{
					Control control = controls[i];
					if (control is RadioButton)
					{
						RadioButton radioButton = (RadioButton)control;
						if (!tabbedInto)
						{
							radioButton.firstfocus = false;
						}
						if (radioButton.autoCheck)
						{
							radioButton.TabStop = false;
						}
					}
				}
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000F2F2F File Offset: 0x000F112F
		internal override ButtonBaseAdapter CreateFlatAdapter()
		{
			return new RadioButtonFlatAdapter(this);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000F2F37 File Offset: 0x000F1137
		internal override ButtonBaseAdapter CreatePopupAdapter()
		{
			return new RadioButtonPopupAdapter(this);
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000F2F3F File Offset: 0x000F113F
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new RadioButtonStandardAdapter(this);
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000F2F48 File Offset: 0x000F1148
		private void OnAppearanceChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[RadioButton.EVENT_APPEARANCECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000F2F78 File Offset: 0x000F1178
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left && base.GetStyle(ControlStyles.UserPaint) && base.MouseIsDown)
			{
				Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
				if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
				{
					base.ResetFlagsandPaint();
					if (!base.ValidationCancelled)
					{
						this.OnClick(mevent);
						this.OnMouseClick(mevent);
					}
				}
			}
			base.OnMouseUp(mevent);
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000F2FFE File Offset: 0x000F11FE
		public void PerformClick()
		{
			if (base.CanSelect)
			{
				base.ResetFlagsandPaint();
				if (!base.ValidationCancelled)
				{
					this.OnClick(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000F3021 File Offset: 0x000F1221
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && Control.IsMnemonic(charCode, this.Text) && base.CanSelect)
			{
				if (!this.Focused)
				{
					this.FocusInternal();
				}
				else
				{
					this.PerformClick();
				}
				return true;
			}
			return false;
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000F305C File Offset: 0x000F125C
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Checked: " + this.Checked.ToString();
		}

		// Token: 0x04001F5E RID: 8030
		private static readonly object EVENT_CHECKEDCHANGED = new object();

		// Token: 0x04001F5F RID: 8031
		private static readonly ContentAlignment anyRight = (ContentAlignment)1092;

		// Token: 0x04001F60 RID: 8032
		private bool firstfocus = true;

		// Token: 0x04001F61 RID: 8033
		private bool isChecked;

		// Token: 0x04001F62 RID: 8034
		private bool autoCheck = true;

		// Token: 0x04001F63 RID: 8035
		private ContentAlignment checkAlign = ContentAlignment.MiddleLeft;

		// Token: 0x04001F64 RID: 8036
		private Appearance appearance;

		// Token: 0x04001F65 RID: 8037
		private const int FlatSystemStylePaddingWidth = 24;

		// Token: 0x04001F66 RID: 8038
		private const int FlatSystemStyleMinimumHeight = 13;

		// Token: 0x04001F67 RID: 8039
		internal int flatSystemStylePaddingWidth = 24;

		// Token: 0x04001F68 RID: 8040
		internal int flatSystemStyleMinimumHeight = 13;

		// Token: 0x04001F69 RID: 8041
		private static readonly object EVENT_APPEARANCECHANGED = new object();

		// Token: 0x020007DA RID: 2010
		[ComVisible(true)]
		public class RadioButtonAccessibleObject : ButtonBase.ButtonBaseAccessibleObject
		{
			// Token: 0x06006DB0 RID: 28080 RVA: 0x0016E643 File Offset: 0x0016C843
			public RadioButtonAccessibleObject(RadioButton owner) : base(owner)
			{
			}

			// Token: 0x170017F6 RID: 6134
			// (get) Token: 0x06006DB1 RID: 28081 RVA: 0x00192CD8 File Offset: 0x00190ED8
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
					return SR.GetString("AccessibleActionCheck");
				}
			}

			// Token: 0x170017F7 RID: 6135
			// (get) Token: 0x06006DB2 RID: 28082 RVA: 0x00192D10 File Offset: 0x00190F10
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.RadioButton;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.RadioButton;
				}
			}

			// Token: 0x170017F8 RID: 6136
			// (get) Token: 0x06006DB3 RID: 28083 RVA: 0x00192D3C File Offset: 0x00190F3C
			public override AccessibleStates State
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleStates.None;
					}
					if (((RadioButton)base.Owner).Checked)
					{
						return AccessibleStates.Checked | base.State;
					}
					return base.State;
				}
			}

			// Token: 0x06006DB4 RID: 28084 RVA: 0x00192D6A File Offset: 0x00190F6A
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				((RadioButton)base.Owner).PerformClick();
			}
		}
	}
}
