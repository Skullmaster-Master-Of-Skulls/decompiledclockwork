using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000037 RID: 55
	internal sealed class ReversePositionQuery : ForwardPositionQuery
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00006E8F File Offset: 0x0000508F
		public ReversePositionQuery(Query input) : base(input)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006E98 File Offset: 0x00005098
		private ReversePositionQuery(ReversePositionQuery other) : base(other)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006EA1 File Offset: 0x000050A1
		public override XPathNodeIterator Clone()
		{
			return new ReversePositionQuery(this);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00006EA9 File Offset: 0x000050A9
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00006EBF File Offset: 0x000050BF
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
