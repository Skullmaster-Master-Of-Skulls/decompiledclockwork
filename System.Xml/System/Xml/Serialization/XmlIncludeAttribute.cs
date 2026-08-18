using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200030F RID: 783
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public class XmlIncludeAttribute : Attribute
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x000ADFA9 File Offset: 0x000ACFA9
		public XmlIncludeAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x000ADFB8 File Offset: 0x000ACFB8
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x000ADFC0 File Offset: 0x000ACFC0
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04001580 RID: 5504
		private Type type;
	}
}
