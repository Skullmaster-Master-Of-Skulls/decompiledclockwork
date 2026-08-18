using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020006D2 RID: 1746
	public abstract class MigrationCodeGenerator
	{
		// Token: 0x060045D1 RID: 17873
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
		public abstract ScaffoldedMigration Generate(string migrationId, IEnumerable<MigrationOperation> operations, string sourceModel, string targetModel, string @namespace, string className);

		// Token: 0x060045D2 RID: 17874 RVA: 0x0014928B File Offset: 0x0014748B
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		private static bool AnnotationsExist(MigrationOperation[] operations)
		{
			return operations.OfType<IAnnotationTarget>().Any((IAnnotationTarget o) => o.HasAnnotations);
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x0014930C File Offset: 0x0014750C
		protected virtual IEnumerable<string> GetNamespaces(IEnumerable<MigrationOperation> operations)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(operations, "operations");
			IEnumerable<string> enumerable = this.GetDefaultNamespaces(false);
			MigrationOperation[] array = operations.ToArray<MigrationOperation>();
			if (array.OfType<AddColumnOperation>().Any((AddColumnOperation o) => o.Column.Type == PrimitiveTypeKind.Geography || o.Column.Type == PrimitiveTypeKind.Geometry))
			{
				enumerable = enumerable.Concat(new string[]
				{
					"System.Data.Entity.Spatial"
				});
			}
			if (MigrationCodeGenerator.AnnotationsExist(array))
			{
				enumerable = enumerable.Concat(new string[]
				{
					"System.Collections.Generic",
					"System.Data.Entity.Infrastructure.Annotations"
				});
				enumerable = (from a in this.AnnotationGenerators
				select a.Value into g
				where g != null
				select g).Aggregate(enumerable, (IEnumerable<string> c, Func<AnnotationCodeGenerator> g) => c.Concat(g().GetExtraNamespaces(this.AnnotationGenerators.Keys)));
			}
			return from n in enumerable.Distinct<string>()
			orderby n
			select n;
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x00149434 File Offset: 0x00147634
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		protected virtual IEnumerable<string> GetDefaultNamespaces(bool designer = false)
		{
			List<string> list = new List<string>
			{
				"System.Data.Entity.Migrations"
			};
			if (designer)
			{
				list.Add("System.CodeDom.Compiler");
				list.Add("System.Data.Entity.Migrations.Infrastructure");
				list.Add("System.Resources");
			}
			else
			{
				list.Add("System");
			}
			return from n in list
			orderby n
			select n;
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x001494A8 File Offset: 0x001476A8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public virtual IDictionary<string, Func<AnnotationCodeGenerator>> AnnotationGenerators
		{
			get
			{
				return this._annotationGenerators;
			}
		}

		// Token: 0x0400199E RID: 6558
		private readonly IDictionary<string, Func<AnnotationCodeGenerator>> _annotationGenerators = new Dictionary<string, Func<AnnotationCodeGenerator>>();
	}
}
