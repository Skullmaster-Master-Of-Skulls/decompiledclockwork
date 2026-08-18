using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F0 RID: 1520
	public interface IHelpService
	{
		// Token: 0x06003835 RID: 14389
		void AddContextAttribute(string name, string value, HelpKeywordType keywordType);

		// Token: 0x06003836 RID: 14390
		void ClearContextAttributes();

		// Token: 0x06003837 RID: 14391
		IHelpService CreateLocalContext(HelpContextType contextType);

		// Token: 0x06003838 RID: 14392
		void RemoveContextAttribute(string name, string value);

		// Token: 0x06003839 RID: 14393
		void RemoveLocalContext(IHelpService localContext);

		// Token: 0x0600383A RID: 14394
		void ShowHelpFromKeyword(string helpKeyword);

		// Token: 0x0600383B RID: 14395
		void ShowHelpFromUrl(string helpUrl);
	}
}
