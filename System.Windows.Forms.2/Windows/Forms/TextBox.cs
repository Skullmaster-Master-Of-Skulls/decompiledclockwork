using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020003A1 RID: 929
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.TextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTextBox")]
	public class TextBox : TextBoxBase
	{
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x00107A0A File Offset: 0x00105C0A
		// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x00107A12 File Offset: 0x00105C12
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxAcceptsReturnDescr")]
		public bool AcceptsReturn
		{
			get
			{
				return this.acceptsReturn;
			}
			set
			{
				this.acceptsReturn = value;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x00107A1B File Offset: 0x00105C1B
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x00107A24 File Offset: 0x00105C24
		[DefaultValue(AutoCompleteMode.None)]
		[SRDescription("TextBoxAutoCompleteModeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteMode AutoCompleteMode
		{
			get
			{
				return this.autoCompleteMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteMode));
				}
				bool autoComplete = false;
				if (this.autoCompleteMode != AutoCompleteMode.None && value == AutoCompleteMode.None)
				{
					autoComplete = true;
				}
				this.autoCompleteMode = value;
				this.SetAutoComplete(autoComplete);
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x00107A74 File Offset: 0x00105C74
		// (set) Token: 0x06003CBA RID: 15546 RVA: 0x00107A7C File Offset: 0x00105C7C
		[DefaultValue(AutoCompleteSource.None)]
		[SRDescription("TextBoxAutoCompleteSourceDescr")]
		[TypeConverter(typeof(TextBoxAutoCompleteSourceConverter))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteSource AutoCompleteSource
		{
			get
			{
				return this.autoCompleteSource;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[]
				{
					128,
					7,
					6,
					64,
					1,
					32,
					2,
					256,
					4
				}))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoCompleteSource));
				}
				if (value == AutoCompleteSource.ListItems)
				{
					throw new NotSupportedException(SR.GetString("TextBoxAutoCompleteSourceNoItems"));
				}
				if (value != AutoCompleteSource.None && value != AutoCompleteSource.CustomSource)
				{
					new FileIOPermission(PermissionState.Unrestricted)
					{
						AllFiles = FileIOPermissionAccess.PathDiscovery
					}.Demand();
				}
				this.autoCompleteSource = value;
				this.SetAutoComplete(false);
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x00107B06 File Offset: 0x00105D06
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x00107B38 File Offset: 0x00105D38
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("TextBoxAutoCompleteCustomSourceDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			get
			{
				if (this.autoCompleteCustomSource == null)
				{
					this.autoCompleteCustomSource = new AutoCompleteStringCollection();
					this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
				}
				return this.autoCompleteCustomSource;
			}
			set
			{
				if (this.autoCompleteCustomSource != value)
				{
					if (this.autoCompleteCustomSource != null)
					{
						this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
					}
					this.autoCompleteCustomSource = value;
					if (value != null)
					{
						this.autoCompleteCustomSource.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
					}
					this.SetAutoComplete(false);
				}
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x00107B95 File Offset: 0x00105D95
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x00107B9D File Offset: 0x00105D9D
		[SRCategory("CatBehavior")]
		[DefaultValue(CharacterCasing.Normal)]
		[SRDescription("TextBoxCharacterCasingDescr")]
		public CharacterCasing CharacterCasing
		{
			get
			{
				return this.characterCasing;
			}
			set
			{
				if (this.characterCasing != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(CharacterCasing));
					}
					this.characterCasing = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x00107BDB File Offset: 0x00105DDB
		private bool ContainsNavigationKeyCode(Keys keyCode)
		{
			return keyCode - Keys.Prior <= 7;
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x000F4117 File Offset: 0x000F2317
		// (set) Token: 0x06003CC1 RID: 15553 RVA: 0x00107BE7 File Offset: 0x00105DE7
		public override bool Multiline
		{
			get
			{
				return base.Multiline;
			}
			set
			{
				if (this.Multiline != value)
				{
					base.Multiline = value;
					if (value && this.AutoCompleteMode != AutoCompleteMode.None)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x00107C0A File Offset: 0x00105E0A
		internal override bool PasswordProtect
		{
			get
			{
				return this.PasswordChar > '\0';
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x00107C18 File Offset: 0x00105E18
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				CharacterCasing characterCasing = this.characterCasing;
				if (characterCasing != CharacterCasing.Upper)
				{
					if (characterCasing == CharacterCasing.Lower)
					{
						createParams.Style |= 16;
					}
				}
				else
				{
					createParams.Style |= 8;
				}
				HorizontalAlignment horizontalAlignment = base.RtlTranslateHorizontal(this.textAlign);
				createParams.ExStyle &= -4097;
				switch (horizontalAlignment)
				{
				case HorizontalAlignment.Left:
					createParams.Style |= 0;
					break;
				case HorizontalAlignment.Right:
					createParams.Style |= 2;
					break;
				case HorizontalAlignment.Center:
					createParams.Style |= 1;
					break;
				}
				if (this.Multiline)
				{
					if ((this.scrollBars & ScrollBars.Horizontal) == ScrollBars.Horizontal && this.textAlign == HorizontalAlignment.Left && !base.WordWrap)
					{
						createParams.Style |= 1048576;
					}
					if ((this.scrollBars & ScrollBars.Vertical) == ScrollBars.Vertical)
					{
						createParams.Style |= 2097152;
					}
				}
				if (this.useSystemPasswordChar)
				{
					createParams.Style |= 32;
				}
				return createParams;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003CC4 RID: 15556 RVA: 0x00107D25 File Offset: 0x00105F25
		// (set) Token: 0x06003CC5 RID: 15557 RVA: 0x00107D48 File Offset: 0x00105F48
		[SRCategory("CatBehavior")]
		[DefaultValue('\0')]
		[Localizable(true)]
		[SRDescription("TextBoxPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public char PasswordChar
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				return (char)((int)base.SendMessage(210, 0, 0));
			}
			set
			{
				this.passwordChar = value;
				if (!this.useSystemPasswordChar && base.IsHandleCreated && this.PasswordChar != value)
				{
					base.SendMessage(204, (int)value, 0);
					base.VerifyImeRestrictedModeChanged();
					this.ResetAutoComplete(false);
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x00107D96 File Offset: 0x00105F96
		// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x00107D9E File Offset: 0x00105F9E
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(ScrollBars.None)]
		[SRDescription("TextBoxScrollBarsDescr")]
		public ScrollBars ScrollBars
		{
			get
			{
				return this.scrollBars;
			}
			set
			{
				if (this.scrollBars != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(ScrollBars));
					}
					this.scrollBars = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00107DDC File Offset: 0x00105FDC
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size empty = Size.Empty;
			if (this.Multiline && !base.WordWrap && (this.ScrollBars & ScrollBars.Horizontal) != ScrollBars.None)
			{
				empty.Height += SystemInformation.GetHorizontalScrollBarHeightForDpi(this.deviceDpi);
			}
			if (this.Multiline && (this.ScrollBars & ScrollBars.Vertical) != ScrollBars.None)
			{
				empty.Width += SystemInformation.GetVerticalScrollBarWidthForDpi(this.deviceDpi);
			}
			proposedConstraints -= empty;
			Size preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
			return preferredSizeCore + empty;
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x00107E65 File Offset: 0x00106065
		// (set) Token: 0x06003CCA RID: 15562 RVA: 0x00107E6D File Offset: 0x0010606D
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.selectionSet = false;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x00107E7D File Offset: 0x0010607D
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x00107E88 File Offset: 0x00106088
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[SRDescription("TextBoxTextAlignDescr")]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (this.textAlign != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
					}
					this.textAlign = value;
					base.RecreateHandle();
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x00107EDC File Offset: 0x001060DC
		// (set) Token: 0x06003CCE RID: 15566 RVA: 0x00107EE4 File Offset: 0x001060E4
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TextBoxUseSystemPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.useSystemPasswordChar;
			}
			set
			{
				if (value != this.useSystemPasswordChar)
				{
					this.useSystemPasswordChar = value;
					base.RecreateHandle();
					if (value)
					{
						this.ResetAutoComplete(false);
					}
				}
			}
		}

		// Token: 0x140002E7 RID: 743
		// (add) Token: 0x06003CCF RID: 15567 RVA: 0x00107F06 File Offset: 0x00106106
		// (remove) Token: 0x06003CD0 RID: 15568 RVA: 0x00107F19 File Offset: 0x00106119
		[SRCategory("CatPropertyChanged")]
		[SRDescription("RadioButtonOnTextAlignChangedDescr")]
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(TextBox.EVENT_TEXTALIGNCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TextBox.EVENT_TEXTALIGNCHANGED, value);
			}
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x00107F2C File Offset: 0x0010612C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ResetAutoComplete(true);
				if (this.autoCompleteCustomSource != null)
				{
					this.autoCompleteCustomSource.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
				}
				if (this.stringSource != null)
				{
					this.stringSource.ReleaseAutoComplete();
					this.stringSource = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x00107F84 File Offset: 0x00106184
		protected override bool IsInputKey(Keys keyData)
		{
			if (this.Multiline && (keyData & Keys.Alt) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Return)
				{
					return this.acceptsReturn;
				}
			}
			return base.IsInputKey(keyData);
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x00107FBD File Offset: 0x001061BD
		private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
		{
			if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
			{
				this.SetAutoComplete(true);
			}
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x000D4A4C File Offset: 0x000D2C4C
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (Application.RenderWithVisualStyles && base.IsHandleCreated && base.BorderStyle == BorderStyle.Fixed3D)
			{
				SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1025);
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x00107FD0 File Offset: 0x001061D0
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x00107FE7 File Offset: 0x001061E7
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (!this.selectionSet)
			{
				this.selectionSet = true;
				if (this.SelectionLength == 0 && Control.MouseButtons == MouseButtons.None)
				{
					base.SelectAll();
				}
			}
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x00108014 File Offset: 0x00106214
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SetSelectionOnHandle();
			if (this.passwordChar != '\0' && !this.useSystemPasswordChar)
			{
				base.SendMessage(204, (int)this.passwordChar, 0);
			}
			base.VerifyImeRestrictedModeChanged();
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				try
				{
					this.fromHandleCreate = true;
					this.SetAutoComplete(false);
				}
				finally
				{
					this.fromHandleCreate = false;
				}
			}
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x00108088 File Offset: 0x00106288
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (this.stringSource != null)
			{
				this.stringSource.ReleaseAutoComplete();
				this.stringSource = null;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x001080AB File Offset: 0x001062AB
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (AccessibilityImprovements.Level5 && base.IsHandleCreated && base.IsAccessibilityObjectCreated && this.ContainsNavigationKeyCode(e.KeyCode))
			{
				base.AccessibilityObject.RaiseAutomationEvent(20014);
			}
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x001080EA File Offset: 0x001062EA
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left && AccessibilityImprovements.Level5 && base.IsHandleCreated && base.IsAccessibilityObjectCreated)
			{
				base.AccessibilityObject.RaiseAutomationEvent(20014);
			}
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x00108128 File Offset: 0x00106328
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[TextBox.EVENT_TEXTALIGNCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00108158 File Offset: 0x00106358
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			bool flag = base.ProcessCmdKey(ref m, keyData);
			if (!flag && this.Multiline && !LocalAppContextSwitches.DoNotSupportSelectAllShortcutInMultilineTextBox && this.ShortcutsEnabled && keyData == (Keys)131137)
			{
				base.SelectAll();
				return true;
			}
			return flag;
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x00108199 File Offset: 0x00106399
		public void Paste(string text)
		{
			base.SetSelectedTextInternal(text, false);
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x001081A3 File Offset: 0x001063A3
		internal override void SelectInternal(int start, int length, int textLen)
		{
			this.selectionSet = true;
			base.SelectInternal(start, length, textLen);
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x001081B8 File Offset: 0x001063B8
		private string[] GetStringsForAutoComplete()
		{
			string[] array = new string[this.AutoCompleteCustomSource.Count];
			for (int i = 0; i < this.AutoCompleteCustomSource.Count; i++)
			{
				array[i] = this.AutoCompleteCustomSource[i];
			}
			return array;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x001081FC File Offset: 0x001063FC
		internal void SetAutoComplete(bool reset)
		{
			if (this.Multiline || this.passwordChar != '\0' || this.useSystemPasswordChar || this.AutoCompleteSource == AutoCompleteSource.None)
			{
				return;
			}
			if (this.AutoCompleteMode != AutoCompleteMode.None)
			{
				if (!this.fromHandleCreate)
				{
					AutoCompleteMode autoCompleteMode = this.AutoCompleteMode;
					this.autoCompleteMode = AutoCompleteMode.None;
					base.RecreateHandle();
					this.autoCompleteMode = autoCompleteMode;
				}
				if (this.AutoCompleteSource == AutoCompleteSource.CustomSource)
				{
					if (!base.IsHandleCreated || this.AutoCompleteCustomSource == null)
					{
						return;
					}
					if (this.AutoCompleteCustomSource.Count == 0)
					{
						this.ResetAutoComplete(true);
						return;
					}
					if (this.stringSource != null)
					{
						this.stringSource.RefreshList(this.GetStringsForAutoComplete());
						return;
					}
					this.stringSource = new StringSource(this.GetStringsForAutoComplete());
					if (!this.stringSource.Bind(new HandleRef(this, base.Handle), (int)this.AutoCompleteMode))
					{
						throw new ArgumentException(SR.GetString("AutoCompleteFailure"));
					}
					return;
				}
				else
				{
					try
					{
						if (base.IsHandleCreated)
						{
							int num = 0;
							if (this.AutoCompleteMode == AutoCompleteMode.Suggest)
							{
								num |= -1879048192;
							}
							if (this.AutoCompleteMode == AutoCompleteMode.Append)
							{
								num |= 1610612736;
							}
							if (this.AutoCompleteMode == AutoCompleteMode.SuggestAppend)
							{
								num |= 268435456;
								num |= 1073741824;
							}
							int num2 = SafeNativeMethods.SHAutoComplete(new HandleRef(this, base.Handle), (int)(this.AutoCompleteSource | (AutoCompleteSource)num));
						}
						return;
					}
					catch (SecurityException)
					{
						return;
					}
				}
			}
			if (reset)
			{
				this.ResetAutoComplete(true);
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x00108374 File Offset: 0x00106574
		private void ResetAutoComplete(bool force)
		{
			if ((this.AutoCompleteMode > AutoCompleteMode.None || force) && base.IsHandleCreated)
			{
				int flags = -1610612729;
				SafeNativeMethods.SHAutoComplete(new HandleRef(this, base.Handle), flags);
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x001083AF File Offset: 0x001065AF
		private void ResetAutoCompleteCustomSource()
		{
			this.AutoCompleteCustomSource = null;
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x001083B8 File Offset: 0x001065B8
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)m.LParam) != 0 && Application.RenderWithVisualStyles && base.BorderStyle == BorderStyle.Fixed3D)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					using (Graphics graphics = Graphics.FromHdc(m.WParam))
					{
						Rectangle rect = new Rectangle(0, 0, base.Size.Width - 1, base.Size.Height - 1);
						using (Pen pen = new Pen(VisualStyleInformation.TextControlBorder))
						{
							graphics.DrawRectangle(pen, rect);
						}
						rect.Inflate(-1, -1);
						graphics.DrawRectangle(SystemPens.Window, rect);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x001084A4 File Offset: 0x001066A4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 513)
			{
				if (msg == 514)
				{
					base.WndProc(ref m);
					return;
				}
				if (msg == 791)
				{
					this.WmPrint(ref m);
					return;
				}
				base.WndProc(ref m);
			}
			else
			{
				MouseButtons mouseButtons = Control.MouseButtons;
				bool validationCancelled = base.ValidationCancelled;
				this.FocusInternal();
				if (mouseButtons == Control.MouseButtons && (!base.ValidationCancelled || validationCancelled))
				{
					base.WndProc(ref m);
					return;
				}
			}
		}

		// Token: 0x040023AE RID: 9134
		private static readonly object EVENT_TEXTALIGNCHANGED = new object();

		// Token: 0x040023AF RID: 9135
		private bool acceptsReturn;

		// Token: 0x040023B0 RID: 9136
		private char passwordChar;

		// Token: 0x040023B1 RID: 9137
		private bool useSystemPasswordChar;

		// Token: 0x040023B2 RID: 9138
		private CharacterCasing characterCasing;

		// Token: 0x040023B3 RID: 9139
		private ScrollBars scrollBars;

		// Token: 0x040023B4 RID: 9140
		private HorizontalAlignment textAlign;

		// Token: 0x040023B5 RID: 9141
		private bool selectionSet;

		// Token: 0x040023B6 RID: 9142
		private AutoCompleteMode autoCompleteMode;

		// Token: 0x040023B7 RID: 9143
		private AutoCompleteSource autoCompleteSource = AutoCompleteSource.None;

		// Token: 0x040023B8 RID: 9144
		private AutoCompleteStringCollection autoCompleteCustomSource;

		// Token: 0x040023B9 RID: 9145
		private bool fromHandleCreate;

		// Token: 0x040023BA RID: 9146
		private StringSource stringSource;
	}
}
