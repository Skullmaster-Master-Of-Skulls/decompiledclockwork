using System;

namespace System.Reflection.Metadata.Ecma335.Blobs
{
	// Token: 0x02000122 RID: 290
	internal struct ExceptionRegionEncoder
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001C55C File Offset: 0x0001A75C
		public BlobBuilder Builder { get; }

		// Token: 0x06000991 RID: 2449 RVA: 0x0001C564 File Offset: 0x0001A764
		internal ExceptionRegionEncoder(BlobBuilder builder, int exceptionRegionCount, bool hasLargeRegions)
		{
			this.Builder = builder;
			this._exceptionRegionCount = exceptionRegionCount;
			this._isSmallFormat = (!hasLargeRegions && ExceptionRegionEncoder.IsSmallRegionCount(exceptionRegionCount));
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001C588 File Offset: 0x0001A788
		public void StartRegions()
		{
			if (this._exceptionRegionCount == 0)
			{
				return;
			}
			int exceptionTableSize = ExceptionRegionEncoder.GetExceptionTableSize(this._exceptionRegionCount, this._isSmallFormat);
			this.Builder.Align(4);
			if (this._isSmallFormat)
			{
				this.Builder.WriteByte(1);
				this.Builder.WriteByte((byte)(exceptionTableSize & 255));
				this.Builder.WriteInt16(0);
				return;
			}
			this.Builder.WriteByte(65);
			this.Builder.WriteByte((byte)(exceptionTableSize & 255));
			this.Builder.WriteUInt16((ushort)(exceptionTableSize >> 8 & 65535));
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001C625 File Offset: 0x0001A825
		public static bool IsSmallRegionCount(int exceptionRegionCount)
		{
			return ExceptionRegionEncoder.GetExceptionTableSize(exceptionRegionCount, true) <= 255;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001C638 File Offset: 0x0001A838
		public static bool IsSmallExceptionRegion(int startOffset, int length)
		{
			return startOffset <= 65535 && length <= 255;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001C64F File Offset: 0x0001A84F
		internal static int GetExceptionTableSize(int exceptionRegionCount, bool isSmallFormat)
		{
			return 4 + exceptionRegionCount * (isSmallFormat ? 12 : 24);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001C660 File Offset: 0x0001A860
		public void AddFinally(int tryOffset, int tryLength, int handlerOffset, int handlerLength)
		{
			this.AddRegion(ExceptionRegionKind.Finally, tryOffset, tryLength, handlerOffset, handlerLength, default(EntityHandle), 0);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001C684 File Offset: 0x0001A884
		public void AddFault(int tryOffset, int tryLength, int handlerOffset, int handlerLength)
		{
			this.AddRegion(ExceptionRegionKind.Fault, tryOffset, tryLength, handlerOffset, handlerLength, default(EntityHandle), 0);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001C6A7 File Offset: 0x0001A8A7
		public void AddCatch(int tryOffset, int tryLength, int handlerOffset, int handlerLength, EntityHandle catchType)
		{
			this.AddRegion(ExceptionRegionKind.Catch, tryOffset, tryLength, handlerOffset, handlerLength, catchType, 0);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		public void AddFilter(int tryOffset, int tryLength, int handlerOffset, int handlerLength, int filterOffset)
		{
			this.AddRegion(ExceptionRegionKind.Filter, tryOffset, tryLength, handlerOffset, handlerLength, default(EntityHandle), filterOffset);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		public void AddRegion(ExceptionRegionKind kind, int tryOffset, int tryLength, int handlerOffset, int handlerLength, EntityHandle catchType, int filterOffset)
		{
			if (this._isSmallFormat)
			{
				this.Builder.WriteUInt16((ushort)kind);
				this.Builder.WriteUInt16((ushort)tryOffset);
				this.Builder.WriteByte((byte)tryLength);
				this.Builder.WriteUInt16((ushort)handlerOffset);
				this.Builder.WriteByte((byte)handlerLength);
			}
			else
			{
				this.Builder.WriteInt32((int)kind);
				this.Builder.WriteInt32(tryOffset);
				this.Builder.WriteInt32(tryLength);
				this.Builder.WriteInt32(handlerOffset);
				this.Builder.WriteInt32(handlerLength);
			}
			if (kind == ExceptionRegionKind.Catch)
			{
				this.Builder.WriteInt32(MetadataTokens.GetToken(catchType));
				return;
			}
			if (kind != ExceptionRegionKind.Filter)
			{
				this.Builder.WriteInt32(0);
				return;
			}
			this.Builder.WriteInt32(filterOffset);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000031EB File Offset: 0x000013EB
		public void EndRegions()
		{
		}

		// Token: 0x04000891 RID: 2193
		private readonly int _exceptionRegionCount;

		// Token: 0x04000892 RID: 2194
		private readonly bool _isSmallFormat;
	}
}
