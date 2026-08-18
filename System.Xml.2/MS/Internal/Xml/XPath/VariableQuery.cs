using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003F RID: 63
	internal sealed class VariableQuery : ExtensionQuery
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00007B10 File Offset: 0x00005D10
		public VariableQuery(string name, string prefix) : base(prefix, name)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007B1A File Offset: 0x00005D1A
		private VariableQuery(VariableQuery other) : base(other)
		{
			this.variable = other.variable;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007B30 File Offset: 0x00005D30
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

		// Token: 0x060001EF RID: 495 RVA: 0x00007B91 File Offset: 0x00005D91
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Xp_NoContext");
			}
			return base.ProcessResult(this.variable.Evaluate(this.xsltContext));
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007BC0 File Offset: 0x00005DC0
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

		// Token: 0x060001F1 RID: 497 RVA: 0x00007C01 File Offset: 0x00005E01
		public override XPathNodeIterator Clone()
		{
			return new VariableQuery(this);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007C0C File Offset: 0x00005E0C
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("name", (this.prefix.Length != 0) ? (this.prefix + ":" + this.name) : this.name);
			w.WriteEndElement();
		}

		// Token: 0x040000D3 RID: 211
		private IXsltContextVariable variable;
	}
}
