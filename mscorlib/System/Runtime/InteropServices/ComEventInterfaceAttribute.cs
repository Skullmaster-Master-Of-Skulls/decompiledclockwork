using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000503 RID: 1283
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		// Token: 0x0600319C RID: 12700 RVA: 0x000A98E1 File Offset: 0x000A88E1
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this._SourceInterface = SourceInterface;
			this._EventProvider = EventProvider;
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000A98F7 File Offset: 0x000A88F7
		public Type SourceInterface
		{
			get
			{
				return this._SourceInterface;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600319E RID: 12702 RVA: 0x000A98FF File Offset: 0x000A88FF
		public Type EventProvider
		{
			get
			{
				return this._EventProvider;
			}
		}

		// Token: 0x040019A6 RID: 6566
		internal Type _SourceInterface;

		// Token: 0x040019A7 RID: 6567
		internal Type _EventProvider;
	}
}
