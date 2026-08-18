using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004DB RID: 1243
	internal class TrieBranchIndex : QueryBranchIndex
	{
		// Token: 0x06002F21 RID: 12065 RVA: 0x000B61DE File Offset: 0x000B43DE
		internal TrieBranchIndex()
		{
			this.count = 0;
			this.trie = new Trie();
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x000B61F8 File Offset: 0x000B43F8
		internal override int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000B2F RID: 2863
		internal override QueryBranch this[object key]
		{
			get
			{
				TrieSegment trieSegment = this.trie[(string)key];
				if (trieSegment != null)
				{
					return trieSegment.Data;
				}
				return null;
			}
			set
			{
				TrieSegment trieSegment = this.trie.Add((string)key);
				this.count++;
				trieSegment.Data = value;
			}
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000B6260 File Offset: 0x000B4460
		internal override void CollectXPathFilters(ICollection<MessageFilter> filters)
		{
			this.trie.Root.CollectXPathFilters(filters);
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000B6274 File Offset: 0x000B4474
		private void Match(int valIndex, string segment, QueryBranchResultSet results)
		{
			TrieTraverser trieTraverser = new TrieTraverser(this.trie.Root, segment);
			while (trieTraverser.MoveNext())
			{
				object data = trieTraverser.Segment.Data;
				if (data != null)
				{
					results.Add((QueryBranch)data, valIndex);
				}
			}
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000B62BC File Offset: 0x000B44BC
		internal override void Match(int valIndex, ref Value val, QueryBranchResultSet results)
		{
			if (ValueDataType.Sequence == val.Type)
			{
				NodeSequence sequence = val.Sequence;
				for (int i = 0; i < sequence.Count; i++)
				{
					this.Match(valIndex, sequence.Items[i].StringValue(), results);
				}
				return;
			}
			this.Match(valIndex, val.String, results);
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000B6312 File Offset: 0x000B4512
		internal override void Remove(object key)
		{
			this.trie.Remove((string)key);
			this.count--;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000B6333 File Offset: 0x000B4533
		internal override void Trim()
		{
			this.trie.Trim();
		}

		// Token: 0x040025BC RID: 9660
		private int count;

		// Token: 0x040025BD RID: 9661
		private Trie trie;
	}
}
