using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200012F RID: 303
	internal abstract class CacheOutputQuery : Query
	{
		// Token: 0x060011AD RID: 4525 RVA: 0x0004E568 File Offset: 0x0004D568
		public CacheOutputQuery(Query input)
		{
			this.input = input;
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004E589 File Offset: 0x0004D589
		protected CacheOutputQuery(CacheOutputQuery other) : base(other)
		{
			this.input = Query.Clone(other.input);
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0004E5C0 File Offset: 0x0004D5C0
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0004E5C9 File Offset: 0x0004D5C9
		public override void SetXsltContext(XsltContext context)
		{
			this.input.SetXsltContext(context);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0004E5D7 File Offset: 0x0004D5D7
		public override object Evaluate(XPathNodeIterator context)
		{
			this.outputBuffer.Clear();
			this.count = 0;
			return this.input.Evaluate(context);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004E5F8 File Offset: 0x0004D5F8
		public override XPathNavigator Advance()
		{
			if (this.count < this.outputBuffer.Count)
			{
				return this.outputBuffer[this.count++];
			}
			return null;
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0004E636 File Offset: 0x0004D636
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

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0004E655 File Offset: 0x0004D655
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0004E658 File Offset: 0x0004D658
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0004E660 File Offset: 0x0004D660
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004E66D File Offset: 0x0004D66D
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0004E671 File Offset: 0x0004D671
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			this.input.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000B49 RID: 2889
		internal Query input;

		// Token: 0x04000B4A RID: 2890
		protected List<XPathNavigator> outputBuffer;
	}
}
