using System;
using System.Collections.Generic;

namespace AutoMapper
{
	// Token: 0x02000032 RID: 50
	public class MappingOperationOptions : IMappingOperationOptions
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00004B8C File Offset: 0x00002D8C
		public MappingOperationOptions()
		{
			this.Items = new Dictionary<string, object>();
			this.BeforeMapAction = delegate(object src, object dest)
			{
			};
			this.AfterMapAction = delegate(object src, object dest)
			{
			};
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004BF4 File Offset: 0x00002DF4
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00004BFC File Offset: 0x00002DFC
		public Func<Type, object> ServiceCtor { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004C05 File Offset: 0x00002E05
		public IDictionary<string, object> Items { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00004C0D File Offset: 0x00002E0D
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00004C15 File Offset: 0x00002E15
		public bool DisableCache { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004C1E File Offset: 0x00002E1E
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00004C26 File Offset: 0x00002E26
		public Action<object, object> BeforeMapAction { get; protected set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00004C2F File Offset: 0x00002E2F
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00004C37 File Offset: 0x00002E37
		public Action<object, object> AfterMapAction { get; protected set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x00004C40 File Offset: 0x00002E40
		public void BeforeMap(Action<object, object> beforeFunction)
		{
			this.BeforeMapAction = beforeFunction;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00004C49 File Offset: 0x00002E49
		public void AfterMap(Action<object, object> afterFunction)
		{
			this.AfterMapAction = afterFunction;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00004C52 File Offset: 0x00002E52
		void IMappingOperationOptions.ConstructServicesUsing(Func<Type, object> constructor)
		{
			this.ServiceCtor = constructor;
		}
	}
}
