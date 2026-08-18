using System;
using System.Configuration;
using System.Data.Entity.Internal.ConfigFile;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x020002A6 RID: 678
	internal class InitializerConfig
	{
		// Token: 0x060017F7 RID: 6135 RVA: 0x000790A3 File Offset: 0x000772A3
		public InitializerConfig()
		{
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000790AB File Offset: 0x000772AB
		public InitializerConfig(EntityFrameworkSection entityFrameworkSettings, KeyValueConfigurationCollection appSettings)
		{
			this._entityFrameworkSettings = entityFrameworkSettings;
			this._appSettings = appSettings;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000790C4 File Offset: 0x000772C4
		private static object TryGetInitializer(Type requiredContextType, string contextTypeName, string initializerTypeName, bool isDisabled, Func<object[]> initializerArgs, Func<object, object, string> exceptionMessage)
		{
			try
			{
				if (Type.GetType(contextTypeName, true) == requiredContextType)
				{
					if (isDisabled)
					{
						return Activator.CreateInstance(typeof(NullDatabaseInitializer<>).MakeGenericType(new Type[]
						{
							requiredContextType
						}));
					}
					return Activator.CreateInstance(Type.GetType(initializerTypeName, true), initializerArgs());
				}
			}
			catch (Exception innerException)
			{
				string arg = isDisabled ? "Disabled" : initializerTypeName;
				throw new InvalidOperationException(exceptionMessage(arg, contextTypeName), innerException);
			}
			return null;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00079150 File Offset: 0x00077350
		public virtual object TryGetInitializer(Type contextType)
		{
			return this.TryGetInitializerFromEntityFrameworkSection(contextType) ?? this.TryGetInitializerFromLegacyConfig(contextType);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00079228 File Offset: 0x00077428
		private object TryGetInitializerFromEntityFrameworkSection(Type contextType)
		{
			return (from e in this._entityFrameworkSettings.Contexts.OfType<ContextElement>()
			where e.IsDatabaseInitializationDisabled || !string.IsNullOrWhiteSpace(e.DatabaseInitializer.InitializerTypeName)
			select InitializerConfig.TryGetInitializer(contextType, e.ContextTypeName, e.DatabaseInitializer.InitializerTypeName ?? string.Empty, e.IsDatabaseInitializationDisabled, () => e.DatabaseInitializer.Parameters.GetTypedParameterValues(), new Func<object, object, string>(Strings.Database_InitializeFromConfigFailed))).FirstOrDefault((object i) => i != null);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000792C0 File Offset: 0x000774C0
		private object TryGetInitializerFromLegacyConfig(Type contextType)
		{
			foreach (string text in from k in this._appSettings.AllKeys
			where k.StartsWith("DatabaseInitializerForType", StringComparison.OrdinalIgnoreCase)
			select k)
			{
				string text2 = text.Remove(0, "DatabaseInitializerForType".Length).Trim();
				string text3 = (this._appSettings[text].Value ?? string.Empty).Trim();
				if (string.IsNullOrWhiteSpace(text2))
				{
					throw new InvalidOperationException(Strings.Database_BadLegacyInitializerEntry(text, text3));
				}
				object obj = InitializerConfig.TryGetInitializer(contextType, text2, text3, text3.Length == 0 || text3.Equals("Disabled", StringComparison.OrdinalIgnoreCase), () => new object[0], new Func<object, object, string>(Strings.Database_InitializeFromLegacyConfigFailed));
				if (obj != null)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x04000860 RID: 2144
		private const string ConfigKeyKey = "DatabaseInitializerForType";

		// Token: 0x04000861 RID: 2145
		private const string DisabledSpecialValue = "Disabled";

		// Token: 0x04000862 RID: 2146
		private readonly EntityFrameworkSection _entityFrameworkSettings;

		// Token: 0x04000863 RID: 2147
		private readonly KeyValueConfigurationCollection _appSettings;
	}
}
