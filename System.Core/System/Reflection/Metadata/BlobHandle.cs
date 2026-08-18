using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000067 RID: 103
	internal struct BlobHandle : IEquatable<BlobHandle>
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x000077D7 File Offset: 0x000059D7
		private BlobHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000077E0 File Offset: 0x000059E0
		internal static BlobHandle FromOffset(int heapOffset)
		{
			return new BlobHandle((uint)heapOffset);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000077E8 File Offset: 0x000059E8
		internal void SubstituteTemplateParameters(byte[] blob)
		{
			ushort virtualValue = this.VirtualValue;
			blob[2] = (byte)(virtualValue & 255);
			blob[3] = (byte)(virtualValue >> 8 & 255);
			blob[4] = 0;
			blob[5] = 0;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000781C File Offset: 0x00005A1C
		public bool IsNil
		{
			get
			{
				return this._value == 0U;
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007827 File Offset: 0x00005A27
		internal int GetHeapOffset()
		{
			return (int)this._value;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000782F File Offset: 0x00005A2F
		internal BlobHandle.VirtualIndex GetVirtualIndex()
		{
			return (BlobHandle.VirtualIndex)(this._value & 255U);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000783E File Offset: 0x00005A3E
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000784F File Offset: 0x00005A4F
		private ushort VirtualValue
		{
			get
			{
				return (ushort)(this._value >> 8);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000785A File Offset: 0x00005A5A
		public override bool Equals(object obj)
		{
			return obj is BlobHandle && this.Equals((BlobHandle)obj);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00007872 File Offset: 0x00005A72
		public bool Equals(BlobHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00007882 File Offset: 0x00005A82
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		// Token: 0x04000366 RID: 870
		private readonly uint _value;

		// Token: 0x04000367 RID: 871
		internal const int TemplateParameterOffset_AttributeUsageTarget = 2;

		// Token: 0x020002FB RID: 763
		internal enum VirtualIndex : byte
		{
			// Token: 0x04000DF3 RID: 3571
			Nil,
			// Token: 0x04000DF4 RID: 3572
			ContractPublicKeyToken,
			// Token: 0x04000DF5 RID: 3573
			ContractPublicKey,
			// Token: 0x04000DF6 RID: 3574
			AttributeUsage_AllowSingle,
			// Token: 0x04000DF7 RID: 3575
			AttributeUsage_AllowMultiple,
			// Token: 0x04000DF8 RID: 3576
			Count
		}
	}
}
