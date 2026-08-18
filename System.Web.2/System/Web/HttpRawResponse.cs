using System;
using System.Collections;

namespace System.Web
{
	// Token: 0x020000AA RID: 170
	internal class HttpRawResponse
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00017F14 File Offset: 0x00016114
		internal HttpRawResponse(int statusCode, string statusDescription, ArrayList headers, ArrayList buffers, bool hasSubstBlocks)
		{
			this._statusCode = statusCode;
			this._statusDescr = statusDescription;
			this._headers = headers;
			this._buffers = buffers;
			this._hasSubstBlocks = hasSubstBlocks;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00017F41 File Offset: 0x00016141
		internal int StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x00017F49 File Offset: 0x00016149
		internal string StatusDescription
		{
			get
			{
				return this._statusDescr;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00017F51 File Offset: 0x00016151
		internal ArrayList Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00017F59 File Offset: 0x00016159
		internal ArrayList Buffers
		{
			get
			{
				return this._buffers;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00017F61 File Offset: 0x00016161
		internal bool HasSubstBlocks
		{
			get
			{
				return this._hasSubstBlocks;
			}
		}

		// Token: 0x040003CB RID: 971
		private int _statusCode;

		// Token: 0x040003CC RID: 972
		private string _statusDescr;

		// Token: 0x040003CD RID: 973
		private ArrayList _headers;

		// Token: 0x040003CE RID: 974
		private ArrayList _buffers;

		// Token: 0x040003CF RID: 975
		private bool _hasSubstBlocks;
	}
}
