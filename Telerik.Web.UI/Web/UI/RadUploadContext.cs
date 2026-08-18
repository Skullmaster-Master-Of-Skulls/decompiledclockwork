using System;
using System.Collections;
using System.Globalization;
using System.Web;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI
{
	// Token: 0x0200134C RID: 4940
	public class RadUploadContext
	{
		// Token: 0x1700425A RID: 16986
		// (get) Token: 0x0600CE54 RID: 52820 RVA: 0x002DE390 File Offset: 0x002DC590
		// (set) Token: 0x0600CE55 RID: 52821 RVA: 0x002DE398 File Offset: 0x002DC598
		private RequestStateStore StateStore { get; set; }

		// Token: 0x1700425B RID: 16987
		// (get) Token: 0x0600CE56 RID: 52822 RVA: 0x002DE3A1 File Offset: 0x002DC5A1
		// (set) Token: 0x0600CE57 RID: 52823 RVA: 0x002DE3A9 File Offset: 0x002DC5A9
		internal int RequestLength { get; set; }

		// Token: 0x1700425C RID: 16988
		// (get) Token: 0x0600CE58 RID: 52824 RVA: 0x002DE3B4 File Offset: 0x002DC5B4
		internal int UploadedFilesCount
		{
			get
			{
				if (this.StateStore == null)
				{
					return 0;
				}
				int num = 0;
				foreach (RequestField requestField in this.StateStore.Fields)
				{
					if (requestField.Header is FileHeaderInfo)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700425D RID: 16989
		// (get) Token: 0x0600CE59 RID: 52825 RVA: 0x002DE424 File Offset: 0x002DC624
		internal int UploadedBytes
		{
			get
			{
				if (this.StateStore != null)
				{
					return this.StateStore.CurrentRequestBytesCount;
				}
				return 0;
			}
		}

		// Token: 0x1700425E RID: 16990
		// (get) Token: 0x0600CE5A RID: 52826 RVA: 0x002DE43B File Offset: 0x002DC63B
		internal bool IsUploadInProgress
		{
			get
			{
				return this.StateStore == null || this.StateStore.Fields.Count > 0;
			}
		}

		// Token: 0x1700425F RID: 16991
		// (get) Token: 0x0600CE5B RID: 52827 RVA: 0x002DE45C File Offset: 0x002DC65C
		internal int ElapsedMilliseconds
		{
			get
			{
				return (int)DateTime.Now.Subtract(this._startTime).TotalMilliseconds;
			}
		}

		// Token: 0x17004260 RID: 16992
		// (get) Token: 0x0600CE5C RID: 52828 RVA: 0x002DE485 File Offset: 0x002DC685
		internal bool UploadComplete
		{
			get
			{
				return this.StateStore != null && this.StateStore.UploadComplete;
			}
		}

		// Token: 0x17004261 RID: 16993
		// (get) Token: 0x0600CE5D RID: 52829 RVA: 0x002DE49C File Offset: 0x002DC69C
		[Obsolete("The HttpContext.Current.UploadedFiles collection is now deprecated. Use the Request.Files collection instead", false)]
		public UploadedFileCollection UploadedFiles
		{
			get
			{
				if (this._uploadedFiles == null)
				{
					if (HttpContext.Current != null && HttpContext.Current.Request != null)
					{
						this._uploadedFiles = new UploadedFileCollection();
						HttpRequest request = HttpContext.Current.Request;
						using (IEnumerator enumerator = request.Files.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								string text = (string)obj;
								HttpPostedFile httpPostedFile = request.Files[text];
								if (httpPostedFile != null && !string.IsNullOrEmpty(httpPostedFile.FileName) && httpPostedFile.InputStream != null)
								{
									this._uploadedFiles.Add(UploadedFile.FromHttpPostedFile(text, request.Files[text]));
								}
							}
							goto IL_BB;
						}
					}
					return new UploadedFileCollection();
				}
				IL_BB:
				return this._uploadedFiles;
			}
		}

		// Token: 0x0600CE5E RID: 52830 RVA: 0x002DE57C File Offset: 0x002DC77C
		internal static string GetUploadUniqueIdentifier(HttpContext context)
		{
			return context.Request.QueryString[RadUploadContext.UNIQUE_REQUEST_QUERY_IDENTIFIER];
		}

		// Token: 0x0600CE5F RID: 52831 RVA: 0x002DE593 File Offset: 0x002DC793
		public static RadUploadContext GetCurrent(HttpContext context)
		{
			return context.Application["RadUploadContext" + RadUploadContext.GetUploadUniqueIdentifier(context)] as RadUploadContext;
		}

		// Token: 0x17004262 RID: 16994
		// (get) Token: 0x0600CE60 RID: 52832 RVA: 0x002DE5B5 File Offset: 0x002DC7B5
		public static RadUploadContext Current
		{
			get
			{
				return HttpContext.Current.Application["RadUploadContext" + RadUploadContext.GetUploadUniqueIdentifier(HttpContext.Current)] as RadUploadContext;
			}
		}

		// Token: 0x0600CE61 RID: 52833 RVA: 0x002DE5DF File Offset: 0x002DC7DF
		internal static void SetUploadContext(HttpContext context, RadUploadContext uploadContext)
		{
			context.Application["RadUploadContext" + RadUploadContext.GetUploadUniqueIdentifier(context)] = uploadContext;
		}

		// Token: 0x0600CE62 RID: 52834 RVA: 0x002DE5FD File Offset: 0x002DC7FD
		internal static void RemoveUploadContext(HttpContext context)
		{
			context.Application.Remove("RadUploadContext" + RadUploadContext.GetUploadUniqueIdentifier(context));
		}

		// Token: 0x0600CE63 RID: 52835 RVA: 0x002DE61A File Offset: 0x002DC81A
		internal RadUploadContext(int requestLength, RequestStateStore stateStore)
		{
			this.RequestLength = requestLength;
			this.StateStore = stateStore;
		}

		// Token: 0x0600CE64 RID: 52836 RVA: 0x002DE63C File Offset: 0x002DC83C
		internal ProgressData GetProgressData()
		{
			RadUploadProgressData radUploadProgressData = new RadUploadProgressData();
			if (this.StateStore != null)
			{
				this.PopulateProgressData(radUploadProgressData);
			}
			return radUploadProgressData;
		}

		// Token: 0x0600CE65 RID: 52837 RVA: 0x002DE660 File Offset: 0x002DC860
		private void PopulateProgressData(RadUploadProgressData progressData)
		{
			FileHeaderInfo fileHeaderInfo = this.FindLastUploadedFile();
			progressData.CurrentOperationText = string.Empty;
			if (fileHeaderInfo != null)
			{
				progressData.CurrentOperationText = fileHeaderInfo.FileName;
			}
			progressData.PrimaryTotal = this.FormatBytes(this.RequestLength);
			progressData.PrimaryValue = this.FormatBytes(this.StateStore.CurrentRequestBytesCount);
			progressData.PrimaryPercent = (int)Math.Round(this.StateStore.CurrentRequestBytesCount / this.RequestLength * 100m);
			progressData.SecondaryValue = this.GetCompleteFileCount();
			progressData.Speed = this.GetFormattedSpeed();
			progressData.TimeElapsed = this.ElapsedMilliseconds;
			progressData.TimeEstimated = this.GetEstimatedTime();
			progressData.RequestLength = this.RequestLength;
			progressData.CompleteBytes = this.StateStore.CurrentRequestBytesCount;
			progressData.OperationComplete = this.UploadComplete;
		}

		// Token: 0x0600CE66 RID: 52838 RVA: 0x002DE760 File Offset: 0x002DC960
		private FileHeaderInfo FindLastUploadedFile()
		{
			for (int i = this.StateStore.Fields.Count - 1; i >= 0; i--)
			{
				RequestField requestField = this.StateStore.Fields[i];
				if (requestField.Header is FileHeaderInfo)
				{
					return (FileHeaderInfo)requestField.Header;
				}
			}
			return null;
		}

		// Token: 0x0600CE67 RID: 52839 RVA: 0x002DE7B8 File Offset: 0x002DC9B8
		private int GetCompleteFileCount()
		{
			int num = 0;
			for (int i = 0; i < this.StateStore.Fields.Count; i++)
			{
				RequestField field = this.StateStore.Fields[i];
				if (this.isFileField(field))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600CE68 RID: 52840 RVA: 0x002DE802 File Offset: 0x002DCA02
		private bool isFileField(RequestField field)
		{
			return field.Header is FileHeaderInfo && field.Complete && !string.IsNullOrEmpty(((FileHeaderInfo)field.Header).FileName);
		}

		// Token: 0x0600CE69 RID: 52841 RVA: 0x002DE834 File Offset: 0x002DCA34
		private string FormatBytes(int bytes)
		{
			int num = 1024;
			int num2 = (int)Math.Pow((double)num, 2.0);
			int num3 = (int)Math.Pow((double)num, 3.0);
			decimal d = 0.8m;
			if (bytes > d * num3)
			{
				return RadUploadContext.FormatBytes("{0}GB", bytes, num3);
			}
			if (bytes > d * num2)
			{
				return RadUploadContext.FormatBytes("{0}MB", bytes, num2);
			}
			if (bytes > d * num)
			{
				return RadUploadContext.FormatBytes("{0}kB", bytes, num);
			}
			return string.Format("{0}B", bytes);
		}

		// Token: 0x0600CE6A RID: 52842 RVA: 0x002DE8F8 File Offset: 0x002DCAF8
		private decimal GetSpeed()
		{
			if (this.ElapsedMilliseconds == 0)
			{
				return 0m;
			}
			return this.StateStore.CurrentRequestBytesCount / this.ElapsedMilliseconds * 1000;
		}

		// Token: 0x0600CE6B RID: 52843 RVA: 0x002DE926 File Offset: 0x002DCB26
		private string GetFormattedSpeed()
		{
			return string.Format("{0}/s", this.FormatBytes(Convert.ToInt32(this.GetSpeed())));
		}

		// Token: 0x0600CE6C RID: 52844 RVA: 0x002DE944 File Offset: 0x002DCB44
		private int GetEstimatedTime()
		{
			decimal speed = this.GetSpeed();
			if (this.ElapsedMilliseconds != 0 && speed > 0m)
			{
				decimal d = this.RequestLength / speed * 1000m;
				return (int)Math.Round(d - this.ElapsedMilliseconds);
			}
			return int.MaxValue;
		}

		// Token: 0x0600CE6D RID: 52845 RVA: 0x002DE9B0 File Offset: 0x002DCBB0
		private static string FormatBytes(string formatString, int bytes, int megaByte)
		{
			return string.Format(formatString, Math.Round(bytes / megaByte, 2).ToString("0.00", CultureInfo.InvariantCulture));
		}

		// Token: 0x0400371B RID: 14107
		internal static readonly string UNIQUE_REQUEST_QUERY_IDENTIFIER = "RadUrid";

		// Token: 0x0400371C RID: 14108
		private UploadedFileCollection _uploadedFiles;

		// Token: 0x0400371D RID: 14109
		private DateTime _startTime = DateTime.Now;
	}
}
