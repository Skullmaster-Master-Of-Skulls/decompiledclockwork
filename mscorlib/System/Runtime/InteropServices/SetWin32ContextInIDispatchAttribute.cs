using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000508 RID: 1288
	[ComVisible(true)]
	[Obsolete("This attribute has been deprecated.  Application Domains no longer respect Activation Context boundaries in IDispatch calls.", false)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class SetWin32ContextInIDispatchAttribute : Attribute
	{
	}
}
