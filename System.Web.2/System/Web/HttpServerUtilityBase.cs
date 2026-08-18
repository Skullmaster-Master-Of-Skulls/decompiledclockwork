using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x02000032 RID: 50
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpServerUtilityBase
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string MachineName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00003ABB File Offset: 0x00001CBB
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ScriptTimeout
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

		// Token: 0x06000439 RID: 1081 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void ClearError()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object CreateObject(string progID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object CreateObject(Type type)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual object CreateObjectFromClsid(string clsid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Execute(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Execute(string path, TextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Execute(string path, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Execute(string path, TextWriter writer, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Exception GetLastError()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string HtmlDecode(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void HtmlDecode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string HtmlEncode(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void HtmlEncode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string MapPath(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Transfer(string path, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Transfer(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void Transfer(IHttpHandler handler, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void TransferRequest(string path)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void TransferRequest(string path, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers, bool preserveUser)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UrlDecode(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void UrlDecode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UrlEncode(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void UrlEncode(string s, TextWriter output)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UrlPathEncode(string s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual byte[] UrlTokenDecode(string input)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string UrlTokenEncode(byte[] input)
		{
			throw new NotImplementedException();
		}
	}
}
