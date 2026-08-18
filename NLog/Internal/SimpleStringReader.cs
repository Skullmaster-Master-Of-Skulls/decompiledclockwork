using System;

namespace NLog.Internal
{
	// Token: 0x020000AC RID: 172
	internal class SimpleStringReader
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0000C15E File Offset: 0x0000A35E
		public SimpleStringReader(string text)
		{
			this.text = text;
			this.Position = 0;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000C174 File Offset: 0x0000A374
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000C17C File Offset: 0x0000A37C
		internal int Position { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000C185 File Offset: 0x0000A385
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000C18D File Offset: 0x0000A38D
		internal int Peek()
		{
			if (this.Position < this.text.Length)
			{
				return (int)this.text[this.Position];
			}
			return -1;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		internal int Read()
		{
			if (this.Position < this.text.Length)
			{
				return (int)this.text[this.Position++];
			}
			return -1;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000C1F6 File Offset: 0x0000A3F6
		internal string Substring(int startIndex, int endIndex)
		{
			return this.text.Substring(startIndex, endIndex - startIndex);
		}

		// Token: 0x0400011E RID: 286
		private readonly string text;
	}
}
