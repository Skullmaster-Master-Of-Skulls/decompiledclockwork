using System;

namespace OracleInternal.BinXml
{
	// Token: 0x02000009 RID: 9
	internal class ObxmlDecodeRequest
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000317C File Offset: 0x0000137C
		internal ObxmlContentObject EncodedContent
		{
			get
			{
				return this.encodedContent;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003184 File Offset: 0x00001384
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000318C File Offset: 0x0000138C
		internal bool IsFullDecode { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003198 File Offset: 0x00001398
		// (set) Token: 0x06000068 RID: 104 RVA: 0x000031A0 File Offset: 0x000013A0
		internal long TextLengthRequested { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x000031AC File Offset: 0x000013AC
		internal ObxmlDecodeRequest()
		{
			this.m_RequestOutput = new ObxmlOutputObject(this);
			this.encodedContent = new ObxmlContentObject(this);
			this.ResetRequestObject();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031D4 File Offset: 0x000013D4
		internal void SetObxmlDecodeRequest(bool isFullDecode, long textLengthRequested = -1L)
		{
			this.IsFullDecode = isFullDecode;
			this.TextLengthRequested = textLengthRequested;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031E4 File Offset: 0x000013E4
		internal bool IsRequestValid()
		{
			return this.TextLengthRequested != 0L && this.EncodedContent.IsContentValid() && this.m_RequestOutput.IsOutputValid();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000320C File Offset: 0x0000140C
		internal bool ResetRequestObject()
		{
			this.EncodedContent.ResetContentObject();
			this.m_RequestOutput.ResetOutputObject();
			this.IsFullDecode = true;
			this.TextLengthRequested = 0L;
			return true;
		}

		// Token: 0x04000042 RID: 66
		private ObxmlContentObject encodedContent;

		// Token: 0x04000043 RID: 67
		internal ObxmlOutputObject m_RequestOutput;

		// Token: 0x04000044 RID: 68
		internal long m_CountRemaining;
	}
}
