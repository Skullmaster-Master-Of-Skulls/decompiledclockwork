using System;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000002 RID: 2
	public abstract class WindowsPathUtils
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000010D0
		internal WindowsPathUtils()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000010D8
		public static string DropPathRoot(string path)
		{
			string text = path;
			if (path != null && path.Length > 0)
			{
				if (path[0] == '\\' || path[0] == '/')
				{
					if (path.Length > 1 && (path[1] == '\\' || path[1] == '/'))
					{
						int num = 2;
						int num2 = 2;
						while (num <= path.Length && ((path[num] != '\\' && path[num] != '/') || --num2 > 0))
						{
							num++;
						}
						num++;
						if (num < path.Length)
						{
							text = path.Substring(num);
						}
						else
						{
							text = "";
						}
					}
				}
				else if (path.Length > 1 && path[1] == ':')
				{
					int count = 2;
					if (path.Length > 2 && (path[2] == '\\' || path[2] == '/'))
					{
						count = 3;
					}
					text = text.Remove(0, count);
				}
			}
			return text;
		}
	}
}
