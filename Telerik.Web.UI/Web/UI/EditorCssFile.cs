using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x0200105A RID: 4186
	public class EditorCssFile : EditorValueItem
	{
		// Token: 0x0600A909 RID: 43273 RVA: 0x0024B931 File Offset: 0x00249B31
		public EditorCssFile()
		{
		}

		// Token: 0x0600A90A RID: 43274 RVA: 0x0024B939 File Offset: 0x00249B39
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public EditorCssFile(string path)
		{
			this.Value = path;
		}

		// Token: 0x1700363F RID: 13887
		// (get) Token: 0x0600A90B RID: 43275 RVA: 0x0024B948 File Offset: 0x00249B48
		// (set) Token: 0x0600A90C RID: 43276 RVA: 0x0024B9CF File Offset: 0x00249BCF
		public override string Value
		{
			get
			{
				string text = base.Value;
				if (!string.IsNullOrEmpty(text) && text.StartsWith("~") && HttpContext.Current != null)
				{
					text = text.Replace("\\", "/").Replace("//", "/");
					string text2 = HttpContext.Current.Request.ApplicationPath;
					if (!text2.EndsWith("/"))
					{
						text2 += "/";
					}
					text = text.Replace("~/", text2);
				}
				return text;
			}
			set
			{
				base.Value = value;
			}
		}
	}
}
