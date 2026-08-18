using System;
using System.Web;
using ClockWorkLogger;
using TechnoPro.Common.UI.ClientManager.Web.Core.Web;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Web
{
	// Token: 0x02000012 RID: 18
	public class WebFileClientManager : IWebFileClientManager
	{
		// Token: 0x0600006D RID: 109 RVA: 0x0000414C File Offset: 0x0000234C
		public void DownloadFile(string filename, byte[] bytes)
		{
			string str = filename.Replace(" ", "_");
			HttpContext httpContext = HttpContext.Current;
			HttpResponse response = httpContext.Response;
			response.Buffer = false;
			response.Clear();
			response.ClearContent();
			response.ClearHeaders();
			response.AddHeader("Content-Type", "binary/octet-stream");
			response.AddHeader("Content-Disposition", "attachment; filename=" + str + "; size=" + bytes.Length.ToString());
			response.OutputStream.Write(bytes, 0, bytes.Length);
			response.Flush();
			try
			{
				response.End();
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("WebFileClientManager:DownloadFile:err={0}", ex.ToString());
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004220 File Offset: 0x00002420
		public void DownloadFile2(string filename, byte[] bytes)
		{
			string text = filename.Replace(" ", "_");
			HttpContext httpContext = HttpContext.Current;
			HttpResponse response = httpContext.Response;
			response.Clear();
			response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
			response.AddHeader("Pragma", "no-cache");
			response.AddHeader("Content-Type", "application/force-download");
			response.AddHeader("Content-Transfer-Encoding", "binary\n");
			response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
			response.BinaryWrite(bytes);
			try
			{
				response.End();
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("WebFileClientManager:DownloadFile2:err={0}", ex.ToString());
			}
		}
	}
}
