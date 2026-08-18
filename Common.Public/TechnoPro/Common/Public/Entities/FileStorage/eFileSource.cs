using System;

namespace TechnoPro.Common.Public.Entities.FileStorage
{
	// Token: 0x02000344 RID: 836
	[Serializable]
	public enum eFileSource
	{
		// Token: 0x040014FE RID: 5374
		Unknown,
		// Token: 0x040014FF RID: 5375
		AlternativeFormat_MediaContentFile,
		// Token: 0x04001500 RID: 5376
		AlternativeFormat_MediaContentCoverImage,
		// Token: 0x04001501 RID: 5377
		AlternativeFormat_MediaContentThumbnail,
		// Token: 0x04001502 RID: 5378
		AlternativeFormat_ProofOfPurchaseReceipt,
		// Token: 0x04001503 RID: 5379
		Inventory_ProductAttachment,
		// Token: 0x04001504 RID: 5380
		Inventory_ProductImage,
		// Token: 0x04001505 RID: 5381
		TestBooking_ClassTestDefinition,
		// Token: 0x04001506 RID: 5382
		ServiceProvider_LectureNote,
		// Token: 0x04001507 RID: 5383
		DynamicForms_SingleFile,
		// Token: 0x04001508 RID: 5384
		DynamicForms_FileList,
		// Token: 0x04001509 RID: 5385
		Email_TemplateAttachment,
		// Token: 0x0400150A RID: 5386
		CustomForms_Files
	}
}
