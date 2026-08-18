using System;

namespace WCFExtras.Soap
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class SoapHeaderAttribute : Attribute
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004D24 File Offset: 0x00002F24
		public SoapHeaderAttribute(string name, Type type)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004D44 File Offset: 0x00002F44
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004D5C File Offset: 0x00002F5C
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004D74 File Offset: 0x00002F74
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00004D8C File Offset: 0x00002F8C
		public SoapHeaderDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
			}
		}

		// Token: 0x04000025 RID: 37
		private string name;

		// Token: 0x04000026 RID: 38
		private Type type;

		// Token: 0x04000027 RID: 39
		private SoapHeaderDirection direction = SoapHeaderDirection.In;
	}
}
