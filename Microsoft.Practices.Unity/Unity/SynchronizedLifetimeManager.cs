using System;
using System.Threading;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000020 RID: 32
	public abstract class SynchronizedLifetimeManager : LifetimeManager, IRequiresRecovery
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00003040 File Offset: 0x00001240
		public override object GetValue()
		{
			Monitor.Enter(this.lockObj);
			object obj = this.SynchronizedGetValue();
			if (obj != null)
			{
				Monitor.Exit(this.lockObj);
			}
			return obj;
		}

		// Token: 0x0600006F RID: 111
		protected abstract object SynchronizedGetValue();

		// Token: 0x06000070 RID: 112 RVA: 0x0000306E File Offset: 0x0000126E
		public override void SetValue(object newValue)
		{
			this.SynchronizedSetValue(newValue);
			this.TryExit();
		}

		// Token: 0x06000071 RID: 113
		protected abstract void SynchronizedSetValue(object newValue);

		// Token: 0x06000072 RID: 114 RVA: 0x0000307D File Offset: 0x0000127D
		public override void RemoveValue()
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000307F File Offset: 0x0000127F
		public void Recover()
		{
			this.TryExit();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003088 File Offset: 0x00001288
		private void TryExit()
		{
			if (Monitor.IsEntered(this.lockObj))
			{
				try
				{
					Monitor.Exit(this.lockObj);
				}
				catch (SynchronizationLockException)
				{
				}
			}
		}

		// Token: 0x04000019 RID: 25
		private object lockObj = new object();
	}
}
