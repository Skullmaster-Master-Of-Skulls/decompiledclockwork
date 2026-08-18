using System;
using System.Collections.Generic;
using System.IO;
using System.util;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002D3 RID: 723
	public class FdfWriter
	{
		// Token: 0x06001AF0 RID: 6896 RVA: 0x0009EFE4 File Offset: 0x0009DFE4
		public void WriteTo(Stream os)
		{
			FdfWriter.Wrt wrt = new FdfWriter.Wrt(os, this);
			wrt.WriteTo();
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0009F000 File Offset: 0x0009E000
		internal bool SetField(string field, PdfObject value)
		{
			Dictionary<string, object> dictionary = this.fields;
			StringTokenizer stringTokenizer = new StringTokenizer(field, ".");
			if (!stringTokenizer.HasMoreTokens())
			{
				return false;
			}
			object obj;
			for (;;)
			{
				string key = stringTokenizer.NextToken();
				dictionary.TryGetValue(key, out obj);
				if (!stringTokenizer.HasMoreTokens())
				{
					goto IL_63;
				}
				if (obj == null)
				{
					obj = new Dictionary<string, object>();
					dictionary[key] = obj;
					dictionary = (Dictionary<string, object>)obj;
				}
				else
				{
					if (!(obj is Dictionary<string, object>))
					{
						break;
					}
					dictionary = (Dictionary<string, object>)obj;
				}
			}
			return false;
			IL_63:
			if (!(obj is Dictionary<string, object>))
			{
				string key;
				dictionary[key] = value;
				return true;
			}
			return false;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0009F084 File Offset: 0x0009E084
		internal void IterateFields(Dictionary<string, object> values, Dictionary<string, object> map, string name)
		{
			foreach (KeyValuePair<string, object> keyValuePair in map)
			{
				string key = keyValuePair.Key;
				object value = keyValuePair.Value;
				if (value is Dictionary<string, object>)
				{
					this.IterateFields(values, (Dictionary<string, object>)value, name + "." + key);
				}
				else
				{
					values[(name + "." + key).Substring(1)] = value;
				}
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x0009F118 File Offset: 0x0009E118
		public bool RemoveField(string field)
		{
			Dictionary<string, object> dictionary = this.fields;
			StringTokenizer stringTokenizer = new StringTokenizer(field, ".");
			if (!stringTokenizer.HasMoreTokens())
			{
				return false;
			}
			List<object> list = new List<object>();
			object obj;
			for (;;)
			{
				string text = stringTokenizer.NextToken();
				dictionary.TryGetValue(text, out obj);
				if (obj == null)
				{
					break;
				}
				list.Add(dictionary);
				list.Add(text);
				if (!stringTokenizer.HasMoreTokens())
				{
					goto IL_65;
				}
				if (!(obj is Dictionary<string, object>))
				{
					return false;
				}
				dictionary = (Dictionary<string, object>)obj;
			}
			return false;
			IL_65:
			if (obj is Dictionary<string, object>)
			{
				return false;
			}
			for (int i = list.Count - 2; i >= 0; i -= 2)
			{
				dictionary = (Dictionary<string, object>)list[i];
				string key = (string)list[i + 1];
				dictionary.Remove(key);
				if (dictionary.Count > 0)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0009F1E0 File Offset: 0x0009E1E0
		public Dictionary<string, object> GetFields()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			this.IterateFields(dictionary, this.fields, "");
			return dictionary;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0009F208 File Offset: 0x0009E208
		public string GetField(string field)
		{
			Dictionary<string, object> dictionary = this.fields;
			StringTokenizer stringTokenizer = new StringTokenizer(field, ".");
			if (!stringTokenizer.HasMoreTokens())
			{
				return null;
			}
			object obj;
			for (;;)
			{
				string key = stringTokenizer.NextToken();
				dictionary.TryGetValue(key, out obj);
				if (obj == null)
				{
					break;
				}
				if (!stringTokenizer.HasMoreTokens())
				{
					goto IL_4E;
				}
				if (!(obj is Dictionary<string, object>))
				{
					goto IL_4C;
				}
				dictionary = (Dictionary<string, object>)obj;
			}
			return null;
			IL_4C:
			return null;
			IL_4E:
			if (obj is Dictionary<string, object>)
			{
				return null;
			}
			if (((PdfObject)obj).IsString())
			{
				return ((PdfString)obj).ToUnicodeString();
			}
			return PdfName.DecodeName(obj.ToString());
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x0009F291 File Offset: 0x0009E291
		public bool SetFieldAsName(string field, string value)
		{
			return this.SetField(field, new PdfName(value));
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x0009F2A0 File Offset: 0x0009E2A0
		public bool SetFieldAsString(string field, string value)
		{
			return this.SetField(field, new PdfString(value, "UnicodeBig"));
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x0009F2B4 File Offset: 0x0009E2B4
		public bool SetFieldAsAction(string field, PdfAction action)
		{
			return this.SetField(field, action);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0009F2C0 File Offset: 0x0009E2C0
		public void SetFields(FdfReader fdf)
		{
			Dictionary<string, PdfDictionary> dictionary = fdf.Fields;
			foreach (KeyValuePair<string, PdfDictionary> keyValuePair in dictionary)
			{
				string key = keyValuePair.Key;
				PdfDictionary value = keyValuePair.Value;
				PdfObject pdfObject = value.Get(PdfName.V);
				if (pdfObject != null)
				{
					this.SetField(key, pdfObject);
				}
				pdfObject = value.Get(PdfName.A);
				if (pdfObject != null)
				{
					this.SetField(key, pdfObject);
				}
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0009F358 File Offset: 0x0009E358
		public void SetFields(PdfReader pdf)
		{
			this.SetFields(pdf.AcroFields);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x0009F368 File Offset: 0x0009E368
		public void SetFields(AcroFields af)
		{
			foreach (KeyValuePair<string, AcroFields.Item> keyValuePair in af.Fields)
			{
				string key = keyValuePair.Key;
				AcroFields.Item value = keyValuePair.Value;
				PdfDictionary merged = value.GetMerged(0);
				PdfObject pdfObjectRelease = PdfReader.GetPdfObjectRelease(merged.Get(PdfName.V));
				if (pdfObjectRelease != null)
				{
					PdfObject pdfObjectRelease2 = PdfReader.GetPdfObjectRelease(merged.Get(PdfName.FT));
					if (pdfObjectRelease2 != null && !PdfName.SIG.Equals(pdfObjectRelease2))
					{
						this.SetField(key, pdfObjectRelease);
					}
				}
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0009F414 File Offset: 0x0009E414
		// (set) Token: 0x06001AFD RID: 6909 RVA: 0x0009F41C File Offset: 0x0009E41C
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		// Token: 0x040011F5 RID: 4597
		private static readonly byte[] HEADER_FDF = DocWriter.GetISOBytes("%FDF-1.4\n%âãÏÓ\n");

		// Token: 0x040011F6 RID: 4598
		private Dictionary<string, object> fields = new Dictionary<string, object>();

		// Token: 0x040011F7 RID: 4599
		private string file;

		// Token: 0x020002D4 RID: 724
		internal class Wrt : PdfWriter
		{
			// Token: 0x06001AFF RID: 6911 RVA: 0x0009F436 File Offset: 0x0009E436
			internal Wrt(Stream os, FdfWriter fdf) : base(new PdfDocument(), os)
			{
				this.fdf = fdf;
				this.os.Write(FdfWriter.HEADER_FDF, 0, FdfWriter.HEADER_FDF.Length);
				this.body = new PdfWriter.PdfBody(this);
			}

			// Token: 0x06001B00 RID: 6912 RVA: 0x0009F470 File Offset: 0x0009E470
			internal void WriteTo()
			{
				PdfDictionary pdfDictionary = new PdfDictionary();
				pdfDictionary.Put(PdfName.FIELDS, this.Calculate(this.fdf.fields));
				if (this.fdf.file != null)
				{
					pdfDictionary.Put(PdfName.F, new PdfString(this.fdf.file, "UnicodeBig"));
				}
				PdfDictionary pdfDictionary2 = new PdfDictionary();
				pdfDictionary2.Put(PdfName.FDF, pdfDictionary);
				PdfIndirectReference indirectReference = base.AddToBody(pdfDictionary2).IndirectReference;
				byte[] isobytes = DocWriter.GetISOBytes("trailer\n");
				this.os.Write(isobytes, 0, isobytes.Length);
				PdfDictionary pdfDictionary3 = new PdfDictionary();
				pdfDictionary3.Put(PdfName.ROOT, indirectReference);
				pdfDictionary3.ToPdf(null, this.os);
				isobytes = DocWriter.GetISOBytes("\n%%EOF\n");
				this.os.Write(isobytes, 0, isobytes.Length);
				this.os.Close();
			}

			// Token: 0x06001B01 RID: 6913 RVA: 0x0009F550 File Offset: 0x0009E550
			internal PdfArray Calculate(Dictionary<string, object> map)
			{
				PdfArray pdfArray = new PdfArray();
				foreach (KeyValuePair<string, object> keyValuePair in map)
				{
					string key = keyValuePair.Key;
					object value = keyValuePair.Value;
					PdfDictionary pdfDictionary = new PdfDictionary();
					pdfDictionary.Put(PdfName.T, new PdfString(key, "UnicodeBig"));
					if (value is Dictionary<string, object>)
					{
						pdfDictionary.Put(PdfName.KIDS, this.Calculate((Dictionary<string, object>)value));
					}
					else if (value is PdfAction)
					{
						pdfDictionary.Put(PdfName.A, (PdfAction)value);
					}
					else
					{
						pdfDictionary.Put(PdfName.V, (PdfObject)value);
					}
					pdfArray.Add(pdfDictionary);
				}
				return pdfArray;
			}

			// Token: 0x040011F8 RID: 4600
			private FdfWriter fdf;
		}
	}
}
