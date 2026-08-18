using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000346 RID: 838
	internal class TextBoxBaseDesigner : ControlDesigner
	{
		// Token: 0x06002138 RID: 8504 RVA: 0x00093E53 File Offset: 0x00092053
		public TextBoxBaseDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x000CB1E4 File Offset: 0x000C93E4
		public override IList SnapLines
		{
			get
			{
				ArrayList arrayList = base.SnapLines as ArrayList;
				int num = DesignerUtils.GetTextBaseline(this.Control, ContentAlignment.TopLeft);
				BorderStyle borderStyle = BorderStyle.Fixed3D;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["BorderStyle"];
				if (propertyDescriptor != null)
				{
					borderStyle = (BorderStyle)propertyDescriptor.GetValue(base.Component);
				}
				if (borderStyle == BorderStyle.None)
				{
					num = num;
				}
				else if (borderStyle == BorderStyle.FixedSingle)
				{
					num += 2;
				}
				else if (borderStyle == BorderStyle.Fixed3D)
				{
					num += 3;
				}
				else
				{
					num = num;
				}
				arrayList.Add(new SnapLine(SnapLineType.Baseline, num, SnapLinePriority.Medium));
				return arrayList;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x000C11C0 File Offset: 0x000BF3C0
		// (set) Token: 0x0600213B RID: 8507 RVA: 0x000CB264 File Offset: 0x000C9464
		private string Text
		{
			get
			{
				return this.Control.Text;
			}
			set
			{
				this.Control.Text = value;
				((TextBoxBase)this.Control).Select(0, 0);
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000CB284 File Offset: 0x000C9484
		private bool ShouldSerializeText()
		{
			return TypeDescriptor.GetProperties(typeof(TextBoxBase))["Text"].ShouldSerializeValue(base.Component);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000CB2AA File Offset: 0x000C94AA
		private void ResetText()
		{
			this.Control.Text = "";
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000CB2BC File Offset: 0x000C94BC
		public override void InitializeNewComponent(IDictionary defaultValues)
		{
			base.InitializeNewComponent(defaultValues);
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["Text"];
			if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(string) && !propertyDescriptor.IsReadOnly && propertyDescriptor.IsBrowsable)
			{
				propertyDescriptor.SetValue(base.Component, "");
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000CB324 File Offset: 0x000C9524
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
					properties[array[i]] = TypeDescriptor.CreateProperty(typeof(TextBoxBaseDesigner), propertyDescriptor, attributes);
				}
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x000CB388 File Offset: 0x000C9588
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules selectionRules = base.SelectionRules;
				object component = base.Component;
				selectionRules |= SelectionRules.AllSizeable;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Multiline"];
				if (propertyDescriptor != null)
				{
					object value = propertyDescriptor.GetValue(component);
					if (value is bool && !(bool)value)
					{
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(component)["AutoSize"];
						if (propertyDescriptor2 != null)
						{
							object value2 = propertyDescriptor2.GetValue(component);
							if (value2 is bool && (bool)value2)
							{
								selectionRules &= ~(SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
							}
						}
					}
				}
				return selectionRules;
			}
		}
	}
}
