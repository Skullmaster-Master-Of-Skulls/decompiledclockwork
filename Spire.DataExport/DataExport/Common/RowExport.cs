using System;
using System.Collections;
using System.Globalization;
using Spire.DataExport.Delegates;

namespace Spire.DataExport.Common
{
	// Token: 0x02000161 RID: 353
	public class RowExport : IEnumerable
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x000599B4 File Offset: 0x000589B4
		public RowExport(ColumnsExport ColumnsExport, FormatsExport FormatsExport, CultureInfo Culture, GetExportFieldData GetExportFieldData)
		{
			this.ᜂ = ColumnsExport;
			this.ᜃ = FormatsExport;
			this.ᜄ = Culture;
			this.ᜅ = GetExportFieldData;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000599FC File Offset: 0x000589FC
		public IEnumerator GetEnumerator()
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
			return this.ᜀ.GetEnumerator();
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00059A44 File Offset: 0x00058A44
		public ColExport Add(string Name, int ColumnIndex)
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
			ColExport colExport = new ColExport(this);
			colExport.Name = Name.Trim();
			colExport.ColumnIndex = ColumnIndex;
			this.ᜀ.Add(colExport);
			this.ᜆ = true;
			return colExport;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00059AB0 File Offset: 0x00058AB0
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
			this.ᜀ.Clear();
			this.ᜆ = true;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00059B00 File Offset: 0x00058B00
		public void Delete(int Index)
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
			this.ᜀ.RemoveAt(Index);
			this.ᜆ = true;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00059B50 File Offset: 0x00058B50
		public ColExport First()
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
			return this[0];
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00059B94 File Offset: 0x00058B94
		public void Insert(int Index, ColExport Item)
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
			this.ᜀ.Insert(Index, Item);
			this.ᜆ = true;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00059BE4 File Offset: 0x00058BE4
		public void SetValue(string Name, string Value)
		{
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜆ)
						{
							if (true)
							{
							}
							num = 4;
							continue;
						}
						goto IL_98;
					case 1:
						if (this.ᜁ.ContainsKey(Name))
						{
							num = 2;
							continue;
						}
						return;
					case 2:
					{
						int index = (int)this.ᜁ[Name];
						this[index].Value = Value.Trim();
						num = 3;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42;
						default:
							goto IL_90;
						}
						break;
					case 4:
						goto IL_42;
					case 5:
						goto IL_98;
					}
					break;
					IL_42:
					this.ᜀ();
					num = 5;
					continue;
					IL_98:
					num = 1;
				}
			}
			IL_90:
			if (false)
			{
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00059CC4 File Offset: 0x00058CC4
		public void SetBinaryColumnValue(string Name, object dataSource)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.ᜁ.ContainsKey(Name))
					{
						num = 5;
						continue;
					}
					return;
				case 2:
					goto IL_8F;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C5;
					default:
						goto IL_87;
					}
					break;
				case 4:
					this.ᜀ();
					num = 2;
					continue;
				case 5:
					goto IL_C5;
				}
				if (this.ᜆ)
				{
					num = 4;
					continue;
				}
				IL_8F:
				if (true)
				{
				}
				num = 1;
				continue;
				IL_C5:
				int index = (int)this.ᜁ[Name];
				this[index].IsBinary = true;
				this[index].DataSource = dataSource;
				num = 3;
			}
			IL_87:
			if (false)
			{
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00059DB0 File Offset: 0x00058DB0
		internal void ᜀ(string A_0, object A_1)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ();
					num = 2;
					continue;
				case 1:
					if (this.ᜁ.ContainsKey(A_0))
					{
						num = 5;
						continue;
					}
					return;
				case 2:
					goto IL_87;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
						goto IL_7F;
					}
					break;
				case 5:
					goto IL_B5;
				}
				if (this.ᜆ)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				IL_87:
				num = 1;
				continue;
				IL_B5:
				int index = (int)this.ᜁ[A_0];
				this[index].OriginalValue = A_1;
				num = 4;
			}
			IL_7F:
			if (false)
			{
			}
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00059E88 File Offset: 0x00058E88
		public void ClearValues()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				IEnumerator enumerator = this.GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							ColExport colExport = (ColExport)enumerator.Current;
							colExport.Value = string.Empty;
							num = 4;
							continue;
						}
						case 1:
							num = 2;
							continue;
						case 2:
							goto IL_98;
						}
						IL_76:
						num = 0;
						continue;
						goto IL_76;
					}
					IL_98:;
				}
				finally
				{
					if (true)
					{
					}
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable.Dispose();
								num = 2;
								continue;
							case 1:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_E2;
							case 2:
								goto IL_E0;
							}
							break;
						}
					}
					IL_E0:
					IL_E2:;
				}
				break;
			}
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00059F88 File Offset: 0x00058F88
		public ColExport Last()
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
			return this[this.ᜀ.Count - 1];
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00059FD8 File Offset: 0x00058FD8
		public int IndexOf(ColExport Item)
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
			return this.ᜀ.IndexOf(Item);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0005A020 File Offset: 0x00059020
		public int Remove(ColExport Item)
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
			int result = this.IndexOf(Item);
			this.ᜀ.Remove(Item);
			this.ᜆ = true;
			return result;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0005A078 File Offset: 0x00059078
		public ColExport ColByName(string Name)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (!this.ᜁ.ContainsKey(Name))
				{
					return null;
				}
				break;
			}
			int index = (int)this.ᜁ[Name];
			return this[index];
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0005A0E0 File Offset: 0x000590E0
		internal string ᜀ(ColExport A_0)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (this.ᜅ != null)
				{
					return this.ᜅ(A_0);
				}
				break;
			}
			return string.Empty;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0005A138 File Offset: 0x00059138
		public Hashtable Index
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_52;
					case 1:
						goto IL_6A;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (this.ᜆ)
					{
						num = 0;
						continue;
					}
					break;
					IL_52:
					this.ᜀ();
					if (true)
					{
					}
					num = 1;
				}
				IL_6A:
				return this.ᜁ;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0005A1B8 File Offset: 0x000591B8
		public ColumnsExport ColumnsExport
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0005A1FC File Offset: 0x000591FC
		public FormatsExport FormatsExport
		{
			get
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
				return this.ᜃ;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0005A240 File Offset: 0x00059240
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x0005A284 File Offset: 0x00059284
		public CultureInfo Culture
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.ᜄ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (value != this.ᜄ)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						goto IL_4D;
					case 3:
						return;
					case 4:
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_4D:
						this.ᜄ = value;
						if (true)
						{
						}
						num = 3;
						break;
					default:
						if (false)
						{
						}
						if (value == null)
						{
							return;
						}
						num = 4;
						break;
					}
				}
			}
		}

		// Token: 0x17000033 RID: 51
		public ColExport this[int Index]
		{
			get
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
				return this.ᜀ[Index] as ColExport;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ[Index] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0005A3B0 File Offset: 0x000593B0
		public int Count
		{
			get
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
				return this.ᜀ.Count;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0005A3F8 File Offset: 0x000593F8
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0005A43C File Offset: 0x0005943C
		public GetExportFieldData GetExportFieldData
		{
			get
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
				return this.ᜅ;
			}
			set
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
				this.ᜅ = value;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0005A480 File Offset: 0x00059480
		private void ᜀ()
		{
			for (;;)
			{
				this.ᜁ.Clear();
				int num = 0;
				if (true)
				{
				}
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_37;
					case 1:
					{
						if (num >= this.ᜀ.Count)
						{
							num2 = 3;
							continue;
						}
						ColExport colExport = this.ᜀ[num] as ColExport;
						this.ᜁ[colExport.Name] = num;
						num++;
						num2 = 2;
						continue;
					}
					case 2:
						IL_B5:
						goto IL_37;
					case 3:
						goto IL_7B;
					}
					break;
					IL_37:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
						if (false)
						{
						}
						num2 = 1;
						break;
					}
				}
			}
			IL_7B:
			this.ᜆ = false;
		}

		// Token: 0x040006E1 RID: 1761
		private ArrayList ᜀ = new ArrayList();

		// Token: 0x040006E2 RID: 1762
		private Hashtable ᜁ = new Hashtable();

		// Token: 0x040006E3 RID: 1763
		private long \u25D9\u009C\u008E\u00A1;

		// Token: 0x040006E4 RID: 1764
		private ColumnsExport ᜂ;

		// Token: 0x040006E5 RID: 1765
		private float \u25D9\u00A5\u0080\u0089;

		// Token: 0x040006E6 RID: 1766
		private FormatsExport ᜃ;

		// Token: 0x040006E7 RID: 1767
		private CultureInfo ᜄ;

		// Token: 0x040006E8 RID: 1768
		private GetExportFieldData ᜅ;

		// Token: 0x040006E9 RID: 1769
		private bool ᜆ;
	}
}
