using System;
using System.Collections;
using System.Collections.Generic;

namespace Spire.Xls.Core.Parser.Biff_Records
{
	// Token: 0x020005E7 RID: 1511
	[CLSCompliant(false)]
	public class RecordArrayList : IList, IList<IRecordStorage>
	{
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x0600599D RID: 22941 RVA: 0x003844DC File Offset: 0x003834DC
		public bool IsFixedSize
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
				return ((IList)this.ᜀ).IsFixedSize;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600599E RID: 22942 RVA: 0x00384524 File Offset: 0x00383524
		public bool IsReadOnly
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
				return ((IList)this.ᜀ).IsReadOnly;
			}
		}

		// Token: 0x17000DEA RID: 3562
		public IRecordStorage this[int index]
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
				return this.ᜀ[index];
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
				this.ᜀ[index] = value;
			}
		}

		// Token: 0x17000DEB RID: 3563
		object IList.this[int index]
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
				return this.ᜀ[index];
			}
			set
			{
				for (;;)
				{
					IL_14:
					IRecordStorage recordStorage = value as IRecordStorage;
					if (true)
					{
					}
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (recordStorage != null)
							{
								num = 2;
								continue;
							}
							goto IL_59;
						case 1:
							goto IL_57;
						case 2:
							this.ᜀ[index] = recordStorage;
							num = 1;
							continue;
						}
						goto IL_14;
					}
					IL_59:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_6F;
					}
					IL_57:
					goto IL_59;
				}
				IL_6F:
				if (false)
				{
				}
			}
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x003846C8 File Offset: 0x003836C8
		public void RemoveAt(int index)
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
			this.ᜀ.RemoveAt(index);
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x00384710 File Offset: 0x00383710
		public void Insert(int index, IRecordStorage value)
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
			this.ᜀ.Insert(index, value);
		}

		// Token: 0x060059A5 RID: 22949 RVA: 0x00384758 File Offset: 0x00383758
		public bool Remove(IRecordStorage value)
		{
			int num;
			for (;;)
			{
				IL_14:
				if (true)
				{
				}
				num = this.ᜀ.IndexOf(value);
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_5F;
					case 1:
						goto IL_5D;
					case 2:
						this.ᜀ.RemoveAt(num);
						num2 = 1;
						continue;
					}
					goto IL_14;
				}
				IL_5F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_75;
				}
				IL_5D:
				goto IL_5F;
			}
			IL_75:
			if (false)
			{
			}
			return num >= 0;
		}

		// Token: 0x060059A6 RID: 22950 RVA: 0x003847E8 File Offset: 0x003837E8
		public bool Contains(IRecordStorage value)
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
			return this.ᜀ.Contains(value);
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x00384830 File Offset: 0x00383830
		public void Clear()
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
			this.ᜀ.Clear();
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x00384878 File Offset: 0x00383878
		public int IndexOf(IRecordStorage value)
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
			return this.ᜀ.IndexOf(value);
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x003848C0 File Offset: 0x003838C0
		internal int ᜀ(IRecordStorage A_0)
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
			this.ᜀ.Add(A_0);
			return this.ᜀ.Count - 1;
		}

		// Token: 0x060059AA RID: 22954 RVA: 0x00384914 File Offset: 0x00383914
		void ICollection<IRecordStorage>.Add(IRecordStorage value)
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
			this.ᜀ.Add(value);
		}

		// Token: 0x060059AB RID: 22955 RVA: 0x0038495C File Offset: 0x0038395C
		public void AddList(IList value)
		{
			for (;;)
			{
				IL_34:
				int num = 0;
				int count = value.Count;
				if (true)
				{
				}
				int num2 = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_4F;
						case 1:
							return;
						case 2:
							goto IL_4F;
						case 3:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IRecordStorage a_ = value[num] as IRecordStorage;
							this.ᜀ(a_);
							num++;
							num2 = 2;
							continue;
						}
						}
						goto IL_34;
						IL_4F:
						num2 = 3;
						break;
					}
				}
			}
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x00384A00 File Offset: 0x00383A00
		public void AddRange(ICollection value)
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
			{
				if (false)
				{
				}
				IEnumerator enumerator = value.GetEnumerator();
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 1:
							num = 3;
							continue;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							IRecordStorage a_ = (IRecordStorage)enumerator.Current;
							this.ᜀ(a_);
							num = 0;
							continue;
						}
						case 3:
							goto IL_9D;
						}
						IL_7B:
						num = 2;
						continue;
						goto IL_7B;
					}
					IL_9D:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable != null)
								{
									num = 1;
									continue;
								}
								goto IL_DF;
							case 1:
								disposable.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_DD;
							}
							break;
						}
					}
					IL_DD:
					IL_DF:;
				}
				break;
			}
			}
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x00384B00 File Offset: 0x00383B00
		internal void ᜀ(ICollection<IRecordStorage> A_0)
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
			this.ᜀ.AddRange(A_0);
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x00384B48 File Offset: 0x00383B48
		public void Insert(int index, object value)
		{
			for (;;)
			{
				IL_30:
				if (true)
				{
				}
				BiffRecordRaw biffRecordRaw = value as BiffRecordRaw;
				for (;;)
				{
					IL_3F:
					int num = 1;
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3F;
						default:
							if (false)
							{
							}
							switch (num)
							{
							case 0:
								return;
							case 1:
								if (biffRecordRaw != null)
								{
									num = 2;
									continue;
								}
								return;
							case 2:
								this.Insert(index, biffRecordRaw);
								num = 0;
								continue;
							}
							goto IL_30;
						}
					}
				}
			}
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x00384BC8 File Offset: 0x00383BC8
		public void Remove(object value)
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
			BiffRecordRaw value2 = value as BiffRecordRaw;
			this.Remove(value2);
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x00384C14 File Offset: 0x00383C14
		public bool Contains(object value)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				BiffRecordRaw biffRecordRaw = value as BiffRecordRaw;
				if (biffRecordRaw != null)
				{
					return this.Contains(biffRecordRaw);
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x00384C64 File Offset: 0x00383C64
		public int IndexOf(object value)
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
				BiffRecordRaw biffRecordRaw = value as BiffRecordRaw;
				if (biffRecordRaw != null)
				{
					return this.IndexOf(biffRecordRaw);
				}
				if (true)
				{
				}
				break;
			}
			}
			return -1;
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x00384CB4 File Offset: 0x00383CB4
		public int Add(object value)
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
				BiffRecordRaw biffRecordRaw = value as BiffRecordRaw;
				if (biffRecordRaw != null)
				{
					return this.ᜀ(biffRecordRaw);
				}
				break;
			}
			}
			if (true)
			{
			}
			return -1;
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060059B3 RID: 22963 RVA: 0x00384D04 File Offset: 0x00383D04
		public bool IsSynchronized
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
				return ((ICollection)this.ᜀ).IsSynchronized;
			}
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060059B4 RID: 22964 RVA: 0x00384D4C File Offset: 0x00383D4C
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

		// Token: 0x060059B5 RID: 22965 RVA: 0x00384D94 File Offset: 0x00383D94
		public void CopyTo(Array array, int index)
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
			((ICollection)this.ᜀ).CopyTo(array, index);
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x060059B6 RID: 22966 RVA: 0x00384DDC File Offset: 0x00383DDC
		public object SyncRoot
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
				return ((ICollection)this.ᜀ).SyncRoot;
			}
		}

		// Token: 0x060059B7 RID: 22967 RVA: 0x00384E24 File Offset: 0x00383E24
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

		// Token: 0x060059B8 RID: 22968 RVA: 0x00384E70 File Offset: 0x00383E70
		IEnumerator<IRecordStorage> IEnumerable<IRecordStorage>.GetEnumerator()
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

		// Token: 0x060059B9 RID: 22969 RVA: 0x00384EBC File Offset: 0x00383EBC
		public void UpdateBiffRecordsOffsets()
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
			this.CalculateRecordsStreamPos();
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x00384F00 File Offset: 0x00383F00
		protected void CalculateRecordsStreamPos()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int num2 = 0;
					int count = this.ᜀ.Count;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_4A;
						case 1:
							goto IL_4A;
						case 2:
						{
							if (num2 >= count)
							{
								num3 = 3;
								continue;
							}
							IRecordStorage recordStorage = this.ᜀ[num2];
							recordStorage.StreamPos = (long)num;
							num += 4 + recordStorage.GetStoreSize(ExcelVersion.Version97to2003);
							num2++;
							num3 = 1;
							continue;
						}
						case 3:
							return;
						}
						break;
						IL_4A:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num3 = 2;
							break;
						}
					}
				}
				return;
			}
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x00384FC8 File Offset: 0x00383FC8
		public void CopyTo(IRecordStorage[] array, int arrayIndex)
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
			this.ᜀ.CopyTo(array, arrayIndex);
		}

		// Token: 0x04002AE5 RID: 10981
		private int \u2593\u00AB\u0099\u0089;

		// Token: 0x04002AE6 RID: 10982
		private string \u2460\u00A6\u0098\u00A4;

		// Token: 0x04002AE7 RID: 10983
		private string \u2609\u009D\u00AE\u0092;

		// Token: 0x04002AE8 RID: 10984
		private List<IRecordStorage> ᜀ = new List<IRecordStorage>();
	}
}
