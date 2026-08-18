using System;
using System.Reflection.Metadata.Decoding;

namespace System.Reflection.Metadata
{
	// Token: 0x020000B2 RID: 178
	public struct TypeSpecification
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0001073D File Offset: 0x0000E93D
		internal TypeSpecification(MetadataReader reader, TypeSpecificationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00010753 File Offset: 0x0000E953
		private TypeSpecificationHandle Handle
		{
			get
			{
				return TypeSpecificationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00010760 File Offset: 0x0000E960
		public BlobHandle Signature
		{
			get
			{
				return this._reader.TypeSpecTable.GetSignature(this.Handle);
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00010778 File Offset: 0x0000E978
		internal TType DecodeSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeType(ref blobReader, false);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000107B0 File Offset: 0x0000E9B0
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000474 RID: 1140
		private readonly MetadataReader _reader;

		// Token: 0x04000475 RID: 1141
		private readonly int _rowId;
	}
}
