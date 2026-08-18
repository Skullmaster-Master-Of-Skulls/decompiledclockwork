using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Telerik.Web
{
	// Token: 0x020001C7 RID: 455
	internal static class RenderModesCache
	{
		// Token: 0x06001093 RID: 4243 RVA: 0x0003CE80 File Offset: 0x0003B080
		private static void InitViewDescriptorsList()
		{
			RenderModesCache.viewDescriptors = new SynchronizedCollection<ViewDescriptorAttribute>();
			Type[] source = null;
			try
			{
				source = Assembly.GetExecutingAssembly().GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				source = ex.Types;
			}
			SynchronizedCollection<ViewDescriptorAttribute> synchronizedCollection = new SynchronizedCollection<ViewDescriptorAttribute>();
			IEnumerable<Type> enumerable = from t in source
			where t != null && t.IsClass && t.IsSealed
			select t;
			foreach (Type type in enumerable)
			{
				RenderModesCache.AddRange(synchronizedCollection, from a in type.GetCustomAttributes(typeof(ViewDescriptorAttribute), false)
				select a as ViewDescriptorAttribute);
			}
			RenderModesCache.AddRange(RenderModesCache.viewDescriptors, synchronizedCollection);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0003CF7C File Offset: 0x0003B17C
		private static void InitControlsListByAttribute(Type attributeType, SynchronizedCollection<Type> target)
		{
			Type[] source = null;
			try
			{
				source = Assembly.GetExecutingAssembly().GetTypes();
			}
			catch (ReflectionTypeLoadException ex)
			{
				source = ex.Types;
			}
			IEnumerable<Type> enumerable = from t in source
			where t != null && t.IsClass
			select t;
			foreach (Type type in enumerable)
			{
				if (type.GetCustomAttributes(attributeType, false).Count<object>() > 0)
				{
					target.Add(type);
				}
			}
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0003D024 File Offset: 0x0003B224
		private static void AddRange(SynchronizedCollection<ViewDescriptorAttribute> target, IEnumerable<ViewDescriptorAttribute> range)
		{
			foreach (ViewDescriptorAttribute item in range)
			{
				target.Add(item);
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0003D06C File Offset: 0x0003B26C
		static RenderModesCache()
		{
			RenderModesCache.InitViewDescriptorsList();
			RenderModesCache.InitControlsListByAttribute(typeof(AdaptiveRenderingAttribute), RenderModesCache.adaptiveControls);
			RenderModesCache.InitControlsListByAttribute(typeof(LightweightRenderingAttribute), RenderModesCache.lightControls);
			RenderModesCache.InitControlsListByAttribute(typeof(NativeRenderingAttribute), RenderModesCache.nativeControls);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0003D0D8 File Offset: 0x0003B2D8
		public static SynchronizedCollection<ViewDescriptorAttribute> GetViewDescriptors()
		{
			return RenderModesCache.viewDescriptors;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0003D0DF File Offset: 0x0003B2DF
		public static SynchronizedCollection<Type> GetAdaptiveTypes()
		{
			return RenderModesCache.adaptiveControls;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0003D0E6 File Offset: 0x0003B2E6
		public static SynchronizedCollection<Type> GetLightweightTypes()
		{
			return RenderModesCache.lightControls;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0003D0ED File Offset: 0x0003B2ED
		public static SynchronizedCollection<Type> GetNativeTypes()
		{
			return RenderModesCache.nativeControls;
		}

		// Token: 0x040004B2 RID: 1202
		private static SynchronizedCollection<ViewDescriptorAttribute> viewDescriptors;

		// Token: 0x040004B3 RID: 1203
		private static readonly SynchronizedCollection<Type> adaptiveControls = new SynchronizedCollection<Type>();

		// Token: 0x040004B4 RID: 1204
		private static readonly SynchronizedCollection<Type> lightControls = new SynchronizedCollection<Type>();

		// Token: 0x040004B5 RID: 1205
		private static readonly SynchronizedCollection<Type> nativeControls = new SynchronizedCollection<Type>();
	}
}
