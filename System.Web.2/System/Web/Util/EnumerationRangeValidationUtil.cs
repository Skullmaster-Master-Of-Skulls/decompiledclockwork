using System;
using System.Web.UI.WebControls;

namespace System.Web.Util
{
	// Token: 0x020001F8 RID: 504
	internal static class EnumerationRangeValidationUtil
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x0004CE35 File Offset: 0x0004B035
		public static void ValidateRepeatLayout(RepeatLayout value)
		{
			if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
			{
				throw new ArgumentOutOfRangeException("value");
			}
		}
	}
}
