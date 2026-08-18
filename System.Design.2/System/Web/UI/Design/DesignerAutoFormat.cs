using System;
using System.ComponentModel.Design;

namespace System.Web.UI.Design
{
	// Token: 0x0200002E RID: 46
	public abstract class DesignerAutoFormat
	{
		// Token: 0x06000178 RID: 376 RVA: 0x0000C906 File Offset: 0x0000AB06
		protected DesignerAutoFormat(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentNullException("name");
			}
			this._name = name;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000C92B File Offset: 0x0000AB2B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000C933 File Offset: 0x0000AB33
		public DesignerAutoFormatStyle Style
		{
			get
			{
				if (this._style == null)
				{
					this._style = new DesignerAutoFormatStyle();
				}
				return this._style;
			}
		}

		// Token: 0x0600017B RID: 379
		public abstract void Apply(Control control);

		// Token: 0x0600017C RID: 380 RVA: 0x0000C950 File Offset: 0x0000AB50
		public virtual Control GetPreviewControl(Control runtimeControl)
		{
			IDesignerHost designerHost = (IDesignerHost)runtimeControl.Site.GetService(typeof(IDesignerHost));
			ControlDesigner controlDesigner = designerHost.GetDesigner(runtimeControl) as ControlDesigner;
			if (controlDesigner != null)
			{
				return controlDesigner.CreateClonedControl(designerHost, true);
			}
			return null;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000C992 File Offset: 0x0000AB92
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000118 RID: 280
		private string _name;

		// Token: 0x04000119 RID: 281
		private DesignerAutoFormatStyle _style;
	}
}
