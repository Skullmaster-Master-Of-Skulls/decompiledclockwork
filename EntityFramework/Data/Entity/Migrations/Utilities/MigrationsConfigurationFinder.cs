using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace System.Data.Entity.Migrations.Utilities
{
	// Token: 0x020002AC RID: 684
	internal class MigrationsConfigurationFinder
	{
		// Token: 0x06001817 RID: 6167 RVA: 0x000797A0 File Offset: 0x000779A0
		public MigrationsConfigurationFinder()
		{
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000797A8 File Offset: 0x000779A8
		public MigrationsConfigurationFinder(TypeFinder typeFinder)
		{
			this._typeFinder = typeFinder;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00079814 File Offset: 0x00077A14
		public virtual DbMigrationsConfiguration FindMigrationsConfiguration(Type contextType, string configurationTypeName, Func<string, Exception> noType = null, Func<string, IEnumerable<Type>, Exception> multipleTypes = null, Func<string, string, Exception> noTypeWithName = null, Func<string, string, Exception> multipleTypesWithName = null)
		{
			Type type = this._typeFinder.FindType((contextType == null) ? typeof(DbMigrationsConfiguration) : typeof(DbMigrationsConfiguration<>).MakeGenericType(new Type[]
			{
				contextType
			}), configurationTypeName, (IEnumerable<Type> types) => (from t in types
			where t.GetPublicConstructor(new Type[0]) != null && !t.IsAbstract() && !t.IsGenericType()
			select t).ToList<Type>(), noType, multipleTypes, noTypeWithName, multipleTypesWithName);
			DbMigrationsConfiguration result;
			try
			{
				DbMigrationsConfiguration dbMigrationsConfiguration;
				if (!(type == null))
				{
					dbMigrationsConfiguration = type.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadMigrationsConfigurationType), (string s) => new MigrationsException(s));
				}
				else
				{
					dbMigrationsConfiguration = null;
				}
				result = dbMigrationsConfiguration;
			}
			catch (TargetInvocationException ex)
			{
				ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x0400086C RID: 2156
		private readonly TypeFinder _typeFinder;
	}
}
