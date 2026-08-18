using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011CB RID: 4555
	public class DictionaryImporter
	{
		// Token: 0x0600BC18 RID: 48152 RVA: 0x0029AE46 File Offset: 0x00299046
		public DictionaryImporter()
		{
			this._dictionary = new ArrayList();
			this.encoder = new DoubleMetaphone();
		}

		// Token: 0x17003CDB RID: 15579
		// (get) Token: 0x0600BC19 RID: 48153 RVA: 0x0029AE64 File Offset: 0x00299064
		public ArrayList dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x0600BC1A RID: 48154 RVA: 0x0029AE6C File Offset: 0x0029906C
		public void Sort()
		{
			if (this.dictionary.Count > 0)
			{
				WordComparer comparer = new WordComparer();
				SorterObjectArray sorterObjectArray = new SorterObjectArray(this.dictionary, comparer);
				sorterObjectArray.QuickSort(0, this.dictionary.Count - 1);
			}
		}

		// Token: 0x0600BC1B RID: 48155 RVA: 0x0029AEB0 File Offset: 0x002990B0
		public void Save(string outputFile)
		{
			using (StreamWriter streamWriter = new StreamWriter(outputFile, false, Encoding.UTF8))
			{
				this.Save(streamWriter);
				streamWriter.Close();
			}
		}

		// Token: 0x0600BC1C RID: 48156 RVA: 0x0029AEF4 File Offset: 0x002990F4
		public void Save(TextWriter output)
		{
			this.Sort();
			string b = string.Empty;
			foreach (object obj in this.dictionary)
			{
				string text = (string)obj;
				if (text != b)
				{
					output.WriteLine(this.LineForWord(text));
				}
				b = text;
			}
		}

		// Token: 0x0600BC1D RID: 48157 RVA: 0x0029AF6C File Offset: 0x0029916C
		public void Load(string inputFile)
		{
			using (StreamReader streamReader = new StreamReader(inputFile, Encoding.UTF8))
			{
				this.Load(streamReader);
				streamReader.Close();
			}
		}

		// Token: 0x0600BC1E RID: 48158 RVA: 0x0029AFB0 File Offset: 0x002991B0
		public void Load(TextReader input)
		{
			string text;
			while ((text = input.ReadLine()) != null)
			{
				string[] array = text.Trim().Split(new char[]
				{
					':'
				});
				this.dictionary.Add(array[0].Trim());
			}
		}

		// Token: 0x0600BC1F RID: 48159 RVA: 0x0029AFF8 File Offset: 0x002991F8
		internal string LineForWord(string word)
		{
			string arg = this.encoder.Encode(word, false);
			string arg2 = this.encoder.Encode(word, true);
			return string.Format("{0}:{1}:{2}", word, arg, arg2);
		}

		// Token: 0x0600BC20 RID: 48160 RVA: 0x0029B030 File Offset: 0x00299230
		internal static string[] ParseLine(string line)
		{
			string[] array = line.Split(new char[]
			{
				':'
			});
			if (array.Length != 3)
			{
				throw new DictionaryFormatException();
			}
			return array;
		}

		// Token: 0x0600BC21 RID: 48161 RVA: 0x0029B05E File Offset: 0x0029925E
		public void AddWord(string word)
		{
			this.dictionary.Add(word);
		}

		// Token: 0x0600BC22 RID: 48162 RVA: 0x0029B070 File Offset: 0x00299270
		public ArrayList Find(string query)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.dictionary)
			{
				string text = (string)obj;
				if (text.IndexOf(query) != -1)
				{
					arrayList.Add(text);
				}
			}
			return arrayList;
		}

		// Token: 0x0600BC23 RID: 48163 RVA: 0x0029B0DC File Offset: 0x002992DC
		public void Delete(ArrayList victims)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.dictionary)
			{
				string text = (string)obj;
				if (victims.Contains(text))
				{
					arrayList.Add(text);
				}
			}
			foreach (object obj2 in arrayList)
			{
				string obj3 = (string)obj2;
				this.dictionary.Remove(obj3);
			}
		}

		// Token: 0x0400316E RID: 12654
		private readonly DoubleMetaphone encoder;

		// Token: 0x0400316F RID: 12655
		private readonly ArrayList _dictionary;
	}
}
