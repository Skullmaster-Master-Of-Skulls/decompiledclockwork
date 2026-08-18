using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001D5 RID: 469
	public class ControllerBuilder
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x00024C66 File Offset: 0x00022E66
		public ControllerBuilder() : this(null)
		{
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00024C80 File Offset: 0x00022E80
		internal ControllerBuilder(IResolver<IControllerFactory> serviceResolver)
		{
			IResolver<IControllerFactory> serviceResolver2 = serviceResolver;
			if (serviceResolver == null)
			{
				serviceResolver2 = new SingleServiceResolver<IControllerFactory>(() => this._factoryThunk(), new DefaultControllerFactory
				{
					ControllerBuilder = this
				}, "ControllerBuilder.GetControllerFactory");
			}
			this._serviceResolver = serviceResolver2;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00024CFC File Offset: 0x00022EFC
		public static ControllerBuilder Current
		{
			get
			{
				return ControllerBuilder._instance;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00024D03 File Offset: 0x00022F03
		public HashSet<string> DefaultNamespaces
		{
			get
			{
				return this._namespaces;
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00024D0B File Offset: 0x00022F0B
		public IControllerFactory GetControllerFactory()
		{
			return this._serviceResolver.Current;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00024D28 File Offset: 0x00022F28
		public void SetControllerFactory(IControllerFactory controllerFactory)
		{
			if (controllerFactory == null)
			{
				throw new ArgumentNullException("controllerFactory");
			}
			this._factoryThunk = (() => controllerFactory);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00024DCC File Offset: 0x00022FCC
		public void SetControllerFactory(Type controllerFactoryType)
		{
			if (controllerFactoryType == null)
			{
				throw new ArgumentNullException("controllerFactoryType");
			}
			if (!typeof(IControllerFactory).IsAssignableFrom(controllerFactoryType))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, MvcResources.ControllerBuilder_MissingIControllerFactory, new object[]
				{
					controllerFactoryType
				}), "controllerFactoryType");
			}
			this._factoryThunk = delegate()
			{
				IControllerFactory result;
				try
				{
					result = (IControllerFactory)Activator.CreateInstance(controllerFactoryType);
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ControllerBuilder_ErrorCreatingControllerFactory, new object[]
					{
						controllerFactoryType
					}), innerException);
				}
				return result;
			};
		}

		// Token: 0x040003A2 RID: 930
		private static ControllerBuilder _instance = new ControllerBuilder();

		// Token: 0x040003A3 RID: 931
		private Func<IControllerFactory> _factoryThunk = () => null;

		// Token: 0x040003A4 RID: 932
		private HashSet<string> _namespaces = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040003A5 RID: 933
		private IResolver<IControllerFactory> _serviceResolver;
	}
}
