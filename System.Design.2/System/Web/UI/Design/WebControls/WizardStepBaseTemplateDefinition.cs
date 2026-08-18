using System;
using System.ComponentModel.Design;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000137 RID: 311
	internal class WizardStepBaseTemplateDefinition : TemplateDefinition
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x00048EF0 File Offset: 0x000470F0
		public WizardStepBaseTemplateDefinition(WizardDesigner designer, WizardStepBase step, string name, Style style) : base(designer, name, step, name, style)
		{
			this._step = step;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00048F08 File Offset: 0x00047108
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00048F78 File Offset: 0x00047178
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

		// Token: 0x040006C2 RID: 1730
		private WizardStepBase _step;
	}
}
