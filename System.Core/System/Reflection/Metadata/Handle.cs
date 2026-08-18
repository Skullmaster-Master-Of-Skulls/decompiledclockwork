using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000054 RID: 84
	internal struct Handle : IEquatable<Handle>
	{
		// Token: 0x06000269 RID: 617 RVA: 0x000065DB File Offset: 0x000047DB
		internal static Handle FromVToken(uint vToken)
		{
			return new Handle((byte)(vToken >> 24), (int)(vToken & 16777215U));
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000065EE File Offset: 0x000047EE
		internal Handle(byte vType, int value)
		{
			this._vType = vType;
			this._value = value;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000065FE File Offset: 0x000047FE
		internal int RowId
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00006606 File Offset: 0x00004806
		internal int Offset
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000660E File Offset: 0x0000480E
		internal uint EntityHandleType
		{
			get
			{
				return this.Type << 24;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00006619 File Offset: 0x00004819
		internal uint Type
		{
			get
			{
				return (uint)(this._vType & 127);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00006624 File Offset: 0x00004824
		internal byte VType
		{
			get
			{
				return this._vType;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000662C File Offset: 0x0000482C
		internal bool IsVirtual
		{
			get
			{
				return (this._vType & 128) > 0;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000663D File Offset: 0x0000483D
		internal bool IsHeapHandle
		{
			get
			{
				return (this._vType & 112) == 112;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000664C File Offset: 0x0000484C
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000666D File Offset: 0x0000486D
		public bool IsNil
		{
			get
			{
				return (this._value | (int)(this._vType & 128)) == 0;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00006685 File Offset: 0x00004885
		internal bool IsEntityOrUserStringHandle
		{
			get
			{
				return this.Type <= 112U;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00006694 File Offset: 0x00004894
		internal int Token
		{
			get
			{
				return (int)this._vType << 24 | this._value;
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000066A6 File Offset: 0x000048A6
		public override bool Equals(object obj)
		{
			return obj is Handle && this.Equals((Handle)obj);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000066BE File Offset: 0x000048BE
		public bool Equals(Handle other)
		{
			return this._value == other._value && this._vType == other._vType;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000066DE File Offset: 0x000048DE
		public override int GetHashCode()
		{
			return this._value ^ (int)this._vType << 24;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000066F0 File Offset: 0x000048F0
		public static bool operator ==(Handle left, Handle right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000066FA File Offset: 0x000048FA
		public static bool operator !=(Handle left, Handle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006708 File Offset: 0x00004908
		internal static int Compare(Handle left, Handle right)
		{
			return ((long)((ulong)left._value | (ulong)left._vType << 32)).CompareTo((long)((ulong)right._value | (ulong)right._vType << 32));
		}

		// Token: 0x04000301 RID: 769
		private readonly int _value;

		// Token: 0x04000302 RID: 770
		private readonly byte _vType;
	}
}
