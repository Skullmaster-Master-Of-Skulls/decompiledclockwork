using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002F RID: 47
	public struct LdapDSConstants
	{
		// Token: 0x040000EC RID: 236
		public static readonly long LDAP_DS_ENTRY_BROWSE = 1L;

		// Token: 0x040000ED RID: 237
		public static readonly long LDAP_DS_ENTRY_ADD = 2L;

		// Token: 0x040000EE RID: 238
		public static readonly long LDAP_DS_ENTRY_DELETE = 4L;

		// Token: 0x040000EF RID: 239
		public static readonly long LDAP_DS_ENTRY_RENAME = 8L;

		// Token: 0x040000F0 RID: 240
		public static readonly long LDAP_DS_ENTRY_SUPERVISOR = 16L;

		// Token: 0x040000F1 RID: 241
		public static readonly long LDAP_DS_ENTRY_INHERIT_CTL = 64L;

		// Token: 0x040000F2 RID: 242
		public static readonly long LDAP_DS_ATTR_COMPARE = 1L;

		// Token: 0x040000F3 RID: 243
		public static readonly long LDAP_DS_ATTR_READ = 2L;

		// Token: 0x040000F4 RID: 244
		public static readonly long LDAP_DS_ATTR_WRITE = 4L;

		// Token: 0x040000F5 RID: 245
		public static readonly long LDAP_DS_ATTR_SELF = 8L;

		// Token: 0x040000F6 RID: 246
		public static readonly long LDAP_DS_ATTR_SUPERVISOR = 32L;

		// Token: 0x040000F7 RID: 247
		public static readonly long LDAP_DS_ATTR_INHERIT_CTL = 64L;

		// Token: 0x040000F8 RID: 248
		public static readonly long LDAP_DS_DYNAMIC_ACL = 1073741824L;

		// Token: 0x040000F9 RID: 249
		public static readonly int LDAP_DS_ALIAS_ENTRY = 1;

		// Token: 0x040000FA RID: 250
		public static readonly int LDAP_DS_PARTITION_ROOT = 2;

		// Token: 0x040000FB RID: 251
		public static readonly int LDAP_DS_CONTAINER_ENTRY = 4;

		// Token: 0x040000FC RID: 252
		public static readonly int LDAP_DS_CONTAINER_ALIAS = 8;

		// Token: 0x040000FD RID: 253
		public static readonly int LDAP_DS_MATCHES_LIST_FILTER = 16;

		// Token: 0x040000FE RID: 254
		public static readonly int LDAP_DS_REFERENCE_ENTRY = 32;

		// Token: 0x040000FF RID: 255
		public static readonly int LDAP_DS_40X_REFERENCE_ENTRY = 64;

		// Token: 0x04000100 RID: 256
		public static readonly int LDAP_DS_BACKLINKED = 128;

		// Token: 0x04000101 RID: 257
		public static readonly int LDAP_DS_NEW_ENTRY = 256;

		// Token: 0x04000102 RID: 258
		public static readonly int LDAP_DS_TEMPORARY_REFERENCE = 512;

		// Token: 0x04000103 RID: 259
		public static readonly int LDAP_DS_AUDITED = 1024;

		// Token: 0x04000104 RID: 260
		public static readonly int LDAP_DS_ENTRY_NOT_PRESENT = 2048;

		// Token: 0x04000105 RID: 261
		public static readonly int LDAP_DS_ENTRY_VERIFY_CTS = 4096;

		// Token: 0x04000106 RID: 262
		public static readonly int LDAP_DS_ENTRY_DAMAGED = 8192;
	}
}
