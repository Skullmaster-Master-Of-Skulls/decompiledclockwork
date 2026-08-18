using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Spire.Doc.Reporting
{
	// Token: 0x0200010C RID: 268
	public class MailMergeDataTable
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x000568B8 File Offset: 0x000558B8
		public string GroupName
		{
			get
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x000568FC File Offset: 0x000558FC
		public IEnumerator SourceData
		{
			get
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
				return this.ᜁ;
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00056940 File Offset: 0x00055940
		public MailMergeDataTable(string groupName, IEnumerable enumerable)
		{
			this.ᜀ = groupName;
			this.ᜁ = enumerable.GetEnumerator();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00056968 File Offset: 0x00055968
		internal MailMergeDataTable(string A_0, IEnumerator A_1)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0005698C File Offset: 0x0005598C
		internal MailMergeDataTable ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				MailMergeDataTable result;
				for (;;)
				{
					string[] array = A_0.Split(new char[]
					{
						' '
					});
					string name = array[0];
					string a = array[2];
					result = null;
					List<object> list = new List<object>();
					this.ᜁ.Reset();
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							result = new MailMergeDataTable(this.GroupName, list.GetEnumerator());
							if (true)
							{
							}
							num = 4;
							continue;
						case 1:
							goto IL_A0;
						case 2:
							num = 5;
							continue;
						case 3:
							goto IL_A0;
						case 4:
							return result;
						case 5:
							if (list.Count > 0)
							{
								num = 0;
								continue;
							}
							return result;
						case 6:
						{
							if (!this.ᜁ.MoveNext())
							{
								num = 2;
								continue;
							}
							PropertyInfo property = this.ᜁ.Current.GetType().GetProperty(name);
							object value = property.GetValue(this.ᜁ.Current, null);
							num = 7;
							continue;
						}
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_186;
							default:
							{
								if (false)
								{
								}
								object value;
								if (a == value.ToString())
								{
									num = 8;
									continue;
								}
								goto IL_A0;
							}
							}
							break;
						case 8:
							list.Add(this.ᜁ.Current);
							goto IL_186;
						}
						break;
						IL_A0:
						num = 6;
						continue;
						IL_186:
						num = 1;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x04000E31 RID: 3633
		private bool \u25D9\u00AE\u0095\u0091;

		// Token: 0x04000E32 RID: 3634
		private float[] \u25D9\u008D\u0085\u00A2;

		// Token: 0x04000E33 RID: 3635
		private string ᜀ;

		// Token: 0x04000E34 RID: 3636
		private byte[] \u2460\u0094\u00A2\u0088;

		// Token: 0x04000E35 RID: 3637
		private long[] \u25D8\u0086\u008F\u00A7;

		// Token: 0x04000E36 RID: 3638
		private int[] \u25D8\u0088\u00AB\u008C;

		// Token: 0x04000E37 RID: 3639
		private string[] \u25D9\u009E\u0095\u00A3;

		// Token: 0x04000E38 RID: 3640
		private IEnumerator ᜁ;
	}
}
