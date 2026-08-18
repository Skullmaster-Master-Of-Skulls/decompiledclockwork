using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace System.Web
{
	// Token: 0x02000024 RID: 36
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpBrowserCapabilitiesBase : IFilterResolutionService
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool ActiveXControls
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IDictionary Adapters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool AOL
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool BackgroundSounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Beta
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Browser
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual ArrayList Browsers
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanCombineFormsInDeck
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanInitiateVoiceCall
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanRenderAfterInputOrSelectElement
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanRenderEmptySelects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanRenderInputAndSelectElementsTogether
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanRenderMixedSelects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanRenderOneventAndPrevElementsTogether
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanRenderPostBackCards
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanRenderSetvarZeroWithMultiSelectionList
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CanSendMail
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual IDictionary Capabilities
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool CDF
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Version ClrVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Cookies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Crawler
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int DefaultSubmitButtonLimit
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Version EcmaScriptVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Frames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int GatewayMajorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual double GatewayMinorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string GatewayVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool HasBackButton
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool HidesRightAlignedMultiselectScrollbars
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string HtmlTextWriter
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Id
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string InputType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsMobileDevice
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool JavaApplets
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Version JScriptVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int MajorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int MaximumHrefLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int MaximumRenderedPageSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int MaximumSoftkeyLabelLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual double MinorVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string MinorVersionString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string MobileDeviceManufacturer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string MobileDeviceModel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Version MSDomVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int NumberOfSoftkeys
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Platform
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PreferredImageMime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PreferredRenderingMime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PreferredRenderingType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PreferredRequestEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string PreferredResponseEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RendersBreakBeforeWmlSelectAndInput
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RendersBreaksAfterHtmlLists
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RendersBreaksAfterWmlAnchor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RendersBreaksAfterWmlInput
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RendersWmlDoAcceptsInline
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RendersWmlSelectsAsMenuCards
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string RequiredMetaTagNameValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresAttributeColonSubstitution
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresContentTypeMetaTag
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresControlStateInSession
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresDBCSCharacter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresHtmlAdaptiveErrorReporting
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresLeadingPageBreak
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresNoBreakInFormatting
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresOutputOptimization
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresPhoneNumbersAsPlainText
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresSpecialViewStateEncoding
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresUniqueFilePathSuffix
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresUniqueHtmlCheckboxNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresUniqueHtmlInputNames
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool RequiresUrlEncodedPostfieldValues
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ScreenBitDepth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ScreenCharactersHeight
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ScreenCharactersWidth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ScreenPixelsHeight
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ScreenPixelsWidth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsAccesskeyAttribute
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsBodyColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsBold
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsCacheControlMetaTag
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsCallback
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsCss
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsDivAlign
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsDivNoWrap
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsEmptyStringInCookieValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsFontColor
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsFontName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsFontSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsImageSubmit
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsIModeSymbols
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsInputIStyle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsInputMode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsItalic
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsJPhoneMultiMediaAttributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsJPhoneSymbols
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsQueryStringInFormAction
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsRedirectWithCookie
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsSelectMultiple
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsUncheck
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool SupportsXmlHttp
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Tables
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Type TagWriter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Type
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool UseOptimizedCacheKey
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool VBScript
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string Version
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Version W3CDomVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Win16
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool Win32
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000A5 RID: 165
		public virtual string this[string key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void AddBrowser(string browserName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void DisableOptimizedCacheKey()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Version[] GetClrVersions()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool IsBrowser(string browserName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int CompareFilters(string filter1, string filter2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual bool EvaluateFilter(string filterName)
		{
			throw new NotImplementedException();
		}
	}
}
