using System;
using System.Runtime;

namespace System.ServiceModel
{
	// Token: 0x0200017C RID: 380
	public class ExceptionMapper
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x00028D08 File Offset: 0x00026F08
		public virtual FaultException FromException(Exception ex)
		{
			return this.FromException(ex, string.Empty, string.Empty);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00028D1B File Offset: 0x00026F1B
		public virtual FaultException FromException(Exception ex, string soapNamespace, string trustNamespace)
		{
			return null;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00028D20 File Offset: 0x00026F20
		public virtual bool HandleSecurityTokenProcessingException(Exception ex)
		{
			if (Fx.IsFatal(ex))
			{
				return false;
			}
			if (ex is FaultException)
			{
				return false;
			}
			FaultException ex2 = this.FromException(ex);
			if (ex2 != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex2);
			}
			return false;
		}

		// Token: 0x04000BF5 RID: 3061
		internal const string SoapSenderFaultCode = "Sender";
	}
}
