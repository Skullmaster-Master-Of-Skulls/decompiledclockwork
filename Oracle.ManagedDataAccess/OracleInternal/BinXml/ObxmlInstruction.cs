using System;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x02000019 RID: 25
	internal class ObxmlInstruction
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005BBC File Offset: 0x00003DBC
		private ObxmlStream DataStream
		{
			get
			{
				if (this.m_DecodeState != null)
				{
					return this.m_DecodeState.m_BinXmlStream;
				}
				return null;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005BD4 File Offset: 0x00003DD4
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005BE8 File Offset: 0x00003DE8
		private long StreamPosition
		{
			get
			{
				return this.m_DecodeState.m_CurrentInstruction.CsxStreamOffset;
			}
			set
			{
				this.m_DecodeState.m_CurrentInstruction.CsxStreamOffset = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00005BFC File Offset: 0x00003DFC
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00005C04 File Offset: 0x00003E04
		internal bool InstructionPending { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005C10 File Offset: 0x00003E10
		internal ObxmlSectionHeader SectionHeader
		{
			get
			{
				if (this.m_DecodeState != null)
				{
					return this.m_DecodeState.SectionHeader;
				}
				return null;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005C28 File Offset: 0x00003E28
		internal ObxmlDocHeader DocHeader
		{
			get
			{
				if (this.m_DecodeState != null)
				{
					return this.m_DecodeState.DocHeader;
				}
				return null;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005C40 File Offset: 0x00003E40
		internal ObxmlInstruction(ObxmlDecodeState state)
		{
			this.ResetObxmlInstruction(state);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005C60 File Offset: 0x00003E60
		internal void ResetObxmlInstruction(ObxmlDecodeState state)
		{
			if (state != null)
			{
				this.m_DecodeState = state;
				if (this.m_DecodeState.m_BinXmlStream == null)
				{
					throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidInputStream, null, ObxmlOpcode.OpcodeIds.None));
				}
			}
			this.InstructionPending = false;
			this.m_InstructionFormat = null;
			this.ResetData();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005CB8 File Offset: 0x00003EB8
		private void ResetLastInstructionToCurrent()
		{
			this.m_DecodeState.SetLastInstructionToCurrent(true, true);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005CC8 File Offset: 0x00003EC8
		internal ObxmlOutputObject RequestOutput
		{
			get
			{
				return this.m_DecodeState.m_RequestObject.m_RequestOutput;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005CDC File Offset: 0x00003EDC
		internal void ResetData()
		{
			for (int i = 0; i < ObxmlInstruction.MaxInstructionDataLen; i++)
			{
				this.m_InstructionData[i] = 0UL;
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005D04 File Offset: 0x00003F04
		internal ObxmlOpcode.OpcodeIds ReadOpcode()
		{
			this.m_DecodeState.m_LastInstruction.m_Opcode = this.m_DecodeState.m_CurrentInstruction.m_Opcode;
			this.m_DecodeState.m_CurrentInstruction.m_Opcode = this.DataStream.ReadShortIntFromByte();
			this.m_DecodeState.m_CurrentInstruction.CsxStreamOffset = this.DataStream.Position;
			return (ObxmlOpcode.OpcodeIds)this.m_DecodeState.m_CurrentInstruction.m_Opcode;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005D78 File Offset: 0x00003F78
		internal bool IsLastTokenAttribute()
		{
			ObxmlNodeState lastNodeState = this.GetLastNodeState();
			return lastNodeState.NodeType == NodeTypes.Attribute && lastNodeState.m_ElementToken != null;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005DA4 File Offset: 0x00003FA4
		internal ObxmlNodeState GetLastNodeState()
		{
			return this.m_DecodeState.GetLastNodeState();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005DB4 File Offset: 0x00003FB4
		internal bool ReadSectionHeader(ObxmlOpcode.OpcodeIds opcode)
		{
			if (opcode != ObxmlOpcode.OpcodeIds.STRTSEC)
			{
				return false;
			}
			this.SectionHeader.Version = (byte)this.DataStream.ReadShortIntFromByte();
			if (this.SectionHeader.Version != ObxmlOpcode.HDR_CSX_VERSION)
			{
				return false;
			}
			this.SectionHeader.Flags = (byte)this.DataStream.ReadShortIntFromByte();
			if ((this.SectionHeader.Flags & 8) > 0)
			{
				byte b = (byte)this.DataStream.ReadShortIntFromByte();
				this.SectionHeader.DocId = new byte[(int)b];
				this.DataStream.ReadAndCopyBytes(this.SectionHeader.DocId, ObxmlStream.sUseCurrentOffset, (long)((ulong)b));
			}
			if ((this.SectionHeader.Flags & 16) > 0)
			{
				byte b2 = (byte)this.DataStream.ReadShortIntFromByte();
				this.SectionHeader.PathId = new byte[(int)b2];
				this.DataStream.ReadAndCopyBytes(this.SectionHeader.PathId, ObxmlStream.sUseCurrentOffset, (long)((ulong)b2));
				byte b3 = (byte)this.DataStream.ReadShortIntFromByte();
				byte[] destination = new byte[(int)b3];
				this.DataStream.ReadAndCopyBytes(destination, ObxmlStream.sUseCurrentOffset, (long)((ulong)b3));
			}
			if ((this.SectionHeader.Flags & 4) > 0)
			{
				this.SectionHeader.Rguid = new byte[ObxmlInstructionFormat.HDR_RGUID_LEN];
				this.DataStream.ReadAndCopyBytes(this.SectionHeader.Rguid, ObxmlStream.sUseCurrentOffset, (long)ObxmlInstructionFormat.HDR_RGUID_LEN);
			}
			if ((this.SectionHeader.Flags & 64) > 0)
			{
				this.SectionHeader.BigEflt = true;
			}
			else
			{
				this.SectionHeader.BigEflt = false;
			}
			if (ConfigBaseClass.m_XMLTypeOpcodeDump && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
				{
					"(BinXMLOpcodeDump) Binary XML " + (this.m_DecodeState.IsNotSchemaBased ? "Is NOT Schema Based" : "Is Schema Based")
				});
			}
			return true;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005F88 File Offset: 0x00004188
		internal bool ReadDocHeader(ObxmlOpcode.OpcodeIds opcode)
		{
			if (opcode != ObxmlOpcode.OpcodeIds.DOC)
			{
				return false;
			}
			int num = (int)this.m_InstructionData[1];
			int count;
			if ((num & ObxmlInstructionFormat.PROLOG_SPECIFIED) > 0)
			{
				this.DocHeader.XmlDecl = true;
				if ((num & ObxmlInstructionFormat.VERSION_SPECIFIED) > 0)
				{
					this.DocHeader.Version = (((num & ObxmlInstructionFormat.VERSION_MASK) > 0) ? "1.1" : "1.0");
				}
				else
				{
					this.DocHeader.Version = "1.0";
				}
				if ((num & ObxmlInstructionFormat.STANDALONE_SPECIFIED) > 0)
				{
					this.DocHeader.Standalone = (((num & ObxmlInstructionFormat.STANDALONE_TRUE) > 0) ? "yes" : "no");
				}
				if ((num & ObxmlInstructionFormat.ENCODING_SPECIFIED) > 0)
				{
					ulong length = this.m_InstructionData[0];
					this.DocHeader.Encoding = this.DataStream.ReadUtf8String(length);
				}
			}
			else if ((count = (int)this.m_InstructionData[0]) > 0)
			{
				this.DataStream.ReadBytes(count);
			}
			return true;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00006074 File Offset: 0x00004274
		internal ObxmlOpcode.OpcodeIds ReadInstructionInfo(bool readNewOpcode = false)
		{
			ObxmlOpcode.OpcodeIds opcodeIds = (ObxmlOpcode.OpcodeIds)this.m_DecodeState.m_CurrentInstruction.m_Opcode;
			this.StreamPosition = this.DataStream.Position;
			if (readNewOpcode)
			{
				opcodeIds = this.ReadOpcode();
			}
			if (!this.m_DecodeState.SectionHeaderFound && opcodeIds != ObxmlOpcode.OpcodeIds.STRTSEC)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.RequestInputInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			if (opcodeIds == ObxmlOpcode.OpcodeIds.None || (opcodeIds != ObxmlOpcode.OpcodeIds.FORMATEXTENSION && (opcodeIds < ObxmlOpcode.OpcodeIds.DATSTR1 || opcodeIds >= ObxmlOpcode.OpcodeIds.OPCODE_NUMBER)))
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidInstruction, null, ObxmlOpcode.OpcodeIds.None));
			}
			if (opcodeIds == ObxmlOpcode.OpcodeIds.FORMATEXTENSION)
			{
				this.m_DecodeState.IsExtendedOpcode = true;
				try
				{
					opcodeIds = this.ReadOpcodeInfo(true, this.m_DecodeState.IsExtendedOpcode);
				}
				catch (Exception ex)
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
					throw;
				}
				if (opcodeIds >= ObxmlOpcode.OpcodeIds.DATSTR6)
				{
					throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidOpcode, null, ObxmlOpcode.OpcodeIds.None));
				}
			}
			else
			{
				this.m_DecodeState.IsExtendedOpcode = false;
				opcodeIds = this.ReadOpcodeInfo(false, this.m_DecodeState.IsExtendedOpcode);
			}
			Array.Clear(this.m_InstructionData, 0, ObxmlInstruction.MaxInstructionDataLen);
			for (int i = 0; i < this.m_InstructionFormat.opnum; i++)
			{
				int num = this.m_InstructionFormat.oplen[i];
				switch (num)
				{
				case 1:
					this.m_InstructionData[i] = (ulong)((long)this.DataStream.ReadShortIntFromByte());
					break;
				case 2:
					this.m_InstructionData[i] = (ulong)((long)this.DataStream.ReadShortInt());
					break;
				case 3:
					break;
				case 4:
					this.m_InstructionData[i] = (ulong)((long)this.DataStream.ReadInt4());
					break;
				default:
					if (num == 8)
					{
						this.m_InstructionData[i] = (ulong)this.DataStream.ReadInt8();
					}
					break;
				}
			}
			if (ConfigBaseClass.m_XMLTypeOpcodeDump && ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
				{
					string.Format("(BinXMLOpcodeDump) *****Current Iteration Opcode value is :{0}  {1}   ******", (int)opcodeIds, ObxmlInstructionFormat.InstructionFormats[(int)opcodeIds].name)
				});
				for (int j = 0; j < ObxmlInstruction.MaxInstructionDataLen; j++)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.BinXML, new string[]
					{
						"(BinXMLOpcodeDump)          " + this.m_InstructionData[j]
					});
				}
			}
			return opcodeIds;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000062D4 File Offset: 0x000044D4
		private ObxmlOpcode.OpcodeIds ReadOpcodeInfo(bool readNewOpcode = false, bool IsExtended = false)
		{
			if (readNewOpcode)
			{
				this.ReadOpcode();
			}
			this.m_InstructionFormat = (IsExtended ? ObxmlInstructionFormat.ExtendedInstructionFormats[(int)this.m_DecodeState.m_CurrentInstruction.m_Opcode] : ObxmlInstructionFormat.InstructionFormats[(int)this.m_DecodeState.m_CurrentInstruction.m_Opcode]);
			return (ObxmlOpcode.OpcodeIds)this.m_DecodeState.m_CurrentInstruction.m_Opcode;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006334 File Offset: 0x00004534
		private ulong GetDataLen(short opcode)
		{
			ulong result = 0UL;
			if (opcode >= 208 && opcode <= 212)
			{
				return result;
			}
			if (this.m_InstructionFormat.hasfixeddata)
			{
				result = (ulong)((long)this.m_InstructionFormat.fixeddatalen);
				this.m_DecodeState.m_CurrentInstruction.DataType = ObxmlOpcode.dataType[(int)opcode];
			}
			else if (this.m_InstructionFormat.hasvardata)
			{
				if ((this.m_InstructionFormat.flags & ObxmlInstructionFormat.DTDLEN) > 0)
				{
					result = this.m_InstructionData[0];
					this.m_DecodeState.m_CurrentInstruction.DataType = 1;
				}
				else if ((this.m_InstructionFormat.flags & ObxmlInstructionFormat.TYPDATA) == 0)
				{
					result = this.m_InstructionData[0];
					this.m_DecodeState.m_CurrentInstruction.DataType = 1;
				}
				else
				{
					int num = this.m_InstructionFormat.oplen[0];
					switch (num)
					{
					case 1:
					{
						ObxmlInstructionFormat obxmlInstructionFormat = ObxmlInstructionFormat.InstructionFormats[(int)this.m_InstructionData[0]];
						bool hasfixeddata = obxmlInstructionFormat.hasfixeddata;
						result = (ulong)((long)obxmlInstructionFormat.fixeddatalen);
						this.m_DecodeState.m_CurrentInstruction.DataType = ObxmlOpcode.dataType[(int)((short)this.m_InstructionData[0])];
						break;
					}
					case 2:
						result = (this.m_InstructionData[0] & 18446744073709502463UL);
						this.m_DecodeState.m_CurrentInstruction.DataType = (((this.m_InstructionData[0] & 49152UL) == (ulong)((long)ObxmlOpcode.DATL2STRMSK)) ? 1 : 2);
						break;
					case 3:
						break;
					case 4:
						result = (this.m_InstructionData[0] & 18446744070488326143UL);
						this.m_DecodeState.m_CurrentInstruction.DataType = (((this.m_InstructionData[0] & (ulong)-1073741824) == (ulong)((long)ObxmlOpcode.DATL2STRMSK)) ? 1 : 2);
						break;
					default:
						if (num == 8)
						{
							result = (this.m_InstructionData[0] & 4611686018427387903UL);
							this.m_DecodeState.m_CurrentInstruction.DataType = (((this.m_InstructionData[0] & 13835058055282163712UL) == (ulong)((long)ObxmlOpcode.DATL2STRMSK)) ? 1 : 2);
						}
						break;
					}
				}
			}
			else
			{
				if ((this.m_InstructionFormat.flags & ObxmlInstructionFormat.TOKENID) == 0)
				{
					result = (ulong)((long)this.m_InstructionFormat.oplen[0]);
					this.m_DecodeState.m_CurrentInstruction.DataType = 1;
					return result;
				}
				result = 0UL;
				this.m_DecodeState.m_CurrentInstruction.DataType = 1;
			}
			return result;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006588 File Offset: 0x00004788
		internal static bool OpcodeHasDataPart(ObxmlOpcode.OpcodeIds opcode)
		{
			return (ObxmlInstructionFormat.InstructionFormats[(int)opcode].flags & ObxmlInstructionFormat.TYPDATA) > 0;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000065A0 File Offset: 0x000047A0
		private void SkipBytes(long skipCount)
		{
			long position = skipCount + this.DataStream.Position;
			this.DataStream.Position = position;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000065C8 File Offset: 0x000047C8
		internal static bool IsCloseTagPending(ObxmlDecodeState decodeState)
		{
			bool flag;
			switch (decodeState.m_LastInstruction.m_Opcode)
			{
			case 188:
			case 189:
			case 190:
			case 191:
			case 192:
			case 193:
			case 194:
			case 195:
			case 196:
			case 197:
				flag = true;
				break;
			default:
				flag = false;
				break;
			}
			return flag && decodeState.m_LastInstruction.m_Opcode != decodeState.m_CurrentInstruction.m_Opcode;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006640 File Offset: 0x00004840
		internal byte ReadNum1()
		{
			byte result;
			try
			{
				result = (byte)this.DataStream.ReadShortIntFromByte();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006680 File Offset: 0x00004880
		internal short ReadNum2()
		{
			short result;
			try
			{
				result = this.DataStream.ReadShortInt();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000066BC File Offset: 0x000048BC
		internal int ReadNum4()
		{
			int result;
			try
			{
				result = this.DataStream.ReadInt4();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000066F8 File Offset: 0x000048F8
		internal int ReadNum4r()
		{
			byte[] array = null;
			try
			{
				array = this.DataStream.ReadBytes(4);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return (int)(array[3] & byte.MaxValue) << 24 | (int)(array[2] & byte.MaxValue) << 16 | (int)(array[1] & byte.MaxValue) << 8 | (int)(array[0] & byte.MaxValue);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006768 File Offset: 0x00004968
		internal long ReadLong8()
		{
			long result;
			try
			{
				result = this.DataStream.ReadInt8();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000067A4 File Offset: 0x000049A4
		internal ObxmlNodeState GetNodeData(ulong tokenId, NodeTypes nodeType, ulong datalen = 0UL)
		{
			ObxmlToken token = this.m_DecodeState.Parent.TokenMap.GetToken(this.m_DecodeState.Parent.DecodeContext, tokenId, true);
			if (nodeType == NodeTypes.None)
			{
				ObxmlNodeState.GetDefaultNodeTypeForTokenType(token.TokenType);
			}
			return new ObxmlNodeState(nodeType, token, datalen);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000067F4 File Offset: 0x000049F4
		internal long ReadAndWriteFixedData(ObxmlOpcode.OpcodeIds opcode, out long processed)
		{
			if (this.m_DecodeState.TryAppendingBeginTagClosure(null, false, false))
			{
				this.m_DecodeState.GetLastNodeState().ChildNodesCount++;
			}
			this.m_DecodeState.GetLastNodeState().PendingDataNode = true;
			ObxmlInstruction.ProcessTokenOrTextData(this.m_DecodeState, false, out processed);
			bool flag = this.HandleArrayModeDataBegin(opcode, out processed);
			this.m_DecodeState.m_CurrentInstruction.m_CsxDataLength = (long)this.GetDataLen((short)opcode);
			ulong num = (ulong)this.m_DecodeState.m_CurrentInstruction.m_CsxDataLength;
			if (num > 0UL)
			{
				if (num > (ulong)this.m_DecodeState.m_RequestObject.m_CountRemaining)
				{
					num = (ulong)this.m_DecodeState.m_RequestObject.m_CountRemaining;
				}
				string str = this.DataStream.ReadUtf8String(num);
				this.m_DecodeState.m_CurrentInstruction.m_CsxDataText = str.ReplaceXmlChars();
				this.m_DecodeState.m_CurrentInstruction.m_CsxDataLength = (long)(this.m_DecodeState.m_CurrentInstruction.m_CsxDataText.Length * 2);
				this.m_DecodeState.GetLastNodeState().PendingDataNode = true;
				if (!this.m_DecodeState.m_DTDInfo.IsProcessingDTD)
				{
					this.RequestOutput.InsideDataSection = true;
				}
				processed = (long)this.m_DecodeState.WriteCsxBytes(true);
				this.RequestOutput.InsideDataSection = false;
			}
			if (flag)
			{
				this.m_DecodeState.SetDecodeState(DecodeStates.ElementEndStart, true);
				this.ProcessEndElement(out processed);
			}
			return this.m_DecodeState.m_RequestObject.m_CountRemaining;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006968 File Offset: 0x00004B68
		private char[] DecodeWhitespaces(ObxmlOpcode.OpcodeIds opcode, out int datalen)
		{
			char[] array = new char[100];
			datalen = 0;
			while (opcode == ObxmlOpcode.OpcodeIds.SPACE1 || opcode == ObxmlOpcode.OpcodeIds.SPACE2)
			{
				this.InstructionPending = true;
				short num;
				short num2;
				if (opcode == ObxmlOpcode.OpcodeIds.SPACE1)
				{
					num = ((short)this.m_InstructionData[0] & ObxmlOpcode.SPACE1_IDMASK);
					num2 = ((short)this.m_InstructionData[0] & ObxmlOpcode.SPACE_FLAGMASK);
				}
				else if (opcode == ObxmlOpcode.OpcodeIds.SPACE2)
				{
					num = ((short)this.m_InstructionData[0] & ObxmlOpcode.SPACE2_IDMASK);
					num2 = (short)((short)this.m_InstructionData[0] >> 8 & (int)ObxmlOpcode.SPACE_FLAGMASK);
				}
				else
				{
					num = 0;
					num2 = 0;
				}
				int num3 = datalen + (int)num;
				if (array.Length < num3)
				{
					char[] destinationArray = array;
					array = new char[num3];
					Array.Copy(array, 0, destinationArray, 0, datalen);
				}
				short num4 = num2;
				char c;
				if (num4 <= 32)
				{
					if (num4 != 0)
					{
						if (num4 != 32)
						{
							goto IL_F0;
						}
						c = '\t';
					}
					else
					{
						c = ' ';
					}
				}
				else if (num4 != 64)
				{
					if (num4 != 96)
					{
						if (num4 != 128)
						{
							goto IL_F0;
						}
						array[datalen++] = '\n';
						c = ' ';
					}
					else
					{
						c = '\r';
					}
				}
				else
				{
					c = '\n';
				}
				IL_F4:
				for (int i = datalen; i < num3; i++)
				{
					if (c == '\n')
					{
						array[i++] = '\r';
						num3++;
					}
					array[i] = c;
				}
				datalen = num3;
				opcode = this.ReadInstructionInfo(true);
				continue;
				IL_F0:
				c = '\r';
				goto IL_F4;
			}
			return array;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006AB8 File Offset: 0x00004CB8
		private void SkipData(ObxmlOpcode.OpcodeIds opcode)
		{
			ulong dataLen = this.GetDataLen((short)opcode);
			this.SkipBytes((long)((int)dataLen));
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006AD8 File Offset: 0x00004CD8
		internal byte[] GetEventChars(ObxmlOpcode.OpcodeIds opcode, out ulong datalen, out string stringValue)
		{
			byte[] result = null;
			stringValue = null;
			datalen = this.GetDataLen((short)opcode);
			byte dataType = this.m_DecodeState.m_CurrentInstruction.DataType;
			if (dataType == 1)
			{
				stringValue = this.DataStream.ReadUtf8String(datalen);
			}
			else
			{
				stringValue = this.ReadEventText(null, opcode);
				datalen = (ulong)((long)stringValue.Length);
			}
			this.m_DecodeState.CurrentDataOperation = DataOperationTypes.NoData;
			return result;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006B3C File Offset: 0x00004D3C
		internal ObxmlNodeState ReadElementInfo(ObxmlOpcode.OpcodeIds Opcode, bool optimizedOpcode, ref ulong qnameid, out string prefix)
		{
			ObxmlNodeState obxmlNodeState = null;
			ulong datalen = this.GetDataLen((short)Opcode);
			bool flag = false;
			prefix = null;
			byte b;
			if (Opcode >= ObxmlOpcode.OpcodeIds.PRPSTK1F && Opcode <= ObxmlOpcode.OpcodeIds.PRPSTT8F)
			{
				b = (byte)this.m_InstructionData[1];
			}
			else if (Opcode >= ObxmlOpcode.OpcodeIds.PRPSTK1V && Opcode <= ObxmlOpcode.OpcodeIds.PRPSTT8V)
			{
				b = (byte)this.m_InstructionData[2];
				flag = true;
				datalen = 0UL;
			}
			else
			{
				b = 0;
			}
			if ((this.m_InstructionFormat.flags & ObxmlInstructionFormat.KIDNUM) <= 0)
			{
				qnameid = ((flag || optimizedOpcode) ? this.m_InstructionData[1] : this.m_InstructionData[0]);
				obxmlNodeState = this.GetNodeData(qnameid, NodeTypes.Element, datalen);
			}
			if (obxmlNodeState == null || obxmlNodeState.IsAttribute)
			{
				return null;
			}
			if (flag)
			{
				if (((int)b & ObxmlInstructionFormat.ELSTF_NOTDECTYP) > 0)
				{
					this.DataStream.ReadInt4();
				}
				if (((int)b & ObxmlInstructionFormat.ELSTF_PFXID) > 0)
				{
					short num = this.DataStream.ReadShortInt();
					PrefixInfo prefixInfo = this.m_DecodeState.GetPrefixInfo((ulong)((long)num));
					prefix = prefixInfo.Prefix;
					obxmlNodeState.PrefixId = num;
				}
			}
			prefix = obxmlNodeState.SetPrefix(this.m_DecodeState, prefix);
			return obxmlNodeState;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006C50 File Offset: 0x00004E50
		internal ObxmlNodeState ReadElementInfo(ObxmlOpcode.OpcodeIds opcode, bool optimizedOpcode, ObxmlNodeState ns = null, bool arrayModeDataBegin = false)
		{
			string text = null;
			ulong num = 0UL;
			if (ns == null)
			{
				ns = this.ReadElementInfo(opcode, optimizedOpcode, ref num, out text);
			}
			if (ns != null)
			{
				ns.IsOptimizedOpcode = optimizedOpcode;
				this.m_DecodeState.PushNodeState(ns);
				long num2 = 0L;
				if (ObxmlInstruction.ProcessPendingBytesOrTokenData(this.m_DecodeState, false, false, out num2) > 0L && !arrayModeDataBegin && ns.NodeDataLen > 0UL)
				{
					ulong num3 = 0UL;
					string text2 = null;
					this.m_DecodeState.m_CurrentInstruction.m_CsxDataLength = 0L;
					byte[] eventChars = this.GetEventChars(opcode, out num3, out text2);
					if (eventChars != null)
					{
						throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.NotImplemented, null, ObxmlOpcode.OpcodeIds.None));
					}
					if (!this.m_DecodeState.m_DTDInfo.IsProcessingDTD)
					{
						this.RequestOutput.InsideDataSection = true;
					}
					if (!string.IsNullOrEmpty(text2))
					{
						ns.PendingDataNode = true;
						this.m_DecodeState.m_CurrentInstruction.m_CsxDataText = text2.ReplaceXmlChars();
						this.m_DecodeState.m_CurrentInstruction.m_CsxDataLength = (long)this.m_DecodeState.m_CurrentInstruction.m_CsxDataText.Length * 2L;
					}
				}
				if (!arrayModeDataBegin)
				{
					this.ReadAttributes_New(false);
				}
				this.m_DecodeState.TryAppendingBeginTagClosure(ns, true, true);
				ns.NodeId = this.m_DecodeState.LastNodeId;
				this.m_DecodeState.m_CurrentInstruction.AppendElementStartToken(this.m_DecodeState, ns, true);
				for (int i = 0; i < this.m_DecodeState.AttributeList.Count; i++)
				{
					this.m_DecodeState.m_CurrentInstruction.AppendAttributeToken(this.m_DecodeState, this.m_DecodeState.AttributeList[i]);
				}
				this.m_DecodeState.AttributeList.Clear();
			}
			return ns;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006E0C File Offset: 0x0000500C
		private static long ProcessPendingBytesOrTokenData(ObxmlDecodeState decodeState, bool initTokenOrDataLength, bool initCsxDataLength, out long processed)
		{
			long result;
			if ((result = ObxmlInstruction.ProcessTokenOrTextData(decodeState, initTokenOrDataLength, out processed)) > 0L)
			{
				result = ObxmlInstruction.ProcessCsxBytes(decodeState, initCsxDataLength, out processed);
			}
			return result;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006E34 File Offset: 0x00005034
		private static long ProcessTokenOrTextData(ObxmlDecodeState decodeState, bool initTokenOrDataLength, out long processed)
		{
			processed = decodeState.WriteTokenOrData(initTokenOrDataLength);
			return decodeState.m_RequestObject.m_CountRemaining;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006E4C File Offset: 0x0000504C
		private static long ProcessCsxBytes(ObxmlDecodeState decodeState, bool initCsxDataLength, out long processed)
		{
			processed = (long)decodeState.WriteCsxBytes(initCsxDataLength);
			return decodeState.m_RequestObject.m_CountRemaining;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006E64 File Offset: 0x00005064
		private long ProcessEndElement(out long processed)
		{
			long countRemaining = this.m_DecodeState.m_RequestObject.m_CountRemaining;
			this.m_DecodeState.SetDecodeState(DecodeStates.ElementEndStart | DecodeStates.ElementEndPending, false);
			ObxmlNodeState obxmlNodeState = this.m_DecodeState.PopNodeState();
			ObxmlToken elementToken = obxmlNodeState.m_ElementToken;
			processed = 0L;
			if (elementToken.IsElementToken)
			{
				this.m_DecodeState.m_CurrentInstruction.AppendElementEndToken(this.m_DecodeState, obxmlNodeState, true);
				processed = this.m_DecodeState.WriteTokenOrData(true);
			}
			if (this.m_DecodeState.m_RequestObject.m_CountRemaining != 0L)
			{
				this.m_DecodeState.ResetDecodeStateMask(DecodeStates.ElementEndStart | DecodeStates.ElementEndPending);
			}
			return this.m_DecodeState.m_RequestObject.m_CountRemaining;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006F0C File Offset: 0x0000510C
		internal bool DecodeInstruction_New()
		{
			ObxmlOpcode.OpcodeIds opcode = (ObxmlOpcode.OpcodeIds)this.m_DecodeState.m_CurrentInstruction.m_Opcode;
			this.InstructionPending = true;
			this.InstructionPending = false;
			bool result = false;
			long num = 0L;
			if (opcode < ObxmlOpcode.OpcodeIds.DEFNM4L1 && !this.m_DecodeState.IsExtendedOpcode)
			{
				switch (opcode)
				{
				case ObxmlOpcode.OpcodeIds.DATSTR1:
				case ObxmlOpcode.OpcodeIds.DATSTR2:
				case ObxmlOpcode.OpcodeIds.DATSTR3:
				case ObxmlOpcode.OpcodeIds.DATSTR4:
				case ObxmlOpcode.OpcodeIds.DATSTR5:
				case ObxmlOpcode.OpcodeIds.DATSTR6:
				case ObxmlOpcode.OpcodeIds.DATSTR7:
				case ObxmlOpcode.OpcodeIds.DATSTR8:
				case ObxmlOpcode.OpcodeIds.DATSTR9:
				case ObxmlOpcode.OpcodeIds.DATSTR10:
				case ObxmlOpcode.OpcodeIds.DATSTR11:
				case ObxmlOpcode.OpcodeIds.DATSTR12:
				case ObxmlOpcode.OpcodeIds.DATSTR13:
				case ObxmlOpcode.OpcodeIds.DATSTR14:
				case ObxmlOpcode.OpcodeIds.DATSTR15:
				case ObxmlOpcode.OpcodeIds.DATSTR16:
				case ObxmlOpcode.OpcodeIds.DATSTR17:
				case ObxmlOpcode.OpcodeIds.DATSTR18:
				case ObxmlOpcode.OpcodeIds.DATSTR19:
				case ObxmlOpcode.OpcodeIds.DATSTR20:
				case ObxmlOpcode.OpcodeIds.DATSTR21:
				case ObxmlOpcode.OpcodeIds.DATSTR22:
				case ObxmlOpcode.OpcodeIds.DATSTR23:
				case ObxmlOpcode.OpcodeIds.DATSTR24:
				case ObxmlOpcode.OpcodeIds.DATSTR25:
				case ObxmlOpcode.OpcodeIds.DATSTR26:
				case ObxmlOpcode.OpcodeIds.DATSTR27:
				case ObxmlOpcode.OpcodeIds.DATSTR28:
				case ObxmlOpcode.OpcodeIds.DATSTR29:
				case ObxmlOpcode.OpcodeIds.DATSTR30:
				case ObxmlOpcode.OpcodeIds.DATSTR31:
				case ObxmlOpcode.OpcodeIds.DATSTR32:
				case ObxmlOpcode.OpcodeIds.DATSTR33:
				case ObxmlOpcode.OpcodeIds.DATSTR34:
				case ObxmlOpcode.OpcodeIds.DATSTR35:
				case ObxmlOpcode.OpcodeIds.DATSTR36:
				case ObxmlOpcode.OpcodeIds.DATSTR37:
				case ObxmlOpcode.OpcodeIds.DATSTR38:
				case ObxmlOpcode.OpcodeIds.DATSTR39:
				case ObxmlOpcode.OpcodeIds.DATSTR40:
				case ObxmlOpcode.OpcodeIds.DATSTR41:
				case ObxmlOpcode.OpcodeIds.DATSTR42:
				case ObxmlOpcode.OpcodeIds.DATSTR43:
				case ObxmlOpcode.OpcodeIds.DATSTR44:
				case ObxmlOpcode.OpcodeIds.DATSTR45:
				case ObxmlOpcode.OpcodeIds.DATSTR46:
				case ObxmlOpcode.OpcodeIds.DATSTR47:
				case ObxmlOpcode.OpcodeIds.DATSTR48:
				case ObxmlOpcode.OpcodeIds.DATSTR49:
				case ObxmlOpcode.OpcodeIds.DATSTR50:
				case ObxmlOpcode.OpcodeIds.DATSTR51:
				case ObxmlOpcode.OpcodeIds.DATSTR52:
				case ObxmlOpcode.OpcodeIds.DATSTR53:
				case ObxmlOpcode.OpcodeIds.DATSTR54:
				case ObxmlOpcode.OpcodeIds.DATSTR55:
				case ObxmlOpcode.OpcodeIds.DATSTR56:
				case ObxmlOpcode.OpcodeIds.DATSTR57:
				case ObxmlOpcode.OpcodeIds.DATSTR58:
				case ObxmlOpcode.OpcodeIds.DATSTR59:
				case ObxmlOpcode.OpcodeIds.DATSTR60:
				case ObxmlOpcode.OpcodeIds.DATSTR61:
				case ObxmlOpcode.OpcodeIds.DATSTR62:
				case ObxmlOpcode.OpcodeIds.DATSTR63:
				case ObxmlOpcode.OpcodeIds.DATSTR64:
				case ObxmlOpcode.OpcodeIds.DATBIN1:
				case ObxmlOpcode.OpcodeIds.DATBIN2:
				case ObxmlOpcode.OpcodeIds.DATBIN3:
				case ObxmlOpcode.OpcodeIds.DATBIN4:
				case ObxmlOpcode.OpcodeIds.DATBIN5:
				case ObxmlOpcode.OpcodeIds.DATBIN6:
				case ObxmlOpcode.OpcodeIds.DATBIN7:
				case ObxmlOpcode.OpcodeIds.DATBIN8:
				case ObxmlOpcode.OpcodeIds.DATBIN9:
				case ObxmlOpcode.OpcodeIds.DATBIN10:
				case ObxmlOpcode.OpcodeIds.DATBIN11:
				case ObxmlOpcode.OpcodeIds.DATBIN12:
				case ObxmlOpcode.OpcodeIds.DATBIN13:
				case ObxmlOpcode.OpcodeIds.DATBIN14:
				case ObxmlOpcode.OpcodeIds.DATBIN15:
				case ObxmlOpcode.OpcodeIds.DATBIN16:
				case ObxmlOpcode.OpcodeIds.DATBIN17:
				case ObxmlOpcode.OpcodeIds.DATBIN18:
				case ObxmlOpcode.OpcodeIds.DATBIN19:
				case ObxmlOpcode.OpcodeIds.DATBIN20:
				case ObxmlOpcode.OpcodeIds.DATBIN21:
				case ObxmlOpcode.OpcodeIds.DATBIN22:
				case ObxmlOpcode.OpcodeIds.DATBIN23:
				case ObxmlOpcode.OpcodeIds.DATBIN24:
				case ObxmlOpcode.OpcodeIds.DATBIN25:
				case ObxmlOpcode.OpcodeIds.DATBIN26:
				case ObxmlOpcode.OpcodeIds.DATBIN27:
				case ObxmlOpcode.OpcodeIds.DATBIN28:
				case ObxmlOpcode.OpcodeIds.DATBIN29:
				case ObxmlOpcode.OpcodeIds.DATBIN30:
				case ObxmlOpcode.OpcodeIds.DATBIN31:
				case ObxmlOpcode.OpcodeIds.DATBIN32:
				case ObxmlOpcode.OpcodeIds.DATNM1:
				case ObxmlOpcode.OpcodeIds.DATNM2:
				case ObxmlOpcode.OpcodeIds.DATNM3:
				case ObxmlOpcode.OpcodeIds.DATNM4:
				case ObxmlOpcode.OpcodeIds.DATNM5:
				case ObxmlOpcode.OpcodeIds.DATNM6:
				case ObxmlOpcode.OpcodeIds.DATNM7:
				case ObxmlOpcode.OpcodeIds.DATNM8:
				case ObxmlOpcode.OpcodeIds.DATNM9:
				case ObxmlOpcode.OpcodeIds.DATNM10:
				case ObxmlOpcode.OpcodeIds.DATNM11:
				case ObxmlOpcode.OpcodeIds.DATNM12:
				case ObxmlOpcode.OpcodeIds.DATNM13:
				case ObxmlOpcode.OpcodeIds.DATNM14:
				case ObxmlOpcode.OpcodeIds.DATNM15:
				case ObxmlOpcode.OpcodeIds.DATNM16:
				case ObxmlOpcode.OpcodeIds.DATNM17:
				case ObxmlOpcode.OpcodeIds.DATNM18:
				case ObxmlOpcode.OpcodeIds.DATNM19:
				case ObxmlOpcode.OpcodeIds.DATNM20:
				case ObxmlOpcode.OpcodeIds.DATNM21:
				case ObxmlOpcode.OpcodeIds.DATINT1:
				case ObxmlOpcode.OpcodeIds.DATINT2:
				case ObxmlOpcode.OpcodeIds.DATINT4:
				case ObxmlOpcode.OpcodeIds.DATINT8:
				case ObxmlOpcode.OpcodeIds.DATUINT1:
				case ObxmlOpcode.OpcodeIds.DATUINT2:
				case ObxmlOpcode.OpcodeIds.DATUINT4:
				case ObxmlOpcode.OpcodeIds.DATUINT8:
				case ObxmlOpcode.OpcodeIds.DATFLT4:
				case ObxmlOpcode.OpcodeIds.DATFLT8:
				case ObxmlOpcode.OpcodeIds.DATEPH4:
				case ObxmlOpcode.OpcodeIds.DATEPH8:
				case ObxmlOpcode.OpcodeIds.DATEPZ6:
				case ObxmlOpcode.OpcodeIds.DATEPZ10:
				case ObxmlOpcode.OpcodeIds.DATODT:
				case ObxmlOpcode.OpcodeIds.DATOTS:
				case ObxmlOpcode.OpcodeIds.DATOTSZ:
				case ObxmlOpcode.OpcodeIds.DATBOL:
				case ObxmlOpcode.OpcodeIds.DATQNM:
				case ObxmlOpcode.OpcodeIds.DATENM1:
				case ObxmlOpcode.OpcodeIds.DATENM2:
				case ObxmlOpcode.OpcodeIds.DATAL2:
				case ObxmlOpcode.OpcodeIds.DATAL8:
				case ObxmlOpcode.OpcodeIds.DATEMPT:
					this.m_DecodeState.CurrentDataOperation = DataOperationTypes.LengthAndData;
					this.m_DecodeState.ResetDecodeStateMask(DecodeStates.ElementDataStartPartial);
					if (this.ReadAndWriteFixedData(opcode, out num) != 0L)
					{
						result = true;
						goto IL_BC2;
					}
					goto IL_BC2;
				case ObxmlOpcode.OpcodeIds.DTDSTR:
				{
					int num2 = (int)this.ReadNum2();
					this.m_DecodeState.m_DTDInfo.ObjectName = ((num2 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num2)) : null);
					num2 = (int)this.ReadNum2();
					this.m_DecodeState.m_DTDInfo.PublicId = ((num2 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num2)) : null);
					num2 = (int)this.ReadNum2();
					this.m_DecodeState.m_DTDInfo.SystemId = ((num2 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num2)) : null);
					string.IsNullOrEmpty(this.m_DecodeState.m_DTDInfo.SystemId);
					this.m_DecodeState.m_DTDInfo.StartDTD();
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.DTDELEM:
				{
					int num3 = (int)this.ReadNum2();
					DTDElementInfo dtdelementInfo = new DTDElementInfo();
					dtdelementInfo.ElementName = ((num3 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num3)) : null);
					num3 = (int)this.ReadNum2();
					dtdelementInfo.ContentSpec = ((num3 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num3)) : null);
					this.m_DecodeState.m_DTDInfo.m_ElementList.Add(dtdelementInfo);
					this.m_DecodeState.m_DTDInfo.ElementDecl(dtdelementInfo);
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.DTDALIST:
				{
					int num4 = (int)this.ReadNum2();
					DTDElementAttributeInfo dtdelementAttributeInfo = new DTDElementAttributeInfo();
					dtdelementAttributeInfo.ElementName = ((num4 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num4)) : null);
					num4 = (int)this.ReadNum2();
					dtdelementAttributeInfo.AttributeName = ((num4 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num4)) : null);
					num4 = (int)this.ReadNum2();
					dtdelementAttributeInfo.AttributeType = ((num4 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num4)) : null);
					this.m_DecodeState.m_DTDInfo.AttributeDecl(dtdelementAttributeInfo);
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.DTDENT:
				{
					int num5 = (int)this.ReadNum2();
					DTDObject dtdobject = new DTDObject(DTDObjectTypes.Entity, (num5 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num5)) : null);
					num5 = (int)this.ReadNum2();
					dtdobject.ObjectValue = ((num5 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num5)) : null);
					num5 = (int)this.ReadNum2();
					dtdobject.PublicId = ((num5 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num5)) : null);
					num5 = (int)this.ReadNum2();
					dtdobject.SystemId = ((num5 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num5)) : null);
					num5 = (int)this.ReadNum2();
					dtdobject.Note = ((num5 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num5)) : null);
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject);
					this.m_DecodeState.m_DTDInfo.EntityDecl(dtdobject);
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.DTDPENT:
				{
					int num6 = (int)this.ReadNum2();
					DTDObject dtdobject2 = new DTDObject(DTDObjectTypes.PartialEntity, (num6 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num6)) : null);
					num6 = (int)this.ReadNum2();
					dtdobject2.PublicId = ((num6 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num6)) : null);
					num6 = (int)this.ReadNum2();
					dtdobject2.SystemId = ((num6 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num6)) : null);
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject2);
					this.m_DecodeState.m_DTDInfo.EntityDecl(dtdobject2);
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.DTDNOT:
				{
					int num7 = (int)this.ReadNum2();
					DTDObject dtdobject3 = new DTDObject(DTDObjectTypes.Note, (num7 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num7)) : null);
					num7 = (int)this.ReadNum2();
					dtdobject3.PublicId = ((num7 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num7)) : null);
					num7 = (int)this.ReadNum2();
					dtdobject3.SystemId = ((num7 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num7)) : null);
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject3);
					this.m_DecodeState.m_DTDInfo.NotationDecl(dtdobject3);
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.DTDEND:
					this.m_DecodeState.m_DTDInfo.EndDTD();
					goto IL_BC2;
				case ObxmlOpcode.OpcodeIds.ENTREF:
				{
					int num8 = (int)this.ReadNum1();
					DTDObject dtdobject4 = new DTDObject(DTDObjectTypes.Entity, (num8 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num8)) : null);
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject4);
					this.m_DecodeState.m_DTDInfo.EntityReference(dtdobject4);
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.CHARREF:
					result = true;
					goto IL_BC2;
				case ObxmlOpcode.OpcodeIds.DOC:
					this.ReadDocHeader(opcode);
					if (this.DocHeader.XmlDecl)
					{
						this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData = new StringBuilder(this.DocHeader.GetHeaderString());
						num = this.m_DecodeState.WriteTokenOrData(true);
					}
					else
					{
						this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData = new StringBuilder();
					}
					result = true;
					goto IL_BC2;
				case ObxmlOpcode.OpcodeIds.STRTSEC:
					this.m_DecodeState.DecodeStateMask = DecodeStates.SectionStart;
					if (this.ReadSectionHeader(opcode))
					{
						this.m_DecodeState.SectionHeaderFound = true;
					}
					result = true;
					goto IL_BC2;
				case ObxmlOpcode.OpcodeIds.ENDSEC:
					if (this.m_DecodeState.ProcessingTokenSet)
					{
						this.m_DecodeState.ProcessingTokenSet = false;
					}
					ObxmlInstruction.ProcessPendingBytesOrTokenData(this.m_DecodeState, false, false, out num);
					this.m_DecodeState.Parent.SetDecodeComplete();
					result = true;
					goto IL_BC2;
				case ObxmlOpcode.OpcodeIds.CHUNK:
					goto IL_BC2;
				case ObxmlOpcode.OpcodeIds.TEXT1:
				case ObxmlOpcode.OpcodeIds.TEXT2:
				case ObxmlOpcode.OpcodeIds.TEXT8:
				{
					this.m_DecodeState.CurrentDataOperation = DataOperationTypes.LengthAndData;
					string text = this.ReadEventText(this.GetLastNodeState(), opcode);
					text = text.ReplaceXmlChars();
					this.m_DecodeState.GetLastNodeState().PendingDataNode = true;
					this.m_DecodeState.TryAppendingBeginTagClosure(null, false, false);
					this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData.Append(text);
					this.m_DecodeState.GetLastNodeState().ChildNodesCount++;
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.CDATA1:
				case ObxmlOpcode.OpcodeIds.CDATA2:
				case ObxmlOpcode.OpcodeIds.CDATA8:
				{
					ulong length = this.m_InstructionData[0];
					string cdataValue = this.DataStream.ReadUtf8String(length);
					this.m_DecodeState.TryAppendingBeginTagClosure(null, false, false);
					this.m_DecodeState.GetLastNodeState().PendingDataNode = true;
					this.m_DecodeState.m_CurrentInstruction.WriteCDATABlock(this.m_DecodeState, cdataValue);
					this.m_DecodeState.GetLastNodeState().ChildNodesCount++;
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.PI1L1:
				case ObxmlOpcode.OpcodeIds.PI2L4:
				{
					ulong num9 = this.m_InstructionData[0];
					int num10 = (int)this.m_InstructionData[1];
					string target = this.DataStream.ReadUtf8String((ulong)((long)num10));
					string value = this.DataStream.ReadUtf8String(num9 - (ulong)((long)num10));
					this.m_DecodeState.GetLastNodeState().ChildNodesCount++;
					this.m_DecodeState.GetLastNodeState().PendingDataNode = true;
					this.ProcessInstruction(target, value);
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.CMT1:
				case ObxmlOpcode.OpcodeIds.CMT2:
				case ObxmlOpcode.OpcodeIds.CMT8:
				{
					if (this.m_DecodeState.m_DTDInfo.IsProcessingDTD)
					{
						this.m_DecodeState.m_DTDInfo.InilializeSubset();
					}
					ulong length2 = this.m_InstructionData[0];
					string value2 = this.DataStream.ReadUtf8String(length2);
					this.m_DecodeState.TryAppendingBeginTagClosure(null, false, false);
					ObxmlNodeState lastNodeState = this.m_DecodeState.GetLastNodeState();
					bool flag = lastNodeState != null && (lastNodeState.m_ParentNode == null || (lastNodeState.m_ParentNode != null && !this.m_DecodeState.PerformFullXmlParse));
					if (flag)
					{
						this.m_DecodeState.m_CurrentInstruction.AppendWhiteSpaces(this.m_DecodeState, lastNodeState, false, true);
					}
					this.m_DecodeState.m_CurrentInstruction.AppendComment(value2, false);
					if (lastNodeState != null)
					{
						lastNodeState.ChildNodesCount++;
						goto IL_BC2;
					}
					goto IL_BC2;
				}
				case ObxmlOpcode.OpcodeIds.PRTDATA:
				case ObxmlOpcode.OpcodeIds.PRTDATAT:
				{
					ulong dataLen = this.GetDataLen((short)opcode);
					string value3 = this.DataStream.ReadUtf8String(dataLen);
					ObxmlInstruction.ProcessPendingBytesOrTokenData(this.m_DecodeState, true, true, out num);
					this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData.Append(value3);
					ObxmlInstruction.ProcessTokenOrTextData(this.m_DecodeState, true, out num);
					this.m_DecodeState.SetDecodeState(DecodeStates.ElementDataStartPartial, false);
					result = true;
					goto IL_BC2;
				}
				}
				result = false;
			}
			IL_BC2:
			if (this.m_DecodeState.IsExtendedOpcode)
			{
				switch (opcode)
				{
				case ObxmlOpcode.OpcodeIds.DATSTR1:
				{
					int num11 = (int)this.ReadNum2();
					this.m_DecodeState.m_DTDInfo.ObjectName = ((num11 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num11)) : null);
					num11 = (int)this.ReadNum2();
					this.m_DecodeState.m_DTDInfo.PublicId = ((num11 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num11)) : null);
					num11 = (int)this.ReadNum2();
					this.m_DecodeState.m_DTDInfo.SystemId = ((num11 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num11)) : null);
					num11 = (int)this.ReadNum1();
					if (num11 == ObxmlOpcode.DTD_SYSTEM_EMPTY)
					{
						this.m_DecodeState.m_DTDInfo.SystemId = "";
					}
					else if (num11 == ObxmlOpcode.DTD_internal_EMPTY)
					{
						this.m_DecodeState.m_DTDInfo.PublicId = "";
					}
					else if (num11 == ObxmlOpcode.DTD_internal_SYSTEM_EMPTY)
					{
						this.m_DecodeState.m_DTDInfo.SystemId = "";
						this.m_DecodeState.m_DTDInfo.PublicId = "";
					}
					this.m_DecodeState.IsExtendedOpcode = false;
					this.m_DecodeState.m_DTDInfo.StartDTD();
					break;
				}
				case ObxmlOpcode.OpcodeIds.DATSTR2:
				{
					int num12 = (int)this.ReadNum2();
					DTDObject dtdobject5 = new DTDObject(DTDObjectTypes.Note, (num12 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num12)) : null);
					num12 = (int)this.ReadNum2();
					dtdobject5.PublicId = ((num12 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num12)) : null);
					num12 = (int)this.ReadNum2();
					dtdobject5.SystemId = ((num12 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num12)) : null);
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject5);
					num12 = (int)this.ReadNum1();
					if (num12 == ObxmlOpcode.DTD_SYSTEM_EMPTY)
					{
						dtdobject5.SystemId = "";
					}
					else if (num12 == ObxmlOpcode.DTD_internal_EMPTY)
					{
						dtdobject5.PublicId = "";
					}
					else if (num12 == ObxmlOpcode.DTD_internal_SYSTEM_EMPTY)
					{
						dtdobject5.SystemId = "";
						dtdobject5.PublicId = "";
					}
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject5);
					this.m_DecodeState.IsExtendedOpcode = false;
					this.m_DecodeState.m_DTDInfo.NotationDecl(dtdobject5);
					break;
				}
				case ObxmlOpcode.OpcodeIds.DATSTR3:
				{
					int num13 = (int)this.ReadNum2();
					DTDObject dtdobject6 = new DTDObject(DTDObjectTypes.Entity, (num13 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num13)) : null);
					num13 = (int)this.ReadNum2();
					dtdobject6.ObjectValue = ((num13 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num13)) : null);
					num13 = (int)this.ReadNum2();
					dtdobject6.PublicId = ((num13 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num13)) : null);
					num13 = (int)this.ReadNum2();
					dtdobject6.SystemId = ((num13 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num13)) : null);
					num13 = (int)this.ReadNum2();
					dtdobject6.Note = ((num13 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num13)) : null);
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject6);
					num13 = (int)this.ReadNum1();
					if (num13 == ObxmlOpcode.DTD_SYSTEM_EMPTY)
					{
						dtdobject6.SystemId = "";
					}
					else if (num13 == ObxmlOpcode.DTD_internal_EMPTY)
					{
						dtdobject6.PublicId = "";
					}
					else if (num13 == ObxmlOpcode.DTD_internal_SYSTEM_EMPTY)
					{
						dtdobject6.SystemId = "";
						dtdobject6.PublicId = "";
					}
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject6);
					this.m_DecodeState.IsExtendedOpcode = false;
					this.m_DecodeState.m_DTDInfo.EntityDecl(dtdobject6);
					break;
				}
				case ObxmlOpcode.OpcodeIds.DATSTR4:
				{
					int num14 = (int)this.ReadNum2();
					DTDObject dtdobject7 = new DTDObject(DTDObjectTypes.PartialEntity, (num14 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num14)) : null);
					num14 = (int)this.ReadNum2();
					dtdobject7.ObjectValue = ((num14 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num14)) : null);
					num14 = (int)this.ReadNum2();
					dtdobject7.PublicId = ((num14 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num14)) : null);
					num14 = (int)this.ReadNum2();
					dtdobject7.SystemId = ((num14 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num14)) : null);
					if (dtdobject7.ObjectName != null && dtdobject7.ObjectName.Length != 0)
					{
						dtdobject7.ObjectName = ObxmlInstructionState.sXmlPercentage + dtdobject7.ObjectName;
					}
					num14 = (int)this.ReadNum1();
					if (num14 == ObxmlOpcode.DTD_SYSTEM_EMPTY)
					{
						dtdobject7.SystemId = "";
					}
					else if (num14 == ObxmlOpcode.DTD_internal_EMPTY)
					{
						dtdobject7.PublicId = "";
					}
					else if (num14 == ObxmlOpcode.DTD_internal_SYSTEM_EMPTY)
					{
						dtdobject7.SystemId = "";
						dtdobject7.PublicId = "";
					}
					this.m_DecodeState.m_DTDInfo.m_ObjectList.Add(dtdobject7);
					this.m_DecodeState.IsExtendedOpcode = false;
					this.m_DecodeState.m_DTDInfo.EntityDecl(dtdobject7);
					break;
				}
				case ObxmlOpcode.OpcodeIds.DATSTR5:
				{
					int num15 = (int)this.ReadNum2();
					string elementName = (num15 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num15)) : null;
					int num16 = (int)this.ReadNum2();
					for (int i = 0; i < num16; i++)
					{
						DTDElementAttributeInfo dtdelementAttributeInfo2 = new DTDElementAttributeInfo();
						dtdelementAttributeInfo2.ElementName = elementName;
						num15 = (int)this.ReadNum2();
						dtdelementAttributeInfo2.AttributeName = ((num15 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num15)) : null);
						num15 = (int)this.ReadNum2();
						dtdelementAttributeInfo2.AttributeType = ((num15 > 0) ? this.DataStream.ReadUtf8String((ulong)((long)num15)) : null);
						dtdelementAttributeInfo2.SplitAttrString(dtdelementAttributeInfo2.AttributeType);
						this.m_DecodeState.m_DTDInfo.AttributeDecl(dtdelementAttributeInfo2);
					}
					byte b = this.ReadNum1();
					if (b != 0)
					{
						throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.StringNotTerminated, null, ObxmlOpcode.OpcodeIds.None));
					}
					if (this.m_DecodeState.m_DTDInfo.m_AttributeList != null)
					{
						this.m_DecodeState.m_DTDInfo.m_AttributeList.Clear();
					}
					this.m_DecodeState.IsExtendedOpcode = false;
					break;
				}
				default:
					throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidOpcode, null, ObxmlOpcode.OpcodeIds.None));
				}
			}
			switch (opcode)
			{
			case ObxmlOpcode.OpcodeIds.DEFNM4L1:
			case ObxmlOpcode.OpcodeIds.DEFNM4L2:
			case ObxmlOpcode.OpcodeIds.DEFNM8L1:
			case ObxmlOpcode.OpcodeIds.DEFNM8L2:
				this.ReadNamespaceToken(opcode);
				result = true;
				break;
			case ObxmlOpcode.OpcodeIds.DEFPFX4:
			case ObxmlOpcode.OpcodeIds.DEFPFX8:
			{
				PrefixInfo prefixInfo = null;
				result = this.ReadPrefix(opcode, out prefixInfo);
				break;
			}
			case ObxmlOpcode.OpcodeIds.DEFQ4N4L1:
			case ObxmlOpcode.OpcodeIds.DEFQ4N4L2:
			case ObxmlOpcode.OpcodeIds.DEFQ4N8L1:
			case ObxmlOpcode.OpcodeIds.DEFQ4N8L2:
			case ObxmlOpcode.OpcodeIds.DEFQ8N4L1:
			case ObxmlOpcode.OpcodeIds.DEFQ8N4L2:
			case ObxmlOpcode.OpcodeIds.DEFQ8N8L1:
			case ObxmlOpcode.OpcodeIds.DEFQ8N8L2:
			{
				ObxmlToken obxmlToken = null;
				this.ReadToken(opcode, out obxmlToken);
				result = true;
				break;
			}
			case ObxmlOpcode.OpcodeIds.PRPK1L1:
			case ObxmlOpcode.OpcodeIds.PRPK1L2:
			case ObxmlOpcode.OpcodeIds.PRPK2L1:
			case ObxmlOpcode.OpcodeIds.PRPK2L2:
			case ObxmlOpcode.OpcodeIds.PRPT2L1:
			case ObxmlOpcode.OpcodeIds.PRPT2L2:
			case ObxmlOpcode.OpcodeIds.PRPT4L1:
			case ObxmlOpcode.OpcodeIds.PRPT4L2:
			case ObxmlOpcode.OpcodeIds.PRPT8L1:
			case ObxmlOpcode.OpcodeIds.PRPT8L2:
			{
				this.m_DecodeState.SetDecodeState(DecodeStates.ElementStart | DecodeStates.ElementEndPending | DecodeStates.ElementDataStartElementOpt, true);
				ObxmlNodeState obxmlNodeState = this.ReadElementInfo(opcode, true, null, false);
				this.m_DecodeState.HasBeginTagClosurePending = true;
				if (ObxmlInstruction.ProcessTokenOrTextData(this.m_DecodeState, true, out num) > 0L)
				{
					if (obxmlNodeState.NodeDataLen > 0UL && this.m_DecodeState.m_CurrentInstruction.m_CsxDataLength > 0L)
					{
						this.m_DecodeState.m_CurrentInstruction.AppendElementStartTagClosing(this.m_DecodeState, this.m_DecodeState.AttributeList.Count > 0);
						obxmlNodeState.ChildNodesCount++;
						ObxmlInstruction.ProcessPendingBytesOrTokenData(this.m_DecodeState, true, false, out num);
					}
					if (this.ProcessEndElement(out num) > 0L)
					{
						this.m_DecodeState.ResetDecodeStateMask(DecodeStates.ElementEndStart | DecodeStates.ElementEndPending | DecodeStates.ElementDataStartElementOpt);
					}
				}
				break;
			}
			case ObxmlOpcode.OpcodeIds.PRPSTK1:
			case ObxmlOpcode.OpcodeIds.PRPSTK2:
			case ObxmlOpcode.OpcodeIds.PRPSTT2:
			case ObxmlOpcode.OpcodeIds.PRPSTT4:
			case ObxmlOpcode.OpcodeIds.PRPSTT8:
			case ObxmlOpcode.OpcodeIds.PRPSTK1F:
			case ObxmlOpcode.OpcodeIds.PRPSTK2F:
			case ObxmlOpcode.OpcodeIds.PRPSTT2F:
			case ObxmlOpcode.OpcodeIds.PRPSTT4F:
			case ObxmlOpcode.OpcodeIds.PRPSTT8F:
			case ObxmlOpcode.OpcodeIds.PRPSTK1V:
			case ObxmlOpcode.OpcodeIds.PRPSTK2V:
			case ObxmlOpcode.OpcodeIds.PRPSTT2V:
			case ObxmlOpcode.OpcodeIds.PRPSTT4V:
			case ObxmlOpcode.OpcodeIds.PRPSTT8V:
				this.m_DecodeState.SetDecodeState(DecodeStates.ElementStart, true);
				this.ReadElementInfo(opcode, false, null, false);
				this.m_DecodeState.HasBeginTagClosurePending = true;
				ObxmlInstruction.ProcessTokenOrTextData(this.m_DecodeState, true, out num);
				break;
			case ObxmlOpcode.OpcodeIds.ELMSTART:
			case ObxmlOpcode.OpcodeIds.ELMSTSSEQ:
			{
				ObxmlNodeState lastNodeState2 = this.GetLastNodeState();
				ObxmlNodeState ns = null;
				if (lastNodeState2.IsArrayMode)
				{
					ns = this.m_DecodeState.GetCurrentNodeState(true);
				}
				else
				{
					if (!lastNodeState2.IsSequentialMode)
					{
						throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidInstruction, null, ObxmlOpcode.OpcodeIds.None));
					}
					lastNodeState2.ArrayModeCount++;
				}
				this.ReadElementInfo(opcode, false, ns, false);
				this.m_DecodeState.HasBeginTagClosurePending = true;
				break;
			}
			case ObxmlOpcode.OpcodeIds.ARRBEG:
			case ObxmlOpcode.OpcodeIds.ARREND:
			{
				ObxmlNodeState lastNodeState3 = this.GetLastNodeState();
				lastNodeState3.SetArrayMode(opcode == ObxmlOpcode.OpcodeIds.ARRBEG);
				break;
			}
			case ObxmlOpcode.OpcodeIds.ENDPRP:
				this.m_DecodeState.SetDecodeState(DecodeStates.ElementEndStart, true);
				this.ProcessEndElement(out num);
				break;
			case ObxmlOpcode.OpcodeIds.SPACE1:
			case ObxmlOpcode.OpcodeIds.SPACE2:
			{
				int charCount = 0;
				char[] value4 = this.DecodeWhitespaces(opcode, out charCount);
				this.m_DecodeState.TryAppendingBeginTagClosure(null, false, false);
				this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData.Append(value4, 0, charCount);
				break;
			}
			case ObxmlOpcode.OpcodeIds.SPACE8:
				this.SkipData(opcode);
				break;
			case ObxmlOpcode.OpcodeIds.ENDPRPSP:
			case ObxmlOpcode.OpcodeIds.ENDPRPSP8:
				this.m_DecodeState.SetDecodeState(DecodeStates.ElementEndStart, true);
				this.ProcessEndElement(out num);
				break;
			}
			return result;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000084B4 File Offset: 0x000066B4
		private bool HandleArrayModeDataBegin(ObxmlOpcode.OpcodeIds opcode, out long processed)
		{
			ObxmlNodeState lastNodeState = this.GetLastNodeState();
			ObxmlNodeState obxmlNodeState = null;
			processed = 0L;
			if (lastNodeState.IsArrayMode)
			{
				obxmlNodeState = this.m_DecodeState.GetCurrentNodeState(true);
			}
			else
			{
				if (!lastNodeState.IsSequentialMode)
				{
					return false;
				}
				lastNodeState.ArrayModeCount++;
			}
			this.ReadElementInfo(opcode, false, obxmlNodeState, true);
			this.m_DecodeState.HasBeginTagClosurePending = true;
			if (obxmlNodeState == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidInstruction, null, ObxmlOpcode.OpcodeIds.None));
			}
			obxmlNodeState.ChildNodesCount++;
			this.m_DecodeState.TryAppendingBeginTagClosure(obxmlNodeState, false, false);
			ObxmlInstruction.ProcessTokenOrTextData(this.m_DecodeState, true, out processed);
			return true;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008564 File Offset: 0x00006764
		private void ReadAttributes_New(bool skip = false)
		{
			this.m_DecodeState.AttributeList.Clear();
			ObxmlNodeState obxmlNodeState = this.GetLastNodeState();
			this.m_DecodeState.SetDecodeState(DecodeStates.ElementStartAttributeStart, true);
			for (;;)
			{
				ObxmlOpcode.OpcodeIds opcodeIds = this.ReadInstructionInfo(true);
				this.InstructionPending = true;
				if (opcodeIds == ObxmlOpcode.OpcodeIds.None)
				{
					break;
				}
				if ((short)opcodeIds == ObxmlInstructionFormat.NOOP)
				{
					goto Block_2;
				}
				switch (opcodeIds)
				{
				case ObxmlOpcode.OpcodeIds.CHUNK:
				case ObxmlOpcode.OpcodeIds.NOSEQ:
				case ObxmlOpcode.OpcodeIds.NOP:
				case ObxmlOpcode.OpcodeIds.NOPARR:
					continue;
				case ObxmlOpcode.OpcodeIds.REF:
				case ObxmlOpcode.OpcodeIds.TEXT1:
				case ObxmlOpcode.OpcodeIds.TEXT2:
				case ObxmlOpcode.OpcodeIds.TEXT8:
				case ObxmlOpcode.OpcodeIds.CDATA1:
				case ObxmlOpcode.OpcodeIds.CDATA2:
				case ObxmlOpcode.OpcodeIds.CDATA8:
				case ObxmlOpcode.OpcodeIds.PI1L1:
				case ObxmlOpcode.OpcodeIds.PI2L4:
				case ObxmlOpcode.OpcodeIds.CMT1:
				case ObxmlOpcode.OpcodeIds.CMT2:
				case ObxmlOpcode.OpcodeIds.CMT8:
				case ObxmlOpcode.OpcodeIds.ELMSTART:
				case ObxmlOpcode.OpcodeIds.ELMSTSSEQ:
				case ObxmlOpcode.OpcodeIds.ARRBEG:
				case ObxmlOpcode.OpcodeIds.ARREND:
				case ObxmlOpcode.OpcodeIds.ENDPRP:
				case ObxmlOpcode.OpcodeIds.PRTDATA:
				case ObxmlOpcode.OpcodeIds.PRTDATAT:
				case ObxmlOpcode.OpcodeIds.PRTTEXT:
				case ObxmlOpcode.OpcodeIds.PRTCDATA:
				case ObxmlOpcode.OpcodeIds.PRTPI:
				case ObxmlOpcode.OpcodeIds.PRTCMT:
				case ObxmlOpcode.OpcodeIds.SPACE1:
				case ObxmlOpcode.OpcodeIds.SPACE2:
				case ObxmlOpcode.OpcodeIds.SPACE8:
				case ObxmlOpcode.OpcodeIds.XMLDECL:
					return;
				case ObxmlOpcode.OpcodeIds.DEFNM4L1:
				case ObxmlOpcode.OpcodeIds.DEFNM4L2:
				case ObxmlOpcode.OpcodeIds.DEFNM8L1:
				case ObxmlOpcode.OpcodeIds.DEFNM8L2:
					this.ReadNamespaceToken(opcodeIds);
					continue;
				case ObxmlOpcode.OpcodeIds.DEFPFX4:
				case ObxmlOpcode.OpcodeIds.DEFPFX8:
				{
					PrefixInfo prefixInfo = null;
					this.ReadPrefix(opcodeIds, out prefixInfo);
					continue;
				}
				case ObxmlOpcode.OpcodeIds.DEFQ4N4L1:
				case ObxmlOpcode.OpcodeIds.DEFQ4N4L2:
				case ObxmlOpcode.OpcodeIds.DEFQ4N8L1:
				case ObxmlOpcode.OpcodeIds.DEFQ4N8L2:
				case ObxmlOpcode.OpcodeIds.DEFQ8N4L1:
				case ObxmlOpcode.OpcodeIds.DEFQ8N4L2:
				case ObxmlOpcode.OpcodeIds.DEFQ8N8L1:
				case ObxmlOpcode.OpcodeIds.DEFQ8N8L2:
				{
					ObxmlToken obxmlToken = null;
					this.ReadToken(opcodeIds, out obxmlToken);
					continue;
				}
				case ObxmlOpcode.OpcodeIds.PRPK1L1:
				case ObxmlOpcode.OpcodeIds.PRPK1L2:
				case ObxmlOpcode.OpcodeIds.PRPK2L1:
				case ObxmlOpcode.OpcodeIds.PRPK2L2:
				case ObxmlOpcode.OpcodeIds.PRPT2L1:
				case ObxmlOpcode.OpcodeIds.PRPT2L2:
				case ObxmlOpcode.OpcodeIds.PRPT4L1:
				case ObxmlOpcode.OpcodeIds.PRPT4L2:
				case ObxmlOpcode.OpcodeIds.PRPT8L1:
				case ObxmlOpcode.OpcodeIds.PRPT8L2:
				{
					if ((this.m_InstructionFormat.flags & ObxmlInstructionFormat.KIDNUM) <= 0)
					{
						obxmlNodeState = this.GetNodeData(this.m_InstructionData[1], NodeTypes.None, 0UL);
					}
					if (!obxmlNodeState.IsAttribute)
					{
						goto Block_22;
					}
					this.m_DecodeState.CurrentDataOperation = DataOperationTypes.LengthAndData;
					if (skip)
					{
						continue;
					}
					string value = this.ReadEventText(null, opcodeIds);
					string text = obxmlNodeState.SetPrefix(this.m_DecodeState, null);
					if (string.IsNullOrEmpty(text))
					{
						obxmlNodeState.AddAttributeInfo(this.m_DecodeState, new AttributeInfo(this.m_DecodeState, obxmlNodeState, false, text, obxmlNodeState.m_ElementToken.TokenName, this.m_DecodeState.GetNamespaceUriForNode(obxmlNodeState), value));
						continue;
					}
					obxmlNodeState.AddAttributeInfo(this.m_DecodeState, new AttributeInfo(this.m_DecodeState, obxmlNodeState, false, text, obxmlNodeState.m_ElementToken.TokenName, this.m_DecodeState.GetNamespaceUriForNode(obxmlNodeState), value));
					continue;
				}
				case ObxmlOpcode.OpcodeIds.PRPSTK1:
				case ObxmlOpcode.OpcodeIds.PRPSTK2:
				case ObxmlOpcode.OpcodeIds.PRPSTT2:
				case ObxmlOpcode.OpcodeIds.PRPSTT4:
				case ObxmlOpcode.OpcodeIds.PRPSTT8:
				case ObxmlOpcode.OpcodeIds.PRPSTK1F:
				case ObxmlOpcode.OpcodeIds.PRPSTK2F:
				case ObxmlOpcode.OpcodeIds.PRPSTT2F:
				case ObxmlOpcode.OpcodeIds.PRPSTT4F:
				case ObxmlOpcode.OpcodeIds.PRPSTT8F:
				case ObxmlOpcode.OpcodeIds.PRPSTK1V:
				case ObxmlOpcode.OpcodeIds.PRPSTK2V:
				case ObxmlOpcode.OpcodeIds.PRPSTT2V:
				case ObxmlOpcode.OpcodeIds.PRPSTT4V:
				case ObxmlOpcode.OpcodeIds.PRPSTT8V:
				case ObxmlOpcode.OpcodeIds.ARRSTK1V:
				case ObxmlOpcode.OpcodeIds.ARRSTK2V:
				case ObxmlOpcode.OpcodeIds.ARRSTT4V:
				case ObxmlOpcode.OpcodeIds.ARRSTT8V:
				{
					bool flag = false;
					string text = null;
					byte b;
					if (opcodeIds >= ObxmlOpcode.OpcodeIds.PRPSTK1F && opcodeIds <= ObxmlOpcode.OpcodeIds.PRPSTT8F)
					{
						b = (byte)this.m_InstructionData[1];
					}
					else if ((opcodeIds >= ObxmlOpcode.OpcodeIds.PRPSTK1V && opcodeIds <= ObxmlOpcode.OpcodeIds.PRPSTT8V) || (opcodeIds >= ObxmlOpcode.OpcodeIds.ARRSTK1V && opcodeIds <= ObxmlOpcode.OpcodeIds.ARRSTT8V))
					{
						b = (byte)this.m_InstructionData[2];
						flag = true;
					}
					else
					{
						b = 0;
					}
					if ((this.m_InstructionFormat.flags & ObxmlInstructionFormat.KIDNUM) <= 0)
					{
						ulong tokenId = flag ? this.m_InstructionData[1] : this.m_InstructionData[0];
						obxmlNodeState = this.GetNodeData(tokenId, NodeTypes.None, 0UL);
					}
					if (!obxmlNodeState.IsAttribute)
					{
						return;
					}
					if (flag)
					{
						if (((int)b & ObxmlInstructionFormat.ELSTF_NOTDECTYP) > 0)
						{
							this.DataStream.ReadInt4();
						}
						if (((int)b & ObxmlInstructionFormat.ELSTF_PFXID) > 0)
						{
							short num = this.DataStream.ReadShortInt();
							PrefixInfo prefixInfo2 = this.m_DecodeState.GetPrefixInfo((ulong)((long)num));
							text = prefixInfo2.Prefix;
						}
					}
					text = obxmlNodeState.SetPrefix(this.m_DecodeState, text);
					string value = "";
					for (;;)
					{
						opcodeIds = this.ReadInstructionInfo(true);
						if (opcodeIds == ObxmlOpcode.OpcodeIds.ENDPRP)
						{
							break;
						}
						if (!this.m_InstructionFormat.hasfixeddata && opcodeIds != ObxmlOpcode.OpcodeIds.DATAL2 && opcodeIds != ObxmlOpcode.OpcodeIds.DATAL8)
						{
							goto Block_17;
						}
						ulong dataLen = this.GetDataLen((short)opcodeIds);
						if (!skip)
						{
							value = this.DataStream.ReadUtf8String(dataLen);
						}
						else
						{
							this.SkipBytes((long)dataLen);
						}
					}
					this.m_DecodeState.SetDecodeState(DecodeStates.ElementStartAttributeDone, true);
					if (skip)
					{
						continue;
					}
					if (text == null || text.Length == 0)
					{
						AttributeInfo attributeInfo = new AttributeInfo(this.m_DecodeState, obxmlNodeState, false, text, obxmlNodeState.m_ElementToken.TokenName, this.m_DecodeState.GetNamespaceUriForNode(obxmlNodeState), value);
						obxmlNodeState.AddAttributeInfo(this.m_DecodeState, attributeInfo);
						continue;
					}
					new PrefixInfo(this.m_DecodeState.Parent.DecodeContext, 0, text, obxmlNodeState.m_ElementToken.NamespaceId, null);
					obxmlNodeState.AddAttributeInfo(this.m_DecodeState, new AttributeInfo(this.m_DecodeState, obxmlNodeState, false, text, obxmlNodeState.m_ElementToken.TokenName, this.m_DecodeState.GetNamespaceUriForNode(obxmlNodeState), value));
					continue;
				}
				case ObxmlOpcode.OpcodeIds.NMSPC:
				{
					short num = (short)this.m_InstructionData[0];
					PrefixInfo prefixInfo3 = this.m_DecodeState.GetPrefixInfo((ulong)((long)num));
					this.m_DecodeState.PushNamespace(prefixInfo3);
					if (skip)
					{
						continue;
					}
					if (prefixInfo3.Prefix == null || prefixInfo3.Prefix.Length == 0)
					{
						obxmlNodeState.AddAttributeInfo(this.m_DecodeState, new AttributeInfo(this.m_DecodeState, obxmlNodeState, true, "xmlns", prefixInfo3.Prefix, ObxmlDecoder.nameXMLNSNamespace, prefixInfo3.Uri));
						continue;
					}
					obxmlNodeState.AddAttributeInfo(this.m_DecodeState, new AttributeInfo(this.m_DecodeState, obxmlNodeState, true, "xmlns", prefixInfo3.Prefix, ObxmlDecoder.nameXMLNSNamespace, prefixInfo3.Uri));
					continue;
				}
				case ObxmlOpcode.OpcodeIds.NSP4:
				case ObxmlOpcode.OpcodeIds.NSP8:
				{
					ulong dataLen = this.GetDataLen((short)opcodeIds);
					string text = this.DataStream.ReadUtf8String(dataLen);
					ulong nsid = this.m_InstructionData[1];
					string value = this.m_DecodeState.GetNamespace(nsid, TokenTypes.NamespaceToken);
					PrefixInfo pInfo = this.m_DecodeState.SetPrefix(0, text, nsid);
					this.m_DecodeState.PushNamespace(pInfo);
					if (skip)
					{
						continue;
					}
					if (text == null || text.Length == 0)
					{
						obxmlNodeState.AddAttributeInfo(this.m_DecodeState, new AttributeInfo(this.m_DecodeState, obxmlNodeState, true, text, "xmlns", ObxmlDecoder.nameXMLNSNamespace, value));
						continue;
					}
					obxmlNodeState.AddAttributeInfo(this.m_DecodeState, new AttributeInfo(this.m_DecodeState, obxmlNodeState, true, text, "xmlns", ObxmlDecoder.nameXMLNSNamespace, value));
					continue;
				}
				case ObxmlOpcode.OpcodeIds.SPACEQN:
				case ObxmlOpcode.OpcodeIds.SPACEQN8:
					this.m_DecodeState.CurrentDataOperation = DataOperationTypes.DataOnly;
					this.SkipData(opcodeIds);
					continue;
				}
				return;
			}
			throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidOpcode, null, ObxmlOpcode.OpcodeIds.None));
			Block_2:
			throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidOpcode, null, ObxmlOpcode.OpcodeIds.None));
			Block_17:
			throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidOpcode, null, ObxmlOpcode.OpcodeIds.None));
			Block_22:
			this.m_DecodeState.SetDecodeState(DecodeStates.ElementStartAttributeDone, true);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008C3C File Offset: 0x00006E3C
		internal bool ReadInlineToken(ObxmlOpcode.OpcodeIds opcode)
		{
			return true;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008C40 File Offset: 0x00006E40
		internal bool ReadNamespaceToken(ObxmlOpcode.OpcodeIds opcode)
		{
			ulong dataLen = this.GetDataLen((short)opcode);
			string tokenName = this.DataStream.ReadUtf8String(dataLen);
			ulong num = this.m_InstructionData[1];
			ObxmlToken token = new ObxmlToken(num, num, tokenName, TokenTypes.NamespaceToken);
			this.m_DecodeState.SetToken(token);
			return true;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008C84 File Offset: 0x00006E84
		internal bool ReadPrefix(ObxmlOpcode.OpcodeIds opcode, out PrefixInfo prefixInfo)
		{
			ulong dataLen = this.GetDataLen((short)opcode);
			string prefix = this.DataStream.ReadUtf8String(dataLen);
			ulong num = this.m_InstructionData[2];
			ulong nsid = this.m_InstructionData[1];
			prefixInfo = new PrefixInfo(this.m_DecodeState.Parent.DecodeContext, (short)num, prefix, nsid, string.Empty);
			this.m_DecodeState.SetPrefix(prefixInfo);
			return true;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008CE8 File Offset: 0x00006EE8
		internal bool ReadToken(ObxmlOpcode.OpcodeIds opcode, out ObxmlToken token)
		{
			token = null;
			ulong dataLen = this.GetDataLen((short)opcode);
			string tokenName = this.DataStream.ReadUtf8String(dataLen);
			ulong tokenId = this.m_InstructionData[2];
			ulong namespaceId = this.m_InstructionData[3];
			bool flag = ((int)this.m_InstructionData[1] & (int)ObxmlInstructionFormat.CSX_DEFQNF_ATTR) > 0;
			token = new ObxmlToken(tokenId, namespaceId, tokenName, flag ? TokenTypes.AttributeToken : TokenTypes.ElementToken);
			this.m_DecodeState.SetToken(token);
			return true;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008D54 File Offset: 0x00006F54
		internal string ReadEventText(ObxmlNodeState elem, ObxmlOpcode.OpcodeIds opcode)
		{
			string text = null;
			char[] array = new char[]
			{
				'a',
				'b'
			};
			ulong dataLen = this.GetDataLen((short)opcode);
			if (this.m_DecodeState.CurrentDataOperation == DataOperationTypes.NoData)
			{
				if (text == null && array != null)
				{
					text = new string(array, 0, (int)dataLen);
				}
				return text;
			}
			byte dataType = this.m_DecodeState.m_CurrentInstruction.DataType;
			if (dataType != 1)
			{
				if (dataType != 4)
				{
					this.DataStream.ReadBytes((int)dataLen);
					text = null;
				}
				else
				{
					int num = (int)dataLen;
					long num2;
					switch (num)
					{
					case 1:
						num2 = (long)((ulong)this.ReadNum1());
						goto IL_E5;
					case 2:
						num2 = (long)this.ReadNum2();
						goto IL_E5;
					case 3:
						break;
					case 4:
						num2 = (long)this.ReadNum4();
						goto IL_E5;
					default:
						if (num == 8)
						{
							num2 = this.DataStream.ReadInt8();
							goto IL_E5;
						}
						break;
					}
					throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.CsxInvalidInstruction, null, ObxmlOpcode.OpcodeIds.None));
					IL_E5:
					text = num2.ToString();
				}
			}
			else
			{
				text = this.DataStream.ReadUtf8String(dataLen);
			}
			this.m_DecodeState.CurrentDataOperation = DataOperationTypes.NoData;
			return text;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008E70 File Offset: 0x00007070
		private void ProcessInstruction(string target, string value)
		{
			try
			{
				this.m_DecodeState.TryAppendingBeginTagClosure(null, false, false);
				this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData.Append("<?");
				this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData.Append(target + " " + value);
				this.m_DecodeState.m_CurrentInstruction.m_TextOrTokenData.Append("?>");
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x040000DE RID: 222
		private static readonly int MaxInstructionDataLen = 10;

		// Token: 0x040000DF RID: 223
		private ObxmlInstructionFormat m_InstructionFormat;

		// Token: 0x040000E0 RID: 224
		private ulong[] m_InstructionData = new ulong[ObxmlInstruction.MaxInstructionDataLen];

		// Token: 0x040000E1 RID: 225
		private ObxmlDecodeState m_DecodeState;
	}
}
