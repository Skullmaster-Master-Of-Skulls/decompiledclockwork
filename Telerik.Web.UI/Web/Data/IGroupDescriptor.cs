using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA1 RID: 7073
	public interface IGroupDescriptor : INotifyPropertyChanged
	{
		// Token: 0x060111DD RID: 70109
		Expression CreateGroupKeyExpression(Expression itemExpression);

		// Token: 0x060111DE RID: 70110
		Expression CreateGroupSortExpression(Expression groupingExpression);

		// Token: 0x17005390 RID: 21392
		// (get) Token: 0x060111DF RID: 70111
		ListSortDirection? SortDirection { get; }
	}
}
