using System;

namespace System.Web.UI
{
	// Token: 0x020002B9 RID: 697
	public interface IThemeResolutionService
	{
		// Token: 0x06001FD1 RID: 8145
		ThemeProvider[] GetAllThemeProviders();

		// Token: 0x06001FD2 RID: 8146
		ThemeProvider GetThemeProvider();

		// Token: 0x06001FD3 RID: 8147
		ThemeProvider GetStylesheetThemeProvider();
	}
}
