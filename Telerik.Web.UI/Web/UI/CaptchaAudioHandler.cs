using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Web;
using Telerik.Web.UI.Captcha;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020016C7 RID: 5831
	public class CaptchaAudioHandler : PreventableHandler, IHttpHandler
	{
		// Token: 0x0600E108 RID: 57608 RVA: 0x0031FC88 File Offset: 0x0031DE88
		public void ProcessRequest(HttpContext context)
		{
			HttpApplication applicationInstance = context.ApplicationInstance;
			bool flag = base.CheckPreventHandler("Telerik.Web.CaptchaDenyAudioHandler", null, "");
			if (flag)
			{
				base.CompleteRequest(context.ApplicationInstance, 404);
				return;
			}
			string text = applicationInstance.Request.QueryString["guid"];
			string isStoredInCache = applicationInstance.Request.QueryString["isc"];
			CaptchaImage captchaImage = null;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					CaptchaImageHelper captchaImageHelper = new CaptchaImageHelper(text, isStoredInCache);
					captchaImage = captchaImageHelper.GetCaptchaImage();
				}
				catch
				{
					base.CompleteRequest(applicationInstance, 404);
					return;
				}
				if (captchaImage == null)
				{
					Brush gray = Brushes.Gray;
					Bitmap bitmap = new Bitmap(50, 50);
					Graphics graphics = Graphics.FromImage(bitmap);
					GraphicsUnit graphicsUnit = GraphicsUnit.Pixel;
					graphics.FillRectangle(gray, bitmap.GetBounds(ref graphicsUnit));
					bitmap.Save(applicationInstance.Context.Response.OutputStream, ImageFormat.Gif);
					bitmap.Dispose();
					applicationInstance.Response.ContentType = "image/gif";
				}
				else if (captchaImage.EnableCaptchaAudio)
				{
					CaptchaAudioHandler.WriteAudioToOutputStream(applicationInstance, text, captchaImage.Text, captchaImage.AudioFilesPath, captchaImage.UseAudioFiles, captchaImage.EnableAudioNoise);
				}
				base.CompleteRequest(applicationInstance, 200);
			}
		}

		// Token: 0x170044F9 RID: 17657
		// (get) Token: 0x0600E109 RID: 57609 RVA: 0x0031FDD8 File Offset: 0x0031DFD8
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600E10A RID: 57610 RVA: 0x0031FDDC File Offset: 0x0031DFDC
		private static void WriteAudioToOutputStream(HttpApplication app, string fileName, string textToSpeak, string folderUrl, bool generateAudioByConcatenation, bool shouldAddNoise)
		{
			app.Response.Clear();
			app.Response.ContentType = "audio/wav";
			app.Response.AddHeader("Accept-Ranges", "bytes");
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				CaptchaAudio captchaAudio = new CaptchaAudio(memoryStream, textToSpeak);
				captchaAudio.CanSpeak = !generateAudioByConcatenation;
				if (captchaAudio.CanSpeak)
				{
					ThreadStart start = new ThreadStart(captchaAudio.SpeakText);
					Thread thread = new Thread(start);
					thread.Start();
					thread.Join();
				}
				if (!captchaAudio.CanSpeak)
				{
					memoryStream = captchaAudio.GetWaveStream(app.Request.MapPath(folderUrl));
				}
				if (memoryStream.Length > 0L)
				{
					string text = "RadCaptcha_Audio_" + fileName.Substring(0, fileName.IndexOf('-')) + ".wav";
					text = text.Replace("\n", " ").Replace("\r", " ");
					app.Response.AddHeader("Content-Disposition", "attachment; filename=" + text);
					if (shouldAddNoise)
					{
						NoiseSynthesizer noiseSynthesizer = new NoiseSynthesizer(memoryStream);
						using (MemoryStream mixedOutput = noiseSynthesizer.GetMixedOutput())
						{
							mixedOutput.Position = 0L;
							mixedOutput.WriteTo(app.Context.Response.OutputStream);
							goto IL_1F7;
						}
					}
					memoryStream.Position = 0L;
					memoryStream.WriteTo(app.Context.Response.OutputStream);
				}
				else
				{
					HttpResponse response = app.Response;
					response.Clear();
					app.Response.ContentType = "text/HTML";
					response.Write("<html><body><h2>CaptchaAudio Exception</h2><br/><br/>");
					response.Write("<span style='font-size: 18px;'>RadCaptcha was not able to generate an audio code. Please check the following:</span><br/>");
					response.Write("<ul>");
					response.Write("<li style='font-size: 18px;'>Make sure that directory named RadCaptcha exists in the application's App_Data folder.</li>");
					response.Write("<li style='font-size: 18px;'>Make sure that you have specified a valid path to the directory containing the audio (*.wav) files. Use CaptchaImage - AudioFilesPath property to set the path to the folder containing the files.</li>");
					response.Write("<li style='font-size: 18px;'>Make sure that there is an audio file for every possible character that might appear in the TextCode. The audio files must be named \"Char\".wav - i.e. A.wav, B.wav, C.wav, 1.wav etc.</li>");
					response.Write("<li style='font-size: 18px;'>Make sure your application is running in <a href='http://msdn.microsoft.com/en-us/library/ms586901.aspx'>full trust</a> environment</li>");
					response.Write("</ul>");
					response.Write("<br/><span style='font-size: 18px;'>For detailed information see RadCaptcha's help section.</span>");
					response.Write("</body></html>");
				}
				IL_1F7:;
			}
			finally
			{
				memoryStream.Close();
			}
		}
	}
}
