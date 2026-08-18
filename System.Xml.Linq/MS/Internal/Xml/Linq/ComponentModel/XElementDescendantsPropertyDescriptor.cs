using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x02000037 RID: 55
	internal class XElementDescendantsPropertyDescriptor : XPropertyDescriptor<XElement, IEnumerable<XElement>>
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000BC18 File Offset: 0x00009E18
		public XElementDescendantsPropertyDescriptor() : base("Descendants")
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000BC28 File Offset: 0x00009E28
		public override object GetValue(object component)
		{
			return this.value = new XDeferredAxis<XElement>(delegate(XElement e, XName n)
			{
				if (!(n != null))
				{
					return e.Descendants();
				}
				return e.Descendants(n);
			}, component as XElement, null);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000BC6C File Offset: 0x00009E6C
		protected override void OnChanged(object sender, XObjectChangeEventArgs args)
		{
			if (this.value == null)
			{
				return;
			}
			XObjectChange objectChange = args.ObjectChange;
			if (objectChange > XObjectChange.Remove)
			{
				if (objectChange != XObjectChange.Name)
				{
					return;
				}
				XElement xelement = sender as XElement;
				if (xelement != null && this.value.element != xelement && this.value.name != null && (this.value.name == xelement.Name || this.value.name == this.changeState))
				{
					this.changeState = null;
					this.OnValueChanged(this.value.element, EventArgs.Empty);
				}
			}
			else
			{
				XElement xelement = sender as XElement;
				if (xelement != null && (this.value.name == xelement.Name || this.value.name == null))
				{
					this.OnValueChanged(this.value.element, EventArgs.Empty);
					return;
				}
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BD5C File Offset: 0x00009F5C
		protected override void OnChanging(object sender, XObjectChangeEventArgs args)
		{
			if (this.value == null)
			{
				return;
			}
			XObjectChange objectChange = args.ObjectChange;
			if (objectChange == XObjectChange.Name)
			{
				XElement xelement = sender as XElement;
				this.changeState = ((xelement != null) ? xelement.Name : null);
			}
		}

		// Token: 0x040000E9 RID: 233
		private XDeferredAxis<XElement> value;

		// Token: 0x040000EA RID: 234
		private XName changeState;
	}
}
