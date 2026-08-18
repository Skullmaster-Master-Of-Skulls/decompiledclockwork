using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace TechnoPro.Common.DataStructure
{
	// Token: 0x02000003 RID: 3
	public static class DataSourceCreator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002090 File Offset: 0x00000290
		public static IEnumerable ToDataSource(this IEnumerable<IDictionary> list)
		{
			IDictionary dictionary = null;
			bool flag = false;
			using (IEnumerator<IDictionary> enumerator = list.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IDictionary dictionary2 = enumerator.Current;
					flag = true;
					dictionary = dictionary2;
				}
			}
			if (!flag)
			{
				return new object[0];
			}
			if (dictionary == null)
			{
				throw new ArgumentException("IDictionary entry cannot be null");
			}
			TypeBuilder typeBuilder = DataSourceCreator.GetTypeBuilder(list.GetHashCode());
			typeBuilder.DefineDefaultConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			foreach (object obj in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!DataSourceCreator.PropertNameRegex.IsMatch(Convert.ToString(dictionaryEntry.Key), 0))
				{
					throw new ArgumentException("Each key of IDictionary must be \r\n                                alphanumeric and start with character.");
				}
				DataSourceCreator.CreateProperty(typeBuilder, Convert.ToString(dictionaryEntry.Key), (dictionaryEntry.Value == null) ? typeof(object) : dictionaryEntry.Value.GetType());
			}
			return DataSourceCreator.GenerateEnumerable(typeBuilder.CreateType(), list, dictionary);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		private static IEnumerable GenerateEnumerable(Type objectType, IEnumerable<IDictionary> list, IDictionary firstDict)
		{
			Type type = typeof(List<>).MakeGenericType(new Type[]
			{
				objectType
			});
			object obj = Activator.CreateInstance(type);
			foreach (IDictionary dictionary in list)
			{
				if (dictionary == null)
				{
					throw new ArgumentException("IDictionary entry cannot be null");
				}
				object obj2 = Activator.CreateInstance(objectType);
				foreach (object obj3 in firstDict)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					if (dictionary.Contains(dictionaryEntry.Key))
					{
						PropertyInfo property = objectType.GetProperty(Convert.ToString(dictionaryEntry.Key));
						property.SetValue(obj2, Convert.ChangeType(dictionary[dictionaryEntry.Key], property.PropertyType, null), null);
					}
				}
				type.GetMethod("Add").Invoke(obj, new object[]
				{
					obj2
				});
			}
			return obj as IEnumerable;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022E0 File Offset: 0x000004E0
		private static TypeBuilder GetTypeBuilder(int code)
		{
			AssemblyName name = new AssemblyName("TempAssembly" + code.ToString());
			return AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run).DefineDynamicModule("MainModule").DefineType("TempType" + code.ToString(), TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.BeforeFieldInit, typeof(object));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002340 File Offset: 0x00000540
		private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
		{
			FieldBuilder field = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
			PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
			MethodBuilder methodBuilder = tb.DefineMethod("get_" + propertyName, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, propertyType, Type.EmptyTypes);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldfld, field);
			ilgenerator.Emit(OpCodes.Ret);
			MethodBuilder methodBuilder2 = tb.DefineMethod("set_" + propertyName, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName, null, new Type[]
			{
				propertyType
			});
			ILGenerator ilgenerator2 = methodBuilder2.GetILGenerator();
			ilgenerator2.Emit(OpCodes.Ldarg_0);
			ilgenerator2.Emit(OpCodes.Ldarg_1);
			ilgenerator2.Emit(OpCodes.Stfld, field);
			ilgenerator2.Emit(OpCodes.Ret);
			propertyBuilder.SetGetMethod(methodBuilder);
			propertyBuilder.SetSetMethod(methodBuilder2);
		}

		// Token: 0x04000003 RID: 3
		private static readonly Regex PropertNameRegex = new Regex("^[A-Za-z]+[A-Za-z0-9_]*$", RegexOptions.Singleline);
	}
}
