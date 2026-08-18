using System;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200006F RID: 111
	public class ArrayMapper : EnumerableMapperBase<Array>
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00009894 File Offset: 0x00007A94
		public override bool IsMatch(TypePair context)
		{
			return context.DestinationType.IsArray && context.SourceType.IsEnumerableType();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000098B0 File Offset: 0x00007AB0
		protected override void ClearEnumerable(Array enumerable)
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000098B2 File Offset: 0x00007AB2
		protected override void SetElementValue(Array destination, object mappedValue, int index)
		{
			destination.SetValue(mappedValue, index);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00008F3F File Offset: 0x0000713F
		protected override Array CreateDestinationObjectBase(Type destElementType, int sourceLength)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000098BC File Offset: 0x00007ABC
		protected override bool ShouldAssignEnumerable(ResolutionContext context)
		{
			return !context.IsSourceValueNull && context.DestinationType.IsAssignableFrom(context.SourceType);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000098D9 File Offset: 0x00007AD9
		protected override object GetOrCreateDestinationObject(ResolutionContext context, Type destElementType, int sourceLength)
		{
			return ObjectCreator.CreateArray(destElementType, sourceLength);
		}
	}
}
