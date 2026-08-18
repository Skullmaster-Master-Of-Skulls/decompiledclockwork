using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001891 RID: 6289
	public class RadFilterNotEqualToFilterExpression<T> : RadFilterSingleValueExpression<T>
	{
		// Token: 0x0600F351 RID: 62289 RVA: 0x00375F23 File Offset: 0x00374123
		internal RadFilterNotEqualToFilterExpression()
		{
		}

		// Token: 0x0600F352 RID: 62290 RVA: 0x00375F2B File Offset: 0x0037412B
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterNotEqualToFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x1700495A RID: 18778
		// (get) Token: 0x0600F353 RID: 62291 RVA: 0x00375F3A File Offset: 0x0037413A
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.NotEqualTo;
			}
		}
	}
}
