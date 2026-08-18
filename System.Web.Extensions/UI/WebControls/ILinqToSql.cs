using System;
using System.Data.Linq;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000098 RID: 152
	internal interface ILinqToSql
	{
		// Token: 0x060006AD RID: 1709
		void Add(ITable table, object row);

		// Token: 0x060006AE RID: 1710
		void Attach(ITable table, object row);

		// Token: 0x060006AF RID: 1711
		object GetOriginalEntityState(ITable table, object row);

		// Token: 0x060006B0 RID: 1712
		void Refresh(DataContext dataContext, RefreshMode mode, object entity);

		// Token: 0x060006B1 RID: 1713
		void Remove(ITable table, object row);

		// Token: 0x060006B2 RID: 1714
		void SubmitChanges(DataContext dataContext);
	}
}
