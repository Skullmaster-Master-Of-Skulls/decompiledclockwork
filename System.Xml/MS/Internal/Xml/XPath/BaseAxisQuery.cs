using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000125 RID: 293
	internal abstract class BaseAxisQuery : Query
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x0004D88D File Offset: 0x0004C88D
		protected BaseAxisQuery(Query qyInput)
		{
			this.name = string.Empty;
			this.prefix = string.Empty;
			this.nsUri = string.Empty;
			this.qyInput = qyInput;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0004D8C0 File Offset: 0x0004C8C0
		protected BaseAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
		{
			this.qyInput = qyInput;
			this.name = name;
			this.prefix = prefix;
			this.typeTest = typeTest;
			this.nameTest = (prefix.Length != 0 || name.Length != 0);
			this.nsUri = string.Empty;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0004D918 File Offset: 0x0004C918
		protected BaseAxisQuery(BaseAxisQuery other) : base(other)
		{
			this.qyInput = Query.Clone(other.qyInput);
			this.name = other.name;
			this.prefix = other.prefix;
			this.nsUri = other.nsUri;
			this.typeTest = other.typeTest;
			this.nameTest = other.nameTest;
			this.position = other.position;
			this.currentNode = other.currentNode;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0004D991 File Offset: 0x0004C991
		public override void Reset()
		{
			this.position = 0;
			this.currentNode = null;
			this.qyInput.Reset();
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0004D9AC File Offset: 0x0004C9AC
		public override void SetXsltContext(XsltContext context)
		{
			this.nsUri = context.LookupNamespace(this.prefix);
			this.qyInput.SetXsltContext(context);
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004D9CC File Offset: 0x0004C9CC
		protected string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0004D9D4 File Offset: 0x0004C9D4
		protected string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0004D9DC File Offset: 0x0004C9DC
		protected string Namespace
		{
			get
			{
				return this.nsUri;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0004D9E4 File Offset: 0x0004C9E4
		protected bool NameTest
		{
			get
			{
				return this.nameTest;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0004D9EC File Offset: 0x0004C9EC
		protected XPathNodeType TypeTest
		{
			get
			{
				return this.typeTest;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0004D9F4 File Offset: 0x0004C9F4
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0004D9FC File Offset: 0x0004C9FC
		public override XPathNavigator Current
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0004DA04 File Offset: 0x0004CA04
		public virtual bool matches(XPathNavigator e)
		{
			if (this.TypeTest == e.NodeType || this.TypeTest == XPathNodeType.All || (this.TypeTest == XPathNodeType.Text && (e.NodeType == XPathNodeType.Whitespace || e.NodeType == XPathNodeType.SignificantWhitespace)))
			{
				if (!this.NameTest)
				{
					return true;
				}
				if ((this.name.Equals(e.LocalName) || this.name.Length == 0) && this.nsUri.Equals(e.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0004DA84 File Offset: 0x0004CA84
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			base.ResetCount();
			this.Reset();
			this.qyInput.Evaluate(nodeIterator);
			return this;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0004DAA0 File Offset: 0x0004CAA0
		public override double XsltDefaultPriority
		{
			get
			{
				if (this.qyInput.GetType() != typeof(ContextQuery))
				{
					return 0.5;
				}
				if (this.name.Length != 0)
				{
					return 0.0;
				}
				if (this.prefix.Length != 0)
				{
					return -0.25;
				}
				return -0.5;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0004DB05 File Offset: 0x0004CB05
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0004DB08 File Offset: 0x0004CB08
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			if (this.NameTest)
			{
				w.WriteAttributeString("name", (this.Prefix.Length != 0) ? (this.Prefix + ':' + this.Name) : this.Name);
			}
			if (this.TypeTest != XPathNodeType.Element)
			{
				w.WriteAttributeString("nodeType", this.TypeTest.ToString());
			}
			this.qyInput.PrintQuery(w);
			w.WriteEndElement();
		}

		// Token: 0x04000B1F RID: 2847
		internal Query qyInput;

		// Token: 0x04000B20 RID: 2848
		private bool nameTest;

		// Token: 0x04000B21 RID: 2849
		private string name;

		// Token: 0x04000B22 RID: 2850
		private string prefix;

		// Token: 0x04000B23 RID: 2851
		private string nsUri;

		// Token: 0x04000B24 RID: 2852
		private XPathNodeType typeTest;

		// Token: 0x04000B25 RID: 2853
		protected XPathNavigator currentNode;

		// Token: 0x04000B26 RID: 2854
		protected int position;
	}
}
