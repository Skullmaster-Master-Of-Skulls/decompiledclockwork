using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200051A RID: 1306
	[ComVisible(true)]
	[Serializable]
	public class SEHException : ExternalException
	{
		// Token: 0x060032BE RID: 12990 RVA: 0x000AB5A4 File Offset: 0x000AA5A4
		public SEHException()
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000AB5B7 File Offset: 0x000AA5B7
		public SEHException(string message) : base(message)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000AB5CB File Offset: 0x000AA5CB
		public SEHException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2147467259);
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000AB5E0 File Offset: 0x000AA5E0
		protected SEHException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000AB5EA File Offset: 0x000AA5EA
		public virtual bool CanResume()
		{
			return false;
		}
	}
}
