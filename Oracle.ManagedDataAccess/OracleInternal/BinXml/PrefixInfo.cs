using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000021 RID: 33
	internal class PrefixInfo
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000B7F0 File Offset: 0x000099F0
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000B7F8 File Offset: 0x000099F8
		internal string Prefix { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000B804 File Offset: 0x00009A04
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000B80C File Offset: 0x00009A0C
		internal string Uri { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000B818 File Offset: 0x00009A18
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000B820 File Offset: 0x00009A20
		internal ulong Nsid { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000B82C File Offset: 0x00009A2C
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000B834 File Offset: 0x00009A34
		internal short PrefixId { get; set; }

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B840 File Offset: 0x00009A40
		internal PrefixInfo()
		{
			this.Prefix = string.Empty;
			this.Uri = string.Empty;
			this.Nsid = 0UL;
			this.PrefixId = 0;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000B870 File Offset: 0x00009A70
		internal PrefixInfo(ObxmlDecodeContext decodeContext, short pfxid, string prefix, ulong nsid, string uri = null)
		{
			if (!string.IsNullOrEmpty(uri))
			{
				this.Uri = uri;
			}
			else if (nsid != 0UL && decodeContext != null && decodeContext.TokenMap != null)
			{
				ObxmlToken obxmlToken = null;
				this.Uri = decodeContext.TokenMap.GetNamespaceUri(decodeContext, nsid, out obxmlToken);
			}
			else
			{
				this.Uri = string.Empty;
			}
			this.PrefixId = pfxid;
			this.Prefix = prefix;
			this.Nsid = nsid;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		internal static PrefixInfo GetPrefixInfo(ObxmlDecodeState decodeState, ulong prefixId, PrefixInfo prefixInfo, bool refetchPrefixInfo = false)
		{
			if (prefixId > 0UL && (refetchPrefixInfo || prefixInfo == null))
			{
				prefixInfo = decodeState.GetPrefixInfo(prefixId);
			}
			return prefixInfo;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B8FC File Offset: 0x00009AFC
		protected object Clone()
		{
			return new PrefixInfo(null, this.PrefixId, this.Prefix, this.Nsid, this.Uri)
			{
				Uri = this.Uri
			};
		}
	}
}
