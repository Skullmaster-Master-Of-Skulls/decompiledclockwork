using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02000070 RID: 112
	public abstract class UploadedFile
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600047B RID: 1147
		public abstract long ContentLength { get; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000BA04 File Offset: 0x00009C04
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public DateTime LastModifiedDate { get; internal set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600047E RID: 1150
		public abstract string ContentType { get; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600047F RID: 1151
		// (set) Token: 0x06000480 RID: 1152
		public abstract string FileName { get; internal set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000481 RID: 1153
		public abstract Stream InputStream { get; }

		// Token: 0x06000482 RID: 1154 RVA: 0x0000BA15 File Offset: 0x00009C15
		public string GetName()
		{
			return Path.GetFileName(this.FileName);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000BA22 File Offset: 0x00009C22
		public string GetNameWithoutExtension()
		{
			return Path.GetFileNameWithoutExtension(this.FileName);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000BA2F File Offset: 0x00009C2F
		public string GetExtension()
		{
			return Path.GetExtension(this.FileName);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000BA3C File Offset: 0x00009C3C
		public virtual string GetFieldValue(string fieldName)
		{
			Regex regex = new Regex("^([\\w\\d]+)file(\\d+)$");
			string name = regex.Replace(this.InputFieldName, string.Format("$1{0}$2", fieldName));
			return HttpContext.Current.Request.Form[name];
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000BA84 File Offset: 0x00009C84
		public bool GetIsFieldChecked(string fieldName)
		{
			string fieldValue = this.GetFieldValue(fieldName);
			return fieldValue != null && fieldValue.Length > 0;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000BAA7 File Offset: 0x00009CA7
		public void SaveAs(string fileName)
		{
			this.SaveAs(fileName, true);
		}

		// Token: 0x06000488 RID: 1160
		public abstract void SaveAs(string fileName, bool overwrite);

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000489 RID: 1161
		protected internal abstract string InputFieldName { get; }

		// Token: 0x0600048A RID: 1162 RVA: 0x0000BAB1 File Offset: 0x00009CB1
		public static UploadedFile FromHttpPostedFile(string inputFieldName, HttpPostedFile file)
		{
			return new PostedFile(inputFieldName, file);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000BABA File Offset: 0x00009CBA
		public static UploadedFile FromHttpPostedFile(HttpPostedFile file)
		{
			return new PostedFile(string.Empty, file);
		}
	}
}
