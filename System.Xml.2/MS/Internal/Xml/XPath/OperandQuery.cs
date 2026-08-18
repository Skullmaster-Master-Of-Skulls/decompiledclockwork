using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002F RID: 47
	internal sealed class OperandQuery : ValueQuery
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00005C77 File Offset: 0x00003E77
		public OperandQuery(object val)
		{
			this.val = val;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005C86 File Offset: 0x00003E86
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.val;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00005C8E File Offset: 0x00003E8E
		public override XPathResultType StaticType
		{
			get
			{
				return base.GetXPathType(this.val);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005C9C File Offset: 0x00003E9C
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005C9F File Offset: 0x00003E9F
		public override void PrintQuery(XmlWriter w)
		{
			w.WriteStartElement(base.GetType().Name);
			w.WriteAttributeString("value", Convert.ToString(this.val, CultureInfo.InvariantCulture));
			w.WriteEndElement();
		}

		// Token: 0x040000AE RID: 174
		internal object val;
	}
}
