using System;
using System.Diagnostics.Tracing;

namespace System.ServiceModel
{
	// Token: 0x020000A1 RID: 161
	internal sealed class TelemetryEventSource : EventSource
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00010639 File Offset: 0x0000E839
		public TelemetryEventSource(string eventSourceName) : base(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat, TelemetryEventSource.telemetryTraits)
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00010648 File Offset: 0x0000E848
		public static EventSourceOptions MeasuresOptions()
		{
			return new EventSourceOptions
			{
				Keywords = (EventKeywords)70368744177664L
			};
		}

		// Token: 0x0400092B RID: 2347
		public const EventKeywords Reserved44Keyword = (EventKeywords)17592186044416L;

		// Token: 0x0400092C RID: 2348
		public const EventKeywords TelemetryKeyword = (EventKeywords)35184372088832L;

		// Token: 0x0400092D RID: 2349
		public const EventKeywords MeasuresKeyword = (EventKeywords)70368744177664L;

		// Token: 0x0400092E RID: 2350
		public const EventKeywords CriticalDataKeyword = (EventKeywords)140737488355328L;

		// Token: 0x0400092F RID: 2351
		public const EventTags CoreData = (EventTags)524288;

		// Token: 0x04000930 RID: 2352
		public const EventTags InjectXToken = (EventTags)1048576;

		// Token: 0x04000931 RID: 2353
		public const EventTags RealtimeLatency = (EventTags)2097152;

		// Token: 0x04000932 RID: 2354
		public const EventTags NormalLatency = (EventTags)4194304;

		// Token: 0x04000933 RID: 2355
		public const EventTags CriticalPersistence = (EventTags)8388608;

		// Token: 0x04000934 RID: 2356
		public const EventTags NormalPersistence = (EventTags)16777216;

		// Token: 0x04000935 RID: 2357
		public const EventTags DropPii = (EventTags)33554432;

		// Token: 0x04000936 RID: 2358
		public const EventTags HashPii = (EventTags)67108864;

		// Token: 0x04000937 RID: 2359
		public const EventTags MarkPii = (EventTags)134217728;

		// Token: 0x04000938 RID: 2360
		public const EventFieldTags DropPiiField = (EventFieldTags)67108864;

		// Token: 0x04000939 RID: 2361
		public const EventFieldTags HashPiiField = (EventFieldTags)134217728;

		// Token: 0x0400093A RID: 2362
		private static readonly string[] telemetryTraits = new string[]
		{
			"ETW_GROUP",
			"{4f50731a-89cf-4782-b3e0-dce8c90476ba}"
		};
	}
}
