using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200018D RID: 397
	internal class SelectorActiveAxis : ActiveAxis
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0005E103 File Offset: 0x0005D103
		public bool EmptyStack
		{
			get
			{
				return this.KSpointer == 0;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x0005E10E File Offset: 0x0005D10E
		public int lastDepth
		{
			get
			{
				if (this.KSpointer != 0)
				{
					return ((KSStruct)this.KSs[this.KSpointer - 1]).depth;
				}
				return -1;
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0005E137 File Offset: 0x0005D137
		public SelectorActiveAxis(Asttree axisTree, ConstraintStruct cs) : base(axisTree)
		{
			this.KSs = new ArrayList();
			this.cs = cs;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0005E152 File Offset: 0x0005D152
		public override bool EndElement(string localname, string URN)
		{
			base.EndElement(localname, URN);
			return this.KSpointer > 0 && base.CurrentDepth == this.lastDepth;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0005E178 File Offset: 0x0005D178
		public int PushKS(int errline, int errcol)
		{
			KeySequence ks = new KeySequence(this.cs.TableDim, errline, errcol);
			KSStruct ksstruct;
			if (this.KSpointer < this.KSs.Count)
			{
				ksstruct = (KSStruct)this.KSs[this.KSpointer];
				ksstruct.ks = ks;
				for (int i = 0; i < this.cs.TableDim; i++)
				{
					ksstruct.fields[i].Reactivate(ks);
				}
			}
			else
			{
				ksstruct = new KSStruct(ks, this.cs.TableDim);
				for (int j = 0; j < this.cs.TableDim; j++)
				{
					ksstruct.fields[j] = new LocatedActiveAxis(this.cs.constraint.Fields[j], ks, j);
					this.cs.axisFields.Add(ksstruct.fields[j]);
				}
				this.KSs.Add(ksstruct);
			}
			ksstruct.depth = base.CurrentDepth - 1;
			return this.KSpointer++;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0005E280 File Offset: 0x0005D280
		public KeySequence PopKS()
		{
			return ((KSStruct)this.KSs[--this.KSpointer]).ks;
		}

		// Token: 0x04000CA7 RID: 3239
		private ConstraintStruct cs;

		// Token: 0x04000CA8 RID: 3240
		private ArrayList KSs;

		// Token: 0x04000CA9 RID: 3241
		private int KSpointer;
	}
}
