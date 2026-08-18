using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200188F RID: 6287
	public class RadFilterLessThanOrEqualToFilterExpression<T> : RadFilterSingleValueExpression<T>
	{
		// Token: 0x0600F34B RID: 62283 RVA: 0x00375EED File Offset: 0x003740ED
		internal RadFilterLessThanOrEqualToFilterExpression()
		{
		}

		// Token: 0x0600F34C RID: 62284 RVA: 0x00375EF5 File Offset: 0x003740F5
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterLessThanOrEqualToFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004958 RID: 18776
		// (get) Token: 0x0600F34D RID: 62285 RVA: 0x00375F04 File Offset: 0x00374104
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.LessThanOrEqualTo;
			}
		}
	}
}
