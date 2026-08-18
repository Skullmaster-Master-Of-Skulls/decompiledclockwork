using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000006 RID: 6
	public sealed class ApplicationCollection : ConfigurationElementCollectionBase<Application>
	{
		// Token: 0x06000063 RID: 99 RVA: 0x000032F1 File Offset: 0x000022F1
		internal ApplicationCollection()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000032F9 File Offset: 0x000022F9
		public static char[] InvalidApplicationPathCharacters()
		{
			return SharedGlobals.GetInvalidApplicationPathCharacters();
		}

		// Token: 0x17000039 RID: 57
		public Application this[string path]
		{
			get
			{
				return base.FindElementWithCollectionKey("path", path);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003310 File Offset: 0x00002310
		private string TrimLastPathPart(string path, out string trimmedPart)
		{
			int num = path.LastIndexOfAny(new char[]
			{
				Path.DirectorySeparatorChar,
				Path.AltDirectorySeparatorChar
			});
			if (num != -1)
			{
				trimmedPart = path.Substring(num + 1);
				return path.Substring(0, num);
			}
			trimmedPart = path;
			return string.Empty;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000335C File Offset: 0x0000235C
		private bool DoesVdirPathExist(string path)
		{
			foreach (Application application in this)
			{
				if (string.Equals(application.Path, path, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			string text2;
			string text = this.TrimLastPathPart(path, out text2);
			if (string.IsNullOrEmpty(text))
			{
				text = "/";
			}
			text2 = "/" + text2;
			Application application2 = this[text];
			if (application2 != null)
			{
				VirtualDirectory virtualDirectory = application2.VirtualDirectories[text2];
				if (virtualDirectory != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003400 File Offset: 0x00002400
		public Application Add(string path, string physicalPath)
		{
			ApplicationCollection.ValidatePath(path);
			if (this.DoesVdirPathExist(path))
			{
				throw new InvalidOperationException(Resources.ApplicationPathAlreadyExists);
			}
			Application application = base.CreateElement();
			application["path"] = path;
			base.Add(application);
			application.VirtualDirectories.Add("/", physicalPath);
			return application;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003455 File Offset: 0x00002455
		protected override Application CreateNewElement(string elementTagName)
		{
			return new Application(this._owner, this._site);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003468 File Offset: 0x00002468
		internal void SetValues(ServerManager owner, Site site)
		{
			this._owner = owner;
			this._site = site;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003478 File Offset: 0x00002478
		private static void ValidatePath(string path)
		{
			string text = null;
			if (string.IsNullOrEmpty(path) || path.Trim().Length < 1)
			{
				text = Resources.ApplicationPathLengthValidation;
			}
			else
			{
				char[] array = ApplicationCollection.InvalidApplicationPathCharacters();
				if (path.IndexOfAny(array) != -1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(array[i]);
						if (i < array.Length - 1)
						{
							stringBuilder.Append(", ");
						}
					}
					text = string.Format(CultureInfo.InvariantCulture, Resources.ApplicationPathCannotContainChars, new object[]
					{
						stringBuilder.ToString()
					});
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				throw new FormatException(text);
			}
		}

		// Token: 0x04000017 RID: 23
		private ServerManager _owner;

		// Token: 0x04000018 RID: 24
		private Site _site;
	}
}
