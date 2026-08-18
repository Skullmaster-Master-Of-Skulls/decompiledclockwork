using System;

namespace TechnoPro.Common.Public.Entities.Files.FileUpload
{
	// Token: 0x0200033B RID: 827
	[Serializable]
	public enum eTempFileUsage
	{
		// Token: 0x040014E9 RID: 5353
		[TempFileUsage("UNK")]
		Unknown,
		// Token: 0x040014EA RID: 5354
		[TempFileUsage("PRF")]
		InstructorUpload,
		// Token: 0x040014EB RID: 5355
		[TempFileUsage("CUF")]
		CustomFileUpload
	}
}
