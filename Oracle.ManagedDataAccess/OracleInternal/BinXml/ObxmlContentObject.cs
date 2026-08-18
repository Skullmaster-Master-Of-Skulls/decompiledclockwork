using System;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.BinXml
{
	// Token: 0x02000007 RID: 7
	internal class ObxmlContentObject
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C6C File Offset: 0x00000E6C
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002C74 File Offset: 0x00000E74
		internal long ReadOffset { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002C80 File Offset: 0x00000E80
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002C88 File Offset: 0x00000E88
		internal long InputLength { get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002C94 File Offset: 0x00000E94
		internal ObxmlContentObject(ObxmlDecodeRequest parent)
		{
			this.Parent = parent;
			this.ResetContentObject();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CB4 File Offset: 0x00000EB4
		internal void SetObxmlContentObject(OracleBlob blob)
		{
			this.InputType = InputOutputTypes.OracleBlob;
			this.ContentObject = blob;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CC4 File Offset: 0x00000EC4
		internal void SetObxmlContentObject(InputOutputTypes contentType, string urlOrPathOrId)
		{
			this.InputType = contentType;
			this.StreamId = urlOrPathOrId;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002CD4 File Offset: 0x00000ED4
		internal void SetObxmlContentObject(byte[] binArray)
		{
			this.InputType = InputOutputTypes.ByteArray;
			this.ContentObject = binArray;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002CE4 File Offset: 0x00000EE4
		internal bool IsInputContentTypeValid(InputOutputTypes contentType)
		{
			return InputOutputTypes.OracleBlob == contentType || InputOutputTypes.ByteArray == contentType;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002CF4 File Offset: 0x00000EF4
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002CFC File Offset: 0x00000EFC
		internal InputOutputTypes InputType
		{
			get
			{
				return this.inputContentType;
			}
			set
			{
				if (this.IsInputContentTypeValid(value))
				{
					this.inputContentType = value;
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002D10 File Offset: 0x00000F10
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002D18 File Offset: 0x00000F18
		internal string StreamId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D24 File Offset: 0x00000F24
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002D2C File Offset: 0x00000F2C
		internal object ContentObject { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D38 File Offset: 0x00000F38
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002D40 File Offset: 0x00000F40
		internal ObxmlDecodeRequest Parent { get; set; }

		// Token: 0x0600004A RID: 74 RVA: 0x00002D4C File Offset: 0x00000F4C
		internal bool IsContentValid()
		{
			return this.InputType != InputOutputTypes.None;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D5C File Offset: 0x00000F5C
		internal bool ResetContentObject()
		{
			this.ContentObject = null;
			this.StreamId = null;
			this.InputType = InputOutputTypes.None;
			this.ReadOffset = -1L;
			this.InputLength = -1L;
			return true;
		}

		// Token: 0x04000032 RID: 50
		internal const int s_InputNumericValueNotSet = -1;

		// Token: 0x04000033 RID: 51
		private InputOutputTypes inputContentType = InputOutputTypes.None;
	}
}
