using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002E1 RID: 737
	internal class RecordStateScratchpad
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00080CE1 File Offset: 0x0007EEE1
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x00080CE9 File Offset: 0x0007EEE9
		internal int StateSlotNumber { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x00080CF2 File Offset: 0x0007EEF2
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x00080CFA File Offset: 0x0007EEFA
		internal int ColumnCount { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x00080D03 File Offset: 0x0007EF03
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x00080D0B File Offset: 0x0007EF0B
		internal DataRecordInfo DataRecordInfo { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x00080D14 File Offset: 0x0007EF14
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x00080D1C File Offset: 0x0007EF1C
		internal Expression GatherData { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00080D25 File Offset: 0x0007EF25
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00080D2D File Offset: 0x0007EF2D
		internal string[] PropertyNames { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00080D36 File Offset: 0x0007EF36
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00080D3E File Offset: 0x0007EF3E
		internal TypeUsage[] TypeUsages { get; set; }

		// Token: 0x060019F5 RID: 6645 RVA: 0x00080D48 File Offset: 0x0007EF48
		internal RecordStateFactory Compile()
		{
			RecordStateFactory[] array = new RecordStateFactory[this._nestedRecordStateScratchpads.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._nestedRecordStateScratchpads[i].Compile();
			}
			return (RecordStateFactory)Activator.CreateInstance(typeof(RecordStateFactory), new object[]
			{
				this.StateSlotNumber,
				this.ColumnCount,
				array,
				this.DataRecordInfo,
				this.GatherData,
				this.PropertyNames,
				this.TypeUsages
			});
		}

		// Token: 0x040008F6 RID: 2294
		private readonly List<RecordStateScratchpad> _nestedRecordStateScratchpads = new List<RecordStateScratchpad>();
	}
}
