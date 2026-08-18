using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001993 RID: 6547
	[Browsable(false)]
	public class RadListViewStartsWithFilterExpression : RadListViewSingleStringExpression
	{
		// Token: 0x0600FD6D RID: 64877 RVA: 0x0038F306 File Offset: 0x0038D506
		internal RadListViewStartsWithFilterExpression()
		{
		}

		// Token: 0x0600FD6E RID: 64878 RVA: 0x0038F30E File Offset: 0x0038D50E
		public RadListViewStartsWithFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C7F RID: 19583
		// (get) Token: 0x0600FD6F RID: 64879 RVA: 0x0038F317 File Offset: 0x0038D517
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0}.StartsWith({1})";
			}
		}

		// Token: 0x17004C80 RID: 19584
		// (get) Token: 0x0600FD70 RID: 64880 RVA: 0x0038F31E File Offset: 0x0038D51E
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} LIKE \"{1}%\"";
			}
		}

		// Token: 0x17004C81 RID: 19585
		// (get) Token: 0x0600FD71 RID: 64881 RVA: 0x0038F325 File Offset: 0x0038D525
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} LIKE \"{1}*\"";
			}
		}

		// Token: 0x17004C82 RID: 19586
		// (get) Token: 0x0600FD72 RID: 64882 RVA: 0x0038F32C File Offset: 0x0038D52C
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.StartsWith;
			}
		}

		// Token: 0x0600FD73 RID: 64883 RVA: 0x0038F38C File Offset: 0x0038D58C
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return (obj == null && this.CurrentValue == null) || (obj == DBNull.Value && this.CurrentValue == Convert.DBNull);
				}
				return ((string)obj).StartsWith(this.CurrentValue);
			};
		}
	}
}
