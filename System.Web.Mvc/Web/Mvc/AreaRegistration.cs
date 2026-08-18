using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace System.Web.Mvc
{
	// Token: 0x02000152 RID: 338
	public abstract class AreaRegistration
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008A2 RID: 2210
		public abstract string AreaName { get; }

		// Token: 0x060008A3 RID: 2211 RVA: 0x00017EEC File Offset: 0x000160EC
		internal void CreateContextAndRegister(RouteCollection routes, object state)
		{
			AreaRegistrationContext areaRegistrationContext = new AreaRegistrationContext(this.AreaName, routes, state);
			string @namespace = base.GetType().Namespace;
			if (@namespace != null)
			{
				areaRegistrationContext.Namespaces.Add(@namespace + ".*");
			}
			this.RegisterArea(areaRegistrationContext);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00017F33 File Offset: 0x00016133
		private static bool IsAreaRegistrationType(Type type)
		{
			return typeof(AreaRegistration).IsAssignableFrom(type) && type.GetConstructor(Type.EmptyTypes) != null;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00017F5A File Offset: 0x0001615A
		public static void RegisterAllAreas()
		{
			AreaRegistration.RegisterAllAreas(null);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00017F62 File Offset: 0x00016162
		public static void RegisterAllAreas(object state)
		{
			AreaRegistration.RegisterAllAreas(RouteTable.Routes, new BuildManagerWrapper(), state);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00017F74 File Offset: 0x00016174
		internal static void RegisterAllAreas(RouteCollection routes, IBuildManager buildManager, object state)
		{
			List<Type> filteredTypesFromAssemblies = TypeCacheUtil.GetFilteredTypesFromAssemblies("MVC-AreaRegistrationTypeCache.xml", new Predicate<Type>(AreaRegistration.IsAreaRegistrationType), buildManager);
			foreach (Type type in filteredTypesFromAssemblies)
			{
				AreaRegistration areaRegistration = (AreaRegistration)Activator.CreateInstance(type);
				areaRegistration.CreateContextAndRegister(routes, state);
			}
		}

		// Token: 0x060008A8 RID: 2216
		public abstract void RegisterArea(AreaRegistrationContext context);

		// Token: 0x04000272 RID: 626
		private const string TypeCacheName = "MVC-AreaRegistrationTypeCache.xml";
	}
}
