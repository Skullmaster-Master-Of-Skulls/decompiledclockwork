using System;

namespace System.Data.Entity.Migrations.Sql
{
	// Token: 0x02000715 RID: 1813
	public class MigrationStatement
	{
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06004969 RID: 18793 RVA: 0x0015F568 File Offset: 0x0015D768
		// (set) Token: 0x0600496A RID: 18794 RVA: 0x0015F570 File Offset: 0x0015D770
		public string Sql { get; set; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600496B RID: 18795 RVA: 0x0015F579 File Offset: 0x0015D779
		// (set) Token: 0x0600496C RID: 18796 RVA: 0x0015F581 File Offset: 0x0015D781
		public bool SuppressTransaction { get; set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600496D RID: 18797 RVA: 0x0015F58A File Offset: 0x0015D78A
		// (set) Token: 0x0600496E RID: 18798 RVA: 0x0015F592 File Offset: 0x0015D792
		public string BatchTerminator { get; set; }
	}
}
