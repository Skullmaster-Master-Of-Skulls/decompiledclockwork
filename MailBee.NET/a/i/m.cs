using System;
using System.Text.RegularExpressions;

namespace a.i
{
	// Token: 0x020001F5 RID: 501
	internal class m
	{
		// Token: 0x04000BC5 RID: 3013
		public static readonly Regex a = new Regex("[^@\\s]+@\\S+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BC6 RID: 3014
		public static readonly Regex b = new Regex("=\\?[^\\s?]+\\?(Q|B)\\?.+\\?=", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BC7 RID: 3015
		public static readonly Regex c = new Regex("[\\s]*(?<name>[\\w]+)([\\s]*=[\\s]*(?(\")([\"](?<value>[^\"]*)[\"])|(?(')(['](?<value>[^']*)['])|((?<value>[^;]+))))){0,1}", RegexOptions.Compiled);

		// Token: 0x04000BC8 RID: 3016
		public static readonly Regex d = new Regex("[\\s]*(?<name>[\\w-_]+)(\\*(\\d)+)?(?<decode>(\\*))?([\\s]*=[\\s]*(?(\")([\"](?<value>[^\"]*)[\"]?)|(?(decode)(?<value>[^;\\r\\n]+)|((?(')(['](?<value>[^']*)['])|((?<value>[^;\\r\\n]+))))))){0,1}", RegexOptions.Compiled);

		// Token: 0x04000BC9 RID: 3017
		public static readonly Regex e = new Regex("(\\s)*(?<encoding>[\\w-\\d]*')*(?<lang>[\\w-\\d]*')(?<value>[^']*)", RegexOptions.Compiled);

		// Token: 0x04000BCA RID: 3018
		public static readonly Regex f = new Regex("^((?<dayWeek>[a-z]*),[\\s]*){0,1}(?<day>\\d{1,2})\\s+(?<month>[a-z]*)\\s+(?<year>\\d{2,4})\\s+(?<hour>\\d{1,2}):(?<minute>\\d{1,2})(:(?<second>\\d{1,2})){0,1}([\\s]+(?<offset>[+,-]?\\d{1,4}))?([\\s]*(?<zone>\\(?([^\\)]+)\\)?))?$", RegexOptions.Compiled);

		// Token: 0x04000BCB RID: 3019
		public static readonly Regex g = new Regex("\\s*content-\\w+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BCC RID: 3020
		public static readonly Regex h = new Regex("^begin \\d{1,3} (?<filename>[^\\r\\n]*)", RegexOptions.Multiline | RegexOptions.Compiled);

		// Token: 0x04000BCD RID: 3021
		public static readonly Regex i = new Regex("^end\\r\\n", RegexOptions.Multiline | RegexOptions.Compiled);

		// Token: 0x04000BCE RID: 3022
		public static readonly Regex j = new Regex("charset=[^\"'>\\s]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BCF RID: 3023
		public static readonly Regex k = new Regex("<(?<tagName>\\w+)\\s*(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BD0 RID: 3024
		public static readonly Regex l = new Regex("^\\S*&gt;[^\\r]*(\\r|$)", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

		// Token: 0x04000BD1 RID: 3025
		public static readonly Regex m = new Regex("href[\\s]*=[\\s]*(?<srcText>(['\"][^'\"]+['\"])|(\\S+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BD2 RID: 3026
		public static readonly Regex n = new Regex("alt[\\s]*=[\\s]*(?<altText>(['\"][^'\"]+['\"])|(\\S+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BD3 RID: 3027
		public static readonly Regex o = new Regex("src[\\s]*=[\\s]*[']{0,1}[\"]{0,1}(?<srcText>[\\S]*)[']{0,1}[\"]{0,1}[\\s]{1,}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BD4 RID: 3028
		public static readonly Regex p = new Regex("<(/?)body(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BD5 RID: 3029
		public static readonly Regex q = new Regex("<pre[^\\>]*>((?!\\<pre\\>.*\\</pre\\>).*?)</pre>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BD6 RID: 3030
		public static readonly Regex r = new Regex("(\\s)+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BD7 RID: 3031
		public static readonly Regex s = new Regex("<style[^\\>]*>((?!\\<style\\>.*\\</style\\>).*?)</style>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BD8 RID: 3032
		public static readonly Regex t = new Regex("<script[^\\>]*>((?!\\<script\\>.*\\</script\\>).*?)</script>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BD9 RID: 3033
		public static readonly Regex u = new Regex("&(?<specSymb>(\\w+|#\\d+));", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BDA RID: 3034
		public static readonly Regex v = new Regex("\\w+://.+", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BDB RID: 3035
		public static readonly Regex w = new Regex("[\\s]+(?<paramName>[\\w]+)([\\s]*=[\\s]*(?(\")([\"](?<paramValue>[^\"]*)[\"])|(?(')(['](?<paramValue>[^']*)['])|((?<paramValue>\\S+))))){0,1}", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

		// Token: 0x04000BDC RID: 3036
		public static readonly Regex x = new Regex("<a(\\s+[^= >]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>(?<tagContent>[^<]*)</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BDD RID: 3037
		public static readonly Regex y = new Regex("(http|https|ftp|nntp|file|telnet|gopher|wais|prospero)://[\\w\\d!\\$&'\\(\\)\\*\\+-\\./:\\?@_~%=;,#]+", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BDE RID: 3038
		public static readonly Regex z = new Regex("((\r){0,1}\n){1}", RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BDF RID: 3039
		public static readonly Regex aa = new Regex("</?(?<tagName>\\w+)(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BE0 RID: 3040
		public static readonly Regex ab = new Regex("<(?<tagName>[^\\s]+)\\s*(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BE1 RID: 3041
		public static readonly Regex ac = new Regex("charset=(?<paramCharset>[^\"'>\\s]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000BE2 RID: 3042
		public static readonly Regex ad = new Regex("\\w+:/+", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x04000BE3 RID: 3043
		public static readonly Regex ae = new Regex("(?<Path>(\\w){1}:\\\\([^\\\\]+\\\\)*[^\\.]+.\\w+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x020001F6 RID: 502
		public class a
		{
			// Token: 0x04000BE4 RID: 3044
			public static readonly Regex a = new Regex("(?<date>((\\w){3},\\s+){0,1}(\\d){1,2}\\s(\\w){3}\\s(\\d){4}\\s(\\d){2}:(\\d){2}(:(\\d){2}){0,1}(\\s+[+,-](\\d){4}){0,1}(\\s+\\((\\w*)\\)){0,1})", RegexOptions.Compiled | RegexOptions.Singleline);

			// Token: 0x04000BE5 RID: 3045
			public static readonly Regex b = new Regex("(?<=\\s+|^)([(]?from|[(]?id|[(]?by|[(]?for|[(]?with|[(]?via)\\s+");

			// Token: 0x04000BE6 RID: 3046
			public static readonly Regex c = new Regex("[+,-](\\d){4}", RegexOptions.Compiled);
		}
	}
}
