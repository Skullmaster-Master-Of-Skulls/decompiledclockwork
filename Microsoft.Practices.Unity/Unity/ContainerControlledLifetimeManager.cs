using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000021 RID: 33
	public class ContainerControlledLifetimeManager : SynchronizedLifetimeManager, IDisposable
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000030D7 File Offset: 0x000012D7
		protected override object SynchronizedGetValue()
		{
			return this.value;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030DF File Offset: 0x000012DF
		protected override void SynchronizedSetValue(object newValue)
		{
			this.value = newValue;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000030E8 File Offset: 0x000012E8
		public override void RemoveValue()
		{
			this.Dispose();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000030F0 File Offset: 0x000012F0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000030FF File Offset: 0x000012FF
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "disposing", Justification = "This method is only here to avoid the other IDisposable warning")]
		protected virtual void Dispose(bool disposing)
		{
			if (this.value != null)
			{
				if (this.value is IDisposable)
				{
					((IDisposable)this.value).Dispose();
				}
				this.value = null;
			}
		}

		// Token: 0x0400001A RID: 26
		private object value;
	}
}
