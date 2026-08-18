using System;
using System.Collections;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002E5 RID: 741
	internal class XmlSchemaObjectComparer : IComparer
	{
		// Token: 0x06002292 RID: 8850 RVA: 0x000A1611 File Offset: 0x000A0611
		public int Compare(object o1, object o2)
		{
			return this.comparer.Compare(XmlSchemaObjectComparer.NameOf((XmlSchemaObject)o1), XmlSchemaObjectComparer.NameOf((XmlSchemaObject)o2));
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000A1634 File Offset: 0x000A0634
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

		// Token: 0x06002294 RID: 8852 RVA: 0x000A17DC File Offset: 0x000A07DC
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

		// Token: 0x06002295 RID: 8853 RVA: 0x000A182A File Offset: 0x000A082A
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

		// Token: 0x040014CE RID: 5326
		private QNameComparer comparer = new QNameComparer();
	}
}
