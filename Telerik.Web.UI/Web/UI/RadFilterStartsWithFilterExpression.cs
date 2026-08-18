using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001894 RID: 6292
	public class RadFilterStartsWithFilterExpression : RadFilterSingleValueExpression<string>
	{
		// Token: 0x0600F35B RID: 62299 RVA: 0x00375F7F File Offset: 0x0037417F
		internal RadFilterStartsWithFilterExpression()
		{
		}

		// Token: 0x0600F35C RID: 62300 RVA: 0x00375F87 File Offset: 0x00374187
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterStartsWithFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x1700495E RID: 18782
		// (get) Token: 0x0600F35D RID: 62301 RVA: 0x00375F96 File Offset: 0x00374196
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.StartsWith;
			}
		}
	}
}
