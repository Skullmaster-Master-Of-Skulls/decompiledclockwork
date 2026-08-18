using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease.Activities
{
	// Token: 0x02000003 RID: 3
	internal static class ResourcePivotActivity
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002180 File Offset: 0x00000380
		internal static IEnumerable<ContentItem> ApplyResourceKeys(ContentItem inputItem, Dictionary<string, IDictionary<string, IDictionary<string, string>>> mergedResoures)
		{
			if (mergedResoures == null || !mergedResoures.Any<KeyValuePair<string, IDictionary<string, IDictionary<string, string>>>>())
			{
				return new ContentItem[]
				{
					inputItem
				};
			}
			List<ContentItem> list = new List<ContentItem>();
			try
			{
				string content = inputItem.Content;
				Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> usedGroupedResources = ResourcePivotActivity.GetUsedGroupedResources(content, mergedResoures);
				foreach (KeyValuePair<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> keyValuePair in usedGroupedResources)
				{
					string text = content;
					foreach (KeyValuePair<string, IDictionary<string, string>> keyValuePair2 in keyValuePair.Value)
					{
						text = ResourcesResolver.ExpandResourceKeys(text, keyValuePair2.Value);
					}
					list.Add(ContentItem.FromContent(text, inputItem, keyValuePair.Key));
				}
			}
			catch (ResourceOverrideException ex)
			{
				string message = string.Format(CultureInfo.CurrentUICulture, ResourceStrings.ResourcePivotActivityDuplicateKeysError, new object[]
				{
					ex.TokenKey
				});
				throw new WorkflowException(message, ex);
			}
			catch (Exception inner)
			{
				throw new WorkflowException(ResourceStrings.ResourcePivotActivityError, inner);
			}
			return list;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022B8 File Offset: 0x000004B8
		internal static Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> GetUsedGroupedResources(string content, Dictionary<string, IDictionary<string, IDictionary<string, string>>> mergedResoures)
		{
			Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> dictionary = new Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>>
			{
				{
					new ResourcePivotKey[0],
					new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase)
				}
			};
			if (mergedResoures == null || !mergedResoures.Any<KeyValuePair<string, IDictionary<string, IDictionary<string, string>>>>())
			{
				return dictionary;
			}
			foreach (KeyValuePair<string, IDictionary<string, IDictionary<string, string>>> keyValuePair in mergedResoures)
			{
				dictionary = ResourcePivotActivity.GetUsedGroupedResources(dictionary, content, keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000235C File Offset: 0x0000055C
		private static Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> GetUsedGroupedResources(Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> groupedAndUsedResources, string content, string resourcePivotGroupKey, IDictionary<string, IDictionary<string, string>> resourcePivotKeyValues)
		{
			if (resourcePivotKeyValues == null || !resourcePivotKeyValues.Any<KeyValuePair<string, IDictionary<string, string>>>())
			{
				return groupedAndUsedResources;
			}
			Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> dictionary = new Dictionary<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>>();
			IEnumerable<Tuple<List<string>, Dictionary<string, string>>> groupedUsedResourceKeys = ResourcesResolver.GetGroupedUsedResourceKeys(content, resourcePivotKeyValues);
			foreach (Tuple<List<string>, Dictionary<string, string>> tuple in groupedUsedResourceKeys)
			{
				foreach (KeyValuePair<ResourcePivotKey[], IDictionary<string, IDictionary<string, string>>> keyValuePair in groupedAndUsedResources)
				{
					IEnumerable<ResourcePivotKey> second = from key in tuple.Item1
					select new ResourcePivotKey(resourcePivotGroupKey, key);
					ResourcePivotKey[] key2 = keyValuePair.Key.Concat(second).ToArray<ResourcePivotKey>();
					Dictionary<string, IDictionary<string, string>> dictionary2 = new Dictionary<string, IDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
					dictionary2.AddRange(keyValuePair.Value);
					dictionary2.Add(resourcePivotGroupKey, tuple.Item2);
					dictionary.Add(key2, dictionary2);
				}
			}
			return dictionary;
		}
	}
}
