using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000320 RID: 800
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class XmlSchemaProviderAttribute : Attribute
	{
		// Token: 0x0600261D RID: 9757 RVA: 0x000B991F File Offset: 0x000B891F
		public XmlSchemaProviderAttribute(string methodName)
		{
			this.methodName = methodName;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x000B992E File Offset: 0x000B892E
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000B9936 File Offset: 0x000B8936
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x000B993E File Offset: 0x000B893E
		public bool IsAny
		{
			get
			{
				return this.any;
			}
			set
			{
				this.any = value;
			}
		}

		// Token: 0x040015C2 RID: 5570
		private string methodName;

		// Token: 0x040015C3 RID: 5571
		private bool any;
	}
}
