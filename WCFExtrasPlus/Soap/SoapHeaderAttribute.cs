using System;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class SoapHeaderAttribute : Attribute
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000024AE File Offset: 0x000006AE
		public SoapHeaderAttribute(string name, Type type)
		{
			this.name = name;
			this.type = type;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024CB File Offset: 0x000006CB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000024D3 File Offset: 0x000006D3
		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024DB File Offset: 0x000006DB
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000024E3 File Offset: 0x000006E3
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

		// Token: 0x04000003 RID: 3
		private string name;

		// Token: 0x04000004 RID: 4
		private Type type;

		// Token: 0x04000005 RID: 5
		private SoapHeaderDirection direction = SoapHeaderDirection.In;
	}
}
