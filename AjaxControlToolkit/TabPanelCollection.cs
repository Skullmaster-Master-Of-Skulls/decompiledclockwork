using System;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000196 RID: 406
	public class TabPanelCollection : ControlCollection
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001E97D File Offset: 0x0001CB7D
		public TabPanelCollection(Control owner) : base(owner)
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001E986 File Offset: 0x0001CB86
		public override void Add(Control child)
		{
			if (!(child is TabPanel))
			{
				throw new ArgumentException("TabPanelCollection can only contain TabPanel controls.");
			}
			base.Add(child);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001E9A2 File Offset: 0x0001CBA2
		public override void AddAt(int index, Control child)
		{
			if (!(child is TabPanel))
			{
				throw new ArgumentException("TabPanelCollection can only contain TabPanel controls.");
			}
			base.AddAt(index, child);
		}

		// Token: 0x17000468 RID: 1128
		public TabPanel this[int index]
		{
			get
			{
				return (TabPanel)base[index];
			}
		}
	}
}
