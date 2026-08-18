using System;
using System.Collections.Generic;
using System.Text;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D7 RID: 1239
	internal class TrieSegment
	{
		// Token: 0x06002EED RID: 12013 RVA: 0x000B580B File Offset: 0x000B3A0B
		internal TrieSegment() : this('\0')
		{
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000B5814 File Offset: 0x000B3A14
		internal TrieSegment(char firstChar) : this(firstChar, string.Empty)
		{
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000B5822 File Offset: 0x000B3A22
		internal TrieSegment(char firstChar, string segmentTail)
		{
			this.SetSegment(firstChar, segmentTail);
			this.children = new SortedBuffer<TrieSegment, TrieSegmentComparer>(TrieSegment.SegComparer);
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000B5842 File Offset: 0x000B3A42
		internal TrieSegment(string sourceSegment, int offset, int length)
		{
			this.SetSegmentString(sourceSegment, offset, length);
			this.children = new SortedBuffer<TrieSegment, TrieSegmentComparer>(TrieSegment.SegComparer);
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000B5863 File Offset: 0x000B3A63
		internal bool CanMerge
		{
			get
			{
				return this.data == null && 1 == this.children.Count;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000B587D File Offset: 0x000B3A7D
		internal bool CanPrune
		{
			get
			{
				return this.data == null && this.children.Count == 0;
			}
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000B5898 File Offset: 0x000B3A98
		internal void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			if (this.data != null)
			{
				this.data.Branch.CollectXPathFilters(filters);
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].CollectXPathFilters(filters);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000B58E6 File Offset: 0x000B3AE6
		// (set) Token: 0x06002EF5 RID: 12021 RVA: 0x000B58EE File Offset: 0x000B3AEE
		internal QueryBranch Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000B58F7 File Offset: 0x000B3AF7
		internal char FirstChar
		{
			get
			{
				return this.segmentFirstChar;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000B58FF File Offset: 0x000B3AFF
		internal bool HasChildren
		{
			get
			{
				return this.children.Count > 0;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000B590F File Offset: 0x000B3B0F
		internal int Length
		{
			get
			{
				return this.segmentLength;
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000B5917 File Offset: 0x000B3B17
		internal TrieSegment AddChild(TrieSegment segment)
		{
			this.children.Insert(segment);
			segment.parent = this;
			return segment;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000B5930 File Offset: 0x000B3B30
		internal int FindDivergence(string compareString, int offset, int length)
		{
			if (compareString[offset] != this.segmentFirstChar)
			{
				return 0;
			}
			length--;
			offset++;
			int num = (length <= this.segmentTail.Length) ? length : this.segmentTail.Length;
			int i = 0;
			int num2 = offset;
			while (i < num)
			{
				if (compareString[num2] != this.segmentTail[i])
				{
					return i + 1;
				}
				i++;
				num2++;
			}
			if (length < this.segmentTail.Length)
			{
				return length + 1;
			}
			return -1;
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000B59B3 File Offset: 0x000B3BB3
		internal TrieSegment GetChild(int index)
		{
			return this.children[index];
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000B59C4 File Offset: 0x000B3BC4
		internal int GetChildPosition(string matchString, int offset, int length)
		{
			if (this.HasChildren)
			{
				char key = matchString[offset];
				int num = length - 1;
				int indexA = offset + 1;
				int num2 = this.children.IndexOfKey<char>(key, TrieSegment.SegKeyComparer);
				if (num2 >= 0)
				{
					TrieSegment trieSegment = this.children[num2];
					if (num >= trieSegment.segmentTail.Length && (trieSegment.segmentTail.Length == 0 || string.CompareOrdinal(matchString, indexA, trieSegment.segmentTail, 0, trieSegment.segmentTail.Length) == 0))
					{
						return num2;
					}
				}
			}
			return -1;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000B5A4A File Offset: 0x000B3C4A
		internal int GetChildPosition(char ch)
		{
			return this.children.IndexOfKey<char>(ch, TrieSegment.SegKeyComparer);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000B5A5D File Offset: 0x000B3C5D
		internal int IndexOf(TrieSegment segment)
		{
			return this.children.IndexOf(segment);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000B5A6C File Offset: 0x000B3C6C
		internal void MergeChild(TrieSegment segment)
		{
			int num = this.IndexOf(segment);
			if (num > -1)
			{
				this.MergeChild(num);
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000B5A8C File Offset: 0x000B3C8C
		internal void MergeChild(int childIndex)
		{
			TrieSegment trieSegment = this.children[childIndex];
			if (trieSegment.CanMerge)
			{
				TrieSegment trieSegment2 = trieSegment.children[0];
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(trieSegment.segmentTail);
				stringBuilder.Append(trieSegment2.segmentFirstChar);
				stringBuilder.Append(trieSegment2.segmentTail);
				trieSegment2.SetSegment(trieSegment.segmentFirstChar, stringBuilder.ToString());
				trieSegment2.parent = this;
				this.children.Exchange(trieSegment, trieSegment2);
				trieSegment.parent = null;
			}
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000B5B15 File Offset: 0x000B3D15
		internal void Remove()
		{
			if (this.parent != null)
			{
				this.parent.RemoveChild(this);
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000B5B2C File Offset: 0x000B3D2C
		private void RemoveChild(TrieSegment segment)
		{
			int num = this.IndexOf(segment);
			if (num >= 0)
			{
				this.RemoveChild(num, true);
			}
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000B5B50 File Offset: 0x000B3D50
		internal void RemoveChild(int childIndex, bool fixupTree)
		{
			TrieSegment trieSegment = this.children[childIndex];
			trieSegment.parent = null;
			this.children.RemoveAt(childIndex);
			if (this.children.Count == 0)
			{
				if (fixupTree && this.CanPrune)
				{
					this.Remove();
					return;
				}
			}
			else if (fixupTree && this.CanMerge && this.parent != null)
			{
				this.parent.MergeChild(this);
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000B5BBB File Offset: 0x000B3DBB
		private void SetSegment(char firstChar, string segmentTail)
		{
			this.segmentFirstChar = firstChar;
			this.segmentTail = segmentTail;
			this.segmentLength = ((firstChar == '\0') ? 0 : (1 + segmentTail.Length));
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000B5BDF File Offset: 0x000B3DDF
		private void SetSegmentString(string segmentString, int offset, int length)
		{
			this.segmentFirstChar = segmentString[offset];
			if (length > 1)
			{
				this.segmentTail = segmentString.Substring(offset + 1, length - 1);
			}
			else
			{
				this.segmentTail = string.Empty;
			}
			this.segmentLength = length;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000B5C18 File Offset: 0x000B3E18
		private TrieSegment SplitAt(int charIndex)
		{
			TrieSegment trieSegment;
			if (1 == charIndex)
			{
				trieSegment = new TrieSegment(this.segmentFirstChar);
			}
			else
			{
				trieSegment = new TrieSegment(this.segmentFirstChar, this.segmentTail.Substring(0, charIndex - 1));
			}
			charIndex--;
			this.SetSegmentString(this.segmentTail, charIndex, this.segmentTail.Length - charIndex);
			trieSegment.AddChild(this);
			return trieSegment;
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000B5C7C File Offset: 0x000B3E7C
		internal TrieSegment SplitChild(int childIndex, int charIndex)
		{
			TrieSegment trieSegment = this.children[childIndex];
			this.children.Remove(trieSegment);
			TrieSegment trieSegment2 = trieSegment.SplitAt(charIndex);
			this.children.Insert(trieSegment2);
			trieSegment2.parent = this;
			return trieSegment2;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000B5CC0 File Offset: 0x000B3EC0
		internal void Trim()
		{
			this.children.Trim();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.children[i].Trim();
			}
		}

		// Token: 0x040025AB RID: 9643
		private static readonly TrieSegmentKeyComparer SegKeyComparer = new TrieSegmentKeyComparer();

		// Token: 0x040025AC RID: 9644
		private static readonly TrieSegmentComparer SegComparer = new TrieSegmentComparer();

		// Token: 0x040025AD RID: 9645
		private SortedBuffer<TrieSegment, TrieSegmentComparer> children;

		// Token: 0x040025AE RID: 9646
		private QueryBranch data;

		// Token: 0x040025AF RID: 9647
		private TrieSegment parent;

		// Token: 0x040025B0 RID: 9648
		private char segmentFirstChar;

		// Token: 0x040025B1 RID: 9649
		private string segmentTail;

		// Token: 0x040025B2 RID: 9650
		private int segmentLength;
	}
}
