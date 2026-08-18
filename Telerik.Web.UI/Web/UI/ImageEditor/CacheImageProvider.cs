using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E8D RID: 3725
	public class CacheImageProvider : ICacheImageProvider
	{
		// Token: 0x17002C8E RID: 11406
		// (get) Token: 0x06008D30 RID: 36144 RVA: 0x00200AC5 File Offset: 0x001FECC5
		// (set) Token: 0x06008D31 RID: 36145 RVA: 0x00200ACD File Offset: 0x001FECCD
		public ImageStorage Storage { get; set; }

		// Token: 0x17002C8F RID: 11407
		// (get) Token: 0x06008D32 RID: 36146 RVA: 0x00200AD6 File Offset: 0x001FECD6
		// (set) Token: 0x06008D33 RID: 36147 RVA: 0x00200ADE File Offset: 0x001FECDE
		public string ImageStorageKey { get; set; }

		// Token: 0x06008D34 RID: 36148 RVA: 0x00200AE7 File Offset: 0x001FECE7
		public CacheImageProvider() : this(ImageStorage.Cache)
		{
		}

		// Token: 0x06008D35 RID: 36149 RVA: 0x00200AF0 File Offset: 0x001FECF0
		public CacheImageProvider(ImageStorage storage) : this(storage, string.Empty)
		{
		}

		// Token: 0x06008D36 RID: 36150 RVA: 0x00200AFE File Offset: 0x001FECFE
		public CacheImageProvider(ImageStorage storage, string imageStorageKey)
		{
			this.Storage = storage;
			this.ImageStorageKey = imageStorageKey;
		}

		// Token: 0x06008D37 RID: 36151 RVA: 0x00200B1C File Offset: 0x001FED1C
		public virtual string Store(EditableImage image)
		{
			string text = Guid.NewGuid().ToString();
			byte[] value = this.ToEditableBytes(image);
			switch (this.Storage)
			{
			case ImageStorage.Cache:
				HttpRuntime.Cache.Add(text, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes((double)this._timeSpanMinutes), CacheItemPriority.NotRemovable, null);
				break;
			case ImageStorage.Session:
				HttpContext.Current.Session[text] = value;
				break;
			case ImageStorage.FileSystem:
				throw new NotImplementedException();
			default:
				return null;
			}
			this.StoreKey(text);
			return text;
		}

		// Token: 0x06008D38 RID: 36152 RVA: 0x00200BA8 File Offset: 0x001FEDA8
		public virtual EditableImage Retrieve(string key)
		{
			switch (this.Storage)
			{
			case ImageStorage.Cache:
				return this.ToEditableImage(HttpRuntime.Cache.Get(key));
			case ImageStorage.Session:
				return this.ToEditableImage(HttpContext.Current.Session[key]);
			case ImageStorage.FileSystem:
				throw new NotImplementedException();
			default:
				throw new KeyNotFoundException("The image was missing from cache");
			}
		}

		// Token: 0x06008D39 RID: 36153 RVA: 0x00200C09 File Offset: 0x001FEE09
		public virtual void ClearImages()
		{
			this.DeleteImages(this.GetKeys(true));
		}

		// Token: 0x06008D3A RID: 36154 RVA: 0x00200C18 File Offset: 0x001FEE18
		public virtual void ClearImages(string imageKey)
		{
			this.DeleteImages(this.GetKeys(false, imageKey));
		}

		// Token: 0x06008D3B RID: 36155 RVA: 0x00200C28 File Offset: 0x001FEE28
		private void DeleteImages(string[] keys)
		{
			if (keys == null)
			{
				return;
			}
			switch (this.Storage)
			{
			case ImageStorage.Cache:
				this.ClearImagesFromCache(keys);
				return;
			case ImageStorage.Session:
				this.ClearImagesFromSession(keys);
				return;
			case ImageStorage.FileSystem:
				this.ClearImagesFromFileSystem(keys);
				return;
			default:
				throw new KeyNotFoundException("The images are already cleared");
			}
		}

		// Token: 0x06008D3C RID: 36156 RVA: 0x00200C78 File Offset: 0x001FEE78
		private void ClearImagesFromCache(string[] keys)
		{
			Cache cache = HttpRuntime.Cache;
			foreach (string key in keys)
			{
				cache.Remove(key);
			}
		}

		// Token: 0x06008D3D RID: 36157 RVA: 0x00200CA8 File Offset: 0x001FEEA8
		private void ClearImagesFromSession(string[] keys)
		{
			HttpSessionState session = HttpContext.Current.Session;
			foreach (string name in keys)
			{
				session.Remove(name);
			}
		}

		// Token: 0x06008D3E RID: 36158 RVA: 0x00200CDB File Offset: 0x001FEEDB
		private void ClearImagesFromFileSystem(string[] keys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008D3F RID: 36159 RVA: 0x00200CE4 File Offset: 0x001FEEE4
		private void StoreKey(string key)
		{
			if (!string.IsNullOrEmpty(this.ImageStorageKey))
			{
				if (this.Storage == ImageStorage.Cache)
				{
					string text = (string)HttpRuntime.Cache.Get(this.ImageStorageKey);
					if (string.IsNullOrEmpty(text))
					{
						text = key;
					}
					else
					{
						text += "," + key;
					}
					HttpRuntime.Cache.Remove(this.ImageStorageKey);
					HttpRuntime.Cache.Add(this.ImageStorageKey, text, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes((double)this._timeSpanMinutes), CacheItemPriority.NotRemovable, null);
					return;
				}
				string text2 = (string)HttpContext.Current.Session[this.ImageStorageKey];
				if (string.IsNullOrEmpty(text2))
				{
					text2 = key;
				}
				else
				{
					text2 += "," + key;
				}
				HttpContext.Current.Session[this.ImageStorageKey] = text2;
			}
		}

		// Token: 0x06008D40 RID: 36160 RVA: 0x00200DC4 File Offset: 0x001FEFC4
		private string[] GetKeys(bool removeKeys)
		{
			return this.GetKeys(removeKeys, string.Empty);
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x00200DD4 File Offset: 0x001FEFD4
		private string[] GetKeys(bool removeKeys, string imageKey)
		{
			string keysString = this.GetKeysString(removeKeys, imageKey);
			if (string.IsNullOrEmpty(keysString))
			{
				return null;
			}
			return keysString.Split(new char[]
			{
				','
			});
		}

		// Token: 0x06008D42 RID: 36162 RVA: 0x00200E08 File Offset: 0x001FF008
		private string GetKeysString(bool removeKeys, string imageKey)
		{
			if (string.IsNullOrEmpty(this.ImageStorageKey))
			{
				return string.Empty;
			}
			if (this.Storage == ImageStorage.Cache)
			{
				string text = (string)HttpRuntime.Cache.Get(this.ImageStorageKey);
				if (!string.IsNullOrEmpty(imageKey))
				{
					int num = text.IndexOf(imageKey);
					string text2 = text.Substring(0, num);
					if (!string.IsNullOrEmpty(text2))
					{
						HttpRuntime.Cache.Remove(this.ImageStorageKey);
						HttpRuntime.Cache.Add(this.ImageStorageKey, text.Substring(num), null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes((double)this._timeSpanMinutes), CacheItemPriority.NotRemovable, null);
						text = text2.Substring(0, text2.Length - 1);
					}
					else
					{
						text = string.Empty;
					}
				}
				if (removeKeys)
				{
					HttpRuntime.Cache.Remove(this.ImageStorageKey);
				}
				return text;
			}
			string text3 = (string)HttpContext.Current.Session[this.ImageStorageKey];
			if (!string.IsNullOrEmpty(imageKey))
			{
				int num2 = text3.IndexOf(imageKey);
				string text4 = text3.Substring(0, num2);
				if (!string.IsNullOrEmpty(text4))
				{
					HttpContext.Current.Session[this.ImageStorageKey] = text3.Substring(num2);
					text3 = text4.Substring(0, text4.Length - 1);
				}
				else
				{
					text3 = string.Empty;
				}
			}
			if (removeKeys)
			{
				HttpContext.Current.Session.Remove(this.ImageStorageKey);
			}
			return text3;
		}

		// Token: 0x06008D43 RID: 36163 RVA: 0x00200F68 File Offset: 0x001FF168
		public virtual string SaveImage(EditableImage editableImage, string physicalPath, string imageUrl, bool overwrite)
		{
			string result;
			try
			{
				Stream file = this.GetFile(physicalPath);
				bool flag = file != null && file.Length > 0L;
				if (flag)
				{
					file.Close();
				}
				if (flag && !overwrite)
				{
					result = "FileExists";
				}
				else
				{
					if (flag)
					{
						string value = this.DeleteFile(physicalPath);
						if (!string.IsNullOrEmpty(value))
						{
							return "MessageCannotWriteToFolder";
						}
					}
					editableImage.Image.Save(physicalPath);
					result = string.Empty;
				}
			}
			catch (Exception)
			{
				result = "MessageCannotWriteToFolder";
			}
			return result;
		}

		// Token: 0x06008D44 RID: 36164 RVA: 0x00200FF4 File Offset: 0x001FF1F4
		protected internal virtual Stream GetFile(string physicalPath)
		{
			physicalPath = Path.GetFullPath(physicalPath);
			if (!File.Exists(physicalPath))
			{
				return null;
			}
			return File.OpenRead(physicalPath);
		}

		// Token: 0x06008D45 RID: 36165 RVA: 0x00201010 File Offset: 0x001FF210
		protected internal virtual string DeleteFile(string physicalPath)
		{
			try
			{
				physicalPath = Path.GetFullPath(physicalPath);
				if (File.Exists(physicalPath))
				{
					if ((File.GetAttributes(physicalPath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						return "FileReadOnly";
					}
					File.Delete(physicalPath);
				}
			}
			catch (UnauthorizedAccessException)
			{
				return "NoPermissionsToDeleteFile";
			}
			return string.Empty;
		}

		// Token: 0x06008D46 RID: 36166 RVA: 0x00201068 File Offset: 0x001FF268
		public virtual Image LoadImage(string imageUrl, string physicalPath, HttpContext context)
		{
			Stream file = this.GetFile(physicalPath);
			if (file == null || file.Length == 0L)
			{
				return null;
			}
			int i = (int)file.Length;
			int num = 0;
			byte[] array = new byte[file.Length];
			while (i > 0)
			{
				int num2 = file.Read(array, num, i);
				num += num2;
				i -= num2;
				if (num2 == 0)
				{
					break;
				}
			}
			if (i > 0)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			memoryStream.Write(array, 0, array.Length);
			file.Close();
			return Image.FromStream(memoryStream);
		}

		// Token: 0x06008D47 RID: 36167 RVA: 0x002010E8 File Offset: 0x001FF2E8
		private byte[] ToEditableBytes(EditableImage editable)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				editable.CopyToStream(memoryStream);
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06008D48 RID: 36168 RVA: 0x00201128 File Offset: 0x001FF328
		private EditableImage ToEditableImage(object data)
		{
			return this.ToEditableImage((byte[])data);
		}

		// Token: 0x06008D49 RID: 36169 RVA: 0x00201138 File Offset: 0x001FF338
		private EditableImage ToEditableImage(byte[] data)
		{
			MemoryStream stream = new MemoryStream(data);
			return new EditableImage(stream);
		}

		// Token: 0x040027A0 RID: 10144
		private readonly int _timeSpanMinutes = 60;
	}
}
