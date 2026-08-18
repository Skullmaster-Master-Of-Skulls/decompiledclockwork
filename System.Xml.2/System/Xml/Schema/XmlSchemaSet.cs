using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Xml.Schema
{
	// Token: 0x020002A9 RID: 681
	public class XmlSchemaSet
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x000CFE70 File Offset: 0x000CE070
		internal object InternalSyncObject
		{
			get
			{
				if (this.internalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange<object>(ref this.internalSyncObject, value, null);
				}
				return this.internalSyncObject;
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000CFE9F File Offset: 0x000CE09F
		public XmlSchemaSet() : this(new NameTable())
		{
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000CFEAC File Offset: 0x000CE0AC
		public XmlSchemaSet(XmlNameTable nameTable)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("nameTable");
			}
			this.nameTable = nameTable;
			this.schemas = new SortedList();
			this.schemaLocations = new Hashtable();
			this.chameleonSchemas = new Hashtable();
			this.targetNamespaces = new Hashtable();
			this.internalEventHandler = new ValidationEventHandler(this.InternalValidationCallback);
			this.eventHandler = this.internalEventHandler;
			this.readerSettings = new XmlReaderSettings();
			if (this.readerSettings.GetXmlResolver() == null)
			{
				this.readerSettings.XmlResolver = new XmlUrlResolver();
				this.readerSettings.IsXmlResolverSet = false;
			}
			this.readerSettings.NameTable = nameTable;
			this.readerSettings.DtdProcessing = DtdProcessing.Prohibit;
			this.compilationSettings = new XmlSchemaCompilationSettings();
			this.cachedCompiledInfo = new SchemaInfo();
			this.compileAll = true;
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x000CFF87 File Offset: 0x000CE187
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06002793 RID: 10131 RVA: 0x000CFF90 File Offset: 0x000CE190
		// (remove) Token: 0x06002794 RID: 10132 RVA: 0x000CFFE4 File Offset: 0x000CE1E4
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, this.internalEventHandler);
				this.eventHandler = (ValidationEventHandler)Delegate.Combine(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
			}
			remove
			{
				this.eventHandler = (ValidationEventHandler)Delegate.Remove(this.eventHandler, value);
				if (this.eventHandler == null)
				{
					this.eventHandler = this.internalEventHandler;
				}
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x000D0011 File Offset: 0x000CE211
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (set) Token: 0x06002796 RID: 10134 RVA: 0x000D0019 File Offset: 0x000CE219
		public XmlResolver XmlResolver
		{
			set
			{
				this.readerSettings.XmlResolver = value;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x000D0027 File Offset: 0x000CE227
		// (set) Token: 0x06002798 RID: 10136 RVA: 0x000D002F File Offset: 0x000CE22F
		public XmlSchemaCompilationSettings CompilationSettings
		{
			get
			{
				return this.compilationSettings;
			}
			set
			{
				this.compilationSettings = value;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x000D0038 File Offset: 0x000CE238
		public int Count
		{
			get
			{
				return this.schemas.Count;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x0600279A RID: 10138 RVA: 0x000D0045 File Offset: 0x000CE245
		public XmlSchemaObjectTable GlobalElements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new XmlSchemaObjectTable();
				}
				return this.elements;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x000D0060 File Offset: 0x000CE260
		public XmlSchemaObjectTable GlobalAttributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlSchemaObjectTable();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000D007B File Offset: 0x000CE27B
		public XmlSchemaObjectTable GlobalTypes
		{
			get
			{
				if (this.schemaTypes == null)
				{
					this.schemaTypes = new XmlSchemaObjectTable();
				}
				return this.schemaTypes;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x000D0096 File Offset: 0x000CE296
		internal XmlSchemaObjectTable SubstitutionGroups
		{
			get
			{
				if (this.substitutionGroups == null)
				{
					this.substitutionGroups = new XmlSchemaObjectTable();
				}
				return this.substitutionGroups;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000D00B1 File Offset: 0x000CE2B1
		internal Hashtable SchemaLocations
		{
			get
			{
				return this.schemaLocations;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x000D00B9 File Offset: 0x000CE2B9
		internal XmlSchemaObjectTable TypeExtensions
		{
			get
			{
				if (this.typeExtensions == null)
				{
					this.typeExtensions = new XmlSchemaObjectTable();
				}
				return this.typeExtensions;
			}
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000D00D4 File Offset: 0x000CE2D4
		public XmlSchema Add(string targetNamespace, string schemaUri)
		{
			if (schemaUri == null || schemaUri.Length == 0)
			{
				throw new ArgumentNullException("schemaUri");
			}
			if (targetNamespace != null)
			{
				targetNamespace = XmlComplianceUtil.CDataNormalize(targetNamespace);
			}
			XmlSchema result = null;
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				XmlResolver xmlResolver = this.readerSettings.GetXmlResolver();
				if (xmlResolver == null)
				{
					xmlResolver = new XmlUrlResolver();
				}
				Uri schemaUri2 = xmlResolver.ResolveUri(null, schemaUri);
				if (this.IsSchemaLoaded(schemaUri2, targetNamespace, out result))
				{
					return result;
				}
				XmlReader xmlReader = XmlReader.Create(schemaUri, this.readerSettings);
				try
				{
					result = this.Add(targetNamespace, this.ParseSchema(targetNamespace, xmlReader));
					while (xmlReader.Read())
					{
					}
				}
				finally
				{
					xmlReader.Close();
				}
			}
			return result;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000D01A4 File Offset: 0x000CE3A4
		public XmlSchema Add(string targetNamespace, XmlReader schemaDocument)
		{
			if (schemaDocument == null)
			{
				throw new ArgumentNullException("schemaDocument");
			}
			if (targetNamespace != null)
			{
				targetNamespace = XmlComplianceUtil.CDataNormalize(targetNamespace);
			}
			object obj = this.InternalSyncObject;
			XmlSchema result;
			lock (obj)
			{
				XmlSchema xmlSchema = null;
				Uri schemaUri = new Uri(schemaDocument.BaseURI, UriKind.RelativeOrAbsolute);
				if (this.IsSchemaLoaded(schemaUri, targetNamespace, out xmlSchema))
				{
					result = xmlSchema;
				}
				else
				{
					DtdProcessing dtdProcessing = this.readerSettings.DtdProcessing;
					this.SetDtdProcessing(schemaDocument);
					xmlSchema = this.Add(targetNamespace, this.ParseSchema(targetNamespace, schemaDocument));
					this.readerSettings.DtdProcessing = dtdProcessing;
					result = xmlSchema;
				}
			}
			return result;
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x000D0250 File Offset: 0x000CE450
		public void Add(XmlSchemaSet schemas)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (this == schemas)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			try
			{
				for (;;)
				{
					Monitor.TryEnter(this.InternalSyncObject, ref flag);
					if (flag)
					{
						Monitor.TryEnter(schemas.InternalSyncObject, ref flag2);
						if (flag2)
						{
							break;
						}
						Monitor.Exit(this.InternalSyncObject);
						flag = false;
						Thread.Yield();
					}
				}
				if (schemas.IsCompiled)
				{
					this.CopyFromCompiledSet(schemas);
				}
				else
				{
					bool flag3 = false;
					foreach (object obj in schemas.SortedSchemas.Values)
					{
						XmlSchema xmlSchema = (XmlSchema)obj;
						string text = xmlSchema.TargetNamespace;
						if (text == null)
						{
							text = string.Empty;
						}
						if (!this.schemas.ContainsKey(xmlSchema.SchemaId) && this.FindSchemaByNSAndUrl(xmlSchema.BaseUri, text, null) == null && this.Add(xmlSchema.TargetNamespace, xmlSchema) == null)
						{
							flag3 = true;
							break;
						}
					}
					if (flag3)
					{
						foreach (object obj2 in schemas.SortedSchemas.Values)
						{
							XmlSchema xmlSchema2 = (XmlSchema)obj2;
							this.schemas.Remove(xmlSchema2.SchemaId);
							this.schemaLocations.Remove(xmlSchema2.BaseUri);
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.InternalSyncObject);
				}
				if (flag2)
				{
					Monitor.Exit(schemas.InternalSyncObject);
				}
			}
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x000D0434 File Offset: 0x000CE634
		public XmlSchema Add(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			object obj = this.InternalSyncObject;
			XmlSchema result;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schema.SchemaId))
				{
					result = schema;
				}
				else
				{
					result = this.Add(schema.TargetNamespace, schema);
				}
			}
			return result;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x000D04A8 File Offset: 0x000CE6A8
		public XmlSchema Remove(XmlSchema schema)
		{
			return this.Remove(schema, true);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000D04B4 File Offset: 0x000CE6B4
		public bool RemoveRecursive(XmlSchema schemaToRemove)
		{
			if (schemaToRemove == null)
			{
				throw new ArgumentNullException("schemaToRemove");
			}
			if (!this.schemas.ContainsKey(schemaToRemove.SchemaId))
			{
				return false;
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schemaToRemove.SchemaId))
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add(this.GetTargetNamespace(schemaToRemove), schemaToRemove);
					for (int i = 0; i < schemaToRemove.ImportedNamespaces.Count; i++)
					{
						string text = (string)schemaToRemove.ImportedNamespaces[i];
						if (hashtable[text] == null)
						{
							hashtable.Add(text, text);
						}
					}
					ArrayList arrayList = new ArrayList();
					for (int j = 0; j < this.schemas.Count; j++)
					{
						XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(j);
						if (xmlSchema != schemaToRemove && !schemaToRemove.ImportedSchemas.Contains(xmlSchema))
						{
							arrayList.Add(xmlSchema);
						}
					}
					for (int k = 0; k < arrayList.Count; k++)
					{
						XmlSchema xmlSchema = (XmlSchema)arrayList[k];
						if (xmlSchema.ImportedNamespaces.Count > 0)
						{
							foreach (object obj2 in hashtable.Keys)
							{
								string item = (string)obj2;
								if (xmlSchema.ImportedNamespaces.Contains(item))
								{
									this.SendValidationEvent(new XmlSchemaException("Sch_SchemaNotRemoved", string.Empty), XmlSeverityType.Warning);
									return false;
								}
							}
						}
					}
					this.Remove(schemaToRemove, true);
					for (int l = 0; l < schemaToRemove.ImportedSchemas.Count; l++)
					{
						XmlSchema schema = (XmlSchema)schemaToRemove.ImportedSchemas[l];
						this.Remove(schema, true);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000D06F0 File Offset: 0x000CE8F0
		public bool Contains(string targetNamespace)
		{
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			return this.targetNamespaces[targetNamespace] != null;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000D070B File Offset: 0x000CE90B
		public bool Contains(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			return this.schemas.ContainsValue(schema);
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000D0728 File Offset: 0x000CE928
		public void Compile()
		{
			if (this.isCompiled)
			{
				return;
			}
			if (this.schemas.Count == 0)
			{
				this.ClearTables();
				this.cachedCompiledInfo = new SchemaInfo();
				this.isCompiled = true;
				this.compileAll = false;
				return;
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (!this.isCompiled)
				{
					Compiler compiler = new Compiler(this.nameTable, this.eventHandler, this.schemaForSchema, this.compilationSettings);
					SchemaInfo schemaInfo = new SchemaInfo();
					int i = 0;
					if (!this.compileAll)
					{
						compiler.ImportAllCompiledSchemas(this);
					}
					try
					{
						XmlSchema buildInSchema = Preprocessor.GetBuildInSchema();
						i = 0;
						while (i < this.schemas.Count)
						{
							XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(i);
							Monitor.Enter(xmlSchema);
							if (!xmlSchema.IsPreprocessed)
							{
								this.SendValidationEvent(new XmlSchemaException("Sch_SchemaNotPreprocessed", string.Empty), XmlSeverityType.Error);
								this.isCompiled = false;
								return;
							}
							if (!xmlSchema.IsCompiledBySet)
							{
								goto IL_FD;
							}
							if (this.compileAll)
							{
								if (xmlSchema != buildInSchema)
								{
									goto IL_FD;
								}
								compiler.Prepare(xmlSchema, false);
							}
							IL_106:
							i++;
							continue;
							IL_FD:
							compiler.Prepare(xmlSchema, true);
							goto IL_106;
						}
						this.isCompiled = compiler.Execute(this, schemaInfo);
						if (this.isCompiled)
						{
							if (!this.compileAll)
							{
								schemaInfo.Add(this.cachedCompiledInfo, this.eventHandler);
							}
							this.compileAll = false;
							this.cachedCompiledInfo = schemaInfo;
						}
					}
					finally
					{
						if (i == this.schemas.Count)
						{
							i--;
						}
						for (int j = i; j >= 0; j--)
						{
							XmlSchema xmlSchema2 = (XmlSchema)this.schemas.GetByIndex(j);
							if (xmlSchema2 == Preprocessor.GetBuildInSchema())
							{
								Monitor.Exit(xmlSchema2);
							}
							else
							{
								xmlSchema2.IsCompiledBySet = this.isCompiled;
								Monitor.Exit(xmlSchema2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000D0938 File Offset: 0x000CEB38
		public XmlSchema Reprocess(XmlSchema schema)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			if (!this.schemas.ContainsKey(schema.SchemaId))
			{
				throw new ArgumentException(Res.GetString("Sch_SchemaDoesNotExist"), "schema");
			}
			XmlSchema xmlSchema = schema;
			object obj = this.InternalSyncObject;
			XmlSchema result;
			lock (obj)
			{
				this.RemoveSchemaFromGlobalTables(schema);
				this.RemoveSchemaFromCaches(schema);
				if (schema.BaseUri != null)
				{
					this.schemaLocations.Remove(schema.BaseUri);
				}
				string targetNamespace = this.GetTargetNamespace(schema);
				if (this.Schemas(targetNamespace).Count == 0)
				{
					this.targetNamespaces.Remove(targetNamespace);
				}
				this.isCompiled = false;
				this.compileAll = true;
				if (schema.ErrorCount != 0)
				{
					result = xmlSchema;
				}
				else if (this.PreprocessSchema(ref schema, schema.TargetNamespace))
				{
					if (this.targetNamespaces[targetNamespace] == null)
					{
						this.targetNamespaces.Add(targetNamespace, targetNamespace);
					}
					if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
					{
						this.schemaForSchema = schema;
					}
					for (int i = 0; i < schema.ImportedSchemas.Count; i++)
					{
						XmlSchema xmlSchema2 = (XmlSchema)schema.ImportedSchemas[i];
						if (!this.schemas.ContainsKey(xmlSchema2.SchemaId))
						{
							this.schemas.Add(xmlSchema2.SchemaId, xmlSchema2);
						}
						targetNamespace = this.GetTargetNamespace(xmlSchema2);
						if (this.targetNamespaces[targetNamespace] == null)
						{
							this.targetNamespaces.Add(targetNamespace, targetNamespace);
						}
						if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
						{
							this.schemaForSchema = schema;
						}
					}
					result = schema;
				}
				else
				{
					result = xmlSchema;
				}
			}
			return result;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000D0B48 File Offset: 0x000CED48
		public void CopyTo(XmlSchema[] schemas, int index)
		{
			if (schemas == null)
			{
				throw new ArgumentNullException("schemas");
			}
			if (index < 0 || index > schemas.Length - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.schemas.Values.CopyTo(schemas, index);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000D0B81 File Offset: 0x000CED81
		public ICollection Schemas()
		{
			return this.schemas.Values;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000D0B90 File Offset: 0x000CED90
		public ICollection Schemas(string targetNamespace)
		{
			ArrayList arrayList = new ArrayList();
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			for (int i = 0; i < this.schemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)this.schemas.GetByIndex(i);
				if (this.GetTargetNamespace(xmlSchema) == targetNamespace)
				{
					arrayList.Add(xmlSchema);
				}
			}
			return arrayList;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000D0BED File Offset: 0x000CEDED
		private XmlSchema Add(string targetNamespace, XmlSchema schema)
		{
			if (schema == null || schema.ErrorCount != 0)
			{
				return null;
			}
			if (this.PreprocessSchema(ref schema, targetNamespace))
			{
				this.AddSchemaToSet(schema);
				this.isCompiled = false;
				return schema;
			}
			return null;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000D0C18 File Offset: 0x000CEE18
		internal void Add(string targetNamespace, XmlReader reader, Hashtable validatedNamespaces)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			if (validatedNamespaces[targetNamespace] != null)
			{
				if (this.FindSchemaByNSAndUrl(new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute), targetNamespace, null) != null)
				{
					return;
				}
				throw new XmlSchemaException("Sch_ComponentAlreadySeenForNS", targetNamespace);
			}
			else
			{
				XmlSchema xmlSchema;
				if (this.IsSchemaLoaded(new Uri(reader.BaseURI, UriKind.RelativeOrAbsolute), targetNamespace, out xmlSchema))
				{
					return;
				}
				xmlSchema = this.ParseSchema(targetNamespace, reader);
				DictionaryEntry[] array = new DictionaryEntry[this.schemaLocations.Count];
				this.schemaLocations.CopyTo(array, 0);
				this.Add(targetNamespace, xmlSchema);
				if (xmlSchema.ImportedSchemas.Count > 0)
				{
					for (int i = 0; i < xmlSchema.ImportedSchemas.Count; i++)
					{
						XmlSchema xmlSchema2 = (XmlSchema)xmlSchema.ImportedSchemas[i];
						string text = xmlSchema2.TargetNamespace;
						if (text == null)
						{
							text = string.Empty;
						}
						if (validatedNamespaces[text] != null && this.FindSchemaByNSAndUrl(xmlSchema2.BaseUri, text, array) == null)
						{
							this.RemoveRecursive(xmlSchema);
							throw new XmlSchemaException("Sch_ComponentAlreadySeenForNS", text);
						}
					}
				}
				return;
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000D0D28 File Offset: 0x000CEF28
		internal XmlSchema FindSchemaByNSAndUrl(Uri schemaUri, string ns, DictionaryEntry[] locationsTable)
		{
			if (schemaUri == null || schemaUri.OriginalString.Length == 0)
			{
				return null;
			}
			XmlSchema xmlSchema = null;
			if (locationsTable == null)
			{
				xmlSchema = (XmlSchema)this.schemaLocations[schemaUri];
			}
			else
			{
				for (int i = 0; i < locationsTable.Length; i++)
				{
					if (schemaUri.Equals(locationsTable[i].Key))
					{
						xmlSchema = (XmlSchema)locationsTable[i].Value;
						break;
					}
				}
			}
			if (xmlSchema != null)
			{
				string a = (xmlSchema.TargetNamespace == null) ? string.Empty : xmlSchema.TargetNamespace;
				if (a == ns)
				{
					return xmlSchema;
				}
				if (a == string.Empty)
				{
					ChameleonKey key = new ChameleonKey(ns, xmlSchema);
					xmlSchema = (XmlSchema)this.chameleonSchemas[key];
				}
				else
				{
					xmlSchema = null;
				}
			}
			return xmlSchema;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000D0DEC File Offset: 0x000CEFEC
		private void SetDtdProcessing(XmlReader reader)
		{
			if (reader.Settings != null)
			{
				this.readerSettings.DtdProcessing = reader.Settings.DtdProcessing;
				return;
			}
			XmlTextReader xmlTextReader = reader as XmlTextReader;
			if (xmlTextReader != null)
			{
				this.readerSettings.DtdProcessing = xmlTextReader.DtdProcessing;
			}
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000D0E34 File Offset: 0x000CF034
		private void AddSchemaToSet(XmlSchema schema)
		{
			this.schemas.Add(schema.SchemaId, schema);
			string targetNamespace = this.GetTargetNamespace(schema);
			if (this.targetNamespaces[targetNamespace] == null)
			{
				this.targetNamespaces.Add(targetNamespace, targetNamespace);
			}
			if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
			{
				this.schemaForSchema = schema;
			}
			for (int i = 0; i < schema.ImportedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)schema.ImportedSchemas[i];
				if (!this.schemas.ContainsKey(xmlSchema.SchemaId))
				{
					this.schemas.Add(xmlSchema.SchemaId, xmlSchema);
				}
				targetNamespace = this.GetTargetNamespace(xmlSchema);
				if (this.targetNamespaces[targetNamespace] == null)
				{
					this.targetNamespaces.Add(targetNamespace, targetNamespace);
				}
				if (this.schemaForSchema == null && targetNamespace == "http://www.w3.org/2001/XMLSchema" && schema.SchemaTypes[DatatypeImplementation.QnAnyType] != null)
				{
					this.schemaForSchema = schema;
				}
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000D0F58 File Offset: 0x000CF158
		private void ProcessNewSubstitutionGroups(XmlSchemaObjectTable substitutionGroupsTable, bool resolve)
		{
			foreach (object obj in substitutionGroupsTable.Values)
			{
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)obj;
				if (resolve)
				{
					this.ResolveSubstitutionGroup(xmlSchemaSubstitutionGroup, substitutionGroupsTable);
				}
				XmlQualifiedName examplar = xmlSchemaSubstitutionGroup.Examplar;
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup2 = (XmlSchemaSubstitutionGroup)this.substitutionGroups[examplar];
				if (xmlSchemaSubstitutionGroup2 != null)
				{
					for (int i = 0; i < xmlSchemaSubstitutionGroup.Members.Count; i++)
					{
						if (!xmlSchemaSubstitutionGroup2.Members.Contains(xmlSchemaSubstitutionGroup.Members[i]))
						{
							xmlSchemaSubstitutionGroup2.Members.Add(xmlSchemaSubstitutionGroup.Members[i]);
						}
					}
				}
				else
				{
					this.AddToTable(this.substitutionGroups, examplar, xmlSchemaSubstitutionGroup);
				}
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000D1038 File Offset: 0x000CF238
		private void ResolveSubstitutionGroup(XmlSchemaSubstitutionGroup substitutionGroup, XmlSchemaObjectTable substTable)
		{
			List<XmlSchemaElement> list = null;
			XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)this.elements[substitutionGroup.Examplar];
			if (substitutionGroup.Members.Contains(xmlSchemaElement))
			{
				return;
			}
			for (int i = 0; i < substitutionGroup.Members.Count; i++)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)substitutionGroup.Members[i];
				XmlSchemaSubstitutionGroup xmlSchemaSubstitutionGroup = (XmlSchemaSubstitutionGroup)substTable[xmlSchemaElement2.QualifiedName];
				if (xmlSchemaSubstitutionGroup != null)
				{
					this.ResolveSubstitutionGroup(xmlSchemaSubstitutionGroup, substTable);
					for (int j = 0; j < xmlSchemaSubstitutionGroup.Members.Count; j++)
					{
						XmlSchemaElement xmlSchemaElement3 = (XmlSchemaElement)xmlSchemaSubstitutionGroup.Members[j];
						if (xmlSchemaElement3 != xmlSchemaElement2)
						{
							if (list == null)
							{
								list = new List<XmlSchemaElement>();
							}
							list.Add(xmlSchemaElement3);
						}
					}
				}
			}
			if (list != null)
			{
				for (int k = 0; k < list.Count; k++)
				{
					substitutionGroup.Members.Add(list[k]);
				}
			}
			substitutionGroup.Members.Add(xmlSchemaElement);
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000D1138 File Offset: 0x000CF338
		internal XmlSchema Remove(XmlSchema schema, bool forceCompile)
		{
			if (schema == null)
			{
				throw new ArgumentNullException("schema");
			}
			object obj = this.InternalSyncObject;
			lock (obj)
			{
				if (this.schemas.ContainsKey(schema.SchemaId))
				{
					if (forceCompile)
					{
						this.RemoveSchemaFromGlobalTables(schema);
						this.RemoveSchemaFromCaches(schema);
					}
					this.schemas.Remove(schema.SchemaId);
					if (schema.BaseUri != null)
					{
						this.schemaLocations.Remove(schema.BaseUri);
					}
					string targetNamespace = this.GetTargetNamespace(schema);
					if (this.Schemas(targetNamespace).Count == 0)
					{
						this.targetNamespaces.Remove(targetNamespace);
					}
					if (forceCompile)
					{
						this.isCompiled = false;
						this.compileAll = true;
					}
					return schema;
				}
			}
			return null;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000D1218 File Offset: 0x000CF418
		private void ClearTables()
		{
			this.GlobalElements.Clear();
			this.GlobalAttributes.Clear();
			this.GlobalTypes.Clear();
			this.SubstitutionGroups.Clear();
			this.TypeExtensions.Clear();
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000D1254 File Offset: 0x000CF454
		internal bool PreprocessSchema(ref XmlSchema schema, string targetNamespace)
		{
			Preprocessor preprocessor = new Preprocessor(this.nameTable, this.GetSchemaNames(this.nameTable), this.eventHandler, this.compilationSettings);
			preprocessor.XmlResolver = this.readerSettings.GetXmlResolver_CheckConfig();
			preprocessor.ReaderSettings = this.readerSettings;
			preprocessor.SchemaLocations = this.schemaLocations;
			preprocessor.ChameleonSchemas = this.chameleonSchemas;
			bool result = preprocessor.Execute(schema, targetNamespace, true);
			schema = preprocessor.RootSchema;
			return result;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000D12D0 File Offset: 0x000CF4D0
		internal XmlSchema ParseSchema(string targetNamespace, XmlReader reader)
		{
			XmlNameTable nt = reader.NameTable;
			SchemaNames schemaNames = this.GetSchemaNames(nt);
			Parser parser = new Parser(SchemaType.XSD, nt, schemaNames, this.eventHandler);
			parser.XmlResolver = this.readerSettings.GetXmlResolver_CheckConfig();
			try
			{
				SchemaType schemaType = parser.Parse(reader, targetNamespace);
			}
			catch (XmlSchemaException e)
			{
				this.SendValidationEvent(e, XmlSeverityType.Error);
				return null;
			}
			return parser.XmlSchema;
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000D1340 File Offset: 0x000CF540
		internal void CopyFromCompiledSet(XmlSchemaSet otherSet)
		{
			SortedList sortedSchemas = otherSet.SortedSchemas;
			bool flag = this.schemas.Count == 0;
			ArrayList arrayList = new ArrayList();
			SchemaInfo schemaInfo = new SchemaInfo();
			for (int i = 0; i < sortedSchemas.Count; i++)
			{
				XmlSchema xmlSchema = (XmlSchema)sortedSchemas.GetByIndex(i);
				Uri baseUri = xmlSchema.BaseUri;
				if (this.schemas.ContainsKey(xmlSchema.SchemaId) || (baseUri != null && baseUri.OriginalString.Length != 0 && this.schemaLocations[baseUri] != null))
				{
					arrayList.Add(xmlSchema);
				}
				else
				{
					this.schemas.Add(xmlSchema.SchemaId, xmlSchema);
					if (baseUri != null && baseUri.OriginalString.Length != 0)
					{
						this.schemaLocations.Add(baseUri, xmlSchema);
					}
					string targetNamespace = this.GetTargetNamespace(xmlSchema);
					if (this.targetNamespaces[targetNamespace] == null)
					{
						this.targetNamespaces.Add(targetNamespace, targetNamespace);
					}
				}
			}
			this.VerifyTables();
			foreach (object obj in otherSet.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (!this.AddToTable(this.elements, xmlSchemaElement.QualifiedName, xmlSchemaElement))
				{
					goto IL_26E;
				}
			}
			foreach (object obj2 in otherSet.GlobalAttributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				if (!this.AddToTable(this.attributes, xmlSchemaAttribute.QualifiedName, xmlSchemaAttribute))
				{
					goto IL_26E;
				}
			}
			foreach (object obj3 in otherSet.GlobalTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				if (!this.AddToTable(this.schemaTypes, xmlSchemaType.QualifiedName, xmlSchemaType))
				{
					goto IL_26E;
				}
			}
			this.ProcessNewSubstitutionGroups(otherSet.SubstitutionGroups, false);
			schemaInfo.Add(this.cachedCompiledInfo, this.eventHandler);
			schemaInfo.Add(otherSet.CompiledInfo, this.eventHandler);
			this.cachedCompiledInfo = schemaInfo;
			if (flag)
			{
				this.isCompiled = true;
				this.compileAll = false;
			}
			return;
			IL_26E:
			foreach (object obj4 in sortedSchemas.Values)
			{
				XmlSchema xmlSchema2 = (XmlSchema)obj4;
				if (!arrayList.Contains(xmlSchema2))
				{
					this.Remove(xmlSchema2, false);
				}
			}
			foreach (object obj5 in otherSet.GlobalElements.Values)
			{
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)obj5;
				if (!arrayList.Contains((XmlSchema)xmlSchemaElement2.Parent))
				{
					this.elements.Remove(xmlSchemaElement2.QualifiedName);
				}
			}
			foreach (object obj6 in otherSet.GlobalAttributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)obj6;
				if (!arrayList.Contains((XmlSchema)xmlSchemaAttribute2.Parent))
				{
					this.attributes.Remove(xmlSchemaAttribute2.QualifiedName);
				}
			}
			foreach (object obj7 in otherSet.GlobalTypes.Values)
			{
				XmlSchemaType xmlSchemaType2 = (XmlSchemaType)obj7;
				if (!arrayList.Contains((XmlSchema)xmlSchemaType2.Parent))
				{
					this.schemaTypes.Remove(xmlSchemaType2.QualifiedName);
				}
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000D179C File Offset: 0x000CF99C
		internal SchemaInfo CompiledInfo
		{
			get
			{
				return this.cachedCompiledInfo;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x000D17A4 File Offset: 0x000CF9A4
		internal XmlReaderSettings ReaderSettings
		{
			get
			{
				return this.readerSettings;
			}
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000D17AC File Offset: 0x000CF9AC
		internal XmlResolver GetResolver()
		{
			return this.readerSettings.GetXmlResolver_CheckConfig();
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000D17B9 File Offset: 0x000CF9B9
		internal ValidationEventHandler GetEventHandler()
		{
			return this.eventHandler;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000D17C1 File Offset: 0x000CF9C1
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

		// Token: 0x060027BE RID: 10174 RVA: 0x000D17F4 File Offset: 0x000CF9F4
		internal bool IsSchemaLoaded(Uri schemaUri, string targetNamespace, out XmlSchema schema)
		{
			schema = null;
			if (targetNamespace == null)
			{
				targetNamespace = string.Empty;
			}
			if (this.GetSchemaByUri(schemaUri, out schema))
			{
				if (!this.schemas.ContainsKey(schema.SchemaId) || (targetNamespace.Length != 0 && !(targetNamespace == schema.TargetNamespace)))
				{
					if (schema.TargetNamespace == null)
					{
						XmlSchema xmlSchema = this.FindSchemaByNSAndUrl(schemaUri, targetNamespace, null);
						if (xmlSchema != null && this.schemas.ContainsKey(xmlSchema.SchemaId))
						{
							schema = xmlSchema;
						}
						else
						{
							schema = this.Add(targetNamespace, schema);
						}
					}
					else if (targetNamespace.Length != 0 && targetNamespace != schema.TargetNamespace)
					{
						this.SendValidationEvent(new XmlSchemaException("Sch_MismatchTargetNamespaceEx", new string[]
						{
							targetNamespace,
							schema.TargetNamespace
						}), XmlSeverityType.Error);
						schema = null;
					}
					else
					{
						this.AddSchemaToSet(schema);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x000D18DD File Offset: 0x000CFADD
		internal bool GetSchemaByUri(Uri schemaUri, out XmlSchema schema)
		{
			schema = null;
			if (schemaUri == null || schemaUri.OriginalString.Length == 0)
			{
				return false;
			}
			schema = (XmlSchema)this.schemaLocations[schemaUri];
			return schema != null;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000D1914 File Offset: 0x000CFB14
		internal string GetTargetNamespace(XmlSchema schema)
		{
			if (schema.TargetNamespace != null)
			{
				return schema.TargetNamespace;
			}
			return string.Empty;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x000D192A File Offset: 0x000CFB2A
		internal SortedList SortedSchemas
		{
			get
			{
				return this.schemas;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x000D1932 File Offset: 0x000CFB32
		internal bool CompileAll
		{
			get
			{
				return this.compileAll;
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000D193C File Offset: 0x000CFB3C
		private void RemoveSchemaFromCaches(XmlSchema schema)
		{
			List<XmlSchema> list = new List<XmlSchema>();
			schema.GetExternalSchemasList(list, schema);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].BaseUri != null && list[i].BaseUri.OriginalString.Length != 0)
				{
					this.schemaLocations.Remove(list[i].BaseUri);
				}
				ICollection keys = this.chameleonSchemas.Keys;
				ArrayList arrayList = new ArrayList();
				foreach (object obj in keys)
				{
					ChameleonKey chameleonKey = (ChameleonKey)obj;
					if (chameleonKey.chameleonLocation.Equals(list[i].BaseUri) && (chameleonKey.originalSchema == null || chameleonKey.originalSchema == list[i]))
					{
						arrayList.Add(chameleonKey);
					}
				}
				for (int j = 0; j < arrayList.Count; j++)
				{
					this.chameleonSchemas.Remove(arrayList[j]);
				}
			}
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000D1A70 File Offset: 0x000CFC70
		private void RemoveSchemaFromGlobalTables(XmlSchema schema)
		{
			if (this.schemas.Count == 0)
			{
				return;
			}
			this.VerifyTables();
			foreach (object obj in schema.Elements.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				XmlSchemaElement xmlSchemaElement2 = (XmlSchemaElement)this.elements[xmlSchemaElement.QualifiedName];
				if (xmlSchemaElement2 == xmlSchemaElement)
				{
					this.elements.Remove(xmlSchemaElement.QualifiedName);
				}
			}
			foreach (object obj2 in schema.Attributes.Values)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)obj2;
				XmlSchemaAttribute xmlSchemaAttribute2 = (XmlSchemaAttribute)this.attributes[xmlSchemaAttribute.QualifiedName];
				if (xmlSchemaAttribute2 == xmlSchemaAttribute)
				{
					this.attributes.Remove(xmlSchemaAttribute.QualifiedName);
				}
			}
			foreach (object obj3 in schema.SchemaTypes.Values)
			{
				XmlSchemaType xmlSchemaType = (XmlSchemaType)obj3;
				XmlSchemaType xmlSchemaType2 = (XmlSchemaType)this.schemaTypes[xmlSchemaType.QualifiedName];
				if (xmlSchemaType2 == xmlSchemaType)
				{
					this.schemaTypes.Remove(xmlSchemaType.QualifiedName);
				}
			}
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x000D1C00 File Offset: 0x000CFE00
		private bool AddToTable(XmlSchemaObjectTable table, XmlQualifiedName qname, XmlSchemaObject item)
		{
			if (qname.Name.Length == 0)
			{
				return true;
			}
			XmlSchemaObject xmlSchemaObject = table[qname];
			if (xmlSchemaObject == null)
			{
				table.Add(qname, item);
				return true;
			}
			if (xmlSchemaObject == item || xmlSchemaObject.SourceUri == item.SourceUri)
			{
				return true;
			}
			string res = string.Empty;
			if (item is XmlSchemaComplexType)
			{
				res = "Sch_DupComplexType";
			}
			else if (item is XmlSchemaSimpleType)
			{
				res = "Sch_DupSimpleType";
			}
			else if (item is XmlSchemaElement)
			{
				res = "Sch_DupGlobalElement";
			}
			else if (item is XmlSchemaAttribute)
			{
				if (qname.Namespace == "http://www.w3.org/XML/1998/namespace")
				{
					XmlSchema buildInSchema = Preprocessor.GetBuildInSchema();
					XmlSchemaObject xmlSchemaObject2 = buildInSchema.Attributes[qname];
					if (xmlSchemaObject == xmlSchemaObject2)
					{
						table.Insert(qname, item);
						return true;
					}
					if (item == xmlSchemaObject2)
					{
						return true;
					}
				}
				res = "Sch_DupGlobalAttribute";
			}
			this.SendValidationEvent(new XmlSchemaException(res, qname.ToString()), XmlSeverityType.Error);
			return false;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000D1CE0 File Offset: 0x000CFEE0
		private void VerifyTables()
		{
			if (this.elements == null)
			{
				this.elements = new XmlSchemaObjectTable();
			}
			if (this.attributes == null)
			{
				this.attributes = new XmlSchemaObjectTable();
			}
			if (this.schemaTypes == null)
			{
				this.schemaTypes = new XmlSchemaObjectTable();
			}
			if (this.substitutionGroups == null)
			{
				this.substitutionGroups = new XmlSchemaObjectTable();
			}
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000D1D39 File Offset: 0x000CFF39
		private void InternalValidationCallback(object sender, ValidationEventArgs e)
		{
			if (e.Severity == XmlSeverityType.Error)
			{
				throw e.Exception;
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000D1D4A File Offset: 0x000CFF4A
		private void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandler != null)
			{
				this.eventHandler(this, new ValidationEventArgs(e, severity));
				return;
			}
			throw e;
		}

		// Token: 0x04001131 RID: 4401
		private XmlNameTable nameTable;

		// Token: 0x04001132 RID: 4402
		private SchemaNames schemaNames;

		// Token: 0x04001133 RID: 4403
		private SortedList schemas;

		// Token: 0x04001134 RID: 4404
		private ValidationEventHandler internalEventHandler;

		// Token: 0x04001135 RID: 4405
		private ValidationEventHandler eventHandler;

		// Token: 0x04001136 RID: 4406
		private bool isCompiled;

		// Token: 0x04001137 RID: 4407
		private Hashtable schemaLocations;

		// Token: 0x04001138 RID: 4408
		private Hashtable chameleonSchemas;

		// Token: 0x04001139 RID: 4409
		private Hashtable targetNamespaces;

		// Token: 0x0400113A RID: 4410
		private bool compileAll;

		// Token: 0x0400113B RID: 4411
		private SchemaInfo cachedCompiledInfo;

		// Token: 0x0400113C RID: 4412
		private XmlReaderSettings readerSettings;

		// Token: 0x0400113D RID: 4413
		private XmlSchema schemaForSchema;

		// Token: 0x0400113E RID: 4414
		private XmlSchemaCompilationSettings compilationSettings;

		// Token: 0x0400113F RID: 4415
		internal XmlSchemaObjectTable elements;

		// Token: 0x04001140 RID: 4416
		internal XmlSchemaObjectTable attributes;

		// Token: 0x04001141 RID: 4417
		internal XmlSchemaObjectTable schemaTypes;

		// Token: 0x04001142 RID: 4418
		internal XmlSchemaObjectTable substitutionGroups;

		// Token: 0x04001143 RID: 4419
		private XmlSchemaObjectTable typeExtensions;

		// Token: 0x04001144 RID: 4420
		private object internalSyncObject;
	}
}
