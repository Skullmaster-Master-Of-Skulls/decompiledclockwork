using System;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x0200001E RID: 30
	public abstract class LifetimeManager : ILifetimePolicy, IBuilderPolicy
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003025 File Offset: 0x00001225
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000302D File Offset: 0x0000122D
		internal bool InUse
		{
			get
			{
				return this.inUse;
			}
			set
			{
				this.inUse = value;
			}
		}

		// Token: 0x06000069 RID: 105
		public abstract object GetValue();

		// Token: 0x0600006A RID: 106
		public abstract void SetValue(object newValue);

		// Token: 0x0600006B RID: 107
		public abstract void RemoveValue();

		// Token: 0x04000018 RID: 24
		private bool inUse;
	}
}
