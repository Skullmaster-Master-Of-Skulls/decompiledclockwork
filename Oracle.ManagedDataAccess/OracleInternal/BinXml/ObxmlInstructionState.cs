using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x0200001C RID: 28
	internal class ObxmlInstructionState : ObxmlStateObject
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000A458 File Offset: 0x00008658
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000A460 File Offset: 0x00008660
		internal InstructionTypes InstructionType { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000A46C File Offset: 0x0000866C
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000A474 File Offset: 0x00008674
		internal byte DataType { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000A480 File Offset: 0x00008680
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000A488 File Offset: 0x00008688
		internal ulong TokenId { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000A494 File Offset: 0x00008694
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000A49C File Offset: 0x0000869C
		internal ulong CsxCharArrayLength { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000A4A8 File Offset: 0x000086A8
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000A4B0 File Offset: 0x000086B0
		internal char[] CsxCharArray { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000A4BC File Offset: 0x000086BC
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000A4C4 File Offset: 0x000086C4
		internal long CsxCharArrayOffset { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000A4D0 File Offset: 0x000086D0
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000A4D8 File Offset: 0x000086D8
		internal long CsxStreamOffset { get; set; }

		// Token: 0x0600019B RID: 411 RVA: 0x0000A4E4 File Offset: 0x000086E4
		internal void InitObxmlInstructionStateObject(long csxOffset, long csxDataLength)
		{
			this.CsxStreamOffset = csxOffset;
			this.m_CsxDataLength = csxDataLength;
			this.m_CsxDataText = null;
			this.m_CsxDataOffset = 0L;
			this.m_TokenOrDataOffset = 0L;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000A50C File Offset: 0x0000870C
		static ObxmlInstructionState()
		{
			ObxmlInstructionState.FillBlankSpaces();
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlTagOpeningBracket] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlTagOpeningBracket));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlTagOpeningBracketWithSlash] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlTagOpeningBracketWithSlash));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlTagClosingBracket] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlTagClosingBracket));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlTagClosingBracketWithSpace] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlTagClosingBracketWithSpace));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlTagClosingBracketWithSlash] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlTagClosingBracketWithSlash));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlTagCommentOpen] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlTagCommentOpen));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlTagCommentClose] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlTagCommentClose));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlWhitespaceNewLine] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlWhitespaceNewLine));
			ObxmlInstructionState.sUTF8XmlTags[ObxmlInstructionState.sXmlWhitespaceTab] = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, Encoding.Unicode.GetBytes(ObxmlInstructionState.sXmlWhitespaceTab));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000A7CC File Offset: 0x000089CC
		internal string GetWhiteSpaces(ObxmlNodeState ns, bool appendEndOfLine = false, bool prefixEndOfLine = false)
		{
			string text = string.Empty;
			if (ns == null)
			{
				return text;
			}
			int num = ns.NodeLevel - 1;
			if (num >= ObxmlInstructionState.sXmlWhiteSpaceBlanks.Count)
			{
				num = ObxmlInstructionState.sXmlWhiteSpaceBlanks.Count - 1;
				text = ObxmlInstructionState.sXmlWhiteSpaceBlanks[num];
			}
			else if (num >= 0)
			{
				text = ObxmlInstructionState.sXmlWhiteSpaceBlanks[num];
			}
			if (appendEndOfLine)
			{
				if (prefixEndOfLine)
				{
					return ObxmlInstructionState.sXmlWhitespaceNewLine + text + ObxmlInstructionState.sXmlWhitespaceNewLine;
				}
				return text + ObxmlInstructionState.sXmlWhitespaceNewLine;
			}
			else
			{
				if (prefixEndOfLine)
				{
					return ObxmlInstructionState.sXmlWhitespaceNewLine + text;
				}
				return text;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000A858 File Offset: 0x00008A58
		internal void AppendElementStartTagClosing(ObxmlDecodeState decodeState, bool hasAttributes)
		{
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagClosingBracket);
			decodeState.HasBeginTagClosurePending = false;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A874 File Offset: 0x00008A74
		internal void AppendEmptyElementToken(ObxmlDecodeState decodeState, ObxmlNodeState ns)
		{
			string qualifiedName = ns.GetQualifiedName(decodeState);
			if (!ns.PendingDataNode && !decodeState.m_DTDInfo.IsValid)
			{
				this.m_TextOrTokenData.Append(this.GetWhiteSpaces(ns, false, true) + ObxmlInstructionState.sXmlTagOpeningBracket);
			}
			else
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagOpeningBracket);
			}
			this.m_TextOrTokenData.Append(qualifiedName);
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagClosingBracketWithSlash);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		internal void AppendWhiteSpaces(ObxmlDecodeState decodeState, ObxmlNodeState ns, bool appendEndOfLine = false, bool prefixEndOfLine = false)
		{
			this.m_TextOrTokenData.Append(this.GetWhiteSpaces(ns, appendEndOfLine, prefixEndOfLine));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A908 File Offset: 0x00008B08
		internal void AppendElementStartToken(ObxmlDecodeState decodeState, ObxmlNodeState ns, bool noClosingBracket = true)
		{
			string qualifiedName = ns.GetQualifiedName(decodeState);
			if (ns.m_ParentNode != null && ns.m_ParentNode.m_ElementToken != null && !decodeState.m_DTDInfo.IsValid)
			{
				this.m_TextOrTokenData.Append((ns.m_PrefixWhiteSpaces = this.GetWhiteSpaces(ns, false, true)) + ObxmlInstructionState.sXmlTagOpeningBracket);
			}
			else
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagOpeningBracket);
				ns.m_PrefixWhiteSpaces = this.GetWhiteSpaces(ns, false, true);
			}
			this.m_TextOrTokenData.Append(qualifiedName);
			if (!noClosingBracket)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagClosingBracket);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A9AC File Offset: 0x00008BAC
		internal void AppendElementEndToken(ObxmlDecodeState decodeState, ObxmlNodeState ns, bool notEmptyElement = true)
		{
			string qualifiedName = ns.GetQualifiedName(decodeState);
			if (notEmptyElement || ns.BeginTagClosed)
			{
				if (decodeState.HasBeginTagClosurePending && decodeState.LastNodeId == ns.NodeId)
				{
					this.AppendElementStartTagClosing(decodeState, decodeState.AttributeList.Count > 0);
				}
				if (!ns.PendingDataNode && !decodeState.m_DTDInfo.IsValid)
				{
					this.m_TextOrTokenData.Append(ns.m_PrefixWhiteSpaces + ObxmlInstructionState.sXmlTagOpeningBracketWithSlash);
				}
				else
				{
					this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagOpeningBracketWithSlash);
				}
				this.m_TextOrTokenData.Append(qualifiedName);
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagClosingBracket);
			}
			else
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagClosingBracketWithSlash);
			}
			decodeState.HasBeginTagClosurePending = false;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000AA78 File Offset: 0x00008C78
		internal void AppendAttributeToken(ObxmlDecodeState decodeState, AttributeInfo attribute)
		{
			this.m_TextOrTokenData.Append(attribute.GetQualifiedAttributeString(decodeState));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000AA90 File Offset: 0x00008C90
		internal bool AppendComment(string value, bool prefixNewLine = false)
		{
			string value2 = prefixNewLine ? (ObxmlInstructionState.sXmlWhitespaceNewLine + ObxmlInstructionState.sXmlTagCommentOpen) : ObxmlInstructionState.sXmlTagCommentOpen;
			this.m_TextOrTokenData.Append(value2);
			this.m_TextOrTokenData.Append(value);
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlTagCommentClose);
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceNewLine);
			return true;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		internal void WriteCDATABlock(ObxmlDecodeState decodeState, string cdataValue)
		{
			if (this.m_TextOrTokenData.Length == 0)
			{
				decodeState.m_RequestObject.m_RequestOutput.XmlTextStream.AppendCData(cdataValue);
				return;
			}
			this.StartCDATA();
			this.m_TextOrTokenData.Append(cdataValue);
			this.EndCDATA();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000AB40 File Offset: 0x00008D40
		internal bool StartCDATA()
		{
			bool result;
			try
			{
				this.AppendString(ObxmlInstructionState.sXmlCdataStart, false, false);
				result = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return result;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000AB80 File Offset: 0x00008D80
		internal bool EndCDATA()
		{
			bool result;
			try
			{
				this.AppendString(ObxmlInstructionState.sXmlCdataEnd, false, false);
				result = true;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		internal bool AppendString(string value, bool appendNewLine = false, bool appendSpace = false)
		{
			this.m_TextOrTokenData.Append(value);
			if (appendSpace)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceBlank);
			}
			if (appendNewLine)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceNewLine);
			}
			return true;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		internal bool AppendStringWithSpaces(string value, string appendSpaces = null, string prefixSpaces = null)
		{
			if (!string.IsNullOrEmpty(prefixSpaces))
			{
				this.m_TextOrTokenData.Append(prefixSpaces);
			}
			this.m_TextOrTokenData.Append(value);
			if (!string.IsNullOrEmpty(appendSpaces))
			{
				this.m_TextOrTokenData.Append(appendSpaces);
			}
			return true;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000AC34 File Offset: 0x00008E34
		internal bool AppendStringWithSpaces(string value, bool appendSpace = false, bool prefixSpace = false)
		{
			if (prefixSpace)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceBlank);
			}
			this.m_TextOrTokenData.Append(value);
			if (appendSpace)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceBlank);
			}
			return true;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000AC6C File Offset: 0x00008E6C
		internal bool AppendQuoted(string value, bool appendSpace = false, bool prefixSpace = false)
		{
			if (prefixSpace)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceBlank);
			}
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlQuote);
			this.m_TextOrTokenData.Append(value);
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlQuote);
			if (appendSpace)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceBlank);
			}
			return true;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		internal bool AppendEntity(string name, bool appendSpace = false, bool prefixSpace = false)
		{
			if (prefixSpace)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceBlank);
			}
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlAmp);
			this.m_TextOrTokenData.Append(name);
			this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlSemi);
			if (appendSpace)
			{
				this.m_TextOrTokenData.Append(ObxmlInstructionState.sXmlWhitespaceBlank);
			}
			return true;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000AD3C File Offset: 0x00008F3C
		private static void FillBlankSpaces()
		{
			ObxmlInstructionState.sXmlWhiteSpaceBlanks[0] = ObxmlInstructionState.sXmlWhitespaceBlank;
			for (int i = 1; i < 32; i++)
			{
				ObxmlInstructionState.sXmlWhiteSpaceBlanks[i] = ObxmlInstructionState.sXmlWhiteSpaceBlanks[i - 1] + ObxmlInstructionState.sXmlWhitespaceBlank;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000AD88 File Offset: 0x00008F88
		internal void ClearStateDataObject()
		{
			if (this.m_TextOrTokenData != null)
			{
				this.m_TextOrTokenData.Clear();
			}
			this.m_CsxDataLength = 0L;
			this.m_CsxDataText = null;
			this.m_CsxDataOffset = 0L;
			this.m_TokenOrDataOffset = 0L;
			this.m_TokenOrDataLength = 0L;
			this.CsxCharArrayLength = 0UL;
			if (this.CsxCharArray != null)
			{
				Array.Clear(this.CsxCharArray, 0, this.CsxCharArray.Length);
			}
			this.CsxCharArrayOffset = 0L;
			this.CsxStreamOffset = 0L;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000AE04 File Offset: 0x00009004
		internal string GetRemainingTextOrTokenData(int lengthRequested = -1)
		{
			if (lengthRequested == -1 || this.m_TokenOrDataOffset == 0L)
			{
				return this.m_TextOrTokenData.ToString();
			}
			int num = (int)this.m_TokenOrDataLength - (int)this.m_TokenOrDataOffset;
			if (lengthRequested > num)
			{
				lengthRequested = num;
			}
			return this.m_TextOrTokenData.ToString(lengthRequested, (int)this.m_TokenOrDataOffset);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000AE58 File Offset: 0x00009058
		internal bool ResetTokenDataConsumed()
		{
			if (this.m_TokenOrDataLength <= 0L)
			{
				this.m_TextOrTokenData.Clear();
				this.m_TokenOrDataOffset = 0L;
				return true;
			}
			return false;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000AE7C File Offset: 0x0000907C
		internal int ResetTextOrTokenDataLength(long consumedLength)
		{
			this.m_TokenOrDataLength -= consumedLength;
			if (!this.ResetTokenDataConsumed())
			{
				this.m_TokenOrDataOffset += consumedLength;
			}
			return (int)this.m_TokenOrDataLength;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000AEAC File Offset: 0x000090AC
		internal int ResetCsxDataLength(int consumedLength)
		{
			this.m_CsxDataLength -= (long)consumedLength;
			if (this.m_CsxDataLength == 0L)
			{
				this.m_CsxDataText = null;
				this.m_CsxDataOffset = 0L;
			}
			else
			{
				this.m_CsxDataOffset += (long)consumedLength;
			}
			return (int)this.m_CsxDataLength;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000AEFC File Offset: 0x000090FC
		internal bool HasMoreCsxInstructionData(out long csxDataSize)
		{
			return (csxDataSize = this.m_CsxDataLength) > 0L;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000AF18 File Offset: 0x00009118
		internal bool HasMoreTokenOrData(out long tokenDataSize)
		{
			return (tokenDataSize = this.m_TokenOrDataLength) > 0L;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000AF34 File Offset: 0x00009134
		internal ObxmlInstructionState()
		{
			this.ClearStateObject();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000AF44 File Offset: 0x00009144
		internal override void ClearStateObject()
		{
			this.InstructionType = InstructionTypes.None;
			this.m_Opcode = -1;
			this.TokenId = 0UL;
			this.DataType = 0;
			this.ClearStateDataObject();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000AF6C File Offset: 0x0000916C
		internal void ResetTo(ObxmlInstructionState newState, bool copyCsxPartialBuffer)
		{
			this.InstructionType = newState.InstructionType;
			this.m_Opcode = newState.m_Opcode;
			this.TokenId = newState.TokenId;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000AF94 File Offset: 0x00009194
		internal void SetInstructionState(long countCharConsumed, ObxmlOpcode.OpcodeIds opcode, long csxOffset, ObxmlToken token = null, InstructionTypes instructionType = InstructionTypes.None)
		{
			this.TokenId = 0UL;
			if (token != null)
			{
				this.InstructionType = InstructionTypes.Token;
				this.TokenId = token.TokenId;
			}
			else
			{
				this.InstructionType = instructionType;
			}
			this.m_Opcode = (short)opcode;
			this.CsxStreamOffset = csxOffset;
			this.m_CsxDataLength = countCharConsumed;
		}

		// Token: 0x0400010B RID: 267
		internal short m_Opcode;

		// Token: 0x0400010C RID: 268
		internal static readonly string sXmlTagOpeningBracket = "<";

		// Token: 0x0400010D RID: 269
		internal static readonly string sXmlTagOpeningBracketWithSlash = "</";

		// Token: 0x0400010E RID: 270
		internal static readonly string sXmlTagClosingBracket = ">";

		// Token: 0x0400010F RID: 271
		internal static readonly string sXmlTagClosingBracketWithSpace = " >";

		// Token: 0x04000110 RID: 272
		internal static readonly string sXmlTagClosingBracketWithSlash = "/>";

		// Token: 0x04000111 RID: 273
		internal static readonly string sXmlTagCommentOpen = "<!--";

		// Token: 0x04000112 RID: 274
		internal static readonly string sXmlTagCommentClose = "-->";

		// Token: 0x04000113 RID: 275
		internal static readonly string sXmlTagSquareOpen = "[";

		// Token: 0x04000114 RID: 276
		internal static readonly string sXmlTagSquareClose = "]";

		// Token: 0x04000115 RID: 277
		internal static readonly string sXmlTagAttributeStart = "<!ATTLIST";

		// Token: 0x04000116 RID: 278
		internal static readonly string sXmlWhitespaceNewLine = "\n";

		// Token: 0x04000117 RID: 279
		internal static readonly string sXmlWhitespaceBlank = " ";

		// Token: 0x04000118 RID: 280
		internal static readonly string sXmlWhitespaceTab = "\t";

		// Token: 0x04000119 RID: 281
		internal static readonly string sXmlQuote = "\"";

		// Token: 0x0400011A RID: 282
		internal static readonly string sXmlDocType = "<!DOCTYPE";

		// Token: 0x0400011B RID: 283
		internal static readonly string sXmlPublic = "PUBLIC";

		// Token: 0x0400011C RID: 284
		internal static readonly string sXmlSystem = "SYSTEM";

		// Token: 0x0400011D RID: 285
		internal static readonly string sXmlElement = "<!ELEMENT";

		// Token: 0x0400011E RID: 286
		internal static readonly string sXmlEntity = "<!ENTITY";

		// Token: 0x0400011F RID: 287
		internal static readonly string sXmlNotation = "<!NOTATION";

		// Token: 0x04000120 RID: 288
		internal static readonly string sXmlNdata = "NDATA";

		// Token: 0x04000121 RID: 289
		internal static readonly string sXmlCdataStart = "<![CDATA[";

		// Token: 0x04000122 RID: 290
		internal static readonly string sXmlCdataEnd = "]]>";

		// Token: 0x04000123 RID: 291
		internal static readonly string sXmlAmp = "&";

		// Token: 0x04000124 RID: 292
		internal static readonly string sXmlPercentage = "% ";

		// Token: 0x04000125 RID: 293
		internal static readonly string sXmlSemi = ";";

		// Token: 0x04000126 RID: 294
		private static Dictionary<int, string> sXmlWhiteSpaceBlanks = new Dictionary<int, string>();

		// Token: 0x04000127 RID: 295
		private static Dictionary<string, byte[]> sUTF8XmlTags = new Dictionary<string, byte[]>();

		// Token: 0x04000128 RID: 296
		internal StringBuilder m_TextOrTokenData;

		// Token: 0x04000129 RID: 297
		internal long m_TokenOrDataLength;

		// Token: 0x0400012A RID: 298
		internal long m_TokenOrDataOffset;

		// Token: 0x0400012B RID: 299
		internal long m_CsxDataLength;

		// Token: 0x0400012C RID: 300
		internal string m_CsxDataText;

		// Token: 0x0400012D RID: 301
		internal long m_CsxDataOffset;
	}
}
