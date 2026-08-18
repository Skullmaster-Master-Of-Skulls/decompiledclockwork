using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace MailBee.Mime
{
	// Token: 0x0200052E RID: 1326
	public class EmailAddressCollection : CollectionBase
	{
		// Token: 0x06002BE5 RID: 11237 RVA: 0x000CFBB2 File Offset: 0x000CEBB2
		public EmailAddressCollection()
		{
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000CFBBA File Offset: 0x000CEBBA
		public EmailAddressCollection(string emails)
		{
			this.AsString = emails;
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000CFBC9 File Offset: 0x000CEBC9
		internal EmailAddressCollection(Header A_0)
		{
			this.a = A_0;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000CFBD8 File Offset: 0x000CEBD8
		// (set) Token: 0x06002BE9 RID: 11241 RVA: 0x000CFBE0 File Offset: 0x000CEBE0
		public string AsString
		{
			get
			{
				return this.ToString();
			}
			set
			{
				base.List.Clear();
				foreach (object obj in EmailAddressCollection.Parse(value))
				{
					EmailAddress emailAddress = (EmailAddress)obj;
					emailAddress.EmailAddressHeader = this.a;
					this.Add(emailAddress);
				}
				this.a();
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000CFC58 File Offset: 0x000CEC58
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000CFC60 File Offset: 0x000CEC60
		internal Header RecipientsHeader
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		public EmailAddress this[int index]
		{
			get
			{
				return (EmailAddress)base.List[index];
			}
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000CFC7C File Offset: 0x000CEC7C
		public void Add(EmailAddress address)
		{
			if (address == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			address.EmailAddressHeader = this.a;
			base.List.Add(address);
			this.a();
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000CFCA8 File Offset: 0x000CECA8
		public void Add(EmailAddressCollection addresses)
		{
			if (addresses == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			for (int i = 0; i < addresses.Count; i++)
			{
				this.Add(addresses[i]);
			}
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000CFCDE File Offset: 0x000CECDE
		public void Add(string email)
		{
			this.Add(new EmailAddress(email));
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000CFCEC File Offset: 0x000CECEC
		public void Add(string email, string name)
		{
			this.Add(new EmailAddress(email, name));
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000CFCFB File Offset: 0x000CECFB
		public void Add(string email, string name, string remarks)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.Add(new EmailAddress(email, name, remarks));
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000CFD16 File Offset: 0x000CED16
		public void AddFromString(string addressString)
		{
			if (addressString == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.Add(EmailAddressCollection.a(addressString, this.a));
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000CFD35 File Offset: 0x000CED35
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
			this.a();
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000CFD4C File Offset: 0x000CED4C
		public void Remove(string email)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			for (int i = 0; i < base.List.Count; i++)
			{
				if (((EmailAddress)base.List[i]).Email == email)
				{
					base.List.RemoveAt(i);
				}
			}
			this.a();
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000CFDAA File Offset: 0x000CEDAA
		public new void Clear()
		{
			base.List.Clear();
			this.a();
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000CFDBD File Offset: 0x000CEDBD
		public static EmailAddressCollection Parse(string addressString)
		{
			return EmailAddressCollection.a(addressString, null);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000CFDC8 File Offset: 0x000CEDC8
		internal static EmailAddressCollection a(string A_0, Header A_1)
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			if (A_0 == null)
			{
				return emailAddressCollection;
			}
			emailAddressCollection.RecipientsHeader = A_1;
			if (A_1 != null)
			{
				A_1.ValueInternal = A_0;
			}
			string text = A_0.Trim();
			int num = 0;
			bool flag = false;
			char c = '"';
			bool flag2 = false;
			bool flag3 = false;
			int i = 0;
			while (i < text.Length)
			{
				char c2 = text[i];
				if (c2 <= ',')
				{
					if (c2 != '"')
					{
						switch (c2)
						{
						case '(':
							if (!flag3)
							{
								flag3 = true;
							}
							break;
						case ')':
							if (flag3)
							{
								flag3 = false;
							}
							break;
						case ',':
							goto IL_145;
						}
					}
					else if (!flag)
					{
						c = text[i];
						flag = true;
					}
					else if (c == text[i] && text[i - 1] != '\\')
					{
						flag = false;
					}
				}
				else
				{
					switch (c2)
					{
					case ';':
						goto IL_145;
					case '<':
						if (!flag2)
						{
							flag2 = true;
						}
						break;
					case '=':
						if (i < text.Length - 2 && text[i + 1] == '?')
						{
							int num2 = text.IndexOf("?=", i + 2);
							if (num2 > -1)
							{
								i = num2 + 1;
							}
						}
						break;
					case '>':
						if (flag2)
						{
							flag2 = false;
						}
						break;
					default:
						if (c2 == '\\')
						{
							if (flag)
							{
								i++;
							}
						}
						break;
					}
				}
				IL_16F:
				i++;
				continue;
				IL_145:
				if (!flag2 && !flag3 && !flag)
				{
					int num3 = i;
					emailAddressCollection.Add(EmailAddress.a(text.Substring(num, num3 - num), A_1));
					num = i + 1;
					goto IL_16F;
				}
				goto IL_16F;
			}
			if (num < i)
			{
				emailAddressCollection.Add(EmailAddress.a(text.Substring(num, i - num), A_1));
			}
			return emailAddressCollection;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000CFF74 File Offset: 0x000CEF74
		public override string ToString()
		{
			string a_ = ",";
			if (this.a != null && this.a.ParentCollection != null && this.a.ParentCollection.MimePart != null && this.a.ParentCollection.MimePart.ParentMessage != null && this.a.ParentCollection.MimePart.ParentMessage.Builder != null)
			{
				if (this.a.ParentCollection.MimePart.ParentMessage.Builder.AddressDelimeter == AddressDelimeterChar.Comma)
				{
					a_ = ",";
				}
				else if (this.a.ParentCollection.MimePart.ParentMessage.Builder.AddressDelimeter == AddressDelimeterChar.Semicolon)
				{
					a_ = ";";
				}
			}
			return this.a(a_, false, MailTransferEncoding.None, null);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000D0045 File Offset: 0x000CF045
		public static implicit operator string(EmailAddressCollection emails)
		{
			if (emails != null)
			{
				return emails.ToString();
			}
			return null;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000D0054 File Offset: 0x000CF054
		internal string a(string A_0, bool A_1, MailTransferEncoding A_2, string A_3)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.List.Count > 0)
			{
				foreach (object obj in base.List)
				{
					EmailAddress emailAddress = (EmailAddress)obj;
					stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}{1} ", new object[]
					{
						A_1 ? emailAddress.a(A_2, A_3) : emailAddress.ToString(),
						A_0
					}));
				}
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000D0108 File Offset: 0x000CF108
		internal void a()
		{
			if (this.a != null)
			{
				this.a.d();
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000D0120 File Offset: 0x000CF120
		internal void c()
		{
			if (this.a != null)
			{
				base.List.Clear();
				foreach (object obj in EmailAddressCollection.Parse(this.a.Value.Replace("\r\n", string.Empty)))
				{
					EmailAddress emailAddress = (EmailAddress)obj;
					emailAddress.EmailAddressHeader = this.a;
					base.List.Add(emailAddress);
				}
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000D01B8 File Offset: 0x000CF1B8
		public EmailAddressCollection ToIdnAddress()
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			foreach (object obj in this)
			{
				EmailAddress emailAddress = (EmailAddress)obj;
				emailAddressCollection.Add(emailAddress.ToIdnAddress());
			}
			return emailAddressCollection;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000D0218 File Offset: 0x000CF218
		public EmailAddressCollection FromIdnAddress()
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			foreach (object obj in this)
			{
				EmailAddress emailAddress = (EmailAddress)obj;
				emailAddressCollection.Add(emailAddress.FromIdnAddress());
			}
			return emailAddressCollection;
		}

		// Token: 0x04001E1F RID: 7711
		private Header a;
	}
}
