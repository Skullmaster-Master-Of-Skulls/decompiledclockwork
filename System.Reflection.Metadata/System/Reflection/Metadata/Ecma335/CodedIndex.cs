using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000B3 RID: 179
	internal static class CodedIndex
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x000107C8 File Offset: 0x0000E9C8
		private static int ToCodedIndex(this int rowId, CodedIndex.HasCustomAttribute tag)
		{
			return rowId << 5 | (int)tag;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000107CF File Offset: 0x0000E9CF
		private static int ToCodedIndex(this int rowId, CodedIndex.HasConstant tag)
		{
			return rowId << 2 | (int)tag;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000107D6 File Offset: 0x0000E9D6
		private static int ToCodedIndex(this int rowId, CodedIndex.CustomAttributeType tag)
		{
			return rowId << 3 | (int)tag;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000107CF File Offset: 0x0000E9CF
		private static int ToCodedIndex(this int rowId, CodedIndex.HasDeclSecurity tag)
		{
			return rowId << 2 | (int)tag;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000107DD File Offset: 0x0000E9DD
		private static int ToCodedIndex(this int rowId, CodedIndex.HasFieldMarshal tag)
		{
			return rowId << 1 | (int)tag;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000107DD File Offset: 0x0000E9DD
		private static int ToCodedIndex(this int rowId, CodedIndex.HasSemantics tag)
		{
			return rowId << 1 | (int)tag;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000107CF File Offset: 0x0000E9CF
		private static int ToCodedIndex(this int rowId, CodedIndex.Implementation tag)
		{
			return rowId << 2 | (int)tag;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000107DD File Offset: 0x0000E9DD
		private static int ToCodedIndex(this int rowId, CodedIndex.MemberForwarded tag)
		{
			return rowId << 1 | (int)tag;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000107D6 File Offset: 0x0000E9D6
		private static int ToCodedIndex(this int rowId, CodedIndex.MemberRefParent tag)
		{
			return rowId << 3 | (int)tag;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000107DD File Offset: 0x0000E9DD
		private static int ToCodedIndex(this int rowId, CodedIndex.MethodDefOrRef tag)
		{
			return rowId << 1 | (int)tag;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000107CF File Offset: 0x0000E9CF
		private static int ToCodedIndex(this int rowId, CodedIndex.ResolutionScope tag)
		{
			return rowId << 2 | (int)tag;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000107CF File Offset: 0x0000E9CF
		private static int ToCodedIndex(this int rowId, CodedIndex.TypeDefOrRefOrSpec tag)
		{
			return rowId << 2 | (int)tag;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000107DD File Offset: 0x0000E9DD
		private static int ToCodedIndex(this int rowId, CodedIndex.TypeOrMethodDef tag)
		{
			return rowId << 1 | (int)tag;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000107C8 File Offset: 0x0000E9C8
		private static int ToCodedIndex(this int rowId, CodedIndex.HasCustomDebugInformation tag)
		{
			return rowId << 5 | (int)tag;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public static int ToHasCustomAttribute(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToHasCustomAttributeTag(handle.Kind));
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000107FD File Offset: 0x0000E9FD
		public static int ToHasConstant(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToHasConstantTag(handle.Kind));
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00010816 File Offset: 0x0000EA16
		public static int ToCustomAttributeType(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToCustomAttributeTypeTag(handle.Kind));
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001082F File Offset: 0x0000EA2F
		public static int ToHasDeclSecurity(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToHasDeclSecurityTag(handle.Kind));
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00010848 File Offset: 0x0000EA48
		public static int ToHasFieldMarshal(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToHasFieldMarshalTag(handle.Kind));
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00010861 File Offset: 0x0000EA61
		public static int ToHasSemantics(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToHasSemanticsTag(handle.Kind));
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001087A File Offset: 0x0000EA7A
		public static int ToImplementation(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToImplementationTag(handle.Kind));
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00010893 File Offset: 0x0000EA93
		public static int ToMemberForwarded(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToMemberForwardedTag(handle.Kind));
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000108AC File Offset: 0x0000EAAC
		public static int ToMemberRefParent(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToMemberRefParentTag(handle.Kind));
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000108C5 File Offset: 0x0000EAC5
		public static int ToMethodDefOrRef(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToMethodDefOrRefTag(handle.Kind));
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000108DE File Offset: 0x0000EADE
		public static int ToResolutionScope(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToResolutionScopeTag(handle.Kind));
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000108F7 File Offset: 0x0000EAF7
		public static int ToTypeDefOrRefOrSpec(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToTypeDefOrRefOrSpecTag(handle.Kind));
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00010910 File Offset: 0x0000EB10
		public static int ToTypeOrMethodDef(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToTypeOrMethodDefTag(handle.Kind));
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00010929 File Offset: 0x0000EB29
		public static int ToHasCustomDebugInformation(EntityHandle handle)
		{
			return MetadataTokens.GetRowNumber(handle).ToCodedIndex(CodedIndex.ToHasCustomDebugInformationTag(handle.Kind));
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00010944 File Offset: 0x0000EB44
		private static CodedIndex.HasCustomAttribute ToHasCustomAttributeTag(HandleKind kind)
		{
			switch (kind)
			{
			case HandleKind.ModuleDefinition:
				return CodedIndex.HasCustomAttribute.Module;
			case HandleKind.TypeReference:
				return CodedIndex.HasCustomAttribute.TypeRef;
			case HandleKind.TypeDefinition:
				return CodedIndex.HasCustomAttribute.TypeDef;
			case (HandleKind)3:
			case (HandleKind)5:
			case (HandleKind)7:
			case HandleKind.Constant:
			case HandleKind.CustomAttribute:
			case (HandleKind)13:
			case (HandleKind)15:
			case (HandleKind)16:
			case (HandleKind)18:
			case (HandleKind)19:
				break;
			case HandleKind.FieldDefinition:
				return CodedIndex.HasCustomAttribute.Field;
			case HandleKind.MethodDefinition:
				return CodedIndex.HasCustomAttribute.MethodDef;
			case HandleKind.Parameter:
				return CodedIndex.HasCustomAttribute.Param;
			case HandleKind.InterfaceImplementation:
				return CodedIndex.HasCustomAttribute.InterfaceImpl;
			case HandleKind.MemberReference:
				return CodedIndex.HasCustomAttribute.MemberRef;
			case HandleKind.DeclarativeSecurityAttribute:
				return CodedIndex.HasCustomAttribute.DeclSecurity;
			case HandleKind.StandaloneSignature:
				return CodedIndex.HasCustomAttribute.StandAloneSig;
			case HandleKind.EventDefinition:
				return CodedIndex.HasCustomAttribute.Event;
			default:
				switch (kind)
				{
				case HandleKind.PropertyDefinition:
					return CodedIndex.HasCustomAttribute.Property;
				case (HandleKind)24:
				case HandleKind.MethodImplementation:
					break;
				case HandleKind.ModuleReference:
					return CodedIndex.HasCustomAttribute.ModuleRef;
				case HandleKind.TypeSpecification:
					return CodedIndex.HasCustomAttribute.TypeSpec;
				default:
					switch (kind)
					{
					case HandleKind.AssemblyDefinition:
						return CodedIndex.HasCustomAttribute.Assembly;
					case HandleKind.AssemblyReference:
						return CodedIndex.HasCustomAttribute.AssemblyRef;
					case HandleKind.AssemblyFile:
						return CodedIndex.HasCustomAttribute.File;
					case HandleKind.ExportedType:
						return CodedIndex.HasCustomAttribute.ExportedType;
					case HandleKind.ManifestResource:
						return CodedIndex.HasCustomAttribute.ManifestResource;
					case HandleKind.GenericParameter:
						return CodedIndex.HasCustomAttribute.GenericParam;
					case HandleKind.MethodSpecification:
						return CodedIndex.HasCustomAttribute.MethodSpec;
					case HandleKind.GenericParameterConstraint:
						return CodedIndex.HasCustomAttribute.GenericParamConstraint;
					}
					break;
				}
				break;
			}
			throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
			{
				kind
			}));
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00010A5E File Offset: 0x0000EC5E
		private static CodedIndex.HasConstant ToHasConstantTag(HandleKind kind)
		{
			if (kind == HandleKind.FieldDefinition)
			{
				return CodedIndex.HasConstant.Field;
			}
			if (kind == HandleKind.Parameter)
			{
				return CodedIndex.HasConstant.Param;
			}
			if (kind != HandleKind.PropertyDefinition)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.HasConstant.Property;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00010A93 File Offset: 0x0000EC93
		private static CodedIndex.CustomAttributeType ToCustomAttributeTypeTag(HandleKind kind)
		{
			if (kind == HandleKind.MethodDefinition)
			{
				return CodedIndex.CustomAttributeType.MethodDef;
			}
			if (kind != HandleKind.MemberReference)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.CustomAttributeType.MemberRef;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00010AC2 File Offset: 0x0000ECC2
		private static CodedIndex.HasDeclSecurity ToHasDeclSecurityTag(HandleKind kind)
		{
			if (kind == HandleKind.TypeDefinition)
			{
				return CodedIndex.HasDeclSecurity.TypeDef;
			}
			if (kind == HandleKind.MethodDefinition)
			{
				return CodedIndex.HasDeclSecurity.MethodDef;
			}
			if (kind != HandleKind.AssemblyDefinition)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.HasDeclSecurity.Assembly;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00010AF7 File Offset: 0x0000ECF7
		private static CodedIndex.HasFieldMarshal ToHasFieldMarshalTag(HandleKind kind)
		{
			if (kind == HandleKind.FieldDefinition)
			{
				return CodedIndex.HasFieldMarshal.Field;
			}
			if (kind != HandleKind.Parameter)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.HasFieldMarshal.Param;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00010B25 File Offset: 0x0000ED25
		private static CodedIndex.HasSemantics ToHasSemanticsTag(HandleKind kind)
		{
			if (kind == HandleKind.EventDefinition)
			{
				return CodedIndex.HasSemantics.Event;
			}
			if (kind != HandleKind.PropertyDefinition)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.HasSemantics.Property;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00010B58 File Offset: 0x0000ED58
		private static CodedIndex.Implementation ToImplementationTag(HandleKind kind)
		{
			switch (kind)
			{
			case HandleKind.AssemblyReference:
				return CodedIndex.Implementation.AssemblyRef;
			case HandleKind.AssemblyFile:
				return CodedIndex.Implementation.File;
			case HandleKind.ExportedType:
				return CodedIndex.Implementation.ExportedType;
			}
			throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
			{
				kind
			}));
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		private static CodedIndex.MemberForwarded ToMemberForwardedTag(HandleKind kind)
		{
			if (kind == HandleKind.FieldDefinition)
			{
				return CodedIndex.MemberForwarded.Field;
			}
			if (kind != HandleKind.MethodDefinition)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.MemberForwarded.MethodDef;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00010BD8 File Offset: 0x0000EDD8
		private static CodedIndex.MemberRefParent ToMemberRefParentTag(HandleKind kind)
		{
			if (kind <= HandleKind.TypeDefinition)
			{
				if (kind == HandleKind.TypeReference)
				{
					return CodedIndex.MemberRefParent.TypeRef;
				}
				if (kind == HandleKind.TypeDefinition)
				{
					return CodedIndex.MemberRefParent.TypeDef;
				}
			}
			else
			{
				if (kind == HandleKind.MethodDefinition)
				{
					return CodedIndex.MemberRefParent.MethodDef;
				}
				if (kind == HandleKind.ModuleReference)
				{
					return CodedIndex.MemberRefParent.ModuleRef;
				}
				if (kind == HandleKind.TypeSpecification)
				{
					return CodedIndex.MemberRefParent.TypeSpec;
				}
			}
			throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
			{
				kind
			}));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00010C2B File Offset: 0x0000EE2B
		private static CodedIndex.MethodDefOrRef ToMethodDefOrRefTag(HandleKind kind)
		{
			if (kind == HandleKind.MethodDefinition)
			{
				return CodedIndex.MethodDefOrRef.MethodDef;
			}
			if (kind != HandleKind.MemberReference)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.MethodDefOrRef.MemberRef;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00010C5C File Offset: 0x0000EE5C
		private static CodedIndex.ResolutionScope ToResolutionScopeTag(HandleKind kind)
		{
			if (kind <= HandleKind.TypeReference)
			{
				if (kind == HandleKind.ModuleDefinition)
				{
					return CodedIndex.ResolutionScope.Module;
				}
				if (kind == HandleKind.TypeReference)
				{
					return CodedIndex.ResolutionScope.TypeRef;
				}
			}
			else
			{
				if (kind == HandleKind.ModuleReference)
				{
					return CodedIndex.ResolutionScope.ModuleRef;
				}
				if (kind == HandleKind.AssemblyReference)
				{
					return CodedIndex.ResolutionScope.AssemblyRef;
				}
			}
			throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
			{
				kind
			}));
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		private static CodedIndex.TypeDefOrRefOrSpec ToTypeDefOrRefOrSpecTag(HandleKind kind)
		{
			if (kind == HandleKind.TypeReference)
			{
				return CodedIndex.TypeDefOrRefOrSpec.TypeRef;
			}
			if (kind == HandleKind.TypeDefinition)
			{
				return CodedIndex.TypeDefOrRefOrSpec.TypeDef;
			}
			if (kind != HandleKind.TypeSpecification)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.TypeDefOrRefOrSpec.TypeSpec;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00010CDD File Offset: 0x0000EEDD
		private static CodedIndex.TypeOrMethodDef ToTypeOrMethodDefTag(HandleKind kind)
		{
			if (kind == HandleKind.TypeDefinition)
			{
				return CodedIndex.TypeOrMethodDef.TypeDef;
			}
			if (kind != HandleKind.MethodDefinition)
			{
				throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
				{
					kind
				}));
			}
			return CodedIndex.TypeOrMethodDef.MethodDef;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00010D0C File Offset: 0x0000EF0C
		private static CodedIndex.HasCustomDebugInformation ToHasCustomDebugInformationTag(HandleKind kind)
		{
			switch (kind)
			{
			case HandleKind.ModuleDefinition:
				return CodedIndex.HasCustomDebugInformation.Module;
			case HandleKind.TypeReference:
				return CodedIndex.HasCustomDebugInformation.TypeRef;
			case HandleKind.TypeDefinition:
				return CodedIndex.HasCustomDebugInformation.TypeDef;
			case (HandleKind)3:
			case (HandleKind)5:
			case (HandleKind)7:
			case HandleKind.Constant:
			case HandleKind.CustomAttribute:
			case (HandleKind)13:
			case (HandleKind)15:
			case (HandleKind)16:
			case (HandleKind)18:
			case (HandleKind)19:
				break;
			case HandleKind.FieldDefinition:
				return CodedIndex.HasCustomDebugInformation.Field;
			case HandleKind.MethodDefinition:
				return CodedIndex.HasCustomDebugInformation.MethodDef;
			case HandleKind.Parameter:
				return CodedIndex.HasCustomDebugInformation.Param;
			case HandleKind.InterfaceImplementation:
				return CodedIndex.HasCustomDebugInformation.InterfaceImpl;
			case HandleKind.MemberReference:
				return CodedIndex.HasCustomDebugInformation.MemberRef;
			case HandleKind.DeclarativeSecurityAttribute:
				return CodedIndex.HasCustomDebugInformation.DeclSecurity;
			case HandleKind.StandaloneSignature:
				return CodedIndex.HasCustomDebugInformation.StandAloneSig;
			case HandleKind.EventDefinition:
				return CodedIndex.HasCustomDebugInformation.Event;
			default:
				switch (kind)
				{
				case HandleKind.PropertyDefinition:
					return CodedIndex.HasCustomDebugInformation.Property;
				case HandleKind.ModuleReference:
					return CodedIndex.HasCustomDebugInformation.ModuleRef;
				case HandleKind.TypeSpecification:
					return CodedIndex.HasCustomDebugInformation.TypeSpec;
				case HandleKind.AssemblyDefinition:
					return CodedIndex.HasCustomDebugInformation.Assembly;
				case HandleKind.AssemblyReference:
					return CodedIndex.HasCustomDebugInformation.AssemblyRef;
				case HandleKind.AssemblyFile:
					return CodedIndex.HasCustomDebugInformation.File;
				case HandleKind.ExportedType:
					return CodedIndex.HasCustomDebugInformation.ExportedType;
				case HandleKind.ManifestResource:
					return CodedIndex.HasCustomDebugInformation.ManifestResource;
				case HandleKind.GenericParameter:
					return CodedIndex.HasCustomDebugInformation.GenericParam;
				case HandleKind.MethodSpecification:
					return CodedIndex.HasCustomDebugInformation.MethodSpec;
				case HandleKind.GenericParameterConstraint:
					return CodedIndex.HasCustomDebugInformation.GenericParamConstraint;
				case HandleKind.Document:
					return CodedIndex.HasCustomDebugInformation.Document;
				case HandleKind.LocalScope:
					return CodedIndex.HasCustomDebugInformation.LocalScope;
				case HandleKind.LocalVariable:
					return CodedIndex.HasCustomDebugInformation.LocalVariable;
				case HandleKind.LocalConstant:
					return CodedIndex.HasCustomDebugInformation.LocalConstant;
				case HandleKind.ImportScope:
					return CodedIndex.HasCustomDebugInformation.ImportScope;
				}
				break;
			}
			throw new ArgumentException(string.Format("Unexpected kind of handle: {0}", new object[]
			{
				kind
			}));
		}

		// Token: 0x02000193 RID: 403
		private enum HasCustomAttribute
		{
			// Token: 0x04000A28 RID: 2600
			MethodDef,
			// Token: 0x04000A29 RID: 2601
			Field,
			// Token: 0x04000A2A RID: 2602
			TypeRef,
			// Token: 0x04000A2B RID: 2603
			TypeDef,
			// Token: 0x04000A2C RID: 2604
			Param,
			// Token: 0x04000A2D RID: 2605
			InterfaceImpl,
			// Token: 0x04000A2E RID: 2606
			MemberRef,
			// Token: 0x04000A2F RID: 2607
			Module,
			// Token: 0x04000A30 RID: 2608
			DeclSecurity,
			// Token: 0x04000A31 RID: 2609
			Property,
			// Token: 0x04000A32 RID: 2610
			Event,
			// Token: 0x04000A33 RID: 2611
			StandAloneSig,
			// Token: 0x04000A34 RID: 2612
			ModuleRef,
			// Token: 0x04000A35 RID: 2613
			TypeSpec,
			// Token: 0x04000A36 RID: 2614
			Assembly,
			// Token: 0x04000A37 RID: 2615
			AssemblyRef,
			// Token: 0x04000A38 RID: 2616
			File,
			// Token: 0x04000A39 RID: 2617
			ExportedType,
			// Token: 0x04000A3A RID: 2618
			ManifestResource,
			// Token: 0x04000A3B RID: 2619
			GenericParam,
			// Token: 0x04000A3C RID: 2620
			GenericParamConstraint,
			// Token: 0x04000A3D RID: 2621
			MethodSpec,
			// Token: 0x04000A3E RID: 2622
			__bits = 5
		}

		// Token: 0x02000194 RID: 404
		private enum HasConstant
		{
			// Token: 0x04000A40 RID: 2624
			Field,
			// Token: 0x04000A41 RID: 2625
			Param,
			// Token: 0x04000A42 RID: 2626
			Property,
			// Token: 0x04000A43 RID: 2627
			__bits = 2,
			// Token: 0x04000A44 RID: 2628
			__mask
		}

		// Token: 0x02000195 RID: 405
		private enum CustomAttributeType
		{
			// Token: 0x04000A46 RID: 2630
			MethodDef = 2,
			// Token: 0x04000A47 RID: 2631
			MemberRef,
			// Token: 0x04000A48 RID: 2632
			__bits = 3
		}

		// Token: 0x02000196 RID: 406
		private enum HasDeclSecurity
		{
			// Token: 0x04000A4A RID: 2634
			TypeDef,
			// Token: 0x04000A4B RID: 2635
			MethodDef,
			// Token: 0x04000A4C RID: 2636
			Assembly,
			// Token: 0x04000A4D RID: 2637
			__bits = 2,
			// Token: 0x04000A4E RID: 2638
			__mask
		}

		// Token: 0x02000197 RID: 407
		private enum HasFieldMarshal
		{
			// Token: 0x04000A50 RID: 2640
			Field,
			// Token: 0x04000A51 RID: 2641
			Param,
			// Token: 0x04000A52 RID: 2642
			__bits = 1,
			// Token: 0x04000A53 RID: 2643
			__mask = 1
		}

		// Token: 0x02000198 RID: 408
		private enum HasSemantics
		{
			// Token: 0x04000A55 RID: 2645
			Event,
			// Token: 0x04000A56 RID: 2646
			Property,
			// Token: 0x04000A57 RID: 2647
			__bits = 1
		}

		// Token: 0x02000199 RID: 409
		private enum Implementation
		{
			// Token: 0x04000A59 RID: 2649
			File,
			// Token: 0x04000A5A RID: 2650
			AssemblyRef,
			// Token: 0x04000A5B RID: 2651
			ExportedType,
			// Token: 0x04000A5C RID: 2652
			__bits = 2
		}

		// Token: 0x0200019A RID: 410
		private enum MemberForwarded
		{
			// Token: 0x04000A5E RID: 2654
			Field,
			// Token: 0x04000A5F RID: 2655
			MethodDef,
			// Token: 0x04000A60 RID: 2656
			__bits = 1
		}

		// Token: 0x0200019B RID: 411
		private enum MemberRefParent
		{
			// Token: 0x04000A62 RID: 2658
			TypeDef,
			// Token: 0x04000A63 RID: 2659
			TypeRef,
			// Token: 0x04000A64 RID: 2660
			ModuleRef,
			// Token: 0x04000A65 RID: 2661
			MethodDef,
			// Token: 0x04000A66 RID: 2662
			TypeSpec,
			// Token: 0x04000A67 RID: 2663
			__bits = 3
		}

		// Token: 0x0200019C RID: 412
		private enum MethodDefOrRef
		{
			// Token: 0x04000A69 RID: 2665
			MethodDef,
			// Token: 0x04000A6A RID: 2666
			MemberRef,
			// Token: 0x04000A6B RID: 2667
			__bits = 1
		}

		// Token: 0x0200019D RID: 413
		private enum ResolutionScope
		{
			// Token: 0x04000A6D RID: 2669
			Module,
			// Token: 0x04000A6E RID: 2670
			ModuleRef,
			// Token: 0x04000A6F RID: 2671
			AssemblyRef,
			// Token: 0x04000A70 RID: 2672
			TypeRef,
			// Token: 0x04000A71 RID: 2673
			__bits = 2
		}

		// Token: 0x0200019E RID: 414
		private enum TypeDefOrRefOrSpec
		{
			// Token: 0x04000A73 RID: 2675
			TypeDef,
			// Token: 0x04000A74 RID: 2676
			TypeRef,
			// Token: 0x04000A75 RID: 2677
			TypeSpec,
			// Token: 0x04000A76 RID: 2678
			__bits = 2
		}

		// Token: 0x0200019F RID: 415
		private enum TypeOrMethodDef
		{
			// Token: 0x04000A78 RID: 2680
			TypeDef,
			// Token: 0x04000A79 RID: 2681
			MethodDef,
			// Token: 0x04000A7A RID: 2682
			__bits = 1
		}

		// Token: 0x020001A0 RID: 416
		private enum HasCustomDebugInformation
		{
			// Token: 0x04000A7C RID: 2684
			MethodDef,
			// Token: 0x04000A7D RID: 2685
			Field,
			// Token: 0x04000A7E RID: 2686
			TypeRef,
			// Token: 0x04000A7F RID: 2687
			TypeDef,
			// Token: 0x04000A80 RID: 2688
			Param,
			// Token: 0x04000A81 RID: 2689
			InterfaceImpl,
			// Token: 0x04000A82 RID: 2690
			MemberRef,
			// Token: 0x04000A83 RID: 2691
			Module,
			// Token: 0x04000A84 RID: 2692
			DeclSecurity,
			// Token: 0x04000A85 RID: 2693
			Property,
			// Token: 0x04000A86 RID: 2694
			Event,
			// Token: 0x04000A87 RID: 2695
			StandAloneSig,
			// Token: 0x04000A88 RID: 2696
			ModuleRef,
			// Token: 0x04000A89 RID: 2697
			TypeSpec,
			// Token: 0x04000A8A RID: 2698
			Assembly,
			// Token: 0x04000A8B RID: 2699
			AssemblyRef,
			// Token: 0x04000A8C RID: 2700
			File,
			// Token: 0x04000A8D RID: 2701
			ExportedType,
			// Token: 0x04000A8E RID: 2702
			ManifestResource,
			// Token: 0x04000A8F RID: 2703
			GenericParam,
			// Token: 0x04000A90 RID: 2704
			GenericParamConstraint,
			// Token: 0x04000A91 RID: 2705
			MethodSpec,
			// Token: 0x04000A92 RID: 2706
			Document,
			// Token: 0x04000A93 RID: 2707
			LocalScope,
			// Token: 0x04000A94 RID: 2708
			LocalVariable,
			// Token: 0x04000A95 RID: 2709
			LocalConstant,
			// Token: 0x04000A96 RID: 2710
			ImportScope,
			// Token: 0x04000A97 RID: 2711
			__bits = 5
		}
	}
}
