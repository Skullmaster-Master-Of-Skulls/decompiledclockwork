using System;
using System.Collections.Generic;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;

namespace OracleInternal.BinXml
{
	// Token: 0x02000004 RID: 4
	internal class ObxmlDecoder : ObxmlDecodeContext, IDisposable
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000022B4 File Offset: 0x000004B4
		internal static Dictionary<short, string> ReservedNSTable { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000022C4 File Offset: 0x000004C4
		internal static Dictionary<short, string> ReservedPrefixTable { get; set; } = new Dictionary<short, string>();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000022D4 File Offset: 0x000004D4
		internal static Dictionary<short, string> ReservedNSPrefixTable { get; set; }

		// Token: 0x06000020 RID: 32 RVA: 0x000022DC File Offset: 0x000004DC
		static ObxmlDecoder()
		{
			ObxmlDecoder.ReservedPrefixTable[1] = "xml";
			ObxmlDecoder.ReservedPrefixTable[2] = "xmlns";
			ObxmlDecoder.ReservedPrefixTable[3] = "xsi";
			ObxmlDecoder.ReservedPrefixTable[4] = "xsd";
			ObxmlDecoder.ReservedPrefixTable[5] = "xs";
			ObxmlDecoder.ReservedPrefixTable[6] = "csx";
			ObxmlDecoder.ReservedNSTable = new Dictionary<short, string>();
			ObxmlDecoder.ReservedNSTable[1] = "http://www.w3.org/XML/1998/namespace";
			ObxmlDecoder.ReservedNSTable[2] = "http://www.w3.org/XML/2000/xmlns/";
			ObxmlDecoder.ReservedNSTable[3] = "http://www.w3.org/2001/XMLSchema-instance";
			ObxmlDecoder.ReservedNSTable[4] = "http://www.w3.org/2001/XMLSchema";
			ObxmlDecoder.ReservedNSTable[5] = "http://xmlns.oracle.com/2004/csx";
			ObxmlDecoder.ReservedNSTable[6] = "http://xmlns.oracle.com/xdb";
			ObxmlDecoder.ReservedNSTable[7] = null;
			ObxmlDecoder.ReservedNSPrefixTable = new Dictionary<short, string>();
			ObxmlDecoder.ReservedNSPrefixTable[1] = "xml";
			ObxmlDecoder.ReservedNSPrefixTable[2] = "xmlns";
			ObxmlDecoder.ReservedNSPrefixTable[3] = "xsi";
			ObxmlDecoder.ReservedNSPrefixTable[4] = "xsd";
			ObxmlDecoder.ReservedNSPrefixTable[5] = "csx";
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024B0 File Offset: 0x000006B0
		internal ObxmlDecoder(char[] encodedArray)
		{
			this.InitState(false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024C8 File Offset: 0x000006C8
		internal ObxmlDecoder()
		{
			this.InitState(false);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000024E0 File Offset: 0x000006E0
		protected ObxmlDecodeRequest RequestObject
		{
			get
			{
				if (this.m_DecodeState != null)
				{
					return this.m_DecodeState.m_RequestObject;
				}
				return null;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000024F8 File Offset: 0x000006F8
		private ObxmlInstructionState CurrentInstruction
		{
			get
			{
				return this.m_DecodeState.m_CurrentInstruction;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002508 File Offset: 0x00000708
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002520 File Offset: 0x00000720
		private ObxmlStream DataStream
		{
			get
			{
				if (this.m_DecodeState == null)
				{
					return null;
				}
				return this.m_DecodeState.m_BinXmlStream;
			}
			set
			{
				if (this.m_DecodeState != null)
				{
					this.m_DecodeState.m_BinXmlStream = value;
					return;
				}
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.DecodeStateInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002554 File Offset: 0x00000754
		private void InitState(bool resetting = false)
		{
			if (!resetting)
			{
				this.m_DecodeState = new ObxmlDecodeState();
			}
			this.m_DecodeState.SetObxmlDecodeState(this, null, null);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002574 File Offset: 0x00000774
		private bool IsDecoderValid()
		{
			return base.IsValid && this.m_DecodeState != null && this.m_DecodeState.TokenMap != null && this.m_DecodeState.IsDecoderStateValid();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025A0 File Offset: 0x000007A0
		private bool InitRequestObject()
		{
			try
			{
				this.DataStream = new ObxmlStream(this.RequestObject.EncodedContent);
				this.m_IsDone = false;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			return true;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025F0 File Offset: 0x000007F0
		internal PrefixInfo GetReservedPrefixInfo(short pfxid)
		{
			string prefix = null;
			if (ObxmlDecoder.ReservedPrefixTable.ContainsKey(pfxid))
			{
				prefix = ObxmlDecoder.ReservedPrefixTable[pfxid];
			}
			long nsid;
			switch (pfxid)
			{
			case 1:
				nsid = 1L;
				break;
			case 2:
				nsid = 2L;
				break;
			case 3:
				nsid = 3L;
				break;
			case 4:
				nsid = 4L;
				break;
			case 5:
				nsid = 4L;
				break;
			case 6:
				nsid = 5L;
				break;
			default:
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.TokenInvalidPrefix, null, ObxmlOpcode.OpcodeIds.None));
			}
			return new PrefixInfo(this.DecodeContext, pfxid, prefix, (ulong)nsid, null);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000268C File Offset: 0x0000088C
		internal ObxmlDecodeState DecodeState
		{
			get
			{
				return this.m_DecodeState;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002694 File Offset: 0x00000894
		internal void GetNextInstruction()
		{
			if (this.m_DecodeState == null)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.DecodeStateInvalid, null, ObxmlOpcode.OpcodeIds.None));
			}
			try
			{
				if (this.m_Instruction == null)
				{
					this.m_Instruction = new ObxmlInstruction(this.m_DecodeState);
				}
				else
				{
					this.m_Instruction.ResetObxmlInstruction(this.m_DecodeState);
				}
				this.m_Instruction.ReadInstructionInfo(true);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002724 File Offset: 0x00000924
		internal void SetDecodeComplete()
		{
			this.m_DecodeState.SetDecodeState(DecodeStates.SectionEnd, false);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777728, new string[]
				{
					"DECODE Completed Successfully..Exiting"
				});
			}
			this.m_IsDone = true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000276C File Offset: 0x0000096C
		internal bool ResetRequestObject()
		{
			if (this.m_DecodeState != null && this.RequestObject != null)
			{
				this.RequestObject.ResetRequestObject();
				this.m_IsDone = false;
				return true;
			}
			return false;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002794 File Offset: 0x00000994
		internal override bool ResetDecodeState()
		{
			this.m_IsDone = true;
			this.Dispose();
			this.m_DecodeState.ClearStateObject();
			bool result = base.ResetDecodeState();
			this.InitState(true);
			this.m_Disposed = false;
			return result;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027D0 File Offset: 0x000009D0
		internal ObxmlDecoder SetRequestObject(bool isFullDecode, long readOffset, byte[] inputBuffer, int inputLength, int writeOffset, byte[] outputBuffer, bool outputOwnedByClient, long textLengthRequested)
		{
			this.RequestObject.IsFullDecode = isFullDecode;
			this.RequestObject.TextLengthRequested = textLengthRequested;
			this.RequestObject.EncodedContent.ReadOffset = readOffset;
			this.RequestObject.EncodedContent.InputLength = (long)inputLength;
			this.RequestObject.EncodedContent.SetObxmlContentObject(inputBuffer);
			this.RequestObject.m_RequestOutput.WriteOffset = (long)writeOffset;
			this.RequestObject.m_RequestOutput.SetObxmlOutputObject(outputBuffer, outputOwnedByClient);
			this.InitRequestObject();
			return this;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000285C File Offset: 0x00000A5C
		internal ObxmlDecoder SetRequestObject(StringBuilder utf16OutPutBuilder, bool outputOwnedByClient, long readOffset, byte[] inputBuffer, int inputLength)
		{
			this.RequestObject.IsFullDecode = true;
			this.RequestObject.TextLengthRequested = long.MaxValue;
			this.RequestObject.EncodedContent.ReadOffset = readOffset;
			this.RequestObject.EncodedContent.InputLength = (long)inputLength;
			this.RequestObject.EncodedContent.SetObxmlContentObject(inputBuffer);
			this.RequestObject.m_RequestOutput.SetObxmlOutputObject(utf16OutPutBuilder, outputOwnedByClient);
			this.InitRequestObject();
			return this;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028DC File Offset: 0x00000ADC
		internal ObxmlDecoder SetRequestObject(bool isFullDecode, long readOffset, OracleBlob blob, int writeOffset, byte[] outputBuffer, bool outputOwnedByClient, long textLengthRequested)
		{
			this.RequestObject.IsFullDecode = isFullDecode;
			this.RequestObject.TextLengthRequested = textLengthRequested;
			this.RequestObject.EncodedContent.ReadOffset = readOffset;
			this.RequestObject.EncodedContent.InputLength = -1L;
			this.RequestObject.EncodedContent.SetObxmlContentObject(blob);
			this.RequestObject.m_RequestOutput.WriteOffset = (long)writeOffset;
			this.RequestObject.m_RequestOutput.SetObxmlOutputObject(outputBuffer, outputOwnedByClient);
			this.InitRequestObject();
			return this;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002968 File Offset: 0x00000B68
		internal ObxmlDecodeContext DecodeContext
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000296C File Offset: 0x00000B6C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000297C File Offset: 0x00000B7C
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_Disposed)
			{
				if (disposing)
				{
					this.m_Instruction = null;
					if (this.DataStream != null)
					{
						this.DataStream.Dispose();
						this.DataStream = null;
					}
				}
				this.m_Disposed = true;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029B4 File Offset: 0x00000BB4
		internal ObxmlDecodeResponse Decode()
		{
			return this.Decode_New(this.RequestObject);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029C4 File Offset: 0x00000BC4
		internal ObxmlDecodeResponse Decode_New(ObxmlDecodeRequest request)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
				{
					string.Concat(new string[]
					{
						"DECODE START context id",
						base.ContextId,
						" time-stamp ",
						base.GetTimeStamp(true),
						" CacheSize ",
						base.CacheSizeString,
						base.PerformanceCounterString
					})
				});
			}
			ObxmlDecodeResponse result;
			try
			{
				ObxmlDecodeResponse lastDecodeResult = this.m_DecodeState.LastDecodeResult;
				if (!this.IsDecoderValid())
				{
					lastDecodeResult.ErrorType = ObxmlErrorTypes.InvalidArguments;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)285212672, new string[]
						{
							"DECODE Request Invalid"
						});
					}
					result = lastDecodeResult;
				}
				else
				{
					ObxmlOutputObject requestOutput = request.m_RequestOutput;
					if ((int)request.TextLengthRequested != -1 && !request.IsFullDecode)
					{
						request.m_CountRemaining = request.TextLengthRequested;
					}
					else if (request.IsFullDecode)
					{
						request.m_CountRemaining = request.TextLengthRequested;
					}
					else
					{
						request.m_CountRemaining = (long)this.DefaultDecodeBufferLength;
					}
					while (request.m_CountRemaining > 0L && !this.m_IsDone)
					{
						if (this.m_Instruction == null || !this.m_Instruction.InstructionPending)
						{
							this.GetNextInstruction();
						}
						if (this.m_Instruction.DecodeInstruction_New() && request.m_CountRemaining <= 0L)
						{
							break;
						}
					}
					this.HandleDecodeComplete_New();
					result = lastDecodeResult;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)285212672, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777728, new string[]
					{
						string.Concat(new string[]
						{
							"DECODE Done/Exiting  context id",
							base.ContextId,
							" time-stamp ",
							base.GetTimeStamp(false),
							" CacheSize ",
							base.CacheSizeString,
							base.PerformanceCounterString,
							" Duration ",
							base.DecodeTimeStamp
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private int HandleDecodeComplete_New()
		{
			byte[] outputBuffer = null;
			ObxmlOutputObject requestOutput = this.RequestObject.m_RequestOutput;
			if (requestOutput.IsOutputType(InputOutputTypes.ByteArray))
			{
				outputBuffer = (byte[])requestOutput.m_OutputObject;
			}
			ObxmlDecodeResponse lastDecodeResult = this.m_DecodeState.LastDecodeResult;
			lastDecodeResult.DecodedTextLength = this.RequestObject.TextLengthRequested - this.RequestObject.m_CountRemaining;
			lastDecodeResult.OutputBuffer = outputBuffer;
			lastDecodeResult.CurrentOffset = this.m_DecodeState.m_BinXmlStream.Position;
			if (this.m_IsDone)
			{
				lastDecodeResult.ErrorType = ObxmlErrorTypes.Done;
			}
			return (int)lastDecodeResult.DecodedTextLength;
		}

		// Token: 0x0400000D RID: 13
		private bool m_Disposed;

		// Token: 0x0400000E RID: 14
		private ObxmlDecodeState m_DecodeState;

		// Token: 0x0400000F RID: 15
		private ObxmlInstruction m_Instruction;

		// Token: 0x04000010 RID: 16
		private int DefaultDecodeBufferLength = 100;

		// Token: 0x04000011 RID: 17
		internal static readonly string nameXML = "xml";

		// Token: 0x04000012 RID: 18
		internal static readonly string nameXSLPI = "xml-stylesheet";

		// Token: 0x04000013 RID: 19
		internal static readonly string nameXMLSpace = "xml:space";

		// Token: 0x04000014 RID: 20
		internal static readonly string nameXMLLang = "xml:lang";

		// Token: 0x04000015 RID: 21
		internal static readonly string nameNamespace = "xmlns";

		// Token: 0x04000016 RID: 22
		internal static readonly string nameXMLNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000017 RID: 23
		internal static readonly string DEFAULT_PREFIX = "#default";

		// Token: 0x04000018 RID: 24
		internal static readonly string nameXMLNSNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000019 RID: 25
		internal static readonly string nameDOCUMENTFRAGMENT = "#document-fragment";

		// Token: 0x0400001A RID: 26
		internal static readonly string nameDOCUMENT = "#document";

		// Token: 0x0400001B RID: 27
		internal static readonly string nameTEXT = "#text";

		// Token: 0x0400001C RID: 28
		internal static readonly string nameCOMMENT = "#comment";

		// Token: 0x0400001D RID: 29
		internal static readonly string nameCDATA = "#cdata-section";

		// Token: 0x0400001E RID: 30
		internal static readonly string XMLSCHEMA = "http://www.w3.org/2001/XMLSchema";
	}
}
