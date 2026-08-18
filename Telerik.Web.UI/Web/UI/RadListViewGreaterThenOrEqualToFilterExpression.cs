using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001986 RID: 6534
	[Browsable(false)]
	public class RadListViewGreaterThenOrEqualToFilterExpression<T> : RadListViewSingleValueExpression<T>
	{
		// Token: 0x0600FD0F RID: 64783 RVA: 0x0038E837 File Offset: 0x0038CA37
		internal RadListViewGreaterThenOrEqualToFilterExpression()
		{
		}

		// Token: 0x0600FD10 RID: 64784 RVA: 0x0038E83F File Offset: 0x0038CA3F
		public RadListViewGreaterThenOrEqualToFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C63 RID: 19555
		// (get) Token: 0x0600FD11 RID: 64785 RVA: 0x0038E848 File Offset: 0x0038CA48
		[Browsable(false)]
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.GreaterThanOrEqualTo;
			}
		}

		// Token: 0x17004C64 RID: 19556
		// (get) Token: 0x0600FD12 RID: 64786 RVA: 0x0038E84B File Offset: 0x0038CA4B
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0} >= {1}";
			}
		}

		// Token: 0x17004C65 RID: 19557
		// (get) Token: 0x0600FD13 RID: 64787 RVA: 0x0038E852 File Offset: 0x0038CA52
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} >= {1}";
			}
		}

		// Token: 0x17004C66 RID: 19558
		// (get) Token: 0x0600FD14 RID: 64788 RVA: 0x0038E859 File Offset: 0x0038CA59
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} >= {1}";
			}
		}

		// Token: 0x0600FD15 RID: 64789 RVA: 0x0038E8E9 File Offset: 0x0038CAE9
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return (obj == null && this.CurrentValue == null) || (obj == DBNull.Value && this.CurrentValue == Convert.DBNull);
				}
				return Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) > 0 || Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) == 0;
			};
		}
	}
}
