using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000353 RID: 851
	internal class ToolStripCustomTypeDescriptor : CustomTypeDescriptor
	{
		// Token: 0x060021C5 RID: 8645 RVA: 0x000CE1F1 File Offset: 0x000CC3F1
		public ToolStripCustomTypeDescriptor(ToolStrip instance)
		{
			this.instance = instance;
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000CE200 File Offset: 0x000CC400
		public override object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this.instance;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000CE208 File Offset: 0x000CC408
		public override PropertyDescriptorCollection GetProperties()
		{
			if (this.instance != null && this.collection == null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.instance);
				PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
				properties.CopyTo(array, 0);
				this.collection = new PropertyDescriptorCollection(array, false);
			}
			if (this.collection.Count > 0)
			{
				this.propItems = this.collection["Items"];
				if (this.propItems != null)
				{
					this.collection.Remove(this.propItems);
				}
			}
			return this.collection;
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000CE298 File Offset: 0x000CC498
		public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this.instance != null && this.collection == null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.instance);
				PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
				properties.CopyTo(array, 0);
				this.collection = new PropertyDescriptorCollection(array, false);
			}
			if (this.collection.Count > 0)
			{
				this.propItems = this.collection["Items"];
				if (this.propItems != null)
				{
					this.collection.Remove(this.propItems);
				}
			}
			return this.collection;
		}

		// Token: 0x04001965 RID: 6501
		private ToolStrip instance;

		// Token: 0x04001966 RID: 6502
		private PropertyDescriptor propItems;

		// Token: 0x04001967 RID: 6503
		private PropertyDescriptorCollection collection;
	}
}
