using System;

namespace System.Xml
{
	// Token: 0x02000075 RID: 117
	internal struct LineInfo
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000F0ED File Offset: 0x0000D2ED
		public LineInfo(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000F0FD File Offset: 0x0000D2FD
		public void Set(int lineNo, int linePos)
		{
			this.lineNo = lineNo;
			this.linePos = linePos;
		}

		// Token: 0x040001C4 RID: 452
		internal int lineNo;

		// Token: 0x040001C5 RID: 453
		internal int linePos;
	}
}
