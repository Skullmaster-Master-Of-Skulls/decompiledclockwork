using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Hosting;
using System.Web.Management;
using System.Web.Security;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200004A RID: 74
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	internal sealed class RootedObjects : IPrincipalContainer
	{
		// Token: 0x06000572 RID: 1394 RVA: 0x00007440 File Offset: 0x00005640
		private RootedObjects()
		{
			this._handle = GCHandle.Alloc(this);
			this.Pointer = (IntPtr)this._handle;
			HttpRuntime.IncrementActivePipelineCount();
			this._activityIdTracingIsEnabled = (ActivityIdHelper.Instance != null && AspNetEventSource.Instance.IsEnabled());
			if (this._activityIdTracingIsEnabled)
			{
				this._requestActivityId = ActivityIdHelper.UnsafeCreateNewActivityId();
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000074A9 File Offset: 0x000056A9
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x000074B1 File Offset: 0x000056B1
		public HttpContext HttpContext { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000074BA File Offset: 0x000056BA
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x000074C2 File Offset: 0x000056C2
		public IPrincipal Principal { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x000074CB File Offset: 0x000056CB
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x000074D3 File Offset: 0x000056D3
		public IntPtr Pointer { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000074DC File Offset: 0x000056DC
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x000074E4 File Offset: 0x000056E4
		public WebSocketPipeline WebSocketPipeline { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000074ED File Offset: 0x000056ED
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x000074F5 File Offset: 0x000056F5
		public IIS7WorkerRequest WorkerRequest { get; set; }

		// Token: 0x0600057D RID: 1405 RVA: 0x000074FE File Offset: 0x000056FE
		public static RootedObjects Create()
		{
			return new RootedObjects();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00007508 File Offset: 0x00005708
		public void Destroy()
		{
			using (this.WithinTraceBlock(true))
			{
				try
				{
					this.ReleaseHttpContext();
					this.ReleaseWebSocketPipeline();
					this.ReleaseWorkerRequest();
					this.ReleasePrincipal();
					this.RaiseOnPipelineCompleted();
					PerfCounters.DecrementCounter(AppPerfCounter.REQUESTS_EXECUTING);
				}
				finally
				{
					if (this._handle.IsAllocated)
					{
						this._handle.Free();
					}
					this.Pointer = IntPtr.Zero;
					HttpRuntime.DecrementActivePipelineCount();
					AspNetEventSource.Instance.RequestCompleted();
				}
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000075A4 File Offset: 0x000057A4
		internal ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
		{
			return this._pipelineCompletedQueue.Enqueue(target);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000075B4 File Offset: 0x000057B4
		public static RootedObjects FromPointer(IntPtr pointer)
		{
			return (RootedObjects)((GCHandle)pointer).Target;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000075D4 File Offset: 0x000057D4
		internal void RaiseOnPipelineCompleted()
		{
			try
			{
				this._pipelineCompletedQueue.FireAndComplete(delegate(IDisposable disposable)
				{
					disposable.Dispose();
				});
			}
			catch (Exception e)
			{
				WebBaseEvent.RaiseRuntimeError(e, null);
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00007628 File Offset: 0x00005828
		public void ReleaseHttpContext()
		{
			if (this.HttpContext != null)
			{
				this.HttpContext.FinishPipelineRequest();
			}
			this.HttpContext = null;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00007644 File Offset: 0x00005844
		public void ReleasePrincipal()
		{
			if (this.Principal != null && this.Principal != WindowsAuthenticationModule.AnonymousPrincipal)
			{
				WindowsIdentity windowsIdentity = this.Principal.Identity as WindowsIdentity;
				if (windowsIdentity != null)
				{
					this.Principal = null;
					windowsIdentity.Dispose();
				}
			}
			if (BinaryCompatibility.Current.TargetsAtLeastFramework45)
			{
				this.Principal = null;
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000769A File Offset: 0x0000589A
		public void ReleaseWebSocketPipeline()
		{
			if (this.WebSocketPipeline != null)
			{
				this.WebSocketPipeline.Dispose();
			}
			this.WebSocketPipeline = null;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000076B6 File Offset: 0x000058B6
		public void ReleaseWorkerRequest()
		{
			if (this.WorkerRequest != null)
			{
				this.WorkerRequest.Dispose();
			}
			this.WorkerRequest = null;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000076D2 File Offset: 0x000058D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RootedObjects.ActivityIdToken WithinTraceBlock()
		{
			return this.WithinTraceBlock(false);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000076DC File Offset: 0x000058DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private RootedObjects.ActivityIdToken WithinTraceBlock(bool isDestroying)
		{
			if (this._activityIdTracingIsEnabled)
			{
				return new RootedObjects.ActivityIdToken(this, isDestroying);
			}
			return default(RootedObjects.ActivityIdToken);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00007702 File Offset: 0x00005902
		public void WriteTransferEventIfNecessary()
		{
			if (this._activityIdTracingIsEnabled)
			{
				AspNetEventSource.Instance.RequestEnteredAspNetPipeline(this.WorkerRequest, this._requestActivityId);
			}
		}

		// Token: 0x04000143 RID: 323
		private readonly bool _activityIdTracingIsEnabled;

		// Token: 0x04000144 RID: 324
		private readonly Guid _requestActivityId;

		// Token: 0x04000145 RID: 325
		private int _requestActivityIdRefCount = 1;

		// Token: 0x04000146 RID: 326
		private SubscriptionQueue<IDisposable> _pipelineCompletedQueue;

		// Token: 0x04000147 RID: 327
		private GCHandle _handle;

		// Token: 0x020008BF RID: 2239
		internal struct ActivityIdToken : IDisposable
		{
			// Token: 0x060067C2 RID: 26562 RVA: 0x0017092C File Offset: 0x0016EB2C
			internal ActivityIdToken(RootedObjects rootedObjects, bool isDestroying)
			{
				ActivityIdHelper.Instance.SetCurrentThreadActivityId(rootedObjects._requestActivityId, out this._originalActivityId);
				lock (rootedObjects)
				{
					rootedObjects._requestActivityIdRefCount++;
				}
				this._rootedObjects = rootedObjects;
				this._isDestroying = isDestroying;
			}

			// Token: 0x060067C3 RID: 26563 RVA: 0x00170994 File Offset: 0x0016EB94
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
				if (this._rootedObjects == null)
				{
					return;
				}
				this.DisposeImpl();
			}

			// Token: 0x060067C4 RID: 26564 RVA: 0x001709A8 File Offset: 0x0016EBA8
			private void DisposeImpl()
			{
				RootedObjects rootedObjects = this._rootedObjects;
				lock (rootedObjects)
				{
					this._rootedObjects._requestActivityIdRefCount -= (this._isDestroying ? 2 : 1);
					if (this._rootedObjects._requestActivityIdRefCount == 0)
					{
						ActivityIdHelper.Instance.SetCurrentThreadActivityId(this._originalActivityId);
					}
					else
					{
						Guid guid;
						ActivityIdHelper.Instance.SetCurrentThreadActivityId(this._originalActivityId, out guid);
					}
				}
			}

			// Token: 0x040035F2 RID: 13810
			private readonly bool _isDestroying;

			// Token: 0x040035F3 RID: 13811
			private readonly Guid _originalActivityId;

			// Token: 0x040035F4 RID: 13812
			private readonly RootedObjects _rootedObjects;
		}
	}
}
