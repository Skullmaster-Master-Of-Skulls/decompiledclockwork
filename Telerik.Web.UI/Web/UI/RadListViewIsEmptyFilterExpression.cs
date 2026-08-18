using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200198C RID: 6540
	[Browsable(false)]
	public class RadListViewIsEmptyFilterExpression : RadListViewFilterExpression
	{
		// Token: 0x0600FD31 RID: 64817 RVA: 0x0038EE60 File Offset: 0x0038D060
		internal RadListViewIsEmptyFilterExpression()
		{
		}

		// Token: 0x0600FD32 RID: 64818 RVA: 0x0038EE68 File Offset: 0x0038D068
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadListViewIsEmptyFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x0600FD33 RID: 64819 RVA: 0x0038EEB0 File Offset: 0x0038D0B0
		public override Predicate<object> ToPredicate()
		{
			return delegate(object item)
			{
				object obj = this.ExtractFieldValueFromItem(item, this.FieldName);
				string text = obj as string;
				return obj != null && obj != DBNull.Value && text != null && string.IsNullOrEmpty(text);
			};
		}

		// Token: 0x17004C6B RID: 19563
		// (get) Token: 0x0600FD34 RID: 64820 RVA: 0x0038EEBE File Offset: 0x0038D0BE
		public override RadListViewFilterFunction FilterFunction
		{
			get
			{
				return RadListViewFilterFunction.IsEmpty;
			}
		}

		// Token: 0x0600FD35 RID: 64821 RVA: 0x0038EEC1 File Offset: 0x0038D0C1
		public override string ToDynamicLinq()
		{
			return string.Format("it.{0} == \"\"", this.FieldName);
		}

		// Token: 0x17004C6C RID: 19564
		// (get) Token: 0x0600FD36 RID: 64822 RVA: 0x0038EED3 File Offset: 0x0038D0D3
		public override Type FieldType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x0600FD37 RID: 64823 RVA: 0x0038EEDF File Offset: 0x0038D0DF
		public override string ToEntitySQL()
		{
			return string.Format("it.{0} == \"\"", this.FieldName);
		}

		// Token: 0x0600FD38 RID: 64824 RVA: 0x0038EEF1 File Offset: 0x0038D0F1
		public override string ToOql()
		{
			return string.Format("{0} == \"\"", this.FieldName);
		}
	}
}
