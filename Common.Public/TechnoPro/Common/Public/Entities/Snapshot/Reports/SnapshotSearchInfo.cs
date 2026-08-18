using System;

namespace TechnoPro.Common.Public.Entities.Snapshot.Reports
{
	// Token: 0x020001BB RID: 443
	public class SnapshotSearchInfo
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x00014272 File Offset: 0x00012472
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0001427A File Offset: 0x0001247A
		public int SearchInfoId { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00014283 File Offset: 0x00012483
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0001428B File Offset: 0x0001248B
		public string Title { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00014294 File Offset: 0x00012494
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0001429C File Offset: 0x0001249C
		public string Description { get; set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x000142A5 File Offset: 0x000124A5
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x000142AD File Offset: 0x000124AD
		public int SearchGroupId { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000142B6 File Offset: 0x000124B6
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x000142BE File Offset: 0x000124BE
		public DateTime DateCreated { get; set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x000142C7 File Offset: 0x000124C7
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x000142CF File Offset: 0x000124CF
		public DateTime DateLastModified { get; set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x000142D8 File Offset: 0x000124D8
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x000142E0 File Offset: 0x000124E0
		public int WhoCreated { get; set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x000142E9 File Offset: 0x000124E9
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x000142F1 File Offset: 0x000124F1
		public int WhoLastModified { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x000142FA File Offset: 0x000124FA
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00014302 File Offset: 0x00012502
		public int OrderNum { get; set; }

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0001430B File Offset: 0x0001250B
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00014313 File Offset: 0x00012513
		public int SearchCharInfoId { get; set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0001431C File Offset: 0x0001251C
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00014324 File Offset: 0x00012524
		public int OverrideDynamicControlsScreenNum { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0001432D File Offset: 0x0001252D
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x00014335 File Offset: 0x00012535
		public string ReportOptions { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0001433E File Offset: 0x0001253E
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x00014346 File Offset: 0x00012546
		public byte[] TproNote { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0001434F File Offset: 0x0001254F
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x00014357 File Offset: 0x00012557
		public byte[] BuilByTpro { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00014360 File Offset: 0x00012560
		// (set) Token: 0x06000BD2 RID: 3026 RVA: 0x00014368 File Offset: 0x00012568
		public string CreatedByLocation { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00014371 File Offset: 0x00012571
		// (set) Token: 0x06000BD4 RID: 3028 RVA: 0x00014379 File Offset: 0x00012579
		public Guid ReportUniqueId { get; set; }
	}
}
