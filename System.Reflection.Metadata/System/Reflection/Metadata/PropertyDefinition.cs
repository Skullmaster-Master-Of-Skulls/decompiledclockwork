using System;
using System.Reflection.Metadata.Decoding;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A7 RID: 167
	public struct PropertyDefinition
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0000FDF9 File Offset: 0x0000DFF9
		internal PropertyDefinition(MetadataReader reader, PropertyDefinitionHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0000FE0F File Offset: 0x0000E00F
		private PropertyDefinitionHandle Handle
		{
			get
			{
				return PropertyDefinitionHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0000FE1C File Offset: 0x0000E01C
		public StringHandle Name
		{
			get
			{
				return this._reader.PropertyTable.GetName(this.Handle);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0000FE34 File Offset: 0x0000E034
		public PropertyAttributes Attributes
		{
			get
			{
				return this._reader.PropertyTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0000FE4C File Offset: 0x0000E04C
		public BlobHandle Signature
		{
			get
			{
				return this._reader.PropertyTable.GetSignature(this.Handle);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000FE64 File Offset: 0x0000E064
		internal MethodSignature<TType> DecodeSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000FE9B File Offset: 0x0000E09B
		public ConstantHandle GetDefaultValue()
		{
			return this._reader.ConstantTable.FindConstant(this.Handle);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		public PropertyAccessors GetAccessors()
		{
			int getterRowId = 0;
			int setterRowId = 0;
			ushort num2;
			int num = this._reader.MethodSemanticsTable.FindSemanticMethodsForProperty(this.Handle, out num2);
			for (ushort num3 = 0; num3 < num2; num3 += 1)
			{
				int rowId = num + (int)num3;
				MethodSemanticsAttributes semantics = this._reader.MethodSemanticsTable.GetSemantics(rowId);
				if (semantics != MethodSemanticsAttributes.Setter)
				{
					if (semantics == MethodSemanticsAttributes.Getter)
					{
						getterRowId = this._reader.MethodSemanticsTable.GetMethod(rowId).RowId;
					}
				}
				else
				{
					setterRowId = this._reader.MethodSemanticsTable.GetMethod(rowId).RowId;
				}
			}
			return new PropertyAccessors(getterRowId, setterRowId);
		}

		// Token: 0x04000421 RID: 1057
		private readonly MetadataReader _reader;

		// Token: 0x04000422 RID: 1058
		private readonly int _rowId;
	}
}
