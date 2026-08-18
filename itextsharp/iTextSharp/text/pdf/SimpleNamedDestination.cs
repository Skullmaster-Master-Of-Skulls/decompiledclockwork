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
	// Token: 0x020002C7 RID: 711
	public sealed class SimpleNamedDestination : ISimpleXMLDocHandler
	{
		// Token: 0x06001A97 RID: 6807 RVA: 0x0009C77C File Offset: 0x0009B77C
		private SimpleNamedDestination()
		{
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x0009C784 File Offset: 0x0009B784
		public static Dictionary<string, string> GetNamedDestination(PdfReader reader, bool fromNames)
		{
			IntHashtable intHashtable = new IntHashtable();
			int numberOfPages = reader.NumberOfPages;
			for (int i = 1; i <= numberOfPages; i++)
			{
				intHashtable[reader.GetPageOrigRef(i).Number] = i;
			}
			Dictionary<string, PdfObject> dictionary = fromNames ? reader.GetNamedDestinationFromNames() : reader.GetNamedDestinationFromStrings();
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>(dictionary.Count);
			string[] array = new string[dictionary.Count];
			dictionary.Keys.CopyTo(array, 0);
			foreach (string key in array)
			{
				PdfArray pdfArray = (PdfArray)dictionary[key];
				StringBuilder stringBuilder = new StringBuilder();
				try
				{
					stringBuilder.Append(intHashtable[pdfArray.GetAsIndirectObject(0).Number]);
					stringBuilder.Append(' ').Append(pdfArray[1].ToString().Substring(1));
					for (int k = 2; k < pdfArray.Size; k++)
					{
						stringBuilder.Append(' ').Append(pdfArray[k].ToString());
					}
					dictionary2[key] = stringBuilder.ToString();
				}
				catch
				{
				}
			}
			return dictionary2;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x0009C8C8 File Offset: 0x0009B8C8
		public static void ExportToXML(Dictionary<string, string> names, Stream outp, string encoding, bool onlyASCII)
		{
			StreamWriter wrt = new StreamWriter(outp, IanaEncodings.GetEncodingEncoding(encoding));
			SimpleNamedDestination.ExportToXML(names, wrt, encoding, onlyASCII);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0009C8EC File Offset: 0x0009B8EC
		public static void ExportToXML(Dictionary<string, string> names, TextWriter wrt, string encoding, bool onlyASCII)
		{
			wrt.Write("<?xml version=\"1.0\" encoding=\"");
			wrt.Write(SimpleXMLParser.EscapeXML(encoding, onlyASCII));
			wrt.Write("\"?>\n<Destination>\n");
			foreach (string text in names.Keys)
			{
				string s = names[text];
				wrt.Write("  <Name Page=\"");
				wrt.Write(SimpleXMLParser.EscapeXML(s, onlyASCII));
				wrt.Write("\">");
				wrt.Write(SimpleXMLParser.EscapeXML(SimpleNamedDestination.EscapeBinaryString(text), onlyASCII));
				wrt.Write("</Name>\n");
			}
			wrt.Write("</Destination>\n");
			wrt.Flush();
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x0009C9B4 File Offset: 0x0009B9B4
		public static Dictionary<string, string> ImportFromXML(Stream inp)
		{
			SimpleNamedDestination simpleNamedDestination = new SimpleNamedDestination();
			SimpleXMLParser.Parse(simpleNamedDestination, inp);
			return simpleNamedDestination.xmlNames;
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x0009C9D4 File Offset: 0x0009B9D4
		public static Dictionary<string, string> ImportFromXML(TextReader inp)
		{
			SimpleNamedDestination simpleNamedDestination = new SimpleNamedDestination();
			SimpleXMLParser.Parse(simpleNamedDestination, inp);
			return simpleNamedDestination.xmlNames;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0009C9F4 File Offset: 0x0009B9F4
		internal static PdfArray CreateDestinationArray(string value, PdfWriter writer)
		{
			PdfArray pdfArray = new PdfArray();
			StringTokenizer stringTokenizer = new StringTokenizer(value);
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
			return pdfArray;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0009CACC File Offset: 0x0009BACC
		public static PdfDictionary OutputNamedDestinationAsNames(Dictionary<string, string> names, PdfWriter writer)
		{
			PdfDictionary pdfDictionary = new PdfDictionary();
			foreach (string text in names.Keys)
			{
				try
				{
					string value = names[text];
					PdfArray value2 = SimpleNamedDestination.CreateDestinationArray(value, writer);
					PdfName key = new PdfName(text);
					pdfDictionary.Put(key, value2);
				}
				catch
				{
				}
			}
			return pdfDictionary;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0009CB54 File Offset: 0x0009BB54
		public static PdfDictionary OutputNamedDestinationAsStrings(Dictionary<string, string> names, PdfWriter writer)
		{
			Dictionary<string, PdfObject> dictionary = new Dictionary<string, PdfObject>(names.Count);
			foreach (string key in names.Keys)
			{
				try
				{
					string value = names[key];
					PdfArray objecta = SimpleNamedDestination.CreateDestinationArray(value, writer);
					dictionary[key] = writer.AddToBody(objecta).IndirectReference;
				}
				catch
				{
				}
			}
			return PdfNameTree.WriteTree<PdfObject>(dictionary, writer);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x0009CBE8 File Offset: 0x0009BBE8
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
					num2.ToString("", CultureInfo.InvariantCulture);
					string text = "00" + Convert.ToString((int)c, 8);
					stringBuilder.Append(text.Substring(text.Length - 3));
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

		// Token: 0x06001AA1 RID: 6817 RVA: 0x0009CC8C File Offset: 0x0009BC8C
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

		// Token: 0x06001AA2 RID: 6818 RVA: 0x0009CD5B File Offset: 0x0009BD5B
		public void EndDocument()
		{
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x0009CD60 File Offset: 0x0009BD60
		public void EndElement(string tag)
		{
			if (tag.Equals("Destination"))
			{
				if (this.xmlLast == null && this.xmlNames != null)
				{
					return;
				}
				throw new ArgumentException(MessageLocalization.GetComposedMessage("destination.end.tag.out.of.place"));
			}
			else
			{
				if (!tag.Equals("Name"))
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("invalid.end.tag.1", tag));
				}
				if (this.xmlLast == null || this.xmlNames == null)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("name.end.tag.out.of.place"));
				}
				if (!this.xmlLast.ContainsKey("Page"))
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("page.attribute.missing"));
				}
				this.xmlNames[SimpleNamedDestination.UnEscapeBinaryString(this.xmlLast["Name"])] = this.xmlLast["Page"];
				this.xmlLast = null;
				return;
			}
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x0009CE32 File Offset: 0x0009BE32
		public void StartDocument()
		{
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0009CE34 File Offset: 0x0009BE34
		public void StartElement(string tag, Dictionary<string, string> h)
		{
			if (this.xmlNames == null)
			{
				if (tag.Equals("Destination"))
				{
					this.xmlNames = new Dictionary<string, string>();
					return;
				}
				throw new ArgumentException(MessageLocalization.GetComposedMessage("root.element.is.not.destination"));
			}
			else
			{
				if (!tag.Equals("Name"))
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("tag.1.not.allowed", tag));
				}
				if (this.xmlLast != null)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("nested.tags.are.not.allowed"));
				}
				this.xmlLast = new Dictionary<string, string>(h);
				this.xmlLast["Name"] = "";
				return;
			}
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0009CECC File Offset: 0x0009BECC
		public void Text(string str)
		{
			if (this.xmlLast == null)
			{
				return;
			}
			string text = this.xmlLast["Name"];
			text += str;
			this.xmlLast["Name"] = text;
		}

		// Token: 0x040011C0 RID: 4544
		private Dictionary<string, string> xmlNames;

		// Token: 0x040011C1 RID: 4545
		private Dictionary<string, string> xmlLast;
	}
}
