using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x0200014B RID: 331
	internal struct SignatureDecoder<TType>
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0001DFD3 File Offset: 0x0001C1D3
		public SignatureDecoder(ISignatureTypeProvider<TType> provider, MetadataReader metadataReader = null, SignatureDecoderOptions options = SignatureDecoderOptions.None)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._metadataReaderOpt = metadataReader;
			this._provider = provider;
			this._options = options;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		public TType DecodeType(ref BlobReader blobReader, bool allowTypeSpecifications = false)
		{
			return this.DecodeType(ref blobReader, allowTypeSpecifications, blobReader.ReadCompressedInteger());
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0001E008 File Offset: 0x0001C208
		private TType DecodeType(ref BlobReader blobReader, bool allowTypeSpecifications, int typeCode)
		{
			switch (typeCode)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 22:
			case 24:
			case 25:
			case 28:
				return this._provider.GetPrimitiveType((PrimitiveTypeCode)typeCode);
			case 15:
			{
				TType elementType = this.DecodeType(ref blobReader, false);
				return this._provider.GetPointerType(elementType);
			}
			case 16:
			{
				TType elementType = this.DecodeType(ref blobReader, false);
				return this._provider.GetByReferenceType(elementType);
			}
			case 17:
			case 18:
				return this.DecodeTypeHandle(ref blobReader, (SignatureTypeHandleCode)typeCode, allowTypeSpecifications);
			case 19:
			{
				int index = blobReader.ReadCompressedInteger();
				return this._provider.GetGenericTypeParameter(index);
			}
			case 20:
				return this.DecodeArrayType(ref blobReader);
			case 21:
				return this.DecodeGenericTypeInstance(ref blobReader);
			case 23:
			case 26:
				break;
			case 27:
			{
				MethodSignature<TType> signature = this.DecodeMethodSignature(ref blobReader);
				return this._provider.GetFunctionPointerType(signature);
			}
			case 29:
			{
				TType elementType = this.DecodeType(ref blobReader, false);
				return this._provider.GetSZArrayType(elementType);
			}
			case 30:
			{
				int index = blobReader.ReadCompressedInteger();
				return this._provider.GetGenericMethodParameter(index);
			}
			case 31:
				return this.DecodeModifiedType(ref blobReader, true);
			case 32:
				return this.DecodeModifiedType(ref blobReader, false);
			default:
				if (typeCode == 69)
				{
					TType elementType = this.DecodeType(ref blobReader, false);
					return this._provider.GetPinnedType(elementType);
				}
				break;
			}
			throw new BadImageFormatException(SR.Format(SR.UnexpectedSignatureTypeCode, typeCode));
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0001E18C File Offset: 0x0001C38C
		private ImmutableArray<TType> DecodeTypeSequence(ref BlobReader blobReader)
		{
			int num = blobReader.ReadCompressedInteger();
			if (num == 0)
			{
				throw new BadImageFormatException(SR.SignatureTypeSequenceMustHaveAtLeastOneElement);
			}
			ImmutableArray<TType>.Builder builder = ImmutableArray.CreateBuilder<TType>(num);
			for (int i = 0; i < num; i++)
			{
				builder.Add(this.DecodeType(ref blobReader, false));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		public MethodSignature<TType> DecodeMethodSignature(ref BlobReader blobReader)
		{
			SignatureHeader header = blobReader.ReadSignatureHeader();
			this.CheckMethodOrPropertyHeader(header);
			int genericParameterCount = 0;
			if (header.IsGeneric)
			{
				genericParameterCount = blobReader.ReadCompressedInteger();
			}
			int num = blobReader.ReadCompressedInteger();
			TType returnType = this.DecodeType(ref blobReader, false);
			int requiredParameterCount;
			ImmutableArray<TType> parameterTypes;
			if (num == 0)
			{
				requiredParameterCount = 0;
				parameterTypes = ImmutableArray<TType>.Empty;
			}
			else
			{
				ImmutableArray<TType>.Builder builder = ImmutableArray.CreateBuilder<TType>(num);
				int i;
				for (i = 0; i < num; i++)
				{
					int num2 = blobReader.ReadCompressedInteger();
					if (num2 == 65)
					{
						break;
					}
					builder.Add(this.DecodeType(ref blobReader, false, num2));
				}
				requiredParameterCount = i;
				while (i < num)
				{
					builder.Add(this.DecodeType(ref blobReader, false));
					i++;
				}
				parameterTypes = builder.MoveToImmutable();
			}
			return new MethodSignature<TType>(header, returnType, requiredParameterCount, genericParameterCount, parameterTypes);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0001E290 File Offset: 0x0001C490
		public ImmutableArray<TType> DecodeMethodSpecificationSignature(ref BlobReader blobReader)
		{
			SignatureHeader header = blobReader.ReadSignatureHeader();
			this.CheckHeader(header, SignatureKind.MethodSpecification);
			return this.DecodeTypeSequence(ref blobReader);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
		public ImmutableArray<TType> DecodeLocalSignature(ref BlobReader blobReader)
		{
			SignatureHeader header = blobReader.ReadSignatureHeader();
			this.CheckHeader(header, SignatureKind.LocalVariables);
			return this.DecodeTypeSequence(ref blobReader);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0001E2D8 File Offset: 0x0001C4D8
		public TType DecodeFieldSignature(ref BlobReader blobReader)
		{
			SignatureHeader header = blobReader.ReadSignatureHeader();
			this.CheckHeader(header, SignatureKind.Field);
			return this.DecodeType(ref blobReader, false);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		private TType DecodeArrayType(ref BlobReader blobReader)
		{
			TType elementType = this.DecodeType(ref blobReader, false);
			int rank = blobReader.ReadCompressedInteger();
			ImmutableArray<int> sizes = ImmutableArray<int>.Empty;
			ImmutableArray<int> lowerBounds = ImmutableArray<int>.Empty;
			int num = blobReader.ReadCompressedInteger();
			if (num > 0)
			{
				ImmutableArray<int>.Builder builder = ImmutableArray.CreateBuilder<int>(num);
				for (int i = 0; i < num; i++)
				{
					builder.Add(blobReader.ReadCompressedInteger());
				}
				sizes = builder.MoveToImmutable();
			}
			int num2 = blobReader.ReadCompressedInteger();
			if (num2 > 0)
			{
				ImmutableArray<int>.Builder builder2 = ImmutableArray.CreateBuilder<int>(num2);
				for (int j = 0; j < num2; j++)
				{
					builder2.Add(blobReader.ReadCompressedSignedInteger());
				}
				lowerBounds = builder2.MoveToImmutable();
			}
			ArrayShape shape = new ArrayShape(rank, sizes, lowerBounds);
			return this._provider.GetArrayType(elementType, shape);
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		private TType DecodeGenericTypeInstance(ref BlobReader blobReader)
		{
			TType genericType = this.DecodeType(ref blobReader, false);
			ImmutableArray<TType> typeArguments = this.DecodeTypeSequence(ref blobReader);
			return this._provider.GetGenericInstance(genericType, typeArguments);
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		private TType DecodeModifiedType(ref BlobReader blobReader, bool isRequired)
		{
			TType modifier = this.DecodeTypeHandle(ref blobReader, SignatureTypeHandleCode.Unresolved, true);
			TType unmodifiedType = this.DecodeType(ref blobReader, false);
			return this._provider.GetModifiedType(this._metadataReaderOpt, isRequired, modifier, unmodifiedType);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001E418 File Offset: 0x0001C618
		private TType DecodeTypeHandle(ref BlobReader blobReader, SignatureTypeHandleCode code, bool allowTypeSpecifications)
		{
			if ((this._options & SignatureDecoderOptions.DifferentiateClassAndValueTypes) == SignatureDecoderOptions.None)
			{
				code = SignatureTypeHandleCode.Unresolved;
			}
			EntityHandle handle = blobReader.ReadTypeHandle();
			if (!handle.IsNil)
			{
				HandleKind kind = handle.Kind;
				if (kind == HandleKind.TypeReference)
				{
					TypeReferenceHandle handle2 = (TypeReferenceHandle)handle;
					if (code != SignatureTypeHandleCode.Unresolved)
					{
						this.ProjectClassOrValueType(handle2, ref code);
					}
					return this._provider.GetTypeFromReference(this._metadataReaderOpt, handle2, code);
				}
				if (kind == HandleKind.TypeDefinition)
				{
					TypeDefinitionHandle handle3 = (TypeDefinitionHandle)handle;
					return this._provider.GetTypeFromDefinition(this._metadataReaderOpt, handle3, code);
				}
				if (kind == HandleKind.TypeSpecification)
				{
					if (!allowTypeSpecifications)
					{
						throw new BadImageFormatException(SR.NotTypeDefOrRefHandle);
					}
					if (code != SignatureTypeHandleCode.Unresolved)
					{
						code = SignatureTypeHandleCode.Unresolved;
					}
					TypeSpecificationHandle handle4 = (TypeSpecificationHandle)handle;
					return this._provider.GetTypeFromSpecification(this._metadataReaderOpt, handle4, SignatureTypeHandleCode.Unresolved);
				}
			}
			throw new BadImageFormatException(SR.NotTypeDefOrRefOrSpecHandle);
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		private void ProjectClassOrValueType(TypeReferenceHandle handle, ref SignatureTypeHandleCode code)
		{
			if (this._metadataReaderOpt == null)
			{
				return;
			}
			TypeRefSignatureTreatment signatureTreatment = this._metadataReaderOpt.GetTypeReference(handle).SignatureTreatment;
			if (signatureTreatment == TypeRefSignatureTreatment.ProjectedToClass)
			{
				code = SignatureTypeHandleCode.Class;
				return;
			}
			if (signatureTreatment != TypeRefSignatureTreatment.ProjectedToValueType)
			{
				return;
			}
			code = SignatureTypeHandleCode.ValueType;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001E519 File Offset: 0x0001C719
		private void CheckHeader(SignatureHeader header, SignatureKind expectedKind)
		{
			if (header.Kind != expectedKind)
			{
				throw new BadImageFormatException(SR.Format(SR.UnexpectedSignatureHeader, expectedKind, header.Kind, header.RawValue));
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001E554 File Offset: 0x0001C754
		private void CheckMethodOrPropertyHeader(SignatureHeader header)
		{
			SignatureKind kind = header.Kind;
			if (kind != SignatureKind.Method && kind != SignatureKind.Property)
			{
				throw new BadImageFormatException(SR.Format(SR.UnexpectedSignatureHeader2, new object[]
				{
					SignatureKind.Property,
					SignatureKind.Method,
					header.Kind,
					header.RawValue
				}));
			}
		}

		// Token: 0x040008CE RID: 2254
		private readonly ISignatureTypeProvider<TType> _provider;

		// Token: 0x040008CF RID: 2255
		private readonly MetadataReader _metadataReaderOpt;

		// Token: 0x040008D0 RID: 2256
		private readonly SignatureDecoderOptions _options;
	}
}
