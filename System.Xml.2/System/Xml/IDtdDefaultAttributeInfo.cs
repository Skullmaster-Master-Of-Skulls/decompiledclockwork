using System;

namespace System.Xml
{
	// Token: 0x020000A8 RID: 168
	internal interface IDtdDefaultAttributeInfo : IDtdAttributeInfo
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005D5 RID: 1493
		string DefaultValueExpanded { get; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005D6 RID: 1494
		object DefaultValueTyped { get; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005D7 RID: 1495
		int ValueLineNumber { get; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005D8 RID: 1496
		int ValueLinePosition { get; }
	}
}
