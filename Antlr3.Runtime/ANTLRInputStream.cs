using System;
using System.IO;
using System.Text;

namespace Antlr.Runtime
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class ANTLRInputStream : ANTLRReaderStream
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002508 File Offset: 0x00000708
		public ANTLRInputStream(Stream input) : this(input, null)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002512 File Offset: 0x00000712
		public ANTLRInputStream(Stream input, int size) : this(input, size, null)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000251D File Offset: 0x0000071D
		public ANTLRInputStream(Stream input, Encoding encoding) : this(input, 1024, encoding)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000252C File Offset: 0x0000072C
		public ANTLRInputStream(Stream input, int size, Encoding encoding) : this(input, size, 1024, encoding)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000253C File Offset: 0x0000073C
		public ANTLRInputStream(Stream input, int size, int readBufferSize, Encoding encoding) : base(ANTLRInputStream.GetStreamReader(input, encoding), size, readBufferSize)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000254E File Offset: 0x0000074E
		private static StreamReader GetStreamReader(Stream input, Encoding encoding)
		{
			if (encoding != null)
			{
				return new StreamReader(input, encoding);
			}
			return new StreamReader(input);
		}
	}
}
