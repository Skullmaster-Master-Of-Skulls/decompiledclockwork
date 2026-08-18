using System;
using System.Design;
using System.Runtime.Serialization;

namespace System.Data.Design
{
	// Token: 0x0200024D RID: 589
	[Serializable]
	internal class InternalException : Exception, ISerializable
	{
		// Token: 0x060016AF RID: 5807 RVA: 0x0007CD1B File Offset: 0x0007AF1B
		internal InternalException(string internalMessage) : this(internalMessage, null)
		{
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0007CD25 File Offset: 0x0007AF25
		internal InternalException(string internalMessage, Exception innerException) : this(innerException, internalMessage, -1, false)
		{
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0007CD31 File Offset: 0x0007AF31
		internal InternalException(string internalMessage, int errorCode) : this(null, internalMessage, errorCode, false)
		{
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0007CD3D File Offset: 0x0007AF3D
		internal InternalException(string internalMessage, int errorCode, bool showTextOnReport) : this(null, internalMessage, errorCode, showTextOnReport)
		{
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0007CD49 File Offset: 0x0007AF49
		internal InternalException(Exception innerException, string internalMessage, int errorCode, bool showErrorMesageOnReport) : this(innerException, internalMessage, errorCode, showErrorMesageOnReport, true)
		{
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0007CD57 File Offset: 0x0007AF57
		internal InternalException(Exception innerException, string internalMessage, int errorCode, bool showErrorMesageOnReport, bool needAssert)
		{
			this.internalMessage = string.Empty;
			this.errorCode = -1;
			base..ctor(SR.GetString("ERR_INTERNAL"), innerException);
			this.errorCode = errorCode;
			this.showErrorMesageOnReport = showErrorMesageOnReport;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0007CD90 File Offset: 0x0007AF90
		private InternalException(SerializationInfo info, StreamingContext context)
		{
			this.internalMessage = string.Empty;
			this.errorCode = -1;
			base..ctor(info, context);
			this.internalMessage = info.GetString("InternalMessage");
			this.errorCode = info.GetInt32("ErrorCode");
			this.showErrorMesageOnReport = info.GetBoolean("ShowErrorMesageOnReport");
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0007CDEA File Offset: 0x0007AFEA
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("InternalMessage", this.internalMessage);
			info.AddValue("ErrorCode", this.errorCode);
			info.AddValue("ShowErrorMesageOnReport", this.showErrorMesageOnReport);
			base.GetObjectData(info, context);
		}

		// Token: 0x04000B95 RID: 2965
		private const string internalExceptionMessageID = "ERR_INTERNAL";

		// Token: 0x04000B96 RID: 2966
		private string internalMessage;

		// Token: 0x04000B97 RID: 2967
		private bool showErrorMesageOnReport;

		// Token: 0x04000B98 RID: 2968
		private int errorCode;
	}
}
