using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200000D RID: 13
	internal abstract class CacheAxisQuery : BaseAxisQuery
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00002984 File Offset: 0x00000B84
		public CacheAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029A3 File Offset: 0x00000BA3
		protected CacheAxisQuery(CacheAxisQuery other) : base(other)
		{
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029C9 File Offset: 0x00000BC9
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029D2 File Offset: 0x00000BD2
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			this.outputBuffer.Clear();
			return this;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029E8 File Offset: 0x00000BE8
		public override XPathNavigator Advance()
		{
			if (this.count < this.outputBuffer.Count)
			{
				List<XPathNavigator> list = this.outputBuffer;
				int count = this.count;
				this.count = count + 1;
				return list[count];
			}
			return null;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002A26 File Offset: 0x00000C26
		public override XPathNavigator Current
		{
			get
			{
				if (this.count == 0)
				{
					return null;
				}
				return this.outputBuffer[this.count - 1];
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A45 File Offset: 0x00000C45
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002A4D File Offset: 0x00000C4D
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A5A File Offset: 0x00000C5A
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04000069 RID: 105
		protected List<XPathNavigator> outputBuffer;
	}
}
