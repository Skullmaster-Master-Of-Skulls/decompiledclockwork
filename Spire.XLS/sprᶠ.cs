using System;
using System.Collections;
using System.Collections.Generic;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003DF RID: 991
internal class spr\u1DA0 : spr\u1AAC
{
	// Token: 0x06003BE1 RID: 15329 RVA: 0x00217024 File Offset: 0x00216024
	public spr\u1DA0(sprᝮ[] A_0, Dictionary<string, string> A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003BE2 RID: 15330 RVA: 0x0021703C File Offset: 0x0021603C
	public static spr\u1DA0 ᜀ(IWorkbook A_0)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			sprᝮ[] array;
			Dictionary<string, string> dictionary;
			for (;;)
			{
				array = new sprᝮ[A_0.Worksheets.Count];
				int num = 0;
				int num2 = 4;
				for (;;)
				{
					IEnumerator enumerator;
					switch (num2)
					{
					case 0:
						goto IL_16A;
					case 1:
						goto IL_1CD;
					case 2:
						if (num >= A_0.Worksheets.Count)
						{
							num2 = 0;
							continue;
						}
						array[num] = new sprᝮ(A_0.Worksheets[num]);
						array[num].ᜂ(A_0.Worksheets[num].Name);
						num++;
						num2 = 1;
						continue;
					case 3:
						for (;;)
						{
							try
							{
								num2 = 4;
								for (;;)
								{
									switch (num2)
									{
									case 1:
										goto IL_11C;
									case 2:
										num2 = 1;
										continue;
									case 3:
									{
										if (!enumerator.MoveNext())
										{
											num2 = 2;
											continue;
										}
										INamedRange namedRange = (INamedRange)enumerator.Current;
										dictionary.Add(namedRange.Scope + RecordTableEnumerator.b("筀", a_) + namedRange.Name, namedRange.Value.Replace(RecordTableEnumerator.b("晀", a_), ""));
										num2 = 0;
										continue;
									}
									}
									IL_91:
									num2 = 3;
									continue;
									goto IL_91;
								}
								IL_11C:
								goto IL_1F8;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_167;
										case 1:
											disposable.Dispose();
											num2 = 0;
											continue;
										case 2:
											if (disposable != null)
											{
												num2 = 1;
												continue;
											}
											goto IL_169;
										}
										break;
									}
								}
								IL_167:
								IL_169:;
							}
							goto IL_16A;
							IL_1F8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_20E;
							}
						}
						break;
					case 4:
						if (true)
						{
						}
						goto IL_1CD;
					}
					break;
					IL_16A:
					dictionary = new Dictionary<string, string>();
					enumerator = A_0.Names.GetEnumerator();
					num2 = 3;
					continue;
					IL_1CD:
					num2 = 2;
				}
			}
			IL_20E:
			if (false)
			{
			}
			return new spr\u1DA0(array, dictionary);
		}
		}
	}

	// Token: 0x04001A09 RID: 6665
	public new IWorkbook ᜀ;
}
