using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020D RID: 525
	internal abstract class SchemaBuilder
	{
		// Token: 0x060018E4 RID: 6372
		internal abstract bool ProcessElement(string prefix, string name, string ns);

		// Token: 0x060018E5 RID: 6373
		internal abstract void ProcessAttribute(string prefix, string name, string ns, string value);

		// Token: 0x060018E6 RID: 6374
		internal abstract bool IsContentParsed();

		// Token: 0x060018E7 RID: 6375
		internal abstract void ProcessMarkup(XmlNode[] markup);

		// Token: 0x060018E8 RID: 6376
		internal abstract void ProcessCData(string value);

		// Token: 0x060018E9 RID: 6377
		internal abstract void StartChildren();

		// Token: 0x060018EA RID: 6378
		internal abstract void EndChildren();
	}
}
