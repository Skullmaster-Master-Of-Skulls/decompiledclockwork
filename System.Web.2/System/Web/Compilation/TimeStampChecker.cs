using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Web.Hosting;

namespace System.Web.Compilation
{
	// Token: 0x02000867 RID: 2151
	internal class TimeStampChecker
	{
		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x001659F4 File Offset: 0x00163BF4
		private static TimeStampChecker Current
		{
			get
			{
				TimeStampChecker timeStampChecker = (TimeStampChecker)CallContext.GetData("TSC");
				if (timeStampChecker == null)
				{
					timeStampChecker = new TimeStampChecker();
					CallContext.SetData("TSC", timeStampChecker);
				}
				return timeStampChecker;
			}
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x00165A26 File Offset: 0x00163C26
		internal static void AddFile(string virtualPath, string path)
		{
			TimeStampChecker.Current.AddFileInternal(virtualPath, path);
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x00165A34 File Offset: 0x00163C34
		private void AddFileInternal(string virtualPath, string path)
		{
			DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(path);
			if (this._timeStamps.Contains(virtualPath))
			{
				DateTime d = (DateTime)this._timeStamps[virtualPath];
				if (d == DateTime.MaxValue)
				{
					return;
				}
				if (d != lastWriteTimeUtc)
				{
					this._timeStamps[virtualPath] = DateTime.MaxValue;
					return;
				}
			}
			else
			{
				this._timeStamps[virtualPath] = lastWriteTimeUtc;
			}
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x00165AA8 File Offset: 0x00163CA8
		internal static bool CheckFilesStillValid(string key, ICollection virtualPaths)
		{
			return virtualPaths == null || TimeStampChecker.Current.CheckFilesStillValidInternal(key, virtualPaths);
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x00165ABC File Offset: 0x00163CBC
		private bool CheckFilesStillValidInternal(string key, ICollection virtualPaths)
		{
			foreach (object obj in virtualPaths)
			{
				string text = (string)obj;
				if (this._timeStamps.Contains(text))
				{
					string path = HostingEnvironment.MapPath(text);
					DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(path);
					DateTime d = (DateTime)this._timeStamps[text];
					if (lastWriteTimeUtc != d)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0400343D RID: 13373
		internal const string CallContextSlotName = "TSC";

		// Token: 0x0400343E RID: 13374
		private Hashtable _timeStamps = new Hashtable(StringComparer.OrdinalIgnoreCase);
	}
}
