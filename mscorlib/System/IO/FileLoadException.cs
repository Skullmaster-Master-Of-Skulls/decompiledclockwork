using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
	// Token: 0x020005B7 RID: 1463
	[ComVisible(true)]
	[Serializable]
	public class FileLoadException : IOException
	{
		// Token: 0x0600360A RID: 13834 RVA: 0x000B4363 File Offset: 0x000B3363
		public FileLoadException() : base(Environment.GetResourceString("IO.FileLoad"))
		{
			base.SetErrorCode(-2146232799);
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000B4380 File Offset: 0x000B3380
		public FileLoadException(string message) : base(message)
		{
			base.SetErrorCode(-2146232799);
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000B4394 File Offset: 0x000B3394
		public FileLoadException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232799);
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000B43A9 File Offset: 0x000B33A9
		public FileLoadException(string message, string fileName) : base(message)
		{
			base.SetErrorCode(-2146232799);
			this._fileName = fileName;
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000B43C4 File Offset: 0x000B33C4
		public FileLoadException(string message, string fileName, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146232799);
			this._fileName = fileName;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000B43E0 File Offset: 0x000B33E0
		public override string Message
		{
			get
			{
				this.SetMessageField();
				return this._message;
			}
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000B43EE File Offset: 0x000B33EE
		private void SetMessageField()
		{
			if (this._message == null)
			{
				this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, base.HResult);
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000B440F File Offset: 0x000B340F
		public string FileName
		{
			get
			{
				return this._fileName;
			}
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000B4418 File Offset: 0x000B3418
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

		// Token: 0x06003613 RID: 13843 RVA: 0x000B450C File Offset: 0x000B350C
		protected FileLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._fileName = info.GetString("FileLoad_FileName");
			try
			{
				this._fusionLog = info.GetString("FileLoad_FusionLog");
			}
			catch
			{
				this._fusionLog = null;
			}
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000B4560 File Offset: 0x000B3560
		private FileLoadException(string fileName, string fusionLog, int hResult) : base(null)
		{
			base.SetErrorCode(hResult);
			this._fileName = fileName;
			this._fusionLog = fusionLog;
			this.SetMessageField();
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003615 RID: 13845 RVA: 0x000B4584 File Offset: 0x000B3584
		public string FusionLog
		{
			[SecurityPermission(SecurityAction.Demand, Flags = (SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy))]
			get
			{
				return this._fusionLog;
			}
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000B458C File Offset: 0x000B358C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("FileLoad_FileName", this._fileName, typeof(string));
			try
			{
				info.AddValue("FileLoad_FusionLog", this.FusionLog, typeof(string));
			}
			catch (SecurityException)
			{
			}
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000B45EC File Offset: 0x000B35EC
		internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
		{
			return string.Format(CultureInfo.CurrentCulture, FileLoadException.GetFileLoadExceptionMessage(hResult), new object[]
			{
				fileName,
				FileLoadException.GetMessageForHR(hResult)
			});
		}

		// Token: 0x06003618 RID: 13848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetFileLoadExceptionMessage(int hResult);

		// Token: 0x06003619 RID: 13849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetMessageForHR(int hresult);

		// Token: 0x04001C37 RID: 7223
		private string _fileName;

		// Token: 0x04001C38 RID: 7224
		private string _fusionLog;
	}
}
