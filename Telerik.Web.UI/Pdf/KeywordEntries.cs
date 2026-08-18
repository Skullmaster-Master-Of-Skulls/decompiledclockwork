using System;
using System.Collections;

namespace Telerik.Pdf
{
	// Token: 0x02001661 RID: 5729
	public sealed class KeywordEntries
	{
		// Token: 0x0600DDEF RID: 56815 RVA: 0x003077F8 File Offset: 0x003059F8
		static KeywordEntries()
		{
			KeywordEntries.Entries.Add(Keyword.Obj, new byte[]
			{
				111,
				98,
				106
			});
			KeywordEntries.Entries.Add(Keyword.EndObj, new byte[]
			{
				101,
				110,
				100,
				111,
				98,
				106
			});
			KeywordEntries.Entries.Add(Keyword.R, new byte[]
			{
				82
			});
			KeywordEntries.Entries.Add(Keyword.DictionaryBegin, new byte[]
			{
				60,
				60
			});
			KeywordEntries.Entries.Add(Keyword.DictionaryEnd, new byte[]
			{
				62,
				62
			});
			KeywordEntries.Entries.Add(Keyword.ArrayBegin, new byte[]
			{
				91
			});
			KeywordEntries.Entries.Add(Keyword.ArrayEnd, new byte[]
			{
				93
			});
			KeywordEntries.Entries.Add(Keyword.Stream, new byte[]
			{
				115,
				116,
				114,
				101,
				97,
				109
			});
			KeywordEntries.Entries.Add(Keyword.EndStream, new byte[]
			{
				101,
				110,
				100,
				115,
				116,
				114,
				101,
				97,
				109
			});
			KeywordEntries.Entries.Add(Keyword.True, new byte[]
			{
				116,
				114,
				117,
				101
			});
			KeywordEntries.Entries.Add(Keyword.False, new byte[]
			{
				102,
				97,
				108,
				115,
				101
			});
			KeywordEntries.Entries.Add(Keyword.Null, new byte[]
			{
				110,
				117,
				108,
				108
			});
			KeywordEntries.Entries.Add(Keyword.XRef, new byte[]
			{
				120,
				114,
				101,
				102
			});
			KeywordEntries.Entries.Add(Keyword.Trailer, new byte[]
			{
				116,
				114,
				97,
				105,
				108,
				101,
				114
			});
			KeywordEntries.Entries.Add(Keyword.StartXRef, new byte[]
			{
				115,
				116,
				97,
				114,
				116,
				120,
				114,
				101,
				102
			});
			KeywordEntries.Entries.Add(Keyword.Eof, new byte[]
			{
				37,
				37,
				69,
				79,
				70
			});
			KeywordEntries.Entries.Add(Keyword.BT, new byte[]
			{
				66,
				84
			});
			KeywordEntries.Entries.Add(Keyword.ET, new byte[]
			{
				69,
				84
			});
			KeywordEntries.Entries.Add(Keyword.Tf, new byte[]
			{
				84,
				102
			});
			KeywordEntries.Entries.Add(Keyword.Td, new byte[]
			{
				84,
				100
			});
			KeywordEntries.Entries.Add(Keyword.Tr, new byte[]
			{
				84,
				114
			});
			KeywordEntries.Entries.Add(Keyword.Tj, new byte[]
			{
				84,
				106
			});
		}

		// Token: 0x0600DDF0 RID: 56816 RVA: 0x00307B0B File Offset: 0x00305D0B
		private KeywordEntries()
		{
		}

		// Token: 0x0600DDF1 RID: 56817 RVA: 0x00307B13 File Offset: 0x00305D13
		public static byte[] GetKeyword(Keyword keyword)
		{
			return (byte[])KeywordEntries.Entries[keyword];
		}

		// Token: 0x04003F3B RID: 16187
		private static readonly IDictionary Entries = new Hashtable();
	}
}
