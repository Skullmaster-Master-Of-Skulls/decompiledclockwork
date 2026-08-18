using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001886 RID: 6278
	public class RadFilterEndsWithFilterExpression : RadFilterSingleValueExpression<string>
	{
		// Token: 0x0600F312 RID: 62226 RVA: 0x00375885 File Offset: 0x00373A85
		internal RadFilterEndsWithFilterExpression()
		{
		}

		// Token: 0x0600F313 RID: 62227 RVA: 0x0037588D File Offset: 0x00373A8D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterEndsWithFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004948 RID: 18760
		// (get) Token: 0x0600F314 RID: 62228 RVA: 0x0037589C File Offset: 0x00373A9C
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.EndsWith;
			}
		}
	}
}
