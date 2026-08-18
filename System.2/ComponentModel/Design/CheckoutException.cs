using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005CA RID: 1482
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CheckoutException : ExternalException
	{
		// Token: 0x0600375F RID: 14175 RVA: 0x000F06A2 File Offset: 0x000EE8A2
		public CheckoutException()
		{
		}

		// Token: 0x06003760 RID: 14176 RVA: 0x000F06AA File Offset: 0x000EE8AA
		public CheckoutException(string message) : base(message)
		{
		}

		// Token: 0x06003761 RID: 14177 RVA: 0x000F06B3 File Offset: 0x000EE8B3
		public CheckoutException(string message, int errorCode) : base(message, errorCode)
		{
		}

		// Token: 0x06003762 RID: 14178 RVA: 0x000F06BD File Offset: 0x000EE8BD
		protected CheckoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06003763 RID: 14179 RVA: 0x000F06C7 File Offset: 0x000EE8C7
		public CheckoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x04002AEF RID: 10991
		public static readonly CheckoutException Canceled = new CheckoutException(SR.GetString("CHECKOUTCanceled"), -2147467260);
	}
}
