using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Words;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Interfaces;

namespace TechnoPro.Common.MergedDocumentText
{
	// Token: 0x02000002 RID: 2
	public class TextMergedDocument : IMergedDocument
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public object Document
		{
			get
			{
				return this._textDocument;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public bool IsLicensed { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public eFileFormat OutputFileFormat
		{
			get
			{
				return this._outputFileFormat;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002071 File Offset: 0x00000271
		public TextMergedDocument()
		{
			TextMergedDocument.RegisterAsposeLicense();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002080 File Offset: 0x00000280
		private static void RegisterAsposeLicense()
		{
			License license = new License();
			using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes("<License>\r\n  <Data>\r\n    <LicensedTo>TechnoPro Computer Solutions</LicensedTo>\r\n    <EmailTo>mike@tpro.ca</EmailTo>\r\n    <LicenseType>Developer OEM</LicenseType>\r\n    <LicenseNote>Limited to 1 developer, unlimited physical locations</LicenseNote>\r\n    <OrderID>190530122340</OrderID>\r\n    <UserID>310030</UserID>\r\n    <OEM>This is a redistributable license</OEM>\r\n    <Products>\r\n      <Product>Aspose.Words for .NET</Product>\r\n    </Products>\r\n    <EditionType>Enterprise</EditionType>\r\n    <SerialNumber>d2cc9eab-516a-49b1-92fe-c32ba0fcafeb</SerialNumber>\r\n    <SubscriptionExpiry>20200530</SubscriptionExpiry>\r\n    <LicenseVersion>3.0</LicenseVersion>\r\n    <LicenseInstructions>https://purchase.aspose.com/policies/use-license</LicenseInstructions>\r\n  </Data>\r\n  <Signature>ag5NOq2e7M0YBSB999ctDhrCAidIIcm1NOFMrrNjghx2PgcVlRbdEc33tUt0bFYXRFMt/buHul3PP1xRXLz2hKiMF/plfwpVceJAg6Nb2L/8wyHURIR9Yr4mVmAWMwO95MAJvtXfBuw95rlXarlK4ux79tmHzU84j0dV5Mc5EKM=</Signature>\r\n</License>")))
			{
				license.SetLicense(memoryStream);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
		public void LoadDocument(byte[] bytes, eFileFormat outputFileFormat)
		{
			this._outputFileFormat = outputFileFormat;
			this.LoadDocument(bytes);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020DC File Offset: 0x000002DC
		public void LoadDocument(byte[] bytes)
		{
			string text = null;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				using (TextReader textReader = new StreamReader(memoryStream))
				{
					text = textReader.ReadToEnd();
				}
			}
			text = text.DecodeHtml();
			this._textDocument = new StringBuilder(text);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public BinaryFile SaveDocument(string fileNameNoExtension)
		{
			Encoding utf = Encoding.UTF8;
			StringBuilder textDocument = this._textDocument;
			byte[] bytes = utf.GetBytes(((textDocument != null) ? textDocument.ToString() : null) ?? "");
			if (this._outputFileFormat == eFileFormat.Text || this._outputFileFormat == eFileFormat.Html || this._outputFileFormat == eFileFormat.Unknown)
			{
				BinaryFile binaryFile = new BinaryFile();
				FileFormatAttribute attribute = this._outputFileFormat.GetAttribute<FileFormatAttribute>();
				binaryFile.FileName = Path.Combine(fileNameNoExtension, ((attribute != null) ? attribute.Extension : null) ?? "");
				binaryFile.ByteArray = bytes;
				return binaryFile;
			}
			return TextMergedDocument.ConvertTextDocumentToOtherDocument(fileNameNoExtension, bytes, this._outputFileFormat);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E1 File Offset: 0x000003E1
		private static SaveFormat GetSaveFormatFromOutputFormat(eFileFormat outputFormat)
		{
			switch (outputFormat)
			{
			case eFileFormat.Word:
				return SaveFormat.Doc;
			case eFileFormat.WordX:
				return SaveFormat.Docx;
			case (eFileFormat)3:
				break;
			case eFileFormat.PDF:
				return SaveFormat.Pdf;
			default:
				if (outputFormat == eFileFormat.Html)
				{
					return SaveFormat.Html;
				}
				if (outputFormat == eFileFormat.Text)
				{
					return SaveFormat.Text;
				}
				break;
			}
			return SaveFormat.Docx;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002218 File Offset: 0x00000418
		private static Document LoadWordDocument(byte[] bytes)
		{
			Document result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = new Document(memoryStream);
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002250 File Offset: 0x00000450
		private static BinaryFile ConvertTextDocumentToOtherDocument(string fileNameNoExtension, byte[] textFileBytes, eFileFormat outputFormat)
		{
			Document document = TextMergedDocument.LoadWordDocument(textFileBytes);
			byte[] byteArray = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				document.Save(memoryStream, TextMergedDocument.GetSaveFormatFromOutputFormat(outputFormat));
				byteArray = memoryStream.ToArray();
				memoryStream.Flush();
				memoryStream.Close();
			}
			BinaryFile binaryFile = new BinaryFile();
			binaryFile.ByteArray = byteArray;
			FileFormatAttribute attribute = outputFormat.GetAttribute<FileFormatAttribute>();
			binaryFile.FileName = fileNameNoExtension + (((attribute != null) ? attribute.Extension : null) ?? "");
			return binaryFile;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022E4 File Offset: 0x000004E4
		public IList<string> ExtractUniqueCodes(byte[] fileBytes)
		{
			this.LoadDocument(fileBytes);
			StringBuilder textDocument = this._textDocument;
			string text = ((textDocument != null) ? textDocument.ToString() : null) ?? "";
			List<string> list = new List<string>();
			if (string.IsNullOrEmpty(text))
			{
				return list;
			}
			MatchCollection source = Regex.Matches(text, "#\\<[^\\>#]+\\>#");
			list.AddRange(from Match match in source
			select match.Value into s
			where s != null && s.Length > 4
			select s.Substring(2, s.Length - 4));
			return list;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023A9 File Offset: 0x000005A9
		public void MergeImageField(MailMergeCode code, string codeName, Image image, byte[] imageBytes)
		{
			this.MergeStringField(code, codeName, "Images are not supported in text/html files.");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023B8 File Offset: 0x000005B8
		public void MergeStringField(MailMergeCode code, string codeName, string codeValue)
		{
			try
			{
				this._textDocument.Replace(codeName, codeValue ?? "");
			}
			catch (Exception)
			{
				TextMergedDocument.ClearMailMergeCodeValues(this._textDocument, codeName);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002400 File Offset: 0x00000600
		private static void ClearMailMergeCodeValues(StringBuilder textDocument, string codeName)
		{
			try
			{
				textDocument.Replace(codeName, "");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002430 File Offset: 0x00000630
		public void MergeBooleanField(MailMergeCode code, string codeName, MailMergeCheckedItem item)
		{
			this.MergeStringField(code, codeName, (item != null && item.IsChecked) ? "True" : "False");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002454 File Offset: 0x00000654
		public void AppendDocument(IMergedDocument documentToAppend)
		{
			this._textDocument.AppendLine("-----------------------------");
			this._textDocument.AppendLine();
			StringBuilder textDocument = this._textDocument;
			StringBuilder stringBuilder = ((documentToAppend != null) ? documentToAppend.Document : null) as StringBuilder;
			textDocument.Append(((stringBuilder != null) ? stringBuilder.ToString() : null) ?? "");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024B0 File Offset: 0x000006B0
		public void MergeDocument(MailMergeCode code, string codeName, IMergedDocument documentToMergeIn)
		{
			try
			{
				this.MergeStringField(code, codeName, ((StringBuilder)documentToMergeIn.Document).ToString());
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000001 RID: 1
		private StringBuilder _textDocument;

		// Token: 0x04000002 RID: 2
		private eFileFormat _outputFileFormat;
	}
}
