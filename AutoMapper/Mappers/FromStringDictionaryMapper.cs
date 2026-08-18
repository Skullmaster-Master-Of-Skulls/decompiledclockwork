using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200008F RID: 143
	public class FromStringDictionaryMapper : IObjectMapper
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00011B6A File Offset: 0x0000FD6A
		public bool IsMatch(TypePair context)
		{
			return typeof(IDictionary<string, object>).IsAssignableFrom(context.SourceType);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00011B84 File Offset: 0x0000FD84
		public object Map(ResolutionContext context)
		{
			IDictionary<string, object> dictionary = (IDictionary<string, object>)context.SourceValue;
			object obj = context.Engine.CreateObject(context);
			TypeDetails typeDetails = new TypeDetails(context.DestinationType, (PropertyInfo _) => true, (FieldInfo _) => true);
			foreach (MemberInfo memberInfo in from name in dictionary.Keys
			join member in typeDetails.PublicWriteAccessors on name equals member.Name
			select member)
			{
				object value = ReflectionHelper.Map(context, memberInfo, dictionary[memberInfo.Name]);
				memberInfo.SetMemberValue(obj, value);
			}
			return obj;
		}
	}
}
