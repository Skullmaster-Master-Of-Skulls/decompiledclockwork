using System;

namespace iTextSharp.text
{
	// Token: 0x020000BE RID: 190
	public interface ILargeElement : IElement
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005EC RID: 1516
		// (set) Token: 0x060005ED RID: 1517
		bool ElementComplete { get; set; }

		// Token: 0x060005EE RID: 1518
		void FlushContent();
	}
}
