using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200015B RID: 347
	internal sealed class ReversePositionQuery : ForwardPositionQuery
	{
		// Token: 0x060012E8 RID: 4840 RVA: 0x00052440 File Offset: 0x00051440
		public ReversePositionQuery(Query input) : base(input)
		{
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00052449 File Offset: 0x00051449
		private ReversePositionQuery(ReversePositionQuery other) : base(other)
		{
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00052452 File Offset: 0x00051452
		public override XPathNodeIterator Clone()
		{
			return new ReversePositionQuery(this);
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0005245A File Offset: 0x0005145A
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00052470 File Offset: 0x00051470
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
