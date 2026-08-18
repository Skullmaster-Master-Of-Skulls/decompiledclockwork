using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200197D RID: 6525
	[Browsable(false)]
	public class RadListViewContainsFilterExpression : RadListViewSingleStringExpression
	{
		// Token: 0x0600FC91 RID: 64657 RVA: 0x0038DD09 File Offset: 0x0038BF09
		internal RadListViewContainsFilterExpression()
		{
		}

		// Token: 0x0600FC92 RID: 64658 RVA: 0x0038DD11 File Offset: 0x0038BF11
		public RadListViewContainsFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C46 RID: 19526
		// (get) Token: 0x0600FC93 RID: 64659 RVA: 0x0038DD1A File Offset: 0x0038BF1A
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0}.Contains({1})";
			}
		}

		// Token: 0x17004C47 RID: 19527
		// (get) Token: 0x0600FC94 RID: 64660 RVA: 0x0038DD21 File Offset: 0x0038BF21
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} LIKE \"%{1}%\"";
			}
		}

		// Token: 0x17004C48 RID: 19528
		// (get) Token: 0x0600FC95 RID: 64661 RVA: 0x0038DD28 File Offset: 0x0038BF28
		[Browsable(false)]
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.Contains;
			}
		}

		// Token: 0x0600FC96 RID: 64662 RVA: 0x0038DD88 File Offset: 0x0038BF88
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return (obj == null && this.CurrentValue == null) || (obj == DBNull.Value && this.CurrentValue == Convert.DBNull);
				}
				return ((string)obj).Contains(this.CurrentValue);
			};
		}

		// Token: 0x17004C49 RID: 19529
		// (get) Token: 0x0600FC97 RID: 64663 RVA: 0x0038DD96 File Offset: 0x0038BF96
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} LIKE \"*{1}*\"";
			}
		}
	}
}
