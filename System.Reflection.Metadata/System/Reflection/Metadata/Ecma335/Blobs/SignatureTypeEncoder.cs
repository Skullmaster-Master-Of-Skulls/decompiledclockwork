using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000137 RID: 311
	internal struct SignatureTypeEncoder
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0001D1D2 File Offset: 0x0001B3D2
		public BlobBuilder Builder { get; }

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001D1DA File Offset: 0x0001B3DA
		public SignatureTypeEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001D1E3 File Offset: 0x0001B3E3
		private void WriteTypeCode(SignatureTypeCode value)
		{
			this.Builder.WriteByte((byte)value);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0001D1F1 File Offset: 0x0001B3F1
		private void ClassOrValue(bool isValueType)
		{
			this.Builder.WriteByte(isValueType ? 17 : 18);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001D207 File Offset: 0x0001B407
		public void Boolean()
		{
			this.WriteTypeCode(SignatureTypeCode.Boolean);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0001D210 File Offset: 0x0001B410
		public void Char()
		{
			this.WriteTypeCode(SignatureTypeCode.Char);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0001D219 File Offset: 0x0001B419
		public void Int8()
		{
			this.WriteTypeCode(SignatureTypeCode.SByte);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0001D222 File Offset: 0x0001B422
		public void UInt8()
		{
			this.WriteTypeCode(SignatureTypeCode.Byte);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001D22B File Offset: 0x0001B42B
		public void Int16()
		{
			this.WriteTypeCode(SignatureTypeCode.Int16);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0001D234 File Offset: 0x0001B434
		public void UInt16()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt16);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0001D23D File Offset: 0x0001B43D
		public void Int32()
		{
			this.WriteTypeCode(SignatureTypeCode.Int32);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0001D246 File Offset: 0x0001B446
		public void UInt32()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt32);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0001D250 File Offset: 0x0001B450
		public void Int64()
		{
			this.WriteTypeCode(SignatureTypeCode.Int64);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001D25A File Offset: 0x0001B45A
		public void UInt64()
		{
			this.WriteTypeCode(SignatureTypeCode.UInt64);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0001D264 File Offset: 0x0001B464
		public void Float32()
		{
			this.WriteTypeCode(SignatureTypeCode.Single);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0001D26E File Offset: 0x0001B46E
		public void Float64()
		{
			this.WriteTypeCode(SignatureTypeCode.Double);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0001D278 File Offset: 0x0001B478
		public void String()
		{
			this.WriteTypeCode(SignatureTypeCode.String);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0001D282 File Offset: 0x0001B482
		public void IntPtr()
		{
			this.WriteTypeCode(SignatureTypeCode.IntPtr);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0001D28C File Offset: 0x0001B48C
		public void UIntPtr()
		{
			this.WriteTypeCode(SignatureTypeCode.UIntPtr);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0001D296 File Offset: 0x0001B496
		public void Object()
		{
			this.WriteTypeCode(SignatureTypeCode.Object);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0001D2A0 File Offset: 0x0001B4A0
		public void Array(out SignatureTypeEncoder elementType, out ArrayShapeEncoder arrayShape)
		{
			this.Builder.WriteByte(20);
			elementType = this;
			arrayShape = new ArrayShapeEncoder(this.Builder);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		public void TypeDefOrRefOrSpec(bool isValueType, EntityHandle typeRefDefSpec)
		{
			this.ClassOrValue(isValueType);
			this.Builder.WriteCompressedInteger(CodedIndex.ToTypeDefOrRefOrSpec(typeRefDefSpec));
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public MethodSignatureEncoder FunctionPointer(SignatureCallingConvention convention, FunctionPointerAttributes attributes, int genericParameterCount)
		{
			if (attributes != FunctionPointerAttributes.None && attributes != FunctionPointerAttributes.HasThis && attributes != FunctionPointerAttributes.HasExplicitThis)
			{
				throw new ArgumentException(SR.InvalidSignature, "attributes");
			}
			this.Builder.WriteByte(27);
			this.Builder.WriteByte(BlobEncoder.SignatureHeader(SignatureKind.Method, convention, (SignatureAttributes)attributes).RawValue);
			if (genericParameterCount != 0)
			{
				this.Builder.WriteCompressedInteger(genericParameterCount);
			}
			return new MethodSignatureEncoder(this.Builder, convention == SignatureCallingConvention.VarArgs);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0001D359 File Offset: 0x0001B559
		public GenericTypeArgumentsEncoder GenericInstantiation(bool isValueType, EntityHandle typeRefDefSpec, int genericArgumentCount)
		{
			this.Builder.WriteByte(21);
			this.ClassOrValue(isValueType);
			this.Builder.WriteCompressedInteger(CodedIndex.ToTypeDefOrRefOrSpec(typeRefDefSpec));
			this.Builder.WriteCompressedInteger(genericArgumentCount);
			return new GenericTypeArgumentsEncoder(this.Builder);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0001D397 File Offset: 0x0001B597
		public void GenericMethodTypeParameter(int parameterIndex)
		{
			this.Builder.WriteByte(30);
			this.Builder.WriteCompressedInteger(parameterIndex);
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0001D3B2 File Offset: 0x0001B5B2
		public void GenericTypeParameter(int parameterIndex)
		{
			this.Builder.WriteByte(19);
			this.Builder.WriteCompressedInteger(parameterIndex);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001D3CD File Offset: 0x0001B5CD
		public SignatureTypeEncoder Pointer()
		{
			this.Builder.WriteByte(15);
			return this;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001D3E2 File Offset: 0x0001B5E2
		public void VoidPointer()
		{
			this.Builder.WriteByte(15);
			this.Builder.WriteByte(1);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001D3FD File Offset: 0x0001B5FD
		public SignatureTypeEncoder SZArray()
		{
			this.Builder.WriteByte(29);
			return this;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001D412 File Offset: 0x0001B612
		public CustomModifiersEncoder CustomModifiers()
		{
			return new CustomModifiersEncoder(this.Builder);
		}
	}
}
