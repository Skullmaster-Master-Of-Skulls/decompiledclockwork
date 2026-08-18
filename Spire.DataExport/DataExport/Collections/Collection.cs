using System;
using System.Collections;
using System.Threading;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.Collections
{
	// Token: 0x020001A4 RID: 420
	public class Collection : CollectionBase, IDisposable
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x00079A78 File Offset: 0x00078A78
		~Collection()
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
			this.Dispose(false);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00079AD4 File Offset: 0x00078AD4
		protected virtual void Dispose(bool Disposing)
		{
			int num = 3;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (Disposing)
					{
						num = 1;
						continue;
					}
					goto IL_14B;
				case 1:
					goto IL_102;
				case 2:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 2:
								goto IL_A0;
							case 3:
								num = 2;
								continue;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								CollectionItem collectionItem = (CollectionItem)enumerator.Current;
								((IDisposable)collectionItem).Dispose();
								num = 0;
								continue;
							}
							}
							IL_7E:
							num = 4;
							continue;
							goto IL_7E;
						}
						IL_A0:
						goto IL_14B;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_101;
								case 1:
									goto IL_FF;
								case 2:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										disposable.Dispose();
										num = 1;
										continue;
									}
									break;
								}
								break;
							}
						}
						IL_FF:
						IL_101:;
					}
					goto IL_102;
				case 4:
					num = 0;
					continue;
				}
				if (!this.ᜀ)
				{
					num = 4;
					continue;
				}
				break;
				IL_102:
				enumerator = base.InnerList.GetEnumerator();
				num = 2;
			}
			IL_14B:
			this.ᜀ = true;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00079C44 File Offset: 0x00078C44
		public void Dispose()
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
				Monitor.Enter(this);
				try
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}
				finally
				{
					if (true)
					{
					}
					Monitor.Exit(this);
				}
				break;
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00079CB0 File Offset: 0x00078CB0
		public void Close()
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
			this.Dispose();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00079CF4 File Offset: 0x00078CF4
		private void ᜁ(CollectionItem A_0)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (A_0.Collection != this)
					{
						num = 3;
						continue;
					}
					goto IL_98;
				case 2:
					goto IL_6E;
				case 3:
					A_0.Collection.Remove(A_0);
					num = 2;
					continue;
				case 4:
					num = 1;
					continue;
				}
				if (A_0.Collection == null)
				{
					break;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
			}
			IL_6E:
			IL_98:
			A_0.Collection = this;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00079DA0 File Offset: 0x00078DA0
		private void ᜀ(CollectionItem A_0)
		{
			int a_ = 18;
			if (A_0 == null)
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
					throw new NullReferenceException(HyperlinksCollectionEditor.b("⌭㨯焱嬳娵吷弹弻䨽⤿ⵁ⩃籅片ཉ≋㵍╏⁑ㅓὕⱗ㽙ㅛၝཟᙡ⩣፥ѧ٩䁫ᡭᅯq乳㽵౷όᅻ", a_));
				}
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00079E00 File Offset: 0x00078E00
		private void ᜀ(int A_0)
		{
			int a_ = 13;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_77;
				case 3:
					goto IL_9A;
				}
				if (true)
				{
				}
				if (A_0 >= 0)
				{
					num = 0;
					continue;
				}
				break;
				IL_77:
				if (A_0 < base.InnerList.Count)
				{
					return;
				}
				num = 3;
			}
			IL_5B:
			throw new ArgumentOutOfRangeException(HyperlinksCollectionEditor.b("␨K栬䄮䈰䘲䜴制瀸唺夼娾㥀ੂ⭄ᕆ⡈╊⩌⩎歐楒ᙔ㡖㕘㝚㡜㱞ᕠ੢੤०䕨䭪᭬๮Ͱ䥲㱴᥶ᵸṺռ", a_));
			IL_9A:
			goto IL_5B;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00079EAC File Offset: 0x00078EAC
		public int Add(CollectionItem Item)
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
			this.ᜀ(Item);
			this.ᜁ(Item);
			Item.InitCollectionItem();
			return base.InnerList.Add(Item);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00079F08 File Offset: 0x00078F08
		public void Insert(int Index, CollectionItem Item)
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
			this.ᜀ(Item);
			this.ᜁ(Item);
			Item.InitCollectionItem();
			base.InnerList.Insert(Index, Item);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00079F64 File Offset: 0x00078F64
		public void Remove(CollectionItem Item)
		{
			for (;;)
			{
				this.ᜀ(Item);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							Item.Collection = null;
							num = 2;
							continue;
						}
						break;
					case 1:
						if (true)
						{
						}
						if (Item.Collection == this)
						{
							num = 0;
							continue;
						}
						goto IL_75;
					case 2:
						goto IL_73;
					}
					break;
				}
			}
			IL_73:
			IL_75:
			base.InnerList.Remove(Item);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00079FF4 File Offset: 0x00078FF4
		public new void RemoveAt(int Index)
		{
			for (;;)
			{
				this.ᜀ(Index);
				CollectionItem collectionItem = this[Index];
				base.InnerList.RemoveAt(Index);
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (collectionItem.Collection == this)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							collectionItem.Collection = null;
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0007A090 File Offset: 0x00079090
		public bool Contains(CollectionItem Item)
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
			return base.InnerList.Contains(Item);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0007A0D8 File Offset: 0x000790D8
		public int IndexOf(CollectionItem Item)
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
			return base.InnerList.IndexOf(Item);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0007A120 File Offset: 0x00079120
		public void CopyTo(CollectionItem[] Array, int Index)
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
			base.InnerList.CopyTo(Array, Index);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0007A168 File Offset: 0x00079168
		public Array ToArray()
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
			return base.InnerList.ToArray(typeof(CollectionItem));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0007A1B8 File Offset: 0x000791B8
		public Array ToArray(Type Type)
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
			return base.InnerList.ToArray(Type);
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0007A200 File Offset: 0x00079200
		public object Holder
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
				return this.m_holder;
			}
		}

		// Token: 0x170000F0 RID: 240
		public CollectionItem this[int Index]
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
				return base.InnerList[Index] as CollectionItem;
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
				base.InnerList[Index] = value;
			}
		}

		// Token: 0x040008CB RID: 2251
		private long \u25D8\u008F\u00A3\u00A3;

		// Token: 0x040008CC RID: 2252
		private bool ᜀ;

		// Token: 0x040008CD RID: 2253
		protected object m_holder;
	}
}
