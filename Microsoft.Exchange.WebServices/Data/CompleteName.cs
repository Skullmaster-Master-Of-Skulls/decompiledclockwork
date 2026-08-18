using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000043 RID: 67
	public sealed class CompleteName : ComplexProperty
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000BE77 File Offset: 0x0000AE77
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0000BE7F File Offset: 0x0000AE7F
		public string GivenName
		{
			get
			{
				return this.givenName;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000BE87 File Offset: 0x0000AE87
		public string MiddleName
		{
			get
			{
				return this.middleName;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000BE8F File Offset: 0x0000AE8F
		public string Surname
		{
			get
			{
				return this.surname;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000BE97 File Offset: 0x0000AE97
		public string Suffix
		{
			get
			{
				return this.suffix;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000BE9F File Offset: 0x0000AE9F
		public string Initials
		{
			get
			{
				return this.initials;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000BEA7 File Offset: 0x0000AEA7
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000BEAF File Offset: 0x0000AEAF
		public string NickName
		{
			get
			{
				return this.nickname;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000BEB7 File Offset: 0x0000AEB7
		public string YomiGivenName
		{
			get
			{
				return this.yomiGivenName;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000BEBF File Offset: 0x0000AEBF
		public string YomiSurname
		{
			get
			{
				return this.yomiSurname;
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000BEC8 File Offset: 0x0000AEC8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			switch (localName = reader.LocalName)
			{
			case "Title":
				this.title = reader.ReadElementValue();
				return true;
			case "FirstName":
				this.givenName = reader.ReadElementValue();
				return true;
			case "MiddleName":
				this.middleName = reader.ReadElementValue();
				return true;
			case "LastName":
				this.surname = reader.ReadElementValue();
				return true;
			case "Suffix":
				this.suffix = reader.ReadElementValue();
				return true;
			case "Initials":
				this.initials = reader.ReadElementValue();
				return true;
			case "FullName":
				this.fullName = reader.ReadElementValue();
				return true;
			case "Nickname":
				this.nickname = reader.ReadElementValue();
				return true;
			case "YomiFirstName":
				this.yomiGivenName = reader.ReadElementValue();
				return true;
			case "YomiLastName":
				this.yomiSurname = reader.ReadElementValue();
				return true;
			}
			return false;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C04C File Offset: 0x0000B04C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string key;
				switch (key = text)
				{
				case "Title":
					this.title = jsonProperty.ReadAsString(text);
					break;
				case "FirstName":
					this.givenName = jsonProperty.ReadAsString(text);
					break;
				case "MiddleName":
					this.middleName = jsonProperty.ReadAsString(text);
					break;
				case "LastName":
					this.surname = jsonProperty.ReadAsString(text);
					break;
				case "Suffix":
					this.suffix = jsonProperty.ReadAsString(text);
					break;
				case "Initials":
					this.initials = jsonProperty.ReadAsString(text);
					break;
				case "FullName":
					this.fullName = jsonProperty.ReadAsString(text);
					break;
				case "Nickname":
					this.nickname = jsonProperty.ReadAsString(text);
					break;
				case "YomiFirstName":
					this.yomiGivenName = jsonProperty.ReadAsString(text);
					break;
				case "YomiLastName":
					this.yomiSurname = jsonProperty.ReadAsString(text);
					break;
				}
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000C224 File Offset: 0x0000B224
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Types, "Title", this.Title);
			writer.WriteElementValue(XmlNamespace.Types, "FirstName", this.GivenName);
			writer.WriteElementValue(XmlNamespace.Types, "MiddleName", this.MiddleName);
			writer.WriteElementValue(XmlNamespace.Types, "LastName", this.Surname);
			writer.WriteElementValue(XmlNamespace.Types, "Suffix", this.Suffix);
			writer.WriteElementValue(XmlNamespace.Types, "Initials", this.Initials);
			writer.WriteElementValue(XmlNamespace.Types, "FullName", this.FullName);
			writer.WriteElementValue(XmlNamespace.Types, "Nickname", this.NickName);
			writer.WriteElementValue(XmlNamespace.Types, "YomiFirstName", this.YomiGivenName);
			writer.WriteElementValue(XmlNamespace.Types, "YomiLastName", this.YomiSurname);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000C2E8 File Offset: 0x0000B2E8
		internal override object InternalToJson(ExchangeService service)
		{
			return new JsonObject
			{
				{
					"Title",
					this.Title
				},
				{
					"FirstName",
					this.GivenName
				},
				{
					"MiddleName",
					this.MiddleName
				},
				{
					"LastName",
					this.Surname
				},
				{
					"Suffix",
					this.Suffix
				},
				{
					"Initials",
					this.Initials
				},
				{
					"FullName",
					this.FullName
				},
				{
					"Nickname",
					this.NickName
				},
				{
					"YomiFirstName",
					this.YomiGivenName
				},
				{
					"YomiLastName",
					this.YomiSurname
				}
			};
		}

		// Token: 0x04000151 RID: 337
		private string title;

		// Token: 0x04000152 RID: 338
		private string givenName;

		// Token: 0x04000153 RID: 339
		private string middleName;

		// Token: 0x04000154 RID: 340
		private string surname;

		// Token: 0x04000155 RID: 341
		private string suffix;

		// Token: 0x04000156 RID: 342
		private string initials;

		// Token: 0x04000157 RID: 343
		private string fullName;

		// Token: 0x04000158 RID: 344
		private string nickname;

		// Token: 0x04000159 RID: 345
		private string yomiGivenName;

		// Token: 0x0400015A RID: 346
		private string yomiSurname;
	}
}
