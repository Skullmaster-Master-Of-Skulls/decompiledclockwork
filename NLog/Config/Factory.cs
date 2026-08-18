using System;
using System.Collections.Generic;
using NLog.Common;
using NLog.Internal;

namespace NLog.Config
{
	// Token: 0x02000049 RID: 73
	internal class Factory<TBaseType, TAttributeType> : INamedItemFactory<TBaseType, Type>, IFactory where TBaseType : class where TAttributeType : NameBaseAttribute
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005315 File Offset: 0x00003515
		internal Factory(ConfigurationItemFactory parentFactory)
		{
			this.parentFactory = parentFactory;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005334 File Offset: 0x00003534
		public void ScanTypes(Type[] types, string prefix)
		{
			foreach (Type type in types)
			{
				try
				{
					this.RegisterType(type, prefix);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Failed to add type '{0}'.", new object[]
					{
						type.FullName
					});
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000539C File Offset: 0x0000359C
		public void RegisterType(Type type, string itemNamePrefix)
		{
			TAttributeType[] array = (TAttributeType[])type.GetCustomAttributes(typeof(TAttributeType), false);
			if (array != null)
			{
				foreach (TAttributeType tattributeType in array)
				{
					this.RegisterDefinition(itemNamePrefix + tattributeType.Name, type);
				}
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000540C File Offset: 0x0000360C
		public void RegisterNamedType(string itemName, string typeName)
		{
			this.items[itemName] = (() => Type.GetType(typeName, false));
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000543E File Offset: 0x0000363E
		public void Clear()
		{
			this.items.Clear();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000545C File Offset: 0x0000365C
		public void RegisterDefinition(string name, Type type)
		{
			this.items[name] = (() => type);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005490 File Offset: 0x00003690
		public bool TryGetDefinition(string itemName, out Type result)
		{
			Factory<TBaseType, TAttributeType>.GetTypeDelegate getTypeDelegate;
			if (!this.items.TryGetValue(itemName, out getTypeDelegate))
			{
				result = null;
				return false;
			}
			bool result2;
			try
			{
				result = getTypeDelegate();
				result2 = (result != null);
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrown())
				{
					throw;
				}
				result = null;
				result2 = false;
			}
			return result2;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000054E8 File Offset: 0x000036E8
		public bool TryCreateInstance(string itemName, out TBaseType result)
		{
			Type itemType;
			if (!this.TryGetDefinition(itemName, out itemType))
			{
				result = default(TBaseType);
				return false;
			}
			result = (TBaseType)((object)this.parentFactory.CreateInstance(itemType));
			return true;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005528 File Offset: 0x00003728
		public TBaseType CreateInstance(string name)
		{
			TBaseType result;
			if (this.TryCreateInstance(name, out result))
			{
				return result;
			}
			throw new ArgumentException(typeof(TBaseType).Name + " cannot be found: '" + name + "'");
		}

		// Token: 0x04000085 RID: 133
		private readonly Dictionary<string, Factory<TBaseType, TAttributeType>.GetTypeDelegate> items = new Dictionary<string, Factory<TBaseType, TAttributeType>.GetTypeDelegate>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000086 RID: 134
		private ConfigurationItemFactory parentFactory;

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x0600015D RID: 349
		private delegate Type GetTypeDelegate();
	}
}
