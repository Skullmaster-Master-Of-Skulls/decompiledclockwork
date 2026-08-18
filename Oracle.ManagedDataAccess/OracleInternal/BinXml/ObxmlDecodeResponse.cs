using System;
using Oracle.ManagedDataAccess.Client;

namespace OracleInternal.BinXml
{
	// Token: 0x0200000C RID: 12
	internal class ObxmlDecodeResponse
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000032CC File Offset: 0x000014CC
		internal ObxmlDecodeResponse(ObxmlErrorTypes errorType, ObxmlDecodeState parent)
		{
			this.ResetResponseObject(parent, errorType);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032E0 File Offset: 0x000014E0
		internal ObxmlDecodeResponse(ObxmlDecodeState parent)
		{
			this.ResetResponseObject(parent, ObxmlErrorTypes.Success);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000032F4 File Offset: 0x000014F4
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000032FC File Offset: 0x000014FC
		internal byte[] OutputBuffer { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003308 File Offset: 0x00001508
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003310 File Offset: 0x00001510
		internal long CurrentOffset { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000331C File Offset: 0x0000151C
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003324 File Offset: 0x00001524
		internal long DecodedTextLength { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003330 File Offset: 0x00001530
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003338 File Offset: 0x00001538
		internal ObxmlErrorTypes ErrorType { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003344 File Offset: 0x00001544
		internal string ErrorMessage
		{
			get
			{
				return ObxmlDecodeResponse.GetErrorMessage(this.ErrorType, null, ObxmlOpcode.OpcodeIds.None);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003354 File Offset: 0x00001554
		internal bool IsSuccess
		{
			get
			{
				return this.ErrorType == ObxmlErrorTypes.Success;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003360 File Offset: 0x00001560
		internal bool IsDone
		{
			get
			{
				return this.ErrorType == ObxmlErrorTypes.Done;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000336C File Offset: 0x0000156C
		internal void SetSuccess()
		{
			this.ErrorType = ObxmlErrorTypes.Success;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003378 File Offset: 0x00001578
		internal bool ResetResponseObject(ObxmlDecodeState parent = null, ObxmlErrorTypes errorType = ObxmlErrorTypes.Success)
		{
			this.ErrorType = errorType;
			this.OutputBuffer = null;
			this.CurrentOffset = 0L;
			this.DecodedTextLength = 0L;
			this.ObxmlDecodeContext = parent;
			return true;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000033A0 File Offset: 0x000015A0
		internal bool IsResponseValid()
		{
			return this.OutputBuffer != null;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000033B0 File Offset: 0x000015B0
		internal static string GetErrorMessage(ObxmlErrorTypes errorType, Exception ex = null, ObxmlOpcode.OpcodeIds opcodeId = ObxmlOpcode.OpcodeIds.None)
		{
			string text = "Unknown";
			if (opcodeId != ObxmlOpcode.OpcodeIds.None)
			{
				text = opcodeId.ToString();
			}
			if (ex == null)
			{
				return OracleStringResourceManager.GetErrorMesgWithErrCode(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, new string[]
				{
					errorType.ToString(),
					text
				});
			}
			if (errorType == ObxmlErrorTypes.DecodeFailed)
			{
				return OracleStringResourceManager.GetErrorMesgWithErrCode(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, new string[]
				{
					ex.ToString(),
					text
				});
			}
			return OracleStringResourceManager.GetErrorMesgWithErrCode(ResourceStringConstants.XML_TYPE_BINARY_INTERNAL_ERROR, new string[]
			{
				errorType.ToString() + ": " + ex.ToString(),
				text
			});
		}

		// Token: 0x04000067 RID: 103
		internal object ObxmlDecodeContext;
	}
}
