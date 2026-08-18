using System;
using System.Collections;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004CB RID: 1227
	internal class NodeSequenceIterator : XPathNodeIterator
	{
		// Token: 0x06002E9F RID: 11935 RVA: 0x000B4F23 File Offset: 0x000B3123
		internal NodeSequenceIterator(NodeSequence seq)
		{
			this.data = this;
			this.seq = seq;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000B4F39 File Offset: 0x000B3139
		internal NodeSequenceIterator(NodeSequenceIterator iter)
		{
			this.data = iter.data;
			this.index = iter.index;
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000B4F59 File Offset: 0x000B3159
		public override int Count
		{
			get
			{
				return this.data.seq.Count;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000B4F6C File Offset: 0x000B316C
		public override XPathNavigator Current
		{
			get
			{
				if (this.index == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryProcessingException(QueryProcessingError.Unexpected, SR.GetString("QueryContextNotSupportedInSequences")));
				}
				if (this.index > this.data.seq.Count)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("QueryAfterNodes")));
				}
				return this.nav;
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000B4FD4 File Offset: 0x000B31D4
		public override int CurrentPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000B4FDC File Offset: 0x000B31DC
		internal void Clear()
		{
			this.data.seq = null;
			this.nav = null;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000B4FF1 File Offset: 0x000B31F1
		public override XPathNodeIterator Clone()
		{
			return new NodeSequenceIterator(this);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000B4FF9 File Offset: 0x000B31F9
		public override IEnumerator GetEnumerator()
		{
			return new NodeSequenceEnumerator(this);
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000B5004 File Offset: 0x000B3204
		public override bool MoveNext()
		{
			if (this.data.seq == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("QueryIteratorOutOfScope")));
			}
			if (this.index < this.data.seq.Count)
			{
				if (this.nav == null)
				{
					this.nav = (SeekableXPathNavigator)this.data.seq[this.index].GetNavigator().Clone();
				}
				else
				{
					this.nav.CurrentPosition = this.data.seq[this.index].GetNavigatorPosition();
				}
				this.index++;
				return true;
			}
			this.index++;
			this.nav = null;
			return false;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000B50D7 File Offset: 0x000B32D7
		public void Reset()
		{
			this.nav = null;
			this.index = 0;
		}

		// Token: 0x0400254B RID: 9547
		private NodeSequence seq;

		// Token: 0x0400254C RID: 9548
		private NodeSequenceIterator data;

		// Token: 0x0400254D RID: 9549
		private int index;

		// Token: 0x0400254E RID: 9550
		private SeekableXPathNavigator nav;
	}
}
