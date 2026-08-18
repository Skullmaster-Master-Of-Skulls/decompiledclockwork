using System;

// Token: 0x02000003 RID: 3
internal static class Interop
{
	// Token: 0x020006AF RID: 1711
	internal static class SChannel
	{
		// Token: 0x04002EC3 RID: 11971
		public const int SCHANNEL_RENEGOTIATE = 0;

		// Token: 0x04002EC4 RID: 11972
		public const int SCHANNEL_SHUTDOWN = 1;

		// Token: 0x04002EC5 RID: 11973
		public const int SCHANNEL_ALERT = 2;

		// Token: 0x04002EC6 RID: 11974
		public const int SCHANNEL_SESSION = 3;

		// Token: 0x04002EC7 RID: 11975
		public const int TLS1_ALERT_WARNING = 1;

		// Token: 0x04002EC8 RID: 11976
		public const int TLS1_ALERT_FATAL = 2;

		// Token: 0x04002EC9 RID: 11977
		public const int TLS1_ALERT_CLOSE_NOTIFY = 0;

		// Token: 0x04002ECA RID: 11978
		public const int TLS1_ALERT_UNEXPECTED_MESSAGE = 10;

		// Token: 0x04002ECB RID: 11979
		public const int TLS1_ALERT_BAD_RECORD_MAC = 20;

		// Token: 0x04002ECC RID: 11980
		public const int TLS1_ALERT_DECRYPTION_FAILED = 21;

		// Token: 0x04002ECD RID: 11981
		public const int TLS1_ALERT_RECORD_OVERFLOW = 22;

		// Token: 0x04002ECE RID: 11982
		public const int TLS1_ALERT_DECOMPRESSION_FAIL = 30;

		// Token: 0x04002ECF RID: 11983
		public const int TLS1_ALERT_HANDSHAKE_FAILURE = 40;

		// Token: 0x04002ED0 RID: 11984
		public const int TLS1_ALERT_BAD_CERTIFICATE = 42;

		// Token: 0x04002ED1 RID: 11985
		public const int TLS1_ALERT_UNSUPPORTED_CERT = 43;

		// Token: 0x04002ED2 RID: 11986
		public const int TLS1_ALERT_CERTIFICATE_REVOKED = 44;

		// Token: 0x04002ED3 RID: 11987
		public const int TLS1_ALERT_CERTIFICATE_EXPIRED = 45;

		// Token: 0x04002ED4 RID: 11988
		public const int TLS1_ALERT_CERTIFICATE_UNKNOWN = 46;

		// Token: 0x04002ED5 RID: 11989
		public const int TLS1_ALERT_ILLEGAL_PARAMETER = 47;

		// Token: 0x04002ED6 RID: 11990
		public const int TLS1_ALERT_UNKNOWN_CA = 48;

		// Token: 0x04002ED7 RID: 11991
		public const int TLS1_ALERT_ACCESS_DENIED = 49;

		// Token: 0x04002ED8 RID: 11992
		public const int TLS1_ALERT_DECODE_ERROR = 50;

		// Token: 0x04002ED9 RID: 11993
		public const int TLS1_ALERT_DECRYPT_ERROR = 51;

		// Token: 0x04002EDA RID: 11994
		public const int TLS1_ALERT_EXPORT_RESTRICTION = 60;

		// Token: 0x04002EDB RID: 11995
		public const int TLS1_ALERT_PROTOCOL_VERSION = 70;

		// Token: 0x04002EDC RID: 11996
		public const int TLS1_ALERT_INSUFFIENT_SECURITY = 71;

		// Token: 0x04002EDD RID: 11997
		public const int TLS1_ALERT_INTERNAL_ERROR = 80;

		// Token: 0x04002EDE RID: 11998
		public const int TLS1_ALERT_USER_CANCELED = 90;

		// Token: 0x04002EDF RID: 11999
		public const int TLS1_ALERT_NO_RENEGOTIATION = 100;

		// Token: 0x04002EE0 RID: 12000
		public const int TLS1_ALERT_UNSUPPORTED_EXT = 110;

		// Token: 0x020008CC RID: 2252
		public struct SCHANNEL_ALERT_TOKEN
		{
			// Token: 0x04003B6C RID: 15212
			public uint dwTokenType;

			// Token: 0x04003B6D RID: 15213
			public uint dwAlertType;

			// Token: 0x04003B6E RID: 15214
			public uint dwAlertNumber;
		}
	}
}
