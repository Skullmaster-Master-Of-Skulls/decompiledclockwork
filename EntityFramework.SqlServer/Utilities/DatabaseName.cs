using System;
using System.Data.Entity.SqlServer.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000009 RID: 9
	internal class DatabaseName
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000036D4 File Offset: 0x000018D4
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

		// Token: 0x06000065 RID: 101 RVA: 0x00003761 File Offset: 0x00001961
		public DatabaseName(string name) : this(name, null)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000376B File Offset: 0x0000196B
		public DatabaseName(string name, string schema)
		{
			this._name = name;
			this._schema = schema;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003781 File Offset: 0x00001981
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003789 File Offset: 0x00001989
		public string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003794 File Offset: 0x00001994
		public override string ToString()
		{
			string text = DatabaseName.Escape(this._name);
			if (this._schema != null)
			{
				text = DatabaseName.Escape(this._schema) + "." + text;
			}
			return text;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000037D6 File Offset: 0x000019D6
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

		// Token: 0x0600006B RID: 107 RVA: 0x00003813 File Offset: 0x00001A13
		public bool Equals(DatabaseName other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (string.Equals(other._name, this._name, StringComparison.Ordinal) && string.Equals(other._schema, this._schema, StringComparison.Ordinal)));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003853 File Offset: 0x00001A53
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (obj.GetType() == typeof(DatabaseName) && this.Equals((DatabaseName)obj)));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003890 File Offset: 0x00001A90
		public override int GetHashCode()
		{
			return this._name.GetHashCode() * 397 ^ ((this._schema != null) ? this._schema.GetHashCode() : 0);
		}

		// Token: 0x0400000C RID: 12
		private const string NamePartRegex = "(?:(?:\\[(?<part{0}>(?:(?:\\]\\])|[^\\]])+)\\])|(?<part{0}>[^\\.\\[\\]]+))";

		// Token: 0x0400000D RID: 13
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

		// Token: 0x0400000E RID: 14
		private readonly string _name;

		// Token: 0x0400000F RID: 15
		private readonly string _schema;
	}
}
