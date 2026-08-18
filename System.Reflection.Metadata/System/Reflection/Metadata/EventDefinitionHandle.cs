using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000064 RID: 100
	public struct EventDefinitionHandle : IEquatable<EventDefinitionHandle>
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00009B42 File Offset: 0x00007D42
		private EventDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00009B4B File Offset: 0x00007D4B
		internal static EventDefinitionHandle FromRowId(int rowId)
		{
			return new EventDefinitionHandle(rowId);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00009B53 File Offset: 0x00007D53
		public static implicit operator Handle(EventDefinitionHandle handle)
		{
			return new Handle(20, handle._rowId);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00009B62 File Offset: 0x00007D62
		public static implicit operator EntityHandle(EventDefinitionHandle handle)
		{
			return new EntityHandle((uint)(335544320L | (long)handle._rowId));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00009B78 File Offset: 0x00007D78
		public static explicit operator EventDefinitionHandle(Handle handle)
		{
			if (handle.VType != 20)
			{
				Throw.InvalidCast();
			}
			return new EventDefinitionHandle(handle.RowId);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00009B96 File Offset: 0x00007D96
		public static explicit operator EventDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 335544320U)
			{
				Throw.InvalidCast();
			}
			return new EventDefinitionHandle(handle.RowId);
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00009BB7 File Offset: 0x00007DB7
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00009BC2 File Offset: 0x00007DC2
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00009BCA File Offset: 0x00007DCA
		public static bool operator ==(EventDefinitionHandle left, EventDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00009BDA File Offset: 0x00007DDA
		public override bool Equals(object obj)
		{
			return obj is EventDefinitionHandle && ((EventDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00009BCA File Offset: 0x00007DCA
		public bool Equals(EventDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00009BFC File Offset: 0x00007DFC
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00009C17 File Offset: 0x00007E17
		public static bool operator !=(EventDefinitionHandle left, EventDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000319 RID: 793
		private const uint tokenType = 335544320U;

		// Token: 0x0400031A RID: 794
		private const byte tokenTypeSmall = 20;

		// Token: 0x0400031B RID: 795
		private readonly int _rowId;
	}
}
