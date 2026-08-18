using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000DF RID: 223
	internal static class LoginDesignerUtil
	{
		// Token: 0x02000408 RID: 1032
		internal abstract class GenericConvertToTemplateHelper<ControlType, ControlDesignerType> where ControlType : WebControl, IControlDesignerAccessor where ControlDesignerType : ControlDesigner
		{
			// Token: 0x060027C9 RID: 10185 RVA: 0x000F4294 File Offset: 0x000F2494
			public GenericConvertToTemplateHelper(ControlDesignerType designer, IDesignerHost designerHost)
			{
				this._designer = designer;
				this._designerHost = designerHost;
			}

			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x060027CA RID: 10186 RVA: 0x000F42AA File Offset: 0x000F24AA
			protected ControlDesignerType Designer
			{
				get
				{
					return this._designer;
				}
			}

			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x060027CB RID: 10187 RVA: 0x000F42B2 File Offset: 0x000F24B2
			private ControlType ViewControl
			{
				get
				{
					return (ControlType)((object)this.Designer.ViewControl);
				}
			}

			// Token: 0x17000850 RID: 2128
			// (get) Token: 0x060027CC RID: 10188
			protected abstract string[] PersistedControlIDs { get; }

			// Token: 0x17000851 RID: 2129
			// (get) Token: 0x060027CD RID: 10189
			protected abstract string[] PersistedIfNotVisibleControlIDs { get; }

			// Token: 0x060027CE RID: 10190
			protected abstract Control GetDefaultTemplateContents();

			// Token: 0x060027CF RID: 10191
			protected abstract Style GetFailureTextStyle(ControlType control);

			// Token: 0x060027D0 RID: 10192
			protected abstract ITemplate GetTemplate(ControlType control);

			// Token: 0x060027D1 RID: 10193 RVA: 0x000F42CC File Offset: 0x000F24CC
			private void ConvertPersistedControlsToLiteralControls(Control defaultTemplateContents)
			{
				foreach (string text in this.PersistedControlIDs)
				{
					Control control = defaultTemplateContents.FindControl(text);
					if (control != null)
					{
						if (Array.IndexOf<string>(this.PersistedIfNotVisibleControlIDs, text) >= 0)
						{
							control.Visible = true;
							control.Parent.Visible = true;
							control.Parent.Parent.Visible = true;
						}
						if (control.Visible)
						{
							string text2 = ControlPersister.PersistControl(control, this._designerHost);
							LiteralControl child = new LiteralControl(text2);
							ControlCollection controls = control.Parent.Controls;
							int index = controls.IndexOf(control);
							controls.Remove(control);
							controls.AddAt(index, child);
						}
					}
				}
			}

			// Token: 0x060027D2 RID: 10194 RVA: 0x000F4380 File Offset: 0x000F2580
			public ITemplate ConvertToTemplate()
			{
				ITemplate template = this.GetTemplate(this.ViewControl);
				ITemplate result;
				if (template != null)
				{
					result = template;
				}
				else
				{
					this._designer.ViewControlCreated = false;
					Hashtable hashtable = new Hashtable(1);
					hashtable.Add("ConvertToTemplate", true);
					this.ViewControl.SetDesignModeState(hashtable);
					this._designer.GetDesignTimeHtml();
					Control defaultTemplateContents = this.GetDefaultTemplateContents();
					this.SetFailureTextStyle(defaultTemplateContents);
					this.ConvertPersistedControlsToLiteralControls(defaultTemplateContents);
					StringWriter stringWriter = new StringWriter(CultureInfo.CurrentCulture);
					HtmlTextWriter writer = new HtmlTextWriter(stringWriter);
					defaultTemplateContents.RenderControl(writer);
					result = ControlParser.ParseTemplate(this._designerHost, stringWriter.ToString());
					Hashtable hashtable2 = new Hashtable(1);
					hashtable2.Add("ConvertToTemplate", false);
					this.ViewControl.SetDesignModeState(hashtable2);
				}
				return result;
			}

			// Token: 0x060027D3 RID: 10195 RVA: 0x000F4464 File Offset: 0x000F2664
			private void SetFailureTextStyle(Control defaultTemplateContents)
			{
				Control control = defaultTemplateContents.FindControl("FailureText");
				if (control != null)
				{
					TableCell tableCell = (TableCell)control.Parent;
					tableCell.ForeColor = Color.Red;
					tableCell.ApplyStyle(this.GetFailureTextStyle(this.ViewControl));
					control.EnableViewState = false;
				}
			}

			// Token: 0x04001C71 RID: 7281
			private const string _failureTextID = "FailureText";

			// Token: 0x04001C72 RID: 7282
			private ControlDesignerType _designer;

			// Token: 0x04001C73 RID: 7283
			private IDesignerHost _designerHost;
		}
	}
}
