using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000014 RID: 20
	public class ChildContainerCreatedEventArgs : EventArgs
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000029F0 File Offset: 0x00000BF0
		public ChildContainerCreatedEventArgs(ExtensionContext childContext)
		{
			this.ChildContext = childContext;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000029FF File Offset: 0x00000BFF
		public IUnityContainer ChildContainer
		{
			get
			{
				return this.ChildContext.Container;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002A0C File Offset: 0x00000C0C
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002A14 File Offset: 0x00000C14
		public ExtensionContext ChildContext { get; private set; }
	}
}
