using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000666 RID: 1638
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
	{
	}
}
