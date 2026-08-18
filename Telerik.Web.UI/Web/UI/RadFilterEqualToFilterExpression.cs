using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001887 RID: 6279
	public class RadFilterEqualToFilterExpression<T> : RadFilterSingleValueExpression<T>
	{
		// Token: 0x0600F315 RID: 62229 RVA: 0x0037589F File Offset: 0x00373A9F
		internal RadFilterEqualToFilterExpression()
		{
		}

		// Token: 0x0600F316 RID: 62230 RVA: 0x003758A7 File Offset: 0x00373AA7
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterEqualToFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004949 RID: 18761
		// (get) Token: 0x0600F317 RID: 62231 RVA: 0x003758B6 File Offset: 0x00373AB6
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.EqualTo;
			}
		}
	}
}
