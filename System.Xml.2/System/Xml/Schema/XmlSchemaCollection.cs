using System;
using System.Collections;
using System.Threading;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x02000275 RID: 629
	[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class XmlSchemaCollection : ICollection, IEnumerable
	{
		// Token: 0x060025D3 RID: 9683 RVA: 0x000CD2F5 File Offset: 0x000CB4F5
		public XmlSchemaCollection() : this(new NameTable())
		{
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000CD304 File Offset: 0x000CB504
		public XmlSchemaCollection(XmlNameTable nametable)
		{
			if (nametable == null)
			{
				throw new ArgumentNullException("nametable");
			}
			this.nameTable = nametable;
			this.collection = Hashtable.Synchronized(new Hashtable());
			this.xmlResolver = XmlReaderSection.CreateDefaultResolver();
			this.isThreadSafe = true;
			if (this.isThreadSafe)
			{
				this.wLock = new ReaderWriterLock();
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x000CD36F File Offset: 0x000CB56F
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060025D6 RID: 9686 RVA: 0x000CD37C File Offset: 0x000CB57C
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060025D7 RID: 9687 RVA: 0x000CD384 File Offset: 0x000CB584
		// (remove) Token: 0x060025D8 RID: 9688 RVA: 0x000CD39D File Offset: 0x000CB59D
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.validationEventHandler = (ValidationEventHandler)Delegate.Combine(this.validationEventHandler, value);
			}
			remove
			{
				this.validationEventHandler = (ValidationEventHandler)Delegate.Remove(this.validationEventHandler, value);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (set) Token: 0x060025D9 RID: 9689 RVA: 0x000CD3B6 File Offset: 0x000CB5B6
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000CD3C0 File Offset: 0x000CB5C0
		public XmlSchema Add(string ns, string uri)
		{
			if (uri == null || uri.Length == 0)
			{
				throw new ArgumentNullException("uri");
			}
			XmlTextReader xmlTextReader = new XmlTextReader(uri, this.nameTable);
			xmlTextReader.XmlResolver = this.xmlResolver;
			XmlSchema result = null;
			try
			{
				result = this.Add(ns, xmlTextReader, this.xmlResolver);
				while (xmlTextReader.Read())
				{
				}
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000CD430 File Offset: 0x000CB630
		public XmlSchema Add(string ns, XmlReader reader)
		{
			return this.Add(ns, reader, this.xmlResolver);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000CD440 File Offset: 0x000CB640
		public XmlSchema Add(string ns, XmlReader reader, XmlResolver resolver)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			XmlNameTable nt = reader.NameTable;
			SchemaInfo schemaInfo = new SchemaInfo();
			Parser parser = new Parser(SchemaType.None, nt, this.GetSchemaNames(nt), this.validationEventHandler);
			parser.XmlResolver = resolver;
			SchemaType schemaType;
			try
			{
				schemaType = parser.Parse(reader, ns);
			}
			catch (XmlSchemaException e)
			{
				this.SendValidationEvent(e);
				return null;
			}
			if (schemaType == SchemaType.XSD)
			{
				schemaInfo.SchemaType = SchemaType.XSD;
				return this.Add(ns, schemaInfo, parser.XmlSchema, true, resolver);
			}
			SchemaInfo xdrSchema = parser.XdrSchema;
			return this.Add(ns, parser.XdrSchema, null, true, resolver);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x000CD4E8 File Offset: 0x000CB6E8
		public XmlSchema Add(XmlSchema schema)
		{
			return this.Add(schema, this.xmlResolver);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x000CD4F8 File Offset: 0x000CB6F8
		public XmlSchema Add(XmlSchema schema, XmlResolver resolver)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			SchemaInfo schemaInfo = new SchemaInfo();
			schemaInfo.SchemaType = SchemaType.XSD;
			return this.Add(schema.TargetNamespace, schemaInfo, schema, true, resolver);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x000CD530 File Offset: 0x000CB730
		public void Add(XmlSchemaCollection schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (this == schema)
			{
				return;
			}
			IDictionaryEnumerator enumerator = schema.collection.GetEnumerator();
			while (enumerator.MoveNext())
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)enumerator.Value;
				this.Add(xmlSchemaCollectionNode.NamespaceURI, xmlSchemaCollectionNode);
			}
		}

		// Token: 0x17000873 RID: 2163
		public XmlSchema this[string ns]
		{
			get
			{
				XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.collection[(ns != null) ? ns : string.Empty];
				if (xmlSchemaCollectionNode == null)
				{
					return null;
				}
				return xmlSchemaCollectionNode.Schema;
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000CD5B4 File Offset: 0x000CB7B4
		public bool Contains(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			return this[schema.TargetNamespace] != null;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000CD5D3 File Offset: 0x000CB7D3
		public bool Contains(string ns)
		{
			return this.collection[(ns != null) ? ns : string.Empty] != null;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000CD5EE File Offset: 0x000CB7EE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x000CD5FB File Offset: 0x000CB7FB
		public XmlSchemaCollectionEnumerator GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000CD608 File Offset: 0x000CB808
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			XmlSchemaCollectionEnumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (index == array.Length && array.IsFixedSize)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				array.SetValue(enumerator.Current, index++);
			}
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000CD674 File Offset: 0x000CB874
		public void CopyTo(XmlSchema[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			foreach (XmlSchema xmlSchema in this)
			{
				if (xmlSchema != null)
				{
					if (index == array.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					XmlSchemaCollectionEnumerator enumerator;
					array[index++] = enumerator.Current;
				}
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000CD6D8 File Offset: 0x000CB8D8
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x000CD6DB File Offset: 0x000CB8DB
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x000CD6DE File Offset: 0x000CB8DE
		int ICollection.Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000CD6EC File Offset: 0x000CB8EC
		internal SchemaInfo GetSchemaInfo(string ns)
		{
			XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.collection[(ns != null) ? ns : string.Empty];
			if (xmlSchemaCollectionNode == null)
			{
				return null;
			}
			return xmlSchemaCollectionNode.SchemaInfo;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000CD720 File Offset: 0x000CB920
		internal SchemaNames GetSchemaNames(XmlNameTable nt)
		{
			if (this.nameTable != nt)
			{
				return new SchemaNames(nt);
			}
			if (this.schemaNames == null)
			{
				this.schemaNames = new SchemaNames(this.nameTable);
			}
			return this.schemaNames;
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000CD751 File Offset: 0x000CB951
		internal XmlSchema Add(string ns, SchemaInfo schemaInfo, XmlSchema schema, bool compile)
		{
			return this.Add(ns, schemaInfo, schema, compile, this.xmlResolver);
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000CD764 File Offset: 0x000CB964
		private XmlSchema Add(string ns, SchemaInfo schemaInfo, XmlSchema schema, bool compile, XmlResolver resolver)
		{
			int num = 0;
			if (schema != null)
			{
				if (schema.ErrorCount == 0 && compile)
				{
					if (!schema.CompileSchema(this, resolver, schemaInfo, ns, this.validationEventHandler, this.nameTable, true))
					{
						num = 1;
					}
					ns = ((schema.TargetNamespace == null) ? string.Empty : schema.TargetNamespace);
				}
				num += schema.ErrorCount;
			}
			else
			{
				num += schemaInfo.ErrorCount;
				ns = this.NameTable.Add(ns);
			}
			if (num == 0)
			{
				this.Add(ns, new XmlSchemaCollectionNode
				{
					NamespaceURI = ns,
					SchemaInfo = schemaInfo,
					Schema = schema
				});
				return schema;
			}
			return null;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000CD804 File Offset: 0x000CBA04
		private void Add(string ns, XmlSchemaCollectionNode node)
		{
			if (this.isThreadSafe)
			{
				this.wLock.AcquireWriterLock(this.timeout);
			}
			try
			{
				if (this.collection[ns] != null)
				{
					this.collection.Remove(ns);
				}
				this.collection.Add(ns, node);
			}
			finally
			{
				if (this.isThreadSafe)
				{
					this.wLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000CD878 File Offset: 0x000CBA78
		private void SendValidationEvent(XmlSchemaException e)
		{
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this, new ValidationEventArgs(e));
				return;
			}
			throw e;
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x000CD896 File Offset: 0x000CBA96
		// (set) Token: 0x060025F1 RID: 9713 RVA: 0x000CD89E File Offset: 0x000CBA9E
		internal ValidationEventHandler EventHandler
		{
			get
			{
				return this.validationEventHandler;
			}
			set
			{
				this.validationEventHandler = value;
			}
		}

		// Token: 0x04001089 RID: 4233
		private Hashtable collection;

		// Token: 0x0400108A RID: 4234
		private XmlNameTable nameTable;

		// Token: 0x0400108B RID: 4235
		private SchemaNames schemaNames;

		// Token: 0x0400108C RID: 4236
		private ReaderWriterLock wLock;

		// Token: 0x0400108D RID: 4237
		private int timeout = -1;

		// Token: 0x0400108E RID: 4238
		private bool isThreadSafe = true;

		// Token: 0x0400108F RID: 4239
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001090 RID: 4240
		private XmlResolver xmlResolver;
	}
}
