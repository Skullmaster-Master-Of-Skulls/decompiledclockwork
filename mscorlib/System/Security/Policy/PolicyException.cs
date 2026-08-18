using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	// Token: 0x020004AD RID: 1197
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException
	{
		// Token: 0x06002F84 RID: 12164 RVA: 0x000A1673 File Offset: 0x000A0673
		public PolicyException() : base(Environment.GetResourceString("Policy_Default"))
		{
			base.HResult = -2146233322;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000A1690 File Offset: 0x000A0690
		public PolicyException(string message) : base(message)
		{
			base.HResult = -2146233322;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000A16A4 File Offset: 0x000A06A4
		public PolicyException(string message, Exception exception) : base(message, exception)
		{
			base.HResult = -2146233322;
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000A16B9 File Offset: 0x000A06B9
		protected PolicyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000A16C3 File Offset: 0x000A06C3
		internal PolicyException(string message, int hresult) : base(message)
		{
			base.HResult = hresult;
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000A16D3 File Offset: 0x000A06D3
		internal PolicyException(string message, int hresult, Exception exception) : base(message, exception)
		{
			base.HResult = hresult;
		}
	}
}
