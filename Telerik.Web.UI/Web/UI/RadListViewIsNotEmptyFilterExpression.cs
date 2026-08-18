using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200198D RID: 6541
	[Browsable(false)]
	public class RadListViewIsNotEmptyFilterExpression : RadListViewFilterExpression
	{
		// Token: 0x0600FD3A RID: 64826 RVA: 0x0038EF03 File Offset: 0x0038D103
		internal RadListViewIsNotEmptyFilterExpression()
		{
		}

		// Token: 0x0600FD3B RID: 64827 RVA: 0x0038EF0B File Offset: 0x0038D10B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadListViewIsNotEmptyFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x0600FD3C RID: 64828 RVA: 0x0038EF59 File Offset: 0x0038D159
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				string text = obj as string;
				if (text != null)
				{
					return !string.IsNullOrEmpty(text);
				}
				return obj != null && obj != DBNull.Value;
			};
		}

		// Token: 0x17004C6D RID: 19565
		// (get) Token: 0x0600FD3D RID: 64829 RVA: 0x0038EF67 File Offset: 0x0038D167
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.NotIsEmpty;
			}
		}

		// Token: 0x0600FD3E RID: 64830 RVA: 0x0038EF6A File Offset: 0x0038D16A
		public override string ToDynamicLinq()
		{
			return string.Format("it.{0} != \"\"", this.FieldName);
		}

		// Token: 0x17004C6E RID: 19566
		// (get) Token: 0x0600FD3F RID: 64831 RVA: 0x0038EF7C File Offset: 0x0038D17C
		public override Type FieldType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x0600FD40 RID: 64832 RVA: 0x0038EF88 File Offset: 0x0038D188
		public override string ToEntitySQL()
		{
			return string.Format("it.{0} != \"\"", this.FieldName);
		}

		// Token: 0x0600FD41 RID: 64833 RVA: 0x0038EF9A File Offset: 0x0038D19A
		public override string ToOql()
		{
			return string.Format("{0} <> \"\"", this.FieldName);
		}
	}
}
