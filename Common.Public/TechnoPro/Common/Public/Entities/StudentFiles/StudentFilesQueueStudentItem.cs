using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x02000190 RID: 400
	public class StudentFilesQueueStudentItem
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00013255 File Offset: 0x00011455
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0001325D File Offset: 0x0001145D
		public int PersonId { get; set; }

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00013266 File Offset: 0x00011466
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0001326E File Offset: 0x0001146E
		public string FirstName { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00013277 File Offset: 0x00011477
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0001327F File Offset: 0x0001147F
		public string MiddleName { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00013288 File Offset: 0x00011488
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00013290 File Offset: 0x00011490
		public string LastName { get; set; }

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00013299 File Offset: 0x00011499
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x000132A1 File Offset: 0x000114A1
		public string StudentNumber { get; set; }

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x000132AA File Offset: 0x000114AA
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x000132B2 File Offset: 0x000114B2
		public string Email { get; set; }

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x000132BB File Offset: 0x000114BB
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x000132C3 File Offset: 0x000114C3
		public string AssignedCounsellorFirstName { get; set; }

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x000132CC File Offset: 0x000114CC
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x000132D4 File Offset: 0x000114D4
		public string AssignedCounsellorLastName { get; set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000132DD File Offset: 0x000114DD
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x000132E5 File Offset: 0x000114E5
		public int AssignedCounsellorPersonId { get; set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000132EE File Offset: 0x000114EE
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x000132F6 File Offset: 0x000114F6
		public int DataId { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000132FF File Offset: 0x000114FF
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x00013307 File Offset: 0x00011507
		public IList<StudentFilesQueueFileItem> FileItems { get; set; }
	}
}
