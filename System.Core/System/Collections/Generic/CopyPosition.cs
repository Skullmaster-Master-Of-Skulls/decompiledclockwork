using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000098 RID: 152
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	internal struct CopyPosition
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		internal CopyPosition(int row, int column)
		{
			this.Row = row;
			this.Column = column;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		public static CopyPosition Start
		{
			get
			{
				return default(CopyPosition);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000BB0E File Offset: 0x00009D0E
		internal int Row { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000BB16 File Offset: 0x00009D16
		internal int Column { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000BB1E File Offset: 0x00009D1E
		private string DebuggerDisplay
		{
			get
			{
				return string.Format("[{0}, {1}]", this.Row, this.Column);
			}
		}
	}
}
