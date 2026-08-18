using System;
using System.Collections.Generic;
using System.Messaging;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008DA RID: 2266
	internal class MsmqBindingMonitor
	{
		// Token: 0x06005636 RID: 22070 RVA: 0x0013B990 File Offset: 0x00139B90
		public MsmqBindingMonitor(string host) : this(host, MsmqBindingMonitor.DefaultUpdateInterval, false)
		{
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0013B9A0 File Offset: 0x00139BA0
		public MsmqBindingMonitor(string host, TimeSpan updateInterval, bool retryMatchedFilters)
		{
			if (string.Compare(host, "localhost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.host = ".";
			}
			else
			{
				this.host = host;
			}
			this.firstRoundComplete = new ManualResetEvent(false);
			this.updateInterval = updateInterval;
			this.retryMatchedFilters = retryMatchedFilters;
			this.iteration = 1;
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x0013BA24 File Offset: 0x00139C24
		public void AddFilter(MsmqBindingFilter filter)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.filters.Add(filter);
				this.MatchFilter(filter, this.knownPublicQueues.Values);
				this.MatchFilter(filter, this.knownPrivateQueues.Values);
			}
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x0013BA90 File Offset: 0x00139C90
		public bool ContainsFilter(MsmqBindingFilter filter)
		{
			object obj = this.thisLock;
			bool result;
			lock (obj)
			{
				result = this.filters.Contains(filter);
			}
			return result;
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x0013BAD8 File Offset: 0x00139CD8
		public void Open()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.currentState != CommunicationState.Created)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CommunicationObjectCannotBeModified", new object[]
					{
						base.GetType().ToString()
					})));
				}
				this.currentState = CommunicationState.Opened;
				this.ScheduleRetryTimerIfNotSet();
			}
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x0013BB58 File Offset: 0x00139D58
		public void Close()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.currentState != CommunicationState.Opened)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CommunicationObjectCannotBeModified", new object[]
					{
						base.GetType().ToString()
					})));
				}
				this.currentState = CommunicationState.Closed;
				this.CancelRetryTimer();
			}
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x0013BBD8 File Offset: 0x00139DD8
		public void RemoveFilter(MsmqBindingFilter filter)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.filters.Remove(filter);
				this.RematchQueues(filter, this.knownPublicQueues.Values);
				this.RematchQueues(filter, this.knownPrivateQueues.Values);
			}
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x0013BC44 File Offset: 0x00139E44
		public void WaitForFirstRoundComplete()
		{
			this.firstRoundComplete.WaitOne();
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x0013BC52 File Offset: 0x00139E52
		private void ScheduleRetryTimerIfNotSet()
		{
			if (this.timer == null)
			{
				this.timer = new IOThreadTimer(new Action<object>(this.OnTimer), null, false);
				this.timer.Set(0);
			}
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x0013BC81 File Offset: 0x00139E81
		private void CancelRetryTimer()
		{
			if (this.timer != null)
			{
				this.timer.Cancel();
				this.timer = null;
			}
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x0013BCA0 File Offset: 0x00139EA0
		private void MatchFilter(MsmqBindingFilter filter, IEnumerable<MsmqBindingMonitor.MatchState> queues)
		{
			foreach (MsmqBindingMonitor.MatchState matchState in queues)
			{
				int num = filter.Match(matchState.QueueName);
				if (num > matchState.LastMatchLength)
				{
					if (matchState.LastMatch != null)
					{
						matchState.LastMatch.MatchLost(this.host, matchState.QueueName, matchState.IsPrivate, matchState.CallbackState);
					}
					matchState.LastMatchLength = num;
					matchState.LastMatch = filter;
					matchState.CallbackState = filter.MatchFound(this.host, matchState.QueueName, matchState.IsPrivate);
				}
			}
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x0013BD50 File Offset: 0x00139F50
		private void RetryMatchFilters(IEnumerable<MsmqBindingMonitor.MatchState> queues)
		{
			foreach (MsmqBindingMonitor.MatchState matchState in queues)
			{
				if (matchState.LastMatch != null)
				{
					matchState.CallbackState = matchState.LastMatch.MatchFound(this.host, matchState.QueueName, matchState.IsPrivate);
				}
			}
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x0013BDBC File Offset: 0x00139FBC
		private void MatchQueue(MsmqBindingMonitor.MatchState state)
		{
			MsmqBindingFilter msmqBindingFilter = state.LastMatch;
			int num = state.LastMatchLength;
			foreach (MsmqBindingFilter msmqBindingFilter2 in this.filters)
			{
				int num2 = msmqBindingFilter2.Match(state.QueueName);
				if (num2 > num)
				{
					num = num2;
					msmqBindingFilter = msmqBindingFilter2;
				}
			}
			if (msmqBindingFilter != state.LastMatch)
			{
				if (state.LastMatch != null)
				{
					state.LastMatch.MatchLost(this.host, state.QueueName, state.IsPrivate, state.CallbackState);
				}
				state.LastMatchLength = num;
				state.LastMatch = msmqBindingFilter;
				state.CallbackState = msmqBindingFilter.MatchFound(this.host, state.QueueName, state.IsPrivate);
			}
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x0013BE90 File Offset: 0x0013A090
		private void OnTimer(object state)
		{
			try
			{
				if (this.currentState == CommunicationState.Opened)
				{
					object obj = this.thisLock;
					lock (obj)
					{
						if (this.retryMatchedFilters)
						{
							this.RetryMatchFilters(this.knownPublicQueues.Values);
							this.RetryMatchFilters(this.knownPrivateQueues.Values);
						}
						bool flag2 = !this.retryMatchedFilters || (this.retryMatchedFilters && this.iteration % 2 != 0);
						if (flag2)
						{
							MsmqDiagnostics.ScanStarted();
							try
							{
								MessageQueue[] publicQueuesByMachine = MessageQueue.GetPublicQueuesByMachine(this.host);
								this.ProcessFoundQueues(publicQueuesByMachine, this.knownPublicQueues, false);
							}
							catch (MessageQueueException ex)
							{
								MsmqDiagnostics.CannotReadQueues(this.host, true, ex);
							}
							try
							{
								MessageQueue[] privateQueuesByMachine = MessageQueue.GetPrivateQueuesByMachine(this.host);
								this.ProcessFoundQueues(privateQueuesByMachine, this.knownPrivateQueues, true);
							}
							catch (MessageQueueException ex2)
							{
								MsmqDiagnostics.CannotReadQueues(this.host, false, ex2);
							}
							this.ProcessLostQueues(this.knownPublicQueues);
							this.ProcessLostQueues(this.knownPrivateQueues);
						}
						this.iteration++;
						this.timer.Set(this.updateInterval);
					}
				}
			}
			finally
			{
				this.firstRoundComplete.Set();
			}
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x0013C024 File Offset: 0x0013A224
		private void ProcessFoundQueues(MessageQueue[] queues, Dictionary<string, MsmqBindingMonitor.MatchState> knownQueues, bool isPrivate)
		{
			foreach (MessageQueue messageQueue in queues)
			{
				string text = this.ExtractQueueName(messageQueue.QueueName, isPrivate);
				MsmqBindingMonitor.MatchState matchState;
				if (!knownQueues.TryGetValue(text, out matchState))
				{
					matchState = new MsmqBindingMonitor.MatchState(text, this.iteration, isPrivate);
					knownQueues.Add(text, matchState);
					this.MatchQueue(matchState);
				}
				else
				{
					matchState.DiscoveryIteration = this.iteration;
				}
			}
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x0013C08D File Offset: 0x0013A28D
		private string ExtractQueueName(string name, bool isPrivate)
		{
			if (isPrivate)
			{
				return name.Substring("private$\\".Length);
			}
			return name;
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x0013C0A4 File Offset: 0x0013A2A4
		private void ProcessLostQueues(Dictionary<string, MsmqBindingMonitor.MatchState> knownQueues)
		{
			List<MsmqBindingMonitor.MatchState> list = new List<MsmqBindingMonitor.MatchState>();
			foreach (MsmqBindingMonitor.MatchState matchState in knownQueues.Values)
			{
				if (matchState.DiscoveryIteration != this.iteration)
				{
					list.Add(matchState);
				}
			}
			foreach (MsmqBindingMonitor.MatchState matchState2 in list)
			{
				knownQueues.Remove(matchState2.QueueName);
				if (matchState2.LastMatch != null)
				{
					matchState2.LastMatch.MatchLost(this.host, matchState2.QueueName, matchState2.IsPrivate, matchState2.CallbackState);
				}
			}
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x0013C180 File Offset: 0x0013A380
		private void RematchQueues(MsmqBindingFilter filter, IEnumerable<MsmqBindingMonitor.MatchState> queues)
		{
			foreach (MsmqBindingMonitor.MatchState matchState in queues)
			{
				if (matchState.LastMatch == filter)
				{
					matchState.LastMatch.MatchLost(this.host, matchState.QueueName, matchState.IsPrivate, matchState.CallbackState);
					matchState.LastMatch = null;
					matchState.LastMatchLength = -1;
					this.MatchQueue(matchState);
				}
			}
		}

		// Token: 0x0400354E RID: 13646
		private static readonly TimeSpan DefaultUpdateInterval = TimeSpan.FromMinutes(10.0);

		// Token: 0x0400354F RID: 13647
		private CommunicationState currentState;

		// Token: 0x04003550 RID: 13648
		private List<MsmqBindingFilter> filters = new List<MsmqBindingFilter>();

		// Token: 0x04003551 RID: 13649
		private string host;

		// Token: 0x04003552 RID: 13650
		private int iteration;

		// Token: 0x04003553 RID: 13651
		private Dictionary<string, MsmqBindingMonitor.MatchState> knownPublicQueues = new Dictionary<string, MsmqBindingMonitor.MatchState>();

		// Token: 0x04003554 RID: 13652
		private Dictionary<string, MsmqBindingMonitor.MatchState> knownPrivateQueues = new Dictionary<string, MsmqBindingMonitor.MatchState>();

		// Token: 0x04003555 RID: 13653
		private object thisLock = new object();

		// Token: 0x04003556 RID: 13654
		private IOThreadTimer timer;

		// Token: 0x04003557 RID: 13655
		private TimeSpan updateInterval;

		// Token: 0x04003558 RID: 13656
		private ManualResetEvent firstRoundComplete;

		// Token: 0x04003559 RID: 13657
		private bool retryMatchedFilters;

		// Token: 0x02000D8A RID: 3466
		private class MatchState
		{
			// Token: 0x06007E88 RID: 32392 RVA: 0x001D7B6D File Offset: 0x001D5D6D
			public MatchState(string name, int iteration, bool isPrivate)
			{
				this.name = name;
				this.iteration = iteration;
				this.isPrivate = isPrivate;
				this.lastMatchLength = -1;
			}

			// Token: 0x17001C32 RID: 7218
			// (get) Token: 0x06007E89 RID: 32393 RVA: 0x001D7B91 File Offset: 0x001D5D91
			// (set) Token: 0x06007E8A RID: 32394 RVA: 0x001D7B99 File Offset: 0x001D5D99
			public object CallbackState
			{
				get
				{
					return this.callbackState;
				}
				set
				{
					this.callbackState = value;
				}
			}

			// Token: 0x17001C33 RID: 7219
			// (get) Token: 0x06007E8B RID: 32395 RVA: 0x001D7BA2 File Offset: 0x001D5DA2
			// (set) Token: 0x06007E8C RID: 32396 RVA: 0x001D7BAA File Offset: 0x001D5DAA
			public int DiscoveryIteration
			{
				get
				{
					return this.iteration;
				}
				set
				{
					this.iteration = value;
				}
			}

			// Token: 0x17001C34 RID: 7220
			// (get) Token: 0x06007E8D RID: 32397 RVA: 0x001D7BB3 File Offset: 0x001D5DB3
			public bool IsPrivate
			{
				get
				{
					return this.isPrivate;
				}
			}

			// Token: 0x17001C35 RID: 7221
			// (get) Token: 0x06007E8E RID: 32398 RVA: 0x001D7BBB File Offset: 0x001D5DBB
			// (set) Token: 0x06007E8F RID: 32399 RVA: 0x001D7BC3 File Offset: 0x001D5DC3
			public MsmqBindingFilter LastMatch
			{
				get
				{
					return this.lastMatch;
				}
				set
				{
					this.lastMatch = value;
				}
			}

			// Token: 0x17001C36 RID: 7222
			// (get) Token: 0x06007E90 RID: 32400 RVA: 0x001D7BCC File Offset: 0x001D5DCC
			// (set) Token: 0x06007E91 RID: 32401 RVA: 0x001D7BD4 File Offset: 0x001D5DD4
			public int LastMatchLength
			{
				get
				{
					return this.lastMatchLength;
				}
				set
				{
					this.lastMatchLength = value;
				}
			}

			// Token: 0x17001C37 RID: 7223
			// (get) Token: 0x06007E92 RID: 32402 RVA: 0x001D7BDD File Offset: 0x001D5DDD
			public string QueueName
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x0400489B RID: 18587
			private string name;

			// Token: 0x0400489C RID: 18588
			private int iteration;

			// Token: 0x0400489D RID: 18589
			private MsmqBindingFilter lastMatch;

			// Token: 0x0400489E RID: 18590
			private int lastMatchLength;

			// Token: 0x0400489F RID: 18591
			private object callbackState;

			// Token: 0x040048A0 RID: 18592
			private bool isPrivate;
		}
	}
}
