using System;
using System.ComponentModel;
using System.Design;
using System.Globalization;
using System.Threading;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200030F RID: 783
	public abstract class MaskDescriptor
	{
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001EE7 RID: 7911
		public abstract string Mask { get; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001EE8 RID: 7912
		public abstract string Name { get; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001EE9 RID: 7913
		public abstract string Sample { get; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001EEA RID: 7914
		public abstract Type ValidatingType { get; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x000B8CB8 File Offset: 0x000B6EB8
		public virtual CultureInfo Culture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000B8CC4 File Offset: 0x000B6EC4
		public static bool IsValidMaskDescriptor(MaskDescriptor maskDescriptor)
		{
			string text;
			return MaskDescriptor.IsValidMaskDescriptor(maskDescriptor, out text);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000B8CDC File Offset: 0x000B6EDC
		public static bool IsValidMaskDescriptor(MaskDescriptor maskDescriptor, out string validationErrorDescription)
		{
			validationErrorDescription = string.Empty;
			if (maskDescriptor == null)
			{
				validationErrorDescription = SR.GetString("MaskDescriptorNull");
				return false;
			}
			if (string.IsNullOrEmpty(maskDescriptor.Mask) || string.IsNullOrEmpty(maskDescriptor.Name) || string.IsNullOrEmpty(maskDescriptor.Sample))
			{
				validationErrorDescription = SR.GetString("MaskDescriptorNullOrEmptyRequiredProperty");
				return false;
			}
			MaskedTextProvider maskedTextProvider = new MaskedTextProvider(maskDescriptor.Mask, maskDescriptor.Culture);
			MaskedTextBox maskedTextBox = new MaskedTextBox(maskedTextProvider);
			maskedTextBox.SkipLiterals = true;
			maskedTextBox.ResetOnPrompt = true;
			maskedTextBox.ResetOnSpace = true;
			maskedTextBox.ValidatingType = maskDescriptor.ValidatingType;
			maskedTextBox.FormatProvider = maskDescriptor.Culture;
			maskedTextBox.Culture = maskDescriptor.Culture;
			maskedTextBox.TypeValidationCompleted += MaskDescriptor.maskedTextBox1_TypeValidationCompleted;
			maskedTextBox.MaskInputRejected += MaskDescriptor.maskedTextBox1_MaskInputRejected;
			maskedTextBox.Text = maskDescriptor.Sample;
			if (maskedTextBox.Tag == null && maskDescriptor.ValidatingType != null)
			{
				maskedTextBox.ValidateText();
			}
			if (maskedTextBox.Tag != null)
			{
				validationErrorDescription = maskedTextBox.Tag.ToString();
			}
			return validationErrorDescription.Length == 0;
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x000B8DF4 File Offset: 0x000B6FF4
		private static void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{
			MaskedTextBox maskedTextBox = sender as MaskedTextBox;
			maskedTextBox.Tag = MaskedTextBoxDesigner.GetMaskInputRejectedErrorMessage(e);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000B8E14 File Offset: 0x000B7014
		private static void maskedTextBox1_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
		{
			if (!e.IsValidInput)
			{
				MaskedTextBox maskedTextBox = sender as MaskedTextBox;
				maskedTextBox.Tag = e.Message;
			}
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000B8E3C File Offset: 0x000B703C
		public override bool Equals(object maskDescriptor)
		{
			MaskDescriptor maskDescriptor2 = maskDescriptor as MaskDescriptor;
			if (!MaskDescriptor.IsValidMaskDescriptor(maskDescriptor2) || !MaskDescriptor.IsValidMaskDescriptor(this))
			{
				return this == maskDescriptor;
			}
			return this.Mask == maskDescriptor2.Mask && this.ValidatingType == maskDescriptor2.ValidatingType;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x000B8E8C File Offset: 0x000B708C
		public override int GetHashCode()
		{
			string text = this.Mask;
			if (this.ValidatingType != null)
			{
				text += this.ValidatingType.ToString();
			}
			return text.GetHashCode();
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000B8EC8 File Offset: 0x000B70C8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}<Name={1}, Mask={2}, ValidatingType={3}", new object[]
			{
				base.GetType(),
				(this.Name != null) ? this.Name : "null",
				(this.Mask != null) ? this.Mask : "null",
				(this.ValidatingType != null) ? this.ValidatingType.ToString() : "null"
			});
		}
	}
}
