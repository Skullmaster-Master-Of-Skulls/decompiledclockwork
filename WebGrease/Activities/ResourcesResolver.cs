using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;

namespace WebGrease.Activities
{
	// Token: 0x02000044 RID: 68
	internal sealed class ResourcesResolver
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		private ResourcesResolver(IWebGreaseContext context, string inputContentDirectory, string resourceGroupKey, string applicationDirectoryName, string siteName, IEnumerable<string> resourceKeys, string outputDirectoryPath)
		{
			ResourcesResolver.<>c__DisplayClass4 CS$<>8__locals1 = new ResourcesResolver.<>c__DisplayClass4();
			CS$<>8__locals1.context = context;
			CS$<>8__locals1.resourceGroupKey = resourceGroupKey;
			CS$<>8__locals1.applicationDirectoryName = applicationDirectoryName;
			CS$<>8__locals1.siteName = siteName;
			CS$<>8__locals1.resourceKeys = resourceKeys;
			CS$<>8__locals1.outputDirectoryPath = outputDirectoryPath;
			this.resourceDirectoryPaths = new List<ResourceDirectoryPath>();
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			DirectoryInfo contentDirectoryInfo = new DirectoryInfo(inputContentDirectory);
			Safe.FileLock(contentDirectoryInfo, delegate()
			{
				foreach (DirectoryInfo directoryInfo in contentDirectoryInfo.EnumerateDirectories())
				{
					if (string.Compare(directoryInfo.Name, CS$<>8__locals1.applicationDirectoryName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						string path = Path.Combine(directoryInfo.FullName, CS$<>8__locals1.siteName ?? string.Empty);
						if (!Directory.Exists(path))
						{
							continue;
						}
						using (IEnumerator<DirectoryInfo> enumerator2 = new DirectoryInfo(path).EnumerateDirectories(CS$<>8__locals1.resourceGroupKey, SearchOption.AllDirectories).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								DirectoryInfo directoryInfo2 = enumerator2.Current;
								CS$<>8__locals1.<>4__this.resourceDirectoryPaths.Add(new ResourceDirectoryPath
								{
									AllowOverrides = true,
									Directory = directoryInfo2.FullName
								});
								CS$<>8__locals1.context.Cache.CurrentCacheSection.AddSourceDependency(directoryInfo2.FullName, "*.resx", SearchOption.TopDirectoryOnly);
							}
							continue;
						}
					}
					foreach (DirectoryInfo directoryInfo3 in directoryInfo.EnumerateDirectories(CS$<>8__locals1.resourceGroupKey, SearchOption.AllDirectories))
					{
						CS$<>8__locals1.<>4__this.resourceDirectoryPaths.Add(new ResourceDirectoryPath
						{
							AllowOverrides = false,
							Directory = directoryInfo3.FullName
						});
						CS$<>8__locals1.context.Cache.CurrentCacheSection.AddSourceDependency(directoryInfo3.FullName, "*.resx", SearchOption.TopDirectoryOnly);
					}
				}
				CS$<>8__locals1.<>4__this.outputDirectoryPath = CS$<>8__locals1.outputDirectoryPath;
				ResourcesResolver <>4__this = CS$<>8__locals1.<>4__this;
				IEnumerable<string> enumerable;
				if ((enumerable = CS$<>8__locals1.resourceKeys) == null)
				{
					enumerable = new List<string>
					{
						"generic-generic"
					};
				}
				<>4__this.resourceKeys = enumerable;
			});
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000CC49 File Offset: 0x0000AE49
		internal static ResourcesResolver Factory(IWebGreaseContext context, string inputContentDirectory, string resourceGroupKey, string applicationDirectoryName, string siteName, IEnumerable<string> resourceKeys, string outputDirectoryPath)
		{
			return new ResourcesResolver(context, inputContentDirectory, resourceGroupKey, applicationDirectoryName, siteName, resourceKeys, outputDirectoryPath);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		internal IDictionary<string, IDictionary<string, string>> GetMergedResources()
		{
			Dictionary<string, IDictionary<string, string>> dictionary = new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in this.resourceKeys)
			{
				string text2 = text.Trim().ToLower(CultureInfo.InvariantCulture);
				dictionary.Add(text2, this.GetResources(text, text2));
			}
			return dictionary;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		internal void ResolveHierarchy()
		{
			foreach (string text in this.resourceKeys)
			{
				string text2 = text.Trim().ToLower(CultureInfo.InvariantCulture);
				SortedDictionary<string, string> resources = this.GetResources(text, text2);
				ResourcesResolver.WriteResources(this.outputDirectoryPath, text2, resources);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000CD48 File Offset: 0x0000AF48
		private SortedDictionary<string, string> GetResources(string resourceKey, string localeOrThemeName)
		{
			SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (ResourceDirectoryPath resourceDirectoryPath2 in from resourceDirectoryPath in this.resourceDirectoryPaths
			orderby resourceDirectoryPath.AllowOverrides
			select resourceDirectoryPath)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(resourceDirectoryPath2.Directory);
				Dictionary<string, string> input = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				if (resourceKey != "generic-generic")
				{
					string text = Path.Combine(directoryInfo.FullName, "generic-generic.resx");
					if (File.Exists(text))
					{
						input = ResourcesResolver.ReadResources(text);
					}
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				string text2 = Path.Combine(directoryInfo.FullName, localeOrThemeName + ".resx");
				if (File.Exists(text2))
				{
					dictionary = ResourcesResolver.ReadResources(text2);
				}
				ResourcesResolver.MergeResources(dictionary, input, false, false);
				ResourcesResolver.MergeResources(sortedDictionary, dictionary, resourceDirectoryPath2.AllowOverrides, resourceDirectoryPath2.AllowOverrides);
			}
			return sortedDictionary;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000CE64 File Offset: 0x0000B064
		internal static Dictionary<string, string> ReadResources(string filePath)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			using (ResXResourceReader resXResourceReader = new ResXResourceReader(filePath))
			{
				foreach (object obj in resXResourceReader)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string text = dictionaryEntry.Key as string;
					if (!string.IsNullOrWhiteSpace(text))
					{
						string value = (dictionaryEntry.Value as string) ?? string.Empty;
						if (dictionary.ContainsKey(text))
						{
							throw new BuildWorkflowException(string.Format(CultureInfo.CurrentCulture, ResourceStrings.ResourceResolverDuplicateKeyExceptionMessage, new object[]
							{
								text,
								filePath
							}));
						}
						dictionary.Add(text, value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000CF88 File Offset: 0x0000B188
		internal static string ExpandResourceKeys(string input, IDictionary<string, string> resources)
		{
			if (input == null || resources == null || resources.Count == 0)
			{
				return input;
			}
			return ResourcesResolver.LocalizationResourceKeyRegex.Replace(input, delegate(Match match)
			{
				string key = match.Result("$1");
				string result;
				if (!resources.TryGetValue(key, out result))
				{
					return match.Value;
				}
				return result;
			});
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		private static void MergeResources(IDictionary<string, string> output, Dictionary<string, string> input, bool allowOverrides, bool throwsException)
		{
			foreach (string text in input.Keys)
			{
				if (output.ContainsKey(text))
				{
					if (allowOverrides)
					{
						output[text] = input[text];
					}
					else if (throwsException)
					{
						throw new ResourceOverrideException(null, text);
					}
				}
				else
				{
					output.Add(text, input[text]);
				}
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000D058 File Offset: 0x0000B258
		private static void WriteResources(string outputDirectoryPath, string key, IDictionary<string, string> resources)
		{
			if (resources == null || resources.Count == 0)
			{
				return;
			}
			Directory.CreateDirectory(outputDirectoryPath);
			using (ResXResourceWriter resXResourceWriter = new ResXResourceWriter(Path.Combine(outputDirectoryPath, key + ".resx")))
			{
				foreach (string text in resources.Keys)
				{
					resXResourceWriter.AddResource(text, resources[text]);
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000D14C File Offset: 0x0000B34C
		public static IEnumerable<Tuple<List<string>, Dictionary<string, string>>> GetGroupedUsedResourceKeys(string css, IDictionary<string, IDictionary<string, string>> resources)
		{
			HashSet<string> @object = new HashSet<string>(resources.Values.SelectMany((IDictionary<string, string> v) => v.Keys).Distinct<string>());
			HashSet<string> usedResourceKeys = new HashSet<string>((from rk in (from m in ResourcesResolver.LocalizationResourceKeyRegex.Matches(css).OfType<Match>()
			select m.Groups[1].Value).Where(new Func<string, bool>(@object.Contains))
			orderby rk
			select rk).ToArray<string>());
			Dictionary<string, Tuple<List<string>, Dictionary<string, string>>> dictionary = new Dictionary<string, Tuple<List<string>, Dictionary<string, string>>>(StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<string, IDictionary<string, string>> keyValuePair in resources)
			{
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>((from kvp in keyValuePair.Value
				where usedResourceKeys.Contains(kvp.Key)
				select kvp).ToDictionary((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value), StringComparer.OrdinalIgnoreCase);
				string key = string.Join("%", from kv in dictionary2
				select kv.ToString());
				Tuple<List<string>, Dictionary<string, string>> tuple;
				if (!dictionary.TryGetValue(key, out tuple))
				{
					tuple = new Tuple<List<string>, Dictionary<string, string>>(new List<string>(), dictionary2);
					dictionary.Add(key, tuple);
				}
				tuple.Item1.Add(keyValuePair.Key);
			}
			return dictionary.Values;
		}

		// Token: 0x040000E6 RID: 230
		internal static readonly Regex LocalizationResourceKeyRegex = new Regex("%([-./\\w_]+)(\\:\\w*)?%", RegexOptions.Compiled);

		// Token: 0x040000E7 RID: 231
		private readonly List<ResourceDirectoryPath> resourceDirectoryPaths;

		// Token: 0x040000E8 RID: 232
		private string outputDirectoryPath;

		// Token: 0x040000E9 RID: 233
		private IEnumerable<string> resourceKeys;
	}
}
