using System;

namespace System.Xml.Schema
{
	// Token: 0x02000257 RID: 599
	internal abstract class SchemaBuilder
	{
		// Token: 0x06002387 RID: 9095
		internal abstract bool ProcessElement(string prefix, string name, string ns);

		// Token: 0x06002388 RID: 9096
		internal abstract void ProcessAttribute(string prefix, string name, string ns, string value);

		// Token: 0x06002389 RID: 9097
		internal abstract bool IsContentParsed();

		// Token: 0x0600238A RID: 9098
		internal abstract void ProcessMarkup(XmlNode[] markup);

		// Token: 0x0600238B RID: 9099
		internal abstract void ProcessCData(string value);

		// Token: 0x0600238C RID: 9100
		internal abstract void StartChildren();

		// Token: 0x0600238D RID: 9101
		internal abstract void EndChildren();
	}
}
