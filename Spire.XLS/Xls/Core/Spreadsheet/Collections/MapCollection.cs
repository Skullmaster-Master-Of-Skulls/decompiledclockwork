using System;
using System.Collections;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x020001EB RID: 491
	public class MapCollection : IEnumerable
	{
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x000F2684 File Offset: 0x000F1684
		public RBTreeNode Empty
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

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000F26C8 File Offset: 0x000F16C8
		public int Count
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

		// Token: 0x17000A77 RID: 2679
		public object this[object key]
		{
			get
			{
				RBTreeNode rbtreeNode = this.LBound(key);
				if (this.ᜂ.Compare(rbtreeNode.Key, key) == 0)
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
						return rbtreeNode.Value;
					}
				}
				return null;
			}
			set
			{
				RBTreeNode rbtreeNode;
				for (;;)
				{
					for (;;)
					{
						rbtreeNode = this.LBound(key);
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (this.ᜂ.Compare(rbtreeNode.Key, key) != 0)
								{
									if (true)
									{
									}
									num = 3;
									continue;
								}
								goto IL_9A;
							case 1:
								if (!rbtreeNode.IsNil)
								{
									num = 2;
									continue;
								}
								goto IL_44;
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
									num = 0;
									continue;
								}
								break;
							case 3:
								goto IL_98;
							}
							break;
						}
					}
				}
				IL_44:
				this.Add(key, value);
				return;
				IL_98:
				goto IL_44;
				IL_9A:
				rbtreeNode.Value = value;
			}
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000F2820 File Offset: 0x000F1820
		public MapCollection()
		{
			this.ᜂ = Comparer.Default;
			base..ctor();
			this.Initialize();
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000F2844 File Offset: 0x000F1844
		public MapCollection(IComparer comparer)
		{
			int a_ = 7;
			this.ᜂ = Comparer.Default;
			base..ctor();
			if (comparer == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("帼倾ⱀ㍂⑄㕆ⱈ㥊", a_));
			}
			this.Initialize();
			this.ᜂ = comparer;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x000F2894 File Offset: 0x000F1894
		protected void Initialize()
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
			this.ᜀ = new RBTreeNode(null, null, null, null, null, NodeColor.Black);
			this.ᜀ.IsNil = true;
			this.ᜀ.Parent = (this.ᜀ.Left = (this.ᜀ.Right = this.ᜀ));
			this.ᜁ = 0;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000F2920 File Offset: 0x000F1920
		public void Clear()
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
			this.Erase(this.ᜀ.Parent);
			this.ᜀ.Parent = (this.ᜀ.Left = (this.ᜀ.Right = this.ᜀ));
			this.ᜁ = 0;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000F29A0 File Offset: 0x000F19A0
		public void Add(object key, object value)
		{
			switch (0)
			{
			default:
			{
				RBTreeNode rbtreeNode2;
				bool flag;
				for (;;)
				{
					RBTreeNode rbtreeNode = this.ᜀ.Parent;
					rbtreeNode2 = this.ᜀ;
					flag = true;
					int num = 10;
					for (;;)
					{
						RBTreeNode rbtreeNode3;
						switch (num)
						{
						case 0:
						{
							RBTreeNode node = rbtreeNode2;
							num = 3;
							continue;
						}
						case 1:
							goto IL_14B;
						case 2:
							num = 5;
							continue;
						case 3:
							if (flag)
							{
								num = 2;
								continue;
							}
							goto IL_18F;
						case 4:
							goto IL_89;
						case 5:
						{
							if (rbtreeNode2 == this.ᜀ())
							{
								goto IL_80;
							}
							RBTreeNode node = MapCollection.Dec(node);
							num = 1;
							continue;
						}
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_80;
							default:
								if (false)
								{
								}
								goto IL_8B;
							}
							break;
						case 7:
							num = 8;
							continue;
						case 8:
							rbtreeNode3 = rbtreeNode.Right;
							goto IL_14D;
						case 9:
							rbtreeNode3 = rbtreeNode.Left;
							goto IL_14D;
						case 10:
							goto IL_8B;
						case 11:
							if (!flag)
							{
								num = 7;
								continue;
							}
							num = 9;
							continue;
						case 12:
							if (rbtreeNode.IsNil)
							{
								num = 0;
								continue;
							}
							rbtreeNode2 = rbtreeNode;
							flag = (this.ᜂ.Compare(key, rbtreeNode.Key) < 0);
							num = 11;
							continue;
						}
						break;
						IL_80:
						num = 4;
						continue;
						IL_8B:
						if (true)
						{
						}
						num = 12;
						continue;
						IL_14D:
						rbtreeNode = rbtreeNode3;
						num = 6;
					}
				}
				IL_89:
				this.Insert(true, rbtreeNode2, key, value);
				return;
				IL_14B:
				IL_18F:
				this.Insert(flag, rbtreeNode2, key, value);
				return;
			}
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000F2B48 File Offset: 0x000F1B48
		public bool Contains(object key)
		{
			RBTreeNode rbtreeNode = this.LBound(key);
			if (rbtreeNode != this.ᜀ)
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
					return this.ᜂ.Compare(rbtreeNode.Key, key) == 0;
				}
			}
			return false;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000F2BB0 File Offset: 0x000F1BB0
		public void Remove(object key)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					RBTreeNode rbtreeNode = this.LBound(key);
					int num = 12;
					for (;;)
					{
						RBTreeNode rbtreeNode2;
						RBTreeNode rbtreeNode3;
						RBTreeNode rbtreeNode4;
						switch (num)
						{
						case 0:
							if (rbtreeNode2.Color == NodeColor.Red)
							{
								num = 74;
								continue;
							}
							goto IL_6BB;
						case 1:
							goto IL_71F;
						case 2:
							if (!rbtreeNode3.IsNil)
							{
								num = 34;
								continue;
							}
							goto IL_5A3;
						case 3:
							goto IL_78C;
						case 4:
							goto IL_71F;
						case 5:
							rbtreeNode2.Right.Color = NodeColor.Black;
							rbtreeNode2.Color = NodeColor.Red;
							this.LRotate(rbtreeNode2);
							rbtreeNode2 = rbtreeNode4.Left;
							num = 50;
							continue;
						case 6:
							if (!rbtreeNode3.IsNil)
							{
								num = 22;
								continue;
							}
							goto IL_A18;
						case 7:
							goto IL_7BA;
						case 8:
							if (rbtreeNode4.Left == rbtreeNode)
							{
								num = 75;
								continue;
							}
							rbtreeNode4.Right = rbtreeNode3;
							num = 59;
							continue;
						case 9:
							num = 65;
							continue;
						case 10:
							num = 86;
							continue;
						case 11:
							goto IL_A18;
						case 12:
							if (rbtreeNode.IsNil)
							{
								num = 76;
								continue;
							}
							rbtreeNode2 = rbtreeNode;
							num = 72;
							continue;
						case 13:
							rbtreeNode4 = rbtreeNode.Parent;
							num = 2;
							continue;
						case 14:
							rbtreeNode3 = rbtreeNode2.Right;
							num = 38;
							continue;
						case 15:
							if (this.ᜀ.Right == rbtreeNode)
							{
								num = 54;
								continue;
							}
							goto IL_4BE;
						case 16:
							if (rbtreeNode2.Right.Color == NodeColor.Black)
							{
								num = 31;
								continue;
							}
							goto IL_4E7;
						case 17:
							if (rbtreeNode3 == rbtreeNode4.Left)
							{
								num = 60;
								continue;
							}
							rbtreeNode2 = rbtreeNode4.Left;
							num = 32;
							continue;
						case 18:
							goto IL_32A;
						case 19:
							goto IL_659;
						case 20:
							rbtreeNode2.Color = NodeColor.Red;
							rbtreeNode3 = rbtreeNode4;
							num = 4;
							continue;
						case 21:
							this.ᜀ.Parent = rbtreeNode3;
							num = 36;
							continue;
						case 22:
							rbtreeNode3.Parent = rbtreeNode4;
							num = 11;
							continue;
						case 23:
							goto IL_5A3;
						case 24:
							goto IL_32A;
						case 25:
							this.ᜀ.Right = (rbtreeNode3.IsNil ? rbtreeNode4 : MapCollection.Max(rbtreeNode3));
							num = 71;
							continue;
						case 26:
							if (rbtreeNode2.Left.Color == NodeColor.Black)
							{
								num = 20;
								continue;
							}
							goto IL_4E7;
						case 27:
							this.ᜀ.Parent = rbtreeNode2;
							num = 79;
							continue;
						case 28:
							goto IL_71F;
						case 29:
							goto IL_6BB;
						case 30:
							goto IL_71F;
						case 31:
							num = 26;
							continue;
						case 32:
							if (rbtreeNode2.Color == NodeColor.Red)
							{
								num = 80;
								continue;
							}
							goto IL_9F0;
						case 33:
							if (rbtreeNode2.IsNil)
							{
								num = 47;
								continue;
							}
							num = 64;
							continue;
						case 34:
							rbtreeNode3.Parent = rbtreeNode4;
							num = 23;
							continue;
						case 35:
							if (rbtreeNode3.Color != NodeColor.Black)
							{
								num = 51;
								continue;
							}
							num = 17;
							continue;
						case 36:
							goto IL_575;
						case 37:
							if (this.ᜀ.Parent == rbtreeNode)
							{
								num = 27;
								continue;
							}
							num = 73;
							continue;
						case 38:
							goto IL_A48;
						case 39:
							goto IL_78C;
						case 40:
							goto IL_40E;
						case 41:
							goto IL_9F0;
						case 42:
							goto IL_1E1;
						case 43:
							goto IL_5FE;
						case 44:
							rbtreeNode2.Color = NodeColor.Red;
							rbtreeNode3 = rbtreeNode4;
							num = 28;
							continue;
						case 45:
							rbtreeNode3 = rbtreeNode4;
							num = 1;
							continue;
						case 46:
							goto IL_5FE;
						case 47:
							rbtreeNode3 = rbtreeNode4;
							num = 30;
							continue;
						case 48:
							goto IL_53E;
						case 49:
							if (rbtreeNode.Color == NodeColor.Black)
							{
								num = 53;
								continue;
							}
							goto IL_1E1;
						case 50:
							goto IL_1AA;
						case 51:
							goto IL_659;
						case 52:
							if (rbtreeNode3 != this.ᜀ.Parent)
							{
								num = 57;
								continue;
							}
							goto IL_659;
						case 53:
							num = 39;
							continue;
						case 54:
							num = 25;
							continue;
						case 55:
							rbtreeNode.Parent.Left = rbtreeNode2;
							num = 43;
							continue;
						case 56:
							if (0 < this.ᜁ)
							{
								num = 70;
								continue;
							}
							return;
						case 57:
							num = 35;
							continue;
						case 58:
							goto IL_A48;
						case 59:
							goto IL_575;
						case 60:
							rbtreeNode2 = rbtreeNode4.Right;
							num = 0;
							continue;
						case 61:
							if (rbtreeNode2.Right.Color == NodeColor.Black)
							{
								num = 67;
								continue;
							}
							goto IL_53E;
						case 62:
							if (rbtreeNode2.IsNil)
							{
								num = 45;
								continue;
							}
							num = 16;
							continue;
						case 63:
							if (rbtreeNode2 == rbtreeNode.Right)
							{
								num = 81;
								continue;
							}
							rbtreeNode4 = rbtreeNode2.Parent;
							num = 6;
							continue;
						case 64:
							if (rbtreeNode2.Left.Color == NodeColor.Black)
							{
								num = 9;
								continue;
							}
							goto IL_2FC;
						case 65:
							if (rbtreeNode2.Right.Color == NodeColor.Black)
							{
								num = 44;
								continue;
							}
							goto IL_2FC;
						case 66:
							if (this.ᜀ.Left == rbtreeNode)
							{
								num = 10;
								continue;
							}
							goto IL_7BA;
						case 67:
							rbtreeNode2.Left.Color = NodeColor.Black;
							rbtreeNode2.Color = NodeColor.Red;
							this.RRotate(rbtreeNode2);
							rbtreeNode2 = rbtreeNode4.Right;
							num = 48;
							continue;
						case 68:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_40E;
							default:
								if (false)
								{
								}
								goto IL_575;
							}
							break;
						case 69:
							goto IL_659;
						case 70:
							this.ᜁ--;
							num = 85;
							continue;
						case 71:
							goto IL_4BE;
						case 72:
							if (rbtreeNode2.Left.IsNil)
							{
								num = 14;
								continue;
							}
							num = 87;
							continue;
						case 73:
							if (rbtreeNode.Parent.Left == rbtreeNode)
							{
								num = 55;
								continue;
							}
							if (true)
							{
							}
							rbtreeNode.Parent.Right = rbtreeNode2;
							num = 46;
							continue;
						case 74:
							rbtreeNode2.Color = NodeColor.Black;
							rbtreeNode4.Color = NodeColor.Red;
							this.LRotate(rbtreeNode4);
							rbtreeNode2 = rbtreeNode4.Right;
							num = 29;
							continue;
						case 75:
							rbtreeNode4.Left = rbtreeNode3;
							num = 68;
							continue;
						case 76:
							return;
						case 77:
							goto IL_4BE;
						case 78:
							goto IL_A48;
						case 79:
							goto IL_5FE;
						case 80:
							rbtreeNode2.Color = NodeColor.Black;
							rbtreeNode4.Color = NodeColor.Red;
							this.RRotate(rbtreeNode4);
							rbtreeNode2 = rbtreeNode4.Left;
							num = 41;
							continue;
						case 81:
							rbtreeNode4 = rbtreeNode2;
							num = 18;
							continue;
						case 82:
							if (this.ᜀ.Parent == rbtreeNode)
							{
								num = 21;
								continue;
							}
							num = 8;
							continue;
						case 83:
							if (rbtreeNode2 == rbtreeNode)
							{
								num = 13;
								continue;
							}
							rbtreeNode.Left.Parent = rbtreeNode2;
							rbtreeNode2.Left = rbtreeNode.Left;
							num = 63;
							continue;
						case 84:
							if (rbtreeNode2.Left.Color == NodeColor.Black)
							{
								num = 5;
								continue;
							}
							goto IL_1AA;
						case 85:
							return;
						case 86:
							this.ᜀ.Left = (rbtreeNode3.IsNil ? rbtreeNode4 : MapCollection.Min(rbtreeNode3));
							num = 7;
							continue;
						case 87:
							if (rbtreeNode2.Right.IsNil)
							{
								num = 40;
								continue;
							}
							rbtreeNode2 = MapCollection.Inc(rbtreeNode);
							rbtreeNode3 = rbtreeNode2.Right;
							num = 78;
							continue;
						}
						break;
						IL_1AA:
						rbtreeNode2.Color = rbtreeNode4.Color;
						rbtreeNode4.Color = NodeColor.Black;
						rbtreeNode2.Left.Color = NodeColor.Black;
						this.RRotate(rbtreeNode4);
						num = 69;
						continue;
						IL_1E1:
						num = 56;
						continue;
						IL_2FC:
						num = 61;
						continue;
						IL_32A:
						num = 37;
						continue;
						IL_40E:
						rbtreeNode3 = rbtreeNode2.Left;
						num = 58;
						continue;
						IL_4BE:
						num = 49;
						continue;
						IL_4E7:
						num = 84;
						continue;
						IL_53E:
						rbtreeNode2.Color = rbtreeNode4.Color;
						rbtreeNode4.Color = NodeColor.Black;
						rbtreeNode2.Right.Color = NodeColor.Black;
						this.LRotate(rbtreeNode4);
						num = 19;
						continue;
						IL_575:
						num = 66;
						continue;
						IL_5A3:
						num = 82;
						continue;
						IL_5FE:
						rbtreeNode2.Parent = rbtreeNode.Parent;
						NodeColor color = rbtreeNode.Color;
						rbtreeNode.Color = rbtreeNode2.Color;
						rbtreeNode2.Color = color;
						num = 77;
						continue;
						IL_659:
						rbtreeNode3.Color = NodeColor.Black;
						num = 42;
						continue;
						IL_6BB:
						num = 33;
						continue;
						IL_71F:
						rbtreeNode4 = rbtreeNode3.Parent;
						num = 3;
						continue;
						IL_78C:
						num = 52;
						continue;
						IL_7BA:
						num = 15;
						continue;
						IL_9F0:
						num = 62;
						continue;
						IL_A18:
						rbtreeNode4.Left = rbtreeNode3;
						rbtreeNode2.Right = rbtreeNode.Right;
						rbtreeNode.Right.Parent = rbtreeNode2;
						num = 24;
						continue;
						IL_A48:
						num = 83;
					}
				}
				return;
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000F3660 File Offset: 0x000F2660
		private RBTreeNode ᜀ()
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
			return this.ᜀ.Left;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000F36A8 File Offset: 0x000F26A8
		public static RBTreeNode Min(RBTreeNode node)
		{
			for (;;)
			{
				IL_00:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						break;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (node.Left.IsNil)
							{
								num = 3;
								continue;
							}
							node = node.Left;
							num = 2;
							continue;
						}
						break;
					case 3:
						return node;
					}
					IL_2A:
					num = 1;
					continue;
					goto IL_2A;
				}
			}
			return node;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000F3738 File Offset: 0x000F2738
		public static RBTreeNode Max(RBTreeNode node)
		{
			for (;;)
			{
				IL_00:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 2:
						return node;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (node.Right.IsNil)
							{
								num = 2;
								continue;
							}
							node = node.Right;
							num = 0;
							continue;
						}
						break;
					}
					IL_22:
					if (true)
					{
					}
					num = 3;
					continue;
					goto IL_22;
				}
			}
			return node;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000F37C8 File Offset: 0x000F27C8
		public static RBTreeNode Inc(RBTreeNode node)
		{
			int a_ = 12;
			int num = 9;
			for (;;)
			{
				RBTreeNode parent;
				switch (num)
				{
				case 0:
					goto IL_140;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_16B;
					default:
						goto IL_71;
					}
					break;
				case 2:
					node = MapCollection.Min(node.Right);
					num = 5;
					continue;
				case 3:
					goto IL_86;
				case 4:
					if (node != parent.Right)
					{
						num = 0;
						continue;
					}
					node = parent;
					num = 3;
					continue;
				case 5:
					goto IL_D0;
				case 6:
					num = 4;
					continue;
				case 7:
					return node;
				case 8:
					return node;
				case 10:
					if (!node.Right.IsNil)
					{
						goto IL_16B;
					}
					goto IL_86;
				case 11:
					if (!(parent = node.Parent).IsNil)
					{
						num = 6;
						continue;
					}
					goto IL_140;
				case 12:
					if (node.IsNil)
					{
						num = 8;
						continue;
					}
					num = 10;
					continue;
				}
				if (node == null)
				{
					num = 1;
					continue;
				}
				num = 12;
				continue;
				IL_86:
				if (true)
				{
				}
				num = 11;
				continue;
				IL_140:
				node = parent;
				num = 7;
				continue;
				IL_16B:
				num = 2;
			}
			IL_71:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ⱁ⭃≅ⵇ", a_));
			IL_D0:
			return node;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000F3954 File Offset: 0x000F2954
		public static RBTreeNode Dec(RBTreeNode node)
		{
			int a_ = 13;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_100;
				case 1:
				{
					RBTreeNode parent;
					if (!parent.IsNil)
					{
						num = 12;
						continue;
					}
					return node;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_161;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 3:
					goto IL_64;
				case 4:
					if (node.IsNil)
					{
						num = 5;
						continue;
					}
					num = 15;
					continue;
				case 5:
					node = node.Right;
					num = 7;
					continue;
				case 6:
					return node;
				case 7:
					return node;
				case 9:
				{
					RBTreeNode parent;
					if (!(parent = node.Parent).IsNil)
					{
						num = 2;
						continue;
					}
					goto IL_C9;
				}
				case 10:
					node = MapCollection.Max(node.Left);
					num = 13;
					continue;
				case 11:
				{
					RBTreeNode parent;
					if (parent != parent.Left)
					{
						num = 14;
						continue;
					}
					node = parent;
					num = 0;
					continue;
				}
				case 12:
				{
					RBTreeNode parent;
					node = parent;
					num = 6;
					continue;
				}
				case 13:
					return node;
				case 14:
					goto IL_C9;
				case 15:
					goto IL_161;
				}
				if (node == null)
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				num = 4;
				continue;
				IL_C9:
				num = 1;
				continue;
				IL_100:
				num = 9;
				continue;
				IL_161:
				if (node.Left.IsNil)
				{
					goto IL_100;
				}
				num = 10;
			}
			IL_64:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⵂ⩄⍆ⱈ", a_));
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000F3B0C File Offset: 0x000F2B0C
		protected RBTreeNode LBound(object key)
		{
			RBTreeNode result;
			for (;;)
			{
				RBTreeNode rbtreeNode = this.ᜀ.Parent;
				result = this.ᜀ;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						rbtreeNode = rbtreeNode.Right;
						num = 6;
						continue;
					case 2:
						goto IL_97;
					case 3:
						IL_BE:
						if (rbtreeNode.IsNil)
						{
							num = 0;
							continue;
						}
						num = 5;
						continue;
					case 4:
						goto IL_97;
					case 5:
						if (this.ᜂ.Compare(rbtreeNode.Key, key) < 0)
						{
							num = 1;
							continue;
						}
						result = rbtreeNode;
						rbtreeNode = rbtreeNode.Left;
						num = 2;
						continue;
					case 6:
						if (true)
						{
						}
						goto IL_97;
					}
					break;
					IL_97:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						if (false)
						{
						}
						num = 3;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000F3BF8 File Offset: 0x000F2BF8
		protected RBTreeNode UBound(object key)
		{
			RBTreeNode result;
			for (;;)
			{
				RBTreeNode rbtreeNode = this.ᜀ.Parent;
				result = this.ᜀ;
				int num = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						result = rbtreeNode;
						rbtreeNode = rbtreeNode.Left;
						num = 2;
						continue;
					case 1:
						return result;
					case 2:
						goto IL_97;
					case 3:
						goto IL_97;
					case 4:
						goto IL_97;
					case 5:
						IL_BE:
						if (rbtreeNode.IsNil)
						{
							num = 1;
							continue;
						}
						num = 6;
						continue;
					case 6:
						if (this.ᜂ.Compare(key, rbtreeNode.Key) < 0)
						{
							num = 0;
							continue;
						}
						rbtreeNode = rbtreeNode.Right;
						num = 4;
						continue;
					}
					break;
					IL_97:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BE;
					default:
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000F3CE4 File Offset: 0x000F2CE4
		protected void LRotate(RBTreeNode _where)
		{
			RBTreeNode right;
			for (;;)
			{
				right = _where.Right;
				_where.Right = right.Left;
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_95;
					case 1:
						goto IL_F2;
					case 2:
						right.Left.Parent = _where;
						num = 1;
						continue;
					case 3:
						goto IL_79;
					case 4:
						if (_where == _where.Parent.Left)
						{
							num = 6;
							continue;
						}
						_where.Parent.Right = right;
						num = 7;
						continue;
					case 5:
						if (_where == this.ᜀ.Parent)
						{
							num = 9;
							continue;
						}
						num = 4;
						continue;
					case 6:
						_where.Parent.Left = right;
						num = 3;
						continue;
					case 7:
						goto IL_E6;
					case 8:
						if (!right.Left.IsNil)
						{
							num = 2;
							continue;
						}
						goto IL_F2;
					case 9:
						if (true)
						{
						}
						this.ᜀ.Parent = right;
						num = 0;
						continue;
					}
					break;
					IL_F2:
					right.Parent = _where.Parent;
					num = 5;
				}
			}
			IL_79:
			goto IL_158;
			IL_95:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_E6:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			IL_158:
			right.Left = _where;
			_where.Parent = right;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000F3E58 File Offset: 0x000F2E58
		protected void RRotate(RBTreeNode _where)
		{
			RBTreeNode left;
			for (;;)
			{
				left = _where.Left;
				_where.Left = left.Right;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F8;
					case 1:
						goto IL_79;
					case 2:
						goto IL_FA;
					case 3:
						if (_where == this.ᜀ.Parent)
						{
							num = 4;
							continue;
						}
						num = 9;
						continue;
					case 4:
						if (true)
						{
						}
						this.ᜀ.Parent = left;
						num = 7;
						continue;
					case 5:
						if (!left.Right.IsNil)
						{
							num = 8;
							continue;
						}
						goto IL_FA;
					case 6:
						_where.Parent.Right = left;
						num = 1;
						continue;
					case 7:
						goto IL_9D;
					case 8:
						left.Right.Parent = _where;
						num = 2;
						continue;
					case 9:
						if (_where == _where.Parent.Right)
						{
							num = 6;
							continue;
						}
						_where.Parent.Left = left;
						num = 0;
						continue;
					}
					break;
					IL_FA:
					left.Parent = _where.Parent;
					num = 3;
				}
			}
			IL_79:
			goto IL_158;
			IL_9D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_F8:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			IL_158:
			left.Right = _where;
			_where.Parent = left;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000F3FCC File Offset: 0x000F2FCC
		protected void Erase(RBTreeNode _root)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_70:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_34;
			}
			RBTreeNode rbtreeNode;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_40;
				case 1:
					goto IL_40;
				case 2:
					if (rbtreeNode.IsNil)
					{
						num = 3;
						continue;
					}
					goto IL_5A;
				case 3:
					goto IL_58;
				}
				goto IL_34;
				IL_40:
				num = 2;
			}
			IL_58:
			if (true)
			{
			}
			return;
			IL_5A:
			this.Erase(rbtreeNode.Right);
			rbtreeNode = rbtreeNode.Left;
			_root = rbtreeNode;
			goto IL_70;
			IL_34:
			rbtreeNode = _root;
			num = 0;
			goto IL_1E;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000F4068 File Offset: 0x000F3068
		protected void Insert(bool _addLeft, RBTreeNode _where, object key, object value)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					RBTreeNode rbtreeNode = new RBTreeNode(this.ᜀ, _where, this.ᜀ, key, value);
					this.ᜁ++;
					int num = 2;
					for (;;)
					{
						RBTreeNode rbtreeNode2;
						switch (num)
						{
						case 0:
							if (_where == this.ᜀ.Left)
							{
								num = 10;
								continue;
							}
							goto IL_32C;
						case 1:
							rbtreeNode2 = rbtreeNode2.Parent;
							this.LRotate(rbtreeNode2);
							num = 15;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_442;
							default:
								if (false)
								{
								}
								if (_where == this.ᜀ)
								{
									num = 16;
									continue;
								}
								num = 27;
								continue;
							}
							break;
						case 3:
							_where = rbtreeNode2.Parent.Parent.Right;
							num = 26;
							continue;
						case 4:
							if (rbtreeNode2 == rbtreeNode2.Parent.Right)
							{
								num = 1;
								continue;
							}
							goto IL_1EF;
						case 5:
							if (rbtreeNode2 == rbtreeNode2.Parent.Left)
							{
								num = 11;
								continue;
							}
							goto IL_22E;
						case 6:
							goto IL_32C;
						case 7:
							goto IL_2FF;
						case 8:
							this.ᜀ.Right = rbtreeNode;
							num = 6;
							continue;
						case 9:
							goto IL_22E;
						case 10:
							this.ᜀ.Left = rbtreeNode;
							num = 29;
							continue;
						case 11:
							rbtreeNode2 = rbtreeNode2.Parent;
							this.RRotate(rbtreeNode2);
							num = 9;
							continue;
						case 12:
							goto IL_327;
						case 13:
							rbtreeNode2.Parent.Color = NodeColor.Black;
							_where.Color = NodeColor.Black;
							rbtreeNode2.Parent.Parent.Color = NodeColor.Red;
							rbtreeNode2 = rbtreeNode2.Parent.Parent;
							num = 7;
							continue;
						case 14:
							if (rbtreeNode2.Parent == rbtreeNode2.Parent.Parent.Left)
							{
								num = 3;
								continue;
							}
							if (true)
							{
							}
							_where = rbtreeNode2.Parent.Parent.Left;
							num = 18;
							continue;
						case 15:
							goto IL_442;
						case 16:
							this.ᜀ.Parent = (this.ᜀ.Left = (this.ᜀ.Right = rbtreeNode));
							num = 20;
							continue;
						case 17:
							if (_where == this.ᜀ.Right)
							{
								num = 8;
								continue;
							}
							goto IL_32C;
						case 18:
							if (_where.Color == NodeColor.Red)
							{
								num = 13;
								continue;
							}
							num = 5;
							continue;
						case 19:
							goto IL_2FF;
						case 20:
							goto IL_32C;
						case 21:
							goto IL_2FF;
						case 22:
							goto IL_2FF;
						case 23:
							rbtreeNode2.Parent.Color = NodeColor.Black;
							_where.Color = NodeColor.Black;
							rbtreeNode2.Parent.Parent.Color = NodeColor.Red;
							rbtreeNode2 = rbtreeNode2.Parent.Parent;
							num = 28;
							continue;
						case 24:
							if (rbtreeNode2.Parent.Color != NodeColor.Red)
							{
								num = 12;
								continue;
							}
							num = 14;
							continue;
						case 25:
							_where.Left = rbtreeNode;
							num = 0;
							continue;
						case 26:
							if (_where.Color == NodeColor.Red)
							{
								num = 23;
								continue;
							}
							num = 4;
							continue;
						case 27:
							if (_addLeft)
							{
								num = 25;
								continue;
							}
							_where.Right = rbtreeNode;
							num = 17;
							continue;
						case 28:
							goto IL_2FF;
						case 29:
							goto IL_32C;
						}
						break;
						IL_1EF:
						rbtreeNode2.Parent.Color = NodeColor.Black;
						rbtreeNode2.Parent.Parent.Color = NodeColor.Red;
						this.RRotate(rbtreeNode2.Parent.Parent);
						num = 22;
						continue;
						IL_442:
						goto IL_1EF;
						IL_22E:
						rbtreeNode2.Parent.Color = NodeColor.Black;
						rbtreeNode2.Parent.Parent.Color = NodeColor.Red;
						this.LRotate(rbtreeNode2.Parent.Parent);
						num = 19;
						continue;
						IL_2FF:
						num = 24;
						continue;
						IL_32C:
						rbtreeNode2 = rbtreeNode;
						num = 21;
					}
				}
				IL_327:
				this.ᜀ.Parent.Color = NodeColor.Black;
				return;
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000F4508 File Offset: 0x000F3508
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
			return new MapEnumerator(this.ᜀ.Left);
		}

		// Token: 0x0400106A RID: 4202
		private byte \u25D8\u0081\u008C\u008B;

		// Token: 0x0400106B RID: 4203
		private RBTreeNode ᜀ;

		// Token: 0x0400106C RID: 4204
		private bool[] \u25D9\u0098\u00A2\u009E;

		// Token: 0x0400106D RID: 4205
		private byte[] \u2609\u00AB\u0096\u0094;

		// Token: 0x0400106E RID: 4206
		private int ᜁ;

		// Token: 0x0400106F RID: 4207
		private int \u25D9\u009F\u0090\u00A5;

		// Token: 0x04001070 RID: 4208
		private IComparer ᜂ;
	}
}
