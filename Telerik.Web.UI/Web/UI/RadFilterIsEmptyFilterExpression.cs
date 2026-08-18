using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x0200188C RID: 6284
	public class RadFilterIsEmptyFilterExpression : RadFilterNonGroupExpression
	{
		// Token: 0x0600F341 RID: 62273 RVA: 0x00375E91 File Offset: 0x00374091
		internal RadFilterIsEmptyFilterExpression()
		{
		}

		// Token: 0x0600F342 RID: 62274 RVA: 0x00375E99 File Offset: 0x00374099
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterIsEmptyFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17004954 RID: 18772
		// (get) Token: 0x0600F343 RID: 62275 RVA: 0x00375EA8 File Offset: 0x003740A8
		public override Type FieldType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x17004955 RID: 18773
		// (get) Token: 0x0600F344 RID: 62276 RVA: 0x00375EB4 File Offset: 0x003740B4
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.IsEmpty;
			}
		}
	}
}
