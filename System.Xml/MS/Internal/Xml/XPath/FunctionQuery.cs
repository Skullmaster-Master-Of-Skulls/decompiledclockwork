using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200013B RID: 315
	internal sealed class FunctionQuery : ExtensionQuery
	{
		// Token: 0x06001205 RID: 4613 RVA: 0x0004F116 File Offset: 0x0004E116
		public FunctionQuery(string prefix, string name, List<Query> args) : base(prefix, name)
		{
			this.args = args;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004F128 File Offset: 0x0004E128
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

		// Token: 0x06001207 RID: 4615 RVA: 0x0004F18C File Offset: 0x0004E18C
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

		// Token: 0x06001208 RID: 4616 RVA: 0x0004F26C File Offset: 0x0004E26C
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

		// Token: 0x06001209 RID: 4617 RVA: 0x0004F334 File Offset: 0x0004E334
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

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004F394 File Offset: 0x0004E394
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

		// Token: 0x0600120B RID: 4619 RVA: 0x0004F3BF File Offset: 0x0004E3BF
		public override XPathNodeIterator Clone()
		{
			return new FunctionQuery(this);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0004F3C8 File Offset: 0x0004E3C8
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", (this.prefix.Length != 0) ? (this.prefix + ':' + this.name) : this.name);
			foreach (Query query in this.args)
			{
				query.PrintQuery(w);
			}
			w.WriteEndElement();
		}

		// Token: 0x04000B5B RID: 2907
		private IList<Query> args;

		// Token: 0x04000B5C RID: 2908
		private IXsltContextFunction function;
	}
}
