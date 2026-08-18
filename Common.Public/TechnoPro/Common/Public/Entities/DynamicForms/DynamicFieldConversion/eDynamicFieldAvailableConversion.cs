using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion
{
	// Token: 0x02000381 RID: 897
	public enum eDynamicFieldAvailableConversion
	{
		// Token: 0x04001658 RID: 5720
		[DynamicFieldAvailableConversion("Encrypted textbox to plain-text textbox", "Will de-crypt all of the textbox data and change the textbox type for future saving of data.", eDynamicFieldConversionFieldInfo.TextBoxEncrypted, eDynamicFieldConversionFieldInfo.TextBoxPlainText)]
		EncryptedTextBoxToPlainTextBox,
		// Token: 0x04001659 RID: 5721
		[DynamicFieldAvailableConversion("Plain-text textbox to encrypted textbox", "Will encrypt all of the textbox data and change the textbox type for future saving of data.", eDynamicFieldConversionFieldInfo.TextBoxPlainText, eDynamicFieldConversionFieldInfo.TextBoxEncrypted)]
		PlainTextBoxToEncryptedTextBox
	}
}
