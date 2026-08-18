using System;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x02000187 RID: 391
	public class StudentFileCategoryField : ICloneable<StudentFileCategoryField>, ICloneable
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x0000D55A File Offset: 0x0000B75A
		public StudentFileCategoryField()
		{
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00012FF0 File Offset: 0x000111F0
		public StudentFileCategoryField(StudentFileCategoryField item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.ControlId = item.ControlId;
				this.FormType = item.FormType;
				this.FieldType = item.FieldType;
				this.NoteColumns = item.NoteColumns;
				this.FilenameFilter = item.FilenameFilter;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x00013050 File Offset: 0x00011250
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x00013058 File Offset: 0x00011258
		public int ControlId { get; set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x00013061 File Offset: 0x00011261
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00013069 File Offset: 0x00011269
		public eStudentFileCategoryFormType FormType { get; set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00013072 File Offset: 0x00011272
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0001307A File Offset: 0x0001127A
		public eStudentFileCategoryFieldType FieldType { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00013083 File Offset: 0x00011283
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0001308B File Offset: 0x0001128B
		public int[] NoteColumns { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00013094 File Offset: 0x00011294
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0001309C File Offset: 0x0001129C
		public string FilenameFilter { get; set; }

		// Token: 0x060009DA RID: 2522 RVA: 0x000130A8 File Offset: 0x000112A8
		public StudentFileCategoryField Clone()
		{
			return new StudentFileCategoryField(this);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000130C0 File Offset: 0x000112C0
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
