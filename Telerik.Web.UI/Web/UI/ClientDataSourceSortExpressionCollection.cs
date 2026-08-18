using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200011E RID: 286
	[PersistChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientDataSourceSortExpressionCollection : StronglyTypedStateManagedCollection<ClientDataSourceSortExpression>
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x000285A4 File Offset: 0x000267A4
		public RadListViewSortExpressionCollection ToListViewFilterExpression()
		{
			RadListViewSortExpressionCollection radListViewSortExpressionCollection = new RadListViewSortExpressionCollection();
			foreach (object obj in this)
			{
				ClientDataSourceSortExpression clientDataSourceSortExpression = (ClientDataSourceSortExpression)obj;
				radListViewSortExpressionCollection.Add(new RadListViewSortExpression
				{
					FieldName = clientDataSourceSortExpression.FieldName,
					SortOrder = ((clientDataSourceSortExpression.SortOrder == ClientDataSourceSortOrder.Asc) ? RadListViewSortOrder.Ascending : RadListViewSortOrder.Descending)
				});
			}
			return radListViewSortExpressionCollection;
		}
	}
}
