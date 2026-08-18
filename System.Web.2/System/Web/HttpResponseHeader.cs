using System;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000B4 RID: 180
	[Serializable]
	internal class HttpResponseHeader
	{
		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001F6A2 File Offset: 0x0001D8A2
		internal HttpResponseHeader(int knownHeaderIndex, string value) : this(knownHeaderIndex, value, HttpRuntime.EnableHeaderChecking)
		{
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		internal HttpResponseHeader(int knownHeaderIndex, string value, bool enableHeaderChecking)
		{
			this._unknownHeader = null;
			this._knownHeaderIndex = knownHeaderIndex;
			if (enableHeaderChecking)
			{
				string text;
				HttpEncoder.Current.HeaderNameValueEncode(this.Name, value, out text, out this._value);
				return;
			}
			this._value = value;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001F6F9 File Offset: 0x0001D8F9
		internal HttpResponseHeader(string unknownHeader, string value) : this(unknownHeader, value, HttpRuntime.EnableHeaderChecking)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001F708 File Offset: 0x0001D908
		internal HttpResponseHeader(string unknownHeader, string value, bool enableHeaderChecking)
		{
			if (enableHeaderChecking)
			{
				HttpEncoder.Current.HeaderNameValueEncode(unknownHeader, value, out this._unknownHeader, out this._value);
				this._knownHeaderIndex = HttpWorkerRequest.GetKnownResponseHeaderIndex(this._unknownHeader);
				return;
			}
			this._unknownHeader = unknownHeader;
			this._knownHeaderIndex = HttpWorkerRequest.GetKnownResponseHeaderIndex(this._unknownHeader);
			this._value = value;
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001F767 File Offset: 0x0001D967
		internal string Name
		{
			get
			{
				if (this._unknownHeader != null)
				{
					return this._unknownHeader;
				}
				return HttpWorkerRequest.GetKnownResponseHeaderName(this._knownHeaderIndex);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001F783 File Offset: 0x0001D983
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001F78B File Offset: 0x0001D98B
		internal void Send(HttpWorkerRequest wr)
		{
			if (this._knownHeaderIndex >= 0)
			{
				wr.SendKnownResponseHeader(this._knownHeaderIndex, this._value);
				return;
			}
			wr.SendUnknownResponseHeader(this._unknownHeader, this._value);
		}

		// Token: 0x04000471 RID: 1137
		private string _unknownHeader;

		// Token: 0x04000472 RID: 1138
		private int _knownHeaderIndex;

		// Token: 0x04000473 RID: 1139
		private string _value;
	}
}
