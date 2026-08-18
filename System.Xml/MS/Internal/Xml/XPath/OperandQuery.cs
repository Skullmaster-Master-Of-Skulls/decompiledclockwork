using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000151 RID: 337
	internal sealed class OperandQuery : ValueQuery
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x000512DE File Offset: 0x000502DE
		public OperandQuery(object val)
		{
			this.val = val;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000512ED File Offset: 0x000502ED
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.val;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x000512F5 File Offset: 0x000502F5
		public override XPathResultType StaticType
		{
			get
			{
				return base.GetXPathType(this.val);
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00051303 File Offset: 0x00050303
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00051306 File Offset: 0x00050306
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("value", Convert.ToString(this.val, CultureInfo.InvariantCulture));
			w.WriteEndElement();
		}

		// Token: 0x04000BA7 RID: 2983
		internal object val;
	}
}
