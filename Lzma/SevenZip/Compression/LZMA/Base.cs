using System;

namespace SevenZip.Compression.LZMA
{
	// Token: 0x02000018 RID: 24
	internal abstract class Base
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00005D49 File Offset: 0x00003F49
		public static uint GetLenToPosState(uint len)
		{
			len -= 2U;
			if (len < 4U)
			{
				return len;
			}
			return 3U;
		}

		// Token: 0x0400008A RID: 138
		public const uint kNumRepDistances = 4U;

		// Token: 0x0400008B RID: 139
		public const uint kNumStates = 12U;

		// Token: 0x0400008C RID: 140
		public const int kNumPosSlotBits = 6;

		// Token: 0x0400008D RID: 141
		public const int kDicLogSizeMin = 0;

		// Token: 0x0400008E RID: 142
		public const int kNumLenToPosStatesBits = 2;

		// Token: 0x0400008F RID: 143
		public const uint kNumLenToPosStates = 4U;

		// Token: 0x04000090 RID: 144
		public const uint kMatchMinLen = 2U;

		// Token: 0x04000091 RID: 145
		public const int kNumAlignBits = 4;

		// Token: 0x04000092 RID: 146
		public const uint kAlignTableSize = 16U;

		// Token: 0x04000093 RID: 147
		public const uint kAlignMask = 15U;

		// Token: 0x04000094 RID: 148
		public const uint kStartPosModelIndex = 4U;

		// Token: 0x04000095 RID: 149
		public const uint kEndPosModelIndex = 14U;

		// Token: 0x04000096 RID: 150
		public const uint kNumPosModels = 10U;

		// Token: 0x04000097 RID: 151
		public const uint kNumFullDistances = 128U;

		// Token: 0x04000098 RID: 152
		public const uint kNumLitPosStatesBitsEncodingMax = 4U;

		// Token: 0x04000099 RID: 153
		public const uint kNumLitContextBitsMax = 8U;

		// Token: 0x0400009A RID: 154
		public const int kNumPosStatesBitsMax = 4;

		// Token: 0x0400009B RID: 155
		public const uint kNumPosStatesMax = 16U;

		// Token: 0x0400009C RID: 156
		public const int kNumPosStatesBitsEncodingMax = 4;

		// Token: 0x0400009D RID: 157
		public const uint kNumPosStatesEncodingMax = 16U;

		// Token: 0x0400009E RID: 158
		public const int kNumLowLenBits = 3;

		// Token: 0x0400009F RID: 159
		public const int kNumMidLenBits = 3;

		// Token: 0x040000A0 RID: 160
		public const int kNumHighLenBits = 8;

		// Token: 0x040000A1 RID: 161
		public const uint kNumLowLenSymbols = 8U;

		// Token: 0x040000A2 RID: 162
		public const uint kNumMidLenSymbols = 8U;

		// Token: 0x040000A3 RID: 163
		public const uint kNumLenSymbols = 272U;

		// Token: 0x040000A4 RID: 164
		public const uint kMatchMaxLen = 273U;

		// Token: 0x02000019 RID: 25
		public struct State
		{
			// Token: 0x0600007B RID: 123 RVA: 0x00005D5F File Offset: 0x00003F5F
			public void Init()
			{
				this.Index = 0U;
			}

			// Token: 0x0600007C RID: 124 RVA: 0x00005D68 File Offset: 0x00003F68
			public void UpdateChar()
			{
				if (this.Index < 4U)
				{
					this.Index = 0U;
					return;
				}
				if (this.Index < 10U)
				{
					this.Index -= 3U;
					return;
				}
				this.Index -= 6U;
			}

			// Token: 0x0600007D RID: 125 RVA: 0x00005DA2 File Offset: 0x00003FA2
			public void UpdateMatch()
			{
				this.Index = ((this.Index < 7U) ? 7U : 10U);
			}

			// Token: 0x0600007E RID: 126 RVA: 0x00005DB8 File Offset: 0x00003FB8
			public void UpdateRep()
			{
				this.Index = ((this.Index < 7U) ? 8U : 11U);
			}

			// Token: 0x0600007F RID: 127 RVA: 0x00005DCE File Offset: 0x00003FCE
			public void UpdateShortRep()
			{
				this.Index = ((this.Index < 7U) ? 9U : 11U);
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00005DE5 File Offset: 0x00003FE5
			public bool IsCharState()
			{
				return this.Index < 7U;
			}

			// Token: 0x040000A5 RID: 165
			public uint Index;
		}
	}
}
