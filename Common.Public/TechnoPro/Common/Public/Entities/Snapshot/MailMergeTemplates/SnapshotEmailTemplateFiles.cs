using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.MailMergeTemplates
{
	// Token: 0x020001C4 RID: 452
	public class SnapshotEmailTemplateFiles
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0001465D File Offset: 0x0001285D
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00014665 File Offset: 0x00012865
		public int FileId { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0001466E File Offset: 0x0001286E
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00014676 File Offset: 0x00012876
		public string FileName { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0001467F File Offset: 0x0001287F
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x00014687 File Offset: 0x00012887
		public byte[] FileBytes { get; set; }
	}
}
