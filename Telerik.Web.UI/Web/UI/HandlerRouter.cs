using System;
using System.Collections.Generic;
using System.Web;
using Telerik.Web.UI.ImageEditor;
using Telerik.Web.UI.Notification;

namespace Telerik.Web.UI
{
	// Token: 0x020011C9 RID: 4553
	internal class HandlerRouter
	{
		// Token: 0x17003CD9 RID: 15577
		// (get) Token: 0x0600BC09 RID: 48137 RVA: 0x0029AC0F File Offset: 0x00298E0F
		internal static string HandlerUrlKey
		{
			get
			{
				return "type";
			}
		}

		// Token: 0x0600BC0A RID: 48138 RVA: 0x0029AC16 File Offset: 0x00298E16
		public HandlerRouter()
		{
			this._handlers = new Dictionary<string, TFunc<IHttpHandler>>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x17003CDA RID: 15578
		// (get) Token: 0x0600BC0B RID: 48139 RVA: 0x0029AC2E File Offset: 0x00298E2E
		protected Dictionary<string, TFunc<IHttpHandler>> Handlers
		{
			get
			{
				return this._handlers;
			}
		}

		// Token: 0x0600BC0C RID: 48140 RVA: 0x0029AC68 File Offset: 0x00298E68
		protected virtual void PopulateHandlers()
		{
			this.Handlers.Add(RadBinaryImage.HandlerRouterKey, () => new RadBinaryImageHandler());
			this.Handlers.Add(RadCaptcha.HandlerRouterKey, () => new CaptchaImageHandler());
			this.Handlers.Add(RadAsyncUpload.HandlerRouterKey, () => new AsyncUploadHandler());
			this.Handlers.Add("rcu", () => new CloudUploadHandler());
			this.Handlers.Add(RadCaptcha.HandlerRouterKeyCaptchaAudio, () => new CaptchaAudioHandler());
			this.Handlers.Add(RadImageEditor.HandlerRouterKey, () => new ImageEditorCacheHandler());
			this.Handlers.Add(RadNotification.HandlerNotificationAudio, () => new NotificationAudioHandler());
		}

		// Token: 0x0600BC0D RID: 48141 RVA: 0x0029ADB0 File Offset: 0x00298FB0
		public bool ProcessHandler(string handlerKey, HttpContext context)
		{
			if (string.IsNullOrEmpty(handlerKey))
			{
				throw new ArgumentNullException("handlerKey");
			}
			this.PopulateHandlers();
			if (this._handlers.ContainsKey(handlerKey))
			{
				this._handlers[handlerKey]().ProcessRequest(context);
				return true;
			}
			return false;
		}

		// Token: 0x0600BC0E RID: 48142 RVA: 0x0029AE00 File Offset: 0x00299000
		public bool ProcessHandler(HttpContext context)
		{
			string text = this.ExtractKey(context);
			return !string.IsNullOrEmpty(text) && this.ProcessHandler(text, context);
		}

		// Token: 0x0600BC0F RID: 48143 RVA: 0x0029AE27 File Offset: 0x00299027
		private string ExtractKey(HttpContext context)
		{
			return context.Request[HandlerRouter.HandlerUrlKey];
		}

		// Token: 0x04003166 RID: 12646
		private Dictionary<string, TFunc<IHttpHandler>> _handlers;
	}
}
