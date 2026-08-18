using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x02000794 RID: 1940
	internal sealed class SuspendManager
	{
		// Token: 0x06005C94 RID: 23700 RVA: 0x001406D7 File Offset: 0x0013E8D7
		public void RegisterObject(ISuspendibleRegisteredObject o)
		{
			this._registeredObjects[o] = null;
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x001406E6 File Offset: 0x0013E8E6
		public void UnregisterObject(ISuspendibleRegisteredObject o)
		{
			((IDictionary<ISuspendibleRegisteredObject, object>)this._registeredObjects).Remove(o);
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x001406F8 File Offset: 0x0013E8F8
		public object Suspend()
		{
			ICollection<ISuspendibleRegisteredObject> keys = this._registeredObjects.Keys;
			return SuspendManager.SuspendImpl(keys);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x00140718 File Offset: 0x0013E918
		private static SuspendManager.SuspendState SuspendImpl(ICollection<ISuspendibleRegisteredObject> allRegisteredObjects)
		{
			CountdownEvent countdownEvent = new CountdownEvent(2);
			SuspendManager.SuspendState suspendState = new SuspendManager.SuspendState(allRegisteredObjects);
			if (allRegisteredObjects.Count > 0)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object _)
				{
					suspendState.Suspend();
					countdownEvent.Signal();
				}, null);
			}
			else
			{
				countdownEvent.Signal();
			}
			ThreadPool.UnsafeQueueUserWorkItem(delegate(object _)
			{
				HttpWriter.ReleaseAllPooledBuffers();
				CacheStoreProvider internalCache = HttpRuntime.Cache.GetInternalCache(false);
				CacheStoreProvider objectCache = HttpRuntime.Cache.GetObjectCache(false);
				if (internalCache != null)
				{
					internalCache.Trim(0);
				}
				if (objectCache != null && !objectCache.Equals(internalCache))
				{
					objectCache.Trim(0);
				}
				HttpApplicationFactory.TrimApplicationInstances(true);
				countdownEvent.Signal();
			}, null);
			if (Debug.IsDebuggerPresent())
			{
				countdownEvent.Wait();
			}
			else
			{
				countdownEvent.Wait(SuspendManager._suspendMethodTimeout);
			}
			return suspendState;
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x001407AB File Offset: 0x0013E9AB
		public void Resume(object state)
		{
			((SuspendManager.SuspendState)state).Resume();
		}

		// Token: 0x040030C8 RID: 12488
		private static readonly TimeSpan _suspendMethodTimeout = TimeSpan.FromSeconds(5.0);

		// Token: 0x040030C9 RID: 12489
		private readonly ConcurrentDictionary<ISuspendibleRegisteredObject, object> _registeredObjects = new ConcurrentDictionary<ISuspendibleRegisteredObject, object>();

		// Token: 0x02000A52 RID: 2642
		internal sealed class SuspendState
		{
			// Token: 0x06006EC6 RID: 28358 RVA: 0x0018AE74 File Offset: 0x00189074
			public SuspendState(ICollection<ISuspendibleRegisteredObject> suspendibleObjects)
			{
				this._suspendibleObjects = suspendibleObjects;
				this._resumeCallbacks = new List<Action>(suspendibleObjects.Count);
			}

			// Token: 0x06006EC7 RID: 28359 RVA: 0x0018AE94 File Offset: 0x00189094
			public void Suspend()
			{
				foreach (ISuspendibleRegisteredObject suspendibleRegisteredObject in this._suspendibleObjects)
				{
					Action action = suspendibleRegisteredObject.Suspend();
					lock (this)
					{
						if (this._resumeWasCalled)
						{
							if (action != null)
							{
								SuspendManager.SuspendState.InvokeResumeCallbackAsync(action);
							}
							break;
						}
						if (action != null)
						{
							this._resumeCallbacks.Add(action);
						}
					}
				}
			}

			// Token: 0x06006EC8 RID: 28360 RVA: 0x0018AF2C File Offset: 0x0018912C
			public void Resume()
			{
				lock (this)
				{
					this._resumeWasCalled = true;
					foreach (Action callback in this._resumeCallbacks)
					{
						SuspendManager.SuspendState.InvokeResumeCallbackAsync(callback);
					}
				}
			}

			// Token: 0x06006EC9 RID: 28361 RVA: 0x0018AFA8 File Offset: 0x001891A8
			private static void InvokeResumeCallbackAsync(Action callback)
			{
				ThreadPool.UnsafeQueueUserWorkItem(SuspendManager.SuspendState._quwiThunk, callback);
			}

			// Token: 0x04003B68 RID: 15208
			private static readonly WaitCallback _quwiThunk = delegate(object state)
			{
				((Action)state)();
			};

			// Token: 0x04003B69 RID: 15209
			private readonly ICollection<ISuspendibleRegisteredObject> _suspendibleObjects;

			// Token: 0x04003B6A RID: 15210
			private readonly List<Action> _resumeCallbacks;

			// Token: 0x04003B6B RID: 15211
			private bool _resumeWasCalled;
		}
	}
}
