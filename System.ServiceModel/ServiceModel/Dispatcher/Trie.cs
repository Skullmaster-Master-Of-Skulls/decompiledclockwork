using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D9 RID: 1241
	internal class Trie
	{
		// Token: 0x06002F12 RID: 12050 RVA: 0x000B5EE7 File Offset: 0x000B40E7
		internal Trie()
		{
			this.hasDescendants = false;
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000B5EF6 File Offset: 0x000B40F6
		private bool HasDescendants
		{
			get
			{
				return this.hasDescendants;
			}
		}

		// Token: 0x17000B2B RID: 2859
		internal TrieSegment this[string prefix]
		{
			get
			{
				return this.Find(prefix);
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002F15 RID: 12053 RVA: 0x000B5F07 File Offset: 0x000B4107
		internal TrieSegment Root
		{
			get
			{
				this.EnsureRoot();
				return this.root;
			}
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000B5F18 File Offset: 0x000B4118
		internal TrieSegment Add(string newPrefix)
		{
			if (newPrefix.Length <= 0)
			{
				return this.Root;
			}
			this.EnsureRoot();
			TrieTraverser trieTraverser = new TrieTraverser(this.root, newPrefix);
			TrieSegment segment;
			for (;;)
			{
				segment = trieTraverser.Segment;
				if (trieTraverser.MoveNextByFirstChar())
				{
					int charIndex;
					if (segment != null && -1 != (charIndex = trieTraverser.Segment.FindDivergence(newPrefix, trieTraverser.Offset, trieTraverser.Length)))
					{
						trieTraverser.Segment = segment.SplitChild(trieTraverser.SegmentIndex, charIndex);
					}
				}
				else
				{
					if (trieTraverser.Length <= 0)
					{
						break;
					}
					trieTraverser.Segment = segment.AddChild(new TrieSegment(newPrefix, trieTraverser.Offset, trieTraverser.Length));
				}
			}
			this.hasDescendants = true;
			return segment;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000B5FC9 File Offset: 0x000B41C9
		private void EnsureRoot()
		{
			if (this.root == null)
			{
				this.root = new TrieSegment();
			}
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000B5FE0 File Offset: 0x000B41E0
		private TrieSegment Find(string prefix)
		{
			if (prefix.Length == 0)
			{
				return this.Root;
			}
			if (!this.HasDescendants)
			{
				return null;
			}
			TrieTraverser trieTraverser = new TrieTraverser(this.root, prefix);
			TrieSegment result = null;
			while (trieTraverser.MoveNext())
			{
				result = trieTraverser.Segment;
			}
			if (trieTraverser.Length > 0)
			{
				return null;
			}
			return result;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000B6036 File Offset: 0x000B4236
		private void PruneRoot()
		{
			if (this.root != null && this.root.CanPrune)
			{
				this.root = null;
			}
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000B6054 File Offset: 0x000B4254
		internal void Remove(string segment)
		{
			TrieSegment trieSegment = this[segment];
			if (trieSegment == null)
			{
				return;
			}
			if (trieSegment.HasChildren)
			{
				trieSegment.Data = null;
				return;
			}
			if (trieSegment == this.root)
			{
				this.root = null;
				this.hasDescendants = false;
				return;
			}
			trieSegment.Remove();
			this.PruneRoot();
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000B60A1 File Offset: 0x000B42A1
		internal void Trim()
		{
			this.root.Trim();
		}

		// Token: 0x040025B9 RID: 9657
		private TrieSegment root;

		// Token: 0x040025BA RID: 9658
		private bool hasDescendants;
	}
}
