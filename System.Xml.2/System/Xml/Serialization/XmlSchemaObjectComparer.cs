using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200016B RID: 363
	internal class XmlSchemaObjectComparer : IComparer
	{
		// Token: 0x06001849 RID: 6217 RVA: 0x000699D5 File Offset: 0x00067BD5
		public int Compare(object o1, object o2)
		{
			return this.comparer.Compare(XmlSchemaObjectComparer.NameOf((XmlSchemaObject)o1), XmlSchemaObjectComparer.NameOf((XmlSchemaObject)o2));
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x000699F8 File Offset: 0x00067BF8
		internal static XmlQualifiedName NameOf(XmlSchemaObject o)
		{
			if (o is XmlSchemaAttribute)
			{
				return ((XmlSchemaAttribute)o).QualifiedName;
			}
			if (o is XmlSchemaAttributeGroup)
			{
				return ((XmlSchemaAttributeGroup)o).QualifiedName;
			}
			if (o is XmlSchemaComplexType)
			{
				return ((XmlSchemaComplexType)o).QualifiedName;
			}
			if (o is XmlSchemaSimpleType)
			{
				return ((XmlSchemaSimpleType)o).QualifiedName;
			}
			if (o is XmlSchemaElement)
			{
				return ((XmlSchemaElement)o).QualifiedName;
			}
			if (o is XmlSchemaGroup)
			{
				return ((XmlSchemaGroup)o).QualifiedName;
			}
			if (o is XmlSchemaGroupRef)
			{
				return ((XmlSchemaGroupRef)o).RefName;
			}
			if (o is XmlSchemaNotation)
			{
				return ((XmlSchemaNotation)o).QualifiedName;
			}
			if (o is XmlSchemaSequence)
			{
				XmlSchemaSequence xmlSchemaSequence = (XmlSchemaSequence)o;
				if (xmlSchemaSequence.Items.Count == 0)
				{
					return new XmlQualifiedName(".sequence", XmlSchemaObjectComparer.Namespace(o));
				}
				return XmlSchemaObjectComparer.NameOf(xmlSchemaSequence.Items[0]);
			}
			else if (o is XmlSchemaAll)
			{
				XmlSchemaAll xmlSchemaAll = (XmlSchemaAll)o;
				if (xmlSchemaAll.Items.Count == 0)
				{
					return new XmlQualifiedName(".all", XmlSchemaObjectComparer.Namespace(o));
				}
				return XmlSchemaObjectComparer.NameOf(xmlSchemaAll.Items);
			}
			else if (o is XmlSchemaChoice)
			{
				XmlSchemaChoice xmlSchemaChoice = (XmlSchemaChoice)o;
				if (xmlSchemaChoice.Items.Count == 0)
				{
					return new XmlQualifiedName(".choice", XmlSchemaObjectComparer.Namespace(o));
				}
				return XmlSchemaObjectComparer.NameOf(xmlSchemaChoice.Items);
			}
			else
			{
				if (o is XmlSchemaAny)
				{
					return new XmlQualifiedName("*", SchemaObjectWriter.ToString(((XmlSchemaAny)o).NamespaceList));
				}
				if (o is XmlSchemaIdentityConstraint)
				{
					return ((XmlSchemaIdentityConstraint)o).QualifiedName;
				}
				return new XmlQualifiedName("?", XmlSchemaObjectComparer.Namespace(o));
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00069BA0 File Offset: 0x00067DA0
		internal static XmlQualifiedName NameOf(XmlSchemaObjectCollection items)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < items.Count; i++)
			{
				arrayList.Add(XmlSchemaObjectComparer.NameOf(items[i]));
			}
			arrayList.Sort(new QNameComparer());
			return (XmlQualifiedName)arrayList[0];
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00069BEE File Offset: 0x00067DEE
		internal static string Namespace(XmlSchemaObject o)
		{
			while (o != null && !(o is XmlSchema))
			{
				o = o.Parent;
			}
			if (o != null)
			{
				return ((XmlSchema)o).TargetNamespace;
			}
			return "";
		}

		// Token: 0x04000B3A RID: 2874
		private QNameComparer comparer = new QNameComparer();
	}
}
