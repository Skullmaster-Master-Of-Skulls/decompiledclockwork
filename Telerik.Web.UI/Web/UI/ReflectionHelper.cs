using System;
using System.Linq;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x020001C3 RID: 451
	internal static class ReflectionHelper
	{
		// Token: 0x06001081 RID: 4225 RVA: 0x0003C404 File Offset: 0x0003A604
		public static Assembly GetAssembly(string assemblyName)
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assembly assembly = assemblies.FirstOrDefault((Assembly a) => a.ManifestModule.Name == assemblyName);
			if (assembly == null)
			{
				assembly = Assembly.Load(assemblyName);
			}
			return assembly;
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0003C454 File Offset: 0x0003A654
		public static object GetProperty(object target, string propertyName)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName);
			return property.GetValue(target, null);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0003C478 File Offset: 0x0003A678
		public static void SetProperty(object target, string propertyName, object propertyValue)
		{
			PropertyInfo property = target.GetType().GetProperty(propertyName);
			if (property.PropertyType.IsEnum)
			{
				object value = Enum.Parse(property.PropertyType, propertyValue.ToString(), true);
				property.SetValue(target, Enum.ToObject(property.PropertyType, Convert.ToUInt64(value)), null);
				return;
			}
			property.SetValue(target, propertyValue, null);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003C4D8 File Offset: 0x0003A6D8
		public static object InvokeMethod(object target, string methodName, object[] parameters = null)
		{
			if (parameters == null)
			{
				parameters = new object[0];
			}
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].GetType();
			}
			return target.GetType().GetMethod(methodName, array).Invoke(target, parameters);
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003C526 File Offset: 0x0003A726
		public static object CreateInstance(Assembly assembly, string type, object[] constructorParameters = null)
		{
			if (constructorParameters != null)
			{
				return Activator.CreateInstance(assembly.GetType(type), constructorParameters);
			}
			return Activator.CreateInstance(assembly.GetType(type));
		}
	}
}
