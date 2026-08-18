using System;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x02000036 RID: 54
	internal class XElementAttributePropertyDescriptor : XPropertyDescriptor<XElement, object>
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000BAC1 File Offset: 0x00009CC1
		public XElementAttributePropertyDescriptor() : base("Attribute")
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		public override object GetValue(object component)
		{
			return this.value = new XDeferredSingleton<XAttribute>((XElement e, XName n) => e.Attribute(n), component as XElement, null);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BB14 File Offset: 0x00009D14
		protected override void OnChanged(object sender, XObjectChangeEventArgs args)
		{
			if (this.value == null)
			{
				return;
			}
			XObjectChange objectChange = args.ObjectChange;
			if (objectChange != XObjectChange.Add)
			{
				if (objectChange != XObjectChange.Remove)
				{
					return;
				}
				XAttribute xattribute = sender as XAttribute;
				if (xattribute != null && this.changeState == xattribute)
				{
					this.changeState = null;
					this.OnValueChanged(this.value.element, EventArgs.Empty);
				}
			}
			else
			{
				XAttribute xattribute = sender as XAttribute;
				if (xattribute != null && this.value.element == xattribute.parent && this.value.name == xattribute.Name)
				{
					this.OnValueChanged(this.value.element, EventArgs.Empty);
					return;
				}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		protected override void OnChanging(object sender, XObjectChangeEventArgs args)
		{
			if (this.value == null)
			{
				return;
			}
			XObjectChange objectChange = args.ObjectChange;
			if (objectChange == XObjectChange.Remove)
			{
				XAttribute xattribute = sender as XAttribute;
				this.changeState = ((xattribute != null && this.value.element == xattribute.parent && this.value.name == xattribute.Name) ? xattribute : null);
			}
		}

		// Token: 0x040000E7 RID: 231
		private XDeferredSingleton<XAttribute> value;

		// Token: 0x040000E8 RID: 232
		private XAttribute changeState;
	}
}
