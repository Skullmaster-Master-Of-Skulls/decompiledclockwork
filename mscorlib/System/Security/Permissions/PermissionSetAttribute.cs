using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Util;
using System.Text;

namespace System.Security.Permissions
{
	// Token: 0x0200064B RID: 1611
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PermissionSetAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06003A0A RID: 14858 RVA: 0x000C2C0E File Offset: 0x000C1C0E
		public PermissionSetAttribute(SecurityAction action) : base(action)
		{
			this.m_unicode = false;
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000C2C1E File Offset: 0x000C1C1E
		// (set) Token: 0x06003A0C RID: 14860 RVA: 0x000C2C26 File Offset: 0x000C1C26
		public string File
		{
			get
			{
				return this.m_file;
			}
			set
			{
				this.m_file = value;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000C2C2F File Offset: 0x000C1C2F
		// (set) Token: 0x06003A0E RID: 14862 RVA: 0x000C2C37 File Offset: 0x000C1C37
		public bool UnicodeEncoded
		{
			get
			{
				return this.m_unicode;
			}
			set
			{
				this.m_unicode = value;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x000C2C40 File Offset: 0x000C1C40
		// (set) Token: 0x06003A10 RID: 14864 RVA: 0x000C2C48 File Offset: 0x000C1C48
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000C2C51 File Offset: 0x000C1C51
		// (set) Token: 0x06003A12 RID: 14866 RVA: 0x000C2C59 File Offset: 0x000C1C59
		public string XML
		{
			get
			{
				return this.m_xml;
			}
			set
			{
				this.m_xml = value;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003A13 RID: 14867 RVA: 0x000C2C62 File Offset: 0x000C1C62
		// (set) Token: 0x06003A14 RID: 14868 RVA: 0x000C2C6A File Offset: 0x000C1C6A
		public string Hex
		{
			get
			{
				return this.m_hex;
			}
			set
			{
				this.m_hex = value;
			}
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000C2C73 File Offset: 0x000C1C73
		public override IPermission CreatePermission()
		{
			return null;
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000C2C78 File Offset: 0x000C1C78
		private PermissionSet BruteForceParseStream(Stream stream)
		{
			Encoding[] array = new Encoding[]
			{
				Encoding.UTF8,
				Encoding.ASCII,
				Encoding.Unicode
			};
			StreamReader streamReader = null;
			Exception ex = null;
			int num = 0;
			while (streamReader == null && num < array.Length)
			{
				try
				{
					stream.Position = 0L;
					streamReader = new StreamReader(stream, array[num]);
					return this.ParsePermissionSet(new Parser(streamReader));
				}
				catch (Exception ex2)
				{
					if (ex == null)
					{
						ex = ex2;
					}
				}
				num++;
			}
			throw ex;
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000C2D00 File Offset: 0x000C1D00
		private PermissionSet ParsePermissionSet(Parser parser)
		{
			SecurityElement topElement = parser.GetTopElement();
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.FromXml(topElement);
			return permissionSet;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000C2D24 File Offset: 0x000C1D24
		public PermissionSet CreatePermissionSet()
		{
			if (this.m_unrestricted)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			if (this.m_name != null)
			{
				return PolicyLevel.GetBuiltInSet(this.m_name);
			}
			if (this.m_xml != null)
			{
				return this.ParsePermissionSet(new Parser(this.m_xml.ToCharArray()));
			}
			if (this.m_hex != null)
			{
				return this.BruteForceParseStream(new MemoryStream(System.Security.Util.Hex.DecodeHexString(this.m_hex)));
			}
			if (this.m_file != null)
			{
				return this.BruteForceParseStream(new FileStream(this.m_file, FileMode.Open, FileAccess.Read));
			}
			return new PermissionSet(PermissionState.None);
		}

		// Token: 0x04001E21 RID: 7713
		private string m_file;

		// Token: 0x04001E22 RID: 7714
		private string m_name;

		// Token: 0x04001E23 RID: 7715
		private bool m_unicode;

		// Token: 0x04001E24 RID: 7716
		private string m_xml;

		// Token: 0x04001E25 RID: 7717
		private string m_hex;
	}
}
