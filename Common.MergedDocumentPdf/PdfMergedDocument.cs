using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Interfaces;

namespace TechnoPro.Common.MergedDocumentPdf
{
	// Token: 0x02000002 RID: 2
	public class PdfMergedDocument : IMergedDocument
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public PdfMergedDocument()
		{
			PdfMergedDocument.RegisterAsposeLicense();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205D File Offset: 0x0000025D
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002065 File Offset: 0x00000265
		public bool IsLicensed { get; set; }

		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		private static void RegisterAsposeLicense()
		{
			License license = new License();
			using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes("<License>\r\n  <Data>\r\n    <LicensedTo>TechnoPro Computer Solutions</LicensedTo>\r\n    <EmailTo>mike@clockworks.ca</EmailTo>\r\n    <LicenseType>Developer OEM</LicenseType>\r\n    <LicenseNote>Limited to 1 developer, unlimited physical locations</LicenseNote>\r\n    <OrderID>190530114858</OrderID>\r\n    <UserID>310030</UserID>\r\n    <OEM>This is a redistributable license</OEM>\r\n    <Products>\r\n      <Product>Aspose.Pdf for .NET</Product>\r\n    </Products>\r\n    <EditionType>Enterprise</EditionType>\r\n    <SerialNumber>b5c58e7d-1c68-4812-b19a-73dbc0c0a028</SerialNumber>\r\n    <SubscriptionExpiry>20200530</SubscriptionExpiry>\r\n    <LicenseVersion>3.0</LicenseVersion>\r\n    <LicenseInstructions>https://purchase.aspose.com/policies/use-license</LicenseInstructions>\r\n  </Data>\r\n  <Signature>uS9s7V9RScPieY/E31ycX3cnbEDN6F7fubbP1Z3a5sOYGG+qFr7Qk6FJl74KSb45yppNs9hpih2hGdLwRorxfKIpgIxaFxXfgvUv7ZvJX/FzZC+SLR5qRDVXaA/BNN+5FUWK/o7BXnSzs/A992GlvnURRkiGjWeDp2vjNAbA8pg=</Signature>\r\n</License>")))
			{
				license.SetLicense(memoryStream);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020BC File Offset: 0x000002BC
		public object Document
		{
			get
			{
				return this._pdfForm;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020C4 File Offset: 0x000002C4
		public eFileFormat OutputFileFormat
		{
			get
			{
				return this._outputFileFormat;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020CC File Offset: 0x000002CC
		public void LoadDocument(byte[] bytes)
		{
			this.LoadDocument(bytes, eFileFormat.PDF);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D8 File Offset: 0x000002D8
		public void LoadDocument(byte[] bytes, eFileFormat outputFileFormat)
		{
			this._outputFileFormat = outputFileFormat;
			if (bytes == null)
			{
				return;
			}
			MemoryStream srcStream = new MemoryStream(bytes);
			this._pdfForm = new Form();
			this._pdfForm.BindPdf(srcStream);
			if (!this.IsLicensed)
			{
				TextStamp textStamp = new TextStamp("PDF-Template License Required")
				{
					Background = true,
					XIndent = 100.0,
					YIndent = 100.0,
					RotateAngle = 45.0
				};
				textStamp.TextState.FontSize = 24f;
				foreach (Page page in this._pdfForm.Document.Pages)
				{
					page.AddStamp(textStamp);
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B0 File Offset: 0x000003B0
		public BinaryFile SaveDocument(string fileNameNoExtension)
		{
			eFileFormat outputFileFormat = this.OutputFileFormat;
			if (outputFileFormat <= eFileFormat.WordX)
			{
				if (outputFileFormat == eFileFormat.Word)
				{
					return PdfMergedDocument.SaveDocument(this._pdfForm, fileNameNoExtension, new DocSaveOptions.DocFormat?(DocSaveOptions.DocFormat.Doc), this._outputFileFormat);
				}
				if (outputFileFormat == eFileFormat.WordX)
				{
					return PdfMergedDocument.SaveDocument(this._pdfForm, fileNameNoExtension, new DocSaveOptions.DocFormat?(DocSaveOptions.DocFormat.DocX), this._outputFileFormat);
				}
			}
			else
			{
				if (outputFileFormat == eFileFormat.Html)
				{
					return PdfMergedDocument.SaveDocumentAsHtml(this._pdfForm, fileNameNoExtension, this._outputFileFormat);
				}
				if (outputFileFormat == eFileFormat.Text)
				{
					return PdfMergedDocument.SaveDocumentAsText(this._pdfForm, fileNameNoExtension, this._outputFileFormat);
				}
			}
			return PdfMergedDocument.SaveDocument(this._pdfForm, fileNameNoExtension, null, this._outputFileFormat);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002250 File Offset: 0x00000450
		private static BinaryFile SaveDocumentAsText(Form pdfForm, string fileNameNoExtension, eFileFormat outputFormat)
		{
			Document document = pdfForm.Document;
			TextAbsorber textAbsorber = new TextAbsorber();
			document.Pages.Accept(textAbsorber);
			string text = textAbsorber.Text;
			return new BinaryFile
			{
				ByteArray = Encoding.ASCII.GetBytes(text ?? ""),
				FileName = PdfMergedDocument.GetFilename(fileNameNoExtension, outputFormat)
			};
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022A8 File Offset: 0x000004A8
		private static BinaryFile SaveDocumentAsHtml(Form pdfForm, string fileNameNoExtension, eFileFormat outputFormat)
		{
			HtmlSaveOptions options = new HtmlSaveOptions
			{
				PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
				LettersPositioningMethod = HtmlSaveOptions.LettersPositioningMethods.UseEmUnitsAndCompensationOfRoundingErrorsInCss,
				RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground,
				FontSavingMode = HtmlSaveOptions.FontSavingModes.SaveInAllFormats
			};
			Document document = pdfForm.Document;
			MemoryStream memoryStream = new MemoryStream();
			document.Save(memoryStream, options);
			memoryStream.Flush();
			memoryStream.Close();
			return new BinaryFile
			{
				ByteArray = memoryStream.ToArray(),
				FileName = PdfMergedDocument.GetFilename(fileNameNoExtension, outputFormat)
			};
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002314 File Offset: 0x00000514
		private static BinaryFile SaveDocument(Form pdfForm, string fileNameNoExtension, DocSaveOptions.DocFormat? format, eFileFormat outputFormat)
		{
			MemoryStream memoryStream = new MemoryStream();
			if (format != null)
			{
				DocSaveOptions options = new DocSaveOptions
				{
					Format = format.Value
				};
				pdfForm.Document.Save(memoryStream, options);
			}
			else
			{
				pdfForm.Document.Save(memoryStream);
			}
			memoryStream.Flush();
			memoryStream.Close();
			return new BinaryFile
			{
				ByteArray = memoryStream.ToArray(),
				FileName = PdfMergedDocument.GetFilename(fileNameNoExtension, outputFormat)
			};
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002388 File Offset: 0x00000588
		private static string GetFilename(string fileNameNoExtension, eFileFormat outputFormat)
		{
			FileFormatAttribute attribute = outputFormat.GetAttribute<FileFormatAttribute>();
			return fileNameNoExtension + (((attribute != null) ? attribute.Extension : null) ?? "");
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023B0 File Offset: 0x000005B0
		private static DocSaveOptions.DocFormat? GetSaveFormat(eFileFormat outputFormat)
		{
			switch (outputFormat)
			{
			case eFileFormat.Word:
				return new DocSaveOptions.DocFormat?(DocSaveOptions.DocFormat.Doc);
			case eFileFormat.WordX:
				return new DocSaveOptions.DocFormat?(DocSaveOptions.DocFormat.DocX);
			case eFileFormat.PDF:
				return null;
			}
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000023F8 File Offset: 0x000005F8
		public IList<string> ExtractUniqueCodes(byte[] fileBytes)
		{
			this.LoadDocument(fileBytes);
			Form pdfForm = this._pdfForm;
			if (pdfForm == null)
			{
				return null;
			}
			return pdfForm.FieldNames;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002414 File Offset: 0x00000614
		public void MergeImageField(MailMergeCode code, string codeName0, System.Drawing.Image image, byte[] imageBytes)
		{
			string fieldName = PdfMergedDocument.StripCodeTags(codeName0);
			if (image != null)
			{
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						image.Save(memoryStream, image.RawFormat);
						this._pdfForm.FillImageField(fieldName, memoryStream);
					}
					return;
				}
				catch (Exception)
				{
				}
			}
			if (imageBytes == null || imageBytes.Length < 1)
			{
				return;
			}
			try
			{
				using (MemoryStream memoryStream2 = new MemoryStream(imageBytes))
				{
					this._pdfForm.FillImageField(fieldName, memoryStream2);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024C4 File Offset: 0x000006C4
		private static void ClearMailMergeCodeValues(Form pdfForm, string codeName0)
		{
			string fieldName = PdfMergedDocument.StripCodeTags(codeName0);
			try
			{
				pdfForm.FillField(fieldName, "");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024FC File Offset: 0x000006FC
		public void MergeStringField(MailMergeCode code, string codeName0, string codeValue)
		{
			if (string.IsNullOrWhiteSpace(codeValue))
			{
				return;
			}
			string text = PdfMergedDocument.StripCodeTags(codeName0);
			try
			{
				this._pdfForm.FillField(text, codeValue);
			}
			catch (Exception)
			{
				PdfMergedDocument.ClearMailMergeCodeValues(this._pdfForm, text);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002548 File Offset: 0x00000748
		public void MergeBooleanField(MailMergeCode code, string codeName0, MailMergeCheckedItem item)
		{
			string fieldName = PdfMergedDocument.StripCodeTags(codeName0);
			try
			{
				if (this._pdfForm.GetFieldType(fieldName) == FieldType.CheckBox)
				{
					this._pdfForm.FillField(fieldName, item.IsChecked);
				}
				else
				{
					this._pdfForm.FillField(fieldName, item.IsChecked ? "True" : "False");
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000025B8 File Offset: 0x000007B8
		public void AppendDocument(IMergedDocument documentToAppend)
		{
			Form form = documentToAppend.Document as Form;
			if (form == null)
			{
				return;
			}
			this._pdfForm.Document.Pages.Add(form.Document.Pages);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025F8 File Offset: 0x000007F8
		private static string StripCodeTags(string code)
		{
			if (code == null)
			{
				return "";
			}
			if (!code.StartsWith("#<") || !code.EndsWith(">#"))
			{
				return code;
			}
			if (code.Length <= 4)
			{
				return "";
			}
			return code.Substring(2, code.Length - 4);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002648 File Offset: 0x00000848
		public void MergeDocument(MailMergeCode code, string codeName0, IMergedDocument documentToMergeIn)
		{
			string codeName = PdfMergedDocument.StripCodeTags(codeName0);
			try
			{
				this.MergeStringField(code, codeName, "Merge document not supported in pdf files.");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04000002 RID: 2
		private Form _pdfForm;

		// Token: 0x04000003 RID: 3
		private eFileFormat _outputFileFormat;
	}
}
