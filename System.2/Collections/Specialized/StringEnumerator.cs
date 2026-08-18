using System;

namespace System.Collections.Specialized
{
	// Token: 0x020003B6 RID: 950
	public class StringEnumerator
	{
		// Token: 0x060023D3 RID: 9171 RVA: 0x000A8D3C File Offset: 0x000A6F3C
		internal StringEnumerator(StringCollection mappings)
		{
			this.temp = mappings;
			this.baseEnumerator = this.temp.GetEnumerator();
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000A8D5C File Offset: 0x000A6F5C
		public string Current
		{
			get
			{
				return (string)this.baseEnumerator.Current;
			}
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000A8D6E File Offset: 0x000A6F6E
		public bool MoveNext()
		{
			return this.baseEnumerator.MoveNext();
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000A8D7B File Offset: 0x000A6F7B
		public void Reset()
		{
			this.baseEnumerator.Reset();
		}

		// Token: 0x04001FF6 RID: 8182
		private IEnumerator baseEnumerator;

		// Token: 0x04001FF7 RID: 8183
		private IEnumerable temp;
	}
}
