using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200036B RID: 875
	[ToolboxItemFilter("System.Windows.Forms.UserControl", ToolboxItemFilterType.Custom)]
	[ToolboxItemFilter("System.Windows.Forms.MainMenu", ToolboxItemFilterType.Prevent)]
	internal class UserControlDocumentDesigner : DocumentDesigner
	{
		// Token: 0x060023E1 RID: 9185 RVA: 0x000E0198 File Offset: 0x000DE398
		public UserControlDocumentDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060023E2 RID: 9186 RVA: 0x000E01A7 File Offset: 0x000DE3A7
		// (set) Token: 0x060023E3 RID: 9187 RVA: 0x000E01B4 File Offset: 0x000DE3B4
		private Size Size
		{
			get
			{
				return this.Control.ClientSize;
			}
			set
			{
				this.Control.ClientSize = value;
			}
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000E01C4 File Offset: 0x000DE3C4
		internal override bool CanDropComponents(DragEventArgs de)
		{
			bool flag = base.CanDropComponents(de);
			if (flag)
			{
				OleDragDropHandler oleDragHandler = base.GetOleDragHandler();
				object[] draggingObjects = oleDragHandler.GetDraggingObjects(de);
				if (draggingObjects != null)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					for (int i = 0; i < draggingObjects.Length; i++)
					{
						if (designerHost != null && draggingObjects[i] != null && draggingObjects[i] is IComponent && draggingObjects[i] is MainMenu)
						{
							return false;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000E0238 File Offset: 0x000DE438
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"Size"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(UserControlDocumentDesigner), propertyDescriptor, attributes);
				}
			}
		}
	}
}
