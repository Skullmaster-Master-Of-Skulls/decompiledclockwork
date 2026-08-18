using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000313 RID: 787
	internal class MaskedTextBoxDesigner : TextBoxBaseDesigner
	{
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x000BB0A0 File Offset: 0x000B92A0
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this.actions == null)
				{
					this.actions = new DesignerActionListCollection();
					this.actions.Add(new MaskedTextBoxDesignerActionList(this));
				}
				return this.actions;
			}
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000BB0D0 File Offset: 0x000B92D0
		internal static MaskedTextBox GetDesignMaskedTextBox(MaskedTextBox mtb)
		{
			MaskedTextBox maskedTextBox;
			if (mtb == null)
			{
				maskedTextBox = new MaskedTextBox();
			}
			else
			{
				if (mtb.MaskedTextProvider == null)
				{
					maskedTextBox = new MaskedTextBox();
					maskedTextBox.Text = mtb.Text;
				}
				else
				{
					maskedTextBox = new MaskedTextBox(mtb.MaskedTextProvider);
				}
				maskedTextBox.ValidatingType = mtb.ValidatingType;
				maskedTextBox.BeepOnError = mtb.BeepOnError;
				maskedTextBox.InsertKeyMode = mtb.InsertKeyMode;
				maskedTextBox.RejectInputOnFirstFailure = mtb.RejectInputOnFirstFailure;
				maskedTextBox.CutCopyMaskFormat = mtb.CutCopyMaskFormat;
				maskedTextBox.Culture = mtb.Culture;
			}
			maskedTextBox.UseSystemPasswordChar = false;
			maskedTextBox.PasswordChar = '\0';
			maskedTextBox.ReadOnly = false;
			maskedTextBox.HidePromptOnLeave = false;
			return maskedTextBox;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x000BB17C File Offset: 0x000B937C
		internal static string GetMaskInputRejectedErrorMessage(MaskInputRejectedEventArgs e)
		{
			MaskedTextResultHint rejectionHint = e.RejectionHint;
			string @string;
			switch (rejectionHint)
			{
			case MaskedTextResultHint.PositionOutOfRange:
				@string = SR.GetString("MaskedTextBoxHintPositionOutOfRange");
				goto IL_C7;
			case MaskedTextResultHint.NonEditPosition:
				@string = SR.GetString("MaskedTextBoxHintNonEditPosition");
				goto IL_C7;
			case MaskedTextResultHint.UnavailableEditPosition:
				@string = SR.GetString("MaskedTextBoxHintUnavailableEditPosition");
				goto IL_C7;
			case MaskedTextResultHint.PromptCharNotAllowed:
				@string = SR.GetString("MaskedTextBoxHintPromptCharNotAllowed");
				goto IL_C7;
			case MaskedTextResultHint.InvalidInput:
				break;
			default:
				switch (rejectionHint)
				{
				case MaskedTextResultHint.SignedDigitExpected:
					@string = SR.GetString("MaskedTextBoxHintSignedDigitExpected");
					goto IL_C7;
				case MaskedTextResultHint.LetterExpected:
					@string = SR.GetString("MaskedTextBoxHintLetterExpected");
					goto IL_C7;
				case MaskedTextResultHint.DigitExpected:
					@string = SR.GetString("MaskedTextBoxHintDigitExpected");
					goto IL_C7;
				case MaskedTextResultHint.AlphanumericCharacterExpected:
					@string = SR.GetString("MaskedTextBoxHintAlphanumericCharacterExpected");
					goto IL_C7;
				case MaskedTextResultHint.AsciiCharacterExpected:
					@string = SR.GetString("MaskedTextBoxHintAsciiCharacterExpected");
					goto IL_C7;
				}
				break;
			}
			@string = SR.GetString("MaskedTextBoxHintInvalidInput");
			IL_C7:
			return string.Format(CultureInfo.CurrentCulture, SR.GetString("MaskedTextBoxTextEditorErrorFormatString"), new object[]
			{
				e.Position,
				@string
			});
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00003937 File Offset: 0x00001B37
		[Obsolete("This method has been deprecated. Use InitializeNewComponent instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public override void OnSetComponentDefaults()
		{
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000BB27C File Offset: 0x000B947C
		private void OnVerbSetMask(object sender, EventArgs e)
		{
			MaskedTextBoxDesignerActionList maskedTextBoxDesignerActionList = new MaskedTextBoxDesignerActionList(this);
			maskedTextBoxDesignerActionList.SetMask();
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000BB298 File Offset: 0x000B9498
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"Text",
				"PasswordChar"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(MaskedTextBoxDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001F23 RID: 7971 RVA: 0x000BB304 File Offset: 0x000B9504
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				return selectionRules & ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x000BB320 File Offset: 0x000B9520
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x000BB360 File Offset: 0x000B9560
		private char PasswordChar
		{
			get
			{
				MaskedTextBox maskedTextBox = this.Control as MaskedTextBox;
				if (maskedTextBox.UseSystemPasswordChar)
				{
					maskedTextBox.UseSystemPasswordChar = false;
					char passwordChar = maskedTextBox.PasswordChar;
					maskedTextBox.UseSystemPasswordChar = true;
					return passwordChar;
				}
				return maskedTextBox.PasswordChar;
			}
			set
			{
				MaskedTextBox maskedTextBox = this.Control as MaskedTextBox;
				maskedTextBox.PasswordChar = value;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x000BB380 File Offset: 0x000B9580
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x000BB3BC File Offset: 0x000B95BC
		private string Text
		{
			get
			{
				MaskedTextBox maskedTextBox = this.Control as MaskedTextBox;
				if (string.IsNullOrEmpty(maskedTextBox.Mask))
				{
					return maskedTextBox.Text;
				}
				return maskedTextBox.MaskedTextProvider.ToString(false, false);
			}
			set
			{
				MaskedTextBox maskedTextBox = this.Control as MaskedTextBox;
				if (string.IsNullOrEmpty(maskedTextBox.Mask))
				{
					maskedTextBox.Text = value;
					return;
				}
				bool resetOnSpace = maskedTextBox.ResetOnSpace;
				bool resetOnPrompt = maskedTextBox.ResetOnPrompt;
				bool skipLiterals = maskedTextBox.SkipLiterals;
				maskedTextBox.ResetOnSpace = true;
				maskedTextBox.ResetOnPrompt = true;
				maskedTextBox.SkipLiterals = true;
				maskedTextBox.Text = value;
				maskedTextBox.ResetOnSpace = resetOnSpace;
				maskedTextBox.ResetOnPrompt = resetOnPrompt;
				maskedTextBox.SkipLiterals = skipLiterals;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x000BB430 File Offset: 0x000B9630
		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (this.verbs == null)
				{
					this.verbs = new DesignerVerbCollection();
					this.verbs.Add(new DesignerVerb(SR.GetString("MaskedTextBoxDesignerVerbsSetMaskDesc"), new EventHandler(this.OnVerbSetMask)));
				}
				return this.verbs;
			}
		}

		// Token: 0x040017FF RID: 6143
		private DesignerVerbCollection verbs;

		// Token: 0x04001800 RID: 6144
		private DesignerActionListCollection actions;
	}
}
