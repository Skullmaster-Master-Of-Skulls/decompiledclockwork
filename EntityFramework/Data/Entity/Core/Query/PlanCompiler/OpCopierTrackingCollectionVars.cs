using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200068A RID: 1674
	internal class OpCopierTrackingCollectionVars : OpCopier
	{
		// Token: 0x06004200 RID: 16896 RVA: 0x00137562 File Offset: 0x00135762
		private OpCopierTrackingCollectionVars(Command cmd) : base(cmd)
		{
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x00137578 File Offset: 0x00135778
		internal static Node Copy(Command cmd, Node n, out VarMap varMap, out Dictionary<Var, Node> newCollectionVarDefinitions)
		{
			OpCopierTrackingCollectionVars opCopierTrackingCollectionVars = new OpCopierTrackingCollectionVars(cmd);
			Node result = opCopierTrackingCollectionVars.CopyNode(n);
			varMap = opCopierTrackingCollectionVars.m_varMap;
			newCollectionVarDefinitions = opCopierTrackingCollectionVars.m_newCollectionVarDefinitions;
			return result;
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x001375A8 File Offset: 0x001357A8
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			Node node = base.Visit(op, n);
			MultiStreamNestOp multiStreamNestOp = (MultiStreamNestOp)node.Op;
			for (int i = 0; i < multiStreamNestOp.CollectionInfo.Count; i++)
			{
				this.m_newCollectionVarDefinitions.Add(multiStreamNestOp.CollectionInfo[i].CollectionVar, node.Children[i + 1]);
			}
			return node;
		}

		// Token: 0x04001872 RID: 6258
		private readonly Dictionary<Var, Node> m_newCollectionVarDefinitions = new Dictionary<Var, Node>();
	}
}
