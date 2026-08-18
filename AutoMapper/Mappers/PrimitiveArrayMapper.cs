using System;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200008C RID: 140
	public class PrimitiveArrayMapper : IObjectMapper
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x000118E4 File Offset: 0x0000FAE4
		public object Map(ResolutionContext context)
		{
			if (context.IsSourceValueNull && context.Engine.ShouldMapSourceCollectionAsNull(context))
			{
				return null;
			}
			if (!context.IsSourceValueNull && context.DestinationType.IsAssignableFrom(context.SourceType))
			{
				return context.SourceValue;
			}
			Type elementType = TypeHelper.GetElementType(context.SourceType);
			Type elementType2 = TypeHelper.GetElementType(context.DestinationType);
			Array array = ((Array)context.SourceValue) ?? ObjectCreator.CreateArray(elementType, 0);
			int length = array.Length;
			Array array2 = ObjectCreator.CreateArray(elementType2, length);
			Array.Copy(array, array2, length);
			return array2;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00011970 File Offset: 0x0000FB70
		private bool IsPrimitiveArrayType(Type type)
		{
			if (type.IsArray)
			{
				Type elementType = TypeHelper.GetElementType(type);
				return elementType.IsPrimitive() || elementType.Equals(typeof(string));
			}
			return false;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000119A8 File Offset: 0x0000FBA8
		public bool IsMatch(TypePair context)
		{
			return this.IsPrimitiveArrayType(context.DestinationType) && this.IsPrimitiveArrayType(context.SourceType) && TypeHelper.GetElementType(context.DestinationType).Equals(TypeHelper.GetElementType(context.SourceType));
		}
	}
}
