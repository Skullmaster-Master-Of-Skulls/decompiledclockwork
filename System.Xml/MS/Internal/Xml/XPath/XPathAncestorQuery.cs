using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000165 RID: 357
	internal sealed class XPathAncestorQuery : CacheAxisQuery
	{
		// Token: 0x06001338 RID: 4920 RVA: 0x000533EB File Offset: 0x000523EB
		public XPathAncestorQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest, bool matchSelf) : base(qyInput, name, prefix, typeTest)
		{
			this.matchSelf = matchSelf;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00053400 File Offset: 0x00052400
		private XPathAncestorQuery(XPathAncestorQuery other) : base(other)
		{
			this.matchSelf = other.matchSelf;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00053418 File Offset: 0x00052418
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator = null;
			XPathNavigator xpathNavigator2;
			while ((xpathNavigator2 = this.qyInput.Advance()) != null)
			{
				if (!this.matchSelf || !this.matches(xpathNavigator2) || base.Insert(this.outputBuffer, xpathNavigator2))
				{
					if (xpathNavigator == null || !xpathNavigator.MoveTo(xpathNavigator2))
					{
						xpathNavigator = xpathNavigator2.Clone();
					}
					while (xpathNavigator.MoveToParent() && (!this.matches(xpathNavigator) || base.Insert(this.outputBuffer, xpathNavigator)))
					{
					}
				}
			}
			return this;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00053496 File Offset: 0x00052496
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorQuery(this);
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0005349E File Offset: 0x0005249E
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x000534B4 File Offset: 0x000524B4
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000534C0 File Offset: 0x000524C0
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			if (this.matchSelf)
			{
				w.WriteAttributeString("self", "yes");
			}
			if (base.NameTest)
			{
				w.WriteAttributeString("name", (base.Prefix.Length != 0) ? (base.Prefix + ':' + base.Name) : base.Name);
			}
			if (base.TypeTest != XPathNodeType.Element)
			{
				w.WriteAttributeString("nodeType", base.TypeTest.ToString());
			}
			this.qyInput.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000BEC RID: 3052
		private bool matchSelf;
	}
}
