using System;

namespace System.Web
{
	// Token: 0x0200009D RID: 157
	[Serializable]
	public sealed class ParserError
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x000030B5 File Offset: 0x000012B5
		public ParserError()
		{
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00017027 File Offset: 0x00015227
		public ParserError(string errorText, string virtualPath, int line) : this(errorText, System.Web.VirtualPath.CreateAllowNull(virtualPath), line)
		{
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00017037 File Offset: 0x00015237
		internal ParserError(string errorText, VirtualPath virtualPath, int line)
		{
			this._virtualPath = virtualPath;
			this._line = line;
			this._errorText = errorText;
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00017054 File Offset: 0x00015254
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0001705C File Offset: 0x0001525C
		internal Exception Exception
		{
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00017065 File Offset: 0x00015265
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x00017072 File Offset: 0x00015272
		public string VirtualPath
		{
			get
			{
				return System.Web.VirtualPath.GetVirtualPathString(this._virtualPath);
			}
			set
			{
				this._virtualPath = System.Web.VirtualPath.Create(value);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00017080 File Offset: 0x00015280
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00017088 File Offset: 0x00015288
		public string ErrorText
		{
			get
			{
				return this._errorText;
			}
			set
			{
				this._errorText = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00017091 File Offset: 0x00015291
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x00017099 File Offset: 0x00015299
		public int Line
		{
			get
			{
				return this._line;
			}
			set
			{
				this._line = value;
			}
		}

		// Token: 0x040003AF RID: 943
		private int _line;

		// Token: 0x040003B0 RID: 944
		private VirtualPath _virtualPath;

		// Token: 0x040003B1 RID: 945
		private string _errorText;

		// Token: 0x040003B2 RID: 946
		private Exception _exception;
	}
}
