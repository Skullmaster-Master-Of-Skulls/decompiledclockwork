using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003F0 RID: 1008
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class EventEntry
	{
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000DBC7B File Offset: 0x000DAC7B
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x000DBC83 File Offset: 0x000DAC83
		public string HandlerMethodName
		{
			get
			{
				return this._handlerMethodName;
			}
			set
			{
				this._handlerMethodName = value;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x000DBC8C File Offset: 0x000DAC8C
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x000DBC94 File Offset: 0x000DAC94
		public Type HandlerType
		{
			get
			{
				return this._handlerType;
			}
			set
			{
				this._handlerType = value;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x000DBC9D File Offset: 0x000DAC9D
		// (set) Token: 0x060031E0 RID: 12768 RVA: 0x000DBCA5 File Offset: 0x000DACA5
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x040022E1 RID: 8929
		private Type _handlerType;

		// Token: 0x040022E2 RID: 8930
		private string _handlerMethodName;

		// Token: 0x040022E3 RID: 8931
		private string _name;
	}
}
