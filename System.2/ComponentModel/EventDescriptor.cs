using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200054F RID: 1359
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class EventDescriptor : MemberDescriptor
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x000E3548 File Offset: 0x000E1748
		protected EventDescriptor(string name, Attribute[] attrs) : base(name, attrs)
		{
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000E3552 File Offset: 0x000E1752
		protected EventDescriptor(MemberDescriptor descr) : base(descr)
		{
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000E355B File Offset: 0x000E175B
		protected EventDescriptor(MemberDescriptor descr, Attribute[] attrs) : base(descr, attrs)
		{
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600330E RID: 13070
		public abstract Type ComponentType { get; }

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600330F RID: 13071
		public abstract Type EventType { get; }

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003310 RID: 13072
		public abstract bool IsMulticast { get; }

		// Token: 0x06003311 RID: 13073
		public abstract void AddEventHandler(object component, Delegate value);

		// Token: 0x06003312 RID: 13074
		public abstract void RemoveEventHandler(object component, Delegate value);
	}
}
