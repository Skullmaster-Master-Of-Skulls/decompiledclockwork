using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004EC RID: 1260
internal sealed class spr\u1A57
{
	// Token: 0x06004D3D RID: 19773 RVA: 0x002F1A48 File Offset: 0x002F0A48
	private spr\u1A57()
	{
	}

	// Token: 0x06004D3E RID: 19774 RVA: 0x002F1A5C File Offset: 0x002F0A5C
	public static int ᜀ(object A_0, string[] A_1)
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
		spr\u1A57.ᜀ = 0;
		StringBuilder stringBuilder = new StringBuilder(8192);
		List<string> list = new List<string>(A_1);
		list.Sort();
		spr\u1A57.ᜀ(stringBuilder, A_0, list);
		return stringBuilder.ToString().GetHashCode();
	}

	// Token: 0x06004D3F RID: 19775 RVA: 0x002F1AC8 File Offset: 0x002F0AC8
	private static void ᜀ(StringBuilder A_0, object A_1, List<string> A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u1A57.ᜀ++;
				Type type = A_1.GetType();
				int num = 5;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						try
						{
							for (;;)
							{
								PropertyInfo[] properties;
								object value = properties[num2].GetValue(A_1, new object[0]);
								num = 15;
								for (;;)
								{
									switch (num)
									{
									case 0:
										A_0.Append(value.ToString());
										num = 7;
										continue;
									case 1:
										goto IL_322;
									case 2:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_322;
										default:
											if (false)
											{
											}
											if (value is ICollection)
											{
												num = 13;
												continue;
											}
											num = 8;
											continue;
										}
										break;
									case 3:
									{
										Type type2 = value.GetType();
										num = 16;
										continue;
									}
									case 4:
										goto IL_1BB;
									case 5:
										spr\u1A57.ᜀ(A_0, value, A_2);
										num = 1;
										continue;
									case 6:
										goto IL_33F;
									case 7:
										goto IL_33F;
									case 8:
									{
										Type type2;
										if (!type2.IsPrimitive)
										{
											num = 5;
											continue;
										}
										A_0.Append(value.ToString());
										num = 9;
										continue;
									}
									case 9:
										goto IL_33F;
									case 10:
										num = 6;
										continue;
									case 11:
										goto IL_34B;
									case 12:
										goto IL_1BB;
									case 13:
									{
										IEnumerator enumerator = ((ICollection)value).GetEnumerator();
										enumerator.Reset();
										int num3 = 0;
										num = 4;
										continue;
									}
									case 14:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 10;
											continue;
										}
										spr\u1A57.ᜀ(A_0, enumerator.Current, A_2);
										int num3;
										num3++;
										num = 12;
										continue;
									}
									case 15:
										if (value != null)
										{
											num = 3;
											continue;
										}
										A_0.Append(RecordTableEnumerator.b("❈㹊⅌⍎", a_));
										num = 17;
										continue;
									case 16:
										if (value is string)
										{
											num = 0;
											continue;
										}
										num = 2;
										continue;
									case 17:
										goto IL_33F;
									}
									break;
									IL_1BB:
									num = 14;
									continue;
									IL_33F:
									num = 11;
									continue;
									IL_322:
									goto IL_33F;
								}
							}
							IL_34B:
							goto IL_381;
						}
						catch (Exception)
						{
							goto IL_381;
						}
						goto IL_350;
					case 1:
					{
						PropertyInfo[] properties;
						if (properties.Length > 0)
						{
							num = 6;
							continue;
						}
						A_0.Append(A_1.ToString());
						num = 3;
						continue;
					}
					case 2:
						goto IL_8E;
					case 3:
						goto IL_E2;
					case 4:
						num = 9;
						continue;
					case 5:
						if (!type.IsPrimitive)
						{
							num = 11;
							continue;
						}
						A_0.Append(A_1.ToString());
						num = 8;
						continue;
					case 6:
						if (true)
						{
						}
						num2 = 0;
						num = 13;
						continue;
					case 7:
						num = 12;
						continue;
					case 8:
						goto IL_13B;
					case 9:
					{
						PropertyInfo[] properties;
						if (A_2.BinarySearch(properties[num2].Name) < 0)
						{
							num = 0;
							continue;
						}
						goto IL_381;
					}
					case 10:
					{
						PropertyInfo[] properties;
						if (num2 >= properties.Length)
						{
							num = 7;
							continue;
						}
						goto IL_350;
					}
					case 11:
					{
						PropertyInfo[] properties = type.GetProperties();
						num = 1;
						continue;
					}
					case 12:
						goto IL_11D;
					case 13:
						goto IL_8E;
					case 14:
					{
						PropertyInfo[] properties;
						if (properties[num2].CanRead)
						{
							num = 4;
							continue;
						}
						goto IL_381;
					}
					}
					break;
					IL_8E:
					num = 10;
					continue;
					IL_350:
					num = 14;
					continue;
					IL_381:
					num2++;
					num = 2;
				}
			}
			IL_E2:
			IL_11D:
			IL_13B:
			spr\u1A57.ᜀ--;
			return;
		}
	}

	// Token: 0x06004D40 RID: 19776 RVA: 0x002F1EC4 File Offset: 0x002F0EC4
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1A57()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x04002320 RID: 8992
	private static int ᜀ;
}
