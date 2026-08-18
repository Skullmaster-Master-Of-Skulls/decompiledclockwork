imports Databases.dll;
imports Common.Core.dll;
imports Common.ICore.dll;
imports Common.Public.dll;

#region Includes
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection; // PropertyInfo
using System.Text;
using Databases; // DatabaseLayer
using TechnoPro.Common.DynamicCompiler.CompilerArgs.Reports; // ReportParameters, ReportReturnValue
using Entities = TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities; // OperationContext
#endregion

/* TechnoPro's intended entry point is ClockWorkDynamicCSharp.CSharp.CustomEntry(ReportParameters).
 * That method is co-opted to
 *     1) process report variables at beginning and end of script, and
 *     2) catch errors, log them, and present them in a DataTable..
 *
 * The effective entry point is ClockWorkDynamicCSharp.CSharp.MainScript(). The table from the
 * previous the report steps (if any) is in CSharp.InputTable.
 *
 * The report variables are in CSharp.ReportVariables, but you should access them with the
 * Load*Variable methods because:
 *     1) ClockWork stores report variables as object, so you have to cast them,
 *     2) ClockWork stores unchecked checkboxes as null, not false,
 *     3) ClockWork stores empty text boxes as null, not empty string.
 *     4) ClockWork doesn't provide a numeric text entry control.
 *
 * Use LoadVariable(string, Func<>) to parse more complex report variables. For example, to parse a
 * text box containing a comma-and-whitespace-separated list of examIDs:
 *     LoadVariable<string, List<int>>(
 *         "examIDs",
 *         strs => strs.Split(',').Select(str => Int32.Parse(str.Trim())).ToList()
 *     )
 *
 * The contents of ReportVariables are exported as report variables at the end of the script.
 *
 * CSharp.Db is a database connection. Use its ExecuteQuery, ExecuteNonQuery, ExecuteScalar, and
 * GetParameter methods. Alternatively, use Db.ConnectionString to create a new SqlConnection.
 *
 * Use CSharp.Encrypt and CSharp.Decrypt to encrypt and decrypt data coming from and going to the
 * database.
 */
namespace ClockWorkDynamicCSharp {
	public class CSharp {
		public static readonly OperationContext Context = new OperationContext();
		public static readonly DatabaseLayer Db = DatabaseLayerFactory.GetDatabaseLayer(
			eDatabaseConnectionStringName.ClockWork,
			Context.TenantId
		);

		private Dictionary<string, object> ReportVariables;
		private DataTable InputTable;

		public DataTable MainScript() {
			// Require specific columns on InputTable. You can also specify a column data type.
			// However, all columns allow nulls, stored as DBNull.Value.
			RequireColumn("required");
			RequireColumn<int>("required int");

			// Report variable names are case-insensitive. ReportVariables["foo"] and
			// ReportVariables["FOo"] are the same.
			bool checkbox = LoadCheckboxVariable("checkbox");
			string textbox = LoadTextVariable("textbox");
			int examID = LoadIDVariable("examID");

			DataTable output = new DataTable();

			return output;
		}

		#region Backend for scripts
		// Program entry point. Process the table and report variables in the arugment,
		// run MainScript, log and present any errors, and export the result DataTable and report
		// variables.
		public ReportReturnValue CustomEntry(ReportParameters args) {
			SetupVariables(args.Variables);
			if (args.Table != null) {
				InputTable = args.Table;
			}

			DataTable result;
			try {
				result = MainScript();
			} catch (Exception ex) {
				ClockWorkLogger.CWLogger.Logger.Error(ex.Message);
				result = new DataTable();
				result.Columns.Add("Error");
				result.Rows.Add(ex.Message);
				result.Rows.Add(ex.ToString());
			}

			return new ReportReturnValue(
				result,
				ReportVariables.Select(kvp => new ReportVariable(kvp.Key, kvp.Value)).ToList<ReportVariable>()
			);
		}

		// Load values from specific types of controls.
		public bool LoadCheckboxVariable(string name) {
			return LoadVariable<int?, bool>(name, x => x == 1);
		}

		public int LoadListVariable(string name) {
			return LoadVariable<int>(name);
		}

		public string LoadTextVariable(string name) {
			return LoadVariable<string, string>(name, s => (s == null) ? "" : s);
		}

		public int LoadIDVariable(string name) {
			return LoadVariable<string, int>(name, Int32.Parse);
		}

		// General-purpose methods to load values from report form controls.
		public T LoadVariable<T>(string name) {
			if (!ReportVariables.ContainsKey(name)) {
				throw new KeyNotFoundException(String.Format(
					"Report parameter '{0}' is missing",
					name
				));
			}

			try {
				return (T) ReportVariables[name];
			} catch (InvalidCastException) {
				throw new ArgumentException(String.Format(
					"Report parameter '{0}' expected type {1} but received {2}",
					name,
					typeof(T).Name,
					ReportVariables[name].GetType().Name
				));
			}
		}

		public TTo LoadVariable<TFrom, TTo>(string name, Func<TFrom, TTo> convert) {
			try {
				return convert(LoadVariable<TFrom>(name));
			} catch (FormatException ex) {
				throw new FormatException(String.Format(
					"Error converting report parameter '{0}' to {1}: {2}",
					name,
					typeof(TTo).Name,
					ex.Message
				));
			}
		}

		// Require InputTable contain a column with a given name, and optionally a given data type.
		public void RequireColumn(string name) {
			if (InputTable == null) {
				throw new Exception("Script did not receive an input DataTable");
			}
			if (!InputTable.Columns.Contains(name)) {
				throw new ArgumentException(String.Format(
					"Required input table column missing: {0}",
					name
				));
			}
		}

		public void RequireColumn<T>(string name) {
			RequireColumn(name);
			Type received = InputTable.Columns[name].DataType;
			if (received != typeof(T)) {
				throw new ArgumentException(String.Format(
					"Input table column '{0}' must have type {1}  but received {2}",
					name,
					typeof(T).Name,
					received.Name
				));
			}
		}

		// Convert the args.Variables list into a name-value dictionary.
		private void SetupVariables(IList<ReportVariable> variables) {
			ReportVariables = variables.ToDictionary(
				variable => variable.Name.Trim(),
				variable => variable.Value,
				StringComparer.InvariantCultureIgnoreCase
			);
		}

		// Db.Encryption.Decrypt incorrectly decrypts null to empty string.
		public string Decrypt(byte[] encrypted) {
			if (encrypted == null) {
				return (string) null;
			}

			return Db.Encryption.Decrypt(encrypted);
		}

		// Db.Encryption.Encrypt incorrectly throws an ArgumentNullException.
		public byte[] Encrypt(string decrypted) {
			if (decrypted == null) {
				return (byte[]) null;
			}

			return Db.Encryption.Encrypt(decrypted);
		}
		#endregion
	}
}

#region Extensions and utilities
public static class HtmlTools {
	// Create an opening tag for an element with certain attributes.
	//
	// Empty attributes are usually useless, but are sometimes necessary, such as for decorative
	// image alt text. Empty attributes will be included only if skipEmptyAttributes is false.
	//
	// OpenTag("img", new Dictionary<string, string> {{"src", "foo.jpg"}, {"alt", ""}}, false)
	// <img src="foo.jpg" alt="">
	//
	// OpenTag("td", new Dictionary<string, string> {{"style", "border: 1"}, {"scope", ""}}, true)
	// <td style="border: 1">
	public static string OpenTag(
		string elementName,
		Dictionary<string, string> attributes,
		bool skipEmptyAttributes
	) {
		return String.Format(
			"<{0}{1}>",
			elementName,
			attributes.
				Where(kvp => (kvp.Value.Length > 0) || !skipEmptyAttributes).
				Select(kvp => String.Format(" {0}=\"{1}\"", kvp.Key, kvp.Value))
		);
	}

	// Mainly for ToHtmlList extension method in LinqExtensions.
	public enum ListType {
		UL,
		OL
	}

	// Styled tables are common in emails. In particular, zebra-shading rows is very common and
	// requires separate styles for odd and even rows.
	public class HtmlTableProperties {
		public Dictionary<string, string> TableAttributes = new Dictionary<string, string>();
		public string TrStyle = "";
		public string OddTrStyle = "";
		public string EvenTrStyle = "";
		public string TdStyle = "";
		public string ThStyle = "";

		public Dictionary<string, string> ThAttributes() {
			return new Dictionary<string, string>() { { "style", ThStyle } };
		}

		public Dictionary<string, string> OddTrAttributes() {
			return new Dictionary<string, string>() {
				{ "style", StringTools.JoinNonEmpty(";", TrStyle, OddTrStyle) }
			};
		}

		public Dictionary<string, string> EvenTrAttributes() {
			return new Dictionary<string, string>() {
				{ "style", StringTools.JoinNonEmpty(";", TrStyle, EvenTrStyle) }
			};
		}

		public Dictionary<string, string> TdAttributes() {
			return new Dictionary<string, string>() { { "style", TdStyle } };
		}
	}
}

public static class DataTableExtensions {
	public static string ToHtml(this DataTable table, HtmlTools.HtmlTableProperties props) {
		var html = new StringBuilder();

		html.AppendLine(HtmlTools.OpenTag("table", props.TableAttributes, true));
		html.AppendLine("<thead>");

		foreach (DataColumn col in table.Columns) {
			html.Append(HtmlTools.OpenTag("th", props.ThAttributes(), true));
			html.Append(col.ColumnName);
			html.AppendLine("</th>");
		}

		html.AppendLine("</thead>");
		html.AppendLine("<tbody>");

		for (int rowIndex = 0; rowIndex < table.Rows.Count; rowIndex++) {
			if (rowIndex % 2 == 0) {
				html.AppendLine(HtmlTools.OpenTag("tr", props.OddTrAttributes(), true));
			} else {
				html.AppendLine(HtmlTools.OpenTag("tr", props.EvenTrAttributes(), true));
			}

			foreach (DataColumn col in table.Columns) {
				html.Append(HtmlTools.OpenTag("td", props.TdAttributes(), true));
				html.Append(table.Rows[rowIndex][col].ToString());
				html.AppendLine("</td>");
			}

			html.AppendLine("</tr>");
		}

		html.AppendLine("</tbody>");
		html.Append("</table>");

		return html.ToString();
	}

	public static string ToHtml(this DataTable table) {
		return table.ToHtml(new HtmlTools.HtmlTableProperties());
	}

	public static T GetValueOrDefault<T>(this DataRow row, string columnName) {
		if (row[columnName] == DBNull.Value) {
			return default(T);
		}

		return (T) row[columnName];
	}
}

public static class StringTools {
	public static string JoinNonEmpty(string separator, params string[] strings) {
		return String.Join(separator, strings.Where(s => s != ""));
	}
}

public static class LinqExtensions {
	// Convert an IEnumerable to a DataTable, with one column per property.
	//
	// (new List<int> {1, 2, 3}).Select(x => new {Original = x, Square = x * x}).ToDataTable()
	// | Original | Square |
	// ---------------------
	// | 1        | 1      |
	// | 2        | 4      |
	// | 3        | 9      |
	// ---------------------
	//
	// Adapted from https://www.c-sharpcorner.com/uploadfile/VIMAL.LAKHERA/convert-a-linq-query-resultset-to-a-datatable/
	public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable) {
	  DataTable table = new DataTable();
	  if (enumerable == null) {
		return table;
	  }

	  PropertyInfo[] properties = null;
	  foreach (T item in enumerable) {
		// Create columns on first iteration.
		if (properties == null) {
		  properties = item.GetType().GetProperties();

		  foreach (PropertyInfo property in properties) {
			Type columnType = property.PropertyType;

			if (columnType.IsGenericType
				&& (columnType.GetGenericTypeDefinition() == typeof(Nullable<>))) {
			  columnType = columnType.GetGenericArguments()[0];
			}

			table.Columns.Add(property.Name, columnType);
		  }
		}

		DataRow row = table.NewRow();
		foreach (PropertyInfo property in properties) {
		  var value = property.GetValue(item, null);
		  row[property.Name] =  (value == null) ? DBNull.Value : value;
		}
		table.Rows.Add(row);
	  }

	  return table;
	}

	// Convert an IEnumerable to an HTML list.
	//
	// (new List<string> {"foo", "bar"}).ToHtmlList(HtmlTools.ListType.OL)
	// "<ol>\n<li>foo</li>\n<li>bar</li>\n</ol>"
	//
	// (new List<string>()).ToHtmlList(HtmlTools.ListType.UL, true)
	// ""

	// (new List<string>()).ToHtmlList(HtmlTools.ListType.UL)
	// "<ul>\n</ul>"
	public static string ToHtmlList<T>(
		this IEnumerable<T> enumerable,
		HtmlTools.ListType listType,
		bool emptyIfEmpty = false
	) {
		if (emptyIfEmpty && (enumerable.Count() == 0)) {
			return "";
		}

		StringBuilder html = new StringBuilder();

		if (listType == HtmlTools.ListType.UL) {
			html.AppendLine("<ul>");
		} else if (listType == HtmlTools.ListType.OL) {
			html.AppendLine("<ol>");
		} else {
			throw new ArgumentException(
				"Unexpected HtmlTools.ListType value: "
				+ System.Enum.GetName(typeof(HtmlTools.ListType), listType)
			);
		}

		foreach (T item in enumerable) {
			html.Append("<li>");
			html.Append(item.ToString());
			html.AppendLine("</li>");
		}

		if (listType == HtmlTools.ListType.UL) {
			html.AppendLine("</ul>");
		} else if (listType == HtmlTools.ListType.OL) {
			html.AppendLine("</ol>");
		}

		return html.ToString();
	}
}
#endregion