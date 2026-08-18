using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001889 RID: 6281
	public class RadFilterGreaterThanFilterExpression<T> : RadFilterSingleValueExpression<T>
	{
		// Token: 0x0600F32F RID: 62255 RVA: 0x00375C25 File Offset: 0x00373E25
		internal RadFilterGreaterThanFilterExpression()
		{
		}

		// Token: 0x0600F330 RID: 62256 RVA: 0x00375C2D File Offset: 0x00373E2D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterGreaterThanFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x1700494E RID: 18766
		// (get) Token: 0x0600F331 RID: 62257 RVA: 0x00375C3C File Offset: 0x00373E3C
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.GreaterThan;
			}
		}
	}
}
