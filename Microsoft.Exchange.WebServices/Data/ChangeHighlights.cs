using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200003E RID: 62
	public sealed class ChangeHighlights : ComplexProperty
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000B54F File Offset: 0x0000A54F
		internal ChangeHighlights()
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000B558 File Offset: 0x0000A558
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			switch (localName = reader.LocalName)
			{
			case "HasLocationChanged":
				this.hasLocationChanged = reader.ReadElementValue<bool>();
				return true;
			case "Location":
				this.location = reader.ReadElementValue();
				return true;
			case "HasStartTimeChanged":
				this.hasStartTimeChanged = reader.ReadElementValue<bool>();
				return true;
			case "Start":
				this.start = reader.ReadElementValueAsDateTime().Value;
				return true;
			case "HasEndTimeChanged":
				this.hasEndTimeChanged = reader.ReadElementValue<bool>();
				return true;
			case "End":
				this.end = reader.ReadElementValueAsDateTime().Value;
				return true;
			}
			return false;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000B66C File Offset: 0x0000A66C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string key;
				switch (key = text)
				{
				case "HasLocationChanged":
					this.hasLocationChanged = jsonProperty.ReadAsBool(text);
					break;
				case "Location":
					this.location = jsonProperty.ReadAsString(text);
					break;
				case "HasStartTimeChanged":
					this.hasStartTimeChanged = jsonProperty.ReadAsBool(text);
					break;
				case "Start":
					this.start = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
					break;
				case "HasEndTimeChanged":
					this.hasEndTimeChanged = jsonProperty.ReadAsBool(text);
					break;
				case "End":
					this.end = service.ConvertUniversalDateTimeStringToLocalDateTime(jsonProperty.ReadAsString(text)).Value;
					break;
				}
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B7DC File Offset: 0x0000A7DC
		public bool HasLocationChanged
		{
			get
			{
				return this.hasLocationChanged;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B7E4 File Offset: 0x0000A7E4
		public string Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B7EC File Offset: 0x0000A7EC
		public bool HasStartTimeChanged
		{
			get
			{
				return this.hasStartTimeChanged;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B7F4 File Offset: 0x0000A7F4
		public DateTime Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B7FC File Offset: 0x0000A7FC
		public bool HasEndTimeChanged
		{
			get
			{
				return this.hasEndTimeChanged;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B804 File Offset: 0x0000A804
		public DateTime End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x04000137 RID: 311
		private bool hasLocationChanged;

		// Token: 0x04000138 RID: 312
		private string location;

		// Token: 0x04000139 RID: 313
		private bool hasStartTimeChanged;

		// Token: 0x0400013A RID: 314
		private DateTime start;

		// Token: 0x0400013B RID: 315
		private bool hasEndTimeChanged;

		// Token: 0x0400013C RID: 316
		private DateTime end;
	}
}
