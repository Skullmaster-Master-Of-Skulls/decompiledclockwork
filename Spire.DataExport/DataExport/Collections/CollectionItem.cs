using System;
using Spire.DataExport.Common;

namespace Spire.DataExport.Collections
{
	// Token: 0x020001A8 RID: 424
	public abstract class CollectionItem : DisposabledObject
	{
		// Token: 0x06000BA0 RID: 2976
		internal abstract void InitCollectionItem();

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0007A6FC File Offset: 0x000796FC
		protected virtual void SetName(string Name)
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
			this.Name = Name;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0007A740 File Offset: 0x00079740
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0007A784 File Offset: 0x00079784
		internal Collection Collection
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
				this.ᜀ = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0007A7C8 File Offset: 0x000797C8
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0007A80C File Offset: 0x0007980C
		internal string Name
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
				return this.ᜁ;
			}
			set
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
				this.ᜁ = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0007A850 File Offset: 0x00079850
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0007A8A4 File Offset: 0x000798A4
		internal int Index
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (this.ᜀ != null)
					{
						return this.ᜀ.IndexOf(this);
					}
					break;
				}
				return -1;
			}
			set
			{
				for (;;)
				{
					int num = this.Index;
					int num2 = 7;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							num2 = 1;
							continue;
						case 1:
							if (value < this.ᜀ.Count)
							{
								num2 = 6;
								continue;
							}
							return;
						case 2:
							if (value >= 0)
							{
								num2 = 0;
								continue;
							}
							return;
						case 3:
							if (value != num)
							{
								num2 = 8;
								continue;
							}
							return;
						case 4:
							return;
						case 5:
							num2 = 3;
							continue;
						case 6:
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
								Collection collection = this.ᜀ;
								CollectionItem item = collection[num];
								collection.RemoveAt(num);
								collection.Insert(value, item);
								num2 = 4;
								continue;
							}
							}
							break;
						case 7:
							if (num > -1)
							{
								num2 = 5;
								continue;
							}
							return;
						case 8:
							num2 = 2;
							continue;
						}
						break;
					}
				}
			}
		}

		// Token: 0x040008EF RID: 2287
		private Collection ᜀ;

		// Token: 0x040008F0 RID: 2288
		private long[] \u25D9\u009B\u00A2\u009B;

		// Token: 0x040008F1 RID: 2289
		private long[] \u2593\u00A3\u00AF\u0087;

		// Token: 0x040008F2 RID: 2290
		private long[] \u25D9\u00B0\u00A3\u00A5;

		// Token: 0x040008F3 RID: 2291
		private string ᜁ = string.Empty;
	}
}
