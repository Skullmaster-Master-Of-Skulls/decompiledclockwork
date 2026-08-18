using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200188D RID: 6285
	public class RadFilterIsNullFilterExpression : RadFilterNonGroupExpression
	{
		// Token: 0x0600F345 RID: 62277 RVA: 0x00375EB8 File Offset: 0x003740B8
		internal RadFilterIsNullFilterExpression()
		{
		}

		// Token: 0x0600F346 RID: 62278 RVA: 0x00375EC0 File Offset: 0x003740C0
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterIsNullFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004956 RID: 18774
		// (get) Token: 0x0600F347 RID: 62279 RVA: 0x00375ECF File Offset: 0x003740CF
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.IsNull;
			}
		}
	}
}
