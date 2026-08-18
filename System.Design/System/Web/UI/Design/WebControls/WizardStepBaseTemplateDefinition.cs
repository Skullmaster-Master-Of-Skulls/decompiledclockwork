using System;
using System.ComponentModel.Design;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000502 RID: 1282
	internal class WizardStepBaseTemplateDefinition : TemplateDefinition
	{
		// Token: 0x06002DBD RID: 11709 RVA: 0x00103646 File Offset: 0x00102646
		public WizardStepBaseTemplateDefinition(WizardDesigner designer, WizardStepBase step, string name, Style style) : base(designer, name, step, name, style)
		{
			this._step = step;
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x0010365C File Offset: 0x0010265C
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x001036CC File Offset: 0x001026CC
		public override string Content
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this._step.Controls)
				{
					Control control = (Control)obj;
					stringBuilder.Append(ControlPersister.PersistControl(control));
				}
				return stringBuilder.ToString();
			}
			set
			{
				this._step.Controls.Clear();
				if (value == null)
				{
					return;
				}
				IDesignerHost designerHost = (IDesignerHost)base.GetService(typeof(IDesignerHost));
				Control[] array = ControlParser.ParseControls(designerHost, value);
				foreach (Control child in array)
				{
					this._step.Controls.Add(child);
				}
			}
		}

		// Token: 0x04001F14 RID: 7956
		private WizardStepBase _step;
	}
}
