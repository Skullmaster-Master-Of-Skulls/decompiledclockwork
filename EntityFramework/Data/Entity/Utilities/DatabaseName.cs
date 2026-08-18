using System;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000005 RID: 5
	internal class DatabaseName
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002F9C File Offset: 0x0000119C
		public static DatabaseName Parse(string name)
		{
			Match match = DatabaseName._partExtractor.Match(name.Trim());
			if (!match.Success)
			{
				throw Error.InvalidDatabaseName(name);
			}
			string text = match.Groups["part1"].Value.Replace("]]", "]");
			string text2 = match.Groups["part2"].Value.Replace("]]", "]");
			if (string.IsNullOrWhiteSpace(text2))
			{
				return new DatabaseName(text);
			}
			return new DatabaseName(text2, text);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003029 File Offset: 0x00001229
		public DatabaseName(string name) : this(name, null)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003033 File Offset: 0x00001233
		public DatabaseName(string name, string schema)
		{
			this._name = name;
			this._schema = schema;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003049 File Offset: 0x00001249
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003051 File Offset: 0x00001251
		public string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000305C File Offset: 0x0000125C
		public override string ToString()
		{
			string text = DatabaseName.Escape(this._name);
			if (this._schema != null)
			{
				text = DatabaseName.Escape(this._schema) + "." + text;
			}
			return text;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000309E File Offset: 0x0000129E
		private static string Escape(string name)
		{
			if (name.IndexOfAny(new char[]
			{
				']',
				'[',
				'.'
			}) == -1)
			{
				return name;
			}
			return "[" + name.Replace("]", "]]") + "]";
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030DB File Offset: 0x000012DB
		public bool Equals(DatabaseName other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (string.Equals(other._name, this._name, StringComparison.Ordinal) && string.Equals(other._schema, this._schema, StringComparison.Ordinal)));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000311B File Offset: 0x0000131B
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj.GetType() == typeof(DatabaseName) && this.Equals((DatabaseName)obj)));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003158 File Offset: 0x00001358
		public override int GetHashCode()
		{
			return this._name.GetHashCode() * 397 ^ ((this._schema != null) ? this._schema.GetHashCode() : 0);
		}

		// Token: 0x04000009 RID: 9
		private const string NamePartRegex = "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))";

		// Token: 0x0400000A RID: 10
		private static readonly Regex _partExtractor = new Regex(string.Format(CultureInfo.InvariantCulture, "^{0}(?:\\.{1})?$", new object[]
		{
			string.Format(CultureInfo.InvariantCulture, "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))", new object[]
			{
				1
			}),
			string.Format(CultureInfo.InvariantCulture, "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))", new object[]
			{
				2
			})
		}), RegexOptions.Compiled);

		// Token: 0x0400000B RID: 11
		private readonly string _name;

		// Token: 0x0400000C RID: 12
		private readonly string _schema;
	}
}
