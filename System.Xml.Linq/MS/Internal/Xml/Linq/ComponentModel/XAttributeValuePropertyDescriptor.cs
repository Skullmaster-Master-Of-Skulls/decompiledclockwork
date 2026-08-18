using System;
using System.Xml.Linq;

namespace MS.Internal.Xml.Linq.ComponentModel
{
	// Token: 0x0200003C RID: 60
	internal class XAttributeValuePropertyDescriptor : XPropertyDescriptor<XAttribute, string>
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000C326 File Offset: 0x0000A526
		public XAttributeValuePropertyDescriptor() : base("Value")
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000C333 File Offset: 0x0000A533
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000C336 File Offset: 0x0000A536
		public override object GetValue(object component)
		{
			this.attribute = (component as XAttribute);
			if (this.attribute == null)
			{
				return string.Empty;
			}
			return this.attribute.Value;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000C35D File Offset: 0x0000A55D
		public override void SetValue(object component, object value)
		{
			this.attribute = (component as XAttribute);
			if (this.attribute == null)
			{
				return;
			}
			this.attribute.Value = (value as string);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000C385 File Offset: 0x0000A585
		protected override void OnChanged(object sender, XObjectChangeEventArgs args)
		{
			if (this.attribute == null)
			{
				return;
			}
			if (args.ObjectChange == XObjectChange.Value)
			{
				this.OnValueChanged(this.attribute, EventArgs.Empty);
			}
		}

		// Token: 0x040000F1 RID: 241
		private XAttribute attribute;
	}
}
