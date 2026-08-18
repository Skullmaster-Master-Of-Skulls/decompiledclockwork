using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Configuration;
using System.Security.Permissions;
using System.Xml.Serialization.Advanced;
using System.Xml.Serialization.Configuration;
using Microsoft.CSharp;

namespace System.Xml.Serialization
{
	// Token: 0x020002E1 RID: 737
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class SchemaImporter
	{
		// Token: 0x06002277 RID: 8823 RVA: 0x000A1174 File Offset: 0x000A0174
		internal SchemaImporter(XmlSchemas schemas, CodeGenerationOptions options, CodeDomProvider codeProvider, ImportContext context)
		{
			if (!schemas.Contains("http://www.w3.org/2001/XMLSchema"))
			{
				schemas.AddReference(XmlSchemas.XsdSchema);
				schemas.SchemaSet.Add(XmlSchemas.XsdSchema);
			}
			if (!schemas.Contains("http://www.w3.org/XML/1998/namespace"))
			{
				schemas.AddReference(XmlSchemas.XmlSchema);
				schemas.SchemaSet.Add(XmlSchemas.XmlSchema);
			}
			this.schemas = schemas;
			this.options = options;
			this.codeProvider = codeProvider;
			this.context = context;
			this.Schemas.SetCache(this.Context.Cache, this.Context.ShareTypes);
			SchemaImporterExtensionsSection schemaImporterExtensionsSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.SchemaImporterExtensionsSectionPath) as SchemaImporterExtensionsSection;
			if (schemaImporterExtensionsSection != null)
			{
				this.extensions = schemaImporterExtensionsSection.SchemaImporterExtensionsInternal;
				return;
			}
			this.extensions = new SchemaImporterExtensionCollection();
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x000A1242 File Offset: 0x000A0242
		internal ImportContext Context
		{
			get
			{
				if (this.context == null)
				{
					this.context = new ImportContext();
				}
				return this.context;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x000A125D File Offset: 0x000A025D
		internal CodeDomProvider CodeProvider
		{
			get
			{
				if (this.codeProvider == null)
				{
					this.codeProvider = new CSharpCodeProvider();
				}
				return this.codeProvider;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x000A1278 File Offset: 0x000A0278
		public SchemaImporterExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new SchemaImporterExtensionCollection();
				}
				return this.extensions;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x000A1293 File Offset: 0x000A0293
		internal Hashtable ImportedElements
		{
			get
			{
				return this.Context.Elements;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000A12A0 File Offset: 0x000A02A0
		internal Hashtable ImportedMappings
		{
			get
			{
				return this.Context.Mappings;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x000A12AD File Offset: 0x000A02AD
		internal CodeIdentifiers TypeIdentifiers
		{
			get
			{
				return this.Context.TypeIdentifiers;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x000A12BA File Offset: 0x000A02BA
		internal XmlSchemas Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemas();
				}
				return this.schemas;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000A12D5 File Offset: 0x000A02D5
		internal TypeScope Scope
		{
			get
			{
				if (this.scope == null)
				{
					this.scope = new TypeScope();
				}
				return this.scope;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x000A12F0 File Offset: 0x000A02F0
		internal NameTable GroupsInUse
		{
			get
			{
				if (this.groupsInUse == null)
				{
					this.groupsInUse = new NameTable();
				}
				return this.groupsInUse;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x000A130B File Offset: 0x000A030B
		internal NameTable TypesInUse
		{
			get
			{
				if (this.typesInUse == null)
				{
					this.typesInUse = new NameTable();
				}
				return this.typesInUse;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x000A1326 File Offset: 0x000A0326
		internal CodeGenerationOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x000A1330 File Offset: 0x000A0330
		internal void MakeDerived(StructMapping structMapping, Type baseType, bool baseTypeCanBeIndirect)
		{
			structMapping.ReferencedByTopLevelElement = true;
			if (baseType != null)
			{
				TypeDesc typeDesc = this.Scope.GetTypeDesc(baseType);
				if (typeDesc != null)
				{
					TypeDesc typeDesc2 = structMapping.TypeDesc;
					if (baseTypeCanBeIndirect)
					{
						while (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
						{
							typeDesc2 = typeDesc2.BaseTypeDesc;
						}
					}
					if (typeDesc2.BaseTypeDesc != null && typeDesc2.BaseTypeDesc != typeDesc)
					{
						throw new InvalidOperationException(Res.GetString("XmlInvalidBaseType", new object[]
						{
							structMapping.TypeDesc.FullName,
							baseType.FullName,
							typeDesc2.BaseTypeDesc.FullName
						}));
					}
					typeDesc2.BaseTypeDesc = typeDesc;
				}
			}
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000A13D3 File Offset: 0x000A03D3
		internal string GenerateUniqueTypeName(string typeName)
		{
			typeName = CodeIdentifier.MakeValid(typeName);
			return this.TypeIdentifiers.AddUnique(typeName, typeName);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000A13EC File Offset: 0x000A03EC
		private StructMapping CreateRootMapping()
		{
			TypeDesc typeDesc = this.Scope.GetTypeDesc(typeof(object));
			return new StructMapping
			{
				TypeDesc = typeDesc,
				Members = new MemberMapping[0],
				IncludeInSchema = false,
				TypeName = "anyType",
				Namespace = "http://www.w3.org/2001/XMLSchema"
			};
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x000A1446 File Offset: 0x000A0446
		internal StructMapping GetRootMapping()
		{
			if (this.root == null)
			{
				this.root = this.CreateRootMapping();
			}
			return this.root;
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000A1462 File Offset: 0x000A0462
		internal StructMapping ImportRootMapping()
		{
			if (!this.rootImported)
			{
				this.rootImported = true;
				this.ImportDerivedTypes(XmlQualifiedName.Empty);
			}
			return this.GetRootMapping();
		}

		// Token: 0x06002288 RID: 8840
		internal abstract void ImportDerivedTypes(XmlQualifiedName baseName);

		// Token: 0x06002289 RID: 8841 RVA: 0x000A1484 File Offset: 0x000A0484
		internal void AddReference(XmlQualifiedName name, NameTable references, string error)
		{
			if (name.Namespace == "http://www.w3.org/2001/XMLSchema")
			{
				return;
			}
			if (references[name] != null)
			{
				throw new InvalidOperationException(Res.GetString(error, new object[]
				{
					name.Name,
					name.Namespace
				}));
			}
			references[name] = name;
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000A14DB File Offset: 0x000A04DB
		internal void RemoveReference(XmlQualifiedName name, NameTable references)
		{
			references[name] = null;
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000A14E5 File Offset: 0x000A04E5
		internal void AddReservedIdentifiersForDataBinding(CodeIdentifiers scope)
		{
			if ((this.options & CodeGenerationOptions.EnableDataBinding) != CodeGenerationOptions.None)
			{
				scope.AddReserved(CodeExporter.PropertyChangedEvent.Name);
				scope.AddReserved(CodeExporter.RaisePropertyChangedEventMethod.Name);
			}
		}

		// Token: 0x040014C4 RID: 5316
		private XmlSchemas schemas;

		// Token: 0x040014C5 RID: 5317
		private StructMapping root;

		// Token: 0x040014C6 RID: 5318
		private CodeGenerationOptions options;

		// Token: 0x040014C7 RID: 5319
		private CodeDomProvider codeProvider;

		// Token: 0x040014C8 RID: 5320
		private TypeScope scope;

		// Token: 0x040014C9 RID: 5321
		private ImportContext context;

		// Token: 0x040014CA RID: 5322
		private bool rootImported;

		// Token: 0x040014CB RID: 5323
		private NameTable typesInUse;

		// Token: 0x040014CC RID: 5324
		private NameTable groupsInUse;

		// Token: 0x040014CD RID: 5325
		private SchemaImporterExtensionCollection extensions;
	}
}
