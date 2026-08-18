using System;
using System.Collections;

namespace ReportFunctions
{
	// Token: 0x02000046 RID: 70
	public class ColumnIndexCollection : CollectionBase
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x0004AB6A File Offset: 0x00049B6A
		public virtual void Add(ColumnIndexClass NewColumnIndexClass)
		{
			base.List.Add(NewColumnIndexClass);
		}

		// Token: 0x170000C9 RID: 201
		public virtual ColumnIndexClass this[int Index]
		{
			get
			{
				return (ColumnIndexClass)base.List[Index];
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0004ABA0 File Offset: 0x00049BA0
		public virtual bool Contains(string colName)
		{
			string text = colName.ToLower().Trim();
			foreach (object obj in base.List)
			{
				ColumnIndexClass columnIndexClass = (ColumnIndexClass)obj;
				string text2 = columnIndexClass.ColName.Trim().ToLower();
				if (text2.CompareTo(colName) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
