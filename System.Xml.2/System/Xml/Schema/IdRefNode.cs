using System;

namespace System.Xml.Schema
{
	// Token: 0x020002BB RID: 699
	internal class IdRefNode
	{
		// Token: 0x0600283B RID: 10299 RVA: 0x000D24D9 File Offset: 0x000D06D9
		internal IdRefNode(IdRefNode next, string id, int lineNo, int linePos)
		{
			this.Id = id;
			this.LineNo = lineNo;
			this.LinePos = linePos;
			this.Next = next;
		}

		// Token: 0x04001180 RID: 4480
		internal string Id;

		// Token: 0x04001181 RID: 4481
		internal int LineNo;

		// Token: 0x04001182 RID: 4482
		internal int LinePos;

		// Token: 0x04001183 RID: 4483
		internal IdRefNode Next;
	}
}
