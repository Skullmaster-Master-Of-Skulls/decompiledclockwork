using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001991 RID: 6545
	[Browsable(false)]
	public class RadListViewLessThanOrEqualToFilterExpression<T> : RadListViewSingleValueExpression<T>
	{
		// Token: 0x0600FD5D RID: 64861 RVA: 0x0038F173 File Offset: 0x0038D373
		internal RadListViewLessThanOrEqualToFilterExpression()
		{
		}

		// Token: 0x0600FD5E RID: 64862 RVA: 0x0038F17B File Offset: 0x0038D37B
		public RadListViewLessThanOrEqualToFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C77 RID: 19575
		// (get) Token: 0x0600FD5F RID: 64863 RVA: 0x0038F184 File Offset: 0x0038D384
		[Browsable(false)]
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.LessThanOrEqualTo;
			}
		}

		// Token: 0x17004C78 RID: 19576
		// (get) Token: 0x0600FD60 RID: 64864 RVA: 0x0038F187 File Offset: 0x0038D387
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0} <= {1}";
			}
		}

		// Token: 0x17004C79 RID: 19577
		// (get) Token: 0x0600FD61 RID: 64865 RVA: 0x0038F18E File Offset: 0x0038D38E
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} <= {1}";
			}
		}

		// Token: 0x0600FD62 RID: 64866 RVA: 0x0038F243 File Offset: 0x0038D443
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return (obj == null && this.CurrentValue == null) || (obj == DBNull.Value && this.CurrentValue == Convert.DBNull) || (this.CurrentValue != null && this.CurrentValue != DBNull.Value);
				}
				return Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) < 0 || Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) == 0;
			};
		}

		// Token: 0x17004C7A RID: 19578
		// (get) Token: 0x0600FD63 RID: 64867 RVA: 0x0038F251 File Offset: 0x0038D451
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} <= {1}";
			}
		}
	}
}
