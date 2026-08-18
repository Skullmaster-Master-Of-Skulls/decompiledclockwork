using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Xml
{
	// Token: 0x02000095 RID: 149
	[__DynamicallyInvokable]
	[Serializable]
	public class XmlQualifiedName
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00013B97 File Offset: 0x00011D97
		[__DynamicallyInvokable]
		public XmlQualifiedName() : this(string.Empty, string.Empty)
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00013BA9 File Offset: 0x00011DA9
		[__DynamicallyInvokable]
		public XmlQualifiedName(string name) : this(name, string.Empty)
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00013BB7 File Offset: 0x00011DB7
		[__DynamicallyInvokable]
		public XmlQualifiedName(string name, string ns)
		{
			this.ns = ((ns == null) ? string.Empty : ns);
			this.name = ((name == null) ? string.Empty : name);
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00013BE1 File Offset: 0x00011DE1
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ns;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00013BE9 File Offset: 0x00011DE9
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00013BF4 File Offset: 0x00011DF4
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			if (this.hash == 0)
			{
				if (XmlQualifiedName.hashCodeDelegate == null)
				{
					XmlQualifiedName.hashCodeDelegate = XmlQualifiedName.GetHashCodeDelegate();
				}
				this.hash = XmlQualifiedName.hashCodeDelegate(this.Name, this.Name.Length, 0L);
			}
			return this.hash;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00013C43 File Offset: 0x00011E43
		[__DynamicallyInvokable]
		public bool IsEmpty
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Name.Length == 0 && this.Namespace.Length == 0;
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00013C62 File Offset: 0x00011E62
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.Namespace.Length != 0)
			{
				return this.Namespace + ":" + this.Name;
			}
			return this.Name;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013C90 File Offset: 0x00011E90
		[__DynamicallyInvokable]
		public override bool Equals(object other)
		{
			if (this == other)
			{
				return true;
			}
			XmlQualifiedName xmlQualifiedName = other as XmlQualifiedName;
			return xmlQualifiedName != null && this.Name == xmlQualifiedName.Name && this.Namespace == xmlQualifiedName.Namespace;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00013CDB File Offset: 0x00011EDB
		[__DynamicallyInvokable]
		public static bool operator ==(XmlQualifiedName a, XmlQualifiedName b)
		{
			return a == b || (a != null && b != null && a.Name == b.Name && a.Namespace == b.Namespace);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00013D11 File Offset: 0x00011F11
		[__DynamicallyInvokable]
		public static bool operator !=(XmlQualifiedName a, XmlQualifiedName b)
		{
			return !(a == b);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00013D1D File Offset: 0x00011F1D
		[__DynamicallyInvokable]
		public static string ToString(string name, string ns)
		{
			if (ns != null && ns.Length != 0)
			{
				return ns + ":" + name;
			}
			return name;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00013D38 File Offset: 0x00011F38
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static XmlQualifiedName.HashCodeOfStringDelegate GetHashCodeDelegate()
		{
			if (!XmlQualifiedName.IsRandomizedHashingDisabled())
			{
				MethodInfo method = typeof(string).GetMethod("InternalMarvin32HashString", BindingFlags.Static | BindingFlags.NonPublic);
				if (method != null)
				{
					return (XmlQualifiedName.HashCodeOfStringDelegate)Delegate.CreateDelegate(typeof(XmlQualifiedName.HashCodeOfStringDelegate), method);
				}
			}
			return new XmlQualifiedName.HashCodeOfStringDelegate(XmlQualifiedName.GetHashCodeOfString);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00013D90 File Offset: 0x00011F90
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool IsRandomizedHashingDisabled()
		{
			bool result = false;
			if (!XmlQualifiedName.ReadBoolFromXmlRegistrySettings(Registry.CurrentUser, "DisableRandomizedHashingOnXmlQualifiedName", ref result))
			{
				XmlQualifiedName.ReadBoolFromXmlRegistrySettings(Registry.LocalMachine, "DisableRandomizedHashingOnXmlQualifiedName", ref result);
			}
			return result;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00013DC8 File Offset: 0x00011FC8
		[SecurityCritical]
		private static bool ReadBoolFromXmlRegistrySettings(RegistryKey hive, string regValueName, ref bool value)
		{
			try
			{
				using (RegistryKey registryKey = hive.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\XML", false))
				{
					if (registryKey != null && registryKey.GetValueKind(regValueName) == RegistryValueKind.DWord)
					{
						value = ((int)registryKey.GetValue(regValueName) == 1);
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013E34 File Offset: 0x00012034
		private static int GetHashCodeOfString(string s, int length, long additionalEntropy)
		{
			return s.GetHashCode();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00013E3C File Offset: 0x0001203C
		internal void Init(string name, string ns)
		{
			this.name = name;
			this.ns = ns;
			this.hash = 0;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00013E53 File Offset: 0x00012053
		internal void SetNamespace(string ns)
		{
			this.ns = ns;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00013E5C File Offset: 0x0001205C
		internal void Verify()
		{
			XmlConvert.VerifyNCName(this.name);
			if (this.ns.Length != 0)
			{
				XmlConvert.ToUri(this.ns);
			}
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00013E83 File Offset: 0x00012083
		internal void Atomize(XmlNameTable nameTable)
		{
			this.name = nameTable.Add(this.name);
			this.ns = nameTable.Add(this.ns);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00013EAC File Offset: 0x000120AC
		internal static XmlQualifiedName Parse(string s, IXmlNamespaceResolver nsmgr, out string prefix)
		{
			string text;
			ValidateNames.ParseQNameThrow(s, out prefix, out text);
			string text2 = nsmgr.LookupNamespace(prefix);
			if (text2 == null)
			{
				if (prefix.Length != 0)
				{
					throw new XmlException("Xml_UnknownNs", prefix);
				}
				text2 = string.Empty;
			}
			return new XmlQualifiedName(text, text2);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00013EF1 File Offset: 0x000120F1
		internal XmlQualifiedName Clone()
		{
			return (XmlQualifiedName)base.MemberwiseClone();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00013F00 File Offset: 0x00012100
		internal static int Compare(XmlQualifiedName a, XmlQualifiedName b)
		{
			if (null == a)
			{
				if (!(null == b))
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (null == b)
				{
					return 1;
				}
				int num = string.CompareOrdinal(a.Namespace, b.Namespace);
				if (num == 0)
				{
					num = string.CompareOrdinal(a.Name, b.Name);
				}
				return num;
			}
		}

		// Token: 0x04000227 RID: 551
		private static XmlQualifiedName.HashCodeOfStringDelegate hashCodeDelegate = null;

		// Token: 0x04000228 RID: 552
		private string name;

		// Token: 0x04000229 RID: 553
		private string ns;

		// Token: 0x0400022A RID: 554
		[NonSerialized]
		private int hash;

		// Token: 0x0400022B RID: 555
		[__DynamicallyInvokable]
		public static readonly XmlQualifiedName Empty = new XmlQualifiedName(string.Empty);

		// Token: 0x02000315 RID: 789
		// (Invoke) Token: 0x06002DBD RID: 11709
		private delegate int HashCodeOfStringDelegate(string s, int sLen, long additionalEntropy);
	}
}
