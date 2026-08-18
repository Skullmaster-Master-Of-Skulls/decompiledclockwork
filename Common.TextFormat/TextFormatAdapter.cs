using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Aspose.Words;
using Aspose.Words.Markup;
using Aspose.Words.Replacing;
using Aspose.Words.Saving;
using HtmlAgilityPack;

namespace TechnoPro.Common.TextFormat.Adapters
{
	// Token: 0x02000004 RID: 4
	public static class TextFormatAdapter
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002428 File Offset: 0x00000628
		private static void RegisterAsposeLicense()
		{
			bool registeredAsposeLicense = TextFormatAdapter._registeredAsposeLicense;
			if (!registeredAsposeLicense)
			{
				try
				{
					License license = new License();
					using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes("<License>\r\n  <Data>\r\n    <LicensedTo>TechnoPro Computer Solutions</LicensedTo>\r\n    <EmailTo>mike@tpro.ca</EmailTo>\r\n    <LicenseType>Developer OEM</LicenseType>\r\n    <LicenseNote>Limited to 1 developer, unlimited physical locations</LicenseNote>\r\n    <OrderID>190530122340</OrderID>\r\n    <UserID>310030</UserID>\r\n    <OEM>This is a redistributable license</OEM>\r\n    <Products>\r\n      <Product>Aspose.Words for .NET</Product>\r\n    </Products>\r\n    <EditionType>Enterprise</EditionType>\r\n    <SerialNumber>d2cc9eab-516a-49b1-92fe-c32ba0fcafeb</SerialNumber>\r\n    <SubscriptionExpiry>20200530</SubscriptionExpiry>\r\n    <LicenseVersion>3.0</LicenseVersion>\r\n    <LicenseInstructions>https://purchase.aspose.com/policies/use-license</LicenseInstructions>\r\n  </Data>\r\n  <Signature>ag5NOq2e7M0YBSB999ctDhrCAidIIcm1NOFMrrNjghx2PgcVlRbdEc33tUt0bFYXRFMt/buHul3PP1xRXLz2hKiMF/plfwpVceJAg6Nb2L/8wyHURIR9Yr4mVmAWMwO95MAJvtXfBuw95rlXarlK4ux79tmHzU84j0dV5Mc5EKM=</Signature>\r\n</License>")))
					{
						license.SetLicense(memoryStream);
						TextFormatAdapter._registeredAsposeLicense = true;
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024A0 File Offset: 0x000006A0
		public static string AddEmbeddedImagesToRtf(this string rtf, IDictionary<string, byte[]> attachments)
		{
			bool flag = attachments == null || attachments.Count < 1 || string.IsNullOrWhiteSpace(rtf) || !rtf.StartsWith("{\\rtf1\\", StringComparison.OrdinalIgnoreCase);
			string result;
			if (flag)
			{
				result = rtf;
			}
			else
			{
				try
				{
					bool flag2 = !TextFormatAdapter._registeredAsposeLicense;
					if (flag2)
					{
						TextFormatAdapter.RegisterAsposeLicense();
					}
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					byte[] bytes = asciiencoding.GetBytes(rtf);
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						LoadOptions loadOptions = new LoadOptions
						{
							LoadFormat = LoadFormat.Rtf,
							PreserveIncludePictureField = true
						};
						Document document = new Document(memoryStream, loadOptions);
						FindReplaceOptions options = new FindReplaceOptions(new TextFormatAdapter.ReplaceImagesInRtfReplaceHandler(attachments))
						{
							Direction = FindReplaceDirection.Backward,
							MatchCase = false
						};
						document.Range.Replace(new Regex("<img.*?>"), "", options);
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							RtfSaveOptions saveOptions = new RtfSaveOptions
							{
								SaveFormat = SaveFormat.Rtf
							};
							document.Save(memoryStream2, saveOptions);
							result = Encoding.UTF8.GetString(memoryStream2.ToArray());
						}
					}
				}
				catch (Exception ex)
				{
					result = rtf;
				}
			}
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025EC File Offset: 0x000007EC
		public static string ConvertHtmlToRtf(this string Html)
		{
			bool flag = string.IsNullOrWhiteSpace(Html);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				try
				{
					TextFormatAdapter.RegisterAsposeLicense();
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					byte[] bytes = asciiencoding.GetBytes(Html);
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						LoadOptions loadOptions = new LoadOptions
						{
							LoadFormat = LoadFormat.Html
						};
						Document document = new Document(memoryStream, loadOptions);
						ArrayList arrayList = new ArrayList();
						foreach (StructuredDocumentTag structuredDocumentTag in document.GetChildNodes(NodeType.StructuredDocumentTag, true).ToArray())
						{
							arrayList.Add(structuredDocumentTag);
							StructuredDocumentTag structuredDocumentTag2 = new StructuredDocumentTag(document, SdtType.RichText, structuredDocumentTag.Level);
							structuredDocumentTag2.RemoveAllChildren();
							foreach (Node node in structuredDocumentTag.ChildNodes)
							{
								structuredDocumentTag2.AppendChild(node.Clone(true));
							}
							structuredDocumentTag.ParentNode.InsertBefore(structuredDocumentTag2, structuredDocumentTag);
						}
						foreach (object obj in arrayList)
						{
							Node node2 = (Node)obj;
							node2.Remove();
						}
						RtfSaveOptions saveOptions = new RtfSaveOptions
						{
							SaveFormat = SaveFormat.Rtf
						};
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							document.Save(memoryStream2, saveOptions);
							result = Encoding.UTF8.GetString(memoryStream2.ToArray());
						}
					}
				}
				catch (Exception ex)
				{
					result = Html;
				}
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000281C File Offset: 0x00000A1C
		public static byte[] ConvertHtmlToPdf(this string Html)
		{
			string text = (Html ?? "").Trim();
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			byte[] bytes = asciiencoding.GetBytes(text);
			bool flag = text.Length < 1;
			byte[] result;
			if (flag)
			{
				result = bytes;
			}
			else
			{
				try
				{
					TextFormatAdapter.RegisterAsposeLicense();
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						LoadOptions loadOptions = new LoadOptions
						{
							LoadFormat = LoadFormat.Html
						};
						Document document = new Document(memoryStream, loadOptions);
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							document.Save(memoryStream2, SaveFormat.Pdf);
							result = memoryStream2.ToArray();
						}
					}
				}
				catch
				{
					result = bytes;
				}
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000028F0 File Offset: 0x00000AF0
		public static byte[] ConvertRtfToPdf(this string Rtf)
		{
			string text = (Rtf ?? "").Trim();
			ASCIIEncoding asciiencoding = new ASCIIEncoding();
			byte[] bytes = asciiencoding.GetBytes(text);
			bool flag = text.Length < 1;
			byte[] result;
			if (flag)
			{
				result = bytes;
			}
			else
			{
				try
				{
					TextFormatAdapter.RegisterAsposeLicense();
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						LoadOptions loadOptions = new LoadOptions
						{
							LoadFormat = LoadFormat.Rtf
						};
						Document document = new Document(memoryStream, loadOptions);
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							document.Save(memoryStream2, SaveFormat.Pdf);
							result = memoryStream2.ToArray();
						}
					}
				}
				catch
				{
					result = bytes;
				}
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000029C4 File Offset: 0x00000BC4
		public static string ConvertPlainTextToHtmlBodyInnerHtml(this string PlainText)
		{
			string text = PlainText.ConvertPlainTextToHtml();
			bool flag = string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = text.GetBodyInnerHtml();
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029F4 File Offset: 0x00000BF4
		private static string GetBodyInnerHtml(this string html)
		{
			try
			{
				int num = html.IndexOf("<body", StringComparison.OrdinalIgnoreCase);
				bool flag = num >= 0;
				if (flag)
				{
					int num2 = html.IndexOf("</body", num + 1, StringComparison.OrdinalIgnoreCase);
					bool flag2 = num2 > 0;
					if (flag2)
					{
						int num3 = html.IndexOf(">", num + 1);
						bool flag3 = num3 > num;
						if (flag3)
						{
							num3++;
							return html.Substring(num3, num2 - num3);
						}
					}
				}
				return html;
			}
			catch (Exception ex)
			{
			}
			return html;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static string ConvertRtfToHtmlBodyInnerHtml(this string Rtf)
		{
			string result;
			try
			{
				string text = Rtf.ConvertRtfToHtml();
				bool flag = string.IsNullOrEmpty(text);
				if (flag)
				{
					result = text;
				}
				else
				{
					string text2 = text.GetBodyInnerHtml();
					text2 = text2.Replace("font-family:'Symbol'", "");
					result = text2;
				}
			}
			catch
			{
				result = new RichTextBox
				{
					Rtf = Rtf
				}.Text;
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002AFC File Offset: 0x00000CFC
		public static string ConvertHtmlToPlainText(this string Html)
		{
			bool flag = string.IsNullOrWhiteSpace(Html);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				try
				{
					TextFormatAdapter.RegisterAsposeLicense();
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					byte[] bytes = asciiencoding.GetBytes(Html);
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						LoadOptions loadOptions = new LoadOptions
						{
							LoadFormat = LoadFormat.Html
						};
						Document document = new Document(memoryStream, loadOptions);
						ArrayList arrayList = new ArrayList();
						foreach (StructuredDocumentTag structuredDocumentTag in document.GetChildNodes(NodeType.StructuredDocumentTag, true).ToArray())
						{
							arrayList.Add(structuredDocumentTag);
							StructuredDocumentTag structuredDocumentTag2 = new StructuredDocumentTag(document, SdtType.PlainText, structuredDocumentTag.Level);
							structuredDocumentTag2.RemoveAllChildren();
							foreach (Node node in structuredDocumentTag.ChildNodes)
							{
								structuredDocumentTag2.AppendChild(node.Clone(true));
							}
							structuredDocumentTag.ParentNode.InsertBefore(structuredDocumentTag2, structuredDocumentTag);
						}
						foreach (object obj in arrayList)
						{
							Node node2 = (Node)obj;
							node2.Remove();
						}
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							document.Save(memoryStream2, SaveFormat.Text);
							result = Encoding.UTF8.GetString(memoryStream2.ToArray());
						}
					}
				}
				catch (Exception ex)
				{
					result = Html;
				}
			}
			return result;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002D1C File Offset: 0x00000F1C
		public static string ConvertPlainTextToRtf(this string PlainText)
		{
			bool flag = string.IsNullOrWhiteSpace(PlainText);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				try
				{
					bool flag2 = !TextFormatAdapter._registeredAsposeLicense;
					if (flag2)
					{
						TextFormatAdapter.RegisterAsposeLicense();
					}
					ASCIIEncoding asciiencoding = new ASCIIEncoding();
					byte[] bytes = asciiencoding.GetBytes(PlainText);
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						LoadOptions loadOptions = new LoadOptions
						{
							LoadFormat = LoadFormat.Text,
							PreserveIncludePictureField = true
						};
						Document document = new Document(memoryStream, loadOptions);
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							document.Save(memoryStream2, SaveFormat.Rtf);
							result = Encoding.UTF8.GetString(memoryStream2.ToArray());
						}
					}
				}
				catch (Exception ex)
				{
					result = PlainText;
				}
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002E04 File Offset: 0x00001004
		public static string ConvertRtfToHtml(this string Rtf)
		{
			bool flag = string.IsNullOrEmpty(Rtf);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = !Rtf.StartsWith("{\\rtf1\\", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					result = Rtf;
				}
				else
				{
					try
					{
						bool flag3 = !TextFormatAdapter._registeredAsposeLicense;
						if (flag3)
						{
							TextFormatAdapter.RegisterAsposeLicense();
						}
						ASCIIEncoding asciiencoding = new ASCIIEncoding();
						byte[] bytes = asciiencoding.GetBytes(Rtf);
						using (MemoryStream memoryStream = new MemoryStream(bytes))
						{
							LoadOptions loadOptions = new LoadOptions
							{
								LoadFormat = LoadFormat.Rtf,
								PreserveIncludePictureField = true
							};
							Document document = new Document(memoryStream, loadOptions);
							using (MemoryStream memoryStream2 = new MemoryStream())
							{
								HtmlSaveOptions saveOptions = new HtmlSaveOptions
								{
									ExportImagesAsBase64 = true,
									CssStyleSheetType = CssStyleSheetType.Embedded,
									SaveFormat = SaveFormat.Html
								};
								document.Save(memoryStream2, saveOptions);
								string @string = Encoding.UTF8.GetString(memoryStream2.ToArray());
								HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
								htmlDocument.LoadHtml(@string);
								HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("/html/body");
								result = htmlNode.InnerHtml;
							}
						}
					}
					catch (Exception ex)
					{
						result = Rtf;
					}
				}
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002F54 File Offset: 0x00001154
		public static string ConvertRtfToPlainText(this string Rtf)
		{
			bool flag = string.IsNullOrEmpty(Rtf);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = !Rtf.StartsWith("{\\rtf1\\", StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					result = Rtf;
				}
				else
				{
					try
					{
						bool flag3 = !TextFormatAdapter._registeredAsposeLicense;
						if (flag3)
						{
							TextFormatAdapter.RegisterAsposeLicense();
						}
						ASCIIEncoding asciiencoding = new ASCIIEncoding();
						byte[] bytes = asciiencoding.GetBytes(Rtf);
						using (MemoryStream memoryStream = new MemoryStream(bytes))
						{
							LoadOptions loadOptions = new LoadOptions
							{
								LoadFormat = LoadFormat.Rtf
							};
							Document document = new Document(memoryStream, loadOptions);
							ArrayList arrayList = new ArrayList();
							foreach (StructuredDocumentTag structuredDocumentTag in document.GetChildNodes(NodeType.StructuredDocumentTag, true).ToArray())
							{
								arrayList.Add(structuredDocumentTag);
								StructuredDocumentTag structuredDocumentTag2 = new StructuredDocumentTag(document, SdtType.PlainText, structuredDocumentTag.Level);
								structuredDocumentTag2.RemoveAllChildren();
								foreach (Node node in structuredDocumentTag.ChildNodes)
								{
									structuredDocumentTag2.AppendChild(node.Clone(true));
								}
								structuredDocumentTag.ParentNode.InsertBefore(structuredDocumentTag2, structuredDocumentTag);
							}
							foreach (object obj in arrayList)
							{
								Node node2 = (Node)obj;
								node2.Remove();
							}
							using (MemoryStream memoryStream2 = new MemoryStream())
							{
								document.Save(memoryStream2, SaveFormat.Text);
								result = Encoding.UTF8.GetString(memoryStream2.ToArray());
							}
						}
					}
					catch (Exception ex)
					{
						result = Rtf;
					}
				}
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000031A0 File Offset: 0x000013A0
		public static string ConvertPlainTextToHtml(this string PlainText)
		{
			TextFormatAdapter.RegisterAsposeLicense();
			string text = (PlainText ?? "").Trim();
			string result;
			try
			{
				Document document = new Document();
				DocumentBuilder documentBuilder = new DocumentBuilder(document);
				documentBuilder.Write(text);
				byte[] bytes = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					document.Save(memoryStream, SaveFormat.Html);
					bytes = memoryStream.ToArray();
					memoryStream.Flush();
					memoryStream.Close();
				}
				string @string = Encoding.Default.GetString(bytes);
				HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
				htmlDocument.LoadHtml(@string);
				HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("/html/body");
				result = htmlNode.InnerHtml;
			}
			catch
			{
				result = text;
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003278 File Offset: 0x00001478
		public static string ConvertEmailToPlainText(this EmailMessage Email, Dictionary<string, int> AttachmentFileIds)
		{
			bool flag = Email == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				string format = "From: {0}\r\nTo: {1}\r\nCc: {2}\r\nBcc: {3}\r\nAttachments: {4}\r\nSubject: {5}\r\nBody:\r\n{6}";
				result = string.Format(format, new object[]
				{
					Email.From ?? "",
					Email.To ?? "",
					Email.Cc ?? "-",
					Email.Bcc ?? "-",
					(Email.Attachments == null || Email.Attachments.Count < 1) ? "-" : string.Join(", ", Email.Attachments.ToArray<string>()),
					Email.Subject ?? "",
					Email.Body ?? ""
				});
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003350 File Offset: 0x00001550
		private static string DecodeHtml(this string htmlEncoded)
		{
			bool flag = string.IsNullOrWhiteSpace(htmlEncoded);
			string result;
			if (flag)
			{
				result = htmlEncoded;
			}
			else
			{
				try
				{
					result = WebUtility.HtmlDecode(htmlEncoded);
				}
				catch
				{
					result = htmlEncoded;
				}
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003390 File Offset: 0x00001590
		public static string ConvertEmailToRtf(this EmailMessage Email, Dictionary<string, int> AttachmentFileIds)
		{
			TextFormatAdapter.RegisterAsposeLicense();
			EmailMessage emailMessage = Email ?? new EmailMessage();
			string text = (emailMessage.From ?? "").Trim();
			string str = (emailMessage.To ?? "").Trim();
			string text2 = (emailMessage.Cc ?? "").Trim();
			string text3 = (emailMessage.Bcc ?? "").Trim();
			var list = (from g in emailMessage.Attachments ?? new string[0]
			select (g ?? "").Trim() into h
			where h.Length > 0
			select h into m
			select new
			{
				Filename = m,
				FileId = (AttachmentFileIds.ContainsKey(m) ? AttachmentFileIds[m] : 0)
			}).ToList();
			string str2 = (emailMessage.Subject ?? "").Trim();
			string text4 = (emailMessage.BodyHtml ?? "").Trim().DecodeHtml();
			string text5 = (emailMessage.Body ?? "").Trim();
			string result;
			try
			{
				Document document = new Document();
				DocumentBuilder documentBuilder = new DocumentBuilder(document);
				string str3 = str + ((text.Length > 0) ? (" (from " + text + ")") : "");
				documentBuilder.InsertHtml("<div><b>To:</b> " + str3 + "</div>");
				bool flag = text2.Length > 0;
				if (flag)
				{
					documentBuilder.InsertHtml("<div><b>Cc:</b> " + text2 + "</div>");
				}
				bool flag2 = text3.Length > 0;
				if (flag2)
				{
					documentBuilder.InsertHtml("<div><b>Bcc:</b> " + text2 + "</div>");
				}
				bool flag3 = list.Count == 1;
				if (flag3)
				{
					documentBuilder.InsertHtml("<div><b>Attachment(s):</b> " + TextFormatAdapter.GetFileAttachmentLink(list[0].Filename, list[0].FileId) + "</div>");
				}
				else
				{
					bool flag4 = list.Count > 1;
					if (flag4)
					{
						documentBuilder.InsertHtml("<div><b>Attachments:</b> <div><ul>" + string.Join("", (from g in list
						select "<li>" + TextFormatAdapter.GetFileAttachmentLink(g.Filename, g.FileId) + "</li>").ToArray<string>()) + "</ul></div></div>");
					}
				}
				documentBuilder.InsertHtml("<div><b>Subject:</b> " + str2 + "</div>");
				bool flag5 = emailMessage.BodyType == 2 && text4.Length > 0;
				if (flag5)
				{
					documentBuilder.InsertHtml("<div><b>Body:</b> " + text4 + "</div>");
				}
				else
				{
					bool flag6 = emailMessage.BodyType == 2 && text5.Length > 0;
					if (flag6)
					{
						documentBuilder.InsertHtml("<div><b>Body:</b> " + text5 + "</div>");
					}
					else
					{
						documentBuilder.InsertHtml("<div><b>Body:</b> " + text5 + "</div>");
					}
				}
				byte[] bytes = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					document.Save(memoryStream, SaveFormat.Rtf);
					bytes = memoryStream.ToArray();
					memoryStream.Flush();
					memoryStream.Close();
				}
				result = Encoding.Default.GetString(bytes);
			}
			catch
			{
				result = Email.ConvertEmailToPlainText(AttachmentFileIds);
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003750 File Offset: 0x00001950
		private static string GetFileAttachmentLink(string filename, int fileId)
		{
			return (fileId < 1) ? filename : string.Format("<a href='http://click_here_to_open_doc#{0}'>File: {1}</a>", fileId, filename);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000377C File Offset: 0x0000197C
		public static string FileSizeDisplayString(this long sizeInBytes)
		{
			float num = 1024f;
			long num2 = sizeInBytes;
			float num3 = (float)num2 / num;
			bool flag = num3 < 1f;
			string result;
			if (flag)
			{
				result = num2.ToString() + " bytes";
			}
			else
			{
				float num4 = num3 / num;
				bool flag2 = num4 < 1f;
				if (flag2)
				{
					result = num3.ToString("F") + " KB";
				}
				else
				{
					float num5 = num4 / num;
					bool flag3 = num5 < 1f;
					if (flag3)
					{
						result = num4.ToString("F") + " MB";
					}
					else
					{
						float num6 = num5 / num;
						bool flag4 = num6 < 1f;
						if (flag4)
						{
							result = num5.ToString("F") + " GB";
						}
						else
						{
							result = num6.ToString("F") + " TB";
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003864 File Offset: 0x00001A64
		public static string FileSizeDisplayString(this int sizeInBytes)
		{
			return ((long)sizeInBytes).FileSizeDisplayString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003880 File Offset: 0x00001A80
		public static string HtmlToXHtml(this string html)
		{
			return html.Replace("<br>", "<br/>").Replace("<BR>", "<br/>");
		}

		// Token: 0x04000003 RID: 3
		private static bool _registeredAsposeLicense;

		// Token: 0x02000006 RID: 6
		internal class ReplaceImagesInRtfReplaceHandler : IReplacingCallback
		{
			// Token: 0x06000032 RID: 50 RVA: 0x00003953 File Offset: 0x00001B53
			public ReplaceImagesInRtfReplaceHandler()
			{
				this._attachmentsUsed = new List<string>();
				this._attachments = new Dictionary<string, byte[]>();
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00003973 File Offset: 0x00001B73
			public ReplaceImagesInRtfReplaceHandler(IDictionary<string, byte[]> attachments)
			{
				this._attachments = attachments;
				this._attachmentsUsed = new List<string>();
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000034 RID: 52 RVA: 0x0000398F File Offset: 0x00001B8F
			public IList<string> AttachmentsUsed
			{
				get
				{
					return this._attachmentsUsed;
				}
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00003998 File Offset: 0x00001B98
			ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
			{
				Regex regex = new Regex("src=\"cid:\\S * \"");
				Match match = regex.Match(e.Match.Value);
				bool flag = !match.Success || string.IsNullOrWhiteSpace(match.Value) || match.Value.Length < 12;
				ReplaceAction result;
				if (flag)
				{
					result = ReplaceAction.Skip;
				}
				else
				{
					string text = match.Value.Substring(9, match.Value.Length - 10).Trim();
					bool flag2 = !this._attachments.ContainsKey(text);
					if (flag2)
					{
						result = ReplaceAction.Skip;
					}
					else
					{
						this._attachmentsUsed.Add(text);
						e.Replacement = "aaaaaaaaaaaaaa";
						result = ReplaceAction.Replace;
					}
				}
				return result;
			}

			// Token: 0x0400000D RID: 13
			private IDictionary<string, byte[]> _attachments;

			// Token: 0x0400000E RID: 14
			private IList<string> _attachmentsUsed;
		}
	}
}
