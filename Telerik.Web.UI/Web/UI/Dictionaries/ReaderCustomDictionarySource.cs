using System;
using System.IO;

namespace Telerik.Web.UI.Dictionaries
{
	// Token: 0x020011D1 RID: 4561
	internal class ReaderCustomDictionarySource : ICustomDictionarySource
	{
		// Token: 0x17003CE4 RID: 15588
		// (get) Token: 0x0600BC87 RID: 48263 RVA: 0x0029D4C3 File Offset: 0x0029B6C3
		// (set) Token: 0x0600BC88 RID: 48264 RVA: 0x0029D4CB File Offset: 0x0029B6CB
		protected virtual TextReader DictionaryReader
		{
			get
			{
				return this.dictionaryReader;
			}
			set
			{
				this.dictionaryReader = value;
			}
		}

		// Token: 0x0600BC89 RID: 48265 RVA: 0x0029D4D4 File Offset: 0x0029B6D4
		internal ReaderCustomDictionarySource(TextReader dictionaryReader)
		{
			this.dictionaryReader = dictionaryReader;
		}

		// Token: 0x17003CE5 RID: 15589
		// (get) Token: 0x0600BC8A RID: 48266 RVA: 0x0029D4E3 File Offset: 0x0029B6E3
		// (set) Token: 0x0600BC8B RID: 48267 RVA: 0x0029D4EB File Offset: 0x0029B6EB
		public string DictionaryPath
		{
			get
			{
				return this.dictionaryPath;
			}
			set
			{
				this.dictionaryPath = value;
			}
		}

		// Token: 0x17003CE6 RID: 15590
		// (get) Token: 0x0600BC8C RID: 48268 RVA: 0x0029D4F4 File Offset: 0x0029B6F4
		// (set) Token: 0x0600BC8D RID: 48269 RVA: 0x0029D4FC File Offset: 0x0029B6FC
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x17003CE7 RID: 15591
		// (get) Token: 0x0600BC8E RID: 48270 RVA: 0x0029D505 File Offset: 0x0029B705
		// (set) Token: 0x0600BC8F RID: 48271 RVA: 0x0029D50D File Offset: 0x0029B70D
		public string CustomAppendix
		{
			get
			{
				return this.customAppendix;
			}
			set
			{
				this.customAppendix = value;
			}
		}

		// Token: 0x0600BC90 RID: 48272 RVA: 0x0029D518 File Offset: 0x0029B718
		public string ReadWord()
		{
			string text = this.DictionaryReader.ReadLine();
			if (text == null)
			{
				this.DictionaryReader.Close();
			}
			return text;
		}

		// Token: 0x0600BC91 RID: 48273 RVA: 0x0029D540 File Offset: 0x0029B740
		public virtual void AddWord(string word)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04003185 RID: 12677
		private TextReader dictionaryReader;

		// Token: 0x04003186 RID: 12678
		private string dictionaryPath;

		// Token: 0x04003187 RID: 12679
		private string language;

		// Token: 0x04003188 RID: 12680
		private string customAppendix;
	}
}
