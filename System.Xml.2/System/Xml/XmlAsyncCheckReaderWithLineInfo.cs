using System;

namespace System.Xml
{
	// Token: 0x020000C6 RID: 198
	internal class XmlAsyncCheckReaderWithLineInfo : XmlAsyncCheckReader, IXmlLineInfo
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00018892 File Offset: 0x00016A92
		public XmlAsyncCheckReaderWithLineInfo(XmlReader reader) : base(reader)
		{
			this.readerAsIXmlLineInfo = (IXmlLineInfo)reader;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000188A7 File Offset: 0x00016AA7
		public virtual bool HasLineInfo()
		{
			return this.readerAsIXmlLineInfo.HasLineInfo();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x000188B4 File Offset: 0x00016AB4
		public virtual int LineNumber
		{
			get
			{
				return this.readerAsIXmlLineInfo.LineNumber;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x000188C1 File Offset: 0x00016AC1
		public virtual int LinePosition
		{
			get
			{
				return this.readerAsIXmlLineInfo.LinePosition;
			}
		}

		// Token: 0x040002DD RID: 733
		private readonly IXmlLineInfo readerAsIXmlLineInfo;
	}
}
