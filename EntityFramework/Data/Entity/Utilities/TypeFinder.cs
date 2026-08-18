using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x020006EF RID: 1775
	internal class TypeFinder
	{
		// Token: 0x06004729 RID: 18217 RVA: 0x00150D0F File Offset: 0x0014EF0F
		public TypeFinder(Assembly assembly)
		{
			this._assembly = assembly;
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x00150D5C File Offset: 0x0014EF5C
		public Type FindType(Type baseType, string typeName, Func<IEnumerable<Type>, IEnumerable<Type>> filter, Func<string, Exception> noType = null, Func<string, IEnumerable<Type>, Exception> multipleTypes = null, Func<string, string, Exception> noTypeWithName = null, Func<string, string, Exception> multipleTypesWithName = null)
		{
			bool flag = !string.IsNullOrWhiteSpace(typeName);
			Type type = null;
			if (flag)
			{
				type = this._assembly.GetType(typeName);
			}
			if (type == null)
			{
				string name = this._assembly.GetName().Name;
				IEnumerable<Type> enumerable = from t in this._assembly.GetAccessibleTypes()
				where baseType.IsAssignableFrom(t)
				select t;
				if (flag)
				{
					enumerable = (from t in enumerable
					where string.Equals(t.Name, typeName, StringComparison.OrdinalIgnoreCase)
					select t).ToList<Type>();
					if (enumerable.Count<Type>() > 1)
					{
						enumerable = (from t in enumerable
						where string.Equals(t.Name, typeName, StringComparison.Ordinal)
						select t).ToList<Type>();
					}
					if (!enumerable.Any<Type>())
					{
						if (noTypeWithName != null)
						{
							throw noTypeWithName(typeName, name);
						}
						return null;
					}
					else if (enumerable.Count<Type>() > 1)
					{
						if (multipleTypesWithName != null)
						{
							throw multipleTypesWithName(typeName, name);
						}
						return null;
					}
				}
				else
				{
					enumerable = filter(enumerable);
					if (!enumerable.Any<Type>())
					{
						if (noType != null)
						{
							throw noType(name);
						}
						return null;
					}
					else if (enumerable.Count<Type>() > 1)
					{
						if (multipleTypes != null)
						{
							throw multipleTypes(name, enumerable);
						}
						return null;
					}
				}
				type = enumerable.Single<Type>();
			}
			return type;
		}

		// Token: 0x04001A1A RID: 6682
		private readonly Assembly _assembly;
	}
}
