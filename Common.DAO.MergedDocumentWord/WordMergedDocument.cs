using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Aspose.Words;
using Aspose.Words.Replacing;
using ClockWorkLogger;
using TechnoPro.Common.DAO.MailMerging;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.DAO.MergedDocumentWord
{
	// Token: 0x02000002 RID: 2
	public class WordMergedDocument : IMergedDocument
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public WordMergedDocument()
		{
			WordMergedDocument.RegisterAsposeLicense();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		private static void RegisterAsposeLicense()
		{
			License license = new License();
			using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes("<License>\r\n  <Data>\r\n    <LicensedTo>TechnoPro Computer Solutions</LicensedTo>\r\n    <EmailTo>mike@tpro.ca</EmailTo>\r\n    <LicenseType>Developer OEM</LicenseType>\r\n    <LicenseNote>Limited to 1 developer, unlimited physical locations</LicenseNote>\r\n    <OrderID>190530122340</OrderID>\r\n    <UserID>310030</UserID>\r\n    <OEM>This is a redistributable license</OEM>\r\n    <Products>\r\n      <Product>Aspose.Words for .NET</Product>\r\n    </Products>\r\n    <EditionType>Enterprise</EditionType>\r\n    <SerialNumber>d2cc9eab-516a-49b1-92fe-c32ba0fcafeb</SerialNumber>\r\n    <SubscriptionExpiry>20200530</SubscriptionExpiry>\r\n    <LicenseVersion>3.0</LicenseVersion>\r\n    <LicenseInstructions>https://purchase.aspose.com/policies/use-license</LicenseInstructions>\r\n  </Data>\r\n  <Signature>ag5NOq2e7M0YBSB999ctDhrCAidIIcm1NOFMrrNjghx2PgcVlRbdEc33tUt0bFYXRFMt/buHul3PP1xRXLz2hKiMF/plfwpVceJAg6Nb2L/8wyHURIR9Yr4mVmAWMwO95MAJvtXfBuw95rlXarlK4ux79tmHzU84j0dV5Mc5EKM=</Signature>\r\n</License>")))
			{
				license.SetLicense(memoryStream);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020B0 File Offset: 0x000002B0
		public object Document
		{
			get
			{
				return this._document;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020B8 File Offset: 0x000002B8
		public eFileFormat OutputFileFormat
		{
			get
			{
				return this._outputFileFormat;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C0 File Offset: 0x000002C0
		public void LoadDocument(byte[] bytes, eFileFormat outputFileFormat)
		{
			this._outputFileFormat = outputFileFormat;
			this._document = WordMergedDocument.LoadDocument(bytes);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		private static Document LoadDocument(byte[] bytes)
		{
			Document result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = new Document(memoryStream);
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002114 File Offset: 0x00000314
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

		// Token: 0x06000008 RID: 8 RVA: 0x0000216C File Offset: 0x0000036C
		public BinaryFile SaveDocument(string fileNameNoExtension)
		{
			byte[] byteArray = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this._document.Save(memoryStream, WordMergedDocument.GetSaveFormatFromOutputFormat(this._outputFileFormat));
				byteArray = memoryStream.ToArray();
				memoryStream.Flush();
				memoryStream.Close();
			}
			BinaryFile binaryFile = new BinaryFile();
			binaryFile.ByteArray = byteArray;
			FileFormatAttribute attribute = this._outputFileFormat.GetAttribute<FileFormatAttribute>();
			binaryFile.FileName = fileNameNoExtension + (((attribute != null) ? attribute.Extension : null) ?? "");
			return binaryFile;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002214 File Offset: 0x00000414
		private static IList<WordMergedDocument.FindMatch> FindAll(Document document, string findWord)
		{
			Regex pattern = new Regex(findWord, RegexOptions.IgnoreCase);
			WordMergedDocument.FindAllEvaluator findAllEvaluator = new WordMergedDocument.FindAllEvaluator();
			document.Range.Replace(pattern, "", new FindReplaceOptions
			{
				ReplacingCallback = findAllEvaluator,
				MatchCase = false,
				Direction = FindReplaceDirection.Backward
			});
			return findAllEvaluator.Matches;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000226C File Offset: 0x0000046C
		private static string RemoveClockWorkMailMergeTags(string s)
		{
			return (s == null || s.Length < 5) ? (s ?? "") : s.Substring(2, s.Length - 4);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022A8 File Offset: 0x000004A8
		public IList<string> ExtractUniqueCodes(byte[] fileBytes)
		{
			this._document = WordMergedDocument.LoadDocument(fileBytes);
			return (from g in WordMergedDocument.FindAll(this._document, "#\\<[^\\>#]+\\>#")
			select WordMergedDocument.RemoveClockWorkMailMergeTags(g.MatchString)).ToList<string>();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002300 File Offset: 0x00000500
		public void MergeImageField(MailMergeCode code, string codeName, Image image, byte[] imageBytes)
		{
			try
			{
				IList<WordMergedDocument.FindMatch> list = WordMergedDocument.FindAll(this._document, codeName);
				DocumentBuilder documentBuilder = new DocumentBuilder(this._document);
				foreach (WordMergedDocument.FindMatch findMatch in list)
				{
					documentBuilder.MoveTo(findMatch.MatchNodes[0]);
					bool flag = image != null;
					if (flag)
					{
						documentBuilder.InsertImage(image);
					}
					else
					{
						documentBuilder.InsertImage(imageBytes, 50.0, 50.0);
					}
					WordMergedDocument.ClearMailMergeCodeValues(findMatch.MatchNodes, 0);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.MergedDocumentWord.WordMergedDocument.MergeImageFiled:codename={0}:image={1}:imageBytes={2}:err={3}", new object[]
				{
					codeName ?? "NULL",
					((image != null) ? image.Size.ToString() : null) ?? "NULL",
					((imageBytes != null) ? imageBytes.Length.ToString() : null) ?? "NULL",
					ex.ToString()
				});
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000243C File Offset: 0x0000063C
		public void MergeStringField(MailMergeCode code, string codeName, string codeVal)
		{
			string text = (codeVal ?? "").Replace("\r\n", "\n").Replace("\n", ControlChar.LineBreak);
			IList<WordMergedDocument.FindMatch> list = null;
			try
			{
				string findWord = codeName.StartsWith("#<") ? codeName : ("#<" + codeName + ">#");
				list = WordMergedDocument.FindAll(this._document, findWord);
				DocumentBuilder documentBuilder = new DocumentBuilder(this._document);
				foreach (WordMergedDocument.FindMatch findMatch in list)
				{
					documentBuilder.MoveTo(findMatch.MatchNodes[0]);
					findMatch.MatchNodes[0].Range.Replace((findMatch.MatchNodes[0].Range.Text ?? "").Trim(), text, new FindReplaceOptions
					{
						MatchCase = true,
						FindWholeWordsOnly = false
					});
					WordMergedDocument.ClearMailMergeCodeValues(findMatch.MatchNodes, 1);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.MergedDocumentWord.WordMergedDocument.MergeStringField:codename={0}:codeValue={1}:err={2}", codeName ?? "NULL", text ?? "NULL", ex.ToString());
				bool flag = list != null;
				if (flag)
				{
					WordMergedDocument.ClearMailMergeCodeValues(list.SelectMany((WordMergedDocument.FindMatch g) => g.MatchNodes).ToArray<Node>(), 0);
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025D4 File Offset: 0x000007D4
		public void MergeBooleanField(MailMergeCode code, string codeName, MailMergeCheckedItem item)
		{
			IList<WordMergedDocument.FindMatch> list = null;
			try
			{
				list = WordMergedDocument.FindAll(this._document, codeName);
				foreach (WordMergedDocument.FindMatch findMatch in list)
				{
					DocumentBuilder documentBuilder = new DocumentBuilder(this._document);
					documentBuilder.MoveTo(findMatch.MatchNodes[0]);
					documentBuilder.InsertCheckBox("", item.IsChecked, 0);
					bool flag = !item.HideCheckboxTitle;
					if (flag)
					{
						documentBuilder.Write(" " + (item.Title ?? ""));
					}
					WordMergedDocument.ClearMailMergeCodeValues(findMatch.MatchNodes, 0);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.MergedDocumentWord.WordMergedDocument.MergeBooleanField:codename={0}:codeValue={1}:err={2}", codeName ?? "NULL", ((item != null) ? item.IsChecked.ToString() : null) ?? "NULL", ex.ToString());
				bool flag2 = list != null;
				if (flag2)
				{
					WordMergedDocument.ClearMailMergeCodeValues(list.SelectMany((WordMergedDocument.FindMatch g) => g.MatchNodes).ToArray<Node>(), 0);
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002728 File Offset: 0x00000928
		private static void ClearMailMergeCodeValues(Node[] matchNodes, int startIndex = 0)
		{
			try
			{
				for (int i = startIndex; i < matchNodes.Length; i++)
				{
					Node node = matchNodes[i];
					node.Range.Replace((node.Range.Text ?? "").Trim(), "", new FindReplaceOptions
					{
						MatchCase = true,
						FindWholeWordsOnly = false
					});
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.MergedDocumentWord.WordMergedDocument.ClearMailMergeCodeValues:err={0}", ex.ToString());
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000027BC File Offset: 0x000009BC
		public void AppendDocument(IMergedDocument documentToAppend)
		{
			WordMergedDocument.AppendDocument(this._document, (Document)documentToAppend.Document, ImportFormatMode.KeepDifferentStyles);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000027D8 File Offset: 0x000009D8
		private static void AppendDocument(Document dstDoc, Document srcDoc, ImportFormatMode mode)
		{
			foreach (Node node in srcDoc)
			{
				Section srcNode = (Section)node;
				Node newChild = dstDoc.ImportNode(srcNode, true, mode);
				dstDoc.AppendChild(newChild);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002838 File Offset: 0x00000A38
		public void MergeDocument(MailMergeCode code, string codeName, IMergedDocument documentToMergeIn)
		{
			IList<WordMergedDocument.FindMatch> list = null;
			try
			{
				bool flag = documentToMergeIn == null;
				if (!flag)
				{
					list = WordMergedDocument.FindAll(this._document, codeName);
					foreach (WordMergedDocument.FindMatch findMatch in list)
					{
						DocumentBuilder documentBuilder = new DocumentBuilder(this._document);
						documentBuilder.MoveTo(findMatch.MatchNodes[0]);
						documentBuilder.InsertDocument((Document)documentToMergeIn.Document, ImportFormatMode.KeepSourceFormatting);
						WordMergedDocument.ClearMailMergeCodeValues(findMatch.MatchNodes, 0);
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("Common.DAO.MergedDocumentWord.WordMergedDocument.MergeDocument:codename={0}:codeValue={1}:err={2}", codeName ?? "NULL", (documentToMergeIn == null) ? "NULL" : "not null", ex.ToString());
				bool flag2 = list != null;
				if (flag2)
				{
					WordMergedDocument.ClearMailMergeCodeValues(list.SelectMany((WordMergedDocument.FindMatch g) => g.MatchNodes).ToArray<Node>(), 0);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000295C File Offset: 0x00000B5C
		private static Run SplitRun(Run run, int position)
		{
			Run run2 = (Run)run.Clone(true);
			run2.Text = run.Text.Substring(position);
			run.Text = run.Text.Substring(0, position);
			run.ParentNode.InsertAfter(run2, run);
			return run2;
		}

		// Token: 0x04000001 RID: 1
		private Document _document;

		// Token: 0x04000002 RID: 2
		private eFileFormat _outputFileFormat;

		// Token: 0x02000003 RID: 3
		internal class FindMatch
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000014 RID: 20 RVA: 0x000029B1 File Offset: 0x00000BB1
			// (set) Token: 0x06000015 RID: 21 RVA: 0x000029B9 File Offset: 0x00000BB9
			public string MatchString { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000016 RID: 22 RVA: 0x000029C2 File Offset: 0x00000BC2
			// (set) Token: 0x06000017 RID: 23 RVA: 0x000029CA File Offset: 0x00000BCA
			public Node[] MatchNodes { get; set; }
		}

		// Token: 0x02000004 RID: 4
		private class FindAllEvaluator : IReplacingCallback
		{
			// Token: 0x06000019 RID: 25 RVA: 0x000029DC File Offset: 0x00000BDC
			public FindAllEvaluator()
			{
				this._matches = new List<WordMergedDocument.FindMatch>();
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001A RID: 26 RVA: 0x000029F1 File Offset: 0x00000BF1
			public IList<WordMergedDocument.FindMatch> Matches
			{
				get
				{
					return this._matches;
				}
			}

			// Token: 0x0600001B RID: 27 RVA: 0x000029FC File Offset: 0x00000BFC
			ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
			{
				Node node = e.MatchNode;
				bool flag = e.MatchOffset > 0;
				if (flag)
				{
					node = WordMergedDocument.SplitRun((Run)node, e.MatchOffset);
				}
				List<Node> list = new List<Node>();
				int num = e.Match.Value.Length;
				while (num > 0 && node != null && node.GetText().Length <= num)
				{
					list.Add(node);
					num -= node.GetText().Length;
					do
					{
						node = node.NextSibling;
					}
					while (node != null && node.NodeType != NodeType.Run);
				}
				bool flag2 = node != null && num > 0;
				if (flag2)
				{
					WordMergedDocument.SplitRun((Run)node, num);
					list.Add(node);
				}
				string text = "";
				List<Node> list2 = new List<Node>();
				foreach (Node node2 in list)
				{
					text += node2.Range.Text;
					list2.Add(node2);
					string value = e.Match.Value;
					bool flag3 = text.Equals(value, StringComparison.OrdinalIgnoreCase);
					if (flag3)
					{
						this._matches.Add(new WordMergedDocument.FindMatch
						{
							MatchString = text,
							MatchNodes = list2.ToArray()
						});
						text = "";
						list2 = new List<Node>();
					}
				}
				return ReplaceAction.Skip;
			}

			// Token: 0x04000005 RID: 5
			private List<WordMergedDocument.FindMatch> _matches;
		}
	}
}
