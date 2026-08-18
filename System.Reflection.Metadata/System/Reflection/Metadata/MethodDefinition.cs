using System;
using System.Reflection.Metadata.Decoding;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x02000081 RID: 129
	public struct MethodDefinition
	{
		// Token: 0x060005EC RID: 1516 RVA: 0x0000E71D File Offset: 0x0000C91D
		internal MethodDefinition(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000E72D File Offset: 0x0000C92D
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000E73B File Offset: 0x0000C93B
		private MethodDefTreatment Treatment
		{
			get
			{
				return (MethodDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000E747 File Offset: 0x0000C947
		private MethodDefinitionHandle Handle
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000E754 File Offset: 0x0000C954
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000E77B File Offset: 0x0000C97B
		public BlobHandle Signature
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetSignature(this.Handle);
				}
				return this.GetProjectedSignature();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		internal MethodSignature<TType> DecodeSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000E7DB File Offset: 0x0000C9DB
		public int RelativeVirtualAddress
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetRva(this.Handle);
				}
				return this.GetProjectedRelativeVirtualAddress();
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000E802 File Offset: 0x0000CA02
		public MethodAttributes Attributes
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetFlags(this.Handle);
				}
				return this.GetProjectedFlags();
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000E829 File Offset: 0x0000CA29
		public MethodImplAttributes ImplAttributes
		{
			get
			{
				if (this.Treatment == MethodDefTreatment.None)
				{
					return this._reader.MethodDefTable.GetImplFlags(this.Handle);
				}
				return this.GetProjectedImplFlags();
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000E850 File Offset: 0x0000CA50
		public TypeDefinitionHandle GetDeclaringType()
		{
			return this._reader.GetDeclaringType(this.Handle);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000E863 File Offset: 0x0000CA63
		public ParameterHandleCollection GetParameters()
		{
			return new ParameterHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000E876 File Offset: 0x0000CA76
		public GenericParameterHandleCollection GetGenericParameters()
		{
			return this._reader.GenericParamTable.FindGenericParametersForMethod(this.Handle);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000E890 File Offset: 0x0000CA90
		public MethodImport GetImport()
		{
			int num = this._reader.ImplMapTable.FindImplForMethod(this.Handle);
			if (num == 0)
			{
				return default(MethodImport);
			}
			return this._reader.ImplMapTable.GetImport(num);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000E8D2 File Offset: 0x0000CAD2
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000E8EA File Offset: 0x0000CAEA
		public DeclarativeSecurityAttributeHandleCollection GetDeclarativeSecurityAttributes()
		{
			return new DeclarativeSecurityAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000E902 File Offset: 0x0000CB02
		private StringHandle GetProjectedName()
		{
			if ((this.Treatment & MethodDefTreatment.KindMask) == MethodDefTreatment.DisposeMethod)
			{
				return StringHandle.FromVirtualIndex(StringHandle.VirtualIndex.Dispose);
			}
			return this._reader.MethodDefTable.GetName(this.Handle);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000E930 File Offset: 0x0000CB30
		private MethodAttributes GetProjectedFlags()
		{
			MethodAttributes methodAttributes = this._reader.MethodDefTable.GetFlags(this.Handle);
			MethodDefTreatment treatment = this.Treatment;
			if ((treatment & MethodDefTreatment.KindMask) == MethodDefTreatment.HiddenInterfaceImplementation)
			{
				methodAttributes = ((methodAttributes & ~MethodAttributes.MemberAccessMask) | MethodAttributes.Private);
			}
			if ((treatment & MethodDefTreatment.MarkAbstractFlag) != MethodDefTreatment.None)
			{
				methodAttributes |= MethodAttributes.Abstract;
			}
			if ((treatment & MethodDefTreatment.MarkPublicFlag) != MethodDefTreatment.None)
			{
				methodAttributes = ((methodAttributes & ~MethodAttributes.MemberAccessMask) | MethodAttributes.Public);
			}
			return methodAttributes | MethodAttributes.HideBySig;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000E98C File Offset: 0x0000CB8C
		private MethodImplAttributes GetProjectedImplFlags()
		{
			MethodImplAttributes methodImplAttributes = this._reader.MethodDefTable.GetImplFlags(this.Handle);
			switch (this.Treatment & MethodDefTreatment.KindMask)
			{
			case MethodDefTreatment.Other:
			case MethodDefTreatment.AttributeMethod:
			case MethodDefTreatment.InterfaceMethod:
			case MethodDefTreatment.HiddenInterfaceImplementation:
			case MethodDefTreatment.DisposeMethod:
				methodImplAttributes |= (MethodImplAttributes)4099;
				break;
			case MethodDefTreatment.DelegateMethod:
				methodImplAttributes |= MethodImplAttributes.CodeTypeMask;
				break;
			}
			return methodImplAttributes;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000E9EF File Offset: 0x0000CBEF
		private BlobHandle GetProjectedSignature()
		{
			return this._reader.MethodDefTable.GetSignature(this.Handle);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000206D File Offset: 0x0000026D
		private int GetProjectedRelativeVirtualAddress()
		{
			return 0;
		}

		// Token: 0x040003BC RID: 956
		private readonly MetadataReader _reader;

		// Token: 0x040003BD RID: 957
		private readonly uint _treatmentAndRowId;
	}
}
