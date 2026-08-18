using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200008E RID: 142
	public class ToStringDictionaryMapper : IObjectMapper
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x00011A89 File Offset: 0x0000FC89
		public bool IsMatch(TypePair context)
		{
			return typeof(IDictionary<string, object>).IsAssignableFrom(context.DestinationType);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		public object Map(ResolutionContext context)
		{
			object source = context.SourceValue;
			Dictionary<string, object> dictionary = new TypeDetails(source.GetType(), (PropertyInfo _) => true, (FieldInfo _) => true).PublicReadAccessors.ToDictionary((MemberInfo p) => p.Name, (MemberInfo p) => p.GetMemberValue(source));
			ResolutionContext context2 = context.CreateTypeContext(null, dictionary, context.DestinationValue, dictionary.GetType(), context.DestinationType);
			return context.Engine.Map(context2);
		}
	}
}
