using System;
using System.Collections;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200007A RID: 122
	public class EnumerableMapper : EnumerableMapperBase<IList>
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00010814 File Offset: 0x0000EA14
		public override bool IsMatch(TypePair context)
		{
			return ((context.DestinationType.IsInterface() && context.DestinationType.IsEnumerableType()) || context.DestinationType.IsListType()) && context.SourceType.IsEnumerableType();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001084A File Offset: 0x0000EA4A
		protected override void SetElementValue(IList destination, object mappedValue, int index)
		{
			destination.Add(mappedValue);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010854 File Offset: 0x0000EA54
		protected override void ClearEnumerable(IList enumerable)
		{
			enumerable.Clear();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001085C File Offset: 0x0000EA5C
		protected override object GetOrCreateDestinationObject(ResolutionContext context, Type destElementType, int sourceLength)
		{
			if (context.DestinationValue is IList && !(context.DestinationValue is Array))
			{
				return context.DestinationValue;
			}
			return ObjectCreator.CreateList(destElementType);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010885 File Offset: 0x0000EA85
		protected override IList CreateDestinationObjectBase(Type destElementType, int sourceLength)
		{
			return ObjectCreator.CreateList(destElementType);
		}
	}
}
