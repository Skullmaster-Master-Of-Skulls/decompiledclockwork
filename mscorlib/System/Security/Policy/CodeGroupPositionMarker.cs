using System;

namespace System.Security.Policy
{
	// Token: 0x0200049F RID: 1183
	internal class CodeGroupPositionMarker
	{
		// Token: 0x06002EFA RID: 12026 RVA: 0x0009EF2C File Offset: 0x0009DF2C
		internal CodeGroupPositionMarker(int elementIndex, int groupIndex, SecurityElement element)
		{
			this.elementIndex = elementIndex;
			this.groupIndex = groupIndex;
			this.element = element;
		}

		// Token: 0x040017F2 RID: 6130
		internal int elementIndex;

		// Token: 0x040017F3 RID: 6131
		internal int groupIndex;

		// Token: 0x040017F4 RID: 6132
		internal SecurityElement element;
	}
}
