using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ClockWorkSnapshot
{
	// Token: 0x02000448 RID: 1096
	public class ClockWorkSnapshotTableRestoreResult
	{
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x000254DB File Offset: 0x000236DB
		// (set) Token: 0x06002137 RID: 8503 RVA: 0x000254E3 File Offset: 0x000236E3
		public ClockWorkSnapshotTable SnapshotTable { get; set; }

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x000254EC File Offset: 0x000236EC
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x000254F4 File Offset: 0x000236F4
		public int? RowCountFromSnapshot { get; set; }

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x000254FD File Offset: 0x000236FD
		// (set) Token: 0x0600213B RID: 8507 RVA: 0x00025505 File Offset: 0x00023705
		public int? RowCountFromExistingDatabase { get; set; }

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0002550E File Offset: 0x0002370E
		// (set) Token: 0x0600213D RID: 8509 RVA: 0x00025516 File Offset: 0x00023716
		public List<string> ErrorMessages { get; set; }

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x0002551F File Offset: 0x0002371F
		// (set) Token: 0x0600213F RID: 8511 RVA: 0x00025527 File Offset: 0x00023727
		public bool StartedProcessing { get; set; }
	}
}
