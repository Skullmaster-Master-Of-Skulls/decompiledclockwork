using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CSharp;
using Microsoft.CSharp.RuntimeBinder;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020006D3 RID: 1747
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class CSharpMigrationCodeGenerator : MigrationCodeGenerator
	{
		// Token: 0x060045DE RID: 17886 RVA: 0x0014978C File Offset: 0x0014798C
		public override ScaffoldedMigration Generate(string migrationId, IEnumerable<MigrationOperation> operations, string sourceModel, string targetModel, string @namespace, string className)
		{
			Check.NotEmpty(migrationId, "migrationId");
			Check.NotNull<IEnumerable<MigrationOperation>>(operations, "operations");
			Check.NotEmpty(targetModel, "targetModel");
			Check.NotEmpty(className, "className");
			className = this.ScrubName(className);
			this._newTableForeignKeys = (from ct in operations.OfType<CreateTableOperation>()
			from cfk in operations.OfType<AddForeignKeyOperation>()
			where ct.Name.EqualsIgnoreCase(cfk.DependentTable)
			select Tuple.Create<CreateTableOperation, AddForeignKeyOperation>(ct, cfk)).ToList<Tuple<CreateTableOperation, AddForeignKeyOperation>>();
			this._newTableIndexes = (from ct in operations.OfType<CreateTableOperation>()
			from ci in operations.OfType<CreateIndexOperation>()
			where ct.Name.EqualsIgnoreCase(ci.Table)
			select Tuple.Create<CreateTableOperation, CreateIndexOperation>(ct, ci)).ToList<Tuple<CreateTableOperation, CreateIndexOperation>>();
			ScaffoldedMigration scaffoldedMigration = new ScaffoldedMigration
			{
				MigrationId = migrationId,
				Language = "cs",
				UserCode = this.Generate(operations, @namespace, className),
				DesignerCode = this.Generate(migrationId, sourceModel, targetModel, @namespace, className)
			};
			if (!string.IsNullOrWhiteSpace(sourceModel))
			{
				scaffoldedMigration.Resources.Add("Source", sourceModel);
			}
			scaffoldedMigration.Resources.Add("Target", targetModel);
			return scaffoldedMigration;
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x00149AAC File Offset: 0x00147CAC
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		protected virtual string Generate(IEnumerable<MigrationOperation> operations, string @namespace, string className)
		{
			Check.NotNull<IEnumerable<MigrationOperation>>(operations, "operations");
			Check.NotEmpty(className, "className");
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (IndentedTextWriter writer = new IndentedTextWriter(stringWriter))
				{
					this.WriteClassStart(@namespace, className, writer, "DbMigration", false, this.GetNamespaces(operations));
					writer.WriteLine("public override void Up()");
					writer.WriteLine("{");
					writer.Indent++;
					operations.Except(from t in this._newTableForeignKeys
					select t.Item2).Except(from t in this._newTableIndexes
					select t.Item2).Each(delegate(dynamic o)
					{
						if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site23 == null)
						{
							CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site23 = CallSite<Action<CallSite, CSharpMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site23.Target(CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site23, this, o, writer);
					});
					writer.Indent--;
					writer.WriteLine("}");
					writer.WriteLine();
					writer.WriteLine("public override void Down()");
					writer.WriteLine("{");
					writer.Indent++;
					operations = (from o in operations
					select o.Inverse into o
					where o != null
					select o).Reverse<MigrationOperation>();
					bool flag = operations.Any((MigrationOperation o) => o is NotSupportedOperation);
					(from o in operations
					where !(o is NotSupportedOperation)
					select o).Each(delegate(dynamic o)
					{
						if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site24 == null)
						{
							CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site24 = CallSite<Action<CallSite, CSharpMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site24.Target(CSharpMigrationCodeGenerator.<Generate>o__SiteContainer22.<>p__Site24, this, o, writer);
					});
					if (flag)
					{
						writer.Write("throw new NotSupportedException(");
						writer.Write(this.Generate(Strings.ScaffoldSprocInDownNotSupported));
						writer.WriteLine(");");
					}
					writer.Indent--;
					writer.WriteLine("}");
					this.WriteClassEnd(@namespace, writer);
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x00149DA4 File Offset: 0x00147FA4
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		protected virtual string Generate(string migrationId, string sourceModel, string targetModel, string @namespace, string className)
		{
			Check.NotEmpty(migrationId, "migrationId");
			Check.NotEmpty(targetModel, "targetModel");
			Check.NotEmpty(className, "className");
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (IndentedTextWriter indentedTextWriter = new IndentedTextWriter(stringWriter))
				{
					indentedTextWriter.WriteLine("// <auto-generated />");
					this.WriteClassStart(@namespace, className, indentedTextWriter, "IMigrationMetadata", true, null);
					indentedTextWriter.Write("private readonly ResourceManager Resources = new ResourceManager(typeof(");
					indentedTextWriter.Write(className);
					indentedTextWriter.WriteLine("));");
					indentedTextWriter.WriteLine();
					this.WriteProperty("Id", this.Quote(migrationId), indentedTextWriter);
					indentedTextWriter.WriteLine();
					this.WriteProperty("Source", (sourceModel == null) ? null : "Resources.GetString(\"Source\")", indentedTextWriter);
					indentedTextWriter.WriteLine();
					this.WriteProperty("Target", "Resources.GetString(\"Target\")", indentedTextWriter);
					this.WriteClassEnd(@namespace, indentedTextWriter);
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x060045E1 RID: 17889 RVA: 0x00149EB8 File Offset: 0x001480B8
		protected virtual void WriteProperty(string name, string value, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("string IMigrationMetadata.");
			writer.WriteLine(name);
			writer.WriteLine("{");
			writer.Indent++;
			writer.Write("get { return ");
			writer.Write(value ?? "null");
			writer.WriteLine("; }");
			writer.Indent--;
			writer.WriteLine("}");
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x00149F47 File Offset: 0x00148147
		protected virtual void WriteClassAttributes(IndentedTextWriter writer, bool designer)
		{
			if (designer)
			{
				writer.WriteLine("[GeneratedCode(\"EntityFramework.Migrations\", \"{0}\")]", typeof(CSharpMigrationCodeGenerator).Assembly().GetInformationalVersion());
			}
		}

		// Token: 0x060045E3 RID: 17891 RVA: 0x00149F90 File Offset: 0x00148190
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
		protected virtual void WriteClassStart(string @namespace, string className, IndentedTextWriter writer, string @base, bool designer = false, IEnumerable<string> namespaces = null)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			Check.NotEmpty(className, "className");
			Check.NotEmpty(@base, "base");
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				writer.Write("namespace ");
				writer.WriteLine(@namespace);
				writer.WriteLine("{");
				writer.Indent++;
			}
			(namespaces ?? this.GetDefaultNamespaces(designer)).Each(delegate(string n)
			{
				writer.WriteLine("using " + n + ";");
			});
			writer.WriteLine();
			this.WriteClassAttributes(writer, designer);
			writer.Write("public ");
			if (designer)
			{
				writer.Write("sealed ");
			}
			writer.Write("partial class ");
			writer.Write(className);
			writer.Write(" : ");
			writer.Write(@base);
			writer.WriteLine();
			writer.WriteLine("{");
			writer.Indent++;
		}

		// Token: 0x060045E4 RID: 17892 RVA: 0x0014A0E0 File Offset: 0x001482E0
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
		protected virtual void WriteClassEnd(string @namespace, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Indent--;
			writer.WriteLine("}");
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				writer.Indent--;
				writer.WriteLine("}");
			}
		}

		// Token: 0x060045E5 RID: 17893 RVA: 0x0014A134 File Offset: 0x00148334
		protected virtual void Generate(AddColumnOperation addColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddColumnOperation>(addColumnOperation, "addColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddColumn(");
			writer.Write(this.Quote(addColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(addColumnOperation.Column.Name));
			writer.Write(", c =>");
			this.Generate(addColumnOperation.Column, writer, false);
			writer.WriteLine(");");
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x0014A1BC File Offset: 0x001483BC
		protected virtual void Generate(DropColumnOperation dropColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropColumnOperation>(dropColumnOperation, "dropColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropColumn(");
			writer.Write(this.Quote(dropColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(dropColumnOperation.Name));
			if (dropColumnOperation.RemovedAnnotations.Any<KeyValuePair<string, object>>())
			{
				writer.Indent++;
				writer.WriteLine(",");
				writer.Write("removedAnnotations: ");
				this.GenerateAnnotations(dropColumnOperation.RemovedAnnotations, writer);
				writer.Indent--;
			}
			writer.WriteLine(");");
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x0014A274 File Offset: 0x00148474
		protected virtual void Generate(AlterColumnOperation alterColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterColumnOperation>(alterColumnOperation, "alterColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AlterColumn(");
			writer.Write(this.Quote(alterColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(alterColumnOperation.Column.Name));
			writer.Write(", c =>");
			this.Generate(alterColumnOperation.Column, writer, false);
			writer.WriteLine(");");
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x0014A300 File Offset: 0x00148500
		protected internal virtual void GenerateAnnotations(IDictionary<string, object> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, object>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("new Dictionary<string, object>");
			writer.WriteLine("{");
			writer.Indent++;
			foreach (string text in from k in annotations.Keys
			orderby k
			select k)
			{
				writer.Write("{ ");
				writer.Write(this.Quote(text) + ", ");
				this.GenerateAnnotation(text, annotations[text], writer);
				writer.WriteLine(" },");
			}
			writer.Indent--;
			writer.Write("}");
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x0014A400 File Offset: 0x00148600
		protected internal virtual void GenerateAnnotations(IDictionary<string, AnnotationValues> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, AnnotationValues>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("new Dictionary<string, AnnotationValues>");
			writer.WriteLine("{");
			writer.Indent++;
			if (annotations != null)
			{
				foreach (string text in from k in annotations.Keys
				orderby k
				select k)
				{
					writer.WriteLine("{ ");
					writer.Indent++;
					writer.WriteLine(this.Quote(text) + ",");
					writer.Write("new AnnotationValues(oldValue: ");
					this.GenerateAnnotation(text, annotations[text].OldValue, writer);
					writer.Write(", newValue: ");
					this.GenerateAnnotation(text, annotations[text].NewValue, writer);
					writer.WriteLine(")");
					writer.Indent--;
					writer.WriteLine("},");
				}
			}
			writer.Indent--;
			writer.Write("}");
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x0014A55C File Offset: 0x0014875C
		protected internal virtual void GenerateAnnotation(string name, object annotation, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (annotation == null)
			{
				writer.Write("null");
				return;
			}
			Func<AnnotationCodeGenerator> func;
			if (this.AnnotationGenerators.TryGetValue(name, out func) && func != null)
			{
				func().Generate(name, annotation, writer);
				return;
			}
			writer.Write(this.Quote(annotation.ToString()));
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x0014A5C4 File Offset: 0x001487C4
		protected virtual void Generate(CreateProcedureOperation createProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateProcedureOperation>(createProcedureOperation, "createProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(createProcedureOperation, "CreateStoredProcedure", writer);
		}

		// Token: 0x060045EC RID: 17900 RVA: 0x0014A5EB File Offset: 0x001487EB
		protected virtual void Generate(AlterProcedureOperation alterProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterProcedureOperation>(alterProcedureOperation, "alterProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(alterProcedureOperation, "AlterStoredProcedure", writer);
		}

		// Token: 0x060045ED RID: 17901 RVA: 0x0014A68C File Offset: 0x0014888C
		private void Generate(ProcedureOperation procedureOperation, string methodName, IndentedTextWriter writer)
		{
			writer.Write(methodName);
			writer.WriteLine("(");
			writer.Indent++;
			writer.Write(this.Quote(procedureOperation.Name));
			writer.WriteLine(",");
			if (procedureOperation.Parameters.Any<ParameterModel>())
			{
				writer.WriteLine("p => new");
				writer.Indent++;
				writer.WriteLine("{");
				writer.Indent++;
				procedureOperation.Parameters.Each(delegate(ParameterModel p)
				{
					string text = this.ScrubName(p.Name);
					writer.Write(text);
					writer.Write(" =");
					this.Generate(p, writer, !string.Equals(p.Name, text, StringComparison.Ordinal));
					writer.WriteLine(",");
				});
				writer.Indent--;
				writer.WriteLine("},");
				writer.Indent--;
			}
			writer.Write("body:");
			if (!string.IsNullOrWhiteSpace(procedureOperation.BodySql))
			{
				writer.WriteLine();
				writer.Indent++;
				string newValue = writer.NewLine + writer.CurrentIndentation() + "  ";
				writer.Write("@");
				writer.WriteLine(this.Generate(procedureOperation.BodySql.Replace(Environment.NewLine, newValue)));
				writer.Indent--;
			}
			else
			{
				writer.WriteLine(" \"\"");
			}
			writer.Indent--;
			writer.WriteLine(");");
			writer.WriteLine();
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x0014A894 File Offset: 0x00148A94
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected virtual void Generate(ParameterModel parameterModel, IndentedTextWriter writer, bool emitName = false)
		{
			Check.NotNull<ParameterModel>(parameterModel, "parameterModel");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write(" p.");
			writer.Write(this.TranslateColumnType(parameterModel.Type));
			writer.Write("(");
			List<string> list = new List<string>();
			if (emitName)
			{
				list.Add("name: " + this.Quote(parameterModel.Name));
			}
			if (parameterModel.MaxLength != null)
			{
				list.Add("maxLength: " + parameterModel.MaxLength);
			}
			byte? precision = parameterModel.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				list.Add("precision: " + parameterModel.Precision);
			}
			byte? scale = parameterModel.Scale;
			int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
			if (num2 != null)
			{
				list.Add("scale: " + parameterModel.Scale);
			}
			if (parameterModel.IsFixedLength != null)
			{
				list.Add("fixedLength: " + parameterModel.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (parameterModel.IsUnicode != null)
			{
				list.Add("unicode: " + parameterModel.IsUnicode.ToString().ToLowerInvariant());
			}
			if (parameterModel.DefaultValue != null)
			{
				if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site43 == null)
				{
					CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site43 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site43.Target;
				CallSite <>p__Site = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site43;
				List<string> arg = list;
				if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site44 == null)
				{
					CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site44 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site44.Target;
				CallSite <>p__Site2 = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site44;
				string arg2 = "defaultValue: ";
				if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site45 == null)
				{
					CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site45 = CallSite<Func<CallSite, CSharpMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__Site, arg, target2(<>p__Site2, arg2, CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site45.Target(CSharpMigrationCodeGenerator.<Generate>o__SiteContainer42.<>p__Site45, this, parameterModel.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.DefaultValueSql))
			{
				list.Add("defaultValueSql: " + this.Quote(parameterModel.DefaultValueSql));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.StoreType))
			{
				list.Add("storeType: " + this.Quote(parameterModel.StoreType));
			}
			if (parameterModel.IsOutParameter)
			{
				list.Add("outParameter: true");
			}
			writer.Write(list.Join(null, ", "));
			writer.Write(")");
		}

		// Token: 0x060045EF RID: 17903 RVA: 0x0014ABF0 File Offset: 0x00148DF0
		protected virtual void Generate(DropProcedureOperation dropProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropProcedureOperation>(dropProcedureOperation, "dropProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropStoredProcedure(");
			writer.Write(this.Quote(dropProcedureOperation.Name));
			writer.WriteLine(");");
		}

		// Token: 0x060045F0 RID: 17904 RVA: 0x0014AD08 File Offset: 0x00148F08
		protected virtual void Generate(CreateTableOperation createTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("CreateTable(");
			writer.Indent++;
			writer.Write(this.Quote(createTableOperation.Name));
			writer.WriteLine(",");
			writer.WriteLine("c => new");
			writer.Indent++;
			writer.WriteLine("{");
			writer.Indent++;
			createTableOperation.Columns.Each(delegate(ColumnModel c)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				writer.WriteLine(",");
			});
			writer.Indent--;
			writer.Write("}");
			writer.Indent--;
			if (createTableOperation.Annotations.Any<KeyValuePair<string, object>>())
			{
				writer.WriteLine(",");
				writer.Write("annotations: ");
				this.GenerateAnnotations(createTableOperation.Annotations, writer);
			}
			writer.Write(")");
			this.GenerateInline(createTableOperation.PrimaryKey, writer);
			(from t in this._newTableForeignKeys
			where t.Item1 == createTableOperation
			select t).Each(delegate(Tuple<CreateTableOperation, AddForeignKeyOperation> t)
			{
				this.GenerateInline(t.Item2, writer);
			});
			(from t in this._newTableIndexes
			where t.Item1 == createTableOperation
			select t).Each(delegate(Tuple<CreateTableOperation, CreateIndexOperation> t)
			{
				this.GenerateInline(t.Item2, writer);
			});
			writer.WriteLine(";");
			writer.Indent--;
			writer.WriteLine();
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x0014AFA0 File Offset: 0x001491A0
		protected internal virtual void Generate(AlterTableOperation alterTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterTableOperation>(alterTableOperation, "alterTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("AlterTableAnnotations(");
			writer.Indent++;
			writer.Write(this.Quote(alterTableOperation.Name));
			writer.WriteLine(",");
			writer.WriteLine("c => new");
			writer.Indent++;
			writer.WriteLine("{");
			writer.Indent++;
			alterTableOperation.Columns.Each(delegate(ColumnModel c)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				writer.WriteLine(",");
			});
			writer.Indent--;
			writer.Write("}");
			writer.Indent--;
			if (alterTableOperation.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
			{
				writer.WriteLine(",");
				writer.Write("annotations: ");
				this.GenerateAnnotations(alterTableOperation.Annotations, writer);
			}
			writer.Write(")");
			writer.WriteLine(";");
			writer.Indent--;
			writer.WriteLine();
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x0014B138 File Offset: 0x00149338
		protected virtual void GenerateInline(AddPrimaryKeyOperation addPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (addPrimaryKeyOperation != null)
			{
				writer.WriteLine();
				writer.Write(".PrimaryKey(");
				this.Generate(addPrimaryKeyOperation.Columns, writer);
				if (!addPrimaryKeyOperation.HasDefaultName)
				{
					writer.Write(", name: ");
					writer.Write(this.Quote(addPrimaryKeyOperation.Name));
				}
				if (!addPrimaryKeyOperation.IsClustered)
				{
					writer.Write(", clustered: false");
				}
				writer.Write(")");
			}
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x0014B1B8 File Offset: 0x001493B8
		protected virtual void GenerateInline(AddForeignKeyOperation addForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine();
			writer.Write(".ForeignKey(" + this.Quote(addForeignKeyOperation.PrincipalTable) + ", ");
			this.Generate(addForeignKeyOperation.DependentColumns, writer);
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete: true");
			}
			writer.Write(")");
		}

		// Token: 0x060045F4 RID: 17908 RVA: 0x0014B230 File Offset: 0x00149430
		protected virtual void GenerateInline(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine();
			writer.Write(".Index(");
			this.Generate(createIndexOperation.Columns, writer);
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.Write(")");
		}

		// Token: 0x060045F5 RID: 17909 RVA: 0x0014B29C File Offset: 0x0014949C
		protected virtual void Generate(IEnumerable<string> columns, IndentedTextWriter writer)
		{
			Check.NotNull<IEnumerable<string>>(columns, "columns");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("t => ");
			if (columns.Count<string>() == 1)
			{
				writer.Write("t." + this.ScrubName(columns.Single<string>()));
				return;
			}
			writer.Write("new { " + columns.Join((string c) => "t." + this.ScrubName(c), ", ") + " }");
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x0014B328 File Offset: 0x00149528
		protected virtual void Generate(AddPrimaryKeyOperation addPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddPrimaryKeyOperation>(addPrimaryKeyOperation, "addPrimaryKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddPrimaryKey(");
			writer.Write(this.Quote(addPrimaryKeyOperation.Table));
			writer.Write(", ");
			bool flag = addPrimaryKeyOperation.Columns.Count<string>() > 1;
			if (flag)
			{
				writer.Write("new[] { ");
			}
			writer.Write(addPrimaryKeyOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			if (!addPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(addPrimaryKeyOperation.Name));
			}
			if (!addPrimaryKeyOperation.IsClustered)
			{
				writer.Write(", clustered: false");
			}
			writer.WriteLine(");");
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x0014B408 File Offset: 0x00149608
		protected virtual void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropPrimaryKeyOperation>(dropPrimaryKeyOperation, "dropPrimaryKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropPrimaryKey(");
			writer.Write(this.Quote(dropPrimaryKeyOperation.Table));
			if (!dropPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(dropPrimaryKeyOperation.Name));
			}
			writer.WriteLine(");");
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x0014B47C File Offset: 0x0014967C
		protected virtual void Generate(AddForeignKeyOperation addForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddForeignKey(");
			writer.Write(this.Quote(addForeignKeyOperation.DependentTable));
			writer.Write(", ");
			bool flag = addForeignKeyOperation.DependentColumns.Count<string>() > 1;
			if (flag)
			{
				writer.Write("new[] { ");
			}
			writer.Write(addForeignKeyOperation.DependentColumns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			writer.Write(", ");
			writer.Write(this.Quote(addForeignKeyOperation.PrincipalTable));
			if (addForeignKeyOperation.PrincipalColumns.Any<string>())
			{
				writer.Write(", ");
				if (flag)
				{
					writer.Write("new[] { ");
				}
				writer.Write(addForeignKeyOperation.PrincipalColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
			}
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete: true");
			}
			if (!addForeignKeyOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(addForeignKeyOperation.Name));
			}
			writer.WriteLine(");");
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x0014B5D0 File Offset: 0x001497D0
		protected virtual void Generate(DropForeignKeyOperation dropForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropForeignKeyOperation>(dropForeignKeyOperation, "dropForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropForeignKey(");
			writer.Write(this.Quote(dropForeignKeyOperation.DependentTable));
			writer.Write(", ");
			if (!dropForeignKeyOperation.HasDefaultName)
			{
				writer.Write(this.Quote(dropForeignKeyOperation.Name));
			}
			else
			{
				bool flag = dropForeignKeyOperation.DependentColumns.Count<string>() > 1;
				if (flag)
				{
					writer.Write("new[] { ");
				}
				writer.Write(dropForeignKeyOperation.DependentColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
				writer.Write(", ");
				writer.Write(this.Quote(dropForeignKeyOperation.PrincipalTable));
			}
			writer.WriteLine(");");
		}

		// Token: 0x060045FA RID: 17914 RVA: 0x0014B6B0 File Offset: 0x001498B0
		protected virtual void Generate(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("CreateIndex(");
			writer.Write(this.Quote(createIndexOperation.Table));
			writer.Write(", ");
			bool flag = createIndexOperation.Columns.Count<string>() > 1;
			if (flag)
			{
				writer.Write("new[] { ");
			}
			writer.Write(createIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.WriteLine(");");
		}

		// Token: 0x060045FB RID: 17915 RVA: 0x0014B760 File Offset: 0x00149960
		private void WriteIndexParameters(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			if (createIndexOperation.IsUnique)
			{
				writer.Write(", unique: true");
			}
			if (createIndexOperation.IsClustered)
			{
				writer.Write(", clustered: true");
			}
			if (!createIndexOperation.HasDefaultName)
			{
				writer.Write(", name: ");
				writer.Write(this.Quote(createIndexOperation.Name));
			}
		}

		// Token: 0x060045FC RID: 17916 RVA: 0x0014B7B8 File Offset: 0x001499B8
		protected virtual void Generate(DropIndexOperation dropIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropIndexOperation>(dropIndexOperation, "dropIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropIndex(");
			writer.Write(this.Quote(dropIndexOperation.Table));
			writer.Write(", ");
			if (!dropIndexOperation.HasDefaultName)
			{
				writer.Write(this.Quote(dropIndexOperation.Name));
			}
			else
			{
				writer.Write("new[] { ");
				writer.Write(dropIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
				writer.Write(" }");
			}
			writer.WriteLine(");");
		}

		// Token: 0x060045FD RID: 17917 RVA: 0x0014B868 File Offset: 0x00149A68
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected virtual void Generate(ColumnModel column, IndentedTextWriter writer, bool emitName = false)
		{
			Check.NotNull<ColumnModel>(column, "column");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write(" c.");
			writer.Write(this.TranslateColumnType(column.Type));
			writer.Write("(");
			List<string> list = new List<string>();
			if (emitName)
			{
				list.Add("name: " + this.Quote(column.Name));
			}
			if (column.IsNullable == false)
			{
				list.Add("nullable: false");
			}
			if (column.MaxLength != null)
			{
				list.Add("maxLength: " + column.MaxLength);
			}
			byte? precision = column.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				list.Add("precision: " + column.Precision);
			}
			byte? scale = column.Scale;
			int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
			if (num2 != null)
			{
				list.Add("scale: " + column.Scale);
			}
			if (column.IsFixedLength != null)
			{
				list.Add("fixedLength: " + column.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (column.IsUnicode != null)
			{
				list.Add("unicode: " + column.IsUnicode.ToString().ToLowerInvariant());
			}
			if (column.IsIdentity)
			{
				list.Add("identity: true");
			}
			if (column.DefaultValue != null)
			{
				if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site53 == null)
				{
					CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site53 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site53.Target;
				CallSite <>p__Site = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site53;
				List<string> arg = list;
				if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site54 == null)
				{
					CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site54 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site54.Target;
				CallSite <>p__Site2 = CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site54;
				string arg2 = "defaultValue: ";
				if (CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site55 == null)
				{
					CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site55 = CallSite<Func<CallSite, CSharpMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(CSharpMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__Site, arg, target2(<>p__Site2, arg2, CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site55.Target(CSharpMigrationCodeGenerator.<Generate>o__SiteContainer52.<>p__Site55, this, column.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(column.DefaultValueSql))
			{
				list.Add("defaultValueSql: " + this.Quote(column.DefaultValueSql));
			}
			if (column.IsTimestamp)
			{
				list.Add("timestamp: true");
			}
			if (!string.IsNullOrWhiteSpace(column.StoreType))
			{
				list.Add("storeType: " + this.Quote(column.StoreType));
			}
			writer.Write(list.Join(null, ", "));
			if (column.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
			{
				writer.Indent++;
				writer.WriteLine(list.Any<string>() ? "," : "");
				writer.Write("annotations: ");
				this.GenerateAnnotations(column.Annotations, writer);
				writer.Indent--;
			}
			writer.Write(")");
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x0014BC57 File Offset: 0x00149E57
		protected virtual string Generate(byte[] defaultValue)
		{
			return "new byte[] {" + defaultValue.Join(null, ", ") + "}";
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x0014BC74 File Offset: 0x00149E74
		protected virtual string Generate(DateTime defaultValue)
		{
			return string.Concat(new object[]
			{
				"new DateTime(",
				defaultValue.Ticks,
				", DateTimeKind.",
				Enum.GetName(typeof(DateTimeKind), defaultValue.Kind),
				")"
			});
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x0014BCD4 File Offset: 0x00149ED4
		protected virtual string Generate(DateTimeOffset defaultValue)
		{
			return string.Concat(new object[]
			{
				"new DateTimeOffset(",
				defaultValue.Ticks,
				", new TimeSpan(",
				defaultValue.Offset.Ticks,
				"))"
			});
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x0014BD2C File Offset: 0x00149F2C
		protected virtual string Generate(decimal defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "m";
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x0014BD44 File Offset: 0x00149F44
		protected virtual string Generate(Guid defaultValue)
		{
			return "new Guid(\"" + defaultValue + "\")";
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x0014BD5B File Offset: 0x00149F5B
		protected virtual string Generate(long defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x0014BD69 File Offset: 0x00149F69
		protected virtual string Generate(float defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "f";
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x0014BD81 File Offset: 0x00149F81
		protected virtual string Generate(string defaultValue)
		{
			return this.Quote(defaultValue);
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x0014BD8A File Offset: 0x00149F8A
		protected virtual string Generate(TimeSpan defaultValue)
		{
			return "new TimeSpan(" + defaultValue.Ticks + ")";
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x0014BDA8 File Offset: 0x00149FA8
		protected virtual string Generate(DbGeography defaultValue)
		{
			return string.Concat(new object[]
			{
				"DbGeography.FromText(\"",
				defaultValue.AsText(),
				"\", ",
				defaultValue.CoordinateSystemId,
				")"
			});
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x0014BDF4 File Offset: 0x00149FF4
		protected virtual string Generate(DbGeometry defaultValue)
		{
			return string.Concat(new object[]
			{
				"DbGeometry.FromText(\"",
				defaultValue.AsText(),
				"\", ",
				defaultValue.CoordinateSystemId,
				")"
			});
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x0014BE40 File Offset: 0x0014A040
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected virtual string Generate(object defaultValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				defaultValue
			}).ToLowerInvariant();
		}

		// Token: 0x0600460A RID: 17930 RVA: 0x0014BE70 File Offset: 0x0014A070
		protected virtual void Generate(DropTableOperation dropTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropTableOperation>(dropTableOperation, "dropTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropTable(");
			writer.Write(this.Quote(dropTableOperation.Name));
			if (dropTableOperation.RemovedAnnotations.Any<KeyValuePair<string, object>>())
			{
				writer.Indent++;
				writer.WriteLine(",");
				writer.Write("removedAnnotations: ");
				this.GenerateAnnotations(dropTableOperation.RemovedAnnotations, writer);
				writer.Indent--;
			}
			IDictionary<string, IDictionary<string, object>> removedColumnAnnotations = dropTableOperation.RemovedColumnAnnotations;
			if (removedColumnAnnotations.Any<KeyValuePair<string, IDictionary<string, object>>>())
			{
				writer.Indent++;
				writer.WriteLine(",");
				writer.Write("removedColumnAnnotations: ");
				writer.WriteLine("new Dictionary<string, IDictionary<string, object>>");
				writer.WriteLine("{");
				writer.Indent++;
				foreach (string text in from k in removedColumnAnnotations.Keys
				orderby k
				select k)
				{
					writer.WriteLine("{");
					writer.Indent++;
					writer.WriteLine(this.Quote(text) + ",");
					this.GenerateAnnotations(removedColumnAnnotations[text], writer);
					writer.WriteLine();
					writer.Indent--;
					writer.WriteLine("},");
				}
				writer.Indent--;
				writer.Write("}");
				writer.Indent--;
			}
			writer.WriteLine(");");
		}

		// Token: 0x0600460B RID: 17931 RVA: 0x0014C044 File Offset: 0x0014A244
		protected virtual void Generate(MoveTableOperation moveTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveTableOperation>(moveTableOperation, "moveTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveTable(name: ");
			writer.Write(this.Quote(moveTableOperation.Name));
			writer.Write(", newSchema: ");
			writer.Write(string.IsNullOrWhiteSpace(moveTableOperation.NewSchema) ? "null" : this.Quote(moveTableOperation.NewSchema));
			writer.WriteLine(");");
		}

		// Token: 0x0600460C RID: 17932 RVA: 0x0014C0C4 File Offset: 0x0014A2C4
		protected virtual void Generate(MoveProcedureOperation moveProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveProcedureOperation>(moveProcedureOperation, "moveProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveStoredProcedure(name: ");
			writer.Write(this.Quote(moveProcedureOperation.Name));
			writer.Write(", newSchema: ");
			writer.Write(string.IsNullOrWhiteSpace(moveProcedureOperation.NewSchema) ? "null" : this.Quote(moveProcedureOperation.NewSchema));
			writer.WriteLine(");");
		}

		// Token: 0x0600460D RID: 17933 RVA: 0x0014C144 File Offset: 0x0014A344
		protected virtual void Generate(RenameTableOperation renameTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameTableOperation>(renameTableOperation, "renameTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameTable(name: ");
			writer.Write(this.Quote(renameTableOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameTableOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x0014C1B0 File Offset: 0x0014A3B0
		protected virtual void Generate(RenameProcedureOperation renameProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameProcedureOperation>(renameProcedureOperation, "renameProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameStoredProcedure(name: ");
			writer.Write(this.Quote(renameProcedureOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameProcedureOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x0014C21C File Offset: 0x0014A41C
		protected virtual void Generate(RenameColumnOperation renameColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameColumnOperation>(renameColumnOperation, "renameColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameColumn(table: ");
			writer.Write(this.Quote(renameColumnOperation.Table));
			writer.Write(", name: ");
			writer.Write(this.Quote(renameColumnOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameColumnOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x0014C2A4 File Offset: 0x0014A4A4
		protected virtual void Generate(RenameIndexOperation renameIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameIndexOperation>(renameIndexOperation, "renameIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameIndex(table: ");
			writer.Write(this.Quote(renameIndexOperation.Table));
			writer.Write(", name: ");
			writer.Write(this.Quote(renameIndexOperation.Name));
			writer.Write(", newName: ");
			writer.Write(this.Quote(renameIndexOperation.NewName));
			writer.WriteLine(");");
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x0014C32C File Offset: 0x0014A52C
		protected virtual void Generate(SqlOperation sqlOperation, IndentedTextWriter writer)
		{
			Check.NotNull<SqlOperation>(sqlOperation, "sqlOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Sql(@");
			writer.Write(this.Quote(sqlOperation.Sql));
			if (sqlOperation.SuppressTransaction)
			{
				writer.Write(", suppressTransaction: true");
			}
			writer.WriteLine(");");
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x0014C38C File Offset: 0x0014A58C
		[SuppressMessage("Microsoft.Security", "CA2141:TransparentMethodsMustNotSatisfyLinkDemandsFxCopRule")]
		protected virtual string ScrubName(string name)
		{
			Check.NotEmpty(name, "name");
			Regex regex = new Regex("[^\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Nd}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Cf}\\p{Pc}\\p{Lm}]");
			name = regex.Replace(name, string.Empty);
			using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider())
			{
				if ((!char.IsLetter(name[0]) && name[0] != '_') || !csharpCodeProvider.IsValidIdentifier(name))
				{
					name = "_" + name;
				}
			}
			return name;
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x0014C410 File Offset: 0x0014A610
		protected virtual string TranslateColumnType(PrimitiveTypeKind primitiveTypeKind)
		{
			switch (primitiveTypeKind)
			{
			case PrimitiveTypeKind.Int16:
				return "Short";
			case PrimitiveTypeKind.Int32:
				return "Int";
			case PrimitiveTypeKind.Int64:
				return "Long";
			default:
				return Enum.GetName(typeof(PrimitiveTypeKind), primitiveTypeKind);
			}
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x0014C45D File Offset: 0x0014A65D
		protected virtual string Quote(string identifier)
		{
			return "\"" + identifier + "\"";
		}

		// Token: 0x040019A5 RID: 6565
		private IEnumerable<Tuple<CreateTableOperation, AddForeignKeyOperation>> _newTableForeignKeys;

		// Token: 0x040019A6 RID: 6566
		private IEnumerable<Tuple<CreateTableOperation, CreateIndexOperation>> _newTableIndexes;

		// Token: 0x02000AE6 RID: 2790
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer22
		{
			// Token: 0x040030A9 RID: 12457
			public static CallSite<Action<CallSite, CSharpMigrationCodeGenerator, object, IndentedTextWriter>> <>p__Site23;

			// Token: 0x040030AA RID: 12458
			public static CallSite<Action<CallSite, CSharpMigrationCodeGenerator, object, IndentedTextWriter>> <>p__Site24;
		}

		// Token: 0x02000AEA RID: 2794
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer42
		{
			// Token: 0x040030B0 RID: 12464
			public static CallSite<Action<CallSite, List<string>, object>> <>p__Site43;

			// Token: 0x040030B1 RID: 12465
			public static CallSite<Func<CallSite, string, object, object>> <>p__Site44;

			// Token: 0x040030B2 RID: 12466
			public static CallSite<Func<CallSite, CSharpMigrationCodeGenerator, object, object>> <>p__Site45;
		}

		// Token: 0x02000AED RID: 2797
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer52
		{
			// Token: 0x040030B8 RID: 12472
			public static CallSite<Action<CallSite, List<string>, object>> <>p__Site53;

			// Token: 0x040030B9 RID: 12473
			public static CallSite<Func<CallSite, string, object, object>> <>p__Site54;

			// Token: 0x040030BA RID: 12474
			public static CallSite<Func<CallSite, CSharpMigrationCodeGenerator, object, object>> <>p__Site55;
		}
	}
}
