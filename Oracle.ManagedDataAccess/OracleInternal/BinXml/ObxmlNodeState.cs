using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000022 RID: 34
	internal class ObxmlNodeState : ObxmlStateObject
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000B938 File Offset: 0x00009B38
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000B940 File Offset: 0x00009B40
		internal NodeTypes NodeType { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000B94C File Offset: 0x00009B4C
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000B954 File Offset: 0x00009B54
		internal ulong NodeDataLen { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000B960 File Offset: 0x00009B60
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000B968 File Offset: 0x00009B68
		internal string Prefix { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000B974 File Offset: 0x00009B74
		// (set) Token: 0x060001EF RID: 495 RVA: 0x0000B97C File Offset: 0x00009B7C
		internal short PrefixId { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000B988 File Offset: 0x00009B88
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x0000B990 File Offset: 0x00009B90
		internal ulong NsIndex { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000B99C File Offset: 0x00009B9C
		internal bool HasPrefix
		{
			get
			{
				return this.Prefix != null;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000B9AC File Offset: 0x00009BAC
		internal bool IsAttribute
		{
			get
			{
				return NodeTypes.Attribute == this.NodeType;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		private string qualifiedName { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000B9CC File Offset: 0x00009BCC
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		internal int NodeLevel { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		internal bool PendingDataNode { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000B9F4 File Offset: 0x00009BF4
		internal bool IsArrayMode
		{
			get
			{
				return (short)(this.NodeMask & NodeFlags.ARRMODE) > 0;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000BA04 File Offset: 0x00009C04
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000BA0C File Offset: 0x00009C0C
		internal bool BeginTagClosed { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000BA18 File Offset: 0x00009C18
		internal bool IsSequentialMode
		{
			get
			{
				return (short)(this.NodeMask & NodeFlags.SEQMODE) > 0;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000BA28 File Offset: 0x00009C28
		internal void SetArrayMode(bool hasArrayMode)
		{
			if (hasArrayMode)
			{
				this.NodeMask |= NodeFlags.ARRMODE;
				return;
			}
			this.ArrayModeCount = 0;
			this.NodeMask &= ~NodeFlags.ARRMODE;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000BA54 File Offset: 0x00009C54
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000BA5C File Offset: 0x00009C5C
		internal bool IsOptimizedOpcode { get; set; }

		// Token: 0x06000201 RID: 513 RVA: 0x0000BA68 File Offset: 0x00009C68
		internal ObxmlNodeState()
		{
			this.Reset();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000BA78 File Offset: 0x00009C78
		internal ObxmlNodeState(NodeTypes nodeType, ObxmlToken token)
		{
			this.NodeType = nodeType;
			this.m_ElementToken = token;
			this.NodeDataLen = 0UL;
			if (token != null)
			{
				this.NodeType = ObxmlNodeState.GetDefaultNodeTypeForTokenType(token.TokenType);
			}
			this.m_PrefixWhiteSpaces = ObxmlInstructionState.sXmlWhitespaceNewLine;
			this.m_ParentNode = null;
			this.NodeLevel = 1;
			this.PendingDataNode = false;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		internal ObxmlNodeState(NodeTypes nodeType, ObxmlToken token, ulong nodeDataLen)
		{
			this.NodeType = nodeType;
			this.m_ElementToken = token;
			this.NodeDataLen = nodeDataLen;
			if (token != null)
			{
				this.NodeType = ObxmlNodeState.GetDefaultNodeTypeForTokenType(token.TokenType);
			}
			this.m_PrefixWhiteSpaces = ObxmlInstructionState.sXmlWhitespaceNewLine;
			this.m_ParentNode = null;
			this.NodeLevel = 1;
			this.PendingDataNode = false;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000BB34 File Offset: 0x00009D34
		internal override void ClearStateObject()
		{
			this.Reset();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000BB3C File Offset: 0x00009D3C
		internal void Reset()
		{
			this.NodeType = NodeTypes.None;
			this.m_ElementToken = null;
			this.NodeDataLen = 0UL;
			this.Prefix = null;
			this.PrefixId = 0;
			this.NsIndex = 0UL;
			this.NodeMask = NodeFlags.SELFMODE;
			this.m_ParentNode = null;
			this.qualifiedName = null;
			this.ArrayModeCount = 0;
			this.ChildNodesCount = 0;
			this.m_PrefixWhiteSpaces = ObxmlInstructionState.sXmlWhitespaceNewLine;
			this.NodeLevel = 1;
			this.PendingDataNode = false;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		internal string ConvertToText(ObxmlDecodeState decodeState)
		{
			string text = "Node_Begin ";
			text += this.GetQualifiedName(decodeState);
			text += ". ";
			if (this.m_ElementToken != null && this.m_ElementToken.IsAttribute)
			{
				text += " Is ATTRIBUTE. ";
			}
			text = text + " NodeDataLen " + this.NodeDataLen;
			text = text + " Prefix " + this.Prefix;
			text = text + " PrefixId " + this.PrefixId;
			text = text + " NsIndex " + this.NsIndex;
			text = text + " NodeMask " + this.NodeMask;
			text = text + " ArrayModeCount " + this.ArrayModeCount;
			text = text + " ChildNodesCount " + this.ChildNodesCount;
			text = text + " NodeLevel " + this.NodeLevel;
			return text + " Node_End  ";
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000BCC4 File Offset: 0x00009EC4
		internal static NodeTypes GetDefaultNodeTypeForTokenType(TokenTypes tokenType)
		{
			switch (tokenType)
			{
			case TokenTypes.NamespaceToken:
				return NodeTypes.Namespace;
			case TokenTypes.AttributeToken:
				return NodeTypes.Attribute;
			case TokenTypes.PrefixToken:
				return NodeTypes.None;
			case TokenTypes.ElementToken:
				return NodeTypes.Element;
			default:
				return NodeTypes.None;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		internal static TokenTypes GetDefaultTokenTypeForNodeType(NodeTypes nodeType)
		{
			switch (nodeType)
			{
			case NodeTypes.None:
				return TokenTypes.None;
			case (NodeTypes)0:
				break;
			case NodeTypes.Element:
				return TokenTypes.ElementToken;
			case NodeTypes.Attribute:
				return TokenTypes.AttributeToken;
			default:
				if (nodeType == NodeTypes.Namespace)
				{
					return TokenTypes.NamespaceToken;
				}
				break;
			}
			return TokenTypes.None;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000BD30 File Offset: 0x00009F30
		internal AttributeInfo GetAttributeInfo(ObxmlDecodeState decodeState, ulong index)
		{
			return decodeState.AttributeList[(int)index];
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000BD40 File Offset: 0x00009F40
		internal void AddAttributeInfo(ObxmlDecodeState decodeState, AttributeInfo attributeInfo)
		{
			decodeState.AttributeList.Add(attributeInfo);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000BD50 File Offset: 0x00009F50
		internal string GetQualifiedName(ObxmlDecodeState decodeState)
		{
			if (!string.IsNullOrEmpty(this.qualifiedName))
			{
				return this.qualifiedName;
			}
			string prefix = this.Prefix;
			if (prefix == null && this.PrefixId > 0)
			{
				prefix = decodeState.GetPrefix(this.PrefixId);
			}
			if (this.m_ElementToken == null)
			{
				return this.qualifiedName = prefix;
			}
			if (string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(prefix.Trim()))
			{
				return this.qualifiedName = this.m_ElementToken.TokenName;
			}
			return this.qualifiedName = prefix + ":" + this.m_ElementToken.TokenName;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		internal string SetPrefix(ObxmlDecodeState decodeState, string px)
		{
			if (px != null)
			{
				this.Prefix = px;
			}
			else if (this.m_ElementToken.NamespaceId != 0UL)
			{
				this.Prefix = decodeState.GetNSPrefix(this.m_ElementToken.NamespaceId);
			}
			else if (!string.IsNullOrEmpty(this.m_ElementToken.Uri))
			{
				this.Prefix = decodeState.GetNSPrefix(this.m_ElementToken.Uri);
			}
			else
			{
				this.Prefix = null;
			}
			return this.Prefix;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000BE6C File Offset: 0x0000A06C
		internal ObxmlNodeState Clone()
		{
			ObxmlNodeState obxmlNodeState = new ObxmlNodeState();
			obxmlNodeState.NodeType = this.NodeType;
			obxmlNodeState.m_ElementToken = this.m_ElementToken;
			obxmlNodeState.NodeDataLen = this.NodeDataLen;
			obxmlNodeState.Prefix = this.Prefix;
			obxmlNodeState.PrefixId = this.PrefixId;
			obxmlNodeState.NsIndex = this.NsIndex;
			obxmlNodeState.NodeMask = NodeFlags.SELFMODE;
			obxmlNodeState.qualifiedName = this.qualifiedName;
			obxmlNodeState.ArrayModeCount = this.ArrayModeCount;
			if (obxmlNodeState.NodeType == NodeTypes.None && obxmlNodeState.m_ElementToken != null)
			{
				this.NodeType = ObxmlNodeState.GetDefaultNodeTypeForTokenType(this.m_ElementToken.TokenType);
			}
			return obxmlNodeState;
		}

		// Token: 0x04000157 RID: 343
		internal ObxmlNodeState m_ParentNode;

		// Token: 0x04000158 RID: 344
		internal ObxmlToken m_ElementToken;

		// Token: 0x04000159 RID: 345
		internal string m_PrefixWhiteSpaces;

		// Token: 0x0400015A RID: 346
		internal NodeFlags NodeMask;

		// Token: 0x0400015B RID: 347
		internal int ArrayModeCount;

		// Token: 0x0400015C RID: 348
		internal int ChildNodesCount;

		// Token: 0x0400015D RID: 349
		internal int NodeId;
	}
}
