using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x0200013F RID: 319
	internal struct CustomAttributeDecoder<TType>
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x0001D824 File Offset: 0x0001BA24
		public CustomAttributeDecoder(ICustomAttributeTypeProvider<TType> provider, MetadataReader reader)
		{
			this._reader = reader;
			this._provider = provider;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001D834 File Offset: 0x0001BA34
		public CustomAttributeValue<TType> DecodeValue(EntityHandle constructor, BlobHandle value)
		{
			HandleKind kind = constructor.Kind;
			BlobHandle signature;
			if (kind != HandleKind.MethodDefinition)
			{
				if (kind != HandleKind.MemberReference)
				{
					throw new BadImageFormatException();
				}
				signature = this._reader.GetMemberReference((MemberReferenceHandle)constructor).Signature;
			}
			else
			{
				signature = this._reader.GetMethodDefinition((MethodDefinitionHandle)constructor).Signature;
			}
			BlobReader blobReader = this._reader.GetBlobReader(signature);
			BlobReader blobReader2 = this._reader.GetBlobReader(value);
			if (blobReader2.ReadUInt16() != 1)
			{
				throw new BadImageFormatException();
			}
			SignatureHeader signatureHeader = blobReader.ReadSignatureHeader();
			if (signatureHeader.Kind != SignatureKind.Method || signatureHeader.IsGeneric)
			{
				throw new BadImageFormatException();
			}
			int count = blobReader.ReadCompressedInteger();
			if (blobReader.ReadSignatureTypeCode() != SignatureTypeCode.Void)
			{
				throw new BadImageFormatException();
			}
			ImmutableArray<CustomAttributeTypedArgument<TType>> fixedArguments = this.DecodeFixedArguments(ref blobReader, ref blobReader2, count);
			ImmutableArray<CustomAttributeNamedArgument<TType>> namedArguments = this.DecodeNamedArguments(ref blobReader2);
			return new CustomAttributeValue<TType>(fixedArguments, namedArguments);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001D918 File Offset: 0x0001BB18
		private ImmutableArray<CustomAttributeTypedArgument<TType>> DecodeFixedArguments(ref BlobReader signatureReader, ref BlobReader valueReader, int count)
		{
			if (count == 0)
			{
				return ImmutableArray<CustomAttributeTypedArgument<TType>>.Empty;
			}
			ImmutableArray<CustomAttributeTypedArgument<TType>>.Builder builder = ImmutableArray.CreateBuilder<CustomAttributeTypedArgument<TType>>(count);
			for (int i = 0; i < count; i++)
			{
				CustomAttributeDecoder<TType>.ArgumentTypeInfo info = this.DecodeFixedArgumentType(ref signatureReader, false);
				builder.Add(this.DecodeArgument(ref valueReader, info));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001D960 File Offset: 0x0001BB60
		private ImmutableArray<CustomAttributeNamedArgument<TType>> DecodeNamedArguments(ref BlobReader valueReader)
		{
			int num = (int)valueReader.ReadUInt16();
			if (num == 0)
			{
				return ImmutableArray<CustomAttributeNamedArgument<TType>>.Empty;
			}
			ImmutableArray<CustomAttributeNamedArgument<TType>>.Builder builder = ImmutableArray.CreateBuilder<CustomAttributeNamedArgument<TType>>(num);
			for (int i = 0; i < num; i++)
			{
				CustomAttributeNamedArgumentKind customAttributeNamedArgumentKind = (CustomAttributeNamedArgumentKind)valueReader.ReadSerializationTypeCode();
				if (customAttributeNamedArgumentKind != CustomAttributeNamedArgumentKind.Field && customAttributeNamedArgumentKind != CustomAttributeNamedArgumentKind.Property)
				{
					throw new BadImageFormatException();
				}
				CustomAttributeDecoder<TType>.ArgumentTypeInfo info = this.DecodeNamedArgumentType(ref valueReader, false);
				string name = valueReader.ReadSerializedString();
				CustomAttributeTypedArgument<TType> customAttributeTypedArgument = this.DecodeArgument(ref valueReader, info);
				builder.Add(new CustomAttributeNamedArgument<TType>(name, customAttributeNamedArgumentKind, customAttributeTypedArgument.Type, customAttributeTypedArgument.Value));
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
		private CustomAttributeDecoder<TType>.ArgumentTypeInfo DecodeFixedArgumentType(ref BlobReader signatureReader, bool isElementType = false)
		{
			SignatureTypeCode signatureTypeCode = signatureReader.ReadSignatureTypeCode();
			CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo = new CustomAttributeDecoder<TType>.ArgumentTypeInfo
			{
				TypeCode = (SerializationTypeCode)signatureTypeCode
			};
			switch (signatureTypeCode)
			{
			case SignatureTypeCode.Boolean:
			case SignatureTypeCode.Char:
			case SignatureTypeCode.SByte:
			case SignatureTypeCode.Byte:
			case SignatureTypeCode.Int16:
			case SignatureTypeCode.UInt16:
			case SignatureTypeCode.Int32:
			case SignatureTypeCode.UInt32:
			case SignatureTypeCode.Int64:
			case SignatureTypeCode.UInt64:
			case SignatureTypeCode.Single:
			case SignatureTypeCode.Double:
			case SignatureTypeCode.String:
				argumentTypeInfo.Type = this._provider.GetPrimitiveType((PrimitiveTypeCode)signatureTypeCode);
				return argumentTypeInfo;
			case SignatureTypeCode.Pointer:
			case SignatureTypeCode.ByReference:
			case (SignatureTypeCode)17:
			case (SignatureTypeCode)18:
			case SignatureTypeCode.GenericTypeParameter:
			case SignatureTypeCode.Array:
			case SignatureTypeCode.GenericTypeInstance:
			case SignatureTypeCode.TypedReference:
			case (SignatureTypeCode)23:
			case SignatureTypeCode.IntPtr:
			case SignatureTypeCode.UIntPtr:
			case (SignatureTypeCode)26:
			case SignatureTypeCode.FunctionPointer:
				break;
			case SignatureTypeCode.Object:
				argumentTypeInfo.TypeCode = SerializationTypeCode.TaggedObject;
				argumentTypeInfo.Type = this._provider.GetPrimitiveType(PrimitiveTypeCode.Object);
				return argumentTypeInfo;
			case SignatureTypeCode.SZArray:
			{
				if (isElementType)
				{
					throw new BadImageFormatException();
				}
				CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo2 = this.DecodeFixedArgumentType(ref signatureReader, true);
				argumentTypeInfo.ElementType = argumentTypeInfo2.Type;
				argumentTypeInfo.ElementTypeCode = argumentTypeInfo2.TypeCode;
				argumentTypeInfo.Type = this._provider.GetSZArrayType(argumentTypeInfo.ElementType);
				return argumentTypeInfo;
			}
			default:
				if (signatureTypeCode == SignatureTypeCode.TypeHandle)
				{
					EntityHandle handle = signatureReader.ReadTypeHandle();
					argumentTypeInfo.Type = this.GetTypeFromHandle(handle);
					argumentTypeInfo.TypeCode = (SerializationTypeCode)(this._provider.IsSystemType(argumentTypeInfo.Type) ? ((PrimitiveTypeCode)80) : this._provider.GetUnderlyingEnumType(argumentTypeInfo.Type));
					return argumentTypeInfo;
				}
				break;
			}
			throw new BadImageFormatException();
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001DB60 File Offset: 0x0001BD60
		private CustomAttributeDecoder<TType>.ArgumentTypeInfo DecodeNamedArgumentType(ref BlobReader valueReader, bool isElementType = false)
		{
			CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo = new CustomAttributeDecoder<TType>.ArgumentTypeInfo
			{
				TypeCode = valueReader.ReadSerializationTypeCode()
			};
			SerializationTypeCode typeCode = argumentTypeInfo.TypeCode;
			if (typeCode <= SerializationTypeCode.SZArray)
			{
				switch (typeCode)
				{
				case SerializationTypeCode.Boolean:
				case SerializationTypeCode.Char:
				case SerializationTypeCode.SByte:
				case SerializationTypeCode.Byte:
				case SerializationTypeCode.Int16:
				case SerializationTypeCode.UInt16:
				case SerializationTypeCode.Int32:
				case SerializationTypeCode.UInt32:
				case SerializationTypeCode.Int64:
				case SerializationTypeCode.UInt64:
				case SerializationTypeCode.Single:
				case SerializationTypeCode.Double:
				case SerializationTypeCode.String:
					argumentTypeInfo.Type = this._provider.GetPrimitiveType((PrimitiveTypeCode)argumentTypeInfo.TypeCode);
					return argumentTypeInfo;
				default:
					if (typeCode == SerializationTypeCode.SZArray)
					{
						if (isElementType)
						{
							throw new BadImageFormatException();
						}
						CustomAttributeDecoder<TType>.ArgumentTypeInfo argumentTypeInfo2 = this.DecodeNamedArgumentType(ref valueReader, true);
						argumentTypeInfo.ElementType = argumentTypeInfo2.Type;
						argumentTypeInfo.ElementTypeCode = argumentTypeInfo2.TypeCode;
						argumentTypeInfo.Type = this._provider.GetSZArrayType(argumentTypeInfo.ElementType);
						return argumentTypeInfo;
					}
					break;
				}
			}
			else
			{
				if (typeCode == SerializationTypeCode.Type)
				{
					argumentTypeInfo.Type = this._provider.GetSystemType();
					return argumentTypeInfo;
				}
				if (typeCode == SerializationTypeCode.TaggedObject)
				{
					argumentTypeInfo.Type = this._provider.GetPrimitiveType(PrimitiveTypeCode.Object);
					return argumentTypeInfo;
				}
				if (typeCode == SerializationTypeCode.Enum)
				{
					string name = valueReader.ReadSerializedString();
					argumentTypeInfo.Type = this._provider.GetTypeFromSerializedName(name);
					argumentTypeInfo.TypeCode = (SerializationTypeCode)this._provider.GetUnderlyingEnumType(argumentTypeInfo.Type);
					return argumentTypeInfo;
				}
			}
			throw new BadImageFormatException();
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
		private CustomAttributeTypedArgument<TType> DecodeArgument(ref BlobReader valueReader, CustomAttributeDecoder<TType>.ArgumentTypeInfo info)
		{
			if (info.TypeCode == SerializationTypeCode.TaggedObject)
			{
				info = this.DecodeNamedArgumentType(ref valueReader, false);
			}
			SerializationTypeCode typeCode = info.TypeCode;
			object value;
			switch (typeCode)
			{
			case SerializationTypeCode.Boolean:
				value = valueReader.ReadBoolean();
				break;
			case SerializationTypeCode.Char:
				value = valueReader.ReadChar();
				break;
			case SerializationTypeCode.SByte:
				value = valueReader.ReadSByte();
				break;
			case SerializationTypeCode.Byte:
				value = valueReader.ReadByte();
				break;
			case SerializationTypeCode.Int16:
				value = valueReader.ReadInt16();
				break;
			case SerializationTypeCode.UInt16:
				value = valueReader.ReadUInt16();
				break;
			case SerializationTypeCode.Int32:
				value = valueReader.ReadInt32();
				break;
			case SerializationTypeCode.UInt32:
				value = valueReader.ReadUInt32();
				break;
			case SerializationTypeCode.Int64:
				value = valueReader.ReadInt64();
				break;
			case SerializationTypeCode.UInt64:
				value = valueReader.ReadUInt64();
				break;
			case SerializationTypeCode.Single:
				value = valueReader.ReadSingle();
				break;
			case SerializationTypeCode.Double:
				value = valueReader.ReadDouble();
				break;
			case SerializationTypeCode.String:
				value = valueReader.ReadSerializedString();
				break;
			default:
				if (typeCode != SerializationTypeCode.SZArray)
				{
					if (typeCode != SerializationTypeCode.Type)
					{
						throw new BadImageFormatException();
					}
					string name = valueReader.ReadSerializedString();
					value = this._provider.GetTypeFromSerializedName(name);
				}
				else
				{
					value = this.DecodeArrayArgument(ref valueReader, info);
				}
				break;
			}
			return new CustomAttributeTypedArgument<TType>(info.Type, value);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001DE3C File Offset: 0x0001C03C
		private ImmutableArray<CustomAttributeTypedArgument<TType>>? DecodeArrayArgument(ref BlobReader blobReader, CustomAttributeDecoder<TType>.ArgumentTypeInfo info)
		{
			int num = blobReader.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			if (num == 0)
			{
				return new ImmutableArray<CustomAttributeTypedArgument<TType>>?(ImmutableArray<CustomAttributeTypedArgument<TType>>.Empty);
			}
			if (num < 0)
			{
				throw new BadImageFormatException();
			}
			CustomAttributeDecoder<TType>.ArgumentTypeInfo info2 = new CustomAttributeDecoder<TType>.ArgumentTypeInfo
			{
				Type = info.ElementType,
				TypeCode = info.ElementTypeCode
			};
			ImmutableArray<CustomAttributeTypedArgument<TType>>.Builder builder = ImmutableArray.CreateBuilder<CustomAttributeTypedArgument<TType>>(num);
			for (int i = 0; i < num; i++)
			{
				builder.Add(this.DecodeArgument(ref blobReader, info2));
			}
			return new ImmutableArray<CustomAttributeTypedArgument<TType>>?(builder.MoveToImmutable());
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001DECC File Offset: 0x0001C0CC
		private TType GetTypeFromHandle(EntityHandle handle)
		{
			HandleKind kind = handle.Kind;
			if (kind == HandleKind.TypeReference)
			{
				return this._provider.GetTypeFromReference(this._reader, (TypeReferenceHandle)handle, SignatureTypeHandleCode.Unresolved);
			}
			if (kind == HandleKind.TypeDefinition)
			{
				return this._provider.GetTypeFromDefinition(this._reader, (TypeDefinitionHandle)handle, SignatureTypeHandleCode.Unresolved);
			}
			throw new BadImageFormatException(SR.NotTypeDefOrRefHandle);
		}

		// Token: 0x040008BD RID: 2237
		private readonly ICustomAttributeTypeProvider<TType> _provider;

		// Token: 0x040008BE RID: 2238
		private readonly MetadataReader _reader;

		// Token: 0x020001D7 RID: 471
		private struct ArgumentTypeInfo
		{
			// Token: 0x04000B49 RID: 2889
			public TType Type;

			// Token: 0x04000B4A RID: 2890
			public TType ElementType;

			// Token: 0x04000B4B RID: 2891
			public SerializationTypeCode TypeCode;

			// Token: 0x04000B4C RID: 2892
			public SerializationTypeCode ElementTypeCode;
		}
	}
}
