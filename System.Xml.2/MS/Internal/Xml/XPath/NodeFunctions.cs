using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002B RID: 43
	internal sealed class NodeFunctions : ValueQuery
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005622 File Offset: 0x00003822
		public NodeFunctions(Function.FunctionType funcType, Query arg)
		{
			this.funcType = funcType;
			this.arg = arg;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005638 File Offset: 0x00003838
		public override void SetXsltContext(XsltContext context)
		{
			this.xsltContext = (context.Whitespace ? context : null);
			if (this.arg != null)
			{
				this.arg.SetXsltContext(context);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005660 File Offset: 0x00003860
		private XPathNavigator EvaluateArg(XPathNodeIterator context)
		{
			if (this.arg == null)
			{
				return context.Current;
			}
			this.arg.Evaluate(context);
			return this.arg.Advance();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000568C File Offset: 0x0000388C
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000577D File Offset: 0x0000397D
		public override XPathResultType StaticType
		{
			get
			{
				return Function.ReturnTypes[(int)this.funcType];
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000578C File Offset: 0x0000398C
		public override XPathNodeIterator Clone()
		{
			return new NodeFunctions(this.funcType, Query.Clone(this.arg))
			{
				xsltContext = this.xsltContext
			};
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000057C0 File Offset: 0x000039C0
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

		// Token: 0x040000A4 RID: 164
		private Query arg;

		// Token: 0x040000A5 RID: 165
		private Function.FunctionType funcType;

		// Token: 0x040000A6 RID: 166
		private XsltContext xsltContext;
	}
}
