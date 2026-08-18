using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.Templates;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Templates;
using TechnoPro.Common.Win32;

namespace DynamicScreens.Controls
{
	// Token: 0x02000010 RID: 16
	public class CtrlLinkLabel : LinkLabel
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00008574 File Offset: 0x00007574
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000858B File Offset: 0x0000758B
		public int TemplateId { get; set; }

		// Token: 0x060000F6 RID: 246 RVA: 0x00008594 File Offset: 0x00007594
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			try
			{
				if (this.TemplateId > 0)
				{
					ITemplateClientManager templateClientManager = new TemplateClientManager();
					TemplateDTO templateDTO = templateClientManager.LoadTemplate(this.TemplateId, true);
					if (templateDTO != null && templateDTO.Document != null && templateDTO.Document.ByteArray != null && templateDTO.Document.ByteArray.Length > 0)
					{
						string extension = Path.GetExtension(templateDTO.Document.FileName);
						string tempFileName = FileSystem.GetTempFileName(extension);
						File.WriteAllBytes(tempFileName, templateDTO.Document.ByteArray);
						Process.Start(tempFileName);
					}
				}
				else if (this.Text.StartsWith("http", StringComparison.OrdinalIgnoreCase))
				{
					Process.Start(this.Text);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("DynamicScreens.Controls.CtrlLinkLabel:Failed:ex={0}", ex.ToString());
			}
		}
	}
}
