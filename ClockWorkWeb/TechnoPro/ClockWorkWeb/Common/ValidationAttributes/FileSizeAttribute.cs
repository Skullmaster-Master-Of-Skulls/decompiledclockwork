using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TechnoPro.ClockWorkWeb.Common.ValidationAttributes
{
	// Token: 0x02000158 RID: 344
	public class FileSizeAttribute : ValidationAttribute
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x0004891C File Offset: 0x00046B1C
		public FileSizeAttribute(int maxSize)
		{
			this._maxSize = maxSize;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00048930 File Offset: 0x00046B30
		public override bool IsValid(object value)
		{
			bool flag = value == null;
			return flag || (value as HttpPostedFileBase).ContentLength <= this._maxSize;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00048964 File Offset: 0x00046B64
		public override string FormatErrorMessage(string name)
		{
			return string.Format("The file size should not exceed {0}", this._maxSize);
		}

		// Token: 0x0400080D RID: 2061
		private readonly int _maxSize;
	}
}
