using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA2 RID: 7074
	public abstract class GroupDescriptorBase : DescriptorBase, IGroupDescriptor, INotifyPropertyChanged
	{
		// Token: 0x060111E0 RID: 70112 RVA: 0x003C66BC File Offset: 0x003C48BC
		public virtual Expression CreateGroupKeyExpression(Expression itemExpression)
		{
			ParameterExpression parameterExpression = itemExpression as ParameterExpression;
			if (parameterExpression == null)
			{
				throw new ArgumentException("Parameter should be of type ParameterExpression", "itemExpression");
			}
			return this.CreateGroupKeyExpression(parameterExpression);
		}

		// Token: 0x060111E1 RID: 70113 RVA: 0x003C66EA File Offset: 0x003C48EA
		protected virtual Expression CreateGroupKeyExpression(ParameterExpression parameterExpression)
		{
			return parameterExpression;
		}

		// Token: 0x060111E2 RID: 70114 RVA: 0x003C66ED File Offset: 0x003C48ED
		public virtual Expression CreateGroupSortExpression(Expression groupingExpression)
		{
			return Expression.Property(groupingExpression, "Key");
		}

		// Token: 0x17005391 RID: 21393
		// (get) Token: 0x060111E3 RID: 70115 RVA: 0x003C66FA File Offset: 0x003C48FA
		// (set) Token: 0x060111E4 RID: 70116 RVA: 0x003C6704 File Offset: 0x003C4904
		public virtual ListSortDirection? SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				if (this.sortDirection != value)
				{
					this.sortDirection = value;
					base.OnPropertyChanged("SortDirection");
				}
			}
		}

		// Token: 0x060111E5 RID: 70117 RVA: 0x003C6754 File Offset: 0x003C4954
		public void CycleSortDirection()
		{
			this.SortDirection = GroupDescriptorBase.GetNextSortDirection(this.SortDirection);
		}

		// Token: 0x060111E6 RID: 70118 RVA: 0x003C6768 File Offset: 0x003C4968
		private static ListSortDirection? GetNextSortDirection(ListSortDirection? sortDirection)
		{
			ListSortDirection valueOrDefault = sortDirection.GetValueOrDefault();
			if (sortDirection != null)
			{
				switch (valueOrDefault)
				{
				case ListSortDirection.Ascending:
					return new ListSortDirection?(ListSortDirection.Descending);
				case ListSortDirection.Descending:
					return null;
				}
			}
			return new ListSortDirection?(ListSortDirection.Ascending);
		}

		// Token: 0x04004CA9 RID: 19625
		private ListSortDirection? sortDirection;
	}
}
