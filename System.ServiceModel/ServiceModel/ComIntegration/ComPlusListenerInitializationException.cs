using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	internal class ComPlusListenerInitializationException : Exception
	{
		// Token: 0x06001083 RID: 4227 RVA: 0x0003D1A0 File Offset: 0x0003B3A0
		public ComPlusListenerInitializationException()
		{
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		public ComPlusListenerInitializationException(string message) : base(message)
		{
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003D1B1 File Offset: 0x0003B3B1
		public ComPlusListenerInitializationException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0003D1BB File Offset: 0x0003B3BB
		protected ComPlusListenerInitializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
