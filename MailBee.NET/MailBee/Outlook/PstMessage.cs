using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using a.b;
using a.i;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x020005AE RID: 1454
	public class PstMessage : PstItem
	{
		// Token: 0x060030E7 RID: 12519 RVA: 0x000E4824 File Offset: 0x000E3824
		internal PstMessage(co A_0) : base(A_0)
		{
			this.c = "X-Msg-";
			this.b["MessageClass"] = A_0.gr();
			this.b["ClientSubmitTime"] = A_0.eu();
			this.b["ReceivedByName"] = A_0.dg();
			this.b["SentRepresentingName"] = A_0.em();
			this.b["SentRepresentingAddressType"] = A_0.db();
			this.b["SentRepresentingEmailAddress"] = A_0.dx();
			this.b["ConversationTopic"] = A_0.dj();
			this.b["ReceivedByAddressType"] = A_0.eh();
			this.b["ReceivedByAddress"] = A_0.c0();
			this.b["TransportMessageHeaders"] = A_0.de().Trim(new char[]
			{
				'\r',
				'\n'
			});
			this.b["Read"] = A_0.cr();
			this.b["Unmodified"] = A_0.dc();
			this.b["Submitted"] = A_0.e0();
			this.b["Unsent"] = A_0.ek();
			this.b["FromMe"] = A_0.en();
			this.b["Associated"] = A_0.c8();
			this.b["Resent"] = A_0.ec();
			this.b["AcknowledgementMode"] = A_0.eg();
			this.b["OriginatorDeliveryReportRequested"] = A_0.dw();
			this.b["ReadReceiptRequested"] = A_0.et();
			this.b["RecipientReassignmentProhibited"] = A_0.cy();
			this.b["OriginalSensitivity"] = A_0.dy();
			this.b["Sensitivity"] = A_0.dv();
			this.b["RcvdRepresentingName"] = A_0.ep();
			this.b["OriginalSubject"] = A_0.cp();
			this.b["ReplyRecipientNames"] = A_0.ed();
			this.b["MessageToMe"] = A_0.eo();
			this.b["MessageCcMe"] = A_0.ej();
			this.b["MessageRecipMe"] = A_0.dd();
			this.b["ResponseRequested"] = A_0.e5();
			this.b["SentRepresentingAddrtype"] = A_0.d2();
			this.b["OriginalDisplayBcc"] = A_0.d0();
			this.b["OriginalDisplayCc"] = A_0.ey();
			this.b["OriginalDisplayTo"] = A_0.du();
			this.b["RcvdRepresentingAddrtype"] = A_0.c1();
			this.b["RcvdRepresentingEmailAddress"] = A_0.c9();
			this.b["NonReceiptNotificationRequested"] = A_0.eq();
			this.b["OriginatorNonDeliveryReportRequested"] = A_0.dn();
			this.b["RecipientType"] = A_0.d9();
			this.b["ReplyRequested"] = A_0.e1();
			this.b["SenderName"] = A_0.cn();
			this.b["SenderAddrtype"] = A_0.dq();
			this.b["SenderEmailAddress"] = A_0.d3();
			this.b["MessageSize"] = A_0.cu();
			this.b["InternetArticleNumber"] = A_0.ee();
			this.b["URLCompNamePostfix"] = A_0.cm();
			this.b["ObjectType"] = A_0.dt();
			this.b["DeleteAfterSubmit"] = A_0.d1();
			this.b["Responsibility"] = A_0.c4();
			this.b["RtfInSync"] = A_0.eb();
			this.b["UrlCompNameSet"] = A_0.dr();
			this.b["DisplayBcc"] = A_0.e2();
			this.b["DisplayCc"] = A_0.el();
			this.b["DisplayTo"] = A_0.cz();
			this.b["MessageDeliveryTime"] = A_0.c6();
			this.b["RtfSyncBodyCrc"] = A_0.d8();
			this.b["RtfSyncBodyCount"] = A_0.c7();
			this.b["RtfSyncBodyTag"] = A_0.es();
			this.b["RtfSyncPrefixCount"] = A_0.e3();
			this.b["RtfSyncTrailingCount"] = A_0.cq();
			this.b["InternetMessageId"] = A_0.dk();
			this.b["InReplyToId"] = A_0.e6();
			this.b["ReturnPath"] = A_0.ef();
			this.b["IconIndex"] = A_0.cw();
			this.b["ActionFlag"] = A_0.e7();
			this.b["ActionDate"] = A_0.ct();
			this.b["DisableFullFidelity"] = A_0.ew();
			this.b["URLCompName"] = A_0.d5();
			this.b["AttrHidden"] = A_0.df();
			this.b["AttrSystem"] = A_0.cx();
			this.b["AttrReadonly"] = A_0.dp();
			this.b["TaskStartDate"] = A_0.co();
			this.b["TaskDueDate"] = A_0.da();
			this.b["Flagged"] = A_0.dh();
			this.b["HasForwarded"] = A_0.ex();
			this.b["HasReplied"] = A_0.ea();
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000E4FC6 File Offset: 0x000E3FC6
		public override PstItemType PstType
		{
			get
			{
				return base.PstType;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x000E4FCE File Offset: 0x000E3FCE
		public override Hashtable PstFields
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000E4FDF File Offset: 0x000E3FDF
		// (set) Token: 0x060030EA RID: 12522 RVA: 0x000E4FD6 File Offset: 0x000E3FD6
		public RtfInEmlStorageMethod RtfInEmlMethod
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

		// Token: 0x060030EC RID: 12524 RVA: 0x000E4FE8 File Offset: 0x000E3FE8
		internal new MailMessage a(co A_0, bool A_1)
		{
			MailMessage mailMessage = new MailMessage();
			string text = A_0.de();
			if (text != null && text != string.Empty)
			{
				HeaderCollection headerCollection = HeaderCollection.a(text, null);
				if (headerCollection["Content-Type"] != null && headerCollection["Content-Type"].IndexOf("ms-tnef") != -1)
				{
					headerCollection.Remove("Content-Type");
					headerCollection.Remove("Content-Transfer-Encoding");
					headerCollection.Add("Content-Type", "text/plain", false);
					headerCollection.Add("Content-Transfer-Encoding", "quoted-printable", false);
				}
				text = headerCollection.a();
				byte[] bytes = Global.DefaultEncoding.GetBytes(text);
				int num = global::a.i.k.a(bytes, 0, bytes.Length);
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes, 0, array, 0, num);
				mailMessage.LoadMessage(array);
				if ((mailMessage.Subject == null || mailMessage.Subject == string.Empty) && A_0.dz() != null && A_0.dz() != string.Empty)
				{
					mailMessage.Subject = A_0.dz();
				}
				if (mailMessage.Headers["Content-Type"] != null && mailMessage.Headers["Content-Type"].IndexOf("signed") != -1)
				{
					mailMessage.Headers.Remove("Content-Type");
					MailMessage mailMessage2 = new MailMessage();
					if (A_0.c5() == 1)
					{
						mailMessage2.LoadMessage(A_0.c(0).k());
						foreach (object obj in mailMessage.Headers)
						{
							Header a_ = (Header)obj;
							mailMessage2.Headers.b(a_);
						}
						return mailMessage2;
					}
				}
			}
			else
			{
				mailMessage.From.Email = A_0.d3();
				mailMessage.From.DisplayName = A_0.cn();
				bool flag = false;
				if (A_0.cz().IndexOf('@') == -1)
				{
					for (int i = 0; i < A_0.cv(); i++)
					{
						hf hf = A_0.b(i);
						if (hf != null && A_0.cz() == hf.f() && hf.d().IndexOf('@') != -1)
						{
							mailMessage.To.Add(new EmailAddress(hf.d(), hf.f()));
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					foreach (string displayName in A_0.cz().Split(new char[]
					{
						';'
					}))
					{
						EmailAddress emailAddress = new EmailAddress();
						emailAddress.DisplayName = displayName;
						mailMessage.To.Add(emailAddress);
					}
				}
				foreach (string displayName2 in A_0.el().Split(new char[]
				{
					';'
				}))
				{
					EmailAddress emailAddress2 = new EmailAddress();
					emailAddress2.DisplayName = displayName2;
					mailMessage.Cc.Add(emailAddress2);
				}
				foreach (string displayName3 in A_0.e2().Split(new char[]
				{
					';'
				}))
				{
					EmailAddress emailAddress3 = new EmailAddress();
					emailAddress3.DisplayName = displayName3;
					mailMessage.Bcc.Add(emailAddress3);
				}
				mailMessage.Date = A_0.ko();
				mailMessage.Headers["Subject"] = A_0.dz();
				switch (A_0.e4())
				{
				case 0:
					mailMessage.Importance = MailPriority.Low;
					break;
				case 1:
					mailMessage.Importance = MailPriority.Normal;
					break;
				case 2:
					mailMessage.Importance = MailPriority.High;
					break;
				}
				switch (A_0.@do())
				{
				case -1:
					mailMessage.Priority = MailPriority.Low;
					break;
				case 0:
					mailMessage.Priority = MailPriority.Normal;
					break;
				case 1:
					mailMessage.Priority = MailPriority.High;
					break;
				}
			}
			mailMessage.MailTransferEncodingHtml = MailTransferEncoding.QuotedPrintable;
			if (this.RtfInEmlMethod != RtfInEmlStorageMethod.None)
			{
				string text2 = A_0.d7();
				if (text2 != string.Empty)
				{
					RtfInEmlStorageMethod rtfInEmlMethod = this.RtfInEmlMethod;
					if (rtfInEmlMethod != RtfInEmlStorageMethod.AsAttachment)
					{
						if (rtfInEmlMethod == RtfInEmlStorageMethod.AsBodyPart)
						{
							mailMessage.BodyParts.Add("text/rtf");
							mailMessage.BodyParts["text/rtf"].Charset = Encoding.ASCII.EncodingName;
							mailMessage.BodyParts["text/rtf"].Text = text2;
						}
					}
					else
					{
						mailMessage.Attachments.Add(Encoding.ASCII.GetBytes(text2), "richbody.rtf", string.Empty, "text/rtf", null, NewAttachmentOptions.None, MailTransferEncoding.QuotedPrintable);
					}
				}
			}
			try
			{
				for (int k = 0; k < A_0.c5(); k++)
				{
					fl fl = A_0.c(k);
					try
					{
						if (fl.m() == 5)
						{
							MailMessage mailMessage3 = this.a(fl.q(), false);
							mailMessage.Attachments.Add(mailMessage3, mailMessage3.Subject, null, null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
						}
						else
						{
							string text3 = fl.d();
							if (text3 == null || text3 == string.Empty)
							{
								text3 = fl.a();
							}
							if (fl.j())
							{
								mailMessage.Attachments.Add(fl.k(), text3, fl.t(), null, null, NewAttachmentOptions.Inline, MailTransferEncoding.Base64);
							}
							else
							{
								mailMessage.Attachments.Add(fl.k(), text3, fl.t(), null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
							}
						}
					}
					catch (MailBeePstException)
					{
					}
				}
			}
			catch (Exception)
			{
			}
			mailMessage.BodyPlainText = A_0.d6();
			mailMessage.BodyHtmlText = A_0.c3();
			if (mailMessage.Charset == string.Empty && A_0.d4() != null)
			{
				mailMessage.Charset = A_0.d4();
			}
			if ((mailMessage.BodyHtmlText == null || mailMessage.BodyHtmlText == string.Empty) && A_0.d7() != null && A_0.d7() != string.Empty)
			{
				if (ba.a(A_0.d7()))
				{
					mailMessage.BodyHtmlText = ba.a(A_0.d7(), Global.DefaultEncoding);
					mailMessage.BodyHtmlText = mailMessage.BodyHtmlText.Replace("http://outlook/outlook9/specs/welcomemsg/", "cid:").Replace("d\\plain", string.Empty).Replace("d\\qc\\plain\\f0", string.Empty);
				}
				else
				{
					try
					{
						ip a_2 = cb.b(A_0.d7().Trim(new char[1]), new g5[0]);
						du du = new du();
						du.b("NonBreakingSpace=&nbsp;,LeftSingleQuote=&#8216;,RightSingleQuote=&#8217;,LeftDoubleQuote=&#8220;,RightDoubleQuote=&#8221;");
						string text4 = new @is(a_2, du).au();
						foreach (object obj2 in new Regex("<img width=\"0\" height=\"0\" src=\"(?<num>\\d+).bmp\" />").Matches(text4))
						{
							string value = ((Match)obj2).Groups["num"].Value;
							text4 = text4.Replace(string.Format("<img width=\"0\" height=\"0\" src=\"{0}.bmp\" />", value), string.Format("<img src=\"cid:outlook_rtf_{0}.bmp\" />", value));
						}
						mailMessage.BodyHtmlText = text4;
					}
					catch (Exception)
					{
					}
				}
			}
			if (A_0.gr() == "REPORT.IPM.Note.NDR")
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Arrival-Date: " + A_0.c6().ToString() + "\r\n");
				stringBuilder.Append("Final-Recipient: rfc822; " + A_0.du() + "\r\n");
				stringBuilder.Append("Action: failed");
				mailMessage.BodyParts.Add("message/delivery-status").Text = stringBuilder.ToString();
			}
			string[] array3 = A_0.g9();
			if (array3.Length != 0)
			{
				string text5 = string.Empty;
				foreach (string text6 in array3)
				{
					text5 += ((text5 != string.Empty) ? (";" + text6) : text6);
				}
				mailMessage.Headers["XCategories"] = text5;
			}
			return mailMessage;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000E5834 File Offset: 0x000E4834
		public override MailMessage GetAsMailMessage()
		{
			return this.GetAsMailMessage(true);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000E5840 File Offset: 0x000E4840
		public MailMessage GetAsMailMessage(bool includeHeaders)
		{
			MailMessage mailMessage = this.a((co)this.a, true);
			if (includeHeaders)
			{
				return base.a(mailMessage);
			}
			return mailMessage;
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000E586C File Offset: 0x000E486C
		public override int PstID
		{
			get
			{
				return base.PstID;
			}
		}

		// Token: 0x0400203A RID: 8250
		private new RtfInEmlStorageMethod a;
	}
}
