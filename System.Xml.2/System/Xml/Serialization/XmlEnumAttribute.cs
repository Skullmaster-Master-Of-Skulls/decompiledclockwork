using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000194 RID: 404
	[AttributeUsage(AttributeTargets.Field)]
	[__DynamicallyInvokable]
	public class XmlEnumAttribute : Attribute
	{
		// Token: 0x06001AD8 RID: 6872 RVA: 0x00076E07 File Offset: 0x00075007
		[__DynamicallyInvokable]
		public XmlEnumAttribute()
		{
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00076E0F File Offset: 0x0007500F
		[__DynamicallyInvokable]
		public XmlEnumAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x00076E1E File Offset: 0x0007501E
		// (set) Token: 0x06001ADB RID: 6875 RVA: 0x00076E26 File Offset: 0x00075026
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000BF4 RID: 3060
		private string name;
	}
}
