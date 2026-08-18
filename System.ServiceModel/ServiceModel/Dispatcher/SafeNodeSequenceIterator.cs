using System;
using System.Threading;
using System.Xml.XPath;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004CD RID: 1229
	internal class SafeNodeSequenceIterator : NodeSequenceIterator, IDisposable
	{
		// Token: 0x06002EAD RID: 11949 RVA: 0x000B518F File Offset: 0x000B338F
		public SafeNodeSequenceIterator(NodeSequence seq, ProcessingContext context) : base(seq)
		{
			this.context = context;
			this.seq = seq;
			Interlocked.Increment(ref this.seq.refCount);
			this.context.Processor.AddRef();
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000B51C7 File Offset: 0x000B33C7
		public override XPathNodeIterator Clone()
		{
			return new SafeNodeSequenceIterator(this.seq, this.context);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000B51DC File Offset: 0x000B33DC
		public void Dispose()
		{
			if (Interlocked.CompareExchange(ref this.disposed, 1, 0) == 0)
			{
				QueryProcessor processor = this.context.Processor;
				this.context.ReleaseSequence(this.seq);
				this.context.Processor.Matcher.ReleaseProcessor(processor);
			}
		}

		// Token: 0x04002550 RID: 9552
		private ProcessingContext context;

		// Token: 0x04002551 RID: 9553
		private int disposed;

		// Token: 0x04002552 RID: 9554
		private NodeSequence seq;
	}
}
