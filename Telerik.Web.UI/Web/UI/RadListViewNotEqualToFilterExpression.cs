using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001992 RID: 6546
	[Browsable(false)]
	public class RadListViewNotEqualToFilterExpression<T> : RadListViewSingleValueExpression<T>
	{
		// Token: 0x0600FD65 RID: 64869 RVA: 0x0038F258 File Offset: 0x0038D458
		internal RadListViewNotEqualToFilterExpression()
		{
		}

		// Token: 0x0600FD66 RID: 64870 RVA: 0x0038F260 File Offset: 0x0038D460
		public RadListViewNotEqualToFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C7B RID: 19579
		// (get) Token: 0x0600FD67 RID: 64871 RVA: 0x0038F269 File Offset: 0x0038D469
		[Browsable(false)]
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.NotEqualTo;
			}
		}

		// Token: 0x17004C7C RID: 19580
		// (get) Token: 0x0600FD68 RID: 64872 RVA: 0x0038F26C File Offset: 0x0038D46C
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0} <> {1}";
			}
		}

		// Token: 0x17004C7D RID: 19581
		// (get) Token: 0x0600FD69 RID: 64873 RVA: 0x0038F273 File Offset: 0x0038D473
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} <> {1}";
			}
		}

		// Token: 0x17004C7E RID: 19582
		// (get) Token: 0x0600FD6A RID: 64874 RVA: 0x0038F27A File Offset: 0x0038D47A
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} <> {1}";
			}
		}

		// Token: 0x0600FD6B RID: 64875 RVA: 0x0038F2F8 File Offset: 0x0038D4F8
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return (obj != null || this.CurrentValue != null) && (obj != DBNull.Value || this.CurrentValue != Convert.DBNull);
				}
				return Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) != 0;
			};
		}
	}
}
