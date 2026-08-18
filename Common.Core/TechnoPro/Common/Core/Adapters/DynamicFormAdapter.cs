using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x0200016E RID: 366
	public static class DynamicFormAdapter
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x00077024 File Offset: 0x00075224
		public static DynamicControlAttribute GetDynamicControlAttribute(this eControlCode eControlCodeDTO)
		{
			return eControlCodeDTO.GetDynamicControlAttribute();
		}
	}
}
