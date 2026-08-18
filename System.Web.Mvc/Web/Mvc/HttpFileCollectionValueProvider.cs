using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System.Web.Mvc
{
	// Token: 0x02000110 RID: 272
	public sealed class HttpFileCollectionValueProvider : DictionaryValueProvider<HttpPostedFileBase[]>
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00013A58 File Offset: 0x00011C58
		public HttpFileCollectionValueProvider(ControllerContext controllerContext) : base(HttpFileCollectionValueProvider.GetHttpPostedFileDictionary(controllerContext), CultureInfo.InvariantCulture)
		{
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00013A90 File Offset: 0x00011C90
		private static Dictionary<string, HttpPostedFileBase[]> GetHttpPostedFileDictionary(ControllerContext controllerContext)
		{
			HttpFileCollectionBase files = controllerContext.HttpContext.Request.Files;
			if (files.Count == 0)
			{
				return HttpFileCollectionValueProvider._emptyDictionary;
			}
			List<KeyValuePair<string, HttpPostedFileBase>> list = new List<KeyValuePair<string, HttpPostedFileBase>>();
			string[] allKeys = files.AllKeys;
			for (int i = 0; i < files.Count; i++)
			{
				string text = allKeys[i];
				if (text != null)
				{
					HttpPostedFileBase value = HttpPostedFileBaseModelBinder.ChooseFileOrNull(files[i]);
					list.Add(new KeyValuePair<string, HttpPostedFileBase>(text, value));
				}
			}
			IEnumerable<IGrouping<string, HttpPostedFileBase>> source = list.GroupBy((KeyValuePair<string, HttpPostedFileBase> el) => el.Key, (KeyValuePair<string, HttpPostedFileBase> el) => el.Value, StringComparer.OrdinalIgnoreCase);
			return source.ToDictionary((IGrouping<string, HttpPostedFileBase> g) => g.Key, (IGrouping<string, HttpPostedFileBase> g) => g.ToArray<HttpPostedFileBase>(), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04000208 RID: 520
		private static readonly Dictionary<string, HttpPostedFileBase[]> _emptyDictionary = new Dictionary<string, HttpPostedFileBase[]>();
	}
}
