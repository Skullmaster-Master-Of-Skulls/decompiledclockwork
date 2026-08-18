// imports Databases.dll;
// imports Common.Core.dll;
// imports Common.ICore.dll;
// imports Common.Public.dll;

#region Includes
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.TPMailMan;
#endregion

namespace ClockWorkDynamicCSharp {
	public class CSharp {
		public static DatabaseLayer Db = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork);
		public static WebSettingManager WebSettingManager = new WebSettingManager(new SettingsOperationContext());
		public static Dictionary<string, object> InputVariables;
		public static DataTable InputTable;

		public DataTable MainScript() {
			return InputTable;
		}

		#region Functions to load report variables and validate report table
		private bool LoadReportBool(string name) {
			return LoadReportVariable<int, bool>(name, x => x == 1, s => s == "1", true);
		}

		private int LoadReportInt(string name) {
			return LoadReportVariable<int, int>(name, x => x, int.Parse);
		}

		private DateTime LoadReportDateTime(string name) {
			return LoadReportVariable<DateTime, DateTime>(name, dt => dt, DateTime.Parse);
		}

		private string LoadReportString(string name) {
			return LoadReportVariable<string, string>(name, s => s, s => s, true);
		}

		private void RequireColumn<T>(string columnName, bool allowNull = true) {
			if (!InputTable.Columns.Contains(columnName)) {
				throw new ArgumentException("Input table missing column \"" + columnName + "\".");
			}
			DataColumn column = InputTable.Columns[columnName];

			if (column.DataType != typeof(T)) {
				throw new ArgumentException("Expected " + typeof(T).Name + " for input table column \"" + columnName + "\", but received " + column.DataType.Name + ".");
			}

			if (!allowNull && column.AllowDBNull) {
				var indexedRows = InputTable.Rows.Cast<DataRow>().Select((row, index) => new {
					IsNull = row[column] == DBNull.Value,
					Index = index
				});
				foreach (var pair in indexedRows) {
					if (pair.IsNull) {
						throw new ArgumentException("Input table column \"" + columnName + "\" is null in row " + pair.Index + "."); 
					}
				}
			}
		}
		#endregion

		#region Functions to encrypt and decrypt
		/* Db.Encryption.Encrypt and Db.Encryption.Decrypt handle nulls differently. Encrypt throws
		 * an ArgumentNullException, but Decrypt returns an empty string. Both of those are probably
		 * wrong, but these methods are at least consistent. */
		public static string Decrypt(byte[] encrypted) {
			return (encrypted == null) ? null : Db.Encryption.Decrypt(encrypted);
		}

		public static byte[] Encrypt(string decrypted) {
			return (decrypted == null) ? null : Db.Encryption.Encrypt(decrypted);
		}
		#endregion

		#region Backend for scripts
		public ReportReturnValue CustomEntry(ReportParameters args) {
			InputTable = args.Table;
			InputVariables = args.Variables.ToDictionary(
				variable => variable.Name.Trim(),
				variable => variable.Value,
				StringComparer.InvariantCultureIgnoreCase
			);


			try {
				return new ReportReturnValue(
					MainScript(),
					InputVariables.Select(kvp => new ReportVariable(kvp.Key, kvp.Value)).ToList()
				);
			} catch (Exception ex) {
				string adminEmail = WebSettingManager.GetSettingValue<string>(Setting.GENERAL_AdminEmail);

				// Report started by server job. There is no user to read the returned DataTable.
				if ((args.WhoAmI == 0) && !String.IsNullOrEmpty(adminEmail)) {
					TPMailResult result = new TechnoPro.Common.Core.EmailManager(new OperationContext()).SendEmail(
						to: adminEmail,
						from: adminEmail,
						subject: "ClockWork server job failed",
						bodytext: null,
						bodyhtml:
							"<p>A ClockWork server failed at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
							" with the error:</p><blockquote>" + ex.Message + "</blockquote>"
					);

					if (result.Status != eTPMailResultStatus.CompletedSuccess) {
						CWLogger.Logger.Error("Problem sending error report to admin at " + adminEmail + ". " + result.ErrorMessage);
					}
				}

				return new ReportReturnValue(
					(new[] {ex.Message, ex.ToString()}).Select(s => new {Error = s}).ToDataTable(),
					InputVariables.Select(kvp => new ReportVariable(kvp.Key, kvp.Value)).ToList()
				);
			}
		}

		private TTo LoadReportVariable<TFrom, TTo>(
			string name,
			Func<TFrom, TTo> convert,
			Func<string, TTo> parse,
			bool allowNull = false
		) {
			string errorTemplate = "Error loading report variable \"" + name + "\". {error}";

			if (!InputVariables.ContainsKey(name)) {
				throw new ArgumentException(errorTemplate.Replace("{message}", "No variable with that name."));
			}
			object raw = InputVariables[name];

			if (raw == null) {
				if (allowNull) {
					return default(TTo);
				}
				throw new ArgumentException(errorTemplate.Replace("{message}", "Variable is null or blank."));
			}
			
			/* A report variable can be the intended type, such as DateTime, if a user runs the
			 * report manually and uses the input form. But if the report is run as a server job,
			 * the report variable is a string, such as "2026-01-01 00:00:00". */
			if (raw is TFrom) {
				try {
					return convert((TFrom) raw);
				} catch (Exception ex) {
					throw new ArgumentException(
						errorTemplate.Replace(
							"{message}",
							"Cannot convert \"{value}\" from {from} to {to}. Details: {details}"
								.Replace("{value}", raw.ToString())
								.Replace("{from}", typeof(TFrom).Name)
								.Replace("{to}", typeof(TTo).Name)
								.Replace("{details}", ex.Message)
						),
						ex
					);
				}
			}

			if (raw is string) {
				try {
					return parse((string) raw);
				} catch (Exception ex) {
					throw new ArgumentException(
						errorTemplate.Replace(
							"{message}",
							"Cannot parse \"{value}\" to {to}. Details: {details}"
								.Replace("{value}", raw.ToString())
								.Replace("{to}", typeof(TTo).Name)
								.Replace("{details}", ex.Message)
						),
						ex
					);
				}
			}

			throw new ArgumentException(
				errorTemplate.Replace(
					"{message}",
					"Expected {from} but received {received}."
						.Replace("{from}", typeof(TFrom).Name)
						.Replace("{received}", raw.GetType().Name)
				)
			);
		}
		#endregion
	}

	#region Extensions
	public static class Extensions {
		/* ClockWork crashes if a DataColumn is created with an unsupported type, so a try-except
		 * around DataTable.Columns.Add doesn't work. Check against a list of types instead. */
		public static readonly Type[] AllowedDataColumnTypes = new [] {
			typeof(bool), typeof(byte), typeof(char), typeof(DateTime), typeof(decimal),typeof(double),
			typeof(Guid), typeof(short), typeof(int), typeof(long), typeof(sbyte), typeof(float),
			typeof(string), typeof(TimeSpan), typeof(ushort), typeof(uint), typeof(ulong)
		};

		/* Convert an IEnumerable to a DataTable, with one column per property.
		 *
		 * (new List<int> {1, 2}).Select(x => new {Original = x, Square = x * x}).ToDataTable()
		 *
		 * | Original | Square |
		 * ---------------------
		 * | 1        | 1      |
		 * | 2        | 4      |  */
		public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable) {
			DataTable table = new DataTable();
			if (enumerable == null) {
				return table;
			}

			PropertyInfo[] properties = typeof(T).GetProperties();

			foreach (PropertyInfo property in properties) {
				Type columnType = property.PropertyType;
				// DataColumn supports nullable forms of all supported types.
				if (columnType.IsGenericType && (columnType.GetGenericTypeDefinition() == typeof(Nullable<>))) {
					columnType = columnType.GetGenericArguments()[0];
				}
				if (!AllowedDataColumnTypes.Contains(columnType)) {
					throw new ArgumentException("DataColumn does not support data type " + columnType.Name);
				}
				table.Columns.Add(property.Name, columnType);
			}

			foreach (T item in enumerable) {
				DataRow row = table.NewRow();
				foreach (PropertyInfo property in properties) {
					row[property.Name] = property.GetValue(item, null) ?? DBNull.Value;
				}
				table.Rows.Add(row);
			}

			return table;
		}

		public static T GetValueOrDefault<T>(this DataRow row, string columnName) {
			if (row[columnName] == DBNull.Value) {
				return default(T);
			}

			return (T) row[columnName];
		}
	}
	#endregion
}
