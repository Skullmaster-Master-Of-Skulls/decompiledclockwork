using System;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x0200003B RID: 59
	internal class XElementXmlPropertyDescriptor : XPropertyDescriptor<XElement, string>
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000C2D5 File Offset: 0x0000A4D5
		public XElementXmlPropertyDescriptor() : base("Xml")
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
		public override object GetValue(object component)
		{
			this.element = (component as XElement);
			if (this.element == null)
			{
				return string.Empty;
			}
			return this.element.ToString(SaveOptions.DisableFormatting);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C30A File Offset: 0x0000A50A
		protected override void OnChanged(object sender, XObjectChangeEventArgs args)
		{
			if (this.element == null)
			{
				return;
			}
			this.OnValueChanged(this.element, EventArgs.Empty);
		}

		// Token: 0x040000F0 RID: 240
		private XElement element;
	}
}
