using System;

namespace AutoMapper
{
	// Token: 0x0200003B RID: 59
	public abstract class TypeConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00005EB4 File Offset: 0x000040B4
		public TDestination Convert(ResolutionContext context)
		{
			if (context.SourceValue != null && !(context.SourceValue is TSource))
			{
				throw new AutoMapperMappingException(context, string.Format("Value supplied is of type {0} but expected {1}.\nChange the type converter source type, or redirect the source value supplied to the value resolver using FromMember.", typeof(TSource), context.SourceValue.GetType()));
			}
			return this.ConvertCore((TSource)((object)context.SourceValue));
		}

		// Token: 0x06000276 RID: 630
		protected abstract TDestination ConvertCore(TSource source);
	}
}
