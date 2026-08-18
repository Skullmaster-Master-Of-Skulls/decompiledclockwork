using System;
using System.Diagnostics.Tracing;

namespace System.Web
{
	// Token: 0x0200001A RID: 26
	internal sealed class TelemetryEventSource : EventSource
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00003BEF File Offset: 0x00001DEF
		internal TelemetryEventSource(string eventSourceName) : base(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat, TelemetryEventSource.telemetryTraits)
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003C00 File Offset: 0x00001E00
		internal static EventSourceOptions TelemetryOptions()
		{
			return new EventSourceOptions
			{
				Keywords = (EventKeywords)35184372088832L
			};
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003C28 File Offset: 0x00001E28
		internal static EventSourceOptions MeasuresOptions()
		{
			return new EventSourceOptions
			{
				Keywords = (EventKeywords)70368744177664L
			};
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003C50 File Offset: 0x00001E50
		internal static EventSourceOptions CriticalDataOptions()
		{
			return new EventSourceOptions
			{
				Keywords = (EventKeywords)140737488355328L
			};
		}

		// Token: 0x040000DB RID: 219
		internal const EventKeywords Reserved44Keyword = (EventKeywords)17592186044416L;

		// Token: 0x040000DC RID: 220
		internal const EventKeywords TelemetryKeyword = (EventKeywords)35184372088832L;

		// Token: 0x040000DD RID: 221
		internal const EventKeywords MeasuresKeyword = (EventKeywords)70368744177664L;

		// Token: 0x040000DE RID: 222
		internal const EventKeywords CriticalDataKeyword = (EventKeywords)140737488355328L;

		// Token: 0x040000DF RID: 223
		internal const EventTags CoreData = (EventTags)524288;

		// Token: 0x040000E0 RID: 224
		internal const EventTags InjectXToken = (EventTags)1048576;

		// Token: 0x040000E1 RID: 225
		internal const EventTags RealtimeLatency = (EventTags)2097152;

		// Token: 0x040000E2 RID: 226
		internal const EventTags NormalLatency = (EventTags)4194304;

		// Token: 0x040000E3 RID: 227
		internal const EventTags CriticalPersistence = (EventTags)8388608;

		// Token: 0x040000E4 RID: 228
		internal const EventTags NormalPersistence = (EventTags)16777216;

		// Token: 0x040000E5 RID: 229
		internal const EventTags DropPii = (EventTags)33554432;

		// Token: 0x040000E6 RID: 230
		internal const EventTags HashPii = (EventTags)67108864;

		// Token: 0x040000E7 RID: 231
		internal const EventTags MarkPii = (EventTags)134217728;

		// Token: 0x040000E8 RID: 232
		internal const EventFieldTags DropPiiField = (EventFieldTags)67108864;

		// Token: 0x040000E9 RID: 233
		internal const EventFieldTags HashPiiField = (EventFieldTags)134217728;

		// Token: 0x040000EA RID: 234
		private static readonly string[] telemetryTraits = new string[]
		{
			"ETW_GROUP",
			"{4f50731a-89cf-4782-b3e0-dce8c90476ba}"
		};
	}
}
