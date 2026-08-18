using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001985 RID: 6533
	[Browsable(false)]
	public class RadListViewGreaterThanFilterExpression<T> : RadListViewSingleValueExpression<T>
	{
		// Token: 0x0600FD07 RID: 64775 RVA: 0x0038E7BD File Offset: 0x0038C9BD
		internal RadListViewGreaterThanFilterExpression()
		{
		}

		// Token: 0x0600FD08 RID: 64776 RVA: 0x0038E7C5 File Offset: 0x0038C9C5
		public RadListViewGreaterThanFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C5F RID: 19551
		// (get) Token: 0x0600FD09 RID: 64777 RVA: 0x0038E7CE File Offset: 0x0038C9CE
		[Browsable(false)]
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.GreaterThan;
			}
		}

		// Token: 0x17004C60 RID: 19552
		// (get) Token: 0x0600FD0A RID: 64778 RVA: 0x0038E7D1 File Offset: 0x0038C9D1
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0} > {1}";
			}
		}

		// Token: 0x17004C61 RID: 19553
		// (get) Token: 0x0600FD0B RID: 64779 RVA: 0x0038E7D8 File Offset: 0x0038C9D8
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} > {1}";
			}
		}

		// Token: 0x17004C62 RID: 19554
		// (get) Token: 0x0600FD0C RID: 64780 RVA: 0x0038E7DF File Offset: 0x0038C9DF
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} > {1}";
			}
		}

		// Token: 0x0600FD0D RID: 64781 RVA: 0x0038E829 File Offset: 0x0038CA29
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				return obj != null && obj != DBNull.Value && Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) > 0;
			};
		}
	}
}
