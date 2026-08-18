using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000A6 RID: 166
	public sealed class CalendarEventDetails : ComplexProperty
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001938C File Offset: 0x0001838C
		internal CalendarEventDetails()
		{
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00019394 File Offset: 0x00018394
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			switch (localName = reader.LocalName)
			{
			case "ID":
				this.storeId = reader.ReadElementValue();
				return true;
			case "Subject":
				this.subject = reader.ReadElementValue();
				return true;
			case "Location":
				this.location = reader.ReadElementValue();
				return true;
			case "IsMeeting":
				this.isMeeting = reader.ReadElementValue<bool>();
				return true;
			case "IsRecurring":
				this.isRecurring = reader.ReadElementValue<bool>();
				return true;
			case "IsException":
				this.isException = reader.ReadElementValue<bool>();
				return true;
			case "IsReminderSet":
				this.isReminderSet = reader.ReadElementValue<bool>();
				return true;
			case "IsPrivate":
				this.isPrivate = reader.ReadElementValue<bool>();
				return true;
			}
			return false;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000194D4 File Offset: 0x000184D4
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string key;
				switch (key = text)
				{
				case "ID":
					this.storeId = jsonProperty.ReadAsString(text);
					break;
				case "Subject":
					this.subject = jsonProperty.ReadAsString(text);
					break;
				case "Location":
					this.location = jsonProperty.ReadAsString(text);
					break;
				case "IsMeeting":
					this.isMeeting = jsonProperty.ReadAsBool(text);
					break;
				case "IsRecurring":
					this.isRecurring = jsonProperty.ReadAsBool(text);
					break;
				case "IsException":
					this.isException = jsonProperty.ReadAsBool(text);
					break;
				case "IsReminderSet":
					this.isReminderSet = jsonProperty.ReadAsBool(text);
					break;
				case "IsPrivate":
					this.isPrivate = jsonProperty.ReadAsBool(text);
					break;
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00019664 File Offset: 0x00018664
		public string StoreId
		{
			get
			{
				return this.storeId;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001966C File Offset: 0x0001866C
		public string Subject
		{
			get
			{
				return this.subject;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x00019674 File Offset: 0x00018674
		public string Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001967C File Offset: 0x0001867C
		public bool IsMeeting
		{
			get
			{
				return this.isMeeting;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00019684 File Offset: 0x00018684
		public bool IsRecurring
		{
			get
			{
				return this.isRecurring;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001968C File Offset: 0x0001868C
		public bool IsException
		{
			get
			{
				return this.isException;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00019694 File Offset: 0x00018694
		public bool IsReminderSet
		{
			get
			{
				return this.isReminderSet;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0001969C File Offset: 0x0001869C
		public bool IsPrivate
		{
			get
			{
				return this.isPrivate;
			}
		}

		// Token: 0x0400026E RID: 622
		private string storeId;

		// Token: 0x0400026F RID: 623
		private string subject;

		// Token: 0x04000270 RID: 624
		private string location;

		// Token: 0x04000271 RID: 625
		private bool isMeeting;

		// Token: 0x04000272 RID: 626
		private bool isRecurring;

		// Token: 0x04000273 RID: 627
		private bool isException;

		// Token: 0x04000274 RID: 628
		private bool isReminderSet;

		// Token: 0x04000275 RID: 629
		private bool isPrivate;
	}
}
