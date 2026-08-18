using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001882 RID: 6274
	public class RadFilterBetweenFilterExpression<T> : RadFilterDualValueExpression<T>
	{
		// Token: 0x0600F303 RID: 62211 RVA: 0x0037576C File Offset: 0x0037396C
		internal RadFilterBetweenFilterExpression()
		{
		}

		// Token: 0x0600F304 RID: 62212 RVA: 0x00375774 File Offset: 0x00373974
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterBetweenFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004942 RID: 18754
		// (get) Token: 0x0600F305 RID: 62213 RVA: 0x00375783 File Offset: 0x00373983
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.Between;
			}
		}
	}
}
