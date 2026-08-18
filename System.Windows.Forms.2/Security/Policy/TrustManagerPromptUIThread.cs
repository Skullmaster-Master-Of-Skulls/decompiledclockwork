using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;

namespace System.Security.Policy
{
	// Token: 0x02000102 RID: 258
	internal class TrustManagerPromptUIThread
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0000DD58 File Offset: 0x0000BF58
		public TrustManagerPromptUIThread(string appName, string defaultBrowserExePath, string supportUrl, string deploymentUrl, string publisherName, X509Certificate2 certificate, TrustManagerPromptOptions options)
		{
			this.m_appName = appName;
			this.m_defaultBrowserExePath = defaultBrowserExePath;
			this.m_supportUrl = supportUrl;
			this.m_deploymentUrl = deploymentUrl;
			this.m_publisherName = publisherName;
			this.m_certificate = certificate;
			this.m_options = options;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		public DialogResult ShowDialog()
		{
			Thread thread = new Thread(new ThreadStart(this.ShowDialogWork));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
			thread.Join();
			return this.m_ret;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		private void ShowDialogWork()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				using (TrustManagerPromptUI trustManagerPromptUI = new TrustManagerPromptUI(this.m_appName, this.m_defaultBrowserExePath, this.m_supportUrl, this.m_deploymentUrl, this.m_publisherName, this.m_certificate, this.m_options))
				{
					this.m_ret = trustManagerPromptUI.ShowDialog();
				}
			}
			catch
			{
			}
			finally
			{
				Application.ExitThread();
			}
		}

		// Token: 0x04000444 RID: 1092
		private string m_appName;

		// Token: 0x04000445 RID: 1093
		private string m_defaultBrowserExePath;

		// Token: 0x04000446 RID: 1094
		private string m_supportUrl;

		// Token: 0x04000447 RID: 1095
		private string m_deploymentUrl;

		// Token: 0x04000448 RID: 1096
		private string m_publisherName;

		// Token: 0x04000449 RID: 1097
		private X509Certificate2 m_certificate;

		// Token: 0x0400044A RID: 1098
		private TrustManagerPromptOptions m_options;

		// Token: 0x0400044B RID: 1099
		private DialogResult m_ret = DialogResult.No;
	}
}
