using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x020002C4 RID: 708
	internal class AttributeAccessor : Accessor
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x0009F10E File Offset: 0x0009E10E
		internal bool IsSpecialXmlNamespace
		{
			get
			{
				return this.isSpecial;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060021A2 RID: 8610 RVA: 0x0009F116 File Offset: 0x0009E116
		// (set) Token: 0x060021A3 RID: 8611 RVA: 0x0009F11E File Offset: 0x0009E11E
		internal bool IsList
		{
			get
			{
				return this.isList;
			}
			set
			{
				this.isList = value;
			}
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0009F128 File Offset: 0x0009E128
		internal void CheckSpecial()
		{
			int num = this.Name.LastIndexOf(':');
			if (num >= 0)
			{
				if (!this.Name.StartsWith("xml:", StringComparison.Ordinal))
				{
					throw new InvalidOperationException(Res.GetString("Xml_InvalidNameChars", new object[]
					{
						this.Name
					}));
				}
				this.Name = this.Name.Substring("xml:".Length);
				base.Namespace = "http://www.w3.org/XML/1998/namespace";
				this.isSpecial = true;
			}
			else if (base.Namespace == "http://www.w3.org/XML/1998/namespace")
			{
				this.isSpecial = true;
			}
			else
			{
				this.isSpecial = false;
			}
			if (this.isSpecial)
			{
				base.Form = XmlSchemaForm.Qualified;
			}
		}

		// Token: 0x0400146C RID: 5228
		private bool isSpecial;

		// Token: 0x0400146D RID: 5229
		private bool isList;
	}
}
