using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000293 RID: 659
	[Serializable]
	internal class AutoSizeToolboxItem : ToolboxItem
	{
		// Token: 0x0600190D RID: 6413 RVA: 0x0008C096 File Offset: 0x0008A296
		public AutoSizeToolboxItem()
		{
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x0008C09E File Offset: 0x0008A29E
		public AutoSizeToolboxItem(Type toolType) : base(toolType)
		{
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x0008C0A7 File Offset: 0x0008A2A7
		private AutoSizeToolboxItem(SerializationInfo info, StreamingContext context)
		{
			this.Deserialize(info, context);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x0008C0B8 File Offset: 0x0008A2B8
		protected override IComponent[] CreateComponentsCore(IDesignerHost host)
		{
			IComponent[] array = base.CreateComponentsCore(host);
			if (array != null && array.Length != 0 && array[0] is Control)
			{
				Control control = array[0] as Control;
				control.AutoSize = true;
			}
			return array;
		}
	}
}
