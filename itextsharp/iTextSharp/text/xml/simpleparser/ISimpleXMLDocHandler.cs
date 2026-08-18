using System;
using System.Collections.Generic;

namespace iTextSharp.text.xml.simpleparser
{
	// Token: 0x0200021C RID: 540
	public interface ISimpleXMLDocHandler
	{
		// Token: 0x06001505 RID: 5381
		void StartElement(string tag, Dictionary<string, string> h);

		// Token: 0x06001506 RID: 5382
		void EndElement(string tag);

		// Token: 0x06001507 RID: 5383
		void StartDocument();

		// Token: 0x06001508 RID: 5384
		void EndDocument();

		// Token: 0x06001509 RID: 5385
		void Text(string str);
	}
}
