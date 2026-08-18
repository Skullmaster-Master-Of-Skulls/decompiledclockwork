using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Net.Http.Properties
{
	// Token: 0x0200003B RID: 59
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060001DE RID: 478 RVA: 0x00007F1A File Offset: 0x0000611A
		internal Resources()
		{
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00007F24 File Offset: 0x00006124
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					Assembly assembly = typeof(Resources).Assembly;
					ResourceManager resourceManager = new ResourceManager("System.Net.Http.Properties.Resources", assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007F65 File Offset: 0x00006165
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00007F6C File Offset: 0x0000616C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007F74 File Offset: 0x00006174
		internal static string AsyncResult_CallbackThrewException
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_CallbackThrewException", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00007F8A File Offset: 0x0000618A
		internal static string AsyncResult_MultipleCompletes
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_MultipleCompletes", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007FA0 File Offset: 0x000061A0
		internal static string AsyncResult_MultipleEnds
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_MultipleEnds", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007FB6 File Offset: 0x000061B6
		internal static string AsyncResult_ResultMismatch
		{
			get
			{
				return Resources.ResourceManager.GetString("AsyncResult_ResultMismatch", Resources.resourceCulture);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007FCC File Offset: 0x000061CC
		internal static string ByteRangeStreamContentNoRanges
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamContentNoRanges", Resources.resourceCulture);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00007FE2 File Offset: 0x000061E2
		internal static string ByteRangeStreamContentNotBytesRange
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamContentNotBytesRange", Resources.resourceCulture);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007FF8 File Offset: 0x000061F8
		internal static string ByteRangeStreamEmpty
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamEmpty", Resources.resourceCulture);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000800E File Offset: 0x0000620E
		internal static string ByteRangeStreamInvalidFrom
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamInvalidFrom", Resources.resourceCulture);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008024 File Offset: 0x00006224
		internal static string ByteRangeStreamNoneOverlap
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamNoneOverlap", Resources.resourceCulture);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000803A File Offset: 0x0000623A
		internal static string ByteRangeStreamNoOverlap
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamNoOverlap", Resources.resourceCulture);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008050 File Offset: 0x00006250
		internal static string ByteRangeStreamNotSeekable
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamNotSeekable", Resources.resourceCulture);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00008066 File Offset: 0x00006266
		internal static string ByteRangeStreamReadOnly
		{
			get
			{
				return Resources.ResourceManager.GetString("ByteRangeStreamReadOnly", Resources.resourceCulture);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000807C File Offset: 0x0000627C
		internal static string CannotHaveNullInList
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotHaveNullInList", Resources.resourceCulture);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00008092 File Offset: 0x00006292
		internal static string CannotUseMediaRangeForSupportedMediaType
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotUseMediaRangeForSupportedMediaType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000080A8 File Offset: 0x000062A8
		internal static string CannotUseNullValueType
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotUseNullValueType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000080BE File Offset: 0x000062BE
		internal static string CookieInvalidName
		{
			get
			{
				return Resources.ResourceManager.GetString("CookieInvalidName", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000080D4 File Offset: 0x000062D4
		internal static string CookieNull
		{
			get
			{
				return Resources.ResourceManager.GetString("CookieNull", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x000080EA File Offset: 0x000062EA
		internal static string DelegatingHandlerArrayContainsNullItem
		{
			get
			{
				return Resources.ResourceManager.GetString("DelegatingHandlerArrayContainsNullItem", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008100 File Offset: 0x00006300
		internal static string DelegatingHandlerArrayHasNonNullInnerHandler
		{
			get
			{
				return Resources.ResourceManager.GetString("DelegatingHandlerArrayHasNonNullInnerHandler", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00008116 File Offset: 0x00006316
		internal static string ErrorReadingFormUrlEncodedStream
		{
			get
			{
				return Resources.ResourceManager.GetString("ErrorReadingFormUrlEncodedStream", Resources.resourceCulture);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000812C File Offset: 0x0000632C
		internal static string FormUrlEncodedMismatchingTypes
		{
			get
			{
				return Resources.ResourceManager.GetString("FormUrlEncodedMismatchingTypes", Resources.resourceCulture);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00008142 File Offset: 0x00006342
		internal static string FormUrlEncodedParseError
		{
			get
			{
				return Resources.ResourceManager.GetString("FormUrlEncodedParseError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00008158 File Offset: 0x00006358
		internal static string HttpInvalidStatusCode
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpInvalidStatusCode", Resources.resourceCulture);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000816E File Offset: 0x0000636E
		internal static string HttpInvalidVersion
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpInvalidVersion", Resources.resourceCulture);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00008184 File Offset: 0x00006384
		internal static string HttpMessageContentAlreadyRead
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageContentAlreadyRead", Resources.resourceCulture);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000819A File Offset: 0x0000639A
		internal static string HttpMessageContentStreamMustBeSeekable
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageContentStreamMustBeSeekable", Resources.resourceCulture);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000081B0 File Offset: 0x000063B0
		internal static string HttpMessageErrorReading
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageErrorReading", Resources.resourceCulture);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000081C6 File Offset: 0x000063C6
		internal static string HttpMessageInvalidMediaType
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageInvalidMediaType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001FE RID: 510 RVA: 0x000081DC File Offset: 0x000063DC
		internal static string HttpMessageParserEmptyUri
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserEmptyUri", Resources.resourceCulture);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000081F2 File Offset: 0x000063F2
		internal static string HttpMessageParserError
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008208 File Offset: 0x00006408
		internal static string HttpMessageParserInvalidHostCount
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserInvalidHostCount", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000821E File Offset: 0x0000641E
		internal static string HttpMessageParserInvalidUriScheme
		{
			get
			{
				return Resources.ResourceManager.GetString("HttpMessageParserInvalidUriScheme", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00008234 File Offset: 0x00006434
		internal static string InvalidArrayInsert
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidArrayInsert", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000824A File Offset: 0x0000644A
		internal static string JQuery13CompatModeNotSupportNestedJson
		{
			get
			{
				return Resources.ResourceManager.GetString("JQuery13CompatModeNotSupportNestedJson", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008260 File Offset: 0x00006460
		internal static string JsonSerializerFactoryReturnedNull
		{
			get
			{
				return Resources.ResourceManager.GetString("JsonSerializerFactoryReturnedNull", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00008276 File Offset: 0x00006476
		internal static string JsonSerializerFactoryThrew
		{
			get
			{
				return Resources.ResourceManager.GetString("JsonSerializerFactoryThrew", Resources.resourceCulture);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000828C File Offset: 0x0000648C
		internal static string MaxDepthExceeded
		{
			get
			{
				return Resources.ResourceManager.GetString("MaxDepthExceeded", Resources.resourceCulture);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000082A2 File Offset: 0x000064A2
		internal static string MaxHttpCollectionKeyLimitReached
		{
			get
			{
				return Resources.ResourceManager.GetString("MaxHttpCollectionKeyLimitReached", Resources.resourceCulture);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000082B8 File Offset: 0x000064B8
		internal static string MediaTypeFormatter_BsonParseError_MissingData
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_BsonParseError_MissingData", Resources.resourceCulture);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000082CE File Offset: 0x000064CE
		internal static string MediaTypeFormatter_BsonParseError_UnexpectedData
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_BsonParseError_UnexpectedData", Resources.resourceCulture);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000082E4 File Offset: 0x000064E4
		internal static string MediaTypeFormatter_JsonReaderFactoryReturnedNull
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_JsonReaderFactoryReturnedNull", Resources.resourceCulture);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000082FA File Offset: 0x000064FA
		internal static string MediaTypeFormatter_JsonWriterFactoryReturnedNull
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatter_JsonWriterFactoryReturnedNull", Resources.resourceCulture);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00008310 File Offset: 0x00006510
		internal static string MediaTypeFormatterCannotRead
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotRead", Resources.resourceCulture);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00008326 File Offset: 0x00006526
		internal static string MediaTypeFormatterCannotReadSync
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotReadSync", Resources.resourceCulture);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000833C File Offset: 0x0000653C
		internal static string MediaTypeFormatterCannotWrite
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotWrite", Resources.resourceCulture);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00008352 File Offset: 0x00006552
		internal static string MediaTypeFormatterCannotWriteSync
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterCannotWriteSync", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008368 File Offset: 0x00006568
		internal static string MediaTypeFormatterNoEncoding
		{
			get
			{
				return Resources.ResourceManager.GetString("MediaTypeFormatterNoEncoding", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000837E File Offset: 0x0000657E
		internal static string MimeMultipartParserBadBoundary
		{
			get
			{
				return Resources.ResourceManager.GetString("MimeMultipartParserBadBoundary", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008394 File Offset: 0x00006594
		internal static string MultipartFormDataStreamProviderNoContentDisposition
		{
			get
			{
				return Resources.ResourceManager.GetString("MultipartFormDataStreamProviderNoContentDisposition", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000083AA File Offset: 0x000065AA
		internal static string MultipartStreamProviderInvalidLocalFileName
		{
			get
			{
				return Resources.ResourceManager.GetString("MultipartStreamProviderInvalidLocalFileName", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000083C0 File Offset: 0x000065C0
		internal static string NestedBracketNotValid
		{
			get
			{
				return Resources.ResourceManager.GetString("NestedBracketNotValid", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000083D6 File Offset: 0x000065D6
		internal static string NonNullUriRequiredForMediaTypeMapping
		{
			get
			{
				return Resources.ResourceManager.GetString("NonNullUriRequiredForMediaTypeMapping", Resources.resourceCulture);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000083EC File Offset: 0x000065EC
		internal static string NoReadSerializerAvailable
		{
			get
			{
				return Resources.ResourceManager.GetString("NoReadSerializerAvailable", Resources.resourceCulture);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00008402 File Offset: 0x00006602
		internal static string ObjectAndTypeDisagree
		{
			get
			{
				return Resources.ResourceManager.GetString("ObjectAndTypeDisagree", Resources.resourceCulture);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008418 File Offset: 0x00006618
		internal static string ObjectContent_FormatterCannotWriteType
		{
			get
			{
				return Resources.ResourceManager.GetString("ObjectContent_FormatterCannotWriteType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000842E File Offset: 0x0000662E
		internal static string QueryStringNameShouldNotNull
		{
			get
			{
				return Resources.ResourceManager.GetString("QueryStringNameShouldNotNull", Resources.resourceCulture);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00008444 File Offset: 0x00006644
		internal static string ReadAsHttpMessageUnexpectedTermination
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsHttpMessageUnexpectedTermination", Resources.resourceCulture);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000845A File Offset: 0x0000665A
		internal static string ReadAsMimeMultipartArgumentNoBoundary
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartArgumentNoBoundary", Resources.resourceCulture);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008470 File Offset: 0x00006670
		internal static string ReadAsMimeMultipartArgumentNoContentType
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartArgumentNoContentType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008486 File Offset: 0x00006686
		internal static string ReadAsMimeMultipartArgumentNoMultipart
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartArgumentNoMultipart", Resources.resourceCulture);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000849C File Offset: 0x0000669C
		internal static string ReadAsMimeMultipartErrorReading
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartErrorReading", Resources.resourceCulture);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000084B2 File Offset: 0x000066B2
		internal static string ReadAsMimeMultipartErrorWriting
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartErrorWriting", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000084C8 File Offset: 0x000066C8
		internal static string ReadAsMimeMultipartHeaderParseError
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartHeaderParseError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000084DE File Offset: 0x000066DE
		internal static string ReadAsMimeMultipartParseError
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartParseError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000084F4 File Offset: 0x000066F4
		internal static string ReadAsMimeMultipartStreamProviderException
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartStreamProviderException", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000850A File Offset: 0x0000670A
		internal static string ReadAsMimeMultipartStreamProviderNull
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartStreamProviderNull", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008520 File Offset: 0x00006720
		internal static string ReadAsMimeMultipartStreamProviderReadOnly
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartStreamProviderReadOnly", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008536 File Offset: 0x00006736
		internal static string ReadAsMimeMultipartUnexpectedTermination
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadAsMimeMultipartUnexpectedTermination", Resources.resourceCulture);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000854C File Offset: 0x0000674C
		internal static string RemoteStreamInfoCannotBeNull
		{
			get
			{
				return Resources.ResourceManager.GetString("RemoteStreamInfoCannotBeNull", Resources.resourceCulture);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00008562 File Offset: 0x00006762
		internal static string SerializerCannotSerializeType
		{
			get
			{
				return Resources.ResourceManager.GetString("SerializerCannotSerializeType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008578 File Offset: 0x00006778
		internal static string UnMatchedBracketNotValid
		{
			get
			{
				return Resources.ResourceManager.GetString("UnMatchedBracketNotValid", Resources.resourceCulture);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000858E File Offset: 0x0000678E
		internal static string UnsupportedIndent
		{
			get
			{
				return Resources.ResourceManager.GetString("UnsupportedIndent", Resources.resourceCulture);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000085A4 File Offset: 0x000067A4
		internal static string XmlMediaTypeFormatter_InvalidSerializerType
		{
			get
			{
				return Resources.ResourceManager.GetString("XmlMediaTypeFormatter_InvalidSerializerType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000085BA File Offset: 0x000067BA
		internal static string XmlMediaTypeFormatter_NullReturnedSerializer
		{
			get
			{
				return Resources.ResourceManager.GetString("XmlMediaTypeFormatter_NullReturnedSerializer", Resources.resourceCulture);
			}
		}

		// Token: 0x04000084 RID: 132
		private static ResourceManager resourceMan;

		// Token: 0x04000085 RID: 133
		private static CultureInfo resourceCulture;
	}
}
