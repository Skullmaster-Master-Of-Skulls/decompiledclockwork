using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace NLog.LogReceiverService
{
	// Token: 0x02000134 RID: 308
	[DataContract(Name = "events", Namespace = "http://nlog-project.org/ws/")]
	[XmlType(Namespace = "http://nlog-project.org/ws/")]
	[XmlRoot("events", Namespace = "http://nlog-project.org/ws/")]
	[DebuggerDisplay("Count = {Events.Length}")]
	public class NLogEvents
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x000190E9 File Offset: 0x000172E9
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x000190F1 File Offset: 0x000172F1
		[XmlElement("cli", Order = 0)]
		[DataMember(Name = "cli", Order = 0)]
		public string ClientName { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x000190FA File Offset: 0x000172FA
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x00019102 File Offset: 0x00017302
		[XmlElement("bts", Order = 1)]
		[DataMember(Name = "bts", Order = 1)]
		public long BaseTimeUtc { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001910B File Offset: 0x0001730B
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x00019113 File Offset: 0x00017313
		[XmlArray("lts", Order = 100)]
		[DataMember(Name = "lts", Order = 100)]
		[XmlArrayItem("l")]
		public StringCollection LayoutNames { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0001911C File Offset: 0x0001731C
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00019124 File Offset: 0x00017324
		[DataMember(Name = "str", Order = 200)]
		[XmlArrayItem("l")]
		[XmlArray("str", Order = 200)]
		public StringCollection Strings { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0001912D File Offset: 0x0001732D
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x00019135 File Offset: 0x00017335
		[DataMember(Name = "ev", Order = 1000)]
		[XmlArrayItem("e")]
		[XmlArray("ev", Order = 1000)]
		public NLogEvent[] Events { get; set; }

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00019140 File Offset: 0x00017340
		public IList<LogEventInfo> ToEventInfo(string loggerNamePrefix)
		{
			LogEventInfo[] array = new LogEventInfo[this.Events.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Events[i].ToEventInfo(this, loggerNamePrefix);
			}
			return array;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0001917C File Offset: 0x0001737C
		public IList<LogEventInfo> ToEventInfo()
		{
			return this.ToEventInfo(string.Empty);
		}
	}
}
