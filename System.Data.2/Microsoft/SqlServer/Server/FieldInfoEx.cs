using System;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200005D RID: 93
	internal sealed class FieldInfoEx : IComparable
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00046684 File Offset: 0x00045A84
		internal FieldInfoEx(FieldInfo fi, int offset, Normalizer normalizer)
		{
			this.fieldInfo = fi;
			this.offset = offset;
			this.normalizer = normalizer;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000466AC File Offset: 0x00045AAC
		public int CompareTo(object other)
		{
			FieldInfoEx fieldInfoEx = other as FieldInfoEx;
			if (fieldInfoEx == null)
			{
				return -1;
			}
			return this.offset.CompareTo(fieldInfoEx.offset);
		}

		// Token: 0x040001DE RID: 478
		internal readonly int offset;

		// Token: 0x040001DF RID: 479
		internal readonly FieldInfo fieldInfo;

		// Token: 0x040001E0 RID: 480
		internal readonly Normalizer normalizer;
	}
}
