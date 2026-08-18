using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Spire.CompoundFile.Doc;

namespace Spire.Doc.Reporting
{
	// Token: 0x020000FE RID: 254
	public class MailMergeDataSet
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x000496F0 File Offset: 0x000486F0
		public List<object> DataSet
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
				return this.ᜂ;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00049734 File Offset: 0x00048734
		public MailMergeDataSet()
		{
			int a_ = 5;
			this.ᜀ = ClipboardData.b("ⱪὬnѰͲ㭴ᙶᑸṺ", a_);
			this.ᜁ = ClipboardData.b("㡪ɬᩮͰၲၴ㍶ᡸེᱼ", a_);
			base..ctor();
			this.ᜂ = new List<object>();
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00049784 File Offset: 0x00048784
		public void Add(object dataTable)
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
			this.ᜂ.Add(dataTable);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000497CC File Offset: 0x000487CC
		public void Clear()
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
			this.ᜂ.Clear();
			this.ᜂ = null;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0004981C File Offset: 0x0004881C
		internal MailMergeDataTable ᜁ(string A_0)
		{
			switch (0)
			{
			default:
			{
				MailMergeDataTable result;
				using (List<object>.Enumerator enumerator = this.ᜂ.GetEnumerator())
				{
					int num = 6;
					for (;;)
					{
						IEnumerator enumerator2;
						MailMergeDataTable mailMergeDataTable;
						switch (num)
						{
						case 0:
						{
							string text;
							mailMergeDataTable = new MailMergeDataTable(text, enumerator2);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_15B;
							default:
								if (false)
								{
								}
								num = 10;
								continue;
							}
							break;
						}
						case 1:
							num = 4;
							continue;
						case 2:
							goto IL_7D;
						case 3:
							goto IL_1A2;
						case 4:
						{
							string text;
							if (text == A_0)
							{
								num = 8;
								continue;
							}
							break;
						}
						case 5:
							goto IL_15B;
						case 7:
						{
							if (!enumerator.MoveNext())
							{
								num = 9;
								continue;
							}
							object obj = enumerator.Current;
							Type type = obj.GetType();
							PropertyInfo property = type.GetProperty(this.ᜀ);
							string text = property.GetValue(obj, null).ToString();
							num = 11;
							continue;
						}
						case 8:
						{
							Type type;
							PropertyInfo property2 = type.GetProperty(this.ᜁ);
							object obj;
							enumerator2 = (property2.GetValue(obj, null) as IEnumerator);
							mailMergeDataTable = null;
							num = 5;
							continue;
						}
						case 9:
							num = 3;
							continue;
						case 10:
							goto IL_70;
						case 11:
						{
							string text;
							if (!string.IsNullOrEmpty(text))
							{
								num = 1;
								continue;
							}
							break;
						}
						}
						goto IL_6B;
						IL_70:
						result = mailMergeDataTable;
						num = 2;
						continue;
						IL_108:
						num = 7;
						continue;
						IL_6B:
						goto IL_108;
						IL_15B:
						if (enumerator2 == null)
						{
							goto IL_70;
						}
						num = 0;
					}
					IL_7D:
					goto IL_1B5;
					IL_1A2:
					goto IL_1D;
				}
				goto IL_1B5;
				IL_1D:
				return null;
				IL_1B5:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00049A04 File Offset: 0x00048A04
		internal void ᜀ(string A_0)
		{
			switch (0)
			{
			default:
			{
				List<object>.Enumerator enumerator = this.ᜂ.GetEnumerator();
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							string text;
							if (!string.IsNullOrEmpty(text))
							{
								num = 8;
								continue;
							}
							break;
						}
						case 1:
							goto IL_11F;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 5;
								continue;
							}
							object obj = enumerator.Current;
							PropertyInfo property = obj.GetType().GetProperty(this.ᜀ);
							string text = property.GetValue(obj, null).ToString();
							num = 0;
							continue;
						}
						case 4:
							goto IL_12B;
						case 5:
							goto IL_11F;
						case 6:
						{
							IL_102:
							object obj;
							this.ᜂ.Remove(obj);
							num = 1;
							continue;
						}
						case 7:
						{
							string text;
							if (text == A_0)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 8:
							num = 7;
							continue;
						}
						IL_A2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_102;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						goto IL_A2;
						IL_11F:
						num = 4;
					}
					IL_12B:;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				return;
			}
			}
		}

		// Token: 0x04000DD1 RID: 3537
		private byte \u25D9\u0099\u0086\u0080;

		// Token: 0x04000DD2 RID: 3538
		private string ᜀ;

		// Token: 0x04000DD3 RID: 3539
		private long[] \u25D9\u008B\u00A2\u0093;

		// Token: 0x04000DD4 RID: 3540
		private bool \u25D8\u00A7\u0099\u00A1;

		// Token: 0x04000DD5 RID: 3541
		private int \u25D9\u009B\u0096\u009C;

		// Token: 0x04000DD6 RID: 3542
		private string ᜁ;

		// Token: 0x04000DD7 RID: 3543
		private List<object> ᜂ;
	}
}
