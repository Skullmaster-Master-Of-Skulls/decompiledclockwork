using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001FE RID: 510
	internal class ListChunk<TInputOutput> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x0003944E File Offset: 0x0003764E
		internal ListChunk(int size)
		{
			this.m_chunk = new TInputOutput[size];
			this.m_chunkCount = 0;
			this.m_tailChunk = this;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00039470 File Offset: 0x00037670
		internal void Add(TInputOutput e)
		{
			ListChunk<TInputOutput> listChunk = this.m_tailChunk;
			if (listChunk.m_chunkCount == listChunk.m_chunk.Length)
			{
				this.m_tailChunk = new ListChunk<TInputOutput>(listChunk.m_chunkCount * 2);
				listChunk = (listChunk.m_nextChunk = this.m_tailChunk);
			}
			TInputOutput[] chunk = listChunk.m_chunk;
			ListChunk<TInputOutput> listChunk2 = listChunk;
			int chunkCount = listChunk2.m_chunkCount;
			listChunk2.m_chunkCount = chunkCount + 1;
			chunk[chunkCount] = e;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x000394D4 File Offset: 0x000376D4
		internal ListChunk<TInputOutput> Next
		{
			get
			{
				return this.m_nextChunk;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x000394DC File Offset: 0x000376DC
		internal int Count
		{
			get
			{
				return this.m_chunkCount;
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000394E4 File Offset: 0x000376E4
		public IEnumerator<TInputOutput> GetEnumerator()
		{
			for (ListChunk<TInputOutput> curr = this; curr != null; curr = curr.m_nextChunk)
			{
				int num;
				for (int i = 0; i < curr.m_chunkCount; i = num + 1)
				{
					yield return curr.m_chunk[i];
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000394F3 File Offset: 0x000376F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<TInputOutput>)this).GetEnumerator();
		}

		// Token: 0x04000931 RID: 2353
		internal TInputOutput[] m_chunk;

		// Token: 0x04000932 RID: 2354
		private int m_chunkCount;

		// Token: 0x04000933 RID: 2355
		private ListChunk<TInputOutput> m_nextChunk;

		// Token: 0x04000934 RID: 2356
		private ListChunk<TInputOutput> m_tailChunk;
	}
}
