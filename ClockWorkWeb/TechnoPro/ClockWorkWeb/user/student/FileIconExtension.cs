using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechnoPro.Common.Public.Adapters;

namespace TechnoPro.ClockWorkWeb.user.student
{
	// Token: 0x0200007F RID: 127
	public static class FileIconExtension
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x000207C8 File Offset: 0x0001E9C8
		public static string GetFileIconClassFromFilename(this string filename)
		{
			string ext = Path.GetExtension(filename).ToLower();
			Func<string, bool> <>9__2;
			var <>f__AnonymousType = (from m in (eFileIconType[])Enum.GetValues(typeof(eFileIconType))
			select new
			{
				Enum = m,
				Attr = m.GetAttribute<FileIconTypeAttribute>()
			}).FirstOrDefault(delegate(g)
			{
				bool result;
				if (g.Attr != null && g.Attr.FileExtensions != null)
				{
					IEnumerable<string> fileExtensions = g.Attr.FileExtensions;
					Func<string, bool> predicate;
					if ((predicate = <>9__2) == null)
					{
						predicate = (<>9__2 = ((string n) => n.Equals(ext)));
					}
					result = fileExtensions.Any(predicate);
				}
				else
				{
					result = false;
				}
				return result;
			});
			string text;
			if (<>f__AnonymousType == null)
			{
				text = null;
			}
			else
			{
				FileIconTypeAttribute attr = <>f__AnonymousType.Attr;
				text = ((attr != null) ? attr.CssClass : null);
			}
			return text ?? eFileIconType.File.GetAttribute<FileIconTypeAttribute>().CssClass;
		}
	}
}
