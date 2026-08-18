using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using NLog.Common;
using NLog.Internal.NetworkSenders;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000148 RID: 328
	[Target("Network")]
	public class NetworkTarget : TargetWithLayout
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		public NetworkTarget()
		{
			this.SenderFactory = NetworkSenderFactory.Default;
			this.Encoding = Encoding.UTF8;
			this.OnOverflow = NetworkTargetOverflowAction.Split;
			this.KeepConnection = true;
			this.MaxMessageSize = 65000;
			this.ConnectionCacheSize = 5;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0001AD17 File Offset: 0x00018F17
		public NetworkTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0001AD26 File Offset: 0x00018F26
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x0001AD2E File Offset: 0x00018F2E
		public Layout Address { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0001AD37 File Offset: 0x00018F37
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0001AD3F File Offset: 0x00018F3F
		[DefaultValue(true)]
		public bool KeepConnection { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0001AD48 File Offset: 0x00018F48
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x0001AD50 File Offset: 0x00018F50
		[DefaultValue(false)]
		public bool NewLine { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0001AD59 File Offset: 0x00018F59
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0001AD61 File Offset: 0x00018F61
		[DefaultValue(65000)]
		public int MaxMessageSize { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0001AD6A File Offset: 0x00018F6A
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0001AD72 File Offset: 0x00018F72
		[DefaultValue(5)]
		public int ConnectionCacheSize { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0001AD7B File Offset: 0x00018F7B
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0001AD83 File Offset: 0x00018F83
		[DefaultValue(0)]
		public int MaxConnections { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0001AD8C File Offset: 0x00018F8C
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0001AD94 File Offset: 0x00018F94
		public NetworkTargetConnectionsOverflowAction OnConnectionOverflow { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0001AD9D File Offset: 0x00018F9D
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0001ADA5 File Offset: 0x00018FA5
		[DefaultValue(0)]
		public int MaxQueueSize { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0001ADAE File Offset: 0x00018FAE
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0001ADB6 File Offset: 0x00018FB6
		public NetworkTargetOverflowAction OnOverflow { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0001ADBF File Offset: 0x00018FBF
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x0001ADC7 File Offset: 0x00018FC7
		[DefaultValue("utf-8")]
		public Encoding Encoding { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0001ADD0 File Offset: 0x00018FD0
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		internal INetworkSenderFactory SenderFactory { get; set; }

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001AE04 File Offset: 0x00019004
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			int remainingCount = 0;
			AsyncContinuation continuation = delegate(Exception ex)
			{
				if (Interlocked.Decrement(ref remainingCount) == 0)
				{
					asyncContinuation(null);
				}
			};
			lock (this.openNetworkSenders)
			{
				remainingCount = this.openNetworkSenders.Count;
				if (remainingCount == 0)
				{
					asyncContinuation(null);
				}
				else
				{
					foreach (NetworkSender networkSender in this.openNetworkSenders)
					{
						networkSender.FlushAsync(continuation);
					}
				}
			}
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001AED0 File Offset: 0x000190D0
		protected override void CloseTarget()
		{
			base.CloseTarget();
			lock (this.openNetworkSenders)
			{
				foreach (NetworkSender networkSender in this.openNetworkSenders)
				{
					networkSender.Close(delegate(Exception ex)
					{
					});
				}
				this.openNetworkSenders.Clear();
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001B0A8 File Offset: 0x000192A8
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			NetworkTarget.<>c__DisplayClassc CS$<>8__locals1 = new NetworkTarget.<>c__DisplayClassc();
			CS$<>8__locals1.logEvent = logEvent;
			CS$<>8__locals1.<>4__this = this;
			string text = this.Address.Render(CS$<>8__locals1.logEvent.LogEvent);
			byte[] bytesToWrite = this.GetBytesToWrite(CS$<>8__locals1.logEvent.LogEvent);
			if (this.KeepConnection)
			{
				LinkedListNode<NetworkSender> senderNode = this.GetCachedNetworkSender(text);
				this.ChunkedSend(senderNode.Value, bytesToWrite, delegate(Exception ex)
				{
					if (ex != null)
					{
						InternalLogger.Error(ex, "Error when sending.");
						CS$<>8__locals1.<>4__this.ReleaseCachedConnection(senderNode);
					}
					CS$<>8__locals1.logEvent.Continuation(ex);
				});
				return;
			}
			NetworkSender sender;
			LinkedListNode<NetworkSender> linkedListNode;
			lock (this.openNetworkSenders)
			{
				bool flag2 = this.openNetworkSenders.Count >= this.MaxConnections;
				if (flag2 && this.MaxConnections > 0)
				{
					switch (this.OnConnectionOverflow)
					{
					case NetworkTargetConnectionsOverflowAction.AllowNewConnnection:
						InternalLogger.Debug("Too may connections, but this is allowed");
						break;
					case NetworkTargetConnectionsOverflowAction.DiscardMessage:
						InternalLogger.Warn("Discarding message otherwise to many connections.");
						CS$<>8__locals1.logEvent.Continuation(null);
						return;
					case NetworkTargetConnectionsOverflowAction.Block:
						while (this.openNetworkSenders.Count >= this.MaxConnections)
						{
							InternalLogger.Debug("Blocking networktarget otherwhise too many connections.");
							Monitor.Wait(this.openNetworkSenders);
							InternalLogger.Trace("Entered critical section.");
						}
						InternalLogger.Trace("Limit ok.");
						break;
					}
				}
				sender = this.SenderFactory.Create(text, this.MaxQueueSize);
				sender.Initialize();
				linkedListNode = this.openNetworkSenders.AddLast(sender);
			}
			this.ChunkedSend(sender, bytesToWrite, delegate(Exception ex)
			{
				lock (CS$<>8__locals1.<>4__this.openNetworkSenders)
				{
					NetworkTarget.TryRemove<NetworkSender>(CS$<>8__locals1.<>4__this.openNetworkSenders, linkedListNode);
					if (CS$<>8__locals1.<>4__this.OnConnectionOverflow == NetworkTargetConnectionsOverflowAction.Block)
					{
						Monitor.PulseAll(CS$<>8__locals1.<>4__this.openNetworkSenders);
					}
				}
				if (ex != null)
				{
					InternalLogger.Error(ex, "Error when sending.");
				}
				sender.Close(delegate(Exception ex2)
				{
				});
				CS$<>8__locals1.logEvent.Continuation(ex);
			});
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001B290 File Offset: 0x00019490
		private static bool TryRemove<T>(LinkedList<T> list, LinkedListNode<T> node)
		{
			if (node == null || list != node.List)
			{
				return false;
			}
			list.Remove(node);
			return true;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001B2A8 File Offset: 0x000194A8
		protected virtual byte[] GetBytesToWrite(LogEventInfo logEvent)
		{
			string s;
			if (this.NewLine)
			{
				s = this.Layout.Render(logEvent) + "\r\n";
			}
			else
			{
				s = this.Layout.Render(logEvent);
			}
			return this.Encoding.GetBytes(s);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001B2F0 File Offset: 0x000194F0
		private LinkedListNode<NetworkSender> GetCachedNetworkSender(string address)
		{
			LinkedListNode<NetworkSender> result;
			lock (this.currentSenderCache)
			{
				LinkedListNode<NetworkSender> linkedListNode;
				if (this.currentSenderCache.TryGetValue(address, out linkedListNode))
				{
					linkedListNode.Value.CheckSocket();
					result = linkedListNode;
				}
				else
				{
					if (this.currentSenderCache.Count >= this.ConnectionCacheSize)
					{
						int num = int.MaxValue;
						LinkedListNode<NetworkSender> linkedListNode2 = null;
						foreach (KeyValuePair<string, LinkedListNode<NetworkSender>> keyValuePair in this.currentSenderCache)
						{
							NetworkSender value = keyValuePair.Value.Value;
							if (value.LastSendTime < num)
							{
								num = value.LastSendTime;
								linkedListNode2 = keyValuePair.Value;
							}
						}
						if (linkedListNode2 != null)
						{
							this.ReleaseCachedConnection(linkedListNode2);
						}
					}
					NetworkSender networkSender = this.SenderFactory.Create(address, this.MaxQueueSize);
					networkSender.Initialize();
					lock (this.openNetworkSenders)
					{
						linkedListNode = this.openNetworkSenders.AddLast(networkSender);
					}
					this.currentSenderCache.Add(address, linkedListNode);
					result = linkedListNode;
				}
			}
			return result;
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001B46C File Offset: 0x0001966C
		private void ReleaseCachedConnection(LinkedListNode<NetworkSender> senderNode)
		{
			lock (this.currentSenderCache)
			{
				NetworkSender value = senderNode.Value;
				lock (this.openNetworkSenders)
				{
					if (NetworkTarget.TryRemove<NetworkSender>(this.openNetworkSenders, senderNode))
					{
						value.Close(delegate(Exception ex)
						{
						});
					}
				}
				LinkedListNode<NetworkSender> objB;
				if (this.currentSenderCache.TryGetValue(value.Address, out objB) && object.ReferenceEquals(senderNode, objB))
				{
					this.currentSenderCache.Remove(value.Address);
				}
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001B658 File Offset: 0x00019858
		private void ChunkedSend(NetworkSender sender, byte[] buffer, AsyncContinuation continuation)
		{
			int tosend = buffer.Length;
			int pos = 0;
			AsyncContinuation sendNextChunk = null;
			sendNextChunk = delegate(Exception ex)
			{
				if (ex != null)
				{
					continuation(ex);
					return;
				}
				if (tosend <= 0)
				{
					continuation(null);
					return;
				}
				int num = tosend;
				if (num > this.MaxMessageSize)
				{
					if (this.OnOverflow == NetworkTargetOverflowAction.Discard)
					{
						continuation(null);
						return;
					}
					if (this.OnOverflow == NetworkTargetOverflowAction.Error)
					{
						continuation(new OverflowException(string.Concat(new object[]
						{
							"Attempted to send a message larger than MaxMessageSize (",
							this.MaxMessageSize,
							"). Actual size was: ",
							buffer.Length,
							". Adjust OnOverflow and MaxMessageSize parameters accordingly."
						})));
						return;
					}
					num = this.MaxMessageSize;
				}
				int pos = pos;
				tosend -= num;
				pos += num;
				sender.Send(buffer, pos, num, sendNextChunk);
			};
			sendNextChunk(null);
		}

		// Token: 0x040002D1 RID: 721
		private Dictionary<string, LinkedListNode<NetworkSender>> currentSenderCache = new Dictionary<string, LinkedListNode<NetworkSender>>();

		// Token: 0x040002D2 RID: 722
		private LinkedList<NetworkSender> openNetworkSenders = new LinkedList<NetworkSender>();
	}
}
