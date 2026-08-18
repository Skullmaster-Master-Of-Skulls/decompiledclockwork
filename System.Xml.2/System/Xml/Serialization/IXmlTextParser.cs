using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000145 RID: 325
	public interface IXmlTextParser
	{
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600171E RID: 5918
		// (set) Token: 0x0600171F RID: 5919
		bool Normalized { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001720 RID: 5920
		// (set) Token: 0x06001721 RID: 5921
		WhitespaceHandling WhitespaceHandling { get; set; }
	}
}
