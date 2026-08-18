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
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualBasic;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020006E2 RID: 1762
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	public class VisualBasicMigrationCodeGenerator : MigrationCodeGenerator
	{
		// Token: 0x06004699 RID: 18073 RVA: 0x0014D114 File Offset: 0x0014B314
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
			from cfk in operations.OfType<CreateIndexOperation>()
			where ct.Name.EqualsIgnoreCase(cfk.Table)
			select Tuple.Create<CreateTableOperation, CreateIndexOperation>(ct, cfk)).ToList<Tuple<CreateTableOperation, CreateIndexOperation>>();
			ScaffoldedMigration scaffoldedMigration = new ScaffoldedMigration
			{
				MigrationId = migrationId,
				Language = "vb",
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

		// Token: 0x0600469A RID: 18074 RVA: 0x0014D434 File Offset: 0x0014B634
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
					this.WriteClassStart(@namespace, className, writer, "Inherits DbMigration", false, this.GetNamespaces(operations));
					writer.WriteLine("Public Overrides Sub Up()");
					writer.Indent++;
					operations.Except(from t in this._newTableForeignKeys
					select t.Item2).Except(from t in this._newTableIndexes
					select t.Item2).Each(delegate(dynamic o)
					{
						if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site14 == null)
						{
							VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site14 = CallSite<Action<CallSite, VisualBasicMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site14.Target(VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site14, this, o, writer);
					});
					writer.Indent--;
					writer.WriteLine("End Sub");
					writer.WriteLine();
					writer.WriteLine("Public Overrides Sub Down()");
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
						if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site15 == null)
						{
							VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site15 = CallSite<Action<CallSite, VisualBasicMigrationCodeGenerator, object, IndentedTextWriter>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site15.Target(VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer13.<>p__Site15, this, o, writer);
					});
					if (flag)
					{
						writer.Write("Throw New NotSupportedException(");
						writer.Write(this.Generate(Strings.ScaffoldSprocInDownNotSupported));
						writer.WriteLine(")");
					}
					writer.Indent--;
					writer.WriteLine("End Sub");
					this.WriteClassEnd(@namespace, writer);
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x0014D708 File Offset: 0x0014B908
		[SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
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
					indentedTextWriter.WriteLine("' <auto-generated />");
					this.WriteClassStart(@namespace, className, indentedTextWriter, "Implements IMigrationMetadata", true, null);
					indentedTextWriter.Write("Private ReadOnly Resources As New ResourceManager(GetType(");
					indentedTextWriter.Write(className);
					indentedTextWriter.WriteLine("))");
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

		// Token: 0x0600469C RID: 18076 RVA: 0x0014D81C File Offset: 0x0014BA1C
		protected virtual void WriteProperty(string name, string value, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Private ReadOnly Property IMigrationMetadata_");
			writer.Write(name);
			writer.Write("() As String Implements IMigrationMetadata.");
			writer.WriteLine(name);
			writer.Indent++;
			writer.WriteLine("Get");
			writer.Indent++;
			writer.Write("Return ");
			writer.WriteLine(value ?? "Nothing");
			writer.Indent--;
			writer.WriteLine("End Get");
			writer.Indent--;
			writer.WriteLine("End Property");
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x0014D8D9 File Offset: 0x0014BAD9
		protected virtual void WriteClassAttributes(IndentedTextWriter writer, bool designer)
		{
			if (designer)
			{
				writer.WriteLine("<GeneratedCode(\"EntityFramework.Migrations\", \"{0}\")>", typeof(VisualBasicMigrationCodeGenerator).Assembly().GetInformationalVersion());
			}
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x0014D920 File Offset: 0x0014BB20
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "base")]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
		protected virtual void WriteClassStart(string @namespace, string className, IndentedTextWriter writer, string @base, bool designer = false, IEnumerable<string> namespaces = null)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			Check.NotEmpty(className, "className");
			Check.NotEmpty(@base, "base");
			(namespaces ?? this.GetDefaultNamespaces(designer)).Each(delegate(string n)
			{
				writer.WriteLine("Imports " + n);
			});
			if (!designer)
			{
				writer.WriteLine("Imports Microsoft.VisualBasic");
			}
			writer.WriteLine();
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				writer.Write("Namespace ");
				writer.WriteLine(@namespace);
				writer.Indent++;
			}
			this.WriteClassAttributes(writer, designer);
			writer.Write("Public ");
			if (designer)
			{
				writer.Write("NotInheritable ");
			}
			writer.Write("Partial Class ");
			writer.Write(className);
			writer.WriteLine();
			writer.Indent++;
			writer.WriteLine(@base);
			writer.Indent--;
			writer.WriteLine();
			writer.Indent++;
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x0014DA84 File Offset: 0x0014BC84
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace")]
		protected virtual void WriteClassEnd(string @namespace, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Indent--;
			writer.WriteLine("End Class");
			if (!string.IsNullOrWhiteSpace(@namespace))
			{
				writer.Indent--;
				writer.WriteLine("End Namespace");
			}
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x0014DAD8 File Offset: 0x0014BCD8
		protected virtual void Generate(AddColumnOperation addColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddColumnOperation>(addColumnOperation, "addColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AddColumn(");
			writer.Write(this.Quote(addColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(addColumnOperation.Column.Name));
			writer.Write(", Function(c)");
			this.Generate(addColumnOperation.Column, writer, false);
			writer.WriteLine(")");
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x0014DB60 File Offset: 0x0014BD60
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
				writer.Write("removedAnnotations := ");
				this.GenerateAnnotations(dropColumnOperation.RemovedAnnotations, writer);
				writer.Indent--;
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046A2 RID: 18082 RVA: 0x0014DC18 File Offset: 0x0014BE18
		protected virtual void Generate(AlterColumnOperation alterColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterColumnOperation>(alterColumnOperation, "alterColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("AlterColumn(");
			writer.Write(this.Quote(alterColumnOperation.Table));
			writer.Write(", ");
			writer.Write(this.Quote(alterColumnOperation.Column.Name));
			writer.Write(", Function(c)");
			this.Generate(alterColumnOperation.Column, writer, false);
			writer.WriteLine(")");
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x0014DCA4 File Offset: 0x0014BEA4
		protected internal virtual void GenerateAnnotations(IDictionary<string, object> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, object>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("New Dictionary(Of String, Object)() From _");
			writer.WriteLine("{");
			writer.Indent++;
			string[] array = (from k in annotations.Keys
			orderby k
			select k).ToArray<string>();
			for (int i = 0; i < array.Length; i++)
			{
				writer.Write("{ ");
				writer.Write(this.Quote(array[i]) + ", ");
				this.GenerateAnnotation(array[i], annotations[array[i]], writer);
				writer.WriteLine((i < array.Length - 1) ? " }," : " }");
			}
			writer.Indent--;
			writer.Write("}");
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x0014DD98 File Offset: 0x0014BF98
		protected internal virtual void GenerateAnnotations(IDictionary<string, AnnotationValues> annotations, IndentedTextWriter writer)
		{
			Check.NotNull<IDictionary<string, AnnotationValues>>(annotations, "annotations");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("New Dictionary(Of String, AnnotationValues)() From _");
			writer.WriteLine("{");
			writer.Indent++;
			if (annotations != null)
			{
				string[] array = (from k in annotations.Keys
				orderby k
				select k).ToArray<string>();
				for (int i = 0; i < array.Length; i++)
				{
					writer.WriteLine("{");
					writer.Indent++;
					writer.WriteLine(this.Quote(array[i]) + ",");
					writer.Write("New AnnotationValues(oldValue := ");
					this.GenerateAnnotation(array[i], annotations[array[i]].OldValue, writer);
					writer.Write(", newValue := ");
					this.GenerateAnnotation(array[i], annotations[array[i]].NewValue, writer);
					writer.WriteLine(")");
					writer.Indent--;
					writer.WriteLine((i < array.Length - 1) ? " }," : " }");
				}
			}
			writer.Indent--;
			writer.Write("}");
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x0014DEF0 File Offset: 0x0014C0F0
		protected internal virtual void GenerateAnnotation(string name, object annotation, IndentedTextWriter writer)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (annotation == null)
			{
				writer.Write("Nothing");
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

		// Token: 0x060046A6 RID: 18086 RVA: 0x0014DF58 File Offset: 0x0014C158
		protected virtual void Generate(CreateProcedureOperation createProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateProcedureOperation>(createProcedureOperation, "createProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(createProcedureOperation, "CreateStoredProcedure", writer);
		}

		// Token: 0x060046A7 RID: 18087 RVA: 0x0014DF7F File Offset: 0x0014C17F
		protected virtual void Generate(AlterProcedureOperation alterProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterProcedureOperation>(alterProcedureOperation, "alterProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			this.Generate(alterProcedureOperation, "AlterStoredProcedure", writer);
		}

		// Token: 0x060046A8 RID: 18088 RVA: 0x0014E050 File Offset: 0x0014C250
		private void Generate(ProcedureOperation procedureOperation, string methodName, IndentedTextWriter writer)
		{
			writer.Write(methodName);
			writer.WriteLine("(");
			writer.Indent++;
			writer.Write(this.Quote(procedureOperation.Name));
			writer.WriteLine(",");
			if (procedureOperation.Parameters.Any<ParameterModel>())
			{
				writer.WriteLine("Function(p) New With");
				writer.Indent++;
				writer.WriteLine("{");
				writer.Indent++;
				procedureOperation.Parameters.Each(delegate(ParameterModel p, int i)
				{
					string text = this.ScrubName(p.Name);
					writer.Write(".");
					writer.Write(text);
					writer.Write(" =");
					this.Generate(p, writer, !string.Equals(p.Name, text, StringComparison.Ordinal));
					if (i < procedureOperation.Parameters.Count - 1)
					{
						writer.Write(",");
					}
					writer.WriteLine();
				});
				writer.Indent--;
				writer.WriteLine("},");
				writer.Indent--;
			}
			writer.Write("body :=");
			if (!string.IsNullOrWhiteSpace(procedureOperation.BodySql))
			{
				writer.WriteLine();
				writer.Indent++;
				string newValue = "\" & vbCrLf & _" + writer.NewLine + writer.CurrentIndentation() + "\"";
				writer.WriteLine(this.Generate(procedureOperation.BodySql.Replace(Environment.NewLine, newValue)));
				writer.Indent--;
			}
			else
			{
				writer.WriteLine(" \"\"");
			}
			writer.Indent--;
			writer.WriteLine(")");
			writer.WriteLine();
		}

		// Token: 0x060046A9 RID: 18089 RVA: 0x0014E26C File Offset: 0x0014C46C
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
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
				list.Add("name := " + this.Quote(parameterModel.Name));
			}
			if (parameterModel.MaxLength != null)
			{
				list.Add("maxLength := " + parameterModel.MaxLength);
			}
			byte? precision = parameterModel.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				list.Add("precision := " + parameterModel.Precision);
			}
			byte? scale = parameterModel.Scale;
			int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
			if (num2 != null)
			{
				list.Add("scale := " + parameterModel.Scale);
			}
			if (parameterModel.IsFixedLength != null)
			{
				list.Add("fixedLength := " + parameterModel.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (parameterModel.IsUnicode != null)
			{
				list.Add("unicode := " + parameterModel.IsUnicode.ToString().ToLowerInvariant());
			}
			if (parameterModel.DefaultValue != null)
			{
				if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site34 == null)
				{
					VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site34 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site34.Target;
				CallSite <>p__Site = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site34;
				List<string> arg = list;
				if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site35 == null)
				{
					VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site35 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site35.Target;
				CallSite <>p__Site2 = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site35;
				string arg2 = "defaultValue := ";
				if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site36 == null)
				{
					VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site36 = CallSite<Func<CallSite, VisualBasicMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__Site, arg, target2(<>p__Site2, arg2, VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site36.Target(VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer33.<>p__Site36, this, parameterModel.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.DefaultValueSql))
			{
				list.Add("defaultValueSql := " + this.Quote(parameterModel.DefaultValueSql));
			}
			if (!string.IsNullOrWhiteSpace(parameterModel.StoreType))
			{
				list.Add("storeType := " + this.Quote(parameterModel.StoreType));
			}
			if (parameterModel.IsOutParameter)
			{
				list.Add("outParameter := True");
			}
			writer.Write(list.Join(null, ", "));
			writer.Write(")");
		}

		// Token: 0x060046AA RID: 18090 RVA: 0x0014E5C8 File Offset: 0x0014C7C8
		protected virtual void Generate(DropProcedureOperation dropProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropProcedureOperation>(dropProcedureOperation, "dropProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropStoredProcedure(");
			writer.Write(this.Quote(dropProcedureOperation.Name));
			writer.WriteLine(")");
		}

		// Token: 0x060046AB RID: 18091 RVA: 0x0014E708 File Offset: 0x0014C908
		protected virtual void Generate(CreateTableOperation createTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateTableOperation>(createTableOperation, "createTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("CreateTable(");
			writer.Indent++;
			writer.Write(this.Quote(createTableOperation.Name));
			writer.WriteLine(",");
			writer.WriteLine("Function(c) New With");
			writer.Indent++;
			writer.WriteLine("{");
			writer.Indent++;
			int columnCount = createTableOperation.Columns.Count<ColumnModel>();
			createTableOperation.Columns.Each(delegate(ColumnModel c, int i)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(".");
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				if (i < columnCount - 1)
				{
					writer.Write(",");
				}
				writer.WriteLine();
			});
			writer.Indent--;
			writer.Write("}");
			writer.Indent--;
			if (createTableOperation.Annotations.Any<KeyValuePair<string, object>>())
			{
				writer.WriteLine(",");
				writer.Write("annotations := ");
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
			writer.WriteLine();
			writer.Indent--;
			writer.WriteLine();
		}

		// Token: 0x060046AC RID: 18092 RVA: 0x0014E9D4 File Offset: 0x0014CBD4
		protected internal virtual void Generate(AlterTableOperation alterTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AlterTableOperation>(alterTableOperation, "alterTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine("AlterTableAnnotations(");
			writer.Indent++;
			writer.Write(this.Quote(alterTableOperation.Name));
			writer.WriteLine(",");
			writer.WriteLine("Function(c) New With");
			writer.Indent++;
			writer.WriteLine("{");
			writer.Indent++;
			int columnCount = alterTableOperation.Columns.Count<ColumnModel>();
			alterTableOperation.Columns.Each(delegate(ColumnModel c, int i)
			{
				string text = this.ScrubName(c.Name);
				writer.Write(".");
				writer.Write(text);
				writer.Write(" =");
				this.Generate(c, writer, !string.Equals(c.Name, text, StringComparison.Ordinal));
				if (i < columnCount - 1)
				{
					writer.Write(",");
				}
				writer.WriteLine();
			});
			writer.Indent--;
			writer.Write("}");
			writer.Indent--;
			if (alterTableOperation.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
			{
				writer.WriteLine(",");
				writer.Write("annotations := ");
				this.GenerateAnnotations(alterTableOperation.Annotations, writer);
			}
			writer.Write(")");
			writer.WriteLine();
			writer.Indent--;
			writer.WriteLine();
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x0014EB78 File Offset: 0x0014CD78
		protected virtual void GenerateInline(AddPrimaryKeyOperation addPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			if (addPrimaryKeyOperation != null)
			{
				writer.WriteLine(" _");
				writer.Write(".PrimaryKey(");
				this.Generate(addPrimaryKeyOperation.Columns, writer);
				if (!addPrimaryKeyOperation.HasDefaultName)
				{
					writer.Write(", name := ");
					writer.Write(this.Quote(addPrimaryKeyOperation.Name));
				}
				if (!addPrimaryKeyOperation.IsClustered)
				{
					writer.Write(", clustered := False");
				}
				writer.Write(")");
			}
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x0014EBFC File Offset: 0x0014CDFC
		protected virtual void GenerateInline(AddForeignKeyOperation addForeignKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<AddForeignKeyOperation>(addForeignKeyOperation, "addForeignKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine(" _");
			writer.Write(".ForeignKey(" + this.Quote(addForeignKeyOperation.PrincipalTable) + ", ");
			this.Generate(addForeignKeyOperation.DependentColumns, writer);
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete := True");
			}
			writer.Write(")");
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x0014EC78 File Offset: 0x0014CE78
		protected virtual void GenerateInline(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<CreateIndexOperation>(createIndexOperation, "createIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.WriteLine(" _");
			writer.Write(".Index(");
			this.Generate(createIndexOperation.Columns, writer);
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.Write(")");
		}

		// Token: 0x060046B0 RID: 18096 RVA: 0x0014ECE8 File Offset: 0x0014CEE8
		protected virtual void Generate(IEnumerable<string> columns, IndentedTextWriter writer)
		{
			Check.NotNull<IEnumerable<string>>(columns, "columns");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Function(t) ");
			if (columns.Count<string>() == 1)
			{
				writer.Write("t." + this.ScrubName(columns.Single<string>()));
				return;
			}
			writer.Write("New With { " + columns.Join((string c) => "t." + this.ScrubName(c), ", ") + " }");
		}

		// Token: 0x060046B1 RID: 18097 RVA: 0x0014ED74 File Offset: 0x0014CF74
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
				writer.Write("New String() { ");
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
					writer.Write("New String() { ");
				}
				writer.Write(addForeignKeyOperation.PrincipalColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
			}
			if (addForeignKeyOperation.CascadeDelete)
			{
				writer.Write(", cascadeDelete := True");
			}
			if (!addForeignKeyOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(addForeignKeyOperation.Name));
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046B2 RID: 18098 RVA: 0x0014EEC8 File Offset: 0x0014D0C8
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
					writer.Write("New String() { ");
				}
				writer.Write(dropForeignKeyOperation.DependentColumns.Join(new Func<string, string>(this.Quote), ", "));
				if (flag)
				{
					writer.Write(" }");
				}
				writer.Write(", ");
				writer.Write(this.Quote(dropForeignKeyOperation.PrincipalTable));
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046B3 RID: 18099 RVA: 0x0014EFA8 File Offset: 0x0014D1A8
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
				writer.Write("New String() { ");
			}
			writer.Write(addPrimaryKeyOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			if (!addPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(addPrimaryKeyOperation.Name));
			}
			if (!addPrimaryKeyOperation.IsClustered)
			{
				writer.Write(", clustered := False");
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046B4 RID: 18100 RVA: 0x0014F088 File Offset: 0x0014D288
		protected virtual void Generate(DropPrimaryKeyOperation dropPrimaryKeyOperation, IndentedTextWriter writer)
		{
			Check.NotNull<DropPrimaryKeyOperation>(dropPrimaryKeyOperation, "dropPrimaryKeyOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("DropPrimaryKey(");
			writer.Write(this.Quote(dropPrimaryKeyOperation.Table));
			if (!dropPrimaryKeyOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(dropPrimaryKeyOperation.Name));
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046B5 RID: 18101 RVA: 0x0014F0FC File Offset: 0x0014D2FC
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
				writer.Write("New String() { ");
			}
			writer.Write(createIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
			if (flag)
			{
				writer.Write(" }");
			}
			this.WriteIndexParameters(createIndexOperation, writer);
			writer.WriteLine(")");
		}

		// Token: 0x060046B6 RID: 18102 RVA: 0x0014F1AC File Offset: 0x0014D3AC
		private void WriteIndexParameters(CreateIndexOperation createIndexOperation, IndentedTextWriter writer)
		{
			if (createIndexOperation.IsUnique)
			{
				writer.Write(", unique := True");
			}
			if (createIndexOperation.IsClustered)
			{
				writer.Write(", clustered := True");
			}
			if (!createIndexOperation.HasDefaultName)
			{
				writer.Write(", name := ");
				writer.Write(this.Quote(createIndexOperation.Name));
			}
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x0014F204 File Offset: 0x0014D404
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
				writer.Write("New String() { ");
				writer.Write(dropIndexOperation.Columns.Join(new Func<string, string>(this.Quote), ", "));
				writer.Write(" }");
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x0014F2B4 File Offset: 0x0014D4B4
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
				list.Add("name := " + this.Quote(column.Name));
			}
			if (column.IsNullable == false)
			{
				list.Add("nullable := False");
			}
			if (column.MaxLength != null)
			{
				list.Add("maxLength := " + column.MaxLength);
			}
			byte? precision = column.Precision;
			int? num = (precision != null) ? new int?((int)precision.GetValueOrDefault()) : null;
			if (num != null)
			{
				list.Add("precision := " + column.Precision);
			}
			byte? scale = column.Scale;
			int? num2 = (scale != null) ? new int?((int)scale.GetValueOrDefault()) : null;
			if (num2 != null)
			{
				list.Add("scale := " + column.Scale);
			}
			if (column.IsFixedLength != null)
			{
				list.Add("fixedLength := " + column.IsFixedLength.ToString().ToLowerInvariant());
			}
			if (column.IsUnicode != null)
			{
				list.Add("unicode := " + column.IsUnicode.ToString().ToLowerInvariant());
			}
			if (column.IsIdentity)
			{
				list.Add("identity := True");
			}
			if (column.DefaultValue != null)
			{
				if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site44 == null)
				{
					VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site44 = CallSite<Action<CallSite, List<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, List<string>, object> target = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site44.Target;
				CallSite <>p__Site = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site44;
				List<string> arg = list;
				if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site45 == null)
				{
					VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site45 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, string, object, object> target2 = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site45.Target;
				CallSite <>p__Site2 = VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site45;
				string arg2 = "defaultValue := ";
				if (VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site46 == null)
				{
					VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site46 = CallSite<Func<CallSite, VisualBasicMigrationCodeGenerator, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "Generate", null, typeof(VisualBasicMigrationCodeGenerator), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				target(<>p__Site, arg, target2(<>p__Site2, arg2, VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site46.Target(VisualBasicMigrationCodeGenerator.<Generate>o__SiteContainer43.<>p__Site46, this, column.DefaultValue)));
			}
			if (!string.IsNullOrWhiteSpace(column.DefaultValueSql))
			{
				list.Add("defaultValueSql := " + this.Quote(column.DefaultValueSql));
			}
			if (column.IsTimestamp)
			{
				list.Add("timestamp := True");
			}
			if (!string.IsNullOrWhiteSpace(column.StoreType))
			{
				list.Add("storeType := " + this.Quote(column.StoreType));
			}
			writer.Write(list.Join(null, ", "));
			if (column.Annotations.Any<KeyValuePair<string, AnnotationValues>>())
			{
				writer.Indent++;
				writer.WriteLine(list.Any<string>() ? "," : "");
				writer.Write("annotations := ");
				this.GenerateAnnotations(column.Annotations, writer);
				writer.Indent--;
			}
			writer.Write(")");
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x0014F6A3 File Offset: 0x0014D8A3
		protected virtual string Generate(byte[] defaultValue)
		{
			return "New Byte() {" + defaultValue.Join(null, ", ") + "}";
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x0014F6C0 File Offset: 0x0014D8C0
		protected virtual string Generate(DateTime defaultValue)
		{
			return string.Concat(new object[]
			{
				"New DateTime(",
				defaultValue.Ticks,
				", DateTimeKind.",
				Enum.GetName(typeof(DateTimeKind), defaultValue.Kind),
				")"
			});
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x0014F720 File Offset: 0x0014D920
		protected virtual string Generate(DateTimeOffset defaultValue)
		{
			return string.Concat(new object[]
			{
				"New DateTimeOffset(",
				defaultValue.Ticks,
				", new TimeSpan(",
				defaultValue.Offset.Ticks,
				"))"
			});
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x0014F778 File Offset: 0x0014D978
		protected virtual string Generate(decimal defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "D";
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x0014F790 File Offset: 0x0014D990
		protected virtual string Generate(Guid defaultValue)
		{
			return "New Guid(\"" + defaultValue + "\")";
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x0014F7A7 File Offset: 0x0014D9A7
		protected virtual string Generate(long defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x0014F7B5 File Offset: 0x0014D9B5
		protected virtual string Generate(float defaultValue)
		{
			return defaultValue.ToString(CultureInfo.InvariantCulture) + "F";
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x0014F7CD File Offset: 0x0014D9CD
		protected virtual string Generate(string defaultValue)
		{
			return this.Quote(defaultValue);
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x0014F7D6 File Offset: 0x0014D9D6
		protected virtual string Generate(TimeSpan defaultValue)
		{
			return "New TimeSpan(" + defaultValue.Ticks + ")";
		}

		// Token: 0x060046C2 RID: 18114 RVA: 0x0014F7F4 File Offset: 0x0014D9F4
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

		// Token: 0x060046C3 RID: 18115 RVA: 0x0014F840 File Offset: 0x0014DA40
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

		// Token: 0x060046C4 RID: 18116 RVA: 0x0014F88C File Offset: 0x0014DA8C
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		protected virtual string Generate(object defaultValue)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				defaultValue
			}).ToLowerInvariant();
		}

		// Token: 0x060046C5 RID: 18117 RVA: 0x0014F8BC File Offset: 0x0014DABC
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
				writer.Write("removedAnnotations := ");
				this.GenerateAnnotations(dropTableOperation.RemovedAnnotations, writer);
				writer.Indent--;
			}
			IDictionary<string, IDictionary<string, object>> removedColumnAnnotations = dropTableOperation.RemovedColumnAnnotations;
			if (removedColumnAnnotations.Any<KeyValuePair<string, IDictionary<string, object>>>())
			{
				writer.Indent++;
				writer.WriteLine(",");
				writer.Write("removedColumnAnnotations := ");
				writer.WriteLine("New Dictionary(Of String, IDictionary(Of String, Object)) From _");
				writer.WriteLine("{");
				writer.Indent++;
				string[] array = (from k in removedColumnAnnotations.Keys
				orderby k
				select k).ToArray<string>();
				for (int i = 0; i < array.Length; i++)
				{
					writer.WriteLine("{");
					writer.Indent++;
					writer.WriteLine(this.Quote(array[i]) + ",");
					this.GenerateAnnotations(removedColumnAnnotations[array[i]], writer);
					writer.WriteLine();
					writer.Indent--;
					writer.WriteLine((i < array.Length - 1) ? " }," : " }");
				}
				writer.Indent--;
				writer.Write("}");
				writer.Indent--;
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046C6 RID: 18118 RVA: 0x0014FA84 File Offset: 0x0014DC84
		protected virtual void Generate(MoveTableOperation moveTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveTableOperation>(moveTableOperation, "moveTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveTable(name := ");
			writer.Write(this.Quote(moveTableOperation.Name));
			writer.Write(", newSchema := ");
			writer.Write(string.IsNullOrWhiteSpace(moveTableOperation.NewSchema) ? "Nothing" : this.Quote(moveTableOperation.NewSchema));
			writer.WriteLine(")");
		}

		// Token: 0x060046C7 RID: 18119 RVA: 0x0014FB04 File Offset: 0x0014DD04
		protected virtual void Generate(MoveProcedureOperation moveProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<MoveProcedureOperation>(moveProcedureOperation, "moveProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("MoveStoredProcedure(name := ");
			writer.Write(this.Quote(moveProcedureOperation.Name));
			writer.Write(", newSchema := ");
			writer.Write(string.IsNullOrWhiteSpace(moveProcedureOperation.NewSchema) ? "Nothing" : this.Quote(moveProcedureOperation.NewSchema));
			writer.WriteLine(")");
		}

		// Token: 0x060046C8 RID: 18120 RVA: 0x0014FB84 File Offset: 0x0014DD84
		protected virtual void Generate(RenameTableOperation renameTableOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameTableOperation>(renameTableOperation, "renameTableOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameTable(name := ");
			writer.Write(this.Quote(renameTableOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameTableOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060046C9 RID: 18121 RVA: 0x0014FBF0 File Offset: 0x0014DDF0
		protected virtual void Generate(RenameProcedureOperation renameProcedureOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameProcedureOperation>(renameProcedureOperation, "renameProcedureOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameStoredProcedure(name := ");
			writer.Write(this.Quote(renameProcedureOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameProcedureOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x0014FC5C File Offset: 0x0014DE5C
		protected virtual void Generate(RenameColumnOperation renameColumnOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameColumnOperation>(renameColumnOperation, "renameColumnOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameColumn(table := ");
			writer.Write(this.Quote(renameColumnOperation.Table));
			writer.Write(", name := ");
			writer.Write(this.Quote(renameColumnOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameColumnOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060046CB RID: 18123 RVA: 0x0014FCE4 File Offset: 0x0014DEE4
		protected virtual void Generate(RenameIndexOperation renameIndexOperation, IndentedTextWriter writer)
		{
			Check.NotNull<RenameIndexOperation>(renameIndexOperation, "renameIndexOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("RenameIndex(table := ");
			writer.Write(this.Quote(renameIndexOperation.Table));
			writer.Write(", name := ");
			writer.Write(this.Quote(renameIndexOperation.Name));
			writer.Write(", newName := ");
			writer.Write(this.Quote(renameIndexOperation.NewName));
			writer.WriteLine(")");
		}

		// Token: 0x060046CC RID: 18124 RVA: 0x0014FD6C File Offset: 0x0014DF6C
		protected virtual void Generate(SqlOperation sqlOperation, IndentedTextWriter writer)
		{
			Check.NotNull<SqlOperation>(sqlOperation, "sqlOperation");
			Check.NotNull<IndentedTextWriter>(writer, "writer");
			writer.Write("Sql(");
			writer.Write(this.Quote(sqlOperation.Sql));
			if (sqlOperation.SuppressTransaction)
			{
				writer.Write(", suppressTransaction := True");
			}
			writer.WriteLine(")");
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x0014FDCC File Offset: 0x0014DFCC
		[SuppressMessage("Microsoft.Security", "CA2141:TransparentMethodsMustNotSatisfyLinkDemandsFxCopRule")]
		protected virtual string ScrubName(string name)
		{
			Check.NotEmpty(name, "name");
			Regex regex = new Regex("[^\\p{Ll}\\p{Lu}\\p{Lt}\\p{Lo}\\p{Nd}\\p{Nl}\\p{Mn}\\p{Mc}\\p{Cf}\\p{Pc}\\p{Lm}]");
			name = regex.Replace(name, string.Empty);
			using (VBCodeProvider vbcodeProvider = new VBCodeProvider())
			{
				if ((!char.IsLetter(name[0]) && name[0] != '_') || !vbcodeProvider.IsValidIdentifier(name))
				{
					name = "_" + name;
				}
			}
			return name;
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x0014FE50 File Offset: 0x0014E050
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

		// Token: 0x060046CF RID: 18127 RVA: 0x0014FE9D File Offset: 0x0014E09D
		protected virtual string Quote(string identifier)
		{
			return "\"" + identifier + "\"";
		}

		// Token: 0x040019DF RID: 6623
		private IEnumerable<Tuple<CreateTableOperation, AddForeignKeyOperation>> _newTableForeignKeys;

		// Token: 0x040019E0 RID: 6624
		private IEnumerable<Tuple<CreateTableOperation, CreateIndexOperation>> _newTableIndexes;

		// Token: 0x02000AEF RID: 2799
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer13
		{
			// Token: 0x040030BC RID: 12476
			public static CallSite<Action<CallSite, VisualBasicMigrationCodeGenerator, object, IndentedTextWriter>> <>p__Site14;

			// Token: 0x040030BD RID: 12477
			public static CallSite<Action<CallSite, VisualBasicMigrationCodeGenerator, object, IndentedTextWriter>> <>p__Site15;
		}

		// Token: 0x02000AF3 RID: 2803
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer33
		{
			// Token: 0x040030C4 RID: 12484
			public static CallSite<Action<CallSite, List<string>, object>> <>p__Site34;

			// Token: 0x040030C5 RID: 12485
			public static CallSite<Func<CallSite, string, object, object>> <>p__Site35;

			// Token: 0x040030C6 RID: 12486
			public static CallSite<Func<CallSite, VisualBasicMigrationCodeGenerator, object, object>> <>p__Site36;
		}

		// Token: 0x02000AF6 RID: 2806
		[CompilerGenerated]
		private static class <Generate>o__SiteContainer43
		{
			// Token: 0x040030CE RID: 12494
			public static CallSite<Action<CallSite, List<string>, object>> <>p__Site44;

			// Token: 0x040030CF RID: 12495
			public static CallSite<Func<CallSite, string, object, object>> <>p__Site45;

			// Token: 0x040030D0 RID: 12496
			public static CallSite<Func<CallSite, VisualBasicMigrationCodeGenerator, object, object>> <>p__Site46;
		}
	}
}
