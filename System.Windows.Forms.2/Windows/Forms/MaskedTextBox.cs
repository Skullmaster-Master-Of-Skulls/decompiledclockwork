using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Media;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020002E7 RID: 743
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("MaskInputRejected")]
	[DefaultBindingProperty("Text")]
	[DefaultProperty("Mask")]
	[Designer("System.Windows.Forms.Design.MaskedTextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionMaskedTextBox")]
	public class MaskedTextBox : TextBoxBase
	{
		// Token: 0x06002ED2 RID: 11986 RVA: 0x000D370C File Offset: 0x000D190C
		public MaskedTextBox()
		{
			MaskedTextProvider maskedTextProvider = new MaskedTextProvider("<>", CultureInfo.CurrentCulture);
			this.flagState[MaskedTextBox.IS_NULL_MASK] = true;
			this.Initialize(maskedTextProvider);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x000D3748 File Offset: 0x000D1948
		public MaskedTextBox(string mask)
		{
			if (mask == null)
			{
				throw new ArgumentNullException();
			}
			MaskedTextProvider maskedTextProvider = new MaskedTextProvider(mask, CultureInfo.CurrentCulture);
			this.flagState[MaskedTextBox.IS_NULL_MASK] = false;
			this.Initialize(maskedTextProvider);
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000D3788 File Offset: 0x000D1988
		public MaskedTextBox(MaskedTextProvider maskedTextProvider)
		{
			if (maskedTextProvider == null)
			{
				throw new ArgumentNullException();
			}
			this.flagState[MaskedTextBox.IS_NULL_MASK] = false;
			this.Initialize(maskedTextProvider);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000D37B4 File Offset: 0x000D19B4
		private void Initialize(MaskedTextProvider maskedTextProvider)
		{
			this.maskedTextProvider = maskedTextProvider;
			if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				this.SetWindowText();
			}
			this.passwordChar = this.maskedTextProvider.PasswordChar;
			this.insertMode = InsertKeyMode.Default;
			this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE] = false;
			this.flagState[MaskedTextBox.BEEP_ON_ERROR] = false;
			this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR] = false;
			this.flagState[MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE] = false;
			this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = this.maskedTextProvider.IncludePrompt;
			this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = this.maskedTextProvider.IncludeLiterals;
			this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = true;
			this.caretTestPos = 0;
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06002ED7 RID: 11991 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool AcceptsTab
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x000D388A File Offset: 0x000D1A8A
		// (set) Token: 0x06002ED9 RID: 11993 RVA: 0x000D3898 File Offset: 0x000D1A98
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxAllowPromptAsInputDescr")]
		[DefaultValue(true)]
		public bool AllowPromptAsInput
		{
			get
			{
				return this.maskedTextProvider.AllowPromptAsInput;
			}
			set
			{
				if (value != this.maskedTextProvider.AllowPromptAsInput)
				{
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, this.maskedTextProvider.Culture, value, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		// Token: 0x14000222 RID: 546
		// (add) Token: 0x06002EDA RID: 11994 RVA: 0x000072B6 File Offset: 0x000054B6
		// (remove) Token: 0x06002EDB RID: 11995 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler AcceptsTabChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x000D38F8 File Offset: 0x000D1AF8
		// (set) Token: 0x06002EDD RID: 11997 RVA: 0x000D3908 File Offset: 0x000D1B08
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxAsciiOnlyDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(false)]
		public bool AsciiOnly
		{
			get
			{
				return this.maskedTextProvider.AsciiOnly;
			}
			set
			{
				if (value != this.maskedTextProvider.AsciiOnly)
				{
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, this.maskedTextProvider.Culture, this.maskedTextProvider.AllowPromptAsInput, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, value);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000D3968 File Offset: 0x000D1B68
		// (set) Token: 0x06002EDF RID: 11999 RVA: 0x000D397A File Offset: 0x000D1B7A
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxBeepOnErrorDescr")]
		[DefaultValue(false)]
		public bool BeepOnError
		{
			get
			{
				return this.flagState[MaskedTextBox.BEEP_ON_ERROR];
			}
			set
			{
				this.flagState[MaskedTextBox.BEEP_ON_ERROR] = value;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x00011A20 File Offset: 0x0000FC20
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool CanUndo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x000D3990 File Offset: 0x000D1B90
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
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
				return createParams;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x000D3A06 File Offset: 0x000D1C06
		// (set) Token: 0x06002EE3 RID: 12003 RVA: 0x000D3A14 File Offset: 0x000D1C14
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxCultureDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public CultureInfo Culture
		{
			get
			{
				return this.maskedTextProvider.Culture;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (!this.maskedTextProvider.Culture.Equals(value))
				{
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, value, this.maskedTextProvider.AllowPromptAsInput, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000D3A82 File Offset: 0x000D1C82
		// (set) Token: 0x06002EE5 RID: 12005 RVA: 0x000D3AC4 File Offset: 0x000D1CC4
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxCutCopyMaskFormat")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(MaskFormat.IncludeLiterals)]
		public MaskFormat CutCopyMaskFormat
		{
			get
			{
				if (this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT])
				{
					if (this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS])
					{
						return MaskFormat.IncludePromptAndLiterals;
					}
					return MaskFormat.IncludePrompt;
				}
				else
				{
					if (this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS])
					{
						return MaskFormat.IncludeLiterals;
					}
					return MaskFormat.ExcludePromptAndLiterals;
				}
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
				}
				if (value == MaskFormat.IncludePrompt)
				{
					this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = true;
					this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = false;
					return;
				}
				if (value == MaskFormat.IncludeLiterals)
				{
					this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = false;
					this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = true;
					return;
				}
				bool value2 = value == MaskFormat.IncludePromptAndLiterals;
				this.flagState[MaskedTextBox.CUTCOPYINCLUDEPROMPT] = value2;
				this.flagState[MaskedTextBox.CUTCOPYINCLUDELITERALS] = value2;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000D3B6C File Offset: 0x000D1D6C
		// (set) Token: 0x06002EE7 RID: 12007 RVA: 0x000D3B74 File Offset: 0x000D1D74
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IFormatProvider FormatProvider
		{
			get
			{
				return this.formatProvider;
			}
			set
			{
				this.formatProvider = value;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000D3B7D File Offset: 0x000D1D7D
		// (set) Token: 0x06002EE9 RID: 12009 RVA: 0x000D3B90 File Offset: 0x000D1D90
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxHidePromptOnLeaveDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(false)]
		public bool HidePromptOnLeave
		{
			get
			{
				return this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE];
			}
			set
			{
				if (this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE] != value)
				{
					this.flagState[MaskedTextBox.HIDE_PROMPT_ON_LEAVE] = value;
					if (!this.flagState[MaskedTextBox.IS_NULL_MASK] && !this.Focused && !this.MaskFull && !base.DesignMode)
					{
						this.SetWindowText();
					}
				}
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000D3BF1 File Offset: 0x000D1DF1
		// (set) Token: 0x06002EEB RID: 12011 RVA: 0x000D3BFE File Offset: 0x000D1DFE
		private bool IncludeLiterals
		{
			get
			{
				return this.maskedTextProvider.IncludeLiterals;
			}
			set
			{
				this.maskedTextProvider.IncludeLiterals = value;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000D3C0C File Offset: 0x000D1E0C
		// (set) Token: 0x06002EED RID: 12013 RVA: 0x000D3C19 File Offset: 0x000D1E19
		private bool IncludePrompt
		{
			get
			{
				return this.maskedTextProvider.IncludePrompt;
			}
			set
			{
				this.maskedTextProvider.IncludePrompt = value;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000D3C27 File Offset: 0x000D1E27
		// (set) Token: 0x06002EEF RID: 12015 RVA: 0x000D3C30 File Offset: 0x000D1E30
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxInsertKeyModeDescr")]
		[DefaultValue(InsertKeyMode.Default)]
		public InsertKeyMode InsertKeyMode
		{
			get
			{
				return this.insertMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(InsertKeyMode));
				}
				if (this.insertMode != value)
				{
					bool isOverwriteMode = this.IsOverwriteMode;
					this.insertMode = value;
					if (isOverwriteMode != this.IsOverwriteMode)
					{
						this.OnIsOverwriteModeChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000D3C8E File Offset: 0x000D1E8E
		protected override bool IsInputKey(Keys keyData)
		{
			return (keyData & Keys.KeyCode) != Keys.Return && base.IsInputKey(keyData);
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000D3CA4 File Offset: 0x000D1EA4
		[Browsable(false)]
		public bool IsOverwriteMode
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return false;
				}
				switch (this.insertMode)
				{
				case InsertKeyMode.Default:
					return this.flagState[MaskedTextBox.INSERT_TOGGLED];
				case InsertKeyMode.Insert:
					return false;
				case InsertKeyMode.Overwrite:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x14000223 RID: 547
		// (add) Token: 0x06002EF2 RID: 12018 RVA: 0x000D3CF6 File Offset: 0x000D1EF6
		// (remove) Token: 0x06002EF3 RID: 12019 RVA: 0x000D3D09 File Offset: 0x000D1F09
		[SRCategory("CatPropertyChanged")]
		[SRDescription("MaskedTextBoxIsOverwriteModeChangedDescr")]
		public event EventHandler IsOverwriteModeChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_ISOVERWRITEMODECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_ISOVERWRITEMODECHANGED, value);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000D3D1C File Offset: 0x000D1F1C
		// (set) Token: 0x06002EF5 RID: 12021 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string[] Lines
		{
			get
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
				string[] lines;
				try
				{
					lines = base.Lines;
				}
				finally
				{
					this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
				}
				return lines;
			}
			set
			{
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000D3D68 File Offset: 0x000D1F68
		// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000D3D90 File Offset: 0x000D1F90
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxMaskDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue("")]
		[MergableProperty(false)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.MaskPropertyEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Mask
		{
			get
			{
				if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return this.maskedTextProvider.Mask;
				}
				return string.Empty;
			}
			set
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK] == string.IsNullOrEmpty(value) && (this.flagState[MaskedTextBox.IS_NULL_MASK] || value == this.maskedTextProvider.Mask))
				{
					return;
				}
				string textOnInitializingMask = null;
				string mask = value;
				if (string.IsNullOrEmpty(value))
				{
					string textOutput = this.TextOutput;
					string text = this.maskedTextProvider.ToString(false, false);
					this.flagState[MaskedTextBox.IS_NULL_MASK] = true;
					if (this.maskedTextProvider.IsPassword)
					{
						this.SetEditControlPasswordChar(this.maskedTextProvider.PasswordChar);
					}
					this.SetWindowText(text, false, false);
					EventArgs empty = EventArgs.Empty;
					this.OnMaskChanged(empty);
					if (text != textOutput)
					{
						this.OnTextChanged(empty);
					}
					mask = "<>";
				}
				else
				{
					for (int i = 0; i < value.Length; i++)
					{
						char c = value[i];
						if (!MaskedTextProvider.IsValidMaskChar(c))
						{
							throw new ArgumentException(SR.GetString("MaskedTextBoxMaskInvalidChar"));
						}
					}
					if (this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						textOnInitializingMask = this.Text;
					}
				}
				MaskedTextProvider newProvider = new MaskedTextProvider(mask, this.maskedTextProvider.Culture, this.maskedTextProvider.AllowPromptAsInput, this.maskedTextProvider.PromptChar, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
				this.SetMaskedTextProvider(newProvider, textOnInitializingMask);
			}
		}

		// Token: 0x14000224 RID: 548
		// (add) Token: 0x06002EF8 RID: 12024 RVA: 0x000D3EFB File Offset: 0x000D20FB
		// (remove) Token: 0x06002EF9 RID: 12025 RVA: 0x000D3F0E File Offset: 0x000D210E
		[SRCategory("CatPropertyChanged")]
		[SRDescription("MaskedTextBoxMaskChangedDescr")]
		public event EventHandler MaskChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_MASKCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_MASKCHANGED, value);
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000D3F21 File Offset: 0x000D2121
		[Browsable(false)]
		public bool MaskCompleted
		{
			get
			{
				return this.maskedTextProvider.MaskCompleted;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000D3F2E File Offset: 0x000D212E
		[Browsable(false)]
		public bool MaskFull
		{
			get
			{
				return this.maskedTextProvider.MaskFull;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000D3F3B File Offset: 0x000D213B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MaskedTextProvider MaskedTextProvider
		{
			get
			{
				if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return (MaskedTextProvider)this.maskedTextProvider.Clone();
				}
				return null;
			}
		}

		// Token: 0x14000225 RID: 549
		// (add) Token: 0x06002EFD RID: 12029 RVA: 0x000D3F61 File Offset: 0x000D2161
		// (remove) Token: 0x06002EFE RID: 12030 RVA: 0x000D3F74 File Offset: 0x000D2174
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxMaskInputRejectedDescr")]
		public event MaskInputRejectedEventHandler MaskInputRejected
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_MASKINPUTREJECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_MASKINPUTREJECTED, value);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x000D3F87 File Offset: 0x000D2187
		// (set) Token: 0x06002F00 RID: 12032 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002F01 RID: 12033 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06002F02 RID: 12034 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Multiline
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x14000226 RID: 550
		// (add) Token: 0x06002F03 RID: 12035 RVA: 0x000072B6 File Offset: 0x000054B6
		// (remove) Token: 0x06002F04 RID: 12036 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler MultilineChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000D3F8F File Offset: 0x000D218F
		// (set) Token: 0x06002F06 RID: 12038 RVA: 0x000D3F9C File Offset: 0x000D219C
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue('\0')]
		public char PasswordChar
		{
			get
			{
				return this.maskedTextProvider.PasswordChar;
			}
			set
			{
				if (!MaskedTextProvider.IsValidPasswordChar(value))
				{
					throw new ArgumentException(SR.GetString("MaskedTextBoxInvalidCharError"));
				}
				if (this.passwordChar != value)
				{
					if (value == this.maskedTextProvider.PromptChar)
					{
						throw new InvalidOperationException(SR.GetString("MaskedTextBoxPasswordAndPromptCharError"));
					}
					this.passwordChar = value;
					if (!this.UseSystemPasswordChar)
					{
						this.maskedTextProvider.PasswordChar = value;
						if (this.flagState[MaskedTextBox.IS_NULL_MASK])
						{
							this.SetEditControlPasswordChar(value);
						}
						else
						{
							this.SetWindowText();
						}
						base.VerifyImeRestrictedModeChanged();
					}
				}
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000D402A File Offset: 0x000D222A
		internal override bool PasswordProtect
		{
			get
			{
				if (this.maskedTextProvider != null)
				{
					return this.maskedTextProvider.IsPassword;
				}
				return base.PasswordProtect;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000D4046 File Offset: 0x000D2246
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x000D4054 File Offset: 0x000D2254
		[SRCategory("CatAppearance")]
		[SRDescription("MaskedTextBoxPromptCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Localizable(true)]
		[DefaultValue('_')]
		public char PromptChar
		{
			get
			{
				return this.maskedTextProvider.PromptChar;
			}
			set
			{
				if (!MaskedTextProvider.IsValidInputChar(value))
				{
					throw new ArgumentException(SR.GetString("MaskedTextBoxInvalidCharError"));
				}
				if (this.maskedTextProvider.PromptChar != value)
				{
					if (value == this.passwordChar || value == this.maskedTextProvider.PasswordChar)
					{
						throw new InvalidOperationException(SR.GetString("MaskedTextBoxPasswordAndPromptCharError"));
					}
					MaskedTextProvider maskedTextProvider = new MaskedTextProvider(this.maskedTextProvider.Mask, this.maskedTextProvider.Culture, this.maskedTextProvider.AllowPromptAsInput, value, this.maskedTextProvider.PasswordChar, this.maskedTextProvider.AsciiOnly);
					this.SetMaskedTextProvider(maskedTextProvider);
				}
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000D40F3 File Offset: 0x000D22F3
		// (set) Token: 0x06002F0B RID: 12043 RVA: 0x000D40FB File Offset: 0x000D22FB
		public new bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				if (this.ReadOnly != value)
				{
					base.ReadOnly = value;
					if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						this.SetWindowText();
					}
				}
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000D4125 File Offset: 0x000D2325
		// (set) Token: 0x06002F0D RID: 12045 RVA: 0x000D4137 File Offset: 0x000D2337
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxRejectInputOnFirstFailureDescr")]
		[DefaultValue(false)]
		public bool RejectInputOnFirstFailure
		{
			get
			{
				return this.flagState[MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE];
			}
			set
			{
				this.flagState[MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE] = value;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x000D414A File Offset: 0x000D234A
		// (set) Token: 0x06002F0F RID: 12047 RVA: 0x000D4157 File Offset: 0x000D2357
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxResetOnPrompt")]
		[DefaultValue(true)]
		public bool ResetOnPrompt
		{
			get
			{
				return this.maskedTextProvider.ResetOnPrompt;
			}
			set
			{
				this.maskedTextProvider.ResetOnPrompt = value;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002F10 RID: 12048 RVA: 0x000D4165 File Offset: 0x000D2365
		// (set) Token: 0x06002F11 RID: 12049 RVA: 0x000D4172 File Offset: 0x000D2372
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxResetOnSpace")]
		[DefaultValue(true)]
		public bool ResetOnSpace
		{
			get
			{
				return this.maskedTextProvider.ResetOnSpace;
			}
			set
			{
				this.maskedTextProvider.ResetOnSpace = value;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002F12 RID: 12050 RVA: 0x000D4180 File Offset: 0x000D2380
		// (set) Token: 0x06002F13 RID: 12051 RVA: 0x000D418D File Offset: 0x000D238D
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxSkipLiterals")]
		[DefaultValue(true)]
		public bool SkipLiterals
		{
			get
			{
				return this.maskedTextProvider.SkipLiterals;
			}
			set
			{
				this.maskedTextProvider.SkipLiterals = value;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002F14 RID: 12052 RVA: 0x000D419B File Offset: 0x000D239B
		// (set) Token: 0x06002F15 RID: 12053 RVA: 0x000138C4 File Offset: 0x00011AC4
		public override string SelectedText
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return base.SelectedText;
				}
				return this.GetSelectedText();
			}
			set
			{
				this.SetSelectedTextInternal(value, true);
			}
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000D41BC File Offset: 0x000D23BC
		internal override void SetSelectedTextInternal(string value, bool clearUndo)
		{
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				base.SetSelectedTextInternal(value, true);
				return;
			}
			this.PasteInt(value);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000D41E0 File Offset: 0x000D23E0
		private void ImeComplete()
		{
			this.flagState[MaskedTextBox.IME_COMPLETING] = true;
			this.ImeNotify(1);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000D41FC File Offset: 0x000D23FC
		private void ImeNotify(int action)
		{
			HandleRef hWnd = new HandleRef(this, base.Handle);
			IntPtr intPtr = UnsafeNativeMethods.ImmGetContext(hWnd);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					UnsafeNativeMethods.ImmNotifyIME(new HandleRef(null, intPtr), 21, action, 0);
				}
				finally
				{
					UnsafeNativeMethods.ImmReleaseContext(hWnd, new HandleRef(null, intPtr));
				}
			}
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000D4260 File Offset: 0x000D2460
		private void SetEditControlPasswordChar(char pwdChar)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(204, (int)pwdChar, 0);
				base.Invalidate();
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x000D4280 File Offset: 0x000D2480
		private char SystemPasswordChar
		{
			get
			{
				if (MaskedTextBox.systemPwdChar == '\0')
				{
					TextBox textBox = new TextBox();
					textBox.UseSystemPasswordChar = true;
					MaskedTextBox.systemPwdChar = textBox.PasswordChar;
					textBox.Dispose();
				}
				return MaskedTextBox.systemPwdChar;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000D42B7 File Offset: 0x000D24B7
		// (set) Token: 0x06002F1C RID: 12060 RVA: 0x000D42EC File Offset: 0x000D24EC
		[Editor("System.Windows.Forms.Design.MaskedTextBoxTextEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Bindable(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK] || this.flagState[MaskedTextBox.QUERY_BASE_TEXT])
				{
					return base.Text;
				}
				return this.TextOutput;
			}
			set
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					base.Text = value;
					return;
				}
				if (string.IsNullOrEmpty(value))
				{
					this.Delete(Keys.Delete, 0, this.maskedTextProvider.Length);
					return;
				}
				if (!this.RejectInputOnFirstFailure)
				{
					this.Replace(value, 0, this.maskedTextProvider.Length);
					return;
				}
				string textOutput = this.TextOutput;
				MaskedTextResultHint rejectionHint;
				if (this.maskedTextProvider.Set(value, out this.caretTestPos, out rejectionHint))
				{
					if (this.TextOutput != textOutput)
					{
						this.SetText();
					}
					int selectionStart = this.caretTestPos + 1;
					this.caretTestPos = selectionStart;
					base.SelectionStart = selectionStart;
					return;
				}
				this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, rejectionHint));
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000D43A7 File Offset: 0x000D25A7
		[Browsable(false)]
		public override int TextLength
		{
			get
			{
				if (this.flagState[MaskedTextBox.IS_NULL_MASK])
				{
					return base.TextLength;
				}
				return this.GetFormattedDisplayString().Length;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x000D43CD File Offset: 0x000D25CD
		private string TextOutput
		{
			get
			{
				return this.maskedTextProvider.ToString();
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000D43DA File Offset: 0x000D25DA
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x000D43E4 File Offset: 0x000D25E4
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

		// Token: 0x14000227 RID: 551
		// (add) Token: 0x06002F21 RID: 12065 RVA: 0x000D4438 File Offset: 0x000D2638
		// (remove) Token: 0x06002F22 RID: 12066 RVA: 0x000D444B File Offset: 0x000D264B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("RadioButtonOnTextAlignChangedDescr")]
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_TEXTALIGNCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_TEXTALIGNCHANGED, value);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000D445E File Offset: 0x000D265E
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000D4480 File Offset: 0x000D2680
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxTextMaskFormat")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(MaskFormat.IncludeLiterals)]
		public MaskFormat TextMaskFormat
		{
			get
			{
				if (this.IncludePrompt)
				{
					if (this.IncludeLiterals)
					{
						return MaskFormat.IncludePromptAndLiterals;
					}
					return MaskFormat.IncludePrompt;
				}
				else
				{
					if (this.IncludeLiterals)
					{
						return MaskFormat.IncludeLiterals;
					}
					return MaskFormat.ExcludePromptAndLiterals;
				}
			}
			set
			{
				if (this.TextMaskFormat == value)
				{
					return;
				}
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(MaskFormat));
				}
				string text = this.flagState[MaskedTextBox.IS_NULL_MASK] ? null : this.TextOutput;
				if (value == MaskFormat.IncludePrompt)
				{
					this.IncludePrompt = true;
					this.IncludeLiterals = false;
				}
				else if (value == MaskFormat.IncludeLiterals)
				{
					this.IncludePrompt = false;
					this.IncludeLiterals = true;
				}
				else
				{
					bool flag = value == MaskFormat.IncludePromptAndLiterals;
					this.IncludePrompt = flag;
					this.IncludeLiterals = flag;
				}
				if (text != null && text != this.TextOutput)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000D4530 File Offset: 0x000D2730
		public override string ToString()
		{
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return base.ToString();
			}
			bool includePrompt = this.IncludePrompt;
			bool includeLiterals = this.IncludeLiterals;
			string result;
			try
			{
				this.IncludePrompt = (this.IncludeLiterals = true);
				result = base.ToString();
			}
			finally
			{
				this.IncludePrompt = includePrompt;
				this.IncludeLiterals = includeLiterals;
			}
			return result;
		}

		// Token: 0x14000228 RID: 552
		// (add) Token: 0x06002F26 RID: 12070 RVA: 0x000D45A0 File Offset: 0x000D27A0
		// (remove) Token: 0x06002F27 RID: 12071 RVA: 0x000D45B3 File Offset: 0x000D27B3
		[SRCategory("CatFocus")]
		[SRDescription("MaskedTextBoxTypeValidationCompletedDescr")]
		public event TypeValidationEventHandler TypeValidationCompleted
		{
			add
			{
				base.Events.AddHandler(MaskedTextBox.EVENT_VALIDATIONCOMPLETED, value);
			}
			remove
			{
				base.Events.RemoveHandler(MaskedTextBox.EVENT_VALIDATIONCOMPLETED, value);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x000D45C6 File Offset: 0x000D27C6
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x000D45D8 File Offset: 0x000D27D8
		[SRCategory("CatBehavior")]
		[SRDescription("MaskedTextBoxUseSystemPasswordCharDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(false)]
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR];
			}
			set
			{
				if (value != this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR])
				{
					if (value)
					{
						if (this.SystemPasswordChar == this.PromptChar)
						{
							throw new InvalidOperationException(SR.GetString("MaskedTextBoxPasswordAndPromptCharError"));
						}
						this.maskedTextProvider.PasswordChar = this.SystemPasswordChar;
					}
					else
					{
						this.maskedTextProvider.PasswordChar = this.passwordChar;
					}
					this.flagState[MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR] = value;
					if (this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						this.SetEditControlPasswordChar(this.maskedTextProvider.PasswordChar);
					}
					else
					{
						this.SetWindowText();
					}
					base.VerifyImeRestrictedModeChanged();
				}
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x000D4682 File Offset: 0x000D2882
		// (set) Token: 0x06002F2B RID: 12075 RVA: 0x000D468A File Offset: 0x000D288A
		[Browsable(false)]
		[DefaultValue(null)]
		public Type ValidatingType
		{
			get
			{
				return this.validatingType;
			}
			set
			{
				if (this.validatingType != value)
				{
					this.validatingType = value;
				}
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x00011A20 File Offset: 0x0000FC20
		// (set) Token: 0x06002F2D RID: 12077 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool WordWrap
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void ClearUndo()
		{
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000D46A1 File Offset: 0x000D28A1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[UIPermission(SecurityAction.InheritanceDemand, Window = UIPermissionWindow.AllWindows)]
		protected override void CreateHandle()
		{
			if (!this.flagState[MaskedTextBox.IS_NULL_MASK] && base.RecreatingHandle)
			{
				this.SetWindowText(this.GetFormattedDisplayString(), false, false);
			}
			base.CreateHandle();
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000D46D4 File Offset: 0x000D28D4
		private void Delete(Keys keyCode, int startPosition, int selectionLen)
		{
			this.caretTestPos = startPosition;
			if (selectionLen == 0)
			{
				if (keyCode == Keys.Back)
				{
					if (startPosition == 0)
					{
						return;
					}
					startPosition--;
				}
				else if (startPosition + selectionLen == this.maskedTextProvider.Length)
				{
					return;
				}
			}
			int endPosition = (selectionLen > 0) ? (startPosition + selectionLen - 1) : startPosition;
			string textOutput = this.TextOutput;
			int position;
			MaskedTextResultHint maskedTextResultHint;
			if (this.maskedTextProvider.RemoveAt(startPosition, endPosition, out position, out maskedTextResultHint))
			{
				if (this.TextOutput != textOutput)
				{
					this.SetText();
					this.caretTestPos = startPosition;
				}
				else if (selectionLen > 0)
				{
					this.caretTestPos = startPosition;
				}
				else if (maskedTextResultHint == MaskedTextResultHint.NoEffect)
				{
					if (keyCode == Keys.Delete)
					{
						this.caretTestPos = this.maskedTextProvider.FindEditPositionFrom(startPosition, true);
					}
					else
					{
						if (this.maskedTextProvider.FindAssignedEditPositionFrom(startPosition, true) == MaskedTextProvider.InvalidIndex)
						{
							this.caretTestPos = this.maskedTextProvider.FindAssignedEditPositionFrom(startPosition, false);
						}
						else
						{
							this.caretTestPos = this.maskedTextProvider.FindEditPositionFrom(startPosition, false);
						}
						if (this.caretTestPos != MaskedTextProvider.InvalidIndex)
						{
							this.caretTestPos++;
						}
					}
					if (this.caretTestPos == MaskedTextProvider.InvalidIndex)
					{
						this.caretTestPos = startPosition;
					}
				}
				else if (keyCode == Keys.Back)
				{
					this.caretTestPos = startPosition;
				}
			}
			else
			{
				this.OnMaskInputRejected(new MaskInputRejectedEventArgs(position, maskedTextResultHint));
			}
			base.SelectInternal(this.caretTestPos, 0, this.maskedTextProvider.Length);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000D4828 File Offset: 0x000D2A28
		public override char GetCharFromPosition(Point pt)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			char charFromPosition;
			try
			{
				charFromPosition = base.GetCharFromPosition(pt);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return charFromPosition;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000D4874 File Offset: 0x000D2A74
		public override int GetCharIndexFromPosition(Point pt)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			int charIndexFromPosition;
			try
			{
				charIndexFromPosition = base.GetCharIndexFromPosition(pt);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return charIndexFromPosition;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000D48C0 File Offset: 0x000D2AC0
		internal override int GetEndPosition()
		{
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return base.GetEndPosition();
			}
			int num = this.maskedTextProvider.FindEditPositionFrom(this.maskedTextProvider.LastAssignedPosition + 1, true);
			if (num == MaskedTextProvider.InvalidIndex)
			{
				num = this.maskedTextProvider.LastAssignedPosition + 1;
			}
			return num;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x00011A20 File Offset: 0x0000FC20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int GetFirstCharIndexOfCurrentLine()
		{
			return 0;
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x00011A20 File Offset: 0x0000FC20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int GetFirstCharIndexFromLine(int lineNumber)
		{
			return 0;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000D4918 File Offset: 0x000D2B18
		private string GetFormattedDisplayString()
		{
			bool includePrompt = !this.ReadOnly && (base.DesignMode || !this.HidePromptOnLeave || this.Focused);
			return this.maskedTextProvider.ToString(false, includePrompt, true, 0, this.maskedTextProvider.Length);
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00011A20 File Offset: 0x0000FC20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetLineFromCharIndex(int index)
		{
			return 0;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000D496C File Offset: 0x000D2B6C
		public override Point GetPositionFromCharIndex(int index)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			Point positionFromCharIndex;
			try
			{
				positionFromCharIndex = base.GetPositionFromCharIndex(index);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return positionFromCharIndex;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000D49B8 File Offset: 0x000D2BB8
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			Size preferredSizeCore;
			try
			{
				preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
			return preferredSizeCore;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000D4A04 File Offset: 0x000D2C04
		private string GetSelectedText()
		{
			int startPosition;
			int num;
			base.GetSelectionStartAndLength(out startPosition, out num);
			if (num == 0)
			{
				return string.Empty;
			}
			bool includePrompt = (this.CutCopyMaskFormat & MaskFormat.IncludePrompt) > MaskFormat.ExcludePromptAndLiterals;
			bool includeLiterals = (this.CutCopyMaskFormat & MaskFormat.IncludeLiterals) > MaskFormat.ExcludePromptAndLiterals;
			return this.maskedTextProvider.ToString(true, includePrompt, includeLiterals, startPosition, num);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000D4A4C File Offset: 0x000D2C4C
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (Application.RenderWithVisualStyles && base.IsHandleCreated && base.BorderStyle == BorderStyle.Fixed3D)
			{
				SafeNativeMethods.RedrawWindow(new HandleRef(this, base.Handle), null, NativeMethods.NullHandleRef, 1025);
			}
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000D4A8A File Offset: 0x000D2C8A
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SetSelectionOnHandle();
			if (this.flagState[MaskedTextBox.IS_NULL_MASK] && this.maskedTextProvider.IsPassword)
			{
				this.SetEditControlPasswordChar(this.maskedTextProvider.PasswordChar);
			}
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000D4ACC File Offset: 0x000D2CCC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnIsOverwriteModeChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[MaskedTextBox.EVENT_ISOVERWRITEMODECHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000D4AFC File Offset: 0x000D2CFC
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return;
			}
			Keys keys = e.KeyCode;
			if (keys == Keys.Return || keys == Keys.Escape)
			{
				this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = false;
			}
			if (keys == Keys.Insert && e.Modifiers == Keys.None && this.insertMode == InsertKeyMode.Default)
			{
				this.flagState[MaskedTextBox.INSERT_TOGGLED] = !this.flagState[MaskedTextBox.INSERT_TOGGLED];
				this.OnIsOverwriteModeChanged(EventArgs.Empty);
				return;
			}
			if (e.Control && char.IsLetter((char)keys))
			{
				if (keys != Keys.H)
				{
					this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = false;
					return;
				}
				keys = Keys.Back;
			}
			if ((keys == Keys.Delete || keys == Keys.Back) && !this.ReadOnly)
			{
				int num;
				int num2;
				base.GetSelectionStartAndLength(out num, out num2);
				Keys modifiers = e.Modifiers;
				if (modifiers != Keys.Shift)
				{
					if (modifiers == Keys.Control)
					{
						if (num2 == 0)
						{
							if (keys == Keys.Delete)
							{
								num2 = this.maskedTextProvider.Length - num;
							}
							else
							{
								num2 = ((num == this.maskedTextProvider.Length) ? num : (num + 1));
								num = 0;
							}
						}
					}
				}
				else if (keys == Keys.Delete)
				{
					keys = Keys.Back;
				}
				if (!this.flagState[MaskedTextBox.HANDLE_KEY_PRESS])
				{
					this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = true;
				}
				this.Delete(keys, num, num2);
				e.SuppressKeyPress = true;
			}
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000D4C5C File Offset: 0x000D2E5C
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return;
			}
			if (!this.flagState[MaskedTextBox.HANDLE_KEY_PRESS])
			{
				this.flagState[MaskedTextBox.HANDLE_KEY_PRESS] = true;
				if (!char.IsLetter(e.KeyChar))
				{
					return;
				}
			}
			if (!this.ReadOnly)
			{
				int startPosition;
				int num;
				base.GetSelectionStartAndLength(out startPosition, out num);
				string textOutput = this.TextOutput;
				MaskedTextResultHint rejectionHint;
				if (this.PlaceChar(e.KeyChar, startPosition, num, this.IsOverwriteMode, out rejectionHint))
				{
					if (this.TextOutput != textOutput)
					{
						this.SetText();
					}
					int selectionStart = this.caretTestPos + 1;
					this.caretTestPos = selectionStart;
					base.SelectionStart = selectionStart;
					if (ImeModeConversion.InputLanguageTable == ImeModeConversion.KoreanTable)
					{
						int num2 = this.maskedTextProvider.FindUnassignedEditPositionFrom(this.caretTestPos, true);
						if (num2 == MaskedTextProvider.InvalidIndex)
						{
							this.ImeComplete();
						}
					}
				}
				else
				{
					this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, rejectionHint));
				}
				if (num > 0)
				{
					this.SelectionLength = 0;
				}
				e.Handled = true;
			}
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000D4D6C File Offset: 0x000D2F6C
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (this.flagState[MaskedTextBox.IME_COMPLETING])
			{
				this.flagState[MaskedTextBox.IME_COMPLETING] = false;
			}
			if (this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION])
			{
				this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION] = false;
			}
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000D4DC8 File Offset: 0x000D2FC8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMaskChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[MaskedTextBox.EVENT_MASKCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000D4DF8 File Offset: 0x000D2FF8
		private void OnMaskInputRejected(MaskInputRejectedEventArgs e)
		{
			if (this.BeepOnError)
			{
				SoundPlayer soundPlayer = new SoundPlayer();
				soundPlayer.Play();
			}
			MaskInputRejectedEventHandler maskInputRejectedEventHandler = base.Events[MaskedTextBox.EVENT_MASKINPUTREJECTED] as MaskInputRejectedEventHandler;
			if (maskInputRejectedEventHandler != null)
			{
				maskInputRejectedEventHandler(this, e);
			}
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnMultilineChanged(EventArgs e)
		{
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000D4E3C File Offset: 0x000D303C
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[MaskedTextBox.EVENT_TEXTALIGNCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000D4E6C File Offset: 0x000D306C
		private void OnTypeValidationCompleted(TypeValidationEventArgs e)
		{
			TypeValidationEventHandler typeValidationEventHandler = base.Events[MaskedTextBox.EVENT_VALIDATIONCOMPLETED] as TypeValidationEventHandler;
			if (typeValidationEventHandler != null)
			{
				typeValidationEventHandler(this, e);
			}
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000D4E9A File Offset: 0x000D309A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnValidating(CancelEventArgs e)
		{
			this.PerformTypeValidation(e);
			base.OnValidating(e);
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000D4EAC File Offset: 0x000D30AC
		protected override void OnTextChanged(EventArgs e)
		{
			bool value = this.flagState[MaskedTextBox.QUERY_BASE_TEXT];
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			try
			{
				base.OnTextChanged(e);
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = value;
			}
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000D4F08 File Offset: 0x000D3108
		private void Replace(string text, int startPosition, int selectionLen)
		{
			MaskedTextProvider maskedTextProvider = (MaskedTextProvider)this.maskedTextProvider.Clone();
			int num = this.caretTestPos;
			MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.NoEffect;
			int num2 = startPosition + selectionLen - 1;
			if (this.RejectInputOnFirstFailure)
			{
				if (!((startPosition > num2) ? maskedTextProvider.InsertAt(text, startPosition, out this.caretTestPos, out maskedTextResultHint) : maskedTextProvider.Replace(text, startPosition, num2, out this.caretTestPos, out maskedTextResultHint)))
				{
					this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, maskedTextResultHint));
				}
			}
			else
			{
				MaskedTextResultHint maskedTextResultHint2 = maskedTextResultHint;
				int i = 0;
				while (i < text.Length)
				{
					char c = text[i];
					if (this.maskedTextProvider.VerifyEscapeChar(c, startPosition))
					{
						goto IL_BF;
					}
					int num3 = maskedTextProvider.FindEditPositionFrom(startPosition, true);
					if (num3 != MaskedTextProvider.InvalidIndex)
					{
						startPosition = num3;
						goto IL_BF;
					}
					this.OnMaskInputRejected(new MaskInputRejectedEventArgs(startPosition, MaskedTextResultHint.UnavailableEditPosition));
					IL_109:
					i++;
					continue;
					IL_BF:
					int num4 = (num2 >= startPosition) ? 1 : 0;
					bool overwrite = num4 > 0;
					if (!this.PlaceChar(maskedTextProvider, c, startPosition, num4, overwrite, out maskedTextResultHint2))
					{
						this.OnMaskInputRejected(new MaskInputRejectedEventArgs(startPosition, maskedTextResultHint2));
						goto IL_109;
					}
					startPosition = this.caretTestPos + 1;
					if (maskedTextResultHint2 == MaskedTextResultHint.Success && maskedTextResultHint != maskedTextResultHint2)
					{
						maskedTextResultHint = maskedTextResultHint2;
						goto IL_109;
					}
					goto IL_109;
				}
				if (selectionLen > 0 && startPosition <= num2)
				{
					if (!maskedTextProvider.RemoveAt(startPosition, num2, out this.caretTestPos, out maskedTextResultHint2))
					{
						this.OnMaskInputRejected(new MaskInputRejectedEventArgs(this.caretTestPos, maskedTextResultHint2));
					}
					if (maskedTextResultHint == MaskedTextResultHint.NoEffect && maskedTextResultHint != maskedTextResultHint2)
					{
						maskedTextResultHint = maskedTextResultHint2;
					}
				}
			}
			bool flag = this.TextOutput != maskedTextProvider.ToString();
			this.maskedTextProvider = maskedTextProvider;
			if (flag)
			{
				this.SetText();
				this.caretTestPos = startPosition;
				base.SelectInternal(this.caretTestPos, 0, this.maskedTextProvider.Length);
				return;
			}
			this.caretTestPos = num;
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000D50B8 File Offset: 0x000D32B8
		private void PasteInt(string text)
		{
			int startPosition;
			int selectionLen;
			base.GetSelectionStartAndLength(out startPosition, out selectionLen);
			if (string.IsNullOrEmpty(text))
			{
				this.Delete(Keys.Delete, startPosition, selectionLen);
				return;
			}
			this.Replace(text, startPosition, selectionLen);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000D50EC File Offset: 0x000D32EC
		private object PerformTypeValidation(CancelEventArgs e)
		{
			object obj = null;
			if (this.validatingType != null)
			{
				string text = null;
				if (!this.flagState[MaskedTextBox.IS_NULL_MASK] && !this.maskedTextProvider.MaskCompleted)
				{
					text = SR.GetString("MaskedTextBoxIncompleteMsg");
				}
				else
				{
					string value;
					if (!this.flagState[MaskedTextBox.IS_NULL_MASK])
					{
						value = this.maskedTextProvider.ToString(false, this.IncludeLiterals);
					}
					else
					{
						value = base.Text;
					}
					try
					{
						obj = Formatter.ParseObject(value, this.validatingType, typeof(string), null, null, this.formatProvider, null, Formatter.GetDefaultDataSourceNullValue(this.validatingType));
					}
					catch (Exception innerException)
					{
						if (ClientUtils.IsSecurityOrCriticalException(innerException))
						{
							throw;
						}
						if (innerException.InnerException != null)
						{
							innerException = innerException.InnerException;
						}
						text = innerException.GetType().ToString() + ": " + innerException.Message;
					}
				}
				bool isValidInput = false;
				if (text == null)
				{
					isValidInput = true;
					text = SR.GetString("MaskedTextBoxTypeValidationSucceeded");
				}
				TypeValidationEventArgs typeValidationEventArgs = new TypeValidationEventArgs(this.validatingType, isValidInput, obj, text);
				this.OnTypeValidationCompleted(typeValidationEventArgs);
				if (e != null)
				{
					e.Cancel = typeValidationEventArgs.Cancel;
				}
			}
			return obj;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000D5224 File Offset: 0x000D3424
		private bool PlaceChar(char ch, int startPosition, int length, bool overwrite, out MaskedTextResultHint hint)
		{
			return this.PlaceChar(this.maskedTextProvider, ch, startPosition, length, overwrite, out hint);
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000D523C File Offset: 0x000D343C
		private bool PlaceChar(MaskedTextProvider provider, char ch, int startPosition, int length, bool overwrite, out MaskedTextResultHint hint)
		{
			this.caretTestPos = startPosition;
			if (startPosition >= this.maskedTextProvider.Length)
			{
				hint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			if (length > 0)
			{
				int endPosition = startPosition + length - 1;
				return provider.Replace(ch, startPosition, endPosition, out this.caretTestPos, out hint);
			}
			if (overwrite)
			{
				return provider.Replace(ch, startPosition, out this.caretTestPos, out hint);
			}
			return provider.InsertAt(ch, startPosition, out this.caretTestPos, out hint);
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000D52A8 File Offset: 0x000D34A8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool flag = base.ProcessCmdKey(ref msg, keyData);
			if (!flag && keyData == (Keys)131137)
			{
				base.SelectAll();
				flag = true;
			}
			return flag;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000D52D4 File Offset: 0x000D34D4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected internal override bool ProcessKeyMessage(ref Message m)
		{
			bool flag = base.ProcessKeyMessage(ref m);
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				return flag;
			}
			return (m.Msg == 258 && base.ImeWmCharsToIgnore > 0) || flag;
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000D5316 File Offset: 0x000D3516
		private void ResetCulture()
		{
			this.Culture = CultureInfo.CurrentCulture;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void ScrollToCaret()
		{
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000D5323 File Offset: 0x000D3523
		private void SetMaskedTextProvider(MaskedTextProvider newProvider)
		{
			this.SetMaskedTextProvider(newProvider, null);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000D5330 File Offset: 0x000D3530
		private void SetMaskedTextProvider(MaskedTextProvider newProvider, string textOnInitializingMask)
		{
			newProvider.IncludePrompt = this.maskedTextProvider.IncludePrompt;
			newProvider.IncludeLiterals = this.maskedTextProvider.IncludeLiterals;
			newProvider.SkipLiterals = this.maskedTextProvider.SkipLiterals;
			newProvider.ResetOnPrompt = this.maskedTextProvider.ResetOnPrompt;
			newProvider.ResetOnSpace = this.maskedTextProvider.ResetOnSpace;
			if (this.flagState[MaskedTextBox.IS_NULL_MASK] && textOnInitializingMask == null)
			{
				this.maskedTextProvider = newProvider;
				return;
			}
			int position = 0;
			MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.NoEffect;
			MaskedTextProvider maskedTextProvider = this.maskedTextProvider;
			bool flag = maskedTextProvider.Mask == newProvider.Mask;
			string a;
			bool flag2;
			if (textOnInitializingMask != null)
			{
				a = textOnInitializingMask;
				flag2 = !newProvider.Set(textOnInitializingMask, out position, out maskedTextResultHint);
			}
			else
			{
				a = this.TextOutput;
				int i = maskedTextProvider.AssignedEditPositionCount;
				int num = 0;
				int num2 = 0;
				while (i > 0)
				{
					num = maskedTextProvider.FindAssignedEditPositionFrom(num, true);
					if (flag)
					{
						num2 = num;
					}
					else
					{
						num2 = newProvider.FindEditPositionFrom(num2, true);
						if (num2 == MaskedTextProvider.InvalidIndex)
						{
							newProvider.Clear();
							position = newProvider.Length;
							maskedTextResultHint = MaskedTextResultHint.UnavailableEditPosition;
							break;
						}
					}
					if (!newProvider.Replace(maskedTextProvider[num], num2, out position, out maskedTextResultHint))
					{
						flag = false;
						newProvider.Clear();
						break;
					}
					num++;
					num2++;
					i--;
				}
				flag2 = !MaskedTextProvider.GetOperationResultFromHint(maskedTextResultHint);
			}
			this.maskedTextProvider = newProvider;
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				this.flagState[MaskedTextBox.IS_NULL_MASK] = false;
			}
			if (flag2)
			{
				this.OnMaskInputRejected(new MaskInputRejectedEventArgs(position, maskedTextResultHint));
			}
			if (newProvider.IsPassword)
			{
				this.SetEditControlPasswordChar('\0');
			}
			EventArgs empty = EventArgs.Empty;
			if (textOnInitializingMask != null || maskedTextProvider.Mask != newProvider.Mask)
			{
				this.OnMaskChanged(empty);
			}
			this.SetWindowText(this.GetFormattedDisplayString(), a != this.TextOutput, flag);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000D5507 File Offset: 0x000D3707
		private void SetText()
		{
			this.SetWindowText(this.GetFormattedDisplayString(), true, false);
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000D5517 File Offset: 0x000D3717
		private void SetWindowText()
		{
			this.SetWindowText(this.GetFormattedDisplayString(), false, true);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000D5528 File Offset: 0x000D3728
		private void SetWindowText(string text, bool raiseTextChangedEvent, bool preserveCaret)
		{
			this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = true;
			try
			{
				if (preserveCaret)
				{
					this.caretTestPos = base.SelectionStart;
				}
				this.WindowText = text;
				if (raiseTextChangedEvent)
				{
					this.OnTextChanged(EventArgs.Empty);
				}
				if (preserveCaret)
				{
					base.SelectionStart = this.caretTestPos;
				}
			}
			finally
			{
				this.flagState[MaskedTextBox.QUERY_BASE_TEXT] = false;
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000D55A0 File Offset: 0x000D37A0
		private bool ShouldSerializeCulture()
		{
			return !CultureInfo.CurrentCulture.Equals(this.Culture);
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new void Undo()
		{
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000D55B5 File Offset: 0x000D37B5
		public object ValidateText()
		{
			return this.PerformTypeValidation(null);
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000D55C0 File Offset: 0x000D37C0
		private bool WmClear()
		{
			if (!this.ReadOnly)
			{
				int startPosition;
				int selectionLen;
				base.GetSelectionStartAndLength(out startPosition, out selectionLen);
				this.Delete(Keys.Delete, startPosition, selectionLen);
				return true;
			}
			return false;
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000D55EC File Offset: 0x000D37EC
		private bool WmCopy()
		{
			if (this.maskedTextProvider.IsPassword)
			{
				return false;
			}
			string selectedText = this.GetSelectedText();
			try
			{
				IntSecurity.ClipboardWrite.Assert();
				if (selectedText.Length == 0)
				{
					Clipboard.Clear();
				}
				else
				{
					Clipboard.SetText(selectedText);
				}
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			return true;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000D5650 File Offset: 0x000D3850
		private bool WmImeComposition(ref Message m)
		{
			if (ImeModeConversion.InputLanguageTable == ImeModeConversion.KoreanTable)
			{
				byte b = 0;
				if ((m.LParam.ToInt32() & 8) != 0)
				{
					b = 1;
				}
				else if ((m.LParam.ToInt32() & 2048) != 0)
				{
					b = 2;
				}
				if (b != 0 && this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION])
				{
					return this.flagState[MaskedTextBox.IME_COMPLETING];
				}
			}
			return false;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000D56C0 File Offset: 0x000D38C0
		private bool WmImeStartComposition()
		{
			int num;
			int num2;
			base.GetSelectionStartAndLength(out num, out num2);
			int num3 = this.maskedTextProvider.FindEditPositionFrom(num, true);
			if (num3 != MaskedTextProvider.InvalidIndex)
			{
				if (num2 > 0 && ImeModeConversion.InputLanguageTable == ImeModeConversion.KoreanTable)
				{
					int num4 = this.maskedTextProvider.FindEditPositionFrom(num + num2 - 1, false);
					if (num4 < num3)
					{
						this.ImeComplete();
						this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num, MaskedTextResultHint.UnavailableEditPosition));
						return true;
					}
					num2 = num4 - num3 + 1;
					this.Delete(Keys.Delete, num3, num2);
				}
				if (num != num3)
				{
					this.caretTestPos = num3;
					base.SelectionStart = this.caretTestPos;
				}
				this.SelectionLength = 0;
				return false;
			}
			this.ImeComplete();
			this.OnMaskInputRejected(new MaskInputRejectedEventArgs(num, MaskedTextResultHint.UnavailableEditPosition));
			return true;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000D5774 File Offset: 0x000D3974
		private void WmPaste()
		{
			if (this.ReadOnly)
			{
				return;
			}
			string text;
			try
			{
				IntSecurity.ClipboardRead.Assert();
				text = Clipboard.GetText();
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
				return;
			}
			this.PasteInt(text);
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000D57C4 File Offset: 0x000D39C4
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)((long)m.LParam)) != 0 && Application.RenderWithVisualStyles && base.BorderStyle == BorderStyle.Fixed3D)
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

		// Token: 0x06002F5F RID: 12127 RVA: 0x000D58B0 File Offset: 0x000D3AB0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 183)
			{
				if (msg != 123)
				{
					if (msg != 183)
					{
						goto IL_5D;
					}
					return;
				}
			}
			else
			{
				switch (msg)
				{
				case 197:
				case 199:
					return;
				case 198:
					break;
				default:
					if (msg == 772)
					{
						return;
					}
					if (msg == 791)
					{
						this.WmPrint(ref m);
						return;
					}
					goto IL_5D;
				}
			}
			base.ClearUndo();
			base.WndProc(ref m);
			return;
			IL_5D:
			if (this.flagState[MaskedTextBox.IS_NULL_MASK])
			{
				base.WndProc(ref m);
				return;
			}
			int msg2 = m.Msg;
			if (msg2 <= 8)
			{
				if (msg2 == 7)
				{
					this.WmSetFocus();
					base.WndProc(ref m);
					return;
				}
				if (msg2 == 8)
				{
					base.WndProc(ref m);
					this.WmKillFocus();
					return;
				}
			}
			else
			{
				switch (msg2)
				{
				case 269:
					if (this.WmImeStartComposition())
					{
						return;
					}
					break;
				case 270:
					this.flagState[MaskedTextBox.IME_ENDING_COMPOSITION] = true;
					break;
				case 271:
					if (this.WmImeComposition(ref m))
					{
						return;
					}
					break;
				default:
					switch (msg2)
					{
					case 768:
						if (!this.ReadOnly && this.WmCopy())
						{
							this.WmClear();
							return;
						}
						return;
					case 769:
						this.WmCopy();
						return;
					case 770:
						this.WmPaste();
						return;
					case 771:
						this.WmClear();
						return;
					}
					break;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000D5A00 File Offset: 0x000D3C00
		private void WmKillFocus()
		{
			base.GetSelectionStartAndLength(out this.caretTestPos, out this.lastSelLength);
			if (this.HidePromptOnLeave && !this.MaskFull)
			{
				this.SetWindowText();
				base.SelectInternal(this.caretTestPos, this.lastSelLength, this.maskedTextProvider.Length);
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000D5A52 File Offset: 0x000D3C52
		private void WmSetFocus()
		{
			if (this.HidePromptOnLeave && !this.MaskFull)
			{
				this.SetWindowText();
			}
			base.SelectInternal(this.caretTestPos, this.lastSelLength, this.maskedTextProvider.Length);
		}

		// Token: 0x04001371 RID: 4977
		private const bool forward = true;

		// Token: 0x04001372 RID: 4978
		private const bool backward = false;

		// Token: 0x04001373 RID: 4979
		private const string nullMask = "<>";

		// Token: 0x04001374 RID: 4980
		private static readonly object EVENT_MASKINPUTREJECTED = new object();

		// Token: 0x04001375 RID: 4981
		private static readonly object EVENT_VALIDATIONCOMPLETED = new object();

		// Token: 0x04001376 RID: 4982
		private static readonly object EVENT_TEXTALIGNCHANGED = new object();

		// Token: 0x04001377 RID: 4983
		private static readonly object EVENT_ISOVERWRITEMODECHANGED = new object();

		// Token: 0x04001378 RID: 4984
		private static readonly object EVENT_MASKCHANGED = new object();

		// Token: 0x04001379 RID: 4985
		private static char systemPwdChar;

		// Token: 0x0400137A RID: 4986
		private const byte imeConvertionNone = 0;

		// Token: 0x0400137B RID: 4987
		private const byte imeConvertionUpdate = 1;

		// Token: 0x0400137C RID: 4988
		private const byte imeConvertionCompleted = 2;

		// Token: 0x0400137D RID: 4989
		private int lastSelLength;

		// Token: 0x0400137E RID: 4990
		private int caretTestPos;

		// Token: 0x0400137F RID: 4991
		private static int IME_ENDING_COMPOSITION = BitVector32.CreateMask();

		// Token: 0x04001380 RID: 4992
		private static int IME_COMPLETING = BitVector32.CreateMask(MaskedTextBox.IME_ENDING_COMPOSITION);

		// Token: 0x04001381 RID: 4993
		private static int HANDLE_KEY_PRESS = BitVector32.CreateMask(MaskedTextBox.IME_COMPLETING);

		// Token: 0x04001382 RID: 4994
		private static int IS_NULL_MASK = BitVector32.CreateMask(MaskedTextBox.HANDLE_KEY_PRESS);

		// Token: 0x04001383 RID: 4995
		private static int QUERY_BASE_TEXT = BitVector32.CreateMask(MaskedTextBox.IS_NULL_MASK);

		// Token: 0x04001384 RID: 4996
		private static int REJECT_INPUT_ON_FIRST_FAILURE = BitVector32.CreateMask(MaskedTextBox.QUERY_BASE_TEXT);

		// Token: 0x04001385 RID: 4997
		private static int HIDE_PROMPT_ON_LEAVE = BitVector32.CreateMask(MaskedTextBox.REJECT_INPUT_ON_FIRST_FAILURE);

		// Token: 0x04001386 RID: 4998
		private static int BEEP_ON_ERROR = BitVector32.CreateMask(MaskedTextBox.HIDE_PROMPT_ON_LEAVE);

		// Token: 0x04001387 RID: 4999
		private static int USE_SYSTEM_PASSWORD_CHAR = BitVector32.CreateMask(MaskedTextBox.BEEP_ON_ERROR);

		// Token: 0x04001388 RID: 5000
		private static int INSERT_TOGGLED = BitVector32.CreateMask(MaskedTextBox.USE_SYSTEM_PASSWORD_CHAR);

		// Token: 0x04001389 RID: 5001
		private static int CUTCOPYINCLUDEPROMPT = BitVector32.CreateMask(MaskedTextBox.INSERT_TOGGLED);

		// Token: 0x0400138A RID: 5002
		private static int CUTCOPYINCLUDELITERALS = BitVector32.CreateMask(MaskedTextBox.CUTCOPYINCLUDEPROMPT);

		// Token: 0x0400138B RID: 5003
		private char passwordChar;

		// Token: 0x0400138C RID: 5004
		private Type validatingType;

		// Token: 0x0400138D RID: 5005
		private IFormatProvider formatProvider;

		// Token: 0x0400138E RID: 5006
		private MaskedTextProvider maskedTextProvider;

		// Token: 0x0400138F RID: 5007
		private InsertKeyMode insertMode;

		// Token: 0x04001390 RID: 5008
		private HorizontalAlignment textAlign;

		// Token: 0x04001391 RID: 5009
		private BitVector32 flagState;
	}
}
