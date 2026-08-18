using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000260 RID: 608
	internal class SchemaNamespaceManager : XmlNamespaceManager
	{
		// Token: 0x06002486 RID: 9350 RVA: 0x000C82C1 File Offset: 0x000C64C1
		public SchemaNamespaceManager(XmlSchemaObject node)
		{
			this.node = node;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000C82D0 File Offset: 0x000C64D0
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

		// Token: 0x06002488 RID: 9352 RVA: 0x000C833C File Offset: 0x000C653C
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

		// Token: 0x04000FD7 RID: 4055
		private XmlSchemaObject node;
	}
}
