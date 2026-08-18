using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200198F RID: 6543
	[Browsable(false)]
	public class RadListViewIsNullFilterExpression : RadListViewFilterExpression
	{
		// Token: 0x0600FD4C RID: 64844 RVA: 0x0038F043 File Offset: 0x0038D243
		internal RadListViewIsNullFilterExpression()
		{
		}

		// Token: 0x0600FD4D RID: 64845 RVA: 0x0038F04B File Offset: 0x0038D24B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadListViewIsNullFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x0600FD4E RID: 64846 RVA: 0x0038F084 File Offset: 0x0038D284
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				return obj == null || obj == DBNull.Value;
			};
		}

		// Token: 0x17004C71 RID: 19569
		// (get) Token: 0x0600FD4F RID: 64847 RVA: 0x0038F092 File Offset: 0x0038D292
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.IsNull;
			}
		}

		// Token: 0x0600FD50 RID: 64848 RVA: 0x0038F096 File Offset: 0x0038D296
		public override string ToDynamicLinq()
		{
			return string.Format("it.{0} == null", this.FieldName);
		}

		// Token: 0x17004C72 RID: 19570
		// (get) Token: 0x0600FD51 RID: 64849 RVA: 0x0038F0A8 File Offset: 0x0038D2A8
		public override Type FieldType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x0600FD52 RID: 64850 RVA: 0x0038F0B4 File Offset: 0x0038D2B4
		public override string ToEntitySQL()
		{
			return string.Format("it.{0} IS null", this.FieldName);
		}

		// Token: 0x0600FD53 RID: 64851 RVA: 0x0038F0C6 File Offset: 0x0038D2C6
		public override string ToOql()
		{
			return string.Format("{0} == nil", this.FieldName);
		}
	}
}
