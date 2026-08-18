using System;

namespace System.Web.Mvc
{
	// Token: 0x020001A4 RID: 420
	internal sealed class ControllerDescriptorCache : ReaderWriterCache<Type, ControllerDescriptor>
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		public ControllerDescriptor GetDescriptor(Type controllerType, Func<ControllerDescriptor> creator)
		{
			return base.FetchOrCreateItem(controllerType, creator);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001EB26 File Offset: 0x0001CD26
		internal ControllerDescriptor GetDescriptor<TArgument>(Type controllerType, Func<TArgument, ControllerDescriptor> creator, TArgument state)
		{
			return base.FetchOrCreateItem<TArgument>(controllerType, creator, state);
		}
	}
}
