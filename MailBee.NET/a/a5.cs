using System;
using MailBee;

namespace a
{
	// Token: 0x02000491 RID: 1169
	internal class a5
	{
		// Token: 0x06002832 RID: 10290 RVA: 0x000BAF9C File Offset: 0x000B9F9C
		private a5()
		{
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x000BAFA4 File Offset: 0x000B9FA4
		public static string a(int A_0)
		{
			if (A_0 <= 624)
			{
				if (A_0 <= 330)
				{
					if (A_0 <= 200)
					{
						switch (A_0)
						{
						case 0:
							return Resources.Instance.ErrorDesc_OK;
						case 1:
							return Resources.Instance.ErrorDesc_0ComponentNotLicensed;
						case 2:
							return Resources.Instance.ErrorDesc_Unknown;
						case 3:
							return Resources.Instance.ErrorDesc_Busy;
						case 4:
							return Resources.Instance.ErrorDesc_NoOperationToEnd;
						case 5:
							return Resources.Instance.ErrorDesc_AbortedByUser;
						case 6:
							return Resources.Instance.ErrorDesc_InternalError;
						case 7:
							return Resources.Instance.ErrorDesc_ExternalError;
						case 8:
							return Resources.Instance.ErrorDesc_IllegalInMultiThreadMode;
						case 9:
							return Resources.Instance.ErrorDesc_IllegalContext;
						case 10:
							return Resources.Instance.ErrorDesc_IllegalRaiseEventsMode;
						case 11:
							return Resources.Instance.ErrorDesc_IllegalInCurrentState;
						case 12:
							return Resources.Instance.ErrorDesc_ObjectReadOnly;
						case 13:
							return Resources.Instance.ErrorDesc_JobInBatchFailed;
						case 14:
						case 15:
						case 16:
						case 17:
						case 18:
						case 19:
						case 24:
						case 25:
						case 26:
						case 27:
						case 28:
						case 29:
						case 37:
						case 38:
						case 39:
						case 46:
						case 47:
						case 48:
						case 49:
						case 62:
						case 63:
						case 64:
						case 65:
						case 66:
						case 67:
						case 68:
						case 69:
						case 74:
						case 75:
						case 76:
						case 77:
						case 78:
						case 79:
						case 80:
						case 81:
						case 82:
						case 83:
						case 84:
						case 85:
						case 86:
						case 87:
						case 88:
						case 89:
						case 90:
						case 91:
						case 92:
						case 93:
						case 94:
						case 95:
						case 96:
						case 97:
						case 98:
						case 99:
						case 103:
						case 104:
						case 105:
						case 106:
						case 107:
						case 108:
						case 109:
						case 118:
						case 119:
						case 127:
						case 128:
						case 129:
							break;
						case 20:
							return Resources.Instance.ErrorDesc_ArgumentInvalid;
						case 21:
							return Resources.Instance.ErrorDesc_ArgumentNull;
						case 22:
							return Resources.Instance.ErrorDesc_ArgumentEmpty;
						case 23:
							return Resources.Instance.ErrorDesc_ArgumentOutOfRange;
						case 30:
							return Resources.Instance.ErrorDesc_IOException;
						case 31:
							return Resources.Instance.ErrorDesc_FileNotFoundException;
						case 32:
							return Resources.Instance.ErrorDesc_UnauthorizedAccessException;
						case 33:
							return Resources.Instance.ErrorDesc_XmlException;
						case 34:
							return Resources.Instance.ErrorDesc_WebException;
						case 35:
							return Resources.Instance.ErrorDesc_CryptographicException;
						case 36:
							return Resources.Instance.ErrorDesc_InvalidOperationException;
						case 40:
							return Resources.Instance.ErrorDesc_StreamCannotRead;
						case 41:
							return Resources.Instance.ErrorDesc_StreamCannotWrite;
						case 42:
							return Resources.Instance.ErrorDesc_EncodingError;
						case 43:
							return Resources.Instance.ErrorDesc_InvalidDateFormat;
						case 44:
							return Resources.Instance.ErrorDesc_InvalidDataFormat;
						case 45:
							return Resources.Instance.ErrorDesc_InvalidDataSyntax;
						case 50:
							return Resources.Instance.ErrorDesc_SocketException;
						case 51:
							return Resources.Instance.ErrorDesc_NoIP4AddressesFound;
						case 52:
							return Resources.Instance.ErrorDesc_SocketTimeout;
						case 53:
							return Resources.Instance.ErrorDesc_AbortedByLocalHost;
						case 54:
							return Resources.Instance.ErrorDesc_ConnectionRefused;
						case 55:
							return Resources.Instance.ErrorDesc_AbortedByServer;
						case 56:
							return Resources.Instance.ErrorDesc_HostNotFound;
						case 57:
							return Resources.Instance.ErrorDesc_HostDown;
						case 58:
							return Resources.Instance.ErrorDesc_HostUnreachable;
						case 59:
							return Resources.Instance.ErrorDesc_ConnectionReset;
						case 60:
							return Resources.Instance.ErrorDesc_SocketDisposed;
						case 61:
							return Resources.Instance.ErrorDesc_ResponseTimeout;
						case 70:
							return Resources.Instance.ErrorDesc_ProxyConnectionDeclined;
						case 71:
							return Resources.Instance.ErrorDesc_ProxyAuthMethodsNotAccepted;
						case 72:
							return Resources.Instance.ErrorDesc_ProxyUserPassAuthFailed;
						case 73:
							return Resources.Instance.ErrorDesc_ProxyGssApiAuthFailed;
						case 100:
							return Resources.Instance.ErrorDesc_NotConnected;
						case 101:
							return Resources.Instance.ErrorDesc_AlreadyConnected;
						case 102:
							return Resources.Instance.ErrorDesc_AlreadySsl;
						case 110:
							return Resources.Instance.ErrorDesc_NotLoggedIn;
						case 111:
							return Resources.Instance.ErrorDesc_AlreadyLoggedIn;
						case 112:
							return Resources.Instance.ErrorDesc_NoCredentials;
						case 113:
							return Resources.Instance.ErrorDesc_BadCredentials;
						case 114:
							return Resources.Instance.ErrorDesc_UnsupportedLoginMethod;
						case 115:
							return Resources.Instance.ErrorDesc_NoSupportedLoginMethods;
						case 116:
							return Resources.Instance.ErrorDesc_SaslAnswerRejected;
						case 117:
							return Resources.Instance.ErrorDesc_LoginWin32Error;
						case 120:
							return Resources.Instance.ErrorDesc_NegativeResponse;
						case 121:
							return Resources.Instance.ErrorDesc_InvalidResponse;
						case 122:
							return Resources.Instance.ErrorDesc_EmptyResponseLine;
						case 123:
							return Resources.Instance.ErrorDesc_InvalidBinaryResponse;
						case 124:
							return Resources.Instance.ErrorDesc_InconsistentResponse;
						case 125:
							return Resources.Instance.ErrorDesc_InvalidResponseItem;
						case 126:
							return Resources.Instance.ErrorDesc_InvalidBase64DataInResponse;
						case 130:
							return Resources.Instance.ErrorDesc_StartTlsNotAvailable;
						default:
							switch (A_0)
							{
							case 140:
								return Resources.Instance.ErrorDesc_SslWin32Error;
							case 141:
								return Resources.Instance.ErrorDesc_SslNegoIOException;
							case 142:
								return Resources.Instance.ErrorDesc_SslNegoNotSupportedException;
							case 143:
								return Resources.Instance.ErrorDesc_SslNegoAuthException;
							case 144:
								return Resources.Instance.ErrorDesc_SslNegoException;
							default:
								if (A_0 == 200)
								{
									return Resources.Instance.ErrorDesc_BadDnsResponseHeader;
								}
								break;
							}
							break;
						}
					}
					else if (A_0 <= 225)
					{
						if (A_0 == 201)
						{
							return Resources.Instance.ErrorDesc_DnsInvalidQName;
						}
						switch (A_0)
						{
						case 210:
							return Resources.Instance.ErrorDesc_DnsQueryMismatch;
						case 211:
							return Resources.Instance.ErrorDesc_DnsRecursionRequired;
						case 212:
							return Resources.Instance.ErrorDesc_NoDnsServersSpecified;
						case 213:
							return Resources.Instance.ErrorDesc_NoDnsAttemptsLeft;
						case 214:
							return Resources.Instance.ErrorDesc_NoDnsServersAvailable;
						case 220:
							return Resources.Instance.ErrorDesc_DnsFormatErrorReply;
						case 221:
							return Resources.Instance.ErrorDesc_DnsFailureReply;
						case 222:
							return Resources.Instance.ErrorDesc_DnsNameErrorReply;
						case 223:
							return Resources.Instance.ErrorDesc_DnsNotSupportedReply;
						case 224:
							return Resources.Instance.ErrorDesc_DnsQueryRefusedReply;
						case 225:
							return Resources.Instance.ErrorDesc_DnsUnknownReply;
						}
					}
					else
					{
						switch (A_0)
						{
						case 300:
							return Resources.Instance.ErrorDesc_NoSmtpServersSpecified;
						case 301:
						case 302:
						case 303:
						case 304:
						case 305:
						case 306:
						case 307:
						case 308:
						case 309:
							break;
						case 310:
							return Resources.Instance.ErrorDesc_NotHelloed;
						case 311:
							return Resources.Instance.ErrorDesc_EhloNotSupported;
						case 312:
							return Resources.Instance.ErrorDesc_NoSender;
						case 313:
							return Resources.Instance.ErrorDesc_BadSender;
						case 314:
							return Resources.Instance.ErrorDesc_NoRecipients;
						case 315:
							return Resources.Instance.ErrorDesc_BadRecipient;
						case 316:
							return Resources.Instance.ErrorDesc_NoAllowedRecipients;
						case 317:
							return Resources.Instance.ErrorDesc_DataNotAllowed;
						case 318:
							return Resources.Instance.ErrorDesc_BadMessageData;
						case 319:
							return Resources.Instance.ErrorDesc_BdatRejected;
						case 320:
							return Resources.Instance.ErrorDesc_MessageDataTooLarge;
						default:
							if (A_0 == 330)
							{
								return Resources.Instance.ErrorDesc_Conversion8bitTo7Bit;
							}
							break;
						}
					}
				}
				else if (A_0 <= 411)
				{
					switch (A_0)
					{
					case 400:
						return Resources.Instance.ErrorDesc_NoSmtpOrDnsServersSpecified;
					case 401:
						return Resources.Instance.ErrorDesc_NoDomainsToSendFor;
					case 402:
						return Resources.Instance.ErrorDesc_NoDomainsForMXLookup;
					case 403:
						return Resources.Instance.ErrorDesc_EmptyHostNameForDnsQuery;
					default:
						if (A_0 == 410)
						{
							return Resources.Instance.ErrorDesc_InvalidDomain0InCache;
						}
						if (A_0 == 411)
						{
							return Resources.Instance.ErrorDesc_DeadDomain0SmtpMXesInCache;
						}
						break;
					}
				}
				else if (A_0 <= 501)
				{
					if (A_0 == 500)
					{
						return Resources.Instance.ErrorDesc_Pop3AuthCommandUnknown;
					}
					if (A_0 == 501)
					{
						return Resources.Instance.ErrorDesc_Pop3CapaCommandUnknown;
					}
				}
				else
				{
					if (A_0 == 600)
					{
						return Resources.Instance.ErrorDesc_ImapFolderNotSelected;
					}
					switch (A_0)
					{
					case 610:
						return Resources.Instance.ErrorDesc_ImapResponseNotFound;
					case 611:
						return Resources.Instance.ErrorDesc_ImapMessageIndexNotFound;
					case 612:
						return Resources.Instance.ErrorDesc_NoMessageInImapResponse;
					case 613:
						return Resources.Instance.ErrorDesc_InvalidImapEnvelope;
					case 620:
						return Resources.Instance.ErrorDesc_ImapUidPlusNotSupported;
					case 621:
						return Resources.Instance.ErrorDesc_ImapQuotaNotSupported;
					case 622:
						return Resources.Instance.ErrorDesc_ImapIdleNotSupported;
					case 623:
						return Resources.Instance.ErrorDesc_ImapSortNotSupported;
					case 624:
						return Resources.Instance.ErrorDesc_ImapNamespaceNotSupported;
					}
				}
			}
			else if (A_0 <= 1110)
			{
				if (A_0 <= 710)
				{
					if (A_0 == 700)
					{
						return Resources.Instance.ErrorDesc_EwsLocalException;
					}
					if (A_0 == 701)
					{
						return Resources.Instance.ErrorDesc_EwsRemoteException;
					}
					if (A_0 == 710)
					{
						return Resources.Instance.ErrorDesc_EwsFolderNotFound;
					}
				}
				else if (A_0 <= 1010)
				{
					if (A_0 == 711)
					{
						return Resources.Instance.ErrorDesc_EwsFolderAlreadyExists;
					}
					switch (A_0)
					{
					case 1000:
						return Resources.Instance.ErrorDesc_TnefSignature0Invalid;
					case 1001:
						return Resources.Instance.ErrorDesc_TnefUnexpectedEndOfStream;
					case 1002:
						return Resources.Instance.ErrorDesc_TnefLevelTypeInvalid0;
					case 1003:
						return Resources.Instance.ErrorDesc_TnefAttributeChecksumInvalid;
					case 1004:
						return Resources.Instance.ErrorDesc_TnefAttributeLevelInvalid0;
					case 1005:
						return Resources.Instance.ErrorDesc_TnefMapiTypeUnknown0;
					case 1006:
						return Resources.Instance.ErrorDesc_TnefGuidInvalid0;
					case 1007:
						return Resources.Instance.ErrorDesc_TnefCompressedRtfHeaderInvalid;
					case 1008:
						return Resources.Instance.ErrorDesc_TnefCompressedRtfDataSizeMismatch;
					case 1009:
						return Resources.Instance.ErrorDesc_TnefCompressedRtfCrc32Failed;
					case 1010:
						return Resources.Instance.ErrorDesc_TnefUnknownRtfCompressionType0;
					}
				}
				else
				{
					switch (A_0)
					{
					case 1100:
						return Resources.Instance.ErrorDesc_CryptoProviderWin32Error;
					case 1101:
						return Resources.Instance.ErrorDesc_CertificateStoreWin32Error;
					case 1102:
						return Resources.Instance.ErrorDesc_CertificateWin32Error;
					case 1103:
						return Resources.Instance.ErrorDesc_SmimeWin32Error;
					default:
						if (A_0 == 1110)
						{
							return Resources.Instance.ErrorDesc_CertificateValidationError;
						}
						break;
					}
				}
			}
			else if (A_0 <= 1121)
			{
				if (A_0 == 1111)
				{
					return Resources.Instance.ErrorDesc_CertificateDataInvalid;
				}
				if (A_0 == 1120)
				{
					return Resources.Instance.ErrorDesc_NotImpersonated;
				}
				if (A_0 == 1121)
				{
					return Resources.Instance.ErrorDesc_ImpersonationWin32Error;
				}
			}
			else if (A_0 <= 1200)
			{
				if (A_0 == 1130)
				{
					return Resources.Instance.ErrorDesc_CryptoPrivateKeyInvalid;
				}
				if (A_0 == 1200)
				{
					return Resources.Instance.ErrorDesc_OleDocParsingError;
				}
			}
			else
			{
				if (A_0 == 1201)
				{
					return Resources.Instance.ErrorDesc_OleDocBuildingError;
				}
				if (A_0 == 1210)
				{
					return Resources.Instance.ErrorDesc_OutlookPstParsingError;
				}
			}
			return "UNDOCUMENTED ERROR. Please contact AfterLogic support team for resolving this issue.";
		}
	}
}
