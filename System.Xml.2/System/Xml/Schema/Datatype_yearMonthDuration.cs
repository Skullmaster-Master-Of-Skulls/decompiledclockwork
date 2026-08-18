using System;

namespace System.Xml.Schema
{
	// Token: 0x02000210 RID: 528
	internal class Datatype_yearMonthDuration : Datatype_duration
	{
		// Token: 0x060021B5 RID: 8629 RVA: 0x000B6C74 File Offset: 0x000B4E74
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

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x000B6CE8 File Offset: 0x000B4EE8
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.YearMonthDuration;
			}
		}
	}
}
