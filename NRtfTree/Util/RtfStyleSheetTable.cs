using System;
using System.Collections;
using System.Collections.Generic;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000013 RID: 19
	public class RtfStyleSheetTable
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00005FA7 File Offset: 0x000041A7
		public RtfStyleSheetTable()
		{
			this.stylesheets = new Dictionary<int, RtfStyleSheet>();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005FBA File Offset: 0x000041BA
		public void AddStyleSheet(RtfStyleSheet ss)
		{
			ss.Index = this.newStyleSheetIndex();
			this.stylesheets.Add(ss.Index, ss);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005FDA File Offset: 0x000041DA
		public void AddStyleSheet(int index, RtfStyleSheet ss)
		{
			ss.Index = index;
			this.stylesheets.Add(index, ss);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005FF0 File Offset: 0x000041F0
		public void RemoveStyleSheet(int index)
		{
			this.stylesheets.Remove(index);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005FFF File Offset: 0x000041FF
		public void RemoveStyleSheet(RtfStyleSheet ss)
		{
			this.stylesheets.Remove(ss.Index);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006013 File Offset: 0x00004213
		public RtfStyleSheet GetStyleSheet(int index)
		{
			return this.stylesheets[index];
		}

		// Token: 0x17000057 RID: 87
		public RtfStyleSheet this[int index]
		{
			get
			{
				return this.stylesheets[index];
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000602F File Offset: 0x0000422F
		public int Count
		{
			get
			{
				return this.stylesheets.Count;
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000603C File Offset: 0x0000423C
		public int IndexOf(string name)
		{
			int result = -1;
			IEnumerator enumerator = this.stylesheets.GetEnumerator();
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (((KeyValuePair<int, RtfStyleSheet>)obj).Value.Name.Equals(name))
				{
					KeyValuePair<int, RtfStyleSheet> keyValuePair = (KeyValuePair<int, RtfStyleSheet>)enumerator.Current;
					result = keyValuePair.Key;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000060A4 File Offset: 0x000042A4
		private int newStyleSheetIndex()
		{
			int num = -1;
			IEnumerator enumerator = this.stylesheets.GetEnumerator();
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (((KeyValuePair<int, RtfStyleSheet>)obj).Key > num)
				{
					KeyValuePair<int, RtfStyleSheet> keyValuePair = (KeyValuePair<int, RtfStyleSheet>)enumerator.Current;
					num = keyValuePair.Key;
				}
			}
			return num + 1;
		}

		// Token: 0x04000062 RID: 98
		private Dictionary<int, RtfStyleSheet> stylesheets;
	}
}
