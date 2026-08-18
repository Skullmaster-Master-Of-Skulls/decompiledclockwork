using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000077 RID: 119
	public sealed class MeetingSuggestion : ExtractedEntity
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x00012B54 File Offset: 0x00011B54
		internal MeetingSuggestion()
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00012B5C File Offset: 0x00011B5C
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00012B64 File Offset: 0x00011B64
		public EmailUserEntityCollection Attendees { get; internal set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00012B6D File Offset: 0x00011B6D
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00012B75 File Offset: 0x00011B75
		public string Location { get; internal set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00012B7E File Offset: 0x00011B7E
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00012B86 File Offset: 0x00011B86
		public string Subject { get; internal set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00012B8F File Offset: 0x00011B8F
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00012B97 File Offset: 0x00011B97
		public string MeetingString { get; internal set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00012BA0 File Offset: 0x00011BA0
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00012BA8 File Offset: 0x00011BA8
		public DateTime? StartTime { get; internal set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00012BB1 File Offset: 0x00011BB1
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00012BB9 File Offset: 0x00011BB9
		public DateTime? EndTime { get; internal set; }

		// Token: 0x0600055E RID: 1374 RVA: 0x00012BC4 File Offset: 0x00011BC4
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			switch (localName = reader.LocalName)
			{
			case "Attendees":
				this.Attendees = new EmailUserEntityCollection();
				this.Attendees.LoadFromXml(reader, XmlNamespace.Types, "Attendees");
				return true;
			case "Location":
				this.Location = reader.ReadElementValue();
				return true;
			case "Subject":
				this.Subject = reader.ReadElementValue();
				return true;
			case "MeetingString":
				this.MeetingString = reader.ReadElementValue();
				return true;
			case "StartTime":
				this.StartTime = reader.ReadElementValueAsDateTime();
				return true;
			case "EndTime":
				this.EndTime = reader.ReadElementValueAsDateTime();
				return true;
			}
			return base.TryReadElementFromXml(reader);
		}
	}
}
