using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Words;
using ClockWorkLogger;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.License;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Entity.Accommodations;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.MailMerging;
using TechnoPro.Common.DAO.Impl.Templates;
using TechnoPro.Common.DAO.MailMerging;
using TechnoPro.Common.DAO.Templates;
using TechnoPro.Common.Graphics;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Interfaces;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.DAO.SpireDoc.Impl
{
	// Token: 0x02000002 RID: 2
	public class MailMergingDocDAO : IMailMergingDocDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private AccommodationsDAO accommodationsDao
		{
			get
			{
				bool flag = this.adao == null;
				if (flag)
				{
					this.adao = new AccommodationsDAO(this.OpContext);
				}
				return this.adao;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002086 File Offset: 0x00000286
		public MailMergingDocDAO()
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		public MailMergingDocDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020A2 File Offset: 0x000002A2
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020AA File Offset: 0x000002AA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000006 RID: 6 RVA: 0x000020B4 File Offset: 0x000002B4
		private BinaryFile GenerateOutputFileMailingLabels(BinaryFile Template, List<List<MailMergeCode>> MultipleCodes, eFileFormat FileFormat)
		{
			BinaryFile result;
			try
			{
				Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
				FileFormat fileFormat;
				string str;
				if (FileFormat != eFileFormat.WordX)
				{
					if (FileFormat != eFileFormat.PDF)
					{
						fileFormat = FileFormat.Doc;
						str = ".doc";
					}
					else
					{
						fileFormat = FileFormat.PDF;
						str = ".pdf";
					}
				}
				else
				{
					fileFormat = FileFormat.Docx;
					str = ".docx";
				}
				bool flag = Template == null || string.IsNullOrEmpty(Template.FileName);
				Spire.Doc.Document document;
				if (flag)
				{
					document = new Spire.Doc.Document();
				}
				else
				{
					string text = (Path.GetExtension(Template.FileName) ?? "").ToLower();
					using (MemoryStream memoryStream = new MemoryStream(Template.ByteArray))
					{
						document = (text.Equals(".docx") ? new Spire.Doc.Document(memoryStream, FileFormat.Docx) : new Spire.Doc.Document(memoryStream));
					}
				}
				Spire.Doc.Section section = document.Sections[0];
				ITable table = section.Tables[0];
				bool flag2 = table.Rows.Count > 0 && table.Rows[0].Cells.Count > 0;
				if (flag2)
				{
					TableCell tableCell = table.Rows[0].Cells[0];
					float width = tableCell.Width;
					List<Spire.Doc.Documents.Paragraph> list = (from Spire.Doc.Documents.Paragraph p5 in tableCell.Paragraphs
					select (Spire.Doc.Documents.Paragraph)p5.Clone()).ToList<Spire.Doc.Documents.Paragraph>();
					double num = 0.0;
					Dictionary<double, int> dictionary = new Dictionary<double, int>();
					for (int i = 0; i < table.Rows[0].Cells.Count; i++)
					{
						TableCell tableCell2 = table.Rows[0].Cells[i];
						float num2 = Math.Abs(width - tableCell2.Width);
						bool flag3 = num2 >= 10f;
						if (!flag3)
						{
							dictionary.Add(num, i);
							num += 1.0;
						}
					}
					int num3 = -1;
					int num4 = -1;
					TempCache tempCache = new TempCache();
					for (int j = 0; j < MultipleCodes.Count; j++)
					{
						List<MailMergeCode> list2 = MultipleCodes[j];
						int num5 = (int)(Convert.ToDouble(j) / num);
						int num6 = (int)(Convert.ToDouble(j) % num);
						num3 = num5;
						num4 = num6;
						bool flag4 = num5 >= table.Rows.Count;
						if (flag4)
						{
							table.AddRow();
						}
						TableRow tableRow = table.Rows[num5];
						int index = dictionary[(double)num6];
						TableCell tableCell3 = tableRow.Cells[index];
						while (tableCell3.Paragraphs.Count > 0)
						{
							tableCell3.Paragraphs.RemoveAt(0);
						}
						tempCache.ClearNonGlobalItems();
						for (int k = 0; k < list.Count; k++)
						{
							Spire.Doc.Documents.Paragraph paragraph = (Spire.Doc.Documents.Paragraph)list[k].Clone();
							tableCell3.Paragraphs.Add(paragraph);
							string text2 = list[k].Text ?? "";
							foreach (MailMergeCode code in list2)
							{
								bool flag5 = text2.IndexOf("#<") < 0;
								if (flag5)
								{
									break;
								}
								string oldValue;
								object obj = MailMergingDocDAO.GetMailMergeValueForWordDoc(code, tempCache, out oldValue, this.accommodationsDao, this.OpContext, null) ?? "";
								string text3 = obj as string;
								bool flag6 = text3 != null;
								if (flag6)
								{
									string newValue = text3;
									text2 = text2.Replace(oldValue, newValue);
								}
								else
								{
									string newValue2 = obj.ToString();
									text2 = text2.Replace(oldValue, newValue2);
								}
								tableCell3.Paragraphs[tableCell3.Paragraphs.Count - 1].Text = text2;
							}
						}
					}
					bool flag7 = num3 >= 0;
					if (flag7)
					{
						try
						{
							num4++;
							bool flag8 = num4 < table.Rows[num3].Cells.Count;
							if (flag8)
							{
								TableRow tableRow2 = table.Rows[num3];
								for (int l = num4; l < table.Rows[num3].Cells.Count; l++)
								{
									TableCell tableCell4 = tableRow2.Cells[l];
									while (tableCell4.Paragraphs.Count > 0)
									{
										tableCell4.Paragraphs.RemoveAt(0);
									}
								}
							}
							num3++;
							bool flag9 = num3 < table.Rows.Count;
							if (flag9)
							{
								for (int m = num3; m < table.Rows.Count; m++)
								{
									for (int n = 0; n < table.Rows[n].Cells.Count; n++)
									{
										TableRow tableRow3 = table.Rows[m];
										TableCell tableCell5 = tableRow3.Cells[n];
										while (tableCell5.Paragraphs.Count > 0)
										{
											tableCell5.Paragraphs.RemoveAt(0);
										}
									}
								}
							}
						}
						catch (Exception ex)
						{
							CWLogger.Logger.Warn("MailMergingDocDAO:GenerateOutputFileMailingLabels:TryingToClearUnusedCells:{0}", ex.ToString());
						}
					}
				}
				byte[] array;
				using (MemoryStream memoryStream2 = new MemoryStream(0))
				{
					document.SaveToStream(memoryStream2, (fileFormat == FileFormat.PDF) ? FileFormat.Docx : fileFormat);
					array = memoryStream2.ToArray();
				}
				bool flag10 = fileFormat == FileFormat.PDF;
				if (flag10)
				{
					array = MailMergingDocDAO.ConvertDocxToPdf(array);
				}
				result = new BinaryFile
				{
					ByteArray = array,
					FileName = ((Template == null || string.IsNullOrEmpty(Template.FileName)) ? ("LOA" + str) : (Path.GetFileNameWithoutExtension(Template.FileName) + str))
				};
			}
			catch (Exception ex2)
			{
				CWLogger.Logger.Error("MailMergingDocDAO:GenerateOutputFile1:{0}", ex2.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002790 File Offset: 0x00000990
		private static IMergedDocument GenerateSingleOutputDocument(IList<MailMergeCode> codes, byte[] fileByteArray, eAllowedExtensionGroup fileType, bool isLicensed, eFileFormat outputFileFormat, TempCache tempCache, OperationContext opContext, IAccommodationsDAO accommodationsDao)
		{
			IMergedDocument mergedDocument = MergedDocumentFactory.GetMergedDocument(opContext.AppContext.ExecutingPath, fileType, isLicensed);
			mergedDocument.LoadDocument(fileByteArray, outputFileFormat);
			AccommodationListFormattingInfoDAO accommodationListFormattingInfoDAO;
			if (outputFileFormat != eFileFormat.Html)
			{
				accommodationListFormattingInfoDAO = null;
			}
			else
			{
				AccommodationListFormattingInfoDAO accommodationListFormattingInfoDAO2 = new AccommodationListFormattingInfoDAO();
				accommodationListFormattingInfoDAO2.emptyListString = "";
				accommodationListFormattingInfoDAO2.itemNewline = "<br />";
				accommodationListFormattingInfoDAO2.itemHeader = "<ul>";
				accommodationListFormattingInfoDAO2.itemFooter = "</ul>";
				accommodationListFormattingInfoDAO2.itemPre = "<li>";
				accommodationListFormattingInfoDAO = accommodationListFormattingInfoDAO2;
				accommodationListFormattingInfoDAO2.itemPost = "</li>";
			}
			AccommodationListFormattingInfoDAO listFormatting = accommodationListFormattingInfoDAO;
			foreach (MailMergeCode mailMergeCode in codes)
			{
				string text;
				object obj = MailMergingDocDAO.GetMailMergeValueForWordDoc(mailMergeCode, tempCache, out text, accommodationsDao, opContext, listFormatting) ?? "";
				InsertedDocumentInfo insertedDocumentInfo = obj as InsertedDocumentInfo;
				bool flag = insertedDocumentInfo != null;
				if (flag)
				{
					ITemplateDAO templateDAO = new TemplateDAO(opContext);
					Template template = templateDAO.LoadTemplate(insertedDocumentInfo.TemplateId, true);
					BinaryFile binaryFile = (template != null) ? template.Document : null;
					bool flag2 = binaryFile != null;
					if (flag2)
					{
						IMergedDocument documentToMergeIn = MailMergingDocDAO.GenerateSingleOutputDocument(codes, binaryFile.ByteArray, binaryFile.FileName.GetAllowedExtensionGroupForFilename(), isLicensed, mergedDocument.OutputFileFormat, tempCache, opContext, accommodationsDao);
						mergedDocument.MergeDocument(mailMergeCode, text, documentToMergeIn);
					}
				}
				else
				{
					bool flag3 = obj is MailMergeCheckedItem || obj is IList<MailMergeCheckedItem>;
					if (flag3)
					{
						IList<MailMergeCheckedItem> list;
						if (!(obj is MailMergeCheckedItem))
						{
							list = (obj as IList<MailMergeCheckedItem>);
						}
						else
						{
							IList<MailMergeCheckedItem> list2 = new List<MailMergeCheckedItem>
							{
								(MailMergeCheckedItem)obj
							};
							list = list2;
						}
						IList<MailMergeCheckedItem> list3 = list;
						IMergedDocument mergedDocument2 = mergedDocument;
						MailMergeCode code = mailMergeCode;
						string codeName = text;
						MailMergeCheckedItem item;
						if (list3.Count >= 1)
						{
							item = list3[0];
						}
						else
						{
							(item = new MailMergeCheckedItem()).Title = "";
						}
						mergedDocument2.MergeBooleanField(code, codeName, item);
					}
					else
					{
						bool flag4 = obj is byte[];
						if (flag4)
						{
							byte[] imageBytes = (byte[])obj;
							mergedDocument.MergeImageField(mailMergeCode, text, MailMergingDocDAO.GetImage(mailMergeCode, imageBytes), imageBytes);
						}
						else
						{
							bool flag5 = obj is string;
							if (flag5)
							{
								mergedDocument.MergeStringField(mailMergeCode, text, (string)obj);
							}
							else
							{
								CWLogger.Logger.Warn("GeneratePdfOutputFile:MailMergeValue is unexpected type: type={0}", ((obj != null) ? obj.GetType().ToString() : null) ?? "NULL");
								mergedDocument.MergeStringField(mailMergeCode, text, ((obj != null) ? obj.ToString() : null) ?? "");
							}
						}
					}
				}
			}
			return mergedDocument;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002A20 File Offset: 0x00000C20
		private static Image GetImage(MailMergeCode code, byte[] imageBytes)
		{
			bool flag = ((code != null) ? code.Args : null) == null;
			Image result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					bool flag2 = code.Args.ContainsKey("width") && code.Args.ContainsKey("height");
					int num;
					int num2;
					if (flag2)
					{
						int.TryParse(code.Args["width"] ?? "", out num);
						int.TryParse(code.Args["height"] ?? "", out num2);
					}
					else
					{
						bool flag3 = code.Args.ContainsKey("imgwidth") && code.Args.ContainsKey("imgheight");
						if (flag3)
						{
							int.TryParse(code.Args["imgwidth"] ?? "", out num);
							int.TryParse(code.Args["imgheight"] ?? "", out num2);
						}
						else
						{
							num = 200;
							num2 = 200;
						}
					}
					bool flag4 = num > 0 && num2 > 0;
					if (flag4)
					{
						Image imgToResize = null;
						using (MemoryStream memoryStream = new MemoryStream(imageBytes))
						{
							imgToResize = Image.FromStream(memoryStream);
						}
						return imgToResize.ResizeImageKeepAspectRatio(new Size(num, num2));
					}
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Warn("MailMergingDocDAO:GenerateOutputFile:gg={0}", ex.ToString());
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002BDC File Offset: 0x00000DDC
		public BinaryFile GenerateOutputFile(byte[] templateByteArray, eAllowedExtensionGroup fileType, string fileName, List<List<MailMergeCode>> MultipleCodes, eFileFormat FileFormat, bool isLicensed)
		{
			bool flag = templateByteArray == null || MultipleCodes == null || MultipleCodes.Count < 1;
			BinaryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					TempCache tempCache = new TempCache();
					bool flag2 = MultipleCodes.Count == 1;
					IMergedDocument mergedDocument;
					if (flag2)
					{
						mergedDocument = MailMergingDocDAO.GenerateSingleOutputDocument(MultipleCodes[0], templateByteArray, fileType, isLicensed, FileFormat, tempCache, this.OpContext, this.accommodationsDao);
					}
					else
					{
						mergedDocument = null;
						foreach (List<MailMergeCode> codes in MultipleCodes)
						{
							IMergedDocument mergedDocument2 = MailMergingDocDAO.GenerateSingleOutputDocument(codes, templateByteArray, fileType, isLicensed, FileFormat, tempCache, this.OpContext, this.accommodationsDao);
							bool flag3 = mergedDocument == null;
							if (flag3)
							{
								mergedDocument = mergedDocument2;
							}
							else
							{
								mergedDocument.AppendDocument(mergedDocument2);
							}
							tempCache.ClearNonGlobalItems();
						}
					}
					result = ((mergedDocument != null) ? mergedDocument.SaveDocument(string.IsNullOrEmpty(fileName) ? "LOA" : Path.GetFileNameWithoutExtension(fileName)) : null);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Error("MailMergingDocDAO:GeneratePdfOutputFile:{0}", ex.ToString());
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002D18 File Offset: 0x00000F18
		private static byte[] ConvertDocxToPdf(byte[] docxBytes)
		{
			License license = new License();
			license.SetLicense("Aspose.Words.lic");
			Aspose.Words.Document document;
			using (MemoryStream memoryStream = new MemoryStream(docxBytes))
			{
				document = new Aspose.Words.Document(memoryStream);
			}
			byte[] result;
			using (MemoryStream memoryStream2 = new MemoryStream(0))
			{
				document.Save(memoryStream2, SaveFormat.Pdf);
				result = memoryStream2.ToArray();
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002DA4 File Offset: 0x00000FA4
		private static object GetMailMergeValueForWordDoc(MailMergeCode code, TempCache tempCache, out string codeName, IAccommodationsDAO accommodationsDao, OperationContext opContext, AccommodationListFormattingInfoDAO listFormatting = null)
		{
			codeName = ((code.OriginalCode.StartsWith("#&lt;") || code.OriginalCode.StartsWith("#<")) ? code.OriginalCode : ("#<" + code.OriginalCode + ">#"));
			bool mailMergeValueIsNull = code.MailMergeValueIsNull;
			object result;
			if (mailMergeValueIsNull)
			{
				result = (code.DefaultValue ?? "");
			}
			else
			{
				bool flag = code.ValueFormat != null && code.ValueFormat.ValueFormatType == eValueFormatType.InsertedMailMergeDocument;
				if (flag)
				{
					string customFormat = code.ValueFormat.CustomFormat;
					int num;
					bool flag2 = customFormat.Length < 1 || !int.TryParse(customFormat, out num);
					if (flag2)
					{
						num = 0;
					}
					bool flag3 = num < 1;
					if (flag3)
					{
						result = string.Empty;
					}
					else
					{
						result = new InsertedDocumentInfo
						{
							TemplateId = num
						};
					}
				}
				else
				{
					bool flag4 = code.IsOfType<MailMergeValueString>();
					if (flag4)
					{
						IList<string> mailMergeValues = code.GetMailMergeValues<MailMergeValueString, string>(string.Empty);
						bool flag5 = mailMergeValues == null || mailMergeValues.Count < 1;
						if (flag5)
						{
							result = string.Empty;
						}
						else
						{
							bool flag6 = mailMergeValues.Count == 1;
							if (flag6)
							{
								result = mailMergeValues[0];
							}
							else
							{
								string text = "";
								bool flag7 = code.ValueFormat == null;
								if (flag7)
								{
									result = text;
								}
								else
								{
									eValueFormatType valueFormatType = code.ValueFormat.ValueFormatType;
									eValueFormatType eValueFormatType = valueFormatType;
									if (eValueFormatType != eValueFormatType.BulletedList)
									{
										text = string.Join(", ", mailMergeValues.ToArray<string>());
									}
									else
									{
										text = string.Join("\r\n", mailMergeValues.ToArray<string>());
									}
									result = text;
								}
							}
						}
					}
					else
					{
						bool flag8 = code.IsOfType<MailMergeValueBool>();
						if (flag8)
						{
							bool firstMailMergeValue = code.GetFirstMailMergeValue<MailMergeValueBool, bool>(false);
							string text2 = null;
							bool flag9 = code.Args.ContainsKey("formatstring");
							if (flag9)
							{
								text2 = code.Args["formatstring"];
							}
							else
							{
								bool flag10 = code.ValueFormat != null;
								if (flag10)
								{
									eValueFormatType valueFormatType2 = code.ValueFormat.ValueFormatType;
									eValueFormatType eValueFormatType2 = valueFormatType2;
									if (eValueFormatType2 == eValueFormatType.BooleanTrueFalse)
									{
										text2 = "tf";
									}
								}
							}
							bool flag11 = string.IsNullOrEmpty(text2);
							if (flag11)
							{
								text2 = "yn";
							}
							int num2 = text2.IndexOf(",");
							bool flag12 = num2 > 0;
							if (flag12)
							{
								string text3 = text2.Substring(0, num2);
								string text4 = text2.Substring(num2 + 1);
								result = (firstMailMergeValue ? text3 : text4);
							}
							else
							{
								bool flag13 = text2.Equals("tf", StringComparison.OrdinalIgnoreCase);
								if (flag13)
								{
									result = (firstMailMergeValue ? "True" : "False");
								}
								else
								{
									result = (firstMailMergeValue ? "Yes" : "No");
								}
							}
						}
						else
						{
							bool flag14 = code.IsOfType<MailMergeValueInt>();
							if (flag14)
							{
								result = code.GetFirstMailMergeValue<MailMergeValueInt, int>(0);
							}
							else
							{
								bool flag15 = code.IsOfType<MailMergeValueDouble>();
								if (flag15)
								{
									result = code.GetFirstMailMergeValue<MailMergeValueDouble, double>(0.0);
								}
								else
								{
									bool flag16 = code.IsOfType<MailMergeValueDateTime>() || code.IsOfType<MailMergeValueDateTimeNullable>();
									if (flag16)
									{
										bool flag17 = code.IsOfType<MailMergeValueDateTime>();
										DateTime d;
										if (flag17)
										{
											d = code.GetFirstMailMergeValue<MailMergeValueDateTime, DateTime>(DateTime.MinValue);
										}
										else
										{
											DateTime firstMailMergeValue2 = code.GetFirstMailMergeValue<MailMergeValueDateTimeNullable, DateTime>(DateTime.MinValue);
											d = firstMailMergeValue2;
										}
										bool flag18 = d == DateTime.MinValue;
										if (flag18)
										{
											result = "";
										}
										else
										{
											bool flag19 = code.Args.ContainsKey("formatstring");
											if (flag19)
											{
												string format = code.Args["formatstring"];
												result = d.ToString(format);
											}
											else
											{
												bool flag20 = code.ValueFormat == null;
												if (flag20)
												{
													result = d.ToString("MMMM d, yyyy");
												}
												else
												{
													switch (code.ValueFormat.ValueFormatType)
													{
													case eValueFormatType.CustomFormat:
														result = d.ToString(code.ValueFormat.CustomFormat ?? "");
														break;
													case eValueFormatType.DateSmall:
														result = d.ToShortDateString();
														break;
													case eValueFormatType.DateLarge:
														result = d.ToLongDateString();
														break;
													case eValueFormatType.TimeAmPm:
														result = d.ToString("h:mm tt");
														break;
													case eValueFormatType.TimeMilitary:
														result = d.ToString("H:mm");
														break;
													default:
														result = d.ToString("MMMM d, yyyy");
														break;
													}
												}
											}
										}
									}
									else
									{
										bool flag21 = code.IsOfType<MailMergeValueDynamicData>();
										if (flag21)
										{
											IList<DynamicData> mailMergeValues2 = code.GetMailMergeValues<MailMergeValueDynamicData, DynamicData>(null);
											bool flag22 = mailMergeValues2 == null || mailMergeValues2.Count <= 0;
											if (flag22)
											{
												result = null;
											}
											else
											{
												bool flag23 = mailMergeValues2[0].Value != null && mailMergeValues2[0].Value is byte[] && (mailMergeValues2[0].Field.ControlCode == eControlCode.Label || mailMergeValues2[0].Field.ControlCode == eControlCode.Picture);
												if (flag23)
												{
													result = (byte[])mailMergeValues2[0].Value;
												}
												else
												{
													bool flag24 = mailMergeValues2.Count == 1;
													string text5;
													if (flag24)
													{
														text5 = mailMergeValues2[0].GetString();
													}
													else
													{
														eValueFormatType eValueFormatType3 = (code.ValueFormat == null) ? eValueFormatType.BulletedList : code.ValueFormat.ValueFormatType;
														string text6 = (code.ValueFormat == null) ? "" : (code.ValueFormat.CustomFormat ?? "");
														StringBuilder stringBuilder = new StringBuilder();
														eValueFormatType eValueFormatType4 = eValueFormatType3;
														eValueFormatType eValueFormatType5 = eValueFormatType4;
														if (eValueFormatType5 != eValueFormatType.CommaSeparatedList)
														{
															if (eValueFormatType5 != eValueFormatType.NumberedList)
															{
																bool flag25 = eValueFormatType3 == eValueFormatType.DefaultToStringFormat && mailMergeValues2.Count == 1;
																string str;
																if (flag25)
																{
																	str = "";
																}
																else
																{
																	str = "* ";
																}
																foreach (DynamicData dynamicData in mailMergeValues2)
																{
																	bool flag26 = stringBuilder.Length > 0;
																	if (flag26)
																	{
																		stringBuilder.Append("\r\n");
																	}
																	bool flag27 = dynamicData.Field.ControlCode == eControlCode.RtfTextBox;
																	if (flag27)
																	{
																		dynamicData.Value = dynamicData.Value.ToString().ConvertRtfToPlainText();
																	}
																	stringBuilder.Append(str + dynamicData.GetStringWithCaption());
																}
																text5 = stringBuilder.ToString();
															}
															else
															{
																bool flag28 = text6.Length > 0;
																TempCacheObject tempCacheObject;
																if (flag28)
																{
																	tempCacheObject = (tempCache.ContainsKey(text6) ? tempCache[text6] : tempCache.AddLocalItem(text6, 1));
																}
																else
																{
																	tempCacheObject = null;
																}
																int ctr = (tempCacheObject == null) ? 1 : ((int)tempCacheObject.Object);
																text5 = string.Join("\r\n", mailMergeValues2.Select(delegate(DynamicData g)
																{
																	bool flag40 = g.Field.ControlCode == eControlCode.RtfTextBox;
																	if (flag40)
																	{
																		g.Value = g.Value.ToString().ConvertRtfToPlainText();
																	}
																	int ctr = ctr;
																	ctr++;
																	return ctr.ToString() + ". " + g.GetStringWithCaption();
																}).ToArray<string>());
																bool flag29 = tempCacheObject != null;
																if (flag29)
																{
																	tempCacheObject.Object = ctr;
																}
															}
														}
														else
														{
															foreach (DynamicData dynamicData2 in mailMergeValues2)
															{
																bool flag30 = stringBuilder.Length > 0;
																if (flag30)
																{
																	stringBuilder.Append(", ");
																}
																bool flag31 = dynamicData2.Field.ControlCode == eControlCode.RtfTextBox;
																if (flag31)
																{
																	dynamicData2.Value = dynamicData2.Value.ToString().ConvertRtfToPlainText();
																}
																stringBuilder.Append(dynamicData2.GetStringWithCaption());
															}
															text5 = stringBuilder.ToString();
														}
													}
													result = text5;
												}
											}
										}
										else
										{
											bool flag32 = code.IsOfType<MailMergeValueCheckedItem>();
											if (flag32)
											{
												result = code.GetFirstMailMergeValue<MailMergeValueCheckedItem, MailMergeCheckedItem>(new MailMergeCheckedItem());
											}
											else
											{
												bool flag33 = code.IsOfType<MailMergeValueByteArray>();
												if (flag33)
												{
													result = code.GetFirstMailMergeValue<MailMergeValueByteArray, byte[]>(null);
												}
												else
												{
													bool flag34 = code.IsOfType<MailMergeValueAccommodationData>();
													if (flag34)
													{
														IList<AccommodationData> mailMergeValues3 = code.GetMailMergeValues<MailMergeValueAccommodationData, AccommodationData>(null);
														bool flag35 = mailMergeValues3 != null && mailMergeValues3.Count > 0;
														if (flag35)
														{
															string mailMergeCode = code.Name.ToLower().Trim();
															bool flag36 = code.ValueFormat != null && code.ValueFormat.ValueFormatType == eValueFormatType.NumberedList;
															string listCounterName;
															if (flag36)
															{
																listFormatting = new AccommodationListFormattingInfoDAO
																{
																	itemFooter = "",
																	itemHeader = "",
																	itemNewline = "\r\n",
																	itemPre = "{ctr}. ",
																	itemPost = "",
																	emptyListString = "None."
																};
																listCounterName = code.ValueFormat.CustomFormat;
															}
															else
															{
																bool flag37 = code.ValueFormat != null && code.ValueFormat.ValueFormatType == eValueFormatType.CommaSeparatedList;
																if (flag37)
																{
																	listFormatting = new AccommodationListFormattingInfoDAO
																	{
																		itemFooter = "",
																		itemHeader = "",
																		itemNewline = ", ",
																		itemPre = "",
																		itemPost = "",
																		emptyListString = "None."
																	};
																	listCounterName = code.ValueFormat.CustomFormat;
																}
																else
																{
																	bool flag38 = code.ValueFormat != null && code.ValueFormat.ValueFormatType == eValueFormatType.AccommodationsListWithCaptionAndLongDescription;
																	if (flag38)
																	{
																		return MailMergingDocDAO.GenerateAccommodationListWithCaptionAndLongDescriptionInHtml(mailMergeValues3.ToList<AccommodationData>());
																	}
																	listCounterName = null;
																}
															}
															bool flag39 = accommodationsDao == null;
															if (flag39)
															{
																accommodationsDao = new AccommodationsDAO(opContext);
															}
															return accommodationsDao.GetAccommodationsListString(mailMergeValues3.ToList<AccommodationData>(), mailMergeCode, listFormatting, tempCache, listCounterName);
														}
													}
													result = (code.GetFirstMailMergeValueAsString() ?? "");
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000375C File Offset: 0x0000195C
		private static string GenerateAccommodationListWithCaptionAndLongDescriptionInHtml(List<AccommodationData> accommodations)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder("#<HTML>#:<ul>");
			foreach (AccommodationData accommodationData in accommodations)
			{
				accommodationData.GetString();
				DynamicField dynamicField;
				if (accommodationData == null)
				{
					dynamicField = null;
				}
				else
				{
					DynamicData data = accommodationData.Data;
					dynamicField = ((data != null) ? data.Field : null);
				}
				DynamicField dynamicField2 = dynamicField;
				string text = ((dynamicField2 != null) ? dynamicField2.GetCaptionForDisplay() : null) ?? "";
				bool flag = list.Contains(text);
				if (!flag)
				{
					list.Add(text);
					string text2;
					if (accommodationData == null)
					{
						text2 = null;
					}
					else
					{
						ExtendedAccommodationInfo detail = accommodationData.Detail;
						text2 = ((detail != null) ? detail.LongDescription : null);
					}
					string text3 = (text2 ?? "").Trim().Replace("\n", "<br />");
					string value = (text3.Length > 0) ? string.Concat(new string[]
					{
						"<li><b>",
						text,
						"</b><br />",
						text3,
						"</li>"
					}) : ("<li><b>" + text + "</b></li>");
					stringBuilder.AppendLine(value);
				}
			}
			stringBuilder.AppendLine("</ul>");
			return stringBuilder.ToString();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000038BC File Offset: 0x00001ABC
		public IList<string> ExtractUniqueCodes(byte[] fileByteArray, eAllowedExtensionGroup fileType, bool isLicensed)
		{
			bool flag = fileByteArray == null || fileByteArray.Length < 1;
			IList<string> result;
			if (flag)
			{
				result = new List<string>();
			}
			else
			{
				IMergedDocument mergedDocument = MergedDocumentFactory.GetMergedDocument(this.OpContext.AppContext.ExecutingPath, fileType, isLicensed);
				result = mergedDocument.ExtractUniqueCodes(fileByteArray);
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00003908 File Offset: 0x00001B08
		public BinaryFile OutputFile(byte[] templateByteArray, eAllowedExtensionGroup fileType, string fileName, bool isLicensed, List<List<MailMergeCode>> MultipleCodes, eFileFormat OutputFileFormat)
		{
			return this.GenerateOutputFile(templateByteArray, fileType, fileName, MultipleCodes, OutputFileFormat, isLicensed);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000392C File Offset: 0x00001B2C
		public BinaryFile OutputFileMailingLabels(BinaryFile Template, List<List<MailMergeCode>> MultipleCodes, eFileFormat OutputFileFormat)
		{
			return this.GenerateOutputFileMailingLabels(Template, MultipleCodes, OutputFileFormat);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003948 File Offset: 0x00001B48
		public BinaryFile GenerateDocumentFromPrintCodes(IList<DocumentPrintItem> printItems, string FileName, eFileFormat OutputFormat)
		{
			Spire.License.LicenseProvider.SetLicenseKey("Nt7LAQBrbp9srInAFY1rTBrNwnmkYJkGXApnOKEuC/9MCHqIMm712u/jfvvnbpVIQiXmzdElmPz/sUFt/zJRG/cOb9fMnza0pvdIOGRFzQC1O+XHsUVxMF2mfKxawADpojVNdvk/OycnX+c7iLqgLfMRGsMQ7FYpqyFtKfxZXwqcFXOMnXGQSVf+bkXv/mfn4o3C7YhCx0IvOoBhgmVWItGwjGfeem0T39gv60LHF2Jw6Eo6Y3LDLASobZXkt0MxOfc330IkYtpeLEovbZWyf/9PUk7iLG6+ursM6zjMEjX6EIKKehcS/yMmPY76pwjcX43eOzWcncDGu6m7tSHb1ZRnewehTDOowiW0dJWsvwjZTn6JYP6JbeyBKGa3WdZ4BUOuzW8GtIgF2FMi7CfNu+Xq51yjzhNYwxAEuLSmLDfgnWmqEzWNWrLGUlMTqQMu07s9RCMp+o9nZNr9U3En2FlMKXZ92Tt9zSZcfi+n8u/Zk2QSfeBN6PEmYOuFO5b+ULp3ymNRpAYuYXWdG6XLka+xJiEdWcjc3tGsi0zhmKohKrL4WFB9uWs7jaHoyqn6IAXjJP+BhMXRaSDn3W8CISe/rsBzq5HS/OMb9MtV7CnUcorvlAJ50Rv6aSgDpfpNZB1z7WKeGNtxedg9Fp+HOJ+xrRLecdUTVAVlqqn5JPM2O8nd84k7TUUqs9t0ZjKyEG2UarSjtcjARWXVAv7kLx/MFaHgHFrLGmec0hDKwwIv2ejCiHQBqrj37J8HYvvc3ShTfkcBSnytUzBuzMVBD8G1Fx0rAwll3gEhRp+bPDXrB/7m/hm+x2ZjriUkbKCmijNJGMRh2KoM/2lwzvDIsZHQheZtYBruz803UtTBjxRlJvwqVAfCX09K/MFTqM7pwv9G9eJQAwpYMKk4vZD87Gvvcr01Fs9A0qk541MCC/D+uDyFv4XwxHNu68R110x1sYOcyYtZzRIOo6Ag+84DQOSDTm9j5pc29IlBAXV5P41z8OmyS2ZTrLe/jGN5ZGc3KgTqbqz9YP6z+KYe2rtmQpCRSs1eDcV2aCntcJTLEMjil/aA7A/FN4m6bQ3EBuV9NqcH3MmNQ/2Js3jq+I0GpzuKX7iGdi6ti0nD16ULLgVTY0BvNYf9+s9k/o1LMGuloZPPf3poxhgNSMUBdyEg+kjh8i+AMa42a6CRv0cpmrE4UM7Kc8n34BzCHNARd3qZLnWt8MlXV+dfSkaAVjdx1TwHvQLGmG9do372RIWHtLTra9rc22hJjvwnsI9LZz88SFtSQlrZfqNxn6Z1h+NvHBmIXDqAoBSvt6TorDgMBL64EYCbxo9fdyEj/+i2TMv8aq4UJUK6p29hWaD/Rmu/v6BT/EDhMzFJdFOHbFu4AxOhNX5jOjhCH1El8Es0JRpc4HMWfT7zDV/WOU3eH3RO5j6Iu2fD2Q3EpGJfINvVKZ8uAnI2z44tQIUgJmHrthd4cecyHBzS2IU=");
			Spire.Doc.Document document = new Spire.Doc.Document();
			Spire.Doc.Section section = document.AddSection();
			section.PageSetup.DifferentFirstPageHeaderFooter = false;
			Spire.Doc.HeaderFooter header = section.HeadersFooters.Header;
			Spire.Doc.Documents.Paragraph paragraph = header.AddParagraph();
			paragraph.AppendText("pg: ");
			paragraph.AppendField("page number", FieldType.FieldPage);
			paragraph.AppendText(" of ");
			paragraph.AppendField("number of pages", FieldType.FieldNumPages);
			paragraph.Format.HorizontalAlignment = HorizontalAlignment.Right;
			Spire.Doc.HeaderFooter footer = section.HeadersFooters.Footer;
			Spire.Doc.Documents.Paragraph paragraph2 = footer.AddParagraph();
			TextRange textRange = paragraph2.AppendText(string.Format("Date Printed: {0}", DateTime.Now.ToString("yyyy.MM.dd")));
			textRange.CharacterFormat.FontSize = 12f;
			textRange.CharacterFormat.Bold = true;
			textRange.CharacterFormat.Italic = true;
			section.PageSetup.PageSize = PageSize.A4;
			section.PageSetup.Margins.Top = 40f;
			section.PageSetup.Margins.Bottom = 40f;
			section.PageSetup.Margins.Left = 25f;
			section.PageSetup.Margins.Right = 25f;
			Spire.Doc.Section section2 = section;
			Spire.Doc.Documents.Paragraph paragraph3 = section2.AddParagraph();
			paragraph3.Format.HorizontalAlignment = HorizontalAlignment.Center;
			Table table = null;
			string[] array = new string[]
			{
				"Time",
				"Name",
				"Patient ID",
				"Medicare",
				"Birthdate",
				"Telephone"
			};
			int[] array2 = new int[]
			{
				45,
				150,
				70,
				115,
				60,
				65
			};
			int num = 0;
			for (int i = 0; i < printItems.Count; i++)
			{
				DocumentPrintItem documentPrintItem = printItems[i];
				switch (documentPrintItem.ItemType)
				{
				case eDocumentPrintItemType.DocumentStart:
					section2 = document.AddSection();
					section2.BreakCode = SectionBreakType.NoBreak;
					break;
				case eDocumentPrintItemType.Regular:
				{
					TableRow tableRow = table.Rows[num + 1];
					tableRow.HeightType = TableRowHeightType.AtLeast;
					tableRow.Height = 20f;
					tableRow.RowFormat.BackColor = Color.White;
					for (int j = 0; j < array.Length; j++)
					{
						tableRow.Cells[j].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
						tableRow.Cells[j].Width = (float)array2[j];
						tableRow.Cells[j].AddParagraph().AppendText(documentPrintItem.ColumnText[j]).CharacterFormat.FontSize = 8f;
					}
					num++;
					break;
				}
				case eDocumentPrintItemType.TableFooter:
				{
					Spire.Doc.Documents.Paragraph paragraph4 = section2.AddParagraph();
					TextRange textRange2 = paragraph4.AppendText(documentPrintItem.ColumnText[0]);
					textRange2.CharacterFormat.Italic = true;
					textRange2.CharacterFormat.FontName = "Courier New";
					textRange2.CharacterFormat.FontSize = 8f;
					section2.AddParagraph().AppendBreak(Spire.Doc.Documents.BreakType.LineBreak);
					break;
				}
				case eDocumentPrintItemType.NewLine:
					paragraph3.AppendBreak(Spire.Doc.Documents.BreakType.LineBreak);
					break;
				case eDocumentPrintItemType.PageTitle:
				{
					paragraph3 = section2.AddParagraph();
					paragraph3.Format.HorizontalAlignment = HorizontalAlignment.Center;
					TextRange textRange3 = paragraph3.AppendText(documentPrintItem.ColumnText[0]);
					textRange3.CharacterFormat.Bold = true;
					paragraph3.AppendBreak(Spire.Doc.Documents.BreakType.LineBreak);
					break;
				}
				case eDocumentPrintItemType.TableStart:
				{
					int k;
					for (k = i + 1; k < printItems.Count; k++)
					{
						bool flag = printItems[k].ItemType == eDocumentPrintItemType.TableFooter;
						if (flag)
						{
							break;
						}
					}
					int rowsNum = k - i - 1;
					num = 0;
					table = section2.AddTable();
					table.ResetCells(rowsNum, array.Length);
					TableRow tableRow2 = table.Rows[0];
					tableRow2.IsHeader = true;
					tableRow2.Height = 20f;
					tableRow2.HeightType = TableRowHeightType.Exactly;
					tableRow2.RowFormat.BackColor = Color.LightSkyBlue;
					for (int l = 0; l < array.Length; l++)
					{
						tableRow2.Cells[l].Width = (float)array2[l];
						tableRow2.Cells[l].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
						Spire.Doc.Documents.Paragraph paragraph5 = tableRow2.Cells[l].AddParagraph();
						paragraph5.Format.HorizontalAlignment = HorizontalAlignment.Center;
						TextRange textRange4 = paragraph5.AppendText(array[l]);
						textRange4.CharacterFormat.Bold = true;
						textRange4.CharacterFormat.FontSize = 8f;
					}
					break;
				}
				case eDocumentPrintItemType.TableHeader:
				{
					paragraph3 = section2.AddParagraph();
					paragraph3.Format.HorizontalAlignment = HorizontalAlignment.Center;
					TextRange textRange5 = paragraph3.AppendText(documentPrintItem.ColumnText[0]);
					textRange5.CharacterFormat.Bold = true;
					textRange5.CharacterFormat.Italic = true;
					break;
				}
				case eDocumentPrintItemType.PageBreak:
					section2 = document.AddSection();
					section2.BreakCode = SectionBreakType.NewPage;
					break;
				}
			}
			FileFormat fileFormat;
			string text;
			if (OutputFormat != eFileFormat.Word)
			{
				if (OutputFormat != eFileFormat.PDF)
				{
					fileFormat = FileFormat.Docx;
					text = ".docx";
				}
				else
				{
					fileFormat = FileFormat.PDF;
					text = ".pdf";
				}
			}
			else
			{
				fileFormat = FileFormat.Doc;
				text = ".doc";
			}
			byte[] array3;
			using (MemoryStream memoryStream = new MemoryStream(0))
			{
				document.SaveToStream(memoryStream, (fileFormat == FileFormat.PDF) ? FileFormat.Docx : fileFormat);
				array3 = memoryStream.ToArray();
			}
			bool flag2 = fileFormat == FileFormat.PDF;
			if (flag2)
			{
				array3 = MailMergingDocDAO.ConvertDocxToPdf(array3);
			}
			bool flag3 = string.IsNullOrEmpty(FileName);
			BinaryFile result;
			if (flag3)
			{
				result = new BinaryFile
				{
					ByteArray = array3,
					FileName = FileName
				};
			}
			else
			{
				string a = Path.GetExtension(FileName).ToLower();
				bool flag4 = a == text;
				if (flag4)
				{
					result = new BinaryFile
					{
						ByteArray = array3,
						FileName = FileName
					};
				}
				else
				{
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
					FileName = fileNameWithoutExtension + text;
					result = new BinaryFile
					{
						ByteArray = array3,
						FileName = FileName
					};
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private AccommodationsDAO adao;
	}
}
