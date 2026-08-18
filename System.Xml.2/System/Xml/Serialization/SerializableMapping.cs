using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x02000159 RID: 345
	internal class SerializableMapping : SpecialMapping
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x00068322 File Offset: 0x00066522
		internal SerializableMapping()
		{
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00068331 File Offset: 0x00066531
		internal SerializableMapping(MethodInfo getSchemaMethod, bool any, string ns)
		{
			this.getSchemaMethod = getSchemaMethod;
			this.any = any;
			base.Namespace = ns;
			this.needSchema = (getSchemaMethod != null);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00068362 File Offset: 0x00066562
		internal SerializableMapping(XmlQualifiedName xsiType, XmlSchemaSet schemas)
		{
			this.xsiType = xsiType;
			this.schemas = schemas;
			base.TypeName = xsiType.Name;
			base.Namespace = xsiType.Namespace;
			this.needSchema = false;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000683A0 File Offset: 0x000665A0
		internal void SetBaseMapping(SerializableMapping mapping)
		{
			this.baseMapping = mapping;
			if (this.baseMapping != null)
			{
				this.nextDerivedMapping = this.baseMapping.derivedMappings;
				this.baseMapping.derivedMappings = this;
				if (this == this.nextDerivedMapping)
				{
					throw new InvalidOperationException(Res.GetString("XmlCircularDerivation", new object[]
					{
						base.TypeDesc.FullName
					}));
				}
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00068408 File Offset: 0x00066608
		internal bool IsAny
		{
			get
			{
				if (this.any)
				{
					return true;
				}
				if (this.getSchemaMethod == null)
				{
					return false;
				}
				if (this.needSchema && typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return false;
				}
				this.RetrieveSerializableSchema();
				return this.any;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x00068464 File Offset: 0x00066664
		internal string NamespaceList
		{
			get
			{
				this.RetrieveSerializableSchema();
				if (this.namespaces == null)
				{
					if (this.schemas != null)
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (object obj in this.schemas.Schemas())
						{
							XmlSchema xmlSchema = (XmlSchema)obj;
							if (xmlSchema.TargetNamespace != null && xmlSchema.TargetNamespace.Length > 0)
							{
								if (stringBuilder.Length > 0)
								{
									stringBuilder.Append(" ");
								}
								stringBuilder.Append(xmlSchema.TargetNamespace);
							}
						}
						this.namespaces = stringBuilder.ToString();
					}
					else
					{
						this.namespaces = string.Empty;
					}
				}
				return this.namespaces;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00068534 File Offset: 0x00066734
		internal SerializableMapping DerivedMappings
		{
			get
			{
				return this.derivedMappings;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0006853C File Offset: 0x0006673C
		internal SerializableMapping NextDerivedMapping
		{
			get
			{
				return this.nextDerivedMapping;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x00068544 File Offset: 0x00066744
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x0006854C File Offset: 0x0006674C
		internal SerializableMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00068555 File Offset: 0x00066755
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x0006855D File Offset: 0x0006675D
		internal Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00068566 File Offset: 0x00066766
		internal XmlSchemaSet Schemas
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schemas;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00068574 File Offset: 0x00066774
		internal XmlSchema Schema
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.schema;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00068584 File Offset: 0x00066784
		internal XmlQualifiedName XsiType
		{
			get
			{
				if (!this.needSchema)
				{
					return this.xsiType;
				}
				if (this.getSchemaMethod == null)
				{
					return null;
				}
				if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
				{
					return null;
				}
				this.RetrieveSerializableSchema();
				return this.xsiType;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000685DA File Offset: 0x000667DA
		internal XmlSchemaType XsdType
		{
			get
			{
				this.RetrieveSerializableSchema();
				return this.xsdType;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000685E8 File Offset: 0x000667E8
		internal static void ValidationCallbackWithErrorCode(object sender, ValidationEventArgs args)
		{
			if (args.Severity == XmlSeverityType.Error)
			{
				throw new InvalidOperationException(Res.GetString("XmlSerializableSchemaError", new object[]
				{
					typeof(IXmlSerializable).Name,
					args.Message
				}));
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00068624 File Offset: 0x00066824
		internal void CheckDuplicateElement(XmlSchemaElement element, string elementNs)
		{
			if (element == null)
			{
				return;
			}
			if (element.Parent == null || !(element.Parent is XmlSchema))
			{
				return;
			}
			XmlSchemaObjectTable xmlSchemaObjectTable;
			if (this.Schema != null && this.Schema.TargetNamespace == elementNs)
			{
				XmlSchemas.Preprocess(this.Schema);
				xmlSchemaObjectTable = this.Schema.Elements;
			}
			else
			{
				if (this.Schemas == null)
				{
					return;
				}
				xmlSchemaObjectTable = this.Schemas.GlobalElements;
			}
			foreach (object obj in xmlSchemaObjectTable.Values)
			{
				XmlSchemaElement xmlSchemaElement = (XmlSchemaElement)obj;
				if (xmlSchemaElement.Name == element.Name && xmlSchemaElement.QualifiedName.Namespace == elementNs)
				{
					if (this.Match(xmlSchemaElement, element))
					{
						break;
					}
					throw new InvalidOperationException(Res.GetString("XmlSerializableRootDupName", new object[]
					{
						this.getSchemaMethod.DeclaringType.FullName,
						xmlSchemaElement.Name,
						elementNs
					}));
				}
			}
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00068748 File Offset: 0x00066948
		private bool Match(XmlSchemaElement e1, XmlSchemaElement e2)
		{
			return e1.IsNillable == e2.IsNillable && !(e1.RefName != e2.RefName) && e1.SchemaType == e2.SchemaType && !(e1.SchemaTypeName != e2.SchemaTypeName) && !(e1.MinOccurs != e2.MinOccurs) && !(e1.MaxOccurs != e2.MaxOccurs) && e1.IsAbstract == e2.IsAbstract && !(e1.DefaultValue != e2.DefaultValue) && !(e1.SubstitutionGroup != e2.SubstitutionGroup);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00068804 File Offset: 0x00066A04
		private void RetrieveSerializableSchema()
		{
			if (this.needSchema)
			{
				this.needSchema = false;
				if (this.getSchemaMethod != null)
				{
					if (this.schemas == null)
					{
						this.schemas = new XmlSchemaSet();
					}
					object obj = this.getSchemaMethod.Invoke(null, new object[]
					{
						this.schemas
					});
					this.xsiType = XmlQualifiedName.Empty;
					if (obj != null)
					{
						if (typeof(XmlSchemaType).IsAssignableFrom(this.getSchemaMethod.ReturnType))
						{
							this.xsdType = (XmlSchemaType)obj;
							this.xsiType = this.xsdType.QualifiedName;
						}
						else
						{
							if (!typeof(XmlQualifiedName).IsAssignableFrom(this.getSchemaMethod.ReturnType))
							{
								throw new InvalidOperationException(Res.GetString("XmlGetSchemaMethodReturnType", new object[]
								{
									this.type.Name,
									this.getSchemaMethod.Name,
									typeof(XmlSchemaProviderAttribute).Name,
									typeof(XmlQualifiedName).FullName
								}));
							}
							this.xsiType = (XmlQualifiedName)obj;
							if (this.xsiType.IsEmpty)
							{
								throw new InvalidOperationException(Res.GetString("XmlGetSchemaEmptyTypeName", new object[]
								{
									this.type.FullName,
									this.getSchemaMethod.Name
								}));
							}
						}
					}
					else
					{
						this.any = true;
					}
					this.schemas.ValidationEventHandler += SerializableMapping.ValidationCallbackWithErrorCode;
					this.schemas.Compile();
					if (!this.xsiType.IsEmpty && this.xsiType.Namespace != "http://www.w3.org/2001/XMLSchema")
					{
						ArrayList arrayList = (ArrayList)this.schemas.Schemas(this.xsiType.Namespace);
						if (arrayList.Count == 0)
						{
							throw new InvalidOperationException(Res.GetString("XmlMissingSchema", new object[]
							{
								this.xsiType.Namespace
							}));
						}
						if (arrayList.Count > 1)
						{
							throw new InvalidOperationException(Res.GetString("XmlGetSchemaInclude", new object[]
							{
								this.xsiType.Namespace,
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name
							}));
						}
						XmlSchema xmlSchema = (XmlSchema)arrayList[0];
						if (xmlSchema == null)
						{
							throw new InvalidOperationException(Res.GetString("XmlMissingSchema", new object[]
							{
								this.xsiType.Namespace
							}));
						}
						this.xsdType = (XmlSchemaType)xmlSchema.SchemaTypes[this.xsiType];
						if (this.xsdType == null)
						{
							throw new InvalidOperationException(Res.GetString("XmlGetSchemaTypeMissing", new object[]
							{
								this.getSchemaMethod.DeclaringType.FullName,
								this.getSchemaMethod.Name,
								this.xsiType.Name,
								this.xsiType.Namespace
							}));
						}
						this.xsdType = ((this.xsdType.Redefined != null) ? this.xsdType.Redefined : this.xsdType);
						return;
					}
				}
				else
				{
					IXmlSerializable xmlSerializable = (IXmlSerializable)Activator.CreateInstance(this.type);
					this.schema = xmlSerializable.GetSchema();
					if (this.schema != null && (this.schema.Id == null || this.schema.Id.Length == 0))
					{
						throw new InvalidOperationException(Res.GetString("XmlSerializableNameMissing1", new object[]
						{
							this.type.FullName
						}));
					}
				}
			}
		}

		// Token: 0x04000B09 RID: 2825
		private XmlSchema schema;

		// Token: 0x04000B0A RID: 2826
		private Type type;

		// Token: 0x04000B0B RID: 2827
		private bool needSchema = true;

		// Token: 0x04000B0C RID: 2828
		private MethodInfo getSchemaMethod;

		// Token: 0x04000B0D RID: 2829
		private XmlQualifiedName xsiType;

		// Token: 0x04000B0E RID: 2830
		private XmlSchemaType xsdType;

		// Token: 0x04000B0F RID: 2831
		private XmlSchemaSet schemas;

		// Token: 0x04000B10 RID: 2832
		private bool any;

		// Token: 0x04000B11 RID: 2833
		private string namespaces;

		// Token: 0x04000B12 RID: 2834
		private SerializableMapping baseMapping;

		// Token: 0x04000B13 RID: 2835
		private SerializableMapping derivedMappings;

		// Token: 0x04000B14 RID: 2836
		private SerializableMapping nextDerivedMapping;

		// Token: 0x04000B15 RID: 2837
		private SerializableMapping next;
	}
}
