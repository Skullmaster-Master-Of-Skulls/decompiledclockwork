using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using a.k;
using MailBee.Mime;

namespace MailBee.BounceMail
{
	// Token: 0x02000083 RID: 131
	public class Result
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000B3F4 File Offset: 0x0000A3F4
		public MailMessage OriginalMessage
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000B3FC File Offset: 0x0000A3FC
		private string OriginalMessageBodyText
		{
			get
			{
				if (this.b.BodyPlainText != string.Empty)
				{
					return this.b.BodyPlainText;
				}
				if (this.b.BodyHtmlText != string.Empty)
				{
					return this.b.BodyHtmlText;
				}
				foreach (object obj in this.b.Attachments)
				{
					Attachment attachment = (Attachment)obj;
					if (attachment.ContentType == "text/plain")
					{
						return Encoding.Default.GetString(attachment.GetData());
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B4C8 File Offset: 0x0000A4C8
		public RecipientStatusCollection Recipients
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000B4D0 File Offset: 0x0000A4D0
		public DsnAttachment DsnStructure
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000B4D8 File Offset: 0x0000A4D8
		internal string Template
		{
			get
			{
				if (this.e == -1)
				{
					return string.Empty;
				}
				return this.a.g()[this.e];
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000B4FC File Offset: 0x0000A4FC
		internal Result(MailMessage A_0, c A_1, bool A_2, int A_3)
		{
			this.b = A_0;
			this.a = A_1;
			this.h = A_2;
			this.i = A_3;
			this.f = new RecipientStatusCollection();
			foreach (object obj in A_0.Attachments)
			{
				Attachment attachment = (Attachment)obj;
				if (attachment.ContentType != null)
				{
					string text = attachment.ContentType.ToLower();
					if (!(text == "message/rfc822"))
					{
						if (!(text == "text/rfc822-headers"))
						{
							if (text == "message/delivery-status" || text == "message/disposition-notification")
							{
								this.g = new DsnAttachment(attachment, null);
							}
						}
						else
						{
							this.c = new MailMessage();
							this.c.LoadMessage(attachment.GetData());
						}
					}
					else
					{
						this.c = attachment.GetEncapsulatedMessage();
					}
				}
			}
			foreach (object obj2 in A_0.BodyParts)
			{
				TextBodyPart textBodyPart = (TextBodyPart)obj2;
				if (textBodyPart.AsMimePart.ContentType != null)
				{
					string text = textBodyPart.AsMimePart.ContentType.ToLower();
					if (text == "text/rfc822-headers")
					{
						this.c = new MailMessage();
						this.c.LoadMessage(textBodyPart.AsMimePart.PartValueAsBytes);
					}
				}
			}
			if (this.g == null && (!this.g() || !this.f()))
			{
				return;
			}
			this.d = this.OriginalMessageBodyText;
			this.d = Regex.Replace(Regex.Replace(this.d, "[\t\r\n]", " "), " +", " ").Trim().ToLower();
			this.e = this.e();
			if (this.e != -1)
			{
				StringDictionary[] array = this.c();
				if (array != null)
				{
					foreach (StringDictionary stringDictionary in array)
					{
						RecipientStatus recipientStatus = new RecipientStatus(this.a);
						recipientStatus.MatchedKeywords = stringDictionary;
						if (stringDictionary.ContainsKey("TO_EMAIL"))
						{
							recipientStatus.EmailAddressFromTemplate = stringDictionary["TO_EMAIL"];
						}
						if (stringDictionary.ContainsKey("DESCRIPTION"))
						{
							recipientStatus.DescriptionFromTemplate = stringDictionary["DESCRIPTION"];
							this.a(recipientStatus, stringDictionary["DESCRIPTION"]);
						}
						this.f.a(recipientStatus);
					}
				}
				if (this.c == null)
				{
					this.c = this.b();
				}
				this.a();
			}
			if (this.g != null)
			{
				foreach (object obj3 in this.g.Recipients)
				{
					DsnRecipient dsnRecipient = (DsnRecipient)obj3;
					if (!dsnRecipient.IsLinked)
					{
						RecipientStatus recipientStatus2 = new RecipientStatus(this.a);
						recipientStatus2.DsnInternal = dsnRecipient;
						this.a(recipientStatus2, recipientStatus2.DsnInfo.Action);
						this.f.a(recipientStatus2);
					}
				}
			}
			if (this.f.Count == 1 && this.f[0].EmailAddress == string.Empty && this.c != null)
			{
				foreach (object obj4 in this.c.GetAllRecipients())
				{
					EmailAddress emailAddress = (EmailAddress)obj4;
					RecipientStatus recipientStatus3 = new RecipientStatus(this.a);
					recipientStatus3.EmailAddressFromTemplate = emailAddress.AsString;
					this.f.a(recipientStatus3);
				}
				if (this.f.Count > 1)
				{
					this.f[0].EmailAddressFromTemplate = this.f[1].EmailAddress;
					this.f.RemoveAt(1);
				}
			}
			foreach (object obj5 in this.f)
			{
				RecipientStatus recipientStatus4 = (RecipientStatus)obj5;
				if (recipientStatus4.DsnInfo != null)
				{
					if (!recipientStatus4.IsBounced)
					{
						this.a(recipientStatus4, recipientStatus4.DsnInfo.ToString());
					}
					if (!recipientStatus4.IsBounced && recipientStatus4.DsnInfo.Action == DsnAction.Failed)
					{
						recipientStatus4.IsBouncedInternal = true;
					}
					if (recipientStatus4.IsBounced && recipientStatus4.Common == CommonType.Unknown && recipientStatus4.Detailed == DetailedType.Unknown)
					{
						this.a(recipientStatus4, recipientStatus4.DsnInfo.Action);
					}
				}
			}
			if (this.f.Count == 0)
			{
				this.f.a(new RecipientStatus(this.a));
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000BA28 File Offset: 0x0000AA28
		private void a(RecipientStatus A_0, DsnAction A_1)
		{
			switch (A_1)
			{
			case DsnAction.Failed:
				A_0.CommonInternal = CommonType.Undeliverable;
				A_0.DetailedInternal = DetailedType.Hard;
				return;
			case DsnAction.Delayed:
				A_0.CommonInternal = CommonType.Undeliverable;
				A_0.DetailedInternal = DetailedType.Soft;
				return;
			case DsnAction.Delivered:
				A_0.CommonInternal = CommonType.Receipt;
				A_0.DetailedInternal = DetailedType.Delivered;
				return;
			case DsnAction.Relayed:
				A_0.CommonInternal = CommonType.Receipt;
				A_0.DetailedInternal = DetailedType.Forwarded;
				return;
			case DsnAction.Expanded:
				A_0.CommonInternal = CommonType.Information;
				A_0.DetailedInternal = DetailedType.Modified;
				return;
			case DsnAction.Unknown:
				A_0.CommonInternal = CommonType.Unknown;
				A_0.DetailedInternal = DetailedType.Unknown;
				return;
			default:
				A_0.CommonInternal = CommonType.Unknown;
				A_0.DetailedInternal = DetailedType.Unknown;
				return;
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000BAC4 File Offset: 0x0000AAC4
		private string a(d A_0, string A_1)
		{
			switch (A_0)
			{
			case global::a.k.d.a:
				return "^" + A_1;
			case global::a.k.d.b:
				return A_1 + "$";
			case global::a.k.d.d:
				return "^" + A_1 + "$";
			}
			return A_1;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000BB14 File Offset: 0x0000AB14
		private bool g()
		{
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = this.a(this.b.Subject, this.a.a());
			bool flag6 = this.a(this.b.Subject, this.a.b());
			return (flag3 || flag2 || flag || flag4) && flag5 && !flag6;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000BB74 File Offset: 0x0000AB74
		private bool a(string A_0, global::a.k.b[] A_1)
		{
			foreach (global::a.k.b b in A_1)
			{
				string text = b.a.Replace("{0}", this.a.i()["BLANK"]);
				text = this.a(b.b, text);
				if (new Regex(text.ToLower()).Match(A_0.ToLower()).Success)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000BBF0 File Offset: 0x0000ABF0
		private bool f()
		{
			string text = ((this.b.BodyPlainText != string.Empty) ? this.b.BodyPlainText : this.b.BodyHtmlText).Replace("_", " ");
			text = Regex.Replace(Regex.Replace(text, "[\t\r\n]", " "), " +", " ").Trim();
			return this.a(text, this.a.j());
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000BC74 File Offset: 0x0000AC74
		private int e()
		{
			for (int i = 0; i < this.a.h().Length; i++)
			{
				string pattern = this.a.h()[i].Replace("<repeat>\\ ", "(").Replace("<repeat>", "(").Replace("\\ </repeat>", " ?)+").Replace("</repeat>", " ?)+");
				if (this.h)
				{
					if (new Regex(pattern, RegexOptions.None, TimeSpan.FromMilliseconds((double)this.i)).Match(this.d).Success)
					{
						return i;
					}
				}
				else if (new Regex(pattern).Match(this.d).Success)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000BD34 File Offset: 0x0000AD34
		private int d()
		{
			if (this.a.h()[this.e].IndexOf("<repeat>") == -1)
			{
				return 1;
			}
			int num = 1;
			int result = 1;
			for (;;)
			{
				Match match = new Regex(this.a.h()[this.e].Replace("<repeat>\\ ", "(").Replace("<repeat>", "(").Replace("\\ </repeat>", " ?){" + num + "}").Replace("</repeat>", " ?){" + num + "}")).Match(this.d);
				if (match.Success)
				{
					result = num;
				}
				if (!match.Success || num > 2)
				{
					break;
				}
				num++;
			}
			return result;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000BE04 File Offset: 0x0000AE04
		private StringDictionary[] c()
		{
			int num = this.d();
			StringDictionary stringDictionary = new StringDictionary();
			string text = this.a.h()[this.e];
			int num2 = text.IndexOf("<repeat>");
			if (num2 != -1)
			{
				int num3 = text.IndexOf("</repeat>");
				if (num3 > num2)
				{
					foreach (object obj in this.a.i())
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (text.IndexOf("<" + dictionaryEntry.Key + ">", num2, num3 - num2) > -1)
						{
							stringDictionary.Add((string)dictionaryEntry.Key, null);
						}
					}
					string text2 = text.Substring(num2 + 8, num3 - num2 - 8);
					if (text2.StartsWith("\\ "))
					{
						text2 = text2.Substring(2);
					}
					if (text2.EndsWith("\\ "))
					{
						text2 = text2.Substring(0, text2.Length - 2);
					}
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < num; i++)
					{
						string text3 = "(" + text2 + " ?)";
						foreach (object obj2 in stringDictionary)
						{
							DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
							text3 = text3.Replace("<" + dictionaryEntry2.Key + ">", string.Concat(new object[]
							{
								"<",
								dictionaryEntry2.Key,
								i + 1,
								">"
							}));
						}
						stringBuilder.Append(text3);
					}
					text = text.Remove(num2, num3 - num2 + 9);
					text = text.Insert(num2, stringBuilder.ToString());
				}
			}
			Match match = new Regex(text).Match(this.d);
			if (match.Success)
			{
				StringDictionary[] array = new StringDictionary[num];
				for (int j = 0; j < num; j++)
				{
					array[j] = new StringDictionary();
					foreach (object obj3 in stringDictionary)
					{
						DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
						array[j].Add((string)dictionaryEntry3.Key, match.Result(string.Concat(new object[]
						{
							"${",
							dictionaryEntry3.Key,
							j + 1,
							"}"
						})));
					}
					foreach (object obj4 in this.a.i())
					{
						DictionaryEntry dictionaryEntry4 = (DictionaryEntry)obj4;
						if (!array[j].ContainsKey((string)dictionaryEntry4.Key) && match.Result("${" + dictionaryEntry4.Key + "}") != "${" + dictionaryEntry4.Key + "}")
						{
							array[j].Add((string)dictionaryEntry4.Key, match.Result("${" + dictionaryEntry4.Key + "}"));
						}
					}
				}
				return array;
			}
			return null;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000C1D0 File Offset: 0x0000B1D0
		private MailMessage b()
		{
			if (!this.f[0].MatchedKeywords.ContainsKey("ORIG_MESSAGE"))
			{
				return null;
			}
			string value = this.f[0].MatchedKeywords["ORIG_MESSAGE"].Split(new char[]
			{
				' '
			}, 2)[0];
			string text = this.OriginalMessageBodyText;
			text = this.OriginalMessageBodyText.Substring(text.ToLower().IndexOf(value));
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			int num = 0;
			while (num < array.Length && (num != 1 || array[num].StartsWith("  ")))
			{
				array[num] = array[num].TrimStart(new char[]
				{
					' '
				});
				num++;
			}
			MailMessage mailMessage = new MailMessage();
			mailMessage.LoadMessage(Encoding.Default.GetBytes(string.Join("\n", array)));
			return mailMessage;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000C2B4 File Offset: 0x0000B2B4
		private void a(RecipientStatus A_0, string A_1)
		{
			for (int i = 0; i < this.a.j().Length; i++)
			{
				if (new Regex(this.a(this.a.j()[i].b, this.a.j()[i].a).ToLower()).Match(A_1.Replace("_", " ")).Success)
				{
					A_0.Keyword = this.a.j()[i].a;
					A_0.Type = this.a.j()[i].c;
					return;
				}
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000C370 File Offset: 0x0000B370
		private void a()
		{
			if (this.g != null && this.g.Recipients.Count > 0)
			{
				if (this.g.Recipients[0].OriginalRecipientAddress == string.Empty)
				{
					for (int i = 0; i < this.f.Count; i++)
					{
						if (i < this.g.Recipients.Count)
						{
							this.f[i].DsnInternal = this.g.Recipients[i];
							this.f[i].DsnInfo.IsLinkedInternal = true;
						}
					}
					return;
				}
				foreach (object obj in this.f)
				{
					RecipientStatus recipientStatus = (RecipientStatus)obj;
					foreach (object obj2 in this.g.Recipients)
					{
						DsnRecipient dsnRecipient = (DsnRecipient)obj2;
						if (!dsnRecipient.IsLinked && ((dsnRecipient.OriginalRecipientAddress != null && recipientStatus.EmailAddress.ToLower() == dsnRecipient.OriginalRecipientAddress.ToLower()) || (dsnRecipient.FinalRecipientAddress != null && recipientStatus.EmailAddress.ToLower() == dsnRecipient.FinalRecipientAddress.ToLower())))
						{
							recipientStatus.DsnInternal = dsnRecipient;
							dsnRecipient.IsLinkedInternal = true;
							break;
						}
					}
				}
			}
		}

		// Token: 0x04000201 RID: 513
		private c a;

		// Token: 0x04000202 RID: 514
		private MailMessage b;

		// Token: 0x04000203 RID: 515
		private MailMessage c;

		// Token: 0x04000204 RID: 516
		private string d;

		// Token: 0x04000205 RID: 517
		private int e;

		// Token: 0x04000206 RID: 518
		private RecipientStatusCollection f;

		// Token: 0x04000207 RID: 519
		private DsnAttachment g;

		// Token: 0x04000208 RID: 520
		private bool h;

		// Token: 0x04000209 RID: 521
		private int i;
	}
}
