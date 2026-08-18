using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using a;
using a.b;
using a.h;
using a.i;
using MailBee.Mime;

namespace MailBee.Tnef
{
	// Token: 0x02000419 RID: 1049
	public class TnefParser
	{
		// Token: 0x0600249D RID: 9373 RVA: 0x0009BAB7 File Offset: 0x0009AAB7
		private TnefParser()
		{
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x0009BABF File Offset: 0x0009AABF
		public static AttachmentCollection GetAttachments(Stream data, TnefExtractionOptions options)
		{
			if (data == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			return TnefParser.a(data, options, true);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0009BAD4 File Offset: 0x0009AAD4
		public static AttachmentCollection GetAttachments(byte[] data, TnefExtractionOptions options)
		{
			if (data == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			return TnefParser.a(data, options, true);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0009BAE9 File Offset: 0x0009AAE9
		public static AttachmentCollection GetAttachments(string filename, TnefExtractionOptions options)
		{
			if (filename == null || filename == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			return TnefParser.a(global::a.ap.e(filename), options, true);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x0009BB10 File Offset: 0x0009AB10
		public static Task<AttachmentCollection> GetAttachmentsAsync(string filename, TnefExtractionOptions options)
		{
			TnefParser.a a;
			a.c = filename;
			a.d = options;
			a.b = AsyncTaskMethodBuilder<AttachmentCollection>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<AttachmentCollection> b = a.b;
			b.Start<TnefParser.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0009BB5D File Offset: 0x0009AB5D
		private static int a(byte[] A_0)
		{
			if (A_0.Length > 2 && A_0[A_0.Length - 2] == 13 && A_0[A_0.Length - 1] == 10)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0009BB80 File Offset: 0x0009AB80
		internal static AttachmentCollection a(byte[] A_0, TnefExtractionOptions A_1, bool A_2)
		{
			int num = TnefParser.a(A_0);
			MemoryStream memoryStream = null;
			AttachmentCollection result;
			try
			{
				memoryStream = new MemoryStream(A_0, 0, A_0.Length - num);
				result = TnefParser.a(memoryStream, A_1, A_2);
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Close();
				}
			}
			return result;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x0009BBCC File Offset: 0x0009ABCC
		internal static AttachmentCollection a(Stream A_0, TnefExtractionOptions A_1, bool A_2)
		{
			AttachmentCollection attachmentCollection = new AttachmentCollection(null);
			global::a.h.k k = null;
			try
			{
				if (A_0.Length != 0L)
				{
					k = new global::a.h.k(A_0);
					TnefParser.a(new global::a.h.i(k), attachmentCollection, A_1);
				}
			}
			catch (MailBeeTnefException)
			{
				if (A_2)
				{
					throw;
				}
				return null;
			}
			finally
			{
				if (k != null)
				{
					k.d();
				}
			}
			return attachmentCollection;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0009BC34 File Offset: 0x0009AC34
		private static void a(global::a.h.i A_0, AttachmentCollection A_1, TnefExtractionOptions A_2)
		{
			foreach (object obj in A_0.j())
			{
				global::a.h.b b = (global::a.h.b)obj;
				if (b.f() != null)
				{
					MailMessage mailMessage = new MailMessage();
					mailMessage.Subject = b.f().f();
					EmailAddress emailAddress = b.f().h();
					if (emailAddress != null)
					{
						mailMessage.From = emailAddress;
					}
					EmailAddressCollection emailAddressCollection = b.f().e();
					if (emailAddressCollection != null)
					{
						mailMessage.To = emailAddressCollection;
					}
					emailAddressCollection = b.f().g();
					if (emailAddressCollection != null)
					{
						mailMessage.Cc = emailAddressCollection;
					}
					emailAddressCollection = b.f().i();
					if (emailAddressCollection != null)
					{
						mailMessage.Bcc = emailAddressCollection;
					}
					DateTime dateTime = b.f().c();
					if (dateTime != DateTime.MinValue)
					{
						mailMessage.Date = dateTime;
					}
					TnefParser.a(b.f(), mailMessage.Attachments, A_2);
					string targetFilename = string.Format("{0}.eml", global::a.i.k.a());
					if (mailMessage.Attachments["richbody.rtf"] != null)
					{
						string a_ = Global.DefaultEncoding.GetString(mailMessage.Attachments["richbody.rtf"].GetData()).Trim(new char[1]);
						if (global::a.b.ba.a(a_))
						{
							mailMessage.BodyHtmlText = global::a.b.ba.a(a_, Global.DefaultEncoding);
							mailMessage.BodyHtmlText = mailMessage.BodyHtmlText.Replace("http://outlook/outlook9/specs/welcomemsg/", "cid:").Replace("d\\plain", string.Empty).Replace("d\\qc\\plain\\f0", string.Empty).Replace("\\htmlbase ATT-2-image001.png", "cid:image001.png").Replace("cid:image001.png@01CF8EFD.C455EF70", "cid:image001.png").Replace("cid:image003.png@01CF951A.B7B34060", "cid:image003.png").Replace("cid:image004.jpg@01CF951A.B7B34060", "cid:image004.jpg");
						}
						else
						{
							try
							{
								string text = new @is(cb.b(a_, new g5[0])).au();
								foreach (object obj2 in new Regex("<img width=\"0\" height=\"0\" src=\"(?<num>\\d+).bmp\" />").Matches(text))
								{
									string value = ((Match)obj2).Groups["num"].Value;
									text = text.Replace(string.Format("<img width=\"0\" height=\"0\" src=\"{0}.bmp\" />", value), string.Format("<img src=\"cid:outlook_rtf_{0}.bmp\" />", value));
								}
								mailMessage.BodyHtmlText = text;
							}
							catch (Exception)
							{
							}
						}
					}
					A_1.Add(mailMessage, targetFilename, null, null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
				}
				else if ((A_2 & TnefExtractionOptions.ExtractAttachments) > TnefExtractionOptions.None)
				{
					string targetFilename2 = b.d();
					string contentID = b.b().a(14098) as string;
					if (b.d() == null || b.d().Length == 0)
					{
						targetFilename2 = global::a.i.k.a();
					}
					if (b.a() == null)
					{
						HeaderCollection headerCollection = null;
						string text2 = b.b().a(14093) as string;
						if (text2 != null)
						{
							headerCollection = new HeaderCollection();
							headerCollection.Add("Content-Location", text2, true);
						}
						A_1.Add(new byte[0], targetFilename2, contentID, null, headerCollection, NewAttachmentOptions.None, MailTransferEncoding.Base64);
					}
					else
					{
						A_1.Add(new global::a.h.c(b.a()), targetFilename2, contentID, null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
					}
				}
			}
			if ((A_2 & TnefExtractionOptions.ExtractRtfBody) > TnefExtractionOptions.None)
			{
				byte[] array = A_0.k();
				if (array != null)
				{
					A_1.Add(array, "richbody.rtf", null, null, null, NewAttachmentOptions.None, MailTransferEncoding.Base64);
				}
			}
		}
	}
}
