using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000347 RID: 839
	internal class TextBoxDesigner : TextBoxBaseDesigner
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x000CB40C File Offset: 0x000C960C
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new TextBoxActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000CB43C File Offset: 0x000C963C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			string[] array = new string[]
			{
				"PasswordChar"
			};
			Attribute[] attributes = new Attribute[0];
			for (int i = 0; i < array.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties[array[i]];
				if (propertyDescriptor != null)
				{
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(TextBoxDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x000CB4A0 File Offset: 0x000C96A0
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x000CB4D0 File Offset: 0x000C96D0
		private char PasswordChar
		{
			get
			{
				TextBox textBox = this.Control as TextBox;
				if (textBox.UseSystemPasswordChar)
				{
					return this.passwordChar;
				}
				return textBox.PasswordChar;
			}
			set
			{
				TextBox textBox = this.Control as TextBox;
				this.passwordChar = value;
				textBox.PasswordChar = value;
			}
		}

		// Token: 0x04001930 RID: 6448
		private char passwordChar;

		// Token: 0x04001931 RID: 6449
		private DesignerActionListCollection _actionLists;
	}
}
