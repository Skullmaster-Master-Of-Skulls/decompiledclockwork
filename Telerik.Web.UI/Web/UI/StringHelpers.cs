using System;

namespace Telerik.Web.UI
{
	// Token: 0x020001CF RID: 463
	public class StringHelpers
	{
		// Token: 0x060010C9 RID: 4297 RVA: 0x0003D494 File Offset: 0x0003B694
		public static string ToCamelCase(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				return string.Empty;
			}
			if (input.Trim().Length <= 2)
			{
				return input.ToLowerInvariant();
			}
			if (input.Length > 1)
			{
				int indexOfFirtNonEmptyCharacter = StringHelpers.GetIndexOfFirtNonEmptyCharacter(input);
				return string.Format("{0}{1}", input.Substring(0, indexOfFirtNonEmptyCharacter + 1).ToLower(), input.Substring(indexOfFirtNonEmptyCharacter + 1));
			}
			return input.ToLower();
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003D500 File Offset: 0x0003B700
		internal static int GetIndexOfFirtNonEmptyCharacter(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return -1;
			}
			string text = value.TrimStart(new char[0]);
			if (text.Length <= 0)
			{
				return -1;
			}
			if (text.Length == value.Length)
			{
				return 0;
			}
			return value.Length - text.Length;
		}
	}
}
