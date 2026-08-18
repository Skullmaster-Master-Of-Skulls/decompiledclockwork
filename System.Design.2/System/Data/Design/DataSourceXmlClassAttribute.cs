using System;

namespace System.Data.Design
{
	// Token: 0x02000228 RID: 552
	[AttributeUsage(AttributeTargets.Class)]
	internal sealed class DataSourceXmlClassAttribute : Attribute
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x0007608D File Offset: 0x0007428D
		internal DataSourceXmlClassAttribute(string elementName)
		{
			this.name = elementName;
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0007609C File Offset: 0x0007429C
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x000760A4 File Offset: 0x000742A4
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000ADE RID: 2782
		private string name;
	}
}
