using System;

namespace Telerik.Web.UI.Widgets
{
	// Token: 0x0200046C RID: 1132
	public class FilePath
	{
		// Token: 0x06002892 RID: 10386 RVA: 0x000835EB File Offset: 0x000817EB
		public FilePath() : this('/')
		{
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000835F5 File Offset: 0x000817F5
		public FilePath(char pathSeparator)
		{
			this.Separator = pathSeparator;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x00083604 File Offset: 0x00081804
		public string NormalizeRelativePath(string path)
		{
			string path2 = this.AddTrailingSeparator(path);
			return this.NormalizePathInternal(path2);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00083620 File Offset: 0x00081820
		public string AddTrailingSeparator(string path)
		{
			string text = this.Separator.ToString();
			if (!string.IsNullOrEmpty(path) && !path.EndsWith(text))
			{
				path += text;
			}
			return path;
		}

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x00083657 File Offset: 0x00081857
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x0008365F File Offset: 0x0008185F
		public virtual char Separator { get; private set; }

		// Token: 0x06002898 RID: 10392 RVA: 0x00083668 File Offset: 0x00081868
		private string NormalizePathInternal(string path)
		{
			string text = ".." + this.Separator;
			string text2 = this.Separator + ".." + this.Separator;
			string str = string.Empty;
			string text3 = path.StartsWith(text) ? text : (path.StartsWith(text2) ? text2 : string.Empty);
			if (!string.IsNullOrEmpty(text3))
			{
				str = text3;
				path = path.Remove(0, text3.Length);
				return str + this.NormalizePathInternal(path);
			}
			string value = this.Separator + "..";
			int num = path.IndexOf(value);
			if (num == -1)
			{
				return path;
			}
			int num2 = num + 3;
			string text4 = path.Substring(0, num);
			int num3 = text4.LastIndexOf(this.Separator);
			path = path.Remove(num3, num2 - num3);
			return this.NormalizePathInternal(path);
		}
	}
}
