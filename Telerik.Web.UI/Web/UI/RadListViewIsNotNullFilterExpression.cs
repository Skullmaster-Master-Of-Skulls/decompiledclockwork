using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200198E RID: 6542
	[Browsable(false)]
	public class RadListViewIsNotNullFilterExpression : RadListViewFilterExpression
	{
		// Token: 0x0600FD43 RID: 64835 RVA: 0x0038EFAC File Offset: 0x0038D1AC
		internal RadListViewIsNotNullFilterExpression()
		{
		}

		// Token: 0x0600FD44 RID: 64836 RVA: 0x0038EFB4 File Offset: 0x0038D1B4
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadListViewIsNotNullFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x0600FD45 RID: 64837 RVA: 0x0038EFEF File Offset: 0x0038D1EF
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				return obj != null && obj != DBNull.Value;
			};
		}

		// Token: 0x17004C6F RID: 19567
		// (get) Token: 0x0600FD46 RID: 64838 RVA: 0x0038EFFD File Offset: 0x0038D1FD
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.NotIsNull;
			}
		}

		// Token: 0x0600FD47 RID: 64839 RVA: 0x0038F001 File Offset: 0x0038D201
		public override string ToDynamicLinq()
		{
			return string.Format("it.{0} != null", this.FieldName);
		}

		// Token: 0x0600FD48 RID: 64840 RVA: 0x0038F013 File Offset: 0x0038D213
		public override string ToEntitySQL()
		{
			return string.Format("NOT(it.{0} IS null)", this.FieldName);
		}

		// Token: 0x17004C70 RID: 19568
		// (get) Token: 0x0600FD49 RID: 64841 RVA: 0x0038F025 File Offset: 0x0038D225
		public override Type FieldType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x0600FD4A RID: 64842 RVA: 0x0038F031 File Offset: 0x0038D231
		public override string ToOql()
		{
			return string.Format("{0} != nil", this.FieldName);
		}
	}
}
