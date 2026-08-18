using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001990 RID: 6544
	[Browsable(false)]
	public class RadListViewLessThanFilterExpression<T> : RadListViewSingleValueExpression<T>
	{
		// Token: 0x0600FD55 RID: 64853 RVA: 0x0038F0D8 File Offset: 0x0038D2D8
		internal RadListViewLessThanFilterExpression()
		{
		}

		// Token: 0x0600FD56 RID: 64854 RVA: 0x0038F0E0 File Offset: 0x0038D2E0
		public RadListViewLessThanFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C73 RID: 19571
		// (get) Token: 0x0600FD57 RID: 64855 RVA: 0x0038F0E9 File Offset: 0x0038D2E9
		[Browsable(false)]
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.LessThan;
			}
		}

		// Token: 0x17004C74 RID: 19572
		// (get) Token: 0x0600FD58 RID: 64856 RVA: 0x0038F0EC File Offset: 0x0038D2EC
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0} < {1}";
			}
		}

		// Token: 0x17004C75 RID: 19573
		// (get) Token: 0x0600FD59 RID: 64857 RVA: 0x0038F0F3 File Offset: 0x0038D2F3
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} < {1}";
			}
		}

		// Token: 0x0600FD5A RID: 64858 RVA: 0x0038F15E File Offset: 0x0038D35E
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return this.CurrentValue != null && this.CurrentValue != Convert.DBNull;
				}
				return Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) < 0;
			};
		}

		// Token: 0x17004C76 RID: 19574
		// (get) Token: 0x0600FD5B RID: 64859 RVA: 0x0038F16C File Offset: 0x0038D36C
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} < {1}";
			}
		}
	}
}
