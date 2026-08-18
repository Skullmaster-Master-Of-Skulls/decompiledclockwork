using System;
using System.Reflection.Metadata.Decoding;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x02000078 RID: 120
	public struct MemberReference
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		internal MemberReference(MetadataReader reader, uint treatmentAndRowId)
		{
			this._reader = reader;
			this._treatmentAndRowId = treatmentAndRowId;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		private int RowId
		{
			get
			{
				return (int)(this._treatmentAndRowId & 16777215U);
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000AD07 File Offset: 0x00008F07
		private MemberRefTreatment Treatment
		{
			get
			{
				return (MemberRefTreatment)(this._treatmentAndRowId >> 24);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000AD13 File Offset: 0x00008F13
		private MemberReferenceHandle Handle
		{
			get
			{
				return MemberReferenceHandle.FromRowId(this.RowId);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000AD20 File Offset: 0x00008F20
		public EntityHandle Parent
		{
			get
			{
				if (this.Treatment == MemberRefTreatment.None)
				{
					return this._reader.MemberRefTable.GetClass(this.Handle);
				}
				return this.GetProjectedParent();
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000AD47 File Offset: 0x00008F47
		public StringHandle Name
		{
			get
			{
				if (this.Treatment == MemberRefTreatment.None)
				{
					return this._reader.MemberRefTable.GetName(this.Handle);
				}
				return this.GetProjectedName();
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000AD6E File Offset: 0x00008F6E
		public BlobHandle Signature
		{
			get
			{
				if (this.Treatment == MemberRefTreatment.None)
				{
					return this._reader.MemberRefTable.GetSignature(this.Handle);
				}
				return this.GetProjectedSignature();
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000AD98 File Offset: 0x00008F98
		internal TType DecodeFieldSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeFieldSignature(ref blobReader);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000ADD0 File Offset: 0x00008FD0
		internal MethodSignature<TType> DecodeMethodSignature<TType>(ISignatureTypeProvider<TType> provider, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			SignatureDecoder<TType> signatureDecoder = new SignatureDecoder<TType>(provider, this._reader, options);
			BlobReader blobReader = this._reader.GetBlobReader(this.Signature);
			return signatureDecoder.DecodeMethodSignature(ref blobReader);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000AE08 File Offset: 0x00009008
		public MemberReferenceKind GetKind()
		{
			SignatureKind kind = this._reader.GetBlobReader(this.Signature).ReadSignatureHeader().Kind;
			if (kind == SignatureKind.Method)
			{
				return MemberReferenceKind.Method;
			}
			if (kind != SignatureKind.Field)
			{
				throw new BadImageFormatException();
			}
			return MemberReferenceKind.Field;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000AE49 File Offset: 0x00009049
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000AE61 File Offset: 0x00009061
		private EntityHandle GetProjectedParent()
		{
			return this._reader.MemberRefTable.GetClass(this.Handle);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000AE79 File Offset: 0x00009079
		private StringHandle GetProjectedName()
		{
			if (this.Treatment == MemberRefTreatment.Dispose)
			{
				return StringHandle.FromVirtualIndex(StringHandle.VirtualIndex.Dispose);
			}
			return this._reader.MemberRefTable.GetName(this.Handle);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000AEA1 File Offset: 0x000090A1
		private BlobHandle GetProjectedSignature()
		{
			return this._reader.MemberRefTable.GetSignature(this.Handle);
		}

		// Token: 0x0400034A RID: 842
		private readonly MetadataReader _reader;

		// Token: 0x0400034B RID: 843
		private readonly uint _treatmentAndRowId;
	}
}
