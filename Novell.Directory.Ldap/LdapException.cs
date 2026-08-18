using System;
using System.Globalization;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000017 RID: 23
	public class LdapException : Exception
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00005434 File Offset: 0x00004434
		public virtual string LdapErrorMessage
		{
			get
			{
				string result;
				if (this.serverMessage != null && this.serverMessage.Length == 0)
				{
					result = null;
				}
				else
				{
					result = this.serverMessage;
				}
				return result;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005464 File Offset: 0x00004464
		public virtual Exception Cause
		{
			get
			{
				return this.rootException;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000547C File Offset: 0x0000447C
		public virtual int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005494 File Offset: 0x00004494
		public virtual string MatchedDN
		{
			get
			{
				return this.matchedDN;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000054AC File Offset: 0x000044AC
		public override string Message
		{
			get
			{
				return this.resultCodeToString();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000054C4 File Offset: 0x000044C4
		public LdapException()
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005504 File Offset: 0x00004504
		public LdapException(string messageOrKey, int resultCode, string serverMsg) : this(messageOrKey, null, resultCode, serverMsg, null, null)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005520 File Offset: 0x00004520
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg) : this(messageOrKey, arguments, resultCode, serverMsg, null, null)
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000553C File Offset: 0x0000453C
		public LdapException(string messageOrKey, int resultCode, string serverMsg, Exception rootException) : this(messageOrKey, null, resultCode, serverMsg, null, rootException)
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005558 File Offset: 0x00004558
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, Exception rootException) : this(messageOrKey, arguments, resultCode, serverMsg, null, rootException)
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005578 File Offset: 0x00004578
		public LdapException(string messageOrKey, int resultCode, string serverMsg, string matchedDN) : this(messageOrKey, null, resultCode, serverMsg, matchedDN, null)
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005594 File Offset: 0x00004594
		public LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, string matchedDN) : this(messageOrKey, arguments, resultCode, serverMsg, matchedDN, null)
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000055B4 File Offset: 0x000045B4
		internal LdapException(string messageOrKey, object[] arguments, int resultCode, string serverMsg, string matchedDN, Exception rootException)
		{
			this.messageOrKey = messageOrKey;
			this.arguments = arguments;
			this.resultCode = resultCode;
			this.rootException = rootException;
			this.matchedDN = matchedDN;
			this.serverMessage = serverMsg;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005620 File Offset: 0x00004620
		public virtual string resultCodeToString()
		{
			return ResourcesHandler.getResultString(this.resultCode);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000563C File Offset: 0x0000463C
		public static string resultCodeToString(int code)
		{
			return ResourcesHandler.getResultString(code);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005654 File Offset: 0x00004654
		public virtual string resultCodeToString(CultureInfo locale)
		{
			return ResourcesHandler.getResultString(this.resultCode, locale);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005674 File Offset: 0x00004674
		public static string resultCodeToString(int code, CultureInfo locale)
		{
			return ResourcesHandler.getResultString(code, locale);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000568C File Offset: 0x0000468C
		public override string ToString()
		{
			return this.getExceptionString("LdapException");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000056A8 File Offset: 0x000046A8
		internal virtual string getExceptionString(string exception)
		{
			string text = ResourcesHandler.getMessage("TOSTRING", new object[]
			{
				exception,
				base.Message,
				this.resultCode,
				this.resultCodeToString()
			});
			if (text.ToUpper().Equals("TOSTRING".ToUpper()))
			{
				text = string.Concat(new object[]
				{
					exception,
					": (",
					this.resultCode,
					") ",
					this.resultCodeToString()
				});
			}
			if (this.serverMessage != null && this.serverMessage.Length != 0)
			{
				string text2 = ResourcesHandler.getMessage("SERVER_MSG", new object[]
				{
					exception,
					this.serverMessage
				});
				if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
				{
					text2 = exception + ": Server Message: " + this.serverMessage;
				}
				text = text + '\n' + text2;
			}
			if (this.matchedDN != null)
			{
				string text2 = ResourcesHandler.getMessage("MATCHED_DN", new object[]
				{
					exception,
					this.matchedDN
				});
				if (text2.ToUpper().Equals("MATCHED_DN".ToUpper()))
				{
					text2 = exception + ": Matched DN: " + this.matchedDN;
				}
				text = text + '\n' + text2;
			}
			if (this.rootException != null)
			{
				text = text + '\n' + this.rootException.ToString();
			}
			return text;
		}

		// Token: 0x04000047 RID: 71
		public const int SUCCESS = 0;

		// Token: 0x04000048 RID: 72
		public const int OPERATIONS_ERROR = 1;

		// Token: 0x04000049 RID: 73
		public const int PROTOCOL_ERROR = 2;

		// Token: 0x0400004A RID: 74
		public const int TIME_LIMIT_EXCEEDED = 3;

		// Token: 0x0400004B RID: 75
		public const int SIZE_LIMIT_EXCEEDED = 4;

		// Token: 0x0400004C RID: 76
		public const int COMPARE_FALSE = 5;

		// Token: 0x0400004D RID: 77
		public const int COMPARE_TRUE = 6;

		// Token: 0x0400004E RID: 78
		public const int AUTH_METHOD_NOT_SUPPORTED = 7;

		// Token: 0x0400004F RID: 79
		public const int STRONG_AUTH_REQUIRED = 8;

		// Token: 0x04000050 RID: 80
		public const int Ldap_PARTIAL_RESULTS = 9;

		// Token: 0x04000051 RID: 81
		public const int REFERRAL = 10;

		// Token: 0x04000052 RID: 82
		public const int ADMIN_LIMIT_EXCEEDED = 11;

		// Token: 0x04000053 RID: 83
		public const int UNAVAILABLE_CRITICAL_EXTENSION = 12;

		// Token: 0x04000054 RID: 84
		public const int CONFIDENTIALITY_REQUIRED = 13;

		// Token: 0x04000055 RID: 85
		public const int SASL_BIND_IN_PROGRESS = 14;

		// Token: 0x04000056 RID: 86
		public const int NO_SUCH_ATTRIBUTE = 16;

		// Token: 0x04000057 RID: 87
		public const int UNDEFINED_ATTRIBUTE_TYPE = 17;

		// Token: 0x04000058 RID: 88
		public const int INAPPROPRIATE_MATCHING = 18;

		// Token: 0x04000059 RID: 89
		public const int CONSTRAINT_VIOLATION = 19;

		// Token: 0x0400005A RID: 90
		public const int ATTRIBUTE_OR_VALUE_EXISTS = 20;

		// Token: 0x0400005B RID: 91
		public const int INVALID_ATTRIBUTE_SYNTAX = 21;

		// Token: 0x0400005C RID: 92
		public const int NO_SUCH_OBJECT = 32;

		// Token: 0x0400005D RID: 93
		public const int ALIAS_PROBLEM = 33;

		// Token: 0x0400005E RID: 94
		public const int INVALID_DN_SYNTAX = 34;

		// Token: 0x0400005F RID: 95
		public const int IS_LEAF = 35;

		// Token: 0x04000060 RID: 96
		public const int ALIAS_DEREFERENCING_PROBLEM = 36;

		// Token: 0x04000061 RID: 97
		public const int INAPPROPRIATE_AUTHENTICATION = 48;

		// Token: 0x04000062 RID: 98
		public const int INVALID_CREDENTIALS = 49;

		// Token: 0x04000063 RID: 99
		public const int INSUFFICIENT_ACCESS_RIGHTS = 50;

		// Token: 0x04000064 RID: 100
		public const int BUSY = 51;

		// Token: 0x04000065 RID: 101
		public const int UNAVAILABLE = 52;

		// Token: 0x04000066 RID: 102
		public const int UNWILLING_TO_PERFORM = 53;

		// Token: 0x04000067 RID: 103
		public const int LOOP_DETECT = 54;

		// Token: 0x04000068 RID: 104
		public const int NAMING_VIOLATION = 64;

		// Token: 0x04000069 RID: 105
		public const int OBJECT_CLASS_VIOLATION = 65;

		// Token: 0x0400006A RID: 106
		public const int NOT_ALLOWED_ON_NONLEAF = 66;

		// Token: 0x0400006B RID: 107
		public const int NOT_ALLOWED_ON_RDN = 67;

		// Token: 0x0400006C RID: 108
		public const int ENTRY_ALREADY_EXISTS = 68;

		// Token: 0x0400006D RID: 109
		public const int OBJECT_CLASS_MODS_PROHIBITED = 69;

		// Token: 0x0400006E RID: 110
		public const int AFFECTS_MULTIPLE_DSAS = 71;

		// Token: 0x0400006F RID: 111
		public const int OTHER = 80;

		// Token: 0x04000070 RID: 112
		public const int SERVER_DOWN = 81;

		// Token: 0x04000071 RID: 113
		public const int LOCAL_ERROR = 82;

		// Token: 0x04000072 RID: 114
		public const int ENCODING_ERROR = 83;

		// Token: 0x04000073 RID: 115
		public const int DECODING_ERROR = 84;

		// Token: 0x04000074 RID: 116
		public const int Ldap_TIMEOUT = 85;

		// Token: 0x04000075 RID: 117
		public const int AUTH_UNKNOWN = 86;

		// Token: 0x04000076 RID: 118
		public const int FILTER_ERROR = 87;

		// Token: 0x04000077 RID: 119
		public const int USER_CANCELLED = 88;

		// Token: 0x04000078 RID: 120
		public const int NO_MEMORY = 90;

		// Token: 0x04000079 RID: 121
		public const int CONNECT_ERROR = 91;

		// Token: 0x0400007A RID: 122
		public const int Ldap_NOT_SUPPORTED = 92;

		// Token: 0x0400007B RID: 123
		public const int CONTROL_NOT_FOUND = 93;

		// Token: 0x0400007C RID: 124
		public const int NO_RESULTS_RETURNED = 94;

		// Token: 0x0400007D RID: 125
		public const int MORE_RESULTS_TO_RETURN = 95;

		// Token: 0x0400007E RID: 126
		public const int CLIENT_LOOP = 96;

		// Token: 0x0400007F RID: 127
		public const int REFERRAL_LIMIT_EXCEEDED = 97;

		// Token: 0x04000080 RID: 128
		public const int INVALID_RESPONSE = 100;

		// Token: 0x04000081 RID: 129
		public const int AMBIGUOUS_RESPONSE = 101;

		// Token: 0x04000082 RID: 130
		public const int TLS_NOT_SUPPORTED = 112;

		// Token: 0x04000083 RID: 131
		public const int SSL_HANDSHAKE_FAILED = 113;

		// Token: 0x04000084 RID: 132
		public const int SSL_PROVIDER_NOT_FOUND = 114;

		// Token: 0x04000085 RID: 133
		private int resultCode = 0;

		// Token: 0x04000086 RID: 134
		private string messageOrKey = null;

		// Token: 0x04000087 RID: 135
		private object[] arguments = null;

		// Token: 0x04000088 RID: 136
		private string matchedDN = null;

		// Token: 0x04000089 RID: 137
		private Exception rootException = null;

		// Token: 0x0400008A RID: 138
		private string serverMessage = null;
	}
}
