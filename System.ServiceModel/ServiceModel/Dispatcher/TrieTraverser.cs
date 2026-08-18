using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D8 RID: 1240
	internal struct TrieTraverser
	{
		// Token: 0x06002F0A RID: 12042 RVA: 0x000B5D15 File Offset: 0x000B3F15
		internal TrieTraverser(TrieSegment root, string prefix)
		{
			this.prefix = prefix;
			this.rootSegment = root;
			this.segment = null;
			this.segmentIndex = -1;
			this.offset = 0;
			this.length = prefix.Length;
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000B5D46 File Offset: 0x000B3F46
		internal int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x000B5D4E File Offset: 0x000B3F4E
		internal int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x000B5D56 File Offset: 0x000B3F56
		// (set) Token: 0x06002F0E RID: 12046 RVA: 0x000B5D5E File Offset: 0x000B3F5E
		internal TrieSegment Segment
		{
			get
			{
				return this.segment;
			}
			set
			{
				this.segment = value;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002F0F RID: 12047 RVA: 0x000B5D67 File Offset: 0x000B3F67
		internal int SegmentIndex
		{
			get
			{
				return this.segmentIndex;
			}
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000B5D70 File Offset: 0x000B3F70
		internal bool MoveNext()
		{
			if (this.segment != null)
			{
				int num = this.segment.Length;
				this.offset += num;
				this.length -= num;
				if (this.length > 0)
				{
					this.segmentIndex = this.segment.GetChildPosition(this.prefix, this.offset, this.length);
					if (this.segmentIndex > -1)
					{
						this.segment = this.segment.GetChild(this.segmentIndex);
						return true;
					}
				}
				else
				{
					this.segmentIndex = -1;
				}
				this.segment = null;
			}
			else if (this.rootSegment != null)
			{
				this.segment = this.rootSegment;
				this.rootSegment = null;
				return true;
			}
			return false;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000B5E2C File Offset: 0x000B402C
		internal bool MoveNextByFirstChar()
		{
			if (this.segment != null)
			{
				int num = this.segment.Length;
				this.offset += num;
				this.length -= num;
				if (this.length > 0)
				{
					this.segmentIndex = this.segment.GetChildPosition(this.prefix[this.offset]);
					if (this.segmentIndex > -1)
					{
						this.segment = this.segment.GetChild(this.segmentIndex);
						return true;
					}
				}
				else
				{
					this.segmentIndex = -1;
				}
				this.segment = null;
			}
			else if (this.rootSegment != null)
			{
				this.segment = this.rootSegment;
				this.rootSegment = null;
				return true;
			}
			return false;
		}

		// Token: 0x040025B3 RID: 9651
		private int length;

		// Token: 0x040025B4 RID: 9652
		private int offset;

		// Token: 0x040025B5 RID: 9653
		private string prefix;

		// Token: 0x040025B6 RID: 9654
		private TrieSegment rootSegment;

		// Token: 0x040025B7 RID: 9655
		private TrieSegment segment;

		// Token: 0x040025B8 RID: 9656
		private int segmentIndex;
	}
}
