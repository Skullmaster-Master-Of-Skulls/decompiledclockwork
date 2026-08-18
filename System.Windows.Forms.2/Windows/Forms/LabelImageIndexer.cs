using System;

namespace System.Windows.Forms
{
	// Token: 0x020002BB RID: 699
	internal class LabelImageIndexer : ImageList.Indexer
	{
		// Token: 0x06002B17 RID: 11031 RVA: 0x000C223C File Offset: 0x000C043C
		public LabelImageIndexer(Label owner)
		{
			this.owner = owner;
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06002B18 RID: 11032 RVA: 0x000C2252 File Offset: 0x000C0452
		// (set) Token: 0x06002B19 RID: 11033 RVA: 0x000072B6 File Offset: 0x000054B6
		public override ImageList ImageList
		{
			get
			{
				if (this.owner != null)
				{
					return this.owner.ImageList;
				}
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000C2269 File Offset: 0x000C0469
		// (set) Token: 0x06002B1B RID: 11035 RVA: 0x000C2271 File Offset: 0x000C0471
		public override string Key
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
				this.useIntegerIndex = false;
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000C2281 File Offset: 0x000C0481
		// (set) Token: 0x06002B1D RID: 11037 RVA: 0x000C2289 File Offset: 0x000C0489
		public override int Index
		{
			get
			{
				return base.Index;
			}
			set
			{
				base.Index = value;
				this.useIntegerIndex = true;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002B1E RID: 11038 RVA: 0x000C229C File Offset: 0x000C049C
		public override int ActualIndex
		{
			get
			{
				if (this.useIntegerIndex)
				{
					if (this.Index >= this.ImageList.Images.Count)
					{
						return this.ImageList.Images.Count - 1;
					}
					return this.Index;
				}
				else
				{
					if (this.ImageList != null)
					{
						return this.ImageList.Images.IndexOfKey(this.Key);
					}
					return -1;
				}
			}
		}

		// Token: 0x04001226 RID: 4646
		private Label owner;

		// Token: 0x04001227 RID: 4647
		private bool useIntegerIndex = true;
	}
}
