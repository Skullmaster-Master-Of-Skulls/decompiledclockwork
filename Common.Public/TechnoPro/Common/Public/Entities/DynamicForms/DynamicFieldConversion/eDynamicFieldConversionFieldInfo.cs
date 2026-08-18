using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicFieldConversion
{
	// Token: 0x02000383 RID: 899
	[Flags]
	public enum eDynamicFieldConversionFieldInfo
	{
		// Token: 0x04001660 RID: 5728
		[DynamicFieldConversionFieldInfo(eControlCode.TextBox, 1)]
		TextBoxEncrypted = 0,
		// Token: 0x04001661 RID: 5729
		[DynamicFieldConversionFieldInfo(eControlCode.TextBox, 0)]
		TextBoxPlainText = 1
	}
}
