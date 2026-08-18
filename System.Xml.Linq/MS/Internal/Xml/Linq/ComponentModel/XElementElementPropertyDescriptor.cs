using System;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x02000038 RID: 56
	internal class XElementElementPropertyDescriptor : XPropertyDescriptor<XElement, object>
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000BD96 File Offset: 0x00009F96
		public XElementElementPropertyDescriptor() : base("Element")
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public override object GetValue(object component)
		{
			return this.value = new XDeferredSingleton<XElement>((XElement e, XName n) => e.Element(n), component as XElement, null);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000BDE8 File Offset: 0x00009FE8
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
				if (xelement != null && this.value.element == xelement.parent && this.value.name == xelement.Name && this.value.element.Element(this.value.name) == xelement)
				{
					this.OnValueChanged(this.value.element, EventArgs.Empty);
					return;
				}
				break;
			}
			case XObjectChange.Remove:
			{
				XElement xelement = sender as XElement;
				if (xelement != null && this.changeState == xelement)
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
				if (xelement != null)
				{
					if (this.value.element == xelement.parent && this.value.name == xelement.Name && this.value.element.Element(this.value.name) == xelement)
					{
						this.OnValueChanged(this.value.element, EventArgs.Empty);
						return;
					}
					if (this.changeState == xelement)
					{
						this.changeState = null;
						this.OnValueChanged(this.value.element, EventArgs.Empty);
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000BF58 File Offset: 0x0000A158
		protected override void OnChanging(object sender, XObjectChangeEventArgs args)
		{
			if (this.value == null)
			{
				return;
			}
			XObjectChange objectChange = args.ObjectChange;
			if (objectChange - XObjectChange.Remove <= 1)
			{
				XElement xelement = sender as XElement;
				this.changeState = ((xelement != null && this.value.element == xelement.parent && this.value.name == xelement.Name && this.value.element.Element(this.value.name) == xelement) ? xelement : null);
			}
		}

		// Token: 0x040000EB RID: 235
		private XDeferredSingleton<XElement> value;

		// Token: 0x040000EC RID: 236
		private XElement changeState;
	}
}
