using System;
using System.Collections.Generic;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x020002DC RID: 732
	public interface IImageProvider
	{
		// Token: 0x06001B45 RID: 6981
		Image GetImage(string src, Dictionary<string, string> h, ChainedProperties cprops, IDocListener doc);
	}
}
