using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200197F RID: 6527
	[Browsable(false)]
	public class RadListViewEqualToFilterExpression<T> : RadListViewSingleValueExpression<T>
	{
		// Token: 0x0600FCA1 RID: 64673 RVA: 0x0038DE32 File Offset: 0x0038C032
		internal RadListViewEqualToFilterExpression()
		{
		}

		// Token: 0x0600FCA2 RID: 64674 RVA: 0x0038DE3A File Offset: 0x0038C03A
		public RadListViewEqualToFilterExpression(string fieldName) : base(fieldName)
		{
		}

		// Token: 0x17004C4E RID: 19534
		// (get) Token: 0x0600FCA3 RID: 64675 RVA: 0x0038DE43 File Offset: 0x0038C043
		[Browsable(false)]
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.EqualTo;
			}
		}

		// Token: 0x17004C4F RID: 19535
		// (get) Token: 0x0600FCA4 RID: 64676 RVA: 0x0038DE46 File Offset: 0x0038C046
		protected override string DynamicLinqStringFormat
		{
			get
			{
				return "it.{0} = {1}";
			}
		}

		// Token: 0x17004C50 RID: 19536
		// (get) Token: 0x0600FCA5 RID: 64677 RVA: 0x0038DE4D File Offset: 0x0038C04D
		protected override string EntitySQLStringFormat
		{
			get
			{
				return "it.{0} = {1}";
			}
		}

		// Token: 0x17004C51 RID: 19537
		// (get) Token: 0x0600FCA6 RID: 64678 RVA: 0x0038DE54 File Offset: 0x0038C054
		[Browsable(false)]
		public override Type FieldType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x0600FCA7 RID: 64679 RVA: 0x0038DECE File Offset: 0x0038C0CE
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				if (obj == null || obj == DBNull.Value)
				{
					return (obj == null && this.CurrentValue == null) || (obj == DBNull.Value && this.CurrentValue == Convert.DBNull);
				}
				return Comparer<T>.Default.Compare((T)((object)obj), this.CurrentValue) == 0;
			};
		}

		// Token: 0x17004C52 RID: 19538
		// (get) Token: 0x0600FCA8 RID: 64680 RVA: 0x0038DEDC File Offset: 0x0038C0DC
		protected override string OqlStringFormat
		{
			get
			{
				return "{0} = {1}";
			}
		}
	}
}
