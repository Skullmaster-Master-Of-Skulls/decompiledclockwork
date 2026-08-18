using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200188E RID: 6286
	public class RadFilterLessThanFilterExpression<T> : RadFilterSingleValueExpression<T>
	{
		// Token: 0x0600F348 RID: 62280 RVA: 0x00375ED3 File Offset: 0x003740D3
		internal RadFilterLessThanFilterExpression()
		{
		}

		// Token: 0x0600F349 RID: 62281 RVA: 0x00375EDB File Offset: 0x003740DB
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterLessThanFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004957 RID: 18775
		// (get) Token: 0x0600F34A RID: 62282 RVA: 0x00375EEA File Offset: 0x003740EA
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.LessThan;
			}
		}
	}
}
