using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000041 RID: 65
	internal sealed class XPathAncestorQuery : CacheAxisQuery
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00007CEB File Offset: 0x00005EEB
		public XPathAncestorQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest, bool matchSelf) : base(qyInput, name, prefix, typeTest)
		{
			this.matchSelf = matchSelf;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00007D00 File Offset: 0x00005F00
		private XPathAncestorQuery(XPathAncestorQuery other) : base(other)
		{
			this.matchSelf = other.matchSelf;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00007D18 File Offset: 0x00005F18
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

		// Token: 0x060001FB RID: 507 RVA: 0x00007D96 File Offset: 0x00005F96
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorQuery(this);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00007D9E File Offset: 0x00005F9E
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00007DB4 File Offset: 0x00005FB4
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			if (this.matchSelf)
			{
				w.WriteAttributeString("self", "yes");
			}
			if (base.NameTest)
			{
				w.WriteAttributeString("name", (base.Prefix.Length != 0) ? (base.Prefix + ":" + base.Name) : base.Name);
			}
			if (base.TypeTest != XPathNodeType.Element)
			{
				w.WriteAttributeString("nodeType", base.TypeTest.ToString());
			}
			this.qyInput.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x040000D4 RID: 212
		private bool matchSelf;
	}
}
