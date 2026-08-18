using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailBee.Mime;

namespace a.i
{
	// Token: 0x020001F1 RID: 497
	internal class e
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x00040A15 File Offset: 0x0003FA15
		private e()
		{
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00040A20 File Offset: 0x0003FA20
		internal static string b(string A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(A_0)))
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00040A7C File Offset: 0x0003FA7C
		public static string b(string A_0, VirtualMappingType A_1, MessageFolderBehavior A_2, MailMessage A_3, MessageParserConfig A_4, bool A_5)
		{
			string result = string.Empty;
			string a_ = string.Empty;
			bool flag = false;
			if (A_1 != VirtualMappingType.Base64)
			{
				switch (A_2)
				{
				case MessageFolderBehavior.CreateAndDelete:
					a_ = e.b(A_3.MessageID);
					flag = true;
					break;
				case MessageFolderBehavior.CreateOnly:
					a_ = e.b(A_3.MessageID);
					flag = false;
					break;
				}
			}
			string text = null;
			MimePart mimePart = A_3.MimeParts["text/html"];
			if (mimePart != null)
			{
				text = mimePart.Headers["Content-Location"];
				if (text != null)
				{
					int num = text.LastIndexOf('/');
					if (num > 0)
					{
						text = text.Substring(0, num + 1);
					}
				}
			}
			switch (A_1)
			{
			case VirtualMappingType.NonWeb:
				if (!A_5)
				{
					A_0 = ap.a(A_0, a_);
				}
				result = e.a(text, A_3.BodyHtmlText, A_3.Attachments, A_0, VirtualMappingType.NonWeb, A_4.WorkingFolder == string.Empty, A_0, A_4, true);
				if (flag)
				{
					A_3.FolderToDelete = A_0;
					A_3.FolderToDeleteCreated = true;
				}
				break;
			case VirtualMappingType.Static:
			{
				string text2 = ap.a(A_4.WorkingFolder, a_);
				A_0 = global::a.i.b.a(A_0, a_);
				result = e.a(text, A_3.BodyHtmlText, A_3.Attachments, A_0, VirtualMappingType.Static, false, text2, A_4, true);
				if (flag)
				{
					A_3.FolderToDelete = text2;
					A_3.FolderToDeleteCreated = true;
				}
				break;
			}
			case VirtualMappingType.Dynamic:
			{
				string text3 = ap.a(A_4.WorkingFolder, a_);
				string a_2 = string.Format(CultureInfo.InvariantCulture, "{0}message_id={1}&file_id=", new object[]
				{
					A_0,
					e.b(A_3.MessageID)
				});
				result = e.a(text, A_3.BodyHtmlText, A_3.Attachments, a_2, VirtualMappingType.Dynamic, false, text3, A_4, true);
				if (flag)
				{
					A_3.FolderToDelete = text3;
					A_3.FolderToDeleteCreated = true;
				}
				break;
			}
			case VirtualMappingType.Base64:
				result = e.a(text, A_3.BodyHtmlText, A_3.Attachments);
				break;
			case VirtualMappingType.StaticInMemory:
				result = e.a(text, A_3.BodyHtmlText, A_3.Attachments, A_0, VirtualMappingType.StaticInMemory, false, null, A_4, false);
				break;
			}
			return result;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00040C6C File Offset: 0x0003FC6C
		private static string a(string A_0, string A_1, AttachmentCollection A_2, string A_3, VirtualMappingType A_4, bool A_5, string A_6, MessageParserConfig A_7, bool A_8)
		{
			int num = 0;
			string str = A_3;
			foreach (object obj in A_2)
			{
				Attachment attachment = (Attachment)obj;
				bool flag = false;
				if (attachment.ContentID != null && attachment.ContentID.Length != 0)
				{
					string contentID = attachment.ContentID;
					MatchCollection matchCollection = new Regex(string.Format(CultureInfo.InvariantCulture, "\"?cid:(\\s)*({0})\"?", new object[]
					{
						Regex.Escape(contentID)
					}), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline).Matches(A_1);
					if (matchCollection.Count > 0)
					{
						if (A_8)
						{
							attachment.SaveToFolder(A_6, false);
							A_7.MessageFolderInternal = attachment.SavedAs;
						}
						else
						{
							num++;
							A_3 = str + num.ToString();
							attachment.c(A_3);
						}
						e.a(matchCollection, A_4, attachment, A_3, ref A_1, A_5);
						flag = true;
					}
				}
				if (!flag && attachment.ContentLocation != null && attachment.ContentLocation.Length != 0)
				{
					string text = attachment.ContentLocation;
					if (A_0 != null && text.StartsWith(A_0, StringComparison.OrdinalIgnoreCase))
					{
						text = text.Substring(A_0.Length);
					}
					MatchCollection matchCollection2 = new Regex(string.Format(CultureInfo.InvariantCulture, "\"?{0}\"?", new object[]
					{
						Regex.Escape(text)
					}), RegexOptions.IgnoreCase | RegexOptions.Singleline).Matches(A_1);
					if (matchCollection2.Count > 0)
					{
						if (A_8)
						{
							attachment.SaveToFolder(A_6, false);
							A_7.MessageFolderInternal = attachment.SavedAs;
						}
						else
						{
							num++;
							A_3 = str + num.ToString();
							attachment.c(A_3);
						}
						e.a(matchCollection2, A_4, attachment, A_3, ref A_1, A_5);
					}
				}
			}
			return A_1;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00040E40 File Offset: 0x0003FE40
		private static void a(MatchCollection A_0, VirtualMappingType A_1, Attachment A_2, string A_3, ref string A_4, bool A_5)
		{
			foreach (object obj in A_0)
			{
				Match match = (Match)obj;
				string text = string.Empty;
				switch (A_1)
				{
				case VirtualMappingType.NonWeb:
					text = A_2.SavedAs;
					if (A_5)
					{
						if (A_3 != string.Empty)
						{
							int num = text.IndexOf(A_3, StringComparison.OrdinalIgnoreCase);
							if (num == 0)
							{
								text = text.Remove(0, A_3.Length);
								while (text.StartsWith("\\"))
								{
									text = text.Substring(1);
								}
							}
							else if (num > 0)
							{
								text = text.Remove(0, num);
							}
						}
						else
						{
							text = Path.GetFileName(text);
						}
					}
					A_4 = A_4.Replace(match.Value, string.Format("'{1}{0}'", text.Replace("#", "%23").Replace("'", "%27"), (!A_5) ? "file:///" : ""));
					break;
				case VirtualMappingType.Static:
					text = string.Empty;
					if (A_2.SavedAs != null && A_2.SavedAs.Length != 0)
					{
						text = Path.GetFileName(A_2.SavedAs);
					}
					else
					{
						text = ((A_2.FilenameOriginalInternal != null && A_2.FilenameOriginalInternal.Length != 0) ? A_2.FilenameOriginalInternal : A_2.NameInternal);
					}
					A_4 = A_4.Replace(match.Value, string.Format("'{0}'", Uri.EscapeUriString(global::a.i.b.a(A_3, text).Replace("'", "%27"))));
					break;
				case VirtualMappingType.Dynamic:
					text = string.Empty;
					if (A_2.Filename != null && A_2.Filename.Length != 0)
					{
						text = A_2.Filename;
					}
					else
					{
						text = ((A_2.FilenameOriginalInternal != null && A_2.FilenameOriginalInternal.Length != 0) ? A_2.FilenameOriginalInternal : A_2.NameInternal);
					}
					A_4 = A_4.Replace(match.Value, string.Format("'{0}'", Uri.EscapeUriString(A_3 + Uri.EscapeUriString(text)).Replace("'", "%27")));
					break;
				case VirtualMappingType.StaticInMemory:
					A_4 = A_4.Replace(match.Value, string.Format("'{0}'", A_3.Replace("'", "%27")));
					break;
				}
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000410B0 File Offset: 0x000400B0
		private static string a(string A_0, string A_1, AttachmentCollection A_2)
		{
			foreach (object obj in A_2)
			{
				Attachment attachment = (Attachment)obj;
				bool flag = false;
				if (attachment.ContentID != null && attachment.ContentID.Length != 0)
				{
					string contentID = attachment.ContentID;
					MatchCollection matchCollection = new Regex(string.Format(CultureInfo.InvariantCulture, "\"?cid:(\\s)*({0})\"?", new object[]
					{
						Regex.Escape(contentID)
					}), RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline).Matches(A_1);
					if (matchCollection.Count > 0)
					{
						e.a(matchCollection, attachment, ref A_1);
						flag = true;
					}
				}
				if (!flag && attachment.ContentLocation != null && attachment.ContentLocation.Length != 0)
				{
					string text = attachment.ContentLocation;
					if (A_0 != null && text.StartsWith(A_0, StringComparison.OrdinalIgnoreCase))
					{
						text = text.Substring(A_0.Length);
					}
					e.a(new Regex(string.Format(CultureInfo.InvariantCulture, "\"?{0}\"?", new object[]
					{
						Regex.Escape(text)
					}), RegexOptions.IgnoreCase | RegexOptions.Singleline).Matches(A_1), attachment, ref A_1);
				}
			}
			return A_1;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000411DC File Offset: 0x000401DC
		private static string a(Attachment A_0)
		{
			return "data:" + A_0.ContentType + ";base64," + Convert.ToBase64String(A_0.GetData());
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00041200 File Offset: 0x00040200
		private static void a(MatchCollection A_0, Attachment A_1, ref string A_2)
		{
			foreach (object obj in A_0)
			{
				Match match = (Match)obj;
				A_2 = A_2.Replace(match.Value, "\"" + e.a(A_1) + "\"");
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00041274 File Offset: 0x00040274
		public static void a(string A_0, Attachment A_1, ref string A_2)
		{
			if (A_1.ContentLocation != null && A_1.ContentLocation.Length != 0)
			{
				string text = A_1.ContentLocation;
				if (A_0 != null && text.StartsWith(A_0, StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(A_0.Length);
				}
				if (text.Length > 0)
				{
					MatchCollection matchCollection = new Regex(string.Format(CultureInfo.InvariantCulture, "\"?({0}|{1})\"?", new object[]
					{
						Regex.Escape(A_1.ContentLocation),
						Regex.Escape(text)
					}), RegexOptions.IgnoreCase | RegexOptions.Singleline).Matches(A_2);
					string text2 = A_1.Headers["Content-ID"];
					if (text2 == null || text2 == string.Empty)
					{
						text2 = k.a();
						A_1.Headers["Content-ID"] = text2;
					}
					foreach (object obj in matchCollection)
					{
						Match match = (Match)obj;
						A_2 = A_2.Replace(match.Value, string.Format("\"cid:{0}\"", text2));
					}
				}
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00041398 File Offset: 0x00040398
		public static void a(AHRefTagAttributes A_0, string A_1, MailMessage A_2)
		{
			if (A_0 == AHRefTagAttributes.None && (A_1 == null || A_1.Length == 0))
			{
				return;
			}
			if (A_2.BodyHtmlText != null && A_2.BodyHtmlText.Length != 0)
			{
				A_2.BodyHtmlText = global::a.i.b.a(A_2.BodyHtmlText, A_0, A_1);
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000413D4 File Offset: 0x000403D4
		public static void a(HtmlMessageAutoSaving A_0, MailMessage A_1, MessageParserConfig A_2)
		{
			switch (A_0)
			{
			case HtmlMessageAutoSaving.NoAutoSave:
				break;
			case HtmlMessageAutoSaving.SaveMessageHtmAndRelatedFiles:
				A_1.SaveHtmlAndRelatedFiles(ap.a(A_2.WorkingFolder, "message.htm"));
				return;
			case HtmlMessageAutoSaving.SaveMessageMht:
				A_1.SaveMessage(ap.a(A_2.WorkingFolder, "message.mht"));
				break;
			case HtmlMessageAutoSaving.AlterHtmlBody:
				if (A_1.BodyHtmlText != null && A_1.BodyHtmlText.Length != 0)
				{
					A_1.BodyHtmlText = e.b(A_2.WorkingFolder, VirtualMappingType.NonWeb, MessageFolderBehavior.CreateOnly, A_1, A_2, false);
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00041454 File Offset: 0x00040454
		public static string a(string A_0)
		{
			foreach (string input in bb.a("meta", A_0, true))
			{
				Match match = m.j.Match(input);
				if (match.Success)
				{
					return match.Value;
				}
			}
			return null;
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000414CC File Offset: 0x000404CC
		public static void a(MailMessage A_0, CharsetMetaTagProcessing A_1)
		{
			if (A_1 == CharsetMetaTagProcessing.SetCorrectCharset)
			{
				foreach (string text in bb.a("meta", A_0.BodyHtmlText, true))
				{
					Match match = m.j.Match(text);
					if (match.Success)
					{
						switch (A_0.Parser.CharsetConverter.ConversionMode)
						{
						case StringConversionMode.NoConversion:
							A_0.BodyHtmlText = A_0.BodyHtmlText.Replace(text, string.Empty);
							break;
						case StringConversionMode.KeepOriginalByteEncoding:
						{
							string newValue = text.Replace(match.Value, string.Format(CultureInfo.InvariantCulture, "charset={0}", new object[]
							{
								A_0.Charset
							}));
							A_0.BodyHtmlText = A_0.BodyHtmlText.Replace(text, newValue);
							break;
						}
						case StringConversionMode.ConvertToWinByteEncoding:
						{
							string newValue2 = text.Replace(match.Value, string.Format(CultureInfo.InvariantCulture, "charset={0}", new object[]
							{
								Encoding.GetEncoding(bb.a(A_0.Charset).WindowsCodePage)
							}));
							A_0.BodyHtmlText = A_0.BodyHtmlText.Replace(text, newValue2);
							break;
						}
						case StringConversionMode.ConvertToDestinationEncoding:
						{
							string newValue3 = text.Replace(match.Value, string.Format(CultureInfo.InvariantCulture, "charset={0}", new object[]
							{
								h.b(A_0.Parser.CharsetConverter.DestinationEncoding)
							}));
							A_0.BodyHtmlText = A_0.BodyHtmlText.Replace(text, newValue3);
							break;
						}
						}
					}
				}
			}
			if (A_1 == CharsetMetaTagProcessing.RemoveCharsetMetaTag)
			{
				foreach (string text2 in bb.a("meta", A_0.BodyHtmlText, true))
				{
					if (m.j.Match(text2).Success)
					{
						A_0.BodyHtmlText = A_0.BodyHtmlText.Replace(text2, string.Empty);
					}
				}
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00041708 File Offset: 0x00040708
		public static void a(HtmlToPlainAutoConvert A_0, HtmlToPlainConvertOptions A_1, MailMessage A_2)
		{
			if ((A_0 == HtmlToPlainAutoConvert.IfNoPlain && A_2.BodyPlainText != null && A_2.BodyPlainText.Length == 0) || (A_0 == HtmlToPlainAutoConvert.IfHtml && A_2.BodyHtmlText != null && A_2.BodyHtmlText.Length != 0))
			{
				A_2.BodyPlainText = global::a.i.b.a(A_2.BodyHtmlText, A_1, false);
			}
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00041759 File Offset: 0x00040759
		public static void a(HtmlToSimpleHtmlAutoConvert A_0, HtmlToSimpleHtmlConvertOptions A_1, MailMessage A_2)
		{
			if (A_0 == HtmlToSimpleHtmlAutoConvert.IfHtml && A_2.BodyHtmlText != null && A_2.BodyHtmlText.Length != 0)
			{
				A_2.BodyHtmlText = global::a.i.b.a(A_2.BodyHtmlText, A_1);
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x00041788 File Offset: 0x00040788
		public static void a(PlainToHtmlAutoConvert A_0, PlainToHtmlConvertOptions A_1, MailMessage A_2, string A_3)
		{
			if (A_0 == PlainToHtmlAutoConvert.IfNoHtml && A_2.BodyHtmlText != null && A_2.BodyHtmlText.Length == 0)
			{
				string text = global::a.i.b.a(A_2.BodyPlainText, A_1);
				if (A_3 != null && A_3.Length != 0)
				{
					text = e.a(text, A_3);
				}
				if (A_2.BodyParts["text/html"] != null)
				{
					A_2.BodyParts["text/html"].Text = text;
				}
				else
				{
					A_2.BodyParts.Add("text/html");
					A_2.BodyParts["text/html"].Text = text;
				}
			}
			if (A_0 == PlainToHtmlAutoConvert.IfPlain && A_2.BodyPlainText != null && A_2.BodyPlainText.Length != 0)
			{
				string text2 = global::a.i.b.a(A_2.BodyPlainText, A_1);
				if (A_3 != null && A_3.Length != 0)
				{
					text2 = e.a(text2, A_3);
				}
				if (A_2.BodyParts["text/html"] != null)
				{
					A_2.BodyParts["text/html"].Text = text2;
					return;
				}
				A_2.BodyParts.Add("text/html");
				A_2.BodyParts["text/html"].Text = text2;
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000418B0 File Offset: 0x000408B0
		public static string a(string A_0, string A_1)
		{
			string text = string.Empty;
			Match match = m.k.Match(A_1);
			if (match.Success)
			{
				text = match.Groups["tagName"].Value;
			}
			if (text != null && text.Length != 0)
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "</{0}>", new object[]
				{
					text
				});
				string[] array = A_0.Split(new char[]
				{
					'\n'
				});
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text3 in array)
				{
					match = m.l.Match(text3);
					if (match.Success)
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}\n{2}", new object[]
						{
							A_1,
							match.Value,
							text2
						});
					}
					else
					{
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}\n", new object[]
						{
							text3
						});
					}
				}
				return stringBuilder.ToString();
			}
			return A_0;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000419B0 File Offset: 0x000409B0
		public static Task<string> a(string A_0, VirtualMappingType A_1, MessageFolderBehavior A_2, MailMessage A_3, MessageParserConfig A_4, bool A_5)
		{
			e.a a;
			a.g = A_0;
			a.e = A_1;
			a.c = A_2;
			a.d = A_3;
			a.h = A_4;
			a.f = A_5;
			a.b = AsyncTaskMethodBuilder<string>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<string> b = a.b;
			b.Start<e.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x00041A20 File Offset: 0x00040A20
		private static Task<string> a(string A_0, string A_1, AttachmentCollection A_2, string A_3, VirtualMappingType A_4, bool A_5, string A_6, MessageParserConfig A_7)
		{
			e.b b;
			b.l = A_0;
			b.d = A_1;
			b.c = A_2;
			b.j = A_3;
			b.i = A_4;
			b.k = A_5;
			b.e = A_6;
			b.f = A_7;
			b.b = AsyncTaskMethodBuilder<string>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<string> b2 = b.b;
			b2.Start<e.b>(ref b);
			return b.b.Task;
		}
	}
}
