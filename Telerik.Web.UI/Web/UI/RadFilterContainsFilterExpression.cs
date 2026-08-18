using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001884 RID: 6276
	public class RadFilterContainsFilterExpression : RadFilterSingleValueExpression<string>
	{
		// Token: 0x0600F30C RID: 62220 RVA: 0x00375851 File Offset: 0x00373A51
		internal RadFilterContainsFilterExpression()
		{
		}

		// Token: 0x0600F30D RID: 62221 RVA: 0x00375859 File Offset: 0x00373A59
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterContainsFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004946 RID: 18758
		// (get) Token: 0x0600F30E RID: 62222 RVA: 0x00375868 File Offset: 0x00373A68
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.Contains;
			}
		}
	}
}
