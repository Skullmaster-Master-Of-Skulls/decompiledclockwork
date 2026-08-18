using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x020006C7 RID: 1735
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class ContextProperty
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000D6FB0 File Offset: 0x000D5FB0
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06003EAF RID: 16047 RVA: 0x000D6FB8 File Offset: 0x000D5FB8
		public virtual object Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x000D6FC0 File Offset: 0x000D5FC0
		internal ContextProperty(string name, object prop)
		{
			this._name = name;
			this._property = prop;
		}

		// Token: 0x04001FE7 RID: 8167
		internal string _name;

		// Token: 0x04001FE8 RID: 8168
		internal object _property;
	}
}
