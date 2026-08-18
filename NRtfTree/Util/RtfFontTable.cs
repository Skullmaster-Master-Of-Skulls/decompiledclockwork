using System;
using System.Collections;
using System.Collections.Generic;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x0200000B RID: 11
	public class RtfFontTable
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004D19 File Offset: 0x00002F19
		public RtfFontTable()
		{
			this.fonts = new Dictionary<int, string>();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004D2C File Offset: 0x00002F2C
		public void AddFont(string name)
		{
			this.fonts.Add(this.newFontIndex(), name);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004D40 File Offset: 0x00002F40
		public void AddFont(int index, string name)
		{
			this.fonts.Add(index, name);
		}

		// Token: 0x17000040 RID: 64
		public string this[int index]
		{
			get
			{
				return this.fonts[index];
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004D5D File Offset: 0x00002F5D
		public int Count
		{
			get
			{
				return this.fonts.Count;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004D6C File Offset: 0x00002F6C
		public int IndexOf(string name)
		{
			int result = -1;
			IEnumerator enumerator = this.fonts.GetEnumerator();
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (((KeyValuePair<int, string>)obj).Value.Equals(name))
				{
					KeyValuePair<int, string> keyValuePair = (KeyValuePair<int, string>)enumerator.Current;
					result = keyValuePair.Key;
					break;
				}
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004DD0 File Offset: 0x00002FD0
		private int newFontIndex()
		{
			int num = -1;
			IEnumerator enumerator = this.fonts.GetEnumerator();
			enumerator.Reset();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				if (((KeyValuePair<int, string>)obj).Key > num)
				{
					KeyValuePair<int, string> keyValuePair = (KeyValuePair<int, string>)enumerator.Current;
					num = keyValuePair.Key;
				}
			}
			return num + 1;
		}

		// Token: 0x04000039 RID: 57
		private Dictionary<int, string> fonts;
	}
}
