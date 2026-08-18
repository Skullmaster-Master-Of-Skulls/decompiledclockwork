using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000321 RID: 801
	public sealed class SimpleBookmark : ISimpleXMLDocHandler
	{
		// Token: 0x06001D1B RID: 7451 RVA: 0x000ADA04 File Offset: 0x000ACA04
		private SimpleBookmark()
		{
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000ADA18 File Offset: 0x000ACA18
		private static IList<Dictionary<string, object>> BookmarkDepth(PdfReader reader, PdfDictionary outline, IntHashtable pages)
		{
			List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
			while (outline != null)
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				PdfString pdfString = (PdfString)PdfReader.GetPdfObjectRelease(outline.Get(PdfName.TITLE));
				dictionary["Title"] = pdfString.ToUnicodeString();
				PdfArray pdfArray = (PdfArray)PdfReader.GetPdfObjectRelease(outline.Get(PdfName.C));
				if (pdfArray != null && pdfArray.Size == 3)
				{
					ByteBuffer byteBuffer = new ByteBuffer();
					byteBuffer.Append(pdfArray.GetAsNumber(0).FloatValue).Append(' ');
					byteBuffer.Append(pdfArray.GetAsNumber(1).FloatValue).Append(' ');
					byteBuffer.Append(pdfArray.GetAsNumber(2).FloatValue);
					dictionary["Color"] = PdfEncodings.ConvertToString(byteBuffer.ToByteArray(), null);
				}
				PdfNumber pdfNumber = (PdfNumber)PdfReader.GetPdfObjectRelease(outline.Get(PdfName.F));
				if (pdfNumber != null)
				{
					int intValue = pdfNumber.IntValue;
					string text = "";
					if ((intValue & 1) != 0)
					{
						text += "italic ";
					}
					if ((intValue & 2) != 0)
					{
						text += "bold ";
					}
					text = text.Trim();
					if (text.Length != 0)
					{
						dictionary["Style"] = text;
					}
				}
				PdfNumber pdfNumber2 = (PdfNumber)PdfReader.GetPdfObjectRelease(outline.Get(PdfName.COUNT));
				if (pdfNumber2 != null && pdfNumber2.IntValue < 0)
				{
					dictionary["Open"] = "false";
				}
				try
				{
					PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(outline.Get(PdfName.DEST));
					if (pdfObjectRelease != null)
					{
						SimpleBookmark.MapGotoBookmark(dictionary, pdfObjectRelease, pages);
					}
					else
					{
						PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(outline.Get(PdfName.A));
						if (pdfDictionary != null)
						{
							if (PdfName.GOTO.Equals(PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.S))))
							{
								pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.D));
								if (pdfObjectRelease != null)
								{
									SimpleBookmark.MapGotoBookmark(dictionary, pdfObjectRelease, pages);
								}
							}
							else if (PdfName.URI.Equals(PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.S))))
							{
								dictionary["Action"] = "URI";
								dictionary["URI"] = ((PdfString)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.URI))).ToUnicodeString();
							}
							else if (PdfName.GOTOR.Equals(PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.S))))
							{
								pdfObjectRelease = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.D));
								if (pdfObjectRelease != null)
								{
									if (pdfObjectRelease.IsString())
									{
										dictionary["Named"] = pdfObjectRelease.ToString();
									}
									else if (pdfObjectRelease.IsName())
									{
										dictionary["NamedN"] = PdfName.DecodeName(pdfObjectRelease.ToString());
									}
									else if (pdfObjectRelease.IsArray())
									{
										PdfArray pdfArray2 = (PdfArray)pdfObjectRelease;
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append(pdfArray2[0].ToString());
										stringBuilder.Append(' ').Append(pdfArray2[1].ToString());
										for (int i = 2; i < pdfArray2.Size; i++)
										{
											stringBuilder.Append(' ').Append(pdfArray2[i].ToString());
										}
										dictionary["Page"] = stringBuilder.ToString();
									}
								}
								dictionary["Action"] = "GoToR";
								PdfObject pdfObject = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.F));
								if (pdfObject != null)
								{
									if (pdfObject.IsString())
									{
										dictionary["File"] = ((PdfString)pdfObject).ToUnicodeString();
									}
									else if (pdfObject.IsDictionary())
									{
										pdfObject = PdfReader.GetPdfObject(((PdfDictionary)pdfObject).Get(PdfName.F));
										if (pdfObject.IsString())
										{
											dictionary["File"] = ((PdfString)pdfObject).ToUnicodeString();
										}
									}
								}
								PdfObject pdfObjectRelease2 = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.NEWWINDOW));
								if (pdfObjectRelease2 != null)
								{
									dictionary["NewWindow"] = pdfObjectRelease2.ToString();
								}
							}
							else if (PdfName.LAUNCH.Equals(PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.S))))
							{
								dictionary["Action"] = "Launch";
								PdfObject pdfObjectRelease3 = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.F));
								if (pdfObjectRelease3 == null)
								{
									pdfObjectRelease3 = PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.WIN));
								}
								if (pdfObjectRelease3 != null)
								{
									if (pdfObjectRelease3.IsString())
									{
										dictionary["File"] = ((PdfString)pdfObjectRelease3).ToUnicodeString();
									}
									else if (pdfObjectRelease3.IsDictionary())
									{
										pdfObjectRelease3 = PdfReader.GetPdfObjectRelease(((PdfDictionary)pdfObjectRelease3).Get(PdfName.F));
										if (pdfObjectRelease3.IsString())
										{
											dictionary["File"] = ((PdfString)pdfObjectRelease3).ToUnicodeString();
										}
									}
								}
							}
						}
					}
				}
				catch
				{
				}
				PdfDictionary pdfDictionary2 = (PdfDictionary)PdfReader.GetPdfObjectRelease(outline.Get(PdfName.FIRST));
				if (pdfDictionary2 != null)
				{
					dictionary["Kids"] = SimpleBookmark.BookmarkDepth(reader, pdfDictionary2, pages);
				}
				list.Add(dictionary);
				outline = (PdfDictionary)PdfReader.GetPdfObjectRelease(outline.Get(PdfName.NEXT));
			}
			return list;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000ADF70 File Offset: 0x000ACF70
		private static void MapGotoBookmark(Dictionary<string, object> map, PdfObject dest, IntHashtable pages)
		{
			if (dest.IsString())
			{
				map["Named"] = dest.ToString();
			}
			else if (dest.IsName())
			{
				map["Named"] = PdfName.DecodeName(dest.ToString());
			}
			else if (dest.IsArray())
			{
				map["Page"] = SimpleBookmark.MakeBookmarkParam((PdfArray)dest, pages);
			}
			map["Action"] = "GoTo";
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000ADFE8 File Offset: 0x000ACFE8
		private static string MakeBookmarkParam(PdfArray dest, IntHashtable pages)
		{
			StringBuilder stringBuilder = new StringBuilder();
			PdfObject pdfObject = dest[0];
			if (pdfObject.IsNumber())
			{
				stringBuilder.Append(((PdfNumber)pdfObject).IntValue + 1);
			}
			else
			{
				stringBuilder.Append(pages[SimpleBookmark.GetNumber((PdfIndirectReference)pdfObject)]);
			}
			stringBuilder.Append(' ').Append(dest[1].ToString().Substring(1));
			for (int i = 2; i < dest.Size; i++)
			{
				stringBuilder.Append(' ').Append(dest[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000AE08C File Offset: 0x000AD08C
		private static int GetNumber(PdfIndirectReference indirect)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(indirect);
			if (pdfDictionary.Contains(PdfName.TYPE) && pdfDictionary.Get(PdfName.TYPE).Equals(PdfName.PAGES) && pdfDictionary.Contains(PdfName.KIDS))
			{
				PdfArray pdfArray = (PdfArray)pdfDictionary.Get(PdfName.KIDS);
				indirect = (PdfIndirectReference)pdfArray[0];
			}
			return indirect.Number;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000AE0FC File Offset: 0x000AD0FC
		public static IList<Dictionary<string, object>> GetBookmark(PdfReader reader)
		{
			PdfDictionary catalog = reader.Catalog;
			PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(catalog.Get(PdfName.OUTLINES));
			if (pdfObjectRelease == null || !pdfObjectRelease.IsDictionary())
			{
				return null;
			}
			PdfDictionary pdfDictionary = (PdfDictionary)pdfObjectRelease;
			IntHashtable intHashtable = new IntHashtable();
			int numberOfPages = reader.NumberOfPages;
			for (int i = 1; i <= numberOfPages; i++)
			{
				intHashtable[reader.GetPageOrigRef(i).Number] = i;
				reader.ReleasePage(i);
			}
			return SimpleBookmark.BookmarkDepth(reader, (PdfDictionary)PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.FIRST)), intHashtable);
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x000AE190 File Offset: 0x000AD190
		public static void EliminatePages(IList<Dictionary<string, object>> list, int[] pageRange)
		{
			if (list == null)
			{
				return;
			}
			ListIterator<Dictionary<string, object>> listIterator = new ListIterator<Dictionary<string, object>>(list);
			while (listIterator.HasNext())
			{
				Dictionary<string, object> dictionary = listIterator.Next();
				bool flag = false;
				if ("GoTo".Equals(dictionary["Action"]))
				{
					string text = null;
					if (dictionary.ContainsKey("Page"))
					{
						text = (string)dictionary["Page"];
					}
					if (text != null)
					{
						text = text.Trim();
						int num = text.IndexOf(' ');
						int num2;
						if (num < 0)
						{
							num2 = int.Parse(text);
						}
						else
						{
							num2 = int.Parse(text.Substring(0, num));
						}
						int num3 = pageRange.Length & 2147483646;
						for (int i = 0; i < num3; i += 2)
						{
							if (num2 >= pageRange[i] && num2 <= pageRange[i + 1])
							{
								flag = true;
								break;
							}
						}
					}
				}
				IList<Dictionary<string, object>> list2 = null;
				if (dictionary.ContainsKey("Kids"))
				{
					list2 = (IList<Dictionary<string, object>>)dictionary["Kids"];
				}
				if (list2 != null)
				{
					SimpleBookmark.EliminatePages(list2, pageRange);
					if (list2.Count == 0)
					{
						dictionary.Remove("Kids");
						list2 = null;
					}
				}
				if (flag)
				{
					if (list2 == null)
					{
						listIterator.Remove();
					}
					else
					{
						dictionary.Remove("Action");
						dictionary.Remove("Page");
						dictionary.Remove("Named");
					}
				}
			}
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x000AE2DC File Offset: 0x000AD2DC
		public static void ShiftPageNumbers(IList<Dictionary<string, object>> list, int pageShift, int[] pageRange)
		{
			if (list == null)
			{
				return;
			}
			foreach (Dictionary<string, object> dictionary in list)
			{
				if (dictionary.ContainsKey("Action") && "GoTo".Equals(dictionary["Action"]))
				{
					string text = null;
					if (dictionary.ContainsKey("Page"))
					{
						text = (string)dictionary["Page"];
					}
					if (text != null)
					{
						text = text.Trim();
						int num = text.IndexOf(' ');
						int num2;
						if (num < 0)
						{
							num2 = int.Parse(text);
						}
						else
						{
							num2 = int.Parse(text.Substring(0, num));
						}
						bool flag = false;
						if (pageRange == null)
						{
							flag = true;
						}
						else
						{
							int num3 = pageRange.Length & 2147483646;
							for (int i = 0; i < num3; i += 2)
							{
								if (num2 >= pageRange[i] && num2 <= pageRange[i + 1])
								{
									flag = true;
									break;
								}
							}
						}
						if (flag)
						{
							if (num < 0)
							{
								text = num2 + pageShift + "";
							}
							else
							{
								text = num2 + pageShift + text.Substring(num);
							}
						}
						dictionary["Page"] = text;
					}
				}
				IList<Dictionary<string, object>> list2 = null;
				if (dictionary.ContainsKey("Kids"))
				{
					list2 = (IList<Dictionary<string, object>>)dictionary["Kids"];
				}
				if (list2 != null)
				{
					SimpleBookmark.ShiftPageNumbers(list2, pageShift, pageRange);
				}
			}
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000AE45C File Offset: 0x000AD45C
		public static string GetVal(Dictionary<string, object> map, string key)
		{
			object obj;
			map.TryGetValue(key, out obj);
			return (string)obj;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x000AE47C File Offset: 0x000AD47C
		internal static void CreateOutlineAction(PdfDictionary outline, Dictionary<string, object> map, PdfWriter writer, bool namedAsNames)
		{
			try
			{
				string val = SimpleBookmark.GetVal(map, "Action");
				if ("GoTo".Equals(val))
				{
					string val2;
					if ((val2 = SimpleBookmark.GetVal(map, "Named")) != null)
					{
						if (namedAsNames)
						{
							outline.Put(PdfName.DEST, new PdfName(val2));
						}
						else
						{
							outline.Put(PdfName.DEST, new PdfString(val2, null));
						}
					}
					else if ((val2 = SimpleBookmark.GetVal(map, "Page")) != null)
					{
						PdfArray pdfArray = new PdfArray();
						StringTokenizer stringTokenizer = new StringTokenizer(val2);
						int page = int.Parse(stringTokenizer.NextToken());
						pdfArray.Add(writer.GetPageReference(page));
						if (!stringTokenizer.HasMoreTokens())
						{
							pdfArray.Add(PdfName.XYZ);
							PdfArray pdfArray2 = pdfArray;
							float[] array = new float[3];
							array[1] = 10000f;
							pdfArray2.Add(array);
						}
						else
						{
							string text = stringTokenizer.NextToken();
							if (text.StartsWith("/"))
							{
								text = text.Substring(1);
							}
							pdfArray.Add(new PdfName(text));
							int num = 0;
							while (num < 4 && stringTokenizer.HasMoreTokens())
							{
								text = stringTokenizer.NextToken();
								if (text.Equals("null"))
								{
									pdfArray.Add(PdfNull.PDFNULL);
								}
								else
								{
									pdfArray.Add(new PdfNumber(text));
								}
								num++;
							}
						}
						outline.Put(PdfName.DEST, pdfArray);
					}
				}
				else if ("GoToR".Equals(val))
				{
					PdfDictionary pdfDictionary = new PdfDictionary();
					string val3;
					if ((val3 = SimpleBookmark.GetVal(map, "Named")) != null)
					{
						pdfDictionary.Put(PdfName.D, new PdfString(val3, null));
					}
					else if ((val3 = SimpleBookmark.GetVal(map, "NamedN")) != null)
					{
						pdfDictionary.Put(PdfName.D, new PdfName(val3));
					}
					else if ((val3 = SimpleBookmark.GetVal(map, "Page")) != null)
					{
						PdfArray pdfArray3 = new PdfArray();
						StringTokenizer stringTokenizer2 = new StringTokenizer(val3);
						pdfArray3.Add(new PdfNumber(stringTokenizer2.NextToken()));
						if (!stringTokenizer2.HasMoreTokens())
						{
							pdfArray3.Add(PdfName.XYZ);
							PdfArray pdfArray4 = pdfArray3;
							float[] array2 = new float[3];
							array2[1] = 10000f;
							pdfArray4.Add(array2);
						}
						else
						{
							string text2 = stringTokenizer2.NextToken();
							if (text2.StartsWith("/"))
							{
								text2 = text2.Substring(1);
							}
							pdfArray3.Add(new PdfName(text2));
							int num2 = 0;
							while (num2 < 4 && stringTokenizer2.HasMoreTokens())
							{
								text2 = stringTokenizer2.NextToken();
								if (text2.Equals("null"))
								{
									pdfArray3.Add(PdfNull.PDFNULL);
								}
								else
								{
									pdfArray3.Add(new PdfNumber(text2));
								}
								num2++;
							}
						}
						pdfDictionary.Put(PdfName.D, pdfArray3);
					}
					string val4 = SimpleBookmark.GetVal(map, "File");
					if (pdfDictionary.Size > 0 && val4 != null)
					{
						pdfDictionary.Put(PdfName.S, PdfName.GOTOR);
						pdfDictionary.Put(PdfName.F, new PdfString(val4));
						string val5 = SimpleBookmark.GetVal(map, "NewWindow");
						if (val5 != null)
						{
							if (val5.Equals("true"))
							{
								pdfDictionary.Put(PdfName.NEWWINDOW, PdfBoolean.PDFTRUE);
							}
							else if (val5.Equals("false"))
							{
								pdfDictionary.Put(PdfName.NEWWINDOW, PdfBoolean.PDFFALSE);
							}
						}
						outline.Put(PdfName.A, pdfDictionary);
					}
				}
				else if ("URI".Equals(val))
				{
					string val6 = SimpleBookmark.GetVal(map, "URI");
					if (val6 != null)
					{
						PdfDictionary pdfDictionary2 = new PdfDictionary();
						pdfDictionary2.Put(PdfName.S, PdfName.URI);
						pdfDictionary2.Put(PdfName.URI, new PdfString(val6));
						outline.Put(PdfName.A, pdfDictionary2);
					}
				}
				else if ("Launch".Equals(val))
				{
					string val7 = SimpleBookmark.GetVal(map, "File");
					if (val7 != null)
					{
						PdfDictionary pdfDictionary3 = new PdfDictionary();
						pdfDictionary3.Put(PdfName.S, PdfName.LAUNCH);
						pdfDictionary3.Put(PdfName.F, new PdfString(val7));
						outline.Put(PdfName.A, pdfDictionary3);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000AE8B0 File Offset: 0x000AD8B0
		public static object[] IterateOutlines(PdfWriter writer, PdfIndirectReference parent, IList<Dictionary<string, object>> kids, bool namedAsNames)
		{
			PdfIndirectReference[] array = new PdfIndirectReference[kids.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = writer.PdfIndirectReference;
			}
			int num = 0;
			int num2 = 0;
			foreach (Dictionary<string, object> dictionary in kids)
			{
				object[] array2 = null;
				IList<Dictionary<string, object>> list = null;
				if (dictionary.ContainsKey("Kids"))
				{
					list = (IList<Dictionary<string, object>>)dictionary["Kids"];
				}
				if (list != null && list.Count > 0)
				{
					array2 = SimpleBookmark.IterateOutlines(writer, array[num], list, namedAsNames);
				}
				PdfDictionary pdfDictionary = new PdfDictionary();
				num2++;
				if (array2 != null)
				{
					pdfDictionary.Put(PdfName.FIRST, (PdfIndirectReference)array2[0]);
					pdfDictionary.Put(PdfName.LAST, (PdfIndirectReference)array2[1]);
					int num3 = (int)array2[2];
					if (dictionary.ContainsKey("Open") && "false".Equals(dictionary["Open"]))
					{
						pdfDictionary.Put(PdfName.COUNT, new PdfNumber(-num3));
					}
					else
					{
						pdfDictionary.Put(PdfName.COUNT, new PdfNumber(num3));
						num2 += num3;
					}
				}
				pdfDictionary.Put(PdfName.PARENT, parent);
				if (num > 0)
				{
					pdfDictionary.Put(PdfName.PREV, array[num - 1]);
				}
				if (num < array.Length - 1)
				{
					pdfDictionary.Put(PdfName.NEXT, array[num + 1]);
				}
				pdfDictionary.Put(PdfName.TITLE, new PdfString((string)dictionary["Title"], "UnicodeBig"));
				string text = null;
				if (dictionary.ContainsKey("Color"))
				{
					text = (string)dictionary["Color"];
				}
				if (text != null)
				{
					try
					{
						PdfArray pdfArray = new PdfArray();
						StringTokenizer stringTokenizer = new StringTokenizer(text);
						for (int j = 0; j < 3; j++)
						{
							float num4 = float.Parse(stringTokenizer.NextToken(), NumberFormatInfo.InvariantInfo);
							if (num4 < 0f)
							{
								num4 = 0f;
							}
							if (num4 > 1f)
							{
								num4 = 1f;
							}
							pdfArray.Add(new PdfNumber(num4));
						}
						pdfDictionary.Put(PdfName.C, pdfArray);
					}
					catch
					{
					}
				}
				string text2 = SimpleBookmark.GetVal(dictionary, "Style");
				if (text2 != null)
				{
					text2 = text2.ToLower(CultureInfo.InvariantCulture);
					int num5 = 0;
					if (text2.IndexOf("italic") >= 0)
					{
						num5 |= 1;
					}
					if (text2.IndexOf("bold") >= 0)
					{
						num5 |= 2;
					}
					if (num5 != 0)
					{
						pdfDictionary.Put(PdfName.F, new PdfNumber(num5));
					}
				}
				SimpleBookmark.CreateOutlineAction(pdfDictionary, dictionary, writer, namedAsNames);
				writer.AddToBody(pdfDictionary, array[num]);
				num++;
			}
			return new object[]
			{
				array[0],
				array[array.Length - 1],
				num2
			};
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000AEBCC File Offset: 0x000ADBCC
		public static void ExportToXMLNode(IList<Dictionary<string, object>> list, TextWriter outp, int indent, bool onlyASCII)
		{
			string text = "";
			for (int i = 0; i < indent; i++)
			{
				text += "  ";
			}
			foreach (Dictionary<string, object> dictionary in list)
			{
				string text2 = null;
				outp.Write(text);
				outp.Write("<Title ");
				IList<Dictionary<string, object>> list2 = null;
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					if (key.Equals("Title"))
					{
						text2 = (string)keyValuePair.Value;
					}
					else if (key.Equals("Kids"))
					{
						list2 = (IList<Dictionary<string, object>>)keyValuePair.Value;
					}
					else
					{
						outp.Write(key);
						outp.Write("=\"");
						string s = (string)keyValuePair.Value;
						if (key.Equals("Named") || key.Equals("NamedN"))
						{
							s = SimpleBookmark.EscapeBinaryString(s);
						}
						outp.Write(SimpleXMLParser.EscapeXML(s, onlyASCII));
						outp.Write("\" ");
					}
				}
				outp.Write(">");
				if (text2 == null)
				{
					text2 = "";
				}
				outp.Write(SimpleXMLParser.EscapeXML(text2, onlyASCII));
				if (list2 != null)
				{
					outp.Write("\n");
					SimpleBookmark.ExportToXMLNode(list2, outp, indent + 1, onlyASCII);
					outp.Write(text);
				}
				outp.Write("</Title>\n");
			}
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000AED94 File Offset: 0x000ADD94
		public static void ExportToXML(IList<Dictionary<string, object>> list, Stream outp, string encoding, bool onlyASCII)
		{
			StreamWriter wrt = new StreamWriter(outp, IanaEncodings.GetEncodingEncoding(encoding));
			SimpleBookmark.ExportToXML(list, wrt, encoding, onlyASCII);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000AEDB7 File Offset: 0x000ADDB7
		public static void ExportToXML(IList<Dictionary<string, object>> list, TextWriter wrt, string encoding, bool onlyASCII)
		{
			wrt.Write("<?xml version=\"1.0\" encoding=\"");
			wrt.Write(SimpleXMLParser.EscapeXML(encoding, onlyASCII));
			wrt.Write("\"?>\n<Bookmark>\n");
			SimpleBookmark.ExportToXMLNode(list, wrt, 1, onlyASCII);
			wrt.Write("</Bookmark>\n");
			wrt.Flush();
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000AEDF8 File Offset: 0x000ADDF8
		public static IList<Dictionary<string, object>> ImportFromXML(Stream inp)
		{
			SimpleBookmark simpleBookmark = new SimpleBookmark();
			SimpleXMLParser.Parse(simpleBookmark, inp);
			return simpleBookmark.topList;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000AEE18 File Offset: 0x000ADE18
		public static IList<Dictionary<string, object>> ImportFromXML(TextReader inp)
		{
			SimpleBookmark simpleBookmark = new SimpleBookmark();
			SimpleXMLParser.Parse(simpleBookmark, inp);
			return simpleBookmark.topList;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000AEE38 File Offset: 0x000ADE38
		public static string EscapeBinaryString(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = s.ToCharArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				char c = array[i];
				if (c < ' ')
				{
					stringBuilder.Append('\\');
					int num2 = (int)c;
					string text = "";
					do
					{
						text = (num2 % 8).ToString() + text;
						num2 /= 8;
					}
					while (num2 > 0);
					stringBuilder.Append(text.PadLeft(3, '0'));
				}
				else if (c == '\\')
				{
					stringBuilder.Append("\\\\");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000AEED8 File Offset: 0x000ADED8
		public static string UnEscapeBinaryString(string s)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = s.ToCharArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				char c = array[i];
				if (c == '\\')
				{
					if (++i >= num)
					{
						stringBuilder.Append('\\');
						break;
					}
					c = array[i];
					if (c >= '0' && c <= '7')
					{
						int num2 = (int)(c - '0');
						i++;
						int num3 = 0;
						while (num3 < 2 && i < num)
						{
							c = array[i];
							if (c < '0' || c > '7')
							{
								break;
							}
							i++;
							num2 = num2 * 8 + (int)c - 48;
							num3++;
						}
						i--;
						stringBuilder.Append((char)num2);
					}
					else
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000AEFA7 File Offset: 0x000ADFA7
		public void EndDocument()
		{
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000AEFAC File Offset: 0x000ADFAC
		public void EndElement(string tag)
		{
			if (tag.Equals("Bookmark"))
			{
				if (this.attr.Count == 0)
				{
					return;
				}
				throw new Exception(MessageLocalization.GetComposedMessage("bookmark.end.tag.out.of.place"));
			}
			else
			{
				if (!tag.Equals("Title"))
				{
					throw new Exception(MessageLocalization.GetComposedMessage("invalid.end.tag.1", tag));
				}
				Dictionary<string, object> dictionary = this.attr.Pop();
				string text = (string)dictionary["Title"];
				dictionary["Title"] = text.Trim();
				string val = SimpleBookmark.GetVal(dictionary, "Named");
				if (val != null)
				{
					dictionary["Named"] = SimpleBookmark.UnEscapeBinaryString(val);
				}
				val = SimpleBookmark.GetVal(dictionary, "NamedN");
				if (val != null)
				{
					dictionary["NamedN"] = SimpleBookmark.UnEscapeBinaryString(val);
				}
				if (this.attr.Count == 0)
				{
					this.topList.Add(dictionary);
					return;
				}
				Dictionary<string, object> dictionary2 = this.attr.Peek();
				IList<Dictionary<string, object>> list = null;
				if (dictionary2.ContainsKey("Kids"))
				{
					list = (IList<Dictionary<string, object>>)dictionary2["Kids"];
				}
				if (list == null)
				{
					list = new List<Dictionary<string, object>>();
					dictionary2["Kids"] = list;
				}
				list.Add(dictionary);
				return;
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000AF0D8 File Offset: 0x000AE0D8
		public void StartDocument()
		{
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000AF0DC File Offset: 0x000AE0DC
		public void StartElement(string tag, Dictionary<string, string> h)
		{
			if (this.topList == null)
			{
				if (tag.Equals("Bookmark"))
				{
					this.topList = new List<Dictionary<string, object>>();
					return;
				}
				throw new Exception(MessageLocalization.GetComposedMessage("root.element.is.not.bookmark.1", tag));
			}
			else
			{
				if (!tag.Equals("Title"))
				{
					throw new Exception(MessageLocalization.GetComposedMessage("tag.1.not.allowed", tag));
				}
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (KeyValuePair<string, string> keyValuePair in h)
				{
					dictionary[keyValuePair.Key] = keyValuePair.Value;
				}
				dictionary["Title"] = "";
				dictionary.Remove("Kids");
				this.attr.Push(dictionary);
				return;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000AF1B8 File Offset: 0x000AE1B8
		public void Text(string str)
		{
			if (this.attr.Count == 0)
			{
				return;
			}
			Dictionary<string, object> dictionary = this.attr.Peek();
			string text = (string)dictionary["Title"];
			text += str;
			dictionary["Title"] = text;
		}

		// Token: 0x04001411 RID: 5137
		private List<Dictionary<string, object>> topList;

		// Token: 0x04001412 RID: 5138
		private Stack<Dictionary<string, object>> attr = new Stack<Dictionary<string, object>>();
	}
}
