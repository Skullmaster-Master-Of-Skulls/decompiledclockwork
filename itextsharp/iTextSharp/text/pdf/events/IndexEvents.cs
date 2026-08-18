using System;
using System.Collections.Generic;
using System.Text;
using System.util;

namespace iTextSharp.text.pdf.events
{
	// Token: 0x020004EB RID: 1259
	public class IndexEvents : PdfPageEventHelper
	{
		// Token: 0x06002B18 RID: 11032 RVA: 0x001058C4 File Offset: 0x001048C4
		public override void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
		{
			this.indextag[text] = writer.PageNumber;
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x001058DC File Offset: 0x001048DC
		public Chunk Create(string text, string in1, string in2, string in3)
		{
			Chunk chunk = new Chunk(text);
			object arg = "idx_";
			long num;
			this.indexcounter = (num = this.indexcounter) + 1L;
			string text2 = arg + num;
			chunk.SetGenericTag(text2);
			chunk.SetLocalDestination(text2);
			IndexEvents.Entry item = new IndexEvents.Entry(in1, in2, in3, text2, this);
			this.indexentry.Add(item);
			return chunk;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0010593B File Offset: 0x0010493B
		public Chunk Create(string text, string in1)
		{
			return this.Create(text, in1, "", "");
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0010594F File Offset: 0x0010494F
		public Chunk Create(string text, string in1, string in2)
		{
			return this.Create(text, in1, in2, "");
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x00105960 File Offset: 0x00104960
		public void Create(Chunk text, string in1, string in2, string in3)
		{
			object arg = "idx_";
			long num;
			this.indexcounter = (num = this.indexcounter) + 1L;
			string text2 = arg + num;
			text.SetGenericTag(text2);
			text.SetLocalDestination(text2);
			IndexEvents.Entry item = new IndexEvents.Entry(in1, in2, in3, text2, this);
			this.indexentry.Add(item);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x001059B7 File Offset: 0x001049B7
		public void Create(Chunk text, string in1)
		{
			this.Create(text, in1, "", "");
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x001059CB File Offset: 0x001049CB
		public void Create(Chunk text, string in1, string in2)
		{
			this.Create(text, in1, in2, "");
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x001059DB File Offset: 0x001049DB
		public void SetComparator(IComparer<IndexEvents.Entry> aComparator)
		{
			this.comparator = aComparator;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x001059E4 File Offset: 0x001049E4
		public List<IndexEvents.Entry> GetSortedEntries()
		{
			Dictionary<string, IndexEvents.Entry> dictionary = new Dictionary<string, IndexEvents.Entry>();
			for (int i = 0; i < this.indexentry.Count; i++)
			{
				IndexEvents.Entry entry = this.indexentry[i];
				string key = entry.GetKey();
				IndexEvents.Entry entry2;
				dictionary.TryGetValue(key, out entry2);
				if (entry2 != null)
				{
					entry2.AddPageNumberAndTag(entry.GetPageNumber(), entry.GetTag());
				}
				else
				{
					entry.AddPageNumberAndTag(entry.GetPageNumber(), entry.GetTag());
					dictionary[key] = entry;
				}
			}
			List<IndexEvents.Entry> list = new List<IndexEvents.Entry>(dictionary.Values);
			list.Sort(0, list.Count, this.comparator);
			return list;
		}

		// Token: 0x04001DC8 RID: 7624
		private Dictionary<string, int> indextag = new Dictionary<string, int>();

		// Token: 0x04001DC9 RID: 7625
		private long indexcounter;

		// Token: 0x04001DCA RID: 7626
		private List<IndexEvents.Entry> indexentry = new List<IndexEvents.Entry>();

		// Token: 0x04001DCB RID: 7627
		private IComparer<IndexEvents.Entry> comparator = new IndexEvents.ISortIndex();

		// Token: 0x020004EC RID: 1260
		private class ISortIndex : IComparer<IndexEvents.Entry>
		{
			// Token: 0x06002B22 RID: 11042 RVA: 0x00105AAC File Offset: 0x00104AAC
			public int Compare(IndexEvents.Entry en1, IndexEvents.Entry en2)
			{
				int result = 0;
				if (en1.GetIn1() != null && en2.GetIn1() != null && (result = Util.CompareToIgnoreCase(en1.GetIn1(), en2.GetIn1())) == 0 && en1.GetIn2() != null && en2.GetIn2() != null && (result = Util.CompareToIgnoreCase(en1.GetIn2(), en2.GetIn2())) == 0 && en1.GetIn3() != null && en2.GetIn3() != null)
				{
					result = Util.CompareToIgnoreCase(en1.GetIn3(), en2.GetIn3());
				}
				return result;
			}
		}

		// Token: 0x020004ED RID: 1261
		public class Entry
		{
			// Token: 0x06002B24 RID: 11044 RVA: 0x00105B30 File Offset: 0x00104B30
			public Entry(string aIn1, string aIn2, string aIn3, string aTag, IndexEvents parent)
			{
				this.in1 = aIn1;
				this.in2 = aIn2;
				this.in3 = aIn3;
				this.tag = aTag;
				this.parent = parent;
			}

			// Token: 0x06002B25 RID: 11045 RVA: 0x00105B7E File Offset: 0x00104B7E
			public string GetIn1()
			{
				return this.in1;
			}

			// Token: 0x06002B26 RID: 11046 RVA: 0x00105B86 File Offset: 0x00104B86
			public string GetIn2()
			{
				return this.in2;
			}

			// Token: 0x06002B27 RID: 11047 RVA: 0x00105B8E File Offset: 0x00104B8E
			public string GetIn3()
			{
				return this.in3;
			}

			// Token: 0x06002B28 RID: 11048 RVA: 0x00105B96 File Offset: 0x00104B96
			public string GetTag()
			{
				return this.tag;
			}

			// Token: 0x06002B29 RID: 11049 RVA: 0x00105B9E File Offset: 0x00104B9E
			public int GetPageNumber()
			{
				if (this.parent.indextag.ContainsKey(this.tag))
				{
					return this.parent.indextag[this.tag];
				}
				return -1;
			}

			// Token: 0x06002B2A RID: 11050 RVA: 0x00105BD0 File Offset: 0x00104BD0
			public void AddPageNumberAndTag(int number, string tag)
			{
				this.pagenumbers.Add(number);
				this.tags.Add(tag);
			}

			// Token: 0x06002B2B RID: 11051 RVA: 0x00105BEC File Offset: 0x00104BEC
			public string GetKey()
			{
				return string.Concat(new string[]
				{
					this.in1,
					"!",
					this.in2,
					"!",
					this.in3
				});
			}

			// Token: 0x06002B2C RID: 11052 RVA: 0x00105C31 File Offset: 0x00104C31
			public List<int> GetPagenumbers()
			{
				return this.pagenumbers;
			}

			// Token: 0x06002B2D RID: 11053 RVA: 0x00105C39 File Offset: 0x00104C39
			public List<string> GetTags()
			{
				return this.tags;
			}

			// Token: 0x06002B2E RID: 11054 RVA: 0x00105C44 File Offset: 0x00104C44
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.in1).Append(' ');
				stringBuilder.Append(this.in2).Append(' ');
				stringBuilder.Append(this.in3).Append(' ');
				for (int i = 0; i < this.pagenumbers.Count; i++)
				{
					stringBuilder.Append(this.pagenumbers[i]).Append(' ');
				}
				return stringBuilder.ToString();
			}

			// Token: 0x04001DCC RID: 7628
			private string in1;

			// Token: 0x04001DCD RID: 7629
			private string in2;

			// Token: 0x04001DCE RID: 7630
			private string in3;

			// Token: 0x04001DCF RID: 7631
			private string tag;

			// Token: 0x04001DD0 RID: 7632
			private List<int> pagenumbers = new List<int>();

			// Token: 0x04001DD1 RID: 7633
			private List<string> tags = new List<string>();

			// Token: 0x04001DD2 RID: 7634
			private IndexEvents parent;
		}
	}
}
