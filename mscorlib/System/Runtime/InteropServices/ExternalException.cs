using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200050B RID: 1291
	[ComVisible(true)]
	[Serializable]
	public class ExternalException : SystemException
	{
		// Token: 0x060031AC RID: 12716 RVA: 0x000A99A8 File Offset: 0x000A89A8
		public ExternalException() : base(Environment.GetResourceString("Arg_ExternalException"))
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000A99C5 File Offset: 0x000A89C5
		public ExternalException(string message) : base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000A99D9 File Offset: 0x000A89D9
		public ExternalException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000A99EE File Offset: 0x000A89EE
		public ExternalException(string message, int errorCode) : base(message)
		{
			base.SetErrorCode(errorCode);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000A99FE File Offset: 0x000A89FE
		protected ExternalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x000A9A08 File Offset: 0x000A8A08
		public virtual int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}
	}
}
