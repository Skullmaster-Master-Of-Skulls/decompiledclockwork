using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000056 RID: 86
	public struct Handle : IEquatable<Handle>
	{
		// Token: 0x06000374 RID: 884 RVA: 0x00008DCA File Offset: 0x00006FCA
		internal static Handle FromVToken(uint vToken)
		{
			return new Handle((byte)(vToken >> 24), (int)(vToken & 16777215U));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00008DDD File Offset: 0x00006FDD
		internal Handle(byte vType, int value)
		{
			this._vType = vType;
			this._value = value;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00008DED File Offset: 0x00006FED
		internal int RowId
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00008DED File Offset: 0x00006FED
		internal int Offset
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00008DF5 File Offset: 0x00006FF5
		internal uint EntityHandleType
		{
			get
			{
				return this.Type << 24;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00008E00 File Offset: 0x00007000
		internal uint Type
		{
			get
			{
				return (uint)(this._vType & 127);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00008E0B File Offset: 0x0000700B
		internal uint EntityHandleValue
		{
			get
			{
				return (uint)((int)this._vType << 24 | this._value);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00008E1D File Offset: 0x0000701D
		internal uint SpecificEntityHandleValue
		{
			get
			{
				return (uint)((int)(this._vType & 128) << 24 | this._value);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00008E35 File Offset: 0x00007035
		internal byte VType
		{
			get
			{
				return this._vType;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00008E3D File Offset: 0x0000703D
		internal bool IsVirtual
		{
			get
			{
				return (this._vType & 128) > 0;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00008E4E File Offset: 0x0000704E
		internal bool IsHeapHandle
		{
			get
			{
				return (this._vType & 112) == 112;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00008E60 File Offset: 0x00007060
		public HandleKind Kind
		{
			get
			{
				uint type = this.Type;
				if ((type & 4294967292U) == 120U)
				{
					return HandleKind.String;
				}
				return (HandleKind)type;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00008E81 File Offset: 0x00007081
		public bool IsNil
		{
			get
			{
				return (this._value | (int)(this._vType & 128)) == 0;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00008E99 File Offset: 0x00007099
		internal bool IsEntityOrUserStringHandle
		{
			get
			{
				return this.Type <= 112U;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00008E0B File Offset: 0x0000700B
		internal int Token
		{
			get
			{
				return (int)this._vType << 24 | this._value;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00008EA8 File Offset: 0x000070A8
		public override bool Equals(object obj)
		{
			return obj is Handle && this.Equals((Handle)obj);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00008EC0 File Offset: 0x000070C0
		public bool Equals(Handle other)
		{
			return this._value == other._value && this._vType == other._vType;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00008EE0 File Offset: 0x000070E0
		public override int GetHashCode()
		{
			return this._value ^ (int)this._vType << 24;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00008EF2 File Offset: 0x000070F2
		public static bool operator ==(Handle left, Handle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00008EFC File Offset: 0x000070FC
		public static bool operator !=(Handle left, Handle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00008F0C File Offset: 0x0000710C
		internal static int Compare(Handle left, Handle right)
		{
			return ((long)((ulong)left._value | (ulong)left._vType << 32)).CompareTo((long)((ulong)right._value | (ulong)right._vType << 32));
		}

		// Token: 0x040002EE RID: 750
		private readonly int _value;

		// Token: 0x040002EF RID: 751
		private readonly byte _vType;

		// Token: 0x040002F0 RID: 752
		public static readonly ModuleDefinitionHandle ModuleDefinition = new ModuleDefinitionHandle(1);

		// Token: 0x040002F1 RID: 753
		public static readonly AssemblyDefinitionHandle AssemblyDefinition = new AssemblyDefinitionHandle(1);
	}
}
