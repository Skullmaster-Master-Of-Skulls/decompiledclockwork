using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F31 RID: 3889
	public class RibbonBarToggleButtonCollection : List<RibbonBarToggleButton>, IRibbonBarSubComponent
	{
		// Token: 0x17002EDD RID: 11997
		// (get) Token: 0x06009433 RID: 37939 RVA: 0x00213B85 File Offset: 0x00211D85
		// (set) Token: 0x06009434 RID: 37940 RVA: 0x00213B8D File Offset: 0x00211D8D
		public IRibbonBarSubComponent Container { get; internal set; }

		// Token: 0x17002EDE RID: 11998
		// (get) Token: 0x06009435 RID: 37941 RVA: 0x00213B96 File Offset: 0x00211D96
		public RadRibbonBar RibbonBar
		{
			get
			{
				if (this.Container == null)
				{
					return null;
				}
				return this.Container.RibbonBar;
			}
		}

		// Token: 0x17002EDF RID: 11999
		// (get) Token: 0x06009436 RID: 37942 RVA: 0x00213BAD File Offset: 0x00211DAD
		// (set) Token: 0x06009437 RID: 37943 RVA: 0x00213BB8 File Offset: 0x00211DB8
		public WebControl ParentWebControl
		{
			get
			{
				return this._parentWebControl;
			}
			internal set
			{
				this._parentWebControl = value;
				foreach (RibbonBarToggleButton ribbonBarToggleButton in this)
				{
					ribbonBarToggleButton.ParentWebControl = this._parentWebControl;
					IRibbonBarGroupHostedItem ribbonBarGroupHostedItem = this._parentWebControl as IRibbonBarGroupHostedItem;
					if (ribbonBarGroupHostedItem != null)
					{
						ribbonBarToggleButton.Group = ribbonBarGroupHostedItem.Group;
					}
				}
			}
		}

		// Token: 0x06009438 RID: 37944 RVA: 0x00213C30 File Offset: 0x00211E30
		public new void Add(RibbonBarToggleButton button)
		{
			base.Add(button);
			button.Container = this;
			if (this.Container != null)
			{
				IRibbonBarGroupHostedItem ribbonBarGroupHostedItem = this.Container.ParentWebControl as IRibbonBarGroupHostedItem;
				if (ribbonBarGroupHostedItem != null)
				{
					button.Group = ribbonBarGroupHostedItem.Group;
				}
			}
		}

		// Token: 0x06009439 RID: 37945 RVA: 0x00213C73 File Offset: 0x00211E73
		public new void Remove(RibbonBarToggleButton button)
		{
			base.Remove(button);
			button.Container = null;
			button.Group = null;
		}

		// Token: 0x04002A73 RID: 10867
		private WebControl _parentWebControl;
	}
}
