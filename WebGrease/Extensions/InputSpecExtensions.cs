using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WebGrease.Configuration;

namespace WebGrease.Extensions
{
	// Token: 0x020000FC RID: 252
	public static class InputSpecExtensions
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x00049690 File Offset: 0x00047890
		public static IEnumerable<string> GetFiles(this IEnumerable<InputSpec> inputs, string rootPath, LogManager log = null, bool throwWhenMissingAndNotOptional = false)
		{
			return (from _ in inputs
			where _ != null && !string.IsNullOrWhiteSpace(_.Path)
			select _).SelectMany((InputSpec i) => i.GetFiles(rootPath, log, throwWhenMissingAndNotOptional));
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000496F0 File Offset: 0x000478F0
		public static IEnumerable<string> GetFiles(this InputSpec input, string rootPath = null, LogManager log = null, bool throwWhenMissingAndNotOptional = false)
		{
			List<string> list = new List<string>();
			string text = Path.Combine(rootPath ?? string.Empty, input.Path);
			if (File.Exists(text))
			{
				if (log != null)
				{
					log.Information("- {0}".InvariantFormat(new object[]
					{
						text
					}), MessageImportance.Normal);
				}
				list.Add(text);
			}
			else
			{
				if (Directory.Exists(text))
				{
					if (log != null)
					{
						log.Information("Folder: {0}, Pattern: {1}, Options: {2}".InvariantFormat(new object[]
						{
							text,
							input.SearchPattern,
							input.SearchOption
						}), MessageImportance.Normal);
					}
					list.AddRange(Directory.EnumerateFiles(text, string.IsNullOrWhiteSpace(input.SearchPattern) ? "*.*" : input.SearchPattern, input.SearchOption).OrderBy((string name) => name, StringComparer.OrdinalIgnoreCase));
					if (log == null)
					{
						return list;
					}
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text2 = enumerator.Current;
							log.Information("- {0}".InvariantFormat(new object[]
							{
								text2
							}), MessageImportance.Normal);
						}
						return list;
					}
				}
				if (!input.IsOptional && throwWhenMissingAndNotOptional)
				{
					throw new FileNotFoundException("Could not find the file for non option input spec: Path:{0}, SearchPattern:{1}, Options:{2}".InvariantFormat(new object[]
					{
						text,
						input.SearchPattern,
						input.SearchOption
					}), text);
				}
			}
			return list;
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00049890 File Offset: 0x00047A90
		internal static void AddInputSpecs(this IList<InputSpec> inputSpecs, string sourceDirectory, XElement element)
		{
			foreach (XElement element2 in element.Descendants())
			{
				InputSpec inputSpec = new InputSpec(element2, sourceDirectory);
				if (!string.IsNullOrWhiteSpace(inputSpec.Path))
				{
					inputSpecs.Add(inputSpec);
				}
			}
		}
	}
}
