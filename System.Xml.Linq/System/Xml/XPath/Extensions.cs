using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace System.Xml.XPath
{
	// Token: 0x0200000A RID: 10
	public static class Extensions
	{
		// Token: 0x06000052 RID: 82 RVA: 0x00003AAC File Offset: 0x00001CAC
		public static XPathNavigator CreateNavigator(this XNode node)
		{
			return node.CreateNavigator(null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public static XPathNavigator CreateNavigator(this XNode node, XmlNameTable nameTable)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node is XDocumentType)
			{
				throw new ArgumentException(Res.GetString("Argument_CreateNavigator", new object[]
				{
					XmlNodeType.DocumentType
				}));
			}
			XText xtext = node as XText;
			if (xtext != null)
			{
				if (xtext.parent is XDocument)
				{
					throw new ArgumentException(Res.GetString("Argument_CreateNavigator", new object[]
					{
						XmlNodeType.Whitespace
					}));
				}
				node = Extensions.CalibrateText(xtext);
			}
			return new XNodeNavigator(node, nameTable);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003B41 File Offset: 0x00001D41
		public static object XPathEvaluate(this XNode node, string expression)
		{
			return node.XPathEvaluate(expression, null);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003B4C File Offset: 0x00001D4C
		public static object XPathEvaluate(this XNode node, string expression, IXmlNamespaceResolver resolver)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return default(XPathEvaluator).Evaluate<object>(node, expression, resolver);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003B78 File Offset: 0x00001D78
		public static XElement XPathSelectElement(this XNode node, string expression)
		{
			return node.XPathSelectElement(expression, null);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003B82 File Offset: 0x00001D82
		public static XElement XPathSelectElement(this XNode node, string expression, IXmlNamespaceResolver resolver)
		{
			return node.XPathSelectElements(expression, resolver).FirstOrDefault<XElement>();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003B91 File Offset: 0x00001D91
		public static IEnumerable<XElement> XPathSelectElements(this XNode node, string expression)
		{
			return node.XPathSelectElements(expression, null);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003B9C File Offset: 0x00001D9C
		public static IEnumerable<XElement> XPathSelectElements(this XNode node, string expression, IXmlNamespaceResolver resolver)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			return (IEnumerable<XElement>)default(XPathEvaluator).Evaluate<XElement>(node, expression, resolver);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private static XText CalibrateText(XText n)
		{
			if (n.parent == null)
			{
				return n;
			}
			XNode xnode = (XNode)n.parent.content;
			XText xtext;
			for (;;)
			{
				IL_1B:
				xnode = xnode.next;
				xtext = (xnode as XText);
				if (xtext != null)
				{
					while (xnode != n)
					{
						xnode = xnode.next;
						if (!(xnode is XText))
						{
							goto IL_1B;
						}
					}
					break;
				}
			}
			return xtext;
		}
	}
}
