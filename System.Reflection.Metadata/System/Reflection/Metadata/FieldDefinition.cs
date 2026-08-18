using System;
using System.Reflection.Metadata.Decoding;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003C RID: 60
	public struct FieldDefinition
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00008396 File Offset: 0x00006596
		internal FieldDefinition(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000083A6 File Offset: 0x000065A6
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000083B4 File Offset: 0x000065B4
		private FieldDefTreatment Treatment
		{
			get
			{
				return (FieldDefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000083C0 File Offset: 0x000065C0
		private FieldDefinitionHandle Handle
		{
			get
			{
				return FieldDefinitionHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000083CD File Offset: 0x000065CD
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == FieldDefTreatment.None)
				{
					return this._reader.FieldTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000083F4 File Offset: 0x000065F4
		public FieldAttributes Attributes
		{
			get
			{
				if (this.Treatment == FieldDefTreatment.None)
				{
					return this._reader.FieldTable.GetFlags(this.Handle);
				}
				return this.GetProjectedFlags();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000841B File Offset: 0x0000661B
		public BlobHandle Signature
		{
			get
			{
				if (this.Treatment == FieldDefTreatment.None)
				{
					return this._reader.FieldTable.GetSignature(this.Handle);
				}
				return this.GetProjectedSignature();
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008444 File Offset: 0x00006644
		internal TType DecodeSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeFieldSignature(ref blobReader);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000847B File Offset: 0x0000667B
		public TypeDefinitionHandle GetDeclaringType()
		{
			return this._reader.GetDeclaringType(this.Handle);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000848E File Offset: 0x0000668E
		public ConstantHandle GetDefaultValue()
		{
			return this._reader.ConstantTable.FindConstant(this.Handle);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000084AC File Offset: 0x000066AC
		public int GetRelativeVirtualAddress()
		{
			int num = this._reader.FieldRvaTable.FindFieldRvaRowId(this.Handle.RowId);
			if (num == 0)
			{
				return 0;
			}
			return this._reader.FieldRvaTable.GetRva(num);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000084F0 File Offset: 0x000066F0
		public int GetOffset()
		{
			int num = this._reader.FieldLayoutTable.FindFieldLayoutRowId(this.Handle);
			if (num == 0)
			{
				return -1;
			}
			uint offset = this._reader.FieldLayoutTable.GetOffset(num);
			if (offset > 2147483647U)
			{
				return -1;
			}
			return (int)offset;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00008538 File Offset: 0x00006738
		public BlobHandle GetMarshallingDescriptor()
		{
			int num = this._reader.FieldMarshalTable.FindFieldMarshalRowId(this.Handle);
			if (num == 0)
			{
				return default(BlobHandle);
			}
			return this._reader.FieldMarshalTable.GetNativeType(num);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000857F File Offset: 0x0000677F
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008597 File Offset: 0x00006797
		private StringHandle GetProjectedName()
		{
			return this._reader.FieldTable.GetName(this.Handle);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000085B0 File Offset: 0x000067B0
		private FieldAttributes GetProjectedFlags()
		{
			FieldAttributes flags = this._reader.FieldTable.GetFlags(this.Handle);
			if (this.Treatment == FieldDefTreatment.EnumValue)
			{
				return (flags & ~FieldAttributes.FieldAccessMask) | FieldAttributes.Public;
			}
			return flags;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000085E5 File Offset: 0x000067E5
		private BlobHandle GetProjectedSignature()
		{
			return this._reader.FieldTable.GetSignature(this.Handle);
		}

		// Token: 0x04000297 RID: 663
		private readonly MetadataReader _reader;

		// Token: 0x04000298 RID: 664
		private readonly uint _treatmentAndRowId;
	}
}
