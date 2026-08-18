using System;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x02000073 RID: 115
	public struct NamespaceDefinitionHandle : IEquatable<NamespaceDefinitionHandle>
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000A944 File Offset: 0x00008B44
		private NamespaceDefinitionHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000A94D File Offset: 0x00008B4D
		internal static NamespaceDefinitionHandle FromFullNameOffset(int stringHeapOffset)
		{
			return new NamespaceDefinitionHandle((uint)stringHeapOffset);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000A955 File Offset: 0x00008B55
		internal static NamespaceDefinitionHandle FromVirtualIndex(uint virtualIndex)
		{
			if (!HeapHandleType.IsValidHeapOffset(virtualIndex))
			{
				Throw.TooManySubnamespaces();
			}
			return new NamespaceDefinitionHandle(2147483648U | virtualIndex);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000A970 File Offset: 0x00008B70
		public static implicit operator Handle(NamespaceDefinitionHandle handle)
		{
			return new Handle((byte)((handle._value & 2147483648U) >> 24 | 124U), (int)(handle._value & 536870911U));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000A996 File Offset: 0x00008B96
		public static explicit operator NamespaceDefinitionHandle(Handle handle)
		{
			if ((handle.VType & 127) != 124)
			{
				Throw.InvalidCast();
			}
			return new NamespaceDefinitionHandle((uint)((int)(handle.VType & 128) << 24 | handle.Offset));
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		public bool IsNil
		{
			get
			{
				return this._value == 0U;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000A9D3 File Offset: 0x00008BD3
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		internal int GetHeapOffset()
		{
			return (int)(this._value & 536870911U);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000A9F2 File Offset: 0x00008BF2
		internal bool HasFullName
		{
			get
			{
				return !this.IsVirtual;
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000A9FD File Offset: 0x00008BFD
		internal StringHandle GetFullName()
		{
			return StringHandle.FromOffset(this.GetHeapOffset());
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000AA0A File Offset: 0x00008C0A
		public override bool Equals(object obj)
		{
			return obj is NamespaceDefinitionHandle && this.Equals((NamespaceDefinitionHandle)obj);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000AA22 File Offset: 0x00008C22
		public bool Equals(NamespaceDefinitionHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000AA32 File Offset: 0x00008C32
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000AA3A File Offset: 0x00008C3A
		public static bool operator ==(NamespaceDefinitionHandle left, NamespaceDefinitionHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000AA44 File Offset: 0x00008C44
		public static bool operator !=(NamespaceDefinitionHandle left, NamespaceDefinitionHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000342 RID: 834
		private readonly uint _value;
	}
}
