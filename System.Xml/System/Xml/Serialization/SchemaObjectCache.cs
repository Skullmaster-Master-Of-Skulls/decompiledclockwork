using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002BA RID: 698
	internal class SchemaObjectCache
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0009E138 File Offset: 0x0009D138
		private Hashtable Graph
		{
			get
			{
				if (this.graph == null)
				{
					this.graph = new Hashtable();
				}
				return this.graph;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x0009E153 File Offset: 0x0009D153
		private Hashtable Hash
		{
			get
			{
				if (this.hash == null)
				{
					this.hash = new Hashtable();
				}
				return this.hash;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0009E16E File Offset: 0x0009D16E
		private Hashtable ObjectCache
		{
			get
			{
				if (this.objectCache == null)
				{
					this.objectCache = new Hashtable();
				}
				return this.objectCache;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x0009E189 File Offset: 0x0009D189
		internal StringCollection Warnings
		{
			get
			{
				if (this.warnings == null)
				{
					this.warnings = new StringCollection();
				}
				return this.warnings;
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0009E1A4 File Offset: 0x0009D1A4
		internal XmlSchemaObject AddItem(XmlSchemaObject item, XmlQualifiedName qname, XmlSchemas schemas)
		{
			if (item == null)
			{
				return null;
			}
			if (qname == null || qname.IsEmpty)
			{
				return null;
			}
			string key = item.GetType().Name + ":" + qname.ToString();
			ArrayList arrayList = (ArrayList)this.ObjectCache[key];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				this.ObjectCache[key] = arrayList;
			}
			for (int i = 0; i < arrayList.Count; i++)
			{
				XmlSchemaObject xmlSchemaObject = (XmlSchemaObject)arrayList[i];
				if (xmlSchemaObject == item)
				{
					return xmlSchemaObject;
				}
				if (this.Match(xmlSchemaObject, item, true))
				{
					return xmlSchemaObject;
				}
				this.Warnings.Add(Res.GetString("XmlMismatchSchemaObjects", new object[]
				{
					item.GetType().Name,
					qname.Name,
					qname.Namespace
				}));
				this.Warnings.Add("DEBUG:Cached item key:\r\n" + (string)this.looks[xmlSchemaObject] + "\r\nnew item key:\r\n" + (string)this.looks[item]);
			}
			arrayList.Add(item);
			return item;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0009E2D0 File Offset: 0x0009D2D0
		internal bool Match(XmlSchemaObject o1, XmlSchemaObject o2, bool shareTypes)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1.GetType() != o2.GetType())
			{
				return false;
			}
			if (this.Hash[o1] == null)
			{
				this.Hash[o1] = this.GetHash(o1);
			}
			int num = (int)this.Hash[o1];
			int num2 = this.GetHash(o2);
			return num == num2 && (!shareTypes || this.CompositeHash(o1, num) == this.CompositeHash(o2, num2));
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0009E350 File Offset: 0x0009D350
		private ArrayList GetDependencies(XmlSchemaObject o, ArrayList deps, Hashtable refs)
		{
			if (refs[o] == null)
			{
				refs[o] = o;
				deps.Add(o);
				ArrayList arrayList = this.Graph[o] as ArrayList;
				if (arrayList != null)
				{
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.GetDependencies((XmlSchemaObject)arrayList[i], deps, refs);
					}
				}
			}
			return deps;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0009E3B4 File Offset: 0x0009D3B4
		private int CompositeHash(XmlSchemaObject o, int hash)
		{
			ArrayList dependencies = this.GetDependencies(o, new ArrayList(), new Hashtable());
			double num = 0.0;
			for (int i = 0; i < dependencies.Count; i++)
			{
				object obj = this.Hash[dependencies[i]];
				if (obj is int)
				{
					num += (double)((int)obj / dependencies.Count);
				}
			}
			return (int)num;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0009E41C File Offset: 0x0009D41C
		internal void GenerateSchemaGraph(XmlSchemas schemas)
		{
			SchemaGraph schemaGraph = new SchemaGraph(this.Graph, schemas);
			ArrayList items = schemaGraph.GetItems();
			for (int i = 0; i < items.Count; i++)
			{
				this.GetHash((XmlSchemaObject)items[i]);
			}
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0009E464 File Offset: 0x0009D464
		private int GetHash(XmlSchemaObject o)
		{
			object obj = this.Hash[o];
			if (obj != null && !(obj is XmlSchemaObject))
			{
				return (int)obj;
			}
			string text = this.ToString(o, new SchemaObjectWriter());
			this.looks[o] = text;
			int hashCode = text.GetHashCode();
			this.Hash[o] = hashCode;
			return hashCode;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0009E4C4 File Offset: 0x0009D4C4
		private string ToString(XmlSchemaObject o, SchemaObjectWriter writer)
		{
			return writer.WriteXmlSchemaObject(o);
		}

		// Token: 0x04001450 RID: 5200
		private Hashtable graph;

		// Token: 0x04001451 RID: 5201
		private Hashtable hash;

		// Token: 0x04001452 RID: 5202
		private Hashtable objectCache;

		// Token: 0x04001453 RID: 5203
		private StringCollection warnings;

		// Token: 0x04001454 RID: 5204
		internal Hashtable looks = new Hashtable();
	}
}
