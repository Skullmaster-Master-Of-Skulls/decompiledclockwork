using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x0200049D RID: 1181
	[SuppressUnmanagedCodeSecurity]
	internal class Com2IManagedPerPropertyBrowsingHandler : Com2ExtendedBrowsingHandler
	{
		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x00143944 File Offset: 0x00141B44
		public override Type Interface
		{
			get
			{
				return typeof(NativeMethods.IManagedPerPropertyBrowsing);
			}
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00143950 File Offset: 0x00141B50
		public override void SetupPropertyHandlers(Com2PropertyDescriptor[] propDesc)
		{
			if (propDesc == null)
			{
				return;
			}
			for (int i = 0; i < propDesc.Length; i++)
			{
				propDesc[i].QueryGetDynamicAttributes += this.OnGetAttributes;
			}
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00143984 File Offset: 0x00141B84
		private void OnGetAttributes(Com2PropertyDescriptor sender, GetAttributesEvent attrEvent)
		{
			object targetObject = sender.TargetObject;
			if (targetObject is NativeMethods.IManagedPerPropertyBrowsing)
			{
				Attribute[] componentAttributes = Com2IManagedPerPropertyBrowsingHandler.GetComponentAttributes((NativeMethods.IManagedPerPropertyBrowsing)targetObject, sender.DISPID);
				if (componentAttributes != null)
				{
					for (int i = 0; i < componentAttributes.Length; i++)
					{
						attrEvent.Add(componentAttributes[i]);
					}
				}
			}
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x001439CC File Offset: 0x00141BCC
		internal static Attribute[] GetComponentAttributes(NativeMethods.IManagedPerPropertyBrowsing target, int dispid)
		{
			int num = 0;
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			if (target.GetPropertyAttributes(dispid, ref num, ref zero, ref zero2) != 0 || num == 0)
			{
				return new Attribute[0];
			}
			ArrayList arrayList = new ArrayList();
			string[] stringsFromPtr = Com2IManagedPerPropertyBrowsingHandler.GetStringsFromPtr(zero, num);
			object[] variantsFromPtr = Com2IManagedPerPropertyBrowsingHandler.GetVariantsFromPtr(zero2, num);
			if (stringsFromPtr.Length != variantsFromPtr.Length)
			{
				return new Attribute[0];
			}
			Type[] array = new Type[stringsFromPtr.Length];
			int i = 0;
			while (i < stringsFromPtr.Length)
			{
				string text = stringsFromPtr[i];
				Type type = Type.GetType(text);
				Assembly assembly = null;
				if (type != null)
				{
					assembly = type.Assembly;
				}
				if (!(type == null))
				{
					goto IL_192;
				}
				string str = "";
				int num2 = text.LastIndexOf(',');
				if (num2 != -1)
				{
					str = text.Substring(num2);
					text = text.Substring(0, num2);
				}
				int num3 = text.LastIndexOf('.');
				if (num3 != -1)
				{
					string name = text.Substring(num3 + 1);
					if (assembly == null)
					{
						type = Type.GetType(text.Substring(0, num3) + str);
					}
					else
					{
						type = assembly.GetType(text.Substring(0, num3) + str);
					}
					if (!(type == null) && typeof(Attribute).IsAssignableFrom(type))
					{
						if (!(type != null))
						{
							goto IL_192;
						}
						FieldInfo field = type.GetField(name);
						if (!(field != null) || !field.IsStatic)
						{
							goto IL_192;
						}
						object value = field.GetValue(null);
						if (!(value is Attribute))
						{
							goto IL_192;
						}
						arrayList.Add(value);
					}
				}
				IL_252:
				i++;
				continue;
				IL_192:
				if (!typeof(Attribute).IsAssignableFrom(type))
				{
					goto IL_252;
				}
				if (!Convert.IsDBNull(variantsFromPtr[i]) && variantsFromPtr[i] != null)
				{
					ConstructorInfo[] constructors = type.GetConstructors();
					for (int j = 0; j < constructors.Length; j++)
					{
						ParameterInfo[] parameters = constructors[j].GetParameters();
						if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(variantsFromPtr[i].GetType()))
						{
							try
							{
								Attribute value2 = (Attribute)Activator.CreateInstance(type, new object[]
								{
									variantsFromPtr[i]
								});
								arrayList.Add(value2);
							}
							catch
							{
							}
						}
					}
					goto IL_252;
				}
				try
				{
					Attribute value2 = (Attribute)Activator.CreateInstance(type);
					arrayList.Add(value2);
				}
				catch
				{
				}
				goto IL_252;
			}
			Attribute[] array2 = new Attribute[arrayList.Count];
			arrayList.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00143C74 File Offset: 0x00141E74
		private static string[] GetStringsFromPtr(IntPtr ptr, int cStrings)
		{
			if (ptr != IntPtr.Zero)
			{
				string[] array = new string[cStrings];
				for (int i = 0; i < cStrings; i++)
				{
					try
					{
						IntPtr intPtr = Marshal.ReadIntPtr(ptr, i * 4);
						if (intPtr != IntPtr.Zero)
						{
							array[i] = Marshal.PtrToStringUni(intPtr);
							SafeNativeMethods.SysFreeString(new HandleRef(null, intPtr));
						}
						else
						{
							array[i] = "";
						}
					}
					catch (Exception ex)
					{
					}
				}
				try
				{
					Marshal.FreeCoTaskMem(ptr);
				}
				catch (Exception ex2)
				{
				}
				return array;
			}
			return new string[0];
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00143D10 File Offset: 0x00141F10
		private static object[] GetVariantsFromPtr(IntPtr ptr, int cVariants)
		{
			if (ptr != IntPtr.Zero)
			{
				object[] array = new object[cVariants];
				for (int i = 0; i < cVariants; i++)
				{
					try
					{
						IntPtr intPtr = (IntPtr)((long)ptr + (long)(i * 16));
						if (intPtr != IntPtr.Zero)
						{
							array[i] = Marshal.GetObjectForNativeVariant(intPtr);
							SafeNativeMethods.VariantClear(new HandleRef(null, intPtr));
						}
						else
						{
							array[i] = Convert.DBNull;
						}
					}
					catch (Exception ex)
					{
					}
				}
				try
				{
					Marshal.FreeCoTaskMem(ptr);
				}
				catch (Exception ex2)
				{
				}
				return array;
			}
			return new object[cVariants];
		}
	}
}
