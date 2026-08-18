using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000093 RID: 147
	public sealed class Rule : ComplexProperty
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x0001683C File Offset: 0x0001583C
		public Rule()
		{
			this.priority = 1;
			this.isEnabled = true;
			this.conditions = new RulePredicates();
			this.actions = new RuleActions();
			this.exceptions = new RulePredicates();
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00016873 File Offset: 0x00015873
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001687B File Offset: 0x0001587B
		public string Id
		{
			get
			{
				return this.ruleId;
			}
			set
			{
				this.SetFieldValue<string>(ref this.ruleId, value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001688A File Offset: 0x0001588A
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x00016892 File Offset: 0x00015892
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.SetFieldValue<string>(ref this.displayName, value);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000168A1 File Offset: 0x000158A1
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x000168A9 File Offset: 0x000158A9
		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.SetFieldValue<int>(ref this.priority, value);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000168B8 File Offset: 0x000158B8
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x000168C0 File Offset: 0x000158C0
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isEnabled, value);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000168CF File Offset: 0x000158CF
		public bool IsNotSupported
		{
			get
			{
				return this.isNotSupported;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x000168D7 File Offset: 0x000158D7
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x000168DF File Offset: 0x000158DF
		public bool IsInError
		{
			get
			{
				return this.isInError;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isInError, value);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x000168EE File Offset: 0x000158EE
		public RulePredicates Conditions
		{
			get
			{
				return this.conditions;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x000168F6 File Offset: 0x000158F6
		public RuleActions Actions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000168FE File Offset: 0x000158FE
		public RulePredicates Exceptions
		{
			get
			{
				return this.exceptions;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00016908 File Offset: 0x00015908
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			switch (localName = reader.LocalName)
			{
			case "DisplayName":
				this.displayName = reader.ReadElementValue();
				return true;
			case "RuleId":
				this.ruleId = reader.ReadElementValue();
				return true;
			case "Priority":
				this.priority = reader.ReadElementValue<int>();
				return true;
			case "IsEnabled":
				this.isEnabled = reader.ReadElementValue<bool>();
				return true;
			case "IsNotSupported":
				this.isNotSupported = reader.ReadElementValue<bool>();
				return true;
			case "IsInError":
				this.isInError = reader.ReadElementValue<bool>();
				return true;
			case "Conditions":
				this.conditions.LoadFromXml(reader, reader.LocalName);
				return true;
			case "Actions":
				this.actions.LoadFromXml(reader, reader.LocalName);
				return true;
			case "Exceptions":
				this.exceptions.LoadFromXml(reader, reader.LocalName);
				return true;
			}
			return false;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00016A7C File Offset: 0x00015A7C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string key;
				switch (key = text)
				{
				case "DisplayName":
					this.displayName = jsonProperty.ReadAsString(text);
					break;
				case "RuleId":
					this.ruleId = jsonProperty.ReadAsString(text);
					break;
				case "Priority":
					this.priority = jsonProperty.ReadAsInt(text);
					break;
				case "IsEnabled":
					this.isEnabled = jsonProperty.ReadAsBool(text);
					break;
				case "IsNotSupported":
					this.isNotSupported = jsonProperty.ReadAsBool(text);
					break;
				case "IsInError":
					this.isInError = jsonProperty.ReadAsBool(text);
					break;
				case "Conditions":
					this.conditions.LoadFromJson(jsonProperty, service);
					break;
				case "Actions":
					this.actions.LoadFromJson(jsonProperty, service);
					break;
				case "Exceptions":
					this.exceptions.LoadFromJson(jsonProperty, service);
					break;
				}
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00016C30 File Offset: 0x00015C30
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.Id))
			{
				writer.WriteElementValue(XmlNamespace.Types, "RuleId", this.Id);
			}
			writer.WriteElementValue(XmlNamespace.Types, "DisplayName", this.DisplayName);
			writer.WriteElementValue(XmlNamespace.Types, "Priority", this.Priority);
			writer.WriteElementValue(XmlNamespace.Types, "IsEnabled", this.IsEnabled);
			writer.WriteElementValue(XmlNamespace.Types, "IsInError", this.IsInError);
			this.Conditions.WriteToXml(writer, "Conditions");
			this.Exceptions.WriteToXml(writer, "Exceptions");
			this.Actions.WriteToXml(writer, "Actions");
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00016CE8 File Offset: 0x00015CE8
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			if (!string.IsNullOrEmpty(this.Id))
			{
				jsonObject.Add("RuleId", this.Id);
			}
			jsonObject.Add("DisplayName", this.DisplayName);
			jsonObject.Add("Priority", this.Priority);
			jsonObject.Add("IsEnabled", this.IsEnabled);
			jsonObject.Add("IsInError", this.IsInError);
			jsonObject.Add("Conditions", this.Conditions.InternalToJson(service));
			jsonObject.Add("Exceptions", this.Exceptions.InternalToJson(service));
			jsonObject.Add("Actions", this.Actions.InternalToJson(service));
			return jsonObject;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00016DA4 File Offset: 0x00015DA4
		internal override void InternalValidate()
		{
			base.InternalValidate();
			EwsUtilities.ValidateParam(this.displayName, "DisplayName");
			EwsUtilities.ValidateParam(this.conditions, "Conditions");
			EwsUtilities.ValidateParam(this.exceptions, "Exceptions");
			EwsUtilities.ValidateParam(this.actions, "Actions");
		}

		// Token: 0x0400021D RID: 541
		private string ruleId;

		// Token: 0x0400021E RID: 542
		private string displayName;

		// Token: 0x0400021F RID: 543
		private int priority;

		// Token: 0x04000220 RID: 544
		private bool isEnabled;

		// Token: 0x04000221 RID: 545
		private bool isNotSupported;

		// Token: 0x04000222 RID: 546
		private bool isInError;

		// Token: 0x04000223 RID: 547
		private RulePredicates conditions;

		// Token: 0x04000224 RID: 548
		private RuleActions actions;

		// Token: 0x04000225 RID: 549
		private RulePredicates exceptions;
	}
}
