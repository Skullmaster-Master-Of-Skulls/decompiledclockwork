using System;
using System.Reflection;

namespace AutoMapper.Internal
{
	// Token: 0x02000096 RID: 150
	public class DeferredInstantiatedConverter : ITypeConverter<object, object>
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x000125AF File Offset: 0x000107AF
		public DeferredInstantiatedConverter(Type typeConverterType, Func<ResolutionContext, object> instantiator)
		{
			this._instantiator = instantiator;
			this._converterMethod = typeConverterType.GetMethod("Convert");
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000125D0 File Offset: 0x000107D0
		public object Convert(ResolutionContext context)
		{
			object obj = this._instantiator(context);
			return (this._converterMethod.ContainsGenericParameters ? this.GetClosedConvertMethod(context) : this._converterMethod).Invoke(obj, new ResolutionContext[]
			{
				context
			});
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00012616 File Offset: 0x00010816
		private MethodInfo GetClosedConvertMethod(ResolutionContext context)
		{
			return typeof(ITypeConverter<, >).MakeGenericType(new Type[]
			{
				context.SourceType,
				context.DestinationType
			}).GetMethod("Convert");
		}

		// Token: 0x040000D5 RID: 213
		private readonly Func<ResolutionContext, object> _instantiator;

		// Token: 0x040000D6 RID: 214
		private readonly MethodInfo _converterMethod;
	}
}
