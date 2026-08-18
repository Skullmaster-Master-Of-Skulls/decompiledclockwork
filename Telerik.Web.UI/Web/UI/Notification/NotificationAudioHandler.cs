using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Web;

namespace Telerik.Web.UI.Notification
{
	// Token: 0x02000620 RID: 1568
	public class NotificationAudioHandler : IHttpHandler
	{
		// Token: 0x060038F9 RID: 14585 RVA: 0x000BB520 File Offset: 0x000B9720
		public void ProcessRequest(HttpContext context)
		{
			this.ChooseAudioFormat(context);
			HttpResponse response = context.Response;
			response.Clear();
			response.ContentType = this.audioFormat.MimeType;
			response.AddHeader("Accept-Ranges", "bytes");
			response.AddHeader("Content-Disposition", string.Format("attachment; filename=notification.{0}", this.audioFormat.FileExtension));
			try
			{
				using (Stream stream = this.ReadEmbeddedSound(context.Request["sound"]))
				{
					this.WriteStreamTo(stream, response.OutputStream);
					response.StatusCode = 200;
				}
			}
			catch (MissingManifestResourceException ex)
			{
				response.StatusCode = 404;
				response.StatusDescription = ex.Message;
			}
			finally
			{
				context.ApplicationInstance.CompleteRequest();
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000BB610 File Offset: 0x000B9810
		private Stream ReadEmbeddedSound(string notificationType)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text = string.Format("Telerik.Web.UI.Notification.EmbeddedSounds.{0}.{1}", notificationType, this.audioFormat.FileExtension);
			if (executingAssembly.GetManifestResourceInfo(text) != null)
			{
				return executingAssembly.GetManifestResourceStream(text);
			}
			throw new MissingManifestResourceException("Notification sound is missing");
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000BB658 File Offset: 0x000B9858
		private void WriteStreamTo(Stream source, Stream dest)
		{
			byte[] array = new byte[4096];
			int count;
			while ((count = source.Read(array, 0, array.Length)) > 0)
			{
				dest.Write(array, 0, count);
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000BB68B File Offset: 0x000B988B
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000BB68E File Offset: 0x000B988E
		private void ChooseAudioFormat(HttpContext context)
		{
			this.audioFormat = RadNotification.GetSupportedAudioFormat(context);
		}

		// Token: 0x04000F38 RID: 3896
		private AudioFormat audioFormat;
	}
}
