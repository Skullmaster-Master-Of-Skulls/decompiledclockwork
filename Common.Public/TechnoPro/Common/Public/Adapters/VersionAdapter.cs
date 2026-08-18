using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005F5 RID: 1525
	public static class VersionAdapter
	{
		// Token: 0x060030EF RID: 12527 RVA: 0x000438B8 File Offset: 0x00041AB8
		public static string SerializeVersionsToString(this IList<Version> versions)
		{
			bool flag = versions == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(",", (from g in versions
				select g.SerializeVersionToString()).ToArray<string>());
			}
			return result;
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x00043910 File Offset: 0x00041B10
		public static IList<Version> DeserializeVersionsFromString(this string versionsString)
		{
			bool flag = string.IsNullOrEmpty(versionsString);
			IList<Version> result;
			if (flag)
			{
				result = new List<Version>();
			}
			else
			{
				result = (from g in versionsString.Split(new char[]
				{
					','
				})
				select g.Trim().DeserializeVersionFromString() into h
				where h != null
				select h).ToList<Version>();
			}
			return result;
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x00043994 File Offset: 0x00041B94
		public static string SerializeVersionToString(this Version version)
		{
			return (version == null) ? "" : version.ToString();
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000439BC File Offset: 0x00041BBC
		public static Version DeserializeVersionFromString(this string versionString)
		{
			bool flag = string.IsNullOrEmpty(versionString);
			Version result;
			if (flag)
			{
				result = null;
			}
			else
			{
				try
				{
					return new Version(versionString);
				}
				catch
				{
				}
				result = null;
			}
			return result;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x00043A00 File Offset: 0x00041C00
		public static string FormatVersion(this string version)
		{
			string[] array = version.Split(new char[]
			{
				'.'
			}, StringSplitOptions.RemoveEmptyEntries);
			bool flag = array.Length == 4;
			string result;
			if (flag)
			{
				result = new Version(version).ToString();
			}
			else
			{
				bool flag2 = array.Length < 3;
				if (flag2)
				{
					throw new ArgumentException("Wrong version format. It should be Major.Minor.Build.Revision or Major.Minor.BuildRevision", "version");
				}
				try
				{
					string text = array[0];
					string text2 = array[1];
					string text3 = array[2];
					string text4 = "0";
					string text5 = "0";
					switch (text3.Length)
					{
					case 1:
						text4 = text3;
						break;
					case 2:
						text4 = text3.Substring(0, 1);
						text5 = text3.Substring(1, 1);
						break;
					case 3:
						text4 = text3.Substring(0, 2);
						text5 = text3.Substring(2, 1);
						break;
					case 4:
						text4 = text3.Substring(0, 2);
						text5 = text3.Substring(2, 2);
						break;
					}
					result = new Version(string.Format("{0}.{1}.{2}.{3}", new object[]
					{
						text,
						text2,
						text4,
						text5
					})).ToString();
				}
				catch (Exception innerException)
				{
					throw new ArgumentException("Wrong version format. It should be Major.Minor.Build.Revision or Major.Minor.BuildRevision", "version", innerException);
				}
			}
			return result;
		}
	}
}
