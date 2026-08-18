using System;
using System.Data.Linq;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000A7 RID: 167
	internal class LinqToSqlWrapper : ILinqToSql
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0001E62B File Offset: 0x0001C82B
		public void Add(ITable table, object row)
		{
			table.InsertOnSubmit(row);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001E634 File Offset: 0x0001C834
		public void Attach(ITable table, object row)
		{
			table.Attach(row);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001E63D File Offset: 0x0001C83D
		public object GetOriginalEntityState(ITable table, object row)
		{
			return table.GetOriginalEntityState(row);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001E646 File Offset: 0x0001C846
		public void Refresh(DataContext dataContext, RefreshMode mode, object entity)
		{
			dataContext.Refresh(mode, entity);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001E650 File Offset: 0x0001C850
		public void Remove(ITable table, object row)
		{
			table.DeleteOnSubmit(row);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001E659 File Offset: 0x0001C859
		public void SubmitChanges(DataContext dataContext)
		{
			dataContext.SubmitChanges();
		}
	}
}
