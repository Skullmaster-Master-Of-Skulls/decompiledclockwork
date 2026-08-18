using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200033D RID: 829
	[Serializable]
	internal class TabControlToolboxItem : ToolboxItem
	{
		// Token: 0x060020A8 RID: 8360 RVA: 0x000C6698 File Offset: 0x000C4898
		public TabControlToolboxItem() : base(typeof(TabControl))
		{
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0008C0A7 File Offset: 0x0008A2A7
		private TabControlToolboxItem(SerializationInfo info, StreamingContext context)
		{
			this.Deserialize(info, context);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000C66AC File Offset: 0x000C48AC
		protected override IComponent[] CreateComponentsCore(IDesignerHost host)
		{
			IComponent[] array = base.CreateComponentsCore(host);
			if (array != null && array.Length != 0 && array[0] is TabControl)
			{
				TabControl tabControl = (TabControl)array[0];
				tabControl.ShowToolTips = true;
			}
			return array;
		}
	}
}
