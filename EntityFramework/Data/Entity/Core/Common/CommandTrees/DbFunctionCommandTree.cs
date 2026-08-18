using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000113 RID: 275
	public sealed class DbFunctionCommandTree : DbCommandTree
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x00026E44 File Offset: 0x00025044
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public DbFunctionCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, EdmFunction edmFunction, TypeUsage resultType, IEnumerable<KeyValuePair<string, TypeUsage>> parameters) : base(metadata, dataSpace, true)
		{
			Check.NotNull<EdmFunction>(edmFunction, "edmFunction");
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
			this._parameterNames = new ReadOnlyCollection<string>(list);
			this._parameterTypes = new ReadOnlyCollection<TypeUsage>(list2);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00026EEC File Offset: 0x000250EC
		public EdmFunction EdmFunction
		{
			get
			{
				return this._edmFunction;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00026EF4 File Offset: 0x000250F4
		public TypeUsage ResultType
		{
			get
			{
				return this._resultType;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00026EFC File Offset: 0x000250FC
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Function;
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00027028 File Offset: 0x00025228
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			for (int idx = 0; idx < this._parameterNames.Count; idx++)
			{
				yield return new KeyValuePair<string, TypeUsage>(this._parameterNames[idx], this._parameterTypes[idx]);
			}
			yield break;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00027045 File Offset: 0x00025245
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.EdmFunction != null)
			{
				dumper.Dump(this.EdmFunction);
			}
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0002705B File Offset: 0x0002525B
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x04000246 RID: 582
		private readonly EdmFunction _edmFunction;

		// Token: 0x04000247 RID: 583
		private readonly TypeUsage _resultType;

		// Token: 0x04000248 RID: 584
		private readonly ReadOnlyCollection<string> _parameterNames;

		// Token: 0x04000249 RID: 585
		private readonly ReadOnlyCollection<TypeUsage> _parameterTypes;
	}
}
