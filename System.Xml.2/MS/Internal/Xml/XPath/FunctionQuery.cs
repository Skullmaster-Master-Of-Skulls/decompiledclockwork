using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001B RID: 27
	internal sealed class FunctionQuery : ExtensionQuery
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000393A File Offset: 0x00001B3A
		public FunctionQuery(string prefix, string name, List<Query> args) : base(prefix, name)
		{
			this.args = args;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000394C File Offset: 0x00001B4C
		private FunctionQuery(FunctionQuery other) : base(other)
		{
			this.function = other.function;
			Query[] array = new Query[other.args.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Query.Clone(other.args[i]);
			}
			this.args = array;
			this.args = array;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000039B0 File Offset: 0x00001BB0
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Xp_NoContext");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				foreach (Query query in this.args)
				{
					query.SetXsltContext(context);
				}
				XPathResultType[] array = new XPathResultType[this.args.Count];
				for (int i = 0; i < this.args.Count; i++)
				{
					array[i] = this.args[i].StaticType;
				}
				this.function = this.xsltContext.ResolveFunction(this.prefix, this.name, array);
				if (this.function == null)
				{
					throw XPathException.Create("Xp_UndefFunc", base.QName);
				}
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003A90 File Offset: 0x00001C90
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Xp_NoContext");
			}
			object[] array = new object[this.args.Count];
			for (int i = 0; i < this.args.Count; i++)
			{
				array[i] = this.args[i].Evaluate(nodeIterator);
				if (array[i] is XPathNodeIterator)
				{
					array[i] = new XPathSelectionIterator(nodeIterator.Current, this.args[i]);
				}
			}
			object result;
			try
			{
				result = base.ProcessResult(this.function.Invoke(this.xsltContext, array, nodeIterator.Current));
			}
			catch (Exception innerException)
			{
				throw XPathException.Create("Xp_FunctionFailed", base.QName, innerException);
			}
			return result;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003B58 File Offset: 0x00001D58
		public override XPathNavigator MatchNode(XPathNavigator navigator)
		{
			if (this.name != "key" && this.prefix.Length != 0)
			{
				throw XPathException.Create("Xp_InvalidPattern");
			}
			this.Evaluate(new XPathSingletonIterator(navigator, true));
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.Advance()) != null)
			{
				if (xpathNavigator.IsSamePosition(navigator))
				{
					return xpathNavigator;
				}
			}
			return xpathNavigator;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public override XPathResultType StaticType
		{
			get
			{
				XPathResultType xpathResultType = (this.function != null) ? this.function.ReturnType : XPathResultType.Any;
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003BE3 File Offset: 0x00001DE3
		public override XPathNodeIterator Clone()
		{
			return new FunctionQuery(this);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003BEC File Offset: 0x00001DEC
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", (this.prefix.Length != 0) ? (this.prefix + ":" + this.name) : this.name);
			foreach (Query query in this.args)
			{
				query.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x04000081 RID: 129
		private IList<Query> args;

		// Token: 0x04000082 RID: 130
		private IXsltContextFunction function;
	}
}
