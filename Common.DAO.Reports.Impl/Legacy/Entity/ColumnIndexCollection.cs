using System;
using System.Collections;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000011 RID: 17
	public class ColumnIndexCollection : CollectionBase
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00023B41 File Offset: 0x00021D41
		public virtual void Add(ColumnIndexClass NewColumnIndexClass)
		{
			base.List.Add(NewColumnIndexClass);
		}

		// Token: 0x17000025 RID: 37
		public virtual ColumnIndexClass this[int Index]
		{
			get
			{
				return (ColumnIndexClass)base.List[Index];
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00023B78 File Offset: 0x00021D78
		public virtual bool Contains(string colName)
		{
			string text = colName.ToLower().Trim();
			foreach (object obj in base.List)
			{
				ColumnIndexClass columnIndexClass = (ColumnIndexClass)obj;
				string text2 = columnIndexClass.ColName.Trim().ToLower();
				bool flag = text2.CompareTo(colName) == 0;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}
	}
}
