using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020008F7 RID: 2295
	internal class TCEAdapterGenerator
	{
		// Token: 0x0600532C RID: 21292 RVA: 0x0012CC48 File Offset: 0x0012BC48
		public void Process(ModuleBuilder ModBldr, ArrayList EventItfList)
		{
			this.m_Module = ModBldr;
			int count = EventItfList.Count;
			for (int i = 0; i < count; i++)
			{
				EventItfInfo eventItfInfo = (EventItfInfo)EventItfList[i];
				Type eventItfType = eventItfInfo.GetEventItfType();
				Type srcItfType = eventItfInfo.GetSrcItfType();
				string eventProviderName = eventItfInfo.GetEventProviderName();
				Type sinkHelperType = new EventSinkHelperWriter(this.m_Module, srcItfType, eventItfType).Perform();
				new EventProviderWriter(this.m_Module, eventProviderName, eventItfType, srcItfType, sinkHelperType).Perform();
			}
		}

		// Token: 0x0600532D RID: 21293 RVA: 0x0012CCC0 File Offset: 0x0012BCC0
		internal static void SetClassInterfaceTypeToNone(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_NoClassItfCABuilder == null)
			{
				Type[] types = new Type[]
				{
					typeof(ClassInterfaceType)
				};
				ConstructorInfo constructor = typeof(ClassInterfaceAttribute).GetConstructor(types);
				TCEAdapterGenerator.s_NoClassItfCABuilder = new CustomAttributeBuilder(constructor, new object[]
				{
					ClassInterfaceType.None
				});
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_NoClassItfCABuilder);
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x0012CD20 File Offset: 0x0012BD20
		internal static TypeBuilder DefineUniqueType(string strInitFullName, TypeAttributes attrs, Type BaseType, Type[] aInterfaceTypes, ModuleBuilder mb)
		{
			string text = strInitFullName;
			int num = 2;
			while (mb.GetType(text) != null)
			{
				text = strInitFullName + "_" + num;
				num++;
			}
			return mb.DefineType(text, attrs, BaseType, aInterfaceTypes);
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x0012CD60 File Offset: 0x0012BD60
		internal static void SetHiddenAttribute(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_HiddenCABuilder == null)
			{
				Type[] types = new Type[]
				{
					typeof(TypeLibTypeFlags)
				};
				ConstructorInfo constructor = typeof(TypeLibTypeAttribute).GetConstructor(types);
				TCEAdapterGenerator.s_HiddenCABuilder = new CustomAttributeBuilder(constructor, new object[]
				{
					TypeLibTypeFlags.FHidden
				});
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_HiddenCABuilder);
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x0012CDC4 File Offset: 0x0012BDC4
		internal static MethodInfo[] GetNonPropertyMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			ArrayList arrayList = new ArrayList(methods);
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo methodInfo in accessors)
				{
					for (int k = 0; k < arrayList.Count; k++)
					{
						if ((MethodInfo)arrayList[k] == methodInfo)
						{
							arrayList.RemoveAt(k);
						}
					}
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x06005331 RID: 21297 RVA: 0x0012CE70 File Offset: 0x0012BE70
		internal static MethodInfo[] GetPropertyMethods(Type type)
		{
			type.GetMethods();
			ArrayList arrayList = new ArrayList();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo value in accessors)
				{
					arrayList.Add(value);
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x04002B0F RID: 11023
		private ModuleBuilder m_Module;

		// Token: 0x04002B10 RID: 11024
		private Hashtable m_SrcItfToSrcItfInfoMap = new Hashtable();

		// Token: 0x04002B11 RID: 11025
		private static CustomAttributeBuilder s_NoClassItfCABuilder;

		// Token: 0x04002B12 RID: 11026
		private static CustomAttributeBuilder s_HiddenCABuilder;
	}
}
