using System;
using System.ServiceModel.Description;
using System.Xml;
using System.Xml.Schema;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x0200001E RID: 30
	internal class ProxyGenerationError
	{
		// Token: 0x06000131 RID: 305 RVA: 0x000049EC File Offset: 0x00002BEC
		public ProxyGenerationError(MetadataConversionError errorMessage)
		{
			this.m_ErrorGeneratorState = ProxyGenerationError.GeneratorState.GenerateCode;
			this.m_IsWarning = errorMessage.IsWarning;
			this.m_Message = errorMessage.Message;
			this.m_MetadataFile = string.Empty;
			this.m_LineNumber = -1;
			this.m_LinePosition = -1;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004A2C File Offset: 0x00002C2C
		public ProxyGenerationError(ProxyGenerationError.GeneratorState generatorState, string fileName, Exception errorException)
		{
			this.m_ErrorGeneratorState = generatorState;
			this.m_IsWarning = false;
			this.m_Message = errorException.Message;
			this.m_MetadataFile = fileName;
			this.m_LineNumber = -1;
			this.m_LinePosition = -1;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004A63 File Offset: 0x00002C63
		public ProxyGenerationError(ProxyGenerationError.GeneratorState generatorState, string fileName, Exception errorException, bool isWarning)
		{
			this.m_ErrorGeneratorState = generatorState;
			this.m_IsWarning = isWarning;
			this.m_Message = errorException.Message;
			this.m_MetadataFile = fileName;
			this.m_LineNumber = -1;
			this.m_LinePosition = -1;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004A9C File Offset: 0x00002C9C
		public ProxyGenerationError(ProxyGenerationError.GeneratorState generatorState, string fileName, XmlException errorException)
		{
			this.m_ErrorGeneratorState = generatorState;
			this.m_IsWarning = false;
			this.m_Message = errorException.Message;
			this.m_MetadataFile = fileName;
			this.m_LineNumber = errorException.LineNumber;
			this.m_LinePosition = errorException.LinePosition;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004AE8 File Offset: 0x00002CE8
		public ProxyGenerationError(ProxyGenerationError.GeneratorState generatorState, string fileName, XmlSchemaException errorException)
		{
			this.m_ErrorGeneratorState = generatorState;
			this.m_IsWarning = false;
			this.m_Message = errorException.Message;
			this.m_MetadataFile = fileName;
			this.m_LineNumber = errorException.LineNumber;
			this.m_LinePosition = errorException.LinePosition;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004B34 File Offset: 0x00002D34
		public ProxyGenerationError(ProxyGenerationError.GeneratorState generatorState, string fileName, XmlSchemaException errorException, bool isWarning)
		{
			this.m_ErrorGeneratorState = generatorState;
			this.m_IsWarning = isWarning;
			this.m_Message = errorException.Message;
			this.m_MetadataFile = fileName;
			this.m_LineNumber = errorException.LineNumber;
			this.m_LinePosition = errorException.LinePosition;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004B81 File Offset: 0x00002D81
		public ProxyGenerationError.GeneratorState ErrorGeneratorState
		{
			get
			{
				return this.m_ErrorGeneratorState;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004B89 File Offset: 0x00002D89
		public bool IsWarning
		{
			get
			{
				return this.m_IsWarning;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004B91 File Offset: 0x00002D91
		public int LineNumber
		{
			get
			{
				return this.m_LineNumber;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004B99 File Offset: 0x00002D99
		public int LinePosition
		{
			get
			{
				return this.m_LinePosition;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004BA1 File Offset: 0x00002DA1
		public string Message
		{
			get
			{
				return this.m_Message;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004BA9 File Offset: 0x00002DA9
		public string MetadataFile
		{
			get
			{
				return this.m_MetadataFile;
			}
		}

		// Token: 0x0400005C RID: 92
		private bool m_IsWarning;

		// Token: 0x0400005D RID: 93
		private string m_Message;

		// Token: 0x0400005E RID: 94
		private string m_MetadataFile;

		// Token: 0x0400005F RID: 95
		private int m_LineNumber;

		// Token: 0x04000060 RID: 96
		private int m_LinePosition;

		// Token: 0x04000061 RID: 97
		private ProxyGenerationError.GeneratorState m_ErrorGeneratorState;

		// Token: 0x0200012E RID: 302
		public enum GeneratorState
		{
			// Token: 0x0400046E RID: 1134
			LoadMetadata,
			// Token: 0x0400046F RID: 1135
			MergeMetadata,
			// Token: 0x04000470 RID: 1136
			GenerateCode
		}
	}
}
