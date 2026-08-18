using System;
using System.IO;
using SevenZip.Compression.LZ;
using SevenZip.Compression.RangeCoder;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x02000007 RID: 7
	public class Encoder : ICoder, ISetCoderProperties, IWriteCoderProperties
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002360 File Offset: 0x00000560
		static Encoder()
		{
			int num = 2;
			Encoder.g_FastPos[0] = 0;
			Encoder.g_FastPos[1] = 1;
			for (byte b = 2; b < 22; b += 1)
			{
				uint num2 = 1U << (b >> 1) - 1;
				uint num3 = 0U;
				while (num3 < num2)
				{
					Encoder.g_FastPos[num] = b;
					num3 += 1U;
					num++;
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023E0 File Offset: 0x000005E0
		private static uint GetPosSlot(uint pos)
		{
			if (pos < 2048U)
			{
				return (uint)Encoder.g_FastPos[(int)((UIntPtr)pos)];
			}
			if (pos < 2097152U)
			{
				return (uint)(Encoder.g_FastPos[(int)((UIntPtr)(pos >> 10))] + 20);
			}
			return (uint)(Encoder.g_FastPos[(int)((UIntPtr)(pos >> 20))] + 40);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002418 File Offset: 0x00000618
		private static uint GetPosSlot2(uint pos)
		{
			if (pos < 131072U)
			{
				return (uint)(Encoder.g_FastPos[(int)((UIntPtr)(pos >> 6))] + 12);
			}
			if (pos < 134217728U)
			{
				return (uint)(Encoder.g_FastPos[(int)((UIntPtr)(pos >> 16))] + 32);
			}
			return (uint)(Encoder.g_FastPos[(int)((UIntPtr)(pos >> 26))] + 52);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002458 File Offset: 0x00000658
		private void BaseInit()
		{
			this._state.Init();
			this._previousByte = 0;
			for (uint num = 0U; num < 4U; num += 1U)
			{
				this._repDistances[(int)((UIntPtr)num)] = 0U;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002490 File Offset: 0x00000690
		private void Create()
		{
			if (this._matchFinder == null)
			{
				BinTree binTree = new BinTree();
				int type = 4;
				if (this._matchFinderType == Encoder.EMatchFinderType.BT2)
				{
					type = 2;
				}
				binTree.SetType(type);
				this._matchFinder = binTree;
			}
			this._literalEncoder.Create(this._numLiteralPosStateBits, this._numLiteralContextBits);
			if (this._dictionarySize == this._dictionarySizePrev && this._numFastBytesPrev == this._numFastBytes)
			{
				return;
			}
			this._matchFinder.Create(this._dictionarySize, 4096U, this._numFastBytes, 274U);
			this._dictionarySizePrev = this._dictionarySize;
			this._numFastBytesPrev = this._numFastBytes;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002534 File Offset: 0x00000734
		public Encoder()
		{
			int num = 0;
			while ((long)num < 4096L)
			{
				this._optimum[num] = new Encoder.Optimal();
				num++;
			}
			int num2 = 0;
			while ((long)num2 < 4L)
			{
				this._posSlotEncoder[num2] = new BitTreeEncoder(6);
				num2++;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000271E File Offset: 0x0000091E
		private void SetWriteEndMarkerMode(bool writeEndMarker)
		{
			this._writeEndMark = writeEndMarker;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002728 File Offset: 0x00000928
		private void Init()
		{
			this.BaseInit();
			this._rangeEncoder.Init();
			for (uint num = 0U; num < 12U; num += 1U)
			{
				for (uint num2 = 0U; num2 <= this._posStateMask; num2 += 1U)
				{
					uint num3 = (num << 4) + num2;
					this._isMatch[(int)((UIntPtr)num3)].Init();
					this._isRep0Long[(int)((UIntPtr)num3)].Init();
				}
				this._isRep[(int)((UIntPtr)num)].Init();
				this._isRepG0[(int)((UIntPtr)num)].Init();
				this._isRepG1[(int)((UIntPtr)num)].Init();
				this._isRepG2[(int)((UIntPtr)num)].Init();
			}
			this._literalEncoder.Init();
			for (uint num = 0U; num < 4U; num += 1U)
			{
				this._posSlotEncoder[(int)((UIntPtr)num)].Init();
			}
			for (uint num = 0U; num < 114U; num += 1U)
			{
				this._posEncoders[(int)((UIntPtr)num)].Init();
			}
			this._lenEncoder.Init(1U << this._posStateBits);
			this._repMatchLenEncoder.Init(1U << this._posStateBits);
			this._posAlignEncoder.Init();
			this._longestMatchWasFound = false;
			this._optimumEndIndex = 0U;
			this._optimumCurrentIndex = 0U;
			this._additionalOffset = 0U;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002878 File Offset: 0x00000A78
		private void ReadMatchDistances(out uint lenRes, out uint numDistancePairs)
		{
			lenRes = 0U;
			numDistancePairs = this._matchFinder.GetMatches(this._matchDistances);
			if (numDistancePairs > 0U)
			{
				lenRes = this._matchDistances[(int)((UIntPtr)(numDistancePairs - 2U))];
				if (lenRes == this._numFastBytes)
				{
					lenRes += this._matchFinder.GetMatchLen((int)(lenRes - 1U), this._matchDistances[(int)((UIntPtr)(numDistancePairs - 1U))], 273U - lenRes);
				}
			}
			this._additionalOffset += 1U;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028EE File Offset: 0x00000AEE
		private void MovePos(uint num)
		{
			if (num > 0U)
			{
				this._matchFinder.Skip(num);
				this._additionalOffset += num;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000290E File Offset: 0x00000B0E
		private uint GetRepLen1Price(Base.State state, uint posState)
		{
			return this._isRepG0[(int)((UIntPtr)state.Index)].GetPrice0() + this._isRep0Long[(int)((UIntPtr)((state.Index << 4) + posState))].GetPrice0();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002948 File Offset: 0x00000B48
		private uint GetPureRepPrice(uint repIndex, Base.State state, uint posState)
		{
			uint num;
			if (repIndex == 0U)
			{
				num = this._isRepG0[(int)((UIntPtr)state.Index)].GetPrice0();
				num += this._isRep0Long[(int)((UIntPtr)((state.Index << 4) + posState))].GetPrice1();
			}
			else
			{
				num = this._isRepG0[(int)((UIntPtr)state.Index)].GetPrice1();
				if (repIndex == 1U)
				{
					num += this._isRepG1[(int)((UIntPtr)state.Index)].GetPrice0();
				}
				else
				{
					num += this._isRepG1[(int)((UIntPtr)state.Index)].GetPrice1();
					num += this._isRepG2[(int)((UIntPtr)state.Index)].GetPrice(repIndex - 2U);
				}
			}
			return num;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A08 File Offset: 0x00000C08
		private uint GetRepPrice(uint repIndex, uint len, Base.State state, uint posState)
		{
			uint price = this._repMatchLenEncoder.GetPrice(len - 2U, posState);
			return price + this.GetPureRepPrice(repIndex, state, posState);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A34 File Offset: 0x00000C34
		private uint GetPosLenPrice(uint pos, uint len, uint posState)
		{
			uint lenToPosState = Base.GetLenToPosState(len);
			uint num;
			if (pos < 128U)
			{
				num = this._distancesPrices[(int)((UIntPtr)(lenToPosState * 128U + pos))];
			}
			else
			{
				num = this._posSlotPrices[(int)((UIntPtr)((lenToPosState << 6) + Encoder.GetPosSlot2(pos)))] + this._alignPrices[(int)((UIntPtr)(pos & 15U))];
			}
			return num + this._lenEncoder.GetPrice(len - 2U, posState);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A98 File Offset: 0x00000C98
		private uint Backward(out uint backRes, uint cur)
		{
			this._optimumEndIndex = cur;
			uint posPrev = this._optimum[(int)((UIntPtr)cur)].PosPrev;
			uint backPrev = this._optimum[(int)((UIntPtr)cur)].BackPrev;
			do
			{
				if (this._optimum[(int)((UIntPtr)cur)].Prev1IsChar)
				{
					this._optimum[(int)((UIntPtr)posPrev)].MakeAsChar();
					this._optimum[(int)((UIntPtr)posPrev)].PosPrev = posPrev - 1U;
					if (this._optimum[(int)((UIntPtr)cur)].Prev2)
					{
						this._optimum[(int)((UIntPtr)(posPrev - 1U))].Prev1IsChar = false;
						this._optimum[(int)((UIntPtr)(posPrev - 1U))].PosPrev = this._optimum[(int)((UIntPtr)cur)].PosPrev2;
						this._optimum[(int)((UIntPtr)(posPrev - 1U))].BackPrev = this._optimum[(int)((UIntPtr)cur)].BackPrev2;
					}
				}
				uint num = posPrev;
				uint backPrev2 = backPrev;
				backPrev = this._optimum[(int)((UIntPtr)num)].BackPrev;
				posPrev = this._optimum[(int)((UIntPtr)num)].PosPrev;
				this._optimum[(int)((UIntPtr)num)].BackPrev = backPrev2;
				this._optimum[(int)((UIntPtr)num)].PosPrev = cur;
				cur = num;
			}
			while (cur > 0U);
			backRes = this._optimum[0].BackPrev;
			this._optimumCurrentIndex = this._optimum[0].PosPrev;
			return this._optimumCurrentIndex;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002BC8 File Offset: 0x00000DC8
		private uint GetOptimum(uint position, out uint backRes)
		{
			if (this._optimumEndIndex != this._optimumCurrentIndex)
			{
				uint result = this._optimum[(int)((UIntPtr)this._optimumCurrentIndex)].PosPrev - this._optimumCurrentIndex;
				backRes = this._optimum[(int)((UIntPtr)this._optimumCurrentIndex)].BackPrev;
				this._optimumCurrentIndex = this._optimum[(int)((UIntPtr)this._optimumCurrentIndex)].PosPrev;
				return result;
			}
			this._optimumCurrentIndex = (this._optimumEndIndex = 0U);
			uint longestMatchLength;
			uint num;
			if (!this._longestMatchWasFound)
			{
				this.ReadMatchDistances(out longestMatchLength, out num);
			}
			else
			{
				longestMatchLength = this._longestMatchLength;
				num = this._numDistancePairs;
				this._longestMatchWasFound = false;
			}
			uint num2 = this._matchFinder.GetNumAvailableBytes() + 1U;
			if (num2 < 2U)
			{
				backRes = uint.MaxValue;
				return 1U;
			}
			if (num2 > 273U)
			{
			}
			uint num3 = 0U;
			for (uint num4 = 0U; num4 < 4U; num4 += 1U)
			{
				this.reps[(int)((UIntPtr)num4)] = this._repDistances[(int)((UIntPtr)num4)];
				this.repLens[(int)((UIntPtr)num4)] = this._matchFinder.GetMatchLen(-1, this.reps[(int)((UIntPtr)num4)], 273U);
				if (this.repLens[(int)((UIntPtr)num4)] > this.repLens[(int)((UIntPtr)num3)])
				{
					num3 = num4;
				}
			}
			if (this.repLens[(int)((UIntPtr)num3)] >= this._numFastBytes)
			{
				backRes = num3;
				uint num5 = this.repLens[(int)((UIntPtr)num3)];
				this.MovePos(num5 - 1U);
				return num5;
			}
			if (longestMatchLength >= this._numFastBytes)
			{
				backRes = this._matchDistances[(int)((UIntPtr)(num - 1U))] + 4U;
				this.MovePos(longestMatchLength - 1U);
				return longestMatchLength;
			}
			byte indexByte = this._matchFinder.GetIndexByte(-1);
			byte indexByte2 = this._matchFinder.GetIndexByte((int)(0U - this._repDistances[0] - 1U - 1U));
			if (longestMatchLength < 2U && indexByte != indexByte2 && this.repLens[(int)((UIntPtr)num3)] < 2U)
			{
				backRes = uint.MaxValue;
				return 1U;
			}
			this._optimum[0].State = this._state;
			uint num6 = position & this._posStateMask;
			this._optimum[1].Price = this._isMatch[(int)((UIntPtr)((this._state.Index << 4) + num6))].GetPrice0() + this._literalEncoder.GetSubCoder(position, this._previousByte).GetPrice(!this._state.IsCharState(), indexByte2, indexByte);
			this._optimum[1].MakeAsChar();
			uint num7 = this._isMatch[(int)((UIntPtr)((this._state.Index << 4) + num6))].GetPrice1();
			uint num8 = num7 + this._isRep[(int)((UIntPtr)this._state.Index)].GetPrice1();
			if (indexByte2 == indexByte)
			{
				uint num9 = num8 + this.GetRepLen1Price(this._state, num6);
				if (num9 < this._optimum[1].Price)
				{
					this._optimum[1].Price = num9;
					this._optimum[1].MakeAsShortRep();
				}
			}
			uint num10 = (longestMatchLength >= this.repLens[(int)((UIntPtr)num3)]) ? longestMatchLength : this.repLens[(int)((UIntPtr)num3)];
			if (num10 < 2U)
			{
				backRes = this._optimum[1].BackPrev;
				return 1U;
			}
			this._optimum[1].PosPrev = 0U;
			this._optimum[0].Backs0 = this.reps[0];
			this._optimum[0].Backs1 = this.reps[1];
			this._optimum[0].Backs2 = this.reps[2];
			this._optimum[0].Backs3 = this.reps[3];
			uint num11 = num10;
			do
			{
				this._optimum[(int)((UIntPtr)(num11--))].Price = 268435455U;
			}
			while (num11 >= 2U);
			for (uint num4 = 0U; num4 < 4U; num4 += 1U)
			{
				uint num12 = this.repLens[(int)((UIntPtr)num4)];
				if (num12 >= 2U)
				{
					uint num13 = num8 + this.GetPureRepPrice(num4, this._state, num6);
					do
					{
						uint num14 = num13 + this._repMatchLenEncoder.GetPrice(num12 - 2U, num6);
						Encoder.Optimal optimal = this._optimum[(int)((UIntPtr)num12)];
						if (num14 < optimal.Price)
						{
							optimal.Price = num14;
							optimal.PosPrev = 0U;
							optimal.BackPrev = num4;
							optimal.Prev1IsChar = false;
						}
					}
					while ((num12 -= 1U) >= 2U);
				}
			}
			uint num15 = num7 + this._isRep[(int)((UIntPtr)this._state.Index)].GetPrice0();
			num11 = ((this.repLens[0] >= 2U) ? (this.repLens[0] + 1U) : 2U);
			if (num11 <= longestMatchLength)
			{
				uint num16 = 0U;
				while (num11 > this._matchDistances[(int)((UIntPtr)num16)])
				{
					num16 += 2U;
				}
				for (;;)
				{
					uint num17 = this._matchDistances[(int)((UIntPtr)(num16 + 1U))];
					uint num18 = num15 + this.GetPosLenPrice(num17, num11, num6);
					Encoder.Optimal optimal2 = this._optimum[(int)((UIntPtr)num11)];
					if (num18 < optimal2.Price)
					{
						optimal2.Price = num18;
						optimal2.PosPrev = 0U;
						optimal2.BackPrev = num17 + 4U;
						optimal2.Prev1IsChar = false;
					}
					if (num11 == this._matchDistances[(int)((UIntPtr)num16)])
					{
						num16 += 2U;
						if (num16 == num)
						{
							break;
						}
					}
					num11 += 1U;
				}
			}
			uint num19 = 0U;
			uint num20;
			for (;;)
			{
				num19 += 1U;
				if (num19 == num10)
				{
					break;
				}
				this.ReadMatchDistances(out num20, out num);
				if (num20 >= this._numFastBytes)
				{
					goto Block_24;
				}
				position += 1U;
				uint num21 = this._optimum[(int)((UIntPtr)num19)].PosPrev;
				Base.State state;
				if (this._optimum[(int)((UIntPtr)num19)].Prev1IsChar)
				{
					num21 -= 1U;
					if (this._optimum[(int)((UIntPtr)num19)].Prev2)
					{
						state = this._optimum[(int)((UIntPtr)this._optimum[(int)((UIntPtr)num19)].PosPrev2)].State;
						if (this._optimum[(int)((UIntPtr)num19)].BackPrev2 < 4U)
						{
							state.UpdateRep();
						}
						else
						{
							state.UpdateMatch();
						}
					}
					else
					{
						state = this._optimum[(int)((UIntPtr)num21)].State;
					}
					state.UpdateChar();
				}
				else
				{
					state = this._optimum[(int)((UIntPtr)num21)].State;
				}
				if (num21 == num19 - 1U)
				{
					if (this._optimum[(int)((UIntPtr)num19)].IsShortRep())
					{
						state.UpdateShortRep();
					}
					else
					{
						state.UpdateChar();
					}
				}
				else
				{
					uint num22;
					if (this._optimum[(int)((UIntPtr)num19)].Prev1IsChar && this._optimum[(int)((UIntPtr)num19)].Prev2)
					{
						num21 = this._optimum[(int)((UIntPtr)num19)].PosPrev2;
						num22 = this._optimum[(int)((UIntPtr)num19)].BackPrev2;
						state.UpdateRep();
					}
					else
					{
						num22 = this._optimum[(int)((UIntPtr)num19)].BackPrev;
						if (num22 < 4U)
						{
							state.UpdateRep();
						}
						else
						{
							state.UpdateMatch();
						}
					}
					Encoder.Optimal optimal3 = this._optimum[(int)((UIntPtr)num21)];
					if (num22 < 4U)
					{
						if (num22 == 0U)
						{
							this.reps[0] = optimal3.Backs0;
							this.reps[1] = optimal3.Backs1;
							this.reps[2] = optimal3.Backs2;
							this.reps[3] = optimal3.Backs3;
						}
						else if (num22 == 1U)
						{
							this.reps[0] = optimal3.Backs1;
							this.reps[1] = optimal3.Backs0;
							this.reps[2] = optimal3.Backs2;
							this.reps[3] = optimal3.Backs3;
						}
						else if (num22 == 2U)
						{
							this.reps[0] = optimal3.Backs2;
							this.reps[1] = optimal3.Backs0;
							this.reps[2] = optimal3.Backs1;
							this.reps[3] = optimal3.Backs3;
						}
						else
						{
							this.reps[0] = optimal3.Backs3;
							this.reps[1] = optimal3.Backs0;
							this.reps[2] = optimal3.Backs1;
							this.reps[3] = optimal3.Backs2;
						}
					}
					else
					{
						this.reps[0] = num22 - 4U;
						this.reps[1] = optimal3.Backs0;
						this.reps[2] = optimal3.Backs1;
						this.reps[3] = optimal3.Backs2;
					}
				}
				this._optimum[(int)((UIntPtr)num19)].State = state;
				this._optimum[(int)((UIntPtr)num19)].Backs0 = this.reps[0];
				this._optimum[(int)((UIntPtr)num19)].Backs1 = this.reps[1];
				this._optimum[(int)((UIntPtr)num19)].Backs2 = this.reps[2];
				this._optimum[(int)((UIntPtr)num19)].Backs3 = this.reps[3];
				uint price = this._optimum[(int)((UIntPtr)num19)].Price;
				indexByte = this._matchFinder.GetIndexByte(-1);
				indexByte2 = this._matchFinder.GetIndexByte((int)(0U - this.reps[0] - 1U - 1U));
				num6 = (position & this._posStateMask);
				uint num23 = price + this._isMatch[(int)((UIntPtr)((state.Index << 4) + num6))].GetPrice0() + this._literalEncoder.GetSubCoder(position, this._matchFinder.GetIndexByte(-2)).GetPrice(!state.IsCharState(), indexByte2, indexByte);
				Encoder.Optimal optimal4 = this._optimum[(int)((UIntPtr)(num19 + 1U))];
				bool flag = false;
				if (num23 < optimal4.Price)
				{
					optimal4.Price = num23;
					optimal4.PosPrev = num19;
					optimal4.MakeAsChar();
					flag = true;
				}
				num7 = price + this._isMatch[(int)((UIntPtr)((state.Index << 4) + num6))].GetPrice1();
				num8 = num7 + this._isRep[(int)((UIntPtr)state.Index)].GetPrice1();
				if (indexByte2 == indexByte && (optimal4.PosPrev >= num19 || optimal4.BackPrev != 0U))
				{
					uint num24 = num8 + this.GetRepLen1Price(state, num6);
					if (num24 <= optimal4.Price)
					{
						optimal4.Price = num24;
						optimal4.PosPrev = num19;
						optimal4.MakeAsShortRep();
						flag = true;
					}
				}
				uint num25 = this._matchFinder.GetNumAvailableBytes() + 1U;
				num25 = Math.Min(4095U - num19, num25);
				num2 = num25;
				if (num2 >= 2U)
				{
					if (num2 > this._numFastBytes)
					{
						num2 = this._numFastBytes;
					}
					if (!flag && indexByte2 != indexByte)
					{
						uint limit = Math.Min(num25 - 1U, this._numFastBytes);
						uint matchLen = this._matchFinder.GetMatchLen(0, this.reps[0], limit);
						if (matchLen >= 2U)
						{
							Base.State state2 = state;
							state2.UpdateChar();
							uint num26 = position + 1U & this._posStateMask;
							uint num27 = num23 + this._isMatch[(int)((UIntPtr)((state2.Index << 4) + num26))].GetPrice1() + this._isRep[(int)((UIntPtr)state2.Index)].GetPrice1();
							uint num28 = num19 + 1U + matchLen;
							while (num10 < num28)
							{
								this._optimum[(int)((UIntPtr)(num10 += 1U))].Price = 268435455U;
							}
							uint num29 = num27 + this.GetRepPrice(0U, matchLen, state2, num26);
							Encoder.Optimal optimal5 = this._optimum[(int)((UIntPtr)num28)];
							if (num29 < optimal5.Price)
							{
								optimal5.Price = num29;
								optimal5.PosPrev = num19 + 1U;
								optimal5.BackPrev = 0U;
								optimal5.Prev1IsChar = true;
								optimal5.Prev2 = false;
							}
						}
					}
					uint num30 = 2U;
					for (uint num31 = 0U; num31 < 4U; num31 += 1U)
					{
						uint num32 = this._matchFinder.GetMatchLen(-1, this.reps[(int)((UIntPtr)num31)], num2);
						if (num32 >= 2U)
						{
							uint num33 = num32;
							for (;;)
							{
								if (num10 >= num19 + num32)
								{
									uint num34 = num8 + this.GetRepPrice(num31, num32, state, num6);
									Encoder.Optimal optimal6 = this._optimum[(int)((UIntPtr)(num19 + num32))];
									if (num34 < optimal6.Price)
									{
										optimal6.Price = num34;
										optimal6.PosPrev = num19;
										optimal6.BackPrev = num31;
										optimal6.Prev1IsChar = false;
									}
									if ((num32 -= 1U) < 2U)
									{
										break;
									}
								}
								else
								{
									this._optimum[(int)((UIntPtr)(num10 += 1U))].Price = 268435455U;
								}
							}
							num32 = num33;
							if (num31 == 0U)
							{
								num30 = num32 + 1U;
							}
							if (num32 < num25)
							{
								uint limit2 = Math.Min(num25 - 1U - num32, this._numFastBytes);
								uint matchLen2 = this._matchFinder.GetMatchLen((int)num32, this.reps[(int)((UIntPtr)num31)], limit2);
								if (matchLen2 >= 2U)
								{
									Base.State state3 = state;
									state3.UpdateRep();
									uint num35 = position + num32 & this._posStateMask;
									uint num36 = num8 + this.GetRepPrice(num31, num32, state, num6) + this._isMatch[(int)((UIntPtr)((state3.Index << 4) + num35))].GetPrice0() + this._literalEncoder.GetSubCoder(position + num32, this._matchFinder.GetIndexByte((int)(num32 - 1U - 1U))).GetPrice(true, this._matchFinder.GetIndexByte((int)(num32 - 1U - (this.reps[(int)((UIntPtr)num31)] + 1U))), this._matchFinder.GetIndexByte((int)(num32 - 1U)));
									state3.UpdateChar();
									num35 = (position + num32 + 1U & this._posStateMask);
									uint num37 = num36 + this._isMatch[(int)((UIntPtr)((state3.Index << 4) + num35))].GetPrice1();
									uint num38 = num37 + this._isRep[(int)((UIntPtr)state3.Index)].GetPrice1();
									uint num39 = num32 + 1U + matchLen2;
									while (num10 < num19 + num39)
									{
										this._optimum[(int)((UIntPtr)(num10 += 1U))].Price = 268435455U;
									}
									uint num40 = num38 + this.GetRepPrice(0U, matchLen2, state3, num35);
									Encoder.Optimal optimal7 = this._optimum[(int)((UIntPtr)(num19 + num39))];
									if (num40 < optimal7.Price)
									{
										optimal7.Price = num40;
										optimal7.PosPrev = num19 + num32 + 1U;
										optimal7.BackPrev = 0U;
										optimal7.Prev1IsChar = true;
										optimal7.Prev2 = true;
										optimal7.PosPrev2 = num19;
										optimal7.BackPrev2 = num31;
									}
								}
							}
						}
					}
					if (num20 > num2)
					{
						num20 = num2;
						num = 0U;
						while (num20 > this._matchDistances[(int)((UIntPtr)num)])
						{
							num += 2U;
						}
						this._matchDistances[(int)((UIntPtr)num)] = num20;
						num += 2U;
					}
					if (num20 >= num30)
					{
						num15 = num7 + this._isRep[(int)((UIntPtr)state.Index)].GetPrice0();
						while (num10 < num19 + num20)
						{
							this._optimum[(int)((UIntPtr)(num10 += 1U))].Price = 268435455U;
						}
						uint num41 = 0U;
						while (num30 > this._matchDistances[(int)((UIntPtr)num41)])
						{
							num41 += 2U;
						}
						uint num42 = num30;
						for (;;)
						{
							uint num43 = this._matchDistances[(int)((UIntPtr)(num41 + 1U))];
							uint num44 = num15 + this.GetPosLenPrice(num43, num42, num6);
							Encoder.Optimal optimal8 = this._optimum[(int)((UIntPtr)(num19 + num42))];
							if (num44 < optimal8.Price)
							{
								optimal8.Price = num44;
								optimal8.PosPrev = num19;
								optimal8.BackPrev = num43 + 4U;
								optimal8.Prev1IsChar = false;
							}
							if (num42 == this._matchDistances[(int)((UIntPtr)num41)])
							{
								if (num42 < num25)
								{
									uint limit3 = Math.Min(num25 - 1U - num42, this._numFastBytes);
									uint matchLen3 = this._matchFinder.GetMatchLen((int)num42, num43, limit3);
									if (matchLen3 >= 2U)
									{
										Base.State state4 = state;
										state4.UpdateMatch();
										uint num45 = position + num42 & this._posStateMask;
										uint num46 = num44 + this._isMatch[(int)((UIntPtr)((state4.Index << 4) + num45))].GetPrice0() + this._literalEncoder.GetSubCoder(position + num42, this._matchFinder.GetIndexByte((int)(num42 - 1U - 1U))).GetPrice(true, this._matchFinder.GetIndexByte((int)(num42 - (num43 + 1U) - 1U)), this._matchFinder.GetIndexByte((int)(num42 - 1U)));
										state4.UpdateChar();
										num45 = (position + num42 + 1U & this._posStateMask);
										uint num47 = num46 + this._isMatch[(int)((UIntPtr)((state4.Index << 4) + num45))].GetPrice1();
										uint num48 = num47 + this._isRep[(int)((UIntPtr)state4.Index)].GetPrice1();
										uint num49 = num42 + 1U + matchLen3;
										while (num10 < num19 + num49)
										{
											this._optimum[(int)((UIntPtr)(num10 += 1U))].Price = 268435455U;
										}
										num44 = num48 + this.GetRepPrice(0U, matchLen3, state4, num45);
										optimal8 = this._optimum[(int)((UIntPtr)(num19 + num49))];
										if (num44 < optimal8.Price)
										{
											optimal8.Price = num44;
											optimal8.PosPrev = num19 + num42 + 1U;
											optimal8.BackPrev = 0U;
											optimal8.Prev1IsChar = true;
											optimal8.Prev2 = true;
											optimal8.PosPrev2 = num19;
											optimal8.BackPrev2 = num43 + 4U;
										}
									}
								}
								num41 += 2U;
								if (num41 == num)
								{
									break;
								}
							}
							num42 += 1U;
						}
					}
				}
			}
			return this.Backward(out backRes, num19);
			Block_24:
			this._numDistancePairs = num;
			this._longestMatchLength = num20;
			this._longestMatchWasFound = true;
			return this.Backward(out backRes, num19);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003C2C File Offset: 0x00001E2C
		private bool ChangePair(uint smallDist, uint bigDist)
		{
			return smallDist < 33554432U && bigDist >= smallDist << 7;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003C44 File Offset: 0x00001E44
		private void WriteEndMarker(uint posState)
		{
			if (!this._writeEndMark)
			{
				return;
			}
			this._isMatch[(int)((UIntPtr)((this._state.Index << 4) + posState))].Encode(this._rangeEncoder, 1U);
			this._isRep[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, 0U);
			this._state.UpdateMatch();
			uint num = 2U;
			this._lenEncoder.Encode(this._rangeEncoder, num - 2U, posState);
			uint symbol = 63U;
			uint lenToPosState = Base.GetLenToPosState(num);
			this._posSlotEncoder[(int)((UIntPtr)lenToPosState)].Encode(this._rangeEncoder, symbol);
			int num2 = 30;
			uint num3 = (1U << num2) - 1U;
			this._rangeEncoder.EncodeDirectBits(num3 >> 4, num2 - 4);
			this._posAlignEncoder.ReverseEncode(this._rangeEncoder, num3 & 15U);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003D1E File Offset: 0x00001F1E
		private void Flush(uint nowPos)
		{
			this.ReleaseMFStream();
			this.WriteEndMarker(nowPos & this._posStateMask);
			this._rangeEncoder.FlushData();
			this._rangeEncoder.FlushStream();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003D4C File Offset: 0x00001F4C
		public void CodeOneBlock(out long inSize, out long outSize, out bool finished)
		{
			inSize = 0L;
			outSize = 0L;
			finished = true;
			if (this._inStream != null)
			{
				this._matchFinder.SetStream(this._inStream);
				this._matchFinder.Init();
				this._needReleaseMFStream = true;
				this._inStream = null;
			}
			if (this._finished)
			{
				return;
			}
			this._finished = true;
			long num = this.nowPos64;
			if (this.nowPos64 == 0L)
			{
				if (this._matchFinder.GetNumAvailableBytes() == 0U)
				{
					this.Flush((uint)this.nowPos64);
					return;
				}
				uint num2;
				uint num3;
				this.ReadMatchDistances(out num2, out num3);
				uint num4 = (uint)this.nowPos64 & this._posStateMask;
				this._isMatch[(int)((UIntPtr)((this._state.Index << 4) + num4))].Encode(this._rangeEncoder, 0U);
				this._state.UpdateChar();
				byte indexByte = this._matchFinder.GetIndexByte((int)(0U - this._additionalOffset));
				this._literalEncoder.GetSubCoder((uint)this.nowPos64, this._previousByte).Encode(this._rangeEncoder, indexByte);
				this._previousByte = indexByte;
				this._additionalOffset -= 1U;
				this.nowPos64 += 1L;
			}
			if (this._matchFinder.GetNumAvailableBytes() == 0U)
			{
				this.Flush((uint)this.nowPos64);
				return;
			}
			for (;;)
			{
				uint num5;
				uint optimum = this.GetOptimum((uint)this.nowPos64, out num5);
				uint num6 = (uint)this.nowPos64 & this._posStateMask;
				uint num7 = (this._state.Index << 4) + num6;
				if (optimum == 1U && num5 == 4294967295U)
				{
					this._isMatch[(int)((UIntPtr)num7)].Encode(this._rangeEncoder, 0U);
					byte indexByte2 = this._matchFinder.GetIndexByte((int)(0U - this._additionalOffset));
					Encoder.LiteralEncoder.Encoder2 subCoder = this._literalEncoder.GetSubCoder((uint)this.nowPos64, this._previousByte);
					if (!this._state.IsCharState())
					{
						byte indexByte3 = this._matchFinder.GetIndexByte((int)(0U - this._repDistances[0] - 1U - this._additionalOffset));
						subCoder.EncodeMatched(this._rangeEncoder, indexByte3, indexByte2);
					}
					else
					{
						subCoder.Encode(this._rangeEncoder, indexByte2);
					}
					this._previousByte = indexByte2;
					this._state.UpdateChar();
				}
				else
				{
					this._isMatch[(int)((UIntPtr)num7)].Encode(this._rangeEncoder, 1U);
					if (num5 < 4U)
					{
						this._isRep[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, 1U);
						if (num5 == 0U)
						{
							this._isRepG0[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, 0U);
							if (optimum == 1U)
							{
								this._isRep0Long[(int)((UIntPtr)num7)].Encode(this._rangeEncoder, 0U);
							}
							else
							{
								this._isRep0Long[(int)((UIntPtr)num7)].Encode(this._rangeEncoder, 1U);
							}
						}
						else
						{
							this._isRepG0[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, 1U);
							if (num5 == 1U)
							{
								this._isRepG1[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, 0U);
							}
							else
							{
								this._isRepG1[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, 1U);
								this._isRepG2[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, num5 - 2U);
							}
						}
						if (optimum == 1U)
						{
							this._state.UpdateShortRep();
						}
						else
						{
							this._repMatchLenEncoder.Encode(this._rangeEncoder, optimum - 2U, num6);
							this._state.UpdateRep();
						}
						uint num8 = this._repDistances[(int)((UIntPtr)num5)];
						if (num5 != 0U)
						{
							for (uint num9 = num5; num9 >= 1U; num9 -= 1U)
							{
								this._repDistances[(int)((UIntPtr)num9)] = this._repDistances[(int)((UIntPtr)(num9 - 1U))];
							}
							this._repDistances[0] = num8;
						}
					}
					else
					{
						this._isRep[(int)((UIntPtr)this._state.Index)].Encode(this._rangeEncoder, 0U);
						this._state.UpdateMatch();
						this._lenEncoder.Encode(this._rangeEncoder, optimum - 2U, num6);
						num5 -= 4U;
						uint posSlot = Encoder.GetPosSlot(num5);
						uint lenToPosState = Base.GetLenToPosState(optimum);
						this._posSlotEncoder[(int)((UIntPtr)lenToPosState)].Encode(this._rangeEncoder, posSlot);
						if (posSlot >= 4U)
						{
							int num10 = (int)((posSlot >> 1) - 1U);
							uint num11 = (2U | (posSlot & 1U)) << num10;
							uint num12 = num5 - num11;
							if (posSlot < 14U)
							{
								BitTreeEncoder.ReverseEncode(this._posEncoders, num11 - posSlot - 1U, this._rangeEncoder, num10, num12);
							}
							else
							{
								this._rangeEncoder.EncodeDirectBits(num12 >> 4, num10 - 4);
								this._posAlignEncoder.ReverseEncode(this._rangeEncoder, num12 & 15U);
								this._alignPriceCount += 1U;
							}
						}
						uint num13 = num5;
						for (uint num14 = 3U; num14 >= 1U; num14 -= 1U)
						{
							this._repDistances[(int)((UIntPtr)num14)] = this._repDistances[(int)((UIntPtr)(num14 - 1U))];
						}
						this._repDistances[0] = num13;
						this._matchPriceCount += 1U;
					}
					this._previousByte = this._matchFinder.GetIndexByte((int)(optimum - 1U - this._additionalOffset));
				}
				this._additionalOffset -= optimum;
				this.nowPos64 += (long)((ulong)optimum);
				if (this._additionalOffset == 0U)
				{
					if (this._matchPriceCount >= 128U)
					{
						this.FillDistancesPrices();
					}
					if (this._alignPriceCount >= 16U)
					{
						this.FillAlignPrices();
					}
					inSize = this.nowPos64;
					outSize = this._rangeEncoder.GetProcessedSizeAdd();
					if (this._matchFinder.GetNumAvailableBytes() == 0U)
					{
						break;
					}
					if (this.nowPos64 - num >= 4096L)
					{
						goto Block_23;
					}
				}
			}
			this.Flush((uint)this.nowPos64);
			return;
			Block_23:
			this._finished = false;
			finished = false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00004340 File Offset: 0x00002540
		private void ReleaseMFStream()
		{
			if (this._matchFinder != null && this._needReleaseMFStream)
			{
				this._matchFinder.ReleaseStream();
				this._needReleaseMFStream = false;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004364 File Offset: 0x00002564
		private void SetOutStream(Stream outStream)
		{
			this._rangeEncoder.SetStream(outStream);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004372 File Offset: 0x00002572
		private void ReleaseOutStream()
		{
			this._rangeEncoder.ReleaseStream();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000437F File Offset: 0x0000257F
		private void ReleaseStreams()
		{
			this.ReleaseMFStream();
			this.ReleaseOutStream();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004390 File Offset: 0x00002590
		private void SetStreams(Stream inStream, Stream outStream, long inSize, long outSize)
		{
			this._inStream = inStream;
			this._finished = false;
			this.Create();
			this.SetOutStream(outStream);
			this.Init();
			this.FillDistancesPrices();
			this.FillAlignPrices();
			this._lenEncoder.SetTableSize(this._numFastBytes + 1U - 2U);
			this._lenEncoder.UpdateTables(1U << this._posStateBits);
			this._repMatchLenEncoder.SetTableSize(this._numFastBytes + 1U - 2U);
			this._repMatchLenEncoder.UpdateTables(1U << this._posStateBits);
			this.nowPos64 = 0L;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004428 File Offset: 0x00002628
		public void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress)
		{
			this._needReleaseMFStream = false;
			try
			{
				this.SetStreams(inStream, outStream, inSize, outSize);
				for (;;)
				{
					long inSize2;
					long outSize2;
					bool flag;
					this.CodeOneBlock(out inSize2, out outSize2, out flag);
					if (flag)
					{
						break;
					}
					if (progress != null)
					{
						progress.SetProgress(inSize2, outSize2);
					}
				}
			}
			finally
			{
				this.ReleaseStreams();
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004480 File Offset: 0x00002680
		public void WriteCoderProperties(Stream outStream)
		{
			this.properties[0] = (byte)((this._posStateBits * 5 + this._numLiteralPosStateBits) * 9 + this._numLiteralContextBits);
			for (int i = 0; i < 4; i++)
			{
				this.properties[1 + i] = (byte)(this._dictionarySize >> 8 * i);
			}
			outStream.Write(this.properties, 0, 5);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000044E4 File Offset: 0x000026E4
		private void FillDistancesPrices()
		{
			for (uint num = 4U; num < 128U; num += 1U)
			{
				uint posSlot = Encoder.GetPosSlot(num);
				int num2 = (int)((posSlot >> 1) - 1U);
				uint num3 = (2U | (posSlot & 1U)) << num2;
				this.tempPrices[(int)((UIntPtr)num)] = BitTreeEncoder.ReverseGetPrice(this._posEncoders, num3 - posSlot - 1U, num2, num - num3);
			}
			for (uint num4 = 0U; num4 < 4U; num4 += 1U)
			{
				BitTreeEncoder bitTreeEncoder = this._posSlotEncoder[(int)((UIntPtr)num4)];
				uint num5 = num4 << 6;
				for (uint num6 = 0U; num6 < this._distTableSize; num6 += 1U)
				{
					this._posSlotPrices[(int)((UIntPtr)(num5 + num6))] = bitTreeEncoder.GetPrice(num6);
				}
				for (uint num6 = 14U; num6 < this._distTableSize; num6 += 1U)
				{
					this._posSlotPrices[(int)((UIntPtr)(num5 + num6))] += (num6 >> 1) - 1U - 4U << 6;
				}
				uint num7 = num4 * 128U;
				uint num8;
				for (num8 = 0U; num8 < 4U; num8 += 1U)
				{
					this._distancesPrices[(int)((UIntPtr)(num7 + num8))] = this._posSlotPrices[(int)((UIntPtr)(num5 + num8))];
				}
				while (num8 < 128U)
				{
					this._distancesPrices[(int)((UIntPtr)(num7 + num8))] = this._posSlotPrices[(int)((UIntPtr)(num5 + Encoder.GetPosSlot(num8)))] + this.tempPrices[(int)((UIntPtr)num8)];
					num8 += 1U;
				}
			}
			this._matchPriceCount = 0U;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004648 File Offset: 0x00002848
		private void FillAlignPrices()
		{
			for (uint num = 0U; num < 16U; num += 1U)
			{
				this._alignPrices[(int)((UIntPtr)num)] = this._posAlignEncoder.ReverseGetPrice(num);
			}
			this._alignPriceCount = 0U;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004680 File Offset: 0x00002880
		private static int FindMatchFinder(string s)
		{
			for (int i = 0; i < Encoder.kMatchFinderIDs.Length; i++)
			{
				if (s == Encoder.kMatchFinderIDs[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000046B4 File Offset: 0x000028B4
		public void SetCoderProperties(CoderPropID[] propIDs, object[] properties)
		{
			uint num = 0U;
			while ((ulong)num < (ulong)((long)properties.Length))
			{
				object obj = properties[(int)((UIntPtr)num)];
				CoderPropID coderPropID = propIDs[(int)((UIntPtr)num)];
				if (coderPropID <= CoderPropID.LitPosBits)
				{
					if (coderPropID != CoderPropID.DictionarySize)
					{
						switch (coderPropID)
						{
						case CoderPropID.PosStateBits:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num2 = (int)obj;
							if (num2 < 0 || (long)num2 > 4L)
							{
								throw new InvalidParamException();
							}
							this._posStateBits = num2;
							this._posStateMask = (1U << this._posStateBits) - 1U;
							break;
						}
						case CoderPropID.LitContextBits:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num3 = (int)obj;
							if (num3 < 0 || (long)num3 > 8L)
							{
								throw new InvalidParamException();
							}
							this._numLiteralContextBits = num3;
							break;
						}
						case CoderPropID.LitPosBits:
						{
							if (!(obj is int))
							{
								throw new InvalidParamException();
							}
							int num4 = (int)obj;
							if (num4 < 0 || (long)num4 > 4L)
							{
								throw new InvalidParamException();
							}
							this._numLiteralPosStateBits = num4;
							break;
						}
						default:
							goto IL_23D;
						}
					}
					else
					{
						if (!(obj is int))
						{
							throw new InvalidParamException();
						}
						int num5 = (int)obj;
						if ((long)num5 < 1L || (long)num5 > 1073741824L)
						{
							throw new InvalidParamException();
						}
						this._dictionarySize = (uint)num5;
						int num6 = 0;
						while ((long)num6 < 30L && (long)num5 > (long)(1UL << (num6 & 31)))
						{
							num6++;
						}
						this._distTableSize = (uint)(num6 * 2);
					}
				}
				else
				{
					switch (coderPropID)
					{
					case CoderPropID.NumFastBytes:
					{
						if (!(obj is int))
						{
							throw new InvalidParamException();
						}
						int num7 = (int)obj;
						if (num7 < 5 || (long)num7 > 273L)
						{
							throw new InvalidParamException();
						}
						this._numFastBytes = (uint)num7;
						break;
					}
					case CoderPropID.MatchFinder:
					{
						if (!(obj is string))
						{
							throw new InvalidParamException();
						}
						Encoder.EMatchFinderType matchFinderType = this._matchFinderType;
						int num8 = Encoder.FindMatchFinder(((string)obj).ToUpper());
						if (num8 < 0)
						{
							throw new InvalidParamException();
						}
						this._matchFinderType = (Encoder.EMatchFinderType)num8;
						if (this._matchFinder != null && matchFinderType != this._matchFinderType)
						{
							this._dictionarySizePrev = uint.MaxValue;
							this._matchFinder = null;
						}
						break;
					}
					default:
						if (coderPropID != CoderPropID.Algorithm)
						{
							if (coderPropID != CoderPropID.EndMarker)
							{
								goto IL_23D;
							}
							if (!(obj is bool))
							{
								throw new InvalidParamException();
							}
							this.SetWriteEndMarkerMode((bool)obj);
						}
						break;
					}
				}
				num += 1U;
				continue;
				IL_23D:
				throw new InvalidParamException();
			}
		}

		// Token: 0x04000005 RID: 5
		private const uint kIfinityPrice = 268435455U;

		// Token: 0x04000006 RID: 6
		private const int kDefaultDictionaryLogSize = 22;

		// Token: 0x04000007 RID: 7
		private const uint kNumFastBytesDefault = 32U;

		// Token: 0x04000008 RID: 8
		private const uint kNumLenSpecSymbols = 16U;

		// Token: 0x04000009 RID: 9
		private const uint kNumOpts = 4096U;

		// Token: 0x0400000A RID: 10
		private const int kPropSize = 5;

		// Token: 0x0400000B RID: 11
		private static byte[] g_FastPos = new byte[2048];

		// Token: 0x0400000C RID: 12
		private Base.State _state = default(Base.State);

		// Token: 0x0400000D RID: 13
		private byte _previousByte;

		// Token: 0x0400000E RID: 14
		private uint[] _repDistances = new uint[4];

		// Token: 0x0400000F RID: 15
		private Encoder.Optimal[] _optimum = new Encoder.Optimal[4096];

		// Token: 0x04000010 RID: 16
		private IMatchFinder _matchFinder;

		// Token: 0x04000011 RID: 17
		private Encoder _rangeEncoder = new Encoder();

		// Token: 0x04000012 RID: 18
		private BitEncoder[] _isMatch = new BitEncoder[192];

		// Token: 0x04000013 RID: 19
		private BitEncoder[] _isRep = new BitEncoder[12];

		// Token: 0x04000014 RID: 20
		private BitEncoder[] _isRepG0 = new BitEncoder[12];

		// Token: 0x04000015 RID: 21
		private BitEncoder[] _isRepG1 = new BitEncoder[12];

		// Token: 0x04000016 RID: 22
		private BitEncoder[] _isRepG2 = new BitEncoder[12];

		// Token: 0x04000017 RID: 23
		private BitEncoder[] _isRep0Long = new BitEncoder[192];

		// Token: 0x04000018 RID: 24
		private BitTreeEncoder[] _posSlotEncoder = new BitTreeEncoder[4];

		// Token: 0x04000019 RID: 25
		private BitEncoder[] _posEncoders = new BitEncoder[114];

		// Token: 0x0400001A RID: 26
		private BitTreeEncoder _posAlignEncoder = new BitTreeEncoder(4);

		// Token: 0x0400001B RID: 27
		private Encoder.LenPriceTableEncoder _lenEncoder = new Encoder.LenPriceTableEncoder();

		// Token: 0x0400001C RID: 28
		private Encoder.LenPriceTableEncoder _repMatchLenEncoder = new Encoder.LenPriceTableEncoder();

		// Token: 0x0400001D RID: 29
		private Encoder.LiteralEncoder _literalEncoder = new Encoder.LiteralEncoder();

		// Token: 0x0400001E RID: 30
		private uint[] _matchDistances = new uint[548];

		// Token: 0x0400001F RID: 31
		private uint _numFastBytes = 32U;

		// Token: 0x04000020 RID: 32
		private uint _longestMatchLength;

		// Token: 0x04000021 RID: 33
		private uint _numDistancePairs;

		// Token: 0x04000022 RID: 34
		private uint _additionalOffset;

		// Token: 0x04000023 RID: 35
		private uint _optimumEndIndex;

		// Token: 0x04000024 RID: 36
		private uint _optimumCurrentIndex;

		// Token: 0x04000025 RID: 37
		private bool _longestMatchWasFound;

		// Token: 0x04000026 RID: 38
		private uint[] _posSlotPrices = new uint[256];

		// Token: 0x04000027 RID: 39
		private uint[] _distancesPrices = new uint[512];

		// Token: 0x04000028 RID: 40
		private uint[] _alignPrices = new uint[16];

		// Token: 0x04000029 RID: 41
		private uint _alignPriceCount;

		// Token: 0x0400002A RID: 42
		private uint _distTableSize = 44U;

		// Token: 0x0400002B RID: 43
		private int _posStateBits = 2;

		// Token: 0x0400002C RID: 44
		private uint _posStateMask = 3U;

		// Token: 0x0400002D RID: 45
		private int _numLiteralPosStateBits;

		// Token: 0x0400002E RID: 46
		private int _numLiteralContextBits = 3;

		// Token: 0x0400002F RID: 47
		private uint _dictionarySize = 4194304U;

		// Token: 0x04000030 RID: 48
		private uint _dictionarySizePrev = uint.MaxValue;

		// Token: 0x04000031 RID: 49
		private uint _numFastBytesPrev = uint.MaxValue;

		// Token: 0x04000032 RID: 50
		private long nowPos64;

		// Token: 0x04000033 RID: 51
		private bool _finished;

		// Token: 0x04000034 RID: 52
		private Stream _inStream;

		// Token: 0x04000035 RID: 53
		private Encoder.EMatchFinderType _matchFinderType = Encoder.EMatchFinderType.BT4;

		// Token: 0x04000036 RID: 54
		private bool _writeEndMark;

		// Token: 0x04000037 RID: 55
		private bool _needReleaseMFStream;

		// Token: 0x04000038 RID: 56
		private uint[] reps = new uint[4];

		// Token: 0x04000039 RID: 57
		private uint[] repLens = new uint[4];

		// Token: 0x0400003A RID: 58
		private byte[] properties = new byte[5];

		// Token: 0x0400003B RID: 59
		private uint[] tempPrices = new uint[128];

		// Token: 0x0400003C RID: 60
		private uint _matchPriceCount;

		// Token: 0x0400003D RID: 61
		private static string[] kMatchFinderIDs = new string[]
		{
			"BT2",
			"BT4"
		};

		// Token: 0x02000008 RID: 8
		private enum EMatchFinderType
		{
			// Token: 0x0400003F RID: 63
			BT2,
			// Token: 0x04000040 RID: 64
			BT4
		}

		// Token: 0x02000009 RID: 9
		private class LiteralEncoder
		{
			// Token: 0x06000030 RID: 48 RVA: 0x00004914 File Offset: 0x00002B14
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
				this.m_Coders = new Encoder.LiteralEncoder.Encoder2[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)((UIntPtr)num2)].Create();
				}
			}

			// Token: 0x06000031 RID: 49 RVA: 0x00004998 File Offset: 0x00002B98
			public void Init()
			{
				uint num = 1U << this.m_NumPrevBits + this.m_NumPosBits;
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					this.m_Coders[(int)((UIntPtr)num2)].Init();
				}
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000049D6 File Offset: 0x00002BD6
			public Encoder.LiteralEncoder.Encoder2 GetSubCoder(uint pos, byte prevByte)
			{
				return this.m_Coders[(int)((UIntPtr)(((pos & this.m_PosMask) << this.m_NumPrevBits) + (uint)(prevByte >> 8 - this.m_NumPrevBits)))];
			}

			// Token: 0x04000041 RID: 65
			private Encoder.LiteralEncoder.Encoder2[] m_Coders;

			// Token: 0x04000042 RID: 66
			private int m_NumPrevBits;

			// Token: 0x04000043 RID: 67
			private int m_NumPosBits;

			// Token: 0x04000044 RID: 68
			private uint m_PosMask;

			// Token: 0x0200000A RID: 10
			public struct Encoder2
			{
				// Token: 0x06000034 RID: 52 RVA: 0x00004A11 File Offset: 0x00002C11
				public void Create()
				{
					this.m_Encoders = new BitEncoder[768];
				}

				// Token: 0x06000035 RID: 53 RVA: 0x00004A24 File Offset: 0x00002C24
				public void Init()
				{
					for (int i = 0; i < 768; i++)
					{
						this.m_Encoders[i].Init();
					}
				}

				// Token: 0x06000036 RID: 54 RVA: 0x00004A54 File Offset: 0x00002C54
				public void Encode(Encoder rangeEncoder, byte symbol)
				{
					uint num = 1U;
					for (int i = 7; i >= 0; i--)
					{
						uint num2 = (uint)(symbol >> i & 1);
						this.m_Encoders[(int)((UIntPtr)num)].Encode(rangeEncoder, num2);
						num = (num << 1 | num2);
					}
				}

				// Token: 0x06000037 RID: 55 RVA: 0x00004A94 File Offset: 0x00002C94
				public void EncodeMatched(Encoder rangeEncoder, byte matchByte, byte symbol)
				{
					uint num = 1U;
					bool flag = true;
					for (int i = 7; i >= 0; i--)
					{
						uint num2 = (uint)(symbol >> i & 1);
						uint num3 = num;
						if (flag)
						{
							uint num4 = (uint)(matchByte >> i & 1);
							num3 += 1U + num4 << 8;
							flag = (num4 == num2);
						}
						this.m_Encoders[(int)((UIntPtr)num3)].Encode(rangeEncoder, num2);
						num = (num << 1 | num2);
					}
				}

				// Token: 0x06000038 RID: 56 RVA: 0x00004AF8 File Offset: 0x00002CF8
				public uint GetPrice(bool matchMode, byte matchByte, byte symbol)
				{
					uint num = 0U;
					uint num2 = 1U;
					int i = 7;
					if (matchMode)
					{
						while (i >= 0)
						{
							uint num3 = (uint)(matchByte >> i & 1);
							uint num4 = (uint)(symbol >> i & 1);
							num += this.m_Encoders[(int)((UIntPtr)((1U + num3 << 8) + num2))].GetPrice(num4);
							num2 = (num2 << 1 | num4);
							if (num3 != num4)
							{
								i--;
								break;
							}
							i--;
						}
					}
					while (i >= 0)
					{
						uint num5 = (uint)(symbol >> i & 1);
						num += this.m_Encoders[(int)((UIntPtr)num2)].GetPrice(num5);
						num2 = (num2 << 1 | num5);
						i--;
					}
					return num;
				}

				// Token: 0x04000045 RID: 69
				private BitEncoder[] m_Encoders;
			}
		}

		// Token: 0x0200000B RID: 11
		private class LenEncoder
		{
			// Token: 0x06000039 RID: 57 RVA: 0x00004B90 File Offset: 0x00002D90
			public LenEncoder()
			{
				for (uint num = 0U; num < 16U; num += 1U)
				{
					this._lowCoder[(int)((UIntPtr)num)] = new BitTreeEncoder(3);
					this._midCoder[(int)((UIntPtr)num)] = new BitTreeEncoder(3);
				}
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00004C20 File Offset: 0x00002E20
			public void Init(uint numPosStates)
			{
				this._choice.Init();
				this._choice2.Init();
				for (uint num = 0U; num < numPosStates; num += 1U)
				{
					this._lowCoder[(int)((UIntPtr)num)].Init();
					this._midCoder[(int)((UIntPtr)num)].Init();
				}
				this._highCoder.Init();
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00004C80 File Offset: 0x00002E80
			public void Encode(Encoder rangeEncoder, uint symbol, uint posState)
			{
				if (symbol < 8U)
				{
					this._choice.Encode(rangeEncoder, 0U);
					this._lowCoder[(int)((UIntPtr)posState)].Encode(rangeEncoder, symbol);
					return;
				}
				symbol -= 8U;
				this._choice.Encode(rangeEncoder, 1U);
				if (symbol < 8U)
				{
					this._choice2.Encode(rangeEncoder, 0U);
					this._midCoder[(int)((UIntPtr)posState)].Encode(rangeEncoder, symbol);
					return;
				}
				this._choice2.Encode(rangeEncoder, 1U);
				this._highCoder.Encode(rangeEncoder, symbol - 8U);
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00004D08 File Offset: 0x00002F08
			public void SetPrices(uint posState, uint numSymbols, uint[] prices, uint st)
			{
				uint price = this._choice.GetPrice0();
				uint price2 = this._choice.GetPrice1();
				uint num = price2 + this._choice2.GetPrice0();
				uint num2 = price2 + this._choice2.GetPrice1();
				uint num3;
				for (num3 = 0U; num3 < 8U; num3 += 1U)
				{
					if (num3 >= numSymbols)
					{
						return;
					}
					prices[(int)((UIntPtr)(st + num3))] = price + this._lowCoder[(int)((UIntPtr)posState)].GetPrice(num3);
				}
				while (num3 < 16U)
				{
					if (num3 >= numSymbols)
					{
						return;
					}
					prices[(int)((UIntPtr)(st + num3))] = num + this._midCoder[(int)((UIntPtr)posState)].GetPrice(num3 - 8U);
					num3 += 1U;
				}
				while (num3 < numSymbols)
				{
					prices[(int)((UIntPtr)(st + num3))] = num2 + this._highCoder.GetPrice(num3 - 8U - 8U);
					num3 += 1U;
				}
			}

			// Token: 0x04000046 RID: 70
			private BitEncoder _choice = default(BitEncoder);

			// Token: 0x04000047 RID: 71
			private BitEncoder _choice2 = default(BitEncoder);

			// Token: 0x04000048 RID: 72
			private BitTreeEncoder[] _lowCoder = new BitTreeEncoder[16];

			// Token: 0x04000049 RID: 73
			private BitTreeEncoder[] _midCoder = new BitTreeEncoder[16];

			// Token: 0x0400004A RID: 74
			private BitTreeEncoder _highCoder = new BitTreeEncoder(8);
		}

		// Token: 0x0200000C RID: 12
		private class LenPriceTableEncoder : Encoder.LenEncoder
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00004DDC File Offset: 0x00002FDC
			public void SetTableSize(uint tableSize)
			{
				this._tableSize = tableSize;
			}

			// Token: 0x0600003E RID: 62 RVA: 0x00004DE5 File Offset: 0x00002FE5
			public uint GetPrice(uint symbol, uint posState)
			{
				return this._prices[(int)((UIntPtr)(posState * 272U + symbol))];
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00004DF8 File Offset: 0x00002FF8
			private void UpdateTable(uint posState)
			{
				base.SetPrices(posState, this._tableSize, this._prices, posState * 272U);
				this._counters[(int)((UIntPtr)posState)] = this._tableSize;
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00004E24 File Offset: 0x00003024
			public void UpdateTables(uint numPosStates)
			{
				for (uint num = 0U; num < numPosStates; num += 1U)
				{
					this.UpdateTable(num);
				}
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00004E44 File Offset: 0x00003044
			public new void Encode(Encoder rangeEncoder, uint symbol, uint posState)
			{
				base.Encode(rangeEncoder, symbol, posState);
				if ((this._counters[(int)((UIntPtr)posState)] -= 1U) == 0U)
				{
					this.UpdateTable(posState);
				}
			}

			// Token: 0x0400004B RID: 75
			private uint[] _prices = new uint[4352];

			// Token: 0x0400004C RID: 76
			private uint _tableSize;

			// Token: 0x0400004D RID: 77
			private uint[] _counters = new uint[16];
		}

		// Token: 0x0200000D RID: 13
		private class Optimal
		{
			// Token: 0x06000043 RID: 67 RVA: 0x00004EA7 File Offset: 0x000030A7
			public void MakeAsChar()
			{
				this.BackPrev = uint.MaxValue;
				this.Prev1IsChar = false;
			}

			// Token: 0x06000044 RID: 68 RVA: 0x00004EB7 File Offset: 0x000030B7
			public void MakeAsShortRep()
			{
				this.BackPrev = 0U;
				this.Prev1IsChar = false;
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00004EC7 File Offset: 0x000030C7
			public bool IsShortRep()
			{
				return this.BackPrev == 0U;
			}

			// Token: 0x0400004E RID: 78
			public Base.State State;

			// Token: 0x0400004F RID: 79
			public bool Prev1IsChar;

			// Token: 0x04000050 RID: 80
			public bool Prev2;

			// Token: 0x04000051 RID: 81
			public uint PosPrev2;

			// Token: 0x04000052 RID: 82
			public uint BackPrev2;

			// Token: 0x04000053 RID: 83
			public uint Price;

			// Token: 0x04000054 RID: 84
			public uint PosPrev;

			// Token: 0x04000055 RID: 85
			public uint BackPrev;

			// Token: 0x04000056 RID: 86
			public uint Backs0;

			// Token: 0x04000057 RID: 87
			public uint Backs1;

			// Token: 0x04000058 RID: 88
			public uint Backs2;

			// Token: 0x04000059 RID: 89
			public uint Backs3;
		}
	}
}
