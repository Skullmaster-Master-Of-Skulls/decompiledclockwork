using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Tasks
{
	// Token: 0x0200017A RID: 378
	public class TaskNote : BusinessBase<int>
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x00012A90 File Offset: 0x00010C90
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TaskNoteId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00012AA8 File Offset: 0x00010CA8
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x00012AB0 File Offset: 0x00010CB0
		public PersonBase WhoEntered { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00012AB9 File Offset: 0x00010CB9
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00012AC1 File Offset: 0x00010CC1
		public DateTime DateEntered { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00012ACA File Offset: 0x00010CCA
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00012AD2 File Offset: 0x00010CD2
		public PersonBase WhoLastModified { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00012ADB File Offset: 0x00010CDB
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x00012AE3 File Offset: 0x00010CE3
		public DateTime DateLastModified { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00012AEC File Offset: 0x00010CEC
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x00012AF4 File Offset: 0x00010CF4
		public string Notes { get; set; }
	}
}
