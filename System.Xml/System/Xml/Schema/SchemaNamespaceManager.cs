using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000217 RID: 535
	internal class SchemaNamespaceManager : XmlNamespaceManager
	{
		// Token: 0x060019B4 RID: 6580 RVA: 0x0007BAE5 File Offset: 0x0007AAE5
		public SchemaNamespaceManager(XmlSchemaObject node)
		{
			this.node = node;
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0007BAF4 File Offset: 0x0007AAF4
		public override string LookupNamespace(string prefix)
		{
			if (prefix == "xml")
			{
				return "http://www.w3.org/XML/1998/namespace";
			}
			for (XmlSchemaObject parent = this.node; parent != null; parent = parent.Parent)
			{
				Hashtable namespaces = parent.Namespaces.Namespaces;
				if (namespaces != null && namespaces.Count > 0)
				{
					object obj = namespaces[prefix];
					if (obj != null)
					{
						return (string)obj;
					}
				}
			}
			if (prefix.Length != 0)
			{
				return null;
			}
			return string.Empty;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0007BB60 File Offset: 0x0007AB60
		public override string LookupPrefix(string ns)
		{
			if (ns == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			for (XmlSchemaObject parent = this.node; parent != null; parent = parent.Parent)
			{
				Hashtable namespaces = parent.Namespaces.Namespaces;
				if (namespaces != null && namespaces.Count > 0)
				{
					foreach (object obj in namespaces)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Value.Equals(ns))
						{
							return (string)dictionaryEntry.Key;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04001004 RID: 4100
		private XmlSchemaObject node;
	}
}
