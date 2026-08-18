using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace TechnoPro.ClockWorkWeb.Common.ValidationAttributes
{
	// Token: 0x02000159 RID: 345
	public class FileTypesAttribute : ValidationAttribute
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x0004898B File Offset: 0x00046B8B
		public FileTypesAttribute(string types)
		{
			this._types = types.Split(new char[]
			{
				','
			}).ToList<string>();
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000489B4 File Offset: 0x00046BB4
		public override bool IsValid(object value)
		{
			bool flag = value == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				string value2 = Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);
				result = this._types.Contains(value2, StringComparer.OrdinalIgnoreCase);
			}
			return result;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000489FC File Offset: 0x00046BFC
		public override string FormatErrorMessage(string name)
		{
			return string.Format("Invalid file type. Only the following types {0} are supported.", string.Join(", ", this._types));
		}

		// Token: 0x0400080E RID: 2062
		private readonly List<string> _types;
	}
}
