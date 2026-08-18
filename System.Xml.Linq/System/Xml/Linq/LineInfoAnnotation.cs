using System;

namespace System.Xml.Linq
{
	// Token: 0x02000012 RID: 18
	internal class LineInfoAnnotation
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000470E File Offset: 0x0000290E
		public LineInfoAnnotation(int lineNumber, int linePosition)
		{
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x04000075 RID: 117
		internal int lineNumber;

		// Token: 0x04000076 RID: 118
		internal int linePosition;
	}
}
