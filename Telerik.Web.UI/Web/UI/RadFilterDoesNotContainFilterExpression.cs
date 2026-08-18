using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001885 RID: 6277
	public class RadFilterDoesNotContainFilterExpression : RadFilterSingleValueExpression<string>
	{
		// Token: 0x0600F30F RID: 62223 RVA: 0x0037586B File Offset: 0x00373A6B
		internal RadFilterDoesNotContainFilterExpression()
		{
		}

		// Token: 0x0600F310 RID: 62224 RVA: 0x00375873 File Offset: 0x00373A73
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterDoesNotContainFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004947 RID: 18759
		// (get) Token: 0x0600F311 RID: 62225 RVA: 0x00375882 File Offset: 0x00373A82
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.DoesNotContain;
			}
		}
	}
}
