using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001871 RID: 6257
	public interface IRadFilterExpressionFormatter
	{
		// Token: 0x0600F2C4 RID: 62148
		string FormatFieldName(string fieldName, Type dataType, bool isCaseSensitive);

		// Token: 0x0600F2C5 RID: 62149
		ArrayList FormatFieldValue(ArrayList values, Type forType, bool isCaseSensitive);
	}
}
