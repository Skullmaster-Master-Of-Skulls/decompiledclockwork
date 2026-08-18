using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C90 RID: 3216
	public static class TPMailAdapter
	{
		// Token: 0x06004303 RID: 17155 RVA: 0x00023F70 File Offset: 0x00022170
		public static TPMailMessageDTO EmailFromXml(this string Xml)
		{
			bool flag = string.IsNullOrEmpty(Xml);
			TPMailMessageDTO result;
			if (flag)
			{
				result = new TPMailMessageDTO();
			}
			else
			{
				string text = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?><emails>{0}</emails>", Xml);
				XDocument xdocument = XDocument.Parse(text);
				List<TPMailMessageDTO> list = xdocument.Descendants("email").Select(delegate(XElement email)
				{
					TPMailMessageDTO tpmailMessageDTO = new TPMailMessageDTO();
					tpmailMessageDTO.From = new TPMailAddressDTO
					{
						EmailAddress = TPMailAdapter.FromXmlFormat(TPMailAdapter.GetXElementString(email.Element("from")))
					};
					tpmailMessageDTO.To = new List<TPMailAddressDTO>(TPMailAdapter.FromXmlFormat(TPMailAdapter.GetXElementString(email.Element("to"))).Split(new char[]
					{
						','
					}).ToList<string>().ConvertAll<TPMailAddressDTO>((string f) => new TPMailAddressDTO
					{
						EmailAddress = f
					}));
					tpmailMessageDTO.Cc = new List<TPMailAddressDTO>(TPMailAdapter.FromXmlFormat(TPMailAdapter.GetXElementString(email.Element("cc"))).Split(new char[]
					{
						','
					}).ToList<string>().ConvertAll<TPMailAddressDTO>((string f) => new TPMailAddressDTO
					{
						EmailAddress = f
					}));
					tpmailMessageDTO.Bcc = new List<TPMailAddressDTO>(TPMailAdapter.FromXmlFormat(TPMailAdapter.GetXElementString(email.Element("bcc"))).Split(new char[]
					{
						','
					}).ToList<string>().ConvertAll<TPMailAddressDTO>((string f) => new TPMailAddressDTO
					{
						EmailAddress = f
					}));
					tpmailMessageDTO.Subject = TPMailAdapter.FromXmlFormat(TPMailAdapter.GetXElementString(email.Element("subject")));
					tpmailMessageDTO.Body = TPMailAdapter.FromXmlFormat(TPMailAdapter.GetXElementString(email.Element("body")));
					tpmailMessageDTO.BodyHtml = TPMailAdapter.FromXmlFormat(TPMailAdapter.GetXElementString(email.Element("bodyhtml")));
					tpmailMessageDTO.BodyType = TPMailAdapter.GetBodyType(email);
					tpmailMessageDTO.Attachments = TPMailAdapter.GetXElementString(email.Element("attachments")).GetAttachmentsDtoFromXmlString().ToList<TPMailAttachmentDTO>();
					tpmailMessageDTO.DeliveryMethod = (Enum.IsDefined(typeof(eTPMessageDeliveryMethodDTO), TPMailAdapter.GetXElementString(email.Element("deliverymethod"))) ? ((eTPMessageDeliveryMethodDTO)Enum.Parse(typeof(eTPMessageDeliveryMethodDTO), TPMailAdapter.GetXElementString(email.Element("deliverymethod")))) : eTPMessageDeliveryMethodDTO.Unknown);
					tpmailMessageDTO.Priority = (Enum.IsDefined(typeof(eTPMessagePriorityDTO), TPMailAdapter.GetXElementString(email.Element("priority"))) ? ((eTPMessagePriorityDTO)Enum.Parse(typeof(eTPMessagePriorityDTO), TPMailAdapter.GetXElementString(email.Element("priority")))) : eTPMessagePriorityDTO.Unknown);
					TPMailMessageDTO tpmailMessageDTO2 = tpmailMessageDTO;
					XElement xelement = email.Element("isactive");
					tpmailMessageDTO2.IsActive = ((((xelement != null) ? xelement.Value : null) ?? "") == "1");
					return tpmailMessageDTO;
				}).ToList<TPMailMessageDTO>();
				result = ((list.Count > 0) ? list[0] : new TPMailMessageDTO());
			}
			return result;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x00023FFC File Offset: 0x000221FC
		public static IList<TPMailAttachmentDTO> GetAttachmentsDtoFromXmlString(this string s)
		{
			return s.GetAttachmentsFromXmlString2(new Func<string, TPMailAttachmentDTO>(TPMailAdapter.GetAttachmentDtoFromXml));
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x00024020 File Offset: 0x00022220
		public static TPMailAttachmentDTO GetAttachmentDtoFromXml(this string s)
		{
			int fileAttachmentId;
			string attachmentInfoFromString = s.GetAttachmentInfoFromString(out fileAttachmentId);
			bool flag = string.IsNullOrEmpty(attachmentInfoFromString);
			TPMailAttachmentDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new TPMailAttachmentDTO
				{
					FileAttachmentId = fileAttachmentId,
					FileNameForDisplay = attachmentInfoFromString
				};
			}
			return result;
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x00024060 File Offset: 0x00022260
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

		// Token: 0x06004307 RID: 17159 RVA: 0x000240D4 File Offset: 0x000222D4
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

		// Token: 0x06004308 RID: 17160 RVA: 0x00024108 File Offset: 0x00022308
		public static string ToEmailXml(this TPMailMessageDTO MailMessage)
		{
			XElement xelement = new XElement("email", new object[]
			{
				new XElement("from", TPMailAdapter.ToXmlFormat((MailMessage.From == null) ? "" : (MailMessage.From.EmailAddress ?? ""))),
				new XElement("to", TPMailAdapter.ToXmlFormat((MailMessage.To == null) ? "" : MailMessage.To.GetEmailList())),
				new XElement("cc", TPMailAdapter.ToXmlFormat((MailMessage.Cc == null) ? "" : MailMessage.Cc.GetEmailList())),
				new XElement("bcc", TPMailAdapter.ToXmlFormat((MailMessage.Bcc == null) ? "" : MailMessage.Bcc.GetEmailList())),
				new XElement("subject", TPMailAdapter.ToXmlFormat(MailMessage.Subject ?? "")),
				new XElement("body", TPMailAdapter.ToXmlFormat(MailMessage.Body ?? "")),
				new XElement("bodytype", MailMessage.BodyType.ToString()),
				new XElement("bodyhtml", TPMailAdapter.ToXmlFormat(MailMessage.BodyHtml ?? "")),
				new XElement("deliverymethod", MailMessage.DeliveryMethod.ToString()),
				new XElement("priority", MailMessage.Priority.ToString()),
				new XElement("isactive", MailMessage.IsActive ? "1" : "0")
			});
			return xelement.ToString();
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x00024314 File Offset: 0x00022514
		private static string ToXmlFormat(string s)
		{
			return TPMailAdapters.ToXmlFormat(s);
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x0002432C File Offset: 0x0002252C
		private static string FromXmlFormat(string s)
		{
			return TPMailAdapters.FromXmlFormat(s);
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x00024344 File Offset: 0x00022544
		public static string GetEmailList(this List<TPMailAddressDTO> Addresses)
		{
			bool flag = Addresses == null || Addresses.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", Addresses.ConvertAll<string>((TPMailAddressDTO f) => f.EmailAddress ?? "").ToArray());
			}
			return result;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000243A8 File Offset: 0x000225A8
		public static string GetAttachmentsString(this List<TPMailAttachmentDTO> Attachments)
		{
			bool flag = Attachments == null || Attachments.Count < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", Attachments.ConvertAll<string>((TPMailAttachmentDTO f) => f.FileNameForDisplay ?? "").ToArray());
			}
			return result;
		}
	}
}
