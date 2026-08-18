using System;

namespace System.Xml
{
	// Token: 0x02000072 RID: 114
	internal class PositionInfo : IXmlLineInfo
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x0000F082 File Offset: 0x0000D282
		public virtual bool HasLineInfo()
		{
			return false;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000F085 File Offset: 0x0000D285
		public virtual int LineNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000F088 File Offset: 0x0000D288
		public virtual int LinePosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000F08C File Offset: 0x0000D28C
		public static PositionInfo GetPositionInfo(object o)
		{
			IXmlLineInfo xmlLineInfo = o as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				return new ReaderPositionInfo(xmlLineInfo);
			}
			return new PositionInfo();
		}
	}
}
