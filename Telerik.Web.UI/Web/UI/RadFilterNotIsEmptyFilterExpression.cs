using System;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI
{
	// Token: 0x02001892 RID: 6290
	public class RadFilterNotIsEmptyFilterExpression : RadFilterNonGroupExpression
	{
		// Token: 0x0600F354 RID: 62292 RVA: 0x00375F3D File Offset: 0x0037413D
		internal RadFilterNotIsEmptyFilterExpression()
		{
		}

		// Token: 0x0600F355 RID: 62293 RVA: 0x00375F45 File Offset: 0x00374145
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RadFilterNotIsEmptyFilterExpression(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x1700495B RID: 18779
		// (get) Token: 0x0600F356 RID: 62294 RVA: 0x00375F54 File Offset: 0x00374154
		public override Type FieldType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x1700495C RID: 18780
		// (get) Token: 0x0600F357 RID: 62295 RVA: 0x00375F60 File Offset: 0x00374160
		public override RadFilterFunction FilterFunction
		{
			get
			{
				return RadFilterFunction.NotIsEmpty;
			}
		}
	}
}
