using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006B RID: 107
	public struct AssemblyReferenceHandle : IEquatable<AssemblyReferenceHandle>
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x0000A196 File Offset: 0x00008396
		private AssemblyReferenceHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000A19F File Offset: 0x0000839F
		internal static AssemblyReferenceHandle FromRowId(int rowId)
		{
			return new AssemblyReferenceHandle((uint)rowId);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000A1A7 File Offset: 0x000083A7
		internal static AssemblyReferenceHandle FromVirtualIndex(AssemblyReferenceHandle.VirtualIndex virtualIndex)
		{
			return new AssemblyReferenceHandle((uint)((AssemblyReferenceHandle.VirtualIndex)(-2147483648) | virtualIndex));
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000A1B5 File Offset: 0x000083B5
		public static implicit operator Handle(AssemblyReferenceHandle handle)
		{
			return Handle.FromVToken(handle.VToken);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000A1C3 File Offset: 0x000083C3
		public static implicit operator EntityHandle(AssemblyReferenceHandle handle)
		{
			return new EntityHandle(handle.VToken);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000A1D1 File Offset: 0x000083D1
		public static explicit operator AssemblyReferenceHandle(Handle handle)
		{
			if (handle.Type != 35U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyReferenceHandle(handle.SpecificEntityHandleValue);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000A1EF File Offset: 0x000083EF
		public static explicit operator AssemblyReferenceHandle(EntityHandle handle)
		{
			if (handle.Type != 587202560U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyReferenceHandle(handle.SpecificHandleValue);
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000A210 File Offset: 0x00008410
		internal uint Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000A218 File Offset: 0x00008418
		private uint VToken
		{
			get
			{
				return this._value | 587202560U;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000A226 File Offset: 0x00008426
		public bool IsNil
		{
			get
			{
				return this._value == 0U;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000A231 File Offset: 0x00008431
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000A242 File Offset: 0x00008442
		internal int RowId
		{
			get
			{
				return (int)(this._value & 16777215U);
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000A250 File Offset: 0x00008450
		public static bool operator ==(AssemblyReferenceHandle left, AssemblyReferenceHandle right)
		{
			return left._value == right._value;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000A260 File Offset: 0x00008460
		public override bool Equals(object obj)
		{
			return obj is AssemblyReferenceHandle && ((AssemblyReferenceHandle)obj)._value == this._value;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000A250 File Offset: 0x00008450
		public bool Equals(AssemblyReferenceHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000A280 File Offset: 0x00008480
		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0000A29B File Offset: 0x0000849B
		public static bool operator !=(AssemblyReferenceHandle left, AssemblyReferenceHandle right)
		{
			return left._value != right._value;
		}

		// Token: 0x0400032E RID: 814
		private const uint tokenType = 587202560U;

		// Token: 0x0400032F RID: 815
		private const byte tokenTypeSmall = 35;

		// Token: 0x04000330 RID: 816
		private readonly uint _value;

		// Token: 0x02000184 RID: 388
		internal enum VirtualIndex
		{
			// Token: 0x040009A0 RID: 2464
			System_Runtime,
			// Token: 0x040009A1 RID: 2465
			System_Runtime_InteropServices_WindowsRuntime,
			// Token: 0x040009A2 RID: 2466
			System_ObjectModel,
			// Token: 0x040009A3 RID: 2467
			System_Runtime_WindowsRuntime,
			// Token: 0x040009A4 RID: 2468
			System_Runtime_WindowsRuntime_UI_Xaml,
			// Token: 0x040009A5 RID: 2469
			System_Numerics_Vectors,
			// Token: 0x040009A6 RID: 2470
			Count
		}
	}
}
