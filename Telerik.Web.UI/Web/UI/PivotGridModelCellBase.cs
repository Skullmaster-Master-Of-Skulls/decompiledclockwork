using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E05 RID: 3589
	[Serializable]
	internal abstract class PivotGridModelCellBase
	{
		// Token: 0x17002A12 RID: 10770
		// (get) Token: 0x0600850E RID: 34062 RVA: 0x001E6355 File Offset: 0x001E4555
		// (set) Token: 0x0600850F RID: 34063 RVA: 0x001E635D File Offset: 0x001E455D
		public PivotGridField Field
		{
			get
			{
				return this.field;
			}
			set
			{
				this.field = value;
			}
		}

		// Token: 0x17002A13 RID: 10771
		// (get) Token: 0x06008510 RID: 34064 RVA: 0x001E6366 File Offset: 0x001E4566
		// (set) Token: 0x06008511 RID: 34065 RVA: 0x001E636E File Offset: 0x001E456E
		public object[] RowIndexes
		{
			get
			{
				return this.rowIndexes;
			}
			set
			{
				this.rowIndexes = value;
			}
		}

		// Token: 0x17002A14 RID: 10772
		// (get) Token: 0x06008512 RID: 34066 RVA: 0x001E6377 File Offset: 0x001E4577
		// (set) Token: 0x06008513 RID: 34067 RVA: 0x001E637F File Offset: 0x001E457F
		public object[] ColumnIndexes
		{
			get
			{
				return this.columnIndexes;
			}
			set
			{
				this.columnIndexes = value;
			}
		}

		// Token: 0x17002A15 RID: 10773
		// (get) Token: 0x06008514 RID: 34068 RVA: 0x001E6388 File Offset: 0x001E4588
		// (set) Token: 0x06008515 RID: 34069 RVA: 0x001E6390 File Offset: 0x001E4590
		public object Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17002A16 RID: 10774
		// (get) Token: 0x06008516 RID: 34070 RVA: 0x001E6399 File Offset: 0x001E4599
		// (set) Token: 0x06008517 RID: 34071 RVA: 0x001E63A1 File Offset: 0x001E45A1
		public string FieldName { get; set; }

		// Token: 0x04002520 RID: 9504
		[NonSerialized]
		private PivotGridField field;

		// Token: 0x04002521 RID: 9505
		[NonSerialized]
		private object[] rowIndexes;

		// Token: 0x04002522 RID: 9506
		[NonSerialized]
		private object[] columnIndexes;

		// Token: 0x04002523 RID: 9507
		[NonSerialized]
		private object name;

		// Token: 0x04002524 RID: 9508
		[NonSerialized]
		internal PivotGridCell DataCell;
	}
}
