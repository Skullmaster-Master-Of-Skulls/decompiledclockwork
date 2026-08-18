using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006F RID: 111
	public struct ManifestResourceHandle : IEquatable<ManifestResourceHandle>
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0000A566 File Offset: 0x00008766
		private ManifestResourceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000A56F File Offset: 0x0000876F
		internal static ManifestResourceHandle FromRowId(int rowId)
		{
			return new ManifestResourceHandle(rowId);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000A577 File Offset: 0x00008777
		public static implicit operator Handle(ManifestResourceHandle handle)
		{
			return new Handle(40, handle._rowId);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000A586 File Offset: 0x00008786
		public static implicit operator EntityHandle(ManifestResourceHandle handle)
		{
			return new EntityHandle((uint)(671088640L | (long)handle._rowId));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000A59C File Offset: 0x0000879C
		public static explicit operator ManifestResourceHandle(Handle handle)
		{
			if (handle.VType != 40)
			{
				Throw.InvalidCast();
			}
			return new ManifestResourceHandle(handle.RowId);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000A5BA File Offset: 0x000087BA
		public static explicit operator ManifestResourceHandle(EntityHandle handle)
		{
			if (handle.VType != 671088640U)
			{
				Throw.InvalidCast();
			}
			return new ManifestResourceHandle(handle.RowId);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0000A5DB File Offset: 0x000087DB
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000A5E6 File Offset: 0x000087E6
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000A5EE File Offset: 0x000087EE
		public static bool operator ==(ManifestResourceHandle left, ManifestResourceHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000A5FE File Offset: 0x000087FE
		public override bool Equals(object obj)
		{
			return obj is ManifestResourceHandle && ((ManifestResourceHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000A5EE File Offset: 0x000087EE
		public bool Equals(ManifestResourceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000A620 File Offset: 0x00008820
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000A63B File Offset: 0x0000883B
		public static bool operator !=(ManifestResourceHandle left, ManifestResourceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400033A RID: 826
		private const uint tokenType = 671088640U;

		// Token: 0x0400033B RID: 827
		private const byte tokenTypeSmall = 40;

		// Token: 0x0400033C RID: 828
		private readonly int _rowId;
	}
}
