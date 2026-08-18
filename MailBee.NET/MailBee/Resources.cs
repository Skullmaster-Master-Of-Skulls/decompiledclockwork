using System;

namespace MailBee
{
	// Token: 0x02000058 RID: 88
	public class Resources
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000082EE File Offset: 0x000072EE
		// (set) Token: 0x060001DC RID: 476 RVA: 0x000082F5 File Offset: 0x000072F5
		public static Resources Instance
		{
			get
			{
				return Resources.a;
			}
			set
			{
				if (Resources.a == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				Resources.a = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000830C File Offset: 0x0000730C
		public virtual string ErrorDesc_SyncIONotSupported
		{
			get
			{
				return "Sync I/O not supported by this platform. Use async I/O instead.";
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00008313 File Offset: 0x00007313
		public virtual string ErrorDescSuffix_InnerException0
		{
			get
			{
				return " InnerException message follows: {0}";
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000831A File Offset: 0x0000731A
		public virtual string ErrorDescSuffix_ServerResponded0
		{
			get
			{
				return " The server responded: {0}.";
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00008321 File Offset: 0x00007321
		public virtual string ErrorDescSuffix_ResponseString0
		{
			get
			{
				return " The response string: {0}.";
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00008328 File Offset: 0x00007328
		public virtual string ErrorDescSuffix_Win32ErrorCode0Desc1
		{
			get
			{
				return " The Win32 error code is: {0}. The Win32 description is: {1}.";
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000832F File Offset: 0x0000732F
		public virtual string LicenseKeyIsWriteOnlyWarning
		{
			get
			{
				return "Warning: LicenseKey property is write-only.";
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00008336 File Offset: 0x00007336
		public virtual string LicenseKeyInvalid
		{
			get
			{
				return "LicenseKey is invalid. See documentation on MailBee.Global.LicenseKey property of the component for more information.";
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000833D File Offset: 0x0000733D
		public virtual string LicenseKeyTrialExpired
		{
			get
			{
				return "Trial LicenseKey is expired.";
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00008344 File Offset: 0x00007344
		public virtual string LicenseKeyOlderVersion
		{
			get
			{
				return "LicenseKey is for an older version of MailBee.NET components.";
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000834B File Offset: 0x0000734B
		public virtual string LicenseKeyComVersion
		{
			get
			{
				return "LicenseKey is for MailBee COM/ActiveX version rather than MailBee.NET version.";
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00008352 File Offset: 0x00007352
		public virtual string ErrorDesc_OK
		{
			get
			{
				return "No error.";
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008359 File Offset: 0x00007359
		public virtual string ErrorDesc_0ComponentNotLicensed
		{
			get
			{
				return "{0} component not licensed.";
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00008360 File Offset: 0x00007360
		public virtual string ErrorDesc_Unknown
		{
			get
			{
				return "Unknown error.";
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008367 File Offset: 0x00007367
		public virtual string ErrorDesc_Busy
		{
			get
			{
				return "There is already an operation in progress.";
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000836E File Offset: 0x0000736E
		public virtual string ErrorDesc_NoOperationToEnd
		{
			get
			{
				return "There is no such operation in progress.";
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008375 File Offset: 0x00007375
		public virtual string ErrorDesc_AbortedByUser
		{
			get
			{
				return "Processing is aborted by user.";
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000837C File Offset: 0x0000737C
		public virtual string ErrorDesc_InternalError
		{
			get
			{
				return "Internal error occurred.";
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008383 File Offset: 0x00007383
		public virtual string ErrorDesc_ExternalError
		{
			get
			{
				return "User code has thrown an exception.";
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000838A File Offset: 0x0000738A
		public virtual string ErrorDesc_IllegalInMultiThreadMode
		{
			get
			{
				return "This operation is allowed in single-thread mode only.";
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008391 File Offset: 0x00007391
		public virtual string ErrorDesc_IllegalContext
		{
			get
			{
				return "This operation is not allowed in the current context.";
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00008398 File Offset: 0x00007398
		public virtual string ErrorDesc_IllegalRaiseEventsMode
		{
			get
			{
				return "Wait is not allowed unless RaiseEventsViaMessageLoop=false.";
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000839F File Offset: 0x0000739F
		public virtual string ErrorDesc_IllegalInCurrentState
		{
			get
			{
				return "An attempt to call a method of an object in the inappropriate state.";
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000083A6 File Offset: 0x000073A6
		public virtual string ErrorDesc_ObjectReadOnly
		{
			get
			{
				return "An attempt to modify the object state has been made while IsReadOnly is set to true.";
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000083AD File Offset: 0x000073AD
		public virtual string ErrorDesc_JobInBatchFailed
		{
			get
			{
				return "At least one job failed in the batch. Further processing is stopped. Enable/see log file for more information.";
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x000083B4 File Offset: 0x000073B4
		public virtual string ErrorDesc_ArgumentInvalid
		{
			get
			{
				return "Argument value is invalid.";
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x000083BB File Offset: 0x000073BB
		public virtual string ErrorDesc_ArgumentNull
		{
			get
			{
				return "Null argument value is not allowed.";
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000083C2 File Offset: 0x000073C2
		public virtual string ErrorDesc_ArgumentEmpty
		{
			get
			{
				return "Empty or null argument value is not allowed.";
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000083C9 File Offset: 0x000073C9
		public virtual string ErrorDesc_ArgumentOutOfRange
		{
			get
			{
				return "Argument value is out of range.";
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000083D0 File Offset: 0x000073D0
		public virtual string ErrorDesc_IOException
		{
			get
			{
				return "IOException occurred.";
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000083D7 File Offset: 0x000073D7
		public virtual string ErrorDesc_FileNotFoundException
		{
			get
			{
				return "FileNotFoundException occurred.";
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000083DE File Offset: 0x000073DE
		public virtual string ErrorDesc_UnauthorizedAccessException
		{
			get
			{
				return "UnauthorizedAccessException occurred.";
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000083E5 File Offset: 0x000073E5
		public virtual string ErrorDesc_XmlException
		{
			get
			{
				return "XmlException occurred.";
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000083EC File Offset: 0x000073EC
		public virtual string ErrorDesc_WebException
		{
			get
			{
				return "WebException occurred.";
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000083F3 File Offset: 0x000073F3
		public virtual string ErrorDesc_CryptographicException
		{
			get
			{
				return "CryptographicException occurred.";
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000083FA File Offset: 0x000073FA
		public virtual string ErrorDesc_InvalidOperationException
		{
			get
			{
				return "InvalidOperationException occurred.";
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008401 File Offset: 0x00007401
		public virtual string ErrorDesc_StreamCannotRead
		{
			get
			{
				return "The specified stream is closed or write-only or doesn't support seeking (random access).";
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008408 File Offset: 0x00007408
		public virtual string ErrorDesc_StreamCannotWrite
		{
			get
			{
				return "The specified stream is closed or read-only or doesn't support seeking (random access).";
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000840F File Offset: 0x0000740F
		public virtual string ErrorDesc_EncodingError
		{
			get
			{
				return "Error occurred during string encoding conversion.";
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00008416 File Offset: 0x00007416
		public virtual string ErrorDesc_InvalidDateFormat
		{
			get
			{
				return "Supplied string date format is invalid.";
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000841D File Offset: 0x0000741D
		public virtual string ErrorDesc_InvalidDataFormat
		{
			get
			{
				return "Supplied data has invalid format.";
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008424 File Offset: 0x00007424
		public virtual string ErrorDesc_InvalidDataSyntax
		{
			get
			{
				return "Supplied string has invalid syntax.";
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000842B File Offset: 0x0000742B
		public virtual string ErrorDesc_SocketException
		{
			get
			{
				return "SocketException occurred.";
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00008432 File Offset: 0x00007432
		public virtual string ErrorDesc_NoIP4AddressesFound
		{
			get
			{
				return "No IPv4-compatible end point found.";
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008439 File Offset: 0x00007439
		public virtual string ErrorDesc_SocketTimeout
		{
			get
			{
				return "Socket connection has timed out.";
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008440 File Offset: 0x00007440
		public virtual string ErrorDesc_AbortedByLocalHost
		{
			get
			{
				return "Socket connection has been aborted by local machine.";
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00008447 File Offset: 0x00007447
		public virtual string ErrorDesc_ConnectionRefused
		{
			get
			{
				return "Socket connection has been refused by remote host.";
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000844E File Offset: 0x0000744E
		public virtual string ErrorDesc_AbortedByServer
		{
			get
			{
				return "Socket connection was aborted by remote host.";
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00008455 File Offset: 0x00007455
		public virtual string ErrorDesc_HostNotFound
		{
			get
			{
				return "Remote host not found.";
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000845C File Offset: 0x0000745C
		public virtual string ErrorDesc_HostDown
		{
			get
			{
				return "Remote host is down.";
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00008463 File Offset: 0x00007463
		public virtual string ErrorDesc_HostUnreachable
		{
			get
			{
				return "Remote host is unreachable.";
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000846A File Offset: 0x0000746A
		public virtual string ErrorDesc_ConnectionReset
		{
			get
			{
				return "An existing connection was forcibly closed by the remote host.";
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008471 File Offset: 0x00007471
		public virtual string ErrorDesc_SocketDisposed
		{
			get
			{
				return "The socket object used by MailBee was unexpectedly closed.";
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00008478 File Offset: 0x00007478
		public virtual string ErrorDesc_ResponseTimeout
		{
			get
			{
				return "Establishing connection with the server has been aborted as it was too slow.";
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000847F File Offset: 0x0000747F
		public virtual string ErrorDesc_ProxyConnectionDeclined
		{
			get
			{
				return "The proxy server declined to act as a proxy gateway for the given client.";
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00008486 File Offset: 0x00007486
		public virtual string ErrorDesc_ProxyAuthMethodsNotAccepted
		{
			get
			{
				return "None of the methods listed by the client to the proxy server are acceptable.";
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000848D File Offset: 0x0000748D
		public virtual string ErrorDesc_ProxyUserPassAuthFailed
		{
			get
			{
				return "Proxy Username/Password authentication failed.";
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00008494 File Offset: 0x00007494
		public virtual string ErrorDesc_ProxyGssApiAuthFailed
		{
			get
			{
				return "Proxy GSSAPI authentication failed.";
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000849B File Offset: 0x0000749B
		public virtual string ErrorDesc_NotConnected
		{
			get
			{
				return "Not yet connected to the server. Call Connect first.";
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000084A2 File Offset: 0x000074A2
		public virtual string ErrorDesc_AlreadyConnected
		{
			get
			{
				return "Already connected to the server.";
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000084A9 File Offset: 0x000074A9
		public virtual string ErrorDesc_AlreadySsl
		{
			get
			{
				return "TLS/SSL connection already established.";
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000084B0 File Offset: 0x000074B0
		public virtual string ErrorDesc_NotLoggedIn
		{
			get
			{
				return "Not yet logged in. Call Login first.";
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000084B7 File Offset: 0x000074B7
		public virtual string ErrorDesc_AlreadyLoggedIn
		{
			get
			{
				return "Already logged in.";
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000084BE File Offset: 0x000074BE
		public virtual string ErrorDesc_NoCredentials
		{
			get
			{
				return "No credentials have been supplied for login.";
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000084C5 File Offset: 0x000074C5
		public virtual string ErrorDesc_BadCredentials
		{
			get
			{
				return "Wrong account name and/or password.";
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000084CC File Offset: 0x000074CC
		public virtual string ErrorDesc_UnsupportedLoginMethod
		{
			get
			{
				return "The specified authentication method is not supported by the server.";
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000084D3 File Offset: 0x000074D3
		public virtual string ErrorDesc_NoSupportedLoginMethods
		{
			get
			{
				return "None of the specified authentication methods is supported by the server.";
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000084DA File Offset: 0x000074DA
		public virtual string ErrorDesc_SaslAnswerRejected
		{
			get
			{
				return "The server has rejected authentication data sent by the client.";
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000084E1 File Offset: 0x000074E1
		public virtual string ErrorDesc_LoginWin32Error
		{
			get
			{
				return "Win32 function related to Integrated Windows Authentication returned an error. See NativeErrorCode property value for more information.";
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000084E8 File Offset: 0x000074E8
		public virtual string ErrorDesc_NegativeResponse
		{
			get
			{
				return "The server has responded with negative reply.";
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000084EF File Offset: 0x000074EF
		public virtual string ErrorDesc_InvalidResponse
		{
			get
			{
				return "The response received from the server could not be parsed.";
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000223 RID: 547 RVA: 0x000084F6 File Offset: 0x000074F6
		public virtual string ErrorDesc_EmptyResponseLine
		{
			get
			{
				return "Status line of the server response contains no status code.";
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000224 RID: 548 RVA: 0x000084FD File Offset: 0x000074FD
		public virtual string ErrorDesc_InvalidBinaryResponse
		{
			get
			{
				return "The binary response received from the server cannot be parsed.";
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008504 File Offset: 0x00007504
		public virtual string ErrorDesc_InconsistentResponse
		{
			get
			{
				return "The response received from the server refers to non-existent data.";
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000850B File Offset: 0x0000750B
		public virtual string ErrorDesc_InvalidResponseItem
		{
			get
			{
				return "The particular item of the response data cannot be parsed.";
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00008512 File Offset: 0x00007512
		public virtual string ErrorDesc_InvalidBase64DataInResponse
		{
			get
			{
				return "Base64 data received from the server cannot be decoded.";
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008519 File Offset: 0x00007519
		public virtual string ErrorDesc_StartTlsNotAvailable
		{
			get
			{
				return "The server does not support STARTTLS (STLS for POP3) command.";
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00008520 File Offset: 0x00007520
		public virtual string ErrorDesc_SslWin32Error
		{
			get
			{
				return "SSL-related Win32 function returned an error. See NativeErrorCode property value for more information.";
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008527 File Offset: 0x00007527
		public virtual string ErrorDesc_SslNegoIOException
		{
			get
			{
				return "IOException occurred during SSL negotiation.";
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000852E File Offset: 0x0000752E
		public virtual string ErrorDesc_SslNegoNotSupportedException
		{
			get
			{
				return "NotSupportedException occurred during SSL negotiation.";
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008535 File Offset: 0x00007535
		public virtual string ErrorDesc_SslNegoAuthException
		{
			get
			{
				return "AuthenticationException occurred during SSL negotiation.";
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000853C File Offset: 0x0000753C
		public virtual string ErrorDesc_SslNegoException
		{
			get
			{
				return "Exception occurred during SSL negotiation.";
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008543 File Offset: 0x00007543
		public virtual string ErrorDescSuffix_DnsResponseCode0HostName1Base64EncodedData2
		{
			get
			{
				return " DnsResponseCode: {0}, HostName DNS server was queried about: \"{1}\", Base64 encoded response data: {2} (trailing zero bytes truncated).";
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000854A File Offset: 0x0000754A
		public virtual string ErrorDesc_BadDnsResponseHeader
		{
			get
			{
				return "The header of DNS query response is incorrect.";
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008551 File Offset: 0x00007551
		public virtual string ErrorDesc_DnsInvalidQName
		{
			get
			{
				return "The length of the item name being queried to DNS server exceeds allowed limit.";
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008558 File Offset: 0x00007558
		public virtual string ErrorDesc_DnsQueryMismatch
		{
			get
			{
				return "The response from DNS server does not correspond to the given query.";
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000855F File Offset: 0x0000755F
		public virtual string ErrorDesc_DnsRecursionRequired
		{
			get
			{
				return "DNS server must support recursion in order to execute the query.";
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008566 File Offset: 0x00007566
		public virtual string ErrorDesc_NoDnsServersSpecified
		{
			get
			{
				return "At least one DNS server must be specified in order to perform direct send or MX query.";
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000856D File Offset: 0x0000756D
		public virtual string ErrorDesc_NoDnsAttemptsLeft
		{
			get
			{
				return "DNS server has failed too many times and is now considered down.";
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008574 File Offset: 0x00007574
		public virtual string ErrorDesc_NoDnsServersAvailable
		{
			get
			{
				return "At least one DNS server must be registered in the system in order to perform direct send.";
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000857B File Offset: 0x0000757B
		public virtual string ErrorDesc_DnsFormatErrorReply
		{
			get
			{
				return "DNS server replied with Format Error status.";
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008582 File Offset: 0x00007582
		public virtual string ErrorDesc_DnsFailureReply
		{
			get
			{
				return "DNS server replied with Server Failure status.";
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008589 File Offset: 0x00007589
		public virtual string ErrorDesc_DnsNameErrorReply
		{
			get
			{
				return "DNS server replied queried host name was unknown.";
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00008590 File Offset: 0x00007590
		public virtual string ErrorDesc_DnsNotSupportedReply
		{
			get
			{
				return "DNS server replied with Not Implemented status";
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00008597 File Offset: 0x00007597
		public virtual string ErrorDesc_DnsQueryRefusedReply
		{
			get
			{
				return "DNS server refused the query. You should try another DNS server.";
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000859E File Offset: 0x0000759E
		public virtual string ErrorDesc_DnsUnknownReply
		{
			get
			{
				return "DNS server replied with unknown status.";
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000085A5 File Offset: 0x000075A5
		public virtual string ErrorDesc_NoSmtpServersSpecified
		{
			get
			{
				return "At least one SMTP server must be specified in order to send to relay server.";
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000085AC File Offset: 0x000075AC
		public virtual string ErrorDesc_NotHelloed
		{
			get
			{
				return "SMTP Hello (HELO or EHLO) has not yet been sent. Call Hello first.";
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000085B3 File Offset: 0x000075B3
		public virtual string ErrorDesc_EhloNotSupported
		{
			get
			{
				return "EHLO command is not supported by the server.";
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000085BA File Offset: 0x000075BA
		public virtual string ErrorDesc_NoSender
		{
			get
			{
				return "No sender specified for the mail message.";
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000085C1 File Offset: 0x000075C1
		public virtual string ErrorDesc_BadSender
		{
			get
			{
				return "The server rejected the specified sender email address.";
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000085C8 File Offset: 0x000075C8
		public virtual string ErrorDesc_NoRecipients
		{
			get
			{
				return "At least one recipient must be specified.";
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000085CF File Offset: 0x000075CF
		public virtual string ErrorDesc_BadRecipient
		{
			get
			{
				return "The server rejected the given recipient.";
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000085D6 File Offset: 0x000075D6
		public virtual string ErrorDesc_NoAllowedRecipients
		{
			get
			{
				return "The server rejected all the recipients of the mail message.";
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000085DD File Offset: 0x000075DD
		public virtual string ErrorDesc_DataNotAllowed
		{
			get
			{
				return "The server rejected DATA command.";
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000085E4 File Offset: 0x000075E4
		public virtual string ErrorDesc_BadMessageData
		{
			get
			{
				return "The mail message data has been rejected by the server.";
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000085EB File Offset: 0x000075EB
		public virtual string ErrorDesc_BdatRejected
		{
			get
			{
				return "BDAT command has been rejected by the server.";
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000085F2 File Offset: 0x000075F2
		public virtual string ErrorDesc_MessageDataTooLarge
		{
			get
			{
				return "The length of message data exceeds maximum allowed limit for the given server.";
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000085F9 File Offset: 0x000075F9
		public virtual string ErrorDesc_Conversion8bitTo7Bit
		{
			get
			{
				return "The message data will be converted to 7bit format because the given server cannot transmit 8bit data. Some data will be lost.";
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00008600 File Offset: 0x00007600
		public virtual string ErrorDesc_NoSmtpOrDnsServersSpecified
		{
			get
			{
				return "At least one DNS or SMTP server must be specified in order to send mail.";
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008607 File Offset: 0x00007607
		public virtual string ErrorDesc_NoDomainsToSendFor
		{
			get
			{
				return "At least one recipient domain must be specified in order to send mail to domains MXes.";
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000860E File Offset: 0x0000760E
		public virtual string ErrorDesc_NoDomainsForMXLookup
		{
			get
			{
				return "At least one recipient domain must be specified in order to perform MX lookup.";
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00008615 File Offset: 0x00007615
		public virtual string ErrorDesc_EmptyHostNameForDnsQuery
		{
			get
			{
				return "Cannot make DNS query for empty host name.";
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000861C File Offset: 0x0000761C
		public virtual string ErrorDesc_InvalidDomain0InCache
		{
			get
			{
				return "DNS cache states the domain \"{0}\" does not exist.";
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00008623 File Offset: 0x00007623
		public virtual string ErrorDesc_DeadDomain0SmtpMXesInCache
		{
			get
			{
				return "DNS cache states all SMTP MXes for the domain \"{0}\" are not available or broken.";
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000862A File Offset: 0x0000762A
		public virtual string ErrorDesc_Pop3AuthCommandUnknown
		{
			get
			{
				return "The server does not support AUTH command. SASL authentication will not be available.";
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00008631 File Offset: 0x00007631
		public virtual string ErrorDesc_Pop3CapaCommandUnknown
		{
			get
			{
				return "The server does not support CAPA command. POP3 pipelining will not be available.";
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008638 File Offset: 0x00007638
		public virtual string ErrorDesc_ImapFolderNotSelected
		{
			get
			{
				return "A folder must be selected on the server. Call SelectFolder or ExamineFolder first.";
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000863F File Offset: 0x0000763F
		public virtual string ErrorDescSuffix_ImapInvalidEnvelopeMessageNumber0
		{
			get
			{
				return " The invalid envelope message number is: {0}.";
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00008646 File Offset: 0x00007646
		public virtual string ErrorDescSuffix_ImapNonExistentMessageNumber0
		{
			get
			{
				return " Non-existent message number is: {0}.";
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000864D File Offset: 0x0000764D
		public virtual string ErrorDescSuffix_ImapNonExistentUid0
		{
			get
			{
				return " Non-existent UID is: {0}.";
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00008654 File Offset: 0x00007654
		public virtual string ErrorDesc_ImapResponseNotFound
		{
			get
			{
				return "No required data found in the server response.";
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000865B File Offset: 0x0000765B
		public virtual string ErrorDesc_ImapMessageIndexNotFound
		{
			get
			{
				return "The message with the specified index does not exist on the server.";
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00008662 File Offset: 0x00007662
		public virtual string ErrorDesc_NoMessageInImapResponse
		{
			get
			{
				return "No required mail message data found in the server response.";
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00008669 File Offset: 0x00007669
		public virtual string ErrorDesc_InvalidImapEnvelope
		{
			get
			{
				return "The envelope data is corrupted or incorrect. Envelope.IsValid will be false.";
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008670 File Offset: 0x00007670
		public virtual string ErrorDesc_ImapUidPlusNotSupported
		{
			get
			{
				return "The IMAP4 server does not support UIDPLUS capability required for the current operation to complete.";
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00008677 File Offset: 0x00007677
		public virtual string ErrorDesc_ImapQuotaNotSupported
		{
			get
			{
				return "The IMAP4 server does not support QUOTA capability required to obtain account and folder quotas.";
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000867E File Offset: 0x0000767E
		public virtual string ErrorDesc_ImapIdleNotSupported
		{
			get
			{
				return "The IMAP4 server does not support IDLE capability required to go into IDLE mode.";
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00008685 File Offset: 0x00007685
		public virtual string ErrorDesc_ImapSortNotSupported
		{
			get
			{
				return "The IMAP4 server does not support SORT capability required to perform sorted search.";
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000868C File Offset: 0x0000768C
		public virtual string ErrorDesc_ImapNamespaceNotSupported
		{
			get
			{
				return "The IMAP4 server does not support NAMESPACE capability.";
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00008693 File Offset: 0x00007693
		public virtual string ErrorDesc_EwsLocalException
		{
			get
			{
				return "Client-side Exchange error occured.";
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000869A File Offset: 0x0000769A
		public virtual string ErrorDesc_EwsRemoteException
		{
			get
			{
				return "Server-side Exchange error occured.";
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000260 RID: 608 RVA: 0x000086A1 File Offset: 0x000076A1
		public virtual string ErrorDesc_EwsFolderNotFound
		{
			get
			{
				return "Exchange folder not found.";
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000086A8 File Offset: 0x000076A8
		public virtual string ErrorDesc_EwsFolderAlreadyExists
		{
			get
			{
				return "Exchange folder already exists.";
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000086AF File Offset: 0x000076AF
		public virtual string ErrorDesc_TnefSignature0Invalid
		{
			get
			{
				return "Invalid TNEF signature 0x{0} (not a valid TNEF stream).";
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000263 RID: 611 RVA: 0x000086B6 File Offset: 0x000076B6
		public virtual string ErrorDesc_TnefUnexpectedEndOfStream
		{
			get
			{
				return "Unexpected end of TNEF stream.";
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000086BD File Offset: 0x000076BD
		public virtual string ErrorDesc_TnefLevelTypeInvalid0
		{
			get
			{
				return "Invalid TNEF level type: {0}.";
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000086C4 File Offset: 0x000076C4
		public virtual string ErrorDesc_TnefMapiPropTypeInvalid0
		{
			get
			{
				return "Invalid MapiProp type: {0}.";
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000086CB File Offset: 0x000076CB
		public virtual string ErrorDesc_TnefMapiMultivalueIsNotAllowedInSingleMapiValue
		{
			get
			{
				return "Multivalue is not allowed in single MapiValue.";
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000086D2 File Offset: 0x000076D2
		public virtual string ErrorDesc_TnefAttributeChecksumInvalid
		{
			get
			{
				return "Invalid checksum of TNEF attribute.";
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000086D9 File Offset: 0x000076D9
		public virtual string ErrorDesc_TnefAttributeLevelInvalid0
		{
			get
			{
				return "Invalid TNEF attribute level: {0}.";
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000086E0 File Offset: 0x000076E0
		public virtual string ErrorDesc_TnefMapiTypeUnknown0
		{
			get
			{
				return "Unknown TNEF MAPI type: {0}.";
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000086E7 File Offset: 0x000076E7
		public virtual string ErrorDesc_TnefGuidInvalid0
		{
			get
			{
				return "Invalid TNEF Guid: {0}.";
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000086EE File Offset: 0x000076EE
		public virtual string ErrorDesc_TnefCompressedRtfHeaderInvalid
		{
			get
			{
				return "Invalid TNEF compressed-RTF header.";
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000086F5 File Offset: 0x000076F5
		public virtual string ErrorDesc_TnefCompressedRtfDataSizeMismatch
		{
			get
			{
				return "TNEF compressed-RTF data size mismatch.";
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000086FC File Offset: 0x000076FC
		public virtual string ErrorDesc_TnefCompressedRtfCrc32Failed
		{
			get
			{
				return "TNEF compressed-RTF CRC32 failed.";
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00008703 File Offset: 0x00007703
		public virtual string ErrorDesc_TnefUnknownRtfCompressionType0
		{
			get
			{
				return "TNEF Unknown RTF compression type (magic number {0}).";
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000870A File Offset: 0x0000770A
		public virtual string ErrorDesc_OleDocEntry0IsNotDocumentEntry
		{
			get
			{
				return "OLE2 entry '{0}' is not a DocumentEntry.";
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00008711 File Offset: 0x00007711
		public virtual string ErrorDesc_OleDocCannotOpenInternalDocumentStorage
		{
			get
			{
				return "Cannot open internal OLE2 document storage.";
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00008718 File Offset: 0x00007718
		public virtual string ErrorDesc_OleDocIllegalBlockCount0
		{
			get
			{
				return "Illegal OLE2 block count; minimum count is 1, got {0} instead.";
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000871F File Offset: 0x0000771F
		public virtual string ErrorDesc_OleDocBatCountExceedsLimit
		{
			get
			{
				return "OLE2 BAT count exceeds limit, yet XBAT index indicates no valid entries.";
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00008726 File Offset: 0x00007726
		public virtual string ErrorDesc_OleDocCouldNotFindAllBlocks
		{
			get
			{
				return "Could not find all OLE2 blocks.";
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000872D File Offset: 0x0000772D
		public virtual string ErrorDesc_OleDocIndex0IsUnused
		{
			get
			{
				return "OLE2 index {0} is unused.";
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00008734 File Offset: 0x00007734
		public virtual string ErrorDesc_OleDocAttemptToReplaceExistingBlockAllocationTable
		{
			get
			{
				return "Attempt to replace existing OLE2 BlockAllocationTable.";
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000873B File Offset: 0x0000773B
		public virtual string ErrorDesc_OleDocBlock0AlreadyRemoved
		{
			get
			{
				return "OLE2 Block[{0}] already removed.";
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00008742 File Offset: 0x00007742
		public virtual string ErrorDesc_OleDocCannotRemoveBlock0OutOfRange1
		{
			get
			{
				return "Cannot remove OLE2 block[{0}] out of range[0 - {1}].";
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00008749 File Offset: 0x00007749
		public virtual string ErrorDesc_OleDocImproperlyInitializedList
		{
			get
			{
				return "Improperly initialized list: no OLE2 block allocation table provided.";
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00008750 File Offset: 0x00007750
		public virtual string ErrorDesc_OleDocInvalidHeaderSignatureRead0Expected1
		{
			get
			{
				return "Invalid OLE2 header signature (Outlook MSG message must be a binary file with a valid OLE2 signature). Read {0}, expected {1}.";
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00008757 File Offset: 0x00007757
		public virtual string ErrorDesc_OleDocUnableToReadEntireHeader0ReadExpected1
		{
			get
			{
				return "Unable to read the entire OLE2 header: {0} byte(s) read; expected {1} bytes.";
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000875E File Offset: 0x0000775E
		public virtual string ErrorDesc_OleDocDuplicateName0
		{
			get
			{
				return "Duplicate OLE2 name {0}.";
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00008765 File Offset: 0x00007765
		public virtual string ErrorDesc_OleDocCannotReturnEmptyData
		{
			get
			{
				return "Cannot return empty OLE2 data.";
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000876C File Offset: 0x0000776C
		public virtual string ErrorDesc_OleDocXmlNotOle2Format
		{
			get
			{
				return "The supplied data appears to be in the Office 2007+ XML. MailBee.NET only supports OLE2 document format.";
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00008773 File Offset: 0x00007773
		public virtual string ErrorDesc_OleDocTriedToWriteTooMuchData
		{
			get
			{
				return "Tried to write too much OLE2 data.";
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000877A File Offset: 0x0000777A
		public virtual string ErrorDesc_OleDocParsingError
		{
			get
			{
				return "An OLE2 parsing error occured.";
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00008781 File Offset: 0x00007781
		public virtual string ErrorDesc_OleDocBuildingError
		{
			get
			{
				return "An OLE2 building error occured.";
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00008788 File Offset: 0x00007788
		public virtual string ErrorDesc_OutlookPstParsingError
		{
			get
			{
				return "Outlook .PST file parsing error occured.";
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000878F File Offset: 0x0000778F
		public virtual string ErrorDesc_OutlookPstUnableToReadSignature
		{
			get
			{
				return "Unable to read PST Signature.";
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00008796 File Offset: 0x00007796
		public virtual string ErrorDesc_OutlookPstAttachmentIsEmpty
		{
			get
			{
				return "PST attachment is empty.";
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000879D File Offset: 0x0000779D
		public virtual string ErrorDesc_OutlookPstInvalidDescriptorsOffsetPassed
		{
			get
			{
				return "Unable to create PST Descriptor, invalid descriptor offset.";
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000087A4 File Offset: 0x000077A4
		public virtual string ErrorDesc_OutlookPstBadSignature0
		{
			get
			{
				return "Unable to process PST descriptor node, bad signature: {0}";
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000087AB File Offset: 0x000077AB
		public virtual string ErrorDesc_OutlookPstUnrecognisedPstFileVersion0
		{
			get
			{
				return "Unrecognised PST File version: {0}.";
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000087B2 File Offset: 0x000077B2
		public virtual string ErrorDesc_OutlookPstOnlyUnencryptedFilesSupported
		{
			get
			{
				return "Only unencrypted and compressable PST files are supported at this time.";
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000288 RID: 648 RVA: 0x000087B9 File Offset: 0x000077B9
		public virtual string ErrorDesc_OutlookPstUnableToReadDescriptorNode
		{
			get
			{
				return "Unable to read PST descriptor node, not a descriptor.";
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000087C0 File Offset: 0x000077C0
		public virtual string ErrorDesc_OutlookPstInvalidFileHeader0
		{
			get
			{
				return "Invalid PST file header: {0}, expected: !BDN.";
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000087C7 File Offset: 0x000077C7
		public virtual string ErrorDesc_OutlookPstExpectingLocalDescriptorNodeRef
		{
			get
			{
				return "Local PST descriptor node ref was expected but not found.";
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000087CE File Offset: 0x000077CE
		public virtual string ErrorDesc_OutlookPstMultipleValueArraysInSubdescriptorsUnsupported
		{
			get
			{
				return "Multiple value array in PST subdescriptor encountered.";
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000087D5 File Offset: 0x000077D5
		public virtual string ErrorDesc_OutlookPstUnableToFetchAttachmentNumber0Only1InThisEmail
		{
			get
			{
				return "Unable to fetch PST attachment #{0}, only {1} attachment(s) found in this e-mail message.";
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000087DC File Offset: 0x000077DC
		public virtual string ErrorDesc_OutlookPstUnableToFetchAttachmentNumber0UnableToReadAttachmentDetailsTable
		{
			get
			{
				return "Unable to fetch PST attachment #{0}, unable to read attachment details table.";
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000087E3 File Offset: 0x000077E3
		public virtual string ErrorDesc_OutlookPstUnableToFetchRecipientNumber0
		{
			get
			{
				return "Unable to fetch PST recipient #{0}.";
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000087EA File Offset: 0x000077EA
		public virtual string ErrorDesc_OutlookPstUnableToFind0
		{
			get
			{
				return "Unable to find {0} in PST data.";
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000087F1 File Offset: 0x000077F1
		public virtual string ErrorDesc_OutlookPstUnableToFindNode0
		{
			get
			{
				return "Unable to find PST node: {0}.";
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000087F8 File Offset: 0x000077F8
		public virtual string ErrorDesc_OutlookPstUnableToProcessArray
		{
			get
			{
				return "Unable to process PST array, not an array.";
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000087FF File Offset: 0x000077FF
		public virtual string ErrorDesc_OutlookPstUnknownChildType01
		{
			get
			{
				return "Unknown PST child type: {0} - {1}.";
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00008806 File Offset: 0x00007806
		public virtual string ErrorDesc_OutlookPstUnableToParseTable0
		{
			get
			{
				return "Unable to parse PST table, can't find BTHHEADER header information: {0}.";
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000880D File Offset: 0x0000780D
		public virtual string ErrorDesc_OutlookPstUnableToParseTableBadType
		{
			get
			{
				return "Unable to parse PST table, bad table type.";
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00008814 File Offset: 0x00007814
		public virtual string ErrorDesc_OutlookPstUnableToParseTableBadType0
		{
			get
			{
				return "Unable to parse PST table, bad table type. Unknown identifier: 0x{0}.";
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000881B File Offset: 0x0000781B
		public virtual string ErrorDesc_OutlookPstUnableToCreate7cTable
		{
			get
			{
				return "Unable to create PSTTable7C, table is not 7c.";
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00008822 File Offset: 0x00007822
		public virtual string ErrorDesc_OutlookPstUnableToCreateBcTable
		{
			get
			{
				return "Unable to create PSTTableBC, table is not bc.";
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008829 File Offset: 0x00007829
		public virtual string ErrorDesc_OutlookPstExternalReferenceButNoLocalDescriptorItems
		{
			get
			{
				return "External reference but no localDescriptorItems where they are expected in PST.";
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00008830 File Offset: 0x00007830
		public virtual string ErrorDesc_OutlookPstAttemptingToGetNonBinaryData
		{
			get
			{
				return "Attempting to get non-binary data where they are not expected in PST.";
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00008837 File Offset: 0x00007837
		public virtual string ErrorDesc_OutlookPstIOExceptionReadingSubNode0
		{
			get
			{
				return "IOException reading PST subNode: 0x{0:X8}.";
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000883E File Offset: 0x0000783E
		public virtual string ErrorDesc_OutlookPstInvalidInternalBlockSize
		{
			get
			{
				return "Invalid PST internal block size.";
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00008845 File Offset: 0x00007845
		public virtual string ErrorDesc_OutlookPstUnableToProcessXBlock
		{
			get
			{
				return "Unable to process PST XBlock, incorrect identifier.";
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000884C File Offset: 0x0000784C
		public virtual string ErrorDesc_OutlookPstUnableToSeekPastEndOfItemSize0SeekingTo1
		{
			get
			{
				return "Unable to seek past end of PST item. size = {0}, seeking to: {1}.";
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00008853 File Offset: 0x00007853
		public virtual string ErrorDesc_OutlookPstCantGetChildFoldersForFolder01ChildCount23
		{
			get
			{
				return "Unable to seek past end of PST item. size = {0}, seeking to: {1}.";
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000885A File Offset: 0x0000785A
		public virtual string ErrorDesc_OutlookPstUnableToReadDescriptorNodeIsNotADescriptor
		{
			get
			{
				return "Unable to read descriptor node, is not a descriptor.";
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00008861 File Offset: 0x00007861
		public virtual string ErrorDesc_OutlookPstMissingAttachmentDescriptorItemFor0
		{
			get
			{
				return "Missing attachment descriptor item for: {0}.";
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008868 File Offset: 0x00007868
		public virtual string ErrorDesc_CryptoProviderWin32Error
		{
			get
			{
				return "Win32 function related to a crypto provider API returned an error. See NativeErrorCode property value for more information.";
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000886F File Offset: 0x0000786F
		public virtual string ErrorDesc_CertificateStoreWin32Error
		{
			get
			{
				return "Win32 function related to certificate store API returned an error. See NativeErrorCode property value for more information.";
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008876 File Offset: 0x00007876
		public virtual string ErrorDesc_CertificateWin32Error
		{
			get
			{
				return "Certificate-related Win32 function returned an error. See NativeErrorCode property value for more information.";
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000887D File Offset: 0x0000787D
		public virtual string ErrorDesc_SmimeWin32Error
		{
			get
			{
				return "S/MIME-related Win32 function returned an error. See NativeErrorCode property value for more information.";
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008884 File Offset: 0x00007884
		public virtual string ErrorDesc_CertificateValidationError
		{
			get
			{
				return "Certificate validation failed: the certificate is invalid or expired.";
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000888B File Offset: 0x0000788B
		public virtual string ErrorDesc_CertificateDataInvalid
		{
			get
			{
				return "Certificate data is invalid, of unknown format, or the supplied password is incorrect.";
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00008892 File Offset: 0x00007892
		public virtual string ErrorDesc_NotImpersonated
		{
			get
			{
				return "Not impersonated yet. Call LogonAs first.";
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00008899 File Offset: 0x00007899
		public virtual string ErrorDesc_ImpersonationWin32Error
		{
			get
			{
				return "Certificate data is either invalid or of unknown format.";
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000088A0 File Offset: 0x000078A0
		public virtual string ErrorDesc_CryptoPrivateKeyInvalid
		{
			get
			{
				return "Invalid cryptographic private key format.";
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000088A7 File Offset: 0x000078A7
		public virtual string Log_MessageTypeInfo
		{
			get
			{
				return "INFO";
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000088AE File Offset: 0x000078AE
		public virtual string Log_MessageTypeRecv
		{
			get
			{
				return "RECV";
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000088B5 File Offset: 0x000078B5
		public virtual string Log_MessageTypeSend
		{
			get
			{
				return "SEND";
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000088BC File Offset: 0x000078BC
		public virtual string Log_MessageTypeUser
		{
			get
			{
				return "USER";
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000088C3 File Offset: 0x000078C3
		public virtual string Log_AssemblyVersion0
		{
			get
			{
				return "Assembly version: {0}.";
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000088CA File Offset: 0x000078CA
		public virtual string Log_Base64Banner
		{
			get
			{
				return "Binary data is Base64-encoded";
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x000088D1 File Offset: 0x000078D1
		public virtual string Log_AbortRequested
		{
			get
			{
				return "User code called Abort.";
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x000088D8 File Offset: 0x000078D8
		public virtual string Log_WillResolveHost0
		{
			get
			{
				return "Will resolve host \"{0}\".";
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x000088DF File Offset: 0x000078DF
		public virtual string Log_Host0ResolvedToIP1
		{
			get
			{
				return "Host \"{0}\" resolved to IP address(es) {1}.";
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x000088E6 File Offset: 0x000078E6
		public virtual string Log_WillConnectToHost0OnPort1
		{
			get
			{
				return "Will connect to host \"{0}\" on port {1}.";
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000088ED File Offset: 0x000078ED
		public virtual string Log_WillConnectVia0ProxyAtHost1OnPort2
		{
			get
			{
				return "Will actually connect via {0} proxy server at host \"{1}\" on port {2}.";
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x000088F4 File Offset: 0x000078F4
		public virtual string Log_SocketConnectedToIPAddress0OnPort1
		{
			get
			{
				return "Socket connected to IP address {0} on port {1}.";
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x000088FB File Offset: 0x000078FB
		public virtual string Log_ConnectedToServerAtHost0OnPort1
		{
			get
			{
				return "Connected to mail service at host \"{0}\" on port {1} and ready.";
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00008902 File Offset: 0x00007902
		public virtual string Log_WillDisconnectFromHost0
		{
			get
			{
				return "Will disconnect from host \"{0}\".";
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00008909 File Offset: 0x00007909
		public virtual string Log_DisconnectedFromHost0
		{
			get
			{
				return "Disconnected from host \"{0}\".";
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008910 File Offset: 0x00007910
		public virtual string Log_StartTls
		{
			get
			{
				return "Notify server that we are ready to start TLS/SSL negotiation.";
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00008917 File Offset: 0x00007917
		public virtual string Log_WillCreateSslCredentials
		{
			get
			{
				return "Will create TLS/SSL credentials.";
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000891E File Offset: 0x0000791E
		public virtual string Log_SslCredentialsCreated
		{
			get
			{
				return "TLS/SSL credentials created.";
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00008925 File Offset: 0x00007925
		public virtual string Log_WillPerformSslHandshake
		{
			get
			{
				return "Will start TLS/SSL negotiation sequence.";
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000892C File Offset: 0x0000792C
		public virtual string Log_SslHandshakeDone
		{
			get
			{
				return "TLS/SSL negotiation completed.";
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00008933 File Offset: 0x00007933
		public virtual string Log_WillValidateServerCert
		{
			get
			{
				return "Will check if server certificate complies with the specified auto-validation flags.";
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000893A File Offset: 0x0000793A
		public virtual string Log_ServerCertRetrieved
		{
			get
			{
				return "Server certificate successfully created from the handle. Can verify it now.";
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00008941 File Offset: 0x00007941
		public virtual string Log_ServerCertAutoValidationSucceeded
		{
			get
			{
				return "Server certificate validation passed.";
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00008948 File Offset: 0x00007948
		public virtual string Log_WillLoginAs0
		{
			get
			{
				return "Will login as \"{0}\".";
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000894F File Offset: 0x0000794F
		public virtual string Log_LoggedInAs0
		{
			get
			{
				return "Logged in as \"{0}\".";
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00008956 File Offset: 0x00007956
		public virtual string Log_WillTrySasl0Auth
		{
			get
			{
				return "Will try SASL {0} authentication method.";
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000895D File Offset: 0x0000795D
		public virtual string Log_Sasl0AuthUnsupported
		{
			get
			{
				return "SASL {0} authentication method is not supported by the server.";
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00008964 File Offset: 0x00007964
		public virtual string Log_Error0
		{
			get
			{
				return "Error: {0}";
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000896B File Offset: 0x0000796B
		public virtual string Log_Warning0
		{
			get
			{
				return "Warning: {0}";
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00008972 File Offset: 0x00007972
		public virtual string Log_0BytesReceived
		{
			get
			{
				return "Total {0} bytes received.";
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008979 File Offset: 0x00007979
		public virtual string Log_0BytesSent
		{
			get
			{
				return "Data chunk of {0} bytes sent.";
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00008980 File Offset: 0x00007980
		public virtual string Log_SmtpWillHello
		{
			get
			{
				return "Will send Hello command (HELO or EHLO).";
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00008987 File Offset: 0x00007987
		public virtual string SmtpHelloed
		{
			get
			{
				return "SMTP Hello completed.";
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000898E File Offset: 0x0000798E
		public virtual string Log_SmtpLoginFailed
		{
			get
			{
				return "Warning: Authentication failed.";
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00008995 File Offset: 0x00007995
		public virtual string Log_SmtpWillResetSmtpSession
		{
			get
			{
				return "Will reset SMTP session.";
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000899C File Offset: 0x0000799C
		public virtual string Log_SmtpSessionReset
		{
			get
			{
				return "SMTP session was reset.";
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000089A3 File Offset: 0x000079A3
		public virtual string Log_SmtpWillPerformAuthPopBeforeSmtp
		{
			get
			{
				return "Will perform POP-before-SMTP authentication.";
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000089AA File Offset: 0x000079AA
		public virtual string Log_SmtpAuthPopBeforeSmtpSucceeded
		{
			get
			{
				return "POP-before-SMTP authentication succeeded.";
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000089B1 File Offset: 0x000079B1
		public virtual string Log_SmtpAuthPopBeforeSmtpFailed
		{
			get
			{
				return "Warning: POP-before-SMTP authentication failed.";
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000089B8 File Offset: 0x000079B8
		public virtual string Log_SmtpWillSendMailMessageToServer0
		{
			get
			{
				return "Will send mail message to SMTP server \"{0}\".";
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000089BF File Offset: 0x000079BF
		public virtual string Log_SmtpSubmittingSenderAndRecipients
		{
			get
			{
				return "Will submit sender and recipients.";
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x000089C6 File Offset: 0x000079C6
		public virtual string Log_SmtpTestSendDone
		{
			get
			{
				return "Test send succeeded.";
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000089CD File Offset: 0x000079CD
		public virtual string Log_SmtpSenderAndRecipientsAccepted
		{
			get
			{
				return "Sender and recipients accepted by SMTP server.";
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000089D4 File Offset: 0x000079D4
		public virtual string Log_SmtpSubmittingMessageData
		{
			get
			{
				return "Will send message data now.";
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000089DB File Offset: 0x000079DB
		public virtual string Log_SmtpSendDone
		{
			get
			{
				return "Message successfully submitted to SMTP server.";
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000089E2 File Offset: 0x000079E2
		public virtual string Log_SendMailWillGetMXLists
		{
			get
			{
				return "Will retrieve MX records for every recipient domain.";
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000089E9 File Offset: 0x000079E9
		public virtual string Log_SendMailWillMakeDnsQueryToDnsAt0RegardingHost1
		{
			get
			{
				return "Will make DNS query to DNS server at {0} regarding host \"{1}\".";
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000089F0 File Offset: 0x000079F0
		public virtual string Log_SendMailMadeDnsQueryToDnsAt0RegardingHost1
		{
			get
			{
				return "Made DNS query to DNS server at {0} regarding host \"{1}\".";
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000089F7 File Offset: 0x000079F7
		public virtual string Log_SendMailGotDnsInfoRegardingHost0FromCache
		{
			get
			{
				return "Got DNS info regarding host \"{0}\" from cache. No actual DNS query has been made.";
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000089FE File Offset: 0x000079FE
		public virtual string Log_SendMailProcessedDnsQueryToDnsAt0RegardingHost1
		{
			get
			{
				return "Processed results of DNS query to DNS server at {0} regarding host \"{1}\".";
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00008A05 File Offset: 0x00007A05
		public virtual string Log_SendMailGotMXListsFor0DomainsOf1Total
		{
			get
			{
				return "Retrieved MX records for {0} recipient domain(s) of {1} total.";
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00008A0C File Offset: 0x00007A0C
		public virtual string Log_SendMailWillSendToRecipientDomains
		{
			get
			{
				return "Will send mail message to SMTP MX server(s) for every domain.";
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00008A13 File Offset: 0x00007A13
		public virtual string Log_SendMailWillSendToMXesOfDomain0
		{
			get
			{
				return "Will send mail message to SMTP MX server(s) of domain \"{0}\".";
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00008A1A File Offset: 0x00007A1A
		public virtual string Log_SendMailSentToMXesOfDomain0
		{
			get
			{
				return "Mail message sent to SMTP MX server(s) of domain \"{0}\".";
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00008A21 File Offset: 0x00007A21
		public virtual string Log_SendMailSentToRecipientDomains
		{
			get
			{
				return "Mail message sent to SMTP MX server(s) for every domain.";
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00008A28 File Offset: 0x00007A28
		public virtual string Log_SendMailWillSendViaMXLookup
		{
			get
			{
				return "Will send mail message using MX lookup.";
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00008A2F File Offset: 0x00007A2F
		public virtual string Log_SendMailFailedRecipientsAllowed
		{
			get
			{
				return "Failed recipients allowed.";
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00008A36 File Offset: 0x00007A36
		public virtual string Log_SendMailFailedRecipientsNotAllowed
		{
			get
			{
				return "Failed recipients not allowed.";
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00008A3D File Offset: 0x00007A3D
		public virtual string Log_SendMailWillTestSendViaMXLookup
		{
			get
			{
				return "Will test sending mail message using MX lookup.";
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00008A44 File Offset: 0x00007A44
		public virtual string Log_SendMailSentViaMXLookup
		{
			get
			{
				return "Mail message sent using MX Lookup.";
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00008A4B File Offset: 0x00007A4B
		public virtual string Log_SendMailTestViaMXLookupDone
		{
			get
			{
				return "Test send of mail message using MX Lookup completed.";
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00008A52 File Offset: 0x00007A52
		public virtual string Log_SendMailWillGetARblRecordsForIP0
		{
			get
			{
				return "Will get A RBL records for IP address {0}.";
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00008A59 File Offset: 0x00007A59
		public virtual string Log_SendMailGotARblListsFor0RblsOf1Total
		{
			get
			{
				return "Retrieved A records from {0} RBL(s) of {1} total.";
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00008A60 File Offset: 0x00007A60
		public virtual string Log_SendMailWillSend
		{
			get
			{
				return "Will send mail message.";
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00008A67 File Offset: 0x00007A67
		public virtual string Log_SendMailWillTestSend
		{
			get
			{
				return "Will test sending mail message.";
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00008A6E File Offset: 0x00007A6E
		public virtual string Log_SendMailDone
		{
			get
			{
				return "Mail message sent.";
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00008A75 File Offset: 0x00007A75
		public virtual string Log_SendMailTestDone
		{
			get
			{
				return "Test send of mail message done.";
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00008A7C File Offset: 0x00007A7C
		public virtual string Log_SendMailWillSubmitMessageToPickupFolder
		{
			get
			{
				return "Will submit mail message to pickup folder.";
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00008A83 File Offset: 0x00007A83
		public virtual string Log_SendMailMessageSubmittedToPickupFolderAs0
		{
			get
			{
				return "Mail message submitted to pickup folder as \"{0}\".";
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00008A8A File Offset: 0x00007A8A
		public virtual string LogSuffix_Tag0Rows1
		{
			get
			{
				return " Tag=\"{0}\", Rows=\"{1}\".";
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00008A91 File Offset: 0x00007A91
		public virtual string LogParam_MailingSucceeded
		{
			get
			{
				return "succeeded";
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00008A98 File Offset: 0x00007A98
		public virtual string LogParam_MailingFailedOrCancelled
		{
			get
			{
				return "failed or cancelled";
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00008A9F File Offset: 0x00007A9F
		public virtual string Log_MailingJob0
		{
			get
			{
				return "Job {0}.";
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00008AA6 File Offset: 0x00007AA6
		public virtual string Log_MailingProcessingJobsStarted
		{
			get
			{
				return "Processing of pending jobs started.";
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00008AAD File Offset: 0x00007AAD
		public virtual string Log_MailingProcessingJobsFinished
		{
			get
			{
				return "All jobs have been processed.";
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00008AB4 File Offset: 0x00007AB4
		public virtual string Log_MailingPendingJobEnqueued
		{
			get
			{
				return "New pending job to send e-mail(s).";
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00008ABB File Offset: 0x00007ABB
		public virtual string Log_MailingFailedJobReEnqueued
		{
			get
			{
				return "Failed job put back into pending list.";
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00008AC2 File Offset: 0x00007AC2
		public virtual string Log_MailingFailedJobsEnqueued
		{
			get
			{
				return "All failed jobs moved back into pending list.";
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00008AC9 File Offset: 0x00007AC9
		public virtual string Log_MailingPendingJobWentToProcessing
		{
			get
			{
				return "Processing of a pending job started.";
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00008AD0 File Offset: 0x00007AD0
		public virtual string Log_MailingNoPendingJobsLeft
		{
			get
			{
				return "All pending jobs have been put into processing.";
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00008AD7 File Offset: 0x00007AD7
		public virtual string Log_MailingWorkerThreadDone
		{
			get
			{
				return "Worker thread with hash {0} and ID {1} has finished all the tasks.";
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00008ADE File Offset: 0x00007ADE
		public virtual string Log_DnsCreatingQueryAboutHost0
		{
			get
			{
				return "Creating DNS query about host \"{0}\".";
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00008AE5 File Offset: 0x00007AE5
		public virtual string Log_DnsSendingQueryToEndPoint0
		{
			get
			{
				return "Sending query to DNS server at \"{0}\".";
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00008AEC File Offset: 0x00007AEC
		public virtual string Log_DnsParsingReceivedResponse
		{
			get
			{
				return "Parsing a response that was received from DNS server.";
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00008AF3 File Offset: 0x00007AF3
		public virtual string Log_DnsRecursionIsSupported
		{
			get
			{
				return "Recursion is supported by DNS server.";
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00008AFA File Offset: 0x00007AFA
		public virtual string Log_DnsRecursionIsNotSupported
		{
			get
			{
				return "Recursion is not supported by DNS server.";
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00008B01 File Offset: 0x00007B01
		public virtual string Log_DnsRecursionStatusUnknown
		{
			get
			{
				return "Recursion status is unknown for the given DNS server.";
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00008B08 File Offset: 0x00007B08
		public virtual string Log_Dns0RecordsFoundForHost1
		{
			get
			{
				return "{0} DNS record(s) found for host \"{1}\".";
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00008B0F File Offset: 0x00007B0F
		public virtual string Log_DnsRecordOfUnknownType
		{
			get
			{
				return "DNS record of unknown type detected.";
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00008B16 File Offset: 0x00007B16
		public virtual string Log_DnsRecordOfATypeHasIP0
		{
			get
			{
				return "DNS record of A type. IP={0}.";
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00008B1D File Offset: 0x00007B1D
		public virtual string Log_DnsRecordOfCNameTypeIsAliasFor0
		{
			get
			{
				return "DNS record of CNAME type. Alias for \"{0}\".";
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00008B24 File Offset: 0x00007B24
		public virtual string Log_DnsRecordOfTxtTypeHas0Strings
		{
			get
			{
				return "DNS record of TXT type. Has {0} strings.";
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00008B2B File Offset: 0x00007B2B
		public virtual string Log_DnsRecordOfPtrTypeDenotesDomain0
		{
			get
			{
				return "DNS record of PTR type. Denotes domain \"{0}\".";
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00008B32 File Offset: 0x00007B32
		public virtual string Log_DnsRecordOfMXTypeHasSmtpHost0OfPreference1
		{
			get
			{
				return "DNS record of MX type. SmtpHost=\"{0}\", Preference={1}.";
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00008B39 File Offset: 0x00007B39
		public virtual string Log_DnsQueryDone
		{
			get
			{
				return "DNS query done.";
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00008B40 File Offset: 0x00007B40
		public virtual string Log_Pop3WillTryApopAuth
		{
			get
			{
				return "Will try APOP authentication.";
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00008B47 File Offset: 0x00007B47
		public virtual string Log_Pop3ApopAuthNotSupported
		{
			get
			{
				return "APOP authentication is not supported by the server.";
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00008B4E File Offset: 0x00007B4E
		public virtual string Log_Pop3WillTryRegularAuth
		{
			get
			{
				return "Will try regular USER/PASS authentication.";
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00008B55 File Offset: 0x00007B55
		public virtual string Log_Pop3GetAdvertizedSaslMethodsViaAuth
		{
			get
			{
				return "Get the list of advertized SASL authentication methods via AUTH command.";
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00008B5C File Offset: 0x00007B5C
		public virtual string Pop3AuthCommandUnknown
		{
			get
			{
				return "AUTH command is not supported by the server.";
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00008B63 File Offset: 0x00007B63
		public virtual string Log_Pop3GetCapabilitiesViaCapa
		{
			get
			{
				return "Get the list of POP3 capabilities via CAPA command.";
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00008B6A File Offset: 0x00007B6A
		public virtual string Pop3CapaCommandUnknown
		{
			get
			{
				return "CAPA command is not supported by the server.";
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00008B71 File Offset: 0x00007B71
		public virtual string Log_Pop3DownloadStat
		{
			get
			{
				return "Download inbox statistics.";
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00008B78 File Offset: 0x00007B78
		public virtual string Log_Pop3DownloadList
		{
			get
			{
				return "Download the list of lengths of all messages in inbox.";
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00008B7F File Offset: 0x00007B7F
		public virtual string Log_Pop3DownloadUidl
		{
			get
			{
				return "Download the list of Unique-IDs of all messages in inbox.";
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00008B86 File Offset: 0x00007B86
		public virtual string Log_Pop3ResetDeletes
		{
			get
			{
				return "Cancel deletion of all messages marked as deleted.";
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00008B8D File Offset: 0x00007B8D
		public virtual string Log_Pop3WillDeleteMessageIndex0
		{
			get
			{
				return "Will mark message (index={0}) as deleted.";
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00008B94 File Offset: 0x00007B94
		public virtual string Log_Pop3DeletedMessageIndex0
		{
			get
			{
				return "Message (index={0}) marked as deleted.";
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00008B9B File Offset: 0x00007B9B
		public virtual string Log_Pop3NothingToDelete
		{
			get
			{
				return "No messages could be deleted from empty inbox.";
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00008BA2 File Offset: 0x00007BA2
		public virtual string Log_Pop3WillDeleteMessagesStartIndex0Count1
		{
			get
			{
				return "Will mark messages (startIndex={0}, count={1}) as deleted.";
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00008BA9 File Offset: 0x00007BA9
		public virtual string Log_Pop3DeletedMessagesStartIndex0Count1
		{
			get
			{
				return "Messages (startIndex={0}, count={1}) marked as deleted.";
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00008BB0 File Offset: 0x00007BB0
		public virtual string Log_Pop3EntireMessage
		{
			get
			{
				return "entire message";
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00008BB7 File Offset: 0x00007BB7
		public virtual string Log_Pop3MessageHeader
		{
			get
			{
				return "message header";
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00008BBE File Offset: 0x00007BBE
		public virtual string Log_Pop3MessageHeaderAnd0BodyLines
		{
			get
			{
				return "partial (header + {0} body lines) message";
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00008BC5 File Offset: 0x00007BC5
		public virtual string Log_Pop3EntireMessages
		{
			get
			{
				return "entire messages";
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00008BCC File Offset: 0x00007BCC
		public virtual string Log_Pop3MessageHeaders
		{
			get
			{
				return "message headers";
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00008BD3 File Offset: 0x00007BD3
		public virtual string Log_Pop3MessageHeadersAnd0BodyLines
		{
			get
			{
				return "partial (header + {0} body lines) messages";
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00008BDA File Offset: 0x00007BDA
		public virtual string Log_Pop3WillDownload0Index1
		{
			get
			{
				return "Will download {0} (index={1}).";
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00008BE1 File Offset: 0x00007BE1
		public virtual string Log_Pop3Downloaded0Index1
		{
			get
			{
				return "Downloaded {0} (index={1}).";
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00008BE8 File Offset: 0x00007BE8
		public virtual string Log_Pop3ZeroMessagesDownloadedFromEmptyInbox
		{
			get
			{
				return "Inbox was empty. Zero messages downloaded.";
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00008BEF File Offset: 0x00007BEF
		public virtual string Log_Pop3WillDownload0StartIndex1Count2
		{
			get
			{
				return "Will download {0} (startIndex={1}, count={2}).";
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00008BF6 File Offset: 0x00007BF6
		public virtual string Log_Pop3Downloaded0StartIndex1Count2
		{
			get
			{
				return "Downloaded {0} (startIndex={1}, count={2}).";
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00008BFD File Offset: 0x00007BFD
		public virtual string Log_ImapLiteralOfLength0
		{
			get
			{
				return "Literal of length {0}.";
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00008C04 File Offset: 0x00007C04
		public virtual string Log_ImapPreauthenticatedUser
		{
			get
			{
				return "Preauthenticated user";
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00008C0B File Offset: 0x00007C0B
		public virtual string Log_ImapGetCapabilitiesViaCapability
		{
			get
			{
				return "Get the list of IMAP4 capabilities via CAPABILITY command.";
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00008C12 File Offset: 0x00007C12
		public virtual string Log_ImapWillTryRegularAuth
		{
			get
			{
				return "Will try regular LOGIN authentication.";
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00008C19 File Offset: 0x00007C19
		public virtual string Log_ImapManageFolder0
		{
			get
			{
				return "Manage folder \"{0}\".";
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00008C20 File Offset: 0x00007C20
		public virtual string Log_ImapRenameFolder0To1
		{
			get
			{
				return "Rename folder \"{0}\" to \"{1}\".";
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00008C27 File Offset: 0x00007C27
		public virtual string Log_ImapSelectFolder0
		{
			get
			{
				return "Select folder \"{0}\".";
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00008C2E File Offset: 0x00007C2E
		public virtual string Log_ImapCloseFolder
		{
			get
			{
				return "Close folder.";
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00008C35 File Offset: 0x00007C35
		public virtual string Log_ImapExpunge
		{
			get
			{
				return "Expunge all deleted messages from the folder.";
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00008C3C File Offset: 0x00007C3C
		public virtual string Log_ImapExpunge0
		{
			get
			{
				return "Expunge messages with UIDs \"{0}\" from the folder.";
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600032E RID: 814 RVA: 0x00008C43 File Offset: 0x00007C43
		public virtual string Log_ImapNamespace
		{
			get
			{
				return "Get namespaces for the account.";
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00008C4A File Offset: 0x00007C4A
		public virtual string Log_ImapFolderStatus0
		{
			get
			{
				return "Get statistics for folder \"{0}\".";
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00008C51 File Offset: 0x00007C51
		public virtual string Log_ImapFolderQuota0
		{
			get
			{
				return "Obtain quota limits and usage for folder \"{0}\".";
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00008C58 File Offset: 0x00007C58
		public virtual string Log_ImapGettingQuotaFromList
		{
			get
			{
				return "Processing a single quota from quota list.";
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00008C5F File Offset: 0x00007C5F
		public virtual string Log_ImapMatchingQuotaFound
		{
			get
			{
				return "Matching quota found.";
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00008C66 File Offset: 0x00007C66
		public virtual string Log_ImapWillDownloadFoldersOf0Matching1
		{
			get
			{
				return "Will download list of sub-folders of \"{0}\" folder matching \"{1}\" condition.";
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00008C6D File Offset: 0x00007C6D
		public virtual string Log_ImapDownloadFoldersDone
		{
			get
			{
				return "Folder list downloaded.";
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00008C74 File Offset: 0x00007C74
		public virtual string Log_ImapWillSearch
		{
			get
			{
				return "Will perform search in the folder.";
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00008C7B File Offset: 0x00007C7B
		public virtual string Log_ImapSearchDone
		{
			get
			{
				return "Search done.";
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00008C82 File Offset: 0x00007C82
		public virtual string Log_ImapWillSort
		{
			get
			{
				return "Will perform sorted search in the folder.";
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00008C89 File Offset: 0x00007C89
		public virtual string Log_ImapSortDone
		{
			get
			{
				return "Sorted search done.";
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00008C90 File Offset: 0x00007C90
		public virtual string Log_ImapWillDownloadEnvelopes
		{
			get
			{
				return "Will download envelopes.";
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00008C97 File Offset: 0x00007C97
		public virtual string Log_ImapDownloadEnvelopesDone
		{
			get
			{
				return "Envelopes downloaded";
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00008C9E File Offset: 0x00007C9E
		public virtual string Log_ImapSetMessageFlags
		{
			get
			{
				return "Setting flags for messages.";
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00008CA5 File Offset: 0x00007CA5
		public virtual string Log_ImapWillUploadMessageTo0
		{
			get
			{
				return "Will upload message to folder \"{0}\".";
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00008CAC File Offset: 0x00007CAC
		public virtual string Log_ImapUploadMessageDone
		{
			get
			{
				return "Message uploaded.";
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00008CB3 File Offset: 0x00007CB3
		public virtual string Log_ImapWillCopyMessagesTo0
		{
			get
			{
				return "Will copy messages to folder \"{0}\".";
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00008CBA File Offset: 0x00007CBA
		public virtual string Log_ImapCopyMessagesDone
		{
			get
			{
				return "Messages copied.";
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00008CC1 File Offset: 0x00007CC1
		public virtual string Log_ImapWillMoveMessagesTo0
		{
			get
			{
				return "Will move messages to folder \"{0}\".";
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00008CC8 File Offset: 0x00007CC8
		public virtual string Log_ImapMoveMessagesDone
		{
			get
			{
				return "Messages moved.";
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00008CCF File Offset: 0x00007CCF
		public virtual string Log_ImapWillIdle
		{
			get
			{
				return "Will go idle.";
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00008CD6 File Offset: 0x00007CD6
		public virtual string Log_ImapWillFinishIdling
		{
			get
			{
				return "Will finish idling.";
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00008CDD File Offset: 0x00007CDD
		public virtual string Log_ImapIdleDone
		{
			get
			{
				return "Idle done.";
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00008CE4 File Offset: 0x00007CE4
		public virtual string Log_EwsOperationDone
		{
			get
			{
				return "Operation done.";
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00008CEB File Offset: 0x00007CEB
		public virtual string Log_EwsOperationDone0ItemsReturned
		{
			get
			{
				return "Operation done, {0} item(s) returned.";
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00008CF2 File Offset: 0x00007CF2
		public virtual string Log_EwsWillBindFolderId0
		{
			get
			{
				return "Will bind folder ID {0}.";
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00008CF9 File Offset: 0x00007CF9
		public virtual string Log_EwsWillFindFolders
		{
			get
			{
				return "Will find folders.";
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00008D00 File Offset: 0x00007D00
		public virtual string Log_EwsWillCreateFolder0
		{
			get
			{
				return "Will create folder \"{0}\".";
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00008D07 File Offset: 0x00007D07
		public virtual string Log_EwsWillCreateFolder0InId1
		{
			get
			{
				return "Will create folder \"{0}\" in folder ID {1}.";
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00008D0E File Offset: 0x00007D0E
		public virtual string Log_EwsWillRenameFolderTo0
		{
			get
			{
				return "Will rename folder to \"{0}\".";
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00008D15 File Offset: 0x00007D15
		public virtual string Log_EwsWillMoveFolderToId0
		{
			get
			{
				return "Will move folder to folder ID {0}.";
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00008D1C File Offset: 0x00007D1C
		public virtual string Log_EwsWillDeleteFolderUsingMethod0
		{
			get
			{
				return "Will delete folder using method {0}.";
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00008D23 File Offset: 0x00007D23
		public virtual string Log_EwsWillEmptyFolderUsingMethod0
		{
			get
			{
				return "Will empty folder using method {0}.";
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00008D2A File Offset: 0x00007D2A
		public virtual string Log_EwsWillFindItemsInFolderId0
		{
			get
			{
				return "Will find items in folder ID {0}.";
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00008D31 File Offset: 0x00007D31
		public virtual string Log_EwsWillLoadPropertiesForItems
		{
			get
			{
				return "Will load properties for items.";
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00008D38 File Offset: 0x00007D38
		public virtual string Log_EwsWillBindItemId0
		{
			get
			{
				return "Will bind item ID {0}.";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00008D3F File Offset: 0x00007D3F
		public virtual string Log_EwsWillGet0Attachments
		{
			get
			{
				return "Will get {0} attachment(s).";
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00008D46 File Offset: 0x00007D46
		public virtual string Log_Ews0AttachmentsReturned
		{
			get
			{
				return "{0} attachments returned.";
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00008D4D File Offset: 0x00007D4D
		public virtual string Log_EwsWillAddAttachmentAndUpdateItem
		{
			get
			{
				return "Will add attachment and update item.";
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00008D54 File Offset: 0x00007D54
		public virtual string Log_EwsWillDeleteAllAttachmentsAndUpdateItem
		{
			get
			{
				return "Will delete all attachments and update item.";
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00008D5B File Offset: 0x00007D5B
		public virtual string Log_EwsWillDelete0AttachmentsAndUpdateItem
		{
			get
			{
				return "Will delete {0} attachment(s) and update item.";
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00008D62 File Offset: 0x00007D62
		public virtual string Log_EwsAttachmentNotFound
		{
			get
			{
				return "Attachment not found.";
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008D69 File Offset: 0x00007D69
		public virtual string Log_EwsWillUpdateItemId0
		{
			get
			{
				return "Will update item ID {0}.";
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00008D70 File Offset: 0x00007D70
		public virtual string Log_EwsWillUpload0BytesMessageIntoFolderId1
		{
			get
			{
				return "Will upload {0} bytes message into folder ID {1}.";
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00008D77 File Offset: 0x00007D77
		public virtual string Log_EwsWillCopyItemToFolderId0
		{
			get
			{
				return "Will copy item to folder ID {0}.";
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00008D7E File Offset: 0x00007D7E
		public virtual string Log_EwsWillMoveItemToFolderId0
		{
			get
			{
				return "Will move item to folder ID {0}.";
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00008D85 File Offset: 0x00007D85
		public virtual string Log_EwsWillDeleteItemUsingMethod0
		{
			get
			{
				return "Will delete item using method {0}.";
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00008D8C File Offset: 0x00007D8C
		public virtual string Log_EwsWillDeleteItemsUsingMethod0
		{
			get
			{
				return "Will delete items using method {0}.";
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00008D93 File Offset: 0x00007D93
		public virtual string Log_EwsOperationDone0MessagesDeleted
		{
			get
			{
				return "Operation done, {0} message(s) deleted.";
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00008D9A File Offset: 0x00007D9A
		public virtual string Log_EwsWillSendEmail
		{
			get
			{
				return "Will send email.";
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00008DA1 File Offset: 0x00007DA1
		public virtual string Log_EwsWillSendEmailAndSaveCopy
		{
			get
			{
				return "Will send email and save copy.";
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00008DA8 File Offset: 0x00007DA8
		public virtual string Log_EwsWillSendEmailAndSaveCopyInFolderId0
		{
			get
			{
				return "Will send email and save copy in folder ID {0}.";
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00008DAF File Offset: 0x00007DAF
		public virtual string Log_EwsWillResolveName0
		{
			get
			{
				return "Will resolve name \"{0}\".";
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00008DB6 File Offset: 0x00007DB6
		public virtual string Log_EwsWillCheckFolderExistsByShortName0InParentFolderId1
		{
			get
			{
				return "Will check folder existance by short name \"{0}\" in parent folder ID {1}.";
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00008DBD File Offset: 0x00007DBD
		public virtual string Log_EwsWillCheckFolderExistsByFullName0
		{
			get
			{
				return "Will check folder existance by full name \"{0}\".";
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00008DC4 File Offset: 0x00007DC4
		public virtual string Log_EwsFolderFullName0AlreadyExists
		{
			get
			{
				return "Folder full name \"{0}\" already exists.";
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00008DCB File Offset: 0x00007DCB
		public virtual string Log_EwsWillDownloadFolderByShortName0InParentFolderId1
		{
			get
			{
				return "Will find and download folder by short name \"{0}\" in parent folder ID {1}.";
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00008DD2 File Offset: 0x00007DD2
		public virtual string Log_EwsWillFindFolderIdByShortName0InParentFolderId1
		{
			get
			{
				return "Will find FolderId by short name \"{0}\" in parent folder ID {1}.";
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00008DD9 File Offset: 0x00007DD9
		public virtual string Log_EwsWillDownloadFolderByFullName0InContainingFolderId1
		{
			get
			{
				return "Will find and download folder by full name \"{0}\" in containing folder ID {1}.";
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00008DE0 File Offset: 0x00007DE0
		public virtual string Log_EwsWillDownloadFolderByFullName0Recursively
		{
			get
			{
				return "Will find and download folder by full name \"{0}\" recursively.";
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00008DE7 File Offset: 0x00007DE7
		public virtual string Log_EwsDownloadFolderByFullNameRecursivelyFoundNothing
		{
			get
			{
				return "Finding folder by full name recursively found nothing";
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00008DEE File Offset: 0x00007DEE
		public virtual string Log_EwsWillRenameOrMoveFolderWithOldFullName0ToNewFullName1
		{
			get
			{
				return "Will rename or move folder with old full name \"{0}\" to new full name \"{1}\".";
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00008DF5 File Offset: 0x00007DF5
		public virtual string Log_EwsFolderFullName0NotFound
		{
			get
			{
				return "Folder full name \"{0}\" not found.";
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00008DFC File Offset: 0x00007DFC
		public virtual string Log_EwsFolderFullName0MatchedToId1
		{
			get
			{
				return "Folder full name \"{0}\" matched to folder ID {1}";
			}
		}

		// Token: 0x04000164 RID: 356
		private static Resources a = new Resources();
	}
}
