using System;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x0200003A RID: 58
	internal class XElementValuePropertyDescriptor : XPropertyDescriptor<XElement, string>
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000C215 File Offset: 0x0000A415
		public XElementValuePropertyDescriptor() : base("Value")
		{
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C222 File Offset: 0x0000A422
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C225 File Offset: 0x0000A425
		public override object GetValue(object component)
		{
			this.element = (component as XElement);
			if (this.element == null)
			{
				return string.Empty;
			}
			return this.element.Value;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C24C File Offset: 0x0000A44C
		public override void SetValue(object component, object value)
		{
			this.element = (component as XElement);
			if (this.element == null)
			{
				return;
			}
			this.element.Value = (value as string);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C274 File Offset: 0x0000A474
		protected override void OnChanged(object sender, XObjectChangeEventArgs args)
		{
			if (this.element == null)
			{
				return;
			}
			XObjectChange objectChange = args.ObjectChange;
			if (objectChange > XObjectChange.Remove)
			{
				if (objectChange != XObjectChange.Value)
				{
					return;
				}
				if (sender is XText)
				{
					this.OnValueChanged(this.element, EventArgs.Empty);
				}
			}
			else if (sender is XElement || sender is XText)
			{
				this.OnValueChanged(this.element, EventArgs.Empty);
				return;
			}
		}

		// Token: 0x040000EF RID: 239
		private XElement element;
	}
}
