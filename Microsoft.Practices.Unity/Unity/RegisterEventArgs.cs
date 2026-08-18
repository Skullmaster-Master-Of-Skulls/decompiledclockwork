using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200007E RID: 126
	public class RegisterEventArgs : NamedEventArgs
	{
		// Token: 0x0600023A RID: 570 RVA: 0x000079D6 File Offset: 0x00005BD6
		public RegisterEventArgs(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager) : base(name)
		{
			this.typeFrom = typeFrom;
			this.typeTo = typeTo;
			this.lifetimeManager = lifetimeManager;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000079F5 File Offset: 0x00005BF5
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000079FD File Offset: 0x00005BFD
		public Type TypeFrom
		{
			get
			{
				return this.typeFrom;
			}
			set
			{
				this.typeFrom = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00007A06 File Offset: 0x00005C06
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00007A0E File Offset: 0x00005C0E
		public Type TypeTo
		{
			get
			{
				return this.typeTo;
			}
			set
			{
				this.typeTo = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00007A17 File Offset: 0x00005C17
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007A1F File Offset: 0x00005C1F
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

		// Token: 0x04000078 RID: 120
		private Type typeFrom;

		// Token: 0x04000079 RID: 121
		private Type typeTo;

		// Token: 0x0400007A RID: 122
		private LifetimeManager lifetimeManager;
	}
}
