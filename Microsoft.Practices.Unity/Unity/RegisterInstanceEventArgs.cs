using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200007F RID: 127
	public class RegisterInstanceEventArgs : NamedEventArgs
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00007A28 File Offset: 0x00005C28
		public RegisterInstanceEventArgs()
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007A30 File Offset: 0x00005C30
		public RegisterInstanceEventArgs(Type registeredType, object instance, string name, LifetimeManager lifetimeManager) : base(name)
		{
			this.registeredType = registeredType;
			this.instance = instance;
			this.lifetimeManager = lifetimeManager;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007A4F File Offset: 0x00005C4F
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007A57 File Offset: 0x00005C57
		public Type RegisteredType
		{
			get
			{
				return this.registeredType;
			}
			set
			{
				this.registeredType = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007A60 File Offset: 0x00005C60
		// (set) Token: 0x06000246 RID: 582 RVA: 0x00007A68 File Offset: 0x00005C68
		public object Instance
		{
			get
			{
				return this.instance;
			}
			set
			{
				this.instance = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00007A71 File Offset: 0x00005C71
		// (set) Token: 0x06000248 RID: 584 RVA: 0x00007A79 File Offset: 0x00005C79
		public LifetimeManager LifetimeManager
		{
			get
			{
				return this.lifetimeManager;
			}
			set
			{
				this.lifetimeManager = value;
			}
		}

		// Token: 0x0400007B RID: 123
		private Type registeredType;

		// Token: 0x0400007C RID: 124
		private object instance;

		// Token: 0x0400007D RID: 125
		private LifetimeManager lifetimeManager;
	}
}
