using System;
using System.Runtime.InteropServices;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000108 RID: 264
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct OpoAQMsgValCtx
	{
		// Token: 0x0400088A RID: 2186
		internal unsafe OpoUdtValCtx* pOpoUdtValCtx;

		// Token: 0x0400088B RID: 2187
		public IntPtr pRefTDO;

		// Token: 0x0400088C RID: 2188
		internal int payloadType;

		// Token: 0x0400088D RID: 2189
		internal int rawPayloadLen;

		// Token: 0x0400088E RID: 2190
		internal IntPtr pMsgId;

		// Token: 0x0400088F RID: 2191
		internal int msgIdLen;

		// Token: 0x04000890 RID: 2192
		internal IntPtr pMsgIdObject;

		// Token: 0x04000891 RID: 2193
		internal IntPtr pPayloadObject;

		// Token: 0x04000892 RID: 2194
		internal IntPtr pPayloadOut;

		// Token: 0x04000893 RID: 2195
		internal IntPtr pXmlPayload;

		// Token: 0x04000894 RID: 2196
		internal int isXmlOrUDTNull;
	}
}
