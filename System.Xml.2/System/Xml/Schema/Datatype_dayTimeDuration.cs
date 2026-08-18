using System;

namespace System.Xml.Schema
{
	// Token: 0x02000211 RID: 529
	internal class Datatype_dayTimeDuration : Datatype_duration
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x000B6CF4 File Offset: 0x000B4EF4
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
				ex = XsdDuration.TryParse(s, XsdDuration.DurationType.DayTimeDuration, out xsdDuration);
				if (ex == null)
				{
					TimeSpan timeSpan;
					ex = xsdDuration.TryToTimeSpan(XsdDuration.DurationType.DayTimeDuration, out timeSpan);
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

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000B6D68 File Offset: 0x000B4F68
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.DayTimeDuration;
			}
		}
	}
}
