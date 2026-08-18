using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x02000631 RID: 1585
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeAccessSecurityAttribute : SecurityAttribute
	{
		// Token: 0x06003930 RID: 14640 RVA: 0x000C13A9 File Offset: 0x000C03A9
		protected CodeAccessSecurityAttribute(SecurityAction action) : base(action)
		{
		}
	}
}
