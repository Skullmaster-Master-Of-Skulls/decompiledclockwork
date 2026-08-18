using System;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000085 RID: 133
	public class MultidimensionalArrayMapper : EnumerableMapperBase<Array>
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0001159B File Offset: 0x0000F79B
		public override bool IsMatch(TypePair context)
		{
			return context.DestinationType.IsArray && context.DestinationType.GetArrayRank() > 1 && context.SourceType.IsEnumerableType();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000098B0 File Offset: 0x00007AB0
		protected override void ClearEnumerable(Array enumerable)
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000115C5 File Offset: 0x0000F7C5
		protected override void SetElementValue(Array destination, object mappedValue, int index)
		{
			this.filler.NewValue(mappedValue);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00008F3F File Offset: 0x0000713F
		protected override Array CreateDestinationObjectBase(Type destElementType, int sourceLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000115D4 File Offset: 0x0000F7D4
		protected override object GetOrCreateDestinationObject(ResolutionContext context, Type destElementType, int sourceLength)
		{
			Array array = context.SourceValue as Array;
			if (array == null)
			{
				return ObjectCreator.CreateArray(destElementType, sourceLength);
			}
			Array array2 = ObjectCreator.CreateArray(destElementType, array);
			this.filler = new MultidimensionalArrayFiller(array2);
			return array2;
		}

		// Token: 0x040000CC RID: 204
		private MultidimensionalArrayFiller filler;
	}
}
