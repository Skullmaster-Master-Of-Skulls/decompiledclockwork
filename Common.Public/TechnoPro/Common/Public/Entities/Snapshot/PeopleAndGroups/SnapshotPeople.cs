using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.PeopleAndGroups
{
	// Token: 0x020001BD RID: 445
	public class SnapshotPeople
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x000143E8 File Offset: 0x000125E8
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x000143F0 File Offset: 0x000125F0
		public int PersonId { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000143F9 File Offset: 0x000125F9
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x00014401 File Offset: 0x00012601
		public byte[] FirstName { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0001440A File Offset: 0x0001260A
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x00014412 File Offset: 0x00012612
		public byte[] MiddleName { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0001441B File Offset: 0x0001261B
		// (set) Token: 0x06000BEA RID: 3050 RVA: 0x00014423 File Offset: 0x00012623
		public byte[] LastName { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0001442C File Offset: 0x0001262C
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00014434 File Offset: 0x00012634
		public byte[] Student_No { get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0001443D File Offset: 0x0001263D
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00014445 File Offset: 0x00012645
		public bool IsActive { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0001444E File Offset: 0x0001264E
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00014456 File Offset: 0x00012656
		public DateTime? DateAdded { get; set; }
	}
}
