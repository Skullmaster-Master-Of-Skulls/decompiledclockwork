using System;

namespace MailBee
{
	// Token: 0x0200001D RID: 29
	public class ErrorCodes
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00007714 File Offset: 0x00006714
		private ErrorCodes()
		{
		}

		// Token: 0x040000A2 RID: 162
		public const int OK = 0;

		// Token: 0x040000A3 RID: 163
		public const int LicenseError = 1;

		// Token: 0x040000A4 RID: 164
		public const int Unknown = 2;

		// Token: 0x040000A5 RID: 165
		public const int Busy = 3;

		// Token: 0x040000A6 RID: 166
		public const int NoOperationToEnd = 4;

		// Token: 0x040000A7 RID: 167
		public const int AbortedByUser = 5;

		// Token: 0x040000A8 RID: 168
		public const int InternalError = 6;

		// Token: 0x040000A9 RID: 169
		public const int ExternalError = 7;

		// Token: 0x040000AA RID: 170
		public const int IllegalInMultiThreadMode = 8;

		// Token: 0x040000AB RID: 171
		public const int IllegalContext = 9;

		// Token: 0x040000AC RID: 172
		public const int IllegalRaiseEventsMode = 10;

		// Token: 0x040000AD RID: 173
		public const int IllegalInCurrentState = 11;

		// Token: 0x040000AE RID: 174
		public const int ObjectReadOnly = 12;

		// Token: 0x040000AF RID: 175
		public const int JobInBatchFailed = 13;

		// Token: 0x040000B0 RID: 176
		public const int ArgumentInvalid = 20;

		// Token: 0x040000B1 RID: 177
		public const int ArgumentNull = 21;

		// Token: 0x040000B2 RID: 178
		public const int ArgumentEmpty = 22;

		// Token: 0x040000B3 RID: 179
		public const int ArgumentOutOfRange = 23;

		// Token: 0x040000B4 RID: 180
		public const int IOException = 30;

		// Token: 0x040000B5 RID: 181
		public const int FileNotFoundException = 31;

		// Token: 0x040000B6 RID: 182
		public const int UnauthorizedAccessException = 32;

		// Token: 0x040000B7 RID: 183
		public const int XmlException = 33;

		// Token: 0x040000B8 RID: 184
		public const int WebException = 34;

		// Token: 0x040000B9 RID: 185
		public const int CryptographicException = 35;

		// Token: 0x040000BA RID: 186
		public const int InvalidOperationException = 36;

		// Token: 0x040000BB RID: 187
		public const int StreamCannotRead = 40;

		// Token: 0x040000BC RID: 188
		public const int StreamCannotWrite = 41;

		// Token: 0x040000BD RID: 189
		public const int EncodingError = 42;

		// Token: 0x040000BE RID: 190
		public const int InvalidDateFormat = 43;

		// Token: 0x040000BF RID: 191
		public const int InvalidDataFormat = 44;

		// Token: 0x040000C0 RID: 192
		public const int InvalidDataSyntax = 45;

		// Token: 0x040000C1 RID: 193
		public const int SocketException = 50;

		// Token: 0x040000C2 RID: 194
		public const int NoIP4AddressesFound = 51;

		// Token: 0x040000C3 RID: 195
		public const int SocketTimeout = 52;

		// Token: 0x040000C4 RID: 196
		public const int AbortedByLocalHost = 53;

		// Token: 0x040000C5 RID: 197
		public const int ConnectionRefused = 54;

		// Token: 0x040000C6 RID: 198
		public const int AbortedByServer = 55;

		// Token: 0x040000C7 RID: 199
		public const int HostNotFound = 56;

		// Token: 0x040000C8 RID: 200
		public const int HostDown = 57;

		// Token: 0x040000C9 RID: 201
		public const int HostUnreachable = 58;

		// Token: 0x040000CA RID: 202
		public const int ConnectionReset = 59;

		// Token: 0x040000CB RID: 203
		public const int SocketDisposed = 60;

		// Token: 0x040000CC RID: 204
		public const int ResponseTimeout = 61;

		// Token: 0x040000CD RID: 205
		public const int ProxyConnectionDeclined = 70;

		// Token: 0x040000CE RID: 206
		public const int ProxyAuthMethodsNotAccepted = 71;

		// Token: 0x040000CF RID: 207
		public const int ProxyUserPassAuthFailed = 72;

		// Token: 0x040000D0 RID: 208
		public const int ProxyGssApiAuthFailed = 73;

		// Token: 0x040000D1 RID: 209
		public const int NotConnected = 100;

		// Token: 0x040000D2 RID: 210
		public const int AlreadyConnected = 101;

		// Token: 0x040000D3 RID: 211
		public const int AlreadySsl = 102;

		// Token: 0x040000D4 RID: 212
		public const int NotLoggedIn = 110;

		// Token: 0x040000D5 RID: 213
		public const int AlreadyLoggedIn = 111;

		// Token: 0x040000D6 RID: 214
		public const int NoCredentials = 112;

		// Token: 0x040000D7 RID: 215
		public const int BadCredentials = 113;

		// Token: 0x040000D8 RID: 216
		public const int UnsupportedLoginMethod = 114;

		// Token: 0x040000D9 RID: 217
		public const int NoSupportedLoginMethods = 115;

		// Token: 0x040000DA RID: 218
		public const int SaslAnswerRejected = 116;

		// Token: 0x040000DB RID: 219
		public const int LoginWin32Error = 117;

		// Token: 0x040000DC RID: 220
		public const int NegativeResponse = 120;

		// Token: 0x040000DD RID: 221
		public const int InvalidResponse = 121;

		// Token: 0x040000DE RID: 222
		public const int EmptyResponseLine = 122;

		// Token: 0x040000DF RID: 223
		public const int InvalidBinaryResponse = 123;

		// Token: 0x040000E0 RID: 224
		public const int InconsistentResponse = 124;

		// Token: 0x040000E1 RID: 225
		public const int InvalidResponseItem = 125;

		// Token: 0x040000E2 RID: 226
		public const int InvalidBase64DataInResponse = 126;

		// Token: 0x040000E3 RID: 227
		public const int StartTlsNotAvailable = 130;

		// Token: 0x040000E4 RID: 228
		public const int SslWin32Error = 140;

		// Token: 0x040000E5 RID: 229
		public const int SslNegoIOException = 141;

		// Token: 0x040000E6 RID: 230
		public const int SslNegoNotSupportedException = 142;

		// Token: 0x040000E7 RID: 231
		public const int SslNegoAuthException = 143;

		// Token: 0x040000E8 RID: 232
		public const int SslNegoException = 144;

		// Token: 0x040000E9 RID: 233
		public const int BadDnsResponseHeader = 200;

		// Token: 0x040000EA RID: 234
		public const int DnsInvalidQName = 201;

		// Token: 0x040000EB RID: 235
		public const int DnsQueryMismatch = 210;

		// Token: 0x040000EC RID: 236
		public const int DnsRecursionRequired = 211;

		// Token: 0x040000ED RID: 237
		public const int NoDnsServersSpecified = 212;

		// Token: 0x040000EE RID: 238
		public const int NoDnsAttemptsLeft = 213;

		// Token: 0x040000EF RID: 239
		public const int NoDnsServersAvailable = 214;

		// Token: 0x040000F0 RID: 240
		public const int DnsFormatErrorReply = 220;

		// Token: 0x040000F1 RID: 241
		public const int DnsFailureReply = 221;

		// Token: 0x040000F2 RID: 242
		public const int DnsNameErrorReply = 222;

		// Token: 0x040000F3 RID: 243
		public const int DnsNotSupportedReply = 223;

		// Token: 0x040000F4 RID: 244
		public const int DnsQueryRefusedReply = 224;

		// Token: 0x040000F5 RID: 245
		public const int DnsUnknownReply = 225;

		// Token: 0x040000F6 RID: 246
		public const int NoSmtpServersSpecified = 300;

		// Token: 0x040000F7 RID: 247
		public const int NotHelloed = 310;

		// Token: 0x040000F8 RID: 248
		public const int EhloNotSupported = 311;

		// Token: 0x040000F9 RID: 249
		public const int NoSender = 312;

		// Token: 0x040000FA RID: 250
		public const int BadSender = 313;

		// Token: 0x040000FB RID: 251
		public const int NoRecipients = 314;

		// Token: 0x040000FC RID: 252
		public const int BadRecipient = 315;

		// Token: 0x040000FD RID: 253
		public const int NoAllowedRecipients = 316;

		// Token: 0x040000FE RID: 254
		public const int DataNotAllowed = 317;

		// Token: 0x040000FF RID: 255
		public const int BadMessageData = 318;

		// Token: 0x04000100 RID: 256
		public const int BdatRejected = 319;

		// Token: 0x04000101 RID: 257
		public const int MessageDataTooLarge = 320;

		// Token: 0x04000102 RID: 258
		public const int Conversion8bitTo7Bit = 330;

		// Token: 0x04000103 RID: 259
		public const int NoSmtpOrDnsServersSpecified = 400;

		// Token: 0x04000104 RID: 260
		public const int NoDomainsToSendFor = 401;

		// Token: 0x04000105 RID: 261
		public const int NoDomainsForMXLookup = 402;

		// Token: 0x04000106 RID: 262
		public const int EmptyHostNameForDnsQuery = 403;

		// Token: 0x04000107 RID: 263
		public const int InvalidDomainInCache = 410;

		// Token: 0x04000108 RID: 264
		public const int DeadDomainSmtpMXesInCache = 411;

		// Token: 0x04000109 RID: 265
		public const int Pop3AuthCommandUnknown = 500;

		// Token: 0x0400010A RID: 266
		public const int Pop3CapaCommandUnknown = 501;

		// Token: 0x0400010B RID: 267
		public const int ImapFolderNotSelected = 600;

		// Token: 0x0400010C RID: 268
		public const int ImapResponseNotFound = 610;

		// Token: 0x0400010D RID: 269
		public const int ImapMessageIndexNotFound = 611;

		// Token: 0x0400010E RID: 270
		public const int NoMessageInImapResponse = 612;

		// Token: 0x0400010F RID: 271
		public const int InvalidImapEnvelope = 613;

		// Token: 0x04000110 RID: 272
		public const int ImapUidPlusNotSupported = 620;

		// Token: 0x04000111 RID: 273
		public const int ImapQuotaNotSupported = 621;

		// Token: 0x04000112 RID: 274
		public const int ImapIdleNotSupported = 622;

		// Token: 0x04000113 RID: 275
		public const int ImapSortNotSupported = 623;

		// Token: 0x04000114 RID: 276
		public const int ImapNamespaceNotSupported = 624;

		// Token: 0x04000115 RID: 277
		public const int EwsLocalException = 700;

		// Token: 0x04000116 RID: 278
		public const int EwsRemoteException = 701;

		// Token: 0x04000117 RID: 279
		public const int EwsFolderNotFound = 710;

		// Token: 0x04000118 RID: 280
		public const int EwsFolderAlreadyExists = 711;

		// Token: 0x04000119 RID: 281
		public const int TnefSignatureInvalid = 1000;

		// Token: 0x0400011A RID: 282
		public const int TnefUnexpectedEndOfStream = 1001;

		// Token: 0x0400011B RID: 283
		public const int TnefLevelTypeInvalid = 1002;

		// Token: 0x0400011C RID: 284
		public const int TnefAttributeChecksumInvalid = 1003;

		// Token: 0x0400011D RID: 285
		public const int TnefAttributeLevelInvalid = 1004;

		// Token: 0x0400011E RID: 286
		public const int TnefMapiTypeUnknown = 1005;

		// Token: 0x0400011F RID: 287
		public const int TnefGuidInvalid = 1006;

		// Token: 0x04000120 RID: 288
		public const int TnefCompressedRtfHeaderInvalid = 1007;

		// Token: 0x04000121 RID: 289
		public const int TnefCompressedRtfDataSizeMismatch = 1008;

		// Token: 0x04000122 RID: 290
		public const int TnefCompressedRtfCrc32Failed = 1009;

		// Token: 0x04000123 RID: 291
		public const int TnefUnknownRtfCompressionType = 1010;

		// Token: 0x04000124 RID: 292
		public const int TnefMapiPropTypeInvalid = 1011;

		// Token: 0x04000125 RID: 293
		public const int TnefMapiValueInvalid = 1012;

		// Token: 0x04000126 RID: 294
		public const int CryptoProviderWin32Error = 1100;

		// Token: 0x04000127 RID: 295
		public const int CertificateStoreWin32Error = 1101;

		// Token: 0x04000128 RID: 296
		public const int CertificateWin32Error = 1102;

		// Token: 0x04000129 RID: 297
		public const int SmimeWin32Error = 1103;

		// Token: 0x0400012A RID: 298
		public const int CertificateValidationError = 1110;

		// Token: 0x0400012B RID: 299
		public const int CertificateDataInvalid = 1111;

		// Token: 0x0400012C RID: 300
		public const int NotImpersonated = 1120;

		// Token: 0x0400012D RID: 301
		public const int ImpersonationWin32Error = 1121;

		// Token: 0x0400012E RID: 302
		public const int CryptoPrivateKeyInvalid = 1130;

		// Token: 0x0400012F RID: 303
		public const int OleDocParsingError = 1200;

		// Token: 0x04000130 RID: 304
		public const int OleDocBuildingError = 1201;

		// Token: 0x04000131 RID: 305
		public const int OutlookPstParsingError = 1210;
	}
}
