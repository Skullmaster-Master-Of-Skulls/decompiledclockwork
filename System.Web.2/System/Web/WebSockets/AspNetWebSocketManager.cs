using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.WebSockets
{
	// Token: 0x020001B7 RID: 439
	internal sealed class AspNetWebSocketManager
	{
		// Token: 0x060016A1 RID: 5793 RVA: 0x00047D79 File Offset: 0x00045F79
		internal AspNetWebSocketManager(IPerfCounters perfCounters)
		{
			this._perfCounters = perfCounters;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00047D94 File Offset: 0x00045F94
		public int ActiveSocketCount
		{
			get
			{
				HashSet<IAsyncAbortableWebSocket> activeSockets = this._activeSockets;
				int count;
				lock (activeSockets)
				{
					count = this._activeSockets.Count;
				}
				return count;
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00047DDC File Offset: 0x00045FDC
		public void AbortAllAndWait()
		{
			HashSet<IAsyncAbortableWebSocket> activeSockets = this._activeSockets;
			IAsyncAbortableWebSocket[] array;
			lock (activeSockets)
			{
				this._aborted = true;
				array = this._activeSockets.ToArray<IAsyncAbortableWebSocket>();
			}
			Task[] tasks = Array.ConvertAll<IAsyncAbortableWebSocket, Task>(array, (IAsyncAbortableWebSocket socket) => socket.AbortAsync());
			Task.WaitAll(tasks);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00047E58 File Offset: 0x00046058
		public void Add(IAsyncAbortableWebSocket webSocket)
		{
			HashSet<IAsyncAbortableWebSocket> activeSockets = this._activeSockets;
			int count;
			bool aborted;
			lock (activeSockets)
			{
				this._activeSockets.Add(webSocket);
				count = this._activeSockets.Count;
				aborted = this._aborted;
			}
			this._perfCounters.SetCounter(AppPerfCounter.REQUESTS_EXECUTING_WEBSOCKETS, count);
			if (aborted)
			{
				webSocket.AbortAsync();
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00047ECC File Offset: 0x000460CC
		public void Remove(IAsyncAbortableWebSocket webSocket)
		{
			HashSet<IAsyncAbortableWebSocket> activeSockets = this._activeSockets;
			int count;
			lock (activeSockets)
			{
				this._activeSockets.Remove(webSocket);
				count = this._activeSockets.Count;
			}
			this._perfCounters.SetCounter(AppPerfCounter.REQUESTS_EXECUTING_WEBSOCKETS, count);
		}

		// Token: 0x040016B7 RID: 5815
		public static readonly AspNetWebSocketManager Current = new AspNetWebSocketManager(PerfCounters.Instance);

		// Token: 0x040016B8 RID: 5816
		private bool _aborted;

		// Token: 0x040016B9 RID: 5817
		internal readonly HashSet<IAsyncAbortableWebSocket> _activeSockets = new HashSet<IAsyncAbortableWebSocket>();

		// Token: 0x040016BA RID: 5818
		private readonly IPerfCounters _perfCounters;
	}
}
