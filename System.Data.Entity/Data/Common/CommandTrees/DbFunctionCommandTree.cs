using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003E2 RID: 994
	public sealed class DbFunctionCommandTree : DbCommandTree
	{
		// Token: 0x0600354E RID: 13646 RVA: 0x000CF96C File Offset: 0x000CDB6C
		internal DbFunctionCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, EdmFunction edmFunction, TypeUsage resultType, IEnumerable<KeyValuePair<string, TypeUsage>> parameters) : base(metadata, dataSpace)
		{
			EntityUtil.CheckArgumentNull<EdmFunction>(edmFunction, "edmFunction");
			this._edmFunction = edmFunction;
			this._resultType = resultType;
			List<string> list = new List<string>();
			List<TypeUsage> list2 = new List<TypeUsage>();
			if (parameters != null)
			{
				foreach (KeyValuePair<string, TypeUsage> keyValuePair in parameters)
				{
					list.Add(keyValuePair.Key);
					list2.Add(keyValuePair.Value);
				}
			}
			this._parameterNames = list.AsReadOnly();
			this._parameterTypes = list2.AsReadOnly();
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000CFA14 File Offset: 0x000CDC14
		public EdmFunction EdmFunction
		{
			get
			{
				return this._edmFunction;
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000CFA1C File Offset: 0x000CDC1C
		public TypeUsage ResultType
		{
			get
			{
				return this._resultType;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		internal override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Function;
			}
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000CFA24 File Offset: 0x000CDC24
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			int num;
			for (int idx = 0; idx < this._parameterNames.Count; idx = num + 1)
			{
				yield return new KeyValuePair<string, TypeUsage>(this._parameterNames[idx], this._parameterTypes[idx]);
				num = idx;
			}
			yield break;
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000CFA34 File Offset: 0x000CDC34
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.EdmFunction != null)
			{
				dumper.Dump(this.EdmFunction);
			}
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000CFA4A File Offset: 0x000CDC4A
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x0400179D RID: 6045
		private readonly EdmFunction _edmFunction;

		// Token: 0x0400179E RID: 6046
		private readonly TypeUsage _resultType;

		// Token: 0x0400179F RID: 6047
		private readonly ReadOnlyCollection<string> _parameterNames;

		// Token: 0x040017A0 RID: 6048
		private readonly ReadOnlyCollection<TypeUsage> _parameterTypes;
	}
}
