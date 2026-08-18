using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000D9 RID: 217
	public static class MimeMapping
	{
		// Token: 0x06000E14 RID: 3604 RVA: 0x00027EB2 File Offset: 0x000260B2
		public static string GetMimeMapping(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			return MimeMapping._mappingDictionary.GetMimeMapping(fileName);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00027ECD File Offset: 0x000260CD
		internal static void SetIntegratedApplicationContext(IntPtr appContext)
		{
			MimeMapping._mappingDictionary = new MimeMapping.MimeMappingDictionaryIntegrated(appContext);
		}

		// Token: 0x04000532 RID: 1330
		private static MimeMapping.MimeMappingDictionaryBase _mappingDictionary = new MimeMapping.MimeMappingDictionaryClassic();

		// Token: 0x020008E6 RID: 2278
		private abstract class MimeMappingDictionaryBase
		{
			// Token: 0x06006851 RID: 26705 RVA: 0x0017267D File Offset: 0x0017087D
			protected void AddMapping(string fileExtension, string mimeType)
			{
				this._mappings.Add(fileExtension, mimeType);
			}

			// Token: 0x06006852 RID: 26706 RVA: 0x0017268C File Offset: 0x0017088C
			private void AddWildcardIfNotPresent()
			{
				if (!this._mappings.ContainsKey(".*"))
				{
					this.AddMapping(".*", "application/octet-stream");
				}
			}

			// Token: 0x06006853 RID: 26707 RVA: 0x001726B0 File Offset: 0x001708B0
			private void EnsureMapping()
			{
				if (!this._isInitialized)
				{
					lock (this)
					{
						if (!this._isInitialized)
						{
							this.PopulateMappings();
							this.AddWildcardIfNotPresent();
							this._isInitialized = true;
						}
					}
				}
			}

			// Token: 0x06006854 RID: 26708
			protected abstract void PopulateMappings();

			// Token: 0x06006855 RID: 26709 RVA: 0x00172708 File Offset: 0x00170908
			private static string GetFileName(string path)
			{
				int num = path.LastIndexOfAny(MimeMapping.MimeMappingDictionaryBase._pathSeparatorChars);
				if (num < 0)
				{
					return path;
				}
				return path.Substring(num);
			}

			// Token: 0x06006856 RID: 26710 RVA: 0x00172730 File Offset: 0x00170930
			public string GetMimeMapping(string fileName)
			{
				this.EnsureMapping();
				fileName = MimeMapping.MimeMappingDictionaryBase.GetFileName(fileName);
				for (int i = 0; i < fileName.Length; i++)
				{
					string result;
					if (fileName[i] == '.' && this._mappings.TryGetValue(fileName.Substring(i), out result))
					{
						return result;
					}
				}
				return this._mappings[".*"];
			}

			// Token: 0x04003653 RID: 13907
			private readonly Dictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			// Token: 0x04003654 RID: 13908
			private static readonly char[] _pathSeparatorChars = new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar,
				Path.VolumeSeparatorChar
			};

			// Token: 0x04003655 RID: 13909
			private bool _isInitialized;
		}

		// Token: 0x020008E7 RID: 2279
		private sealed class MimeMappingDictionaryClassic : MimeMapping.MimeMappingDictionaryBase
		{
			// Token: 0x06006859 RID: 26713 RVA: 0x001727CC File Offset: 0x001709CC
			protected override void PopulateMappings()
			{
				base.AddMapping(".323", "text/h323");
				base.AddMapping(".aaf", "application/octet-stream");
				base.AddMapping(".aca", "application/octet-stream");
				base.AddMapping(".accdb", "application/msaccess");
				base.AddMapping(".accde", "application/msaccess");
				base.AddMapping(".accdt", "application/msaccess");
				base.AddMapping(".acx", "application/internet-property-stream");
				base.AddMapping(".afm", "application/octet-stream");
				base.AddMapping(".ai", "application/postscript");
				base.AddMapping(".aif", "audio/x-aiff");
				base.AddMapping(".aifc", "audio/aiff");
				base.AddMapping(".aiff", "audio/aiff");
				base.AddMapping(".application", "application/x-ms-application");
				base.AddMapping(".art", "image/x-jg");
				base.AddMapping(".asd", "application/octet-stream");
				base.AddMapping(".asf", "video/x-ms-asf");
				base.AddMapping(".asi", "application/octet-stream");
				base.AddMapping(".asm", "text/plain");
				base.AddMapping(".asr", "video/x-ms-asf");
				base.AddMapping(".asx", "video/x-ms-asf");
				base.AddMapping(".atom", "application/atom+xml");
				base.AddMapping(".au", "audio/basic");
				base.AddMapping(".avi", "video/x-msvideo");
				base.AddMapping(".axs", "application/olescript");
				base.AddMapping(".bas", "text/plain");
				base.AddMapping(".bcpio", "application/x-bcpio");
				base.AddMapping(".bin", "application/octet-stream");
				base.AddMapping(".bmp", "image/bmp");
				base.AddMapping(".c", "text/plain");
				base.AddMapping(".cab", "application/octet-stream");
				base.AddMapping(".calx", "application/vnd.ms-office.calx");
				base.AddMapping(".cat", "application/vnd.ms-pki.seccat");
				base.AddMapping(".cdf", "application/x-cdf");
				base.AddMapping(".chm", "application/octet-stream");
				base.AddMapping(".class", "application/x-java-applet");
				base.AddMapping(".clp", "application/x-msclip");
				base.AddMapping(".cmx", "image/x-cmx");
				base.AddMapping(".cnf", "text/plain");
				base.AddMapping(".cod", "image/cis-cod");
				base.AddMapping(".cpio", "application/x-cpio");
				base.AddMapping(".cpp", "text/plain");
				base.AddMapping(".crd", "application/x-mscardfile");
				base.AddMapping(".crl", "application/pkix-crl");
				base.AddMapping(".crt", "application/x-x509-ca-cert");
				base.AddMapping(".csh", "application/x-csh");
				base.AddMapping(".css", "text/css");
				base.AddMapping(".csv", "application/octet-stream");
				base.AddMapping(".cur", "application/octet-stream");
				base.AddMapping(".dcr", "application/x-director");
				base.AddMapping(".deploy", "application/octet-stream");
				base.AddMapping(".der", "application/x-x509-ca-cert");
				base.AddMapping(".dib", "image/bmp");
				base.AddMapping(".dir", "application/x-director");
				base.AddMapping(".disco", "text/xml");
				base.AddMapping(".dll", "application/x-msdownload");
				base.AddMapping(".dll.config", "text/xml");
				base.AddMapping(".dlm", "text/dlm");
				base.AddMapping(".doc", "application/msword");
				base.AddMapping(".docm", "application/vnd.ms-word.document.macroEnabled.12");
				base.AddMapping(".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
				base.AddMapping(".dot", "application/msword");
				base.AddMapping(".dotm", "application/vnd.ms-word.template.macroEnabled.12");
				base.AddMapping(".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template");
				base.AddMapping(".dsp", "application/octet-stream");
				base.AddMapping(".dtd", "text/xml");
				base.AddMapping(".dvi", "application/x-dvi");
				base.AddMapping(".dwf", "drawing/x-dwf");
				base.AddMapping(".dwp", "application/octet-stream");
				base.AddMapping(".dxr", "application/x-director");
				base.AddMapping(".eml", "message/rfc822");
				base.AddMapping(".emz", "application/octet-stream");
				base.AddMapping(".eot", "application/octet-stream");
				base.AddMapping(".eps", "application/postscript");
				base.AddMapping(".etx", "text/x-setext");
				base.AddMapping(".evy", "application/envoy");
				base.AddMapping(".exe", "application/octet-stream");
				base.AddMapping(".exe.config", "text/xml");
				base.AddMapping(".fdf", "application/vnd.fdf");
				base.AddMapping(".fif", "application/fractals");
				base.AddMapping(".fla", "application/octet-stream");
				base.AddMapping(".flr", "x-world/x-vrml");
				base.AddMapping(".flv", "video/x-flv");
				base.AddMapping(".gif", "image/gif");
				base.AddMapping(".gtar", "application/x-gtar");
				base.AddMapping(".gz", "application/x-gzip");
				base.AddMapping(".h", "text/plain");
				base.AddMapping(".hdf", "application/x-hdf");
				base.AddMapping(".hdml", "text/x-hdml");
				base.AddMapping(".hhc", "application/x-oleobject");
				base.AddMapping(".hhk", "application/octet-stream");
				base.AddMapping(".hhp", "application/octet-stream");
				base.AddMapping(".hlp", "application/winhlp");
				base.AddMapping(".hqx", "application/mac-binhex40");
				base.AddMapping(".hta", "application/hta");
				base.AddMapping(".htc", "text/x-component");
				base.AddMapping(".htm", "text/html");
				base.AddMapping(".html", "text/html");
				base.AddMapping(".htt", "text/webviewhtml");
				base.AddMapping(".hxt", "text/html");
				base.AddMapping(".ico", "image/x-icon");
				base.AddMapping(".ics", "application/octet-stream");
				base.AddMapping(".ief", "image/ief");
				base.AddMapping(".iii", "application/x-iphone");
				base.AddMapping(".inf", "application/octet-stream");
				base.AddMapping(".ins", "application/x-internet-signup");
				base.AddMapping(".isp", "application/x-internet-signup");
				base.AddMapping(".IVF", "video/x-ivf");
				base.AddMapping(".jar", "application/java-archive");
				base.AddMapping(".java", "application/octet-stream");
				base.AddMapping(".jck", "application/liquidmotion");
				base.AddMapping(".jcz", "application/liquidmotion");
				base.AddMapping(".jfif", "image/pjpeg");
				base.AddMapping(".jpb", "application/octet-stream");
				base.AddMapping(".jpe", "image/jpeg");
				base.AddMapping(".jpeg", "image/jpeg");
				base.AddMapping(".jpg", "image/jpeg");
				base.AddMapping(".js", "application/x-javascript");
				base.AddMapping(".jsx", "text/jscript");
				base.AddMapping(".latex", "application/x-latex");
				base.AddMapping(".lit", "application/x-ms-reader");
				base.AddMapping(".lpk", "application/octet-stream");
				base.AddMapping(".lsf", "video/x-la-asf");
				base.AddMapping(".lsx", "video/x-la-asf");
				base.AddMapping(".lzh", "application/octet-stream");
				base.AddMapping(".m13", "application/x-msmediaview");
				base.AddMapping(".m14", "application/x-msmediaview");
				base.AddMapping(".m1v", "video/mpeg");
				base.AddMapping(".m3u", "audio/x-mpegurl");
				base.AddMapping(".man", "application/x-troff-man");
				base.AddMapping(".manifest", "application/x-ms-manifest");
				base.AddMapping(".map", "text/plain");
				base.AddMapping(".mdb", "application/x-msaccess");
				base.AddMapping(".mdp", "application/octet-stream");
				base.AddMapping(".me", "application/x-troff-me");
				base.AddMapping(".mht", "message/rfc822");
				base.AddMapping(".mhtml", "message/rfc822");
				base.AddMapping(".mid", "audio/mid");
				base.AddMapping(".midi", "audio/mid");
				base.AddMapping(".mix", "application/octet-stream");
				base.AddMapping(".mmf", "application/x-smaf");
				base.AddMapping(".mno", "text/xml");
				base.AddMapping(".mny", "application/x-msmoney");
				base.AddMapping(".mov", "video/quicktime");
				base.AddMapping(".movie", "video/x-sgi-movie");
				base.AddMapping(".mp2", "video/mpeg");
				base.AddMapping(".mp3", "audio/mpeg");
				base.AddMapping(".mpa", "video/mpeg");
				base.AddMapping(".mpe", "video/mpeg");
				base.AddMapping(".mpeg", "video/mpeg");
				base.AddMapping(".mpg", "video/mpeg");
				base.AddMapping(".mpp", "application/vnd.ms-project");
				base.AddMapping(".mpv2", "video/mpeg");
				base.AddMapping(".ms", "application/x-troff-ms");
				base.AddMapping(".msi", "application/octet-stream");
				base.AddMapping(".mso", "application/octet-stream");
				base.AddMapping(".mvb", "application/x-msmediaview");
				base.AddMapping(".mvc", "application/x-miva-compiled");
				base.AddMapping(".nc", "application/x-netcdf");
				base.AddMapping(".nsc", "video/x-ms-asf");
				base.AddMapping(".nws", "message/rfc822");
				base.AddMapping(".ocx", "application/octet-stream");
				base.AddMapping(".oda", "application/oda");
				base.AddMapping(".odc", "text/x-ms-odc");
				base.AddMapping(".ods", "application/oleobject");
				base.AddMapping(".one", "application/onenote");
				base.AddMapping(".onea", "application/onenote");
				base.AddMapping(".onetoc", "application/onenote");
				base.AddMapping(".onetoc2", "application/onenote");
				base.AddMapping(".onetmp", "application/onenote");
				base.AddMapping(".onepkg", "application/onenote");
				base.AddMapping(".osdx", "application/opensearchdescription+xml");
				base.AddMapping(".p10", "application/pkcs10");
				base.AddMapping(".p12", "application/x-pkcs12");
				base.AddMapping(".p7b", "application/x-pkcs7-certificates");
				base.AddMapping(".p7c", "application/pkcs7-mime");
				base.AddMapping(".p7m", "application/pkcs7-mime");
				base.AddMapping(".p7r", "application/x-pkcs7-certreqresp");
				base.AddMapping(".p7s", "application/pkcs7-signature");
				base.AddMapping(".pbm", "image/x-portable-bitmap");
				base.AddMapping(".pcx", "application/octet-stream");
				base.AddMapping(".pcz", "application/octet-stream");
				base.AddMapping(".pdf", "application/pdf");
				base.AddMapping(".pfb", "application/octet-stream");
				base.AddMapping(".pfm", "application/octet-stream");
				base.AddMapping(".pfx", "application/x-pkcs12");
				base.AddMapping(".pgm", "image/x-portable-graymap");
				base.AddMapping(".pko", "application/vnd.ms-pki.pko");
				base.AddMapping(".pma", "application/x-perfmon");
				base.AddMapping(".pmc", "application/x-perfmon");
				base.AddMapping(".pml", "application/x-perfmon");
				base.AddMapping(".pmr", "application/x-perfmon");
				base.AddMapping(".pmw", "application/x-perfmon");
				base.AddMapping(".png", "image/png");
				base.AddMapping(".pnm", "image/x-portable-anymap");
				base.AddMapping(".pnz", "image/png");
				base.AddMapping(".pot", "application/vnd.ms-powerpoint");
				base.AddMapping(".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12");
				base.AddMapping(".potx", "application/vnd.openxmlformats-officedocument.presentationml.template");
				base.AddMapping(".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12");
				base.AddMapping(".ppm", "image/x-portable-pixmap");
				base.AddMapping(".pps", "application/vnd.ms-powerpoint");
				base.AddMapping(".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12");
				base.AddMapping(".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow");
				base.AddMapping(".ppt", "application/vnd.ms-powerpoint");
				base.AddMapping(".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12");
				base.AddMapping(".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation");
				base.AddMapping(".prf", "application/pics-rules");
				base.AddMapping(".prm", "application/octet-stream");
				base.AddMapping(".prx", "application/octet-stream");
				base.AddMapping(".ps", "application/postscript");
				base.AddMapping(".psd", "application/octet-stream");
				base.AddMapping(".psm", "application/octet-stream");
				base.AddMapping(".psp", "application/octet-stream");
				base.AddMapping(".pub", "application/x-mspublisher");
				base.AddMapping(".qt", "video/quicktime");
				base.AddMapping(".qtl", "application/x-quicktimeplayer");
				base.AddMapping(".qxd", "application/octet-stream");
				base.AddMapping(".ra", "audio/x-pn-realaudio");
				base.AddMapping(".ram", "audio/x-pn-realaudio");
				base.AddMapping(".rar", "application/octet-stream");
				base.AddMapping(".ras", "image/x-cmu-raster");
				base.AddMapping(".rf", "image/vnd.rn-realflash");
				base.AddMapping(".rgb", "image/x-rgb");
				base.AddMapping(".rm", "application/vnd.rn-realmedia");
				base.AddMapping(".rmi", "audio/mid");
				base.AddMapping(".roff", "application/x-troff");
				base.AddMapping(".rpm", "audio/x-pn-realaudio-plugin");
				base.AddMapping(".rtf", "application/rtf");
				base.AddMapping(".rtx", "text/richtext");
				base.AddMapping(".scd", "application/x-msschedule");
				base.AddMapping(".sct", "text/scriptlet");
				base.AddMapping(".sea", "application/octet-stream");
				base.AddMapping(".setpay", "application/set-payment-initiation");
				base.AddMapping(".setreg", "application/set-registration-initiation");
				base.AddMapping(".sgml", "text/sgml");
				base.AddMapping(".sh", "application/x-sh");
				base.AddMapping(".shar", "application/x-shar");
				base.AddMapping(".sit", "application/x-stuffit");
				base.AddMapping(".sldm", "application/vnd.ms-powerpoint.slide.macroEnabled.12");
				base.AddMapping(".sldx", "application/vnd.openxmlformats-officedocument.presentationml.slide");
				base.AddMapping(".smd", "audio/x-smd");
				base.AddMapping(".smi", "application/octet-stream");
				base.AddMapping(".smx", "audio/x-smd");
				base.AddMapping(".smz", "audio/x-smd");
				base.AddMapping(".snd", "audio/basic");
				base.AddMapping(".snp", "application/octet-stream");
				base.AddMapping(".spc", "application/x-pkcs7-certificates");
				base.AddMapping(".spl", "application/futuresplash");
				base.AddMapping(".src", "application/x-wais-source");
				base.AddMapping(".ssm", "application/streamingmedia");
				base.AddMapping(".sst", "application/vnd.ms-pki.certstore");
				base.AddMapping(".stl", "application/vnd.ms-pki.stl");
				base.AddMapping(".sv4cpio", "application/x-sv4cpio");
				base.AddMapping(".sv4crc", "application/x-sv4crc");
				base.AddMapping(".swf", "application/x-shockwave-flash");
				base.AddMapping(".t", "application/x-troff");
				base.AddMapping(".tar", "application/x-tar");
				base.AddMapping(".tcl", "application/x-tcl");
				base.AddMapping(".tex", "application/x-tex");
				base.AddMapping(".texi", "application/x-texinfo");
				base.AddMapping(".texinfo", "application/x-texinfo");
				base.AddMapping(".tgz", "application/x-compressed");
				base.AddMapping(".thmx", "application/vnd.ms-officetheme");
				base.AddMapping(".thn", "application/octet-stream");
				base.AddMapping(".tif", "image/tiff");
				base.AddMapping(".tiff", "image/tiff");
				base.AddMapping(".toc", "application/octet-stream");
				base.AddMapping(".tr", "application/x-troff");
				base.AddMapping(".trm", "application/x-msterminal");
				base.AddMapping(".tsv", "text/tab-separated-values");
				base.AddMapping(".ttf", "application/octet-stream");
				base.AddMapping(".txt", "text/plain");
				base.AddMapping(".u32", "application/octet-stream");
				base.AddMapping(".uls", "text/iuls");
				base.AddMapping(".ustar", "application/x-ustar");
				base.AddMapping(".vbs", "text/vbscript");
				base.AddMapping(".vcf", "text/x-vcard");
				base.AddMapping(".vcs", "text/plain");
				base.AddMapping(".vdx", "application/vnd.ms-visio.viewer");
				base.AddMapping(".vml", "text/xml");
				base.AddMapping(".vsd", "application/vnd.visio");
				base.AddMapping(".vss", "application/vnd.visio");
				base.AddMapping(".vst", "application/vnd.visio");
				base.AddMapping(".vsto", "application/x-ms-vsto");
				base.AddMapping(".vsw", "application/vnd.visio");
				base.AddMapping(".vsx", "application/vnd.visio");
				base.AddMapping(".vtx", "application/vnd.visio");
				base.AddMapping(".wav", "audio/wav");
				base.AddMapping(".wax", "audio/x-ms-wax");
				base.AddMapping(".wbmp", "image/vnd.wap.wbmp");
				base.AddMapping(".wcm", "application/vnd.ms-works");
				base.AddMapping(".wdb", "application/vnd.ms-works");
				base.AddMapping(".wks", "application/vnd.ms-works");
				base.AddMapping(".wm", "video/x-ms-wm");
				base.AddMapping(".wma", "audio/x-ms-wma");
				base.AddMapping(".wmd", "application/x-ms-wmd");
				base.AddMapping(".wmf", "application/x-msmetafile");
				base.AddMapping(".wml", "text/vnd.wap.wml");
				base.AddMapping(".wmlc", "application/vnd.wap.wmlc");
				base.AddMapping(".wmls", "text/vnd.wap.wmlscript");
				base.AddMapping(".wmlsc", "application/vnd.wap.wmlscriptc");
				base.AddMapping(".wmp", "video/x-ms-wmp");
				base.AddMapping(".wmv", "video/x-ms-wmv");
				base.AddMapping(".wmx", "video/x-ms-wmx");
				base.AddMapping(".wmz", "application/x-ms-wmz");
				base.AddMapping(".wps", "application/vnd.ms-works");
				base.AddMapping(".wri", "application/x-mswrite");
				base.AddMapping(".wrl", "x-world/x-vrml");
				base.AddMapping(".wrz", "x-world/x-vrml");
				base.AddMapping(".wsdl", "text/xml");
				base.AddMapping(".wvx", "video/x-ms-wvx");
				base.AddMapping(".x", "application/directx");
				base.AddMapping(".xaf", "x-world/x-vrml");
				base.AddMapping(".xaml", "application/xaml+xml");
				base.AddMapping(".xap", "application/x-silverlight-app");
				base.AddMapping(".xbap", "application/x-ms-xbap");
				base.AddMapping(".xbm", "image/x-xbitmap");
				base.AddMapping(".xdr", "text/plain");
				base.AddMapping(".xla", "application/vnd.ms-excel");
				base.AddMapping(".xlam", "application/vnd.ms-excel.addin.macroEnabled.12");
				base.AddMapping(".xlc", "application/vnd.ms-excel");
				base.AddMapping(".xlm", "application/vnd.ms-excel");
				base.AddMapping(".xls", "application/vnd.ms-excel");
				base.AddMapping(".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12");
				base.AddMapping(".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12");
				base.AddMapping(".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
				base.AddMapping(".xlt", "application/vnd.ms-excel");
				base.AddMapping(".xltm", "application/vnd.ms-excel.template.macroEnabled.12");
				base.AddMapping(".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template");
				base.AddMapping(".xlw", "application/vnd.ms-excel");
				base.AddMapping(".xml", "text/xml");
				base.AddMapping(".xof", "x-world/x-vrml");
				base.AddMapping(".xpm", "image/x-xpixmap");
				base.AddMapping(".xps", "application/vnd.ms-xpsdocument");
				base.AddMapping(".xsd", "text/xml");
				base.AddMapping(".xsf", "text/xml");
				base.AddMapping(".xsl", "text/xml");
				base.AddMapping(".xslt", "text/xml");
				base.AddMapping(".xsn", "application/octet-stream");
				base.AddMapping(".xtp", "application/octet-stream");
				base.AddMapping(".xwd", "image/x-xwindowdump");
				base.AddMapping(".z", "application/x-compress");
				base.AddMapping(".zip", "application/x-zip-compressed");
			}
		}

		// Token: 0x020008E8 RID: 2280
		private sealed class MimeMappingDictionaryIntegrated : MimeMapping.MimeMappingDictionaryBase
		{
			// Token: 0x0600685B RID: 26715 RVA: 0x00173D51 File Offset: 0x00171F51
			public MimeMappingDictionaryIntegrated(IntPtr applicationContext)
			{
				this._applicationContext = applicationContext;
			}

			// Token: 0x0600685C RID: 26716 RVA: 0x00173D60 File Offset: 0x00171F60
			protected override void PopulateMappings()
			{
				IntPtr zero = IntPtr.Zero;
				try
				{
					int num;
					int errorCode = UnsafeIISMethods.MgdGetMimeMapCollection(IntPtr.Zero, this._applicationContext, out zero, out num);
					Marshal.ThrowExceptionForHR(errorCode);
					for (int i = 0; i < num; i++)
					{
						IntPtr zero2 = IntPtr.Zero;
						IntPtr zero3 = IntPtr.Zero;
						try
						{
							int num2;
							int num3;
							errorCode = UnsafeIISMethods.MgdGetNextMimeMap(zero, (uint)i, out zero2, out num2, out zero3, out num3);
							Marshal.ThrowExceptionForHR(errorCode);
							string fileExtension = (num2 > 0) ? StringUtil.StringFromWCharPtr(zero2, num2) : null;
							string mimeType = (num3 > 0) ? StringUtil.StringFromWCharPtr(zero3, num3) : null;
							base.AddMapping(fileExtension, mimeType);
						}
						finally
						{
							if (zero2 != IntPtr.Zero)
							{
								Marshal.FreeBSTR(zero2);
							}
							if (zero3 != IntPtr.Zero)
							{
								Marshal.FreeBSTR(zero3);
							}
						}
					}
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Marshal.Release(zero);
					}
				}
			}

			// Token: 0x04003656 RID: 13910
			private readonly IntPtr _applicationContext;
		}
	}
}
