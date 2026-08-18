using System;
using System.Globalization;

namespace System.Web.Globalization
{
	// Token: 0x02000695 RID: 1685
	public interface IStringLocalizerProvider
	{
		// Token: 0x06005130 RID: 20784
		string GetLocalizedString(CultureInfo culture, string name, params object[] arguments);
	}
}
