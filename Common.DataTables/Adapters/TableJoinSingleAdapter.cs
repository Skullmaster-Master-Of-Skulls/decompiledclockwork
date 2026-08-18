using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.DataTables.Entities;

namespace TechnoPro.Common.DataTables.Adapters
{
	// Token: 0x0200000C RID: 12
	public static class TableJoinSingleAdapter
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002F1C File Offset: 0x0000111C
		public static string ConvertToTableJoinSinglesXml(this IList<TableJoinSingle> tableJoinSingles)
		{
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
			object[] array = new object[1];
			array[0] = new XElement("tablejoinsingles", from j in tableJoinSingles
			select new XElement("tablejoinsingle", new object[]
			{
				new XElement("t1", j.Table1Name ?? ""),
				new XElement("c1", j.JoinCol2Name ?? ""),
				new XElement("t2", j.Table2Name ?? ""),
				new XElement("c2", j.JoinCol2Name ?? ""),
				new XElement("newtable", j.NewTableName ?? "")
			}));
			return new XDocument(declaration, array).ToString();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F84 File Offset: 0x00001184
		public static IList<TableJoinSingle> ConvertFromTableJoinSinglesXml(this string Xml)
		{
			if (string.IsNullOrEmpty(Xml))
			{
				return new List<TableJoinSingle>();
			}
			return (from lbl in XDocument.Parse(Xml).Descendants("tablejoinsingle")
			let xT1 = lbl.Element("t1")
			let xC1 = lbl.Element("c1")
			let xT2 = lbl.Element("t2")
			let xC2 = lbl.Element("c2")
			let xNewTable = lbl.Element("newtable")
			select new TableJoinSingle
			{
				Table1Name = ((xT1 == null) ? "" : xT1.Value),
				JoinCol1Name = ((xC1 == null) ? "" : xC1.Value),
				Table2Name = ((xT2 == null) ? "" : xT2.Value),
				JoinCol2Name = ((xC2 == null) ? "" : xC2.Value),
				NewTableName = ((xNewTable == null) ? "" : xNewTable.Value)
			}).ToList<TableJoinSingle>();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003094 File Offset: 0x00001294
		public static IList<TableJoinSingle> ConvertFromTableJoinSinglesString(this string s)
		{
			return (from g in (s ?? "").Split(new string[]
			{
				"\r\n",
				"\n"
			}, StringSplitOptions.None)
			where g != null && g.Trim().Length > 0
			select g).ToList<string>().Select(delegate(string line)
			{
				string[] array = line.Split(new char[]
				{
					','
				});
				string text = (array.Length != 0) ? array[0] : "";
				string text2 = (array.Length > 1) ? array[1] : "";
				string text3 = (array.Length > 2) ? array[2] : "";
				int num = text.IndexOf('.');
				int num2 = text2.IndexOf('.');
				int num3 = text3.IndexOf('.');
				string[] columnsToPull;
				if (num3 < 1)
				{
					columnsToPull = null;
				}
				else
				{
					columnsToPull = (from g in text3.Substring(num3 + 1).Split(new char[]
					{
						','
					}, StringSplitOptions.RemoveEmptyEntries)
					select g.Trim() into h
					where h.Length > 0
					select h).ToArray<string>();
					text3 = text3.Substring(0, num3);
				}
				return new TableJoinSingle((num > 0) ? text.Substring(0, num) : text, (num > 0) ? text.Substring(num + 1) : "", (num2 > 0) ? text2.Substring(0, num2) : "", (num2 > 0) ? text2.Substring(num2 + 1) : ((num > 0) ? text.Substring(num + 1) : ""), text3, columnsToPull);
			}).ToList<TableJoinSingle>();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003119 File Offset: 0x00001319
		public static string ConvertToTableJoinSinglesString(this IList<TableJoinSingle> tableJoinSingles)
		{
			return string.Join("\r\n", (from g in tableJoinSingles
			select string.Concat(new string[]
			{
				g.Table1Name ?? "",
				".",
				g.JoinCol1Name ?? "",
				",",
				g.Table2Name ?? "",
				".",
				g.JoinCol2Name ?? "",
				",",
				g.NewTableName ?? "",
				(g.ColumnsToPull == null || g.ColumnsToPull.Length < 1) ? "" : ("." + string.Join(",", g.ColumnsToPull))
			})).ToArray<string>());
		}
	}
}
