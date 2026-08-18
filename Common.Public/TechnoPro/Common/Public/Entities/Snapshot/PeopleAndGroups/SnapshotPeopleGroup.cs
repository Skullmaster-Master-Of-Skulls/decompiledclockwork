using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.PeopleAndGroups
{
	// Token: 0x020001BE RID: 446
	public class SnapshotPeopleGroup
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0001445F File Offset: 0x0001265F
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00014467 File Offset: 0x00012667
		public int PersonGroupId { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00014470 File Offset: 0x00012670
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00014478 File Offset: 0x00012678
		public int PersonId { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00014481 File Offset: 0x00012681
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x00014489 File Offset: 0x00012689
		public int GroupId { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00014492 File Offset: 0x00012692
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x0001449A File Offset: 0x0001269A
		public bool IsPrimaryGroup { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x000144A3 File Offset: 0x000126A3
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x000144AB File Offset: 0x000126AB
		public int OrderNum { get; set; }
	}
}
