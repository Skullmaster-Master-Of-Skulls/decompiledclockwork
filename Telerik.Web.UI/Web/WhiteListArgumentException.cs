using System;

namespace Telerik.Web
{
	// Token: 0x020001C9 RID: 457
	internal class WhiteListArgumentException : ArgumentException
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x0003D179 File Offset: 0x0003B379
		public WhiteListArgumentException(string assemblyName) : base(string.Format("The target assembly {0} is not in the assembly white list. Please make sure that you have the assembly added to the assembly white list", assemblyName))
		{
		}
	}
}
