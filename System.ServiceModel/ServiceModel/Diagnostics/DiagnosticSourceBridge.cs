using System;
using System.Diagnostics.Tracing;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A76 RID: 2678
	[EventSource(Name = "Microsoft-Windows-Application ServiceModel-DiagnosticSource-Bridge")]
	internal sealed class DiagnosticSourceBridge : EventSource
	{
		// Token: 0x06006992 RID: 27026 RVA: 0x00189B72 File Offset: 0x00187D72
		[Event(1, Keywords = (EventKeywords)1L, Level = EventLevel.Verbose)]
		public void DispatchMessageInspectorAfterReceive(string TypeName, long Duration)
		{
			base.WriteEvent(1, TypeName, Duration);
		}

		// Token: 0x06006993 RID: 27027 RVA: 0x00189B7D File Offset: 0x00187D7D
		[Event(2, Keywords = (EventKeywords)1L, Level = EventLevel.Verbose)]
		public void DispatchMessageInspectorBeforeSend(string TypeName, long Duration)
		{
			base.WriteEvent(2, TypeName, Duration);
		}

		// Token: 0x06006994 RID: 27028 RVA: 0x00189B88 File Offset: 0x00187D88
		[Event(3, Keywords = (EventKeywords)1L, Level = EventLevel.Verbose)]
		public void ClientMessageInspectorAfterReceive(string TypeName, long Duration)
		{
			base.WriteEvent(3, TypeName, Duration);
		}

		// Token: 0x06006995 RID: 27029 RVA: 0x00189B93 File Offset: 0x00187D93
		[Event(4, Keywords = (EventKeywords)1L, Level = EventLevel.Verbose)]
		public void ClientMessageInspectorBeforeSend(string TypeName, long Duration)
		{
			base.WriteEvent(4, TypeName, Duration);
		}

		// Token: 0x06006996 RID: 27030 RVA: 0x00189B9E File Offset: 0x00187D9E
		[Event(5, Keywords = (EventKeywords)2L, Level = EventLevel.Verbose)]
		public void ParameterInspectorAfter(string TypeName, long Duration)
		{
			base.WriteEvent(5, TypeName, Duration);
		}

		// Token: 0x06006997 RID: 27031 RVA: 0x00189BA9 File Offset: 0x00187DA9
		[Event(6, Keywords = (EventKeywords)2L, Level = EventLevel.Verbose)]
		public void ParameterInspectorBefore(string TypeName, long Duration)
		{
			base.WriteEvent(6, TypeName, Duration);
		}

		// Token: 0x06006998 RID: 27032 RVA: 0x00189BB4 File Offset: 0x00187DB4
		[Event(7, Keywords = (EventKeywords)4L, Level = EventLevel.Verbose)]
		public void DispatchMessageFormatterDeserialize(string TypeName, long Duration)
		{
			base.WriteEvent(7, TypeName, Duration);
		}

		// Token: 0x06006999 RID: 27033 RVA: 0x00189BBF File Offset: 0x00187DBF
		[Event(8, Keywords = (EventKeywords)4L, Level = EventLevel.Verbose)]
		public void DispatchMessageFormatterSerialize(string TypeName, long Duration)
		{
			base.WriteEvent(8, TypeName, Duration);
		}

		// Token: 0x0600699A RID: 27034 RVA: 0x00189BCA File Offset: 0x00187DCA
		[Event(9, Keywords = (EventKeywords)4L, Level = EventLevel.Verbose)]
		public void ClientMessageFormatterDeserialize(string TypeName, long Duration)
		{
			base.WriteEvent(9, TypeName, Duration);
		}

		// Token: 0x0600699B RID: 27035 RVA: 0x00189BD6 File Offset: 0x00187DD6
		[Event(10, Keywords = (EventKeywords)4L, Level = EventLevel.Verbose)]
		public void ClientMessageFormatterSerialize(string TypeName, long Duration)
		{
			base.WriteEvent(10, TypeName, Duration);
		}

		// Token: 0x0600699C RID: 27036 RVA: 0x00189BE2 File Offset: 0x00187DE2
		[Event(11, Keywords = (EventKeywords)8L, Level = EventLevel.Verbose)]
		public void DispatchSelectOperation(string TypeName, string SelectedOperation, long Duration)
		{
			base.WriteEvent(11, new object[]
			{
				TypeName,
				SelectedOperation,
				Duration
			});
		}

		// Token: 0x0600699D RID: 27037 RVA: 0x00189C03 File Offset: 0x00187E03
		[Event(12, Keywords = (EventKeywords)8L, Level = EventLevel.Verbose)]
		public void ClientSelectOperation(string TypeName, string SelectedOperation, long Duration)
		{
			base.WriteEvent(12, new object[]
			{
				TypeName,
				SelectedOperation,
				Duration
			});
		}

		// Token: 0x0600699E RID: 27038 RVA: 0x00189C24 File Offset: 0x00187E24
		[Event(13, Keywords = (EventKeywords)16L, Level = EventLevel.Verbose)]
		public void InvokeOperationStart(string TypeName, long Timestamp)
		{
			base.WriteEvent(13, TypeName, Timestamp);
		}

		// Token: 0x0600699F RID: 27039 RVA: 0x00189C30 File Offset: 0x00187E30
		[Event(14, Keywords = (EventKeywords)16L, Level = EventLevel.Verbose)]
		public void InvokeOperationStop(long Timestamp)
		{
			base.WriteEvent(14, Timestamp);
		}

		// Token: 0x060069A0 RID: 27040 RVA: 0x00189C3B File Offset: 0x00187E3B
		[Event(15, Keywords = (EventKeywords)32L, Level = EventLevel.Verbose)]
		public void InstanceProviderGet(string TypeName, int InstanceHash, long Duration)
		{
			base.WriteEvent(15, new object[]
			{
				TypeName,
				InstanceHash,
				Duration
			});
		}

		// Token: 0x060069A1 RID: 27041 RVA: 0x00189C61 File Offset: 0x00187E61
		[Event(16, Keywords = (EventKeywords)32L, Level = EventLevel.Verbose)]
		public void InstanceProviderRelease(string TypeName, int InstanceHash, long Duration)
		{
			base.WriteEvent(16, new object[]
			{
				TypeName,
				InstanceHash,
				Duration
			});
		}

		// Token: 0x060069A2 RID: 27042 RVA: 0x00189C87 File Offset: 0x00187E87
		[Event(17, Keywords = (EventKeywords)64L, Level = EventLevel.Verbose)]
		public void CallThrottled(long Duration)
		{
			base.WriteEvent(17, Duration);
		}

		// Token: 0x060069A3 RID: 27043 RVA: 0x00189C92 File Offset: 0x00187E92
		[Event(18, Keywords = (EventKeywords)64L, Level = EventLevel.Verbose)]
		public void InstanceThrottled(long Duration)
		{
			base.WriteEvent(18, Duration);
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x00189C9D File Offset: 0x00187E9D
		[Event(19, Keywords = (EventKeywords)128L, Level = EventLevel.Verbose)]
		public void Authentication(string TypeName, bool Authenticated, long Duration)
		{
			base.WriteEvent(19, new object[]
			{
				TypeName,
				Authenticated,
				Duration
			});
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x00189CC3 File Offset: 0x00187EC3
		[Event(20, Keywords = (EventKeywords)256L, Level = EventLevel.Verbose)]
		public void Authorization(string TypeName, bool Authorized, long Duration)
		{
			base.WriteEvent(20, new object[]
			{
				TypeName,
				Authorized,
				Duration
			});
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x00189CE9 File Offset: 0x00187EE9
		internal bool IsEnabled(EventKeywords keywords)
		{
			return base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, keywords);
		}

		// Token: 0x02000EA3 RID: 3747
		public class EventIds
		{
			// Token: 0x04004BEB RID: 19435
			public const int DispatchMessageInspectorAfterReceive = 1;

			// Token: 0x04004BEC RID: 19436
			public const int DispatchMessageInspectorBeforeSend = 2;

			// Token: 0x04004BED RID: 19437
			public const int ClientMessageInspectorAfterReceive = 3;

			// Token: 0x04004BEE RID: 19438
			public const int ClientMessageInspectorBeforeSend = 4;

			// Token: 0x04004BEF RID: 19439
			public const int ParameterInspectorAfter = 5;

			// Token: 0x04004BF0 RID: 19440
			public const int ParameterInspectorBefore = 6;

			// Token: 0x04004BF1 RID: 19441
			public const int DispatchMessageFormatterDeserialize = 7;

			// Token: 0x04004BF2 RID: 19442
			public const int DispatchMessageFormatterSerialize = 8;

			// Token: 0x04004BF3 RID: 19443
			public const int ClientMessageFormatterDeserialize = 9;

			// Token: 0x04004BF4 RID: 19444
			public const int ClientMessageFormatterSerialize = 10;

			// Token: 0x04004BF5 RID: 19445
			public const int DispatchSelectOperation = 11;

			// Token: 0x04004BF6 RID: 19446
			public const int ClientSelectOperation = 12;

			// Token: 0x04004BF7 RID: 19447
			public const int InvokeOperationStart = 13;

			// Token: 0x04004BF8 RID: 19448
			public const int InvokeOperationStop = 14;

			// Token: 0x04004BF9 RID: 19449
			public const int InstanceProviderGet = 15;

			// Token: 0x04004BFA RID: 19450
			public const int InstanceProviderRelease = 16;

			// Token: 0x04004BFB RID: 19451
			public const int CallThrottled = 17;

			// Token: 0x04004BFC RID: 19452
			public const int InstanceThrottled = 18;

			// Token: 0x04004BFD RID: 19453
			public const int Authentication = 19;

			// Token: 0x04004BFE RID: 19454
			public const int Authorization = 20;
		}

		// Token: 0x02000EA4 RID: 3748
		public class Keywords
		{
			// Token: 0x04004BFF RID: 19455
			public const EventKeywords MessageInspector = (EventKeywords)1L;

			// Token: 0x04004C00 RID: 19456
			public const EventKeywords ParameterInspector = (EventKeywords)2L;

			// Token: 0x04004C01 RID: 19457
			public const EventKeywords MessageFormatter = (EventKeywords)4L;

			// Token: 0x04004C02 RID: 19458
			public const EventKeywords OperationSelector = (EventKeywords)8L;

			// Token: 0x04004C03 RID: 19459
			public const EventKeywords OperationInvoker = (EventKeywords)16L;

			// Token: 0x04004C04 RID: 19460
			public const EventKeywords InstanceProvider = (EventKeywords)32L;

			// Token: 0x04004C05 RID: 19461
			public const EventKeywords ServiceThrottle = (EventKeywords)64L;

			// Token: 0x04004C06 RID: 19462
			public const EventKeywords Authentication = (EventKeywords)128L;

			// Token: 0x04004C07 RID: 19463
			public const EventKeywords Authorization = (EventKeywords)256L;
		}
	}
}
