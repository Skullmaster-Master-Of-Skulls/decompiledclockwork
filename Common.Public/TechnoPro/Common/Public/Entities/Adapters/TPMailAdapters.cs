using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using EncryptionClassLibrary;
using TechnoPro.Common.Public.Entities.TPMailMan;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005E0 RID: 1504
	public static class TPMailAdapters
	{
		// Token: 0x0600306C RID: 12396 RVA: 0x000404F8 File Offset: 0x0003E6F8
		public static string ConvertEmailToRichText(this TPMailMessage Email, Dictionary<string, int> AttachmentFileIds)
		{
			bool flag = Email == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = Email.ConvertToEmailMessageForFormatting().ConvertEmailToRtf(AttachmentFileIds);
			}
			return result;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x00040528 File Offset: 0x0003E728
		public static EmailMessage ConvertToEmailMessageForFormatting(this TPMailMessage Email)
		{
			bool flag = Email == null;
			EmailMessage result;
			if (flag)
			{
				result = null;
			}
			else
			{
				EmailMessage emailMessage = new EmailMessage();
				emailMessage.From = ((Email.From == null) ? "" : (Email.From.EmailAddress ?? ""));
				emailMessage.To = Email.To.GetEmailList();
				emailMessage.Cc = Email.Cc.GetEmailList();
				emailMessage.Bcc = Email.Bcc.GetEmailList();
				IList<string> attachments;
				if (Email.Attachments != null)
				{
					attachments = Email.Attachments.ConvertAll<string>((TPMailAttachment g) => g.FileNameForDisplay);
				}
				else
				{
					attachments = new List<string>();
				}
				emailMessage.Attachments = attachments;
				emailMessage.Body = Email.Body;
				emailMessage.BodyHtml = Email.BodyHtml;
				emailMessage.BodyType = (int)Email.BodyType;
				emailMessage.Subject = Email.Subject;
				result = emailMessage;
			}
			return result;
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x00040624 File Offset: 0x0003E824
		public static string ConvertToDisplayString(this TPMailMessage MailMessage)
		{
			bool flag = MailMessage == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("Date: {0}\nTo: {1}\nFrom: {2}\n", DateTime.Now.ToString("dddd MMMM d, yyyy h:mm tt"), MailMessage.To.GetEmailList(), (MailMessage.From == null) ? "" : (MailMessage.From.EmailAddress ?? ""));
				string emailList = MailMessage.Cc.GetEmailList();
				string emailList2 = MailMessage.Bcc.GetEmailList();
				bool flag2 = !string.IsNullOrEmpty(emailList);
				if (flag2)
				{
					stringBuilder.AppendFormat("Cc: {0}\n", emailList);
				}
				bool flag3 = !string.IsNullOrEmpty(emailList2);
				if (flag3)
				{
					stringBuilder.AppendFormat("Bcc: {0}\n", emailList2);
				}
				stringBuilder.AppendFormat("Subject: {0}\n", MailMessage.Subject ?? "");
				string attachmentsString = MailMessage.Attachments.GetAttachmentsString();
				bool flag4 = !string.IsNullOrEmpty(attachmentsString);
				if (flag4)
				{
					stringBuilder.AppendFormat("Attachments: {0}\n", attachmentsString);
				}
				string text = MailMessage.GetPlainTextBody();
				bool flag5 = string.IsNullOrEmpty(text);
				if (flag5)
				{
					text = (MailMessage.GetHtmlBody() ?? "").ConvertHtmlToPlainText();
				}
				stringBuilder.AppendFormat("Body: \n{0}", text);
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x00040774 File Offset: 0x0003E974
		private static string ConvertHtmlToPlainText(this string html)
		{
			return html.Replace("<br />", "\n");
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x00040798 File Offset: 0x0003E998
		public static TPMailMessage EmailFromXml(this string Xml)
		{
			return Xml.EmailFromXml("email");
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000407B8 File Offset: 0x0003E9B8
		public static TPMailMessage ConvertXmlToBatchEmail(this string xml)
		{
			IList<TPMailMessage> list = xml.ConvertXmlToBatchEmails();
			return (list == null || list.Count < 1) ? new TPMailMessage() : list[0];
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x000407EC File Offset: 0x0003E9EC
		public static IList<TPMailMessage> ConvertXmlToBatchEmails(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			IList<TPMailMessage> result;
			if (flag)
			{
				result = new List<TPMailMessage>
				{
					new TPMailMessage()
				};
			}
			else
			{
				xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?>{0}", xml);
				XDocument xdocument = XDocument.Parse(xml);
				char[] commaCharArray = new char[]
				{
					','
				};
				IEnumerable<XElement> source = xdocument.Descendants("batchemail");
				List<TPMailMessage> list = (from email in source
				let attrTo = email.Attribute("to")
				let attrFrom = email.Attribute("from")
				let attrCc = email.Attribute("cc")
				let attrBcc = email.Attribute("bcc")
				let attrSubject = email.Attribute("subject")
				let attrBody = email.Attribute("body")
				let attrBodyHtml = email.Attribute("bodyhtml")
				let attrIsActive = email.Attribute("isactive")
				let xIsActive = (attrIsActive == null) ? "" : (attrIsActive.Value ?? "")
				let attrDeliveryMethod = email.Attribute("deliverymethod")
				let xDeliveryMethod = (attrDeliveryMethod == null) ? "" : (attrDeliveryMethod.Value ?? "")
				let attrAttachments = email.Attribute("attachments")
				let attrPriority = email.Attribute("priority")
				select new
				{
					<>h__TransparentIdentifier12 = <>h__TransparentIdentifier12,
					xPriority = ((attrPriority == null) ? "" : (attrPriority.Value ?? ""))
				}).Select(delegate(<>h__TransparentIdentifier13)
				{
					TPMailMessage tpmailMessage = new TPMailMessage();
					tpmailMessage.From = new TPMailAddress
					{
						EmailAddress = ((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.attrFrom == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.attrFrom.Value ?? ""))
					};
					tpmailMessage.To = new List<TPMailAddress>(((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.attrTo == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.attrTo.Value ?? "")).Split(commaCharArray, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string f) => new TPMailAddress
					{
						EmailAddress = f
					}));
					tpmailMessage.Cc = new List<TPMailAddress>(((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.attrCc == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.attrCc.Value ?? "")).Split(commaCharArray, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string f) => new TPMailAddress
					{
						EmailAddress = f
					}));
					tpmailMessage.Bcc = new List<TPMailAddress>(((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.attrBcc == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.attrBcc.Value ?? "")).Split(commaCharArray, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string f) => new TPMailAddress
					{
						EmailAddress = f
					}));
					tpmailMessage.Subject = ((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.attrSubject == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.attrSubject.Value ?? ""));
					tpmailMessage.Body = ((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.attrBody == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.attrBody.Value ?? ""));
					tpmailMessage.BodyHtml = ((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.attrBodyHtml == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.attrBodyHtml.Value ?? ""));
					tpmailMessage.BodyType = TPMailAdapters.GetBodyType(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.email);
					tpmailMessage.Attachments = ((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.attrAttachments == null) ? "" : TPMailAdapters.FromXmlFormat(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.attrAttachments.Value ?? "")).GetAttachmentsFromXmlString().ToList<TPMailAttachment>();
					tpmailMessage.DeliveryMethod = ((<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.xDeliveryMethod.Length > 0 && Enum.IsDefined(typeof(eTPMessageDeliveryMethod), <>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.xDeliveryMethod)) ? ((eTPMessageDeliveryMethod)Enum.Parse(typeof(eTPMessageDeliveryMethod), <>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.xDeliveryMethod)) : eTPMessageDeliveryMethod.HtmlAndPlainText);
					tpmailMessage.Priority = ((<>h__TransparentIdentifier13.xPriority.Length > 0 && Enum.IsDefined(typeof(eTPMessagePriority), <>h__TransparentIdentifier13.xPriority)) ? ((eTPMessagePriority)Enum.Parse(typeof(eTPMessagePriority), <>h__TransparentIdentifier13.xPriority)) : eTPMessagePriority.Unknown);
					tpmailMessage.IsActive = (!string.IsNullOrEmpty(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.xIsActive) && "1yestrue".IndexOf(<>h__TransparentIdentifier13.<>h__TransparentIdentifier12.<>h__TransparentIdentifier11.<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.xIsActive) >= 0);
					return tpmailMessage;
				}).ToList<TPMailMessage>();
				result = list;
			}
			return result;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00040A74 File Offset: 0x0003EC74
		public static TPMailMessage EmailFromXml(this string Xml, string xmlElementName)
		{
			bool flag = string.IsNullOrEmpty(Xml);
			TPMailMessage result;
			if (flag)
			{
				result = new TPMailMessage();
			}
			else
			{
				string text = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><emails>" + Xml + "</emails>";
				XDocument xdocument = XDocument.Parse(text);
				List<TPMailMessage> source = (from email in xdocument.Descendants(xmlElementName)
				select email.EmailFromXml()).ToList<TPMailMessage>();
				result = source.FirstOrDefault<TPMailMessage>();
			}
			return result;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x00040AF0 File Offset: 0x0003ECF0
		public static TPMailMessage EmailFromXml(this XElement emailElement)
		{
			bool flag = emailElement == null || !emailElement.HasElements;
			TPMailMessage result;
			if (flag)
			{
				result = null;
			}
			else
			{
				char[] separator = new char[]
				{
					','
				};
				string xelementString = TPMailAdapters.GetXElementString(emailElement.Element("isactive"));
				TPMailMessage tpmailMessage = new TPMailMessage();
				tpmailMessage.From = new TPMailAddress
				{
					EmailAddress = TPMailAdapters.FromXmlFormat(TPMailAdapters.GetXElementString(emailElement.Element("from")))
				};
				tpmailMessage.To = new List<TPMailAddress>(TPMailAdapters.FromXmlFormat(TPMailAdapters.GetXElementString(emailElement.Element("to"))).Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string f) => new TPMailAddress
				{
					EmailAddress = f
				}));
				tpmailMessage.Cc = new List<TPMailAddress>(TPMailAdapters.FromXmlFormat(TPMailAdapters.GetXElementString(emailElement.Element("cc"))).Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string f) => new TPMailAddress
				{
					EmailAddress = f
				}));
				tpmailMessage.Bcc = new List<TPMailAddress>(TPMailAdapters.FromXmlFormat(TPMailAdapters.GetXElementString(emailElement.Element("bcc"))).Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList<string>().ConvertAll<TPMailAddress>((string f) => new TPMailAddress
				{
					EmailAddress = f
				}));
				tpmailMessage.Subject = TPMailAdapters.FromXmlFormat(TPMailAdapters.GetXElementString(emailElement.Element("subject")));
				tpmailMessage.Body = TPMailAdapters.FromXmlFormat(TPMailAdapters.GetXElementString(emailElement.Element("body")));
				tpmailMessage.BodyHtml = TPMailAdapters.FromXmlFormat(TPMailAdapters.GetXElementString(emailElement.Element("bodyhtml")));
				tpmailMessage.BodyType = TPMailAdapters.GetBodyType(emailElement);
				tpmailMessage.Attachments = TPMailAdapters.GetXElementString(emailElement.Element("attachments")).GetAttachmentsFromXmlString().ToList<TPMailAttachment>();
				tpmailMessage.DeliveryMethod = (Enum.IsDefined(typeof(eTPMessageDeliveryMethod), TPMailAdapters.GetXElementString(emailElement.Element("deliverymethod"))) ? ((eTPMessageDeliveryMethod)Enum.Parse(typeof(eTPMessageDeliveryMethod), TPMailAdapters.GetXElementString(emailElement.Element("deliverymethod")))) : eTPMessageDeliveryMethod.HtmlAndPlainText);
				tpmailMessage.Priority = (Enum.IsDefined(typeof(eTPMessagePriority), TPMailAdapters.GetXElementString(emailElement.Element("priority"))) ? ((eTPMessagePriority)Enum.Parse(typeof(eTPMessagePriority), TPMailAdapters.GetXElementString(emailElement.Element("priority")))) : eTPMessagePriority.Unknown);
				tpmailMessage.IsActive = (!string.IsNullOrEmpty(xelementString) && "1yestrue".IndexOf(xelementString) >= 0);
				TPMailMessage tpmailMessage2 = tpmailMessage;
				bool flag2 = !string.IsNullOrEmpty(tpmailMessage2.Body);
				if (flag2)
				{
					tpmailMessage2.Body = tpmailMessage2.Body.DecodeHtml();
				}
				bool flag3 = tpmailMessage2.BodyType == eEmailBodyType.Unknown && string.IsNullOrEmpty(tpmailMessage2.BodyHtml) && !string.IsNullOrEmpty(tpmailMessage2.Body);
				if (flag3)
				{
					int num = tpmailMessage2.Body.IndexOf("<br", StringComparison.OrdinalIgnoreCase);
					int num2 = tpmailMessage2.Body.IndexOf("<li>", StringComparison.OrdinalIgnoreCase);
					bool flag4 = num >= 0 || num2 >= 0;
					if (flag4)
					{
						tpmailMessage2.BodyType = eEmailBodyType.Html;
						bool flag5 = string.IsNullOrEmpty(tpmailMessage2.BodyHtml);
						if (flag5)
						{
							tpmailMessage2.BodyHtml = tpmailMessage2.Body;
						}
						tpmailMessage2.Body = tpmailMessage2.Body.ConvertHtmlToPlainText();
						tpmailMessage2.DeliveryMethod = eTPMessageDeliveryMethod.HtmlAndPlainText;
					}
					else
					{
						bool flag6 = num < 0 && num2 < 0;
						if (flag6)
						{
							tpmailMessage2.BodyType = eEmailBodyType.Html;
							tpmailMessage2.Body = tpmailMessage2.Body.Replace("\r\n", "<br /").Replace("\n", "<br />");
							tpmailMessage2.BodyHtml = tpmailMessage2.Body;
						}
					}
				}
				bool flag7 = string.IsNullOrEmpty(tpmailMessage2.BodyHtml) && tpmailMessage2.BodyType == eEmailBodyType.Html;
				if (flag7)
				{
					tpmailMessage2.BodyHtml = (tpmailMessage2.Body ?? "");
				}
				TPMailAdapters.ChangeMailMergeTagsFromXmlFormatToNormalFormat(ref tpmailMessage2);
				result = tpmailMessage2;
			}
			return result;
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x00040F5C File Offset: 0x0003F15C
		public static IList<TPMailAttachment> GetAttachmentsFromXmlString(this string s)
		{
			return (from g in s.GetAttachmentsFromXmlString2(new Func<string, TPMailAttachment>(TPMailAdapters.GetAttachmentFromXml))
			where !string.IsNullOrEmpty(g.FileNameForDisplay)
			select g).ToList<TPMailAttachment>();
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x00040FAC File Offset: 0x0003F1AC
		public static IList<T> GetAttachmentsFromXmlString2<T>(this string s, Func<string, T> getAttachmentFromStringFunction) where T : class
		{
			bool flag = string.IsNullOrEmpty(s);
			IList<T> result;
			if (flag)
			{
				result = new List<T>();
			}
			else
			{
				result = (from q in (from g in s.Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).Select(getAttachmentFromStringFunction)
				where q != null
				select q).ToList<T>();
			}
			return result;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x0004105C File Offset: 0x0003F25C
		public static string GetAttachmentInfoFromString(this string s, out int fileAttachmentId)
		{
			fileAttachmentId = 0;
			bool flag = string.IsNullOrEmpty(s);
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (s.Length > 2) ? s.IndexOf("~~") : -1;
				bool flag2 = num <= -1;
				if (flag2)
				{
					result = s;
				}
				else
				{
					string s2 = s.Substring(0, num);
					string text = s.Substring(num + 2);
					int num2;
					bool flag3 = !int.TryParse(s2, out num2);
					if (flag3)
					{
						result = s;
					}
					else
					{
						fileAttachmentId = num2;
						result = text;
					}
				}
			}
			return result;
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000410DC File Offset: 0x0003F2DC
		public static TPMailAttachment GetAttachmentFromXml(this string s)
		{
			int fileAttachmentId;
			string attachmentInfoFromString = s.GetAttachmentInfoFromString(out fileAttachmentId);
			bool flag = string.IsNullOrEmpty(attachmentInfoFromString);
			TPMailAttachment result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new TPMailAttachment
				{
					FileAttachmentId = fileAttachmentId,
					FileNameForDisplay = attachmentInfoFromString
				};
			}
			return result;
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x0004111C File Offset: 0x0003F31C
		private static void ChangeMailMergeTagsFromXmlFormatToNormalFormat(ref TPMailMessage email)
		{
			email.Subject = email.Subject.ChangeMailMergeTagsFromXmlFormatToNormalFormat();
			email.Body = email.Body.ChangeMailMergeTagsFromXmlFormatToNormalFormat();
			email.BodyHtml = email.BodyHtml.ChangeMailMergeTagsFromXmlFormatToNormalFormat();
			TPMailAdapters.ChangeMailMergeTagsFromXmlFormatToNormalFormat(email.From);
			TPMailAdapters.ChangeMailMergeTagsFromXmlFormatToNormalFormat(email.To);
			TPMailAdapters.ChangeMailMergeTagsFromXmlFormatToNormalFormat(email.Cc);
			TPMailAdapters.ChangeMailMergeTagsFromXmlFormatToNormalFormat(email.Bcc);
			bool flag = email.Attachments != null;
			if (flag)
			{
				foreach (TPMailAttachment tpmailAttachment in email.Attachments)
				{
					bool flag2 = tpmailAttachment != null && !string.IsNullOrEmpty(tpmailAttachment.FileNameForDisplay);
					if (flag2)
					{
						tpmailAttachment.FileNameForDisplay = tpmailAttachment.FileNameForDisplay.ChangeMailMergeTagsFromXmlFormatToNormalFormat();
					}
				}
			}
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x00041214 File Offset: 0x0003F414
		private static void ChangeMailMergeTagsFromXmlFormatToNormalFormat(IList<TPMailAddress> addresses)
		{
			bool flag = addresses == null;
			if (!flag)
			{
				for (int i = 0; i < addresses.Count; i++)
				{
					TPMailAdapters.ChangeMailMergeTagsFromXmlFormatToNormalFormat(addresses[i]);
				}
			}
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x00041250 File Offset: 0x0003F450
		private static void ChangeMailMergeTagsFromXmlFormatToNormalFormat(TPMailAddress address)
		{
			bool flag = address == null;
			if (!flag)
			{
				address.EmailAddress = address.EmailAddress.ChangeMailMergeTagsFromXmlFormatToNormalFormat();
			}
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x0004127C File Offset: 0x0003F47C
		private static string ChangeMailMergeTagsFromXmlFormatToNormalFormat(this string s)
		{
			bool flag = string.IsNullOrEmpty(s) || s.IndexOf("#~") < 0;
			string result;
			if (flag)
			{
				result = s;
			}
			else
			{
				result = Regex.Replace(s, "#~(?'code'[^#~]*)~#", "#<${code}>#");
			}
			return result;
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000412C0 File Offset: 0x0003F4C0
		private static eEmailBodyType GetBodyType(XElement emailElement)
		{
			XElement xelement = emailElement.Element("bodytype");
			bool flag = xelement == null || xelement.Value == null;
			eEmailBodyType result;
			if (flag)
			{
				result = eEmailBodyType.Unknown;
			}
			else
			{
				string value = xelement.Value;
				bool flag2 = !Enum.IsDefined(typeof(eEmailBodyType), value);
				if (flag2)
				{
					result = eEmailBodyType.Unknown;
				}
				else
				{
					result = (eEmailBodyType)Enum.Parse(typeof(eEmailBodyType), value);
				}
			}
			return result;
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x00041334 File Offset: 0x0003F534
		private static string GetXElementString(XElement element)
		{
			bool flag = element == null || element.Value == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = element.Value;
			}
			return result;
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x00041368 File Offset: 0x0003F568
		public static XElement ToEmailXElement(this TPMailMessage MailMessage, string xmlElementName)
		{
			bool flag = MailMessage == null;
			if (flag)
			{
				MailMessage = new TPMailMessage();
			}
			return new XElement(xmlElementName, new object[]
			{
				new XElement("from", TPMailAdapters.ToXmlFormat((MailMessage.From == null) ? "" : (MailMessage.From.EmailAddress ?? ""))),
				new XElement("to", TPMailAdapters.ToXmlFormat((MailMessage.To == null) ? "" : MailMessage.To.GetEmailList())),
				new XElement("cc", TPMailAdapters.ToXmlFormat((MailMessage.Cc == null) ? "" : MailMessage.Cc.GetEmailList())),
				new XElement("bcc", TPMailAdapters.ToXmlFormat((MailMessage.Bcc == null) ? "" : MailMessage.Bcc.GetEmailList())),
				new XElement("subject", TPMailAdapters.ToXmlFormat(MailMessage.Subject ?? "")),
				new XElement("body", TPMailAdapters.ToXmlFormat(MailMessage.Body ?? "")),
				new XElement("bodyhtml", TPMailAdapters.ToXmlFormat(MailMessage.BodyHtml ?? "")),
				new XElement("bodytype", MailMessage.BodyType.ToString()),
				new XElement("deliverymethod", MailMessage.DeliveryMethod.ToString()),
				new XElement("priority", MailMessage.Priority.ToString()),
				new XElement("attachments", MailMessage.Attachments.GetXmlFromAttachmentsList()),
				new XElement("isactive", MailMessage.IsActive ? 1 : 0)
			});
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x00041594 File Offset: 0x0003F794
		public static string GetXmlFromAttachmentsList(this IList<TPMailAttachment> attachments)
		{
			bool flag = attachments == null || attachments.Count < 1;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = string.Join(",", attachments.Select(new Func<TPMailAttachment, string>(TPMailAdapters.GetXmlFromAttachment)).ToArray<string>());
			}
			return result;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000415E4 File Offset: 0x0003F7E4
		private static string GetXmlFromAttachment(TPMailAttachment attachment)
		{
			bool flag = attachment == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				string text = attachment.FileNameForDisplay ?? "";
				bool flag2 = attachment.FileAttachmentId > 0;
				if (flag2)
				{
					result = attachment.FileAttachmentId.ToString() + "~~" + text;
				}
				else
				{
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x00041644 File Offset: 0x0003F844
		public static string ToEmailXml(this TPMailMessage MailMessage, string xmlElementName)
		{
			XElement xelement = MailMessage.ToEmailXElement(xmlElementName);
			return xelement.ToString();
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x00041664 File Offset: 0x0003F864
		public static string ToEmailXml(this TPMailMessage MailMessage)
		{
			return MailMessage.ToEmailXml("email");
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x00041684 File Offset: 0x0003F884
		public static string ToXmlFormat(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				Regex regex = new Regex("#<[^#<]*>#");
				MatchCollection matchCollection = regex.Matches(s);
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					bool flag2 = !string.IsNullOrEmpty(match.Value);
					if (flag2)
					{
						s = s.Replace(match.Value, match.Value.Replace("#<", "#~").Replace(">#", "~#"));
					}
				}
				result = s;
			}
			return result;
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x00041754 File Offset: 0x0003F954
		public static string FromXmlFormat(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				s = s.DecodeHtml();
				Regex regex = new Regex("#~[^#~]*~#");
				MatchCollection matchCollection = regex.Matches(s);
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					bool flag2 = !string.IsNullOrEmpty(match.Value);
					if (flag2)
					{
						s = s.Replace(match.Value, match.Value.Replace("#~", "#<").Replace("~#", ">#"));
					}
				}
				result = s;
			}
			return result;
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x0004182C File Offset: 0x0003FA2C
		public static string DecodeHtml(this string source)
		{
			bool flag = string.IsNullOrEmpty(source);
			string result;
			if (flag)
			{
				result = source;
			}
			else
			{
				result = source.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", "\"").Replace("&apos;", "'");
			}
			return result;
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x00041898 File Offset: 0x0003FA98
		public static string GetEmailList(this List<TPMailAddress> Addresses)
		{
			bool flag = Addresses == null || Addresses.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", Addresses.FindAll((TPMailAddress aa) => !string.IsNullOrEmpty(aa.EmailAddress)).ConvertAll<string>((TPMailAddress f) => f.EmailAddress ?? "").ToArray());
			}
			return result;
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x00041920 File Offset: 0x0003FB20
		public static string GetAttachmentsString(this List<TPMailAttachment> Attachments)
		{
			bool flag = Attachments == null || Attachments.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", Attachments.FindAll((TPMailAttachment aa) => !string.IsNullOrEmpty(aa.FileNameForDisplay)).ConvertAll<string>((TPMailAttachment f) => f.FileNameForDisplay ?? "").ToArray());
			}
			return result;
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000419A8 File Offset: 0x0003FBA8
		public static TPSmtpClient GetSmtpSettingsFromXml(this string xml, IEncryption tripleDES)
		{
			bool flag = string.IsNullOrEmpty(xml);
			TPSmtpClient result;
			if (flag)
			{
				result = new TPSmtpClient();
			}
			else
			{
				try
				{
					XDocument xdocument = XDocument.Parse(xml);
					TPSmtpClient tpsmtpClient = new TPSmtpClient();
					XElement xelement = xdocument.Element("SmtpSettings");
					bool flag2 = xelement != null;
					if (flag2)
					{
						XElement xelement2 = xelement.Element("ServerName");
						XElement xelement3 = xelement.Element("PortNum");
						XElement xelement4 = xelement.Element("UseSsl");
						XElement xelement5 = xelement.Element("SslProtocol");
						XElement xelement6 = xelement.Element("UserName");
						XElement xelement7 = xelement.Element("Pwd");
						XElement xelement8 = xelement.Element("PwdEnc");
						XElement xelement9 = xelement.Element("AuthMethods");
						XElement xelement10 = xelement.Element("AuthOptions");
						XElement xelement11 = xelement.Element("SslStartupMode");
						XElement xelement12 = xelement.Element("ServerTimeout");
						XElement xelement13 = xelement.Element("ExtendedSmtpOptions");
						XElement xelement14 = xelement.Element("HelloDomain");
						XElement xelement15 = xelement.Element("EnableNonFipsAlgorithms");
						tpsmtpClient.Server = ((xelement2 == null) ? "" : (xelement2.Value ?? ""));
						int num;
						tpsmtpClient.Port = ((xelement3 == null || !int.TryParse(xelement3.Value ?? "", out num)) ? 25 : num);
						eSslProtocol? eSslProtocol = null;
						bool flag3 = xelement5 != null && !string.IsNullOrEmpty(xelement5.Value);
						if (flag3)
						{
							string value = xelement5.Value;
							int num2;
							bool flag4 = int.TryParse(value, out num2);
							if (flag4)
							{
								bool flag5 = Enum.IsDefined(typeof(eSslProtocol), num2);
								if (flag5)
								{
									eSslProtocol = new eSslProtocol?((eSslProtocol)num2);
								}
							}
						}
						bool flag6 = eSslProtocol != null;
						if (flag6)
						{
							tpsmtpClient.SslProtocol = eSslProtocol.Value;
						}
						else
						{
							bool flag7 = xelement4 != null && !string.IsNullOrEmpty(xelement4.Value) && xelement4.Value != "0";
							tpsmtpClient.SslProtocol = (flag7 ? eSslProtocol.Auto : eSslProtocol.None);
						}
						tpsmtpClient.Username = ((xelement6 == null) ? "" : (xelement6.Value ?? ""));
						string text = (xelement8 == null) ? "" : (xelement8.Value ?? "");
						bool flag8 = text.Length > 0;
						if (flag8)
						{
							byte[] encryptedText = Convert.FromBase64String(text);
							tpsmtpClient.Password = tripleDES.Decrypt(encryptedText);
						}
						else
						{
							tpsmtpClient.Password = ((xelement7 == null) ? "" : (xelement7.Value ?? ""));
						}
						tpsmtpClient.AuthenticationMethods = ((xelement9 == null) ? "" : (xelement9.Value ?? ""));
						tpsmtpClient.AuthenticationOptions = ((xelement10 == null) ? "" : (xelement10.Value ?? ""));
						tpsmtpClient.SslStartupMode = ((xelement11 == null) ? "" : (xelement11.Value ?? ""));
						string text2 = (xelement12 == null) ? "" : xelement12.Value;
						int serverTimeoutSeconds;
						bool flag9 = !string.IsNullOrEmpty(text2) && int.TryParse(text2, out serverTimeoutSeconds);
						if (flag9)
						{
							tpsmtpClient.ServerTimeoutSeconds = serverTimeoutSeconds;
						}
						string text3 = (xelement13 == null) ? "" : (xelement13.Value ?? "");
						bool flag10 = text3.Length > 0 && Enum.IsDefined(typeof(eExtendedSmtpOptions), text3);
						if (flag10)
						{
							tpsmtpClient.ExtendedSmtpOptions = (eExtendedSmtpOptions)Enum.Parse(typeof(eExtendedSmtpOptions), text3);
						}
						string a = (xelement15 == null) ? "" : (xelement15.Value ?? "");
						bool flag11 = a == "1";
						if (flag11)
						{
							tpsmtpClient.EnableNonFipsAlgorithms = true;
						}
						tpsmtpClient.HelloDomain = ((xelement14 == null) ? "" : (xelement14.Value ?? "").Trim());
					}
					return tpsmtpClient;
				}
				catch (Exception ex)
				{
				}
				result = new TPSmtpClient();
			}
			return result;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x00041E24 File Offset: 0x00040024
		public static string GetXmlFromSmtpSettings(this TPSmtpClient smtpSettings, IEncryption encryption)
		{
			bool flag = smtpSettings == null;
			if (flag)
			{
				smtpSettings = new TPSmtpClient();
			}
			XElement xelement = new XElement("SmtpSettings", new object[]
			{
				new XElement("ServerName", smtpSettings.Server ?? ""),
				new XElement("PortNum", smtpSettings.Port.ToString()),
				new XElement("SslProtocol", ((int)smtpSettings.SslProtocol).ToString()),
				new XElement("UserName", smtpSettings.Username ?? ""),
				new XElement("PwdEnc", Convert.ToBase64String(encryption.Encrypt(smtpSettings.Password ?? ""))),
				new XElement("AuthMethods", smtpSettings.AuthenticationMethods ?? ""),
				new XElement("AuthOptions", smtpSettings.AuthenticationOptions ?? ""),
				new XElement("SslStartupMode", smtpSettings.SslStartupMode ?? ""),
				new XElement("HelloDomain", (smtpSettings.HelloDomain ?? "").Trim()),
				new XElement("EnableNonFipsAlgorithms", smtpSettings.EnableNonFipsAlgorithms ? "1" : "")
			});
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new object[]
			{
				xelement
			});
			bool flag2 = smtpSettings.ServerTimeoutSeconds > 0;
			if (flag2)
			{
				xelement.Add(new XElement("ServerTimeout", smtpSettings.ServerTimeoutSeconds.ToString()));
			}
			bool flag3 = smtpSettings.ExtendedSmtpOptions > eExtendedSmtpOptions.Unknown;
			if (flag3)
			{
				xelement.Add(new XElement("ExtendedSmtpOptions", smtpSettings.ExtendedSmtpOptions.ToString()));
			}
			return xdocument.ToString();
		}
	}
}
