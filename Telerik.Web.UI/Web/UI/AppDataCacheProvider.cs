using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E7A RID: 3706
	public class AppDataCacheProvider : WebResourceCacheProvider
	{
		// Token: 0x06008C82 RID: 35970 RVA: 0x001FE3A7 File Offset: 0x001FC5A7
		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);
			this.Initialize(config["appDataSubFolder"]);
		}

		// Token: 0x06008C83 RID: 35971 RVA: 0x001FE3C2 File Offset: 0x001FC5C2
		public override void Initialize()
		{
			this.Initialize(string.Empty);
		}

		// Token: 0x06008C84 RID: 35972 RVA: 0x001FE3D0 File Offset: 0x001FC5D0
		public void Initialize(string appDataRelativePath)
		{
			string str = "~/App_Data";
			string text = string.IsNullOrEmpty(appDataRelativePath) ? "/CombinedScriptsCache" : appDataRelativePath;
			string path = str + text.TrimEnd(new char[]
			{
				'/'
			});
			this._cachePath = HttpContext.Current.Server.MapPath(path);
			this._cacheIndexPath = this._cachePath + "\\" + "cacheIndex.json";
			if (!Directory.Exists(this._cachePath))
			{
				Directory.CreateDirectory(this._cachePath);
			}
			this.LoadCacheIndex();
			base.IsInitialized = true;
		}

		// Token: 0x06008C85 RID: 35973 RVA: 0x001FE468 File Offset: 0x001FC668
		private void LoadCacheIndex()
		{
			if (File.Exists(this._cacheIndexPath))
			{
				using (StreamReader streamReader = File.OpenText(this._cacheIndexPath))
				{
					JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
					this._cacheIndex = javaScriptSerializer.Deserialize<Hashtable>(streamReader.ReadToEnd());
					this.LoadKeyIndex(javaScriptSerializer);
					this.LoadUrlIndex(javaScriptSerializer);
					return;
				}
			}
			this.InitializeNewCacheIndex();
		}

		// Token: 0x06008C86 RID: 35974 RVA: 0x001FE4D8 File Offset: 0x001FC6D8
		private void InitializeNewCacheIndex()
		{
			this._cacheIndex = new Hashtable();
			this._keyIndex = new Hashtable();
			this._cacheIndex["keyIndex"] = this._keyIndex;
			this._urlIndex = new Hashtable();
			this._cacheIndex["urlIndex"] = this._urlIndex;
		}

		// Token: 0x06008C87 RID: 35975 RVA: 0x001FE534 File Offset: 0x001FC734
		private void LoadKeyIndex(JavaScriptSerializer serializer)
		{
			if (this._cacheIndex != null && this._cacheIndex["keyIndex"] != null)
			{
				this._keyIndex = serializer.ConvertToType<Hashtable>(this._cacheIndex["keyIndex"]);
			}
			else
			{
				this._keyIndex = new Hashtable();
			}
			this._cacheIndex["keyIndex"] = this._keyIndex;
		}

		// Token: 0x06008C88 RID: 35976 RVA: 0x001FE59C File Offset: 0x001FC79C
		private void LoadUrlIndex(JavaScriptSerializer serializer)
		{
			if (this._cacheIndex != null && this._cacheIndex["urlIndex"] != null)
			{
				this._urlIndex = serializer.ConvertToType<Hashtable>(this._cacheIndex["urlIndex"]);
			}
			else
			{
				this._urlIndex = new Hashtable();
			}
			this._cacheIndex["urlIndex"] = this._urlIndex;
		}

		// Token: 0x06008C89 RID: 35977 RVA: 0x001FE604 File Offset: 0x001FC804
		private void UpdateCacheIndex()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			string value = javaScriptSerializer.Serialize(this._cacheIndex);
			using (StreamWriter streamWriter = File.CreateText(this._cacheIndexPath))
			{
				streamWriter.Write(value);
			}
		}

		// Token: 0x06008C8A RID: 35978 RVA: 0x001FE654 File Offset: 0x001FC854
		public override void Store(string resourceUid, string output)
		{
			Guid guid = Guid.NewGuid();
			using (FileStream fileStream = File.Create(this._cachePath + "/" + guid))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					streamWriter.Write(output);
				}
			}
			this._urlIndex.Add(resourceUid, guid.ToString());
			this.UpdateCacheIndex();
		}

		// Token: 0x06008C8B RID: 35979 RVA: 0x001FE6E8 File Offset: 0x001FC8E8
		public override void Associate(string pageKey, string resourceUid)
		{
			if (!this._keyIndex.ContainsKey(pageKey))
			{
				object[] value = new object[]
				{
					resourceUid
				};
				this._keyIndex.Add(pageKey, value);
			}
			else
			{
				List<string> urlList = this.GetUrlList(pageKey);
				urlList.Add(resourceUid);
				this._keyIndex[pageKey] = Array.ConvertAll<string, object>(urlList.ToArray(), (string s) => s);
			}
			this.UpdateCacheIndex();
		}

		// Token: 0x06008C8C RID: 35980 RVA: 0x001FE768 File Offset: 0x001FC968
		public override string Get(string resourceUid)
		{
			Guid guid = new Guid((string)this._urlIndex[resourceUid]);
			string path = this._cachePath + "/" + guid;
			string result = string.Empty;
			if (File.Exists(path))
			{
				using (StreamReader streamReader = File.OpenText(path))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x06008C8D RID: 35981 RVA: 0x001FE7E0 File Offset: 0x001FC9E0
		public override bool Exists(string resourceUid)
		{
			return this._urlIndex.ContainsKey(resourceUid);
		}

		// Token: 0x06008C8E RID: 35982 RVA: 0x001FE7F0 File Offset: 0x001FC9F0
		public override bool AreAssociated(string pageKey, string resourceUid)
		{
			if (!this._keyIndex.ContainsKey(pageKey))
			{
				return false;
			}
			List<string> urlList = this.GetUrlList(pageKey);
			return urlList.Contains(resourceUid);
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x001FE81C File Offset: 0x001FCA1C
		private List<string> GetUrlList(string pageKey)
		{
			object[] array = (object[])this._keyIndex[pageKey];
			return new List<string>(Array.ConvertAll<object, string>(array, new Converter<object, string>(Convert.ToString)));
		}

		// Token: 0x06008C90 RID: 35984 RVA: 0x001FE854 File Offset: 0x001FCA54
		public override void Invalidate(string pageKey)
		{
			List<string> urlList = this.GetUrlList(pageKey);
			foreach (string key in urlList)
			{
				this._urlIndex.Remove(key);
			}
			this._keyIndex.Remove(pageKey);
			this.UpdateCacheIndex();
		}

		// Token: 0x06008C91 RID: 35985 RVA: 0x001FE8C4 File Offset: 0x001FCAC4
		public override void Invalidate()
		{
			this._cacheIndex.Clear();
			this.UpdateCacheIndex();
		}

		// Token: 0x04002774 RID: 10100
		private string _cachePath;

		// Token: 0x04002775 RID: 10101
		private string _cacheIndexPath;

		// Token: 0x04002776 RID: 10102
		private Hashtable _cacheIndex;

		// Token: 0x04002777 RID: 10103
		private Hashtable _keyIndex;

		// Token: 0x04002778 RID: 10104
		private Hashtable _urlIndex;
	}
}
