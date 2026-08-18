using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001E6 RID: 486
	internal class SelectorActiveAxis : ActiveAxis
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06002065 RID: 8293 RVA: 0x000B1E1F File Offset: 0x000B001F
		public bool EmptyStack
		{
			get
			{
				return this.KSpointer == 0;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x000B1E2A File Offset: 0x000B002A
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

		// Token: 0x06002067 RID: 8295 RVA: 0x000B1E53 File Offset: 0x000B0053
		public SelectorActiveAxis(Asttree axisTree, ConstraintStruct cs) : base(axisTree)
		{
			this.KSs = new ArrayList();
			this.cs = cs;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x000B1E6E File Offset: 0x000B006E
		public override bool EndElement(string localname, string URN)
		{
			base.EndElement(localname, URN);
			return this.KSpointer > 0 && base.CurrentDepth == this.lastDepth;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x000B1E94 File Offset: 0x000B0094
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
			int kspointer = this.KSpointer;
			this.KSpointer = kspointer + 1;
			return kspointer;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x000B1F9C File Offset: 0x000B019C
		public KeySequence PopKS()
		{
			ArrayList kss = this.KSs;
			int num = this.KSpointer - 1;
			this.KSpointer = num;
			return ((KSStruct)kss[num]).ks;
		}

		// Token: 0x04000D9E RID: 3486
		private ConstraintStruct cs;

		// Token: 0x04000D9F RID: 3487
		private ArrayList KSs;

		// Token: 0x04000DA0 RID: 3488
		private int KSpointer;
	}
}
