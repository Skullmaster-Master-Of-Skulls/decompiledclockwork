using System;

namespace System.Xml.Schema
{
	// Token: 0x02000285 RID: 645
	internal class IdRefNode
	{
		// Token: 0x06001D7E RID: 7550 RVA: 0x00086132 File Offset: 0x00085132
		internal IdRefNode(IdRefNode next, string id, int lineNo, int linePos)
		{
			this.Id = id;
			this.LineNo = lineNo;
			this.LinePos = linePos;
			this.Next = next;
		}

		// Token: 0x04001200 RID: 4608
		internal string Id;

		// Token: 0x04001201 RID: 4609
		internal int LineNo;

		// Token: 0x04001202 RID: 4610
		internal int LinePos;

		// Token: 0x04001203 RID: 4611
		internal IdRefNode Next;
	}
}
