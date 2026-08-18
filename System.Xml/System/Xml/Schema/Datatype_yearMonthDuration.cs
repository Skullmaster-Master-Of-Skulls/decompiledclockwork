using System;

namespace System.Xml.Schema
{
	// Token: 0x020001BA RID: 442
	internal class Datatype_yearMonthDuration : Datatype_duration
	{
		// Token: 0x0600168B RID: 5771 RVA: 0x00063304 File Offset: 0x00062304
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("Sch_EmptyAttributeValue", string.Empty);
			}
			Exception ex = DatatypeImplementation.durationFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XsdDuration xsdDuration;
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.YearMonthDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.YearMonthDuration, out timeSpan);
					if (ex == null)
					{
						ex = DatatypeImplementation.durationFacetsChecker.CheckValueFacets(timeSpan, this);
						if (ex == null)
						{
							typedValue = timeSpan;
							return null;
						}
					}
				}
			}
			return ex;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00063378 File Offset: 0x00062378
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.YearMonthDuration;
			}
		}
	}
}
