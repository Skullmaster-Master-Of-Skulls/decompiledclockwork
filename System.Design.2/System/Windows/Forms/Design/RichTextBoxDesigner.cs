using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000325 RID: 805
	internal class RichTextBoxDesigner : TextBoxBaseDesigner
	{
		// Token: 0x06001FD8 RID: 8152 RVA: 0x000C10F0 File Offset: 0x000BF2F0
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			Control control = this.Control;
			if (control != null && control.Handle != IntPtr.Zero)
			{
				NativeMethods.RevokeDragDrop(control.Handle);
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x000C112C File Offset: 0x000BF32C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new RichTextBoxActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000C115C File Offset: 0x000BF35C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"Text"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(RichTextBoxDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x000C11C0 File Offset: 0x000BF3C0
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x000C11D0 File Offset: 0x000BF3D0
		private string Text
		{
			get
			{
				return this.Control.Text;
			}
			set
			{
				string text = this.Control.Text;
				if (value != null)
				{
					value = value.Replace("\r\n", "\n");
				}
				if (text != value)
				{
					this.Control.Text = value;
				}
			}
		}

		// Token: 0x04001899 RID: 6297
		private DesignerActionListCollection _actionLists;
	}
}
