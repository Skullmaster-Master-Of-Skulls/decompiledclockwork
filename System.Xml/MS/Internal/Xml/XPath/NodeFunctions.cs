using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200014D RID: 333
	internal sealed class NodeFunctions : ValueQuery
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x00050C7A File Offset: 0x0004FC7A
		public NodeFunctions(Function.FunctionType funcType, Query arg)
		{
			this.funcType = funcType;
			this.arg = arg;
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00050C90 File Offset: 0x0004FC90
		public override void SetXsltContext(XsltContext context)
		{
			this.xsltContext = (context.Whitespace ? context : null);
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00050CB8 File Offset: 0x0004FCB8
		private XPathNavigator EvaluateArg(XPathNodeIterator context)
		{
			if (this.arg == null)
			{
				return context.Current;
			}
			this.arg.Evaluate(context);
			return this.arg.Advance();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00050CE4 File Offset: 0x0004FCE4
		public override object Evaluate(XPathNodeIterator context)
		{
			switch (this.funcType)
			{
			case Function.FunctionType.FuncLast:
				return (double)context.Count;
			case Function.FunctionType.FuncPosition:
				return (double)context.CurrentPosition;
			case Function.FunctionType.FuncCount:
			{
				this.arg.Evaluate(context);
				int num = 0;
				if (this.xsltContext != null)
				{
					XPathNavigator xpathNavigator;
					while ((xpathNavigator = this.arg.Advance()) != null)
					{
						if (xpathNavigator.NodeType != XPathNodeType.Whitespace || this.xsltContext.PreserveWhitespace(xpathNavigator))
						{
							num++;
						}
					}
				}
				else
				{
					while (this.arg.Advance() != null)
					{
						num++;
					}
				}
				return (double)num;
			}
			case Function.FunctionType.FuncLocalName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.LocalName;
				}
				break;
			}
			case Function.FunctionType.FuncNameSpaceUri:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.NamespaceURI;
				}
				break;
			}
			case Function.FunctionType.FuncName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.Name;
				}
				break;
			}
			}
			return string.Empty;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00050DD5 File Offset: 0x0004FDD5
		public override XPathResultType StaticType
		{
			get
			{
				return Function.ReturnTypes[(int)this.funcType];
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00050DE4 File Offset: 0x0004FDE4
		public override XPathNodeIterator Clone()
		{
			return new NodeFunctions(this.funcType, Query.Clone(this.arg))
			{
				xsltContext = this.xsltContext
			};
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00050E18 File Offset: 0x0004FE18
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", this.funcType.ToString());
			if (this.arg != null)
			{
				this.arg.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x04000B9D RID: 2973
		private Query arg;

		// Token: 0x04000B9E RID: 2974
		private Function.FunctionType funcType;

		// Token: 0x04000B9F RID: 2975
		private XsltContext xsltContext;
	}
}
