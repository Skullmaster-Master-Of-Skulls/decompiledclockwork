using System;
using System.Collections;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x0200021B RID: 539
	internal class ResourcePool : IDisposable
	{
		// Token: 0x06001A00 RID: 6656 RVA: 0x00051272 File Offset: 0x0004F472
		internal ResourcePool(TimeSpan interval, int max)
		{
			this._interval = interval;
			this._resources = new ArrayList(4);
			this._max = max;
			this._callback = new TimerCallback(this.TimerProc);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000512A6 File Offset: 0x0004F4A6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000512B8 File Offset: 0x0004F4B8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (!this._disposed)
					{
						if (this._resources != null)
						{
							foreach (object obj in this._resources)
							{
								IDisposable disposable = (IDisposable)obj;
								disposable.Dispose();
							}
							this._resources.Clear();
						}
						if (this._timer != null)
						{
							this._timer.Dispose();
						}
						this._disposed = true;
					}
				}
			}
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00051374 File Offset: 0x0004F574
		internal object RetrieveResource()
		{
			object result = null;
			if (this._resources.Count != 0)
			{
				lock (this)
				{
					if (!this._disposed)
					{
						if (this._resources.Count == 0)
						{
							result = null;
						}
						else
						{
							result = this._resources[this._resources.Count - 1];
							this._resources.RemoveAt(this._resources.Count - 1);
							if (this._resources.Count < this._iDisposable)
							{
								this._iDisposable = this._resources.Count;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0005142C File Offset: 0x0004F62C
		internal void StoreResource(IDisposable o)
		{
			lock (this)
			{
				if (!this._disposed && this._resources.Count < this._max)
				{
					this._resources.Add(o);
					o = null;
					if (this._timer == null)
					{
						this._timer = new Timer(this._callback, null, this._interval, this._interval);
					}
				}
			}
			if (o != null)
			{
				o.Dispose();
			}
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000514BC File Offset: 0x0004F6BC
		private void TimerProc(object userData)
		{
			IDisposable[] array = null;
			lock (this)
			{
				if (!this._disposed)
				{
					if (this._resources.Count == 0)
					{
						if (this._timer != null)
						{
							this._timer.Dispose();
							this._timer = null;
						}
						return;
					}
					array = new IDisposable[this._iDisposable];
					this._resources.CopyTo(0, array, 0, this._iDisposable);
					this._resources.RemoveRange(0, this._iDisposable);
					this._iDisposable = this._resources.Count;
				}
			}
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					try
					{
						array[i].Dispose();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x04001806 RID: 6150
		private ArrayList _resources;

		// Token: 0x04001807 RID: 6151
		private int _iDisposable;

		// Token: 0x04001808 RID: 6152
		private int _max;

		// Token: 0x04001809 RID: 6153
		private Timer _timer;

		// Token: 0x0400180A RID: 6154
		private TimerCallback _callback;

		// Token: 0x0400180B RID: 6155
		private TimeSpan _interval;

		// Token: 0x0400180C RID: 6156
		private bool _disposed;
	}
}
