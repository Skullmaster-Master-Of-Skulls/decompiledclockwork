using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B7 RID: 183
	public sealed class Missing
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0003798B File Offset: 0x00035B8B
		public static Missing Value
		{
			get
			{
				return Missing.s_instance;
			}
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00037992 File Offset: 0x00035B92
		private Missing()
		{
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0003799A File Offset: 0x00035B9A
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x040004D5 RID: 1237
		private static readonly Missing s_instance = new Missing();
	}
}
