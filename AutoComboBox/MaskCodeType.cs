using System;

namespace AutoComboBox
{
	// Token: 0x0200002F RID: 47
	public enum MaskCodeType : byte
	{
		// Token: 0x04000189 RID: 393
		required_digit = 48,
		// Token: 0x0400018A RID: 394
		optional_digit = 57,
		// Token: 0x0400018B RID: 395
		required_alphanumeric = 65,
		// Token: 0x0400018C RID: 396
		optional_alphanumeric = 97,
		// Token: 0x0400018D RID: 397
		required_unicode = 38,
		// Token: 0x0400018E RID: 398
		optional_unicode = 67,
		// Token: 0x0400018F RID: 399
		optional_digit_or_space_or_plus_or_minus_symbol = 35,
		// Token: 0x04000190 RID: 400
		required_letter = 76,
		// Token: 0x04000191 RID: 401
		optional_letter = 63,
		// Token: 0x04000192 RID: 402
		force_to_lower_case = 60,
		// Token: 0x04000193 RID: 403
		force_to_upper_case = 62,
		// Token: 0x04000194 RID: 404
		STATIC_CHAR = 32
	}
}
