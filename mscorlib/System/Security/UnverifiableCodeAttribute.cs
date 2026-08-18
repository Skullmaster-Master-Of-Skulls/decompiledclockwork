using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x02000667 RID: 1639
	[AttributeUsage(AttributeTargets.Module, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	public sealed class UnverifiableCodeAttribute : Attribute
	{
	}
}
