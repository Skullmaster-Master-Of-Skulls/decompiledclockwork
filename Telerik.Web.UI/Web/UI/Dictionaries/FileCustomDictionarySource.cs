using System;
using System.IO;
using System.Text;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D2 RID: 4562
	internal class FileCustomDictionarySource : ReaderCustomDictionarySource
	{
		// Token: 0x0600BC92 RID: 48274 RVA: 0x0029D547 File Offset: 0x0029B747
		internal FileCustomDictionarySource() : base(null)
		{
		}

		// Token: 0x0600BC93 RID: 48275 RVA: 0x0029D550 File Offset: 0x0029B750
		public override void AddWord(string word)
		{
			using (StreamWriter streamWriter = new StreamWriter(this.CustomDictionaryFile(), true))
			{
				streamWriter.WriteLine(word);
			}
		}

		// Token: 0x17003CE8 RID: 15592
		// (get) Token: 0x0600BC94 RID: 48276 RVA: 0x0029D590 File Offset: 0x0029B790
		// (set) Token: 0x0600BC95 RID: 48277 RVA: 0x0029D5AC File Offset: 0x0029B7AC
		protected override TextReader DictionaryReader
		{
			get
			{
				if (this.dictionaryReader == null)
				{
					this.dictionaryReader = this.CreateFileReader();
				}
				return this.dictionaryReader;
			}
			set
			{
				this.dictionaryReader = value;
			}
		}

		// Token: 0x0600BC96 RID: 48278 RVA: 0x0029D5B8 File Offset: 0x0029B7B8
		private TextReader CreateFileReader()
		{
			TextReader result;
			using (TextReader textReader = FileCustomDictionarySource.ReaderForFile(this.CustomDictionaryFile()))
			{
				string s = textReader.ReadToEnd();
				result = new StringReader(s);
			}
			return result;
		}

		// Token: 0x0600BC97 RID: 48279 RVA: 0x0029D5FC File Offset: 0x0029B7FC
		private string CustomDictionaryFile()
		{
			return Path.Combine(base.DictionaryPath, base.Language + base.CustomAppendix + ".txt");
		}

		// Token: 0x0600BC98 RID: 48280 RVA: 0x0029D61F File Offset: 0x0029B81F
		private static TextReader ReaderForFile(string dictionaryFile)
		{
			if (File.Exists(dictionaryFile))
			{
				return new StreamReader(dictionaryFile, Encoding.UTF8);
			}
			return new StreamReader(new MemoryStream());
		}

		// Token: 0x04003189 RID: 12681
		private TextReader dictionaryReader;
	}
}
