using System;

namespace TechnoPro.Common.DAO.Impl.Vets.QueryStorage
{
	// Token: 0x02000023 RID: 35
	internal static class QueryStorageVetsChapter
	{
		// Token: 0x04000045 RID: 69
		internal const string QU_CHAPTER_DISABLED = "UPDATE VetsChapter SET IsDisabled=@disabled WHERE ChapterId=@id";

		// Token: 0x04000046 RID: 70
		internal const string QU_CHAPTER = "UPDATE VetsChapter SET IsDisabled=@disabled,ChapterTitle=@title,ChapterDescription=@description,ChapterFormId=@formid,OrderNum=@ordernum WHERE ChapterId=@id";

		// Token: 0x04000047 RID: 71
		internal const string QI_CHAPTER = "INSERT INTO VetsChapter (ChapterId,ChapterTitle,ChapterDescription,ChapterFormId,IsDisabled,OrderNum) VALUES (@id,@title,@description,@formid,@disabled,@ordernum)";

		// Token: 0x04000048 RID: 72
		internal const string QS_CHAPTERS_ENABLED = "SELECT ChapterId,ChapterTitle,ChapterDescription,ChapterFormId,IsDisabled,OrderNum FROM VetsChapter WHERE IsDisabled=0 ORDER BY OrderNum";

		// Token: 0x04000049 RID: 73
		internal const string QD_CHAPTER = "IF NOT EXISTS(SELECT TOP 1 ChapterId FROM VetsBenefitApplication WHERE ChapterId=@id) \r\n    DELETE FROM VetsChapter WHERE ChapterId=@id\r\nSELECT COUNT(ChapterId) AS ct FROM VetsChapter WHERE ChapterId=@id";
	}
}
