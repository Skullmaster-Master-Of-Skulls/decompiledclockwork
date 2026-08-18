using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x02000039 RID: 57
	internal class XElementElementsPropertyDescriptor : XPropertyDescriptor<XElement, IEnumerable<XElement>>
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000BFD8 File Offset: 0x0000A1D8
		public XElementElementsPropertyDescriptor() : base("Elements")
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		public override object GetValue(object component)
		{
			return this.value = new XDeferredAxis<XElement>(delegate(XElement e, XName n)
			{
				if (!(n != null))
				{
					return e.Elements();
				}
				return e.Elements(n);
			}, component as XElement, null);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000C02C File Offset: 0x0000A22C
		protected override void OnChanged(object sender, XObjectChangeEventArgs args)
		{
			if (this.value == null)
			{
				return;
			}
			switch (args.ObjectChange)
			{
			case XObjectChange.Add:
			{
				XElement xelement = sender as XElement;
				if (xelement != null && this.value.element == xelement.parent && (this.value.name == xelement.Name || this.value.name == null))
				{
					this.OnValueChanged(this.value.element, EventArgs.Empty);
					return;
				}
				break;
			}
			case XObjectChange.Remove:
			{
				XElement xelement = sender as XElement;
				if (xelement != null && this.value.element == this.changeState as XContainer && (this.value.name == xelement.Name || this.value.name == null))
				{
					this.changeState = null;
					this.OnValueChanged(this.value.element, EventArgs.Empty);
					return;
				}
				break;
			}
			case XObjectChange.Name:
			{
				XElement xelement = sender as XElement;
				if (xelement != null && this.value.element == xelement.parent && this.value.name != null && (this.value.name == xelement.Name || this.value.name == this.changeState as XName))
				{
					this.changeState = null;
					this.OnValueChanged(this.value.element, EventArgs.Empty);
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		protected override void OnChanging(object sender, XObjectChangeEventArgs args)
		{
			if (this.value == null)
			{
				return;
			}
			XObjectChange objectChange = args.ObjectChange;
			XElement xelement;
			if (objectChange == XObjectChange.Remove)
			{
				xelement = (sender as XElement);
				this.changeState = ((xelement != null) ? xelement.parent : null);
				return;
			}
			if (objectChange != XObjectChange.Name)
			{
				return;
			}
			xelement = (sender as XElement);
			this.changeState = ((xelement != null) ? xelement.Name : null);
		}

		// Token: 0x040000ED RID: 237
		private XDeferredAxis<XElement> value;

		// Token: 0x040000EE RID: 238
		private object changeState;
	}
}
