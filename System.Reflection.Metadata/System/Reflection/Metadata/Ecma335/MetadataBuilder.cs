using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Text;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000B7 RID: 183
	internal sealed class MetadataBuilder
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x000111EC File Offset: 0x0000F3EC
		public void SetCapacity(TableIndex table, int capacity)
		{
			switch (table)
			{
			case TableIndex.Module:
				this._moduleTable.Capacity = capacity;
				return;
			case TableIndex.TypeRef:
				this._typeRefTable.Capacity = capacity;
				return;
			case TableIndex.TypeDef:
				this._typeDefTable.Capacity = capacity;
				return;
			case TableIndex.FieldPtr:
			case TableIndex.MethodPtr:
			case TableIndex.ParamPtr:
			case TableIndex.EventPtr:
			case TableIndex.PropertyPtr:
			case TableIndex.AssemblyProcessor:
			case TableIndex.AssemblyOS:
			case TableIndex.AssemblyRefProcessor:
			case TableIndex.AssemblyRefOS:
				throw new NotSupportedException();
			case TableIndex.Field:
				this._fieldTable.Capacity = capacity;
				return;
			case TableIndex.MethodDef:
				this._methodDefTable.Capacity = capacity;
				return;
			case TableIndex.Param:
				this._paramTable.Capacity = capacity;
				return;
			case TableIndex.InterfaceImpl:
				this._interfaceImplTable.Capacity = capacity;
				return;
			case TableIndex.MemberRef:
				this._memberRefTable.Capacity = capacity;
				return;
			case TableIndex.Constant:
				this._constantTable.Capacity = capacity;
				return;
			case TableIndex.CustomAttribute:
				this._customAttributeTable.Capacity = capacity;
				return;
			case TableIndex.FieldMarshal:
				this._fieldMarshalTable.Capacity = capacity;
				return;
			case TableIndex.DeclSecurity:
				this._declSecurityTable.Capacity = capacity;
				return;
			case TableIndex.ClassLayout:
				this._classLayoutTable.Capacity = capacity;
				return;
			case TableIndex.FieldLayout:
				this._fieldLayoutTable.Capacity = capacity;
				return;
			case TableIndex.StandAloneSig:
				this._standAloneSigTable.Capacity = capacity;
				return;
			case TableIndex.EventMap:
				this._eventMapTable.Capacity = capacity;
				return;
			case TableIndex.Event:
				this._eventTable.Capacity = capacity;
				return;
			case TableIndex.PropertyMap:
				this._propertyMapTable.Capacity = capacity;
				return;
			case TableIndex.Property:
				this._propertyTable.Capacity = capacity;
				return;
			case TableIndex.MethodSemantics:
				this._methodSemanticsTable.Capacity = capacity;
				return;
			case TableIndex.MethodImpl:
				this._methodImplTable.Capacity = capacity;
				return;
			case TableIndex.ModuleRef:
				this._moduleRefTable.Capacity = capacity;
				return;
			case TableIndex.TypeSpec:
				this._typeSpecTable.Capacity = capacity;
				return;
			case TableIndex.ImplMap:
				this._implMapTable.Capacity = capacity;
				return;
			case TableIndex.FieldRva:
				this._fieldRvaTable.Capacity = capacity;
				return;
			case TableIndex.EncLog:
				this._encLogTable.Capacity = capacity;
				return;
			case TableIndex.EncMap:
				this._encMapTable.Capacity = capacity;
				return;
			case TableIndex.Assembly:
				this._assemblyTable.Capacity = capacity;
				return;
			case TableIndex.AssemblyRef:
				this._assemblyRefTable.Capacity = capacity;
				return;
			case TableIndex.File:
				this._fileTable.Capacity = capacity;
				return;
			case TableIndex.ExportedType:
				this._exportedTypeTable.Capacity = capacity;
				return;
			case TableIndex.ManifestResource:
				this._manifestResourceTable.Capacity = capacity;
				return;
			case TableIndex.NestedClass:
				this._nestedClassTable.Capacity = capacity;
				return;
			case TableIndex.GenericParam:
				this._genericParamTable.Capacity = capacity;
				return;
			case TableIndex.MethodSpec:
				this._methodSpecTable.Capacity = capacity;
				return;
			case TableIndex.GenericParamConstraint:
				this._genericParamConstraintTable.Capacity = capacity;
				return;
			case TableIndex.Document:
				this._documentTable.Capacity = capacity;
				return;
			case TableIndex.MethodDebugInformation:
				this._methodDebugInformationTable.Capacity = capacity;
				return;
			case TableIndex.LocalScope:
				this._localScopeTable.Capacity = capacity;
				return;
			case TableIndex.LocalVariable:
				this._localVariableTable.Capacity = capacity;
				return;
			case TableIndex.LocalConstant:
				this._localConstantTable.Capacity = capacity;
				return;
			case TableIndex.ImportScope:
				this._importScopeTable.Capacity = capacity;
				return;
			case TableIndex.StateMachineMethod:
				this._stateMachineMethodTable.Capacity = capacity;
				return;
			case TableIndex.CustomDebugInformation:
				this._customDebugInformationTable.Capacity = capacity;
				return;
			}
			throw new ArgumentOutOfRangeException("table");
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00011530 File Offset: 0x0000F730
		public ModuleDefinitionHandle AddModule(int generation, StringHandle moduleName, GuidHandle mvid, GuidHandle encId, GuidHandle encBaseId)
		{
			this._moduleTable.Add(new MetadataBuilder.ModuleRow
			{
				Generation = (ushort)generation,
				Name = moduleName,
				ModuleVersionId = mvid,
				EncId = encId,
				EncBaseId = encBaseId
			});
			return EntityHandle.ModuleDefinition;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00011584 File Offset: 0x0000F784
		public AssemblyDefinitionHandle AddAssembly(StringHandle name, Version version, StringHandle culture, BlobHandle publicKey, AssemblyFlags flags, AssemblyHashAlgorithm hashAlgorithm)
		{
			this._assemblyTable.Add(new MetadataBuilder.AssemblyRow
			{
				Flags = (ushort)flags,
				HashAlgorithm = (uint)hashAlgorithm,
				Version = version,
				AssemblyKey = publicKey,
				AssemblyName = name,
				AssemblyCulture = culture
			});
			return EntityHandle.AssemblyDefinition;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000115E0 File Offset: 0x0000F7E0
		public AssemblyReferenceHandle AddAssemblyReference(StringHandle name, Version version, StringHandle culture, BlobHandle publicKeyOrToken, AssemblyFlags flags, BlobHandle hashValue)
		{
			this._assemblyRefTable.Add(new MetadataBuilder.AssemblyRefTableRow
			{
				Name = name,
				Version = version,
				Culture = culture,
				PublicKeyToken = publicKeyOrToken,
				Flags = (uint)flags,
				HashValue = hashValue
			});
			return MetadataTokens.AssemblyReferenceHandle(this._assemblyRefTable.Count);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00011644 File Offset: 0x0000F844
		public TypeDefinitionHandle AddTypeDefinition(TypeAttributes attributes, StringHandle @namespace, StringHandle name, EntityHandle baseType, FieldDefinitionHandle fieldList, MethodDefinitionHandle methodList)
		{
			this._typeDefTable.Add(new MetadataBuilder.TypeDefRow
			{
				Flags = (uint)attributes,
				Name = name,
				Namespace = @namespace,
				Extends = (uint)(baseType.IsNil ? 0 : CodedIndex.ToTypeDefOrRefOrSpec(baseType)),
				FieldList = (uint)MetadataTokens.GetRowNumber(fieldList),
				MethodList = (uint)MetadataTokens.GetRowNumber(methodList)
			});
			return MetadataTokens.TypeDefinitionHandle(this._typeDefTable.Count);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000116D0 File Offset: 0x0000F8D0
		public void AddTypeLayout(TypeDefinitionHandle type, ushort packingSize, uint size)
		{
			this._classLayoutTable.Add(new MetadataBuilder.ClassLayoutRow
			{
				Parent = (uint)MetadataTokens.GetRowNumber(type),
				PackingSize = packingSize,
				ClassSize = size
			});
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00011714 File Offset: 0x0000F914
		public InterfaceImplementationHandle AddInterfaceImplementation(TypeDefinitionHandle type, EntityHandle implementedInterface)
		{
			this._interfaceImplTable.Add(new MetadataBuilder.InterfaceImplRow
			{
				Class = (uint)MetadataTokens.GetRowNumber(type),
				Interface = (uint)CodedIndex.ToTypeDefOrRefOrSpec(implementedInterface)
			});
			return (InterfaceImplementationHandle)MetadataTokens.Handle(TableIndex.InterfaceImpl, this._interfaceImplTable.Count);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001176C File Offset: 0x0000F96C
		public void AddNestedType(TypeDefinitionHandle type, TypeDefinitionHandle enclosingType)
		{
			this._nestedClassTable.Add(new MetadataBuilder.NestedClassRow
			{
				NestedClass = (uint)MetadataTokens.GetRowNumber(type),
				EnclosingClass = (uint)MetadataTokens.GetRowNumber(enclosingType)
			});
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000117B4 File Offset: 0x0000F9B4
		public TypeReferenceHandle AddTypeReference(EntityHandle resolutionScope, StringHandle @namespace, StringHandle name)
		{
			this._typeRefTable.Add(new MetadataBuilder.TypeRefRow
			{
				ResolutionScope = (uint)CodedIndex.ToResolutionScope(resolutionScope),
				Name = name,
				Namespace = @namespace
			});
			return MetadataTokens.TypeReferenceHandle(this._typeRefTable.Count);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00011804 File Offset: 0x0000FA04
		public TypeSpecificationHandle AddTypeSpecification(BlobHandle signature)
		{
			this._typeSpecTable.Add(new MetadataBuilder.TypeSpecRow
			{
				Signature = signature
			});
			return MetadataTokens.TypeSpecificationHandle(this._typeSpecTable.Count);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00011840 File Offset: 0x0000FA40
		public StandaloneSignatureHandle AddStandaloneSignature(BlobHandle signature)
		{
			this._standAloneSigTable.Add(new MetadataBuilder.StandaloneSigRow
			{
				Signature = signature
			});
			return MetadataTokens.StandaloneSignatureHandle(this._standAloneSigTable.Count);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001187C File Offset: 0x0000FA7C
		public PropertyDefinitionHandle AddProperty(PropertyAttributes attributes, StringHandle name, BlobHandle signature)
		{
			this._propertyTable.Add(new MetadataBuilder.PropertyRow
			{
				PropFlags = (ushort)attributes,
				Name = name,
				Type = signature
			});
			return MetadataTokens.PropertyDefinitionHandle(this._propertyTable.Count);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public void AddPropertyMap(TypeDefinitionHandle declaringType, PropertyDefinitionHandle propertyList)
		{
			this._propertyMapTable.Add(new MetadataBuilder.PropertyMapRow
			{
				Parent = (uint)MetadataTokens.GetRowNumber(declaringType),
				PropertyList = (uint)MetadataTokens.GetRowNumber(propertyList)
			});
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00011910 File Offset: 0x0000FB10
		public EventDefinitionHandle AddEvent(EventAttributes attributes, StringHandle name, EntityHandle type)
		{
			this._eventTable.Add(new MetadataBuilder.EventRow
			{
				EventFlags = (ushort)attributes,
				Name = name,
				EventType = (uint)CodedIndex.ToTypeDefOrRefOrSpec(type)
			});
			return MetadataTokens.EventDefinitionHandle(this._eventTable.Count);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00011960 File Offset: 0x0000FB60
		public void AddEventMap(TypeDefinitionHandle declaringType, EventDefinitionHandle eventList)
		{
			this._eventMapTable.Add(new MetadataBuilder.EventMapRow
			{
				Parent = (uint)MetadataTokens.GetRowNumber(declaringType),
				EventList = (uint)MetadataTokens.GetRowNumber(eventList)
			});
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public ConstantHandle AddConstant(EntityHandle parent, object value)
		{
			uint num = (uint)CodedIndex.ToHasConstant(parent);
			this._constantTableNeedsSorting |= (num < this._constantTableLastParent);
			this._constantTableLastParent = num;
			this._constantTable.Add(new MetadataBuilder.ConstantRow
			{
				Type = (byte)MetadataWriterUtilities.GetConstantTypeCode(value),
				Parent = num,
				Value = this.GetConstantBlob(value)
			});
			return MetadataTokens.ConstantHandle(this._constantTable.Count);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00011A20 File Offset: 0x0000FC20
		public void AddMethodSemantics(EntityHandle association, ushort semantics, MethodDefinitionHandle methodDefinition)
		{
			uint num = (uint)CodedIndex.ToHasSemantics(association);
			this._methodSemanticsTableNeedsSorting |= (num < this._methodSemanticsTableLastAssociation);
			this._methodSemanticsTableLastAssociation = num;
			this._methodSemanticsTable.Add(new MetadataBuilder.MethodSemanticsRow
			{
				Association = num,
				Method = (uint)MetadataTokens.GetRowNumber(methodDefinition),
				Semantic = semantics
			});
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00011A88 File Offset: 0x0000FC88
		public CustomAttributeHandle AddCustomAttribute(EntityHandle parent, EntityHandle constructor, BlobHandle value)
		{
			uint num = (uint)CodedIndex.ToHasCustomAttribute(parent);
			this._customAttributeTableNeedsSorting |= (num < this._customAttributeTableLastParent);
			this._customAttributeTableLastParent = num;
			this._customAttributeTable.Add(new MetadataBuilder.CustomAttributeRow
			{
				Parent = num,
				Type = (uint)CodedIndex.ToCustomAttributeType(constructor),
				Value = value
			});
			return MetadataTokens.CustomAttributeHandle(this._customAttributeTable.Count);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00011AFC File Offset: 0x0000FCFC
		public MethodSpecificationHandle AddMethodSpecification(EntityHandle method, BlobHandle instantiation)
		{
			this._methodSpecTable.Add(new MetadataBuilder.MethodSpecRow
			{
				Method = (uint)CodedIndex.ToMethodDefOrRef(method),
				Instantiation = instantiation
			});
			return MetadataTokens.MethodSpecificationHandle(this._methodSpecTable.Count);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00011B44 File Offset: 0x0000FD44
		public ModuleReferenceHandle AddModuleReference(StringHandle moduleName)
		{
			this._moduleRefTable.Add(new MetadataBuilder.ModuleRefRow
			{
				Name = moduleName
			});
			return MetadataTokens.ModuleReferenceHandle(this._moduleRefTable.Count);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00011B80 File Offset: 0x0000FD80
		public ParameterHandle AddParameter(ParameterAttributes attributes, StringHandle name, int sequenceNumber)
		{
			this._paramTable.Add(new MetadataBuilder.ParamRow
			{
				Flags = (ushort)attributes,
				Name = name,
				Sequence = (ushort)sequenceNumber
			});
			return MetadataTokens.ParameterHandle(this._paramTable.Count);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public GenericParameterHandle AddGenericParameter(EntityHandle parent, GenericParameterAttributes attributes, StringHandle name, int index)
		{
			this._genericParamTable.Add(new MetadataBuilder.GenericParamRow
			{
				Flags = (ushort)attributes,
				Name = name,
				Number = (ushort)index,
				Owner = (uint)CodedIndex.ToTypeOrMethodDef(parent)
			});
			return MetadataTokens.GenericParameterHandle(this._genericParamTable.Count);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00011C28 File Offset: 0x0000FE28
		public GenericParameterConstraintHandle AddGenericParameterConstraint(GenericParameterHandle genericParameter, EntityHandle constraint)
		{
			this._genericParamConstraintTable.Add(new MetadataBuilder.GenericParamConstraintRow
			{
				Owner = (uint)MetadataTokens.GetRowNumber(genericParameter),
				Constraint = (uint)CodedIndex.ToTypeDefOrRefOrSpec(constraint)
			});
			return MetadataTokens.GenericParameterConstraintHandle(this._genericParamConstraintTable.Count);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00011C78 File Offset: 0x0000FE78
		public FieldDefinitionHandle AddFieldDefinition(FieldAttributes attributes, StringHandle name, BlobHandle signature)
		{
			this._fieldTable.Add(new MetadataBuilder.FieldDefRow
			{
				Flags = (ushort)attributes,
				Name = name,
				Signature = signature
			});
			return MetadataTokens.FieldDefinitionHandle(this._fieldTable.Count);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00011CC4 File Offset: 0x0000FEC4
		public void AddFieldLayout(FieldDefinitionHandle field, int offset)
		{
			this._fieldLayoutTable.Add(new MetadataBuilder.FieldLayoutRow
			{
				Field = (uint)MetadataTokens.GetRowNumber(field),
				Offset = (uint)offset
			});
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00011D00 File Offset: 0x0000FF00
		public void AddMarshallingDescriptor(EntityHandle parent, BlobHandle descriptor)
		{
			uint num = (uint)CodedIndex.ToHasFieldMarshal(parent);
			this._fieldMarshalTableNeedsSorting |= (num < this._fieldMarshalTableLastParent);
			this._fieldMarshalTableLastParent = num;
			this._fieldMarshalTable.Add(new MetadataBuilder.FieldMarshalRow
			{
				Parent = num,
				NativeType = descriptor
			});
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00011D58 File Offset: 0x0000FF58
		public void AddFieldRelativeVirtualAddress(FieldDefinitionHandle field, int relativeVirtualAddress)
		{
			this._fieldRvaTable.Add(new MetadataBuilder.FieldRvaRow
			{
				Field = (uint)MetadataTokens.GetRowNumber(field),
				Offset = (uint)relativeVirtualAddress
			});
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00011D94 File Offset: 0x0000FF94
		public MethodDefinitionHandle AddMethodDefinition(MethodAttributes attributes, MethodImplAttributes implAttributes, StringHandle name, BlobHandle signature, int bodyOffset, ParameterHandle paramList)
		{
			this._methodDefTable.Add(new MetadataBuilder.MethodRow
			{
				Flags = (ushort)attributes,
				ImplFlags = (ushort)implAttributes,
				Name = name,
				Signature = signature,
				BodyOffset = bodyOffset,
				ParamList = (uint)MetadataTokens.GetRowNumber(paramList)
			});
			return MetadataTokens.MethodDefinitionHandle(this._methodDefTable.Count);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00011E04 File Offset: 0x00010004
		public void AddMethodImport(EntityHandle member, MethodImportAttributes attributes, StringHandle name, ModuleReferenceHandle module)
		{
			this._implMapTable.Add(new MetadataBuilder.ImplMapRow
			{
				MemberForwarded = (uint)CodedIndex.ToMemberForwarded(member),
				ImportName = name,
				ImportScope = (uint)MetadataTokens.GetRowNumber(module),
				MappingFlags = (ushort)attributes
			});
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00011E58 File Offset: 0x00010058
		public MethodImplementationHandle AddMethodImplementation(TypeDefinitionHandle type, EntityHandle methodBody, EntityHandle methodDeclaration)
		{
			this._methodImplTable.Add(new MetadataBuilder.MethodImplRow
			{
				Class = (uint)MetadataTokens.GetRowNumber(type),
				MethodBody = (uint)CodedIndex.ToMethodDefOrRef(methodBody),
				MethodDecl = (uint)CodedIndex.ToMethodDefOrRef(methodDeclaration)
			});
			return MetadataTokens.MethodImplementationHandle(this._methodImplTable.Count);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00011EB8 File Offset: 0x000100B8
		public MemberReferenceHandle AddMemberReference(EntityHandle parent, StringHandle name, BlobHandle signature)
		{
			this._memberRefTable.Add(new MetadataBuilder.MemberRefRow
			{
				Class = (uint)CodedIndex.ToMemberRefParent(parent),
				Name = name,
				Signature = signature
			});
			return MetadataTokens.MemberReferenceHandle(this._memberRefTable.Count);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00011F08 File Offset: 0x00010108
		public ManifestResourceHandle AddManifestResource(ManifestResourceAttributes attributes, StringHandle name, EntityHandle implementation, long offset)
		{
			this._manifestResourceTable.Add(new MetadataBuilder.ManifestResourceRow
			{
				Flags = (uint)attributes,
				Name = name,
				Implementation = (uint)(implementation.IsNil ? 0 : CodedIndex.ToImplementation(implementation)),
				Offset = (uint)offset
			});
			return MetadataTokens.ManifestResourceHandle(this._manifestResourceTable.Count);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00011F6C File Offset: 0x0001016C
		public AssemblyFileHandle AddAssemblyFile(StringHandle name, BlobHandle hashValue, bool containsMetadata)
		{
			this._fileTable.Add(new MetadataBuilder.FileTableRow
			{
				FileName = name,
				Flags = (containsMetadata ? 0U : 1U),
				HashValue = hashValue
			});
			return MetadataTokens.AssemblyFileHandle(this._fileTable.Count);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00011FBC File Offset: 0x000101BC
		public ExportedTypeHandle AddExportedType(TypeAttributes attributes, StringHandle @namespace, StringHandle name, EntityHandle implementation, int typeDefinitionId)
		{
			this._exportedTypeTable.Add(new MetadataBuilder.ExportedTypeRow
			{
				Flags = (uint)attributes,
				Implementation = (uint)CodedIndex.ToImplementation(implementation),
				TypeNamespace = @namespace,
				TypeName = name,
				TypeDefId = (uint)typeDefinitionId
			});
			return MetadataTokens.ExportedTypeHandle(this._exportedTypeTable.Count);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001201C File Offset: 0x0001021C
		public uint GetExportedTypeFlags(int rowId)
		{
			return this._exportedTypeTable[rowId].Flags;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00012030 File Offset: 0x00010230
		public DeclarativeSecurityAttributeHandle AddDeclarativeSecurityAttribute(EntityHandle parent, DeclarativeSecurityAction action, BlobHandle permissionSet)
		{
			uint num = (uint)CodedIndex.ToHasDeclSecurity(parent);
			this._declSecurityTableNeedsSorting |= (num < this._declSecurityTableLastParent);
			this._declSecurityTableLastParent = num;
			this._declSecurityTable.Add(new MetadataBuilder.DeclSecurityRow
			{
				Parent = num,
				Action = (ushort)action,
				PermissionSet = permissionSet
			});
			return MetadataTokens.DeclarativeSecurityAttributeHandle(this._declSecurityTable.Count);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000120A0 File Offset: 0x000102A0
		public void AddEncLogEntry(EntityHandle entity, EditAndContinueOperation code)
		{
			this._encLogTable.Add(new MetadataBuilder.EncLogRow
			{
				Token = (uint)MetadataTokens.GetToken(entity),
				FuncCode = (byte)code
			});
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000120D8 File Offset: 0x000102D8
		public void AddEncMapEntry(EntityHandle entity)
		{
			this._encMapTable.Add(new MetadataBuilder.EncMapRow
			{
				Token = (uint)MetadataTokens.GetToken(entity)
			});
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00012108 File Offset: 0x00010308
		public DocumentHandle AddDocument(BlobHandle name, GuidHandle hashAlgorithm, BlobHandle hash, GuidHandle language)
		{
			this._documentTable.Add(new MetadataBuilder.DocumentRow
			{
				Name = name,
				HashAlgorithm = hashAlgorithm,
				Hash = hash,
				Language = language
			});
			return MetadataTokens.DocumentHandle(this._documentTable.Count);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001215C File Offset: 0x0001035C
		public MethodDebugInformationHandle AddMethodDebugInformation(DocumentHandle document, BlobHandle sequencePoints)
		{
			this._methodDebugInformationTable.Add(new MetadataBuilder.MethodDebugInformationRow
			{
				Document = (uint)MetadataTokens.GetRowNumber(document),
				SequencePoints = sequencePoints
			});
			return (MethodDebugInformationHandle)MetadataTokens.Handle(TableIndex.MethodDebugInformation, this._methodDebugInformationTable.Count);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000121B0 File Offset: 0x000103B0
		public LocalScopeHandle AddLocalScope(MethodDefinitionHandle method, ImportScopeHandle importScope, LocalVariableHandle variableList, LocalConstantHandle constantList, int startOffset, int length)
		{
			this._localScopeTable.Add(new MetadataBuilder.LocalScopeRow
			{
				Method = (uint)MetadataTokens.GetRowNumber(method),
				ImportScope = (uint)MetadataTokens.GetRowNumber(importScope),
				VariableList = (uint)MetadataTokens.GetRowNumber(variableList),
				ConstantList = (uint)MetadataTokens.GetRowNumber(constantList),
				StartOffset = (uint)startOffset,
				Length = (uint)length
			});
			return MetadataTokens.LocalScopeHandle(this._localScopeTable.Count);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001223C File Offset: 0x0001043C
		public LocalVariableHandle AddLocalVariable(LocalVariableAttributes attributes, int index, StringHandle name)
		{
			this._localVariableTable.Add(new MetadataBuilder.LocalVariableRow
			{
				Attributes = (ushort)attributes,
				Index = (ushort)index,
				Name = name
			});
			return MetadataTokens.LocalVariableHandle(this._localVariableTable.Count);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00012288 File Offset: 0x00010488
		public LocalConstantHandle AddLocalConstant(StringHandle name, BlobHandle signature)
		{
			this._localConstantTable.Add(new MetadataBuilder.LocalConstantRow
			{
				Name = name,
				Signature = signature
			});
			return MetadataTokens.LocalConstantHandle(this._localConstantTable.Count);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000122CC File Offset: 0x000104CC
		public ImportScopeHandle AddImportScope(ImportScopeHandle parentScope, BlobHandle imports)
		{
			this._importScopeTable.Add(new MetadataBuilder.ImportScopeRow
			{
				Parent = (uint)MetadataTokens.GetRowNumber(parentScope),
				Imports = imports
			});
			return MetadataTokens.ImportScopeHandle(this._importScopeTable.Count);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00012318 File Offset: 0x00010518
		public void AddStateMachineMethod(MethodDefinitionHandle moveNextMethod, MethodDefinitionHandle kickoffMethod)
		{
			this._stateMachineMethodTable.Add(new MetadataBuilder.StateMachineMethodRow
			{
				MoveNextMethod = (uint)MetadataTokens.GetRowNumber(moveNextMethod),
				KickoffMethod = (uint)MetadataTokens.GetRowNumber(kickoffMethod)
			});
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00012360 File Offset: 0x00010560
		public CustomDebugInformationHandle AddCustomDebugInformation(EntityHandle parent, GuidHandle kind, BlobHandle value)
		{
			this._customDebugInformationTable.Add(new MetadataBuilder.CustomDebugInformationRow
			{
				Parent = (uint)CodedIndex.ToHasCustomDebugInformation(parent),
				Kind = kind,
				Value = value
			});
			return MetadataTokens.CustomDebugInformationHandle(this._customDebugInformationTable.Count);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x000123B0 File Offset: 0x000105B0
		public ImmutableArray<int> GetRowCounts()
		{
			int[] array = new int[MetadataTokens.TableCount];
			array[32] = this._assemblyTable.Count;
			array[35] = this._assemblyRefTable.Count;
			array[15] = this._classLayoutTable.Count;
			array[11] = this._constantTable.Count;
			array[12] = this._customAttributeTable.Count;
			array[14] = this._declSecurityTable.Count;
			array[30] = this._encLogTable.Count;
			array[31] = this._encMapTable.Count;
			array[18] = this._eventMapTable.Count;
			array[20] = this._eventTable.Count;
			array[39] = this._exportedTypeTable.Count;
			array[16] = this._fieldLayoutTable.Count;
			array[13] = this._fieldMarshalTable.Count;
			array[29] = this._fieldRvaTable.Count;
			array[4] = this._fieldTable.Count;
			array[38] = this._fileTable.Count;
			array[44] = this._genericParamConstraintTable.Count;
			array[42] = this._genericParamTable.Count;
			array[28] = this._implMapTable.Count;
			array[9] = this._interfaceImplTable.Count;
			array[40] = this._manifestResourceTable.Count;
			array[10] = this._memberRefTable.Count;
			array[25] = this._methodImplTable.Count;
			array[24] = this._methodSemanticsTable.Count;
			array[43] = this._methodSpecTable.Count;
			array[6] = this._methodDefTable.Count;
			array[26] = this._moduleRefTable.Count;
			array[0] = this._moduleTable.Count;
			array[41] = this._nestedClassTable.Count;
			array[8] = this._paramTable.Count;
			array[21] = this._propertyMapTable.Count;
			array[23] = this._propertyTable.Count;
			array[17] = this._standAloneSigTable.Count;
			array[2] = this._typeDefTable.Count;
			array[1] = this._typeRefTable.Count;
			array[27] = this._typeSpecTable.Count;
			array[48] = this._documentTable.Count;
			array[49] = this._methodDebugInformationTable.Count;
			array[50] = this._localScopeTable.Count;
			array[51] = this._localVariableTable.Count;
			array[52] = this._localConstantTable.Count;
			array[54] = this._stateMachineMethodTable.Count;
			array[53] = this._importScopeTable.Count;
			array[55] = this._customDebugInformationTable.Count;
			return ImmutableArray.CreateRange<int>(array);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001265C File Offset: 0x0001085C
		internal void SerializeMetadataTables(BlobBuilder writer, MetadataSizes metadataSizes, int methodBodyStreamRva, int mappedFieldDataStreamRva)
		{
			int position = writer.Position;
			this.SerializeTablesHeader(writer, metadataSizes);
			if (metadataSizes.IsPresent(TableIndex.Module))
			{
				this.SerializeModuleTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.TypeRef))
			{
				this.SerializeTypeRefTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.TypeDef))
			{
				this.SerializeTypeDefTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Field))
			{
				this.SerializeFieldTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodDef))
			{
				this.SerializeMethodDefTable(writer, metadataSizes, methodBodyStreamRva);
			}
			if (metadataSizes.IsPresent(TableIndex.Param))
			{
				this.SerializeParamTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.InterfaceImpl))
			{
				this.SerializeInterfaceImplTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MemberRef))
			{
				this.SerializeMemberRefTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Constant))
			{
				this.SerializeConstantTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.CustomAttribute))
			{
				this.SerializeCustomAttributeTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.FieldMarshal))
			{
				this.SerializeFieldMarshalTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.DeclSecurity))
			{
				this.SerializeDeclSecurityTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ClassLayout))
			{
				this.SerializeClassLayoutTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.FieldLayout))
			{
				this.SerializeFieldLayoutTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.StandAloneSig))
			{
				this.SerializeStandAloneSigTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.EventMap))
			{
				this.SerializeEventMapTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Event))
			{
				this.SerializeEventTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.PropertyMap))
			{
				this.SerializePropertyMapTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Property))
			{
				this.SerializePropertyTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodSemantics))
			{
				this.SerializeMethodSemanticsTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodImpl))
			{
				this.SerializeMethodImplTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ModuleRef))
			{
				this.SerializeModuleRefTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.TypeSpec))
			{
				this.SerializeTypeSpecTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ImplMap))
			{
				this.SerializeImplMapTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.FieldRva))
			{
				this.SerializeFieldRvaTable(writer, metadataSizes, mappedFieldDataStreamRva);
			}
			if (metadataSizes.IsPresent(TableIndex.EncLog))
			{
				this.SerializeEncLogTable(writer);
			}
			if (metadataSizes.IsPresent(TableIndex.EncMap))
			{
				this.SerializeEncMapTable(writer);
			}
			if (metadataSizes.IsPresent(TableIndex.Assembly))
			{
				this.SerializeAssemblyTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.AssemblyRef))
			{
				this.SerializeAssemblyRefTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.File))
			{
				this.SerializeFileTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ExportedType))
			{
				this.SerializeExportedTypeTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ManifestResource))
			{
				this.SerializeManifestResourceTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.NestedClass))
			{
				this.SerializeNestedClassTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.GenericParam))
			{
				this.SerializeGenericParamTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodSpec))
			{
				this.SerializeMethodSpecTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.GenericParamConstraint))
			{
				this.SerializeGenericParamConstraintTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.Document))
			{
				this.SerializeDocumentTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.MethodDebugInformation))
			{
				this.SerializeMethodDebugInformationTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.LocalScope))
			{
				this.SerializeLocalScopeTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.LocalVariable))
			{
				this.SerializeLocalVariableTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.LocalConstant))
			{
				this.SerializeLocalConstantTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.ImportScope))
			{
				this.SerializeImportScopeTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.StateMachineMethod))
			{
				this.SerializeStateMachineMethodTable(writer, metadataSizes);
			}
			if (metadataSizes.IsPresent(TableIndex.CustomDebugInformation))
			{
				this.SerializeCustomDebugInformationTable(writer, metadataSizes);
			}
			writer.WriteByte(0);
			writer.Align(4);
			int position2 = writer.Position;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000129A0 File Offset: 0x00010BA0
		private void SerializeTablesHeader(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			int position = writer.Position;
			HeapSizeFlag heapSizeFlag = (HeapSizeFlag)0;
			if (metadataSizes.StringIndexSize > 2)
			{
				heapSizeFlag |= HeapSizeFlag.StringHeapLarge;
			}
			if (metadataSizes.GuidIndexSize > 2)
			{
				heapSizeFlag |= HeapSizeFlag.GuidHeapLarge;
			}
			if (metadataSizes.BlobIndexSize > 2)
			{
				heapSizeFlag |= HeapSizeFlag.BlobHeapLarge;
			}
			if (metadataSizes.IsMinimalDelta)
			{
				heapSizeFlag |= (HeapSizeFlag)160;
			}
			ulong value = (metadataSizes.PresentTablesMask & 55169095435288576UL) | (metadataSizes.IsStandaloneDebugMetadata ? 0UL : 24190111578624UL);
			writer.WriteUInt32(0U);
			writer.WriteByte(2);
			writer.WriteByte(0);
			writer.WriteByte((byte)heapSizeFlag);
			writer.WriteByte(1);
			writer.WriteUInt64(metadataSizes.PresentTablesMask);
			writer.WriteUInt64(value);
			MetadataWriterUtilities.SerializeRowCounts(writer, metadataSizes.RowCounts);
			int position2 = writer.Position;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00012A60 File Offset: 0x00010C60
		private void SerializeModuleTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ModuleRow moduleRow in this._moduleTable)
			{
				writer.WriteUInt16(moduleRow.Generation);
				writer.WriteReference((uint)this.GetHeapOffset(moduleRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(moduleRow.ModuleVersionId), (int)metadataSizes.GuidIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(moduleRow.EncId), (int)metadataSizes.GuidIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(moduleRow.EncBaseId), (int)metadataSizes.GuidIndexSize);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00012B18 File Offset: 0x00010D18
		private void SerializeEncLogTable(BlobBuilder writer)
		{
			foreach (MetadataBuilder.EncLogRow encLogRow in this._encLogTable)
			{
				writer.WriteUInt32(encLogRow.Token);
				writer.WriteUInt32((uint)encLogRow.FuncCode);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00012B7C File Offset: 0x00010D7C
		private void SerializeEncMapTable(BlobBuilder writer)
		{
			foreach (MetadataBuilder.EncMapRow encMapRow in this._encMapTable)
			{
				writer.WriteUInt32(encMapRow.Token);
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00012BD4 File Offset: 0x00010DD4
		private void SerializeTypeRefTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.TypeRefRow typeRefRow in this._typeRefTable)
			{
				writer.WriteReference(typeRefRow.ResolutionScope, (int)metadataSizes.ResolutionScopeCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(typeRefRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(typeRefRow.Namespace), (int)metadataSizes.StringIndexSize);
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00012C64 File Offset: 0x00010E64
		private void SerializeTypeDefTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.TypeDefRow typeDefRow in this._typeDefTable)
			{
				writer.WriteUInt32(typeDefRow.Flags);
				writer.WriteReference((uint)this.GetHeapOffset(typeDefRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(typeDefRow.Namespace), (int)metadataSizes.StringIndexSize);
				writer.WriteReference(typeDefRow.Extends, (int)metadataSizes.TypeDefOrRefCodedIndexSize);
				writer.WriteReference(typeDefRow.FieldList, (int)metadataSizes.FieldDefIndexSize);
				writer.WriteReference(typeDefRow.MethodList, (int)metadataSizes.MethodDefIndexSize);
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00012D28 File Offset: 0x00010F28
		private void SerializeFieldTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.FieldDefRow fieldDefRow in this._fieldTable)
			{
				writer.WriteUInt16(fieldDefRow.Flags);
				writer.WriteReference((uint)this.GetHeapOffset(fieldDefRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(fieldDefRow.Signature), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00012DB0 File Offset: 0x00010FB0
		private void SerializeMethodDefTable(BlobBuilder writer, MetadataSizes metadataSizes, int methodBodyStreamRva)
		{
			foreach (MetadataBuilder.MethodRow methodRow in this._methodDefTable)
			{
				if (methodRow.BodyOffset == -1)
				{
					writer.WriteUInt32(0U);
				}
				else
				{
					writer.WriteUInt32((uint)(methodBodyStreamRva + methodRow.BodyOffset));
				}
				writer.WriteUInt16(methodRow.ImplFlags);
				writer.WriteUInt16(methodRow.Flags);
				writer.WriteReference((uint)this.GetHeapOffset(methodRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(methodRow.Signature), (int)metadataSizes.BlobIndexSize);
				writer.WriteReference(methodRow.ParamList, (int)metadataSizes.ParameterIndexSize);
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00012E7C File Offset: 0x0001107C
		private void SerializeParamTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ParamRow paramRow in this._paramTable)
			{
				writer.WriteUInt16(paramRow.Flags);
				writer.WriteUInt16(paramRow.Sequence);
				writer.WriteReference((uint)this.GetHeapOffset(paramRow.Name), (int)metadataSizes.StringIndexSize);
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00012EF8 File Offset: 0x000110F8
		private void SerializeInterfaceImplTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.InterfaceImplRow interfaceImplRow in this._interfaceImplTable)
			{
				writer.WriteReference(interfaceImplRow.Class, (int)metadataSizes.TypeDefIndexSize);
				writer.WriteReference(interfaceImplRow.Interface, (int)metadataSizes.TypeDefOrRefCodedIndexSize);
			}
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00012F68 File Offset: 0x00011168
		private void SerializeMemberRefTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MemberRefRow memberRefRow in this._memberRefTable)
			{
				writer.WriteReference(memberRefRow.Class, (int)metadataSizes.MemberRefParentCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(memberRefRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(memberRefRow.Signature), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00012FF8 File Offset: 0x000111F8
		private void SerializeConstantTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.ConstantRow> enumerable2;
			if (!this._constantTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.ConstantRow> enumerable = this._constantTable;
				enumerable2 = enumerable;
			}
			else
			{
				IEnumerable<MetadataBuilder.ConstantRow> enumerable = this._constantTable.OrderBy((MetadataBuilder.ConstantRow x, MetadataBuilder.ConstantRow y) => (int)(x.Parent - y.Parent));
				enumerable2 = enumerable;
			}
			foreach (MetadataBuilder.ConstantRow constantRow in enumerable2)
			{
				writer.WriteByte(constantRow.Type);
				writer.WriteByte(0);
				writer.WriteReference(constantRow.Parent, (int)metadataSizes.HasConstantCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(constantRow.Value), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000130B4 File Offset: 0x000112B4
		private void SerializeCustomAttributeTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.CustomAttributeRow> enumerable2;
			if (!this._customAttributeTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.CustomAttributeRow> enumerable = this._customAttributeTable;
				enumerable2 = enumerable;
			}
			else
			{
				IEnumerable<MetadataBuilder.CustomAttributeRow> enumerable = this._customAttributeTable.OrderBy((MetadataBuilder.CustomAttributeRow x, MetadataBuilder.CustomAttributeRow y) => (int)(x.Parent - y.Parent));
				enumerable2 = enumerable;
			}
			foreach (MetadataBuilder.CustomAttributeRow customAttributeRow in enumerable2)
			{
				writer.WriteReference(customAttributeRow.Parent, (int)metadataSizes.HasCustomAttributeCodedIndexSize);
				writer.WriteReference(customAttributeRow.Type, (int)metadataSizes.CustomAttributeTypeCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(customAttributeRow.Value), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00013170 File Offset: 0x00011370
		private void SerializeFieldMarshalTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.FieldMarshalRow> enumerable2;
			if (!this._fieldMarshalTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.FieldMarshalRow> enumerable = this._fieldMarshalTable;
				enumerable2 = enumerable;
			}
			else
			{
				IEnumerable<MetadataBuilder.FieldMarshalRow> enumerable = this._fieldMarshalTable.OrderBy((MetadataBuilder.FieldMarshalRow x, MetadataBuilder.FieldMarshalRow y) => (int)(x.Parent - y.Parent));
				enumerable2 = enumerable;
			}
			foreach (MetadataBuilder.FieldMarshalRow fieldMarshalRow in enumerable2)
			{
				writer.WriteReference(fieldMarshalRow.Parent, (int)metadataSizes.HasFieldMarshalCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(fieldMarshalRow.NativeType), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00013218 File Offset: 0x00011418
		private void SerializeDeclSecurityTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.DeclSecurityRow> enumerable2;
			if (!this._declSecurityTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.DeclSecurityRow> enumerable = this._declSecurityTable;
				enumerable2 = enumerable;
			}
			else
			{
				IEnumerable<MetadataBuilder.DeclSecurityRow> enumerable = this._declSecurityTable.OrderBy((MetadataBuilder.DeclSecurityRow x, MetadataBuilder.DeclSecurityRow y) => (int)(x.Parent - y.Parent));
				enumerable2 = enumerable;
			}
			foreach (MetadataBuilder.DeclSecurityRow declSecurityRow in enumerable2)
			{
				writer.WriteUInt16(declSecurityRow.Action);
				writer.WriteReference(declSecurityRow.Parent, (int)metadataSizes.DeclSecurityCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(declSecurityRow.PermissionSet), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000132CC File Offset: 0x000114CC
		private void SerializeClassLayoutTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ClassLayoutRow classLayoutRow in this._classLayoutTable)
			{
				writer.WriteUInt16(classLayoutRow.PackingSize);
				writer.WriteUInt32(classLayoutRow.ClassSize);
				writer.WriteReference(classLayoutRow.Parent, (int)metadataSizes.TypeDefIndexSize);
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00013344 File Offset: 0x00011544
		private void SerializeFieldLayoutTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.FieldLayoutRow fieldLayoutRow in this._fieldLayoutTable)
			{
				writer.WriteUInt32(fieldLayoutRow.Offset);
				writer.WriteReference(fieldLayoutRow.Field, (int)metadataSizes.FieldDefIndexSize);
			}
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000133B0 File Offset: 0x000115B0
		private void SerializeStandAloneSigTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.StandaloneSigRow standaloneSigRow in this._standAloneSigTable)
			{
				writer.WriteReference((uint)this.GetHeapOffset(standaloneSigRow.Signature), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00013414 File Offset: 0x00011614
		private void SerializeEventMapTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.EventMapRow eventMapRow in this._eventMapTable)
			{
				writer.WriteReference(eventMapRow.Parent, (int)metadataSizes.TypeDefIndexSize);
				writer.WriteReference(eventMapRow.EventList, (int)metadataSizes.EventDefIndexSize);
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00013484 File Offset: 0x00011684
		private void SerializeEventTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.EventRow eventRow in this._eventTable)
			{
				writer.WriteUInt16(eventRow.EventFlags);
				writer.WriteReference((uint)this.GetHeapOffset(eventRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference(eventRow.EventType, (int)metadataSizes.TypeDefOrRefCodedIndexSize);
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00013508 File Offset: 0x00011708
		private void SerializePropertyMapTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.PropertyMapRow propertyMapRow in this._propertyMapTable)
			{
				writer.WriteReference(propertyMapRow.Parent, (int)metadataSizes.TypeDefIndexSize);
				writer.WriteReference(propertyMapRow.PropertyList, (int)metadataSizes.PropertyDefIndexSize);
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00013578 File Offset: 0x00011778
		private void SerializePropertyTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.PropertyRow propertyRow in this._propertyTable)
			{
				writer.WriteUInt16(propertyRow.PropFlags);
				writer.WriteReference((uint)this.GetHeapOffset(propertyRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(propertyRow.Type), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00013600 File Offset: 0x00011800
		private void SerializeMethodSemanticsTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			IEnumerable<MetadataBuilder.MethodSemanticsRow> enumerable2;
			if (!this._methodSemanticsTableNeedsSorting)
			{
				IEnumerable<MetadataBuilder.MethodSemanticsRow> enumerable = this._methodSemanticsTable;
				enumerable2 = enumerable;
			}
			else
			{
				IEnumerable<MetadataBuilder.MethodSemanticsRow> enumerable = this._methodSemanticsTable.OrderBy((MetadataBuilder.MethodSemanticsRow x, MetadataBuilder.MethodSemanticsRow y) => (int)(x.Association - y.Association));
				enumerable2 = enumerable;
			}
			foreach (MetadataBuilder.MethodSemanticsRow methodSemanticsRow in enumerable2)
			{
				writer.WriteUInt16(methodSemanticsRow.Semantic);
				writer.WriteReference(methodSemanticsRow.Method, (int)metadataSizes.MethodDefIndexSize);
				writer.WriteReference(methodSemanticsRow.Association, (int)metadataSizes.HasSemanticsCodedIndexSize);
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x000136B0 File Offset: 0x000118B0
		private void SerializeMethodImplTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MethodImplRow methodImplRow in this._methodImplTable)
			{
				writer.WriteReference(methodImplRow.Class, (int)metadataSizes.TypeDefIndexSize);
				writer.WriteReference(methodImplRow.MethodBody, (int)metadataSizes.MethodDefOrRefCodedIndexSize);
				writer.WriteReference(methodImplRow.MethodDecl, (int)metadataSizes.MethodDefOrRefCodedIndexSize);
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00013734 File Offset: 0x00011934
		private void SerializeModuleRefTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ModuleRefRow moduleRefRow in this._moduleRefTable)
			{
				writer.WriteReference((uint)this.GetHeapOffset(moduleRefRow.Name), (int)metadataSizes.StringIndexSize);
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00013798 File Offset: 0x00011998
		private void SerializeTypeSpecTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.TypeSpecRow typeSpecRow in this._typeSpecTable)
			{
				writer.WriteReference((uint)this.GetHeapOffset(typeSpecRow.Signature), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000137FC File Offset: 0x000119FC
		private void SerializeImplMapTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ImplMapRow implMapRow in this._implMapTable)
			{
				writer.WriteUInt16(implMapRow.MappingFlags);
				writer.WriteReference(implMapRow.MemberForwarded, (int)metadataSizes.MemberForwardedCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(implMapRow.ImportName), (int)metadataSizes.StringIndexSize);
				writer.WriteReference(implMapRow.ImportScope, (int)metadataSizes.ModuleRefIndexSize);
			}
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00013890 File Offset: 0x00011A90
		private void SerializeFieldRvaTable(BlobBuilder writer, MetadataSizes metadataSizes, int mappedFieldDataStreamRva)
		{
			foreach (MetadataBuilder.FieldRvaRow fieldRvaRow in this._fieldRvaTable)
			{
				writer.WriteUInt32((uint)(mappedFieldDataStreamRva + (int)fieldRvaRow.Offset));
				writer.WriteReference(fieldRvaRow.Field, (int)metadataSizes.FieldDefIndexSize);
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x000138FC File Offset: 0x00011AFC
		private void SerializeAssemblyTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.AssemblyRow assemblyRow in this._assemblyTable)
			{
				writer.WriteUInt32(assemblyRow.HashAlgorithm);
				writer.WriteUInt16((ushort)assemblyRow.Version.Major);
				writer.WriteUInt16((ushort)assemblyRow.Version.Minor);
				writer.WriteUInt16((ushort)assemblyRow.Version.Build);
				writer.WriteUInt16((ushort)assemblyRow.Version.Revision);
				writer.WriteUInt32((uint)assemblyRow.Flags);
				writer.WriteReference((uint)this.GetHeapOffset(assemblyRow.AssemblyKey), (int)metadataSizes.BlobIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(assemblyRow.AssemblyName), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(assemblyRow.AssemblyCulture), (int)metadataSizes.StringIndexSize);
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x000139F8 File Offset: 0x00011BF8
		private void SerializeAssemblyRefTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.AssemblyRefTableRow assemblyRefTableRow in this._assemblyRefTable)
			{
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Major);
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Minor);
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Build);
				writer.WriteUInt16((ushort)assemblyRefTableRow.Version.Revision);
				writer.WriteUInt32(assemblyRefTableRow.Flags);
				writer.WriteReference((uint)this.GetHeapOffset(assemblyRefTableRow.PublicKeyToken), (int)metadataSizes.BlobIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(assemblyRefTableRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(assemblyRefTableRow.Culture), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(assemblyRefTableRow.HashValue), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00013B00 File Offset: 0x00011D00
		private void SerializeFileTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.FileTableRow fileTableRow in this._fileTable)
			{
				writer.WriteUInt32(fileTableRow.Flags);
				writer.WriteReference((uint)this.GetHeapOffset(fileTableRow.FileName), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(fileTableRow.HashValue), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00013B88 File Offset: 0x00011D88
		private void SerializeExportedTypeTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ExportedTypeRow exportedTypeRow in this._exportedTypeTable)
			{
				writer.WriteUInt32(exportedTypeRow.Flags);
				writer.WriteUInt32(exportedTypeRow.TypeDefId);
				writer.WriteReference((uint)this.GetHeapOffset(exportedTypeRow.TypeName), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(exportedTypeRow.TypeNamespace), (int)metadataSizes.StringIndexSize);
				writer.WriteReference(exportedTypeRow.Implementation, (int)metadataSizes.ImplementationCodedIndexSize);
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00013C30 File Offset: 0x00011E30
		private void SerializeManifestResourceTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ManifestResourceRow manifestResourceRow in this._manifestResourceTable)
			{
				writer.WriteUInt32(manifestResourceRow.Offset);
				writer.WriteUInt32(manifestResourceRow.Flags);
				writer.WriteReference((uint)this.GetHeapOffset(manifestResourceRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference(manifestResourceRow.Implementation, (int)metadataSizes.ImplementationCodedIndexSize);
			}
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00013CC0 File Offset: 0x00011EC0
		private void SerializeNestedClassTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.NestedClassRow nestedClassRow in this._nestedClassTable)
			{
				writer.WriteReference(nestedClassRow.NestedClass, (int)metadataSizes.TypeDefIndexSize);
				writer.WriteReference(nestedClassRow.EnclosingClass, (int)metadataSizes.TypeDefIndexSize);
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00013D30 File Offset: 0x00011F30
		private void SerializeGenericParamTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.GenericParamRow genericParamRow in this._genericParamTable)
			{
				writer.WriteUInt16(genericParamRow.Number);
				writer.WriteUInt16(genericParamRow.Flags);
				writer.WriteReference(genericParamRow.Owner, (int)metadataSizes.TypeOrMethodDefCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(genericParamRow.Name), (int)metadataSizes.StringIndexSize);
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00013DC0 File Offset: 0x00011FC0
		private void SerializeGenericParamConstraintTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.GenericParamConstraintRow genericParamConstraintRow in this._genericParamConstraintTable)
			{
				writer.WriteReference(genericParamConstraintRow.Owner, (int)metadataSizes.GenericParamIndexSize);
				writer.WriteReference(genericParamConstraintRow.Constraint, (int)metadataSizes.TypeDefOrRefCodedIndexSize);
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00013E30 File Offset: 0x00012030
		private void SerializeMethodSpecTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MethodSpecRow methodSpecRow in this._methodSpecTable)
			{
				writer.WriteReference(methodSpecRow.Method, (int)metadataSizes.MethodDefOrRefCodedIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(methodSpecRow.Instantiation), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00013EA8 File Offset: 0x000120A8
		private void SerializeDocumentTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.DocumentRow documentRow in this._documentTable)
			{
				writer.WriteReference((uint)this.GetHeapOffset(documentRow.Name), (int)metadataSizes.BlobIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(documentRow.HashAlgorithm), (int)metadataSizes.GuidIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(documentRow.Hash), (int)metadataSizes.BlobIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(documentRow.Language), (int)metadataSizes.GuidIndexSize);
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00013F54 File Offset: 0x00012154
		private void SerializeMethodDebugInformationTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.MethodDebugInformationRow methodDebugInformationRow in this._methodDebugInformationTable)
			{
				writer.WriteReference(methodDebugInformationRow.Document, (int)metadataSizes.DocumentIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(methodDebugInformationRow.SequencePoints), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00013FCC File Offset: 0x000121CC
		private void SerializeLocalScopeTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.LocalScopeRow localScopeRow in this._localScopeTable)
			{
				writer.WriteReference(localScopeRow.Method, (int)metadataSizes.MethodDefIndexSize);
				writer.WriteReference(localScopeRow.ImportScope, (int)metadataSizes.ImportScopeIndexSize);
				writer.WriteReference(localScopeRow.VariableList, (int)metadataSizes.LocalVariableIndexSize);
				writer.WriteReference(localScopeRow.ConstantList, (int)metadataSizes.LocalConstantIndexSize);
				writer.WriteUInt32(localScopeRow.StartOffset);
				writer.WriteUInt32(localScopeRow.Length);
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00014078 File Offset: 0x00012278
		private void SerializeLocalVariableTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.LocalVariableRow localVariableRow in this._localVariableTable)
			{
				writer.WriteUInt16(localVariableRow.Attributes);
				writer.WriteUInt16(localVariableRow.Index);
				writer.WriteReference((uint)this.GetHeapOffset(localVariableRow.Name), (int)metadataSizes.StringIndexSize);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000140F4 File Offset: 0x000122F4
		private void SerializeLocalConstantTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.LocalConstantRow localConstantRow in this._localConstantTable)
			{
				writer.WriteReference((uint)this.GetHeapOffset(localConstantRow.Name), (int)metadataSizes.StringIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(localConstantRow.Signature), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00014170 File Offset: 0x00012370
		private void SerializeImportScopeTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.ImportScopeRow importScopeRow in this._importScopeTable)
			{
				writer.WriteReference(importScopeRow.Parent, (int)metadataSizes.ImportScopeIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(importScopeRow.Imports), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000141E8 File Offset: 0x000123E8
		private void SerializeStateMachineMethodTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.StateMachineMethodRow stateMachineMethodRow in this._stateMachineMethodTable)
			{
				writer.WriteReference(stateMachineMethodRow.MoveNextMethod, (int)metadataSizes.MethodDefIndexSize);
				writer.WriteReference(stateMachineMethodRow.KickoffMethod, (int)metadataSizes.MethodDefIndexSize);
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00014258 File Offset: 0x00012458
		private void SerializeCustomDebugInformationTable(BlobBuilder writer, MetadataSizes metadataSizes)
		{
			foreach (MetadataBuilder.CustomDebugInformationRow customDebugInformationRow in this._customDebugInformationTable.OrderBy(delegate(MetadataBuilder.CustomDebugInformationRow x, MetadataBuilder.CustomDebugInformationRow y)
			{
				int num = (int)(x.Parent - y.Parent);
				if (num == 0)
				{
					return MetadataTokens.GetHeapOffset(x.Kind) - MetadataTokens.GetHeapOffset(y.Kind);
				}
				return num;
			}))
			{
				writer.WriteReference(customDebugInformationRow.Parent, (int)metadataSizes.HasCustomDebugInformationSize);
				writer.WriteReference((uint)this.GetHeapOffset(customDebugInformationRow.Kind), (int)metadataSizes.GuidIndexSize);
				writer.WriteReference((uint)this.GetHeapOffset(customDebugInformationRow.Value), (int)metadataSizes.BlobIndexSize);
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00014304 File Offset: 0x00012504
		public MetadataBuilder(int userStringHeapStartOffset = 0, int stringHeapStartOffset = 0, int blobHeapStartOffset = 0, int guidHeapStartOffset = 0)
		{
			this._userStringWriter.WriteByte(0);
			this._blobs.Add(ImmutableArray<byte>.Empty, default(BlobHandle));
			this._blobHeapSize = 1;
			this._userStringHeapStartOffset = userStringHeapStartOffset;
			this._stringHeapStartOffset = stringHeapStartOffset;
			this._blobHeapStartOffset = blobHeapStartOffset;
			this._guidWriter.WriteBytes(0, guidHeapStartOffset);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001459F File Offset: 0x0001279F
		public BlobHandle GetBlob(BlobBuilder builder)
		{
			return this.GetBlob(builder.ToImmutableArray());
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000145B0 File Offset: 0x000127B0
		public BlobHandle GetBlob(ImmutableArray<byte> blob)
		{
			BlobHandle blobHandle;
			if (!this._blobs.TryGetValue(blob, out blobHandle))
			{
				blobHandle = MetadataTokens.BlobHandle(this._blobHeapSize);
				this._blobs.Add(blob, blobHandle);
				this._blobHeapSize += BlobWriterImpl.GetCompressedIntegerSize(blob.Length) + blob.Length;
			}
			return blobHandle;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00014608 File Offset: 0x00012808
		public BlobHandle GetConstantBlob(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return this.GetBlob(text);
			}
			BlobBuilder blobBuilder = new BlobBuilder(256);
			blobBuilder.WriteConstant(value);
			return this.GetBlob(blobBuilder);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00014640 File Offset: 0x00012840
		public BlobHandle GetBlob(string str)
		{
			byte[] array = new byte[str.Length * 2];
			int num = 0;
			foreach (char c in str)
			{
				array[num++] = (byte)(c & 'ÿ');
				array[num++] = (byte)(c >> 8);
			}
			return this.GetBlob(ImmutableArray.Create<byte>(array));
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000146A1 File Offset: 0x000128A1
		public BlobHandle GetBlobUtf8(string str)
		{
			return this.GetBlob(ImmutableArray.Create<byte>(Encoding.UTF8.GetBytes(str)));
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000146BC File Offset: 0x000128BC
		public GuidHandle GetGuid(Guid guid)
		{
			if (guid == Guid.Empty)
			{
				return default(GuidHandle);
			}
			GuidHandle nextGuid;
			if (this._guids.TryGetValue(guid, out nextGuid))
			{
				return nextGuid;
			}
			nextGuid = this.GetNextGuid();
			this._guids.Add(guid, nextGuid);
			this._guidWriter.WriteBytes(guid.ToByteArray());
			return nextGuid;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00014719 File Offset: 0x00012919
		public GuidHandle ReserveGuid(out Blob reservedBlob)
		{
			GuidHandle nextGuid = this.GetNextGuid();
			reservedBlob = this._guidWriter.ReserveBytes(16);
			return nextGuid;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00014734 File Offset: 0x00012934
		private GuidHandle GetNextGuid()
		{
			return MetadataTokens.GuidHandle((this._guidWriter.Count >> 4) + 1);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001474C File Offset: 0x0001294C
		public StringHandle GetString(string str)
		{
			StringHandle stringHandle;
			if (str.Length == 0)
			{
				stringHandle = default(StringHandle);
			}
			else if (!this._strings.TryGetValue(str, out stringHandle))
			{
				stringHandle = MetadataTokens.StringHandle(this._strings.Count + 1);
				this._strings.Add(str, stringHandle);
			}
			return stringHandle;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001479C File Offset: 0x0001299C
		public int GetHeapOffset(StringHandle handle)
		{
			return this._stringIndexToResolvedOffsetMap[MetadataTokens.GetHeapOffset(handle)];
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000147B0 File Offset: 0x000129B0
		public int GetHeapOffset(BlobHandle handle)
		{
			int heapOffset = MetadataTokens.GetHeapOffset(handle);
			if (heapOffset != 0)
			{
				return this._blobHeapStartOffset + heapOffset;
			}
			return 0;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000147D6 File Offset: 0x000129D6
		public int GetHeapOffset(GuidHandle handle)
		{
			return MetadataTokens.GetHeapOffset(handle);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000147E3 File Offset: 0x000129E3
		public int GetHeapOffset(UserStringHandle handle)
		{
			return MetadataTokens.GetHeapOffset(handle);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000147F0 File Offset: 0x000129F0
		public UserStringHandle GetUserString(string str)
		{
			int num;
			if (!this._userStrings.TryGetValue(str, out num))
			{
				num = this._userStringWriter.Position + this._userStringHeapStartOffset;
				this._userStrings.Add(str, num);
				this._userStringWriter.WriteCompressedInteger(str.Length * 2 + 1);
				this._userStringWriter.WriteUTF16(str);
				byte value = 0;
				foreach (char c in str)
				{
					if (c >= '\u007f')
					{
						value = 1;
						break;
					}
					switch (c)
					{
					case '\u0001':
					case '\u0002':
					case '\u0003':
					case '\u0004':
					case '\u0005':
					case '\u0006':
					case '\a':
					case '\b':
					case '\u000e':
					case '\u000f':
					case '\u0010':
					case '\u0011':
					case '\u0012':
					case '\u0013':
					case '\u0014':
					case '\u0015':
					case '\u0016':
					case '\u0017':
					case '\u0018':
					case '\u0019':
					case '\u001a':
					case '\u001b':
					case '\u001c':
					case '\u001d':
					case '\u001e':
					case '\u001f':
					case '\'':
					case '-':
						value = 1;
						goto IL_14C;
					}
				}
				IL_14C:
				this._userStringWriter.WriteByte(value);
			}
			return MetadataTokens.UserStringHandle(num);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001495B File Offset: 0x00012B5B
		internal void CompleteHeaps()
		{
			this._streamsAreComplete = true;
			this.SerializeStringHeap();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001496C File Offset: 0x00012B6C
		public ImmutableArray<int> GetHeapSizes()
		{
			int[] array = new int[MetadataTokens.HeapCount];
			array[0] = this._userStringWriter.Count;
			array[1] = this._stringWriter.Count;
			array[2] = this._blobHeapSize;
			array[3] = this._guidWriter.Count;
			return ImmutableArray.CreateRange<int>(array);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000149BC File Offset: 0x00012BBC
		private void SerializeStringHeap()
		{
			List<KeyValuePair<string, StringHandle>> list = new List<KeyValuePair<string, StringHandle>>(this._strings);
			list.Sort(new MetadataBuilder.SuffixSort());
			this._strings = null;
			this._stringWriter = new BlobBuilder(1024);
			this._stringIndexToResolvedOffsetMap = new int[list.Count + 1];
			this._stringIndexToResolvedOffsetMap[0] = 0;
			this._stringWriter.WriteByte(0);
			string text = string.Empty;
			foreach (KeyValuePair<string, StringHandle> keyValuePair in list)
			{
				int num = this._stringHeapStartOffset + this._stringWriter.Position;
				if (text.EndsWith(keyValuePair.Key, StringComparison.Ordinal) && !BlobUtilities.IsLowSurrogateChar((int)keyValuePair.Key[0]))
				{
					this._stringIndexToResolvedOffsetMap[MetadataTokens.GetHeapOffset(keyValuePair.Value)] = num - (BlobUtilities.GetUTF8ByteCount(keyValuePair.Key) + 1);
				}
				else
				{
					this._stringIndexToResolvedOffsetMap[MetadataTokens.GetHeapOffset(keyValuePair.Value)] = num;
					this._stringWriter.WriteUTF8(keyValuePair.Key, false);
					this._stringWriter.WriteByte(0);
				}
				text = keyValuePair.Key;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00014B0C File Offset: 0x00012D0C
		public void WriteHeapsTo(BlobBuilder writer)
		{
			MetadataBuilder.WriteAligned(this._stringWriter, writer);
			MetadataBuilder.WriteAligned(this._userStringWriter, writer);
			MetadataBuilder.WriteAligned(this._guidWriter, writer);
			this.WriteAlignedBlobHeap(writer);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00014B3C File Offset: 0x00012D3C
		private void WriteAlignedBlobHeap(BlobBuilder builder)
		{
			int num = BitArithmetic.Align(this._blobHeapSize, 4) - this._blobHeapSize;
			BlobWriter blobWriter = new BlobWriter(builder.ReserveBytes(this._blobHeapSize + num));
			foreach (KeyValuePair<ImmutableArray<byte>, BlobHandle> keyValuePair in this._blobs)
			{
				int heapOffset = MetadataTokens.GetHeapOffset(keyValuePair.Value);
				ImmutableArray<byte> key = keyValuePair.Key;
				blobWriter.Offset = heapOffset;
				blobWriter.WriteCompressedInteger(key.Length);
				blobWriter.WriteBytes(key);
			}
			blobWriter.Offset = this._blobHeapSize;
			blobWriter.WriteBytes(0, num);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00014C04 File Offset: 0x00012E04
		private static void WriteAligned(BlobBuilder source, BlobBuilder target)
		{
			int count = source.Count;
			target.LinkSuffix(source);
			target.WriteBytes(0, BitArithmetic.Align(count, 4) - count);
		}

		// Token: 0x0400047D RID: 1149
		private const byte MetadataFormatMajorVersion = 2;

		// Token: 0x0400047E RID: 1150
		private const byte MetadataFormatMinorVersion = 0;

		// Token: 0x0400047F RID: 1151
		private readonly List<MetadataBuilder.ModuleRow> _moduleTable = new List<MetadataBuilder.ModuleRow>(1);

		// Token: 0x04000480 RID: 1152
		private readonly List<MetadataBuilder.AssemblyRow> _assemblyTable = new List<MetadataBuilder.AssemblyRow>(1);

		// Token: 0x04000481 RID: 1153
		private readonly List<MetadataBuilder.ClassLayoutRow> _classLayoutTable = new List<MetadataBuilder.ClassLayoutRow>();

		// Token: 0x04000482 RID: 1154
		private readonly List<MetadataBuilder.ConstantRow> _constantTable = new List<MetadataBuilder.ConstantRow>();

		// Token: 0x04000483 RID: 1155
		private uint _constantTableLastParent;

		// Token: 0x04000484 RID: 1156
		private bool _constantTableNeedsSorting;

		// Token: 0x04000485 RID: 1157
		private readonly List<MetadataBuilder.CustomAttributeRow> _customAttributeTable = new List<MetadataBuilder.CustomAttributeRow>();

		// Token: 0x04000486 RID: 1158
		private uint _customAttributeTableLastParent;

		// Token: 0x04000487 RID: 1159
		private bool _customAttributeTableNeedsSorting;

		// Token: 0x04000488 RID: 1160
		private readonly List<MetadataBuilder.DeclSecurityRow> _declSecurityTable = new List<MetadataBuilder.DeclSecurityRow>();

		// Token: 0x04000489 RID: 1161
		private uint _declSecurityTableLastParent;

		// Token: 0x0400048A RID: 1162
		private bool _declSecurityTableNeedsSorting;

		// Token: 0x0400048B RID: 1163
		private readonly List<MetadataBuilder.EncLogRow> _encLogTable = new List<MetadataBuilder.EncLogRow>();

		// Token: 0x0400048C RID: 1164
		private readonly List<MetadataBuilder.EncMapRow> _encMapTable = new List<MetadataBuilder.EncMapRow>();

		// Token: 0x0400048D RID: 1165
		private readonly List<MetadataBuilder.EventRow> _eventTable = new List<MetadataBuilder.EventRow>();

		// Token: 0x0400048E RID: 1166
		private readonly List<MetadataBuilder.EventMapRow> _eventMapTable = new List<MetadataBuilder.EventMapRow>();

		// Token: 0x0400048F RID: 1167
		private readonly List<MetadataBuilder.ExportedTypeRow> _exportedTypeTable = new List<MetadataBuilder.ExportedTypeRow>();

		// Token: 0x04000490 RID: 1168
		private readonly List<MetadataBuilder.FieldLayoutRow> _fieldLayoutTable = new List<MetadataBuilder.FieldLayoutRow>();

		// Token: 0x04000491 RID: 1169
		private readonly List<MetadataBuilder.FieldMarshalRow> _fieldMarshalTable = new List<MetadataBuilder.FieldMarshalRow>();

		// Token: 0x04000492 RID: 1170
		private uint _fieldMarshalTableLastParent;

		// Token: 0x04000493 RID: 1171
		private bool _fieldMarshalTableNeedsSorting;

		// Token: 0x04000494 RID: 1172
		private readonly List<MetadataBuilder.FieldRvaRow> _fieldRvaTable = new List<MetadataBuilder.FieldRvaRow>();

		// Token: 0x04000495 RID: 1173
		private readonly List<MetadataBuilder.FieldDefRow> _fieldTable = new List<MetadataBuilder.FieldDefRow>();

		// Token: 0x04000496 RID: 1174
		private readonly List<MetadataBuilder.FileTableRow> _fileTable = new List<MetadataBuilder.FileTableRow>();

		// Token: 0x04000497 RID: 1175
		private readonly List<MetadataBuilder.GenericParamConstraintRow> _genericParamConstraintTable = new List<MetadataBuilder.GenericParamConstraintRow>();

		// Token: 0x04000498 RID: 1176
		private readonly List<MetadataBuilder.GenericParamRow> _genericParamTable = new List<MetadataBuilder.GenericParamRow>();

		// Token: 0x04000499 RID: 1177
		private readonly List<MetadataBuilder.ImplMapRow> _implMapTable = new List<MetadataBuilder.ImplMapRow>();

		// Token: 0x0400049A RID: 1178
		private readonly List<MetadataBuilder.InterfaceImplRow> _interfaceImplTable = new List<MetadataBuilder.InterfaceImplRow>();

		// Token: 0x0400049B RID: 1179
		private readonly List<MetadataBuilder.ManifestResourceRow> _manifestResourceTable = new List<MetadataBuilder.ManifestResourceRow>();

		// Token: 0x0400049C RID: 1180
		private readonly List<MetadataBuilder.MemberRefRow> _memberRefTable = new List<MetadataBuilder.MemberRefRow>();

		// Token: 0x0400049D RID: 1181
		private readonly List<MetadataBuilder.MethodImplRow> _methodImplTable = new List<MetadataBuilder.MethodImplRow>();

		// Token: 0x0400049E RID: 1182
		private readonly List<MetadataBuilder.MethodSemanticsRow> _methodSemanticsTable = new List<MetadataBuilder.MethodSemanticsRow>();

		// Token: 0x0400049F RID: 1183
		private uint _methodSemanticsTableLastAssociation;

		// Token: 0x040004A0 RID: 1184
		private bool _methodSemanticsTableNeedsSorting;

		// Token: 0x040004A1 RID: 1185
		private readonly List<MetadataBuilder.MethodSpecRow> _methodSpecTable = new List<MetadataBuilder.MethodSpecRow>();

		// Token: 0x040004A2 RID: 1186
		private readonly List<MetadataBuilder.MethodRow> _methodDefTable = new List<MetadataBuilder.MethodRow>();

		// Token: 0x040004A3 RID: 1187
		private readonly List<MetadataBuilder.ModuleRefRow> _moduleRefTable = new List<MetadataBuilder.ModuleRefRow>();

		// Token: 0x040004A4 RID: 1188
		private readonly List<MetadataBuilder.NestedClassRow> _nestedClassTable = new List<MetadataBuilder.NestedClassRow>();

		// Token: 0x040004A5 RID: 1189
		private readonly List<MetadataBuilder.ParamRow> _paramTable = new List<MetadataBuilder.ParamRow>();

		// Token: 0x040004A6 RID: 1190
		private readonly List<MetadataBuilder.PropertyMapRow> _propertyMapTable = new List<MetadataBuilder.PropertyMapRow>();

		// Token: 0x040004A7 RID: 1191
		private readonly List<MetadataBuilder.PropertyRow> _propertyTable = new List<MetadataBuilder.PropertyRow>();

		// Token: 0x040004A8 RID: 1192
		private readonly List<MetadataBuilder.TypeDefRow> _typeDefTable = new List<MetadataBuilder.TypeDefRow>();

		// Token: 0x040004A9 RID: 1193
		private readonly List<MetadataBuilder.TypeRefRow> _typeRefTable = new List<MetadataBuilder.TypeRefRow>();

		// Token: 0x040004AA RID: 1194
		private readonly List<MetadataBuilder.TypeSpecRow> _typeSpecTable = new List<MetadataBuilder.TypeSpecRow>();

		// Token: 0x040004AB RID: 1195
		private readonly List<MetadataBuilder.AssemblyRefTableRow> _assemblyRefTable = new List<MetadataBuilder.AssemblyRefTableRow>();

		// Token: 0x040004AC RID: 1196
		private readonly List<MetadataBuilder.StandaloneSigRow> _standAloneSigTable = new List<MetadataBuilder.StandaloneSigRow>();

		// Token: 0x040004AD RID: 1197
		private readonly List<MetadataBuilder.DocumentRow> _documentTable = new List<MetadataBuilder.DocumentRow>();

		// Token: 0x040004AE RID: 1198
		private readonly List<MetadataBuilder.MethodDebugInformationRow> _methodDebugInformationTable = new List<MetadataBuilder.MethodDebugInformationRow>();

		// Token: 0x040004AF RID: 1199
		private readonly List<MetadataBuilder.LocalScopeRow> _localScopeTable = new List<MetadataBuilder.LocalScopeRow>();

		// Token: 0x040004B0 RID: 1200
		private readonly List<MetadataBuilder.LocalVariableRow> _localVariableTable = new List<MetadataBuilder.LocalVariableRow>();

		// Token: 0x040004B1 RID: 1201
		private readonly List<MetadataBuilder.LocalConstantRow> _localConstantTable = new List<MetadataBuilder.LocalConstantRow>();

		// Token: 0x040004B2 RID: 1202
		private readonly List<MetadataBuilder.ImportScopeRow> _importScopeTable = new List<MetadataBuilder.ImportScopeRow>();

		// Token: 0x040004B3 RID: 1203
		private readonly List<MetadataBuilder.StateMachineMethodRow> _stateMachineMethodTable = new List<MetadataBuilder.StateMachineMethodRow>();

		// Token: 0x040004B4 RID: 1204
		private readonly List<MetadataBuilder.CustomDebugInformationRow> _customDebugInformationTable = new List<MetadataBuilder.CustomDebugInformationRow>();

		// Token: 0x040004B5 RID: 1205
		private readonly Dictionary<string, int> _userStrings = new Dictionary<string, int>();

		// Token: 0x040004B6 RID: 1206
		private readonly BlobBuilder _userStringWriter = new BlobBuilder(1024);

		// Token: 0x040004B7 RID: 1207
		private readonly int _userStringHeapStartOffset;

		// Token: 0x040004B8 RID: 1208
		private Dictionary<string, StringHandle> _strings = new Dictionary<string, StringHandle>(128);

		// Token: 0x040004B9 RID: 1209
		private int[] _stringIndexToResolvedOffsetMap;

		// Token: 0x040004BA RID: 1210
		private BlobBuilder _stringWriter;

		// Token: 0x040004BB RID: 1211
		private readonly int _stringHeapStartOffset;

		// Token: 0x040004BC RID: 1212
		private readonly Dictionary<ImmutableArray<byte>, BlobHandle> _blobs = new Dictionary<ImmutableArray<byte>, BlobHandle>(ByteSequenceComparer.Instance);

		// Token: 0x040004BD RID: 1213
		private readonly int _blobHeapStartOffset;

		// Token: 0x040004BE RID: 1214
		private int _blobHeapSize;

		// Token: 0x040004BF RID: 1215
		private readonly Dictionary<Guid, GuidHandle> _guids = new Dictionary<Guid, GuidHandle>();

		// Token: 0x040004C0 RID: 1216
		private readonly BlobBuilder _guidWriter = new BlobBuilder(16);

		// Token: 0x040004C1 RID: 1217
		private bool _streamsAreComplete;

		// Token: 0x020001A1 RID: 417
		private struct AssemblyRefTableRow
		{
			// Token: 0x04000A98 RID: 2712
			public Version Version;

			// Token: 0x04000A99 RID: 2713
			public BlobHandle PublicKeyToken;

			// Token: 0x04000A9A RID: 2714
			public StringHandle Name;

			// Token: 0x04000A9B RID: 2715
			public StringHandle Culture;

			// Token: 0x04000A9C RID: 2716
			public uint Flags;

			// Token: 0x04000A9D RID: 2717
			public BlobHandle HashValue;
		}

		// Token: 0x020001A2 RID: 418
		private struct ModuleRow
		{
			// Token: 0x04000A9E RID: 2718
			public ushort Generation;

			// Token: 0x04000A9F RID: 2719
			public StringHandle Name;

			// Token: 0x04000AA0 RID: 2720
			public GuidHandle ModuleVersionId;

			// Token: 0x04000AA1 RID: 2721
			public GuidHandle EncId;

			// Token: 0x04000AA2 RID: 2722
			public GuidHandle EncBaseId;
		}

		// Token: 0x020001A3 RID: 419
		private struct AssemblyRow
		{
			// Token: 0x04000AA3 RID: 2723
			public uint HashAlgorithm;

			// Token: 0x04000AA4 RID: 2724
			public Version Version;

			// Token: 0x04000AA5 RID: 2725
			public ushort Flags;

			// Token: 0x04000AA6 RID: 2726
			public BlobHandle AssemblyKey;

			// Token: 0x04000AA7 RID: 2727
			public StringHandle AssemblyName;

			// Token: 0x04000AA8 RID: 2728
			public StringHandle AssemblyCulture;
		}

		// Token: 0x020001A4 RID: 420
		private struct ClassLayoutRow
		{
			// Token: 0x04000AA9 RID: 2729
			public ushort PackingSize;

			// Token: 0x04000AAA RID: 2730
			public uint ClassSize;

			// Token: 0x04000AAB RID: 2731
			public uint Parent;
		}

		// Token: 0x020001A5 RID: 421
		private struct ConstantRow
		{
			// Token: 0x04000AAC RID: 2732
			public byte Type;

			// Token: 0x04000AAD RID: 2733
			public uint Parent;

			// Token: 0x04000AAE RID: 2734
			public BlobHandle Value;
		}

		// Token: 0x020001A6 RID: 422
		private struct CustomAttributeRow
		{
			// Token: 0x04000AAF RID: 2735
			public uint Parent;

			// Token: 0x04000AB0 RID: 2736
			public uint Type;

			// Token: 0x04000AB1 RID: 2737
			public BlobHandle Value;
		}

		// Token: 0x020001A7 RID: 423
		private struct DeclSecurityRow
		{
			// Token: 0x04000AB2 RID: 2738
			public ushort Action;

			// Token: 0x04000AB3 RID: 2739
			public uint Parent;

			// Token: 0x04000AB4 RID: 2740
			public BlobHandle PermissionSet;
		}

		// Token: 0x020001A8 RID: 424
		private struct EncLogRow
		{
			// Token: 0x04000AB5 RID: 2741
			public uint Token;

			// Token: 0x04000AB6 RID: 2742
			public byte FuncCode;
		}

		// Token: 0x020001A9 RID: 425
		private struct EncMapRow
		{
			// Token: 0x04000AB7 RID: 2743
			public uint Token;
		}

		// Token: 0x020001AA RID: 426
		private struct EventRow
		{
			// Token: 0x04000AB8 RID: 2744
			public ushort EventFlags;

			// Token: 0x04000AB9 RID: 2745
			public StringHandle Name;

			// Token: 0x04000ABA RID: 2746
			public uint EventType;
		}

		// Token: 0x020001AB RID: 427
		private struct EventMapRow
		{
			// Token: 0x04000ABB RID: 2747
			public uint Parent;

			// Token: 0x04000ABC RID: 2748
			public uint EventList;
		}

		// Token: 0x020001AC RID: 428
		private struct ExportedTypeRow
		{
			// Token: 0x04000ABD RID: 2749
			public uint Flags;

			// Token: 0x04000ABE RID: 2750
			public uint TypeDefId;

			// Token: 0x04000ABF RID: 2751
			public StringHandle TypeName;

			// Token: 0x04000AC0 RID: 2752
			public StringHandle TypeNamespace;

			// Token: 0x04000AC1 RID: 2753
			public uint Implementation;
		}

		// Token: 0x020001AD RID: 429
		private struct FieldLayoutRow
		{
			// Token: 0x04000AC2 RID: 2754
			public uint Offset;

			// Token: 0x04000AC3 RID: 2755
			public uint Field;
		}

		// Token: 0x020001AE RID: 430
		private struct FieldMarshalRow
		{
			// Token: 0x04000AC4 RID: 2756
			public uint Parent;

			// Token: 0x04000AC5 RID: 2757
			public BlobHandle NativeType;
		}

		// Token: 0x020001AF RID: 431
		private struct FieldRvaRow
		{
			// Token: 0x04000AC6 RID: 2758
			public uint Offset;

			// Token: 0x04000AC7 RID: 2759
			public uint Field;
		}

		// Token: 0x020001B0 RID: 432
		private struct FieldDefRow
		{
			// Token: 0x04000AC8 RID: 2760
			public ushort Flags;

			// Token: 0x04000AC9 RID: 2761
			public StringHandle Name;

			// Token: 0x04000ACA RID: 2762
			public BlobHandle Signature;
		}

		// Token: 0x020001B1 RID: 433
		private struct FileTableRow
		{
			// Token: 0x04000ACB RID: 2763
			public uint Flags;

			// Token: 0x04000ACC RID: 2764
			public StringHandle FileName;

			// Token: 0x04000ACD RID: 2765
			public BlobHandle HashValue;
		}

		// Token: 0x020001B2 RID: 434
		private struct GenericParamConstraintRow
		{
			// Token: 0x04000ACE RID: 2766
			public uint Owner;

			// Token: 0x04000ACF RID: 2767
			public uint Constraint;
		}

		// Token: 0x020001B3 RID: 435
		private struct GenericParamRow
		{
			// Token: 0x04000AD0 RID: 2768
			public ushort Number;

			// Token: 0x04000AD1 RID: 2769
			public ushort Flags;

			// Token: 0x04000AD2 RID: 2770
			public uint Owner;

			// Token: 0x04000AD3 RID: 2771
			public StringHandle Name;
		}

		// Token: 0x020001B4 RID: 436
		private struct ImplMapRow
		{
			// Token: 0x04000AD4 RID: 2772
			public ushort MappingFlags;

			// Token: 0x04000AD5 RID: 2773
			public uint MemberForwarded;

			// Token: 0x04000AD6 RID: 2774
			public StringHandle ImportName;

			// Token: 0x04000AD7 RID: 2775
			public uint ImportScope;
		}

		// Token: 0x020001B5 RID: 437
		private struct InterfaceImplRow
		{
			// Token: 0x04000AD8 RID: 2776
			public uint Class;

			// Token: 0x04000AD9 RID: 2777
			public uint Interface;
		}

		// Token: 0x020001B6 RID: 438
		private struct ManifestResourceRow
		{
			// Token: 0x04000ADA RID: 2778
			public uint Offset;

			// Token: 0x04000ADB RID: 2779
			public uint Flags;

			// Token: 0x04000ADC RID: 2780
			public StringHandle Name;

			// Token: 0x04000ADD RID: 2781
			public uint Implementation;
		}

		// Token: 0x020001B7 RID: 439
		private struct MemberRefRow
		{
			// Token: 0x04000ADE RID: 2782
			public uint Class;

			// Token: 0x04000ADF RID: 2783
			public StringHandle Name;

			// Token: 0x04000AE0 RID: 2784
			public BlobHandle Signature;
		}

		// Token: 0x020001B8 RID: 440
		private struct MethodImplRow
		{
			// Token: 0x04000AE1 RID: 2785
			public uint Class;

			// Token: 0x04000AE2 RID: 2786
			public uint MethodBody;

			// Token: 0x04000AE3 RID: 2787
			public uint MethodDecl;
		}

		// Token: 0x020001B9 RID: 441
		private struct MethodSemanticsRow
		{
			// Token: 0x04000AE4 RID: 2788
			public ushort Semantic;

			// Token: 0x04000AE5 RID: 2789
			public uint Method;

			// Token: 0x04000AE6 RID: 2790
			public uint Association;
		}

		// Token: 0x020001BA RID: 442
		private struct MethodSpecRow
		{
			// Token: 0x04000AE7 RID: 2791
			public uint Method;

			// Token: 0x04000AE8 RID: 2792
			public BlobHandle Instantiation;
		}

		// Token: 0x020001BB RID: 443
		private struct MethodRow
		{
			// Token: 0x04000AE9 RID: 2793
			public int BodyOffset;

			// Token: 0x04000AEA RID: 2794
			public ushort ImplFlags;

			// Token: 0x04000AEB RID: 2795
			public ushort Flags;

			// Token: 0x04000AEC RID: 2796
			public StringHandle Name;

			// Token: 0x04000AED RID: 2797
			public BlobHandle Signature;

			// Token: 0x04000AEE RID: 2798
			public uint ParamList;
		}

		// Token: 0x020001BC RID: 444
		private struct ModuleRefRow
		{
			// Token: 0x04000AEF RID: 2799
			public StringHandle Name;
		}

		// Token: 0x020001BD RID: 445
		private struct NestedClassRow
		{
			// Token: 0x04000AF0 RID: 2800
			public uint NestedClass;

			// Token: 0x04000AF1 RID: 2801
			public uint EnclosingClass;
		}

		// Token: 0x020001BE RID: 446
		private struct ParamRow
		{
			// Token: 0x04000AF2 RID: 2802
			public ushort Flags;

			// Token: 0x04000AF3 RID: 2803
			public ushort Sequence;

			// Token: 0x04000AF4 RID: 2804
			public StringHandle Name;
		}

		// Token: 0x020001BF RID: 447
		private struct PropertyMapRow
		{
			// Token: 0x04000AF5 RID: 2805
			public uint Parent;

			// Token: 0x04000AF6 RID: 2806
			public uint PropertyList;
		}

		// Token: 0x020001C0 RID: 448
		private struct PropertyRow
		{
			// Token: 0x04000AF7 RID: 2807
			public ushort PropFlags;

			// Token: 0x04000AF8 RID: 2808
			public StringHandle Name;

			// Token: 0x04000AF9 RID: 2809
			public BlobHandle Type;
		}

		// Token: 0x020001C1 RID: 449
		private struct TypeDefRow
		{
			// Token: 0x04000AFA RID: 2810
			public uint Flags;

			// Token: 0x04000AFB RID: 2811
			public StringHandle Name;

			// Token: 0x04000AFC RID: 2812
			public StringHandle Namespace;

			// Token: 0x04000AFD RID: 2813
			public uint Extends;

			// Token: 0x04000AFE RID: 2814
			public uint FieldList;

			// Token: 0x04000AFF RID: 2815
			public uint MethodList;
		}

		// Token: 0x020001C2 RID: 450
		private struct TypeRefRow
		{
			// Token: 0x04000B00 RID: 2816
			public uint ResolutionScope;

			// Token: 0x04000B01 RID: 2817
			public StringHandle Name;

			// Token: 0x04000B02 RID: 2818
			public StringHandle Namespace;
		}

		// Token: 0x020001C3 RID: 451
		private struct TypeSpecRow
		{
			// Token: 0x04000B03 RID: 2819
			public BlobHandle Signature;
		}

		// Token: 0x020001C4 RID: 452
		private struct StandaloneSigRow
		{
			// Token: 0x04000B04 RID: 2820
			public BlobHandle Signature;
		}

		// Token: 0x020001C5 RID: 453
		private struct DocumentRow
		{
			// Token: 0x04000B05 RID: 2821
			public BlobHandle Name;

			// Token: 0x04000B06 RID: 2822
			public GuidHandle HashAlgorithm;

			// Token: 0x04000B07 RID: 2823
			public BlobHandle Hash;

			// Token: 0x04000B08 RID: 2824
			public GuidHandle Language;
		}

		// Token: 0x020001C6 RID: 454
		private struct MethodDebugInformationRow
		{
			// Token: 0x04000B09 RID: 2825
			public uint Document;

			// Token: 0x04000B0A RID: 2826
			public BlobHandle SequencePoints;
		}

		// Token: 0x020001C7 RID: 455
		private struct LocalScopeRow
		{
			// Token: 0x04000B0B RID: 2827
			public uint Method;

			// Token: 0x04000B0C RID: 2828
			public uint ImportScope;

			// Token: 0x04000B0D RID: 2829
			public uint VariableList;

			// Token: 0x04000B0E RID: 2830
			public uint ConstantList;

			// Token: 0x04000B0F RID: 2831
			public uint StartOffset;

			// Token: 0x04000B10 RID: 2832
			public uint Length;
		}

		// Token: 0x020001C8 RID: 456
		private struct LocalVariableRow
		{
			// Token: 0x04000B11 RID: 2833
			public ushort Attributes;

			// Token: 0x04000B12 RID: 2834
			public ushort Index;

			// Token: 0x04000B13 RID: 2835
			public StringHandle Name;
		}

		// Token: 0x020001C9 RID: 457
		private struct LocalConstantRow
		{
			// Token: 0x04000B14 RID: 2836
			public StringHandle Name;

			// Token: 0x04000B15 RID: 2837
			public BlobHandle Signature;
		}

		// Token: 0x020001CA RID: 458
		private struct ImportScopeRow
		{
			// Token: 0x04000B16 RID: 2838
			public uint Parent;

			// Token: 0x04000B17 RID: 2839
			public BlobHandle Imports;
		}

		// Token: 0x020001CB RID: 459
		private struct StateMachineMethodRow
		{
			// Token: 0x04000B18 RID: 2840
			public uint MoveNextMethod;

			// Token: 0x04000B19 RID: 2841
			public uint KickoffMethod;
		}

		// Token: 0x020001CC RID: 460
		private struct CustomDebugInformationRow
		{
			// Token: 0x04000B1A RID: 2842
			public uint Parent;

			// Token: 0x04000B1B RID: 2843
			public GuidHandle Kind;

			// Token: 0x04000B1C RID: 2844
			public BlobHandle Value;
		}

		// Token: 0x020001CD RID: 461
		private class SuffixSort : IComparer<KeyValuePair<string, StringHandle>>
		{
			// Token: 0x06000C27 RID: 3111 RVA: 0x00021FA0 File Offset: 0x000201A0
			public int Compare(KeyValuePair<string, StringHandle> xPair, KeyValuePair<string, StringHandle> yPair)
			{
				string key = xPair.Key;
				string key2 = yPair.Key;
				int num = key.Length - 1;
				int num2 = key2.Length - 1;
				while (num >= 0 & num2 >= 0)
				{
					if (key[num] < key2[num2])
					{
						return -1;
					}
					if (key[num] > key2[num2])
					{
						return 1;
					}
					num--;
					num2--;
				}
				return key2.Length.CompareTo(key.Length);
			}
		}
	}
}
