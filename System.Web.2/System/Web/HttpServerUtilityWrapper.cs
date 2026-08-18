using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x02000033 RID: 51
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public class HttpServerUtilityWrapper : HttpServerUtilityBase
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00005AB7 File Offset: 0x00003CB7
		public HttpServerUtilityWrapper(HttpServerUtility httpServerUtility)
		{
			if (httpServerUtility == null)
			{
				throw new ArgumentNullException("httpServerUtility");
			}
			this._httpServerUtility = httpServerUtility;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public override Exception GetLastError()
		{
			return this._httpServerUtility.GetLastError();
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00005AE1 File Offset: 0x00003CE1
		public override string MachineName
		{
			get
			{
				return this._httpServerUtility.MachineName;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00005AEE File Offset: 0x00003CEE
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00005AFB File Offset: 0x00003CFB
		public override int ScriptTimeout
		{
			get
			{
				return this._httpServerUtility.ScriptTimeout;
			}
			set
			{
				this._httpServerUtility.ScriptTimeout = value;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00005B09 File Offset: 0x00003D09
		public override void ClearError()
		{
			this._httpServerUtility.ClearError();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00005B16 File Offset: 0x00003D16
		public override object CreateObject(string progID)
		{
			return this._httpServerUtility.CreateObject(progID);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00005B24 File Offset: 0x00003D24
		public override object CreateObject(Type type)
		{
			return this._httpServerUtility.CreateObject(type);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00005B32 File Offset: 0x00003D32
		public override object CreateObjectFromClsid(string clsid)
		{
			return this._httpServerUtility.CreateObjectFromClsid(clsid);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00005B40 File Offset: 0x00003D40
		public override void Execute(string path)
		{
			this._httpServerUtility.Execute(path);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00005B4E File Offset: 0x00003D4E
		public override void Execute(string path, TextWriter writer)
		{
			this._httpServerUtility.Execute(path, writer);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00005B5D File Offset: 0x00003D5D
		public override void Execute(string path, bool preserveForm)
		{
			this._httpServerUtility.Execute(path, preserveForm);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00005B6C File Offset: 0x00003D6C
		public override void Execute(string path, TextWriter writer, bool preserveForm)
		{
			this._httpServerUtility.Execute(path, writer, preserveForm);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00005B7C File Offset: 0x00003D7C
		public override void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
		{
			this._httpServerUtility.Execute(handler, writer, preserveForm);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00005B8C File Offset: 0x00003D8C
		public override string HtmlDecode(string s)
		{
			return this._httpServerUtility.HtmlDecode(s);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00005B9A File Offset: 0x00003D9A
		public override void HtmlDecode(string s, TextWriter output)
		{
			this._httpServerUtility.HtmlDecode(s, output);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00005BA9 File Offset: 0x00003DA9
		public override string HtmlEncode(string s)
		{
			return this._httpServerUtility.HtmlEncode(s);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00005BB7 File Offset: 0x00003DB7
		public override void HtmlEncode(string s, TextWriter output)
		{
			this._httpServerUtility.HtmlEncode(s, output);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00005BC6 File Offset: 0x00003DC6
		public override string MapPath(string path)
		{
			return this._httpServerUtility.MapPath(path);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public override void Transfer(string path, bool preserveForm)
		{
			this._httpServerUtility.Transfer(path, preserveForm);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00005BE3 File Offset: 0x00003DE3
		public override void Transfer(string path)
		{
			this._httpServerUtility.Transfer(path);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00005BF1 File Offset: 0x00003DF1
		public override void Transfer(IHttpHandler handler, bool preserveForm)
		{
			this._httpServerUtility.Transfer(handler, preserveForm);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00005C00 File Offset: 0x00003E00
		public override void TransferRequest(string path)
		{
			this._httpServerUtility.TransferRequest(path);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00005C0E File Offset: 0x00003E0E
		public override void TransferRequest(string path, bool preserveForm)
		{
			this._httpServerUtility.TransferRequest(path, preserveForm);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00005C1D File Offset: 0x00003E1D
		public override void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
		{
			this._httpServerUtility.TransferRequest(path, preserveForm, method, headers);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00005C2F File Offset: 0x00003E2F
		public override void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers, bool preserveUser)
		{
			this._httpServerUtility.TransferRequest(path, preserveForm, method, headers, preserveUser);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00005C43 File Offset: 0x00003E43
		public override string UrlDecode(string s)
		{
			return this._httpServerUtility.UrlDecode(s);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00005C51 File Offset: 0x00003E51
		public override void UrlDecode(string s, TextWriter output)
		{
			this._httpServerUtility.UrlDecode(s, output);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00005C60 File Offset: 0x00003E60
		public override string UrlEncode(string s)
		{
			return this._httpServerUtility.UrlEncode(s);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00005C6E File Offset: 0x00003E6E
		public override void UrlEncode(string s, TextWriter output)
		{
			this._httpServerUtility.UrlEncode(s, output);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00005C7D File Offset: 0x00003E7D
		public override string UrlPathEncode(string s)
		{
			return this._httpServerUtility.UrlPathEncode(s);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00005C8B File Offset: 0x00003E8B
		public override byte[] UrlTokenDecode(string input)
		{
			return HttpServerUtility.UrlTokenDecode(input);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00005C93 File Offset: 0x00003E93
		public override string UrlTokenEncode(byte[] input)
		{
			return HttpServerUtility.UrlTokenEncode(input);
		}

		// Token: 0x0400010F RID: 271
		private HttpServerUtility _httpServerUtility;
	}
}
