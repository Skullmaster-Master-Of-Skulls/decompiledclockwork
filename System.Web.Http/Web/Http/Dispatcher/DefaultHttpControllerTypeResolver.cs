using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x020000A9 RID: 169
	public class DefaultHttpControllerTypeResolver : IHttpControllerTypeResolver
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000C66A File Offset: 0x0000A86A
		public DefaultHttpControllerTypeResolver() : this(new Predicate<Type>(DefaultHttpControllerTypeResolver.IsControllerType))
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000C67E File Offset: 0x0000A87E
		public DefaultHttpControllerTypeResolver(Predicate<Type> predicate)
		{
			if (predicate == null)
			{
				throw Error.ArgumentNull("predicate");
			}
			this._isControllerTypePredicate = predicate;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000C6AD File Offset: 0x0000A8AD
		protected internal virtual Predicate<Type> IsControllerTypePredicate
		{
			get
			{
				return this._isControllerTypePredicate;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000C6B5 File Offset: 0x0000A8B5
		internal static bool IsControllerType(Type t)
		{
			return t != null && t.IsClass && t.IsVisible && !t.IsAbstract && typeof(IHttpController).IsAssignableFrom(t) && DefaultHttpControllerTypeResolver.HasValidControllerName(t);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000C70C File Offset: 0x0000A90C
		public virtual ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
		{
			if (assembliesResolver == null)
			{
				throw Error.ArgumentNull("assembliesResolver");
			}
			List<Type> list = new List<Type>();
			ICollection<Assembly> assemblies = assembliesResolver.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				Type[] array = null;
				if (!(assembly == null) && !assembly.IsDynamic)
				{
					try
					{
						array = this._getTypesFunc(assembly);
					}
					catch (ReflectionTypeLoadException ex)
					{
						array = ex.Types;
					}
					catch
					{
						continue;
					}
					if (array != null)
					{
						list.AddRange(from x in array
						where DefaultHttpControllerTypeResolver.TypeIsVisible(x) && this.IsControllerTypePredicate(x)
						select x);
					}
				}
			}
			return list;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		internal static Type[] GetTypes(Assembly assembly)
		{
			return assembly.GetTypes();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		internal static bool HasValidControllerName(Type controllerType)
		{
			string controllerSuffix = DefaultHttpControllerSelector.ControllerSuffix;
			return controllerType.Name.Length > controllerSuffix.Length && controllerType.Name.EndsWith(controllerSuffix, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000C81D File Offset: 0x0000AA1D
		internal void SetGetTypesFunc(Func<Assembly, Type[]> getTypesFunc)
		{
			this._getTypesFunc = getTypesFunc;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000C826 File Offset: 0x0000AA26
		private static bool TypeIsVisible(Type type)
		{
			return type != null && type.IsVisible;
		}

		// Token: 0x04000126 RID: 294
		private readonly Predicate<Type> _isControllerTypePredicate;

		// Token: 0x04000127 RID: 295
		private Func<Assembly, Type[]> _getTypesFunc = new Func<Assembly, Type[]>(DefaultHttpControllerTypeResolver.GetTypes);
	}
}
