using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200009B RID: 155
	public interface IQueryableDataSource : IDataSource
	{
		// Token: 0x060006B8 RID: 1720
		void RaiseViewChanged();

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060006B9 RID: 1721
		// (remove) Token: 0x060006BA RID: 1722
		event EventHandler<QueryCreatedEventArgs> QueryCreated;
	}
}
