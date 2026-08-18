using System;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000AD RID: 173
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ContentPlaceHolderDesigner : ControlDesigner
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00019310 File Offset: 0x00017510
		private string CreateDesignTimeHTML()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			Font captionFont = SystemFonts.CaptionFont;
			Color controlText = SystemColors.ControlText;
			Color control = SystemColors.Control;
			string text = base.Component.GetType().Name + " - " + base.Component.Site.Name;
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<table cellspacing=0 cellpadding=0 style=\"border:1px solid black; width:100%; height:200px;\">\r\n            <tr>\r\n              <td style=\"width:100%; height:25px; font-family:Tahoma; font-size:{2}pt; color:{3}; background-color:{4}; padding:5px; border-bottom:1px solid black;\">\r\n                &nbsp;{0}\r\n              </td>\r\n            </tr>\r\n            <tr>\r\n              <td style=\"width:100%; height:175px; vertical-align:top;\" {1}=\"0\">\r\n              </td>\r\n            </tr>\r\n          </table>", new object[]
			{
				text,
				DesignerRegion.DesignerRegionAttributeName,
				captionFont.SizeInPoints,
				ColorTranslator.ToHtml(controlText),
				ColorTranslator.ToHtml(control)
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000193B8 File Offset: 0x000175B8
		public override string GetDesignTimeHtml()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (!(designerHost.RootComponent is MasterPage))
			{
				throw new InvalidOperationException(SR.GetString("ContentPlaceHolder_Invalid_RootComponent"));
			}
			return base.GetDesignTimeHtml();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00019400 File Offset: 0x00017600
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (!(designerHost.RootComponent is MasterPage))
			{
				throw new InvalidOperationException(SR.GetString("ContentPlaceHolder_Invalid_RootComponent"));
			}
			regions.Add(new EditableDesignerRegion(this, "Content"));
			return this.CreateDesignTimeHTML();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00019458 File Offset: 0x00017658
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			if (this._content == null)
			{
				this._content = base.Tag.GetContent();
			}
			if (this._content == null)
			{
				return string.Empty;
			}
			return this._content.Trim();
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001948C File Offset: 0x0001768C
		public override string GetPersistenceContent()
		{
			return this._content;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00019494 File Offset: 0x00017694
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			this._content = content;
			base.Tag.SetDirty(true);
		}

		// Token: 0x0400028B RID: 651
		private string _content;

		// Token: 0x0400028C RID: 652
		private const string designtimeHTML = "<table cellspacing=0 cellpadding=0 style=\"border:1px solid black; width:100%; height:200px;\">\r\n            <tr>\r\n              <td style=\"width:100%; height:25px; font-family:Tahoma; font-size:{2}pt; color:{3}; background-color:{4}; padding:5px; border-bottom:1px solid black;\">\r\n                &nbsp;{0}\r\n              </td>\r\n            </tr>\r\n            <tr>\r\n              <td style=\"width:100%; height:175px; vertical-align:top;\" {1}=\"0\">\r\n              </td>\r\n            </tr>\r\n          </table>";
	}
}
