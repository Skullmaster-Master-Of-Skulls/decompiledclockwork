using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.Data.Design
{
	// Token: 0x02000258 RID: 600
	internal sealed class ProviderManager
	{
		// Token: 0x060016F9 RID: 5881 RVA: 0x0007E05C File Offset: 0x0007C25C
		public static DbProviderFactory GetFactoryFromType(Type type, ProviderManager.ProviderSupportedClasses kindOfObject)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (ProviderManager.providerData.Matches(type))
			{
				return ProviderManager.providerData.CachedFactory;
			}
			ProviderManager.EnsureFactoryTable();
			foreach (object obj in ProviderManager.factoryTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DbProviderFactory factory = DbProviderFactories.GetFactory(dataRow);
				string providerName = (string)dataRow[ProviderManager.PROVIDER_NAME];
				object obj2 = ProviderManager.CreateObject(factory, kindOfObject, providerName);
				if (type.Equals(obj2.GetType()))
				{
					ProviderManager.providerData.Initialize(factory, (string)dataRow[ProviderManager.PROVIDER_INVARIANT_NAME], (string)dataRow[ProviderManager.PROVIDER_NAME], type);
					return factory;
				}
			}
			throw new InternalException(string.Format(CultureInfo.CurrentCulture, "Unable to find DbProviderFactory for type {0}", new object[]
			{
				type.ToString()
			}));
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0007E170 File Offset: 0x0007C370
		public static string GetInvariantProviderName(DbProviderFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			if (ProviderManager.providerData.Matches(factory))
			{
				return ProviderManager.providerData.CachedInvariantProviderName;
			}
			ProviderManager.EnsureFactoryTable();
			string assemblyQualifiedName = factory.GetType().AssemblyQualifiedName;
			foreach (object obj in ProviderManager.factoryTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (StringUtil.EqualValue((string)dataRow[ProviderManager.PROVIDER_ASSEMBLY], assemblyQualifiedName))
				{
					ProviderManager.providerData.Initialize(factory, (string)dataRow[ProviderManager.PROVIDER_INVARIANT_NAME], (string)dataRow[ProviderManager.PROVIDER_NAME]);
					return ProviderManager.providerData.CachedInvariantProviderName;
				}
			}
			throw new InternalException(string.Format(CultureInfo.CurrentCulture, "Unable to get invariant name from factory. Factory type is {0}", new object[]
			{
				factory.GetType().ToString()
			}));
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0007E27C File Offset: 0x0007C47C
		public static DbProviderFactory GetFactory(string invariantName)
		{
			if (StringUtil.EmptyOrSpace(invariantName))
			{
				throw new ArgumentNullException("invariantName");
			}
			if (ProviderManager.ActiveFactoryContext != null)
			{
				ProviderManager.providerData.Initialize(ProviderManager.ActiveFactoryContext, invariantName, invariantName);
				return ProviderManager.ActiveFactoryContext;
			}
			if (ProviderManager.CustomDBProviders != null && ProviderManager.CustomDBProviders.ContainsKey(invariantName))
			{
				DbProviderFactory dbProviderFactory = ProviderManager.CustomDBProviders[invariantName] as DbProviderFactory;
				if (dbProviderFactory != null)
				{
					ProviderManager.providerData.Initialize(dbProviderFactory, invariantName, invariantName);
					return dbProviderFactory;
				}
			}
			if (ProviderManager.providerData.Matches(invariantName))
			{
				return ProviderManager.providerData.CachedFactory;
			}
			ProviderManager.EnsureFactoryTable();
			DataRow[] array = ProviderManager.factoryTable.Select(string.Format(CultureInfo.CurrentCulture, "InvariantName = '{0}'", new object[]
			{
				invariantName
			}));
			if (array.Length == 0)
			{
				throw new InternalException(string.Format(CultureInfo.CurrentCulture, "Cannot find provider factory for provider named {0}", new object[]
				{
					invariantName
				}));
			}
			if (array.Length > 1)
			{
				throw new InternalException(string.Format(CultureInfo.CurrentCulture, "More that one data row for provider named {0}", new object[]
				{
					invariantName
				}));
			}
			DbProviderFactory factory = DbProviderFactories.GetFactory(array[0]);
			ProviderManager.providerData.Initialize(factory, invariantName, (string)array[0][ProviderManager.PROVIDER_NAME]);
			return factory;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0007E3A4 File Offset: 0x0007C5A4
		public static PropertyInfo GetProviderTypeProperty(DbProviderFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory should not be null.");
			}
			if (ProviderManager.providerData.UseCachedPropertyValue)
			{
				return ProviderManager.providerData.ProviderTypeProperty;
			}
			ProviderManager.providerData.UseCachedPropertyValue = true;
			DbParameter dbParameter = factory.CreateParameter();
			PropertyInfo[] properties = dbParameter.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.PropertyType.IsEnum)
				{
					object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DbProviderSpecificTypePropertyAttribute), true);
					if (customAttributes.Length != 0 && ((DbProviderSpecificTypePropertyAttribute)customAttributes[0]).IsProviderSpecificTypeProperty)
					{
						ProviderManager.providerData.ProviderTypeProperty = propertyInfo;
						return propertyInfo;
					}
				}
			}
			ProviderManager.providerData.ProviderTypeProperty = null;
			return null;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0007E460 File Offset: 0x0007C660
		private static object CreateObject(DbProviderFactory factory, ProviderManager.ProviderSupportedClasses kindOfObject, string providerName)
		{
			switch (kindOfObject)
			{
			case ProviderManager.ProviderSupportedClasses.DbConnection:
				return factory.CreateConnection();
			case ProviderManager.ProviderSupportedClasses.DbDataAdapter:
				return factory.CreateDataAdapter();
			case ProviderManager.ProviderSupportedClasses.DbParameter:
				return factory.CreateParameter();
			case ProviderManager.ProviderSupportedClasses.DbCommand:
				return factory.CreateCommand();
			case ProviderManager.ProviderSupportedClasses.DbCommandBuilder:
				return factory.CreateCommandBuilder();
			case ProviderManager.ProviderSupportedClasses.DbDataSourceEnumerator:
				return factory.CreateDataSourceEnumerator();
			case ProviderManager.ProviderSupportedClasses.CodeAccessPermission:
				return factory.CreatePermission(PermissionState.None);
			default:
			{
				string internalMessage = string.Format(CultureInfo.CurrentCulture, "Cannot create object of provider class identified by enum {0} for provider {1}", new object[]
				{
					Enum.GetName(typeof(ProviderManager.ProviderSupportedClasses), kindOfObject),
					providerName
				});
				throw new InternalException(internalMessage);
			}
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0007E4FB File Offset: 0x0007C6FB
		private static void EnsureFactoryTable()
		{
			if (ProviderManager.factoryTable == null)
			{
				ProviderManager.factoryTable = DbProviderFactories.GetFactoryClasses();
				if (ProviderManager.factoryTable == null)
				{
					throw new InternalException("Unable to get factory-table.");
				}
			}
		}

		// Token: 0x04000BB2 RID: 2994
		private static DataTable factoryTable = null;

		// Token: 0x04000BB3 RID: 2995
		private static ProviderManager.CachedProviderData providerData = new ProviderManager.CachedProviderData();

		// Token: 0x04000BB4 RID: 2996
		internal static Hashtable CustomDBProviders = null;

		// Token: 0x04000BB5 RID: 2997
		internal static DbProviderFactory ActiveFactoryContext = null;

		// Token: 0x04000BB6 RID: 2998
		private static readonly string PROVIDER_NAME = "Name";

		// Token: 0x04000BB7 RID: 2999
		private static readonly string PROVIDER_INVARIANT_NAME = "InvariantName";

		// Token: 0x04000BB8 RID: 3000
		private static readonly string PROVIDER_ASSEMBLY = "AssemblyQualifiedName";

		// Token: 0x020004BF RID: 1215
		internal enum ProviderSupportedClasses
		{
			// Token: 0x04001EA8 RID: 7848
			DbConnection,
			// Token: 0x04001EA9 RID: 7849
			DbDataAdapter,
			// Token: 0x04001EAA RID: 7850
			DbParameter,
			// Token: 0x04001EAB RID: 7851
			DbCommand,
			// Token: 0x04001EAC RID: 7852
			DbCommandBuilder,
			// Token: 0x04001EAD RID: 7853
			DbDataSourceEnumerator,
			// Token: 0x04001EAE RID: 7854
			CodeAccessPermission,
			// Token: 0x04001EAF RID: 7855
			DbConnectionStringBuilder
		}

		// Token: 0x020004C0 RID: 1216
		private class CachedProviderData
		{
			// Token: 0x17000957 RID: 2391
			// (get) Token: 0x06002C38 RID: 11320 RVA: 0x00107219 File Offset: 0x00105419
			// (set) Token: 0x06002C39 RID: 11321 RVA: 0x00107221 File Offset: 0x00105421
			public PropertyInfo ProviderTypeProperty
			{
				get
				{
					return this.providerTypeProperty;
				}
				set
				{
					this.providerTypeProperty = value;
				}
			}

			// Token: 0x17000958 RID: 2392
			// (get) Token: 0x06002C3A RID: 11322 RVA: 0x0010722A File Offset: 0x0010542A
			// (set) Token: 0x06002C3B RID: 11323 RVA: 0x00107232 File Offset: 0x00105432
			public bool UseCachedPropertyValue
			{
				get
				{
					return this.useCachedPropertyValue;
				}
				set
				{
					this.useCachedPropertyValue = value;
				}
			}

			// Token: 0x06002C3C RID: 11324 RVA: 0x0010723B File Offset: 0x0010543B
			public bool Matches(Type type)
			{
				return this.CachedFactory != null && this.CachedType != null && this.CachedType.Equals(type);
			}

			// Token: 0x06002C3D RID: 11325 RVA: 0x00107264 File Offset: 0x00105464
			public bool Matches(string invariantName)
			{
				return this.CachedFactory != null && this.CachedInvariantProviderName != null && StringUtil.EqualValue(this.CachedInvariantProviderName, invariantName);
			}

			// Token: 0x06002C3E RID: 11326 RVA: 0x00107287 File Offset: 0x00105487
			public bool Matches(DbProviderFactory factory)
			{
				return this.CachedFactory != null && this.CachedFactory.GetType().Equals(factory.GetType());
			}

			// Token: 0x06002C3F RID: 11327 RVA: 0x001072AC File Offset: 0x001054AC
			public void Initialize(DbProviderFactory factory, string invariantProviderName, string displayName)
			{
				this.CachedFactory = factory;
				this.CachedInvariantProviderName = invariantProviderName;
				this.CachedType = null;
				this.CachedDisplayName = displayName;
				this.ProviderTypeProperty = null;
				this.UseCachedPropertyValue = false;
			}

			// Token: 0x06002C40 RID: 11328 RVA: 0x001072D8 File Offset: 0x001054D8
			public void Initialize(DbProviderFactory factory, string invariantProviderName, string displayName, Type type)
			{
				this.Initialize(factory, invariantProviderName, displayName);
				this.CachedType = type;
			}

			// Token: 0x04001EB0 RID: 7856
			public DbProviderFactory CachedFactory;

			// Token: 0x04001EB1 RID: 7857
			public Type CachedType;

			// Token: 0x04001EB2 RID: 7858
			public string CachedInvariantProviderName = string.Empty;

			// Token: 0x04001EB3 RID: 7859
			public string CachedDisplayName = string.Empty;

			// Token: 0x04001EB4 RID: 7860
			private PropertyInfo providerTypeProperty;

			// Token: 0x04001EB5 RID: 7861
			private bool useCachedPropertyValue;
		}
	}
}
