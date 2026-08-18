using System;

namespace TechnoPro.Common.WCF.Attributes
{
	// Token: 0x02000018 RID: 24
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
	public class NoSslCertificateAttribute : BindingServiceAttribute
	{
	}
}
