using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	internal class WrappedDispatcherException : SystemException
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0001E540 File Offset: 0x0001C740
		public WrappedDispatcherException()
		{
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001E548 File Offset: 0x0001C748
		public WrappedDispatcherException(string message) : base(message)
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001E551 File Offset: 0x0001C751
		public WrappedDispatcherException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001E55B File Offset: 0x0001C75B
		public WrappedDispatcherException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
