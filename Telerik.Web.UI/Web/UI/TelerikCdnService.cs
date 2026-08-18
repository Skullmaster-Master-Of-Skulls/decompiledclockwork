using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200181C RID: 6172
	internal class TelerikCdnService : IScriptReferenceResolver, IStyleSheetReferenceResolver
	{
		// Token: 0x170048B1 RID: 18609
		// (get) Token: 0x0600F036 RID: 61494 RVA: 0x0036A3ED File Offset: 0x003685ED
		// (set) Token: 0x0600F037 RID: 61495 RVA: 0x0036A3F5 File Offset: 0x003685F5
		protected ICdnSettings Settings { get; set; }

		// Token: 0x170048B2 RID: 18610
		// (get) Token: 0x0600F038 RID: 61496 RVA: 0x0036A3FE File Offset: 0x003685FE
		// (set) Token: 0x0600F039 RID: 61497 RVA: 0x0036A406 File Offset: 0x00368606
		protected IHttpRequestInfo Request { get; set; }

		// Token: 0x0600F03A RID: 61498 RVA: 0x0036A40F File Offset: 0x0036860F
		public TelerikCdnService(ICdnSettings settings, IHttpRequestInfo request)
		{
			this.Settings = settings;
			this.Request = request;
		}

		// Token: 0x0600F03B RID: 61499 RVA: 0x0036A428 File Offset: 0x00368628
		public void ResolveScriptReference(ScriptReference script)
		{
			if (!TelerikCdnService.IsTelerikAssembly(script.Assembly))
			{
				return;
			}
			if (!string.IsNullOrEmpty(script.Path))
			{
				return;
			}
			string resourceLocation = TelerikCdnService.ResolveManifestName(script.Name);
			script.Path = this.FormatLocation(resourceLocation);
		}

		// Token: 0x0600F03C RID: 61500 RVA: 0x0036A46A File Offset: 0x0036866A
		public Uri ResoveScriptUri(string resourceUri)
		{
			return new Uri(this.FormatLocation(resourceUri));
		}

		// Token: 0x0600F03D RID: 61501 RVA: 0x0036A478 File Offset: 0x00368678
		public Uri ResoveSkinUri(string resourceUri)
		{
			return new Uri(this.FormatLocation(resourceUri));
		}

		// Token: 0x0600F03E RID: 61502 RVA: 0x0036A488 File Offset: 0x00368688
		public void ResolveStyleSheetReference(StyleSheetReference styleSheet)
		{
			if (!TelerikCdnService.IsTelerikAssembly(styleSheet.Assembly))
			{
				return;
			}
			if (!string.IsNullOrEmpty(styleSheet.Path))
			{
				return;
			}
			string resourceLocation = styleSheet.IsCommonCss ? TelerikCdnService.ResolveCommonCssManifestName(styleSheet.Name) : TelerikCdnService.ResolveSkinManifestName(styleSheet.Name);
			styleSheet.Path = this.FormatLocation(resourceLocation);
		}

		// Token: 0x0600F03F RID: 61503 RVA: 0x0036A4E0 File Offset: 0x003686E0
		private string FormatLocation(string resourceLocation)
		{
			string arg = this.Request.IsSecure ? this.Settings.BaseSecureUrl : this.Settings.BaseUrl;
			bool flag = false;
			if (this.GetOutputCompression != null)
			{
				OutputCompression outputCompression = this.GetOutputCompression();
				if (outputCompression == OutputCompression.Forced || (outputCompression == OutputCompression.AutoDetect && this.Request.SupportsGzip))
				{
					flag = true;
				}
			}
			else
			{
				flag = this.Request.SupportsGzip;
			}
			string arg2 = flag ? this.Settings.BaseCompressedPath : this.Settings.BasePath;
			return string.Format("{0}/{1}/{2}", arg, arg2, resourceLocation);
		}

		// Token: 0x0600F040 RID: 61504 RVA: 0x0036A578 File Offset: 0x00368778
		private static bool IsTelerikAssembly(string fullName)
		{
			string a = fullName.Split(new char[]
			{
				','
			})[0].Trim();
			return a == "Telerik.Web.UI" || a == "Telerik.Web.UI.Skins";
		}

		// Token: 0x0600F041 RID: 61505 RVA: 0x0036A5BC File Offset: 0x003687BC
		private static string ResolveManifestName(string manifestName)
		{
			string input = manifestName.Substring("Telerik.Web.UI".Length + 1);
			return TelerikCdnService.ManifestNameToPathRegex.Replace(input, "/");
		}

		// Token: 0x0600F042 RID: 61506 RVA: 0x0036A5EC File Offset: 0x003687EC
		private static string ResolveCommonCssManifestName(string manifestName)
		{
			return TelerikCdnService.ManifestNameToPathRegex.Replace(TelerikCdnService.GetStyleSheetRelativePath(manifestName), "/");
		}

		// Token: 0x0600F043 RID: 61507 RVA: 0x0036A603 File Offset: 0x00368803
		private static string ResolveSkinManifestName(string manifestName)
		{
			return TelerikCdnService.SkinManifestNameToPathRegex.Replace(TelerikCdnService.GetStyleSheetRelativePath(manifestName), "/");
		}

		// Token: 0x0600F044 RID: 61508 RVA: 0x0036A61A File Offset: 0x0036881A
		private static string GetStyleSheetRelativePath(string manifestName)
		{
			return manifestName.Substring("Telerik.Web.UI.Skins".Length + 1);
		}

		// Token: 0x170048B3 RID: 18611
		// (get) Token: 0x0600F045 RID: 61509 RVA: 0x0036A62E File Offset: 0x0036882E
		// (set) Token: 0x0600F046 RID: 61510 RVA: 0x0036A636 File Offset: 0x00368836
		public TelerikCdnService.GetOutputCompressionDelegate GetOutputCompression { get; set; }

		// Token: 0x0400453E RID: 17726
		private const string TelerikAssemblyName = "Telerik.Web.UI";

		// Token: 0x0400453F RID: 17727
		private const string DefaultSkinsAssemblyName = "Telerik.Web.UI.Skins";

		// Token: 0x04004540 RID: 17728
		private const string SkinsBasePath = "Telerik.Web.UI.Skins";

		// Token: 0x04004541 RID: 17729
		private static readonly Regex ManifestNameToPathRegex = new Regex("(\\.)(?=(.*\\.))");

		// Token: 0x04004542 RID: 17730
		private static readonly Regex SkinManifestNameToPathRegex = new Regex("(\\.)(?=(.*\\..*\\.))");

		// Token: 0x0200181D RID: 6173
		// (Invoke) Token: 0x0600F049 RID: 61513
		public delegate OutputCompression GetOutputCompressionDelegate();
	}
}
