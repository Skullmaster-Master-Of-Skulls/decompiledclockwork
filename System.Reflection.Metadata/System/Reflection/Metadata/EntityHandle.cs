using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000057 RID: 87
	public struct EntityHandle : IEquatable<EntityHandle>
	{
		// Token: 0x0600038A RID: 906 RVA: 0x00008F5D File Offset: 0x0000715D
		internal EntityHandle(uint vToken)
		{
			this._vToken = vToken;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00008F66 File Offset: 0x00007166
		public static implicit operator Handle(EntityHandle handle)
		{
			return Handle.FromVToken(handle._vToken);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00008F73 File Offset: 0x00007173
		public static explicit operator EntityHandle(Handle handle)
		{
			if (handle.IsHeapHandle)
			{
				Throw.InvalidCast();
			}
			return new EntityHandle(handle.EntityHandleValue);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00008F8F File Offset: 0x0000718F
		internal uint Type
		{
			get
			{
				return this._vToken & 2130706432U;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00008F9D File Offset: 0x0000719D
		internal uint VType
		{
			get
			{
				return this._vToken & 4278190080U;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00008FAB File Offset: 0x000071AB
		internal bool IsVirtual
		{
			get
			{
				return (this._vToken & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00008FBC File Offset: 0x000071BC
		public bool IsNil
		{
			get
			{
				return (this._vToken & 2164260863U) == 0U;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00008FCD File Offset: 0x000071CD
		internal int RowId
		{
			get
			{
				return (int)(this._vToken & 16777215U);
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00008FDB File Offset: 0x000071DB
		internal uint SpecificHandleValue
		{
			get
			{
				return this._vToken & 2164260863U;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00008FE9 File Offset: 0x000071E9
		public HandleKind Kind
		{
			get
			{
				return (HandleKind)(this.Type >> 24);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00008FF5 File Offset: 0x000071F5
		internal int Token
		{
			get
			{
				return (int)this._vToken;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00008FFD File Offset: 0x000071FD
		public override bool Equals(object obj)
		{
			return obj is EntityHandle && this.Equals((EntityHandle)obj);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009015 File Offset: 0x00007215
		public bool Equals(EntityHandle other)
		{
			return this._vToken == other._vToken;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00008FF5 File Offset: 0x000071F5
		public override int GetHashCode()
		{
			return (int)this._vToken;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00009025 File Offset: 0x00007225
		public static bool operator ==(EntityHandle left, EntityHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000902F File Offset: 0x0000722F
		public static bool operator !=(EntityHandle left, EntityHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000903C File Offset: 0x0000723C
		internal static int Compare(EntityHandle left, EntityHandle right)
		{
			return left._vToken.CompareTo(right._vToken);
		}

		// Token: 0x040002F2 RID: 754
		private readonly uint _vToken;

		// Token: 0x040002F3 RID: 755
		public static readonly ModuleDefinitionHandle ModuleDefinition = new ModuleDefinitionHandle(1);

		// Token: 0x040002F4 RID: 756
		public static readonly AssemblyDefinitionHandle AssemblyDefinition = new AssemblyDefinitionHandle(1);
	}
}
