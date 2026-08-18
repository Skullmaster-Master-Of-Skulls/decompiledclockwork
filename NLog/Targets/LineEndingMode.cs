using System;
using System.ComponentModel;
using System.Globalization;
using JetBrains.Annotations;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x0200015F RID: 351
	[TypeConverter(typeof(LineEndingMode.LineEndingModeConverter))]
	public sealed class LineEndingMode
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0001F855 File Offset: 0x0001DA55
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0001F85D File Offset: 0x0001DA5D
		public string NewLineCharacters
		{
			get
			{
				return this.newLineCharacters;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0001F865 File Offset: 0x0001DA65
		private LineEndingMode()
		{
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0001F86D File Offset: 0x0001DA6D
		private LineEndingMode(string name, string newLineCharacters)
		{
			this.name = name;
			this.newLineCharacters = newLineCharacters;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0001F884 File Offset: 0x0001DA84
		public static LineEndingMode FromString([NotNull] string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Equals(LineEndingMode.CRLF.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.CRLF;
			}
			if (name.Equals(LineEndingMode.LF.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.LF;
			}
			if (name.Equals(LineEndingMode.CR.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.CR;
			}
			if (name.Equals(LineEndingMode.Default.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.Default;
			}
			if (name.Equals(LineEndingMode.None.Name, StringComparison.OrdinalIgnoreCase))
			{
				return LineEndingMode.None;
			}
			throw new ArgumentOutOfRangeException("name", name, "LineEndingMode is out of range");
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0001F92C File Offset: 0x0001DB2C
		public static bool operator ==(LineEndingMode mode1, LineEndingMode mode2)
		{
			if (object.ReferenceEquals(mode1, null))
			{
				return object.ReferenceEquals(mode2, null);
			}
			return !object.ReferenceEquals(mode2, null) && mode1.NewLineCharacters == mode2.NewLineCharacters;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0001F95B File Offset: 0x0001DB5B
		public static bool operator !=(LineEndingMode mode1, LineEndingMode mode2)
		{
			if (object.ReferenceEquals(mode1, null))
			{
				return !object.ReferenceEquals(mode2, null);
			}
			return object.ReferenceEquals(mode2, null) || mode1.NewLineCharacters != mode2.NewLineCharacters;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0001F98D File Offset: 0x0001DB8D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0001F995 File Offset: 0x0001DB95
		public override int GetHashCode()
		{
			return this.NewLineCharacters.GetHashCode();
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
		public override bool Equals(object obj)
		{
			LineEndingMode lineEndingMode = obj as LineEndingMode;
			return lineEndingMode != null && this.NewLineCharacters == lineEndingMode.NewLineCharacters;
		}

		// Token: 0x04000381 RID: 897
		public static readonly LineEndingMode Default = new LineEndingMode("Default", EnvironmentHelper.NewLine);

		// Token: 0x04000382 RID: 898
		public static readonly LineEndingMode CRLF = new LineEndingMode("CRLF", "\r\n");

		// Token: 0x04000383 RID: 899
		public static readonly LineEndingMode CR = new LineEndingMode("CR", "\r");

		// Token: 0x04000384 RID: 900
		public static readonly LineEndingMode LF = new LineEndingMode("LF", "\n");

		// Token: 0x04000385 RID: 901
		public static readonly LineEndingMode None = new LineEndingMode("None", string.Empty);

		// Token: 0x04000386 RID: 902
		private readonly string name;

		// Token: 0x04000387 RID: 903
		private readonly string newLineCharacters;

		// Token: 0x02000160 RID: 352
		public class LineEndingModeConverter : TypeConverter
		{
			// Token: 0x06000D33 RID: 3379 RVA: 0x0001FA41 File Offset: 0x0001DC41
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06000D34 RID: 3380 RVA: 0x0001FA60 File Offset: 0x0001DC60
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text == null)
				{
					return base.ConvertFrom(context, culture, value);
				}
				return LineEndingMode.FromString(text);
			}
		}
	}
}
