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
	// Token: 0x02000167 RID: 359
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class SchemaImporter
	{
		// Token: 0x0600182E RID: 6190 RVA: 0x00069538 File Offset: 0x00067738
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x00069606 File Offset: 0x00067806
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

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x00069621 File Offset: 0x00067821
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

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x0006963C File Offset: 0x0006783C
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

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x00069657 File Offset: 0x00067857
		internal Hashtable ImportedElements
		{
			get
			{
				return this.Context.Elements;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001833 RID: 6195 RVA: 0x00069664 File Offset: 0x00067864
		internal Hashtable ImportedMappings
		{
			get
			{
				return this.Context.Mappings;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x00069671 File Offset: 0x00067871
		internal CodeIdentifiers TypeIdentifiers
		{
			get
			{
				return this.Context.TypeIdentifiers;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x0006967E File Offset: 0x0006787E
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

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00069699 File Offset: 0x00067899
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

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x000696B4 File Offset: 0x000678B4
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

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x000696CF File Offset: 0x000678CF
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000696EA File Offset: 0x000678EA
		internal CodeGenerationOptions Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x000696F4 File Offset: 0x000678F4
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

		// Token: 0x0600183B RID: 6203 RVA: 0x0006979B File Offset: 0x0006799B
		internal string GenerateUniqueTypeName(string typeName)
		{
			typeName = CodeIdentifier.MakeValid(typeName);
			return this.TypeIdentifiers.AddUnique(typeName, typeName);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000697B4 File Offset: 0x000679B4
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

		// Token: 0x0600183D RID: 6205 RVA: 0x0006980E File Offset: 0x00067A0E
		internal StructMapping GetRootMapping()
		{
			if (this.root == null)
			{
				this.root = this.CreateRootMapping();
			}
			return this.root;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x0006982A File Offset: 0x00067A2A
		internal StructMapping ImportRootMapping()
		{
			if (!this.rootImported)
			{
				this.rootImported = true;
				this.ImportDerivedTypes(XmlQualifiedName.Empty);
			}
			return this.GetRootMapping();
		}

		// Token: 0x0600183F RID: 6207
		internal abstract void ImportDerivedTypes(XmlQualifiedName baseName);

		// Token: 0x06001840 RID: 6208 RVA: 0x0006984C File Offset: 0x00067A4C
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

		// Token: 0x06001841 RID: 6209 RVA: 0x000698A1 File Offset: 0x00067AA1
		internal void RemoveReference(XmlQualifiedName name, NameTable references)
		{
			references[name] = null;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000698AB File Offset: 0x00067AAB
		internal void AddReservedIdentifiersForDataBinding(CodeIdentifiers scope)
		{
			if ((this.options & CodeGenerationOptions.EnableDataBinding) != CodeGenerationOptions.None)
			{
				scope.AddReserved(CodeExporter.PropertyChangedEvent.Name);
				scope.AddReserved(CodeExporter.RaisePropertyChangedEventMethod.Name);
			}
		}

		// Token: 0x04000B30 RID: 2864
		private XmlSchemas schemas;

		// Token: 0x04000B31 RID: 2865
		private StructMapping root;

		// Token: 0x04000B32 RID: 2866
		private CodeGenerationOptions options;

		// Token: 0x04000B33 RID: 2867
		private CodeDomProvider codeProvider;

		// Token: 0x04000B34 RID: 2868
		private TypeScope scope;

		// Token: 0x04000B35 RID: 2869
		private ImportContext context;

		// Token: 0x04000B36 RID: 2870
		private bool rootImported;

		// Token: 0x04000B37 RID: 2871
		private NameTable typesInUse;

		// Token: 0x04000B38 RID: 2872
		private NameTable groupsInUse;

		// Token: 0x04000B39 RID: 2873
		private SchemaImporterExtensionCollection extensions;
	}
}
