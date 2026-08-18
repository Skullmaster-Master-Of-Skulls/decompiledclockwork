using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000658 RID: 1624
	internal class CodeGen
	{
		// Token: 0x06003F7E RID: 16254 RVA: 0x001228E8 File Offset: 0x00120AE8
		internal static void Process(PlanCompiler compilerState, out List<ProviderCommandInfo> childCommands, out ColumnMap resultColumnMap, out int columnCount)
		{
			CodeGen codeGen = new CodeGen(compilerState);
			codeGen.Process(out childCommands, out resultColumnMap, out columnCount);
		}

		// Token: 0x06003F7F RID: 16255 RVA: 0x00122905 File Offset: 0x00120B05
		private CodeGen(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
		}

		// Token: 0x06003F80 RID: 16256 RVA: 0x00122914 File Offset: 0x00120B14
		private void Process(out List<ProviderCommandInfo> childCommands, out ColumnMap resultColumnMap, out int columnCount)
		{
			PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)this.Command.Root.Op;
			this.m_subCommands = new List<Node>(new Node[]
			{
				this.Command.Root
			});
			childCommands = new List<ProviderCommandInfo>(new ProviderCommandInfo[]
			{
				ProviderCommandInfoUtils.Create(this.Command, this.Command.Root)
			});
			resultColumnMap = this.BuildResultColumnMap(physicalProjectOp);
			columnCount = physicalProjectOp.Outputs.Count;
		}

		// Token: 0x06003F81 RID: 16257 RVA: 0x00122998 File Offset: 0x00120B98
		private ColumnMap BuildResultColumnMap(PhysicalProjectOp projectOp)
		{
			Dictionary<Var, KeyValuePair<int, int>> varToCommandColumnMap = this.BuildVarMap();
			return ColumnMapTranslator.Translate(projectOp.ColumnMap, varToCommandColumnMap);
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x001229BC File Offset: 0x00120BBC
		private Dictionary<Var, KeyValuePair<int, int>> BuildVarMap()
		{
			Dictionary<Var, KeyValuePair<int, int>> dictionary = new Dictionary<Var, KeyValuePair<int, int>>();
			int num = 0;
			foreach (Node node in this.m_subCommands)
			{
				PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)node.Op;
				int num2 = 0;
				foreach (Var key in physicalProjectOp.Outputs)
				{
					KeyValuePair<int, int> value = new KeyValuePair<int, int>(num, num2);
					dictionary[key] = value;
					num2++;
				}
				num++;
			}
			return dictionary;
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06003F83 RID: 16259 RVA: 0x00122A7C File Offset: 0x00120C7C
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x040017B2 RID: 6066
		private readonly PlanCompiler m_compilerState;

		// Token: 0x040017B3 RID: 6067
		private List<Node> m_subCommands;
	}
}
