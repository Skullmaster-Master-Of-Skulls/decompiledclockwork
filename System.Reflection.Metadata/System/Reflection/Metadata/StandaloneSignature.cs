using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Decoding;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AE RID: 174
	public struct StandaloneSignature
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x000100CC File Offset: 0x0000E2CC
		internal StandaloneSignature(MetadataReader reader, StandaloneSignatureHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x000100E2 File Offset: 0x0000E2E2
		private StandaloneSignatureHandle Handle
		{
			get
			{
				return StandaloneSignatureHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x000100EF File Offset: 0x0000E2EF
		public BlobHandle Signature
		{
			get
			{
				return this._reader.StandAloneSigTable.GetSignature(this._rowId);
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00010108 File Offset: 0x0000E308
		internal MethodSignature<TType> DecodeMethodSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00010140 File Offset: 0x0000E340
		internal ImmutableArray<TType> DecodeLocalSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeLocalSignature(ref blobReader);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00010177 File Offset: 0x0000E377
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00010190 File Offset: 0x0000E390
		public StandaloneSignatureKind GetKind()
		{
			SignatureKind kind = this._reader.GetBlobReader(this.Signature).ReadSignatureHeader().Kind;
			if (kind == SignatureKind.Method)
			{
				return StandaloneSignatureKind.Method;
			}
			if (kind != SignatureKind.LocalVariables)
			{
				throw new BadImageFormatException();
			}
			return StandaloneSignatureKind.LocalVariables;
		}

		// Token: 0x0400046C RID: 1132
		private readonly MetadataReader _reader;

		// Token: 0x0400046D RID: 1133
		private readonly int _rowId;
	}
}
