using System;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000053 RID: 83
	public sealed class HandleComparer : IEqualityComparer<Handle>, IComparer<Handle>, IEqualityComparer<EntityHandle>, IComparer<EntityHandle>
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00005A68 File Offset: 0x00003C68
		private HandleComparer()
		{
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00008D69 File Offset: 0x00006F69
		public static HandleComparer Default
		{
			get
			{
				return HandleComparer.s_default;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00008D70 File Offset: 0x00006F70
		public bool Equals(Handle x, Handle y)
		{
			return x.Equals(y);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00008D7A File Offset: 0x00006F7A
		public bool Equals(EntityHandle x, EntityHandle y)
		{
			return x.Equals(y);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00008D84 File Offset: 0x00006F84
		public int GetHashCode(Handle obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00008D93 File Offset: 0x00006F93
		public int GetHashCode(EntityHandle obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00008DA2 File Offset: 0x00006FA2
		public int Compare(Handle x, Handle y)
		{
			return Handle.Compare(x, y);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00008DAB File Offset: 0x00006FAB
		public int Compare(EntityHandle x, EntityHandle y)
		{
			return EntityHandle.Compare(x, y);
		}

		// Token: 0x040002C7 RID: 711
		private static readonly HandleComparer s_default = new HandleComparer();
	}
}
