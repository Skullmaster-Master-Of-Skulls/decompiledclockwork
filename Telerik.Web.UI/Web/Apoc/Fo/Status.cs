using System;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015C1 RID: 5569
	internal struct Status
	{
		// Token: 0x0600D944 RID: 55620 RVA: 0x002FB2C1 File Offset: 0x002F94C1
		public Status(int code)
		{
			this.code = code;
		}

		// Token: 0x0600D945 RID: 55621 RVA: 0x002FB2CA File Offset: 0x002F94CA
		public int getCode()
		{
			return this.code;
		}

		// Token: 0x0600D946 RID: 55622 RVA: 0x002FB2D2 File Offset: 0x002F94D2
		public bool isIncomplete()
		{
			return this.code != 1 && this.code != 8;
		}

		// Token: 0x0600D947 RID: 55623 RVA: 0x002FB2EB File Offset: 0x002F94EB
		public bool laidOutNone()
		{
			return this.code == 2;
		}

		// Token: 0x0600D948 RID: 55624 RVA: 0x002FB2F6 File Offset: 0x002F94F6
		public bool isPageBreak()
		{
			return this.code == 4 || this.code == 5 || this.code == 6;
		}

		// Token: 0x04003C0D RID: 15373
		public const int OK = 1;

		// Token: 0x04003C0E RID: 15374
		public const int AREA_FULL_NONE = 2;

		// Token: 0x04003C0F RID: 15375
		public const int AREA_FULL_SOME = 3;

		// Token: 0x04003C10 RID: 15376
		public const int FORCE_PAGE_BREAK = 4;

		// Token: 0x04003C11 RID: 15377
		public const int FORCE_PAGE_BREAK_EVEN = 5;

		// Token: 0x04003C12 RID: 15378
		public const int FORCE_PAGE_BREAK_ODD = 6;

		// Token: 0x04003C13 RID: 15379
		public const int FORCE_COLUMN_BREAK = 7;

		// Token: 0x04003C14 RID: 15380
		public const int KEEP_WITH_NEXT = 8;

		// Token: 0x04003C15 RID: 15381
		private int code;
	}
}
