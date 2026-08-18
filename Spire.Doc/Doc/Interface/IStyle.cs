using System;
using Spire.Doc.Documents;

namespace Spire.Doc.Interface
{
	// Token: 0x020001F0 RID: 496
	public interface IStyle
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060015B9 RID: 5561
		// (set) Token: 0x060015BA RID: 5562
		string Name { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060015BB RID: 5563
		StyleType StyleType { get; }

		// Token: 0x060015BC RID: 5564
		IStyle Clone();
	}
}
