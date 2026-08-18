using System;
using System.Collections.Generic;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x02000017 RID: 23
	internal class ObxmlDecodeState : ObxmlStateObject
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000442C File Offset: 0x0000262C
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004434 File Offset: 0x00002634
		private Dictionary<int, PrefixInfo> m_PrefixTable { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004440 File Offset: 0x00002640
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00004448 File Offset: 0x00002648
		private Dictionary<int, PrefixInfo> m_NsPrefixTable { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004454 File Offset: 0x00002654
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000445C File Offset: 0x0000265C
		internal List<AttributeInfo> AttributeList { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004468 File Offset: 0x00002668
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004470 File Offset: 0x00002670
		internal DecodeStates DecodeStateMask { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000447C File Offset: 0x0000267C
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004484 File Offset: 0x00002684
		internal DataOperationTypes CurrentDataOperation { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004490 File Offset: 0x00002690
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004498 File Offset: 0x00002698
		internal ObxmlSectionHeader SectionHeader { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000044A4 File Offset: 0x000026A4
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000044AC File Offset: 0x000026AC
		internal ObxmlDocHeader DocHeader { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000044B8 File Offset: 0x000026B8
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000044C0 File Offset: 0x000026C0
		internal byte[] CurrentCsxBuffer { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000044CC File Offset: 0x000026CC
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000044D4 File Offset: 0x000026D4
		internal byte[] LastCsxBuffer { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000044E0 File Offset: 0x000026E0
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000044E8 File Offset: 0x000026E8
		internal bool IsExtendedOpcode { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000044F4 File Offset: 0x000026F4
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000044FC File Offset: 0x000026FC
		internal ObxmlOpcode.OpcodeIds PrtOpcode { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00004508 File Offset: 0x00002708
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00004510 File Offset: 0x00002710
		internal string PrtBuffer { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000451C File Offset: 0x0000271C
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00004524 File Offset: 0x00002724
		internal bool SectionHeaderFound { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004530 File Offset: 0x00002730
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00004538 File Offset: 0x00002738
		internal ObxmlDecoder Parent { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00004544 File Offset: 0x00002744
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000454C File Offset: 0x0000274C
		internal ObxmlErrorTypes ErrorType { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00004558 File Offset: 0x00002758
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00004560 File Offset: 0x00002760
		internal ObxmlDecodeResponse LastDecodeResult { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000456C File Offset: 0x0000276C
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00004574 File Offset: 0x00002774
		internal bool HasBeginTagClosurePending { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00004580 File Offset: 0x00002780
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00004588 File Offset: 0x00002788
		internal int LastNodeId { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004594 File Offset: 0x00002794
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000459C File Offset: 0x0000279C
		internal bool ProcessingTokenSet { get; set; }

		// Token: 0x06000115 RID: 277 RVA: 0x000045A8 File Offset: 0x000027A8
		internal ObxmlDecodeState()
		{
			this.m_CurrentInstruction = new ObxmlInstructionState();
			this.m_LastInstruction = new ObxmlInstructionState();
			this.CurrentCsxBuffer = null;
			this.LastCsxBuffer = null;
			this.PrtOpcode = ObxmlOpcode.OpcodeIds.None;
			this.PrtBuffer = null;
			this.LastNodeId = 0;
			this.m_BinXmlStream = null;
			this.HasBeginTagClosurePending = false;
			this.Parent = null;
			this.ErrorType = ObxmlErrorTypes.Success;
			this.ResetDecodeStateMaskToNone();
			this.m_CurrentNode = null;
			this.m_bInit = false;
			this.m_PreviousSibling = null;
			this.m_NodeStates = new Stack<ObxmlNodeState>();
			this.m_NsStack = new List<PrefixInfo>(20);
			this.m_DTDInfo = new ObxmlDTDInfo(this);
			this.m_RequestObject = new ObxmlDecodeRequest();
			this.SectionHeader = new ObxmlSectionHeader();
			this.DocHeader = new ObxmlDocHeader();
			this.LastDecodeResult = new ObxmlDecodeResponse(this);
			this.m_PrefixTable = new Dictionary<int, PrefixInfo>();
			this.m_NsPrefixTable = new Dictionary<int, PrefixInfo>();
			this.CurrentDataOperation = DataOperationTypes.NoData;
			this.IsExtendedOpcode = false;
			this.SectionHeaderFound = false;
			this.AttributeList = new List<AttributeInfo>(100);
			this.ProcessingTokenSet = false;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000046BC File Offset: 0x000028BC
		internal override void ClearStateObject()
		{
			if (this.m_bInit)
			{
				if (this.m_CurrentInstruction != null)
				{
					this.m_CurrentInstruction.ClearStateObject();
				}
				if (this.m_LastInstruction != null)
				{
					this.m_LastInstruction.ClearStateObject();
				}
				this.CurrentCsxBuffer = null;
				this.LastCsxBuffer = null;
				this.PrtOpcode = ObxmlOpcode.OpcodeIds.None;
				this.PrtBuffer = null;
				this.LastNodeId = 0;
				if (this.m_BinXmlStream != null)
				{
					this.m_BinXmlStream.Dispose();
					this.m_BinXmlStream = null;
				}
				this.HasBeginTagClosurePending = false;
				this.Parent = null;
				this.ResetDecodeStateMaskToNone();
				this.ErrorType = ObxmlErrorTypes.Success;
				this.m_CurrentNode = null;
				this.m_bInit = false;
				this.m_PreviousSibling = null;
				if (this.m_NodeStates != null)
				{
					this.m_NodeStates.Clear();
				}
				if (this.m_NsStack != null)
				{
					this.m_NsStack.Clear();
				}
				if (this.m_DTDInfo != null)
				{
					this.m_DTDInfo.ClearStateObject();
					this.m_DTDInfo.SetDecodeStateObject(this);
				}
				this.m_RequestObject.ResetRequestObject();
				this.SectionHeader.ClearStateObject();
				this.DocHeader.ClearStateObject();
				this.LastDecodeResult.ResetResponseObject(this, ObxmlErrorTypes.Success);
				if (this.m_PrefixTable != null)
				{
					this.m_PrefixTable.Clear();
				}
				if (this.m_NsPrefixTable != null)
				{
					this.m_NsPrefixTable.Clear();
				}
				this.CurrentDataOperation = DataOperationTypes.NoData;
				this.IsExtendedOpcode = false;
				this.SectionHeaderFound = false;
				this.AttributeList.Clear();
				this.ProcessingTokenSet = false;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000482C File Offset: 0x00002A2C
		internal void SetObxmlDecodeState(ObxmlDecoder decoder, ObxmlStream stream, ObxmlDecodeResponse lastDecodeResult)
		{
			this.ClearStateObject();
			this.Parent = decoder;
			this.m_BinXmlStream = stream;
			if (lastDecodeResult != null)
			{
				this.LastDecodeResult = lastDecodeResult;
			}
			this.Init();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004854 File Offset: 0x00002A54
		internal bool IsDecoderStateValid()
		{
			return (this.ErrorType == ObxmlErrorTypes.Success || ObxmlErrorTypes.Done == this.ErrorType) && this.m_RequestObject != null && this.m_RequestObject.IsRequestValid();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000487C File Offset: 0x00002A7C
		internal bool IsInitialized
		{
			get
			{
				return this.m_bInit;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004884 File Offset: 0x00002A84
		internal bool IsNotSchemaBased
		{
			get
			{
				return (this.SectionHeader.Flags & 2) > 0;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004898 File Offset: 0x00002A98
		internal bool PerformFullXmlParse
		{
			get
			{
				return this.m_DTDInfo != null && this.m_DTDInfo.IsValid;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000048B0 File Offset: 0x00002AB0
		internal bool UseXmlWriterForDTDAndData
		{
			get
			{
				return this.m_bUseXmlWriterForDTDAndData;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000048B8 File Offset: 0x00002AB8
		internal bool TryAppendingBeginTagClosure(ObxmlNodeState ns, bool checkForInitialNode, bool checkForNewNode)
		{
			if (!this.HasBeginTagClosurePending)
			{
				return false;
			}
			bool flag = this.HasBeginTagClosurePending;
			if (checkForInitialNode)
			{
				flag = (flag && this.m_NodeStates.Count > 0);
			}
			if (checkForNewNode && ns != null)
			{
				flag = (flag && this.LastNodeId != ns.NodeId);
			}
			if (flag)
			{
				this.m_CurrentInstruction.AppendElementStartTagClosing(this, this.AttributeList.Count > 0);
			}
			return flag;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000492C File Offset: 0x00002B2C
		internal ObxmlTokenMap TokenMap
		{
			get
			{
				return this.Parent.TokenMap;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000493C File Offset: 0x00002B3C
		internal bool Init()
		{
			if (!this.m_bInit)
			{
				this.PushNodeState(new ObxmlNodeState());
				this.m_bInit = true;
			}
			return this.m_bInit;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004960 File Offset: 0x00002B60
		internal ObxmlNodeState GetLastNodeState()
		{
			if (this.m_NodeStates.Count != 0)
			{
				return this.m_NodeStates.Peek();
			}
			return null;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000497C File Offset: 0x00002B7C
		internal ObxmlNodeState GetCurrentNodeState(bool clone = false)
		{
			if (clone)
			{
				return this.m_CurrentNode.Clone();
			}
			return this.m_CurrentNode;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004994 File Offset: 0x00002B94
		internal void SetLastInstructionToCurrent(bool resetCurrent, bool copyCsxPartialBuffer = false)
		{
			this.m_LastInstruction.ResetTo(this.m_CurrentInstruction, copyCsxPartialBuffer);
			if (resetCurrent)
			{
				this.m_CurrentInstruction.ClearStateObject();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000049B8 File Offset: 0x00002BB8
		internal void SwapLastInstructionToCurrent(bool resetCurrent)
		{
			this.m_LastInstruction = this.m_CurrentInstruction;
			if (resetCurrent)
			{
				this.m_CurrentInstruction = new ObxmlInstructionState();
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000049D4 File Offset: 0x00002BD4
		internal bool HasDecodeState(DecodeStates stateFlag)
		{
			return (stateFlag & this.DecodeStateMask) == stateFlag;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000049E4 File Offset: 0x00002BE4
		internal bool HasMoreElementData()
		{
			return this.HasDecodeState(DecodeStates.ElementStart | DecodeStates.ElementDataStart);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000049F8 File Offset: 0x00002BF8
		internal bool HasElementEndPending()
		{
			return !this.HasDecodeState(DecodeStates.ElementStart) && this.HasDecodeState(DecodeStates.ElementEndPending);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004A14 File Offset: 0x00002C14
		internal bool HasMoreCsxInstructionData(out long csxDataSize)
		{
			return this.m_CurrentInstruction.HasMoreCsxInstructionData(out csxDataSize);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004A24 File Offset: 0x00002C24
		internal bool HasMoreTokenOrData(out long tokenDataSize)
		{
			return this.m_CurrentInstruction.HasMoreTokenOrData(out tokenDataSize);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004A34 File Offset: 0x00002C34
		internal void SetDecodeState(DecodeStates stateFlag, bool resetOldMasks = true)
		{
			if (resetOldMasks)
			{
				this.DecodeStateMask = stateFlag;
				return;
			}
			this.DecodeStateMask |= stateFlag;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004A50 File Offset: 0x00002C50
		internal void SetDecodeState(DecodeStates stateFlag, DecodeStates resetStateFlag)
		{
			this.DecodeStateMask |= stateFlag;
			this.ResetDecodeStateMask(resetStateFlag);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004A68 File Offset: 0x00002C68
		internal void ResetDecodeStateMask(DecodeStates stateFlag)
		{
			this.DecodeStateMask &= ~stateFlag;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004A7C File Offset: 0x00002C7C
		internal void ResetDecodeStateMaskToNone()
		{
			this.DecodeStateMask = DecodeStates.None;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004A88 File Offset: 0x00002C88
		internal void PushNodeState(ObxmlNodeState ns)
		{
			this.m_CurrentNode = ns;
			ns.m_ParentNode = this.GetLastNodeState();
			if (ns.m_ParentNode != null)
			{
				ns.m_ParentNode.ChildNodesCount++;
				ns.NodeLevel = ns.m_ParentNode.NodeLevel + 1;
			}
			ns.NsIndex = (ulong)((long)this.m_NsStack.Count<PrefixInfo>());
			this.LastNodeId++;
			this.m_NodeStates.Push(ns);
			this.m_PreviousSibling = null;
			if (ConfigBaseClass.m_XMLTypeOpcodeDump && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
				{
					"(BinXMLOpcodeDump) Node Pushed"
				});
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004B34 File Offset: 0x00002D34
		internal ObxmlNodeState PopNodeState()
		{
			if (this.m_NodeStates.Count <= 0)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.DecodeStateInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			this.m_PreviousSibling = (this.m_CurrentNode = this.m_NodeStates.Pop());
			this.PopNamespace((int)this.m_CurrentNode.NsIndex);
			if (ConfigBaseClass.m_XMLTypeOpcodeDump && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
				{
					"(BinXMLOpcodeDump) Node Popped"
				});
			}
			return this.m_CurrentNode;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004BC8 File Offset: 0x00002DC8
		internal void PushNamespace(PrefixInfo pInfo)
		{
			this.m_NsStack.Add(pInfo);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004BD8 File Offset: 0x00002DD8
		internal void PopNamespace(int index)
		{
			for (int i = this.m_NsStack.Count<PrefixInfo>() - 1; i >= index; i--)
			{
				this.m_NsStack.RemoveAt(i);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004C0C File Offset: 0x00002E0C
		internal string GetNamespace(ulong nsid, TokenTypes tokenType = TokenTypes.NamespaceToken)
		{
			return this.TokenMap.GetTokenName(this.Parent.DecodeContext, nsid, tokenType);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004C28 File Offset: 0x00002E28
		internal ObxmlToken GetNamespaceToken(ulong nsid)
		{
			return this.TokenMap.GetNamespaceToken(this.Parent.DecodeContext, nsid, TokenTypes.NamespaceToken);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004C44 File Offset: 0x00002E44
		internal string GetNamespaceUri(ulong nsid)
		{
			ObxmlToken obxmlToken = null;
			return this.TokenMap.GetNamespaceUri(this.Parent.DecodeContext, nsid, out obxmlToken);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004C6C File Offset: 0x00002E6C
		internal string GetNamespaceUriForNode(ObxmlNodeState node)
		{
			ObxmlToken obxmlToken = null;
			return this.TokenMap.GetNamespaceUri(this.Parent.DecodeContext, node.m_ElementToken.NamespaceId, out obxmlToken);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004CA0 File Offset: 0x00002EA0
		internal PrefixInfo GetPrefixInfo(ulong pfxid)
		{
			PrefixInfo prefixInfo = null;
			if (this.m_PrefixTable.ContainsKey((int)pfxid))
			{
				prefixInfo = this.m_PrefixTable[(int)pfxid];
			}
			if (prefixInfo == null)
			{
				prefixInfo = this.Parent.GetReservedPrefixInfo((short)pfxid);
			}
			if (prefixInfo == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.TokenInvalidPrefix, null, ObxmlOpcode.OpcodeIds.None));
			}
			return prefixInfo;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004D00 File Offset: 0x00002F00
		internal string GetPrefix(short pfxid)
		{
			PrefixInfo prefixInfo = null;
			if (this.m_PrefixTable.ContainsKey((int)pfxid))
			{
				prefixInfo = this.m_PrefixTable[(int)pfxid];
			}
			if (prefixInfo == null && pfxid >= 1 && pfxid <= 6)
			{
				return this.Parent.GetReservedPrefixInfo(pfxid).Prefix;
			}
			if (prefixInfo == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.TokenInvalidPrefix, null, ObxmlOpcode.OpcodeIds.None));
			}
			return prefixInfo.Prefix;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004D70 File Offset: 0x00002F70
		internal PrefixInfo SetPrefix(short pfxid, string prefix, ulong nsid)
		{
			PrefixInfo prefixInfo = null;
			if (!this.m_PrefixTable.ContainsKey((int)pfxid))
			{
				prefixInfo = new PrefixInfo(this.Parent.DecodeContext, pfxid, prefix, nsid, null);
				this.m_PrefixTable[(int)prefixInfo.PrefixId] = prefixInfo;
			}
			else
			{
				this.m_PrefixTable.TryGetValue((int)pfxid, out prefixInfo);
			}
			if (!this.m_NsPrefixTable.ContainsKey((int)nsid))
			{
				this.m_NsPrefixTable[(int)nsid] = prefixInfo;
			}
			return prefixInfo;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004DE4 File Offset: 0x00002FE4
		internal PrefixInfo SetPrefix(PrefixInfo prefixInfo)
		{
			PrefixInfo prefixInfo2 = null;
			if (!this.m_PrefixTable.ContainsKey((int)prefixInfo.PrefixId))
			{
				Dictionary<int, PrefixInfo> prefixTable = this.m_PrefixTable;
				int prefixId = (int)prefixInfo.PrefixId;
				prefixInfo2 = prefixInfo;
				prefixTable[prefixId] = prefixInfo;
			}
			else
			{
				this.m_PrefixTable.TryGetValue((int)prefixInfo.PrefixId, out prefixInfo2);
			}
			if (!this.m_NsPrefixTable.ContainsKey((int)prefixInfo.Nsid))
			{
				this.m_NsPrefixTable[(int)prefixInfo.Nsid] = prefixInfo2;
			}
			return prefixInfo2;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004E58 File Offset: 0x00003058
		internal ObxmlToken GetToken(ulong tokenId, TokenTypes tokenType)
		{
			if (tokenType == TokenTypes.None)
			{
				return this.Parent.TokenMap.GetToken(this.Parent.DecodeContext, tokenId, true);
			}
			return this.Parent.TokenMap.GetToken(this.Parent.DecodeContext, tokenId, tokenType, true);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004EA8 File Offset: 0x000030A8
		internal void SetToken(ObxmlToken token)
		{
			this.Parent.TokenMap.SetToken(token);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004EBC File Offset: 0x000030BC
		internal string GetNSPrefix(string uri)
		{
			for (int i = 0; i < this.m_NsStack.Count; i++)
			{
				PrefixInfo prefixInfo = this.m_NsStack[i];
				if (prefixInfo.Uri == uri)
				{
					return prefixInfo.Prefix;
				}
			}
			foreach (int key in this.m_PrefixTable.Keys)
			{
				PrefixInfo prefixInfo = this.m_PrefixTable[key];
				if (prefixInfo.Uri == uri)
				{
					return prefixInfo.Prefix;
				}
			}
			foreach (short key2 in ObxmlDecoder.ReservedNSTable.Keys)
			{
				string a = ObxmlDecoder.ReservedNSTable[key2];
				if (a == uri)
				{
					return ObxmlDecoder.ReservedNSTable[key2];
				}
			}
			return null;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004FDC File Offset: 0x000031DC
		internal string GetNSPrefix(ulong nsid)
		{
			PrefixInfo prefixInfo = null;
			for (int i = 0; i < this.m_NsStack.Count; i++)
			{
				prefixInfo = this.m_NsStack[i];
				if (prefixInfo.Nsid == nsid)
				{
					return prefixInfo.Prefix;
				}
			}
			if (this.m_NsPrefixTable.TryGetValue((int)nsid, out prefixInfo) && prefixInfo != null)
			{
				return prefixInfo.Prefix;
			}
			string result = null;
			if (ObxmlDecoder.ReservedNSPrefixTable.ContainsKey((short)nsid))
			{
				result = ObxmlDecoder.ReservedNSPrefixTable[(short)nsid];
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005058 File Offset: 0x00003258
		internal long WriteTokenOrData(bool initTokenOrDataLength)
		{
			initTokenOrDataLength = true;
			ObxmlOutputObject requestOutput = this.m_RequestObject.m_RequestOutput;
			long num = 0L;
			if (this.m_CurrentInstruction.m_TextOrTokenData != null && this.m_CurrentInstruction.m_TextOrTokenData.Length > 0)
			{
				num = requestOutput.WriteTextOutput(this.m_CurrentInstruction.m_TextOrTokenData, (int)this.m_CurrentInstruction.m_TokenOrDataOffset);
				if (initTokenOrDataLength)
				{
					this.m_CurrentInstruction.m_TokenOrDataLength = (long)this.m_CurrentInstruction.m_TextOrTokenData.Length;
				}
				this.m_CurrentInstruction.ResetTextOrTokenDataLength(num);
			}
			return num;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000050E4 File Offset: 0x000032E4
		internal int WriteCsxBytes(bool initCsxDataLength)
		{
			initCsxDataLength = true;
			ObxmlOutputObject requestOutput = this.m_RequestObject.m_RequestOutput;
			long num = 0L;
			if (!string.IsNullOrEmpty(this.m_CurrentInstruction.m_CsxDataText))
			{
				if (initCsxDataLength)
				{
					this.m_CurrentInstruction.m_CsxDataLength = (long)(this.m_CurrentInstruction.m_CsxDataText.Length * 2);
				}
				num = requestOutput.WriteTextOutput(this.m_CurrentInstruction.m_CsxDataText, (int)this.m_CurrentInstruction.m_CsxDataOffset) * 2L;
				this.m_CurrentInstruction.ResetCsxDataLength((int)num);
			}
			return (int)num;
		}

		// Token: 0x040000B2 RID: 178
		private bool m_bInit;

		// Token: 0x040000B3 RID: 179
		private bool m_bUseXmlWriterForDTDAndData;

		// Token: 0x040000B4 RID: 180
		private Stack<ObxmlNodeState> m_NodeStates;

		// Token: 0x040000B5 RID: 181
		private ObxmlNodeState m_CurrentNode;

		// Token: 0x040000B6 RID: 182
		private ObxmlNodeState m_PreviousSibling;

		// Token: 0x040000B7 RID: 183
		private List<PrefixInfo> m_NsStack;

		// Token: 0x040000B8 RID: 184
		internal ObxmlDTDInfo m_DTDInfo;

		// Token: 0x040000B9 RID: 185
		internal ObxmlInstructionState m_CurrentInstruction;

		// Token: 0x040000BA RID: 186
		internal ObxmlInstructionState m_LastInstruction;

		// Token: 0x040000BB RID: 187
		internal ObxmlDecodeRequest m_RequestObject;

		// Token: 0x040000BC RID: 188
		internal ObxmlStream m_BinXmlStream;
	}
}
