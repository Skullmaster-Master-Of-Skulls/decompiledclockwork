using System;
using System.IO;
using SevenZip.Compression.LZ;
using SevenZip.Compression.RangeCoder;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x0200001E RID: 30
	public class Decoder : ICoder, ISetDecoderProperties
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000630C File Offset: 0x0000450C
		public Decoder()
		{
			this.m_DictionarySize = uint.MaxValue;
			int num = 0;
			while ((long)num < 4L)
			{
				this.m_PosSlotDecoder[num] = new BitTreeDecoder(6);
				num++;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006404 File Offset: 0x00004604
		private void SetDictionarySize(uint dictionarySize)
		{
			if (this.m_DictionarySize != dictionarySize)
			{
				this.m_DictionarySize = dictionarySize;
				this.m_DictionarySizeCheck = Math.Max(this.m_DictionarySize, 1U);
				uint windowSize = Math.Max(this.m_DictionarySizeCheck, 4096U);
				this.m_OutWindow.Create(windowSize);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006450 File Offset: 0x00004650
		private void SetLiteralProperties(int lp, int lc)
		{
			if (lp > 8)
			{
				throw new InvalidParamException();
			}
			if (lc > 8)
			{
				throw new InvalidParamException();
			}
			this.m_LiteralDecoder.Create(lp, lc);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006474 File Offset: 0x00004674
		private void SetPosBitsProperties(int pb)
		{
			if (pb > 4)
			{
				throw new InvalidParamException();
			}
			uint num = 1U << pb;
			this.m_LenDecoder.Create(num);
			this.m_RepLenDecoder.Create(num);
			this.m_PosStateMask = num - 1U;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000064B4 File Offset: 0x000046B4
		private void Init(Stream inStream, Stream outStream)
		{
			this.m_RangeDecoder.Init(inStream);
			this.m_OutWindow.Init(outStream);
			for (uint num = 0U; num < 12U; num += 1U)
			{
				for (uint num2 = 0U; num2 <= this.m_PosStateMask; num2 += 1U)
				{
					uint num3 = (num << 4) + num2;
					this.m_IsMatchDecoders[(int)((UIntPtr)num3)].Init();
					this.m_IsRep0LongDecoders[(int)((UIntPtr)num3)].Init();
				}
				this.m_IsRepDecoders[(int)((UIntPtr)num)].Init();
				this.m_IsRepG0Decoders[(int)((UIntPtr)num)].Init();
				this.m_IsRepG1Decoders[(int)((UIntPtr)num)].Init();
				this.m_IsRepG2Decoders[(int)((UIntPtr)num)].Init();
			}
			this.m_LiteralDecoder.Init();
			for (uint num = 0U; num < 4U; num += 1U)
			{
				this.m_PosSlotDecoder[(int)((UIntPtr)num)].Init();
			}
			for (uint num = 0U; num < 114U; num += 1U)
			{
				this.m_PosDecoders[(int)((UIntPtr)num)].Init();
			}
			this.m_LenDecoder.Init();
			this.m_RepLenDecoder.Init();
			this.m_PosAlignDecoder.Init();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000065D8 File Offset: 0x000047D8
		public void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress)
		{
			this.Init(inStream, outStream);
			Base.State state = default(Base.State);
			state.Init();
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			ulong num5 = 0UL;
			if (num5 < (ulong)outSize)
			{
				if (this.m_IsMatchDecoders[(int)((UIntPtr)(state.Index << 4))].Decode(this.m_RangeDecoder) != 0U)
				{
					throw new DataErrorException();
				}
				state.UpdateChar();
				byte b = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, 0U, 0);
				this.m_OutWindow.PutByte(b);
				num5 += 1UL;
			}
			while (num5 < (ulong)outSize)
			{
				uint num6 = (uint)num5 & this.m_PosStateMask;
				if (this.m_IsMatchDecoders[(int)((UIntPtr)((state.Index << 4) + num6))].Decode(this.m_RangeDecoder) == 0U)
				{
					byte @byte = this.m_OutWindow.GetByte(0U);
					byte b2;
					if (!state.IsCharState())
					{
						b2 = this.m_LiteralDecoder.DecodeWithMatchByte(this.m_RangeDecoder, (uint)num5, @byte, this.m_OutWindow.GetByte(num));
					}
					else
					{
						b2 = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, (uint)num5, @byte);
					}
					this.m_OutWindow.PutByte(b2);
					state.UpdateChar();
					num5 += 1UL;
				}
				else
				{
					uint num8;
					if (this.m_IsRepDecoders[(int)((UIntPtr)state.Index)].Decode(this.m_RangeDecoder) == 1U)
					{
						if (this.m_IsRepG0Decoders[(int)((UIntPtr)state.Index)].Decode(this.m_RangeDecoder) == 0U)
						{
							if (this.m_IsRep0LongDecoders[(int)((UIntPtr)((state.Index << 4) + num6))].Decode(this.m_RangeDecoder) == 0U)
							{
								state.UpdateShortRep();
								this.m_OutWindow.PutByte(this.m_OutWindow.GetByte(num));
								num5 += 1UL;
								continue;
							}
						}
						else
						{
							uint num7;
							if (this.m_IsRepG1Decoders[(int)((UIntPtr)state.Index)].Decode(this.m_RangeDecoder) == 0U)
							{
								num7 = num2;
							}
							else
							{
								if (this.m_IsRepG2Decoders[(int)((UIntPtr)state.Index)].Decode(this.m_RangeDecoder) == 0U)
								{
									num7 = num3;
								}
								else
								{
									num7 = num4;
									num4 = num3;
								}
								num3 = num2;
							}
							num2 = num;
							num = num7;
						}
						num8 = this.m_RepLenDecoder.Decode(this.m_RangeDecoder, num6) + 2U;
						state.UpdateRep();
					}
					else
					{
						num4 = num3;
						num3 = num2;
						num2 = num;
						num8 = 2U + this.m_LenDecoder.Decode(this.m_RangeDecoder, num6);
						state.UpdateMatch();
						uint num9 = this.m_PosSlotDecoder[(int)((UIntPtr)Base.GetLenToPosState(num8))].Decode(this.m_RangeDecoder);
						if (num9 >= 4U)
						{
							int num10 = (int)((num9 >> 1) - 1U);
							num = (2U | (num9 & 1U)) << num10;
							if (num9 < 14U)
							{
								num += BitTreeDecoder.ReverseDecode(this.m_PosDecoders, num - num9 - 1U, this.m_RangeDecoder, num10);
							}
							else
							{
								num += this.m_RangeDecoder.DecodeDirectBits(num10 - 4) << 4;
								num += this.m_PosAlignDecoder.ReverseDecode(this.m_RangeDecoder);
							}
						}
						else
						{
							num = num9;
						}
					}
					if ((ulong)num >= num5 || num >= this.m_DictionarySizeCheck)
					{
						if (num != 4294967295U)
						{
							throw new DataErrorException();
						}
						break;
					}
					else
					{
						this.m_OutWindow.CopyBlock(num, num8);
						num5 += (ulong)num8;
					}
				}
			}
			this.m_OutWindow.Flush();
			this.m_OutWindow.ReleaseStream();
			this.m_RangeDecoder.ReleaseStream();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006934 File Offset: 0x00004B34
		public void SetDecoderProperties(byte[] properties)
		{
			if (properties.Length < 5)
			{
				throw new InvalidParamException();
			}
			int lc = (int)(properties[0] % 9);
			int num = (int)(properties[0] / 9);
			int lp = num % 5;
			int num2 = num / 5;
			if (num2 > 4)
			{
				throw new InvalidParamException();
			}
			uint num3 = 0U;
			for (int i = 0; i < 4; i++)
			{
				num3 += (uint)((uint)properties[1 + i] << i * 8);
			}
			this.SetDictionarySize(num3);
			this.SetLiteralProperties(lp, lc);
			this.SetPosBitsProperties(num2);
		}

		// Token: 0x040000B7 RID: 183
		private OutWindow m_OutWindow = new OutWindow();

		// Token: 0x040000B8 RID: 184
		private Decoder m_RangeDecoder = new Decoder();

		// Token: 0x040000B9 RID: 185
		private BitDecoder[] m_IsMatchDecoders = new BitDecoder[192];

		// Token: 0x040000BA RID: 186
		private BitDecoder[] m_IsRepDecoders = new BitDecoder[12];

		// Token: 0x040000BB RID: 187
		private BitDecoder[] m_IsRepG0Decoders = new BitDecoder[12];

		// Token: 0x040000BC RID: 188
		private BitDecoder[] m_IsRepG1Decoders = new BitDecoder[12];

		// Token: 0x040000BD RID: 189
		private BitDecoder[] m_IsRepG2Decoders = new BitDecoder[12];

		// Token: 0x040000BE RID: 190
		private BitDecoder[] m_IsRep0LongDecoders = new BitDecoder[192];

		// Token: 0x040000BF RID: 191
		private BitTreeDecoder[] m_PosSlotDecoder = new BitTreeDecoder[4];

		// Token: 0x040000C0 RID: 192
		private BitDecoder[] m_PosDecoders = new BitDecoder[114];

		// Token: 0x040000C1 RID: 193
		private BitTreeDecoder m_PosAlignDecoder = new BitTreeDecoder(4);

		// Token: 0x040000C2 RID: 194
		private Decoder.LenDecoder m_LenDecoder = new Decoder.LenDecoder();

		// Token: 0x040000C3 RID: 195
		private Decoder.LenDecoder m_RepLenDecoder = new Decoder.LenDecoder();

		// Token: 0x040000C4 RID: 196
		private Decoder.LiteralDecoder m_LiteralDecoder = new Decoder.LiteralDecoder();

		// Token: 0x040000C5 RID: 197
		private uint m_DictionarySize;

		// Token: 0x040000C6 RID: 198
		private uint m_DictionarySizeCheck;

		// Token: 0x040000C7 RID: 199
		private uint m_PosStateMask;

		// Token: 0x0200001F RID: 31
		private class LenDecoder
		{
			// Token: 0x06000098 RID: 152 RVA: 0x000069AC File Offset: 0x00004BAC
			public void Create(uint numPosStates)
			{
				for (uint num = this.m_NumPosStates; num < numPosStates; num += 1U)
				{
					this.m_LowCoder[(int)((UIntPtr)num)] = new BitTreeDecoder(3);
					this.m_MidCoder[(int)((UIntPtr)num)] = new BitTreeDecoder(3);
				}
				this.m_NumPosStates = numPosStates;
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00006A04 File Offset: 0x00004C04
			public void Init()
			{
				this.m_Choice.Init();
				for (uint num = 0U; num < this.m_NumPosStates; num += 1U)
				{
					this.m_LowCoder[(int)((UIntPtr)num)].Init();
					this.m_MidCoder[(int)((UIntPtr)num)].Init();
				}
				this.m_Choice2.Init();
				this.m_HighCoder.Init();
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00006A68 File Offset: 0x00004C68
			public uint Decode(Decoder rangeDecoder, uint posState)
			{
				if (this.m_Choice.Decode(rangeDecoder) == 0U)
				{
					return this.m_LowCoder[(int)((UIntPtr)posState)].Decode(rangeDecoder);
				}
				uint num = 8U;
				if (this.m_Choice2.Decode(rangeDecoder) == 0U)
				{
					num += this.m_MidCoder[(int)((UIntPtr)posState)].Decode(rangeDecoder);
				}
				else
				{
					num += 8U;
					num += this.m_HighCoder.Decode(rangeDecoder);
				}
				return num;
			}

			// Token: 0x040000C8 RID: 200
			private BitDecoder m_Choice = default(BitDecoder);

			// Token: 0x040000C9 RID: 201
			private BitDecoder m_Choice2 = default(BitDecoder);

			// Token: 0x040000CA RID: 202
			private BitTreeDecoder[] m_LowCoder = new BitTreeDecoder[16];

			// Token: 0x040000CB RID: 203
			private BitTreeDecoder[] m_MidCoder = new BitTreeDecoder[16];

			// Token: 0x040000CC RID: 204
			private BitTreeDecoder m_HighCoder = new BitTreeDecoder(8);

			// Token: 0x040000CD RID: 205
			private uint m_NumPosStates;
		}

		// Token: 0x02000020 RID: 32
		private class LiteralDecoder
		{
			// Token: 0x0600009C RID: 156 RVA: 0x00006B28 File Offset: 0x00004D28
			public void Create(int numPosBits, int numPrevBits)
			{
				if (this.m_Coders != null && this.m_NumPrevBits == numPrevBits && this.m_NumPosBits == numPosBits)
				{
					return;
				}
				this.m_NumPosBits = numPosBits;
				this.m_PosMask = (1U << numPosBits) - 1U;
				this.m_NumPrevBits = numPrevBits;
				uint num = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				this.m_Coders = new Decoder.LiteralDecoder.Decoder2[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)((UIntPtr)num2)].Create();
				}
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00006BAC File Offset: 0x00004DAC
			public void Init()
			{
				uint num = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)((UIntPtr)num2)].Init();
				}
			}

			// Token: 0x0600009E RID: 158 RVA: 0x00006BEA File Offset: 0x00004DEA
			private uint GetState(uint pos, byte prevByte)
			{
				return ((pos & this.m_PosMask) << this.m_NumPrevBits) + (uint)(prevByte >> 8 - this.m_NumPrevBits);
			}

			// Token: 0x0600009F RID: 159 RVA: 0x00006C0C File Offset: 0x00004E0C
			public byte DecodeNormal(Decoder rangeDecoder, uint pos, byte prevByte)
			{
				return this.m_Coders[(int)((UIntPtr)this.GetState(pos, prevByte))].DecodeNormal(rangeDecoder);
			}

			// Token: 0x060000A0 RID: 160 RVA: 0x00006C28 File Offset: 0x00004E28
			public byte DecodeWithMatchByte(Decoder rangeDecoder, uint pos, byte prevByte, byte matchByte)
			{
				return this.m_Coders[(int)((UIntPtr)this.GetState(pos, prevByte))].DecodeWithMatchByte(rangeDecoder, matchByte);
			}

			// Token: 0x040000CE RID: 206
			private Decoder.LiteralDecoder.Decoder2[] m_Coders;

			// Token: 0x040000CF RID: 207
			private int m_NumPrevBits;

			// Token: 0x040000D0 RID: 208
			private int m_NumPosBits;

			// Token: 0x040000D1 RID: 209
			private uint m_PosMask;

			// Token: 0x02000021 RID: 33
			private struct Decoder2
			{
				// Token: 0x060000A2 RID: 162 RVA: 0x00006C4E File Offset: 0x00004E4E
				public void Create()
				{
					this.m_Decoders = new BitDecoder[768];
				}

				// Token: 0x060000A3 RID: 163 RVA: 0x00006C60 File Offset: 0x00004E60
				public void Init()
				{
					for (int i = 0; i < 768; i++)
					{
						this.m_Decoders[i].Init();
					}
				}

				// Token: 0x060000A4 RID: 164 RVA: 0x00006C90 File Offset: 0x00004E90
				public byte DecodeNormal(Decoder rangeDecoder)
				{
					uint num = 1U;
					do
					{
						num = (num << 1 | this.m_Decoders[(int)((UIntPtr)num)].Decode(rangeDecoder));
					}
					while (num < 256U);
					return (byte)num;
				}

				// Token: 0x060000A5 RID: 165 RVA: 0x00006CC4 File Offset: 0x00004EC4
				public byte DecodeWithMatchByte(Decoder rangeDecoder, byte matchByte)
				{
					uint num = 1U;
					for (;;)
					{
						uint num2 = (uint)(matchByte >> 7 & 1);
						matchByte = (byte)(matchByte << 1);
						uint num3 = this.m_Decoders[(int)((UIntPtr)((1U + num2 << 8) + num))].Decode(rangeDecoder);
						num = (num << 1 | num3);
						if (num2 != num3)
						{
							break;
						}
						if (num >= 256U)
						{
							goto IL_5E;
						}
					}
					while (num < 256U)
					{
						num = (num << 1 | this.m_Decoders[(int)((UIntPtr)num)].Decode(rangeDecoder));
					}
					IL_5E:
					return (byte)num;
				}

				// Token: 0x040000D2 RID: 210
				private BitDecoder[] m_Decoders;
			}
		}
	}
}
