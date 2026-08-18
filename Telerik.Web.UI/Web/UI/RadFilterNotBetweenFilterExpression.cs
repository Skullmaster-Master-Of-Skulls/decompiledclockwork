using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001890 RID: 6288
	public class RadFilterNotBetweenFilterExpression<T> : RadFilterDualValueExpression<T>
	{
		// Token: 0x0600F34E RID: 62286 RVA: 0x00375F08 File Offset: 0x00374108
		internal RadFilterNotBetweenFilterExpression()
		{
		}

		// Token: 0x0600F34F RID: 62287 RVA: 0x00375F10 File Offset: 0x00374110
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterNotBetweenFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004959 RID: 18777
		// (get) Token: 0x0600F350 RID: 62288 RVA: 0x00375F1F File Offset: 0x0037411F
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.NotBetween;
			}
		}
	}
}
