using System;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200019C RID: 412
	internal static class TextSyndicationContentKindHelper
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x000306F9 File Offset: 0x0002E8F9
		public static bool IsDefined(TextSyndicationContentKind kind)
		{
			return kind == TextSyndicationContentKind.Plaintext || kind == TextSyndicationContentKind.Html || kind == TextSyndicationContentKind.XHtml;
		}
	}
}
