using System;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000B7 RID: 183
	public class TriggerBackgroundProcessRequest : LdapExtendedOperation
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0001758C File Offset: 0x0001658C
		public TriggerBackgroundProcessRequest(int processID) : base(null, null)
		{
			switch (processID)
			{
			case 1:
				this.setID("2.16.840.1.113719.1.27.100.43");
				break;
			case 2:
				this.setID("2.16.840.1.113719.1.27.100.47");
				break;
			case 3:
				this.setID("2.16.840.1.113719.1.27.100.49");
				break;
			case 4:
				this.setID("2.16.840.1.113719.1.27.100.51");
				break;
			case 5:
				this.setID("2.16.840.1.113719.1.27.100.53");
				break;
			case 6:
				this.setID("2.16.840.1.113719.1.27.100.55");
				break;
			default:
				throw new ArgumentException("PARAM_ERROR");
			}
		}

		// Token: 0x040003F2 RID: 1010
		public const int Ldap_BK_PROCESS_BKLINKER = 1;

		// Token: 0x040003F3 RID: 1011
		public const int Ldap_BK_PROCESS_JANITOR = 2;

		// Token: 0x040003F4 RID: 1012
		public const int Ldap_BK_PROCESS_LIMBER = 3;

		// Token: 0x040003F5 RID: 1013
		public const int Ldap_BK_PROCESS_SKULKER = 4;

		// Token: 0x040003F6 RID: 1014
		public const int Ldap_BK_PROCESS_SCHEMA_SYNC = 5;

		// Token: 0x040003F7 RID: 1015
		public const int Ldap_BK_PROCESS_PART_PURGE = 6;
	}
}
