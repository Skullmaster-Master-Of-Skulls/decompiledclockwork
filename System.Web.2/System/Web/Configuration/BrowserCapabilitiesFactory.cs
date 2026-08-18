using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;

namespace System.Web.Configuration
{
	// Token: 0x020006A5 RID: 1701
	public class BrowserCapabilitiesFactory : BrowserCapabilitiesFactoryBase
	{
		// Token: 0x060051E0 RID: 20960 RVA: 0x0011BCAC File Offset: 0x00119EAC
		public override void ConfigureBrowserCapabilities(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			this.DefaultProcess(headers, browserCaps);
			if (!base.IsBrowserUnknown(browserCaps))
			{
				return;
			}
			this.DefaultDefaultProcess(headers, browserCaps);
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IeProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IeProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051E3 RID: 20963 RVA: 0x0011BCCC File Offset: 0x00119ECC
		private bool IeProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "MSIE (?'version'(?'major'\\d+)(\\.(?'minor'\\d+)?)(?'letters'\\w*))(?'extra'[^)]*)"))
			{
				return false;
			}
			target = browserCaps[string.Empty];
			bool flag = regexWorker.ProcessRegex(target, "IEMobile");
			if (flag)
			{
				return false;
			}
			regexWorker.ProcessRegex(browserCaps[string.Empty], "Trident/(?'layoutVersion'\\d+)");
			capabilities["browser"] = "IE";
			capabilities["layoutEngine"] = "Trident";
			capabilities["layoutEngineVersion"] = regexWorker["${layoutVersion}"];
			capabilities["extra"] = regexWorker["${extra}"];
			capabilities["isColor"] = "true";
			capabilities["letters"] = regexWorker["${letters}"];
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["screenBitDepth"] = "8";
			capabilities["type"] = regexWorker["IE${major}"];
			capabilities["version"] = regexWorker["${version}"];
			browserCaps.AddBrowser("IE");
			this.IeProcessGateways(headers, browserCaps);
			this.IebetaProcess(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Ie6plusProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.IeProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie6plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie6plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x0011BE4C File Offset: 0x0011A04C
		private bool Ie6plusProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^[6-9]|\\d{2,}$"))
			{
				return false;
			}
			capabilities["jscriptversion"] = "5.6";
			capabilities["msdomversion"] = regexWorker["${majorversion}.${minorversion}"];
			capabilities["ExchangeOmaSupported"] = "true";
			capabilities["activexcontrols"] = "true";
			capabilities["backgroundsounds"] = "true";
			capabilities["javaapplets"] = "true";
			capabilities["supportsVCard"] = "true";
			capabilities["supportsAccessKeyAttribute"] = "true";
			capabilities["vbscript"] = "true";
			browserCaps.AddBrowser("IE6Plus");
			this.Ie6plusProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Ie6to9Process(headers, browserCaps) && !this.Ie10plusProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.Ie6plusProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie6to9ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie6to9ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x0011BF58 File Offset: 0x0011A158
		private bool Ie6to9Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^[6-9]$"))
			{
				return false;
			}
			browserCaps.AddBrowser("IE6to9");
			this.Ie6to9ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Ie7Process(headers, browserCaps) && !this.Ie8Process(headers, browserCaps) && !this.Ie9Process(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.Ie6to9ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie7ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie7ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051EC RID: 20972 RVA: 0x0011BFD8 File Offset: 0x0011A1D8
		private bool Ie7Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^7$"))
			{
				return false;
			}
			capabilities["jscriptversion"] = "5.7";
			browserCaps.AddBrowser("IE7");
			this.Ie7ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Ie7ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie8ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie8ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0011C048 File Offset: 0x0011A248
		private bool Ie8Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^8$"))
			{
				return false;
			}
			capabilities["jscriptversion"] = "6.0";
			browserCaps.AddBrowser("IE8");
			this.Ie8ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Ie8ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie9ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie9ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x0011C0B8 File Offset: 0x0011A2B8
		private bool Ie9Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^9$"))
			{
				return false;
			}
			capabilities["jscriptversion"] = "6.0";
			browserCaps.AddBrowser("IE9");
			this.Ie9ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Ie9ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie10plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Ie10plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0011C128 File Offset: 0x0011A328
		private bool Ie10plusProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "\\d{2,}"))
			{
				return false;
			}
			capabilities["jscriptversion"] = "6.0";
			browserCaps.AddBrowser("IE10Plus");
			this.Ie10plusProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Ie10plusProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IebetaProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IebetaProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x0011C198 File Offset: 0x0011A398
		private bool IebetaProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["letters"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^([bB]|ab)"))
			{
				return false;
			}
			capabilities["beta"] = "true";
			this.IebetaProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.IebetaProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void InternetexplorerProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void InternetexplorerProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x0011C1FC File Offset: 0x0011A3FC
		private bool InternetexplorerProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Trident/(?'layoutVersion'[7-9]|0*[1-9]\\d+)(\\.\\d+)?;(.*;)?\\s*rv:(?'version'(?'major'\\d+)(\\.(?'minor'\\d+)))"))
			{
				return false;
			}
			target = browserCaps[string.Empty];
			bool flag = regexWorker.ProcessRegex(target, "IEMobile");
			if (flag)
			{
				return false;
			}
			flag = regexWorker.ProcessRegex(target, "MSIE ");
			if (flag)
			{
				return false;
			}
			capabilities["browser"] = "InternetExplorer";
			capabilities["version"] = regexWorker["${version}"];
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["layoutEngine"] = "Trident";
			capabilities["layoutEngineVersion"] = regexWorker["${layoutVersion}"];
			capabilities["type"] = regexWorker["InternetExplorer${major}"];
			browserCaps.AddBrowser("InternetExplorer");
			this.InternetexplorerProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.InternetexplorerProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void BlackberryProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void BlackberryProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0011C314 File Offset: 0x0011A514
		private bool BlackberryProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "BlackBerry(?'deviceName'\\w+)/(?'version'(?'major'\\d+)(\\.(?'minor'\\d+)?)\\w*)"))
			{
				return false;
			}
			capabilities["layoutEngine"] = "BlackBerry";
			capabilities["browser"] = "BlackBerry";
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["type"] = regexWorker["BlackBerry${major}"];
			capabilities["mobileDeviceModel"] = regexWorker["${deviceName}"];
			capabilities["isMobileDevice"] = "true";
			capabilities["version"] = regexWorker["${version}"];
			capabilities["ecmascriptversion"] = "3.0";
			capabilities["javascript"] = "true";
			capabilities["javascriptversion"] = "1.3";
			capabilities["w3cdomversion"] = "1.0";
			capabilities["supportsAccesskeyAttribute"] = "true";
			capabilities["tagwriter"] = "System.Web.UI.HtmlTextWriter";
			capabilities["cookies"] = "true";
			capabilities["frames"] = "true";
			capabilities["javaapplets"] = "true";
			capabilities["supportsCallback"] = "true";
			capabilities["supportsDivNoWrap"] = "false";
			capabilities["supportsFileUpload"] = "true";
			capabilities["supportsMultilineTextBoxDisplay"] = "true";
			capabilities["supportsXmlHttp"] = "true";
			capabilities["tables"] = "true";
			capabilities["canInitiateVoiceCall"] = "true";
			browserCaps.AddBrowser("BlackBerry");
			this.BlackberryProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.BlackberryProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OperaProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OperaProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x0011C50C File Offset: 0x0011A70C
		private bool OperaProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Opera[ /](?'version'(?'major'\\d+)(\\.(?'minor'\\d+)?)(?'letters'\\w*))"))
			{
				return false;
			}
			regexWorker.ProcessRegex(browserCaps[string.Empty], "Presto/(?'layoutVersion'\\d+)");
			capabilities["browser"] = "Opera";
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["type"] = regexWorker["Opera${major}"];
			capabilities["version"] = regexWorker["${version}"];
			capabilities["layoutEngine"] = "Presto";
			capabilities["layoutEngineVersion"] = regexWorker["${layoutVersion}"];
			capabilities["ecmascriptversion"] = "3.0";
			capabilities["javascript"] = "true";
			capabilities["javascriptversion"] = "1.5";
			capabilities["letters"] = regexWorker["${letters}"];
			capabilities["w3cdomversion"] = "1.0";
			capabilities["tagwriter"] = "System.Web.UI.HtmlTextWriter";
			capabilities["cookies"] = "true";
			capabilities["frames"] = "true";
			capabilities["javaapplets"] = "true";
			capabilities["supportsAccesskeyAttribute"] = "true";
			capabilities["supportsCallback"] = "true";
			capabilities["supportsFileUpload"] = "true";
			capabilities["supportsMultilineTextBoxDisplay"] = "true";
			capabilities["supportsXmlHttp"] = "true";
			capabilities["tables"] = "true";
			capabilities["inputType"] = "keyboard";
			capabilities["isColor"] = "true";
			capabilities["isMobileDevice"] = "false";
			capabilities["maximumRenderedPageSize"] = "300000";
			capabilities["screenBitDepth"] = "8";
			capabilities["supportsBold"] = "true";
			capabilities["supportsCss"] = "true";
			capabilities["supportsDivNoWrap"] = "true";
			capabilities["supportsFontName"] = "true";
			capabilities["supportsFontSize"] = "true";
			capabilities["supportsImageSubmit"] = "true";
			capabilities["supportsItalic"] = "true";
			browserCaps.AddBrowser("Opera");
			this.OperaProcessGateways(headers, browserCaps);
			this.OperaminiProcess(headers, browserCaps);
			this.OperamobileProcess(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Opera8plusProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.OperaProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OperaminiProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OperaminiProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0011C7E0 File Offset: 0x0011A9E0
		private bool OperaminiProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Opera Mini"))
			{
				return false;
			}
			capabilities["isMobileDevice"] = "true";
			this.OperaminiProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.OperaminiProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OperamobileProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OperamobileProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x0011C840 File Offset: 0x0011AA40
		private bool OperamobileProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Opera Mobi"))
			{
				return false;
			}
			capabilities["isMobileDevice"] = "true";
			this.OperamobileProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.OperamobileProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Opera8plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Opera8plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x0011C8A0 File Offset: 0x0011AAA0
		private bool Opera8plusProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^[8-9]|\\d{2,}$"))
			{
				return false;
			}
			capabilities["supportsMaintainScrollPositionOnPostback"] = "true";
			browserCaps.AddBrowser("Opera8Plus");
			this.Opera8plusProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Opera8to9Process(headers, browserCaps) && !this.Opera10Process(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.Opera8plusProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Opera8to9ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Opera8to9ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0011C928 File Offset: 0x0011AB28
		private bool Opera8to9Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^[8-9]$"))
			{
				return false;
			}
			target = (string)capabilities["Version"];
			bool flag = regexWorker.ProcessRegex(target, "^9.80$");
			if (flag)
			{
				return false;
			}
			browserCaps.AddBrowser("Opera8to9");
			this.Opera8to9ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Opera8to9ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Opera10ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Opera10ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005210 RID: 21008 RVA: 0x0011C9AC File Offset: 0x0011ABAC
		private bool Opera10Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Opera/10\\.|Version/10\\."))
			{
				return false;
			}
			capabilities["version"] = "10.00";
			capabilities["majorversion"] = "10";
			capabilities["minorversion"] = "00";
			browserCaps.AddBrowser("Opera10");
			this.Opera10ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Opera10ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005211 RID: 21009 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void ChromeProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void ChromeProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x0011CA38 File Offset: 0x0011AC38
		private bool ChromeProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Chrome/(?'version'(?'major'\\d+)(\\.(?'minor'\\d+)?)\\w*)"))
			{
				return false;
			}
			capabilities["browser"] = "Chrome";
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["type"] = regexWorker["Chrome${major}"];
			capabilities["version"] = regexWorker["${version}"];
			capabilities["ecmascriptversion"] = "3.0";
			capabilities["javascript"] = "true";
			capabilities["javascriptversion"] = "1.7";
			capabilities["w3cdomversion"] = "1.0";
			capabilities["supportsAccesskeyAttribute"] = "true";
			capabilities["tagwriter"] = "System.Web.UI.HtmlTextWriter";
			capabilities["cookies"] = "true";
			capabilities["frames"] = "true";
			capabilities["javaapplets"] = "true";
			capabilities["supportsCallback"] = "true";
			capabilities["supportsDivNoWrap"] = "false";
			capabilities["supportsFileUpload"] = "true";
			capabilities["supportsMaintainScrollPositionOnPostback"] = "true";
			capabilities["supportsMultilineTextBoxDisplay"] = "true";
			capabilities["supportsXmlHttp"] = "true";
			capabilities["tables"] = "true";
			browserCaps.AddBrowser("Chrome");
			this.ChromeProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.ChromeProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void DefaultProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005215 RID: 21013 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void DefaultProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0011CBFC File Offset: 0x0011ADFC
		private bool DefaultProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			capabilities["activexcontrols"] = "false";
			capabilities["aol"] = "false";
			capabilities["backgroundsounds"] = "false";
			capabilities["beta"] = "false";
			capabilities["browser"] = "Unknown";
			capabilities["canCombineFormsInDeck"] = "true";
			capabilities["canInitiateVoiceCall"] = "false";
			capabilities["canRenderAfterInputOrSelectElement"] = "true";
			capabilities["canRenderEmptySelects"] = "true";
			capabilities["canRenderInputAndSelectElementsTogether"] = "true";
			capabilities["canRenderMixedSelects"] = "true";
			capabilities["canRenderOneventAndPrevElementsTogether"] = "true";
			capabilities["canRenderPostBackCards"] = "true";
			capabilities["canRenderSetvarZeroWithMultiSelectionList"] = "true";
			capabilities["canSendMail"] = "true";
			capabilities["cdf"] = "false";
			capabilities["cookies"] = "true";
			capabilities["crawler"] = "false";
			capabilities["defaultSubmitButtonLimit"] = "1";
			capabilities["ecmascriptversion"] = "0.0";
			capabilities["frames"] = "false";
			capabilities["gatewayMajorVersion"] = "0";
			capabilities["gatewayMinorVersion"] = "0";
			capabilities["gatewayVersion"] = "None";
			capabilities["hasBackButton"] = "true";
			capabilities["hidesRightAlignedMultiselectScrollbars"] = "false";
			capabilities["inputType"] = "telephoneKeypad";
			capabilities["isColor"] = "false";
			capabilities["isMobileDevice"] = "false";
			capabilities["javaapplets"] = "false";
			capabilities["javascript"] = "false";
			capabilities["jscriptversion"] = "0.0";
			capabilities["majorversion"] = "0";
			capabilities["maximumHrefLength"] = "10000";
			capabilities["maximumRenderedPageSize"] = "2000";
			capabilities["maximumSoftkeyLabelLength"] = "5";
			capabilities["minorversion"] = "0";
			capabilities["mobileDeviceManufacturer"] = "Unknown";
			capabilities["mobileDeviceModel"] = "Unknown";
			capabilities["msdomversion"] = "0.0";
			capabilities["numberOfSoftkeys"] = "0";
			capabilities["platform"] = "Unknown";
			capabilities["preferredImageMime"] = "image/gif";
			capabilities["preferredRenderingMime"] = "text/html";
			capabilities["preferredRenderingType"] = "html32";
			capabilities["rendersBreakBeforeWmlSelectAndInput"] = "false";
			capabilities["rendersBreaksAfterHtmlLists"] = "true";
			capabilities["rendersBreaksAfterWmlAnchor"] = "false";
			capabilities["rendersBreaksAfterWmlInput"] = "false";
			capabilities["rendersWmlDoAcceptsInline"] = "true";
			capabilities["rendersWmlSelectsAsMenuCards"] = "false";
			capabilities["requiredMetaTagNameValue"] = "";
			capabilities["requiresAbsolutePostbackUrl"] = "false";
			capabilities["requiresAdaptiveErrorReporting"] = "false";
			capabilities["requiresAttributeColonSubstitution"] = "false";
			capabilities["requiresContentTypeMetaTag"] = "false";
			capabilities["requiresControlStateInSession"] = "false";
			capabilities["requiresDBCSCharacter"] = "false";
			capabilities["requiresFullyQualifiedRedirectUrl"] = "false";
			capabilities["requiresLeadingPageBreak"] = "false";
			capabilities["requiresNoBreakInFormatting"] = "false";
			capabilities["requiresOutputOptimization"] = "false";
			capabilities["requiresPhoneNumbersAsPlainText"] = "false";
			capabilities["requiresPostRedirectionHandling"] = "false";
			capabilities["requiresSpecialViewStateEncoding"] = "false";
			capabilities["requiresUniqueFilePathSuffix"] = "false";
			capabilities["requiresUniqueHtmlCheckboxNames"] = "false";
			capabilities["requiresUniqueHtmlInputNames"] = "false";
			capabilities["requiresUrlEncodedPostfieldValues"] = "false";
			capabilities["requiresXhtmlCssSuppression"] = "false";
			capabilities["screenBitDepth"] = "1";
			capabilities["supportsAccesskeyAttribute"] = "false";
			capabilities["supportsBodyColor"] = "true";
			capabilities["supportsBold"] = "false";
			capabilities["supportsCallback"] = "false";
			capabilities["supportsCacheControlMetaTag"] = "true";
			capabilities["supportsCss"] = "false";
			capabilities["supportsDivAlign"] = "true";
			capabilities["supportsDivNoWrap"] = "false";
			capabilities["supportsEmptyStringInCookieValue"] = "true";
			capabilities["supportsFileUpload"] = "false";
			capabilities["supportsFontColor"] = "true";
			capabilities["supportsFontName"] = "false";
			capabilities["supportsFontSize"] = "false";
			capabilities["supportsImageSubmit"] = "false";
			capabilities["supportsIModeSymbols"] = "false";
			capabilities["supportsInputIStyle"] = "false";
			capabilities["supportsInputMode"] = "false";
			capabilities["supportsItalic"] = "false";
			capabilities["supportsJPhoneMultiMediaAttributes"] = "false";
			capabilities["supportsJPhoneSymbols"] = "false";
			capabilities["SupportsMaintainScrollPositionOnPostback"] = "false";
			capabilities["supportsMultilineTextBoxDisplay"] = "false";
			capabilities["supportsQueryStringInFormAction"] = "true";
			capabilities["supportsRedirectWithCookie"] = "true";
			capabilities["supportsSelectMultiple"] = "true";
			capabilities["supportsUncheck"] = "true";
			capabilities["supportsVCard"] = "false";
			capabilities["tables"] = "false";
			capabilities["tagwriter"] = "System.Web.UI.Html32TextWriter";
			capabilities["type"] = "Unknown";
			capabilities["vbscript"] = "false";
			capabilities["version"] = "0.0";
			capabilities["w3cdomversion"] = "0.0";
			capabilities["win16"] = "false";
			capabilities["win32"] = "false";
			browserCaps.AddBrowser("Default");
			this.DefaultProcessGateways(headers, browserCaps);
			this.CrawlerProcess(headers, browserCaps);
			this.PlatformProcess(headers, browserCaps);
			this.WinProcess(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.BlackberryProcess(headers, browserCaps) && !this.OperaProcess(headers, browserCaps) && !this.GenericdownlevelProcess(headers, browserCaps) && !this.MozillaProcess(headers, browserCaps) && !this.UcbrowserProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.DefaultProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void FirefoxProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void FirefoxProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005219 RID: 21017 RVA: 0x0011D320 File Offset: 0x0011B520
		private bool FirefoxProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Firefox\\/(?'version'(?'major'\\d+)(\\.(?'minor'\\d+)?)\\w*)"))
			{
				return false;
			}
			regexWorker.ProcessRegex(browserCaps[string.Empty], "Gecko/(?'layoutVersion'\\d+)");
			capabilities["browser"] = "Firefox";
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["version"] = regexWorker["${version}"];
			capabilities["type"] = regexWorker["Firefox${major}"];
			capabilities["layoutEngine"] = "Gecko";
			capabilities["layoutEngineVersion"] = regexWorker["${layoutVersion}"];
			capabilities["supportsAccesskeyAttribute"] = "true";
			capabilities["javaapplets"] = "true";
			capabilities["supportsDivNoWrap"] = "false";
			browserCaps.AddBrowser("Firefox");
			this.FirefoxProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Firefox3plusProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.FirefoxProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Firefox3plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Firefox3plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x0011D45C File Offset: 0x0011B65C
		private bool Firefox3plusProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "[3-9]|\\d{2,}"))
			{
				return false;
			}
			capabilities["javascriptversion"] = "1.8";
			browserCaps.AddBrowser("Firefox3Plus");
			this.Firefox3plusProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Firefox3Process(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.Firefox3plusProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Firefox3ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Firefox3ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x0011D4D8 File Offset: 0x0011B6D8
		private bool Firefox3Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^3$"))
			{
				return false;
			}
			browserCaps.AddBrowser("Firefox3");
			this.Firefox3ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Firefox35Process(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.Firefox3ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Firefox35ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Firefox35ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0011D544 File Offset: 0x0011B744
		private bool Firefox35Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["minorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^[5-9]"))
			{
				return false;
			}
			browserCaps.AddBrowser("Firefox35");
			this.Firefox35ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Firefox35ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void CrawlerProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void CrawlerProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0011D5A4 File Offset: 0x0011B7A4
		private bool CrawlerProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "crawler|Crawler|Googlebot|bingbot"))
			{
				return false;
			}
			capabilities["crawler"] = "true";
			this.CrawlerProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.CrawlerProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0011D604 File Offset: 0x0011B804
		private bool PlatformProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string value = browserCaps[string.Empty];
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			this.PlatformProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.PlatformwinntProcess(headers, browserCaps) && !this.Platformwin2000bProcess(headers, browserCaps) && !this.Platformwin95Process(headers, browserCaps) && !this.Platformwin98Process(headers, browserCaps) && !this.Platformwin16Process(headers, browserCaps) && !this.PlatformwinceProcess(headers, browserCaps) && !this.Platformmac68kProcess(headers, browserCaps) && !this.PlatformmacppcProcess(headers, browserCaps) && !this.PlatformunixProcess(headers, browserCaps) && !this.PlatformwebtvProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.PlatformProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005229 RID: 21033 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwinntProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600522A RID: 21034 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwinntProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600522B RID: 21035 RVA: 0x0011D6A8 File Offset: 0x0011B8A8
		private bool PlatformwinntProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Windows NT|WinNT|Windows XP"))
			{
				return false;
			}
			target = browserCaps[string.Empty];
			bool flag = regexWorker.ProcessRegex(target, "WinCE|Windows CE");
			if (flag)
			{
				return false;
			}
			capabilities["platform"] = "WinNT";
			this.PlatformwinntProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.PlatformwinxpProcess(headers, browserCaps) && !this.Platformwin2000aProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.PlatformwinntProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600522C RID: 21036 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwinxpProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600522D RID: 21037 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwinxpProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600522E RID: 21038 RVA: 0x0011D73C File Offset: 0x0011B93C
		private bool PlatformwinxpProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Windows (NT 5\\.1|XP)"))
			{
				return false;
			}
			capabilities["platform"] = "WinXP";
			this.PlatformwinxpProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.PlatformwinxpProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600522F RID: 21039 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin2000aProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005230 RID: 21040 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin2000aProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005231 RID: 21041 RVA: 0x0011D79C File Offset: 0x0011B99C
		private bool Platformwin2000aProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Windows NT 5\\.0"))
			{
				return false;
			}
			capabilities["platform"] = "Win2000";
			this.Platformwin2000aProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Platformwin2000aProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin2000bProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin2000bProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0011D7FC File Offset: 0x0011B9FC
		private bool Platformwin2000bProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Windows 2000"))
			{
				return false;
			}
			capabilities["platform"] = "Win2000";
			this.Platformwin2000bProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Platformwin2000bProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin95ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin95ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0011D85C File Offset: 0x0011BA5C
		private bool Platformwin95Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Win(dows )?95"))
			{
				return false;
			}
			capabilities["platform"] = "Win95";
			this.Platformwin95ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Platformwin95ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin98ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin98ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x0011D8BC File Offset: 0x0011BABC
		private bool Platformwin98Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Win(dows )?98"))
			{
				return false;
			}
			capabilities["platform"] = "Win98";
			this.Platformwin98ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Platformwin98ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin16ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformwin16ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x0011D91C File Offset: 0x0011BB1C
		private bool Platformwin16Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Win(dows 3\\.1|16)"))
			{
				return false;
			}
			capabilities["platform"] = "Win16";
			this.Platformwin16ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Platformwin16ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwinceProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwinceProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0011D97C File Offset: 0x0011BB7C
		private bool PlatformwinceProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Win(dows )?CE"))
			{
				return false;
			}
			capabilities["platform"] = "WinCE";
			this.PlatformwinceProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.PlatformwinceProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformmac68kProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005242 RID: 21058 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Platformmac68kProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005243 RID: 21059 RVA: 0x0011D9DC File Offset: 0x0011BBDC
		private bool Platformmac68kProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Mac(_68(000|K)|intosh.*68K)"))
			{
				return false;
			}
			capabilities["platform"] = "Mac68K";
			this.Platformmac68kProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Platformmac68kProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005244 RID: 21060 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformmacppcProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005245 RID: 21061 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformmacppcProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005246 RID: 21062 RVA: 0x0011DA3C File Offset: 0x0011BC3C
		private bool PlatformmacppcProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Mac(_PowerPC|intosh.*PPC|_PPC)|PPC Mac"))
			{
				return false;
			}
			capabilities["platform"] = "MacPPC";
			this.PlatformmacppcProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.PlatformmacppcProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005247 RID: 21063 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformunixProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005248 RID: 21064 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformunixProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005249 RID: 21065 RVA: 0x0011DA9C File Offset: 0x0011BC9C
		private bool PlatformunixProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "X11"))
			{
				return false;
			}
			capabilities["platform"] = "UNIX";
			this.PlatformunixProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.PlatformunixProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600524A RID: 21066 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwebtvProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600524B RID: 21067 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PlatformwebtvProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600524C RID: 21068 RVA: 0x0011DAFC File Offset: 0x0011BCFC
		private bool PlatformwebtvProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "WebTV"))
			{
				return false;
			}
			capabilities["platform"] = "WebTV";
			this.PlatformwebtvProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.PlatformwebtvProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600524D RID: 21069 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WinProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600524E RID: 21070 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WinProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600524F RID: 21071 RVA: 0x0011DB5C File Offset: 0x0011BD5C
		private bool WinProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string value = browserCaps[string.Empty];
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			this.WinProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Win32Process(headers, browserCaps) && !this.Win16Process(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.WinProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005250 RID: 21072 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Win32ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005251 RID: 21073 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Win32ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005252 RID: 21074 RVA: 0x0011DBB0 File Offset: 0x0011BDB0
		private bool Win32Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Win(dows )?(9[58]|NT|32)"))
			{
				return false;
			}
			capabilities["win32"] = "true";
			this.Win32ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Win32ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005253 RID: 21075 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Win16ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005254 RID: 21076 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Win16ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005255 RID: 21077 RVA: 0x0011DC10 File Offset: 0x0011BE10
		private bool Win16Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "16bit|Win(dows 3\\.1|16)"))
			{
				return false;
			}
			capabilities["win16"] = "true";
			this.Win16ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Win16ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005256 RID: 21078 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void GenericdownlevelProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005257 RID: 21079 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void GenericdownlevelProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005258 RID: 21080 RVA: 0x0011DC70 File Offset: 0x0011BE70
		private bool GenericdownlevelProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^Generic Downlevel$"))
			{
				return false;
			}
			capabilities["cookies"] = "false";
			capabilities["ecmascriptversion"] = "1.0";
			capabilities["tables"] = "true";
			capabilities["type"] = "Downlevel";
			browserCaps.Adapters["System.Web.UI.WebControls.Menu, System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"] = "System.Web.UI.WebControls.Adapters.MenuAdapter";
			browserCaps.AddBrowser("GenericDownlevel");
			this.GenericdownlevelProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.GenericdownlevelProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void MozillaProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void MozillaProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600525B RID: 21083 RVA: 0x0011DD20 File Offset: 0x0011BF20
		private bool MozillaProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Mozilla"))
			{
				return false;
			}
			capabilities["browser"] = "Mozilla";
			capabilities["cookies"] = "true";
			capabilities["ecmascriptversion"] = "3.0";
			capabilities["frames"] = "true";
			capabilities["inputType"] = "keyboard";
			capabilities["isColor"] = "true";
			capabilities["isMobileDevice"] = "false";
			capabilities["javascript"] = "true";
			capabilities["javascriptversion"] = "1.5";
			capabilities["maximumRenderedPageSize"] = "300000";
			capabilities["screenBitDepth"] = "8";
			capabilities["supportsBold"] = "true";
			capabilities["supportsCallback"] = "true";
			capabilities["supportsCss"] = "true";
			capabilities["supportsDivNoWrap"] = "true";
			capabilities["supportsFileUpload"] = "true";
			capabilities["supportsFontName"] = "true";
			capabilities["supportsFontSize"] = "true";
			capabilities["supportsImageSubmit"] = "true";
			capabilities["supportsItalic"] = "true";
			capabilities["supportsMaintainScrollPositionOnPostback"] = "true";
			capabilities["supportsMultilineTextBoxDisplay"] = "true";
			capabilities["supportsXmlHttp"] = "true";
			capabilities["tables"] = "true";
			capabilities["tagwriter"] = "System.Web.UI.HtmlTextWriter";
			capabilities["type"] = "Mozilla";
			capabilities["w3cdomversion"] = "1.0";
			browserCaps.AddBrowser("Mozilla");
			this.MozillaProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.IeProcess(headers, browserCaps) && !this.InternetexplorerProcess(headers, browserCaps) && !this.FirefoxProcess(headers, browserCaps) && !this.WebkitProcess(headers, browserCaps) && !this.IemobileProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.MozillaProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600525C RID: 21084 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WebkitProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600525D RID: 21085 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WebkitProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600525E RID: 21086 RVA: 0x0011DF60 File Offset: 0x0011C160
		private bool WebkitProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "AppleWebKit"))
			{
				return false;
			}
			regexWorker.ProcessRegex(browserCaps[string.Empty], "AppleWebKit/(?'layoutVersion'\\d+)");
			capabilities["layoutEngine"] = "WebKit";
			capabilities["layoutEngineVersion"] = regexWorker["${layoutVersion}"];
			browserCaps.AddBrowser("WebKit");
			this.WebkitProcessGateways(headers, browserCaps);
			this.WebkitmobileProcess(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.ChromeProcess(headers, browserCaps) && !this.SafariProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.WebkitProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600525F RID: 21087 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WebkitmobileProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005260 RID: 21088 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WebkitmobileProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0011E018 File Offset: 0x0011C218
		private bool WebkitmobileProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Mobile( Safari)?/(?'iOSVersion'[^ ]+)"))
			{
				return false;
			}
			regexWorker.ProcessRegex(browserCaps[string.Empty], "Mozilla/5.0 \\((?'deviceName'[^;]+)");
			capabilities["mobileDeviceModel"] = regexWorker["${deviceName}"];
			capabilities["isMobileDevice"] = "true";
			capabilities["ecmascriptversion"] = "3.0";
			capabilities["javascript"] = "true";
			capabilities["javascriptversion"] = "1.6";
			capabilities["w3cdomversion"] = "1.0";
			capabilities["supportsAccesskeyAttribute"] = "true";
			capabilities["tagwriter"] = "System.Web.UI.HtmlTextWriter";
			capabilities["cookies"] = "true";
			capabilities["frames"] = "true";
			capabilities["supportsCallback"] = "true";
			capabilities["supportsDivNoWrap"] = "false";
			capabilities["supportsFileUpload"] = "true";
			capabilities["supportsMaintainScrollPositionOnPostback"] = "true";
			capabilities["supportsMultilineTextBoxDisplay"] = "true";
			capabilities["supportsXmlHttp"] = "true";
			capabilities["tables"] = "true";
			this.WebkitmobileProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.WebkitmobileProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IemobileProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IemobileProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x0011E194 File Offset: 0x0011C394
		private bool IemobileProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "IEMobile.(?'version'(?'major'\\d+)(\\.(?'minor'\\d+)?)\\w*)"))
			{
				return false;
			}
			regexWorker.ProcessRegex(browserCaps[string.Empty], "MSIE (?'msieMajorVersion'\\d+)");
			capabilities["layoutEngine"] = "Trident";
			capabilities["browser"] = "IEMobile";
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["type"] = regexWorker["IEMobile${msieMajorVersion}"];
			capabilities["isMobileDevice"] = "true";
			capabilities["version"] = regexWorker["${version}"];
			capabilities["jscriptversion"] = "5.6";
			capabilities["msdomversion"] = regexWorker["${majorversion}.${minorversion}"];
			capabilities["supportsAccesskeyAttribute"] = "true";
			capabilities["javaapplets"] = "true";
			capabilities["supportsDivNoWrap"] = "false";
			capabilities["vbscript"] = "true";
			capabilities["inputType"] = "virtualKeyboard";
			capabilities["numberOfSoftkeys"] = "2";
			browserCaps.AddBrowser("IEMobile");
			this.IemobileProcessGateways(headers, browserCaps);
			this.MonoProcess(headers, browserCaps);
			this.PixelsProcess(headers, browserCaps);
			this.OsProcess(headers, browserCaps);
			this.CpuProcess(headers, browserCaps);
			this.VoiceProcess(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.WindowsphoneProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.IemobileProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WindowsphoneProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void WindowsphoneProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x0011E350 File Offset: 0x0011C550
		private bool WindowsphoneProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Windows Phone OS"))
			{
				return false;
			}
			capabilities["javaapplets"] = "false";
			capabilities["jscriptversion"] = "5.7";
			browserCaps.AddBrowser("WindowsPhone");
			this.WindowsphoneProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.WindowsphoneProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void MonoProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void MonoProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x0011E3CC File Offset: 0x0011C5CC
		private bool MonoProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string text = headers["UA-COLOR"];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			text = headers["UA-COLOR"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(text, "mono(?'colorDepth'\\d+)"))
			{
				return false;
			}
			browserCaps.DisableOptimizedCacheKey();
			capabilities["isColor"] = "false";
			capabilities["screenBitDepth"] = regexWorker["${colorDepth}"];
			this.MonoProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.MonoProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PixelsProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void PixelsProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x0011E460 File Offset: 0x0011C660
		private bool PixelsProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string text = headers["UA-PIXELS"];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			text = headers["UA-PIXELS"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(text, "(?'screenWidth'\\d+)x(?'screenHeight'\\d+)"))
			{
				return false;
			}
			browserCaps.DisableOptimizedCacheKey();
			capabilities["screenPixelsHeight"] = regexWorker["${screenHeight}"];
			capabilities["screenPixelsWidth"] = regexWorker["${screenWidth}"];
			this.PixelsProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.PixelsProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OSProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OSProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005270 RID: 21104 RVA: 0x0011E4F8 File Offset: 0x0011C6F8
		private bool OsProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = headers["UA-OS"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "(?'os'.+)"))
			{
				return false;
			}
			browserCaps.DisableOptimizedCacheKey();
			capabilities["platform"] = regexWorker["${os}"];
			this.OSProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.OSProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005271 RID: 21105 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void CpuProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void CpuProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0011E564 File Offset: 0x0011C764
		private bool CpuProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = headers["UA-CPU"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "(?'cpu'.+)"))
			{
				return false;
			}
			browserCaps.DisableOptimizedCacheKey();
			capabilities["cpu"] = regexWorker["${cpu}"];
			this.CpuProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.CpuProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void VoiceProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void VoiceProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0011E5D0 File Offset: 0x0011C7D0
		private bool VoiceProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string text = headers["UA-VOICE"];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			text = headers["UA-VOICE"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(text, "(?i:TRUE)"))
			{
				return false;
			}
			browserCaps.DisableOptimizedCacheKey();
			capabilities["canInitiateVoiceCall"] = "true";
			this.VoiceProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.VoiceProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IphoneProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IphoneProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0011E64C File Offset: 0x0011C84C
		private bool IphoneProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "iPhone"))
			{
				return false;
			}
			capabilities["isMobileDevice"] = "true";
			capabilities["mobileDeviceManufacturer"] = "Apple";
			capabilities["mobileDeviceModel"] = "IPhone";
			capabilities["canInitiateVoiceCall"] = "true";
			this.IphoneProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.IphoneProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IpodProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IpodProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0011E6DC File Offset: 0x0011C8DC
		private bool IpodProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "iPod"))
			{
				return false;
			}
			capabilities["isMobileDevice"] = "true";
			capabilities["mobileDeviceManufacturer"] = "Apple";
			capabilities["mobileDeviceModel"] = "IPod";
			this.IpodProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.IpodProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IpadProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void IpadProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0011E75C File Offset: 0x0011C95C
		private bool IpadProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "iPad"))
			{
				return false;
			}
			capabilities["isMobileDevice"] = "true";
			capabilities["mobileDeviceManufacturer"] = "Apple";
			capabilities["mobileDeviceModel"] = "IPad";
			this.IpadProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.IpadProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void SafariProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void SafariProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x0011E7DC File Offset: 0x0011C9DC
		private bool SafariProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Safari"))
			{
				return false;
			}
			target = browserCaps[string.Empty];
			bool flag = regexWorker.ProcessRegex(target, "Chrome");
			if (flag)
			{
				return false;
			}
			target = browserCaps[string.Empty];
			flag = regexWorker.ProcessRegex(target, "Android");
			if (flag)
			{
				return false;
			}
			capabilities["browser"] = "Safari";
			capabilities["type"] = "Safari";
			browserCaps.AddBrowser("Safari");
			this.SafariProcessGateways(headers, browserCaps);
			this.IphoneProcess(headers, browserCaps);
			this.IpodProcess(headers, browserCaps);
			this.IpadProcess(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Safari3plusProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.SafariProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Safari3plusProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Safari3plusProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0011E8BC File Offset: 0x0011CABC
		private bool Safari3plusProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "Version/(?'version'(?'major'[3-9]|\\d{2,})(\\.(?'minor'\\d+)?)\\w*)"))
			{
				return false;
			}
			capabilities["version"] = regexWorker["${version}"];
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["type"] = regexWorker["Safari${major}"];
			capabilities["ecmascriptversion"] = "3.0";
			capabilities["javascript"] = "true";
			capabilities["javascriptversion"] = "1.6";
			capabilities["w3cdomversion"] = "1.0";
			capabilities["tagwriter"] = "System.Web.UI.HtmlTextWriter";
			capabilities["cookies"] = "true";
			capabilities["frames"] = "true";
			capabilities["javaapplets"] = "true";
			capabilities["supportsAccesskeyAttribute"] = "true";
			capabilities["supportsCallback"] = "true";
			capabilities["supportsDivNoWrap"] = "false";
			capabilities["supportsFileUpload"] = "true";
			capabilities["supportsMaintainScrollPositionOnPostback"] = "true";
			capabilities["supportsMultilineTextBoxDisplay"] = "true";
			capabilities["supportsXmlHttp"] = "true";
			capabilities["tables"] = "true";
			browserCaps.AddBrowser("Safari3Plus");
			this.Safari3plusProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Safari3to4Process(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.Safari3plusProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Safari3to4ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Safari3to4ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x0011EA7C File Offset: 0x0011CC7C
		private bool Safari3to4Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^[3-4]$"))
			{
				return false;
			}
			browserCaps.AddBrowser("Safari3to4");
			this.Safari3to4ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = true;
			if (!this.Safari4Process(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.Safari3to4ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Safari4ProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void Safari4ProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0011EAE8 File Offset: 0x0011CCE8
		private bool Safari4Process(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = (string)capabilities["majorversion"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "^4$"))
			{
				return false;
			}
			capabilities["javascriptversion"] = "1.7";
			browserCaps.AddBrowser("Safari4");
			this.Safari4ProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.Safari4ProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void UcbrowserProcessGateways(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void UcbrowserProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0011EB58 File Offset: 0x0011CD58
		private bool UcbrowserProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = browserCaps[string.Empty];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "(UC Browser |UCWEB)(?'version'(?'major'\\d+)(\\.(?'minor'[\\d\\.]+)?)\\w*)"))
			{
				return false;
			}
			capabilities["browser"] = "UCBrowser";
			capabilities["majorversion"] = regexWorker["${major}"];
			capabilities["minorversion"] = regexWorker["${minor}"];
			capabilities["isMobileDevice"] = "true";
			capabilities["version"] = regexWorker["${version}"];
			capabilities["ecmascriptversion"] = "3.0";
			capabilities["javascript"] = "true";
			capabilities["javascriptversion"] = "1.5";
			capabilities["tagwriter"] = "System.Web.UI.HtmlTextWriter";
			capabilities["cookies"] = "true";
			capabilities["frames"] = "true";
			capabilities["supportsCallback"] = "true";
			capabilities["supportsFileUpload"] = "true";
			capabilities["supportsMultilineTextBoxDisplay"] = "true";
			capabilities["supportsXmlHttp"] = "true";
			capabilities["tables"] = "true";
			browserCaps.AddBrowser("UCBrowser");
			this.UcbrowserProcessGateways(headers, browserCaps);
			bool ignoreApplicationBrowsers = false;
			this.UcbrowserProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void DefaultDefaultProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x0011ECC4 File Offset: 0x0011CEC4
		private bool DefaultDefaultProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			capabilities["ecmascriptversion"] = "0.0";
			capabilities["javascript"] = "false";
			capabilities["jscriptversion"] = "0.0";
			bool ignoreApplicationBrowsers = true;
			if (!this.DefaultWmlProcess(headers, browserCaps) && !this.DefaultXhtmlmpProcess(headers, browserCaps))
			{
				ignoreApplicationBrowsers = false;
			}
			this.DefaultDefaultProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void DefaultWmlProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0011ED2C File Offset: 0x0011CF2C
		private bool DefaultWmlProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = headers["Accept"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "text/vnd\\.wap\\.wml|text/hdml"))
			{
				return false;
			}
			target = headers["Accept"];
			bool flag = regexWorker.ProcessRegex(target, "application/xhtml\\+xml; profile|application/vnd\\.wap\\.xhtml\\+xml");
			if (flag)
			{
				return false;
			}
			browserCaps.DisableOptimizedCacheKey();
			capabilities["preferredRenderingMime"] = "text/vnd.wap.wml";
			capabilities["preferredRenderingType"] = "wml11";
			bool ignoreApplicationBrowsers = false;
			this.DefaultWmlProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void DefaultXhtmlmpProcessBrowsers(bool ignoreApplicationBrowsers, NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x0011EDB8 File Offset: 0x0011CFB8
		private bool DefaultXhtmlmpProcess(NameValueCollection headers, HttpBrowserCapabilities browserCaps)
		{
			IDictionary capabilities = browserCaps.Capabilities;
			string target = headers["Accept"];
			RegexWorker regexWorker = new RegexWorker(browserCaps);
			if (!regexWorker.ProcessRegex(target, "application/xhtml\\+xml; profile|application/vnd\\.wap\\.xhtml\\+xml"))
			{
				return false;
			}
			target = headers["Accept"];
			bool flag = regexWorker.ProcessRegex(target, "text/hdml");
			if (flag)
			{
				return false;
			}
			target = headers["Accept"];
			flag = regexWorker.ProcessRegex(target, "text/vnd\\.wap\\.wml");
			if (flag)
			{
				return false;
			}
			browserCaps.DisableOptimizedCacheKey();
			capabilities["preferredRenderingMime"] = "text/html";
			capabilities["preferredRenderingType"] = "xhtml-mp";
			browserCaps.HtmlTextWriter = "System.Web.UI.XhtmlTextWriter";
			bool ignoreApplicationBrowsers = false;
			this.DefaultXhtmlmpProcessBrowsers(ignoreApplicationBrowsers, headers, browserCaps);
			return true;
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x0011EE6C File Offset: 0x0011D06C
		protected override void PopulateMatchedHeaders(IDictionary dictionary)
		{
			base.PopulateMatchedHeaders(dictionary);
			dictionary[""] = null;
			dictionary["UA-COLOR"] = null;
			dictionary["UA-PIXELS"] = null;
			dictionary["UA-OS"] = null;
			dictionary["UA-CPU"] = null;
			dictionary["UA-VOICE"] = null;
			dictionary["Accept"] = null;
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x0011EED4 File Offset: 0x0011D0D4
		protected override void PopulateBrowserElements(IDictionary dictionary)
		{
			base.PopulateBrowserElements(dictionary);
			dictionary["Default"] = new Triplet(null, string.Empty, 0);
			dictionary["BlackBerry"] = new Triplet("Default", string.Empty, 1);
			dictionary["Opera"] = new Triplet("Default", string.Empty, 1);
			dictionary["Opera8Plus"] = new Triplet("Opera", string.Empty, 2);
			dictionary["Opera8to9"] = new Triplet("Opera8plus", string.Empty, 3);
			dictionary["Opera10"] = new Triplet("Opera8plus", string.Empty, 3);
			dictionary["GenericDownlevel"] = new Triplet("Default", string.Empty, 1);
			dictionary["Mozilla"] = new Triplet("Default", string.Empty, 1);
			dictionary["IE"] = new Triplet("Mozilla", string.Empty, 2);
			dictionary["IE6Plus"] = new Triplet("Ie", string.Empty, 3);
			dictionary["IE6to9"] = new Triplet("Ie6plus", string.Empty, 4);
			dictionary["IE7"] = new Triplet("Ie6to9", string.Empty, 5);
			dictionary["IE8"] = new Triplet("Ie6to9", string.Empty, 5);
			dictionary["IE9"] = new Triplet("Ie6to9", string.Empty, 5);
			dictionary["IE10Plus"] = new Triplet("Ie6plus", string.Empty, 4);
			dictionary["InternetExplorer"] = new Triplet("Mozilla", string.Empty, 2);
			dictionary["Firefox"] = new Triplet("Mozilla", string.Empty, 2);
			dictionary["Firefox3Plus"] = new Triplet("Firefox", string.Empty, 3);
			dictionary["Firefox3"] = new Triplet("Firefox3plus", string.Empty, 4);
			dictionary["Firefox35"] = new Triplet("Firefox3", string.Empty, 5);
			dictionary["WebKit"] = new Triplet("Mozilla", string.Empty, 2);
			dictionary["Chrome"] = new Triplet("Webkit", string.Empty, 3);
			dictionary["Safari"] = new Triplet("Webkit", string.Empty, 3);
			dictionary["Safari3Plus"] = new Triplet("Safari", string.Empty, 4);
			dictionary["Safari3to4"] = new Triplet("Safari3plus", string.Empty, 5);
			dictionary["Safari4"] = new Triplet("Safari3to4", string.Empty, 6);
			dictionary["IEMobile"] = new Triplet("Mozilla", string.Empty, 2);
			dictionary["WindowsPhone"] = new Triplet("Iemobile", string.Empty, 3);
			dictionary["UCBrowser"] = new Triplet("Default", string.Empty, 1);
		}
	}
}
