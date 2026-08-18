using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace AutoMapper.Internal
{
	// Token: 0x020000B8 RID: 184
	public class ProxyGenerator : IProxyGenerator
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00014417 File Offset: 0x00012617
		private static ModuleBuilder CreateProxyModule()
		{
			AssemblyName assemblyName = new AssemblyName("AutoMapper.Proxies");
			assemblyName.SetPublicKey(ProxyGenerator.privateKey);
			assemblyName.SetPublicKeyToken(ProxyGenerator.privateKeyToken);
			return AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run).DefineDynamicModule("AutoMapper.Proxies.emit");
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001444C File Offset: 0x0001264C
		private static Type CreateProxyType(Type interfaceType)
		{
			if (!interfaceType.IsInterface())
			{
				throw new ArgumentException("Only interfaces can be proxied", "interfaceType");
			}
			string format = "Proxy<{0}>";
			string input;
			if ((input = interfaceType.AssemblyQualifiedName) == null)
			{
				input = (interfaceType.FullName ?? interfaceType.Name);
			}
			string name = string.Format(format, Regex.Replace(input, "[\\s,]+", "_"));
			List<Type> list = new List<Type>
			{
				interfaceType
			};
			list.AddRange(interfaceType.GetInterfaces());
			TypeBuilder typeBuilder = ProxyGenerator.proxyModule.DefineType(name, TypeAttributes.Public | TypeAttributes.Sealed, typeof(ProxyBase), list.ToArray());
			ILGenerator ilgenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes).GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, ProxyGenerator.proxyBase_ctor);
			ilgenerator.Emit(OpCodes.Ret);
			FieldBuilder fieldBuilder = null;
			if (typeof(INotifyPropertyChanged).IsAssignableFrom(interfaceType))
			{
				fieldBuilder = typeBuilder.DefineField("PropertyChanged", typeof(PropertyChangedEventHandler), FieldAttributes.Private);
				MethodBuilder methodBuilder = typeBuilder.DefineMethod("add_PropertyChanged", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.SpecialName, typeof(void), new Type[]
				{
					typeof(PropertyChangedEventHandler)
				});
				ILGenerator ilgenerator2 = methodBuilder.GetILGenerator();
				ilgenerator2.Emit(OpCodes.Ldarg_0);
				ilgenerator2.Emit(OpCodes.Dup);
				ilgenerator2.Emit(OpCodes.Ldfld, fieldBuilder);
				ilgenerator2.Emit(OpCodes.Ldarg_1);
				ilgenerator2.Emit(OpCodes.Call, ProxyGenerator.delegate_Combine);
				ilgenerator2.Emit(OpCodes.Castclass, typeof(PropertyChangedEventHandler));
				ilgenerator2.Emit(OpCodes.Stfld, fieldBuilder);
				ilgenerator2.Emit(OpCodes.Ret);
				MethodBuilder methodBuilder2 = typeBuilder.DefineMethod("remove_PropertyChanged", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask | MethodAttributes.SpecialName, typeof(void), new Type[]
				{
					typeof(PropertyChangedEventHandler)
				});
				ILGenerator ilgenerator3 = methodBuilder2.GetILGenerator();
				ilgenerator3.Emit(OpCodes.Ldarg_0);
				ilgenerator3.Emit(OpCodes.Dup);
				ilgenerator3.Emit(OpCodes.Ldfld, fieldBuilder);
				ilgenerator3.Emit(OpCodes.Ldarg_1);
				ilgenerator3.Emit(OpCodes.Call, ProxyGenerator.delegate_Remove);
				ilgenerator3.Emit(OpCodes.Castclass, typeof(PropertyChangedEventHandler));
				ilgenerator3.Emit(OpCodes.Stfld, fieldBuilder);
				ilgenerator3.Emit(OpCodes.Ret);
				typeBuilder.DefineMethodOverride(methodBuilder, ProxyGenerator.iNotifyPropertyChanged_PropertyChanged.GetAddMethod());
				typeBuilder.DefineMethodOverride(methodBuilder2, ProxyGenerator.iNotifyPropertyChanged_PropertyChanged.GetRemoveMethod());
			}
			List<PropertyInfo> list2 = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in (from intf in list
			where intf != typeof(INotifyPropertyChanged)
			select intf).SelectMany((Type intf) => intf.GetProperties()))
			{
				if (propertyInfo.CanWrite)
				{
					list2.Insert(0, propertyInfo);
				}
				else
				{
					list2.Add(propertyInfo);
				}
			}
			Dictionary<string, PropertyEmitter> dictionary = new Dictionary<string, PropertyEmitter>();
			foreach (PropertyInfo propertyInfo2 in list2)
			{
				PropertyEmitter propertyEmitter;
				if (dictionary.TryGetValue(propertyInfo2.Name, out propertyEmitter))
				{
					if (propertyEmitter.PropertyType != propertyInfo2.PropertyType && (propertyInfo2.CanWrite || !propertyInfo2.PropertyType.IsAssignableFrom(propertyEmitter.PropertyType)))
					{
						throw new ArgumentException(string.Format("The interface has a conflicting property {0}", propertyInfo2.Name), "interfaceType");
					}
				}
				else
				{
					dictionary.Add(propertyInfo2.Name, propertyEmitter = new PropertyEmitter(typeBuilder, propertyInfo2.Name, propertyInfo2.PropertyType, fieldBuilder));
				}
				if (propertyInfo2.CanRead)
				{
					typeBuilder.DefineMethodOverride(propertyEmitter.GetGetter(propertyInfo2.PropertyType), propertyInfo2.GetGetMethod());
				}
				if (propertyInfo2.CanWrite)
				{
					typeBuilder.DefineMethodOverride(propertyEmitter.GetSetter(propertyInfo2.PropertyType), propertyInfo2.GetSetMethod());
				}
			}
			return typeBuilder.CreateType();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00014868 File Offset: 0x00012A68
		public Type GetProxyType(Type interfaceType)
		{
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			Dictionary<Type, Type> obj = ProxyGenerator.proxyTypes;
			Type result;
			lock (obj)
			{
				Type type;
				if (!ProxyGenerator.proxyTypes.TryGetValue(interfaceType, out type))
				{
					ProxyGenerator.proxyTypes.Add(interfaceType, type = ProxyGenerator.CreateProxyType(interfaceType));
				}
				result = type;
			}
			return result;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000148DC File Offset: 0x00012ADC
		private static byte[] StringToByteArray(string hex)
		{
			int length = hex.Length;
			byte[] array = new byte[length / 2];
			for (int i = 0; i < length; i += 2)
			{
				array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
			}
			return array;
		}

		// Token: 0x040000FB RID: 251
		private static readonly byte[] privateKey = ProxyGenerator.StringToByteArray("002400000480000094000000060200000024000052534131000400000100010079dfef85ed6ba841717e154f13182c0a6029a40794a6ecd2886c7dc38825f6a4c05b0622723a01cd080f9879126708eef58f134accdc99627947425960ac2397162067507e3c627992aa6b92656ad3380999b30b5d5645ba46cc3fcc6a1de5de7afebcf896c65fb4f9547a6c0c6433045fceccb1fa15e960d519d0cd694b29a4");

		// Token: 0x040000FC RID: 252
		private static readonly byte[] privateKeyToken = ProxyGenerator.StringToByteArray("be96cd2c38ef1005");

		// Token: 0x040000FD RID: 253
		private static readonly MethodInfo delegate_Combine = typeof(Delegate).GetMethod("Combine", new Type[]
		{
			typeof(Delegate),
			typeof(Delegate)
		});

		// Token: 0x040000FE RID: 254
		private static readonly MethodInfo delegate_Remove = typeof(Delegate).GetMethod("Remove", new Type[]
		{
			typeof(Delegate),
			typeof(Delegate)
		});

		// Token: 0x040000FF RID: 255
		private static readonly EventInfo iNotifyPropertyChanged_PropertyChanged = typeof(INotifyPropertyChanged).GetEvent("PropertyChanged", BindingFlags.Instance | BindingFlags.Public);

		// Token: 0x04000100 RID: 256
		private static readonly ConstructorInfo proxyBase_ctor = typeof(ProxyBase).GetConstructor(Type.EmptyTypes);

		// Token: 0x04000101 RID: 257
		private static readonly ModuleBuilder proxyModule = ProxyGenerator.CreateProxyModule();

		// Token: 0x04000102 RID: 258
		private static readonly Dictionary<Type, Type> proxyTypes = new Dictionary<Type, Type>();
	}
}
