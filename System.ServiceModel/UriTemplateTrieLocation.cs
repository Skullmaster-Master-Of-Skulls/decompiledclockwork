using System;

namespace System
{
	// Token: 0x02000018 RID: 24
	internal class UriTemplateTrieLocation
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x000058B0 File Offset: 0x00003AB0
		public UriTemplateTrieLocation(UriTemplateTrieNode n, UriTemplateTrieIntraNodeLocation i)
		{
			this.node = n;
			this.locationWithin = i;
		}

		// Token: 0x04000090 RID: 144
		public UriTemplateTrieIntraNodeLocation locationWithin;

		// Token: 0x04000091 RID: 145
		public UriTemplateTrieNode node;
	}
}
