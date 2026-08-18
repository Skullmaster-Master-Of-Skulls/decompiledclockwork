using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;
using NLog.LogReceiverService;

namespace NLog.Targets
{
	// Token: 0x02000161 RID: 353
	[Target("LogReceiverService")]
	public class LogReceiverWebServiceTarget : Target
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x0001FA8F File Offset: 0x0001DC8F
		public LogReceiverWebServiceTarget()
		{
			this.Parameters = new List<MethodCallParameter>();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		public LogReceiverWebServiceTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0001FAC7 File Offset: 0x0001DCC7
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x0001FACF File Offset: 0x0001DCCF
		[RequiredParameter]
		public virtual string EndpointAddress { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0001FAD8 File Offset: 0x0001DCD8
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x0001FAE0 File Offset: 0x0001DCE0
		public string EndpointConfigurationName { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0001FAE9 File Offset: 0x0001DCE9
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x0001FAF1 File Offset: 0x0001DCF1
		public bool UseBinaryEncoding { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0001FAFA File Offset: 0x0001DCFA
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x0001FB02 File Offset: 0x0001DD02
		public bool UseOneWayContract { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0001FB0B File Offset: 0x0001DD0B
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x0001FB13 File Offset: 0x0001DD13
		public Layout ClientId { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0001FB1C File Offset: 0x0001DD1C
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x0001FB24 File Offset: 0x0001DD24
		[ArrayParameter(typeof(MethodCallParameter), "parameter")]
		public IList<MethodCallParameter> Parameters { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x0001FB2D File Offset: 0x0001DD2D
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x0001FB35 File Offset: 0x0001DD35
		public bool IncludeEventProperties { get; set; }

		// Token: 0x06000D46 RID: 3398 RVA: 0x0001FB3E File Offset: 0x0001DD3E
		protected internal virtual bool OnSend(NLogEvents events, IEnumerable<AsyncLogEventInfo> asyncContinuations)
		{
			return true;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001FB44 File Offset: 0x0001DD44
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			this.Write(new AsyncLogEventInfo[]
			{
				logEvent
			});
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0001FB6C File Offset: 0x0001DD6C
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			if (this.inCall)
			{
				foreach (AsyncLogEventInfo eventInfo in logEvents)
				{
					this.buffer.Append(eventInfo);
				}
				return;
			}
			NLogEvents events = this.TranslateLogEvents(logEvents);
			this.Send(events, logEvents);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001FBBC File Offset: 0x0001DDBC
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			try
			{
				this.SendBufferedEvents();
				asyncContinuation(null);
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrown())
				{
					throw;
				}
				asyncContinuation(exception);
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		private static int AddValueAndGetStringOrdinal(NLogEvents context, Dictionary<string, int> stringTable, string value)
		{
			int count;
			if (!stringTable.TryGetValue(value, out count))
			{
				count = context.Strings.Count;
				stringTable.Add(value, count);
				context.Strings.Add(value);
			}
			return count;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0001FC38 File Offset: 0x0001DE38
		private NLogEvents TranslateLogEvents(AsyncLogEventInfo[] logEvents)
		{
			if (logEvents.Length == 0 && !LogManager.ThrowExceptions)
			{
				InternalLogger.Error("LogEvents array is empty, sending empty event...");
				return new NLogEvents();
			}
			string clientName = string.Empty;
			if (this.ClientId != null)
			{
				clientName = this.ClientId.Render(logEvents[0].LogEvent);
			}
			NLogEvents nlogEvents = new NLogEvents
			{
				ClientName = clientName,
				LayoutNames = new StringCollection(),
				Strings = new StringCollection(),
				BaseTimeUtc = logEvents[0].LogEvent.TimeStamp.ToUniversalTime().Ticks
			};
			Dictionary<string, int> stringTable = new Dictionary<string, int>();
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				nlogEvents.LayoutNames.Add(this.Parameters[i].Name);
			}
			if (this.IncludeEventProperties)
			{
				for (int j = 0; j < logEvents.Length; j++)
				{
					LogEventInfo logEvent = logEvents[j].LogEvent;
					foreach (KeyValuePair<object, object> keyValuePair in logEvent.Properties)
					{
						string text = keyValuePair.Key as string;
						if (text != null && !nlogEvents.LayoutNames.Contains(text))
						{
							nlogEvents.LayoutNames.Add(text);
						}
					}
				}
			}
			nlogEvents.Events = new NLogEvent[logEvents.Length];
			for (int k = 0; k < logEvents.Length; k++)
			{
				nlogEvents.Events[k] = this.TranslateEvent(logEvents[k].LogEvent, nlogEvents, stringTable);
			}
			return nlogEvents;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001FE58 File Offset: 0x0001E058
		private void Send(NLogEvents events, IEnumerable<AsyncLogEventInfo> asyncContinuations)
		{
			if (!this.OnSend(events, asyncContinuations))
			{
				return;
			}
			IWcfLogReceiverClient wcfLogReceiverClient = this.CreateLogReceiver();
			wcfLogReceiverClient.ProcessLogMessagesCompleted += delegate(object sender, AsyncCompletedEventArgs e)
			{
				foreach (AsyncLogEventInfo asyncLogEventInfo in asyncContinuations)
				{
					asyncLogEventInfo.Continuation(e.Error);
				}
				this.SendBufferedEvents();
			};
			this.inCall = true;
			wcfLogReceiverClient.ProcessLogMessagesAsync(events);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
		[Obsolete("Ths may be removed in a future release.  Use CreateLogReceiver.")]
		protected virtual WcfLogReceiverClient CreateWcfLogReceiverClient()
		{
			WcfLogReceiverClient wcfLogReceiverClient;
			if (string.IsNullOrEmpty(this.EndpointConfigurationName))
			{
				Binding binding;
				if (this.UseBinaryEncoding)
				{
					binding = new CustomBinding(new BindingElement[]
					{
						new BinaryMessageEncodingBindingElement(),
						new HttpTransportBindingElement()
					});
				}
				else
				{
					binding = new BasicHttpBinding();
				}
				wcfLogReceiverClient = new WcfLogReceiverClient(this.UseOneWayContract, binding, new EndpointAddress(this.EndpointAddress));
			}
			else
			{
				wcfLogReceiverClient = new WcfLogReceiverClient(this.UseOneWayContract, this.EndpointConfigurationName, new EndpointAddress(this.EndpointAddress));
			}
			wcfLogReceiverClient.ProcessLogMessagesCompleted += this.ClientOnProcessLogMessagesCompleted;
			return wcfLogReceiverClient;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0001FF42 File Offset: 0x0001E142
		protected virtual IWcfLogReceiverClient CreateLogReceiver()
		{
			return this.CreateWcfLogReceiverClient();
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0001FF4C File Offset: 0x0001E14C
		private void ClientOnProcessLogMessagesCompleted(object sender, AsyncCompletedEventArgs asyncCompletedEventArgs)
		{
			IWcfLogReceiverClient wcfLogReceiverClient = sender as IWcfLogReceiverClient;
			if (wcfLogReceiverClient != null && wcfLogReceiverClient.State == CommunicationState.Opened)
			{
				try
				{
					wcfLogReceiverClient.Close();
				}
				catch
				{
					wcfLogReceiverClient.Abort();
				}
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0001FF90 File Offset: 0x0001E190
		private void SendBufferedEvents()
		{
			lock (base.SyncRoot)
			{
				AsyncLogEventInfo[] eventsAndClear = this.buffer.GetEventsAndClear();
				if (eventsAndClear.Length > 0)
				{
					NLogEvents events = this.TranslateLogEvents(eventsAndClear);
					this.Send(events, eventsAndClear);
				}
				else
				{
					this.inCall = false;
				}
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		private NLogEvent TranslateEvent(LogEventInfo eventInfo, NLogEvents context, Dictionary<string, int> stringTable)
		{
			NLogEvent nlogEvent = new NLogEvent();
			nlogEvent.Id = eventInfo.SequenceID;
			nlogEvent.MessageOrdinal = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, eventInfo.FormattedMessage);
			nlogEvent.LevelOrdinal = eventInfo.Level.Ordinal;
			nlogEvent.LoggerOrdinal = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, eventInfo.LoggerName);
			nlogEvent.TimeDelta = eventInfo.TimeStamp.ToUniversalTime().Ticks - context.BaseTimeUtc;
			for (int i = 0; i < this.Parameters.Count; i++)
			{
				MethodCallParameter methodCallParameter = this.Parameters[i];
				string value = methodCallParameter.Layout.Render(eventInfo);
				int item = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, value);
				nlogEvent.ValueIndexes.Add(item);
			}
			for (int j = this.Parameters.Count; j < context.LayoutNames.Count; j++)
			{
				object value2;
				string value3;
				if (eventInfo.Properties.TryGetValue(context.LayoutNames[j], out value2))
				{
					value3 = Convert.ToString(value2, CultureInfo.InvariantCulture);
				}
				else
				{
					value3 = string.Empty;
				}
				int item2 = LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, value3);
				nlogEvent.ValueIndexes.Add(item2);
			}
			if (eventInfo.Exception != null)
			{
				nlogEvent.ValueIndexes.Add(LogReceiverWebServiceTarget.AddValueAndGetStringOrdinal(context, stringTable, eventInfo.Exception.ToString()));
			}
			return nlogEvent;
		}

		// Token: 0x04000388 RID: 904
		private LogEventInfoBuffer buffer = new LogEventInfoBuffer(10000, false, 10000);

		// Token: 0x04000389 RID: 905
		private bool inCall;
	}
}
