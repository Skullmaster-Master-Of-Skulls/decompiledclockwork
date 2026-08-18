using System;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003BD RID: 957
	internal static class MsmqMessageSerializationFormatHelper
	{
		// Token: 0x060023D8 RID: 9176 RVA: 0x00082707 File Offset: 0x00080907
		internal static bool IsDefined(MsmqMessageSerializationFormat value)
		{
			return value == MsmqMessageSerializationFormat.ActiveX || value == MsmqMessageSerializationFormat.Binary || value == MsmqMessageSerializationFormat.ByteArray || value == MsmqMessageSerializationFormat.Stream || value == MsmqMessageSerializationFormat.Xml;
		}
	}
}
