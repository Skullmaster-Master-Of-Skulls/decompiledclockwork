using System;
using System.Collections.Generic;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x0200041F RID: 1055
	public interface IImg
	{
		// Token: 0x060023DC RID: 9180
		bool Process(Image img, Dictionary<string, string> h, ChainedProperties cprops, IDocListener doc);
	}
}
