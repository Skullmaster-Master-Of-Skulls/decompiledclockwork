using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace NLog.LogReceiverService
{
	// Token: 0x02000133 RID: 307
	[XmlType(Namespace = "http://nlog-project.org/ws/")]
	[DataContract(Name = "e", Namespace = "http://nlog-project.org/ws/")]
	[DebuggerDisplay("Event ID = {Id} Level={LevelName} Values={Values.Count}")]
	public class NLogEvent
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x00018ECA File Offset: 0x000170CA
		public NLogEvent()
		{
			this.ValueIndexes = new List<int>();
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00018EDD File Offset: 0x000170DD
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x00018EE5 File Offset: 0x000170E5
		[XmlElement("id", Order = 0)]
		[DataMember(Name = "id", Order = 0)]
		public int Id { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00018EEE File Offset: 0x000170EE
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x00018EF6 File Offset: 0x000170F6
		[XmlElement("lv", Order = 1)]
		[DataMember(Name = "lv", Order = 1)]
		public int LevelOrdinal { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x00018EFF File Offset: 0x000170FF
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x00018F07 File Offset: 0x00017107
		[DataMember(Name = "lg", Order = 2)]
		[XmlElement("lg", Order = 2)]
		public int LoggerOrdinal { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x00018F10 File Offset: 0x00017110
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00018F18 File Offset: 0x00017118
		[DataMember(Name = "ts", Order = 3)]
		[XmlElement("ts", Order = 3)]
		public long TimeDelta { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00018F21 File Offset: 0x00017121
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x00018F29 File Offset: 0x00017129
		[XmlElement("m", Order = 4)]
		[DataMember(Name = "m", Order = 4)]
		public int MessageOrdinal { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00018F34 File Offset: 0x00017134
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x00018FAC File Offset: 0x000171AC
		[XmlElement("val", Order = 100)]
		[DataMember(Name = "val", Order = 100)]
		public string Values
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				string value = string.Empty;
				if (this.ValueIndexes != null)
				{
					foreach (int value2 in this.ValueIndexes)
					{
						stringBuilder.Append(value);
						stringBuilder.Append(value2);
						value = "|";
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (this.ValueIndexes != null)
				{
					this.ValueIndexes.Clear();
				}
				else
				{
					this.ValueIndexes = new List<int>();
				}
				if (!string.IsNullOrEmpty(value))
				{
					string[] array = value.Split(new char[]
					{
						'|'
					});
					foreach (string value2 in array)
					{
						this.ValueIndexes.Add(Convert.ToInt32(value2, CultureInfo.InvariantCulture));
					}
				}
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00019024 File Offset: 0x00017224
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0001902C File Offset: 0x0001722C
		[XmlIgnore]
		[IgnoreDataMember]
		internal IList<int> ValueIndexes { get; private set; }

		// Token: 0x06000A9D RID: 2717 RVA: 0x00019038 File Offset: 0x00017238
		internal LogEventInfo ToEventInfo(NLogEvents context, string loggerNamePrefix)
		{
			LogEventInfo logEventInfo = new LogEventInfo(LogLevel.FromOrdinal(this.LevelOrdinal), loggerNamePrefix + context.Strings[this.LoggerOrdinal], context.Strings[this.MessageOrdinal]);
			logEventInfo.TimeStamp = new DateTime(context.BaseTimeUtc + this.TimeDelta, DateTimeKind.Utc).ToLocalTime();
			for (int i = 0; i < context.LayoutNames.Count; i++)
			{
				string key = context.LayoutNames[i];
				string value = context.Strings[this.ValueIndexes[i]];
				logEventInfo.Properties[key] = value;
			}
			return logEventInfo;
		}
	}
}
