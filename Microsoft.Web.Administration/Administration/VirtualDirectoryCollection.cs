using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000075 RID: 117
	public sealed class VirtualDirectoryCollection : ConfigurationElementCollectionBase<VirtualDirectory>
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00008D66 File Offset: 0x00007D66
		internal VirtualDirectoryCollection()
		{
		}

		// Token: 0x1700019D RID: 413
		public VirtualDirectory this[string path]
		{
			get
			{
				return base.FindElementWithCollectionKey("path", path);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00008D7C File Offset: 0x00007D7C
		public VirtualDirectory Add(string path, string physicalPath)
		{
			VirtualDirectoryCollection.ValidatePath(path);
			if (this.DoesAppPathExist(path))
			{
				throw new InvalidOperationException(Resources.ApplicationPathAlreadyExists);
			}
			VirtualDirectory virtualDirectory = base.CreateElement();
			virtualDirectory["path"] = path;
			virtualDirectory["physicalPath"] = physicalPath;
			base.Add(virtualDirectory);
			return virtualDirectory;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00008DCB File Offset: 0x00007DCB
		protected override VirtualDirectory CreateNewElement(string elementTagName)
		{
			return new VirtualDirectory(this._parentApplication);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00008DD8 File Offset: 0x00007DD8
		private bool DoesAppPathExist(string path)
		{
			if (path == "/")
			{
				return false;
			}
			string path2 = string.Empty;
			if (string.IsNullOrEmpty(this._parentApplication.Path) || this._parentApplication.Path.Equals("/"))
			{
				path2 = path;
			}
			else
			{
				path2 = this._parentApplication.Path + path;
			}
			return this._parentApplication.Site.Applications[path2] != null;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00008E56 File Offset: 0x00007E56
		public static char[] InvalidVirtualDirectoryPathCharacters()
		{
			return SharedGlobals.GetInvalidVirtualDirectoryPathCharacters();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00008E5D File Offset: 0x00007E5D
		internal void SetParentApplication(Application parentApplication)
		{
			this._parentApplication = parentApplication;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00008E68 File Offset: 0x00007E68
		private static void ValidatePath(string path)
		{
			string text = null;
			if (string.IsNullOrEmpty(path) || path.Trim().Length < 1)
			{
				text = Resources.VirtualDirectoryPathLengthValidation;
			}
			else
			{
				char[] array = VirtualDirectoryCollection.InvalidVirtualDirectoryPathCharacters();
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
					text = string.Format(CultureInfo.InvariantCulture, Resources.VirtualDirectoryPathCannotContainChars, new object[]
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

		// Token: 0x0400012C RID: 300
		private Application _parentApplication;
	}
}
