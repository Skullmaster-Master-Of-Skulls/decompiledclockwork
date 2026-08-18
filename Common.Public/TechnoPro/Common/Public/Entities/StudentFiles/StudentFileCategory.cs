using System;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x02000186 RID: 390
	public class StudentFileCategory : BusinessBase<string>, ICloneable<StudentFileCategory>, ICloneable
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x00011AE5 File Offset: 0x0000FCE5
		public StudentFileCategory()
		{
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00012ED8 File Offset: 0x000110D8
		public StudentFileCategory(StudentFileCategory item)
		{
			StudentFileCategory.CopyItem(item, this);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00012EEC File Offset: 0x000110EC
		private static void CopyItem(StudentFileCategory source, StudentFileCategory dest)
		{
			bool flag = source == null;
			if (!flag)
			{
				dest.Title = source.Title;
				StudentFileCategoryField[] fields = source.Fields;
				StudentFileCategoryField[] fields2;
				if (fields == null)
				{
					fields2 = null;
				}
				else
				{
					fields2 = (from g in fields
					select g.Clone()).ToArray<StudentFileCategoryField>();
				}
				dest.Fields = fields2;
				dest.IsDisabled = source.IsDisabled;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00012F5C File Offset: 0x0001115C
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string Title
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00012F74 File Offset: 0x00011174
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00012F7C File Offset: 0x0001117C
		public StudentFileCategoryField[] Fields { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x00012F85 File Offset: 0x00011185
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x00012F8D File Offset: 0x0001118D
		public bool IsDisabled { get; set; }

		// Token: 0x060009CB RID: 2507 RVA: 0x00012F98 File Offset: 0x00011198
		public StudentFileCategory Clone()
		{
			return new StudentFileCategory(this);
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00012FB0 File Offset: 0x000111B0
		public T Clone<T>() where T : StudentFileCategory
		{
			T t = Activator.CreateInstance<T>();
			StudentFileCategory.CopyItem(this, t);
			return t;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00012FD8 File Offset: 0x000111D8
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
