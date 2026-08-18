using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200026F RID: 623
	internal abstract class ParserGrammarReservedWordsAndKeywords
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060018BD RID: 6333
		public abstract string[] ReservedWords { get; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060018BE RID: 6334
		public abstract string[] Keywords { get; }

		// Token: 0x060018BF RID: 6335 RVA: 0x00104770 File Offset: 0x00102970
		public ParserGrammarReservedWordsAndKeywords()
		{
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00104778 File Offset: 0x00102978
		public static string[] ReadCompressedDataFromManifest(string resourceName)
		{
			List<string> list = new List<string>();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resourceName))
			{
				using (GZipStream gzipStream = new GZipStream(manifestResourceStream, CompressionMode.Decompress))
				{
					MemoryStream memoryStream = new MemoryStream();
					gzipStream.CopyTo(memoryStream);
					memoryStream.Position = 0L;
					InputStream inputStream = new InputStream(memoryStream);
					if (inputStream != null)
					{
						while (!inputStream.EndOfStream)
						{
							string text = inputStream.ReadLine().Trim();
							if (!text.StartsWith("#") && text.Length != 0)
							{
								list.Add(text);
							}
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00104838 File Offset: 0x00102A38
		public static string[] ReadDataFromManifest(string resourceName)
		{
			List<string> list = new List<string>();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resourceName))
			{
				InputStream inputStream = new InputStream(manifestResourceStream);
				if (inputStream != null)
				{
					while (!inputStream.EndOfStream)
					{
						string text = inputStream.ReadLine().Trim();
						if (!text.StartsWith("#") && text.Length != 0)
						{
							list.Add(text);
						}
					}
				}
			}
			return list.ToArray();
		}
	}
}
