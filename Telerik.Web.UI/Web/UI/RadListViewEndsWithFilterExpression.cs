using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200197E RID: 6526
	[Browsable(false)]
	public class RadListViewEndsWithFilterExpression : RadListViewSingleStringExpression
	{
		// Token: 0x0600FC99 RID: 64665 RVA: 0x0038DD9D File Offset: 0x0038BF9D
		internal RadListViewEndsWithFilterExpression()
		{
		}

		// Token: 0x0600FC9A RID: 64666 RVA: 0x0038DDA5 File Offset: 0x0038BFA5
		public RadListViewEndsWithFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C4A RID: 19530
		// (get) Token: 0x0600FC9B RID: 64667 RVA: 0x0038DDAE File Offset: 0x0038BFAE
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0}.EndsWith({1})";
			}
		}

		// Token: 0x17004C4B RID: 19531
		// (get) Token: 0x0600FC9C RID: 64668 RVA: 0x0038DDB5 File Offset: 0x0038BFB5
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} LIKE \"%{1}\"";
			}
		}

		// Token: 0x17004C4C RID: 19532
		// (get) Token: 0x0600FC9D RID: 64669 RVA: 0x0038DDBC File Offset: 0x0038BFBC
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} LIKE \"*{1}\"";
			}
		}

		// Token: 0x17004C4D RID: 19533
		// (get) Token: 0x0600FC9E RID: 64670 RVA: 0x0038DDC3 File Offset: 0x0038BFC3
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.EndsWith;
			}
		}

		// Token: 0x0600FC9F RID: 64671 RVA: 0x0038DE24 File Offset: 0x0038C024
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return (obj == null && this.CurrentValue == null) || (obj == DBNull.Value && this.CurrentValue == Convert.DBNull);
				}
				return ((string)obj).EndsWith(this.CurrentValue);
			};
		}
	}
}
