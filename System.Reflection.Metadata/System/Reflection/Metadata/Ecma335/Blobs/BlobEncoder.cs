using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000124 RID: 292
	internal struct BlobEncoder
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001CB70 File Offset: 0x0001AD70
		public BlobBuilder Builder { get; }

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001CB78 File Offset: 0x0001AD78
		public BlobEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001CB81 File Offset: 0x0001AD81
		public SignatureTypeEncoder FieldSignature()
		{
			this.Builder.WriteByte(6);
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0001CB9A File Offset: 0x0001AD9A
		public GenericTypeArgumentsEncoder MethodSpecificationSignature(int genericArgumentCount)
		{
			this.Builder.WriteByte(10);
			this.Builder.WriteCompressedInteger(genericArgumentCount);
			return new GenericTypeArgumentsEncoder(this.Builder);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		public MethodSignatureEncoder MethodSignature(SignatureCallingConvention convention = SignatureCallingConvention.Default, int genericParameterCount = 0, bool isInstanceMethod = false)
		{
			SignatureAttributes attributes = ((genericParameterCount != 0) ? SignatureAttributes.Generic : SignatureAttributes.None) | (isInstanceMethod ? SignatureAttributes.Instance : SignatureAttributes.None);
			this.Builder.WriteByte(BlobEncoder.SignatureHeader(SignatureKind.Method, convention, attributes).RawValue);
			if (genericParameterCount != 0)
			{
				this.Builder.WriteCompressedInteger(genericParameterCount);
			}
			return new MethodSignatureEncoder(this.Builder, convention == SignatureCallingConvention.VarArgs);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001CC18 File Offset: 0x0001AE18
		public MethodSignatureEncoder PropertySignature(bool isInstanceProperty = false)
		{
			this.Builder.WriteByte(BlobEncoder.SignatureHeader(SignatureKind.Property, SignatureCallingConvention.Default, isInstanceProperty ? SignatureAttributes.Instance : SignatureAttributes.None).RawValue);
			return new MethodSignatureEncoder(this.Builder, false);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001CC53 File Offset: 0x0001AE53
		public void CustomAttributeSignature(out FixedArgumentsEncoder fixedArguments, out CustomAttributeNamedArgumentsEncoder namedArguments)
		{
			this.Builder.WriteUInt16(1);
			fixedArguments = new FixedArgumentsEncoder(this.Builder);
			namedArguments = new CustomAttributeNamedArgumentsEncoder(this.Builder);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0001CC83 File Offset: 0x0001AE83
		public LocalVariablesEncoder LocalVariableSignature(int count)
		{
			this.Builder.WriteByte(7);
			this.Builder.WriteCompressedInteger(count);
			return new LocalVariablesEncoder(this.Builder);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		public SignatureTypeEncoder TypeSpecificationSignature()
		{
			return new SignatureTypeEncoder(this.Builder);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001CCB5 File Offset: 0x0001AEB5
		public PermissionSetEncoder PermissionSetBlob(int attributeCount)
		{
			this.Builder.WriteByte(46);
			this.Builder.WriteCompressedInteger(attributeCount);
			return new PermissionSetEncoder(this.Builder);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001CCDB File Offset: 0x0001AEDB
		public NamedArgumentsEncoder PermissionSetArguments(int argumentCount)
		{
			this.Builder.WriteCompressedInteger(argumentCount);
			return new NamedArgumentsEncoder(this.Builder);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
		internal static SignatureHeader SignatureHeader(SignatureKind kind, SignatureCallingConvention convention, SignatureAttributes attributes)
		{
			return new SignatureHeader((byte)(kind | (SignatureKind)convention | (SignatureKind)attributes));
		}
	}
}
