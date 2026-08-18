using System;

namespace System.Xml.Schema
{
	// Token: 0x0200022E RID: 558
	internal class Datatype_NOTATION : Datatype_anySimpleType
	{
		// Token: 0x06002226 RID: 8742 RVA: 0x000B7382 File Offset: 0x000B5582
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x000B738A File Offset: 0x000B558A
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x000B7391 File Offset: 0x000B5591
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Notation;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x000B7395 File Offset: 0x000B5595
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NOTATION;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000B7398 File Offset: 0x000B5598
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000B739C File Offset: 0x000B559C
		public override Type ValueType
		{
			get
			{
				return Datatype_NOTATION.atomicValueType;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x000B73A3 File Offset: 0x000B55A3
		internal override Type ListValueType
		{
			get
			{
				return Datatype_NOTATION.listValueType;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x000B73AA File Offset: 0x000B55AA
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x000B73B0 File Offset: 0x000B55B0
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

		// Token: 0x0600222F RID: 8751 RVA: 0x000B743C File Offset: 0x000B563C
		internal override void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller)
		{
			for (Datatype_NOTATION datatype_NOTATION = this; datatype_NOTATION != null; datatype_NOTATION = (Datatype_NOTATION)datatype_NOTATION.Base)
			{
				if (datatype_NOTATION.Restriction != null && (datatype_NOTATION.Restriction.Flags & RestrictionFlags.Enumeration) != (RestrictionFlags)0)
				{
					for (int i = 0; i < datatype_NOTATION.Restriction.Enumeration.Count; i++)
					{
						XmlQualifiedName name = (XmlQualifiedName)datatype_NOTATION.Restriction.Enumeration[i];
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

		// Token: 0x04000E82 RID: 3714
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000E83 RID: 3715
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
