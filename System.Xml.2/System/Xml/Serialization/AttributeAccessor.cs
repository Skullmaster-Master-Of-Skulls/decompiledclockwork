using System;
using System.Xml.Schema;

namespace System.Xml.Serialization
{
	// Token: 0x0200014B RID: 331
	internal class AttributeAccessor : Accessor
	{
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x000673DF File Offset: 0x000655DF
		internal bool IsSpecialXmlNamespace
		{
			get
			{
				return this.isSpecial;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x000673E7 File Offset: 0x000655E7
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x000673EF File Offset: 0x000655EF
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

		// Token: 0x06001750 RID: 5968 RVA: 0x000673F8 File Offset: 0x000655F8
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

		// Token: 0x04000AD2 RID: 2770
		private bool isSpecial;

		// Token: 0x04000AD3 RID: 2771
		private bool isList;
	}
}
