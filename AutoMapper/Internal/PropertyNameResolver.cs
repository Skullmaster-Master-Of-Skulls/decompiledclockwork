using System;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x020000B6 RID: 182
	public class PropertyNameResolver : IValueResolver
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x0001436F File Offset: 0x0001256F
		public PropertyNameResolver(Type sourceType, string propertyName)
		{
			this._sourceType = sourceType;
			this._propertyInfo = sourceType.GetProperty(propertyName);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0001438C File Offset: 0x0001258C
		public ResolutionResult Resolve(ResolutionResult source)
		{
			if (source.Value == null)
			{
				return source;
			}
			Type type = source.Value.GetType();
			if (!this._sourceType.IsAssignableFrom(type))
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Expected obj to be of type ",
					this._sourceType,
					" but was ",
					type
				}));
			}
			object value = this._propertyInfo.GetValue(source.Value, null);
			return source.New(value);
		}

		// Token: 0x040000F9 RID: 249
		private readonly Type _sourceType;

		// Token: 0x040000FA RID: 250
		private readonly PropertyInfo _propertyInfo;
	}
}
