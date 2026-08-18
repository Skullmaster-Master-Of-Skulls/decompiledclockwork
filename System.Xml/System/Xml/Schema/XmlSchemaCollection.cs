using System;
using System.Collections;
using System.Threading;
using System.Xml.XmlConfiguration;

namespace System.Xml.Schema
{
	// Token: 0x0200023D RID: 573
	[Obsolete("Use System.Xml.Schema.XmlSchemaSet for schema compilation and validation. http://go.microsoft.com/fwlink/?linkid=14202")]
	public sealed class XmlSchemaCollection : ICollection, IEnumerable
	{
		// Token: 0x06001B4B RID: 6987 RVA: 0x00081421 File Offset: 0x00080421
		public XmlSchemaCollection() : this(new NameTable())
		{
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00081430 File Offset: 0x00080430
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

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0008149B File Offset: 0x0008049B
		public int Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000814A8 File Offset: 0x000804A8
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001B4F RID: 6991 RVA: 0x000814B0 File Offset: 0x000804B0
		// (remove) Token: 0x06001B50 RID: 6992 RVA: 0x000814C9 File Offset: 0x000804C9
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

		// Token: 0x170006D6 RID: 1750
		// (set) Token: 0x06001B51 RID: 6993 RVA: 0x000814E2 File Offset: 0x000804E2
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000814EC File Offset: 0x000804EC
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

		// Token: 0x06001B53 RID: 6995 RVA: 0x0008155C File Offset: 0x0008055C
		public XmlSchema Add(string ns, XmlReader reader)
		{
			return this.Add(ns, reader, this.xmlResolver);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0008156C File Offset: 0x0008056C
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

		// Token: 0x06001B55 RID: 6997 RVA: 0x00081610 File Offset: 0x00080610
		public XmlSchema Add(XmlSchema schema)
		{
			return this.Add(schema, this.xmlResolver);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x00081620 File Offset: 0x00080620
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

		// Token: 0x06001B57 RID: 6999 RVA: 0x00081658 File Offset: 0x00080658
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

		// Token: 0x170006D7 RID: 1751
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

		// Token: 0x06001B59 RID: 7001 RVA: 0x000816DC File Offset: 0x000806DC
		public bool Contains(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			return this[schema.TargetNamespace] != null;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000816FE File Offset: 0x000806FE
		public bool Contains(string ns)
		{
			return this.collection[(ns != null) ? ns : string.Empty] != null;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0008171C File Offset: 0x0008071C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x00081729 File Offset: 0x00080729
		public XmlSchemaCollectionEnumerator GetEnumerator()
		{
			return new XmlSchemaCollectionEnumerator(this.collection);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00081738 File Offset: 0x00080738
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

		// Token: 0x06001B5E RID: 7006 RVA: 0x000817A4 File Offset: 0x000807A4
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

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x00081808 File Offset: 0x00080808
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0008180B File Offset: 0x0008080B
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0008180E File Offset: 0x0008080E
		int ICollection.Count
		{
			get
			{
				return this.collection.Count;
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0008181C File Offset: 0x0008081C
		internal SchemaInfo GetSchemaInfo(string ns)
		{
			XmlSchemaCollectionNode xmlSchemaCollectionNode = (XmlSchemaCollectionNode)this.collection[(ns != null) ? ns : string.Empty];
			if (xmlSchemaCollectionNode == null)
			{
				return null;
			}
			return xmlSchemaCollectionNode.SchemaInfo;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00081850 File Offset: 0x00080850
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

		// Token: 0x06001B64 RID: 7012 RVA: 0x00081881 File Offset: 0x00080881
		internal XmlSchema Add(string ns, SchemaInfo schemaInfo, XmlSchema schema, bool compile)
		{
			return this.Add(ns, schemaInfo, schema, compile, this.xmlResolver);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00081894 File Offset: 0x00080894
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

		// Token: 0x06001B66 RID: 7014 RVA: 0x00081930 File Offset: 0x00080930
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

		// Token: 0x06001B67 RID: 7015 RVA: 0x000819A4 File Offset: 0x000809A4
		private void SendValidationEvent(XmlSchemaException e)
		{
			if (this.validationEventHandler != null)
			{
				this.validationEventHandler(this, new ValidationEventArgs(e));
				return;
			}
			throw e;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000819C2 File Offset: 0x000809C2
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x000819CA File Offset: 0x000809CA
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

		// Token: 0x04001102 RID: 4354
		private Hashtable collection;

		// Token: 0x04001103 RID: 4355
		private XmlNameTable nameTable;

		// Token: 0x04001104 RID: 4356
		private SchemaNames schemaNames;

		// Token: 0x04001105 RID: 4357
		private ReaderWriterLock wLock;

		// Token: 0x04001106 RID: 4358
		private int timeout = -1;

		// Token: 0x04001107 RID: 4359
		private bool isThreadSafe = true;

		// Token: 0x04001108 RID: 4360
		private ValidationEventHandler validationEventHandler;

		// Token: 0x04001109 RID: 4361
		private XmlResolver xmlResolver;
	}
}
