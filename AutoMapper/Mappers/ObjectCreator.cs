using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200008A RID: 138
	public static class ObjectCreator
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x000117AC File Offset: 0x0000F9AC
		public static Array CreateArray(Type elementType, int length)
		{
			return Array.CreateInstance(elementType, length);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000117B5 File Offset: 0x0000F9B5
		public static Array CreateArray(Type elementType, Array sourceArray)
		{
			return Array.CreateInstance(elementType, sourceArray.GetLengths());
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000117C3 File Offset: 0x0000F9C3
		public static IList CreateList(Type elementType)
		{
			return (IList)ObjectCreator.CreateObject(typeof(List<>).MakeGenericType(new Type[]
			{
				elementType
			}));
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000117E8 File Offset: 0x0000F9E8
		public static object CreateDictionary(Type dictionaryType, Type keyType, Type valueType)
		{
			return ObjectCreator.CreateObject(dictionaryType.IsInterface() ? typeof(Dictionary<, >).MakeGenericType(new Type[]
			{
				keyType,
				valueType
			}) : dictionaryType);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00011817 File Offset: 0x0000FA17
		public static object CreateDefaultValue(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			return ObjectCreator.CreateObject(type);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00011829 File Offset: 0x0000FA29
		public static object CreateNonNullValue(Type type)
		{
			if (type.IsValueType())
			{
				return ObjectCreator.CreateObject(type);
			}
			if (!(type == typeof(string)))
			{
				return ObjectCreator.CreateObject(type);
			}
			return string.Empty;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011858 File Offset: 0x0000FA58
		public static object CreateObject(Type type)
		{
			if (type.IsArray)
			{
				return ObjectCreator.CreateArray(type.GetElementType(), 0);
			}
			if (!(type == typeof(string)))
			{
				return ObjectCreator.DelegateFactory.CreateCtor(type)();
			}
			return null;
		}

		// Token: 0x040000CF RID: 207
		private static readonly DelegateFactory DelegateFactory = new DelegateFactory();
	}
}
