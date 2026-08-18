using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000162 RID: 354
	internal sealed class VariableQuery : ExtensionQuery
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x0005307D File Offset: 0x0005207D
		public VariableQuery(string name, string prefix) : base(prefix, name)
		{
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00053087 File Offset: 0x00052087
		private VariableQuery(VariableQuery other) : base(other)
		{
			this.variable = other.variable;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0005309C File Offset: 0x0005209C
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Xp_NoContext");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				this.variable = this.xsltContext.ResolveVariable(this.prefix, this.name);
				if (this.variable == null)
				{
					throw XPathException.Create("Xp_UndefVar", base.QName);
				}
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000530FD File Offset: 0x000520FD
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Xp_NoContext");
			}
			return base.ProcessResult(this.variable.Evaluate(this.xsltContext));
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x0005312C File Offset: 0x0005212C
		public override XPathResultType StaticType
		{
			get
			{
				if (this.variable != null)
				{
					return base.GetXPathType(this.Evaluate(null));
				}
				XPathResultType xpathResultType = (this.variable != null) ? this.variable.VariableType : XPathResultType.Any;
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0005316D File Offset: 0x0005216D
		public override XPathNodeIterator Clone()
		{
			return new VariableQuery(this);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00053178 File Offset: 0x00052178
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", (this.prefix.Length != 0) ? (this.prefix + ':' + this.name) : this.name);
			w.WriteEndElement();
		}

		// Token: 0x04000BE4 RID: 3044
		private IXsltContextVariable variable;
	}
}
