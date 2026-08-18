using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200188A RID: 6282
	public class RadFilterGreaterThanOrEqualToFilterExpression<T> : RadFilterSingleValueExpression<T>
	{
		// Token: 0x0600F332 RID: 62258 RVA: 0x00375C3F File Offset: 0x00373E3F
		internal RadFilterGreaterThanOrEqualToFilterExpression()
		{
		}

		// Token: 0x0600F333 RID: 62259 RVA: 0x00375C47 File Offset: 0x00373E47
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterGreaterThanOrEqualToFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x1700494F RID: 18767
		// (get) Token: 0x0600F334 RID: 62260 RVA: 0x00375C56 File Offset: 0x00373E56
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.GreaterThanOrEqualTo;
			}
		}
	}
}
