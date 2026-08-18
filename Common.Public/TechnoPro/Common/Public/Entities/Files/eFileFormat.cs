using System;

namespace TechnoPro.Common.Public.Entities.Files
{
	// Token: 0x02000338 RID: 824
	[Serializable]
	public enum eFileFormat
	{
		// Token: 0x040014DC RID: 5340
		[FileFormat("")]
		Unknown,
		// Token: 0x040014DD RID: 5341
		[FileFormat(".doc")]
		Word,
		// Token: 0x040014DE RID: 5342
		[FileFormat(".docx")]
		WordX,
		// Token: 0x040014DF RID: 5343
		[FileFormat(".pdf")]
		PDF = 4,
		// Token: 0x040014E0 RID: 5344
		[FileFormat(".html")]
		Html = 8,
		// Token: 0x040014E1 RID: 5345
		[FileFormat(".txt")]
		Text = 16
	}
}
