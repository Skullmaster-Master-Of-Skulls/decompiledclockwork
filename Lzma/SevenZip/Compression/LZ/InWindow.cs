using System;
using System.IO;

namespace SevenZip.Compression.LZ
{
	// Token: 0x0200000E RID: 14
	public class InWindow
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00004EDC File Offset: 0x000030DC
		public void MoveBlock()
		{
			uint num = this._bufferOffset + this._pos - this._keepSizeBefore;
			if (num > 0U)
			{
				num -= 1U;
			}
			uint num2 = this._bufferOffset + this._streamPos - num;
			for (uint num3 = 0U; num3 < num2; num3 += 1U)
			{
				this._bufferBase[(int)((UIntPtr)num3)] = this._bufferBase[(int)((UIntPtr)(num + num3))];
			}
			this._bufferOffset -= num;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004F44 File Offset: 0x00003144
		public virtual void ReadBlock()
		{
			if (this._streamEndWasReached)
			{
				return;
			}
			for (;;)
			{
				int num = (int)(0U - this._bufferOffset + this._blockSize - this._streamPos);
				if (num == 0)
				{
					break;
				}
				int num2 = this._stream.Read(this._bufferBase, (int)(this._bufferOffset + this._streamPos), num);
				if (num2 == 0)
				{
					goto Block_3;
				}
				this._streamPos += (uint)num2;
				if (this._streamPos >= this._pos + this._keepSizeAfter)
				{
					this._posLimit = this._streamPos - this._keepSizeAfter;
				}
			}
			return;
			Block_3:
			this._posLimit = this._streamPos;
			uint num3 = this._bufferOffset + this._posLimit;
			if (num3 > this._pointerToLastSafePosition)
			{
				this._posLimit = this._pointerToLastSafePosition - this._bufferOffset;
			}
			this._streamEndWasReached = true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005013 File Offset: 0x00003213
		private void Free()
		{
			this._bufferBase = null;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000501C File Offset: 0x0000321C
		public void Create(uint keepSizeBefore, uint keepSizeAfter, uint keepSizeReserv)
		{
			this._keepSizeBefore = keepSizeBefore;
			this._keepSizeAfter = keepSizeAfter;
			uint num = keepSizeBefore + keepSizeAfter + keepSizeReserv;
			if (this._bufferBase == null || this._blockSize != num)
			{
				this.Free();
				this._blockSize = num;
				this._bufferBase = new byte[this._blockSize];
			}
			this._pointerToLastSafePosition = this._blockSize - keepSizeAfter;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000507B File Offset: 0x0000327B
		public void SetStream(Stream stream)
		{
			this._stream = stream;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00005084 File Offset: 0x00003284
		public void ReleaseStream()
		{
			this._stream = null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000508D File Offset: 0x0000328D
		public void Init()
		{
			this._bufferOffset = 0U;
			this._pos = 0U;
			this._streamPos = 0U;
			this._streamEndWasReached = false;
			this.ReadBlock();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000050B4 File Offset: 0x000032B4
		public void MovePos()
		{
			this._pos += 1U;
			if (this._pos > this._posLimit)
			{
				uint num = this._bufferOffset + this._pos;
				if (num > this._pointerToLastSafePosition)
				{
					this.MoveBlock();
				}
				this.ReadBlock();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005100 File Offset: 0x00003300
		public byte GetIndexByte(int index)
		{
			return this._bufferBase[(int)(checked((IntPtr)(unchecked((ulong)(this._bufferOffset + this._pos) + (ulong)((long)index)))))];
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000511C File Offset: 0x0000331C
		public uint GetMatchLen(int index, uint distance, uint limit)
		{
			if (this._streamEndWasReached && (ulong)this._pos + (ulong)((long)index) + (ulong)limit > (ulong)this._streamPos)
			{
				limit = this._streamPos - (uint)((ulong)this._pos + (ulong)((long)index));
			}
			distance += 1U;
			uint num = this._bufferOffset + this._pos + (uint)index;
			uint num2 = 0U;
			while (num2 < limit && this._bufferBase[(int)((UIntPtr)(num + num2))] == this._bufferBase[(int)((UIntPtr)(num + num2 - distance))])
			{
				num2 += 1U;
			}
			return num2;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005197 File Offset: 0x00003397
		public uint GetNumAvailableBytes()
		{
			return this._streamPos - this._pos;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000051A6 File Offset: 0x000033A6
		public void ReduceOffsets(int subValue)
		{
			this._bufferOffset += (uint)subValue;
			this._posLimit -= (uint)subValue;
			this._pos -= (uint)subValue;
			this._streamPos -= (uint)subValue;
		}

		// Token: 0x0400005A RID: 90
		public byte[] _bufferBase;

		// Token: 0x0400005B RID: 91
		private Stream _stream;

		// Token: 0x0400005C RID: 92
		private uint _posLimit;

		// Token: 0x0400005D RID: 93
		private bool _streamEndWasReached;

		// Token: 0x0400005E RID: 94
		private uint _pointerToLastSafePosition;

		// Token: 0x0400005F RID: 95
		public uint _bufferOffset;

		// Token: 0x04000060 RID: 96
		public uint _blockSize;

		// Token: 0x04000061 RID: 97
		public uint _pos;

		// Token: 0x04000062 RID: 98
		private uint _keepSizeBefore;

		// Token: 0x04000063 RID: 99
		private uint _keepSizeAfter;

		// Token: 0x04000064 RID: 100
		public uint _streamPos;
	}
}
