using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public sealed class HttpParseException : HttpException
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x00016C58 File Offset: 0x00014E58
		public HttpParseException()
		{
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00016C60 File Offset: 0x00014E60
		public HttpParseException(string message) : base(message)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00016CA3 File Offset: 0x00014EA3
		public HttpParseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00016E70 File Offset: 0x00015070
		public HttpParseException(string message, Exception innerException, string virtualPath, string sourceCode, int line) : this(message, innerException, System.Web.VirtualPath.CreateAllowNull(virtualPath), sourceCode, line)
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00016E84 File Offset: 0x00015084
		internal HttpParseException(string message, Exception innerException, VirtualPath virtualPath, string sourceCode, int line) : base(message, innerException)
		{
			this._virtualPath = virtualPath;
			this._line = line;
			string message2;
			if (innerException != null)
			{
				message2 = innerException.Message;
			}
			else
			{
				message2 = message;
			}
			base.SetFormatter(new ParseErrorFormatter(this, System.Web.VirtualPath.GetVirtualPathString(virtualPath), sourceCode, line, message2));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00016ED0 File Offset: 0x000150D0
		private HttpParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._virtualPath = (VirtualPath)info.GetValue("_virtualPath", typeof(VirtualPath));
			this._line = info.GetInt32("_line");
			this._parserErrors = (ParserErrorCollection)info.GetValue("_parserErrors", typeof(ParserErrorCollection));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00016F36 File Offset: 0x00015136
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_virtualPath", this._virtualPath);
			info.AddValue("_line", this._line);
			info.AddValue("_parserErrors", this._parserErrors);
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00016F74 File Offset: 0x00015174
		public string FileName
		{
			get
			{
				string text = this._virtualPath.MapPathInternal();
				if (text == null)
				{
					return null;
				}
				InternalSecurityPermissions.PathDiscovery(text).Demand();
				return text;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00016F9E File Offset: 0x0001519E
		public string VirtualPath
		{
			get
			{
				return System.Web.VirtualPath.GetVirtualPathString(this._virtualPath);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00016FAB File Offset: 0x000151AB
		internal VirtualPath VirtualPathObject
		{
			get
			{
				return this._virtualPath;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00016FB3 File Offset: 0x000151B3
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00016FBC File Offset: 0x000151BC
		public ParserErrorCollection ParserErrors
		{
			get
			{
				if (this._parserErrors == null)
				{
					this._parserErrors = new ParserErrorCollection();
					ParserError value = new ParserError(this.Message, this._virtualPath, this._line);
					this._parserErrors.Add(value);
				}
				return this._parserErrors;
			}
		}

		// Token: 0x040003AC RID: 940
		private VirtualPath _virtualPath;

		// Token: 0x040003AD RID: 941
		private int _line;

		// Token: 0x040003AE RID: 942
		private ParserErrorCollection _parserErrors;
	}
}
