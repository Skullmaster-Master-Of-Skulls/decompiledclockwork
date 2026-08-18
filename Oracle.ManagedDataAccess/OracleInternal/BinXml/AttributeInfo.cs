using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000020 RID: 32
	internal class AttributeInfo
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000B348 File Offset: 0x00009548
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000B350 File Offset: 0x00009550
		internal bool IsNamespaceAttribute { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000B35C File Offset: 0x0000955C
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000B364 File Offset: 0x00009564
		internal string Prefix { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000B370 File Offset: 0x00009570
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000B378 File Offset: 0x00009578
		internal ulong PrefixId { get; set; }

		// Token: 0x1700007A RID: 122
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000B384 File Offset: 0x00009584
		internal string LocalName
		{
			set
			{
				this.localName = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000B390 File Offset: 0x00009590
		internal string Namespace
		{
			set
			{
				this.nameSpace = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000B39C File Offset: 0x0000959C
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000B3A4 File Offset: 0x000095A4
		internal string Value
		{
			get
			{
				return this.m_attributeValue;
			}
			set
			{
				this.m_attributeValue = value.ReplaceXmlChars();
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000B3B4 File Offset: 0x000095B4
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000B3BC File Offset: 0x000095BC
		private string attributeString { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000B3C8 File Offset: 0x000095C8
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000B3D0 File Offset: 0x000095D0
		private string localName { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000B3DC File Offset: 0x000095DC
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000B3E4 File Offset: 0x000095E4
		private string nameSpace { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000B3F0 File Offset: 0x000095F0
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000B3F8 File Offset: 0x000095F8
		private string qualifiedName { get; set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x0000B404 File Offset: 0x00009604
		internal AttributeInfo()
		{
			this.m_nodeState = null;
			this.IsNamespaceAttribute = false;
			this.PrefixId = 0UL;
			this.Value = string.Empty;
			this.LocalName = string.Empty;
			this.Namespace = string.Empty;
			this.prefixInfo = null;
			this.Prefix = string.Empty;
			this.attributeString = null;
			this.qualifiedName = null;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000B470 File Offset: 0x00009670
		internal AttributeInfo(ObxmlDecodeState decodeState, ObxmlNodeState nodeState, bool isNamespaceAttribute, ulong pfxid, string localName, string nameSpace, string value)
		{
			this.m_nodeState = nodeState;
			this.IsNamespaceAttribute = isNamespaceAttribute;
			this.PrefixId = pfxid;
			this.Value = value;
			this.LocalName = localName;
			this.Namespace = nameSpace;
			this.prefixInfo = null;
			this.Prefix = string.Empty;
			this.attributeString = null;
			this.qualifiedName = null;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000B4D4 File Offset: 0x000096D4
		internal AttributeInfo(ObxmlDecodeState decodeState, ObxmlNodeState nodeState, bool isNamespaceAttribute, PrefixInfo pfxInfo, string localname, string nmspace, string value)
		{
			this.m_nodeState = nodeState;
			this.IsNamespaceAttribute = isNamespaceAttribute;
			this.prefixInfo = pfxInfo;
			this.PrefixId = (ulong)((this.prefixInfo == null) ? 0L : ((long)this.prefixInfo.PrefixId));
			this.Value = value;
			this.LocalName = localname;
			this.Namespace = nmspace;
			this.Prefix = string.Empty;
			this.attributeString = null;
			this.qualifiedName = null;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000B54C File Offset: 0x0000974C
		internal AttributeInfo(ObxmlDecodeState decodeState, ObxmlNodeState nodeState, bool isNamespaceAttribute, string prefix, string localname, string nmspace, string value)
		{
			this.m_nodeState = nodeState;
			this.IsNamespaceAttribute = isNamespaceAttribute;
			this.Prefix = prefix;
			this.prefixInfo = null;
			this.PrefixId = 0UL;
			this.Value = value;
			this.LocalName = localname;
			this.Namespace = nmspace;
			this.attributeString = null;
			this.qualifiedName = null;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B5AC File Offset: 0x000097AC
		internal string GetQualifiedName(ObxmlDecodeState decodeState)
		{
			if (!string.IsNullOrEmpty(this.qualifiedName))
			{
				return this.qualifiedName;
			}
			if (!string.IsNullOrEmpty(this.Prefix))
			{
				this.Prefix.Trim();
			}
			string prefix = this.Prefix;
			if (string.IsNullOrEmpty(prefix) && this.prefixInfo != null)
			{
				prefix = this.prefixInfo.Prefix;
			}
			if (!string.IsNullOrEmpty(prefix))
			{
				return this.GetQualifiedName(prefix, this.GetLocalName(decodeState));
			}
			ulong num = this.PrefixId;
			if (num <= 0UL && this.prefixInfo != null && this.prefixInfo.PrefixId > 0)
			{
				num = (ulong)((long)this.prefixInfo.PrefixId);
			}
			if (num > 0UL)
			{
				prefix = decodeState.GetPrefix((short)num);
			}
			return this.GetQualifiedName(prefix, this.GetLocalName(decodeState));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000B66C File Offset: 0x0000986C
		internal string GetQualifiedName(string prefix, string localName)
		{
			if (string.IsNullOrEmpty(prefix))
			{
				this.qualifiedName = localName;
				return localName;
			}
			if (!string.IsNullOrEmpty(localName))
			{
				return this.qualifiedName = prefix + ":" + localName;
			}
			this.qualifiedName = prefix;
			return prefix;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000B6B4 File Offset: 0x000098B4
		internal PrefixInfo GetPrefixInfo(ObxmlDecodeState decodeState, bool refetchPrefixInfo = false)
		{
			return PrefixInfo.GetPrefixInfo(decodeState, this.PrefixId, this.prefixInfo, refetchPrefixInfo);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B6CC File Offset: 0x000098CC
		internal string GetLocalName(ObxmlDecodeState decodeState)
		{
			if (this.localName != null)
			{
				return this.localName;
			}
			return string.Empty;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000B6E4 File Offset: 0x000098E4
		internal string GetNamespace(ObxmlDecodeState decodeState)
		{
			if (this.nameSpace != null)
			{
				return this.nameSpace;
			}
			this.nameSpace = decodeState.GetNamespace(this.m_nodeState.m_ElementToken.NamespaceId, TokenTypes.NamespaceToken);
			return this.nameSpace;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000B718 File Offset: 0x00009918
		internal string GetQualifiedAttributeString(ObxmlDecodeState decodeState)
		{
			if (!string.IsNullOrEmpty(this.attributeString))
			{
				return this.attributeString;
			}
			this.GetQualifiedName(decodeState);
			this.GetNamespace(decodeState);
			this.attributeString = string.Concat(new string[]
			{
				" ",
				this.qualifiedName,
				"=\"",
				this.Value,
				"\""
			});
			return this.attributeString;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000B78C File Offset: 0x0000998C
		protected object Clone(ObxmlDecodeState decodeState, bool refetchPrefixInfo = false)
		{
			this.prefixInfo = this.GetPrefixInfo(decodeState, refetchPrefixInfo);
			AttributeInfo attributeInfo = new AttributeInfo(decodeState, this.m_nodeState, this.IsNamespaceAttribute, this.prefixInfo, this.GetLocalName(decodeState), this.GetNamespace(decodeState), this.Value);
			if (!string.IsNullOrEmpty(this.Prefix))
			{
				attributeInfo.Prefix = this.Prefix;
			}
			return attributeInfo;
		}

		// Token: 0x04000149 RID: 329
		private ObxmlNodeState m_nodeState;

		// Token: 0x0400014A RID: 330
		private string m_attributeValue;

		// Token: 0x0400014B RID: 331
		private PrefixInfo prefixInfo;
	}
}
