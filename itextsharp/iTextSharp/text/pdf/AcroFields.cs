using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.util;
using System.Xml;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200028A RID: 650
	public class AcroFields
	{
		// Token: 0x06001879 RID: 6265 RVA: 0x0008EDAC File Offset: 0x0008DDAC
		internal AcroFields(PdfReader reader, PdfWriter writer)
		{
			this.reader = reader;
			this.writer = writer;
			this.xfa = new XfaForm(reader);
			if (writer is PdfStamperImp)
			{
				this.append = ((PdfStamperImp)writer).append;
			}
			this.Fill();
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0008EE18 File Offset: 0x0008DE18
		internal void Fill()
		{
			this.fields = new Dictionary<string, AcroFields.Item>();
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(this.reader.Catalog.Get(PdfName.ACROFORM));
			if (pdfDictionary == null)
			{
				return;
			}
			PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.FIELDS));
			if (pdfArray == null || pdfArray.Size == 0)
			{
				return;
			}
			for (int i = 1; i <= this.reader.NumberOfPages; i++)
			{
				PdfDictionary pageNRelease = this.reader.GetPageNRelease(i);
				PdfArray pdfArray2 = (PdfArray)PdfReader.GetPdfObjectRelease(pageNRelease.Get(PdfName.ANNOTS), pageNRelease);
				if (pdfArray2 != null)
				{
					for (int j = 0; j < pdfArray2.Size; j++)
					{
						PdfDictionary asDict = pdfArray2.GetAsDict(j);
						if (asDict == null)
						{
							PdfReader.ReleaseLastXrefPartial(pdfArray2.GetAsIndirectObject(j));
						}
						else if (!PdfName.WIDGET.Equals(asDict.GetAsName(PdfName.SUBTYPE)))
						{
							PdfReader.ReleaseLastXrefPartial(pdfArray2.GetAsIndirectObject(j));
						}
						else
						{
							PdfDictionary pdfDictionary2 = asDict;
							PdfDictionary pdfDictionary3 = new PdfDictionary();
							pdfDictionary3.Merge(asDict);
							string text = "";
							PdfDictionary pdfDictionary4 = null;
							PdfObject pdfObject = null;
							while (asDict != null)
							{
								pdfDictionary3.MergeDifferent(asDict);
								PdfString asString = asDict.GetAsString(PdfName.T);
								if (asString != null)
								{
									text = asString.ToUnicodeString() + "." + text;
								}
								if (pdfObject == null && asDict.Get(PdfName.V) != null)
								{
									pdfObject = PdfReader.GetPdfObjectRelease(asDict.Get(PdfName.V));
								}
								if (pdfDictionary4 == null && asString != null)
								{
									pdfDictionary4 = asDict;
									if (asDict.Get(PdfName.V) == null && pdfObject != null)
									{
										pdfDictionary4.Put(PdfName.V, pdfObject);
									}
								}
								asDict = asDict.GetAsDict(PdfName.PARENT);
							}
							if (text.Length > 0)
							{
								text = text.Substring(0, text.Length - 1);
							}
							AcroFields.Item item;
							if (!this.fields.TryGetValue(text, out item))
							{
								item = new AcroFields.Item();
								this.fields[text] = item;
							}
							if (pdfDictionary4 == null)
							{
								item.AddValue(pdfDictionary2);
							}
							else
							{
								item.AddValue(pdfDictionary4);
							}
							item.AddWidget(pdfDictionary2);
							item.AddWidgetRef(pdfArray2.GetAsIndirectObject(j));
							if (pdfDictionary != null)
							{
								pdfDictionary3.MergeDifferent(pdfDictionary);
							}
							item.AddMerged(pdfDictionary3);
							item.AddPage(i);
							item.AddTabOrder(j);
						}
					}
				}
			}
			PdfNumber asNumber = pdfDictionary.GetAsNumber(PdfName.SIGFLAGS);
			if (asNumber == null || (asNumber.IntValue & 1) != 1)
			{
				return;
			}
			for (int k = 0; k < pdfArray.Size; k++)
			{
				PdfDictionary asDict2 = pdfArray.GetAsDict(k);
				if (asDict2 == null)
				{
					PdfReader.ReleaseLastXrefPartial(pdfArray.GetAsIndirectObject(k));
				}
				else if (!PdfName.WIDGET.Equals(asDict2.GetAsName(PdfName.SUBTYPE)))
				{
					PdfReader.ReleaseLastXrefPartial(pdfArray.GetAsIndirectObject(k));
				}
				else if ((PdfArray)PdfReader.GetPdfObjectRelease(asDict2.Get(PdfName.KIDS)) == null)
				{
					PdfDictionary pdfDictionary5 = new PdfDictionary();
					pdfDictionary5.Merge(asDict2);
					PdfString asString2 = asDict2.GetAsString(PdfName.T);
					if (asString2 != null)
					{
						string key = asString2.ToUnicodeString();
						if (!this.fields.ContainsKey(key))
						{
							AcroFields.Item item2 = new AcroFields.Item();
							this.fields[key] = item2;
							item2.AddValue(pdfDictionary5);
							item2.AddWidget(pdfDictionary5);
							item2.AddWidgetRef(pdfArray.GetAsIndirectObject(k));
							item2.AddMerged(pdfDictionary5);
							item2.AddPage(-1);
							item2.AddTabOrder(-1);
						}
					}
				}
			}
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x0008F1AC File Offset: 0x0008E1AC
		public string[] GetAppearanceStates(string fieldName)
		{
			if (!this.fields.ContainsKey(fieldName))
			{
				return null;
			}
			AcroFields.Item item = this.fields[fieldName];
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PdfDictionary value = item.GetValue(0);
			PdfString asString = value.GetAsString(PdfName.OPT);
			if (asString != null)
			{
				dictionary[asString.ToUnicodeString()] = null;
			}
			else
			{
				PdfArray asArray = value.GetAsArray(PdfName.OPT);
				if (asArray != null)
				{
					for (int i = 0; i < asArray.Size; i++)
					{
						PdfString asString2 = asArray.GetAsString(i);
						if (asString2 != null)
						{
							dictionary[asString2.ToUnicodeString()] = null;
						}
					}
				}
			}
			for (int j = 0; j < item.Size; j++)
			{
				PdfDictionary pdfDictionary = item.GetWidget(j);
				pdfDictionary = pdfDictionary.GetAsDict(PdfName.AP);
				if (pdfDictionary != null)
				{
					pdfDictionary = pdfDictionary.GetAsDict(PdfName.N);
					if (pdfDictionary != null)
					{
						foreach (PdfName pdfName in pdfDictionary.Keys)
						{
							string key = PdfName.DecodeName(pdfName.ToString());
							dictionary[key] = null;
						}
					}
				}
			}
			string[] array = new string[dictionary.Count];
			dictionary.Keys.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0008F304 File Offset: 0x0008E304
		private string[] GetListOption(string fieldName, int idx)
		{
			AcroFields.Item fieldItem = this.GetFieldItem(fieldName);
			if (fieldItem == null)
			{
				return null;
			}
			PdfArray asArray = fieldItem.GetMerged(0).GetAsArray(PdfName.OPT);
			if (asArray == null)
			{
				return null;
			}
			string[] array = new string[asArray.Size];
			for (int i = 0; i < asArray.Size; i++)
			{
				PdfObject directObject = asArray.GetDirectObject(i);
				try
				{
					if (directObject.IsArray())
					{
						directObject = ((PdfArray)directObject).GetDirectObject(idx);
					}
					if (directObject.IsString())
					{
						array[i] = ((PdfString)directObject).ToUnicodeString();
					}
					else
					{
						array[i] = directObject.ToString();
					}
				}
				catch
				{
					array[i] = "";
				}
			}
			return array;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0008F3B8 File Offset: 0x0008E3B8
		public string[] GetListOptionExport(string fieldName)
		{
			return this.GetListOption(fieldName, 0);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0008F3C2 File Offset: 0x0008E3C2
		public string[] GetListOptionDisplay(string fieldName)
		{
			return this.GetListOption(fieldName, 1);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0008F3CC File Offset: 0x0008E3CC
		public bool SetListOption(string fieldName, string[] exportValues, string[] displayValues)
		{
			if (exportValues == null && displayValues == null)
			{
				return false;
			}
			if (exportValues != null && displayValues != null && exportValues.Length != displayValues.Length)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.export.and.the.display.array.must.have.the.same.size"));
			}
			int fieldType = this.GetFieldType(fieldName);
			if (fieldType != 6 && fieldType != 5)
			{
				return false;
			}
			AcroFields.Item item = this.fields[fieldName];
			string[] array = null;
			if (exportValues == null && displayValues != null)
			{
				array = displayValues;
			}
			else if (exportValues != null && displayValues == null)
			{
				array = exportValues;
			}
			PdfArray pdfArray = new PdfArray();
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					pdfArray.Add(new PdfString(array[i], "UnicodeBig"));
				}
			}
			else
			{
				for (int j = 0; j < exportValues.Length; j++)
				{
					PdfArray pdfArray2 = new PdfArray();
					pdfArray2.Add(new PdfString(exportValues[j], "UnicodeBig"));
					pdfArray2.Add(new PdfString(displayValues[j], "UnicodeBig"));
					pdfArray.Add(pdfArray2);
				}
			}
			item.WriteToAll(PdfName.OPT, pdfArray, 5);
			return true;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0008F4C0 File Offset: 0x0008E4C0
		public int GetFieldType(string fieldName)
		{
			AcroFields.Item fieldItem = this.GetFieldItem(fieldName);
			if (fieldItem == null)
			{
				return 0;
			}
			PdfDictionary merged = fieldItem.GetMerged(0);
			PdfName asName = merged.GetAsName(PdfName.FT);
			if (asName == null)
			{
				return 0;
			}
			int num = 0;
			PdfNumber asNumber = merged.GetAsNumber(PdfName.FF);
			if (asNumber != null)
			{
				num = asNumber.IntValue;
			}
			if (PdfName.BTN.Equals(asName))
			{
				if ((num & 65536) != 0)
				{
					return 1;
				}
				if ((num & 32768) != 0)
				{
					return 3;
				}
				return 2;
			}
			else
			{
				if (PdfName.TX.Equals(asName))
				{
					return 4;
				}
				if (PdfName.CH.Equals(asName))
				{
					if ((num & 131072) != 0)
					{
						return 6;
					}
					return 5;
				}
				else
				{
					if (PdfName.SIG.Equals(asName))
					{
						return 7;
					}
					return 0;
				}
			}
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0008F56C File Offset: 0x0008E56C
		public void ExportAsFdf(FdfWriter writer)
		{
			foreach (KeyValuePair<string, AcroFields.Item> keyValuePair in this.fields)
			{
				AcroFields.Item value = keyValuePair.Value;
				string key = keyValuePair.Key;
				PdfObject pdfObject = value.GetMerged(0).Get(PdfName.V);
				if (pdfObject != null)
				{
					string field = this.GetField(key);
					if (this.lastWasString)
					{
						writer.SetFieldAsString(key, field);
					}
					else
					{
						writer.SetFieldAsName(key, field);
					}
				}
			}
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0008F608 File Offset: 0x0008E608
		public bool RenameField(string oldName, string newName)
		{
			int num = oldName.LastIndexOf('.') + 1;
			int num2 = newName.LastIndexOf('.') + 1;
			if (num != num2)
			{
				return false;
			}
			if (!oldName.Substring(0, num).Equals(newName.Substring(0, num2)))
			{
				return false;
			}
			if (this.fields.ContainsKey(newName))
			{
				return false;
			}
			if (!this.fields.ContainsKey(oldName))
			{
				return false;
			}
			AcroFields.Item item = this.fields[oldName];
			newName = newName.Substring(num2);
			PdfString value = new PdfString(newName, "UnicodeBig");
			item.WriteToAll(PdfName.T, value, 5);
			item.MarkUsed(this, 4);
			this.fields.Remove(oldName);
			this.fields[newName] = item;
			return true;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0008F6BC File Offset: 0x0008E6BC
		public static object[] SplitDAelements(string da)
		{
			PRTokeniser prtokeniser = new PRTokeniser(PdfEncodings.ConvertToBytes(da, null));
			List<string> list = new List<string>();
			object[] array = new object[3];
			while (prtokeniser.NextToken())
			{
				if (prtokeniser.TokenType != PRTokeniser.TokType.COMMENT)
				{
					if (prtokeniser.TokenType == PRTokeniser.TokType.OTHER)
					{
						string stringValue = prtokeniser.StringValue;
						if (stringValue.Equals("Tf"))
						{
							if (list.Count >= 2)
							{
								array[0] = list[list.Count - 2];
								array[1] = float.Parse(list[list.Count - 1], NumberFormatInfo.InvariantInfo);
							}
						}
						else if (stringValue.Equals("g"))
						{
							if (list.Count >= 1)
							{
								float num = float.Parse(list[list.Count - 1], NumberFormatInfo.InvariantInfo);
								if (num != 0f)
								{
									array[2] = new GrayColor(num);
								}
							}
						}
						else if (stringValue.Equals("rg"))
						{
							if (list.Count >= 3)
							{
								float red = float.Parse(list[list.Count - 3], NumberFormatInfo.InvariantInfo);
								float green = float.Parse(list[list.Count - 2], NumberFormatInfo.InvariantInfo);
								float blue = float.Parse(list[list.Count - 1], NumberFormatInfo.InvariantInfo);
								array[2] = new BaseColor(red, green, blue);
							}
						}
						else if (stringValue.Equals("k") && list.Count >= 4)
						{
							float floatCyan = float.Parse(list[list.Count - 4], NumberFormatInfo.InvariantInfo);
							float floatMagenta = float.Parse(list[list.Count - 3], NumberFormatInfo.InvariantInfo);
							float floatYellow = float.Parse(list[list.Count - 2], NumberFormatInfo.InvariantInfo);
							float floatBlack = float.Parse(list[list.Count - 1], NumberFormatInfo.InvariantInfo);
							array[2] = new CMYKColor(floatCyan, floatMagenta, floatYellow, floatBlack);
						}
						list.Clear();
					}
					else
					{
						list.Add(prtokeniser.StringValue);
					}
				}
			}
			return array;
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0008F8D0 File Offset: 0x0008E8D0
		public void DecodeGenericDictionary(PdfDictionary merged, BaseField tx)
		{
			PdfString asString = merged.GetAsString(PdfName.DA);
			if (asString != null)
			{
				object[] array = AcroFields.SplitDAelements(asString.ToUnicodeString());
				if (array[1] != null)
				{
					tx.FontSize = (float)array[1];
				}
				if (array[2] != null)
				{
					tx.TextColor = (BaseColor)array[2];
				}
				if (array[0] != null)
				{
					PdfDictionary asDict = merged.GetAsDict(PdfName.DR);
					if (asDict != null)
					{
						asDict = asDict.GetAsDict(PdfName.FONT);
						if (asDict != null)
						{
							PdfObject pdfObject = asDict.Get(new PdfName((string)array[0]));
							if (pdfObject != null && pdfObject.Type == 10)
							{
								PRIndirectReference prindirectReference = (PRIndirectReference)pdfObject;
								BaseFont font = new DocumentFont((PRIndirectReference)pdfObject);
								tx.Font = font;
								int number = prindirectReference.Number;
								BaseFont baseFont;
								this.extensionFonts.TryGetValue(number, out baseFont);
								if (baseFont == null && !this.extensionFonts.ContainsKey(number))
								{
									PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(pdfObject);
									PdfDictionary asDict2 = pdfDictionary.GetAsDict(PdfName.FONTDESCRIPTOR);
									if (asDict2 != null)
									{
										PRStream prstream = (PRStream)PdfReader.GetPdfObject(asDict2.Get(PdfName.FONTFILE2));
										if (prstream == null)
										{
											prstream = (PRStream)PdfReader.GetPdfObject(asDict2.Get(PdfName.FONTFILE3));
										}
										if (prstream == null)
										{
											this.extensionFonts[number] = null;
										}
										else
										{
											try
											{
												baseFont = BaseFont.CreateFont("font.ttf", "Identity-H", true, false, PdfReader.GetStreamBytes(prstream), null);
											}
											catch
											{
											}
											this.extensionFonts[number] = baseFont;
										}
									}
								}
								if (tx is TextField)
								{
									((TextField)tx).ExtensionFont = baseFont;
								}
							}
							else
							{
								BaseFont font2;
								if (!this.localFonts.TryGetValue((string)array[0], out font2))
								{
									string[] array2;
									AcroFields.stdFieldFontNames.TryGetValue((string)array[0], out array2);
									if (array2 == null)
									{
										goto IL_202;
									}
									try
									{
										string encoding = "winansi";
										if (array2.Length > 1)
										{
											encoding = array2[1];
										}
										font2 = BaseFont.CreateFont(array2[0], encoding, false);
										tx.Font = font2;
										goto IL_202;
									}
									catch
									{
										goto IL_202;
									}
								}
								tx.Font = font2;
							}
						}
					}
				}
			}
			IL_202:
			PdfDictionary asDict3 = merged.GetAsDict(PdfName.MK);
			if (asDict3 != null)
			{
				PdfArray asArray = asDict3.GetAsArray(PdfName.BC);
				BaseColor mkcolor = this.GetMKColor(asArray);
				tx.BorderColor = mkcolor;
				if (mkcolor != null)
				{
					tx.BorderWidth = 1f;
				}
				asArray = asDict3.GetAsArray(PdfName.BG);
				tx.BackgroundColor = this.GetMKColor(asArray);
				PdfNumber asNumber = asDict3.GetAsNumber(PdfName.R);
				if (asNumber != null)
				{
					tx.Rotation = asNumber.IntValue;
				}
			}
			PdfNumber asNumber2 = merged.GetAsNumber(PdfName.F);
			tx.Visibility = 2;
			int num;
			if (asNumber2 != null)
			{
				num = asNumber2.IntValue;
				if ((num & 4) != 0 && (num & 2) != 0)
				{
					tx.Visibility = 1;
				}
				else if ((num & 4) != 0 && (num & 32) != 0)
				{
					tx.Visibility = 3;
				}
				else if ((num & 4) != 0)
				{
					tx.Visibility = 0;
				}
			}
			asNumber2 = merged.GetAsNumber(PdfName.FF);
			num = 0;
			if (asNumber2 != null)
			{
				num = asNumber2.IntValue;
			}
			tx.Options = num;
			if ((num & 16777216) != 0)
			{
				PdfNumber asNumber3 = merged.GetAsNumber(PdfName.MAXLEN);
				int maxCharacterLength = 0;
				if (asNumber3 != null)
				{
					maxCharacterLength = asNumber3.IntValue;
				}
				tx.MaxCharacterLength = maxCharacterLength;
			}
			asNumber2 = merged.GetAsNumber(PdfName.Q);
			if (asNumber2 != null)
			{
				if (asNumber2.IntValue == 1)
				{
					tx.Alignment = 1;
				}
				else if (asNumber2.IntValue == 2)
				{
					tx.Alignment = 2;
				}
			}
			PdfDictionary asDict4 = merged.GetAsDict(PdfName.BS);
			if (asDict4 != null)
			{
				PdfNumber asNumber4 = asDict4.GetAsNumber(PdfName.W);
				if (asNumber4 != null)
				{
					tx.BorderWidth = asNumber4.FloatValue;
				}
				PdfName asName = asDict4.GetAsName(PdfName.S);
				if (PdfName.D.Equals(asName))
				{
					tx.BorderStyle = 1;
					return;
				}
				if (PdfName.B.Equals(asName))
				{
					tx.BorderStyle = 2;
					return;
				}
				if (PdfName.I.Equals(asName))
				{
					tx.BorderStyle = 3;
					return;
				}
				if (PdfName.U.Equals(asName))
				{
					tx.BorderStyle = 4;
					return;
				}
			}
			else
			{
				PdfArray asArray2 = merged.GetAsArray(PdfName.BORDER);
				if (asArray2 != null)
				{
					if (asArray2.Size >= 3)
					{
						tx.BorderWidth = asArray2.GetAsNumber(2).FloatValue;
					}
					if (asArray2.Size >= 4)
					{
						tx.BorderStyle = 1;
					}
				}
			}
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x0008FD28 File Offset: 0x0008ED28
		internal PdfAppearance GetAppearance(PdfDictionary merged, string[] values, string fieldName)
		{
			this.topFirst = 0;
			string text = (values.Length > 0) ? values[0] : null;
			TextField textField;
			if (this.fieldCache == null || !this.fieldCache.ContainsKey(fieldName))
			{
				textField = new TextField(this.writer, null, null);
				textField.SetExtraMargin(this.extraMarginLeft, this.extraMarginTop);
				textField.BorderWidth = 0f;
				textField.SubstitutionFonts = this.substitutionFonts;
				this.DecodeGenericDictionary(merged, textField);
				PdfArray asArray = merged.GetAsArray(PdfName.RECT);
				Rectangle rectangle = PdfReader.GetNormalizedRectangle(asArray);
				if (textField.Rotation == 90 || textField.Rotation == 270)
				{
					rectangle = rectangle.Rotate();
				}
				textField.Box = rectangle;
				if (this.fieldCache != null)
				{
					this.fieldCache[fieldName] = textField;
				}
			}
			else
			{
				textField = this.fieldCache[fieldName];
				textField.Writer = this.writer;
			}
			PdfName asName = merged.GetAsName(PdfName.FT);
			if (PdfName.TX.Equals(asName))
			{
				if (values.Length > 0 && values[0] != null)
				{
					textField.Text = values[0];
				}
				return textField.GetAppearance();
			}
			if (!PdfName.CH.Equals(asName))
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("an.appearance.was.requested.without.a.variable.text.field"));
			}
			PdfArray asArray2 = merged.GetAsArray(PdfName.OPT);
			int num = 0;
			PdfNumber asNumber = merged.GetAsNumber(PdfName.FF);
			if (asNumber != null)
			{
				num = asNumber.IntValue;
			}
			if ((num & 131072) != 0 && asArray2 == null)
			{
				textField.Text = text;
				return textField.GetAppearance();
			}
			if (asArray2 != null)
			{
				string[] array = new string[asArray2.Size];
				string[] array2 = new string[asArray2.Size];
				for (int i = 0; i < asArray2.Size; i++)
				{
					PdfObject pdfObject = asArray2[i];
					if (pdfObject.IsString())
					{
						array[i] = (array2[i] = ((PdfString)pdfObject).ToUnicodeString());
					}
					else
					{
						PdfArray pdfArray = (PdfArray)pdfObject;
						array2[i] = pdfArray.GetAsString(0).ToUnicodeString();
						array[i] = pdfArray.GetAsString(1).ToUnicodeString();
					}
				}
				if ((num & 131072) != 0)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (text.Equals(array2[j]))
						{
							text = array[j];
							break;
						}
					}
					textField.Text = text;
					return textField.GetAppearance();
				}
				List<int> list = new List<int>();
				for (int k = 0; k < array2.Length; k++)
				{
					foreach (string text2 in values)
					{
						if (text2 != null && text2.Equals(array2[k]))
						{
							list.Add(k);
							break;
						}
					}
				}
				textField.Choices = array;
				textField.ChoiceExports = array2;
				textField.ChoiceSelections = list;
			}
			PdfAppearance listAppearance = textField.GetListAppearance();
			this.topFirst = textField.TopFirst;
			return listAppearance;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x0008FFFC File Offset: 0x0008EFFC
		internal PdfAppearance GetAppearance(PdfDictionary merged, string text, string fieldName)
		{
			string[] values = new string[]
			{
				text
			};
			return this.GetAppearance(merged, values, fieldName);
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00090020 File Offset: 0x0008F020
		internal BaseColor GetMKColor(PdfArray ar)
		{
			if (ar == null)
			{
				return null;
			}
			switch (ar.Size)
			{
			case 1:
				return new GrayColor(ar.GetAsNumber(0).FloatValue);
			case 3:
				return new BaseColor(ExtendedColor.Normalize(ar.GetAsNumber(0).FloatValue), ExtendedColor.Normalize(ar.GetAsNumber(1).FloatValue), ExtendedColor.Normalize(ar.GetAsNumber(2).FloatValue));
			case 4:
				return new CMYKColor(ar.GetAsNumber(0).FloatValue, ar.GetAsNumber(1).FloatValue, ar.GetAsNumber(2).FloatValue, ar.GetAsNumber(3).FloatValue);
			}
			return null;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000900D8 File Offset: 0x0008F0D8
		public string GetField(string name)
		{
			if (this.xfa.XfaPresent)
			{
				name = this.xfa.FindFieldName(name, this);
				if (name == null)
				{
					return null;
				}
				name = XfaForm.Xml2Som.GetShortName(name);
				return XfaForm.GetNodeText(this.xfa.FindDatasetsNode(name));
			}
			else
			{
				if (!this.fields.ContainsKey(name))
				{
					return null;
				}
				AcroFields.Item item = this.fields[name];
				this.lastWasString = false;
				PdfDictionary merged = item.GetMerged(0);
				PdfObject pdfObject = PdfReader.GetPdfObject(merged.Get(PdfName.V));
				if (pdfObject == null)
				{
					return "";
				}
				if (pdfObject is PRStream)
				{
					byte[] streamBytes = PdfReader.GetStreamBytes((PRStream)pdfObject);
					return PdfEncodings.ConvertToString(streamBytes, "Cp1252");
				}
				PdfName asName = merged.GetAsName(PdfName.FT);
				if (PdfName.BTN.Equals(asName))
				{
					PdfNumber asNumber = merged.GetAsNumber(PdfName.FF);
					int num = 0;
					if (asNumber != null)
					{
						num = asNumber.IntValue;
					}
					if ((num & 65536) != 0)
					{
						return "";
					}
					string text = "";
					if (pdfObject is PdfName)
					{
						text = PdfName.DecodeName(pdfObject.ToString());
					}
					else if (pdfObject is PdfString)
					{
						text = ((PdfString)pdfObject).ToUnicodeString();
					}
					PdfArray asArray = item.GetValue(0).GetAsArray(PdfName.OPT);
					if (asArray != null)
					{
						try
						{
							int idx = int.Parse(text);
							PdfString asString = asArray.GetAsString(idx);
							text = asString.ToUnicodeString();
							this.lastWasString = true;
						}
						catch
						{
						}
					}
					return text;
				}
				else
				{
					if (pdfObject is PdfString)
					{
						this.lastWasString = true;
						return ((PdfString)pdfObject).ToUnicodeString();
					}
					if (pdfObject is PdfName)
					{
						return PdfName.DecodeName(pdfObject.ToString());
					}
					return "";
				}
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x00090290 File Offset: 0x0008F290
		public string[] GetListSelection(string name)
		{
			string field = this.GetField(name);
			string[] array;
			if (field == null)
			{
				array = new string[0];
			}
			else
			{
				array = new string[]
				{
					field
				};
			}
			if (!this.fields.ContainsKey(name))
			{
				return null;
			}
			AcroFields.Item item = this.fields[name];
			PdfArray asArray = item.GetMerged(0).GetAsArray(PdfName.I);
			if (asArray == null)
			{
				return array;
			}
			array = new string[asArray.Size];
			string[] listOptionExport = this.GetListOptionExport(name);
			int num = 0;
			foreach (PdfObject pdfObject in asArray.ArrayList)
			{
				PdfNumber pdfNumber = (PdfNumber)pdfObject;
				array[num++] = listOptionExport[pdfNumber.IntValue];
			}
			return array;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00090368 File Offset: 0x0008F368
		public bool SetFieldProperty(string field, string name, object value, int[] inst)
		{
			if (this.writer == null)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("this.acrofields.instance.is.read.only"));
			}
			if (!this.fields.ContainsKey(field))
			{
				return false;
			}
			AcroFields.Item item = this.fields[field];
			AcroFields.InstHit instHit = new AcroFields.InstHit(inst);
			if (Util.EqualsIgnoreCase(name, "textfont"))
			{
				for (int i = 0; i < item.Size; i++)
				{
					if (instHit.IsHit(i))
					{
						PdfDictionary merged = item.GetMerged(i);
						PdfString asString = merged.GetAsString(PdfName.DA);
						PdfDictionary pdfDictionary = merged.GetAsDict(PdfName.DR);
						if (asString != null && pdfDictionary != null)
						{
							object[] array = AcroFields.SplitDAelements(asString.ToUnicodeString());
							PdfAppearance pdfAppearance = new PdfAppearance();
							if (array[0] != null)
							{
								BaseFont baseFont = (BaseFont)value;
								PdfName pdfName;
								if (!PdfAppearance.stdFieldFontNames.TryGetValue(baseFont.PostscriptFontName, out pdfName))
								{
									pdfName = new PdfName(baseFont.PostscriptFontName);
								}
								PdfDictionary pdfDictionary2 = pdfDictionary.GetAsDict(PdfName.FONT);
								if (pdfDictionary2 == null)
								{
									pdfDictionary2 = new PdfDictionary();
									pdfDictionary.Put(PdfName.FONT, pdfDictionary2);
								}
								PdfIndirectReference pdfIndirectReference = (PdfIndirectReference)pdfDictionary2.Get(pdfName);
								PdfDictionary asDict = this.reader.Catalog.GetAsDict(PdfName.ACROFORM);
								this.MarkUsed(asDict);
								pdfDictionary = asDict.GetAsDict(PdfName.DR);
								if (pdfDictionary == null)
								{
									pdfDictionary = new PdfDictionary();
									asDict.Put(PdfName.DR, pdfDictionary);
								}
								this.MarkUsed(pdfDictionary);
								PdfDictionary pdfDictionary3 = pdfDictionary.GetAsDict(PdfName.FONT);
								if (pdfDictionary3 == null)
								{
									pdfDictionary3 = new PdfDictionary();
									pdfDictionary.Put(PdfName.FONT, pdfDictionary3);
								}
								this.MarkUsed(pdfDictionary3);
								PdfIndirectReference pdfIndirectReference2 = (PdfIndirectReference)pdfDictionary3.Get(pdfName);
								if (pdfIndirectReference2 != null)
								{
									if (pdfIndirectReference == null)
									{
										pdfDictionary2.Put(pdfName, pdfIndirectReference2);
									}
								}
								else if (pdfIndirectReference == null)
								{
									FontDetails fontDetails;
									if (baseFont.FontType == 4)
									{
										fontDetails = new FontDetails(null, ((DocumentFont)baseFont).IndirectReference, baseFont);
									}
									else
									{
										baseFont.Subset = false;
										fontDetails = this.writer.AddSimple(baseFont);
										this.localFonts[pdfName.ToString().Substring(1)] = baseFont;
									}
									pdfDictionary3.Put(pdfName, fontDetails.IndirectReference);
									pdfDictionary2.Put(pdfName, fontDetails.IndirectReference);
								}
								ByteBuffer internalBuffer = pdfAppearance.InternalBuffer;
								internalBuffer.Append(pdfName.GetBytes()).Append(' ').Append((float)array[1]).Append(" Tf ");
								if (array[2] != null)
								{
									pdfAppearance.SetColorFill((BaseColor)array[2]);
								}
								PdfString value2 = new PdfString(pdfAppearance.ToString());
								item.GetMerged(i).Put(PdfName.DA, value2);
								item.GetWidget(i).Put(PdfName.DA, value2);
								this.MarkUsed(item.GetWidget(i));
							}
						}
					}
				}
			}
			else if (Util.EqualsIgnoreCase(name, "textcolor"))
			{
				for (int j = 0; j < item.Size; j++)
				{
					if (instHit.IsHit(j))
					{
						PdfDictionary merged = item.GetMerged(j);
						PdfString asString = merged.GetAsString(PdfName.DA);
						if (asString != null)
						{
							object[] array2 = AcroFields.SplitDAelements(asString.ToUnicodeString());
							PdfAppearance pdfAppearance2 = new PdfAppearance();
							if (array2[0] != null)
							{
								ByteBuffer internalBuffer2 = pdfAppearance2.InternalBuffer;
								internalBuffer2.Append(new PdfName((string)array2[0]).GetBytes()).Append(' ').Append((float)array2[1]).Append(" Tf ");
								pdfAppearance2.SetColorFill((BaseColor)value);
								PdfString value3 = new PdfString(pdfAppearance2.ToString());
								item.GetMerged(j).Put(PdfName.DA, value3);
								item.GetWidget(j).Put(PdfName.DA, value3);
								this.MarkUsed(item.GetWidget(j));
							}
						}
					}
				}
			}
			else if (Util.EqualsIgnoreCase(name, "textsize"))
			{
				for (int k = 0; k < item.Size; k++)
				{
					if (instHit.IsHit(k))
					{
						PdfDictionary merged = item.GetMerged(k);
						PdfString asString = merged.GetAsString(PdfName.DA);
						if (asString != null)
						{
							object[] array3 = AcroFields.SplitDAelements(asString.ToUnicodeString());
							PdfAppearance pdfAppearance3 = new PdfAppearance();
							if (array3[0] != null)
							{
								ByteBuffer internalBuffer3 = pdfAppearance3.InternalBuffer;
								internalBuffer3.Append(new PdfName((string)array3[0]).GetBytes()).Append(' ').Append((float)value).Append(" Tf ");
								if (array3[2] != null)
								{
									pdfAppearance3.SetColorFill((BaseColor)array3[2]);
								}
								PdfString value4 = new PdfString(pdfAppearance3.ToString());
								item.GetMerged(k).Put(PdfName.DA, value4);
								item.GetWidget(k).Put(PdfName.DA, value4);
								this.MarkUsed(item.GetWidget(k));
							}
						}
					}
				}
			}
			else
			{
				if (!Util.EqualsIgnoreCase(name, "bgcolor") && !Util.EqualsIgnoreCase(name, "bordercolor"))
				{
					return false;
				}
				PdfName key = Util.EqualsIgnoreCase(name, "bgcolor") ? PdfName.BG : PdfName.BC;
				for (int l = 0; l < item.Size; l++)
				{
					if (instHit.IsHit(l))
					{
						PdfDictionary merged = item.GetMerged(l);
						PdfDictionary pdfDictionary4 = merged.GetAsDict(PdfName.MK);
						if (pdfDictionary4 == null)
						{
							if (value == null)
							{
								return true;
							}
							pdfDictionary4 = new PdfDictionary();
							item.GetMerged(l).Put(PdfName.MK, pdfDictionary4);
							item.GetWidget(l).Put(PdfName.MK, pdfDictionary4);
							this.MarkUsed(item.GetWidget(l));
						}
						else
						{
							this.MarkUsed(pdfDictionary4);
						}
						if (value == null)
						{
							pdfDictionary4.Remove(key);
						}
						else
						{
							pdfDictionary4.Put(key, PdfAnnotation.GetMKColor((BaseColor)value));
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00090950 File Offset: 0x0008F950
		public bool SetFieldProperty(string field, string name, int value, int[] inst)
		{
			if (this.writer == null)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("this.acrofields.instance.is.read.only"));
			}
			if (!this.fields.ContainsKey(field))
			{
				return false;
			}
			AcroFields.Item item = this.fields[field];
			AcroFields.InstHit instHit = new AcroFields.InstHit(inst);
			if (Util.EqualsIgnoreCase(name, "flags"))
			{
				PdfNumber value2 = new PdfNumber(value);
				for (int i = 0; i < item.Size; i++)
				{
					if (instHit.IsHit(i))
					{
						item.GetMerged(i).Put(PdfName.F, value2);
						item.GetWidget(i).Put(PdfName.F, value2);
						this.MarkUsed(item.GetWidget(i));
					}
				}
			}
			else if (Util.EqualsIgnoreCase(name, "setflags"))
			{
				for (int j = 0; j < item.Size; j++)
				{
					if (instHit.IsHit(j))
					{
						PdfNumber pdfNumber = item.GetWidget(j).GetAsNumber(PdfName.F);
						int num = 0;
						if (pdfNumber != null)
						{
							num = pdfNumber.IntValue;
						}
						pdfNumber = new PdfNumber(num | value);
						item.GetMerged(j).Put(PdfName.F, pdfNumber);
						item.GetWidget(j).Put(PdfName.F, pdfNumber);
						this.MarkUsed(item.GetWidget(j));
					}
				}
			}
			else if (Util.EqualsIgnoreCase(name, "clrflags"))
			{
				for (int k = 0; k < item.Size; k++)
				{
					if (instHit.IsHit(k))
					{
						PdfDictionary widget = item.GetWidget(k);
						PdfNumber pdfNumber2 = widget.GetAsNumber(PdfName.F);
						int num2 = 0;
						if (pdfNumber2 != null)
						{
							num2 = pdfNumber2.IntValue;
						}
						pdfNumber2 = new PdfNumber(num2 & ~value);
						item.GetMerged(k).Put(PdfName.F, pdfNumber2);
						widget.Put(PdfName.F, pdfNumber2);
						this.MarkUsed(widget);
					}
				}
			}
			else if (Util.EqualsIgnoreCase(name, "fflags"))
			{
				PdfNumber value3 = new PdfNumber(value);
				for (int l = 0; l < item.Size; l++)
				{
					if (instHit.IsHit(l))
					{
						item.GetMerged(l).Put(PdfName.FF, value3);
						item.GetValue(l).Put(PdfName.FF, value3);
						this.MarkUsed(item.GetValue(l));
					}
				}
			}
			else if (Util.EqualsIgnoreCase(name, "setfflags"))
			{
				for (int m = 0; m < item.Size; m++)
				{
					if (instHit.IsHit(m))
					{
						PdfDictionary value4 = item.GetValue(m);
						PdfNumber pdfNumber3 = value4.GetAsNumber(PdfName.FF);
						int num3 = 0;
						if (pdfNumber3 != null)
						{
							num3 = pdfNumber3.IntValue;
						}
						pdfNumber3 = new PdfNumber(num3 | value);
						item.GetMerged(m).Put(PdfName.FF, pdfNumber3);
						value4.Put(PdfName.FF, pdfNumber3);
						this.MarkUsed(value4);
					}
				}
			}
			else
			{
				if (!Util.EqualsIgnoreCase(name, "clrfflags"))
				{
					return false;
				}
				for (int n = 0; n < item.Size; n++)
				{
					if (instHit.IsHit(n))
					{
						PdfDictionary value5 = item.GetValue(n);
						PdfNumber pdfNumber4 = value5.GetAsNumber(PdfName.FF);
						int num4 = 0;
						if (pdfNumber4 != null)
						{
							num4 = pdfNumber4.IntValue;
						}
						pdfNumber4 = new PdfNumber(num4 & ~value);
						item.GetMerged(n).Put(PdfName.FF, pdfNumber4);
						value5.Put(PdfName.FF, pdfNumber4);
						this.MarkUsed(value5);
					}
				}
			}
			return true;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00090CB8 File Offset: 0x0008FCB8
		public void MergeXfaData(XmlNode n)
		{
			XfaForm.Xml2SomDatasets xml2SomDatasets = new XfaForm.Xml2SomDatasets(n);
			foreach (string text in xml2SomDatasets.Order)
			{
				string nodeText = XfaForm.GetNodeText(xml2SomDatasets.Name2Node[text]);
				this.SetField(text, nodeText);
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00090D28 File Offset: 0x0008FD28
		public void SetFields(FdfReader fdf)
		{
			Dictionary<string, PdfDictionary> dictionary = fdf.Fields;
			foreach (string name in dictionary.Keys)
			{
				string fieldValue = fdf.GetFieldValue(name);
				if (fieldValue != null)
				{
					this.SetField(name, fieldValue);
				}
			}
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00090D90 File Offset: 0x0008FD90
		public void SetFields(XfdfReader xfdf)
		{
			Dictionary<string, string> dictionary = xfdf.Fields;
			foreach (string name in dictionary.Keys)
			{
				string fieldValue = xfdf.GetFieldValue(name);
				if (fieldValue != null)
				{
					this.SetField(name, fieldValue);
				}
				List<string> listValues = xfdf.GetListValues(name);
				if (listValues != null)
				{
					string[] value = listValues.ToArray();
					this.SetListSelection(fieldValue, value);
				}
			}
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00090E18 File Offset: 0x0008FE18
		public bool RegenerateField(string name)
		{
			string field = this.GetField(name);
			return this.SetField(name, field, field);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00090E36 File Offset: 0x0008FE36
		public bool SetField(string name, string value)
		{
			return this.SetField(name, value, null);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00090E44 File Offset: 0x0008FE44
		public bool SetField(string name, string value, string display)
		{
			if (this.writer == null)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("this.acrofields.instance.is.read.only"));
			}
			if (this.xfa.XfaPresent)
			{
				name = this.xfa.FindFieldName(name, this);
				if (name == null)
				{
					return false;
				}
				string shortName = XfaForm.Xml2Som.GetShortName(name);
				XmlNode xmlNode = this.xfa.FindDatasetsNode(shortName);
				if (xmlNode == null)
				{
					xmlNode = this.xfa.DatasetsSom.InsertNode(this.xfa.DatasetsNode, shortName);
				}
				this.xfa.SetNodeText(xmlNode, value);
			}
			if (!this.fields.ContainsKey(name))
			{
				return false;
			}
			AcroFields.Item item = this.fields[name];
			PdfDictionary merged = item.GetMerged(0);
			PdfName asName = merged.GetAsName(PdfName.FT);
			if (PdfName.TX.Equals(asName))
			{
				PdfNumber asNumber = merged.GetAsNumber(PdfName.MAXLEN);
				int num = 0;
				if (asNumber != null)
				{
					num = asNumber.IntValue;
				}
				if (num > 0)
				{
					value = value.Substring(0, Math.Min(num, value.Length));
				}
			}
			if (display == null)
			{
				display = value;
			}
			if (PdfName.TX.Equals(asName) || PdfName.CH.Equals(asName))
			{
				PdfString value2 = new PdfString(value, "UnicodeBig");
				for (int i = 0; i < item.Size; i++)
				{
					PdfDictionary value3 = item.GetValue(i);
					value3.Put(PdfName.V, value2);
					value3.Remove(PdfName.I);
					this.MarkUsed(value3);
					merged = item.GetMerged(i);
					merged.Remove(PdfName.I);
					merged.Put(PdfName.V, value2);
					PdfDictionary widget = item.GetWidget(i);
					if (this.generateAppearances)
					{
						PdfAppearance appearance = this.GetAppearance(merged, display, name);
						if (PdfName.CH.Equals(asName))
						{
							PdfNumber value4 = new PdfNumber(this.topFirst);
							widget.Put(PdfName.TI, value4);
							merged.Put(PdfName.TI, value4);
						}
						PdfDictionary pdfDictionary = widget.GetAsDict(PdfName.AP);
						if (pdfDictionary == null)
						{
							pdfDictionary = new PdfDictionary();
							widget.Put(PdfName.AP, pdfDictionary);
							merged.Put(PdfName.AP, pdfDictionary);
						}
						pdfDictionary.Put(PdfName.N, appearance.IndirectReference);
						this.writer.ReleaseTemplate(appearance);
					}
					else
					{
						widget.Remove(PdfName.AP);
						merged.Remove(PdfName.AP);
					}
					this.MarkUsed(widget);
				}
				return true;
			}
			if (!PdfName.BTN.Equals(asName))
			{
				return false;
			}
			PdfNumber asNumber2 = item.GetMerged(0).GetAsNumber(PdfName.FF);
			int num2 = 0;
			if (asNumber2 != null)
			{
				num2 = asNumber2.IntValue;
			}
			if ((num2 & 65536) != 0)
			{
				Image instance;
				try
				{
					instance = Image.GetInstance(Convert.FromBase64String(value));
				}
				catch
				{
					return false;
				}
				PushbuttonField newPushbuttonFromField = this.GetNewPushbuttonFromField(name);
				newPushbuttonFromField.Image = instance;
				this.ReplacePushbuttonField(name, newPushbuttonFromField.Field);
				return true;
			}
			PdfName pdfName = new PdfName(value);
			List<string> list = new List<string>();
			PdfArray asArray = item.GetValue(0).GetAsArray(PdfName.OPT);
			if (asArray != null)
			{
				for (int j = 0; j < asArray.Size; j++)
				{
					PdfString asString = asArray.GetAsString(j);
					if (asString != null)
					{
						list.Add(asString.ToUnicodeString());
					}
					else
					{
						list.Add(null);
					}
				}
			}
			int num3 = list.IndexOf(value);
			PdfName pdfName2;
			if (num3 >= 0)
			{
				pdfName2 = new PdfName(num3.ToString());
			}
			else
			{
				pdfName2 = pdfName;
			}
			for (int k = 0; k < item.Size; k++)
			{
				merged = item.GetMerged(k);
				PdfDictionary widget2 = item.GetWidget(k);
				PdfDictionary value5 = item.GetValue(k);
				this.MarkUsed(item.GetValue(k));
				value5.Put(PdfName.V, pdfName2);
				merged.Put(PdfName.V, pdfName2);
				this.MarkUsed(widget2);
				if (this.IsInAP(widget2, pdfName2))
				{
					merged.Put(PdfName.AS, pdfName2);
					widget2.Put(PdfName.AS, pdfName2);
				}
				else
				{
					merged.Put(PdfName.AS, PdfName.Off_);
					widget2.Put(PdfName.AS, PdfName.Off_);
				}
			}
			return true;
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00091274 File Offset: 0x00090274
		public bool SetListSelection(string name, string[] value)
		{
			AcroFields.Item fieldItem = this.GetFieldItem(name);
			if (fieldItem == null)
			{
				return false;
			}
			PdfDictionary merged = fieldItem.GetMerged(0);
			PdfName asName = merged.GetAsName(PdfName.FT);
			if (!PdfName.CH.Equals(asName))
			{
				return false;
			}
			string[] listOptionExport = this.GetListOptionExport(name);
			PdfArray pdfArray = new PdfArray();
			foreach (string value2 in value)
			{
				for (int j = 0; j < listOptionExport.Length; j++)
				{
					if (listOptionExport[j].Equals(value2))
					{
						pdfArray.Add(new PdfNumber(j));
					}
				}
			}
			fieldItem.WriteToAll(PdfName.I, pdfArray, 5);
			PdfArray pdfArray2 = new PdfArray();
			for (int k = 0; k < value.Length; k++)
			{
				pdfArray2.Add(new PdfString(value[k]));
			}
			fieldItem.WriteToAll(PdfName.V, pdfArray2, 5);
			PdfAppearance appearance = this.GetAppearance(merged, value, name);
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(PdfName.N, appearance.IndirectReference);
			fieldItem.WriteToAll(PdfName.AP, pdfDictionary, 3);
			this.writer.ReleaseTemplate(appearance);
			fieldItem.MarkUsed(this, 6);
			return true;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0009139C File Offset: 0x0009039C
		internal bool IsInAP(PdfDictionary dic, PdfName check)
		{
			PdfDictionary asDict = dic.GetAsDict(PdfName.AP);
			if (asDict == null)
			{
				return false;
			}
			PdfDictionary asDict2 = asDict.GetAsDict(PdfName.N);
			return asDict2 != null && asDict2.Get(check) != null;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000913D8 File Offset: 0x000903D8
		public Dictionary<string, AcroFields.Item> Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000913E0 File Offset: 0x000903E0
		public AcroFields.Item GetFieldItem(string name)
		{
			if (this.xfa.XfaPresent)
			{
				name = this.xfa.FindFieldName(name, this);
				if (name == null)
				{
					return null;
				}
			}
			if (!this.fields.ContainsKey(name))
			{
				return null;
			}
			return this.fields[name];
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00091420 File Offset: 0x00090420
		public string GetTranslatedFieldName(string name)
		{
			if (this.xfa.XfaPresent)
			{
				string text = this.xfa.FindFieldName(name, this);
				if (text != null)
				{
					name = text;
				}
			}
			return name;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00091450 File Offset: 0x00090450
		public IList<AcroFields.FieldPosition> GetFieldPositions(string name)
		{
			AcroFields.Item fieldItem = this.GetFieldItem(name);
			if (fieldItem == null)
			{
				return null;
			}
			List<AcroFields.FieldPosition> list = new List<AcroFields.FieldPosition>();
			for (int i = 0; i < fieldItem.Size; i++)
			{
				try
				{
					PdfDictionary widget = fieldItem.GetWidget(i);
					PdfArray asArray = widget.GetAsArray(PdfName.RECT);
					if (asArray != null)
					{
						Rectangle rectangle = PdfReader.GetNormalizedRectangle(asArray);
						int page = fieldItem.GetPage(i);
						int pageRotation = this.reader.GetPageRotation(page);
						AcroFields.FieldPosition fieldPosition = new AcroFields.FieldPosition();
						fieldPosition.page = page;
						if (pageRotation != 0)
						{
							Rectangle pageSize = this.reader.GetPageSize(page);
							int num = pageRotation;
							if (num != 90)
							{
								if (num != 180)
								{
									if (num == 270)
									{
										rectangle = new Rectangle(pageSize.Top - rectangle.Bottom, rectangle.Left, pageSize.Top - rectangle.Top, rectangle.Right);
									}
								}
								else
								{
									rectangle = new Rectangle(pageSize.Right - rectangle.Left, pageSize.Top - rectangle.Bottom, pageSize.Right - rectangle.Right, pageSize.Top - rectangle.Top);
								}
							}
							else
							{
								rectangle = new Rectangle(rectangle.Bottom, pageSize.Right - rectangle.Left, rectangle.Top, pageSize.Right - rectangle.Right);
							}
							rectangle.Normalize();
						}
						fieldPosition.position = rectangle;
						list.Add(fieldPosition);
					}
				}
				catch
				{
				}
			}
			return list;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000915F8 File Offset: 0x000905F8
		private int RemoveRefFromArray(PdfArray array, PdfObject refo)
		{
			if (refo == null || !refo.IsIndirect())
			{
				return array.Size;
			}
			PdfIndirectReference pdfIndirectReference = (PdfIndirectReference)refo;
			for (int i = 0; i < array.Size; i++)
			{
				PdfObject pdfObject = array[i];
				if (pdfObject.IsIndirect() && ((PdfIndirectReference)pdfObject).Number == pdfIndirectReference.Number)
				{
					array.Remove(i--);
				}
			}
			return array.Size;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00091664 File Offset: 0x00090664
		public bool RemoveFieldsFromPage(int page)
		{
			if (page < 1)
			{
				return false;
			}
			string[] array = new string[this.fields.Count];
			this.fields.Keys.CopyTo(array, 0);
			bool flag = false;
			for (int i = 0; i < array.Length; i++)
			{
				bool flag2 = this.RemoveField(array[i], page);
				flag = (flag || flag2);
			}
			return flag;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000916C0 File Offset: 0x000906C0
		public bool RemoveField(string name, int page)
		{
			AcroFields.Item fieldItem = this.GetFieldItem(name);
			if (fieldItem == null)
			{
				return false;
			}
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObject(this.reader.Catalog.Get(PdfName.ACROFORM), this.reader.Catalog);
			if (pdfDictionary == null)
			{
				return false;
			}
			PdfArray asArray = pdfDictionary.GetAsArray(PdfName.FIELDS);
			if (asArray == null)
			{
				return false;
			}
			for (int i = 0; i < fieldItem.Size; i++)
			{
				int page2 = fieldItem.GetPage(i);
				if (page == -1 || page == page2)
				{
					PdfIndirectReference pdfIndirectReference = fieldItem.GetWidgetRef(i);
					PdfDictionary pdfDictionary2 = fieldItem.GetWidget(i);
					PdfDictionary pageN = this.reader.GetPageN(page2);
					PdfArray asArray2 = pageN.GetAsArray(PdfName.ANNOTS);
					if (asArray2 != null)
					{
						if (this.RemoveRefFromArray(asArray2, pdfIndirectReference) == 0)
						{
							pageN.Remove(PdfName.ANNOTS);
							this.MarkUsed(pageN);
						}
						else
						{
							this.MarkUsed(asArray2);
						}
					}
					PdfReader.KillIndirect(pdfIndirectReference);
					PdfIndirectReference refo = pdfIndirectReference;
					while ((pdfIndirectReference = pdfDictionary2.GetAsIndirectObject(PdfName.PARENT)) != null)
					{
						pdfDictionary2 = pdfDictionary2.GetAsDict(PdfName.PARENT);
						PdfArray asArray3 = pdfDictionary2.GetAsArray(PdfName.KIDS);
						if (this.RemoveRefFromArray(asArray3, refo) != 0)
						{
							break;
						}
						refo = pdfIndirectReference;
						PdfReader.KillIndirect(pdfIndirectReference);
					}
					if (pdfIndirectReference == null)
					{
						this.RemoveRefFromArray(asArray, refo);
						this.MarkUsed(asArray);
					}
					if (page != -1)
					{
						fieldItem.Remove(i);
						i--;
					}
				}
			}
			if (page == -1 || fieldItem.Size == 0)
			{
				this.fields.Remove(name);
			}
			return true;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00091835 File Offset: 0x00090835
		public bool RemoveField(string name)
		{
			return this.RemoveField(name, -1);
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0009188E File Offset: 0x0009088E
		// (set) Token: 0x0600189C RID: 6300 RVA: 0x00091840 File Offset: 0x00090840
		public bool GenerateAppearances
		{
			get
			{
				return this.generateAppearances;
			}
			set
			{
				this.generateAppearances = value;
				PdfDictionary asDict = this.reader.Catalog.GetAsDict(PdfName.ACROFORM);
				if (this.generateAppearances)
				{
					asDict.Remove(PdfName.NEEDAPPEARANCES);
					return;
				}
				asDict.Put(PdfName.NEEDAPPEARANCES, PdfBoolean.PDFTRUE);
			}
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00091898 File Offset: 0x00090898
		private void FindSignatureNames()
		{
			if (this.sigNames != null)
			{
				return;
			}
			this.sigNames = new Dictionary<string, int[]>();
			List<object[]> list = new List<object[]>();
			foreach (KeyValuePair<string, AcroFields.Item> keyValuePair in this.fields)
			{
				AcroFields.Item value = keyValuePair.Value;
				PdfDictionary merged = value.GetMerged(0);
				if (PdfName.SIG.Equals(merged.Get(PdfName.FT)))
				{
					PdfDictionary asDict = merged.GetAsDict(PdfName.V);
					if (asDict != null)
					{
						PdfString asString = asDict.GetAsString(PdfName.CONTENTS);
						if (asString != null)
						{
							PdfArray asArray = asDict.GetAsArray(PdfName.BYTERANGE);
							if (asArray != null)
							{
								int size = asArray.Size;
								if (size >= 2)
								{
									int num = asArray.GetAsNumber(size - 1).IntValue + asArray.GetAsNumber(size - 2).IntValue;
									List<object[]> list2 = list;
									object[] array = new object[2];
									array[0] = keyValuePair.Key;
									object[] array2 = array;
									int num2 = 1;
									int[] array3 = new int[2];
									array3[0] = num;
									array2[num2] = array3;
									list2.Add(array);
								}
							}
						}
					}
				}
			}
			list.Sort(new AcroFields.ISorterComparator());
			if (list.Count > 0)
			{
				if (((int[])list[list.Count - 1][1])[0] == this.reader.FileLength)
				{
					this.totalRevisions = list.Count;
				}
				else
				{
					this.totalRevisions = list.Count + 1;
				}
				for (int i = 0; i < list.Count; i++)
				{
					object[] array4 = list[i];
					string key = (string)array4[0];
					int[] array5 = (int[])array4[1];
					array5[1] = i + 1;
					this.sigNames[key] = array5;
				}
			}
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x00091A6C File Offset: 0x00090A6C
		public List<string> GetSignatureNames()
		{
			this.FindSignatureNames();
			return new List<string>(this.sigNames.Keys);
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00091A84 File Offset: 0x00090A84
		public List<string> GetBlankSignatureNames()
		{
			this.FindSignatureNames();
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, AcroFields.Item> keyValuePair in this.fields)
			{
				AcroFields.Item value = keyValuePair.Value;
				PdfDictionary merged = value.GetMerged(0);
				if (PdfName.SIG.Equals(merged.GetAsName(PdfName.FT)) && !this.sigNames.ContainsKey(keyValuePair.Key))
				{
					list.Add(keyValuePair.Key);
				}
			}
			return list;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00091B28 File Offset: 0x00090B28
		public PdfDictionary GetSignatureDictionary(string name)
		{
			this.FindSignatureNames();
			name = this.GetTranslatedFieldName(name);
			if (!this.sigNames.ContainsKey(name))
			{
				return null;
			}
			AcroFields.Item item = this.fields[name];
			PdfDictionary merged = item.GetMerged(0);
			return merged.GetAsDict(PdfName.V);
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00091B74 File Offset: 0x00090B74
		public bool SignatureCoversWholeDocument(string name)
		{
			this.FindSignatureNames();
			name = this.GetTranslatedFieldName(name);
			return this.sigNames.ContainsKey(name) && this.sigNames[name][0] == this.reader.FileLength;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00091BB0 File Offset: 0x00090BB0
		public PdfPKCS7 VerifySignature(string name)
		{
			PdfDictionary signatureDictionary = this.GetSignatureDictionary(name);
			if (signatureDictionary == null)
			{
				return null;
			}
			PdfName asName = signatureDictionary.GetAsName(PdfName.SUBFILTER);
			PdfString asString = signatureDictionary.GetAsString(PdfName.CONTENTS);
			PdfPKCS7 pdfPKCS;
			if (asName.Equals(PdfName.ADBE_X509_RSA_SHA1))
			{
				PdfString asString2 = signatureDictionary.GetAsString(PdfName.CERT);
				pdfPKCS = new PdfPKCS7(asString.GetOriginalBytes(), asString2.GetBytes());
			}
			else
			{
				pdfPKCS = new PdfPKCS7(asString.GetOriginalBytes());
			}
			this.UpdateByteRange(pdfPKCS, signatureDictionary);
			PdfString asString3 = signatureDictionary.GetAsString(PdfName.M);
			if (asString3 != null)
			{
				pdfPKCS.SignDate = PdfDate.Decode(asString3.ToString());
			}
			PdfObject pdfObject = PdfReader.GetPdfObject(signatureDictionary.Get(PdfName.NAME));
			if (pdfObject != null)
			{
				if (pdfObject.IsString())
				{
					pdfPKCS.SignName = ((PdfString)pdfObject).ToUnicodeString();
				}
				else if (pdfObject.IsName())
				{
					pdfPKCS.SignName = PdfName.DecodeName(pdfObject.ToString());
				}
			}
			asString3 = signatureDictionary.GetAsString(PdfName.REASON);
			if (asString3 != null)
			{
				pdfPKCS.Reason = asString3.ToUnicodeString();
			}
			asString3 = signatureDictionary.GetAsString(PdfName.LOCATION);
			if (asString3 != null)
			{
				pdfPKCS.Location = asString3.ToUnicodeString();
			}
			return pdfPKCS;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00091CD8 File Offset: 0x00090CD8
		private void UpdateByteRange(PdfPKCS7 pkcs7, PdfDictionary v)
		{
			PdfArray asArray = v.GetAsArray(PdfName.BYTERANGE);
			RandomAccessFileOrArray safeFile = this.reader.SafeFile;
			try
			{
				safeFile.ReOpen();
				byte[] array = new byte[8192];
				for (int i = 0; i < asArray.Size; i++)
				{
					int intValue = asArray.GetAsNumber(i).IntValue;
					int j = asArray.GetAsNumber(++i).IntValue;
					safeFile.Seek(intValue);
					while (j > 0)
					{
						int num = safeFile.Read(array, 0, Math.Min(j, array.Length));
						if (num <= 0)
						{
							break;
						}
						j -= num;
						pkcs7.Update(array, 0, num);
					}
				}
			}
			finally
			{
				try
				{
					safeFile.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00091DA4 File Offset: 0x00090DA4
		public int TotalRevisions
		{
			get
			{
				this.FindSignatureNames();
				return this.totalRevisions;
			}
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00091DB2 File Offset: 0x00090DB2
		public int GetRevision(string field)
		{
			this.FindSignatureNames();
			field = this.GetTranslatedFieldName(field);
			if (!this.sigNames.ContainsKey(field))
			{
				return 0;
			}
			return this.sigNames[field][1];
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00091DE4 File Offset: 0x00090DE4
		public Stream ExtractRevision(string field)
		{
			this.FindSignatureNames();
			field = this.GetTranslatedFieldName(field);
			if (!this.sigNames.ContainsKey(field))
			{
				return null;
			}
			int length = this.sigNames[field][0];
			RandomAccessFileOrArray safeFile = this.reader.SafeFile;
			safeFile.ReOpen();
			safeFile.Seek(0);
			return new AcroFields.RevisionStream(safeFile, length);
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00091E48 File Offset: 0x00090E48
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x00091E3F File Offset: 0x00090E3F
		public IDictionary<string, TextField> FieldCache
		{
			get
			{
				return this.fieldCache;
			}
			set
			{
				this.fieldCache = value;
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00091E50 File Offset: 0x00090E50
		private void MarkUsed(PdfObject obj)
		{
			if (!this.append)
			{
				return;
			}
			((PdfStamperImp)this.writer).MarkUsed(obj);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00091E6C File Offset: 0x00090E6C
		public void SetExtraMargin(float extraMarginLeft, float extraMarginTop)
		{
			this.extraMarginLeft = extraMarginLeft;
			this.extraMarginTop = extraMarginTop;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00091E7C File Offset: 0x00090E7C
		public void AddSubstitutionFont(BaseFont font)
		{
			if (this.substitutionFonts == null)
			{
				this.substitutionFonts = new List<BaseFont>();
			}
			this.substitutionFonts.Add(font);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00091EA0 File Offset: 0x00090EA0
		static AcroFields()
		{
			AcroFields.stdFieldFontNames["CoBO"] = new string[]
			{
				"Courier-BoldOblique"
			};
			AcroFields.stdFieldFontNames["CoBo"] = new string[]
			{
				"Courier-Bold"
			};
			AcroFields.stdFieldFontNames["CoOb"] = new string[]
			{
				"Courier-Oblique"
			};
			AcroFields.stdFieldFontNames["Cour"] = new string[]
			{
				"Courier"
			};
			AcroFields.stdFieldFontNames["HeBO"] = new string[]
			{
				"Helvetica-BoldOblique"
			};
			AcroFields.stdFieldFontNames["HeBo"] = new string[]
			{
				"Helvetica-Bold"
			};
			AcroFields.stdFieldFontNames["HeOb"] = new string[]
			{
				"Helvetica-Oblique"
			};
			AcroFields.stdFieldFontNames["Helv"] = new string[]
			{
				"Helvetica"
			};
			AcroFields.stdFieldFontNames["Symb"] = new string[]
			{
				"Symbol"
			};
			AcroFields.stdFieldFontNames["TiBI"] = new string[]
			{
				"Times-BoldItalic"
			};
			AcroFields.stdFieldFontNames["TiBo"] = new string[]
			{
				"Times-Bold"
			};
			AcroFields.stdFieldFontNames["TiIt"] = new string[]
			{
				"Times-Italic"
			};
			AcroFields.stdFieldFontNames["TiRo"] = new string[]
			{
				"Times-Roman"
			};
			AcroFields.stdFieldFontNames["ZaDb"] = new string[]
			{
				"ZapfDingbats"
			};
			AcroFields.stdFieldFontNames["HySm"] = new string[]
			{
				"HYSMyeongJo-Medium",
				"UniKS-UCS2-H"
			};
			AcroFields.stdFieldFontNames["HyGo"] = new string[]
			{
				"HYGoThic-Medium",
				"UniKS-UCS2-H"
			};
			AcroFields.stdFieldFontNames["KaGo"] = new string[]
			{
				"HeiseiKakuGo-W5",
				"UniKS-UCS2-H"
			};
			AcroFields.stdFieldFontNames["KaMi"] = new string[]
			{
				"HeiseiMin-W3",
				"UniJIS-UCS2-H"
			};
			AcroFields.stdFieldFontNames["MHei"] = new string[]
			{
				"MHei-Medium",
				"UniCNS-UCS2-H"
			};
			AcroFields.stdFieldFontNames["MSun"] = new string[]
			{
				"MSung-Light",
				"UniCNS-UCS2-H"
			};
			AcroFields.stdFieldFontNames["STSo"] = new string[]
			{
				"STSong-Light",
				"UniGB-UCS2-H"
			};
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x000921FD File Offset: 0x000911FD
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x000921F4 File Offset: 0x000911F4
		public List<BaseFont> SubstitutionFonts
		{
			get
			{
				return this.substitutionFonts;
			}
			set
			{
				this.substitutionFonts = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00092205 File Offset: 0x00091205
		public XfaForm Xfa
		{
			get
			{
				return this.xfa;
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00092210 File Offset: 0x00091210
		public void RemoveXfa()
		{
			PdfDictionary catalog = this.reader.Catalog;
			PdfDictionary asDict = catalog.GetAsDict(PdfName.ACROFORM);
			asDict.Remove(PdfName.XFA);
			this.xfa = new XfaForm(this.reader);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00092251 File Offset: 0x00091251
		public PushbuttonField GetNewPushbuttonFromField(string field)
		{
			return this.GetNewPushbuttonFromField(field, 0);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0009225C File Offset: 0x0009125C
		public PushbuttonField GetNewPushbuttonFromField(string field, int order)
		{
			if (this.GetFieldType(field) != 1)
			{
				return null;
			}
			AcroFields.Item fieldItem = this.GetFieldItem(field);
			if (order >= fieldItem.Size)
			{
				return null;
			}
			IList<AcroFields.FieldPosition> fieldPositions = this.GetFieldPositions(field);
			Rectangle position = fieldPositions[order].position;
			PushbuttonField pushbuttonField = new PushbuttonField(this.writer, position, null);
			PdfDictionary merged = fieldItem.GetMerged(order);
			this.DecodeGenericDictionary(merged, pushbuttonField);
			PdfDictionary asDict = merged.GetAsDict(PdfName.MK);
			if (asDict != null)
			{
				PdfString asString = asDict.GetAsString(PdfName.CA);
				if (asString != null)
				{
					pushbuttonField.Text = asString.ToUnicodeString();
				}
				PdfNumber asNumber = asDict.GetAsNumber(PdfName.TP);
				if (asNumber != null)
				{
					pushbuttonField.Layout = asNumber.IntValue + 1;
				}
				PdfDictionary asDict2 = asDict.GetAsDict(PdfName.IF);
				if (asDict2 != null)
				{
					PdfName asName = asDict2.GetAsName(PdfName.SW);
					if (asName != null)
					{
						int scaleIcon = 1;
						if (asName.Equals(PdfName.B))
						{
							scaleIcon = 3;
						}
						else if (asName.Equals(PdfName.S))
						{
							scaleIcon = 4;
						}
						else if (asName.Equals(PdfName.N))
						{
							scaleIcon = 2;
						}
						pushbuttonField.ScaleIcon = scaleIcon;
					}
					asName = asDict2.GetAsName(PdfName.S);
					if (asName != null && asName.Equals(PdfName.A))
					{
						pushbuttonField.ProportionalIcon = false;
					}
					PdfArray asArray = asDict2.GetAsArray(PdfName.A);
					if (asArray != null && asArray.Size == 2)
					{
						float floatValue = asArray.GetAsNumber(0).FloatValue;
						float floatValue2 = asArray.GetAsNumber(1).FloatValue;
						pushbuttonField.IconHorizontalAdjustment = floatValue;
						pushbuttonField.IconVerticalAdjustment = floatValue2;
					}
					PdfBoolean asBoolean = asDict2.GetAsBoolean(PdfName.FB);
					if (asBoolean != null && asBoolean.BooleanValue)
					{
						pushbuttonField.IconFitToBounds = true;
					}
				}
				PdfObject pdfObject = asDict.Get(PdfName.I);
				if (pdfObject != null && pdfObject.IsIndirect())
				{
					pushbuttonField.IconReference = (PRIndirectReference)pdfObject;
				}
			}
			return pushbuttonField;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00092437 File Offset: 0x00091437
		public bool ReplacePushbuttonField(string field, PdfFormField button)
		{
			return this.ReplacePushbuttonField(field, button, 0);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00092444 File Offset: 0x00091444
		public bool ReplacePushbuttonField(string field, PdfFormField button, int order)
		{
			if (this.GetFieldType(field) != 1)
			{
				return false;
			}
			AcroFields.Item fieldItem = this.GetFieldItem(field);
			if (order >= fieldItem.Size)
			{
				return false;
			}
			PdfDictionary merged = fieldItem.GetMerged(order);
			PdfDictionary value = fieldItem.GetValue(order);
			PdfDictionary widget = fieldItem.GetWidget(order);
			for (int i = 0; i < AcroFields.buttonRemove.Length; i++)
			{
				merged.Remove(AcroFields.buttonRemove[i]);
				value.Remove(AcroFields.buttonRemove[i]);
				widget.Remove(AcroFields.buttonRemove[i]);
			}
			foreach (PdfName pdfName in button.Keys)
			{
				if (!pdfName.Equals(PdfName.T) && !pdfName.Equals(PdfName.RECT))
				{
					if (pdfName.Equals(PdfName.FF))
					{
						value.Put(pdfName, button.Get(pdfName));
					}
					else
					{
						widget.Put(pdfName, button.Get(pdfName));
					}
					merged.Put(pdfName, button.Get(pdfName));
				}
			}
			return true;
		}

		// Token: 0x0400109B RID: 4251
		public const int DA_FONT = 0;

		// Token: 0x0400109C RID: 4252
		public const int DA_SIZE = 1;

		// Token: 0x0400109D RID: 4253
		public const int DA_COLOR = 2;

		// Token: 0x0400109E RID: 4254
		public const int FIELD_TYPE_NONE = 0;

		// Token: 0x0400109F RID: 4255
		public const int FIELD_TYPE_PUSHBUTTON = 1;

		// Token: 0x040010A0 RID: 4256
		public const int FIELD_TYPE_CHECKBOX = 2;

		// Token: 0x040010A1 RID: 4257
		public const int FIELD_TYPE_RADIOBUTTON = 3;

		// Token: 0x040010A2 RID: 4258
		public const int FIELD_TYPE_TEXT = 4;

		// Token: 0x040010A3 RID: 4259
		public const int FIELD_TYPE_LIST = 5;

		// Token: 0x040010A4 RID: 4260
		public const int FIELD_TYPE_COMBO = 6;

		// Token: 0x040010A5 RID: 4261
		public const int FIELD_TYPE_SIGNATURE = 7;

		// Token: 0x040010A6 RID: 4262
		internal PdfReader reader;

		// Token: 0x040010A7 RID: 4263
		internal PdfWriter writer;

		// Token: 0x040010A8 RID: 4264
		internal Dictionary<string, AcroFields.Item> fields;

		// Token: 0x040010A9 RID: 4265
		private int topFirst;

		// Token: 0x040010AA RID: 4266
		private Dictionary<string, int[]> sigNames;

		// Token: 0x040010AB RID: 4267
		private bool append;

		// Token: 0x040010AC RID: 4268
		private Dictionary<int, BaseFont> extensionFonts = new Dictionary<int, BaseFont>();

		// Token: 0x040010AD RID: 4269
		private XfaForm xfa;

		// Token: 0x040010AE RID: 4270
		private bool lastWasString;

		// Token: 0x040010AF RID: 4271
		private bool generateAppearances = true;

		// Token: 0x040010B0 RID: 4272
		private Dictionary<string, BaseFont> localFonts = new Dictionary<string, BaseFont>();

		// Token: 0x040010B1 RID: 4273
		private float extraMarginLeft;

		// Token: 0x040010B2 RID: 4274
		private float extraMarginTop;

		// Token: 0x040010B3 RID: 4275
		private List<BaseFont> substitutionFonts;

		// Token: 0x040010B4 RID: 4276
		private static Dictionary<string, string[]> stdFieldFontNames = new Dictionary<string, string[]>();

		// Token: 0x040010B5 RID: 4277
		private IDictionary<string, TextField> fieldCache;

		// Token: 0x040010B6 RID: 4278
		private int totalRevisions;

		// Token: 0x040010B7 RID: 4279
		private static readonly PdfName[] buttonRemove = new PdfName[]
		{
			PdfName.MK,
			PdfName.F,
			PdfName.FF,
			PdfName.Q,
			PdfName.BS,
			PdfName.BORDER
		};

		// Token: 0x0200028B RID: 651
		public class Item
		{
			// Token: 0x060018B6 RID: 6326 RVA: 0x00092568 File Offset: 0x00091568
			public void WriteToAll(PdfName key, PdfObject value, int writeFlags)
			{
				if ((writeFlags & 1) != 0)
				{
					for (int i = 0; i < this.merged.Count; i++)
					{
						PdfDictionary pdfDictionary = this.GetMerged(i);
						pdfDictionary.Put(key, value);
					}
				}
				if ((writeFlags & 2) != 0)
				{
					for (int i = 0; i < this.widgets.Count; i++)
					{
						PdfDictionary pdfDictionary = this.GetWidget(i);
						pdfDictionary.Put(key, value);
					}
				}
				if ((writeFlags & 4) != 0)
				{
					for (int i = 0; i < this.values.Count; i++)
					{
						PdfDictionary pdfDictionary = this.GetValue(i);
						pdfDictionary.Put(key, value);
					}
				}
			}

			// Token: 0x060018B7 RID: 6327 RVA: 0x000925F8 File Offset: 0x000915F8
			public void MarkUsed(AcroFields parentFields, int writeFlags)
			{
				if ((writeFlags & 4) != 0)
				{
					for (int i = 0; i < this.Size; i++)
					{
						parentFields.MarkUsed(this.GetValue(i));
					}
				}
				if ((writeFlags & 2) != 0)
				{
					for (int j = 0; j < this.Size; j++)
					{
						parentFields.MarkUsed(this.GetWidget(j));
					}
				}
			}

			// Token: 0x17000480 RID: 1152
			// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0009264B File Offset: 0x0009164B
			public int Size
			{
				get
				{
					return this.values.Count;
				}
			}

			// Token: 0x060018B9 RID: 6329 RVA: 0x00092658 File Offset: 0x00091658
			internal void Remove(int killIdx)
			{
				this.values.RemoveAt(killIdx);
				this.widgets.RemoveAt(killIdx);
				this.widget_refs.RemoveAt(killIdx);
				this.merged.RemoveAt(killIdx);
				this.page.RemoveAt(killIdx);
				this.tabOrder.RemoveAt(killIdx);
			}

			// Token: 0x060018BA RID: 6330 RVA: 0x000926AD File Offset: 0x000916AD
			public PdfDictionary GetValue(int idx)
			{
				return this.values[idx];
			}

			// Token: 0x060018BB RID: 6331 RVA: 0x000926BB File Offset: 0x000916BB
			internal void AddValue(PdfDictionary value)
			{
				this.values.Add(value);
			}

			// Token: 0x060018BC RID: 6332 RVA: 0x000926C9 File Offset: 0x000916C9
			public PdfDictionary GetWidget(int idx)
			{
				return this.widgets[idx];
			}

			// Token: 0x060018BD RID: 6333 RVA: 0x000926D7 File Offset: 0x000916D7
			internal void AddWidget(PdfDictionary widget)
			{
				this.widgets.Add(widget);
			}

			// Token: 0x060018BE RID: 6334 RVA: 0x000926E5 File Offset: 0x000916E5
			public PdfIndirectReference GetWidgetRef(int idx)
			{
				return this.widget_refs[idx];
			}

			// Token: 0x060018BF RID: 6335 RVA: 0x000926F3 File Offset: 0x000916F3
			internal void AddWidgetRef(PdfIndirectReference widgRef)
			{
				this.widget_refs.Add(widgRef);
			}

			// Token: 0x060018C0 RID: 6336 RVA: 0x00092701 File Offset: 0x00091701
			public PdfDictionary GetMerged(int idx)
			{
				return this.merged[idx];
			}

			// Token: 0x060018C1 RID: 6337 RVA: 0x0009270F File Offset: 0x0009170F
			internal void AddMerged(PdfDictionary mergeDict)
			{
				this.merged.Add(mergeDict);
			}

			// Token: 0x060018C2 RID: 6338 RVA: 0x0009271D File Offset: 0x0009171D
			public int GetPage(int idx)
			{
				return this.page[idx];
			}

			// Token: 0x060018C3 RID: 6339 RVA: 0x0009272B File Offset: 0x0009172B
			internal void AddPage(int pg)
			{
				this.page.Add(pg);
			}

			// Token: 0x060018C4 RID: 6340 RVA: 0x00092739 File Offset: 0x00091739
			internal void ForcePage(int idx, int pg)
			{
				this.page[idx] = pg;
			}

			// Token: 0x060018C5 RID: 6341 RVA: 0x00092748 File Offset: 0x00091748
			public int GetTabOrder(int idx)
			{
				return this.tabOrder[idx];
			}

			// Token: 0x060018C6 RID: 6342 RVA: 0x00092756 File Offset: 0x00091756
			internal void AddTabOrder(int order)
			{
				this.tabOrder.Add(order);
			}

			// Token: 0x040010B8 RID: 4280
			public const int WRITE_MERGED = 1;

			// Token: 0x040010B9 RID: 4281
			public const int WRITE_WIDGET = 2;

			// Token: 0x040010BA RID: 4282
			public const int WRITE_VALUE = 4;

			// Token: 0x040010BB RID: 4283
			protected internal List<PdfDictionary> values = new List<PdfDictionary>();

			// Token: 0x040010BC RID: 4284
			protected internal List<PdfDictionary> widgets = new List<PdfDictionary>();

			// Token: 0x040010BD RID: 4285
			protected internal List<PdfIndirectReference> widget_refs = new List<PdfIndirectReference>();

			// Token: 0x040010BE RID: 4286
			protected internal List<PdfDictionary> merged = new List<PdfDictionary>();

			// Token: 0x040010BF RID: 4287
			protected internal List<int> page = new List<int>();

			// Token: 0x040010C0 RID: 4288
			protected internal List<int> tabOrder = new List<int>();
		}

		// Token: 0x0200028C RID: 652
		private class InstHit
		{
			// Token: 0x060018C8 RID: 6344 RVA: 0x000927BC File Offset: 0x000917BC
			public InstHit(int[] inst)
			{
				if (inst == null)
				{
					return;
				}
				this.hits = new IntHashtable();
				for (int i = 0; i < inst.Length; i++)
				{
					this.hits[inst[i]] = 1;
				}
			}

			// Token: 0x060018C9 RID: 6345 RVA: 0x000927FB File Offset: 0x000917FB
			public bool IsHit(int n)
			{
				return this.hits == null || this.hits.ContainsKey(n);
			}

			// Token: 0x040010C1 RID: 4289
			private IntHashtable hits;
		}

		// Token: 0x0200028D RID: 653
		public class RevisionStream : Stream
		{
			// Token: 0x060018CA RID: 6346 RVA: 0x00092813 File Offset: 0x00091813
			internal RevisionStream(RandomAccessFileOrArray raf, int length)
			{
				this.raf = raf;
				this.length = length;
			}

			// Token: 0x060018CB RID: 6347 RVA: 0x00092838 File Offset: 0x00091838
			public override int ReadByte()
			{
				int num = this.Read(this.b, 0, 1);
				if (num != 1)
				{
					return -1;
				}
				return (int)(this.b[0] & byte.MaxValue);
			}

			// Token: 0x060018CC RID: 6348 RVA: 0x00092868 File Offset: 0x00091868
			public override int Read(byte[] b, int off, int len)
			{
				if (b == null)
				{
					throw new ArgumentNullException();
				}
				if (off < 0 || off > b.Length || len < 0 || off + len > b.Length || off + len < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (len == 0)
				{
					return 0;
				}
				if (this.rangePosition >= this.length)
				{
					this.Close();
					return -1;
				}
				int num = Math.Min(len, this.length - this.rangePosition);
				this.raf.ReadFully(b, off, num);
				this.rangePosition += num;
				return num;
			}

			// Token: 0x060018CD RID: 6349 RVA: 0x000928EC File Offset: 0x000918EC
			public override void Close()
			{
				if (!this.closed)
				{
					this.raf.Close();
					this.closed = true;
				}
			}

			// Token: 0x17000481 RID: 1153
			// (get) Token: 0x060018CE RID: 6350 RVA: 0x00092908 File Offset: 0x00091908
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000482 RID: 1154
			// (get) Token: 0x060018CF RID: 6351 RVA: 0x0009290B File Offset: 0x0009190B
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000483 RID: 1155
			// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0009290E File Offset: 0x0009190E
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x060018D1 RID: 6353 RVA: 0x00092911 File Offset: 0x00091911
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00092915 File Offset: 0x00091915
			// (set) Token: 0x060018D3 RID: 6355 RVA: 0x00092919 File Offset: 0x00091919
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x060018D4 RID: 6356 RVA: 0x0009291B File Offset: 0x0009191B
			public override void Flush()
			{
			}

			// Token: 0x060018D5 RID: 6357 RVA: 0x0009291D File Offset: 0x0009191D
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x060018D6 RID: 6358 RVA: 0x00092921 File Offset: 0x00091921
			public override void SetLength(long value)
			{
			}

			// Token: 0x060018D7 RID: 6359 RVA: 0x00092923 File Offset: 0x00091923
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x040010C2 RID: 4290
			private byte[] b = new byte[1];

			// Token: 0x040010C3 RID: 4291
			private RandomAccessFileOrArray raf;

			// Token: 0x040010C4 RID: 4292
			private int length;

			// Token: 0x040010C5 RID: 4293
			private int rangePosition;

			// Token: 0x040010C6 RID: 4294
			private bool closed;
		}

		// Token: 0x0200028E RID: 654
		private class ISorterComparator : IComparer<object[]>
		{
			// Token: 0x060018D8 RID: 6360 RVA: 0x00092928 File Offset: 0x00091928
			public int Compare(object[] o1, object[] o2)
			{
				int num = ((int[])o1[1])[0];
				int num2 = ((int[])o2[1])[0];
				return num - num2;
			}
		}

		// Token: 0x0200028F RID: 655
		public class FieldPosition
		{
			// Token: 0x040010C7 RID: 4295
			public int page;

			// Token: 0x040010C8 RID: 4296
			public Rectangle position;
		}
	}
}
