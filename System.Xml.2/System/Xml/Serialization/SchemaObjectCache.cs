using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000141 RID: 321
	internal class SchemaObjectCache
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x000663D8 File Offset: 0x000645D8
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

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x000663F3 File Offset: 0x000645F3
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

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0006640E File Offset: 0x0006460E
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

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00066429 File Offset: 0x00064629
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

		// Token: 0x06001706 RID: 5894 RVA: 0x00066444 File Offset: 0x00064644
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

		// Token: 0x06001707 RID: 5895 RVA: 0x00066568 File Offset: 0x00064768
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

		// Token: 0x06001708 RID: 5896 RVA: 0x000665F0 File Offset: 0x000647F0
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

		// Token: 0x06001709 RID: 5897 RVA: 0x00066654 File Offset: 0x00064854
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

		// Token: 0x0600170A RID: 5898 RVA: 0x000666BC File Offset: 0x000648BC
		internal void GenerateSchemaGraph(XmlSchemas schemas)
		{
			SchemaGraph schemaGraph = new SchemaGraph(this.Graph, schemas);
			ArrayList items = schemaGraph.GetItems();
			for (int i = 0; i < items.Count; i++)
			{
				this.GetHash((XmlSchemaObject)items[i]);
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00066704 File Offset: 0x00064904
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

		// Token: 0x0600170C RID: 5900 RVA: 0x00066764 File Offset: 0x00064964
		private string ToString(XmlSchemaObject o, SchemaObjectWriter writer)
		{
			return writer.WriteXmlSchemaObject(o);
		}

		// Token: 0x04000AB5 RID: 2741
		private Hashtable graph;

		// Token: 0x04000AB6 RID: 2742
		private Hashtable hash;

		// Token: 0x04000AB7 RID: 2743
		private Hashtable objectCache;

		// Token: 0x04000AB8 RID: 2744
		private StringCollection warnings;

		// Token: 0x04000AB9 RID: 2745
		internal Hashtable looks = new Hashtable();
	}
}
