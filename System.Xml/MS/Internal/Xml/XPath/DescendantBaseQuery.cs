using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000135 RID: 309
	internal abstract class DescendantBaseQuery : BaseAxisQuery
	{
		// Token: 0x060011DC RID: 4572 RVA: 0x0004EA5E File Offset: 0x0004DA5E
		public DescendantBaseQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis) : base(qyParent, Name, Prefix, Type)
		{
			this.matchSelf = matchSelf;
			this.abbrAxis = abbrAxis;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0004EA7B File Offset: 0x0004DA7B
		public DescendantBaseQuery(DescendantBaseQuery other) : base(other)
		{
			this.matchSelf = other.matchSelf;
			this.abbrAxis = other.abbrAxis;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0004EA9C File Offset: 0x0004DA9C
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null)
			{
				if (!this.abbrAxis)
				{
					throw XPathException.Create("Xp_InvalidPattern");
				}
				if (this.matches(context))
				{
					XPathNavigator result;
					if (this.matchSelf && (result = this.qyInput.MatchNode(context)) != null)
					{
						return result;
					}
					XPathNavigator xpathNavigator = context.Clone();
					while (xpathNavigator.MoveToParent())
					{
						if ((result = this.qyInput.MatchNode(xpathNavigator)) != null)
						{
							return result;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0004EB08 File Offset: 0x0004DB08
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

		// Token: 0x04000B53 RID: 2899
		protected bool matchSelf;

		// Token: 0x04000B54 RID: 2900
		protected bool abbrAxis;
	}
}
