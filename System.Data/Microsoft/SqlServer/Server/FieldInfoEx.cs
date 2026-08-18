using System;
using System.Reflection;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000288 RID: 648
	internal sealed class FieldInfoEx : IComparable
	{
		// Token: 0x06002214 RID: 8724 RVA: 0x0028AC28 File Offset: 0x0028A028
		internal FieldInfoEx(FieldInfo fi, int offset, Normalizer normalizer)
		{
			this.fieldInfo = fi;
			this.offset = offset;
			this.normalizer = normalizer;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0028AC58 File Offset: 0x0028A058
		public int CompareTo(object other)
		{
			FieldInfoEx fieldInfoEx = other as FieldInfoEx;
			if (fieldInfoEx == null)
			{
				return -1;
			}
			return this.offset.CompareTo(fieldInfoEx.offset);
		}

		// Token: 0x04001657 RID: 5719
		internal readonly int offset;

		// Token: 0x04001658 RID: 5720
		internal readonly FieldInfo fieldInfo;

		// Token: 0x04001659 RID: 5721
		internal readonly Normalizer normalizer;
	}
}
