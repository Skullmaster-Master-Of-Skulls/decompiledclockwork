using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000095 RID: 149
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequiredScriptAttribute : Attribute
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000CDA1 File Offset: 0x0000AFA1
		public RequiredScriptAttribute(Type extenderType) : this(extenderType, 0)
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000CDAB File Offset: 0x0000AFAB
		public RequiredScriptAttribute(Type extenderType, int loadOrder)
		{
			this._extenderType = extenderType;
			this._order = loadOrder;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000CDC1 File Offset: 0x0000AFC1
		public int LoadOrder
		{
			get
			{
				return this._order;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000CDC9 File Offset: 0x0000AFC9
		public Type ExtenderType
		{
			get
			{
				return this._extenderType;
			}
		}

		// Token: 0x040002A4 RID: 676
		private int _order;

		// Token: 0x040002A5 RID: 677
		private Type _extenderType;
	}
}
