using System;
using System.Collections;

namespace Telerik.Pdf
{
	// Token: 0x0200164E RID: 5710
	public class PdfCMap : PdfContentStream
	{
		// Token: 0x0600DD66 RID: 56678 RVA: 0x00305C81 File Offset: 0x00303E81
		public PdfCMap(PdfObjectId id) : base(id)
		{
			this.ranges = new SortedList();
		}

		// Token: 0x170043C3 RID: 17347
		// (set) Token: 0x0600DD67 RID: 56679 RVA: 0x00305C95 File Offset: 0x00303E95
		public PdfCIDSystemInfo SystemInfo
		{
			set
			{
				this.systemInfo = value;
			}
		}

		// Token: 0x0600DD68 RID: 56680 RVA: 0x00305CA0 File Offset: 0x00303EA0
		public void AddBfRanges(IDictionary map)
		{
			foreach (object obj in map)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.AddBfRange((int)Convert.ToUInt16(dictionaryEntry.Key), (int)Convert.ToUInt16(dictionaryEntry.Value));
			}
		}

		// Token: 0x0600DD69 RID: 56681 RVA: 0x00305D0C File Offset: 0x00303F0C
		public void AddBfRange(int glyphIndex, int unicodeValue)
		{
			this.ranges.Add(glyphIndex, unicodeValue);
		}

		// Token: 0x0600DD6A RID: 56682 RVA: 0x00305D28 File Offset: 0x00303F28
		protected internal override void Write(PdfWriter writer)
		{
			base.WriteLine("/CIDInit /ProcSet findresource begin");
			base.WriteLine("12 dict begin");
			base.WriteLine("begincmap");
			base.WriteLine("/CIDSystemInfo");
			base.WriteLine(this.systemInfo);
			base.WriteLine("def");
			base.WriteLine(string.Format("/CMapName /{0} def", "Adobe-Identity-UCS"));
			base.WriteLine("/CMapType 2 def");
			if (this.ranges.Count > 0)
			{
				BfEntryList entries = this.GroupCMapEntries();
				this.WriteCodespaceRange(entries);
				this.WriteBfChars(entries);
				this.WriteBfRanges(entries);
			}
			base.WriteLine("endcmap");
			base.WriteLine("CMapName currentdict /CMap defineresource pop");
			base.WriteLine("end");
			base.Write("end");
			base.Write(writer);
		}

		// Token: 0x0600DD6B RID: 56683 RVA: 0x00305DF8 File Offset: 0x00303FF8
		private void WriteCodespaceRange(BfEntryList entries)
		{
			BfEntry bfEntry = entries[0];
			BfEntry bfEntry2 = entries[entries.Count - 1];
			base.WriteLine("1 begincodespacerange");
			base.WriteLine(string.Format("<{0:X4}> <{1:X4}>", bfEntry.StartGlyphIndex, bfEntry2.EndGlyphIndex));
			base.WriteLine("endcodespacerange");
		}

		// Token: 0x0600DD6C RID: 56684 RVA: 0x00305E58 File Offset: 0x00304058
		private void WriteBfChars(BfEntryList entries)
		{
			BfEntry[] chars = entries.Chars;
			int num = chars.Length / 100 + ((chars.Length % 100 > 0) ? 1 : 0);
			for (int i = 0; i < num; i++)
			{
				int num2;
				if (i + 1 == num)
				{
					num2 = chars.Length - i * 100;
				}
				else
				{
					num2 = 100;
				}
				base.WriteLine(string.Format("{0} beginbfchar", num2));
				for (int j = 0; j < num2; j++)
				{
					BfEntry bfEntry = chars[i * 100 + j];
					base.WriteLine(string.Format("<{0:X4}> <{1:X4}>", bfEntry.StartGlyphIndex, bfEntry.UnicodeValue));
				}
				base.WriteLine("endbfchar");
			}
		}

		// Token: 0x0600DD6D RID: 56685 RVA: 0x00305F0C File Offset: 0x0030410C
		private void WriteBfRanges(BfEntryList entries)
		{
			BfEntry[] array = entries.Ranges;
			int num = array.Length / 100 + ((array.Length % 100 > 0) ? 1 : 0);
			for (int i = 0; i < num; i++)
			{
				int num2;
				if (i + 1 == num)
				{
					num2 = array.Length - i * 100;
				}
				else
				{
					num2 = 100;
				}
				base.WriteLine(string.Format("{0} beginbfrange", num2));
				for (int j = 0; j < num2; j++)
				{
					BfEntry bfEntry = array[i * 100 + j];
					base.WriteLine(string.Format("<{0:X4}> <{1:X4}> <{2:X4}>", bfEntry.StartGlyphIndex, bfEntry.EndGlyphIndex, bfEntry.UnicodeValue));
				}
				base.WriteLine("endbfrange");
			}
		}

		// Token: 0x0600DD6E RID: 56686 RVA: 0x00305FD0 File Offset: 0x003041D0
		private BfEntryList GroupCMapEntries()
		{
			BfEntryList bfEntryList = new BfEntryList();
			int num = (int)this.ranges.GetKey(0);
			int num2 = (int)this.ranges[num];
			BfEntry bfEntry = new BfEntry(num, num2);
			bfEntryList.Add(bfEntry);
			for (int i = 1; i < this.ranges.Count; i++)
			{
				int num3 = (int)this.ranges.GetKey(i);
				int num4 = (int)this.ranges[num3];
				if (num4 == num2 + 1 && num3 == num + 1)
				{
					bfEntry.IncrementEndIndex();
				}
				else
				{
					bfEntry = new BfEntry(num3, num4);
					bfEntryList.Add(bfEntry);
				}
				num = num3;
				num2 = num4;
			}
			return bfEntryList;
		}

		// Token: 0x04003EF7 RID: 16119
		public const string DefaultName = "Adobe-Identity-UCS";

		// Token: 0x04003EF8 RID: 16120
		private PdfCIDSystemInfo systemInfo;

		// Token: 0x04003EF9 RID: 16121
		private SortedList ranges;
	}
}
