using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000121 RID: 289
	internal class ContextQuery : Query
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0004D7B5 File Offset: 0x0004C7B5
		public ContextQuery()
		{
			this.count = 0;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0004D7C4 File Offset: 0x0004C7C4
		protected ContextQuery(ContextQuery other) : base(other)
		{
			this.contextNode = other.contextNode;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0004D7D9 File Offset: 0x0004C7D9
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0004D7E2 File Offset: 0x0004C7E2
		public override XPathNavigator Current
		{
			get
			{
				return this.contextNode;
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0004D7EA File Offset: 0x0004C7EA
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current;
			this.count = 0;
			return this;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0004D800 File Offset: 0x0004C800
		public override XPathNavigator Advance()
		{
			if (this.count == 0)
			{
				this.count = 1;
				return this.contextNode;
			}
			return null;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0004D819 File Offset: 0x0004C819
		public override XPathNavigator MatchNode(XPathNavigator current)
		{
			return current;
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0004D81C File Offset: 0x0004C81C
		public override XPathNodeIterator Clone()
		{
			return new ContextQuery(this);
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x0004D824 File Offset: 0x0004C824
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x0004D827 File Offset: 0x0004C827
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0004D82F File Offset: 0x0004C82F
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004D832 File Offset: 0x0004C832
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04000B14 RID: 2836
		protected XPathNavigator contextNode;
	}
}
