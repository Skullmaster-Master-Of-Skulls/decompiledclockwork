using System;
using System.Text;

namespace System.Runtime.Versioning
{
	// Token: 0x020003D9 RID: 985
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class FrameworkName : IEquatable<FrameworkName>
	{
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000B0259 File Offset: 0x000AE459
		[__DynamicallyInvokable]
		public string Identifier
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_identifier;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000B0261 File Offset: 0x000AE461
		[__DynamicallyInvokable]
		public Version Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000B0269 File Offset: 0x000AE469
		[__DynamicallyInvokable]
		public string Profile
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_profile;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x000B0274 File Offset: 0x000AE474
		[__DynamicallyInvokable]
		public string FullName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_fullName == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.Identifier);
					stringBuilder.Append(',');
					stringBuilder.Append("Version").Append('=');
					stringBuilder.Append('v');
					stringBuilder.Append(this.Version);
					if (!string.IsNullOrEmpty(this.Profile))
					{
						stringBuilder.Append(',');
						stringBuilder.Append("Profile").Append('=');
						stringBuilder.Append(this.Profile);
					}
					this.m_fullName = stringBuilder.ToString();
				}
				return this.m_fullName;
			}
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x000B0319 File Offset: 0x000AE519
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FrameworkName);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x000B0327 File Offset: 0x000AE527
		[__DynamicallyInvokable]
		public bool Equals(FrameworkName other)
		{
			return other != null && (this.Identifier == other.Identifier && this.Version == other.Version) && this.Profile == other.Profile;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x000B0367 File Offset: 0x000AE567
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode() ^ this.Version.GetHashCode() ^ this.Profile.GetHashCode();
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000B038C File Offset: 0x000AE58C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000B0394 File Offset: 0x000AE594
		[__DynamicallyInvokable]
		public FrameworkName(string identifier, Version version) : this(identifier, version, null)
		{
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x000B03A0 File Offset: 0x000AE5A0
		[__DynamicallyInvokable]
		public FrameworkName(string identifier, Version version, string profile)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			if (identifier.Trim().Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"identifier"
				}), "identifier");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.m_identifier = identifier.Trim();
			this.m_version = (Version)version.Clone();
			this.m_profile = ((profile == null) ? string.Empty : profile.Trim());
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000B0438 File Offset: 0x000AE638
		[__DynamicallyInvokable]
		public FrameworkName(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"frameworkName"
				}), "frameworkName");
			}
			string[] array = frameworkName.Split(new char[]
			{
				','
			});
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(SR.GetString("Argument_FrameworkNameTooShort"), "frameworkName");
			}
			this.m_identifier = array[0].Trim();
			if (this.m_identifier.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalid"), "frameworkName");
			}
			bool flag = false;
			this.m_profile = string.Empty;
			int i = 1;
			while (i < array.Length)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length != 2)
				{
					throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalid"), "frameworkName");
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					try
					{
						this.m_version = new Version(text2);
						goto IL_196;
					}
					catch (Exception innerException)
					{
						throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalidVersion"), "frameworkName", innerException);
					}
					goto IL_15F;
				}
				goto IL_15F;
				IL_196:
				i++;
				continue;
				IL_15F:
				if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(SR.GetString("Argument_FrameworkNameInvalid"), "frameworkName");
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.m_profile = text2;
					goto IL_196;
				}
				goto IL_196;
			}
			if (!flag)
			{
				throw new ArgumentException(SR.GetString("Argument_FrameworkNameMissingVersion"), "frameworkName");
			}
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000B0610 File Offset: 0x000AE810
		[__DynamicallyInvokable]
		public static bool operator ==(FrameworkName left, FrameworkName right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000B0621 File Offset: 0x000AE821
		[__DynamicallyInvokable]
		public static bool operator !=(FrameworkName left, FrameworkName right)
		{
			return !(left == right);
		}

		// Token: 0x04002076 RID: 8310
		private readonly string m_identifier;

		// Token: 0x04002077 RID: 8311
		private readonly Version m_version;

		// Token: 0x04002078 RID: 8312
		private readonly string m_profile;

		// Token: 0x04002079 RID: 8313
		private string m_fullName;

		// Token: 0x0400207A RID: 8314
		private const char c_componentSeparator = ',';

		// Token: 0x0400207B RID: 8315
		private const char c_keyValueSeparator = '=';

		// Token: 0x0400207C RID: 8316
		private const char c_versionValuePrefix = 'v';

		// Token: 0x0400207D RID: 8317
		private const string c_versionKey = "Version";

		// Token: 0x0400207E RID: 8318
		private const string c_profileKey = "Profile";
	}
}
