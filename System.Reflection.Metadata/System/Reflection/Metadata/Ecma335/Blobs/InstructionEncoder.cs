using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000123 RID: 291
	internal struct InstructionEncoder
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0001C7A9 File Offset: 0x0001A9A9
		public BlobBuilder Builder { get; }

		// Token: 0x0600099D RID: 2461 RVA: 0x0001C7B1 File Offset: 0x0001A9B1
		public InstructionEncoder(BlobBuilder builder)
		{
			this.Builder = builder;
			this._startPosition = builder.Count;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0001C7C6 File Offset: 0x0001A9C6
		public int Offset
		{
			get
			{
				return this.Builder.Count - this._startPosition;
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001C7DA File Offset: 0x0001A9DA
		public void OpCode(ILOpCode code)
		{
			if ((ILOpCode)((byte)code) == code)
			{
				this.Builder.WriteByte((byte)code);
				return;
			}
			this.Builder.WriteUInt16BE((ushort)code);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001C7FB File Offset: 0x0001A9FB
		public void Token(EntityHandle handle)
		{
			this.Token(MetadataTokens.GetToken(handle));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001C809 File Offset: 0x0001AA09
		public void Token(int token)
		{
			this.Builder.WriteInt32(token);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001C809 File Offset: 0x0001AA09
		public void LongBranchTarget(int ilOffset)
		{
			this.Builder.WriteInt32(ilOffset);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001C817 File Offset: 0x0001AA17
		public void ShortBranchTarget(byte ilOffset)
		{
			this.Builder.WriteByte(ilOffset);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0001C825 File Offset: 0x0001AA25
		public void LoadString(UserStringHandle handle)
		{
			this.OpCode(ILOpCode.Ldstr);
			this.Token(MetadataTokens.GetToken(handle));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001C840 File Offset: 0x0001AA40
		public void Call(EntityHandle methodHandle)
		{
			this.OpCode(ILOpCode.Call);
			this.Token(methodHandle);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001C851 File Offset: 0x0001AA51
		public void CallIndirect(StandaloneSignatureHandle signature)
		{
			this.OpCode(ILOpCode.Calli);
			this.Token(signature);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001C868 File Offset: 0x0001AA68
		public void LoadConstantI4(int value)
		{
			ILOpCode code;
			switch (value)
			{
			case -1:
				code = ILOpCode.Ldc_i4_m1;
				break;
			case 0:
				code = ILOpCode.Ldc_i4_0;
				break;
			case 1:
				code = ILOpCode.Ldc_i4_1;
				break;
			case 2:
				code = ILOpCode.Ldc_i4_2;
				break;
			case 3:
				code = ILOpCode.Ldc_i4_3;
				break;
			case 4:
				code = ILOpCode.Ldc_i4_4;
				break;
			case 5:
				code = ILOpCode.Ldc_i4_5;
				break;
			case 6:
				code = ILOpCode.Ldc_i4_6;
				break;
			case 7:
				code = ILOpCode.Ldc_i4_7;
				break;
			case 8:
				code = ILOpCode.Ldc_i4_8;
				break;
			default:
				if ((int)((sbyte)value) == value)
				{
					this.OpCode(ILOpCode.Ldc_i4_s);
					this.Builder.WriteSByte((sbyte)value);
					return;
				}
				this.OpCode(ILOpCode.Ldc_i4);
				this.Builder.WriteInt32(value);
				return;
			}
			this.OpCode(code);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001C910 File Offset: 0x0001AB10
		public void LoadConstantI8(long value)
		{
			this.OpCode(ILOpCode.Ldc_i8);
			this.Builder.WriteInt64(value);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001C926 File Offset: 0x0001AB26
		public void LoadConstantR4(float value)
		{
			this.OpCode(ILOpCode.Ldc_r4);
			this.Builder.WriteSingle(value);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001C93C File Offset: 0x0001AB3C
		public void LoadConstantR8(double value)
		{
			this.OpCode(ILOpCode.Ldc_r8);
			this.Builder.WriteDouble(value);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001C954 File Offset: 0x0001AB54
		public void LoadLocal(int slotIndex)
		{
			switch (slotIndex)
			{
			case 0:
				this.OpCode(ILOpCode.Ldloc_0);
				return;
			case 1:
				this.OpCode(ILOpCode.Ldloc_1);
				return;
			case 2:
				this.OpCode(ILOpCode.Ldloc_2);
				return;
			case 3:
				this.OpCode(ILOpCode.Ldloc_3);
				return;
			default:
				if (slotIndex < 255)
				{
					this.OpCode(ILOpCode.Ldloc_s);
					this.Builder.WriteByte((byte)slotIndex);
					return;
				}
				this.OpCode(ILOpCode.Ldloc);
				this.Builder.WriteInt32(slotIndex);
				return;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		public void StoreLocal(int slotIndex)
		{
			switch (slotIndex)
			{
			case 0:
				this.OpCode(ILOpCode.Stloc_0);
				return;
			case 1:
				this.OpCode(ILOpCode.Stloc_1);
				return;
			case 2:
				this.OpCode(ILOpCode.Stloc_2);
				return;
			case 3:
				this.OpCode(ILOpCode.Stloc_3);
				return;
			default:
				if (slotIndex < 255)
				{
					this.OpCode(ILOpCode.Stloc_s);
					this.Builder.WriteByte((byte)slotIndex);
					return;
				}
				this.OpCode(ILOpCode.Stloc);
				this.Builder.WriteInt32(slotIndex);
				return;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001CA4E File Offset: 0x0001AC4E
		public void LoadLocalAddress(int slotIndex)
		{
			if (slotIndex < 255)
			{
				this.OpCode(ILOpCode.Ldloca_s);
				this.Builder.WriteByte((byte)slotIndex);
				return;
			}
			this.OpCode(ILOpCode.Ldloca);
			this.Builder.WriteInt32(slotIndex);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001CA88 File Offset: 0x0001AC88
		public void LoadArgument(int argumentIndex)
		{
			switch (argumentIndex)
			{
			case 0:
				this.OpCode(ILOpCode.Ldarg_0);
				return;
			case 1:
				this.OpCode(ILOpCode.Ldarg_1);
				return;
			case 2:
				this.OpCode(ILOpCode.Ldarg_2);
				return;
			case 3:
				this.OpCode(ILOpCode.Ldarg_3);
				return;
			default:
				if (argumentIndex < 255)
				{
					this.OpCode(ILOpCode.Ldarg_s);
					this.Builder.WriteByte((byte)argumentIndex);
					return;
				}
				this.OpCode(ILOpCode.Ldarg);
				this.Builder.WriteInt32(argumentIndex);
				return;
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001CB02 File Offset: 0x0001AD02
		public void LoadArgumentAddress(int argumentIndex)
		{
			if (argumentIndex < 255)
			{
				this.OpCode(ILOpCode.Ldarga_s);
				this.Builder.WriteByte((byte)argumentIndex);
				return;
			}
			this.OpCode(ILOpCode.Ldarga);
			this.Builder.WriteInt32(argumentIndex);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001CB39 File Offset: 0x0001AD39
		public void StoreArgument(int argumentIndex)
		{
			if (argumentIndex < 255)
			{
				this.OpCode(ILOpCode.Starg_s);
				this.Builder.WriteByte((byte)argumentIndex);
				return;
			}
			this.OpCode(ILOpCode.Starg);
			this.Builder.WriteInt32(argumentIndex);
		}

		// Token: 0x04000895 RID: 2197
		private readonly int _startPosition;
	}
}
