using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D8 RID: 472
	internal class Datatype_NOTATION : Datatype_anySimpleType
	{
		// Token: 0x060016FC RID: 5884 RVA: 0x00063A0A File Offset: 0x00062A0A
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00063A12 File Offset: 0x00062A12
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00063A19 File Offset: 0x00062A19
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Notation;
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00063A1D File Offset: 0x00062A1D
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NOTATION;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00063A20 File Offset: 0x00062A20
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00063A24 File Offset: 0x00062A24
		public override Type ValueType
		{
			get
			{
				return Datatype_NOTATION.atomicValueType;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00063A2B File Offset: 0x00062A2B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_NOTATION.listValueType;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00063A32 File Offset: 0x00062A32
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00063A38 File Offset: 0x00062A38
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("Sch_EmptyAttributeValue", string.Empty);
			}
			Exception ex = DatatypeImplementation.qnameFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XmlQualifiedName xmlQualifiedName = null;
				try
				{
					string text;
					xmlQualifiedName = XmlQualifiedName.Parse(s, nsmgr, out text);
				}
				catch (ArgumentException result)
				{
					return result;
				}
				catch (XmlException result2)
				{
					return result2;
				}
				ex = DatatypeImplementation.qnameFacetsChecker.CheckValueFacets(xmlQualifiedName, this);
				if (ex == null)
				{
					typedValue = xmlQualifiedName;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00063AC4 File Offset: 0x00062AC4
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
			for (Datatype_NOTATION datatype_NOTATION = this; datatype_NOTATION != null; datatype_NOTATION = (Datatype_NOTATION)datatype_NOTATION.Base)
			{
				if (datatype_NOTATION.Restriction != null && (datatype_NOTATION.Restriction.Flags & RestrictionFlags.Enumeration) != (RestrictionFlags)0)
				{
					foreach (object obj in datatype_NOTATION.Restriction.Enumeration)
					{
						XmlQualifiedName name = (XmlQualifiedName)obj;
						if (!notations.Contains(name))
						{
							throw new XmlSchemaException("Sch_NotationRequired", caller);
						}
					}
					return;
				}
			}
			throw new XmlSchemaException("Sch_NotationRequired", caller);
		}

		// Token: 0x04000D90 RID: 3472
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000D91 RID: 3473
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
