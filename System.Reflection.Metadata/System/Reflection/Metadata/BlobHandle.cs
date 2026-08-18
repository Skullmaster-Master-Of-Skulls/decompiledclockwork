using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000074 RID: 116
	public struct BlobHandle : IEquatable<BlobHandle>
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x0000AA51 File Offset: 0x00008C51
		private BlobHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000AA5A File Offset: 0x00008C5A
		internal static BlobHandle FromOffset(int heapOffset)
		{
			return new BlobHandle((uint)heapOffset);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000AA62 File Offset: 0x00008C62
		internal static BlobHandle FromVirtualIndex(BlobHandle.VirtualIndex virtualIndex, ushort virtualValue)
		{
			return new BlobHandle((uint)(int.MinValue | (int)virtualValue << 8 | (int)virtualIndex));
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000AA74 File Offset: 0x00008C74
		internal unsafe void SubstituteTemplateParameters(byte[] blob)
		{
			fixed (byte* ptr = &blob[2])
			{
				*(int*)ptr = (int)this.VirtualValue;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000AA95 File Offset: 0x00008C95
		public static implicit operator Handle(BlobHandle handle)
		{
			return new Handle((byte)((handle._value & 2147483648U) >> 24 | 113U), (int)(handle._value & 536870911U));
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000AABB File Offset: 0x00008CBB
		public static explicit operator BlobHandle(Handle handle)
		{
			if ((handle.VType & 127) != 113)
			{
				Throw.InvalidCast();
			}
			return new BlobHandle((uint)((int)(handle.VType & 128) << 24 | handle.Offset));
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000AAED File Offset: 0x00008CED
		public bool IsNil
		{
			get
			{
				return this._value == 0U;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		internal int GetHeapOffset()
		{
			return (int)this._value;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000AB00 File Offset: 0x00008D00
		internal BlobHandle.VirtualIndex GetVirtualIndex()
		{
			return (BlobHandle.VirtualIndex)(this._value & 255U);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000AB0F File Offset: 0x00008D0F
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000AB20 File Offset: 0x00008D20
		private ushort VirtualValue
		{
			get
			{
				return (ushort)(this._value >> 8);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000AB2B File Offset: 0x00008D2B
		public override bool Equals(object obj)
		{
			return obj is BlobHandle && this.Equals((BlobHandle)obj);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000AB43 File Offset: 0x00008D43
		public bool Equals(BlobHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000AB53 File Offset: 0x00008D53
		public static bool operator ==(BlobHandle left, BlobHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000AB5D File Offset: 0x00008D5D
		public static bool operator !=(BlobHandle left, BlobHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000343 RID: 835
		private readonly uint _value;

		// Token: 0x04000344 RID: 836
		internal const int TemplateParameterOffset_AttributeUsageTarget = 2;

		// Token: 0x02000186 RID: 390
		internal enum VirtualIndex : byte
		{
			// Token: 0x040009F1 RID: 2545
			Nil,
			// Token: 0x040009F2 RID: 2546
			ContractPublicKeyToken,
			// Token: 0x040009F3 RID: 2547
			ContractPublicKey,
			// Token: 0x040009F4 RID: 2548
			AttributeUsage_AllowSingle,
			// Token: 0x040009F5 RID: 2549
			AttributeUsage_AllowMultiple,
			// Token: 0x040009F6 RID: 2550
			Count
		}
	}
}
