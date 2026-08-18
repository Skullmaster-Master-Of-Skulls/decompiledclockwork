using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleInternal.Common;
using OracleInternal.I18N;

namespace OracleInternal.BinXml
{
	// Token: 0x02000018 RID: 24
	internal class ObxmlDecodeStream : ObxmlDecoder, IDisposable
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00005168 File Offset: 0x00003368
		internal ObxmlDecodeStream()
		{
			base.ResetRequestObject();
			this.m_CountDecoded = 0;
			base.TokenMap = null;
			this.m_InputLength = 0L;
			this.m_DecodeResponse = null;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000051A4 File Offset: 0x000033A4
		internal new bool IsValid
		{
			get
			{
				return base.IsValid;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000051AC File Offset: 0x000033AC
		internal ObxmlDecodeStream(ObxmlDecodeContext decodeContext, OracleBlob blob)
		{
			this.ResetRequestObject(decodeContext, blob);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000051D0 File Offset: 0x000033D0
		internal ObxmlDecodeStream ResetRequestObject(ObxmlDecodeContext decodeContext, OracleBlob blob)
		{
			base.ResetRequestObject();
			this.m_CountDecoded = 0;
			base.SetDecodeContext(decodeContext);
			this.m_Blob = blob;
			this.m_InputLength = 0L;
			this.m_DecodeResponse = null;
			return this;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005200 File Offset: 0x00003400
		internal long CountDecoded
		{
			get
			{
				return (long)this.m_CountDecoded;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000520C File Offset: 0x0000340C
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005214 File Offset: 0x00003414
		internal string DecodedText { get; set; }

		// Token: 0x06000146 RID: 326 RVA: 0x00005220 File Offset: 0x00003420
		internal bool Open(ObxmlDecodeContext decodeContext, byte[] encodedCSX, int inputLength)
		{
			this.m_InputLength = (long)inputLength;
			this.m_CsxBuffer = encodedCSX;
			base.SetDecodeContext(decodeContext);
			if (base.IsValid)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			this.m_RawDecodedText = new byte[ObxmlDecodeStream.DefaultTextBufferSize];
			base.SetRequestObject(false, this.m_CsxOffset, this.m_CsxBuffer, (int)this.m_InputLength, this.m_TextBufferOffset, this.m_RawDecodedText, false, (long)ObxmlDecodeStream.DefaultTextBufferSize);
			return this.m_bInitStreaming = true;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000052B0 File Offset: 0x000034B0
		public new void Dispose()
		{
			this.Close();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000052BC File Offset: 0x000034BC
		~ObxmlDecodeStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000052EC File Offset: 0x000034EC
		internal override bool ResetDecodeState()
		{
			this.m_CountDecoded = 0;
			this.m_RawDecodedText = null;
			this.m_DecodeResponse = null;
			this.m_InputLength = -1L;
			this.m_Blob = null;
			this.m_CsxBuffer = null;
			this.m_CsxOffset = -1L;
			this.m_TextBufferOffset = 0;
			this.m_IsLastSubstring = false;
			this.m_bInitStreaming = false;
			this.m_IsLastSubstring = false;
			this.m_Disposed = false;
			return base.ResetDecodeState();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005358 File Offset: 0x00003558
		protected new virtual void Dispose(bool disposing)
		{
			try
			{
				if (!this.m_Disposed)
				{
					if (disposing)
					{
						base.Dispose();
					}
					this.m_CountDecoded = 0;
					this.m_RawDecodedText = null;
					this.m_Blob = null;
					this.m_TextBufferOffset = 0;
					if (this.m_bInitStreaming)
					{
						this.m_DecodeResponse = null;
						this.m_InputLength = 0L;
						this.m_CsxBuffer = null;
						this.m_CsxOffset = -1L;
						this.m_bInitStreaming = false;
						this.m_IsLastSubstring = false;
					}
					this.m_Disposed = true;
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000053E4 File Offset: 0x000035E4
		internal bool Close()
		{
			bool bInitStreaming = this.m_bInitStreaming;
			this.Dispose(true);
			GC.SuppressFinalize(this);
			return bInitStreaming;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005408 File Offset: 0x00003608
		internal string DecodeNext(int numCharsRequested)
		{
			if (!this.m_bInitStreaming)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.NotOpenForStreaming, null, ObxmlOpcode.OpcodeIds.None));
			}
			base.RequestObject.TextLengthRequested = (long)numCharsRequested;
			base.RequestObject.EncodedContent.ReadOffset = this.m_CsxOffset;
			base.RequestObject.m_RequestOutput.WriteOffset = 0L;
			if (this.m_DecodeResponse != null)
			{
				base.RequestObject.EncodedContent.ReadOffset = this.m_DecodeResponse.CurrentOffset;
			}
			else
			{
				base.RequestObject.EncodedContent.ReadOffset = 0L;
			}
			this.m_DecodeResponse = base.Decode_New(base.RequestObject);
			if (this.m_IsLastSubstring)
			{
				return string.Empty;
			}
			if (this.m_DecodeResponse.ErrorType == ObxmlErrorTypes.Done)
			{
				this.m_IsLastSubstring = true;
			}
			else if (!this.m_DecodeResponse.IsSuccess)
			{
				return null;
			}
			Encoding @default = Encoding.Default;
			this.DecodedText = string.Empty;
			this.m_CountDecoded = (int)this.m_DecodeResponse.DecodedTextLength;
			string @string = Encoding.Default.GetString(this.m_DecodeResponse.OutputBuffer, 0, this.m_CountDecoded);
			if (this.m_CountDecoded <= 0)
			{
				return string.Empty;
			}
			return this.DecodedText = @string;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005548 File Offset: 0x00003748
		private int Decode(byte[] textBuffer, int numCharsToDecode, long textBufferOffset, long csxOffset)
		{
			base.RequestObject.m_RequestOutput.SetObxmlOutputObject(textBuffer, textBufferOffset);
			base.RequestObject.TextLengthRequested = (long)numCharsToDecode;
			base.RequestObject.EncodedContent.ReadOffset = csxOffset;
			return (int)base.Decode_New(base.RequestObject).DecodedTextLength;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005598 File Offset: 0x00003798
		internal string Decode(byte[] buffer, int inputLength, int numCharsRequested)
		{
			if (!this.IsValid)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			this.m_InputLength = (long)inputLength;
			this.m_RawDecodedText = new byte[numCharsRequested];
			base.SetRequestObject(false, this.m_CsxOffset, buffer, (int)this.m_InputLength, this.m_TextBufferOffset, this.m_RawDecodedText, false, (long)numCharsRequested);
			Encoding @default = Encoding.Default;
			this.DecodedText = string.Empty;
			this.m_CountDecoded = 0;
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			while ((num = this.Decode(this.m_RawDecodedText, numCharsRequested, (long)this.m_TextBufferOffset, this.m_CsxOffset)) != 0)
			{
				string value = Conv.GetInstance(2000).ConvertBytesToString(this.m_RawDecodedText, 0, num, null, true);
				stringBuilder.Append(value);
				this.m_CountDecoded += num;
				if (num <= 0)
				{
					break;
				}
			}
			this.DecodedText = stringBuilder.ToString();
			return this.DecodedText;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000568C File Offset: 0x0000388C
		internal string Decode(byte[] buffer, int inputLength)
		{
			if (!this.IsValid)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			this.m_InputLength = (long)inputLength;
			StringBuilder stringBuilder = new StringBuilder();
			base.SetRequestObject(stringBuilder, false, this.m_CsxOffset, buffer, (int)this.m_InputLength);
			this.DecodedText = string.Empty;
			ObxmlDecodeResponse obxmlDecodeResponse = base.Decode();
			if (obxmlDecodeResponse != null && (obxmlDecodeResponse.IsSuccess || obxmlDecodeResponse.IsDone))
			{
				this.DecodedText = stringBuilder.ToString();
				this.HandleCompleteParse();
			}
			return this.DecodedText;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005720 File Offset: 0x00003920
		internal void HandleCompleteParse()
		{
			if (ConfigBaseClass.m_XMLTypeParseAllXml)
			{
				if (!string.IsNullOrEmpty(this.DecodedText))
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
						{
							"DECODE Full Parse START context id" + base.ContextId + " time-stamp " + base.GetTimeStamp(true)
						});
					}
					XDocument xdocument = XDocument.Parse(this.DecodedText);
					XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
					xmlWriterSettings.Indent = true;
					xmlWriterSettings.Encoding = Encoding.Unicode;
					StringBuilder stringBuilder = new StringBuilder();
					using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
					{
						xdocument.WriteTo(xmlWriter);
					}
					this.DecodedText = stringBuilder.ToString();
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777728, new string[]
						{
							string.Concat(new string[]
							{
								"DECODE Full Parse Done context id",
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
				return;
			}
			if (ConfigBaseClass.m_XMLTypeParseXml && !string.IsNullOrEmpty(this.DecodedText) && base.DecodeState.PerformFullXmlParse)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
					{
						"DECODE Full Parse START context id" + base.ContextId + " time-stamp " + base.GetTimeStamp(true)
					});
				}
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
				xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
				xmlReaderSettings.ProhibitDtd = false;
				StringReader input = new StringReader(this.DecodedText);
				XmlReader xmlReader = XmlReader.Create(input, xmlReaderSettings);
				XmlWriterSettings xmlWriterSettings2 = new XmlWriterSettings();
				xmlWriterSettings2.Indent = true;
				xmlWriterSettings2.NamespaceHandling = NamespaceHandling.OmitDuplicates;
				xmlWriterSettings2.ConformanceLevel = ConformanceLevel.Document;
				xmlWriterSettings2.Encoding = Encoding.Unicode;
				StringBuilder stringBuilder2 = new StringBuilder();
				using (XmlWriter xmlWriter2 = XmlWriter.Create(stringBuilder2, xmlWriterSettings2))
				{
					xmlWriter2.WriteNode(xmlReader, true);
				}
				xmlReader.Close();
				this.DecodedText = stringBuilder2.ToString();
				stringBuilder2.Clear();
				stringBuilder2 = null;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777728, new string[]
					{
						string.Concat(new string[]
						{
							"DECODE Full Parse Done context id",
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
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005A1C File Offset: 0x00003C1C
		internal StringBuilder DecodeBlobForXmlStream()
		{
			if (this.m_Blob == null || !this.IsValid)
			{
				throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.InvalidArguments, null, ObxmlOpcode.OpcodeIds.None));
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777472, new string[]
				{
					"DECODE BLOB START context id" + base.ContextId + " time-stamp " + base.GetTimeStamp(true)
				});
			}
			byte[] value = this.m_Blob.Value;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)16777728, new string[]
				{
					string.Concat(new string[]
					{
						"DECODE BLOB START (buffer created) context id",
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
			this.m_InputLength = (long)value.Length;
			StringBuilder stringBuilder = new StringBuilder();
			base.SetRequestObject(stringBuilder, true, this.m_CsxOffset, value, (int)this.m_InputLength);
			ObxmlDecodeResponse obxmlDecodeResponse = base.Decode();
			if (obxmlDecodeResponse != null && (obxmlDecodeResponse.IsSuccess || obxmlDecodeResponse.IsDone))
			{
				return stringBuilder;
			}
			return null;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005B6C File Offset: 0x00003D6C
		internal string DecodeBlob()
		{
			this.DecodedText = string.Empty;
			StringBuilder stringBuilder;
			if ((stringBuilder = this.DecodeBlobForXmlStream()) != null)
			{
				this.DecodedText = stringBuilder.ToString();
				this.HandleCompleteParse();
			}
			return this.DecodedText;
		}

		// Token: 0x040000D0 RID: 208
		private int m_CountDecoded;

		// Token: 0x040000D1 RID: 209
		private bool m_bInitStreaming;

		// Token: 0x040000D2 RID: 210
		private byte[] m_RawDecodedText;

		// Token: 0x040000D3 RID: 211
		private ObxmlDecodeResponse m_DecodeResponse;

		// Token: 0x040000D4 RID: 212
		private long m_InputLength = -1L;

		// Token: 0x040000D5 RID: 213
		private OracleBlob m_Blob;

		// Token: 0x040000D6 RID: 214
		private byte[] m_CsxBuffer;

		// Token: 0x040000D7 RID: 215
		private long m_CsxOffset = -1L;

		// Token: 0x040000D8 RID: 216
		private int m_TextBufferOffset;

		// Token: 0x040000D9 RID: 217
		private bool m_IsLastSubstring;

		// Token: 0x040000DA RID: 218
		private bool m_Disposed;

		// Token: 0x040000DB RID: 219
		internal static readonly int DefaultChunkSize = 5;

		// Token: 0x040000DC RID: 220
		internal static readonly int DefaultTextBufferSize = 16384;
	}
}
