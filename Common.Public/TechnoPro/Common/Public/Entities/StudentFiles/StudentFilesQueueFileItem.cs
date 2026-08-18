using System;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x0200018D RID: 397
	public class StudentFilesQueueFileItem
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0001314F File Offset: 0x0001134F
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00013157 File Offset: 0x00011357
		public int FileId { get; set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00013160 File Offset: 0x00011360
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x00013168 File Offset: 0x00011368
		public StudentFilesStatus Status { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00013171 File Offset: 0x00011371
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00013179 File Offset: 0x00011379
		public string StudentComment { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00013182 File Offset: 0x00011382
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0001318A File Offset: 0x0001138A
		public string StaffComment { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00013193 File Offset: 0x00011393
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0001319B File Offset: 0x0001139B
		public string DateAddedStr { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000131A4 File Offset: 0x000113A4
		public DateTime? DateAdded
		{
			get
			{
				DateTime value;
				return (string.IsNullOrWhiteSpace(this.DateAddedStr) || !DateTime.TryParse(this.DateAddedStr, out value)) ? null : new DateTime?(value);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x000131DE File Offset: 0x000113DE
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x000131E6 File Offset: 0x000113E6
		public string FileName { get; set; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x000131EF File Offset: 0x000113EF
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x000131F7 File Offset: 0x000113F7
		public string[] OriginalColumn { get; set; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00013200 File Offset: 0x00011400
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x00013208 File Offset: 0x00011408
		public bool WasModified { get; set; }
	}
}
