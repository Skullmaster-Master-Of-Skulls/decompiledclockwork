using System;
using System.Text.RegularExpressions;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x0200009E RID: 158
	public interface IBodyRegion : IDocumentObject
	{
		// Token: 0x06000199 RID: 409
		int Replace(Regex pattern, string replace);

		// Token: 0x0600019A RID: 410
		int Replace(string given, string replace, bool caseSensitive, bool wholeWord);

		// Token: 0x0600019B RID: 411
		int Replace(Regex pattern, TextSelection textSelection);
	}
}
