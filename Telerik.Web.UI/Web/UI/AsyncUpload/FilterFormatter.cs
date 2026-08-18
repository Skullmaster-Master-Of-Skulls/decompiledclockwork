using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x0200006E RID: 110
	public class FilterFormatter : IFilterFormatter
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0000B92C File Offset: 0x00009B2C
		public string[] Format(FileFilterCollection filters)
		{
			List<string> list = new List<string>();
			foreach (object obj in filters)
			{
				FileFilter fileFilter = (FileFilter)obj;
				if (string.IsNullOrEmpty(fileFilter.Description))
				{
					fileFilter.Description = FileFilter.GetFilter(fileFilter.Extensions, true);
				}
				list.Add(FileFilter.GetFilter(fileFilter.Extensions, false));
			}
			return list.ToArray();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public string Serialize(FileFilterCollection filters, bool format)
		{
			if (format)
			{
				this.Format(filters);
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new FileFilterConverter()
			});
			return javaScriptSerializer.Serialize(filters);
		}
	}
}
