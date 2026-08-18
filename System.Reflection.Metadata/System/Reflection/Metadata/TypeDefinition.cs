using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AF RID: 175
	public struct TypeDefinition
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x000101D1 File Offset: 0x0000E3D1
		internal TypeDefinition(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000101E1 File Offset: 0x0000E3E1
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x000101EF File Offset: 0x0000E3EF
		private TypeDefTreatment Treatment
		{
			get
			{
				return (TypeDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x000101FB File Offset: 0x0000E3FB
		private TypeDefinitionHandle Handle
		{
			get
			{
				return TypeDefinitionHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00010208 File Offset: 0x0000E408
		public TypeAttributes Attributes
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetFlags(this.Handle);
				}
				return this.GetProjectedFlags();
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001022F File Offset: 0x0000E42F
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x00010256 File Offset: 0x0000E456
		public StringHandle Namespace
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetNamespace(this.Handle);
				}
				return this.GetProjectedNamespaceString();
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001027D File Offset: 0x0000E47D
		public NamespaceDefinitionHandle NamespaceDefinition
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetNamespaceDefinition(this.Handle);
				}
				return this.GetProjectedNamespace();
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x000102A4 File Offset: 0x0000E4A4
		public EntityHandle BaseType
		{
			get
			{
				if (this.Treatment == TypeDefTreatment.None)
				{
					return this._reader.TypeDefTable.GetExtends(this.Handle);
				}
				return this.GetProjectedBaseType();
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000102CC File Offset: 0x0000E4CC
		public TypeLayout GetLayout()
		{
			int num = this._reader.ClassLayoutTable.FindRow(this.Handle);
			if (num == 0)
			{
				return default(TypeLayout);
			}
			uint classSize = this._reader.ClassLayoutTable.GetClassSize(num);
			if ((long)classSize != (long)((ulong)classSize))
			{
				throw new BadImageFormatException(SR.InvalidTypeSize);
			}
			int packingSize = (int)this._reader.ClassLayoutTable.GetPackingSize(num);
			return new TypeLayout((int)classSize, packingSize);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00010339 File Offset: 0x0000E539
		public TypeDefinitionHandle GetDeclaringType()
		{
			return this._reader.NestedClassTable.FindEnclosingType(this.Handle);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00010351 File Offset: 0x0000E551
		public GenericParameterHandleCollection GetGenericParameters()
		{
			return this._reader.GenericParamTable.FindGenericParametersForType(this.Handle);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00010369 File Offset: 0x0000E569
		public MethodDefinitionHandleCollection GetMethods()
		{
			return new MethodDefinitionHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001037C File Offset: 0x0000E57C
		public FieldDefinitionHandleCollection GetFields()
		{
			return new FieldDefinitionHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001038F File Offset: 0x0000E58F
		public PropertyDefinitionHandleCollection GetProperties()
		{
			return new PropertyDefinitionHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x000103A2 File Offset: 0x0000E5A2
		public EventDefinitionHandleCollection GetEvents()
		{
			return new EventDefinitionHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x000103B5 File Offset: 0x0000E5B5
		public ImmutableArray<TypeDefinitionHandle> GetNestedTypes()
		{
			return this._reader.GetNestedTypes(this.Handle);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000103C8 File Offset: 0x0000E5C8
		public MethodImplementationHandleCollection GetMethodImplementations()
		{
			return new MethodImplementationHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000103DB File Offset: 0x0000E5DB
		public InterfaceImplementationHandleCollection GetInterfaceImplementations()
		{
			return new InterfaceImplementationHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x000103EE File Offset: 0x0000E5EE
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00010406 File Offset: 0x0000E606
		public DeclarativeSecurityAttributeHandleCollection GetDeclarativeSecurityAttributes()
		{
			return new DeclarativeSecurityAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00010420 File Offset: 0x0000E620
		private TypeAttributes GetProjectedFlags()
		{
			TypeAttributes typeAttributes = this._reader.TypeDefTable.GetFlags(this.Handle);
			TypeDefTreatment treatment = this.Treatment;
			switch (treatment & TypeDefTreatment.KindMask)
			{
			case TypeDefTreatment.NormalNonAttribute:
				typeAttributes |= (TypeAttributes.Import | TypeAttributes.WindowsRuntime);
				break;
			case TypeDefTreatment.NormalAttribute:
				typeAttributes |= (TypeAttributes.Sealed | TypeAttributes.WindowsRuntime);
				break;
			case TypeDefTreatment.UnmangleWinRTName:
				typeAttributes = ((typeAttributes & ~TypeAttributes.SpecialName) | TypeAttributes.Public);
				break;
			case TypeDefTreatment.PrefixWinRTName:
				typeAttributes = ((typeAttributes & ~TypeAttributes.Public) | TypeAttributes.Import);
				break;
			case TypeDefTreatment.RedirectedToClrType:
				typeAttributes = ((typeAttributes & ~TypeAttributes.Public) | TypeAttributes.Import);
				break;
			case TypeDefTreatment.RedirectedToClrAttribute:
				typeAttributes &= ~TypeAttributes.Public;
				break;
			}
			if ((treatment & TypeDefTreatment.MarkAbstractFlag) != TypeDefTreatment.None)
			{
				typeAttributes |= TypeAttributes.Abstract;
			}
			if ((treatment & TypeDefTreatment.MarkInternalFlag) != TypeDefTreatment.None)
			{
				typeAttributes &= ~TypeAttributes.Public;
			}
			return typeAttributes;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000104CC File Offset: 0x0000E6CC
		private StringHandle GetProjectedName()
		{
			StringHandle name = this._reader.TypeDefTable.GetName(this.Handle);
			TypeDefTreatment typeDefTreatment = this.Treatment & TypeDefTreatment.KindMask;
			if (typeDefTreatment == TypeDefTreatment.UnmangleWinRTName)
			{
				return name.SuffixRaw("<CLR>".Length);
			}
			if (typeDefTreatment != TypeDefTreatment.PrefixWinRTName)
			{
				return name;
			}
			return name.WithWinRTPrefix();
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001051F File Offset: 0x0000E71F
		private NamespaceDefinitionHandle GetProjectedNamespace()
		{
			return this._reader.TypeDefTable.GetNamespaceDefinition(this.Handle);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00010537 File Offset: 0x0000E737
		private StringHandle GetProjectedNamespaceString()
		{
			return this._reader.TypeDefTable.GetNamespace(this.Handle);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001054F File Offset: 0x0000E74F
		private EntityHandle GetProjectedBaseType()
		{
			return this._reader.TypeDefTable.GetExtends(this.Handle);
		}

		// Token: 0x0400046E RID: 1134
		private readonly MetadataReader _reader;

		// Token: 0x0400046F RID: 1135
		private readonly uint _treatmentAndRowId;
	}
}
