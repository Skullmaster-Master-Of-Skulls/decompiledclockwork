using System;

namespace System.Data
{
	// Token: 0x02000126 RID: 294
	internal struct IndexField
	{
		// Token: 0x0600117C RID: 4476 RVA: 0x00087480 File Offset: 0x00086880
		internal IndexField(DataColumn column, bool isDescending)
		{
			this.Column = column;
			this.IsDescending = isDescending;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0008749C File Offset: 0x0008689C
		public static bool operator ==(IndexField if1, IndexField if2)
		{
			return if1.Column == if2.Column && if1.IsDescending == if2.IsDescending;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000874C8 File Offset: 0x000868C8
		public static bool operator !=(IndexField if1, IndexField if2)
		{
			return !(if1 == if2);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000874E0 File Offset: 0x000868E0
		public override bool Equals(object obj)
		{
			return obj is IndexField && this == (IndexField)obj;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00087508 File Offset: 0x00086908
		public override int GetHashCode()
		{
			return this.Column.GetHashCode() ^ this.IsDescending.GetHashCode();
		}

		// Token: 0x040005E8 RID: 1512
		public readonly DataColumn Column;

		// Token: 0x040005E9 RID: 1513
		public readonly bool IsDescending;
	}
}
