using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using a;
using a.b;
using a.i;
using MailBee.Mime;

namespace MailBee.Outlook
{
	// Token: 0x02000589 RID: 1417
	public class MsgConvert
	{
		// Token: 0x06002F5C RID: 12124 RVA: 0x000DFF85 File Offset: 0x000DEF85
		public MsgConvert() : this(null)
		{
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000DFF8E File Offset: 0x000DEF8E
		public MsgConvert(string licenseKey)
		{
			MsgConvert.b(licenseKey);
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000DFFAC File Offset: 0x000DEFAC
		// (set) Token: 0x06002F5E RID: 12126 RVA: 0x000DFFA3 File Offset: 0x000DEFA3
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

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x000DFFBD File Offset: 0x000DEFBD
		// (set) Token: 0x06002F60 RID: 12128 RVA: 0x000DFFB4 File Offset: 0x000DEFB4
		public bool MsgAsDraft
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06002F63 RID: 12131 RVA: 0x000DFFCE File Offset: 0x000DEFCE
		// (set) Token: 0x06002F62 RID: 12130 RVA: 0x000DFFC5 File Offset: 0x000DEFC5
		public bool MsgIsFromMe
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002F65 RID: 12133 RVA: 0x000DFFDF File Offset: 0x000DEFDF
		// (set) Token: 0x06002F64 RID: 12132 RVA: 0x000DFFD6 File Offset: 0x000DEFD6
		public bool MsgAsUnicode
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002F67 RID: 12135 RVA: 0x000DFFF0 File Offset: 0x000DEFF0
		// (set) Token: 0x06002F66 RID: 12134 RVA: 0x000DFFE7 File Offset: 0x000DEFE7
		public bool PreferRtfBodyToHtml
		{
			get
			{
				return this.e;
			}
			set
			{
				this.e = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x000E0001 File Offset: 0x000DF001
		// (set) Token: 0x06002F68 RID: 12136 RVA: 0x000DFFF8 File Offset: 0x000DEFF8
		public HtmlToRtfConversionMethod HtmlToRtfMethod
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002F6B RID: 12139 RVA: 0x000E0012 File Offset: 0x000DF012
		// (set) Token: 0x06002F6A RID: 12138 RVA: 0x000E0009 File Offset: 0x000DF009
		public HtmlToRtfConversionHandler OnHtmlToRtfConversion
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x000E0023 File Offset: 0x000DF023
		// (set) Token: 0x06002F6C RID: 12140 RVA: 0x000E001A File Offset: 0x000DF01A
		public ByteToStringConversionHandler OnByteToStringConversion
		{
			get
			{
				return this.h;
			}
			set
			{
				this.h = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000E002B File Offset: 0x000DF02B
		// (set) Token: 0x06002F6F RID: 12143 RVA: 0x000E0037 File Offset: 0x000DF037
		[Obsolete("This property is obsolete. Use MailBee.Global.LicenseKey instead.")]
		public static string LicenseKey
		{
			get
			{
				return Resources.Instance.LicenseKeyIsWriteOnlyWarning;
			}
			set
			{
				Global.u = global::a.bn.a(value, typeof(MsgConvert));
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000E004E File Offset: 0x000DF04E
		internal static global::a.bm License
		{
			get
			{
				return Global.u;
			}
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000E0055 File Offset: 0x000DF055
		internal static void b(string A_0)
		{
			Global.a(typeof(MsgConvert), A_0);
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000E0067 File Offset: 0x000DF067
		public int TrialDaysLeft
		{
			get
			{
				return Global.u.b();
			}
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000E0073 File Offset: 0x000DF073
		public void MsgToEml(string msgFilename, string emlFilename)
		{
			this.MsgToMailMessage(msgFilename).SaveMessage(emlFilename);
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000E0083 File Offset: 0x000DF083
		public void MsgToEml(Stream msgStream, Stream emlStream)
		{
			this.MsgToMailMessage(msgStream).SaveMessage(emlStream);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000E0093 File Offset: 0x000DF093
		public void EmlToMsg(string emlFilename, string msgFilename)
		{
			this.a(emlFilename, msgFilename, this.d);
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000E00A4 File Offset: 0x000DF0A4
		private void a(string A_0, string A_1, bool A_2)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.LoadMessage(A_0);
			this.a(mailMessage, A_1, A_2);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000E00C8 File Offset: 0x000DF0C8
		public void EmlToMsg(Stream emlStream, Stream msgStream)
		{
			this.a(emlStream, msgStream, this.d);
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000E00D8 File Offset: 0x000DF0D8
		private void a(Stream A_0, Stream A_1, bool A_2)
		{
			MailMessage mailMessage = new MailMessage();
			mailMessage.LoadMessage(A_0);
			this.a(mailMessage, A_1, A_2);
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000E00FC File Offset: 0x000DF0FC
		public MailMessage MsgToMailMessage(string msgFilename)
		{
			if (msgFilename == null || msgFilename == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			FileStream fileStream = null;
			Encoding defaultEncoding = Global.DefaultEncoding;
			try
			{
				fileStream = new FileStream(msgFilename, FileMode.Open, FileAccess.Read);
				if (fileStream == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				if (!fileStream.CanRead)
				{
					throw new MailBeeStreamException(40);
				}
				fileStream = new FileStream(msgFilename, FileMode.Open, FileAccess.Read);
			}
			catch (ArgumentException a_)
			{
				throw new MailBeeIOException(20, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (IOException a_3)
			{
				throw new MailBeeIOException(30, a_3);
			}
			return this.a(fileStream, defaultEncoding);
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000E01A8 File Offset: 0x000DF1A8
		public MailMessage MsgToMailMessage(Stream msgStream)
		{
			return this.a(msgStream, Global.DefaultEncoding);
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000E01B8 File Offset: 0x000DF1B8
		private MailMessage a(Stream A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_0.CanRead)
			{
				throw new MailBeeStreamException(40);
			}
			new b8();
			global::a.b.ba a_ = b8.a(A_0, this.OnByteToStringConversion, this.e, A_1);
			return this.a(a_);
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000E0204 File Offset: 0x000DF204
		private MailMessage a(global::a.b.ba A_0)
		{
			MailMessage mailMessage = new MailMessage();
			if (A_0.s() != null)
			{
				A_0.l(A_0.s().Replace("multipart/signed", "multipart/mixed"));
				byte[] bytes = Global.DefaultEncoding.GetBytes(A_0.s());
				int num = global::a.i.k.a(bytes, 0, bytes.Length);
				byte[] array = new byte[num];
				Buffer.BlockCopy(bytes, 0, array, 0, num);
				mailMessage.LoadMessage(array);
			}
			if (A_0.s() == null || (A_0.s() != null && (mailMessage.From.AsString == string.Empty || mailMessage.To.AsString == string.Empty)))
			{
				if (A_0.h() != null)
				{
					if (A_0.h().Length > "0000000a".Length && A_0.h().StartsWith("0000000a"))
					{
						mailMessage.From.DisplayName = A_0.h().Substring("0000000a".Length + 1);
					}
					else
					{
						mailMessage.From.DisplayName = A_0.h();
					}
				}
				mailMessage.From.Email = A_0.i();
				if (A_0.l() != null)
				{
					foreach (object obj in A_0.l())
					{
						string[] array2 = (string[])obj;
						EmailAddress emailAddress = new EmailAddress();
						if (array2[1] == null || array2[1] != array2[0])
						{
							if (array2[0].IndexOf('@') != -1)
							{
								emailAddress.Email = array2[0];
							}
							else
							{
								emailAddress.DisplayName = array2[0];
							}
						}
						if (array2[1] != null)
						{
							emailAddress.Email = array2[1];
						}
						bool flag = false;
						using (IEnumerator enumerator2 = mailMessage.To.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (((EmailAddress)enumerator2.Current).Email == emailAddress.Email)
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							mailMessage.To.Add(emailAddress);
						}
					}
				}
				if (A_0.p() != null)
				{
					foreach (object obj2 in A_0.p())
					{
						string[] array3 = (string[])obj2;
						EmailAddress emailAddress2 = new EmailAddress();
						if (array3[1] == null || array3[1] != array3[0])
						{
							if (array3[0].IndexOf('@') != -1)
							{
								emailAddress2.Email = array3[0];
							}
							else
							{
								emailAddress2.DisplayName = array3[0];
							}
						}
						if (array3[1] != null)
						{
							emailAddress2.Email = array3[1];
						}
						bool flag2 = false;
						using (IEnumerator enumerator2 = mailMessage.Cc.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (((EmailAddress)enumerator2.Current).Email == emailAddress2.Email)
								{
									flag2 = true;
									break;
								}
							}
						}
						if (!flag2)
						{
							mailMessage.Cc.Add(emailAddress2);
						}
					}
				}
				if (A_0.g() != null)
				{
					foreach (object obj3 in A_0.g())
					{
						string[] array4 = (string[])obj3;
						EmailAddress emailAddress3 = new EmailAddress();
						if (array4[1] == null || array4[1] != array4[0])
						{
							if (array4[0].IndexOf('@') != -1)
							{
								emailAddress3.Email = array4[0];
							}
							else
							{
								emailAddress3.DisplayName = array4[0];
							}
						}
						if (array4[1] != null)
						{
							emailAddress3.Email = array4[1];
						}
						bool flag3 = false;
						using (IEnumerator enumerator2 = mailMessage.Bcc.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (((EmailAddress)enumerator2.Current).Email == emailAddress3.Email)
								{
									flag3 = true;
									break;
								}
							}
						}
						if (!flag3)
						{
							mailMessage.Bcc.Add(emailAddress3);
						}
					}
				}
			}
			mailMessage.Subject = A_0.n();
			mailMessage.Priority = A_0.j();
			if (A_0.r() != null)
			{
				mailMessage.BodyPlainText = A_0.r();
			}
			if (A_0.a() != null)
			{
				mailMessage.BodyHtmlText = A_0.a().Replace("<img border=\"0\" img src=\"ofc-Otlk07-BCM_rgb.jpg\"", "<img border=\"0\" src=\"cid:ofc-Otlk07-BCM_rgb.jpg\"").Replace("src=\"\\htmlbase image002.gif\">", "src=\"cid:image002.gif@01C54A77.5AC08FE0\">").Replace("src=\"\\htmlbase image001.jpg\"", "src=\"cid:image001.jpg@01CDDEA3.D0127B00\"").Replace("src=\"\\htmlbase image001.png\"", "src=\"cid:image001.png\"").Replace("src=\"\\htmlbase ATT-0-image001.png\"", "src=\"cid:image001.png\"").Replace("d\\plain", string.Empty).Replace("d\\intbl\\itap2\\cbpat6\\plain", string.Empty).Replace("d\\intbl\\itap2\\qc\\plain", string.Empty).Replace("d\\intbl\\itap2\\plain", string.Empty).Replace("\\u-3913 ?", "\\b7").Replace("\\u-3929 ?", "\\a7");
				Regex regex = new Regex("\\\\u(\\d{3,4}) ?\\?");
				mailMessage.BodyHtmlText = regex.Replace(mailMessage.BodyHtmlText, "&#$1;");
			}
			if (A_0.k() != null)
			{
				RtfInEmlStorageMethod rtfInEmlMethod = this.RtfInEmlMethod;
				if (rtfInEmlMethod != RtfInEmlStorageMethod.AsAttachment)
				{
					if (rtfInEmlMethod == RtfInEmlStorageMethod.AsBodyPart)
					{
						mailMessage.BodyParts.Add("text/rtf");
						mailMessage.BodyParts["text/rtf"].Charset = Encoding.ASCII.EncodingName;
						mailMessage.BodyParts["text/rtf"].Text = A_0.k();
					}
				}
				else
				{
					mailMessage.Attachments.Add(A_0.c(), "richbody.rtf", string.Empty, "text/rtf", null, NewAttachmentOptions.None, MailTransferEncoding.QuotedPrintable);
				}
				if (A_0.a() == null)
				{
					try
					{
						string text = new @is(cb.b(A_0.k().Trim(new char[1]), new g5[0])).au();
						foreach (object obj4 in new Regex("<img width=\"0\" height=\"0\" src=\"(?<num>\\d+).bmp\" />").Matches(text))
						{
							string value = ((Match)obj4).Groups["num"].Value;
							text = text.Replace(string.Format("<img width=\"0\" height=\"0\" src=\"{0}.bmp\" />", value), string.Format("<img src=\"cid:outlook_rtf_{0}.bmp\" />", value));
						}
						mailMessage.BodyHtmlText = text;
					}
					catch (Exception)
					{
					}
				}
			}
			mailMessage.Date = ((A_0.q() != DateTime.MinValue && (A_0.b() == DateTime.MinValue || A_0.q() < A_0.b().AddDays(1.0))) ? A_0.q() : A_0.b());
			if (A_0.m() != DateTime.MinValue)
			{
				mailMessage.Headers.a(0, new Header("Received", global::a.i.k.a(A_0.m())));
			}
			if (A_0.b() != DateTime.MinValue)
			{
				mailMessage.Headers["X-Date-Sent"] = global::a.i.k.a(A_0.b());
				if (A_0.m() == DateTime.MinValue)
				{
					mailMessage.Headers.a(0, new Header("Received", mailMessage.Headers["X-Date-Sent"]));
				}
			}
			for (int i = 0; i < A_0.v().Count; i++)
			{
				if (A_0.v()[i] is e4)
				{
					e4 e = (e4)A_0.v()[i];
					string text2 = e.a();
					if (text2 == null || text2 == string.Empty)
					{
						text2 = e.c();
					}
					if (e.g() != null && e.g() == "multipart/signed")
					{
						MailMessage mailMessage2 = new MailMessage();
						mailMessage2.LoadMessage(e.e());
						mailMessage2.From = mailMessage.From;
						mailMessage2.To = mailMessage.To;
						mailMessage2.Cc = mailMessage.Cc;
						mailMessage2.Bcc = mailMessage.Bcc;
						mailMessage2.Subject = mailMessage.Subject;
						mailMessage2.Priority = mailMessage.Priority;
						mailMessage2.Date = mailMessage.Date;
						foreach (object obj5 in mailMessage.Headers)
						{
							Header header = (Header)obj5;
							if (header.Name.IndexOf("Message-ID") != -1)
							{
								mailMessage2.Headers.b(header);
							}
						}
						return mailMessage2;
					}
					string text3 = e.h();
					if (text3 == null && text2 == "ofc-Otlk07-BCM_rgb.jpg")
					{
						text3 = text2;
					}
					string text4 = e.g();
					if (text4 == null)
					{
						text4 = string.Empty;
						if (e.b() != null && e.b().Length > 3)
						{
							text4 = global::a.i.k.e(e.b().Substring(1).ToLower());
						}
					}
					if (mailMessage.IsEncrypted && mailMessage.ContentType != null && e.g() != null && mailMessage.Attachments.Count > 0 && mailMessage.ContentType.ToLower() == "application/pkcs7-mime" && e.g().ToLower() == "application/pkcs7-mime")
					{
						mailMessage.Attachments[0].AsMimePart.PartValueAsBytes = e.e();
						mailMessage.Attachments[0].AsMimePart.MimePartTransferEncoding = MailTransferEncoding.Base64;
					}
					else if (mailMessage.ContentType != null && e.g() != null && mailMessage.Attachments.Count > 0 && mailMessage.ContentType.ToLower() == "application/ms-tnef" && e.g().ToLower() == "application/pkcs7-mime")
					{
						Header header2 = mailMessage.Headers.a("Content-Type");
						if (header2 != null)
						{
							header2.Value = "application/pkcs7-mime";
							header2.HeaderParameters = ((header2.HeaderParameters != null) ? header2.HeaderParameters : new global::a.i.j());
							header2.HeaderParameters.b();
							header2.HeaderParameters.c(new global::a.i.n("smime-type", "enveloped-data"));
							header2.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
						}
						else
						{
							header2 = new Header("Content-Type", "application/pkcs7-mime");
							header2.HeaderParameters = ((header2.HeaderParameters != null) ? header2.HeaderParameters : new global::a.i.j());
							header2.HeaderParameters.c(new global::a.i.n("smime-type", "enveloped-data"));
							header2.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
							mailMessage.Headers.b(header2);
						}
						Header header3 = mailMessage.Headers.a("Content-Disposition");
						if (header3 != null)
						{
							header3.Value = "attachment";
							header3.HeaderParameters = ((header3.HeaderParameters != null) ? header3.HeaderParameters : new global::a.i.j());
							header3.HeaderParameters.b();
							header3.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
						}
						else
						{
							header3 = new Header("Content-Disposition", "attachment");
							header3.HeaderParameters = ((header3.HeaderParameters != null) ? header3.HeaderParameters : new global::a.i.j());
							header3.HeaderParameters.c(new global::a.i.n("name", "smime.p7m"));
							mailMessage.Headers.b(header3);
						}
						mailMessage.Attachments[0].AsMimePart.PartValueAsBytes = e.e();
						mailMessage.Attachments[0].AsMimePart.MimePartTransferEncoding = MailTransferEncoding.Base64;
					}
					else
					{
						mailMessage.Attachments.Add(e.e(), text2, (text3 != null) ? text3 : string.Empty, text4, null, (text3 != null) ? NewAttachmentOptions.Inline : NewAttachmentOptions.None, MailTransferEncoding.Base64);
					}
				}
				else if (A_0.v()[i] is ij)
				{
					ij ij = (ij)A_0.v()[i];
					MailMessage mailMessage3 = this.a(ij.a());
					mailMessage.Attachments.Add(mailMessage3, mailMessage3.Subject + ".eml", string.Empty, "message/rfc822", null, NewAttachmentOptions.None, MailTransferEncoding.None);
				}
			}
			for (int j = mailMessage.Attachments.Count - 1; j >= 0; j--)
			{
				if (mailMessage.Attachments[j].Filename == "winmail.dat")
				{
					mailMessage.Attachments.RemoveAt(j);
				}
			}
			if (A_0.f() != null)
			{
				mailMessage.Charset = A_0.f().WebName;
			}
			else
			{
				Encoding encoding = global::a.bb.b(mailMessage.BodyHtmlText);
				if (encoding != null)
				{
					mailMessage.Charset = encoding.HeaderName;
					mailMessage.Builder.Apply();
				}
			}
			mailMessage.k();
			return mailMessage;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000E0FD4 File Offset: 0x000DFFD4
		private Stream a(string A_0, bool A_1)
		{
			if (A_0 == null || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			Stream baseStream;
			try
			{
				baseStream = new BinaryWriter(new FileStream(new FileInfo(A_0).FullName, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, true)).BaseStream;
			}
			catch (IOException a_)
			{
				throw new MailBeeIOException(30, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (ArgumentException a_3)
			{
				throw new MailBeeIOException(20, a_3);
			}
			return baseStream;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000E1068 File Offset: 0x000E0068
		public void MailMessageToMsg(MailMessage msg, string msgFilename)
		{
			this.a(msg, msgFilename, this.d);
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000E1078 File Offset: 0x000E0078
		private void a(MailMessage A_0, string A_1, bool A_2)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			Stream stream = this.a(A_1, false);
			try
			{
				this.a(A_0, stream, A_2);
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000E10BC File Offset: 0x000E00BC
		public void MailMessageToMsg(MailMessage msg, Stream msgStream)
		{
			this.a(msg, msgStream, this.d);
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000E10CC File Offset: 0x000E00CC
		private void a(MailMessage A_0, Stream A_1, bool A_2)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_1.CanWrite)
			{
				throw new MailBeeStreamException(41);
			}
			this.a(A_0, A_2).a(A_1, this.HtmlToRtfMethod > HtmlToRtfConversionMethod.None);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000E1104 File Offset: 0x000E0104
		private g4 a(MailMessage A_0, bool A_1)
		{
			string text = null;
			if (this.HtmlToRtfMethod == HtmlToRtfConversionMethod.UseDelegate && this.OnHtmlToRtfConversion != null)
			{
				text = this.OnHtmlToRtfConversion(A_0, A_0.BodyHtmlText);
			}
			g4 g = new g4(A_1, this.b, this.c);
			Encoding a_ = null;
			if (A_0.Charset != null && A_0.Charset != string.Empty)
			{
				try
				{
					a_ = Encoding.GetEncoding(A_0.Charset);
				}
				catch (ArgumentException)
				{
				}
				g.a(a_);
			}
			if (A_0.From.AsString != string.Empty)
			{
				g.a(A_0.From.DisplayName, A_0.From.Email);
			}
			if (A_0.Subject != string.Empty)
			{
				g.e(A_0.Subject);
			}
			if (A_0.RawHeader.Length > 0)
			{
				g.a(A_0.RawHeader);
			}
			if (A_0.Date > DateTime.MinValue)
			{
				g.c(A_0.Date);
			}
			if (A_0.DateReceived > DateTime.MinValue)
			{
				g.b(A_0.DateReceived);
			}
			if (A_0.DateSent > DateTime.MinValue)
			{
				g.a(A_0.DateSent);
			}
			else if (A_0.Date > DateTime.MinValue)
			{
				g.a(A_0.Date);
			}
			foreach (object obj in A_0.To)
			{
				EmailAddress emailAddress = (EmailAddress)obj;
				g.a(emailAddress.DisplayName, emailAddress.Email, 1);
			}
			foreach (object obj2 in A_0.Cc)
			{
				EmailAddress emailAddress2 = (EmailAddress)obj2;
				g.a(emailAddress2.DisplayName, emailAddress2.Email, 2);
			}
			foreach (object obj3 in A_0.Bcc)
			{
				EmailAddress emailAddress3 = (EmailAddress)obj3;
				g.a(emailAddress3.DisplayName, emailAddress3.Email, 3);
			}
			g.a(A_0.Priority);
			if (A_0.BodyHtmlText.Length != 0)
			{
				g.b(A_0.BodyHtmlText);
				switch (this.HtmlToRtfMethod)
				{
				case HtmlToRtfConversionMethod.None:
					if (this.b)
					{
						g.e();
					}
					break;
				case HtmlToRtfConversionMethod.Internal:
				{
					b0 b = new b0();
					g.f(b.b(A_0.BodyHtmlText));
					break;
				}
				case HtmlToRtfConversionMethod.UseDelegate:
					if (text != null)
					{
						g.f(text);
					}
					break;
				}
			}
			if (A_0.BodyPlainText.Length != 0)
			{
				g.d(A_0.BodyPlainText);
			}
			foreach (object obj4 in A_0.Attachments)
			{
				Attachment attachment = (Attachment)obj4;
				g.a(attachment.Filename, attachment.GetData(), attachment.ContentID, attachment.ContentType);
			}
			return g;
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000E147C File Offset: 0x000E047C
		private string a(string A_0)
		{
			StringCollection stringCollection = new StringCollection();
			int num = 0;
			int num2 = 4;
			char c = ' ';
			for (int i = 0; i < A_0.Length; i++)
			{
				if (num2 == 1 || num2 == 3)
				{
					num2 = 4;
				}
				if (num2 == 4)
				{
					if (A_0[i] == '<')
					{
						num2 = 0;
					}
					else
					{
						num2 = 2;
					}
					num = i;
				}
				if (num2 == 0)
				{
					if (i > 2 && A_0.Length >= num + 4 && A_0.Substring(num, 4) == "<!--")
					{
						if (A_0.Substring(i - 2, 3) == "-->" || i == A_0.Length - 1)
						{
							num2 = 1;
						}
					}
					else if (A_0[i] == '\'' || A_0[i] == '"')
					{
						num2 = 5;
						c = A_0[i];
					}
					else if (A_0[i] == '>' || i == A_0.Length - 1)
					{
						num2 = 1;
					}
				}
				else if (num2 == 5 && (A_0[i] == '>' || i == A_0.Length - 1))
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 5 && c == A_0[i])
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 2 && ((i < A_0.Length - 1 && A_0[i + 1] == '<') || i == A_0.Length - 1))
				{
					num2 = 3;
				}
				if (num2 == 1 || num2 == 3 || i == A_0.Length - 1)
				{
					int length = i - num + 1;
					stringCollection.Add(A_0.Substring(num, length));
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\rtf1\\fromhtml1\r\n\r\n");
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int j = 0; j < stringCollection.Count; j++)
			{
				if (stringCollection[j][0] == '<')
				{
					if (j > 0 && !flag)
					{
						stringBuilder.Append("\r\n\r\n");
					}
					if (stringCollection[j].Trim().ToUpper().StartsWith("<BR") || stringCollection[j].Trim().ToUpper().StartsWith("<P") || stringCollection[j].Trim().ToUpper().StartsWith("<TR"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} \\line"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("<HR"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"}"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("<B>") || stringCollection[j].Trim().ToUpper().StartsWith("<STRONG>"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} \\b"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("</B>") || stringCollection[j].Trim().ToUpper().StartsWith("</STRONG>"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} \\b0"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("<I>"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} \\i"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("</I>"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} \\i0"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("<U>"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} \\ul"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("</U>"))
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} \\ul0"
						}));
						flag = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("<A HREF"))
					{
						int num3 = stringCollection[j].IndexOf('"');
						int num4 = -1;
						if (num3 > 0)
						{
							num4 = stringCollection[j].LastIndexOf('"');
						}
						else
						{
							num3 = stringCollection[j].IndexOf('\'');
							if (num3 > 0)
							{
								num4 = stringCollection[j].LastIndexOf('\'');
							}
							else
							{
								num3 = stringCollection[j].IndexOf('=');
								if (num3 > 0)
								{
									num4 = stringCollection[j].LastIndexOf('>');
								}
							}
						}
						if (num3 == num4 && num3 != -1)
						{
							num4 = stringCollection[j].IndexOf('>');
						}
						string text = string.Empty;
						if (num3 != -1 && num4 != -1)
						{
							text = stringCollection[j].Substring(num3 + 1, num4 - num3 - 1);
						}
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"} {\\field{\\*\\fldinst{HYPERLINK \"",
							text,
							"\"}}{\\fldrslt\\ul "
						}));
						flag = true;
						flag3 = true;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("</A>"))
					{
						if (flag3)
						{
							stringBuilder.Append("}}\r\n");
						}
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"}"
						}));
						flag = false;
						flag3 = false;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("<STYLE"))
					{
						flag2 = true;
					}
					else if (stringCollection[j].Trim().ToUpper().StartsWith("</STYLE"))
					{
						flag2 = false;
					}
					else
					{
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							j,
							" ",
							stringCollection[j],
							"}"
						}));
					}
				}
				else if (!(stringCollection[j].Trim() == string.Empty) && !flag2)
				{
					stringBuilder.Append("\r\n ");
					for (int k = 0; k < stringCollection[j].Length; k++)
					{
						if ((short)stringCollection[j][k] > 127 || (short)stringCollection[j][k] < 0)
						{
							stringBuilder.Append("\\u" + ((short)stringCollection[j][k]).ToString() + "\\'f3");
						}
						else
						{
							stringBuilder.Append(stringCollection[j][k]);
						}
					}
					flag = false;
				}
			}
			stringBuilder.Append("\r\n\r\n}");
			return stringBuilder.ToString();
		}

		// Token: 0x0400200C RID: 8204
		private RtfInEmlStorageMethod a;

		// Token: 0x0400200D RID: 8205
		private bool b;

		// Token: 0x0400200E RID: 8206
		private bool c;

		// Token: 0x0400200F RID: 8207
		private bool d = true;

		// Token: 0x04002010 RID: 8208
		private bool e;

		// Token: 0x04002011 RID: 8209
		private HtmlToRtfConversionMethod f;

		// Token: 0x04002012 RID: 8210
		private HtmlToRtfConversionHandler g;

		// Token: 0x04002013 RID: 8211
		private ByteToStringConversionHandler h;

		// Token: 0x0200058A RID: 1418
		private enum a
		{
			// Token: 0x04002015 RID: 8213
			a,
			// Token: 0x04002016 RID: 8214
			b,
			// Token: 0x04002017 RID: 8215
			c,
			// Token: 0x04002018 RID: 8216
			d,
			// Token: 0x04002019 RID: 8217
			e,
			// Token: 0x0400201A RID: 8218
			f
		}
	}
}
