using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace System.Web
{
	// Token: 0x02000025 RID: 37
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpBrowserCapabilitiesWrapper : HttpBrowserCapabilitiesBase
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000447A File Offset: 0x0000267A
		public HttpBrowserCapabilitiesWrapper(HttpBrowserCapabilities httpBrowserCapabilities)
		{
			if (httpBrowserCapabilities == null)
			{
				throw new ArgumentNullException("httpBrowserCapabilities");
			}
			this._browser = httpBrowserCapabilities;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00004497 File Offset: 0x00002697
		public override string Browser
		{
			get
			{
				return this._browser.Browser;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000044A4 File Offset: 0x000026A4
		public override Version EcmaScriptVersion
		{
			get
			{
				return this._browser.EcmaScriptVersion;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000044B1 File Offset: 0x000026B1
		public override Version JScriptVersion
		{
			get
			{
				return this._browser.JScriptVersion;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000044BE File Offset: 0x000026BE
		public override bool SupportsCallback
		{
			get
			{
				return this._browser.SupportsCallback;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000044CB File Offset: 0x000026CB
		public override Version W3CDomVersion
		{
			get
			{
				return this._browser.W3CDomVersion;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000044D8 File Offset: 0x000026D8
		public override bool ActiveXControls
		{
			get
			{
				return this._browser.ActiveXControls;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000044E5 File Offset: 0x000026E5
		public override IDictionary Adapters
		{
			get
			{
				return this._browser.Adapters;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000044F2 File Offset: 0x000026F2
		public override bool AOL
		{
			get
			{
				return this._browser.AOL;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000044FF File Offset: 0x000026FF
		public override bool BackgroundSounds
		{
			get
			{
				return this._browser.BackgroundSounds;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000450C File Offset: 0x0000270C
		public override bool Beta
		{
			get
			{
				return this._browser.Beta;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004519 File Offset: 0x00002719
		public override ArrayList Browsers
		{
			get
			{
				return this._browser.Browsers;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004526 File Offset: 0x00002726
		public override bool CanCombineFormsInDeck
		{
			get
			{
				return this._browser.CanCombineFormsInDeck;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004533 File Offset: 0x00002733
		public override bool CanInitiateVoiceCall
		{
			get
			{
				return this._browser.CanInitiateVoiceCall;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004540 File Offset: 0x00002740
		public override bool CanRenderAfterInputOrSelectElement
		{
			get
			{
				return this._browser.CanRenderAfterInputOrSelectElement;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000454D File Offset: 0x0000274D
		public override bool CanRenderEmptySelects
		{
			get
			{
				return this._browser.CanRenderEmptySelects;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000455A File Offset: 0x0000275A
		public override bool CanRenderInputAndSelectElementsTogether
		{
			get
			{
				return this._browser.CanRenderInputAndSelectElementsTogether;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004567 File Offset: 0x00002767
		public override bool CanRenderMixedSelects
		{
			get
			{
				return this._browser.CanRenderMixedSelects;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004574 File Offset: 0x00002774
		public override bool CanRenderOneventAndPrevElementsTogether
		{
			get
			{
				return this._browser.CanRenderOneventAndPrevElementsTogether;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00004581 File Offset: 0x00002781
		public override bool CanRenderPostBackCards
		{
			get
			{
				return this._browser.CanRenderPostBackCards;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000458E File Offset: 0x0000278E
		public override bool CanRenderSetvarZeroWithMultiSelectionList
		{
			get
			{
				return this._browser.CanRenderSetvarZeroWithMultiSelectionList;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000459B File Offset: 0x0000279B
		public override bool CanSendMail
		{
			get
			{
				return this._browser.CanSendMail;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000045A8 File Offset: 0x000027A8
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000045B5 File Offset: 0x000027B5
		public override IDictionary Capabilities
		{
			get
			{
				return this._browser.Capabilities;
			}
			set
			{
				this._browser.Capabilities = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000045C3 File Offset: 0x000027C3
		public override bool CDF
		{
			get
			{
				return this._browser.CDF;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000045D0 File Offset: 0x000027D0
		public override Version ClrVersion
		{
			get
			{
				return this._browser.ClrVersion;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000045DD File Offset: 0x000027DD
		public override bool Cookies
		{
			get
			{
				return this._browser.Cookies;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000045EA File Offset: 0x000027EA
		public override bool Crawler
		{
			get
			{
				return this._browser.Crawler;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000045F7 File Offset: 0x000027F7
		public override int DefaultSubmitButtonLimit
		{
			get
			{
				return this._browser.DefaultSubmitButtonLimit;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004604 File Offset: 0x00002804
		public override bool Frames
		{
			get
			{
				return this._browser.Frames;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00004611 File Offset: 0x00002811
		public override int GatewayMajorVersion
		{
			get
			{
				return this._browser.GatewayMajorVersion;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000461E File Offset: 0x0000281E
		public override double GatewayMinorVersion
		{
			get
			{
				return this._browser.GatewayMinorVersion;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000462B File Offset: 0x0000282B
		public override string GatewayVersion
		{
			get
			{
				return this._browser.GatewayVersion;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00004638 File Offset: 0x00002838
		public override bool HasBackButton
		{
			get
			{
				return this._browser.HasBackButton;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00004645 File Offset: 0x00002845
		public override bool HidesRightAlignedMultiselectScrollbars
		{
			get
			{
				return this._browser.HidesRightAlignedMultiselectScrollbars;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00004652 File Offset: 0x00002852
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000465F File Offset: 0x0000285F
		public override string HtmlTextWriter
		{
			get
			{
				return this._browser.HtmlTextWriter;
			}
			set
			{
				this._browser.HtmlTextWriter = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000466D File Offset: 0x0000286D
		public override string Id
		{
			get
			{
				return this._browser.Id;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000467A File Offset: 0x0000287A
		public override string InputType
		{
			get
			{
				return this._browser.InputType;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00004687 File Offset: 0x00002887
		public override bool IsColor
		{
			get
			{
				return this._browser.IsColor;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004694 File Offset: 0x00002894
		public override bool IsMobileDevice
		{
			get
			{
				return this._browser.IsMobileDevice;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000046A1 File Offset: 0x000028A1
		public override bool JavaApplets
		{
			get
			{
				return this._browser.JavaApplets;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000046AE File Offset: 0x000028AE
		public override int MajorVersion
		{
			get
			{
				return this._browser.MajorVersion;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x000046BB File Offset: 0x000028BB
		public override int MaximumHrefLength
		{
			get
			{
				return this._browser.MaximumHrefLength;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000046C8 File Offset: 0x000028C8
		public override int MaximumRenderedPageSize
		{
			get
			{
				return this._browser.MaximumRenderedPageSize;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000046D5 File Offset: 0x000028D5
		public override int MaximumSoftkeyLabelLength
		{
			get
			{
				return this._browser.MaximumSoftkeyLabelLength;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000046E2 File Offset: 0x000028E2
		public override double MinorVersion
		{
			get
			{
				return this._browser.MinorVersion;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000046EF File Offset: 0x000028EF
		public override string MinorVersionString
		{
			get
			{
				return this._browser.MinorVersionString;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000046FC File Offset: 0x000028FC
		public override string MobileDeviceManufacturer
		{
			get
			{
				return this._browser.MobileDeviceManufacturer;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00004709 File Offset: 0x00002909
		public override string MobileDeviceModel
		{
			get
			{
				return this._browser.MobileDeviceModel;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004716 File Offset: 0x00002916
		public override Version MSDomVersion
		{
			get
			{
				return this._browser.MSDomVersion;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004723 File Offset: 0x00002923
		public override int NumberOfSoftkeys
		{
			get
			{
				return this._browser.NumberOfSoftkeys;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00004730 File Offset: 0x00002930
		public override string Platform
		{
			get
			{
				return this._browser.Platform;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000473D File Offset: 0x0000293D
		public override string PreferredImageMime
		{
			get
			{
				return this._browser.PreferredImageMime;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000474A File Offset: 0x0000294A
		public override string PreferredRenderingMime
		{
			get
			{
				return this._browser.PreferredRenderingMime;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00004757 File Offset: 0x00002957
		public override string PreferredRenderingType
		{
			get
			{
				return this._browser.PreferredRenderingType;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004764 File Offset: 0x00002964
		public override string PreferredRequestEncoding
		{
			get
			{
				return this._browser.PreferredRequestEncoding;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004771 File Offset: 0x00002971
		public override string PreferredResponseEncoding
		{
			get
			{
				return this._browser.PreferredResponseEncoding;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000477E File Offset: 0x0000297E
		public override bool RendersBreakBeforeWmlSelectAndInput
		{
			get
			{
				return this._browser.RendersBreakBeforeWmlSelectAndInput;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000478B File Offset: 0x0000298B
		public override bool RendersBreaksAfterHtmlLists
		{
			get
			{
				return this._browser.RendersBreaksAfterHtmlLists;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00004798 File Offset: 0x00002998
		public override bool RendersBreaksAfterWmlAnchor
		{
			get
			{
				return this._browser.RendersBreaksAfterWmlAnchor;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000047A5 File Offset: 0x000029A5
		public override bool RendersBreaksAfterWmlInput
		{
			get
			{
				return this._browser.RendersBreaksAfterWmlInput;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000047B2 File Offset: 0x000029B2
		public override bool RendersWmlDoAcceptsInline
		{
			get
			{
				return this._browser.RendersWmlDoAcceptsInline;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000047BF File Offset: 0x000029BF
		public override bool RendersWmlSelectsAsMenuCards
		{
			get
			{
				return this._browser.RendersWmlSelectsAsMenuCards;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000047CC File Offset: 0x000029CC
		public override string RequiredMetaTagNameValue
		{
			get
			{
				return this._browser.RequiredMetaTagNameValue;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000047D9 File Offset: 0x000029D9
		public override bool RequiresAttributeColonSubstitution
		{
			get
			{
				return this._browser.RequiresAttributeColonSubstitution;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000047E6 File Offset: 0x000029E6
		public override bool RequiresContentTypeMetaTag
		{
			get
			{
				return this._browser.RequiresContentTypeMetaTag;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000047F3 File Offset: 0x000029F3
		public override bool RequiresControlStateInSession
		{
			get
			{
				return this._browser.RequiresControlStateInSession;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00004800 File Offset: 0x00002A00
		public override bool RequiresDBCSCharacter
		{
			get
			{
				return this._browser.RequiresDBCSCharacter;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000480D File Offset: 0x00002A0D
		public override bool RequiresHtmlAdaptiveErrorReporting
		{
			get
			{
				return this._browser.RequiresHtmlAdaptiveErrorReporting;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000481A File Offset: 0x00002A1A
		public override bool RequiresLeadingPageBreak
		{
			get
			{
				return this._browser.RequiresLeadingPageBreak;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00004827 File Offset: 0x00002A27
		public override bool RequiresNoBreakInFormatting
		{
			get
			{
				return this._browser.RequiresNoBreakInFormatting;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00004834 File Offset: 0x00002A34
		public override bool RequiresOutputOptimization
		{
			get
			{
				return this._browser.RequiresOutputOptimization;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00004841 File Offset: 0x00002A41
		public override bool RequiresPhoneNumbersAsPlainText
		{
			get
			{
				return this._browser.RequiresPhoneNumbersAsPlainText;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000484E File Offset: 0x00002A4E
		public override bool RequiresSpecialViewStateEncoding
		{
			get
			{
				return this._browser.RequiresSpecialViewStateEncoding;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000485B File Offset: 0x00002A5B
		public override bool RequiresUniqueFilePathSuffix
		{
			get
			{
				return this._browser.RequiresUniqueFilePathSuffix;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00004868 File Offset: 0x00002A68
		public override bool RequiresUniqueHtmlCheckboxNames
		{
			get
			{
				return this._browser.RequiresUniqueHtmlCheckboxNames;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00004875 File Offset: 0x00002A75
		public override bool RequiresUniqueHtmlInputNames
		{
			get
			{
				return this._browser.RequiresUniqueHtmlInputNames;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004882 File Offset: 0x00002A82
		public override bool RequiresUrlEncodedPostfieldValues
		{
			get
			{
				return this._browser.RequiresUrlEncodedPostfieldValues;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000488F File Offset: 0x00002A8F
		public override int ScreenBitDepth
		{
			get
			{
				return this._browser.ScreenBitDepth;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000489C File Offset: 0x00002A9C
		public override int ScreenCharactersHeight
		{
			get
			{
				return this._browser.ScreenCharactersHeight;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000048A9 File Offset: 0x00002AA9
		public override int ScreenCharactersWidth
		{
			get
			{
				return this._browser.ScreenCharactersWidth;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000048B6 File Offset: 0x00002AB6
		public override int ScreenPixelsHeight
		{
			get
			{
				return this._browser.ScreenPixelsHeight;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000048C3 File Offset: 0x00002AC3
		public override int ScreenPixelsWidth
		{
			get
			{
				return this._browser.ScreenPixelsWidth;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000048D0 File Offset: 0x00002AD0
		public override bool SupportsAccesskeyAttribute
		{
			get
			{
				return this._browser.SupportsAccesskeyAttribute;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000048DD File Offset: 0x00002ADD
		public override bool SupportsBodyColor
		{
			get
			{
				return this._browser.SupportsBodyColor;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x000048EA File Offset: 0x00002AEA
		public override bool SupportsBold
		{
			get
			{
				return this._browser.SupportsBold;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x000048F7 File Offset: 0x00002AF7
		public override bool SupportsCacheControlMetaTag
		{
			get
			{
				return this._browser.SupportsCacheControlMetaTag;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00004904 File Offset: 0x00002B04
		public override bool SupportsCss
		{
			get
			{
				return this._browser.SupportsCss;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00004911 File Offset: 0x00002B11
		public override bool SupportsDivAlign
		{
			get
			{
				return this._browser.SupportsDivAlign;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000491E File Offset: 0x00002B1E
		public override bool SupportsDivNoWrap
		{
			get
			{
				return this._browser.SupportsDivNoWrap;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000492B File Offset: 0x00002B2B
		public override bool SupportsEmptyStringInCookieValue
		{
			get
			{
				return this._browser.SupportsEmptyStringInCookieValue;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00004938 File Offset: 0x00002B38
		public override bool SupportsFontColor
		{
			get
			{
				return this._browser.SupportsFontColor;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00004945 File Offset: 0x00002B45
		public override bool SupportsFontName
		{
			get
			{
				return this._browser.SupportsFontName;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00004952 File Offset: 0x00002B52
		public override bool SupportsFontSize
		{
			get
			{
				return this._browser.SupportsFontSize;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000495F File Offset: 0x00002B5F
		public override bool SupportsImageSubmit
		{
			get
			{
				return this._browser.SupportsImageSubmit;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000496C File Offset: 0x00002B6C
		public override bool SupportsIModeSymbols
		{
			get
			{
				return this._browser.SupportsIModeSymbols;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00004979 File Offset: 0x00002B79
		public override bool SupportsInputIStyle
		{
			get
			{
				return this._browser.SupportsInputIStyle;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00004986 File Offset: 0x00002B86
		public override bool SupportsInputMode
		{
			get
			{
				return this._browser.SupportsInputMode;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00004993 File Offset: 0x00002B93
		public override bool SupportsItalic
		{
			get
			{
				return this._browser.SupportsItalic;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000049A0 File Offset: 0x00002BA0
		public override bool SupportsJPhoneMultiMediaAttributes
		{
			get
			{
				return this._browser.SupportsJPhoneMultiMediaAttributes;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000049AD File Offset: 0x00002BAD
		public override bool SupportsJPhoneSymbols
		{
			get
			{
				return this._browser.SupportsJPhoneSymbols;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000049BA File Offset: 0x00002BBA
		public override bool SupportsQueryStringInFormAction
		{
			get
			{
				return this._browser.SupportsQueryStringInFormAction;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000202 RID: 514 RVA: 0x000049C7 File Offset: 0x00002BC7
		public override bool SupportsRedirectWithCookie
		{
			get
			{
				return this._browser.SupportsRedirectWithCookie;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000049D4 File Offset: 0x00002BD4
		public override bool SupportsSelectMultiple
		{
			get
			{
				return this._browser.SupportsSelectMultiple;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000049E1 File Offset: 0x00002BE1
		public override bool SupportsUncheck
		{
			get
			{
				return this._browser.SupportsUncheck;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000049EE File Offset: 0x00002BEE
		public override bool SupportsXmlHttp
		{
			get
			{
				return this._browser.SupportsXmlHttp;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000049FB File Offset: 0x00002BFB
		public override bool Tables
		{
			get
			{
				return this._browser.Tables;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00004A08 File Offset: 0x00002C08
		public override Type TagWriter
		{
			get
			{
				return this._browser.TagWriter;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00004A15 File Offset: 0x00002C15
		public override string Type
		{
			get
			{
				return this._browser.Type;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00004A22 File Offset: 0x00002C22
		public override bool UseOptimizedCacheKey
		{
			get
			{
				return this._browser.UseOptimizedCacheKey;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00004A2F File Offset: 0x00002C2F
		public override bool VBScript
		{
			get
			{
				return this._browser.VBScript;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00004A3C File Offset: 0x00002C3C
		public override string Version
		{
			get
			{
				return this._browser.Version;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00004A49 File Offset: 0x00002C49
		public override bool Win16
		{
			get
			{
				return this._browser.Win16;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00004A56 File Offset: 0x00002C56
		public override bool Win32
		{
			get
			{
				return this._browser.Win32;
			}
		}

		// Token: 0x17000116 RID: 278
		public override string this[string key]
		{
			get
			{
				return this._browser[key];
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00004A71 File Offset: 0x00002C71
		public override void AddBrowser(string browserName)
		{
			this._browser.AddBrowser(browserName);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00004A7F File Offset: 0x00002C7F
		public override HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
		{
			return this._browser.CreateHtmlTextWriter(w);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00004A8D File Offset: 0x00002C8D
		public override void DisableOptimizedCacheKey()
		{
			this._browser.DisableOptimizedCacheKey();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00004A9A File Offset: 0x00002C9A
		public override Version[] GetClrVersions()
		{
			return this._browser.GetClrVersions();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00004AA7 File Offset: 0x00002CA7
		public override bool IsBrowser(string browserName)
		{
			return this._browser.IsBrowser(browserName);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00004AB5 File Offset: 0x00002CB5
		public override int CompareFilters(string filter1, string filter2)
		{
			return ((IFilterResolutionService)this._browser).CompareFilters(filter1, filter2);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public override bool EvaluateFilter(string filterName)
		{
			return ((IFilterResolutionService)this._browser).EvaluateFilter(filterName);
		}

		// Token: 0x04000108 RID: 264
		private HttpBrowserCapabilities _browser;
	}
}
