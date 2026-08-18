using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200012C RID: 300
	internal abstract class CacheAxisQuery : BaseAxisQuery
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x0004E0FF File Offset: 0x0004D0FF
		public CacheAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0004E11E File Offset: 0x0004D11E
		protected CacheAxisQuery(CacheAxisQuery other) : base(other)
		{
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004E144 File Offset: 0x0004D144
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0004E14D File Offset: 0x0004D14D
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			this.outputBuffer.Clear();
			return this;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0004E164 File Offset: 0x0004D164
		public override XPathNavigator Advance()
		{
			if (this.count < this.outputBuffer.Count)
			{
				return this.outputBuffer[this.count++];
			}
			return null;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x0004E1A2 File Offset: 0x0004D1A2
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

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0004E1C1 File Offset: 0x0004D1C1
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0004E1C9 File Offset: 0x0004D1C9
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600119F RID: 4511 RVA: 0x0004E1D6 File Offset: 0x0004D1D6
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04000B43 RID: 2883
		protected List<XPathNavigator> outputBuffer;
	}
}
