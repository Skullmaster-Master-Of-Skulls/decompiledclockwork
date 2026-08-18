using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001893 RID: 6291
	public class RadFilterNotIsNullFilterExpression : RadFilterNonGroupExpression
	{
		// Token: 0x0600F358 RID: 62296 RVA: 0x00375F64 File Offset: 0x00374164
		internal RadFilterNotIsNullFilterExpression()
		{
		}

		// Token: 0x0600F359 RID: 62297 RVA: 0x00375F6C File Offset: 0x0037416C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterNotIsNullFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x1700495D RID: 18781
		// (get) Token: 0x0600F35A RID: 62298 RVA: 0x00375F7B File Offset: 0x0037417B
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.NotIsNull;
			}
		}
	}
}
