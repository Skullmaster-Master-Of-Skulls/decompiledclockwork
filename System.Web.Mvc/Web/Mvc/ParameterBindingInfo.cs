using System;
using System.Collections.Generic;

namespace System.Web.Mvc
{
	// Token: 0x020001A5 RID: 421
	public abstract class ParameterBindingInfo
	{
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0001EB31 File Offset: 0x0001CD31
		public virtual IModelBinder Binder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0001EB34 File Offset: 0x0001CD34
		public virtual ICollection<string> Exclude
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0001EB3C File Offset: 0x0001CD3C
		public virtual ICollection<string> Include
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0001EB44 File Offset: 0x0001CD44
		public virtual string Prefix
		{
			get
			{
				return null;
			}
		}
	}
}
