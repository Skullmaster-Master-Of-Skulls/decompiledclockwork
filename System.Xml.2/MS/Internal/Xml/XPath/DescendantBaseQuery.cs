using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000015 RID: 21
	internal abstract class DescendantBaseQuery : BaseAxisQuery
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00003276 File Offset: 0x00001476
		public DescendantBaseQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis) : base(qyParent, Name, Prefix, Type)
		{
			this.matchSelf = matchSelf;
			this.abbrAxis = abbrAxis;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003293 File Offset: 0x00001493
		public DescendantBaseQuery(DescendantBaseQuery other) : base(other)
		{
			this.matchSelf = other.matchSelf;
			this.abbrAxis = other.abbrAxis;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000032B4 File Offset: 0x000014B4
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

		// Token: 0x0600008A RID: 138 RVA: 0x00003320 File Offset: 0x00001520
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

		// Token: 0x04000079 RID: 121
		protected bool matchSelf;

		// Token: 0x0400007A RID: 122
		protected bool abbrAxis;
	}
}
