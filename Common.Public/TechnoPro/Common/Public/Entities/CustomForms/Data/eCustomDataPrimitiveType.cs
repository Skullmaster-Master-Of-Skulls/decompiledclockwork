using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data
{
	// Token: 0x02000425 RID: 1061
	[Serializable]
	public enum eCustomDataPrimitiveType
	{
		// Token: 0x040018A7 RID: 6311
		[CustomDataPrimitiveType(IsHidden = true)]
		Unknown,
		// Token: 0x040018A8 RID: 6312
		[CustomDataPrimitiveType("str")]
		String,
		// Token: 0x040018A9 RID: 6313
		[CustomDataPrimitiveType("int")]
		Int,
		// Token: 0x040018AA RID: 6314
		[CustomDataPrimitiveType("file")]
		File,
		// Token: 0x040018AB RID: 6315
		[CustomDataPrimitiveType("bool")]
		Boolean,
		// Token: 0x040018AC RID: 6316
		[CustomDataPrimitiveType("date")]
		DateTime,
		// Token: 0x040018AD RID: 6317
		[CustomDataPrimitiveType("item")]
		ListItem,
		// Token: 0x040018AE RID: 6318
		[CustomDataPrimitiveType("booln")]
		BooleanNullable
	}
}
