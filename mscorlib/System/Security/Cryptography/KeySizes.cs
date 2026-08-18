using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000862 RID: 2146
	[ComVisible(true)]
	public sealed class KeySizes
	{
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x0010FEB7 File Offset: 0x0010EEB7
		public int MinSize
		{
			get
			{
				return this.m_minSize;
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06004E67 RID: 20071 RVA: 0x0010FEBF File Offset: 0x0010EEBF
		public int MaxSize
		{
			get
			{
				return this.m_maxSize;
			}
		}

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x0010FEC7 File Offset: 0x0010EEC7
		public int SkipSize
		{
			get
			{
				return this.m_skipSize;
			}
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x0010FECF File Offset: 0x0010EECF
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this.m_minSize = minSize;
			this.m_maxSize = maxSize;
			this.m_skipSize = skipSize;
		}

		// Token: 0x04002890 RID: 10384
		private int m_minSize;

		// Token: 0x04002891 RID: 10385
		private int m_maxSize;

		// Token: 0x04002892 RID: 10386
		private int m_skipSize;
	}
}
