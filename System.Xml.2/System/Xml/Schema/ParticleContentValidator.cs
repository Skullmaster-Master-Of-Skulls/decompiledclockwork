using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001F9 RID: 505
	internal sealed class ParticleContentValidator : ContentValidator
	{
		// Token: 0x060020DD RID: 8413 RVA: 0x000B32FE File Offset: 0x000B14FE
		public ParticleContentValidator(XmlSchemaContentType contentType) : this(contentType, true)
		{
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000B3308 File Offset: 0x000B1508
		public ParticleContentValidator(XmlSchemaContentType contentType, bool enableUpaCheck) : base(contentType)
		{
			this.enableUpaCheck = enableUpaCheck;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000B3318 File Offset: 0x000B1518
		public override void InitValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000B331F File Offset: 0x000B151F
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000B3326 File Offset: 0x000B1526
		public override bool CompleteValidation(ValidationState context)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000B332D File Offset: 0x000B152D
		public void Start()
		{
			this.symbols = new SymbolsDictionary();
			this.positions = new Positions();
			this.stack = new Stack();
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000B3350 File Offset: 0x000B1550
		public void OpenGroup()
		{
			this.stack.Push(null);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000B3360 File Offset: 0x000B1560
		public void CloseGroup()
		{
			SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
			if (syntaxTreeNode == null)
			{
				return;
			}
			if (this.stack.Count == 0)
			{
				this.contentNode = syntaxTreeNode;
				this.isPartial = false;
				return;
			}
			InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
			if (interiorNode != null)
			{
				interiorNode.RightChild = syntaxTreeNode;
				syntaxTreeNode = interiorNode;
				this.isPartial = true;
			}
			else
			{
				this.isPartial = false;
			}
			this.stack.Push(syntaxTreeNode);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000B33D7 File Offset: 0x000B15D7
		public bool Exists(XmlQualifiedName name)
		{
			return this.symbols.Exists(name);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000B33EA File Offset: 0x000B15EA
		public void AddName(XmlQualifiedName name, object particle)
		{
			this.AddLeafNode(new LeafNode(this.positions.Add(this.symbols.AddName(name, particle), particle)));
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000B3410 File Offset: 0x000B1610
		public void AddNamespaceList(NamespaceList namespaceList, object particle)
		{
			this.symbols.AddNamespaceList(namespaceList, particle, false);
			this.AddLeafNode(new NamespaceListNode(namespaceList, particle));
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000B3430 File Offset: 0x000B1630
		private void AddLeafNode(SyntaxTreeNode node)
		{
			if (this.stack.Count > 0)
			{
				InteriorNode interiorNode = (InteriorNode)this.stack.Pop();
				if (interiorNode != null)
				{
					interiorNode.RightChild = node;
					node = interiorNode;
				}
			}
			this.stack.Push(node);
			this.isPartial = true;
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000B347C File Offset: 0x000B167C
		public void AddChoice()
		{
			SyntaxTreeNode leftChild = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new ChoiceNode();
			interiorNode.LeftChild = leftChild;
			this.stack.Push(interiorNode);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000B34B4 File Offset: 0x000B16B4
		public void AddSequence()
		{
			SyntaxTreeNode leftChild = (SyntaxTreeNode)this.stack.Pop();
			InteriorNode interiorNode = new SequenceNode();
			interiorNode.LeftChild = leftChild;
			this.stack.Push(interiorNode);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000B34EB File Offset: 0x000B16EB
		public void AddStar()
		{
			this.Closure(new StarNode());
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000B34F8 File Offset: 0x000B16F8
		public void AddPlus()
		{
			this.Closure(new PlusNode());
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000B3505 File Offset: 0x000B1705
		public void AddQMark()
		{
			this.Closure(new QmarkNode());
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x000B3514 File Offset: 0x000B1714
		public void AddLeafRange(decimal min, decimal max)
		{
			LeafRangeNode leafRangeNode = new LeafRangeNode(min, max);
			int pos = this.positions.Add(-2, leafRangeNode);
			leafRangeNode.Pos = pos;
			this.Closure(new SequenceNode
			{
				RightChild = leafRangeNode
			});
			this.minMaxNodesCount++;
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x000B3564 File Offset: 0x000B1764
		private void Closure(InteriorNode node)
		{
			if (this.stack.Count > 0)
			{
				SyntaxTreeNode syntaxTreeNode = (SyntaxTreeNode)this.stack.Pop();
				InteriorNode interiorNode = syntaxTreeNode as InteriorNode;
				if (this.isPartial && interiorNode != null)
				{
					node.LeftChild = interiorNode.RightChild;
					interiorNode.RightChild = node;
				}
				else
				{
					node.LeftChild = syntaxTreeNode;
					syntaxTreeNode = node;
				}
				this.stack.Push(syntaxTreeNode);
				return;
			}
			if (this.contentNode != null)
			{
				node.LeftChild = this.contentNode;
				this.contentNode = node;
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x000B35E8 File Offset: 0x000B17E8
		public ContentValidator Finish()
		{
			return this.Finish(true);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x000B35F4 File Offset: 0x000B17F4
		public ContentValidator Finish(bool useDFA)
		{
			if (this.contentNode == null)
			{
				if (base.ContentType != XmlSchemaContentType.Mixed)
				{
					return ContentValidator.Empty;
				}
				string text = base.IsOpen ? "Any" : "TextOnly";
				if (!base.IsOpen)
				{
					return ContentValidator.TextOnly;
				}
				return ContentValidator.Any;
			}
			else
			{
				InteriorNode interiorNode = new SequenceNode();
				interiorNode.LeftChild = this.contentNode;
				LeafNode leafNode = new LeafNode(this.positions.Add(this.symbols.AddName(XmlQualifiedName.Empty, null), null));
				interiorNode.RightChild = leafNode;
				this.contentNode.ExpandTree(interiorNode, this.symbols, this.positions);
				int count = this.symbols.Count;
				int count2 = this.positions.Count;
				BitSet bitSet = new BitSet(count2);
				BitSet lastpos = new BitSet(count2);
				BitSet[] array = new BitSet[count2];
				for (int i = 0; i < count2; i++)
				{
					array[i] = new BitSet(count2);
				}
				interiorNode.ConstructPos(bitSet, lastpos, array);
				if (this.minMaxNodesCount > 0)
				{
					BitSet bitSet2;
					BitSet[] minmaxFollowPos = this.CalculateTotalFollowposForRangeNodes(bitSet, array, out bitSet2);
					if (this.enableUpaCheck)
					{
						this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(bitSet, bitSet2, minmaxFollowPos));
						for (int j = 0; j < count2; j++)
						{
							this.CheckCMUPAWithLeafRangeNodes(this.GetApplicableMinMaxFollowPos(array[j], bitSet2, minmaxFollowPos));
						}
					}
					return new RangeContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, interiorNode.LeftChild.IsNullable, bitSet2, this.minMaxNodesCount);
				}
				int[][] array2 = null;
				if (!this.symbols.IsUpaEnforced)
				{
					if (this.enableUpaCheck)
					{
						this.CheckUniqueParticleAttribution(bitSet, array);
					}
				}
				else if (useDFA)
				{
					array2 = this.BuildTransitionTable(bitSet, array, leafNode.Pos);
				}
				if (array2 != null)
				{
					return new DfaContentValidator(array2, this.symbols, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
				}
				return new NfaContentValidator(bitSet, array, this.symbols, this.positions, leafNode.Pos, base.ContentType, base.IsOpen, interiorNode.LeftChild.IsNullable);
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x000B3810 File Offset: 0x000B1A10
		private BitSet[] CalculateTotalFollowposForRangeNodes(BitSet firstpos, BitSet[] followpos, out BitSet posWithRangeTerminals)
		{
			int count = this.positions.Count;
			posWithRangeTerminals = new BitSet(count);
			BitSet[] array = new BitSet[this.minMaxNodesCount];
			int num = 0;
			for (int i = count - 1; i >= 0; i--)
			{
				Position position = this.positions[i];
				if (position.symbol == -2)
				{
					LeafRangeNode leafRangeNode = position.particle as LeafRangeNode;
					BitSet bitSet = new BitSet(count);
					bitSet.Clear();
					bitSet.Or(followpos[i]);
					if (leafRangeNode.Min != leafRangeNode.Max)
					{
						bitSet.Or(leafRangeNode.NextIteration);
					}
					for (int num2 = bitSet.NextSet(-1); num2 != -1; num2 = bitSet.NextSet(num2))
					{
						if (num2 > i)
						{
							Position position2 = this.positions[num2];
							if (position2.symbol == -2)
							{
								LeafRangeNode leafRangeNode2 = position2.particle as LeafRangeNode;
								bitSet.Or(array[leafRangeNode2.Pos]);
							}
						}
					}
					array[num] = bitSet;
					leafRangeNode.Pos = num++;
					posWithRangeTerminals.Set(i);
				}
			}
			return array;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x000B392C File Offset: 0x000B1B2C
		private void CheckCMUPAWithLeafRangeNodes(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				Position position = this.positions[num];
				int symbol = position.symbol;
				if (symbol >= 0)
				{
					if (array[symbol] != null)
					{
						throw new UpaException(array[symbol], position.particle);
					}
					array[symbol] = position.particle;
				}
			}
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x000B3998 File Offset: 0x000B1B98
		private BitSet GetApplicableMinMaxFollowPos(BitSet curpos, BitSet posWithRangeTerminals, BitSet[] minmaxFollowPos)
		{
			if (curpos.Intersects(posWithRangeTerminals))
			{
				BitSet bitSet = new BitSet(this.positions.Count);
				bitSet.Or(curpos);
				bitSet.And(posWithRangeTerminals);
				curpos = curpos.Clone();
				for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
				{
					LeafRangeNode leafRangeNode = this.positions[num].particle as LeafRangeNode;
					curpos.Or(minmaxFollowPos[leafRangeNode.Pos]);
				}
			}
			return curpos;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x000B3A14 File Offset: 0x000B1C14
		private void CheckUniqueParticleAttribution(BitSet firstpos, BitSet[] followpos)
		{
			this.CheckUniqueParticleAttribution(firstpos);
			for (int i = 0; i < this.positions.Count; i++)
			{
				this.CheckUniqueParticleAttribution(followpos[i]);
			}
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x000B3A48 File Offset: 0x000B1C48
		private void CheckUniqueParticleAttribution(BitSet curpos)
		{
			object[] array = new object[this.symbols.Count];
			for (int num = curpos.NextSet(-1); num != -1; num = curpos.NextSet(num))
			{
				int symbol = this.positions[num].symbol;
				if (array[symbol] == null)
				{
					array[symbol] = this.positions[num].particle;
				}
				else if (array[symbol] != this.positions[num].particle)
				{
					throw new UpaException(array[symbol], this.positions[num].particle);
				}
			}
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x000B3ADC File Offset: 0x000B1CDC
		private int[][] BuildTransitionTable(BitSet firstpos, BitSet[] followpos, int endMarkerPos)
		{
			int count = this.positions.Count;
			int num = 8192 / count;
			int count2 = this.symbols.Count;
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable = new Hashtable();
			hashtable.Add(new BitSet(count), -1);
			Queue queue = new Queue();
			int num2 = 0;
			queue.Enqueue(firstpos);
			hashtable.Add(firstpos, 0);
			arrayList.Add(new int[count2 + 1]);
			while (queue.Count > 0)
			{
				BitSet bitSet = (BitSet)queue.Dequeue();
				int[] array = (int[])arrayList[num2];
				if (bitSet[endMarkerPos])
				{
					array[count2] = 1;
				}
				for (int i = 0; i < count2; i++)
				{
					BitSet bitSet2 = new BitSet(count);
					for (int num3 = bitSet.NextSet(-1); num3 != -1; num3 = bitSet.NextSet(num3))
					{
						if (i == this.positions[num3].symbol)
						{
							bitSet2.Or(followpos[num3]);
						}
					}
					object obj = hashtable[bitSet2];
					if (obj != null)
					{
						array[i] = (int)obj;
					}
					else
					{
						int num4 = hashtable.Count - 1;
						if (num4 >= num)
						{
							return null;
						}
						queue.Enqueue(bitSet2);
						hashtable.Add(bitSet2, num4);
						arrayList.Add(new int[count2 + 1]);
						array[i] = num4;
					}
				}
				num2++;
			}
			return (int[][])arrayList.ToArray(typeof(int[]));
		}

		// Token: 0x04000DC9 RID: 3529
		private SymbolsDictionary symbols;

		// Token: 0x04000DCA RID: 3530
		private Positions positions;

		// Token: 0x04000DCB RID: 3531
		private Stack stack;

		// Token: 0x04000DCC RID: 3532
		private SyntaxTreeNode contentNode;

		// Token: 0x04000DCD RID: 3533
		private bool isPartial;

		// Token: 0x04000DCE RID: 3534
		private int minMaxNodesCount;

		// Token: 0x04000DCF RID: 3535
		private bool enableUpaCheck;
	}
}
