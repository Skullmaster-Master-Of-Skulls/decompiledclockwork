using System;
using System.Reflection;

// Token: 0x0200000B RID: 11
internal static class spr\u20BD
{
	// Token: 0x06000041 RID: 65 RVA: 0x000036E0 File Offset: 0x000018E0
	internal static void ᜁ<ᜀ, ᜁ>(ᜀ A_0, ᜁ A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u20BD.ᜀ<ᜀ, ᜁ>(A_0, A_1, true);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003724 File Offset: 0x00001924
	internal static void ᜀ<ᜀ, ᜁ>(ᜀ A_0, ᜁ A_1)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u20BD.ᜀ<ᜀ, ᜁ>(A_0, A_1, false);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003768 File Offset: 0x00001968
	private static void ᜀ<ᜀ, ᜁ>(ᜀ A_0, ᜁ A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int num2;
				PropertyInfo propertyInfo;
				string name;
				object obj;
				spr\u2454[] array2;
				Type typeFromHandle3;
				string text;
				switch (num)
				{
				case 0:
					goto IL_25F;
				case 1:
					goto IL_3D1;
				case 2:
				{
					PropertyInfo[] array;
					if (num2 >= array.Length)
					{
						num = 32;
						continue;
					}
					propertyInfo = array[num2];
					object value = propertyInfo.GetValue(A_0, null);
					num = 21;
					continue;
				}
				case 3:
					num = 22;
					continue;
				case 4:
				{
					spr\u2454 spr_u;
					if (spr_u.ᜀ() != null)
					{
						num = 16;
						continue;
					}
					num = 30;
					continue;
				}
				case 5:
				{
					PropertyInfo property;
					if (property != null)
					{
						num = 20;
						continue;
					}
					goto IL_19F;
				}
				case 6:
					goto IL_1C0;
				case 7:
				{
					Type typeFromHandle;
					PropertyInfo property = typeFromHandle.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					num = 15;
					continue;
				}
				case 9:
					num = 23;
					continue;
				case 10:
					goto IL_19F;
				case 11:
				{
					object value;
					if (value == null)
					{
						num = 17;
						continue;
					}
					goto IL_1C0;
				}
				case 12:
					goto IL_3EE;
				case 13:
					if (!spr\u17C5.ᜀ(ref name))
					{
						num = 7;
						continue;
					}
					obj = A_1;
					num = 10;
					continue;
				case 14:
					if (array2.Length > 0)
					{
						num = 27;
						continue;
					}
					goto IL_1C0;
				case 15:
				{
					PropertyInfo property;
					if (property == null)
					{
						num = 26;
						continue;
					}
					goto IL_3EE;
				}
				case 16:
					num = 24;
					continue;
				case 17:
					goto IL_207;
				case 18:
					goto IL_25F;
				case 19:
					if (obj != null)
					{
						num = 9;
						continue;
					}
					goto IL_1C0;
				case 20:
				{
					PropertyInfo property;
					obj = property.GetValue(A_1, null);
					if (true)
					{
					}
					num = 29;
					continue;
				}
				case 21:
					if (!A_2)
					{
						num = 28;
						continue;
					}
					goto IL_207;
				case 22:
				{
					IL_2AB:
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					Type typeFromHandle2 = typeof(ᜀ);
					Type typeFromHandle = typeof(ᜁ);
					typeFromHandle3 = typeof(spr\u2454);
					PropertyInfo[] properties = typeFromHandle2.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					PropertyInfo[] array = properties;
					num2 = 0;
					num = 18;
					continue;
				}
				case 23:
				{
					spr\u2454 spr_u;
					if (!string.IsNullOrEmpty(spr_u.ᜁ()))
					{
						num = 31;
						continue;
					}
					goto IL_23E;
				}
				case 24:
				{
					spr\u2454 spr_u;
					text = spr_u.ᜀ();
					goto IL_3A3;
				}
				case 25:
					goto IL_23E;
				case 26:
				{
					Type typeFromHandle;
					PropertyInfo property = typeFromHandle.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					num = 12;
					continue;
				}
				case 27:
				{
					spr\u2454 spr_u = array2[0];
					num = 4;
					continue;
				}
				case 28:
					num = 11;
					continue;
				case 29:
					goto IL_19F;
				case 30:
					text = propertyInfo.Name;
					goto IL_3A3;
				case 31:
				{
					spr\u2454 spr_u;
					Type typeFromHandle2;
					MethodInfo method = typeFromHandle2.GetMethod(spr_u.ᜁ(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					obj = method.Invoke(A_0, new object[]
					{
						obj
					});
					num = 25;
					continue;
				}
				case 32:
					return;
				}
				if (A_0 != null)
				{
					num = 3;
					continue;
				}
				IL_3D1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2AB;
				default:
					goto IL_3E7;
				}
				IL_19F:
				num = 19;
				continue;
				IL_1C0:
				num2++;
				num = 0;
				continue;
				IL_207:
				array2 = (propertyInfo.GetCustomAttributes(typeFromHandle3, true) as spr\u2454[]);
				num = 14;
				continue;
				IL_23E:
				propertyInfo.SetValue(A_0, obj, null);
				num = 6;
				continue;
				IL_25F:
				num = 2;
				continue;
				IL_3A3:
				name = text;
				obj = null;
				num = 13;
				continue;
				IL_3EE:
				num = 5;
			}
			return;
			IL_3E7:
			if (false)
			{
			}
			return;
		}
		}
	}
}
