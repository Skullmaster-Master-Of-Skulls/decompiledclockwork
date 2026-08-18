using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002BE RID: 702
	public interface IXmlTextParser
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002174 RID: 8564
		// (set) Token: 0x06002175 RID: 8565
		bool Normalized { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002176 RID: 8566
		// (set) Token: 0x06002177 RID: 8567
		WhitespaceHandling WhitespaceHandling { get; set; }
	}
}
