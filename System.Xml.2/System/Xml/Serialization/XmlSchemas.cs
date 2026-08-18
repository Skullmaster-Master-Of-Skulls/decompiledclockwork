using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020001A5 RID: 421
	public class XmlSchemas : CollectionBase, IEnumerable<XmlSchema>, IEnumerable
	{
		// Token: 0x17000611 RID: 1553
		public XmlSchema this[int index]
		{
			get
			{
				return (XmlSchema)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		public XmlSchema this[string ns]
		{
			get
			{
				IList list = (IList)this.SchemaSet.Schemas(ns);
				if (list.Count == 0)
				{
					return null;
				}
				if (list.Count == 1)
				{
					return (XmlSchema)list[0];
				}
				throw new InvalidOperationException(Res.GetString("XmlSchemaDuplicateNamespace", new object[]
				{
					ns
				}));
			}
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0008268C File Offset: 0x0008088C
		public IList GetSchemas(string ns)
		{
			return (IList)this.SchemaSet.Schemas(ns);
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0008269F File Offset: 0x0008089F
		internal SchemaObjectCache Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new SchemaObjectCache();
				}
				return this.cache;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000826BA File Offset: 0x000808BA
		internal Hashtable MergedSchemas
		{
			get
			{
				if (this.mergedSchemas == null)
				{
					this.mergedSchemas = new Hashtable();
				}
				return this.mergedSchemas;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000826D5 File Offset: 0x000808D5
		internal Hashtable References
		{
			get
			{
				if (this.references == null)
				{
					this.references = new Hashtable();
				}
				return this.references;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x000826F0 File Offset: 0x000808F0
		internal XmlSchemaSet SchemaSet
		{
			get
			{
				if (this.schemaSet == null)
				{
					this.schemaSet = new XmlSchemaSet();
					this.schemaSet.XmlResolver = null;
					this.schemaSet.ValidationEventHandler += XmlSchemas.IgnoreCompileErrors;
				}
				return this.schemaSet;
			}
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0008272E File Offset: 0x0008092E
		internal int Add(XmlSchema schema, bool delay)
		{
			if (delay)
			{
				if (this.delayedSchemas[schema] == null)
				{
					this.delayedSchemas.Add(schema, schema);
				}
				return -1;
			}
			return this.Add(schema);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00082757 File Offset: 0x00080957
		public int Add(XmlSchema schema)
		{
			if (base.List.Contains(schema))
			{
				return base.List.IndexOf(schema);
			}
			return base.List.Add(schema);
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x00082780 File Offset: 0x00080980
		public int Add(XmlSchema schema, Uri baseUri)
		{
			if (base.List.Contains(schema))
			{
				return base.List.IndexOf(schema);
			}
			if (baseUri != null)
			{
				schema.BaseUri = baseUri;
			}
			return base.List.Add(schema);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000827BC File Offset: 0x000809BC
		public void Add(XmlSchemas schemas)
		{
			foreach (object obj in schemas)
			{
				XmlSchema schema = (XmlSchema)obj;
				this.Add(schema);
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x00082814 File Offset: 0x00080A14
		public void AddReference(XmlSchema schema)
		{
			this.References[schema] = schema;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x00082823 File Offset: 0x00080A23
		public void Insert(int index, XmlSchema schema)
		{
			base.List.Insert(index, schema);
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x00082832 File Offset: 0x00080A32
		public int IndexOf(XmlSchema schema)
		{
			return base.List.IndexOf(schema);
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00082840 File Offset: 0x00080A40
		public bool Contains(XmlSchema schema)
		{
			return base.List.Contains(schema);
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0008284E File Offset: 0x00080A4E
		public bool Contains(string targetNamespace)
		{
			return this.SchemaSet.Contains(targetNamespace);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x0008285C File Offset: 0x00080A5C
		public void Remove(XmlSchema schema)
		{
			base.List.Remove(schema);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0008286A File Offset: 0x00080A6A
		public void CopyTo(XmlSchema[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00082879 File Offset: 0x00080A79
		protected override void OnInsert(int index, object value)
		{
			this.AddName((XmlSchema)value);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x00082887 File Offset: 0x00080A87
		protected override void OnRemove(int index, object value)
		{
			this.RemoveName((XmlSchema)value);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x00082895 File Offset: 0x00080A95
		protected override void OnClear()
		{
			this.schemaSet = null;
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x0008289E File Offset: 0x00080A9E
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			this.RemoveName((XmlSchema)oldValue);
			this.AddName((XmlSchema)newValue);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x000828B8 File Offset: 0x00080AB8
		private void AddName(XmlSchema schema)
		{
			if (this.isCompiled)
			{
				throw new InvalidOperationException(Res.GetString("XmlSchemaCompiled"));
			}
			if (this.SchemaSet.Contains(schema))
			{
				this.SchemaSet.Reprocess(schema);
				return;
			}
			this.Prepare(schema);
			this.SchemaSet.Add(schema);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00082910 File Offset: 0x00080B10
		private void Prepare(XmlSchema schema)
		{
			ArrayList arrayList = new ArrayList();
			string targetNamespace = schema.TargetNamespace;
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal is XmlSchemaImport && targetNamespace == ((XmlSchemaImport)xmlSchemaExternal).Namespace)
				{
					arrayList.Add(xmlSchemaExternal);
				}
			}
			foreach (object obj in arrayList)
			{
				XmlSchemaObject item = (XmlSchemaObject)obj;
				schema.Includes.Remove(item);
			}
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x000829E8 File Offset: 0x00080BE8
		private void RemoveName(XmlSchema schema)
		{
			this.SchemaSet.Remove(schema);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x000829F7 File Offset: 0x00080BF7
		public object Find(XmlQualifiedName name, Type type)
		{
			return this.Find(name, type, true);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x00082A04 File Offset: 0x00080C04
		internal object Find(XmlQualifiedName name, Type type, bool checkCache)
		{
			if (!this.IsCompiled)
			{
				foreach (object obj in base.List)
				{
					XmlSchema schema = (XmlSchema)obj;
					XmlSchemas.Preprocess(schema);
				}
			}
			IList list = (IList)this.SchemaSet.Schemas(name.Namespace);
			if (list == null)
			{
				return null;
			}
			foreach (object obj2 in list)
			{
				XmlSchema xmlSchema = (XmlSchema)obj2;
				XmlSchemas.Preprocess(xmlSchema);
				XmlSchemaObject xmlSchemaObject = null;
				if (typeof(XmlSchemaType).IsAssignableFrom(type))
				{
					xmlSchemaObject = xmlSchema.SchemaTypes[name];
					if (xmlSchemaObject == null)
					{
						continue;
					}
					if (!type.IsAssignableFrom(xmlSchemaObject.GetType()))
					{
						continue;
					}
				}
				else if (type == typeof(XmlSchemaGroup))
				{
					xmlSchemaObject = xmlSchema.Groups[name];
				}
				else if (type == typeof(XmlSchemaAttributeGroup))
				{
					xmlSchemaObject = xmlSchema.AttributeGroups[name];
				}
				else if (type == typeof(XmlSchemaElement))
				{
					xmlSchemaObject = xmlSchema.Elements[name];
				}
				else if (type == typeof(XmlSchemaAttribute))
				{
					xmlSchemaObject = xmlSchema.Attributes[name];
				}
				else if (type == typeof(XmlSchemaNotation))
				{
					xmlSchemaObject = xmlSchema.Notations[name];
				}
				if (xmlSchemaObject != null && this.shareTypes && checkCache && !this.IsReference(xmlSchemaObject))
				{
					xmlSchemaObject = this.Cache.AddItem(xmlSchemaObject, name, this);
				}
				if (xmlSchemaObject != null)
				{
					return xmlSchemaObject;
				}
			}
			return null;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00082C14 File Offset: 0x00080E14
		IEnumerator<XmlSchema> IEnumerable<XmlSchema>.GetEnumerator()
		{
			return new XmlSchemaEnumerator(this);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00082C1C File Offset: 0x00080E1C
		internal static void Preprocess(XmlSchema schema)
		{
			if (!schema.IsPreprocessed)
			{
				try
				{
					XmlNameTable nameTable = new NameTable();
					new Preprocessor(nameTable, new SchemaNames(nameTable), null)
					{
						SchemaLocations = new Hashtable()
					}.Execute(schema, schema.TargetNamespace, false);
				}
				catch (XmlSchemaException ex)
				{
					throw XmlSchemas.CreateValidationException(ex, ex.Message);
				}
			}
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00082C80 File Offset: 0x00080E80
		public static bool IsDataSet(XmlSchema schema)
		{
			foreach (XmlSchemaObject xmlSchemaObject in schema.Items)
			{
				if (xmlSchemaObject is XmlSchemaElement)
				{
					XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)xmlSchemaObject;
					if (xmlSchemaElement.UnhandledAttributes != null)
					{
						foreach (XmlAttribute xmlAttribute in xmlSchemaElement.UnhandledAttributes)
						{
							if (xmlAttribute.LocalName == "IsDataSet" && xmlAttribute.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata" && (xmlAttribute.Value == "True" || xmlAttribute.Value == "true" || xmlAttribute.Value == "1"))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00082D7C File Offset: 0x00080F7C
		private void Merge(XmlSchema schema)
		{
			if (this.MergedSchemas[schema] != null)
			{
				return;
			}
			IList list = (IList)this.SchemaSet.Schemas(schema.TargetNamespace);
			if (list != null && list.Count > 0)
			{
				this.MergedSchemas.Add(schema, schema);
				this.Merge(list, schema);
				return;
			}
			this.Add(schema);
			this.MergedSchemas.Add(schema, schema);
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00082DE8 File Offset: 0x00080FE8
		private void AddImport(IList schemas, string ns)
		{
			foreach (object obj in schemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				bool flag = true;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Includes)
				{
					XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
					if (xmlSchemaExternal is XmlSchemaImport && ((XmlSchemaImport)xmlSchemaExternal).Namespace == ns)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
					xmlSchemaImport.Namespace = ns;
					xmlSchema.Includes.Add(xmlSchemaImport);
				}
			}
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00082EC8 File Offset: 0x000810C8
		private void Merge(IList originals, XmlSchema schema)
		{
			foreach (object obj in originals)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (schema == xmlSchema)
				{
					return;
				}
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaExternal xmlSchemaExternal = (XmlSchemaExternal)xmlSchemaObject;
				if (xmlSchemaExternal is XmlSchemaImport)
				{
					xmlSchemaExternal.SchemaLocation = null;
					if (xmlSchemaExternal.Schema != null)
					{
						this.Merge(xmlSchemaExternal.Schema);
					}
					else
					{
						this.AddImport(originals, ((XmlSchemaImport)xmlSchemaExternal).Namespace);
					}
				}
				else if (xmlSchemaExternal.Schema == null)
				{
					if (xmlSchemaExternal.SchemaLocation != null)
					{
						throw new InvalidOperationException(Res.GetString("XmlSchemaIncludeLocation", new object[]
						{
							base.GetType().Name,
							xmlSchemaExternal.SchemaLocation
						}));
					}
				}
				else
				{
					xmlSchemaExternal.SchemaLocation = null;
					this.Merge(originals, xmlSchemaExternal.Schema);
				}
			}
			bool[] array = new bool[schema.Items.Count];
			int num = 0;
			for (int i = 0; i < schema.Items.Count; i++)
			{
				XmlSchemaObject xmlSchemaObject2 = schema.Items[i];
				XmlSchemaObject xmlSchemaObject3 = this.Find(xmlSchemaObject2, originals);
				if (xmlSchemaObject3 != null)
				{
					if (!this.Cache.Match(xmlSchemaObject3, xmlSchemaObject2, this.shareTypes))
					{
						throw new InvalidOperationException(XmlSchemas.MergeFailedMessage(xmlSchemaObject2, xmlSchemaObject3, schema.TargetNamespace));
					}
					array[i] = true;
					num++;
				}
			}
			if (num != schema.Items.Count)
			{
				XmlSchema xmlSchema2 = (XmlSchema)originals[0];
				for (int j = 0; j < schema.Items.Count; j++)
				{
					if (!array[j])
					{
						xmlSchema2.Items.Add(schema.Items[j]);
					}
				}
				xmlSchema2.IsPreprocessed = false;
				XmlSchemas.Preprocess(xmlSchema2);
			}
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000830EC File Offset: 0x000812EC
		private static string ItemName(XmlSchemaObject o)
		{
			if (o is XmlSchemaNotation)
			{
				return ((XmlSchemaNotation)o).Name;
			}
			if (o is XmlSchemaGroup)
			{
				return ((XmlSchemaGroup)o).Name;
			}
			if (o is XmlSchemaElement)
			{
				return ((XmlSchemaElement)o).Name;
			}
			if (o is XmlSchemaType)
			{
				return ((XmlSchemaType)o).Name;
			}
			if (o is XmlSchemaAttributeGroup)
			{
				return ((XmlSchemaAttributeGroup)o).Name;
			}
			if (o is XmlSchemaAttribute)
			{
				return ((XmlSchemaAttribute)o).Name;
			}
			return null;
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x00083174 File Offset: 0x00081374
		internal static XmlQualifiedName GetParentName(XmlSchemaObject item)
		{
			while (item.Parent != null)
			{
				if (item.Parent is XmlSchemaType)
				{
					XmlSchemaType xmlSchemaType = (XmlSchemaType)item.Parent;
					if (xmlSchemaType.Name != null && xmlSchemaType.Name.Length != 0)
					{
						return xmlSchemaType.QualifiedName;
					}
				}
				item = item.Parent;
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x000831D0 File Offset: 0x000813D0
		private static string GetSchemaItem(XmlSchemaObject o, string ns, string details)
		{
			if (o == null)
			{
				return null;
			}
			while (o.Parent != null && !(o.Parent is XmlSchema))
			{
				o = o.Parent;
			}
			if (ns == null || ns.Length == 0)
			{
				XmlSchemaObject xmlSchemaObject = o;
				while (xmlSchemaObject.Parent != null)
				{
					xmlSchemaObject = xmlSchemaObject.Parent;
				}
				if (xmlSchemaObject is XmlSchema)
				{
					ns = ((XmlSchema)xmlSchemaObject).TargetNamespace;
				}
			}
			string @string;
			if (o is XmlSchemaNotation)
			{
				@string = Res.GetString("XmlSchemaNamedItem", new object[]
				{
					ns,
					"notation",
					((XmlSchemaNotation)o).Name,
					details
				});
			}
			else if (o is XmlSchemaGroup)
			{
				@string = Res.GetString("XmlSchemaNamedItem", new object[]
				{
					ns,
					"group",
					((XmlSchemaGroup)o).Name,
					details
				});
			}
			else if (o is XmlSchemaElement)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)o;
				if (xmlSchemaElement.Name == null || xmlSchemaElement.Name.Length == 0)
				{
					XmlQualifiedName parentName = XmlSchemas.GetParentName(o);
					@string = Res.GetString("XmlSchemaElementReference", new object[]
					{
						xmlSchemaElement.RefName.ToString(),
						parentName.Name,
						parentName.Namespace
					});
				}
				else
				{
					@string = Res.GetString("XmlSchemaNamedItem", new object[]
					{
						ns,
						"element",
						xmlSchemaElement.Name,
						details
					});
				}
			}
			else if (o is XmlSchemaType)
			{
				string name = "XmlSchemaNamedItem";
				object[] array = new object[4];
				array[0] = ns;
				array[1] = ((o.GetType() == typeof(XmlSchemaSimpleType)) ? "simpleType" : "complexType");
				array[2] = ((XmlSchemaType)o).Name;
				@string = Res.GetString(name, array);
			}
			else if (o is XmlSchemaAttributeGroup)
			{
				@string = Res.GetString("XmlSchemaNamedItem", new object[]
				{
					ns,
					"attributeGroup",
					((XmlSchemaAttributeGroup)o).Name,
					details
				});
			}
			else if (o is XmlSchemaAttribute)
			{
				XmlSchemaAttribute xmlSchemaAttribute = (XmlSchemaAttribute)o;
				if (xmlSchemaAttribute.Name == null || xmlSchemaAttribute.Name.Length == 0)
				{
					XmlQualifiedName parentName2 = XmlSchemas.GetParentName(o);
					return Res.GetString("XmlSchemaAttributeReference", new object[]
					{
						xmlSchemaAttribute.RefName.ToString(),
						parentName2.Name,
						parentName2.Namespace
					});
				}
				@string = Res.GetString("XmlSchemaNamedItem", new object[]
				{
					ns,
					"attribute",
					xmlSchemaAttribute.Name,
					details
				});
			}
			else if (o is XmlSchemaContent)
			{
				XmlQualifiedName parentName3 = XmlSchemas.GetParentName(o);
				string name2 = "XmlSchemaContentDef";
				object[] array2 = new object[3];
				array2[0] = parentName3.Name;
				array2[1] = parentName3.Namespace;
				@string = Res.GetString(name2, array2);
			}
			else if (o is XmlSchemaExternal)
			{
				string text = (o is XmlSchemaImport) ? "import" : ((o is XmlSchemaInclude) ? "include" : ((o is XmlSchemaRedefine) ? "redefine" : o.GetType().Name));
				@string = Res.GetString("XmlSchemaItem", new object[]
				{
					ns,
					text,
					details
				});
			}
			else if (o is XmlSchema)
			{
				@string = Res.GetString("XmlSchema", new object[]
				{
					ns,
					details
				});
			}
			else
			{
				@string = Res.GetString("XmlSchemaNamedItem", new object[]
				{
					ns,
					o.GetType().Name,
					null,
					details
				});
			}
			return @string;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00083550 File Offset: 0x00081750
		private static string Dump(XmlSchemaObject o)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.OmitXmlDeclaration = true;
			xmlWriterSettings.Indent = true;
			XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings);
			XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
			xmlSerializerNamespaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
			xmlSerializer.Serialize(xmlWriter, o, xmlSerializerNamespaces);
			return stringWriter.ToString();
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x000835B8 File Offset: 0x000817B8
		private static string MergeFailedMessage(XmlSchemaObject src, XmlSchemaObject dest, string ns)
		{
			string str = Res.GetString("XmlSerializableMergeItem", new object[]
			{
				ns,
				XmlSchemas.GetSchemaItem(src, ns, null)
			});
			str = str + "\r\n" + XmlSchemas.Dump(src);
			return str + "\r\n" + XmlSchemas.Dump(dest);
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0008360C File Offset: 0x0008180C
		internal XmlSchemaObject Find(XmlSchemaObject o, IList originals)
		{
			string text = XmlSchemas.ItemName(o);
			if (text == null)
			{
				return null;
			}
			Type type = o.GetType();
			foreach (object obj in originals)
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				foreach (XmlSchemaObject xmlSchemaObject in xmlSchema.Items)
				{
					if (xmlSchemaObject.GetType() == type && text == XmlSchemas.ItemName(xmlSchemaObject))
					{
						return xmlSchemaObject;
					}
				}
			}
			return null;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x000836DC File Offset: 0x000818DC
		public bool IsCompiled
		{
			get
			{
				return this.isCompiled;
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000836E4 File Offset: 0x000818E4
		public void Compile(ValidationEventHandler handler, bool fullCompile)
		{
			if (this.isCompiled)
			{
				return;
			}
			foreach (object obj in this.delayedSchemas.Values)
			{
				XmlSchema schema = (XmlSchema)obj;
				this.Merge(schema);
			}
			this.delayedSchemas.Clear();
			if (fullCompile)
			{
				this.schemaSet = new XmlSchemaSet();
				this.schemaSet.XmlResolver = null;
				this.schemaSet.ValidationEventHandler += handler;
				foreach (object obj2 in this.References.Values)
				{
					XmlSchema schema2 = (XmlSchema)obj2;
					this.schemaSet.Add(schema2);
				}
				int num = this.schemaSet.Count;
				foreach (object obj3 in base.List)
				{
					XmlSchema schema3 = (XmlSchema)obj3;
					if (!this.SchemaSet.Contains(schema3))
					{
						this.schemaSet.Add(schema3);
						num++;
					}
				}
				if (!this.SchemaSet.Contains("http://www.w3.org/2001/XMLSchema"))
				{
					this.AddReference(XmlSchemas.XsdSchema);
					this.schemaSet.Add(XmlSchemas.XsdSchema);
					num++;
				}
				if (!this.SchemaSet.Contains("http://www.w3.org/XML/1998/namespace"))
				{
					this.AddReference(XmlSchemas.XmlSchema);
					this.schemaSet.Add(XmlSchemas.XmlSchema);
					num++;
				}
				this.schemaSet.Compile();
				this.schemaSet.ValidationEventHandler -= handler;
				this.isCompiled = (this.schemaSet.IsCompiled && num == this.schemaSet.Count);
				return;
			}
			try
			{
				XmlNameTable nameTable = new NameTable();
				Preprocessor preprocessor = new Preprocessor(nameTable, new SchemaNames(nameTable), null);
				preprocessor.XmlResolver = null;
				preprocessor.SchemaLocations = new Hashtable();
				preprocessor.ChameleonSchemas = new Hashtable();
				foreach (object obj4 in this.SchemaSet.Schemas())
				{
					XmlSchema xmlSchema = (XmlSchema)obj4;
					preprocessor.Execute(xmlSchema, xmlSchema.TargetNamespace, true);
				}
			}
			catch (XmlSchemaException ex)
			{
				throw XmlSchemas.CreateValidationException(ex, ex.Message);
			}
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000839A0 File Offset: 0x00081BA0
		internal static Exception CreateValidationException(XmlSchemaException exception, string message)
		{
			XmlSchemaObject xmlSchemaObject = exception.SourceSchemaObject;
			if (exception.LineNumber == 0 && exception.LinePosition == 0)
			{
				throw new InvalidOperationException(XmlSchemas.GetSchemaItem(xmlSchemaObject, null, message), exception);
			}
			string text = null;
			if (xmlSchemaObject != null)
			{
				while (xmlSchemaObject.Parent != null)
				{
					xmlSchemaObject = xmlSchemaObject.Parent;
				}
				if (xmlSchemaObject is XmlSchema)
				{
					text = ((XmlSchema)xmlSchemaObject).TargetNamespace;
				}
			}
			throw new InvalidOperationException(Res.GetString("XmlSchemaSyntaxErrorDetails", new object[]
			{
				text,
				message,
				exception.LineNumber,
				exception.LinePosition
			}), exception);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00083A37 File Offset: 0x00081C37
		internal static void IgnoreCompileErrors(object sender, ValidationEventArgs args)
		{
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00083A39 File Offset: 0x00081C39
		internal static XmlSchema XsdSchema
		{
			get
			{
				if (XmlSchemas.xsd == null)
				{
					XmlSchemas.xsd = XmlSchemas.CreateFakeXsdSchema("http://www.w3.org/2001/XMLSchema", "schema");
				}
				return XmlSchemas.xsd;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x00083A61 File Offset: 0x00081C61
		internal static XmlSchema XmlSchema
		{
			get
			{
				if (XmlSchemas.xml == null)
				{
					XmlSchemas.xml = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?> \r\n<xs:schema targetNamespace='http://www.w3.org/XML/1998/namespace' xmlns:xs='http://www.w3.org/2001/XMLSchema' xml:lang='en'>\r\n <xs:attribute name='lang' type='xs:language'/>\r\n <xs:attribute name='space'>\r\n  <xs:simpleType>\r\n   <xs:restriction base='xs:NCName'>\r\n    <xs:enumeration value='default'/>\r\n    <xs:enumeration value='preserve'/>\r\n   </xs:restriction>\r\n  </xs:simpleType>\r\n </xs:attribute>\r\n <xs:attribute name='base' type='xs:anyURI'/>\r\n <xs:attribute name='id' type='xs:ID' />\r\n <xs:attributeGroup name='specialAttrs'>\r\n  <xs:attribute ref='xml:base'/>\r\n  <xs:attribute ref='xml:lang'/>\r\n  <xs:attribute ref='xml:space'/>\r\n </xs:attributeGroup>\r\n</xs:schema>"), null);
				}
				return XmlSchemas.xml;
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00083A8C File Offset: 0x00081C8C
		private static XmlSchema CreateFakeXsdSchema(string ns, string name)
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.TargetNamespace = ns;
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = name;
			XmlSchemaComplexType schemaType = new XmlSchemaComplexType();
			xmlSchemaElement.SchemaType = schemaType;
			xmlSchema.Items.Add(xmlSchemaElement);
			return xmlSchema;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00083ACE File Offset: 0x00081CCE
		internal void SetCache(SchemaObjectCache cache, bool shareTypes)
		{
			this.shareTypes = shareTypes;
			this.cache = cache;
			if (shareTypes)
			{
				cache.GenerateSchemaGraph(this);
			}
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00083AE8 File Offset: 0x00081CE8
		internal bool IsReference(XmlSchemaObject type)
		{
			XmlSchemaObject xmlSchemaObject = type;
			while (xmlSchemaObject.Parent != null)
			{
				xmlSchemaObject = xmlSchemaObject.Parent;
			}
			return this.References.Contains(xmlSchemaObject);
		}

		// Token: 0x04000C30 RID: 3120
		private XmlSchemaSet schemaSet;

		// Token: 0x04000C31 RID: 3121
		private Hashtable references;

		// Token: 0x04000C32 RID: 3122
		private SchemaObjectCache cache;

		// Token: 0x04000C33 RID: 3123
		private bool shareTypes;

		// Token: 0x04000C34 RID: 3124
		private Hashtable mergedSchemas;

		// Token: 0x04000C35 RID: 3125
		internal Hashtable delayedSchemas = new Hashtable();

		// Token: 0x04000C36 RID: 3126
		private bool isCompiled;

		// Token: 0x04000C37 RID: 3127
		private static volatile XmlSchema xsd;

		// Token: 0x04000C38 RID: 3128
		private static volatile XmlSchema xml;

		// Token: 0x04000C39 RID: 3129
		internal const string xmlSchema = "<?xml version='1.0' encoding='UTF-8' ?> \r\n<xs:schema targetNamespace='http://www.w3.org/XML/1998/namespace' xmlns:xs='http://www.w3.org/2001/XMLSchema' xml:lang='en'>\r\n <xs:attribute name='lang' type='xs:language'/>\r\n <xs:attribute name='space'>\r\n  <xs:simpleType>\r\n   <xs:restriction base='xs:NCName'>\r\n    <xs:enumeration value='default'/>\r\n    <xs:enumeration value='preserve'/>\r\n   </xs:restriction>\r\n  </xs:simpleType>\r\n </xs:attribute>\r\n <xs:attribute name='base' type='xs:anyURI'/>\r\n <xs:attribute name='id' type='xs:ID' />\r\n <xs:attributeGroup name='specialAttrs'>\r\n  <xs:attribute ref='xml:base'/>\r\n  <xs:attribute ref='xml:lang'/>\r\n  <xs:attribute ref='xml:space'/>\r\n </xs:attributeGroup>\r\n</xs:schema>";
	}
}
