using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003E9 RID: 1001
	[Guid("4f33a9a0-5cf4-47a4-b16f-d7faaf17457e")]
	[ComImport]
	internal interface IPropertyChangedEventArgs
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002627 RID: 9767
		string PropertyName { get; }
	}
}
