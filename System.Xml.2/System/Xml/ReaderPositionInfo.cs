using System;

namespace System.Xml
{
	// Token: 0x02000073 RID: 115
	internal class ReaderPositionInfo : PositionInfo
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000F0B7 File Offset: 0x0000D2B7
		public ReaderPositionInfo(IXmlLineInfo lineInfo)
		{
			this.lineInfo = lineInfo;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000F0C6 File Offset: 0x0000D2C6
		public override bool HasLineInfo()
		{
			return this.lineInfo.HasLineInfo();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000F0D3 File Offset: 0x0000D2D3
		public override int LineNumber
		{
			get
			{
				return this.lineInfo.LineNumber;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		public override int LinePosition
		{
			get
			{
				return this.lineInfo.LinePosition;
			}
		}

		// Token: 0x040001C3 RID: 451
		private IXmlLineInfo lineInfo;
	}
}
