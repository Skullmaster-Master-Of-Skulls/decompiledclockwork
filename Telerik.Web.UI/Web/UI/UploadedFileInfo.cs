using System;
using System.IO;

namespace Telerik.Web.UI
{
	// Token: 0x020016AC RID: 5804
	public class UploadedFileInfo : IAsyncUploadResult
	{
		// Token: 0x170044AB RID: 17579
		// (get) Token: 0x0600E021 RID: 57377 RVA: 0x0031DD6E File Offset: 0x0031BF6E
		// (set) Token: 0x0600E022 RID: 57378 RVA: 0x0031DD76 File Offset: 0x0031BF76
		public string FileName { get; set; }

		// Token: 0x170044AC RID: 17580
		// (get) Token: 0x0600E023 RID: 57379 RVA: 0x0031DD7F File Offset: 0x0031BF7F
		// (set) Token: 0x0600E024 RID: 57380 RVA: 0x0031DD87 File Offset: 0x0031BF87
		public string ContentType { get; set; }

		// Token: 0x170044AD RID: 17581
		// (get) Token: 0x0600E025 RID: 57381 RVA: 0x0031DD90 File Offset: 0x0031BF90
		// (set) Token: 0x0600E026 RID: 57382 RVA: 0x0031DD98 File Offset: 0x0031BF98
		public long ContentLength { get; set; }

		// Token: 0x170044AE RID: 17582
		// (get) Token: 0x0600E027 RID: 57383 RVA: 0x0031DDA1 File Offset: 0x0031BFA1
		// (set) Token: 0x0600E028 RID: 57384 RVA: 0x0031DDA9 File Offset: 0x0031BFA9
		internal DateTime LastModifiedDate { get; set; }

		// Token: 0x170044AF RID: 17583
		// (get) Token: 0x0600E029 RID: 57385 RVA: 0x0031DDB2 File Offset: 0x0031BFB2
		// (set) Token: 0x0600E02A RID: 57386 RVA: 0x0031DDBA File Offset: 0x0031BFBA
		public string DateJson { get; internal set; }

		// Token: 0x170044B0 RID: 17584
		// (get) Token: 0x0600E02B RID: 57387 RVA: 0x0031DDC3 File Offset: 0x0031BFC3
		// (set) Token: 0x0600E02C RID: 57388 RVA: 0x0031DDCB File Offset: 0x0031BFCB
		public int Index { get; set; }

		// Token: 0x170044B1 RID: 17585
		// (get) Token: 0x0600E02D RID: 57389 RVA: 0x0031DDD4 File Offset: 0x0031BFD4
		// (set) Token: 0x0600E02E RID: 57390 RVA: 0x0031DDDC File Offset: 0x0031BFDC
		internal string SerializedData { get; set; }

		// Token: 0x170044B2 RID: 17586
		// (get) Token: 0x0600E02F RID: 57391 RVA: 0x0031DDE5 File Offset: 0x0031BFE5
		// (set) Token: 0x0600E030 RID: 57392 RVA: 0x0031DDED File Offset: 0x0031BFED
		internal string FileType { get; set; }

		// Token: 0x170044B3 RID: 17587
		// (get) Token: 0x0600E031 RID: 57393 RVA: 0x0031DDF6 File Offset: 0x0031BFF6
		// (set) Token: 0x0600E032 RID: 57394 RVA: 0x0031DDFE File Offset: 0x0031BFFE
		internal string TempFileName { get; set; }

		// Token: 0x0600E033 RID: 57395 RVA: 0x0031DE07 File Offset: 0x0031C007
		public UploadedFileInfo()
		{
		}

		// Token: 0x0600E034 RID: 57396 RVA: 0x0031DE0F File Offset: 0x0031C00F
		public UploadedFileInfo(UploadedFile file)
		{
			UploadedFileInfo.CopyFileInfo(this, file);
		}

		// Token: 0x0600E035 RID: 57397 RVA: 0x0031DE20 File Offset: 0x0031C020
		public static void CopyFileInfo(IAsyncUploadResult result, UploadedFile file)
		{
			result.ContentLength = file.ContentLength;
			result.ContentType = file.ContentType;
			result.FileName = Path.GetFileName(file.FileName);
			AsyncPostedFile asyncPostedFile = file as AsyncPostedFile;
			UploadedFileInfo uploadedFileInfo = result as UploadedFileInfo;
			if (uploadedFileInfo != null && asyncPostedFile != null)
			{
				uploadedFileInfo.DateJson = asyncPostedFile.lastModifiedDateInJson;
			}
		}
	}
}
