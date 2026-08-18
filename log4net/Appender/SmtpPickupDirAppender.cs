using System;
using System.IO;
using log4net.Core;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000043 RID: 67
	public class SmtpPickupDirAppender : BufferingAppenderSkeleton
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00008460 File Offset: 0x00006660
		public SmtpPickupDirAppender()
		{
			this.m_fileExtension = string.Empty;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00008473 File Offset: 0x00006673
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000847B File Offset: 0x0000667B
		public string To
		{
			get
			{
				return this.m_to;
			}
			set
			{
				this.m_to = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008484 File Offset: 0x00006684
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000848C File Offset: 0x0000668C
		public string From
		{
			get
			{
				return this.m_from;
			}
			set
			{
				this.m_from = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00008495 File Offset: 0x00006695
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000849D File Offset: 0x0000669D
		public string Subject
		{
			get
			{
				return this.m_subject;
			}
			set
			{
				this.m_subject = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600025F RID: 607 RVA: 0x000084A6 File Offset: 0x000066A6
		// (set) Token: 0x06000260 RID: 608 RVA: 0x000084AE File Offset: 0x000066AE
		public string PickupDir
		{
			get
			{
				return this.m_pickupDir;
			}
			set
			{
				this.m_pickupDir = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000261 RID: 609 RVA: 0x000084B7 File Offset: 0x000066B7
		// (set) Token: 0x06000262 RID: 610 RVA: 0x000084C0 File Offset: 0x000066C0
		public string FileExtension
		{
			get
			{
				return this.m_fileExtension;
			}
			set
			{
				this.m_fileExtension = value;
				if (this.m_fileExtension == null)
				{
					this.m_fileExtension = string.Empty;
				}
				if (!string.IsNullOrEmpty(this.m_fileExtension) && !this.m_fileExtension.StartsWith("."))
				{
					this.m_fileExtension = "." + this.m_fileExtension;
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000851C File Offset: 0x0000671C
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00008524 File Offset: 0x00006724
		public SecurityContext SecurityContext
		{
			get
			{
				return this.m_securityContext;
			}
			set
			{
				this.m_securityContext = value;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008530 File Offset: 0x00006730
		protected override void SendBuffer(LoggingEvent[] events)
		{
			try
			{
				string text = null;
				StreamWriter streamWriter = null;
				using (this.SecurityContext.Impersonate(this))
				{
					text = Path.Combine(this.m_pickupDir, SystemInfo.NewGuid().ToString("N") + this.m_fileExtension);
					streamWriter = File.CreateText(text);
				}
				if (streamWriter == null)
				{
					this.ErrorHandler.Error("Failed to create output file for writing [" + text + "]", null, ErrorCode.FileOpenFailure);
				}
				else
				{
					using (streamWriter)
					{
						streamWriter.WriteLine("To: " + this.m_to);
						streamWriter.WriteLine("From: " + this.m_from);
						streamWriter.WriteLine("Subject: " + this.m_subject);
						streamWriter.WriteLine("Date: " + DateTime.UtcNow.ToString("r"));
						streamWriter.WriteLine("");
						string text2 = this.Layout.Header;
						if (text2 != null)
						{
							streamWriter.Write(text2);
						}
						for (int i = 0; i < events.Length; i++)
						{
							base.RenderLoggingEvent(streamWriter, events[i]);
						}
						text2 = this.Layout.Footer;
						if (text2 != null)
						{
							streamWriter.Write(text2);
						}
						streamWriter.WriteLine("");
						streamWriter.WriteLine(".");
					}
				}
			}
			catch (Exception e)
			{
				this.ErrorHandler.Error("Error occurred while sending e-mail notification.", e);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000086F4 File Offset: 0x000068F4
		public override void ActivateOptions()
		{
			base.ActivateOptions();
			if (this.m_securityContext == null)
			{
				this.m_securityContext = SecurityContextProvider.DefaultProvider.CreateSecurityContext(this);
			}
			using (this.SecurityContext.Impersonate(this))
			{
				this.m_pickupDir = SmtpPickupDirAppender.ConvertToFullPath(this.m_pickupDir.Trim());
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00008760 File Offset: 0x00006960
		protected override bool RequiresLayout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00008763 File Offset: 0x00006963
		protected static string ConvertToFullPath(string path)
		{
			return SystemInfo.ConvertToFullPath(path);
		}

		// Token: 0x04000134 RID: 308
		private string m_to;

		// Token: 0x04000135 RID: 309
		private string m_from;

		// Token: 0x04000136 RID: 310
		private string m_subject;

		// Token: 0x04000137 RID: 311
		private string m_pickupDir;

		// Token: 0x04000138 RID: 312
		private string m_fileExtension;

		// Token: 0x04000139 RID: 313
		private SecurityContext m_securityContext;
	}
}
