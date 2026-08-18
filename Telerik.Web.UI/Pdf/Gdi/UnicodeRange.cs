using System;
using System.Text;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001641 RID: 5697
	internal class UnicodeRange
	{
		// Token: 0x0600DD09 RID: 56585 RVA: 0x00304958 File Offset: 0x00302B58
		public UnicodeRange(GdiDeviceContent dc, int start, int end)
		{
			if (start > end)
			{
				throw new ArgumentException("start cannot be greater than end");
			}
			this.dc = dc;
			this.start = start;
			this.end = end;
		}

		// Token: 0x0600DD0A RID: 56586 RVA: 0x00304984 File Offset: 0x00302B84
		public int MapCharacter(char c)
		{
			if (this.indices == null)
			{
				this.LoadGlyphIndices();
			}
			return (int)this.indices[(int)c - this.start];
		}

		// Token: 0x170043AC RID: 17324
		// (get) Token: 0x0600DD0B RID: 56587 RVA: 0x003049A3 File Offset: 0x00302BA3
		public int Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x170043AD RID: 17325
		// (get) Token: 0x0600DD0C RID: 56588 RVA: 0x003049AB File Offset: 0x00302BAB
		public int End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x0600DD0D RID: 56589 RVA: 0x003049B4 File Offset: 0x00302BB4
		private void LoadGlyphIndices()
		{
			string text = this.BuildString();
			this.indices = new ushort[text.Length];
			NativeMethods.GetGlyphIndices(this.dc.Handle, text, text.Length, this.indices, 0);
		}

		// Token: 0x0600DD0E RID: 56590 RVA: 0x003049F8 File Offset: 0x00302BF8
		private string BuildString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.End - this.Start);
			for (int i = this.Start; i <= this.End; i++)
			{
				stringBuilder.Append((char)i);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04003EE5 RID: 16101
		private GdiDeviceContent dc;

		// Token: 0x04003EE6 RID: 16102
		private int start;

		// Token: 0x04003EE7 RID: 16103
		private int end;

		// Token: 0x04003EE8 RID: 16104
		private ushort[] indices;
	}
}
