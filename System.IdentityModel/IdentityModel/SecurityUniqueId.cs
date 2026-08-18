using System;
using System.Globalization;
using System.Threading;

namespace System.IdentityModel
{
	// Token: 0x02000076 RID: 118
	internal class SecurityUniqueId
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F06E File Offset: 0x0000D26E
		private SecurityUniqueId(string prefix, long id)
		{
			this.id = id;
			this.prefix = prefix;
			this.val = null;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000F08B File Offset: 0x0000D28B
		public static SecurityUniqueId Create()
		{
			return SecurityUniqueId.Create(SecurityUniqueId.commonPrefix);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000F097 File Offset: 0x0000D297
		public static SecurityUniqueId Create(string prefix)
		{
			return new SecurityUniqueId(prefix, Interlocked.Increment(ref SecurityUniqueId.nextId));
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000F0A9 File Offset: 0x0000D2A9
		public string Value
		{
			get
			{
				if (this.val == null)
				{
					this.val = this.prefix + this.id.ToString(CultureInfo.InvariantCulture);
				}
				return this.val;
			}
		}

		// Token: 0x0400037B RID: 891
		private static long nextId = 0L;

		// Token: 0x0400037C RID: 892
		private static string commonPrefix = "uuid-" + Guid.NewGuid().ToString() + "-";

		// Token: 0x0400037D RID: 893
		private long id;

		// Token: 0x0400037E RID: 894
		private string prefix;

		// Token: 0x0400037F RID: 895
		private string val;
	}
}
