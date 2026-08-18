using System;
using System.IO;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.I18N;

namespace OracleInternal.BinXml
{
	// Token: 0x02000008 RID: 8
	internal class ObxmlOutputObject
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D88 File Offset: 0x00000F88
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002D90 File Offset: 0x00000F90
		internal long WriteOffset { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002D9C File Offset: 0x00000F9C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002DA4 File Offset: 0x00000FA4
		internal bool OutputObjectOwnedByClient { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002DB0 File Offset: 0x00000FB0
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002DB8 File Offset: 0x00000FB8
		internal InputOutputTypes OutputType
		{
			get
			{
				return this.m_outputContentType;
			}
			set
			{
				if (this.IsOutputContentTypeValid(value))
				{
					this.m_outputContentType = value;
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DCC File Offset: 0x00000FCC
		internal bool IsOutputType(InputOutputTypes outputType)
		{
			return this.OutputType == outputType;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DD8 File Offset: 0x00000FD8
		internal ObxmlOutputObject(ObxmlDecodeRequest parent)
		{
			this.m_Parent = parent;
			this.ResetOutputObject();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002DF8 File Offset: 0x00000FF8
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002E00 File Offset: 0x00001000
		internal OutputEncodingTypes EncodingType { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002E0C File Offset: 0x0000100C
		internal ObxmlTextStream XmlTextStream
		{
			get
			{
				return this.m_xmlTextStream;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002E1C File Offset: 0x0000101C
		internal bool InsideDataSection { get; set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00002E28 File Offset: 0x00001028
		internal void SetObxmlOutputObject(byte[] textBuffer, bool outputOwnedByClient)
		{
			this.OutputType = InputOutputTypes.ByteArray;
			this.m_OutputObject = textBuffer;
			this.OutputObjectOwnedByClient = outputOwnedByClient;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E40 File Offset: 0x00001040
		internal void SetObxmlOutputObject(StringBuilder utf16OutputBuilder, bool outputOwnedByClient)
		{
			this.OutputType = InputOutputTypes.StringBuilder;
			this.m_OutputObject = utf16OutputBuilder;
			this.OutputObjectOwnedByClient = outputOwnedByClient;
			this.EncodingType = OutputEncodingTypes.Utf16;
			this.m_xmlTextStream = new ObxmlTextStream(utf16OutputBuilder);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E6C File Offset: 0x0000106C
		internal void SetObxmlOutputObject(byte[] textBuffer, long offset)
		{
			this.OutputType = InputOutputTypes.ByteArray;
			this.m_OutputObject = textBuffer;
			this.WriteOffset = offset;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E84 File Offset: 0x00001084
		internal void SetObxmlOutputObject(FileStream outputFileStream)
		{
			this.OutputType = InputOutputTypes.FileStream;
			this.m_OutputObject = outputFileStream;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E94 File Offset: 0x00001094
		internal long WriteTextOutput(StringBuilder sbText, int readOffset)
		{
			long num = this.m_Parent.m_CountRemaining / 2L;
			if (num > (long)(sbText.Length - readOffset))
			{
				num = (long)(sbText.Length - readOffset);
			}
			long num2 = num * 2L;
			if (this.IsOutputType(InputOutputTypes.StringBuilder))
			{
				StringBuilder stringBuilder = (StringBuilder)this.m_OutputObject;
				stringBuilder.Append(sbText.ToString(readOffset, (int)num));
				this.m_Parent.m_CountRemaining -= num2;
			}
			return num;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F04 File Offset: 0x00001104
		internal long WriteTextOutput(string text, int readOffset)
		{
			long num = this.m_Parent.m_CountRemaining / 2L;
			if (num > (long)(text.Length - readOffset))
			{
				num = (long)(text.Length - readOffset);
			}
			long num2 = num * 2L;
			if (this.IsOutputType(InputOutputTypes.StringBuilder))
			{
				StringBuilder stringBuilder = (StringBuilder)this.m_OutputObject;
				stringBuilder.Append(text.Substring(readOffset, (int)num));
				this.m_Parent.m_CountRemaining -= num2;
			}
			return num;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F74 File Offset: 0x00001174
		internal long WriteTextOutput(char[] text, int readOffset)
		{
			long num = this.m_Parent.m_CountRemaining / 2L;
			if (num > (long)(text.Length - readOffset))
			{
				num = (long)(text.Length - readOffset);
			}
			if (this.IsOutputType(InputOutputTypes.StringBuilder))
			{
				StringBuilder stringBuilder = (StringBuilder)this.m_OutputObject;
				stringBuilder.Append(text, readOffset, (int)num);
				this.m_Parent.m_CountRemaining -= 2L * num;
				return num;
			}
			throw new OracleException(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, string.Empty, string.Empty, ObxmlDecodeResponse.GetErrorMessage(ObxmlErrorTypes.RequestOutputInvalid, null, ObxmlOpcode.OpcodeIds.None));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal long WriteTextOutput(byte[] text, int readOffset)
		{
			long num = this.m_Parent.m_CountRemaining;
			if (num > (long)(text.Length - readOffset))
			{
				num = (long)(text.Length - readOffset);
			}
			if (this.IsOutputType(InputOutputTypes.StringBuilder))
			{
				StringBuilder stringBuilder = (StringBuilder)this.m_OutputObject;
				if (this.EncodingType == OutputEncodingTypes.Utf16)
				{
					if (this.InsideDataSection)
					{
						this.XmlTextStream.AppendData(text);
						this.InsideDataSection = false;
					}
					else
					{
						string value;
						if (readOffset == 0)
						{
							value = Conv.GetInstance(2000).ConvertBytesToString(text, 0, text.Length, null, true);
						}
						else
						{
							value = Conv.GetInstance(2000).ConvertBytesToString(text, readOffset, (int)num, null, true);
						}
						stringBuilder.Append(value);
					}
				}
				else
				{
					byte[] array = Conv.GetInstance(873).ConvertBytesToUTF16(text, readOffset, (int)num, true);
					string value2 = Conv.GetInstance(2000).ConvertBytesToString(array, 0, array.Length, null, true);
					stringBuilder.Append(value2);
				}
				this.m_Parent.m_CountRemaining -= num;
			}
			return num;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030EC File Offset: 0x000012EC
		internal bool IsOutputContentTypeValid(InputOutputTypes contentType)
		{
			return InputOutputTypes.ByteArray == contentType || InputOutputTypes.StringBuilder == contentType || InputOutputTypes.BinaryXmlUri == contentType || contentType == InputOutputTypes.XmlFilePath;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003100 File Offset: 0x00001300
		internal bool IsOutputValid()
		{
			return this.OutputType != InputOutputTypes.None;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003110 File Offset: 0x00001310
		internal bool ResetOutputObject()
		{
			this.WriteOffset = 0L;
			this.OutputType = InputOutputTypes.None;
			this.EncodingType = OutputEncodingTypes.None;
			if (this.m_xmlTextStream != null)
			{
				this.m_xmlTextStream.Close();
				this.m_xmlTextStream = null;
			}
			if (!this.OutputObjectOwnedByClient)
			{
				StringBuilder stringBuilder = (StringBuilder)this.m_OutputObject;
				if (stringBuilder != null)
				{
					stringBuilder.Clear();
				}
			}
			this.OutputObjectOwnedByClient = false;
			this.m_OutputObject = null;
			return true;
		}

		// Token: 0x04000039 RID: 57
		internal const int s_OutputNumericValueNotSet = -1;

		// Token: 0x0400003A RID: 58
		private InputOutputTypes m_outputContentType = InputOutputTypes.None;

		// Token: 0x0400003B RID: 59
		private ObxmlTextStream m_xmlTextStream;

		// Token: 0x0400003C RID: 60
		internal object m_OutputObject;

		// Token: 0x0400003D RID: 61
		internal ObxmlDecodeRequest m_Parent;
	}
}
