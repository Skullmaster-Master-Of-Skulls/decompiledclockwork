using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x020005B9 RID: 1465
	[ComVisible(true)]
	[Serializable]
	public class FileNotFoundException : IOException
	{
		// Token: 0x0600361A RID: 13850 RVA: 0x000B461E File Offset: 0x000B361E
		public FileNotFoundException() : base(Environment.GetResourceString("IO.FileNotFound"))
		{
			base.SetErrorCode(-2147024894);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000B463B File Offset: 0x000B363B
		public FileNotFoundException(string message) : base(message)
		{
			base.SetErrorCode(-2147024894);
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000B464F File Offset: 0x000B364F
		public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000B4664 File Offset: 0x000B3664
		public FileNotFoundException(string message, string fileName) : base(message)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000B467F File Offset: 0x000B367F
		public FileNotFoundException(string message, string fileName, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024894);
			this._fileName = fileName;
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x000B469B File Offset: 0x000B369B
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000B46AC File Offset: 0x000B36AC
		private void SetMessageField()
		{
			if (this._message == null)
			{
				if (this._fileName == null && base.HResult == -2146233088)
				{
					this._message = Environment.GetResourceString("IO.FileNotFound");
					return;
				}
				if (this._fileName != null)
				{
					this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
				}
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003621 RID: 13857 RVA: 0x000B4706 File Offset: 0x000B3706
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000B4710 File Offset: 0x000B3710
		public override string ToString()
		{
			string text = base.GetType().FullName + ": " + this.Message;
			if (this._fileName != null && this._fileName.Length != 0)
			{
				text = text + Environment.NewLine + string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("IO.FileName_Name"), new object[]
				{
					this._fileName
				});
			}
			if (base.InnerException != null)
			{
				text = text + " ---> " + base.InnerException.ToString();
			}
			if (this.StackTrace != null)
			{
				text = text + Environment.NewLine + this.StackTrace;
			}
			try
			{
				if (this.FusionLog != null)
				{
					if (text == null)
					{
						text = " ";
					}
					text += Environment.NewLine;
					text += Environment.NewLine;
					text += this.FusionLog;
				}
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000B4804 File Offset: 0x000B3804
		protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._fileName = info.GetString("FileNotFound_FileName");
			try
			{
				this._fusionLog = info.GetString("FileNotFound_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000B4858 File Offset: 0x000B3858
		private FileNotFoundException(string fileName, string fusionLog, int hResult) : base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000B487C File Offset: 0x000B387C
		public string FusionLog
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000B4884 File Offset: 0x000B3884
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileNotFound_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("FileNotFound_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x04001C40 RID: 7232
		private string _fileName;

		// Token: 0x04001C41 RID: 7233
		private string _fusionLog;
	}
}
