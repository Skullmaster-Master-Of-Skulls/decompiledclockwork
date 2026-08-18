using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.SessionState;

namespace AjaxControlToolkit
{
	// Token: 0x0200003B RID: 59
	internal class PersistentStoreManager
	{
		// Token: 0x06000200 RID: 512 RVA: 0x00007233 File Offset: 0x00005433
		private PersistentStoreManager()
		{
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000723B File Offset: 0x0000543B
		public static PersistentStoreManager Instance
		{
			get
			{
				return PersistentStoreManager.InstanceInitializer.instance;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00007242 File Offset: 0x00005442
		// (set) Token: 0x06000203 RID: 515 RVA: 0x0000724A File Offset: 0x0000544A
		public string ExtendedFileUploadGUID
		{
			get
			{
				return this._extendedFileUploadGUID;
			}
			set
			{
				this._extendedFileUploadGUID = value;
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007253 File Offset: 0x00005453
		public string GetFullID(string controlId)
		{
			return this._extendedFileUploadGUID + "~!~" + controlId;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00007268 File Offset: 0x00005468
		public void ClearAllFilesFromSession(string controlId)
		{
			HttpContext currentContext = this.GetCurrentContext();
			if (currentContext == null)
			{
				return;
			}
			Collection<string> collection = new Collection<string>();
			foreach (object obj in currentContext.Session.Keys)
			{
				string text = (string)obj;
				if (text.StartsWith(this._extendedFileUploadGUID))
				{
					collection.Add(text);
				}
			}
			foreach (string name in collection)
			{
				currentContext.Session.Remove(name);
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007330 File Offset: 0x00005530
		public void RemoveFileFromSession(string controlId)
		{
			HttpContext currentContext = this.GetCurrentContext();
			if (currentContext == null)
			{
				return;
			}
			Collection<string> collection = new Collection<string>();
			foreach (object obj in currentContext.Session.Keys)
			{
				string text = (string)obj;
				if (text.StartsWith(this.GetFullID(controlId)))
				{
					collection.Add(text);
				}
			}
			foreach (string name in collection)
			{
				currentContext.Session.Remove(name);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000073F8 File Offset: 0x000055F8
		public void AddFileToSession(string controlId, string filename, HttpPostedFile fileUpload)
		{
			if (fileUpload == null)
			{
				throw new ArgumentNullException("fileUpload");
			}
			if (string.IsNullOrEmpty(controlId))
			{
				throw new ArgumentException("controlId cannot be empty", "controlId");
			}
			HttpContext currentContext = this.GetCurrentContext();
			if (currentContext == null)
			{
				return;
			}
			SessionStateMode mode = currentContext.Session.Mode;
			if (mode != SessionStateMode.InProc)
			{
				throw new InvalidOperationException("The AsyncFileUpload control only supports session state mode \"InProc\" when persisting files in session.");
			}
			currentContext.Session.Add(this.GetFullID(controlId), fileUpload);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007464 File Offset: 0x00005664
		public bool FileExists(string controlId)
		{
			if (string.IsNullOrEmpty(controlId))
			{
				throw new ArgumentException("controlId cannot be empty", "controlId");
			}
			HttpContext currentContext = this.GetCurrentContext();
			return currentContext != null && currentContext.Session[this.GetFullID(controlId)] != null && currentContext.Session[this.GetFullID(controlId)] is HttpPostedFile;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000074C8 File Offset: 0x000056C8
		public string GetFileName(string controlId)
		{
			if (string.IsNullOrEmpty(controlId))
			{
				throw new ArgumentException("controlId cannot be empty", "controlId");
			}
			HttpContext currentContext = this.GetCurrentContext();
			if (currentContext == null)
			{
				return string.Empty;
			}
			HttpPostedFile httpPostedFile = currentContext.Session[this.GetFullID(controlId)] as HttpPostedFile;
			if (httpPostedFile == null)
			{
				return string.Empty;
			}
			return httpPostedFile.FileName;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007524 File Offset: 0x00005724
		public string GetContentType(string controlId)
		{
			if (string.IsNullOrEmpty(controlId))
			{
				throw new ArgumentException("controlId cannot be empty", "controlId");
			}
			HttpContext currentContext = this.GetCurrentContext();
			if (currentContext == null)
			{
				return string.Empty;
			}
			HttpPostedFile httpPostedFile = currentContext.Session[this.GetFullID(controlId)] as HttpPostedFile;
			if (httpPostedFile == null)
			{
				return string.Empty;
			}
			return httpPostedFile.ContentType;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007580 File Offset: 0x00005780
		public HttpPostedFile GetFileFromSession(string controlId)
		{
			if (string.IsNullOrEmpty(controlId))
			{
				throw new ArgumentException("controlId cannot be empty", "controlId");
			}
			HttpContext currentContext = this.GetCurrentContext();
			if (currentContext == null)
			{
				return null;
			}
			if (currentContext.Session[this.GetFullID(controlId)] == null)
			{
				return null;
			}
			HttpPostedFile httpPostedFile = currentContext.Session[this.GetFullID(controlId)] as HttpPostedFile;
			if (httpPostedFile == null)
			{
				throw new InvalidCastException("postedFile");
			}
			return httpPostedFile;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000075F0 File Offset: 0x000057F0
		public List<HttpPostedFile> GetAllFilesFromSession(string controlId)
		{
			List<HttpPostedFile> list = new List<HttpPostedFile>();
			HttpContext currentContext = this.GetCurrentContext();
			if (currentContext == null)
			{
				return list;
			}
			foreach (object obj in currentContext.Session.Keys)
			{
				string text = (string)obj;
				if (text.StartsWith(this._extendedFileUploadGUID) && HttpContext.Current.Session[text] != null)
				{
					HttpPostedFile httpPostedFile = HttpContext.Current.Session[text] as HttpPostedFile;
					if (httpPostedFile != null)
					{
						list.Add(httpPostedFile);
					}
				}
			}
			return list;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000076A4 File Offset: 0x000058A4
		private HttpContext GetCurrentContext()
		{
			if (HttpContext.Current == null || HttpContext.Current.Session == null)
			{
				return null;
			}
			return HttpContext.Current;
		}

		// Token: 0x040000A6 RID: 166
		private const string _idSeperator = "~!~";

		// Token: 0x040000A7 RID: 167
		private string _extendedFileUploadGUID;

		// Token: 0x0200003C RID: 60
		private class InstanceInitializer
		{
			// Token: 0x040000A8 RID: 168
			internal static readonly PersistentStoreManager instance = new PersistentStoreManager();
		}
	}
}
