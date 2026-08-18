using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000014 RID: 20
	internal class ContextQuery : Query
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000031F5 File Offset: 0x000013F5
		public ContextQuery()
		{
			this.count = 0;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003204 File Offset: 0x00001404
		protected ContextQuery(ContextQuery other) : base(other)
		{
			this.contextNode = other.contextNode;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003219 File Offset: 0x00001419
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003222 File Offset: 0x00001422
		public override XPathNavigator Current
		{
			get
			{
				return this.contextNode;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000322A File Offset: 0x0000142A
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current;
			this.count = 0;
			return this;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003240 File Offset: 0x00001440
		public override XPathNavigator Advance()
		{
			if (this.count == 0)
			{
				this.count = 1;
				return this.contextNode;
			}
			return null;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003259 File Offset: 0x00001459
		public override XPathNavigator MatchNode(XPathNavigator current)
		{
			return current;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000325C File Offset: 0x0000145C
		public override XPathNodeIterator Clone()
		{
			return new ContextQuery(this);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003264 File Offset: 0x00001464
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003267 File Offset: 0x00001467
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000326F File Offset: 0x0000146F
		public override int Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003272 File Offset: 0x00001472
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04000078 RID: 120
		protected XPathNavigator contextNode;
	}
}
