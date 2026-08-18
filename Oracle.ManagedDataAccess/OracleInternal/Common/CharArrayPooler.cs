using System;

namespace OracleInternal.Common
{
	// Token: 0x020000C0 RID: 192
	internal class CharArrayPooler : SyncQueueList<char[]>
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x000456F0 File Offset: 0x000438F0
		internal CharArrayPooler(int maxElements) : base(maxElements)
		{
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00045704 File Offset: 0x00043904
		internal CharArrayPooler(int maxElements, int charBufSize) : base(maxElements)
		{
			this.m_charBufSize = charBufSize;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00045720 File Offset: 0x00043920
		internal override char[] Dequeue()
		{
			char[] result;
			lock (this.m_sync)
			{
				if (this.m_list.Count == 0)
				{
					result = new char[this.m_charBufSize];
				}
				else
				{
					char[] array = this.m_list[0];
					this.m_list.Remove(array);
					result = array;
				}
			}
			return result;
		}

		// Token: 0x04000A1F RID: 2591
		private int m_charBufSize = 4000;
	}
}
