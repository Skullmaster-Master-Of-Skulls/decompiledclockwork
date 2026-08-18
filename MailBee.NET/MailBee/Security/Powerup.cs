using System;
using a;

namespace MailBee.Security
{
	// Token: 0x02000108 RID: 264
	public class Powerup
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x00029DE5 File Offset: 0x00028DE5
		public Powerup() : this(null)
		{
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00029DEE File Offset: 0x00028DEE
		public Powerup(string licenseKey)
		{
			Powerup.a(licenseKey);
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00029DFC File Offset: 0x00028DFC
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00029E08 File Offset: 0x00028E08
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = bn.a(value, typeof(Powerup));
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00029E1F File Offset: 0x00028E1F
		internal static bm License
		{
			get
			{
				return Global.u;
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00029E26 File Offset: 0x00028E26
		internal static void a(string A_0)
		{
			Global.a(typeof(Powerup), A_0);
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00029E38 File Offset: 0x00028E38
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}
	}
}
