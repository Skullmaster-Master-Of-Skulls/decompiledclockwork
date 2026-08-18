using System;
using System.Globalization;
using System.Threading;

namespace System.ServiceModel.Security
{
	// Token: 0x02000352 RID: 850
	internal struct SecurityUniqueId
	{
		// Token: 0x06001F36 RID: 7990 RVA: 0x000741B2 File Offset: 0x000723B2
		private SecurityUniqueId(string prefix, long id)
		{
			this.id = id;
			this.prefix = prefix;
			this.val = null;
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x000741C9 File Offset: 0x000723C9
		public static SecurityUniqueId Create()
		{
			return SecurityUniqueId.Create(SecurityUniqueId.commonPrefix);
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x000741D5 File Offset: 0x000723D5
		public static SecurityUniqueId Create(string prefix)
		{
			return new SecurityUniqueId(prefix, Interlocked.Increment(ref SecurityUniqueId.nextId));
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000741E7 File Offset: 0x000723E7
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

		// Token: 0x04001ECE RID: 7886
		private static long nextId = 0L;

		// Token: 0x04001ECF RID: 7887
		private static string commonPrefix = "uuid-" + Guid.NewGuid().ToString() + "-";

		// Token: 0x04001ED0 RID: 7888
		private long id;

		// Token: 0x04001ED1 RID: 7889
		private string prefix;

		// Token: 0x04001ED2 RID: 7890
		private string val;
	}
}
