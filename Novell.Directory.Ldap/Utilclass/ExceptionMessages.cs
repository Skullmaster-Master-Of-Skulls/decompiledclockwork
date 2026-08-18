using System;
using System.Resources;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000F0 RID: 240
	public class ExceptionMessages : ResourceManager
	{
		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C320 File Offset: 0x0001B320
		public object[][] getContents()
		{
			return ExceptionMessages.contents;
		}

		// Token: 0x04000446 RID: 1094
		[CLSCompliant(false)]
		public const string TOSTRING = "TOSTRING";

		// Token: 0x04000447 RID: 1095
		public const string SERVER_MSG = "SERVER_MSG";

		// Token: 0x04000448 RID: 1096
		public const string MATCHED_DN = "MATCHED_DN";

		// Token: 0x04000449 RID: 1097
		public const string FAILED_REFERRAL = "FAILED_REFERRAL";

		// Token: 0x0400044A RID: 1098
		public const string REFERRAL_ITEM = "REFERRAL_ITEM";

		// Token: 0x0400044B RID: 1099
		public const string CONNECTION_ERROR = "CONNECTION_ERROR";

		// Token: 0x0400044C RID: 1100
		public const string CONNECTION_IMPOSSIBLE = "CONNECTION_IMPOSSIBLE";

		// Token: 0x0400044D RID: 1101
		public const string CONNECTION_WAIT = "CONNECTION_WAIT";

		// Token: 0x0400044E RID: 1102
		public const string CONNECTION_FINALIZED = "CONNECTION_FINALIZED";

		// Token: 0x0400044F RID: 1103
		public const string CONNECTION_CLOSED = "CONNECTION_CLOSED";

		// Token: 0x04000450 RID: 1104
		public const string CONNECTION_READER = "CONNECTION_READER";

		// Token: 0x04000451 RID: 1105
		public const string DUP_ERROR = "DUP_ERROR";

		// Token: 0x04000452 RID: 1106
		public const string REFERRAL_ERROR = "REFERRAL_ERROR";

		// Token: 0x04000453 RID: 1107
		public const string REFERRAL_LOCAL = "REFERRAL_LOCAL";

		// Token: 0x04000454 RID: 1108
		public const string REFERENCE_ERROR = "REFERENCE_ERROR";

		// Token: 0x04000455 RID: 1109
		public const string REFERRAL_SEND = "REFERRAL_SEND";

		// Token: 0x04000456 RID: 1110
		public const string REFERENCE_NOFOLLOW = "REFERENCE_NOFOLLOW";

		// Token: 0x04000457 RID: 1111
		public const string REFERRAL_BIND = "REFERRAL_BIND";

		// Token: 0x04000458 RID: 1112
		public const string REFERRAL_BIND_MATCH = "REFERRAL_BIND_MATCH";

		// Token: 0x04000459 RID: 1113
		public const string NO_DUP_REQUEST = "NO_DUP_REQUEST";

		// Token: 0x0400045A RID: 1114
		public const string SERVER_CONNECT_ERROR = "SERVER_CONNECT_ERROR";

		// Token: 0x0400045B RID: 1115
		public const string NO_SUP_PROPERTY = "NO_SUP_PROPERTY";

		// Token: 0x0400045C RID: 1116
		public const string ENTRY_PARAM_ERROR = "ENTRY_PARAM_ERROR";

		// Token: 0x0400045D RID: 1117
		public const string DN_PARAM_ERROR = "DN_PARAM_ERROR";

		// Token: 0x0400045E RID: 1118
		public const string RDN_PARAM_ERROR = "RDN_PARAM_ERROR";

		// Token: 0x0400045F RID: 1119
		public const string OP_PARAM_ERROR = "OP_PARAM_ERROR";

		// Token: 0x04000460 RID: 1120
		public const string PARAM_ERROR = "PARAM_ERROR";

		// Token: 0x04000461 RID: 1121
		public const string DECODING_ERROR = "DECODING_ERROR";

		// Token: 0x04000462 RID: 1122
		public const string ENCODING_ERROR = "ENCODING_ERROR";

		// Token: 0x04000463 RID: 1123
		public const string IO_EXCEPTION = "IO_EXCEPTION";

		// Token: 0x04000464 RID: 1124
		public const string INVALID_ESCAPE = "INVALID_ESCAPE";

		// Token: 0x04000465 RID: 1125
		public const string SHORT_ESCAPE = "SHORT_ESCAPE";

		// Token: 0x04000466 RID: 1126
		public const string INVALID_CHAR_IN_FILTER = "INVALID_CHAR_IN_FILTER";

		// Token: 0x04000467 RID: 1127
		public const string INVALID_CHAR_IN_DESCR = "INVALID_CHAR_IN_DESCR";

		// Token: 0x04000468 RID: 1128
		public const string INVALID_ESC_IN_DESCR = "INVALID_ESC_IN_DESCR";

		// Token: 0x04000469 RID: 1129
		public const string UNEXPECTED_END = "UNEXPECTED_END";

		// Token: 0x0400046A RID: 1130
		public const string MISSING_LEFT_PAREN = "MISSING_LEFT_PAREN";

		// Token: 0x0400046B RID: 1131
		public const string MISSING_RIGHT_PAREN = "MISSING_RIGHT_PAREN";

		// Token: 0x0400046C RID: 1132
		public const string EXPECTING_RIGHT_PAREN = "EXPECTING_RIGHT_PAREN";

		// Token: 0x0400046D RID: 1133
		public const string EXPECTING_LEFT_PAREN = "EXPECTING_LEFT_PAREN";

		// Token: 0x0400046E RID: 1134
		public const string NO_OPTION = "NO_OPTION";

		// Token: 0x0400046F RID: 1135
		public const string INVALID_FILTER_COMPARISON = "INVALID_FILTER_COMPARISON";

		// Token: 0x04000470 RID: 1136
		public const string NO_MATCHING_RULE = "NO_MATCHING_RULE";

		// Token: 0x04000471 RID: 1137
		public const string NO_ATTRIBUTE_NAME = "NO_ATTRIBUTE_NAME";

		// Token: 0x04000472 RID: 1138
		public const string NO_DN_NOR_MATCHING_RULE = "NO_DN_NOR_MATCHING_RULE";

		// Token: 0x04000473 RID: 1139
		public const string NOT_AN_ATTRIBUTE = "NOT_AN_ATTRIBUTE";

		// Token: 0x04000474 RID: 1140
		public const string UNEQUAL_LENGTHS = "UNEQUAL_LENGTHS";

		// Token: 0x04000475 RID: 1141
		public const string IMPROPER_REFERRAL = "IMPROPER_REFERRAL";

		// Token: 0x04000476 RID: 1142
		public const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";

		// Token: 0x04000477 RID: 1143
		public const string NO_MEMORY = "NO_MEMORY";

		// Token: 0x04000478 RID: 1144
		public const string SERVER_SHUTDOWN_REQ = "SERVER_SHUTDOWN_REQ";

		// Token: 0x04000479 RID: 1145
		public const string INVALID_ADDRESS = "INVALID_ADDRESS";

		// Token: 0x0400047A RID: 1146
		public const string UNKNOWN_RESULT = "UNKNOWN_RESULT";

		// Token: 0x0400047B RID: 1147
		public const string OUTSTANDING_OPERATIONS = "OUTSTANDING_OPERATIONS";

		// Token: 0x0400047C RID: 1148
		public const string WRONG_FACTORY = "WRONG_FACTORY";

		// Token: 0x0400047D RID: 1149
		public const string NO_TLS_FACTORY = "NO_TLS_FACTORY";

		// Token: 0x0400047E RID: 1150
		public const string NO_STARTTLS = "NO_STARTTLS";

		// Token: 0x0400047F RID: 1151
		public const string STOPTLS_ERROR = "STOPTLS_ERROR";

		// Token: 0x04000480 RID: 1152
		public const string MULTIPLE_SCHEMA = "MULTIPLE_SCHEMA";

		// Token: 0x04000481 RID: 1153
		public const string NO_SCHEMA = "NO_SCHEMA";

		// Token: 0x04000482 RID: 1154
		public const string READ_MULTIPLE = "READ_MULTIPLE";

		// Token: 0x04000483 RID: 1155
		public const string CANNOT_BIND = "CANNOT_BIND";

		// Token: 0x04000484 RID: 1156
		public const string SSL_PROVIDER_MISSING = "SSL_PROVIDER_MISSING";

		// Token: 0x04000485 RID: 1157
		internal static readonly object[][] contents = new object[][]
		{
			new object[]
			{
				"TOSTRING",
				"{0}: {1} ({2}) {3}"
			},
			new object[]
			{
				"SERVER_MSG",
				"{0}: Server Message: {1}"
			},
			new object[]
			{
				"MATCHED_DN",
				"{0}: Matched DN: {1}"
			},
			new object[]
			{
				"FAILED_REFERRAL",
				"{0}: Failed Referral: {1}"
			},
			new object[]
			{
				"REFERRAL_ITEM",
				"{0}: Referral: {1}"
			},
			new object[]
			{
				"CONNECTION_ERROR",
				"Unable to connect to server {0}:{1}"
			},
			new object[]
			{
				"CONNECTION_IMPOSSIBLE",
				"Unable to reconnect to server, application has never called connect()"
			},
			new object[]
			{
				"CONNECTION_WAIT",
				"Connection lost waiting for results from {0}:{1}"
			},
			new object[]
			{
				"CONNECTION_FINALIZED",
				"Connection closed by the application finalizing the object"
			},
			new object[]
			{
				"CONNECTION_CLOSED",
				"Connection closed by the application disconnecting"
			},
			new object[]
			{
				"CONNECTION_READER",
				"Reader thread terminated"
			},
			new object[]
			{
				"DUP_ERROR",
				"RfcLdapMessage: Cannot duplicate message built from the input stream"
			},
			new object[]
			{
				"REFERENCE_ERROR",
				"Error attempting to follow a search continuation reference"
			},
			new object[]
			{
				"REFERRAL_ERROR",
				"Error attempting to follow a referral"
			},
			new object[]
			{
				"REFERRAL_LOCAL",
				"LdapSearchResults.{0}(): No entry found & request is not complete"
			},
			new object[]
			{
				"REFERRAL_SEND",
				"Error sending request to referred server"
			},
			new object[]
			{
				"REFERENCE_NOFOLLOW",
				"Search result reference received, and referral following is off"
			},
			new object[]
			{
				"REFERRAL_BIND",
				"LdapBind.bind() function returned null"
			},
			new object[]
			{
				"REFERRAL_BIND_MATCH",
				"Could not match LdapBind.bind() connection with Server Referral URL list"
			},
			new object[]
			{
				"NO_DUP_REQUEST",
				"Cannot duplicate message to follow referral for {0} request, not allowed"
			},
			new object[]
			{
				"SERVER_CONNECT_ERROR",
				"Error connecting to server {0} while attempting to follow a referral"
			},
			new object[]
			{
				"NO_SUP_PROPERTY",
				"Requested property is not supported."
			},
			new object[]
			{
				"ENTRY_PARAM_ERROR",
				"Invalid Entry parameter"
			},
			new object[]
			{
				"DN_PARAM_ERROR",
				"Invalid DN parameter"
			},
			new object[]
			{
				"RDN_PARAM_ERROR",
				"Invalid DN or RDN parameter"
			},
			new object[]
			{
				"OP_PARAM_ERROR",
				"Invalid extended operation parameter, no OID specified"
			},
			new object[]
			{
				"PARAM_ERROR",
				"Invalid parameter"
			},
			new object[]
			{
				"DECODING_ERROR",
				"Error Decoding responseValue"
			},
			new object[]
			{
				"ENCODING_ERROR",
				"Encoding Error"
			},
			new object[]
			{
				"IO_EXCEPTION",
				"I/O Exception on host {0}, port {1}"
			},
			new object[]
			{
				"INVALID_ESCAPE",
				"Invalid value in escape sequence \"{0}\""
			},
			new object[]
			{
				"SHORT_ESCAPE",
				"Incomplete escape sequence"
			},
			new object[]
			{
				"UNEXPECTED_END",
				"Unexpected end of filter"
			},
			new object[]
			{
				"MISSING_LEFT_PAREN",
				"Unmatched parentheses, left parenthesis missing"
			},
			new object[]
			{
				"NO_OPTION",
				"Semicolon present, but no option specified"
			},
			new object[]
			{
				"MISSING_RIGHT_PAREN",
				"Unmatched parentheses, right parenthesis missing"
			},
			new object[]
			{
				"EXPECTING_RIGHT_PAREN",
				"Expecting right parenthesis, found \"{0}\""
			},
			new object[]
			{
				"EXPECTING_LEFT_PAREN",
				"Expecting left parenthesis, found \"{0}\""
			},
			new object[]
			{
				"NO_ATTRIBUTE_NAME",
				"Missing attribute description"
			},
			new object[]
			{
				"NO_DN_NOR_MATCHING_RULE",
				"DN and matching rule not specified"
			},
			new object[]
			{
				"NO_MATCHING_RULE",
				"Missing matching rule"
			},
			new object[]
			{
				"INVALID_FILTER_COMPARISON",
				"Invalid comparison operator"
			},
			new object[]
			{
				"INVALID_CHAR_IN_FILTER",
				"The invalid character \"{0}\" needs to be escaped as \"{1}\""
			},
			new object[]
			{
				"INVALID_ESC_IN_DESCR",
				"Escape sequence not allowed in attribute description"
			},
			new object[]
			{
				"INVALID_CHAR_IN_DESCR",
				"Invalid character \"{0}\" in attribute description"
			},
			new object[]
			{
				"NOT_AN_ATTRIBUTE",
				"Schema element is not an LdapAttributeSchema object"
			},
			new object[]
			{
				"UNEQUAL_LENGTHS",
				"Length of attribute Name array does not equal length of Flags array"
			},
			new object[]
			{
				"IMPROPER_REFERRAL",
				"Referral not supported for command {0}"
			},
			new object[]
			{
				"NOT_IMPLEMENTED",
				"Method LdapConnection.startTLS not implemented"
			},
			new object[]
			{
				"NO_MEMORY",
				"All results could not be stored in memory, sort failed"
			},
			new object[]
			{
				"SERVER_SHUTDOWN_REQ",
				"Received unsolicited notification from server {0}:{1} to shutdown"
			},
			new object[]
			{
				"INVALID_ADDRESS",
				"Invalid syntax for address with port; {0}"
			},
			new object[]
			{
				"UNKNOWN_RESULT",
				"Unknown Ldap result code {0}"
			},
			new object[]
			{
				"OUTSTANDING_OPERATIONS",
				"Cannot start or stop TLS because outstanding Ldap operations exist on this connection"
			},
			new object[]
			{
				"WRONG_FACTORY",
				"StartTLS cannot use the set socket factory because it does not implement LdapTLSSocketFactory"
			},
			new object[]
			{
				"NO_TLS_FACTORY",
				"StartTLS failed because no LdapTLSSocketFactory has been set for this Connection"
			},
			new object[]
			{
				"NO_STARTTLS",
				"An attempt to stopTLS on a connection where startTLS had not been called"
			},
			new object[]
			{
				"STOPTLS_ERROR",
				"Error stopping TLS: Error getting input & output streams from the original socket"
			},
			new object[]
			{
				"MULTIPLE_SCHEMA",
				"Multiple schema found when reading the subschemaSubentry for {0}"
			},
			new object[]
			{
				"NO_SCHEMA",
				"No schema found when reading the subschemaSubentry for {0}"
			},
			new object[]
			{
				"READ_MULTIPLE",
				"Read response is ambiguous, multiple entries returned"
			},
			new object[]
			{
				"CANNOT_BIND",
				"Cannot bind. Use PoolManager.getBoundConnection()"
			}
		};
	}
}
